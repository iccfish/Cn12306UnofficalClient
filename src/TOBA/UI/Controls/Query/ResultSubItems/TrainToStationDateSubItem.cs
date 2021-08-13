using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TOBA.Entity.Web;

namespace TOBA.UI.Controls.Query.ResultSubItems
{
	using TOBA.Query.Entity;

	internal class TrainToStationDateSubItem : SubItemBase
	{
		/// <summary>
		/// 创建 <see cref="TrainToStationDateSubItem"/>  的新实例(TrainToStationDateSubItem)
		/// </summary>
		public TrainToStationDateSubItem(QueryResultItem resultItem, Font[] font)
			: base(resultItem, font)
		{
			var (days, strinfo) = resultItem.ElapsedTimeInfo;
			if (days > 0)
			{
				Text = ("第" + (days + 1).ToString("#0") + "天");
			}
			else
			{
				Text = ("当天");
			}
			Text += "\n" + strinfo;
		}

		protected override void RefreshStyle()
		{

		}

		public override int CompareTo(SubItemBase other)
		{
			return (int)(ResultItem.ElapsedTime - other.ResultItem.ElapsedTime).TotalMinutes;
		}
	}
}
