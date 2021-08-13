namespace TOBA.UI.Controls.Misc
{
	partial class MsgLayer
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
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.pMain = new DevComponents.DotNetBar.PanelEx();
			this.lblInfo = new DevComponents.DotNetBar.LabelX();
			this.btnOk = new DevComponents.DotNetBar.ButtonX();
			this.btnCancel = new DevComponents.DotNetBar.ButtonX();
			this.pConfirm = new System.Windows.Forms.Panel();
			this.pInfo = new System.Windows.Forms.Panel();
			this.btnOkOnly = new DevComponents.DotNetBar.ButtonX();
			this.pMain.SuspendLayout();
			this.pConfirm.SuspendLayout();
			this.pInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// pMain
			// 
			this.pMain.CanvasColor = System.Drawing.SystemColors.Control;
			this.pMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.pMain.Controls.Add(this.pInfo);
			this.pMain.Controls.Add(this.pConfirm);
			this.pMain.Controls.Add(this.lblInfo);
			this.pMain.DisabledBackColor = System.Drawing.Color.Empty;
			this.pMain.Location = new System.Drawing.Point(88, 54);
			this.pMain.Name = "pMain";
			this.pMain.Size = new System.Drawing.Size(482, 162);
			this.pMain.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.pMain.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.pMain.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.pMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.pMain.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.pMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.pMain.Style.GradientAngle = 90;
			this.pMain.TabIndex = 1;
			// 
			// lblInfo
			// 
			// 
			// 
			// 
			this.lblInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblInfo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblInfo.Location = new System.Drawing.Point(0, 0);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.PaddingBottom = 5;
			this.lblInfo.PaddingLeft = 5;
			this.lblInfo.PaddingRight = 5;
			this.lblInfo.PaddingTop = 5;
			this.lblInfo.Size = new System.Drawing.Size(482, 34);
			this.lblInfo.TabIndex = 0;
			this.lblInfo.Text = "我是文本";
			this.lblInfo.TextLineAlignment = System.Drawing.StringAlignment.Near;
			this.lblInfo.WordWrap = true;
			// 
			// btnOk
			// 
			this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnOk.Location = new System.Drawing.Point(3, 0);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(122, 31);
			this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "确定(&O)";
			// 
			// btnCancel
			// 
			this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnCancel.Location = new System.Drawing.Point(131, 0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(122, 31);
			this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "取消(&C)";
			// 
			// pConfirm
			// 
			this.pConfirm.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.pConfirm.Controls.Add(this.btnOk);
			this.pConfirm.Controls.Add(this.btnCancel);
			this.pConfirm.Location = new System.Drawing.Point(121, 120);
			this.pConfirm.Name = "pConfirm";
			this.pConfirm.Size = new System.Drawing.Size(256, 35);
			this.pConfirm.TabIndex = 2;
			// 
			// pInfo
			// 
			this.pInfo.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.pInfo.Controls.Add(this.btnOkOnly);
			this.pInfo.Location = new System.Drawing.Point(186, 120);
			this.pInfo.Name = "pInfo";
			this.pInfo.Size = new System.Drawing.Size(130, 35);
			this.pInfo.TabIndex = 3;
			// 
			// btnOkOnly
			// 
			this.btnOkOnly.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnOkOnly.Location = new System.Drawing.Point(3, 0);
			this.btnOkOnly.Name = "btnOkOnly";
			this.btnOkOnly.Size = new System.Drawing.Size(122, 31);
			this.btnOkOnly.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnOkOnly.TabIndex = 1;
			this.btnOkOnly.Text = "确定(&O)";
			// 
			// MsgLayer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.pMain);
			this.Name = "MsgLayer";
			this.Size = new System.Drawing.Size(667, 284);
			this.pMain.ResumeLayout(false);
			this.pConfirm.ResumeLayout(false);
			this.pInfo.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private DevComponents.DotNetBar.PanelEx pMain;
		private DevComponents.DotNetBar.ButtonX btnCancel;
		private DevComponents.DotNetBar.ButtonX btnOk;
		private DevComponents.DotNetBar.LabelX lblInfo;
		private System.Windows.Forms.Panel pConfirm;
		private System.Windows.Forms.Panel pInfo;
		private DevComponents.DotNetBar.ButtonX btnOkOnly;
	}
}
