#region License
// Copyright ?2013 £ukasz Œwi¹tkowski
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
	using System;
	using System.Windows.Forms;

	/// <summary>
	/// Vista-like chevron button.
	/// </summary>
	internal partial class ChevronButton : System.Windows.Forms.Button
	{
		#region Constructor

		public ChevronButton()
		{
			InitializeComponent();
			Image = Properties.Resources.ChevronMore;
		}

		#endregion

		#region Fields & Properties

		private bool _isHovered;
		private bool _isFocused;
		private bool _isFocusedByKey;
		private bool _isKeyDown;
		private bool _isMouseDown;
		private bool _isPressed
		{
			get { return _isKeyDown || (_isMouseDown && _isHovered); }
		}

		private bool _expanded;
		public bool Expanded
		{
			get { return _expanded; }
			set
			{
				_expanded = value;
				SetImage();
			}
		}

		public override bool Focused
		{
			get { return false; }
		}

		#endregion

		#region Private Methods

		private void SetImage()
		{
			if (_isPressed)
			{
				Image = _expanded
						? Properties.Resources.ChevronLessPressed
						: Properties.Resources.ChevronMorePressed;
			}
			else if (_isHovered || _isFocused)
			{
				Image = _expanded
						? Properties.Resources.ChevronLessHovered
						: Properties.Resources.ChevronMoreHovered;
			}
			else
			{
				Image = _expanded
						? Properties.Resources.ChevronLess
						: Properties.Resources.ChevronMore;
			}
		}

		#endregion

		#region Overrided Methods

		protected override void OnClick(EventArgs e)
		{
			_isKeyDown = false;
			_isMouseDown = false;
			_expanded ^= true;
			SetImage();
			base.OnClick(e);
		}

		protected override void OnEnter(EventArgs e)
		{
			_isFocused = true;
			_isFocusedByKey = true;
			SetImage();
			base.OnEnter(e);
		}

		protected override void OnLeave(EventArgs e)
		{
			base.OnLeave(e);
			_isFocused = false;
			_isFocusedByKey = false;
			_isKeyDown = false;
			_isMouseDown = false;
			SetImage();
		}

		protected override void OnKeyDown(KeyEventArgs kevent)
		{
			if (kevent.KeyCode == Keys.Space)
			{
				_isKeyDown = true;
				SetImage();
			}
			base.OnKeyDown(kevent);
		}

		protected override void OnKeyUp(KeyEventArgs kevent)
		{
			if (_isKeyDown && (kevent.KeyCode == Keys.Space))
			{
				_isKeyDown = false;
				SetImage();
			}
			base.OnKeyUp(kevent);
		}

		protected override void OnMouseDown(MouseEventArgs mevent)
		{
			if (!_isMouseDown && (mevent.Button == MouseButtons.Left))
			{
				_isMouseDown = true;
				_isFocusedByKey = false;
				SetImage();
			}
			base.OnMouseDown(mevent);
		}

		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			if ((_isMouseDown))
			{
				_isMouseDown = false;
				SetImage();
			}
			base.OnMouseUp(mevent);
		}

		protected override void OnMouseMove(MouseEventArgs mevent)
		{
			base.OnMouseMove(mevent);
			if (mevent.Button != MouseButtons.None)
			{
				if (!ClientRectangle.Contains(mevent.X, mevent.Y))
				{
					if (_isHovered)
					{
						_isHovered = false;
						SetImage();
					}
				}
				else if (!_isHovered)
				{
					_isHovered = true;
					SetImage();
				}
			}
		}

		protected override void OnMouseEnter(EventArgs e)
		{
			_isHovered = true;
			SetImage();
			base.OnMouseEnter(e);
		}

		protected override void OnMouseLeave(EventArgs e)
		{
			_isHovered = false;
			SetImage();
			base.OnMouseLeave(e);
		}

		#endregion
	}
}
