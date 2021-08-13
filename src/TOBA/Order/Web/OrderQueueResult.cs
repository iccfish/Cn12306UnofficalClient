namespace TOBA.Order.Web
{
	using Newtonsoft.Json;

	using TOBA.Entity;

	internal class OrderQueueResult : Dto
	{
		[JsonProperty("submitStatus")]
		public bool SubmitStatus { get; set; }

		public string errMsg { get; set; }
	}
}