
namespace TOBA.UI.Dialogs.TaskDialog
{
	using System.Drawing;
	using System.Windows.Forms;

	/// <summary>
	/// Button which preferred width is always divisible by 25.
	/// </summary>
	internal partial class Button : System.Windows.Forms.Button, IButtonControlWithImage
	{
		public override Size GetPreferredSize(Size proposedSize)
		{
			proposedSize = base.GetPreferredSize(proposedSize);
			proposedSize.Width += 24;
			if (this.Image != null)
			{
				proposedSize.Width += this.Image.Width;
			}
			proposedSize.Width -= proposedSize.Width % 25;
			proposedSize.Width = proposedSize.Width < 75 ? 75 : proposedSize.Width;
			proposedSize.Height = proposedSize.Height < 25 ? 25 : proposedSize.Height;
			return proposedSize;
		}
	}

	public interface IButtonControlWithImage : IButtonControl
	{
		Image Image { get; set; }
	}
}