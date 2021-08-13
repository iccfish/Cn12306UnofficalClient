namespace TOBA.UI.Dialogs.Order
{
	using Controls.Common;
	using Controls.Misc;

	partial class PayOrder
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
			this.loading1 = new Loading();
			this.wb = new System.Windows.Forms.WebBrowser();
			this.tipBarMessage1 = new TipBarMessage();
			this.SuspendLayout();
			// 
			// loading1
			// 
			this.loading1.BackColor = System.Drawing.SystemColors.Window;
			this.loading1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.loading1.KeepCenter = false;
			this.loading1.LoadingText = "正在准备订单信息，请稍等....";
			this.loading1.Location = new System.Drawing.Point(254, 189);
			this.loading1.Name = "loading1";
			this.loading1.Size = new System.Drawing.Size(246, 59);
			this.loading1.TabIndex = 2;
			this.loading1.TextInLoading = "正在加载中....";
			this.loading1.TextLoadingError = "加载失败....";
			this.loading1.TextLoadingOk = "加载成功";
			// 
			// wb
			// 
			this.wb.Dock = System.Windows.Forms.DockStyle.Fill;
			this.wb.Location = new System.Drawing.Point(0, 0);
			this.wb.MinimumSize = new System.Drawing.Size(20, 20);
			this.wb.Name = "wb";
			this.wb.ScriptErrorsSuppressed = true;
			this.wb.Size = new System.Drawing.Size(1017, 478);
			this.wb.TabIndex = 1;
			// 
			// tipBarMessage1
			// 
			this.tipBarMessage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
			this.tipBarMessage1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.tipBarMessage1.BorderThickness = 1;
			this.tipBarMessage1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tipBarMessage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(153)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
			this.tipBarMessage1.Image = global::TOBA.Properties.Resources.warning_16;
			this.tipBarMessage1.Location = new System.Drawing.Point(0, 478);
			this.tipBarMessage1.Name = "tipBarMessage1";
			this.tipBarMessage1.Padding = new System.Windows.Forms.Padding(3);
			this.tipBarMessage1.Size = new System.Drawing.Size(1017, 25);
			this.tipBarMessage1.TabIndex = 0;
			this.tipBarMessage1.Text = "请仔细确认金额！如果无法完成支付，请使用12306登录浏览器支付！";
			// 
			// PayOrder
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1017, 503);
			this.Controls.Add(this.loading1);
			this.Controls.Add(this.wb);
			this.Controls.Add(this.tipBarMessage1);
			this.DoubleBuffered = true;
			this.MinimizeBox = false;
			this.Name = "PayOrder";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "支付订单";
			this.ResumeLayout(false);

		}

		#endregion

		private TipBarMessage tipBarMessage1;
		private System.Windows.Forms.WebBrowser wb;
		private Loading loading1;
	}
}