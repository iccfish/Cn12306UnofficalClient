namespace TOBA.UI.Controls
{
	using System;
	using System.Drawing;
	using System.Windows.Forms;

	using AutoVc;

	using TOBA.Service;
	using TOBA.WebLib;

	internal interface IVerifyCodeControl
	{
		/// <summary>
		/// 自动识别失败
		/// </summary>
		event EventHandler AutoVcFailed;
		/// <summary>
		/// 放弃自动识别（失败次数过多）
		/// </summary>
		event EventHandler AutoVcGiveUp;

		/// <summary>
		/// 开始自动识别时触发
		/// </summary>
		event EventHandler BeginAutoVc;
		/// <summary>
		/// 指定当前的输入验证码状态已变
		/// </summary>
		event EventHandler CodeEnterChanged;
		/// <summary>
		/// 完成识别
		/// </summary>
		event EventHandler EndAutoVc;
		/// <summary>
		/// 指定当前已经完成验证
		/// </summary>
		event EventHandler VerifyCodeEnterComplete;
		/// <summary>
		/// 验证码加载成功
		/// </summary>
		event EventHandler VerifyCodeLoadComplete;

		/// <summary>
		/// 验证码加载失败
		/// </summary>
		event EventHandler VerifyCodeLoadFailed;

		event EventHandler VerifyCodeOnLoad;

		/// <summary>
		/// 加载验证码
		/// </summary>
		void LoadVerifyCode();

		void MarkError(IVerifyCodeRecognizeResult code);

		void ResetLoadImage();

		void SetExpires();

		/// <summary>
		/// 设置是否需要验证码
		/// </summary>
		/// <param name="need"></param>
		void SetIfNeedVc(bool? need);

		/// <summary>
		/// 获得或设置是否无法识别时自动重载
		/// </summary>
		bool AutoReloadIfAutoVcFailed { get; set; }

		/// <summary>
		/// 获得自动识别的验证码
		/// </summary>
		IVerifyCodeRecognizeResult AutoVcCode { get; }

		/// <summary>
		/// 获得自动识别的次数
		/// </summary>
		int AutoVcCount { get; }

		string Code { get; }

		PictureBoxSizeMode CodeSizeMode { get; set; }

		/// <summary>
		/// 获得或设置是否允许自动识别
		/// </summary>
		bool EnableAutoVc { get; set; }
		bool EnableClickReload { get; set; }

		/// <summary>
		/// 获得或设置是否启用控件
		/// </summary>
		bool Enabled { get; set; }

		/// <summary>
		/// 获得是否是传统的验证码
		/// </summary>
		bool IsTraditionalCode { get; }

		/// <summary>
		/// 获得验证码是否已经加载成功
		/// </summary>
		bool Loaded { get; }

		int MaxAutoVcCount { get; set; }

		/// <summary>
		/// 获得或设置是否需要验证码
		/// </summary>
		bool? NeedVc { get; }

		RandCodeType RandType { get; set; }
		bool ShowOkButton { get; set; }

		/// <summary>
		/// 获得或设置当前的验证码图像
		/// </summary>
		System.Drawing.Image ValidateImage { get; set; }

		/// <summary>
		/// 上级控件
		/// </summary>
		Control Parent
		{
			get;
		}

		/// <summary>
		/// 当前位置
		/// </summary>
		Point Location { get; }

		Size Size { get; }

		bool Visible { get; set; }
	}
}