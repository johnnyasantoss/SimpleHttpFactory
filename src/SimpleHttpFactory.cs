using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace SimpleHttpFactory
{
    public class SimpleHttpFactory : ISimpleHttpFactory
    {
        private readonly ConcurrentDictionary<string, Action<HttpClientOptions>> _clientsFactories;

        public SimpleHttpFactory()
        {
            _clientsFactories = new ConcurrentDictionary<string, Action<HttpClientOptions>>();
        }

        public HttpClient CreateClient(string key)
        {
            if (!_clientsFactories.TryGetValue(key, out var action))
                throw new InvalidOperationException("Not found");

            var httpClientOptions = new HttpClientOptions();

            action(httpClientOptions);

            return httpClientOptions.Build();
        }

        public void AddClientFactory(string key, Action<HttpClientOptions> factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            if (!_clientsFactories.TryAdd(key, factory))
                throw new InvalidOperationException("Could not add this factory");
        }
    }
}
