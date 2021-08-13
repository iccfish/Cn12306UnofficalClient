using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Workers
{
	using System.ComponentModel;
	using System.Threading;

	internal abstract class TaskBase
	{
		/// <summary>
		/// 获得同步对象
		/// </summary>
		public System.Threading.AutoResetEvent WaitHandle
		{
			get { return _waitHandler ?? (_waitHandler = new AutoResetEvent(false)); }
		}

		/// <summary>
		/// 任务标题
		/// </summary>
		public string TaskTitle { get; set; }

		/// <summary>
		/// 任务描述
		/// </summary>
		public string TaskDescription { get; set; }

		/// <summary>
		/// 任务图标
		/// </summary>
		public System.Drawing.Image TaskIcon { get; set; }

		/// <summary>
		/// 获得或设置任务的总数
		/// </summary>
		public int TaskLength { get; set; }

		/// <summary>
		/// 获得或设置任务的进度
		/// </summary>
		public int TaskProgress { get; set; }

		/// <summary>
		/// 获得或设置是否异步执行
		/// </summary>
		public bool AsyncRun
		{
			get { return _asyncRun; }
			set
			{
				if (_operation != null) throw new InvalidOperationException();
				_asyncRun = value;
			}
		}

		/// <summary>
		/// 工作进度发生变化
		/// </summary>
		public event EventHandler ProgressChanged;

		/// <summary>
		/// 引发 <see cref="ProgressChanged" /> 事件
		/// </summary>
		protected virtual void OnProgressChanged()
		{
			var handler = ProgressChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 工作描述发生变化
		/// </summary>
		public event EventHandler DescriptionChanged;

		/// <summary>
		/// 引发 <see cref="DescriptionChanged" /> 事件
		/// </summary>
		protected virtual void OnDescriptionChanged()
		{
			var handler = DescriptionChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 任务开始执行
		/// </summary>
		public event EventHandler Start;

		/// <summary>
		/// 引发 <see cref="Start" /> 事件
		/// </summary>
		protected virtual void OnStart()
		{
			var handler = Start;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 任务完成
		/// </summary>
		public event EventHandler Done;

		/// <summary>
		/// 引发 <see cref="Done" /> 事件
		/// </summary>
		protected virtual void OnDone()
		{
			var handler = Done;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 任务失败
		/// </summary>
		public event EventHandler Fail;

		/// <summary>
		/// 引发 <see cref="Fail" /> 事件
		/// </summary>
		protected virtual void OnFail()
		{
			var handler = Fail;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 操作完成
		/// </summary>
		public event EventHandler Finish;

		/// <summary>
		/// 引发 <see cref="Finish" /> 事件
		/// </summary>
		protected virtual void OnFinish()
		{
			if (WaitHandle != null) WaitHandle.Set();
			var handler = Finish;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 获得在执行过程中发生的错误
		/// </summary>
		public Exception Exception { get; protected set; }


		private bool _asyncRun;
		AsyncOperation _operation;
		AutoResetEvent _waitHandler;

		/// <summary>
		/// 获得是否正在执行
		/// </summary>
		public bool IsBusy { get { return _operation != null; } }

		/// <summary>
		/// 获得操作是否成功
		/// </summary>
		public bool Success { get; protected set; }

		public virtual void Run()
		{
			if (_operation != null)
				throw new InvalidOperationException();

			_operation = AsyncOperationManager.CreateOperation(null);
			Success = true;
			if (AsyncRun)
			{
				System.Threading.ThreadPool.QueueUserWorkItem(_ => InnerRun());
			}
			else
				InnerRun();
		}

		/// <summary>
		/// 核心任务运行
		/// </summary>
		void InnerRun()
		{
			try
			{
				_operation.Post(_ => OnStart(), null);
				RunCore();

			}
			catch (Exception ex)
			{
				Exception = ex;
				_operation.Post(_ => OnFail(), null);
			}
			Success &= Exception == null;
			if (Success)
				_operation.Post(_ => OnDone(), null);
			else
				_operation.Post(_ => OnFail(), null);


			_operation.PostOperationCompleted(_ => OnFinish(), null);
		}

		/// <summary>
		/// 核心任务运行
		/// </summary>
		protected abstract void RunCore();

		/// <summary>
		/// 获得上下文的环境
		/// </summary>
		public Session Context { get; set; }

		/// <summary>
		/// 绑定启动事件
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		public TaskBase OnStart(Action<TaskBase> action)
		{
			if (action != null)
				Start += (s, e) => action(this);

			return this;
		}
		/// <summary>
		/// 绑定启动事件
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		public TaskBase OnDone(Action<TaskBase> action)
		{
			if (action != null)
				Done += (s, e) => action(this);

			return this;
		}

		/// <summary>
		/// 绑定启动事件
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		public TaskBase OnFinish(Action<TaskBase> action)
		{
			if (action != null)
				Finish += (s, e) => action(this);

			return this;
		}

		/// <summary>
		/// 绑定启动事件
		/// </summary>
		/// <param name="action"></param>
		/// <returns></returns>
		public TaskBase OnFail(Action<TaskBase> action)
		{
			if (action != null)
				Fail += (s, e) => action(this);

			return this;
		}

		/// <summary>
		/// 等待当前任务完成
		/// </summary>
		public void Wait()
		{
			if (!AsyncRun) return;
			WaitHandle.WaitOne();
		}

	}
}
