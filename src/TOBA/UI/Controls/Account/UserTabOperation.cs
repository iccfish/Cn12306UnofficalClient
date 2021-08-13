using System;

using TOBA.UI.Dialogs.Account;

namespace TOBA.UI.Controls.Account
{
	internal partial class UserTabOperation : ControlBase, IOperation
	{
		public UserTabOperation()
		{
			InitializeComponent();

			btnOpModPwd.Click += (s, e) => new ModifyPassword() { Session = Session }.ShowDialog();
			TOBA.Session.MobileCheckStateChanged += Session_MobileCheckStateChanged;
			Disposed += (s, e) =>
			{
				TOBA.Session.MobileCheckStateChanged -= Session_MobileCheckStateChanged;
			};
			btnChangeMobile.Click += (s, e) => new ModifyMobileCode(Session).ShowDialog(this);
			btnCheckMobile.Click += (s, e) => new AccountMobileCheck(Session).ShowDialog(this);
		}

		private void Session_MobileCheckStateChanged(object sender, EventArgs e)
		{
			if (Session != sender)
				return;

			AppContext.HostForm.Invoke(() =>
			{
				try
				{
					var state = Session.IsMobileChecked;
					var mb = Session.UserKeyData.MobileNumber ?? "";

					if (!IsHandleCreated)
						CreateHandle();

					lblMobileStatus.Text = "当前号码 <b><font color='blue'>" + mb + "</font></b> <b><font color='" + (state == null ? "red" : state.Value ? "green" : "red") + "'>" + (state == null ? "刷新异常" : state.Value ? "已通过核验" : "未通过核验") + "</font></b>";
				}
				catch (Exception)
				{

				}
			});
		}
	}
}
