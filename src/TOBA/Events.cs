using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA
{
	using System.Diagnostics;

	using WebLib;

	public static class Events
	{
		/// <summary>
		/// 警告信息
		/// </summary>
		public static event EventHandler<EventInfoArgs> Warning;

		/// <summary>
		/// 引发 <see cref="Warning" /> 事件
		/// </summary>
		public static void OnWarning(object sender, EventInfoArgs ea)
		{
			Trace.TraceWarning("警告：" + ea.Message + "，" + (ea.Continue ? "可以" : "不可") + "继续。附加数据：" + (ea.Data == null ? "无" : ea.Data.ToString()));

			Warning?.Invoke(sender, ea);
		}

		/// <summary>
		/// 警告信息
		/// </summary>
		public static event EventHandler<EventInfoArgs> Error;

		/// <summary>
		/// 引发 <see cref="Error" /> 事件
		/// </summary>
		public static void OnError(object sender, EventInfoArgs ea)
		{
			Trace.TraceError("错误：" + ea.Message + "，" + (ea.Continue ? "可以" : "不可") + "继续。附加数据：" + (ea.Data == null ? "无" : ea.Data.ToString()));
			var handler = Error;

			if (handler == null)
				return;
			handler(sender, ea);
		}
		/// <summary>
		/// 警告信息
		/// </summary>
		public static event EventHandler<EventInfoArgs> Message;

		/// <summary>
		/// 引发 <see cref="Message" /> 事件
		/// </summary>
		public static void OnMessage(object sender, EventInfoArgs ea)
		{
			Trace.TraceInformation("信息：" + ea.Message + "，" + (ea.Continue ? "可以" : "不可") + "继续。附加数据：" + (ea.Data == null ? "无" : ea.Data.ToString()));

			var handler = Message;
			if (handler == null)
				return;

			handler(sender, ea);
		}

		#region 系统事件

		/// <summary>
		/// 系统支持错误
		/// </summary>
		public static event EventHandler<EventInfoArgs> SystemSupportError;

		/// <summary>
		/// 引发 <see cref="SystemSupportError" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		/// <param name="ea">包含此事件的参数</param>
		public static void OnSystemSupportError(object sender, EventInfoArgs ea)
		{
			Trace.TraceError(ea.Message);
			var handler = SystemSupportError;
			if (handler != null)
				handler(sender, ea);
		}

		/// <summary>
		/// 系统已经关闭
		/// </summary>
		public static event EventHandler SystemClosed;


		/// <summary>
		/// 引发 <see cref="SystemClosed" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		public static void OnSystemClosed(object sender)
		{
			var handler = SystemClosed;
			if (handler != null)
				handler(sender, EventArgs.Empty);
		}

		#endregion

		/// <summary>
		/// IP被封锁
		/// </summary>
		public static event EventHandler IpBlocked;


		/// <summary>
		/// 引发 <see cref="IpBlocked"/> 事件
		/// </summary>
		public static void OnIpBlocked()
		{
			IpBlocked?.Invoke(null, EventArgs.Empty);
		}

		public static event EventHandler SiteAccountShouldRefresh;

		public static void OnSiteAccountShouldRefresh(object sender) { SiteAccountShouldRefresh?.Invoke(sender, EventArgs.Empty); }
	}
}
