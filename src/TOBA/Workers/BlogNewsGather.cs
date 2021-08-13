namespace TOBA.Workers
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Linq;
	using System.Threading;
	using System.Xml;

	using Entity.Web;

	using FSLib.Network.Http;

	internal class BlogNewsGather
	{
		/// <summary>
		/// 获得所有的通知列表
		/// </summary>
		public List<Entity.Web.SystemNotice> SystemNotice { get; private set; }

		public int Fid { get; set; }


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
			var task = client.Create<XmlDocument>(HttpMethod.Get, "https://blog.iccfish.com/feed/", "").Send();
			if (task == null || !task.IsSuccess || task.Result == null)
			{
				Success = false;
			}
			else
			{
				Success = true;
				SystemNotice = new List<SystemNotice>();
				var nodes = task.Result.SelectNodes("//item");
				foreach (var node in nodes.Cast<XmlNode>())
				{
					var no = new SystemNotice(node.SelectSingleNode("title").InnerText.Trim(),
						node.SelectSingleNode("link").InnerText.Trim(),
						node.SelectSingleNode("pubDate").InnerText.Trim().ToDateTimeNullable() ?? DateTime.Now,
						false,
						node.SelectSingleNode("description").InnerText.Trim()
					);
					SystemNotice.Add(no);
				}
			}

			_operation.PostOperationCompleted(_ => OnDownloadComplete(), null);
		}
	}
}