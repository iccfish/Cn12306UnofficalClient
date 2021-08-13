using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI.Controls.Order
{
	using System.Drawing;
	using System.Windows.Forms;
	using DevComponents.DotNetBar.Controls;
	using Entity;
	using Entity.Web;

	using TOBA.Order.Entity;

	internal class OrderList : ListViewEx
	{
		/// <summary>
		/// 创建 <see cref="OrderList" />  的新实例(OrderList)
		/// </summary>
		public OrderList()
		{
			if (Program.IsRunning)
				Init();
		}

		/// <summary>
		/// 初始化请求
		/// </summary>
		void Init()
		{
			View = View.Details;
			FullRowSelect = true;
			HideSelection = false;
			SmallImageList = new ImageList() { ImageSize = new Size(1, 20), ColorDepth = ColorDepth.Depth32Bit };
			HeaderStyle = ColumnHeaderStyle.Nonclickable;
			CheckBoxes = true;

			//init column header
			Columns.AddRange(new[]
				{"发车时间", "到达时刻", "车次", "发站", "到站", "距离", "票种", "席别", "车厢", "座位", "票价", "乘客", "证件", "证件号码", "状态"}.Select(s => new ColumnHeader() {Text = s}).ToArray());

			//事件
			ItemCheck += OrderList_ItemCheck;
		}

		bool _suspendCheckEvent = false;

		void OrderList_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (e.NewValue != CheckState.Checked || _suspendCheckEvent)
				return;

			_suspendCheckEvent = true;

			var item = Items[e.Index];
			//取消其它订单的选择
			CheckedItems.Cast<OrderTicketListViewItem>()
						.Where(s => s.Group != item.Group)
						.ToArray().ForEach(s => s.Checked = false);
			//如果是未完成订单，则同时选择
			var titem = (item as OrderTicketListViewItem).Ticket;
			if (titem.OrderStatus == OrderStatus.NotPay || titem.OrderStatus == OrderStatus.ResignNotPaid || titem.OrderStatus == OrderStatus.Queue)
			{
				Items.Cast<OrderTicketListViewItem>()
					.Where(s => s != item && s.Group == item.Group)
					.ForEach(s => s.Checked = true);
			}
			_suspendCheckEvent = false;
		}

		/// <summary>
		/// 加载订单
		/// </summary>
		/// <param name="orders"></param>
		internal void LoadOrders(IEnumerable<OrderItem> orders)
		{
			SuspendLayout();
			BeginUpdate();

			Items.Clear();
			var items = orders.Select(s => new OrderTicketListViewItemGroup(s, this)).ToArray();
			Groups.AddRange(items.Select(s => s.Group).ToArray());
			Items.AddRange(items.SelectMany(s => s).Cast<ListViewItem>().ToArray());
			if (Items.Count > 0)
			{
				AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
			}
			else
			{
				AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			}

			EndUpdate();
			ResumeLayout();
		}

		/// <summary>
		/// 移除列表中的订单
		/// </summary>
		/// <param name="notComplete"></param>
		internal void ClearOrders(bool? notComplete)
		{
			SuspendLayout();
			var items = Items.Cast<OrderTicketListViewItemGroup>().Where(s => notComplete == null || (notComplete.Value ^ !s.Order.SequenceNo.IsNullOrEmpty()))
				.ToArray();
			items.ForEach(s =>
			{
				s.ForEach(Items.Remove);
				Groups.Remove(s.Group);
			});
			ResumeLayout();
		}

		public KeyValuePair<OrderItem, OrderTicket[]>? SelectedOrder
		{
			get
			{
				var items = CheckedItems.Cast<OrderTicketListViewItem>().ToArray();
				var order = items.Select(s => s.Group.Tag as OrderTicketListViewItemGroup).Distinct().ToArray();
				if (order.Length == 0)
					return null;

				if (order.Length > 1)
					return null;

				var o = order[0].Order;
				var ts = items.Select(s => s.Ticket).ToArray();

				return new KeyValuePair<OrderItem, OrderTicket[]>(o, ts);
			}
		}

		private void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// OrderList
			// 
			// 
			// 
			// 
			this.Border.Class = "ListViewBorder";
			this.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.FocusCuesEnabled = false;
			this.ResumeLayout(false);

		}
	}
}
