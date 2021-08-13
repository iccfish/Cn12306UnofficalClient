namespace TOBA.Account
{
	using System;
	using System.Text.RegularExpressions;

	using Entities;

	using FSLib.Network.Http;

	using Otn.Entity;

	using TOBA.Profile;
	using TOBA.UI;

	internal class GetDisplayNameWorker : IOperation
	{
		/// <summary>
		/// 开始运行工作操作
		/// </summary>
		public void Run()
		{
			Events.OnMessage(this, new EventInfoArgs()
			{
				Message = "正在获得账号【" + Session.UserProfile.UserName + "】的相关信息……"
			});

			var result = Session.GetService<IAccountService>().GetQueryInfoResponseAsync().Result;
			if (result == null)
			{
				Events.OnWarning(this, new EventInfoArgs()
				{
					Message = "无法获得账号【" + Session.UserProfile.UserName + "】的真实姓名和相关信息！"
				});
			}
			else
			{
				Session.UserKeyData.MobileNumber = result.UserInfo?.MobileNo;
				Session.IsMobileChecked = result.IsMobileCheck || result.UserInfo?.IsReceive == true;
				if (Session.UserKeyData.DisplayName.IsNullOrEmpty())
				{
					Session.UserKeyData.DisplayName = result.UserInfo?.LoginUserInfo?.Name;
				}
				UserKeyDataMap.Current.Save();

				if (Session.IsUserVerified == null)
				{
					//已通过；预通过；未通过；请报验；待核验
					Session.IsUserVerified = Regex.IsMatch(result.Notice, @"[已预]通过");
				}
				Events.OnMessage(this, new EventInfoArgs()
				{
					Message = "已获得账户【" + Session.UserProfile.UserName + "】的手机检验状态 => 【" + (Session.IsMobileChecked == true ? "已通过" : "未通过") + "】"
				});
				Events.OnMessage(this, new EventInfoArgs()
				{
					Message = "已获得【" + Session.UserProfile.UserName + "】的真实姓名 => 【" + Session.UserKeyData.DisplayName + "】"
				});
				Events.OnMessage(this, new EventInfoArgs()
				{
					Message = "已获得账户【" + Session.UserProfile.UserName + "】的检验状态 => 【" + (Session.IsUserVerified == true ? "已通过" : "未通过或待校验") + "】"
				});
			}
		}

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session { get; set; }
	}
}
