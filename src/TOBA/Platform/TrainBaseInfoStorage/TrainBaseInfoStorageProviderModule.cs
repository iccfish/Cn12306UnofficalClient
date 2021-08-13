namespace TOBA.Platform.TrainBaseInfoStorage
{
	using Autofac;
	using Autofac.Core;

	class TrainBaseInfoStorageProviderModule : Module, IModule
	{
		/// <summary>Loads the module into the kernel.</summary>
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.RegisterType<TrainBaseInfoStorageProvider>().AsImplementedInterfaces().SingleInstance();
		}
	}
}
