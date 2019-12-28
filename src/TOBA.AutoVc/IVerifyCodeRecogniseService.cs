using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TOBA.AutoVc
{

	/// <summary>
	/// 用以实现验证码识别的接口
	/// </summary>
	public interface IVerifyCodeRecognizeService
	{
		/// <summary>
		/// 根据验证码图片获得对应的验证码。这是同步请求，异步识别请阻塞当前线程。
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		IVerifyCodeRecognizeResult GetCode(Image image);

		/// <summary>
		/// 标记识别结果
		/// </summary>
		/// <param name="result"></param>
		/// <param name="correct"></param>
		void MarkResult(IVerifyCodeRecognizeResult result, bool correct);

		/// <summary>
		/// 获得或设置是否可用
		/// </summary>
		bool IsEnabled { get; }

		/// <summary>
		/// 是否已经登录
		/// </summary>
		bool IsLoggedIn { get; }

		/// <summary>
		/// 是否已经通过验证
		/// </summary>
		bool Verified { get; }

		/// <summary>
		/// 状态发生变化
		/// </summary>
		event EventHandler StateChanged;

		/// <summary>
		/// 获得剩余题分
		/// </summary>
		int Score { get; }

		/// <summary>
		/// 获得错误代码
		/// </summary>
		int ErrorCode { get; }

		/// <summary>
		/// 获得官网地址
		/// </summary>
		string WebUrl { get; }

		/// <summary>
		/// 错误代码查询地址
		/// </summary>
		string ErrorCodeQueryUrl { get; }

		/// <summary>
		/// 获得提供者的名字
		/// </summary>
		string ProviderName { get; }

		/// <summary>
		/// 作者
		/// </summary>
		string Author { get; }

		/// <summary>
		/// 加载
		/// </summary>
		void Load();

		/// <summary>
		/// 卸载
		/// </summary>
		void Unload();

		/// <summary>
		/// 用户名
		/// </summary>
		string UserName { get; set; }

		/// <summary>
		/// 登录密码
		/// </summary>
		string Password { get; set; }

		/// <summary>
		/// 登录
		/// </summary>
		/// <returns></returns>
		bool DoLogin();

		/// <summary>
		/// 注销
		/// </summary>
		/// <returns></returns>
		bool Logout();

		/// <summary>
		/// 刷新题分
		/// </summary>
		/// <returns></returns>
		bool RefreshScore();

		/// <summary>
		/// Gets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		string Id { get; }

		/// <summary>
		/// Gets the name.
		/// </summary>
		/// <value>The name.</value>
		string Name { get; }

	}
}
