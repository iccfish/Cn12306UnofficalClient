using System;
using System.Text;

namespace TOBA.Account
{
	using System.Threading.Tasks;
	using TOBA.Entity;

	/// <summary>
	/// 用户登录服务
	/// </summary>
	internal interface ISessionLoginService
	{

		/// <summary>
		/// 状态已变化事件
		/// </summary>
		event EventHandler StateChanged;

		/// <summary>
		/// 登录
		/// </summary>
		/// <returns></returns>
		Task<bool> LoginAsync();

		/// <summary>
		/// 准备登录。如果已登录过，则返回null
		/// </summary>
		/// <returns></returns>
		Task<bool?> PrepareLoginAsync();

		/// <summary>
		/// 获得此次登录是否引发了登录冲突
		/// </summary>
		bool LoginConflict { get; }

		/// <summary>
		/// 获得或设置当前的密码
		/// </summary>
		string Password { get; set; }

		/// <summary>
		/// 获得或设置当前的验证码
		/// </summary>
		string RandCode { get; set; }

		/// <summary>
		/// 获得或设置当前的会话
		/// </summary>
		Session Session { get; set; }

		/// <summary>
		/// 获得或设置当前的状态
		/// </summary>
		string State { get; }

		/// <summary>
		/// 是否保存密码
		/// </summary>
		bool StorePwd { get; set; }

		/// <summary>
		/// 获得或设置是否是临时模式
		/// </summary>
		bool TempMode { get; set; }

		/// <summary>
		/// 获得或设置当前的用户名
		/// </summary>
		string UserName { get; set; }

		/// <summary>
		/// 获得或设置是否是重新登录
		/// </summary>
		bool IsRelogin
		{
			get; set;
		}

		bool NeedSlideVcLogin { get; }

		/// <summary>
		/// 登录使用的APPID
		/// </summary>
		string SlideAppId { get; }

		string SlideVcToken { get; }
		string CfSessionId { get; set; }
		string Sig { get; set; }

		bool NeedVcLogin { get; set; }
		bool NeedSmsLogin { get; }
		Task<bool> CompleteVcAsync();
		DateTime SmsTime { get; set; }
		VcType VcType { get; set; }
	}
}
