using System.Net.Http;

namespace SimpleHttpFactory
{
    public interface ISimpleHttpFactory
    {
        HttpClient CreateClient(string key);
    }
}
