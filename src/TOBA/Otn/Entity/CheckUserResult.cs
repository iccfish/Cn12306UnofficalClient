namespace TOBA.Otn.Entity
{
	using Newtonsoft.Json;

	internal class CheckUserResult
	{
		[JsonProperty("flag")]
		public bool Flag { get; set; }
	}
}