namespace TOBA.Order
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics.Eventing.Reader;
	using System.Linq;
	using System.Net;
	using System.Threading;
	using System.Threading.Tasks;
	using Configuration;

	using FSLib.Network.Http;

	using Order.Web;

	using Otn;

	using TOBA.Entity;

	internal class QueueOrderWorker
	{
		/// <summary>
		/// 创建 <see cref="QueueOrderWorker" />  的新实例(QueueOrderWorker)
		/// </summary>
		public QueueOrderWorker(Session session)
		{
			Session = session;
		}

		public Session Session { get; set; }

		public string TourFlag { get; set; }

		public string RepeatSubmitToken { get; set; }

		public int WaitTime { get; set; }

		public int WaitCount { get; set; }

		public string OrderID { get; set; }

		public object Error { get; set; }

		/// <summary>
		/// 获得或设置是否请求停止
		/// </summary>
		public bool RequestCancel { get; set; }

		/// <summary>
		/// 查询成功
		/// </summary>
		public event EventHandler QueryOrderQueueSuccess;

		AsyncOperation _operation;

		/// <summary>
		/// 引发 <see cref="QueryOrderQueueSuccess" /> 事件
		/// </summary>
		protected virtual void OnQueryOrderQueueSuccess()
		{
			_operation = null;
			if (RequestCancel)
				return;
			var handler = QueryOrderQueueSuccess;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 查询失败
		/// </summary>
		public event EventHandler QueryOrderQueueFailed;

		/// <summary>
		/// 引发 <see cref="QueryOrderQueueFailed" /> 事件
		/// </summary>
		protected virtual void OnQueryOrderQueueFailed()
		{
			_operation = null;
			if (RequestCancel)
				return;
			var handler = QueryOrderQueueFailed;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 获得队列信息
		/// </summary>
		public string QueueMessage { get; private set; }

		/// <summary>
		/// 获得或设置当前是否遇到了慢速队列警告
		/// </summary>
		public bool SlowQueueWarning { get; set; }

		public event EventHandler QueueMessageChanged;

		/// <summary>
		/// 引发 <see cref="QueueMessageChanged" /> 事件
		/// </summary>
		protected virtual void OnQueueMessageChanged()
		{
			if (RequestCancel)
				return;

			var handler = QueueMessageChanged;
			if (handler != null)
			{
				if (_operation != null)
					_operation.Post(_ => handler(this, EventArgs.Empty), null);
				else handler(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// 检查队列
		/// </summary>
		public void RunQueryOrderQueue()
		{
			if (_operation != null)
				return;

			_operation = AsyncOperationManager.CreateOperation(null);
			ThreadPool.QueueUserWorkItem(_ => RunQueryOrderQueueInternal(), null);
		}

		void RunQueryOrderQueueInternal()
		{
			var n = Session.NetClient;
			var lastCount = 0;

#if DEBUG

			//
			if (TestFlag.MockQueueFailed)
			{
				_operation.PostOperationCompleted(_ => OnQueryOrderQueueFailed(), null);
				return;
			}

			if (TestFlag.MockQueueSuccess)
			{
				OrderID = "test";
				_operation.PostOperationCompleted(_ => OnQueryOrderQueueSuccess(), null);
				return;
			}

#endif

			do
			{
				var task = n.Create<QueueOrderResponse>(HttpMethod.Get,
														"confirmPassenger/queryOrderWaitTime?random=" + DateTime.Now.ToJsTicks() + "&tourFlag=" + TourFlag + "&_json_att=" + (Session.Attributes ?? "") + (string.IsNullOrEmpty(RepeatSubmitToken) ? "" : "&REPEAT_SUBMIT_TOKEN=" + RepeatSubmitToken),
														"leftTicket/init")
														.Timeout(10000)
														.ReadWriteTimeout(10000)
														.Send();

				if (!task.IsValid())
				{
					if (task.Response?.Status == HttpStatusCode.OK && task.Response?.ContentLength == 0)
					{
						Error = "当前没有排队信息或排队已取消";
						WaitTime = -4;
					}
					else if (task.IsRedirection || task.Exception?.GetType() == typeof(ForceLogoutException))
					{
						Error = "您已被踢，请重新登录查看排队信息";
						break;
					}
					else
					{
						WaitTime = -99;
						Error = "网络错误";
					}
				}
				else
				{
					var err = "检查排队信息时出现错误(" + task.Result.GetErrorMessage() + "), 请酌情重试，并留意是否已经成功";

					if (!task.Result.Status || task.Result.Data == null)
					{
						if (err.IndexOf("系统忙") != -1)
						{
							//检测是否没有排队订单
							var verifyCtx = n.Create(HttpMethod.Post, "queryOrder/queryMyOrderNoComplete", "queryOrder/initNoComplete", result: new { data = new { to_page = "" } });
							verifyCtx.Send();
							if (verifyCtx.IsValid() && verifyCtx.Result.data?.to_page != "cache")
							{
								//排队已经取消
								Error = "当前没有排队订单或排队已取消";
								_operation.Post(_ => Session.SetQueueOrderCancelled(), null);
							}
						}
						else
						{
							Error = err;
						}
						_operation.PostOperationCompleted(_ => OnQueryOrderQueueFailed(), null);
						return;
					}

					if (!task.Result.Data.queryOrderWaitTimeStatus)
					{
						Error = "可能需要重新登录(" + task.Result.GetErrorMessage() + ")";
						_operation.PostOperationCompleted(_ => OnQueryOrderQueueFailed(), null);
						return;
					}

					WaitTime = task.Result.Data.waitTime;
					QueueMessage = task.Result.Data.msg;
					WaitCount = task.Result.Data.waitCount;
				}

				if (WaitTime == -1)
				{
					OrderID = task.Result.Data.orderId;
					Statistics.Current.SubmitSuccessCount++;
					QueueMessage = "订票成功！订单号为 " + OrderID;
				}
				else if (WaitTime == -4)
				{
					Error = "排队已取消";
				}
				else if (WaitTime == -3)
				{
					Error = "订单被撤销了。。。习惯了。。重新下呗。。。";
				}
				else if (WaitTime == -2)
				{
					Error = "占座失败：" + QueueMessage;
				}
				else if (WaitTime == -100)
				{
					QueueMessage = $"等待中...({WaitTime})";
				}
				else if (WaitTime < 0)
				{
					QueueMessage = $"等待状态信息...({WaitTime})";
					Error = "未知排队信息：" + QueueMessage;
				}
				else if (WaitTime > 0)
				{
					if (WaitCount >= 2000)
					{
						SlowQueueWarning = true;
						QueueMessage = "2K人排队…预计 " + Utility.ShowSecondInfo(WaitTime) + "...传说中的慢速队列？？建议换账号重新刷票！等待过久建议取消！请刷票速度慢一点！";
					}
					else
					{
						var diff = lastCount > 0 ? WaitCount - lastCount : 0;
						var desc = "";
						if (diff < 0)
							desc = $"比上次还多了{diff}...阿西吧...";
						else if (diff == 0)
							desc = "和上次一样, 服务器干嘛呢...";
						else if (diff < 30)
							desc = "比上次少 " + diff + "，紧张紧张";
						else
							desc = "比上次少 " + diff + "，可能没票";

						QueueMessage = $"排队 {WaitCount:N0} 预计 " + Utility.ShowSecondInfo(WaitTime) + "..." + desc;
						lastCount = diff;
					}
				}
				OnQueueMessageChanged();

				if (WaitTime > -1 || WaitTime == -99 || WaitTime == -100)
					Thread.Sleep(1000);
			} while ((WaitTime > -1 || WaitTime == -99 || WaitTime == -100) && !RequestCancel);

			if (RequestCancel)
			{
				Error = "已取消";
				return;
			}

			if (Error == null)
				Error = "未知错误";

			if (WaitTime == -4)
			{
				_operation.Post(_ => Session.SetQueueOrderCancelled(), null);
			}
			if (WaitTime == -1)
				_operation.PostOperationCompleted(_ => OnQueryOrderQueueSuccess(), null);
			else
			{
				_operation.PostOperationCompleted(_ => OnQueryOrderQueueFailed(), null);
			}
		}

		/// <summary>
		/// 取消排队订单
		/// </summary>
		/// <returns></returns>
		public async Task<(CancelQueueStatus status, string msg)> CancelQueueOrderAsync()
		{
			var client = Session.NetClient;

			var ctx = await client.RunRequestLoopAsync(_ => client.Create(
				HttpMethod.Post,
				"queryOrder/cancelQueueNoCompleteMyOrder",
				"queryOrder/initNoCompleteQueue",
				new { tourFlag = TourFlag },
				new { data = new { existError = "" }, messages = new List<string>() }
			)).ConfigureAwait(true);

			if (!ctx.IsValid())
			{
				if (ctx.IsForceLogout())
					return (CancelQueueStatus.ForceLogout, "已被取消登录");
				else return (CancelQueueStatus.NetworkError, "网络错误");
			}

			if (ctx.IsValid() && ctx.Response.Status == HttpStatusCode.OK && ctx.Result?.data?.existError == "N")
				return (CancelQueueStatus.Success, null);
			else return (CancelQueueStatus.Failed, ctx.Result.messages?.JoinAsString("") ?? "未知错误");
		}
	}

	internal enum CancelQueueStatus
	{
		NetworkError,
		ForceLogout,
		Success,
		Failed
	}
}

