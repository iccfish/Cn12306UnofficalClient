using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.BackupOrder.Entity
{
	using System.CodeDom;

	using Newtonsoft.Json;

	using TOBA.Entity;

	using WebLib;

	class BackupOrderItem
	{
		/// <summary>
		/// 00 排队中
		/// 01 待支付
		/// 04 待兑现
		/// 05 status_name=已退单
		/// 06 status_name=已退单（自动退单）
		/// 07 兑现成功
		/// 08
		/// </summary>
		[JsonProperty("status_code")]
		public int StatusCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("realize_alltype_num")]
		public string RealizeAlltypeNum { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("total_page")]
		public int TotalPage { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("sequence_no")]
		public string SequenceNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("reserve_no")]
		public string ReserveNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("ticket_num")]
		public string TicketNum { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("prepay_amount")]
		public double PrepayAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("needs")]
		public List<BackupTrain> Needs { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("relation_no")]
		public string RelationNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("trace_id")]
		public string TraceId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("num_in_page")]
		public string NumInPage { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("reserve_time")]
		public DateTime ReserveTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("realize_limit_time")]
		public DateTime RealizeLimitTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("refund_trade_no")]
		public string RefundTradeNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("current_page")]
		public string CurrentPage { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("realize_tmp_train")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool RealizeTmpTrain { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("passengers")]
		public List<PassengerInTicket> Passengers { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("accept_tmp_train")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool AcceptTmpTrain { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("accept_tmp_train_name")]
		public string AcceptTmpTrainName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("status_name")]
		public string StatusName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("ticket_price")]
		public string TicketPrice { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("tmp_train_flag")]
		public string TmpTrainFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("refund_diff_flag")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool RefundDiffFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("batch_no")]
		public int BatchNo { get; set; }

		[JsonProperty("day_difference")]
		public int DayDifference { get; set; }

		/// <summary>
		/// 退款信息
		/// </summary>
		public RefundInfo RefundInfo { get; set; }

		/// <summary>
		/// 是否有退款信息
		/// </summary>
		public bool HasRefundInfo =>
			(StatusCode == 5 || StatusCode == 6 || StatusCode == 8 || (StatusCode == 7 && RefundDiffFlag))
			&& !RefundTradeNo.IsNullOrEmpty();
	}
}
