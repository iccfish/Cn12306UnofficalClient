using System;
using System.Collections.Generic;
using System.Linq;

namespace TOBA.BackupOrder
{
	using Autofac;

	using Entity;

	using FSLib.Network.Http;

	using Newtonsoft.Json;

	using Otn.Entity;

	using Query.Entity;

	using System.Runtime.Caching;
	using System.Threading.Tasks;

	using TOBA.Entity;


	class BackupOrderService : IBackupOrderService
	{
		static MemoryCache _cache = new MemoryCache("BackupOrder");

		public BackupOrderService(Session session, ILifetimeScope scope)
		{
			Session = session;
			Scope = scope;
		}

		public Session Session { get; }

		public ILifetimeScope Scope { get; }

		public async Task<(bool? faceChecked, bool allow, string msg)> CheckFaceAsync(params BackupCartItem[] items)
		{
			var data = new
			{
				secretList = items.Select(s => s.ToSecretData()).JoinAsString("|") + "|"
			};
			var ctx = await Session.NetClient.RunRequestLoopAsync(_ => Session.NetClient.Create<OtnWebResponse<CheckFaceResult>>(HttpMethod.Post, "afterNate/chechFace", data: data));
			var result = ctx.Result;

			//先行检测人脸认证
			if (ctx.IsValid())
			{
				if (result.Data != null)
				{
					if (!result.Data.LoginFlag)
					{
						Session.BeenForceLogout();
						return (null, false, "登录已失效，请重新登录");
					}

					if (!ctx.Result.Data.FaceFlag)
					{
						//没有通过认证
						return (false, false, GetFaceCheckMsg(result.Data.FaceCheckCode));
					}
					else
					{
						return (true, true, null);
					}
				}
				else
				{
					return (true, false, ctx.Get12306ErrorMessage());
				}
			}
			else
			{
				return (null, false, ctx.Get12306ErrorMessage());
			}
		}

		public async Task<(bool success, string message)> SubmitOrderRequestAsync()
		{
			var data = new
			{
				secretList = Session.BackupOrderCart.ToSecretList()
			};
			var ctx = await Session.NetClient.RunRequestLoopAsync(_ => Session.NetClient.Create<OtnWebResponse<SubmitBackupOrderResponse>>(HttpMethod.Post, "afterNate/submitOrderRequest", data: data));
			var result = ctx.Result;

			var error = ctx.Get12306ErrorMessage();
			if (error != null)
				return (false, error);

			if (!result.Data.Flag)
			{
				if (result.Data.FaceCheck.HasValue)
					return (false, GetFaceCheckMsg(result.Data.FaceCheck.Value));

				return (false, result.GetErrorMessages(JsonConvert.SerializeObject(result.Data)));
			}

			//测试 1:初始化
			var ctxInit = await Session.NetClient.RunRequestLoopAsync(_ => Session.NetClient.Create(HttpMethod.Post, "afterNate/passengerInitApi", result: OtnWebResponse.Create(new
			{
				checkcode = 0,
				if_check_slide_passcode = 0,
				if_check_slide_passcode_token = "",
				isLimitTran = "Y",
				jzdhDateE = "",
				jzdhDateS = "",
				jzdhHourE = "",
				jzdhHourS = "",
				limitTranStr = new List<string>(),
				result_code = 0
			})));
			if (!ctxInit.IsValid() || !ctxInit.Result.Status)
			{
				return (false, ctxInit.Get12306ErrorMessage());
			}
			if (ctxInit.Result.Data.result_code != 0)
			{
				return (false, $"无效响应：{ctxInit.ResponseContent.RawStringResult}");
			}
			//时间
			var initData = ctxInit.Result.Data;
			var cart = Session.BackupOrderCart;
			if (!initData.jzdhDateS.IsNullOrEmpty() && !initData.jzdhHourS.IsNullOrEmpty())
			{
				cart.HbStartTime = DateTime.Parse(initData.jzdhDateS).Add(TimeSpan.Parse(initData.jzdhHourS));
			}
			else
			{
				cart.HbStartTime = DateTime.Now;
			}

			if (!initData.jzdhDateE.IsNullOrEmpty() && !initData.jzdhHourE.IsNullOrEmpty())
			{
				cart.HbEndTime = DateTime.Parse(initData.jzdhDateE).Add(TimeSpan.Parse(initData.jzdhHourE));
			}
			else
			{
				cart.HbEndTime = cart.Items.Select(s => s.Train.Date).Min().AddDays(-1).AddHours(19).AddMinutes(30);
			}

			//滑动验证
			cart.SlideToken = initData.if_check_slide_passcode_token;
			cart.NeedSlideVc = initData.if_check_slide_passcode == 1;
			cart.SlideCSessionId = null;
			cart.SlideSig = null;

			return (true, null);
		}

		public async Task<(int level, string info)> GetSuccessRateAsync(BackupCartItem item)
		{
			var (level, msg) = await GetSuccessRateAsync(item.Train.SubmitOrderInfo, item.Seat);

			item.SetSuccessRate(level, msg);

			return (level, msg);
		}

		public async Task<(int level, string info)> GetSuccessRateAsync(string submitInfo, char seat)
		{
			var data = new
			{
				successSecret = $"{submitInfo}#{seat}"
			};
			var ctx = await Session.NetClient.RunRequestLoopAsync(_ =>
				Session.NetClient.Create(HttpMethod.Post, "afterNate/getSuccessRate", data: data, result: OtnWebResponse.Create(new { flag = FSLib.Extension.CollectionUtility.CreateAnymousTypeList(new { level = 0, info = "" }) }))
			);
			var result = ctx.Result;

			var error = ctx.Get12306ErrorMessage();
			if (error != null || !(result.Data?.flag?.Count > 0))
			{
				return (0, "查询候补人数出错");
			}

			return (result.Data.flag[0].level, result.Data.flag[0].info);
		}



		/// <summary>
		/// 取消排队中订单
		/// </summary>
		/// <returns></returns>
		public async Task<(bool ok, string msg)> CancelWaitingOrderAsync()
		{
			var apiUrl = "afterNateOrder/cancelWaitingHOrder";

			var ctx = await Session.NetClient.RunRequestLoopAsync(
				_ => Session.NetClient.Create(
					HttpMethod.Post,
					apiUrl,
					result: OtnWebResponse.Create(new { flag = false, msg = "" })
				)
			);
			var result = ctx.Result;

			var error = ctx.Get12306ErrorMessage();
			if (error != null)
			{
				return (false, error);
			}

			if (!result.Data.flag)
				return (false, result.Data.msg.DefaultForEmpty("哎哟……取消排队失败了……"));

			return (true, null);
		}

		/// <summary>
		/// 提交候补订单
		/// </summary>
		/// <returns></returns>
		public async Task<(bool ok, string msg, ConfirmBackupOrderResponseData)> CommitBackupOrderAsync()
		{
			var apiUrl = "afterNate/confirmHB";
			var data = Session.BackupOrderCart.ToOrderParam();


			var ctx = await Session.NetClient.RunRequestLoopAsync(
				_ => Session.NetClient.Create<OtnWebResponse<ConfirmBackupOrderResponseData>>(
					HttpMethod.Post,
					apiUrl,
					data: data
				)
			);
			var result = ctx.Result;

			var error = ctx.Get12306ErrorMessage();
			if (error != null)
			{
				return (false, error, null);
			}

			if (result.Data?.Flag != true)
				return (false, ctx.Get12306ErrorMessage(Assets.SR.Error_DefaultServerError), result.Data);

			return (true, null, result.Data);
		}

		/// <summary>
		/// 查询候补订单队列
		/// </summary>
		/// <returns></returns>
		public async Task<(bool ok, string msg, QueryBackupOrderQueueResponse)> QueryHbQueueAsync()
		{
			var apiUrl = "afterNate/queryQueue";

			var ctx = await Session.NetClient.RunRequestLoopAsync(
				_ => Session.NetClient.Create<OtnWebResponse<QueryBackupOrderQueueResponse>>(
					HttpMethod.Post,
					apiUrl
				)
			);
			var result = ctx.Result;

			var error = ctx.Get12306ErrorMessage();
			if (error != null)
			{
				return (false, error, null);
			}

			if (!result.Data.Flag || result.Data.Status == -1)
				return (false, result.GetErrorMessages(), result.Data);

			if (!result.Data.IsAsync && result.Data.Status == 2)
				return (false, "没有排队中候补订单", null);

			return (true, null, result.Data);
		}

		public string GetFaceCheckMsg(int code)
		{
			switch (code)
			{
				case 1:
				case 11:
					return "证件信息正在审核中，请您耐心等待，审核通过后可继续完成候补操作。";
				case 3:
				case 13:
					return "证件信息审核失败，请检查所填写的身份信息内容与原证件是否一致。";
				case 4:
				case 14:
					return "通过人证一致性核验的用户及激活的会员可以提交候补需求，请在铁路12306app上完成人证核验";
				case 2:
				case 12:
					return null;
				default:
					return "系统忙，请稍后再试！";
			}
		}

		/// <summary>
		/// 获得未完成候补订单（待支付）
		/// </summary>
		/// <returns></returns>
		public async Task<(bool ok, string msg, UnpayBackupOrder)> GetUnpayHbOrderAsync()
		{
			var apiUrl = "afterNateOrder/queryQueue";

			var ctx = await Session.NetClient.RunRequestLoopAsync(
				_ => Session.NetClient.Create<OtnWebResponse<UnpayBackupOrder>>(
					HttpMethod.Post,
					apiUrl
				)
			);
			var result = ctx.Result;

			var error = ctx.Get12306ErrorMessage();
			if (error != null)
			{
				return (false, error, null);
			}

			if (!result.Data.Flag || result.Data.Status == -1)
				return (false, result.GetErrorMessages(), result.Data);

			if (!result.Data.IsAsync && result.Data.Status == 2)
				return (false, "没有排队中候补订单", null);

			return (true, null, result.Data);
		}

		/// <summary>
		/// 获得未完成候补订单（待兑现）
		/// </summary>
		/// <returns></returns>
		public async Task<(bool ok, string msg, List<BackupOrderItem>)> GetUnprocessedHbOrderItemsAsync()
		{
			var apiUrl = "afterNateOrder/queryUnHonourHOrder";
			var pageNo = 0;
			var pageTotal = 1;
			var resultList = new List<BackupOrderItem>();


			while (pageNo < pageTotal)
			{
				var ctx = await Session.NetClient.RunRequestLoopAsync(
					_ => Session.NetClient.Create(
						HttpMethod.Post,
						apiUrl,
						data: new
						{
							page_no = pageNo,
							query_start_date = DateTime.Now.ToString("yyyy-MM-dd"),
							query_end_date = DateTime.Now.Date.AddDays(22).ToString("yyyy-MM-dd")
						},
						result: OtnWebResponse.Create(new { list = new List<BackupOrderItem>() })
					)
				);

				if (!ctx.IsValid() || !ctx.Result.IsValid() || ctx.Result.Data.list == null)
				{
					return (false, ctx.Get12306ErrorMessage(), null);
				}

				resultList.AddRange(ctx.Result.Data.list);
				if (ctx.Result.Data?.list?.Count > 0)
				{
					pageTotal = Math.Max(pageTotal, ctx.Result.Data.list[0].TotalPage);
				}

				pageNo++;
			}

			return (true, null, resultList);
		}

		/// <summary>
		/// 获得已完成订单
		/// </summary>
		/// <returns></returns>
		public async Task<(bool ok, string msg, List<BackupOrderItem>)> GetProcessedHbOrderItemsAsync()
		{
			var apiUrl = "afterNateOrder/queryProcessedHOrder";
			var pageNo = 0;
			var pageTotal = 1;
			var resultList = new List<BackupOrderItem>();


			while (pageNo < pageTotal)
			{
				var ctx = await Session.NetClient.RunRequestLoopAsync(
					_ => Session.NetClient.Create(
						HttpMethod.Post,
						apiUrl,
						data: new
						{
							page_no = pageNo,
							query_start_date = DateTime.Now.ToString("yyyy-MM-dd"),
							query_end_date = DateTime.Now.Date.AddDays(22).ToString("yyyy-MM-dd")
						},
						result: OtnWebResponse.Create(new { list = new List<BackupOrderItem>() })
					)
				);

				if (!ctx.IsValid() || !ctx.Result.IsValid() || ctx.Result.Data.list == null)
				{
					return (false, ctx.Get12306ErrorMessage(), null);
				}

				resultList.AddRange(ctx.Result.Data.list);
				if (ctx.Result.Data?.list?.Count > 0)
				{
					pageTotal = Math.Max(pageTotal, ctx.Result.Data.list[0].TotalPage);
				}

				pageNo++;
			}

			return (true, null, resultList);
		}

		/// <summary>
		/// 查询退款信息
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		public async Task<(bool ok, string msg)> QueryRefundInfo(BackupOrderItem order)
		{
			if (order == null) throw new ArgumentNullException(nameof(order));

			if (!order.HasRefundInfo)
			{
				return (false, "无退款信息");
			}

			if (order.RefundInfo != null)
				return (true, null);

			var data = new
			{
				trade_no = order.RefundTradeNo,
				trade_mode = 1,
				query_flag = 2,
				start_time = order.ReserveTime.ToString("yyyyMMddHHmmss"),
				stop_time = order.ReserveTime.AddDays(15).AddSeconds(-1).ToString("yyyyMMddHHmmss")
			};
			var apiUrl = "queryRefund/queryRefundInfo";

			var ctx = await Session.NetClient.RunRequestLoopAsync(
				_ => Session.NetClient.Create<OtnWebResponse<RefundInfoContainer>>(
					HttpMethod.Post,
					apiUrl,
					data: data
				)
			);

			if (!ctx.IsValid() || !ctx.Result.IsValid() || ctx.Result.Data?.Flag != true)
			{
				return (false, ctx.Get12306ErrorMessage());
			}

			order.RefundInfo = ctx.Result.Data.Data;

			return (true, null);
		}

		public async Task<(bool ok, string msg, Dictionary<string, string> form)> CreatePayForm(BackupOrderItem order)
		{
			var ctx = await Session.NetClient.RunRequestLoopAsync(
					_ => Session.NetClient.Create<OtnWebResponse<BaseOtnApiResponseWithFlagAndMsg>>(
						HttpMethod.Post,
						"afterNateOrder/continuePayNoCompleteMyOrder",
						data: new { reserve_no = order.ReserveNo })
				);
			if (!ctx.IsValid() || ctx.Result.Data?.Flag != true)
			{
				return (false, ctx.Get12306ErrorMessage(Assets.SR.Error_DefaultServerError), null);
			}

			var ctxPay = await Session.NetClient.RunRequestLoopAsync(
				_ => Session.NetClient.Create(
					HttpMethod.Post,
					"afterNatePay/paycheck",
					result: OtnWebResponse.Create(new { flag = false, payForm = new Dictionary<string, string>() }))
			);
			if (!ctxPay.IsValid() || ctxPay.Result.Data?.flag != true || ctxPay.Result.Data?.payForm == null)
			{
				return (false, ctx.Get12306ErrorMessage(Assets.SR.Error_DefaultServerError), null);
			}

			return (true, null, ctxPay.Result.Data.payForm);
		}

		public async Task<(bool ok, string msg)> CancelNotCompleteOrder(BackupOrderItem order, Func<double, bool> confirm)
		{

			var api = order.StatusCode == 1 ? "afterNateOrder/cancelNotComplete" : "afterNateOrder/reserveReturnCheck";
			var data = order.StatusCode == 1 ? (object)new { reserve_no = order.ReserveNo } : new { sequence_no = order.ReserveNo };

			var ctx = await Session.NetClient.RunRequestLoopAsync(
					_ => Session.NetClient.Create(
						HttpMethod.Post,
						api,
						data: data,
						result: OtnWebResponse.Create(new { flag = false, msg = "", data = 0.0 }))
				);
			if (!ctx.IsValid() || ctx.Result.Data?.flag != true)
			{
				return (false, ctx.Get12306ErrorMessage(Assets.SR.Error_DefaultServerError));
			}

			if (order.StatusCode > 1)
			{
				//确认退费
				if (confirm?.Invoke(ctx.Result.Data.data) == false)
					return (false, "已终止取消订单");

				var ctxRet = await Session.NetClient.RunRequestLoopAsync(
					_ => Session.NetClient.Create(
						HttpMethod.Post,
						"afterNateOrder/reserveReturn",
						data: data,
						result: OtnWebResponse.Create(new { flag = false, msg = "" }))
				);
				if (!ctxRet.IsValid() || ctxRet.Result.Data?.flag != true)
				{
					return (false, ctxRet.Get12306ErrorMessage(Assets.SR.Error_DefaultServerError));
				}
			}

			return (true, null);
		}

		public async Task DetectAutoHbTrainsToCart(QueryParam query, QueryResult result)
		{
			var cart = Session.BackupOrderCart;
			if (cart.InAutoSubmit)
				return;

			var date = query.CurrentDepartureDate;
			if (cart.Items.Count(s => s.Query.CurrentDepartureDate == date) > 0)
				return;

			var trains = result.Where(s => s.AllowBackup && !s.NoTicketNeeded && s.Selected > 0).OrderBy(s => (s.FromStation.IsFirst ? 1 : 0) + (s.ToStation.IsEnd ? 1 : 0) + s.Selected * 10);
			var count = 0;
			foreach (var item in trains)
			{
				//这里不考虑席别优先还是车次优先
				foreach (var seat in query.AutoPreSubmitConfig.SeatList)
				{
					if (!item.IsSeatBackupAvailable(seat))
						continue;
					var sd = item.TicketCount.GetTicketData(seat, true);
					if (sd?.NoTicket != true || sd?.NotSell != false)
						continue;

					cart.Items.Add(new BackupCartItem() { Seat = item.FindCorrectSeat(seat), Train = item });

					count++;
					if (count >= 2) break;
				}

				if (count >= 2)
					break;
			}

			if (count > 0)
			{
				cart.Passengers.Clear();
				cart.ImportQueryPassengers(query.AutoPreSubmitConfig.Passenger);
				cart.StartOrStopAutoSubmit();
			}
		}
	}
}
