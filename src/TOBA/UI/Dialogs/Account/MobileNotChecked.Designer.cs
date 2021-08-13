namespace TOBA.UI.Dialogs.Account
{
	partial class MobileNotChecked
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
			this.chkEnableNotify = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblTip = new DevComponents.DotNetBar.LabelX();
			this.btnYes = new DevComponents.DotNetBar.ButtonX();
			this.btnNo = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// chkEnableNotify
			// 
			this.chkEnableNotify.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			// 
			// 
			// 
			this.chkEnableNotify.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.chkEnableNotify.FocusCuesEnabled = false;
			this.chkEnableNotify.Location = new System.Drawing.Point(12, 103);
			this.chkEnableNotify.Name = "chkEnableNotify";
			this.chkEnableNotify.Size = new System.Drawing.Size(125, 21);
			this.chkEnableNotify.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.chkEnableNotify.TabIndex = 0;
			this.chkEnableNotify.Text = "下次继续提醒我";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TOBA.Properties.Resources.warning;
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// lblTip
			// 
			this.lblTip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.lblTip.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblTip.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblTip.Location = new System.Drawing.Point(66, 12);
			this.lblTip.Name = "lblTip";
			this.lblTip.Size = new System.Drawing.Size(381, 80);
			this.lblTip.TabIndex = 2;
			this.lblTip.TextLineAlignment = System.Drawing.StringAlignment.Near;
			this.lblTip.WordWrap = true;
			// 
			// btnYes
			// 
			this.btnYes.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnYes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnYes.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.btnYes.Image = global::TOBA.Properties.Resources.tick_16;
			this.btnYes.Location = new System.Drawing.Point(273, 98);
			this.btnYes.Name = "btnYes";
			this.btnYes.Size = new System.Drawing.Size(84, 26);
			this.btnYes.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnYes.TabIndex = 3;
			this.btnYes.Text = "是";
			// 
			// btnNo
			// 
			this.btnNo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnNo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnNo.DialogResult = System.Windows.Forms.DialogResult.No;
			this.btnNo.Image = global::TOBA.Properties.Resources.delete_16;
			this.btnNo.Location = new System.Drawing.Point(363, 98);
			this.btnNo.Name = "btnNo";
			this.btnNo.Size = new System.Drawing.Size(84, 26);
			this.btnNo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnNo.TabIndex = 3;
			this.btnNo.Text = "否";
			// 
			// MobileNotChecked
			// 
			this.AcceptButton = this.btnYes;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.btnNo;
			this.ClientSize = new System.Drawing.Size(459, 136);
			this.Controls.Add(this.btnNo);
			this.Controls.Add(this.btnYes);
			this.Controls.Add(this.lblTip);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.chkEnableNotify);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MobileNotChecked";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "手机号码未通过核验";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevComponents.DotNetBar.Controls.CheckBoxX chkEnableNotify;
		private System.Windows.Forms.PictureBox pictureBox1;
		private DevComponents.DotNetBar.LabelX lblTip;
		private DevComponents.DotNetBar.ButtonX btnYes;
		private DevComponents.DotNetBar.ButtonX btnNo;
	}
}