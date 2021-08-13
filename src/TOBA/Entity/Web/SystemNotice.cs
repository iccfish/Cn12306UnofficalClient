using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Entity.Web
{
	internal class SystemNotice
	{
		/// <summary>
		/// 标题
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// 地址
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// 发布时间
		/// </summary>
		public DateTime Date { get; set; }

		public bool Important { get; set; }

		public string Description { get; set; }

		/// <summary>
		/// 创建 <see cref="SystemNotice" />  的新实例(SystemNotice)
		/// </summary>
		public SystemNotice(string title, string url, DateTime date, bool important, string description = "")
		{
			Important = important;
			Title = title;
			Url = url;
			Date = date;
			Description = description;
		}
	}
}
