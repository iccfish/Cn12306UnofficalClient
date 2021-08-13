using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA
{
	using WebLib;

	interface ISessionPresentor
	{
		void Save(Session session, NetClient netClient, Dictionary<string, string> target);

		void Restore(Session session, NetClient netClient, Dictionary<string, string> target);
	}
}
