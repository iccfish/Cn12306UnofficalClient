using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI.Controls.Query.ResultSubItems
{
	using System.Drawing;
	using System.Text.RegularExpressions;
	using Entity.Web;

	using TOBA.Query.Entity;

	class TrainMemoSubItem : SubItemBase
	{
		/// <summary>
		/// 创建 <see cref="SubItemBase" />  的新实例(SubItemBase)
		/// </summary>
		public TrainMemoSubItem(QueryResultItem resultItem, Font[] font)
			: base(resultItem, font)
		{
			Font = font.Last();
			ForeColor = Color.Chocolate;

			var feature = new List<string>(3);
			if (!resultItem.HasAc)
				feature.Add("无空调");
			if (resultItem.SupportIdCard)
				feature.Add("可身份证进站");
			if (resultItem.CanExchangeByCredit)
			{
				feature.Add("可积分兑换");
			}

			//起售信息？
			if (!resultItem.IsAvailable && resultItem.BeginSellTime != null)
			{
				//查找高级软卧并显示
				var tip = resultItem.BeginSellTime.Value == DateTime.MinValue ? "不在预售期里啊.." : new[]
					{
						resultItem.BeginSellTime.Value.MakeDateFriendly(),
						resultItem.BeginSellTime.Value.Hour + "点",
						(resultItem.BeginSellTime.Value.Minute > 0 ? resultItem.BeginSellTime.Value.Minute + "分" : ""),
						"起售"
					}.Where(s => !string.IsNullOrEmpty(s)).JoinAsString("");

				feature.Add(tip);
			}
			if (!resultItem.IsAvailable && resultItem.IsLimitSell)
			{
				//暂售
				feature.Add(resultItem.ButtonTextInfo);
			}


			//
			Text = feature.JoinAsString("\n");
		}
	}
}
