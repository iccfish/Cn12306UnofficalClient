namespace TOBA.Passport
{
	using Autofac;
	using Autofac.Core;

	class PassportModule : Module, IModule
	{
		/// <summary>Loads the module into the kernel.</summary>
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.RegisterType<UamAuthService>().AsImplementedInterfaces().SingleInstance();
		}
	}
}