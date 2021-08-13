namespace TOBA.Passport
{
	using System;
	using System.Threading.Tasks;

	using WebLib;

	interface IUamAuthService
	{
		Task<(bool? valid, string message, string displayName)> AuthTkAsync(NetClient client, Action<string> stateIndicator);

		/// <summary>
		/// 校验是否已经本地登录（仅限统一认证登录）
		/// </summary>
		/// <param name="session"></param>
		/// <returns></returns>
		Task<(bool logined, string message)> UamtkStaticAsync(Session session);
	}
}