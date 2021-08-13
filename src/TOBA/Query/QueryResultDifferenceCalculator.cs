using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Query
{
	using System.Drawing;
	using System.Runtime.InteropServices;
	using System.Threading.Tasks;
	using TOBA.Query.Entity;

	internal class QueryResultDifferenceCalculator
	{
		/// <summary>
		/// 计算序列差集
		/// </summary>
		/// <param name="current"></param>
		/// <param name="previous"></param>
		/// <returns></returns>
		public static Task<IEnumerable<QueryResultItem>> CalculateAsync(Dictionary<string, QueryResultItem> current, Dictionary<string, QueryResultItem> previous)
		{
			return Task<IEnumerable<QueryResultItem>>.Factory.StartNew(() => Calculate(current, previous));
		}

		/// <summary>
		/// 计算序列差集
		/// </summary>
		/// <param name="current"></param>
		/// <param name="previous"></param>
		/// <returns></returns>
		public static IEnumerable<QueryResultItem> Calculate(Dictionary<string, QueryResultItem> current, Dictionary<string, QueryResultItem> previous)
		{
			previous = previous ?? new Dictionary<string, QueryResultItem>();

			var prevEmpty = previous.Count == 0;

			//新增的
			var diffAdd = current.Keys.Where(s => !previous.ContainsKey(s)).Select(s =>
			{
				var item = current[s];
				if (prevEmpty)
					return item;

				item.TicketCount.Values.ForEach(x =>
				{
					x.MemoText = "+" + x.Count;
					x.MemoTextColorName = KnownColor.Gray;
				});
				return item;
			});

			//消失的
			var diffRemoved = previous.Keys.Where(s => !current.ContainsKey(s)).Select(s =>
			{
				var nitem = previous[s].RawResult.ToQueryResultItem(previous[s].QueryResult);
				nitem.TicketCount.Values.ForEach(x => x.Count = 0);

				nitem.TicketCompareTo(previous[s]);
				return nitem;
			});

			//变更的
			var diffChanged = previous.Keys.Intersect(current.Keys).Select(s =>
			{
				var item = current[s];
				return item.TicketCompareTo(previous[s]) ? item : null;
			}).ExceptNull();

			return diffAdd.Union(diffChanged).Union(diffRemoved).ToArray();
		}

	}
}
