namespace TOBA.UI.Controls.Query
{
	partial class QueryParams
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
			this.dep = new TOBA.UI.Controls.Common.DateComboBox();
			this.pPassType = new System.Windows.Forms.ComboBox();
			this.pAtTo = new System.Windows.Forms.ComboBox();
			this.pDtTo = new System.Windows.Forms.ComboBox();
			this.pAtFrom = new System.Windows.Forms.ComboBox();
			this.pDtFrom = new System.Windows.Forms.ComboBox();
			this.pTo = new TOBA.UI.Controls.Query.TrainStation();
			this.pFrom = new TOBA.UI.Controls.Query.TrainStation();
			this.pTrainType = new System.Windows.Forms.Panel();
			this.checkBox9 = new System.Windows.Forms.CheckBox();
			this.lnkAllTrainType = new System.Windows.Forms.LinkLabel();
			this.checkBox7 = new System.Windows.Forms.CheckBox();
			this.checkBox8 = new System.Windows.Forms.CheckBox();
			this.checkBox6 = new System.Windows.Forms.CheckBox();
			this.checkBox3 = new System.Windows.Forms.CheckBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox5 = new System.Windows.Forms.CheckBox();
			this.ttG = new System.Windows.Forms.CheckBox();
			this.label6 = new System.Windows.Forms.Label();
			this.ddC = new System.Windows.Forms.CheckBox();
			this.pSeatType = new System.Windows.Forms.Panel();
			this.lnkAllSeatType = new System.Windows.Forms.LinkLabel();
			this.checkBox17 = new System.Windows.Forms.CheckBox();
			this.checkBox16 = new System.Windows.Forms.CheckBox();
			this.checkBox24 = new System.Windows.Forms.CheckBox();
			this.checkBox15 = new System.Windows.Forms.CheckBox();
			this.label9 = new System.Windows.Forms.Label();
			this.checkBox22 = new System.Windows.Forms.CheckBox();
			this.checkBox11 = new System.Windows.Forms.CheckBox();
			this.checkBox12 = new System.Windows.Forms.CheckBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox14 = new System.Windows.Forms.CheckBox();
			this.checkBox23 = new System.Windows.Forms.CheckBox();
			this.checkBox13 = new System.Windows.Forms.CheckBox();
			this.checkBox10 = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.chkHideNoValid = new System.Windows.Forms.CheckBox();
			this.chkHideToNotSame = new System.Windows.Forms.CheckBox();
			this.label10 = new System.Windows.Forms.Label();
			this.chkHideFromNotSame = new System.Windows.Forms.CheckBox();
			this.chkHideNoTicket = new System.Windows.Forms.CheckBox();
			this.btnExchange = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btnQuery = new System.Windows.Forms.Button();
			this.pStu = new System.Windows.Forms.CheckBox();
			this.pAutoRefresh = new System.Windows.Forms.CheckBox();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.pbPrev = new System.Windows.Forms.PictureBox();
			this.pbNext = new System.Windows.Forms.PictureBox();
			this.pTrainType.SuspendLayout();
			this.pSeatType.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbPrev)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbNext)).BeginInit();
			this.SuspendLayout();
			// 
			// dep
			// 
			this.dep.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.dep.FormattingEnabled = true;
			this.dep.Location = new System.Drawing.Point(184, 7);
			this.dep.Name = "dep";
			this.dep.Size = new System.Drawing.Size(250, 25);
			this.dep.TabIndex = 13;
			// 
			// pPassType
			// 
			this.pPassType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pPassType.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.pPassType.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pPassType.FormattingEnabled = true;
			this.pPassType.Items.AddRange(new object[] {
            "所有过路类型",
            "始发终到",
            "始发车",
            "终到车",
            "始发不终到",
            "终到不始发",
            "不始发不终到"});
			this.pPassType.Location = new System.Drawing.Point(513, 0);
			this.pPassType.Margin = new System.Windows.Forms.Padding(0);
			this.pPassType.Name = "pPassType";
			this.pPassType.Size = new System.Drawing.Size(96, 25);
			this.pPassType.TabIndex = 9;
			// 
			// pAtTo
			// 
			this.pAtTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pAtTo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pAtTo.FormattingEnabled = true;
			this.pAtTo.ItemHeight = 17;
			this.pAtTo.Location = new System.Drawing.Point(661, 7);
			this.pAtTo.Margin = new System.Windows.Forms.Padding(0);
			this.pAtTo.MaxDropDownItems = 24;
			this.pAtTo.Name = "pAtTo";
			this.pAtTo.Size = new System.Drawing.Size(50, 25);
			this.pAtTo.TabIndex = 7;
			// 
			// pDtTo
			// 
			this.pDtTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pDtTo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pDtTo.FormattingEnabled = true;
			this.pDtTo.ItemHeight = 17;
			this.pDtTo.Location = new System.Drawing.Point(530, 7);
			this.pDtTo.Margin = new System.Windows.Forms.Padding(0);
			this.pDtTo.MaxDropDownItems = 24;
			this.pDtTo.Name = "pDtTo";
			this.pDtTo.Size = new System.Drawing.Size(50, 25);
			this.pDtTo.TabIndex = 5;
			// 
			// pAtFrom
			// 
			this.pAtFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pAtFrom.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pAtFrom.FormattingEnabled = true;
			this.pAtFrom.ItemHeight = 17;
			this.pAtFrom.Location = new System.Drawing.Point(600, 7);
			this.pAtFrom.Margin = new System.Windows.Forms.Padding(0);
			this.pAtFrom.MaxDropDownItems = 24;
			this.pAtFrom.Name = "pAtFrom";
			this.pAtFrom.Size = new System.Drawing.Size(50, 25);
			this.pAtFrom.TabIndex = 6;
			// 
			// pDtFrom
			// 
			this.pDtFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.pDtFrom.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pDtFrom.FormattingEnabled = true;
			this.pDtFrom.ItemHeight = 17;
			this.pDtFrom.Location = new System.Drawing.Point(472, 7);
			this.pDtFrom.Margin = new System.Windows.Forms.Padding(0);
			this.pDtFrom.MaxDropDownItems = 24;
			this.pDtFrom.Name = "pDtFrom";
			this.pDtFrom.Size = new System.Drawing.Size(50, 25);
			this.pDtFrom.TabIndex = 4;
			// 
			// pTo
			// 
			this.pTo.Code = null;
			this.pTo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pTo.ForeColor = System.Drawing.Color.RoyalBlue;
			this.pTo.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.pTo.Location = new System.Drawing.Point(99, 8);
			this.pTo.Margin = new System.Windows.Forms.Padding(0);
			this.pTo.Name = "pTo";
			this.pTo.Size = new System.Drawing.Size(68, 23);
			this.pTo.StationType = null;
			this.pTo.TabIndex = 2;
			this.pTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// pFrom
			// 
			this.pFrom.Code = null;
			this.pFrom.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pFrom.ForeColor = System.Drawing.Color.RoyalBlue;
			this.pFrom.ImeMode = System.Windows.Forms.ImeMode.Disable;
			this.pFrom.Location = new System.Drawing.Point(5, 8);
			this.pFrom.Margin = new System.Windows.Forms.Padding(0);
			this.pFrom.Name = "pFrom";
			this.pFrom.Size = new System.Drawing.Size(68, 23);
			this.pFrom.StationType = null;
			this.pFrom.TabIndex = 1;
			this.pFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// pTrainType
			// 
			this.pTrainType.BackColor = System.Drawing.Color.WhiteSmoke;
			this.pTrainType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pTrainType.Controls.Add(this.checkBox9);
			this.pTrainType.Controls.Add(this.lnkAllTrainType);
			this.pTrainType.Controls.Add(this.checkBox7);
			this.pTrainType.Controls.Add(this.checkBox8);
			this.pTrainType.Controls.Add(this.checkBox6);
			this.pTrainType.Controls.Add(this.checkBox3);
			this.pTrainType.Controls.Add(this.pPassType);
			this.pTrainType.Controls.Add(this.checkBox4);
			this.pTrainType.Controls.Add(this.checkBox5);
			this.pTrainType.Controls.Add(this.ttG);
			this.pTrainType.Controls.Add(this.label6);
			this.pTrainType.Controls.Add(this.ddC);
			this.pTrainType.Location = new System.Drawing.Point(5, 37);
			this.pTrainType.Margin = new System.Windows.Forms.Padding(0);
			this.pTrainType.Name = "pTrainType";
			this.pTrainType.Size = new System.Drawing.Size(645, 26);
			this.pTrainType.TabIndex = 10;
			// 
			// checkBox9
			// 
			this.checkBox9.AutoSize = true;
			this.checkBox9.Location = new System.Drawing.Point(418, 5);
			this.checkBox9.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox9.Name = "checkBox9";
			this.checkBox9.Size = new System.Drawing.Size(48, 16);
			this.checkBox9.TabIndex = 8;
			this.checkBox9.Tag = "*";
			this.checkBox9.Text = "其它";
			this.checkBox9.UseVisualStyleBackColor = true;
			// 
			// lnkAllTrainType
			// 
			this.lnkAllTrainType.AutoSize = true;
			this.lnkAllTrainType.BackColor = System.Drawing.Color.Transparent;
			this.lnkAllTrainType.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lnkAllTrainType.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.lnkAllTrainType.Location = new System.Drawing.Point(611, 7);
			this.lnkAllTrainType.Margin = new System.Windows.Forms.Padding(0);
			this.lnkAllTrainType.Name = "lnkAllTrainType";
			this.lnkAllTrainType.Size = new System.Drawing.Size(29, 12);
			this.lnkAllTrainType.TabIndex = 10;
			this.lnkAllTrainType.TabStop = true;
			this.lnkAllTrainType.Text = "全选";
			// 
			// checkBox7
			// 
			this.checkBox7.AutoSize = true;
			this.checkBox7.Location = new System.Drawing.Point(370, 5);
			this.checkBox7.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox7.Name = "checkBox7";
			this.checkBox7.Size = new System.Drawing.Size(48, 16);
			this.checkBox7.TabIndex = 6;
			this.checkBox7.Tag = "L";
			this.checkBox7.Text = "临客";
			this.checkBox7.UseVisualStyleBackColor = true;
			// 
			// checkBox8
			// 
			this.checkBox8.AutoSize = true;
			this.checkBox8.Location = new System.Drawing.Point(322, 5);
			this.checkBox8.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox8.Name = "checkBox8";
			this.checkBox8.Size = new System.Drawing.Size(48, 16);
			this.checkBox8.TabIndex = 7;
			this.checkBox8.Tag = "P";
			this.checkBox8.Text = "普客";
			this.checkBox8.UseVisualStyleBackColor = true;
			// 
			// checkBox6
			// 
			this.checkBox6.AutoSize = true;
			this.checkBox6.Location = new System.Drawing.Point(178, 5);
			this.checkBox6.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox6.Name = "checkBox6";
			this.checkBox6.Size = new System.Drawing.Size(48, 16);
			this.checkBox6.TabIndex = 5;
			this.checkBox6.Tag = "T";
			this.checkBox6.Text = "特快";
			this.checkBox6.UseVisualStyleBackColor = true;
			// 
			// checkBox3
			// 
			this.checkBox3.AutoSize = true;
			this.checkBox3.Location = new System.Drawing.Point(82, 5);
			this.checkBox3.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox3.Name = "checkBox3";
			this.checkBox3.Size = new System.Drawing.Size(48, 16);
			this.checkBox3.TabIndex = 2;
			this.checkBox3.Tag = "C";
			this.checkBox3.Text = "城铁";
			this.checkBox3.UseVisualStyleBackColor = true;
			// 
			// checkBox4
			// 
			this.checkBox4.AutoSize = true;
			this.checkBox4.Location = new System.Drawing.Point(274, 5);
			this.checkBox4.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(48, 16);
			this.checkBox4.TabIndex = 3;
			this.checkBox4.Tag = "K";
			this.checkBox4.Text = "快车";
			this.checkBox4.UseVisualStyleBackColor = true;
			// 
			// checkBox5
			// 
			this.checkBox5.AutoSize = true;
			this.checkBox5.Location = new System.Drawing.Point(226, 5);
			this.checkBox5.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox5.Name = "checkBox5";
			this.checkBox5.Size = new System.Drawing.Size(48, 16);
			this.checkBox5.TabIndex = 4;
			this.checkBox5.Tag = "Z";
			this.checkBox5.Text = "直达";
			this.checkBox5.UseVisualStyleBackColor = true;
			// 
			// ttG
			// 
			this.ttG.AutoSize = true;
			this.ttG.Location = new System.Drawing.Point(36, 5);
			this.ttG.Margin = new System.Windows.Forms.Padding(0);
			this.ttG.Name = "ttG";
			this.ttG.Size = new System.Drawing.Size(48, 16);
			this.ttG.TabIndex = 0;
			this.ttG.Tag = "G";
			this.ttG.Text = "高铁";
			this.ttG.UseVisualStyleBackColor = true;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label6.Location = new System.Drawing.Point(3, 6);
			this.label6.Margin = new System.Windows.Forms.Padding(0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(31, 12);
			this.label6.TabIndex = 0;
			this.label6.Text = "车型";
			// 
			// ddC
			// 
			this.ddC.AutoSize = true;
			this.ddC.Location = new System.Drawing.Point(130, 5);
			this.ddC.Margin = new System.Windows.Forms.Padding(0);
			this.ddC.Name = "ddC";
			this.ddC.Size = new System.Drawing.Size(48, 16);
			this.ddC.TabIndex = 1;
			this.ddC.Tag = "D";
			this.ddC.Text = "动车";
			this.ddC.UseVisualStyleBackColor = true;
			// 
			// pSeatType
			// 
			this.pSeatType.BackColor = System.Drawing.Color.WhiteSmoke;
			this.pSeatType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pSeatType.Controls.Add(this.lnkAllSeatType);
			this.pSeatType.Controls.Add(this.checkBox17);
			this.pSeatType.Controls.Add(this.checkBox16);
			this.pSeatType.Controls.Add(this.checkBox24);
			this.pSeatType.Controls.Add(this.checkBox15);
			this.pSeatType.Controls.Add(this.label9);
			this.pSeatType.Controls.Add(this.checkBox22);
			this.pSeatType.Controls.Add(this.checkBox11);
			this.pSeatType.Controls.Add(this.checkBox12);
			this.pSeatType.Controls.Add(this.checkBox1);
			this.pSeatType.Controls.Add(this.checkBox14);
			this.pSeatType.Controls.Add(this.checkBox23);
			this.pSeatType.Controls.Add(this.checkBox13);
			this.pSeatType.Controls.Add(this.checkBox10);
			this.pSeatType.Location = new System.Drawing.Point(5, 62);
			this.pSeatType.Margin = new System.Windows.Forms.Padding(0);
			this.pSeatType.Name = "pSeatType";
			this.pSeatType.Size = new System.Drawing.Size(645, 26);
			this.pSeatType.TabIndex = 11;
			// 
			// lnkAllSeatType
			// 
			this.lnkAllSeatType.AutoSize = true;
			this.lnkAllSeatType.BackColor = System.Drawing.Color.Transparent;
			this.lnkAllSeatType.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lnkAllSeatType.LinkColor = System.Drawing.SystemColors.HotTrack;
			this.lnkAllSeatType.Location = new System.Drawing.Point(611, 6);
			this.lnkAllSeatType.Margin = new System.Windows.Forms.Padding(0);
			this.lnkAllSeatType.Name = "lnkAllSeatType";
			this.lnkAllSeatType.Size = new System.Drawing.Size(29, 12);
			this.lnkAllSeatType.TabIndex = 10;
			this.lnkAllSeatType.TabStop = true;
			this.lnkAllSeatType.Text = "全选";
			// 
			// checkBox17
			// 
			this.checkBox17.AutoSize = true;
			this.checkBox17.Location = new System.Drawing.Point(561, 5);
			this.checkBox17.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox17.Name = "checkBox17";
			this.checkBox17.Size = new System.Drawing.Size(48, 16);
			this.checkBox17.TabIndex = 11;
			this.checkBox17.Tag = "*";
			this.checkBox17.Text = "其它";
			this.checkBox17.UseVisualStyleBackColor = true;
			// 
			// checkBox16
			// 
			this.checkBox16.AutoSize = true;
			this.checkBox16.Location = new System.Drawing.Point(511, 4);
			this.checkBox16.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox16.Name = "checkBox16";
			this.checkBox16.Size = new System.Drawing.Size(48, 16);
			this.checkBox16.TabIndex = 10;
			this.checkBox16.Tag = "0";
			this.checkBox16.Text = "无座";
			this.checkBox16.UseVisualStyleBackColor = true;
			// 
			// checkBox24
			// 
			this.checkBox24.AutoSize = true;
			this.checkBox24.Location = new System.Drawing.Point(417, 5);
			this.checkBox24.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox24.Name = "checkBox24";
			this.checkBox24.Size = new System.Drawing.Size(48, 16);
			this.checkBox24.TabIndex = 8;
			this.checkBox24.Tag = "2";
			this.checkBox24.Text = "软座";
			this.checkBox24.UseVisualStyleBackColor = true;
			// 
			// checkBox15
			// 
			this.checkBox15.AutoSize = true;
			this.checkBox15.Location = new System.Drawing.Point(465, 5);
			this.checkBox15.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox15.Name = "checkBox15";
			this.checkBox15.Size = new System.Drawing.Size(48, 16);
			this.checkBox15.TabIndex = 9;
			this.checkBox15.Tag = "1";
			this.checkBox15.Text = "硬座";
			this.checkBox15.UseVisualStyleBackColor = true;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label9.Location = new System.Drawing.Point(2, 6);
			this.label9.Margin = new System.Windows.Forms.Padding(0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(31, 12);
			this.label9.TabIndex = 0;
			this.label9.Text = "席别";
			// 
			// checkBox22
			// 
			this.checkBox22.AutoSize = true;
			this.checkBox22.Location = new System.Drawing.Point(81, 5);
			this.checkBox22.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox22.Name = "checkBox22";
			this.checkBox22.Size = new System.Drawing.Size(48, 16);
			this.checkBox22.TabIndex = 2;
			this.checkBox22.Tag = "P";
			this.checkBox22.Text = "特等";
			this.checkBox22.UseVisualStyleBackColor = true;
			// 
			// checkBox11
			// 
			this.checkBox11.AutoSize = true;
			this.checkBox11.Location = new System.Drawing.Point(130, 5);
			this.checkBox11.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox11.Name = "checkBox11";
			this.checkBox11.Size = new System.Drawing.Size(48, 16);
			this.checkBox11.TabIndex = 3;
			this.checkBox11.Tag = "M";
			this.checkBox11.Text = "一等";
			this.checkBox11.UseVisualStyleBackColor = true;
			// 
			// checkBox12
			// 
			this.checkBox12.AutoSize = true;
			this.checkBox12.Location = new System.Drawing.Point(178, 5);
			this.checkBox12.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox12.Name = "checkBox12";
			this.checkBox12.Size = new System.Drawing.Size(48, 16);
			this.checkBox12.TabIndex = 4;
			this.checkBox12.Tag = "O";
			this.checkBox12.Text = "二等";
			this.checkBox12.UseVisualStyleBackColor = true;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(369, 5);
			this.checkBox1.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(48, 16);
			this.checkBox1.TabIndex = 7;
			this.checkBox1.Tag = "F";
			this.checkBox1.Text = "动卧";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// checkBox14
			// 
			this.checkBox14.AutoSize = true;
			this.checkBox14.Location = new System.Drawing.Point(322, 5);
			this.checkBox14.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox14.Name = "checkBox14";
			this.checkBox14.Size = new System.Drawing.Size(48, 16);
			this.checkBox14.TabIndex = 7;
			this.checkBox14.Tag = "3";
			this.checkBox14.Text = "硬卧";
			this.checkBox14.UseVisualStyleBackColor = true;
			// 
			// checkBox23
			// 
			this.checkBox23.AutoSize = true;
			this.checkBox23.Location = new System.Drawing.Point(226, 5);
			this.checkBox23.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox23.Name = "checkBox23";
			this.checkBox23.Size = new System.Drawing.Size(48, 16);
			this.checkBox23.TabIndex = 5;
			this.checkBox23.Tag = "6";
			this.checkBox23.Text = "高软";
			this.checkBox23.UseVisualStyleBackColor = true;
			// 
			// checkBox13
			// 
			this.checkBox13.AutoSize = true;
			this.checkBox13.Location = new System.Drawing.Point(274, 5);
			this.checkBox13.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox13.Name = "checkBox13";
			this.checkBox13.Size = new System.Drawing.Size(48, 16);
			this.checkBox13.TabIndex = 6;
			this.checkBox13.Tag = "4";
			this.checkBox13.Text = "软卧";
			this.checkBox13.UseVisualStyleBackColor = true;
			// 
			// checkBox10
			// 
			this.checkBox10.AutoSize = true;
			this.checkBox10.Location = new System.Drawing.Point(36, 5);
			this.checkBox10.Margin = new System.Windows.Forms.Padding(0);
			this.checkBox10.Name = "checkBox10";
			this.checkBox10.Size = new System.Drawing.Size(48, 16);
			this.checkBox10.TabIndex = 1;
			this.checkBox10.Tag = "9";
			this.checkBox10.Text = "商务";
			this.checkBox10.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.chkHideNoValid);
			this.panel1.Controls.Add(this.chkHideToNotSame);
			this.panel1.Controls.Add(this.label10);
			this.panel1.Controls.Add(this.chkHideFromNotSame);
			this.panel1.Controls.Add(this.chkHideNoTicket);
			this.panel1.Location = new System.Drawing.Point(649, 37);
			this.panel1.Margin = new System.Windows.Forms.Padding(0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(168, 51);
			this.panel1.TabIndex = 12;
			// 
			// chkHideNoValid
			// 
			this.chkHideNoValid.AutoSize = true;
			this.chkHideNoValid.Location = new System.Drawing.Point(24, 31);
			this.chkHideNoValid.Margin = new System.Windows.Forms.Padding(0);
			this.chkHideNoValid.Name = "chkHideNoValid";
			this.chkHideNoValid.Size = new System.Drawing.Size(72, 16);
			this.chkHideNoValid.TabIndex = 2;
			this.chkHideNoValid.Text = "席别未选";
			this.chkHideNoValid.UseVisualStyleBackColor = true;
			// 
			// chkHideToNotSame
			// 
			this.chkHideToNotSame.AutoSize = true;
			this.chkHideToNotSame.Location = new System.Drawing.Point(96, 31);
			this.chkHideToNotSame.Margin = new System.Windows.Forms.Padding(0);
			this.chkHideToNotSame.Name = "chkHideToNotSame";
			this.chkHideToNotSame.Size = new System.Drawing.Size(72, 16);
			this.chkHideToNotSame.TabIndex = 3;
			this.chkHideToNotSame.Text = "到站不同";
			this.chkHideToNotSame.UseVisualStyleBackColor = true;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.label10.Location = new System.Drawing.Point(5, 7);
			this.label10.Margin = new System.Windows.Forms.Padding(0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(18, 36);
			this.label10.TabIndex = 4;
			this.label10.Text = "隐\r\n\r\n藏";
			// 
			// chkHideFromNotSame
			// 
			this.chkHideFromNotSame.AutoSize = true;
			this.chkHideFromNotSame.Location = new System.Drawing.Point(96, 7);
			this.chkHideFromNotSame.Margin = new System.Windows.Forms.Padding(0);
			this.chkHideFromNotSame.Name = "chkHideFromNotSame";
			this.chkHideFromNotSame.Size = new System.Drawing.Size(72, 16);
			this.chkHideFromNotSame.TabIndex = 1;
			this.chkHideFromNotSame.Text = "发站不同";
			this.chkHideFromNotSame.UseVisualStyleBackColor = true;
			// 
			// chkHideNoTicket
			// 
			this.chkHideNoTicket.AutoSize = true;
			this.chkHideNoTicket.Location = new System.Drawing.Point(24, 7);
			this.chkHideNoTicket.Margin = new System.Windows.Forms.Padding(0);
			this.chkHideNoTicket.Name = "chkHideNoTicket";
			this.chkHideNoTicket.Size = new System.Drawing.Size(48, 16);
			this.chkHideNoTicket.TabIndex = 0;
			this.chkHideNoTicket.Text = "无票";
			this.chkHideNoTicket.UseVisualStyleBackColor = true;
			// 
			// btnExchange
			// 
			this.btnExchange.FlatAppearance.BorderColor = System.Drawing.SystemColors.WindowFrame;
			this.btnExchange.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnExchange.Image = global::TOBA.Properties.Resources.xfsm_switch;
			this.btnExchange.Location = new System.Drawing.Point(72, 8);
			this.btnExchange.Margin = new System.Windows.Forms.Padding(0);
			this.btnExchange.Name = "btnExchange";
			this.btnExchange.Size = new System.Drawing.Size(28, 23);
			this.btnExchange.TabIndex = 12;
			this.btnExchange.TabStop = false;
			this.btnExchange.UseVisualStyleBackColor = true;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(650, 14);
			this.label5.Margin = new System.Windows.Forms.Padding(0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(11, 12);
			this.label5.TabIndex = 4;
			this.label5.Text = "-";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(451, 13);
			this.label4.Margin = new System.Windows.Forms.Padding(0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(17, 12);
			this.label4.TabIndex = 0;
			this.label4.Text = "发";
			// 
			// btnQuery
			// 
			this.btnQuery.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnQuery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnQuery.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnQuery.ForeColor = System.Drawing.Color.RoyalBlue;
			this.btnQuery.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnQuery.Location = new System.Drawing.Point(831, 10);
			this.btnQuery.Margin = new System.Windows.Forms.Padding(0);
			this.btnQuery.Name = "btnQuery";
			this.btnQuery.Size = new System.Drawing.Size(94, 79);
			this.btnQuery.TabIndex = 0;
			this.btnQuery.Text = "查 询\r\n余 票";
			this.btnQuery.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnQuery.UseVisualStyleBackColor = true;
			// 
			// pStu
			// 
			this.pStu.AutoSize = true;
			this.pStu.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pStu.ForeColor = System.Drawing.Color.Green;
			this.pStu.Location = new System.Drawing.Point(713, 12);
			this.pStu.Margin = new System.Windows.Forms.Padding(0);
			this.pStu.Name = "pStu";
			this.pStu.Size = new System.Drawing.Size(63, 16);
			this.pStu.TabIndex = 8;
			this.pStu.Text = "学生票";
			this.pStu.UseVisualStyleBackColor = true;
			// 
			// pAutoRefresh
			// 
			this.pAutoRefresh.AutoSize = true;
			this.pAutoRefresh.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pAutoRefresh.ForeColor = System.Drawing.Color.Purple;
			this.pAutoRefresh.Location = new System.Drawing.Point(776, 12);
			this.pAutoRefresh.Margin = new System.Windows.Forms.Padding(0);
			this.pAutoRefresh.Name = "pAutoRefresh";
			this.pAutoRefresh.Size = new System.Drawing.Size(50, 16);
			this.pAutoRefresh.TabIndex = 9;
			this.pAutoRefresh.Text = "刷票";
			this.pAutoRefresh.UseVisualStyleBackColor = true;
			this.pAutoRefresh.Visible = false;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(521, 13);
			this.label8.Margin = new System.Windows.Forms.Padding(0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(11, 12);
			this.label8.TabIndex = 4;
			this.label8.Text = "-";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(581, 13);
			this.label7.Margin = new System.Windows.Forms.Padding(0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(17, 12);
			this.label7.TabIndex = 0;
			this.label7.Text = "到";
			// 
			// pbPrev
			// 
			this.pbPrev.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbPrev.Image = global::TOBA.Properties.Resources._132;
			this.pbPrev.Location = new System.Drawing.Point(168, 12);
			this.pbPrev.Name = "pbPrev";
			this.pbPrev.Size = new System.Drawing.Size(16, 16);
			this.pbPrev.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbPrev.TabIndex = 12;
			this.pbPrev.TabStop = false;
			// 
			// pbNext
			// 
			this.pbNext.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbNext.Image = global::TOBA.Properties.Resources._131;
			this.pbNext.Location = new System.Drawing.Point(434, 12);
			this.pbNext.Name = "pbNext";
			this.pbNext.Size = new System.Drawing.Size(16, 16);
			this.pbNext.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbNext.TabIndex = 12;
			this.pbNext.TabStop = false;
			// 
			// QueryParams
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pbNext);
			this.Controls.Add(this.pbPrev);
			this.Controls.Add(this.dep);
			this.Controls.Add(this.pTrainType);
			this.Controls.Add(this.pSeatType);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.btnExchange);
			this.Controls.Add(this.pAtTo);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.btnQuery);
			this.Controls.Add(this.pDtTo);
			this.Controls.Add(this.pAtFrom);
			this.Controls.Add(this.pDtFrom);
			this.Controls.Add(this.pStu);
			this.Controls.Add(this.pAutoRefresh);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.pTo);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.pFrom);
			this.Controls.Add(this.label5);
			this.DoubleBuffered = true;
			this.Margin = new System.Windows.Forms.Padding(0);
			this.Name = "QueryParams";
			this.Size = new System.Drawing.Size(933, 95);
			this.pTrainType.ResumeLayout(false);
			this.pTrainType.PerformLayout();
			this.pSeatType.ResumeLayout(false);
			this.pSeatType.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbPrev)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbNext)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private TrainStation pFrom;
		private TrainStation pTo;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.CheckBox ttG;
		private System.Windows.Forms.CheckBox ddC;
		private System.Windows.Forms.CheckBox checkBox3;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox5;
		private System.Windows.Forms.CheckBox checkBox6;
		private System.Windows.Forms.CheckBox checkBox7;
		private System.Windows.Forms.CheckBox checkBox8;
		private System.Windows.Forms.CheckBox checkBox9;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.CheckBox checkBox10;
		private System.Windows.Forms.CheckBox checkBox11;
		private System.Windows.Forms.CheckBox checkBox12;
		private System.Windows.Forms.CheckBox checkBox13;
		private System.Windows.Forms.CheckBox checkBox14;
		private System.Windows.Forms.CheckBox checkBox15;
		private System.Windows.Forms.CheckBox checkBox16;
		private System.Windows.Forms.CheckBox checkBox17;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox chkHideNoTicket;
		private System.Windows.Forms.CheckBox chkHideNoValid;
		private System.Windows.Forms.CheckBox chkHideFromNotSame;
		private System.Windows.Forms.CheckBox chkHideToNotSame;
		private System.Windows.Forms.CheckBox checkBox22;
		private System.Windows.Forms.CheckBox checkBox23;
		private System.Windows.Forms.CheckBox checkBox24;
		private System.Windows.Forms.ComboBox pDtFrom;
		private System.Windows.Forms.ComboBox pDtTo;
		private System.Windows.Forms.ComboBox pAtFrom;
		private System.Windows.Forms.ComboBox pAtTo;
		private System.Windows.Forms.CheckBox pAutoRefresh;
		private System.Windows.Forms.Button btnQuery;
		private System.Windows.Forms.Panel pTrainType;
		private System.Windows.Forms.Panel pSeatType;
		private System.Windows.Forms.CheckBox pStu;
		private System.Windows.Forms.ComboBox pPassType;
		private System.Windows.Forms.Button btnExchange;
		private System.Windows.Forms.LinkLabel lnkAllTrainType;
		private System.Windows.Forms.LinkLabel lnkAllSeatType;
		private System.Windows.Forms.Label label10;
		private Common.DateComboBox dep;
		private System.Windows.Forms.PictureBox pbPrev;
		private System.Windows.Forms.PictureBox pbNext;
		private System.Windows.Forms.CheckBox checkBox1;
	}
}
