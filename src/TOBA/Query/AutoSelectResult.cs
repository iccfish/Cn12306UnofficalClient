namespace TOBA.Query
{
	using Entity;

	using TOBA.Entity;
	using TOBA.Entity.Web;

	internal class AutoSelectResult
	{
		public QueryResultItem Train { get; private set; }

		public char Seat { get; private set; }

		public int Count { get; private set; }

		public PassengerInTicket[] Passengers { get; private set; }

		/// <summary>
		/// 创建 <see cref="AutoSelectResult" />  的新实例(AutoSelectResult)
		/// </summary>
		/// <param name="train"></param>
		/// <param name="seat"></param>
		/// <param name="count"></param>
		/// <param name="passengers"></param>
		public AutoSelectResult(QueryResultItem train, char seat, int count, PassengerInTicket[] passengers)
		{
			Train = train;
			Seat = seat;
			Count = count;
			Passengers = passengers;
		}
	}
}