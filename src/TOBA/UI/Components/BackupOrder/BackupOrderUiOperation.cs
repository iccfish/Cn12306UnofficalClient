using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI.Components.BackupOrder
{
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using Autofac;

	using DevComponents.DotNetBar;

	using Dialogs;
	using Dialogs.BackupOrder;

	using Query.Entity;

	using TOBA.BackupOrder;

	class BackupOrderUiOperation : IBackupOrderUiOperation
	{

		private ILifetimeScope _scope;


		private IBackupOrderService BackupOrderService { get => _scope.Resolve<IBackupOrderService>(); }

		private Form HostForm { get => AppContext.HostForm; }

		private BackupCart Cart { get => Session.BackupOrderCart; }

		public BackupOrderUiOperation(Session session, ILifetimeScope scope)
		{
			Session = session;
			_scope = scope;
		}

		public Session Session { get; }

		public async Task<bool> AddToBackupOrderAsync(QueryResultItem train, LeftTicketData ticket)
		{
			var (allow, message) = Cart.CanAdd(train, ticket.Code);
			if (!allow)
			{
				HostForm.ShowErrorToast(message);
				return false;
			}

			var item = new BackupCartItem() { Seat = ticket.Code, Train = train };

			if (Session.FaceCheckStatus == null)
			{
				var toast = HostForm.ShowInfoToast("正在加入候补订单...", 0);
				var result = await Session.DetectFaceCheckStatusByQueryResult(train.QueryResult, train, ticket.Code);
				ToastNotification.Close(HostForm, toast);
			}

			if (Session.FaceCheckStatus == null)
			{
				HostForm.ShowWarningToastMini("网络错误或尚未登录，没法检测状态呢。");
				return false;
			}

			if (!Session.FaceCheckStatus.Value)
			{
				HostForm.ShowWarningToastMini("悲剧了，咋还没通过人脸认证呢。");
				return false;
			}

			//查询排队人数
			var toastQ = HostForm.ShowInfoToast("正在查询候补人数...", 0);
			var (level, info) = await BackupOrderService.GetSuccessRateAsync(item);
			ToastNotification.Close(HostForm, toastQ);
			if (level == 0 || level == 4)
			{
				//0-失败；4-过多
				HostForm.ShowErrorToast(info);
				return false;
			}
			Cart.Items.Add(item);
			HostForm.ShowSuccessToast("已加入候补订单列表");

			return true;
		}

		public async Task<bool> SubmitOrderRequestAsync()
		{
			if (Cart.Items.Count == 0)
			{
				HostForm.ShowWarningToast("没有候补车次");
				return false;
			}

			var intPtr = HostForm.ShowInfoToast("正在提交候补需求...", int.MaxValue);
			var (ok, msg) = await BackupOrderService.SubmitOrderRequestAsync();
			ToastNotification.Close(HostForm, intPtr);

			if (ok)
			{
				var dlg = DialogBase.FindCurrentTypeFormInSession<SubmitBackupOrderDlg>(Session);
				if (dlg == null)
				{
					dlg = Session.GetService<SubmitBackupOrderDlg>();
					dlg.Show();
				}
				else
				{
					dlg.Activate();
				}

				return true;
			}
			else
			{
				HostForm.ShowErrorToast(msg);

				if (msg.IndexOf("重新登录") != -1)
				{
					Session.BeenForceLogout();
				}

				return false;
			}
		}
	}
}
