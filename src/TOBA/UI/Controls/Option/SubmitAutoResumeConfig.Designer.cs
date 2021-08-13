namespace TOBA.UI.Controls.Option
{
	using Common;

	partial class SubmitAutoResumeConfig
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
			this.chkAutoClose = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pOptions = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.chkTimeout = new System.Windows.Forms.CheckBox();
			this.chkCloseSubmitFailed = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.chkCloseNoTicket = new System.Windows.Forms.CheckBox();
			this.nudCloseTimeout = new IntNumbericUpDown();
			this.chkCloseQueueElse = new System.Windows.Forms.CheckBox();
			this.chkCloseInitFailed = new System.Windows.Forms.CheckBox();
			this.chkCloseVcFailed = new System.Windows.Forms.CheckBox();
			this.tipBarMessage1 = new TipBarMessage();
			this.panel1.SuspendLayout();
			this.pOptions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudCloseTimeout)).BeginInit();
			this.SuspendLayout();
			// 
			// chkAutoClose
			// 
			this.chkAutoClose.AutoSize = true;
			this.chkAutoClose.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkAutoClose.Location = new System.Drawing.Point(28, 16);
			this.chkAutoClose.Name = "chkAutoClose";
			this.chkAutoClose.Size = new System.Drawing.Size(423, 21);
			this.chkAutoClose.TabIndex = 11;
			this.chkAutoClose.Text = "除非订票成功，否则出现下列情况时关闭所有订票提交窗口后自动重新刷票";
			this.chkAutoClose.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.pOptions);
			this.panel1.Controls.Add(this.tipBarMessage1);
			this.panel1.Controls.Add(this.chkAutoClose);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(530, 360);
			this.panel1.TabIndex = 12;
			// 
			// pOptions
			// 
			this.pOptions.Controls.Add(this.label2);
			this.pOptions.Controls.Add(this.chkTimeout);
			this.pOptions.Controls.Add(this.chkCloseSubmitFailed);
			this.pOptions.Controls.Add(this.label1);
			this.pOptions.Controls.Add(this.chkCloseNoTicket);
			this.pOptions.Controls.Add(this.nudCloseTimeout);
			this.pOptions.Controls.Add(this.chkCloseQueueElse);
			this.pOptions.Controls.Add(this.chkCloseInitFailed);
			this.pOptions.Controls.Add(this.chkCloseVcFailed);
			this.pOptions.Location = new System.Drawing.Point(30, 43);
			this.pOptions.Name = "pOptions";
			this.pOptions.Size = new System.Drawing.Size(500, 281);
			this.pOptions.TabIndex = 15;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
			this.label2.Location = new System.Drawing.Point(9, 177);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(414, 102);
			this.label2.TabIndex = 14;
			this.label2.Text = "提交自动管理仅针对下列情况有效：\r\n\r\n1.提交窗口是自动刷新出来的（手动提交的不操作）\r\n2.查询已开启刷票模式\r\n3.出现的错误可通过重试解决（如账户本身问题" +
    "等不可订票的情况不会处理）\r\n4.捡漏情况下建议关闭『查询设置』中的『自动停止其它查询』选项";
			// 
			// chkTimeout
			// 
			this.chkTimeout.AutoSize = true;
			this.chkTimeout.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkTimeout.Location = new System.Drawing.Point(12, 14);
			this.chkTimeout.Name = "chkTimeout";
			this.chkTimeout.Size = new System.Drawing.Size(291, 21);
			this.chkTimeout.TabIndex = 11;
			this.chkTimeout.Text = "订单提交页如果没有操作，指定时间后自动取消：";
			this.chkTimeout.UseVisualStyleBackColor = true;
			// 
			// chkCloseSubmitFailed
			// 
			this.chkCloseSubmitFailed.AutoSize = true;
			this.chkCloseSubmitFailed.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkCloseSubmitFailed.Location = new System.Drawing.Point(12, 41);
			this.chkCloseSubmitFailed.Name = "chkCloseSubmitFailed";
			this.chkCloseSubmitFailed.Size = new System.Drawing.Size(445, 21);
			this.chkCloseSubmitFailed.TabIndex = 11;
			this.chkCloseSubmitFailed.Text = "如果订单提交失败(余票不足/排队人数过多/网络错误)，则自动关闭提交对话框";
			this.chkCloseSubmitFailed.UseVisualStyleBackColor = true;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(385, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 17);
			this.label1.TabIndex = 13;
			this.label1.Text = "分钟";
			// 
			// chkCloseNoTicket
			// 
			this.chkCloseNoTicket.AutoSize = true;
			this.chkCloseNoTicket.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkCloseNoTicket.Location = new System.Drawing.Point(12, 68);
			this.chkCloseNoTicket.Name = "chkCloseNoTicket";
			this.chkCloseNoTicket.Size = new System.Drawing.Size(429, 21);
			this.chkCloseNoTicket.TabIndex = 11;
			this.chkCloseNoTicket.Text = "如果订单排队失败（没有足够的票/排队人数过多），则自动关闭提交对话框";
			this.chkCloseNoTicket.UseVisualStyleBackColor = true;
			// 
			// nudCloseTimeout
			// 
			this.nudCloseTimeout.IntValue = 0;
			this.nudCloseTimeout.Location = new System.Drawing.Point(309, 12);
			this.nudCloseTimeout.Name = "nudCloseTimeout";
			this.nudCloseTimeout.Size = new System.Drawing.Size(70, 23);
			this.nudCloseTimeout.TabIndex = 12;
			// 
			// chkCloseQueueElse
			// 
			this.chkCloseQueueElse.AutoSize = true;
			this.chkCloseQueueElse.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkCloseQueueElse.Location = new System.Drawing.Point(12, 95);
			this.chkCloseQueueElse.Name = "chkCloseQueueElse";
			this.chkCloseQueueElse.Size = new System.Drawing.Size(327, 21);
			this.chkCloseQueueElse.TabIndex = 11;
			this.chkCloseQueueElse.Text = "如果订单排队失败（其它原因），则自动关闭提交对话框";
			this.chkCloseQueueElse.UseVisualStyleBackColor = true;
			// 
			// chkCloseInitFailed
			// 
			this.chkCloseInitFailed.AutoSize = true;
			this.chkCloseInitFailed.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkCloseInitFailed.Location = new System.Drawing.Point(12, 149);
			this.chkCloseInitFailed.Name = "chkCloseInitFailed";
			this.chkCloseInitFailed.Size = new System.Drawing.Size(481, 21);
			this.chkCloseInitFailed.TabIndex = 11;
			this.chkCloseInitFailed.Text = "如果无法初始化提交订单(排队人数过多/网络错误/余票过期)，则自动关闭提交对话框";
			this.chkCloseInitFailed.UseVisualStyleBackColor = true;
			// 
			// chkCloseVcFailed
			// 
			this.chkCloseVcFailed.AutoSize = true;
			this.chkCloseVcFailed.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkCloseVcFailed.Location = new System.Drawing.Point(12, 122);
			this.chkCloseVcFailed.Name = "chkCloseVcFailed";
			this.chkCloseVcFailed.Size = new System.Drawing.Size(339, 21);
			this.chkCloseVcFailed.TabIndex = 11;
			this.chkCloseVcFailed.Text = "如果验证码没有能被远程打码识别，则自动关闭提交对话框";
			this.chkCloseVcFailed.UseVisualStyleBackColor = true;
			// 
			// tipBarMessage1
			// 
			this.tipBarMessage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(253)))), ((int)(((byte)(219)))));
			this.tipBarMessage1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(205)))), ((int)(((byte)(60)))));
			this.tipBarMessage1.BorderThickness = 1;
			this.tipBarMessage1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tipBarMessage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(149)))), ((int)(((byte)(43)))));
			this.tipBarMessage1.Image = global::TOBA.Properties.Resources.cou_16_process;
			this.tipBarMessage1.LabelMargin = new System.Windows.Forms.Padding(29, 7, -83, -3);
			this.tipBarMessage1.Location = new System.Drawing.Point(0, 331);
			this.tipBarMessage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tipBarMessage1.Name = "tipBarMessage1";
			this.tipBarMessage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tipBarMessage1.Size = new System.Drawing.Size(530, 29);
			this.tipBarMessage1.TabIndex = 14;
			this.tipBarMessage1.Text = "设置会影响订单提交，请确认您已知设置的用途。已启用远程打码时可能导致积分大量消耗";
			// 
			// SubmitAutoResumeConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "SubmitAutoResumeConfig";
			this.Size = new System.Drawing.Size(530, 360);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.pOptions.ResumeLayout(false);
			this.pOptions.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudCloseTimeout)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.CheckBox chkAutoClose;
		private System.Windows.Forms.Panel panel1;
		private IntNumbericUpDown nudCloseTimeout;
		private System.Windows.Forms.CheckBox chkTimeout;
		private TipBarMessage tipBarMessage1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chkCloseVcFailed;
		private System.Windows.Forms.CheckBox chkCloseNoTicket;
		private System.Windows.Forms.CheckBox chkCloseSubmitFailed;
		private System.Windows.Forms.CheckBox chkCloseInitFailed;
		private System.Windows.Forms.Panel pOptions;
		private System.Windows.Forms.CheckBox chkCloseQueueElse;
		private System.Windows.Forms.Label label2;
	}
}
