using System;
using Autofac;
using Autofac.Builder;

namespace SimpleHttpFactory.Extensions.AutoFac
{
    public static class AutoFacContainerBuilderExtensions
    {
        public static IRegistrationBuilder<SimpleHttpFactory, SimpleActivatorData, SingleRegistrationStyle>
            RegisterSimpleHttpFactory(
                this ContainerBuilder cb
                , Action<SimpleHttpFactory> configure
            )
        {
            return cb.Register(ctx =>
                     {
                         var factory = new SimpleHttpFactory();

                         configure(factory);

                         return factory;
                     })
                     .As<ISimpleHttpFactory>();
        }
    }
}
