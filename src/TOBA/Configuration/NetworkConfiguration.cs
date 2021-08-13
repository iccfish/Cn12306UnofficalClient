using Newtonsoft.Json;

using System;

namespace TOBA.Configuration
{
	using TOBA.WebLib;

	internal class NetworkConfiguration : ConfigurationBase
	{
		/// <summary>
		/// 获得当前的配置
		/// </summary>
		public static NetworkConfiguration Current
		{
			get { return _current ?? AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<NetworkConfiguration>("main", "Network"); }
			private set
			{
				_current = value;
			}
		}

		public NetworkConfiguration()
		{
			Current = this;
			_networkRetryCount = 3;
			_proxyType = 0;
			_proxyAddress = "";
			_proxyPassword = "";
			_proxyPort = 80;
			_proxyUserName = "";
			_disableCdn = true;
			_baseUri = new Uri("https://kyfw.12306.cn/otn/");
			_enableServerSmartChange = true;
			_useSeperatorIpForQuery = _enableServerSmartChange;
			_hostFailedLimit = 20;
			_autoExcludeLongCacheHost = true;
			_reloadVcCodeDelay = 500;
			_ban405ReturnHost = true;
			_checkLoginStateInterval = 15;
			_enableAutoReloadDnsServer = true;
			_autoReloadDnsLimit = 15;
		}



		int _networkRetryCount;
		string _proxyAddress;
		int _proxyPort;
		string _proxyUserName;
		string _proxyPassword;
		int _proxyType;
		bool _disableCdn;
		Uri _baseUri;
		bool _enableServerSmartChange;
		bool _canEnableServerSmartChange;
		bool _useSeperatorIpForQuery;
		int _hostFailedLimit;
		bool _autoExcludeLongCacheHost;
		int _reloadVcCodeDelay;
		bool _ban405ReturnHost;
		int _checkLoginStateInterval;
		bool _enableAutoReloadDnsServer;
		int _autoReloadDnsLimit;
		static NetworkConfiguration _current;

		/// <summary>
		/// 重新加载验证码延迟
		/// </summary>
		[JsonProperty("rvcd1")]
		public int ReloadVcCodeDelay
		{
			get { return _reloadVcCodeDelay; }
			set
			{
				if (value == _reloadVcCodeDelay) return;
				_reloadVcCodeDelay = value;
				OnPropertyChanged("ReloadVcCodeDelay");
			}
		}

		/// <summary>
		/// 获得或设置网络请求每次操作尝试的次数
		/// </summary>
		public int NetworkRetryCount
		{
			get { return _networkRetryCount; }
			set
			{
				if (value < 1) value = 1;
				if (value == _networkRetryCount) return;


				_networkRetryCount = value;
			}
		}

		/// <summary>
		/// 获得或设置代理服务器地址
		/// </summary>
		public string ProxyAddress
		{
			get { return _proxyAddress; }
			set
			{
				if (value == _proxyAddress) return;
				_proxyAddress = value;
				OnPropertyChanged("ProxyAddress");
			}
		}

		/// <summary>
		/// 获得或设置代理服务器端口
		/// </summary>
		public int ProxyPort
		{
			get { return _proxyPort; }
			set
			{
				if (value == _proxyPort) return;
				_proxyPort = value;
				OnPropertyChanged("ProxyPort");
			}
		}

		/// <summary>
		/// 获得或设置代理服务器用户名
		/// </summary>
		public string ProxyUserName
		{
			get { return _proxyUserName; }
			set
			{
				if (value == _proxyUserName) return;
				_proxyUserName = value;
				OnPropertyChanged("ProxyUserName");
			}
		}

		/// <summary>
		/// 获得或设置代理服务器密码
		/// </summary>
		public string ProxyPassword
		{
			get { return _proxyPassword; }
			set
			{
				if (value == _proxyPassword) return;
				_proxyPassword = value;
				OnPropertyChanged("ProxyPassword");
			}
		}

		/// <summary>
		/// 获得或设置是否使用系统默认代理
		/// </summary>
		public int ProxyType
		{
			get { return _proxyType; }
			set
			{
				if (value.Equals(_proxyType)) return;
				_proxyType = value;
				OnPropertyChanged("ProxyType");
			}
		}

		/// <summary>
		/// 获得或设置是否禁止CDN服务器的缓存
		/// </summary>
		public bool DisableCdn
		{
			get { return _disableCdn; }
			set
			{
				if (value.Equals(_disableCdn)) return;
				_disableCdn = value;
				OnPropertyChanged("DisableCdn");
			}
		}

		public Uri BaseUri
		{
			get { return _baseUri; }
			set
			{
				if (value == _baseUri)
					return;
				_baseUri = value;
				OnPropertyChanged("BaseUri");
			}
		}


		public int CheckLoginStateInterval
		{
			get { return _checkLoginStateInterval; }
			set
			{
				if (value == _checkLoginStateInterval)
					return;
				_checkLoginStateInterval = value;
				OnPropertyChanged("CheckLoginStateInterval");
			}
		}

		/// <summary>
		/// 获得或设置是否允许自动刷新DNS
		/// </summary>
		public bool EnableAutoReloadDnsServer
		{
			get { return _enableAutoReloadDnsServer; }
			set
			{
				if (value.Equals(_enableAutoReloadDnsServer))
					return;
				_enableAutoReloadDnsServer = value;
				OnPropertyChanged("EnableAutoReloadDnsServer");
			}
		}

		/// <summary>
		/// 自动刷新DNS缓存阈值
		/// </summary>
		public int AutoReloadDnsLimit
		{
			get { return _autoReloadDnsLimit; }
			set
			{
				if (value == _autoReloadDnsLimit)
					return;
				_autoReloadDnsLimit = value;
				OnPropertyChanged("AutoReloadDnsLimit");
			}
		}

		private ProxyType _proxyClass;

		public ProxyType ProxyClass
		{
			get { return _proxyClass; }
			set
			{
				if (value == _proxyClass) return;
				_proxyClass = value;
				OnPropertyChanged(nameof(ProxyClass));
			}
		}


		private string _socks5ServerAddr;

		public string Socks5ServerAddr
		{
			get { return _socks5ServerAddr; }
			set
			{
				if (value == _socks5ServerAddr) return;
				_socks5ServerAddr = value;
				OnPropertyChanged(nameof(Socks5ServerAddr));
			}
		}

		private int _socks5ServerPort = 1080;

		public int Socks5ServerPort
		{
			get { return _socks5ServerPort; }
			set
			{
				if (value == _socks5ServerPort) return;
				_socks5ServerPort = value;
				OnPropertyChanged(nameof(Socks5ServerPort));
			}
		}

		private bool _autoRetryOnNetworkError = true;

		/// <summary>
		/// 出现网络错误时自动重试
		/// </summary>
		public bool AutoRetryOnNetworkError
		{
			get { return _autoRetryOnNetworkError; }
			set
			{
				if (value == _autoRetryOnNetworkError) return;
				_autoRetryOnNetworkError = value;
				OnPropertyChanged(nameof(AutoRetryOnNetworkError));
			}
		}

		private int _retryMaxCount = 5;

		/// <summary>
		/// 最高重试次数
		/// </summary>
		public int RetryMaxCount
		{
			get { return _retryMaxCount; }
			set
			{
				if (value == _retryMaxCount) return;
				_retryMaxCount = value;
				OnPropertyChanged(nameof(RetryMaxCount));
			}
		}

		private int _retrySleepTime = 1000;

		/// <summary>
		/// 重试休息时间
		/// </summary>
		public int RetrySleepTime
		{
			get { return _retrySleepTime; }
			set
			{
				if (value == _retrySleepTime) return;
				_retrySleepTime = value;
				OnPropertyChanged(nameof(RetrySleepTime));
			}
		}

		private int _vcSubmitDelay = 4000;

		/// <summary>
		/// 获得或设置验证码提交延迟
		/// </summary>
		public int VcSubmitDelay
		{
			get { return _vcSubmitDelay; }
			set
			{
				if (value == _vcSubmitDelay) return;
				_vcSubmitDelay = value;
				OnPropertyChanged(nameof(VcSubmitDelay));
			}
		}
	}
}
