namespace TOBA.UI.Controls.Query
{
	partial class BackupOrderCart
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
			this.label1 = new System.Windows.Forms.Label();
			this.btnSubmit = new System.Windows.Forms.Button();
			this.pContainer = new System.Windows.Forms.FlowLayoutPanel();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnAutoSumit = new System.Windows.Forms.Button();
			this.lblAutoSubmitStatus = new DevComponents.DotNetBar.LabelX();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.BackColor = System.Drawing.Color.MediumVioletRed;
			this.label1.Dock = System.Windows.Forms.DockStyle.Left;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 45);
			this.label1.TabIndex = 1;
			this.label1.Text = "候补订票";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnSubmit
			// 
			this.btnSubmit.BackColor = System.Drawing.Color.White;
			this.btnSubmit.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnSubmit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnSubmit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnSubmit.ForeColor = System.Drawing.Color.Crimson;
			this.btnSubmit.Image = global::TOBA.Properties.Resources.cou_16_accept;
			this.btnSubmit.Location = new System.Drawing.Point(497, 0);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.Size = new System.Drawing.Size(73, 45);
			this.btnSubmit.TabIndex = 2;
			this.btnSubmit.Text = "提交";
			this.btnSubmit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSubmit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSubmit.UseVisualStyleBackColor = false;
			// 
			// pContainer
			// 
			this.pContainer.AutoScroll = true;
			this.pContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pContainer.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.pContainer.Location = new System.Drawing.Point(64, 0);
			this.pContainer.Name = "pContainer";
			this.pContainer.Size = new System.Drawing.Size(609, 45);
			this.pContainer.TabIndex = 3;
			// 
			// btnCancel
			// 
			this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnCancel.Image = global::TOBA.Properties.Resources.cou_16_block;
			this.btnCancel.Location = new System.Drawing.Point(673, 0);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(65, 45);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "取消";
			this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnAutoSumit
			// 
			this.btnAutoSumit.BackColor = System.Drawing.Color.White;
			this.btnAutoSumit.Dock = System.Windows.Forms.DockStyle.Right;
			this.btnAutoSumit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnAutoSumit.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnAutoSumit.ForeColor = System.Drawing.Color.RoyalBlue;
			this.btnAutoSumit.Image = global::TOBA.Properties.Resources.cou_16_refresh;
			this.btnAutoSumit.Location = new System.Drawing.Point(570, 0);
			this.btnAutoSumit.Name = "btnAutoSumit";
			this.btnAutoSumit.Size = new System.Drawing.Size(103, 45);
			this.btnAutoSumit.TabIndex = 5;
			this.btnAutoSumit.Text = "自动提交";
			this.btnAutoSumit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnAutoSumit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnAutoSumit.UseVisualStyleBackColor = false;
			// 
			// lblAutoSubmitStatus
			// 
			// 
			// 
			// 
			this.lblAutoSubmitStatus.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblAutoSubmitStatus.Dock = System.Windows.Forms.DockStyle.Right;
			this.lblAutoSubmitStatus.Location = new System.Drawing.Point(399, 0);
			this.lblAutoSubmitStatus.Name = "lblAutoSubmitStatus";
			this.lblAutoSubmitStatus.Size = new System.Drawing.Size(200, 45);
			this.lblAutoSubmitStatus.TabIndex = 0;
			this.lblAutoSubmitStatus.Text = "自动提交中...";
			this.lblAutoSubmitStatus.Visible = false;
			this.lblAutoSubmitStatus.WordWrap = true;
			// 
			// BackupOrderCart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.lblAutoSubmitStatus);
			this.Controls.Add(this.btnSubmit);
			this.Controls.Add(this.btnAutoSumit);
			this.Controls.Add(this.pContainer);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnCancel);
			this.Name = "BackupOrderCart";
			this.Size = new System.Drawing.Size(738, 45);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btnSubmit;
		private System.Windows.Forms.FlowLayoutPanel pContainer;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnAutoSumit;
		private DevComponents.DotNetBar.LabelX lblAutoSubmitStatus;
	}
}
