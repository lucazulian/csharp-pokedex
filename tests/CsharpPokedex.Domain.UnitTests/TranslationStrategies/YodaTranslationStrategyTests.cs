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
    public class YodaTranslationStrategyTests
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
                this.sut = new YodaTranslationStrategy(this._translationsClient.Object);
            }

            [TearDown]
            public void TearDown()
            {
                this._translationsClient.Verify();
            }

            [Test]
            public async Task ShouldReturnCorrectTranslationFormSuccessTranslationsClientResponse()
            {
                var translated = "Master obiwan hath did lose a planet.";
                var text = "Master Obiwan has lost a planet.";

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
                        It.Is<TranslatorType>(x => x == TranslatorType.Yoda),
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
                var text = "Master Obiwan has lost a planet.";

                this._translationsClient
                    .Setup(s => s.Get(
                        It.Is<TranslatorType>(x => x == TranslatorType.Yoda),
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
}