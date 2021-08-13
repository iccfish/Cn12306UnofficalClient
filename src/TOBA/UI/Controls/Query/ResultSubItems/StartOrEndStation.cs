using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI.Controls.Query.ResultSubItems
{
	using System.Drawing;
	using TOBA.Query.Entity;

	class StartOrEndStation : SubItemBase
	{
		bool _start;

		/// <summary>
		/// 创建 <see cref="TrainToStationNameSubItem" />  的新实例(SubItemBase)
		/// </summary>
		public StartOrEndStation(QueryResultItem resultItem, Font[] font, bool start)
			: base(resultItem, font)
		{
			_start = start;
			if (resultItem == null || (start && resultItem.FromStation.IsFirst) || (!start && resultItem.ToStation.IsEnd))
				Text = "";
			else
			{
				if (start)
					Text = resultItem.StartStation.StationName;
				else
				{
					Text = resultItem.EndStation.StationName;
				}
			}

			ForeColor = Configuration.QueryViewConfiguration.Instance.StartEndStationColor;
		}

		public override int CompareTo(SubItemBase other)
		{
			if (_start)
				return StringComparer.OrdinalIgnoreCase.Compare(ResultItem.StartStation.StationName, other.ResultItem.StartStation.StationName);
			else
			{
				return StringComparer.OrdinalIgnoreCase.Compare(ResultItem.EndStation.StationName, other.ResultItem.EndStation.StationName);
			}
		}

	}
}
