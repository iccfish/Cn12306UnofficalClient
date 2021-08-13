namespace TOBA.Platform.DeviceFingerprint
{
	using Newtonsoft.Json;

	using System;
	using System.Collections.Generic;

	using WebLib;

	class DeviceFingerprintSessinPersentor : ISessionPresentor
	{
		private static readonly string KeyPrefix = "TOBA.DeviceFingerprint.DeviceFingerprintSessinPersentor";

		public void Save(Session session, NetClient netClient, Dictionary<string, string> target)
		{
			if (session == null)
				return;

			var host = new HostContext(session);
			var fp = host.FingerprintInfo;
			if (fp == null)
				return;

			target[KeyPrefix] = JsonConvert.SerializeObject(fp);
		}

		public void Restore(Session session, NetClient netClient, Dictionary<string, string> target)
		{
			if (session == null)
				return;

			var data = target?.GetValue(KeyPrefix);
			if (data == null)
				return;

			var host = new HostContext(session);
			var fp = JsonConvert.DeserializeObject<FingerprintInfo>(data);
			host.FingerprintInfo = fp;
			host.SetFingerprintInfoToSession();
		}
	}
}
