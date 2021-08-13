namespace TOBA.Account.Entities
{
	using System.Linq;

	enum LogoutReason
	{
		/// <summary>
		/// 账户被踢
		/// </summary>
		AccoutKicked,
		/// <summary>
		/// 程序退出
		/// </summary>
		Shutdown,
		/// <summary>
		/// 手动关闭
		/// </summary>
		UserManually
	}
}
