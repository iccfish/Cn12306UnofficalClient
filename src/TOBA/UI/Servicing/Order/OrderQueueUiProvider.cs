namespace TOBA.UI.Servicing.Order
{
	using System;
	using TOBA.Order.Entity;
	using TOBA.UI.Dialogs.Order;

	class OrderQueueUiProvider : IOrderQueueUiProvider
	{
		private readonly object _lockObject = new object();
		private OrderQueue _queueDlg;

		public OrderQueueUiProvider(Session session)
		{
			Session = session;
		}

		/// <summary>
		/// 获得关联的会话
		/// </summary>
		public Session Session { get; }

		/// <summary>
		/// 要求显示
		/// </summary>
		/// <param name="queueInfo"></param>
		public void Show(OrderCacheItem queueInfo)
		{
			lock (_lockObject)
			{
				if (_queueDlg == null)
				{
					_queueDlg = new OrderQueue(Session, queueInfo);

					_queueDlg.Closed += (s, e) =>
										{
											_queueDlg = null;
										};
					UiUtility.PlaceFormAtCenter(_queueDlg, AppContext.HostForm);
				}

			}
		}
	}
}