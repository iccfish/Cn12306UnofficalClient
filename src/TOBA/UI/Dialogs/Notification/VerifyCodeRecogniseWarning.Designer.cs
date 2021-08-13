namespace TOBA.UI.Dialogs.Notification
{
	partial class VerifyCodeRecogniseWarning
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
			this.lnkForum = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblMessage.Size = new System.Drawing.Size(421, 100);
			this.lblMessage.Text = "您所使用的版本已加载验证码识别扩展，具备验证码自动识别能力。请严格遵循作者的要求，不得扩散您所开发的验证码扩展。\r\n一旦发现有扩散趋势，后续版本将永久移除此扩展支" +
    "持。如果您不是此扩展的开发者，请向作者（木鱼）举报，感谢您的配合。\r\n良好的购票秩序需要我们共同维护，请不要给借此牟利的人任何机会。";
			// 
			// chkHide
			// 
			this.chkHide.Location = new System.Drawing.Point(23, 143);
			this.chkHide.Visible = false;
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(387, 129);
			// 
			// lnkForum
			// 
			this.lnkForum.AutoSize = true;
			this.lnkForum.Location = new System.Drawing.Point(21, 144);
			this.lnkForum.Name = "lnkForum";
			this.lnkForum.Size = new System.Drawing.Size(137, 12);
			this.lnkForum.TabIndex = 4;
			this.lnkForum.TabStop = true;
			this.lnkForum.Text = "发现违规请进入论坛举报";
			this.lnkForum.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkForum_LinkClicked);
			// 
			// VerifyCodeRecogniseWarning
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(539, 177);
			this.Controls.Add(this.lnkForum);
			this.Name = "VerifyCodeRecogniseWarning";
			this.Text = "验证码识别警告";
			this.Controls.SetChildIndex(this.lblMessage, 0);
			this.Controls.SetChildIndex(this.chkHide, 0);
			this.Controls.SetChildIndex(this.btnOk, 0);
			this.Controls.SetChildIndex(this.lnkForum, 0);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel lnkForum;
	}
}