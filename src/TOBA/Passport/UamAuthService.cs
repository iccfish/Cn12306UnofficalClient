namespace TOBA.Passport
{
	using System;
	using FSLib.Extension;
	using System.Threading.Tasks;

	using FSLib.Network.Http;

	using WebLib;

	class UamAuthService : IUamAuthService
	{
		public async Task<(bool? valid, string message, string displayName)> AuthTkAsync([NotNull] NetClient client, Action<string> stateIndicator)
		{
			if (client == null)
				throw new ArgumentNullException(nameof(client));

			var ckResult = await client.RunRequestLoopAsync(
				_ => client.Create(HttpMethod.Post,
					"/passport/web/auth/uamtk",
					"/otn/passport?redirect=/otn/login/userLogin",
					new
					{
						appid = "otn"
					},
					new
					{
						apptk = "",
						newapptk = "",
						result_code = 0,
						result_message = "",
						mobile = ""
					}
				),
				_ => stateIndicator?.Invoke($"[{_}] 校验状态")
			).ConfigureAwait(true);

			if (!ckResult.IsValid())
			{
				stateIndicator?.Invoke("登录失败，请重试");
				return (null, "登录失败，请重试", null);
			}
			if (ckResult.Result.result_code != 0)
			{
				var error = "登录失败: " + ckResult.Result.result_message.DefaultForEmpty("未知错误");
				switch (ckResult.Result.result_code)
				{
					case 91:
						error = $"需验证手机号，请用尾号{ckResult.Result.mobile}的手机发送“666”到12306";
						break;
					case 101:
						error = "需要更新密码，请登录12306重置密码后登录";
						break;
				}

				stateIndicator?.Invoke(error);
				return (false, error, null);
			}
			client.AppTk = ckResult.Result.newapptk;

			//获得信息
			var uamAuthCtx = await client.RunRequestLoopAsync(_ => client.Create(
					HttpMethod.Post,
					"/otn/uamauthclient",
					"/otn/passport?redirect=/otn/login/userLogin",
					new { tk = client.AppTk },
					new
					{
						apptk = "",
						result_code = 0,
						result_message = "",
						username = ""
					}),
				_ => stateIndicator?.Invoke($"[{_}] 校验状态[UAM]")).ConfigureAwait(true);

			if (!uamAuthCtx.IsValid())
			{
				var msg = "登录失败，请重试 [UAMAUTH_FAILED]";
				stateIndicator?.Invoke(msg);
				return (null, msg, null);
			}
			var uamAuthResult = uamAuthCtx.Result;
			if (uamAuthResult.result_code != 0)
			{
				var error = uamAuthResult.result_message.DefaultForEmpty("未知错误");
				var msg = "登录失败 [UAMAUTH_FAILED]：" + error;
				stateIndicator?.Invoke(msg);
				return (false, msg, null);
			}

			return (true, null, uamAuthResult.username);
		}

		/// <summary>
		/// 校验是否已经本地登录（仅限统一认证登录）
		/// </summary>
		/// <param name="session"></param>
		/// <returns></returns>
		public async Task<(bool logined, string message)> UamtkStaticAsync(Session session)
		{
			var uamAuthCtx = await session.NetClient.RunRequestLoopAsync(_ => session.NetClient.Create(
						HttpMethod.Post,
						"/passport/web/auth/uamtk-static",
						"/otn/passport?redirect=/otn/login/userLogin",
						new { appid = "otn" },
						new
						{
							result_code = 0,
							result_message = ""
						}),
					null).
				ConfigureAwait(true);

			if (!uamAuthCtx.IsValid())
			{
				return (false, uamAuthCtx.GetExceptionMessage("网络错误"));
			}

			return (uamAuthCtx.Result.result_code == 0, uamAuthCtx.Result.result_message);
		}
	}
}