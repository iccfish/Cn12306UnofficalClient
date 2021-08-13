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
	using DevComponents.DotNetBar;
	internal partial class LocalTimeDifferenceTooLarge : Office2007Form
	{
		public LocalTimeDifferenceTooLarge()
		{
			InitializeComponent();

			timer.Tick += (s, e) => Update();
			timer.Enabled = true;
			FormClosing += (s, e) => timer.Stop();
			Update();
		}


		void Update()
		{
			if (RunTime.ServerTimeOffset == null)
				return;
			var serverTime = DateTime.Now.Add(RunTime.ServerTimeOffset.Value);
			lblServerTime.Text = serverTime.ToString();
			lblLocalTime.Text = DateTime.Now.ToString();

			double seconds = RunTime.ServerTimeOffset.Value.TotalSeconds;
			lblTimeDiff.Text = (seconds < 0 ? "慢" : "快") + Math.Abs(seconds).ToString("#0.00") + "秒";
		}
	}
}
