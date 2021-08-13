namespace TOBA.Platform.SlideVc
{
	using Autofac;

	class SlideVcModule : Module
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.RegisterType<SlideVcService>().AsImplementedInterfaces().InstancePerLifetimeScope();
		}

	}
}
