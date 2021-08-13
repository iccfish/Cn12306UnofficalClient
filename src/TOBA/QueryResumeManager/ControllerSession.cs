using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using FSLib;
using TOBA.Configuration;
using TOBA.Entity;
using TOBA.UI;

namespace TOBA.QueryResumeManager
{
	using FSLib.Extension;

	internal class ControllerSession : IOperation
	{
		AutoResumeRefreshConfiguration _config;
		readonly object _lockObject = new object();
		SourceSubmitContext _sharedSourceSubmitContext;
		Dictionary<IOrderSubmitEventSource, SourceSubmitContext> _sources = new Dictionary<IOrderSubmitEventSource, SourceSubmitContext>();



		/// <summary>
		/// 创建 <see cref="ControllerSession" />  的新实例(ControllerSession)
		/// </summary>
		/// <param name="session"></param>
		public ControllerSession(Session session)
		{
			Session = session;
			_config = AutoResumeRefreshConfiguration.Instance;
		}

		public static event EventHandler<GeneralEventArgs<string>> NotificationCreated;

		SourceSubmitContext CreateSourceSubmitContext(QueryParam query)
		{
			var context = new SourceSubmitContext();

			lock (_lockObject)
			{
				context.UsingSharedContext = QueryConfiguration.Current.StopQueryWhenFoundTicket;

				if (!context.UsingSharedContext)
					context.Queries = new List<QueryParam>() { query };
			}

			return context;
		}

		void EnsureCreateSharedSubmitContext(IOrderSubmitEventSource source)
		{
			lock (_lockObject)
			{
				if (_sharedSourceSubmitContext != null)
					return;

				_sharedSourceSubmitContext = new SourceSubmitContext();
				var queries = Session.UserProfile.QueryParams;
				//非停止状态/有票状态
				_sharedSourceSubmitContext.Queries = queries.Where(s => s.QueryState == QueryState.Query || s.QueryState == QueryState.Wait || s == source.Query).ToList();
			}
		}

		void LogMessage(string msg)
		{
			Events.OnMessage(this, new EventInfoArgs(msg));
			OnNotificationCreated(this, new GeneralEventArgs<string>(msg));
		}

		/// <summary>
		/// 引发 <see cref="NotificationCreated" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		/// <param name="ea">包含此事件的参数</param>
		static void OnNotificationCreated(object sender, GeneralEventArgs<string> ea)
		{
			var handler = NotificationCreated;
			if (handler != null)
				handler(sender, ea);
		}

		void source_AutoVcFailed(object sender, System.EventArgs e)
		{
			var source = sender as IOrderSubmitEventSource;
			if (source == null || !_sources.ContainsKey(source))
				return;

			var train = source.Train;
			var result = train.QueryResult;

			if (_config.AutoCloseSubmit && _config.AutoCloseSubmitIfAutoVcFailed)
			{
				_sources[source].OperationByAutoResume = true;
				LogMessage("提交失败： {0:MM-dd} {1} 次 {2} 至 {3} ，未能自动打码。".FormatWith(result.Date, train.Code, train.FromStation.StationName, train.ToStation.StationName));
				source.Cancel();
			}
		}

		void source_InitSubmitFailed(object sender, System.EventArgs e)
		{
			var source = sender as IOrderSubmitEventSource;
			if (source == null || !_sources.ContainsKey(source))
				return;

			var train = source.Train;
			var result = train.QueryResult;

			if (_config.AutoCloseSubmit && _config.AutoCloseSubmitIfNotSubmitable)
			{
				_sources[source].OperationByAutoResume = true;
				LogMessage("提交失败： {0:MM-dd} {1} 次 {2} 至 {3} ，{4}。".FormatWith(result.Date, source.Train.Code, source.Train.FromStation.StationName, source.Train.ToStation.StationName, source.Error));
				source.Cancel();
			}
		}
		void source_OperationPerformed(object sender, System.EventArgs e)
		{
			var source = sender as IOrderSubmitEventSource;
			if (source == null || !_sources.ContainsKey(source))
				return;

			lock (_lockObject)
			{
				if (!_sources.ContainsKey(source))
					return;

				var context = _sources[source];
				context.Performed = true;
			}
		}

		void source_QueueFailedElse(object sender, System.EventArgs e)
		{
			var source = sender as IOrderSubmitEventSource;
			if (source == null || !_sources.ContainsKey(source))
				return;

			var train = source.Train;
			var result = train.QueryResult;

			if (_config.AutoCloseSubmit && _config.AutoCloseSubmitIfQueueFailedElse)
			{
				_sources[source].OperationByAutoResume = true;
				LogMessage("提交失败： {0:MM-dd} {1} 次 {2} 至 {3} ，无法排队。".FormatWith(result.Date, source.Train.Code, source.Train.FromStation.StationName, source.Train.ToStation.StationName));
				source.Cancel();
			}
		}

		void source_QueueFailedNoTicket(object sender, System.EventArgs e)
		{
			var source = sender as IOrderSubmitEventSource;
			if (source == null || !_sources.ContainsKey(source))
				return;

			var train = source.Train;
			var result = train.QueryResult;

			if (_config.AutoCloseSubmit && _config.AutoCloseSubmitIfNoEnoughTicket)
			{
				_sources[source].OperationByAutoResume = true;
				LogMessage("提交失败： {0:MM-dd} {1} 次 {2} 至 {3} ，余票不足。".FormatWith(result.Date, source.Train.Code, source.Train.FromStation.StationName, source.Train.ToStation.StationName));
				source.Cancel();
			}
		}

		void source_SubmitClosed(object sender, System.EventArgs e)
		{
			var source = sender as IOrderSubmitEventSource;
			if (source == null || !_sources.ContainsKey(source))
				return;


			lock (_lockObject)
			{
				if (!_sources.ContainsKey(source))
					return;

				var context = _sources[source];
				_sources.Remove(source);

				//如果不是使用共享的context，则关闭后恢复对应的查询
				if (context.OperationByAutoResume && !context.IsSubmitSuccess)
				{
					if (!context.UsingSharedContext)
					{
						context.Queries.ForEach(s => s.OnRequestQuery(false));
					}
					//如果没有查询了，则恢复查询
					if (_sources.Count == 0)
					{
						_sharedSourceSubmitContext?.Queries.ForEach(s => s.OnRequestQuery(false));
						_sharedSourceSubmitContext = null;
					}
				}
			}
		}

		void source_SubmitFailed(object sender, System.EventArgs e)
		{
			var source = sender as IOrderSubmitEventSource;
			if (source == null || !_sources.ContainsKey(source))
				return;

			var train = source.Train;
			var result = train.QueryResult;

			if (_config.AutoCloseSubmit && _config.AutoCloseSubmitIfSubmitFailed)
			{
				_sources[source].OperationByAutoResume = true;
				LogMessage("订单 {0:MM-dd} {1} 次 {2} 至 {3} 无法提交，可能已经无票。".FormatWith(result.Date, source.Train.Code, source.Train.FromStation.StationName, source.Train.ToStation.StationName));
				source.Cancel();
			}
		}

		private void Source_UserEnterReady(object sender, EventArgs e)
		{
			var source = sender as IOrderSubmitEventSource;
			if (source == null || !_sources.ContainsKey(source))
				return;

			lock (_lockObject)
			{
				if (!_sources.ContainsKey(source))
					return;

				var context = _sources[source];
				context.CreateTime = DateTime.Now;
			}
		}

		/// <summary>
		/// 检测超时的提交
		/// </summary>
		internal void CheckTimeoutSubmit()
		{
			IOrderSubmitEventSource[] sourcesToCancel;

			lock (_lockObject)
			{
				sourcesToCancel = _sources.Where(source =>
				{
					var context = source.Value;
					if (context.Performed || context.CreateTime == null || (DateTime.Now - context.CreateTime.Value).TotalMinutes < _config.AutoCloseSubmitTimeout)
						return false;

					//已超时
					source.Value.OperationByAutoResume = true;
					return true;
				}).Select(s => s.Key).ToArray();
			}

			sourcesToCancel.ForEach(s =>
			{
				var train = s.Train;
				var result = train.QueryResult;

				LogMessage("订单提交 {0:MM-dd} {1} 次 {2} 至 {3} 未在设置的时间内操作。".FormatWith(result.Date, s.Train.Code, s.Train.FromStation.StationName, s.Train.ToStation.StationName));
				s.Cancel();
			});
		}

		public SourceSubmitContext GetContext(IOrderSubmitEventSource source)
		{
			return _sources.GetValue(source);
		}

		/// <summary>
		/// 注册
		/// </summary>
		/// <param name="source"></param>
		public void Register(IOrderSubmitEventSource source)
		{
			if (source == null || !_config.AutoCloseSubmit || !source.Query.EnableAutoPreSubmit)
				return;

			lock (_lockObject)
			{
				if (_sources.ContainsKey(source))
					return;

				source.AutoResumeAttached = true;
				var query = source.Query;
				var context = CreateSourceSubmitContext(query);
				_sources.Add(source, context);

				if (context.UsingSharedContext)
					EnsureCreateSharedSubmitContext(source);

				source.AutoVcFailed += source_AutoVcFailed;
				source.InitSubmitFailed += source_InitSubmitFailed;
				source.OperationPerformed += source_OperationPerformed;
				source.QueueFailedElse += source_QueueFailedElse;
				source.QueueFailedNoTicket += source_QueueFailedNoTicket;
				source.SubmitClosed += source_SubmitClosed;
				source.SubmitFailed += source_SubmitFailed;
				source.UserEnterReady += Source_UserEnterReady;

				var train = source.Train;
				var result = train.QueryResult;


				Events.OnMessage(this, new EventInfoArgs("正在提交订单 {0:MM-dd} {1} 次 {2} 至 {3} 。".FormatWith(result.Date, source.Train.Code, source.Train.FromStation.StationName, source.Train.ToStation.StationName)));
			}
		}

		#region Implementation of IOperation

		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		public Session Session
		{
			get;
			private set;
		}

		#endregion


	}
}