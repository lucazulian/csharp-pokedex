using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using CsharpPokedex.Domain.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;

namespace CsharpPokedex.Api.IntegrationTests
{
    [TestFixture]
    public class PokemonBasicInformationRouteTests
    {
        private HttpClient _client;
        private CancellationTokenSource _tokenSource;

        private TestServer sut;

        [SetUp]
        public void SetUp()
        {
            _tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(30));

            var builder = new WebHostBuilder();
            builder.UseStartup<Startup>();

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
        public async Task ShouldReturnCorrectPokemonBasicInformationFromExistingPokemon()
        {
            const string name = "charizard";

            var response = await _client.GetAsync($"/pokemon/{name}", _tokenSource.Token);

            Assert.IsTrue(response.IsSuccessStatusCode);
            Assert.IsTrue(HttpStatusCode.OK == response.StatusCode);

            var actualPokemon = await response.Content.ReadFromJsonAsync<PokemonBasicInformation>();

            Assert.IsNotNull(actualPokemon);
            Assert.That(name.Equals(actualPokemon.Name, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public async Task ShouldGetNotFoundStatusCodeFromNonExistingPokemon()
        {
            const string pokemonName = "luca";

            var response = await _client.GetAsync($"/pokemon/{pokemonName}", _tokenSource.Token);

            Assert.IsFalse(response.IsSuccessStatusCode);
            Assert.IsTrue(HttpStatusCode.NotFound == response.StatusCode);
        }
    }
}