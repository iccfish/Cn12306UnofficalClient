using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Platform
{
	using System.Drawing;
	using System.Threading.Tasks;

	using WebLib;

	interface IQrLogin12306Service
	{
		Task<(int code, string message, Image qrImg, string uuid)> CreateQrImageAsync(Session session);

		Task<(LoginQrState? state, string message, string uamtk)> CheckLoginStateAsync(Session session, string uuid);

		Task<Session> InitQrLoginAsync();
	}

	/// <summary>
	/// 登录QR状态
	/// </summary>
	enum LoginQrState
	{
		/// <summary>
		/// 有效、未识别
		/// </summary>
		Valid = 0,
		/// <summary>
		/// 已识别、未授权
		/// </summary>
		Success = 1,
		/// <summary>
		/// 已登录(UAM登录)
		/// </summary>
		LoggedIn = 2,
		/// <summary>
		/// 已失效
		/// </summary>
		Expired = 3,
		/// <summary>
		/// 系统错误
		/// </summary>
		SystemError = 5
	}
}
