
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace TOBA.UI.Dialogs.BackupOrder
{
	using Autofac;

	using Controls.Vc;

	using Data;

	using DevComponents.DotNetBar;

	using Entity;
	using Entity.Web;

	using Platform.SlideVc;

	using System.Threading.Tasks;

	using TOBA.BackupOrder;
	using TOBA.BackupOrder.Entity;

	using Padding = System.Windows.Forms.Padding;

	partial class SubmitBackupOrderDlg : DialogBase
	{
		private readonly Session _session;
		private readonly BackupCart _cart;
		private SlideVcControl _slideVc;


		public SubmitBackupOrderDlg(Session session, BackupCart cart)
		{
			_session = session;
			_cart = cart;
			InitializeComponent();
			Icon = Properties.Resources.backup;

			if (!Program.IsRunning)
				return;

			InitSession(session);
			Load += SubmitBackupOrderDlg_Load;
			FormClosing += (_1, _2) => _2.Cancel = !btnSubmit.Enabled || pQueue.Visible;

			//接受临客禁用
			pLk.Visible = false;
		}

		private void SubmitBackupOrderDlg_Load(object sender, EventArgs e)
		{
			//车次列表
			RefreshTrains();

			void CartItemChanged(object s1, ItemEventArgs<BackupCartItem> e2) => RefreshTrains();
			_cart.Items.Added += CartItemChanged;
			_cart.Items.Removed += CartItemChanged;
			Disposed += (s1, e1) =>
			{
				_cart.Items.Added -= CartItemChanged;
				_cart.Items.Removed -= CartItemChanged;
			};

			//联系人
			ps.ShowAddLink = false;
			ps.RequestSelectPassenger += (_1, _2) => _cart.Passengers.Add(_2.PassengerRaw);
			ps.FilterExprList.Add(_ => _cart.Passengers.Contains(_));
			RefreshPassengers();
			void PassengersChanged(object s1, ItemEventArgs<Passenger> e2) => RefreshPassengers();
			_cart.Passengers.Added += PassengersChanged;
			_cart.Passengers.Removed += PassengersChanged;
			Disposed += (s1, e1) =>
			{
				_cart.Passengers.Added -= PassengersChanged;
				_cart.Passengers.Removed -= PassengersChanged;
			};

			//提交
			btnSubmit.Click += (_1, _2) =>
			{
				if (pQueue.Visible)
					CancelOrderQueueAsync();
				else
					SubmitOrderAsync();
			};

			//是否需要验证码
			if (_cart.NeedSlideVc)
			{
				_slideVc = new SlideVcControl(Session, _cart.SlideToken, SlideAppId.Order);
				pBottom.Controls.Add(_slideVc);
				_slideVc.Size = new Size(310, 65);
				_slideVc.Location = new Point(pBottom.ClientSize.Width - _slideVc.Width - 10, (pBottom.ClientSize.Height - _slideVc.Height) / 2);
				_slideVc.SlideOk += (o, args) =>
				{
					_slideVc.Visible = false;
					btnSubmit.Visible = true;
					_cart.SlideSig = _slideVc.Sig;
					_cart.SlideCSessionId = _slideVc.CfSessionId;
					btnSubmit.PerformClick();
				};
				btnSubmit.Visible = false;
				_slideVc.BringToFront();
			}
		}

		void RefreshPassengers()
		{
			var items = _cart.Passengers;
			pPassenger.SuspendLayout();
			ps.RefreshPassengerList();

			var lvItems = pPassenger.Controls.Cast<ButtonX>().ToArray();
			var lvItemTags = lvItems.Select(s => s.Tag as Passenger).MapToHashSet();

			//删除没有的
			lvItems.Where(s => !items.Contains(s.Tag as Passenger)).ToArray().ForEach(pPassenger.Controls.Remove);

			//插入没有的
			items.Except(lvItemTags).ForEach(s =>
			{
				var btn = new ButtonX()
				{
					ColorTable = eButtonColor.Flat,
					Text = $"<b>{s.Name}</b> <font size=\"-1\" color=\"gray\">{s.TypeName}/{s.IdTypeName}</font>",
					Tag = s,
					AutoExpandOnClick = true,
					Padding = new Padding(10)
				};

				//子项
				if (s.Type == 1)
				{
					var addChild = new ButtonItem("addchild", "添加儿童票");
					addChild.Click += (_1, _2) =>
					{
						var pc = s.CreateChild();
						items.Add(pc);
					};
					btn.SubItems.Add(addChild);
				}

				var deleteItem = new ButtonItem("delete", "删除");
				deleteItem.Click += (_1, _2) => items.Remove(s);
				btn.SubItems.Add(deleteItem);

				pPassenger.Controls.Add(btn);
				btn.SetToPreferredSize();
			});

			pPassenger.ResumeLayout(true);

			//达到上限后禁止编辑
			ps.Enabled = _cart.Passengers.Count < 3;
		}

		void RefreshTrains()
		{
			var items = _cart.Items;
			if (items.Count == 0)
			{
				Close();
				return;
			}

			tList.BeginUpdate();
			tList.SuspendLayout();

			var lvItems = tList.Items.Cast<ListViewItem>().ToArray();
			var lvItemTags = lvItems.Select(s => s.Tag as BackupCartItem).MapToHashSet();

			//删除没有的
			lvItems.Where(s => !items.Contains(s.Tag as BackupCartItem)).ToArray().ForEach(tList.Items.Remove);

			//插入没有的
			items.Except(lvItemTags).ForEach(s =>
			{
				var lvi = new ListViewItem(s.Train.QueryResult.Date.ToShortDateString());
				var (days, strinfo) = s.Train.ElapsedTimeInfo;

				lvi.SubItems.AddRange(new[]
				{
					s.Train.FromStation.StationName,
					s.Train.ToStation.StationName,
					s.Train.Code,
					ParamData.GetSeatTypeName(s.Seat),
					s.Train.FromStation.DepartureTime.Value.ToShortTimeString(),
					s.Train.ToStation.ArriveTime.Value.ToShortTimeString(),
					strinfo + (days > 0 ? $" (+{days + 1})" : "")
				});

				tList.Items.Add(lvi);
			});

			tList.ResumeLayout(true);
			tList.EndUpdate();

			//候选车次
			var allSeats = items.Select(s => s.Seat).ToArray().Distinct();
			var allSeatChecks = allSeats.Select(s =>
				{
					var chk = new CheckBox()
					{
						Tag = s,
						Text = ParamData.GetSeatTypeName(s),
						AutoSize = true
					};

					return (Control)chk;
				}).
				ToArray();
			flpLkList.Controls.Clear();
			flpLkList.Controls.AddRange(allSeatChecks);
			lnkLkSelAll.Click += (_1, _2) => { flpLkList.Controls.OfType<CheckBox>().ForEach(s => s.Checked = true); };
			lnkLkSelNone.Click += (_1, _2) => { flpLkList.Controls.OfType<CheckBox>().ForEach(s => s.Checked = false); };

			//兑换截止时间
			//var endTime = _cart.Items.Select(s => s.Train.FromStation.DepartureTime.Value).Min();
			//endTime = endTime.Date.AddDays(endTime.Hour <= 6 ? -2 : -1).AddHours(19);
			//backupEndDate.MaxDate = endTime.Date;
			//backupEndDate.MinDate = DateTime.Now;
			//backupEndDate.Value = endTime.Date;
			//backupEndTime.MinDate = DateTime.Now;
			//backupEndTime.MaxDate = endTime;
			//backupEndTime.Value = endTime;
			backupEndDate.MinDate = _cart.HbStartTime;
			backupEndDate.MaxDate = _cart.HbEndTime;
			backupEndDate.Value = _cart.HbEndTime;
		}

		private bool _cancelFlag = false;

		async void SubmitOrderAsync()
		{
			var pas = _cart.Passengers;
			var trains = _cart.Items;

			if (pas.Count == 0 || pas.Count > 3)
			{
				this.ShowWarningToastMini("联系人必选且不得超过三位啊哈哈哈……");
				return;
			}

			if (trains.Count == 0)
			{
				this.ShowWarningToastMini("怎么会没有车次呢，好奇怪……");
				return;
			}

			//参数
			_cart.EndTime = backupEndDate.Value;
			_cart.EnableLkList = flpLkList.Controls.OfType<CheckBox>().Where(s => s.Checked).Select(s => (char)s.Tag).MapToHashSet();

			btnSubmit.Enabled = false;


			this.ShowInfoToast("正在信心满满地提交订单...", 0);

			var service = _session.GetService<IBackupOrderService>();
			var (ok, msg, data) = await service.CommitBackupOrderAsync();
			this.CloseToast();


			if (ok && data.IsAsync)
			{
				(ok, msg) = await QueueOrderAsync();
			}

			btnSubmit.Enabled = true;

			if (ok)
			{
				_cart.Items.Clear();
				this.ShowSuccessToastMini("嘿嘿，提交成功！");
				Session.OnRequestShowPanel(PanelIndex.HbOrder);
				Session.OnHbOrderChanged();

				await Task.Delay(1500);
				Close();
			}
			else
			{
				if (msg.Contains("验证码"))
				{
					_slideVc.Visible = true;
					_slideVc.Reload();
					btnSubmit.Visible = false;
				}
				if (!msg.IsNullOrEmpty())
					this.ShowErrorToastMini(msg);
			}
		}

		async Task<(bool ok, string msg)> QueueOrderAsync()
		{
			pQueue.Visible = true;
			cpQueue.IsRunning = true;
			btnSubmit.Text = "取消排队";
			btnSubmit.Enabled = true;

			var success = false;
			var msg = "";
			QueryBackupOrderQueueResponse data;
			var service = _session.GetService<IBackupOrderService>();

			while (!_cancelFlag)
			{
				var ok = false;

				(ok, msg, data) = await service.QueryHbQueueAsync();

				if (!ok)
				{
					break;
				}

				if (data.Status == 1)
				{
					success = true;
					break;
				}

				lblQueueTip.Text = $"不厌其烦地排队中，预计还需要 <font color=\"red\">{Utility.ShowSecondInfo(data.WaitTime)}</font>(人数 <font color=\"red\">{data.WaitCount}</font>)";
				await Task.Delay(BackupOrderConfiguration.Instance.QueryBackupOrderQueueTime);
			}
			_cancelFlag = false;

			cpQueue.IsRunning = false;
			pQueue.Visible = false;
			btnSubmit.Text = "提交订单";
			btnSubmit.Enabled = false;

			return (success, msg);
		}

		async Task CancelOrderQueueAsync()
		{
			btnSubmit.Enabled = false;
			btnSubmit.Text = "正在取消……";
			_cancelFlag = true;

			var service = _session.GetService<IBackupOrderService>();
			var (ok, msg) = await service.CancelWaitingOrderAsync();

			btnSubmit.Enabled = true;
			if (ok)
			{
				this.ShowInfoToastMini("取消订单成功...");
				await Task.Delay(1000);
				Close();
			}
			else
			{
				this.ShowErrorToastMini(msg);
				btnSubmit.Enabled = true;
			}
		}
	}
}
