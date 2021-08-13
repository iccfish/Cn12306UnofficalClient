using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Configuration
{
	using Newtonsoft.Json;

	internal class AutoResumeRefreshConfiguration : ConfigurationBase
	{
		#region 单例模式

		static AutoResumeRefreshConfiguration _instance;
		static readonly object _lockObject = new object();
		bool _autoCloseSubmit = true;
		int _autoCloseSubmitTimeout;
		bool _autoCloseSubmitIfNoEnoughTicket = true;
		bool _autoCloseSubmitIfNotSubmitable = true;
		bool _autoCloseSubmitIfSubmitFailed = true;
		bool _autoCloseSubmitIfAutoVcFailed = true;
		bool _autoCloseSubmitIfQueueFailedElse = true;
		bool _limitSubmitTimeNoPerformTime = true;

		public static AutoResumeRefreshConfiguration Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<AutoResumeRefreshConfiguration>("autoresume", "order");
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		public AutoResumeRefreshConfiguration()
		{
			_autoCloseSubmitTimeout = 1;
		}

		public bool AutoCloseSubmit
		{
			get { return _autoCloseSubmit; }
			set
			{
				if (value.Equals(_autoCloseSubmit)) return;
				_autoCloseSubmit = value;
				OnPropertyChanged("AutoCloseSubmit");
			}
		}

		/// <summary>
		/// 限制无操作时间
		/// </summary>
		public bool LimitSubmitTimeNoPerformTime
		{
			get { return _limitSubmitTimeNoPerformTime; }
			set
			{
				if (value.Equals(_limitSubmitTimeNoPerformTime)) return;
				_limitSubmitTimeNoPerformTime = value;
				OnPropertyChanged("LimitSubmitTimeNoPerformTime");
			}
		}

		public int AutoCloseSubmitTimeout
		{
			get { return _autoCloseSubmitTimeout; }
			set
			{
				if (value == _autoCloseSubmitTimeout) return;
				_autoCloseSubmitTimeout = value;
				OnPropertyChanged("AutoCloseSubmitTimeout");
			}
		}

		public bool AutoCloseSubmitIfNoEnoughTicket
		{
			get { return _autoCloseSubmitIfNoEnoughTicket; }
			set
			{
				if (value.Equals(_autoCloseSubmitIfNoEnoughTicket)) return;
				_autoCloseSubmitIfNoEnoughTicket = value;
				OnPropertyChanged("AutoCloseSubmitIfNoEnoughTicket");
			}
		}

		/// <summary>
		/// 其它原因的排队失败
		/// </summary>
		public bool AutoCloseSubmitIfQueueFailedElse
		{
			get { return _autoCloseSubmitIfQueueFailedElse; }
			set
			{
				if (value.Equals(_autoCloseSubmitIfQueueFailedElse)) return;
				_autoCloseSubmitIfQueueFailedElse = value;
				OnPropertyChanged("AutoCloseSubmitIfQueueFailedElse");
			}
		}

		public bool AutoCloseSubmitIfAutoVcFailed
		{
			get { return _autoCloseSubmitIfAutoVcFailed; }
			set
			{
				if (value.Equals(_autoCloseSubmitIfAutoVcFailed)) return;
				_autoCloseSubmitIfAutoVcFailed = value;
				OnPropertyChanged("AutoCloseSubmitIfAutoVcFailed");
			}
		}

		public bool AutoCloseSubmitIfSubmitFailed
		{
			get { return _autoCloseSubmitIfSubmitFailed; }
			set
			{
				if (value.Equals(_autoCloseSubmitIfSubmitFailed)) return;
				_autoCloseSubmitIfSubmitFailed = value;
				OnPropertyChanged("AutoCloseSubmitIfSubmitFailed");
			}
		}

		public bool AutoCloseSubmitIfNotSubmitable
		{
			get { return _autoCloseSubmitIfNotSubmitable; }
			set
			{
				if (value.Equals(_autoCloseSubmitIfNotSubmitable)) return;
				_autoCloseSubmitIfNotSubmitable = value;
				OnPropertyChanged("AutoCloseSubmitIfNotSubmitable");
			}
		}

		private bool _autoReloadVc = true;

		/// <summary>
		/// 加载验证码出错的时候，自动重新加载验证码
		/// </summary>
		public bool AutoReloadVc
		{
			get { return _autoReloadVc; }
			set
			{
				if (value == _autoReloadVc)
					return;
				_autoReloadVc = value;
				OnPropertyChanged(nameof(AutoReloadVc));
			}
		}


		private int _autoReloadVcTime = 500;

		/// <summary>
		/// 默认自动重新加载验证码时间间隔
		/// </summary>
		[JsonProperty("arvc1")]
		public int AutoReloadVcTime
		{
			get { return _autoReloadVcTime; }
			set
			{
				if (value == _autoReloadVcTime)
					return;
				_autoReloadVcTime = value < 100 ? 100 : value > 10000 ? 10000 : value;
				OnPropertyChanged(nameof(AutoReloadVcTime));
			}
		}
	}
}
