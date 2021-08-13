using System;

namespace TOBA.UI.Dialogs.Notification
{
	using System.Windows.Forms;

	internal class BatchQueryTip : NotificationBase
	{
		public BatchQueryTip()
		{
			InitializeComponent();

			Load += BatchQueryTip_Load;
		}

		void BatchQueryTip_Load(object sender, EventArgs e)
		{
			chkHide.AddDataBinding(Configuration.ProgramConfiguration.Instance, x => x.Checked, x => x.HideBatchQueryTip);
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.Text = "同步查询会启动所有查询，并且查询之间没有任何等待关系。当你设置的查询较多时，可能会导致查询过快，会增加您被12306封IP的概率。建议使用『开始轮询』。";
			// 
			// BatchQueryTip
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.ClientSize = new System.Drawing.Size(545, 153);
			this.Name = "BatchQueryTip";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
