namespace TOBA.Order
{
	using System;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.Diagnostics;
	using System.Linq;
	using System.Net;
	using System.Text.RegularExpressions;
	using System.Threading;
	using System.Threading.Tasks;
	using System.Web;

	using Configuration;

	using FSLib.Network.Http;

	using Newtonsoft.Json;

	using Order.Web;

	using Otn;

	using Query.Entity;

	using TOBA.Entity;
	using TOBA.Entity.Web;
	using TOBA.WebLib;

	internal class SubmitOrderWorker : SubmitOrderBase
	{
		public string Token { get; set; }

		/// <summary>
		/// 创建 <see cref="SubmitOrderWorker" />  的新实例(SubmitOrderWorker)
		/// </summary>
		public SubmitOrderWorker(Session session, QueryResultItem train, QueryParam query)
			: base(session, train, query)
		{
		}

		/// <summary>
		/// 准备数据
		/// </summary>
		protected override void PrepareInternal()
		{
			var json_att = !string.IsNullOrEmpty(Session.Attributes) ? "_json_att=" + System.Web.HttpUtility.UrlEncode(Session.Attributes) : "undefined";
			var purposeCode = IsStudent ? "0X00" : "ADULT";

			//var rk = Base32.CreateRandomKey();

			//检测
			StateMessage = "检测订单信息....";
			var exdata = "";
			if (Session.DynamicJsData != null)
			{
				exdata = HttpUtility.UrlEncode(Session.DynamicJsData.Key) + "=" + HttpUtility.UrlEncode(Session.DynamicJsData.EncodedValue) + "&";
			}
			var data = exdata;
			data += "secretStr=" + Train.SubmitOrderInfo + "&train_date" + "=" + Date.ToString("yyyy-MM-dd") + "&back_train_date" + "=" + (Query.Resign ? Query.ResignDate : Date).ToString("yyyy-MM-dd") + "&" +
						"tour_flag" + "=" + (Query.Resign ? "gc" : "dc") + "&" +
						"purpose_codes" + "=" + purposeCode + "&" +
						"query_from_station_name" + "=" + Train.FromStation.StationName + "&" +
						"query_to_station_name" + "=" + Train.ToStation.StationName + "&" + json_att;
			var task = Session.NetClient.RunRequestLoop(
				_ => Session.NetClient.Create(
					HttpMethod.Post,
					"leftTicket/submitOrderRequest",
					"leftTicket/query",
					data,
					new { status = false, data = "", messages = new string[0] }
				),
				_ => StateMessage = $"[{_}] 检测订单信息....");
			if (!task.IsValid())
			{
				SetPrepareOrderError("网络错误：" + task.GetExceptionMessage("发生网络错误"));
				return;
			}

			try
			{
				var submitdata = task.Result;
				//data为Y意思是 “您选择的列车距开车时间很近了，请确保有足够的时间抵达车站，并办理安全检查、实名制验证及检票等手续，以免耽误您的旅行。”，不是说不能订票
				if (!submitdata.status)
				{
					SetPrepareOrderError(submitdata.messages.JoinAsString("").DefaultForEmpty("无法提交订单：" + JsonConvert.SerializeObject(task.Result)));
					return;
				}
			}
			catch (Exception)
			{
				SetPrepareOrderError("无法提交订单：" + task.GetExceptionMessage("无法提交请求"));
				return;
			}


			//发送请求
			var strtask = Session.NetClient.RunRequestLoop(
				_ => Session.NetClient.Create(
					HttpMethod.Post,
					Query.Resign ? "confirmPassenger/initGc" : "confirmPassenger/initDc",
					"leftTicket/init",
					new
					{
						_json_att = Session.Attributes ?? ""
					},
					""
				),
				_ => StateMessage = $"[{_}] 正在准备订单信息....");
			if (!strtask.IsValid())
			{
				SetPrepareOrderError("网络错误：" + strtask.GetExceptionMessage("无法加载预定页面"));
				return;
			}

			//token
			var token = Regex.Match(strtask.Result, @"var\s*globalRepeatSubmitToken\s*=\s*['""]([^'""]+)[""']");
			if (!token.Success)
			{
				SetPrepareOrderError("网络错误：" + "无法获得Token");
				return;
			}

			Token = token.Groups[1].Value;
			FormData = new Dictionary<string, string>();

			var trimKey = new[] { '"', '\'', ' ', '	' };
			foreach (var source in Regex.Matches(strtask.Result, @"(init_seatTypes|defaultTicketTypes|ticket_seat_codeMap|init_cardTypes|ticketInfoForPassengerForm|orderRequestDTO|oldTicketDTOs|if_check_slide_passcode_token|if_check_slide_passcode)\s*=\s*([^;]+);", RegexOptions.IgnoreCase | RegexOptions.Singleline)
										.Cast<Match>()
										.Where(s => s.Success))
			{
				var value = source.Groups[2].Value.Trim(trimKey);
				if (value.IsNullOrEmpty())
					continue;

				if (FormData.ContainsKey(source.Groups[1].Value))
					FormData[source.Groups[1].Value] = value;
				else
					FormData.Add(source.Groups[1].Value, value);
			}
			if (!FormData.ContainsKey("ticketInfoForPassengerForm"))
			{
				SetPrepareOrderError("网络错误：" + "服务器返回的数据无效");
				return;
			}

			var orderdata = JsonConvert.DeserializeAnonymousType(FormData["ticketInfoForPassengerForm"],
				new
				{
					key_check_isChange = "",
					leftTicketStr = "",
					train_location = "",
					isAsync = ""
				});
			if (orderdata == null)
			{
				SetPrepareOrderError("网络错误：服务器返回的数据无效, " + (FormData["ticketInfoForPassengerForm"] ?? ""));
				return;
			}

			KeyCheckIsChange = orderdata.key_check_isChange;
			LeftTicketStr = orderdata.leftTicketStr;
			TrainLocation = orderdata.train_location;
			Async = orderdata.isAsync == "1";

			////加载联系人
			//Session.IsPassengerLoaded = false;
			//Session.LoadPassengers();

			//滑动验证
			NeedSlideVc = FormData["if_check_slide_passcode"] == "1";//||true;
			SlideVcToken = FormData["if_check_slide_passcode_token"];//?? $"FFFF0N000000000085DE:1577082658881:0.14912449110297998";
		}

		private string _isCheckOrderInfo, _purposeCode, _currentPage, _doneHmd;
		private bool _orderInfoChecked;

		bool CheckOrderInfo()
		{
			_purposeCode = IsStudent ? "0X00" : "00";
			_currentPage = Query.Resign ? "confirmPassenger/initGc" : "confirmPassenger/initDc";

			//检测
			StateMessage = "检测订单信息....";
			var exdata = "";
			if (Session.DynamicJsData != null)
			{
				exdata = HttpUtility.UrlEncode(Session.DynamicJsData.Key) + "=" + HttpUtility.UrlEncode(Session.DynamicJsData.EncodedValue) + "&";
			}

			var data = "cancel_flag=2&" +
				"bed_level_order_num=000000000000000000000000000000&" +
				"passengerTicketStr=" + PassengerString[0] + "&" +
				"oldPassengerStr=" + PassengerString[1] + "&" +
				"tour_flag=" + (Query.Resign ? "gc" : "dc") +
				"&randCode=" + RandCode +
				"&" + exdata +
				"_json_att=" + (Session.Attributes ?? "") +
				"&REPEAT_SUBMIT_TOKEN=" + Token
				+ "&whatsSelect=1&scene=nc_login"; //受让人为0
			data += $"&sessionId={HttpUtility.UrlEncode(SlideCSessionId)}&sig={HttpUtility.UrlEncode(SlideSig)}";

			//检测订单信息
			var checkOrderInfoTask = Session.NetClient.RunRequestLoop(
				_ => Session.NetClient.Create(
					HttpMethod.Post,
					"confirmPassenger/checkOrderInfo",
					_currentPage,
					data,
					new
					{
						status = false,
						data = new
						{
							submitStatus = false,
							isRelogin = false,
							isNoActive = false,
							errMsg = "",
							get608Msg = "",
							isCheckOrderInfo = "",
							doneHMD = "",
							canChooseBeds = "N",
							canChooseSeats = "N",
							choose_Seats = "MOP9",
							isCanChooseMid = "N",
							ifShowPassCodeTime = 1,
							ifShowPassCode = "N"
						},
						messages = new string[0]
					}),
				_ => StateMessage = $"[{_}] 检测订单信息....");
			if (checkOrderInfoTask == null || !checkOrderInfoTask.IsSuccess)
			{
				SetSubmitOrderError("网络错误：[CHECKORDERINFO] 提交到服务器是发生异常错误, " + (checkOrderInfoTask.GetExceptionMessage()));
				return false;
			}

			var checkResult = checkOrderInfoTask.Result.data;
			if (!checkOrderInfoTask.Result.status || checkResult == null)
			{
				SetSubmitOrderError("网络错误: 提交信息错误，" + Msg.Translate(checkOrderInfoTask.Result?.messages?.JoinAsString("") ?? "无错误信息") + $"({checkOrderInfoTask.ResponseContent?.RawStringResult})");
				return false;
			}
			if (checkResult.isRelogin)
			{
				SetSubmitOrderError("登录状态异常，请重新登录。");
				return false;
			}
			if (!checkResult.submitStatus || !checkResult.get608Msg.IsNullOrEmpty())
			{
				SetSubmitOrderError(Msg.Translate(checkResult.errMsg.DefaultForEmpty(checkResult.get608Msg).DefaultForEmpty("系统忙，请重试")));
				return false;
			}

			NeedVc = checkResult.ifShowPassCode == "Y";
			NeedVcTime = checkResult.ifShowPassCodeTime;
			VcBaseTime = DateTime.Now;
			Trace.TraceInformation($"CheckOrderInfo: NeedVc={NeedVc} NeedVcTime={NeedVcTime} VcBaseTime={VcBaseTime}");

			_isCheckOrderInfo = checkResult.isCheckOrderInfo;
			_doneHmd = checkResult.doneHMD;

			//判断席别选择是否可用
			CanChooseBed = checkResult.canChooseBeds == "Y";
			CanChooseSeat = checkResult.canChooseSeats == "Y";
			CanChooseBedMiddle = checkResult.isCanChooseMid == "Y";
			ChooseSeatTypes = checkResult.choose_Seats;
			_orderInfoChecked = true;

			return true;
		}

		/// <summary>
		/// 正式提交订单
		/// </summary>
		protected override bool SubmitOrderInternal()
		{
			if (!base.SubmitOrderInternal())
				return false;

			if (!_orderInfoChecked && !CheckOrderInfo())
			{
				return false;
			}

			if (NeedVc == true && RandCode.IsNullOrEmpty())
			{
				SetSubmitOrderError("[NEEDVC] 请输入验证码");
				return false;
			}


			if (NeedVcTime > 0)
			{
				var safeTime = NeedVcTime - (int)(DateTime.Now - VcBaseTime.Value).TotalMilliseconds;
				StateMessage = $"Delayed for {safeTime}ms";
				Trace.TraceInformation($"SUMITORDER: DELAY {safeTime}ms");
				if (safeTime > 0)
					Thread.Sleep(safeTime);
			}
			if (!RandCode.IsNullOrEmpty())
			{
				StateMessage = "正在校验验证码...";

				//var result = Session.NetClient.RunRequestLoop(
				//											 () => Session.NetClient.Create(HttpMethod.Post,
				//																			"passcodeNew/checkRandCodeAnsyn",
				//																			currentPage,
				//																			new {randCode = RandCode, rand = "randp", _json_att = Session.Attributes ?? "", REPEAT_SUBMIT_TOKEN = Token},
				//																			new {data = new {result = 0}}, isXhr: true
				//														),
				//											_ => StateMessage = $"[{_}] 正在校验验证码...");
				//Session.LastCheckRandCodeTime = DateTime.Now;
				//if (!result.IsValid())
				//{
				//	SetSubmitOrderError("校验验证码时网络错误，请重试");
				//	return;
				//}
				//if (result.Result.data.result != 1)
				//{
				//	SetSubmitOrderError("验证码错误，请重试。");
				//	return;
				//}
				var result = Session.NetClient.VerifyRandCode(Query.Resign ? RandCodeType.RandpResign : RandCodeType.Randp, RandCode, null, _ => StateMessage = $"[{_}] 正在检查验证码……");

				if (!result.IsNullOrEmpty())
				{
					SetSubmitOrderError(result.DefaultForEmpty("验证码错误"));
					return false;
				}
			}

			//检测队列信息。仅异步模式下需要。
			if (Async)
			{
				var queueSeat = Passengers[0].SeatType;
				StateMessage = "正在检测排队信息....";
				var checkQueueData = new NameValueCollection
				{
					{ "train_date", Date.Date.ToString("R") },
					//checkQueueData.Add("train_date", "Mon Apr 28 2014 00:00:00 GMT+0800 (中国标准时间)");
					{ "train_no", Train.Id },
					{ "stationTrainCode", Train.Code },
					{ "seatType", queueSeat.ToString() },
					{ "fromStationTelecode", Train.FromStation.Code },
					{ "toStationTelecode", Train.ToStation.Code },
					{ "leftTicket", LeftTicketStr },
					{ "purpose_codes", _purposeCode }
				};
				if (_isCheckOrderInfo != null)
					checkQueueData.Add("isCheckOrderInfo", _isCheckOrderInfo);
				checkQueueData.Add("_json_att", Session.Attributes ?? "");
				checkQueueData.Add("train_location", TrainLocation);
				checkQueueData.Add("REPEAT_SUBMIT_TOKEN", Token);

				var checkQueueResult = Session.NetClient.RunRequestLoop(
					_ => Session.NetClient.Create<GetQueueCountResponse>(HttpMethod.Post, "confirmPassenger/getQueueCount", _currentPage, checkQueueData), _ => StateMessage = $"[{_}] 正在检测排队信息...."
				);

				if (!checkQueueResult.IsValid())
				{
					SetSubmitOrderError("网络错误：" + checkQueueResult?.GetErrorMsg());
					return false;
				}

				if (!checkQueueResult.Result.Status || checkQueueResult.Result.Data == null)
				{
					SetSubmitOrderError(checkQueueResult.Result.Messages.JoinAsString("").DefaultForEmpty("系统忙，请重试"));
					return false;
				}

				if (checkQueueResult.Result.Data.isRelogin == "Y")
				{
					SetSubmitOrderError("登录状态异常，请重新登录。");
					return false;
				}

				SetTicketCount(queueSeat, checkQueueResult.Result.Data.ticket);
				if (!CheckTicketCount())
					return false;

				WaitCount = checkQueueResult.Result.Data.countT.ToInt32();
				if (checkQueueResult.Result.Data.op_2 == "true")
				{
					if (OrderConfiguration.Instance.IgnoreQueueError)
					{
						QueueWarning = true;
					}
					else
					{
						SetSubmitOrderError("排队人数过多，不允许提交订单。排队人数=" + (WaitCount > 0 ? WaitCount.ToString() : "<已无票>"));
						return false;
					}
				}
			}

			//开始提交订单
			StateMessage = "正在提交订单...";

			//座位
			var (seats, beds) = BuildSeatInfo();

			var orderData = new NameValueCollection
			{
				{ "passengerTicketStr", PassengerString[0] },
				{ "oldPassengerStr", PassengerString[1] },
				{ "purpose_codes", _purposeCode },
				{ "key_check_isChange", KeyCheckIsChange },
				{ "leftTicketStr", LeftTicketStr },
				{ "train_location", TrainLocation },
				{ "choose_seats", seats },
				{ "seatDetailType", beds },
				{ "whatsSelect", "1" }, //受让人为0
				{ "roomType", "00" }, //动卧需要
				{ "dwAll", BuyAllSeat ? "Y" : "N" },
				{ "_json_att", Session.Attributes ?? "" },
				{ "REPEAT_SUBMIT_TOKEN", Token }
			};
			if (!RandCode.IsNullOrEmpty())
				orderData.Add("randCode", RandCode);

			//Thread.Sleep(Configuration.SubmitOrder.Current.OrderSubmitDelay);

#if DEBUG

			if (TestFlag.BlockOrderSubmit)
				SetSubmitOrderError("[测试]余票不足！");

			if (TestFlag.BlockOrderSubmit || TestFlag.FakeOrderSubmit)
				return false;

#endif
			//confirmPassenger/confirmSingle
			//confirmPassenger/confirmGo
			//confirmPassenger/confirmBack
			//confirmPassenger/confirmResign

			//延迟
			var submiturl = Async ? (Query.Resign ? "confirmPassenger/confirmResignForQueue" : "confirmPassenger/confirmSingleForQueue") : (Query.Resign ? "confirmPassenger/confirmResign" : "confirmPassenger/confirmSingle");
			var orderResponse = Session.NetClient.RunRequestLoop(
				_ => Session.NetClient.Create(
					FSLib.Network.Http.HttpMethod.Post,
					submiturl,
					_currentPage,
					orderData,
					new
					{
						status = false,
						data = new
						{
							submitStatus = true,
							errMsg = ""
						},
						messages = new string[0]
					}),
				_ => StateMessage = $"[{_}] 正在提交订单..."
			);
			if (orderResponse == null || !orderResponse.IsSuccess)
			{
				SetSubmitOrderError("网络错误：[SUBMITORDER] " + orderResponse?.GetErrorMsg());
				return false;
			}
			var orderresult = orderResponse.Result;
			if (orderresult.status)
			{
				if (!orderresult.data.submitStatus)
				{
					SetSubmitOrderError("提交订单错误：" + orderresult.data.errMsg);
				}
				else if (!Async)
				{
					LoadOrderIdFromNotComplete();
				}
			}
			else
			{
				SetSubmitOrderError(orderresult.messages.JoinAsString("").DefaultForEmpty("系统忙"));
			}

			return true;
		}
	}
}
