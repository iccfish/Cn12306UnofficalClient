namespace TOBA.Account
{
	using System.Threading.Tasks;

	using Entities;

	internal interface IAccountService
	{
		/// <summary>
		/// 获得我的12306页面显示的用户信息
		/// </summary>
		/// <returns></returns>
		Task<My12306Info> GetInitMy12306InfoAsync();

		/// <summary>
		/// 获得绑定手机号页面的信息
		/// </summary>
		/// <returns></returns>
		Task<BindTelApiInfo> GetBindTelInfoAsync();

		/// <summary>
		/// 获得绑定手机号页面的信息
		/// </summary>
		/// <returns></returns>
		Task<QueryInfoResponse> GetQueryInfoResponseAsync();
	}
}