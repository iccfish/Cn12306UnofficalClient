namespace TOBA.UI.Controls.Query
{
	using Common;

	partial class TrainPrompt
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
			this.lstStations = new ListBoxEx();
			this.SuspendLayout();
			// 
			// lstStations
			// 
			this.lstStations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lstStations.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.lstStations.FormattingEnabled = true;
			this.lstStations.ItemPadding = new System.Windows.Forms.Padding(5);
			this.lstStations.Location = new System.Drawing.Point(0, 0);
			this.lstStations.Name = "lstStations";
			this.lstStations.Size = new System.Drawing.Size(130, 282);
			this.lstStations.TabIndex = 0;
			// 
			// TrainPrompt
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lstStations);
			this.Name = "TrainPrompt";
			this.Size = new System.Drawing.Size(130, 282);
			this.ResumeLayout(false);

		}

		#endregion

		private ListBoxEx lstStations;

	}
}