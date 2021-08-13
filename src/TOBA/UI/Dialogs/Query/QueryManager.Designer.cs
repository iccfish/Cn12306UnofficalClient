namespace TOBA.UI.Dialogs.Query
{
	partial class QueryManager
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
			this.lstQuery = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lstQuery
			// 
			this.lstQuery.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
			this.lstQuery.FullRowSelect = true;
			this.lstQuery.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.lstQuery.HideSelection = false;
			this.lstQuery.Location = new System.Drawing.Point(12, 12);
			this.lstQuery.Name = "lstQuery";
			this.lstQuery.Size = new System.Drawing.Size(380, 197);
			this.lstQuery.SmallImageList = this.imageList1;
			this.lstQuery.TabIndex = 0;
			this.lstQuery.UseCompatibleStateImageBehavior = false;
			this.lstQuery.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 365;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(1, 25);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDelete.Image = global::TOBA.Properties.Resources.trash_16;
			this.btnDelete.Location = new System.Drawing.Point(12, 224);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(95, 30);
			this.btnDelete.TabIndex = 1;
			this.btnDelete.Text = "删除(&D)";
			this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.button1_Click);
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Image = global::TOBA.Properties.Resources.tick_16;
			this.btnOk.Location = new System.Drawing.Point(297, 224);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(95, 30);
			this.btnOk.TabIndex = 2;
			this.btnOk.Text = "确定(&O)";
			this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// QueryManager
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(404, 266);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.lstQuery);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "QueryManager";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "情景模式管理";
			this.Load += new System.EventHandler(this.QueryManager_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListView lstQuery;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnOk;
	}
}