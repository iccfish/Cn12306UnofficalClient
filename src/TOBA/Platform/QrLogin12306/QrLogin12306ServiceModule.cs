namespace TOBA.Platform.QrLogin12306
{
	using Autofac;
	using Autofac.Core;

	class QrLogin12306ServiceModule : Module
	{
		/// <summary>Loads the module into the kernel.</summary>
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.RegisterType<QrLogin12306Service>().AsImplementedInterfaces().SingleInstance();
		}
	}
}