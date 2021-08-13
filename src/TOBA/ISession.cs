using System;
using System.Collections.Generic;

namespace TOBA
{
	/// <summary>
	/// 表示一个会话
	/// </summary>
	public interface ISession
	{

		/// <summary>
		/// 加载用户的联系人
		/// </summary>
		void LoadPassengers();

		/// <summary>
		/// 获得或设置最后心跳包的时间
		/// </summary>
		DateTime? LastHeartBeatTime { get; set; }

		/// <summary>
		/// 是否已经登录
		/// </summary>
		bool IsLogined { get; }
		/// <summary>
		/// 获得或设置最后的验证码
		/// </summary>
		string LastVerifyCode { get; set; }

		string Attributes { get; set; }

		/// <summary>
		/// 用户名
		/// </summary>
		string UserName { get; }

		/// <summary>
		/// 获得当前使用的客户端
		/// </summary>
		TOBA.WebLib.INetClient NetClient { get; }

		/// <summary>
		/// 获得需要在会话中保存的数据字典
		/// </summary>
		Dictionary<string, object> SessionData { get; }
	}
}
