namespace TOBA.BackupOrder.Entity
{
	using System;

	using Newtonsoft.Json;

	using WebLib;

	class BackupTrain
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("status_code")]
		public int StatusCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("to_tele_code")]
		public string ToTeleCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("board_train_code")]
		public string BoardTrainCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("from_station_name")]
		public string FromStationName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("show_fail_info")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool ShowFailInfo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("to_station_name")]
		public string ToStationName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("realize_tmp_train")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool RealizeTmpTrain { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("realize_fail_info")]
		public string RealizeFailInfo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("arrive_time")]
		public TimeSpan ArriveTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("from_tele_code")]
		public string FromTeleCode { get; set; }

		/// <summary>
		/// 0 - 已退单
		/// 1 - 兑现成功
		/// 3 - 已失效（兑现成功外的）
		/// 2 - 兑现失败
		/// </summary>
		[JsonProperty("batch_status")]
		public int BatchStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("train_date")]
		public DateTime TrainDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("arrive_date")]
		public DateTime ArriveDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("status_name")]
		public string StatusName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("start_tele_code")]
		public string StartTeleCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("tmp_train_flag")]
		public string TmpTrainFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("start_time")]
		public TimeSpan StartTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("batch_ticket_price")]
		public double BatchTicketPrice { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("seat_name")]
		public string SeatName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("end_tele_code")]
		public string EndTeleCode { get; set; }


		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("batch_no")]
		public int BatchNo { get; set; }


	}
}