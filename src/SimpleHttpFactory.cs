using System;
using System.Collections.Concurrent;
using System.Net.Http;

namespace SimpleHttpFactory
{
    public class SimpleHttpFactory : ISimpleHttpFactory
    {
        private readonly ConcurrentDictionary<string, Action<HttpClientOptions>> _clientsFactories;
        private readonly ConcurrentDictionary<string, HttpClient> _clients;

        public SimpleHttpFactory()
        {
            _clientsFactories = new ConcurrentDictionary<string, Action<HttpClientOptions>>();
            _clients = new ConcurrentDictionary<string, HttpClient>();
        }

        private HttpClient CreateClient(string key)
        {
            if (!_clientsFactories.TryGetValue(key, out var action))
                throw new InvalidOperationException("Not found");

            var httpClientOptions = new HttpClientOptions();

            action(httpClientOptions);

            return httpClientOptions.Build();
        }

        public HttpClient GetClient(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(key));

            if (!_clients.TryGetValue(key, out var client))
            {
                client = CreateClient(key);
            }

            return client;
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
