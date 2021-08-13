using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.BackupOrder.Entity
{
	using Newtonsoft.Json;

	using TOBA.Entity;

	class SubmitBackupOrderResponse : Dto
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonProperty("flag")]
		public bool Flag { get; set; }

		/// <summary>
		/// 人脸识别错误码
		/// </summary>
		[JsonProperty("faceCheck")]
		public int? FaceCheck { get; set; }

	}
}
