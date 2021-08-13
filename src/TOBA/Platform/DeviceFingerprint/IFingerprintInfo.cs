namespace TOBA.Platform.DeviceFingerprint
{
	using System;

	internal interface IFingerprintInfo
	{
		string Dfp { get; set; }
		string CookieCode { get; set; }
		DateTime Expire { get; set; }
		long Expiration { get; set; }
		string AlgorithmId { get; set; }
		string CookieID { get; set; }
		string FpVersion { get; set; }
	}
}
