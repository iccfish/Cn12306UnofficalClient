namespace TOBA.UI.Dialogs.BackupOrder
{
	partial class SubmitBackupOrderDlg
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pLk = new System.Windows.Forms.Panel();
			this.flpLkList = new System.Windows.Forms.FlowLayoutPanel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.lnkLkSelNone = new System.Windows.Forms.LinkLabel();
			this.lnkLkSelAll = new System.Windows.Forms.LinkLabel();
			this.label2 = new System.Windows.Forms.Label();
			this.tList = new DevComponents.DotNetBar.Controls.ListViewEx();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ilTrain = new System.Windows.Forms.ImageList(this.components);
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.ps = new TOBA.UI.Controls.Passenger.PassengerSelector();
			this.pPassenger = new System.Windows.Forms.FlowLayoutPanel();
			this.pBottom = new System.Windows.Forms.Panel();
			this.pQueue = new DevComponents.DotNetBar.PanelEx();
			this.lblQueueTip = new DevComponents.DotNetBar.LabelX();
			this.cpQueue = new DevComponents.DotNetBar.Controls.CircularProgress();
			this.label1 = new System.Windows.Forms.Label();
			this.backupEndDate = new System.Windows.Forms.DateTimePicker();
			this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
			this.panel1.SuspendLayout();
			this.pLk.SuspendLayout();
			this.panel4.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.pBottom.SuspendLayout();
			this.pQueue.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.pLk);
			this.panel1.Controls.Add(this.tList);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(697, 206);
			this.panel1.TabIndex = 8;
			// 
			// pLk
			// 
			this.pLk.BackColor = System.Drawing.Color.White;
			this.pLk.Controls.Add(this.flpLkList);
			this.pLk.Controls.Add(this.panel4);
			this.pLk.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pLk.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pLk.Location = new System.Drawing.Point(0, 136);
			this.pLk.Name = "pLk";
			this.pLk.Size = new System.Drawing.Size(697, 70);
			this.pLk.TabIndex = 14;
			// 
			// flpLkList
			// 
			this.flpLkList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flpLkList.Location = new System.Drawing.Point(0, 27);
			this.flpLkList.Name = "flpLkList";
			this.flpLkList.Size = new System.Drawing.Size(697, 43);
			this.flpLkList.TabIndex = 1;
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(139)))), ((int)(((byte)(75)))));
			this.panel4.Controls.Add(this.lnkLkSelNone);
			this.panel4.Controls.Add(this.lnkLkSelAll);
			this.panel4.Controls.Add(this.label2);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.ForeColor = System.Drawing.Color.White;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(697, 27);
			this.panel4.TabIndex = 0;
			// 
			// lnkLkSelNone
			// 
			this.lnkLkSelNone.AutoSize = true;
			this.lnkLkSelNone.LinkColor = System.Drawing.Color.White;
			this.lnkLkSelNone.Location = new System.Drawing.Point(662, 6);
			this.lnkLkSelNone.Name = "lnkLkSelNone";
			this.lnkLkSelNone.Size = new System.Drawing.Size(32, 17);
			this.lnkLkSelNone.TabIndex = 1;
			this.lnkLkSelNone.TabStop = true;
			this.lnkLkSelNone.Text = "不选";
			// 
			// lnkLkSelAll
			// 
			this.lnkLkSelAll.AutoSize = true;
			this.lnkLkSelAll.LinkColor = System.Drawing.Color.White;
			this.lnkLkSelAll.Location = new System.Drawing.Point(624, 6);
			this.lnkLkSelAll.Name = "lnkLkSelAll";
			this.lnkLkSelAll.Size = new System.Drawing.Size(32, 17);
			this.lnkLkSelAll.TabIndex = 1;
			this.lnkLkSelAll.TabStop = true;
			this.lnkLkSelAll.Text = "全选";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "允许候补临客车次";
			// 
			// tList
			// 
			// 
			// 
			// 
			this.tList.Border.Class = "ListViewBorder";
			this.tList.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.tList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
			this.tList.DisabledBackColor = System.Drawing.Color.Empty;
			this.tList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tList.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tList.FullRowSelect = true;
			this.tList.HideSelection = false;
			this.tList.Location = new System.Drawing.Point(0, 0);
			this.tList.Name = "tList";
			this.tList.Size = new System.Drawing.Size(697, 206);
			this.tList.SmallImageList = this.ilTrain;
			this.tList.TabIndex = 10;
			this.tList.UseCompatibleStateImageBehavior = false;
			this.tList.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "日期";
			this.columnHeader1.Width = 83;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "发站";
			this.columnHeader2.Width = 92;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "到站";
			this.columnHeader3.Width = 90;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "车次";
			this.columnHeader4.Width = 96;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "席别";
			this.columnHeader5.Width = 59;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "发车时间";
			this.columnHeader6.Width = 88;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "到站时间";
			this.columnHeader7.Width = 88;
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "历时";
			this.columnHeader8.Width = 91;
			// 
			// ilTrain
			// 
			this.ilTrain.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.ilTrain.ImageSize = new System.Drawing.Size(1, 20);
			this.ilTrain.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.splitContainer1.Location = new System.Drawing.Point(0, 206);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.ps);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.BackColor = System.Drawing.Color.White;
			this.splitContainer1.Panel2.Controls.Add(this.pPassenger);
			this.splitContainer1.Size = new System.Drawing.Size(697, 273);
			this.splitContainer1.SplitterDistance = 165;
			this.splitContainer1.TabIndex = 11;
			// 
			// ps
			// 
			this.ps.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ps.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ps.Image = null;
			this.ps.Location = new System.Drawing.Point(0, 0);
			this.ps.Margin = new System.Windows.Forms.Padding(4);
			this.ps.Name = "ps";
			this.ps.SearchKey = null;
			this.ps.ShowAddLink = false;
			this.ps.ShowOnlyStudent = false;
			this.ps.Size = new System.Drawing.Size(697, 165);
			this.ps.TabIndex = 0;
			// 
			// pPassenger
			// 
			this.pPassenger.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pPassenger.Location = new System.Drawing.Point(0, 0);
			this.pPassenger.Name = "pPassenger";
			this.pPassenger.Size = new System.Drawing.Size(697, 104);
			this.pPassenger.TabIndex = 0;
			// 
			// pBottom
			// 
			this.pBottom.Controls.Add(this.pQueue);
			this.pBottom.Controls.Add(this.label1);
			this.pBottom.Controls.Add(this.backupEndDate);
			this.pBottom.Controls.Add(this.btnSubmit);
			this.pBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pBottom.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pBottom.Location = new System.Drawing.Point(0, 479);
			this.pBottom.Name = "pBottom";
			this.pBottom.Size = new System.Drawing.Size(697, 83);
			this.pBottom.TabIndex = 9;
			// 
			// pQueue
			// 
			this.pQueue.CanvasColor = System.Drawing.SystemColors.Control;
			this.pQueue.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.pQueue.Controls.Add(this.lblQueueTip);
			this.pQueue.Controls.Add(this.cpQueue);
			this.pQueue.DisabledBackColor = System.Drawing.Color.Empty;
			this.pQueue.Location = new System.Drawing.Point(0, 47);
			this.pQueue.Name = "pQueue";
			this.pQueue.Size = new System.Drawing.Size(309, 36);
			this.pQueue.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.pQueue.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.pQueue.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.pQueue.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.pQueue.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.pQueue.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.pQueue.Style.GradientAngle = 90;
			this.pQueue.TabIndex = 10;
			this.pQueue.Visible = false;
			// 
			// lblQueueTip
			// 
			// 
			// 
			// 
			this.lblQueueTip.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblQueueTip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblQueueTip.Location = new System.Drawing.Point(40, 5);
			this.lblQueueTip.Name = "lblQueueTip";
			this.lblQueueTip.Size = new System.Drawing.Size(266, 28);
			this.lblQueueTip.TabIndex = 1;
			this.lblQueueTip.Text = "订单已提交，正在排队……";
			// 
			// cpQueue
			// 
			// 
			// 
			// 
			this.cpQueue.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.cpQueue.Location = new System.Drawing.Point(3, 3);
			this.cpQueue.Name = "cpQueue";
			this.cpQueue.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Donut;
			this.cpQueue.Size = new System.Drawing.Size(31, 32);
			this.cpQueue.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.cpQueue.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(3, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(106, 22);
			this.label1.TabIndex = 2;
			this.label1.Text = "兑现截止时间";
			// 
			// backupEndDate
			// 
			this.backupEndDate.CustomFormat = "MM\'月\'dd\'日\' HH\'时\'mm\'分\'";
			this.backupEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.backupEndDate.Location = new System.Drawing.Point(115, 9);
			this.backupEndDate.Name = "backupEndDate";
			this.backupEndDate.Size = new System.Drawing.Size(194, 29);
			this.backupEndDate.TabIndex = 3;
			// 
			// btnSubmit
			// 
			this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSubmit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnSubmit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnSubmit.Location = new System.Drawing.Point(518, 15);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.Size = new System.Drawing.Size(167, 58);
			this.btnSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnSubmit.TabIndex = 0;
			this.btnSubmit.Text = "提交订单";
			// 
			// SubmitBackupOrderDlg
			// 
			this.AcceptButton = this.btnSubmit;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(697, 562);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.pBottom);
			this.Controls.Add(this.panel1);
			this.DoubleBuffered = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SubmitBackupOrderDlg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "提交候补订单";
			this.panel1.ResumeLayout(false);
			this.pLk.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.pBottom.ResumeLayout(false);
			this.pBottom.PerformLayout();
			this.pQueue.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private DevComponents.DotNetBar.Controls.ListViewEx tList;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private Controls.Passenger.PassengerSelector ps;
		private System.Windows.Forms.Panel pBottom;
		private DevComponents.DotNetBar.ButtonX btnSubmit;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.DateTimePicker backupEndDate;
		private DevComponents.DotNetBar.PanelEx pQueue;
		private DevComponents.DotNetBar.LabelX lblQueueTip;
		private DevComponents.DotNetBar.Controls.CircularProgress cpQueue;
		private System.Windows.Forms.Panel pLk;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.LinkLabel lnkLkSelNone;
		private System.Windows.Forms.LinkLabel lnkLkSelAll;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.FlowLayoutPanel flpLkList;
		private System.Windows.Forms.FlowLayoutPanel pPassenger;
		private System.Windows.Forms.ImageList ilTrain;
	}
}