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
    public class FunTranslationsClientTests
    {
        private TestHttpClientFactory _httpClientFactory;
        private Mock<ILogger<FunTranslationsClient>> _loggerMock;
        private IFunTranslationsClient sut;

        [SetUp]
        public void SetUp()
        {
            this._httpClientFactory = new TestHttpClientFactory();
            this._loggerMock = new Mock<ILogger<FunTranslationsClient>>();

            this.sut = new FunTranslationsClient(
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
        [TestCase(TranslatorType.Shakespeare,
            "You gave Mr. Tim a hearty meal, but unfortunately what he ate made him die.",
            "Thee did giveth mr. Tim a hearty meal,  but unfortunately what he did doth englut did maketh him kicketh the bucket.")]
        [TestCase(TranslatorType.Yoda, 
            "Master Obiwan has lost a planet.", 
            "Master obiwan hath did lose a planet.")]
        public async Task ShouldReturnSuccessWithTranslationsFromText(TranslatorType translator, string originalText, string expectedTranslatedText)
        {
            var actual = await this.sut.Get(TranslatorType.Shakespeare, originalText);

            Assert.IsTrue(actual.IsSuccess);
            Assert.AreEqual(expectedTranslatedText, actual.Value?.Contents?.Translated);
        }
    }
}