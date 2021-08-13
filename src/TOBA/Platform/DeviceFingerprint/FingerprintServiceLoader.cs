namespace TOBA.Platform.DeviceFingerprint
{
	using Autofac;
	using Autofac.Core;

	class FingerprintServiceLoader : Module, IModule
	{
		/// <summary>Loads the module into the kernel.</summary>
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<FingerprintService>().SingleInstance().AsImplementedInterfaces();
		}
	}
}