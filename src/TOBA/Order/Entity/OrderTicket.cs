namespace TOBA.Order.Entity
{
	using Newtonsoft.Json;

	using System;

	using TOBA.Entity;

	using WebLib;

	internal partial class OrderTicket : Dto
	{
		/// <summary>
		/// 获得当前的订单状态
		/// </summary>
		public OrderStatus OrderStatus
		{
			get
			{
				switch (this.ticket_status_code)
				{
					case "a":
						return OrderStatus.Paid;
					case "b": return OrderStatus.TicketPrinted;
					case "c":
						return OrderStatus.Refunded;
					case "d":
						return OrderStatus.Resigned;
					case "f":
						return OrderStatus.ResignTicket;
					case "i": return OrderStatus.NotPay;
					case "j":
						return OrderStatus.ResignNotPaid;
					case "l": return OrderStatus.Leaved;
					case "s": return OrderStatus.ResignChangeTsNotPaid;
					case "q": return OrderStatus.ResignChangeTsIng;
					case "r": return OrderStatus.ResignChagneTsTicket;
					case "p": return OrderStatus.ResignChagneTsed;
				}
				return OrderStatus.Unknown;
			}
		}
	}

	internal partial class OrderTicket
	{
		public StationInTicket stationTrainDTO { get; set; }

		[JsonProperty("passengerDTO")]
		public TOBA.Entity.Web.Passenger Passenger { get; set; }
		public string ticket_no { get; set; }
		public string sequence_no { get; set; }
		public string batch_no { get; set; }
		public DateTime train_date { get; set; }
		public string coach_no { get; set; }
		public string coach_name { get; set; }
		public string seat_no { get; set; }
		public string seat_name { get; set; }
		public string seat_flag { get; set; }
		public string seat_type_code { get; set; }
		public string seat_type_name { get; set; }
		public string ticket_type_code { get; set; }
		public string ticket_type_name { get; set; }
		public DateTime reserve_time { get; set; }
		public DateTime limit_time { get; set; }
		public DateTime lose_time { get; set; }
		public DateTime pay_limit_time { get; set; }
		public float ticket_price { get; set; }
		public string print_eticket_flag { get; set; }
		public string resign_flag { get; set; }
		public string return_flag { get; set; }
		public string confirm_flag { get; set; }
		public string pay_mode_code { get; set; }
		public string ticket_status_code { get; set; }
		public string ticket_status_name { get; set; }
		public string cancel_flag { get; set; }
		public int amount_char { get; set; }
		public string trade_mode { get; set; }
		public DateTime start_train_date_page { get; set; }
		public string str_ticket_price_page { get; set; }
		public string come_go_traveller_ticket_page { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("return_deliver_flag")]
		public string ReturnDeliverFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("deliver_fee_char")]
		public string DeliverFeeChar { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("is_need_alert_flag")]
		public bool IsNeedAlertFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("is_deliver")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool IsDeliver { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("dynamicProp")]
		public string DynamicProp { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("fee_char")]
		public string FeeChar { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("insure_query_no")]
		public string InsureQueryNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("column_nine_msg")]
		public string ColumnNineMsg { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("lc_flag")]
		public string LcFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("integral_pay_flag")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool IntegralPayFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("trms_price_rate")]
		public string TrmsPriceRate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("trms_price_number")]
		public string TrmsPriceNumber { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("trms_service_code")]
		public string TrmsServiceCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("ext_ticket_no")]
		public string ExtTicketNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("sale_mode_type")]
		public string SaleModeType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("if_cash")]
		public string IfCash { get; set; }

		/// <summary>
		/// 候补订单标记，H-候补订单，N-非候补订单
		/// </summary>
		[JsonProperty("alternate_flag")]
		public string AlternateFlag { get; set; }
	}
}