namespace TOBA.UI.Controls.Misc
{
	partial class ForumNews
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.plist = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lnkRefresh = new System.Windows.Forms.LinkLabel();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.label1 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.loading1 = new Loading();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// plist
			// 
			this.plist.AutoScroll = true;
			this.plist.Dock = System.Windows.Forms.DockStyle.Fill;
			this.plist.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.plist.Location = new System.Drawing.Point(0, 36);
			this.plist.Name = "plist";
			this.plist.Padding = new System.Windows.Forms.Padding(5);
			this.plist.Size = new System.Drawing.Size(385, 234);
			this.plist.TabIndex = 3;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(156)))), ((int)(((byte)(78)))));
			this.panel1.Controls.Add(this.lnkRefresh);
			this.panel1.Controls.Add(this.linkLabel1);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.ForeColor = System.Drawing.SystemColors.Window;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(385, 36);
			this.panel1.TabIndex = 2;
			// 
			// lnkRefresh
			// 
			this.lnkRefresh.Dock = System.Windows.Forms.DockStyle.Right;
			this.lnkRefresh.Image = global::TOBA.Properties.Resources.cou_16_refresh;
			this.lnkRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnkRefresh.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.lnkRefresh.LinkColor = System.Drawing.Color.White;
			this.lnkRefresh.Location = new System.Drawing.Point(285, 0);
			this.lnkRefresh.Name = "lnkRefresh";
			this.lnkRefresh.Size = new System.Drawing.Size(50, 36);
			this.lnkRefresh.TabIndex = 2;
			this.lnkRefresh.TabStop = true;
			this.lnkRefresh.Text = "刷新";
			this.lnkRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.lnkRefresh, "刷新显示的列表");
			// 
			// linkLabel1
			// 
			this.linkLabel1.Dock = System.Windows.Forms.DockStyle.Right;
			this.linkLabel1.Image = global::TOBA.Properties.Resources.window_new;
			this.linkLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(0, 2);
			this.linkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.linkLabel1.LinkColor = System.Drawing.SystemColors.Window;
			this.linkLabel1.Location = new System.Drawing.Point(335, 0);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(50, 36);
			this.linkLabel1.TabIndex = 1;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "更多";
			this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Image = global::TOBA.Properties.Resources.gnome_blog;
			this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.label1.Size = new System.Drawing.Size(100, 36);
			this.label1.TabIndex = 0;
			this.label1.Text = "鱼的博客";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// toolTip1
			// 
			this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.toolTip1.ToolTipTitle = "新博客";
			// 
			// timer
			// 
			this.timer.Interval = 4800000;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// loading1
			// 
			this.loading1.BackColor = System.Drawing.SystemColors.Window;
			this.loading1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.loading1.LoadingText = "正在加载中....";
			this.loading1.Location = new System.Drawing.Point(75, 117);
			this.loading1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.loading1.Name = "loading1";
			this.loading1.Size = new System.Drawing.Size(235, 37);
			this.loading1.TabIndex = 5;
			this.loading1.TextInLoading = "正在加载中....";
			this.loading1.TextLoadingError = "加载失败....";
			this.loading1.TextLoadingOk = "加载成功";
			// 
			// ForumNews
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.loading1);
			this.Controls.Add(this.plist);
			this.Controls.Add(this.panel1);
			this.Name = "ForumNews";
			this.Size = new System.Drawing.Size(385, 270);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel plist;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.LinkLabel lnkRefresh;
		private Loading loading1;
	}
}
