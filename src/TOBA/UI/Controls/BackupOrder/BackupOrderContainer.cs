using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

/*
 * 第一次可见的时候，如果没有进行过加载，则进行加载；
 * 请求刷新的时候，进行加载
 *
 * 如果加载发现排队中订单，则只显示排队中订单；
 * 如果请求刷新时指定了刷新范围，则只显示指定范围内的订单
 *
 */

namespace TOBA.UI.Controls.BackupOrder
{
	using Data;

	using Dialogs.BackupOrder;

	using System.Threading.Tasks;

	using TOBA.BackupOrder;
	using TOBA.BackupOrder.Entity;

	partial class BackupOrderContainer : ControlBase
	{
		public const string ICON_NOT_PAY = "notpay";
		public const string ICON_WAIT = "wait";
		public const string ICON_SUCCEED = "succeed";
		public const string ICON_FAILED = "failed";

		private bool _firstLoad = false;
		private IBackupOrderService _service;

		public BackupOrderContainer()
		{
			InitializeComponent();

			if (!Program.IsRunning)
				return;

			//图片资源
			//1 - 待付款
			imgList.Images.Add(ICON_NOT_PAY, UiUtility.Get24PxImageFrom16PxImg(Properties.Resources.asset));
			//2- 待兑现
			imgList.Images.Add(ICON_WAIT, UiUtility.Get24PxImageFrom16PxImg(Properties.Resources.clock_16));
			//兑现成功
			imgList.Images.Add(ICON_SUCCEED, UiUtility.Get24PxImageFrom16PxImg(Properties.Resources.cou_16_accept));
			//兑现失败
			imgList.Images.Add(ICON_FAILED, UiUtility.Get24PxImageFrom16PxImg(Properties.Resources.cou_16_delete));

			this.Load += BackupOrderContainer_Load;
		}

		/// <inheritdoc />
		public override void InitSession(Session session)
		{
			base.InitSession(session);
			_service = session.GetService<IBackupOrderService>();
			session.HbOrderChanged += (_1, _2) =>
			{
				LoadOrdersAsync(HbOrderLoadScope.NotComplete | HbOrderLoadScope.NotProcessed);
			};
		}

		public void LoadOnDemand()
		{
			if (Visible && !_firstLoad)
			{
#pragma warning disable CS4014
				LoadOrdersAsync(HbOrderLoadScope.All);
#pragma warning restore CS4014
			}
		}

		private void BackupOrderContainer_Load(object sender, EventArgs e)
		{
			//加载动画
			loading.KeepCenter(this);
			loading.Visible = false;

			//工具栏
			orderList.SelectedIndexChanged += (_1, _2) =>
			{
				RefreshToolbar();
			};
			RefreshToolbar();
			//刷新
			btnRefresh.Click += (_1, _2) => LoadOrdersAsync(HbOrderLoadScope.All);

			//支付
			btnPay.Click += BtnPay_Click;
			//取消订单
			btnCancel.Click += BtnCancel_Click;

			//取消订单
			btnCancelQueue.Click += async (_1, _2) =>
			{
				this.ShowInfoToastMini("正在取消排队...");
				var (ok, msg) = await _service.CancelWaitingOrderAsync();
				this.CloseToast();

				if (!ok)
				{
					this.ShowErrorToastMini(msg);
				}
				else
				{
					this.ShowSuccessToastMini("取消排队成功");
				}
			};
		}

		private async void BtnCancel_Click(object sender, EventArgs e)
		{
			var lvitem = orderList.SelectedItems.OfType<ListViewItem>().FirstOrDefault();
			var lvgroup = lvitem?.Group;
			var lvGroupObj = lvgroup?.Tag as HbOrderListViewGroup;

			var notCompleteOrder = lvGroupObj?.NotCompleteOrder;
			var orderItem = lvGroupObj?.Order;

			if (!(notCompleteOrder != null || orderItem?.StatusCode < 5))
			{
				return;
			}

			if (!this.Question("确定要取消选中的订单吗？每个账户每天只能取消五次候补订单哦。", true))
				return;

			//取消订单
			this.ShowInfoToastMini("正在卖力地取消订单，请稍候啊客官...");
			var (ok, msg) = await _service.CancelNotCompleteOrder(notCompleteOrder?.Order ?? orderItem, _ => this.Question($"将会退回 {_:C}，确定继续咩？"));
			this.CloseToast();
			if (ok)
			{
				this.ShowSuccessToastMini("取消订单成功 :-)");
				Session.OnHbOrderChanged();
			}
			else
			{
				this.ShowErrorToastMini("取消订单失败：" + msg);
			}
		}

		private void BtnPay_Click(object sender, EventArgs e)
		{
			if (_unpayOrder == null)
				return;

			using (var dlg = new PayBackupOrderDlg(Session, _unpayOrder))
			{
				dlg.ShowDialog(this.FindForm());
				Session.OnHbOrderChanged();
			}
		}

		async Task LoadOrdersAsync(HbOrderLoadScope scope)
		{
			if (!btnRefresh.Enabled)
				return;

			btnRefresh.Enabled = false;
			loading.Visible = true;
			loading.Reset();
			btnCancelQueue.Visible = false;

			var ok = false;
			var msg = string.Empty;


			//未完成订单
			if (scope.HasFlag(HbOrderLoadScope.NotComplete))
			{
				loading.LoadingText = "正在查询未完成候补订单...";

				(ok, msg, _unpayOrder) = await _service.GetUnpayHbOrderAsync();
				//如果有未完成订单，则提前退出动作并进行排队

				if (ok)
				{
					if (_unpayOrder.IsAsync && _unpayOrder.Status == 0)
					{
						await CompleteOrderQueue();
						await LoadOrdersAsync(scope);
						return;
					}

					if (_unpayOrder.Order == null)
						_unpayOrder = null;
				}
			}

			//待处理订单
			if (scope.HasFlag(HbOrderLoadScope.NotProcessed))
			{
				loading.LoadingText = "正在查询待兑现订单...";
				(ok, msg, NotProcessedOrderItems) = await _service.GetUnprocessedHbOrderItemsAsync();

				if (!ok)
				{
					this.ShowErrorToastMini($"查询未兑现订单错误：{msg}");
				}
			}

			//已处理订单
			if (scope.HasFlag(HbOrderLoadScope.Processed))
			{
				loading.LoadingText = "正在查询已处理订单...";
				(ok, msg, ProcessedOrderItems) = await _service.GetProcessedHbOrderItemsAsync();

				if (!ok)
				{
					this.ShowErrorToastMini($"查询已处理订单错误：{msg}");
				}
			}

			RefreshList();
			RefreshToolbar();

			btnRefresh.Enabled = true;
			loading.Hide();
		}

		/*
		 * 列表显示逻辑：
		 * 待付款-待兑现-已处理订单
		 * 后两类订单合并显示
		 */

		/// <summary>
		/// 刷新订单列表
		/// </summary>
		void RefreshList()
		{
			var orders = (NotProcessedOrderItems ?? new List<BackupOrderItem>()).Concat(ProcessedOrderItems ?? new List<BackupOrderItem>()).ToList();

			orderList.BeginUpdate();
			orderList.SuspendLayout();

			//先移除，再更新
			orderList.Items.Clear();
			orderList.Groups.Clear();

			//添加未完成订单
			if (_unpayOrder != null)
			{
				var g = new HbOrderListViewGroup(_unpayOrder);
				orderList.Groups.Add(g.Group);
				orderList.Items.AddRange(g.Items.ToArray());
			}

			//添加其它订单
			foreach (var order in orders)
			{
				var g = new HbOrderListViewGroup(order);
				orderList.Groups.Add(g.Group);

				var items = g.Items.ToArray();
				orderList.Items.AddRange(items);
			}

			orderList.EndUpdate();
			orderList.ResumeLayout(true);
			orderList.AutoResizeColumns(orderList.Items.Count > 0 ? ColumnHeaderAutoResizeStyle.ColumnContent : ColumnHeaderAutoResizeStyle.HeaderSize);
		}

		/// <summary>
		/// 排队未完成订单
		/// </summary>
		/// <returns></returns>
		async Task CompleteOrderQueue()
		{
			loading.Text = "候补订单正在排队中...";
			btnCancelQueue.Visible = true;

			while (true)
			{
				var (qok, _, data) = await _service.QueryHbQueueAsync();
				if (!qok || data?.Status == 1)
				{
					return;
				}

				loading.SetMessage(
					Color.White,
					$"不厌其烦地排队中，预计还需要 {Utility.ShowSecondInfo(data.WaitTime)} (人数 {data.WaitCount})",
					Properties.Resources.gif_beg7
				);
				await Task.Delay(1000);
			}
		}

		private UnpayBackupOrder _unpayOrder;
		public List<BackupOrderItem> NotProcessedOrderItems { get; set; }

		public List<BackupOrderItem> ProcessedOrderItems { get; set; }

		async void RefreshToolbar()
		{
			var lvitem = orderList.SelectedItems.OfType<ListViewItem>().FirstOrDefault();
			var lvgroup = lvitem?.Group;
			var lvGroupObj = lvgroup?.Tag as HbOrderListViewGroup;

			var notCompleteOrder = lvGroupObj?.NotCompleteOrder;
			var orderItem = lvGroupObj?.Order;
			var need = (lvitem?.Tag is BackupTrain tmp) ? tmp : null;

			//待支付订单
			btnPay.Enabled = notCompleteOrder != null;
			//取消订单
			btnCancel.Enabled = notCompleteOrder != null || orderItem?.StatusCode < 5;

			//提示
			pMemo.Visible = false;
			if (orderList.Items.Count == 0)
			{
				pMemo.Visible = true;
				lblMemo.Text = "<font color='gray'><i>没有查询到候补订单</i></font>";
			}
			else if (notCompleteOrder != null)
			{
				pMemo.Visible = true;
				lblMemo.Text = $"<font color='red'><b>未支付订单，请在 {notCompleteOrder.LoseTime.ToLongTimeString()} 前完成支付</b></font>";
			}
			else if (orderItem?.HasRefundInfo == true)
			{
				pMemo.Visible = true;
				if (orderItem.RefundInfo == null)
				{
					lblMemo.Text = "<font color='gray'>退款信息正在努力地查询中...</font>";

					var (ok, msg) = await _service.QueryRefundInfo(orderItem);
					if (!ok)
					{
						lblMemo.Text = $"<font color='red'>查询退款信息失败：<b>{msg}</b></font>";
					}
				}

				if (orderItem.RefundInfo != null)
				{
					var ri = orderItem.RefundInfo;
					string txt;

					if (ri.TransAmount > 0)
					{
						txt = $"<font color='ForestGreen'>退款金额：</font><font color='Crimson'>{(ri.TransAmount / 100):C}</font><font color='ForestGreen'>，平台流水号：</font><font color='Crimson'>{ri.TransNo}</font><font color='ForestGreen'>，业务流水号：</font><font color='Crimson'>{ri.TradeNo}</font><font color='ForestGreen'>，退款状态：</font>";
						if (ri.TransStatus == 1)
						{
							txt += $"<b><font color='royalblue'>成功</font></b><font color='ForestGreen'>，已成功退至 </font><font color='Crimson'><b>{ParamData.GetBankName(ri.BankCode)}</b></font>";
						}
						else if (ri.TransStatus == 2)
						{
							txt += $"<b><font color='red'>失败</font></b><font color='ForestGreen'>，请与 </font><font color='Crimson'><b>{ParamData.GetBankName(ri.BankCode)} </b></font><font color='ForestGreen'>联系</font>";
						}
						else
						{
							txt += "<font color='royalblue'>退款处理中，两周没到账则火速 CALL 12306</font>";
						}
					}
					else
					{
						txt = "<font color='royalblue'>退款金额为0，无后续处理</font>";
					}

					lblMemo.Text = txt;
				}
			}
		}
	}
}
