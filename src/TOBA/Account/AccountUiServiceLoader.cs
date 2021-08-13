using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Account
{
	using System.ComponentModel.Composition;

	using Autofac;
	using Autofac.Core;

	class AccountUiServiceLoader : Module, IModule
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<AccountService>().AsImplementedInterfaces().InstancePerLifetimeScope();
		}
	}
}
