namespace TOBA.UI.Controls.Option
{
	using Common;

	partial class SubmitOrderConfig
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
			this.panel2 = new System.Windows.Forms.Panel();
			this.chkTryStopNoTicket = new System.Windows.Forms.CheckBox();
			this.tipBarMessage1 = new TOBA.UI.Controls.Common.TipBarMessage();
			this.chkIgnoreSafeTime = new System.Windows.Forms.CheckBox();
			this.iiAutoRetryLimit = new DevComponents.Editors.IntegerInput();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label12 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.iDisableSubmitTicketRate = new TOBA.UI.Controls.Common.IntNumbericUpDown();
			this.iDisableSubmitTicketTimeRange = new TOBA.UI.Controls.Common.IntNumbericUpDown();
			this.chkDisableSubmitTicket = new System.Windows.Forms.CheckBox();
			this.wbs = new System.Windows.Forms.ComboBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.chkIgnoreQueueError = new System.Windows.Forms.CheckBox();
			this.chkEnableArchive = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.chkTryStopWz = new System.Windows.Forms.CheckBox();
			this.chkDisableChangeInfoForAutoAdd = new System.Windows.Forms.CheckBox();
			this.chkAutoCheckTicket = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.enableFastSubmitOrder = new System.Windows.Forms.CheckBox();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.iiAutoRetryLimit)).BeginInit();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.iDisableSubmitTicketRate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.iDisableSubmitTicketTimeRange)).BeginInit();
			this.SuspendLayout();
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.chkTryStopNoTicket);
			this.panel2.Controls.Add(this.tipBarMessage1);
			this.panel2.Controls.Add(this.chkIgnoreSafeTime);
			this.panel2.Controls.Add(this.iiAutoRetryLimit);
			this.panel2.Controls.Add(this.panel1);
			this.panel2.Controls.Add(this.chkDisableSubmitTicket);
			this.panel2.Controls.Add(this.wbs);
			this.panel2.Controls.Add(this.label14);
			this.panel2.Controls.Add(this.label13);
			this.panel2.Controls.Add(this.label3);
			this.panel2.Controls.Add(this.chkIgnoreQueueError);
			this.panel2.Controls.Add(this.chkEnableArchive);
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.label4);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.chkTryStopWz);
			this.panel2.Controls.Add(this.chkDisableChangeInfoForAutoAdd);
			this.panel2.Controls.Add(this.chkAutoCheckTicket);
			this.panel2.Controls.Add(this.label1);
			this.panel2.Controls.Add(this.label5);
			this.panel2.Controls.Add(this.label7);
			this.panel2.Controls.Add(this.enableFastSubmitOrder);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(530, 360);
			this.panel2.TabIndex = 19;
			// 
			// chkTryStopNoTicket
			// 
			this.chkTryStopNoTicket.AutoSize = true;
			this.chkTryStopNoTicket.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkTryStopNoTicket.Location = new System.Drawing.Point(217, 221);
			this.chkTryStopNoTicket.Name = "chkTryStopNoTicket";
			this.chkTryStopNoTicket.Size = new System.Drawing.Size(183, 21);
			this.chkTryStopNoTicket.TabIndex = 40;
			this.chkTryStopNoTicket.Text = "实时余票无票时，不提交订单";
			this.chkTryStopNoTicket.UseVisualStyleBackColor = true;
			// 
			// tipBarMessage1
			// 
			this.tipBarMessage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
			this.tipBarMessage1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.tipBarMessage1.BorderThickness = 1;
			this.tipBarMessage1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tipBarMessage1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tipBarMessage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.tipBarMessage1.Image = global::TOBA.Properties.Resources.warning_16;
			this.tipBarMessage1.Location = new System.Drawing.Point(0, 314);
			this.tipBarMessage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tipBarMessage1.Name = "tipBarMessage1";
			this.tipBarMessage1.Padding = new System.Windows.Forms.Padding(3);
			this.tipBarMessage1.Size = new System.Drawing.Size(530, 46);
			this.tipBarMessage1.TabIndex = 36;
			this.tipBarMessage1.Text = "启用“忽略排队人数错误”或“忽略安全期”可能会导致提交订单后出现异常错误（如退出登录）";
			// 
			// chkIgnoreSafeTime
			// 
			this.chkIgnoreSafeTime.AutoSize = true;
			this.chkIgnoreSafeTime.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkIgnoreSafeTime.ForeColor = System.Drawing.Color.Red;
			this.chkIgnoreSafeTime.Location = new System.Drawing.Point(16, 221);
			this.chkIgnoreSafeTime.Name = "chkIgnoreSafeTime";
			this.chkIgnoreSafeTime.Size = new System.Drawing.Size(135, 21);
			this.chkIgnoreSafeTime.TabIndex = 39;
			this.chkIgnoreSafeTime.Text = "忽略安全期（危险）";
			this.chkIgnoreSafeTime.UseVisualStyleBackColor = true;
			// 
			// iiAutoRetryLimit
			// 
			// 
			// 
			// 
			this.iiAutoRetryLimit.BackgroundStyle.Class = "DateTimeInputBackground";
			this.iiAutoRetryLimit.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.iiAutoRetryLimit.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
			this.iiAutoRetryLimit.Location = new System.Drawing.Point(126, 247);
			this.iiAutoRetryLimit.Name = "iiAutoRetryLimit";
			this.iiAutoRetryLimit.ShowUpDown = true;
			this.iiAutoRetryLimit.Size = new System.Drawing.Size(81, 23);
			this.iiAutoRetryLimit.TabIndex = 38;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.label12);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.label11);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.label8);
			this.panel1.Controls.Add(this.iDisableSubmitTicketRate);
			this.panel1.Controls.Add(this.iDisableSubmitTicketTimeRange);
			this.panel1.Location = new System.Drawing.Point(32, 166);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(474, 49);
			this.panel1.TabIndex = 37;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.ForeColor = System.Drawing.Color.Red;
			this.label12.Location = new System.Drawing.Point(264, 29);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(200, 17);
			this.label12.TabIndex = 3;
			this.label12.Text = "出票失败不代表没票，请谨慎使用。";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(91, 6);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(188, 17);
			this.label9.TabIndex = 1;
			this.label9.Text = "分钟的时间里同样的票数出现超过";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(13, 29);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(248, 17);
			this.label11.TabIndex = 1;
			this.label11.Text = "再查出相同的票数时直接忽略，不提交订单。";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(328, 7);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(140, 17);
			this.label10.TabIndex = 1;
			this.label10.Text = "次且提交报余票不足后，";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(13, 5);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(20, 17);
			this.label8.TabIndex = 1;
			this.label8.Text = "在";
			// 
			// iDisableSubmitTicketRate
			// 
			this.iDisableSubmitTicketRate.IntValue = 0;
			this.iDisableSubmitTicketRate.Location = new System.Drawing.Point(280, 2);
			this.iDisableSubmitTicketRate.Name = "iDisableSubmitTicketRate";
			this.iDisableSubmitTicketRate.Size = new System.Drawing.Size(46, 23);
			this.iDisableSubmitTicketRate.TabIndex = 2;
			this.iDisableSubmitTicketRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// iDisableSubmitTicketTimeRange
			// 
			this.iDisableSubmitTicketTimeRange.IntValue = 0;
			this.iDisableSubmitTicketTimeRange.Location = new System.Drawing.Point(36, 2);
			this.iDisableSubmitTicketTimeRange.Name = "iDisableSubmitTicketTimeRange";
			this.iDisableSubmitTicketTimeRange.Size = new System.Drawing.Size(51, 23);
			this.iDisableSubmitTicketTimeRange.TabIndex = 1;
			this.iDisableSubmitTicketTimeRange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// chkDisableSubmitTicket
			// 
			this.chkDisableSubmitTicket.AutoSize = true;
			this.chkDisableSubmitTicket.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkDisableSubmitTicket.Location = new System.Drawing.Point(16, 144);
			this.chkDisableSubmitTicket.Name = "chkDisableSubmitTicket";
			this.chkDisableSubmitTicket.Size = new System.Drawing.Size(435, 21);
			this.chkDisableSubmitTicket.TabIndex = 31;
			this.chkDisableSubmitTicket.Text = "如果车次有余票反复出现但却无法提交订单（余票不足），则自动忽略此票数";
			this.chkDisableSubmitTicket.UseVisualStyleBackColor = true;
			// 
			// wbs
			// 
			this.wbs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.wbs.FormattingEnabled = true;
			this.wbs.Location = new System.Drawing.Point(125, 279);
			this.wbs.Name = "wbs";
			this.wbs.Size = new System.Drawing.Size(160, 25);
			this.wbs.TabIndex = 30;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(214, 252);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(272, 17);
			this.label14.TabIndex = 28;
			this.label14.Text = "订单提交出现问题需要重试时，最多自动重试次数";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label13.Location = new System.Drawing.Point(14, 252);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(80, 17);
			this.label13.TabIndex = 29;
			this.label13.Text = "自动重试限制";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(13, 282);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(92, 17);
			this.label3.TabIndex = 27;
			this.label3.Text = "支付使用浏览器";
			// 
			// chkIgnoreQueueError
			// 
			this.chkIgnoreQueueError.AutoSize = true;
			this.chkIgnoreQueueError.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkIgnoreQueueError.ForeColor = System.Drawing.Color.Red;
			this.chkIgnoreQueueError.Location = new System.Drawing.Point(16, 103);
			this.chkIgnoreQueueError.Name = "chkIgnoreQueueError";
			this.chkIgnoreQueueError.Size = new System.Drawing.Size(123, 21);
			this.chkIgnoreQueueError.TabIndex = 25;
			this.chkIgnoreQueueError.Text = "忽略排队人数错误";
			this.chkIgnoreQueueError.UseVisualStyleBackColor = true;
			// 
			// chkEnableArchive
			// 
			this.chkEnableArchive.AutoSize = true;
			this.chkEnableArchive.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkEnableArchive.Location = new System.Drawing.Point(16, 122);
			this.chkEnableArchive.Name = "chkEnableArchive";
			this.chkEnableArchive.Size = new System.Drawing.Size(99, 21);
			this.chkEnableArchive.TabIndex = 26;
			this.chkEnableArchive.Text = "启用订单存档";
			this.chkEnableArchive.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(201, 76);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(317, 28);
			this.label6.TabIndex = 33;
			this.label6.Text = "仅当预设条件完整时可用，提交更快速，提交时不可修改乘车人信息。改签时不可用。";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(303, 282);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(164, 17);
			this.label4.TabIndex = 34;
			this.label4.Text = "选择支付订单时使用的浏览器";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(177, 123);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(344, 17);
			this.label2.TabIndex = 35;
			this.label2.Text = "启用后，订单助手将会为你保存所有已经查到过的历史订单记录";
			// 
			// chkTryStopWz
			// 
			this.chkTryStopWz.AutoSize = true;
			this.chkTryStopWz.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkTryStopWz.Location = new System.Drawing.Point(16, 53);
			this.chkTryStopWz.Name = "chkTryStopWz";
			this.chkTryStopWz.Size = new System.Drawing.Size(147, 21);
			this.chkTryStopWz.TabIndex = 21;
			this.chkTryStopWz.Text = "尽量避免提交到无座票";
			this.chkTryStopWz.UseVisualStyleBackColor = true;
			// 
			// chkDisableChangeInfoForAutoAdd
			// 
			this.chkDisableChangeInfoForAutoAdd.AutoSize = true;
			this.chkDisableChangeInfoForAutoAdd.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkDisableChangeInfoForAutoAdd.Location = new System.Drawing.Point(16, 31);
			this.chkDisableChangeInfoForAutoAdd.Name = "chkDisableChangeInfoForAutoAdd";
			this.chkDisableChangeInfoForAutoAdd.Size = new System.Drawing.Size(243, 21);
			this.chkDisableChangeInfoForAutoAdd.TabIndex = 20;
			this.chkDisableChangeInfoForAutoAdd.Text = "自动提交添加的联系人禁止编辑身份信息";
			this.chkDisableChangeInfoForAutoAdd.UseVisualStyleBackColor = true;
			// 
			// chkAutoCheckTicket
			// 
			this.chkAutoCheckTicket.AutoSize = true;
			this.chkAutoCheckTicket.Enabled = false;
			this.chkAutoCheckTicket.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkAutoCheckTicket.Location = new System.Drawing.Point(16, 9);
			this.chkAutoCheckTicket.Name = "chkAutoCheckTicket";
			this.chkAutoCheckTicket.Size = new System.Drawing.Size(159, 21);
			this.chkAutoCheckTicket.TabIndex = 19;
			this.chkAutoCheckTicket.Text = "提交订单时实时查询余票";
			this.chkAutoCheckTicket.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(201, 54);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(176, 17);
			this.label1.TabIndex = 24;
			this.label1.Text = "尽量尝试避免无座，不一定有效";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(201, 10);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(320, 17);
			this.label5.TabIndex = 23;
			this.label5.Text = "在提交订单时实时查询余票数。出莫名其妙问题时请取消。";
			// 
			// label7
			// 
			this.label7.ForeColor = System.Drawing.Color.Red;
			this.label7.Location = new System.Drawing.Point(201, 104);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(317, 16);
			this.label7.TabIndex = 32;
			this.label7.Text = "忽略排队人数错误，强制提交订单。";
			// 
			// enableFastSubmitOrder
			// 
			this.enableFastSubmitOrder.AutoSize = true;
			this.enableFastSubmitOrder.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.enableFastSubmitOrder.Location = new System.Drawing.Point(16, 75);
			this.enableFastSubmitOrder.Name = "enableFastSubmitOrder";
			this.enableFastSubmitOrder.Size = new System.Drawing.Size(147, 21);
			this.enableFastSubmitOrder.TabIndex = 22;
			this.enableFastSubmitOrder.Text = "启用订单快速提交模式";
			this.enableFastSubmitOrder.UseVisualStyleBackColor = true;
			// 
			// SubmitOrderConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel2);
			this.Name = "SubmitOrderConfig";
			this.Size = new System.Drawing.Size(530, 360);
			this.Load += new System.EventHandler(this.SubmitOrderConfig_Load);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.iiAutoRetryLimit)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.iDisableSubmitTicketRate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.iDisableSubmitTicketTimeRange)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel2;
		private TipBarMessage tipBarMessage1;
		private System.Windows.Forms.CheckBox chkIgnoreSafeTime;
		private DevComponents.Editors.IntegerInput iiAutoRetryLimit;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label8;
		private IntNumbericUpDown iDisableSubmitTicketRate;
		private IntNumbericUpDown iDisableSubmitTicketTimeRange;
		private System.Windows.Forms.CheckBox chkDisableSubmitTicket;
		private System.Windows.Forms.ComboBox wbs;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.CheckBox chkIgnoreQueueError;
		private System.Windows.Forms.CheckBox chkEnableArchive;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox chkTryStopWz;
		private System.Windows.Forms.CheckBox chkDisableChangeInfoForAutoAdd;
		private System.Windows.Forms.CheckBox chkAutoCheckTicket;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.CheckBox enableFastSubmitOrder;
		private System.Windows.Forms.CheckBox chkTryStopNoTicket;
	}
}
