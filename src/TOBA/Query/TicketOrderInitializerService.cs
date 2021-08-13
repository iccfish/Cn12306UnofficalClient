using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Query
{
	using System.Threading.Tasks;

	using FSLib.Network.Http;

	class TicketOrderInitializerService : ITicketOrderInitializerService
	{
		public TicketOrderInitializerService(Session session)
		{
			Session = session;
		}

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session { get; }

		/// <summary>
		/// 执行异步初始化
		/// </summary>
		/// <returns></returns>
		public async Task<bool> InitializeAsync()
		{
			await Session.NetClient.RunRequestLoopAsync(
					_ => Session.NetClient.Create<string>(HttpMethod.Get, "leftTicket/init", "login/init")
				).ConfigureAwait(true);

			Session.IsPassengerLoaded = false;
			if (!Session.IsPassengerLoaded)
				await Task.Factory.StartNew(Session.LoadPassengers).ConfigureAwait(true);

			return true;
		}
	}
}
