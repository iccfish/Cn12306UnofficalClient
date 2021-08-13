namespace TOBA.Query
{
	using System.Threading.Tasks;

	using UI;

	internal interface ITicketOrderInitializerService : IOperation
	{
		/// <summary>
		/// 执行异步初始化
		/// </summary>
		/// <returns></returns>
		Task<bool> InitializeAsync();
	}
}