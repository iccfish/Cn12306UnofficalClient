namespace TOBA.UI.Controls.Query
{
	partial class QueryConfiguration
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
			this.chkWaitToHourIfExist = new System.Windows.Forms.CheckBox();
			this.chkOClockRefresh = new System.Windows.Forms.CheckBox();
			this.chkIgnoreIllegal = new System.Windows.Forms.CheckBox();
			this.chkEnableSCSLoop = new System.Windows.Forms.CheckBox();
			this.chkAutoTrain = new System.Windows.Forms.CheckBox();
			this.chkStuAsCommon = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// chkWaitToHourIfExist
			// 
			this.chkWaitToHourIfExist.AutoSize = true;
			this.chkWaitToHourIfExist.Location = new System.Drawing.Point(15, 38);
			this.chkWaitToHourIfExist.Name = "chkWaitToHourIfExist";
			this.chkWaitToHourIfExist.Size = new System.Drawing.Size(348, 16);
			this.chkWaitToHourIfExist.TabIndex = 2;
			this.chkWaitToHourIfExist.Text = "如果自动预定的车次有尚未售票，那么等待到指定整点再刷新";
			this.chkWaitToHourIfExist.UseVisualStyleBackColor = true;
			// 
			// chkOClockRefresh
			// 
			this.chkOClockRefresh.AutoSize = true;
			this.chkOClockRefresh.Location = new System.Drawing.Point(15, 16);
			this.chkOClockRefresh.Name = "chkOClockRefresh";
			this.chkOClockRefresh.Size = new System.Drawing.Size(96, 16);
			this.chkOClockRefresh.TabIndex = 13;
			this.chkOClockRefresh.Text = "等待整点刷新";
			this.chkOClockRefresh.UseVisualStyleBackColor = true;
			// 
			// chkIgnoreIllegal
			// 
			this.chkIgnoreIllegal.AutoSize = true;
			this.chkIgnoreIllegal.Location = new System.Drawing.Point(15, 104);
			this.chkIgnoreIllegal.Name = "chkIgnoreIllegal";
			this.chkIgnoreIllegal.Size = new System.Drawing.Size(96, 16);
			this.chkIgnoreIllegal.TabIndex = 26;
			this.chkIgnoreIllegal.Text = "忽略错误数据";
			this.chkIgnoreIllegal.UseVisualStyleBackColor = true;
			// 
			// chkEnableSCSLoop
			// 
			this.chkEnableSCSLoop.AutoSize = true;
			this.chkEnableSCSLoop.Location = new System.Drawing.Point(15, 82);
			this.chkEnableSCSLoop.Name = "chkEnableSCSLoop";
			this.chkEnableSCSLoop.Size = new System.Drawing.Size(72, 16);
			this.chkEnableSCSLoop.TabIndex = 24;
			this.chkEnableSCSLoop.Text = "站点轮询";
			this.chkEnableSCSLoop.UseVisualStyleBackColor = true;
			// 
			// chkAutoTrain
			// 
			this.chkAutoTrain.AutoSize = true;
			this.chkAutoTrain.Location = new System.Drawing.Point(138, 16);
			this.chkAutoTrain.Name = "chkAutoTrain";
			this.chkAutoTrain.Size = new System.Drawing.Size(144, 16);
			this.chkAutoTrain.TabIndex = 27;
			this.chkAutoTrain.Text = "自动选择较优车次席别";
			this.chkAutoTrain.UseVisualStyleBackColor = true;
			// 
			// chkStuAsCommon
			// 
			this.chkStuAsCommon.AutoSize = true;
			this.chkStuAsCommon.Location = new System.Drawing.Point(16, 126);
			this.chkStuAsCommon.Name = "chkStuAsCommon";
			this.chkStuAsCommon.Size = new System.Drawing.Size(132, 16);
			this.chkStuAsCommon.TabIndex = 28;
			this.chkStuAsCommon.Text = "学生票提交为成人票";
			this.chkStuAsCommon.UseVisualStyleBackColor = true;
			// 
			// QueryConfiguration
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.chkStuAsCommon);
			this.Controls.Add(this.chkAutoTrain);
			this.Controls.Add(this.chkIgnoreIllegal);
			this.Controls.Add(this.chkEnableSCSLoop);
			this.Controls.Add(this.chkOClockRefresh);
			this.Controls.Add(this.chkWaitToHourIfExist);
			this.Name = "QueryConfiguration";
			this.Size = new System.Drawing.Size(366, 205);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckBox chkWaitToHourIfExist;
		private System.Windows.Forms.CheckBox chkOClockRefresh;
		private System.Windows.Forms.CheckBox chkIgnoreIllegal;
		private System.Windows.Forms.CheckBox chkEnableSCSLoop;
		private System.Windows.Forms.CheckBox chkAutoTrain;
		private System.Windows.Forms.CheckBox chkStuAsCommon;
	}
}
