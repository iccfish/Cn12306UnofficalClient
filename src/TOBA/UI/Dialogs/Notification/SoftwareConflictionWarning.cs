using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Notification
{
	using Configuration;

	internal partial class SoftwareConflictionWarning : Form
	{
		public SoftwareConflictionWarning()
		{
			InitializeComponent();
			imageList1.Images.Add(UiUtility.Get20PxImageFrom16PxImg(Properties.Resources.warning_16));
		}

		public void AttachSoftList(List<string> list)
		{
			lstProgs.Items.AddRange(list.Select(s => new ListViewItem(s) { ImageIndex = 0 }).ToArray());
			lstProgs.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
		}

		private void lnkOpenUninstall_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Shell.OpenProgramUninstallDialog();
		}

		private void btnUninstall_Click(object sender, EventArgs e)
		{
			Shell.OpenProgramUninstallDialog();
			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnContinue_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void chkNotShowAgain_CheckedChanged(object sender, EventArgs e)
		{
			ProgramConfiguration.Instance.ShowSoftConflictWarning = !chkNotShowAgain.Checked;
		}
	}
}
