using System;
using System.Collections.Generic;
using System.Linq;

using TOBA.Configuration;
using TOBA.Order;

namespace TOBA.Entity
{
	using Autofac;

	using Data;

	using DevComponents.DotNetBar;

	using FSLib.Extension;

	using Newtonsoft.Json;

	using Order.Entity;

	using Query.Entity;

	using System.ComponentModel;
	using System.Xml.Serialization;

	/// <summary>
	/// 查询参数
	/// </summary>
	internal partial class QueryParam : Dto, INotifyPropertyChanged, ICloneable
	{
		int _arriveTimeFrom;
		int _arriveTimeTo;
		AutoPreSubmitConfiguration _autoPreSubmitConfig;
		DateTime _departureDate;
		int _departureTimeFrom;
		int _departureTimeTo;
		bool _enableAutoPreSubmit;
		string _fromStationCode;
		string _fromStationName;
		bool _hideFromNotSame;
		bool _hideNoNeedTicket = true;
		bool _hideNoTicket;
		bool _hideToNotSame;

		private bool _ignoreIllegalData;
		bool _isLoaded;
		bool _isPersistent;

		bool _isSameCityStationLoopAvailable;
		private DateTime? _lastQueryTime;
		string _name;

		private bool _proxyMode;
		bool _queryStudentTicket;
		EventHashSet<char> _selectedSeatClass;
		EventHashSet<char> _selectedTrainClass;
		string _toStationCode;
		string _toStationName;
		string _trainId;
		int? _trainPassType;
		QueryParamUiSetting _uiSetting;

		/// <summary>
		/// 创建 <see cref="QueryParam" />  的新实例(QueryParam)
		/// </summary>
		public QueryParam()
		{
			Name = "新建查询";
			IsLoaded = true;
			EnableAutoPreSubmit = false;
			DepartureTimeTo = 23;
			ArriveTimeTo = 23;
			AutoPreSubmitConfig.EnableOClockRefresh = true;
			AutoPreSubmitConfig.AutoWaitToSell = true;
			EnableAutoVC = false;
			DepartureDate = ParamData.GetMaxTicketDate(false);
			_enableTrainSuggest = QueryConfiguration.Current.EnableAutoSuggest;
		}

		/// <summary>
		/// 当加载状态发生变化时触发
		/// </summary>
		public event EventHandler LoadedChanged;

		/// <summary>
		/// 当情景模式发生变化时触发
		/// </summary>
		public event EventHandler PersistentChanged;

		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// 请求查询
		/// </summary>
		public event EventHandler<GeneralEventArgs<bool>> RequestQuery;

		public event EventHandler RequestStop;

		/// <summary>
		/// 请求保存
		/// </summary>
		public event EventHandler RequireSave;

		/// <summary>
		/// 已选择车次席别发生变更
		/// </summary>
		public event EventHandler SelectedSeatClassChanged;

		/// <summary>
		/// 已选择车次类型发生变更
		/// </summary>
		public event EventHandler SelecteTrainClassChanged;

		/// <summary>
		/// 查询状态发生变更
		/// </summary>
		public event EventHandler StatusChanged;

		bool IsTrainTicketDataDisabled(QueryResultItem train)
		{
			var provider = RecentlySubmitFailedTokenStorageProvider.Instance;
			return provider.IsTicketDataDisabledExists(train);
		}

		/// <summary>
		/// 引发 <see cref="LoadedChanged" /> 事件
		/// </summary>
		protected virtual void OnLoadedChanged()
		{
			LoadedChanged?.Invoke(this, EventArgs.Empty);
		}


		/// <summary>
		/// 引发 <see cref="PersistentChanged" /> 事件
		/// </summary>
		protected virtual void OnPersistentChanged()
		{
			PersistentChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// </summary>
		/// <param name="propertyName"></param>
		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public virtual void OnSelecteTrainClassChanged()
		{
			SelecteTrainClassChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// 引发 <see cref="StatusChanged" /> 事件
		/// </summary>
		protected virtual void OnStatusChanged()
		{
			var handler = StatusChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 确认有ID
		/// </summary>
		internal void EnsureID()
		{
			if (ID.IsNullOrEmpty()) ID = Guid.NewGuid().ToString("N");
		}

		/// <summary>
		/// 获得或设置文件路径
		/// </summary>
		internal string FilePath { get; set; }

		/// <summary>
		/// 检查是否可以进行查询
		/// </summary>
		/// <returns></returns>
		public string CanQuery()
		{
			var lastResult = LastQueryResult;
			LastQueryResult = null;

			if (FromStationCode.IsNullOrEmpty()) return "请选择始发站";
			if (ToStationCode.IsNullOrEmpty()) return "请选择到达站";
			//日期
			if (!QueryStudentTicket && !ParamData.IsCommonTicketSell(CurrentDepartureDate))
			{
				AppContext.HostForm.ShowToast("超出预售期，无法正常查询，仅供12306系统维护期间值守查询用。", backColor: System.Drawing.Color.DarkRed, glowColor: eToastGlowColor.Red, timeout: 2000);
			}
			//席别选择有误
			if (SelectedSeatClass.Count > 0 && EnableAutoPreSubmit && AutoPreSubmitConfig.SeatList.Count > 0 && AutoPreSubmitConfig.SeatList.Any(s => !SelectedSeatClass.Contains(s)))
			{
				var seats = AutoPreSubmitConfig.SeatList.Except(SelectedSeatClass).ToArray();

				seats.ForEach(s => SelectedSeatClass.Add(s));
			}
			//出发时间和到达时间
			if (DepartureTimeTo < DepartureTimeFrom) return "出发时间的结束时间不得大于开始时间";
			if (ArriveTimeTo < ArriveTimeFrom) return "到达时间的结束时间不得大于开始时间";

			//学生票？
			if (QueryStudentTicket && (IsAutoSubmitEnabled && AutoPreSubmitConfig.Passenger.Any(s => s.TicketType != 3)))
			{
				return "选择了查询学生票，可是为嘛儿添加的乘客不全是学生嘞，你个臭流氓 o(>_<)o ~~";
			}

			LastQueryResult = lastResult;

			return null;
		}

		#region Implementation of ICloneable

		/// <summary>
		/// 创建作为当前实例副本的新对象。
		/// </summary>
		/// <returns>
		/// 作为此实例副本的新对象。
		/// </returns>
		public object Clone()
		{
			//....用序列化好了
			var setting = new JsonSerializerSettings()
			{
				TypeNameHandling = TypeNameHandling.Auto
			};
			var text = JsonConvert.SerializeObject(this, Formatting.None, setting);
			var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryParam>(text, setting);
			obj.TrainIdList = LastQueryResult?.Where(s => s.Selected > 0).Select(s => s.Id).MapToHashSet();
			obj.ID = "";

			return obj;
		}

		#endregion


		/// <summary>
		/// 引发 <see cref="RequestQuery" /> 事件
		/// </summary>
		public virtual string OnRequestQuery(bool resetCount = true)
		{
			if (!IsLoaded)
				return "Query has been unloaded.";

			var msg = CanQuery();

			if (msg.IsNullOrEmpty())
			{
				if (resetCount)
				{
					CurrentAlternativeIndex = null;
					QueryCount = 0;
				}
				else
				{
					if (AlternativeDate.Count > 0)
					{
						var nindex = (CurrentAlternativeIndex ?? -1) + 1;
						if (nindex >= AlternativeDate.Count)
							CurrentAlternativeIndex = null;
						else CurrentAlternativeIndex = nindex;
					}
				}
				RequestQuery?.Invoke(this, new GeneralEventArgs<bool>(resetCount));
			}

			return msg;
		}

		/// <summary>
		/// 引发 <see cref="RequestStop" /> 事件
		/// </summary>
		public virtual void OnRequestStop()
		{
			RequestStop?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// 引发 <see cref="RequireSave" /> 事件
		/// </summary>
		public virtual void OnRequireSave()
		{
			var handler = RequireSave;
			handler?.Invoke(this, EventArgs.Empty);
		}

		void AutoRenameTitle()
		{
			if (!Name.IsNullOrEmpty() && Name != "新建查询" && Name.IndexOf("->") == -1) return;

			Name = (Resign ? "【改签】" : "") + DepartureDate.ToString("MM-dd") + " " + FromStationName.DefaultForEmpty("(未选)") + "->" + ToStationName.DefaultForEmpty("(未选)");
		}

		/// <summary>
		/// 引发 <see cref="SelectedSeatClassChanged"/> 事件
		/// </summary>
		public virtual void OnSelectedSeatClassChanged()
		{
			SelectedSeatClassChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// 到达时刻开始
		/// </summary>
		public int ArriveTimeFrom
		{
			get => _arriveTimeFrom;
			set
			{
				if (value == _arriveTimeFrom)
					return;
				_arriveTimeFrom = value;
				OnPropertyChanged(nameof(ArriveTimeFrom));
			}
		}

		/// <summary>
		/// 到达时刻结束
		/// </summary>
		public int ArriveTimeTo
		{
			get => _arriveTimeTo;
			set
			{
				if (value == _arriveTimeTo)
					return;
				_arriveTimeTo = value;
				OnPropertyChanged(nameof(ArriveTimeTo));
			}
		}

		/// <summary>
		/// 获得或设置自动提交设置
		/// </summary>
		public AutoPreSubmitConfiguration AutoPreSubmitConfig
		{
			get => _autoPreSubmitConfig ?? (_autoPreSubmitConfig = new AutoPreSubmitConfiguration());
			set => _autoPreSubmitConfig = value;
		}

		/// <summary>
		/// 当前实际查询有效的时间
		/// </summary>
		[JsonIgnore]
		[XmlIgnore]
		public DateTime CurrentDepartureDate
		{
			get
			{
				if (CurrentAlternativeIndex == null || CurrentAlternativeIndex.Value >= AlternativeDate.Count)
					return DepartureDate;
				return AlternativeDate[CurrentAlternativeIndex ?? 0];
			}
		}

		/// <summary>
		/// 出发日期
		/// </summary>
		public DateTime DepartureDate
		{
			get => _departureDate;
			set
			{
				value = value.Date;
				if (value.Equals(_departureDate))
					return;

				//如果备选日期里有，则交换
				if (AlternativeDate.Contains(value))
				{
					AddAlternativeDate(_departureDate);
					AlternativeDate.Remove(value);
				}

				_departureDate = value;
				CurrentAlternativeIndex = null;
				AutoRenameTitle();
				OnPropertyChanged(nameof(DepartureDate));
			}
		}

		/// <summary>
		/// 出发时刻开始
		/// </summary>
		public int DepartureTimeFrom
		{
			get => _departureTimeFrom;
			set
			{
				if (value == _departureTimeFrom)
					return;
				_departureTimeFrom = value;
				OnPropertyChanged(nameof(DepartureTimeFrom));
			}
		}

		/// <summary>
		/// 出发时刻结束
		/// </summary>
		public int DepartureTimeTo
		{
			get => _departureTimeTo;
			set
			{
				if (value == _departureTimeTo)
					return;
				_departureTimeTo = value;
				OnPropertyChanged(nameof(DepartureTimeTo));
			}
		}

		/// <summary>
		/// 获得或设置是否允许自动提交
		/// </summary>
		public bool EnableAutoPreSubmit
		{
			get => _enableAutoPreSubmit;
			set
			{
				if (value.Equals(_enableAutoPreSubmit))
					return;
				_enableAutoPreSubmit = value;
				OnPropertyChanged(nameof(EnableAutoPreSubmit));
				//OnRequireSave();
			}
		}

		/// <summary>
		/// 获得是否允许自动验证码
		/// </summary>
		public bool EnableAutoVC
		{
			get => _enableAutoVc;
			set
			{
				if (value.Equals(_enableAutoVc))
					return;
				_enableAutoVc = value;
				OnPropertyChanged(nameof(EnableAutoVC));
			}
		}

		/// <summary>
		/// 是否启用同城站点轮询
		/// </summary>
		public bool EnableSameCityStationLoop
		{
			get => _enableSameCityStationLoop;
			set
			{
				if (value.Equals(_enableSameCityStationLoop))
					return;
				_enableSameCityStationLoop = value;
				OnPropertyChanged(nameof(EnableSameCityStationLoop));
			}
		}

		/// <summary>
		/// 获得或设置是否允许提醒
		/// </summary>
		public bool EnableTrainSuggest
		{
			get => _enableTrainSuggest;
			set
			{
				if (value.Equals(_enableTrainSuggest))
					return;
				_enableTrainSuggest = value;
				OnPropertyChanged(nameof(EnableTrainSuggest));
			}
		}

		/// <summary>
		/// 获得或设置是否找到有效的车票
		/// </summary>
		[JsonIgnore, XmlIgnore]
		public bool FoundValidTickets
		{
			get => _foundValidTickets;
			set
			{
				if (value.Equals(_foundValidTickets))
					return;
				_foundValidTickets = value;
				OnPropertyChanged(nameof(FoundValidTickets));
			}
		}

		/// <summary>
		/// 发站电码
		/// </summary>
		public string FromStationCode
		{
			get => _fromStationCode;
			set
			{
				if (value == _fromStationCode)
					return;
				_fromStationCode = value;
				OnPropertyChanged(nameof(FromStationCode));
			}
		}

		/// <summary>
		/// 发站名称
		/// </summary>
		public string FromStationName
		{
			get => _fromStationName;
			set
			{
				if (value == _fromStationName)
					return;
				_fromStationName = value;
				AutoRenameTitle();
				OnPropertyChanged(nameof(FromStationName));
			}
		}

		/// <summary>
		/// 获得或设置是否有查出来票
		/// </summary>
		[JsonIgnore, XmlIgnore]
		public bool HasTicket
		{
			get => _hasTicket;
			set
			{
				if (value.Equals(_hasTicket))
					return;
				_hasTicket = value;
				OnPropertyChanged(nameof(HasTicket));
			}
		}

		/// <summary>
		/// 获得或设置是否隐藏发站不同的车次
		/// </summary>
		public bool HideFromNotSame
		{
			get => _hideFromNotSame;
			set
			{
				if (value.Equals(_hideFromNotSame))
					return;
				_hideFromNotSame = value;
				OnPropertyChanged(nameof(HideFromNotSame));
			}
		}

		/// <summary>
		/// 获得或设置是否不显示指定席别无票车次
		/// </summary>
		public bool HideNoNeedTicket
		{
			get => _hideNoNeedTicket;
			set
			{
				if (value.Equals(_hideNoNeedTicket))
					return;
				_hideNoNeedTicket = value;
				OnPropertyChanged(nameof(HideNoNeedTicket));
			}
		}

		/// <summary>
		/// 获得或设置是否隐藏无票车次
		/// </summary>
		public bool HideNoTicket
		{
			get => _hideNoTicket;
			set
			{
				if (value.Equals(_hideNoTicket))
					return;
				_hideNoTicket = value;
				OnPropertyChanged(nameof(HideNoTicket));
			}
		}

		/// <summary>
		/// 获得或设置是否隐藏到站不同的车次
		/// </summary>
		public bool HideToNotSame
		{
			get => _hideToNotSame;
			set
			{
				if (value.Equals(_hideToNotSame))
					return;
				_hideToNotSame = value;
				OnPropertyChanged(nameof(HideToNotSame));
			}
		}


		/// <summary>
		/// 获得或设置唯一标记符
		/// </summary>
		public string ID { get; set; }

		/// <summary>
		/// 忽略可能的错误数据
		/// </summary>
		public bool IgnoreIllegalData
		{
			get => _ignoreIllegalData;
			set
			{
				if (value == _ignoreIllegalData)
					return;
				_ignoreIllegalData = value && !ApiConfiguration.Instance.DisableIllegalDetect && QueryConfiguration.Current.IgnoreAlmostIllegalResult;
				OnPropertyChanged(nameof(IgnoreIllegalData));
			}
		}

		[XmlIgnore, JsonIgnore]
		public bool IsAutoRecognizeCodeAllowed => true;

		/// <summary>
		/// 获得是否有效的自动预定
		/// </summary>
		[JsonIgnore]
		public bool IsAutoSubmitEnabled => EnableAutoPreSubmit && AutoPreSubmitConfig.AllSetOk;


		/// <summary>
		/// 是否正在被加载
		/// </summary>
		public bool IsLoaded
		{
			get => _isLoaded;
			set
			{
				if (_isLoaded == value) return;
				_isLoaded = value;
				OnLoadedChanged();
			}
		}

		/// <summary>
		/// 是否已经保存为情景模式
		/// </summary>
		public bool IsPersistent
		{
			get => _isPersistent;
			set
			{
				if (_isPersistent == value) return;

				_isPersistent = value;
				OnPersistentChanged();
			}
		}

		/// <summary>
		/// 获得或设置同城站点轮询是否可用
		/// </summary>
		public bool IsSameCityStationLoopAvailable
		{
			get => _isSameCityStationLoopAvailable;
			set
			{
				if (value == _isSameCityStationLoopAvailable) return;
				_isSameCityStationLoopAvailable = value;
				OnPropertyChanged(nameof(IsSameCityStationLoopAvailable));
			}
		}

		public DateTime? LastQueryRequestTime
		{
			get => _lastQueryRequestTime;
			set
			{
				if (value.Equals(_lastQueryRequestTime)) return;
				_lastQueryRequestTime = value;
				OnPropertyChanged(nameof(LastQueryRequestTime));
			}
		}

		/// <summary>
		/// 获得或设置上次查询结果
		/// </summary>
		[JsonIgnore]
		public QueryResult LastQueryResult { get; set; }

		[JsonIgnore, XmlIgnore]
		public bool? LastQuerySuccess
		{
			get => _lastQuerySuccess;
			set
			{
				if (value.Equals(_lastQuerySuccess))
					return;
				_lastQuerySuccess = value;
				OnPropertyChanged(nameof(LastQuerySuccess));
			}
		}

		/// <summary>
		/// 获得或设置最后查询时间
		/// </summary>
		public DateTime? LastQueryTime
		{
			get => _lastQueryTime;
			set
			{
				if (_lastQueryTime == value)
					return;

				_lastQueryTime = value;
				OnPropertyChanged(nameof(LastQueryTime));
			}
		}

		/// <summary>
		/// 获得或设置查询的名称（情景模式）
		/// </summary>
		public string Name
		{
			get => _name;
			set
			{
				if (value == _name)
					return;
				_name = value;
				OnPropertyChanged(nameof(Name));

				if (value.IsNullOrEmpty())
					AutoRenameTitle();
			}
		}

		/// <summary>
		/// 获得或设置原始的订单ID
		/// </summary>
		[JsonIgnore, XmlIgnore]
		public Tuple<OrderItem, OrderTicket[]> OriginalOrder
		{
			get => _originalOrder;
			set
			{
				if (value == _originalOrder) return;
				_originalOrder = value;
				OnPropertyChanged(nameof(OriginalOrder));
			}
		}

		/// <summary>
		/// 车辆过路类型（QB=全部，SF=始发，GL=过路）
		/// </summary>
		public int? PassType
		{
			get => _trainPassType;
			set
			{
				if (value.Equals(_trainPassType))
					return;
				_trainPassType = value;
				OnPropertyChanged(nameof(PassType));
			}
		}

		[JsonIgnore]
		public int QueryCount { get; set; }

		/// <summary>
		/// 获得或设置查询页面地址
		/// </summary>
		[JsonIgnore]
		public string QueryPageUrl { get; set; }

		/// <summary>
		/// 获得或设置查询状态
		/// </summary>
		[JsonIgnore, XmlIgnore]
		public QueryState QueryState
		{
			get => _queryState;
			set
			{
				if (value == _queryState)
					return;
				_queryState = value;
				OnPropertyChanged(nameof(QueryState));
				OnStatusChanged();
			}
		}

		/// <summary>
		/// 获得或设置是否查学生票
		/// </summary>
		public bool QueryStudentTicket
		{
			get => _queryStudentTicket;
			set
			{
				if (value.Equals(_queryStudentTicket))
					return;
				_queryStudentTicket = value;
				OnPropertyChanged(nameof(QueryStudentTicket));
			}
		}

		/// <summary>
		/// 获得或设置是否是改签
		/// </summary>
		[JsonIgnore]
		public bool Resign
		{
			get => _resign;
			set
			{
				if (value.Equals(_resign))
					return;
				_resign = value;
				AutoRenameTitle();
				OnPropertyChanged(nameof(Resign));
			}
		}

		/// <summary>
		/// 改签的时候是否变更到站
		/// </summary>
		[JsonIgnore]
		public bool ResignChangeTs
		{
			get => _resignChangeTs;
			set
			{
				if (value == _resignChangeTs)
					return;
				_resignChangeTs = value;
				OnPropertyChanged(nameof(ResignChangeTs));
			}
		}

		/// <summary>
		/// 获得或设置原日期
		/// </summary>
		public DateTime ResignDate
		{
			get => _resignDate;
			set
			{
				if (value.Equals(_resignDate)) return;
				_resignDate = value;
				OnPropertyChanged(nameof(ResignDate));
			}
		}

		/// <summary>
		/// 获得或设置选定的席别
		/// </summary>
		public EventHashSet<char> SelectedSeatClass
		{
			get => _selectedSeatClass ?? (_selectedSeatClass = new EventHashSet<char>());
			set => _selectedSeatClass = value;
		}

		/// <summary>
		/// 获得或设置选定的车次
		/// </summary>
		public EventHashSet<char> SelectedTrainClass
		{
			get => _selectedTrainClass ?? (_selectedTrainClass = new EventHashSet<char>());
			set => _selectedTrainClass = value;
		}

		/// <summary>
		/// 将学生提交为成人票
		/// </summary>
		public bool SubmitStudentAsCommon
		{
			get => _submitStudentAsCommon;
			set
			{
				if (value.Equals(_submitStudentAsCommon))
					return;
				_submitStudentAsCommon = value;
				OnPropertyChanged(nameof(SubmitStudentAsCommon));
			}
		}

		/// <summary>
		/// 到站电码
		/// </summary>
		public string ToStationCode
		{
			get => _toStationCode;
			set
			{
				if (value == _toStationCode)
					return;
				_toStationCode = value;
				OnPropertyChanged(nameof(ToStationCode));
			}
		}

		/// <summary>
		/// 到站名称
		/// </summary>
		public string ToStationName
		{
			get => _toStationName;
			set
			{
				if (value == _toStationName)
					return;
				_toStationName = value;
				AutoRenameTitle();
				OnPropertyChanged(nameof(ToStationName));
			}
		}

		/// <summary>
		/// 获得或设置列车ID
		/// </summary>
		public string TrainID
		{
			get => _trainId;
			set
			{
				if (value == _trainId) return;
				_trainId = value;
				OnPropertyChanged(nameof(TrainID));
			}
		}

		/// <summary>
		/// 车次ID列表
		/// </summary>
		[JsonIgnore]
		public HashSet<string> TrainIdList { get; set; }

		/// <summary>
		/// UI设置
		/// </summary>
		public QueryParamUiSetting UiSetting
		{
			get => _uiSetting ?? (_uiSetting = new QueryParamUiSetting());
			set
			{
				if (Equals(value, _uiSetting)) return;
				_uiSetting = value;
				OnPropertyChanged(nameof(UiSetting));
			}
		}


		public KeyValuePair<string, string>? VerifyData { get; set; }

		private bool _isLastInQuery;

		/// <summary>
		/// 最后是否处于查询状态
		/// </summary>
		public bool IsLastInQuery
		{
			get => _isLastInQuery;
			set
			{
				if (value == _isLastInQuery)
					return;
				_isLastInQuery = value;
				OnPropertyChanged(nameof(IsLastInQuery));
			}
		}

		[JsonIgnore]
		public OrderSubmitEventArgs SubmitEventArgs { get; set; }

		/// <summary>
		/// 轮换日期
		/// </summary>
		public EventList<DateTime> AlternativeDate { get; } = new EventList<DateTime>();

		public void AddAlternativeDate(DateTime date)
		{
			date = date.Date;
			if (AlternativeDate.Contains(date))
				return;

			var index = 0;
			while (index < AlternativeDate.Count && date > AlternativeDate[index])
				index++;

			if (index >= AlternativeDate.Count)
				AlternativeDate.Add(date);
			else AlternativeDate.Insert(index, date);
		}

		private int? _currentAlternativeIndex;

		/// <summary>
		/// 当前的轮询日期
		/// </summary>
		[JsonIgnore]
		public int? CurrentAlternativeIndex
		{
			get => _currentAlternativeIndex;
			set
			{
				if (value == _currentAlternativeIndex) return;
				_currentAlternativeIndex = value;
				OnPropertyChanged(nameof(CurrentAlternativeIndex));
			}
		}

		#region 缓存

		bool _resign;
		bool _hasTicket;
		QueryState _queryState;
		bool _enableAutoVc;
		bool? _lastQuerySuccess;
		DateTime _resignDate;
		Tuple<OrderItem, OrderTicket[]> _originalOrder;
		DateTime? _lastQueryRequestTime;
		bool _submitStudentAsCommon;
		bool _enableTrainSuggest;
		bool _enableSameCityStationLoop = true;
		bool _foundValidTickets;
		bool _inAutoRefreshMode;
		bool _resignChangeTs;

		#endregion

		/// <summary>
		/// 填充详细信息以便于提交订单
		/// </summary>
		/// <param name="passengers"></param>
		/// <param name="seat"></param>
		/// <returns></returns>
		public IEnumerable<PassengerInTicket> PrepareTicketInfoForPassengers(QueryResultItem train, IEnumerable<PassengerInTicket> passengers, char seat)
		{
			if (passengers == null)
				return null;

			// ReSharper disable once AccessToModifiedClosure
			// ReSharper disable once PossibleMultipleEnumeration
			passengers.ForEach(s => { s.OriginalSeatType = seat; });

			var availableSeatSubTypes = ParamData.GetSeatSubTypeList(seat);
			var index = 0;
			EventList<SubType> group = null;
			if (seat == 'P' || seat == 'M' || seat == 'O' || seat == '9')
				group = AutoPreSubmitConfig.SeatSubTypesHighSpeed;
			else if (seat == '6' || seat == '4' || seat == '3')
				group = AutoPreSubmitConfig.SeatSubTypesBed;

			SubType GetSubSeatType()
			{
				if (group == null || index >= group.Count || availableSeatSubTypes == null)
					return SubType.Random;

				SubType type;
				while (index < group.Count)
				{
					type = group[index++];
					if (group.Contains(type))
						return type;
				}

				return SubType.Random;
			}

			var date = train.QueryResult.Date;

			foreach (var s in passengers)
			{
				s.SeatType = seat;
				s.SeatSubType = GetSubSeatType();

				if (s.TicketType == 3 && (!ParamData.IsDateInStudentRange(date) || SubmitStudentAsCommon))
					s.TicketType = 1;
			}

			return passengers;
		}

		/// <summary>
		/// 新建一个结果集
		/// </summary>
		/// <returns></returns>
		public QueryResult CreateQueryResult(int capacity = 0)
		{
			return new QueryResult(CurrentDepartureDate, FromStationCode, FromStationName, ToStationCode, ToStationName, capacity)
			{
				Query = this
			};
		}

		/// <summary>
		/// 获得或设置是否临时关闭快速订单提交
		/// </summary>
		[JsonIgnore]
		[XmlIgnore]
		public bool TemporaryDisableFastSubmitOrder { get; set; }

		private bool _autoHb = false;

		/// <summary>
		/// 自动候补
		/// </summary>
		public bool AutoHb
		{
			get => _autoHb && !Resign;
			set
			{
				if (value == _autoHb) return;
				_autoHb = value;
				OnPropertyChanged(nameof(AutoHb));
			}
		}
	}
}
