namespace TOBA.UI.Dialogs.Order
{
	partial class OrderQueue
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
			this.panel2 = new System.Windows.Forms.Panel();
			this.pbAnimate = new System.Windows.Forms.PictureBox();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblTo = new System.Windows.Forms.Label();
			this.lblDate = new System.Windows.Forms.Label();
			this.lblTrainCode = new System.Windows.Forms.Label();
			this.lblFrom = new System.Windows.Forms.Label();
			this.lstPas = new DevComponents.DotNetBar.Controls.ListViewEx();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imglist = new System.Windows.Forms.ImageList(this.components);
			this.btnCancelQueue = new DevComponents.DotNetBar.ButtonX();
			this.lblTimeInfo = new DevComponents.DotNetBar.LabelX();
			this.btnClose = new DevComponents.DotNetBar.ButtonX();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbAnimate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.pbAnimate);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Controls.Add(this.lblTo);
			this.panel1.Controls.Add(this.lblDate);
			this.panel1.Controls.Add(this.lblTrainCode);
			this.panel1.Controls.Add(this.lblFrom);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(583, 65);
			this.panel1.TabIndex = 3;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.DarkGray;
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 64);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(583, 1);
			this.panel2.TabIndex = 4;
			// 
			// pbAnimate
			// 
			this.pbAnimate.Image = global::TOBA.Properties.Resources.lxh_working;
			this.pbAnimate.Location = new System.Drawing.Point(21, 0);
			this.pbAnimate.Name = "pbAnimate";
			this.pbAnimate.Size = new System.Drawing.Size(65, 65);
			this.pbAnimate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbAnimate.TabIndex = 3;
			this.pbAnimate.TabStop = false;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TOBA.Properties.Resources.cou_32_next;
			this.pictureBox1.Location = new System.Drawing.Point(360, 19);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// lblTo
			// 
			this.lblTo.AutoSize = true;
			this.lblTo.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblTo.ForeColor = System.Drawing.Color.ForestGreen;
			this.lblTo.Location = new System.Drawing.Point(398, 21);
			this.lblTo.Name = "lblTo";
			this.lblTo.Size = new System.Drawing.Size(72, 27);
			this.lblTo.TabIndex = 0;
			this.lblTo.Text = "目标地";
			// 
			// lblDate
			// 
			this.lblDate.AutoSize = true;
			this.lblDate.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblDate.ForeColor = System.Drawing.Color.Brown;
			this.lblDate.Location = new System.Drawing.Point(92, 21);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(52, 27);
			this.lblDate.TabIndex = 0;
			this.lblDate.Text = "日期";
			// 
			// lblTrainCode
			// 
			this.lblTrainCode.AutoSize = true;
			this.lblTrainCode.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblTrainCode.ForeColor = System.Drawing.Color.Brown;
			this.lblTrainCode.Location = new System.Drawing.Point(195, 21);
			this.lblTrainCode.Name = "lblTrainCode";
			this.lblTrainCode.Size = new System.Drawing.Size(52, 27);
			this.lblTrainCode.TabIndex = 0;
			this.lblTrainCode.Text = "车次";
			// 
			// lblFrom
			// 
			this.lblFrom.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblFrom.ForeColor = System.Drawing.Color.ForestGreen;
			this.lblFrom.Location = new System.Drawing.Point(254, 21);
			this.lblFrom.Name = "lblFrom";
			this.lblFrom.Size = new System.Drawing.Size(100, 27);
			this.lblFrom.TabIndex = 0;
			this.lblFrom.Text = "出发地";
			this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lstPas
			// 
			// 
			// 
			// 
			this.lstPas.Border.Class = "ListViewBorder";
			this.lstPas.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lstPas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
			this.lstPas.DisabledBackColor = System.Drawing.Color.Empty;
			this.lstPas.FullRowSelect = true;
			this.lstPas.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstPas.HideSelection = false;
			this.lstPas.Location = new System.Drawing.Point(21, 81);
			this.lstPas.Name = "lstPas";
			this.lstPas.Size = new System.Drawing.Size(551, 150);
			this.lstPas.SmallImageList = this.imglist;
			this.lstPas.TabIndex = 4;
			this.lstPas.UseCompatibleStateImageBehavior = false;
			this.lstPas.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "票种";
			this.columnHeader1.Width = 88;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "席别";
			this.columnHeader2.Width = 56;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "乘客";
			this.columnHeader3.Width = 73;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "证件类型";
			this.columnHeader4.Width = 80;
			// 
			// imglist
			// 
			this.imglist.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imglist.ImageSize = new System.Drawing.Size(24, 24);
			this.imglist.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btnCancelQueue
			// 
			this.btnCancelQueue.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnCancelQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnCancelQueue.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnCancelQueue.Image = global::TOBA.Properties.Resources.cou_16_delete;
			this.btnCancelQueue.Location = new System.Drawing.Point(21, 370);
			this.btnCancelQueue.Name = "btnCancelQueue";
			this.btnCancelQueue.Size = new System.Drawing.Size(105, 27);
			this.btnCancelQueue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnCancelQueue.TabIndex = 5;
			this.btnCancelQueue.Text = "取消排队";
			// 
			// lblTimeInfo
			// 
			// 
			// 
			// 
			this.lblTimeInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblTimeInfo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblTimeInfo.ForeColor = System.Drawing.Color.MediumVioletRed;
			this.lblTimeInfo.Location = new System.Drawing.Point(21, 237);
			this.lblTimeInfo.Name = "lblTimeInfo";
			this.lblTimeInfo.Size = new System.Drawing.Size(550, 115);
			this.lblTimeInfo.TabIndex = 6;
			this.lblTimeInfo.Text = "未出票，正在等待出票。。。。";
			this.lblTimeInfo.TextLineAlignment = System.Drawing.StringAlignment.Near;
			this.lblTimeInfo.WordWrap = true;
			// 
			// btnClose
			// 
			this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnClose.Image = global::TOBA.Properties.Resources.cou_32_block;
			this.btnClose.Location = new System.Drawing.Point(451, 358);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(121, 39);
			this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnClose.TabIndex = 5;
			this.btnClose.Text = "关闭窗口";
			// 
			// OrderQueue
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(583, 409);
			this.Controls.Add(this.lblTimeInfo);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnCancelQueue);
			this.Controls.Add(this.lstPas);
			this.Controls.Add(this.panel1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OrderQueue";
			this.Text = "订单排队中";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbAnimate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pbAnimate;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblTo;
		private System.Windows.Forms.Label lblDate;
		private System.Windows.Forms.Label lblTrainCode;
		private System.Windows.Forms.Label lblFrom;
		private System.Windows.Forms.Panel panel2;
		private DevComponents.DotNetBar.Controls.ListViewEx lstPas;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private DevComponents.DotNetBar.ButtonX btnCancelQueue;
		private DevComponents.DotNetBar.LabelX lblTimeInfo;
		private System.Windows.Forms.ImageList imglist;
		private DevComponents.DotNetBar.ButtonX btnClose;
	}
}