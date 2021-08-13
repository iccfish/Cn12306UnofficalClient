namespace TOBA.Order.Web
{
	using Newtonsoft.Json;

	internal class CancelOrderResult
	{
		[JsonProperty("cancelStatus")]
		public bool CancelStatus { get; set; }
	}
}