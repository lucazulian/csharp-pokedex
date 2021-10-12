using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsharpPokedex.Domain.Clients;
using Microsoft.Extensions.Logging;

namespace CsharpPokedex.Domain.TranslationStrategies
{
    public class YodaTranslationStrategy : ITranslationStrategy
    {
        public TranslatorType Name => TranslatorType.Yoda;

        private readonly IFunTranslationsClient _translationsClient;

        public YodaTranslationStrategy(IFunTranslationsClient translationsClient)
        {
            this._translationsClient = translationsClient;
        }

        public async Task<string> Translate(string text)
        {
            var (isSuccess, _, value) = await this._translationsClient.Get(this.Name, text);
            return isSuccess ? value.Contents?.Translated : text;
        }
    }
}