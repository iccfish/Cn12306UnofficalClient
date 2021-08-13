using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Configuration
{
	using TOBA.Query.Entity;

	internal class TrainIdAliasStorage
	{
		#region 单例模式

		static TrainIdAliasStorage _instance;
		static readonly object _lockObject = new object();

		public static TrainIdAliasStorage Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new TrainIdAliasStorage();
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		/// <summary>
		/// 车次ID-别名映射表
		/// </summary>
		public Dictionary<string, HashSet<string>> AliasMap { get; } = new Dictionary<string, HashSet<string>>();

		/// <summary>
		/// 从结果中更新映射缓存
		/// </summary>
		/// <param name="result"></param>
		public void Update(QueryResultItem[] result)
		{
			lock (AliasMap)
			{
				foreach (var item in result)
				{
					var hs = AliasMap.GetValue(item.Id, s => new HashSet<string>());
					hs.SafeAdd(item.Code);
				}
			}
		}
	}
}
