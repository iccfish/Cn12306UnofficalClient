using System;

namespace TOBA.WebLib
{
	using Configuration;

	using Data;

	using FSLib.Network.Http;

	using TOBA.WebLib.Sockets5;

	using UI.Dialogs.Misc;

	internal class NetworkEnvironment
	{

		public static MaxOccourTimesCounter MaxOccourTimesCounter { get; private set; }

		#region 常量部分

		public static string UrlScriptCityName = "resources/js/framework/station_name.js";
		public static string UrlResourceVersion = "leftTicket/init";

		#endregion

		public static void Init(IStartup startup, Action requireInitProxyType)
		{
			NetClientHandler.Init();
			if (NetworkConfiguration.Current.ProxyType == 3 && NetworkConfiguration.Current.ProxyClass == ProxyType.Socks5)
			{
				startup.Update("正在初始化代理服务器...");
				var instance = PolipoSocks5ToHttpProxyWrapper.Instance;
				instance.ParentSocksServerAddress = NetworkConfiguration.Current.Socks5ServerAddr;
				instance.ParentSocksServerPort = NetworkConfiguration.Current.Socks5ServerPort;

				instance.Start().Wait();
				if (!instance.IsRunning)
				{
					throw new Exception("未能初始化本地代理服务器");
				}

				startup.Update("正在测试代理是否正确...");
				var client = new NetClient();
				var str = client.Create<string>(HttpMethod.Get, "https://kyfw.12306.cn/otn/", null).Send();
				if (!str.IsValid() || str.Result.IndexOf("铁路客户服务中心", StringComparison.Ordinal) == -1)
				{
					instance.Stop().Wait();
					throw new Exception("无法通过指定的Socks5代理服务器访问12306。");
				}
			}

			startup.Update("正在检测网络信息...");

			var host = NetworkConfiguration.Current.BaseUri.Host;
			var ip = "";
			try
			{
				var addlist = System.Net.Dns.GetHostAddresses(host);
				if (!addlist.IsEmpty())
				{
					ip = addlist[0].ToString();
				}
			}
			catch (Exception)
			{
			}

			GlobalEvents.BeforeRequest += (s, e) =>
			{
				Statistics.Current.WebRequestCount++;
			};
		}
	}
}