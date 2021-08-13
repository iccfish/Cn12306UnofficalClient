namespace TOBA.BackupOrder.Entity
{
	using Newtonsoft.Json;

	using Otn.Entity;

	using TOBA.Entity;

	class ConfirmBackupOrderResponseData : BaseOtnApiResponseWithFlagAndMsg
	{

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("trace_id")]
		public string TraceId { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isAsync")]
		public bool IsAsync { get; set; }
	}
}