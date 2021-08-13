namespace TOBA.UI.Dialogs.Notification
{
	partial class LocalTimeDifferenceTooLarge
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
			this.reflectionImage1 = new DevComponents.DotNetBar.Controls.ReflectionImage();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.panel1 = new System.Windows.Forms.Panel();
			this.lblTimeDiff = new DevComponents.DotNetBar.LabelX();
			this.lblServerTime = new DevComponents.DotNetBar.LabelX();
			this.lblLocalTime = new DevComponents.DotNetBar.LabelX();
			this.btnOK = new DevComponents.DotNetBar.ButtonX();
			this.labelX4 = new DevComponents.DotNetBar.LabelX();
			this.labelX3 = new DevComponents.DotNetBar.LabelX();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.labelX5 = new DevComponents.DotNetBar.LabelX();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// reflectionImage1
			// 
			// 
			// 
			// 
			this.reflectionImage1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.reflectionImage1.BackgroundStyle.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
			this.reflectionImage1.Image = global::TOBA.Properties.Resources.warning;
			this.reflectionImage1.Location = new System.Drawing.Point(3, 3);
			this.reflectionImage1.Name = "reflectionImage1";
			this.reflectionImage1.Size = new System.Drawing.Size(83, 94);
			this.reflectionImage1.TabIndex = 0;
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX1.Location = new System.Drawing.Point(92, 16);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(582, 61);
			this.labelX1.TabIndex = 1;
			this.labelX1.Text = "注意昂，你的电脑系统时间看起来和服务器时间差了很多昂！注意看是不是系统时间不正确昂！";
			this.labelX1.TextLineAlignment = System.Drawing.StringAlignment.Near;
			this.labelX1.WordWrap = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.lblTimeDiff);
			this.panel1.Controls.Add(this.lblServerTime);
			this.panel1.Controls.Add(this.lblLocalTime);
			this.panel1.Controls.Add(this.btnOK);
			this.panel1.Controls.Add(this.labelX5);
			this.panel1.Controls.Add(this.labelX4);
			this.panel1.Controls.Add(this.labelX3);
			this.panel1.Controls.Add(this.labelX2);
			this.panel1.Controls.Add(this.reflectionImage1);
			this.panel1.Controls.Add(this.labelX1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(686, 311);
			this.panel1.TabIndex = 2;
			// 
			// lblTimeDiff
			// 
			// 
			// 
			// 
			this.lblTimeDiff.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblTimeDiff.Location = new System.Drawing.Point(264, 156);
			this.lblTimeDiff.Name = "lblTimeDiff";
			this.lblTimeDiff.Size = new System.Drawing.Size(212, 20);
			this.lblTimeDiff.TabIndex = 4;
			// 
			// lblServerTime
			// 
			// 
			// 
			// 
			this.lblServerTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblServerTime.Location = new System.Drawing.Point(264, 125);
			this.lblServerTime.Name = "lblServerTime";
			this.lblServerTime.Size = new System.Drawing.Size(212, 20);
			this.lblServerTime.TabIndex = 4;
			// 
			// lblLocalTime
			// 
			// 
			// 
			// 
			this.lblLocalTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblLocalTime.Location = new System.Drawing.Point(264, 96);
			this.lblLocalTime.Name = "lblLocalTime";
			this.lblLocalTime.Size = new System.Drawing.Size(212, 20);
			this.lblLocalTime.TabIndex = 4;
			// 
			// btnOK
			// 
			this.btnOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Image = global::TOBA.Properties.Resources.tick_16;
			this.btnOK.Location = new System.Drawing.Point(213, 258);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(179, 41);
			this.btnOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnOK.TabIndex = 3;
			this.btnOK.Text = "朕知道了昂！";
			// 
			// labelX4
			// 
			// 
			// 
			// 
			this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX4.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX4.Location = new System.Drawing.Point(92, 154);
			this.labelX4.Name = "labelX4";
			this.labelX4.Size = new System.Drawing.Size(155, 25);
			this.labelX4.TabIndex = 2;
			this.labelX4.Text = "服务器-本机时间差";
			this.labelX4.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// labelX3
			// 
			// 
			// 
			// 
			this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX3.Location = new System.Drawing.Point(92, 123);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(155, 25);
			this.labelX3.TabIndex = 2;
			this.labelX3.Text = "服务器大概时间";
			this.labelX3.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// labelX2
			// 
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX2.Location = new System.Drawing.Point(92, 92);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(155, 25);
			this.labelX2.TabIndex = 2;
			this.labelX2.Text = "系统本机时间";
			this.labelX2.TextAlignment = System.Drawing.StringAlignment.Far;
			// 
			// labelX5
			// 
			// 
			// 
			// 
			this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX5.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.labelX5.ForeColor = System.Drawing.Color.Green;
			this.labelX5.Location = new System.Drawing.Point(92, 185);
			this.labelX5.Name = "labelX5";
			this.labelX5.Size = new System.Drawing.Size(569, 67);
			this.labelX5.TabIndex = 2;
			this.labelX5.Text = "如果您确定本机时间没有错误，则可安全忽略此提示，不会有太大影响。";
			this.labelX5.TextAlignment = System.Drawing.StringAlignment.Center;
			this.labelX5.WordWrap = true;
			// 
			// LocalTimeDifferenceTooLarge
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.CancelButton = this.btnOK;
			this.ClientSize = new System.Drawing.Size(686, 311);
			this.Controls.Add(this.panel1);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LocalTimeDifferenceTooLarge";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "时差警告";
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.Controls.ReflectionImage reflectionImage1;
		private DevComponents.DotNetBar.LabelX labelX1;
		private System.Windows.Forms.Panel panel1;
		private DevComponents.DotNetBar.LabelX lblTimeDiff;
		private DevComponents.DotNetBar.LabelX lblServerTime;
		private DevComponents.DotNetBar.LabelX lblLocalTime;
		private DevComponents.DotNetBar.ButtonX btnOK;
		private DevComponents.DotNetBar.LabelX labelX4;
		private DevComponents.DotNetBar.LabelX labelX3;
		private DevComponents.DotNetBar.LabelX labelX2;
		private System.Windows.Forms.Timer timer;
		private DevComponents.DotNetBar.LabelX labelX5;
	}
}