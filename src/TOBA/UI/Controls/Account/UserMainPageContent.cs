using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

using TOBA.Configuration;
using TOBA.Entity;
using TOBA.UI.Controls.Query;

namespace TOBA.UI.Controls.Account
{
	using BackupOrder;

	using Dialogs;

	using FSLib.Extension;

	using Order;

	internal partial class UserMainPageContent : ControlBase
	{
		public UserMainPageContent(Session session)
			: this()
		{
			InitSession(session);
			st.ReorderTabsEnabled = false;
		}

		public UserMainPageContent()
		{
			InitializeComponent();

			Disposed += UserMainPageContent_Disposed;
		}

		void UserMainPageContent_Disposed(object sender, EventArgs e)
		{
			QueryViewConfiguration.Instance.PropertyChanged -= Instance_PropertyChanged;
		}

		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public override void InitSession(Session session)
		{
			base.InitSession(session);

			LoadUserQuerys();
			session.UserProfile.QueryParams.QueryParamAdded += QueryParams_QueryParamAdded;
			session.UserProfile.QueryParams.QueryParamRemoved += QueryParams_QueryParamRemoved;

			InitUi();
			InitMenu();
		}

		void InitUi()
		{
			//ctxImg.Images.Add("contact", Properties.Resources.address_16);
			//ctxImg.Images.Add("orders", Properties.Resources.briefcase_16);
			//ctxImg.Images.Add("querying", FSLib.Windows.Properties.Resources.right);
			//ctxImg.Images.Add("querynone", Properties.Resources.calendar_16);
			//ctxImg.Images.Add("querywait", Properties.Resources.alarm_clock);
			//ctxImg.Images.Add("queryTicket", Properties.Resources.tick_16);
			//ctxImg.Images.Add("queryManager", Properties.Resources.cou_16_chart_pie);
			//ctxImg.Images.Add("monitor", Properties.Resources.monitor_16);
			//tabOrderManager.ImageKey = "contact";
			//tabPassengerManager.ImageKey = "orders";
			//tpQueryManager.ImageKey = "queryManager";
			//tpOperation.ImageKey = "monitor";

			Session.RequestShowPanel += Session_RequestShowPanel;
			Session.UserProfile.QueryParams.SelectedQueryChanged += (s, e) =>
			{
				var query = Session.UserProfile.QueryParams.SelectedQuery;
				if (query != null)
					SelectedQueryParam = query;

			};
			//tabQuery.TabPageHeaderDoubleClicked += (s, e) =>
			//{
			//	if (!ProgramConfiguration.Instance.DoubleClickHeaderToCloseTab || !(e.TabPage is Query.QueryPageContainer) || !Session.CanAddQueryParam)
			//	{
			//		return;
			//	}

			//	(e.TabPage as Query.QueryPageContainer).QueryParam.IsLoaded = false;
			//};
			st.TabStripMouseDoubleClick += (s, e) =>
			{
				var tab = st.GetTabFromPoint(e.Location);
				if (tab == null)
				{
					if (Session.UserProfile.QueryParams.HasResignQuery)
					{
						Information("当前正在改签，无法添加其它查询。");
						return;
					}

					AddQuery();
				}
			};
			st.TabStripMouseClick += (sender, args) =>
			{
				if (args.Button == MouseButtons.Right)
				{
					ctxTab.Show(PointToScreen(args.Location));
				}
			};
			//MouseDoubleClick += (s, e) => AddQuery();
			//label1.MouseDoubleClick += (s, e) => AddQuery();
			//label1.Visible = QueryViewConfiguration.Instance.DoubleClickOnEmptyAreaAddQuery && Session.UserProfile.QueryParams.Count <= 2;
			QueryViewConfiguration.Instance.PropertyChanged += Instance_PropertyChanged;
			st.TabItemClose += (s, e) =>
			{
				var page = e.Tab as QueryPageContainer;
				if (page != null)
					page.QueryParam.IsLoaded = false;
			};
		}

		void AddQuery()
		{
			//新开标签页
			Session.UserProfile.QueryParams.Add();
		}

		void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			//if ((sender as QueryViewConfiguration).IsPropertyOf(e, s => s.DoubleClickOnEmptyAreaAddQuery))
			//{
			//	label1.Visible = QueryViewConfiguration.Instance.DoubleClickOnEmptyAreaAddQuery && Session.CanAddQueryParam && Session.UserProfile.QueryParams.Count <= 2;
			//}
		}

		void Session_RequestShowPanel(object sender, GeneralEventArgs<PanelIndex> e)
		{
			var p = e.Data;

			switch (p)
			{
				case PanelIndex.QueryPanel:
					st.SelectedTab = stQueryManage;
					break;
				case PanelIndex.NotComplete:
					st.SelectedTab = stOrderManage;
					break;
				case PanelIndex.Orders:
					st.SelectedTab = stOrderManage;
					break;
				case PanelIndex.PassengerManage:
					st.SelectedTab = stPasManage;
					break;
				case PanelIndex.HbOrder:
					st.SelectedTab = stBackupOrder;
					break;
				default:
					break;
			}
		}

		#region 菜单

		void InitMenu()
		{
			mnuSaveAsTheme.Click += (s, e) =>
			{
				if (SelectedQueryParam == null) return;

				if (Question("确定将查询【" + SelectedQueryParam.Name + "】另存为出行模式吗？保存后即便没有打开也将会保存，您可以随时从菜单中打开此查询。", true))
				{
					SelectedQueryParam.IsPersistent = true;
					SelectedQueryParam.OnRequireSave();
				}
			};
			mnuChangeName.Click += (s, e) =>
			{
				var p = SelectedQueryParam;
				if (p == null) return;

				var ip = CreateInputBox("修改名称", "请输入查询的名称，这将显示为标题", p.Name, false);
				if (ip.ShowDialog() == DialogResult.OK)
				{
					p.Name = ip.InputedText;
					p.OnRequireSave();
				}
			};
			mnuAddQuery.Click += (s, e) =>
			{
				if (Session.UserProfile.QueryParams.HasResignQuery)
				{
					Information("当前正在改签，无法添加其它查询。");
					return;
				}

				Session.UserProfile.QueryParams.Add(new Entity.QueryParam() { IsLoaded = true });
			};
			mnuRemoveQuery.Click += (sender, args) =>
			{
				if (SelectedQueryParam != null)
					SelectedQueryParam.IsLoaded = false;
			};
			st.SelectedTabChanged += (s, e) =>
			{
				Session.UserProfile.QueryParams.SelectedQuery = SelectedQueryParam;

				if (st.SelectedTab == stOrderManage)
				{
					(stOrderManage.AttachedControl.Controls[0] as OrderPanel).InitLoad();
				}
				else if (st.SelectedTab == stBackupOrder)
				{
					(stBackupOrder.AttachedControl.Controls[0] as BackupOrderContainer).LoadOnDemand();
				}
			};

			//复制到其它查询
			ctxTab.Opening += (sender, args) =>
			{
				//非查询则禁止复制; 代购模式下禁止复制； 改签模式下禁止复制
				tsSendTo.Enabled = SelectedQueryTab != null && !SelectedQueryParam.Resign;
				mnuRemoveQuery.Enabled = SelectedQueryParam != null;
				mnuChangeName.Enabled = SelectedQueryParam != null;
				mnuSaveAsTheme.Enabled = SelectedQueryParam != null;
			};
			tsSendTo.DropDownOpening += (sender, args) =>
			{
				tsSendTo.DropDownItems.Cast<ToolStripMenuItem>().Skip(1).ToArray().ForEach(tsSendTo.DropDownItems.Remove);

				//加载其它用户
				var sessions = RunTime.SessionManager.Where(s => s != this.Session).ToArray();
				tsSendTo.DropDownItems.AddRange(sessions.Select(s =>
				{
					var item = new ToolStripMenuItem(s.DisplayPlainText);
					item.Image = s.TemporaryMode ? Properties.Resources.cou_16_protection : s.ShadowMode ? Properties.Resources.cou_16_users : Properties.Resources.user_16;
					item.Click += (o, eventArgs) =>
					{
						s.UserProfile.QueryParams.Add((QueryParam)SelectedQueryParam.Clone());
						MainForm.Instance.SelectedSession = s;
					};

					return (ToolStripItem)item;
				}).ToArray());

				tsNoOtherAccount.Visible = tsSendTo.DropDownItems.Count < 2;
			};
		}

		#endregion

		#region 属性

		/// <summary>
		/// 获得当前选定的查询页
		/// </summary>
		public Query.QueryPageContainer SelectedQueryTab
		{
			get { return st.SelectedTab as Query.QueryPageContainer; }
		}

		/// <summary>
		/// 获得选定的查询页
		/// </summary>
		public Query.QueryPage SelectedQueryPage
		{
			get
			{
				return SelectedQueryTab.SelectValue(s => s.AttachedControl.Controls[0] as Query.QueryPage);
			}
		}

		/// <summary>
		/// 获得选定的查询参数
		/// </summary>
		public Entity.QueryParam SelectedQueryParam
		{
			get { return SelectedQueryTab.SelectValue(s => s.QueryParam); }
			set
			{
				if (value == null)
					return;

				var tab = st.Tabs.OfType<QueryPageContainer>().FirstOrDefault(s => s.QueryParam == value);
				if (tab != null)
					st.SelectedTab = tab;
			}
		}


		void QueryParams_QueryParamRemoved(object sender, Entity.QueryParamEventArgs e)
		{
			e.QueryParam.LoadedChanged -= QueryParam_LoadedChanged;
			var tp = st.Tabs.OfType<Query.QueryPageContainer>().FirstOrDefault(s => s.QueryParam == e.QueryParam);
			if (tp != null) st.CloseTab(tp);
		}

		void QueryParams_QueryParamAdded(object sender, Entity.QueryParamEventArgs e)
		{
			e.QueryParam.LoadedChanged += QueryParam_LoadedChanged;

			if (e.QueryParam.IsLoaded)
				QueryParam_LoadedChanged(e.QueryParam, null);
		}

		void QueryParam_LoadedChanged(object sender, EventArgs e)
		{
			var param = sender as QueryParam;

			if (param.IsLoaded)
			{
				if (st.Tabs.OfType<Query.QueryPageContainer>().Any(s => s.QueryParam == param))
					return;
				var p = new Query.QueryPageContainer(Session, param, st);
				st.SelectedTab = p;
				p.Focus();
			}
			else
			{
				var page = st.Tabs.OfType<Query.QueryPageContainer>().FirstOrDefault(s => s.QueryParam == param);
				if (page != null)
				{
					st.Tabs.Remove(page);
					st.Controls.Remove(page.AttachedControl);
				}
			}
		}

		#endregion

		#region 辅助方法
		void LoadUserQuerys()
		{
			var querys = Session.UserProfile.QueryParams.Where(s => s.IsLoaded && !s.Resign).ToArray();
			if (querys.Length == 0)
			{
				var queryParam = new Entity.QueryParam();
				Session.UserProfile.QueryParams.Add(queryParam);
				queryParam.LoadedChanged += QueryParam_LoadedChanged;
				new Query.QueryPageContainer(Session, queryParam, st);
				st.SelectedTabIndex = 0;
			}
			else
			{
				Array.ForEach(querys, s => new Query.QueryPageContainer(Session, s, st));

				var resumeQuery = ProgramConfiguration.Instance.KeepQueryStateAfterRestart;
				querys.ForEach(s =>
				{
					s.LoadedChanged += QueryParam_LoadedChanged;
					if (resumeQuery && s.IsLastInQuery && s.DepartureDate.Date > DateTime.Now.Date)
						s.OnRequestQuery();
				});
				st.SelectedTabIndex = 0;
			}
		}
		#endregion
	}
}
