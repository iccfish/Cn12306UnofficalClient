using System;
using System.Collections.Generic;

namespace TOBA
{
	using FSLib.Extension;

	using UI.Dialogs;

	internal class UiEvents
	{

		/// <summary>
		/// 请求获得所有的配置页面
		/// </summary>
		public static event EventHandler<GeneralEventArgs<List<OptionConfigForm.AbstractOptionConfigUI>>> GenerateOptionsTabs;

		/// <summary>
		/// 引发 <see cref="GenerateOptionsTabs" /> 事件
		/// </summary>
		/// <param name="sender">引发此事件的源对象</param>
		/// <param name="ea">包含此事件的参数</param>
		public static void OnGenerateOptionsTabs(object sender, GeneralEventArgs<List<OptionConfigForm.AbstractOptionConfigUI>> ea)
		{
			var handler = GenerateOptionsTabs;
			if (handler != null)
				handler(sender, ea);
		}

	}
}
