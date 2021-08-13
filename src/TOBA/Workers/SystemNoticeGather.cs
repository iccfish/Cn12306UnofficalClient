using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Workers
{
	using System.ComponentModel;
	using System.Text.RegularExpressions;
	using System.Threading;

	using FSLib.Network.Http;

	/// <summary>
	/// 系统通知获取
	/// </summary>
	internal class SystemNoticeGather
	{
		/// <summary>
		/// 获得所有的通知列表
		/// </summary>
		public List<Entity.Web.SystemNotice> SystemNotice { get; private set; }


		/// <summary>
		/// 获得下载是否成功
		/// </summary>
		public bool Success { get; private set; }

		/// <summary>
		/// 加载完成
		/// </summary>
		public event EventHandler DownloadComplete;

		/// <summary>
		/// 引发 <see cref="DownloadComplete" /> 事件
		/// </summary>
		protected virtual void OnDownloadComplete()
		{
			_operation = null;
			var handler = DownloadComplete;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		AsyncOperation _operation;

		/// <summary>
		/// 开始加载
		/// </summary>
		public void BeginGet()
		{
			if (_operation != null)
				return;

			_operation = AsyncOperationManager.CreateOperation(null);
			ThreadPool.QueueUserWorkItem(_ => GetInternal(), null);
		}

		void GetInternal()
		{
			var client = new WebLib.NetClient();
			//var task = client.Create<string>(HttpMethod.Get, "http://www.12306.cn/mormhweb/tlxw_tip.html").SendAsync();
			var task = client.Create<string>(HttpMethod.Get, "https://www.12306.cn/mormhweb/zxdt/index_zxdt.html").Send();
			if (task == null || !task.IsSuccess)
			{
				Success = false;
			}
			else
			{
				Success = true;
				var baseUri = new Uri("https://www.12306.cn/mormhweb/zxdt/");
				SystemNotice = System.Text.RegularExpressions.Regex.Matches(GetNewListHtml(task.Result), @"(class='red'>)?<a.*?href=['""](.*?)['""].*?title=['""](.*?)['""].*?\((\d{4}-\d{2}-\d{2})\)", RegexOptions.IgnoreCase | RegexOptions.Singleline)
									.Cast<Match>().Where(s => s.Success).Select(s => new Entity.Web.SystemNotice(s.Groups[3].Value, new Uri(baseUri, s.Groups[2].Value).ToString(), DateTime.Parse(s.Groups[4].Value), !s.Groups[1].Value.IsNullOrEmpty())).ToList();
			}

			_operation.PostOperationCompleted(_ => OnDownloadComplete(), null);
		}

		string GetNewListHtml(string html)
		{
			var key = "<div id=\"newList\">";
			var index = html.IndexOf(key);
			if (index == -1 || index + key.Length >= html.Length)
				return string.Empty;

			var endIndex = html.IndexOf("</div>", index + key.Length);

			if (index != -1 && endIndex != -1)
			{
				return html.Substring(index + key.Length, endIndex - index - key.Length);
			}
			return html;
		}
	}
}
