using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace TOBA.Entity
{
	internal class SystemClosedException : ApplicationException
	{
		/// <summary>
		/// 创建 <see cref="SystemClosedException" />  的新实例(SystemClosedException)
		/// </summary>
		public SystemClosedException()
			: base("系统维护中，请等待系统开放后访问")
		{

		}
	
	}
}
