namespace TOBA.Platform.DeviceFingerprint
{
	using Entity;

	using FSLib.Network.Http;

	using System;
	using System.Net;

	class FingerprintInfo : Dto, IFingerprintInfo
	{
		public string Dfp { get; set; }

		public string CookieCode { get; set; }

		public DateTime Expire { get; set; }

		public long Expiration { get; set; }

		public string AlgorithmId { get; set; }

		public string CookieID { get; set; }

		public string FpVersion { get; set; }

		public void SetToNetClient(HttpClient client)
		{
			if (!Dfp.IsNullOrEmpty())
				client.CookieContainer.Add(new Cookie
				{
					Path = "/",
					Domain = ".12306.cn",
					Name = "RAIL_DEVICEID",
					Value = Dfp
				});
			if (Expiration > 0)
				client.CookieContainer.Add(new Cookie
				{
					Path = "/",
					Domain = ".12306.cn",
					Name = "RAIL_EXPIRATION",
					Value = Expiration.ToString()
				});
			if (!FpVersion.IsNullOrEmpty())
				client.CookieContainer.Add(new Cookie
				{
					Path = "/",
					Domain = ".12306.cn",
					Name = "fp_ver",
					Value = FpVersion
				});

		}
	}
}
