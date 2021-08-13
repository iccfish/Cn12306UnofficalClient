namespace TOBA.Otn.Entity
{
	using Newtonsoft.Json;

	using TOBA.Entity;

	class BaseOtnApiResponseWithFlagAndMsg:Dto
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("flag")]
		public bool Flag { get; set; }

		[JsonProperty("msg")]
		public string Msg { get; set; }

	}
}