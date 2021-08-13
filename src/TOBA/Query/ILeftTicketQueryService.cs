namespace TOBA.Query
{
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Entity;

	using TOBA.Entity;

	internal interface ILeftTicketQueryService
	{
		Task<Dictionary<string, QueryResultItem>> QueryLeftTicketAsync(QueryParam query);
	}
}