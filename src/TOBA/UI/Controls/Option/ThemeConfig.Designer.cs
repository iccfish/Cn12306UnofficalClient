namespace TOBA.UI.Controls.Option
{
	using Common;

	partial class ThemeConfig
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ThemeConfig));
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnResetColorHint = new DevComponents.DotNetBar.ButtonX();
			this.cbp = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.cb = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.cp = new DevComponents.DotNetBar.ColorPickerButton();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnResetColorHint);
			this.panel1.Controls.Add(this.cbp);
			this.panel1.Controls.Add(this.cb);
			this.panel1.Controls.Add(this.cp);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(530, 360);
			this.panel1.TabIndex = 5;
			// 
			// btnResetColorHint
			// 
			this.btnResetColorHint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnResetColorHint.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnResetColorHint.Location = new System.Drawing.Point(214, 46);
			this.btnResetColorHint.Name = "btnResetColorHint";
			this.btnResetColorHint.Size = new System.Drawing.Size(79, 23);
			this.btnResetColorHint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnResetColorHint.TabIndex = 11;
			this.btnResetColorHint.Text = "重置颜色";
			// 
			// cbp
			// 
			this.cbp.DisplayMember = "Text";
			this.cbp.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbp.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbp.FormattingEnabled = true;
			this.cbp.ItemHeight = 18;
			this.cbp.Location = new System.Drawing.Point(108, 78);
			this.cbp.Name = "cbp";
			this.cbp.Size = new System.Drawing.Size(185, 24);
			this.cbp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cbp.TabIndex = 9;
			// 
			// cb
			// 
			this.cb.DisplayMember = "Text";
			this.cb.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cb.FormattingEnabled = true;
			this.cb.ItemHeight = 18;
			this.cb.Location = new System.Drawing.Point(108, 17);
			this.cb.Name = "cb";
			this.cb.Size = new System.Drawing.Size(185, 24);
			this.cb.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cb.TabIndex = 10;
			// 
			// cp
			// 
			this.cp.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.cp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.cp.Image = ((System.Drawing.Image)(resources.GetObject("cp.Image")));
			this.cp.Location = new System.Drawing.Point(108, 46);
			this.cp.Name = "cp";
			this.cp.SelectedColorImageRectangle = new System.Drawing.Rectangle(2, 2, 12, 12);
			this.cp.Size = new System.Drawing.Size(100, 23);
			this.cp.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cp.TabIndex = 8;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(39, 81);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(56, 17);
			this.label3.TabIndex = 5;
			this.label3.Text = "标签位置";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(39, 52);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(56, 17);
			this.label2.TabIndex = 6;
			this.label2.Text = "色彩调整";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(39, 20);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(56, 17);
			this.label1.TabIndex = 7;
			this.label1.Text = "主题风格";
			// 
			// ThemeConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "ThemeConfig";
			this.Size = new System.Drawing.Size(530, 360);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private DevComponents.DotNetBar.ButtonX btnResetColorHint;
		private DevComponents.DotNetBar.Controls.ComboBoxEx cbp;
		private DevComponents.DotNetBar.Controls.ComboBoxEx cb;
		private DevComponents.DotNetBar.ColorPickerButton cp;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}
