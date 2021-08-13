#region License
// Copyright © 2013 Łukasz Świątkowski
// http://www.lukesw.net/
//
// This library is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This library is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public License
// along with this library.  If not, see <http://www.gnu.org/licenses/>.
#endregion

namespace TOBA.UI.Dialogs.TaskDialog
{
	using System.Drawing;
	using System.Windows.Forms;

	/// <summary>
	/// Label which measures its size in a “message box”-like way.
	/// </summary>
	internal partial class Label : LinkLabel
	{
		public Label()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// Add any initialization after the InitializeComponent() call.
			LinkColor = Color.FromArgb(0, 102, 204);
			ActiveLinkColor = LinkColor;
			DisabledLinkColor = Color.FromArgb(126, 133, 156);
		}

		public override Size GetPreferredSize(Size proposedSize)
		{
			proposedSize = base.GetPreferredSize(proposedSize);
			int w = Screen.FromControl(this).WorkingArea.Width / 2;
			proposedSize.Width = w < proposedSize.Width ? w : proposedSize.Width;
			return base.GetPreferredSize(proposedSize);
		}
	}
}