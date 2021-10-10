using System;
using System.Threading.Tasks;
using CsharpPokedex.Domain.Clients;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;


namespace CsharpPokedex.Domain.UnitTests.Clients
{
    [TestFixture]
    public class PokemonClientTests
    {
        private TestHttpClientFactory _httpClientFactory;
        private Mock<ILogger<PokemonClient>> _loggerMock;
        
        private IPokemonClient sut;
     
        [SetUp]
        public void SetUp()
        {
            _httpClientFactory = new TestHttpClientFactory();
            _loggerMock = new Mock<ILogger<PokemonClient>>();
            
            sut = new PokemonClient(
                _httpClientFactory.CreateClient(),
                _loggerMock.Object
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
