namespace TOBA.Account
{
	using Autofac;

	class SessionCookiesPersentatorModule : Module
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.RegisterType<SessionCookiesPersentator>().AsImplementedInterfaces().SingleInstance();
		}
	}
}