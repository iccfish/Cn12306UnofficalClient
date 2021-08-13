namespace TOBA.WebLib
{
	using System.Linq;

	using Entity;

	using Newtonsoft.Json;

	abstract class UamWebResponse : Dto
	{
		[JsonProperty("result_code")]
		public int Code { get; set; }

		[JsonProperty("result_message")]
		public string Message { get; set; }
	}
}
