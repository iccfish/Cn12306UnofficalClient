using FSLib.Network.Http;

namespace TOBA.WebLib
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Drawing;
	using System.Linq;
	using System.Net;
	using System.Text.RegularExpressions;
	using System.Threading;
	using System.Threading.Tasks;

	using Configuration;

	using Entity;

	using FSLib;
	using FSLib.Extension;

	using Workers;

	internal static class NetworkTaskManager
	{
		/// <summary>
		/// 获得网站的版本
		/// </summary>
		/// <returns></returns>
		public static string GetResourceVersion(out int code, out string err)
		{
			code = 0;
			err = null;

			var client = new NetClient();
			var task = client.RunRequestLoop(_ => client.Create<string>(HttpMethod.Get, NetworkEnvironment.UrlResourceVersion, ""), retryCount: 1);
			if (!task.IsValid() && task.Exception is SystemBusyException)
			{
				//网络繁忙，被封锁IP
				code = -2;
				err = task.GetErrorMsg();

				return null;
			}

			if (task != null && task.IsSuccess)
			{
				var text = task.Result;
				var m = System.Text.RegularExpressions.Regex.Match(text, @"station_name\.js\?station_version=([\d\.]+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
				if (m.Success)
					return m.Groups[1].Value;

			}
			code = -1;
			err = task.GetErrorMsg();

			return null;
		}

		/// <summary>
		/// 获得站点JS的内容
		/// </summary>
		/// <returns></returns>
		public static string GetCityNameJsContent(out string err)
		{
			err = null;

			var client = new NetClient();
			var task = client.RunRequestLoop(_ => client.Create<string>(HttpMethod.Get, NetworkEnvironment.UrlScriptCityName));
			if (task != null && task.IsSuccess)
			{
				return task.Result;
			}

			err = task.GetErrorMsg();
			return null;
		}

		public static Dictionary<string, string> GetSellTimeMap()
		{
			var task = new NetClient().Create(HttpMethod.Post, "https://www.12306.cn/index/otn/index12306/queryAllCacheSaleTime", result: new { data = CollectionUtility.CreateAnymousTypeList(new { station_telecode = "", station_name = "", sale_time = "", start_date = 0 }) }).Send();
			if (!task.IsValid() || task.Result.data == null)
			{
				return null;
			}

			var nowDate = DateTime.Now;
			var current = nowDate.Year * 10000 + nowDate.Month * 100 + nowDate.Day;

			return task.Result.data.Where(s => s.start_date <= current).GroupBy(s => s.station_telecode).ToDictionary(s => s.Key, s => s.First().sale_time);
		}

	}
}
