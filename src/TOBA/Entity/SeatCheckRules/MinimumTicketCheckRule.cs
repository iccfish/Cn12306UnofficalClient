using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Entity.SeatCheckRules
{
	using Newtonsoft.Json;

	using TOBA.Query.Entity;

	internal class MinimumTicketCheckRule : ISeatCheckRule
	{
		/// <summary>
		/// 获得或设置判断依据——是小于指定的数还是大于指定的数
		/// </summary>
		[JsonProperty("ilt")]
		public bool IsLessThan { get; set; } = true;

		/// <summary>
		/// 指定判断的依据
		/// </summary>
		[JsonProperty("ct")]
		public int Count { get; set; } = 10;

		/// <summary>
		/// 判断指定的选项是否符合规则。
		/// </summary>
		/// <param name="query">当前的查询</param>
		/// <param name="result">当前的结果</param>
		/// <param name="currentItem">当前的车次</param>
		/// <param name="ticket">当前的车票</param>
		/// <returns>
		/// 如果返回 <see langword="true" />，则说明此记录符合结果，将会继续处理；否则则会跳过
		/// </returns>
		public bool IsMatch(QueryParam query, QueryResult result, QueryResultItem currentItem, LeftTicketData ticket)
		{
			return ((ticket.Count >= Count) ^ IsLessThan);
		} 

		/// <summary>
		/// 获得界面描述
		/// </summary>
		/// <returns></returns>
		public string GetDescription()
		{
			return $"当票数{(IsLessThan ? "少于" : "不少于")}{Count}张时，将会提交此席别；否则此席别将会被忽略";
		}
	}
}
