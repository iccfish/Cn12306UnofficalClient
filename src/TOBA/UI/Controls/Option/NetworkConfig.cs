using System;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Option
{
	using Dialogs;
	using Dialogs.Common;

	using FSLib.Network.Http;

	using TOBA.Configuration;
	using TOBA.WebLib;

	using WebLib.Sockets5;

	internal partial class NetworkConfig : OptionConfigForm.AbstractOptionConfigUI
	{
		public NetworkConfig()
		{
			InitializeComponent();

			DisplayText = "网络设置";
			Image = Properties.Resources.globe_16;
			BigImage = Properties.Resources.cou_32_search;

			//独立服务器查询-隐藏
			//chkUseIndividualServer.Visible = false;
			label10.Visible = false;
		}

		private void NetworkConfig_Load(object sender, EventArgs e)
		{
			var nc = Configuration.NetworkConfiguration.Current;
			cbProxyType.SelectedIndexChanged += (ss, ee) =>
			{
				gbProxy.Enabled = cbProxyType.SelectedIndex == 3;
			};
			var fc = ApiConfiguration.Instance;
			pVcLoadDelay.Visible = fc.EnableVerifycodeLoadDelay;
			pVcSubmitDelay.Visible = fc.EnableVerifycodeSubmitDelay;

			cbProxyType.AddDataBinding(nc, s => s.SelectedIndex, s => s.ProxyType);
			txtProxyPwd.AddDataBinding(nc, s => s.Text, s => s.ProxyPassword);
			txtProxyUrl.AddDataBinding(nc, s => s.Text, s => s.ProxyAddress);
			txtProxyUser.AddDataBinding(nc, s => s.Text, s => s.ProxyUserName);
			chkAppendDisableCdn.AddDataBinding(nc, s => s.Checked, s => s.DisableCdn);
			iVcDelay.AddDataBinding(nc, s => s.Value, s => s.ReloadVcCodeDelay);
			iVcSubmitDelay.AddDataBinding(nc, s => s.Value, s => s.VcSubmitDelay);

			//cbBaseUri.SelectedItem = nc.BaseUri;
			//cbBaseUri.SelectedIndexChanged += (ss, ee) =>
			//{
			//	nc.BaseUri = cbBaseUri.SelectedItem as string;
			//};
			nudProxyPort.Value = nc.ProxyPort.IsValueInRange(1, 65535) ? nc.ProxyPort : 80;
			//			nudRetryLimit.Value = nc.NetworkRetryCount;
			nudProxyPort.ValueChanged += (ss, ee) =>
			{
				nc.ProxyPort = (int)nudProxyPort.Value;
			};
			autoReloadServerCount.AddDataBinding(nc, s => s.IntValue, s => s.AutoReloadDnsLimit);

			//代理服务器类型
			cbProxyClass.AttachType = typeof(ProxyType);
			cbProxyClass.SelectedValue = nc.ProxyClass;
			pHttp.Visible = nc.ProxyClass == ProxyType.Http;
			pSocks.Visible = nc.ProxyClass == ProxyType.Socks5;
			nudSocks5Port.Value = nc.Socks5ServerPort.IsValueInRange(1, 65535) ? nc.Socks5ServerPort : 1080;
			nudSocks5Port.ValueChanged += (ss, ee) =>
			{
				nc.Socks5ServerPort = (int)nudSocks5Port.Value;
			};
			nudSocks5Url.AddDataBinding(nc, s => s.Text, s => s.Socks5ServerAddr);
			cbProxyClass.SelectedIndexChanged += (s, ex) =>
			{
				nc.ProxyClass = (ProxyType)cbProxyClass.SelectedValue;
				pHttp.Visible = nc.ProxyClass == ProxyType.Http;
				pSocks.Visible = nc.ProxyClass == ProxyType.Socks5;
			};

			//网络重试
			chkAutoRetryNetworkError.AddDataBinding(nc, s => s.Checked, s => s.AutoRetryOnNetworkError);
			pRetry.Visible = nc.AutoRetryOnNetworkError;
			chkAutoRetryNetworkError.CheckedChanged += (s, ex) =>
			{
				pRetry.Visible = chkAutoRetryNetworkError.Checked;
			};
			iptMaxRetry.Value = nc.RetryMaxCount;
			iptRetrySleep.Value = nc.RetrySleepTime;
			iptMaxRetry.ValueChanged += (s, ex) => nc.RetryMaxCount = iptMaxRetry.IntValue;
			iptRetrySleep.ValueChanged += (s, ex) => nc.RetrySleepTime = iptRetrySleep.IntValue;

			//失败时验证码自动重新加载
			var ar = AutoResumeRefreshConfiguration.Instance;
			chkAutoreloadWhenVcFailed.AddDataBinding(ar, s => s.Checked, s => s.AutoReloadVc);
			nudLoadDelayWhenFail.AddDataBinding(ar, s => s.Enabled, s => s.AutoReloadVc);
			nudLoadDelayWhenFail.Value = ar.AutoReloadVcTime;
			nudLoadDelayWhenFail.ValueChanged += (_1, _2) => ar.AutoReloadVcTime = (int)nudLoadDelayWhenFail.Value;
		}

		/// <summary>
		/// 请求保存
		/// </summary>
		/// <returns></returns>
		public override bool Save()
		{
			var nc = NetworkConfiguration.Current;
			if (nc.ProxyType != 3 || nc.ProxyClass == ProxyType.Http)
			{
				PolipoSocks5ToHttpProxyWrapper.Instance.Stop();
			}
			else
			{
				var errMsg = "";

				using (var dlg = new YetAnotherWaitingDialog
				{
					Title = "正在尝试启动Socks5代理..."
				})
				{
					dlg.WorkCallbackAdvanced = _ =>
					{
						var pw = PolipoSocks5ToHttpProxyWrapper.Instance;
						pw.Stop().Wait();

						pw.ParentSocksServerAddress = nc.Socks5ServerAddr;
						pw.ParentSocksServerPort = nc.Socks5ServerPort;
						pw.Start().Wait();

						if (!pw.IsRunning)
							throw new Exception("无法启动应用。");

						NetClientHandler.RefreshWebProxy();
						_.Title = "正在测试代理是否正确...";
						var client = new NetClient();
						client.Setting.Timeout = 30000;

						var str = client.Create<string>(HttpMethod.Get, "https://kyfw.12306.cn/otn/", null).Send();
						if (!str.IsValid() || str.Result.IndexOf("铁路客户服务中心", StringComparison.Ordinal) == -1)
						{
							pw.Stop().Wait();
							throw new Exception("无法通过指定的Socks5代理服务器访问12306。");
						}
					};
					dlg.ShowDialog(FindForm());

					errMsg = dlg.Exception?.Message ?? "";
				}

				if (!PolipoSocks5ToHttpProxyWrapper.Instance.IsRunning || !errMsg.IsNullOrEmpty())
				{
					this.Error("未能成功启动Socks5代理服务器！错误：" + errMsg);
					return false;
				}
			}
			NetClientHandler.RefreshWebProxy();

			return base.Save();
		}

	}
}
