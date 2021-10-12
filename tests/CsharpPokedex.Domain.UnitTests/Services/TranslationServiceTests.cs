using System.Collections.Generic;
using System.Threading.Tasks;
using CsharpPokedex.Domain.Clients;
using CsharpPokedex.Domain.Models;
using CsharpPokedex.Domain.Services;
using CsharpPokedex.Domain.TranslationStrategies;
using Moq;
using NUnit.Framework;

namespace CsharpPokedex.Domain.UnitTests.Services
{
    [TestFixture]
    public class TranslationServiceTests
    {
        private ITranslationService sut;
        private Mock<ITranslationStrategy> _yodaTranslationStrategy;
        private Mock<ITranslationStrategy> _shakespeareTranslationStrategy;

        [SetUp]
        public void SetUp()
        {
            this._yodaTranslationStrategy = new Mock<ITranslationStrategy>();
            this._shakespeareTranslationStrategy = new Mock<ITranslationStrategy>();

            var strategies = new Dictionary<TranslatorType, ITranslationStrategy>
            {
                {TranslatorType.Yoda, this._yodaTranslationStrategy.Object},
                {TranslatorType.Shakespeare, this._shakespeareTranslationStrategy.Object}
            };

            this.sut = new TranslationService(strategies);
        }

        [TearDown]
        public void TearDown()
        {
            this._yodaTranslationStrategy.Verify();
            this._shakespeareTranslationStrategy.Verify();
        }

        [Test]
        public async Task ShouldReturnCorrectYodaTranslationFromCaveHabitatPokemon()
        {
            var expected_translation = "Master obiwan hath did lose a planet.";
            var description = "Master Obiwan has lost a planet.";

            var pokemon = new PokemonBasicInformation {Habitat = "cave", Description = description};

            this._yodaTranslationStrategy
                .Setup(s => s.Translate(It.Is<string>(x => x == description)))
                .ReturnsAsync(expected_translation)
                .Verifiable();

            var translation = await this.sut.Translate(pokemon);

            Assert.IsNotEmpty(translation);
            Assert.AreEqual(expected_translation, translation);
        }

        [Test]
        public async Task ShouldReturnCorrectYodaTranslationFromLegendaryPokemon()
        {
            var expected_translation = "Master obiwan hath did lose a planet.";
            var description = "Master Obiwan has lost a planet.";

            var pokemon = new PokemonBasicInformation {IsLegendary = true, Description = description};

            this._yodaTranslationStrategy
                .Setup(s => s.Translate(It.Is<string>(x => x == description)))
                .ReturnsAsync(expected_translation)
                .Verifiable();

            var translation = await this.sut.Translate(pokemon);

            Assert.IsNotEmpty(translation);
            Assert.AreEqual(expected_translation, translation);
        }

        [Test]
        public async Task ShouldReturnCorrectYodaTranslationFromPokemon()
        {
            var expected_translation =
                "Thee did giveth mr. Tim a hearty meal,  but unfortunately what he did doth englut did maketh him kicketh the bucket.";
            var description = "You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die.";

            var pokemon = new PokemonBasicInformation {Description = description};

            this._shakespeareTranslationStrategy
                .Setup(s => s.Translate(It.Is<string>(x => x == description)))
                .ReturnsAsync(expected_translation)
                .Verifiable();

            var translation = await this.sut.Translate(pokemon);

            Assert.IsNotEmpty(translation);
            Assert.AreEqual(expected_translation, translation);
        }
    }
}