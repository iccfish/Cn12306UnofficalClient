using System;
using System.Collections.Generic;
using System.Linq;

using TOBA.WebLib;

namespace TOBA.Workers
{
	using Data;

	internal class SameCityStationResolveWorker
	{
		#region 单例模式

		static SameCityStationResolveWorker _instance;
		static readonly object _lockObject = new object();

		public static SameCityStationResolveWorker Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new SameCityStationResolveWorker();
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		NetClient _client;
		Dictionary<string, HashSet<string>> _cache;

		private SameCityStationResolveWorker()
		{
			_client = new NetClient();
			_cache = new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase);
		}

		public HashSet<string> Resolve(string code)
		{
			code = code.ToUpper();

			var set = _cache.GetValue(code);

			if (set == null)
			{
				var slist = ParamData.FindSimilarStation(code);
				if (slist == null)
					set = new HashSet<string>() { code };
				else
					set = slist.Select(s => s.Code).MapToHashSet();

				foreach (var s in set)
				{
					_cache.AddOrUpdate(s, set);
				}
			}

			return set;
		}
	}
}
