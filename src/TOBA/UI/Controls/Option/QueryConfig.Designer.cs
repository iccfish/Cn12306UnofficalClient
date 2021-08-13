namespace TOBA.UI.Controls.Option
{
	using Common;

	partial class QueryConfig
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
			this.toQuickWarning = new TOBA.UI.Controls.Common.TipBarMessage();
			this.panel1 = new System.Windows.Forms.Panel();
			this.nudDetectHbNonSelectInterval = new System.Windows.Forms.NumericUpDown();
			this.nudDetectHbSelectInterval = new System.Windows.Forms.NumericUpDown();
			this.chkNotifyMeIfHbOk = new System.Windows.Forms.CheckBox();
			this.chkAutoDetectNOSelectedTrainsHb = new System.Windows.Forms.CheckBox();
			this.chkAutoDetectSelectedTrainsHb = new System.Windows.Forms.CheckBox();
			this.nudSleepRandom = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.chkSleepRandom = new System.Windows.Forms.CheckBox();
			this.chkQueryProtector = new System.Windows.Forms.CheckBox();
			this.chkBoostSpeed = new System.Windows.Forms.CheckBox();
			this.label16 = new System.Windows.Forms.Label();
			this.nudAqRate = new System.Windows.Forms.NumericUpDown();
			this.chkAutoIncreaseTimeout = new System.Windows.Forms.CheckBox();
			this.chkAlwaysSendQueryLog = new System.Windows.Forms.CheckBox();
			this.chkAutoStopAllWhenFound = new System.Windows.Forms.CheckBox();
			this.chkAnonymousQuery = new System.Windows.Forms.CheckBox();
			this.chkIngoreAlmostIllegal = new System.Windows.Forms.CheckBox();
			this.chkIgnoreServerError = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.nudQueryTimeout = new System.Windows.Forms.NumericUpDown();
			this.nudAutoDelayOClock = new System.Windows.Forms.NumericUpDown();
			this.nudBoostSpeed = new System.Windows.Forms.NumericUpDown();
			this.label18 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.nudSleepTimeE = new System.Windows.Forms.NumericUpDown();
			this.nudSleepTime = new System.Windows.Forms.NumericUpDown();
			this.label17 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudDetectHbNonSelectInterval)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudDetectHbSelectInterval)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSleepRandom)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudAqRate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudQueryTimeout)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudAutoDelayOClock)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudBoostSpeed)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSleepTimeE)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSleepTime)).BeginInit();
			this.SuspendLayout();
			// 
			// toQuickWarning
			// 
			this.toQuickWarning.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
			this.toQuickWarning.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.toQuickWarning.BorderThickness = 1;
			this.toQuickWarning.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.toQuickWarning.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.toQuickWarning.Image = global::TOBA.Properties.Resources.warning_16;
			this.toQuickWarning.Location = new System.Drawing.Point(0, 379);
			this.toQuickWarning.Name = "toQuickWarning";
			this.toQuickWarning.Padding = new System.Windows.Forms.Padding(3);
			this.toQuickWarning.Size = new System.Drawing.Size(530, 23);
			this.toQuickWarning.TabIndex = 7;
			this.toQuickWarning.Text = "警告：查询休息时间设置过低可能会导致频繁被系统强退！";
			this.toQuickWarning.Visible = false;
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.nudDetectHbNonSelectInterval);
			this.panel1.Controls.Add(this.nudDetectHbSelectInterval);
			this.panel1.Controls.Add(this.chkNotifyMeIfHbOk);
			this.panel1.Controls.Add(this.chkAutoDetectNOSelectedTrainsHb);
			this.panel1.Controls.Add(this.chkAutoDetectSelectedTrainsHb);
			this.panel1.Controls.Add(this.nudSleepRandom);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.chkSleepRandom);
			this.panel1.Controls.Add(this.chkQueryProtector);
			this.panel1.Controls.Add(this.chkBoostSpeed);
			this.panel1.Controls.Add(this.label16);
			this.panel1.Controls.Add(this.nudAqRate);
			this.panel1.Controls.Add(this.chkAutoIncreaseTimeout);
			this.panel1.Controls.Add(this.chkAlwaysSendQueryLog);
			this.panel1.Controls.Add(this.chkAutoStopAllWhenFound);
			this.panel1.Controls.Add(this.chkAnonymousQuery);
			this.panel1.Controls.Add(this.chkIngoreAlmostIllegal);
			this.panel1.Controls.Add(this.chkIgnoreServerError);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label14);
			this.panel1.Controls.Add(this.nudQueryTimeout);
			this.panel1.Controls.Add(this.nudAutoDelayOClock);
			this.panel1.Controls.Add(this.nudBoostSpeed);
			this.panel1.Controls.Add(this.label18);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.label15);
			this.panel1.Controls.Add(this.nudSleepTimeE);
			this.panel1.Controls.Add(this.nudSleepTime);
			this.panel1.Controls.Add(this.label17);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(530, 379);
			this.panel1.TabIndex = 0;
			// 
			// nudDetectHbNonSelectInterval
			// 
			this.nudDetectHbNonSelectInterval.Location = new System.Drawing.Point(289, 299);
			this.nudDetectHbNonSelectInterval.Maximum = new decimal(new int[] {
            7200,
            0,
            0,
            0});
			this.nudDetectHbNonSelectInterval.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            0});
			this.nudDetectHbNonSelectInterval.Name = "nudDetectHbNonSelectInterval";
			this.nudDetectHbNonSelectInterval.Size = new System.Drawing.Size(65, 23);
			this.nudDetectHbNonSelectInterval.TabIndex = 39;
			this.nudDetectHbNonSelectInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudDetectHbNonSelectInterval.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
			// 
			// nudDetectHbSelectInterval
			// 
			this.nudDetectHbSelectInterval.Location = new System.Drawing.Point(289, 274);
			this.nudDetectHbSelectInterval.Maximum = new decimal(new int[] {
            7200,
            0,
            0,
            0});
			this.nudDetectHbSelectInterval.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            0});
			this.nudDetectHbSelectInterval.Name = "nudDetectHbSelectInterval";
			this.nudDetectHbSelectInterval.Size = new System.Drawing.Size(65, 23);
			this.nudDetectHbSelectInterval.TabIndex = 39;
			this.nudDetectHbSelectInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudDetectHbSelectInterval.Value = new decimal(new int[] {
            60,
            0,
            0,
            0});
			// 
			// chkNotifyMeIfHbOk
			// 
			this.chkNotifyMeIfHbOk.AutoSize = true;
			this.chkNotifyMeIfHbOk.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkNotifyMeIfHbOk.Location = new System.Drawing.Point(26, 328);
			this.chkNotifyMeIfHbOk.Name = "chkNotifyMeIfHbOk";
			this.chkNotifyMeIfHbOk.Size = new System.Drawing.Size(353, 21);
			this.chkNotifyMeIfHbOk.TabIndex = 38;
			this.chkNotifyMeIfHbOk.Text = "当已选车次的候补队列变为可用时，弹层提示我 (仅选定车次)";
			this.chkNotifyMeIfHbOk.UseVisualStyleBackColor = true;
			// 
			// chkAutoDetectNOSelectedTrainsHb
			// 
			this.chkAutoDetectNOSelectedTrainsHb.AutoSize = true;
			this.chkAutoDetectNOSelectedTrainsHb.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkAutoDetectNOSelectedTrainsHb.Location = new System.Drawing.Point(26, 302);
			this.chkAutoDetectNOSelectedTrainsHb.Name = "chkAutoDetectNOSelectedTrainsHb";
			this.chkAutoDetectNOSelectedTrainsHb.Size = new System.Drawing.Size(351, 21);
			this.chkAutoDetectNOSelectedTrainsHb.TabIndex = 38;
			this.chkAutoDetectNOSelectedTrainsHb.Text = "自动查询未选车次候补席别的排队情况，间隔　　　　　　秒";
			this.chkAutoDetectNOSelectedTrainsHb.UseVisualStyleBackColor = true;
			// 
			// chkAutoDetectSelectedTrainsHb
			// 
			this.chkAutoDetectSelectedTrainsHb.AutoSize = true;
			this.chkAutoDetectSelectedTrainsHb.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkAutoDetectSelectedTrainsHb.Location = new System.Drawing.Point(26, 276);
			this.chkAutoDetectSelectedTrainsHb.Name = "chkAutoDetectSelectedTrainsHb";
			this.chkAutoDetectSelectedTrainsHb.Size = new System.Drawing.Size(351, 21);
			this.chkAutoDetectSelectedTrainsHb.TabIndex = 38;
			this.chkAutoDetectSelectedTrainsHb.Text = "自动查询已选车次候补席别的排队情况，间隔　　　　　　秒";
			this.chkAutoDetectSelectedTrainsHb.UseVisualStyleBackColor = true;
			// 
			// nudSleepRandom
			// 
			this.nudSleepRandom.Location = new System.Drawing.Point(289, 248);
			this.nudSleepRandom.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.nudSleepRandom.Name = "nudSleepRandom";
			this.nudSleepRandom.Size = new System.Drawing.Size(65, 23);
			this.nudSleepRandom.TabIndex = 37;
			this.nudSleepRandom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudSleepRandom.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(181, 251);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(256, 17);
			this.label3.TabIndex = 36;
			this.label3.Text = "查询休息时间浮动                    毫秒 [专业版]";
			// 
			// chkSleepRandom
			// 
			this.chkSleepRandom.AutoSize = true;
			this.chkSleepRandom.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkSleepRandom.Location = new System.Drawing.Point(26, 249);
			this.chkSleepRandom.Name = "chkSleepRandom";
			this.chkSleepRandom.Size = new System.Drawing.Size(135, 21);
			this.chkSleepRandom.TabIndex = 35;
			this.chkSleepRandom.Text = "查询等待时间随机化";
			this.chkSleepRandom.UseVisualStyleBackColor = true;
			// 
			// chkQueryProtector
			// 
			this.chkQueryProtector.AutoSize = true;
			this.chkQueryProtector.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkQueryProtector.Location = new System.Drawing.Point(28, 145);
			this.chkQueryProtector.Name = "chkQueryProtector";
			this.chkQueryProtector.Size = new System.Drawing.Size(147, 21);
			this.chkQueryProtector.TabIndex = 34;
			this.chkQueryProtector.Text = "启用查询间隔安全保护";
			this.chkQueryProtector.UseVisualStyleBackColor = true;
			// 
			// chkBoostSpeed
			// 
			this.chkBoostSpeed.AutoSize = true;
			this.chkBoostSpeed.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkBoostSpeed.Location = new System.Drawing.Point(6, 64);
			this.chkBoostSpeed.Name = "chkBoostSpeed";
			this.chkBoostSpeed.Size = new System.Drawing.Size(135, 21);
			this.chkBoostSpeed.TabIndex = 1;
			this.chkBoostSpeed.Text = "整点和半点加速查询";
			this.chkBoostSpeed.UseVisualStyleBackColor = true;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(207, 121);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(20, 17);
			this.label16.TabIndex = 32;
			this.label16.Text = "秒";
			// 
			// nudAqRate
			// 
			this.nudAqRate.Location = new System.Drawing.Point(319, 142);
			this.nudAqRate.Name = "nudAqRate";
			this.nudAqRate.Size = new System.Drawing.Size(48, 23);
			this.nudAqRate.TabIndex = 14;
			this.nudAqRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudAqRate.Visible = false;
			// 
			// chkAutoIncreaseTimeout
			// 
			this.chkAutoIncreaseTimeout.AutoSize = true;
			this.chkAutoIncreaseTimeout.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkAutoIncreaseTimeout.Location = new System.Drawing.Point(233, 118);
			this.chkAutoIncreaseTimeout.Name = "chkAutoIncreaseTimeout";
			this.chkAutoIncreaseTimeout.Size = new System.Drawing.Size(159, 21);
			this.chkAutoIncreaseTimeout.TabIndex = 9;
			this.chkAutoIncreaseTimeout.Text = "频繁超时时自动增加设置";
			this.chkAutoIncreaseTimeout.UseVisualStyleBackColor = true;
			// 
			// chkAlwaysSendQueryLog
			// 
			this.chkAlwaysSendQueryLog.AutoSize = true;
			this.chkAlwaysSendQueryLog.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkAlwaysSendQueryLog.Location = new System.Drawing.Point(365, 170);
			this.chkAlwaysSendQueryLog.Name = "chkAlwaysSendQueryLog";
			this.chkAlwaysSendQueryLog.Size = new System.Drawing.Size(135, 21);
			this.chkAlwaysSendQueryLog.TabIndex = 9;
			this.chkAlwaysSendQueryLog.Text = "总是发送QueryLog";
			this.chkAlwaysSendQueryLog.UseVisualStyleBackColor = true;
			this.chkAlwaysSendQueryLog.Visible = false;
			// 
			// chkAutoStopAllWhenFound
			// 
			this.chkAutoStopAllWhenFound.AutoSize = true;
			this.chkAutoStopAllWhenFound.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkAutoStopAllWhenFound.Location = new System.Drawing.Point(141, 170);
			this.chkAutoStopAllWhenFound.Name = "chkAutoStopAllWhenFound";
			this.chkAutoStopAllWhenFound.Size = new System.Drawing.Size(219, 21);
			this.chkAutoStopAllWhenFound.TabIndex = 8;
			this.chkAutoStopAllWhenFound.Text = "查到票后，自动停止其它所有的查询";
			this.chkAutoStopAllWhenFound.UseVisualStyleBackColor = true;
			// 
			// chkAnonymousQuery
			// 
			this.chkAnonymousQuery.AutoSize = true;
			this.chkAnonymousQuery.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkAnonymousQuery.ForeColor = System.Drawing.Color.DarkRed;
			this.chkAnonymousQuery.Location = new System.Drawing.Point(28, 222);
			this.chkAnonymousQuery.Name = "chkAnonymousQuery";
			this.chkAnonymousQuery.Size = new System.Drawing.Size(99, 21);
			this.chkAnonymousQuery.TabIndex = 12;
			this.chkAnonymousQuery.Text = "启用匿名查询";
			this.chkAnonymousQuery.UseVisualStyleBackColor = true;
			// 
			// chkIngoreAlmostIllegal
			// 
			this.chkIngoreAlmostIllegal.AutoSize = true;
			this.chkIngoreAlmostIllegal.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkIngoreAlmostIllegal.ForeColor = System.Drawing.Color.DarkRed;
			this.chkIngoreAlmostIllegal.Location = new System.Drawing.Point(28, 195);
			this.chkIngoreAlmostIllegal.Name = "chkIngoreAlmostIllegal";
			this.chkIngoreAlmostIllegal.Size = new System.Drawing.Size(484, 21);
			this.chkIngoreAlmostIllegal.TabIndex = 11;
			this.chkIngoreAlmostIllegal.Text = "如果返回的数据被检测出可能是伪数据，则不进行车次匹配（直接视作无票—不推荐）";
			this.chkIngoreAlmostIllegal.UseVisualStyleBackColor = true;
			// 
			// chkIgnoreServerError
			// 
			this.chkIgnoreServerError.AutoSize = true;
			this.chkIgnoreServerError.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkIgnoreServerError.Location = new System.Drawing.Point(28, 170);
			this.chkIgnoreServerError.Name = "chkIgnoreServerError";
			this.chkIgnoreServerError.Size = new System.Drawing.Size(111, 21);
			this.chkIgnoreServerError.TabIndex = 7;
			this.chkIgnoreServerError.Text = "忽略服务器错误";
			this.chkIgnoreServerError.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.ForeColor = System.Drawing.Color.DarkRed;
			this.label6.Location = new System.Drawing.Point(144, 223);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(308, 17);
			this.label6.TabIndex = 24;
			this.label6.Text = "实验性，导致无法提交订单或无法查询时，请取消此选项";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label5.Location = new System.Drawing.Point(373, 146);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(51, 17);
			this.label5.TabIndex = 23;
			this.label5.Text = "默认为6";
			this.label5.Visible = false;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(230, 64);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(246, 17);
			this.label14.TabIndex = 25;
			this.label14.Text = "(毫秒)。整点半点的开始至30秒以此速度查询";
			// 
			// nudQueryTimeout
			// 
			this.nudQueryTimeout.Location = new System.Drawing.Point(141, 117);
			this.nudQueryTimeout.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
			this.nudQueryTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudQueryTimeout.Name = "nudQueryTimeout";
			this.nudQueryTimeout.Size = new System.Drawing.Size(60, 23);
			this.nudQueryTimeout.TabIndex = 5;
			this.nudQueryTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudQueryTimeout.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
			// 
			// nudAutoDelayOClock
			// 
			this.nudAutoDelayOClock.Location = new System.Drawing.Point(141, 91);
			this.nudAutoDelayOClock.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
			this.nudAutoDelayOClock.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            -2147483648});
			this.nudAutoDelayOClock.Name = "nudAutoDelayOClock";
			this.nudAutoDelayOClock.Size = new System.Drawing.Size(83, 23);
			this.nudAutoDelayOClock.TabIndex = 4;
			this.nudAutoDelayOClock.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudAutoDelayOClock.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// nudBoostSpeed
			// 
			this.nudBoostSpeed.Location = new System.Drawing.Point(141, 62);
			this.nudBoostSpeed.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.nudBoostSpeed.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
			this.nudBoostSpeed.Name = "nudBoostSpeed";
			this.nudBoostSpeed.Size = new System.Drawing.Size(83, 23);
			this.nudBoostSpeed.TabIndex = 2;
			this.nudBoostSpeed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudBoostSpeed.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
			// 
			// label18
			// 
			this.label18.AutoSize = true;
			this.label18.Location = new System.Drawing.Point(230, 36);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(224, 17);
			this.label18.TabIndex = 21;
			this.label18.Text = "设置查询失败之后等待的时间，单位是秒";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(230, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(248, 17);
			this.label2.TabIndex = 21;
			this.label2.Text = "设置两次查询之间必须等待的时间，单位是秒";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label9.Location = new System.Drawing.Point(230, 146);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(80, 17);
			this.label9.TabIndex = 13;
			this.label9.Text = "匿名查询频率";
			this.label9.Visible = false;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label7.Location = new System.Drawing.Point(59, 93);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(80, 17);
			this.label7.TabIndex = 12;
			this.label7.Text = "整点查询延迟";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label15.Location = new System.Drawing.Point(79, 121);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(56, 17);
			this.label15.TabIndex = 11;
			this.label15.Text = "查询超时";
			// 
			// nudSleepTimeE
			// 
			this.nudSleepTimeE.DecimalPlaces = 2;
			this.nudSleepTimeE.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
			this.nudSleepTimeE.Location = new System.Drawing.Point(141, 34);
			this.nudSleepTimeE.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.nudSleepTimeE.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.nudSleepTimeE.Name = "nudSleepTimeE";
			this.nudSleepTimeE.Size = new System.Drawing.Size(83, 23);
			this.nudSleepTimeE.TabIndex = 0;
			this.nudSleepTimeE.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudSleepTimeE.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			// 
			// nudSleepTime
			// 
			this.nudSleepTime.DecimalPlaces = 2;
			this.nudSleepTime.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
			this.nudSleepTime.Location = new System.Drawing.Point(141, 6);
			this.nudSleepTime.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.nudSleepTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.nudSleepTime.Name = "nudSleepTime";
			this.nudSleepTime.Size = new System.Drawing.Size(83, 23);
			this.nudSleepTime.TabIndex = 0;
			this.nudSleepTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.nudSleepTime.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label17.Location = new System.Drawing.Point(23, 38);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(116, 17);
			this.label17.TabIndex = 15;
			this.label17.Text = "查询出错时休息时间";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(59, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 17);
			this.label1.TabIndex = 15;
			this.label1.Text = "查询休息时间";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(230, 93);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(251, 17);
			this.label8.TabIndex = 22;
			this.label8.Text = "(S)整点后延迟指定的时间再查询，可以为负数";
			// 
			// QueryConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.toQuickWarning);
			this.Name = "QueryConfig";
			this.Size = new System.Drawing.Size(530, 402);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudDetectHbNonSelectInterval)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudDetectHbSelectInterval)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSleepRandom)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudAqRate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudQueryTimeout)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudAutoDelayOClock)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudBoostSpeed)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSleepTimeE)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nudSleepTime)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
        private TipBarMessage toQuickWarning;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox chkIngoreAlmostIllegal;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown nudSleepTime;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.NumericUpDown nudAutoDelayOClock;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox chkIgnoreServerError;
		private System.Windows.Forms.CheckBox chkAnonymousQuery;
		private System.Windows.Forms.CheckBox chkAutoStopAllWhenFound;
		private System.Windows.Forms.NumericUpDown nudAqRate;
		private System.Windows.Forms.CheckBox chkAlwaysSendQueryLog;
		private System.Windows.Forms.CheckBox chkBoostSpeed;
		private System.Windows.Forms.NumericUpDown nudBoostSpeed;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.NumericUpDown nudQueryTimeout;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.CheckBox chkAutoIncreaseTimeout;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.NumericUpDown nudSleepTimeE;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.CheckBox chkQueryProtector;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkSleepRandom;
		private System.Windows.Forms.NumericUpDown nudSleepRandom;
		private System.Windows.Forms.NumericUpDown nudDetectHbNonSelectInterval;
		private System.Windows.Forms.NumericUpDown nudDetectHbSelectInterval;
		private System.Windows.Forms.CheckBox chkNotifyMeIfHbOk;
		private System.Windows.Forms.CheckBox chkAutoDetectNOSelectedTrainsHb;
		private System.Windows.Forms.CheckBox chkAutoDetectSelectedTrainsHb;
	}
}
