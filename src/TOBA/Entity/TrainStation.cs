using System;
using Newtonsoft.Json;

namespace TOBA.Entity
{
	internal class TrainStation : BasicTrainStation, IComparable<TrainStation>
	{
		public string FirstLetter { get; set; }

		public string Py { get; set; }

		public string ShortName { get; set; }

		public int Order { get; set; }

		/// <summary>
		/// 测试是否匹配
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public bool IsMatch(string text)
		{
			if (text.IsNullOrEmpty() || Name.IsNullOrEmpty()) return false;

			SortOrder = -1;
			int? temp;

			if ((temp = IsSubstringFound(Name, text)) != null) SortOrder = temp.Value * 5;
			else if ((temp = IsSubstringFound(ShortName, text)) != null) SortOrder = temp.Value * 5 + 1;
			else if ((temp = IsSubstringFound(Py, text)) != null) SortOrder = temp.Value * 5 + 2;
			else if ((temp = IsSubstringFound(FirstLetter, text)) != null) SortOrder = temp.Value * 5 + 3;
			else if ((temp = IsSubstringFound(Code, text)) != null) SortOrder = temp.Value * 5 + 4;

			return SortOrder != -1;

		}

		int? IsSubstringFound(string src, string sub)
		{
			var idx = src.IndexOf(sub, StringComparison.OrdinalIgnoreCase);
			return idx == -1 ? (int?)null : idx != 0 ? 2 : src.Length == sub.Length ? 0 : 1;
		}

		#region IComparable<TrainStation> 成员

		public int CompareTo(TrainStation other)
		{
			if (Order == other.Order) return string.Compare(Name, other.Name, true);
			return Order - other.Order;
		}

		#endregion

		private static TrainStation _emptyStation;
		/// <summary>
		/// 获得车站分割线
		/// </summary>
		public static TrainStation EmptyStation
		{
			get { return _emptyStation ?? (_emptyStation = new TrainStation()); }
		}

		public override string ToString()
		{
			return Name.DefaultForEmpty("-----------");
		}

		/// <summary>
		/// 提供在一次查询中的排序数值
		/// </summary>
		[JsonIgnore]
		public int SortOrder { get; private set; }
	}
}
