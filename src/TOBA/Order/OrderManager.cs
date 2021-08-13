namespace TOBA.Order
{
	using Entity;

	using FSLib.Network.Http;

	using Newtonsoft.Json;

	using Otn;

	using System;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.ComponentModel;
	using System.Linq;
	using System.Threading;

	using TOBA.Entity;

	using Web;

	internal class OrderManager : IOperation
	{
		#region Implementation of IOperation

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session { get; set; }

		#endregion

		/// <summary>
		/// 获得查询到的订单
		/// </summary>
		public List<OrderItem> Orders { get; private set; }

		/// <summary>
		/// 获得查询到的未完成订单
		/// </summary>
		public List<OrderItem> NotCompleteOrders { get; private set; }

		/// <summary>
		/// 排队中订单
		/// </summary>
		public OrderCacheItem QueueOrder { get; private set; }

		/// <summary>
		/// 获得查询中遇到的错误
		/// </summary>
		public object Error { get; private set; }

		/// <summary>
		/// 加载订单完成
		/// </summary>
		public event EventHandler LoadOrderComplete;

		/// <summary>
		/// 获得或设置用于提交的TOKEN
		/// </summary>
		public string Token { get; set; }

		/// <summary>
		/// 引发 <see cref="LoadOrderComplete" /> 事件
		/// </summary>
		protected virtual void OnLoadOrderComplete()
		{
			IsBusy = false;
			var handler = LoadOrderComplete;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 加载订单完成
		/// </summary>
		public event EventHandler LoadOrderFailed;

		/// <summary>
		/// 引发 <see cref="LoadOrderFailed" /> 事件
		/// </summary>
		protected virtual void OnLoadOrderFailed()
		{
			IsBusy = false;
			var handler = LoadOrderFailed;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 加载订单完成
		/// </summary>
		public event EventHandler LoadNotCompleteOrderComplete;

		/// <summary>
		/// 引发 <see cref="LoadNotCompleteOrderComplete" /> 事件
		/// </summary>
		protected virtual void OnLoadNotCompleteOrderComplete()
		{
			IsBusy = false;
			var handler = LoadNotCompleteOrderComplete;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 加载订单完成
		/// </summary>
		public event EventHandler LoadNotCompleteOrderFailed;

		/// <summary>
		/// 引发 <see cref="LoadNotCompleteOrderFailed" /> 事件
		/// </summary>
		protected virtual void OnLoadNotCompleteOrderFailed()
		{
			IsBusy = false;
			var handler = LoadNotCompleteOrderFailed;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 当加载订单的进度发生变化时触发
		/// </summary>
		public event EventHandler LoadProgressChanged;

		/// <summary>
		/// 引发 <see cref="LoadProgressChanged" /> 事件
		/// </summary>
		protected virtual void OnLoadProgressChanged()
		{
			var handler = LoadProgressChanged;
			if (handler != null)
			{
				if (_operation != null)
					_operation.Post(_ => handler(this, EventArgs.Empty), null);
				else handler(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// 获得当前已加载订单数
		/// </summary>
		public int CurrentLoadedOrders { get; private set; }

		/// <summary>
		/// 获得需要加载的订单数
		/// </summary>
		public int TotalOrdersToLoad { get; private set; }

		AsyncOperation _operation;

		/// <summary>
		/// 获得当前是否正在忙碌
		/// </summary>
		public bool IsBusy
		{
			get { return _operation != null; }
			protected set
			{
				if (!value)
					_operation = null;
			}
		}

		public void QueryNotComplete()
		{
			if (IsBusy)
				return;

			_operation = AsyncOperationManager.CreateOperation(null);
			ThreadPool.QueueUserWorkItem(_ => QueryNotCompleteInternal(), null);
		}

		internal void QueryNotCompleteInternal()
		{
			var task = Session.NetClient.RunRequestLoop(_ => Session.NetClient.Create<string>(
				HttpMethod.Post,
				"queryOrder/queryMyOrderNoComplete",
				"queryOrder/initNoComplete",
				new
				{
					_json_att = Session.Attributes ?? ""
				}));
			if (!task.IsValid())
			{
				Error = task.Exception?.Message ?? "网络错误";
				_operation?.PostOperationCompleted(_ => OnLoadNotCompleteOrderFailed(), null);
				return;
			}

			try
			{
				QueueOrder = null;
				var obj = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(task.Result, new
				{
					data = new
					{
						orderDBList = new OrderItem[0],
						orderCacheDTO = new OrderCacheItem(),
						to_page = ""
					},
					messages = new List<string>()
				});
				if (obj.data != null)
				{
					if (obj.data.to_page == "db")
						NotCompleteOrders = obj.data.orderDBList.ToList();
					else if (obj.data.to_page == "cache")
					{
						NotCompleteOrders = new List<OrderItem>();
						QueueOrder = obj.data.orderCacheDTO;
					}
				}
				else
				{
					NotCompleteOrders = new List<OrderItem>();
					Error = obj.messages?.JoinAsString() ?? "未知错误";
				}
			}
			catch (Exception ex)
			{
				Error = ex.Message;
				_operation?.PostOperationCompleted(_ => OnLoadNotCompleteOrderFailed(), null);
				return;
			}
			foreach (var notCompleteOrder in NotCompleteOrders)
			{
				Array.ForEach(notCompleteOrder.tickets, s => s.Passenger.Type = s.ticket_type_code.ToInt32());
			}

			_operation?.PostOperationCompleted(_ => OnLoadNotCompleteOrderComplete(), null);
		}

		private DateTime _fromDate, _toDate;

		public void QueryOrders(string type, DateTime fromDate, DateTime toDate, string orderNo, string trainCode, string passenger)
		{
			if (IsBusy)
				return;
			_fromDate = fromDate;
			_toDate = toDate;

			_operation = AsyncOperationManager.CreateOperation(null);
			ThreadPool.QueueUserWorkItem(_ => QueryOrdersInternal(type, fromDate, toDate, orderNo, trainCode, passenger), null);
		}

		void QueryOrdersInternal(string type, DateTime fromDate, DateTime toDate, string orderNo, string trainCode, string passenger)
		{
			Orders = new List<OrderItem>();

			OnLoadProgressChanged();
			foreach (var subtype in new string[] { "H", "G" })
			{
				var pageIndex = (int?)null;
				var pageSize = 8;
				var totalPage = 1;
				var baseCount = Orders.Count;

				var endDate = toDate;
				if (subtype == "H" && endDate >= DateTime.Now.Date)
				{
					endDate = DateTime.Now.Date.AddDays(-1);
				}

				do
				{
					var task = Session.NetClient.RunRequestLoop(_ => Session.NetClient.Create<QueryMyOrderResponse>(
						HttpMethod.Post,
						"queryOrder/queryMyOrder",
						"queryOrder/init",
						new Dictionary<string, string>()
						{
							{"queryType", type},
							{"queryStartDate", fromDate.ToString("yyyy-MM-dd")},
							{"queryEndDate", endDate.ToString("yyyy-MM-dd")},
							{"come_from_flag", "my_order"},
							{"pageSize", pageSize.ToString()},
							{"pageIndex", pageIndex.ToString()},
							{"query_where", subtype},
							{"sequeue_train_name", trainCode}
						}
					));
					if (task == null || !task.IsSuccess || task.Result == null || !task.Result.Status || task.Result.Data == null)
					{
						Error = task.Result?.GetErrorMessage(null) ?? task.Exception?.Message ?? "网络错误";
						_operation.PostOperationCompleted(_ => OnLoadOrderFailed(), null);
						return;
					}

					//分析订单
					task.Result.Data.List.ForEach(s =>
					{
						s.OriginalPage = pageIndex ?? 0;
						s.OriginalSubType = subtype;
					});
					Orders.AddRange(task.Result.Data.List);
					TotalOrdersToLoad = baseCount + task.Result.Data.OrderTotalNumber.ToInt32();
					CurrentLoadedOrders = Orders.Count;
					pageIndex = (pageIndex ?? 0) + 1;
					OnLoadProgressChanged();
				} while (Orders.Count < TotalOrdersToLoad);

			}

			foreach (var order in Orders)
			{
				Array.ForEach(order.tickets, s => s.Passenger.Type = s.ticket_type_code.ToInt32());
			}
			_operation.PostOperationCompleted(_ => OnLoadOrderComplete(), null);
		}

		/// <summary>
		/// 初始化查询订单，部分操作需要
		/// </summary>
		/// <param name="order"></param>
		void InitQueryOrder(OrderItem order)
		{
			Session.NetClient.RunRequestLoop(_ => Session.NetClient.Create<QueryMyOrderResponse>(
				HttpMethod.Post,
				"queryOrder/queryMyOrder",
				"queryOrder/init",
				new Dictionary<string, string>()
				{
					{"queryType", "1"},
					//{"queryStartDate", order.order_date.ToString("yyyy-MM-dd")},
					//{"queryEndDate", order.order_date.ToString("yyyy-MM-dd")},
					{"queryStartDate", _fromDate.ToString("yyyy-MM-dd")},
					{"queryEndDate", _toDate.ToString("yyyy-MM-dd")},
					//{"come_from_flag", changeTs ? "my_cs_resign" : "my_resign"},
					{"come_from_flag", "my_order"},
					{"pageSize", "8"},
					{"pageIndex", (order.OriginalPage).ToString()},
					{"query_where", "G"},
					{"sequeue_train_name", ""}
				}
			));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="tickets"></param>
		/// <param name="changeTs">是否变更到站</param>
		public QueryParam Resign(OrderItem order, OrderTicket[] tickets, bool changeTs, out string message)
		{
			message = null;

			InitQueryOrder(order);

			var url = "queryOrder/resginTicket";
			var keys = tickets.Select(s => s.sequence_no + "," + s.batch_no + "," + s.coach_no + "," + s.seat_no + "," + s.start_train_date_page.ToString("yyyy-MM-dd HH:mm")).JoinAsString("#") + "#";
			var data = new
			{
				ticketkey = keys,
				sequenceNo = tickets[0].sequence_no,
				changeTSFlag = changeTs ? "Y" : "N",
				_json_att = Session.Attributes ?? ""
			};
			var refer = "queryOrder/init";

			//验证等待请求
			var task = Session.NetClient.RunRequestLoop(_ => Session.NetClient.Create<string>(HttpMethod.Post, url, refer, data));
			if (task == null || !task.IsSuccess)
			{
				message = "网络错误";
				return null;
			}

			var resignData = new
			{
				status = true,
				data = new
				{
					existError = "",
					errorMsg = ""
				}
			};
			try
			{
				resignData = JsonConvert.DeserializeAnonymousType(task.Result, resignData);
			}
			catch (Exception)
			{
				message = "数据不正确";
				return null;
			}

			if (resignData == null || !resignData.status || resignData.data == null || resignData.data.existError != "N")
			{
				message = resignData.data.SelectValue(s => s.errorMsg).DefaultForEmpty("无效回答，请尝试重新登录。");
				return null;
			}

			var qp = new QueryParam();
			//qp.QueryPageUrl = task.Request.Uri.PathAndQuery.Remove(0, 8);
			qp.FromStationCode = tickets[0].stationTrainDTO.from_station_telecode;
			qp.FromStationName = tickets[0].stationTrainDTO.from_station_name;
			qp.ToStationCode = tickets[0].stationTrainDTO.to_station_telecode;
			qp.ToStationName = tickets[0].stationTrainDTO.to_station_name;
			qp.DepartureDate = tickets[0].train_date.Date;
			qp.QueryStudentTicket = tickets.All(s => s.ticket_type_code == "3");
			qp.ResignChangeTs = changeTs;

			if (qp.FromStationCode.IsNullOrEmpty() || qp.ToStationCode.IsNullOrEmpty())
				return null;

			//添加乘客
			qp.EnableAutoPreSubmit = true;
			qp.AutoPreSubmitConfig.Passenger.AddRange(tickets.Select(s => (PassengerInTicket)s.Passenger));

			qp.IsLoaded = true;
			qp.Resign = true;
			qp.ResignDate = tickets[0].train_date;

			return qp;
		}



		#region 退票

		AsyncOperation _refundOperation;

		/// <summary>
		/// 初始化退票完成
		/// </summary>
		public event EventHandler InitRefundTicketComplete;

		/// <summary>
		/// 引发 <see cref="InitRefundTicketComplete" /> 事件
		/// </summary>
		protected virtual void OnInitRefundTicketComplete()
		{
			_refundOperation = null;
			var handler = InitRefundTicketComplete;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 退票手续费
		/// </summary>
		public decimal RefundCost { get; private set; }

		/// <summary>
		/// 退款金额
		/// </summary>
		public decimal RefundFee { get; private set; }

		/// <summary>
		/// 获得是否退票成功
		/// </summary>
		public bool RefundSuccess { get; private set; }

		/// <summary>
		/// 初始化退票数据
		/// </summary>
		/// <param name="ticket"></param>
		public void InitRefundTicket(OrderItem order, OrderTicket ticket)
		{
			if (_refundOperation != null) return;
			_refundOperation = AsyncOperationManager.CreateOperation(null);
			RefundSuccess = false;
			Error = null;

			ThreadPool.QueueUserWorkItem(_ =>
			{
				InitQueryOrder(order);

				var data = new
				{
					sequence_no = ticket.sequence_no,
					batch_no = ticket.batch_no,
					coach_no = ticket.coach_no,
					seat_no = ticket.seat_no,
					start_train_date_page = ticket.start_train_date_page.ToString("yyyy-MM-dd HH:mm"),
					train_code = ticket.stationTrainDTO.station_train_code,
					coach_name = ticket.coach_name,
					seat_name = ticket.seat_name,
					seat_type_name = ticket.seat_type_name,
					train_date = ticket.train_date.ToString("yyyy-MM-dd 00:00:00"),
					from_station_name = ticket.stationTrainDTO.from_station_name,
					to_station_name = ticket.stationTrainDTO.to_station_name,
					start_time = ticket.stationTrainDTO.start_time.ToString("yyyy-MM-dd HH:mm:ss"),
					passenger_name = ticket.Passenger.Name,
					from_station_telecode = ticket.stationTrainDTO.from_station_telecode,
					to_station_telecode = ticket.stationTrainDTO.to_station_telecode,
					train_no = ticket.stationTrainDTO.TrainDto.TrainNo,
					id_type = ticket.Passenger.IdTypeCode,
					id_no = ticket.Passenger.IdNo,
					_json_att = Session.Attributes ?? ""
				};

				var txt = Session.NetClient.Create<string>(HttpMethod.Post, "queryOrder/returnTicketAffirm", "queryOrder/init", data).Send();
				if (!txt.IsSuccess())
				{
					//失败
					Error = "网络错误";
					_refundOperation.PostOperationCompleted(a => OnInitRefundTicketComplete(), null);
					return;
				}
				var costdata = new
				{
					status = false,
					data = new
					{
						errMsg = "",
						submitStatus = (bool?)null,
						ticket_price = 0m,
						return_price = 0m,
						return_cost = 0m
					}
				};
				try
				{
					costdata = JsonConvert.DeserializeAnonymousType(txt.Result, costdata);
				}
				catch (Exception ex)
				{
					Error = "数据错误";
					_refundOperation.PostOperationCompleted(a => OnInitRefundTicketComplete(), null);
					return;
				}

				if (costdata == null || !costdata.status || costdata.data == null)
				{
					Error = "提交失败";
					RefundSuccess = false;
					_refundOperation.PostOperationCompleted(a => OnInitRefundTicketComplete(), null);
					return;
				}
				if (costdata.data.submitStatus == false)
				{
					Error = costdata.data.errMsg;
					RefundSuccess = false;
					_refundOperation.PostOperationCompleted(a => OnInitRefundTicketComplete(), null);
					return;
				}
				RefundFee = costdata.data.return_price;
				RefundCost = costdata.data.return_cost;
				RefundSuccess = true;
				_refundOperation.PostOperationCompleted(a => OnInitRefundTicketComplete(), null);
			}, null);
		}

		/// <summary>
		/// 退票成功
		/// </summary>
		public event EventHandler RefundTicketFinished;

		/// <summary>
		/// 引发 <see cref="RefundTicketFinished" /> 事件
		/// </summary>
		protected virtual void OnRefundTicketFinished()
		{
			_refundOperation = null;

			var handler = RefundTicketFinished;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 执行退票
		/// </summary>
		public void RunRefundTicket()
		{
			if (_refundOperation != null) return;
			_refundOperation = AsyncOperationManager.CreateOperation(null);
			RefundSuccess = false;

			ThreadPool.QueueUserWorkItem(_ =>
			{
				var task = Session.NetClient.Create(HttpMethod.Post, "queryOrder/returnTicket", "queryOrder/init", result: new
				{
					data = new { errmes = new List<string>() }
				}).Send();
				if (task.IsValid())
				{
					var errmes = task.Result.data?.errmes;
					RefundSuccess = errmes == null || errmes.Count == 0;
				}
				_refundOperation.PostOperationCompleted(x => OnRefundTicketFinished(), null);
			});
		}

		#endregion

		#region 取消未完成，设计为同步方法

		/// <summary>
		/// 取消未完成订单
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool CancelNotComplete(string id, string flag)
		{
			Session.NetClient.RunRequestLoop(_ => Session.NetClient.Create<string>(HttpMethod.Post, "queryOrder/queryMyOrderNoComplete", "queryOrder/initNoComplete", new
			{
				_json_att = Session.Attributes ?? ""
			}));

			var task = Session.NetClient.RunRequestLoop(
				_ => Session.NetClient.Create<string>(HttpMethod.Post,
					"queryOrder/cancelNoCompleteMyOrder",
					"queryOrder/initNoComplete",
					new NameValueCollection()
					{
						{"sequence_no", id},
						{"cancel_flag", flag},
						{"_json_att", Session.Attributes ?? ""}
					})
			);
			if (task == null || !task.IsValid())
				return false;

			try
			{
				var obj = JsonConvert.DeserializeAnonymousType(task.Result, new
				{
					status = true,
					data = new
					{
						existError = "Y"
					}
				});
				return obj != null && obj.status && obj.data.existError != "Y";
			}
			catch (Exception)
			{
				return false;
			}
		}

		#endregion
	}
}
