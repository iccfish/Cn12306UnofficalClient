using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.WebLib
{
	using FSLib.Extension.Attributes;

	/// <summary>
	/// 代理服务器类型
	/// </summary>
	public enum ProxyType
	{
		/// <summary>
		/// HTTP代理
		/// </summary>
		[DisplayNameW("HTTP代理")]
		Http =0,
		/// <summary>
		/// Socks5
		/// </summary>
		[DisplayNameW("SOCKS5代理")]
		Socks5 =1
	}
}
