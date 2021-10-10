using System;
using System.Threading.Tasks;
using CsharpPokedex.Domain.Clients;
using NUnit.Framework;

namespace CsharpPokedex.Domain.UnitTests.Clients
{
    [TestFixture]
    public class PokemonClientTests
    {
        private IPokemonClient sut;
        private TestHttpClientFactory _httpClientFactory;

        [SetUp]
        public void SetUp()
        {
            _httpClientFactory = new TestHttpClientFactory();
            
            sut = new PokemonClient(
                _httpClientFactory.CreateClient()
            );
        }

        [TearDown]
        public void TearDown()
        {
            _httpClientFactory.Dispose();
        }

        [Test]
        public async Task ShouldReturnSuccessWithPokemonDataFromExistingName()
        {
            const string name = "bulbasaur";

            var actual = await sut.GetByName(name);

            Assert.IsTrue(actual.IsSuccess);
            Assert.IsTrue(name.Equals(actual.Value.Name, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public async Task ShouldReturnFailureFromNonExistingName()
        {
            const string name = "non-existing-pokemon";
            
            var actual = await sut.GetByName(name);

            Assert.IsTrue(actual.IsFailure);
        }
    }   
}
