using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace SimpleHttpFactory
{
    public class SimpleHttpFactory : ISimpleHttpFactory
    {
        private readonly ConcurrentDictionary<string, Func<HttpClientOptions, HttpClientOptions>> _clientsFactories;

        public SimpleHttpFactory()
        {
            _clientsFactories = new ConcurrentDictionary<string, Func<HttpClientOptions, HttpClientOptions>>();
        }

        public HttpClient CreateClient(string key)
        {
            if (!_clientsFactories.TryGetValue(key, out var action))
                throw new InvalidOperationException("Not found");

            return action(new HttpClientOptions())
                .Build();
        }
    }
}
