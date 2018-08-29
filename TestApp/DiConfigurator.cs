using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace TestApp
{
	public class DIConfigurator
	{
		public  IServiceProvider ServiceProvider { private set; get; }

		public  void SetUp(Action<IServiceCollection> setUp)
		{
			var services = new ServiceCollection();
			setUp.Invoke(services);

			ServiceProvider = services.BuildServiceProvider();
		}
	}
}
