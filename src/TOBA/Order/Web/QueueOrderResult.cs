namespace TOBA.Order.Web
{
	using System.Linq;

	using Otn.Entity;

	using TOBA.Entity;

	internal class QueueOrderResult : Dto
	{
		public bool queryOrderWaitTimeStatus { get; set; }
		public int count { get; set; }
		public int waitTime { get; set; }
		public long requestId { get; set; }
		public int waitCount { get; set; }
		public string tourFlag { get; set; }
		public string orderId { get; set; }

		public string msg { get; set; }

	}

	internal class QueueOrderResponse : OtnWebResponse<QueueOrderResult>
	{
		
	}
}
