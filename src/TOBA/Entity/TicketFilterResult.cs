using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Entity
{
	/// <summary>
	/// 车票检测状态
	/// </summary>
	public enum TicketFilterResult
	{
		/// <summary>
		/// 么有车票
		/// </summary>
		NoTicket = 0,
		/// <summary>
		/// 无此座位类型
		/// </summary>
		TypeUnavailable = -1,
		/// <summary>
		/// 此席别不需要
		/// </summary>
		SeatNotNeed = 1,
		/// <summary>
		/// 被自动预定过滤
		/// </summary>
		FilterByAutoSubmit = 2,
		/// <summary>
		/// 被黑名单过滤
		/// </summary>
		FilterByBlackList = 3,

		/// <summary>
		/// 有效
		/// </summary>
		Valid = 100
	}
}
