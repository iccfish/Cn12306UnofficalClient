namespace TOBA.UI.Controls.PlatformUI
{
	using Common;

	partial class VcConfig
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lnkHome = new System.Windows.Forms.LinkLabel();
			this.scoreDesc = new DevComponents.DotNetBar.LabelX();
			this.btnLogin = new DevComponents.DotNetBar.ButtonX();
			this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
			this.txtUserName = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.txtPassword = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.chkEnableOrderVc = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.chkEnableLoginVc = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.pState = new DevComponents.DotNetBar.PanelEx();
			this.cbVcConflict = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.comboItem1 = new DevComponents.Editors.ComboItem();
			this.comboItem3 = new DevComponents.Editors.ComboItem();
			this.comboItem2 = new DevComponents.Editors.ComboItem();
			this.labelX5 = new DevComponents.DotNetBar.LabelX();
			this.iiFailLimit = new DevComponents.Editors.IntegerInput();
			this.chkAutoReloadIfNotRecognized = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.lnkUU = new System.Windows.Forms.LinkLabel();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.cbEngine = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.tipBarMessage1 = new TipBarMessage();
			this.pTip = new DevComponents.DotNetBar.PanelEx();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.labelX4 = new DevComponents.DotNetBar.LabelX();
			this.labelX3 = new DevComponents.DotNetBar.LabelX();
			this.pState.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.iiFailLimit)).BeginInit();
			this.pTip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(18, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 17);
			this.label1.TabIndex = 4;
			this.label1.Text = "登录用户名";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(18, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 17);
			this.label2.TabIndex = 4;
			this.label2.Text = "登录密码";
			// 
			// lnkHome
			// 
			this.lnkHome.AutoSize = true;
			this.lnkHome.Location = new System.Drawing.Point(384, 19);
			this.lnkHome.Name = "lnkHome";
			this.lnkHome.Size = new System.Drawing.Size(77, 12);
			this.lnkHome.TabIndex = 8;
			this.lnkHome.TabStop = true;
			this.lnkHome.Text = "远程打码官网";
			this.lnkHome.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHome_LinkClicked);
			// 
			// scoreDesc
			// 
			// 
			// 
			// 
			this.scoreDesc.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.scoreDesc.Location = new System.Drawing.Point(98, 71);
			this.scoreDesc.Name = "scoreDesc";
			this.scoreDesc.Size = new System.Drawing.Size(351, 20);
			this.scoreDesc.TabIndex = 7;
			this.scoreDesc.Text = "---";
			// 
			// btnLogin
			// 
			this.btnLogin.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnLogin.Location = new System.Drawing.Point(89, 109);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(125, 37);
			this.btnLogin.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnLogin.TabIndex = 2;
			this.btnLogin.Text = "登录(&L)";
			// 
			// btnRefresh
			// 
			this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnRefresh.Location = new System.Drawing.Point(262, 109);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(125, 37);
			this.btnRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnRefresh.TabIndex = 3;
			this.btnRefresh.Text = "刷新题分(&R)";
			// 
			// txtUserName
			// 
			this.txtUserName.AutoSelectAll = true;
			// 
			// 
			// 
			this.txtUserName.Border.Class = "TextBoxBorder";
			this.txtUserName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtUserName.FocusHighlightEnabled = true;
			this.txtUserName.Location = new System.Drawing.Point(89, 14);
			this.txtUserName.Name = "txtUserName";
			this.txtUserName.PreventEnterBeep = true;
			this.txtUserName.Size = new System.Drawing.Size(273, 21);
			this.txtUserName.TabIndex = 0;
			this.txtUserName.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty;
			this.txtUserName.WatermarkText = "用户名";
			// 
			// txtPassword
			// 
			this.txtPassword.AutoSelectAll = true;
			// 
			// 
			// 
			this.txtPassword.Border.Class = "TextBoxBorder";
			this.txtPassword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtPassword.FocusHighlightEnabled = true;
			this.txtPassword.Location = new System.Drawing.Point(89, 44);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.PasswordChar = '●';
			this.txtPassword.PreventEnterBeep = true;
			this.txtPassword.Size = new System.Drawing.Size(273, 21);
			this.txtPassword.TabIndex = 1;
			this.txtPassword.UseSystemPasswordChar = true;
			this.txtPassword.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty;
			this.txtPassword.WatermarkText = "登录密码";
			// 
			// chkEnableOrderVc
			// 
			this.chkEnableOrderVc.AutoSize = true;
			// 
			// 
			// 
			this.chkEnableOrderVc.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.chkEnableOrderVc.Location = new System.Drawing.Point(171, 160);
			this.chkEnableOrderVc.Name = "chkEnableOrderVc";
			this.chkEnableOrderVc.Size = new System.Drawing.Size(144, 18);
			this.chkEnableOrderVc.TabIndex = 5;
			this.chkEnableOrderVc.Text = "启动订单自动打码(&O)";
			// 
			// chkEnableLoginVc
			// 
			this.chkEnableLoginVc.AutoSize = true;
			// 
			// 
			// 
			this.chkEnableLoginVc.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.chkEnableLoginVc.Location = new System.Drawing.Point(21, 160);
			this.chkEnableLoginVc.Name = "chkEnableLoginVc";
			this.chkEnableLoginVc.Size = new System.Drawing.Size(119, 18);
			this.chkEnableLoginVc.TabIndex = 4;
			this.chkEnableLoginVc.Text = "登录远程打码(&L)";
			// 
			// pState
			// 
			this.pState.CanvasColor = System.Drawing.SystemColors.Control;
			this.pState.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.pState.Controls.Add(this.cbVcConflict);
			this.pState.Controls.Add(this.labelX5);
			this.pState.Controls.Add(this.iiFailLimit);
			this.pState.Controls.Add(this.chkAutoReloadIfNotRecognized);
			this.pState.Controls.Add(this.lnkUU);
			this.pState.Controls.Add(this.chkEnableLoginVc);
			this.pState.Controls.Add(this.txtPassword);
			this.pState.Controls.Add(this.chkEnableOrderVc);
			this.pState.Controls.Add(this.txtUserName);
			this.pState.Controls.Add(this.btnRefresh);
			this.pState.Controls.Add(this.label1);
			this.pState.Controls.Add(this.btnLogin);
			this.pState.Controls.Add(this.label2);
			this.pState.Controls.Add(this.lnkHome);
			this.pState.Controls.Add(this.scoreDesc);
			this.pState.Controls.Add(this.labelX2);
			this.pState.DisabledBackColor = System.Drawing.Color.Empty;
			this.pState.Location = new System.Drawing.Point(17, 60);
			this.pState.Name = "pState";
			this.pState.Size = new System.Drawing.Size(484, 222);
			this.pState.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.pState.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.pState.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.pState.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.pState.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.pState.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.pState.Style.GradientAngle = 90;
			this.pState.TabIndex = 1;
			// 
			// cbVcConflict
			// 
			this.cbVcConflict.DisplayMember = "Text";
			this.cbVcConflict.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbVcConflict.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbVcConflict.FormattingEnabled = true;
			this.cbVcConflict.ItemHeight = 15;
			this.cbVcConflict.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem3,
            this.comboItem2});
			this.cbVcConflict.Location = new System.Drawing.Point(136, 186);
			this.cbVcConflict.Name = "cbVcConflict";
			this.cbVcConflict.Size = new System.Drawing.Size(132, 21);
			this.cbVcConflict.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cbVcConflict.TabIndex = 16;
			// 
			// comboItem1
			// 
			this.comboItem1.Text = "无操作";
			// 
			// comboItem3
			// 
			this.comboItem3.Text = "忽略打码结果";
			// 
			// comboItem2
			// 
			this.comboItem2.Text = "清空我的输入";
			// 
			// labelX5
			// 
			this.labelX5.AutoSize = true;
			// 
			// 
			// 
			this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX5.Location = new System.Drawing.Point(21, 189);
			this.labelX5.Name = "labelX5";
			this.labelX5.Size = new System.Drawing.Size(118, 18);
			this.labelX5.TabIndex = 15;
			this.labelX5.Text = "自动识别时手动输入";
			// 
			// iiFailLimit
			// 
			// 
			// 
			// 
			this.iiFailLimit.BackgroundStyle.Class = "DateTimeInputBackground";
			this.iiFailLimit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.iiFailLimit.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
			this.iiFailLimit.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Center;
			this.iiFailLimit.Location = new System.Drawing.Point(304, 185);
			this.iiFailLimit.MaxValue = 20;
			this.iiFailLimit.MinValue = 1;
			this.iiFailLimit.Name = "iiFailLimit";
			this.iiFailLimit.ShowUpDown = true;
			this.iiFailLimit.Size = new System.Drawing.Size(39, 21);
			this.iiFailLimit.TabIndex = 7;
			this.iiFailLimit.Value = 5;
			// 
			// chkAutoReloadIfNotRecognized
			// 
			this.chkAutoReloadIfNotRecognized.AutoSize = true;
			// 
			// 
			// 
			this.chkAutoReloadIfNotRecognized.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.chkAutoReloadIfNotRecognized.Location = new System.Drawing.Point(321, 160);
			this.chkAutoReloadIfNotRecognized.Name = "chkAutoReloadIfNotRecognized";
			this.chkAutoReloadIfNotRecognized.Size = new System.Drawing.Size(162, 18);
			this.chkAutoReloadIfNotRecognized.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.chkAutoReloadIfNotRecognized.TabIndex = 6;
			this.chkAutoReloadIfNotRecognized.Text = "如果未能识别则自动刷新";
			// 
			// lnkUU
			// 
			this.lnkUU.AutoSize = true;
			this.lnkUU.Location = new System.Drawing.Point(384, 45);
			this.lnkUU.Name = "lnkUU";
			this.lnkUU.Size = new System.Drawing.Size(65, 12);
			this.lnkUU.TabIndex = 9;
			this.lnkUU.TabStop = true;
			this.lnkUU.Text = "错误码查询";
			// 
			// labelX2
			// 
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Location = new System.Drawing.Point(274, 189);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(207, 16);
			this.labelX2.TabIndex = 14;
			this.labelX2.Text = "连续        次没能识别后放弃识别";
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX1.Location = new System.Drawing.Point(17, 19);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(131, 35);
			this.labelX1.TabIndex = 9;
			this.labelX1.Text = "远程打码引擎：";
			// 
			// cbEngine
			// 
			this.cbEngine.DisplayMember = "Text";
			this.cbEngine.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbEngine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbEngine.FormattingEnabled = true;
			this.cbEngine.ItemHeight = 15;
			this.cbEngine.Location = new System.Drawing.Point(144, 25);
			this.cbEngine.Name = "cbEngine";
			this.cbEngine.Size = new System.Drawing.Size(334, 21);
			this.cbEngine.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cbEngine.TabIndex = 0;
			// 
			// tipBarMessage1
			// 
			this.tipBarMessage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
			this.tipBarMessage1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.tipBarMessage1.BorderThickness = 1;
			this.tipBarMessage1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tipBarMessage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.tipBarMessage1.Image = global::TOBA.Properties.Resources.cou_16_warning;
			this.tipBarMessage1.Location = new System.Drawing.Point(0, 320);
			this.tipBarMessage1.Name = "tipBarMessage1";
			this.tipBarMessage1.Padding = new System.Windows.Forms.Padding(3);
			this.tipBarMessage1.Size = new System.Drawing.Size(530, 40);
			this.tipBarMessage1.TabIndex = 2;
			this.tipBarMessage1.Text = "由于12306验证码不稳定，为了避免额外的麻烦，对于远程打码错误的验证码尚不支持报错，还请见谅。";
			// 
			// pTip
			// 
			this.pTip.CanvasColor = System.Drawing.SystemColors.Control;
			this.pTip.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.pTip.Controls.Add(this.pictureBox1);
			this.pTip.Controls.Add(this.labelX4);
			this.pTip.Controls.Add(this.labelX3);
			this.pTip.DisabledBackColor = System.Drawing.Color.Empty;
			this.pTip.Location = new System.Drawing.Point(28, 74);
			this.pTip.Name = "pTip";
			this.pTip.Size = new System.Drawing.Size(460, 191);
			this.pTip.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.pTip.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.pTip.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.pTip.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.pTip.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.pTip.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.pTip.Style.GradientAngle = 90;
			this.pTip.TabIndex = 10;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TOBA.Properties.Resources.onebit_47;
			this.pictureBox1.Location = new System.Drawing.Point(14, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// labelX4
			// 
			// 
			// 
			// 
			this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX4.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
			this.labelX4.ForeColor = System.Drawing.Color.Crimson;
			this.labelX4.Location = new System.Drawing.Point(68, 124);
			this.labelX4.Name = "labelX4";
			this.labelX4.Size = new System.Drawing.Size(389, 64);
			this.labelX4.TabIndex = 0;
			this.labelX4.Text = "警告： 远程打码一般速度较慢且成功率较低！不是万不得已的时候强烈建议不要启用！！由远程打码引起的一切不良后果本软件概不负责！";
			this.labelX4.TextLineAlignment = System.Drawing.StringAlignment.Near;
			this.labelX4.WordWrap = true;
			// 
			// labelX3
			// 
			// 
			// 
			// 
			this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX3.Font = new System.Drawing.Font("微软雅黑", 11F, System.Drawing.FontStyle.Bold);
			this.labelX3.Location = new System.Drawing.Point(68, 12);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(389, 106);
			this.labelX3.TabIndex = 0;
			this.labelX3.Text = "远程打码仅用于实在无法守在电脑前或视觉有障碍情况下的值守购票，请合理使用此功能。\r\n\r\n打码为付费服务，请自行解决相关账号注册和充值操作。";
			this.labelX3.TextLineAlignment = System.Drawing.StringAlignment.Near;
			this.labelX3.WordWrap = true;
			// 
			// VcConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pState);
			this.Controls.Add(this.tipBarMessage1);
			this.Controls.Add(this.cbEngine);
			this.Controls.Add(this.labelX1);
			this.Controls.Add(this.pTip);
			this.Name = "VcConfig";
			this.Size = new System.Drawing.Size(530, 360);
			this.Load += new System.EventHandler(this.VcConfig_Load);
			this.pState.ResumeLayout(false);
			this.pState.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.iiFailLimit)).EndInit();
			this.pTip.ResumeLayout(false);
			this.pTip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel lnkHome;
		private DevComponents.DotNetBar.LabelX scoreDesc;
		private DevComponents.DotNetBar.ButtonX btnLogin;
		private DevComponents.DotNetBar.ButtonX btnRefresh;
		private DevComponents.DotNetBar.Controls.TextBoxX txtUserName;
		private DevComponents.DotNetBar.Controls.TextBoxX txtPassword;
		private DevComponents.DotNetBar.Controls.CheckBoxX chkEnableOrderVc;
		private DevComponents.DotNetBar.Controls.CheckBoxX chkEnableLoginVc;
		private DevComponents.DotNetBar.PanelEx pState;
		private System.Windows.Forms.LinkLabel lnkUU;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.Controls.ComboBoxEx cbEngine;
		private DevComponents.DotNetBar.Controls.CheckBoxX chkAutoReloadIfNotRecognized;
		private DevComponents.Editors.IntegerInput iiFailLimit;
		private DevComponents.DotNetBar.LabelX labelX2;
		private TipBarMessage tipBarMessage1;
		private DevComponents.DotNetBar.PanelEx pTip;
		private System.Windows.Forms.PictureBox pictureBox1;
		private DevComponents.DotNetBar.LabelX labelX3;
		private DevComponents.DotNetBar.LabelX labelX4;
		private DevComponents.DotNetBar.Controls.ComboBoxEx cbVcConflict;
		private DevComponents.Editors.ComboItem comboItem1;
		private DevComponents.Editors.ComboItem comboItem3;
		private DevComponents.Editors.ComboItem comboItem2;
		private DevComponents.DotNetBar.LabelX labelX5;
	}
}
