namespace TOBA.Configuration
{
	using DevComponents.DotNetBar;

	using Newtonsoft.Json;

	using System;
	using System.Collections.Generic;
	using System.Drawing;

	using TOBA.Entity;
	using TOBA.Interactive;

	/// <summary>
	/// 获得程序的设置
	/// </summary>

	internal class ProgramConfiguration : ConfigurationBase
	{
		/// <summary>
		/// 不发出警告的最大内存占用
		/// </summary>
		public static readonly long WraningWorksetSize = 1024 * 1024 * 400;

		bool _autoEnterLoginVcCode;
		bool _minimizeToTray;
		bool _tabsOnTop;
		bool _enableSingleProcessMode;
		bool _hideBatchQueryTip;
		bool _autoRelogin;
		bool _notSaleVerification;
		bool _enableMusicPrompt;
		string _regInfo;
		string _siteToken;
		string _siteUserName;
		string _sitePassword;
		bool _useAutoSubmitForm;
		bool _enablePreEnterVcCode;
		bool _doubleClickHeaderToCloseTab;
		static ProgramConfiguration _instance;
		char _passwordChar = '♥';
		string _currentAppVersion;
		string _lastCachedResourceVersion;
		bool _showSoftConflictWarning;
		bool _enableConflictLogin;
		bool _autoSubmitOrderVcCode;
		WebBrowserInfo _submitOrderBrowser;
		eTabStripAlignment? _mainTabPosition;
		Color? _globalColorHint;
		eStyle? _dnbGlobalStyle = eStyle.Office2010Blue;
		bool _autoReloginAfterLogout;
		int _maxReloginCount;
		bool _autoShowLoginDialog = true;
		HashSet<string> _dismissedDialogs;
		bool? _enablePromotion;

		public eTabStripAlignment? MainTabPosition
		{
			get { return _mainTabPosition; }
			set
			{
				if (value == _mainTabPosition) return;
				_mainTabPosition = value;
				OnPropertyChanged("MainTabPosition");
			}
		}

		/// <summary>
		/// 整体风格类型
		/// </summary>
		public eStyle? DnbGlobalStyle
		{
			get { return _dnbGlobalStyle; }
			set
			{
				if (value == _dnbGlobalStyle) return;
				_dnbGlobalStyle = value;
				OnPropertyChanged("DnbGlobalStyle");
			}
		}

		/// <summary>
		/// 色彩调整
		/// </summary>
		public Color? GlobalColorHint
		{
			get { return _globalColorHint; }
			set
			{
				if (value.Equals(_globalColorHint)) return;
				_globalColorHint = value;
				OnPropertyChanged("GlobalColorHint");
			}
		}

		public static ProgramConfiguration Instance
		{
			get { return _instance ?? AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<ProgramConfiguration>(); }
		}

		public ProgramConfiguration()
		{
			_instance = this;

			_autoEnterLoginVcCode = false;
			_minimizeToTray = false;
			_tabsOnTop = false;
			_enableSingleProcessMode = true;
			_hideBatchQueryTip = false;
			_autoRelogin = true;
			_notSaleVerification = false;
			_enableMusicPrompt = true;
			_regInfo = "";
			_siteToken = "";
			_sitePassword = "";
			_siteUserName = "";
			_doubleClickHeaderToCloseTab = true;
			_useAutoSubmitForm = true;
			_autoReloginAfterLogout = true;
			_enableConflictLogin = false;
			_maxReloginCount = 3;
			_dismissedDialogs = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>
		/// 获得或设置网络请求每次操作尝试的次数
		/// </summary>
		public string LastCachedResourceVersion
		{
			get { return _lastCachedResourceVersion; }
			set
			{
				if (value == _lastCachedResourceVersion)
					return;
				_lastCachedResourceVersion = value;
				OnPropertyChanged("LastCachedResourceVersion");
			}
		}

		public bool ShowSoftConflictWarning
		{
			get { return _showSoftConflictWarning; }
			set
			{
				if (value.Equals(_showSoftConflictWarning))
					return;
				_showSoftConflictWarning = value;
				OnPropertyChanged("ShowSoftConflictWarning");
			}
		}


		/// <summary>
		/// 获得或设置密码字符
		/// </summary>
		public char PasswordChar
		{
			get
			{
				//♬♫♪❀
				return _passwordChar;
			}
			set
			{
				if (_passwordChar == value)
					return;

				_passwordChar = value;
				OnPropertyChanged("PasswordChar");
			}
		}

		public bool AutoEnterLoginVcCode
		{
			get { return _autoEnterLoginVcCode; }
			set
			{
				if (value.Equals(_autoEnterLoginVcCode))
					return;
				_autoEnterLoginVcCode = value;
				OnPropertyChanged("AutoEnterLoginVcCode");
			}
		}

		/// <summary>
		/// 是否在提交订单时自动输入验证码
		/// </summary>
		public bool AutoSubmitOrderVcCode
		{
			get { return _autoSubmitOrderVcCode; }
			set
			{
				if (value.Equals(_autoSubmitOrderVcCode))
					return;
				_autoSubmitOrderVcCode = value;
				OnPropertyChanged("AutoSubmitOrderVcCode");
			}
		}

		public bool MinimizeToTray
		{
			get { return _minimizeToTray; }
			set
			{
				if (value.Equals(_minimizeToTray))
					return;
				_minimizeToTray = value;
				OnPropertyChanged("MinimizeToTray");
			}
		}

		public bool TabsOnTop
		{
			get { return _tabsOnTop; }
			set
			{
				if (value.Equals(_tabsOnTop))
					return;
				_tabsOnTop = value;
				OnPropertyChanged("TabsOnTop");
			}
		}

		public bool EnableSingleProcessMode
		{
			get { return _enableSingleProcessMode; }
			set
			{
				if (value.Equals(_enableSingleProcessMode)) return;
				_enableSingleProcessMode = value;
				OnPropertyChanged("EnableSingleProcessMode");
			}
		}

		[JsonIgnore]
		public static bool EnablePassengerManager
		{
			get { return true; }
		}

		public bool HideBatchQueryTip
		{
			get { return _hideBatchQueryTip; }
			set
			{
				if (value.Equals(_hideBatchQueryTip))
					return;
				_hideBatchQueryTip = value;
				OnPropertyChanged("HideBatchQueryTip");
			}
		}

		/// <summary>
		/// 当用户被注销时，是否自动重新登录
		/// </summary>
		public bool AutoRelogin
		{
			get { return _autoRelogin; }
			set
			{
				if (value.Equals(_autoRelogin))
					return;
				_autoRelogin = value;
				OnPropertyChanged("AutoRelogin");
			}
		}

		public bool NotSaleVerification
		{
			get { return _notSaleVerification; }
			set
			{
				if (value.Equals(_notSaleVerification))
					return;
				_notSaleVerification = value;
				OnPropertyChanged("NotSaleVerification");
			}
		}

		/// <summary>
		/// 获得或设置是否允许启用声音提示
		/// </summary>
		public bool EnableMusicPrompt
		{
			get { return _enableMusicPrompt; }
			set
			{
				if (value.Equals(_enableMusicPrompt)) return;
				_enableMusicPrompt = value;
				OnPropertyChanged("EnableMusicPrompt");
			}
		}

		public string RegInfo
		{
			get { return _regInfo; }
			set
			{
				if (value == _regInfo)
					return;
				_regInfo = value;
				//if (SoftSession.Instance != null)
				//	SoftSession.Instance.SetRegInfo(OldSecurity.DecodeString(value));
				OnPropertyChanged("RegInfo");
			}
		}

		public string SiteToken
		{
			get { return _siteToken; }
			set
			{
				if (value == _siteToken) return;
				_siteToken = value;
				//if (SoftSession.Instance != null)
				//	SoftSession.Instance.SiteToken = OldSecurity.DecodeString(value);
				OnPropertyChanged("SiteToken");
			}
		}

		public string SiteUserName
		{
			get { return _siteUserName; }
			set
			{
				if (value == _siteUserName) return;
				_siteUserName = value;
				//if (SoftSession.Instance != null)
				//	SoftSession.Instance.UserName = value;
				OnPropertyChanged("SiteUserName");
			}
		}

		public string SitePassword
		{
			get { return _sitePassword; }
			set
			{
				if (value == _sitePassword) return;

				_sitePassword = value;
				//if (SoftSession.Instance != null)
				//	SoftSession.Instance.Password = AppContext.ServcieManager.SecurityProviderEncryptor.Encrypt(value);
				OnPropertyChanged("SitePassword");
			}
		}

		/// <summary>
		/// 获得或设置是否使用快速订票模式
		/// </summary>
		public bool UseAutoSubmitForm
		{
			get { return _useAutoSubmitForm; }
			set
			{
				if (value.Equals(_useAutoSubmitForm)) return;
				_useAutoSubmitForm = value;
				OnPropertyChanged("UseAutoSubmitForm");
			}
		}

		/// <summary>
		/// 获得或设置是否允许提前输入验证码
		/// </summary>
		public bool EnablePreEnterVcCode
		{
			get { return _enablePreEnterVcCode; }
			set
			{
				if (value.Equals(_enablePreEnterVcCode)) return;
				_enablePreEnterVcCode = value;
				OnPropertyChanged("EnablePreEnterVcCode");
			}
		}

		public bool DoubleClickHeaderToCloseTab
		{
			get { return _doubleClickHeaderToCloseTab; }
			set
			{
				if (value.Equals(_doubleClickHeaderToCloseTab)) return;
				_doubleClickHeaderToCloseTab = value;
				OnPropertyChanged("DoubleClickHeaderToCloseTab");
			}
		}

		/// <summary>
		/// 同一个用户是否允许多开
		/// </summary>
		[JsonIgnore]
		public bool EnableConflictLogin
		{
			get { return _enableConflictLogin; }
			set
			{
				if (value.Equals(_enableConflictLogin)) return;
				_enableConflictLogin = value;
				OnPropertyChanged("EnableConflictLogin");
			}
		}

		/// <summary>
		/// 获得或设置用户自定义浏览器
		/// </summary>
		public WebBrowserInfo SubmitOrderBrowser
		{
			get { return _submitOrderBrowser; }
			set
			{
				if (Equals(value, _submitOrderBrowser))
					return;
				_submitOrderBrowser = value;
				OnPropertyChanged("SubmitOrderBrowser");
			}
		}

		/// <summary>
		/// 最大重新登录次数
		/// </summary>
		public int MaxReloginCount
		{
			get { return _maxReloginCount; }
			set
			{
				if (value.Equals(_maxReloginCount)) return;
				_maxReloginCount = value;
				OnPropertyChanged("MaxReloginCount");
			}
		}

		/// <summary>
		/// 没有已登录账号时是否自动显示登录
		/// </summary>
		public bool AutoShowLoginDialog
		{
			get { return _autoShowLoginDialog; }
			set
			{
				if (value.Equals(_autoShowLoginDialog))
					return;
				_autoShowLoginDialog = value;
				OnPropertyChanged("AutoShowLoginDialog");
			}
		}

		/// <summary>
		/// 已禁用提示的对话框类型
		/// </summary>
		public HashSet<string> DismissedDialogs
		{
			get { return _dismissedDialogs; }
			set
			{
				if (Equals(value, _dismissedDialogs)) return;
				_dismissedDialogs = value;
				OnPropertyChanged("DismissedDialogs");
			}
		}

		/// <summary>
		/// 获得或设置是否允许推广
		/// </summary>
		public bool? EnablePromotion
		{
			get { return _enablePromotion; }
			set
			{
				if (value.Equals(_enablePromotion))
					return;
				_enablePromotion = value;
				OnPropertyChanged("EnablePromotion");
			}
		}

		QueryParam _defaultQuery;
		/// <summary>
		/// 未登录情况下的默认查询
		/// </summary>
		public QueryParam DefaultQuery
		{
			get { return _defaultQuery ?? (_defaultQuery = new QueryParam()); }
			set
			{
				if (Equals(value, _defaultQuery)) return;
				_defaultQuery = value;
				OnPropertyChanged(nameof(DefaultQuery));
			}
		}

		private string _currentVersion;
		/// <summary>
		/// 当前版本
		/// </summary>
		public string CurrentVersion
		{
			get { return _currentVersion; }
			set
			{
				if (Equals(value, _currentVersion)) return;
				_currentVersion = value;
				OnPropertyChanged(nameof(CurrentVersion));
			}
		}

		private bool _notifyIfMobileNotChecked = true;

		/// <summary>
		/// 如果手机号没有通过审核，那么自动提醒
		/// </summary>
		public bool NotifyIfMobileNotChecked
		{
			get { return _notifyIfMobileNotChecked; }
			set
			{
				if (value == _notifyIfMobileNotChecked) return;
				_notifyIfMobileNotChecked = value;
				OnPropertyChanged(nameof(NotifyIfMobileNotChecked));
			}
		}

		private bool _orderDlgCenterMainform = true;

		/// <summary>
		/// 订单窗口是否主窗口居中
		/// </summary>
		public bool OrderDlgCenterMainform
		{
			get { return _orderDlgCenterMainform; }
			set
			{
				if (value == _orderDlgCenterMainform) return;
				_orderDlgCenterMainform = value;
				OnPropertyChanged(nameof(OrderDlgCenterMainform));
			}
		}

		private int _captchaZoom = 15;

		/// <summary>
		/// 验证码缩放倍数
		/// </summary>
		public int CaptchaZoom
		{
			get { return _captchaZoom; }
			set
			{
				if (value == _captchaZoom) return;
				_captchaZoom = value;
				OnPropertyChanged(nameof(CaptchaZoom));
			}
		}

		private bool _showIPBlockTip = true;

		/// <summary>
		/// 显示IP被封锁警告
		/// </summary>
		public bool ShowIPBlockTip
		{
			get { return _showIPBlockTip; }
			set
			{
				if (value == _showIPBlockTip) return;
				_showIPBlockTip = value;
				OnPropertyChanged(nameof(ShowIPBlockTip));
			}
		}


		private bool _showMessageFrom12306 = true;

		/// <summary>
		/// 显示来自于12306的通知
		/// </summary>
		public bool ShowMessageFrom12306
		{
			get { return _showMessageFrom12306; }
			set
			{
				if (value == _showMessageFrom12306) return;
				_showMessageFrom12306 = value;
				OnPropertyChanged(nameof(ShowMessageFrom12306));
			}
		}

		private RunningMode _mode = RunningMode.Professional;

		/// <summary>
		/// 运行模式
		/// </summary>
		public RunningMode Mode
		{
			get { return _mode; }
			set
			{
				if (value == _mode)
					return;
				_mode = value;
				OnModeChanged();
				OnPropertyChanged(nameof(Mode));
			}
		}

		/// <summary>
		/// <see cref="Mode" /> 发生变化
		/// </summary>
		public event EventHandler ModeChanged;

		/// <summary>
		/// 引发 <see cref="ModeChanged"/> 事件
		/// </summary>

		protected virtual void OnModeChanged()
		{
			ModeChanged?.Invoke(this, EventArgs.Empty);
		}

		private bool _keepQueryStateAfterRestart = true;

		public bool KeepQueryStateAfterRestart
		{
			get { return _keepQueryStateAfterRestart; }
			set
			{
				if (value == _keepQueryStateAfterRestart)
					return;
				_keepQueryStateAfterRestart = value;
				OnPropertyChanged(nameof(KeepQueryStateAfterRestart));
			}
		}

		private bool _keepLoginStateAfterRestart = true;

		public bool KeepLoginStateAfterRestart
		{
			get { return _keepLoginStateAfterRestart; }
			set
			{
				if (value == _keepLoginStateAfterRestart)
					return;
				_keepLoginStateAfterRestart = value;
				OnPropertyChanged(nameof(KeepLoginStateAfterRestart));
			}
		}
		private bool _autoSendLoginVerifySms;
		public bool AutoSendLoginVerifySms
		{
			get => _autoSendLoginVerifySms;
			set
			{
				if (value == _autoSendLoginVerifySms) return;
				_autoSendLoginVerifySms = value;
				OnPropertyChanged(nameof(AutoSendLoginVerifySms));
			}
		}
	}
}
