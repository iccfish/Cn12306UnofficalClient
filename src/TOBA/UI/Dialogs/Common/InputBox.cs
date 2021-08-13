namespace TOBA.UI.Dialogs.Common
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	/// <summary>
	/// 输入提示框
	/// </summary>
	partial class InputBox : FormBase
	{
		public InputBox()
		{
			InitializeComponent();

			this.Shown += (s, e) => { txtEdit.SelectAll(); };
			btnOK.Click += (s, e) =>
			{
				if (!AllowBlank && string.IsNullOrEmpty(InputedText))
				{
					Information("内容不能为空，请重新填写。");
					return;
				}

				if (ValidateHandler != null && !ValidateHandler(InputedText)) return;

				DialogResult = DialogResult.OK;
				Close();
			};
			DialogResult = DialogResult.Cancel;
		}

		/// <summary>
		/// 获得或设置提示文字
		/// </summary>
		public string TipMessage
		{
			get
			{
				return lblTip.Text;
			}
			set
			{
				lblTip.Text = value;
			}
		}

		/// <summary>
		/// 获得或设置输入的内容
		/// </summary>
		public string InputedText
		{
			get
			{
				return txtEdit.Text;
			}
			set
			{
				txtEdit.Text = value;
			}
		}

		private bool _multiLine = false;

		/// <summary>
		/// 获得或设置是否是多行输入
		/// </summary>
		public bool MultiLine
		{
			get
			{
				return _multiLine;
			}
			set
			{
				if (_multiLine == value) return;

				if (value)
				{
					this.Size = new Size(this.Width, this.Height + 100);
					this.txtEdit.Multiline = true;
					this.txtEdit.ScrollBars = ScrollBars.Vertical;
					this.txtEdit.Size = new Size(this.txtEdit.Size.Width, this.txtEdit.Size.Height + 100);
					this.txtEdit.Location = new Point(this.txtEdit.Location.X, this.txtEdit.Location.Y - 100);
				}
				else
				{
					this.Size = new Size(this.Width, this.Height - 100);
					this.txtEdit.Multiline = false;
					this.txtEdit.ScrollBars = ScrollBars.None;
					this.txtEdit.Size = new Size(this.txtEdit.Size.Width, this.txtEdit.Size.Height - 100);
					this.txtEdit.Location = new Point(this.txtEdit.Location.X, this.txtEdit.Location.Y + 100);
				}

				_multiLine = value;
			}
		}

		/// <summary>
		/// 是否允许空字符串
		/// </summary>
		public bool AllowBlank { get; set; }

		/// <summary>
		/// 测试值函数
		/// </summary>
		public Func<string, bool> ValidateHandler { get; set; }
	}
}
