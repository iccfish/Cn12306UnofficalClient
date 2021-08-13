namespace TOBA.UI.Dialogs.Notification
{
	partial class MsgFrom12306
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
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.btnOk = new DevComponents.DotNetBar.ButtonX();
			this.chkEnable = new DevComponents.DotNetBar.Controls.CheckBoxX();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX1.ForeColor = System.Drawing.Color.MediumVioletRed;
			this.labelX1.Location = new System.Drawing.Point(66, 12);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(487, 216);
			this.labelX1.TabIndex = 0;
			this.labelX1.TextLineAlignment = System.Drawing.StringAlignment.Near;
			this.labelX1.WordWrap = true;
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
			// btnOk
			// 
			this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnOk.Image = global::TOBA.Properties.Resources.cou_32_accept;
			this.btnOk.Location = new System.Drawing.Point(382, 252);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(171, 37);
			this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "确定(&O)";
			// 
			// chkEnable
			// 
			this.chkEnable.AutoSize = true;
			// 
			// 
			// 
			this.chkEnable.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.chkEnable.Location = new System.Drawing.Point(12, 271);
			this.chkEnable.Name = "chkEnable";
			this.chkEnable.Size = new System.Drawing.Size(206, 18);
			this.chkEnable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.chkEnable.TabIndex = 3;
			this.chkEnable.Text = "下次继续显示来自于12306的通知";
			// 
			// MsgFrom12306
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.btnOk;
			this.ClientSize = new System.Drawing.Size(565, 301);
			this.Controls.Add(this.chkEnable);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.labelX1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "MsgFrom12306";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "12306通知";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevComponents.DotNetBar.LabelX labelX1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private DevComponents.DotNetBar.ButtonX btnOk;
		private DevComponents.DotNetBar.Controls.CheckBoxX chkEnable;
	}
}