using System;
using System.Collections.Generic;
using System.Linq;

namespace TOBA.Workers
{
	using Entity.Web;

	using FSLib.Network.Http;

	using System.Threading.Tasks;
	using System.Xml;

	/// <summary>
	/// 最谈最新资料获取
	/// </summary>
	internal class ForumNewsGather
	{
		public int Fid { get; set; }


		public async Task<List<Entity.Web.SystemNotice>> LoadAsync()
		{
			var client = new WebLib.NetClient();
			var task = client.Create<string>(HttpMethod.Get, "https://forum.iccfish.com/forum.php?mod=rss" + (Fid > 0 ? "&fid=" + Fid : ""), "");
			var result = await task.SendAsync();

			if (!task.IsValid())
			{
				return null;
			}

			var ret = new List<SystemNotice>();

			await Task.Factory.StartNew(() =>
			{
				var doc = new XmlDocument();
				doc.LoadXml(result);

				var nodes = doc.SelectNodes("//item");
				foreach (var node in nodes.Cast<XmlNode>())
				{
					var categoryName = node.SelectSingleNode("category").InnerText.Trim();

					var no = new SystemNotice(node.SelectSingleNode("title").InnerText.Trim(),
						node.SelectSingleNode("link").InnerText.Trim(),
						node.SelectSingleNode("pubDate").InnerText.Trim().ToDateTimeNullable() ?? DateTime.Now,
						categoryName.IndexOf("新闻") > -1 || categoryName.IndexOf("公告") > -1,
						"作者：" + node.SelectSingleNode("author").InnerText.Trim() + "\n\n" +
						node.SelectSingleNode("description").InnerText.Trim()
					);
					ret.Add(no);
				}
			});

			return ret;
		}
	}
}
