namespace TOBA.Order
{
	using System;
	using System.Collections.Generic;
	using FSLib.Extension;
	using System.Linq;
	using System.Net;
	using System.Text.RegularExpressions;

	using Entity;

	using FSLib.Network.Http;

	using Newtonsoft.Json;
	using TOBA.Workers;

	/// <summary>
	/// 订单支付接口
	/// </summary>
	internal class PayOrderWorker : TOBA.Workers.WorkerBase
	{
		/// <summary>
		/// 创建 <see cref="WorkerBase" />  的新实例(WorkerBase)
		/// </summary>
		public PayOrderWorker(Session session)
			: base(session)
		{

		}

		/// <summary>
		/// 获得提交订单信息
		/// </summary>
		public Dictionary<string, string> FormData { get; protected set; }
		public Dictionary<string, string> ParamData { get; protected set; }

		public event EventHandler<GeneralEventArgs<bool>> RequireDirectResign;

		/// <summary>
		/// 引发 <see cref="RequireDirectResign" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnRequireDirectResign(GeneralEventArgs<bool> ea)
		{
			var handler = RequireDirectResign;
			if (handler != null)
				handler(this, ea);
		}

		public event EventHandler PayOrderSuccess;

		/// <summary>
		/// 引发 <see cref="PayOrderSuccess" /> 事件
		/// </summary>
		protected virtual void OnPayOrderSuccess()
		{
			var handler = PayOrderSuccess;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}
		public event EventHandler PayOrderFailed;

		/// <summary>
		/// 引发 <see cref="PayOrderFailed" /> 事件
		/// </summary>
		protected virtual void OnPayOrderFailed()
		{
			var handler = PayOrderFailed;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}
		public event EventHandler DirectResignSuccess;

		/// <summary>
		/// 引发 <see cref="DirectResignSuccess" /> 事件
		/// </summary>
		protected virtual void OnDirectResignSuccess()
		{
			var handler = DirectResignSuccess;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}
		public event EventHandler DirectResignFailed;

		/// <summary>
		/// 引发 <see cref="DirectResignFailed" /> 事件
		/// </summary>
		protected virtual void OnDirectResignFailed()
		{
			var handler = DirectResignFailed;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}


		public void GetOrderData(OrderItem order)
		{
			_order = order;
			RunWorker(GetOrderInternal, asyc: false);
		}

		OrderItem _order;

		void GetOrderInternal()
		{
			Session.NetClient.Create<string>(HttpMethod.Post,
											"queryOrder/queryMyOrderNoComplete",
											"queryOrder/initNoComplete",
											new
											{
												_json_att = Session.Attributes ?? ""
											})
					.Send();

			FormData = null;
			ParamData = null;

			CompleteAction = OnPayOrderSuccess;
			FailedAction = OnPayOrderFailed;

			var orderid = _order.SequenceNo;
			var task = Session.NetClient.Create<string>(HttpMethod.Post,
														"queryOrder/continuePayNoCompleteMyOrder",
														"queryOrder/initNoComplete",
														new
														{
															sequence_no = orderid,
															pay_flag = _order.pay_resign_flag == "Y" ? "resign" : "pay",
															_json_att = Session.Attributes ?? ""
														}
				).Send();
			if (task == null || !task.IsValid())
			{
				Error = "无效的服务器响应";
				return;
			}

			try
			{
				var checkOrderData = JsonConvert.DeserializeAnonymousType(task.Result, new
																						{
																							data = new
																									{
																										existError = "N",
																										errorMsg = ""
																									},
																							status = false,
																							messages = new string[0]
																						});
				if (!checkOrderData.status || checkOrderData.data == null)
				{
					Error = "服务器返回操作失败: " + checkOrderData.messages.JoinAsString("").DefaultForEmpty(task.Result);
					return;
				}
				if (checkOrderData.data.existError != "N")
				{
					Error = "无法支付订单：" + checkOrderData.data.errorMsg;
					return;
				}
			}
			catch (Exception ex)
			{
				Error = "无法解析服务器响应: " + task.Result;
				return;
			}

			//进入支付页面
			var orderPage = Session.NetClient.Create<string>(HttpMethod.Post, "payOrder/init", "queryOrder/initNoComplete", new { _json_att = Session.Attributes ?? "" }).Send();
			if (!orderPage.IsValid())
			{
				Error = "无效的服务器响应";
				return;
			}

			ParamData = Regex.Matches(orderPage.Result, @"var\s*(interfaceName|InterfaceVersion|tranData|appId|transType|merSignMsg|tour_flag|batch_no|pay_mode|sequence_no|epayurl|ticketTitleForm|passangerTicketList|fcPassangerTicketList|fcTicketTitleForm|oldTicketDTOJson|orderRequestDTOJson|parOrderDTOJson)\s*=\s*['""]?([^;]+)['""]?;", RegexOptions.IgnoreCase | RegexOptions.Singleline)
				.Cast<Match>().Where(s => s.Success)
				.ToDictionary(s => s.Groups[1].Value, s => s.Groups[2].Value.Trim(new[] { '"', '\'', ' ' }).DecodeFromJsExpression());
			var flag = ParamData.GetValue("tour_flag");
			var payMode = ParamData.GetValue("pay_mode");

			if ((payMode == "N" || payMode == "T") && flag == "gc")
			{
				CompleteAction = OnDirectResignSuccess;
				FailedAction = OnDirectResignFailed;

				//无需付款，直接改签
				DirectResign();
				return;
			}

			//支付检查
			var checkResult = Session.NetClient.Create(HttpMethod.Post, "payOrder/paycheck", "payOrder/init", new { _json_att = Session.Attributes ?? "" }, new
																						{
																							data = new
																									{
																										flag = false,
																										payForm = new Dictionary<string, string>()
																									}

																						}).Send();
			if (!checkResult.IsValid())
			{
				Error = "无效的服务器响应";
				return;
			}
			if (checkResult.Result.data == null || !checkResult.Result.data.flag)
			{
				Error = "服务器拒绝了支付请求，请使用浏览器支付或重试。";
				return;
			}

			//解析form
			FormData = checkResult.Result.data.payForm;


			//生成表单数据
			//var ticketForm = new[] { "interfaceName", "InterfaceVersion", "tranData", "appId", "transType", "merSignMsg" };
			//FormData = ParamData.Where(s => ticketForm.Contains(s.Key)).ToDictionary(s => s.Key, s => s.Value);
		}

		public bool DirectResignCancelled { get; private set; }

		void DirectResign()
		{
			var confirmEventArg = new GeneralEventArgs<bool>(false);

			DirectResignCancelled = false;
			OnRequireDirectResign(confirmEventArg);
			if (!confirmEventArg.Data)
			{
				DirectResignCancelled = true;
				return;
			}

			//确认直接改签
			var resignResult = Session.NetClient.Create<string>(HttpMethod.Post, "pay/payConfirm" + ParamData.GetValue("pay_mode"), "payOrder/init", new
																													{
																														parOrderDTOJson = ParamData.GetValue("parOrderDTOJson"),
																														oldTicketDTOJson = ParamData.GetValue("oldTicketDTOJson"),
																														sequence_no = ParamData.GetValue("sequence_no"),
																														batch_no = ParamData.GetValue("batch_no"),
																														_json_att = Session.Attributes ?? ""
																													}).Send();
			if (!resignResult.IsValid())
			{
				Error = "无效的服务器响应";
				return;
			}

			try
			{
				var resignData = JsonConvert.DeserializeAnonymousType(resignResult.Result, new
																							{
																								status = true,
																								data = new
																										{
																											resignStatus = false,
																											errorMessage = ""
																										},
																								messages = new string[0]
																							});
				if (!resignData.status || resignData.data == null)
				{
					Error = "服务器返回操作失败: " + resignData.messages.JoinAsString("").DefaultForEmpty(resignResult.Result);
					return;
				}
				if (!resignData.data.resignStatus)
				{
					Error = "无法完成改签: " + resignData.data.errorMessage.DefaultForEmpty("未知错误");
					return;
				}
			}
			catch (Exception)
			{
				Error = "无法解析服务器响应: " + resignResult.Result;
				return;
			}
		}
	}
}
