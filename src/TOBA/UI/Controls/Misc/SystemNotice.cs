using FSLib.Network.Http;

using System;
using System.Drawing;
using System.Windows.Forms;

using TOBA.WebLib;

namespace TOBA.UI.Controls.Misc
{
	internal partial class SystemNotice : UserControl
	{
		public SystemNotice()
		{
			InitializeComponent();

			if (!Program.IsRunning)
				return;

			loading1.RequestLoad += (s, e) =>
			{
				var task = new NetClient().Create<byte[]>(HttpMethod.Get, "https://www.fishlee.net/service/download/592", allowAutoRedirect: true).Send();
				if (!task.IsValid())
				{
					Invoke(new Action(() => rtbNotice.Text = "无法加载信息，错误：" + task.Exception.Message));
				}
				else
				{
					Invoke(new Action(() => rtbNotice.Rtf = task.Result.DecompressAsString()));
				}
			};
			rtbNotice.LinkClicked += rtbNotice_LinkClicked;
			rtbNotice.DetectUrls = true;
			rtbNotice.ReadOnly = true;
			rtbNotice.BackColor = SystemColors.Window;

			Load += SystemNotice_Load;
		}

		void rtbNotice_LinkClicked(object sender, LinkClickedEventArgs e)
		{
			Shell.StartUrl(e.LinkText);
		}

		void SystemNotice_Load(object sender, EventArgs e)
		{
			loading1.Reload();
		}
		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			linkLabel1.Click += (s, ee) => Shell.StartUrl("https://www.fishlee.net/");
		}

		private void wb_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
		{
			loading1.Visible = false;
		}

		private void lnkRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			loading1.Reload();
		}


	}
}
