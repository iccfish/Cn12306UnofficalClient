namespace TOBA.UI.Controls.BackupOrder
{
	using Misc;

	partial class BackupOrderContainer
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
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.bar1 = new DevComponents.DotNetBar.Bar();
			this.btnRefresh = new DevComponents.DotNetBar.ButtonItem();
			this.btnPay = new DevComponents.DotNetBar.ButtonItem();
			this.btnCancel = new DevComponents.DotNetBar.ButtonItem();
			this.btnCancelQueue = new DevComponents.DotNetBar.ButtonItem();
			this.orderList = new DevComponents.DotNetBar.Controls.ListViewEx();
			this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colTrain = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colDep = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colArr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colDepTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colArrTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colElap = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colSeat = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colMsg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imgList = new System.Windows.Forms.ImageList(this.components);
			this.pMemo = new System.Windows.Forms.Panel();
			this.lblMemo = new DevComponents.DotNetBar.LabelX();
			this.loading = new Loading();
			this.tip = new System.Windows.Forms.ToolTip(this.components);
			((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
			this.pMemo.SuspendLayout();
			this.SuspendLayout();
			// 
			// bar1
			// 
			this.bar1.AntiAlias = true;
			this.bar1.Dock = System.Windows.Forms.DockStyle.Top;
			this.bar1.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
			this.bar1.IsMaximized = false;
			this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnRefresh,
            this.btnPay,
            this.btnCancel,
            this.btnCancelQueue});
			this.bar1.Location = new System.Drawing.Point(0, 0);
			this.bar1.Name = "bar1";
			this.bar1.Size = new System.Drawing.Size(594, 26);
			this.bar1.Stretch = true;
			this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.bar1.TabIndex = 0;
			this.bar1.TabStop = false;
			this.bar1.Text = "bar1";
			// 
			// btnRefresh
			// 
			this.btnRefresh.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btnRefresh.Image = global::TOBA.Properties.Resources.cou_16_refresh;
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Text = "刷新列表(&R)";
			// 
			// btnPay
			// 
			this.btnPay.BeginGroup = true;
			this.btnPay.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btnPay.Image = global::TOBA.Properties.Resources.asset;
			this.btnPay.Name = "btnPay";
			this.btnPay.Text = "继续支付(&C)";
			// 
			// btnCancel
			// 
			this.btnCancel.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btnCancel.Image = global::TOBA.Properties.Resources.trash_16;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Text = "取消订单(&T)";
			// 
			// btnCancelQueue
			// 
			this.btnCancelQueue.BeginGroup = true;
			this.btnCancelQueue.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
			this.btnCancelQueue.Image = global::TOBA.Properties.Resources._148;
			this.btnCancelQueue.Name = "btnCancelQueue";
			this.btnCancelQueue.Text = "取消排队";
			// 
			// orderList
			// 
			// 
			// 
			// 
			this.orderList.Border.Class = "ListViewBorder";
			this.orderList.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.orderList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colStatus,
            this.colDate,
            this.colTrain,
            this.colDep,
            this.colArr,
            this.colDepTime,
            this.colArrTime,
            this.colElap,
            this.colSeat,
            this.colMsg});
			this.orderList.DisabledBackColor = System.Drawing.Color.Empty;
			this.orderList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.orderList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.orderList.FullRowSelect = true;
			this.orderList.Location = new System.Drawing.Point(0, 26);
			this.orderList.Name = "orderList";
			this.orderList.Size = new System.Drawing.Size(594, 383);
			this.orderList.SmallImageList = this.imgList;
			this.orderList.TabIndex = 1;
			this.orderList.UseCompatibleStateImageBehavior = false;
			this.orderList.View = System.Windows.Forms.View.Details;
			// 
			// colStatus
			// 
			this.colStatus.Text = "状态";
			// 
			// colDate
			// 
			this.colDate.Text = "乘车日期";
			this.colDate.Width = 70;
			// 
			// colTrain
			// 
			this.colTrain.Text = "车次";
			// 
			// colDep
			// 
			this.colDep.Text = "出发";
			// 
			// colArr
			// 
			this.colArr.Text = "到达";
			// 
			// colDepTime
			// 
			this.colDepTime.Text = "出发时间";
			// 
			// colArrTime
			// 
			this.colArrTime.Text = "到达时间";
			// 
			// colElap
			// 
			this.colElap.Text = "历时";
			// 
			// colSeat
			// 
			this.colSeat.Text = "席别";
			// 
			// colMsg
			// 
			this.colMsg.Text = "状态信息";
			// 
			// imgList
			// 
			this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imgList.ImageSize = new System.Drawing.Size(24, 24);
			this.imgList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// pMemo
			// 
			this.pMemo.BackColor = System.Drawing.Color.White;
			this.pMemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pMemo.Controls.Add(this.lblMemo);
			this.pMemo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pMemo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pMemo.Location = new System.Drawing.Point(0, 409);
			this.pMemo.Name = "pMemo";
			this.pMemo.Padding = new System.Windows.Forms.Padding(10);
			this.pMemo.Size = new System.Drawing.Size(594, 94);
			this.pMemo.TabIndex = 2;
			// 
			// lblMemo
			// 
			// 
			// 
			// 
			this.lblMemo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblMemo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblMemo.ForeColor = System.Drawing.Color.Crimson;
			this.lblMemo.Location = new System.Drawing.Point(10, 10);
			this.lblMemo.Name = "lblMemo";
			this.lblMemo.Size = new System.Drawing.Size(572, 72);
			this.lblMemo.TabIndex = 0;
			this.lblMemo.TextLineAlignment = System.Drawing.StringAlignment.Near;
			// 
			// loading
			// 
			this.loading.BackColor = System.Drawing.SystemColors.Window;
			this.loading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.loading.LoadingText = "正在加载中....";
			this.loading.Location = new System.Drawing.Point(124, 221);
			this.loading.Name = "loading";
			this.loading.Size = new System.Drawing.Size(304, 70);
			this.loading.TabIndex = 3;
			this.loading.TextInLoading = "正在加载中....";
			this.loading.TextLoadingError = "加载失败....";
			this.loading.TextLoadingOk = "加载成功";
			// 
			// BackupOrderContainer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.loading);
			this.Controls.Add(this.orderList);
			this.Controls.Add(this.pMemo);
			this.Controls.Add(this.bar1);
			this.Name = "BackupOrderContainer";
			this.Size = new System.Drawing.Size(594, 503);
			((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
			this.pMemo.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.Bar bar1;
		private DevComponents.DotNetBar.ButtonItem btnRefresh;
		private DevComponents.DotNetBar.ButtonItem btnPay;
		private DevComponents.DotNetBar.ButtonItem btnCancel;
		private DevComponents.DotNetBar.Controls.ListViewEx orderList;
		private System.Windows.Forms.ColumnHeader colDate;
		private System.Windows.Forms.ColumnHeader colTrain;
		private System.Windows.Forms.ColumnHeader colDep;
		private System.Windows.Forms.ColumnHeader colArr;
		private System.Windows.Forms.ColumnHeader colDepTime;
		private System.Windows.Forms.ColumnHeader colArrTime;
		private System.Windows.Forms.ColumnHeader colElap;
		private System.Windows.Forms.ColumnHeader colSeat;
		private System.Windows.Forms.ColumnHeader colMsg;
		private System.Windows.Forms.ImageList imgList;
		private System.Windows.Forms.ColumnHeader colStatus;
		private System.Windows.Forms.Panel pMemo;
		private DevComponents.DotNetBar.LabelX lblMemo;
		private DevComponents.DotNetBar.ButtonItem btnCancelQueue;
		private Loading loading;
		private System.Windows.Forms.ToolTip tip;
	}
}
