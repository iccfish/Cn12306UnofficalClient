
namespace TOBA.UI.Dialogs.TaskDialog
{
	using Media;

	using System;
	using System.Drawing;
	using System.Linq;
	using System.Reflection;
	using System.Threading;
	using System.Windows.Forms;

	using Win32;

	/// <summary>
	/// Displays a Vista-like message box or task dialog that can contain text, buttons, symbols and other elements that inform and instruct the user.
	/// </summary>
	public sealed class TaskDialog
	{
		#region MessageBox

		public static IWin32Window DefaultOwnerWindow { get; set; }

		/// <summary>
		/// Displays a Vista-like message box with specified text.
		/// </summary>
		/// <param name="text">The text to display in the Vista-like message box.</param>
		/// <returns>One of the DialogResult values.</returns>
		public static DialogResult Show(string text)
		{
			return Show(DefaultOwnerWindow, text);
		}

		/// <summary>
		/// Displays a Vista-like message box in front of the specified object and with the specified text.
		/// </summary>
		/// <param name="owner">An implementation of IWin32Window that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the Vista-like message box.</param>
		/// <returns>One of the DialogResult values.</returns>
		public static DialogResult Show(IWin32Window owner, string text)
		{
			var caption = "Application";

			//HACK: not using getentryassembly() due to this method will fail if reactor repleaced loader.
			//var p = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false).FirstOrDefault() as AssemblyProductAttribute;
			//if (p != null) caption = p.Product;

			var assembly = Assembly.GetEntryAssembly();
			if (assembly != null)
			{
				var p = assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false).FirstOrDefault() as AssemblyProductAttribute;
				if (p != null) caption = p.Product;
			}
			else
			{
				//using file version info
				var fileversioninfo = FSLib.Extension.ApplicationRunTimeContext.GetProcessMainModule().FileVersionInfo.ProductName;
				if (!fileversioninfo.IsNullOrEmpty())
					caption = fileversioninfo;
			}


			return Show(owner, text, caption);
		}

		/// <summary>
		/// Displays a Vista-like message box with specified text and caption.
		/// </summary>
		/// <param name="text">The text to display in the Vista-like message box.</param>
		/// <param name="caption">The text to display in the title bar of the Vista-like message box.</param>
		/// <returns>One of the DialogResult values.</returns>
		public static DialogResult Show(string text, string caption)
		{
			return Show(DefaultOwnerWindow, text, caption);
		}

		/// <summary>
		/// Displays a Vista-like message box in front of the specified object and with the specified text and caption.
		/// </summary>
		/// <param name="owner">An implementation of IWin32Window that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the Vista-like message box.</param>
		/// <param name="caption">The text to display in the title bar of the Vista-like message box.</param>
		/// <returns>One of the DialogResult values.</returns>
		public static DialogResult Show(IWin32Window owner, string text, string caption)
		{
			return Show(owner, text, caption, MessageBoxButtons.OK);
		}

		/// <summary>
		/// Displays a Vista-like message box with specified text, caption, and buttons.
		/// </summary>
		/// <param name="text">The text to display in the Vista-like message box.</param>
		/// <param name="caption">The text to display in the title bar of the Vista-like message box.</param>
		/// <param name="buttons">One of the MessageBoxButtons values that specifies which buttons to display in the Vista-like message box.</param>
		/// <returns>One of the DialogResult values.</returns>
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons)
		{
			return Show(DefaultOwnerWindow, text, caption, buttons);
		}

		/// <summary>
		/// Displays a Vista-like message box in front of the specified object and with the specified text, caption, and buttons.
		/// </summary>
		/// <param name="owner">An implementation of IWin32Window that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the Vista-like message box.</param>
		/// <param name="caption">The text to display in the title bar of the Vista-like message box.</param>
		/// <param name="buttons">One of the MessageBoxButtons values that specifies which buttons to display in the Vista-like message box.</param>
		/// <returns>One of the DialogResult values.</returns>
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons)
		{
			return Show(owner, text, caption, buttons, MessageBoxIcon.None);
		}

		/// <summary>
		/// Displays a Vista-like message box with specified text, caption, buttons, and icon.
		/// </summary>
		/// <param name="text">The text to display in the Vista-like message box.</param>
		/// <param name="caption">The text to display in the title bar of the Vista-like message box.</param>
		/// <param name="buttons">One of the MessageBoxButtons values that specifies which buttons to display in the Vista-like message box.</param>
		/// <param name="icon">One of the MessageBoxIcon values that specifies which icon to display in the Vista-like message box.</param>
		/// <returns>One of the DialogResult values.</returns>
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return Show(DefaultOwnerWindow, text, caption, buttons, icon, MessageBoxDefaultButton.Button1);
		}

		/// <summary>
		/// Displays a Vista-like message box in front of the specified object and with the specified text, caption, buttons, and icon.
		/// </summary>
		/// <param name="owner">An implementation of IWin32Window that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the Vista-like message box.</param>
		/// <param name="caption">The text to display in the title bar of the Vista-like message box.</param>
		/// <param name="buttons">One of the MessageBoxButtons values that specifies which buttons to display in the Vista-like message box.</param>
		/// <param name="icon">One of the MessageBoxIcon values that specifies which icon to display in the Vista-like message box.</param>
		/// <returns>One of the DialogResult values.</returns>
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon)
		{
			return Show(owner, text, caption, buttons, icon, MessageBoxDefaultButton.Button1);
		}

		/// <summary>
		/// Displays a Vista-like message box with specified text, caption, buttons, icon, and default button.
		/// </summary>
		/// <param name="text">The text to display in the Vista-like message box.</param>
		/// <param name="caption">The text to display in the title bar of the Vista-like message box.</param>
		/// <param name="buttons">One of the MessageBoxButtons values that specifies which buttons to display in the Vista-like message box.</param>
		/// <param name="icon">One of the MessageBoxIcon values that specifies which icon to display in the Vista-like message box.</param>
		/// <param name="defaultButton">One of the MessageBoxDefaultButton values that specifies the default button for the Vista-like message box.</param>
		/// <returns>One of the DialogResult values.</returns>
		public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			return Show(DefaultOwnerWindow, text, caption, buttons, icon, defaultButton);
		}

		/// <summary>
		/// Displays a Vista-like message box in front of the specified object and with specified text, caption, buttons, icon, and default button.
		/// </summary>
		/// <param name="owner">An implementation of IWin32Window that will own the modal dialog box.</param>
		/// <param name="text">The text to display in the Vista-like message box.</param>
		/// <param name="caption">The text to display in the title bar of the Vista-like message box.</param>
		/// <param name="buttons">One of the MessageBoxButtons values that specifies which buttons to display in the Vista-like message box.</param>
		/// <param name="icon">One of the MessageBoxIcon values that specifies which icon to display in the Vista-like message box.</param>
		/// <param name="defaultButton">One of the MessageBoxDefaultButton values that specifies the default button for the Vista-like message box.</param>
		/// <returns>One of the DialogResult values.</returns>
		public static DialogResult Show(IWin32Window owner, string text, string caption, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
		{
			TaskDialog vd = new TaskDialog();
			vd.Content = text;
			vd.WindowTitle = caption;
			switch (buttons)
			{
				case MessageBoxButtons.AbortRetryIgnore:
					vd.Buttons = new TaskDialogButton[] { new TaskDialogButton(TaskDialogResult.Abort), new TaskDialogButton(TaskDialogResult.Retry), new TaskDialogButton(TaskDialogResult.Ignore) };
					break;
				case MessageBoxButtons.OKCancel:
					vd.Buttons = new TaskDialogButton[] { new TaskDialogButton(TaskDialogResult.OK), new TaskDialogButton(TaskDialogResult.Cancel) };
					break;
				case MessageBoxButtons.RetryCancel:
					vd.Buttons = new TaskDialogButton[] { new TaskDialogButton(TaskDialogResult.Retry), new TaskDialogButton(TaskDialogResult.Cancel) };
					break;
				case MessageBoxButtons.YesNo:
					vd.Buttons = new TaskDialogButton[] { new TaskDialogButton(TaskDialogResult.Yes), new TaskDialogButton(TaskDialogResult.No) };
					break;
				case MessageBoxButtons.YesNoCancel:
					vd.Buttons = new TaskDialogButton[] { new TaskDialogButton(TaskDialogResult.Yes), new TaskDialogButton(TaskDialogResult.No), new TaskDialogButton(TaskDialogResult.Cancel) };
					break;
				default:
					vd.Buttons = new TaskDialogButton[] { new TaskDialogButton(TaskDialogResult.OK) };
					break;
			}
			switch (icon)
			{
				case MessageBoxIcon.Information:
					vd.MainIcon = TaskDialogIcon.Information;
					break;
				case MessageBoxIcon.Warning:
					vd.MainIcon = TaskDialogIcon.Warning;
					break;
				case MessageBoxIcon.Error:
					vd.MainIcon = TaskDialogIcon.Error;
					break;
				case MessageBoxIcon.Question:
					vd.MainIcon = TaskDialogIcon.Question;
					break;
				default:
					vd.CustomMainIcon = null;
					break;
			}
			vd.DefaultButton = TaskDialogHelpers.MakeTaskDialogDefaultButton(defaultButton);
			if (owner is Form && (owner as Form).IsDisposed)
			{
				owner = DefaultOwnerWindow;
			}
			vd.Owner = owner;
			vd.Show();
			return TaskDialogHelpers.MakeDialogResult(vd.Result);
		}

		private static void SetStartPosition(Form f, IWin32Window o)
		{
			if (o == null)
			{
				f.StartPosition = FormStartPosition.CenterScreen;
			}
			else
			{
				f.StartPosition = FormStartPosition.CenterParent;
			}
		}

		#endregion

		#region TaskDialog

		#region Methods

		/// <summary>
		/// Shows the TaskDialog form as a modal dialog box.
		/// </summary>
		/// <returns>One of the TaskDialogResult values.</returns>
		public TaskDialogResult Show()
		{
			if (LockSystem)
			{
				return LockSystemAndShow();
			}
			else
			{
				return ShowInternal(Owner);
			}
		}

		private TaskDialogResult ShowInternal(IWin32Window owner)
		{
			using (var vdf = new TaskDialogForm())
			{
				_vdf = vdf;
				vdf.LinkClicked += vdf_LinkClicked;
				vdf.Load += vdf_Load;
				vdf.RightToLeft = RightToLeft;
				vdf.RightToLeftLayout = RightToLeftLayout;
				SetStartPosition(_vdf, owner);

				// «∑Ò÷√∂•£ø
				try
				{
					var phwnd = owner == null ? UnsafeNativeMethods.GetActiveWindow() : owner.Handle;
					if (phwnd != IntPtr.Zero)
						vdf.TopMost = UnsafeNativeMethods.IsTopMost(phwnd);
				}
				catch (Exception ex)
				{

				}

				vdf.ShowDialog(owner);
				_vdf = null;
				VerificationFlagChecked = vdf.CheckBoxState;
				Result = vdf.Tag is TaskDialogButton ? ((TaskDialogButton)vdf.Tag).Result
													 : (TaskDialogResult)(-1); // TaskDialog force-closed by exiting app
				return Result;
			}
		}

		private void vdf_LinkClicked(System.Object sender, LinkLabelLinkClickedEventArgs e)
		{
			if (LinkClicked != null)
			{
				LinkClicked(sender, e);
			}
		}

		private void vdf_Load(object sender, EventArgs e)
		{
			_vdf.SuspendLayouts();
			_vdf.Content = Content;
			if (_contentLink != null && _contentLink != _vdf.LabelContent)
			{
				foreach (LinkLabel.Link link in ContentLinks)
				{
					_vdf.LabelContent.Links.Add(link);
				}
				_contentLink.Dispose();
				_contentLink = _vdf.LabelContent;
			}
			_vdf.Caption = WindowTitle;
			_vdf.Title = MainInstruction;
			_vdf.VButtons = Buttons;
			_vdf.MainIcon = MainIcon;
			if (CustomMainIcon != null)
				_vdf.Image = CustomMainIcon;
			_vdf.FooterIcon = FooterIcon;
			if (CustomFooterIcon != null)
				_vdf.FooterImage = CustomFooterIcon;
			_vdf.FooterText = FooterText;
			if (_footer != null && _footer != _vdf.LabelFooter)
			{
				foreach (LinkLabel.Link link in FooterLinks)
				{
					_vdf.LabelFooter.Links.Add(link);
				}
				_footer.Dispose();
				_footer = _vdf.LabelFooter;
			}
			_vdf.DefaultButton = DefaultButton;
			_vdf.CheckBoxState = VerificationFlagChecked;
			_vdf.CheckBoxText = VerificationText;
			_vdf.ExpandFooterArea = ExpandFooterArea;
			_vdf.ExpandedInformation = ExpandedInformation;
			if (_expanded != null && _expanded != (ExpandFooterArea ? _vdf.LabelExpandedFooter : _vdf.LabelExpandedContent))
			{
				foreach (LinkLabel.Link link in ExpandedInformationLinks)
				{
					if (ExpandFooterArea)
						_vdf.LabelExpandedFooter.Links.Add(link);
					else
						_vdf.LabelExpandedContent.Links.Add(link);
				}
				_expanded.Dispose();
				_expanded = ExpandFooterArea
							? _vdf.LabelExpandedFooter
							: _vdf.LabelExpandedContent;
			}
			_vdf.Expanded = ExpandedByDefault;
			_vdf.ExpandedControlText = ExpandedControlText;
			_vdf.CollapsedControlText = CollapsedControlText;
			_vdf.CustomControl = CustomControl;
			if (Sound != null)
				_vdf.Sound = Sound;
			_vdf.ResumeLayouts();
		}

		private TaskDialogForm _vdf;

		/// <summary>
		/// Closes the TaskDialog form.
		/// </summary>
		/// <param name="result">A TaskDialogResult that represents the result of the form.</param>
		public void Close(TaskDialogResult result)
		{
			if (_vdf == null)
			{
				throw new InvalidOperationException("Cannot invoke this method before the dialog is shown, or after it is closed.");
			}
			var button = new TaskDialogButton(result);
			_vdf.Tag = button;
			if (button.Result != TaskDialogResult.None)
			{
				_vdf.DialogResult = DialogResult.OK;
			}
		}

		#endregion

		#region Fields and Properties

		public static float _customScale = 1f;
		public static float CustomScale
		{
			get { return _customScale; }
			set
			{
				_customScale = value;
			}
		}

		private SystemSound _sound;
		/// <summary>
		/// Gets or sets a sound played when the Vista-like task dialog is shown.
		/// </summary>
		public SystemSound Sound
		{
			get
			{
				if (_lockSystem && _sound == null)
				{
					return SoundScheme.WindowsUAC;
				}
				return _sound;
			}
			set { _sound = value; }
		}

		private IWin32Window _owner;
		/// <summary>
		/// Gets or sets an implementation of IWin32Window that will own the modal task dialog.
		/// </summary>
		public IWin32Window Owner
		{
			get { return _owner; }
			set { _owner = value; }
		}

		private string _content;
		/// <summary>
		/// Gets or sets the text to display in the Vista-like task dialog.
		/// </summary>
		public string Content
		{
			get { return _content; }
			set { _content = value; }
		}

		private LinkLabel _contentLink;
		/// <summary>
		/// The collection of links contained within the Content LinkLabel.
		/// </summary>
		public LinkLabel.LinkCollection ContentLinks
		{
			get
			{
				if (_contentLink == null)
				{
					if (_vdf != null)
					{
						_contentLink = _vdf.LabelContent;
					}
					else
					{
						_contentLink = new LinkLabel();
					}
				}
				return _contentLink.Links;
			}
		}

		private string _windowTitle;
		/// <summary>
		/// Gets or sets the text to display in the title bar of the Vista-like task dialog.
		/// </summary>
		public string WindowTitle
		{
			get { return _windowTitle; }
			set { _windowTitle = value; }
		}

		private string _mainInstruction;
		/// <summary>
		/// Gets or sets the text to be used for the main instruction of the Vista-like task dialog.
		/// </summary>
		public string MainInstruction
		{
			get { return _mainInstruction; }
			set { _mainInstruction = value; }
		}

		private TaskDialogButton[] _vButtons;
		/// <summary>
		/// Gets or sets the array of the TaskDialogButtons that specifies which buttons to display in the Vista-like task dialog.
		/// </summary>
		public TaskDialogButton[] Buttons
		{
			get { return _vButtons; }
			set { _vButtons = value; }
		}

		private TaskDialogIcon _mainIcon;
		/// <summary>
		/// Gets or sets the one of the TaskDialogIcon values that specifies which icon on what background to display in the Vista-like task dialog.
		/// </summary>
		public TaskDialogIcon MainIcon
		{
			get { return _mainIcon; }
			set { _mainIcon = value; }
		}

		private Image _customMainIcon;
		/// <summary>
		/// Gets or sets the custom image to display in the Vista-like task dialog.
		/// </summary>
		public Image CustomMainIcon
		{
			get { return _customMainIcon; }
			set { _customMainIcon = value; }
		}

		private TaskDialogIcon _footerIcon;
		/// <summary>
		/// Gets or sets the one of the TaskDialogIcon values that specifies which icon to display in the footer of the Vista-like task dialog.
		/// </summary>
		public TaskDialogIcon FooterIcon
		{
			get { return _footerIcon; }
			set { _footerIcon = value; }
		}

		private Image _customFooterIcon;
		/// <summary>
		/// Gets or sets the custom image to display in the footer of the Vista-like task dialog.
		/// </summary>
		public Image CustomFooterIcon
		{
			get { return _customFooterIcon; }
			set { _customFooterIcon = value; }
		}

		private string _footerText;
		/// <summary>
		/// Gets or sets the text to display in the footer of the Vista-like task dialog.
		/// </summary>
		public string FooterText
		{
			get { return _footerText; }
			set { _footerText = value; }
		}

		private LinkLabel _footer;
		/// <summary>
		/// The collection of links contained within the Footer LinkLabel.
		/// </summary>
		public LinkLabel.LinkCollection FooterLinks
		{
			get
			{
				if (_footer == null)
				{
					if (_vdf != null)
					{
						_footer = _vdf.LabelFooter;
					}
					else
					{
						_footer = new LinkLabel();
					}
				}
				return _footer.Links;
			}
		}

		private TaskDialogDefaultButton _defaultButton = TaskDialogDefaultButton.Button1;
		/// <summary>
		/// Gets or sets one of the TaskDialogDefaultButton values that specifies the default button for the Vista-like task dialog.
		/// </summary>
		public TaskDialogDefaultButton DefaultButton
		{
			get { return _defaultButton; }
			set { _defaultButton = value; }
		}

		private RightToLeft _rightToLeft;
		/// <summary>
		/// Gets or sets a value indicating whether the text appears from right to left, such as when using Hebrew or Arabic fonts.
		/// </summary>
		public RightToLeft RightToLeft
		{
			get { return _rightToLeft; }
			set { _rightToLeft = value; }
		}

		private bool _rightToLeftLayout;
		/// <summary>
		/// Gets or sets a value indicating whether right-to-left mirror placement is turned on.
		/// </summary>
		public bool RightToLeftLayout
		{
			get { return _rightToLeftLayout; }
			set { _rightToLeftLayout = value; }
		}

		private TaskDialogResult _result;
		/// <summary>
		/// Gets or sets the dialog result for the TaskDialog form.
		/// </summary>
		public TaskDialogResult Result
		{
			get { return _result; }
			set { _result = value; }
		}

		private bool _lockSystem;
		/// <summary>
		/// Determines whether to lock system while showing the Vista-like task dialog.
		/// </summary>
		public bool LockSystem
		{
			get { return _lockSystem; }
			set { _lockSystem = value; }
		}

		private Control _control;
		/// <summary>
		/// Gets or sets the custom control to display in the Vista-like task dialog.
		/// </summary>
		public Control CustomControl
		{
			get { return _control; }
			set { _control = value; }
		}

		private CheckState _verificationFlagChecked;
		/// <summary>
		/// Gets or sets a value indicating whether the verification checkbox in the dialog should be checked when the dialog is initially displayed.
		/// </summary>
		public CheckState VerificationFlagChecked
		{
			get { return _verificationFlagChecked; }
			set { _verificationFlagChecked = value; }
		}

		private string _verificationText;
		/// <summary>
		/// Gets or sets the string to be used to label the verification checkbox. If this parameter is Nothing (null), the verification checkbox is not displayed in the Vista-like task dialog.
		/// </summary>
		public string VerificationText
		{
			get { return _verificationText; }
			set { _verificationText = value; }
		}

		private bool _expandFooterArea;
		/// <summary>
		/// Gets or sets a value indicating whether the the string specified by the ExpandedInformation property should be displayed at the bottom of the Vista-like task dialog's footer area instead of immediately after the Vista-like task dialog's content.
		/// </summary>
		public bool ExpandFooterArea
		{
			get { return _expandFooterArea; }
			set { _expandFooterArea = value; }
		}

		private bool _expandedByDefault;
		/// <summary>
		/// Gets or sets a value indicating whether the string specified by the ExpandedInformation property should be displayed when the Vista-like task dialog is initially displayed.
		/// </summary>
		public bool ExpandedByDefault
		{
			get { return _expandedByDefault; }
			set { _expandedByDefault = value; }
		}

		private string _expandedInformation;
		/// <summary>
		/// Gets or sets the string to be used for displaying additional information. The additional information is displayed either immediately below the content or below the footer text depending on whether the ExpandFooterArea property is set.
		/// </summary>
		public string ExpandedInformation
		{
			get { return _expandedInformation; }
			set { _expandedInformation = value; }
		}

		private LinkLabel _expanded;
		/// <summary>
		/// The collection of links contained within the Footer LinkLabel.
		/// </summary>
		public LinkLabel.LinkCollection ExpandedInformationLinks
		{
			get
			{
				if (_expanded == null)
				{
					if (_vdf != null)
					{
						_expanded = ExpandFooterArea
									? _vdf.LabelExpandedFooter
									: _vdf.LabelExpandedContent;
					}
					else
					{
						_expanded = new LinkLabel();
					}
				}
				return _expanded.Links;
			}
		}

		private string _expandedControlText;
		/// <summary>
		/// Gets or sets the string to be used to label the button for collapsing the expandable information.
		/// </summary>
		public string ExpandedControlText
		{
			get { return _expandedControlText; }
			set { _expandedControlText = value; }
		}

		private string _collapsedControlText;
		/// <summary>
		/// Gets or sets the string to be used to label the button for expanding the expandable information.
		/// </summary>
		public string CollapsedControlText
		{
			get { return _collapsedControlText; }
			set { _collapsedControlText = value; }
		}

		#endregion

		#region System Locking Methods

		private TaskDialogResult LockSystemAndShow()
		{
			Control owner = this.Owner as Control;
			Screen scr;
			if (owner == null)
			{
				scr = Screen.PrimaryScreen;
			}
			else
			{
				scr = Screen.FromControl(owner);
			}
			using (Bitmap background = new Bitmap(scr.Bounds.Width, scr.Bounds.Height))
			{
				using (Graphics g = Graphics.FromImage(background))
				{
					g.CopyFromScreen(0, 0, 0, 0, scr.Bounds.Size);
					using (Brush br = new SolidBrush(Color.FromArgb(192, Color.Black)))
					{
						g.FillRectangle(br, scr.Bounds);
					}

					if (owner != null)
					{
						Form form = owner.FindForm();
						g.CopyFromScreen(form.Location, form.Location, form.Size);
						using (Brush br = new SolidBrush(Color.FromArgb(128, Color.Black)))
						{
							g.FillRectangle(br, new Rectangle(form.Location, form.Size));
						}
					}
				}

				IntPtr originalThread;
				IntPtr originalInput;
				IntPtr newDesktop;

				originalThread = UnsafeNativeMethods.GetThreadDesktop((uint)Thread.CurrentThread.ManagedThreadId);
				originalInput = UnsafeNativeMethods.OpenInputDesktop(0, false, Consts.DESKTOP_SWITCHDESKTOP);

				newDesktop = UnsafeNativeMethods.CreateDesktop("Desktop" + Guid.NewGuid(), null, null, 0, Consts.GENERIC_ALL, IntPtr.Zero);
				UnsafeNativeMethods.SetThreadDesktop(newDesktop);
				UnsafeNativeMethods.SwitchDesktop(newDesktop);

				Thread newThread = new Thread(NewThreadMethod);
				newThread.CurrentCulture = Thread.CurrentThread.CurrentCulture;
				newThread.CurrentUICulture = Thread.CurrentThread.CurrentUICulture;
				newThread.Start(new TaskDialogLockSystemParameters(newDesktop, background));
				newThread.Join();

				UnsafeNativeMethods.SwitchDesktop(originalInput);
				UnsafeNativeMethods.SetThreadDesktop(originalThread);

				UnsafeNativeMethods.CloseDesktop(newDesktop);
				UnsafeNativeMethods.CloseDesktop(originalInput);

				return Result;
			}
		}

		private void NewThreadMethod(object @params)
		{
			TaskDialogLockSystemParameters p = (TaskDialogLockSystemParameters)@params;
			UnsafeNativeMethods.SetThreadDesktop(p.NewDesktop);
			using (Form f = new BackgroundForm(p.Background))
			{
				f.Show();
				ShowInternal(f);
				f.BackgroundImage = null;
				Application.DoEvents();
				Thread.Sleep(250);
			}
		}
		#endregion

		#region Events

		public event LinkLabelLinkClickedEventHandler LinkClicked;

		#endregion

		#endregion

		#region Custom Theming

		public static Func<IButtonControlWithImage> ButtonFactory = new Func<IButtonControlWithImage>(() => new Button() { AutoSizeMode = AutoSizeMode.GrowAndShrink, TextImageRelation = TextImageRelation.ImageBeforeText });

		internal static void OnFormConstructed(Form form)
		{
			FormConstructed(form, EventArgs.Empty);
		}

		public static event EventHandler FormConstructed = delegate { };

		#endregion
	}
}