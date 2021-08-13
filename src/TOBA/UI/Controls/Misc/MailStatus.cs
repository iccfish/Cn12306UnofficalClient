using System;

namespace TOBA.UI.Controls.Misc
{
	using Dialogs;

	using Option;

	using System.Drawing;
	using System.Windows.Forms;

	using TOBA.MailNotification;

	class MailStatus : ToolStripButton
	{

		MailConfiguration _mc;

		public MailStatus()
		{
			Image = Properties.Resources.letter_16;
			Text = "邮件状态";


			var mc = MailConfiguration.Instance;
			mc.PropertyChanged += (s, e) => RefreshStatus();

			Click += MailStatus_Click;
		}

		private void MailStatus_Click(object sender, EventArgs e)
		{
			using (var f = new ConfigCenter())
			{
				f.SelectConfigUI<MailConfig>();
				f.ShowDialog(AppContext.HostForm);
			}
		}

		private void RefreshStatus()
		{
			if (Parent == null)
			{
				return;
			}
			try
			{
				var f = Parent.FindForm();

				if (f?.InvokeRequired == true)
				{
					f.Invoke(new Action(RefreshStatus));
				}
			}
			catch (Exception e)
			{
				return;
			}

			var mc = MailConfiguration.Instance;

			if (!mc.Enabled)
			{
				Text = "邮件: 禁用";
			}
			else
			{
				Text = $"邮件: {MailConfiguration.Instance.Receivers?.JoinAsString(";")}";
				ToolTipText = "正在使用自定义的发送服务器";
				ForeColor = Color.RoyalBlue;
			}
		}
	}
}
