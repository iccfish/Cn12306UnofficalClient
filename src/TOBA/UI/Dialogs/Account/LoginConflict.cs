using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Account
{
	using Configuration;

	using DevComponents.DotNetBar;

	internal partial class LoginConflict : Office2007Form
	{
		public LoginConflict()
		{
			InitializeComponent();

			chkNotShowAnyMore.Checked = ProgramConfiguration.Instance.DismissedDialogs.Contains("login_confliect");
            chkNotShowAnyMore.CheckedChanged += ChkNotShowAnyMore_CheckedChanged;

			Load += (s, e) =>
			{
				BringToFront();
			};
		}

		private void ChkNotShowAnyMore_CheckedChanged(object sender, EventArgs e)
		{
			if (chkNotShowAnyMore.Checked)
			{
				if (!ProgramConfiguration.Instance.DismissedDialogs.Contains("login_confliect"))
				{
					ProgramConfiguration.Instance.DismissedDialogs.Add("login_confliect");
				}
			}
			else
			{
				if (ProgramConfiguration.Instance.DismissedDialogs.Contains("login_confliect"))
				{
					ProgramConfiguration.Instance.DismissedDialogs.Remove("login_confliect");
				}
			}
		}
	}
}
