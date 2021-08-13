using FSLib.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.WebLib
{
	using System.Net;

	using FSLib.Network.Http;

	interface IRequestInspector
	{
		/// <summary>
		/// 校验请求
		/// </summary>
		/// <param name="context"></param>
		void ValidateResponse(HttpContext context, HttpRequestMessage request, HttpResponseMessage response, HttpRequestContent requestContent, HttpResponseContent responseContent, CookieCollection cookies);
	}
}
