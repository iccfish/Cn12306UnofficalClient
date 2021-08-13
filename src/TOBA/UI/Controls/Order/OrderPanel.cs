using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Order
{
	using Autofac;

	using Configuration;

	using DevComponents.DotNetBar;

	using Dialogs.Common;

	using Entity;

	using TOBA.Order;
	using TOBA.Order.Entity;
	using TOBA.UI.Servicing.Order;

	internal partial class OrderPanel : ControlBase
	{
		OrderManager _orderManager;
		public OrderPanel()
		{
			InitializeComponent();
			//屏蔽改签
			//tsResign.Visible = false;

			if (!Program.IsRunning)
				return;

			Load += OrderPanel_Load;
		}

		#region Overrides of ControlBase

		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public override void InitSession(Session session)
		{
			base.InitSession(session);

			_orderManager = new OrderManager()
			{
				Session = Session
			};
			Session.OrderChanged += (s, e) => LoadOrders();
			SetToolbarStipStatus();
			InitReload();
			InitResign();
			InitRefund();
			InitUI();
			InitCancel();
			InitPay();

			tsCleanLocalHistory.Click += (s, e) =>
			{
				if (TaskDialog.Show("确定", eTaskDialogIcon.Delete, "确定要删除本地订单历史记录吗？", "这些订单是由订票助手为您在本地保存的记录，12306上可能已经无法查询。如果您选择清空，那么您可能会再也无法见到它们。", eTaskDialogButton.Yes | eTaskDialogButton.No, eTaskDialogBackgroundColor.Red) != eTaskDialogResult.Yes)
					return;
				Session.UserProfile.OrderArchive.ClearArchive();

				tsReload.PerformClick();
			};
		}

		#endregion

		void OrderPanel_Load(object sender, EventArgs ee)
		{
			tsCleanLocalHistory.Enabled = OrderConfiguration.Instance.EnableOrderArchive;

			OrderConfiguration.Instance.PropertyChanged += Instance_PropertyChanged;
			Disposed += (s, e) =>
			{
				OrderConfiguration.Instance.PropertyChanged -= Instance_PropertyChanged;
			};
		}

		private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(OrderConfiguration.EnableOrderArchive))
				tsCleanLocalHistory.Enabled = OrderConfiguration.Instance.EnableOrderArchive;
		}

		#region UI处理

		void InitUI()
		{
			olist.ItemChecked += (s, e) => SetToolbarStipStatus();
			SetToolbarStipStatus();
		}

		void SetToolbarStipStatus()
		{
			var order = olist.SelectedOrder;

			if (order == null)
			{
				tsPay.Enabled = tsResign.Enabled = tsCancelUnpay.Enabled = tsRefund.Enabled = tsResignChangeTs.Enabled = false;
				return;
			}

			tsRefund.Enabled = order.Value.Value.All(s => s.start_train_date_page > DateTime.Now) && order.Value.Value.Any(s => s.return_flag == "Y");
			tsPay.Enabled = tsCancelUnpay.Enabled = order.Value.Value.Any(s => s.OrderStatus == OrderStatus.NotPay || s.OrderStatus == OrderStatus.ResignNotPaid || s.OrderStatus == OrderStatus.ResignChangeTsNotPaid || s.OrderStatus == OrderStatus.Queue);
			tsResign.Enabled = order.Value.Value.All(s => (s.resign_flag == "1" || s.resign_flag == "2") && s.start_train_date_page > DateTime.Now);
			tsResignChangeTs.Enabled = order.Value.Value.All(s => (s.resign_flag == "1" || s.resign_flag == "3") && s.start_train_date_page > DateTime.Now);
		}

		#endregion

		#region 改签

		void InitResign()
		{
			EventHandler ceh = (s, e) =>
			{
				if (Session.UserProfile.QueryParams.HasResignQuery)
				{
					Information("当前正在改签，无法进行其它操作。");
					return;
				}

				var resign = olist.SelectedOrder;
				if (resign == null)
				{
					Information("请勾选要改签的车票啊亲");
					return;
				}

				if (!resign.Value.Value.CanResign())
				{
					Information("无法进行改签，改签的车票必须为已支付状态且行程一致");
					return;
				}
				//测试是否已经有改签
				var currentResign = Session.UserProfile.QueryParams.FirstOrDefault(x => x.Resign);
				if (currentResign != null)
				{
					if (Question("当前正在进行改签【" + currentResign.Name + "】(" + currentResign.DepartureDate.ToString("MM月dd日") + " " + currentResign.FromStationName + " 到 " + currentResign.ToStationName + ")，要继续改签将取消上次改签，是否确认继续？", true))
					{
						currentResign.IsLoaded = false;
					}
					else
					{
						return;
					}
				}

				Resign(s == tsResignChangeTs, resign.Value.Key, resign.Value.Value);
			};

			tsResign.Click += ceh;
			tsResignChangeTs.Click += ceh;
		}

		void Resign(bool changeTs, OrderItem order, OrderTicket[] tickets)
		{
			if (Session.UserProfile.QueryParams.Count > 0 && !Question("您已经打开了其它的查询，如果继续改签，将会自动关闭它们。\n请注意，在改签过程中，无法查询其它车票，否则会导致改签订单无法正确提交！", true))
			{
				return;
			}

			QueryParam qp = null;
			string message = null;

			var dlg = new YetAnotherWaitingDialog();
			dlg.Title = "正在准备改签，请稍等....";
			dlg.WorkCallback = () =>
			{
				qp = _orderManager.Resign(order, tickets, changeTs, out message);
			};
			dlg.ShowDialog();

			if (qp == null)
			{
				Error("暂时无法改签，请检查您的选择并稍后再试。\n\n错误信息：" + message.DefaultForEmpty("未知错误"));
			}
			else
			{
				qp.OriginalOrder = Tuple.Create(order, tickets);
				//移除所有其他查询
				Session.UserProfile.QueryParams.Clear();
				Session.UserProfile.QueryParams.Add(qp);
			}
		}

		#endregion

		#region 重新加载

		void InitReload()
		{
			tsReload.Click += (s, e) =>
			{
				if (Session.UserProfile.QueryParams.HasResignQuery)
				{
					Information("当前正在改签，无法进行其它操作。");
					return;
				}

				LoadOrders();
			};
		}

		public void InitLoad()
		{
			if (olist.Items.Count == 0)
				LoadOrders();
		}

		#endregion

		#region Overrides of Control

		/// <summary>
		/// 引发 <see cref="E:System.Windows.Forms.Control.SizeChanged"/> 事件。
		/// </summary>
		/// <param name="e">包含事件数据的 <see cref="T:System.EventArgs"/>。</param>
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			loading.Location = new Point((Width - loading.Width) / 2, (Height - loading.Height) / 2);
			pEmpty.Location = new Point((Width - pEmpty.Width) / 2, (Height - pEmpty.Height) / 2);
		}

		#endregion

		#region 订单加载

		public void LoadOrders(IEnumerable<OrderItem> orders)
		{
			olist.LoadOrders(orders);
			SetToolbarStipStatus();
		}

		void LoadOrders()
		{
			if (_orderManager.IsBusy || Session == null)
				return;

			pEmpty.Visible = false;
			loading.Visible = true;
			loading.Reset();

			loading.Reset();
			_orderManager.LoadNotCompleteOrderComplete += _orderManager_LoadNotCompleteOrderComplete;
			_orderManager.LoadNotCompleteOrderFailed += _orderManager_LoadNotCompleteOrderFailed;
			loading.LoadingText = "正在查询未完成订单...";

			_orderManager.QueryNotComplete();
		}

		void _orderManager_LoadNotCompleteOrderFailed(object sender, EventArgs e)
		{
			loading.SetLoadingError();
			loading.LoadingText = "未完成订单查询失败: " + _orderManager.Error.ToString();
			_orderManager.LoadNotCompleteOrderComplete -= _orderManager_LoadNotCompleteOrderComplete;
			_orderManager.LoadNotCompleteOrderFailed -= _orderManager_LoadNotCompleteOrderFailed;
		}

		void _orderManager_LoadNotCompleteOrderComplete(object sender, EventArgs e)
		{
			_orderManager.LoadNotCompleteOrderComplete -= _orderManager_LoadNotCompleteOrderComplete;
			_orderManager.LoadNotCompleteOrderFailed -= _orderManager_LoadNotCompleteOrderFailed;
			loading.LoadingText = "正在查询常规订单...";
			_orderManager.LoadOrderComplete += _orderManager_LoadOrderComplete;
			_orderManager.LoadOrderFailed += _orderManager_LoadOrderFailed;
			_orderManager.LoadProgressChanged += _orderManager_LoadProgressChanged;

			if (_orderManager.QueueOrder != null && _orderManager.QueueOrder.status == 1)
			{
				Session.ServiceContainer.Resolve<IOrderQueueUiProvider>().Show(_orderManager.QueueOrder);
			}

			_orderManager.QueryOrders("1", DateTime.Now.Date.AddDays(-62), DateTime.Now.Date, "", "", "");
		}

		void _orderManager_LoadProgressChanged(object sender, EventArgs e)
		{
			if (_orderManager.CurrentLoadedOrders == 0)
				loading.LoadingText = "正在查询第 1 页订单...";
			else
			{
				loading.LoadingText = "共 {0} 个订单，已加载 {1} 个 ...".FormatWith(_orderManager.TotalOrdersToLoad, _orderManager.CurrentLoadedOrders);
			}
		}

		void _orderManager_LoadOrderFailed(object sender, EventArgs e)
		{
			_orderManager.LoadOrderComplete -= _orderManager_LoadOrderComplete;
			_orderManager.LoadOrderFailed -= _orderManager_LoadOrderFailed;
			loading.SetLoadingError();
			loading.LoadingText = "常规订单查询失败: " + _orderManager.Error.ToString();
			//未完成订单显示
			LoadOrders(_orderManager.NotCompleteOrders.ToArray());
		}

		void _orderManager_LoadOrderComplete(object sender, EventArgs e)
		{
			_orderManager.LoadOrderComplete -= _orderManager_LoadOrderComplete;
			_orderManager.LoadOrderFailed -= _orderManager_LoadOrderFailed;

			loading.Hide();

			var orders = _orderManager.Orders ?? new List<OrderItem>();
			if (OrderConfiguration.Instance.EnableOrderArchive && Session.UserProfile.OrderArchive != null)
			{
				Session.UserProfile.OrderArchive.Merge(orders);
				orders = Session.UserProfile.OrderArchive.Archive.ToList();
			}
			if (_orderManager.NotCompleteOrders != null)
				orders = _orderManager.NotCompleteOrders.Union(orders).ToList();

			LoadOrders(orders);
			if (orders.Count == 0)
				pEmpty.Visible = true;
		}

		#endregion

		#region 退票

		Queue<KeyValuePair<OrderItem, OrderTicket>> _refundQueue = new Queue<KeyValuePair<OrderItem, OrderTicket>>();
		OrderItem _currnetRefundOrder = null;
		OrderTicket _currentRefundTicket;
		YetAnotherWaitingDialog _refundWaitingDialog;

		void InitRefund()
		{
			tsRefund.Click += tsRefund_Click;
			_orderManager.InitRefundTicketComplete += (s, e) =>
			{
				var trainCode = _currentRefundTicket.stationTrainDTO.station_train_code;
				var id = _currentRefundTicket.sequence_no;
				var batchNo = _currentRefundTicket.batch_no;

				if (!_orderManager.RefundSuccess)
				{
					Information(string.Format("退票失败： 【{0}】{1} {2}\n错误信息： {3}", id, trainCode, _currentRefundTicket.Passenger.Name, _orderManager.Error?.ToString() ?? "未知错误"));
					BeginRefundTicket();
				}
				else
				{
					_refundWaitingDialog.SetState(ExecutionState.Warning, string.Format("请确认退票 【{0}/{1}】{2} {3}", id, batchNo, trainCode, _currentRefundTicket.Passenger.Name));
					if (Question(string.Format("确认退票 【{0}】{1} {2} ？\n退票手续费 {3}，退回金额 {4}", id, trainCode, _currentRefundTicket.Passenger.Name, _orderManager.RefundCost.ToString("C"), _orderManager.RefundFee.ToString("C")), true))
					{
						_refundWaitingDialog.SetState(ExecutionState.Running, string.Format("正在退票 【{0}】{1} {2}", id, trainCode, _currentRefundTicket.Passenger.Name));
						_orderManager.RunRefundTicket();
					}
					else
					{
						BeginRefundTicket();
					}
				}
			};
			_orderManager.RefundTicketFinished += (s, e) =>
			{
				var trainCode = _currentRefundTicket.stationTrainDTO.station_train_code;
				var id = _currentRefundTicket.sequence_no;
				var batchNo = _currentRefundTicket.batch_no;

				if (_orderManager.RefundSuccess)
				{
					Information(string.Format("退票成功： 【{0}】{1} {2}", id, trainCode, _currentRefundTicket.Passenger.Name));

					Session.OnOrderRefundSuccess(Session, new OrderRefundEventArgs(_currnetRefundOrder, new[] { _currentRefundTicket }, false));
				}
				else
				{
					Information(string.Format("退票失败： 【{0}】{1} {2}", id, trainCode, _currentRefundTicket.Passenger.Name));
				}
				BeginRefundTicket();
			};
		}

		void tsRefund_Click(object sender, EventArgs e)
		{
			var order = olist.SelectedOrder;
			if (order == null)
			{
				Information("请先选择您要退的票");
				return;
			}
			foreach (var ticket in order.Value.Value)
			{
				if (ticket.return_flag != "Y")
				{
					Information("车票 【" + ticket.sequence_no + "】" + ticket.stationTrainDTO.station_train_code + " " + ticket.Passenger.Name + " 不可退");
				}
				else
				{
					_refundQueue.Enqueue(new KeyValuePair<OrderItem, OrderTicket>(order.Value.Key, ticket));
				}
			}
			Trace.TraceInformation("进入退票流程");
			BeginRefundTicket();
		}

		void BeginRefundTicket()
		{
			if (_refundQueue.Count == 0)
			{
				_currnetRefundOrder = null;
				_currentRefundTicket = null;

				if (_refundWaitingDialog != null)
				{
					_refundWaitingDialog.Close();
					_refundWaitingDialog = null;
				}
				LoadOrders();
				return;
			}

			var item = _refundQueue.Dequeue();
			_currentRefundTicket = item.Value;
			_currnetRefundOrder = item.Key;
			//TODO 退票流程
			_orderManager.InitRefundTicket(_currnetRefundOrder, _currentRefundTicket);

			if (_refundWaitingDialog == null)
			{
				_refundWaitingDialog = new YetAnotherWaitingDialog();
			}
			_refundWaitingDialog.SetState(ExecutionState.Running, "正在准备退票 【" + _currentRefundTicket.sequence_no + "】" + _currentRefundTicket.stationTrainDTO.station_train_code + " " + _currentRefundTicket.Passenger.Name);
			if (!_refundWaitingDialog.Visible) _refundWaitingDialog.ShowDialog();
		}

		#endregion

		#region 取消订单

		void InitCancel()
		{
			tsCancelUnpay.Click += (s, e) => CancelUnpay();
		}

		void CancelUnpay()
		{
			var order = olist.SelectedOrder;
			if (order == null || !order.Value.Value.Any(s => s.OrderStatus == OrderStatus.NotPay || s.OrderStatus == OrderStatus.ResignNotPaid || s.OrderStatus == OrderStatus.ResignChangeTsNotPaid))
			{
				Information("请先选择您要取消的车票，车票必须为未支付状态。");
				return;
			}
			if (!Question("确定要取消这个订单吗？超过三次就只能换马甲订票了的 ....", true))
			{
				return;
			}

			//取消订单
			var dlg = new YetAnotherWaitingDialog()
			{
				Text = "正在取消未完成订单，请稍等..."
			};
			dlg.WorkCallbackAdvanced = _ =>
			{
				var result = _orderManager.CancelNotComplete(order.Value.Key.SequenceNo, order.Value.Key.pay_resign_flag == "Y" ? "cancel_resign" : "cancel_order");
				_.SetState(result ? ExecutionState.Ok : ExecutionState.Block, "取消" + (result ? "成功" : "失败") + " ....");
				Thread.Sleep(2000);
				AppContext.HostForm.Invoke(LoadOrders);

				Session.OnOrderRefundSuccess(Session, new OrderRefundEventArgs(order.Value.Key, order.Value.Value, false));
			};
			dlg.ShowDialog();
		}

		#endregion

		#region 支付订单

		void InitPay()
		{
			tsPay.Click += (s, e) => PayOrder();
		}
		void PayOrder()
		{
			var order = olist.SelectedOrder;
			if (order == null || !order.Value.Value.Any(s => s.OrderStatus == OrderStatus.NotPay || s.OrderStatus == OrderStatus.ResignNotPaid || s.OrderStatus == OrderStatus.ResignChangeTsNotPaid))
			{
				Information("请先选择您要支付的车票，车票必须为未支付状态。");
				return;
			}

			var dlg = new Dialogs.Order.PayOrder()
			{
				Session = Session,
				Order = order.Value.Key
			};
			dlg.ShowDialog();
		}


		#endregion
	}
}
