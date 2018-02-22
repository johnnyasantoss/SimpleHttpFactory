using System;
using StructureMap;

namespace SimpleHttpFactory.Extensions.StructureMap
{
    public static class StructureMapContainerBuilderExtensions
    {
        public static IContainer ConfigureSimpleHttpFactory(
            this IContainer container
            , Action<SimpleHttpFactory> configure
        )
        {
            container.Configure(c => c.ConfigureSimpleHttpFactory(configure));

            return container;
        }

        public static ConfigurationExpression ConfigureSimpleHttpFactory(
            this ConfigurationExpression c
            , Action<SimpleHttpFactory> configure
        )
        {
            var httpFac = new SimpleHttpFactory();

            configure?.Invoke(httpFac);

            c.ForSingletonOf<ISimpleHttpFactory>()
             .Use(httpFac);

            return c;
        }
    }
}
