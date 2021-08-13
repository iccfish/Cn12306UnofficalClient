using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Account
{
	using System.ComponentModel.Composition;
	using System.IO;
	using System.Net;

	using WebLib;


	class SessionCookiesPersentator : ISessionPresentor
	{
		private static readonly string KeyPrefix = "TOBA.Account.SessionCookiesPersentator";

		public void Save(Session session, NetClient netClient, Dictionary<string, string> target)
		{
			var cnt = netClient?.CookieContainer ?? session?.NetClient.CookieContainer;
			var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
			var ms = new MemoryStream();
			bf.Serialize(ms, cnt);
			ms.Close();

			target[KeyPrefix] = Convert.ToBase64String(ms.ToArray());
		}

		public void Restore(Session session, NetClient netClient, Dictionary<string, string> target)
		{
			var cnt = netClient ?? session?.NetClient;

			var data = target?.GetValue(KeyPrefix);
			if (data.IsNullOrEmpty() || cnt == null)
				return;

			try
			{
				var bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
				var ms = new MemoryStream(Convert.FromBase64String(data));
				var cookieContainer = bf.Deserialize(ms) as CookieContainer;
				ms.Close();

				if (cookieContainer != null)
					cnt.CookieContainer = cookieContainer;
			}
			catch (Exception e)
			{
			}
		}
	}
}
