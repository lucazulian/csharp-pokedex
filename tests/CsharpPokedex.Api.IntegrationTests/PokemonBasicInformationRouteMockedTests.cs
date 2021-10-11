using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsharpPokedex.Domain.Clients;
using CsharpPokedex.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;

namespace CsharpPokedex.Api.IntegrationTests
{
    [TestFixture]
    public class PokemonBasicInformationRouteMockedTests
    {
        private Mock<IPokemonClient> _pokemonClient;
        private HttpClient _client;
        private CancellationTokenSource _tokenSource;

        private TestServer sut;

        [SetUp]
        public void SetUp()
        {
            this._pokemonClient = new Mock<IPokemonClient>();

            _tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            var builder = new WebHostBuilder();
            builder
                .ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPokemonClient>(this._pokemonClient.Object);
                })
                .UseStartup<Startup>();

            sut = new TestServer(builder);

            _client = sut.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            sut.Dispose();
            _tokenSource.Dispose();
        }

        [Test]
        [TestCase("500", HttpStatusCode.InternalServerError)]
        [TestCase("404", HttpStatusCode.NotFound)]
        public async Task ShouldGetInternalServerErrorCodeOnPokemonClientError(string errorCode,
            HttpStatusCode expectedStatus)
        {
            const string name = "celebi";

            this._pokemonClient
                .Setup(s => s.GetByName(It.Is<string>(x => x == name)))
                .ReturnsAsync(Result.Failure<PokemonSpecies>(errorCode))
                .Verifiable();

            var response = await _client.GetAsync($"/pokemon/{name}", _tokenSource.Token);

            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsTrue(expectedStatus == response.StatusCode);
        }
    }
}