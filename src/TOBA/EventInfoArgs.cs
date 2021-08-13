using System;

namespace TOBA
{
	public class EventInfoArgs : EventArgs
	{
		public string Message { get; set; }

		public object Data { get; set; }

		/// <summary>
		/// 创建 <see cref="EventInfoArgs" /> 的新实例
		/// </summary>
		public EventInfoArgs()
		{
			Continue = true;
		}

		/// <summary>
		/// 创建 <see cref="EventInfoArgs" /> 的新实例
		/// </summary>
		public EventInfoArgs(string message, object data=null)
		{
			Message = message;
			Data = data;
			Continue = true;
		}

		/// <summary>
		/// 获得或设置是否继续
		/// </summary>
		public bool Continue { get; set; }
	}
}