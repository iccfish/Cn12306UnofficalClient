using System;
using System.Windows.Forms;

using TOBA.Configuration;
using TOBA.Otn.Workers;
using TOBA.Profile;

namespace TOBA.UI.Dialogs.Account
{
	using Common;

	using DevComponents.DotNetBar;

	internal partial class ModifyPassword : DialogBase, IOperation
	{
		DateTime _nextTime;

		public ModifyPassword()
		{
			InitializeComponent();

			btnOk.Click += btnOk_Click;
			this.Load += ModifyPassword_Load;
			codeTimer.Tick += (s, e) =>
			{
				var st = _nextTime - DateTime.Now;
				if (st.TotalSeconds >= 0)
				{
					lnkGetMobileCode.Text = $"{st.TotalSeconds.ToString("N0")}秒可重发";
				}
				else
				{
					lnkGetMobileCode.Text = "获得手机验证码";
					lnkGetMobileCode.Enabled = true;

					codeTimer.Enabled = false;
				}
			};
		}

		void ModifyPassword_Load(object sender, EventArgs e)
		{
			var pc = ProgramConfiguration.Instance;
			txtConfirm.PasswordChar = pc.PasswordChar;
			txtOrg.PasswordChar = pc.PasswordChar;
			txtNew.PasswordChar = pc.PasswordChar;
		}

		void btnOk_Click(object sender, EventArgs e)
		{
			var ori = txtOrg.Text;
			var newp = txtNew.Text;

			if (string.IsNullOrEmpty(ori))
			{
				this.Information("请输入原始密码");
				return;
			}
			if (string.IsNullOrEmpty(newp) || newp.Length < 6)
			{
				this.Information("请输入新密码，不得少于六位");
				return;
			}
			if (newp != txtConfirm.Text)
			{
				this.Information("新密码和确认密码不一致。");
				return;
			}

			var errmsg = "";
			var worker = new AccountModifyWorker(Session);
			using (var dlg = new YetAnotherWaitingDialog())
			{
				dlg.WorkCallback = () => errmsg = worker.ModifyPassword(ori, newp, txtCode.Text);
				dlg.ShowDialog();
			}

			if (string.IsNullOrEmpty(errmsg))
			{
				var uk = UserKeyDataMap.Current[Session.UserName];
				if (uk != null && !string.IsNullOrEmpty(uk.Password))
				{
					uk.Password = newp;
					UserKeyDataMap.Current.Save();
				}

				this.Information("密码修改成功！");
				DialogResult = DialogResult.OK;
				Close();
			}
			else
			{
				this.Error(errmsg);
			}
		}

		private async void lnkGetMobileCode_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			lnkGetMobileCode.Enabled = false;
			ToastNotification.Show(this, "正在获得手机验证码，请稍等", Properties.Resources.xfsm_switch, 0);

			var result = await Session.NetClient.GetMobileCode4EmailPwdAsync().ConfigureAwait(true);
			ToastNotification.Close(this);

			if (result.IsNullOrEmpty())
			{
				ToastNotification.Show(this, "验证码发送成功", Properties.Resources.tick_16, 1500);
				lnkGetMobileCode.Enabled = false;
				_nextTime = DateTime.Now.AddSeconds(120);
				codeTimer.Start();
			}
			else
			{
				lnkGetMobileCode.Enabled = true;
				ToastNotification.Show(this, result, Properties.Resources.warning_16, 1500);
			}
		}
	}
}
