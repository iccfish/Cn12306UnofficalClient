namespace TOBA.Query
{
	using System;
	using System.ComponentModel;
	using System.Linq;
	using System.Threading;

	using Entity;

	using FSLib.Network.Http;

	/// <summary>
	/// 查询列车停靠站信息
	/// </summary>
	internal class QueryTrainStopInfoWorker
	{
		/// <summary>
		/// 获得或设置会话
		/// </summary>
		public Session Session { get; set; }

		/// <summary>
		/// 获得查询的结果
		/// </summary>
		public TrainStopCollection Result { get; private set; }

		/// <summary>
		/// 获得或设置错误数据
		/// </summary>
		public object Error { get; private set; }

		/// <summary>
		/// 获得或设置是否成功
		/// </summary>
		public bool Success { get; private set; }

		AsyncOperation _operation;

		/// <summary>
		/// 返回当前是否正在忙
		/// </summary>
		public bool IsBusy
		{
			get { return _operation != null; }
		}

		public event EventHandler LoadComplete;

		/// <summary>
		/// 引发 <see cref="LoadComplete" /> 事件
		/// </summary>
		protected virtual void OnLoadComplete()
		{
			_operation = null;
			var handler = LoadComplete;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		public event EventHandler LoadFailed;

		/// <summary>
		/// 引发 <see cref="LoadFailed" /> 事件
		/// </summary>
		protected virtual void OnLoadFailed()
		{
			_operation = null;
			var handler = LoadFailed;
			if (handler != null)
				handler(this, EventArgs.Empty);
		}

		/// <summary>
		/// 查询车次停靠站信息
		/// </summary>
		/// <param name="train"></param>
		/// <param name="fromStatCode"></param>
		/// <param name="toStatCode"></param>
		/// <param name="depDate"></param>
		public void Query(QueryResultItem train)
		{
			if (IsBusy)
				return;

			_operation = AsyncOperationManager.CreateOperation(null);
			ThreadPool.QueueUserWorkItem(_ => QueryInternal(train));
		}

		/// <summary>
		/// 查询车次停靠站信息
		/// </summary>
		/// <param name="train"></param>
		public void QueryInternal(QueryResultItem train)
		{
			var trainid = train.Id;
			var fromStatCode = train.FromStation.Code;
			var toStatCode = train.ToStation.Code;
			var depDate = train.FromStation.DepartureTime.Value;
			var firstStationTime = train.StartStation.DepartureTime;

			var data = new
			{
				train_no = trainid,
				from_station_telecode = fromStatCode,
				to_station_telecode = toStatCode,
				depart_date = depDate.ToString("yyyy-MM-dd")
			};

			var task = Session.NetClient.Create<TrainStopResponse>(HttpMethod.Get, "czxx/queryByTrainNo",
																	"leftTicket/init", data).Send();
			if (task == null || !task.IsSuccess || task.Result?.Data?.Data == null)
			{
				Error = task?.Exception;
				Success = false;

				if (_operation == null)
					OnLoadFailed();
				else
					_operation.PostOperationCompleted(_ => OnLoadFailed(), null);
			}
			else
			{
				Result = task.Result.Data.Data;
				Result.ApplyTime(train, firstStationTime, depDate);

				Success = true;
				if (_operation == null)
					OnLoadComplete();
				else
					_operation.PostOperationCompleted(_ => OnLoadComplete(), null);
			}

		}
	}
}
