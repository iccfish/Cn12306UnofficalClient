using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Entity
{
	internal class DynamicJsData
	{
		public string Id { get; set; }

		public string PingUrl { get; set; }

		public string SourceUrl { get; set; }

		public string Code { get; set; }

		public string Key { get; set; }

		public string EncodedValue { get; set; }

		public string ValidateData { get; set; }

		public bool NeedImmediatePingback { get; set; }
	}
}
