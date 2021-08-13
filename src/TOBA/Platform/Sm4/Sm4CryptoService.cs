using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Platform.Sm4
{
	class Sm4CryptoService : ISm4CryptoService
	{
		private Sm4 _sm4;

		public Sm4CryptoService()
		{
			_sm4 = new Sm4(Encoding.UTF8.GetBytes("tiekeyuankp12306"), mode: Mode.Encrypt);
		}

		/// <inheritdoc />
		public byte[] CryptoEcb(byte[] data) => _sm4.CryptEcb(data);

		public byte[] CryptoEcb(string data) => CryptoEcb(Encoding.UTF8.GetBytes(data));

		public string CryptoEcbBase64(string data) => Convert.ToBase64String(CryptoEcb(data));
	}
}
