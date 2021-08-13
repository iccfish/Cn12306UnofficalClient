using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.UI.Servicing.Order
{
	using Autofac;
	using Autofac.Core;

	class OrderServiceModule : Module, IModule
	{
		/// <summary>
		/// Loads the module into the kernel.
		/// </summary>
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);
			builder.RegisterType<OrderQueueUiProvider>().AsImplementedInterfaces().InstancePerLifetimeScope();
		}
	}
}
