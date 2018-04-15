using System.Net.Http;

namespace SimpleHttpFactory
{
    public interface ISimpleHttpFactory
    {
        HttpClient GetClient(string key);
    }
}
