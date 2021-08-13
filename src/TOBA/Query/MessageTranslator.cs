using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Query
{
	internal class MessageTranslator
	{
		public static string GetMemoForCode(int code)
		{
			switch (code)
			{
				case 0:
					return "<服务器脑袋短路中，请重试>";
				case 302:
					return "<服务器网络内部短路了，请重试>";
				case 403:
					return "<您的IP被封，请使用代理或启用智能DNS并等待节点数大于0后再登录>";
				case 404:
					return "<无效的服务器节点，请重新登录>";
				case 405:
					return "<服务器抽风，反复出现此错误可尝试重新登录>";
				case 200:
					return "<服务器有病，可安全无视此错误>";
			}

			return string.Empty;
		}

		public static string GetAdditionalInfo(string error)
		{
			if (error.IndexOf("非法的席别") != -1 || error.IndexOf("排队人数=0") != -1)
			{
				return " （可能已经无票，请查证，一般重新提交无效，反复提交会被飞）";
			}
			if (error.IndexOf("排队人数") != -1)
			{
				return " (票过少而人太多，如果票不多，可能是假象或已经无票，一般重新提交无效，反复提交会被飞)";
			}
			if (error.IndexOf("非法余票") != -1)
			{
				//TODO 非法余票信息核查
				return " (余票信息异常，同一个登录请不要同时提交多个线路)";
			}

			return null;
		}
	}
}
