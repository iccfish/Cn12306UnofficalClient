using System.Windows.Forms;

namespace TOBA.UI.Controls.Misc
{
	internal partial class TabWelcomeContent : UserControl
	{
		Timer _timer;

		public TabWelcomeContent()
		{
			if (!Program.IsRunning) return;

			InitializeComponent();
		}
	}
}
