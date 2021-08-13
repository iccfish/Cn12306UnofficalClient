namespace TOBA.UI.Dialogs.Order
{
	using DevComponents.DotNetBar;
	using DevComponents.DotNetBar.Controls;

	partial class FastSubmitOrder
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
			this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
			this.tc = new TOBA.UI.Controls.Vc.TouchClickSimple();
			this.lblError = new DevComponents.DotNetBar.LabelX();
			this.panel3 = new System.Windows.Forms.Panel();
			this.panel4 = new System.Windows.Forms.Panel();
			this.lblErrMsg = new System.Windows.Forms.Label();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnSubmit
			// 
			this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSubmit.Font = new System.Drawing.Font("微软雅黑", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.btnSubmit.Image = global::TOBA.Properties.Resources.cou_32_accept;
			this.btnSubmit.Location = new System.Drawing.Point(12, 413);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.Size = new System.Drawing.Size(217, 42);
			this.btnSubmit.TabIndex = 1;
			this.btnSubmit.Text = "提交订单";
			// 
			// tc
			// 
			this.tc.BackColor = System.Drawing.Color.White;
			this.tc.Image = null;
			this.tc.Location = new System.Drawing.Point(12, 134);
			this.tc.Name = "tc";
			this.tc.ShowOkButton = false;
			this.tc.Size = new System.Drawing.Size(396, 273);
			this.tc.TabIndex = 8;
			// 
			// lblError
			// 
			this.lblError.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.lblError.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblError.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lblError.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblError.ForeColor = System.Drawing.Color.RoyalBlue;
			this.lblError.Location = new System.Drawing.Point(0, 0);
			this.lblError.Name = "lblError";
			this.lblError.PaddingBottom = 5;
			this.lblError.PaddingLeft = 5;
			this.lblError.PaddingRight = 5;
			this.lblError.PaddingTop = 5;
			this.lblError.Size = new System.Drawing.Size(622, 63);
			this.lblError.TabIndex = 10;
			this.lblError.Text = "<b><font color=\"#049B59\">哈哈哈哈哈哈</font></b>(成人票/二等座/二代身份证)  <expand></expand>";
			this.lblError.WordWrap = true;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.panel4);
			this.panel3.Controls.Add(this.lblError);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(0, 65);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(622, 63);
			this.panel3.TabIndex = 11;
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.Color.RoyalBlue;
			this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel4.Location = new System.Drawing.Point(0, 62);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(622, 1);
			this.panel4.TabIndex = 11;
			// 
			// lblErrMsg
			// 
			this.lblErrMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblErrMsg.AutoEllipsis = true;
			this.lblErrMsg.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblErrMsg.ForeColor = System.Drawing.Color.Red;
			this.lblErrMsg.Location = new System.Drawing.Point(377, 413);
			this.lblErrMsg.Name = "lblErrMsg";
			this.lblErrMsg.Size = new System.Drawing.Size(242, 42);
			this.lblErrMsg.TabIndex = 12;
			this.lblErrMsg.Text = "错误信息";
			this.lblErrMsg.Visible = false;
			// 
			// FastSubmitOrder
			// 
			this.AcceptButton = this.btnSubmit;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(622, 481);
			this.Controls.Add(this.lblErrMsg);
			this.Controls.Add(this.panel3);
			this.Controls.Add(this.btnSubmit);
			this.Controls.Add(this.tc);
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FastSubmitOrder";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.SubmitButton = this.btnSubmit;
			this.Text = "快速提交订单";
			this.Controls.SetChildIndex(this.tc, 0);
			this.Controls.SetChildIndex(this.btnSubmit, 0);
			this.Controls.SetChildIndex(this.panel3, 0);
			this.Controls.SetChildIndex(this.lblErrMsg, 0);
			this.panel3.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private ButtonX btnSubmit;
		private Controls.Vc.TouchClickSimple tc;
		private LabelX lblError;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label lblErrMsg;
	}
}