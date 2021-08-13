using System;
using System.Collections.Generic;
using System.Linq;

namespace TOBA
{
	using Entity;

	using FSLib.Network.Http;

	using Newtonsoft.Json;

	using System.Net;
	using System.Net.Sockets;

	internal static class Utility
	{
		/// <summary>
		/// 获得两个站点是否是相同或相近的（包含关系）
		/// </summary>
		/// <param name="statName1"></param>
		/// <param name="statName2"></param>
		/// <returns></returns>
		public static bool IsStationInclude(string statName1, string statName2)
		{
			if (statName1.IsNullOrEmpty() || statName2.IsNullOrEmpty()) return false;

			if (statName1.StartsWith(statName2) || statName2.StartsWith(statName1))
				return true;

			statName1 = statName1.TrimEnd('东', '南', '西', '北');
			statName2 = statName2.TrimEnd('东', '南', '西', '北');
			return statName1.IsIgnoreCaseEqualTo(statName2);
		}

		/// <summary>
		/// 转换秒钟为友好的显示格式
		/// </summary>
		/// <param name="secs"></param>
		/// <returns></returns>
		public static string ShowSecondInfo(int secs)
		{
			var ts = new TimeSpan(0, 0, secs);
			var h = ts.Hours;
			var m = ts.Minutes;
			var s = ts.Seconds;

			return (h > 0 ? h + "时" : "") + (m > 0 ? m + "分" : "") + s + "秒";
		}

		/// <summary>
		/// 转换秒钟为友好的显示格式
		/// </summary>
		/// <param name="milliSecs"></param>
		/// <returns></returns>
		public static string ShowMilliSecondInfo(int milliSecs)
		{
			var ts = new TimeSpan(0, 0, 0, 0, milliSecs);
			var h = ts.Hours;
			var m = ts.Minutes;
			var s = ts.Seconds;
			var ms = ts.Milliseconds;

			return (h > 0 ? h + "时" : "") + (m > 0 ? m + "分" : "") + s + (ms > 0 ? "." + ms / 100 : "") + "秒";
		}

		public static string ShowElapsedMiniutes(TimeSpan time)
		{
			var hours = time.TotalHours;
			return (hours > 1 ? (int)hours + "时" : "") + time.Minutes + "分";
		}

		public static string CreateCaptchaValidate(string code)
		{
			if (string.IsNullOrEmpty(code))
				return string.Empty;
			return string.Format("{0} {1} " + "keyup" + " {2}", code, DateTime.Now.ToString(), JsonConvert.SerializeObject(new { keyCode = (int)code[code.Length - 1] }));
		}

		readonly static Dictionary<string, string> _ipHashMap = new Dictionary<string, string>();
		private static readonly bool DisableMaskIp = System.Configuration.ConfigurationManager.AppSettings["disable_ip_mask"] == "true";

		public static string FormatIp(string ip)
		{
			if (ip.IsNullOrEmpty() || DisableMaskIp)
				return ip;

			if (!_ipHashMap.ContainsKey(ip))
			{
				lock (_ipHashMap)
				{
					if (!_ipHashMap.ContainsKey(ip))
					{
						var bytes = IPAddress.Parse(ip).GetAddressBytes().SplitPage(4).Select(s => BitConverter.ToUInt32(s, 0).CompressToString()).JoinAsString(".");
						_ipHashMap.Add(ip, bytes);
					}

				}
			}

			return _ipHashMap[ip];
		}

		public static bool IsHighSpeedSeat(this SubType type)
		{
			return type == SubType.A || type == SubType.B || type == SubType.C || type == SubType.D || type == SubType.F;
		}

		public static bool IsBed(this SubType type)
		{
			return type == SubType.X || type == SubType.Z || type == SubType.S;
		}

		internal static bool IsForceLogout(this HttpContext context)
		{
			if (context == null)
				return false;

			if (context.Exception != null && typeof(ForceLogoutException) == context.Exception.GetType())
				return true;

			return false;
		}

		internal static string GetErrorMsg(this HttpContext context)
		{
			var err = context.GetExceptionMessage();
			if (context.Exception != null)
			{
				var socketex = context.Exception.InnerException as SocketException;

				if (context.Exception is WebException webex)
				{
					if (webex.Status == WebExceptionStatus.ConnectFailure)
						err = "网络错误：连接服务器失败。" + socketex?.Message + "，请确认网络正常，您可以访问12306，且代理服务器设置正确。";
					if (webex.Status == WebExceptionStatus.ConnectionClosed)
						err = "网络错误：服务器连接异常。" + socketex?.Message + "，请确认网络正常，您可以访问12306，且代理服务器设置正确。";
					if (webex.Status == WebExceptionStatus.NameResolutionFailure)
						err = "网络错误：无法解析域名。" + socketex?.Message + "，请确认网络正常且您可以访问12306。";
					if (webex.Status == WebExceptionStatus.ProxyNameResolutionFailure)
						err = "网络错误：无法解析代理服务器域名。" + socketex?.Message + "，请确认网络正常且设置正确。";
				}

				return err;
			}

			var status = context.Status;
			if (status == HttpStatusCode.ProxyAuthenticationRequired)
			{
				return "网络错误，代理服务器认证失败，请检查代理服务器设置的用户名和密码是否正确。";
			}

			if (status == HttpStatusCode.BadGateway)
			{
				return "网络错误 502 网关错误，可能是网络临时故障，请用浏览器验证是否正确。";
			}

			if (status == HttpStatusCode.InternalServerError)
			{
				return "网络错误 500 服务器错误，可能是服务器临时故障，请用浏览器验证是否正确。";
			}

			if (status == HttpStatusCode.ServiceUnavailable)
			{
				return "网络错误 504 服务不可用，请检查代理服务器设置的用户名和密码是否正确。";
			}

			if (status == HttpStatusCode.NotFound)
			{
				return "网络错误 404 请确认网络正常，您可以访问12306。";
			}

			if (status == HttpStatusCode.Forbidden)
			{
				return "网络错误 403 拒绝访问，对应的服务器无法使用，请确认网络设置正确。";
			}

			if (context.ResponseContent is ResponseBinaryContent rbc)
			{
				var str = rbc.StringResult;
				if (!str.IsNullOrEmpty())
					return (context.Status?.ToString() ?? "") + str.GetSubString(100);
			}

			return (context.Status?.ToString() ?? "") + " 网络错误";
		}
	}
}
