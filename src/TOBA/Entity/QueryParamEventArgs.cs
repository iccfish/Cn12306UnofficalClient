using System;

namespace TOBA.Entity
{
	internal class QueryParamEventArgs : EventArgs
	{
		/// <summary>
		/// 获得事件关联的查询参数
		/// </summary>
		public QueryParam QueryParam { get; private set; }

		/// <summary>
		/// 创建 <see cref="QueryParamEventArgs" />  的新实例(QueryParamEventArgs)
		/// </summary>
		public QueryParamEventArgs(QueryParam queryParam)
		{
			QueryParam = queryParam;
		}
	}
}