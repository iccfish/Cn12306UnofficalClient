namespace TOBA.UI.Dialogs.Account
{
	using Controls.Common;

	partial class ModifyPassword
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.txtOrg = new System.Windows.Forms.TextBox();
			this.txtNew = new System.Windows.Forms.TextBox();
			this.txtConfirm = new System.Windows.Forms.TextBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.txtCode = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lnkGetMobileCode = new System.Windows.Forms.LinkLabel();
			this.tipBarMessage1 = new TipBarMessage();
			this.codeTimer = new System.Windows.Forms.Timer(this.components);
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label1.Location = new System.Drawing.Point(37, 46);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "原密码";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label2.Location = new System.Drawing.Point(37, 72);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(44, 17);
			this.label2.TabIndex = 1;
			this.label2.Text = "新密码";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label3.Location = new System.Drawing.Point(37, 99);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 17);
			this.label3.TabIndex = 1;
			this.label3.Text = "确　认";
			// 
			// txtOrg
			// 
			this.txtOrg.Location = new System.Drawing.Point(84, 43);
			this.txtOrg.Name = "txtOrg";
			this.txtOrg.Size = new System.Drawing.Size(389, 23);
			this.txtOrg.TabIndex = 0;
			// 
			// txtNew
			// 
			this.txtNew.Location = new System.Drawing.Point(84, 69);
			this.txtNew.Name = "txtNew";
			this.txtNew.Size = new System.Drawing.Size(389, 23);
			this.txtNew.TabIndex = 1;
			// 
			// txtConfirm
			// 
			this.txtConfirm.Location = new System.Drawing.Point(84, 96);
			this.txtConfirm.Name = "txtConfirm";
			this.txtConfirm.Size = new System.Drawing.Size(389, 23);
			this.txtConfirm.TabIndex = 2;
			// 
			// btnOk
			// 
			this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnOk.Image = global::TOBA.Properties.Resources.cou_16_accept;
			this.btnOk.Location = new System.Drawing.Point(153, 168);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(88, 29);
			this.btnOk.TabIndex = 3;
			this.btnOk.Text = "确定";
			this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOk.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnCancel.Image = global::TOBA.Properties.Resources.cou_16_block;
			this.btnCancel.Location = new System.Drawing.Point(245, 168);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(88, 29);
			this.btnCancel.TabIndex = 4;
			this.btnCancel.Text = "取消";
			this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// txtCode
			// 
			this.txtCode.Location = new System.Drawing.Point(84, 123);
			this.txtCode.Name = "txtCode";
			this.txtCode.Size = new System.Drawing.Size(291, 23);
			this.txtCode.TabIndex = 2;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.label4.Location = new System.Drawing.Point(13, 126);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(68, 17);
			this.label4.TabIndex = 1;
			this.label4.Text = "手机验证码";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lnkGetMobileCode);
			this.panel1.Controls.Add(this.tipBarMessage1);
			this.panel1.Controls.Add(this.btnOk);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.btnCancel);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.txtCode);
			this.panel1.Controls.Add(this.txtOrg);
			this.panel1.Controls.Add(this.txtConfirm);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.txtNew);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(500, 209);
			this.panel1.TabIndex = 6;
			// 
			// lnkGetMobileCode
			// 
			this.lnkGetMobileCode.AutoSize = true;
			this.lnkGetMobileCode.LinkBehavior = System.Windows.Forms.LinkBehavior.HoverUnderline;
			this.lnkGetMobileCode.Location = new System.Drawing.Point(381, 126);
			this.lnkGetMobileCode.Name = "lnkGetMobileCode";
			this.lnkGetMobileCode.Size = new System.Drawing.Size(92, 17);
			this.lnkGetMobileCode.TabIndex = 7;
			this.lnkGetMobileCode.TabStop = true;
			this.lnkGetMobileCode.Text = "获得手机验证码";
			this.lnkGetMobileCode.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkGetMobileCode_LinkClicked);
			// 
			// tipBarMessage1
			// 
			this.tipBarMessage1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(245)))), ((int)(((byte)(254)))));
			this.tipBarMessage1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(201)))), ((int)(((byte)(226)))));
			this.tipBarMessage1.BorderThickness = 1;
			this.tipBarMessage1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tipBarMessage1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(85)))), ((int)(((byte)(138)))), ((int)(((byte)(196)))));
			this.tipBarMessage1.Image = global::TOBA.Properties.Resources.cou_16_unlock;
			this.tipBarMessage1.LabelMargin = new System.Windows.Forms.Padding(29, 7, -78, -4);
			this.tipBarMessage1.Location = new System.Drawing.Point(0, 0);
			this.tipBarMessage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tipBarMessage1.Name = "tipBarMessage1";
			this.tipBarMessage1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.tipBarMessage1.Size = new System.Drawing.Size(500, 30);
			this.tipBarMessage1.TabIndex = 6;
			this.tipBarMessage1.Text = "请输入原始密码和新密码";
			// 
			// codeTimer
			// 
			this.codeTimer.Interval = 1000;
			// 
			// ModifyPassword
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(500, 209);
			this.Controls.Add(this.panel1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ModifyPassword";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "修改账户密码";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtOrg;
		private System.Windows.Forms.TextBox txtNew;
		private System.Windows.Forms.TextBox txtConfirm;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtCode;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.LinkLabel lnkGetMobileCode;
		private TipBarMessage tipBarMessage1;
		private System.Windows.Forms.Timer codeTimer;
	}
}