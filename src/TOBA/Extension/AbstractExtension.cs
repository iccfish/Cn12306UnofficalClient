namespace TOBA.Extension
{
	using Query;
	using Query.Entity;

	using System;

	abstract class AbstractExtension : IExtension
	{
		/// <summary>
		/// 断开连接
		/// </summary>
		public virtual void Disconnect()
		{
			Dispose();
		}

		/// <summary>
		/// 插件ID
		/// </summary>
		public abstract string Id { get; }

		/// <summary>
		/// 名称
		/// </summary>
		public virtual string Name { get; }

		/// <summary>
		/// 连接插件
		/// </summary>
		public virtual void Connect()
		{

		}

		public virtual void MainWindowInitialized() { }

		/// <summary>
		/// 请求对一个Session进行初始化
		/// </summary>
		/// <param name="session"></param>
		public virtual void SessionInit(Session session)
		{
		}

		/// <summary>
		/// 当余票查询请求成功
		/// </summary>
		/// <param name="worker"></param>
		/// <param name="result"></param>
		public virtual void OnTicketQuerySuccess(TicketQueryWorker worker, QueryResult result) { }

		#region Dispose方法实现

		bool _disposed;

		/// <summary>
		/// 释放资源
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed) return;
			_disposed = true;

			if (disposing)
			{
				//TODO 释放托管资源

			}
			//TODO 释放非托管资源

			//挂起终结器
			GC.SuppressFinalize(this);
		}

		/// <summary>
		/// 检查是否已经被销毁。如果被销毁，则抛出异常
		/// </summary>
		/// <exception cref="ObjectDisposedException">对象已被销毁</exception>
		protected void CheckDisposed()
		{
			if (_disposed)
				throw new ObjectDisposedException(this.GetType().Name);
		}


		#endregion


	}
}
