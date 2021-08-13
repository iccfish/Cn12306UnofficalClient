using System.Drawing;

namespace TOBA.UI.Controls.Query.ResultSubItems
{
	using TOBA.Query.Entity;

	internal class TrainToStationNameSubItem : SubItemBase
	{
		/// <summary>
		/// 创建 <see cref="TrainToStationNameSubItem" />  的新实例(SubItemBase)
		/// </summary>
		public TrainToStationNameSubItem(QueryResultItem resultItem, Font[] font)
			: base(resultItem, font)
		{
			if (resultItem != null)
				Text = resultItem.ToStation.StationName + "\n" + resultItem.ToStation.ArriveTime.Value.ToString("HH:mm");
			else
				Text = "数据\n无效";
		}

		protected override void RefreshStyle()
		{
			if (ResultItem != null)
			{
				if (ResultItem.ToStation.Code != ResultItem.QueryParam?.ToStationCode)
					ForeColor = Configuration.QueryViewConfiguration.Instance.EndPointDifferentColor;
				else if (ResultItem.ToStation.IsEnd)
					ForeColor = Configuration.QueryViewConfiguration.Instance.EndPointTextColor;
			}
		}


		public override int CompareTo(SubItemBase other)
		{
			return (int)(ResultItem.ToStation.ArriveTime.Value.TimeOfDay - other.ResultItem.ToStation.ArriveTime.Value.TimeOfDay).TotalSeconds;
		}

	}
}
