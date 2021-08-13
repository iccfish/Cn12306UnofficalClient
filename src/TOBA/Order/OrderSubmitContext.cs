namespace TOBA.Order
{
	using System.Linq;

	using Query.Entity;

	using TOBA.Entity;
	using TOBA.Entity.Web;

	/// <summary>
	/// 订单提交上下文
	/// </summary>
	internal class OrderSubmitContext
	{

		/// <summary>
		/// 获得或设置会话
		/// </summary>
		public Session Session { get; set; }

		/// <summary>
		/// 获得或设置查询信息
		/// </summary>
		public QueryParam QueryParam { get; set; }

		/// <summary>
		/// 获得或设置列车
		/// </summary>
		public QueryResultItem Train { get; set; }

		/// <summary>
		/// 获得或设置订单ID
		/// </summary>
		public string OrderID { get; set; }

		/// <summary>
		/// 获得或设置乘客
		/// </summary>
		public PassengerInTicket[] Passengers { get; set; }

		/// <summary>
		/// 获得或设置信息
		/// </summary>
		public string Message { get; set; }
	}
}
