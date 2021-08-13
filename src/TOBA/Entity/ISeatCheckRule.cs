using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Entity
{
	using TOBA.Query.Entity;

	/// <summary>
	/// 自动预定时，席别选择规则
	/// </summary>
	internal interface ISeatCheckRule
	{
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
		bool IsMatch(QueryParam query, QueryResult result, QueryResultItem currentItem, LeftTicketData ticket);

		/// <summary>
		/// 获得界面描述
		/// </summary>
		/// <returns></returns>
		string GetDescription();
	}
}
