using Autofac;
using SimpleHttpFactory.Extensions.AutoFac;
using Xunit;

namespace SimpleHttpFactory.Extensions.Autofac.Tests
{
    public class AutofacTests
    {
        [Fact]
        public void AutofacUsageTest()
        {
            var cb = new ContainerBuilder();

            cb.RegisterSimpleHttpFactory(f => f.AddClientFactory("test", _ => {}));

            var container = cb.Build();

            var factory = container.Resolve<ISimpleHttpFactory>();
            Assert.NotNull(factory);

            var testClient = factory.GetClient("test");
            Assert.NotNull(testClient);
        }
    }
}
