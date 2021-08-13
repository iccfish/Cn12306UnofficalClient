namespace TOBA.UI.Controls.Order
{
	using Misc;

	partial class OrderPanel
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
			this.pEmpty = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.loading = new Loading();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.tsReload = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsPay = new System.Windows.Forms.ToolStripButton();
			this.tsCancelUnpay = new System.Windows.Forms.ToolStripButton();
			this.tsResign = new System.Windows.Forms.ToolStripButton();
			this.tsResignChangeTs = new System.Windows.Forms.ToolStripButton();
			this.tsRefund = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsCleanLocalHistory = new System.Windows.Forms.ToolStripButton();
			this.olist = new TOBA.UI.Controls.Order.OrderList();
			this.pEmpty.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// pEmpty
			// 
			this.pEmpty.BackColor = System.Drawing.Color.White;
			this.pEmpty.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pEmpty.Controls.Add(this.label1);
			this.pEmpty.Controls.Add(this.pictureBox1);
			this.pEmpty.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pEmpty.Location = new System.Drawing.Point(238, 163);
			this.pEmpty.Name = "pEmpty";
			this.pEmpty.Size = new System.Drawing.Size(353, 84);
			this.pEmpty.TabIndex = 3;
			this.pEmpty.Visible = false;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(76, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(128, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "嗯哼，没有查到订单！";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TOBA.Properties.Resources.QQ20180813110824;
			this.pictureBox1.Location = new System.Drawing.Point(13, 15);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(55, 55);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// loading
			// 
			this.loading.BackColor = System.Drawing.Color.White;
			this.loading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.loading.LoadingText = "正在加载中....";
			this.loading.Location = new System.Drawing.Point(227, 163);
			this.loading.Name = "loading";
			this.loading.Size = new System.Drawing.Size(364, 84);
			this.loading.TabIndex = 2;
			this.loading.TextInLoading = "正在加载中....";
			this.loading.TextLoadingError = "加载失败....";
			this.loading.TextLoadingOk = "加载成功";
			this.loading.Visible = false;
			// 
			// toolStrip1
			// 
			this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsReload,
            this.toolStripSeparator1,
            this.tsPay,
            this.tsCancelUnpay,
            this.tsResign,
            this.tsResignChangeTs,
            this.tsRefund,
            this.toolStripSeparator2,
            this.tsCleanLocalHistory});
			this.toolStrip1.Location = new System.Drawing.Point(0, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.toolStrip1.Size = new System.Drawing.Size(842, 25);
			this.toolStrip1.TabIndex = 0;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// tsReload
			// 
			this.tsReload.Image = global::TOBA.Properties.Resources.cou_16_refresh;
			this.tsReload.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsReload.Name = "tsReload";
			this.tsReload.Size = new System.Drawing.Size(76, 22);
			this.tsReload.Text = "刷新列表";
			this.tsReload.ToolTipText = "重新加载当前列表";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// tsPay
			// 
			this.tsPay.Image = global::TOBA.Properties.Resources.buy_16;
			this.tsPay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsPay.Name = "tsPay";
			this.tsPay.Size = new System.Drawing.Size(91, 22);
			this.tsPay.Text = "继续支付(&P)";
			// 
			// tsCancelUnpay
			// 
			this.tsCancelUnpay.Image = global::TOBA.Properties.Resources.cou_16_block;
			this.tsCancelUnpay.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCancelUnpay.Name = "tsCancelUnpay";
			this.tsCancelUnpay.Size = new System.Drawing.Size(52, 22);
			this.tsCancelUnpay.Text = "取消";
			// 
			// tsResign
			// 
			this.tsResign.Image = global::TOBA.Properties.Resources.pencil_16;
			this.tsResign.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsResign.Name = "tsResign";
			this.tsResign.Size = new System.Drawing.Size(52, 22);
			this.tsResign.Text = "改签";
			this.tsResign.ToolTipText = "改签所选择的车票";
			// 
			// tsResignChangeTs
			// 
			this.tsResignChangeTs.Image = global::TOBA.Properties.Resources.pencil_16;
			this.tsResignChangeTs.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsResignChangeTs.Name = "tsResignChangeTs";
			this.tsResignChangeTs.Size = new System.Drawing.Size(108, 22);
			this.tsResignChangeTs.Text = "改签(变更到站)";
			this.tsResignChangeTs.ToolTipText = "改签所选择的车票";
			// 
			// tsRefund
			// 
			this.tsRefund.Image = global::TOBA.Properties.Resources.trash_16;
			this.tsRefund.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsRefund.Name = "tsRefund";
			this.tsRefund.Size = new System.Drawing.Size(52, 22);
			this.tsRefund.Text = "退票";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			// 
			// tsCleanLocalHistory
			// 
			this.tsCleanLocalHistory.Image = global::TOBA.Properties.Resources.cleanmgr_113;
			this.tsCleanLocalHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsCleanLocalHistory.Name = "tsCleanLocalHistory";
			this.tsCleanLocalHistory.Size = new System.Drawing.Size(124, 22);
			this.tsCleanLocalHistory.Text = "清空本地历史订单";
			// 
			// olist
			// 
			this.olist.BackColor = System.Drawing.Color.White;
			// 
			// 
			// 
			this.olist.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.olist.DisabledBackColor = System.Drawing.Color.Empty;
			this.olist.Dock = System.Windows.Forms.DockStyle.Fill;
			this.olist.FocusCuesEnabled = false;
			this.olist.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.olist.FullRowSelect = true;
			this.olist.Location = new System.Drawing.Point(0, 25);
			this.olist.Name = "olist";
			this.olist.Size = new System.Drawing.Size(842, 365);
			this.olist.TabIndex = 1;
			this.olist.UseCompatibleStateImageBehavior = false;
			this.olist.View = System.Windows.Forms.View.Details;
			// 
			// OrderPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.olist);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.loading);
			this.Controls.Add(this.pEmpty);
			this.Name = "OrderPanel";
			this.Size = new System.Drawing.Size(842, 390);
			this.pEmpty.ResumeLayout(false);
			this.pEmpty.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton tsReload;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsResign;
		private OrderList olist;
		private Loading loading;
		private System.Windows.Forms.Panel pEmpty;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.ToolStripButton tsRefund;
		private System.Windows.Forms.ToolStripButton tsCancelUnpay;
		private System.Windows.Forms.ToolStripButton tsPay;
		private System.Windows.Forms.ToolStripButton tsResignChangeTs;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsCleanLocalHistory;
	}
}
