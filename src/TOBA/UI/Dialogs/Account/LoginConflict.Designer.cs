namespace TOBA.UI.Dialogs.Account
{
	partial class LoginConflict
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
			this.label1 = new System.Windows.Forms.Label();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.chkNotShowAnyMore = new DevComponents.DotNetBar.Controls.CheckBoxX();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TOBA.Properties.Resources.cou_64_warning;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(64, 64);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.ForeColor = System.Drawing.Color.Crimson;
			this.label1.Location = new System.Drawing.Point(93, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(639, 77);
			this.label1.TabIndex = 1;
			this.label1.Text = "此次登录检测到会话冲突，如果您此时还在其它的软件或浏览器上登录此账号，它们将会被12306无情地注销掉\r\n世界就是这样的残酷，多坑点身份证注册几个账号吧……";
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX1.Location = new System.Drawing.Point(97, 92);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(635, 161);
			this.labelX1.TabIndex = 5;
			this.labelX1.Text = "现在12306已经不允许多次重复登录，同一个账号在别的设备、浏览器或软件中登录时，之前的此帐号登录记录将会被强行注销。\r\n\r\n如果您需要多次登录来提高成功率，那么" +
    "也许您需要多注册几个账号。有媳妇儿吗？把媳妇儿的身份证坑过来。\r\n没媳妇儿的话老爸老妈舅公舅婆的也行。\r\n\r\n如果您遇到身份证被冒用的情形，记得尽快拿着身份证去" +
    "窗口核实。";
			this.labelX1.TextLineAlignment = System.Drawing.StringAlignment.Near;
			this.labelX1.WordWrap = true;
			// 
			// chkNotShowAnyMore
			// 
			// 
			// 
			// 
			this.chkNotShowAnyMore.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.chkNotShowAnyMore.Location = new System.Drawing.Point(583, 218);
			this.chkNotShowAnyMore.Name = "chkNotShowAnyMore";
			this.chkNotShowAnyMore.Size = new System.Drawing.Size(149, 22);
			this.chkNotShowAnyMore.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.chkNotShowAnyMore.TabIndex = 6;
			this.chkNotShowAnyMore.Text = "不再显示此信息";
			// 
			// LoginConflict
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(744, 252);
			this.Controls.Add(this.chkNotShowAnyMore);
			this.Controls.Add(this.labelX1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LoginConflict";
			this.ShowInTaskbar = false;
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "登录冲突警告";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label label1;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.Controls.CheckBoxX chkNotShowAnyMore;
	}
}