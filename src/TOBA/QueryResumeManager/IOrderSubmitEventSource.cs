using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TOBA.UI;

namespace TOBA.QueryResumeManager
{
	using Query.Entity;

	internal interface IOrderSubmitEventSource : IOperation
	{
		char Seat { get; }
		TOBA.Query.Entity.QueryResultItem Train { get; }
		/// <summary>
		/// 是否已经附加自动重刷控制器
		/// </summary>
		bool AutoResumeAttached { get; set; }

		TOBA.Entity.QueryParam Query { get; }

		/// <summary>
		/// 初始化提交失败
		/// </summary>
		event EventHandler InitSubmitFailed;

		/// <summary>
		/// 验证码自动识别失败
		/// </summary>
		event EventHandler AutoVcFailed;

		/// <summary>
		/// 提交失败
		/// </summary>
		event EventHandler SubmitFailed;

		/// <summary>
		/// 排队失败(无票)
		/// </summary>
		event EventHandler QueueFailedNoTicket;

		/// <summary>
		/// 因为其它原因提交失败
		/// </summary>
		event EventHandler QueueFailedElse;

		/// <summary>
		/// 开始触发人工操作
		/// </summary>
		event EventHandler OperationPerformed;

		/// <summary>
		/// 取消提交
		/// </summary>
		void Cancel();

		/// <summary>
		/// 已关闭
		/// </summary>
		event EventHandler SubmitClosed;

		/// <summary>
		/// 用户输入已就绪
		/// </summary>
		event EventHandler UserEnterReady;

		/// <summary>
		/// 错误信息
		/// </summary>
		string Error { get; set; }
	}
}
