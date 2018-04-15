using StructureMap;
using Xunit;

namespace SimpleHttpFactory.Extensions.StructureMap.Tests
{
    public class StructureMapTests
    {
        [Fact]
        public void ContainerExtensionTests()
        {
            var container = new Container()
                .ConfigureSimpleHttpFactory(f => f.AddClientFactory("test", _ => {}));

            var factory = container.GetInstance<ISimpleHttpFactory>();
            Assert.NotNull(factory);

            var testClient = factory.GetClient("test");
            Assert.NotNull(testClient);
        }
        
        [Fact]
        public void ContainerExpressionsExtensionTests()
        {
            var container = new Container(c => c.ConfigureSimpleHttpFactory(f => f.AddClientFactory("test", _ => {})));

            var factory = container.GetInstance<ISimpleHttpFactory>();
            Assert.NotNull(factory);

            var testClient = factory.GetClient("test");
            Assert.NotNull(testClient);
        }
    }
}
