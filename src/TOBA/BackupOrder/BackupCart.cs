using System;
using System.Linq;

namespace TOBA.BackupOrder
{
	using FSLib.Extension;

	using Media;

	using Platform.SlideVc;

	using Query.Entity;

	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics;
	using System.Runtime.CompilerServices;
	using System.Threading.Tasks;
	using System.Windows.Forms;

	using TOBA.Entity;
	using TOBA.Entity.Web;

	using UI;
	using UI.Dialogs.Vc;

	class BackupCart : INotifyPropertyChanged
	{
		public BackupCart(Session session) { Session = session; }

		public Session Session { get; }

		public EventList<BackupCartItem> Items { get; } = new EventList<BackupCartItem>();

		/// <summary>
		/// 联系人
		/// </summary>
		public EventList<Passenger> Passengers { get; } = new EventList<Passenger>(3);

		public DateTime HbStartTime { get; set; }

		public DateTime HbEndTime { get; set; }

		/// <summary>
		/// 获得或设置候补截止时间
		/// </summary>
		public DateTime EndTime { get; set; }

		public HashSet<char> EnableLkList { get; set; }

		public string ToSecretList() => Items.Select(s => s.ToSecretData()).JoinAsString("|") + "|";

		public bool NeedSlideVc { get; set; }

		public string SlideToken { get; set; }

		public string SlideCSessionId { get; set; }

		public string SlideSig { get; set; }

		public object ToOrderParam()
		{
			return new
			{
				passengerInfo = Passengers.Select(s => $"{s.Type}#{s.Name}#{s.IdTypeCode}#{s.IdNo}#{s.AllEncStr}#0;").JoinAsString(""),
				jzParam = EndTime.ToString("yyyy-MM-dd#HH#mm"),
				hbTrain = Items.Select(s => $"{s.Train.Id},{s.Seat}").JoinAsString("") + "#",
				lkParam = EnableLkList?.JoinAsString("") ?? "",
				sessionId = SlideCSessionId,
				sig = SlideSig ?? "",
				scene = "nc_login"
			};
		}

		public (bool allow, string message) CanAdd(QueryResultItem train, char seat)
		{
			//已经添加过？
			if (Items.Any(s => s.Train.Id == train.Id && s.Seat == seat && s.Train.Date == train.Date))
			{
				return (false, "已经在候选列表中");
			}

			if (seat == '0' || seat == '*')
				return (false, "无座或其它不可候补");

			if (Items.Count == 4)
				return (false, "不能超过四个候选");

			var dates = Items.Select(s => s.Train.QueryResult.Date).Concat(new[] { train.QueryResult.Date }).Distinct().ToArray();
			if (dates.Length > 2)
			{
				return (false, "候补最多只能选择两个日期");
			}

			if (dates.Length == 2 && Math.Round((dates[1] - dates[0]).TotalDays) > 1)
			{
				return (false, "候补订单日期必须相邻");
			}

			//每日只能选择两个组合
			if (Items.Select(s => s.Train.QueryResult.Date).Concat(new[] { train.QueryResult.Date }).GroupBy(s => s).Any(s => s.Count() > 2))
			{
				return (false, "每日选择的车次+席别组合不能超过两个");
			}

			return (true, null);
		}

		/// <summary>
		/// 导入
		/// </summary>
		/// <param name="passengers">The passengers.</param>
		/// <returns></returns>
		public bool ImportQueryPassengers(List<PassengerInTicket> passengers)
		{
			if (passengers?.Any() != true || Session.UserProfile.Passengers == null)
				return false;

			var pas = passengers.Select(s =>
				{
					var p = Session.UserProfile.Passengers?.FindMatch(s);
					if (p == null || p.Type == s.TicketType)
						return p;

					return p.CreateChild();
				}).
				ExceptNull().
				ToArray();

			if (pas.Length != passengers.Count)
			{
				AppContext.HostForm.ShowWarningToastMini("联系人数据不正确，请关闭当前查询并重新查询后再试。");
				return false;
			}

			Passengers.Clear();
			pas.Take(3).ForEach(Passengers.Add);
			if (pas.Length > 3)
				AppContext.HostForm.ShowInfoToastMini("候补订单最多3个联系人，多出来的乘客已经忽略。");

			return true;
		}

		/// <summary>
		/// 同步最新的数据
		/// </summary>
		public async Task SyncTrainSubmitInfoAsync(QueryResult result)
		{
			var map = result.OriginalList.ToDictionary(s => $"{s.Id}-{s.FromStation.Code}-{s.ToStation.Code}", s => s);
			foreach (var item in Items.ToArray())
			{
				if (item.Train.Date != result.Date)
					continue;

				var t = map.GetValue($"{item.Train.Id}-{item.Train.FromStation.Code}-{item.Train.ToStation.Code}");
				if (t == null || t.SubmitOrderInfo.IsNullOrEmpty() || t.SubmitOrderInfo == item.Train.SubmitOrderInfo)
					continue;
				if (t.FromStation.Code != item.Train.FromStation.Code || t.ToStation.Code != item.Train.ToStation.Code)
					continue;

				item.Train = t;
				Debug.WriteLine($"同步SecretStr：{item.Query.CurrentDepartureDate} {item.Train.Code} => {item.Train.SubmitOrderInfo}");
			}
		}

		#region 自动提交

		private bool _inAutoSubmit;

		/// <summary>
		/// 是否正在自动提交候补订单
		/// </summary>
		public bool InAutoSubmit
		{
			get => _inAutoSubmit;
			set
			{
				if (value == _inAutoSubmit) return;
				_inAutoSubmit = value;
				OnPropertyChanged();
			}
		}

		private int _autoSubmitCount;

		/// <summary>
		/// 已经提交的次数
		/// </summary>
		public int AutoSubmitCount
		{
			get => _autoSubmitCount;
			set
			{
				if (value == _autoSubmitCount) return;
				_autoSubmitCount = value;
				OnPropertyChanged();
			}
		}

		private string _lastSubmitMessage;

		/// <summary>
		/// 最后提交信息
		/// </summary>
		public string LastSubmitMessage
		{
			get => _lastSubmitMessage;
			set
			{
				if (value == _lastSubmitMessage) return;
				_lastSubmitMessage = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// 开始或停止自动提交候补订单
		/// </summary>
		public (bool success, string message) StartOrStopAutoSubmit()
		{
			InAutoSubmit = !InAutoSubmit;

			if (InAutoSubmit)
			{
				//开始
				AutoSubmitCount = 0;
				AutoSubmitHbOrderLoopAsync();
			}

			return (true, null);
		}

		async Task AutoSubmitHbOrderLoopAsync()
		{
			var service = Session.GetService<IBackupOrderService>();
			var ok = false;
			var msg = "";

			while (InAutoSubmit)
			{
				AutoSubmitCount++;
				LastSubmitMessage = $"[{AutoSubmitCount}] 开始提交候补订单...";

				(ok, msg) = await service.SubmitOrderRequestAsync();
				if (ok)
					(ok, msg) = await AutoConfirmOrderAsync();

				if (!ok)
				{
					LastSubmitMessage = $"[{AutoSubmitCount}] 失败：{msg}";

					//失败信息
					if (msg.Contains("车票信息已过期"))
					{
						msg = "车票信息已过期。启用自动提交候补时，请保持对应的车票信息也在刷票中，才可以正常保持信息更新。";
						break;
					}

					if (msg.Contains("待兑现") || msg.Contains("待支付") || msg.Contains("未完成"))
					{
						Session.OnRequestShowPanel(PanelIndex.HbOrder);
						break;
					}

					if (msg.Contains("人证一致性"))
					{
						break;
					}
				}
				else
				{
					break;
				}

				var sleep = BackupOrderConfiguration.Instance.AutoSubmitOrderInterval;
				if (msg.Contains("当前时间"))
				{
					//等待整点
					var next = 60 - DateTime.Now.Second + 1;
					sleep = Math.Max(1000, 1000 * next);
				}
				await Task.Delay(sleep);
			}

			InAutoSubmit = false;
			if (ok)
			{
				Session.OnRequestShowPanel(PanelIndex.HbOrder);
				Session.OnHbOrderChanged();

				AppContext.MainForm.ShowBalloonTip(5000, "候补订单提交成功", "请尽快付款.", ToolTipIcon.Info);
				if (BackupOrderConfiguration.Instance.PlayMusicOnAutoSubmitSuccess)
					HbOrderMusic.Instance.PlayAsync();
			}
			else
			{
				AppContext.MainForm.ShowBalloonTip(15000, "候补订单自动提交已终止", msg, ToolTipIcon.Info);
				LosingSoundPlayer.Instance.PlayAsync();
			}
		}

		async Task<(bool success, string msg)> AutoConfirmOrderAsync()
		{
			var service = Session.GetService<IBackupOrderService>();
			EndTime = HbEndTime;
			if (NeedSlideVc)
			{
				LastSubmitMessage = $"[{AutoSubmitCount}] 需要滑动验证码...";
				using (var vcd = new SlideVcForm(Session, SlideToken, SlideAppId.Order))
				{
					vcd.TopMost = true;
					vcd.StartPosition = FormStartPosition.CenterScreen;
					AppContext.MainForm.ShowBalloonTip(5000, "请完成候补订单滑块验证", "正在提交候补订单，请尽快操作", ToolTipIcon.Info);
					vcd.Show(AppContext.HostForm);

					if (!await vcd.UserOpTask)
					{
						return (false, "滑块验证已取消");
					}

					SlideSig = vcd.Sig;
					SlideCSessionId = vcd.CfSessionId;
				}
			}

			LastSubmitMessage = $"[{AutoSubmitCount}] 确认候补订单...";
			var (ok, msg, data) = await service.CommitBackupOrderAsync();
			if (!ok)
			{
				return (false, msg);
			}

			LastSubmitMessage = $"[{AutoSubmitCount}] 提交成功，排队中...";
			while (true)
			{
				var (okq, msgq, dataq) = await service.QueryHbQueueAsync();
				if (!okq)
				{
					return (false, msgq);
				}
				if (dataq.Status == 1)
				{
					return (true, "候补成功！");
				}


				LastSubmitMessage = $"[{AutoSubmitCount}] 提交成功，排队中...{msgq}";

				await Task.Delay(BackupOrderConfiguration.Instance.QueryBackupOrderQueueTime);
			}
		}

		#endregion

		public event PropertyChangedEventHandler PropertyChanged;

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
	}
}
