using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TOBA.Otn.Entity;

namespace TOBA.Otn
{
	using FSLib.Network.Http;

	internal static class ExtensionMethod
	{

		/// <summary>
		/// 获得错误信息
		/// </summary>
		/// <param name="defaultMsg"></param>
		/// <returns></returns>
		public static string GetErrorMessage(this OtnWebResponse response, string defaultMsg = "网络错误")
		{
			if (response == null)
				return defaultMsg;

			return (response.Messages?.JoinAsString("")).DefaultForEmpty(defaultMsg);
		}

	}
}
