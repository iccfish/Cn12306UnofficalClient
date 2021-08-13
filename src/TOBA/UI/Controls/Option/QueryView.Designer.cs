namespace TOBA.UI.Controls.Option
{
	using Common;

	partial class QueryView
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
			this.cbGridLine = new System.Windows.Forms.ComboBox();
			this.chkHideExtraFilter = new System.Windows.Forms.CheckBox();
			this.chkDoubleClickToClose = new System.Windows.Forms.CheckBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.chkEnableAutoAddQueryParam = new System.Windows.Forms.CheckBox();
			this.chkEnableSellTip = new System.Windows.Forms.CheckBox();
			this.chkIgnoreTempClass = new System.Windows.Forms.CheckBox();
			this.ckEnablePresellTip = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.chkHideColumnIfNotSelect = new System.Windows.Forms.CheckBox();
			this.chkShowStartAndEnd = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbGridLine
			// 
			this.cbGridLine.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbGridLine.FormattingEnabled = true;
			this.cbGridLine.Items.AddRange(new object[] {
            "无分隔线",
            "水平分隔线",
            "垂直分隔线",
            "网格线"});
			this.cbGridLine.Location = new System.Drawing.Point(116, 304);
			this.cbGridLine.Name = "cbGridLine";
			this.cbGridLine.Size = new System.Drawing.Size(244, 25);
			this.cbGridLine.TabIndex = 17;
			// 
			// chkHideExtraFilter
			// 
			this.chkHideExtraFilter.AutoSize = true;
			this.chkHideExtraFilter.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkHideExtraFilter.Location = new System.Drawing.Point(17, 39);
			this.chkHideExtraFilter.Name = "chkHideExtraFilter";
			this.chkHideExtraFilter.Size = new System.Drawing.Size(147, 21);
			this.chkHideExtraFilter.TabIndex = 11;
			this.chkHideExtraFilter.Text = "查询页面隐藏高级选项";
			this.chkHideExtraFilter.UseVisualStyleBackColor = true;
			// 
			// chkDoubleClickToClose
			// 
			this.chkDoubleClickToClose.AutoSize = true;
			this.chkDoubleClickToClose.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkDoubleClickToClose.Location = new System.Drawing.Point(17, 17);
			this.chkDoubleClickToClose.Name = "chkDoubleClickToClose";
			this.chkDoubleClickToClose.Size = new System.Drawing.Size(171, 21);
			this.chkDoubleClickToClose.TabIndex = 10;
			this.chkDoubleClickToClose.Text = "双击查询标签页时关闭查询";
			this.chkDoubleClickToClose.UseVisualStyleBackColor = true;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(199, 43);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(309, 40);
			this.label9.TabIndex = 9;
			this.label9.Text = "启用这个选项会隐藏查询界面上的车次类型、过路类型、席别以及过滤选项";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(14, 307);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 17);
			this.label1.TabIndex = 16;
			this.label1.Text = "列表分隔线";
			// 
			// chkEnableAutoAddQueryParam
			// 
			this.chkEnableAutoAddQueryParam.AutoSize = true;
			this.chkEnableAutoAddQueryParam.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkEnableAutoAddQueryParam.Location = new System.Drawing.Point(17, 86);
			this.chkEnableAutoAddQueryParam.Name = "chkEnableAutoAddQueryParam";
			this.chkEnableAutoAddQueryParam.Size = new System.Drawing.Size(267, 21);
			this.chkEnableAutoAddQueryParam.TabIndex = 18;
			this.chkEnableAutoAddQueryParam.Text = "双击用户标签栏空白区域时自动创建新的查询";
			this.chkEnableAutoAddQueryParam.UseVisualStyleBackColor = true;
			// 
			// chkEnableSellTip
			// 
			this.chkEnableSellTip.AutoSize = true;
			this.chkEnableSellTip.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkEnableSellTip.Location = new System.Drawing.Point(17, 111);
			this.chkEnableSellTip.Name = "chkEnableSellTip";
			this.chkEnableSellTip.Size = new System.Drawing.Size(183, 21);
			this.chkEnableSellTip.TabIndex = 18;
			this.chkEnableSellTip.Text = "启用预售期以及起售时间提示";
			this.chkEnableSellTip.UseVisualStyleBackColor = true;
			// 
			// chkIgnoreTempClass
			// 
			this.chkIgnoreTempClass.AutoSize = true;
			this.chkIgnoreTempClass.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkIgnoreTempClass.Location = new System.Drawing.Point(17, 161);
			this.chkIgnoreTempClass.Name = "chkIgnoreTempClass";
			this.chkIgnoreTempClass.Size = new System.Drawing.Size(214, 21);
			this.chkIgnoreTempClass.TabIndex = 18;
			this.chkIgnoreTempClass.Text = "过滤时将非L开始的车次不视作临客";
			this.chkIgnoreTempClass.UseVisualStyleBackColor = true;
			// 
			// ckEnablePresellTip
			// 
			this.ckEnablePresellTip.AutoSize = true;
			this.ckEnablePresellTip.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.ckEnablePresellTip.Location = new System.Drawing.Point(17, 186);
			this.ckEnablePresellTip.Name = "ckEnablePresellTip";
			this.ckEnablePresellTip.Size = new System.Drawing.Size(163, 21);
			this.ckEnablePresellTip.TabIndex = 18;
			this.ckEnablePresellTip.Text = "启用购票建议 （专业版）";
			this.ckEnablePresellTip.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.chkDoubleClickToClose);
			this.panel1.Controls.Add(this.chkHideColumnIfNotSelect);
			this.panel1.Controls.Add(this.chkShowStartAndEnd);
			this.panel1.Controls.Add(this.ckEnablePresellTip);
			this.panel1.Controls.Add(this.label9);
			this.panel1.Controls.Add(this.chkIgnoreTempClass);
			this.panel1.Controls.Add(this.chkHideExtraFilter);
			this.panel1.Controls.Add(this.chkEnableSellTip);
			this.panel1.Controls.Add(this.chkEnableAutoAddQueryParam);
			this.panel1.Controls.Add(this.cbGridLine);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(530, 360);
			this.panel1.TabIndex = 19;
			// 
			// chkHideColumnIfNotSelect
			// 
			this.chkHideColumnIfNotSelect.AutoSize = true;
			this.chkHideColumnIfNotSelect.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkHideColumnIfNotSelect.Location = new System.Drawing.Point(17, 263);
			this.chkHideColumnIfNotSelect.Name = "chkHideColumnIfNotSelect";
			this.chkHideColumnIfNotSelect.Size = new System.Drawing.Size(363, 21);
			this.chkHideColumnIfNotSelect.TabIndex = 18;
			this.chkHideColumnIfNotSelect.Text = "如果查询条件中有席别未选择，则隐藏查询结果中的对应车票列";
			this.chkHideColumnIfNotSelect.UseVisualStyleBackColor = true;
			// 
			// chkShowStartAndEnd
			// 
			this.chkShowStartAndEnd.AutoSize = true;
			this.chkShowStartAndEnd.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.chkShowStartAndEnd.Location = new System.Drawing.Point(17, 211);
			this.chkShowStartAndEnd.Name = "chkShowStartAndEnd";
			this.chkShowStartAndEnd.Size = new System.Drawing.Size(315, 21);
			this.chkShowStartAndEnd.TabIndex = 18;
			this.chkShowStartAndEnd.Text = "如果不是始发站或终到站，则显示始发站和终到站信息";
			this.chkShowStartAndEnd.UseVisualStyleBackColor = true;
			// 
			// QueryView
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panel1);
			this.Name = "QueryView";
			this.Size = new System.Drawing.Size(530, 360);
			this.Load += new System.EventHandler(this.QueryViewConfiguration_Load);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkHideExtraFilter;
        private System.Windows.Forms.CheckBox chkDoubleClickToClose;
        private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox cbGridLine;
		private System.Windows.Forms.CheckBox chkEnableAutoAddQueryParam;
		private System.Windows.Forms.CheckBox chkEnableSellTip;
		private System.Windows.Forms.CheckBox chkIgnoreTempClass;
		private System.Windows.Forms.CheckBox ckEnablePresellTip;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox chkShowStartAndEnd;
		private System.Windows.Forms.CheckBox chkHideColumnIfNotSelect;
	}
}
