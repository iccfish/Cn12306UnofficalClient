using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Entity
{
	using Newtonsoft.Json;

	class BasicIdValueEntity<TKey, TValue> : Dto
	{
		[JsonProperty("id")]
		public TKey Id { get; set; }

		[JsonProperty("value")]
		public TValue Value { get; set; }
	}
}
