namespace TOBA.UI.Dialogs.Passenger
{
	using Controls.Misc;

	partial class ImportPassenger
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lst = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imgList = new System.Windows.Forms.ImageList(this.components);
			this.btnFromCb = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnFromFile = new System.Windows.Forms.Button();
			this.loading = new Loading();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.WindowFrame;
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(736, 32);
			this.panel1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.SystemColors.Window;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(557, 12);
			this.label1.TabIndex = 1;
			this.label1.Text = "从剪贴板或文件中导入联系人，会自动跳过已经存在的联系人。已复制的联系人直接按CTRL+V即可粘贴。";
			// 
			// groupBox1
			// 
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 29);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(736, 3);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// lst
			// 
			this.lst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5});
			this.lst.FullRowSelect = true;
			this.lst.HideSelection = false;
			this.lst.Location = new System.Drawing.Point(14, 49);
			this.lst.Name = "lst";
			this.lst.Size = new System.Drawing.Size(710, 252);
			this.lst.SmallImageList = this.imgList;
			this.lst.TabIndex = 0;
			this.lst.UseCompatibleStateImageBehavior = false;
			this.lst.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "姓名";
			this.columnHeader1.Width = 155;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "类型";
			this.columnHeader2.Width = 67;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "证件类型";
			this.columnHeader3.Width = 102;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "证件号码";
			this.columnHeader4.Width = 198;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "手机号码";
			this.columnHeader5.Width = 163;
			// 
			// imgList
			// 
			this.imgList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			this.imgList.ImageSize = new System.Drawing.Size(24, 24);
			this.imgList.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btnFromCb
			// 
			this.btnFromCb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnFromCb.Image = global::TOBA.Properties.Resources.paste_plain;
			this.btnFromCb.Location = new System.Drawing.Point(138, 307);
			this.btnFromCb.Name = "btnFromCb";
			this.btnFromCb.Size = new System.Drawing.Size(118, 31);
			this.btnFromCb.TabIndex = 2;
			this.btnFromCb.Text = "粘贴联系人(&V)";
			this.btnFromCb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFromCb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnFromCb.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = global::TOBA.Properties.Resources.stop_16;
			this.btnCancel.Location = new System.Drawing.Point(606, 307);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(118, 31);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "取  消(&C)";
			this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Image = global::TOBA.Properties.Resources.tick_16;
			this.btnOk.Location = new System.Drawing.Point(482, 307);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(118, 31);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "确定导入(&O)";
			this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnFromFile
			// 
			this.btnFromFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnFromFile.Image = global::TOBA.Properties.Resources.folder_16;
			this.btnFromFile.Location = new System.Drawing.Point(14, 307);
			this.btnFromFile.Name = "btnFromFile";
			this.btnFromFile.Size = new System.Drawing.Size(118, 31);
			this.btnFromFile.TabIndex = 1;
			this.btnFromFile.Text = "打开文件(&F)";
			this.btnFromFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFromFile.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnFromFile.UseVisualStyleBackColor = true;
			// 
			// loading
			// 
			this.loading.BackColor = System.Drawing.SystemColors.Window;
			this.loading.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.loading.LoadingText = "正在导入中，请稍等....";
			this.loading.Location = new System.Drawing.Point(277, 149);
			this.loading.Name = "loading";
			this.loading.Size = new System.Drawing.Size(196, 59);
			this.loading.TabIndex = 5;
			this.loading.Visible = false;
			// 
			// ImportPassenger
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(736, 344);
			this.Controls.Add(this.loading);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnFromCb);
			this.Controls.Add(this.btnFromFile);
			this.Controls.Add(this.lst);
			this.Controls.Add(this.panel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ImportPassenger";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "导入联系人";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ListView lst;
		private System.Windows.Forms.Button btnFromFile;
		private System.Windows.Forms.Button btnFromCb;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ImageList imgList;
		private Loading loading;
	}
}