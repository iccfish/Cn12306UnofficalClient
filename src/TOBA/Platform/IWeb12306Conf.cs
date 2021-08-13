namespace TOBA.Platform
{
	using System;
	using System.Collections.Generic;

	internal interface IWeb12306Conf
	{
		bool IsStudentDate { get; set; }

		/// <summary>
		/// 登录是否需要验证码
		/// </summary>
		bool IsLoginPassCode { get; set; }

		bool IsSweepLogin { get; set; }
		bool PsrQrCodeResult { get; set; }

		/// <summary>
		/// 登录地址
		/// </summary>
		string LoginUrl { get; set; }

		/// <summary>
		/// 学生开始-结束日期
		/// </summary>
		List<DateTime> StudentDate { get; set; }

		int StuControl { get; set; }
		bool IsUamLogin { get; set; }
		bool IsLogin { get; set; }
		int OtherControl { get; set; }
	}
}