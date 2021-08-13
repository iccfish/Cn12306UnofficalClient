using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using FSLib.Network.Http;
using TOBA.UI;

namespace TOBA.Otn.Workers
{
	internal class VerifyCodeCheckWorker : IOperation
	{
		public void StartAliveCheck()
		{
			ThreadPool.QueueUserWorkItem(_ =>
			{
				while (true)
				{
					Verify();
					Thread.Sleep(20 * 1000);
				}
			});
		}

		void Verify()
		{
			if (Session == null || Session.LastVerifyCode.IsNullOrEmpty() || Session.LastVerifyCode.Length != 4)
			{
				return;
			}

			var result = Session.NetClient.Create<string>(HttpMethod.Post,
														"/passport/captcha/captcha-check",
														"login/init",
														new {answer = Session.LastVerifyCode, rand = "sjrand", login_site = Session.Attributes}
				).Send();

			if (!result.IsValid())
			{
				return;
			}
			var checkResult = Newtonsoft.Json.JsonConvert.DeserializeAnonymousType(result.Result, new { result_message = "", result_code=0 });
			if (checkResult.result_code == 5)
			{
				return;
			}

			Session.LastVerifyCode = null;
			TOBA.Session.OnPreInputedVcMissed(Session);
		}

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session { get; set; }
	}
}
