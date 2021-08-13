using System;
using System.Drawing;
using System.Windows.Forms;

using TOBA.MailNotification;

namespace TOBA.UI.Controls.Option
{
	using DevComponents.DotNetBar;

	using Dialogs;

	internal partial class MailConfig : OptionConfigForm.AbstractOptionConfigUI
	{
		public MailConfig()
		{
			InitializeComponent();
			Load += MailConfig_Load;
		}

		void MailConfig_Load(object sender, EventArgs ee)
		{
			var mc = MailConfiguration.Instance;
			chkEnableMail.AddDataBinding(mc, s => s.Checked, s => s.Enabled);
			pConfig.AddDataBinding(chkEnableMail, s => s.Visible, s => s.Checked);
			txtBody.AddDataBinding(mc, s => s.Text, s => s.Body);
			txtTitle.AddDataBinding(mc, s => s.Text, s => s.Title);
			txtPwd.AddDataBinding(mc, s => s.Text, s => s.LoginPassword);
			txtSmtp.AddDataBinding(mc, s => s.Text, s => s.MailServer);
			chkSsl.AddDataBinding(mc, s => s.Checked, s => s.SSL);
			iptServerPort.Value = mc.MailServerPort;
			iptServerPort.ValueChanged += (s, e) =>
			{
				mc.MailServerPort = iptServerPort.Value;
			};
			mc.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(MailConfiguration.MailServerPort))
					iptServerPort.Value = mc.MailServerPort;
				else if (e.PropertyName == "LoginCredential")
					txtMailAdd.Text = mc.LoginCredential;
			};
			txtReceivers.Text = (mc.Receivers ?? new string[0]).JoinAsString(";");
			txtReceivers.TextChanged += (s, e) => mc.Receivers = txtReceivers.Text.Split(new[] { ',', ';', ' ', '/', '\\', '　' }, StringSplitOptions.RemoveEmptyEntries);

			//自动猜测
			txtMailAdd.AddDataBinding(mc, s => s.Text, s => s.LoginCredential);
			txtMailAdd.TextChanged += txtMailAdd_TextChanged;

			btnTest.Click += (s, e) =>
			{
				SendTestEmail();
			};
		}

		async void SendTestEmail()
		{
			btnTest.Enabled = false;
			//发送测试邮件
			ToastNotification.Show(this, "正在发送测试邮件中...", Properties.Resources.xfsm_switch, int.MaxValue);
			var title = "这是来自于订票助手.NET的一封测试邮件";
			var body = "这是来自于订票助手.NET的一封测试邮件。如果您能正常读取到这里，说明您在订票助手.NET中的设置是正确的。";
			var cfg = MailConfiguration.Instance;
			var err = "";

			err = await MailSender.Instance.SendEmailAsync(title, body, cfg.Receivers).ConfigureAwait(true);

			ToastNotification.Close(this);
			if (!string.IsNullOrEmpty(err))
			{
				HandleError(err);
			}
			else
			{
				Information("测试邮件发送成功。");
			}

			btnTest.Enabled = true;
		}

		void HandleError(string msg)
		{
			var exMsg = "QQ邮箱和126/163邮箱等需要用授权码登录而不是密码，详情请咨询邮箱帮助。";

			if (msg.IndexOf("STARTTLS") != -1)
			{
				exMsg = "看起来服务器需要SSL加密，已为您自动勾选，请重试。";
				chkSsl.Checked = true;
			}

			Error("测试邮件发送失败：" + msg + "\n\n" + exMsg);

		}

		void txtMailAdd_TextChanged(object sender, EventArgs e)
		{
			if (txtReceivers.TextLength == 0)
			{
				txtReceivers.Text = txtMailAdd.Text;
			}
			if (txtSmtp.TextLength != 0)
				return;

			MailSender.Instance.TryFigureOutMailConfig(txtMailAdd.Text);
		}

		#region Overrides of UserControl

		/// <returns>
		/// 与该控件关联的文本。
		/// </returns>
		public override string DisplayText
		{
			get { return "邮件设置"; }
		}


		/// <summary>
		/// 获得32PX大图像
		/// </summary>
		public override Image BigImage
		{
			get { return Properties.Resources.cou_32_mail; }
		}


		/// <summary>
		/// 获得图片
		/// </summary>
		public override Image Image
		{
			get { return Properties.Resources.cou_16_mail; }
		}

		/// <summary>
		/// 请求保存
		/// </summary>
		/// <returns></returns>
		public override bool Save()
		{
			if (chkEnableMail.Checked)
			{
				var msg = MailSender.Instance.CheckConfigurationError();
				if (!string.IsNullOrEmpty(msg))
				{
					MessageDialog.Information(msg);
					return false;
				}
			}
			return base.Save();
		}

		#endregion

	}
}
