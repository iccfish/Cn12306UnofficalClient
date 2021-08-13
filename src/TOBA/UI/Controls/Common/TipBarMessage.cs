namespace TOBA.UI.Controls.Common
{
	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;

	public partial class TipBarMessage : UserControl
	{
		public TipBarMessage()
		{
			InitializeComponent();
			this.Padding = new Padding(3);
			lblMessage.Links.RemoveAt(0);
			this.pb.Click += pb_Click;
			this.lblMessage.Click += pb_Click;
			lblMessage.LinkClicked += lblMessage_LinkClicked;
		}

		/// <summary>
		/// LINK点击
		/// </summary>
		public event EventHandler<LinkLabelLinkClickedEventArgs> LinkLabelClicked;


		/// <summary>
		/// 引发 <see cref="LinkLabelClicked" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnLinkLabelClicked(LinkLabelLinkClickedEventArgs ea)
		{
			var handler = LinkLabelClicked;
			if (handler != null)
				handler(this, ea);
		}


		void lblMessage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			OnLinkLabelClicked(e);
		}

		void pb_Click(object sender, EventArgs e)
		{
			OnClick(EventArgs.Empty);
		}

		#region 属性

		/// <summary>
		/// 返回链接集合
		/// </summary>
		public LinkLabel.LinkCollection LinkCollection
		{
			get { return lblMessage.Links; }
		}

		private Color _borderColor;
		/// <summary>
		/// 边框颜色
		/// </summary>
		public Color BorderColor
		{
			get { return _borderColor; }
			set
			{
				_borderColor = value;
				lblMessage.LinkColor = value;
				Invalidate();
			}
		}

		/// <summary>
		/// 获得或设置文字颜色
		/// </summary>
		public override System.Drawing.Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
				Invalidate();
			}
		}

		private int _borderThickness;
		/// <summary>
		/// 边框厚度
		/// </summary>
		public int BorderThickness
		{
			get { return _borderThickness; }
			set { _borderThickness = value; Invalidate(); }
		}


		/// <summary>
		/// 获得或设置显示的信息
		/// </summary>
		[Localizable(true), Browsable(true), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), Bindable(true)]
		public override string Text
		{
			get
			{
				return lblMessage.Text;
			}
			set
			{
				lblMessage.Text = value;
			}
		}

		protected override void OnClientSizeChanged(EventArgs e)
		{
			base.OnClientSizeChanged(e);

			//重新计算边距
			pb.Location = new Point(this.Padding.Left + 1, this.Padding.Top + 1);
			lblMessage.Location = new Point(this.Padding.Left + 5 + 16 + 1, this.Padding.Top + 1);
			lblMessage.Size = new Size(this.Width - this.Padding.Left - this.Padding.Right - 16 - 5 - 2, this.Height - 1 - this.Padding.Top - this.Padding.Bottom - 2);
		}

		/// <summary>
		/// 获得或设置图片
		/// </summary>
		public Image Image
		{
			get { return pb.Image; }
			set { pb.Image = value; }
		}

		/// <summary>
		/// 获得或设置标签的边距
		/// </summary>
		[DefaultValue(null)]
		public Padding? LabelMargin
		{
			get
			{
				var p = new Padding(lblMessage.Left, lblMessage.Top, Width - lblMessage.Width - lblMessage.Left, Height - lblMessage.Top - lblMessage.Height);

				if (p == new Padding(25, 4, 4, 5))
					return null;

				return p;
			}
			set
			{
				var p = value ?? new Padding(25, 4, 4, 5);
				lblMessage.Location = new Point(p.Left, p.Top);
				lblMessage.Size = new Size(Width - p.Left - p.Right, Height - p.Top - p.Bottom);
			}
		}

		#endregion

		#region 绘制方法

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);

			if (BorderThickness > 0) DrawBorder();
		}

		/// <summary>
		/// 绘制边框
		/// </summary>
		void DrawBorder()
		{
			var pen = new Pen(BorderColor, BorderThickness);
			var g = this.CreateGraphics();

			if (this.Dock == DockStyle.None || this.Dock == DockStyle.Bottom)
			{
				//顶部边框
				g.DrawLine(pen, 0, 0, this.Width - 1, 0);
			}
			if (this.Dock == DockStyle.None || this.Dock == DockStyle.Top)
			{
				//底部边框
				g.DrawLine(pen, 0, this.Height - 1, this.Width - 1, this.Height - 1);
			}
			if (this.Dock == DockStyle.None || this.Dock == DockStyle.Left)
			{
				//右侧边框
				g.DrawLine(pen, this.Width - 1, 0, this.Width - 1, this.Height - 1);
			}
			if (this.Dock == DockStyle.Right || this.Dock == DockStyle.None)
			{
				//左侧边框
				g.DrawLine(pen, 0, 0, 0, this.Height - 1);
			}
		}

		#endregion


		#region 公共方法

		public void ApplyColorSchema(ColorSchema cs)
		{
			ForeColor = cs.ForColor;
			BackColor = cs.BackColor;
			BorderColor = cs.BorderColor;

			Invalidate();
		}

		public void ApplyColorSchema(RowStyleType type)
		{
			ApplyColorSchema(ListViewResource.Style(type));
		}

		#endregion

	}
}
