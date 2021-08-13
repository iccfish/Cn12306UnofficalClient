using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Query.Entity
{
	using TOBA.Entity;
	class TicketQueryRequestParamInfo : Dto
	{
		internal object LockObject = new object();

		/// <summary>
		/// 查询的URL
		/// </summary>
		public string Url { get; set; }

		public bool QueryFlag { get; set; }


		public bool EnableSaveLog { get; set; }


		public DateTime LastSaveLogTime { get; set; }
	}

}
