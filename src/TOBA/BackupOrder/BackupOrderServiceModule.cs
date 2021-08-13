namespace TOBA.BackupOrder
{
	using Autofac;
	using Autofac.Core;

	class BackupOrderServiceModule : Module, IModule
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.RegisterType<BackupOrderService>().AsImplementedInterfaces().InstancePerLifetimeScope();
			builder.RegisterType<HbInfoProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();
		}
	}
}