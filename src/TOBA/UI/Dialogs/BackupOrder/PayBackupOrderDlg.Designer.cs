namespace TOBA.UI.Dialogs.BackupOrder
{
	using Controls.Misc;

	partial class PayBackupOrderDlg
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
			this.loading = new Loading();
			this.SuspendLayout();
			// 
			// loading
			// 
			this.loading.BackColor = System.Drawing.SystemColors.Window;
			this.loading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.loading.Dock = System.Windows.Forms.DockStyle.Fill;
			this.loading.HideOnSuccess = false;
			this.loading.LoadingText = "订单准备中...";
			this.loading.Location = new System.Drawing.Point(0, 0);
			this.loading.Name = "loading";
			this.loading.Size = new System.Drawing.Size(390, 42);
			this.loading.TabIndex = 0;
			this.loading.TextInLoading = "正在加载中....";
			this.loading.TextLoadingError = "加载失败....";
			this.loading.TextLoadingOk = "加载成功";
			// 
			// PayBackupOrderDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(390, 42);
			this.Controls.Add(this.loading);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PayBackupOrderDlg";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "支付候补订单";
			this.ResumeLayout(false);

		}

		#endregion

		private Loading loading;
	}
}