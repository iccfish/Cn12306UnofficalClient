using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms.VisualStyles;

using TOBA.Configuration;

namespace TOBA.UI.Controls.Query
{
	using ComponentOwl.BetterListView;

	using Data;

	using FSLib.Extension;

	using Popup;

	using ResultSubItems;

	using System.ComponentModel;
	using System.Diagnostics;
	using System.Drawing;
	using System.Windows.Forms;

	using TOBA.Query.Entity;

	internal class QueryResult : BetterListView, IRequireSessionInit, IDisposable
	{
		private Entity.QueryParam _queryParam1;
		private Entity.QueryParam _queryParam;
		BetterListViewGroup _notiket = new BetterListViewGroup("notticket", "无票") { HeaderAlignmentHorizontal = TextAlignmentHorizontal.Center };
		BetterListViewGroup _ignored = new BetterListViewGroup("ignored", "已被过滤车次") { HeaderAlignmentHorizontal = TextAlignmentHorizontal.Center };
		BetterListViewGroup _valid = new BetterListViewGroup("valid", "可定车次") { HeaderAlignmentHorizontal = TextAlignmentHorizontal.Center };
		BetterListViewGroup _noneedticket = new BetterListViewGroup("notsell", "指定的席别无票车次") { HeaderAlignmentHorizontal = TextAlignmentHorizontal.Center };
		BetterListViewGroup _underControl = new BetterListViewGroup("undercontrol", "受控未售票车次") { HeaderAlignmentHorizontal = TextAlignmentHorizontal.Center };
		Dictionary<string, BetterListViewGroup> _hourSellListGroup;
		private ContextMenuStrip ctxResult;
		private IContainer components;
		private ToolStripMenuItem tsUseGroup;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem tsIconType;
		private ToolStripMenuItem tsIconTypeT;
		private ToolStripMenuItem tsIconTypeS;
		Font[] _fonts;
		Configuration.QueryViewConfiguration _viewConfiguration;
		private ImageList imglist;
		private ToolStripMenuItem tsmiAddToSubmit;
		Dictionary<string, QueryResultItem> _origianlResult;

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public Font[] RowFonts { get { return _fonts; } }

		/// <summary>
		/// 获得或设置是否禁止菜单
		/// </summary>
		public bool DisableContextMenu { get; set; }

		/// <summary>
		/// 是否禁用分组设置
		/// </summary>
		public bool IgnoreGroupSetting { get; set; }

		/// <summary>
		/// 创建 <see cref="QueryResult" />  的新实例(QueryResult)
		/// </summary>
		public QueryResult()
		{
			InitializeComponent();

			if (!Program.IsRunning)
				return;

			_viewConfiguration = Configuration.QueryViewConfiguration.Instance;


			InitUi();
			InitConfigInfo();
		}

		private bool _showStartAndEndStation;

		public bool ShowStartAndEndStation
		{
			get { return _showStartAndEndStation; }
			set
			{
				if (value == _showStartAndEndStation)
					return;

				_showStartAndEndStation = value;
				InitStartAndEndStationColumn();
			}
		}

		void InitStartAndEndStationColumn()
		{
			if (ShowStartAndEndStation)
			{
				Columns.Insert(3, new BetterListViewColumnHeader("") { Text = "终到站", Width = 70, Style = BetterListViewColumnHeaderStyle.Sortable, SortMethod = BetterListViewSortMethod.Auto, AlignHorizontal = TextAlignmentHorizontal.Center });
				Columns.Insert(1, new BetterListViewColumnHeader("") { Text = "始发站", Width = 70, Style = BetterListViewColumnHeaderStyle.Sortable, SortMethod = BetterListViewSortMethod.Auto, AlignHorizontal = TextAlignmentHorizontal.Center });
			}
			else
			{
				Columns.RemoveAt(4);
				Columns.RemoveAt(1);
			}

			Items.Clear();
			Groups.ForEach(s => s.Items.Clear());
		}

		void InitUi()
		{
			SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
			SuspendLayout();
			BeginUpdate();

			AllowAutoToolTips = true;
			CacheImages = true;
			DoubleBuffered = true;
			GridLines = (BetterListViewGridLines)QueryViewConfiguration.Instance.QueryResultListGridLine;
			SmoothScroll = true;

			Columns.AddRange(new[]{
				new BetterListViewColumnHeader(""){ Text="车次", Width=120,Tag="TC", AlignHorizontal = TextAlignmentHorizontal.Center},
				new BetterListViewColumnHeader(""){ Text="发站/时间", Width=80,Tag="TS", AlignHorizontal = TextAlignmentHorizontal.Center},
				new BetterListViewColumnHeader(""){ Text="到站/时间", Width=80, Tag="TR", AlignHorizontal = TextAlignmentHorizontal.Center},
				new BetterListViewColumnHeader(""){ Text="天数/历时", Width=70,Tag="TE", AlignHorizontal = TextAlignmentHorizontal.Center}
			});
			Columns.AddRange(_viewConfiguration.SeatOrder.Select(s => new BetterListViewColumnHeader("") { Text = ParamData.SeatType[s], Width = ParamData.SeatType[s].Length > 3 ? 65 : 53, Tag = "S" + s, AlignHorizontal = TextAlignmentHorizontal.Center }).ToArray());
			Columns.Add(new BetterListViewColumnHeader("") { Text = "备注", Width = 120, AlignHorizontal = TextAlignmentHorizontal.Center });
			Groups.AddRange(new[]
							{
								_valid,
								_noneedticket,
								_notiket,
								_ignored,
								_underControl
							});
			_hourSellListGroup = new Dictionary<string, BetterListViewGroup>();
			ShowGroups = tsUseGroup.Checked = _viewConfiguration.ShowGroups;
			tsUseGroup.CheckedChanged += (s, e) =>
			{
				_viewConfiguration.ShowGroups = tsUseGroup.Checked;
				ShowGroups = tsUseGroup.Checked;
				if (ShowGroups)
				{
					var items = Items.Cast<QueryResultListViewItem>().ToArray();
					Items.Clear();
					items.ForEach(AssignItemGroup);
					Items.AddRange(items);
				}
			};
			RefreshColumnVisibility();

			//图标状态
			if (_viewConfiguration.UseStatusIcon)
			{
				tsIconTypeS.Checked = true;
			}
			else
			{
				tsIconTypeT.Checked = true;
			}
			tsIconTypeT.Click += (s, e) =>
			{
				if (tsIconTypeT.Checked) return;

				tsIconTypeT.Checked = true;
				tsIconTypeS.Checked = false;
				_viewConfiguration.UseStatusIcon = false;
				RefreshItemIcon();
			};
			tsIconTypeS.Click += (s, e) =>
			{
				if (tsIconTypeS.Checked) return;

				tsIconTypeS.Checked = true;
				tsIconTypeT.Checked = false;
				_viewConfiguration.UseStatusIcon = true;
				RefreshItemIcon();
			};
			SortedColumnsRowsHighlight = BetterListViewSortedColumnsRowsHighlight.ShowAlways;
			ColorSortedColumn = Color.FromArgb(255, 245, 245, 245);
			MultiSelect = false;
			AutoSizeItemsInDetailsView = true;
			ctxResult.Opening += (s, e) =>
			{
				var item = GetSubItemAt(PointToClient(MousePosition));
				var row = (QueryResultListViewItem)item?.Item;

				if (row == null)
				{
					tsmiAddToSubmit.Tag = null;
					tsmiAddToSubmit.Enabled = false;
					tsmiAddToSubmit.Text = "添加所选车次到预定列表...";
				}
				else
				{
					tsmiAddToSubmit.Enabled = true;

					var ticketItem = item as TicketCellSubItem;
					if (ticketItem == null || row.ResultItem.TicketCount.GetTicketData(ticketItem.Code) == null)
					{
						tsmiAddToSubmit.Text = $"添加车次 [{row.ResultItem.Code}] 到预定列表";
						tsmiAddToSubmit.Tag = Tuple.Create(row.ResultItem, (char?)null);
					}
					else
					{
						tsmiAddToSubmit.Text = $"添加车次 [{row.ResultItem.Code}] 的 [{ParamData.GetSeatTypeName(ticketItem.Code)}] 到预定列表";
						tsmiAddToSubmit.Tag = Tuple.Create(row.ResultItem, (char?)ticketItem.Code);
					}
				}

				if (DisableContextMenu)
					e.Cancel = true;
			};
			//添加到预定列表
			tsmiAddToSubmit.Click += (sender, args) =>
			{
				var (train, code) = (Tuple<QueryResultItem, char?>)tsmiAddToSubmit.Tag;

				QueryParam.AutoPreSubmitConfig.AddTrainCode(train.Code);
				if (code.HasValue)
					QueryParam.AutoPreSubmitConfig.AddSeat(code.Value);
			};

			//排序
			InitSortInfo();

			//拖放支持
			InitDragDrop();

			//停靠站提示
			InitTrainStopPrompt();

			EndUpdate();
			ResumeLayout();

			AppContext.MainForm.IsWindowVisibleChanged += DetectReloadData;
			VisibleChanged += DetectReloadData;
		}

		private void DetectReloadData(object sender, EventArgs e)
		{
			if (Visible && AppContext.HostForm.Visible && AppContext.HostForm.WindowState != FormWindowState.Minimized && _lastData != null)
			{
				LoadTrainResult(_lastData.Item1, _lastData.Item2);
			}
		}

		void InitConfigInfo()
		{
			QueryViewConfiguration.Instance.PropertyChanged += Instance_PropertyChanged;
		}

		void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "QueryResultListGridLine")
				GridLines = (BetterListViewGridLines)QueryViewConfiguration.Instance.QueryResultListGridLine;
			else if (e.PropertyName == nameof(QueryViewConfiguration.HideResultColumnIfNotSelected))
				RefreshColumnVisibility();
		}

		/// <summary>
		/// 刷新列可见性
		/// </summary>
		public void RefreshColumnVisibility()
		{
			if (QueryParam == null || Columns.Count == 0)
				return;

			var seatClasses = QueryParam.SelectedSeatClass;
			lock (seatClasses)
			{

				var qvCfg = QueryViewConfiguration.Instance;

				BeginUpdate();
				foreach (BetterListViewColumnHeader column in Columns)
				{
					if (column.Tag == null)
						continue;

					var tag = column.Tag as string;
					if (tag[0] != 'S')
						continue;

					column.Visible = !qvCfg.HideResultColumnIfNotSelected || seatClasses.Count == 0 || seatClasses.Contains(tag[1]);
				}
			}
			EndUpdate();
		}

		void InitSortInfo()
		{
			var columns = Columns.ToArray();
			foreach (var column in columns)
			{
				column.SortMethod = BetterListViewSortMethod.Auto;
				column.Style = false && column.Tag == null ? BetterListViewColumnHeaderStyle.Unsortable : BetterListViewColumnHeaderStyle.Sortable;
			}

			var index = Configuration.QueryViewConfiguration.Instance.SortInfo.Split('|');
			var list = new BetterListViewSortList();
			index.ForEach(s =>
			{
				var arg = s.Split(',');
				var idx = columns.FindIndex(x => x.Tag as string == arg[0]);
				if (idx >= 0 && idx < Columns.Count)
					list.Add(idx, arg[1] == "0");
			});
			SortList = list;
			ItemComparer = new QueryResultItemSorter();

			AfterItemSort += (s, e) =>
			{
				if (!e.ColumnClicked)
					return;
				Configuration.QueryViewConfiguration.Instance.SortInfo = SortList.Select(x => Columns[x.ColumnIndex].Tag?.ToString() + "," + (x.OrderAscending ? "0" : "1")).JoinAsString("|");
			};
		}

		void InitDragDrop()
		{
			AllowDrag = true;
			AllowDrop = true;
			AllowedDragEffects = (DragDropEffects.Copy | DragDropEffects.Move | DragDropEffects.Link | DragDropEffects.Scroll);
			ItemDrag += (s, e) =>
			{
				if (e.ItemDragData.Items.Count > 0 && e.ItemDragData.Items[0] is QueryResultListViewItem)
				{
					var location = PointToClient(Control.MousePosition);
					var subitem = (e.ItemDragData.Items[0] as QueryResultListViewItem).GetSubItemAt(location.X, location.Y);
					Debug.WriteLine("==>" + (subitem?.Text ?? ""));
					var ticketSubItem = subitem as ResultSubItems.TicketCellSubItem;

					DoDragDrop(Tuple.Create(e.ItemDragData.Items[0] as QueryResultListViewItem, ticketSubItem), DragDropEffects.Link);
				}
			};
		}

		/// <summary>
		/// 获得或设置查询参数
		/// </summary>
		public Entity.QueryParam QueryParam
		{
			get { return _queryParam1; }
			private set
			{
				_queryParam1 = value;
				if (_trainStopPrompt != null)
					_trainStopPrompt.Query = value;
				(ItemComparer as QueryResultItemSorter).QueryParam = value;
				RefreshColumnVisibility();
			}
		}

		public void Init(Entity.QueryParam param)
		{
			QueryParam = param;
			if (_fonts == null) Font = Font;
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override System.Drawing.Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;

				if (!Program.IsRunning || value == null || _viewConfiguration == null)
					return;

				_priceFont = new Font("Verdana", 9.0F, GraphicsUnit.Pixel);

				_fonts = new[]
				{
					new Font(value, _viewConfiguration.ValidFontStyle),
					new Font(value, _viewConfiguration.NoTicketFontStyle),
					new Font(value, _viewConfiguration.NotSellFontStyle),
					new Font(value, _viewConfiguration.NotNeedFontStyle),
					new Font(value, _viewConfiguration.NotAvailableFontStyle),
					new Font(value.FontFamily, 12.0F, _viewConfiguration.ValidFontStyle | FontStyle.Bold),
					new Font(value.FontFamily, 12.0F, _viewConfiguration.NoTicketFontStyle | FontStyle.Bold),
					new Font(value.FontFamily, 12.0F, _viewConfiguration.NotSellFontStyle | FontStyle.Bold),
					new Font(value.FontFamily, 12.0F, _viewConfiguration.NotNeedFontStyle | FontStyle.Bold),
					value
				};
			}
		}

		#region Implementation of IOperation

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session { get; private set; }

		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public void InitSession(Session session)
		{
			Session = session;
			if (_trainStopPrompt != null)
				_trainStopPrompt.InitSession(session);

		}

		#endregion

		#region 系统界面处理

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]

		public BetterListViewColumnHeaderCollection Columns
		{
			get
			{
				return base.Columns;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public BetterListViewGroupCollection Groups
		{
			get
			{
				return base.Groups;
			}
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeShowGroups()
		{
			return false;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeView()
		{
			return false;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeSession()
		{
			return false;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		internal virtual bool ShouldSerializeFullRowSelect()
		{
			return false;
		}

		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(QueryResult));
			this.ctxResult = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiAddToSubmit = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsUseGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.tsIconType = new System.Windows.Forms.ToolStripMenuItem();
			this.tsIconTypeT = new System.Windows.Forms.ToolStripMenuItem();
			this.tsIconTypeS = new System.Windows.Forms.ToolStripMenuItem();
			this.imglist = new System.Windows.Forms.ImageList(this.components);
			this.ctxResult.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			this.SuspendLayout();
			// 
			// ctxResult
			// 
			this.ctxResult.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsmiAddToSubmit,
			this.toolStripSeparator1,
			this.tsUseGroup,
			this.tsIconType});
			this.ctxResult.Name = "ctxResult";
			this.ctxResult.Size = new System.Drawing.Size(166, 76);
			// 
			// tsmiAddToSubmit
			// 
			this.tsmiAddToSubmit.Image = global::TOBA.Properties.Resources.cou_16_add;
			this.tsmiAddToSubmit.Name = "tsmiAddToSubmit";
			this.tsmiAddToSubmit.Size = new System.Drawing.Size(165, 22);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
			// 
			// tsUseGroup
			// 
			this.tsUseGroup.CheckOnClick = true;
			this.tsUseGroup.Image = global::TOBA.Properties.Resources.label_16;
			this.tsUseGroup.Name = "tsUseGroup";
			this.tsUseGroup.Size = new System.Drawing.Size(165, 22);
			this.tsUseGroup.Text = "显示状态分组(&G)";
			// 
			// tsIconType
			// 
			this.tsIconType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsIconTypeT,
			this.tsIconTypeS});
			this.tsIconType.Name = "tsIconType";
			this.tsIconType.Size = new System.Drawing.Size(165, 22);
			this.tsIconType.Text = "图标类型(&I)";
			// 
			// tsIconTypeT
			// 
			this.tsIconTypeT.Name = "tsIconTypeT";
			this.tsIconTypeT.Size = new System.Drawing.Size(139, 22);
			this.tsIconTypeT.Text = "车次类型(&T)";
			// 
			// tsIconTypeS
			// 
			this.tsIconTypeS.Name = "tsIconTypeS";
			this.tsIconTypeS.Size = new System.Drawing.Size(139, 22);
			this.tsIconTypeS.Text = "车票状态(&S)";
			// 
			// imglist
			// 
			this.imglist.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglist.ImageStream")));
			this.imglist.TransparentColor = System.Drawing.Color.Transparent;
			this.imglist.Images.SetKeyName(0, "c");
			this.imglist.Images.SetKeyName(1, "d");
			this.imglist.Images.SetKeyName(2, "f");
			this.imglist.Images.SetKeyName(3, "g");
			this.imglist.Images.SetKeyName(4, "k");
			this.imglist.Images.SetKeyName(5, "n");
			this.imglist.Images.SetKeyName(6, "o");
			this.imglist.Images.SetKeyName(7, "p");
			this.imglist.Images.SetKeyName(8, "s");
			this.imglist.Images.SetKeyName(9, "t");
			this.imglist.Images.SetKeyName(10, "u");
			this.imglist.Images.SetKeyName(11, "z");
			this.imglist.Images.SetKeyName(12, "l");
			this.imglist.Images.SetKeyName(13, "i");
			this.imglist.Images.SetKeyName(14, "y");
			this.imglist.Images.SetKeyName(15, "fx");
			this.imglist.Images.SetKeyName(16, "zn");
			// 
			// QueryResult
			// 
			this.ContextMenuStrip = this.ctxResult;
			this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			this.HideSelectionMode = ComponentOwl.BetterListView.BetterListViewHideSelectionMode.Disable;
			this.ImageList = this.imglist;
			this.ctxResult.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();
			this.ResumeLayout(false);

		}


		#endregion

		private Tuple<IEnumerable<QueryResultItem>, bool> _lastData;

		/// <summary>
		/// 加载查询结果
		/// </summary>
		/// <param name="result"></param>
		public void LoadTrainResult(IEnumerable<QueryResultItem> result, bool removeCurrentList = true)
		{
			if (!Visible || !AppContext.MainForm.IsWindowVisible)
			{
#if DEBUG
				TOBA.Events.OnMessage(this, new EventInfoArgs("不可见查询：" + QueryParam.Name + "，已挂起刷新界面"));
#endif
				_lastData = Tuple.Create(result, removeCurrentList);
				return;
			}
			_lastData = null;

			BeginUpdate();

			if (removeCurrentList)
			{
				Clear(true);

				//fix memory leak
				Groups.ForEach(_ => _.Items.Clear());
			}

			if (result != null)
			{
				var items = result.Select(s =>
				{
					var item = new QueryResultListViewItem(s, _fonts, ShowStartAndEndStation);
					AssignItemGroup(item);
					return item;
				}).ToArray();
				Items.AddRange(items);
			}
			EndUpdate();
		}


		/// <summary>
		/// 请求对车次进行分组
		/// </summary>
		public event EventHandler<AssignItemGroupEventArgs> RequireAssignItemGroup;

		/// <summary>
		/// 刷新指定项的分组
		/// </summary>
		/// <param name="item"></param>
		void AssignItemGroup(QueryResultListViewItem item)
		{
			var handler = RequireAssignItemGroup;
			if (handler != null)
			{
				handler(this, new AssignItemGroupEventArgs(item, item.ResultItem));
				return;
			}

			var s = item.ResultItem;
			if (s.ControlFlag != 0)
			{
				item.Group = _underControl;
			}
			else if (s.NoTicketNeeded)
			{
				item.Group = _noneedticket;
			}
			else if (!s.IsAvailable)
			{
				if (s.BeginSellTime == null)
					item.Group = _notiket;
				else
				{
					var grouptext = s.SellTimeTip;

					item.Group = _hourSellListGroup.GetValue(grouptext, _ =>
					{
						var g = new BetterListViewGroup(grouptext);
						Groups.Add(g);
						return g;
					});
				}
			}
			else
			{
				item.Group = _valid;
			}
		}

		void RefreshItemIcon()
		{
			Items.Cast<QueryResultListViewItem>().ForEach(RefreshItemIcon);
		}

		void RefreshItemIcon(QueryResultListViewItem item)
		{
			item.RefreshIcon();
		}

		/// <summary>
		/// 请求提交订单
		/// </summary>
		public event EventHandler<GeneralEventArgs<QueryResultItem, char>> RequestSubmitOrder;

		/// <summary>
		/// 引发 <see cref="RequestSubmitOrder" /> 事件
		/// </summary>
		/// <param name="ea">包含此事件的参数</param>
		protected virtual void OnRequestSubmitOrder(GeneralEventArgs<QueryResultItem, char> ea)
		{
			var handler = RequestSubmitOrder;
			if (handler != null)
				handler(this, ea);
		}

		protected override void OnMouseDoubleClick(MouseEventArgs e)
		{
			base.OnMouseDoubleClick(e);

			var item = GetItemAt(e.Location.X, e.Location.Y) as QueryResultListViewItem;
			if (item == null)
				return;
			var subitem = item.GetSubItemAt(e.Location.X, e.Location.Y);

			OnRequestSubmitOrder(new GeneralEventArgs<QueryResultItem, char>(item.ResultItem, (subitem is ResultSubItems.TicketCellSubItem) ? (subitem as ResultSubItems.TicketCellSubItem).Code : '\0'));
		}

		#region 停靠站提示

		TrainStopQuery _trainStopPrompt;
		Popup _trainStoPopup;

		void InitTrainStopPrompt()
		{
			_trainStopPrompt = new TrainStopQuery(Session) { Query = QueryParam };
			_trainStoPopup = new Popup(_trainStopPrompt)
			{
				FocusOnOpen = true,
				ShowingAnimation = PopupAnimations.TopToBottom | PopupAnimations.Slide,
				HidingAnimation = PopupAnimations.BottomToTop | PopupAnimations.Slide
			};

			Click += (s, e) =>
			{
				var item = FocusedItem as QueryResultListViewItem;
				if (item == null)
				{
					_trainStoPopup.Close();
					return;
				}

				var loc = PointToClient(Control.MousePosition);
				var subitem = item.GetSubItemAt(loc.X, loc.Y);
				if (!(subitem is ResultSubItems.TrainCodeSubItem))
				{
					_trainStoPopup.Close();
					return;
				}
				var bounds = PointToScreen(new Point(item.SubItems[1].Bounds.BoundsOuter.X, subitem.Bounds.BoundsOuter.Bottom));
				_trainStopPrompt.ShowTrain(item.ResultItem);
				_trainStoPopup.Show(bounds);
			};
		}

		#endregion


		/// <summary>
		/// Releases the unmanaged resources used by the <see cref="T:System.Windows.Forms.Control"/> and its child controls and optionally releases the managed resources.
		/// </summary>
		/// <param name="disposing">True to release both managed and unmanaged resources; false to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);

			if (Program.IsRunning)
			{
				QueryViewConfiguration.Instance.PropertyChanged -= Instance_PropertyChanged;
				AppContext.MainForm.IsWindowVisibleChanged -= DetectReloadData;
			}
		}

		Font _priceFont;
		static readonly Dictionary<Color, Brush> _brushes = new Dictionary<Color, Brush>();
		bool _isClassic = Application.VisualStyleState == VisualStyleState.NoneEnabled || Environment.OSVersion.Version.Major < 6;
		StringFormat _flag = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

		static Brush GetBrush(Color color)
		{
			return _brushes.GetValue(color, _ => new SolidBrush(color));
		}

		void DrawSubitemText(BetterListViewDrawItemEventArgs eventArgs, int index)
		{
			var subitem = eventArgs.Item.SubItems[index];
			var bounds = eventArgs.ItemBounds.SubItemBounds[index].BoundsInner;
			eventArgs.Graphics.DrawString(subitem.Text, subitem.Font, _isClassic && eventArgs.Item.Selected ? SystemBrushes.HighlightText : GetBrush(subitem.ForeColor), bounds, _flag);
		}


		StringFormat _stringflag_leftcenter = new StringFormat() { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };

		/// <summary>
		/// Raises the DrawItem event.
		/// </summary>
		/// <param name="eventArgs">The BetterListViewDrawItemEventArgs instance containing the event data.</param>
		protected override void OnDrawItem(BetterListViewDrawItemEventArgs eventArgs)
		{
			if (eventArgs.Item.SubItems.Count <= 6)
				return;

			var traincell = (ResultSubItems.TrainCodeSubItem)eventArgs.Item.SubItems[0];
			var result = traincell.ResultItem;

			eventArgs.DrawSubItemTexts[1] = false;
			eventArgs.DrawSubItemTexts[2] = false;
			eventArgs.DrawSubItemTexts[3] = false;
			if (ShowStartAndEndStation)
			{
				eventArgs.DrawSubItemTexts[4] = false;
				eventArgs.DrawSubItemTexts[5] = false;
			}
			//备注
			eventArgs.DrawSubItemTexts[eventArgs.Item.SubItems.Count - 1] = false;


			base.OnDrawItem(eventArgs);

			//发站
			DrawSubitemText(eventArgs, 1);
			DrawSubitemText(eventArgs, 2);
			DrawSubitemText(eventArgs, 3);
			if (ShowStartAndEndStation)
			{
				DrawSubitemText(eventArgs, 4);
				DrawSubitemText(eventArgs, 5);
			}

			var ticketStartIndex = 4 + (ShowStartAndEndStation ? 2 : 0);

			for (var i1 = ticketStartIndex; i1 < eventArgs.Item.SubItems.Count - 1; i1++)
			{
				var tcell = (ResultSubItems.TicketCellSubItem)eventArgs.Item.SubItems[i1];
				var price = result.TicketCount.GetTicketData(tcell.Code);
				if (price == null)
					continue;
				if (price.Price > 0.01 || !price.MemoText.IsNullOrEmpty())
				{
					var innerBoun = eventArgs.ItemBounds.SubItemBounds[i1].BoundsInner;
					var text = price.MemoText ?? "¥" + (price.Price ?? 0.0).ToString("#0.0");
					eventArgs.Graphics.DrawString(text, _priceFont, _isClassic && eventArgs.Item.Selected ? SystemBrushes.HighlightText : (price.MemoTextColor ?? (price.HasDiscount ? Brushes.BlueViolet : price.NotNeed || price.NoTicket ? Brushes.Gray : Brushes.Green)), new PointF(innerBoun.Left, innerBoun.Bottom - 10));
				}
			}
			//备注
			DrawSubitemText(eventArgs, eventArgs.Item.SubItems.Count - 1);

			var statusMsg = "";

			if (result.ControlFlag != 0)
			{
				statusMsg += result.ControlMessage;
			}
			if (result.IsLimitSell)
			{
				statusMsg += result.ButtonTextInfo;
			}
			if (!result.IsAvailable)
			{
				if (result.BeginSellTime != null)
					statusMsg += result.SellTimeTip;
				else if (result.IsLimitSell)
					statusMsg += result.ButtonTextInfo;
			}


			if (!statusMsg.IsNullOrEmpty())
			{
				var bounds = eventArgs.Item.Bounds;
				var ticketPriceBounds = eventArgs.ItemBounds.SubItemBounds[ticketStartIndex];
				//var size = eventArgs.Graphics.MeasureString("鱼", Font);

				var xbound = new Rectangle(ticketPriceBounds.BoundsCell.Location, new Size(bounds.BoundsInner.Width - ticketPriceBounds.BoundsCell.X, ticketPriceBounds.BoundsCell.Height));
				//eventArgs.Graphics.DrawString(statusMsg, Font, Brushes.Chocolate, ticketPriceBounds.BoundsCell.X, ticketPriceBounds.BoundsCell.Y + (ticketPriceBounds.BoundsCell.Height - size.Height) / 2);
				eventArgs.Graphics.DrawString(statusMsg, Font, Brushes.Chocolate, xbound, _stringflag_leftcenter);
			}
		}
	}
}