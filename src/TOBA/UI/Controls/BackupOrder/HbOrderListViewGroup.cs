using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI.Controls.BackupOrder
{
	using System.Drawing;
	using System.Security.Cryptography.X509Certificates;
	using System.Windows.Forms;

	using TOBA.BackupOrder.Entity;

	class HbOrderListViewGroup
	{
		private readonly BackupOrderItem _order;
		private readonly UnpayBackupOrder _norder;
		private static Font _boldFont, _italicFont, _defaultFont;

		public UnpayBackupOrder NotCompleteOrder => _norder;

		public BackupOrderItem Order => _order;

		static HbOrderListViewGroup()
		{
			_defaultFont = new Font("微软雅黑", 12.0f, GraphicsUnit.Pixel);
			_boldFont = new Font(_defaultFont, FontStyle.Bold);
			_italicFont = new Font(_defaultFont, FontStyle.Italic);
		}

		public ListViewGroup Group { get; private set; }

		public List<ListViewItem> Items { get; private set; }

		public HbOrderListViewGroup(UnpayBackupOrder norder)
		{
			_norder = norder;
			CreateGroup();
		}

		public HbOrderListViewGroup(BackupOrderItem order)
		{
			_order = order;
			CreateGroup();
		}

		string GetText()
		{
			var order = _norder?.Order ?? _order;

			var text = $"{order.StatusName} / 单号 {order.ReserveNo} / {order.Passengers.Select(x => x.DisplayTitle).JoinAsString("、")} / 金额 {order.PrepayAmount:C} / 下单 {order.ReserveTime.ToLongDateString()} / 兑现截止 {order.RealizeLimitTime:F}";
			if (order.StatusCode == 1)
			{
				//待支付
				text += $" / 支付时限 {_norder.LoseTime.ToLongTimeString()}";
			}

			if (order.RefundDiffFlag)
			{
				text += " / 有退款";
			}

			if (order.AcceptTmpTrain)
			{
				text += $" / 接受临客 {order.AcceptTmpTrainName.Replace('#', ' ')}";
			}

			return text;
		}

		void CreateGroup()
		{
			Group = new ListViewGroup(GetText()) { Tag = this };
			CreateItems();
		}

		void CreateItems()
		{
			var order = _norder?.Order ?? _order;

			var tmpTrainName = order.RealizeTmpTrain ? order.AcceptTmpTrainName.Replace('#', ' ') : "";

			Items = new List<ListViewItem>(order.Needs.Count);
			var appendix = "    ";

			foreach (var need in order.Needs)
			{
				var status = need.StatusName;
				if (need.StatusCode == 7)
				{
					var type = order.RealizeTmpTrain ? "临客" + tmpTrainName : "";
					status = need.BatchStatus == 1 ? $"兑现{type}成功" : $"兑现{type}无效";
				}
				var item = new ListViewItem(status + appendix, Group)
				{
					Tag = need,
					Group = Group
				};
				var startTime = need.TrainDate.Add(need.StartTime);
				var arriveTime = need.ArriveDate.Add(need.ArriveTime);
				var elapsedTime = arriveTime - startTime;
				//信息
				item.SubItems.AddRange(new[]
				{
					need.TrainDate.ToLongDateString() + appendix,
					need.BoardTrainCode + appendix,
					need.FromStationName + appendix,
					need.ToStationName + appendix,
					startTime.ToString("F") + appendix,
					arriveTime.ToString("F") + appendix,
					elapsedTime.ToFriendlyDisplay() + appendix,
					need.SeatName + appendix,
					(need.RealizeFailInfo ?? "").GetSubString(30)
				});
				item.ToolTipText = need.RealizeFailInfo ?? "";

				//图标
				if (need.StatusCode == 1)
				{
					item.ImageKey = BackupOrderContainer.ICON_NOT_PAY;
					item.ForeColor = Color.Crimson;
					item.Font = _boldFont;
				}
				else if (need.StatusCode == 7 && need.BatchStatus == 1)
				{
					item.ImageKey = BackupOrderContainer.ICON_SUCCEED;
					item.ForeColor = Color.ForestGreen;
					item.Font = _boldFont;
				}
				else if (need.StatusCode == 5 || need.StatusCode == 6 || need.StatusCode == 8 || (need.StatusCode == 7 && need.BatchStatus != 1))
				{
					item.ImageKey = BackupOrderContainer.ICON_FAILED;
					item.ForeColor = Color.Gray;
					item.Font = _italicFont;
				}
				else
				{
					item.ImageKey = BackupOrderContainer.ICON_WAIT;
					item.ForeColor = Color.RoyalBlue;
				}

				item.UseItemStyleForSubItems = true;
				foreach (ListViewItem.ListViewSubItem subItem in item.SubItems)
				{
					subItem.Font = item.Font;
					subItem.ForeColor = item.ForeColor;
					subItem.BackColor = item.BackColor;
				}

				Items.Add(item);
			}
		}
	}
}
