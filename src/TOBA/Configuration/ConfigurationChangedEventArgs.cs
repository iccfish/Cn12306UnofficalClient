namespace TOBA.Configuration
{
	using System;

	internal class ConfigurationChangedEventArgs : EventArgs
	{
		/// <summary>
		/// 获得或设置属性名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 获得或设置原始值
		/// </summary>
		public object OrginalValue { get; set; }

		/// <summary>
		/// 获得或设置当前值
		/// </summary>
		public object CurrentValue { get; set; }

		/// <summary>
		/// 创建 <see cref="ConfigurationChangedEventArgs" />  的新实例(ConfigurationChangedEventArgs)
		/// </summary>
		public ConfigurationChangedEventArgs(string name, object orginalValue, object currentValue)
		{
			Name = name;
			OrginalValue = orginalValue;
			CurrentValue = currentValue;
		}
	}
}