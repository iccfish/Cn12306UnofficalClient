namespace TOBA.Query.Entity
{
	using System.Collections.Generic;

	using TOBA.Entity;

	internal class QueryLeftTicketResponseSimpleData : Dto
	{

		public Dictionary<string, string> Map { get; set; }

		public string Flag { get; set; }

		public List<string> Result { get; set; }

	}
}