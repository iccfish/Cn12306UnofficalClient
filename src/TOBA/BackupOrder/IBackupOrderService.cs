namespace TOBA.BackupOrder
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	using Entity;

	using Query.Entity;

	using TOBA.Entity;

	interface IBackupOrderService
	{
		Task<(int level, string info)> GetSuccessRateAsync(string submitInfo, char seat);
		Task<(bool? faceChecked, bool allow, string msg)> CheckFaceAsync(params BackupCartItem[] items);

		string GetFaceCheckMsg(int code);

		Task<(int level, string info)> GetSuccessRateAsync(BackupCartItem item);

		Task<(bool success, string message)> SubmitOrderRequestAsync();

		/// <summary>
		/// 取消排队中订单
		/// </summary>
		/// <returns></returns>
		Task<(bool ok, string msg)> CancelWaitingOrderAsync();

		/// <summary>
		/// 提交候补订单
		/// </summary>
		/// <returns></returns>
		Task<(bool ok, string msg, ConfirmBackupOrderResponseData)> CommitBackupOrderAsync();

		/// <summary>
		/// 查询候补订单队列
		/// </summary>
		/// <returns></returns>
		Task<(bool ok, string msg, QueryBackupOrderQueueResponse)> QueryHbQueueAsync();

		/// <summary>
		/// 获得未完成候补订单（待支付）
		/// </summary>
		/// <returns></returns>
		Task<(bool ok, string msg, UnpayBackupOrder)> GetUnpayHbOrderAsync();

		/// <summary>
		/// 获得未完成候补订单（待兑现）
		/// </summary>
		/// <returns></returns>
		Task<(bool ok, string msg, List<BackupOrderItem>)> GetUnprocessedHbOrderItemsAsync();

		/// <summary>
		/// 获得已完成订单
		/// </summary>
		/// <returns></returns>
		Task<(bool ok, string msg, List<BackupOrderItem>)> GetProcessedHbOrderItemsAsync();

		/// <summary>
		/// 查询退款信息
		/// </summary>
		/// <param name="order"></param>
		/// <returns></returns>
		Task<(bool ok, string msg)> QueryRefundInfo(BackupOrderItem order);

		Task<(bool ok, string msg, Dictionary<string, string> form)> CreatePayForm(BackupOrderItem order);

		Task<(bool ok, string msg)> CancelNotCompleteOrder(BackupOrderItem order, Func<double, bool> confirm);

		/// <summary>
		/// 自动添加候补
		/// </summary>
		/// <param name="query"></param>
		/// <param name="result"></param>
		/// <returns></returns>
		Task DetectAutoHbTrainsToCart(QueryParam query, QueryResult result);
	}
}
