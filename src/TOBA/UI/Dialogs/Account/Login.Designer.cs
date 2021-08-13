namespace TOBA.UI.Dialogs.Account
{
	using System.Windows.Forms;

	using Controls.Common;

	partial class Login
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtPassword = new System.Windows.Forms.TextBox();
			this.cbUserName = new ComboBoxEx();
			this.cbRememberPwd = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblBadge = new System.Windows.Forms.Label();
			this.pbLogin = new System.Windows.Forms.PictureBox();
			this.label3 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.chkAutoVc = new System.Windows.Forms.CheckBox();
			this.chkTmp = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.ulogin = new TOBA.UI.Components.Account.UserLoginComponent(this.components);
			this.btnLogin = new DevComponents.DotNetBar.ButtonX();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.tc = new TOBA.UI.Controls.Vc.TouchClickSimple();
			this.panel6 = new System.Windows.Forms.Panel();
			this.lblError = new System.Windows.Forms.Label();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.ts = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsVirutalLogin = new System.Windows.Forms.ToolStripStatusLabel();
			this.lnkDelete = new System.Windows.Forms.ToolStripDropDownButton();
			this.cmDeletePassword = new System.Windows.Forms.ToolStripMenuItem();
			this.cmDeleteUser = new System.Windows.Forms.ToolStripMenuItem();
			this.tsQrLogin = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbLogin)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel6.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(3, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(75, 21);
			this.label1.TabIndex = 1;
			this.label1.Text = "用户名";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(3, 2);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(75, 21);
			this.label2.TabIndex = 1;
			this.label2.Text = "密码";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtPassword
			// 
			this.txtPassword.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtPassword.Location = new System.Drawing.Point(199, 33);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '囧';
			this.txtPassword.Size = new System.Drawing.Size(191, 23);
			this.txtPassword.TabIndex = 1;
			// 
			// cbUserName
			// 
			this.cbUserName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbUserName.DefaultCaptionFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
			this.cbUserName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.cbUserName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.cbUserName.DropDownHeight = 200;
			this.cbUserName.FormattingEnabled = true;
			this.cbUserName.IntegralHeight = false;
			this.cbUserName.ItemPadding = new System.Windows.Forms.Padding(5);
			this.cbUserName.Location = new System.Drawing.Point(3, 33);
			this.cbUserName.MaxLength = 50;
			this.cbUserName.Name = "cbUserName";
			this.cbUserName.Size = new System.Drawing.Size(190, 24);
			this.cbUserName.TabIndex = 0;
			// 
			// cbRememberPwd
			// 
			this.cbRememberPwd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbRememberPwd.AutoSize = true;
			this.cbRememberPwd.Location = new System.Drawing.Point(119, 2);
			this.cbRememberPwd.Name = "cbRememberPwd";
			this.cbRememberPwd.Size = new System.Drawing.Size(75, 21);
			this.cbRememberPwd.TabIndex = 2;
			this.cbRememberPwd.Text = "记住密码";
			this.toolTip1.SetToolTip(this.cbRememberPwd, "选中此选项，则会在本机加密记录当前账户密码");
			this.cbRememberPwd.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.lblBadge);
			this.panel1.Controls.Add(this.pbLogin);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(393, 73);
			this.panel1.TabIndex = 7;
			// 
			// lblBadge
			// 
			this.lblBadge.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblBadge.AutoEllipsis = true;
			this.lblBadge.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblBadge.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblBadge.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.lblBadge.Location = new System.Drawing.Point(61, 25);
			this.lblBadge.Name = "lblBadge";
			this.lblBadge.Size = new System.Drawing.Size(329, 44);
			this.lblBadge.TabIndex = 3;
			this.lblBadge.Text = "我在过马路，你人在哪里";
			// 
			// pbLogin
			// 
			this.pbLogin.Image = global::TOBA.Properties.Resources.gif1;
			this.pbLogin.Location = new System.Drawing.Point(7, 3);
			this.pbLogin.Name = "pbLogin";
			this.pbLogin.Size = new System.Drawing.Size(48, 48);
			this.pbLogin.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbLogin.TabIndex = 2;
			this.pbLogin.TabStop = false;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(61, 3);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(108, 22);
			this.label3.TabIndex = 1;
			this.label3.Text = "登录到12306";
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.WindowFrame;
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 72);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(393, 1);
			this.panel2.TabIndex = 0;
			// 
			// chkAutoVc
			// 
			this.chkAutoVc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkAutoVc.AutoSize = true;
			this.chkAutoVc.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkAutoVc.Location = new System.Drawing.Point(306, 387);
			this.chkAutoVc.Name = "chkAutoVc";
			this.chkAutoVc.Size = new System.Drawing.Size(75, 21);
			this.chkAutoVc.TabIndex = 9;
			this.chkAutoVc.Text = "远程打码";
			this.toolTip1.SetToolTip(this.chkAutoVc, "使用远程打码自动识别验证码");
			this.chkAutoVc.UseVisualStyleBackColor = true;
			this.chkAutoVc.Visible = false;
			// 
			// chkTmp
			// 
			this.chkTmp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkTmp.AutoSize = true;
			this.chkTmp.Location = new System.Drawing.Point(121, 2);
			this.chkTmp.Name = "chkTmp";
			this.chkTmp.Size = new System.Drawing.Size(75, 21);
			this.chkTmp.TabIndex = 3;
			this.chkTmp.Text = "临时账户";
			this.toolTip1.SetToolTip(this.chkTmp, "临时账户所有的资料都将不会记录在本机，包括用户名。");
			this.chkTmp.UseVisualStyleBackColor = true;
			// 
			// toolTip1
			// 
			this.toolTip1.AutomaticDelay = 0;
			this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.toolTip1.ToolTipTitle = "助手提示";
			// 
			// ulogin
			// 
			this.ulogin.EnableFallback = false;
			this.ulogin.OwnerForm = null;
			// 
			// btnLogin
			// 
			this.btnLogin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLogin.Image = global::TOBA.Properties.Resources.tick_16;
			this.btnLogin.Location = new System.Drawing.Point(271, 2);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(119, 35);
			this.btnLogin.TabIndex = 4;
			this.btnLogin.Text = "登录(&L)";
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.panel5, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.cbUserName, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.txtPassword, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 73);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(393, 60);
			this.tableLayoutPanel1.TabIndex = 10;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.label2);
			this.panel5.Controls.Add(this.cbRememberPwd);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel5.Location = new System.Drawing.Point(199, 3);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(191, 24);
			this.panel5.TabIndex = 3;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.label1);
			this.panel4.Controls.Add(this.chkTmp);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(3, 3);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(190, 24);
			this.panel4.TabIndex = 2;
			// 
			// tc
			// 
			this.tc.BackColor = System.Drawing.Color.White;
			this.tc.Image = null;
			this.tc.Location = new System.Drawing.Point(3, 136);
			this.tc.Name = "tc";
			this.tc.ShowOkButton = false;
			this.tc.Size = new System.Drawing.Size(387, 270);
			this.tc.TabIndex = 12;
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.lblError);
			this.panel6.Controls.Add(this.btnLogin);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel6.Location = new System.Drawing.Point(0, 409);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(393, 45);
			this.panel6.TabIndex = 13;
			// 
			// lblError
			// 
			this.lblError.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblError.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblError.ForeColor = System.Drawing.Color.Red;
			this.lblError.Location = new System.Drawing.Point(3, 4);
			this.lblError.Name = "lblError";
			this.lblError.Size = new System.Drawing.Size(262, 38);
			this.lblError.TabIndex = 15;
			this.lblError.Visible = false;
			// 
			// statusStrip1
			// 
			this.statusStrip1.BackColor = System.Drawing.SystemColors.Control;
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts,
            this.toolStripStatusLabel5,
            this.tsVirutalLogin,
            this.lnkDelete,
            this.tsQrLogin});
			this.statusStrip1.Location = new System.Drawing.Point(0, 454);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.statusStrip1.ShowItemToolTips = true;
			this.statusStrip1.Size = new System.Drawing.Size(393, 23);
			this.statusStrip1.TabIndex = 14;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// ts
			// 
			this.ts.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ts.ForeColor = System.Drawing.Color.RoyalBlue;
			this.ts.Image = global::TOBA.Properties.Resources._16px_loading_1;
			this.ts.Name = "ts";
			this.ts.Size = new System.Drawing.Size(66, 18);
			this.ts.Text = "准备中..";
			// 
			// toolStripStatusLabel5
			// 
			this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
			this.toolStripStatusLabel5.Size = new System.Drawing.Size(40, 18);
			this.toolStripStatusLabel5.Spring = true;
			// 
			// tsVirutalLogin
			// 
			this.tsVirutalLogin.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tsVirutalLogin.Image = global::TOBA.Properties.Resources.cou_16_protection;
			this.tsVirutalLogin.IsLink = true;
			this.tsVirutalLogin.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.tsVirutalLogin.LinkColor = System.Drawing.Color.RoyalBlue;
			this.tsVirutalLogin.Name = "tsVirutalLogin";
			this.tsVirutalLogin.Size = new System.Drawing.Size(72, 18);
			this.tsVirutalLogin.Text = "虚拟登录";
			this.tsVirutalLogin.ToolTipText = "虚拟登录模式，将可以在12306无法登录的时候模拟出登录的效果，打开用户标签。\r\n但此模式的登录是不可靠的，受到在线状态的制约。但保持重新登录对话框不关闭，可以在" +
    "第一时间重新登录。\r\n同时，此模式可以执行查票等操作。";
			// 
			// lnkDelete
			// 
			this.lnkDelete.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmDeletePassword,
            this.cmDeleteUser});
			this.lnkDelete.ForeColor = System.Drawing.Color.Maroon;
			this.lnkDelete.Image = global::TOBA.Properties.Resources.trash_16;
			this.lnkDelete.Name = "lnkDelete";
			this.lnkDelete.Size = new System.Drawing.Size(85, 21);
			this.lnkDelete.Text = "删除信息";
			// 
			// cmDeletePassword
			// 
			this.cmDeletePassword.Image = global::TOBA.Properties.Resources.flag_16;
			this.cmDeletePassword.Name = "cmDeletePassword";
			this.cmDeletePassword.Size = new System.Drawing.Size(187, 22);
			this.cmDeletePassword.Text = "删除已保存的密码(&P)";
			// 
			// cmDeleteUser
			// 
			this.cmDeleteUser.Image = global::TOBA.Properties.Resources.trash_16;
			this.cmDeleteUser.Name = "cmDeleteUser";
			this.cmDeleteUser.Size = new System.Drawing.Size(187, 22);
			this.cmDeleteUser.Text = "彻底删除此用户(&U)...";
			// 
			// tsQrLogin
			// 
			this.tsQrLogin.Image = global::TOBA.Properties.Resources.xfsm_switch;
			this.tsQrLogin.IsLink = true;
			this.tsQrLogin.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.tsQrLogin.LinkColor = System.Drawing.Color.Crimson;
			this.tsQrLogin.Name = "tsQrLogin";
			this.tsQrLogin.Size = new System.Drawing.Size(84, 18);
			this.tsQrLogin.Text = "二维码登录";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Image = global::TOBA.Properties.Resources.flag_16;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(187, 22);
			this.toolStripMenuItem1.Text = "删除已保存的密码(&P)";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Image = global::TOBA.Properties.Resources.trash_16;
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(187, 22);
			this.toolStripMenuItem2.Text = "彻底删除此用户(&U)...";
			// 
			// Login
			// 
			this.AcceptButton = this.btnLogin;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(393, 477);
			this.Controls.Add(this.panel6);
			this.Controls.Add(this.chkAutoVc);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.tc);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.panel1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Login";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Load += new System.EventHandler(this.Login_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbLogin)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel6.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtPassword;
		private DevComponents.DotNetBar.ButtonX btnLogin;
		private ComboBoxEx cbUserName;
		private System.Windows.Forms.CheckBox cbRememberPwd;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pbLogin;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label lblBadge;
		private System.Windows.Forms.CheckBox chkAutoVc;
		private System.Windows.Forms.CheckBox chkTmp;
		private System.Windows.Forms.ToolTip toolTip1;
		private Components.Account.UserLoginComponent ulogin;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Panel panel4;
		private Controls.Vc.TouchClickSimple tc;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel ts;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
		private System.Windows.Forms.ToolStripMenuItem cmDeletePassword;
		private System.Windows.Forms.ToolStripMenuItem cmDeleteUser;
		private System.Windows.Forms.ToolStripDropDownButton lnkDelete;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private Label lblError;
		private ToolStripStatusLabel tsVirutalLogin;
		private ToolStripStatusLabel tsQrLogin;
	}
}