namespace TOBA.Query.Entity
{
	using Data;

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	using TOBA.Configuration;
	using TOBA.Entity;

	/// <summary>
	/// 查询到的车次列表
	/// </summary>
	internal class QueryResult : List<QueryResultItem>
	{
		/// <inheritdoc />
		public QueryResult(DateTime date, string fromCode, string fromName, string toCode, string toName, int capacity = 0) : base(capacity)
		{
			Date = date;
			FromCode = fromCode;
			FromName = fromName;
			ToCode = toCode;
			ToName = toName;
		}

		/// <summary>
		/// 获得或设置关联的查询
		/// </summary>
		public QueryParam Query { get; set; }

		/// <summary>
		/// 获得过滤前的原始数据
		/// </summary>
		[Obfuscation(Exclude = false, Feature = "-rename")]
		public QueryResultItem[] OriginalList { get; private set; }

		public DateTime Date { get; }

		public string FromCode { get; private set; }

		public string FromName { get; set; }

		public string ToCode { get; private set; }

		public string ToName { get; private set; }

		/// <summary>
		/// 按查询过滤
		/// </summary>
		/// <param name="p"></param>
		public void Filter(QueryParam p)
		{
			OriginalList = this.ToArray();

			lock (p.SelectedTrainClass)
			{
				PreFilterByTrainType(p.SelectedTrainClass);
			}
			FilterByTime(true, p.DepartureTimeFrom, p.DepartureTimeTo);
			FilterByTime(false, p.ArriveTimeFrom, p.ArriveTimeTo);
			//始发终到？
			if (p.PassType != null && p.PassType.Value > 0)
			{
				if (p.PassType.Value == 1)
				{
					Remove(this.Where(s => !s.FromStation.IsFirst || !s.ToStation.IsEnd));
				}
				else if (p.PassType.Value == 2)
				{
					Remove(this.Where(s => !s.FromStation.IsFirst));
				}
				else if (p.PassType.Value == 3)
				{
					Remove(this.Where(s => !s.ToStation.IsEnd));
				}
				else if (p.PassType.Value == 4)
				{
					Remove(this.Where(s => !s.FromStation.IsFirst || s.ToStation.IsEnd));
				}
				else if (p.PassType.Value == 5)
				{
					Remove(this.Where(s => s.FromStation.IsFirst || !s.ToStation.IsEnd));
				}
				else if (p.PassType.Value == 6)
				{
					Remove(this.Where(s => s.FromStation.IsFirst || s.ToStation.IsEnd));
				}
			}

			//过滤不可定车次
			if (p.HideNoTicket)
				FilterNoTicket(p);
			//过滤发站
			if (p.HideFromNotSame)
				FilterFromStationNotSame(p.FromStationName);
			if (p.HideToNotSame)
				FilterToStationNotSame(p.ToStationName);
			//检测预定状态
			CheckTicketStatus(p);
			if (p.HideNoNeedTicket)
				FilterNoNeedTicket();
			//标记状态
			MarkSelectedStatus(p);
			//自动预定过滤？
			if (p.EnableAutoPreSubmit && p.AutoPreSubmitConfig != null && p.AutoPreSubmitConfig.HideOtherTrains && p.AutoPreSubmitConfig.TrainList.Count > 0)
			{
				Remove(this.Where(s => s.Selected == 0).ToArray());
			}
		}

		/// <summary>
		/// 标记车次和席别的选择状态
		/// </summary>
		void MarkSelectedStatus(QueryParam p)
		{
			if (p == null || !p.EnableAutoPreSubmit || p.AutoPreSubmitConfig == null)
				return;

			var cfg = p.AutoPreSubmitConfig;
			if (cfg.TrainReg.Count > 0)
			{
				foreach (var t in this)
				{
					t.Selected = cfg.TrainReg.FindIndex(s => s.IsMatch(t.Code)) + 1;
				}
			}

			if (cfg.SeatList.Count > 0)
			{
				foreach (var t in this)
				{
					t.SeatSelected = cfg.SeatList.FindIndex(s => t.TicketCount.GetTicketData(s) != null) + 1;
				}
			}
			if (p.TrainIdList != null)
			{
				var idlist = p.TrainIdList;
				foreach (var id in idlist)
				{
					var train = this.FirstOrDefault(s => s.Id == id);
					if (train != null)
					{
						if (train.Selected == 0)
						{
							//没有在选择列表，则添加到新的列表中
							cfg.TrainList.Add(train.Code);
						}
						train.Selected++;
					}
				}
				p.TrainIdList = null;
			}
		}

		/// <summary>
		/// 过滤不需要的车票
		/// </summary>
		public void FilterNoNeedTicket()
		{
			Remove(this.Where(s => s.NoTicketNeeded));
		}

		/// <summary>
		/// 检测车票状态
		/// </summary>
		/// <param name="p"></param>
		public void CheckTicketStatus(QueryParam p)
		{
			foreach (var item in this)
			{
				//if (!item.IsAvailable) continue;    //不可预订

				//检测每个席别
				item.TicketCount.ForEach(s =>
										{
											var map = ParamData.GetSeatCompatibleMap(s.Key);
											s.Value.NotNeed = p.SelectedSeatClass.Count > 0 && !p.SelectedSeatClass.Contains(s.Key) && (map == null || !map.Any(p.SelectedSeatClass.Contains));
										});
				item.NoTicketNeeded = item.TicketCount.Values.All(s => s.NotNeed);
			}
		}

		/// <summary>
		/// 按时间进行过滤
		/// </summary>
		/// <param name="isDepart"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		public void FilterByTime(bool isDepart, int from, int to)
		{
			Remove(this.Where(s => (isDepart && (s.FromStation.DepartureTime.Value.Hour < from || s.FromStation.DepartureTime.Value.Hour > to)) || (!isDepart && (s.ToStation.ArriveTime.Value.Hour < from || s.ToStation.ArriveTime.Value.Hour > to))));
		}

		/// <summary>
		/// 过滤发站不同车次
		/// </summary>
		/// <param name="name"></param>
		public void FilterFromStationNotSame(string name)
		{
			Remove(this.Where(s => string.Compare(name, s.FromStation.StationName, true) != 0));
		}

		/// <summary>
		/// 过滤发站不同车次
		/// </summary>
		/// <param name="name"></param>
		public void FilterToStationNotSame(string name)
		{
			Remove(this.Where(s => string.Compare(name, s.ToStation.StationName, true) != 0));
		}

		/// <summary>
		/// 隐藏无票车次
		/// </summary>
		public void FilterNoTicket(QueryParam p)
		{
			Remove(this.Where(s => !s.IsAvailable && s.BeginSellTime == null));

			if (p.SelectedSeatClass.Count > 0)
			{
				Remove(this.Where(s =>
				{
					return p.SelectedSeatClass.All(x =>
					{
						var ticket = s.TicketCount.GetTicketData(x);
						return ticket == null || ticket.NoTicket || ticket.NotAvailable;
					});
				}));
			}
		}

		/// <summary>
		/// 移除不符合要求的车次类型
		/// </summary>
		/// <param name="selectedTrains"></param>
		public void PreFilterByTrainType(EventHashSet<char> selectedTrains)
		{
			if (selectedTrains.Count == 0)
				return;

			Remove(this.Where(s =>
			{
				var code = s.TrainClass;
				var charCode = s.Code[0];

				//如果不适用临客过滤方式，则还原为车次类型
				if (QueryConfiguration.Current.IgnoreTempTrainClass && (charCode == 'C' || charCode == 'G' || charCode == 'D' || charCode == 'K' || charCode == 'T' || charCode == 'Z'))
					code = charCode;

                if (s.IsFuXing)
                    return !selectedTrains.Contains('F') && !selectedTrains.Contains('G');
                if (s.IsSmartD)
                    return !selectedTrains.Contains('X') && !selectedTrains.Contains('D');
				if (code == 'G' || code == 'D' || code == 'C' || code == 'L' || code == 'Z' || code == 'K' || code == 'T') return !selectedTrains.Contains(code);
				if (char.IsDigit(code)) return !selectedTrains.Contains('P');

				return !selectedTrains.Contains('*');
			}));
		}

		/// <summary>
		/// 移除指定的队列
		/// </summary>
		/// <param name="src"></param>
		void Remove(IEnumerable<QueryResultItem> src)
		{
			src.ToArray().ForEach(s => Remove(s));
		}
	}
}
