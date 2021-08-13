namespace TOBA.UI.Controls.QueryManager
{
	partial class QmPanel
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
			this.lstQuery = new System.Windows.Forms.ListView();
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imgStatus = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip2 = new System.Windows.Forms.ToolStrip();
			this.tsQuery = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsStartBatch = new System.Windows.Forms.ToolStripButton();
			this.tsStopAll = new System.Windows.Forms.ToolStripButton();
			this.tsQs = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsQsAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsQsNone = new System.Windows.Forms.ToolStripMenuItem();
			this.tsQsInvent = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsQsDate = new System.Windows.Forms.ToolStripMenuItem();
			this.tsQsDepart = new System.Windows.Forms.ToolStripMenuItem();
			this.tsQsArrival = new System.Windows.Forms.ToolStripMenuItem();
			this.tsQsTrip = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsAdd = new System.Windows.Forms.ToolStripButton();
			this.tsCopy = new System.Windows.Forms.ToolStripButton();
			this.tsClose = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsSendTo = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsNoOtherAccount = new System.Windows.Forms.ToolStripMenuItem();
			this.tsCopyToClip = new System.Windows.Forms.ToolStripButton();
			this.tsPasteFromClip = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsOneKeySet = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsOneKeySetAllRefresh = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStrip2.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstQuery
			// 
			this.lstQuery.CheckBoxes = true;
			this.lstQuery.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader7,
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader9,
            this.columnHeader8,
            this.columnHeader5});
			this.lstQuery.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstQuery.FullRowSelect = true;
			this.lstQuery.Location = new System.Drawing.Point(0, 50);
			this.lstQuery.Name = "lstQuery";
			this.lstQuery.Size = new System.Drawing.Size(1151, 401);
			this.lstQuery.SmallImageList = this.imgStatus;
			this.lstQuery.TabIndex = 2;
			this.lstQuery.UseCompatibleStateImageBehavior = false;
			this.lstQuery.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "状态";
			this.columnHeader7.Width = 100;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "名称";
			this.columnHeader1.Width = 251;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "发站";
			this.columnHeader2.Width = 142;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "到站";
			this.columnHeader3.Width = 132;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "日期";
			this.columnHeader4.Width = 144;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "自动刷新";
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "上次查询时间";
			this.columnHeader8.Width = 102;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "上次查询状态";
			this.columnHeader5.Width = 142;
			// 
			// imgStatus
			// 
			this.imgStatus.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imgStatus.ImageSize = new System.Drawing.Size(24, 24);
			this.imgStatus.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// toolStrip2
			// 
			this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsQuery,
            this.toolStripSeparator2,
            this.tsStartBatch,
            this.tsStopAll});
			this.toolStrip2.Location = new System.Drawing.Point(0, 25);
			this.toolStrip2.Name = "toolStrip2";
			this.toolStrip2.Size = new System.Drawing.Size(1151, 25);
			this.toolStrip2.TabIndex = 3;
			this.toolStrip2.Text = "toolStrip2";
			// 
			// tsQuery
			// 
			this.tsQuery.Image = global::TOBA.Properties.Resources.cou_16_search;
			this.tsQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsQuery.Name = "tsQuery";
			this.tsQuery.Size = new System.Drawing.Size(70, 22);
			this.tsQuery.Text = "查询(Q)";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsStartBatch
			// 
			this.tsStartBatch.Image = global::TOBA.Properties.Resources._099;
			this.tsStartBatch.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsStartBatch.Name = "tsStartBatch";
			this.tsStartBatch.Size = new System.Drawing.Size(76, 22);
			this.tsStartBatch.Text = "同步查询";
			// 
			// tsStopAll
			// 
			this.tsStopAll.Image = global::TOBA.Properties.Resources._148;
			this.tsStopAll.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsStopAll.Name = "tsStopAll";
			this.tsStopAll.Size = new System.Drawing.Size(76, 22);
			this.tsStopAll.Text = "全部停止";
			// 
			// tsQs
			// 
			this.tsQs.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsQsAll,
            this.tsQsNone,
            this.tsQsInvent,
            this.toolStripMenuItem1,
            this.tsQsDate,
            this.tsQsDepart,
            this.tsQsArrival,
            this.tsQsTrip});
			this.tsQs.Image = global::TOBA.Properties.Resources.cou_16_accept;
			this.tsQs.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsQs.Name = "tsQs";
			this.tsQs.Size = new System.Drawing.Size(103, 22);
			this.tsQs.Text = "快速选择(&Q)";
			// 
			// tsQsAll
			// 
			this.tsQsAll.Name = "tsQsAll";
			this.tsQsAll.Size = new System.Drawing.Size(164, 22);
			this.tsQsAll.Text = "全部选择(&A)";
			// 
			// tsQsNone
			// 
			this.tsQsNone.Name = "tsQsNone";
			this.tsQsNone.Size = new System.Drawing.Size(164, 22);
			this.tsQsNone.Text = "全部不选(&N)";
			// 
			// tsQsInvent
			// 
			this.tsQsInvent.Name = "tsQsInvent";
			this.tsQsInvent.Size = new System.Drawing.Size(164, 22);
			this.tsQsInvent.Text = "反向选择(&I)";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(161, 6);
			// 
			// tsQsDate
			// 
			this.tsQsDate.Name = "tsQsDate";
			this.tsQsDate.Size = new System.Drawing.Size(164, 22);
			this.tsQsDate.Text = "按日期选择(&D)";
			// 
			// tsQsDepart
			// 
			this.tsQsDepart.Name = "tsQsDepart";
			this.tsQsDepart.Size = new System.Drawing.Size(164, 22);
			this.tsQsDepart.Text = "按出发地选择(P)";
			// 
			// tsQsArrival
			// 
			this.tsQsArrival.Name = "tsQsArrival";
			this.tsQsArrival.Size = new System.Drawing.Size(164, 22);
			this.tsQsArrival.Text = "按到达地选择(&R)";
			// 
			// tsQsTrip
			// 
			this.tsQsTrip.Name = "tsQsTrip";
			this.tsQsTrip.Size = new System.Drawing.Size(164, 22);
			this.tsQsTrip.Text = "按行程选择(&T)";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
			// 
			// tsAdd
			// 
			this.tsAdd.Image = global::TOBA.Properties.Resources.plus_16;
			this.tsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsAdd.Name = "tsAdd";
			this.tsAdd.Size = new System.Drawing.Size(66, 22);
			this.tsAdd.Text = "新建&N)";
			// 
			// tsCopy
			// 
			this.tsCopy.Image = global::TOBA.Properties.Resources.plus_16;
			this.tsCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCopy.Name = "tsCopy";
			this.tsCopy.Size = new System.Drawing.Size(68, 22);
			this.tsCopy.Text = "复制(&C)";
			// 
			// tsClose
			// 
			this.tsClose.Image = global::TOBA.Properties.Resources.trash_16;
			this.tsClose.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsClose.Name = "tsClose";
			this.tsClose.Size = new System.Drawing.Size(66, 22);
			this.tsClose.Text = "关闭(&L)";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// tsSendTo
			// 
			this.tsSendTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNoOtherAccount});
			this.tsSendTo.Image = global::TOBA.Properties.Resources.layer_export;
			this.tsSendTo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsSendTo.Name = "tsSendTo";
			this.tsSendTo.Size = new System.Drawing.Size(121, 22);
			this.tsSendTo.Text = "复制到其它账户";
			// 
			// tsNoOtherAccount
			// 
			this.tsNoOtherAccount.Enabled = false;
			this.tsNoOtherAccount.Image = global::TOBA.Properties.Resources.block_16;
			this.tsNoOtherAccount.Name = "tsNoOtherAccount";
			this.tsNoOtherAccount.Size = new System.Drawing.Size(184, 22);
			this.tsNoOtherAccount.Text = "没有其它已登录账户";
			// 
			// tsCopyToClip
			// 
			this.tsCopyToClip.Image = global::TOBA.Properties.Resources.transfer;
			this.tsCopyToClip.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCopyToClip.Name = "tsCopyToClip";
			this.tsCopyToClip.Size = new System.Drawing.Size(115, 22);
			this.tsCopyToClip.Text = "复制到剪贴板(&P)";
			// 
			// tsPasteFromClip
			// 
			this.tsPasteFromClip.Image = global::TOBA.Properties.Resources.paste_plain;
			this.tsPasteFromClip.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsPasteFromClip.Name = "tsPasteFromClip";
			this.tsPasteFromClip.Size = new System.Drawing.Size(115, 22);
			this.tsPasteFromClip.Text = "从剪贴板粘贴(&P)";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// tsOneKeySet
			// 
			this.tsOneKeySet.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsOneKeySetAllRefresh});
			this.tsOneKeySet.Image = global::TOBA.Properties.Resources.cou_16_tag_blue;
			this.tsOneKeySet.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsOneKeySet.Name = "tsOneKeySet";
			this.tsOneKeySet.Size = new System.Drawing.Size(101, 22);
			this.tsOneKeySet.Text = "一键设置(&K)";
			// 
			// tsOneKeySetAllRefresh
			// 
			this.tsOneKeySetAllRefresh.Name = "tsOneKeySetAllRefresh";
			this.tsOneKeySetAllRefresh.Size = new System.Drawing.Size(200, 22);
			this.tsOneKeySetAllRefresh.Text = "全部设置为刷票模式(&A)";
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsQs,
            this.toolStripSeparator3,
            this.tsAdd,
            this.tsCopy,
            this.tsClose,
            this.toolStripSeparator5,
            this.tsSendTo,
            this.tsCopyToClip,
            this.tsPasteFromClip,
            this.toolStripSeparator4,
            this.tsOneKeySet});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(1151, 25);
			this.toolStrip1.TabIndex = 1;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// QmPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.lstQuery);
			this.Controls.Add(this.toolStrip2);
			this.Controls.Add(this.toolStrip1);
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "QmPanel";
			this.Size = new System.Drawing.Size(1151, 451);
			this.toolStrip2.ResumeLayout(false);
			this.toolStrip2.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ListView lstQuery;
		private System.Windows.Forms.ImageList imgStatus;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ToolStrip toolStrip2;
		private System.Windows.Forms.ToolStripButton tsStartBatch;
		private System.Windows.Forms.ToolStripButton tsStopAll;
		private System.Windows.Forms.ToolStripButton tsQuery;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripDropDownButton tsQs;
		private System.Windows.Forms.ToolStripMenuItem tsQsAll;
		private System.Windows.Forms.ToolStripMenuItem tsQsNone;
		private System.Windows.Forms.ToolStripMenuItem tsQsInvent;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsQsDate;
		private System.Windows.Forms.ToolStripMenuItem tsQsDepart;
		private System.Windows.Forms.ToolStripMenuItem tsQsArrival;
		private System.Windows.Forms.ToolStripMenuItem tsQsTrip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripButton tsAdd;
		private System.Windows.Forms.ToolStripButton tsCopy;
		private System.Windows.Forms.ToolStripButton tsClose;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripDropDownButton tsSendTo;
		private System.Windows.Forms.ToolStripMenuItem tsNoOtherAccount;
		private System.Windows.Forms.ToolStripButton tsCopyToClip;
		private System.Windows.Forms.ToolStripButton tsPasteFromClip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripDropDownButton tsOneKeySet;
		private System.Windows.Forms.ToolStripMenuItem tsOneKeySetAllRefresh;
		private System.Windows.Forms.ToolStrip toolStrip1;
	}
}
