using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI.Controls.Query.ResultSubItems
{
	using System.Drawing;
	using TOBA.Query.Entity;

	class TrainAlterFromStationName : SubItemBase
	{
		/// <summary>
		/// 创建 <see cref="TrainAlterFromStationName" />  的新实例(SubItemBase)
		/// </summary>
		public TrainAlterFromStationName(QueryResultItem resultItem, Font[] font)
			: base(resultItem, font)
		{
			Text = resultItem.FromStation.StationName + "\n" + resultItem.FromStation.DepartureTime.Value.ToString("HH:mm");
		}

		protected override void RefreshStyle()
		{
			ForeColor = Color.Gray;
		}


		public override int CompareTo(SubItemBase other)
		{
			return (int)(ResultItem.FromStation.DepartureTime.Value.TimeOfDay - other.ResultItem.FromStation.DepartureTime.Value.TimeOfDay).TotalSeconds;
		}

	}
}

