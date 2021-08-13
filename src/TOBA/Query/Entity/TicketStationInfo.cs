namespace TOBA.Query.Entity
{
	using System;
	using System.Linq;

	using TOBA.Entity;

	/// <summary>
	/// 查票结果中的车站信息
	/// </summary>
	internal class TicketStationInfo : Dto
	{
		private string _stationName;

		/// <summary>
		/// 站点名
		/// </summary>
		public string StationName
		{
			get => _stationName ?? "";
			set => _stationName = value;
		}

		/// <summary>
		/// 发车时间
		/// </summary>
		public DateTime? DepartureTime { get; set; }

		/// <summary>
		/// 到达时间
		/// </summary>
		public DateTime? ArriveTime { get; set; }

		/// <summary>
		/// 获得或设置车站编码
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// 获得或设置是否是始发站
		/// </summary>
		public bool IsFirst { get; set; }

		/// <summary>
		/// 获得或设置是否是终点站
		/// </summary>
		public bool IsEnd { get; set; }
	}
}
