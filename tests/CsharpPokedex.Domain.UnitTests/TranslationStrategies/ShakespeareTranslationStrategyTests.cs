using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsharpPokedex.Domain.Clients;
using CsharpPokedex.Domain.Models;
using CsharpPokedex.Domain.TranslationStrategies;
using Moq;
using NUnit.Framework;

namespace CsharpPokedex.Domain.UnitTests.TranslationStrategies
{
    [TestFixture]
    public class ShakespeareTranslationStrategyTests
    {
        private Mock<IFunTranslationsClient> _translationsClient;
        private ITranslationStrategy sut;

        [SetUp]
        public void SetUp()
        {
            this._translationsClient = new Mock<IFunTranslationsClient>();
            this.sut = new ShakespeareTranslationStrategy(this._translationsClient.Object);
        }

        [TearDown]
        public void TearDown()
        {
            this._translationsClient.Verify();
        }

        [Test]
        public async Task ShouldReturnCorrectTranslationFormSuccessTranslationsClientResponse()
        {
            var translated =
                "Thee did giveth mr. Tim a hearty meal,  but unfortunately what he did doth englut did maketh him kicketh the bucket.";
            var text = "You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die.";

            var translations = new Translations
            {
                Success = new TranslationsSuccess()
                {
                    Total = 1
                },
                Contents = new TranslationsContents()
                {
                    Translated = translated
                }
            };

            this._translationsClient
                .Setup(s => s.Get(
                    It.Is<TranslatorType>(x => x == TranslatorType.Shakespeare),
                    It.Is<String>(x => x == text)
                ))
                .ReturnsAsync(Result.Success(translations))
                .Verifiable();

            var translation = await sut.Translate(text);

            Assert.IsNotEmpty(translation);
            Assert.AreEqual(translated, translation);
        }

        [Test]
        public async Task ShouldReturnOriginalTextFormFailureTranslationsClientResponse()
        {
            var text = "You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die.";

            this._translationsClient
                .Setup(s => s.Get(
                    It.Is<TranslatorType>(x => x == TranslatorType.Shakespeare),
                    It.Is<String>(x => x == text)
                ))
                .ReturnsAsync(Result.Failure<Translations>("429"))
                .Verifiable();

            var translation = await sut.Translate(text);

            Assert.IsNotEmpty(translation);
            Assert.AreEqual(text, translation);
        }
    }
}