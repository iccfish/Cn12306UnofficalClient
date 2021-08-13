using System;
using System.ComponentModel;

namespace TOBA.Configuration
{

	using Newtonsoft.Json;

	internal class QueryConfiguration : ConfigurationBase, INotifyPropertyChanged
	{
		static QueryConfiguration _current;

		private bool _alwaysSendingQueryLog = true;
		int _anonymousQueryRate;
		double _autoDelayAfterOClock;
		int _autoSuggestCount;
		bool _enableAutoSuggest;

		bool _enableStartStationTip = true;

		private bool _ignoreAlmostIllegalResult = true;
		bool _ignoreCacheResult;
		bool _ignoreServerError;
		bool _ignoreTempTrainClass;

		string _musicPath;
		decimal _querySleep;

		private int _queryTimeout = 20000;

		private int? _speedingQueryOnOClock = 1500;
		bool _stopQueryWhenFoundTicket;
		bool _useAnonymousQuery;
		private bool _timeoutAutoIncreaseSetting = true;
		private decimal _querySleepError = 3m;

		public QueryConfiguration()
		{
			_current = this;
			_querySleep = 5.0m;
			if (_querySleep >= 300)
				_querySleep = Math.Max(_querySleep / 1000, 3);

			_musicPath = @"audio\music\蓝精灵-女声(萌版).mp3";
			_ignoreServerError = true;
			_ignoreCacheResult = true;
			_autoDelayAfterOClock = 0.5;
			_stopQueryWhenFoundTicket = false;
			_autoSuggestCount = 1;
			_enableAutoSuggest = true;
			_useAnonymousQuery = true;
			_anonymousQueryRate = 3;
			_ignoreTempTrainClass = true;
		}

		/// <summary>
		/// 获得或设置是否在查询的时候始终发送QueryLog
		/// </summary>
		public bool AlwaysSendingQueryLog
		{
			get { return false && _alwaysSendingQueryLog; }
			set
			{
				if (value == _alwaysSendingQueryLog) return;
				_alwaysSendingQueryLog = value;
				OnPropertyChanged(nameof(AlwaysSendingQueryLog));
			}
		}

		/// <summary>
		/// 匿名查询的周期
		/// </summary>
		[JsonProperty("aqr")]
		public int AnonymousQueryRate
		{
			get { return _anonymousQueryRate; }
			set
			{
				if (value == _anonymousQueryRate) return;
				_anonymousQueryRate = value;
				OnPropertyChanged("AnonymousQueryRate");
			}
		}

		/// <summary>
		/// 获得或设置整点后等待的时间
		/// </summary>
		[JsonProperty("adao")]
		public double AutoDelayAfterOClock
		{
			get { return _autoDelayAfterOClock; }
			set
			{
				if (value == _autoDelayAfterOClock) return;
				_autoDelayAfterOClock = value;
				OnPropertyChanged("AutoDelayAfterOClock");
			}
		}

		/// <summary>
		/// 进行自动推荐的阈值
		/// </summary>
		[JsonIgnore]
		public int AutoSuggestCount
		{
			get { return _autoSuggestCount; }
			set
			{
				if (value == _autoSuggestCount) return;
				_autoSuggestCount = value;
				OnPropertyChanged("AutoSuggestCount");
			}
		}

		/// <summary>
		/// 获得当前的配置对象
		/// </summary>
		public static QueryConfiguration Current
		{
			get { return _current ?? AppContext.ExtensionManager.ConfigurationProvider.LoadConfiguration<QueryConfiguration>("main", "Query"); }
		}

		/// <summary>
		/// 是否启用站点推荐
		/// </summary>
		public bool EnableAutoSuggest
		{
			get { return _enableAutoSuggest; }
			set
			{
				if (value.Equals(_enableAutoSuggest)) return;
				_enableAutoSuggest = value;
				OnPropertyChanged("EnableAutoSuggest");
			}
		}

		/// <summary>
		/// 获得或设置是否启用始发站预售提醒
		/// </summary>
		public bool EnableStartStationTip
		{
			get { return _enableStartStationTip; }
			set
			{
				if (value == _enableStartStationTip) return;
				_enableStartStationTip = value;
				OnPropertyChanged(nameof(EnableStartStationTip));
			}
		}

		/// <summary>
		/// 是否忽略疑似无效的结果
		/// </summary>
		public bool IgnoreAlmostIllegalResult
		{
			get { return _ignoreAlmostIllegalResult; }
			set
			{
				if (value == _ignoreAlmostIllegalResult) return;
				_ignoreAlmostIllegalResult = value;
				OnPropertyChanged(nameof(IgnoreAlmostIllegalResult));
			}
		}

		/// <summary>
		/// 忽略服务器错误？
		/// </summary>
		public bool IgnoreServerError
		{
			get { return _ignoreServerError; }
			set
			{
				if (value.Equals(_ignoreServerError))
					return;
				_ignoreServerError = value;
				OnPropertyChanged(nameof(IgnoreServerError));
			}
		}

		/// <summary>
		/// 过滤时是否遵循临客原则
		/// </summary>
		public bool IgnoreTempTrainClass
		{
			get { return _ignoreTempTrainClass; }
			set
			{
				if (value.Equals(_ignoreTempTrainClass)) return;
				_ignoreTempTrainClass = value;
				OnPropertyChanged("IgnoreTempTrainClass");
			}
		}

		/// <summary>
		/// 获得或设置用于提示的路径
		/// </summary>
		public string MusicPath
		{
			get { return _musicPath; }
			set
			{
				var rp = value;

				if (rp == _musicPath)
					return;
				_musicPath = value.DefaultForEmpty(@"audio\music\大风车 -  CCTV版.mp3");
				OnPropertyChanged("MusicPath");
			}
		}

		/// <summary>
		/// 获得或设置查询的休息时间
		/// </summary>
		[JsonProperty("QuerySleepA")]
		public decimal QuerySleep
		{
			get { return _querySleep; }
			set
			{
				if (value == _querySleep) return;
				_querySleep = value;
				OnPropertyChanged(nameof(QuerySleep));
			}
		}

		/// <summary>
		/// 获得或设置查询的休息时间
		/// </summary>
		[JsonProperty("QuerySleepError_v1")]
		public decimal QuerySleepError
		{
			get { return _querySleepError; }
			set
			{
				if (value == _querySleepError)
					return;
				_querySleepError = value;
				OnPropertyChanged(nameof(QuerySleepError));
			}
		}

		/// <summary>
		/// 查询超时
		/// </summary>
		public int QueryTimeout
		{
			get { return _queryTimeout; }
			set
			{
				if (value == _queryTimeout)
					return;
				_queryTimeout = value;
				OnPropertyChanged(nameof(QueryTimeout));
			}
		}

		/// <summary>
		/// 获得或设置是否在放票的时间点附近的1分钟内以高频率查询
		/// </summary>
		/// <value><c>true</c> if [speeding query on o clock]; otherwise, <c>false</c>.</value>
		public int? SpeedingQueryOnOClock
		{
			get { return _speedingQueryOnOClock; }
			set
			{
				if (value == _speedingQueryOnOClock)
					return;
				_speedingQueryOnOClock = value;
				OnPropertyChanged(nameof(SpeedingQueryOnOClock));
			}
		}


		[JsonProperty("StopQueryWhenFoundTicket_v2")]
		public bool StopQueryWhenFoundTicket
		{
			get { return _stopQueryWhenFoundTicket; }
			set
			{
				if (value.Equals(_stopQueryWhenFoundTicket)) return;
				_stopQueryWhenFoundTicket = value;
				OnPropertyChanged("StopQueryWhenFoundTicket");
			}
		}

		/// <summary>
		/// 获得或设置是否使用匿名查询
		/// </summary>
		public bool UseAnonymousQuery
		{
			get { return _useAnonymousQuery; }
			set
			{
				if (value.Equals(_useAnonymousQuery)) return;
				_useAnonymousQuery = value;
				OnPropertyChanged("UseAnonymousQuery");
			}
		}

		/// <summary>
		/// 频繁超时时是否自动增加设置值
		/// </summary>
		public bool TimeoutAutoIncreaseSetting
		{
			get { return _timeoutAutoIncreaseSetting; }
			set
			{
				if (value == _timeoutAutoIncreaseSetting)
					return;
				_timeoutAutoIncreaseSetting = value;
				OnPropertyChanged(nameof(TimeoutAutoIncreaseSetting));
			}
		}

		private bool _enableSpeedProtect = true;
		/// <summary>
		/// 是否启用查询速度监控
		/// </summary>
		public bool EnableSpeedProtect
		{
			get { return _enableSpeedProtect; }
			set
			{
				if (value == _enableSpeedProtect)
					return;
				_enableSpeedProtect = value;
				OnPropertyChanged(nameof(EnableSpeedProtect));
			}
		}

		private int? _randomQueryDelay = 1500;

		/// <summary>
		/// 随机化查询延迟（毫秒）
		/// </summary>
		public int? RandomQueryDelay
		{
			get => _randomQueryDelay;
			set
			{
				if (value == _randomQueryDelay) return;
				_randomQueryDelay = value;
				OnPropertyChanged(nameof(RandomQueryDelay));
			}
		}

		Random _random = new Random();

		/// <summary>
		/// 获得休息时间
		/// </summary>
		/// <param name="isError"></param>
		/// <returns></returns>
		public int GetRandomQuerySleep(bool isError)
		{
			var time = (isError ? QuerySleepError : QuerySleep) * 1000;

			if (RandomQueryDelay.HasValue)
			{
				time += _random.Next(RandomQueryDelay.Value);
			}

			return Math.Max(100, (int)time);
		}

		private bool _autoDetectHbQueue = true;

		/// <summary>
		/// 自动查询候选订单状态
		/// </summary>
		[JsonProperty("adhq")]
		public bool AutoDetectHbQueue
		{
			get => _autoDetectHbQueue;
			set
			{
				if (_autoDetectHbQueue == value)
				{
					return;
				}

				_autoDetectHbQueue = value;
				OnPropertyChanged(nameof(AutoDetectHbQueue));
			}
		}

		private bool _detectAllHbQueue = true;

		/// <summary>
		/// 自动查询全部车次候选订单状态，不选择的话仅查选中车次
		/// </summary>
		[JsonProperty("hahq")]
		public bool DetectAllHbQueue
		{
			get => _detectAllHbQueue;
			set
			{
				if (_detectAllHbQueue == value)
				{
					return;
				}

				_detectAllHbQueue = value;
				OnPropertyChanged(nameof(DetectAllHbQueue));
			}
		}

		private int _hbQueueInSelectedTrainsInterval = 600;

		/// <summary>
		/// 选中车次候选队列刷新时间间隔
		/// </summary>
		[JsonProperty("hqisti")]
		public int HbQueueInSelectedTrainsInterval
		{
			get => _hbQueueInSelectedTrainsInterval;
			set
			{
				if (_hbQueueInSelectedTrainsInterval == value)
				{
					return;
				}

				_hbQueueInSelectedTrainsInterval = value;
				OnPropertyChanged(nameof(HbQueueInSelectedTrainsInterval));
			}
		}

		private int _hbQueueInNonSelectedTrainsInterval = 60 * 60;

		/// <summary>
		/// 未选择车次候选队列刷新时间间隔
		/// </summary>
		[JsonProperty("hqinsti1")]
		public int HbQueueInNonSelectedTrainsInterval
		{
			get => _hbQueueInNonSelectedTrainsInterval;
			set
			{
				if (_hbQueueInNonSelectedTrainsInterval == value)
				{
					return;
				}

				_hbQueueInNonSelectedTrainsInterval = value;
				OnPropertyChanged(nameof(HbQueueInNonSelectedTrainsInterval));
			}
		}

		private bool _notifyWhenHbAvailable = true;

		/// <summary>
		/// 当候选变得可用时，给提示
		/// </summary>
		[JsonProperty("nwha")]
		public bool NotifyWhenHbAvailable
		{
			get => _notifyWhenHbAvailable;
			set
			{
				if (_notifyWhenHbAvailable == value)
				{
					return;
				}

				_notifyWhenHbAvailable = value;
				OnPropertyChanged(nameof(NotifyWhenHbAvailable));
			}
		}
	}
}
