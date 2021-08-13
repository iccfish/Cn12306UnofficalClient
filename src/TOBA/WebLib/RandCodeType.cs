using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.WebLib
{
	/// <summary>
	/// 验证码类型
	/// </summary>
	public enum RandCodeType
	{
		/// <summary>
		/// 登录验证码
		/// </summary>
		SjRand = 0,
		/// <summary>
		/// 订单提交验证码
		/// </summary>
		Randp = 1,
		RandpResign
	}
}
