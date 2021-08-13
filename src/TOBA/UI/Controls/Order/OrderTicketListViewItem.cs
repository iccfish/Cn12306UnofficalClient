using System;
using System.Linq;
using System.Text;

namespace TOBA.UI.Controls.Order
{
	using System.Drawing;
	using System.Windows.Forms;

	using Autofac;

	using DevComponents.DotNetBar.Controls;

	using Entity;
	using Entity.Web;


	using Platform.TrainBaseInfoStorage;

	using TOBA.Order.Entity;

	internal class OrderTicketListViewItem : ListViewItem
	{
		public OrderItem OrderItem { get; private set; }
		/// <summary>
		/// 获得关联的订单
		/// </summary>
		public OrderTicket Ticket { get; private set; }

		/// <summary>
		/// 创建 <see cref="OrderItem" />  的新实例(OrderItem)
		/// </summary>
		public OrderTicketListViewItem(OrderItem orderItem, OrderTicket ticket, ListViewEx owner)
		{
			OrderItem = orderItem;
			Ticket = ticket;

			InitSubItems(owner);
		}

		void InitSubItems(ListViewEx owner)
		{
			Font = owner.Font;
			UseItemStyleForSubItems = true;

			Text = Ticket.start_train_date_page.ToString("MM-dd HH:mm");

			//计算到达时间
			var (_, _, baseInfo) = AppContext.ExtensionManager.GlobalKernel.Resolve<ITrainBaseInfoStorageProvider>()
				.Find(
					Ticket.stationTrainDTO.TrainDto?.TrainNo,
					Ticket.stationTrainDTO.from_station_telecode,
					Ticket.stationTrainDTO.to_station_telecode);
			var elapsedMinute = baseInfo?.ElapsedMinutes;

			var arriveTime = Ticket.stationTrainDTO.arrive_time?.ToLongTimeString();
			if (elapsedMinute != null)
			{
				arriveTime = Ticket.start_train_date_page.AddMinutes(elapsedMinute.Value).ToString("MM-dd HH:mm");
			}

			//"抵达时间", "车次", "发站", "到站", "距离", "票种", "席别", "车厢", "座位", "票价", "乘客", "证件", "证件号码", "状态"
			var textArray = new[]
			{
				arriveTime?.PadRight(14, ' ')??"---",
				Ticket.stationTrainDTO.station_train_code,
				Ticket.stationTrainDTO.from_station_name,
				Ticket.stationTrainDTO.to_station_name,
				Ticket.stationTrainDTO.distance + "公里",
				Ticket.ticket_type_name,
				Ticket.seat_type_name,
				Ticket.coach_name + "车厢",
				Ticket.seat_name,
				"¥" + (Ticket.ticket_price / 100).ToString("#0.00  "),
				Ticket.Passenger.Name,
				Ticket.Passenger.IdTypeName,
				Ticket.Passenger.IdNo + "  ",
				Ticket.ticket_status_name
			};
			SubItems.AddRange(textArray);

			if (Ticket.OrderStatus == OrderStatus.NotPay || Ticket.OrderStatus == OrderStatus.ResignNotPaid)
			{
				SubItems[SubItems.Count - 1].Text += "(请在 " + Ticket.pay_limit_time.ToShortTimeString() + " 前支付)";
			}

			switch (Ticket.OrderStatus)
			{
				case OrderStatus.NotPay:
					ForeColor = System.Drawing.Color.Red;
					Font = new Font(Font, FontStyle.Bold);
					break;
				case OrderStatus.TicketPrinted:
					ForeColor = System.Drawing.Color.Gray;
					Font = new Font(Font, FontStyle.Strikeout);
					break;
				case OrderStatus.Used:
					ForeColor = System.Drawing.Color.Gray;
					Font = new Font(Font, FontStyle.Strikeout);
					break;
				case OrderStatus.Refunded:
					ForeColor = System.Drawing.Color.Gray;
					Font = new Font(Font, FontStyle.Strikeout);
					break;
				case OrderStatus.Resigned:
				case OrderStatus.ResignChagneTsed:
					ForeColor = System.Drawing.Color.Gray;
					Font = new Font(Font, FontStyle.Strikeout);
					break;
				case OrderStatus.ResignTicket:
				case OrderStatus.ResignChagneTsTicket:
					if (Ticket.start_train_date_page < DateTime.Now)
					{
						ForeColor = Color.Gray;
						Font = new Font(Font, FontStyle.Strikeout);
					}
					else
					{
						ForeColor = Ticket.OrderStatus == OrderStatus.ResignTicket ? Color.Firebrick : Color.BlueViolet;
						Font = new Font(Font, FontStyle.Bold);
					}
					break;
				case OrderStatus.ResignNotPaid:
				case OrderStatus.ResignChangeTsNotPaid:
					Font = new Font(Font, FontStyle.Bold);
					ForeColor = Color.MediumBlue;
					break;
				case OrderStatus.Queue:
					Font = new Font(Font, FontStyle.Bold);
					ForeColor = Color.FromArgb(0x00, 0x66, 0x99);
					break;
				case OrderStatus.BeResigned:
				case OrderStatus.ResignChangeTsIng:
					ForeColor = Color.SaddleBrown;
					break;
				case OrderStatus.Failed:
					Font = new Font(Font, FontStyle.Strikeout);
					ForeColor = System.Drawing.Color.Gray;
					break;
				case OrderStatus.Paid:
					if (Ticket.start_train_date_page < DateTime.Now)
					{
						ForeColor = Color.Gray;
						Font = new Font(Font, FontStyle.Strikeout);
					}
					else
					{
						ForeColor = Color.DarkGreen;
						Font = new Font(Font, FontStyle.Bold);
					}
					break;
				default:
					ForeColor = System.Drawing.Color.Gray;
					break;
			}


			UiUtility.ApplySubStyle(this);

		}

	}
}
