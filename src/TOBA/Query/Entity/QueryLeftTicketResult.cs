namespace TOBA.Query.Entity
{
	using System;
	using System.Linq;
	using System.Text.RegularExpressions;

	using Newtonsoft.Json;

	using TOBA.Configuration;
	using TOBA.Entity;
	using TOBA.Entity.Web;

	using WebLib;

	internal partial class QueryLeftTicketResult
	{
		public QueryResultItem ToQueryResultItem(QueryResult result)
		{
			var queryDate = result.Date;
			var tinfo = queryLeftNewDTO;

			var startTrainDateMatch = Regex.Match(queryLeftNewDTO.start_train_date, @"(\d{4})(\d{2})(\d{2})");
			var trainControlDayMatch = Regex.Match(tinfo.control_train_day ?? "", @"(\d{4})(0(\d)|([^0]\d))(0(\d)|([^0]\d))");
			DateTime? trainControlDay = trainControlDayMatch.Success ? new DateTime(
																					 trainControlDayMatch.GetGroupValue(1).ToInt32(),
																					 trainControlDayMatch.GetGroupValue(3).ToInt32(),
																					 trainControlDayMatch.GetGroupValue(5).ToInt32()
																					 ) : (DateTime?)null;
			var train = new QueryResultItem(result)
			{
				RawResult = this,
				Id = tinfo.train_no,
				Code = tinfo.station_train_code,
				ElapsedTime = new TimeSpan(0, tinfo.lishiValue, 0),
				StartStation = new TicketStationInfo()
				{
					StationName = tinfo.start_station_name,
					Code = tinfo.start_station_telecode,
					DepartureTime = startTrainDateMatch.Success ? (DateTime?)new DateTime(startTrainDateMatch.GetGroupValue(1).ToInt32(), startTrainDateMatch.GetGroupValue(2).ToInt32(), startTrainDateMatch.GetGroupValue(3).ToInt32()) : null
				},
				FromStation = new TicketStationInfo()
				{
					StationName = tinfo.from_station_name,
					Code = tinfo.from_station_telecode,
					IsFirst = tinfo.from_station_telecode == tinfo.start_station_telecode
				},
				ToStation = new TicketStationInfo()
				{
					StationName = tinfo.to_station_name,
					Code = tinfo.to_station_telecode,
					IsEnd = tinfo.to_station_telecode == tinfo.end_station_telecode
				},
				EndStation = new TicketStationInfo()
				{
					StationName = tinfo.end_station_name,
					Code = tinfo.end_station_telecode
				},
				ControlFlag = tinfo.controlled_train_flag ?? 0,
				ControlMessage = tinfo.controlled_train_message,
				AlmostIllegalResult = !ApiConfiguration.Instance.DisableIllegalDetect && tinfo.controlled_train_flag == null,
				AllowBackup         = tinfo.AllowBackup,
				IsSmartD            = tinfo.IsSmartD,
				IsQuiet             = tinfo.IsQuiet,
				IsFuXing            = tinfo.IsFuXing
			};
			train.FromStation.DepartureTime = queryDate.Add(ParseTimeSpan(tinfo.start_time));
			train.ToStation.ArriveTime = train.FromStation.DepartureTime.Value.Add(train.ElapsedTime);
			train.SubmitOrderInfo = secretStr;
			train.IsAvailable = tinfo.canWebBuy == "Y";
			train.ButtonTextInfo = Regex.Replace(buttonTextInfo ?? "", @"(\s|<[^>]*>)", "");
			train.IsLimitSell = train.ControlFlag > 0 || train.ButtonTextInfo.IndexOf("暂售", StringComparison.Ordinal) != -1;
			train.CanExchangeByCredit = tinfo.exchange_train_flag == "1";
			if (!train.IsAvailable && trainControlDay < queryDate && trainControlDay.HasValue)
			{
				train.ControlFlag = 1;
				train.IsLimitSell = true;
				train.ControlMessage = $"限售至{trainControlDay.Value.ToShortDateString()}";
			}

			TOBA.Entity.ParseUtility.GetTicketCountFromOrderInfo(tinfo, train.TicketCount);
			//折扣
			if (!string.IsNullOrEmpty(tinfo.yp_ex))
			{
				for (var i = 0; i < (tinfo.yp_ex.Length - 1); i += 2)
				{
					if (tinfo.yp_ex[i + 1] == '1')
						train.TicketCount[tinfo.yp_ex[i]].HasDiscount = true;
				}
			}
			if (buttonTextInfo?.IndexOf("起售", StringComparison.Ordinal) >= 0)
			{
				var now = DateTime.Now.Date;
				TOBA.Entity.ParseUtility.Match(@"(0*(\d+)月0*(\d+)日)?(\d+)\s*点\s*((\d+)分)?\s*起售", Regex.Replace(buttonTextInfo, "<[^>]+>", ""), _ =>
				{
					if (_[0].IsNullOrEmpty())
					{
						train.BeginSellTime = now.Date.AddHours(_[4].ToInt32());
					}
					else
					{
						var mm = _[2].ToInt32(now.Month);
						var dd = _[3].ToInt32(now.Day);
						var minute = _[6].ToInt32(0);

						var year = now.Year;
						if (mm < now.Month)
							year++;
						train.BeginSellTime = new DateTime(year, mm, dd, _[4].ToInt32(), minute, 0);
					}
				}, "起售时间");
			}
			//if (tinfo.controlled_train_flag == 0 && !tinfo.control_train_day.IsNullOrEmpty() && !train.IsAvailable)
			//{
			//	var tm = Regex.Match(tinfo.control_train_day, @"(\d2)(\d2)");

			//	if (dm.Success && tm.Success)
			//		train.BeginSellTime = new DateTime(
			//										 dm.GetGroupValue(1).ToInt32(),
			//										 dm.GetGroupValue(2).ToInt32(),
			//										 dm.GetGroupValue(3).ToInt32(),
			//										 tm.GetGroupValue(1).ToInt32(),
			//										 tm.GetGroupValue(2).ToInt32(),
			//										 0
			//										);
			//}

			//有可疑的结果？


			return train;
		}

		TimeSpan ParseTimeSpan(string str)
		{
			var m = Regex.Match(str, @"(\d{2}):?(\d{2})");
			if (m.Success)
				return new TimeSpan(m.Groups[1].Value.TrimStart('0').ToInt32(), m.Groups[2].Value.TrimStart('0').ToInt32(), 0);

			return TimeSpan.Zero;
		}

		DateTime ToDateTime(string datePart, string timePart = null)
		{
			var m = Regex.Match(datePart, @"(\d{4})[-/]?(\d{2})[-/]?(\d{2})");
			if (m.Success)
			{
				var date = new DateTime(m.Groups[1].Value.ToInt32(), m.Groups[2].Value.TrimStart('0').ToInt32(), m.Groups[3].Value.TrimStart('0').ToInt32());
				if (!timePart.IsNullOrEmpty())
					date = date.Add(ParseTimeSpan(timePart));

				return date;
			}

			return DateTime.MinValue;
		}
	}


	internal partial class QueryLeftTicketResult : Dto
	{
		[JsonProperty("queryLeftNewDTO")]
		public QueryLeftTicketItem queryLeftNewDTO { get; set; }
		public string secretStr { get; set; }
		public string buttonTextInfo { get; set; }
	}

	internal class QueryLeftTicketItem : Dto
	{
		public string train_no { get; set; }
		public string station_train_code { get; set; }
		public string start_station_telecode { get; set; }
		public string start_station_name { get; set; }
		public string end_station_telecode { get; set; }
		public string end_station_name { get; set; }
		public string from_station_telecode { get; set; }
		public string from_station_name { get; set; }
		public string to_station_telecode { get; set; }
		public string to_station_name { get; set; }
		public string start_time { get; set; }
		public string arrive_time { get; set; }
		public string day_difference { get; set; }
		public string train_class_name { get; set; }
		public string lishi { get; set; }
		public string canWebBuy { get; set; }
		public int lishiValue { get; set; }
		public string yp_info { get; set; }
		public string control_train_day { get; set; }
		public string start_train_date { get; set; }
		public string seat_feature { get; set; }
		public string yp_ex { get; set; }
		public string train_seat_feature { get; set; }
		public string seat_types { get; set; }
		public string location_code { get; set; }
		public string from_station_no { get; set; }
		public string to_station_no { get; set; }
		public int control_day { get; set; }
		public string sale_time { get; set; }
		public string is_support_card { get; set; }
		public string gr_num { get; set; }
		public string gg_num { get; set; }
		public string qt_num { get; set; }
		public string rw_num { get; set; }
		public string tz_num { get; set; }
		public string wz_num { get; set; }
		public string yb_num { get; set; }
		public string yw_num { get; set; }
		public string yz_num { get; set; }
		public string ze_num { get; set; }
		public string zy_num { get; set; }
		public string rz_num { get; set; }
		public string swz_num { get; set; }

		public string srrb_num { get; set; }

		#region 2015年11月19日新增

		public int? controlled_train_flag { get; set; }

		public string controlled_train_message { get; set; }

		#endregion


		/// <summary>
		/// 是否允许积分兑换
		/// </summary>
		public string exchange_train_flag { get; set; }

		/// <summary>
		/// 是否允许候补
		/// </summary>
		[JsonProperty("houbu_train_flag")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool AllowBackup { get; set; }
		public bool IsSmartD { get; set; }
		public bool IsFuXing { get; set; }
		public bool IsQuiet { get; set; }
	}

}
