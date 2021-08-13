using System;
using System.Collections.Generic;
using System.Linq;

using TOBA.Query.Entity;

namespace TOBA.Query
{
	using Data;

	using TOBA.Entity;

	class QueryStationManager
	{
		#region 单例模式

		static QueryStationManager _instance;
		static readonly object _lockObject = new object();

		public static QueryStationManager Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new QueryStationManager();
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		List<List<string>> _data;
		Dictionary<string, bool?> _routeStatusDic = new Dictionary<string, bool?>(StringComparer.OrdinalIgnoreCase);
		Random _random = new Random();

		/// <summary>
		/// 创建 <see cref="QueryStationManager" />  的新实例(QueryStationManager)
		/// </summary>
		private QueryStationManager()
		{
			_data = new List<List<string>>();
		}

		/// <summary>
		/// 随机化发到站
		/// </summary>
		/// <param name="fromName"></param>
		/// <param name="time"></param>
		/// <param name="fromCode"></param>
		/// <param name="toName"></param>
		/// <param name="toCode"></param>
		public void RandomStation(DateTime time, ref string fromName, ref string fromCode, ref string toName, ref string toCode)
		{
			if (_routeStatusDic.GetValue(fromCode + toCode) != true) return;


			RandomStation(ref fromName, ref fromCode);
			RandomStation(ref toName, ref toCode);
		}

		/// <summary>
		/// 随机化站点
		/// </summary>
		/// <param name="name"></param>
		/// <param name="code"></param>
		public void RandomStation(ref string name, ref string code)
		{
			var ccode = code;
			var item = _data.FirstOrDefault(s => s.Contains(ccode));
			if (item == null)
				return;

			//随机
			var index = _random.Next(item.Count);
			code = item[index];
			name = ParamData.TrainStationMap.GetValue(code).SelectValue(s => s.Name) ?? "";
		}

		public List<string> this[string code]
		{
			get
			{
				lock (_data)
				{
					return _data.FirstOrDefault(s => s.Contains(code));
				}
			}
		}

		/// <summary>
		/// 将同站记录更新
		/// </summary>
		/// <param name="codes"></param>
		public bool Update(IEnumerable<string> codes)
		{
			if (codes.Count() < 2)
				return false;

			lock (_data)
			{
				var item = _data.FirstOrDefault(s => codes.Any(s.Contains));
				var hasChange = false;
				if (item == null)
				{
					item = new List<string>();
					item.AddRange(codes);
					_data.Add(item);
					hasChange = true;
				}
				else
				{
					codes.Except(item).ForEach(s =>
					{
						hasChange = true;
						item.Add(s);
					});
				}
				return hasChange;
			}
		}

		public bool? IsStationLoopAvailable(string fromCode, string toCode, DateTime date)
		{
			return _routeStatusDic.GetValue(fromCode + toCode);
		}

		/// <summary>
		/// 获得是否已更新跟踪标记
		/// </summary>
		/// <param name="fromCode"></param>
		/// <param name="toCode"></param>
		/// <param name="date"></param>
		/// <returns></returns>
		public bool IsRouteKeyUpdated(string fromCode, string toCode, DateTime date)
		{
			return _routeStatusDic.ContainsKey(fromCode + toCode);
		}


		public void Update(QueryParam param, QueryResult query)
		{
			var key = param.FromStationCode + param.ToStationCode;

			if (_routeStatusDic.ContainsKey(key))
				return;

			var froms = query.Select(s => s.FromStation.Code).Distinct(StringComparer.OrdinalIgnoreCase).MapToHashSet();
			var tos = query.Select(s => s.ToStation.Code).Distinct(StringComparer.OrdinalIgnoreCase).MapToHashSet();

			//是否可用？
			var available = (froms.Count > 1 || tos.Count > 1) && !froms.Contains(param.ToStationCode) && !tos.Contains(param.FromStationCode);
			lock (_routeStatusDic)
			{
				if (_routeStatusDic.ContainsKey(key))
					return;
				_routeStatusDic[key] = available;
			}
		}
	}
}
