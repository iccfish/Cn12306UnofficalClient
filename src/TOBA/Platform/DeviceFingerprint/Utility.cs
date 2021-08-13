namespace TOBA.Platform.DeviceFingerprint
{
	using System;
	using System.Linq;
	using System.Security.Cryptography;
	using System.Text;

	class Utility
	{
		public static SHA256 _sha256 = SHA256.Create();

		public static MD5 _md5 = MD5.Create();

		public static string Base64(byte[] buffer) => Convert.ToBase64String(buffer).Replace('+', '-').Replace('/', '_').Replace("=", "");

		public static byte[] Sha256(string str) => _sha256.ComputeHash(Encoding.UTF8.GetBytes(str));

		public static string Sha256WithBase64(string str) => Base64(Sha256(str));

		public static string Md5(string str)
		{
			var md5 = System.Security.Cryptography.MD5.Create();
			var buffer = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

			return buffer.Select(s => s.ToString("x2")).JoinAsString("");
		}

	}
}