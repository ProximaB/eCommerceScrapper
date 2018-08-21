using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Autofac;
using eCommerceScrapper.Interfaces;
using eCommerceScrapper.StrategyProvider;
using Unity;
using Unity.RegistrationByConvention;
using Unity.Strategies;
using Unity.Container;
using IContainer = Autofac.IContainer;

namespace eCommerceScrapper.Bootstrapper
{
    public static class BootstrapperIoC
    {
        public static IContainer RegisterComponents ()
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(typeof(IParseStrategy).Assembly)
                .AssignableTo<IParseStrategy>()
                .AsImplementedInterfaces();

            builder.Register(c => new EbayStrategiesProvider(c.Resolve<IEnumerable<IEbayStrategy>>())).As<IParseStrategiesProvider<IEbayStrategy>>();

            var container = builder.Build();
            return container;
        }
    }
}