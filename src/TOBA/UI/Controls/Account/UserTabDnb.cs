using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TOBA.UI.Dialogs;

namespace TOBA.UI.Controls.Account
{
	using System.Drawing;
	using System.Windows.Forms;

	using Configuration;

	using DevComponents.DotNetBar;


	using TOBA.Account;
	using TOBA.Query;

	using Workers;

	internal class UserTabDnb : SuperTabItem, IRequireSessionInit
	{
		SuperTabControl _controlParent;
		UserMainPageContent _userPage;
		SuperTabControlPanel _panel;
		Session _session;

		public UserTabDnb(Session session, SuperTabControl parent)
		{
			InitSession(session);

			_userPage = new UserMainPageContent(session)
			{
				Dock = DockStyle.Fill,
				BackColor = SystemColors.Window
			};
			_panel = new SuperTabControlPanel();
			_panel.Controls.Add(_userPage);
			_panel.TabItem = this;
			AttachedControl = _panel;
			parent.Controls.Add(_panel);

			//set property
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
			Image = session.TemporaryMode ? Properties.Resources.cou_16_protection : session.ShadowMode ? Properties.Resources.cou_16_users : Properties.Resources.user_16;


			//find index
			_controlParent = parent;
			var last = parent.Tabs.OfType<UserTabDnb>().LastOrDefault();
			var idx = last == null ? 1 : parent.Tabs.IndexOf(last) + 1;

			parent.Tabs.Add(this, idx);
		}

		public void Remove()
		{
			//_controlParent.CloseTab(this);
			_controlParent.Tabs.Remove(this);
			_controlParent.Controls.Remove(_panel);

			//reset index
			var tab = _controlParent.Tabs.OfType<UserTabDnb>().FirstOrDefault();
			if (tab != null)
				_controlParent.SelectedTab = tab;
			else
				_controlParent.SelectedTabIndex = 0;
		}

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session
		{
			get { return _session; }
		}

		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public void InitSession(Session session)
		{
			_session = session;
			if (_userPage != null)
				_userPage.InitSession(session);
		}
	}
}
