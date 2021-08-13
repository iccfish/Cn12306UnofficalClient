namespace TOBA.BackupOrder.Entity
{
	using System;

	using Newtonsoft.Json;

	using WebLib;

	/// <summary>
	/// 未完成候补订单信息
	/// </summary>
	class UnpayBackupOrder
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("flag")]
		public bool Flag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("order")]
		public BackupOrderItem Order { get; set; }

		/// <summary>
		/// -1 排队失败
		/// 1 排队成功
		/// 0 排队中
		/// 2 取消排队/排队查询失败
		/// </summary>
		[JsonProperty("status")]
		public int Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isAsync")]
		public bool IsAsync { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("beginTime")]
		[JsonConverter(typeof(JsonTick2DateTimeConverter))]
		public DateTime BeginTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonConverter(typeof(JsonTick2DateTimeConverter))]
		[JsonProperty("loseTime")]
		public DateTime LoseTime { get; set; }

	}
}