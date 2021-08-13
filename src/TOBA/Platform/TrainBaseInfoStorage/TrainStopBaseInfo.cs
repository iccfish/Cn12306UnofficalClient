namespace TOBA.Platform.TrainBaseInfoStorage
{
	using System.Collections.Generic;

	using Newtonsoft.Json;

	class TrainStopBaseInfo : BaseStat
	{
		[JsonProperty("e")]
		public Dictionary<string, TrainStop2StopInfo> StopBaseInfos { get; } = new Dictionary<string, TrainStop2StopInfo>();
	}
}