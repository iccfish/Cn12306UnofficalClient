using System;
using System.Collections.Generic;
using System.Linq;

namespace TOBA.Entity
{
	using Data;

	using Query;
	using Query.Entity;

	partial class QueryParam
	{

		/// <summary>
		/// 返回按照要求进行过滤的结果
		/// </summary>
		/// <param name="trainList"></param>
		/// <returns></returns>
		public AutoSelectResult FindMatchedTrain(IEnumerable<QueryResultItem> trainList, bool forceAutoSelect = false)
		{
			return FindMatchedTrain(trainList, out _, forceAutoSelect);
		}

		/// <summary>
		/// 返回按照要求进行过滤的结果
		/// </summary>
		/// <param name="trainList"></param>
		/// <returns></returns>
		public AutoSelectResult FindMatchedTrain(IEnumerable<QueryResultItem> trainList, out AutoSelectStat stat, bool forceAutoSelect = false)
		{
			stat = null;

			if (trainList?.Any() != true)
				return null;

			var cfg = AutoPreSubmitConfig;
			stat = new AutoSelectStat();

			//过滤可用的车次以及secureStr已经被禁用的车次
			IEnumerable<QueryResultItem> trainQuery = trainList;

			//如果已选择车次，则过滤没有选择的车次并进行排序
			if (!forceAutoSelect && EnableAutoPreSubmit)
			{
				if (AutoPreSubmitConfig.TrainReg.Count > 0)
				{
					trainQuery = trainQuery.Select(s =>
					{
						return new KeyValuePair<int, QueryResultItem>(
							AutoPreSubmitConfig.TrainReg.FindIndex(x => x.IsMatch(s.Code)),
							s
							);
					}).Where(s => s.Key >= 0).OrderBy(s => s.Key).Select(s => s.Value).ToArray();
				}

				var flag1 = trainQuery.Any();   //是否有车次
												//如果已选择席别，则忽略没有指定席别的车次
				if (AutoPreSubmitConfig.SeatList.Count > 0)
				{
					trainQuery = trainQuery.Where(s => AutoPreSubmitConfig.SeatListForSelect.Any(x => s.TicketCount.ContainsKey(x))).ToArray();
				}

				var flag2 = trainQuery.Any();

				if (flag1 && !flag2)
				{
					stat.IsTrainSeatMismatch = true;
				}
			}

			//过滤不可订票以及车票黑数据
			trainQuery = trainQuery.Where(s => s.IsAvailable && !IsTrainTicketDataDisabled(s));

			//过滤可能无效的车次
			if (IgnoreIllegalData)
			{
				trainQuery = trainQuery.Where(s => !s.AlmostIllegalResult);
			}
			var trains = trainQuery.ToArray();
			var pasCount = cfg.Passenger.Count;
			var selectedSeat = SelectedSeatClass?.Select(ParamData.GetSeatCompatibleMap).ExceptNull().SelectMany(s => s).Distinct().ToArray();
			var seatOrder = AutoPreSubmitConfig.SeatList.Count == 0 ? Configuration.SubmitOrder.Current.DefaultSeatPreferOrder.Where(s => (selectedSeat?.Length ?? 0) == 0 || selectedSeat.Contains(s)).ToArray() : AutoPreSubmitConfig.SeatListForSelect.Where(s => (selectedSeat?.Length ?? 0) == 0 || selectedSeat.Contains(s)).ToArray();
			Dictionary<bool, Tuple<QueryResultItem, LeftTicketData, int, bool>[]>[] data;

			if (cfg.SeatFirst)
			{
				//席别优先，先过滤所有车次并生成候选列表
				//按照席别顺序找，每个席别里优先找车票最多的
				data = seatOrder.Select(seat =>
				{
					var rules = AutoPreSubmitConfig.SeatCheckRules.GetValue(seat);
					var tTrains = trains.Select(x =>
					{
						var ticket = x.TicketCount.GetTicketData(seat, false);
						if (ticket == null || ticket.NotAvailable || ticket.NotSell || ticket.NoTicket)
							return null;

						var count = ticket.TicketForCompute;
						if (count < pasCount && !AutoPreSubmitConfig.EnablePartialSubmit)
							return null;
						//规则限制
						if (rules?.Any(yx => !yx.IsMatch(this, null, x, ticket)) == true)
							return null;

						return Tuple.Create(x, ticket, ticket.TicketForCompute, pasCount <= ticket.TicketForCompute);
					}).ExceptNull().GroupBy(x => x.Item4).ToDictionary(x => x.Key, x => cfg.AutoSelectTrain ? x.OrderByDescending(y => y.Item3).ToArray() : x.ToArray());

					return tTrains;
				}).ToArray();
			}
			else
			{
				//车次优先。按照trains的顺序查找，每个车次里面选定所有席别，选择优先保证全部提交的席别。
				data = trains.Select(train =>
				{
					return seatOrder.Select(seat =>
					{
						var rules = AutoPreSubmitConfig.SeatCheckRules.GetValue(seat);
						var ticket = train.TicketCount.GetTicketData(seat, false);
						if (ticket == null || ticket.NotAvailable || ticket.NotSell || ticket.NoTicket)
							return null;

						var count = ticket.TicketForCompute;
						if (count < pasCount && !AutoPreSubmitConfig.EnablePartialSubmit)
							return null;
						//规则限制
						if (rules?.Any(yx => !yx.IsMatch(this, null, train, ticket)) == true)
							return null;

						return Tuple.Create(train, ticket, ticket.TicketForCompute, pasCount <= ticket.TicketForCompute);
					}).ExceptNull().GroupBy(x => x.Item4).ToDictionary(x => x.Key, x => cfg.AutoSelectTrain ? x.OrderByDescending(y => y.Item3).ToArray() : x.ToArray()); ;
				}).ToArray();
			}

			{
				//找第一个满足要求的车次
				var tTrain = data.Select(s => s.GetValue(true)?.FirstOrDefault()).ExceptNull().FirstOrDefault()
							?? data.Select(s => s.GetValue(false)?.FirstOrDefault()).ExceptNull().FirstOrDefault();
				if (tTrain != null)
				{
					pasCount = Math.Min(pasCount, tTrain.Item3);
					return new AutoSelectResult(tTrain.Item1, tTrain.Item2.Code, pasCount, AutoPreSubmitConfig.Passenger.Take(pasCount).ToArray());
				}
			}

			return null;
		}

		/// <summary>
		/// 查找下个起售的时间
		/// </summary>
		/// <param name="trains"></param>
		/// <returns></returns>
		public bool FindNextSellTime(IEnumerable<QueryResultItem> trains, out DateTime? dateTime)
		{
			dateTime = null;
			if (trains == null)
			{
				return false;
			}

			dateTime = FindNextSellTime(trains);

			return dateTime != null;
		}

		/// <summary>
		/// 查找下个起售的时间
		/// </summary>
		/// <param name="trains"></param>
		/// <returns></returns>
		public DateTime? FindNextSellTime(IEnumerable<QueryResultItem> trains)
		{
			if (trains == null)
				return null;

			var nowDate = DateTime.Now.Date;
			//查找符合要求的车次
			if (!EnableAutoPreSubmit || !AutoPreSubmitConfig.AllSetOk)
			{
				//如果有任何已经开售的，那么就直接返回
				if (!trains.Any() || trains.Any(s => s.BeginSellTime == null) || !trains.Any(s => s.BeginSellTime != null && s.BeginSellTime.Value.Date == nowDate))
					return null;
				var st = trains.Where(s => s.BeginSellTime != null && s.BeginSellTime.Value.Date == nowDate).Min(s => s.BeginSellTime.Value);
				return nowDate.Add(st.TimeOfDay);
			}

			var ts = trains.Where(s => AutoPreSubmitConfig.TrainReg.Any(x => x.IsMatch(s.Code))).ToArray();

			//如果有任何已经开售的，那么就直接返回
			if (!ts.Any() || ts.Any(s => s.BeginSellTime == null) || !ts.Any(s => s.BeginSellTime != null && s.BeginSellTime.Value.Date == nowDate))
				return null;

			var time = ts.Where(s => s.BeginSellTime != null && s.BeginSellTime.Value.Date == nowDate).Min(s => s.BeginSellTime.Value);
			//修正两个起售时间时，一个时间查询无票时，自动等下一个时间的bug
			if (time <= DateTime.Now)
				return null;

			return nowDate.Add(time.TimeOfDay);
		}


		/// <summary>
		/// 持久化当前查询状态
		/// </summary>
		/// <param name="clear"></param>
		public void PersistentQueryState(bool clear = false)
		{
			if (clear)
			{
				IsLastInQuery = false;
			}
			else
			{
				IsLastInQuery = QueryState == QueryState.Wait || QueryState == QueryState.Query;
			}
		}

		/// <summary>
		/// 根据当前查询结果，获得下一个可能的休息时间
		/// </summary>
		/// <param name="session"></param>
		/// <param name="enableAutoRefresh"></param>
		/// <param name="isError"></param>
		/// <returns></returns>
		public (int sleep, string msg, int msgTime) GetSleepTime(Session session = null, bool enableAutoRefresh = true, bool isError = false)
		{
			var sleep = 0;
			var msg = string.Empty;
			var msgTime = 0;
			var now = DateTime.Now;
			var sp = RunTime.ServerTime;

			if (EnableAutoPreSubmit && enableAutoRefresh)
			{
				var (isOpen, toOpenTime) = ParamData.GetSubmitOrderOpenTime();
				if (!isOpen)
				{
					sleep = toOpenTime;
					msg = "当前不可提交订单，请等待系统开放。";
					msgTime = 30 * 1000;
				}
				else if (AutoPreSubmitConfig.AutoWaitToSell && FindNextSellTime(LastQueryResult, out var time))
				{
					sleep = (int)(time.Value - sp).TotalMilliseconds + (int)(Configuration.QueryConfiguration.Current.AutoDelayAfterOClock * 1000);
					msg = "车票未起售，等待车票起售，等待时间可能会有变化。";
					msgTime = sleep - 10000;
				}
				else if (AutoPreSubmitConfig.EnableOClockRefresh && (sp.Minute >= 59 || sp.Minute == 29))
				{
					sleep = (60 - sp.Second) * 1000;
					msg = "即将等待整点起售，等待时间可能会有变化。";
					msgTime = sleep - 10000;
				}
				else if (Configuration.QueryConfiguration.Current.SpeedingQueryOnOClock != null && (now.Minute == 0 || now.Minute == 30) && now.Second <= 30)
				{
					sleep = Configuration.QueryConfiguration.Current.SpeedingQueryOnOClock.Value;
				}
				else
				{
					//默认时间，最低1秒
					sleep = Configuration.QueryConfiguration.Current.GetRandomQuerySleep(isError);
				}

				if (session != null)
					sleep = session.UserProfile.QueryParams.CheckForSafty(sleep);
			}

			return (sleep, msg, msgTime);
		}
	}
}
