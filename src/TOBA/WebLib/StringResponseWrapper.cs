using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using TOBA.Entity;

namespace TOBA.WebLib
{
	using System.Text.RegularExpressions;

	using FSLib.Network.Http;

	internal class StringResponseWrapper : FSLib.Network.Http.ResponseStringContent
	{
		/// <summary>
		/// 创建 <see cref="ResponseStringContent"/>  的新实例(ResponseStringContent)
		/// </summary>
		public StringResponseWrapper(HttpContext context, HttpClient client)
			: base(context, client)
		{
		}

		/// <summary>
		/// 获得登录错误信息
		/// </summary>
		/// <returns></returns>
		public string GetErrorMessage()
		{
			if (Result.IndexOf("正确的验证码") != -1) return "请输入正确的验证码";

			var m = Regex.Match(Result, @"var\s+message\s*=\s*""([^""]*)", System.Text.RegularExpressions.RegexOptions.Singleline);
			return m.Success ? m.Groups[1].Value.Replace("\\n", "\n") : null;
		}

		///// <summary>
		///// 获得用户显示的名字
		///// </summary>
		///// <returns></returns>
		//public string GetDisplayUserName()
		//{
		//	var m = Regex.Match(Result, @"'(.*?)(先生|女士)\s*，\s*您好");
		//	if (m.Success) return m.Groups[1].Value;
		//	return string.Empty;
		//}


		///// <summary>
		///// 分析表单域中的提交信息
		///// </summary>
		///// <returns></returns>
		//public NameValueCollection ParseHtmlFields()
		//{
		//	return ParseUtility.ParseHtmlFields(Result);
		//}

		///// <summary>
		///// 分析表单中的SELECT域
		///// </summary>
		///// <returns></returns>
		//public NameValueCollection GenerateSelectFields()
		//{
		//	return ParseUtility.GenerateSelectFields(Result);
		//}
	}
}
