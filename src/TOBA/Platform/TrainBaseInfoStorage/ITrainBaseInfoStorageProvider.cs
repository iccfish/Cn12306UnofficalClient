namespace TOBA.Platform.TrainBaseInfoStorage
{
	using Query.Entity;

	interface ITrainBaseInfoStorageProvider
	{

		TrainBaseInfo this[string trainId] { get; }

		/// <summary>
		/// 查找指定车次的两个停靠站之间的信息
		/// </summary>
		/// <param name="trainId"></param>
		/// <param name="fromStop"></param>
		/// <param name="toStop"></param>
		/// <returns></returns>
		(TrainBaseInfo baseInfo, TrainStopBaseInfo stopInfo, TrainStop2StopInfo stop2stop) Find(string trainId, string fromStop, string toStop);
	}
}