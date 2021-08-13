namespace TOBA
{
	using System.Linq;

	/// <summary>
	/// 需要进行上下文初始化的操作
	/// </summary>
	internal interface IOperation
	{
		/// <summary>
		/// 获得或设置上下文环境
		/// </summary>
		Session Session { get; }
	}

	internal interface IRequireSessionInit : IOperation
	{
		/// <summary>
		/// 执行初始化
		/// </summary>
		/// <param name="session"></param>
		void InitSession(Session session);
	}
}
