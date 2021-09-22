using System.Runtime.Caching;

using TOBA.WebLib;

namespace TOBA.Query
{
	using Autofac;

	using BackupOrder;

	using Configuration;

	using Data;

	using FSLib.Network.Http;

	using Newtonsoft.Json;

	using Platform.DeviceFingerprint;

	using Query.Entity;

	using System;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.ComponentModel;
	using System.Linq;
	using System.Net;
	using System.Net.Cache;
	using System.Threading.Tasks;
	using System.Web;

	using TOBA.Entity;

	/// <summary>
	/// 查票工作类
	/// </summary>
	internal class TicketQueryWorker
	{
		const string _queryInfoKey = "queryInfo";

		static MemoryCache _secureCache = new("toba_securestrCache");
		int _anonymousCounter = 0;
		NetClient _client;
		private IFillLeftTicketService _fillLeftTicketService;
		bool _isBusy;
		AsyncOperation _operation;

		private QueryParam _query;
		TicketQueryRequestParamInfo _queryInfo;
		private IQueryTimeoutWarningService _queryTimeoutWarningService;
		private StationRandomService _randomService = new();
		Session _session;
		private int _code405Count = 0;
		private ISystemBusyWarningService _systemBusyWarningService;

		/// <summary>
		/// 获得或设置是否启用超时警告
		/// </summary>
		public bool EnableTimeoutService { get; set; } = false;

		/// <summary>
		/// 获得或设置是否启用系统繁忙警告
		/// </summary>
		public bool EnableSystemBusyService { get; set; } = false;

		/// <summary>
		/// 初始化 <see cref="TicketQueryWorker" />
		/// </summary>
		/// <param name="session">The session.</param>
		/// <param name="query">The query.</param>
		public TicketQueryWorker(Session session, QueryParam query)
		{
			Query = query;
			Session = session;
			_fillLeftTicketService = session.ServiceContainer.ResolveOptional<IFillLeftTicketService>();
		}

		/// <summary>
		/// 操作完成
		/// </summary>
		public event EventHandler Finished;

		/// <summary>
		/// 当忙碌状态改变时触发
		/// </summary>
		public event EventHandler IsBusyChanged;

		/// <summary>
		/// 当查票失败的时候触发
		/// </summary>
		public event EventHandler TicketQueryFailed;

		/// <summary>
		/// 当查票成功的时候触发
		/// </summary>
		public event EventHandler TicketQuerySuccess;

		void DetectStartStationSellInfo(QueryResult result)
		{
			foreach (var train in result)
			{
				//始发站、非预售票、预售期不在当天的，均跳过
				if (train.FromStation.IsFirst)
				{
					continue;
				}

				var firstStationSellTimeTip = ParamData.SellTimeMap?.GetValue(train.StartStation.Code);
				if (firstStationSellTimeTip.IsNullOrEmpty())
					continue;
				var firstStationSellTime = DateTime.Today.Add(TimeSpan.Parse(firstStationSellTimeTip));

				var ts = new StartStationSellInfo()
				{
					Code = train.StartStation.Code,
					Name = train.StartStation.StationName,
					IsEarly = train.BeginSellTime.HasValue ? firstStationSellTime < train.BeginSellTime.Value : firstStationSellTime < DateTime.Now,
					SellTime = firstStationSellTime,
					IsInSell = firstStationSellTime < DateTime.Now
				};
				train.StartStationSellInfo = ts;
			}
		}

		void ClearQueryInfo()
		{
			_queryInfo = null;
			Session.RemoveSessionData(_queryInfoKey);
		}

		bool InitQueryInfo()
		{
			if (_queryInfo != null)
				return true;

			_queryInfo = Session.GetSessionData<TicketQueryRequestParamInfo>(_queryInfoKey);
			if (_queryInfo == null || _queryInfo.QueryFlag != Query.Resign)
			{
				lock (this)
				{
					if (_queryInfo == null || _queryInfo.QueryFlag != Query.Resign)
					{
						HttpContext<string> contextHtmlContext;
						if (Query.Resign)
						{
							contextHtmlContext = Session.NetClient.RunRequestLoop(_ => Session.NetClient.Create<string>(HttpMethod.Get, "leftTicket/init?pre_step_flag=gcInit", "queryOrder/init"), null);
						}
						else
						{
							contextHtmlContext = Session.NetClient.RunRequestLoop(_ => Session.NetClient.Create<string>(HttpMethod.Get, "leftTicket/init", "login/init"));
						}
						if (!contextHtmlContext.IsValid())
						{
							Message = "无法进入查询页，查看系统消息获得更多信息";

							var retMsg = contextHtmlContext.ResponseContent is ResponseBinaryContent ? (contextHtmlContext.ResponseContent as ResponseBinaryContent).StringResult : "未知";
							TOBA.Events.OnError(this, new EventInfoArgs($"未能进入查询页。返回信息={retMsg}"));

							return false;
						}
						var html = contextHtmlContext.Result;
						var matches = ParseUtility.Matches(@"(CLeftTicketUrl|isSaveQueryLog)\s*=\s*['""]([^'""]+)['""]", html).ToArray();
						var saveQueryLog = matches.Where(s => s.Success && s.Groups[1].Value.IsIgnoreCaseEqualTo("isSaveQueryLog")).Select(s => s.Groups[2].Value).FirstOrDefault() == "Y";
						var queryUrl = matches.Where(s => s.Success && s.Groups[1].Value.IsIgnoreCaseEqualTo("CLeftTicketUrl")).Select(s => s.Groups[2].Value).FirstOrDefault();

						if (string.IsNullOrEmpty(queryUrl))
						{
							Message = "无法获得查询地址，查看系统消息获得更多信息";

							var retMsg = contextHtmlContext.ResponseContent is ResponseBinaryContent ? (contextHtmlContext.ResponseContent as ResponseBinaryContent).StringResult : "未知";
							retMsg = $"未能进入查询页。返回信息 {retMsg}";
							TOBA.Events.OnError(this, new EventInfoArgs(retMsg));

							OnTicketQueryFailed();
							return false;
						}
						//MOD@2015年5月17日16:51:07：必须重新加载联系人，否则会出票失败。
						//if (ApiConfiguration.Instance.EnableAutoSubmitAPI)
						//{
						Session.IsPassengerLoaded = false;
						if (!Query.Resign)
							Session.LoadPassengers();
						//}

						_queryInfo = new TicketQueryRequestParamInfo()
						{
							EnableSaveLog = saveQueryLog,
							LastSaveLogTime = DateTime.MinValue,
							QueryFlag = Query.Resign,
							Url = queryUrl
						};
						Session.SetSessionData(_queryInfoKey, _queryInfo);
					}
				}
			}

			return _queryInfo != null;
		}

		static bool IsHitSecureStrCache(string trainid, string from, string to, string date, string server, string str)
		{
			var key = trainid + from + to + date + (server ?? "*");
			var data = _secureCache.Get(key);
			if (data == null)
			{
				_secureCache.Add(key, str, DateTime.Now.AddMinutes(5));
				return false;
			}
			var isCache = (data as string).IsIgnoreCaseEqualTo(str);

			_secureCache.Remove(key);
			_secureCache.Add(key, str, DateTime.Now.AddMinutes(5));

			return isCache;
		}

		void RunQueryInternal()
		{
			NotInSellDate = false;
			StartTime = DateTime.Now;
			Success = false;
			Statistics.Current.QueryCount++;
			Message = null;
			Query.QueryCount++;
			Query.FoundValidTickets = false;

			//初始化状态
			Query.HasTicket = false;
			Query.QueryState = QueryState.Query;

			_fillLeftTicketService?.Init(Query);

			//页面参数
			if (!InitQueryInfo())
			{
				OnTicketQueryFailed();
				return;
			}

			var fromName = Query.FromStationName;
			var fromCode = Query.FromStationCode;
			var toName = Query.ToStationName;
			var toCode = Query.ToStationCode;
			var date = Query.CurrentDepartureDate;

			_randomService.BeforeQuery(ref fromCode, ref fromName, ref toCode, ref toName);

			//组装查询参数
			var query = new NameValueCollection();
			query.Add("leftTicketDTO.train_date", date.ToString("yyyy-MM-dd"));
			query.Add("leftTicketDTO.from_station", fromCode);
			query.Add("leftTicketDTO.to_station", toCode);
			query.Add("purpose_codes", Query.QueryStudentTicket ? "0X00" : "ADULT");

			//设置Cookies
			const string cokDomain = "kyfw.12306.cn";
			const string cokPath = "/";
			var coc = new CookieCollection();
			coc.Add(new Cookie("_jc_save_fromStation", HttpUtility.UrlEncodeUnicode(fromName + "," + fromCode), cokPath, cokDomain));
			coc.Add(new Cookie("_jc_save_toStation", HttpUtility.UrlEncodeUnicode(toName + "," + toCode), cokPath, cokDomain));
			coc.Add(new Cookie("_jc_save_fromDate", date.ToString("yyyy-MM-dd"), cokPath, cokDomain));
			coc.Add(new Cookie("_jc_save_toDate", DateTime.Now.ToString("yyyy-MM-dd"), cokPath, cokDomain));
			coc.Add(new Cookie("_jc_save_wfdc_flag", Query.Resign ? "gc" : "dc", cokPath, cokDomain));

			Session.NetClient.CookieContainer.Add(coc);

			//运行查询
			//LOG请求

			NetClient netClient;
			Dictionary<string, object> contextData = null;

			if (QueryConfiguration.Current.UseAnonymousQuery && Query.QueryCount > 1 && _anonymousCounter > 0 && IsAnonymousQuery == null)
			{
				netClient = _client ?? (_client = new NetClient());
				netClient.CookieContainer.Add(coc);
				new HostContext(Session).FingerprintInfo?.SetToNetClient(netClient);

				contextData = new Dictionary<string, object>() { { "anonymous", true } };

				IsAnonymousQuery = true;
			}
			else
			{
				IsAnonymousQuery = false;

				netClient = Session.NetClient;
				IsQueryLogSubmited = SubmitQueryLog(query);
			}
			if (++_anonymousCounter > QueryConfiguration.Current.AnonymousQueryRate)
				_anonymousCounter = 0;

			Statistics.Current.QueryInterfaceStatus.TotalCount++;
			//QueryLeftTicketResponse
			var task = netClient.Create<string>(HttpMethod.Get, _queryInfo.Url, "leftTicket/init", query, isXhr: true, contextData: contextData).Timeout(QueryConfiguration.Current.QueryTimeout).ReadWriteTimeout(QueryConfiguration.Current.QueryTimeout);
			task.Request.HttpRequestCacheLevel = HttpRequestCacheLevel.NoCacheNoStore;
			//task.Request.Headers.Add(HttpRequestHeader.IfNoneMatch, "0");
			//task.Request.IfModifiedSince = DateTime.MinValue;
			task.Request.Headers.Add(HttpRequestHeader.CacheControl, "no-cache");
			task.Request.Headers.Add("Accept-Language", "zh-CN,zh;q=0.9,en;q=0.8");
			task.Send();

			if (task.ContextData.ContainsKey("ip"))
			{
				LastServerIP = task.ContextData["ip"] as string;
			}
			else
			{
				LastServerIP = null;
			}
			if (!task.IsSuccess)
			{
				if (!string.IsNullOrEmpty(LastServerIP))
				{
					//匿名查询出现错误，直接重试：关闭匿名查询
					IsAnonymousQuery = false;
					RunQueryInternal();
					return;
				}

				//如果是系统繁忙，则压栈
				if (task.Exception is SystemBusyException)
				{
					if (EnableSystemBusyService)
						SystemBusyWarningService.Enque(true);
				}
				else
				{
					if (EnableSystemBusyService)
						SystemBusyWarningService.Enque(false);
				}

				if (task.Exception is WebException)
				{
					var ex = task.Exception as WebException;

					if (ex.Status == WebExceptionStatus.Timeout)
					{
						Statistics.Current.QueryInterfaceStatus.TimeoutCount++;
						Message = "服务器操作超时，频繁超时请修改超时设置";

						if (EnableTimeoutService && QueryConfiguration.Current.TimeoutAutoIncreaseSetting)
							QueryTimeoutWarningService.Enque(true);
						OnTicketQueryFailed();

						return;
					}
					if (ex.Status == WebExceptionStatus.ConnectFailure)
					{
						Statistics.Current.QueryInterfaceStatus.ConnectErrorCount++;
						Message = "无法连接到服务器";
					}
					else
					{
						Statistics.Current.QueryInterfaceStatus.NetworkErrorCount++;
						Message = ex.Message + "(" + ex.Status + ")";
					}
				}
				else if (task.Exception != null)
				{
					Statistics.Current.QueryInterfaceStatus.DataExceptionCount++;

					Message = "错误：" + task.Exception.Message;
				}
				else if (task.Response != null)
				{
					var code = (int)task.Response.Status;
					if (code == 200)
						Statistics.Current.QueryInterfaceStatus.Code200Count++;
					else if (code == 302)
						Statistics.Current.QueryInterfaceStatus.Code302Count++;
					else if (code == 405)
					{
						//405错误，尝试重新初始化DeviceID
						if (IsAnonymousQuery == false && ++_code405Count > 5)
						{
							//ModuleManager.Instance.GlobalKernel.Get<IFingerprintService>().ClearFpInfo(this.Session);
							//ClearQueryInfo();
						}

						Statistics.Current.QueryInterfaceStatus.Code405Count++;
					}
					else if (code == 502)
						Statistics.Current.QueryInterfaceStatus.Code502Count++;

					Message = $"服务器返回：{(int)task.Response.Status}{MessageTranslator.GetMemoForCode((int)task.Response.Status)}";
				}
				else
				{
					Statistics.Current.QueryInterfaceStatus.EmptyReponseCount++;
					Message = "<服务器无响应>";
				}
				if (EnableTimeoutService && QueryConfiguration.Current.TimeoutAutoIncreaseSetting)
					QueryTimeoutWarningService.Enque(false);

				OnTicketQueryFailed();
				return;
			}

			_code405Count = 0;
			if (EnableTimeoutService && QueryConfiguration.Current.TimeoutAutoIncreaseSetting)
				QueryTimeoutWarningService.Enque(false);
			if (EnableSystemBusyService)
				SystemBusyWarningService.Enque(false);

			if (task.Result.IsNullOrEmpty())
			{
				Statistics.Current.QueryInterfaceStatus.DataEmptyCount++;
				Message = "服务器并未返回任何数据";
				OnTicketQueryFailed();
				return;
			}
			IQueryLeftTicketResponse response;
			try
			{
				if (task.Result.IndexOf("\"flag\":\"1\"", StringComparison.OrdinalIgnoreCase) != -1)
				{
					response = JsonConvert.DeserializeObject<QueryLeftTicketResponseSimple>(task.Result);
				}
				else
				{
					response = JsonConvert.DeserializeObject<QueryLeftTicketResponseComplex>(task.Result);
				}
			}
			catch (Exception e)
			{
				Message = "无法识别的服务器响应，可能官网已升级：" + task.Result;
				OnTicketQueryFailed();
				return;
			}
			if (!string.IsNullOrEmpty(response.QueryUrl))
			{
				Statistics.Current.QueryInterfaceStatus.QueryInterfaceChangeCount++;
				_queryInfo.Url = response.QueryUrl;
				RunQueryInternal();
				return;
			}

			if (!response.HasData)
			{
				Message = response.GetErrorMessages(Assets.SR.Error_DefaultServerError);

				if (Message.IndexOf("查询失败") != -1 || LastServerIP != null)
				{
					Statistics.Current.QueryInterfaceStatus.ResultFailedCount++;

					//查询失败策略。如果是匿名查询，则不使用匿名查询重新查一次
					if (IsAnonymousQuery == true)
					{
						IsAnonymousQuery = false;
						RunQueryInternal();
						return;
					}
					if (IsQueryLogSubmited != true)
					{
						//如果没有发送日志，则重新发送查询日志后重新查一次
						IsQueryLogSubmited = SubmitQueryLog(query, false, true);
						RunQueryInternal();
						return;
					}

					//既不是匿名查询，发送了日志后还是失败了，则返回错误。
				}
				else
				{
					Statistics.Current.QueryInterfaceStatus.OtherFailedCount++;
				}

				NotInSellDate = Message.IndexOf("不在预售日期", StringComparison.OrdinalIgnoreCase) != -1;
				OnTicketQueryFailed();
				return;
			}

			//设置相关信息
			DataTime = task.Response.Date;

			var result = response.ToQueryResult(Query);
			//缓存结果检测
			if (task.Response.IsCachedByCdn)
			{
				IsCache = true;
			}
			//else if (task.Response.Headers["i"] != null)
			//{
			//	IsCache = true;
			//}
			else if (result.Count > 0)
			{
				var t = result[0];
				IsCache = IsHitSecureStrCache(t.Id, t.FromStation.StationName, t.ToStation.StationName, t.QueryLeftTicketItem.start_train_date, LastServerIP, t.SubmitOrderInfo);
			}
			//票价填充
			_fillLeftTicketService?.FillAsync(Query, result).Wait();

			result.Filter(Query);

			//更新站点缓存
			_randomService.ValidateResponse(result);

			//执行预过滤，按照实际选择的车次进行
			if (!TrainID.IsNullOrEmpty())
			{
				var train = result.FirstOrDefault(s => s.Id == TrainID);
				result = Query.CreateQueryResult(1);
				result.Add(train);
			}

			Result = result;

			//填充票价
			var priceService = AppContext.ExtensionManager.GlobalKernel.ResolveOptional<ITicketPriceFillPrice>();
			priceService?.FillPriceAsync(Session, Query, result).Wait();

			AutoSelect = Query.FindMatchedTrain(Result, out var stat);
			AutoSelectStat = stat;
			Query.FoundValidTickets = AutoSelect != null;
			Success = true;

			Query.LastQueryResult = result;

			foreach (var item in result.OriginalList)
			{
				item.QueryParam = Query;
			}

			//检测始发站预售信息
			if (QueryConfiguration.Current.EnableStartStationTip && Query.CurrentDepartureDate >= ParamData.GetMaxTicketDate(Query.QueryStudentTicket))
				DetectStartStationSellInfo(result);
			//候补信息
			Session.GetService<IHbInfoProvider>().FillInfo(result);
			Statistics.Current.QueryInterfaceStatus.SuccessCount++;

			OnTicketQuerySuccess();
		}

		bool SubmitQueryLog(NameValueCollection query, bool async = true, bool force = false)
		{
			if ((!force && !QueryConfiguration.Current.AlwaysSendingQueryLog && !ApiConfiguration.Instance.EnableAlwaysSendQueryLog) && (DateTime.Now - _queryInfo.LastSaveLogTime).TotalSeconds < 5)
				return false;
			_queryInfo.LastSaveLogTime = DateTime.Now;

			if (!_queryInfo.EnableSaveLog)
				return false;

			var ctx = Session.NetClient.Create<string>(HttpMethod.Get, "leftTicket/log", "leftTicket/init", query);
			//ctx.ContextData.Add("not_important", true); //skip online check
			if (async)
				ctx.SendAsync();
			else ctx.Send();

			return true;
		}

		/// <summary>
		/// 引发 <see cref="Finished" /> 事件
		/// </summary>
		protected virtual void OnFinished()
		{
			var handler = Finished;
			if (handler != null)
			{
				if (_operation != null)
					_operation.Post(_ => handler(this, EventArgs.Empty), null);
				else
					handler(this, EventArgs.Empty);
			}
			_operation = null;
		}

		/// <summary>
		/// 引发 <see cref="IsBusyChanged" /> 事件
		/// </summary>
		protected virtual void OnIsBusyChanged()
		{
			var handler = IsBusyChanged;
			if (handler != null)
			{
				if (_operation != null)
					_operation.Post(_ => handler(this, EventArgs.Empty), null);
				else
					handler(this, EventArgs.Empty);
			}
		}


		/// <summary>
		/// 引发 <see cref="TicketQueryFailed" /> 事件
		/// </summary>
		protected virtual void OnTicketQueryFailed()
		{
			IsBusy = false;

			var handler = TicketQueryFailed;
			if (handler != null)
			{
				if (_operation != null)
					_operation.Post(_ => handler(this, EventArgs.Empty), null);
				else
					handler(this, EventArgs.Empty);
			}
			OnFinished();
		}

		/// <summary>
		/// 引发 <see>
		///     <cref>TicketQuerySuccess</cref>
		/// </see>
		///     事件
		/// </summary>
		protected virtual void OnTicketQuerySuccess()
		{
			IsBusy = false;

			ElapsedTime = DateTime.Now - StartTime.Value;

			var handler = TicketQuerySuccess;
			if (handler != null)
			{
				if (_operation != null)
					_operation.Post(_ => handler(this, EventArgs.Empty), null);
				else
					handler(this, EventArgs.Empty);
			}
			OnFinished();

			//扩展
			AppContext.ExtensionManager.Extensions.ForEach(s => s.OnTicketQuerySuccess(this, Result));
		}

		/// <summary>
		/// 开始查票
		/// </summary>
		public void RunQuery()
		{
			if (_operation != null || IsBusy)
				return;

			IsQueryLogSubmited = null;
			IsAnonymousQuery = null;

			//开始异步操作
			IsBusy = true;
			_operation = AsyncOperationManager.CreateOperation(null);
			System.Threading.ThreadPool.QueueUserWorkItem(_ => RunQueryInternal(), null);
		}

		/// <summary>
		/// 开始查票
		/// </summary>
		public Task<QueryResult> RunQueryAsync()
		{
			if (_operation != null || IsBusy)
				throw new InvalidOperationException();

			IsQueryLogSubmited = null;
			IsAnonymousQuery = null;

			//开始异步操作
			IsBusy = true;

			return Task<QueryResult>.Factory.StartNew(() =>
			{
				RunQueryInternal();

				return Result;
			});
		}


		/// <summary>
		/// 开始查票
		/// </summary>
		public void RunQuerySync()
		{
			if (_operation != null || IsBusy)
				return;

			IsQueryLogSubmited = null;
			IsAnonymousQuery = null;

			//开始异步操作
			IsBusy = true;
			RunQueryInternal();
		}


		#region 自动匹配结果

		/// <summary>
		/// 获得自动匹配的结果
		/// </summary>
		public AutoSelectResult AutoSelect { get; private set; }

		public AutoSelectStat AutoSelectStat { get; private set; }

		#endregion


		/// <summary>
		/// 获得上次查询的数据时间
		/// </summary>
		public DateTime? DataTime { get; private set; }
		/*
				static readonly char[] _allCodes = new[] { 'D', 'Z', 'T', 'K', '*' };
		*/

		public TimeSpan? ElapsedTime { get; private set; }

		/// <summary>
		/// 获得或设置当前的请求是否是匿名请求
		/// </summary>
		public bool? IsAnonymousQuery { get; set; }

		/// <summary>
		/// 获得或设置是否正在忙碌
		/// </summary>
		public bool IsBusy
		{
			get => _isBusy;
			private set
			{
				if (_isBusy == value) return;
				_isBusy = value;
				OnIsBusyChanged();
			}
		}

		/// <summary>
		/// 获得上次的查询是否是缓存
		/// </summary>
		public bool IsCache { get; private set; }

		/// <summary>
		/// 获得或设置是否此次发送了QueryLog
		/// </summary>
		public bool? IsQueryLogSubmited { get; set; }

		public string LastServerIP { get; set; }

		public bool LoadVerifyData { get; set; }

		/// <summary>
		/// 获得查票中的信息
		/// </summary>
		public string Message { get; private set; }

		public bool NotInSellDate { get; private set; }

		/// <summary>
		/// 获得查询参数
		/// </summary>
		public TOBA.Entity.QueryParam Query
		{
			get => _query;
			set { _query = value; _randomService.Query = value; }
		}
		//private NetClient _netClient;

		public IQueryTimeoutWarningService QueryTimeoutWarningService => _queryTimeoutWarningService ?? (_queryTimeoutWarningService = Session.ServiceContainer.Resolve<IQueryTimeoutWarningService>());
		public ISystemBusyWarningService SystemBusyWarningService => _systemBusyWarningService ?? (_systemBusyWarningService = Session.ServiceContainer.Resolve<ISystemBusyWarningService>());

		/// <summary>
		/// 获得查询的结果
		/// </summary>
		public QueryResult Result { get; private set; }

		/// <summary>
		/// 结果代码
		/// </summary>
		public int ResultCode { get; set; }

		/// <summary>
		/// 获得创建的会话
		/// </summary>
		public Session Session
		{
			get { return _session; }
			set
			{
				_session = value;
			}
		}

		/// <summary>
		/// 获得开始查询的时间
		/// </summary>
		public DateTime? StartTime { get; private set; }

		/// <summary>
		/// 获得是否查询成功
		/// </summary>
		public bool Success { get; private set; }

		/// <summary>
		/// 指定要查询的列车ID
		/// </summary>
		public string TrainID { get; set; }

		public KeyValuePair<string, string>? VerifyData { get; set; }

		public string BuildStatusText()
		{
			var queryCount = Query.QueryCount;
			var code = ResultCode;
			var msg = Message;
			var sip = LastServerIP;
			var isCache = IsCache ? "缓存" : "非缓存";
			var time = this.ElapsedTime?.ToFriendlyDisplay() ?? "----";
			var result = Result;

			if (!sip.IsNullOrEmpty())
				sip = "|" + TOBA.Utility.FormatIp(sip);

			string statusMsg = "";
			if (Success)
			{
				if (result.Count > 0)
					statusMsg = $"[{queryCount}{sip}] {result.Count} 趟车 ({isCache}), 耗时 {time}";
				else
				{
					statusMsg = $"[{queryCount}{sip}] 未能查到任何车次，请检查查询设置（12306有时候也会故障导致如此）";
				}
			}
			else
			{
				var exMsg = "";
				switch (code)
				{
					case -1:
						exMsg = "服务器正在装忙！重试即可！";
						break;
					case -10:
						exMsg = "这群老东西说你还没登录！";
						break;
					default:
						exMsg = msg;
						break;
				}
				statusMsg = $"[{queryCount}{sip}] 失败，{exMsg}";
			}

			return statusMsg;
		}

	}
}
