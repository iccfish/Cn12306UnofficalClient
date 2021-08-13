using System;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.OsSupport
{
	using FSLib.Extension;

	using System.Diagnostics;

	public partial class SysErrorFound : Form
	{
		public SysErrorFound()
		{
			Icon = Properties.Resources.icon_warning;
			InitializeComponent();
		}

		private void btnAutoFix_Click(object sender, EventArgs e)
		{
			var path = ApplicationRunTimeContext.GetProcessMainModulePath();

			var psi = new ProcessStartInfo(path, "-fixos");
			if (Environment.OSVersion.Version.Major > 5)
				psi.Verb = "runas";

			try
			{
				Process.Start(psi);
			}
			catch
			{

			}

			System.Environment.Exit(0);
		}
	}
}
