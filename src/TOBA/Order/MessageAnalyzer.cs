using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Order
{
	internal class MessageAnalyzer
	{
		private static string[] _notEnoughTicket = new[]
		{
			"没有足够",
			"余票不足",
			"排队人数",
			"非法的席别",
			"实时余票无票"
		};

		/// <summary>
		/// 创建 <see cref="MessageAnalyzer" />  的新实例(MessageAnalyzer)
		/// </summary>
		/// <param name="message"></param>
		public MessageAnalyzer(string message)
		{
			Message = message.DefaultForEmpty("网络错误");

			NeedRetry = Message.IndexOf("网络错误") != -1 || Message.IndexOf("重试") != -1 || Message.IndexOf("繁忙") != -1 || Message.IndexOf("人数过多") != -1 || Message.IndexOf("系统忙") != -1;

			//无法获得TOKEN、数据错误、连接已关闭、非法的订票请求
			NeedRetry |= Message.IndexOf("非法的订票请求") != -1 || Message.IndexOf("数据无效") != -1 || Message.IndexOf("无法提交") != -1 || Message.IndexOf("无法获得") != -1 || Message.IndexOf("已经关闭") != -1;

			NeedRelogin = Message.IndexOf("未登录", StringComparison.OrdinalIgnoreCase) != -1 || message.IndexOf("重新登录", StringComparison.OrdinalIgnoreCase) != -1;
			CaptchaError = Message.IndexOf("验证码", StringComparison.OrdinalIgnoreCase) != -1;
			TicketNotEnough = _notEnoughTicket.Any(s => Message.IndexOf(s, StringComparison.OrdinalIgnoreCase) != -1);
			AccountProblem = Message.IndexOf("用户信息", StringComparison.OrdinalIgnoreCase) != -1 || Message.IndexOf("身份信息", StringComparison.OrdinalIgnoreCase) != -1;
			NeedOrderProcess = Message.IndexOf("未完成", StringComparison.OrdinalIgnoreCase) != -1 || Message.IndexOf("排队中", StringComparison.OrdinalIgnoreCase) != -1;
			TicketConflict = Message.IndexOf("您的证件", StringComparison.OrdinalIgnoreCase) != -1;
			DataExpired = Message.IndexOf("重新查询", StringComparison.OrdinalIgnoreCase) != -1;
			NeedVc = message.IndexOf("[NEEDVC]", StringComparison.OrdinalIgnoreCase) != -1;

			//自动回滚提交操作
			NeedRollbackOrderCommitMethod = Message.IndexOf("系统忙") != -1;

			ErrorElse = !new[] { NeedRetry, NeedOrderProcess, NeedRelogin, CaptchaError, AccountProblem, TicketConflict, TicketNotEnough, DataExpired }.Any();
		}


		public bool NeedRollbackOrderCommitMethod { get; private set; }

		/// <summary>
		/// 账户有问题
		/// </summary>
		public bool AccountProblem { get; private set; }

		/// <summary>
		/// 验证码错误
		/// </summary>
		public bool CaptchaError { get; private set; }

		/// <summary>
		/// 余票信息已过期
		/// </summary>
		public bool DataExpired { get; private set; }

		public bool ErrorElse { get; private set; }
		/// <summary>
		/// 获得错误信息
		/// </summary>
		public string Message { get; private set; }

		/// <summary>
		/// 有订单等待处理
		/// </summary>
		public bool NeedOrderProcess { get; private set; }

		/// <summary>
		/// 需要重新登录
		/// </summary>
		public bool NeedRelogin { get; private set; }

		/// <summary>
		/// 是否需要重试
		/// </summary>
		public bool NeedRetry { get; }

		/// <summary>
		/// 是否需要验证码
		/// </summary>
		public bool NeedVc { get; private set; }

		/// <summary>
		/// 车票冲突
		/// </summary>
		public bool TicketConflict { get; private set; }

		/// <summary>
		/// 票不足
		/// </summary>
		public bool TicketNotEnough { get; private set; }
	}
}
