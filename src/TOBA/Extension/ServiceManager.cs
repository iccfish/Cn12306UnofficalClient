using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace TOBA.Extension
{

	using Autofac;

	using AutoVc;

	using Configuration;

	using FSLib.Extension;

	using WebLib;

	internal class ServiceManager : IExtensionManager
	{
		/// <summary>
		/// 枚举所有扩展，并执行指定操作
		/// </summary>
		/// <param name="action"></param>
		public void EnumerateExtension(Action<IExtension> action)
		{
			Extensions.ForEach(action);
		}

		/// <summary>
		/// 根据类型查找扩展
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T FindExtension<T>() => (T)FindExtension(typeof(T));

		/// <summary>
		/// 根据扩展类型查找扩展
		/// </summary>
		/// <param name="t"></param>
		/// <returns></returns>
		public IExtension FindExtension(Type t) => Extensions.FirstOrDefault(s => s.GetType() == t || (t.IsInterface && s.GetType().GetInterfaces().Contains(t)));


		/// <summary>
		/// 获得当前全局的配置提供对象
		/// </summary>
		public IConfigurationProvider ConfigurationProvider => GlobalKernel.Resolve<IConfigurationProvider>();

		/// <summary>
		/// 获得全局核心
		/// </summary>
		public ILifetimeScope GlobalKernel { get; private set; }


		/// <summary>
		/// 所有的插件
		/// </summary>
		public IExtension[] Extensions { get; private set; }


		/// <summary>
		/// 验证码服务
		/// </summary>
		public IVerifyCodeRecognizeService[] VerifyCodeRecogniseService { get; private set; }


		/// <summary>
		/// 请求校验服务
		/// </summary>
		public IRequestInspector[] WebRequestInspectors { get; private set; }

		/// <summary>
		/// 身份状态保存服务
		/// </summary>
		public ISessionPresentor[] SessionPresentors { get; private set; }

		public ServiceManager()
		{
			AppContext.ExtensionManager = this;
		}

		public void Load()
		{
			var root = ApplicationRunTimeContext.GetProcessMainModuleDirectory();

			var builder = new ContainerBuilder();
			builder.RegisterInstance(this).AsImplementedInterfaces().SingleInstance();

			var list = new[]
			{
				Assembly.GetEntryAssembly(),
				Assembly.GetExecutingAssembly()
			}.Distinct();
			var extDir = Path.Combine(root, "extensions");
			if (Directory.Exists(extDir))
				list = list.Union(Directory.GetFiles(extDir, "TOBA.*.dll", SearchOption.AllDirectories).Select(Assembly.LoadFile).ToArray());

			var assemblies = list.ToArray();
			builder.RegisterAssemblyModules(assemblies);
			builder.RegisterAssemblyTypes(assemblies).
				Where(s =>
					s.HasInterface<IExtension>()
					|| s.HasInterface<IVerifyCodeRecognizeService>()
					|| s.HasInterface<ISessionPresentor>()
					|| s.HasInterface<IRequestInspector>()
				).
				AsImplementedInterfaces().
				SingleInstance();
			builder.RegisterAssemblyTypes(assemblies).
				Where(s =>
					s.HasInterface<IRequireSessionInit>()
				).
				AsImplementedInterfaces().
				AsSelf();
			GlobalKernel = builder.Build();

			//初始化
			Extensions = GlobalKernel.Resolve<IExtension[]>();
			SessionPresentors = GlobalKernel.Resolve<ISessionPresentor[]>();
			WebRequestInspectors = GlobalKernel.Resolve<IRequestInspector[]>();
			VerifyCodeRecogniseService = GlobalKernel.Resolve<IVerifyCodeRecognizeService[]>();

			Extensions.ForEach(s => s.Connect());
		}

		/// <summary>
		/// 断开连接
		/// </summary>
		public void Unload()
		{
			foreach (var extension in Extensions)
			{
				extension.Disconnect();
			}
		}
	}
}
