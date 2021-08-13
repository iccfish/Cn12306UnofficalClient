namespace TOBA.UI.Controls.Query
{
	partial class AddTrainCode
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
			this.tcMain = new System.Windows.Forms.TabControl();
			this.tabAdd = new System.Windows.Forms.TabPage();
			this.pFrq = new System.Windows.Forms.FlowLayoutPanel();
			this.lblTip = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.txtCode = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tabSelect = new System.Windows.Forms.TabPage();
			this.pQueryTip = new System.Windows.Forms.Panel();
			this.label3 = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.lstTrain = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ilList = new System.Windows.Forms.ImageList(this.components);
			this.panel3 = new System.Windows.Forms.Panel();
			this.btnCheck = new System.Windows.Forms.Button();
			this.btnCheckNone = new System.Windows.Forms.Button();
			this.btnCheckAll = new System.Windows.Forms.Button();
			this.btnCheckK = new System.Windows.Forms.Button();
			this.btnCheckC = new System.Windows.Forms.Button();
			this.btnCheckZ = new System.Windows.Forms.Button();
			this.btnCheckT = new System.Windows.Forms.Button();
			this.btnCheckD = new System.Windows.Forms.Button();
			this.btnCheckG = new System.Windows.Forms.Button();
			this.ilTab = new System.Windows.Forms.ImageList(this.components);
			this.panel4 = new System.Windows.Forms.Panel();
			this.panel5 = new System.Windows.Forms.Panel();
			this.radListAvailable = new System.Windows.Forms.RadioButton();
			this.radListAll = new System.Windows.Forms.RadioButton();
			this.tcMain.SuspendLayout();
			this.tabAdd.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.tabSelect.SuspendLayout();
			this.pQueryTip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.panel3.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel5.SuspendLayout();
			this.SuspendLayout();
			// 
			// tcMain
			// 
			this.tcMain.Controls.Add(this.tabAdd);
			this.tcMain.Controls.Add(this.tabSelect);
			this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcMain.ImageList = this.ilTab;
			this.tcMain.Location = new System.Drawing.Point(0, 0);
			this.tcMain.Name = "tcMain";
			this.tcMain.SelectedIndex = 0;
			this.tcMain.Size = new System.Drawing.Size(569, 266);
			this.tcMain.TabIndex = 0;
			// 
			// tabAdd
			// 
			this.tabAdd.Controls.Add(this.pFrq);
			this.tabAdd.Controls.Add(this.lblTip);
			this.tabAdd.Controls.Add(this.btnOk);
			this.tabAdd.Controls.Add(this.txtCode);
			this.tabAdd.Controls.Add(this.label2);
			this.tabAdd.Controls.Add(this.label1);
			this.tabAdd.Controls.Add(this.panel1);
			this.tabAdd.ImageIndex = 0;
			this.tabAdd.Location = new System.Drawing.Point(4, 23);
			this.tabAdd.Name = "tabAdd";
			this.tabAdd.Padding = new System.Windows.Forms.Padding(3);
			this.tabAdd.Size = new System.Drawing.Size(561, 239);
			this.tabAdd.TabIndex = 0;
			this.tabAdd.Text = "手动添加车次";
			this.tabAdd.UseVisualStyleBackColor = true;
			// 
			// pFrq
			// 
			this.pFrq.Location = new System.Drawing.Point(77, 108);
			this.pFrq.Name = "pFrq";
			this.pFrq.Size = new System.Drawing.Size(427, 84);
			this.pFrq.TabIndex = 13;
			// 
			// lblTip
			// 
			this.lblTip.ForeColor = System.Drawing.Color.Red;
			this.lblTip.Image = global::TOBA.Properties.Resources.block_16;
			this.lblTip.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblTip.Location = new System.Drawing.Point(449, 82);
			this.lblTip.Name = "lblTip";
			this.lblTip.Size = new System.Drawing.Size(91, 18);
			this.lblTip.TabIndex = 12;
			this.lblTip.Text = "    错误";
			this.lblTip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(183, 198);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(188, 29);
			this.btnOk.TabIndex = 11;
			this.btnOk.Text = "确定(&O)";
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// txtCode
			// 
			this.txtCode.Location = new System.Drawing.Point(77, 80);
			this.txtCode.Name = "txtCode";
			this.txtCode.Size = new System.Drawing.Size(368, 21);
			this.txtCode.TabIndex = 10;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 109);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 8;
			this.label2.Text = "常用表达式";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 85);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 9;
			this.label1.Text = "检测表达式";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(248)))), ((int)(((byte)(248)))));
			this.panel1.Controls.Add(this.linkLabel1);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(555, 68);
			this.panel1.TabIndex = 7;
			// 
			// linkLabel1
			// 
			this.linkLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(91, 5);
			this.linkLabel1.Location = new System.Drawing.Point(66, 8);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(480, 55);
			this.linkLabel1.TabIndex = 2;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "操作提示：您可以把车次直接从查询列表中『拖』到到自动预定面板里放开即可快速添加~如果拖动的是车票的席别列（每行后面显示席别票数的地方），那么还可以自动添加席别哦。" +
    "（这里输入的车次支持正则表达式……）";
			this.linkLabel1.UseCompatibleTextRendering = true;
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TOBA.Properties.Resources.onebit_20;
			this.pictureBox1.Location = new System.Drawing.Point(12, 9);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(48, 48);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.SystemColors.WindowFrame;
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.ForeColor = System.Drawing.SystemColors.WindowFrame;
			this.panel2.Location = new System.Drawing.Point(0, 67);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(555, 1);
			this.panel2.TabIndex = 0;
			// 
			// tabSelect
			// 
			this.tabSelect.Controls.Add(this.pQueryTip);
			this.tabSelect.Controls.Add(this.lstTrain);
			this.tabSelect.Controls.Add(this.panel3);
			this.tabSelect.Controls.Add(this.panel4);
			this.tabSelect.ImageIndex = 1;
			this.tabSelect.Location = new System.Drawing.Point(4, 23);
			this.tabSelect.Name = "tabSelect";
			this.tabSelect.Padding = new System.Windows.Forms.Padding(3);
			this.tabSelect.Size = new System.Drawing.Size(561, 239);
			this.tabSelect.TabIndex = 1;
			this.tabSelect.Text = "从已有车次中选择";
			this.tabSelect.UseVisualStyleBackColor = true;
			// 
			// pQueryTip
			// 
			this.pQueryTip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pQueryTip.Controls.Add(this.label3);
			this.pQueryTip.Controls.Add(this.pictureBox2);
			this.pQueryTip.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pQueryTip.Location = new System.Drawing.Point(152, 76);
			this.pQueryTip.Name = "pQueryTip";
			this.pQueryTip.Size = new System.Drawing.Size(228, 48);
			this.pQueryTip.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(37, 15);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(176, 17);
			this.label3.TabIndex = 1;
			this.label3.Text = "请至少查询一次以便于获得列表";
			// 
			// pictureBox2
			// 
			this.pictureBox2.Image = global::TOBA.Properties.Resources.cou_16_refresh;
			this.pictureBox2.Location = new System.Drawing.Point(15, 16);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(16, 16);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox2.TabIndex = 0;
			this.pictureBox2.TabStop = false;
			// 
			// lstTrain
			// 
			this.lstTrain.CheckBoxes = true;
			this.lstTrain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
			this.lstTrain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstTrain.FullRowSelect = true;
			this.lstTrain.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
			this.lstTrain.HideSelection = false;
			this.lstTrain.Location = new System.Drawing.Point(3, 31);
			this.lstTrain.Name = "lstTrain";
			this.lstTrain.Size = new System.Drawing.Size(555, 164);
			this.lstTrain.SmallImageList = this.ilList;
			this.lstTrain.TabIndex = 1;
			this.lstTrain.UseCompatibleStateImageBehavior = false;
			this.lstTrain.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "车次";
			this.columnHeader1.Width = 95;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "发站";
			this.columnHeader2.Width = 87;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "到站";
			this.columnHeader3.Width = 88;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "发车时间";
			this.columnHeader4.Width = 75;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "到站时间";
			this.columnHeader5.Width = 78;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "历时";
			this.columnHeader6.Width = 85;
			// 
			// ilList
			// 
			this.ilList.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.ilList.ImageSize = new System.Drawing.Size(1, 20);
			this.ilList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.btnCheck);
			this.panel3.Controls.Add(this.btnCheckNone);
			this.panel3.Controls.Add(this.btnCheckAll);
			this.panel3.Controls.Add(this.btnCheckK);
			this.panel3.Controls.Add(this.btnCheckC);
			this.panel3.Controls.Add(this.btnCheckZ);
			this.panel3.Controls.Add(this.btnCheckT);
			this.panel3.Controls.Add(this.btnCheckD);
			this.panel3.Controls.Add(this.btnCheckG);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel3.Location = new System.Drawing.Point(3, 195);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(555, 41);
			this.panel3.TabIndex = 0;
			// 
			// btnCheck
			// 
			this.btnCheck.Image = global::TOBA.Properties.Resources.tick_16;
			this.btnCheck.Location = new System.Drawing.Point(463, 4);
			this.btnCheck.Name = "btnCheck";
			this.btnCheck.Size = new System.Drawing.Size(89, 34);
			this.btnCheck.TabIndex = 0;
			this.btnCheck.Text = "确定";
			this.btnCheck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnCheck.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCheck.UseVisualStyleBackColor = true;
			// 
			// btnCheckNone
			// 
			this.btnCheckNone.Location = new System.Drawing.Point(318, 9);
			this.btnCheckNone.Name = "btnCheckNone";
			this.btnCheckNone.Size = new System.Drawing.Size(45, 25);
			this.btnCheckNone.TabIndex = 0;
			this.btnCheckNone.Text = "不选";
			this.btnCheckNone.UseVisualStyleBackColor = true;
			// 
			// btnCheckAll
			// 
			this.btnCheckAll.Location = new System.Drawing.Point(273, 9);
			this.btnCheckAll.Name = "btnCheckAll";
			this.btnCheckAll.Size = new System.Drawing.Size(45, 25);
			this.btnCheckAll.TabIndex = 0;
			this.btnCheckAll.Text = "全选";
			this.btnCheckAll.UseVisualStyleBackColor = true;
			// 
			// btnCheckK
			// 
			this.btnCheckK.Location = new System.Drawing.Point(228, 9);
			this.btnCheckK.Name = "btnCheckK";
			this.btnCheckK.Size = new System.Drawing.Size(45, 25);
			this.btnCheckK.TabIndex = 0;
			this.btnCheckK.Text = "快车";
			this.btnCheckK.UseVisualStyleBackColor = true;
			// 
			// btnCheckC
			// 
			this.btnCheckC.Location = new System.Drawing.Point(183, 9);
			this.btnCheckC.Name = "btnCheckC";
			this.btnCheckC.Size = new System.Drawing.Size(45, 25);
			this.btnCheckC.TabIndex = 0;
			this.btnCheckC.Text = "城铁";
			this.btnCheckC.UseVisualStyleBackColor = true;
			// 
			// btnCheckZ
			// 
			this.btnCheckZ.Location = new System.Drawing.Point(138, 9);
			this.btnCheckZ.Name = "btnCheckZ";
			this.btnCheckZ.Size = new System.Drawing.Size(45, 25);
			this.btnCheckZ.TabIndex = 0;
			this.btnCheckZ.Text = "直达";
			this.btnCheckZ.UseVisualStyleBackColor = true;
			// 
			// btnCheckT
			// 
			this.btnCheckT.Location = new System.Drawing.Point(93, 9);
			this.btnCheckT.Name = "btnCheckT";
			this.btnCheckT.Size = new System.Drawing.Size(45, 25);
			this.btnCheckT.TabIndex = 0;
			this.btnCheckT.Text = "特快";
			this.btnCheckT.UseVisualStyleBackColor = true;
			// 
			// btnCheckD
			// 
			this.btnCheckD.Location = new System.Drawing.Point(48, 9);
			this.btnCheckD.Name = "btnCheckD";
			this.btnCheckD.Size = new System.Drawing.Size(45, 25);
			this.btnCheckD.TabIndex = 0;
			this.btnCheckD.Text = "动车";
			this.btnCheckD.UseVisualStyleBackColor = true;
			// 
			// btnCheckG
			// 
			this.btnCheckG.Location = new System.Drawing.Point(3, 9);
			this.btnCheckG.Name = "btnCheckG";
			this.btnCheckG.Size = new System.Drawing.Size(45, 25);
			this.btnCheckG.TabIndex = 0;
			this.btnCheckG.Text = "高铁";
			this.btnCheckG.UseVisualStyleBackColor = true;
			// 
			// ilTab
			// 
			this.ilTab.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.ilTab.ImageSize = new System.Drawing.Size(16, 16);
			this.ilTab.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.panel5);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel4.Location = new System.Drawing.Point(3, 3);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(555, 28);
			this.panel4.TabIndex = 3;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.radListAll);
			this.panel5.Controls.Add(this.radListAvailable);
			this.panel5.Location = new System.Drawing.Point(3, 0);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(253, 27);
			this.panel5.TabIndex = 0;
			// 
			// radListAvailable
			// 
			this.radListAvailable.AutoSize = true;
			this.radListAvailable.Checked = true;
			this.radListAvailable.Location = new System.Drawing.Point(3, 3);
			this.radListAvailable.Name = "radListAvailable";
			this.radListAvailable.Size = new System.Drawing.Size(110, 21);
			this.radListAvailable.TabIndex = 0;
			this.radListAvailable.TabStop = true;
			this.radListAvailable.Text = "仅显示列表车次";
			this.radListAvailable.UseVisualStyleBackColor = true;
			// 
			// radListAll
			// 
			this.radListAll.AutoSize = true;
			this.radListAll.Location = new System.Drawing.Point(119, 3);
			this.radListAll.Name = "radListAll";
			this.radListAll.Size = new System.Drawing.Size(98, 21);
			this.radListAll.TabIndex = 1;
			this.radListAll.Text = "显示所有车次";
			this.radListAll.UseVisualStyleBackColor = true;
			// 
			// AddTrainCode
			// 
			this.BackColor = System.Drawing.SystemColors.Window;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.tcMain);
			this.Name = "AddTrainCode";
			this.Size = new System.Drawing.Size(569, 266);
			this.tcMain.ResumeLayout(false);
			this.tabAdd.ResumeLayout(false);
			this.tabAdd.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.tabSelect.ResumeLayout(false);
			this.pQueryTip.ResumeLayout(false);
			this.pQueryTip.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.panel3.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tcMain;
		private System.Windows.Forms.TabPage tabAdd;
		private System.Windows.Forms.FlowLayoutPanel pFrq;
		private System.Windows.Forms.Label lblTip;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.TabPage tabSelect;
		private System.Windows.Forms.ImageList ilTab;
		private System.Windows.Forms.ListView lstTrain;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.ImageList ilList;
		private System.Windows.Forms.Button btnCheck;
		private System.Windows.Forms.Button btnCheckK;
		private System.Windows.Forms.Button btnCheckC;
		private System.Windows.Forms.Button btnCheckZ;
		private System.Windows.Forms.Button btnCheckT;
		private System.Windows.Forms.Button btnCheckD;
		private System.Windows.Forms.Button btnCheckG;
		private System.Windows.Forms.Button btnCheckNone;
		private System.Windows.Forms.Button btnCheckAll;
		private System.Windows.Forms.Panel pQueryTip;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.RadioButton radListAll;
		private System.Windows.Forms.RadioButton radListAvailable;
	}
}