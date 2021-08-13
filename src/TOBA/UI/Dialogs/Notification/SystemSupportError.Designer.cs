namespace TOBA.UI.Dialogs.Notification
{
	partial class SystemSupportError
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
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblTitle = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.lblError = new System.Windows.Forms.Label();
			this.lblMessage = new System.Windows.Forms.Label();
			this.btnSysConfig = new System.Windows.Forms.Button();
			this.btnUseDiagMode = new System.Windows.Forms.Button();
			this.gb = new System.Windows.Forms.GroupBox();
			this.label4 = new System.Windows.Forms.Label();
			this.lnkVisitForum = new System.Windows.Forms.LinkLabel();
			this.btnOpenLocation = new System.Windows.Forms.Button();
			this.lblFile = new System.Windows.Forms.Label();
			this.lblAdvice = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.lblInfo = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lnkHelp = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.gb.SuspendLayout();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TOBA.Properties.Resources.cou_64_warning;
			this.pictureBox1.Location = new System.Drawing.Point(14, 17);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(69, 66);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// lblTitle
			// 
			this.lblTitle.Location = new System.Drawing.Point(89, 17);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(491, 81);
			this.lblTitle.TabIndex = 1;
			this.lblTitle.Text = "无法初始化运行，请尝试点击『软件设置』并设置相关网络选项。12306过于缓慢或无法访问时，也会出现此问题。您可以尝试启动诊断模式来看看是否能自动确定原因。";
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Location = new System.Drawing.Point(452, 320);
			this.btnOk.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(130, 30);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "确定(&O)";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// lblError
			// 
			this.lblError.AutoSize = true;
			this.lblError.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblError.Location = new System.Drawing.Point(89, 58);
			this.lblError.Name = "lblError";
			this.lblError.Size = new System.Drawing.Size(68, 17);
			this.lblError.TabIndex = 4;
			this.lblError.Text = "错误信息：";
			// 
			// lblMessage
			// 
			this.lblMessage.ForeColor = System.Drawing.Color.Red;
			this.lblMessage.Location = new System.Drawing.Point(163, 58);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(412, 63);
			this.lblMessage.TabIndex = 5;
			// 
			// btnSysConfig
			// 
			this.btnSysConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSysConfig.Image = global::TOBA.Properties.Resources.cou_16_process;
			this.btnSysConfig.Location = new System.Drawing.Point(12, 320);
			this.btnSysConfig.Name = "btnSysConfig";
			this.btnSysConfig.Size = new System.Drawing.Size(112, 30);
			this.btnSysConfig.TabIndex = 6;
			this.btnSysConfig.Text = "软件设置";
			this.btnSysConfig.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSysConfig.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSysConfig.UseVisualStyleBackColor = true;
			this.btnSysConfig.Click += new System.EventHandler(this.btnSysConfig_Click);
			// 
			// btnUseDiagMode
			// 
			this.btnUseDiagMode.Font = new System.Drawing.Font("Microsoft YaHei", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnUseDiagMode.ForeColor = System.Drawing.Color.Red;
			this.btnUseDiagMode.Image = global::TOBA.Properties.Resources.onebit_16;
			this.btnUseDiagMode.Location = new System.Drawing.Point(165, 167);
			this.btnUseDiagMode.Name = "btnUseDiagMode";
			this.btnUseDiagMode.Size = new System.Drawing.Size(266, 83);
			this.btnUseDiagMode.TabIndex = 8;
			this.btnUseDiagMode.Text = "获得日志文件";
			this.btnUseDiagMode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnUseDiagMode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnUseDiagMode.UseVisualStyleBackColor = true;
			// 
			// gb
			// 
			this.gb.Controls.Add(this.label4);
			this.gb.Controls.Add(this.lnkVisitForum);
			this.gb.Controls.Add(this.btnOpenLocation);
			this.gb.Controls.Add(this.lblFile);
			this.gb.Controls.Add(this.lblAdvice);
			this.gb.Controls.Add(this.label7);
			this.gb.Controls.Add(this.lblInfo);
			this.gb.Controls.Add(this.label5);
			this.gb.Controls.Add(this.label3);
			this.gb.Location = new System.Drawing.Point(16, 146);
			this.gb.Name = "gb";
			this.gb.Size = new System.Drawing.Size(565, 144);
			this.gb.TabIndex = 9;
			this.gb.TabStop = false;
			this.gb.Text = "诊断信息";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 123);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(553, 21);
			this.label4.TabIndex = 12;
			this.label4.Text = "如果自动诊断无法确定问题, 或者你不会操作或操作了也无法解决问题, 请访问论坛并按照介绍操作.";
			// 
			// lnkVisitForum
			// 
			this.lnkVisitForum.AutoSize = true;
			this.lnkVisitForum.Location = new System.Drawing.Point(493, 78);
			this.lnkVisitForum.Name = "lnkVisitForum";
			this.lnkVisitForum.Size = new System.Drawing.Size(56, 17);
			this.lnkVisitForum.TabIndex = 11;
			this.lnkVisitForum.TabStop = true;
			this.lnkVisitForum.Text = "访问论坛";
			// 
			// btnOpenLocation
			// 
			this.btnOpenLocation.Image = global::TOBA.Properties.Resources.flag_16;
			this.btnOpenLocation.Location = new System.Drawing.Point(436, 27);
			this.btnOpenLocation.Name = "btnOpenLocation";
			this.btnOpenLocation.Size = new System.Drawing.Size(113, 23);
			this.btnOpenLocation.TabIndex = 10;
			this.btnOpenLocation.Text = "打开位置";
			this.btnOpenLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOpenLocation.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOpenLocation.UseVisualStyleBackColor = true;
			// 
			// lblFile
			// 
			this.lblFile.AutoSize = true;
			this.lblFile.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblFile.Location = new System.Drawing.Point(104, 27);
			this.lblFile.Name = "lblFile";
			this.lblFile.Size = new System.Drawing.Size(56, 17);
			this.lblFile.TabIndex = 9;
			this.lblFile.Text = "日志文件";
			// 
			// lblAdvice
			// 
			this.lblAdvice.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblAdvice.ForeColor = System.Drawing.Color.Blue;
			this.lblAdvice.Location = new System.Drawing.Point(104, 78);
			this.lblAdvice.Name = "lblAdvice";
			this.lblAdvice.Size = new System.Drawing.Size(383, 58);
			this.lblAdvice.TabIndex = 9;
			this.lblAdvice.Text = "诊断信息";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label7.Location = new System.Drawing.Point(23, 78);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(56, 17);
			this.label7.TabIndex = 9;
			this.label7.Text = "操作建议";
			// 
			// lblInfo
			// 
			this.lblInfo.AutoSize = true;
			this.lblInfo.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblInfo.ForeColor = System.Drawing.Color.Red;
			this.lblInfo.Location = new System.Drawing.Point(104, 51);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(56, 17);
			this.lblInfo.TabIndex = 9;
			this.lblInfo.Text = "诊断信息";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label5.Location = new System.Drawing.Point(23, 51);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 17);
			this.label5.TabIndex = 9;
			this.label5.Text = "诊断信息";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(23, 27);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 17);
			this.label3.TabIndex = 9;
			this.label3.Text = "日志文件";
			// 
			// lnkHelp
			// 
			this.lnkHelp.Image = global::TOBA.Properties.Resources.cou_16_help;
			this.lnkHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnkHelp.LinkColor = System.Drawing.Color.RoyalBlue;
			this.lnkHelp.Location = new System.Drawing.Point(481, 125);
			this.lnkHelp.Name = "lnkHelp";
			this.lnkHelp.Size = new System.Drawing.Size(99, 18);
			this.lnkHelp.TabIndex = 12;
			this.lnkHelp.TabStop = true;
			this.lnkHelp.Text = "查看更多信息";
			this.lnkHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lnkHelp.Visible = false;
			// 
			// SystemSupportError
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(594, 363);
			this.Controls.Add(this.lnkHelp);
			this.Controls.Add(this.btnUseDiagMode);
			this.Controls.Add(this.gb);
			this.Controls.Add(this.btnSysConfig);
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.lblError);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.lblTitle);
			this.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SystemSupportError";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "订票助手无法完成初始化";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.gb.ResumeLayout(false);
			this.gb.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Label lblError;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.Button btnSysConfig;
		private System.Windows.Forms.Button btnUseDiagMode;
		private System.Windows.Forms.GroupBox gb;
		private System.Windows.Forms.LinkLabel lnkVisitForum;
		private System.Windows.Forms.Button btnOpenLocation;
		private System.Windows.Forms.Label lblFile;
		private System.Windows.Forms.Label lblAdvice;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel lnkHelp;
	}
}