using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Platform.HttpConf
{
	using Newtonsoft.Json;

	using WebLib;

	class HttpConf : IWeb12306Conf
	{
		[JsonProperty("isstudentDate")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool IsStudentDate { get; set; }

		/// <summary>
		/// 登录是否需要验证码
		/// </summary>
		[JsonProperty("is_login_passCode")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool IsLoginPassCode { get; set; }

		[JsonProperty("is_sweep_login")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool IsSweepLogin { get; set; }

		[JsonProperty("psr_qr_code_result")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool PsrQrCodeResult { get; set; }

		/// <summary>
		/// 登录地址
		/// </summary>
		[JsonProperty("login_url")]
		public string LoginUrl { get; set; }

		/// <summary>
		/// 学生开始-结束日期
		/// </summary>
		[JsonProperty("studentDate")]
		public List<DateTime> StudentDate { get; set; }

		[JsonProperty("stu_control")]
		public int StuControl { get; set; }

		[JsonProperty("is_uam_login")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool IsUamLogin { get; set; }

		[JsonProperty("is_login")]
		[JsonConverter(typeof(JsonString2BoolConverter))]
		public bool IsLogin { get; set; }

		[JsonProperty("other_control")]
		public int OtherControl { get; set; }
	}
}
