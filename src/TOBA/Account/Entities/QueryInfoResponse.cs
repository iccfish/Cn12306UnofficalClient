using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Account.Entities
{
	using Entity;

	using Newtonsoft.Json;

	using WebLib;

	class LoginUser
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("center")]
		public string Center { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("login_channel")]
		public string LoginChannel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("login_site")]
		public string LoginSite { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("login_id")]
		public string LoginId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("order_type")]
		public string OrderType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("agent_contact")]
		public string AgentContact { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("user_type")]
		public string UserType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("user_name")]
		public string UserName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("id_type_code")]
		public string IdTypeCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("id_type_name")]
		public string IdTypeName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("id_no")]
		public string IdNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("member_id")]
		public string MemberId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("member_level")]
		public string MemberLevel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("userIpAddress")]
		public string UserIpAddress { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("gat_born_date")]
		public string GatBornDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("gat_valid_date_start")]
		public string GatValidDateStart { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("gat_valid_date_end")]
		public string GatValidDateEnd { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("gat_version")]
		public string GatVersion { get; set; }

	}
	class StudentInfo
	{
	}
	class UserInfo
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("loginUserDTO")]
		public LoginUser LoginUserInfo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("studentInfoDTO")]
		public StudentInfo StudentInfo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("is_receive")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool IsReceive { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("password")]
		public string Password { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("password_new")]
		public string PasswordNew { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("pwd_question")]
		public string PwdQuestion { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("pwd_answer")]
		public string PwdAnswer { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("sex_code")]
		public string SexCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("born_date")]
		public string BornDate { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("country_code")]
		public string CountryCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("mobile_no")]
		public string MobileNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("phone_no")]
		public string PhoneNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("email")]
		public string Email { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("address")]
		public string Address { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("postalcode")]
		public string Postalcode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("is_active")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool IsActive { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("revSm_code")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool RevSmCode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("last_login_time")]
		public string LastLoginTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("user_id")]
		public long UserId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("phone_flag")]
		public string PhoneFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("encourage_flag")]
		public string EncourageFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("user_status")]
		public string UserStatus { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("check_id_flag")]
		public string CheckIdFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("is_valid")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool IsValid { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("display_control_flag")]
		public string DisplayControlFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("needModifyEmail")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool NeedModifyEmail { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("flag_member")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool FlagMember { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("pic_control_flag")]
		public string PicControlFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("regist_time")]
		public string RegistTime { get; set; }

	}
	class QueryInfoResponse
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("userTypeName")]
		public string UserTypeName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("picFlag")]
		public string PicFlag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("canUpload")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool CanUpload { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("userPassword")]
		public string UserPassword { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("notice1")]
		public string Notice1 { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("canAddGAT")]
		public bool CanAddGAT { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isMobileCheck")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool IsMobileCheck { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("listEntrySchoolYeasrs")]
		public List<BasicIdValueEntity<string, string>> ListEntrySchoolYeasrs { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("canEdit")]
		public bool CanEdit { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("country_name")]
		public string CountryName { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("listSchoolSystem")]
		public List<BasicIdValueEntity<string, string>> ListSchoolSystem { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("userDTO")]
		public UserInfo UserInfo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("listCardType")]
		public List<BasicIdValueEntity<string, string>> ListCardType { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("listPassengerTypes")]
		public List<BasicIdValueEntity<string, string>> ListPassengerTypes { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("bornDateString")]
		public string BornDateString { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("listCountry")]
		public List<BasicIdValueEntity<string, string>> ListCountry { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("listProvince")]
		public List<BasicIdValueEntity<string, string>> ListProvince { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("notice")]
		public string Notice { get; set; }

	}
}
