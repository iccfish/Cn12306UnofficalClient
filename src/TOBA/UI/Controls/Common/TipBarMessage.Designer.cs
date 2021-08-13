namespace TOBA.UI.Controls.Common
{
	partial class TipBarMessage
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
			this.pb = new System.Windows.Forms.PictureBox();
			this.lblMessage = new System.Windows.Forms.LinkLabel();
			((System.ComponentModel.ISupportInitialize)(this.pb)).BeginInit();
			this.SuspendLayout();
			// 
			// pb
			// 
			this.pb.Location = new System.Drawing.Point(10, 10);
			this.pb.Name = "pb";
			this.pb.Size = new System.Drawing.Size(16, 16);
			this.pb.TabIndex = 0;
			this.pb.TabStop = false;
			// 
			// lblMessage
			// 
			this.lblMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblMessage.AutoEllipsis = true;
			this.lblMessage.Location = new System.Drawing.Point(32, 13);
			this.lblMessage.Margin = new System.Windows.Forms.Padding(50, 5, 10, 5);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(190, 52);
			this.lblMessage.TabIndex = 1;
			// 
			// TipBarMessage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.pb);
			this.Name = "TipBarMessage";
			this.Size = new System.Drawing.Size(232, 67);
			((System.ComponentModel.ISupportInitialize)(this.pb)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pb;
		private System.Windows.Forms.LinkLabel lblMessage;
	}
}
