using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.WebNotification
{
	using System.Collections.Specialized;
	using System.Threading.Tasks;

	using FSLib.Network.Http;

	using WebLib;

	internal class WebNotifier
	{
		public async Task<string> SendAsync(string url, string content)
		{
			var cfg = WebNotifyConfiguration.Instance;
			if (!cfg.Enabled)
				return "WEB通知未启用";
			if (cfg.UrlTemplate.IsNullOrEmpty())
				return "地址未设置";

			var body = cfg.HttpMethod == HttpMethod.Get && !content.IsNullOrEmpty() ? null : new RequestStringContent(content, cfg.RequestContentType);

			var nc = new NetClient();
			var ctx = nc.Create<string>(cfg.HttpMethod, url, cfg.Refer.DefaultForEmpty(url), body);
			await ctx.SendAsync().ConfigureAwait(true);

			if (!ctx.IsValid())
				return $"无法完成请求。状态码：{ctx.Status}，错误信息：{ctx.Exception?.Message.DefaultForEmpty("无其它错误")}";

			if (!cfg.CheckKeyword.IsNullOrEmpty())
			{
				if (ctx.Result.IndexOf(cfg.CheckKeyword, StringComparison.OrdinalIgnoreCase) == -1)
					return "响应关键字不存在";
			}

			return null;
		}
	}
}
