using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Account
{
	using System.Threading.Tasks;

	using Entities;

	using FSLib.Network.Http;

	using Otn.Entity;

	class AccountService : IAccountService
	{
		public AccountService(Session session) { Session = session; }

		public Session Session { get; }

		/// <summary>
		/// 获得我的12306页面显示的用户信息
		/// </summary>
		/// <returns></returns>
		public async Task<My12306Info> GetInitMy12306InfoAsync()
		{
			var ctx = Session.NetClient.Create<OtnWebResponse<My12306Info>>(HttpMethod.Post, "index/initMy12306Api", "view/index.html");
			var result = await ctx.SendAsync();

			return result?.Data;
		}

		/// <summary>
		/// 获得绑定手机号页面的信息
		/// </summary>
		/// <returns></returns>
		public async Task<BindTelApiInfo> GetBindTelInfoAsync()
		{
			var ctx = Session.NetClient.Create<BindTelApiResponse>(HttpMethod.Post, "userSecurity/bindTelApi", "view/userSecurity_bindTel.html");
			var result = await ctx.SendAsync();

			return result?.Data;
		}

		/// <summary>
		/// 获得绑定手机号页面的信息
		/// </summary>
		/// <returns></returns>
		public async Task<QueryInfoResponse> GetQueryInfoResponseAsync()
		{
			var htmlCtx = Session.NetClient.Create<string>(HttpMethod.Get, "login/userLogin", "passport?redirect=/otn/login/userLogin");
			await htmlCtx.SendAsync();
			var ctx = Session.NetClient.Create<OtnWebResponse<QueryInfoResponse>>(HttpMethod.Post, "modifyUser/initQueryUserInfoApi", "view/information.html");
			var result = await ctx.SendAsync();

			return result?.Data;
		}
	}
}
