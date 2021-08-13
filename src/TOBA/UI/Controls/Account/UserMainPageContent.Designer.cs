namespace TOBA.UI.Controls.Account
{
	partial class UserMainPageContent
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
			this.components = new System.ComponentModel.Container();
			this.ctxTab = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsSendTo = new System.Windows.Forms.ToolStripMenuItem();
			this.tsNoOtherAccount = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuAddQuery = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuRemoveQuery = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.mnuChangeName = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSaveAsTheme = new System.Windows.Forms.ToolStripMenuItem();
			this.st = new DevComponents.DotNetBar.SuperTabControl();
			this.superTabControlPanel5 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.backupOrderContainer = new TOBA.UI.Controls.BackupOrder.BackupOrderContainer();
			this.stBackupOrder = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.userTabOperation1 = new TOBA.UI.Controls.Account.UserTabOperation();
			this.stOperation = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.advancedToolPanel1 = new TOBA.UI.Controls.QueryManager.QmPanel();
			this.stQueryManage = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabControlPanel4 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.passengerPanel1 = new TOBA.UI.Controls.Passenger.PassengerPanel();
			this.stPasManage = new DevComponents.DotNetBar.SuperTabItem();
			this.superTabControlPanel3 = new DevComponents.DotNetBar.SuperTabControlPanel();
			this.orderPanel1 = new TOBA.UI.Controls.Order.OrderPanel();
			this.stOrderManage = new DevComponents.DotNetBar.SuperTabItem();
			this.ctxTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.st)).BeginInit();
			this.st.SuspendLayout();
			this.superTabControlPanel5.SuspendLayout();
			this.superTabControlPanel1.SuspendLayout();
			this.superTabControlPanel2.SuspendLayout();
			this.superTabControlPanel4.SuspendLayout();
			this.superTabControlPanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// ctxTab
			// 
			this.ctxTab.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSendTo,
            this.toolStripSeparator2,
            this.mnuAddQuery,
            this.mnuRemoveQuery,
            this.toolStripSeparator1,
            this.mnuChangeName,
            this.mnuSaveAsTheme});
			this.ctxTab.Name = "contextMenuStrip1";
			this.ctxTab.Size = new System.Drawing.Size(177, 126);
			// 
			// tsSendTo
			// 
			this.tsSendTo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNoOtherAccount});
			this.tsSendTo.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold);
			this.tsSendTo.Image = global::TOBA.Properties.Resources.layer_export;
			this.tsSendTo.Name = "tsSendTo";
			this.tsSendTo.Size = new System.Drawing.Size(176, 22);
			this.tsSendTo.Text = "复制到其它账户";
			// 
			// tsNoOtherAccount
			// 
			this.tsNoOtherAccount.Enabled = false;
			this.tsNoOtherAccount.Image = global::TOBA.Properties.Resources.block_16;
			this.tsNoOtherAccount.Name = "tsNoOtherAccount";
			this.tsNoOtherAccount.Size = new System.Drawing.Size(184, 22);
			this.tsNoOtherAccount.Text = "没有其它已登录账户";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(173, 6);
			// 
			// mnuAddQuery
			// 
			this.mnuAddQuery.Image = global::TOBA.Properties.Resources.add;
			this.mnuAddQuery.Name = "mnuAddQuery";
			this.mnuAddQuery.Size = new System.Drawing.Size(176, 22);
			this.mnuAddQuery.Text = "新建查票窗口(&N)";
			// 
			// mnuRemoveQuery
			// 
			this.mnuRemoveQuery.Image = global::TOBA.Properties.Resources.block_16;
			this.mnuRemoveQuery.Name = "mnuRemoveQuery";
			this.mnuRemoveQuery.Size = new System.Drawing.Size(176, 22);
			this.mnuRemoveQuery.Text = "关闭查票窗口(&C)";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(173, 6);
			// 
			// mnuChangeName
			// 
			this.mnuChangeName.Image = global::TOBA.Properties.Resources.pencil_16;
			this.mnuChangeName.Name = "mnuChangeName";
			this.mnuChangeName.Size = new System.Drawing.Size(176, 22);
			this.mnuChangeName.Text = "修改查询名称(&R)...";
			// 
			// mnuSaveAsTheme
			// 
			this.mnuSaveAsTheme.Image = global::TOBA.Properties.Resources.save_16;
			this.mnuSaveAsTheme.Name = "mnuSaveAsTheme";
			this.mnuSaveAsTheme.Size = new System.Drawing.Size(176, 22);
			this.mnuSaveAsTheme.Text = "另存为出行模式(&A)";
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
			this.st.Controls.Add(this.superTabControlPanel5);
			this.st.Controls.Add(this.superTabControlPanel4);
			this.st.Controls.Add(this.superTabControlPanel1);
			this.st.Controls.Add(this.superTabControlPanel2);
			this.st.Controls.Add(this.superTabControlPanel3);
			this.st.Dock = System.Windows.Forms.DockStyle.Fill;
			this.st.Location = new System.Drawing.Point(3, 3);
			this.st.Name = "st";
			this.st.ReorderTabsEnabled = true;
			this.st.SelectedTabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
			this.st.SelectedTabIndex = 3;
			this.st.Size = new System.Drawing.Size(1081, 608);
			this.st.TabFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.st.TabIndex = 4;
			this.st.TabLayoutType = DevComponents.DotNetBar.eSuperTabLayoutType.MultiLine;
			this.st.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.stOperation,
            this.stQueryManage,
            this.stOrderManage,
            this.stBackupOrder,
            this.stPasManage});
			this.st.TabStyle = DevComponents.DotNetBar.eSuperTabStyle.Office2010BackstageBlue;
			this.st.Text = "superTabControl1";
			// 
			// superTabControlPanel5
			// 
			this.superTabControlPanel5.Controls.Add(this.backupOrderContainer);
			this.superTabControlPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel5.Location = new System.Drawing.Point(0, 30);
			this.superTabControlPanel5.Name = "superTabControlPanel5";
			this.superTabControlPanel5.Size = new System.Drawing.Size(1081, 578);
			this.superTabControlPanel5.TabIndex = 0;
			this.superTabControlPanel5.TabItem = this.stBackupOrder;
			// 
			// backupOrderContainer
			// 
			this.backupOrderContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.backupOrderContainer.Image = null;
			this.backupOrderContainer.Location = new System.Drawing.Point(0, 0);
			this.backupOrderContainer.Name = "backupOrderContainer";
			this.backupOrderContainer.Size = new System.Drawing.Size(1081, 578);
			this.backupOrderContainer.TabIndex = 0;
			// 
			// stBackupOrder
			// 
			this.stBackupOrder.AttachedControl = this.superTabControlPanel5;
			this.stBackupOrder.CloseButtonVisible = false;
			this.stBackupOrder.GlobalItem = false;
			this.stBackupOrder.Image = global::TOBA.Properties.Resources.Backup_Green_Button_16;
			this.stBackupOrder.Name = "stBackupOrder";
			this.stBackupOrder.Text = "候补订单";
			// 
			// superTabControlPanel1
			// 
			this.superTabControlPanel1.Controls.Add(this.userTabOperation1);
			this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel1.Location = new System.Drawing.Point(0, 30);
			this.superTabControlPanel1.Name = "superTabControlPanel1";
			this.superTabControlPanel1.Size = new System.Drawing.Size(1081, 578);
			this.superTabControlPanel1.TabIndex = 1;
			this.superTabControlPanel1.TabItem = this.stOperation;
			this.superTabControlPanel1.Visible = false;
			// 
			// userTabOperation1
			// 
			this.userTabOperation1.BackColor = System.Drawing.Color.White;
			this.userTabOperation1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.userTabOperation1.Image = null;
			this.userTabOperation1.Location = new System.Drawing.Point(0, 0);
			this.userTabOperation1.Name = "userTabOperation1";
			this.userTabOperation1.Size = new System.Drawing.Size(1081, 578);
			this.userTabOperation1.TabIndex = 1;
			// 
			// stOperation
			// 
			this.stOperation.AttachedControl = this.superTabControlPanel1;
			this.stOperation.BeginGroup = true;
			this.stOperation.CloseButtonVisible = false;
			this.stOperation.GlobalItem = false;
			this.stOperation.Image = global::TOBA.Properties.Resources.monitor_16;
			this.stOperation.Name = "stOperation";
			this.stOperation.Text = "操作中心";
			// 
			// superTabControlPanel2
			// 
			this.superTabControlPanel2.Controls.Add(this.advancedToolPanel1);
			this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel2.Location = new System.Drawing.Point(0, 30);
			this.superTabControlPanel2.Name = "superTabControlPanel2";
			this.superTabControlPanel2.Size = new System.Drawing.Size(1081, 578);
			this.superTabControlPanel2.TabIndex = 0;
			this.superTabControlPanel2.TabItem = this.stQueryManage;
			// 
			// advancedToolPanel1
			// 
			this.advancedToolPanel1.BackColor = System.Drawing.SystemColors.Window;
			this.advancedToolPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.advancedToolPanel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.advancedToolPanel1.Image = null;
			this.advancedToolPanel1.Location = new System.Drawing.Point(0, 0);
			this.advancedToolPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.advancedToolPanel1.Name = "advancedToolPanel1";
			this.advancedToolPanel1.Size = new System.Drawing.Size(1081, 578);
			this.advancedToolPanel1.TabIndex = 1;
			// 
			// stQueryManage
			// 
			this.stQueryManage.AttachedControl = this.superTabControlPanel2;
			this.stQueryManage.CloseButtonVisible = false;
			this.stQueryManage.GlobalItem = false;
			this.stQueryManage.Image = global::TOBA.Properties.Resources.cou_16_chart_pie;
			this.stQueryManage.Name = "stQueryManage";
			this.stQueryManage.Text = "查询管理";
			// 
			// superTabControlPanel4
			// 
			this.superTabControlPanel4.Controls.Add(this.passengerPanel1);
			this.superTabControlPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel4.Location = new System.Drawing.Point(0, 30);
			this.superTabControlPanel4.Name = "superTabControlPanel4";
			this.superTabControlPanel4.Size = new System.Drawing.Size(1081, 578);
			this.superTabControlPanel4.TabIndex = 0;
			this.superTabControlPanel4.TabItem = this.stPasManage;
			// 
			// passengerPanel1
			// 
			this.passengerPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.passengerPanel1.Image = null;
			this.passengerPanel1.Location = new System.Drawing.Point(0, 0);
			this.passengerPanel1.Name = "passengerPanel1";
			this.passengerPanel1.Size = new System.Drawing.Size(1081, 578);
			this.passengerPanel1.TabIndex = 1;
			// 
			// stPasManage
			// 
			this.stPasManage.AttachedControl = this.superTabControlPanel4;
			this.stPasManage.CloseButtonVisible = false;
			this.stPasManage.GlobalItem = false;
			this.stPasManage.Image = global::TOBA.Properties.Resources.briefcase_16;
			this.stPasManage.Name = "stPasManage";
			this.stPasManage.Text = "联系人管理";
			// 
			// superTabControlPanel3
			// 
			this.superTabControlPanel3.Controls.Add(this.orderPanel1);
			this.superTabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superTabControlPanel3.Location = new System.Drawing.Point(0, 30);
			this.superTabControlPanel3.Name = "superTabControlPanel3";
			this.superTabControlPanel3.Size = new System.Drawing.Size(1081, 578);
			this.superTabControlPanel3.TabIndex = 0;
			this.superTabControlPanel3.TabItem = this.stOrderManage;
			// 
			// orderPanel1
			// 
			this.orderPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.orderPanel1.Image = null;
			this.orderPanel1.Location = new System.Drawing.Point(0, 0);
			this.orderPanel1.Name = "orderPanel1";
			this.orderPanel1.Size = new System.Drawing.Size(1081, 578);
			this.orderPanel1.TabIndex = 1;
			// 
			// stOrderManage
			// 
			this.stOrderManage.AttachedControl = this.superTabControlPanel3;
			this.stOrderManage.CloseButtonVisible = false;
			this.stOrderManage.GlobalItem = false;
			this.stOrderManage.Image = global::TOBA.Properties.Resources.address_16;
			this.stOrderManage.Name = "stOrderManage";
			this.stOrderManage.Text = "订单管理";
			// 
			// UserMainPageContent
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.st);
			this.Name = "UserMainPageContent";
			this.Padding = new System.Windows.Forms.Padding(3);
			this.Size = new System.Drawing.Size(1087, 614);
			this.ctxTab.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.st)).EndInit();
			this.st.ResumeLayout(false);
			this.superTabControlPanel5.ResumeLayout(false);
			this.superTabControlPanel1.ResumeLayout(false);
			this.superTabControlPanel2.ResumeLayout(false);
			this.superTabControlPanel4.ResumeLayout(false);
			this.superTabControlPanel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ContextMenuStrip ctxTab;
		private System.Windows.Forms.ToolStripMenuItem mnuAddQuery;
		private System.Windows.Forms.ToolStripMenuItem mnuRemoveQuery;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem mnuChangeName;
		private System.Windows.Forms.ToolStripMenuItem mnuSaveAsTheme;
		private DevComponents.DotNetBar.SuperTabControl st;
		private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
		private DevComponents.DotNetBar.SuperTabItem stOperation;
		private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel4;
		private DevComponents.DotNetBar.SuperTabItem stPasManage;
		private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel3;
		private DevComponents.DotNetBar.SuperTabItem stOrderManage;
		private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
		private DevComponents.DotNetBar.SuperTabItem stQueryManage;
		private Passenger.PassengerPanel passengerPanel1;
		private Order.OrderPanel orderPanel1;
		private QueryManager.QmPanel advancedToolPanel1;
		private UserTabOperation userTabOperation1;
		private System.Windows.Forms.ToolStripMenuItem tsSendTo;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem tsNoOtherAccount;
		private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel5;
		private DevComponents.DotNetBar.SuperTabItem stBackupOrder;
		private BackupOrder.BackupOrderContainer backupOrderContainer;
	}
}
