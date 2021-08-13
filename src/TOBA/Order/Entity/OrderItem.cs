namespace TOBA.Order.Entity
{
	using System;

	using Newtonsoft.Json;

	using TOBA.Entity;


	//internal class Ticket
	//{
	//	public string seatTypeName { get; set; }
	//	public string seatTypeCode { get; set; }
	//	public string ticketTypeName { get; set; }
	//	public string passengerName { get; set; }
	//	public string passengerIdTypeName { get; set; }
	//}


	internal partial class OrderItem: Dto
	{
		[JsonProperty("sequence_no")]
		public string SequenceNo { get; set; }
		public DateTime order_date { get; set; }
		public int ticket_totalnum { get; set; }
		public float ticket_price_all { get; set; }
		public string cancel_flag { get; set; }
		public string resign_flag { get; set; }
		public string return_flag { get; set; }
		public string print_eticket_flag { get; set; }
		public string pay_flag { get; set; }
		public string pay_resign_flag { get; set; }
		public string confirm_flag { get; set; }
		public OrderTicket[] tickets { get; set; }
		public string reserve_flag_query { get; set; }
		public string if_show_resigning_info { get; set; }
		public string recordCount { get; set; }
		public string isNeedSendMailAndMsg { get; set; }
		public string[] array_passser_name_page { get; set; }
		public string[] from_station_name_page { get; set; }
		public string[] to_station_name_page { get; set; }
		public string start_train_date_page { get; set; }
		public string start_time_page { get; set; }
		public string arrive_time_page { get; set; }
		public string train_code_page { get; set; }
		public string ticket_total_price_page { get; set; }
		public string come_go_traveller_order_page { get; set; }
		public string canOffLinePay { get; set; }

		[JsonIgnore]
		public int OriginalPage { get; set; }

		[JsonIgnore]
		public string OriginalSubType { get; set; }

		/// <summary>
		/// 获得是否是候补订单
		/// </summary>
		public bool IsBackupOrder => tickets[0].AlternateFlag == "H";
	}
}