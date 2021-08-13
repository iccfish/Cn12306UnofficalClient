namespace TOBA.UI.Dialogs.Order
{
	using Controls.Common;

	partial class OrderSubmitDialogUiBase
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
			this.panel2 = new System.Windows.Forms.Panel();
			this.pbAnimate = new System.Windows.Forms.PictureBox();
			this.lblTimeInfo = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.lblTo = new System.Windows.Forms.Label();
			this.lblDate = new System.Windows.Forms.Label();
			this.lblTrainCode = new System.Windows.Forms.Label();
			this.lblFrom = new System.Windows.Forms.Label();
			this.ts = new TipBarMessage();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbAnimate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Window;
			this.panel1.Controls.Add(this.panel2);
			this.panel1.Controls.Add(this.pbAnimate);
			this.panel1.Controls.Add(this.lblTimeInfo);
			this.panel1.Controls.Add(this.pictureBox1);
			this.panel1.Controls.Add(this.lblTo);
			this.panel1.Controls.Add(this.lblDate);
			this.panel1.Controls.Add(this.lblTrainCode);
			this.panel1.Controls.Add(this.lblFrom);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(618, 65);
			this.panel1.TabIndex = 2;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.DarkGray;
			this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel2.Location = new System.Drawing.Point(0, 64);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(618, 1);
			this.panel2.TabIndex = 5;
			// 
			// pbAnimate
			// 
			this.pbAnimate.Image = global::TOBA.Properties.Resources.lxh_happy__2_;
			this.pbAnimate.Location = new System.Drawing.Point(0, 0);
			this.pbAnimate.Name = "pbAnimate";
			this.pbAnimate.Size = new System.Drawing.Size(65, 65);
			this.pbAnimate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbAnimate.TabIndex = 3;
			this.pbAnimate.TabStop = false;
			// 
			// lblTimeInfo
			// 
			this.lblTimeInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTimeInfo.Location = new System.Drawing.Point(489, 9);
			this.lblTimeInfo.Name = "lblTimeInfo";
			this.lblTimeInfo.Size = new System.Drawing.Size(126, 54);
			this.lblTimeInfo.TabIndex = 2;
			this.lblTimeInfo.Text = "出发 {0}\r\n到达 {1}\r\n需时 {2}";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = global::TOBA.Properties.Resources.cou_32_next;
			this.pictureBox1.Location = new System.Drawing.Point(338, 20);
			this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// lblTo
			// 
			this.lblTo.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblTo.ForeColor = System.Drawing.Color.ForestGreen;
			this.lblTo.Location = new System.Drawing.Point(372, 21);
			this.lblTo.Name = "lblTo";
			this.lblTo.Size = new System.Drawing.Size(112, 27);
			this.lblTo.TabIndex = 0;
			this.lblTo.Text = "乌鲁木齐南";
			// 
			// lblDate
			// 
			this.lblDate.AutoSize = true;
			this.lblDate.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblDate.ForeColor = System.Drawing.Color.Brown;
			this.lblDate.Location = new System.Drawing.Point(71, 21);
			this.lblDate.Name = "lblDate";
			this.lblDate.Size = new System.Drawing.Size(69, 27);
			this.lblDate.TabIndex = 0;
			this.lblDate.Text = "18-88";
			// 
			// lblTrainCode
			// 
			this.lblTrainCode.AutoSize = true;
			this.lblTrainCode.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblTrainCode.ForeColor = System.Drawing.Color.Brown;
			this.lblTrainCode.Location = new System.Drawing.Point(146, 21);
			this.lblTrainCode.Name = "lblTrainCode";
			this.lblTrainCode.Size = new System.Drawing.Size(76, 27);
			this.lblTrainCode.TabIndex = 0;
			this.lblTrainCode.Text = "D8888";
			// 
			// lblFrom
			// 
			this.lblFrom.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblFrom.ForeColor = System.Drawing.Color.ForestGreen;
			this.lblFrom.Location = new System.Drawing.Point(228, 21);
			this.lblFrom.Name = "lblFrom";
			this.lblFrom.Size = new System.Drawing.Size(112, 27);
			this.lblFrom.TabIndex = 0;
			this.lblFrom.Text = "乌鲁木齐南";
			this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ts
			// 
			this.ts.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(243)))), ((int)(((byte)(247)))));
			this.ts.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(194)))), ((int)(((byte)(241)))));
			this.ts.BorderThickness = 1;
			this.ts.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(114)))), ((int)(((byte)(138)))));
			this.ts.Image = global::TOBA.Properties.Resources._099;
			this.ts.Location = new System.Drawing.Point(0, 329);
			this.ts.Name = "ts";
			this.ts.Padding = new System.Windows.Forms.Padding(3);
			this.ts.Size = new System.Drawing.Size(618, 22);
			this.ts.TabIndex = 7;
			this.ts.TabStop = false;
			this.ts.Text = "订单快速提交";
			// 
			// OrderSubmitDialogUiBase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(618, 351);
			this.Controls.Add(this.ts);
			this.Controls.Add(this.panel1);
			this.DoubleBuffered = true;
			this.Name = "OrderSubmitDialogUiBase";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbAnimate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox pbAnimate;
		private System.Windows.Forms.Label lblTimeInfo;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label lblTo;
		private System.Windows.Forms.Label lblDate;
		private System.Windows.Forms.Label lblTrainCode;
		private System.Windows.Forms.Label lblFrom;
		private TipBarMessage ts;
		private System.Windows.Forms.Panel panel2;
	}
}
