namespace TOBA.UI.Controls.Option
{
	using Common;

	partial class NetworkConfig
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.pPro = new System.Windows.Forms.Panel();
			this.nudLoadDelayWhenFail = new System.Windows.Forms.NumericUpDown();
			this.chkAutoreloadWhenVcFailed = new System.Windows.Forms.CheckBox();
			this.pVcSubmitDelay = new System.Windows.Forms.Panel();
			this.iVcSubmitDelay = new DevComponents.Editors.IntegerInput();
			this.label20 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.pVcLoadDelay = new System.Windows.Forms.Panel();
			this.iVcDelay = new DevComponents.Editors.IntegerInput();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.pRetry = new System.Windows.Forms.Panel();
			this.iptMaxRetry = new TOBA.UI.Controls.Common.IntNumbericUpDown();
			this.iptRetrySleep = new TOBA.UI.Controls.Common.IntNumbericUpDown();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.chkAutoRetryNetworkError = new System.Windows.Forms.CheckBox();
			this.label11 = new System.Windows.Forms.Label();
			this.nudServerErrorLimit = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.chkAppendDisableCdn = new System.Windows.Forms.CheckBox();
			this.label12 = new System.Windows.Forms.Label();
			this.cbProxyType = new System.Windows.Forms.ComboBox();
			this.cbBaseUri = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.gbProxy = new System.Windows.Forms.GroupBox();
			this.pHttp = new System.Windows.Forms.Panel();
			this.txtProxyUrl = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtProxyPwd = new System.Windows.Forms.TextBox();
			this.nudProxyPort = new System.Windows.Forms.NumericUpDown();
			this.txtProxyUser = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.cbProxyClass = new TOBA.UI.Controls.Common.EnumComboBox();
			this.label13 = new System.Windows.Forms.Label();
			this.pSocks = new System.Windows.Forms.Panel();
			this.nudSocks5Url = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.nudSocks5Port = new System.Windows.Forms.NumericUpDown();
			this.label15 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tipBarMessage1 = new TOBA.UI.Controls.Common.TipBarMessage();
			this.panel1.SuspendLayout();
			this.pPro.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudLoadDelayWhenFail)).BeginInit();
			this.pVcSubmitDelay.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.iVcSubmitDelay)).BeginInit();
			this.pVcLoadDelay.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.iVcDelay)).BeginInit();
			this.pRetry.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.iptMaxRetry)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iptRetrySleep)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudServerErrorLimit)).BeginInit();
			this.gbProxy.SuspendLayout();
			this.pHttp.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudProxyPort)).BeginInit();
			this.pSocks.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudSocks5Port)).BeginInit();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.pPro);
			this.panel1.Controls.Add(this.cbProxyType);
			this.panel1.Controls.Add(this.cbBaseUri);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.gbProxy);
			this.panel1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(530, 332);
			this.panel1.TabIndex = 19;
			// 
			// pPro
			// 
			this.pPro.Controls.Add(this.nudLoadDelayWhenFail);
			this.pPro.Controls.Add(this.chkAutoreloadWhenVcFailed);
			this.pPro.Controls.Add(this.pVcSubmitDelay);
			this.pPro.Controls.Add(this.pVcLoadDelay);
			this.pPro.Controls.Add(this.pRetry);
			this.pPro.Controls.Add(this.chkAutoRetryNetworkError);
			this.pPro.Controls.Add(this.label11);
			this.pPro.Controls.Add(this.nudServerErrorLimit);
			this.pPro.Controls.Add(this.label7);
			this.pPro.Controls.Add(this.chkAppendDisableCdn);
			this.pPro.Controls.Add(this.label12);
			this.pPro.Location = new System.Drawing.Point(4, 139);
			this.pPro.Name = "pPro";
			this.pPro.Size = new System.Drawing.Size(493, 190);
			this.pPro.TabIndex = 32;
			// 
			// nudLoadDelayWhenFail
			// 
			this.nudLoadDelayWhenFail.Location = new System.Drawing.Point(174, 148);
			this.nudLoadDelayWhenFail.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.nudLoadDelayWhenFail.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.nudLoadDelayWhenFail.Name = "nudLoadDelayWhenFail";
			this.nudLoadDelayWhenFail.Size = new System.Drawing.Size(60, 23);
			this.nudLoadDelayWhenFail.TabIndex = 67;
			this.nudLoadDelayWhenFail.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudLoadDelayWhenFail.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			// 
			// chkAutoreloadWhenVcFailed
			// 
			this.chkAutoreloadWhenVcFailed.AutoSize = true;
			this.chkAutoreloadWhenVcFailed.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkAutoreloadWhenVcFailed.Location = new System.Drawing.Point(13, 150);
			this.chkAutoreloadWhenVcFailed.Name = "chkAutoreloadWhenVcFailed";
			this.chkAutoreloadWhenVcFailed.Size = new System.Drawing.Size(421, 21);
			this.chkAutoreloadWhenVcFailed.TabIndex = 66;
			this.chkAutoreloadWhenVcFailed.Text = "验证码加载失败时自动在　　　　　　　毫秒后重新加载(除系统维护时间)";
			this.chkAutoreloadWhenVcFailed.UseVisualStyleBackColor = true;
			// 
			// pVcSubmitDelay
			// 
			this.pVcSubmitDelay.Controls.Add(this.iVcSubmitDelay);
			this.pVcSubmitDelay.Controls.Add(this.label20);
			this.pVcSubmitDelay.Controls.Add(this.label19);
			this.pVcSubmitDelay.Location = new System.Drawing.Point(-5, 117);
			this.pVcSubmitDelay.Name = "pVcSubmitDelay";
			this.pVcSubmitDelay.Size = new System.Drawing.Size(484, 27);
			this.pVcSubmitDelay.TabIndex = 65;
			// 
			// iVcSubmitDelay
			// 
			// 
			// 
			// 
			this.iVcSubmitDelay.BackgroundStyle.Class = "DateTimeInputBackground";
			this.iVcSubmitDelay.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.iVcSubmitDelay.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
			this.iVcSubmitDelay.Location = new System.Drawing.Point(106, 2);
			this.iVcSubmitDelay.MaxValue = 10000;
			this.iVcSubmitDelay.MinValue = 0;
			this.iVcSubmitDelay.Name = "iVcSubmitDelay";
			this.iVcSubmitDelay.ShowUpDown = true;
			this.iVcSubmitDelay.Size = new System.Drawing.Size(69, 23);
			this.iVcSubmitDelay.TabIndex = 43;
			// 
			// label20
			// 
			this.label20.AutoSize = true;
			this.label20.Location = new System.Drawing.Point(187, 6);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(291, 17);
			this.label20.TabIndex = 42;
			this.label20.Text = "(ms, 0-10000，不明白此设置用途的切勿修改此设置)";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label19.Location = new System.Drawing.Point(4, 6);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(92, 17);
			this.label19.TabIndex = 41;
			this.label19.Text = "验证码提交延迟";
			// 
			// pVcLoadDelay
			// 
			this.pVcLoadDelay.Controls.Add(this.iVcDelay);
			this.pVcLoadDelay.Controls.Add(this.label6);
			this.pVcLoadDelay.Controls.Add(this.label5);
			this.pVcLoadDelay.Location = new System.Drawing.Point(-8, 90);
			this.pVcLoadDelay.Name = "pVcLoadDelay";
			this.pVcLoadDelay.Size = new System.Drawing.Size(487, 27);
			this.pVcLoadDelay.TabIndex = 64;
			// 
			// iVcDelay
			// 
			// 
			// 
			// 
			this.iVcDelay.BackgroundStyle.Class = "DateTimeInputBackground";
			this.iVcDelay.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.iVcDelay.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
			this.iVcDelay.Location = new System.Drawing.Point(109, 2);
			this.iVcDelay.MaxValue = 10000;
			this.iVcDelay.MinValue = 0;
			this.iVcDelay.Name = "iVcDelay";
			this.iVcDelay.ShowUpDown = true;
			this.iVcDelay.Size = new System.Drawing.Size(69, 23);
			this.iVcDelay.TabIndex = 42;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(190, 6);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(291, 17);
			this.label6.TabIndex = 41;
			this.label6.Text = "(ms, 0-10000，不明白此设置用途的切勿修改此设置)";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label5.Location = new System.Drawing.Point(7, 6);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(92, 17);
			this.label5.TabIndex = 40;
			this.label5.Text = "验证码加载延迟";
			// 
			// pRetry
			// 
			this.pRetry.Controls.Add(this.iptMaxRetry);
			this.pRetry.Controls.Add(this.iptRetrySleep);
			this.pRetry.Controls.Add(this.label18);
			this.pRetry.Controls.Add(this.label17);
			this.pRetry.Controls.Add(this.label16);
			this.pRetry.Location = new System.Drawing.Point(178, 61);
			this.pRetry.Name = "pRetry";
			this.pRetry.Size = new System.Drawing.Size(289, 27);
			this.pRetry.TabIndex = 63;
			// 
			// iptMaxRetry
			// 
			this.iptMaxRetry.IntValue = 20;
			this.iptMaxRetry.Location = new System.Drawing.Point(198, 2);
			this.iptMaxRetry.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.iptMaxRetry.Name = "iptMaxRetry";
			this.iptMaxRetry.Size = new System.Drawing.Size(46, 23);
			this.iptMaxRetry.TabIndex = 1;
			this.iptMaxRetry.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.iptMaxRetry.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
			// 
			// iptRetrySleep
			// 
			this.iptRetrySleep.IntValue = 300;
			this.iptRetrySleep.Location = new System.Drawing.Point(37, 2);
			this.iptRetrySleep.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.iptRetrySleep.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
			this.iptRetrySleep.Name = "iptRetrySleep";
			this.iptRetrySleep.Size = new System.Drawing.Size(84, 23);
			this.iptRetrySleep.TabIndex = 1;
			this.iptRetrySleep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.iptRetrySleep.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(250, 5);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(20, 17);
			this.label18.TabIndex = 0;
			this.label18.Text = "次";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(127, 5);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(68, 17);
			this.label17.TabIndex = 0;
			this.label17.Text = "毫秒，最多";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(5, 5);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(32, 17);
			this.label16.TabIndex = 0;
			this.label16.Text = "间隔";
			// 
			// chkAutoRetryNetworkError
			// 
			this.chkAutoRetryNetworkError.AutoSize = true;
			this.chkAutoRetryNetworkError.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkAutoRetryNetworkError.Location = new System.Drawing.Point(13, 64);
			this.chkAutoRetryNetworkError.Name = "chkAutoRetryNetworkError";
			this.chkAutoRetryNetworkError.Size = new System.Drawing.Size(159, 21);
			this.chkAutoRetryNetworkError.TabIndex = 62;
			this.chkAutoRetryNetworkError.Text = "遇到网络错误时自动重试";
			this.chkAutoRetryNetworkError.UseVisualStyleBackColor = true;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(172, 14);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(296, 17);
			this.label11.TabIndex = 59;
			this.label11.Text = "当某个节点服务器访问失败超过指定次数，则放弃使用";
			// 
			// nudServerErrorLimit
			// 
			this.nudServerErrorLimit.Location = new System.Drawing.Point(102, 8);
			this.nudServerErrorLimit.Name = "nudServerErrorLimit";
			this.nudServerErrorLimit.Size = new System.Drawing.Size(64, 23);
			this.nudServerErrorLimit.TabIndex = 58;
			this.nudServerErrorLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(171, 37);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(325, 17);
			this.label7.TabIndex = 51;
			this.label7.Text = "尝试禁用CDN，但由于12306的CDN设置变态，不一定起效";
			// 
			// chkAppendDisableCdn
			// 
			this.chkAppendDisableCdn.AutoSize = true;
			this.chkAppendDisableCdn.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkAppendDisableCdn.Location = new System.Drawing.Point(13, 35);
			this.chkAppendDisableCdn.Name = "chkAppendDisableCdn";
			this.chkAppendDisableCdn.Size = new System.Drawing.Size(127, 21);
			this.chkAppendDisableCdn.TabIndex = 49;
			this.chkAppendDisableCdn.Text = "尝试禁用CDN缓存";
			this.chkAppendDisableCdn.UseVisualStyleBackColor = true;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label12.Location = new System.Drawing.Point(0, 14);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(92, 17);
			this.label12.TabIndex = 57;
			this.label12.Text = "服务器错误限制";
			// 
			// cbProxyType
			// 
			this.cbProxyType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbProxyType.FormattingEnabled = true;
			this.cbProxyType.Items.AddRange(new object[] {
            "0. 直接访问(支持智能DNS和服务器加速以及多服务器节点查询)",
            "1. IE代理(缓存模式，速度较快，但不支持PAC，代理变动时会需要重启才恢复正常)",
            "2. IE系统代理模式(每次都向系统查询代理服务器，速度最慢，非必要不推荐选择)",
            "3. 自定义模式 (自己直接设置所需要的代理服务器信息)"});
			this.cbProxyType.Location = new System.Drawing.Point(48, 16);
			this.cbProxyType.Name = "cbProxyType";
			this.cbProxyType.Size = new System.Drawing.Size(427, 25);
			this.cbProxyType.TabIndex = 31;
			// 
			// cbBaseUri
			// 
			this.cbBaseUri.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbBaseUri.FormattingEnabled = true;
			this.cbBaseUri.Items.AddRange(new object[] {
            "http://kyfw.12306.cn/otn/",
            "https://kyfw.12306.cn/otn/"});
			this.cbBaseUri.Location = new System.Drawing.Point(110, 5);
			this.cbBaseUri.Name = "cbBaseUri";
			this.cbBaseUri.Size = new System.Drawing.Size(368, 25);
			this.cbBaseUri.TabIndex = 26;
			this.cbBaseUri.Visible = false;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label8.ForeColor = System.Drawing.Color.Red;
			this.label8.Location = new System.Drawing.Point(26, 8);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(72, 17);
			this.label8.TabIndex = 20;
			this.label8.Text = "访问通道(*)";
			this.label8.Visible = false;
			// 
			// gbProxy
			// 
			this.gbProxy.Controls.Add(this.pHttp);
			this.gbProxy.Controls.Add(this.cbProxyClass);
			this.gbProxy.Controls.Add(this.label13);
			this.gbProxy.Controls.Add(this.pSocks);
			this.gbProxy.Location = new System.Drawing.Point(28, 19);
			this.gbProxy.Name = "gbProxy";
			this.gbProxy.Size = new System.Drawing.Size(469, 114);
			this.gbProxy.TabIndex = 19;
			this.gbProxy.TabStop = false;
			// 
			// pHttp
			// 
			this.pHttp.Controls.Add(this.txtProxyUrl);
			this.pHttp.Controls.Add(this.label1);
			this.pHttp.Controls.Add(this.txtProxyPwd);
			this.pHttp.Controls.Add(this.nudProxyPort);
			this.pHttp.Controls.Add(this.txtProxyUser);
			this.pHttp.Controls.Add(this.label2);
			this.pHttp.Controls.Add(this.label4);
			this.pHttp.Controls.Add(this.label3);
			this.pHttp.Location = new System.Drawing.Point(20, 52);
			this.pHttp.Name = "pHttp";
			this.pHttp.Size = new System.Drawing.Size(435, 53);
			this.pHttp.TabIndex = 10;
			// 
			// txtProxyUrl
			// 
			this.txtProxyUrl.Location = new System.Drawing.Point(93, 0);
			this.txtProxyUrl.Name = "txtProxyUrl";
			this.txtProxyUrl.Size = new System.Drawing.Size(136, 23);
			this.txtProxyUrl.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(-2, 4);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(92, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "代理服务器地址";
			// 
			// txtProxyPwd
			// 
			this.txtProxyPwd.Location = new System.Drawing.Point(294, 27);
			this.txtProxyPwd.Name = "txtProxyPwd";
			this.txtProxyPwd.PasswordChar = '啊';
			this.txtProxyPwd.Size = new System.Drawing.Size(136, 23);
			this.txtProxyPwd.TabIndex = 8;
			// 
			// nudProxyPort
			// 
			this.nudProxyPort.Location = new System.Drawing.Point(294, 1);
			this.nudProxyPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nudProxyPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudProxyPort.Name = "nudProxyPort";
			this.nudProxyPort.Size = new System.Drawing.Size(136, 23);
			this.nudProxyPort.TabIndex = 4;
			this.nudProxyPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudProxyPort.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
			// 
			// txtProxyUser
			// 
			this.txtProxyUser.Location = new System.Drawing.Point(93, 27);
			this.txtProxyUser.Name = "txtProxyUser";
			this.txtProxyUser.Size = new System.Drawing.Size(136, 23);
			this.txtProxyUser.TabIndex = 6;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(259, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 17);
			this.label2.TabIndex = 3;
			this.label2.Text = "端口";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(235, 33);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(56, 17);
			this.label4.TabIndex = 7;
			this.label4.Text = "认证密码";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(22, 33);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(68, 17);
			this.label3.TabIndex = 5;
			this.label3.Text = "认证用户名";
			// 
			// cbProxyClass
			// 
			this.cbProxyClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbProxyClass.FormattingEnabled = true;
			this.cbProxyClass.Location = new System.Drawing.Point(113, 26);
			this.cbProxyClass.Name = "cbProxyClass";
			this.cbProxyClass.Size = new System.Drawing.Size(136, 25);
			this.cbProxyClass.TabIndex = 9;
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(18, 29);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(92, 17);
			this.label13.TabIndex = 1;
			this.label13.Text = "代理服务器类型";
			// 
			// pSocks
			// 
			this.pSocks.Controls.Add(this.nudSocks5Url);
			this.pSocks.Controls.Add(this.label14);
			this.pSocks.Controls.Add(this.nudSocks5Port);
			this.pSocks.Controls.Add(this.label15);
			this.pSocks.Location = new System.Drawing.Point(20, 52);
			this.pSocks.Name = "pSocks";
			this.pSocks.Size = new System.Drawing.Size(435, 41);
			this.pSocks.TabIndex = 11;
			this.pSocks.Visible = false;
			// 
			// nudSocks5Url
			// 
			this.nudSocks5Url.Location = new System.Drawing.Point(96, 9);
			this.nudSocks5Url.Name = "nudSocks5Url";
			this.nudSocks5Url.Size = new System.Drawing.Size(136, 23);
			this.nudSocks5Url.TabIndex = 6;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(1, 13);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(108, 17);
			this.label14.TabIndex = 5;
			this.label14.Text = "SOCKS服务器地址";
			// 
			// nudSocks5Port
			// 
			this.nudSocks5Port.Location = new System.Drawing.Point(297, 10);
			this.nudSocks5Port.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
			this.nudSocks5Port.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudSocks5Port.Name = "nudSocks5Port";
			this.nudSocks5Port.Size = new System.Drawing.Size(133, 23);
			this.nudSocks5Port.TabIndex = 8;
			this.nudSocks5Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudSocks5Port.Value = new decimal(new int[] {
            80,
            0,
            0,
            0});
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(262, 12);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(32, 17);
			this.label15.TabIndex = 7;
			this.label15.Text = "端口";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.panel1);
			this.panel2.Controls.Add(this.tipBarMessage1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(530, 360);
			this.panel2.TabIndex = 20;
			// 
			// tipBarMessage1
			// 
			this.tipBarMessage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(254)))));
			this.tipBarMessage1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(201)))), ((int)(((byte)(226)))));
			this.tipBarMessage1.BorderThickness = 1;
			this.tipBarMessage1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tipBarMessage1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tipBarMessage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(138)))), ((int)(((byte)(196)))));
			this.tipBarMessage1.Image = global::TOBA.Properties.Resources.cou_16_warning;
			this.tipBarMessage1.LabelMargin = new System.Windows.Forms.Padding(25, 5, 4, 6);
			this.tipBarMessage1.Location = new System.Drawing.Point(0, 335);
			this.tipBarMessage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tipBarMessage1.Name = "tipBarMessage1";
			this.tipBarMessage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tipBarMessage1.Size = new System.Drawing.Size(530, 25);
			this.tipBarMessage1.TabIndex = 20;
			this.tipBarMessage1.Text = "带有红色的选项需要重启订票助手.NET后才可起效，部分选项可能因不可用而不可见";
			// 
			// NetworkConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel2);
			this.Name = "NetworkConfig";
			this.Size = new System.Drawing.Size(530, 360);
			this.Load += new System.EventHandler(this.NetworkConfig_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.pPro.ResumeLayout(false);
			this.pPro.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudLoadDelayWhenFail)).EndInit();
			this.pVcSubmitDelay.ResumeLayout(false);
			this.pVcSubmitDelay.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.iVcSubmitDelay)).EndInit();
			this.pVcLoadDelay.ResumeLayout(false);
			this.pVcLoadDelay.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.iVcDelay)).EndInit();
			this.pRetry.ResumeLayout(false);
			this.pRetry.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.iptMaxRetry)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iptRetrySleep)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudServerErrorLimit)).EndInit();
			this.gbProxy.ResumeLayout(false);
			this.gbProxy.PerformLayout();
			this.pHttp.ResumeLayout(false);
			this.pHttp.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudProxyPort)).EndInit();
			this.pSocks.ResumeLayout(false);
			this.pSocks.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudSocks5Port)).EndInit();
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox cbProxyType;
		private System.Windows.Forms.ComboBox cbBaseUri;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.GroupBox gbProxy;
		private System.Windows.Forms.Panel pSocks;
		private System.Windows.Forms.TextBox nudSocks5Url;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.NumericUpDown nudSocks5Port;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Panel pHttp;
		private System.Windows.Forms.TextBox txtProxyUrl;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtProxyPwd;
		private System.Windows.Forms.NumericUpDown nudProxyPort;
		private System.Windows.Forms.TextBox txtProxyUser;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private EnumComboBox cbProxyClass;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Panel panel2;
		private TipBarMessage tipBarMessage1;
		private System.Windows.Forms.Panel pPro;
		private System.Windows.Forms.NumericUpDown nudLoadDelayWhenFail;
		private System.Windows.Forms.CheckBox chkAutoreloadWhenVcFailed;
		private System.Windows.Forms.Panel pVcSubmitDelay;
		private DevComponents.Editors.IntegerInput iVcSubmitDelay;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Panel pVcLoadDelay;
		private DevComponents.Editors.IntegerInput iVcDelay;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel pRetry;
		private IntNumbericUpDown iptMaxRetry;
		private IntNumbericUpDown iptRetrySleep;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.CheckBox chkAutoRetryNetworkError;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.NumericUpDown nudServerErrorLimit;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox chkAppendDisableCdn;
		private System.Windows.Forms.Label label12;
	}
}
