using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.BackupOrder.Entity
{
	using Newtonsoft.Json;

	using Otn.Entity;

	using TOBA.Entity;

	class QueryBackupOrderQueueResponse : BaseOtnApiResponseWithFlagAndMsg
	{

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("waitTime")]
		public int WaitTime { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("reserve_no")]
		public string ReserveNo { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("status")]
		public int Status { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("waitCount")]
		public int WaitCount { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("isAsync")]
		public bool IsAsync { get; set; }
	}


}
