using System;
using System.Collections.Generic;
using System.Linq;

using TOBA.Entity;

namespace TOBA.Profile
{
	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	/// <summary>
	/// 用户配置
	/// </summary>
	internal class UserConfiguration
	{
		private SortedSet<UserStationHitCount> _userStationFrequencyFrom;
		private SortedSet<UserStationHitCount> _userStationFrequencyTo;
		string _path;


		public UserConfiguration()
		{
		}

		/// <summary>
		/// 用户的车站序列
		/// </summary>
		[JsonProperty("usff")]
		public SortedSet<UserStationHitCount> UserStationFrequencyFrom
		{
			get { return _userStationFrequencyFrom ?? (_userStationFrequencyFrom = new SortedSet<UserStationHitCount>()); }
			set { _userStationFrequencyFrom = value; }
		}

		/// <summary>
		/// 用户的车站序列
		/// </summary>
		[JsonProperty("usft")]
		public SortedSet<UserStationHitCount> UserStationFrequencyTo
		{
			get { return _userStationFrequencyTo ?? (_userStationFrequencyTo = new SortedSet<UserStationHitCount>()); }
			set { _userStationFrequencyTo = value; }
		}

		/// <summary>
		/// 增加站点查询次数
		/// </summary>
		/// <param name="code"></param>
		/// <param name="num"></param>
		public void IncreaseStationQueryFromCount(string code, int num = 1)
		{
			var t = UserStationFrequencyFrom.FirstOrDefault(s => s.Code == code);
			if (t == null) t = new UserStationHitCount() { Code = code };
			else UserStationFrequencyFrom.Remove(t);
			t.HitCount++;
			UserStationFrequencyFrom.Add(t);

			if (UserStationFrequencyFrom.Count > 20)
			{
				UserStationFrequencyFrom.Remove(UserStationFrequencyFrom.Last());
			}
		}

		/// <summary>
		/// 增加站点查询次数
		/// </summary>
		/// <param name="code"></param>
		/// <param name="num"></param>
		public void IncreaseStationQueryToCount(string code, int num = 1)
		{
			var t = UserStationFrequencyTo.FirstOrDefault(s => s.Code == code);
			if (t == null) t = new UserStationHitCount() { Code = code };
			else UserStationFrequencyTo.Remove(t);
			t.HitCount++;
			UserStationFrequencyTo.Add(t);

			if (UserStationFrequencyTo.Count > 20)
			{
				UserStationFrequencyTo.Remove(UserStationFrequencyTo.Last());
			}
		}

		/// <summary>
		/// 保存配置
		/// </summary>
		public void Save()
		{
			if (string.IsNullOrEmpty(_path))
				return;

			//FIX 2014-8-11：加锁，防止多线程时资源争用
			lock (_path)
			{
				if (!string.IsNullOrEmpty(_path))
					this.SaveToFile(_path);
			}
		}

		/// <summary>
		/// 创建用户配置
		/// </summary>
		/// <param name="path"></param>
		/// <param name="shadowMode">是否是影子模式</param>
		/// <returns></returns>
		internal static UserConfiguration Create(string path, bool shadowMode = false)
		{
			UserConfiguration result = null;
			if (!string.IsNullOrEmpty(path) && System.IO.File.Exists(path))
			{
				try
				{
					result = Newtonsoft.Json.JsonConvert.DeserializeObject<UserConfiguration>(System.IO.File.ReadAllText(path, System.Text.Encoding.UTF8));
				}
				catch (Exception)
				{
				}
			}
			if (shadowMode)
				path = null;
			if (result == null)
			{
				//配置文件损坏
				result = new UserConfiguration { _path = path };
			}
			else
				result._path = path;

			return result;
		}
	}
}
