using System;
using System.Collections.Generic;
using System.Linq;

/*
 * 缓存KEY=日期+车ID+席别
 * 缓存时间为最后使用60分钟内
 *
 */


namespace TOBA.BackupOrder
{
	using Autofac;

	using Configuration;

	using Entity;

	using Query.Entity;

	using System.Collections.Concurrent;
	using System.Diagnostics;
	using System.Threading;

	class HbInfoProvider : IHbInfoProvider
	{
		static readonly Dictionary<string, HbQueueInfoCacheItem> _cache = new Dictionary<string, HbQueueInfoCacheItem>();
		private static Timer _cleanTimer;
		static readonly ReaderWriterLockSlim _locker = new ReaderWriterLockSlim();
		private readonly ILifetimeScope _scope;
		private readonly Session _session;

		static HbInfoProvider()
		{
			_cleanTimer = new Timer(_ => { CleanUp(); }, null, 0, 1000 * 120);
		}

		public HbInfoProvider(Session session, ILifetimeScope scope)
		{
			_session = session;
			_scope = scope;
		}

		public event EventHandler<TrainHbAvailableEventArgs> TrainHbAvailable;

		static void CleanUp()
		{
			var minTime = DateTime.Now.AddHours(-1);
			_locker.EnterWriteLock();
			_cache.Keys.Where(s => _cache[s].LastHitTime < minTime).ToArray().ForEach(s => _cache.Remove(s));
			_locker.ExitWriteLock();
			Debug.WriteLine($"{nameof(HbInfoProvider)} has done cleanup caches.");
		}

		static string GetPrefix(QueryResultItem item)
		{
			return $"{item.QueryResult.Date:yyyyMMdd}{item.Id}";
		}

		protected virtual void OnTrainHbAvailable(TrainHbAvailableEventArgs e)
		{
			TrainHbAvailable?.Invoke(this, e);
		}

		public void FillInfo(QueryResult result)
		{
			if (_session.FaceCheckStatus == null || !FuncConfiguration.Instance.EnableHbStatusAutoCheck)
				return;

			var qc = QueryConfiguration.Current;
			if (!qc.DetectAllHbQueue && !qc.AutoDetectHbQueue)
				return;

			var minTimeSelected = DateTime.Now.AddSeconds(-qc.HbQueueInSelectedTrainsInterval);
			var minTimeNoSelected = DateTime.Now.AddSeconds(-qc.HbQueueInNonSelectedTrainsInterval);

			_locker.EnterReadLock();
			foreach (var train in result)
			{
				if (!train.AllowBackup)
					continue;

				//如果没有人脸认证，则直接设置
				if (_session.FaceCheckStatus == false)
				{
					foreach (var pair in train.TicketCount.Where(s => s.Value.NoTicket && s.Key != '*' && s.Key != '0'))
					{
						pair.Value.HbInfo = "未验证";
						pair.Value.HbLevel = -1;
					}

					continue;
				}

				//是否不更新？
				if (train.Selected == 0 && !qc.DetectAllHbQueue)
					continue;
				if (train.Selected > 0 && !qc.AutoDetectHbQueue)
					continue;

				var key = GetPrefix(train);

				foreach (var (code, data) in train.TicketCount)
				{
					//不需要？
					if ((data.NotNeed && !qc.DetectAllHbQueue) || (!data.NotNeed && !qc.AutoDetectHbQueue))
						continue;
					//不可候补？
					if (!data.NoTicket || code == '*' || code == '0')
						continue;

					var skey = key + code;
					var cacheItem = _cache.GetValue(skey);
					if (cacheItem != null)
					{
						data.HbLevel = cacheItem.Level;
						data.HbInfo = cacheItem.Info;
						cacheItem.LastHitTime = DateTime.Now;
					}
					//是否要更新？
					var limitTime = (train.Selected > 0 && !data.NotNeed ? minTimeSelected : minTimeNoSelected);
#if DEBUG
					if (cacheItem != null)
					{
						Debug.WriteLine($"{train.Code} - {data.Code} UpdateTime- {cacheItem.UpdateTime} - LimitTime- {limitTime}");
					}
#endif
					if (cacheItem == null || cacheItem.UpdateTime < limitTime)
					{
						_queue.Enqueue((_session, result, train, data));
					}
				}
			}
			_locker.ExitReadLock();
			LaunchUpdate();
		}

		#region 更新

		static readonly ConcurrentQueue<(Session session, QueryResult result, QueryResultItem item, LeftTicketData seat)> _queue = new ConcurrentQueue<(Session session, QueryResult result, QueryResultItem item, LeftTicketData seat)>();
		private static bool _inupdate;
		private static Random _random = new Random();

		static void LaunchUpdate()
		{
			if (_inupdate || _queue.Count == 0)
				return;

			_inupdate = true;
			ThreadPool.QueueUserWorkItem(_ => ProcessUpdateQueue());
		}

		static void ProcessUpdateQueue()
		{
			var qc = QueryConfiguration.Current;

			_locker.EnterUpgradeableReadLock();
			while (_queue.TryDequeue(out var data))
			{
				Thread.Sleep(FuncConfiguration.Instance.HbStateQueryInterval);
				if (!FuncConfiguration.Instance.EnableHbStatusAutoCheck)
					continue;

				var (session, result, item, seat) = data;
				var key = GetPrefix(item) + seat.Code;

				var current = _cache.GetValue(key);
				if (current != null && current.UpdateTime >= DateTime.Now.AddSeconds(current.Selected ? -qc.HbQueueInSelectedTrainsInterval : -qc.HbQueueInNonSelectedTrainsInterval))
					continue;

				//更新
				var (level, msg) = session.GetService<IBackupOrderService>().GetSuccessRateAsync(item.SubmitOrderInfo, seat.Code).Result;
				if (level == 0)
				{
					//出错
					continue;
				}

				seat.HbInfo = msg;
				seat.HbLevel = level;
				//是否变更？
				if (current == null)
				{
					current = new HbQueueInfoCacheItem();
					_cache.Add(key, current);
				}
				//变更通知
				var changeToAvailable = current.Level == 4 && level < 4;
				current.Update(level, msg, item.Selected > 0 && !seat.NotNeed);
				if (changeToAvailable)
				{
					var e = new TrainHbAvailableEventArgs(result, item, seat, current);
					(session.GetService<IHbInfoProvider>() as HbInfoProvider)?.OnTrainHbAvailable(e);
				}
				Thread.Sleep(FuncConfiguration.Instance.HbStateQueryInterval + _random.Next(1000));
				if (!FuncConfiguration.Instance.EnableHbStatusAutoCheck)
					break;
			}
			_locker.ExitUpgradeableReadLock();

			_inupdate = false;
		}

		#endregion
	}
}
