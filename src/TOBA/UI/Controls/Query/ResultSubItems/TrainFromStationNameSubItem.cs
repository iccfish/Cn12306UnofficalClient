using System.Drawing;

namespace TOBA.UI.Controls.Query.ResultSubItems
{

	using TOBA.Query.Entity;

	internal class TrainFromStationNameSubItem : SubItemBase
	{
		/// <summary>
		/// 创建 <see cref="TrainFromStationNameSubItem" />  的新实例(SubItemBase)
		/// </summary>
		public TrainFromStationNameSubItem(QueryResultItem resultItem, Font[] font)
			: base(resultItem, font)
		{
			if (resultItem != null)
			{
				Text = resultItem.FromStation.StationName + "\n" + resultItem.FromStation.DepartureTime.Value.ToString("HH:mm");
			}
			else
				Text = "数据\n无效";
			//var vi = new BetterListViewItem(resultItem.From.StationName + "\n" + resultItem.From.DepartureTime.Value.ToString("HH:mm"));

		}

		protected override void RefreshStyle()
		{
			if (ResultItem != null)
			{
				if (ResultItem.FromStation.Code != ResultItem.QueryParam?.FromStationCode)
					ForeColor = Configuration.QueryViewConfiguration.Instance.EndPointDifferentColor;
				else if (ResultItem.FromStation.IsFirst)
					ForeColor = Configuration.QueryViewConfiguration.Instance.EndPointTextColor;
			}
		}


		public override int CompareTo(SubItemBase other)
		{
			return (int)(ResultItem.FromStation.DepartureTime.Value - other.ResultItem.FromStation.DepartureTime.Value).TotalSeconds;
		}

	}
}
