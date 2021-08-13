namespace TOBA.UI.Controls.Vc
{
	partial class TouchClickVc
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
			this.ps = new DevComponents.DotNetBar.ProgressSteps();
			this.sibegin = new DevComponents.DotNetBar.StepItem();
			this.pb = new System.Windows.Forms.PictureBox();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
			this.SuspendLayout();
			// 
			// ps
			// 
			this.ps.AutoSize = true;
			// 
			// 
			// 
			this.ps.BackgroundStyle.Class = "ProgressSteps";
			this.ps.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ps.ContainerControlProcessDialogKey = true;
			this.ps.Dock = System.Windows.Forms.DockStyle.Top;
			this.ps.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ps.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.sibegin});
			this.ps.Location = new System.Drawing.Point(0, 22);
			this.ps.Name = "ps";
			this.ps.Size = new System.Drawing.Size(297, 21);
			this.ps.Style = DevComponents.DotNetBar.eDotNetBarStyle.Office2010;
			this.ps.TabIndex = 1;
			// 
			// sibegin
			// 
			this.sibegin.HotTracking = false;
			this.sibegin.Name = "sibegin";
			this.sibegin.Symbol = "";
			this.sibegin.SymbolColor = System.Drawing.Color.White;
			this.sibegin.SymbolSize = 13F;
			this.sibegin.Text = "依次点击";
			this.sibegin.TextColor = System.Drawing.Color.White;
			this.sibegin.Value = 100;
			// 
			// pb
			// 
			this.pb.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pb.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pb.Location = new System.Drawing.Point(0, 43);
			this.pb.Name = "pb";
			this.pb.Size = new System.Drawing.Size(297, 218);
			this.pb.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pb.TabIndex = 2;
			this.pb.TabStop = false;
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelEx1.Location = new System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(297, 22);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 3;
			this.panelEx1.Text = "请在图片中依次点击指定的文字来完成输入并验证。";
			// 
			// TouchClickVc
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(297, 261);
			this.Controls.Add(this.pb);
			this.Controls.Add(this.ps);
			this.Controls.Add(this.panelEx1);
			this.Name = "TouchClickVc";
			this.Text = "请点击图片输入验证码";
			((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevComponents.DotNetBar.ProgressSteps ps;
		private DevComponents.DotNetBar.StepItem sibegin;
		private System.Windows.Forms.PictureBox pb;
		private DevComponents.DotNetBar.PanelEx panelEx1;
	}
}