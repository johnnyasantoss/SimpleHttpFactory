using System;
using Xunit;

namespace SimpleHttpFactory.Tests
{
    public class SimpleHttpFactoryTests
    {
        [Fact]
        public void SimpleUsageTest()
        {
            var sut = new SimpleHttpFactory();
            var baseAddress = new Uri("http://0.0.0.0:9090");
            var timeout = TimeSpan.FromMinutes(5);

            sut.AddClientFactory("test",
                opt => opt.ClientConfiguration = c =>
                {
                    c.BaseAddress = baseAddress;
                    c.Timeout = timeout;
                }
            );
            var client = sut.CreateClient("test");

            Assert.Same(baseAddress, client.BaseAddress);
            Assert.Equal(timeout, client.Timeout);
        }
    }
}
