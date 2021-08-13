using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Workers
{
	/// <summary>
	/// 表示一个后台任务
	/// </summary>
	internal class BackgroundTask
	{
		/// <summary>
		/// 获得或设置任务的标题
		/// </summary>
		public string TaskName { get; set; }

		/// <summary>
		/// 获得或设置要执行的操作
		/// </summary>
		public Action Action { get; set; }

		/// <summary>
		/// 任务完成回调
		/// </summary>
		public Action Callback { get; set; }

		/// <summary>
		/// 获得在执行过程中发生的错误
		/// </summary>
		public Exception Exception { get; internal set; }
	}
}
