using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using TOBA.Configuration;

namespace TOBA.UI.Controls.Query
{

	using Data;

	using Dialogs.Common;

	using Entity;

	using Otn.Workers;

	using Popup;

	using System.Diagnostics;

	using TOBA.Service;

	internal partial class AutoSubmitSetting : ControlBase
	{
		public AutoSubmitSetting()
		{
			InitializeComponent();
		}

		void RefreshAutoVcStatus()
		{
			chkAutoVc.Visible = Service.VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine != null;
			chkAutoVc.Enabled = ProgramConfiguration.Instance.AutoSubmitOrderVcCode && VerifyCodeRecognizeServiceLoader.VerifyCodeRecognizeEngine?.IsLoggedIn == true;
		}

		public void Init(Entity.QueryParam param)
		{
			QueryParam = param;
			Configuration = param.AutoPreSubmitConfig;

			//init drag drop
			InitDragDrop();

			//add property binding
			//chkEnableAutoSubmit.Checked = QueryParam.EnableAutoPreSubmit;
			//chkEnableAutoSubmit.CheckedChanged += (s, e) => QueryParam.EnableAutoPreSubmit = chkEnableAutoSubmit.Checked;
			chkEnableAutoSubmit.AddDataBinding(QueryParam, s => s.Checked, s => s.EnableAutoPreSubmit);
			if (param.Resign)
				chkAutoHb.Visible = false;

			tableLayoutPanel1.AddDataBinding(QueryParam, s => s.Enabled, s => s.EnableAutoPreSubmit);
			chkEnablePartialSubmit.DataBindings.Add("Checked", Configuration, "EnablePartialSubmit", false, DataSourceUpdateMode.OnPropertyChanged);
			chkHideOtherTrains.DataBindings.Add("Checked", Configuration, "HideOtherTrains", false, DataSourceUpdateMode.OnPropertyChanged);
			chkAutoVc.Checked = QueryParam.EnableAutoVC;
			chkAutoVc.DataBindings.Add("Checked", QueryParam, "EnableAutoVC", false, DataSourceUpdateMode.OnPropertyChanged);
			chkAutoHb.AddDataBinding(QueryParam, s => s.Checked, s => s.AutoHb);
			//chkAuto.AddDataBinding(Configuration, s => s.Checked, s => s.AutoMode);

			//chkAutoVc.AddDataBinding(ProgramConfiguration.Instance, s => s.Enabled, s => s.AutoSubmitOrderVcCode);
			//chkAutoTrain.AddDataBinding(rSeatFirst, s => s.Enabled, s => s.Checked);

			RefreshAutoVcStatus();
			var svceh = this.SafeInvoke((s, e) =>
			  {
				  RefreshAutoVcStatus();
			  });
			Service.VerifyCodeRecognizeServiceLoader.OnVerifyCodeRecognizeEngineChanged += svceh;
			Service.VerifyCodeRecognizeServiceLoader.StateChanged += svceh;
			var svceh1 = ProgramConfiguration.Instance.AddPropertyChangedEventHandler(s => s.AutoSubmitOrderVcCode, (s, e) => RefreshAutoVcStatus());
			Disposed += (s, e) =>
			{
				Service.VerifyCodeRecognizeServiceLoader.OnVerifyCodeRecognizeEngineChanged -= svceh;
				Service.VerifyCodeRecognizeServiceLoader.StateChanged -= svceh;
				svceh1();
			};

			//chkAuto.CheckedChanged += (s, e) =>
			//{
			//	if (chkAuto.Checked)
			//		Information("警告：此功能随时可能会被失效，请不要依赖此选项，非必要时刻不建议选择。");
			//};

			if (Configuration.SeatFirst)
			{
				rSeatFirst.Checked = true;
			}
			else
			{
				rTrainFirst.Checked = true;
			}
			rTrainFirst.CheckedChanged += (s, e) => { if (rTrainFirst.Checked) Configuration.SeatFirst = false; };
			rSeatFirst.CheckedChanged += (s, e) => { if (rSeatFirst.Checked) Configuration.SeatFirst = true; };
			//btnPreVerifyCode.Click += btnPreVerifyCode_Click;
			//btnPreVerifyCode.AddDataBinding(ProgramConfiguration.Instance, s => s.Visible, s => s.EnablePreEnterVcCode);
			//chkEnableSCSLoop.AddDataBinding(QueryParam, s => s.Checked, s => s.EnableSameCityStationLoop);

			InitTrainEditor();
			InitPassengerEdit();
			InitSeatEditor();
			InitStatusCheck();
			InitSeatSubTypeEditor();
		}

		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		public override void InitSession(Session session)
		{
			base.InitSession(session);

			_passselector?.InitSession(session);
			_addseeat?.InitSession(session);
			_addtrain?.InitSession(session);
		}

		public Entity.AutoPreSubmitConfiguration Configuration { get; private set; }

		/// <summary>
		/// 获得或设置查询参数
		/// </summary>
		public Entity.QueryParam QueryParam
		{
			get => _queryParam;
			private set
			{
				_queryParam = value;
				if (_addseeat != null) _addseeat.Query = value;
				if (_passselector != null) _passselector.AvailableFilter = value.AutoPreSubmitConfig.Passenger;
			}
		}

		//void btnPreVerifyCode_Click(object sender, EventArgs e)
		//{
		//	Information("提醒：\n1.请不要依赖此功能；\n2.请在放票前1-2分钟填写，不要提前太久，过期会失效；\n3.由于失效过快，不适合捡漏，除非你有耐心反复重新输入；\n4.所输入的验证码不适合改签；\n5.所有的查询标签页共享同一个验证码，不需要重复输入。");

		//	using (var vc = new RequireVcCode())
		//	{
		//		vc.Session = Session;
		//		vc.RandType = RandCodeType.SjRand;
		//		vc.ShowDialog();

		//		if (vc.Code.Length != 4)
		//			return;

		//		var code = vc.Code;
		//		using (var wait = new FSLib.Windows.Dialogs.YetAnotherWaitingDialog())
		//		{
		//			bool? valid = null;
		//			wait.Title = "正在看看验证码对不对....";
		//			wait.WorkCallback = () =>
		//			{
		//				var checker = new Otn.Workers.VerifyCodeWorker()
		//							{
		//								Code = code,
		//								Session = Session
		//							};
		//				valid = checker.Check();
		//			};
		//			wait.ShowDialog();
		//			if (valid == null)
		//			{
		//				Information("检验验证码时网络发生错误");
		//			}
		//			else if (valid == false)
		//			{
		//				Information("验证码好像不对，请重试。");
		//			}
		//			else
		//			{
		//				Session.LastVerifyCode = vc.Code;
		//				Information("看起来验证码正确。。买票时切记及时关注提交状态，以免错过输入验证码。");
		//			}
		//		}
		//	}
		//}

		#region 拖放支持

		void InitDragDrop()
		{
			AllowDrop = true;

		}

		protected override void OnDragEnter(DragEventArgs drgevent)
		{
			base.OnDragEnter(drgevent);

			if (drgevent.Data.GetDataPresent(typeof(Tuple<QueryResultListViewItem, ResultSubItems.TicketCellSubItem>)))
			{
				drgevent.Effect = DragDropEffects.Link;
				BackColor = Color.FromArgb(0xEF, 0xEF, 0x8C);
			}
		}

		protected override void OnDragDrop(DragEventArgs drgevent)
		{
			base.OnDragDrop(drgevent);

			if (drgevent.Data.GetDataPresent(typeof(Tuple<QueryResultListViewItem, ResultSubItems.TicketCellSubItem>)))
			{
				var data = (Tuple<QueryResultListViewItem, ResultSubItems.TicketCellSubItem>)drgevent.Data.GetData(typeof(Tuple<QueryResultListViewItem, ResultSubItems.TicketCellSubItem>));

				Debug.WriteLine($"AutoPreSubmitSettingDrop, Item1={data.Item1}, Item2={data.Item2}");
				Configuration.AddTrainCode(data.Item1.Text);
				if (data.Item2 != null)
				{
					Configuration.AddSeat(data.Item2.Code);
				}

				BackColor = SystemColors.Window;
				QueryParam.EnableAutoPreSubmit = true;
			}
		}

		protected override void OnDragLeave(EventArgs e)
		{
			base.OnDragLeave(e);
			BackColor = SystemColors.Control;
		}

		#endregion

		#region 车次修改

		void lAddTrain_Click(object sender, EventArgs e)
		{
			_addTrainCodePopup.Show(lAddTrain);
		}

		AddTrainCode _addtrain;
		Popup _addTrainCodePopup;

		void InitTrainEditor()
		{
			cTrains.BindEmptyIndicator();
			_addtrain = new AddTrainCode(Session, QueryParam);
			_addTrainCodePopup = new Popup(_addtrain)
			{
				FocusOnOpen = true,
				HidingAnimation = PopupAnimations.Slide | PopupAnimations.BottomToTop,
				ShowingAnimation = PopupAnimations.Slide | PopupAnimations.TopToBottom
			};
			Configuration.TrainList.ForEach(s => cTrains.Controls.Add(CreateTrainLabel(s)));
			Configuration.TrainList.Added += this.SafeInvoke<ItemEventArgs<string>>((s, e) =>
			{
				cTrains.Controls.Add(CreateTrainLabel(e.Item));
				EnsureTrainStatus();
			});
			Configuration.TrainList.Removed += (sender, args) =>
			{
				var link = cTrains.Controls.OfType<LinkLabel>().FirstOrDefault(s => s.Text == args.Item);
				link?.Parent?.Controls.Remove(link);
				EnsureTrainStatus();
			};
			EnsureTrainStatus();
			lClearTrain.Click += lClearTrain_Click;
			lAddTrain.Click += lAddTrain_Click;
			_addtrain.RequestAddCode += (s, e) => Configuration.AddTrainCode(e.Data);
		}

		void lClearTrain_Click(object sender, EventArgs e)
		{
			if (!Question("确定清空自动预定车次列表咩？", true)) return;
			Configuration.TrainList.Clear();
			EnsureTrainStatus();
		}

		void EnsureTrainStatus()
		{
			lClearTrain.Enabled = cTrains.Controls.Count > 0;
			CheckAutoSubmitStatus();
		}

		LinkLabel CreateTrainLabel(string code)
		{
			var lbl = new LinkLabel { Text = code, TextAlign = ContentAlignment.MiddleLeft, AutoSize = true, Size = new Size(100, 16), LinkColor = Color.RoyalBlue, LinkBehavior = LinkBehavior.HoverUnderline };
			lbl.MouseClick += (s, e) =>
			{
				var l = s as LinkLabel;
				var c = l.Text;

				Configuration.TrainList.Remove(c);
			};
			return lbl;
		}


		#endregion

		#region 席别修改


		AddSeatClass _addseeat;
		Popup _addseatPopup;

		void InitSeatEditor()
		{
			cSeat.BindEmptyIndicator();
			_addseeat = new AddSeatClass() { Query = QueryParam };
			_addseeat.InitSession(Session);
			_addseatPopup = new Popup(_addseeat)
			{
				FocusOnOpen = true,
				HidingAnimation = PopupAnimations.Slide | PopupAnimations.BottomToTop,
				ShowingAnimation = PopupAnimations.Slide | PopupAnimations.TopToBottom
			};
			lAddSeat.Click += lAddSeat_Click;
			lClearSeat.Click += lClearSeat_Click;
			Configuration.SeatList.ForEach(s => cSeat.Controls.Add(CreateSeatLabel(s)));
			Configuration.SeatList.Added += (sender, args) =>
			{
				cSeat.Controls.Add(CreateSeatLabel(args.Item));
				EnsureSeatStatus();
			};
			Configuration.SeatList.Removed += (sender, args) =>
			{
				var item = cSeat.Controls.OfType<LinkLabel>().FirstOrDefault(s => (char)s.Tag == args.Item);
				item?.Parent?.Controls.Remove(item);
				EnsureSeatStatus();
			};
			_addseeat.RequestSelectSeat += (s, e) => Configuration.AddSeat(e.Data);

			EnsureSeatStatus();

			_seatRuleEditor = new SeatRuleEditor()
			{
				QueryParam = QueryParam
			};
			_seatRuleEditorPopup = new Popup(_seatRuleEditor)
			{
				FocusOnOpen = true,

				HidingAnimation = PopupAnimations.Slide | PopupAnimations.BottomToTop,
				ShowingAnimation = PopupAnimations.Slide | PopupAnimations.TopToBottom
			};
			_seatRuleEditorPopup.Closed += (s, e) =>
			{
				_seatRuleEditor.Init(null, '\0');
			};
			_seatRuleEditor.SettingChanged += (s, e) =>
			{
				SetSeatLabelStatus(_seatRuleEditor.OwnerControl as LinkLabel);
			};
		}

		SeatRuleEditor _seatRuleEditor;
		Popup _seatRuleEditorPopup;

		LinkLabel CreateSeatLabel(char code)
		{
			var lbl = new LinkLabel { TextAlign = ContentAlignment.MiddleCenter, AutoSize = true, Size = new Size(100, 16), LinkBehavior = LinkBehavior.HoverUnderline, Padding = new Padding(0, 0, 5, 0), Tag = code, LinkColor = Color.RoyalBlue };
			SetSeatLabelStatus(lbl);

			lbl.MouseClick += (s, e) =>
			{
				var l = s as LinkLabel;
				var c = (char)l.Tag;

				//左键移除，右键显示菜单
				if (e.Button == MouseButtons.Left)
				{
					Configuration.SeatList.Remove(c);
					EnsureSeatStatus();
					QueryParam.OnRequireSave();
				}
				else
				{
					_seatRuleEditor.Init(l, c);
					_seatRuleEditorPopup.Show(l);
				}
			};
			return lbl;
		}

		void SetSeatLabelStatus(LinkLabel lbl)
		{
			if (lbl == null)
				return;

			var code = (char)lbl.Tag;

			lbl.Text = ParamData.SeatType[code];
			lbl.LinkColor = Color.RoyalBlue;

			var tipText = "左键点击移除； 右键点击显示规则编辑器";
			var rules = QueryParam.AutoPreSubmitConfig.SeatCheckRules.GetValue(code);
			if (rules?.Count > 0)
			{
				lbl.LinkColor = Color.MediumPurple;
				lbl.Text += "*";
				tipText += "\n-------------[当符合以下条件时此席别才会起效]-------------\n" + rules.Select(s => s.GetDescription()).JoinAsString("\n");
			}

			toolTip1.SetToolTip(lbl, tipText);
		}

		void lClearSeat_Click(object sender, EventArgs e)
		{
			if (!Question("确定清空自动预定席别列表咩？", true)) return;
			cSeat.Controls.Clear();
			Configuration.SeatList.Clear();
			QueryParam.OnRequireSave();
			EnsureSeatStatus();
		}

		void lAddSeat_Click(object sender, EventArgs e)
		{
			_addseatPopup.Show(lAddSeat);
		}


		void EnsureSeatStatus()
		{
			lClearSeat.Enabled = cSeat.Controls.Count > 0;
			CheckAutoSubmitStatus();
		}


		#endregion

		#region 联系人修改

		Passenger.PassengerSelector _passselector;
		Popup _passPopup;
		QueryParam _queryParam;

		void lClearPassenger_Click(object sender, EventArgs e)
		{
			if (!Question("确定清空自动预定联系人列表咩？", true)) return;
			cPassenger.Controls.Clear();
			Configuration.Passenger.Clear();
			QueryParam.OnRequireSave();
			EnsureAddPassengerStatus();
		}

		void lAddPassenger_Click(object sender, EventArgs e)
		{
			_passselector.ShowOnlyStudent = QueryParam.QueryStudentTicket;
			_passPopup.Show(lAddPassenger);
		}

		void InitPassengerEdit()
		{
			cPassenger.BindEmptyIndicator();
			_passselector = new Passenger.PassengerSelector(Session) { AvailableFilter = Configuration.Passenger };
			_passPopup = new Popup(_passselector)
			{
				FocusOnOpen = true,
				HidingAnimation = PopupAnimations.Slide | PopupAnimations.BottomToTop,
				ShowingAnimation = PopupAnimations.Slide | PopupAnimations.TopToBottom
			};
			_passselector.RequestSelectPassenger += (s, e) => AddPassenger(e.Passenger);
			lAddPassenger.Click += lAddPassenger_Click;
			lClearPassenger.Click += lClearPassenger_Click;
			Configuration.Passenger.ForEach(s => cPassenger.Controls.Add(CreatePassengerLabel(s)));

			_passselector.ShowAddLink = true;
			_passselector.RequestAddPassenger += (s, e) =>
			{
				if (!Session.UserProfile.IsPassengerLoaded)
				{
					this.ShowInfoToast("当前联系人尚未成功加载，请刷新联系人列表后再试");
					return;
				}
				using (var dlg = new Dialogs.Passenger.AddPassenger())
				{
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						var p = dlg.Passenger;
						p.AddDate = DateTime.Now;
						var manager = new PassengerManager(Session);
						var dlgWait = new YetAnotherWaitingDialog();
						var result = "";
						dlgWait.Title = "正在记录中, 请稍等.";
						dlgWait.WorkCallback = () =>
						{
							result = manager.AddPassenger(p);
						};
						dlgWait.ShowDialog();
						if (result.IsNullOrEmpty())
						{
							//添加
							var po = Session.UserProfile.Passengers.FindMatch(p);

							if (po == null)
							{
								AppContext.HostForm.ShowWarningToastMini("联系人添加成功，但未能查询到联系人信息，请稍后手动刷新联系人列表");
							}
							else if (po.CanAddIntoOrder)
							{
								AddPassenger(po);
							}
							else
							{
								var state = p.Verification;
								AppContext.HostForm.ShowWarningToastMini("联系人添加成功，但是无法添加到乘客中：" + state.VerifyMessage);
							}
						}
						else
						{
							AppContext.HostForm.ShowErrorToastMini("添加失败：" + result);
						}
					}
				}
			};
			ctxPassengerRemove.Click += (s, e) =>
			{
				var link = ctxPassenger.Tag as LinkLabel;
				var pas = link.Tag as PassengerInTicket;

				cPassenger.Controls.Remove(link);
				Configuration.Passenger.Remove(pas);

				EnsureAddPassengerStatus();
				QueryParam.OnRequireSave();
			};
			ctxPassengerAddChild.Click += (s, e) =>
			{
				var link = ctxPassenger.Tag as LinkLabel;
				var pas = link.Tag as PassengerInTicket;

				pas = (PassengerInTicket)pas.Clone();
				pas.TicketType = 2;

				AddPassenger(pas);
			};

			//改签禁止修改乘客
			if (QueryParam.Resign)
			{
				lAddPassenger.Enabled = false;
				lClearPassenger.Enabled = false;
				cPassenger.Enabled = false;
				chkEnablePartialSubmit.Enabled = false;
				QueryParam.AutoPreSubmitConfig.EnablePartialSubmit = false;
			}
		}

		LinkLabel CreatePassengerLabel(PassengerInTicket p)
		{
			var lbl = new LinkLabel { Text = p.DisplayTitle, TextAlign = ContentAlignment.MiddleCenter, AutoSize = true, Size = new Size(100, 16), LinkBehavior = LinkBehavior.HoverUnderline, Padding = new Padding(0, 0, 5, 0), Tag = p, LinkColor = Color.RoyalBlue };
			lbl.MouseClick += (s, e) =>
			{
				var l = s as LinkLabel;
				var c = (PassengerInTicket)l.Tag;

				if (e.Button == MouseButtons.Right)
				{
					cPassenger.Controls.Remove(l);
					Configuration.Passenger.Remove(c);

					EnsureAddPassengerStatus();
					QueryParam.OnRequireSave();
				}
				else
				{
					//显示菜单
					ctxPassenger.Tag = l;
					ctxPassenger.Show(l, 0, l.Height);
				}
			};
			return lbl;
		}

		internal void AddPassenger(PassengerInTicket p)
		{
			if (Configuration.Passenger.Count >= 5)
			{
				Information("那个。。一次选择的乘客不能多于5位哦。。");
				return;
			}
			if ((p.TicketType != 2) && Configuration.Passenger.Any(s => s.Name.IsIgnoreCaseEqualTo(p.Name) && s.IdNo.IsIgnoreCaseEqualTo(p.IdNo))) return;
			cPassenger.Controls.Add(CreatePassengerLabel(p));
			Configuration.Passenger.Add(p);
			EnsureAddPassengerStatus();
			QueryParam.OnRequireSave();
		}

		void EnsureAddPassengerStatus()
		{
			lClearPassenger.Enabled = cPassenger.Controls.Count > 0;
			lAddPassenger.Enabled = cPassenger.Controls.Count < 5;
			CheckAutoSubmitStatus();
		}


		#endregion

		#region 预定状态

		void InitStatusCheck()
		{
			QueryParam.AutoPreSubmitConfig.PropertyChanged += (s, e) => CheckAutoSubmitStatus();
			CheckAutoSubmitStatus();
		}

		void CheckAutoSubmitStatus()
		{
			//if (QueryParam.AutoPreSubmitConfig.AllSetOk)
			//{
			//	lblTip.Image = Properties.Resources.tick_16;
			//	lblTip.Text = "    设置正常";
			//	lblTip.ForeColor = Color.Green;
			//}
			//else
			//{
			//	lblTip.Image = Properties.Resources.block_16;
			//	lblTip.Text = "    设置错误";
			//	lblTip.ForeColor = Color.Red;
			//}
		}

		#endregion

		#region 席位信息

		private SeatSubTypeSequenceSetting _seatSubTypeSequenceSetting;
		private Popup _seatSubTypeEditorPopup;

		void InitSeatSubTypeEditor()
		{
			_seatSubTypeSequenceSetting = new SeatSubTypeSequenceSetting(Session, QueryParam);
			_seatSubTypeEditorPopup = new Popup(_seatSubTypeSequenceSetting)
			{
				FocusOnOpen = true,
				HidingAnimation = PopupAnimations.Slide | PopupAnimations.BottomToTop,
				ShowingAnimation = PopupAnimations.Slide | PopupAnimations.TopToBottom
			};
			lnkSetSubType.Click += (s, e) =>
			{
				_seatSubTypeEditorPopup.Show(lnkSetSubType);
			};

			//绑定相关事件
			void BindEventList(EventList<SubType> list)
			{
				list.Added += (s, e) => RefreshSeatSubTypeDisplay();
				list.Removed += (s, e) => RefreshSeatSubTypeDisplay();
			}

			BindEventList(QueryParam.AutoPreSubmitConfig.SeatSubTypesBed);
			BindEventList(QueryParam.AutoPreSubmitConfig.SeatSubTypesHighSpeed);

			RefreshSeatSubTypeDisplay();
		}

		void RefreshSeatSubTypeDisplay()
		{
			string Build(EventList<SubType> list)
			{
				if (list.Count == 0)
					return null;

				return list.Select(_ => ParamData.SeatSubTypeDisplayNameSimple[_]).JoinAsString("/");
			}

			var txt = new[] { QueryParam.AutoPreSubmitConfig.SeatSubTypesHighSpeed, QueryParam.AutoPreSubmitConfig.SeatSubTypesBed }.ExceptNull().Select(Build).JoinAsString("; ").DefaultForEmpty("未设置");
			if ((QueryParam.AutoPreSubmitConfig.SeatSubTypesHighSpeed.Count > 0 || QueryParam.AutoPreSubmitConfig.SeatSubTypesBed.Count > 0) && QueryParam.AutoPreSubmitConfig.SeatList.Count == 0)
			{
				txt = "[请设置席别] " + txt;
			}
			lblSeatSubType.Text = txt;
		}

		#endregion

	}
}
