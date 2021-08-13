namespace TOBA.UI.Controls.Vc
{
	partial class SlideVcControl
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
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.ts = new System.Windows.Forms.ToolStripStatusLabel();
			this.viewArea = new System.Windows.Forms.Panel();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.BackColor = System.Drawing.Color.White;
			this.statusStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts});
			this.statusStrip1.Location = new System.Drawing.Point(0, 71);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 21, 0);
			this.statusStrip1.Size = new System.Drawing.Size(548, 31);
			this.statusStrip1.SizingGrip = false;
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// ts
			// 
			this.ts.Name = "ts";
			this.ts.Size = new System.Drawing.Size(136, 24);
			this.ts.Text = "请完成滑动验证";
			// 
			// viewArea
			// 
			this.viewArea.Dock = System.Windows.Forms.DockStyle.Fill;
			this.viewArea.Location = new System.Drawing.Point(0, 0);
			this.viewArea.Name = "viewArea";
			this.viewArea.Size = new System.Drawing.Size(548, 71);
			this.viewArea.TabIndex = 1;
			// 
			// SlideVcControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.viewArea);
			this.Controls.Add(this.statusStrip1);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "SlideVcControl";
			this.Size = new System.Drawing.Size(548, 102);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel ts;
		private System.Windows.Forms.Panel viewArea;
	}
}