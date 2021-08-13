namespace TOBA.UI.Dialogs
{
	using Controls.Common;

	partial class OptionConfigForm
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
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.optList = new ListBoxEx();
			this.panelMain = new System.Windows.Forms.Panel();
			this.panel3 = new System.Windows.Forms.Panel();
			this.lblTitle = new System.Windows.Forms.Label();
			this.pbIcon = new System.Windows.Forms.PictureBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnSave = new ButtonExtend();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbIcon)).BeginInit();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(5, 5);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.optList);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.panelMain);
			this.splitContainer1.Panel2.Controls.Add(this.panel3);
			this.splitContainer1.Panel2.Controls.Add(this.panel1);
			this.splitContainer1.Size = new System.Drawing.Size(683, 448);
			this.splitContainer1.SplitterDistance = 144;
			this.splitContainer1.TabIndex = 0;
			// 
			// optList
			// 
			this.optList.DefaultCaptionFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
			this.optList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.optList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.optList.FormattingEnabled = true;
			this.optList.ItemPadding = new System.Windows.Forms.Padding(5);
			this.optList.Location = new System.Drawing.Point(0, 0);
			this.optList.Name = "optList";
			this.optList.Size = new System.Drawing.Size(144, 448);
			this.optList.TabIndex = 0;
			// 
			// panelMain
			// 
			this.panelMain.BackColor = System.Drawing.SystemColors.Window;
			this.panelMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(0, 38);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(535, 366);
			this.panelMain.TabIndex = 3;
			// 
			// panel3
			// 
			this.panel3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.panel3.Controls.Add(this.lblTitle);
			this.panel3.Controls.Add(this.pbIcon);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.ForeColor = System.Drawing.SystemColors.Window;
			this.panel3.Location = new System.Drawing.Point(0, 0);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(535, 38);
			this.panel3.TabIndex = 2;
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblTitle.Location = new System.Drawing.Point(40, 9);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(74, 22);
			this.lblTitle.TabIndex = 1;
			this.lblTitle.Text = "选项配置";
			// 
			// pbIcon
			// 
			this.pbIcon.Location = new System.Drawing.Point(7, 4);
			this.pbIcon.Name = "pbIcon";
			this.pbIcon.Size = new System.Drawing.Size(32, 32);
			this.pbIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.pbIcon.TabIndex = 0;
			this.pbIcon.TabStop = false;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnSave);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 404);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new System.Windows.Forms.Padding(2);
			this.panel1.Size = new System.Drawing.Size(535, 44);
			this.panel1.TabIndex = 0;
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSave.Image = global::TOBA.Properties.Resources.tick_16;
			this.btnSave.Location = new System.Drawing.Point(427, 5);
			this.btnSave.Name = "btnSave";
			this.btnSave.ShowShield = false;
			this.btnSave.Size = new System.Drawing.Size(103, 36);
			this.btnSave.TabIndex = 0;
			this.btnSave.Text = "保存配置(&S)";
			this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// OptionConfigForm
			// 
			this.AcceptButton = this.btnSave;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(693, 458);
			this.Controls.Add(this.splitContainer1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionConfigForm";
			this.Padding = new System.Windows.Forms.Padding(5);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "首选项";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbIcon)).EndInit();
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer splitContainer1;
		private Controls.Common.ListBoxEx optList;
		private System.Windows.Forms.Panel panelMain;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.PictureBox pbIcon;
		private ButtonExtend btnSave;
		protected System.Windows.Forms.Panel panel1;
	}
}