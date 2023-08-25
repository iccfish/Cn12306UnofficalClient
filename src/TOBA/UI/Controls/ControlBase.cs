using System;

namespace TOBA.UI.Controls
{
	using Dialogs.Common;

	using System.Drawing;
	using System.Windows.Forms;

	internal class ControlBase : UserControl, IRequireSessionInit
	{

		Session _operationContext;

		public ControlBase()
		{
			if (!Program.IsRunning)
				return;

			if (AppContext.MainForm != null)
			{
				AppContext.MainForm.IsWindowVisibleChanged += MainForm_IsWindowVisibleChanged;
				Disposed += (s, e) =>
				{
					AppContext.MainForm.IsWindowVisibleChanged -= MainForm_IsWindowVisibleChanged;
				};
			}
		}

		/// <summary>
		/// <see cref="IsControlVisible" /> 发生变化
		/// </summary>
		public event EventHandler IsControlVisibleChanged;

		private void MainForm_IsWindowVisibleChanged(object sender, EventArgs e)
		{
			OnIsControlVisibleChanged();
		}

		/// <summary>
		/// 引发 <see cref="IsControlVisibleChanged"/> 事件
		/// </summary>

		protected virtual void OnIsControlVisibleChanged()
		{
			IsControlVisibleChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <param name="e">包含事件数据的 <see cref="T:System.EventArgs"/>。</param>
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);

			if (!Program.IsRunning)
				return;

			OnIsControlVisibleChanged();
		}

		protected bool IsControlVisible => Visible && AppContext.MainForm.IsWindowVisible;

		/// <summary>
		/// 创建指定的控件或窗体
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetControl<T>() where T : Control
		{
			var dlg = Activator.CreateInstance(typeof(T)) as T;
			if (dlg is IRequireSessionInit sessionInit)
				sessionInit.InitSession(Session);
			return dlg;
		}

		#region Implementation of IOperation

		/// <summary>
		/// 获得或设置关联的图片
		/// </summary>
		public Image Image { get; set; }

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public virtual Session Session
		{
			get { return _operationContext; }
			private set
			{
				if (_operationContext == value) return;
				_operationContext = value;

				UiUtility.AttachContext(this, value);
			}
		}


		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public virtual void InitSession(Session session)
		{
			if (session == null || Session == session)
				return;
			Session = session;
		}

		#endregion

		public void Error(string msg) => MessageBox.Show(this, "错误", msg, MessageBoxButtons.OK, MessageBoxIcon.Error);

		public void Information(string msg) => MessageBox.Show(this, "提示", msg, MessageBoxButtons.OK, MessageBoxIcon.Information);

		public bool Question(string msg, bool yesNo = true)
		{
			if (yesNo)
			{
				return MessageBox.Show(this, msg, "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
			}

			return MessageBox.Show(this, msg, "确认", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
		}

		/// <summary>
		/// 创建提示输入对话框
		/// </summary>
		/// <param name="description">描述</param>
		/// <returns></returns>
		public static InputBox CreateInputBox(string description)
		{
			return CreateInputBox(String.Empty, description, String.Empty);
		}

		/// <summary>
		/// 创建提示输入对话框
		/// </summary>
		/// <param name="title">对话框标题</param>
		/// <param name="description">描述</param>
		/// <param name="defaultValue">默认值</param>
		/// <returns></returns>
		public static InputBox CreateInputBox(string title, string description, string defaultValue)
		{
			return CreateInputBox(title, description, defaultValue, false);
		}

		/// <summary>
		/// 创建提示输入对话框
		/// </summary>
		/// <param name="title">对话框标题</param>
		/// <param name="description">描述</param>
		/// <returns></returns>
		public static InputBox CreateInputBox(string title, string description)
		{
			return CreateInputBox(title, description, false);
		}

		/// <summary>
		/// 创建提示输入对话框
		/// </summary>
		/// <param name="title">对话框标题</param>
		/// <param name="description">描述</param>
		/// <param name="allowBlank">允许空</param>
		/// <returns></returns>
		public static InputBox CreateInputBox(string title, string description, bool allowBlank)
		{
			return CreateInputBox(title, description, String.Empty, allowBlank);
		}

		/// <summary>
		/// 创建提示输入对话框
		/// </summary>
		/// <param name="title">对话框标题</param>
		/// <param name="description">描述</param>
		/// <param name="defaultValue">默认值</param>
		/// <param name="allowBlank">允许空</param>
		/// <returns></returns>
		public static InputBox CreateInputBox(string title, string description, string defaultValue, bool allowBlank)
		{
			return new InputBox()
			{
				Text = title,
				TipMessage = description,
				InputedText = defaultValue,
				AllowBlank = allowBlank
			};
		}
	}
}
