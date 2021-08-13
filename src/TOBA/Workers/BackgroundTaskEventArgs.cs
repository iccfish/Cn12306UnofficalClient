using System;

namespace TOBA.Workers
{
	/// <summary>
	/// 包含任务数据的事件对象
	/// </summary>
	internal class BackgroundTaskEventArgs : EventArgs
	{
		/// <summary>
		/// 获得关联的任务
		/// </summary>
		public BackgroundTask Task { get; private set; }

		/// <summary>
		/// 创建 <see cref="BackgroundTaskEventArgs" />  的新实例(BackgroundTaskEventArgs)
		/// </summary>
		public BackgroundTaskEventArgs(BackgroundTask task)
		{
			Task = task;
		}
	}
}