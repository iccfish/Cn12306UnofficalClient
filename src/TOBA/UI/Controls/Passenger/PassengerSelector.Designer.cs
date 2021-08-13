namespace TOBA.UI.Controls.Passenger
{
	using Common;

	partial class PassengerSelector
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
			this.panel4 = new System.Windows.Forms.Panel();
			this.lnkReload = new System.Windows.Forms.LinkLabel();
			this.lnkAdd = new System.Windows.Forms.LinkLabel();
			this.txtSearch = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lstPas = new DevComponents.DotNetBar.Controls.ListViewEx();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ilList = new System.Windows.Forms.ImageList(this.components);
			this.pLoading = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pEmpty = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.tt = new System.Windows.Forms.ToolTip(this.components);
			this.tipBarMessage1 = new TipBarMessage();
			this.panel4.SuspendLayout();
			this.pLoading.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.pEmpty.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.SuspendLayout();
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(139)))), ((int)(((byte)(75)))));
			this.panel4.Controls.Add(this.lnkReload);
			this.panel4.Controls.Add(this.lnkAdd);
			this.panel4.Controls.Add(this.txtSearch);
			this.panel4.Controls.Add(this.label1);
			this.panel4.Controls.Add(this.label4);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel4.ForeColor = System.Drawing.Color.White;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(576, 30);
			this.panel4.TabIndex = 1;
			// 
			// lnkReload
			// 
			this.lnkReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lnkReload.AutoSize = true;
			this.lnkReload.LinkArea = new System.Windows.Forms.LinkArea(1, 2);
			this.lnkReload.LinkColor = System.Drawing.Color.White;
			this.lnkReload.Location = new System.Drawing.Point(331, 7);
			this.lnkReload.Name = "lnkReload";
			this.lnkReload.Size = new System.Drawing.Size(37, 21);
			this.lnkReload.TabIndex = 4;
			this.lnkReload.TabStop = true;
			this.lnkReload.Text = "[刷新]";
			this.lnkReload.UseCompatibleTextRendering = true;
			// 
			// lnkAdd
			// 
			this.lnkAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lnkAdd.AutoSize = true;
			this.lnkAdd.LinkArea = new System.Windows.Forms.LinkArea(1, 2);
			this.lnkAdd.LinkColor = System.Drawing.Color.White;
			this.lnkAdd.Location = new System.Drawing.Point(283, 7);
			this.lnkAdd.Name = "lnkAdd";
			this.lnkAdd.Size = new System.Drawing.Size(37, 21);
			this.lnkAdd.TabIndex = 3;
			this.lnkAdd.TabStop = true;
			this.lnkAdd.Text = "[新建]";
			this.lnkAdd.UseCompatibleTextRendering = true;
			this.lnkAdd.Visible = false;
			// 
			// txtSearch
			// 
			this.txtSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.txtSearch.Border.Class = "TextBoxBorder";
			this.txtSearch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtSearch.Location = new System.Drawing.Point(412, 3);
			this.txtSearch.Name = "txtSearch";
			this.txtSearch.PreventEnterBeep = true;
			this.txtSearch.Size = new System.Drawing.Size(161, 23);
			this.txtSearch.TabIndex = 4;
			this.txtSearch.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty;
			this.txtSearch.WatermarkImage = global::TOBA.Properties.Resources.search_magnifier;
			this.txtSearch.WatermarkText = "搜索姓名，证件号...";
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(377, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "搜索";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 7);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(152, 17);
			this.label4.TabIndex = 0;
			this.label4.Text = "双击姓名添加联系人到列表";
			// 
			// lstPas
			// 
			// 
			// 
			// 
			this.lstPas.Border.Class = "ListViewBorder";
			this.lstPas.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lstPas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.lstPas.DisabledBackColor = System.Drawing.Color.Empty;
			this.lstPas.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstPas.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lstPas.FullRowSelect = true;
			this.lstPas.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lstPas.Location = new System.Drawing.Point(0, 30);
			this.lstPas.MultiSelect = false;
			this.lstPas.Name = "lstPas";
			this.lstPas.Size = new System.Drawing.Size(576, 266);
			this.lstPas.SmallImageList = this.ilList;
			this.lstPas.TabIndex = 2;
			this.lstPas.UseCompatibleStateImageBehavior = false;
			this.lstPas.View = System.Windows.Forms.View.List;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 100;
			// 
			// ilList
			// 
			this.ilList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.ilList.ImageSize = new System.Drawing.Size(20, 20);
			this.ilList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// pLoading
			// 
			this.pLoading.BackColor = System.Drawing.SystemColors.Window;
			this.pLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pLoading.Controls.Add(this.label2);
			this.pLoading.Controls.Add(this.pictureBox1);
			this.pLoading.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pLoading.Location = new System.Drawing.Point(143, 140);
			this.pLoading.Name = "pLoading";
			this.pLoading.Size = new System.Drawing.Size(273, 41);
			this.pLoading.TabIndex = 3;
			this.pLoading.Visible = false;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(41, 11);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(140, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "联系人正在加载中。。。";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TOBA.Properties.Resources._32px_loading_1;
			this.pictureBox1.Location = new System.Drawing.Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// pEmpty
			// 
			this.pEmpty.BackColor = System.Drawing.SystemColors.Window;
			this.pEmpty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pEmpty.Controls.Add(this.label3);
			this.pEmpty.Controls.Add(this.pictureBox2);
			this.pEmpty.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pEmpty.Location = new System.Drawing.Point(143, 140);
			this.pEmpty.Name = "pEmpty";
			this.pEmpty.Size = new System.Drawing.Size(273, 41);
			this.pEmpty.TabIndex = 3;
			this.pEmpty.Visible = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(41, 3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(224, 34);
			this.label3.TabIndex = 1;
			this.label3.Text = "根据当前的条件，没有找到可用的联系人\r\n请留意当前是否选择了查询学生票";
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::TOBA.Properties.Resources.cou_32_warning;
			this.pictureBox2.Location = new System.Drawing.Point(3, 3);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(32, 32);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 0;
			this.pictureBox2.TabStop = false;
			// 
			// tt
			// 
			this.tt.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.tt.ToolTipTitle = "联系人信息";
			// 
			// tipBarMessage1
			// 
			this.tipBarMessage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(253)))), ((int)(((byte)(219)))));
			this.tipBarMessage1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(205)))), ((int)(((byte)(60)))));
			this.tipBarMessage1.BorderThickness = 0;
			this.tipBarMessage1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tipBarMessage1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tipBarMessage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(149)))), ((int)(((byte)(43)))));
			this.tipBarMessage1.Image = global::TOBA.Properties.Resources.info_16;
			this.tipBarMessage1.LabelMargin = new System.Windows.Forms.Padding(25, 5, 4, 6);
			this.tipBarMessage1.Location = new System.Drawing.Point(0, 296);
			this.tipBarMessage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tipBarMessage1.Name = "tipBarMessage1";
			this.tipBarMessage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tipBarMessage1.Size = new System.Drawing.Size(576, 29);
			this.tipBarMessage1.TabIndex = 4;
			this.tipBarMessage1.Text = "儿童不可单独订票，请点击已添加的成人增加儿童票。";
			// 
			// PassengerSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pEmpty);
			this.Controls.Add(this.pLoading);
			this.Controls.Add(this.lstPas);
			this.Controls.Add(this.panel4);
			this.Controls.Add(this.tipBarMessage1);
			this.Name = "PassengerSelector";
			this.Size = new System.Drawing.Size(576, 325);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.pLoading.ResumeLayout(false);
			this.pLoading.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.pEmpty.ResumeLayout(false);
			this.pEmpty.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel lnkAdd;
		private System.Windows.Forms.LinkLabel lnkReload;
		private DevComponents.DotNetBar.Controls.TextBoxX txtSearch;
		private DevComponents.DotNetBar.Controls.ListViewEx lstPas;
		private System.Windows.Forms.ImageList ilList;
		private System.Windows.Forms.Panel pLoading;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel pEmpty;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ToolTip tt;
		private TipBarMessage tipBarMessage1;
	}
}
