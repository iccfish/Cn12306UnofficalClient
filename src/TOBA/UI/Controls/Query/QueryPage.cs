using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using TOBA.Configuration;

namespace TOBA.UI.Controls.Query
{
	using Common;

	using Components;

	using Data;

	using DevComponents.DotNetBar;

	using Dialogs;
	using Dialogs.Order;

	using Entity;

	using Popup;

	using System.Diagnostics;

	using TOBA.BackupOrder;
	using TOBA.Query;
	using TOBA.Query.Entity;

	internal partial class QueryPage : ControlBase
	{
		double _dpiScale;
		TicketQueryWorker _worker;

		public QueryPage()
			: this(null, null)
		{
		}

		public QueryPage(Session session, Entity.QueryParam param)
		{
			InitializeComponent();

			if (!Program.IsRunning)
				return;

			//High DPI
			if (Program.DpiY > 96.0F)
			{
				qs.Height = Program.GetScaledY(qs.Height);
			}

			InitSession(session);

			QueryParam = param;
			if (QueryParam != null)
			{
				InitQuery();
				InitStartStationTip();
				InitUi();
			}

			VisibleChanged += HandleVisibleChanged;
			AppContext.MainForm.IsWindowVisibleChanged += HandleVisibleChanged;
			Disposed += (s, e) =>
			{
				AppContext.MainForm.IsWindowVisibleChanged -= HandleVisibleChanged;
			};
		}

		private void HandleVisibleChanged(object sender, EventArgs e)
		{
			ShowQueryStatus();
		}

		void HandleProgramConfigChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "HideExtraFilterOption")
			{
				qp.Height = (int)(_dpiScale * (QueryViewConfiguration.Instance.HideExtraFilterOption ? 35 : 95));
			}
			if (e.PropertyName == "EnableSelltip")
			{
				sellTimeTip.Visible = Program.EnableSellTip && QueryViewConfiguration.Instance.EnableSelltip;
			}
		}

		void ShowQueryStatus()
		{
			if (!IsControlVisible) return;

			qp.Enabled = !_worker.IsBusy;
			panWait.Visible = _worker.IsBusy;

			if (QueryParam.QueryState == QueryState.Query)
			{
				tbStatus.ApplyColorSchema(RowStyleType.LightGreen);
				tbStatus.Text = $"正在查询中，客官别急...";
				tbStatus.Image = Properties.Resources.loading_16_3;
			}
			else if (QueryParam.LastQuerySuccess != null)
			{
				tbStatus.Text = _worker.BuildStatusText();

				if (!_worker.Success || _worker.Result == null)
				{
					tbStatus.ApplyColorSchema(RowStyleType.Red);
					tbStatus.Image = Properties.Resources.block_16;
				}
				else
				{
					if (_worker.Result.Count == 0)
					{
						tbStatus.ApplyColorSchema(RowStyleType.Red);
						tbStatus.Image = Properties.Resources.block_16;
					}
					else
					{
						tbStatus.ApplyColorSchema(RowStyleType.RoyalBlue);
						tbStatus.Image = Properties.Resources.tick_16;

						LoadStartStationTip(_worker.Result);
					}
				}
			}
		}

		/// <summary>
		/// 初始化查询参数
		/// </summary>
		void InitQuery()
		{
			var param = QueryParam;

			qp.Init(param);
			qs.Init(param);
			qr.Init(param);
			sellTimeTip.AttachQuery(param);
			alternativeDateSetting.InitQuery(param);
			qr.RequestSubmitOrder += (s, e) => HandleOrderRequest(e.Data1, e.Data2);
			_worker = new TicketQueryWorker(Session, param);
			_worker.EnableTimeoutService = true;
			_worker.EnableSystemBusyService = true;
			_worker.QueryTimeoutWarningService.TimeoutWarning += (s, e) =>
			{
				this.Invoke(new Action(() =>
				{
					var cfg = ApiConfiguration.Instance;
					var qcfg = TOBA.Configuration.QueryConfiguration.Current;
					AppContext.MainForm.ShowBalloonTip(3000, "查询超时警告", $"在过去的 {cfg.TimeoutRecordCount} 次查询中发生了至少 {cfg.TimeoutWarningLimit} 次查询超时，{(qcfg.TimeoutAutoIncreaseSetting ? "已自动增加超时设置，" : "")}请留意", ToolTipIcon.Warning);
				}));
			};
			_worker.SystemBusyWarningService.SystemBusyWarning += (s, e) =>
			{
				this.Invoke(new Action(() =>
				{
					var cfg = ApiConfiguration.Instance;
					var qcfg = TOBA.Configuration.QueryConfiguration.Current;
					AppContext.MainForm.ShowBalloonTip(3000, "系统繁忙警告", $"在过去的 {cfg.SystemBusyRecordCount} 次查询中发生了至少 {cfg.SystemBusyRecordCount} 次系统繁忙，频繁繁忙可能意味着当前IP或账户已受限，请尽量放慢查询。", ToolTipIcon.Warning);
				}));
			};
			_worker.IsBusyChanged += (s, e) =>
									{
										QueryParam.QueryState = _worker.IsBusy ? QueryState.Query : QueryState.None;
										ShowQueryStatus();
									};
			_worker.TicketQuerySuccess += (s, e) => HandleQuerySuccess();
			_worker.TicketQueryFailed += (s, e) => HandleQueryFailed();
			QueryParam.RequestQuery += (s, e) =>
			{
				if (QueryParam.EnableAutoPreSubmit && !QueryParam.AutoPreSubmitConfig.AllSetOk && IsControlVisible)
				{
					this.ShowToast("自动预定未启用或未设置完成，查票有票后将无法自动提交，请确认车次、联系人和席别均设置正确", Properties.Resources.warning_16, Color.DarkRed, timeout: 5000);
				}

				Debug.WriteLine($"请求刷新....当前Worker状态：{_worker.IsBusy}");
				_worker.RunQuery();
				MainForm.Instance.StopPlayTicketMusic();
			};

			//停止查询的时候，清空所有的toast
			param.StatusChanged += this.SafeInvoke((s, e) =>
			{
				if (QueryParam.QueryState != QueryState.None)
				{
					ToastNotification.Close(this);
				}
			});

			QueryParam.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(QueryParam.EnableAutoPreSubmit))
				{
					if (!QueryParam.EnableAutoPreSubmit)
					{
						this.ShowWarningToast("自动预定已关闭！如果刷票，将不会按照要求过滤并自动提交！");
					}
					else
					{
						this.ShowSuccessToast("自动预定已开启，请注意是否设置完成，否则无法起效。");
					}
				}
			};


			//查询设置
			var qc = new QueryConfiguration(Session);
			qc.QueryParam = param;
			btnConfig.PopupControl = qc;

			//匿名查询，禁用刷票，禁用自动预定
			if (!Session.IsLogined)
			{
				//chkEnableAutoSubmit.Hide();
				//chkEnableAutoSubmit.Value = false;
				param.EnableAutoPreSubmit = false;
				qs.Enabled = false;

				btnDateLoop.Hide();
				btnConfig.Hide();
			}

			//刷新界面
			QueryParam.SelectedSeatClassChanged += (s, e) =>
			{
				qr.LoadTrainResult(null);
				qr.RefreshColumnVisibility();
				RefreshList();
			};
			QueryParam.SelecteTrainClassChanged += (s, e) =>
			{
				RefreshList();
			};
			var refreshedProps = new HashSet<string>()
			{
				nameof(QueryParam.DepartureTimeFrom),
				nameof(QueryParam.DepartureTimeTo),
				nameof(QueryParam.ArriveTimeFrom),
				nameof(QueryParam.ArriveTimeTo),
				nameof(QueryParam.PassType),
				nameof(QueryParam.HideNoNeedTicket),
				nameof(QueryParam.HideFromNotSame),
				nameof(QueryParam.HideNoTicket),
				nameof(QueryParam.HideToNotSame)
			};
			QueryParam.PropertyChanged += (s, e) =>
			{
				if (refreshedProps.Contains(e.PropertyName))
					RefreshList();
			};
		}

		async void RefreshList()
		{
			if (QueryParam.LastQueryResult == null)
				return;

			var result = QueryParam.LastQueryResult;
			result.Clear();
			result.AddRange(result.OriginalList);
			result.Filter(QueryParam);

			qr.LoadTrainResult(result);
		}

		/// <summary>
		/// 初始化界面
		/// </summary>

		void InitUi()
		{
			tbStatus.BorderThickness = 0;
			InitCalendarLoop();
			panWait.KeepCenter();

			_dpiScale = (int)this.CreateGraphics().DpiY / 96.0;
			QueryViewConfiguration.Instance.PropertyChanged += HandleProgramConfigChanged;
			qp.Height = (int)(_dpiScale * (QueryViewConfiguration.Instance.HideExtraFilterOption ? 35 : 95));

			sellTimeTip.Visible = Program.EnableSellTip;

			qr.ShowStartAndEndStation = QueryViewConfiguration.Instance.ShowStartAndEndStation;
			QueryViewConfiguration.Instance.PropertyChanged += Instance_PropertyChanged;
			Disposed += (s, e) =>
			{
				QueryViewConfiguration.Instance.PropertyChanged -= Instance_PropertyChanged;
			};
			backupOrder.QueryParam = QueryParam;
		}

		private void Instance_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(QueryViewConfiguration.ShowStartAndEndStation))
				qr.ShowStartAndEndStation = QueryViewConfiguration.Instance.ShowStartAndEndStation;
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			QueryViewConfiguration.Instance.PropertyChanged -= HandleProgramConfigChanged;

			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public override void InitSession(Session session)
		{
			base.InitSession(session);

			if (_worker != null)
			{
				_worker.Session = session;
				_worker.LoadVerifyData = true;
			}
			if (_clControl != null)
				_clControl.Session = session;

		}

		/// <summary>
		/// 获得是否正在忙碌
		/// </summary>
		public bool IsBusy
		{
			get { return qp.IsInAutoRefreshCountDown || _worker.IsBusy; }
		}

		public Entity.QueryParam QueryParam { get; private set; }

		#region 查询结果

		void HandleOrderRequest(QueryResultItem train, char seat)
		{
			if (!Session.IsLogined)
			{
				this.ShowInfoToast("当前查询未登录，买个屁哦。。");
				return;
			}
			var data = train.TicketCount.GetTicketData(seat);


			//候补订票
			if (data?.NoTicket == true && train.IsSeatBackupAvailable(data.Code))
			{
				Session.GetService<IBackupOrderUiOperation>().AddToBackupOrderAsync(train, data);
				return;
			}

			if (!train.IsAvailable)
			{
				var backupMsg = train.AllowBackup ? ", 双击候补的席别可候补订票" : "";

				var pos = PointToClient(MousePosition);
				if (QueryParam.EnableAutoPreSubmit)
				{
					QueryParam.AutoPreSubmitConfig.AddTrainCode(train.Code);
					this.ShowToast("车次【" + train.Code + "】不可购买, 已加入自动预定列表" + backupMsg, x: pos.X, y: pos.Y);
				}
				else
				{
					this.ShowToast("车次【" + train.Code + "】不可购买" + backupMsg, x: pos.X, y: pos.Y);
				}
				return;
			}

			if (seat == '\0' || data == null)
			{
				var pas = (QueryParam.IsAutoSubmitEnabled || QueryParam.Resign) ? QueryParam.AutoPreSubmitConfig.Passenger.ToArray() : null;
				QueryParam.PrepareTicketInfoForPassengers(train, pas, seat);
				var dlg = new Dialogs.Order.SubmitOrder(Session, train, QueryParam, seat, pas, false);
				dlg.Show();
				return;
			}

			//指定的席别无票吗？
			if (data.NoTicket)
			{
				QueryParam.AutoPreSubmitConfig.AddTrainCode(train.Code);
				QueryParam.AutoPreSubmitConfig.AddSeat(seat);
				QueryParam.EnableAutoPreSubmit = true;
				this.ShowInfoToast("已将车次【" + train.Code + "】和席别【" + ParamData.SeatType[seat] + "】加入自动预定列表 :-)");
				return;
			}
			if (data.NotAvailable || data.NotSell)
			{
				seat = '\0';
			}


			//打开自动预定列表
			var illegalStudent = QueryParam.QueryStudentTicket && QueryParam.AutoPreSubmitConfig.Passenger.Any(s => s.TicketType != 3);
			if (illegalStudent)
			{
				this.ShowToast("嘿，你选择的联系人里不全是学生，但是查询的是学生票，臭不要脸....请重新选择联系人再提交订单...", Properties.Resources.warning_16, Color.DarkRed, glowColor: eToastGlowColor.Red);
			}
			if (!illegalStudent && QueryParam.EnableAutoPreSubmit && seat != '\0' && QueryParam.AutoPreSubmitConfig.Passenger.Count > 0)
			{
				var pasfast = QueryParam.AutoPreSubmitConfig.Passenger.Take(Math.Min(data.TicketForCompute, QueryParam.AutoPreSubmitConfig.Passenger.Count)).ToArray();
				QueryParam.PrepareTicketInfoForPassengers(train, pasfast, seat);
				var fastsubmit = new FastSubmitOrder(Session, train, seat, pasfast, QueryParam, false);
				fastsubmit.Show();
			}
			else
			{
				var pas = (QueryParam.IsAutoSubmitEnabled || QueryParam.Resign) ? QueryParam.AutoPreSubmitConfig.Passenger.Take(Math.Min(data.TicketForCompute, QueryParam.AutoPreSubmitConfig.Passenger.Count)).ToArray() : null;
				QueryParam.PrepareTicketInfoForPassengers(train, pas, seat);
				var order = new Dialogs.Order.SubmitOrder(Session, train, QueryParam, seat, pas, false);
				order.Show();
			}
		}

		void HandleQuerySuccess()
		{
			if (Parent == null || Parent.Parent == null)
			{
				//fix 查询过久被手动关了？
				return;
			}

			QueryParam.LastQuerySuccess = true;
			QueryParam.LastQueryTime = DateTime.Now;

			qr.LoadTrainResult(_worker.Result);

			Session.BackupOrderCart.SyncTrainSubmitInfoAsync(_worker.Result);
			if (_worker.AutoSelect == null && QueryParam.EnableAutoPreSubmit && QueryParam.AutoHb)
			{
				Session.GetService<IBackupOrderService>().DetectAutoHbTrainsToCart(QueryParam, _worker.Result);
			}
			//Session.DetectFaceCheckStatusByQueryResult(_worker.Result);

			QueryParam.LastQueryRequestTime = _worker.DataTime;
			ShowQueryStatus();

			if (_worker.Result.Count > 0 && QueryParam.QueryCount == 1)
			{
				//自动分析别名
				TrainIdAliasStorage.Instance.Update(_worker.Result.OriginalList);
			}

			//查找预选择的车次
			if (_worker.AutoSelect != null)
			{
				//自动打开当前页
				Session.UserProfile.QueryParams.SelectedQuery = QueryParam;
				MainForm.Instance.RestoreForm();
				//音乐提示？
				if (QueryParam.EnableAutoPreSubmit)
				{
					QueryParam.HasTicket = true;
					MainForm.Instance.PlayTicketMusic();
				}

				if (QueryParam.IsAutoSubmitEnabled)
				{
					var selected = _worker.AutoSelect;
					if (OrderConfiguration.Instance.EnableFastSubmitOrder)
					{
						var fastSubmit = new FastSubmitOrder(Session, selected.Train, selected.Seat, selected.Passengers, QueryParam, true);
						fastSubmit.Show();
					}
					else
					{
						//允许自动提交？
						var submit = new UI.Dialogs.Order.SubmitOrder(Session, selected.Train, QueryParam, selected.Seat, QueryParam.Resign ? QueryParam.AutoPreSubmitConfig.Passenger.ToArray() : selected.Passengers, true);
						submit.Show();
					}
				}
				//停掉所有的查询
				if (Configuration.QueryConfiguration.Current.StopQueryWhenFoundTicket)
					Session.UserProfile.QueryParams.RequestStopAll();
			}
			else if (QueryParam.IsAutoSubmitEnabled)
			{
				StartRefreshCountDown();

				//席别不对
				var stat = _worker.AutoSelectStat;
				if (stat?.IsTrainSeatMismatch == true)
				{
					this.ShowToast("警告：所选择的车次中并没有所选择要自动预定的席别！请检查自动预定车次和席别的设置！", Assets.FreeWp8Icons_White.FreeWp8IconsWhite_No_entry, Color.Firebrick, Color.White, eToastGlowColor.Red, timeout: 5000);
				}
			}
			else if (QueryParam.EnableAutoPreSubmit)
			{
				this.ShowToast("设置不正确，无法继续刷票。请检查自动预定车次和席别的设置！", Assets.FreeWp8Icons_White.FreeWp8IconsWhite_No_entry, Color.Firebrick, Color.White, eToastGlowColor.Red, timeout: 5000);
			}
		}

		void StartRefreshCountDown(bool enableAutoRefresh = true, bool isError = false)
		{
			if (IsDisposed || !Session.IsLogined)
				return;


			//TODO: 需要更换为 QueryParam.GetSleepTime()
			var (sleep, msg, msgTime) = QueryParam.GetSleepTime(Session, enableAutoRefresh, isError);
			if (!msg.IsNullOrEmpty())
			{
				ToastNotification.Show(this, msg, Properties.Resources.cou_16_warning, msgTime);
			}

			qp.StartSleep(sleep);
		}

		void HandleQueryFailed()
		{
			QueryParam.LastQuerySuccess = false;
			QueryParam.LastQueryTime = DateTime.Now;
			qr.LoadTrainResult(null);
			ShowQueryStatus();
			StartRefreshCountDown(Configuration.QueryConfiguration.Current.IgnoreServerError, true);
		}


		#endregion

		#region 日期轮查

		CalendarQueryLoop _clControl;
		Popup _clPopup;

		void InitCalendarLoop()
		{
			_clControl = new CalendarQueryLoop(Session, QueryParam);
			_clPopup = new Popup(_clControl)
			{
				ShowingAnimation = PopupAnimations.TopToBottom | PopupAnimations.Slide,
				HidingAnimation = PopupAnimations.BottomToTop | PopupAnimations.Slide
			};
			btnDateLoop.Click += (s, e) =>
			{
				_clControl.Refresh(QueryParam.FromStationName, QueryParam.ToStationName, DateTime.Now.Date, qp.MaxDate);
				_clPopup.Show(btnDateLoop);
			};
		}

		#endregion

		#region 起售站预售时间提醒

		/// <summary>
		/// 初始化预售时间提醒
		/// </summary>
		void InitStartStationTip()
		{
			ilStartTip.Images.Add(UiUtility.Get20PxImageFrom16PxImg(Properties.Resources.info_16));
			lvSellTip.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			btnEnableSellTip.Visible = false;

			lvSellTip.DoubleClick += (s, e) =>
			{
				var item = lvSellTip.SelectedItems.Cast<ListViewItem>().FirstOrDefault();
				if (item == null)
					return;

				var train = item.Tag as QueryResultItem[];
				if (train != null)
				{
					var stationInfo = train[0].StartStationSellInfo;
					var query = QueryParam.Clone() as QueryParam;

					//添加车次
					query.EnableAutoPreSubmit = true;
					query.AutoPreSubmitConfig.HideOtherTrains = true;
					train.Select(x => x.Code).ForEach(query.AutoPreSubmitConfig.TrainList.Add);
					query.FromStationCode = stationInfo.Code;
					query.FromStationName = stationInfo.Name;

					Session.UserProfile.QueryParams.Add(query);
				}
			};
			sellTipClose.Click += (s, e) =>
			{
				pSellTipContainer.Visible = false;
				btnEnableSellTip.Visible = true;
				QueryParam.UiSetting.EnableSellTip = false;
			};
			btnEnableSellTip.Click += (s, e) =>
			{
				pSellTipContainer.Visible = true;
				btnEnableSellTip.Visible = false;
				QueryParam.UiSetting.EnableSellTip = true;
			};
		}

		private string _tipCacheKey = null;

		/// <summary>
		/// 刷新起售站预售时间提醒
		/// </summary>
		void LoadStartStationTip(TOBA.Query.Entity.QueryResult result)
		{
			if (!IsControlVisible)
				return;

			var cacheKey = $"{result.Date.DayOfYear}{QueryParam.FromStationCode}{QueryParam.ToStationCode}{DateTime.Now.Minute / 30}";

			if (!Configuration.QueryConfiguration.Current.EnableStartStationTip)
			{
				pSellTipContainer.Visible = false;
				btnEnableSellTip.Visible = false;
				return;
			}

			if (cacheKey == _tipCacheKey)
				return;

			var list = new List<ListViewItem>();
			if (result.Date >= ParamData.GetMaxTicketDate(QueryParam.QueryStudentTicket))
			{
				foreach (var train in result.Where(s => s.StartStationSellInfo != null).GroupBy(s => s.StartStation.StationName))
				{
					var item = new ListViewItem() { ImageIndex = 0 };
					var trains = train.ToArray();
					item.Tag = trains;

					var info = trains[0].StartStationSellInfo;
					item.Text = $"车次【{trains.Select(s => s.Code).JoinAsString("/")}】的始发站【{train.Key}】的起售时间为【{info.SellTime.ToShortTimeString()}】，{(info.IsInSell ? "已经起售" : info.IsEarly ? "时间早一些，可以提前关注" : "可以稍后关注")}";
					if (info.IsInSell)
					{
						item.ForeColor = Color.Red;
					}
					else if (info.IsEarly)
					{
						item.ForeColor = Color.RoyalBlue;
					}
					else
					{
						item.ForeColor = Color.Green;
					}

					list.Add(item);
				}
			}
			//是否车次过少？
			if (result.Count <= 3)
			{
				list.Add(new ListViewItem("车次较少，建议更改查询条件。如果两站之间没有更多的车次，可试试中转查询。") { ImageIndex = 0, ForeColor = Color.BlueViolet });
			}
			var feCount = result.Count(s => s.FromStation.IsFirst && s.ToStation.IsEnd);
			if (feCount < 3 && feCount < result.Count / 3)
			{
				list.Add(new ListViewItem("大多数车次为过路车。如果站不大容易导致车票难买，可考虑跨站购票。") { ImageIndex = 0, ForeColor = Color.BlueViolet });
			}
			var notSellList = result.Where(s => s.BeginSellTime != null && !s.FromStation.IsFirst).ToArray();
			if (notSellList.Length > 0)
			{
				list.Add(new ListViewItem($"车次【{notSellList.Select(s => s.Code).JoinAsString("/")}】尚未起售，但不是始发站。建议留意对应车次之前几站以及始发站的起售时间，可以碰碰运气是否能拿到票。") { ImageIndex = 0, ForeColor = Color.BlueViolet });
			}

			lvSellTip.Items.Clear();
			lvSellTip.Items.AddRange(list.ToArray());

			var hasTip = lvSellTip.Items.Count > 0;
			if (QueryParam.UiSetting.EnableSellTip)
			{
				pSellTipContainer.Visible = hasTip;
			}
			else
			{
				btnEnableSellTip.Visible = hasTip;
			}

			_tipCacheKey = cacheKey;
		}

		#endregion
	}
}
