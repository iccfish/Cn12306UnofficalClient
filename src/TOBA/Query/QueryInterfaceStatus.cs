using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Query
{
	using TOBA.Entity;
	class QueryInterfaceStatus : Dto
	{


		public long Code200Count { get; set; }

		public long Code302Count { get; set; }

		public long Code405Count { get; set; }
		public long Code502Count { get; set; }

		public long ConnectErrorCount { get; set; }

		public long DataEmptyCount { get; set; }

		public long DataExceptionCount { get; set; }

		public long EmptyReponseCount { get; set; }

		public long NetworkErrorCount { get; set; }

		public long QueryInterfaceChangeCount { get; set; }

		public long SuccessCount { get; set; }

		public long TimeoutCount { get; set; }

		public long TotalCount { get; set; }

		public long ResultFailedCount { get; set; }

		public long OtherFailedCount { get; set; }
	}
}
