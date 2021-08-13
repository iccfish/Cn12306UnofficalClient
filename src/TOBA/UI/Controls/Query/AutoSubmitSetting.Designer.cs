namespace TOBA.UI.Controls.Query
{
	partial class AutoSubmitSetting
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
			this.components = new System.ComponentModel.Container();
			this.ctxPassenger = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ctxPassengerAddChild = new System.Windows.Forms.ToolStripMenuItem();
			this.ctxPassengerRemove = new System.Windows.Forms.ToolStripMenuItem();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.chkEnablePartialSubmit = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lnkSetSubType = new System.Windows.Forms.LinkLabel();
			this.lblSeatSubType = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel6 = new System.Windows.Forms.Panel();
			this.chkAutoVc = new System.Windows.Forms.CheckBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.rTrainFirst = new System.Windows.Forms.RadioButton();
			this.rSeatFirst = new System.Windows.Forms.RadioButton();
			this.label5 = new System.Windows.Forms.Label();
			this.panel5 = new System.Windows.Forms.Panel();
			this.cSeat = new System.Windows.Forms.FlowLayoutPanel();
			this.label8 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.lClearSeat = new System.Windows.Forms.LinkLabel();
			this.lAddSeat = new System.Windows.Forms.LinkLabel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.cPassenger = new System.Windows.Forms.FlowLayoutPanel();
			this.label7 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lClearPassenger = new System.Windows.Forms.LinkLabel();
			this.lAddPassenger = new System.Windows.Forms.LinkLabel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.cTrains = new System.Windows.Forms.FlowLayoutPanel();
			this.label6 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lAddTrain = new System.Windows.Forms.LinkLabel();
			this.lClearTrain = new System.Windows.Forms.LinkLabel();
			this.chkHideOtherTrains = new System.Windows.Forms.CheckBox();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panel7 = new System.Windows.Forms.Panel();
			this.chkEnableAutoSubmit = new System.Windows.Forms.CheckBox();
			this.chkAutoHb = new System.Windows.Forms.CheckBox();
			this.ctxPassenger.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panel6.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel5.SuspendLayout();
			this.cSeat.SuspendLayout();
			this.panel4.SuspendLayout();
			this.cPassenger.SuspendLayout();
			this.panel3.SuspendLayout();
			this.cTrains.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel7.SuspendLayout();
			this.SuspendLayout();
			// 
			// ctxPassenger
			// 
			this.ctxPassenger.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxPassengerAddChild,
            this.ctxPassengerRemove});
			this.ctxPassenger.Name = "ctxPassenger";
			this.ctxPassenger.Size = new System.Drawing.Size(153, 48);
			// 
			// ctxPassengerAddChild
			// 
			this.ctxPassengerAddChild.Name = "ctxPassengerAddChild";
			this.ctxPassengerAddChild.Size = new System.Drawing.Size(152, 22);
			this.ctxPassengerAddChild.Text = "添加儿童票(&C)";
			// 
			// ctxPassengerRemove
			// 
			this.ctxPassengerRemove.Name = "ctxPassengerRemove";
			this.ctxPassengerRemove.Size = new System.Drawing.Size(152, 22);
			this.ctxPassengerRemove.Text = "移除(&C)";
			// 
			// toolTip1
			// 
			this.toolTip1.AutomaticDelay = 100;
			this.toolTip1.AutoPopDelay = 10000;
			this.toolTip1.InitialDelay = 100;
			this.toolTip1.ReshowDelay = 20;
			this.toolTip1.ShowAlways = true;
			this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
			this.toolTip1.ToolTipTitle = "选项提示";
			// 
			// chkEnablePartialSubmit
			// 
			this.chkEnablePartialSubmit.AutoSize = true;
			this.chkEnablePartialSubmit.Location = new System.Drawing.Point(188, 2);
			this.chkEnablePartialSubmit.Name = "chkEnablePartialSubmit";
			this.chkEnablePartialSubmit.Size = new System.Drawing.Size(75, 21);
			this.chkEnablePartialSubmit.TabIndex = 11;
			this.chkEnablePartialSubmit.Text = "分开订票";
			this.toolTip1.SetToolTip(this.chkEnablePartialSubmit, "如果票数不足，此选项可以按照选择的顺序优选指定数目的联系人提交订单");
			this.chkEnablePartialSubmit.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.lnkSetSubType);
			this.panel1.Controls.Add(this.lblSeatSubType);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(1, 86);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(512, 20);
			this.panel1.TabIndex = 4;
			// 
			// lnkSetSubType
			// 
			this.lnkSetSubType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lnkSetSubType.AutoSize = true;
			this.lnkSetSubType.Image = global::TOBA.Properties.Resources.Tools2_16;
			this.lnkSetSubType.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lnkSetSubType.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lnkSetSubType.LinkColor = System.Drawing.Color.RoyalBlue;
			this.lnkSetSubType.Location = new System.Drawing.Point(461, 2);
			this.lnkSetSubType.Name = "lnkSetSubType";
			this.lnkSetSubType.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lnkSetSubType.Size = new System.Drawing.Size(52, 17);
			this.lnkSetSubType.TabIndex = 11;
			this.lnkSetSubType.TabStop = true;
			this.lnkSetSubType.Text = "设置";
			this.lnkSetSubType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblSeatSubType
			// 
			this.lblSeatSubType.AutoSize = true;
			this.lblSeatSubType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblSeatSubType.ForeColor = System.Drawing.Color.Crimson;
			this.lblSeatSubType.Location = new System.Drawing.Point(37, 2);
			this.lblSeatSubType.Name = "lblSeatSubType";
			this.lblSeatSubType.Size = new System.Drawing.Size(155, 17);
			this.lblSeatSubType.TabIndex = 10;
			this.lblSeatSubType.Text = "当前仅G/D/C支持选择席位";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
			this.label1.Location = new System.Drawing.Point(3, 2);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(32, 17);
			this.label1.TabIndex = 9;
			this.label1.Text = "席位";
			// 
			// panel6
			// 
			this.panel6.BackColor = System.Drawing.SystemColors.Window;
			this.panel6.Controls.Add(this.chkAutoHb);
			this.panel6.Controls.Add(this.chkAutoVc);
			this.panel6.Controls.Add(this.chkEnablePartialSubmit);
			this.panel6.Controls.Add(this.panel2);
			this.panel6.Controls.Add(this.label5);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel6.Location = new System.Drawing.Point(514, 86);
			this.panel6.Margin = new System.Windows.Forms.Padding(0);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(512, 20);
			this.panel6.TabIndex = 3;
			// 
			// chkAutoVc
			// 
			this.chkAutoVc.AutoSize = true;
			this.chkAutoVc.Location = new System.Drawing.Point(270, 2);
			this.chkAutoVc.Name = "chkAutoVc";
			this.chkAutoVc.Size = new System.Drawing.Size(87, 21);
			this.chkAutoVc.TabIndex = 19;
			this.chkAutoVc.Text = "验证码识别";
			this.chkAutoVc.UseVisualStyleBackColor = true;
			this.chkAutoVc.Visible = false;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.rTrainFirst);
			this.panel2.Controls.Add(this.rSeatFirst);
			this.panel2.Location = new System.Drawing.Point(33, 1);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(151, 18);
			this.panel2.TabIndex = 10;
			// 
			// rTrainFirst
			// 
			this.rTrainFirst.AutoSize = true;
			this.rTrainFirst.Location = new System.Drawing.Point(3, 1);
			this.rTrainFirst.Name = "rTrainFirst";
			this.rTrainFirst.Size = new System.Drawing.Size(74, 21);
			this.rTrainFirst.TabIndex = 1;
			this.rTrainFirst.TabStop = true;
			this.rTrainFirst.Text = "车次优先";
			this.rTrainFirst.UseVisualStyleBackColor = true;
			// 
			// rSeatFirst
			// 
			this.rSeatFirst.AutoSize = true;
			this.rSeatFirst.Location = new System.Drawing.Point(77, 1);
			this.rSeatFirst.Name = "rSeatFirst";
			this.rSeatFirst.Size = new System.Drawing.Size(74, 21);
			this.rSeatFirst.TabIndex = 0;
			this.rSeatFirst.TabStop = true;
			this.rSeatFirst.Text = "席别优先";
			this.rSeatFirst.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label5.ForeColor = System.Drawing.Color.RoyalBlue;
			this.label5.Location = new System.Drawing.Point(3, 4);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(32, 17);
			this.label5.TabIndex = 17;
			this.label5.Text = "优选";
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.cSeat);
			this.panel5.Controls.Add(this.label4);
			this.panel5.Controls.Add(this.lClearSeat);
			this.panel5.Controls.Add(this.lAddSeat);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel5.Location = new System.Drawing.Point(514, 55);
			this.panel5.Margin = new System.Windows.Forms.Padding(0);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(512, 30);
			this.panel5.TabIndex = 2;
			// 
			// cSeat
			// 
			this.cSeat.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cSeat.BackColor = System.Drawing.SystemColors.Window;
			this.cSeat.Controls.Add(this.label8);
			this.cSeat.Location = new System.Drawing.Point(33, 1);
			this.cSeat.Name = "cSeat";
			this.cSeat.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.cSeat.Size = new System.Drawing.Size(363, 29);
			this.cSeat.TabIndex = 7;
			// 
			// label8
			// 
			this.label8.ForeColor = System.Drawing.Color.Red;
			this.label8.Image = global::TOBA.Properties.Resources.cou_16_warning;
			this.label8.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label8.Location = new System.Drawing.Point(3, 1);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(204, 18);
			this.label8.TabIndex = 2;
			this.label8.Text = "请添加席别，否则不会自动提交！";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.ForeColor = System.Drawing.Color.Crimson;
			this.label4.Location = new System.Drawing.Point(2, 3);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 17);
			this.label4.TabIndex = 8;
			this.label4.Text = "席别";
			// 
			// lClearSeat
			// 
			this.lClearSeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lClearSeat.AutoSize = true;
			this.lClearSeat.Image = global::TOBA.Properties.Resources.cou_16_delete;
			this.lClearSeat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lClearSeat.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lClearSeat.LinkColor = System.Drawing.Color.RoyalBlue;
			this.lClearSeat.Location = new System.Drawing.Point(455, 4);
			this.lClearSeat.Name = "lClearSeat";
			this.lClearSeat.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lClearSeat.Size = new System.Drawing.Size(52, 17);
			this.lClearSeat.TabIndex = 9;
			this.lClearSeat.TabStop = true;
			this.lClearSeat.Text = "清空";
			// 
			// lAddSeat
			// 
			this.lAddSeat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lAddSeat.AutoSize = true;
			this.lAddSeat.Image = global::TOBA.Properties.Resources.cou_16_add;
			this.lAddSeat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lAddSeat.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lAddSeat.LinkColor = System.Drawing.Color.RoyalBlue;
			this.lAddSeat.Location = new System.Drawing.Point(402, 4);
			this.lAddSeat.Name = "lAddSeat";
			this.lAddSeat.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lAddSeat.Size = new System.Drawing.Size(52, 17);
			this.lAddSeat.TabIndex = 8;
			this.lAddSeat.TabStop = true;
			this.lAddSeat.Text = "添加";
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.cPassenger);
			this.panel4.Controls.Add(this.label3);
			this.panel4.Controls.Add(this.lClearPassenger);
			this.panel4.Controls.Add(this.lAddPassenger);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(1, 55);
			this.panel4.Margin = new System.Windows.Forms.Padding(0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(512, 30);
			this.panel4.TabIndex = 1;
			// 
			// cPassenger
			// 
			this.cPassenger.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cPassenger.BackColor = System.Drawing.SystemColors.Window;
			this.cPassenger.Controls.Add(this.label7);
			this.cPassenger.Location = new System.Drawing.Point(33, 1);
			this.cPassenger.Name = "cPassenger";
			this.cPassenger.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.cPassenger.Size = new System.Drawing.Size(366, 29);
			this.cPassenger.TabIndex = 4;
			// 
			// label7
			// 
			this.label7.ForeColor = System.Drawing.Color.Red;
			this.label7.Image = global::TOBA.Properties.Resources.cou_16_warning;
			this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label7.Location = new System.Drawing.Point(3, 1);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(204, 18);
			this.label7.TabIndex = 1;
			this.label7.Text = "请添加乘客，否则不会自动提交！";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.ForeColor = System.Drawing.Color.Crimson;
			this.label3.Location = new System.Drawing.Point(3, 2);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(32, 17);
			this.label3.TabIndex = 9;
			this.label3.Text = "乘客";
			// 
			// lClearPassenger
			// 
			this.lClearPassenger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lClearPassenger.AutoSize = true;
			this.lClearPassenger.Image = global::TOBA.Properties.Resources.cou_16_delete;
			this.lClearPassenger.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lClearPassenger.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lClearPassenger.LinkColor = System.Drawing.Color.RoyalBlue;
			this.lClearPassenger.Location = new System.Drawing.Point(460, 3);
			this.lClearPassenger.Name = "lClearPassenger";
			this.lClearPassenger.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lClearPassenger.Size = new System.Drawing.Size(52, 17);
			this.lClearPassenger.TabIndex = 6;
			this.lClearPassenger.TabStop = true;
			this.lClearPassenger.Text = "清空";
			// 
			// lAddPassenger
			// 
			this.lAddPassenger.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lAddPassenger.AutoSize = true;
			this.lAddPassenger.Image = global::TOBA.Properties.Resources.cou_16_add;
			this.lAddPassenger.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lAddPassenger.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lAddPassenger.LinkColor = System.Drawing.Color.RoyalBlue;
			this.lAddPassenger.Location = new System.Drawing.Point(408, 4);
			this.lAddPassenger.Name = "lAddPassenger";
			this.lAddPassenger.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lAddPassenger.Size = new System.Drawing.Size(52, 17);
			this.lAddPassenger.TabIndex = 5;
			this.lAddPassenger.TabStop = true;
			this.lAddPassenger.Text = "添加";
			// 
			// panel3
			// 
			this.tableLayoutPanel1.SetColumnSpan(this.panel3, 2);
			this.panel3.Controls.Add(this.cTrains);
			this.panel3.Controls.Add(this.label2);
			this.panel3.Controls.Add(this.lAddTrain);
			this.panel3.Controls.Add(this.lClearTrain);
			this.panel3.Controls.Add(this.chkHideOtherTrains);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel3.Location = new System.Drawing.Point(1, 1);
			this.panel3.Margin = new System.Windows.Forms.Padding(0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(1025, 53);
			this.panel3.TabIndex = 0;
			// 
			// cTrains
			// 
			this.cTrains.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cTrains.AutoScroll = true;
			this.cTrains.BackColor = System.Drawing.SystemColors.Window;
			this.cTrains.Controls.Add(this.label6);
			this.cTrains.Location = new System.Drawing.Point(33, 1);
			this.cTrains.Name = "cTrains";
			this.cTrains.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.cTrains.Size = new System.Drawing.Size(876, 49);
			this.cTrains.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.ForeColor = System.Drawing.Color.Red;
			this.label6.Image = global::TOBA.Properties.Resources.cou_16_warning;
			this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.label6.Location = new System.Drawing.Point(3, 1);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(204, 18);
			this.label6.TabIndex = 2;
			this.label6.Text = "请添加车次，否则不会自动提交！";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.ForeColor = System.Drawing.Color.Crimson;
			this.label2.Location = new System.Drawing.Point(3, 1);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(32, 17);
			this.label2.TabIndex = 10;
			this.label2.Text = "车次";
			// 
			// lAddTrain
			// 
			this.lAddTrain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lAddTrain.AutoSize = true;
			this.lAddTrain.Image = global::TOBA.Properties.Resources.cou_16_add;
			this.lAddTrain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lAddTrain.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lAddTrain.LinkColor = System.Drawing.Color.RoyalBlue;
			this.lAddTrain.Location = new System.Drawing.Point(915, 5);
			this.lAddTrain.Name = "lAddTrain";
			this.lAddTrain.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lAddTrain.Size = new System.Drawing.Size(52, 17);
			this.lAddTrain.TabIndex = 2;
			this.lAddTrain.TabStop = true;
			this.lAddTrain.Text = "添加";
			// 
			// lClearTrain
			// 
			this.lClearTrain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lClearTrain.AutoSize = true;
			this.lClearTrain.Image = global::TOBA.Properties.Resources.cou_16_delete;
			this.lClearTrain.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lClearTrain.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lClearTrain.LinkColor = System.Drawing.Color.RoyalBlue;
			this.lClearTrain.Location = new System.Drawing.Point(968, 5);
			this.lClearTrain.Name = "lClearTrain";
			this.lClearTrain.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
			this.lClearTrain.Size = new System.Drawing.Size(52, 17);
			this.lClearTrain.TabIndex = 3;
			this.lClearTrain.TabStop = true;
			this.lClearTrain.Text = "清空";
			// 
			// chkHideOtherTrains
			// 
			this.chkHideOtherTrains.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkHideOtherTrains.AutoSize = true;
			this.chkHideOtherTrains.Location = new System.Drawing.Point(914, 20);
			this.chkHideOtherTrains.Name = "chkHideOtherTrains";
			this.chkHideOtherTrains.Size = new System.Drawing.Size(99, 21);
			this.chkHideOtherTrains.TabIndex = 13;
			this.chkHideOtherTrains.Text = "隐藏其它车次";
			this.chkHideOtherTrains.UseVisualStyleBackColor = true;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.panel3, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panel4, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel5, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel6, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 29);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1027, 107);
			this.tableLayoutPanel1.TabIndex = 21;
			// 
			// panel7
			// 
			this.panel7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(143)))), ((int)(((byte)(84)))));
			this.panel7.Controls.Add(this.chkEnableAutoSubmit);
			this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel7.Location = new System.Drawing.Point(0, 0);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(1027, 29);
			this.panel7.TabIndex = 22;
			// 
			// chkEnableAutoSubmit
			// 
			this.chkEnableAutoSubmit.AutoSize = true;
			this.chkEnableAutoSubmit.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkEnableAutoSubmit.ForeColor = System.Drawing.Color.White;
			this.chkEnableAutoSubmit.Location = new System.Drawing.Point(3, 3);
			this.chkEnableAutoSubmit.Name = "chkEnableAutoSubmit";
			this.chkEnableAutoSubmit.Size = new System.Drawing.Size(116, 23);
			this.chkEnableAutoSubmit.TabIndex = 0;
			this.chkEnableAutoSubmit.Text = "开启自动下单 ";
			this.chkEnableAutoSubmit.UseVisualStyleBackColor = true;
			// 
			// chkAutoHb
			// 
			this.chkAutoHb.AutoSize = true;
			this.chkAutoHb.Location = new System.Drawing.Point(438, 1);
			this.chkAutoHb.Name = "chkAutoHb";
			this.chkAutoHb.Size = new System.Drawing.Size(75, 21);
			this.chkAutoHb.TabIndex = 21;
			this.chkAutoHb.Text = "自动候补";
			this.chkAutoHb.UseVisualStyleBackColor = true;
			// 
			// AutoSubmitSetting
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Window;
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.panel7);
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "AutoSubmitSetting";
			this.Size = new System.Drawing.Size(1027, 136);
			this.ctxPassenger.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel6.ResumeLayout(false);
			this.panel6.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.cSeat.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.cPassenger.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.cTrains.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panel7.ResumeLayout(false);
			this.panel7.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ContextMenuStrip ctxPassenger;
		private System.Windows.Forms.ToolStripMenuItem ctxPassengerAddChild;
		private System.Windows.Forms.ToolStripMenuItem ctxPassengerRemove;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.LinkLabel lnkSetSubType;
		private System.Windows.Forms.Label lblSeatSubType;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.CheckBox chkAutoVc;
		private System.Windows.Forms.CheckBox chkEnablePartialSubmit;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.RadioButton rTrainFirst;
		private System.Windows.Forms.RadioButton rSeatFirst;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.FlowLayoutPanel cSeat;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.LinkLabel lClearSeat;
		private System.Windows.Forms.LinkLabel lAddSeat;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.FlowLayoutPanel cPassenger;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.LinkLabel lClearPassenger;
		private System.Windows.Forms.LinkLabel lAddPassenger;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel cTrains;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.LinkLabel lAddTrain;
		private System.Windows.Forms.LinkLabel lClearTrain;
		private System.Windows.Forms.CheckBox chkHideOtherTrains;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.CheckBox chkEnableAutoSubmit;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox chkAutoHb;
	}
}
