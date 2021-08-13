namespace TOBA.UI.Controls.Query
{
	partial class SeatRuleEditor
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnClose = new DevComponents.DotNetBar.ButtonX();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.pContentMinimum = new System.Windows.Forms.Panel();
			this.cbEditorMinimumType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.comboItem1 = new DevComponents.Editors.ComboItem();
			this.comboItem2 = new DevComponents.Editors.ComboItem();
			this.iEditorMinimumValue = new DevComponents.Editors.IntegerInput();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.ckMinimum = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.lnkHelp = new System.Windows.Forms.LinkLabel();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panelEx1.SuspendLayout();
			this.pContentMinimum.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.iEditorMinimumValue)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lnkHelp);
			this.panel1.Controls.Add(this.btnClose);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(3, 240);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(434, 37);
			this.panel1.TabIndex = 0;
			// 
			// btnClose
			// 
			this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnClose.Location = new System.Drawing.Point(334, 6);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(100, 27);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "确定(&O)";
			// 
			// panel2
			// 
			this.panel2.AutoScroll = true;
			this.panel2.Controls.Add(this.panelEx1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(3, 3);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(434, 237);
			this.panel2.TabIndex = 1;
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.pContentMinimum);
			this.panelEx1.Controls.Add(this.ckMinimum);
			this.panelEx1.DisabledBackColor = System.Drawing.Color.Empty;
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelEx1.Location = new System.Drawing.Point(0, 0);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(434, 28);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 6;
			// 
			// pContentMinimum
			// 
			this.pContentMinimum.Controls.Add(this.cbEditorMinimumType);
			this.pContentMinimum.Controls.Add(this.iEditorMinimumValue);
			this.pContentMinimum.Controls.Add(this.labelX1);
			this.pContentMinimum.Location = new System.Drawing.Point(87, 2);
			this.pContentMinimum.Name = "pContentMinimum";
			this.pContentMinimum.Size = new System.Drawing.Size(350, 25);
			this.pContentMinimum.TabIndex = 5;
			// 
			// cbEditorMinimumType
			// 
			this.cbEditorMinimumType.DisplayMember = "Text";
			this.cbEditorMinimumType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cbEditorMinimumType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbEditorMinimumType.FormattingEnabled = true;
			this.cbEditorMinimumType.ItemHeight = 15;
			this.cbEditorMinimumType.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2});
			this.cbEditorMinimumType.Location = new System.Drawing.Point(99, 1);
			this.cbEditorMinimumType.Name = "cbEditorMinimumType";
			this.cbEditorMinimumType.Size = new System.Drawing.Size(58, 21);
			this.cbEditorMinimumType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cbEditorMinimumType.TabIndex = 2;
			// 
			// comboItem1
			// 
			this.comboItem1.Text = "少于";
			// 
			// comboItem2
			// 
			this.comboItem2.Text = "多于";
			// 
			// iEditorMinimumValue
			// 
			// 
			// 
			// 
			this.iEditorMinimumValue.BackgroundStyle.Class = "DateTimeInputBackground";
			this.iEditorMinimumValue.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.iEditorMinimumValue.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
			this.iEditorMinimumValue.Location = new System.Drawing.Point(163, 1);
			this.iEditorMinimumValue.MaxValue = 1000;
			this.iEditorMinimumValue.MinValue = 0;
			this.iEditorMinimumValue.Name = "iEditorMinimumValue";
			this.iEditorMinimumValue.ShowUpDown = true;
			this.iEditorMinimumValue.Size = new System.Drawing.Size(63, 21);
			this.iEditorMinimumValue.TabIndex = 3;
			this.iEditorMinimumValue.Value = 10;
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Location = new System.Drawing.Point(5, 3);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(342, 19);
			this.labelX1.TabIndex = 1;
			this.labelX1.Text = "除非此席别车票　　　　　　　　　　　张，否则忽略此席别";
			// 
			// ckMinimum
			// 
			// 
			// 
			// 
			this.ckMinimum.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.ckMinimum.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ckMinimum.Location = new System.Drawing.Point(11, 5);
			this.ckMinimum.Name = "ckMinimum";
			this.ckMinimum.Size = new System.Drawing.Size(80, 15);
			this.ckMinimum.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ckMinimum.TabIndex = 0;
			this.ckMinimum.Text = "票数限制";
			// 
			// lnkHelp
			// 
			this.lnkHelp.Image = global::TOBA.Properties.Resources.cou_16_help;
			this.lnkHelp.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnkHelp.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lnkHelp.LinkColor = System.Drawing.Color.Maroon;
			this.lnkHelp.Location = new System.Drawing.Point(3, 3);
			this.lnkHelp.Name = "lnkHelp";
			this.lnkHelp.Size = new System.Drawing.Size(75, 34);
			this.lnkHelp.TabIndex = 1;
			this.lnkHelp.TabStop = true;
			this.lnkHelp.Text = "如何使用";
			this.lnkHelp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lnkHelp.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkHelp_LinkClicked);
			// 
			// SeatRuleEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "SeatRuleEditor";
			this.Padding = new System.Windows.Forms.Padding(3);
			this.Size = new System.Drawing.Size(440, 280);
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panelEx1.ResumeLayout(false);
			this.pContentMinimum.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.iEditorMinimumValue)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private DevComponents.DotNetBar.ButtonX btnClose;
		private System.Windows.Forms.Panel panel2;
		private DevComponents.DotNetBar.PanelEx panelEx1;
		private System.Windows.Forms.Panel pContentMinimum;
		private DevComponents.DotNetBar.Controls.ComboBoxEx cbEditorMinimumType;
		private DevComponents.Editors.ComboItem comboItem1;
		private DevComponents.Editors.ComboItem comboItem2;
		private DevComponents.Editors.IntegerInput iEditorMinimumValue;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.Controls.CheckBoxX ckMinimum;
		private System.Windows.Forms.LinkLabel lnkHelp;
	}
}
