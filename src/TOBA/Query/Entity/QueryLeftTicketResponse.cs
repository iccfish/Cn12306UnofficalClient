namespace TOBA.Query.Entity
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.InteropServices;

	using Newtonsoft.Json;

	using Otn.Entity;

	using TOBA.Entity;

	internal class QueryLeftTicketResponseComplex : OtnWebResponse<List<QueryLeftTicketResult>>, IQueryLeftTicketResponse
	{
		[JsonProperty("c_url")]
		public string QueryUrl { get; set; }

		/// <summary>
		/// 将OTN实体转换为程序使用的信息
		/// </summary>
		/// <returns>
		///
		/// </returns>
		public QueryResult ToQueryResult(QueryParam query)
		{
			var r = query.CreateQueryResult(Data?.Count ?? 0);
			Data?.ForEach(s => r.Add(s.ToQueryResultItem(r)));

			return r;
		}

		public bool HasData => Data != null;
	}
}
