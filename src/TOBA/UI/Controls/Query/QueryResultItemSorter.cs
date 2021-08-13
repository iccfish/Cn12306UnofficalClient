using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI.Controls.Query
{
	using System.ComponentModel;
	using System.Drawing;
	using System.Windows.Forms;

	using ComponentOwl.BetterListView;

	using Entity;

	using TOBA.Query.Entity;

	internal class QueryResultItemSorter : BetterListViewItemComparer
	{

		public QueryParam QueryParam { get; set; }

		/// <summary>
		/// Compare two sub-items.
		/// </summary>
		/// <param name="subItemA">First sub-item to compare.</param><param name="subItemB">Second sub-item to compare.</param><param name="sortMethod">Item comparison method.</param><param name="order">Sort order.</param>
		/// <returns>
		/// Comparison result.
		/// </returns>
		protected override int CompareSubItems(BetterListViewSubItem subItemA, BetterListViewSubItem subItemB, BetterListViewSortMethod sortMethod, int order)
		{
			var itema = subItemA.Item as QueryResultListViewItem;
			var itemb = subItemB.Item as QueryResultListViewItem;

			if (QueryParam != null)
			{
				if (QueryParam.EnableAutoPreSubmit && QueryParam.AutoPreSubmitConfig != null)
				{
					if (QueryParam.AutoPreSubmitConfig.SeatFirst)
					{
						var a = Compare(itema, itemb, s => s.ResultItem.Selected);
						if (a != 0)
							return a;
						a = Compare(itema, itemb, s => s.ResultItem.SeatSelected);
						if (a != 0)
							return a;
					}
					else
					{
						var a = Compare(itema, itemb, s => s.ResultItem.SeatSelected);
						if (a != 0)
							return a;
						a = Compare(itema, itemb, s => s.ResultItem.Selected);
						if (a != 0)
							return a;
					}
				}
			}

			var value = (subItemA as ResultSubItems.SubItemBase).CompareTo((subItemB as ResultSubItems.SubItemBase)) * order;
			return value;
		}

		int Compare<T>(T t1, T t2, Func<T, int> selector)
		{
			return CompareSelectLevel(selector(t1), selector(t2));
		}

		int CompareSelectLevel(int a, int b)
		{
			a = a == 0 ? int.MaxValue : a;
			b = b == 0 ? int.MaxValue : b;

			return a - b;
		}
	}
}

