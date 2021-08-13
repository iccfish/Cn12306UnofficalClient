using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Query
{
	using System.Drawing;

	using DevComponents.DotNetBar;

	using Entity;

	using TabControl = System.Windows.Forms.TabControl;

	internal class QueryPageContainer : SuperTabItem, IOperation
	{
		Session _session;
		public Entity.QueryParam QueryParam { get; private set; }

		/// <summary>
		/// 获得查询页
		/// </summary>
		public QueryPage QueryPage { get; private set; }

		SuperTabControlPanel _panel;

		SuperTabControl _tabControl;


		/// <summary>
		/// 创建 <see cref="QueryPageContainer" />  的新实例(QueryPageContainer)
		/// </summary>
		public QueryPageContainer(Session session, Entity.QueryParam queryParam, SuperTabControl control)
		{
			Session = session;
			QueryParam = queryParam;

			if (!Program.IsRunning)
				return;

			QueryPage = new QueryPage(Session, queryParam)
			{
				Dock = DockStyle.Fill,
				BackColor = SystemColors.Window
			};
			_panel = new SuperTabControlPanel();
			_panel.Controls.Add(QueryPage);
			AttachedControl = _panel;
			_panel.TabItem = this;
			_tabControl = control;
			var idx = control.Tabs.OfType<QueryPageContainer>().LastOrDefault();
			_tabControl.CreateTab(this, _panel, idx == null ? 0 : control.Tabs.IndexOf(idx) + 1);

			Text = queryParam.Name;
			Image = Properties.Resources.calendar_16;
			DataBindings.Add("Text", queryParam, "Name", false, DataSourceUpdateMode.OnPropertyChanged);
			BindQueryState();

			EnableImageAnimation = false;

			this.DoubleClick += QueryPageContainer_DoubleClick;
		}

		private void QueryPageContainer_DoubleClick(object sender, EventArgs e)
		{
			//复制查询
			var q1 = QueryParam.Clone() as QueryParam;
			q1.IsPersistent = false;
			q1.IsLoaded = true;
			q1.Resign = false;
			Session.UserProfile.QueryParams.Add(q1);
		}

		void BindQueryState()
		{
			QueryParam.PropertyChanged += QueryParam_PropertyChanged;
			RefreshTicketStatus();
			RefreshQueryStatus();
		}

		void QueryParam_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (Parent == null)
				return;

			if (e.PropertyName == "HasTicket")
				RefreshTicketStatus();
			if (e.PropertyName == "QueryState")
				RefreshQueryStatus();
		}

		void RefreshTicketStatus()
		{
			if (Parent == null)
				return;

			if (QueryParam.HasTicket)
			{
				Image = Properties.Resources.tick_16;
			}
		}

		void RefreshQueryStatus()
		{
			if (Parent == null || !_tabControl.Tabs.Contains(this))
				return;

			var state = QueryParam.QueryState;
			if (QueryParam.QueryState == QueryState.None && QueryParam.HasTicket)
				return;

			switch (state)
			{
				case QueryState.None:
					Image = Properties.Resources.calendar_16;
					break;
				case QueryState.Query:
					Image = Properties.Resources._146;
					break;
				case QueryState.Wait:
					Image = Properties.Resources.clock_16;
					break;
				default:
					break;
			}
		}

		#region Implementation of IOperation

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session
		{
			get { return _session; }
			set
			{
				_session = value;

				if (AttachedControl != null)
				{
					if (AttachedControl is IRequireSessionInit)
						(AttachedControl as IRequireSessionInit).InitSession(value);
					UiUtility.AttachContext(AttachedControl, value);

				}
			}
		}

		#endregion
	}
}
