namespace TOBA.Order.Entity
{
	using System;

	using TOBA.Entity;

	internal class OrderCacheItem : Dto
	{
		public string requestId { get; set; }
		public string userId { get; set; }
		public int number { get; set; }
		public string tourFlag { get; set; }
		public DateTime requestTime { get; set; }
		public long queueOffset { get; set; }
		public string queueName { get; set; }
		public DateTime trainDate { get; set; }
		public DateTime startTime { get; set; }
		public string stationTrainCode { get; set; }
		public string fromStationCode { get; set; }
		public string fromStationName { get; set; }
		public string toStationCode { get; set; }
		public string toStationName { get; set; }
		public int status { get; set; }
		public DateTime modifyTime { get; set; }
		public OrderCacheTicketItem[] tickets { get; set; }
		public int waitTime { get; set; }
		public int waitCount { get; set; }
		public int ticketCount { get; set; }
		public string startTimeString { get; set; }
		public string[] array_passser_name_page { get; set; }
	}


	internal class OrderCacheTicketItem : Dto
	{
		public string seatTypeName { get; set; }
		public string seatTypeCode { get; set; }
		public string ticketTypeName { get; set; }
		public string passengerName { get; set; }
		public string passengerIdTypeName { get; set; }
	}

}