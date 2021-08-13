using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace TOBA.Order
{
	using System.Threading;
	using TOBA.Configuration;
	using TOBA.Query.Entity;

	internal class RecentlySubmitFailedTokenStorageProvider
	{
		#region 单例模式

		static RecentlySubmitFailedTokenStorageProvider _instance;
		static readonly object _lockObject = new object();

		public static RecentlySubmitFailedTokenStorageProvider Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_lockObject)
					{
						if (_instance == null)
						{
							_instance = new RecentlySubmitFailedTokenStorageProvider();
						}
					}
				}

				return _instance;
			}
		}

		#endregion

		Dictionary<string, Queue<DateTime>> _cache;

		private RecentlySubmitFailedTokenStorageProvider()
		{
			_cache = new Dictionary<string, Queue<DateTime>>();

			ThreadPool.QueueUserWorkItem(_ =>
			{
				while (true)
				{
					if (OrderConfiguration.Instance.SubmitFailedNoTicketAutoDisable)
					{
						var now = DateTime.Now;
						var expiresTime = now.AddMinutes(-1 * OrderConfiguration.Instance.SubmitFailedNoTicketControlTime);
						lock (_cache)
						{
							foreach (var key in _cache.Keys.ToArray().Where(s =>
							{
								var queue = _cache[s];
								while (queue.Count > 0 && queue.Peek() < expiresTime)
								{
									queue.Dequeue();
								}
								return queue.Count == 0;
							})) { _cache.Remove(key); }
						}
					}
					Thread.Sleep(1000);
				}
			}, null);
		}

		string CreateTicketDataSign(QueryResultItem item)
		{
			return item.Id + item.TicketCount.OrderBy(s => s.Key).Select(s => s.Key + s.Value.Count).JoinAsString("");
		}

		public void AddDisableTicketData(QueryResultItem train)
		{
			var key = CreateTicketDataSign(train);

			lock (_cache)
			{
				if (_cache.ContainsKey(key))
				{
					_cache[key].Enqueue(DateTime.Now);
				}
				else
				{
					_cache.Add(key, new Queue<DateTime>(new[] { DateTime.Now }));
				}
			}
		}

		public bool IsTicketDataDisabledExists(QueryResultItem train)
		{
			var key = CreateTicketDataSign(train);
			Queue<DateTime> queue;

			if (!_cache.TryGetValue(key, out queue))
				return false;

			if (queue.Count >= OrderConfiguration.Instance.SubmitFailedNoTicketControlRate)
			{
				queue.Enqueue(DateTime.Now);
				return true;
			}

			return false;
		}
	}
}
