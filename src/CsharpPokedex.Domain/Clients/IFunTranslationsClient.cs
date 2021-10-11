using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsharpPokedex.Domain.Models;

namespace CsharpPokedex.Domain.Clients
{
    public interface IFunTranslationsClient
    {
        Task<Result<Translations>> Get(TranslatorType translator, string text);
    }
}