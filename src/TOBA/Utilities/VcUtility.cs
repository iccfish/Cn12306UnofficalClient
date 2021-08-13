namespace TOBA.Utilities
{
	using System.Drawing;

	using AutoVc;

	using Service;

	class VcUtility
	{
		/// <summary>
		/// 判断是不是老的验证码
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		public static bool IsTraditionalCode(Image image)
		{
			return image != null && image.Height < 60;
		}

		/// <summary>
		/// 当前的引擎是否可用
		/// </summary>
		/// <param name="service"></param>
		/// <returns></returns>
		public static bool IsEngineAvailable(IVerifyCodeRecognizeService service)
		{
			return service != null && service.Verified && service.IsLoggedIn;
		}
	}
}
