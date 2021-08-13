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
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Drawing.Imaging;
	using System.Windows.Forms;
	using System.Windows.Forms.VisualStyles;

	//TODO: RTL
	//TODO: gray shield
	//TODO: measuring
	//TODO: note
	//TODO: &

	/// <summary>
	/// Vista-like command link.
	/// </summary>
	[ToolboxBitmap(typeof(Dialogs.TaskDialog.Button)), ToolboxItem(true), ToolboxItemFilter("System.Windows.Forms"), Description("Raises an event when the user clicks it.")]
	public partial class CommandLink : System.Windows.Forms.Button
	{
		public CommandLink()
		{
			InitializeComponent();

			base.BackColor = Color.Transparent;
			BackColor = Color.Transparent;
			ForeColor = Color.FromArgb(21, 28, 85); //(0, 102, 204) // 0 51 153
			_hotForeColor = Color.FromArgb(7, 74, 229); //(0, 102, 204) // 0 51 153
														//Font * 4 / 3
			_pressedBorder = Color.FromArgb(153, 118, 118, 118);
			_pressedBackground = Color.FromArgb(102, 222, 225, 225);
			_activeBorderHot1 = Color.FromArgb(163, 0, 201, 253);
			_activeBorderHot2 = Color.FromArgb(128, 0, 201, 253);
			_activeBorderCold = Color.FromArgb(41, 0, 201, 253);
			_hoveredBorder1 = Color.FromArgb(107, 119, 119, 119);
			_hoveredBorder2 = Color.FromArgb(252, 255, 255, 255);
			_hoveredBorder3 = Color.FromArgb(183, 249, 249, 249);
			_hoveredBackground1 = Color.White;
			_hoveredBackground2 = Color.FromArgb(135, 242, 242, 242);
			_hoveredBackground3 = Color.FromArgb(102, 232, 232, 232);
			_animatingTo = FrameType.Normal;
			SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.Opaque, false);
		}

		public override Size GetPreferredSize(Size proposedSize)
		{
			proposedSize = base.GetPreferredSize(proposedSize);
			if (proposedSize.Height < TaskDialog.CustomScale * 41)
				proposedSize.Height = (int)(TaskDialog.CustomScale * 41);
			//proposedSize.Width = proposedSize.Width * 4 / 3;
			proposedSize.Width = (int)(TaskDialog.CustomScale * TextRenderer.MeasureText(Text, Font).Width / 0.75 + 60);
			return proposedSize;
		}

		#region Fields and Properties

		private Color _backColor;
		/// <summary> 
		/// Gets or sets the background color of the control. 
		/// </summary> 
		/// <returns>A <see cref="T:System.Drawing.Color" /> value representing the background color.</returns> 
		[DefaultValue(typeof(Color), "Transparent")]
		public virtual new Color BackColor
		{
			get { return _backColor; }
			set
			{
				if (!_backColor.Equals(value))
				{
					_backColor = value;
					UseVisualStyleBackColor = false;
					if (IsHandleCreated)
					{
						CreateFrames();
					}
					OnBackColorChanged(EventArgs.Empty);
				}
			}
		}

		/// <summary> 
		/// Gets or sets the foreground color of the control. 
		/// </summary> 
		/// <returns>The foreground <see cref="T:System.Drawing.Color" /> of the control.</returns> 
		[DefaultValue(typeof(Color), "21, 28, 85")]
		public virtual new Color ForeColor
		{
			get { return base.ForeColor; }
			set { base.ForeColor = value; }
		}

		private Color _hotForeColor;
		/// <summary> 
		/// Gets or sets the hot foreground color of the control. 
		/// </summary> 
		/// <returns>The hot foreground <see cref="T:System.Drawing.Color" /> of the control.</returns> 
		[DefaultValue(typeof(Color), "7, 74, 229"), Category("Appearance")]
		public virtual Color HotForeColor
		{
			get { return _hotForeColor; }
			set
			{
				if (!_hotForeColor.Equals(value))
				{
					_hotForeColor = value;
					//CreateFrames()
					OnHotForeColorChanged(EventArgs.Empty);
					if (IsHandleCreated)
					{
						Invalidate();
					}
				}
			}
		}

		//private string _note;
		///// <summary>
		///// Gets or sets the supporting note text of the command link.
		///// </summary>
		///// <value>New supporting note text of the command link.</value>
		///// <returns>The supporting note text of the command link.</returns>
		//[Category("Appearance"), Description("Specifies the supporting note text."), Browsable(true), DefaultValue((string)null)]
		//public string Note
		//{
		//    get { return _note; }
		//    set
		//    {
		//        if (_note != value)
		//        {
		//            _note = value;
		//            if (IsHandleCreated)
		//            {
		//                //CreateFrames();
		//                Invalidate();
		//            }
		//        }
		//    }
		//}

		protected override void OnTextChanged(System.EventArgs e)
		{
			base.OnTextChanged(e);
			if (IsHandleCreated)
			{
				//CreateFrames();
				Invalidate();
			}
		}

		private bool _showElevationIcon;
		/// <summary>
		/// Determines whether to show the elevation icon (shield).
		/// </summary>
		[Category("Appearance"), Description("Indicates whether the button should be decorated with the security shield icon."), Browsable(true), DefaultValue(false)]
		public bool ShowElevationIcon
		{
			get { return _showElevationIcon; }
			set
			{
				if (_showElevationIcon != value)
				{
					_showElevationIcon = value;
					if (IsHandleCreated)
					{
						CreateFrames();
						Invalidate();
					}
				}
			}
		}

		private Color _pressedBorder;
		private Color _pressedBackground;
		private Color _activeBorderHot1;
		private Color _activeBorderHot2;
		private Color _activeBorderCold;
		private Color _hoveredBorder1;
		private Color _hoveredBorder2;
		private Color _hoveredBorder3;
		private Color _hoveredBackground1;
		private Color _hoveredBackground2;
		private Color _hoveredBackground3;

		private bool _isHovered;
		private bool _isFocused;
		private bool _isFocusedByKey;
		private bool _isKeyDown;
		private bool _isMouseDown;
		private bool _isPressed
		{
			get { return _isKeyDown || (_isMouseDown && _isHovered); }
		}

		/// <summary> 
		/// Gets the state of the button control. 
		/// </summary> 
		/// <value>The state of the button control.</value> 
		[Browsable(false)]
		public PushButtonState State
		{
			get
			{
				if (!Enabled)
				{
					return PushButtonState.Disabled;
				}
				if (_isPressed)
				{
					return PushButtonState.Pressed;
				}
				if (_isHovered)
				{
					return PushButtonState.Hot;
				}
				if (_isFocused || IsDefault)
				{
					return PushButtonState.Default;
				}
				return PushButtonState.Normal;
			}
		}
		#endregion

		#region Events

		/// <summary>Occurs when the value of the <see cref="P:Glass.GlassButton.HotForeColor" /> property changes.</summary> 
		[Description("Event raised when the value of the HotForeColor property is changed."), Category("Property Changed")]
		public event EventHandler HotForeColorChanged;

		/// <summary> 
		/// Raises the <see cref="E:Glass.GlassButton.HotForeColorChanged" /> event. 
		/// </summary> 
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param> 
		protected virtual void OnHotForeColorChanged(EventArgs e)
		{
			if (HotForeColorChanged != null)
			{
				HotForeColorChanged(this, e);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:Glass.GlassButton.InnerBorderColor" /> property changes.</summary> 
		[Description("Event raised when the value of the InnerBorderColor property is changed."), Category("Property Changed")]
		public event EventHandler InnerBorderColorChanged;

		/// <summary> 
		/// Raises the <see cref="E:Glass.GlassButton.InnerBorderColorChanged" /> event. 
		/// </summary> 
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param> 
		protected virtual void OnInnerBorderColorChanged(EventArgs e)
		{
			if (InnerBorderColorChanged != null)
			{
				InnerBorderColorChanged(this, e);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:Glass.GlassButton.OuterBorderColor" /> property changes.</summary> 
		[Description("Event raised when the value of the OuterBorderColor property is changed."), Category("Property Changed")]
		public event EventHandler OuterBorderColorChanged;

		/// <summary> 
		/// Raises the <see cref="E:Glass.GlassButton.OuterBorderColorChanged" /> event. 
		/// </summary> 
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param> 
		protected virtual void OnOuterBorderColorChanged(EventArgs e)
		{
			if (OuterBorderColorChanged != null)
			{
				OuterBorderColorChanged(this, e);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:Glass.GlassButton.ShineColor" /> property changes.</summary> 
		[Description("Event raised when the value of the ShineColor property is changed."), Category("Property Changed")]
		public event EventHandler ShineColorChanged;

		/// <summary> 
		/// Raises the <see cref="E:Glass.GlassButton.ShineColorChanged" /> event. 
		/// </summary> 
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param> 
		protected virtual void OnShineColorChanged(EventArgs e)
		{
			if (ShineColorChanged != null)
			{
				ShineColorChanged(this, e);
			}
		}

		/// <summary>Occurs when the value of the <see cref="P:Glass.GlassButton.GlowColor" /> property changes.</summary> 
		[Description("Event raised when the value of the GlowColor property is changed."), Category("Property Changed")]
		public event EventHandler GlowColorChanged;

		/// <summary> 
		/// Raises the <see cref="E:Glass.GlassButton.GlowColorChanged" /> event. 
		/// </summary> 
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param> 
		protected virtual void OnGlowColorChanged(EventArgs e)
		{
			if (GlowColorChanged != null)
			{
				GlowColorChanged(this, e);
			}
		}
		#endregion

		#region Overrided Methods

		/// <summary> 
		/// Raises the <see cref="E:System.Windows.Forms.Control.EnabledChanged" /> event. 
		/// </summary> 
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param> 
		protected override void OnEnabledChanged(System.EventArgs e)
		{
			Fade();
			base.OnEnabledChanged(e);
		}

		/// <summary> 
		/// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event. 
		/// </summary> 
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param> 
		protected override void OnSizeChanged(EventArgs e)
		{
			CreateFrames();
			base.OnSizeChanged(e);
		}

		/// <summary> 
		/// Raises the <see cref="E:System.Windows.Forms.Control.Click" /> event. 
		/// </summary> 
		/// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param> 
		protected override void OnClick(EventArgs e)
		{
			_isKeyDown = false;
			_isMouseDown = false;
			Fade();
			base.OnClick(e);
		}

		/// <summary> 
		/// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event. 
		/// </summary> 
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnEnter(System.EventArgs e)
		{
			_isFocused = true;
			_isFocusedByKey = true;
			Fade();
			base.OnEnter(e);
		}

		/// <summary> 
		/// Raises the <see cref="E:System.Windows.Forms.Control.Leave" /> event. 
		/// </summary> 
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param> 
		protected override void OnLeave(System.EventArgs e)
		{
			base.OnLeave(e);
			_isFocused = false;
			_isFocusedByKey = false;
			_isKeyDown = false;
			_isMouseDown = false;
			Fade();
			Invalidate();
		}

		/// <summary> 
		/// Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnKeyUp(System.Windows.Forms.KeyEventArgs)" /> event. 
		/// </summary> 
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param> 
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Space)
			{
				_isKeyDown = true;
				Fade();
				Invalidate();
			}
			base.OnKeyDown(e);
		}

		/// <summary> 
		/// Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnKeyUp(System.Windows.Forms.KeyEventArgs)" /> event. 
		/// </summary> 
		/// <param name="e">A <see cref="T:System.Windows.Forms.KeyEventArgs" /> that contains the event data.</param> 
		protected override void OnKeyUp(KeyEventArgs e)
		{
			if (_isKeyDown && e.KeyCode == Keys.Space)
			{
				_isKeyDown = false;
				Fade();
				Invalidate();
			}
			base.OnKeyUp(e);
		}

		/// <summary> 
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event. 
		/// </summary> 
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param> 
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (!_isMouseDown && e.Button == MouseButtons.Left)
			{
				_isMouseDown = true;
				_isFocusedByKey = false;
				Fade();
				Invalidate();
			}
			base.OnMouseDown(e);
		}

		/// <summary> 
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseUp" /> event. 
		/// </summary> 
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param> 
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (_isMouseDown)
			{
				_isMouseDown = false;
				Fade();
				Invalidate();
			}
			base.OnMouseUp(e);
		}

		/// <summary> 
		/// Raises the <see cref="M:System.Windows.Forms.Control.OnMouseMove(System.Windows.Forms.MouseEventArgs)" /> event. 
		/// </summary> 
		/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param> 
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			if (e.Button != MouseButtons.None)
			{
				if (!ClientRectangle.Contains(e.X, e.Y))
				{
					if (_isHovered)
					{
						_isHovered = false;
						Fade();
						Invalidate();
					}
				}
				else if (!_isHovered)
				{
					_isHovered = true;
					Fade();
					Invalidate();
				}
			}
		}

		/// <summary> 
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event. 
		/// </summary> 
		/// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param> 
		protected override void OnMouseEnter(EventArgs e)
		{
			_isHovered = true;
			Fade();
			Invalidate();
			base.OnMouseEnter(e);
		}

		/// <summary> 
		/// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event. 
		/// </summary> 
		/// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param> 
		protected override void OnMouseLeave(EventArgs e)
		{
			_isHovered = false;
			Fade();
			Invalidate();
			base.OnMouseLeave(e);
		}
		#endregion

		#region Painting

		/// <summary> 
		/// Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnPaint(System.Windows.Forms.PaintEventArgs)" /> event. 
		/// </summary> 
		/// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param> 
		protected override void OnPaint(PaintEventArgs e)
		{
			DrawButtonBackgroundFromBuffer(e.Graphics);
			DrawButtonForeground(e.Graphics);
			if (Paint != null)
			{
				Paint(this, e);
			}
		}

		/// <summary> 
		/// Occurs when the control is redrawn. 
		/// </summary> 
		public new event PaintEventHandler Paint;

		private void DrawButtonBackgroundFromBuffer(Graphics graphics)
		{
			if (_frames == null || _frames.Count == 0)
			{
				CreateFrames();
			}
			if (_isAnimating)
			{
				using (Bitmap alphaImage = new Bitmap(ClientSize.Width, ClientSize.Height))
				{
					DrawAlphaImage(alphaImage, _frames[_animatingFrom], (float)(_animationDuration - _animationProgress) / _animationDuration);
					DrawAlphaImage(alphaImage, _frames[_animatingTo], (float)_animationProgress / _animationDuration);
					graphics.DrawImage(alphaImage, Point.Empty);
				}
			}
			else
			{
				graphics.DrawImage(_frames[_animatingTo], Point.Empty);
			}
		}

		private void DrawAlphaImage(Image alphaImage, Image image, float percent)
		{
			float[][] newColorMatrix = new float[5][];
			newColorMatrix[0] = new float[] { 1, 0, 0, 0, 0 };
			newColorMatrix[1] = new float[] { 0, 1, 0, 0, 0 };
			newColorMatrix[2] = new float[] { 0, 0, 1, 0, 0 };
			newColorMatrix[3] = new float[] { 0, 0, 0, percent, 0 };
			newColorMatrix[4] = new float[] { 0, 0, 0, 0, 1 };
			ColorMatrix matrix = new ColorMatrix(newColorMatrix);
			ImageAttributes imageAttributes = new ImageAttributes();
			imageAttributes.ClearColorKey();
			imageAttributes.SetColorMatrix(matrix);
			using (Graphics gr = Graphics.FromImage(alphaImage))
			{
				gr.DrawImage(image, new Rectangle(0, 0, Size.Width, Size.Height), 0, 0, Size.Width, Size.Height, GraphicsUnit.Pixel, imageAttributes);
			}
		}

		private Image CreateBackgroundFrame(FrameType frameType)
		{
			Rectangle rect = ClientRectangle;
			if (rect.Width <= 0)
			{
				rect.Width = 1;
			}
			if (rect.Height <= 0)
			{
				rect.Height = 1;
			}
			Image img = new Bitmap(rect.Width, rect.Height);
			using (Graphics g = Graphics.FromImage(img))
			{
				g.Clear(Color.Transparent);
				DrawButtonBackground(g, rect, frameType);
			}
			return img;
		}

		private void DrawButtonBackground(Graphics g, Rectangle rectangle, FrameType frameType)
		{
			SmoothingMode sm = g.SmoothingMode;
			g.SmoothingMode = SmoothingMode.AntiAlias;

			Rectangle rect;
			int arrowY = 12 + (int)(12f * (TaskDialog.CustomScale - 1f));

			switch (frameType)
			{
				case CommandLink.FrameType.Disabled:
					// TODO: gray elevated
					if (_showElevationIcon)
						g.DrawImage(Properties.Resources.SmallSecurity, 10, 14);
					else
						g.DrawImage(Properties.Resources.ArrowDisabled, 8, arrowY);
					break;

				case CommandLink.FrameType.Pressed:
					rect = rectangle;
					rect.Width -= 1;
					rect.Height -= 1;
					using (GraphicsPath bw = CreateRoundRectangle(rect, 3))
					{
						using (Pen p = new Pen(_pressedBorder))
						{
							g.DrawPath(p, bw);
						}
						using (SolidBrush b = new SolidBrush(_pressedBackground))
						{
							g.FillPath(b, bw);
						}
					}

					if (_showElevationIcon)
						g.DrawImage(Properties.Resources.SmallSecurity, 10, 14);
					else
						g.DrawImage(Properties.Resources.ArrowNormal, 8, arrowY);
					break;

				case CommandLink.FrameType.Normal:
					if (_showElevationIcon)
						g.DrawImage(Properties.Resources.SmallSecurity, 10, 14);
					else
						g.DrawImage(Properties.Resources.ArrowNormal, 8, arrowY);
					break;

				case CommandLink.FrameType.Hovered:
					rect = rectangle;
					rect.Width -= 1;
					rect.Height -= 1;
					using (GraphicsPath bw = CreateRoundRectangle(rect, 3))
					{
						using (Pen p = new Pen(_hoveredBorder1))
						{
							g.DrawPath(p, bw);
						}
					}

					rect.Inflate(-1, -1);
					using (GraphicsPath bw = CreateRoundRectangle(rect, 2))
					{
						using (LinearGradientBrush b = new LinearGradientBrush(rect, _hoveredBorder2, _hoveredBorder3, LinearGradientMode.Vertical))
						{
							using (Pen p = new Pen(b))
							{
								g.DrawPath(p, bw);
							}
						}
						if (rect.Height < 40)
						{
							using (LinearGradientBrush b = new LinearGradientBrush(rect, _hoveredBackground1, _hoveredBackground2, LinearGradientMode.Vertical))
							{
								g.FillPath(b, bw);
							}
						}
						else
						{
							RectangleF rect1 = rect;
							rect1.Height *= 0.75f;
							RectangleF rect2 = rect;
							rect2.Height -= rect1.Height;
							rect2.Y += rect1.Height;
							using (LinearGradientBrush b = new LinearGradientBrush(rect1, _hoveredBackground1, _hoveredBackground2, LinearGradientMode.Vertical))
							{
								g.SetClip(rect1);
								g.FillPath(b, bw);
								g.ResetClip();
							}
							using (LinearGradientBrush b = new LinearGradientBrush(rect2, _hoveredBackground2, _hoveredBackground3, LinearGradientMode.Vertical))
							{
								g.SetClip(rect2);
								g.FillPath(b, bw);
								g.ResetClip();
							}
						}
					}

					if (_showElevationIcon)
						g.DrawImage(Properties.Resources.SmallSecurity, 10, 14);
					else
						g.DrawImage(Properties.Resources.ArrowHovered, 8, arrowY);
					break;

				case CommandLink.FrameType.Active1:
					rect = rectangle;
					rect.Width -= 3;
					rect.X += 1;
					rect.Height -= 1;
					using (GraphicsPath bw = CreateRoundRectangle(rect, 3))
					{
						using (LinearGradientBrush b = new LinearGradientBrush(rect, _activeBorderHot1, _activeBorderHot2, LinearGradientMode.Vertical))
						{
							using (Pen p = new Pen(b))
							{
								g.DrawPath(p, bw);
							}
						}
					}

					if (_showElevationIcon)
						g.DrawImage(Properties.Resources.SmallSecurity, 10, 14);
					else
						g.DrawImage(Properties.Resources.ArrowNormal, 8, arrowY);
					break;

				case CommandLink.FrameType.Active2:
					rect = rectangle;
					rect.Width -= 3;
					rect.X += 1;
					rect.Height -= 1;
					using (GraphicsPath bw = CreateRoundRectangle(rect, 3))
					{
						using (Pen p = new Pen(_activeBorderCold))
						{
							g.DrawPath(p, bw);
						}
					}

					if (_showElevationIcon)
						g.DrawImage(Properties.Resources.SmallSecurity, 10, 14);
					else
						g.DrawImage(Properties.Resources.ArrowNormal, 8, arrowY);
					break;
			}

			g.SmoothingMode = sm;
		}

		private void DrawButtonForeground(Graphics g)
		{
			if (Focused && ShowFocusCues && _isFocusedByKey)
			{
				Rectangle rect = ClientRectangle;
				rect.Inflate(-4, -4);
				ControlPaint.DrawFocusRectangle(g, rect);
			}

			using (Font f = new Font(Font.FontFamily, Font.SizeInPoints / 0.75f, Font.Style, GraphicsUnit.Point, Font.GdiCharSet, Font.GdiVerticalFont))
			{
				if (!Enabled)
				{
					TextRenderer.DrawText(g, Text, f, new Point(28, 12), Color.Gray, Color.Transparent);
				}
				else if (State == PushButtonState.Hot)
				{
					TextRenderer.DrawText(g, Text, f, new Point(28, 12), HotForeColor, Color.Transparent);
				}
				else
				{
					TextRenderer.DrawText(g, Text, f, new Point(28, 12), ForeColor, Color.Transparent);
				}
			}

		}

		//'Private imageButton As Button
		//'Private Sub DrawForegroundFromButton(ByVal pevent As PaintEventArgs)
		//'    If imageButton Is Nothing Then
		//'        imageButton = New Button()
		//'        imageButton.Parent = New TransparentControl()
		//'        imageButton.SuspendLayout()
		//'        imageButton.BackColor = Color.Transparent
		//'        imageButton.FlatAppearance.BorderSize = 0
		//'        imageButton.FlatStyle = FlatStyle.Flat
		//'    Else
		//'        imageButton.SuspendLayout()
		//'    End If
		//'    imageButton.AutoEllipsis = AutoEllipsis
		//'    If Enabled Then
		//'        imageButton.ForeColor = ForeColor
		//'    Else
		//'        imageButton.ForeColor = Color.FromArgb((3 * ForeColor.R + _backColor.R) >> 2, (3 * ForeColor.G + _backColor.G) >> 2, (3 * ForeColor.B + _backColor.B) >> 2)
		//'    End If
		//'    imageButton.Font = Font
		//'    imageButton.RightToLeft = RightToLeft
		//'    If imageButton.Image IsNot Image AndAlso imageButton.Image IsNot Nothing Then
		//'        imageButton.Image.Dispose()
		//'    End If
		//'    If Image IsNot Nothing Then
		//'        imageButton.Image = Image
		//'        If Not Enabled Then
		//'            Dim size As Size = Image.Size
		//'            Dim newColorMatrix As Single()() = New Single(4)() {}
		//'            newColorMatrix(0) = New Single() {0.2125F, 0.2125F, 0.2125F, 0.0F, 0.0F}
		//'            newColorMatrix(1) = New Single() {0.2577F, 0.2577F, 0.2577F, 0.0F, 0.0F}
		//'            newColorMatrix(2) = New Single() {0.0361F, 0.0361F, 0.0361F, 0.0F, 0.0F}
		//'            Dim arr As Single() = New Single(4) {}
		//'            arr(3) = 1.0F
		//'            newColorMatrix(3) = arr
		//'            newColorMatrix(4) = New Single() {0.38F, 0.38F, 0.38F, 0.0F, 1.0F}
		//'            Dim matrix As New System.Drawing.Imaging.ColorMatrix(newColorMatrix)
		//'            Dim disabledImageAttr As New System.Drawing.Imaging.ImageAttributes()
		//'            disabledImageAttr.ClearColorKey()
		//'            disabledImageAttr.SetColorMatrix(matrix)
		//'            imageButton.Image = New Bitmap(Image.Width, Image.Height)
		//'            Using gr As Graphics = Graphics.FromImage(imageButton.Image)
		//'                gr.DrawImage(Image, New Rectangle(0, 0, size.Width, size.Height), 0, 0, size.Width, size.Height, _
		//'                GraphicsUnit.Pixel, disabledImageAttr)
		//'            End Using
		//'        End If
		//'    End If
		//'    imageButton.ImageAlign = ImageAlign
		//'    imageButton.ImageIndex = ImageIndex
		//'    imageButton.ImageKey = ImageKey
		//'    imageButton.ImageList = ImageList
		//'    imageButton.Padding = Padding
		//'    imageButton.Size = Size
		//'    imageButton.Text = Text
		//'    imageButton.TextAlign = TextAlign
		//'    imageButton.TextImageRelation = TextImageRelation
		//'    imageButton.UseCompatibleTextRendering = UseCompatibleTextRendering
		//'    imageButton.UseMnemonic = UseMnemonic
		//'    imageButton.ResumeLayout()
		//'    InvokePaint(imageButton, pevent)
		//'End Sub

		//'Private Class TransparentControl
		//'    Inherits Control
		//'    Protected Overloads Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
		//'    End Sub
		//'    Protected Overloads Overrides Sub OnPaint(ByVal e As PaintEventArgs)
		//'    End Sub
		//'End Class

		private static GraphicsPath CreateRoundRectangle(Rectangle rectangle, int radius)
		{
			GraphicsPath path = new GraphicsPath();
			int l = rectangle.Left;
			int t = rectangle.Top;
			int w = rectangle.Width;
			int h = rectangle.Height;
			int d = radius << 1;
			// topleft 
			path.AddArc(l, t, d, d, 180, 90);
			// top 
			path.AddLine(l + radius, t, l + w - radius, t);
			// topright 
			path.AddArc(l + w - d, t, d, d, 270, 90);
			// right 
			path.AddLine(l + w, t + radius, l + w, t + h - radius);
			// bottomright 
			path.AddArc(l + w - d, t + h - d, d, d, 0, 90);
			// bottom 
			path.AddLine(l + w - radius, t + h, l + radius, t + h);
			// bottomleft 
			path.AddArc(l, t + h - d, d, d, 90, 90);
			// left 
			path.AddLine(l, t + h - radius, l, t + radius);
			path.CloseFigure();
			return path;
		}

		#endregion

		#region Unused Properties & Events

		/// <summary>This property is not relevant for this class.</summary> 
		/// <returns>This property is not relevant for this class.</returns> 
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
		public new FlatButtonAppearance FlatAppearance
		{
			get { return base.FlatAppearance; }
		}

		/// <summary>This property is not relevant for this class.</summary> 
		/// <returns>This property is not relevant for this class.</returns> 
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
		public new FlatStyle FlatStyle
		{
			get { return base.FlatStyle; }
			set { base.FlatStyle = value; }
		}

		/// <summary>This property is not relevant for this class.</summary> 
		/// <returns>This property is not relevant for this class.</returns> 
		[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
		public new bool UseVisualStyleBackColor
		{
			get { return base.UseVisualStyleBackColor; }
			set { base.UseVisualStyleBackColor = value; }
		}
		#endregion

		#region Animation Support

		private Dictionary<FrameType, Image> _frames;

		private enum FrameType
		{
			Disabled = 0,
			Pressed = 1,
			Normal = 2,
			Hovered = 3,
			Active1 = 4,
			Active2 = 5
		}

		private void CreateFrames()
		{
			DestroyFrames();
			if (!IsHandleCreated)
			{
				return;
			}
			if (_frames == null)
			{
				_frames = new Dictionary<FrameType, Image>();
			}
			_frames.Add(FrameType.Disabled, CreateBackgroundFrame(FrameType.Disabled));
			_frames.Add(FrameType.Pressed, CreateBackgroundFrame(FrameType.Pressed));
			_frames.Add(FrameType.Normal, CreateBackgroundFrame(FrameType.Normal));
			_frames.Add(FrameType.Hovered, CreateBackgroundFrame(FrameType.Hovered));
			_frames.Add(FrameType.Active1, CreateBackgroundFrame(FrameType.Active1));
			_frames.Add(FrameType.Active2, CreateBackgroundFrame(FrameType.Active2));
		}

		private void DestroyFrames()
		{
			if (_frames != null)
			{
				_frames[FrameType.Disabled].Dispose();
				_frames[FrameType.Pressed].Dispose();
				_frames[FrameType.Normal].Dispose();
				_frames[FrameType.Hovered].Dispose();
				_frames[FrameType.Active1].Dispose();
				_frames[FrameType.Active2].Dispose();
				_frames.Clear();
				_frames = null;
			}
		}

		private int _animationProgress;
		private FrameType _animatingFrom;
		private FrameType _animatingTo;
		private int _animationDuration;

		private bool _isAnimating
		{
			get { return timer.Enabled; }
		}

		private void Fade()
		{
			if (!Enabled)
			{
				FadeTo(FrameType.Disabled, 200);
			}
			else if (_isPressed)
			{
				FadeTo(FrameType.Pressed, 200);
			}
			else if (_isHovered || _isFocusedByKey)
			{
				if (_animatingTo == FrameType.Pressed)
				{
					FadeTo(FrameType.Hovered, 200);
				}
				else
				{
					FadeTo(FrameType.Hovered, 200);
				}
			}
			else if (_isFocused) // || IsDefault
			{
				if (_animatingTo == FrameType.Hovered)
				{
					FadeTo(FrameType.Active1, 1000);
				}
				else
				{
					FadeTo(FrameType.Active1, 200);
				}
			}
			else
			{
				if (_animatingTo == FrameType.Pressed)
				{
					FadeTo(FrameType.Normal, 200);
				}
				else
				{
					FadeTo(FrameType.Normal, 1000);
				}
			}
		}

		private void FadeTo(FrameType frameType, int animationDuration)
		{
			if (_animatingTo == frameType) return;
			if (_animatingFrom == frameType)
			{
				_animationProgress = (_animationDuration - _animationProgress) * animationDuration / _animationDuration;
				_animationDuration = animationDuration;
				_animatingFrom = _animatingTo;
				_animatingTo = frameType;
			}
			else
			{
				_animationDuration = animationDuration;
				_animationProgress = 0;
				_animatingFrom = _animatingTo;
				_animatingTo = frameType;
			}
			timer.Enabled = true;
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if (!timer.Enabled)
			{
				return;
			}
			_animationProgress += timer.Interval;
			if (_animationProgress > _animationDuration)
			{
				_animationProgress = _animationDuration;
				if (_animatingTo == FrameType.Active1 || _animatingTo == FrameType.Active2)
				{
					if (_animatingTo == FrameType.Active1)
					{
						_animatingFrom = FrameType.Active1;
						_animatingTo = FrameType.Active2;
					}
					else
					{
						_animatingFrom = FrameType.Active2;
						_animatingTo = FrameType.Active1;
					}
					_animationProgress = 0;
					_animationDuration = 2000;
				}
				else
				{
					timer.Enabled = false;
				}
			}
			Refresh();
		}
		#endregion
	}
}