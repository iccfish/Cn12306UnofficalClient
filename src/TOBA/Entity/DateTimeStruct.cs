using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Entity
{
	using Newtonsoft.Json;

	internal class DateTimeStruct
	{
		public int Year { get; set; }
		public int Month { get; set; }
		[JsonProperty("date")]
		public int Day { get; set; }
	}
}
