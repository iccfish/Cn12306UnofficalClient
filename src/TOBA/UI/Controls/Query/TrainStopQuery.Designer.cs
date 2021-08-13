using TOBA.Otn.Entity;

namespace TOBA.UI.Controls.Query
{
	using System.Windows.Forms;
	using TOBA.Query.Entity;

	partial class TrainStopQuery
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
			this.dgv = new DevComponents.DotNetBar.Controls.DataGridViewX();
			this.trainStopInfoBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.pLoading = new System.Windows.Forms.Panel();
			this.lblStatus = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.pTools = new DevComponents.DotNetBar.PanelEx();
			this.pInfo = new DevComponents.DotNetBar.PanelEx();
			this.stationNoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.stationNameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.colSellTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.arriveTimeStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.departureTimeStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.stopOverTimeStringDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.trainStopInfoBindingSource)).BeginInit();
			this.pLoading.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.pTools.SuspendLayout();
			this.SuspendLayout();
			// 
			// dgv
			// 
			this.dgv.AllowUserToAddRows = false;
			this.dgv.AllowUserToDeleteRows = false;
			this.dgv.AutoGenerateColumns = false;
			this.dgv.BackgroundColor = System.Drawing.SystemColors.Window;
			this.dgv.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.dgv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
			this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.stationNoDataGridViewTextBoxColumn,
            this.stationNameDataGridViewTextBoxColumn,
            this.colSellTime,
            this.arriveTimeStringDataGridViewTextBoxColumn,
            this.departureTimeStringDataGridViewTextBoxColumn,
            this.stopOverTimeStringDataGridViewTextBoxColumn});
			this.dgv.DataSource = this.trainStopInfoBindingSource;
			dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
			dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
			dataGridViewCellStyle5.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.ControlText;
			dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
			this.dgv.DefaultCellStyle = dataGridViewCellStyle5;
			this.dgv.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgv.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
			this.dgv.HighlightSelectedColumnHeaders = false;
			this.dgv.Location = new System.Drawing.Point(0, 39);
			this.dgv.MultiSelect = false;
			this.dgv.Name = "dgv";
			this.dgv.ReadOnly = true;
			this.dgv.RowHeadersVisible = false;
			this.dgv.RowTemplate.Height = 23;
			this.dgv.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
			this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dgv.Size = new System.Drawing.Size(546, 258);
			this.dgv.TabIndex = 0;
			// 
			// trainStopInfoBindingSource
			// 
			this.trainStopInfoBindingSource.DataSource = typeof(TOBA.Query.Entity.TrainStopInfo);
			// 
			// pLoading
			// 
			this.pLoading.BackColor = System.Drawing.SystemColors.Window;
			this.pLoading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pLoading.Controls.Add(this.lblStatus);
			this.pLoading.Controls.Add(this.pictureBox1);
			this.pLoading.Location = new System.Drawing.Point(156, 118);
			this.pLoading.Name = "pLoading";
			this.pLoading.Size = new System.Drawing.Size(232, 47);
			this.pLoading.TabIndex = 1;
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Location = new System.Drawing.Point(35, 15);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(83, 12);
			this.lblStatus.TabIndex = 1;
			this.lblStatus.Text = "正在查询中...";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TOBA.Properties.Resources._16px_loading_1;
			this.pictureBox1.Location = new System.Drawing.Point(13, 14);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(16, 16);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			// 
			// pTools
			// 
			this.pTools.CanvasColor = System.Drawing.SystemColors.Control;
			this.pTools.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.pTools.DisabledBackColor = System.Drawing.Color.Empty;
			this.pTools.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pTools.Location = new System.Drawing.Point(0, 338);
			this.pTools.Name = "pTools";
			this.pTools.Size = new System.Drawing.Size(546, 43);
			this.pTools.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.pTools.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.pTools.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.pTools.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.pTools.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.pTools.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.pTools.Style.GradientAngle = 90;
			this.pTools.TabIndex = 3;
			// 
			// pInfo
			// 
			this.pInfo.CanvasColor = System.Drawing.SystemColors.Control;
			this.pInfo.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.pInfo.DisabledBackColor = System.Drawing.Color.Empty;
			this.pInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.pInfo.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pInfo.Location = new System.Drawing.Point(0, 0);
			this.pInfo.MarkupUsesStyleAlignment = true;
			this.pInfo.Name = "pInfo";
			this.pInfo.Size = new System.Drawing.Size(546, 39);
			this.pInfo.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.pInfo.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.pInfo.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.pInfo.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.pInfo.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
			this.pInfo.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.pInfo.Style.GradientAngle = 90;
			this.pInfo.TabIndex = 11;
			// 
			// stationNoDataGridViewTextBoxColumn
			// 
			this.stationNoDataGridViewTextBoxColumn.DataPropertyName = "StationNo";
			this.stationNoDataGridViewTextBoxColumn.HeaderText = "序号";
			this.stationNoDataGridViewTextBoxColumn.Name = "stationNoDataGridViewTextBoxColumn";
			this.stationNoDataGridViewTextBoxColumn.ReadOnly = true;
			this.stationNoDataGridViewTextBoxColumn.Width = 40;
			// 
			// stationNameDataGridViewTextBoxColumn
			// 
			this.stationNameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.stationNameDataGridViewTextBoxColumn.DataPropertyName = "StationName";
			dataGridViewCellStyle1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.stationNameDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
			this.stationNameDataGridViewTextBoxColumn.HeaderText = "站名";
			this.stationNameDataGridViewTextBoxColumn.Name = "stationNameDataGridViewTextBoxColumn";
			this.stationNameDataGridViewTextBoxColumn.ReadOnly = true;
			// 
			// colSellTime
			// 
			dataGridViewCellStyle2.ForeColor = System.Drawing.Color.ForestGreen;
			this.colSellTime.DefaultCellStyle = dataGridViewCellStyle2;
			this.colSellTime.HeaderText = "起售";
			this.colSellTime.Name = "colSellTime";
			this.colSellTime.ReadOnly = true;
			this.colSellTime.Width = 60;
			// 
			// arriveTimeStringDataGridViewTextBoxColumn
			// 
			this.arriveTimeStringDataGridViewTextBoxColumn.DataPropertyName = "ArriveFullTime";
			dataGridViewCellStyle3.ForeColor = System.Drawing.Color.RoyalBlue;
			dataGridViewCellStyle3.Format = "MM-dd HH:mm";
			dataGridViewCellStyle3.NullValue = "----";
			this.arriveTimeStringDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
			this.arriveTimeStringDataGridViewTextBoxColumn.HeaderText = "到达时间";
			this.arriveTimeStringDataGridViewTextBoxColumn.Name = "arriveTimeStringDataGridViewTextBoxColumn";
			this.arriveTimeStringDataGridViewTextBoxColumn.ReadOnly = true;
			this.arriveTimeStringDataGridViewTextBoxColumn.Width = 85;
			// 
			// departureTimeStringDataGridViewTextBoxColumn
			// 
			this.departureTimeStringDataGridViewTextBoxColumn.DataPropertyName = "DepartureFullTime";
			dataGridViewCellStyle4.Format = "MM-dd HH:mm";
			dataGridViewCellStyle4.NullValue = "----";
			dataGridViewCellStyle4.ForeColor = System.Drawing.Color.RoyalBlue;
			this.departureTimeStringDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle4;
			this.departureTimeStringDataGridViewTextBoxColumn.HeaderText = "出发时间";
			this.departureTimeStringDataGridViewTextBoxColumn.Name = "departureTimeStringDataGridViewTextBoxColumn";
			this.departureTimeStringDataGridViewTextBoxColumn.ReadOnly = true;
			this.departureTimeStringDataGridViewTextBoxColumn.Width = 85;
			// 
			// stopOverTimeStringDataGridViewTextBoxColumn
			// 
			this.stopOverTimeStringDataGridViewTextBoxColumn.DataPropertyName = "StopOverTimeString";
			this.stopOverTimeStringDataGridViewTextBoxColumn.HeaderText = "停留时间";
			this.stopOverTimeStringDataGridViewTextBoxColumn.Name = "stopOverTimeStringDataGridViewTextBoxColumn";
			this.stopOverTimeStringDataGridViewTextBoxColumn.ReadOnly = true;
			this.stopOverTimeStringDataGridViewTextBoxColumn.Width = 90;
			// 
			// TrainStopQuery
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Controls.Add(this.dgv);
			this.Controls.Add(this.pLoading);
			this.Controls.Add(this.pTools);
			this.Controls.Add(this.pInfo);
			this.Name = "TrainStopQuery";
			this.Size = new System.Drawing.Size(546, 381);
			((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.trainStopInfoBindingSource)).EndInit();
			this.pLoading.ResumeLayout(false);
			this.pLoading.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.pTools.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.Controls.DataGridViewX dgv;
		private System.Windows.Forms.Panel pLoading;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.BindingSource trainStopInfoBindingSource;
		private DevComponents.DotNetBar.PanelEx pTools;
		private DevComponents.DotNetBar.PanelEx pInfo;
		private DataGridViewTextBoxColumn stopOverTimeStringDataGridViewTextBoxColumn;
		private DataGridViewTextBoxColumn departureTimeStringDataGridViewTextBoxColumn;
		private DataGridViewTextBoxColumn arriveTimeStringDataGridViewTextBoxColumn;
		private DataGridViewTextBoxColumn colSellTime;
		private DataGridViewTextBoxColumn stationNameDataGridViewTextBoxColumn;
		private DataGridViewTextBoxColumn stationNoDataGridViewTextBoxColumn;
	}
}
