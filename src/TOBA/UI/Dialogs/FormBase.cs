using System.Windows.Forms;

namespace TOBA.UI.Dialogs
{
	abstract class FormBase : Form
	{
		public void Information(string msg) => MessageBox.Show(this, "提示", msg, MessageBoxButtons.OK, MessageBoxIcon.Information);
	}
}
