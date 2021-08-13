using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Entity
{
	internal class EmptyResponseException : ApplicationException
	{
		/// <summary>
		/// 创建 <see cref="EmptyResponseException" />  的新实例(EmptyResponseException)
		/// </summary>
		public EmptyResponseException()
			: base("服务器返回错误的响应")
		{
			
		}
	}
}
