using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Account
{
	using System.Threading.Tasks;

	using WebLib;

	using static System.Runtime.CompilerServices.RuntimeHelpers;

	internal interface ISmsService
	{
		/// <summary>
		/// 发送登录验证码
		/// </summary>
		/// <returns></returns>
		Task<(int code, string message)> SendLoginVerifySmsAsync(NetClient client, string username, string idlast4);
	}
}
