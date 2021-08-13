using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using TOBA.UI.Dialogs.Passenger;
using TOBA.WebLib;

namespace TOBA.UI.Dialogs.Order
{
	using Common;

	using Data;

	using DevComponents.DotNetBar;

	using Entity;

	using Otn.Workers;

	using TOBA.Order;
	using TOBA.Query;
	using TOBA.Query.Entity;

	internal partial class SubmitOrder : OrderSubmitDialogUiBase
	{

		//控制是否允许手动添加乘客
		bool _allowUserAdd = false;

		Configuration.SubmitOrder _config;
		Dictionary<char, string> _validSeatTypes;
		Dictionary<int, string> _validTicketTypes;
		SubmitOrderBase _worker;

		public SubmitOrder(Session session, QueryResultItem train, Entity.QueryParam query, char seat, IEnumerable<PassengerInTicket> passengers, bool byAuto)
			: base(session, train, seat, passengers, query, byAuto)
		{
			InitializeComponent();
			Icon = Properties.Resources.shoppingcart;

			tc.InitSession(session);
			ps.InitSession(session);
			//VerifyCodeBox = verifyCodeBox1;
			AttachVerifyCodeControl(tc);

			if (Session == null)
				return;

			Resign = Query.Resign;

			_config = Configuration.SubmitOrder.Current;

			//初始化票类型
			_validTicketTypes = new Dictionary<int, string>();
			if (Query.QueryStudentTicket)
			{
				_validTicketTypes.Add(3, "学生票");
			}
			else
			{
				_validTicketTypes.Add(1, "成人票");
				_validTicketTypes.Add(2, "儿童票");
				_validTicketTypes.Add(3, "学生票");
				_validTicketTypes.Add(4, "残军票");
			}
			//初始化席别类型
			var allSeatCodes = Train.TicketCount.Keys.Where(s => Train.TicketCount[s].Count > 0 && ParamData.SeatTypeFull.ContainsKey(s)).Select(s => Train.FindCorrectSeat(s)).Where(s => s != 0).Distinct().ToArray();

			_validSeatTypes = allSeatCodes.ToDictionary(s => s, s => ParamData.SeatTypeFull[s]);
			//_validSeatTypesInTicket = _validTicketTypes.Keys.ToDictionary(s => s, s => allSeatCodes);

			_worker = new SubmitOrderWorker(Session, Train, Query);
			//_ticketQueryWorker = new TicketQueryWorker(null, Query);
			_queueWorker = new QueueOrderWorker(Session);
			_queueWorker.TourFlag = "dc";

			AttachWorker(_worker, _queueWorker);

			//初始化事件
			InitEditor();
			InitSubmit();

			PostInitialize();
		}

		protected override void OnClosing(CancelEventArgs e)
		{
			if (_queryTicketTimer != null) _queryTicketTimer.Enabled = false;
			if (_queueWorker != null) _queueWorker.RequestCancel = true;

			base.OnClosing(e);
		}

		/// <summary>
		/// 获得或设置是否是改签
		/// </summary>
		public bool Resign { get; set; }

		#region 编辑事件

		void InitEditor()
		{
			//新版12306禁止手动添加乘客

			if (Query.Resign)
			{
				dgvColAddChild.Visible = dgvPassenger.AllowUserToAddRows = false;
				ps.Enabled = false;
				dgvColName.ReadOnly = true;
				dgvColRemove.Visible = false;
				dgvColAddChild.Visible = false;
				dgvId.ReadOnly = true;
				dgvIdType.ReadOnly = true;
				//dgvTicketType.ReadOnly = true;
				chkDwAll.Enabled = false;
			}
			else
			{
				dgvPassenger.AllowUserToAddRows = _allowUserAdd;
				chkDwAll.Enabled = _validSeatTypes.ContainsKey('A') || _validSeatTypes.ContainsKey('F');
			}

			dgvPassenger.RowsAdded += dgvPassenger_RowsAdded;
			dgvPassenger.UserAddedRow += dgvPassenger_UserAddedRow;
			dgvPassenger.CellBeginEdit += dgvPassenger_CellBeginEdit;
			dgvPassenger.EditingControlShowing += dgvPassenger_EditingControlShowing;
			dgvPassenger.CellEndEdit += dgvPassenger_CellEndEdit;
			ps.RequestSelectPassenger += (s, e) => AddPassengerToList(e.Passenger);
			if (Query.QueryStudentTicket)
			{
				ps.ShowOnlyStudent = Query.QueryStudentTicket;  //学生票过滤
																//学生票只能选联系人
				dgvColName.ReadOnly = true;
				dgvId.ReadOnly = true;
				dgvIdType.ReadOnly = true;
				dgvTicketType.ReadOnly = true;
			}

			//席别子类型
			//dgvSeatSubType.Items.AddRange(new[]{
			//	new Entity.SeatSubType(1),
			//	new Entity.SeatSubType(2),
			//	new Entity.SeatSubType(3),
			//	new Entity.SeatSubType(0)
			//});
			dgvSeatSubType.ValueMember = "Id";
			dgvSeatSubType.ValueType = typeof(SubType);
			dgvSeatSubType.DisplayMember = "DisplayName";

			//身份证类型
			dgvIdType.Items.AddRange(ParamData.PassengerIdType.Select(s => (object)s).ToArray());
			dgvIdType.ValueMember = "Key";
			dgvIdType.DisplayMember = "Value";
			dgvIdType.ValueType = typeof(char);

			//席别
			dgvSeatType.ValueType = typeof(char);
			dgvSeatType.ValueMember = "Id";
			dgvSeatType.DisplayMember = "DisplayName";


			//票种
			dgvTicketType.ValueType = typeof(int);
			dgvTicketType.DisplayMember = "DisplayName";
			dgvTicketType.ValueMember = "Id";


			chkAutoSubmit.DataBindings.Add("Checked", _config, "AutoSubmitAfterEnterCode", false, DataSourceUpdateMode.OnPropertyChanged);

			if (PassengerInTickets != null && PassengerInTickets.Any())
			{
				PassengerInTickets.ForEach(AddPassengerToList);

				if (Configuration.SubmitOrder.Current.DisableEditNameOfAutoAddedPassenger || Query.Resign || !_allowUserAdd)
				{
					dgvPassenger.Rows.Cast<DataGridViewRow>().Where(s => !s.IsNewRow).ForEach(s =>
					{
						s.Cells[0].ReadOnly = true;
						s.Cells[4].ReadOnly = true;
						s.Cells[5].ReadOnly = true;
					});
				}

				//txtBox.Focus();
			}

			//验证码OK
			ps.RequestAddPassenger += ps_RequestAddPassenger;
		}

		/// <summary>
		/// 添加联系人
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void ps_RequestAddPassenger(object sender, EventArgs e)
		{
			if (!Session.UserProfile.IsPassengerLoaded)
			{
				this.ShowInfoToast("当前联系人尚未成功加载，请刷新联系人列表后再试");
				return;
			}

			var topmost = TopMost;
			TopMost = false;

			var adg = new AddPassenger();
			if (adg.ShowDialog() != DialogResult.OK)
			{
				TopMost = topmost;
				return;
			}

			var p = adg.Passenger;
			var fa = new PassengerManager(Session);
			var err = string.Empty;
			var wd = new YetAnotherWaitingDialog()
			{
				Title = "正在添加联系人，请稍等...",
				WorkCallback = () =>
				{
					err = fa.AddPassenger(p);
				}
			};
			wd.ShowDialog();
			TopMost = topmost;

			if (!string.IsNullOrEmpty(err))
			{
				this.ShowToast("无法添加联系人：" + err, Properties.Resources.warning_16, Color.DarkRed, glowColor: eToastGlowColor.Red);
				return;
			}

			var po = Session.UserProfile.Passengers.FindMatch(p);

			if (po == null)
			{
				AppContext.HostForm.ShowWarningToastMini("联系人添加成功，但未能查询到联系人信息，请稍后手动刷新联系人列表");
			}
			else if (po.CanAddIntoOrder)
			{
				AddPassengerToList(po);
			}
			else
			{
				var state = p.Verification;
				AppContext.HostForm.ShowWarningToastMini("联系人添加成功，但是无法添加到乘客中：" + state.VerifyMessage);
			}
		}

		/// <summary>
		/// 初始化列表
		/// </summary>
		void InitPassengerList()
		{
			//FIX: 一等座-》一等软座，二等座-》二等软座 兼容
			if (!_validSeatTypes.ContainsKey(Seat))
			{
				Seat = Train.FindCorrectSeat(Seat);
			}

			dgvTicketType.Items.Clear();
			dgvTicketType.Items.AddRange(_validTicketTypes.Keys.Select(s => (object)ParamData.TicketType[s]).ToArray());

			dgvPassenger.Rows.Cast<DataGridViewRow>().ForEach(s =>
			{
				var ps = s.Tag == null ? null : s.Tag as Entity.Web.Passenger;

				//票类型没有刷新？
				var ticketCell = s.Cells[1] as DataGridViewComboBoxCell;
				if (ticketCell.Value == null || !_validTicketTypes.ContainsKey((int)ticketCell.Value))
				{
					//票类型不正确，强行刷新
					ticketCell.Items.Clear();
					ticketCell.Items.AddRange(_validTicketTypes.Select(y => (object)new Entity.TicketType(y.Key)).ToArray());
					ticketCell.Value = _validTicketTypes.Keys.First();
				}
				if (ps != null && ps.Type == 2)
					ticketCell.Value = 2;


				//席别类型
				RebindSeatType(s);

				//身份证类型？
				var idcell = s.Cells[4] as DataGridViewComboBoxCell;
				if (idcell.Value == null)
					idcell.Value = '1';
				s.Cells[7].Value = true;
			});

			//if (Query.Resign)
			//{
			//	//TODO 改签联系人选择
			//	foreach (var p in _worker.Passengers)
			//	{
			//		var idx = dgvPassenger.Rows.Add();
			//		var row = dgvPassenger.Rows[idx];
			//		row.Cells[0].Value = p.Name;
			//		row.Cells[1].Value = p.TicketType;
			//		row.Cells[4].Value = p.IdType;
			//		row.Cells[5].Value = p.IdNo;
			//		RebindSeatType(row);
			//	}
			//}
			CheckOrder();

			WindowState = FormWindowState.Normal;
			BringToFront();
			Activate();
		}

		void dgvPassenger_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			var index = e.ColumnIndex;
			if (index == 1) RebindSeatType(dgvPassenger.Rows[e.RowIndex]);
			else if (index == 2) ReBindSeatSubType(dgvPassenger.Rows[e.RowIndex]);

			CheckOrder();
		}

		void dgvPassenger_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
		}

		void dgvPassenger_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{
		}

		void AddPassengerToList(PassengerInTicket p)
		{
			//查找联系人
			var row = dgvPassenger.Rows.Cast<DataGridViewRow>().FirstOrDefault(x => x.Tag == p);
			if (row != null) return;
			if (dgvPassenger.Rows.Cast<DataGridViewRow>().Count(s => !s.IsNewRow) >= 5)
			{
				this.ShowToast("每个订单中联系人不得超过五位 o(>_<)o ~~", Properties.Resources.warning_16, Color.DarkRed, glowColor: eToastGlowColor.Red);
				return;
			}

			ps.AvailableFilter.Add(p);

			//添加新的
			row = dgvPassenger.Rows[dgvPassenger.Rows.Add()];
			row.Tag = p;
			row.Cells[0].Value = p.Name;
			//row.Cells[1].ReadOnly = true;

			//1.车票类型
			var ticketCell = row.Cells[1] as DataGridViewComboBoxCell;
			//ticketCell.Items.Clear();
			//ticketCell.Items.Add(new Entity.TicketType(p.Type));
			if (p.TicketType == 3 && Query.QueryStudentTicket)
			{
				var type = new Entity.TicketType(p.TicketType);
				ticketCell.Items.Clear();
				ticketCell.Items.Add(type);
				ticketCell.Value = 3;
			}
			else
			{
				if (_validTicketTypes != null)
					ticketCell.Value = p.TicketType;
			}

			//2.席别
			RebindSeatType(row);

			//3.上下铺
			//ReBindSeatSubType(row);

			//4.身份证类型
			row.Cells[4].Value = p.IdType;
			row.Cells[4].ReadOnly = true;

			row.Cells[5].Value = p.IdNo;
			row.Cells[5].ReadOnly = true;

			row.Cells[6].Value = p.Mobile;
			row.Cells[7].ReadOnly = true;

			row.Cells[7].Value = true;
			row.Cells[7].ReadOnly = true;

			EnsureDgvControlsAvailable();
			CheckOrder();
		}

		/// <summary>
		/// 重新绑定席别类型
		/// </summary>
		/// <param name="row"></param>
		void RebindSeatType(DataGridViewRow row)
		{
			if (_validSeatTypes == null) return;
			var tickettype = (int)(row.Cells[1].Value ?? 0);
			//if (!_validSeatTypesInTicket.ContainsKey(tickettype)) return;

			var seatCell = row.Cells[2] as DataGridViewComboBoxCell;
			seatCell.Items.Clear();
			seatCell.Items.AddRange(_validSeatTypes.Keys.Select(s => (object)new Entity.SeatType(s)).ToArray());
			if (_validSeatTypes.Keys.Contains(Seat))
			{
				seatCell.Value = Seat;
			}
			else if (_validSeatTypes.Count > 0)
			{
				seatCell.Value = _validSeatTypes.Keys.First();
			}

			//var data = seatCell.Value == null ? null : (char?)seatCell.Value;
			//seatCell.Items.Clear();
			//seatCell.Items.AddRange(_validSeatTypes.Keys.Select(s => (object)new Entity.SeatType(s)).ToArray());

			//if (data != null && _validSeatTypesInTicket[tickettype].Contains(data.Value))
			//{
			//	seatCell.Value = data.Value;
			//}
			//else
			//{
			//	if (PreSelectSeatType.HasValue && _validSeatTypesInTicket[tickettype].Contains(PreSelectSeatType.Value))
			//	{
			//		seatCell.Value = PreSelectSeatType.Value;
			//	}
			//	else if (seatCell.Items.Count == 0)
			//	{
			//		Close();
			//		Information("爷，席别票额不足，无法继续购票。是否是学生票？");
			//		return;
			//	}
			//	else
			//	{
			//		//自动选择优先级
			//		var code = _config.DefaultSeatPreferOrder.FirstOrDefault(s => _validSeatTypesInTicket[tickettype].Contains(s));
			//		if (code == '\0') code = (seatCell.Items[0] as Entity.SeatType).Id;
			//		seatCell.Value = code;
			//	}
			//}

			ReBindSeatSubType(row);
		}

		void ReBindSeatSubType(DataGridViewRow row)
		{
			//if (row.Cells[3].Value == null) row.Cells[3].Value = 1;
			//if (row.Cells[2].Value == null)
			//{
			//	row.Cells[3].ReadOnly = true;
			//	return;
			//}

			//var seatType = (char)row.Cells[2].Value;
			//row.Cells[3].ReadOnly = !(seatType == '2' || seatType == '3' || seatType == '4' || seatType == '6');

			var seatCell = (DataGridViewComboBoxCell)row.Cells[3];
			seatCell.Items.Clear();

			if (row.Cells[2].Value != null)
			{
				var seat = (char)row.Cells[2].Value;
				seatCell.Items.Add(new SeatSubType(SubType.Random));

				var sublist = ParamData.GetSeatSubTypeEntityList(seat);
				if (sublist != null)
					seatCell.Items.AddRange(sublist);
				seatCell.Value = SubType.Random;
			}
		}

		void dgvPassenger_UserAddedRow(object sender, DataGridViewRowEventArgs e)
		{
			EnsureDgvControlsAvailable();
			CheckOrder();
		}

		void dgvPassenger_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
		{
			var row = dgvPassenger.Rows[e.RowIndex];
			if (_validTicketTypes != null)
			{
				var cell = row.Cells[1] as DataGridViewComboBoxCell;
				if (cell.Items.Count == 0)
				{
					cell.Items.AddRange(_validTicketTypes.Keys.Select(s => (object)ParamData.TicketType[s]).ToArray());
					cell.Value = 1;
				}
			}
			RebindSeatType(row);

			//身份证类型
			if (row.Cells[4].Value == null)
			{
				row.Cells[4].Value = '1';
			}
			row.Cells[7].Value = true;
		}

		private void dgvPassenger_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.ColumnIndex == 8)
			{
				if (dgvPassenger.Rows[e.RowIndex].IsNewRow) return;


				var row = dgvPassenger.Rows[e.RowIndex];
				var obj = (DataGridViewRow)row.Clone();
				obj.SetValues(row.Cells.Cast<DataGridViewCell>().Take(8).Select(s => s.Value).ToArray());
				obj.Cells[1].Value = 2;
				obj.Cells[1].ReadOnly = true;
				RebindSeatType(obj);
				dgvPassenger.Rows.Add(obj);

				EnsureDgvControlsAvailable();
				return;
			}
			if (e.ColumnIndex == 9)
			{
				if (dgvPassenger.Rows[e.RowIndex].IsNewRow) return;

				var pas = dgvPassenger.Rows[e.RowIndex].Tag as PassengerInTicket;
				var filter = ps.AvailableFilter;
				if (filter.Contains(pas))
				{
					filter.Remove(pas);
					ps.RefreshPassengerList();
				}

				dgvPassenger.Rows.RemoveAt(e.RowIndex);
				EnsureDgvControlsAvailable();
				return;
			}

			CheckOrder();
		}

		void EnsureDgvControlsAvailable()
		{
			dgvColAddChild.Visible = dgvPassenger.Rows.Count < ParamData.MaxPassengersPerOrder && !Query.Resign;
			dgvPassenger.AllowUserToAddRows = _allowUserAdd && dgvPassenger.Rows.Count <= ParamData.MaxPassengersPerOrder;
		}

		#endregion

		#region 提交处理

		protected override void InitTrainInfoDisplay()
		{
			LoadPriceInfo();
			base.InitTrainInfoDisplay();
		}

		/// <summary>
		/// 订单准备成功
		/// </summary>
		/// <param name="queueWarning">是否有排队警告</param>
		/// <returns></returns>
		protected override async Task PrepareSuccessAsync(bool queueWarning)
		{
			if (IsFormClosed)
				return;

			InitPassengerList();

			await base.PrepareSuccessAsync(queueWarning).ConfigureAwait(true);
		}

		void InitSubmit()
		{
			//验证码
			VerifyCodeBox.RandType = Resign ? RandCodeType.RandpResign : RandCodeType.Randp;

			lstInfo.DoubleClick += (s, e) =>
			{
				var item = lstInfo.SelectedItems.Cast<ListViewItem>().FirstOrDefault(x => char.IsDigit(x.SubItems[1].Text[0]));
				if (item == null) return;

				if (!_validSeatTypes.ContainsKey((char)item.Tag)) return;

				Seat = (char)item.Tag;
				dgvPassenger.Rows.Cast<DataGridViewRow>()
							.ForEach(x =>
							{
								x.Cells[2].Value = (char)item.Tag;
							});
			};

			Load += async (x, y) =>
						{
							await PrepareOrderAsync().ConfigureAwait(true);
						};
		}

		/// <summary>
		/// 返回是否可以提交订单
		/// </summary>
		/// <returns></returns>
		protected override async Task<bool> CanSubmitAsync()
		{
			PassengerInTickets = BindPassengers();
			_worker.BuyAllSeat = chkDwAll.Checked;

			var msg = CheckOrder();
			if (!msg.IsNullOrEmpty())
				return false;

			return await base.CanSubmitAsync().ConfigureAwait(true);
		}

		/// <summary>
		/// 加载票价信息
		/// </summary>
		void LoadPriceInfo()
		{
			lstInfo.BeginUpdate();
			Train.TicketCount.ForEach(s =>
			{
				var lvi = new ListViewItem(ParamData.SeatTypeFull.GetValue(s.Key).DefaultForEmpty("未知席别【" + s.Key + "】，请向作者报告！")) { Tag = s.Key };

				var count = s.Value.TicketForCompute;
				lvi.SubItems.Add(count == LeftTicketData.LargeQuantityOfTicket ? "有" : count.ToString("N0"));
				lvi.SubItems.Add(s.Value.Price != null && s.Value.Price.Value > 0.01 ? (s.Value.Price ?? 0.0).ToString("N") : "<未获得>");
				lstInfo.Items.Add(lvi);
			});
			lstInfo.EndUpdate();
		}

		string CheckOrder()
		{
			var err = new List<string>();

			if (dgvPassenger.Rows.Cast<DataGridViewRow>().Count(s => !s.IsNewRow) == 0)
				err.Add("· 您至少需要添加一位乘客");

			for (int i = 0; i < dgvPassenger.Rows.Count; i++)
			{
				var row = dgvPassenger.Rows[i];
				if (row.IsNewRow) continue;

				if ((row.Cells[0].Value as string).IsNullOrEmpty())
				{
					err.Add("· 乘客 " + (i + 1) + " 还没有输入姓名");
				}
				if (row.Cells[1].Value == null || row.Cells[2].Value == null || row.Cells[3].Value == null)
				{
					err.Add("· 乘客 " + (i + 1) + " 还没有选择票种和席别");
				}
				if (row.Cells[4].Value == null || (row.Cells[5].Value as string).IsNullOrEmpty())
				{
					err.Add("· 乘客 " + (i + 1) + " 还没有输入身份证信息");
				}
			}

			if (PassengerInTickets != null)
			{
				var (valid, error) = SubmitOrderWorker.CheckIfSeatSubTypeValid(PassengerInTickets);
				if (!valid)
				{
					err.Add($"· {error}");
				}
			}

			if (SubmitOrderWorker.NeedVc == true && VerifyCodeBox.Code.Length == 0)
			{
				err.Add("· 请输入验证码");
			}

			var result = err.JoinAsString("\n");
			gpError.Visible = true;

			if (err.Count > 0)
			{
				if (!LastErrorMessage.IsNullOrEmpty())
				{
					err.Add("· 上次错误信息：" + LastErrorMessage);
				}
				lblError.ForeColor = Color.Red;
				lblError.Text = err.JoinAsString("\n");
			}
			else
			{
				lblError.ForeColor = Color.Green;
				lblError.Text = "· 当前订单可以正常提交\n" + (LastErrorMessage.IsNullOrEmpty() ? "" : "上次错误信息：" + LastErrorMessage);
			}

			return result;
		}

		/// <summary>
		/// 根据订单信息创建联系人
		/// </summary>
		/// <returns></returns>
		Entity.PassengerInTicket[] BindPassengers()
		{
			var pas = dgvPassenger.Rows.Cast<DataGridViewRow>().Where(s => !s.IsNewRow)
				.Select(s => new Entity.PassengerInTicket
				{
					Name = s.Cells[0].Value as string,
					TicketType = (int)s.Cells[1].Value,
					SeatType = (char)s.Cells[2].Value,
					SeatSubType = (SubType)s.Cells[3].Value,
					IdType = (char)s.Cells[4].Value,
					IdNo = s.Cells[5].Value as string,
					Mobile = (s.Cells[6].Value as string) ?? "",
					Save = s.Cells[7].Value != null && (bool)s.Cells[7].Value
				}).ToArray();

			return pas;
		}

		#endregion

		#region 实时余票查询

		TicketQueryWorker _ticketQueryWorker;
		Timer _queryTicketTimer;
		QueryResultItem _train;

		void StartAliveCheckTicket()
		{
			return;

			if (!_config.EnableLiveTicketCheck) return;

			_ticketQueryWorker.TrainID = Train.Id;
			_queryTicketTimer = new Timer() { Interval = 1000 * _config.CheckTicketFrequency };
			_queryTicketTimer.Tick += (s, e) =>
			{
				Trace.WriteLine("实时查询余票 @ " + DateTime.Now);
				_queryTicketTimer.Enabled = false;
				_ticketQueryWorker.RunQuery();
			};

			_ticketQueryWorker.TicketQueryFailed += (s, e) =>
			{
				_queryTicketTimer.Enabled = true;
			};

			_ticketQueryWorker.TicketQuerySuccess += (s, e) =>
			{
				if (_ticketQueryWorker.Result == null)
					return;

				var t = _ticketQueryWorker.Result.FirstOrDefault();
				if (t == null) return;

				Train.SubmitOrderInfo = t.SubmitOrderInfo;
				lstInfo.Items.Cast<ListViewItem>().ForEach(r =>
				{
					var code = (char)r.Tag;
					var ticketdata = t.TicketCount.GetTicketData(code);
					Debug.WriteLine(string.Format("TAG {0} -> {1}", code, ticketdata.SelectValue(x => x.Count)));
					if (ticketdata != null)
					{
						var si = r.SubItems[1];
						if (ticketdata.NoTicket)
						{
							si.Text = "无票";
							r.ForeColor = Color.Gray;
						}
						else
						{
							si.Text = ticketdata.Count.ToString();
							r.ForeColor = SystemColors.ControlText;
						}
					}
				});

				_queryTicketTimer.Enabled = true;
			};

			_ticketQueryWorker.RunQuery();
		}

		#endregion

		QueueOrderWorker _queueWorker;

		protected override void ShowError(string msg)
		{
			lblError.ForeColor = Color.Red;
			lblError.Visible = true;
			lblError.Text = msg;
		}

		protected override void ShowTip(string tip)
		{
			lblError.ForeColor = Color.SteelBlue;
			lblError.Visible = true;
			lblError.Text = tip;
		}
	}
}
