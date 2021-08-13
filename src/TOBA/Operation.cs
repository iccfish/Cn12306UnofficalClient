using System;
using System.Threading;

using TOBA.Configuration;
using TOBA.Media;

using Timer = System.Windows.Forms.Timer;

namespace TOBA
{
	class Operation
	{
		System.Threading.SynchronizationContext _context;

		/// <summary>
		/// 创建 <see cref="Operation" />  的新实例(Operation)
		/// </summary>
		public Operation()
		{
			_context = SynchronizationContext.Current;

			InitMedia();
		}

		protected virtual void OnMusic4SuccessStart()
		{
			Music4SuccessStart?.Invoke(this, EventArgs.Empty);
		}
		protected virtual void OnMusic4SuccessStop()
		{
			Music4SuccessStop?.Invoke(this, EventArgs.Empty);
		}

		#region 原位登录

		public static bool IsNeedRelogin(string msg)
		{
			return !string.IsNullOrEmpty(msg) && msg.IndexOf("未登录") != -1;
		}

		#endregion


		#region 静态属性

		#region 单例模式

		static Operation _instance;
		static readonly object _lockObject = new object();

		public static Operation Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new Operation();
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		#endregion



		#region 音乐控制区

		/// <summary>
		/// 检测音乐是否停止的定时器
		/// </summary>
		Timer _ticketMediaCheckTimer;

		private bool _checkTicketPromptMusic, _checkTicketSuccessMusic;

		/// <summary>
		/// 当前的音乐
		/// </summary>
		public ITicketMusic TicketPromptMusic { get; internal set; }

		public ITicketMusic TicketSuccessMusic { get; internal set; }


		/// <summary>
		/// 音乐播放已经开始
		/// </summary>
		public event EventHandler MusicPlayStarted;

		/// <summary>
		/// 引发 <see cref="MusicPlayStarted" /> 事件
		/// </summary>
		protected virtual void OnMusicPlayStarted()
		{
			var handler = MusicPlayStarted;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 音乐播放已经停止
		/// </summary>
		public event EventHandler MusicPlayStoped;

		/// <summary>
		/// 引发 <see cref="MusicPlayStoped" /> 事件
		/// </summary>
		protected virtual void OnMusicPlayStoped()
		{
			var handler = MusicPlayStoped;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 初始化音频
		/// </summary>
		void InitMedia()
		{
			_ticketMediaCheckTimer = new Timer();
			_ticketMediaCheckTimer.Interval = 100;

			_ticketMediaCheckTimer.Tick += (s, e) =>
			{
				if (_checkTicketPromptMusic && !TicketPromptMusic.IsPlaying)
				{
					_checkTicketPromptMusic = false;
					_context.Send(OnMusicPlayStoped);
				}
				if (_checkTicketSuccessMusic && !TicketSuccessMusic.IsPlaying)
				{
					_checkTicketSuccessMusic = false;
					_context.Send(OnMusic4SuccessStop);
				}

				if (!_checkTicketSuccessMusic && !_checkTicketPromptMusic)
					_ticketMediaCheckTimer.Enabled = false;

			};
		}

		/// <summary>
		/// 开始播放音乐
		/// </summary>
		public void PlayMusic()
		{
			_context.Send(OnMusicPlayStarted);
			TicketPromptMusic.Play();
			_checkTicketPromptMusic = true;
			_ticketMediaCheckTimer.Enabled = true;
		}



		public event EventHandler Music4SuccessStop;
		public event EventHandler Music4SuccessStart;

		/// <summary>
		/// 停止播放音乐
		/// </summary>
		public void StopMusic()
		{
			TicketPromptMusic.Pause();
		}

		public void PlayMusicForSuccess()
		{
			if (!MediaConfiguration.Instance.MusicOnSuccess)
				return;

			_context.Send(OnMusic4SuccessStart);
			TicketSuccessMusic.Play();
			_checkTicketSuccessMusic = true;
			_ticketMediaCheckTimer.Enabled = true;
		}

		public void StopMusicForSuccess()
		{
			TicketSuccessMusic.Pause();
		}

		#endregion
	}
}
