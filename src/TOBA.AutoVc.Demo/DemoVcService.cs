using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.AutoVc.RkVc
{
	using System.Drawing;
	using System.Drawing.Imaging;
	using System.IO;
	using System.Net.Mime;
	using System.Text.RegularExpressions;
	using System.Threading;
	using System.Threading.Tasks;

	using AutoVc;

	public class DemoVcService : AbstractVerifyCodeRecognizeService
	{
		public DemoVcService()
		{
			Author = "木魚(iFish)";
			ErrorCodeQueryUrl = "http://查询错误地址";
			Name = "测试远程打码";
			WebUrl = "http://WEB网址";
			ProviderName = "测试远程打码";
		}

		/// <summary>
		/// 加载
		/// </summary>
		public override void Load()
		{
			base.Load();
			Verified = true;
			ErrorCode = 0;
		}

		/// <summary>
		/// 登录
		/// </summary>
		/// <returns></returns>
		public override bool DoLogin()
		{
			//执行登录动作，成功返回true
			IsLoggedIn = true;
			Score = 1000;
			return true;
		}

		/// <summary>
		/// 注销
		/// </summary>
		/// <returns></returns>
		public override bool Logout()
		{
			//执行注销动作，成功返回true
			return true;
		}

		/// <summary>
		/// 刷新题分
		/// </summary>
		/// <returns></returns>
		public override bool RefreshScore()
		{
			//刷新题分
			return true;
		}

		/// <summary>
		/// 插件ID
		/// </summary>
		public override string Id => "testvc";

		/// <summary>
		/// 标记识别结果
		/// </summary>
		/// <param name="result"></param>
		/// <param name="correct"></param>
		public override void MarkResult(IVerifyCodeRecognizeResult result, bool correct)
		{
			//标记结果错误
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="image"></param>
		/// <returns></returns>
		public override IVerifyCodeRecognizeResult GetCode(Image image)
		{
			//识别图片，失败返回null
			var result = new VerifyCodeRecognizeResult(image);
			result.SetPointsFromIndex("这里不重要的ID", "1,4,5,8");

			//延迟3秒，好显示效果
			Thread.Sleep(3000);
			return result;
		}
	}
}
