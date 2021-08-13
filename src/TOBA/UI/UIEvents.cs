using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI
{
	public class UIEvents
	{
		/// <summary>
		/// 请求更新状态统计
		/// </summary>
		public static event EventHandler<RequireUpdateStatisticsEventArgs> RequireUpdateStatistics;

		/// <summary>
		/// 引发 <see cref="RequireUpdateStatistics" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		/// <param name="ea">包含此事件的参数</param>
		public static void OnRequireUpdateStatistics(object sender, RequireUpdateStatisticsEventArgs ea)
		{
			var handler = RequireUpdateStatistics;
			if (handler != null)
				handler(sender, ea);
		}

	}
}
