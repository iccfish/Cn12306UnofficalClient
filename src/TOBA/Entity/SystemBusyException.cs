using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Entity
{
	internal class SystemBusyException : ApplicationException
	{
		/// <summary>
		/// 创建 <see cref="SystemBusyException" />  的新实例(SystemBusyException)
		/// </summary>
		public SystemBusyException()
			: base("系统繁忙，请重试，频繁出现此错误意味着您的IP已经被12306封了")
		{
			
		}
	}
}
