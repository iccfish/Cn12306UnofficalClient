namespace TOBA.Entity
{
	using Newtonsoft.Json;
	using Newtonsoft.Json.Serialization;

	using System.Collections.Generic;

	using WebLib;

	internal class ExportedSession : Dto
	{
		public ExportedSession()
		{

		}
		public ExportedSession(Session session, NetClient netClient = null)
		{
			FromSession(session, netClient);
		}

		[JsonProperty("data")]
		public Dictionary<string, string> Data { get; set; } = new Dictionary<string, string>();

		[JsonProperty("userName")]
		public string UserName { get; set; }

		[JsonProperty("appTk")]
		public string AppTk { get; set; }

		[JsonProperty("uamtk")]
		public string Uamtk { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }

		public void FromSession(Session session, NetClient netClient = null)
		{
			if (netClient == null)
				netClient = session.NetClient;

			UserName = session.UserName;
			AppTk = netClient.AppTk;
			Uamtk = netClient.Uamtk;
			Password = session.Password;

			var exts = AppContext.ExtensionManager.SessionPresentors;
			if (exts == null)
				return;

			foreach (var presentor in exts)
			{
				presentor.Save(session, netClient, Data);
			}
		}

		public void ToSession(Session session, NetClient netClient = null)
		{
			if (netClient == null)
				netClient = session.NetClient;
			netClient.Uamtk = Uamtk;
			netClient.AppTk = AppTk;
			if (session != null && !string.IsNullOrEmpty(Password))
				session.Password = Password;

			var exts = AppContext.ExtensionManager.SessionPresentors;
			if (exts == null)
				return;

			foreach (var presentor in exts)
			{
				presentor.Restore(session, netClient, Data);
			}
		}
	}
}