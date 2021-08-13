using System;

namespace TOBA.Platform.TrainBaseInfoStorage
{
	using System.ComponentModel.Composition;

	using Autofac;

	using Extension;

	using Query;
	using Query.Entity;

	class TrainBaseInfoStorageExtensionModule : Module
	{

	}
	class TrainBaseInfoStorageExtension : AbstractExtension, IExtension
	{
		/// <summary>
		/// 插件ID
		/// </summary>
		public override string Id => "fishlee.net.trainbaseinfostorage";

		/// <summary>
		/// 连接插件
		/// </summary>
		public override void Connect()
		{
			base.Connect();
		}

		/// <summary>
		/// 断开连接
		/// </summary>
		public override void Disconnect()
		{
			base.Disconnect();
			TrainBaseInfoStorage.Instance.Save();
		}

		/// <summary>
		/// 当余票查询请求成功
		/// </summary>
		/// <param name="worker"></param>
		/// <param name="result"></param>
		public override void OnTicketQuerySuccess(TicketQueryWorker worker, QueryResult result)
		{
			base.OnTicketQuerySuccess(worker, result);

			//只处理第一次
			if (worker.Query.QueryCount >= 2)
				return;

			var storage = TrainBaseInfoStorage.Instance;
			lock (storage)
			{
				foreach (var item in result.OriginalList)
				{
					var train = storage.GetOrAdd(item.Id, _ =>
					{
						var info = new TrainBaseInfo()
						{
							From = item.StartStation.Code,
							To = item.EndStation.Code
						};
						if (item.FromStation.IsFirst)
							info.Departure = item.FromStation.DepartureTime?.TimeOfDay;
						if (item.ToStation.IsEnd)
							info.Arrive = item.ToStation.ArriveTime?.TimeOfDay;
						if (item.FromStation.IsFirst && item.ToStation.IsEnd)
							info.ElapsedTme = item.ElapsedTime;

						return info;
					});

					//发站
					var fromStop = train.StopBaseInfos.GetValue(item.FromStation.Code, _ => new TrainStopBaseInfo());
					var from2endStop = fromStop.StopBaseInfos.GetValue(item.ToStation.Code, _ => new TrainStop2StopInfo());
					from2endStop.ElapsedMinutes = (int)item.ElapsedTime.TotalMinutes;
				}
			}
		}
	}
}
