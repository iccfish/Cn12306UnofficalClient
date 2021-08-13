namespace TOBA.Platform.DeviceFingerprint
{
	using System;
	using System.Collections.Generic;
	using System.Net;

	class HostContext
	{
		internal static readonly string SessionDataKey = "TOBA.DeviceFingerprint.HostContext";

		public List<DeviceData> DeviceData { get; set; }

		public string Sign { get; set; }

		public string Query { get; set; }


		public string Js { get; set; }

		public string ResourceUrl { get; set; }

		public string CookieID { get; set; }

		private Session _session;

		public HostContext(Session session)
		{
			Session = session;
		}

		public Session Session
		{
			get { return _session; }
			set
			{
				_session = value;
				FingerprintInfo = Session?.GetSessionData<FingerprintInfo>(SessionDataKey);
			}
		}

		public FingerprintInfo FingerprintInfo { get; set; } = new FingerprintInfo();

		public void ClearFinterprintInfo()
		{
			Session.RemoveSessionData(SessionDataKey);
		}

		public void SetFingerprintInfoToSession()
		{
			//add cookies
			if (FingerprintInfo == null)
				return;

			Session.SetSessionData(SessionDataKey, FingerprintInfo);

			Session.NetClient.CookieContainer.Add(new Cookie
			{
				Path = "/",
				Domain = ".12306.cn",
				Name = "RAIL_DEVICEID",
				Value = FingerprintInfo.Dfp
			});
			Session.NetClient.CookieContainer.Add(new Cookie
			{
				Path = "/",
				Domain = ".12306.cn",
				Name = "RAIL_EXPIRATION",
				Value = FingerprintInfo.Expiration.ToString()
			});
		}

		public List<DeviceData> GetDeviceInfo()
		{
			var prevCookieCode = FingerprintInfo?.CookieCode;

			var random = new Random();

			var result = new List<DeviceData>()
			{
				new DeviceData("adblock", "1"),
				new DeviceData("appCodeName", "Mozilla"),
				new DeviceData("appMinorVersion", ""),
				new DeviceData("appName", "Netscape"),
				new DeviceData("browserLanguage", "zh-CN"),
				new DeviceData("cookieCode", prevCookieCode),
				new DeviceData("cookieEnabled", "1"),
				new DeviceData("cpuClass", ""),
				new DeviceData("custID", "133"),
				new DeviceData("doNotTrack", "1"),
				new DeviceData("flashVersion", "26.0 r0"),
				new DeviceData("hasLiedBrowser", "false"),
				new DeviceData("hasLiedLanguages", "false"),
				new DeviceData("hasLiedOs", "false"),
				new DeviceData("hasLiedResolution", "false"),
				new DeviceData("historyList", "4"),
				new DeviceData("indexedDb", "1"),
				new DeviceData("javaEnabled", "0"),
				new DeviceData("jsFonts", Utility.Md5(random.NextDouble().ToString())),
				new DeviceData("localStorage", "1"),
				new DeviceData("mimeTypes", Utility.Md5(random.NextDouble().ToString())),
				new DeviceData("onLine", "true"),
				new DeviceData("openDatabase", "1"),
				new DeviceData("os", "Win32"),
				new DeviceData("platform", "WEB"),
				new DeviceData("plugins", Utility.Md5(random.NextDouble().ToString())),
				new DeviceData("scrAvailHeight", "1040"),
				new DeviceData("scrAvailWidth", "1920"),
				new DeviceData("scrColorDepth", "24"),
				new DeviceData("scrDeviceXDPI", ""),
				new DeviceData("scrHeight", "1080"),
				new DeviceData("scrWidth", "1920"),
				new DeviceData("sessionStorage", "1"),
				new DeviceData("systemLanguage", ""),
				new DeviceData("timeZone", "-8"),
				new DeviceData("touchSupport", Utility.Md5(random.NextDouble().ToString())),
				new DeviceData("userAgent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.1; WOW64; Trident/6.0;)"),
				new DeviceData("userLanguage", ""),
				new DeviceData("webSmartID", Utility.Md5(random.NextDouble().ToString()))
			};

			for (int i = result.Count - 1; i >= 0; i--)
			{
				if (result[i].Value == null)
					result.RemoveAt(i);
			}

			//sort
			result.Sort();

			return result;
		}

	}
}