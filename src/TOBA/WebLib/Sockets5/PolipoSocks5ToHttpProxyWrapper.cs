using System;

namespace TOBA.WebLib.Sockets5
{
	using System.Diagnostics;
	using System.IO;
	using System.Net;
	using System.Net.Sockets;
	using System.Reflection;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Timers;

	using Timer = System.Timers.Timer;

	/// <summary>
	/// 使用Polipo将Socks5转换为HTTP代理的辅助类
	/// </summary>
	public class PolipoSocks5ToHttpProxyWrapper : IDisposable
	{
		static PolipoSocks5ToHttpProxyWrapper()
		{
			var baseDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

			_polipoPath = Path.Combine(baseDir, "polipo.exe");

			if (!File.Exists(_polipoPath))
			{
				//search
				_polipoPath = Path.Combine(baseDir, System.Configuration.ConfigurationManager.AppSettings["polipo_path"], "polipo.exe");
			}
		}


		readonly static string _polipoPath;

		readonly object _mutex = new object();

		int _failedCount;

		private int _pingInterval;

		/// <summary>
		/// 当前的进程
		/// </summary>
		Process _process;

		Timer _timer;

		private PolipoSocks5ToHttpProxyWrapper()
		{
			PingInterval = 5;
		}

		private Uri _localUri;

		/// <summary>
		/// 获得代理服务器的本地路径
		/// </summary>
		public Uri LocalUri
		{
			get { return _localUri; }
			set { _localUri = value; }
		}

		private int _localPort = 0;

		/// <summary>
		/// 本地端口
		/// </summary>
		public int LocalPort
		{
			get { return _localPort; }
			set
			{
				if (_localPort == value)
					return;
				_localPort = value;
				LocalUri = new Uri("http://127.0.0.1:" + value);
				LocalWebProxy = new WebProxy(LocalUri);
			}
		}

		/// <summary>
		/// 获得本地的Web代理服务器
		/// </summary>
		public IWebProxy LocalWebProxy { get; private set; }

		/// <summary>
		/// 获得后设置上游服务器地址
		/// </summary>
		public string ParentSocksServerAddress { get; set; }

		/// <summary>
		/// 获得或设置上游服务器端口
		/// </summary>
		public int ParentSocksServerPort { get; set; } = 1080;

		/// <summary>
		/// 获得当前的服务状态
		/// </summary>
		public bool IsRunning
		{
			get
			{
				lock (_mutex)
				{
					return _process?.HasExited == false;
				}
			}
		}

		/// <summary>
		/// 限制从启动到正常工作的时间限制，默认为30秒
		/// </summary>
		public int StartTimeLimit { get; set; } = 30;

		/// <summary>
		/// Ping判断是否正常的周期。默认为5秒。如果设置为0，则禁用此功能
		/// </summary>
		public int PingInterval
		{
			get { return _pingInterval; }
			set
			{
				if (value == _pingInterval)
					return;

				lock (_mutex)
				{
					_pingInterval = value;
					if (_pingInterval <= 0)
					{
						_timer.Stop();
						_timer = null;
					}
					else
					{
						if (_timer == null)
						{
							_timer = new Timer() { AutoReset = false };
							_timer.Elapsed += _timer_Elapsed;
						}

						_timer.Stop();
						_timer.Interval = value * 1000;
						if (IsRunning)
							_timer.Start();

					}
				}
			}
		}

		/// <summary>
		/// Ping探测连续失败次数限制，默认为3次。如果连续失败超过此限制，将会被重启。
		/// </summary>
		public int PingFailedLimit { get; set; } = 3;

		/// <summary>
		/// 当服务异常退出时是否自动重启
		/// </summary>
		public bool AutoRestartIfTerminated { get; set; }

		private void _timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (CheckServiceResponseOk())
			{
				_failedCount = 0;
			}
			else
			{
				_failedCount++;

				if (_failedCount >= PingFailedLimit)
				{
					_failedCount = 0;
					Stop().ContinueWith(_ => Start()).ContinueWith(_ =>
					{
						if (IsRunning)
							_timer.Start();
					});

					return;
				}
			}
			_timer.Start();
		}

		/// <summary>
		/// 测试服务器响应
		/// </summary>
		/// <returns></returns>
		bool CheckServiceResponseOk()
		{
			var url = $"http://localhost:{LocalPort}/polipo/status";
			Debug.WriteLine("Checking polipo status...");
			using (var client = new WebClient())
			{
				try
				{
					client.DownloadData(url);

					return true;
				}
				catch (Exception ex)
				{
					Debug.WriteLine("Failed to check polipo status. error: " + ex.Message);
				}

				return false;
			}
		}

		public Task Start()
		{
			Debug.WriteLine("Try to start polipo server...");
			if (IsRunning)
				throw new InvalidOperationException();

			return Task.Factory.StartNew(() =>
			{
				lock (_mutex)
				{
					if (IsRunning)
						return;

					LocalPort = GetLocalPort();

					_process = Process.Start(new ProcessStartInfo(_polipoPath, $"-c \"\" socksParentProxy={ParentSocksServerAddress}:{ParentSocksServerPort} socksProxyType=socks5 diskCacheRoot= localDocumentRoot= proxyPort={LocalPort}") { WindowStyle = ProcessWindowStyle.Hidden });
					Debug.WriteLine("polipo process started.");

					var limitTime = DateTime.Now.AddSeconds(StartTimeLimit);

					while (_process?.HasExited == false && !CheckServiceResponseOk() && DateTime.Now < limitTime)
					{
						Debug.WriteLine("polipo not ready.");
						Thread.Sleep(500);
					}
					Debug.WriteLine("polipo got ready.");
					_process.EnableRaisingEvents = true;
					_process.Exited += _process_Exited;

					if (!IsRunning)
					{
						Debug.WriteLine("polipo failed to start.");
						Stop();
						OnStartFailed();
					}
					else
					{
						Debug.WriteLine("polipo started.");
						OnStarted();
						_timer?.Start();
					}
				}
			});
		}

		private void _process_Exited(object sender, EventArgs e)
		{
			Stop().Wait();
			Start().Wait();
		}

		public Task Stop()
		{
			Debug.WriteLine("Start task to stop polipo server...");
			if (_process != null)
				_process.Exited -= _process_Exited;

			return Task.Factory.StartNew(() =>
			{
				Debug.WriteLine("task to stop polipo server running...");
				lock (_mutex)
				{
					Debug.WriteLine("Got executing lock, stoping...");
					StopInternal();
				}
			});
		}

		void StopInternal()
		{
			_timer?.Stop();
			if (_process?.HasExited == false)
			{
				Debug.WriteLine("Killing polipo process...");
				_process.Kill();
				OnKilled();
			}
			Debug.WriteLine("polipo stopped.");

			_process = null;
			OnStopped();
		}

		public event EventHandler Started;

		public event EventHandler StartFailed;


		public event EventHandler Stopped;

		public event EventHandler Killed;

		protected virtual void OnStarted()
		{
			Started?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnStartFailed()
		{
			StartFailed?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnStopped()
		{
			Stopped?.Invoke(this, EventArgs.Empty);
		}

		protected virtual void OnKilled()
		{
			Killed?.Invoke(this, EventArgs.Empty);
		}

		int GetLocalPort()
		{
			var port = LocalPort;
			if (port < 1025)
				port = 1025;

			while (port < 65535)
			{
				try
				{
					using (var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
					{
						socket.Bind(new IPEndPoint(IPAddress.Loopback, port));
						socket.Close();
					}
					return port;
				}
				catch (Exception)
				{
				}
				port++;
			}

			throw new Exception("Unable to get a available local port.");
		}

		#region 单例模式

		static PolipoSocks5ToHttpProxyWrapper _instance;
		static readonly object _lockObject = new object();

		public static PolipoSocks5ToHttpProxyWrapper Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new PolipoSocks5ToHttpProxyWrapper();
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		#region Dispose方法实现

		bool _disposed;

		/// <summary>
		/// 释放资源
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;
			_disposed = true;

			if (disposing)
			{
			}
			StopInternal();

			//挂起终结器
			GC.SuppressFinalize(this);
		}

		~PolipoSocks5ToHttpProxyWrapper()
		{
			StopInternal();
		}

		/// <summary>
		/// 检查是否已经被销毁。如果被销毁，则抛出异常
		/// </summary>
		/// <exception cref="ObjectDisposedException">对象已被销毁</exception>
		protected void CheckDisposed()
		{
			if (_disposed)
				throw new ObjectDisposedException(this.GetType().Name);
		}


		#endregion


	}
}
