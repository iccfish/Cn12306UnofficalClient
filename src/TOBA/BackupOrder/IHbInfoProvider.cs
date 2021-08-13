using System;

namespace TOBA.BackupOrder
{
	using Query.Entity;

	interface IHbInfoProvider
	{
		event EventHandler<TrainHbAvailableEventArgs> TrainHbAvailable;

		void FillInfo(QueryResult result);
	}
}