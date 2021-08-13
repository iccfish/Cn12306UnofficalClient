namespace TOBA.Order.Web
{
	using System.Linq;

	using Entity;

	using Newtonsoft.Json;

	using TOBA.Entity;

	internal class QueryMyOrderResult : Dto
	{
		[JsonProperty("order_total_number")]
		public string OrderTotalNumber { get; set; }

		[JsonProperty("OrderDTODataList")]
		public OrderItem[] List { get; set; }
	}
}
