using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Notification
{
	using System.Threading;

	internal partial class ServerIpQuickFix : Form
	{

		public ServerIpQuickFix(AutoResetEvent waitHandle)
		{
			InitializeComponent();

			//create handle
			if (!IsHandleCreated)
				CreateHandle();

			if (waitHandle != null)
				waitHandle.Set();
		}

		private void ServerIpQuickFix_Load(object sender, EventArgs e)
		{

		}

		public void SetProgress(int count, int current)
		{
			if (InvokeRequired)
			{
				Invoke(new Action<int, int>(SetProgress));
				return;
			}

			pg.Style = count > 0 ? ProgressBarStyle.Blocks : ProgressBarStyle.Marquee;
			if (count > 0)
			{
				pg.Value = 0;
				pg.Maximum = count;
				pg.Value = current;
			}
		}
	}
}
