using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Query.Entity
{
	using TOBA.Entity;
	internal class StartStationSellInfo : Dto
	{
		/// <summary>
		/// 始发站名称
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 始发站代码
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// 始发站的起售时间
		/// </summary>
		public DateTime SellTime { get; set; }

		/// <summary>
		/// 是否已经起售了
		/// </summary>
		public bool IsInSell { get; set; }

		/// <summary>
		/// 是不是始发站的起售时间要比当前车站早
		/// </summary>
		public bool IsEarly { get; set; }
	}
}
