using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using FSLib.Network.Http;
using TOBA.UI;

namespace TOBA.Otn.Workers
{
	internal class AccountModifyWorker : IOperation
	{
		#region Implementation of IOperation

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session { get; set; }

		#endregion

		/// <summary>
		/// 创建 <see cref="AccountModifyWorker" />  的新实例(AccountModifyWorker)
		/// </summary>
		/// <param name="session"></param>
		public AccountModifyWorker(Session session)
		{
			Session = session;
		}

		/// <summary>
		/// 修改账户密码
		/// </summary>
		/// <param name="oldPwd"></param>
		/// <param name="newPwd"></param>
		/// <returns></returns>
		public string ModifyPassword(string oldPwd, string newPwd, string mobileRandcode)
		{
			var result = Session.NetClient.Create(HttpMethod.Post,
												"userSecurity/editLoginPwd",
												"userSecurity/loginPwd",
												new
												{
													password = oldPwd,
													password_new = newPwd,
													confirmPassWord = newPwd,
													mobileRandcode
												},
												new
												{
													data = new { message = "", flag = false }
												})
								.Send();
			if (!result.IsValid())
				return "网络错误";
			if (result.Result.data == null)
				return "服务器返回结果不正确";
			if (!result.Result.data.flag)
				return result.Result.data.message;

			return null;
		}
	}
}
