using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace TOBA.UI.Controls.Query
{
	using Common;

	using Entity;

	using System.Diagnostics;

	internal partial class QueryParams : ControlBase
	{
		Timer _sleepTimer;
		double _dpiScale;

		public Entity.QueryParam QueryParam { get; private set; }

		private Image _navLeftDisabled = UiUtility.MakeGrayImage(Properties.Resources._132);
		private Image _navLeftEnabled = Properties.Resources._132;
		private Image _navRightDisabled = UiUtility.MakeGrayImage(Properties.Resources._131);
		private Image _navRightEnabled = Properties.Resources._131;

		public QueryParams()
		{
			InitializeComponent();

			//setting button height
			btnExchange.Height = pFrom.Height;

			this.SizeChanged += QueryParams_SizeChanged;
			_dpiScale = (int)this.CreateGraphics().DpiY / 96.0;
		}


		bool _miniMode;

		int Scale(int value)
		{
			return (int)(value * _dpiScale);
		}

		void QueryParams_SizeChanged(object sender, EventArgs e)
		{
			if (Height < 50 == _miniMode)
				return;

			_miniMode = Height < 50;

			if (_miniMode)
			{
				btnQuery.Size = new Size(Scale(110), Height - 4);
				btnQuery.Location = new Point(Width - btnQuery.Width - Scale(2), Scale(2));
			}
			else
			{
				btnQuery.Size = new Size(Scale(95), Scale(84));
				btnQuery.Location = new Point(Width - btnQuery.Width - Scale(2), Scale(8));
			}
			SetButtonText(btnQuery.Text);
		}

		void SetButtonText(string text)
		{
			if (_miniMode)
				text = Regex.Replace(text, @"[\s\n]", "");
			btnQuery.Text = text;
		}

		/// <summary>
		/// 获得或设置是否正在倒计时
		/// </summary>
		public bool IsInAutoRefreshCountDown
		{
			get { return _sleepTimer != null && _sleepTimer.Enabled; }
		}


		public void Init(Entity.QueryParam param)
		{
			QueryParam = param;
			//初始化
			InitUi();
			if (param != null)
			{
				InitUiEvents();
				InitSleep();
				InitOperationRules();
			}
		}

		/// <summary>
		/// 初始化操作授权
		/// </summary>
		void InitOperationRules()
		{
			dep.Enabled = false;
			pFrom.Enabled = false;
			pTo.Enabled = false;
			pStu.Enabled = false;
			btnExchange.Enabled = false;
			pbPrev.Enabled = pbNext.Enabled = false;
			pbPrev.Image = _navLeftDisabled;
			pbNext.Image = _navRightDisabled;
		}

		void ResetButtonText()
		{
			btnQuery.ForeColor = QueryParam.EnableAutoPreSubmit ? Color.Firebrick : Color.RoyalBlue;
			btnQuery.Text = QueryParam.EnableAutoPreSubmit ? "开 始\n刷 票" : "查 询\n余 票";
		}


		/// <summary>
		/// 初始化界面显示
		/// </summary>
		void InitUi()
		{
			var timeRange = Enumerable.Range(0, 24).Select(s => (object)s).ToArray();
			var timeRangeEnd = Enumerable.Range(1, 24).Select(s => (object)s).ToArray();
			pDtFrom.Items.AddRange(timeRange);
			pDtFrom.SelectedIndex = 0;
			pDtFrom.FormatString = "00'点'";
			pDtTo.Items.AddRange(timeRangeEnd);
			pDtTo.FormatString = "00'点'";
			pDtTo.SelectedIndex = 23;
			pAtFrom.Items.AddRange(timeRange);
			pAtFrom.FormatString = "00'点'";
			pAtFrom.SelectedIndex = 0;
			pAtTo.Items.AddRange(timeRangeEnd);
			pAtTo.SelectedIndex = 23;
			pAtTo.FormatString = "00'点'";

			//始发站
			pFrom.StationType = "from";

			//到达站
			pTo.StationType = "to";

			//ensure handler
			if (!IsHandleCreated)
				CreateHandle();
		}

		/// <summary>
		/// 初始化界面信息
		/// </summary>
		void InitUiEvents()
		{
			var p = QueryParam;

			//始发站
			pFrom.DataBindings.Add(nameof(TrainStation.Code), p, nameof(QueryParam.FromStationCode), false, DataSourceUpdateMode.OnPropertyChanged);
			pFrom.DataBindings.Add(nameof(TrainStation.Text), p, nameof(QueryParam.FromStationName), false, DataSourceUpdateMode.OnPropertyChanged);
			//到达站
			pTo.DataBindings.Add(nameof(TrainStation.Code), p, nameof(QueryParam.ToStationCode), false, DataSourceUpdateMode.OnPropertyChanged);
			pTo.DataBindings.Add(nameof(TrainStation.Text), p, nameof(QueryParam.ToStationName), false, DataSourceUpdateMode.OnPropertyChanged);

			//学生票？
			pStu.DataBindings.Add("Checked", p, nameof(QueryParam.QueryStudentTicket), false, DataSourceUpdateMode.OnPropertyChanged);
			dep.AddDataBinding(p, s => s.StudentTicket, s => s.QueryStudentTicket);

			//日期
			if (p.DepartureDate < DateTime.Now.Date || p.DepartureDate > dep.MaxDate) p.DepartureDate = DateTime.Now.Date;
			dep.DataBindings.Add(nameof(DateComboBox.Date), p, nameof(QueryParam.DepartureDate), false, DataSourceUpdateMode.OnPropertyChanged);
			pbPrev.Click += (s, e) =>
			{
				p.DepartureDate = p.DepartureDate.AddDays(-1);
				p.OnRequireSave();
				p.OnRequestQuery(true);
			};
			pbNext.Click += (s, e) =>
			{
				p.DepartureDate = p.DepartureDate.AddDays(1);
				p.OnRequireSave();
				p.OnRequestQuery(true);
			};


			//发车时间
			pDtFrom.DataBindings.Add("SelectedIndex", p, "DepartureTimeFrom", false, DataSourceUpdateMode.OnPropertyChanged);
			pDtTo.DataBindings.Add("SelectedIndex", p, "DepartureTimeTo", false, DataSourceUpdateMode.OnPropertyChanged);
			pAtFrom.DataBindings.Add("SelectedIndex", p, "ArriveTimeFrom", false, DataSourceUpdateMode.OnPropertyChanged);
			pAtTo.DataBindings.Add("SelectedIndex", p, "ArriveTimeTo", false, DataSourceUpdateMode.OnPropertyChanged);
			//四个显示过滤
			chkHideFromNotSame.DataBindings.Add("Checked", p, "HideFromNotSame", false, DataSourceUpdateMode.OnPropertyChanged);
			chkHideNoTicket.DataBindings.Add("Checked", p, "HideNoTicket", false, DataSourceUpdateMode.OnPropertyChanged);
			chkHideNoValid.DataBindings.Add("Checked", p, "HideNoNeedTicket", false, DataSourceUpdateMode.OnPropertyChanged);
			chkHideToNotSame.DataBindings.Add("Checked", p, "HideToNotSame", false, DataSourceUpdateMode.OnPropertyChanged);

			//自动刷新
			pAutoRefresh.DataBindings.Add("Checked", p, "AutoRefresh", false, DataSourceUpdateMode.OnPropertyChanged);

			//。。。席别
			var seatChecks = pSeatType.Controls.OfType<CheckBox>().ToArray();
			lock (p.SelectedSeatClass)
			{
				if (p.SelectedSeatClass.Count == 0)
				{
					//默认全选

					seatChecks.ForEach(s => { s.Checked = true; p.SelectedSeatClass.Add(((string)s.Tag)[0]); p.OnSelectedSeatClassChanged(); });
				}
				else
				{
					seatChecks.ForEach(s => { s.Checked = p.SelectedSeatClass.Contains(((string)s.Tag)[0]); });
				}
			}
			bool suspendEvent = false;
			seatChecks.ForEach(s =>
			{
				lock (p.SelectedSeatClass)
				{
					s.CheckedChanged += (x, y) =>
					{
						var ck = x as CheckBox;
						var code = (ck.Tag as string)[0];
						if (ck.Checked) p.SelectedSeatClass.Add(code);
						else p.SelectedSeatClass.Remove(code);

						if (!suspendEvent)
						{
							p.OnSelectedSeatClassChanged();
							QueryParam.OnRequireSave();
						}
					};
				}
			});
			p.SelectedSeatClass.Added += (s, e) =>
			{
				var code = e.Item;
				var check = seatChecks.FirstOrDefault(_ => (_.Tag as string)[0] == code);
				if (check?.Checked == false)
					check.Checked = true;
			};
			p.SelectedSeatClass.Removed += (s, e) =>
			{
				var code = e.Item;
				var check = seatChecks.FirstOrDefault(_ => (_.Tag as string)[0] == code);
				if (check?.Checked == true)
					check.Checked = false;
			};
			//全选？
			lnkAllSeatType.Click += (s, e) =>
			{
				suspendEvent = true;
				var check = seatChecks.Any(x => !x.Checked);
				seatChecks.ForEach(x => x.Checked = check);
				suspendEvent = false;
				p.OnSelectedSeatClassChanged();
				QueryParam.OnRequireSave();
			};

			//。。。车型
			var tcChecks = pTrainType.Controls.OfType<CheckBox>().ToArray();
			if (p.SelectedTrainClass.Count == 0)
			{
				//默认全选
				tcChecks.ForEach(s => { s.Checked = true; p.SelectedTrainClass.Add((s.Tag as string)[0]); });
			}
			else
			{
				tcChecks.ForEach(s => { s.Checked = p.SelectedTrainClass.Contains((s.Tag as string)[0]); });
			}
			tcChecks.ForEach(s =>
			{
				s.CheckedChanged += (x, y) =>
				{
					var ck = x as CheckBox;
					var code = (ck.Tag as string)[0];
					lock (p.SelectedTrainClass)
					{
						if (ck.Checked) p.SelectedTrainClass.Add(code);
						else p.SelectedTrainClass.Remove(code);
					}

					if (!suspendEvent)
					{
						p.OnSelecteTrainClassChanged();
						QueryParam.OnRequireSave();
					}
				};
			});
			p.SelectedTrainClass.Added += (s, e) =>
			{
				var code = e.Item;
				var check = tcChecks.FirstOrDefault(_ => (_.Tag as string)[0] == code);
				if (check?.Checked == false)
					check.Checked = true;
			};
			p.SelectedTrainClass.Removed += (s, e) =>
			{
				var code = e.Item;
				var check = tcChecks.FirstOrDefault(_ => (_.Tag as string)[0] == code);
				if (check?.Checked == true)
					check.Checked = false;
			};
			//全选？
			lnkAllTrainType.Click += (s, e) =>
			{
				suspendEvent = true;
				var check = tcChecks.Any(x => !x.Checked);
				tcChecks.ForEach(x => x.Checked = check);
				suspendEvent = false;
				p.OnSelecteTrainClassChanged();
				QueryParam.OnRequireSave();
			};

			//过路类型
			pPassType.SelectedIndex = QueryParam.PassType == null ? 0 : QueryParam.PassType.Value;
			pPassType.SelectedIndexChanged += (s, e) =>
			{
				QueryParam.PassType = pPassType.SelectedIndex == 0 ? (int?)null : pPassType.SelectedIndex;
				QueryParam.OnRequireSave();
			};

			//交换出发地和到达地
			btnExchange.Click += (s, e) =>
			{
				var code = pFrom.Code;
				var name = pFrom.Text;
				if (code.IsNullOrEmpty() || name.IsNullOrEmpty() || pTo.Code.IsNullOrEmpty() || pTo.Text.IsNullOrEmpty()) return;
				pFrom.Code = pTo.Code;
				pFrom.Text = pTo.Text;
				pTo.Code = code;
				pTo.Text = name;
				QueryParam.OnRequireSave();
			};

			//请求查询
			btnQuery.Click += (s, e) =>
			{
				QueryParam.QueryCount = 0;
				QueryParam.OnRequireSave();
				if (_sleepTimer.Enabled)
				{
					QueryParam.OnRequestStop();
				}

				else QueryParam.OnRequestQuery(true);
			};

			//改签
			if (p.Resign)
			{
				pFrom.Enabled = false;
				pTo.Enabled = p.ResignChangeTs;
				//pDate.Enabled = false;
				btnExchange.Enabled = false;
				pStu.Enabled = false;
			}

			//请求查询
			p.RequestQuery += (s, e) =>
			{
				if (!IsInAutoRefreshCountDown)
				{
					Session.UserProfile.Configuration.IncreaseStationQueryFromCount(QueryParam.FromStationCode);
					Session.UserProfile.Configuration.IncreaseStationQueryToCount(QueryParam.ToStationCode);
				}
			};
			p.RequestStop += (s, e) =>
			{
				StopSleep();
			};
			p.PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == "DepartureDate" || e.PropertyName == "QueryStudentTicket")
				{
					CheckNavigateToolbarStatus();
				}
				if (e.PropertyName == nameof(QueryParam.EnableAutoPreSubmit))
				{
					ResetButtonText();
				}
			};
			CheckNavigateToolbarStatus();
			ResetButtonText();


			if (!Session.IsLogined)
			{
				pAutoRefresh.Checked = false;
				pAutoRefresh.Enabled = false;
				pAutoRefresh.Hide();
			}
		}

		void CheckNavigateToolbarStatus()
		{
			pbPrev.Enabled = QueryParam.DepartureDate.Date > DateTime.Now.Date;
			pbPrev.Image = pbPrev.Enabled ? _navLeftEnabled : _navLeftDisabled;

			pbNext.Enabled = QueryParam.DepartureDate.Date < MaxDate.Date;
			pbNext.Image = pbNext.Enabled ? _navRightEnabled : _navRightDisabled;
		}

		/// <summary>
		/// 获得最大的日期
		/// </summary>
		public DateTime MaxDate
		{
			get { return dep.MaxDate; }
		}

		#region Overrides of ScrollableControl

		/// <param name="e">包含事件数据的 <see cref="T:System.EventArgs"/>。</param>
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);

			if (Visible)
				btnQuery.Focus();
		}

		#endregion

		#region 自动重新查询倒计时

		DateTime _targetTime;

		public void StartSleep(int milliseconds)
		{
			if (_sleepTimer.Enabled)
				return;

			Debug.WriteLine($"倒计时={milliseconds}");
			//ensureHandler
			if (milliseconds > 0)
			{
				QueryParam.QueryState = QueryState.Wait;
				_targetTime = DateTime.Now.AddMilliseconds(milliseconds);
				btnQuery.ForeColor = Color.Chocolate;
				SetButtonText(Utility.ShowMilliSecondInfo(milliseconds));
				_sleepTimer.Enabled = true;
			}
			else
			{
				StopSleep();
			}
		}

		/// <summary>
		/// 初始化休息
		/// </summary>
		void InitSleep()
		{
			_sleepTimer = new Timer()
			{
				Interval = 100
			};

			_sleepTimer.Tick += (s, e) =>
			{
				var dt = DateTime.Now;
				if (dt >= _targetTime)
				{
					StopSleep();

					if (QueryParam.IsAutoSubmitEnabled)
					{
						QueryParam.OnRequestQuery(false);
					}
				}
				else
				{
					var ts = _targetTime - dt;
					if (IsControlVisible)
						SetButtonText(Utility.ShowMilliSecondInfo((int)ts.TotalMilliseconds));
				}
			};
		}

		/// <summary>
		/// 停止刷新倒计时
		/// </summary>
		public void StopSleep()
		{
			//if (!_sleepTimer.Enabled) return;

			QueryParam.QueryState = QueryState.None;
			ResetButtonText();
			_sleepTimer.Enabled = false;
		}

		#endregion

	}
}
