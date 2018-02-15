using System;
using System.Net.Http;

namespace SimpleHttpFactory
{
    public class HttpClientOptions
    {
        public HttpMessageHandler MessageHandler { get; set; }

        public Action<HttpClient> ClientConfiguration { get; set; }

        public bool DisposeMessageHandler { get; set; }

        internal HttpClient Build()
        {
            HttpClient client;

            if (MessageHandler == null)
                client = new HttpClient();
            else
                client = new HttpClient(MessageHandler, DisposeMessageHandler);

            ClientConfiguration?.Invoke(client);

            return client;
        }
    }
}
