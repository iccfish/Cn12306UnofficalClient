using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Order
{
	using TOBA.Order;
	using TOBA.Order.Entity;

	internal partial class OrderQueue : DialogBase
	{
		private readonly OrderCacheItem _queueInfo;
		private QueueOrderWorker _worker;
		private bool _cancelQueueConfirm;

		public OrderQueue(Session session, OrderCacheItem queueInfo)
		{
			_queueInfo = queueInfo;
			InitializeComponent();
			imglist.Images.Add(UiUtility.Get24PxImageFrom16PxImg(Properties.Resources.user_16));
			Gif.SetLoadingImage(pbAnimate);

			Icon = Properties.Resources.icon_warning;
			InitSession(session);

			LoadInfo();

			_worker = new QueueOrderWorker(session)
			{
				TourFlag = _queueInfo.tourFlag
			};
			_worker.QueueMessageChanged += _worker_QueueMessageChanged;
			_worker.QueryOrderQueueFailed += _worker_QueryOrderQueueFailed;
			_worker.QueryOrderQueueSuccess += _worker_QueryOrderQueueSuccess;

			Load += (s, e) =>
			{
				btnClose.Enabled = false;
				_worker.RunQueryOrderQueue();
			};
			FormClosing += (s, e) =>
			{
				e.Cancel = _worker != null;
			};
			btnClose.DialogResult = DialogResult.Cancel;
			btnCancelQueue.Click += BtnCancelQueue_Click;
			FormPlacementManager.Instance.Control(this);
		}

		private async void BtnCancelQueue_Click(object sender, EventArgs e)
		{
			if (_worker == null)
			{
				Close();
				return;
			}

			if (!_cancelQueueConfirm)
			{
				btnCancelQueue.Text = "再次点击以确认取消排队";
				_cancelQueueConfirm = true;
				return;
			}

			btnCancelQueue.Text = "正在取消...";
			btnCancelQueue.Enabled = false;
			var (ret, msg) = await _worker.CancelQueueOrderAsync().ConfigureAwait(true);

			btnCancelQueue.Enabled = ret == CancelQueueStatus.Failed || ret == CancelQueueStatus.NetworkError;
			btnCancelQueue.Text = msg;
			btnClose.Enabled = !btnCancelQueue.Enabled;

			if (ret == CancelQueueStatus.ForceLogout)
			{
				DevComponents.DotNetBar.MessageBoxEx.Show(this, "登录已失效，尽快重新登录查看排队结果，请留意是否有未完成订单。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				if (!await Session.BeenForceLogout().ConfigureAwait(true))
					Close();
			}
		}

		private void _worker_QueryOrderQueueSuccess(object sender, EventArgs e)
		{
			pbAnimate.Image = Properties.Resources.lxh_happy__2_;
			btnCancelQueue.Enabled = true;
			lblTimeInfo.Text = $"排队成功！订单号为 {_worker.OrderID}，请尽快付款！";
			lblTimeInfo.ForeColor = Color.Green;

			Session.OnRequestShowPanel(PanelIndex.Orders);
			Session.OnOrderChanged();
			btnClose.Image = Properties.Resources.cou_32_accept;
			btnClose.Enabled = true;
			btnCancelQueue.Visible = false;

			_worker = null;
		}

		private void _worker_QueryOrderQueueFailed(object sender, EventArgs e)
		{
			pbAnimate.Image = Properties.Resources.lxh_cry;
			btnCancelQueue.Enabled = true;
			lblTimeInfo.Text = _worker.Error?.ToString() ?? "未知信息";
			lblTimeInfo.ForeColor = Color.Red;

			btnClose.Enabled = true;
			btnCancelQueue.Visible = false;
			_worker = null;
		}

		private void _worker_QueueMessageChanged(object sender, EventArgs e)
		{
			lblTimeInfo.Text = _worker.QueueMessage;
			if (_worker.SlowQueueWarning)
			{
				lblTimeInfo.ForeColor = Color.Red;
			}
		}

		/// <summary>
		/// 加载信息
		/// </summary>
		void LoadInfo()
		{
			lblDate.Text = _queueInfo.trainDate.ToString("MM-dd");
			lblFrom.Text = _queueInfo.fromStationName;
			lblTo.Text = _queueInfo.toStationName;
			lblTrainCode.Text = _queueInfo.stationTrainCode;

			lstPas.BeginUpdate();
			lstPas.Items.AddRange(_queueInfo.tickets.Select(s => new ListViewItem(
				new[]
				{
					s.ticketTypeName,
					s.seatTypeName,
					s.passengerName,
					s.passengerIdTypeName
				})
			{ ImageIndex = 0 }
			).ToArray());
		}
	}
}
