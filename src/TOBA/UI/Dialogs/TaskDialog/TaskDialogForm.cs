#region License
// Copyright ?2013 kasz 寃i箃kowski
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
	using Media;

	using System;
	using System.Drawing;
	using System.Drawing.Drawing2D;
	using System.Windows.Forms;

	/// <summary>
	/// The TaskDialog form.
	/// </summary>
	internal partial class TaskDialogForm : Form
	{
		public TaskDialogForm()
		{
			// This call is required by the Windows Form Designer.
			InitializeComponent();

			// Add any initialization after the InitializeComponent() call.
			SuspendLayouts();

			Font = new Font("Segoe UI", 9f * TaskDialog.CustomScale, SystemFonts.MessageBoxFont.Style, GraphicsUnit.Point, SystemFonts.MessageBoxFont.GdiCharSet, SystemFonts.MessageBoxFont.GdiVerticalFont);
			LabelTitle.Font = new Font(Font.FontFamily, Font.Size * 1.5f, Font.Style, Font.Unit, Font.GdiCharSet, Font.GdiVerticalFont);

			PictureBoxIcon.Scale(new SizeF(TaskDialog.CustomScale, TaskDialog.CustomScale));

			//'TableLayoutPanel.RowStyles(2) = New RowStyle(SizeType.Absolute, 0.0!)
			//'TableLayoutPanel.RowStyles(3) = New RowStyle(SizeType.Absolute, 0.0!)

			//'TableLayoutPanelButtons.ColumnStyles(0) = New ColumnStyle(SizeType.Absolute, 0.0!)
			//'VButtons = Nothing
			//'FlowLayoutPanelButtonsLeft.Visible = False

			//'TableLayoutPanelContent.ColumnStyles(0) = New ColumnStyle(SizeType.Absolute, 0.0!)
			//'LabelIcon.Visible = False
			//'TableLayoutPanelContent.RowStyles(0) = New RowStyle(SizeType.Absolute, 0.0!)
			//'LabelTitle.Visible = False
			//'TableLayoutPanelContent.RowStyles(1) = New RowStyle(SizeType.Absolute, 0.0!) ' content
			//'LabelContent.Visible = False
			//'TableLayoutPanelContent.RowStyles(2) = New RowStyle(SizeType.Absolute, 0.0!) ' expanded content
			//'LabelExpandedContent.Visible = False
			//'TableLayoutPanelContent.RowStyles(3) = New RowStyle(SizeType.Absolute, 0.0!) ' reszta

			//'TableLayoutPanelContents.Visible = False
			//'CheckBox.Visible = False
			//'ButtonExpander.Visible = False

			//'TableLayoutPanelFooter.ColumnStyles(0) = New ColumnStyle(SizeType.Absolute, 0.0!)
			//'TableLayoutPanelExpanderFooter.ColumnStyles(0) = New ColumnStyle(SizeType.Absolute, 0.0!)

			TaskDialog.OnFormConstructed(this);

			ResumeLayouts();
		}

		public void SuspendLayouts()
		{
			this.TableLayoutPanelContent.SuspendLayout();
			this.PanelExpandedFooter.SuspendLayout();
			this.TableLayoutPanelExpanderFooter.SuspendLayout();
			this.PanelFooter.SuspendLayout();
			this.TableLayoutPanelFooter.SuspendLayout();
			this.PanelButtons.SuspendLayout();
			this.TableLayoutPanelButtons.SuspendLayout();
			this.FlowLayoutPanelButtons.SuspendLayout();
			this.FlowLayoutPanelButtonsLeft.SuspendLayout();
			this.TableLayoutPanel.SuspendLayout();
			this.SuspendLayout();
		}

		public void ResumeLayouts()
		{
			this.TableLayoutPanelContent.ResumeLayout(false);
			this.TableLayoutPanelContent.PerformLayout();
			this.PanelExpandedFooter.ResumeLayout(false);
			this.PanelExpandedFooter.PerformLayout();
			this.TableLayoutPanelExpanderFooter.ResumeLayout(false);
			this.TableLayoutPanelExpanderFooter.PerformLayout();
			this.PanelFooter.ResumeLayout(false);
			this.PanelFooter.PerformLayout();
			this.TableLayoutPanelFooter.ResumeLayout(false);
			this.TableLayoutPanelFooter.PerformLayout();
			this.PanelButtons.ResumeLayout(false);
			this.PanelButtons.PerformLayout();
			this.TableLayoutPanelButtons.ResumeLayout(false);
			this.TableLayoutPanelButtons.PerformLayout();
			this.FlowLayoutPanelButtons.ResumeLayout(false);
			this.FlowLayoutPanelButtons.PerformLayout();
			this.FlowLayoutPanelButtonsLeft.ResumeLayout(false);
			this.FlowLayoutPanelButtonsLeft.PerformLayout();
			this.TableLayoutPanel.ResumeLayout(false);
			this.TableLayoutPanel.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}

		//protected override System.Windows.Forms.CreateParams CreateParams
		//{
		//    get
		//    {
		//        CreateParams cp = base.CreateParams;
		//        if (!CanCancel)
		//        {
		//            cp.ClassStyle |= 0x200; // CS_NOCLOSE
		//        }
		//        return cp;
		//    }
		//}

		#region Fields and Properties

		private Control[] _buttons;
		private Control[] Buttons
		{
			get
			{
				if (_buttons == null)
				{
					_buttons = new[] { Button1, Button2, Button3, Button4, Button5, Button6, Button7, Button8, Button9, Button10, Button11 };
				}
				return _buttons;
			}
		}

		private SystemSound _sound;
		public SystemSound Sound
		{
			get
			{
				if (_sound != null)
				{
					return _sound;
				}
				return SoundScheme.Default;
			}
			set { _sound = value; }
		}

		public string Content
		{
			get { return LabelContent.Text; }
			set
			{
				LabelContent.Text = value;
				if (string.IsNullOrEmpty(value))
				{
					LabelContent.Visible = false;
					TableLayoutPanelContent.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
				}
				else
				{
					TableLayoutPanelContent.RowStyles[1] = new RowStyle(SizeType.AutoSize);
					LabelContent.Visible = true;
				}
			}
		}

		public string Title
		{
			get { return LabelTitle.Text; }
			set
			{
				LabelTitle.Text = value;
				if (string.IsNullOrEmpty(value))
				{
					LabelTitle.Visible = false;
					TableLayoutPanelContent.RowStyles[0] = new RowStyle(SizeType.Absolute, 0f);
				}
				else
				{
					TableLayoutPanelContent.RowStyles[0] = new RowStyle(SizeType.AutoSize);
					LabelTitle.Visible = true;
				}
			}
		}

		public string Caption
		{
			get { return Text; }
			set { Text = value; }
		}

		public string FooterText
		{
			get { return LabelFooter.Text; }
			set
			{
				LabelFooter.Text = value;
				if (value == null)
				{
					//TableLayoutPanelFooter.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0f);
					TableLayoutPanel.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
				}
				else
				{
					//TableLayoutPanelFooter.ColumnStyles[0] = new ColumnStyle(SizeType.AutoSize);
					TableLayoutPanel.RowStyles[2] = new RowStyle(SizeType.AutoSize);
				}
			}
		}

		private bool CanCancel;

		private TaskDialogButton[] _vButtons;
		public TaskDialogButton[] VButtons
		{
			get { return _vButtons; }
			set
			{
				if (value == null)
				{
					value = new TaskDialogButton[] { };
				}
				_vButtons = value;
				if (value.Length == 0)
				{
					TableLayoutPanelButtons.ColumnStyles[1] = new ColumnStyle(SizeType.Absolute, 0f);
					if (TableLayoutPanelButtons.ColumnStyles[0].SizeType == SizeType.Absolute)
					{
						TableLayoutPanel.RowStyles[1] = new RowStyle(SizeType.Absolute, 0f);
					}
				}
				else
				{
					TableLayoutPanel.RowStyles[1] = new RowStyle(SizeType.AutoSize);
					TableLayoutPanelButtons.ColumnStyles[1] = new ColumnStyle(SizeType.AutoSize);
				}
				for (int i = 0; i < Buttons.Length; i++)
				{
					Control button = this.Buttons[i];
					button.Visible = i < value.Length;
					if (i < value.Length)
					{
						if (value[i].Result == TaskDialogResult.Cancel)
						{
							this.CanCancel = true;
							this.CancelButton = (IButtonControl)this.Buttons[i];
						}
						if (value[i].UseCustomText)
						{
							button.Text = value[i].Text;
						}
						else
						{
							button.Text = TaskDialogHelpers.GetButtonName(value[i].Result);
						}
						if (value[i].ShowElevationIcon)
						{
							((IButtonControlWithImage)button).Image = Properties.Resources.SmallSecurity;
						}
						else
						{
							((IButtonControlWithImage)button).Image = null;
						}
						button.Enabled = value[i].IsEnabled;
						button.Tag = value[i];
					}
				}
				if (value.Length == 1)
				{
					CanCancel = true;
					CancelButton = (IButtonControl)Buttons[0];
					AcceptButton = (IButtonControl)Buttons[0];
				}
			}
		}

		public Image Image
		{
			get { return PictureBoxIcon.Image; }
			set
			{
				PictureBoxIcon.Image = value;
				if (value == null)
				{
					//LabelIcon.Visible = False
					TableLayoutPanelContent.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0f);
				}
				else
				{
					TableLayoutPanelContent.ColumnStyles[0] = new ColumnStyle(SizeType.AutoSize);
					//LabelIcon.Visible = True
				}
			}
		}

		public Image FooterImage
		{
			get { return LabelIconFooter.Image; }
			set
			{
				LabelIconFooter.Image = value;
				if (value == null)
				{
					TableLayoutPanelFooter.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0f);
					LabelFooter.Margin = new Padding(16, LabelFooter.Margin.Top, LabelFooter.Margin.Right, LabelFooter.Margin.Bottom);
				}
				else
				{
					TableLayoutPanelFooter.ColumnStyles[0] = new ColumnStyle(SizeType.AutoSize);
					LabelFooter.Margin = new Padding(4, LabelFooter.Margin.Top, LabelFooter.Margin.Right, LabelFooter.Margin.Bottom);
				}
			}
		}

		private bool _drawGradient;
		private Color _gradientBegin;
		private Color _gradientEnd;

		private TaskDialogIcon _mainIcon;
		public TaskDialogIcon MainIcon
		{
			get { return _mainIcon; }
			set
			{
				_mainIcon = value;
				TableLayoutPanelContent.SetRowSpan(PictureBoxIcon, 4);
				LabelTitle.ForeColor = SystemColors.HotTrack;
				_drawGradient = false;
				Sound = SoundScheme.Default;

				switch (_mainIcon)
				{
					case TaskDialogIcon.Information:
						Image = TaskDialog.CustomScale > 1 ? TaskDialogBigIcon.Information : TaskDialogMediumIcon.Information;
						Sound = SoundScheme.Information;
						break;
					case TaskDialogIcon.Question:
						Image = TaskDialog.CustomScale > 1 ? TaskDialogBigIcon.Question : TaskDialogMediumIcon.Question;
						Sound = SoundScheme.Question;
						break;

					case TaskDialogIcon.Warning:
						Image = TaskDialog.CustomScale > 1 ? TaskDialogBigIcon.Warning : TaskDialogMediumIcon.Warning;
						Sound = SoundScheme.Warning;
						break;

					case TaskDialogIcon.Error:
						Image = TaskDialog.CustomScale > 1 ? TaskDialogBigIcon.Error : TaskDialogMediumIcon.Error;
						Sound = SoundScheme.Error;
						break;

					case TaskDialogIcon.SecuritySuccess:
						TableLayoutPanelContent.SetRowSpan(PictureBoxIcon, 1);
						Image = TaskDialog.CustomScale > 1 ? TaskDialogBigIcon.SecuritySuccess : TaskDialogMediumIcon.SecuritySuccess;
						_drawGradient = true;
						_gradientBegin = Color.FromArgb(21, 118, 21);
						_gradientEnd = Color.FromArgb(57, 150, 63);
						LabelTitle.ForeColor = Color.White;
						Sound = SoundScheme.Information;
						break;

					case TaskDialogIcon.SecurityQuestion:
						TableLayoutPanelContent.SetRowSpan(PictureBoxIcon, 1);
						Image = TaskDialog.CustomScale > 1 ? TaskDialogBigIcon.SecurityQuestion : TaskDialogMediumIcon.SecurityQuestion;
						_drawGradient = true;
						_gradientBegin = Color.FromArgb(0, 177, 242);
						_gradientEnd = Color.FromArgb(72, 205, 254);
						LabelTitle.ForeColor = Color.White;
						Sound = SoundScheme.Question;
						break;

					case TaskDialogIcon.SecurityWarning:
						TableLayoutPanelContent.SetRowSpan(PictureBoxIcon, 1);
						Image = TaskDialog.CustomScale > 1 ? TaskDialogBigIcon.SecurityWarning : TaskDialogMediumIcon.SecurityWarning;
						_drawGradient = true;
						_gradientBegin = Color.FromArgb(242, 177, 0);
						_gradientEnd = Color.FromArgb(254, 205, 72);
						LabelTitle.ForeColor = Color.Black;
						Sound = SoundScheme.Warning;
						break;

					case TaskDialogIcon.SecurityError:
						TableLayoutPanelContent.SetRowSpan(PictureBoxIcon, 1);
						Image = TaskDialog.CustomScale > 1 ? TaskDialogBigIcon.SecurityError : TaskDialogMediumIcon.SecurityError;
						_drawGradient = true;
						_gradientBegin = Color.FromArgb(172, 1, 0);
						_gradientEnd = Color.FromArgb(227, 1, 0);
						LabelTitle.ForeColor = Color.White;
						Sound = SoundScheme.Error;
						break;

					case TaskDialogIcon.SecurityShield:
						Image = TaskDialog.CustomScale > 1 ? TaskDialogBigIcon.Security : TaskDialogMediumIcon.Security;
						Sound = SoundScheme.Security;
						break;

					case TaskDialogIcon.SecurityShieldBlue:
						TableLayoutPanelContent.SetRowSpan(PictureBoxIcon, 1);
						Image = TaskDialog.CustomScale > 1 ? TaskDialogBigIcon.Security : TaskDialogMediumIcon.Security;
						_drawGradient = true;
						_gradientBegin = Color.FromArgb(4, 80, 130);
						_gradientEnd = Color.FromArgb(28, 120, 133);
						LabelTitle.ForeColor = Color.White;
						Sound = SoundScheme.Security;
						break;

					case TaskDialogIcon.SecurityShieldGray:
						TableLayoutPanelContent.SetRowSpan(PictureBoxIcon, 1);
						Image = TaskDialog.CustomScale > 1 ? TaskDialogBigIcon.Security : TaskDialogMediumIcon.Security;
						_drawGradient = true;
						_gradientBegin = Color.FromArgb(157, 143, 133);
						_gradientEnd = Color.FromArgb(164, 152, 144);
						LabelTitle.ForeColor = Color.White;
						Sound = SoundScheme.Security;
						break;

					default:
						Image = null;
						break;
				}
			}
		}

		private TaskDialogIcon _footerIcon;
		public TaskDialogIcon FooterIcon
		{
			get { return _footerIcon; }
			set
			{
				_footerIcon = value;
				switch (_footerIcon)
				{
					case TaskDialogIcon.Information:
						FooterImage = TaskDialogSmallIcon.Information;
						break;
					case TaskDialogIcon.Question:
						FooterImage = TaskDialogSmallIcon.Question;
						break;
					case TaskDialogIcon.Warning:
						FooterImage = TaskDialogSmallIcon.Warning;
						break;
					case TaskDialogIcon.Error:
						FooterImage = TaskDialogSmallIcon.Error;
						break;
					case TaskDialogIcon.SecuritySuccess:
						FooterImage = TaskDialogSmallIcon.SecuritySuccess;
						break;
					case TaskDialogIcon.SecurityQuestion:
						FooterImage = TaskDialogSmallIcon.SecurityQuestion;
						break;
					case TaskDialogIcon.SecurityWarning:
						FooterImage = TaskDialogSmallIcon.SecurityWarning;
						break;
					case TaskDialogIcon.SecurityError:
						FooterImage = TaskDialogSmallIcon.SecurityError;
						break;
					case TaskDialogIcon.SecurityShield:
						FooterImage = TaskDialogSmallIcon.Security;
						break;
					case TaskDialogIcon.SecurityShieldBlue:
						FooterImage = TaskDialogSmallIcon.Security;
						break;
					case TaskDialogIcon.SecurityShieldGray:
						FooterImage = TaskDialogSmallIcon.Security;
						break;
					default:
						FooterImage = null;
						break;
				}
			}
		}

		private TaskDialogDefaultButton _defaultButton;
		public TaskDialogDefaultButton DefaultButton
		{
			get { return _defaultButton; }
			set { _defaultButton = value; }
		}

		private bool ShowCheckBox
		{
			get { return CheckBox.Visible; }
			set
			{
				if (value)
				{
					TableLayoutPanelButtons.ColumnStyles[0] = new ColumnStyle(SizeType.AutoSize);
					FlowLayoutPanelButtonsLeft.Visible = true;
					CheckBox.Visible = true;
				}
				else
				{
					CheckBox.Visible = false;
					if (!ShowButtonExpander)
					{
						FlowLayoutPanelButtonsLeft.Visible = false;
						TableLayoutPanelButtons.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0f);
					}
				}
			}
		}

		public bool ShowButtonExpander
		{
			get { return ButtonExpander.Visible; }
			set
			{
				if (value)
				{
					SetExpanderText();
					TableLayoutPanelButtons.ColumnStyles[0] = new ColumnStyle(SizeType.AutoSize);
					FlowLayoutPanelButtonsLeft.Visible = true;
					ButtonExpander.Visible = true;
				}
				else
				{
					ButtonExpander.Visible = false;
					if (!ShowCheckBox)
					{
						FlowLayoutPanelButtonsLeft.Visible = false;
						TableLayoutPanelButtons.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0f);
					}
				}
			}
		}

		public string ExpandedInformation
		{
			get { return LabelExpandedContent.Text; }
			set
			{
				LabelExpandedContent.Text = value;
				LabelExpandedFooter.Text = value;
				LabelExpandedContent.Visible = !string.IsNullOrEmpty(value);
				LabelExpandedFooter.Visible = !string.IsNullOrEmpty(value);
				ShowButtonExpander = !string.IsNullOrEmpty(value);
				Expanded = !string.IsNullOrEmpty(value);
			}
		}

		private bool _expandFooterArea;
		public bool ExpandFooterArea
		{
			get { return _expandFooterArea; }
			set { _expandFooterArea = value; }
		}

		private bool _expanded;
		public bool Expanded
		{
			get { return _expanded; }
			set
			{
				_expanded = value;
				ButtonExpander.Expanded = value;
				SetExpanderText();
				if (ExpandFooterArea)
				{
					TableLayoutPanelContent.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
					LabelExpandedContent.Visible = false;
					if (!value)
					{
						TableLayoutPanelExpanderFooter.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0f);
						TableLayoutPanel.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);
					}
					else
					{
						TableLayoutPanel.RowStyles[3] = new RowStyle(SizeType.AutoSize);
						TableLayoutPanelExpanderFooter.ColumnStyles[0] = new ColumnStyle(SizeType.AutoSize);
					}
				}
				else
				{
					TableLayoutPanelExpanderFooter.ColumnStyles[0] = new ColumnStyle(SizeType.Absolute, 0f);
					TableLayoutPanel.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);
					if (value)
					{
						LabelExpandedContent.Visible = true;
						TableLayoutPanelContent.RowStyles[2] = new RowStyle(SizeType.AutoSize);
					}
					else
					{
						TableLayoutPanelContent.RowStyles[2] = new RowStyle(SizeType.Absolute, 0f);
						LabelExpandedContent.Visible = false;
					}
				}
				UpdateLabelContentMargin();
			}
		}

		private void SetExpanderText()
		{
			if (Expanded)
			{
				ButtonExpander.Text = ExpandedControlText;
			}
			else
			{
				ButtonExpander.Text = CollapsedControlText;
			}
		}

		private string _expandedControlText;
		public string ExpandedControlText
		{
			get
			{
				if (string.IsNullOrEmpty(_expandedControlText))
					return "展开";
				return _expandedControlText;
			}
			set
			{
				_expandedControlText = value;
				SetExpanderText();
			}
		}

		private string _collapsedControlText;
		public string CollapsedControlText
		{
			get
			{
				if (string.IsNullOrEmpty(_collapsedControlText))
					return "收起";
				return _collapsedControlText;
			}
			set
			{
				_collapsedControlText = value;
				SetExpanderText();
			}
		}

		public CheckState CheckBoxState
		{
			get { return CheckBox.CheckState; }
			set { CheckBox.CheckState = value; }
		}

		public string CheckBoxText
		{
			get { return CheckBox.Text; }
			set
			{
				CheckBox.Text = value;
				ShowCheckBox = !string.IsNullOrEmpty(value);
			}
		}

		private Control _control;
		public Control CustomControl
		{
			get { return _control; }
			set
			{
				this.TableLayoutPanelContents.Controls.Clear();
				if (value == null)
				{
					TableLayoutPanelContents.Visible = false;
					TableLayoutPanelContent.RowStyles[3] = new RowStyle(SizeType.Absolute, 0f);
				}
				else
				{
					this.TableLayoutPanelContents.Controls.Add(value, 0, 0);
					value.Dock = DockStyle.Fill;
					TableLayoutPanelContent.RowStyles[3] = new RowStyle(SizeType.AutoSize);
					TableLayoutPanelContents.Visible = true;
				}
				_control = value;
				UpdateLabelContentMargin();
			}
		}
		#endregion

		private void TaskDialogForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			//Visible = False
			if (this.Tag == null)
			{
				if (CancelButton == null || !(CancelButton is Dialogs.TaskDialog.Button) || (CancelButton as Dialogs.TaskDialog.Button).Tag == null)
				{
					this.Tag = new TaskDialogButton(TaskDialogResult.Cancel);
				}
				else
				{
					this.Tag = (TaskDialogButton)(CancelButton as Dialogs.TaskDialog.Button).Tag;
				}
			}
		}

		private void TaskDialog_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (_control != null)
			{
				this.TableLayoutPanelContents.Controls.Remove(_control);
				_control = null;
			}
		}

		protected override void OnLoad(System.EventArgs e)
		{
			base.OnLoad(e);
			borderSize = Size - ClientSize;

			//'Tag = Nothing
			//'Dim i As Integer = DirectCast(DefaultButton, Integer)
			//'If i > 0 AndAlso i <= VButtons.Length Then
			//'    AcceptButton = Buttons(i - 1)
			//'    Buttons(i - 1).Focus()
			//'Else
			//'    LabelContent.Focus()
			//'End If
			if (LabelTitle.Visible && (LabelContent.Visible || CustomControl != null))
			{
				LabelTitle.Margin = new Padding(LabelTitle.Margin.Left, LabelTitle.Margin.Top, LabelTitle.Margin.Right, 0);
				UpdateLabelContentMargin();
			}

			if (!string.IsNullOrEmpty(ExpandedInformation) && Expanded)
			{
				Expanded = true;
			}

			MakeCenter();

			//'ResumeLayouts()

			//'If Sound IsNot Nothing Then
			//'    Sound.Play()
			//'End If
		}

		private Size borderSize;

		public override Size GetPreferredSize(Size proposedSize)
		{
			Size size = this.TableLayoutPanel.GetPreferredSize(proposedSize);
			size.Width = Math.Max(size.Width, MinimumSize.Width) + borderSize.Width;
			size.Height = Math.Max(size.Height, MinimumSize.Height) + borderSize.Height;
			return size;
		}

		private void UpdateLabelContentMargin()
		{
			LabelContent.Margin = new Padding(LabelContent.Margin.Left, _drawGradient ? 16 : 8, LabelContent.Margin.Right, (!LabelExpandedContent.Visible || string.IsNullOrEmpty(LabelExpandedContent.Text)) && CustomControl == null ? 16 : 8);
			LabelExpandedContent.Margin = new Padding(LabelExpandedContent.Margin.Left, LabelExpandedContent.Visible ? LabelExpandedContent.Margin.Top : 0, LabelExpandedContent.Margin.Right, LabelExpandedContent.Visible || CustomControl != null ? LabelExpandedContent.Margin.Bottom : 16);
		}

		protected override void OnShown(System.EventArgs e)
		{
			//SuspendLayouts()
			Tag = null;
			int i = (int)DefaultButton;
			if (i > 0 && i <= VButtons.Length)
			{
				AcceptButton = (IButtonControl)Buttons[i - 1];
				Buttons[i - 1].Focus();
			}
			else
			{
				LabelContent.Focus();
			}
			//'If LabelTitle.Visible AndAlso LabelContent.Visible Then
			//'    LabelTitle.Margin = New Padding(LabelTitle.Margin.Left, LabelTitle.Margin.Top, LabelTitle.Margin.Right, 0)
			//'    LabelContent.Margin = New Padding(LabelContent.Margin.Left, If(_drawGradient, 16, LabelContent.Margin.Top), LabelContent.Margin.Right, 16)
			//'End If

			//'If Not String.IsNullOrEmpty(ExpandedInformation) AndAlso Expanded Then
			//'    Expanded = True
			//'End If

			//'MakeCenter()

			//'ResumeLayouts()

			if (Sound != null)
			{
				Sound.Play();
			}

			Activate();

			base.OnShown(e);
		}

		private void Button_Click(object sender, System.EventArgs e)
		{
			var button = sender as Control;
			TaskDialogButton vButton = (TaskDialogButton)button.Tag;
			Tag = vButton;
			vButton.RaiseClickEvent(sender, e);
			if (vButton.Result != TaskDialogResult.None)
			{
				DialogResult = DialogResult.OK;
			}
		}

		private void TableLayoutPanelContent_CellPaint(System.Object sender, System.Windows.Forms.TableLayoutCellPaintEventArgs e)
		{
			if (_drawGradient && e.Row == 0 && e.Column == 1)
			{
				Rectangle bounds = new Rectangle(0, 0, TableLayoutPanel.Width, e.CellBounds.Height);
				using (var b = new LinearGradientBrush(bounds, _gradientBegin, _gradientEnd, LinearGradientMode.Horizontal))
				{
					e.Graphics.FillRectangle(b, bounds);
				}
			}
		}

		private void PictureBoxIcon_Paint(System.Object sender, System.Windows.Forms.PaintEventArgs e)
		{
			if (_drawGradient)
			{
				Rectangle bounds = new Rectangle(0, 0, TableLayoutPanel.Width, PictureBoxIcon.Height);
				using (var b = new LinearGradientBrush(bounds, _gradientBegin, _gradientEnd, LinearGradientMode.Horizontal))
				{
					bounds.X -= PictureBoxIcon.Left;
					e.Graphics.FillRectangle(b, bounds);
				}
				e.Graphics.DrawImage(PictureBoxIcon.Image, 0, 0);
			}
		}

		private void ButtonExpander_Click(System.Object sender, System.EventArgs e)
		{
			Expanded ^= true;
			MakeCenter();
		}

		private void RaiseLinkClicked(System.Object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (LinkClicked != null)
			{
				LinkClicked(sender, e);
			}
		}

		private void MakeCenter()
		{
			if (Owner == null)
			{
				CenterToScreen();
			}
			else
			{
				CenterToParent();
			}
		}

		public event LinkLabelLinkClickedEventHandler LinkClicked;
	}
}