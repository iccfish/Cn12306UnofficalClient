using System;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Account
{
	using DevComponents.DotNetBar;

	using TOBA.Account;

	internal partial class ModifyMobileCode : Office2007Form, IRequireSessionInit
	{
		BindMobileService _service;

		public string MobileNo => txtMobile.Text;

		public ModifyMobileCode(Session session)
		{
			Session = session;
			InitializeComponent();

			InitSession(session);
		}

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session { get; }

		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public void InitSession(Session session)
		{
			txtMobile.Text = session.UserKeyData.MobileNumber ?? "";
			_service = new BindMobileService(session);
		}

		private async void btnOK_Click(object sender, EventArgs e)
		{
			btnOK.Enabled = false;

			if (!txtMobile.MaskCompleted)
			{
				ToastNotification.Show(this, "请输入正确的手机号", Properties.Resources.cou_16_clock);
				btnOK.Enabled = true;
				return;
			}

			ToastNotification.Show(this, "正在提交新手机号...", Properties.Resources.xfsm_switch, -1);
			var result = await _service.ChangeMobileAsync(txtMobile.Text);
			ToastNotification.Close(this);

			if (result.IsNullOrEmpty())
			{
				this.Information("手机号修改成功。双向验证模式下，在核验之前，账户资料里的手机号不会变化。");
				Session.UserKeyData.MobileNumber = txtMobile.Text;

				DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				ToastNotification.Show(this, result, Properties.Resources.block_16, 2500);
			}

			btnOK.Enabled = true;

		}
	}
}
