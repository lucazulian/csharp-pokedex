using System;
using System.Net.Http;

namespace CsharpPokedex.Domain.UnitTests
{
    public class TestHttpClientFactory : IDisposable
    {
        private readonly HttpClient _httpClient;
        
        public TestHttpClientFactory()
        {
            _httpClient = new HttpClient();
        }

        public HttpClient CreateClient()
        {
            return _httpClient;
        }

        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}