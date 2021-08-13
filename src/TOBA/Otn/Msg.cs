using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Otn
{
	using Newtonsoft.Json;

	using Workers;

	internal static class Msg
	{

		static Dictionary<string, string> _errorMsg;

		static Msg()
		{
			_errorMsg = JsonConvert.DeserializeObject<Dictionary<string, string>>("{randCodeError: \"验证码错误!\",randCodeExpired: \"验证码失效\",randCodeLentgh: \"验证码长度为4位!\",randCodeFormat: \"验证码只能由数字或字母组成!\",randCodeEmpty: \"验证码不能为空!\",userNameEmpty: \"登录名必须填写!\",userNameFormat: \"登录名格式不正确，请重新输入!\",passwordEmpty: \"密码必须填写,且不少于6位!\",passwordLength: \"密码长度不能少于6位!\",pleaseClickCaptcha: \"请点击验证码\",pleaseClickLeftCaptcha: \"请点击左侧验证码\",pleaseClickCaptchaRight: \"请正确点击验证码\",loginError: \"当前访问用户过多,请稍候重试!\"}");
		}

		public static string Translate(string msg)
		{
			if (msg == "FALSE")
				msg = "randCodeError";
			return _errorMsg.GetValue(msg).DefaultForEmpty(msg);
		}
	}
}
