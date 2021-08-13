namespace TOBA.Entity
{
	/// <summary>
	/// 查询状态
	/// </summary>
	public enum QueryState
	{
		/// <summary>
		/// 停止
		/// </summary>
		None,
		/// <summary>
		/// 正在查询
		/// </summary>
		Query,
		/// <summary>
		/// 等待中
		/// </summary>
		Wait
	}
}