using System;

namespace TOBA.Query.Entity
{
	using TOBA.Entity;

	internal interface IQueryLeftTicketResponse
	{
		string QueryUrl { get; set; }

		QueryResult ToQueryResult(QueryParam query);

		/// <summary>
		/// 获得当前的响应中是否有数据
		/// </summary>
		bool HasData { get; }

		string GetErrorMessages(string defaultError);
	}
}
