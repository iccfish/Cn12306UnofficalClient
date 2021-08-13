using System;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.Order
{
	using Configuration;

	using FSLib.Extension;

	using System.IO;

	using TOBA.Order;
	using TOBA.Order.Entity;

	internal partial class PayOrder : DialogBase
	{
		public OrderItem Order { get; set; }

		public PayOrder()
		{
			InitializeComponent();
			Icon = Properties.Resources.creditcard;

			Load += PayOrder_Load;
			Shown += PayOrder_Shown;
			FormClosing += (s, e) =>
			{
				Session.OnOrderChanged();
			};
		}

		void PayOrder_Shown(object sender, EventArgs e)
		{
			var isload = false;
			wb.DocumentCompleted += (s, x) =>
			{
				if (isload) return;
				isload = true;
				loading1.Reload();
			};
			wb.Navigate("about:blank");
		}

		PayOrderWorker _payOrderWorker;
		bool _directResign;

		void PayOrder_Load(object sender, EventArgs e)
		{
			_payOrderWorker = new PayOrderWorker(Session);
			_payOrderWorker.RequireDirectResign += this.SafeInvoke<GeneralEventArgs<bool>>(_payOrderWorker_RequireDirectResign);
			_payOrderWorker.DirectResignFailed += this.SafeInvoke(_payOrderWorker_DirectResignFailed);
			_payOrderWorker.DirectResignSuccess += this.SafeInvoke(_payOrderWorker_DirectResignSuccess);
			_payOrderWorker.PayOrderFailed += this.SafeInvoke(_payOrderWorker_PayOrderFailed);
			_payOrderWorker.PayOrderSuccess += this.SafeInvoke(_payOrderWorker_PayOrderSuccess);

			loading1.RequestLoad += loading1_RequestLoad;
			loading1.LoadFailed += loading1_LoadFailed;
			loading1.KeepCenter(this);
		}

		void _payOrderWorker_PayOrderSuccess(object sender, EventArgs e)
		{
			var html = new StringBuilder();
			var htmlheader = new StringBuilder();
			var htmlfooter = new StringBuilder();

			var data = _payOrderWorker.FormData;
			var paramdata = _payOrderWorker.ParamData;

			if (data == null)
			{
				if (_directResign)
				{
					Close();
				}
				else
				{
					this.Information("无法完成支付，请尽快前往12306网站支付！");
					Shell.StartUrl("https://kyfw.12306.cn/otn/");
					Close();
				}
			}

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

			var useDefaultSubmit = false;
			if (!useDefaultSubmit || ProgramConfiguration.Instance.SubmitOrderBrowser != null)
			{
				var tempfile = Path.Combine(Path.GetTempPath(), DateTime.Now.Ticks + ".html");
				File.WriteAllText(tempfile, htmlheader + html.ToString() + "<script>window.onload=function(){var f=document.getElementsByTagName('form')[0];f.setAttribute('target', '_self');f.submit();}</script>" + htmlfooter);

				if (ProgramConfiguration.Instance.SubmitOrderBrowser?.Launch(tempfile) != true)
				{
					//IE
					Shell.StartUrlInIE(tempfile);
				}
			}

			if (useDefaultSubmit)
			{
				//wb.Document.Body.InnerHtml = html.ToString();
				//(wb.Document.GetElementById("payform").DomElement as HTMLFormElement).submit();
				//WindowState = FormWindowState.Maximized;
			}
			else
			{
				wb.Document.Body.InnerHtml = "订票支付页面已在外部浏览器打开，请在浏览器中完成支付。";
			}

			loading1.Hide();
		}

		void _payOrderWorker_PayOrderFailed(object sender, EventArgs e)
		{
			this.Information("无法完成支付，请尽快前往12306网站支付！\n\n错误信息：" + _payOrderWorker.Error.DefaultForEmpty("未知错误"));
			Shell.StartUrl("https://kyfw.12306.cn/otn/");
			Close();
		}

		void _payOrderWorker_DirectResignSuccess(object sender, EventArgs e)
		{
			if (!_payOrderWorker.DirectResignCancelled)
			{
				this.Information("改签成功！");
			}
			Session.OnOrderChanged();
			Close();
		}

		void _payOrderWorker_DirectResignFailed(object sender, EventArgs e)
		{
			this.Information("直接改签失败，请尽快前往12306网站完成改签！\n\n错误信息：" + _payOrderWorker.Error.DefaultForEmpty("未知错误"));
			Shell.StartUrl("https://kyfw.12306.cn/otn/");
			Close();
		}

		void _payOrderWorker_RequireDirectResign(object sender, GeneralEventArgs<bool> e)
		{
			AppContext.HostForm.Invoke(() =>
			{
				e.Data = this.Question("改签无需支付，确认即可改签。确定要改签吗？", true);
			});
		}

		void loading1_LoadFailed(object sender, EventArgs e)
		{
			this.Information("无法完成支付，请尽快前往12306网站支付！\n\n错误信息：" + _payOrderWorker.Error.DefaultForEmpty("未知错误"));
			Shell.StartUrl("https://kyfw.12306.cn/otn/");
			Close();
		}

		void loading1_RequestLoad(object sender, GeneralEventArgs<Action<Action>> e)
		{
			_payOrderWorker.GetOrderData(Order);
		}
	}
}
