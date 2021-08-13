namespace TOBA.Query
{
	using System.Threading.Tasks;

	using Entity;

	using TOBA.Entity;

	internal interface IFillLeftTicketService
	{
		/// <summary>
		/// 填充结果
		/// </summary>
		/// <param name="query"></param>
		/// <param name="result"></param>
		Task FillAsync(QueryParam query, QueryResult result);

		Task Init(QueryParam query);
	}
}