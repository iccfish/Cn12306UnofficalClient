using System.Linq;
using System.Text;

namespace TOBA.Query
{
	using System.Threading.Tasks;

	using Entity;

	using TOBA.Entity;

	interface ITicketPriceFillPrice
	{
		Task FillPriceAsync(Session session, QueryParam query, QueryResult result);
	}
}
