namespace TOBA.Order
{
	using System;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.Linq;
	using System.Net;
	using System.Threading;
	using System.Web;

	using Configuration;

	using Entity;

	using FSLib.Network.Http;

	using Order.Web;

	using Otn;
	using Otn.Entity;

	using Query.Entity;

	using TOBA.Entity;
	using TOBA.Entity.Web;
	using TOBA.WebLib;

	internal class AutoSubmitWorker : SubmitOrderBase
	{


		/// <summary>
		/// 创建 <see cref="SubmitOrderWorker" />  的新实例(SubmitOrderWorker)
		/// </summary>
		public AutoSubmitWorker(Session session, QueryResultItem train, QueryParam query, PassengerInTicket[] passenger)
			: base(session, train, query)
		{
			Passengers = passenger;
		}

		protected override void PrepareInternal()
		{
			base.PrepareInternal();

			var (ok, msg) = PreparePassenger();
			if (!ok)
			{
				SetPrepareOrderError(msg);
				return;
			}

			//如果在进入查票页后没有加载联系人，则自动加载一次联系人。

			//创建信息
			var json_att = !string.IsNullOrEmpty(Session.Attributes) ? "&_json_att=" + System.Web.HttpUtility.UrlEncode(Session.Attributes) : "";
			var purposeCode = IsStudent ? "0X00" : "ADULT";

			//检测
			StateMessage = "检测订单信息....";

			var exdata = "";
			if (Session.DynamicJsData != null)
			{
				exdata = HttpUtility.UrlEncode(Session.DynamicJsData.Key) + "=" + HttpUtility.UrlEncode(Session.DynamicJsData.EncodedValue) + "&";
			}
			//引发强退
			var data = exdata;
			data += "secretStr" + "=" + Train.SubmitOrderInfo + "&" +
				"train_date" + "=" + Date.ToString("yyyy-MM-dd") + "&" +
				"tour_flag" + "=" + "dc" + "&" +
				"purpose_codes" + "=" + purposeCode + "&" +
				"query_from_station_name" + "=" + Query.FromStationName + "&" +
				"query_to_station_name" + "=" + Query.ToStationName + "&" + json_att + "&" +
				"cancel_flag" + "=2&" +
				"bed_level_order_num" + "=000000000000000000000000000000&" +
				"passengerTicketStr" + "=" + PassengerString[0] + "&" +
				"oldPassengerStr" + "=" + PassengerString[1];
			var autosubmitresponse =
				Session.NetClient.RunRequestLoop(_ =>
												Session.NetClient.Create<AutoSubmitOrderResponse>(HttpMethod.Post, "confirmPassenger/autoSubmitOrderRequest", "leftTicket/init", data, isXhr: true),
												_ => StateMessage = $"[{_}] 检测订单信息...."
					);
			if (!autosubmitresponse.IsValid())
			{
				SetPrepareOrderError("网络错误：" + autosubmitresponse.GetErrorMsg());
				return;
			}

			//for test

			if (!autosubmitresponse.Result.Status)
			{
				SetPrepareOrderError(autosubmitresponse.Result.Messages.JoinAsString("").DefaultForEmpty("系统忙。可能是这次登录已经被限制，请等待几分钟再操作，或点击『注销登录』后重新登录。无法解决时请暂时改用浏览器订票。"));
				return;
			}

			var submitOrderResult = autosubmitresponse.Result.Data;

			if (submitOrderResult == null)
			{
				SetPrepareOrderError(autosubmitresponse.Result.Messages.JoinAsString("").DefaultForEmpty("系统返回正常，但是结果不对。可能是这次登录已经被限制，请等待几分钟再操作，或点击『注销登录』后重新登录。无法解决时请暂时改用浏览器订票。"));
				return;
			}

			if (submitOrderResult.isRelogin == "Y")
			{
				SetPrepareOrderError("登录状态异常，请重新登录。");
				return;
			}
			if (submitOrderResult.isNoActive || !submitOrderResult.submitStatus || !submitOrderResult.CheckSeatNum.IsNullOrEmpty())
			{
				SetPrepareOrderError("提交订单失败：" + autosubmitresponse.Result.Data.errMsg);
				return;
			}

			object isCheckOrderInfo;
			var infoarr = submitOrderResult.result.Split('#');
			TrainLocation = infoarr[0];
			KeyCheckIsChange = infoarr[1];
			LeftTicketStr = infoarr[2];
			Async = infoarr[3] == "1";
			isCheckOrderInfo = submitOrderResult.isCheckOrderInfo;
			//是否需要验证码？
			NeedVc = submitOrderResult.IfShowPassCode == "Y";
			NeedVcTime = submitOrderResult.IfShowPassCodeTime;
			VcBaseTime = DateTime.Now;

			//判断席别选择是否可用
			CanChooseBed = submitOrderResult.canChooseBeds == "Y";
			CanChooseSeat = submitOrderResult.canChooseSeats == "Y";
			CanChooseBedMiddle = submitOrderResult.isCanChooseMid == "Y";
			ChooseSeatTypes = submitOrderResult.choose_Seats;


			//检测排队
			if (Async)
			{
				StateMessage = "正在检测排队信息....";
				var seatType = Passengers[0].SeatType;

				var checkQueueData = new NameValueCollection();
				checkQueueData.Add("train_date", Date.ToUniversalTime().ToString("R"));
				checkQueueData.Add("train_no", Train.Id);
				checkQueueData.Add("stationTrainCode", Train.Code);
				checkQueueData.Add("seatType", seatType.ToString());
				checkQueueData.Add("fromStationTelecode", Train.FromStation.Code);
				checkQueueData.Add("toStationTelecode", Train.ToStation.Code);
				checkQueueData.Add("leftTicket", LeftTicketStr);
				checkQueueData.Add("purpose_codes", purposeCode);
				if (isCheckOrderInfo != null)
					checkQueueData.Add("isCheckOrderInfo", isCheckOrderInfo.ToString());
				checkQueueData.Add("_json_att", Session.Attributes ?? "");

				var checkQueueResult = Session.NetClient.RunRequestLoop(_ => Session.NetClient.Create<GetQueueCountResponse>(HttpMethod.Post, "confirmPassenger/getQueueCountAsync", "leftTicket/init", checkQueueData, isXhr: true),
																		_ => StateMessage = $"[{_}] 正在检测排队信息...."
					);
				checkQueueResult.Request.Accept = "application/json, text/javascript, */*; q=0.01";
				checkQueueResult.Send();

				if (!checkQueueResult.IsValid())
				{
					SetPrepareOrderError("网络错误：[ASW-CQ] " + checkQueueResult.GetErrorMsg());
					return;
				}

				if (!checkQueueResult.Result.Status)
				{
					SetPrepareOrderError(checkQueueResult.Result.Messages.JoinAsString("").DefaultForEmpty("系统忙。可能是这次登录已经被限制，请等待几分钟再操作，或点击『注销登录』后重新登录。无法解决时请暂时改用浏览器订票。"));
					return;
				}
				if (checkQueueResult.Result.Data == null)
				{
					SetPrepareOrderError(checkQueueResult.Result.Messages.JoinAsString("").DefaultForEmpty("系统返回正常，但是结果不对。可能是这次登录已经被限制，请等待几分钟再操作，或点击『注销登录』后重新登录。无法解决时请暂时改用浏览器订票。"));
					return;
				}

				if (checkQueueResult.Result.Data.isRelogin == "Y")
				{
					SetPrepareOrderError("登录状态异常，请重新登录。");
					return;
				}

				WaitCount = checkQueueResult.Result.Data.countT.ToInt32();
				SetTicketCount(seatType, checkQueueResult.Result.Data.ticket);
				if (!CheckTicketCount())
					return;


				if (checkQueueResult.Result.Data.op_2 == "true")
				{
					if (OrderConfiguration.Instance.IgnoreQueueError)
					{
						QueueWarning = true;
					}
					else
					{
						SetPrepareOrderError("排队人数过多，不允许提交订单。排队人数=" + (WaitCount > 0 ? WaitCount.ToString() : "<已无票>"));
					}
				}
			}
		}

		//bool _hasTryReloadingPassenger = false;

		/// <summary>
		/// 正式提交订单
		/// </summary>
		protected override bool SubmitOrderInternal()
		{
			if (!base.SubmitOrderInternal())
				return false;

			var purposeCode = IsStudent ? "0X00" : "ADULT";

			//test.import cookies.js


			if (NeedVcTime > 0)
			{
				var safeTime = NeedVcTime - (int)(DateTime.Now - VcBaseTime.Value).TotalMilliseconds;
				StateMessage = $"Delayed for {safeTime}ms";
				if (safeTime > 0)
					Thread.Sleep(safeTime);
			}
			if (!RandCode.IsNullOrEmpty())
			{
				StateMessage = "正在校验验证码...";

				//var result =
				//	Session.NetClient.RunRequestLoop(
				//									 () =>
				//									Session.NetClient.Create(HttpMethod.Post,
				//															"passcodeNew/checkRandCodeAnsyn",
				//															"leftTicket/init",
				//															new { randCode = RandCode, rand = "randp", _json_att = Session.Attributes ?? "" },
				//															new { data = new { result = 0 }, messages = new List<string>() }, isXhr: true
				//										),
				//									_ => StateMessage = $"[{_}] 正在校验验证码..."
				//		);
				//Session.LastCheckRandCodeTime = DateTime.Now;

				//if (!result.IsValid())
				//{
				//	SetSubmitOrderError("校验验证码时网络错误，请重试");
				//	return;
				//}
				//if (result.Result.data == null)
				//{
				//	if (result.Result.messages?.Count > 0)
				//	{
				//		SetSubmitOrderError(result.Result.messages.JoinAsString(""));
				//	}
				//	else
				//		SetSubmitOrderError($"未知错误信息。返回：{(result.ResponseContent as FSLib.Network.Http.ResponseStringContent)?.StringResult}");

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
					SetSubmitOrderError(result);
					return false;
				}
			}

#if DEBUG

			if (TestFlag.BlockOrderSubmit)
				SetSubmitOrderError("[测试]余票不足！");

			if (TestFlag.BlockOrderSubmit || TestFlag.FakeOrderSubmit)
				return false;

#endif


			//提交订单
			StateMessage = "正在提交订单...";
			//座位
			var (seats, beds) = BuildSeatInfo();

			var orderData = new NameValueCollection();
			orderData.Add("passengerTicketStr", PassengerString[0]);
			orderData.Add("oldPassengerStr", PassengerString[1]);
			//if (ForceMode)
			//	orderData.Add("tour_flag", "dc");
			orderData.Add("randCode", RandCode);
			orderData.Add("purpose_codes", purposeCode);
			orderData.Add("key_check_isChange", KeyCheckIsChange);
			//if (!ForceMode)
			orderData.Add("leftTicketStr", LeftTicketStr);
			orderData.Add("train_location", TrainLocation);
			orderData.Add("_json_att", Session.Attributes ?? "");
			orderData.Add("choose_seats", seats);      //二等ABCDF 一等ACDF 特等 AC
			orderData.Add("seatDetailType", beds);    //下铺中铺上铺，每个铺位的数字

			var ordertask = Session.NetClient.RunRequestLoop(
				_ =>
					Session.NetClient.Create<OrderQueueResponse>(
						HttpMethod.Post,
						Async ? "confirmPassenger/confirmSingleForQueueAsys" : "confirmPassenger/confirmSingle",
						"leftTicket/init",
						orderData,
						isXhr: true
					),
				_ => StateMessage = $"[{_}] 正在提交订单..."
			);
			if (!ordertask.IsValid())
			{
				SetSubmitOrderError("网络错误：[ASW-CQ] " + ordertask.GetErrorMsg());
				return false;
			}

			if (!ordertask.Result.Status)
			{
				var msg = ordertask?.Result?.Messages.JoinAsString("");
				SetSubmitOrderError(ordertask.GetExceptionMessage(msg ?? (ordertask.ResponseContent as ResponseStringContent)?.StringResult));
				return false;
			}

			if (ordertask.Result.Data == null)
			{
				SetSubmitOrderError(ordertask.Result.Messages.JoinAsString("").DefaultForEmpty("系统返回正常，但是结果不对。可能是这次登录已经被限制，请等待几分钟再操作，或点击『注销登录』后重新登录。无法解决时请暂时改用浏览器订票。"));
				return false;
			}
			if (!ordertask.Result.Data.SubmitStatus)
			{
				if (ordertask.Result.Data.errMsg?.IndexOf("出票失败") > -1)
				{
					SetSubmitOrderError($"出票失败，可能是流程有问题，请重试一次。如果重试无效，请及时重新查询 ({ordertask.Result.Data.errMsg})");
					//_hasTryReloadingPassenger = true;
					Session.IsPassengerLoaded = false;
					return false;
				}
				SetSubmitOrderError("订单提交错误：" + ordertask.Result.Data.errMsg);
			}
			else if (!Async)
			{
				//直接等待订单
				LoadOrderIdFromNotComplete();
			}

			return true;
		}
	}
}
