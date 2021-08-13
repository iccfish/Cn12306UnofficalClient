namespace TOBA.UI.Components
{
	using Autofac;
	using Autofac.Core;

	using BackupOrder;

	class UiOperationModule : Module, IModule
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.RegisterType<BackupOrderUiOperation>().AsImplementedInterfaces().InstancePerLifetimeScope();
		}
	}
}