using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Configuration
{
	class MediaConfiguration : ConfigurationBase
	{
		private bool _enableForceLogoutAudioPrompt = true;

		private bool _enableSuggestTicketFoundPrompt = true;

		/// <summary>
		/// 当被强制退出登录时，是否播放音乐提醒
		/// </summary>
		public bool EnableForceLogoutAudioPrompt
		{
			get { return _enableForceLogoutAudioPrompt; }
			set
			{
				if (value == _enableForceLogoutAudioPrompt)
					return;
				_enableForceLogoutAudioPrompt = value;
				OnPropertyChanged(nameof(EnableForceLogoutAudioPrompt));
			}
		}

		/// <summary>
		/// 查到跨站票提示
		/// </summary>
		public bool EnableSuggestTicketFoundPrompt
		{
			get { return _enableSuggestTicketFoundPrompt; }
			set
			{
				if (value == _enableSuggestTicketFoundPrompt) return;
				_enableSuggestTicketFoundPrompt = value;
				OnPropertyChanged(nameof(EnableSuggestTicketFoundPrompt));
			}
		}

		public string TicketSuccessMusicFile
		{
			get { return _ticketSuccessMusicFile; }
			set
			{
				if (value == _ticketSuccessMusicFile)
					return;
				_ticketSuccessMusicFile = value;
				OnPropertyChanged(nameof(TicketSuccessMusicFile));
			}
		}

		private bool _stopMusicIfUserOperated = true;
		private bool _musicOnSuccess = true;
		private string _ticketSuccessMusicFile = "终于等到你";

		/// <summary>
		/// 获得或设置当用户操作的时候，是否自动停止音乐
		/// </summary>
		public bool StopMusicIfUserOperated
		{
			get { return _stopMusicIfUserOperated; }
			set
			{
				if (value == _stopMusicIfUserOperated) return;
				_stopMusicIfUserOperated = value;
				OnPropertyChanged(nameof(StopMusicIfUserOperated));
			}
		}

		/// <summary>
		/// 订票成功后声音通知
		/// </summary>
		public bool MusicOnSuccess
		{
			get { return _musicOnSuccess; }
			set
			{
				if (value == _musicOnSuccess)
					return;
				_musicOnSuccess = value;
				OnPropertyChanged(nameof(MusicOnSuccess));
			}
		}

		#region 单例模式

		static MediaConfiguration _instance;
		static readonly object _lockObject = new object();

		public static MediaConfiguration Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<MediaConfiguration>("media");
						}
					}
				}

				return _instance;
			}
		}

		#endregion

	}
}
