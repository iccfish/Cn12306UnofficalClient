namespace TOBA.UI.Dialogs.Common
{
	using Dialogs;

	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;

	partial class YetAnotherWaitingDialog : FormBase, IYetAnotherWaitingInfo
	{
		public YetAnotherWaitingDialog()
		{
			InitializeComponent();
			Shown += YetAnotherWaitingDialog_Shown;
			AsyncWork = true;
			AutoCloseOnComplete = true;
			RefreshUi();
		}

		/// <summary>
		/// 获得或设置是否使用异步来执行任务
		/// </summary>
		public bool AsyncWork { get; set; }

		public YetAnotherWaitingDialog(Action action)
			: this()
		{
			WorkCallback = action;
		}

		public YetAnotherWaitingDialog(Action<IYetAnotherWaitingInfo> action, object data)
			: this()
		{
			WorkCallbackAdvanced = action;
			Data = data;
		}


		void YetAnotherWaitingDialog_Shown(object sender, EventArgs e)
		{
			StartOperation();
		}

		/// <summary>
		/// 获得或设置是否当任务完成时自动关闭对话框
		/// </summary>
		public bool AutoCloseOnComplete { get; set; }

		/// <summary>
		/// 开始操作
		/// </summary>
		void StartOperation()
		{
			if (WorkCallback == null && WorkCallbackAdvanced == null)
			{
				_completed = true;
				return;
			}

			if (AsyncWork)
			{
				_operation = AsyncOperationManager.CreateOperation(this);
				System.Threading.ThreadPool.QueueUserWorkItem(ThreadWorker);
			}
			else ThreadWorker(null);
		}

		void ThreadWorker(object arg)
		{
			try
			{
				if (WorkCallback != null) WorkCallback();
				if (WorkCallbackAdvanced != null) WorkCallbackAdvanced(this);
			}
			catch (Exception ex)
			{
				Exception = ex;
			}

			if (_operation != null)
			{
				_operation.PostOperationCompleted(_ => OnOperationCompleted(), null);
			}
			else OnOperationCompleted();
		}

		bool _completed;
		/// <summary>
		/// 任务结束
		/// </summary>
		public event EventHandler OperationCompleted;
		/// <summary>
		/// 引发 <see cref="OperationCompleted" /> 事件
		/// </summary>
		protected virtual void OnOperationCompleted()
		{
			_completed = true;
			if (AutoCloseOnComplete)
				Close();

			if (OperationCompleted == null)
				return;
			OperationCompleted(this, EventArgs.Empty);
		}

		protected override void OnFormClosing(FormClosingEventArgs e)
		{
			if (!_completed)
			{
				e.Cancel = true;
				return;
			}

			base.OnFormClosing(e);
		}


		/// <summary>
		/// 获得或设置当前的工作回调
		/// </summary>
		public Action WorkCallback { get; set; }

		/// <summary>
		/// 获得或设置当前支持参数的工作回调
		/// </summary>
		public Action<IYetAnotherWaitingInfo> WorkCallbackAdvanced { get; set; }

		AsyncOperation _operation;
		bool _showProgressDesc;

		#region Implementation of IYetAnotherWaitingInfo

		/// <summary>
		/// 工作的标题
		/// </summary>
		public string Title
		{
			get { return lblTip.Text; }
			set { Invoke(() => { lblTip.Text = value.DefaultForEmpty("正在操作，请稍等...."); }); }
		}

		/// <summary>
		/// 获得或设置工作中出现的错误
		/// </summary>
		public Exception Exception
		{
			get;
			set;
		}

		/// <summary>
		/// 获得或设置任务是否显示为单位
		/// </summary>
		public bool ShowProgressAsSize { get; set; }


		int _taskCount;
		int _taskIndex;
		/// <summary>
		/// 总工作项数，默认为100
		/// </summary>
		public int TaskCount
		{
			get { return _taskCount; }
			set
			{
				if (_taskCount == value)
					return;

				_taskCount = value;
				RefreshProgress();
			}
		}

		/// <summary>
		/// 当前工作的项索引
		/// </summary>
		public int TaskIndex
		{
			get { return _taskIndex; }
			set
			{
				if (_taskIndex == value)
					return;

				_taskIndex = value;
				RefreshProgress();
			}
		}

		void RefreshProgress()
		{
			if (InvokeRequired)
			{
				Invoke(RefreshProgress);
				return;
			}

			progress.Maximum = Math.Max(1, _taskCount);
			progress.Style = _taskCount > 0 ? ProgressBarStyle.Continuous : ProgressBarStyle.Marquee;
			progress.Value = Math.Max(0, Math.Min(_taskIndex, progress.Maximum));

			if (ShowProgressDesc)
			{
				if (ShowProgressAsSize)
				{
					lblProgress.Text = string.Format("{0}/{1}", _taskCount.ToSizeDescription(), _taskCount.ToSizeDescription());
				}
				else
				{
					lblProgress.Text = string.Format("{0}/{1}", _taskCount.ToString("N0"), _taskCount.ToString("N0"));
				}
			}
		}

		/// <summary>
		/// 获得或设置图像，16px*16px
		/// </summary>
		public Image Icon
		{
			get { return pbIcon.Image; }
			set { Invoke(() => { pbIcon.Image = value ?? Properties.Resources._16px_loading_1; }); }
		}

		/// <summary>
		/// 获得或设置工作的结果
		/// </summary>
		public object Result
		{
			get;
			set;
		}

		/// <summary>
		/// 获得或设置工作的参数
		/// </summary>
		public object Data
		{
			get;
			set;
		}

		bool _showProgress;

		/// <summary>
		/// 获得或设置是否显示进度
		/// </summary>
		public bool ShowProgress
		{
			get { return _showProgress; }
			set
			{
				if (_showProgress == value)
					return;

				_showProgress = value;
				RefreshUi();
			}
		}

		/// <summary>
		/// 获得或设置是否显示文字描述
		/// </summary>
		public bool ShowProgressDesc
		{
			get { return _showProgressDesc; }
			set
			{
				if (_showProgressDesc == value)
					return;
				_showProgressDesc = value;
				RefreshUi();
			}
		}

		/// <summary>
		/// 刷新界面
		/// </summary>
		void RefreshUi()
		{
			if (InvokeRequired)
			{
				Invoke(RefreshUi);
				return;
			}

			progress.Visible = ShowProgress;
			lblProgress.Visible = ShowProgressDesc;
			Size = new Size(Width, 40 + (ShowProgressDesc ? 15 : 0) + (ShowProgress ? 15 : 0));
			Invalidate();
		}

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		public void Invoke(Action action)
		{
			if (InvokeRequired && this.IsHandleAvailable())
				base.Invoke(action);
			else action();
		}

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		public void Invoke<T1>(Action<T1> action, T1 p1)
		{
			if (InvokeRequired && this.IsHandleAvailable())
				base.Invoke(action, p1);
			else action(p1);
		}

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		public void Invoke<T1, T2>(Action<T1, T2> action, T1 p1, T2 p2)
		{
			if (InvokeRequired && this.IsHandleAvailable())
				base.Invoke(action, p1, p2);
			else action(p1, p2);
		}

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		public void Invoke<T1, T2, T3>(Action<T1, T2, T3> action, T1 p1, T2 p2, T3 p3)
		{
			if (InvokeRequired && this.IsHandleAvailable())
				base.Invoke(action, p1, p2, p3);
			else action(p1, p2, p3);
		}

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		public void Invoke<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 p1, T2 p2, T3 p3, T4 p4)
		{
			if (InvokeRequired && this.IsHandleAvailable())
				base.Invoke(action, p1, p2, p3, p4);
			else action(p1, p2, p3, p4);
		}

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		public TR Invoke<TR>(Func<TR> action)
		{
			if (InvokeRequired && this.IsHandleAvailable())
				return (TR)base.Invoke(action);
			else return action();
		}

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		public TR Invoke<T1, TR>(Func<T1, TR> action, T1 p1)
		{
			if (InvokeRequired && this.IsHandleAvailable())
				return (TR)base.Invoke(action, p1);
			else return action(p1);
		}

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		public TR Invoke<T1, T2, TR>(Func<T1, T2, TR> action, T1 p1, T2 p2)
		{
			return InvokeRequired && this.IsHandleAvailable() ? (TR)base.Invoke(action, p1, p2) : action(p1, p2);
		}

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		public TR Invoke<T1, T2, T3, TR>(Func<T1, T2, T3, TR> action, T1 p1, T2 p2, T3 p3)
		{
			if (InvokeRequired && this.IsHandleAvailable())
				return (TR)base.Invoke(action, p1, p2, p3);
			else return action(p1, p2, p3);
		}

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		public TR Invoke<T1, T2, T3, T4, TR>(Func<T1, T2, T3, T4, TR> action, T1 p1, T2 p2, T3 p3, T4 p4)
		{
			if (InvokeRequired && this.IsHandleAvailable())
				return (TR)base.Invoke(action, p1, p2, p3, p4);
			else return action(p1, p2, p3, p4);
		}

		#endregion

		/// <summary>
		/// 设置图标状态
		/// </summary>
		/// <param name="state"></param>
		public void SetState(ExecutionState state, string title = null)
		{
			if (InvokeRequired)
			{
				Invoke(() => SetState(state, title));
			}
			else
			{
				switch (state)
				{
					case ExecutionState.Running:
						pbIcon.Image = Properties.Resources._16px_loading_1;
						break;
					case ExecutionState.Wait:
						pbIcon.Image = Properties.Resources.clock_16;
						break;
					case ExecutionState.Ok:
						pbIcon.Image = Properties.Resources.tick_16;
						break;
					case ExecutionState.Warning:
						pbIcon.Image = Properties.Resources.warning_16;
						break;
					case ExecutionState.InterActive:
						pbIcon.Image = Properties.Resources.bubble_16;
						break;
					case ExecutionState.Block:
						pbIcon.Image = Properties.Resources.block_16;
						break;
					default:
						pbIcon.Image = Properties.Resources._16px_loading_1;
						break;
				}
				if (title != null) Title = title;
			}
		}

	}

	/// <summary>
	/// 另外一个工作提示框的等待信息
	/// </summary>
	public interface IYetAnotherWaitingInfo
	{
		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		TR Invoke<TR>(Func<TR> action);

		/// <summary>
		/// 设置图标状态
		/// </summary>
		/// <param name="state"></param>
		void SetState(ExecutionState state, string title = null);

		/// <summary>
		/// 工作的标题
		/// </summary>
		string Title { get; set; }

		/// <summary>
		/// 获得或设置工作中出现的错误
		/// </summary>
		Exception Exception { get; set; }

		/// <summary>
		/// 总工作项数，默认为100
		/// </summary>
		int TaskCount { get; set; }

		/// <summary>
		/// 当前工作的项索引
		/// </summary>
		int TaskIndex { get; set; }

		/// <summary>
		/// 获得或设置图像，16px*16px
		/// </summary>
		Image Icon { get; set; }

		/// <summary>
		/// 关闭工作对话框
		/// </summary>
		void Close();

		/// <summary>
		/// 获得或设置工作的结果
		/// </summary>
		object Result { get; set; }

		/// <summary>
		/// 获得或设置工作的参数
		/// </summary>
		object Data { get; set; }

		/// <summary>
		/// 获得或设置是否显示进度
		/// </summary>
		bool ShowProgress { get; set; }

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		void Invoke(Action action);

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		void Invoke<T1>(Action<T1> action, T1 p1);

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		void Invoke<T1, T2>(Action<T1, T2> action, T1 p1, T2 p2);

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		void Invoke<T1, T2, T3>(Action<T1, T2, T3> action, T1 p1, T2 p2, T3 p3);

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		void Invoke<T1, T2, T3, T4>(Action<T1, T2, T3, T4> action, T1 p1, T2 p2, T3 p3, T4 p4);

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		TR Invoke<T1, TR>(Func<T1, TR> action, T1 p1);

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		TR Invoke<T1, T2, TR>(Func<T1, T2, TR> action, T1 p1, T2 p2);

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		TR Invoke<T1, T2, T3, TR>(Func<T1, T2, T3, TR> action, T1 p1, T2 p2, T3 p3);

		/// <summary>
		/// 在UI上执行操作
		/// </summary>
		TR Invoke<T1, T2, T3, T4, TR>(Func<T1, T2, T3, T4, TR> action, T1 p1, T2 p2, T3 p3, T4 p4);

	}

	/// <summary>
	/// 执行的状态
	/// </summary>
	public enum ExecutionState
	{
		/// <summary>
		/// 运行中
		/// </summary>
		Running,
		/// <summary>
		/// 等待中
		/// </summary>
		Wait,
		/// <summary>
		/// 成功
		/// </summary>
		Ok,
		/// <summary>
		/// 警告
		/// </summary>
		Warning,
		/// <summary>
		/// 交互操作
		/// </summary>
		InterActive,
		/// <summary>
		/// 被阻拦或失败
		/// </summary>
		Block
	}
}
