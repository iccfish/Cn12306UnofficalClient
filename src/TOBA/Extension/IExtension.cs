namespace TOBA.Extension
{
	using System;

	using Query;
	using Query.Entity;

	/// <summary>
	/// 插件基类
	/// </summary>
	interface IExtension : IDisposable
	{
		/// <summary>
		/// 断开连接
		/// </summary>
		void Disconnect();

		/// <summary>
		/// 插件ID
		/// </summary>
		string Id { get; }

		/// <summary>
		/// 名称
		/// </summary>
		string Name { get; }

		/// <summary>
		/// 连接插件
		/// </summary>
		void Connect();

		/// <summary>
		/// 主窗口已经初始化
		/// </summary>
		void MainWindowInitialized();

		/// <summary>
		/// 请求对一个Session进行初始化
		/// </summary>
		/// <param name="session"></param>
		void SessionInit(Session session);

		/// <summary>
		/// 当余票查询请求成功
		/// </summary>
		/// <param name="worker"></param>
		/// <param name="result"></param>
		void OnTicketQuerySuccess(TicketQueryWorker worker, QueryResult result);
	}
}
