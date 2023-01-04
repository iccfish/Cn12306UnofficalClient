using Newtonsoft.Json;

namespace TOBA.Query.Entity
{
	using Data;

	using System;
	using System.Diagnostics;
	using System.Drawing;
	using System.Linq;
	using System.Text.RegularExpressions;
	using System.Xml.Serialization;

	using TOBA.Entity;

	/// <summary>
	/// 表示一个查询到的车次
	/// </summary>
	[DebuggerDisplay("车次 {Code} 编号 {Id} IsAvailable={IsAvailable} IsLimitSell={IsLimitSell} NoTicketNeeded={NoTicketNeeded}")]
	internal class QueryResultItem : Dto
	{
		public QueryResultItem(QueryResult parent)
		{
			QueryResult = parent;
			Date = parent.Query.CurrentDepartureDate;
		}


		/// <summary>
		/// 获得所属的查询结果
		/// </summary>
		public QueryResult QueryResult { get; }


		LeftTickets _ticketCount;
		string _submitOrderInfo;
		string _code;

		public QueryLeftTicketResult RawResult { get; set; }

		[JsonIgnore]
		public QueryLeftTicketItem QueryLeftTicketItem => RawResult?.queryLeftNewDTO;

		/// <summary>
		/// 始发站预售信息提示
		/// </summary>
		public StartStationSellInfo StartStationSellInfo { get; set; }

		/// <summary>
		/// ID
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// 唯一KEY
		/// </summary>
		[XmlIgnore]
		[JsonIgnore]
		public string Key => $"{Id}-{FromStation.Code}-{ToStation.Code}";

		/// <summary>
		/// 停靠站查询ID
		/// </summary>
		public string TrainStopData { get; set; }

		/// <summary>
		/// 是否支持二代身份证直接进出站
		/// </summary>
		public bool SupportIdCard => QueryLeftTicketItem.is_support_card != "0";

		/// <summary>
		/// 获得是否有空调
		/// </summary>
		public bool HasAc => !(QueryLeftTicketItem.train_seat_feature == "0" || QueryLeftTicketItem.train_seat_feature == "2" || QueryLeftTicketItem.train_seat_feature == "4");

		/// <summary>
		/// 车次号码
		/// </summary>
		public string Code
		{
			get => _code;
			set
			{
				_code = value;
				if (!string.IsNullOrEmpty(_code))
				{
					if (Regex.IsMatch(_code, @"^(L\d+|Z[46]\d{3}|T3\d{3}|K[456]\d{3}|[GD]4\d{3}|G9\d{3}|3\d{3})$", RegexOptions.IgnoreCase))
						TrainClass = 'L';
					else
						TrainClass = _code[0];
				}
			}
		}

		public char TrainClass { get; private set; }

		public TicketStationInfo StartStation { get; set; }

		public TicketStationInfo EndStation { get; set; }

		/// <summary>
		/// 发站
		/// </summary>
		public TicketStationInfo FromStation { get; set; }

		/// <summary>
		/// 到站
		/// </summary>
		public TicketStationInfo ToStation { get; set; }

		/// <summary>
		/// 需要时间
		/// </summary>
		public TimeSpan ElapsedTime { get; set; }

		/// <summary>
		/// 车次座位的车票数
		/// </summary>
		public LeftTickets TicketCount
		{
			get { return _ticketCount ??= new LeftTickets(); }
			set => _ticketCount = value;
		}

		/// <summary>
		/// 是否可以预定
		/// </summary>
		public bool IsAvailable { get; set; }

		/// <summary>
		/// 起售时间
		/// </summary>
		public DateTime? BeginSellTime { get; set; }

		/// <summary>
		/// 获得或设置提交订单数据
		/// </summary>
		public string SubmitOrderInfo
		{
			get { return _submitOrderInfo; }
			set
			{
				_submitOrderInfo = value;
				TrainDataInfo = null;
				if (!string.IsNullOrEmpty(value))
				{
					try
					{
						TrainDataInfo = new TrainDataInfo(value);
					}
					catch (Exception)
					{
					}
				}
			}
		}

		TrainDataInfo _trainDataInfo;

		[JsonIgnore]
		public TrainDataInfo TrainDataInfo
		{
			get { return _trainDataInfo ?? (_trainDataInfo = new TrainDataInfo("")); }
			private set { _trainDataInfo = value; }
		}

		/// <summary>
		/// 检查结果
		/// </summary>
		public bool NoTicketNeeded { get; set; }

		/// <summary>
		/// 获得停靠站信息
		/// </summary>
		public TrainStopCollection TicketStationInfo { get; set; }

		/// <summary>
		/// 获得是否已经加载停靠站信息
		/// </summary>
		public bool IsTicketStationInfoLoaded => TicketStationInfo != null;

		public string ButtonTextInfo { get; set; }

		/// <summary>
		/// 是否是限售状态？
		/// </summary>
		public bool IsLimitSell { get; set; }

		/// <summary>
		/// 有选择的车次
		/// </summary>
		public int Selected { get; set; }

		/// <summary>
		/// 有选择的席别
		/// </summary>
		public int SeatSelected { get; set; }

		public char FindCorrectSeat(char seat)
		{
			if (!TicketCount.ContainsKey(seat))
			{
				var altSeat = ParamData.GetSeatCompatibleMap(seat).FirstOrDefault(s => TicketCount.ContainsKey(s));
				seat = altSeat > 0 ? altSeat : '0';
			}
			if (seat == '0')
			{
				seat = new[] { 'O', '8', '1', '2' }.FirstOrDefault(s => TicketCount.ContainsKey(s));
			}

			return seat;
		}

		/// <summary>
		/// 获得或设置原始的查询结果
		/// </summary>
		[JsonIgnore]
		public QueryParam QueryParam { get; set; }

		public bool TicketCompareTo(QueryResultItem prev)
		{
			var keys = TicketCount.Keys;
			var changes = 0;

			foreach (var key in keys)
			{
				var current = TicketCount[key];
				var changeAmount = 0;

				var tkn = prev.TicketCount?.GetValue(key);
				if (tkn == null)
					changeAmount = current.TicketForCompute;
				else changeAmount = current.TicketForCompute - tkn.TicketForCompute;

				current.MemoText = changeAmount == 0 ? "--" : changeAmount > 0 ? "+" + changeAmount : changeAmount.ToString();
				current.MemoTextColorName = changeAmount == 0 ? KnownColor.Gray : changeAmount > 0 ? KnownColor.Red : KnownColor.Green;
				if (changeAmount != 0)
					changes++;
				else
				{
					current.NotNeed = true;
				}
			}
			foreach (var key in prev.TicketCount.Keys.Except(TicketCount.Keys))
			{
				var pv = prev.TicketCount[key];

				TicketCount.Add(key, new LeftTicketData(0, null, key)
				{
					MemoText = "-" + pv.Count,
					MemoTextColorName = KnownColor.Green,
					NotAvailable = false,
					NotNeed = pv.NotNeed
				});
				changes++;
			}

			return changes != 0;
		}

		public string ControlMessage { get; set; }

		/// <summary>
		/// 控制标记
		/// </summary>
		public int ControlFlag { get; set; }

		private string _sellTimeTip;

		[JsonIgnore]
		public string SellTimeTip
		{
			get
			{
				if (_sellTimeTip == null)
				{
					if (BeginSellTime == null || BeginSellTime.Value.Year == DateTime.MinValue.Year)
					{
						_sellTimeTip = "不在预售期里....";
					}
					else
					{
						_sellTimeTip = BeginSellTime.Value.MakeDateFriendly() + BeginSellTime.Value.Hour + "点" + (BeginSellTime.Value.Minute > 0 ? BeginSellTime.Value.Minute + "分" : "") + "起售";
					}
				}
				return _sellTimeTip;
			}
		}

		/// <summary>
		/// 可能是无效的结果
		/// </summary>
		public bool AlmostIllegalResult { get; set; }

		/// <summary>
		/// 是否允许积分兑换
		/// </summary>
		public bool CanExchangeByCredit { get; set; }


		/// <summary>
		/// 是否允许候补
		/// </summary>
		public bool AllowBackup { get; set; }

		/// <summary>
		/// 获得当前席别的候补订票是否可用
		/// </summary>
		/// <param name="code"></param>
		/// <returns></returns>
		public bool IsSeatBackupAvailable(char code) => AllowBackup && code != '0' && code != '*';

		public (int days, string elapsedTimeStr) ElapsedTimeInfo
		{
			get
			{
				return ((ToStation.ArriveTime.Value.Date - FromStation.DepartureTime.Value.Date).Days,
					(ElapsedTime.Hours + ElapsedTime.Days * 24).ToString("00") + ":" + ElapsedTime.Minutes.ToString("00"));
			}
		}

		/// <summary>
		/// 车次的日期
		/// </summary>
		public DateTime Date { get; }
		public bool IsSmartD { get; set; }
		public bool IsFuXing { get; set; }
		public bool IsQuiet { get; set; }
	}
}
