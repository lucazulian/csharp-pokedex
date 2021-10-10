using System;
using System.Net.Http;

namespace CsharpPokedex.Domain.UnitTests
{
    public class TestHttpClientFactory : IHttpClientFactory, IDisposable
    {
        private readonly HttpClient httpClient;
        public TestHttpClientFactory()
        {
            httpClient = new HttpClient();
        }

        public HttpClient CreateClient(string name)
        {
            return httpClient;
        }

        public void Dispose()
        {
            httpClient?.Dispose();
        }
    }
}