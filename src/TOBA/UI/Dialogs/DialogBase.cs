using System;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs
{
	using Common;

	using DevComponents.DotNetBar;

	internal class DialogBase : Office2007Form, IRequireSessionInit
	{
		public DialogBase()
		{
			Session.Logout += Session_Logout;

			FormClosing += (s, e) =>
			{
				Session.Logout -= Session_Logout;
			};
		}

		private void Session_Logout(object sender, EventArgs e)
		{
			if (Session != null && sender == Session)
			{
				Close();
			}
		}

		Session _operationContext;

		/// <summary>
		/// 使当前窗口具有焦点
		/// </summary>
		protected void SetFocus()
		{
			if (Configuration.SubmitOrder.Current.AutoTopMost)
			{
				TopMost = true;
			}

			WindowState = FormWindowState.Normal;
			BringToFront();
			Activate();
		}

		/// <summary>
		/// 创建指定的控件或窗体
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetControl<T>() where T : Control
		{
			var dlg = Activator.CreateInstance(typeof(T)) as T;
			if (dlg is IRequireSessionInit)
				(dlg as IRequireSessionInit).InitSession(Session);
			return dlg;
		}

		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public virtual void InitSession(Session session)
		{
			Session = session;
		}

		/// <summary>
		/// 获得或设置状态窗口
		/// </summary>
		public IYetAnotherWaitingInfo ProgressDialog { get; set; }

		/// <summary>
		/// 获得或设置当前上下文环境
		/// </summary>
		public virtual Session Session
		{
			get { return _operationContext; }
			set
			{
				if (value == _operationContext) return;

				_operationContext = value;

				UiUtility.AttachContext(this, value);
			}
		}

		/// <summary>
		/// 查找当前会话里已经打开的相同对话框
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="session"></param>
		/// <returns></returns>
		public static T FindCurrentTypeFormInSession<T>(Session session) where T : DialogBase
		{
			return Application.OpenForms.OfType<T>().FirstOrDefault(s => s.Session == session);
		}
	}


}
