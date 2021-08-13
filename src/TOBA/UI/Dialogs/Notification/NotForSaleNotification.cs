using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI.Dialogs.Notification
{
	using System.Windows.Forms;

	using Configuration;

	internal class NotForSaleNotification : NotificationBase
	{
		public NotForSaleNotification()
		{
			InitializeComponent();
			Load += NotForSaleNotification_Load;
		}

		void NotForSaleNotification_Load(object sender, EventArgs e)
		{
			chkHide.AddDataBinding(ProgramConfiguration.Instance, s => s.Checked, s => s.NotSaleVerification);
			ProgramConfiguration.Instance.NotSaleVerification = true;
		}
		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.lblMessage.ForeColor = System.Drawing.Color.Red;
			this.lblMessage.Size = new System.Drawing.Size(593, 171);
			this.lblMessage.Text = "订票助手.NET 是免费软件，没有许可任何人通过任何渠道出售（任何形式的收费），仅接受赞助。如果您从淘宝或任何渠道购买的本软件，请立刻申请退款、打差评。任何对侵权" +
    "行为的纵容，作者（木鱼）都将鄙视你，并谢绝你使用本软件，谢谢。\r\n\r\n同时，向连免费软件都想着要卖的卖家们致以最诚挚的鄙视。";
			// 
			// chkHide
			// 
			this.chkHide.Location = new System.Drawing.Point(23, 214);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(559, 193);
			// 
			// NotForSaleNotification
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.ClientSize = new System.Drawing.Size(711, 248);
			this.Name = "NotForSaleNotification";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
