namespace TOBA.Order
{
	using System;

	/// <summary>
	/// 订单事件参数
	/// </summary>
	internal class OrderSubmitEventArgs : EventArgs
	{
		public OrderSubmitContext OrderSubmitContext { get; set; }

		/// <summary>
		/// 创建 <see cref="OrderSubmitEventArgs" />  的新实例(OrderSubmitEventArgs)
		/// </summary>
		/// <param name="orderSubmitContext"></param>
		public OrderSubmitEventArgs(OrderSubmitContext orderSubmitContext)
		{
			OrderSubmitContext = orderSubmitContext;
		}
	}
}