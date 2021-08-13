using System;
using System.Linq;
using System.Text;

namespace TOBA.Platform.TrainBaseInfoStorage
{
	using Query.Entity;

	class TrainBaseInfoStorageProvider : ITrainBaseInfoStorageProvider
	{
		public TrainBaseInfo this[string trainId]
		{
			get
			{
				var ret = trainId.IsNullOrEmpty() ? null : TrainBaseInfoStorage.Instance.TryGetValue(trainId, out var tmp) ? tmp : null;
				ret?.SetHit();
				return ret;
			}
		}

		/// <summary>
		/// 查找指定车次的两个停靠站之间的信息
		/// </summary>
		/// <param name="trainId"></param>
		/// <param name="fromStop"></param>
		/// <param name="toStop"></param>
		/// <returns></returns>
		public (TrainBaseInfo baseInfo, TrainStopBaseInfo stopInfo, TrainStop2StopInfo stop2stop) Find(string trainId, string fromStop, string toStop)
		{
			var train = this[trainId];
			var stop = train?.StopBaseInfos.GetValue(fromStop);
			stop?.SetHit();

			var stop2stop = stop?.StopBaseInfos.GetValue(toStop);
			stop2stop?.SetHit();

			return (train, stop, stop2stop);
		}
	}
}
