using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Account.Entities
{
	using Entity;

	using Newtonsoft.Json;

	using Otn.Entity;

	using WebLib;

	class BindTelApiResponse : OtnWebResponse<BindTelApiInfo>
	{
	}

	class BindTelApiInfo : Dto
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("info_show")]
		public string InfoShow { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isMobileCheck")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool IsMobileCheck { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("mo_msg")]
		public string MoMsg { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("mobile_no")]
		public string MobileNo { get; set; }
	}
}
