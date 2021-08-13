using TOBA.UI.Controls.Menu;

namespace TOBA.UI.Controls.QueryManager
{
	using Common;

	using Configuration;

	using Entity;

	using Newtonsoft.Json;

	using System;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using TOBA.UI.Dialogs;

	using ListViewItem = System.Windows.Forms.ListViewItem;

	internal partial class QmPanel : ControlBase
	{
		bool _allowModParams;
		ListViewItem _nextItem;

		DateTime _nextTime;

		public QmPanel()
		{
			InitializeComponent();

			this.Load += AdvancedToolPanel_Load;
			imgStatus.Images.Add("standby", UiUtility.Get24PxImageFrom16PxImg(Properties.Resources._148));
			imgStatus.Images.Add("query", UiUtility.Get24PxImageFrom16PxImg(Properties.Resources._146));
			imgStatus.Images.Add("wait", UiUtility.Get24PxImageFrom16PxImg(Properties.Resources.clock_16));

			IsControlVisibleChanged += QmPanel_IsControlVisibleChanged;
		}

		void AdvancedToolPanel_Load(object sender, EventArgs e)
		{
			if (this.IsInDesignMode())
				return;

			tsAdd.Click += tsAdd_Click;
			tsCopy.Click += tsCopy_Click;
			tsClose.Click += tsClose_Click;
			tsQuery.Click += tsQuery_Click;
			tsStopAll.Click += (x, y) => Session.UserProfile.QueryParams.RequestStopAll();
			tsStartBatch.Click += tsStartBatch_Click;
			lstQuery.SelectedIndexChanged += lstQuery_SelectedIndexChanged;
			tsQuery.Enabled = false;
			lstQuery.MouseDoubleClick += lstQuery_MouseDoubleClick;
			tsAdd.Enabled = tsCopy.Enabled = tsClose.Enabled = _allowModParams;

			//复制到其它账户
			tsSendTo.DropDownOpening += (os, args) =>
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
							var items = lstQuery.SelectedItems
								.Cast<ListViewItem>()
								.Select(x => x.Tag as QueryParam)
								.Where(x => !x.Resign)
								.Select(x => (QueryParam)x.Clone())
								.ToArray();

							if (!items.Any())
							{
								this.ShowWarningToast("请先选择可以复制的查询啦");
								return;
							}
							items.ForEach(s.UserProfile.QueryParams.Add);
							MainForm.Instance.SelectedSession = s;
						};

						return (ToolStripItem)item;
					}).
					ToArray());

				tsNoOtherAccount.Visible = tsSendTo.DropDownItems.Count < 2;
			};


			//快速选择
			tsQs.DropDownOpening += tsQs_DropDownOpening;
			tsQsAll.Click += (x, y) => lstQuery.Items.OfType<ListViewItem>().ForEach(s => s.Checked = true);
			tsQsNone.Click += (x, y) => lstQuery.Items.OfType<ListViewItem>().ForEach(s => s.Checked = false);
			tsQsInvent.Click += (x, y) => lstQuery.Items.OfType<ListViewItem>().ForEach(s => s.Checked = !s.Checked);

			//复制粘贴
			tsCopyToClip.Click += (s, x) => CopyToClipboard();
			tsPasteFromClip.Click += (s, x) => PasteFromClipboard();
			lstQuery.KeyDown += (s, x) =>
			{
				if ((x.Modifiers & Keys.Control) == Keys.Control)
				{
					if ((x.KeyCode & Keys.C) == Keys.C)
					{
						CopyToClipboard();
					}
					else if ((x.KeyCode & Keys.V) == Keys.V)
						PasteFromClipboard();
				}
			};

			//快速设置
			tsOneKeySetAllRefresh.Click += (s, x) => lstQuery.Items.OfType<ListViewItem>().ForEach(t => ((QueryParam)t.Tag).EnableAutoPreSubmit = true);

			//出行模式
			toolStrip1.Items.Insert(toolStrip1.Items.IndexOf(tsQuery) + 1, new UserTravelSessionMenuItem(Session));

			InitList();
		}

		ListViewItem createListViewItem(QueryParam param)
		{
			var item = new ListViewItem("");
			item.SubItems.AddRange(new[]
									{
										param.Name.DefaultForEmpty("<未命名>"),
										param.FromStationName ?? "",
										param.ToStationName ?? "",
										param.DepartureDate.ToShortDateString(),
										param.EnableAutoPreSubmit ? "是" : "",
										param.LastQueryTime?.ToLongTimeString() ?? "从未",
										param.LastQuerySuccess == null ? "" : param.LastQuerySuccess.Value ? "成功" : "失败"
									});
			item.Tag = param;

			param.PropertyChanged += (s, e) => AppContext.MainForm.UiInvoke(() => param_PropertyChanged(s, e));
			param.StatusChanged += (s, e) => AppContext.MainForm.UiInvoke(() => param_StatusChanged(s, e));
			RefreshItemState(item);

			return item;
		}

		void InitList()
		{
			var qc = Session.UserProfile.QueryParams.Where(s => s.IsLoaded).ToArray();
			var items = qc.Select(createListViewItem).ToArray();
			lstQuery.Items.AddRange(items);

			Session.UserProfile.QueryParams.QueryParamAdded += qc_QueryParamAdded;
			Session.UserProfile.QueryParams.QueryParamRemoved += qc_QueryParamRemoved;
			Session.UserProfile.QueryParams.ForEach(s =>
			{
				s.LoadedChanged += s_LoadedChanged;
			});
		}

		void lstQuery_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			var item = lstQuery.GetItemAt(e.X, e.Y);
			if (item == null)
				return;

			var query = item.Tag as QueryParam;
			Session.UserProfile.QueryParams.SelectedQuery = query;
		}

		void lstQuery_SelectedIndexChanged(object sender, EventArgs e)
		{
			var q = SelectedQueryParam;
			tsQuery.Enabled = q != null && q.QueryState == QueryState.None;
			tsCopyToClip.Enabled = q != null;
		}

		void param_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			var param = sender as QueryParam;
			var item = lstQuery.Items.Cast<ListViewItem>().FirstOrDefault(s => s.Tag == sender);
			if (item == null)
				return;

			if (e.PropertyName == "LastQueryTime")
			{
				item.SubItems[6].Text = param.LastQueryTime == null ? "从未" : param.LastQueryTime.Value.ToLongTimeString();
				item.SubItems[7].Text = param.LastQuerySuccess == null ? "" : "第 " + param.QueryCount + " 次" + (param.LastQuerySuccess.Value ? "成功" : "失败");
			}
			else if (e.PropertyName == "Name")
			{
				item.SubItems[1].Text = param.Name.DefaultForEmpty("<未命名>");
			}
			else if (e.PropertyName == "FromStationName")
			{
				item.SubItems[2].Text = param.FromStationName ?? "";
			}
			else if (e.PropertyName == "ToStationName")
			{
				item.SubItems[3].Text = param.ToStationName ?? "";
			}
			else if (e.PropertyName == "DepartureDate")
			{
				item.SubItems[4].Text = param.DepartureDate.ToShortDateString();
			}
			else if (e.PropertyName == "EnableAutoPreSubmit")
			{
				item.SubItems[5].Text = param.EnableAutoPreSubmit ? "是" : "";
			}
		}

		void param_StatusChanged(object sender, EventArgs e)
		{
			var query = sender as QueryParam;
			var item = lstQuery.Items.Cast<ListViewItem>().FirstOrDefault(s => s.Tag == query);
			if (item == null)
				return;

			if (IsControlVisible)
				RefreshItemState(item);
		}

		void qc_QueryParamAdded(object sender, QueryParamEventArgs e)
		{
			e.QueryParam.LoadedChanged += s_LoadedChanged;
			if (!e.QueryParam.IsLoaded)
				return;

			lstQuery.Items.Add(createListViewItem(e.QueryParam));
		}

		void qc_QueryParamRemoved(object sender, QueryParamEventArgs e)
		{
			var queryparam = e.QueryParam;
			var item = lstQuery.Items.Cast<ListViewItem>().FirstOrDefault(s => s.Tag == e.QueryParam);
			if (item == null)
				return;

			queryparam.PropertyChanged -= param_PropertyChanged;
			queryparam.StatusChanged -= param_StatusChanged;
			queryparam.LoadedChanged -= s_LoadedChanged;
			lstQuery.Items.Remove(item);
		}

		private void QmPanel_IsControlVisibleChanged(object sender, EventArgs e)
		{
			if (!IsControlVisible)
				return;

			lstQuery.BeginUpdate();
			foreach (var param in lstQuery.Items.Cast<ListViewItem>().ToArray())
			{
				RefreshItemState(param);
			}
			lstQuery.EndUpdate();
		}

		void QuickSelectByArrival(object sender, EventArgs e)
		{
			var m = sender as ToolStripMenuItem;
			var tag = (string)m.Tag;
			lstQuery.Items.OfType<ListViewItem>().ForEach(s => s.Checked = (s.Tag as QueryParam).ToStationName == tag);
		}

		void QuickSelectByDate(object sender, EventArgs e)
		{
			var m = sender as ToolStripMenuItem;
			var date = (DateTime)m.Tag;
			lstQuery.Items.OfType<ListViewItem>().ForEach(s => s.Checked = (s.Tag as QueryParam).DepartureDate.Date == date);
		}

		void QuickSelectByDepart(object sender, EventArgs e)
		{
			var m = sender as ToolStripMenuItem;
			var tag = (string)m.Tag;
			lstQuery.Items.OfType<ListViewItem>().ForEach(s => s.Checked = (s.Tag as QueryParam).FromStationName == tag);
		}

		void QuickSelectByTrip(object sender, EventArgs e)
		{
			var m = sender as ToolStripMenuItem;
			var tag = (string)m.Tag;
			lstQuery.Items.OfType<ListViewItem>().ForEach(s => s.Checked = (((((QueryParam)s.Tag).FromStationName + " -> " + (s.Tag as QueryParam).ToStationName)) == tag));
		}

		void RefreshItemState(ListViewItem item)
		{
			if (item == null)
				return;

			var query = item.Tag as QueryParam;
			switch (query.QueryState)
			{
				case QueryState.None:
					item.ImageKey = "standby";
					ListViewResource.SwitchListViewItemStyle(item, RowStyleType.RoyalBlue);
					item.Text = "等待查票";
					break;
				case QueryState.Query:
					item.ImageKey = "query";
					ListViewResource.SwitchListViewItemStyle(item, RowStyleType.LightGreen);
					item.Text = "查票中";
					break;
				case QueryState.Wait:
					item.ImageKey = "wait";
					ListViewResource.SwitchListViewItemStyle(item, RowStyleType.Olive);
					item.Text = "休息中";
					break;
				default:
					break;
			}
		}

		void s_LoadedChanged(object sender, EventArgs e)
		{
			var param = sender as QueryParam;
			var item = lstQuery.Items.Cast<ListViewItem>().FirstOrDefault(s => s.Tag == param);

			if (param.IsLoaded)
			{
				if (item == null)
				{
					lstQuery.Items.Add(createListViewItem((param)));
				}
			}
			else
			{
				if (item != null)
				{
					param.StatusChanged -= param_StatusChanged;
					param.PropertyChanged -= param_PropertyChanged;
					lstQuery.Items.Remove(item);
				}
			}
		}

		void tsAdd_Click(object sender, EventArgs e)
		{
			if (Session.UserProfile.QueryParams.HasResignQuery)
			{
				Information("当前正在改签，无法添加其它查询。");
				return;
			}

			Session.UserProfile.QueryParams.Add(new QueryParam { IsLoaded = true });
		}

		void tsClose_Click(object sender, EventArgs e)
		{
			lstQuery.SelectedItems.Cast<ListViewItem>().Union(lstQuery.CheckedItems.Cast<ListViewItem>())
				.ForEach(s => (s.Tag as QueryParam).IsLoaded = false);
		}

		void tsCopy_Click(object sender, EventArgs e)
		{
			if (!HasSelectQuery)
				return;
			if (Session.UserProfile.QueryParams.HasResignQuery)
			{
				Information("当前正在改签，无法添加其它查询。");
				return;
			}


			var query = lstQuery.SelectedItems[0].Tag as QueryParam;
			var ip = CreateInputBox("查询名称", "复制当前查票设置为新的查询，请指定新查询的名称。", query.Name + " (1)", false);
			if (ip.ShowDialog() != DialogResult.OK)
				return;
			var q1 = query.Clone() as QueryParam;
			q1.Name = ip.InputedText;
			q1.IsPersistent = false;
			q1.IsLoaded = true;
			q1.Resign = false;

			Session.UserProfile.QueryParams.Add(q1);
		}

		void tsQs_DropDownOpening(object sender, EventArgs e)
		{
			if (lstQuery.Items.Count == 0)
			{
				tsQs.DropDownItems.OfType<ToolStripMenuItem>().ForEach(s => s.Enabled = false);
				return;
			}

			tsQs.DropDownItems.OfType<ToolStripMenuItem>().ForEach(s => s.Enabled = true);
			//按出发日期选择
			tsQsDate.DropDownItems.Clear();
			tsQsDate.DropDownItems
				.AddRange(
				lstQuery.Items.OfType<ListViewItem>()
				.Select(s => (s.Tag as QueryParam).DepartureDate.Date)
				.Distinct()
				.Select(s => new ToolStripMenuItem(s.ToShortDateString()) { Tag = s })
				.ToArray()
				);
			//按出发地选择
			tsQsDepart.DropDownItems.Clear();
			tsQsDepart.DropDownItems
				.AddRange(
				lstQuery.Items.OfType<ListViewItem>()
				.Select(s => (s.Tag as QueryParam).FromStationName)
				.Distinct()
				.Select(s => new ToolStripMenuItem(s) { Tag = s })
				.ToArray()
				);
			//按目标地选择
			tsQsArrival.DropDownItems.Clear();
			tsQsArrival.DropDownItems
				.AddRange(
				lstQuery.Items.OfType<ListViewItem>()
				.Select(s => (s.Tag as QueryParam).ToStationName)
				.Distinct()
				.Select(s => new ToolStripMenuItem(s) { Tag = s })
				.ToArray()
				);
			//按行程选择
			tsQsTrip.DropDownItems.Clear();
			tsQsTrip.DropDownItems
				.AddRange(
				lstQuery.Items.OfType<ListViewItem>()
				.Select(s => (s.Tag as QueryParam).FromStationName + " -> " + (s.Tag as QueryParam).ToStationName)
				.Distinct()
				.Select(s => new ToolStripMenuItem(s) { Tag = s })
				.ToArray()
				);

			//绑定事件
			tsQsDate.DropDownItems.Cast<ToolStripMenuItem>().ForEach(s =>
			{
				s.Click += QuickSelectByDate;
			});
			tsQsDepart.DropDownItems.Cast<ToolStripMenuItem>().ForEach(s =>
			{
				s.Click += QuickSelectByDepart;
			});
			tsQsArrival.DropDownItems.Cast<ToolStripMenuItem>().ForEach(s =>
			{
				s.Click += QuickSelectByArrival;
			});
			tsQsTrip.DropDownItems.Cast<ToolStripMenuItem>().ForEach(s =>
			{
				s.Click += QuickSelectByTrip;
			});
		}

		void tsQuery_Click(object sender, EventArgs e)
		{
			lstQuery.SelectedItems.Cast<ListViewItem>().Union(lstQuery.CheckedItems.Cast<ListViewItem>())
				.Select(s => s.Tag as QueryParam).Where(s => s.QueryState == QueryState.None && s.CanQuery().IsNullOrEmpty())
				.ForEach(s => s.OnRequestQuery());
		}

		async void tsStartBatch_Click(object sender, EventArgs e)
		{
			if (Session.UserProfile.QueryParams.Count >= 3 && !ProgramConfiguration.Instance.HideBatchQueryTip)
			{
				new Dialogs.Notification.BatchQueryTip().ShowDialog();
			}

			var hasChecked = lstQuery.Items.Cast<ListViewItem>().Any(s => s.Checked);
			var query = (hasChecked ? Session.UserProfile.QueryParams.Where(s => s.QueryState == QueryState.None && s.CanQuery().IsNullOrEmpty()) : lstQuery.Items.Cast<ListViewItem>().Where(s => s.Checked).Select(s => s.Tag as QueryParam).Where(s => s.QueryState == QueryState.None && s.CanQuery().IsNullOrEmpty())).ToArray();

			if (query.Length >= QueryConfiguration.Current.QuerySleep)
			{
				//
				this.Information($"你将要同时开始查询 {query.Length} 个查询，查询间隔为 {QueryConfiguration.Current.QuerySleep} 秒，这不安全。\n\n建议查询间隔至少为 {query.Length} 秒以上。");
			}

			foreach (var param in query)
			{
				param.OnRequestQuery();
				await Task.Delay(1000).ConfigureAwait(true);
			}
		}

		bool HasSelectQuery
		{
			get { return lstQuery.SelectedItems.Count > 0; }
		}

		QueryParam SelectedQueryParam
		{
			get
			{
				if (HasSelectQuery)
					return lstQuery.SelectedItems[0].Tag as QueryParam;
				return null;
			}
		}


		#region 从剪贴板复制粘贴

		void CopyToClipboard()
		{
			var items = lstQuery.SelectedItems.Cast<ListViewItem>().Select(s => s.Tag as QueryParam).ToArray();
			if (!items.Any())
				return;

			if (items.Any(s => s.Resign))
			{
				this.ShowWarningToast("当前正在改签呢，改签的查询复制没有意义的哦");
				return;
			}

			try
			{
				var dbobj = new DataObject();
				dbobj.SetText(Convert.ToBase64String(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(items)).Compress()));
				Clipboard.SetDataObject(dbobj);

				Information("看起来很愉快地复制成功了...");
			}
			catch (Exception)
			{
				Error("未能成功复制，发生啥了我也不知道...");
			}
		}

		void PasteFromClipboard()
		{
			var text = Clipboard.GetText();

			if (!string.IsNullOrEmpty(text))
			{
				try
				{
					//序列化
					var list = JsonConvert.DeserializeObject<QueryParam[]>(Encoding.UTF8.GetString(Convert.FromBase64String(text).Decompress()));
					foreach (var queryParam in list)
					{
						Session.UserProfile.QueryParams.Add(queryParam);
					}
				}
				catch (Exception)
				{
				}
				return;
			}
			Information("嗯...你还没复制就来粘贴，粘贴个毛线啊.....");
		}

		#endregion
	}
}
