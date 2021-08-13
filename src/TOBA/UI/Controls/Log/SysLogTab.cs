using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Log
{
	internal class SysLogTab:TabPage
	{
		public SysLogTab()
		{
			Text = "系统日志";
			ImageIndex = 4;

			var control = new SysLogPanel();
			control.Dock = DockStyle.Fill;
			Controls.Add(control);
		}
	}
}
