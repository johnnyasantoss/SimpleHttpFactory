using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SimpleHttpFactory.Tests
{
    public class SimpleHttpFactoryTests
    {
        [Fact]
        public void SimpleUsageTest()
        {
            var simpleHttpFactory = new SimpleHttpFactory();
            var baseAddress = new Uri("http://0.0.0.0:9090");
            var timeout = TimeSpan.FromMinutes(5);

            simpleHttpFactory.AddClientFactory(
                "test",
                opt => opt.ClientConfiguration = c =>
                {
                    c.BaseAddress = baseAddress;
                    c.Timeout = timeout;
                }
            );
            var client = simpleHttpFactory.CreateClient("test");

            Assert.Same(baseAddress, client.BaseAddress);
            Assert.Equal(timeout, client.Timeout);
        }

        [Fact]
        public async Task ShouldBuildAHttpClientWithMessageHandlerIfAny()
        {
            var simpleHttpFactory = new SimpleHttpFactory();

            simpleHttpFactory.AddClientFactory(
                "test",
                opt => opt.MessageHandler = new TestMessageHandler()
            );
            var client = simpleHttpFactory.CreateClient("test");

            var exception = await Assert.ThrowsAsync<NotImplementedException>(() => client.GetAsync("http://0.0.0.0"));
            Assert.Equal("yeah it is right", exception.Message);
        }

        [Fact]
        public void ShouldBuildEvenWhenThereIsNoOptionsSet()
        {
            var simpleHttpFactory = new SimpleHttpFactory();

            simpleHttpFactory.AddClientFactory("test", _ => {});
            var client = simpleHttpFactory.CreateClient("test");

            Assert.NotNull(client);
        }
    }

    public class TestMessageHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request
            , CancellationToken cancellationToken
        )
        {
            throw new NotImplementedException("yeah it is right");
        }
    }
}
