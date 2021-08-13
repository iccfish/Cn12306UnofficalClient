namespace TOBA.UI.Controls.Order
{
	using System.Collections.Generic;
	using System.Linq;
	using System.Windows.Forms;

	using DevComponents.DotNetBar.Controls;

	using TOBA.Order.Entity;

	internal class OrderTicketListViewItemGroup : List<OrderTicketListViewItem>
	{
		public OrderItem Order { get; private set; }

		/// <summary>
		/// 获得关联的组
		/// </summary>
		public ListViewGroup Group { get; private set; }

		/// <summary>
		/// 创建 <see cref="OrderItem" />  的新实例(OrderItem)
		/// </summary>
		public OrderTicketListViewItemGroup(OrderItem order, ListViewEx owner)
		{
			Order = order;

			var groupName = $"编号：{order.SequenceNo} / 时间：{order.order_date} / 票数：{order.ticket_totalnum} / 总票价：¥{((order.ticket_price_all) / 100):#0.00}";
			if (order.IsBackupOrder)
				groupName += " / 候补兑现订单";

			Group = new ListViewGroup(groupName) { Tag = this };

			AddRange(order.tickets.Select(s => new OrderTicketListViewItem(order, s, owner) { Group = Group }));
		}

	}
}