using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TOBA.UI.Dialogs;

namespace TOBA.UI.Controls.Account
{
	using System.Windows.Forms;
	using TOBA.Account;
	using Workers;

	/// <summary>
	/// 表示用户登录后的一个标签页
	/// </summary>
	internal class UserTab : TabPage, IRequireSessionInit
	{
		Session _operationContext;

		#region Implementation of IOperation

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session
		{
			get { return _operationContext; }
			private set
			{
				_operationContext = value;
				Controls
					.Cast<Control>()
					.OfType<IRequireSessionInit>()
					.ForEach(s => s.InitSession(value));
			}
		}

		public void InitSession(Session session)
		{
			Session = session;
		}

		#endregion

		public UserMainPageContent MainPage
		{
			get;
			private set;
		}

		public UserTab(Session session)
		{
			MainPage = new UserMainPageContent(session)
			{
				Dock = DockStyle.Fill
			};
			Controls.Add(MainPage);

			Text = session.DisplayText;
			session.UserKeyData.DisplayNameChanged += (s, e) =>
			{
				var action = new Action(() =>
				{
					Text = Session.DisplayText;
				});
				AppContext.MainForm.UiInvoke(action);
			};
			TaskManager.Instance.EnqueueTask("获得账号【" + session.UserName + "】的注册用户姓名", () => new GetDisplayNameWorker() { Session = Session }.Run());
			ImageIndex = session.TemporaryMode ? 5 : 1;

			Session = session;
		}
	}
}
