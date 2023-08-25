namespace TOBA.UI.Controls.Option
{
	using Common;

	partial class GenericOption
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
			this.txtPasswordChar = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.chkMinimizeToTray = new System.Windows.Forms.CheckBox();
			this.chkAutoRelogin = new System.Windows.Forms.CheckBox();
			this.chkConflictLogin = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.nudCheckLoginStateInterval = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.nudMaxTryReloginCount = new TOBA.UI.Controls.Common.IntNumbericUpDown();
			this.chkAutoShowLoginDlg = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.chkKeepQuery = new System.Windows.Forms.CheckBox();
			this.chkKeepLogin = new System.Windows.Forms.CheckBox();
			this.chkNotifyIfNotMobileChecked = new System.Windows.Forms.CheckBox();
			this.chkAutoSendSms = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.nudCheckLoginStateInterval)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMaxTryReloginCount)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(17, 7);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "密码字符";
			// 
			// txtPasswordChar
			// 
			this.txtPasswordChar.Location = new System.Drawing.Point(76, 4);
			this.txtPasswordChar.Name = "txtPasswordChar";
			this.txtPasswordChar.Size = new System.Drawing.Size(124, 23);
			this.txtPasswordChar.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(210, 7);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(200, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "需要输入密码的地方显示的掩码字符";
			// 
			// chkMinimizeToTray
			// 
			this.chkMinimizeToTray.AutoSize = true;
			this.chkMinimizeToTray.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkMinimizeToTray.Location = new System.Drawing.Point(20, 33);
			this.chkMinimizeToTray.Name = "chkMinimizeToTray";
			this.chkMinimizeToTray.Size = new System.Drawing.Size(159, 21);
			this.chkMinimizeToTray.TabIndex = 1;
			this.chkMinimizeToTray.Text = "最小化时缩小到系统托盘";
			this.chkMinimizeToTray.UseVisualStyleBackColor = true;
			// 
			// chkAutoRelogin
			// 
			this.chkAutoRelogin.AutoSize = true;
			this.chkAutoRelogin.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkAutoRelogin.Location = new System.Drawing.Point(184, 33);
			this.chkAutoRelogin.Name = "chkAutoRelogin";
			this.chkAutoRelogin.Size = new System.Drawing.Size(159, 21);
			this.chkAutoRelogin.TabIndex = 2;
			this.chkAutoRelogin.Text = "账户被踢后自动重新登录";
			this.chkAutoRelogin.UseVisualStyleBackColor = true;
			// 
			// chkConflictLogin
			// 
			this.chkConflictLogin.AutoSize = true;
			this.chkConflictLogin.Enabled = false;
			this.chkConflictLogin.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkConflictLogin.Location = new System.Drawing.Point(20, 60);
			this.chkConflictLogin.Name = "chkConflictLogin";
			this.chkConflictLogin.Size = new System.Drawing.Size(123, 21);
			this.chkConflictLogin.TabIndex = 3;
			this.chkConflictLogin.Text = "允许重复登录多次";
			this.chkConflictLogin.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(36, 88);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(128, 17);
			this.label6.TabIndex = 11;
			this.label6.Text = "检查账户登录状态周期";
			// 
			// nudCheckLoginStateInterval
			// 
			this.nudCheckLoginStateInterval.Location = new System.Drawing.Point(177, 84);
			this.nudCheckLoginStateInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudCheckLoginStateInterval.Name = "nudCheckLoginStateInterval";
			this.nudCheckLoginStateInterval.Size = new System.Drawing.Size(60, 23);
			this.nudCheckLoginStateInterval.TabIndex = 4;
			this.nudCheckLoginStateInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudCheckLoginStateInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(243, 88);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(40, 17);
			this.label7.TabIndex = 6;
			this.label7.Text = "(分钟)";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(243, 116);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(28, 17);
			this.label8.TabIndex = 6;
			this.label8.Text = "(次)";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(36, 116);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(128, 17);
			this.label9.TabIndex = 11;
			this.label9.Text = "尝试重新登录次数限制";
			// 
			// nudMaxTryReloginCount
			// 
			this.nudMaxTryReloginCount.IntValue = 1;
			this.nudMaxTryReloginCount.Location = new System.Drawing.Point(177, 112);
			this.nudMaxTryReloginCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudMaxTryReloginCount.Name = "nudMaxTryReloginCount";
			this.nudMaxTryReloginCount.Size = new System.Drawing.Size(60, 23);
			this.nudMaxTryReloginCount.TabIndex = 5;
			this.nudMaxTryReloginCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudMaxTryReloginCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// chkAutoShowLoginDlg
			// 
			this.chkAutoShowLoginDlg.AutoSize = true;
			this.chkAutoShowLoginDlg.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkAutoShowLoginDlg.Location = new System.Drawing.Point(20, 139);
			this.chkAutoShowLoginDlg.Name = "chkAutoShowLoginDlg";
			this.chkAutoShowLoginDlg.Size = new System.Drawing.Size(291, 21);
			this.chkAutoShowLoginDlg.TabIndex = 6;
			this.chkAutoShowLoginDlg.Text = "如果没有已登录账号，则启动后自动弹出登录窗口";
			this.chkAutoShowLoginDlg.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.chkAutoSendSms);
			this.panel1.Controls.Add(this.chkKeepQuery);
			this.panel1.Controls.Add(this.chkKeepLogin);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.nudMaxTryReloginCount);
			this.panel1.Controls.Add(this.txtPasswordChar);
			this.panel1.Controls.Add(this.nudCheckLoginStateInterval);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.chkMinimizeToTray);
			this.panel1.Controls.Add(this.chkAutoShowLoginDlg);
			this.panel1.Controls.Add(this.chkConflictLogin);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.chkAutoRelogin);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.chkNotifyIfNotMobileChecked);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(530, 360);
			this.panel1.TabIndex = 13;
			// 
			// chkKeepQuery
			// 
			this.chkKeepQuery.AutoSize = true;
			this.chkKeepQuery.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkKeepQuery.Location = new System.Drawing.Point(332, 60);
			this.chkKeepQuery.Name = "chkKeepQuery";
			this.chkKeepQuery.Size = new System.Drawing.Size(171, 21);
			this.chkKeepQuery.TabIndex = 12;
			this.chkKeepQuery.Text = "开始关闭时还在运行的刷票";
			this.chkKeepQuery.UseVisualStyleBackColor = true;
			// 
			// chkKeepLogin
			// 
			this.chkKeepLogin.AutoSize = true;
			this.chkKeepLogin.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkKeepLogin.Location = new System.Drawing.Point(184, 60);
			this.chkKeepLogin.Name = "chkKeepLogin";
			this.chkKeepLogin.Size = new System.Drawing.Size(147, 21);
			this.chkKeepLogin.TabIndex = 12;
			this.chkKeepLogin.Text = "自动登录关闭时的账户";
			this.chkKeepLogin.UseVisualStyleBackColor = true;
			// 
			// chkNotifyIfNotMobileChecked
			// 
			this.chkNotifyIfNotMobileChecked.AutoSize = true;
			this.chkNotifyIfNotMobileChecked.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkNotifyIfNotMobileChecked.Location = new System.Drawing.Point(20, 166);
			this.chkNotifyIfNotMobileChecked.Name = "chkNotifyIfNotMobileChecked";
			this.chkNotifyIfNotMobileChecked.Size = new System.Drawing.Size(231, 21);
			this.chkNotifyIfNotMobileChecked.TabIndex = 7;
			this.chkNotifyIfNotMobileChecked.Text = "如果手机号码没有通过核验，请通知我";
			this.chkNotifyIfNotMobileChecked.UseVisualStyleBackColor = true;
			this.chkAutoSendSms.AutoSize = true;
			this.chkAutoSendSms.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkAutoSendSms.Location = new System.Drawing.Point(340, 33);
			this.chkAutoSendSms.Name = "chkAutoSendSms";
			this.chkAutoSendSms.Size = new System.Drawing.Size(171, 21);
			this.chkAutoSendSms.TabIndex = 13;
			this.chkAutoSendSms.Text = "需要短信验证码时自动发送";
			this.chkAutoSendSms.UseVisualStyleBackColor = true;
			// 
			// GenericOption
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "GenericOption";
			this.Size = new System.Drawing.Size(530, 360);
			((System.ComponentModel.ISupportInitialize)(this.nudCheckLoginStateInterval)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudMaxTryReloginCount)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtPasswordChar;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox chkMinimizeToTray;
		private System.Windows.Forms.CheckBox chkAutoRelogin;
		private System.Windows.Forms.CheckBox chkConflictLogin;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown nudCheckLoginStateInterval;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private IntNumbericUpDown nudMaxTryReloginCount;
		private System.Windows.Forms.CheckBox chkAutoShowLoginDlg;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox chkNotifyIfNotMobileChecked;
		private System.Windows.Forms.CheckBox chkKeepQuery;
		private System.Windows.Forms.CheckBox chkKeepLogin;
		private System.Windows.Forms.CheckBox chkAutoSendSms;
	}
}
