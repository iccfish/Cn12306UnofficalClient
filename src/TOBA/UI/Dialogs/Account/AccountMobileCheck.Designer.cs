namespace TOBA.UI.Dialogs.Account
{
	partial class AccountMobileCheck
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
			this.btnApply = new DevComponents.DotNetBar.ButtonX();
			this.btnGetCode = new DevComponents.DotNetBar.ButtonX();
			this.btnModify = new DevComponents.DotNetBar.ButtonX();
			this.txtCode = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.txtMobile = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.pDesc = new DevComponents.DotNetBar.PanelEx();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.lblStatus = new DevComponents.DotNetBar.LabelX();
			this.labelX3 = new DevComponents.DotNetBar.LabelX();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnApply);
			this.panel1.Controls.Add(this.btnGetCode);
			this.panel1.Controls.Add(this.btnModify);
			this.panel1.Controls.Add(this.txtCode);
			this.panel1.Controls.Add(this.txtMobile);
			this.panel1.Controls.Add(this.pDesc);
			this.panel1.Controls.Add(this.labelX2);
			this.panel1.Controls.Add(this.lblStatus);
			this.panel1.Controls.Add(this.labelX3);
			this.panel1.Controls.Add(this.labelX1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(554, 311);
			this.panel1.TabIndex = 0;
			// 
			// btnApply
			// 
			this.btnApply.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnApply.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnApply.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnApply.Image = global::TOBA.Properties.Resources.accept;
			this.btnApply.Location = new System.Drawing.Point(180, 240);
			this.btnApply.Name = "btnApply";
			this.btnApply.Size = new System.Drawing.Size(178, 50);
			this.btnApply.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnApply.TabIndex = 7;
			this.btnApply.Text = "完成核验";
			// 
			// btnGetCode
			// 
			this.btnGetCode.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnGetCode.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnGetCode.DisabledImage = global::TOBA.Properties.Resources.clock_16;
			this.btnGetCode.Image = global::TOBA.Properties.Resources.xfsm_switch;
			this.btnGetCode.Location = new System.Drawing.Point(401, 194);
			this.btnGetCode.Name = "btnGetCode";
			this.btnGetCode.Size = new System.Drawing.Size(120, 26);
			this.btnGetCode.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnGetCode.TabIndex = 6;
			this.btnGetCode.Text = "获得验证码";
			// 
			// btnModify
			// 
			this.btnModify.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnModify.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnModify.Image = global::TOBA.Properties.Resources.pencil_16;
			this.btnModify.Location = new System.Drawing.Point(401, 150);
			this.btnModify.Name = "btnModify";
			this.btnModify.Size = new System.Drawing.Size(120, 26);
			this.btnModify.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnModify.TabIndex = 6;
			this.btnModify.Text = "修改";
			// 
			// txtCode
			// 
			// 
			// 
			// 
			this.txtCode.Border.Class = "TextBoxBorder";
			this.txtCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtCode.FocusHighlightEnabled = true;
			this.txtCode.Location = new System.Drawing.Point(110, 194);
			this.txtCode.Name = "txtCode";
			this.txtCode.PreventEnterBeep = true;
			this.txtCode.Size = new System.Drawing.Size(285, 26);
			this.txtCode.TabIndex = 5;
			// 
			// txtMobile
			// 
			// 
			// 
			// 
			this.txtMobile.Border.Class = "TextBoxBorder";
			this.txtMobile.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtMobile.Location = new System.Drawing.Point(110, 150);
			this.txtMobile.Name = "txtMobile";
			this.txtMobile.PreventEnterBeep = true;
			this.txtMobile.ReadOnly = true;
			this.txtMobile.Size = new System.Drawing.Size(285, 26);
			this.txtMobile.TabIndex = 5;
			// 
			// pDesc
			// 
			this.pDesc.CanvasColor = System.Drawing.SystemColors.Control;
			this.pDesc.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.pDesc.DisabledBackColor = System.Drawing.Color.Empty;
			this.pDesc.Dock = System.Windows.Forms.DockStyle.Top;
			this.pDesc.Location = new System.Drawing.Point(0, 0);
			this.pDesc.Name = "pDesc";
			this.pDesc.Size = new System.Drawing.Size(554, 81);
			this.pDesc.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.pDesc.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.pDesc.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.pDesc.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.pDesc.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.pDesc.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.pDesc.Style.GradientAngle = 90;
			this.pDesc.TabIndex = 1;
			this.pDesc.Text = "请点击发送验证码，并将手机收到的验证码填入验证码框中，并继续完成核验";
			// 
			// labelX2
			// 
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX2.Location = new System.Drawing.Point(18, 199);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(86, 21);
			this.labelX2.TabIndex = 0;
			this.labelX2.Text = "手机验证码";
			this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// lblStatus
			// 
			// 
			// 
			// 
			this.lblStatus.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblStatus.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblStatus.ForeColor = System.Drawing.Color.Red;
			this.lblStatus.Location = new System.Drawing.Point(110, 104);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(411, 21);
			this.lblStatus.TabIndex = 0;
			// 
			// labelX3
			// 
			// 
			// 
			// 
			this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX3.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX3.Location = new System.Drawing.Point(18, 104);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(86, 21);
			this.labelX3.TabIndex = 0;
			this.labelX3.Text = "当前状态";
			this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX1.Location = new System.Drawing.Point(18, 151);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(86, 21);
			this.labelX1.TabIndex = 0;
			this.labelX1.Text = "当前手机号";
			this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// AccountMobileCheck
			// 
			this.AcceptButton = this.btnApply;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(554, 311);
			this.Controls.Add(this.panel1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "AccountMobileCheck";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "手机核验";
			this.Load += new System.EventHandler(this.AccountMobileCheck_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private DevComponents.DotNetBar.ButtonX btnApply;
		private DevComponents.DotNetBar.ButtonX btnGetCode;
		private DevComponents.DotNetBar.ButtonX btnModify;
		private DevComponents.DotNetBar.Controls.TextBoxX txtCode;
		private DevComponents.DotNetBar.Controls.TextBoxX txtMobile;
		private DevComponents.DotNetBar.PanelEx pDesc;
		private DevComponents.DotNetBar.LabelX labelX2;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.LabelX lblStatus;
		private DevComponents.DotNetBar.LabelX labelX3;
	}
}