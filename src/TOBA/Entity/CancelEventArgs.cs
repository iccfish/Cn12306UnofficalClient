namespace TOBA.Entity
{
	using System;

	/// <summary>
	/// 请求取消事件
	/// </summary>
	public class RequireCancelEventArgs : EventArgs
	{
		/// <summary>
		/// 获得或设置是否允许取消
		/// </summary>
		public bool CancelAccpted { get; set; }

		/// <summary>
		/// 创建 RequireCancelEventArgs class 的新实例
		/// </summary>
		public RequireCancelEventArgs()
		{
			this.CancelAccpted = true;
		}
	}
}
