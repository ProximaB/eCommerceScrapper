using System.Collections.Generic;
using System.Net.Http;
using Autofac;
using eCommerceScrapper.Interfaces;
using eCommerceScrapper.StrategyProvider;
using System;
using Autofac.Core;
using IContainer = Autofac.IContainer;

namespace eCommerceScrapper.Bootstrapper
{
    public static class BootstrapperIoC
    {
        public static IContainer RegisterComponents ()
        {
            var builder = new ContainerBuilder();

            // Create Singleton for HttpClient
            builder.Register(ctx => new HttpClient()) //{ BaseAddress = new Uri("https://api.ipify.org") })
                .Named<HttpClient>("BaseClient")
                .SingleInstance();

            // Register all available instances of IEbayStrategy for injection IEnumerable<IEbayStrategy>
            builder.RegisterAssemblyTypes(typeof(IEbayStrategy).Assembly)
                .AssignableTo<IEbayStrategy>()
                .AsImplementedInterfaces()
                .SingleInstance();

            // Register EBayStrategy Provider
            builder.Register(c =>
                    new EbayStrategiesProvider(c.Resolve<IEnumerable<IEbayStrategy>>()))
                .As<IParseStrategiesProvider<IEbayStrategy>>().InstancePerDependency();

            // Register EbayStrategiesProcessor
            builder.Register(c =>
                new EbayStrategiesProcessor(c.Resolve<IParseStrategiesProvider<IEbayStrategy>>(),
                   c.ResolveNamed<HttpClient>("BaseClient"))).As<EbayStrategiesProcessor>().InstancePerDependency();

            return builder.Build();
        }
    }
}