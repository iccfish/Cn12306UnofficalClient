namespace TOBA.Workers
{
	using Data;

	using Newtonsoft.Json;

	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using System.Text.RegularExpressions;

	internal class RemoteResourceManager
	{
		static string _cacheFileTrainStation = "trainStation.js";
		static string _cacheSellTime = "selltime.js";
		static string _cacheFileQs = "qs.js";

		/// <summary>
		/// 将列表翻译为对象
		/// </summary>
		/// <param name="content"></param>
		/// <returns></returns>
		public static List<Entity.TrainStation> ParseCityName(string content)
		{
			var matches = System.Text.RegularExpressions.Regex.Matches(content, @"@([^\|]+)\|([^\|]+)\|([^\|]+)\|([^\|]+)\|([^\|]+)\|([^@|]+)");
			var list = matches.Cast<Match>().Select(s => new Entity.TrainStation()
			{
				FirstLetter = s.Groups[1].Value,
				Name = s.Groups[2].Value,
				Code = s.Groups[3].Value,
				Py = s.Groups[4].Value,
				ShortName = s.Groups[5].Value,
				Order = s.Groups[6].Value.ToInt32()
			}).ToList();
			if (list.Count == 0)
				throw new Exception("无法加载车站信息");

			return list;
		}

		/// <summary>
		/// 保存车站信息到缓存中
		/// </summary>
		public static void SaveTrainStationList()
		{
			File.WriteAllText(Profile.Root.GetCacheFile(_cacheFileTrainStation), JsonConvert.SerializeObject(ParamData.TrainStationList));
		}

		/// <summary>
		/// 保存起售信息到缓存中
		/// </summary>
		public static void SaveSellTimeMap()
		{
			File.WriteAllText(Profile.Root.GetCacheFile(_cacheSellTime), JsonConvert.SerializeObject(ParamData.SellTimeMap));
		}

		/// <summary>
		/// 从缓存中加载车站信息
		/// </summary>
		public static bool LoadTrainStationListFromCache()
		{
			var path = Profile.Root.GetCacheFile(_cacheFileTrainStation);
			if (System.IO.File.Exists(path))
			{
				try
				{
					ParamData.TrainStationList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Entity.TrainStation>>(System.IO.File.ReadAllText(path));
				}
				catch (Exception)
				{
					return false;
				}

				return ParamData.TrainStationList?.Count > 0;
			}
			return false;
		}

		/// <summary>
		/// 从缓存中加载起售时间信息
		/// </summary>
		public static bool LoadSellTimeFromCache()
		{
			var path = Profile.Root.GetCacheFile(_cacheSellTime);
			if (System.IO.File.Exists(path))
			{
				try
				{
					ParamData.SellTimeMap = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(System.IO.File.ReadAllText(path));
				}
				catch (Exception)
				{
					return false;
				}
				return ParamData.SellTimeMap != null && ParamData.SellTimeMap.Count > 0;
			}
			return false;
		}
	}
}
