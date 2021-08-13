namespace TOBA.UI.Controls.Option
{
	partial class WebNotificationConfig
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
			this.chkEnable = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.panel1 = new System.Windows.Forms.Panel();
			this.pDetail = new System.Windows.Forms.Panel();
			this.ct = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.ciType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.txtKeyword = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.btnTest = new DevComponents.DotNetBar.ButtonX();
			this.txtPostBody = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.txtRefer = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.txtUrl = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX6 = new DevComponents.DotNetBar.LabelX();
			this.labelX4 = new DevComponents.DotNetBar.LabelX();
			this.labelX5 = new DevComponents.DotNetBar.LabelX();
			this.labelX3 = new DevComponents.DotNetBar.LabelX();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.panel1.SuspendLayout();
			this.pDetail.SuspendLayout();
			this.SuspendLayout();
			// 
			// chkEnable
			// 
			// 
			// 
			// 
			this.chkEnable.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.chkEnable.Location = new System.Drawing.Point(22, 15);
			this.chkEnable.Name = "chkEnable";
			this.chkEnable.Size = new System.Drawing.Size(508, 18);
			this.chkEnable.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.chkEnable.TabIndex = 0;
			this.chkEnable.Text = "启用订票成功WEB通知<font color=\"#595959\"> (如果启用，订票成功时订票助手.NET会自动发送请求到指定地址)</font>";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.pDetail);
			this.panel1.Controls.Add(this.chkEnable);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(535, 366);
			this.panel1.TabIndex = 1;
			// 
			// pDetail
			// 
			this.pDetail.Controls.Add(this.ct);
			this.pDetail.Controls.Add(this.ciType);
			this.pDetail.Controls.Add(this.txtKeyword);
			this.pDetail.Controls.Add(this.btnTest);
			this.pDetail.Controls.Add(this.txtPostBody);
			this.pDetail.Controls.Add(this.txtRefer);
			this.pDetail.Controls.Add(this.txtUrl);
			this.pDetail.Controls.Add(this.labelX6);
			this.pDetail.Controls.Add(this.labelX4);
			this.pDetail.Controls.Add(this.labelX5);
			this.pDetail.Controls.Add(this.labelX3);
			this.pDetail.Controls.Add(this.labelX1);
			this.pDetail.Controls.Add(this.labelX2);
			this.pDetail.Location = new System.Drawing.Point(6, 39);
			this.pDetail.Name = "pDetail";
			this.pDetail.Size = new System.Drawing.Size(526, 324);
			this.pDetail.TabIndex = 1;
			// 
			// ct
			// 
			this.ct.DisplayMember = "Text";
			this.ct.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.ct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ct.FormattingEnabled = true;
			this.ct.ItemHeight = 18;
			this.ct.Location = new System.Drawing.Point(180, 117);
			this.ct.Name = "ct";
			this.ct.Size = new System.Drawing.Size(326, 24);
			this.ct.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ct.TabIndex = 14;
			this.ct.ValueMember = "Value";
			this.ct.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty;
			this.ct.WatermarkText = "ContentType";
			// 
			// ciType
			// 
			this.ciType.DisplayMember = "Text";
			this.ciType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.ciType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ciType.FormattingEnabled = true;
			this.ciType.ItemHeight = 18;
			this.ciType.Location = new System.Drawing.Point(91, 116);
			this.ciType.Name = "ciType";
			this.ciType.Size = new System.Drawing.Size(83, 24);
			this.ciType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.ciType.TabIndex = 13;
			this.ciType.ValueMember = "Value";
			this.ciType.WatermarkBehavior = DevComponents.DotNetBar.eWatermarkBehavior.HideNonEmpty;
			this.ciType.WatermarkText = "Method";
			// 
			// txtKeyword
			// 
			// 
			// 
			// 
			this.txtKeyword.Border.Class = "TextBoxBorder";
			this.txtKeyword.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtKeyword.Location = new System.Drawing.Point(89, 237);
			this.txtKeyword.Name = "txtKeyword";
			this.txtKeyword.PreventEnterBeep = true;
			this.txtKeyword.Size = new System.Drawing.Size(416, 23);
			this.txtKeyword.TabIndex = 12;
			this.txtKeyword.WatermarkText = "留空不检查。如果设置，只有服务器返回包含指定关键字才视作成功。";
			// 
			// btnTest
			// 
			this.btnTest.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnTest.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnTest.Image = global::TOBA.Properties.Resources.xfsm_switch;
			this.btnTest.Location = new System.Drawing.Point(372, 266);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(136, 38);
			this.btnTest.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnTest.TabIndex = 11;
			this.btnTest.Text = "测试设置";
			// 
			// txtPostBody
			// 
			this.txtPostBody.AcceptsReturn = true;
			// 
			// 
			// 
			this.txtPostBody.Border.Class = "TextBoxBorder";
			this.txtPostBody.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtPostBody.FocusHighlightEnabled = true;
			this.txtPostBody.Location = new System.Drawing.Point(89, 143);
			this.txtPostBody.Multiline = true;
			this.txtPostBody.Name = "txtPostBody";
			this.txtPostBody.PreventEnterBeep = true;
			this.txtPostBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.txtPostBody.Size = new System.Drawing.Size(417, 87);
			this.txtPostBody.TabIndex = 9;
			this.txtPostBody.WatermarkText = "如果对应的请求内容格式要求转义，请自行转义后填入。\r\n\r\n文本请求编码一律为UTF-8。";
			// 
			// txtRefer
			// 
			// 
			// 
			// 
			this.txtRefer.Border.Class = "TextBoxBorder";
			this.txtRefer.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtRefer.FocusHighlightEnabled = true;
			this.txtRefer.Location = new System.Drawing.Point(91, 64);
			this.txtRefer.Multiline = true;
			this.txtRefer.Name = "txtRefer";
			this.txtRefer.PreventEnterBeep = true;
			this.txtRefer.Size = new System.Drawing.Size(417, 49);
			this.txtRefer.TabIndex = 9;
			this.txtRefer.WatermarkText = "可以使用变量，留空则使用通知网址同样的值";
			// 
			// txtUrl
			// 
			// 
			// 
			// 
			this.txtUrl.Border.Class = "TextBoxBorder";
			this.txtUrl.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtUrl.FocusHighlightEnabled = true;
			this.txtUrl.Location = new System.Drawing.Point(91, 3);
			this.txtUrl.Multiline = true;
			this.txtUrl.Name = "txtUrl";
			this.txtUrl.PreventEnterBeep = true;
			this.txtUrl.Size = new System.Drawing.Size(417, 55);
			this.txtUrl.TabIndex = 9;
			this.txtUrl.WatermarkText = "要发送的目标网址。可以使用变量，在发送时将会自动替换成指定的值，如：\r\nhttp://..../send.aspx?acc=$acc$&&order=$order" +
    "$";
			// 
			// labelX6
			// 
			// 
			// 
			// 
			this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX6.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX6.Location = new System.Drawing.Point(18, 238);
			this.labelX6.Name = "labelX6";
			this.labelX6.Size = new System.Drawing.Size(67, 15);
			this.labelX6.TabIndex = 8;
			this.labelX6.Text = "检测关键字";
			this.labelX6.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// labelX4
			// 
			// 
			// 
			// 
			this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX4.Location = new System.Drawing.Point(16, 143);
			this.labelX4.Name = "labelX4";
			this.labelX4.Size = new System.Drawing.Size(67, 15);
			this.labelX4.TabIndex = 8;
			this.labelX4.Text = "数据";
			this.labelX4.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// labelX5
			// 
			// 
			// 
			// 
			this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX5.Location = new System.Drawing.Point(16, 64);
			this.labelX5.Name = "labelX5";
			this.labelX5.Size = new System.Drawing.Size(69, 21);
			this.labelX5.TabIndex = 6;
			this.labelX5.Text = "引用网址";
			this.labelX5.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// labelX3
			// 
			// 
			// 
			// 
			this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX3.Location = new System.Drawing.Point(16, 118);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(67, 20);
			this.labelX3.TabIndex = 7;
			this.labelX3.Text = "类型";
			this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX1.Location = new System.Drawing.Point(16, 3);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(69, 21);
			this.labelX1.TabIndex = 6;
			this.labelX1.Text = "通知网址";
			this.labelX1.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// labelX2
			// 
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Location = new System.Drawing.Point(3, 266);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(363, 55);
			this.labelX2.TabIndex = 5;
			this.labelX2.Text = "<font color=\"#ED1C24\">可以用以下变量加入内容中，发送时会替换为实际信息</font><br />\r\n<u>$date$</u> 车票日期 <" +
    "u>$code$</u> 车次编号 <u>$order$</u> 订单编号<br />\r\n<u>$from$</u> 发站 <u>$to$</u> 到站 <u>" +
    "$acc$</u> 帐户名 <u>$pas$</u> 乘车人";
			// 
			// WebNotificationConfig
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BigImage = global::TOBA.Properties.Resources.cou_32_rss;
			this.Controls.Add(this.panel1);
			this.DisplayText = "订票WEB通知";
			this.Image = global::TOBA.Properties.Resources.cou_16_rss;
			this.Name = "WebNotificationConfig";
			this.panel1.ResumeLayout(false);
			this.pDetail.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.Controls.CheckBoxX chkEnable;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel pDetail;
		private DevComponents.DotNetBar.LabelX labelX2;
		private DevComponents.DotNetBar.LabelX labelX4;
		private DevComponents.DotNetBar.LabelX labelX3;
		private DevComponents.DotNetBar.LabelX labelX1;
		private DevComponents.DotNetBar.Controls.TextBoxX txtPostBody;
		private DevComponents.DotNetBar.Controls.TextBoxX txtRefer;
		private DevComponents.DotNetBar.Controls.TextBoxX txtUrl;
		private DevComponents.DotNetBar.LabelX labelX5;
		private DevComponents.DotNetBar.ButtonX btnTest;
		private DevComponents.DotNetBar.Controls.TextBoxX txtKeyword;
		private DevComponents.DotNetBar.LabelX labelX6;
		private DevComponents.DotNetBar.Controls.ComboBoxEx ciType;
		private DevComponents.DotNetBar.Controls.ComboBoxEx ct;
	}
}
