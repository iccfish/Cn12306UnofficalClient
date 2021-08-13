using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Entity
{
	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	internal class UserStationHitCount : IComparable<UserStationHitCount>
	{
		/// <summary>
		/// 获得或设置查询次数
		/// </summary>
		[JsonProperty("h")]
		public int HitCount { get; set; }

		/// <summary>
		/// 获得代码
		/// </summary>
		[JsonProperty("c")]
		public string Code { get; set; }

		#region Implementation of IComparable<in UserStationHitCount>

		public int CompareTo(UserStationHitCount other)
		{
			if (other == null) return 1;
			return HitCount - other.HitCount;
		}

		#endregion
	}
}
