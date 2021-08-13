namespace TOBA.Platform.Sm4
{
	using Autofac;

	class Sm4CryptoModule:Module
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<Sm4CryptoService>().AsImplementedInterfaces().SingleInstance();
		}
	}
}
