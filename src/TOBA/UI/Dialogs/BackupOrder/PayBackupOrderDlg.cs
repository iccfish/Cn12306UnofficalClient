using System;
using System.Text;

namespace TOBA.UI.Dialogs.BackupOrder
{
	using Configuration;

	using System.IO;

	using TOBA.BackupOrder;
	using TOBA.BackupOrder.Entity;

	partial class PayBackupOrderDlg : DialogBase
	{
		private readonly UnpayBackupOrder _order;

		public PayBackupOrderDlg(Session session, UnpayBackupOrder order)
		{
			_order = order;
			InitializeComponent();

			Icon = Properties.Resources.creditcard;

			if (!Program.IsRunning)
				return;

			InitSession(session);
			Shown += PayBackupOrderDlg_Shown;
		}

		private async void PayBackupOrderDlg_Shown(object sender, EventArgs e)
		{
			var _service = Session.GetService<IBackupOrderService>();
			var (ok, msg, data) = await _service.CreatePayForm(_order.Order);

			if (!ok)
			{
				loading.SetLoadingError("无法支付：" + msg);
				return;
			}

			var html = new StringBuilder();
			var htmlheader = new StringBuilder();
			var htmlfooter = new StringBuilder();

			htmlheader.Append("<!DOCTYPE html><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><title>12306订单支付</title></head><body>");
			html.Append("<form id='payform' action='" + data.GetValue("epayurl") + "' method='post'>");
			foreach (var fdata in data)
			{
				if (fdata.Key == "epayurl")
					continue;
				html.Append("<input type='hidden' name='" + fdata.Key + "' value=\"" + fdata.Value + "\" />");
			}
			html.Append("<div style='font-family:微软雅黑;font-size:14px;'><p>正在跳转到12306支付页面，请稍等.....</p><div>如果已完成支付但订单状态始终不对，请前往12306检查或重新支付。多扣的金额将会在15个工作日内退还。</div><button style='margin: 20px auto; font-size: 18px; font-weight:bold; display: block;' type='submit'>如果没有自动跳转，请点击这里继续支付</button></div></form>");
			htmlfooter.Append("</body></html>");

			var tempfile = Path.Combine(Path.GetTempPath(), DateTime.Now.Ticks + ".html");
			File.WriteAllText(tempfile, htmlheader + html.ToString() + "<script>window.onload=function(){var f=document.getElementsByTagName('form')[0];f.setAttribute('target', '_self');f.submit();}</script>" + htmlfooter);

			if (ProgramConfiguration.Instance.SubmitOrderBrowser?.Launch(tempfile) != true)
			{
				//IE
				Shell.StartUrlInIE(tempfile);
			}

			loading.SetLoadingSuccess("正在打开支付页面，请尽快完成支付哈");
		}
	}
}
