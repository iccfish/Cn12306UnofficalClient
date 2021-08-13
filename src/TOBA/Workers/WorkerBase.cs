using System;
using System.ComponentModel;

namespace TOBA.Workers
{
	internal abstract class WorkerBase : IOperation
	{
		protected string Tag { get; set; } = "WorkerBase";

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session { get; set; }

		/// <summary>
		/// 创建 <see cref="WorkerBase" />  的新实例(WorkerBase)
		/// </summary>
		protected WorkerBase(Session session)
		{
			Session = session;
		}

		/// <summary>
		/// 创建 <see cref="WorkerBase" />  的新实例(WorkerBase)
		/// </summary>
		protected WorkerBase()
			: this(null)
		{
		}

		/// <summary>
		/// 是否正在忙碌
		/// </summary>
		public bool IsBusy
		{
			get { return _operation != null; }
		}

		/// <summary>
		/// 是否正在忙碌变化
		/// </summary>
		public event EventHandler IsBusyChanged;

		/// <summary>
		/// 引发 <see cref="IsBusyChanged" /> 事件
		/// </summary>
		protected virtual void OnIsBusyChanged()
		{
			var handler = IsBusyChanged;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 获得或设置完成操作的回调
		/// </summary>
		protected Action CompleteAction { get; set; }

		/// <summary>
		/// 获得或设置操作失败的回调
		/// </summary>
		public Action FailedAction { get; set; }


		public string StateMessage
		{
			get { return _stateMessage; }
			protected set
			{
				if (_error == value)
					return;
				_stateMessage = value;

				if (!value.IsNullOrEmpty())
					OnAction(OnProgressChanged);
			}
		}

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


		public string Error
		{
			get { return _error; }
			protected set
			{
				if (_error == value)
					return;
				_error = value;
			}
		}

		/// <summary>
		/// 获得或设置操作中出现的错误
		/// </summary>
		public Exception Exception { get; set; }

		private string _stateMessage;
		private string _error;

		protected virtual void OnAction(Action action, bool complete = false)
		{
			var operation = _operation;
			if (complete)
			{
				_operation = null;
			}

			if (action != null)
			{
				if (operation == null)
					action();
				else if (complete)
				{
					operation.Post(_ => action(), null);
					operation.PostOperationCompleted(_ => OnIsBusyChanged(), null);
				}
				else
				{
					operation.Post(_ => action(), null);
				}
			}
			else if (complete)
			{
				operation.PostOperationCompleted(_ => OnIsBusyChanged(), null);
			}
		}

		AsyncOperation _operation;

		/// <summary>
		/// 完成操作
		/// </summary>
		protected virtual void Complete()
		{
			OnAction(CompleteAction, true);
		}

		/// <summary>
		/// 获得或设置工作回调
		/// </summary>
		protected Action WorkAction { get; set; }

		/// <summary>
		/// 启动操作
		/// </summary>
		/// <param name="workeAction"></param>
		/// <param name="succAction"></param>
		/// <param name="failAction"></param>
		/// <param name="asyc">是否是异步运行</param>
		/// <returns></returns>
		protected bool RunWorker(Action workeAction = null, Action succAction = null, Action failAction = null, bool asyc = true)
		{
			if (IsBusy)
				return false;

			WorkAction = workeAction;
			CompleteAction = succAction;
			FailedAction = failAction;
			StateMessage = Error = null;
			Exception = null;

			if (WorkAction == null)
				return false;


			if (asyc)
			{
				_operation = AsyncOperationManager.CreateOperation(this);
				System.Threading.ThreadPool.QueueUserWorkItem(_ => RunInternal());
			}
			else
			{
				RunInternal();
			}
			OnIsBusyChanged();

			return true;
		}

		/// <summary>
		/// 开始运行工作操作
		/// </summary>
		protected virtual void RunInternal()
		{
			try
			{
				WorkAction();
			}
			catch (Exception ex)
			{
				Exception = ex;
				Error = ex.ToString();
				OnAction(FailedAction, true);

				return;
			}

			if (Error.IsNullOrEmpty() && Exception == null)
				Complete();
			else
			{
				if (string.IsNullOrEmpty(Error) && Exception != null)
					Error = Exception.Message;

				OnAction(FailedAction, true);
			}
		}
	}
}
