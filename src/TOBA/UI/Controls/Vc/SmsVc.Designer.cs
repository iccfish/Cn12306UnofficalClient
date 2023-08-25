namespace TOBA.UI.Controls.Vc
{
	partial class SmsVc
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
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnGetCode = new System.Windows.Forms.Button();
			this.txtCode = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.txtAppendix = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.panel3);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(386, 129);
			this.panel1.TabIndex = 0;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.btnGetCode);
			this.panel3.Controls.Add(this.txtCode);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel3.Location = new System.Drawing.Point(0, 75);
			this.panel3.Margin = new System.Windows.Forms.Padding(2);
			this.panel3.Name = "panel3";
			this.panel3.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
			this.panel3.Size = new System.Drawing.Size(386, 35);
			this.panel3.TabIndex = 2;
			// 
			// btnGetCode
			// 
			this.btnGetCode.Dock = System.Windows.Forms.DockStyle.Left;
			this.btnGetCode.Image = global::TOBA.Properties.Resources.bubble_16;
			this.btnGetCode.Location = new System.Drawing.Point(281, 7);
			this.btnGetCode.Margin = new System.Windows.Forms.Padding(2);
			this.btnGetCode.Name = "btnGetCode";
			this.btnGetCode.Size = new System.Drawing.Size(89, 28);
			this.btnGetCode.TabIndex = 2;
			this.btnGetCode.Text = "发送";
			this.btnGetCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnGetCode.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnGetCode.UseVisualStyleBackColor = true;
			// 
			// txtCode
			// 
			this.txtCode.Dock = System.Windows.Forms.DockStyle.Left;
			this.txtCode.Location = new System.Drawing.Point(110, 7);
			this.txtCode.Margin = new System.Windows.Forms.Padding(2);
			this.txtCode.Name = "txtCode";
			this.txtCode.Size = new System.Drawing.Size(171, 29);
			this.txtCode.TabIndex = 1;
			this.txtCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Left;
			this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(0, 7);
			this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(110, 28);
			this.label3.TabIndex = 0;
			this.label3.Text = "六位验证码";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.txtAppendix);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel2.Location = new System.Drawing.Point(0, 40);
			this.panel2.Margin = new System.Windows.Forms.Padding(2);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(0, 7, 0, 0);
			this.panel2.Size = new System.Drawing.Size(386, 35);
			this.panel2.TabIndex = 1;
			// 
			// txtAppendix
			// 
			this.txtAppendix.Dock = System.Windows.Forms.DockStyle.Left;
			this.txtAppendix.Location = new System.Drawing.Point(110, 7);
			this.txtAppendix.Margin = new System.Windows.Forms.Padding(2);
			this.txtAppendix.Name = "txtAppendix";
			this.txtAppendix.Size = new System.Drawing.Size(257, 29);
			this.txtAppendix.TabIndex = 1;
			this.txtAppendix.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Left;
			this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(0, 7);
			this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(110, 28);
			this.label2.TabIndex = 0;
			this.label2.Text = "证件号后四位";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.ForeColor = System.Drawing.Color.Maroon;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(386, 40);
			this.label1.TabIndex = 0;
			this.label1.Text = "请输入登录账号绑定的证件号后四位来获取验证码";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// SmsVc
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Margin = new System.Windows.Forms.Padding(2);
			this.Name = "SmsVc";
			this.Size = new System.Drawing.Size(386, 129);
			this.panel1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtAppendix;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button btnGetCode;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.Label label3;
	}
}
