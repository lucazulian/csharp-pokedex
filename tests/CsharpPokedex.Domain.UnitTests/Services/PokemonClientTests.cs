using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsharpPokedex.Domain.Clients;
using CsharpPokedex.Domain.Services;
using CsharpPokedex.Domain.UnitTests.Helpers;
using Moq;
using NUnit.Framework;

namespace CsharpPokedex.Domain.UnitTests.Services
{
    [TestFixture]
    public class PokemonClientTests
    {
        private Mock<IPokemonClient> _pokemonClient;
        private Mock<ITranslationService> _translationService;
        private IPokemonService sut;

        [SetUp]
        public void SetUp()
        {
            this._pokemonClient = new Mock<IPokemonClient>();
            this._translationService = new Mock<ITranslationService>();

            this.sut = new PokemonService(
                this._pokemonClient.Object,
                this._translationService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this._pokemonClient.Verify();
            this._translationService.Verify();
        }

        [Test]
        public async Task ShouldReturnCorrectPokemonBasicInformationFromExistingPokemon()
        {
            const string pokemonName = "bulbasaur";

            var expected = PokemonBasicInformationFixture.CreateFullDummy();
            var pokemonSpecies = PokemonSpeciesFixture.CreateFullDummy();

            this._pokemonClient
                .Setup(s => s.GetByName(It.Is<string>(x => x == pokemonName)))
                .ReturnsAsync(Result.Success(pokemonSpecies))
                .Verifiable();

            var actual = await sut.GetByName(pokemonName);

            Assert.IsTrue(actual.IsSuccess);
            Assert.IsTrue(Comparer.Compare(expected, actual.Value));
        }
    }
}