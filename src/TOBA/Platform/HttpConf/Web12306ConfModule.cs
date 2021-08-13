namespace TOBA.Platform.HttpConf
{
	using Autofac;
	using Autofac.Core;

	class Web12306ConfModule : Module, IModule
	{
		/// <summary>Loads the module into the kernel.</summary>
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.RegisterType<Web12306ConfProvider>().AsImplementedInterfaces().SingleInstance();
		}
	}
}