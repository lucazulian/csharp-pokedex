using System.Threading.Tasks;
using CsharpPokedex.Domain.Clients;

namespace CsharpPokedex.Domain.TranslationStrategies
{
    public interface ITranslationStrategy
    {
        TranslatorType Name { get; }

        Task<string> Translate(string text);
    }
}