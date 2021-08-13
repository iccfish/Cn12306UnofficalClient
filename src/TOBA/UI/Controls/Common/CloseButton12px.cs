using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI.Controls.Common
{
	using System.Drawing;
	using System.Windows.Forms;

	class CloseButton12Px : PictureBox
	{
		public CloseButton12Px()
		{
			Image = Properties.Resources._12_em_cross_gray;
			SizeMode = PictureBoxSizeMode.CenterImage;
			Size = new Size(12, 12);
			Cursor = Cursors.Hand;

			MouseHover += CloseButton12px_MouseHover;
			MouseLeave += (s, e) =>
			{
				Image = Properties.Resources._12_em_cross_gray;
			};
		}

		private void CloseButton12px_MouseHover(object sender, EventArgs e)
		{
			Image = Properties.Resources._12_em_cross;
		}

		public bool ShouldSerializeCursor() => false;

		public bool ShouldSerializeImage() => false;

		public bool ShouldSerializeSize() => false;

		public bool ShouldSerializeSizeMode() => false;
	}
}
