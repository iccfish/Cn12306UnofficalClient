using System;
using System.Collections.Generic;
using TOBA.Entity;

namespace TOBA.QueryResumeManager
{
	internal class SourceSubmitContext
	{
		public List<QueryParam> Queries { get; set; }

		public bool Performed { get; set; }

		public bool UsingSharedContext { get; set; }

		public DateTime? CreateTime { get; set; }

		public bool OperationByAutoResume { get; set; }

		/// <summary>
		/// 获得或设置是否提交成功
		/// </summary>
		public bool IsSubmitSuccess { get; set; }

		/// <summary>
		/// 创建 <see cref="SourceSubmitContext" />  的新实例(SourceSubmitContext)
		/// </summary>
		public SourceSubmitContext()
		{
		}
	}
}