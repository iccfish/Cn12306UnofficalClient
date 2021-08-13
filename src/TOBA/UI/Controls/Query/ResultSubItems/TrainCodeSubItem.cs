using System.Drawing;

namespace TOBA.UI.Controls.Query.ResultSubItems
{

	using TOBA.Query.Entity;

	internal class TrainCodeSubItem : SubItemBase
	{
		/// <summary>
		/// 创建 <see cref="SubItemBase" />  的新实例(SubItemBase)
		/// </summary>
		public TrainCodeSubItem(QueryResultItem resultItem, Font[] font)
			: base(resultItem, font)
		{
			//Font = new System.Drawing.Font(font.FontFamily, 12.0F, System.Drawing.FontStyle.Bold);
		}

		/// <summary>
		/// 刷新单元格显示
		/// </summary>
		protected override void RefreshContent()
		{
			base.RefreshContent();
			Text = ResultItem.Code;
		}

		protected override void RefreshStyle()
		{
			base.RefreshStyle();

			if (ResultItem.NoTicketNeeded)
			{
				Font = Fonts[8];
			}
			else if (!ResultItem.IsAvailable)
			{
				Font = ResultItem.BeginSellTime == null ? Fonts[6] : Fonts[7];
			}
			else
			{
				Font = Fonts[5];
			}
		}
	}
}
