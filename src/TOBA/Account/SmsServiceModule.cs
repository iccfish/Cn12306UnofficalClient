namespace TOBA.Account;

using Autofac;
using Autofac.Core;

class SmsServiceModule : Module, IModule
{
	/// <inheritdoc />
	protected override void Load(ContainerBuilder builder)
	{
		base.Load(builder);
		builder.RegisterType<SmsService>().AsImplementedInterfaces().SingleInstance();
	}
}
