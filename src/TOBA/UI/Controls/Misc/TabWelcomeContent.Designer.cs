namespace TOBA.UI.Controls.Misc
{
	partial class TabWelcomeContent
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.systemNotice1 = new TOBA.UI.Controls.Misc.SystemNotice();
			this.forumNews1 = new TOBA.UI.Controls.Misc.ForumNews();
			this.stationNews1 = new TOBA.UI.Controls.Misc.StationNews();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
			this.tableLayoutPanel1.Controls.Add(this.systemNotice1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.forumNews1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.stationNews1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1044, 651);
			this.tableLayoutPanel1.TabIndex = 4;
			// 
			// systemNotice1
			// 
			this.systemNotice1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.systemNotice1.Location = new System.Drawing.Point(4, 4);
			this.systemNotice1.Margin = new System.Windows.Forms.Padding(4);
			this.systemNotice1.Name = "systemNotice1";
			this.tableLayoutPanel1.SetRowSpan(this.systemNotice1, 2);
			this.systemNotice1.Size = new System.Drawing.Size(514, 643);
			this.systemNotice1.TabIndex = 11;
			// 
			// forumNews1
			// 
			this.forumNews1.BackColor = System.Drawing.SystemColors.Window;
			this.forumNews1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.forumNews1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.forumNews1.Fid = 0;
			this.forumNews1.Location = new System.Drawing.Point(526, 329);
			this.forumNews1.Margin = new System.Windows.Forms.Padding(4);
			this.forumNews1.Name = "forumNews1";
			this.forumNews1.Size = new System.Drawing.Size(514, 318);
			this.forumNews1.TabIndex = 12;
			// 
			// stationNews1
			// 
			this.stationNews1.BackColor = System.Drawing.SystemColors.Window;
			this.stationNews1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.stationNews1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.stationNews1.Location = new System.Drawing.Point(525, 3);
			this.stationNews1.Name = "stationNews1";
			this.stationNews1.Size = new System.Drawing.Size(516, 319);
			this.stationNews1.TabIndex = 14;
			// 
			// TabWelcomeContent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Name = "TabWelcomeContent";
			this.Size = new System.Drawing.Size(1044, 651);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private ForumNews forumNews1;
		private SystemNotice systemNotice1;
		private StationNews stationNews1;
	}
}
