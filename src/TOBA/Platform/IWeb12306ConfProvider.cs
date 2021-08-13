namespace TOBA.Platform
{
	using System;
	using System.Threading.Tasks;

	interface IWeb12306ConfProvider
	{
		/// <summary>
		/// 获得当前配置
		/// </summary>
		IWeb12306Conf Current { get; }

		/// <summary>
		/// 刷新当前配置
		/// </summary>
		/// <returns></returns>
		Task<(bool success, string message)> RefreshAsync();

		/// <summary>
		/// 当前配置已更新
		/// </summary>
		event EventHandler CurrentConfUpdated;

		/// <summary>
		/// 刷新当前配置
		/// </summary>
		/// <returns></returns>
		Task<(bool success, string message, HttpConf.HttpConf conf)> UpdateAsync();
	}
}