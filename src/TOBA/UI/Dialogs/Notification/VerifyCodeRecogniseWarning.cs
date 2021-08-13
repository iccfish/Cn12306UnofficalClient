using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Notification
{
	internal partial class VerifyCodeRecogniseWarning : NotificationBase
	{
		public VerifyCodeRecogniseWarning()
		{
			InitializeComponent();
		}

		private void lnkForum_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Shell.StartUrl("http://forum.iccfish.com/forum-39-1.html");
		}
	}
}
