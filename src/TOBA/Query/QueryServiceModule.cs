namespace TOBA.Query
{

	using Autofac;
	using Autofac.Core;


	class QueryServiceModule : Module, IModule
	{
		/// <summary>
		/// Loads the module into the kernel.
		/// </summary>
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<TicketOrderInitializerService>().InstancePerLifetimeScope().AsImplementedInterfaces();
			builder.RegisterType<LeftTicketQueryService>().AsImplementedInterfaces().SingleInstance();
			builder.RegisterType<QueryTimeoutWarningService>().AsImplementedInterfaces().SingleInstance();
			builder.RegisterType<SystemBusyWarningService>().AsImplementedInterfaces().SingleInstance();
		}
	}
}
