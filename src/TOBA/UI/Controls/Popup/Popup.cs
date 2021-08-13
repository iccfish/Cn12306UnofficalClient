namespace TOBA.UI.Controls.Popup
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Runtime.InteropServices;
	using System.Security.Permissions;
	using System.Windows.Forms;

	using Win32;

	using VS = System.Windows.Forms.VisualStyles;

	/// <summary>
	/// Represents a pop-up window.
	/// </summary>
	[ToolboxItem(false)]
	public partial class Popup : ToolStripDropDown
	{
		#region Fields & Properties

		/// <summary>
		/// Gets the content of the pop-up.
		/// </summary>
		public Control Content { get; private set; }

		/// <summary>
		/// Determines which animation to use while showing the pop-up window.
		/// </summary>
		public PopupAnimations ShowingAnimation { get; set; }

		/// <summary>
		/// Determines which animation to use while hiding the pop-up window.
		/// </summary>
		public PopupAnimations HidingAnimation { get; set; }

		/// <summary>
		/// Determines the duration of the animation.
		/// </summary>
		public int AnimationDuration { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the content should receive the focus after the pop-up has been opened.
		/// </summary>
		/// <value><c>true</c> if the content should be focused after the pop-up has been opened; otherwise, <c>false</c>.</value>
		/// <remarks>If the FocusOnOpen property is set to <c>false</c>, then pop-up cannot use the fade effect.</remarks>
		public bool FocusOnOpen { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether pressing the alt key should close the pop-up.
		/// </summary>
		/// <value><c>true</c> if pressing the alt key does not close the pop-up; otherwise, <c>false</c>.</value>
		public bool AcceptAlt { get; set; }

		private ToolStripControlHost _host;
		private Control _opener;
		private Popup _ownerPopup;
		private Popup _childPopup;
		private bool _resizableTop;
		private bool _resizableLeft;

		private bool _isChildPopupOpened;
		private bool _resizable;
		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="Popup" /> is resizable.
		/// </summary>
		/// <value><c>true</c> if resizable; otherwise, <c>false</c>.</value>
		public bool Resizable
		{
			get { return _resizable && !_isChildPopupOpened; }
			set { _resizable = value; }
		}

		private bool _nonInteractive;
		/// <summary>
		/// Gets or sets a value indicating whether the <see cref="Popup"></see> acts like a transparent windows (so it cannot be clicked).
		/// </summary>
		/// <value>
		/// <c>true</c> if the popup is noninteractive; otherwise, <c>false</c>.</value>
		public bool NonInteractive
		{
			get { return _nonInteractive; }
			set
			{
				if (value != _nonInteractive)
				{
					_nonInteractive = value;
					if (IsHandleCreated) RecreateHandle();
				}
			}
		}

		/// <summary>
		/// Gets or sets a minimum size of the pop-up.
		/// </summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		public new Size MinimumSize { get; set; }

		/// <summary>
		/// Gets or sets a maximum size of the pop-up.
		/// </summary>
		/// <returns>An ordered pair of type <see cref="T:System.Drawing.Size" /> representing the width and height of a rectangle.</returns>
		public new Size MaximumSize { get; set; }

		/// <summary>
		/// Gets parameters of a new window.
		/// </summary>
		/// <returns>An object of type <see cref="T:System.Windows.Forms.CreateParams" /> used when creating a new window.</returns>
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= Consts.WS_EX_NOACTIVATE;
				if (NonInteractive) cp.ExStyle |= Consts.WS_EX_TRANSPARENT | Consts.WS_EX_LAYERED | Consts.WS_EX_TOOLWINDOW;
				return cp;
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Popup"/> class.
		/// </summary>
		/// <param name="content">The content of the pop-up.</param>
		/// <remarks>
		/// Pop-up will be disposed immediately after disposion of the content control.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="content" /> is <code>null</code>.</exception>
		public Popup(Control content)
		{
			Content = content;
			FocusOnOpen = true;
			AcceptAlt = true;
			ShowingAnimation = PopupAnimations.SystemDefault;
			HidingAnimation = PopupAnimations.None;
			AnimationDuration = 100;
			InitializeComponent();
			AutoSize = false;
			DoubleBuffered = true;
			ResizeRedraw = true;
			_host = new ToolStripControlHost(content);
			Padding = Margin = _host.Padding = _host.Margin = Padding.Empty;
			if (UnsafeNativeMethods.IsRunningOnMono) content.Margin = Padding.Empty;
			MinimumSize = content.MinimumSize;
			content.MinimumSize = content.Size;
			MaximumSize = content.MaximumSize;
			content.MaximumSize = content.Size;
			Size = content.Size;
			if (UnsafeNativeMethods.IsRunningOnMono) _host.Size = content.Size;
			TabStop = content.TabStop = true;
			content.Location = Point.Empty;
			Items.Add(_host);
			content.Disposed += (sender, e) =>
			{
				content = null;
				Dispose(true);
			};
			content.RegionChanged += (sender, e) => UpdateRegion();
			content.Paint += (sender, e) => PaintSizeGrip(e);
			UpdateRegion();
		}

		#endregion

		#region Methods

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.ToolStripItem.VisibleChanged"/> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs"/> that contains the event data.</param>
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			if (UnsafeNativeMethods.IsRunningOnMono) return; // in case of non-Windows
			if ((Visible && ShowingAnimation == PopupAnimations.None) || (!Visible && HidingAnimation == PopupAnimations.None))
			{
				return;
			}
			var flags = Visible ? Consts.AW_ROLL : Consts.AW_HIDE;
			PopupAnimations _flags = Visible ? ShowingAnimation : HidingAnimation;
			if (_flags == PopupAnimations.SystemDefault)
			{
				if (SystemInformation.IsMenuAnimationEnabled)
				{
					if (SystemInformation.IsMenuFadeEnabled)
					{
						_flags = PopupAnimations.Blend;
					}
					else
					{
						_flags = PopupAnimations.Slide | (Visible ? PopupAnimations.TopToBottom : PopupAnimations.BottomToTop);
					}
				}
				else
				{
					_flags = PopupAnimations.None;
				}
			}
			if ((_flags & (PopupAnimations.Blend | PopupAnimations.Center | PopupAnimations.Roll | PopupAnimations.Slide)) == PopupAnimations.None)
			{
				return;
			}
			if (_resizableTop) // popup is “inverted”, so the animation must be
			{
				if ((_flags & PopupAnimations.BottomToTop) != PopupAnimations.None)
				{
					_flags = (_flags & ~PopupAnimations.BottomToTop) | PopupAnimations.TopToBottom;
				}
				else if ((_flags & PopupAnimations.TopToBottom) != PopupAnimations.None)
				{
					_flags = (_flags & ~PopupAnimations.TopToBottom) | PopupAnimations.BottomToTop;
				}
			}
			if (_resizableLeft) // popup is “inverted”, so the animation must be
			{
				if ((_flags & PopupAnimations.RightToLeft) != PopupAnimations.None)
				{
					_flags = (_flags & ~PopupAnimations.RightToLeft) | PopupAnimations.LeftToRight;
				}
				else if ((_flags & PopupAnimations.LeftToRight) != PopupAnimations.None)
				{
					_flags = (_flags & ~PopupAnimations.LeftToRight) | PopupAnimations.RightToLeft;
				}
			}
			flags = flags | (Consts.AW_MASK & (int)_flags);
			UnsafeNativeMethods.SetTopMost(this);
			UnsafeNativeMethods.AnimateWindow(this, AnimationDuration, flags);
		}

		/// <summary>
		/// Processes a dialog box key.
		/// </summary>
		/// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
		/// <returns>
		/// true if the key was processed by the control; otherwise, false.
		/// </returns>
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected override bool ProcessDialogKey(Keys keyData)
		{
			if (AcceptAlt && ((keyData & Keys.KeyCode) == Keys.Alt))
			{
				if ((keyData & Keys.F4) != Keys.F4)
				{
					return false;
				}
				else
				{
					Close();
				}
			}
			bool processed = base.ProcessDialogKey(keyData);
			if (!processed && (keyData == Keys.Tab || keyData == (Keys.Tab | Keys.Shift)))
			{
				bool backward = (keyData & Keys.Shift) == Keys.Shift;
				Content.SelectNextControl(null, !backward, true, true, true);
			}
			return processed;
		}

		/// <summary>
		/// Updates the pop-up region.
		/// </summary>
		protected void UpdateRegion()
		{
			if (Region != null)
			{
				Region.Dispose();
				Region = null;
			}
			if (Content.Region != null)
			{
				Region = Content.Region.Clone();
			}
		}

		/// <summary>
		/// Shows the pop-up window below the specified control.
		/// </summary>
		/// <param name="control">The control below which the pop-up will be shown.</param>
		/// <remarks>
		/// When there is no space below the specified control, the pop-up control is shown above it.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="control"/> is <code>null</code>.</exception>
		public void Show(Control control)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			Show(control, control.ClientRectangle);
		}

		/// <summary>
		/// Shows the pop-up window below the specified area.
		/// </summary>
		/// <param name="area">The area of desktop below which the pop-up will be shown.</param>
		/// <remarks>
		/// When there is no space below specified area, the pop-up control is shown above it.
		/// </remarks>
		public void Show(Rectangle area)
		{
			_resizableTop = _resizableLeft = false;
			Point location = new Point(area.Left, area.Top + area.Height);
			Rectangle screen = Screen.FromControl(this).WorkingArea;
			if (location.X + Size.Width > (screen.Left + screen.Width))
			{
				_resizableLeft = true;
				location.X = (screen.Left + screen.Width) - Size.Width;
			}
			if (location.Y + Size.Height > (screen.Top + screen.Height))
			{
				_resizableTop = true;
				location.Y -= Size.Height + area.Height;
			}
			//location = control.PointToClient(location);
			Show(location, ToolStripDropDownDirection.BelowRight);
		}

		/// <summary>
		/// Shows the pop-up window below the specified area of the specified control.
		/// </summary>
		/// <param name="control">The control used to compute screen location of specified area.</param>
		/// <param name="area">The area of control below which the pop-up will be shown.</param>
		/// <remarks>
		/// When there is no space below specified area, the pop-up control is shown above it.
		/// </remarks>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="control"/> is <code>null</code>.</exception>
		public void Show(Control control, Rectangle area)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			SetOwnerItem(control);

			_resizableTop = _resizableLeft = false;
			Point location = control.PointToScreen(new Point(area.Left, area.Top + area.Height));
			Rectangle screen = Screen.FromControl(control).WorkingArea;
			if (location.X + Size.Width > (screen.Left + screen.Width))
			{
				_resizableLeft = true;
				location.X = (screen.Left + screen.Width) - Size.Width;
			}
			if (location.Y + Size.Height > (screen.Top + screen.Height))
			{
				_resizableTop = true;
				location.Y -= Size.Height + area.Height;
			}
			location = control.PointToClient(location);
			Show(control, location, ToolStripDropDownDirection.BelowRight);
		}

		private void SetOwnerItem(Control control)
		{
			if (control == null)
			{
				return;
			}
			if (control is Popup)
			{
				Popup popupControl = control as Popup;
				_ownerPopup = popupControl;
				_ownerPopup._childPopup = this;
				OwnerItem = popupControl.Items[0];
				return;
			}
			else if (_opener == null)
			{
				_opener = control;
			}
			if (control.Parent != null)
			{
				SetOwnerItem(control.Parent);
			}
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.SizeChanged" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnSizeChanged(EventArgs e)
		{
			if (Content != null)
			{
				Content.MinimumSize = Size;
				Content.MaximumSize = Size;
				Content.Size = Size;
				Content.Location = Point.Empty;
			}
			base.OnSizeChanged(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.Control.Layout" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.LayoutEventArgs" /> that contains the event data.</param>
		protected override void OnLayout(LayoutEventArgs e)
		{
			if (!UnsafeNativeMethods.IsRunningOnMono)
			{
				base.OnLayout(e);
				return;
			}
			Size suggestedSize = GetPreferredSize(Size.Empty);
			if (AutoSize && suggestedSize != Size)
			{
				Size = suggestedSize;
			}
			SetDisplayedItems();
			OnLayoutCompleted(EventArgs.Empty);
			Invalidate();
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Opening" /> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.ComponentModel.CancelEventArgs" /> that contains the event data.</param>
		protected override void OnOpening(CancelEventArgs e)
		{
			if (Content.IsDisposed || Content.Disposing)
			{
				if (e != null)
				{
					e.Cancel = true;
				}
				return;
			}
			UpdateRegion();
			base.OnOpening(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Opened" /> event.
		/// </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		protected override void OnOpened(EventArgs e)
		{
			if (_ownerPopup != null)
			{
				_ownerPopup._isChildPopupOpened = true;
			}
			if (FocusOnOpen)
			{
				Content.Focus();
			}
			base.OnOpened(e);
		}

		/// <summary>
		/// Raises the <see cref="E:System.Windows.Forms.ToolStripDropDown.Closed"/> event.
		/// </summary>
		/// <param name="e">A <see cref="T:System.Windows.Forms.ToolStripDropDownClosedEventArgs"/> that contains the event data.</param>
		protected override void OnClosed(ToolStripDropDownClosedEventArgs e)
		{
			_opener = null;
			if (_ownerPopup != null)
			{
				_ownerPopup._isChildPopupOpened = false;
			}
			base.OnClosed(e);
		}

		#endregion

		#region Resizing Support

		/// <summary>
		/// Processes Windows messages.
		/// </summary>
		/// <param name="m">The Windows <see cref="T:System.Windows.Forms.Message" /> to process.</param>
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			//if (m.Msg == NativeMethods.WM_PRINT && !Visible)
			//{
			//    Visible = true;
			//}
			if (InternalProcessResizing(ref m, false))
			{
				return;
			}
			base.WndProc(ref m);
		}

		/// <summary>
		/// Processes the resizing messages.
		/// </summary>
		/// <param name="m">The message.</param>
		/// <returns>true, if the WndProc method from the base class shouldn't be invoked.</returns>
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public bool ProcessResizing(ref Message m)
		{
			return InternalProcessResizing(ref m, true);
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private bool InternalProcessResizing(ref Message m, bool contentControl)
		{
			if (m.Msg == Consts.WM_NCACTIVATE && m.WParam != IntPtr.Zero && _childPopup != null && _childPopup.Visible)
			{
				_childPopup.Hide();
			}
			if (!Resizable && !NonInteractive)
			{
				return false;
			}
			if (m.Msg == Consts.WM_NCHITTEST)
			{
				return OnNcHitTest(ref m, contentControl);
			}
			else if (m.Msg == Consts.WM_GETMINMAXINFO)
			{
				return OnGetMinMaxInfo(ref m);
			}
			return false;
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		private bool OnGetMinMaxInfo(ref Message m)
		{
			var minmax = (Minmaxinfo)Marshal.PtrToStructure(m.LParam, typeof(Minmaxinfo));
			if (!MaximumSize.IsEmpty)
			{
				minmax.maxTrackSize = MaximumSize;
			}
			minmax.minTrackSize = MinimumSize;
			Marshal.StructureToPtr(minmax, m.LParam, false);
			return true;
		}

		private bool OnNcHitTest(ref Message m, bool contentControl)
		{
			if (NonInteractive)
			{
				m.Result = (IntPtr)HitTest.HTTRANSPARENT;
				return true;
			}

			int x = Cursor.Position.X; // NativeMethods.LOWORD(m.LParam);
			int y = Cursor.Position.Y; // NativeMethods.HIWORD(m.LParam);
			Point clientLocation = PointToClient(new Point(x, y));

			GripBounds gripBouns = new GripBounds(contentControl ? Content.ClientRectangle : ClientRectangle);
			IntPtr transparent = (IntPtr)HitTest.HTTRANSPARENT;

			if (_resizableTop)
			{
				if (_resizableLeft && gripBouns.TopLeft.Contains(clientLocation))
				{
					m.Result = contentControl ? transparent : (IntPtr)HitTest.HTTOPLEFT;
					return true;
				}
				if (!_resizableLeft && gripBouns.TopRight.Contains(clientLocation))
				{
					m.Result = contentControl ? transparent : (IntPtr)HitTest.HTTOPRIGHT;
					return true;
				}
				if (gripBouns.Top.Contains(clientLocation))
				{
					m.Result = contentControl ? transparent : (IntPtr)HitTest.HTTOP;
					return true;
				}
			}
			else
			{
				if (_resizableLeft && gripBouns.BottomLeft.Contains(clientLocation))
				{
					m.Result = contentControl ? transparent : (IntPtr)HitTest.HTBOTTOMLEFT;
					return true;
				}
				if (!_resizableLeft && gripBouns.BottomRight.Contains(clientLocation))
				{
					m.Result = contentControl ? transparent : (IntPtr)HitTest.HTBOTTOMRIGHT;
					return true;
				}
				if (gripBouns.Bottom.Contains(clientLocation))
				{
					m.Result = contentControl ? transparent : (IntPtr)HitTest.HTBOTTOM;
					return true;
				}
			}
			if (_resizableLeft && gripBouns.Left.Contains(clientLocation))
			{
				m.Result = contentControl ? transparent : (IntPtr)HitTest.HTLEFT;
				return true;
			}
			if (!_resizableLeft && gripBouns.Right.Contains(clientLocation))
			{
				m.Result = contentControl ? transparent : (IntPtr)TOBA.Win32.HitTest.HTRIGHT;
				return true;
			}
			return false;
		}

		private VS.VisualStyleRenderer _sizeGripRenderer;
		/// <summary>
		/// Paints the sizing grip.
		/// </summary>
		/// <param name="e">The <see cref="System.Windows.Forms.PaintEventArgs" /> instance containing the event data.</param>
		public void PaintSizeGrip(PaintEventArgs e)
		{
			if (e == null || e.Graphics == null || !_resizable)
			{
				return;
			}
			Size clientSize = Content.ClientSize;
			using (Bitmap gripImage = new Bitmap(0x10, 0x10))
			{
				using (Graphics g = Graphics.FromImage(gripImage))
				{
					if (Application.RenderWithVisualStyles)
					{
						if (_sizeGripRenderer == null)
						{
							_sizeGripRenderer = new VS.VisualStyleRenderer(VS.VisualStyleElement.Status.Gripper.Normal);
						}
						_sizeGripRenderer.DrawBackground(g, new Rectangle(0, 0, 0x10, 0x10));
					}
					else
					{
						ControlPaint.DrawSizeGrip(g, Content.BackColor, 0, 0, 0x10, 0x10);
					}
				}
				GraphicsState gs = e.Graphics.Save();
				e.Graphics.ResetTransform();
				if (_resizableTop)
				{
					if (_resizableLeft)
					{
						e.Graphics.RotateTransform(180);
						e.Graphics.TranslateTransform(-clientSize.Width, -clientSize.Height);
					}
					else
					{
						e.Graphics.ScaleTransform(1, -1);
						e.Graphics.TranslateTransform(0, -clientSize.Height);
					}
				}
				else if (_resizableLeft)
				{
					e.Graphics.ScaleTransform(-1, 1);
					e.Graphics.TranslateTransform(-clientSize.Width, 0);
				}
				e.Graphics.DrawImage(gripImage, clientSize.Width - 0x10, clientSize.Height - 0x10 + 1, 0x10, 0x10);
				e.Graphics.Restore(gs);
			}
		}

		#endregion
	}
}
