namespace TOBA.UI.Dialogs.Common
{
	partial class YetAnotherWaitingDialog
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
			this.panel2 = new System.Windows.Forms.Panel();
			this.lblTip = new System.Windows.Forms.Label();
			this.pbIcon = new System.Windows.Forms.PictureBox();
			this.lblProgress = new System.Windows.Forms.Label();
			this.progress = new System.Windows.Forms.ProgressBar();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.lblProgress);
			this.panel1.Controls.Add(this.progress);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(530, 65);
			this.panel1.TabIndex = 0;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.lblTip);
			this.panel2.Controls.Add(this.pbIcon);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new System.Windows.Forms.Padding(5);
			this.panel2.Size = new System.Drawing.Size(528, 40);
			this.panel2.TabIndex = 7;
			// 
			// lblTip
			// 
			this.lblTip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTip.Location = new System.Drawing.Point(31, 8);
			this.lblTip.Name = "lblTip";
			this.lblTip.Size = new System.Drawing.Size(492, 27);
			this.lblTip.TabIndex = 3;
			this.lblTip.Text = "正在操作中，请稍等....";
			// 
			// pbIcon
			// 
			this.pbIcon.Image = global::TOBA.Properties.Resources._16px_loading_1;
			this.pbIcon.Location = new System.Drawing.Point(11, 11);
			this.pbIcon.Name = "pbIcon";
			this.pbIcon.Size = new System.Drawing.Size(16, 16);
			this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbIcon.TabIndex = 2;
			this.pbIcon.TabStop = false;
			// 
			// lblProgress
			// 
			this.lblProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.lblProgress.Location = new System.Drawing.Point(0, 40);
			this.lblProgress.Name = "lblProgress";
			this.lblProgress.Padding = new System.Windows.Forms.Padding(0, 0, 5, 0);
			this.lblProgress.Size = new System.Drawing.Size(528, 12);
			this.lblProgress.TabIndex = 6;
			this.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblProgress.Visible = false;
			// 
			// progress
			// 
			this.progress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.progress.Location = new System.Drawing.Point(0, 52);
			this.progress.Name = "progress";
			this.progress.Size = new System.Drawing.Size(528, 11);
			this.progress.TabIndex = 5;
			this.progress.Visible = false;
			// 
			// YetAnotherWaitingDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.ClientSize = new System.Drawing.Size(530, 65);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "YetAnotherWaitingDialog";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "YetAnotherWaitingDialog";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label lblTip;
		private System.Windows.Forms.PictureBox pbIcon;
		private System.Windows.Forms.ProgressBar progress;
		private System.Windows.Forms.Label lblProgress;
	}
}