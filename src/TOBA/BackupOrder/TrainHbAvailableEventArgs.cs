namespace TOBA.BackupOrder
{
	using System;

	using Entity;

	using Query.Entity;

	class TrainHbAvailableEventArgs : EventArgs
	{
		/// <summary>初始化 <see cref="TrainHbAvailableEventArgs" /> 类的新实例。</summary>
		public TrainHbAvailableEventArgs(QueryResult result, QueryResultItem resultItem, LeftTicketData ticketData, HbQueueInfoCacheItem cacheItem)
		{
			Result = result;
			ResultItem = resultItem;
			TicketData = ticketData;
			CacheItem = cacheItem;
		}

		public QueryResult Result { get; }

		public QueryResultItem ResultItem { get; }

		public LeftTicketData TicketData { get; }


		public HbQueueInfoCacheItem CacheItem { get; }
	}
}