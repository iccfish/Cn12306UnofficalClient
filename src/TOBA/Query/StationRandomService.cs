using System;
using System.Collections.Generic;
using System.Linq;

namespace TOBA.Query
{
	using Data;

	using Entity;

	using System.Diagnostics;

	using TOBA.Entity;

	class StationRandomService
	{
		public QueryParam Query { get; set; }

		string _fromStation, _toStation;
		private List<string> _altFromList;
		List<string> _altToList;
		Random _random;

		public StationRandomService()
		{

		}

		public void BeforeQuery(ref string fromCode, ref string fromName, ref string toCode, ref string toName)
		{
			if (!Query.EnableSameCityStationLoop)
				return;

			if (Query.QueryCount <= 1)
			{
				//第一次查询，只做匹配操作
				if (_fromStation != Query.FromStationCode)
				{
					_fromStation = Query.FromStationCode;
					_altFromList = QueryStationManager.Instance[_fromStation]?.ToList();
				}
				if (_toStation != Query.ToStationCode)
				{
					_toStation = Query.ToStationCode;
					_altToList = QueryStationManager.Instance[_toStation]?.ToList();
				}
			}
			else if (Query.EnableSameCityStationLoop)
			{
				//虚拟化查询
				if (_altFromList?.Count > 1)
				{
					fromCode = _altFromList.RandomTake(_random ??= new Random());
					fromName = ParamData.TrainStationMap.GetValue(fromCode)?.Name ?? "";
				}
				if (_altToList?.Count > 1)
				{
					toCode = _altToList.RandomTake(_random ??= new Random());
					toName = ParamData.TrainStationMap.GetValue(toCode)?.Name ?? "";
				}
			}
		}

		private Dictionary<string, (HashSet<string> from, HashSet<string> to)> _resultCapture;

		public void ValidateResponse(QueryResult result)
		{
			if (!Query.EnableSameCityStationLoop)
				return;

			if (Query.QueryCount <= 1 || _resultCapture == null)
			{
				if (!QueryStationManager.Instance.IsRouteKeyUpdated(result.FromCode, result.ToCode, result.Date))
				{
					QueryStationManager.Instance.Update(Query, result);
				}

				//第一次查询，做更新操作
				var fromStations = result.OriginalList.Select(s => s.FromStation.Code).Distinct().ToArray();
				var toStations = result.OriginalList.Select(s => s.ToStation.Code).Distinct().ToArray();

				if (_altFromList == null)
					_altFromList = fromStations.ToList();
				else
					_altFromList.AddRange(fromStations.Except(_altFromList));
				if (_altToList == null)
					_altToList = toStations.ToList();
				else
					_altToList.AddRange(toStations.Except(_altFromList));

				_resultCapture = result.OriginalList.GroupBy(s => s.Id).ToDictionary(s => s.Key, s => (s.Select(x => x.FromStation.Code).MapToHashSet(), s.Select(x => x.ToStation.Code).MapToHashSet()));
				Query.EnableSameCityStationLoop = _altFromList?.Count > 0 || _altToList?.Count > 0;
			}
			else if (_resultCapture != null)
			{
				//检测并发冲突：如果包含的车次和原始结果不匹配，则排除
				foreach (var item in result)
				{
					var checkRow = _resultCapture.GetValue(item.Id);
					if (checkRow.from?.Contains(item.FromStation.Code) == false)
					{
						//标记为不可用
						item.IsAvailable = false;

						if (_altFromList?.Contains(item.FromStation.Code) == true)
						{
							Debug.WriteLine("检测到冲突的同站轮询。已清理 {0}，当前站 {1}", item.FromStation.Code, _fromStation);
							_altFromList.Remove(item.FromStation.Code);
						}
					}
					if (checkRow.to?.Contains(item.ToStation.Code) == false)
					{
						//标记为不可用
						item.IsAvailable = false;

						if (_altToList?.Contains(item.ToStation.Code) == true)
						{
							Debug.WriteLine("检测到冲突的同站轮询。已清理 {0}，当前站 {1}", item.ToStation.Code, _toStation);
							_altToList.Remove(item.ToStation.Code);
						}
					}
				}

				Query.EnableSameCityStationLoop = _altFromList?.Count > 0 || _altToList?.Count > 0;
			}
		}
	}
}
