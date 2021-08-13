namespace TOBA.UI.Dialogs
{
	partial class ConfigCenter
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
			this.cbMode = new System.Windows.Forms.ComboBox();
			this.lblModeChanged = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lblModeChanged);
			this.panel1.Controls.Add(this.cbMode);
			this.panel1.Controls.SetChildIndex(this.cbMode, 0);
			this.panel1.Controls.SetChildIndex(this.lblModeChanged, 0);
			// 
			// cbMode
			// 
			this.cbMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbMode.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.cbMode.FormattingEnabled = true;
			this.cbMode.Items.AddRange(new object[] {
            "本日起售模式",
            "捡漏模式",
            "熟练抢票模式"});
			this.cbMode.Location = new System.Drawing.Point(9, 8);
			this.cbMode.Name = "cbMode";
			this.cbMode.Size = new System.Drawing.Size(132, 29);
			this.cbMode.TabIndex = 1;
			// 
			// lblModeChanged
			// 
			this.lblModeChanged.AutoSize = true;
			this.lblModeChanged.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblModeChanged.ForeColor = System.Drawing.Color.Crimson;
			this.lblModeChanged.Location = new System.Drawing.Point(147, 16);
			this.lblModeChanged.Name = "lblModeChanged";
			this.lblModeChanged.Size = new System.Drawing.Size(199, 15);
			this.lblModeChanged.TabIndex = 2;
			this.lblModeChanged.Text = "模式已切换，请重新打开配置中心。";
			this.lblModeChanged.Visible = false;
			// 
			// ConfigCenter
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(693, 458);
			this.Name = "ConfigCenter";
			this.ShowIcon = true;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "配置中心";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox cbMode;
		private System.Windows.Forms.Label lblModeChanged;
	}
}