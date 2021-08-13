namespace TOBA.UI.Dialogs.Notification
{
	partial class SoftwareConflictionWarning
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lstProgs = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.lnkOpenUninstall = new System.Windows.Forms.LinkLabel();
			this.chkNotShowAgain = new System.Windows.Forms.CheckBox();
			this.btnContinue = new System.Windows.Forms.Button();
			this.btnUninstall = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TOBA.Properties.Resources.onebit_36;
			this.pictureBox1.Location = new System.Drawing.Point(12, 15);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(66, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(478, 73);
			this.label1.TabIndex = 1;
			this.label1.Text = "助手检测到您的系统中安装有下列软件。这些软件的安装将可能导致助手无法正常工作甚至锁死，建议在运行前卸载下列软件，再重新启动助手。\r\n您也可以暂时不卸载，但请注意，" +
    "当助手后续无法正常工作（如卡在某个界面无法继续）时，请使用任务管理器强行结束进程，并在卸载下列软件后重新启动助手。";
			// 
			// lstProgs
			// 
			this.lstProgs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.lstProgs.FullRowSelect = true;
			this.lstProgs.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lstProgs.Location = new System.Drawing.Point(14, 93);
			this.lstProgs.Name = "lstProgs";
			this.lstProgs.Size = new System.Drawing.Size(531, 70);
			this.lstProgs.SmallImageList = this.imageList1;
			this.lstProgs.TabIndex = 2;
			this.lstProgs.UseCompatibleStateImageBehavior = false;
			this.lstProgs.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 522;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(20, 20);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// lnkOpenUninstall
			// 
			this.lnkOpenUninstall.AutoSize = true;
			this.lnkOpenUninstall.Location = new System.Drawing.Point(12, 177);
			this.lnkOpenUninstall.Name = "lnkOpenUninstall";
			this.lnkOpenUninstall.Size = new System.Drawing.Size(107, 12);
			this.lnkOpenUninstall.TabIndex = 3;
			this.lnkOpenUninstall.TabStop = true;
			this.lnkOpenUninstall.Text = "打开添加/卸载程序";
			this.lnkOpenUninstall.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOpenUninstall_LinkClicked);
			// 
			// chkNotShowAgain
			// 
			this.chkNotShowAgain.AutoSize = true;
			this.chkNotShowAgain.Location = new System.Drawing.Point(14, 196);
			this.chkNotShowAgain.Name = "chkNotShowAgain";
			this.chkNotShowAgain.Size = new System.Drawing.Size(108, 16);
			this.chkNotShowAgain.TabIndex = 4;
			this.chkNotShowAgain.Text = "不再显示此警告";
			this.chkNotShowAgain.UseVisualStyleBackColor = true;
			this.chkNotShowAgain.CheckedChanged += new System.EventHandler(this.chkNotShowAgain_CheckedChanged);
			// 
			// btnContinue
			// 
			this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnContinue.Image = global::TOBA.Properties.Resources.block_16;
			this.btnContinue.Location = new System.Drawing.Point(300, 180);
			this.btnContinue.Name = "btnContinue";
			this.btnContinue.Size = new System.Drawing.Size(119, 32);
			this.btnContinue.TabIndex = 5;
			this.btnContinue.Text = "继续但不卸载";
			this.btnContinue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnContinue.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnContinue.UseVisualStyleBackColor = true;
			this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
			// 
			// btnUninstall
			// 
			this.btnUninstall.Image = global::TOBA.Properties.Resources.tick_16;
			this.btnUninstall.Location = new System.Drawing.Point(425, 180);
			this.btnUninstall.Name = "btnUninstall";
			this.btnUninstall.Size = new System.Drawing.Size(119, 32);
			this.btnUninstall.TabIndex = 5;
			this.btnUninstall.Text = "退出并卸载";
			this.btnUninstall.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnUninstall.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnUninstall.UseVisualStyleBackColor = true;
			this.btnUninstall.Click += new System.EventHandler(this.btnUninstall_Click);
			// 
			// SoftwareConflictionWarning
			// 
			this.AcceptButton = this.btnUninstall;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnContinue;
			this.ClientSize = new System.Drawing.Size(554, 223);
			this.ControlBox = false;
			this.Controls.Add(this.btnUninstall);
			this.Controls.Add(this.btnContinue);
			this.Controls.Add(this.chkNotShowAgain);
			this.Controls.Add(this.lnkOpenUninstall);
			this.Controls.Add(this.lstProgs);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.Name = "SoftwareConflictionWarning";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "软件冲突警告";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView lstProgs;
		private System.Windows.Forms.LinkLabel lnkOpenUninstall;
		private System.Windows.Forms.CheckBox chkNotShowAgain;
		private System.Windows.Forms.Button btnContinue;
		private System.Windows.Forms.Button btnUninstall;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ColumnHeader columnHeader1;
	}
}