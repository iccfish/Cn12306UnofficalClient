namespace TOBA.Service
{
	using TOBA.Configuration;

	/// <summary>
	/// 验证码识别引擎配置
	/// </summary>
	internal class AutoVcConfig : ConfigurationBase
	{
		#region 单例模式

		static AutoVcConfig _instance;
		static readonly object _lockObject = new object();

		public static AutoVcConfig Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<AutoVcConfig>("autovc", "platform");
						}
					}
				}

				return _instance;
			}
		}

		#endregion


		string _activeVcEngine;
		string _userName;
		string _pasword;

		/// <summary>
		/// 活动的Vc引擎
		/// </summary>
		public string ActiveVcEngine
		{
			get { return _activeVcEngine; }
			set
			{
				if (value == _activeVcEngine) return;
				_activeVcEngine = value;
				OnPropertyChanged(nameof(ActiveVcEngine));
			}
		}


		public string UserName
		{
			get { return _userName; }
			set
			{
				if (value == _userName)
					return;
				_userName = value;
				OnPropertyChanged(nameof(UserName));
			}
		}

		public string Pasword
		{
			get { return _pasword; }
			set
			{
				if (value == _pasword)
					return;
				_pasword = value;
				OnPropertyChanged(nameof(Pasword));
			}
		}

		bool _autoReloadIfNotSuccess = true;

		/// <summary>
		/// 获得或设置如果没有能识别，是否自动重新载入验证码
		/// </summary>
		public bool AutoReloadIfNotSuccess
		{
			get { return _autoReloadIfNotSuccess; }
			set
			{
				if (value == _autoReloadIfNotSuccess)
					return;
				_autoReloadIfNotSuccess = value;
				OnPropertyChanged(nameof(AutoReloadIfNotSuccess));
			}
		}

		int _maxGiveupFailed = 5;

		/// <summary>
		/// 获得或设置连续识别失败多少次时确定为失败
		/// </summary>
		public int MaxGiveupFailed
		{
			get { return _maxGiveupFailed; }
			set
			{
				if (value == _maxGiveupFailed)
					return;
				_maxGiveupFailed = value;
				OnPropertyChanged(nameof(MaxGiveupFailed));
			}
		}

		private int _maxGiveupError = 5;

		/// <summary>
		/// 连续识别错误多少次后放弃识别
		/// </summary>
		public int MaxGiveupError
		{
			get { return _maxGiveupError; }
			set
			{
				if (value == _maxGiveupError) return;
				_maxGiveupError = value;
				OnPropertyChanged(nameof(MaxGiveupError));
			}
		}

		private AutoVcConflictResult _vcResultConflict = AutoVcConflictResult.Ignore;

		public AutoVcConflictResult VcResultConflict
		{
			get { return _vcResultConflict; }
			set
			{
				if (value == _vcResultConflict) return;
				_vcResultConflict = value;
				OnPropertyChanged(nameof(VcResultConflict));
			}
		}
	}

	public enum AutoVcConflictResult
	{
		None = 0,
		Ignore = 1,
		ClearUser = 2
	}
}