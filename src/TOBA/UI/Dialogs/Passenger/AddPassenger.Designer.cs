namespace TOBA.UI.Dialogs.Passenger
{
	partial class AddPassenger
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
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.txtId = new System.Windows.Forms.TextBox();
			this.txtMobile = new System.Windows.Forms.TextBox();
			this.cbType = new System.Windows.Forms.ComboBox();
			this.cbIdType = new System.Windows.Forms.ComboBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.epp = new System.Windows.Forms.ErrorProvider(this.components);
			this.label6 = new System.Windows.Forms.Label();
			this.cbSex = new System.Windows.Forms.ComboBox();
			this.label8 = new System.Windows.Forms.Label();
			this.cbCountry = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.epp)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(6, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(49, 12);
			this.label1.TabIndex = 0;
			this.label1.Text = "姓名";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(126, 42);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(49, 12);
			this.label2.TabIndex = 0;
			this.label2.Text = "类型";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(6, 93);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(49, 12);
			this.label3.TabIndex = 0;
			this.label3.Text = "证件";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(6, 119);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(49, 12);
			this.label4.TabIndex = 0;
			this.label4.Text = "证件号";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(6, 146);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(49, 12);
			this.label5.TabIndex = 0;
			this.label5.Text = "手机号";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(57, 13);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(225, 21);
			this.txtName.TabIndex = 0;
			// 
			// txtId
			// 
			this.txtId.Location = new System.Drawing.Point(57, 117);
			this.txtId.Name = "txtId";
			this.txtId.Size = new System.Drawing.Size(225, 21);
			this.txtId.TabIndex = 6;
			// 
			// txtMobile
			// 
			this.txtMobile.Location = new System.Drawing.Point(57, 144);
			this.txtMobile.Name = "txtMobile";
			this.txtMobile.Size = new System.Drawing.Size(225, 21);
			this.txtMobile.TabIndex = 7;
			// 
			// cbType
			// 
			this.cbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbType.FormattingEnabled = true;
			this.cbType.Location = new System.Drawing.Point(177, 40);
			this.cbType.Name = "cbType";
			this.cbType.Size = new System.Drawing.Size(105, 20);
			this.cbType.TabIndex = 4;
			// 
			// cbIdType
			// 
			this.cbIdType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbIdType.FormattingEnabled = true;
			this.cbIdType.Location = new System.Drawing.Point(57, 91);
			this.cbIdType.Name = "cbIdType";
			this.cbIdType.Size = new System.Drawing.Size(225, 20);
			this.cbIdType.TabIndex = 5;
			// 
			// btnOk
			// 
			this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOk.Image = global::TOBA.Properties.Resources.tick_16;
			this.btnOk.Location = new System.Drawing.Point(90, 176);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(93, 31);
			this.btnOk.TabIndex = 8;
			this.btnOk.Text = "确定(&O)";
			this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Image = global::TOBA.Properties.Resources.stop_16;
			this.btnCancel.Location = new System.Drawing.Point(189, 176);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(93, 31);
			this.btnCancel.TabIndex = 9;
			this.btnCancel.Text = "取消(&C)";
			this.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// epp
			// 
			this.epp.ContainerControl = this;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(6, 43);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(49, 12);
			this.label6.TabIndex = 0;
			this.label6.Text = "性别";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbSex
			// 
			this.cbSex.DisplayMember = "Code";
			this.cbSex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbSex.FormattingEnabled = true;
			this.cbSex.Location = new System.Drawing.Point(57, 40);
			this.cbSex.Name = "cbSex";
			this.cbSex.Size = new System.Drawing.Size(66, 20);
			this.cbSex.TabIndex = 1;
			this.cbSex.ValueMember = "Code";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(6, 69);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(49, 12);
			this.label8.TabIndex = 0;
			this.label8.Text = "国家";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cbCountry
			// 
			this.cbCountry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbCountry.FormattingEnabled = true;
			this.cbCountry.Location = new System.Drawing.Point(57, 67);
			this.cbCountry.Name = "cbCountry";
			this.cbCountry.Size = new System.Drawing.Size(225, 20);
			this.cbCountry.TabIndex = 3;
			// 
			// AddPassenger
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(301, 219);
			this.ControlBox = false;
			this.Controls.Add(this.cbSex);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.cbIdType);
			this.Controls.Add(this.cbCountry);
			this.Controls.Add(this.cbType);
			this.Controls.Add(this.txtMobile);
			this.Controls.Add(this.txtId);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "AddPassenger";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "编辑联系人";
			((System.ComponentModel.ISupportInitialize)(this.epp)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.TextBox txtId;
		private System.Windows.Forms.TextBox txtMobile;
		private System.Windows.Forms.ComboBox cbType;
		private System.Windows.Forms.ComboBox cbIdType;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ErrorProvider epp;
		private System.Windows.Forms.ComboBox cbSex;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cbCountry;
		private System.Windows.Forms.Label label8;
	}
}