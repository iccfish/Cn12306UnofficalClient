namespace TOBA.UI.Controls.Log
{
	using Common;

	partial class SysLogPanel
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
			this.components = new System.ComponentModel.Container();
			this.log = new LogView();
			this.SuspendLayout();
			// 
			// log
			// 
			this.log.Dock = System.Windows.Forms.DockStyle.Fill;
			this.log.ItemCountLimit = 5000;
			this.log.Location = new System.Drawing.Point(3, 3);
			this.log.Name = "log";
			this.log.Size = new System.Drawing.Size(643, 407);
			this.log.TabIndex = 0;
			this.log.UseCompatibleStateImageBehavior = false;
			// 
			// SysLogPanel
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.log);
			this.Name = "SysLogPanel";
			this.Padding = new System.Windows.Forms.Padding(3);
			this.Size = new System.Drawing.Size(649, 413);
			this.ResumeLayout(false);

		}

		#endregion

		private LogView log;
	}
}
