using TOBA.UI.Dialogs.Misc;

namespace TOBA.Configuration
{
	/// <summary>
	/// 提供配置文件的加载和读取
	/// </summary>
	interface IConfigurationProvider
	{
		/// <summary>
		/// 重置所有设置
		/// </summary>
		void Reset();

		/// <summary>
		/// 获得配置的根目录
		/// </summary>
		string ProfileRoot { get; }

		/// <summary>
		/// 加载配置
		/// </summary>
		/// <typeparam name="T">配置的类型</typeparam>
		/// <param name="name">配置文件名</param>
		/// <param name="category">类别目录列表</param>
		/// <returns></returns>
		T LoadConfiguration<T>(string name = "main", params string[] category) where T : ConfigurationBase, new();

		/// <summary>
		/// 初始化服务提供类。仅供初始化调用，后续重复调用将会导致抛出异常。
		/// </summary>
		void Init(IStartup startup);
	}
}
