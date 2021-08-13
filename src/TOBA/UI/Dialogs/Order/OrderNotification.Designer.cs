namespace TOBA.UI.Dialogs.Order
{
	using Controls.Common;

	partial class OrderNotification
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
			this.imglist = new System.Windows.Forms.ImageList(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.pbAnimate = new System.Windows.Forms.PictureBox();
			this.lblTimeInfo = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblTo = new System.Windows.Forms.Label();
			this.lblDate = new System.Windows.Forms.Label();
			this.lblTrainCode = new System.Windows.Forms.Label();
			this.lblFrom = new System.Windows.Forms.Label();
			this.btnOK = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.lblError = new System.Windows.Forms.Label();
			this.lt = new LoadingTip();
			this.tipEx = new System.Windows.Forms.TextBox();
			this.lstPas = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbAnimate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// imglist
			// 
			this.imglist.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imglist.ImageSize = new System.Drawing.Size(24, 24);
			this.imglist.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.Controls.Add(this.pbAnimate);
			this.panel1.Controls.Add(this.lblTimeInfo);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Controls.Add(this.lblTo);
			this.panel1.Controls.Add(this.lblDate);
			this.panel1.Controls.Add(this.lblTrainCode);
			this.panel1.Controls.Add(this.lblFrom);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(683, 65);
			this.panel1.TabIndex = 3;
			// 
			// pbAnimate
			// 
			this.pbAnimate.Image = global::TOBA.Properties.Resources.lxh_happy__2_;
			this.pbAnimate.Location = new System.Drawing.Point(21, 0);
			this.pbAnimate.Name = "pbAnimate";
			this.pbAnimate.Size = new System.Drawing.Size(65, 65);
			this.pbAnimate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbAnimate.TabIndex = 3;
			this.pbAnimate.TabStop = false;
			// 
			// lblTimeInfo
			// 
			this.lblTimeInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTimeInfo.Location = new System.Drawing.Point(507, 9);
			this.lblTimeInfo.Name = "lblTimeInfo";
			this.lblTimeInfo.Size = new System.Drawing.Size(176, 54);
			this.lblTimeInfo.TabIndex = 2;
			this.lblTimeInfo.Text = "出发 {0}\r\n到达 {1}\r\n需时 {2}";
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
			// btnOK
			// 
			this.btnOK.BackColor = System.Drawing.Color.RoyalBlue;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnOK.FlatAppearance.BorderSize = 0;
			this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOK.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnOK.ForeColor = System.Drawing.Color.White;
			this.btnOK.Image = global::TOBA.Properties.Resources.cou_32_accept;
			this.btnOK.Location = new System.Drawing.Point(0, 411);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(683, 36);
			this.btnOK.TabIndex = 6;
			this.btnOK.Text = "乖~~戳我去付款啦~~要截图留念请速度并打码哦~~~";
			this.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOK.UseVisualStyleBackColor = false;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.panel3);
			this.panel2.Controls.Add(this.lt);
			this.panel2.Controls.Add(this.tipEx);
			this.panel2.Controls.Add(this.lstPas);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 65);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(3);
			this.panel2.Size = new System.Drawing.Size(683, 346);
			this.panel2.TabIndex = 10;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.lblError);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(3, 173);
			this.panel3.Name = "panel3";
			this.panel3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
			this.panel3.Size = new System.Drawing.Size(677, 92);
			this.panel3.TabIndex = 13;
			// 
			// lblError
			// 
			this.lblError.BackColor = System.Drawing.SystemColors.Window;
			this.lblError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblError.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblError.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblError.ForeColor = System.Drawing.Color.Red;
			this.lblError.Location = new System.Drawing.Point(0, 3);
			this.lblError.Margin = new System.Windows.Forms.Padding(3);
			this.lblError.Name = "lblError";
			this.lblError.Padding = new System.Windows.Forms.Padding(10);
			this.lblError.Size = new System.Drawing.Size(677, 86);
			this.lblError.TabIndex = 7;
			this.lblError.Text = "订单提交错误";
			// 
			// lt
			// 
			this.lt.BackColor = System.Drawing.Color.White;
			this.lt.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lt.LoadingImage = global::TOBA.Properties.Resources._32px_loading_1;
			this.lt.Location = new System.Drawing.Point(144, 271);
			this.lt.Name = "lt";
			this.lt.Size = new System.Drawing.Size(402, 61);
			this.lt.TabIndex = 10;
			this.lt.Visible = false;
			// 
			// tipEx
			// 
			this.tipEx.BackColor = System.Drawing.Color.White;
			this.tipEx.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.tipEx.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tipEx.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tipEx.ForeColor = System.Drawing.Color.Green;
			this.tipEx.Location = new System.Drawing.Point(3, 265);
			this.tipEx.Multiline = true;
			this.tipEx.Name = "tipEx";
			this.tipEx.ReadOnly = true;
			this.tipEx.Size = new System.Drawing.Size(677, 78);
			this.tipEx.TabIndex = 11;
			this.tipEx.Visible = false;
			// 
			// lstPas
			// 
			this.lstPas.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
			this.lstPas.Dock = System.Windows.Forms.DockStyle.Top;
			this.lstPas.FullRowSelect = true;
			this.lstPas.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstPas.HideSelection = false;
			this.lstPas.Location = new System.Drawing.Point(3, 3);
			this.lstPas.Name = "lstPas";
			this.lstPas.Size = new System.Drawing.Size(677, 170);
			this.lstPas.SmallImageList = this.imglist;
			this.lstPas.TabIndex = 12;
			this.lstPas.UseCompatibleStateImageBehavior = false;
			this.lstPas.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "票种";
			this.columnHeader1.Width = 104;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "席别";
			this.columnHeader2.Width = 79;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "乘客";
			this.columnHeader3.Width = 104;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "证件类型";
			this.columnHeader4.Width = 86;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "证件号码";
			this.columnHeader5.Width = 175;
			// 
			// OrderNotification
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.btnOK;
			this.ClientSize = new System.Drawing.Size(683, 447);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnOK);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OrderNotification";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "订单通知信息";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbAnimate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pbAnimate;
		private System.Windows.Forms.Label lblTimeInfo;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblTo;
		private System.Windows.Forms.Label lblDate;
		private System.Windows.Forms.Label lblTrainCode;
		private System.Windows.Forms.Label lblFrom;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ImageList imglist;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView lstPas;
		private System.Windows.Forms.TextBox tipEx;
		private LoadingTip lt;
		private System.Windows.Forms.Label lblError;
	}
}