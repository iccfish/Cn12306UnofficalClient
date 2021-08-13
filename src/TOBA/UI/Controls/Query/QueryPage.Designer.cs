using System.Windows.Forms;

namespace TOBA.UI.Controls.Query
{
	using Common;

	using ComponentOwl.BetterListView;

	using Popup;

	partial class QueryPage
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryPage));
			this.qr = new TOBA.UI.Controls.Query.QueryResult();
			this.btnDateLoop = new System.Windows.Forms.Button();
			this.btnConfig = new ButtonWithPopup();
			this.qs = new TOBA.UI.Controls.Query.AutoSubmitSetting();
			this.qp = new TOBA.UI.Controls.Query.QueryParams();
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnEnableSellTip = new System.Windows.Forms.Button();
			this.tbStatus = new TipBarMessage();
			this.panWait = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.sellTimeTip = new TOBA.UI.Controls.Query.SellTimeTip();
			this.ilStartTip = new System.Windows.Forms.ImageList(this.components);
			this.pSellTipContainer = new System.Windows.Forms.Panel();
			this.lvSellTip = new System.Windows.Forms.ListView();
			this.colTipInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.pSellTip = new System.Windows.Forms.Panel();
			this.sellTipClose = new TOBA.UI.Controls.Common.CloseButton12Px();
			this.alternativeDateSetting = new TOBA.UI.Controls.Query.AlternativeDateSetting();
			this.backupOrder = new TOBA.UI.Controls.Query.BackupOrderCart();
			((System.ComponentModel.ISupportInitialize)(this.qr)).BeginInit();
			this.panel3.SuspendLayout();
			this.panWait.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.pSellTipContainer.SuspendLayout();
			this.pSellTip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.sellTipClose)).BeginInit();
			this.SuspendLayout();
			// 
			// qr
			// 
			this.qr.AllowDrag = true;
			this.qr.AllowDrop = true;
			this.qr.AllowedDragEffects = ((System.Windows.Forms.DragDropEffects)((((System.Windows.Forms.DragDropEffects.Copy | System.Windows.Forms.DragDropEffects.Move) 
            | System.Windows.Forms.DragDropEffects.Link) 
            | System.Windows.Forms.DragDropEffects.Scroll)));
			this.qr.AutoSizeItemsInDetailsView = true;
			this.qr.ColorSortedColumn = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
			this.qr.DisableContextMenu = false;
			this.qr.Dock = System.Windows.Forms.DockStyle.Fill;
			this.qr.GridLines = ComponentOwl.BetterListView.BetterListViewGridLines.Horizontal;
			this.qr.HideSelectionMode = ComponentOwl.BetterListView.BetterListViewHideSelectionMode.Disable;
			this.qr.IgnoreGroupSetting = false;
			this.qr.Location = new System.Drawing.Point(0, 114);
			this.qr.MultiSelect = false;
			this.qr.Name = "qr";
			this.qr.ShowGroups = true;
			this.qr.ShowStartAndEndStation = false;
			this.qr.Size = new System.Drawing.Size(1027, 228);
			this.qr.SortedColumnsRowsHighlight = ComponentOwl.BetterListView.BetterListViewSortedColumnsRowsHighlight.ShowAlways;
			this.qr.TabIndex = 0;
			// 
			// btnDateLoop
			// 
			this.btnDateLoop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDateLoop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnDateLoop.Image = global::TOBA.Properties.Resources.calendar_16;
			this.btnDateLoop.Location = new System.Drawing.Point(879, 2);
			this.btnDateLoop.Name = "btnDateLoop";
			this.btnDateLoop.Size = new System.Drawing.Size(74, 28);
			this.btnDateLoop.TabIndex = 10;
			this.btnDateLoop.TabStop = false;
			this.btnDateLoop.Text = " 轮查";
			this.btnDateLoop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnDateLoop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnDateLoop.UseVisualStyleBackColor = true;
			// 
			// btnConfig
			// 
			this.btnConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnConfig.AnimationDuration = 100;
			this.btnConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnConfig.HidingAnimation = ((PopupAnimations)((PopupAnimations.BottomToTop | PopupAnimations.Slide)));
			this.btnConfig.Image = global::TOBA.Properties.Resources.gear_16;
			this.btnConfig.Location = new System.Drawing.Point(952, 2);
			this.btnConfig.Name = "btnConfig";
			this.btnConfig.PopupControl = null;
			this.btnConfig.ShowingAnimation = ((PopupAnimations)((PopupAnimations.TopToBottom | PopupAnimations.Slide)));
			this.btnConfig.Size = new System.Drawing.Size(74, 28);
			this.btnConfig.TabIndex = 11;
			this.btnConfig.TabStop = false;
			this.btnConfig.Text = " 设置";
			this.btnConfig.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnConfig.UseVisualStyleBackColor = true;
			// 
			// qs
			// 
			this.qs.BackColor = System.Drawing.Color.White;
			this.qs.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.qs.Image = null;
			this.qs.Location = new System.Drawing.Point(0, 542);
			this.qs.Margin = new System.Windows.Forms.Padding(0);
			this.qs.Name = "qs";
			this.qs.Size = new System.Drawing.Size(1027, 130);
			this.qs.TabIndex = 0;
			// 
			// qp
			// 
			this.qp.BackColor = System.Drawing.SystemColors.Window;
			this.qp.Dock = System.Windows.Forms.DockStyle.Top;
			this.qp.Image = null;
			this.qp.Location = new System.Drawing.Point(0, 0);
			this.qp.Margin = new System.Windows.Forms.Padding(0);
			this.qp.Name = "qp";
			this.qp.Size = new System.Drawing.Size(1027, 96);
			this.qp.TabIndex = 0;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.btnEnableSellTip);
			this.panel3.Controls.Add(this.btnDateLoop);
			this.panel3.Controls.Add(this.btnConfig);
			this.panel3.Controls.Add(this.tbStatus);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(0, 512);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(1027, 30);
			this.panel3.TabIndex = 3;
			// 
			// btnEnableSellTip
			// 
			this.btnEnableSellTip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnEnableSellTip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnEnableSellTip.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnEnableSellTip.ForeColor = System.Drawing.Color.Purple;
			this.btnEnableSellTip.Image = global::TOBA.Properties.Resources.info_16;
			this.btnEnableSellTip.Location = new System.Drawing.Point(733, 2);
			this.btnEnableSellTip.Name = "btnEnableSellTip";
			this.btnEnableSellTip.Size = new System.Drawing.Size(74, 28);
			this.btnEnableSellTip.TabIndex = 10;
			this.btnEnableSellTip.TabStop = false;
			this.btnEnableSellTip.Text = " 建议";
			this.btnEnableSellTip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnEnableSellTip.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnEnableSellTip.UseVisualStyleBackColor = true;
			// 
			// tbStatus
			// 
			this.tbStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
			this.tbStatus.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(194)))), ((int)(((byte)(241)))));
			this.tbStatus.BorderThickness = 0;
			this.tbStatus.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(138)))), ((int)(((byte)(196)))));
			this.tbStatus.Image = global::TOBA.Properties.Resources.clock_16;
			this.tbStatus.Location = new System.Drawing.Point(0, 0);
			this.tbStatus.Margin = new System.Windows.Forms.Padding(4);
			this.tbStatus.Name = "tbStatus";
			this.tbStatus.Padding = new System.Windows.Forms.Padding(3);
			this.tbStatus.Size = new System.Drawing.Size(1027, 30);
			this.tbStatus.TabIndex = 3;
			this.tbStatus.Text = "尚未开始查询";
			// 
			// panWait
			// 
			this.panWait.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panWait.Controls.Add(this.label1);
			this.panWait.Controls.Add(this.pictureBox1);
			this.panWait.Location = new System.Drawing.Point(407, 307);
			this.panWait.Name = "panWait";
			this.panWait.Size = new System.Drawing.Size(212, 58);
			this.panWait.TabIndex = 4;
			this.panWait.Visible = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(35, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(135, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "正在查询中, 必须稍等....";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TOBA.Properties.Resources._16px_loading_1;
			this.pictureBox1.Location = new System.Drawing.Point(13, 21);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(16, 16);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// sellTimeTip
			// 
			this.sellTimeTip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.sellTimeTip.Dock = System.Windows.Forms.DockStyle.Top;
			this.sellTimeTip.Image = null;
			this.sellTimeTip.Location = new System.Drawing.Point(0, 96);
			this.sellTimeTip.Margin = new System.Windows.Forms.Padding(4);
			this.sellTimeTip.Name = "sellTimeTip";
			this.sellTimeTip.Size = new System.Drawing.Size(1027, 18);
			this.sellTimeTip.TabIndex = 5;
			// 
			// ilStartTip
			// 
			this.ilStartTip.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.ilStartTip.ImageSize = new System.Drawing.Size(20, 20);
			this.ilStartTip.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// pSellTipContainer
			// 
			this.pSellTipContainer.Controls.Add(this.lvSellTip);
			this.pSellTipContainer.Controls.Add(this.pSellTip);
			this.pSellTipContainer.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pSellTipContainer.Location = new System.Drawing.Point(0, 412);
			this.pSellTipContainer.Name = "pSellTipContainer";
			this.pSellTipContainer.Size = new System.Drawing.Size(1027, 100);
			this.pSellTipContainer.TabIndex = 7;
			this.pSellTipContainer.Visible = false;
			// 
			// lvSellTip
			// 
			this.lvSellTip.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTipInfo});
			this.lvSellTip.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvSellTip.FullRowSelect = true;
			this.lvSellTip.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lvSellTip.HideSelection = false;
			this.lvSellTip.Location = new System.Drawing.Point(21, 0);
			this.lvSellTip.MultiSelect = false;
			this.lvSellTip.Name = "lvSellTip";
			this.lvSellTip.Size = new System.Drawing.Size(1006, 100);
			this.lvSellTip.SmallImageList = this.ilStartTip;
			this.lvSellTip.TabIndex = 7;
			this.lvSellTip.UseCompatibleStateImageBehavior = false;
			this.lvSellTip.View = System.Windows.Forms.View.Details;
			// 
			// colTipInfo
			// 
			this.colTipInfo.Text = "始发站与手提式";
			// 
			// pSellTip
			// 
			this.pSellTip.BackColor = System.Drawing.SystemColors.ButtonFace;
			this.pSellTip.Controls.Add(this.sellTipClose);
			this.pSellTip.Dock = System.Windows.Forms.DockStyle.Left;
			this.pSellTip.Location = new System.Drawing.Point(0, 0);
			this.pSellTip.Name = "pSellTip";
			this.pSellTip.Size = new System.Drawing.Size(21, 100);
			this.pSellTip.TabIndex = 8;
			// 
			// sellTipClose
			// 
			this.sellTipClose.Cursor = System.Windows.Forms.Cursors.Hand;
			this.sellTipClose.Image = ((System.Drawing.Image)(resources.GetObject("sellTipClose.Image")));
			this.sellTipClose.Location = new System.Drawing.Point(5, 6);
			this.sellTipClose.Name = "sellTipClose";
			this.sellTipClose.Size = new System.Drawing.Size(11, 18);
			this.sellTipClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.sellTipClose.TabIndex = 0;
			this.sellTipClose.TabStop = false;
			// 
			// alternativeDateSetting
			// 
			this.alternativeDateSetting.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.alternativeDateSetting.Image = null;
			this.alternativeDateSetting.Location = new System.Drawing.Point(0, 383);
			this.alternativeDateSetting.Name = "alternativeDateSetting";
			this.alternativeDateSetting.Size = new System.Drawing.Size(1027, 29);
			this.alternativeDateSetting.TabIndex = 8;
			// 
			// backupOrder
			// 
			this.backupOrder.BackColor = System.Drawing.Color.White;
			this.backupOrder.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.backupOrder.Location = new System.Drawing.Point(0, 342);
			this.backupOrder.Name = "backupOrder";
			this.backupOrder.Size = new System.Drawing.Size(1027, 40);
			this.backupOrder.TabIndex = 9;
			// 
			// QueryPage
			// 
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.qr);
			this.Controls.Add(this.backupOrder);
			this.Controls.Add(this.alternativeDateSetting);
			this.Controls.Add(this.pSellTipContainer);
			this.Controls.Add(this.sellTimeTip);
			this.Controls.Add(this.panWait);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.qs);
			this.Controls.Add(this.qp);
			this.DoubleBuffered = true;
			this.Name = "QueryPage";
			this.Size = new System.Drawing.Size(1027, 672);
			((System.ComponentModel.ISupportInitialize)(this.qr)).EndInit();
			this.panel3.ResumeLayout(false);
			this.panWait.ResumeLayout(false);
			this.panWait.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.pSellTipContainer.ResumeLayout(false);
			this.pSellTip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.sellTipClose)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private QueryParams qp;
		private QueryResult qr;
		private AutoSubmitSetting qs;
		private System.Windows.Forms.Button btnDateLoop;
		private ButtonWithPopup btnConfig;
		private Panel panel3;
		private TipBarMessage tbStatus;
		private Panel panWait;
		private Label label1;
		private PictureBox pictureBox1;
		private SellTimeTip sellTimeTip;
		private ImageList ilStartTip;
		private Panel pSellTipContainer;
		private ListView lvSellTip;
		private ColumnHeader colTipInfo;
		private Panel pSellTip;
		private Common.CloseButton12Px sellTipClose;
		private Button btnEnableSellTip;
		private AlternativeDateSetting alternativeDateSetting;
		private BackupOrderCart backupOrder;
	}
}
