namespace TOBA.Platform.DeviceFingerprint
{
	interface IFingerprintService
	{
		/// <summary>
		/// 初始化
		/// </summary>
		/// <returns></returns>
		bool Initialize();

		(bool success, string message) InitTagStatus(HostContext context, string jsContent = "");

		(bool success, string message) BuildDeviceFingerprint(HostContext host);

		(bool success, string message) PostFpInfo(HostContext host);

		void ClearFpInfo(Session session);

		bool IsInitialized { get; }

		/// <summary>
		/// Gets the fingerprint information from session.
		/// </summary>
		/// <param name="session">The session.</param>
		/// <returns></returns>
		IFingerprintInfo GetFingerprintInfoFromSession(Session session);
	}
}