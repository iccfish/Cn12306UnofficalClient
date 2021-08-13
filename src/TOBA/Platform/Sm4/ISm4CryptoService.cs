namespace TOBA.Platform.Sm4
{
	interface ISm4CryptoService
	{
		byte[] CryptoEcb(byte[] data);

		byte[] CryptoEcb(string data);

		string CryptoEcbBase64(string data);
	}
}
