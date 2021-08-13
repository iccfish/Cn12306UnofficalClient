namespace TOBA.UI.Controls.Account
{
	partial class UserTabOperation
	{
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserTabOperation));
			this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
			this.btnOpModPwd = new DevComponents.DotNetBar.ButtonX();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.expandablePanel3 = new DevComponents.DotNetBar.ExpandablePanel();
			this.btnCheckMobile = new DevComponents.DotNetBar.ButtonX();
			this.btnChangeMobile = new DevComponents.DotNetBar.ButtonX();
			this.lblMobileStatus = new DevComponents.DotNetBar.LabelX();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.expandablePanel1.SuspendLayout();
			this.expandablePanel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// expandablePanel1
			// 
			this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
			this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.expandablePanel1.Controls.Add(this.btnOpModPwd);
			this.expandablePanel1.Controls.Add(this.labelX1);
			this.expandablePanel1.DisabledBackColor = System.Drawing.Color.Empty;
			this.expandablePanel1.ExpandButtonVisible = false;
			this.expandablePanel1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.expandablePanel1.HideControlsWhenCollapsed = true;
			this.expandablePanel1.Location = new System.Drawing.Point(16, 18);
			this.expandablePanel1.Name = "expandablePanel1";
			this.expandablePanel1.Size = new System.Drawing.Size(381, 146);
			this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandablePanel1.Style.GradientAngle = 90;
			this.expandablePanel1.TabIndex = 6;
			this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
			this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.expandablePanel1.TitleStyle.GradientAngle = 90;
			this.expandablePanel1.TitleText = "更改登录密码";
			// 
			// btnOpModPwd
			// 
			this.btnOpModPwd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnOpModPwd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnOpModPwd.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnOpModPwd.Image = global::TOBA.Properties.Resources.cou_32_unlock;
			this.btnOpModPwd.Location = new System.Drawing.Point(79, 97);
			this.btnOpModPwd.Name = "btnOpModPwd";
			this.btnOpModPwd.Size = new System.Drawing.Size(205, 39);
			this.btnOpModPwd.TabIndex = 1;
			this.btnOpModPwd.Text = "修改我的12306密码";
			// 
			// labelX1
			// 
			this.labelX1.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Location = new System.Drawing.Point(21, 37);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(354, 54);
			this.labelX1.TabIndex = 4;
			this.labelX1.Text = "在把您的账号给别人请其代为购票时，建议先修改为临时的密码，并及时修改回来，以保证车票和账户安全。";
			this.labelX1.WordWrap = true;
			// 
			// expandablePanel3
			// 
			this.expandablePanel3.CanvasColor = System.Drawing.SystemColors.Control;
			this.expandablePanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.expandablePanel3.Controls.Add(this.btnCheckMobile);
			this.expandablePanel3.Controls.Add(this.btnChangeMobile);
			this.expandablePanel3.Controls.Add(this.lblMobileStatus);
			this.expandablePanel3.Controls.Add(this.labelX2);
			this.expandablePanel3.DisabledBackColor = System.Drawing.Color.Empty;
			this.expandablePanel3.ExpandButtonVisible = false;
			this.expandablePanel3.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.expandablePanel3.HideControlsWhenCollapsed = true;
			this.expandablePanel3.Location = new System.Drawing.Point(403, 18);
			this.expandablePanel3.Name = "expandablePanel3";
			this.expandablePanel3.Size = new System.Drawing.Size(381, 146);
			this.expandablePanel3.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanel3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanel3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.expandablePanel3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.expandablePanel3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
			this.expandablePanel3.Style.GradientAngle = 90;
			this.expandablePanel3.TabIndex = 6;
			this.expandablePanel3.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
			this.expandablePanel3.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.expandablePanel3.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.expandablePanel3.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
			this.expandablePanel3.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.expandablePanel3.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.expandablePanel3.TitleStyle.GradientAngle = 90;
			this.expandablePanel3.TitleText = "手机号及核验";
			// 
			// btnCheckMobile
			// 
			this.btnCheckMobile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnCheckMobile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnCheckMobile.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnCheckMobile.Image = ((System.Drawing.Image)(resources.GetObject("btnCheckMobile.Image")));
			this.btnCheckMobile.Location = new System.Drawing.Point(199, 97);
			this.btnCheckMobile.Name = "btnCheckMobile";
			this.btnCheckMobile.Size = new System.Drawing.Size(146, 39);
			this.btnCheckMobile.TabIndex = 1;
			this.btnCheckMobile.Text = "核验手机号码";
			// 
			// btnChangeMobile
			// 
			this.btnChangeMobile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnChangeMobile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnChangeMobile.Font = new System.Drawing.Font("Microsoft YaHei", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnChangeMobile.Image = ((System.Drawing.Image)(resources.GetObject("btnChangeMobile.Image")));
			this.btnChangeMobile.Location = new System.Drawing.Point(47, 97);
			this.btnChangeMobile.Name = "btnChangeMobile";
			this.btnChangeMobile.Size = new System.Drawing.Size(146, 39);
			this.btnChangeMobile.TabIndex = 1;
			this.btnChangeMobile.Text = "修改手机号码";
			// 
			// lblMobileStatus
			// 
			this.lblMobileStatus.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.lblMobileStatus.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblMobileStatus.Location = new System.Drawing.Point(21, 66);
			this.lblMobileStatus.Name = "lblMobileStatus";
			this.lblMobileStatus.Size = new System.Drawing.Size(354, 25);
			this.lblMobileStatus.TabIndex = 4;
			this.lblMobileStatus.WordWrap = true;
			// 
			// labelX2
			// 
			this.labelX2.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Location = new System.Drawing.Point(21, 37);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(354, 25);
			this.labelX2.TabIndex = 4;
			this.labelX2.Text = "12306已开通手机核验。请及时核验以免对操作产生影响。";
			this.labelX2.WordWrap = true;
			// 
			// UserTabOperation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.expandablePanel3);
			this.Controls.Add(this.expandablePanel1);
			this.Name = "UserTabOperation";
			this.Size = new System.Drawing.Size(833, 483);
			this.expandablePanel1.ResumeLayout(false);
			this.expandablePanel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private DevComponents.DotNetBar.ButtonX btnOpModPwd;
		private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.ExpandablePanel expandablePanel3;
		private DevComponents.DotNetBar.ButtonX btnCheckMobile;
		private DevComponents.DotNetBar.ButtonX btnChangeMobile;
		private DevComponents.DotNetBar.LabelX labelX2;
		private DevComponents.DotNetBar.LabelX lblMobileStatus;
	}
}
