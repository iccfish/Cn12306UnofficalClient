using System;
using System.Linq;

namespace TOBA.Media
{
	using System.Threading.Tasks;

	abstract class BaseMp3Player : IDisposable
	{
		private MciController _controller;


		private string _musicPath;

		/// <summary>
		/// 暂停播放
		/// </summary>
		public void Pause()
		{
			_controller.Pause();
		}


		/// <summary>
		/// 播放
		/// </summary>
		public void Play()
		{
			_controller.Replay();
		}

		public async Task PlayAsync()
		{
			_controller.Replay();
		}

		/// <summary>
		/// 获得或设置是否正在播放
		/// </summary>
		public bool IsPlaying => _controller.IsPlaying;

		public string MusicPath
		{
			get => _musicPath;
			set
			{
				if (_musicPath == value)
				{
					return;
				}

				CheckDisposed();

				_musicPath = value;
				_controller?.Dispose();
				_controller = new MciController(value);
			}
		}


		#region Dispose方法实现

		bool _disposed;

		protected BaseMp3Player(string musicPath) { MusicPath = musicPath; }

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

			_controller.Dispose();

			//挂起终结器
			GC.SuppressFinalize(this);
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
