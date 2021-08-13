namespace TOBA.UI.Components
{
	using System.Threading.Tasks;

	using Query.Entity;

	internal interface IBackupOrderUiOperation
	{
		Task<bool> AddToBackupOrderAsync(QueryResultItem train, LeftTicketData ticket);

		Task<bool> SubmitOrderRequestAsync();
	}
}