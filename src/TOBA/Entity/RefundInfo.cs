using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Entity
{
	using Newtonsoft.Json;

	using Otn.Entity;

	class RefundInfo
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("trade_mode")]
		public int TradeMode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("query_flag")]
		public int QueryFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("start_time")]
		public long StartTime { get; set; }

		/// <summary>
		/// yyyyMMddHHmmss
		/// </summary>
		[JsonProperty("stop_time")]
		public string StopTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("tradeNo")]
		public string TradeNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("transNo")]
		public string TransNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("bankCode")]
		public string BankCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("transType")]
		public int TransType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("transAmount")]
		public decimal TransAmount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("transDate")]
		public DateTime TransDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("transStatus")]
		public int TransStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("merVAR")]
		public string MerVar { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("currencyId")]
		public int CurrencyId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("bankTransNo")]
		public string BankTransNo { get; set; }
	}

	class RefundInfoContainer : BaseOtnApiResponseWithFlagAndMsg
	{
		[JsonProperty("data")]
		public RefundInfo Data { get; set; }
	}
}
