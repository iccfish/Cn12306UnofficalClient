namespace TOBA.UI.Dialogs.Notification
{
	partial class SystemClosed
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
			this.btnOk = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.lblContinuable = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(369, 71);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(127, 36);
			this.btnOk.TabIndex = 7;
			this.btnOk.Text = "确定(&O)";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(76, 11);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(421, 32);
			this.label1.TabIndex = 5;
			this.label1.Text = "系统维护中，您将无法正常使用12306以及订票助手，请等待系统开放后使用。";
			// 
			// lblContinuable
			// 
			this.lblContinuable.Image = global::TOBA.Properties.Resources.block_16;
			this.lblContinuable.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblContinuable.Location = new System.Drawing.Point(12, 84);
			this.lblContinuable.Name = "lblContinuable";
			this.lblContinuable.Size = new System.Drawing.Size(246, 24);
			this.lblContinuable.TabIndex = 6;
			this.lblContinuable.Text = "这个错误将导致订票助手无法继续运行";
			this.lblContinuable.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TOBA.Properties.Resources.onebit_36;
			this.pictureBox1.Location = new System.Drawing.Point(12, 11);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 4;
			this.pictureBox1.TabStop = false;
			// 
			// SystemClosed
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(509, 118);
			this.ControlBox = false;
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lblContinuable);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.pictureBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "SystemClosed";
			this.ShowInTaskbar = true;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "系统维护";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Label lblContinuable;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}