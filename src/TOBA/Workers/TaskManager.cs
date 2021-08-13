using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace TOBA.Workers
{
	/// <summary>
	/// 后台任务管理类
	/// </summary>
	internal class TaskManager
	{
		public static TaskManager Instance
		{
			get { return _instance ?? new TaskManager(); }
		}

		AsyncOperation _operation;
		static TaskManager _instance;

		/// <summary>
		/// 创建对象实例
		/// </summary>
		TaskManager()
		{
			_instance = this;
			_operation = AsyncOperationManager.CreateOperation(this);
			_actionQueue = new Queue<BackgroundTask>();
			System.Threading.ThreadPool.QueueUserWorkItem(_ => RunQueue(), null);
		}

		Queue<BackgroundTask> _actionQueue;

		/// <summary>
		/// 将一个任务加入队列
		/// </summary>
		/// <param name="name"></param>
		/// <param name="action"></param>
		public void EnqueueTask(string name, Action action, Action callback = null)
		{
			EnqueueTask(new BackgroundTask { TaskName = name, Action = action, Callback = callback });
		}

		/// <summary>
		/// 将一个任务加入队列
		/// </summary>
		/// <param name="task"></param>
		public void EnqueueTask(BackgroundTask task)
		{
			TOBA.Events.OnMessage(this, new EventInfoArgs() { Message = "已创建后台任务：" + task.TaskName });

			_actionQueue.Enqueue(task);
			OnTaskAdded(new BackgroundTaskEventArgs(task));
		}

		/// <summary>
		/// 获得当前的任务数
		/// </summary>
		public int Count
		{
			get { return _actionQueue.Count; }
		}

		/// <summary>
		/// 任务已添加
		/// </summary>
		public event EventHandler<BackgroundTaskEventArgs> TaskAdded;

		/// <summary>
		/// 引发 <see cref="TaskAdded" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnTaskAdded(BackgroundTaskEventArgs ea)
		{
			var handler = TaskAdded;
			if (handler != null)
				handler(this, ea);
		}

		/// <summary>
		/// 任务失败
		/// </summary>
		public event EventHandler<BackgroundTaskEventArgs> TaskFailed;

		/// <summary>
		/// 引发 <see cref="TaskFailed" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnTaskFailed(BackgroundTaskEventArgs ea)
		{
			var handler = TaskFailed;
			if (handler != null)
				handler(this, ea);
		}

		/// <summary>
		/// 任务完成
		/// </summary>
		public event EventHandler<BackgroundTaskEventArgs> TaskSuccess;

		/// <summary>
		/// 引发 <see cref="TaskSuccess" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnTaskSuccess(BackgroundTaskEventArgs ea)
		{
			var handler = TaskSuccess;
			if (handler != null)
				handler(this, ea);
		}

		/// <summary>
		/// 任务开始执行
		/// </summary>
		public event EventHandler<BackgroundTaskEventArgs> TaskStarted;

		/// <summary>
		/// 引发 <see cref="TaskStarted" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnTaskStarted(BackgroundTaskEventArgs ea)
		{
			var handler = TaskStarted;
			if (handler != null)
				handler(this, ea);
		}

		/// <summary>
		/// 所有任务均已完成
		/// </summary>
		public event EventHandler AllTaskFinished;

		/// <summary>
		/// 引发 <see cref="AllTaskFinished" /> 事件
		/// </summary>
		protected virtual void OnAllTaskFinished()
		{
			var handler = AllTaskFinished;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 运行任务
		/// </summary>
		void RunQueue()
		{
			while (true)
			{
				while (_actionQueue.Count > 0)
				{
					var task = _actionQueue.Dequeue();
					TOBA.Events.OnMessage(this, new EventInfoArgs() { Message = "正在执行任务：" + task.TaskName });

					_operation.Post(_ => OnTaskStarted(new BackgroundTaskEventArgs(task)), null);
					try
					{
						task.Action();
						_operation.Post(_ => OnTaskSuccess(new BackgroundTaskEventArgs(task)), null);
						TOBA.Events.OnMessage(this, new EventInfoArgs() { Message = "已完成后台任务：" + task.TaskName });

					}
					catch (Exception ex)
					{
						TOBA.Events.OnMessage(this, new EventInfoArgs() { Message = "任务执行失败：" + task.TaskName + "，错误信息：" + ex.Message });
						Trace.TraceError(ex.ToString());
						task.Exception = ex;
						_operation.Post(_ => OnTaskFailed(new BackgroundTaskEventArgs(task)), null);
					}
					if (task.Callback != null)
						task.Callback();

					if (_actionQueue.Count == 0)
						_operation.Post(_ => OnAllTaskFinished(), null);
				}
				System.Threading.Thread.Sleep(100);
			}
		}
	}
}
