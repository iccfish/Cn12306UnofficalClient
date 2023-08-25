namespace TOBA.Account;

using System.Threading.Tasks;

using FSLib.Network.Http;

using WebLib;

class SmsService : ISmsService
{
	/// <inheritdoc />
	public async Task<(int code, string message)> SendLoginVerifySmsAsync(NetClient client, string username, string idlast4)
	{
		var url    = "/passport/web/getMessageCode";
		var data   = new { appid       = "otn", username, castNum = idlast4 };
		var result = new { result_code = 0, result_message        = "" };

		var ctx = client.Create(HttpMethod.Post, url, data: data, result: result);
		await ctx.SendAsync();

		return !ctx.IsValid() ? (-1, $"发送验证码失败：{ctx.GetExceptionMessage("网络错误")}") : (ctx.Result.result_code, ctx.Result.result_message);
	}
}
