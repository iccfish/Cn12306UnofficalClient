using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Account.Entities
{
	using Entity;

	using Newtonsoft.Json;

	using WebLib;

	class My12306Info : Dto
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("notify_way")]
		public string NotifyWay { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("qr_code_url")]
		public string QrCodeUrl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("if_show_ali_qr_code")]
		public bool IfShowAliQrCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isSuperUser")]
		public string IsSuperUser { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("_email")]
		public string Email { get; set; }

		/// <summary>
		/// 密码安全级别较低
		/// </summary>
		[JsonProperty("_is_needModifyPassword")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool? IsNeedModifyPassword { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("needEdit")]
		public bool NeedEdit { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("member_status")]
		public string MemberStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("notify_TWO_2")]
		public string NotifyTwo2 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("user_name")]
		public string UserName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("member_level")]
		public string MemberLevel { get; set; }

		/// <summary>
		/// 是否可以注册会员
		/// </summary>
		[JsonProperty("isCanRegistMember")]
		public bool IsCanRegistMember { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("user_regard")]
		public string UserRegard { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("resetMemberPwd")]
		public string ResetMemberPwd { get; set; }

		/// <summary>
		/// 是否验证邮箱
		/// </summary>
		[JsonProperty("_is_active")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool? IsActive { get; set; }

	}
}
