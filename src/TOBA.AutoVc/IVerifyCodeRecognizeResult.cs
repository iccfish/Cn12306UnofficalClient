using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.AutoVc
{
	using System.Drawing;

	/// <summary>
	/// 验证码识别结果
	/// </summary>
	public interface IVerifyCodeRecognizeResult
	{
		/// <summary>
		/// 获得原始的图片
		/// </summary>
		Image Image { get; }

		/// <summary>
		/// 对于字符型验证码，获得最终识别的字符
		/// </summary>
		string Code { get; }

		/// <summary>
		/// 打码ID，用于报错
		/// </summary>
		string Id { get; }

		/// <summary>
		/// 验证码类型，如是字符串型的还是坐标型的
		/// </summary>
		VerifyCodeRecognizeType CodeType { get; }

		/// <summary>
		/// 获得坐标题的时候返回的坐标（数组）
		/// </summary>
		List<Point> Points { get; }
	}
}
