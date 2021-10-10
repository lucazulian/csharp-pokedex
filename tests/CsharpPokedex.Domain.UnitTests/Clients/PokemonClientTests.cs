using System;
using System.Threading.Tasks;
using CsharpPokedex.Domain.Clients;
using CsharpPokedex.Domain.UnitTests.Helpers;
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
            this._httpClientFactory = new TestHttpClientFactory();
            this._loggerMock = new Mock<ILogger<PokemonClient>>();
            
            this.sut = new PokemonClient(
                this._httpClientFactory.CreateClient(),
                this._loggerMock.Object
            );
        }

        [TearDown]
        public void TearDown()
        {
            this._httpClientFactory.Dispose();
        }

        [Test]
        public async Task ShouldReturnSuccessWithPokemonDataFromExistingName()
        {
            const string name = "bulbasaur";

            var actual = await this.sut.GetByName(name);

            Assert.IsTrue(actual.IsSuccess);
            Assert.IsTrue(name.Equals(actual.Value.Name, StringComparison.OrdinalIgnoreCase));
        }

        [Test]
        public async Task ShouldReturnFailureFromNonExistingName()
        {
            var actual = await this.sut.GetByName("non-existing-pokemon");

            Assert.IsTrue(actual.IsFailure);
        }
    }   
}
