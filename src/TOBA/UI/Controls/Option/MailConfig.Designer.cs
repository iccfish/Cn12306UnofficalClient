namespace TOBA.UI.Controls.Option
{
	partial class MailConfig
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MailConfig));
			this.panel1 = new System.Windows.Forms.Panel();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.chkEnableMail = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.pConfig = new System.Windows.Forms.Panel();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.chkSsl = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.btnTest = new DevComponents.DotNetBar.ButtonX();
			this.iptServerPort = new DevComponents.Editors.IntegerInput();
			this.txtBody = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.txtTitle = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.txtPwd = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.txtSmtp = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.txtReceivers = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.txtMailAdd = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.label2 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.pConfig.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.iptServerPort)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.labelX1);
			this.panel1.Controls.Add(this.chkEnableMail);
			this.panel1.Controls.Add(this.pConfig);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(530, 360);
			this.panel1.TabIndex = 0;
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX1.Location = new System.Drawing.Point(20, 19);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(504, 37);
			this.labelX1.TabIndex = 5;
			this.labelX1.Text = "邮件发送不一定总是成功，请尽量定时用12306手机客户端查看订单状态。手机端可用邮箱APP或手机系统自带邮件客户端定时收件，及时收到通知";
			this.labelX1.WordWrap = true;
			// 
			// chkEnableMail
			// 
			// 
			// 
			// 
			this.chkEnableMail.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.chkEnableMail.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkEnableMail.Location = new System.Drawing.Point(6, 3);
			this.chkEnableMail.Name = "chkEnableMail";
			this.chkEnableMail.Size = new System.Drawing.Size(518, 20);
			this.chkEnableMail.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.chkEnableMail.TabIndex = 3;
			this.chkEnableMail.Text = "订票成功时启用邮件通知 ";
			// 
			// pConfig
			// 
			this.pConfig.Controls.Add(this.labelX2);
			this.pConfig.Controls.Add(this.chkSsl);
			this.pConfig.Controls.Add(this.btnTest);
			this.pConfig.Controls.Add(this.iptServerPort);
			this.pConfig.Controls.Add(this.txtBody);
			this.pConfig.Controls.Add(this.txtTitle);
			this.pConfig.Controls.Add(this.txtPwd);
			this.pConfig.Controls.Add(this.txtSmtp);
			this.pConfig.Controls.Add(this.txtReceivers);
			this.pConfig.Controls.Add(this.txtMailAdd);
			this.pConfig.Controls.Add(this.label2);
			this.pConfig.Controls.Add(this.label6);
			this.pConfig.Controls.Add(this.label5);
			this.pConfig.Controls.Add(this.label7);
			this.pConfig.Controls.Add(this.label4);
			this.pConfig.Controls.Add(this.label3);
			this.pConfig.Controls.Add(this.label1);
			this.pConfig.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pConfig.Location = new System.Drawing.Point(3, 62);
			this.pConfig.Name = "pConfig";
			this.pConfig.Size = new System.Drawing.Size(524, 295);
			this.pConfig.TabIndex = 4;
			// 
			// labelX2
			// 
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Location = new System.Drawing.Point(3, 216);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(403, 79);
			this.labelX2.TabIndex = 4;
			this.labelX2.Text = resources.GetString("labelX2.Text");
			// 
			// chkSsl
			// 
			// 
			// 
			// 
			this.chkSsl.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.chkSsl.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkSsl.Location = new System.Drawing.Point(372, 85);
			this.chkSsl.Name = "chkSsl";
			this.chkSsl.Size = new System.Drawing.Size(74, 15);
			this.chkSsl.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.chkSsl.TabIndex = 5;
			this.chkSsl.Text = "SSL加密";
			// 
			// btnTest
			// 
			this.btnTest.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnTest.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnTest.Location = new System.Drawing.Point(412, 247);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(109, 42);
			this.btnTest.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnTest.TabIndex = 2;
			this.btnTest.Text = "发送测试邮件";
			// 
			// iptServerPort
			// 
			// 
			// 
			// 
			this.iptServerPort.BackgroundStyle.Class = "DateTimeInputBackground";
			this.iptServerPort.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.iptServerPort.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
			this.iptServerPort.FocusHighlightEnabled = true;
			this.iptServerPort.Location = new System.Drawing.Point(314, 81);
			this.iptServerPort.MaxValue = 65535;
			this.iptServerPort.MinValue = 1;
			this.iptServerPort.Name = "iptServerPort";
			this.iptServerPort.ShowUpDown = true;
			this.iptServerPort.Size = new System.Drawing.Size(52, 23);
			this.iptServerPort.TabIndex = 4;
			this.iptServerPort.Value = 1;
			this.iptServerPort.WatermarkText = "发送服务器端口";
			// 
			// txtBody
			// 
			// 
			// 
			// 
			this.txtBody.Border.Class = "TextBoxBorder";
			this.txtBody.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtBody.FocusHighlightEnabled = true;
			this.txtBody.Location = new System.Drawing.Point(94, 169);
			this.txtBody.Multiline = true;
			this.txtBody.Name = "txtBody";
			this.txtBody.PreventEnterBeep = true;
			this.txtBody.Size = new System.Drawing.Size(352, 45);
			this.txtBody.TabIndex = 7;
			this.txtBody.WatermarkText = "发送的邮件内容，支持占位符。";
			// 
			// txtTitle
			// 
			// 
			// 
			// 
			this.txtTitle.Border.Class = "TextBoxBorder";
			this.txtTitle.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtTitle.FocusHighlightEnabled = true;
			this.txtTitle.Location = new System.Drawing.Point(94, 142);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.PreventEnterBeep = true;
			this.txtTitle.Size = new System.Drawing.Size(352, 23);
			this.txtTitle.TabIndex = 6;
			this.txtTitle.WatermarkText = "发送的邮件标题，支持占位符。";
			// 
			// txtPwd
			// 
			// 
			// 
			// 
			this.txtPwd.Border.Class = "TextBoxBorder";
			this.txtPwd.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtPwd.FocusHighlightEnabled = true;
			this.txtPwd.Location = new System.Drawing.Point(94, 54);
			this.txtPwd.Name = "txtPwd";
			this.txtPwd.PreventEnterBeep = true;
			this.txtPwd.Size = new System.Drawing.Size(352, 23);
			this.txtPwd.TabIndex = 1;
			this.txtPwd.UseSystemPasswordChar = true;
			this.txtPwd.WatermarkText = "发送邮箱的登录密码";
			// 
			// txtSmtp
			// 
			// 
			// 
			// 
			this.txtSmtp.Border.Class = "TextBoxBorder";
			this.txtSmtp.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtSmtp.FocusHighlightEnabled = true;
			this.txtSmtp.Location = new System.Drawing.Point(94, 81);
			this.txtSmtp.Name = "txtSmtp";
			this.txtSmtp.PreventEnterBeep = true;
			this.txtSmtp.Size = new System.Drawing.Size(143, 23);
			this.txtSmtp.TabIndex = 3;
			this.txtSmtp.WatermarkText = "SMTP服务器地址";
			// 
			// txtReceivers
			// 
			// 
			// 
			// 
			this.txtReceivers.Border.Class = "TextBoxBorder";
			this.txtReceivers.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtReceivers.FocusHighlightEnabled = true;
			this.txtReceivers.Location = new System.Drawing.Point(94, 115);
			this.txtReceivers.Name = "txtReceivers";
			this.txtReceivers.PreventEnterBeep = true;
			this.txtReceivers.Size = new System.Drawing.Size(352, 23);
			this.txtReceivers.TabIndex = 2;
			this.txtReceivers.WatermarkText = "收件人，可以与登录邮箱相同; 多个账号可用分号“;”隔开";
			// 
			// txtMailAdd
			// 
			// 
			// 
			// 
			this.txtMailAdd.Border.Class = "TextBoxBorder";
			this.txtMailAdd.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtMailAdd.FocusHighlightEnabled = true;
			this.txtMailAdd.Location = new System.Drawing.Point(94, 27);
			this.txtMailAdd.Name = "txtMailAdd";
			this.txtMailAdd.PreventEnterBeep = true;
			this.txtMailAdd.Size = new System.Drawing.Size(352, 23);
			this.txtMailAdd.TabIndex = 0;
			this.txtMailAdd.WatermarkText = "发送邮箱的账户";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(243, 84);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(68, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "服务器端口";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label6.Location = new System.Drawing.Point(36, 171);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(56, 17);
			this.label6.TabIndex = 0;
			this.label6.Text = "邮件正文";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label5.Location = new System.Drawing.Point(36, 142);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(56, 17);
			this.label5.TabIndex = 0;
			this.label5.Text = "邮件标题";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label7.Location = new System.Drawing.Point(48, 117);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(44, 17);
			this.label7.TabIndex = 0;
			this.label7.Text = "收件人";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Location = new System.Drawing.Point(36, 56);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 17);
			this.label4.TabIndex = 0;
			this.label4.Text = "登录密码";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(36, 29);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 17);
			this.label3.TabIndex = 0;
			this.label3.Text = "登录邮箱";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(13, 84);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "SMTP服务器";
			// 
			// MailConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "MailConfig";
			this.Size = new System.Drawing.Size(530, 360);
			this.panel1.ResumeLayout(false);
			this.pConfig.ResumeLayout(false);
			this.pConfig.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.iptServerPort)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.Controls.CheckBoxX chkEnableMail;
		private System.Windows.Forms.Panel pConfig;
		private DevComponents.DotNetBar.LabelX labelX2;
		private DevComponents.DotNetBar.Controls.CheckBoxX chkSsl;
		private DevComponents.DotNetBar.ButtonX btnTest;
		private DevComponents.Editors.IntegerInput iptServerPort;
		private DevComponents.DotNetBar.Controls.TextBoxX txtBody;
		private DevComponents.DotNetBar.Controls.TextBoxX txtTitle;
		private DevComponents.DotNetBar.Controls.TextBoxX txtPwd;
		private DevComponents.DotNetBar.Controls.TextBoxX txtSmtp;
		private DevComponents.DotNetBar.Controls.TextBoxX txtReceivers;
		private DevComponents.DotNetBar.Controls.TextBoxX txtMailAdd;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label1;
	}
}
