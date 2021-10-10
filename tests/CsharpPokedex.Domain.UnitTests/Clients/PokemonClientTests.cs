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
        private TestHttpClientFactory httpClientFactory;

        [SetUp]
        public void SetUp()
        {
            httpClientFactory = new TestHttpClientFactory();
            
            sut = new PokemonClient(
                httpClientFactory
            );
        }

        [TearDown]
        public void TearDown()
        {
            httpClientFactory.Dispose();
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
