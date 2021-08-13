namespace TOBA.UI.Dialogs.Order
{
	partial class SubmitOrder
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
			this.components = new System.ComponentModel.Container();
			this.lstInfo = new System.Windows.Forms.ListView();
			this.colSeatClass = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colTicketCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.colTicketPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imgListTicketInfo = new System.Windows.Forms.ImageList(this.components);
			this.dgvPassenger = new System.Windows.Forms.DataGridView();
			this.dgvColName = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvTicketType = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dgvSeatType = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dgvSeatSubType = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dgvIdType = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dgvId = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvMobileNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dgvSave = new System.Windows.Forms.DataGridViewCheckBoxColumn();
			this.dgvColAddChild = new System.Windows.Forms.DataGridViewLinkColumn();
			this.dgvColRemove = new System.Windows.Forms.DataGridViewLinkColumn();
			this.chkAutoSubmit = new System.Windows.Forms.CheckBox();
			this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.ps = new TOBA.UI.Controls.Passenger.PassengerSelector();
			this.chkDwAll = new System.Windows.Forms.CheckBox();
			this.tc = new TOBA.UI.Controls.Vc.TouchClickSimple();
			this.gpError = new DevComponents.DotNetBar.Controls.GroupPanel();
			this.lblError = new DevComponents.DotNetBar.LabelX();
			((System.ComponentModel.ISupportInitialize)(this.dgvPassenger)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.gpError.SuspendLayout();
			this.SuspendLayout();
			// 
			// lstInfo
			// 
			this.lstInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colSeatClass,
            this.colTicketCount,
            this.colTicketPrice});
			this.lstInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstInfo.FullRowSelect = true;
			this.lstInfo.GridLines = true;
			this.lstInfo.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstInfo.Location = new System.Drawing.Point(3, 3);
			this.lstInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.lstInfo.Name = "lstInfo";
			this.lstInfo.Size = new System.Drawing.Size(292, 122);
			this.lstInfo.SmallImageList = this.imgListTicketInfo;
			this.lstInfo.TabIndex = 2;
			this.lstInfo.UseCompatibleStateImageBehavior = false;
			this.lstInfo.View = System.Windows.Forms.View.Details;
			// 
			// colSeatClass
			// 
			this.colSeatClass.Text = "席别名称";
			this.colSeatClass.Width = 106;
			// 
			// colTicketCount
			// 
			this.colTicketCount.Text = "余票数";
			this.colTicketCount.Width = 78;
			// 
			// colTicketPrice
			// 
			this.colTicketPrice.Text = "价格";
			this.colTicketPrice.Width = 88;
			// 
			// imgListTicketInfo
			// 
			this.imgListTicketInfo.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imgListTicketInfo.ImageSize = new System.Drawing.Size(1, 20);
			this.imgListTicketInfo.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// dgvPassenger
			// 
			this.dgvPassenger.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgvPassenger.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvPassenger.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvColName,
            this.dgvTicketType,
            this.dgvSeatType,
            this.dgvSeatSubType,
            this.dgvIdType,
            this.dgvId,
            this.dgvMobileNo,
            this.dgvSave,
            this.dgvColAddChild,
            this.dgvColRemove});
			this.dgvPassenger.Dock = System.Windows.Forms.DockStyle.Top;
			this.dgvPassenger.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
			this.dgvPassenger.Location = new System.Drawing.Point(0, 193);
			this.dgvPassenger.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.dgvPassenger.Name = "dgvPassenger";
			this.dgvPassenger.RowTemplate.Height = 23;
			this.dgvPassenger.Size = new System.Drawing.Size(859, 152);
			this.dgvPassenger.TabIndex = 3;
			this.dgvPassenger.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPassenger_CellContentClick);
			// 
			// dgvColName
			// 
			this.dgvColName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.dgvColName.HeaderText = "乘客姓名";
			this.dgvColName.Name = "dgvColName";
			// 
			// dgvTicketType
			// 
			this.dgvTicketType.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.ComboBox;
			this.dgvTicketType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.dgvTicketType.HeaderText = "票种";
			this.dgvTicketType.Name = "dgvTicketType";
			this.dgvTicketType.Width = 80;
			// 
			// dgvSeatType
			// 
			this.dgvSeatType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.dgvSeatType.HeaderText = "席别";
			this.dgvSeatType.Name = "dgvSeatType";
			this.dgvSeatType.Width = 80;
			// 
			// dgvSeatSubType
			// 
			this.dgvSeatSubType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.dgvSeatSubType.HeaderText = "席位";
			this.dgvSeatSubType.Name = "dgvSeatSubType";
			this.dgvSeatSubType.Width = 70;
			// 
			// dgvIdType
			// 
			this.dgvIdType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.dgvIdType.HeaderText = "证件类型";
			this.dgvIdType.Name = "dgvIdType";
			this.dgvIdType.Width = 110;
			// 
			// dgvId
			// 
			this.dgvId.HeaderText = "证件号码";
			this.dgvId.Name = "dgvId";
			this.dgvId.Width = 160;
			// 
			// dgvMobileNo
			// 
			this.dgvMobileNo.HeaderText = "手机号码";
			this.dgvMobileNo.Name = "dgvMobileNo";
			// 
			// dgvSave
			// 
			this.dgvSave.HeaderText = "保存";
			this.dgvSave.Name = "dgvSave";
			this.dgvSave.Visible = false;
			this.dgvSave.Width = 70;
			// 
			// dgvColAddChild
			// 
			this.dgvColAddChild.HeaderText = "儿童票";
			this.dgvColAddChild.Name = "dgvColAddChild";
			this.dgvColAddChild.Text = "添加";
			this.dgvColAddChild.ToolTipText = "使用此乘客的证件添加一个儿童票乘客";
			this.dgvColAddChild.TrackVisitedState = false;
			this.dgvColAddChild.UseColumnTextForLinkValue = true;
			this.dgvColAddChild.Width = 60;
			// 
			// dgvColRemove
			// 
			this.dgvColRemove.FillWeight = 40F;
			this.dgvColRemove.HeaderText = "删除";
			this.dgvColRemove.Name = "dgvColRemove";
			this.dgvColRemove.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dgvColRemove.Text = "删除";
			this.dgvColRemove.ToolTipText = "将这个联系人从乘客列表中删除";
			this.dgvColRemove.UseColumnTextForLinkValue = true;
			this.dgvColRemove.Width = 50;
			// 
			// chkAutoSubmit
			// 
			this.chkAutoSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.chkAutoSubmit.AutoSize = true;
			this.chkAutoSubmit.Location = new System.Drawing.Point(700, 527);
			this.chkAutoSubmit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.chkAutoSubmit.Name = "chkAutoSubmit";
			this.chkAutoSubmit.Size = new System.Drawing.Size(144, 16);
			this.chkAutoSubmit.TabIndex = 11;
			this.chkAutoSubmit.Text = "输入验证码后自动提交";
			this.chkAutoSubmit.UseVisualStyleBackColor = true;
			this.chkAutoSubmit.Visible = false;
			// 
			// btnSubmit
			// 
			this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSubmit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnSubmit.Image = global::TOBA.Properties.Resources.cou_32_accept;
			this.btnSubmit.Location = new System.Drawing.Point(700, 578);
			this.btnSubmit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.Size = new System.Drawing.Size(149, 44);
			this.btnSubmit.TabIndex = 1;
			this.btnSubmit.Text = "提交订单";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Top;
			this.splitContainer1.Location = new System.Drawing.Point(0, 65);
			this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.lstInfo);
			this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(3, 3, 0, 3);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.ps);
			this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(3);
			this.splitContainer1.Size = new System.Drawing.Size(859, 128);
			this.splitContainer1.SplitterDistance = 295;
			this.splitContainer1.SplitterWidth = 3;
			this.splitContainer1.TabIndex = 13;
			// 
			// ps
			// 
			this.ps.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ps.Image = null;
			this.ps.Location = new System.Drawing.Point(3, 3);
			this.ps.Margin = new System.Windows.Forms.Padding(3, 1, 3, 1);
			this.ps.Name = "ps";
			this.ps.SearchKey = null;
			this.ps.ShowAddLink = true;
			this.ps.ShowOnlyStudent = false;
			this.ps.Size = new System.Drawing.Size(555, 122);
			this.ps.TabIndex = 0;
			// 
			// chkDwAll
			// 
			this.chkDwAll.AutoSize = true;
			this.chkDwAll.Location = new System.Drawing.Point(760, 350);
			this.chkDwAll.Name = "chkDwAll";
			this.chkDwAll.Size = new System.Drawing.Size(84, 16);
			this.chkDwAll.TabIndex = 15;
			this.chkDwAll.Text = "订动卧整包";
			this.chkDwAll.UseVisualStyleBackColor = true;
			// 
			// tc
			// 
			this.tc.BackColor = System.Drawing.Color.White;
			this.tc.Image = null;
			this.tc.Location = new System.Drawing.Point(298, 350);
			this.tc.Name = "tc";
			this.tc.ShowOkButton = false;
			this.tc.Size = new System.Drawing.Size(396, 273);
			this.tc.TabIndex = 16;
			// 
			// gpError
			// 
			this.gpError.CanvasColor = System.Drawing.SystemColors.Control;
			this.gpError.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
			this.gpError.Controls.Add(this.lblError);
			this.gpError.DisabledBackColor = System.Drawing.Color.Empty;
			this.gpError.Location = new System.Drawing.Point(12, 358);
			this.gpError.Name = "gpError";
			this.gpError.Size = new System.Drawing.Size(255, 242);
			// 
			// 
			// 
			this.gpError.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.gpError.Style.BackColorGradientAngle = 90;
			this.gpError.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.gpError.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.gpError.Style.BorderBottomWidth = 1;
			this.gpError.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.gpError.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.gpError.Style.BorderLeftWidth = 1;
			this.gpError.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.gpError.Style.BorderRightWidth = 1;
			this.gpError.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.gpError.Style.BorderTopWidth = 1;
			this.gpError.Style.CornerDiameter = 4;
			this.gpError.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
			this.gpError.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
			this.gpError.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.gpError.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
			// 
			// 
			// 
			this.gpError.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			// 
			// 
			// 
			this.gpError.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.gpError.TabIndex = 17;
			this.gpError.Text = "错误信息";
			this.gpError.Visible = false;
			// 
			// lblError
			// 
			this.lblError.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.lblError.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblError.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblError.ForeColor = System.Drawing.Color.Red;
			this.lblError.Location = new System.Drawing.Point(0, 0);
			this.lblError.Name = "lblError";
			this.lblError.Size = new System.Drawing.Size(249, 218);
			this.lblError.TabIndex = 0;
			this.lblError.WordWrap = true;
			// 
			// SubmitOrder
			// 
			this.AcceptButton = this.btnSubmit;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(859, 649);
			this.Controls.Add(this.dgvPassenger);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.gpError);
			this.Controls.Add(this.tc);
			this.Controls.Add(this.chkDwAll);
			this.Controls.Add(this.chkAutoSubmit);
			this.Controls.Add(this.btnSubmit);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "SubmitOrder";
			this.SubmitButton = this.btnSubmit;
			this.Text = "提交订单";
			this.Controls.SetChildIndex(this.btnSubmit, 0);
			this.Controls.SetChildIndex(this.chkAutoSubmit, 0);
			this.Controls.SetChildIndex(this.chkDwAll, 0);
			this.Controls.SetChildIndex(this.tc, 0);
			this.Controls.SetChildIndex(this.gpError, 0);
			this.Controls.SetChildIndex(this.splitContainer1, 0);
			this.Controls.SetChildIndex(this.dgvPassenger, 0);
			((System.ComponentModel.ISupportInitialize)(this.dgvPassenger)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.gpError.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ListView lstInfo;
		private System.Windows.Forms.ColumnHeader colSeatClass;
		private System.Windows.Forms.ColumnHeader colTicketCount;
		private System.Windows.Forms.ColumnHeader colTicketPrice;
		private System.Windows.Forms.DataGridView dgvPassenger;
		private DevComponents.DotNetBar.ButtonX btnSubmit;
		private System.Windows.Forms.CheckBox chkAutoSubmit;
		private Controls.Passenger.PassengerSelector ps;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ImageList imgListTicketInfo;
		private System.Windows.Forms.CheckBox chkDwAll;
		private Controls.Vc.TouchClickSimple tc;
		private DevComponents.DotNetBar.Controls.GroupPanel gpError;
		private DevComponents.DotNetBar.LabelX lblError;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvColName;
		private System.Windows.Forms.DataGridViewComboBoxColumn dgvTicketType;
		private System.Windows.Forms.DataGridViewComboBoxColumn dgvSeatType;
		private System.Windows.Forms.DataGridViewComboBoxColumn dgvSeatSubType;
		private System.Windows.Forms.DataGridViewComboBoxColumn dgvIdType;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvId;
		private System.Windows.Forms.DataGridViewTextBoxColumn dgvMobileNo;
		private System.Windows.Forms.DataGridViewCheckBoxColumn dgvSave;
		private System.Windows.Forms.DataGridViewLinkColumn dgvColAddChild;
		private System.Windows.Forms.DataGridViewLinkColumn dgvColRemove;
	}
}