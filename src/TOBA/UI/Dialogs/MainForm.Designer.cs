using FSLib.Network.Http;

namespace TOBA.UI.Dialogs
{
	partial class MainForm
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
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.stuMute = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsStatusServerTime = new System.Windows.Forms.ToolStripStatusLabel();
			this.stuStatistics = new System.Windows.Forms.ToolStripDropDownButton();
			this.stuStatisticsQuery = new System.Windows.Forms.ToolStripMenuItem();
			this.总计调用次数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.成功次数SuccessCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.错误次数Code200CountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.错误次数Code302CountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.错误次数Code405CountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.错误次数Code502CountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.连接失败错误次数ConnectErrorCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.查询超时错误次数TimeoutCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.返回空数据次数DataEmptyCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.返回错误数据次数DataExceptionCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.空响应次数EmptyResponseCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.其它网络错误次数NetworkErrorCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.接口变更次数QueryInterfaceChangeCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.返回查询失败次数ResultFailedCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.其它失败错误次数OtherFailedCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.助手总运行时间RunningTimeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.登录次数累计LoginCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.成功订票次数累计SubmitSuccessCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.提交订单次数累计SubmitCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.查票次数累计QueryCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.网络请求次数累计WebRequestCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.助手启动次数StartupCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stuAdv = new System.Windows.Forms.ToolStripMenuItem();
			this.tabImgList = new System.Windows.Forms.ImageList(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.ni = new System.Windows.Forms.NotifyIcon(this.components);
			this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.stWelcome = new DevComponents.DotNetBar.SuperTabItem();
			this.groupPanel1 = new DevComponents.DotNetBar.Controls.GroupPanel();
			this.st = new DevComponents.DotNetBar.SuperTabControl();
			this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.tabWelcomeContent1 = new TOBA.UI.Controls.Misc.TabWelcomeContent();
			this.stHome = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabControlPanel4 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.sysLogPanel1 = new TOBA.UI.Controls.Log.SysLogPanel();
			this.stSysLog = new DevComponents.DotNetBar.SuperTabItem();
			this.sm = new DevComponents.DotNetBar.StyleManager(this.components);
			this.userrelogin = new TOBA.UI.Components.Account.UserLoginComponent(this.components);
			this.topNav1 = new TOBA.UI.Controls.MainFrame.TopNav();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.statusStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.st)).BeginInit();
			this.st.SuspendLayout();
			this.superTabControlPanel2.SuspendLayout();
			this.superTabControlPanel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.stuMute,
            this.tsStatusServerTime,
            this.stuStatistics,
            this.stuAdv});
			this.statusStrip1.Location = new System.Drawing.Point(0, 884);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
			this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
			this.statusStrip1.ShowItemToolTips = true;
			this.statusStrip1.Size = new System.Drawing.Size(1566, 32);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 2;
			// 
			// stuMute
			// 
			this.stuMute.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.stuMute.Image = global::TOBA.Properties.Resources.Mute;
			this.stuMute.IsLink = true;
			this.stuMute.LinkColor = System.Drawing.Color.Purple;
			this.stuMute.Name = "stuMute";
			this.stuMute.Size = new System.Drawing.Size(72, 25);
			this.stuMute.Text = "静音";
			// 
			// tsStatusServerTime
			// 
			this.tsStatusServerTime.Image = global::TOBA.Properties.Resources.alarm_clock;
			this.tsStatusServerTime.Name = "tsStatusServerTime";
			this.tsStatusServerTime.Size = new System.Drawing.Size(196, 25);
			this.tsStatusServerTime.Text = "尚未获得服务器时间";
			this.tsStatusServerTime.ToolTipText = "服务器时间。依据网络的速度和服务器速度，此值可能会被随时修正，而且可能存在部分差异。";
			// 
			// stuStatistics
			// 
			this.stuStatistics.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stuStatisticsQuery,
            this.toolStripSeparator1,
            this.助手总运行时间RunningTimeToolStripMenuItem,
            this.登录次数累计LoginCountToolStripMenuItem,
            this.成功订票次数累计SubmitSuccessCountToolStripMenuItem,
            this.提交订单次数累计SubmitCountToolStripMenuItem,
            this.查票次数累计QueryCountToolStripMenuItem,
            this.网络请求次数累计WebRequestCountToolStripMenuItem,
            this.助手启动次数StartupCountToolStripMenuItem});
			this.stuStatistics.Image = global::TOBA.Properties.Resources.statistics_16;
			this.stuStatistics.Name = "stuStatistics";
			this.stuStatistics.Size = new System.Drawing.Size(88, 29);
			this.stuStatistics.Text = "统计";
			// 
			// stuStatisticsQuery
			// 
			this.stuStatisticsQuery.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.总计调用次数ToolStripMenuItem,
            this.toolStripMenuItem2,
            this.成功次数SuccessCountToolStripMenuItem,
            this.错误次数Code200CountToolStripMenuItem,
            this.错误次数Code302CountToolStripMenuItem,
            this.错误次数Code405CountToolStripMenuItem,
            this.错误次数Code502CountToolStripMenuItem,
            this.连接失败错误次数ConnectErrorCountToolStripMenuItem,
            this.查询超时错误次数TimeoutCountToolStripMenuItem,
            this.返回空数据次数DataEmptyCountToolStripMenuItem,
            this.返回错误数据次数DataExceptionCountToolStripMenuItem,
            this.空响应次数EmptyResponseCountToolStripMenuItem,
            this.其它网络错误次数NetworkErrorCountToolStripMenuItem,
            this.接口变更次数QueryInterfaceChangeCountToolStripMenuItem,
            this.返回查询失败次数ResultFailedCountToolStripMenuItem,
            this.其它失败错误次数OtherFailedCountToolStripMenuItem});
			this.stuStatisticsQuery.Image = global::TOBA.Properties.Resources.cou_16_chart_pie;
			this.stuStatisticsQuery.Name = "stuStatisticsQuery";
			this.stuStatisticsQuery.Size = new System.Drawing.Size(455, 34);
			this.stuStatisticsQuery.Text = "12306查询接口统计";
			// 
			// 总计调用次数ToolStripMenuItem
			// 
			this.总计调用次数ToolStripMenuItem.Name = "总计调用次数ToolStripMenuItem";
			this.总计调用次数ToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
			this.总计调用次数ToolStripMenuItem.Text = "总计调用次数\t\t#TotalCount#";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(488, 6);
			// 
			// 成功次数SuccessCountToolStripMenuItem
			// 
			this.成功次数SuccessCountToolStripMenuItem.Name = "成功次数SuccessCountToolStripMenuItem";
			this.成功次数SuccessCountToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
			this.成功次数SuccessCountToolStripMenuItem.Text = "成功次数\t\t#SuccessCount#";
			// 
			// 错误次数Code200CountToolStripMenuItem
			// 
			this.错误次数Code200CountToolStripMenuItem.Name = "错误次数Code200CountToolStripMenuItem";
			this.错误次数Code200CountToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
			this.错误次数Code200CountToolStripMenuItem.Text = "200错误次数\t\t#Code200Count#";
			// 
			// 错误次数Code302CountToolStripMenuItem
			// 
			this.错误次数Code302CountToolStripMenuItem.Name = "错误次数Code302CountToolStripMenuItem";
			this.错误次数Code302CountToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
			this.错误次数Code302CountToolStripMenuItem.Text = "302错误次数\t\t#Code302Count#";
			// 
			// 错误次数Code405CountToolStripMenuItem
			// 
			this.错误次数Code405CountToolStripMenuItem.Name = "错误次数Code405CountToolStripMenuItem";
			this.错误次数Code405CountToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
			this.错误次数Code405CountToolStripMenuItem.Text = "405错误次数 #Code405Count#";
			// 
			// 错误次数Code502CountToolStripMenuItem
			// 
			this.错误次数Code502CountToolStripMenuItem.Name = "错误次数Code502CountToolStripMenuItem";
			this.错误次数Code502CountToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
			this.错误次数Code502CountToolStripMenuItem.Text = "502错误次数 #Code502Count#";
			// 
			// 连接失败错误次数ConnectErrorCountToolStripMenuItem
			// 
			this.连接失败错误次数ConnectErrorCountToolStripMenuItem.Name = "连接失败错误次数ConnectErrorCountToolStripMenuItem";
			this.连接失败错误次数ConnectErrorCountToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
			this.连接失败错误次数ConnectErrorCountToolStripMenuItem.Text = "连接失败错误次数 #ConnectErrorCount#";
			// 
			// 查询超时错误次数TimeoutCountToolStripMenuItem
			// 
			this.查询超时错误次数TimeoutCountToolStripMenuItem.Name = "查询超时错误次数TimeoutCountToolStripMenuItem";
			this.查询超时错误次数TimeoutCountToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
			this.查询超时错误次数TimeoutCountToolStripMenuItem.Text = "查询超时错误次数 #TimeoutCount#";
			// 
			// 返回空数据次数DataEmptyCountToolStripMenuItem
			// 
			this.返回空数据次数DataEmptyCountToolStripMenuItem.Name = "返回空数据次数DataEmptyCountToolStripMenuItem";
			this.返回空数据次数DataEmptyCountToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
			this.返回空数据次数DataEmptyCountToolStripMenuItem.Text = "返回空数据次数 #DataEmptyCount#";
			// 
			// 返回错误数据次数DataExceptionCountToolStripMenuItem
			// 
			this.返回错误数据次数DataExceptionCountToolStripMenuItem.Name = "返回错误数据次数DataExceptionCountToolStripMenuItem";
			this.返回错误数据次数DataExceptionCountToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
			this.返回错误数据次数DataExceptionCountToolStripMenuItem.Text = "返回错误数据次数 #DataExceptionCount#";
			// 
			// 空响应次数EmptyResponseCountToolStripMenuItem
			// 
			this.空响应次数EmptyResponseCountToolStripMenuItem.Name = "空响应次数EmptyResponseCountToolStripMenuItem";
			this.空响应次数EmptyResponseCountToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
			this.空响应次数EmptyResponseCountToolStripMenuItem.Text = "空响应次数 #EmptyReponseCount#";
			// 
			// 其它网络错误次数NetworkErrorCountToolStripMenuItem
			// 
			this.其它网络错误次数NetworkErrorCountToolStripMenuItem.Name = "其它网络错误次数NetworkErrorCountToolStripMenuItem";
			this.其它网络错误次数NetworkErrorCountToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
			this.其它网络错误次数NetworkErrorCountToolStripMenuItem.Text = "其它网络错误次数 #NetworkErrorCount#";
			// 
			// 接口变更次数QueryInterfaceChangeCountToolStripMenuItem
			// 
			this.接口变更次数QueryInterfaceChangeCountToolStripMenuItem.Name = "接口变更次数QueryInterfaceChangeCountToolStripMenuItem";
			this.接口变更次数QueryInterfaceChangeCountToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
			this.接口变更次数QueryInterfaceChangeCountToolStripMenuItem.Text = "接口变更次数 #QueryInterfaceChangeCount#";
			// 
			// 返回查询失败次数ResultFailedCountToolStripMenuItem
			// 
			this.返回查询失败次数ResultFailedCountToolStripMenuItem.Name = "返回查询失败次数ResultFailedCountToolStripMenuItem";
			this.返回查询失败次数ResultFailedCountToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
			this.返回查询失败次数ResultFailedCountToolStripMenuItem.Text = "返回查询失败次数 #ResultFailedCount#";
			// 
			// 其它失败错误次数OtherFailedCountToolStripMenuItem
			// 
			this.其它失败错误次数OtherFailedCountToolStripMenuItem.Name = "其它失败错误次数OtherFailedCountToolStripMenuItem";
			this.其它失败错误次数OtherFailedCountToolStripMenuItem.Size = new System.Drawing.Size(491, 34);
			this.其它失败错误次数OtherFailedCountToolStripMenuItem.Text = "其它失败错误次数 #OtherFailedCount#";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(452, 6);
			// 
			// 助手总运行时间RunningTimeToolStripMenuItem
			// 
			this.助手总运行时间RunningTimeToolStripMenuItem.Name = "助手总运行时间RunningTimeToolStripMenuItem";
			this.助手总运行时间RunningTimeToolStripMenuItem.Size = new System.Drawing.Size(455, 34);
			this.助手总运行时间RunningTimeToolStripMenuItem.Text = "助手总运行时间\t\t#RunningTime#";
			// 
			// 登录次数累计LoginCountToolStripMenuItem
			// 
			this.登录次数累计LoginCountToolStripMenuItem.Name = "登录次数累计LoginCountToolStripMenuItem";
			this.登录次数累计LoginCountToolStripMenuItem.Size = new System.Drawing.Size(455, 34);
			this.登录次数累计LoginCountToolStripMenuItem.Text = "登录次数累计\t\t#LoginCount#";
			// 
			// 成功订票次数累计SubmitSuccessCountToolStripMenuItem
			// 
			this.成功订票次数累计SubmitSuccessCountToolStripMenuItem.Name = "成功订票次数累计SubmitSuccessCountToolStripMenuItem";
			this.成功订票次数累计SubmitSuccessCountToolStripMenuItem.Size = new System.Drawing.Size(455, 34);
			this.成功订票次数累计SubmitSuccessCountToolStripMenuItem.Text = "成功订票次数累计\t\t#SubmitSuccessCount#";
			// 
			// 提交订单次数累计SubmitCountToolStripMenuItem
			// 
			this.提交订单次数累计SubmitCountToolStripMenuItem.Name = "提交订单次数累计SubmitCountToolStripMenuItem";
			this.提交订单次数累计SubmitCountToolStripMenuItem.Size = new System.Drawing.Size(455, 34);
			this.提交订单次数累计SubmitCountToolStripMenuItem.Text = "提交订单次数累计\t\t#SubmitCount#";
			// 
			// 查票次数累计QueryCountToolStripMenuItem
			// 
			this.查票次数累计QueryCountToolStripMenuItem.Name = "查票次数累计QueryCountToolStripMenuItem";
			this.查票次数累计QueryCountToolStripMenuItem.Size = new System.Drawing.Size(455, 34);
			this.查票次数累计QueryCountToolStripMenuItem.Text = "查票次数累计\t\t#QueryCount#";
			// 
			// 网络请求次数累计WebRequestCountToolStripMenuItem
			// 
			this.网络请求次数累计WebRequestCountToolStripMenuItem.Name = "网络请求次数累计WebRequestCountToolStripMenuItem";
			this.网络请求次数累计WebRequestCountToolStripMenuItem.Size = new System.Drawing.Size(455, 34);
			this.网络请求次数累计WebRequestCountToolStripMenuItem.Text = "网络请求次数累计\t\t#WebRequestCount#";
			// 
			// 助手启动次数StartupCountToolStripMenuItem
			// 
			this.助手启动次数StartupCountToolStripMenuItem.Name = "助手启动次数StartupCountToolStripMenuItem";
			this.助手启动次数StartupCountToolStripMenuItem.Size = new System.Drawing.Size(455, 34);
			this.助手启动次数StartupCountToolStripMenuItem.Text = "助手启动次数累计\t\t#StartupCount#";
			// 
			// stuAdv
			// 
			this.stuAdv.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.stuAdv.ForeColor = System.Drawing.Color.Green;
			this.stuAdv.Image = global::TOBA.Properties.Resources.ion_social_github;
			this.stuAdv.Name = "stuAdv";
			this.stuAdv.Size = new System.Drawing.Size(126, 29);
			this.stuAdv.Text = "GITHUB";
			// 
			// tabImgList
			// 
			this.tabImgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.tabImgList.ImageSize = new System.Drawing.Size(16, 16);
			this.tabImgList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// toolTip1
			// 
			this.toolTip1.IsBalloon = true;
			this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.toolTip1.ToolTipTitle = "订票助手提示";
			// 
			// ni
			// 
			this.ni.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.ni.BalloonTipTitle = "订票助手.NET";
			this.ni.Text = "订票助手.NET";
			this.ni.Visible = true;
			// 
			// superTabControlPanel1
			// 
			this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel1.Location = new System.Drawing.Point(191, 0);
			this.superTabControlPanel1.Name = "superTabControlPanel1";
			this.superTabControlPanel1.Size = new System.Drawing.Size(811, 274);
			this.superTabControlPanel1.TabIndex = 1;
			this.superTabControlPanel1.TabItem = this.stWelcome;
			// 
			// stWelcome
			// 
			this.stWelcome.AttachedControl = this.superTabControlPanel1;
			this.stWelcome.GlobalItem = false;
			this.stWelcome.Name = "stWelcome";
			this.stWelcome.Text = "欢迎使用订票助手.NET";
			// 
			// groupPanel1
			// 
			this.groupPanel1.DisabledBackColor = System.Drawing.Color.Empty;
			this.groupPanel1.Location = new System.Drawing.Point(0, 0);
			this.groupPanel1.Name = "groupPanel1";
			this.groupPanel1.Size = new System.Drawing.Size(200, 100);
			// 
			// 
			// 
			this.groupPanel1.Style.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.groupPanel1.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.groupPanel1.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.groupPanel1.TabIndex = 0;
			// 
			// st
			// 
			this.st.AutoCloseTabs = false;
			this.st.CloseButtonOnTabsAlwaysDisplayed = false;
			this.st.CloseButtonOnTabsVisible = true;
			// 
			// 
			// 
			// 
			// 
			// 
			this.st.ControlBox.CloseBox.Name = "";
			// 
			// 
			// 
			this.st.ControlBox.MenuBox.AutoHide = true;
			this.st.ControlBox.MenuBox.Name = "";
			this.st.ControlBox.MenuBox.Visible = false;
			this.st.ControlBox.Name = "";
			this.st.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.st.ControlBox.MenuBox,
            this.st.ControlBox.CloseBox});
			this.st.ControlBox.Visible = false;
			this.st.Controls.Add(this.superTabControlPanel2);
			this.st.Controls.Add(this.superTabControlPanel4);
			this.st.Dock = System.Windows.Forms.DockStyle.Fill;
			this.st.FixedTabSize = new System.Drawing.Size(180, 0);
			this.st.ImageList = this.tabImgList;
			this.st.Location = new System.Drawing.Point(0, 64);
			this.st.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.st.Name = "st";
			this.st.ReorderTabsEnabled = true;
			this.st.SelectedTabFont = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.st.SelectedTabIndex = 0;
			this.st.Size = new System.Drawing.Size(1566, 820);
			this.st.TabAlignment = DevComponents.DotNetBar.eTabStripAlignment.Left;
			this.st.TabFont = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.st.TabHorizontalSpacing = 10;
			this.st.TabIndex = 4;
			this.st.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.stHome,
            this.stSysLog});
			this.st.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.Office2010BackstageBlue;
			this.st.TabVerticalSpacing = 8;
			// 
			// superTabControlPanel2
			// 
			this.superTabControlPanel2.Controls.Add(this.tabWelcomeContent1);
			this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel2.Location = new System.Drawing.Point(183, 0);
			this.superTabControlPanel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.superTabControlPanel2.Name = "superTabControlPanel2";
			this.superTabControlPanel2.Size = new System.Drawing.Size(1383, 820);
			this.superTabControlPanel2.TabIndex = 2;
			this.superTabControlPanel2.TabItem = this.stHome;
			// 
			// tabWelcomeContent1
			// 
			this.tabWelcomeContent1.BackColor = System.Drawing.SystemColors.Window;
			this.tabWelcomeContent1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabWelcomeContent1.Location = new System.Drawing.Point(0, 0);
			this.tabWelcomeContent1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tabWelcomeContent1.Name = "tabWelcomeContent1";
			this.tabWelcomeContent1.Size = new System.Drawing.Size(1383, 820);
			this.tabWelcomeContent1.TabIndex = 0;
			// 
			// stHome
			// 
			this.stHome.AttachedControl = this.superTabControlPanel2;
			this.stHome.CloseButtonVisible = false;
			this.stHome.EnableImageAnimation = true;
			this.stHome.GlobalItem = false;
			this.stHome.Image = global::TOBA.Properties.Resources.home_16;
			this.stHome.Name = "stHome";
			this.stHome.Text = "欢迎使用订票助手.NET";
			// 
			// superTabControlPanel4
			// 
			this.superTabControlPanel4.Controls.Add(this.sysLogPanel1);
			this.superTabControlPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel4.Location = new System.Drawing.Point(273, 0);
			this.superTabControlPanel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.superTabControlPanel4.Name = "superTabControlPanel4";
			this.superTabControlPanel4.Size = new System.Drawing.Size(1293, 882);
			this.superTabControlPanel4.TabIndex = 0;
			this.superTabControlPanel4.TabItem = this.stSysLog;
			this.superTabControlPanel4.Visible = false;
			// 
			// sysLogPanel1
			// 
			this.sysLogPanel1.BackColor = System.Drawing.SystemColors.Window;
			this.sysLogPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.sysLogPanel1.Image = null;
			this.sysLogPanel1.Location = new System.Drawing.Point(0, 0);
			this.sysLogPanel1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.sysLogPanel1.Name = "sysLogPanel1";
			this.sysLogPanel1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.sysLogPanel1.Size = new System.Drawing.Size(1293, 882);
			this.sysLogPanel1.TabIndex = 0;
			// 
			// stSysLog
			// 
			this.stSysLog.AttachedControl = this.superTabControlPanel4;
			this.stSysLog.ClickAutoRepeat = true;
			this.stSysLog.CloseButtonVisible = false;
			this.stSysLog.EnableImageAnimation = true;
			this.stSysLog.GlobalItem = false;
			this.stSysLog.Image = global::TOBA.Properties.Resources.letter_16;
			this.stSysLog.Name = "stSysLog";
			this.stSysLog.Text = "系统消息";
			// 
			// sm
			// 
			this.sm.ManagerStyle = DevComponents.DotNetBar.eStyle.Office2007Blue;
			this.sm.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
			// 
			// userrelogin
			// 
			this.userrelogin.EnableFallback = false;
			this.userrelogin.OwnerForm = null;
			// 
			// topNav1
			// 
			this.topNav1.Dock = System.Windows.Forms.DockStyle.Top;
			this.topNav1.Location = new System.Drawing.Point(0, 0);
			this.topNav1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.topNav1.Name = "topNav1";
			this.topNav1.Size = new System.Drawing.Size(1566, 64);
			this.topNav1.TabIndex = 5;
			// 
			// toolStripStatusLabel1
			// 
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(1015, 25);
			this.toolStripStatusLabel1.Spring = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1566, 916);
			this.Controls.Add(this.st);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.topNav1);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "订票助手.NET";
			this.Load += new System.EventHandler(this.MainForm_Load);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.st)).EndInit();
			this.st.ResumeLayout(false);
			this.superTabControlPanel2.ResumeLayout(false);
			this.superTabControlPanel4.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripDropDownButton stuStatistics;
		private System.Windows.Forms.ImageList tabImgList;
		private System.Windows.Forms.ToolStripStatusLabel tsStatusServerTime;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ToolStripStatusLabel stuMute;
		private System.Windows.Forms.NotifyIcon ni;
		private System.Windows.Forms.ToolStripMenuItem 助手启动次数StartupCountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 网络请求次数累计WebRequestCountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 查票次数累计QueryCountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 提交订单次数累计SubmitCountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 成功订票次数累计SubmitSuccessCountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 登录次数累计LoginCountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 助手总运行时间RunningTimeToolStripMenuItem;
		private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
		private DevComponents.DotNetBar.Controls.GroupPanel groupPanel1;
		private DevComponents.DotNetBar.SuperTabItem stWelcome;
		private DevComponents.DotNetBar.SuperTabControl st;
		private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
		private DevComponents.DotNetBar.SuperTabItem stHome;
		private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel4;
		private DevComponents.DotNetBar.SuperTabItem stSysLog;
		private Controls.Log.SysLogPanel sysLogPanel1;
		private Controls.Misc.TabWelcomeContent tabWelcomeContent1;
		private DevComponents.DotNetBar.StyleManager sm;
		private Components.Account.UserLoginComponent userrelogin;
		private System.Windows.Forms.ToolStripMenuItem stuStatisticsQuery;
		private System.Windows.Forms.ToolStripMenuItem 总计调用次数ToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem 成功次数SuccessCountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 错误次数Code200CountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 错误次数Code302CountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 错误次数Code405CountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 错误次数Code502CountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 连接失败错误次数ConnectErrorCountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 查询超时错误次数TimeoutCountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 返回空数据次数DataEmptyCountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 返回错误数据次数DataExceptionCountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 空响应次数EmptyResponseCountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 其它网络错误次数NetworkErrorCountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 接口变更次数QueryInterfaceChangeCountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 返回查询失败次数ResultFailedCountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 其它失败错误次数OtherFailedCountToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private Controls.MainFrame.TopNav topNav1;
		private System.Windows.Forms.ToolStripMenuItem stuAdv;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
	}
}