namespace TOBA.Platform.TrainBaseInfoStorage
{
	using System.Collections.Generic;

	using Newtonsoft.Json;

	class TrainStop2StopInfo: BaseStat
	{
		[JsonProperty("m")]
		public int ElapsedMinutes { get; set; }

		[JsonProperty("p")]
		public Dictionary<char, double> Price { get; set; }
	}
}