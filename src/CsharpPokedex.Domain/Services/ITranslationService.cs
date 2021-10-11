using System.Threading.Tasks;
using CsharpPokedex.Domain.Models;

namespace CsharpPokedex.Domain.Services
{
    public interface ITranslationService
    {
        Task<string> Translate(PokemonBasicInformation pokemon);
    }
}