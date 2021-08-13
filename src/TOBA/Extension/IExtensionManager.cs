using System;

namespace TOBA.Extension
{
	using Autofac;

	using AutoVc;

	using Configuration;

	using TOBA.WebLib;

	interface IExtensionManager
	{
		/// <summary>
		/// 断开连接
		/// </summary>
		void Unload();

		/// <summary>
		/// 已加载的扩展
		/// </summary>
		IExtension[] Extensions { get; }

		/// <summary>
		/// 枚举所有扩展，并执行指定操作
		/// </summary>
		/// <param name="action"></param>
		void EnumerateExtension(Action<IExtension> action);

		/// <summary>
		/// 根据类型查找扩展
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		T FindExtension<T>();

		/// <summary>
		/// 根据扩展类型查找扩展
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		IExtension FindExtension(Type t);

		/// <summary>
		/// 已加载的验证码识别扩展
		/// </summary>
		IVerifyCodeRecognizeService[] VerifyCodeRecogniseService { get; }

		/// <summary>
		/// 验证码服务
		/// </summary>
		IRequestInspector[] WebRequestInspectors { get; }

		/// <summary>
		/// 身份状态保存服务
		/// </summary>
		ISessionPresentor[] SessionPresentors { get; }

		/// <summary>
		/// 获得当前全局的配置提供对象
		/// </summary>
		IConfigurationProvider ConfigurationProvider { get; }

		/// <summary>
		/// 获得全局核心
		/// </summary>
		ILifetimeScope GlobalKernel { get; }
	}
}
