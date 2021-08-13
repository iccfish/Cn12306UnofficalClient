namespace TOBA.Order
{
	using Configuration;

	using Data;

	using FSLib.Network.Http;

	using Query.Entity;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Threading;

	using TOBA.Entity;

	internal abstract class SubmitOrderBase : TOBA.Workers.WorkerBase
	{
		/// <summary>
		/// 创建 <see cref="SubmitOrderWorker" />  的新实例(SubmitOrderWorker)
		/// </summary>
		public SubmitOrderBase(Session session, QueryResultItem train, TOBA.Entity.QueryParam query)
			: base(session)
		{
			Train = train;
			Query = query;
		}

		public char TicketCountSeat { get; private set; }

		public int TicketCount { get; private set; } = -1;

		public int TicketCountNoSeat { get; private set; } = -1;

		protected string KeyCheckIsChange { get; set; }

		protected string LeftTicketStr { get; set; }

		/// <summary>
		/// 联系人数据
		/// </summary>
		protected string[] PassengerString { get; private set; }

		protected string TrainLocation { get; set; }

		/// <summary>
		/// 获得是否是异步提交订单
		/// </summary>
		public bool Async { get; protected set; }

		/// <summary>
		/// 获得或设置是否购买整包
		/// </summary>
		public bool BuyAllSeat { get; set; }

		public DateTime Date => Train.QueryResult.Date;

		public Dictionary<string, string> FormData { get; set; }

		public bool IsStudent => Query.QueryStudentTicket;

		public DateTime? VcBaseTime { get; protected set; }

		/// <summary>
		/// 是否需要验证码
		/// </summary>
		public bool? NeedVc { get; protected set; } = null;

		#region 滑动验证

		public bool NeedSlideVc { get; protected set; }
		public string SlideVcToken { get; protected set; }
		public string SlideCSessionId { get; set; }
		public string SlideSig { get; set; }

		#endregion

		private int _needVcTime;

		/// <summary>
		/// 提交前需要等待的时间
		/// </summary>
		public int NeedVcTime
		{
			get
			{
				//TODO: 不能忽略，否则2000人排队
				//if (OrderConfiguration.Instance.IgnoreSafeTime)
				//	return 0;
				return _needVcTime;
			}
			protected set => _needVcTime = value;
		}

		/// <summary>
		/// 订单编号
		/// </summary>
		public string OrderID { get; protected set; }


		/// <summary>
		/// 获得或设置订单的联系人
		/// </summary>
		public PassengerInTicket[] Passengers { get; set; }

		public QueryParam Query { get; set; }

		/// <summary>
		/// 获得是否发生了排队警告
		/// </summary>
		public bool QueueWarning { get; protected set; }

		public string RandCode { get; set; }
		public string Token { get; private set; }
		public QueryResultItem Train { get; set; }
		public int WaitCount { get; set; }

		public bool CanChooseSeat { get; set; }

		public bool CanChooseBed { get; set; }

		public bool CanChooseBedMiddle { get; set; }

		public string ChooseSeatTypes { get; set; }

		protected void SetTicketCount(char seat, int ticketCount, int ticketCountNoSeat)
		{
			TicketCountSeat = seat;
			TicketCount = ticketCount;
			TicketCountNoSeat = ticketCountNoSeat;
			OnTicketCountChanged();
		}

		protected void SetTicketCount(char seat, string ticketCountStr)
		{
			if (ticketCountStr.IsNullOrEmpty())
			{
				SetTicketCount(seat, -1, -1);
				return;
			}

			var args = ticketCountStr.Split(',');
			SetTicketCount(seat, args[0].ToInt32(-1), args.Length > 1 ? args[1].ToInt32(-1) : -1);
		}

		public event EventHandler TicketCountChanged;

		protected void LoadOrderIdFromNotComplete()
		{
			StateMessage = "正在等待订单现身...";
			//等待订单结果
			var count = 0;
			var om = new OrderManager();
			om.Session = Session;

			while (count++ < 3)
			{
				om.QueryNotCompleteInternal();

				if (om.NotCompleteOrders.Count > 0)
				{
					OrderID = om.NotCompleteOrders[0].SequenceNo;
					break;
				}

				Thread.Sleep(2000);
			}
		}

		protected virtual bool ReEntryQueryPage(string msg)
		{
			if (string.IsNullOrEmpty(msg) || ApiConfiguration.Instance.ResubmitTextDetection.All(s => msg.IndexOf(s, 0, StringComparison.OrdinalIgnoreCase) == -1))
				return false;

			//重新进入查票页
			var session = Session;
			var queryCtx = session.NetClient.RunRequestLoop(_ => session.NetClient.Create<string>(HttpMethod.Get, "leftTicket/init", "leftTicket/init"));
			session.IsPassengerLoaded = false;
			if (!queryCtx.IsSuccess)
			{
				throw new Exception("无法查询页面信息");
			}

			return true;
		}

		/// <summary>
		/// 检测错误信息是否需要重试
		/// </summary>
		/// <param name="error"></param>
		/// <returns></returns>
		protected virtual bool SetError(string error)
		{
			//filter html codes
			error = Regex.Replace(error, @"</?.*?>", "");

			if (ReEntryQueryPage(error))
			{
				return true;
			}

			if (!string.IsNullOrEmpty(error) && error.IndexOf("未处理的订单", StringComparison.Ordinal) != -1)
			{
				//有未处理订单？尝试看看是不是被强退了。
				if (Session.NetClient.VerifySessionValid() == false)
				{
					error = "用户未登录";
				}
			}

			Error = error;
			return false;
		}

		/// <summary>
		/// 创建事件参数
		/// </summary>
		/// <returns></returns>
		public OrderSubmitEventArgs CreateEventArgs()
		{
			var ctx = new OrderSubmitContext
			{
				Passengers = Passengers,
				QueryParam = Query,
				Train = Train,
				Session = Session
			};

			var e = new OrderSubmitEventArgs(ctx);
			Query.SubmitEventArgs = e;
			return e;
		}

		protected virtual void OnTicketCountChanged()
		{
			TicketCountChanged?.Invoke(this, EventArgs.Empty);
		}

		/// <summary>
		/// 设置票数并检查是否允许继续提交
		/// </summary>
		/// <returns></returns>
		protected bool CheckTicketCount()
		{
			if (OrderConfiguration.Instance.TryStopNoTicket && TicketCount <= 0 && TicketCountNoSeat <= 0)
			{
				SetPrepareOrderError("实时余票无票，停止提交，请重试。");
				return false;
			}

			if (OrderConfiguration.Instance.TryStopStandTicket && TicketCount <= 0 && TicketCountNoSeat > 0)
			{
				if (Passengers?.Any(s => s.OriginalSeatType == '0' || s.OriginalSeatType == '\0') == false)
				{
					SetPrepareOrderError("余票不足，只有无座票，停止提交，请重试。");
					return false;
				}
			}

			return true;
		}

		#region 联系人工具

		protected (string seatTypes, string seatBedDetails) BuildSeatInfo()
		{
			Passengers.ForEach(s => s.SeatSubType = GetFinalSeatSubtypeCode(s.SeatType, s.SeatSubType));

			var seatBed = CanChooseBed ? Passengers.Count(s => s.SeatSubType == SubType.X) + "" + Passengers.Count(s => s.SeatSubType == SubType.Z) + Passengers.Count(s => s.SeatSubType == SubType.S) : "";
			var seatDetails = string.Empty;
			if (CanChooseSeat)
			{
				var counter = new Dictionary<SubType, int>();
				seatDetails = Passengers.Select(s =>
				{
					if (s.SeatSubType == SubType.Random)
						return string.Empty;
					if (!counter.TryGetValue(s.SeatSubType, out var count))
					{
						counter.Add(s.SeatSubType, 1);
						count = 1;
					}
					else
					{
						counter[s.SeatSubType] = ++count;
					}

					if (count > 2)
						return "";
					return count + s.SeatSubType.ToString();
				}).JoinAsString("");
			}

			return (seatDetails, seatBed);
		}

		SubType GetFinalSeatSubtypeCode(char seat, SubType type)
		{
			if (!CanChooseSeat && type.IsHighSpeedSeat())
			{
				return SubType.Random;
			}

			if (!CanChooseBed && type.IsBed())
			{
				return SubType.Random;
			}

			if (!CanChooseBedMiddle && type == SubType.Z)
				return SubType.Random;

			if (ParamData.GetSeatSubTypeList(seat)?.Contains(type) != true)
				return SubType.Random;

			return type;
		}

		/// <summary>
		/// 限制数目的席别类型
		/// </summary>
		private static HashSet<SubType> _limitedSeatTypes = new HashSet<SubType>
		{
			SubType.A,
			SubType.B,
			SubType.C,
			SubType.D,
			SubType.F
		};

		/// <summary>
		/// 确认席别选择是否是有效的
		/// </summary>
		/// <param name="passengers"></param>
		/// <returns></returns>
		public (bool valid, string error) CheckIfSeatSubTypeValid(IEnumerable<PassengerInTicket> passengers)
		{
			if (passengers.Where(s => _limitedSeatTypes.Contains(s.SeatSubType)).GroupBy(s => s.SeatSubType).Any(s => s.Count() > 2))
				return (false, "选座情况下同座位不能选择超过2次");

			return (true, null);
		}


		#endregion

		#region 准备提交数据

		public void Prepare()
		{
			RunWorker(PrepareInternal, OnPrepareFinished, OnPrepareFailed);
		}

		protected virtual void PrepareInternal()
		{
			if (!Session.IsPassengerLoaded)
			{
				Session.LoadPassengers();
			}
		}


		/// <summary>
		/// 设置提交订单的错误信息
		/// </summary>
		/// <param name="error"></param>
		protected virtual void SetPrepareOrderError(string error)
		{
			if (SetError(error))
			{
				PrepareInternal();
			}
		}

		/// <summary>
		/// 引发 <see cref="PrepareFinished" /> 事件
		/// </summary>
		protected virtual void OnPrepareFinished()
		{
			var handler = PrepareFinished;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 引发 <see cref="PrepareFailed" /> 事件
		/// </summary>
		protected virtual void OnPrepareFailed()
		{
			var handler = PrepareFailed;
			if (handler != null)
				handler(this, EventArgs.Empty);

			//全局事件
			var oe = CreateEventArgs();
			oe.OrderSubmitContext.Message = Error;
			Session.OnOrderSubmitFailed(Session, oe);
		}

		/// <summary>
		/// 开始提交订单
		/// </summary>
		public void BeginSubmitOrder()
		{
			RunWorker(() =>
			{
				SubmitOrderInternal();
			}, OnSubmitOrderSuccess, OnSubmitOrderFailed);
		}

		/// <summary>
		/// 正式提交订单
		/// </summary>
		protected virtual bool SubmitOrderInternal()
		{
			Statistics.Current.SubmitCount++;

			if (PassengerString == null)
			{
				var (ok, msg) = PreparePassenger();
				if (!ok)
				{
					SetError(msg);
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 设置提交订单的错误信息
		/// </summary>
		/// <param name="error"></param>
		protected virtual void SetSubmitOrderError(string error)
		{
			if (SetError(error))
			{
				SubmitOrderInternal();
			}
			else
			{
				if (error.IndexOf("没有足够") != -1 || error.IndexOf("超过") != -1)
				{
					RecentlySubmitFailedTokenStorageProvider.Instance.AddDisableTicketData(Train);
				}
			}
		}

		/// <summary>
		/// 准备联系人
		/// </summary>
		protected (bool success, string msg) PreparePassenger()
		{
			var pass1 = new List<string>();
			var pass2 = new List<string>();

			foreach (var p in Passengers)
			{
				//fix 联系人
				if (p.AllEncStr.IsNullOrEmpty())
				{
					var str = Session.UserProfile.Passengers.FindMatch(p)?.AllEncStr;
					if (str.IsNullOrEmpty())
					{
						return (false, "联系人信息过期，请关闭当前查询并重新创建。");
					}

					p.AllEncStr = str;
				}

				pass1.Add($"{p.SeatType},{0},{p.TicketType},{p.Name},{p.IdType},{p.IdNo},{p.Mobile ?? ""},{(Query.Resign ? "Y" : "N")},{p.AllEncStr}");
				pass2.Add(p.TicketType == 2 ? " " : $"{p.Name},{p.IdType},{p.IdNo},{p.TicketType}");
			}

			pass2.Add("");

			PassengerString = new[]
			{
				pass1.JoinAsString("_"),
				pass2.JoinAsString("_")
			};

			return (true, null);
		}

		/// <summary>
		/// 引发 <see cref="SubmitOrderSuccess" /> 事件
		/// </summary>
		protected virtual void OnSubmitOrderSuccess()
		{
			var handler = SubmitOrderSuccess;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 引发 <see cref="SubmitOrderFailed" /> 事件
		/// </summary>
		protected virtual void OnSubmitOrderFailed()
		{
			var handler = SubmitOrderFailed;
			if (handler != null)
				handler(this, EventArgs.Empty);

			//全局事件
			var oe = CreateEventArgs();
			oe.OrderSubmitContext.Message = Error;
			Session.OnOrderSubmitFailed(Session, oe);
		}

		/// <summary>
		/// 准备完成
		/// </summary>
		public event EventHandler PrepareFinished;

		/// <summary>
		/// 准备失败
		/// </summary>
		public event EventHandler PrepareFailed;

		#endregion

		#region 提交订单

		/// <summary>
		/// 当订单提交成功时引发
		/// </summary>
		public event EventHandler SubmitOrderSuccess;

		/// <summary>
		/// 当订单提交失败时引发
		/// </summary>
		public event EventHandler SubmitOrderFailed;

		#endregion
	}
}
