namespace TOBA.Order.Entity
{
	using System;

	using Newtonsoft.Json;

	using TOBA.Entity;

	internal class StationInTicket : Dto
	{
		internal class TrainDTO
		{
			/// <summary>
			/// 
			/// </summary>
			[JsonProperty("train_no")]
			public string TrainNo { get; set; }

			/// <summary>
			/// 
			/// </summary>
			[JsonProperty("train_code")]
			public string TrainCode { get; set; }

			/// <summary>
			/// 
			/// </summary>
			[JsonProperty("start_station_telecode")]
			public string StartStationTelecode { get; set; }

			/// <summary>
			/// 
			/// </summary>
			[JsonProperty("end_station_telecode")]
			public string EndStationTelecode { get; set; }

			/// <summary>
			/// 
			/// </summary>
			[JsonProperty("start_date_str")]
			public string StartDateStr { get; set; }
		}

		//public Traindto trainDTO { get; set; }
		public string station_train_code { get; set; }
		public string from_station_telecode { get; set; }
		public string from_station_name { get; set; }
		public DateTime start_time { get; set; }
		public string to_station_telecode { get; set; }
		public string to_station_name { get; set; }
		public DateTime? arrive_time { get; set; }
		public string distance { get; set; }

		public TrainDTO TrainDto { get; set; }
	}
}