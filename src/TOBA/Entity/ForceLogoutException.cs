using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Entity
{
	internal class ForceLogoutException : ApplicationException
	{
		/// <summary>
		/// 创建 <see cref="ForceLogoutException" />  的新实例(ForceLogoutException)
		/// </summary>
		public ForceLogoutException()
			: base("您已经被踢，尽快重新登录哦")
		{
			
		}
	}
}
