namespace TOBA.Order
{
	using System;

	using Entity;

	/// <summary>
	/// 退票事件参数
	/// </summary>
	internal class OrderRefundEventArgs : EventArgs
	{
		public TOBA.Order.Entity.OrderItem Order { get; private set; }

		public OrderTicket[] OrderTickets { get; private set; }

		/// <summary>
		/// 是否已经支付
		/// </summary>
		public bool IsPaid { get; private set; }

		/// <summary>
		/// 创建 <see cref="OrderRefundEventArgs"/>  的新实例(OrderRefundEventArgs)
		/// </summary>
		/// <param name="order"></param>
		/// <param name="orderTickets"></param>
		public OrderRefundEventArgs(TOBA.Order.Entity.OrderItem order, OrderTicket[] orderTickets, bool isPaid)
		{
			OrderTickets = orderTickets;
			Order = order;
			IsPaid = isPaid;
		}
	}
}