namespace TOBA.UI.Controls.Common
{
	partial class TrainSelector
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
			this.lstTrain = new DevComponents.DotNetBar.Controls.ListViewEx();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.SuspendLayout();
			// 
			// lstTrain
			// 
			// 
			// 
			// 
			this.lstTrain.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lstTrain.CheckBoxes = true;
			this.lstTrain.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
			this.lstTrain.DisabledBackColor = System.Drawing.Color.Empty;
			this.lstTrain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstTrain.FullRowSelect = true;
			this.lstTrain.HideSelection = false;
			this.lstTrain.Location = new System.Drawing.Point(0, 0);
			this.lstTrain.Name = "lstTrain";
			this.lstTrain.Size = new System.Drawing.Size(520, 304);
			this.lstTrain.TabIndex = 3;
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
			// TrainSelector
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lstTrain);
			this.Name = "TrainSelector";
			this.Size = new System.Drawing.Size(520, 304);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.Controls.ListViewEx lstTrain;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
	}
}
