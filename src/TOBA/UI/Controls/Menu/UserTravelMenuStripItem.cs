using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TOBA.Entity;
using TOBA.UI.Dialogs;

namespace TOBA.UI.Controls.Menu
{
	internal class UserTravelSessionMenuItem : ToolStripDropDownButton, IOperation
	{
		ToolStripMenuItem _emptyItem;

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session { get; set; }

		/// <summary>
		/// 创建 <see cref="UserTravelSessionMenuItem" />  的新实例(UserTravelSessionMenuItem)
		/// </summary>
		public UserTravelSessionMenuItem(Session session)
		{
			Session = session;
			DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
			Text = "出行模式";
			Image = Properties.Resources.briefcase_16;

			var list = session.UserProfile.QueryParams;
			//加载
			list.QueryParamAdded += (s, e) => AddQueryParam(e.QueryParam);
			list.QueryParamRemoved += (s, e) => RemoveQueryParam(e.QueryParam);

			var man = new ToolStripMenuItem("管理出行模式...");
			man.Click += (s, e) => new Dialogs.Query.QueryManager() { Session = Session }.ShowDialog();
			DropDownItems.Add(man);
			DropDownItems.Add(new ToolStripSeparator());
			DropDownItems.Add(_emptyItem = new ToolStripMenuItem("没有保存的出行模式...") { Tag = null, Enabled = false });

			list.ForEach(AddQueryParam);
		}

		void AddQueryParam(QueryParam param)
		{
			param.PersistentChanged += param_PersistentChanged;
			param.PropertyChanged += param_PropertyChanged;
			if (param.IsPersistent)
			{
				param_PersistentChanged(param, null);
			}
		}

		void param_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "Name")
			{
				var item = DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(s => s.Tag == sender);
				if (item != null)
					item.Text = (sender as QueryParam).Name.DefaultForEmpty("<未命名>");
			}
		}

		void param_PersistentChanged(object sender, EventArgs e)
		{
			var param = sender as QueryParam;

			if (param.IsPersistent)
			{
				//添加
				var item = DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(s => s.Tag == param);
				if (item == null)
				{
					item = new ToolStripMenuItem(param.Name.DefaultForEmpty("<未命名>")) { Tag = param };
					item.Click += (x, y) =>
					{
						var p = ((x as ToolStripMenuItem).Tag as QueryParam);
						p.IsLoaded = true;
						Session.UserProfile.QueryParams.SelectedQuery = p;
						MainForm.Instance.SelectedSession = Session;
					};
					DropDownItems.Add(item);
				}
				_emptyItem.Visible = false;
			}
			else
			{
				//删除
				var item = DropDownItems.OfType<ToolStripMenuItem>().FirstOrDefault(s => s.Tag == param);
				if (item != null)
					DropDownItems.Remove(item);
				_emptyItem.Visible = DropDownItems.OfType<ToolStripMenuItem>().All(s => s.Tag == null);
			}
		}

		void RemoveQueryParam(QueryParam param)
		{
			param.PersistentChanged -= param_PersistentChanged;
			param.PropertyChanged -= param_PropertyChanged;
		}
	}
	//internal class UserTravelMenuStripItem : ToolStripDropDownButton
	//{

	//ToolStripMenuItem _emptyItem;

	//public UserTravelMenuStripItem()
	//{
	//	Text = "出行模式(&T)";
	//	Image = Properties.Resources.briefcase_16;

	//	var sm = RunTime.SessionManager;

	//	Session.Logout += (s, e) => RemoveSession(s as Session);
	//	Session.UserLogined += (s, e) => AddSession(s as Session);

	//	DropDownItems.Add(_emptyItem = new ToolStripMenuItem("没有已登录的账号...") { Tag = null, Enabled = false });

	//	sm.ForEach(AddSession);
	//}

	//void RemoveSession(Session session)
	//{
	//	var item = DropDownItems.OfType<UserTravelSessionMenuItem>().FirstOrDefault(s => s.Session == session);
	//	if (item != null)
	//		DropDownItems.Remove(item);

	//	_emptyItem.Visible = !DropDownItems.OfType<ToolStripMenuItem>().Any();
	//}

	//void AddSession(Session session)
	//{
	//	var item = DropDownItems.OfType<UserTravelSessionMenuItem>().FirstOrDefault(s => s.Session == session);
	//	if (item == null)
	//	{
	//		DropDownItems.Add(new UserTravelSessionMenuItem(session));
	//	}

	//	_emptyItem.Visible = !DropDownItems.OfType<ToolStripMenuItem>().Any();
	//}
	//}
}
