
namespace TOBA.UI.Dialogs.TaskDialog
{
	using System.Drawing;
	using System.Windows.Forms;

	/// <summary>
	/// Form used in LockSystem mode. Contains an image of grayed desktop.
	/// </summary>
	internal class BackgroundForm : Form
	{
		private Bitmap _background;

		public BackgroundForm(Bitmap background)
		{
			BackColor = Color.Black;
			FormBorderStyle = FormBorderStyle.None;
			Location = Point.Empty;
			Size = Screen.PrimaryScreen.Bounds.Size;
			StartPosition = FormStartPosition.Manual;
			Visible = true;
			_background = background;
		}

		protected override void OnShown(System.EventArgs e)
		{
			this.BackgroundImage = _background;
			this.DoubleBuffered = true;
			base.OnShown(e);
		}
	}
}
