using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TOBA.Configuration
{
	using System.ComponentModel.Composition;

	using Autofac;
	using Autofac.Core;


	class ConfigurationServiceModule : Module, IModule
	{
		/// <summary>
		/// Loads the module into the kernel.
		/// </summary>
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<ConfigurationProvider>().AsImplementedInterfaces().SingleInstance();
		}
	}
}
