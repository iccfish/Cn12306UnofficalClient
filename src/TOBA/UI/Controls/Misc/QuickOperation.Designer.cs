namespace TOBA.UI.Controls.Misc
{
	partial class QuickOperation
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnLogin = new System.Windows.Forms.Button();
			this.ctxLogin = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsLoginNone = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsNewLogin = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.ctxLogin.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.ForeColor = System.Drawing.SystemColors.Window;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(353, 28);
			this.panel1.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Image = global::TOBA.Properties.Resources.briefcase_16;
			this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label1.Location = new System.Drawing.Point(12, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(80, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "　　快速操作";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.Window;
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.btnLogin);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 28);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(353, 122);
			this.panel2.TabIndex = 7;
			// 
			// btnLogin
			// 
			this.btnLogin.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnLogin.Image = global::TOBA.Properties.Resources.cou_32_promotion;
			this.btnLogin.Location = new System.Drawing.Point(32, 34);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.Size = new System.Drawing.Size(280, 49);
			this.btnLogin.TabIndex = 0;
			this.btnLogin.Text = "登录12306账号订票";
			this.btnLogin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnLogin.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnLogin.UseVisualStyleBackColor = true;
			this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
			// 
			// ctxLogin
			// 
			this.ctxLogin.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsLoginNone,
            this.toolStripMenuItem1,
            this.tsNewLogin});
			this.ctxLogin.Name = "ctxLogin";
			this.ctxLogin.Size = new System.Drawing.Size(206, 76);
			// 
			// tsLoginNone
			// 
			this.tsLoginNone.Enabled = false;
			this.tsLoginNone.Name = "tsLoginNone";
			this.tsLoginNone.Size = new System.Drawing.Size(205, 22);
			this.tsLoginNone.Text = "没有已记住密码的账号...";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(202, 6);
			// 
			// tsNewLogin
			// 
			this.tsNewLogin.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tsNewLogin.Image = global::TOBA.Properties.Resources.user_16;
			this.tsNewLogin.Name = "tsNewLogin";
			this.tsNewLogin.Size = new System.Drawing.Size(205, 22);
			this.tsNewLogin.Text = "使用新用户名登录...(&N)";
			this.tsNewLogin.Click += new System.EventHandler(this.tsNewLogin_Click);
			// 
			// QuickOperation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "QuickOperation";
			this.Size = new System.Drawing.Size(353, 150);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.ctxLogin.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnLogin;
		private System.Windows.Forms.ContextMenuStrip ctxLogin;
		private System.Windows.Forms.ToolStripMenuItem tsLoginNone;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsNewLogin;
	}
}
