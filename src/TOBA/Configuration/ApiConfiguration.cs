using System;
using System.Collections.Generic;

namespace TOBA.Configuration
{
	internal class ApiConfiguration : ConfigurationBase
	{
		private bool _disableIllegalDetect = false;

		private bool _enableAlwaysSendQueryLog;

		private bool _enableNetworkDiag = false;

		bool _fastSubmitOrderSkipVc;

		int _maxPassengerCount = 15;

		int _minDeletePassengerDays = 180;

		private int _networkDiagEntryCount = 50;

		public int MinimalVcWaitTime
		{
			get => _minimalVcWaitTime;
			set
			{
				if (value == _minimalVcWaitTime)
					return;
				_minimalVcWaitTime = value;
				OnPropertyChanged(nameof(MinimalVcWaitTime));
				MinimalVcWaitTimeRunTime = value;
			}
		}

		public int MinimalVcWaitTimeRunTime
		{
			get => _minimalVcWaitTimeRunTime;
			set
			{
				if (value == _minimalVcWaitTimeRunTime)
					return;
				_minimalVcWaitTimeRunTime = value;
				OnPropertyChanged(nameof(MinimalVcWaitTimeRunTime));
			}
		}


		/// <summary>
		/// 创建 <see cref="ApiConfiguration" />  的新实例(ApiConfiguration)
		/// </summary>
		public ApiConfiguration()
		{
			ResetDefault();
		}

		public void ResetDefault()
		{
			MinimalVcWaitTime = 1000;
			_fastSubmitOrderSkipVc = false;

		}

		/// <summary>
		/// 是否禁用无效数据监测
		/// </summary>
		public bool DisableIllegalDetect
		{
			get => _disableIllegalDetect;
			set
			{
				if (value == _disableIllegalDetect) return;
				_disableIllegalDetect = value;
				OnPropertyChanged(nameof(DisableIllegalDetect));
			}
		}

		/// <summary>
		/// 启用总是发送QueryLog
		/// </summary>
		public bool EnableAlwaysSendQueryLog
		{
			get => _enableAlwaysSendQueryLog;
			set
			{
				if (value == _enableAlwaysSendQueryLog) return;
				_enableAlwaysSendQueryLog = value;
				OnPropertyChanged(nameof(EnableAlwaysSendQueryLog));
			}
		}

		public bool EnableNetworkDiag
		{
			get => _enableNetworkDiag;
			set
			{
				if (value == _enableNetworkDiag)
					return;
				_enableNetworkDiag = value;
				OnPropertyChanged(nameof(EnableNetworkDiag));
			}
		}

		/// <summary>
		/// 快速提交订单是否跳过验证码
		/// </summary>
		public bool FastSubmitOrderSkipVc
		{
			get => _fastSubmitOrderSkipVc;
			set
			{
				if (value == _fastSubmitOrderSkipVc) return;
				_fastSubmitOrderSkipVc = value;
				OnPropertyChanged(nameof(FastSubmitOrderSkipVc));
			}
		}

		public int LastSysMessageId
		{
			get => _lastSysMessageId;
			set
			{
				if (value == _lastSysMessageId)
					return;
				_lastSysMessageId = value;
				OnPropertyChanged(nameof(LastSysMessageId));
			}
		}

		/// <summary>
		/// 最多联系人数目
		/// </summary>
		public int MaxPassengerCount
		{
			get => _maxPassengerCount;
			set
			{
				if (value == _maxPassengerCount) return;
				_maxPassengerCount = value;
				OnPropertyChanged(nameof(MaxPassengerCount));
			}
		}

		/// <summary>
		/// 最小可删日期
		/// </summary>
		public int MinDeletePassengerDays
		{
			get => _minDeletePassengerDays;
			set
			{
				if (value == _minDeletePassengerDays) return;
				_minDeletePassengerDays = value;
				OnPropertyChanged(nameof(MinDeletePassengerDays));
			}
		}

		/// <summary>
		/// 网络记录最多数目
		/// </summary>
		public int NetworkDiagEntryCount
		{
			get => _networkDiagEntryCount;
			set
			{
				if (value == _networkDiagEntryCount) return;
				_networkDiagEntryCount = value;
				OnPropertyChanged(nameof(NetworkDiagEntryCount));
			}
		}

		/// <summary>
		/// 被检测的非法关键字检测
		/// </summary>
		public HashSet<string> ResubmitTextDetection
		{
			get
			{
				if (_resubmitTextDetection == null)
					_resubmitTextDetection = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

				if (_resubmitTextDetection.Count == 0)
				{
					_resubmitTextDetection.SafeAdd("第三方");
					_resubmitTextDetection.SafeAdd("非法请求");
				}

				return _resubmitTextDetection;
			}
			set
			{
				if (Equals(value, _resubmitTextDetection)) return;
				_resubmitTextDetection = value;
				OnPropertyChanged(nameof(ResubmitTextDetection));
			}
		}
		public int TimeoutRecordCount
		{
			get => _timeoutRecordCount;
			set
			{
				if (value == _timeoutRecordCount)
					return;
				_timeoutRecordCount = value;
				OnPropertyChanged(nameof(TimeoutRecordCount));
			}
		}
		/// <summary>
		/// 发出警告的最小值
		/// </summary>
		public int TimeoutWarningLimit
		{
			get => _timeoutWarningLimit;
			set
			{
				if (value == _timeoutWarningLimit)
					return;
				_timeoutWarningLimit = value;
				OnPropertyChanged(nameof(TimeoutWarningLimit));
			}
		}

		public bool EnableDynamicJs { get; set; } = false;

		public bool EnableHttpZf { get; set; } = true;

		public int NetworkMaxRetry { get; set; } = 5;

		/// <summary>
		/// 是否尝试挽救掉线
		/// </summary>
		public bool EnableLogoutRescure { get; set; } = false;

		#region 单例模式

		static ApiConfiguration _instance;
		static readonly object _lockObject = new object();
		string _transitApi;
		string _trainSuggestionApi;
		string _stationReportApi;
		string _sameCityStationApi;
		string _stationDataUpdateUrl;
		int _sysMessageId;
		int _lastSysMessageId;
		string _systemMessageTitle;
		string _systemMessage;
		HashSet<string> _resubmitTextDetection;
		private bool _skipNoVcWait = true;
		private int _minimalVcWaitTime = 1000;
		private int _minimalVcWaitTimeRunTime;
		private double _trainSuggestMaxRatio = 0.7;
		private bool _enableTicketDataGuess = true;
		private int _timeoutRecordCount = 6;
		private int _timeoutWarningLimit = 3;
		private bool _enableTicketFillService = false;

		public bool SkipNoVcWait
		{
			get => _skipNoVcWait;
			set
			{
				if (value == _skipNoVcWait)
					return;
				_skipNoVcWait = value;
				OnPropertyChanged(nameof(SkipNoVcWait));
			}
		}

		public static ApiConfiguration Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new ApiConfiguration();
						}
					}
				}

				return _instance;
			}
		}

		public bool EnableTicketFillService
		{
			get => _enableTicketFillService;
			set
			{
				if (value == _enableTicketFillService)
					return;
				_enableTicketFillService = value;
				OnPropertyChanged(nameof(EnableTicketFillService));
			}
		}

		#endregion

		public List<string> TicketStatusMsg { get; set; } = new List<string>()
		{
			"欢迎使用订票助手.NET",
			"订票是一个技术活，还请提前做好规划",
			"请主动拒绝黄牛、付费暴力软件，净化购票氛围",
			"祝您购票顺利",
			"实在买不到票吗？群里的小伙伴没准能帮助你哦",
			"觉得订票助手不错的话，欢迎关注鱼的微博和博客哦，不差银子的也欢迎捐助哦",
			"不要刷太快！否则会封得很快！",
			"候补不影响刷票！能提交候补的时候尽量提交候补！"
		};

		/// <summary>
		/// 是否启用快速订单提交
		/// </summary>
		public bool EnableAutoSubmitAPI { get; set; } = false;


		/// <summary>
		/// 是否启用验证码加载延迟
		/// </summary>
		public bool EnableVerifycodeLoadDelay { get; set; } = true;

		/// <summary>
		/// 是否启用验证码提交延迟
		/// </summary>
		public bool EnableVerifycodeSubmitDelay { get; set; } = true;

		public int SystemBusyRecordCount { get; set; } = 10;

		public int SystemBusyWarningLimit { get; set; } = 6;
		
		/// <summary>
		/// 短信验证码等待起效时间
		/// </summary>
		public int SmsVerificationTimeWait { get; set; } = 5000;

		/// <summary>
		/// Gets or sets the system open time.
		/// </summary>
		/// <value>
		/// The system open time.
		/// </value>
		public int SystemOpenTime { get; set; } = 5 * 60;

		/// <summary>
		/// Gets or sets the system close time.
		/// </summary>
		/// <value>
		/// The system close time.
		/// </value>
		public int SystemCloseTime { get; set; } = 24 * 60;

		public int OrderSubmitOpenTime { get; set; } = 5 * 60;

		public int OrderSubmitCloseTime { get; set; } = 23 * 60 + 30;
	}
}
