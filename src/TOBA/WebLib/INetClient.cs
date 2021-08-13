using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.WebLib
{
	public interface INetClient
	{
		/// <summary>
		/// 验证当前会话是否有效
		/// </summary>
		/// <returns></returns>
		bool? VerifySessionValid(int recursiveCount = 4, bool noOtnPortCheck = false);
	}
}
