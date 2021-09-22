namespace TOBA.UI.Controls.Passenger
{
	using DevComponents.DotNetBar.Controls;

	using Misc;

	partial class PassengerPanel
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		#region 组件设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.imgList = new System.Windows.Forms.ImageList(this.components);
			this.list = new DevComponents.DotNetBar.Controls.ListViewEx();
			this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colIdType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colId = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colMobile = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colDeleteMsg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.tsFilter = new System.Windows.Forms.ToolStripTextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblUsage = new DevComponents.DotNetBar.LabelX();
			this.usage = new DevComponents.DotNetBar.Controls.ProgressBarX();
			this.ts = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsRefresh = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsAdd = new System.Windows.Forms.ToolStripButton();
			this.tsEdit = new System.Windows.Forms.ToolStripButton();
			this.tsDelete = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsImport = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsImportDlg = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsImportToClipboard = new System.Windows.Forms.ToolStripMenuItem();
			this.tsImportToFile = new System.Windows.Forms.ToolStripMenuItem();
			this.loadingtip = new Loading();
			this.panel1.SuspendLayout();
			this.ts.SuspendLayout();
			this.SuspendLayout();
			// 
			// imgList
			// 
			this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imgList.ImageSize = new System.Drawing.Size(24, 24);
			this.imgList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// list
			// 
			// 
			// 
			// 
			this.list.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.list.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.colName,
			this.colType,
			this.colIdType,
			this.colId,
			this.colMobile,
			this.colStatus,
			this.colDeleteMsg,
			this.colMessage});
			this.list.DisabledBackColor = System.Drawing.Color.Empty;
			this.list.Dock = System.Windows.Forms.DockStyle.Fill;
			this.list.FullRowSelect = true;
			this.list.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.list.Location = new System.Drawing.Point(0, 25);
			this.list.Name = "list";
			this.list.Size = new System.Drawing.Size(850, 368);
			this.list.SmallImageList = this.imgList;
			this.list.TabIndex = 1;
			this.list.UseCompatibleStateImageBehavior = false;
			this.list.View = System.Windows.Forms.View.Details;
			// 
			// colName
			// 
			this.colName.Text = "姓名";
			this.colName.Width = 90;
			// 
			// colType
			// 
			this.colType.Text = "类型";
			this.colType.Width = 65;
			// 
			// colIdType
			// 
			this.colIdType.Text = "证件类型";
			this.colIdType.Width = 99;
			// 
			// colId
			// 
			this.colId.Text = "证件号";
			this.colId.Width = 132;
			// 
			// colMobile
			// 
			this.colMobile.Text = "手机号";
			this.colMobile.Width = 120;
			// 
			// colStatus
			// 
			this.colStatus.Text = "状态";
			this.colStatus.Width = 53;
			// 
			// colDeleteMsg
			// 
			this.colDeleteMsg.Text = "可删除状态";
			this.colDeleteMsg.Width = 150;
			// 
			// colMessage
			// 
			this.colMessage.Text = "信息";
			this.colMessage.Width = 200;
			// 
			// tsFilter
			// 
			this.tsFilter.Name = "tsFilter";
			this.tsFilter.Size = new System.Drawing.Size(200, 25);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.Controls.Add(this.lblUsage);
			this.panel1.Controls.Add(this.usage);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 393);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(850, 25);
			this.panel1.TabIndex = 3;
			// 
			// lblUsage
			// 
			this.lblUsage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblUsage.AutoSize = true;
			this.lblUsage.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.lblUsage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblUsage.Location = new System.Drawing.Point(429, 4);
			this.lblUsage.Name = "lblUsage";
			this.lblUsage.Size = new System.Drawing.Size(0, 0);
			this.lblUsage.TabIndex = 2;
			this.lblUsage.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// usage
			// 
			this.usage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.usage.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.usage.Location = new System.Drawing.Point(569, 4);
			this.usage.Maximum = 30;
			this.usage.Name = "usage";
			this.usage.Size = new System.Drawing.Size(278, 16);
			this.usage.TabIndex = 1;
			this.usage.Text = "20/30";
			this.usage.Value = 20;
			// 
			// ts
			// 
			this.ts.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.toolStripLabel1,
			this.tsFilter,
			this.toolStripSeparator3,
			this.tsRefresh,
			this.toolStripSeparator2,
			this.tsAdd,
			this.tsEdit,
			this.tsDelete,
			this.toolStripSeparator1,
			this.tsImport});
			this.ts.Location = new System.Drawing.Point(0, 0);
			this.ts.Name = "ts";
			this.ts.Size = new System.Drawing.Size(850, 25);
			this.ts.TabIndex = 0;
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(32, 22);
			this.toolStripLabel1.Text = "查找";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tsRefresh
			// 
			this.tsRefresh.Image = global::TOBA.Properties.Resources.cou_16_refresh;
			this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRefresh.Name = "tsRefresh";
			this.tsRefresh.Size = new System.Drawing.Size(76, 22);
			this.tsRefresh.Text = "刷新列表";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsAdd
			// 
			this.tsAdd.Image = global::TOBA.Properties.Resources.plus_16;
			this.tsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAdd.Name = "tsAdd";
			this.tsAdd.Size = new System.Drawing.Size(52, 22);
			this.tsAdd.Text = "添加";
			// 
			// tsEdit
			// 
			this.tsEdit.Image = global::TOBA.Properties.Resources.pencil_16;
			this.tsEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsEdit.Name = "tsEdit";
			this.tsEdit.Size = new System.Drawing.Size(52, 22);
			this.tsEdit.Text = "编辑";
			// 
			// tsDelete
			// 
			this.tsDelete.Image = global::TOBA.Properties.Resources.trash_16;
			this.tsDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsDelete.Name = "tsDelete";
			this.tsDelete.Size = new System.Drawing.Size(52, 22);
			this.tsDelete.Text = "删除";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsImport
			// 
			this.tsImport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsImportDlg,
			this.toolStripMenuItem1,
			this.tsImportToClipboard,
			this.tsImportToFile});
			this.tsImport.Image = global::TOBA.Properties.Resources.transfer;
			this.tsImport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsImport.Name = "tsImport";
			this.tsImport.Size = new System.Drawing.Size(133, 22);
			this.tsImport.Text = "导入导出联系人(&I)";
			// 
			// tsImportDlg
			// 
			this.tsImportDlg.Image = global::TOBA.Properties.Resources.user_16;
			this.tsImportDlg.Name = "tsImportDlg";
			this.tsImportDlg.Size = new System.Drawing.Size(164, 22);
			this.tsImportDlg.Text = "导入联系人(&P)...";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(161, 6);
			// 
			// tsImportToClipboard
			// 
			this.tsImportToClipboard.Image = global::TOBA.Properties.Resources.paste_plain;
			this.tsImportToClipboard.Name = "tsImportToClipboard";
			this.tsImportToClipboard.Size = new System.Drawing.Size(164, 22);
			this.tsImportToClipboard.Text = "复制到剪贴板(&C)";
			// 
			// tsImportToFile
			// 
			this.tsImportToFile.Image = global::TOBA.Properties.Resources.save_16;
			this.tsImportToFile.Name = "tsImportToFile";
			this.tsImportToFile.Size = new System.Drawing.Size(164, 22);
			this.tsImportToFile.Text = "导出到文件(&F)...";
			// 
			// loadingtip
			// 
			this.loadingtip.BackColor = System.Drawing.SystemColors.Window;
			this.loadingtip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.loadingtip.LoadingText = "正在加载中....";
			this.loadingtip.Location = new System.Drawing.Point(293, 173);
			this.loadingtip.Name = "loadingtip";
			this.loadingtip.Size = new System.Drawing.Size(251, 58);
			this.loadingtip.TabIndex = 4;
			this.loadingtip.TextInLoading = "正在加载中....";
			this.loadingtip.TextLoadingError = "加载失败....";
			this.loadingtip.TextLoadingOk = "加载成功";
			// 
			// PassengerPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.list);
			this.Controls.Add(this.loadingtip);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.ts);
			this.Name = "PassengerPanel";
			this.Size = new System.Drawing.Size(850, 418);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ts.ResumeLayout(false);
			this.ts.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip ts;
		private DevComponents.DotNetBar.Controls.ListViewEx list;
		private System.Windows.Forms.ToolStripTextBox tsFilter;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsAdd;
		private System.Windows.Forms.ToolStripButton tsEdit;
		private System.Windows.Forms.ToolStripButton tsDelete;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsRefresh;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ImageList imgList;
		private System.Windows.Forms.ColumnHeader colName;
		private System.Windows.Forms.ColumnHeader colType;
		private System.Windows.Forms.ColumnHeader colIdType;
		private System.Windows.Forms.ColumnHeader colId;
		private System.Windows.Forms.ColumnHeader colMobile;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripDropDownButton tsImport;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsImportToClipboard;
		private System.Windows.Forms.ToolStripMenuItem tsImportToFile;
		private System.Windows.Forms.ToolStripMenuItem tsImportDlg;
		private System.Windows.Forms.ColumnHeader colStatus;
		private System.Windows.Forms.ColumnHeader colMessage;
		private Loading loadingtip;
		private DevComponents.DotNetBar.LabelX lblUsage;
		private DevComponents.DotNetBar.Controls.ProgressBarX usage;
		private System.Windows.Forms.ColumnHeader colDeleteMsg;
	}
}
