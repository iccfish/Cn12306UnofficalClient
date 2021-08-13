using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Account
{
	using System.Text.RegularExpressions;
	using System.Threading.Tasks;

	using FSLib.Network.Http;

	using Profile;

	internal class BindMobileService
	{
		public BindMobileService(Session session)
		{
			Session = session;
		}

		public Session Session { get; private set; }

		/// <summary>
		/// 判断当前的验证模式是不是双向验证。双向验证模式下，禁止主动请求服务器
		/// </summary>
		/// <returns></returns>
		public async Task<bool?> GetMobileCheckIsDoubleAsync()
		{
			var client = Session.NetClient;
			var ctx = client.Create<string>(HttpMethod.Get, "userSecurity/bindTel");
			await ctx.SendAsync().ConfigureAwait(true);

			if (!ctx.IsValid())
				return null;

			var m = Regex.Match(ctx.Result, @"\sinfo_show\s*=\s*['""]([YN])['""]", RegexOptions.IgnoreCase | RegexOptions.Singleline);

			return m.Success && m.GetGroupValue(1) == "Y";
		}

		/// <summary>
		/// 修改手机号码
		/// </summary>
		/// <param name="mobileCode"></param>
		/// <returns></returns>
		public async Task<string> ChangeMobileAsync(string mobileCode)
		{
			var ctx = Session.NetClient.Create(HttpMethod.Post, "userSecurity/doEditTel", "userSecurity/bindTel", new
			{
				mobile_no = mobileCode,
				_loginPwd = Session.Password
			}, new
			{
				data = new { message = "", flag = false }
			});
			await ctx.SendAsync().ConfigureAwait(true);

			if (!ctx.IsValid())
			{
				return "网络错误";
			}

			return ctx.Result.data?.flag == true ? null : ctx.Result.data?.message ?? "未知错误";
		}

		/// <summary>
		/// 要求将验证码下发到手机。返回错误信息。如果返回为null或空字符串，则操作成功；否则操作失败。
		/// </summary>
		/// <returns></returns>
		public async Task<string> GetMobileCodeAsync(string mobileCode)
		{
			var ctx = Session.NetClient.Create(HttpMethod.Post, "userSecurity/getMobileCode", "userSecurity/bindTel", new
			{
				mobile = mobileCode
			}, new
			{
				data = new { errorMsg = "" }
			});
			await ctx.SendAsync().ConfigureAwait(true);

			if (!ctx.IsValid())
			{
				return "网络错误";
			}

			return ctx.Result.data?.errorMsg ?? "未知错误";
		}

		/// <summary>
		/// 核验验证码是否正确
		/// </summary>
		/// <returns></returns>
		public async Task<string> CheckMobileCodeAsync(string mobileCode, string randCode)
		{
			var ctx = Session.NetClient.Create(HttpMethod.Post, "userSecurity/checkMobileCode", "userSecurity/bindTel", new
			{
				mobile = mobileCode, randCode
			}, new
			{
				data = new { errorMsg = "" }
			});
			await ctx.SendAsync().ConfigureAwait(true);

			if (!ctx.IsValid())
			{
				return "网络错误";
			}

			return ctx.Result.data?.errorMsg;
		}

		/// <summary>
		/// 刷新当前的核验状态
		/// </summary>
		/// <returns></returns>
		public async Task<bool> RefreshStatusAsync()
		{
			var nc = Session.NetClient;
			var result = nc.Create<string>(HttpMethod.Post, "modifyUser/initQueryUserInfo", "leftTicket/init");
			await result.SendAsync().ConfigureAwait(true);
			if (!result.IsSuccess)
			{
				return false;
			}

			//手机校验
			var m = Regex.Match(result.Result, @"id=""relation_way_view"".*?手机号码：.*?(\d{5,}).*?([已未])通过核验", RegexOptions.Singleline | RegexOptions.IgnoreCase);
			if (!m.Success)
			{
				return false;
			}
			Session.UserKeyData.MobileNumber = m.GetGroupValue(1).Trim();
			Session.IsMobileChecked = m.GetGroupValue(2).Trim() == "已";

			return true;
		}
	}
}
