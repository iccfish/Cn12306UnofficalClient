namespace TOBA.UI.Controls.Misc
{
	using FSLib.Extension;

	using System;
	using System.ComponentModel;
	using System.Drawing;
	using System.Threading;
	using System.Windows.Forms;

	using UserControl = System.Windows.Forms.UserControl;

	public partial class Loading : UserControl
	{
		public string TextInLoading { get; set; }

		public string TextLoadingError { get; set; }

		public string TextLoadingOk { get; set; }

		AsyncOperation _operation;

		/// <summary>
		/// 构造器
		/// </summary>
		public Loading()
		{
			TextInLoading = "正在加载中....";
			TextLoadingError = "加载失败....";
			TextLoadingOk = "加载成功";

			InitializeComponent();
			this.Load += Loading_Load;
			HideOnSuccess = true;
			KeepCenter = true;
		}

		void Loading_Load(object sender, EventArgs e)
		{
			if (KeepCenter && !this.IsInDesignMode())
				this.KeepCenter();
		}

		public string LoadingText
		{
			get { return label1.Text; }
			set { label1.Text = value; }
		}

		/// <summary>
		/// 当请求加载时会触发此事件
		/// </summary>
		public event EventHandler<GeneralEventArgs<Action<Action>>> RequestLoad;


		/// <summary>
		/// 引发 <see cref="RequestLoad" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnRequestLoad(GeneralEventArgs<Action<Action>> ea)
		{
			var handler = RequestLoad;
			if (handler != null)
				handler(this, ea);
		}


		/// <summary>
		/// 当加载成功时会触发此事件
		/// </summary>
		public event EventHandler LoadSuccess;

		/// <summary>
		/// 引发 <see cref="LoadSuccess" /> 事件
		/// </summary>
		protected virtual void OnLoadSuccess()
		{
			SetLoadingSuccess();
			var handler = LoadSuccess;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 当加载失败时会触发此事件
		/// </summary>
		public event EventHandler LoadFailed;

		/// <summary>
		/// 引发 <see cref="LoadFailed" /> 事件
		/// </summary>
		protected virtual void OnLoadFailed()
		{
			SetLoadingError();
			var handler = LoadFailed;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 获得在加载过程中遇到的错误
		/// </summary>
		[System.ComponentModel.Browsable(false)]
		public Exception Exception { get; private set; }

		/// <summary>
		/// 获得或设置是否当加载成功时自动隐藏
		/// </summary>
		[DefaultValue(true)]
		public bool HideOnSuccess { get; set; }

		/// <summary>
		/// 获得或设置是否保持在父控件上居中。已经显示后，设置无效。
		/// </summary>
		[DefaultValue(true)]
		public bool KeepCenter { get; set; }

		/// <summary>
		/// 设置已经加载成功
		/// </summary>
		public void SetLoadingSuccess(string text = null)
		{
			BringToFront();
			BackColor = Color.FromArgb(0xE7, 0xFF, 0xE9);
			TextLoadingOk = LoadingText = text.DefaultForEmpty(TextLoadingOk);
			pictureBox1.Image = Properties.Resources.cou_32_accept;

			if (HideOnSuccess)
				Hide();
		}

		/// <summary>
		/// 设置加载状态为失败
		/// </summary>
		public void SetLoadingError(string text = null)
		{
			BringToFront();
			BackColor = Color.FromArgb(0xFF, 0xE7, 0xEB);
			TextLoadingError = LoadingText = text.DefaultForEmpty(TextLoadingError);
			pictureBox1.Image = Properties.Resources.cou_32_block;
		}

		/// <summary>
		/// 重置状态
		/// </summary>
		public void Reset(string text = null)
		{
			BringToFront();
			BackColor = SystemColors.Window;
			ForeColor = SystemColors.WindowText;
			TextInLoading = LoadingText = text.DefaultForEmpty(TextInLoading);
			pictureBox1.Image = Properties.Resources._32px_loading_1;
		}

		/// <summary>
		/// 设置显示信息
		/// </summary>
		/// <param name="backColor"></param>
		/// <param name="message"></param>
		/// <param name="image"></param>
		public void SetMessage(Color backColor, string message, Image image)
		{
			BringToFront();
			BackColor = backColor;
			LoadingText = message;
			pictureBox1.Image = image;
		}

		/// <summary>
		/// 设置显示信息
		/// </summary>
		/// <param name="backColor"></param>
		/// <param name="message"></param>
		/// <param name="image"></param>
		public void SetMessage(Color backColor, Color foreColor, string message, Image image)
		{
			BringToFront();
			BackColor = backColor;
			ForeColor = foreColor;
			LoadingText = message;
			pictureBox1.Image = image;
		}

		/// <summary>
		/// 调用事件
		/// </summary>
		/// <param name="action"></param>
		/// <param name="complete"></param>
		void OnAction(Action action, bool complete = false)
		{
			if (_operation == null)
				return;

			if (complete)
			{
				_operation.PostOperationCompleted(_ => action(), null);
				_operation = null;
			}
			else
			{
				_operation.Post(_ => action(), null);
			}
		}

		public void Reload()
		{
			if (_operation != null)
				return;

			_operation = AsyncOperationManager.CreateOperation(null);
			Reset();
			Show();
			ThreadPool.QueueUserWorkItem(obj =>
			{
				try
				{
					OnRequestLoad(new GeneralEventArgs<Action<Action>>(_ => OnAction(_)));
					OnAction(OnLoadSuccess, true);
				}
				catch (Exception ex)
				{
					Exception = ex;
					OnAction(OnLoadFailed, true);
				}
				finally
				{
					_operation = null;
				}
			});
		}

		#region Overrides of ContainerControl

		/// <param name="e">包含事件数据的 <see cref="T:System.EventArgs"/>。</param>
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);

			if (Parent == null)
			{

			}
		}

		#endregion
	}
}
