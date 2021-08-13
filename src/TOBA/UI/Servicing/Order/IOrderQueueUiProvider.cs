using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI.Servicing.Order
{
	using TOBA.Order.Entity;

	interface IOrderQueueUiProvider
	{
		/// <summary>
		/// 获得关联的会话
		/// </summary>
		 Session Session { get; }

		/// <summary>
		/// 要求显示
		/// </summary>
		/// <param name="queueInfo"></param>
		void Show(OrderCacheItem queueInfo);
	}
}
