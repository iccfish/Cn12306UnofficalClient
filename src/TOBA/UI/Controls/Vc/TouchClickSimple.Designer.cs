namespace TOBA.UI.Controls.Vc
{
	partial class TouchClickSimple
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pReload = new System.Windows.Forms.Panel();
			this.lblReloadTip = new DevComponents.DotNetBar.LabelX();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pVcTip = new System.Windows.Forms.Panel();
			this.lblVcTip = new DevComponents.DotNetBar.LabelX();
			this.pbVcTp = new System.Windows.Forms.PictureBox();
			this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
			this.btnOk = new DevComponents.DotNetBar.ButtonX();
			this.verifyCodeBox1 = new TOBA.UI.Controls.VerifyCodeBox();
			this.textBoxX1 = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.panel1.SuspendLayout();
			this.pReload.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.pVcTip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbVcTp)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.verifyCodeBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelEx1.Location = new System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(340, 22);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 4;
			this.panelEx1.Text = "请稍候...";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.pVcTip);
			this.panel1.Controls.Add(this.btnRefresh);
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Controls.Add(this.pReload);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 257);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(340, 40);
			this.panel1.TabIndex = 6;
			// 
			// pReload
			// 
			this.pReload.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pReload.Controls.Add(this.lblReloadTip);
			this.pReload.Controls.Add(this.pictureBox1);
			this.pReload.Location = new System.Drawing.Point(108, 5);
			this.pReload.Name = "pReload";
			this.pReload.Size = new System.Drawing.Size(132, 30);
			this.pReload.TabIndex = 12;
			this.pReload.Visible = false;
			// 
			// lblReloadTip
			// 
			this.lblReloadTip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.lblReloadTip.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblReloadTip.Location = new System.Drawing.Point(18, 3);
			this.lblReloadTip.Name = "lblReloadTip";
			this.lblReloadTip.Size = new System.Drawing.Size(107, 24);
			this.lblReloadTip.TabIndex = 1;
			this.lblReloadTip.WordWrap = true;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TOBA.Properties.Resources.loading_16_3;
			this.pictureBox1.Location = new System.Drawing.Point(0, 7);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(16, 15);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// pVcTip
			// 
			this.pVcTip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pVcTip.Controls.Add(this.lblVcTip);
			this.pVcTip.Controls.Add(this.pbVcTp);
			this.pVcTip.Location = new System.Drawing.Point(109, 7);
			this.pVcTip.Name = "pVcTip";
			this.pVcTip.Size = new System.Drawing.Size(132, 30);
			this.pVcTip.TabIndex = 1;
			this.pVcTip.Visible = false;
			// 
			// lblVcTip
			// 
			this.lblVcTip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			// 
			// 
			// 
			this.lblVcTip.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblVcTip.Location = new System.Drawing.Point(21, 3);
			this.lblVcTip.Name = "lblVcTip";
			this.lblVcTip.Size = new System.Drawing.Size(104, 24);
			this.lblVcTip.TabIndex = 1;
			this.lblVcTip.WordWrap = true;
			// 
			// pbVcTp
			// 
			this.pbVcTp.Image = global::TOBA.Properties.Resources.loading_16_3;
			this.pbVcTp.Location = new System.Drawing.Point(0, 7);
			this.pbVcTp.Name = "pbVcTp";
			this.pbVcTp.Size = new System.Drawing.Size(16, 15);
			this.pbVcTp.TabIndex = 0;
			this.pbVcTp.TabStop = false;
			// 
			// btnRefresh
			// 
			this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.Orange;
			this.btnRefresh.FocusCuesEnabled = false;
			this.btnRefresh.Image = global::TOBA.Properties.Resources.cou_16_refresh;
			this.btnRefresh.Location = new System.Drawing.Point(1, 7);
			this.btnRefresh.Name = "btnRefresh";
			this.btnRefresh.Size = new System.Drawing.Size(102, 30);
			this.btnRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnRefresh.TabIndex = 0;
			this.btnRefresh.Text = "看不懂！(&R)";
			// 
			// btnOk
			// 
			this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnOk.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnOk.Image = global::TOBA.Properties.Resources.tick_16;
			this.btnOk.Location = new System.Drawing.Point(247, 1);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(93, 36);
			this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "确定(&O)";
			// 
			// verifyCodeBox1
			// 
			this.verifyCodeBox1.CodeSizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.verifyCodeBox1.Cursor = System.Windows.Forms.Cursors.Hand;
			this.verifyCodeBox1.EnableClickReload = false;
			this.verifyCodeBox1.Location = new System.Drawing.Point(3, 28);
			this.verifyCodeBox1.Name = "verifyCodeBox1";
			this.verifyCodeBox1.RandType = TOBA.WebLib.RandCodeType.SjRand;
			this.verifyCodeBox1.ShowOkButton = false;
			this.verifyCodeBox1.Size = new System.Drawing.Size(51, 37);
			this.verifyCodeBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.verifyCodeBox1.TabIndex = 7;
			this.verifyCodeBox1.TabStop = false;
			this.verifyCodeBox1.ValidateImage = null;
			// 
			// textBoxX1
			// 
			// 
			// 
			// 
			this.textBoxX1.Border.Class = "TextBoxBorder";
			this.textBoxX1.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.textBoxX1.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.textBoxX1.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.textBoxX1.Location = new System.Drawing.Point(3, 133);
			this.textBoxX1.Name = "textBoxX1";
			this.textBoxX1.PreventEnterBeep = true;
			this.textBoxX1.Size = new System.Drawing.Size(334, 35);
			this.textBoxX1.TabIndex = 8;
			this.textBoxX1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.textBoxX1.Visible = false;
			this.textBoxX1.WatermarkText = "点击输验证码";
			// 
			// TouchClickSimple
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.textBoxX1);
			this.Controls.Add(this.verifyCodeBox1);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.panelEx1);
			this.Name = "TouchClickSimple";
			this.Size = new System.Drawing.Size(340, 297);
			this.panel1.ResumeLayout(false);
			this.pReload.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.pVcTip.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbVcTp)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.verifyCodeBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.PanelEx panelEx1;
		private System.Windows.Forms.Panel panel1;
		private DevComponents.DotNetBar.ButtonX btnRefresh;
		private DevComponents.DotNetBar.ButtonX btnOk;
		private VerifyCodeBox verifyCodeBox1;
		private DevComponents.DotNetBar.LabelX lblVcTip;
		private DevComponents.DotNetBar.Controls.TextBoxX textBoxX1;
		private System.Windows.Forms.Panel pVcTip;
		private System.Windows.Forms.PictureBox pbVcTp;
		private System.Windows.Forms.Panel pReload;
		private DevComponents.DotNetBar.LabelX lblReloadTip;
		private System.Windows.Forms.PictureBox pictureBox1;
	}
}
