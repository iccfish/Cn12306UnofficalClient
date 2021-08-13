namespace TOBA.Account
{
	using System.ComponentModel.Composition;

	using Autofac;
	using Autofac.Core;

	class AccountServiceModule : Module
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.RegisterType<SessionReloginService>().AsImplementedInterfaces().InstancePerLifetimeScope();
			builder.RegisterType<SessionLoginService>().AsImplementedInterfaces();
			builder.RegisterType<AccountService>().AsImplementedInterfaces().InstancePerLifetimeScope();
		}
	}
}