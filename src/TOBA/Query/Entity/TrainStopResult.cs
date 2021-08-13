namespace TOBA.Query.Entity
{
	using Newtonsoft.Json;

	using TOBA.Entity;

	internal class TrainStopResult : Dto
	{
		[JsonProperty("data")]
		public TrainStopCollection Data { get; set; }
	}
}