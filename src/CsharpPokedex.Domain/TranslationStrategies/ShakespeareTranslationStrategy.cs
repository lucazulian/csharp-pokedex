using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsharpPokedex.Domain.Clients;

namespace CsharpPokedex.Domain.TranslationStrategies
{
    public class ShakespeareTranslationStrategy : ITranslationStrategy
    {
        public TranslatorType Name => TranslatorType.Shakespeare;

        private readonly IFunTranslationsClient _translationsClient;

        public ShakespeareTranslationStrategy(IFunTranslationsClient translationsClient)
        {
            this._translationsClient = translationsClient;
        }

        public async Task<string> Translate(string text)
        {
            var (isSuccess, _, value) = await this._translationsClient.Get(this.Name, text);
            return isSuccess ? value.Contents.Translated : text;
        }
    }
}