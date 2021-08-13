namespace TOBA.UI.Dialogs.OsSupport
{
	partial class SysErrorFound
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnAutoFix = new DevComponents.DotNetBar.ButtonX();
			this.label1 = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnAutoFix);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(575, 276);
			this.panel1.TabIndex = 0;
			// 
			// btnAutoFix
			// 
			this.btnAutoFix.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnAutoFix.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnAutoFix.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnAutoFix.Image = global::TOBA.Properties.Resources.UAC_32;
			this.btnAutoFix.Location = new System.Drawing.Point(140, 191);
			this.btnAutoFix.Name = "btnAutoFix";
			this.btnAutoFix.Size = new System.Drawing.Size(271, 64);
			this.btnAutoFix.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnAutoFix.TabIndex = 2;
			this.btnAutoFix.Text = "自动修复";
			this.btnAutoFix.Click += new System.EventHandler(this.btnAutoFix_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.ForeColor = System.Drawing.Color.Crimson;
			this.label1.Location = new System.Drawing.Point(88, 26);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(440, 137);
			this.label1.TabIndex = 1;
			this.label1.Text = "订票助手检测到您系统中存在一些非系统默认值的设置，可能会导致订票助手无法正确运行。\r\n\r\n订票助手能尝试自动修复他们。修复需要管理员权限，如果出现运行风险提醒，请" +
    "允许。";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TOBA.Properties.Resources.onebit_47;
			this.pictureBox1.Location = new System.Drawing.Point(34, 26);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// SysErrorFound
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(575, 276);
			this.Controls.Add(this.panel1);
			this.DoubleBuffered = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SysErrorFound";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "发现系统设置问题";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private DevComponents.DotNetBar.ButtonX btnAutoFix;
		private System.Windows.Forms.Label label1;
	}
}