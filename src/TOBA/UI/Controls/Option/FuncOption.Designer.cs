namespace TOBA.UI.Controls.Option
{
	using Common;

	partial class FuncOption
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
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.panel1 = new System.Windows.Forms.Panel();
			this.panel2 = new System.Windows.Forms.Panel();
			this.priceAutoQueryEnable = new System.Windows.Forms.CheckBox();
			this.hbAutoCheckEnable = new System.Windows.Forms.CheckBox();
			this.priceAutoQueryOption = new System.Windows.Forms.GroupBox();
			this.priceAutoQueryInterval = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.hbAutoCheckOption = new System.Windows.Forms.GroupBox();
			this.hbAutoCheckOptionInterval = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tipBarMessage1 = new TipBarMessage();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.priceAutoQueryOption.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.priceAutoQueryInterval)).BeginInit();
			this.hbAutoCheckOption.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.hbAutoCheckOptionInterval)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.tipBarMessage1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(535, 366);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.AutoScroll = true;
			this.panel2.Controls.Add(this.priceAutoQueryEnable);
			this.panel2.Controls.Add(this.hbAutoCheckEnable);
			this.panel2.Controls.Add(this.priceAutoQueryOption);
			this.panel2.Controls.Add(this.hbAutoCheckOption);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(535, 338);
			this.panel2.TabIndex = 5;
			// 
			// priceAutoQueryEnable
			// 
			this.priceAutoQueryEnable.AutoSize = true;
			this.priceAutoQueryEnable.Location = new System.Drawing.Point(30, 90);
			this.priceAutoQueryEnable.Name = "priceAutoQueryEnable";
			this.priceAutoQueryEnable.Size = new System.Drawing.Size(123, 21);
			this.priceAutoQueryEnable.TabIndex = 3;
			this.priceAutoQueryEnable.Text = "启用票价自动查询";
			this.priceAutoQueryEnable.UseVisualStyleBackColor = true;
			// 
			// hbAutoCheckEnable
			// 
			this.hbAutoCheckEnable.AutoSize = true;
			this.hbAutoCheckEnable.Location = new System.Drawing.Point(30, 18);
			this.hbAutoCheckEnable.Name = "hbAutoCheckEnable";
			this.hbAutoCheckEnable.Size = new System.Drawing.Size(255, 21);
			this.hbAutoCheckEnable.TabIndex = 1;
			this.hbAutoCheckEnable.Text = "如果车次有候补状态，则自动检测候补人数";
			this.hbAutoCheckEnable.UseVisualStyleBackColor = true;
			// 
			// priceAutoQueryOption
			// 
			this.priceAutoQueryOption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.priceAutoQueryOption.Controls.Add(this.priceAutoQueryInterval);
			this.priceAutoQueryOption.Controls.Add(this.label3);
			this.priceAutoQueryOption.Controls.Add(this.label4);
			this.priceAutoQueryOption.Location = new System.Drawing.Point(23, 92);
			this.priceAutoQueryOption.Name = "priceAutoQueryOption";
			this.priceAutoQueryOption.Size = new System.Drawing.Size(480, 66);
			this.priceAutoQueryOption.TabIndex = 4;
			this.priceAutoQueryOption.TabStop = false;
			// 
			// priceAutoQueryInterval
			// 
			this.priceAutoQueryInterval.Location = new System.Drawing.Point(92, 29);
			this.priceAutoQueryInterval.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
			this.priceAutoQueryInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.priceAutoQueryInterval.Name = "priceAutoQueryInterval";
			this.priceAutoQueryInterval.Size = new System.Drawing.Size(70, 23);
			this.priceAutoQueryInterval.TabIndex = 1;
			this.priceAutoQueryInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.priceAutoQueryInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(169, 31);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 17);
			this.label3.TabIndex = 0;
			this.label3.Text = "毫秒";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 31);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 17);
			this.label4.TabIndex = 0;
			this.label4.Text = "查询时间间隔";
			// 
			// hbAutoCheckOption
			// 
			this.hbAutoCheckOption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.hbAutoCheckOption.Controls.Add(this.hbAutoCheckOptionInterval);
			this.hbAutoCheckOption.Controls.Add(this.label2);
			this.hbAutoCheckOption.Controls.Add(this.label1);
			this.hbAutoCheckOption.Location = new System.Drawing.Point(23, 20);
			this.hbAutoCheckOption.Name = "hbAutoCheckOption";
			this.hbAutoCheckOption.Size = new System.Drawing.Size(480, 66);
			this.hbAutoCheckOption.TabIndex = 2;
			this.hbAutoCheckOption.TabStop = false;
			// 
			// hbAutoCheckOptionInterval
			// 
			this.hbAutoCheckOptionInterval.Location = new System.Drawing.Point(92, 29);
			this.hbAutoCheckOptionInterval.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
			this.hbAutoCheckOptionInterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.hbAutoCheckOptionInterval.Name = "hbAutoCheckOptionInterval";
			this.hbAutoCheckOptionInterval.Size = new System.Drawing.Size(70, 23);
			this.hbAutoCheckOptionInterval.TabIndex = 1;
			this.hbAutoCheckOptionInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.hbAutoCheckOptionInterval.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(169, 31);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 17);
			this.label2.TabIndex = 0;
			this.label2.Text = "毫秒";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 31);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "查询时间间隔";
			// 
			// tipBarMessage1
			// 
			this.tipBarMessage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(253)))), ((int)(((byte)(219)))));
			this.tipBarMessage1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(205)))), ((int)(((byte)(60)))));
			this.tipBarMessage1.BorderThickness = 1;
			this.tipBarMessage1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tipBarMessage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(126)))), ((int)(((byte)(149)))), ((int)(((byte)(43)))));
			this.tipBarMessage1.Image = global::TOBA.Properties.Resources.bubble_16;
			this.tipBarMessage1.LabelMargin = new System.Windows.Forms.Padding(29, 7, -84, -3);
			this.tipBarMessage1.Location = new System.Drawing.Point(0, 338);
			this.tipBarMessage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tipBarMessage1.Name = "tipBarMessage1";
			this.tipBarMessage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tipBarMessage1.Size = new System.Drawing.Size(535, 28);
			this.tipBarMessage1.TabIndex = 0;
			this.tipBarMessage1.Text = "开启功能较多可能会导致被封IP概率提升。在高峰期，适当禁用功能可提高刷票稳定性。";
			// 
			// FuncOption
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "FuncOption";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.priceAutoQueryOption.ResumeLayout(false);
			this.priceAutoQueryOption.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.priceAutoQueryInterval)).EndInit();
			this.hbAutoCheckOption.ResumeLayout(false);
			this.hbAutoCheckOption.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.hbAutoCheckOptionInterval)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private TipBarMessage tipBarMessage1;
		private System.Windows.Forms.CheckBox priceAutoQueryEnable;
		private System.Windows.Forms.CheckBox hbAutoCheckEnable;
		private System.Windows.Forms.GroupBox priceAutoQueryOption;
		private System.Windows.Forms.NumericUpDown priceAutoQueryInterval;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.GroupBox hbAutoCheckOption;
		private System.Windows.Forms.NumericUpDown hbAutoCheckOptionInterval;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
	}
}
