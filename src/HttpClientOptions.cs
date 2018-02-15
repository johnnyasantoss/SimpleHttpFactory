using System;
using System.Net.Http;

namespace SimpleHttpFactory
{
    public class HttpClientOptions
    {
        public HttpMessageHandler MessageHandler { get; set; }

        public Func<HttpClient, HttpClient> ClientConfiguration { get; set; }

        public bool DisposeMessageHandler { get; set; }

        internal HttpClient Build()
        {
            HttpClient client;

            if (MessageHandler == null)
                client = new HttpClient();
            else
                client = new HttpClient(MessageHandler, DisposeMessageHandler);

            return ClientConfiguration == null
                ? client
                : ClientConfiguration(client);
        }
    }
}
