namespace TOBA.UI.Controls.Common
{
	using System.Drawing;
	using System.Windows.Forms;

	public partial class LoadingTip : UserControl
	{
		public LoadingTip()
		{
			InitializeComponent();
		}

		/// <returns>
		/// 与该控件关联的文本。
		/// </returns>
		public override string Text
		{
			get { return label1.Text; }
			set { label1.Text = value; }
		}

		public Image LoadingImage
		{
			get { return pictureBox1.Image; }
			set { pictureBox1.Image = value; }
		}

		bool ShouldSerializeBorderStyle()
		{
			return false;
		}

		bool ShouldSerializeBackColor()
		{
			return BackColor != Color.White;
		}

		bool ShouldSerializeLoadingImage()
		{
			return pictureBox1.Image != Properties.Resources._32px_loading_1;
		}

	}
}
