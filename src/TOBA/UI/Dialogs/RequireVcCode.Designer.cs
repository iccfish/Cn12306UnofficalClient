namespace TOBA.UI.Dialogs
{
	partial class RequireVcCode
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
			this.verifyCodeBox1 = new TOBA.UI.Controls.VerifyCodeBox();
			this.txtBox = new System.Windows.Forms.TextBox();
			this.vcStatus = new DevComponents.DotNetBar.LabelX();
			((System.ComponentModel.ISupportInitialize)(this.verifyCodeBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// verifyCodeBox1
			// 
			this.verifyCodeBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.verifyCodeBox1.Location = new System.Drawing.Point(12, 16);
			this.verifyCodeBox1.Name = "verifyCodeBox1";
			this.verifyCodeBox1.RandType = TOBA.WebLib.RandCodeType.SjRand;
			this.verifyCodeBox1.Size = new System.Drawing.Size(178, 81);
			this.verifyCodeBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.verifyCodeBox1.TabIndex = 0;
			this.verifyCodeBox1.TabStop = false;
			// 
			// txtBox
			// 
			this.txtBox.Font = new System.Drawing.Font("Verdana", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtBox.ImeMode = System.Windows.Forms.ImeMode.Off;
			this.txtBox.Location = new System.Drawing.Point(196, 16);
			this.txtBox.MaxLength = 4;
			this.txtBox.Name = "txtBox";
			this.txtBox.Size = new System.Drawing.Size(139, 46);
			this.txtBox.TabIndex = 1;
			this.txtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// vcStatus
			// 
			this.vcStatus.Location = new System.Drawing.Point(196, 68);
			this.vcStatus.EnableMarkup = true;
			this.vcStatus.Name = "vcStatus";
			this.vcStatus.Size = new System.Drawing.Size(139, 29);
			this.vcStatus.TabIndex = 2;
			// 
			// RequireVcCode
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(347, 109);
			this.Controls.Add(this.vcStatus);
			this.Controls.Add(this.txtBox);
			this.Controls.Add(this.verifyCodeBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RequireVcCode";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "请输入验证码";
			this.TopMost = true;
			this.Load += new System.EventHandler(this.RequireVcCode_Load);
			((System.ComponentModel.ISupportInitialize)(this.verifyCodeBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Controls.VerifyCodeBox verifyCodeBox1;
		private System.Windows.Forms.TextBox txtBox;
		private DevComponents.DotNetBar.LabelX vcStatus;
	}
}