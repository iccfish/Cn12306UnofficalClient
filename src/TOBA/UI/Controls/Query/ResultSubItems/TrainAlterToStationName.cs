using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI.Controls.Query.ResultSubItems
{
	using System.Drawing;

	using TOBA.Query.Entity;
	class TrainAlterToStationName : SubItemBase
	{
		/// <summary>
		/// 创建 <see cref="TrainAlterToStationName" />  的新实例(SubItemBase)
		/// </summary>
		public TrainAlterToStationName(QueryResultItem resultItem, Font[] font)
			: base(resultItem, font)
		{
			Text = resultItem.ToStation.StationName + "\n" + resultItem.ToStation.ArriveTime.Value.ToString("HH:mm");
		}

		protected override void RefreshStyle()
		{
			ForeColor = Color.Gray;
		}


		public override int CompareTo(SubItemBase other)
		{
			return (int)(ResultItem.ToStation.ArriveTime.Value.TimeOfDay - other.ResultItem.ToStation.ArriveTime.Value.TimeOfDay).TotalSeconds;
		}

	}
}
