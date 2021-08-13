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
	using TOBA.Configuration;

	internal partial class MsgFrom12306 : OfficeForm
	{
		public MsgFrom12306(string message)
		{
			InitializeComponent();

			labelX1.Text = message;

			chkEnable.AddDataBinding(ProgramConfiguration.Instance, s => s.Checked, s => s.ShowMessageFrom12306);
		}
	}
}
