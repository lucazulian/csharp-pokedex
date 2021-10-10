using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsharpPokedex.Domain.Models;

namespace CsharpPokedex.Domain.Services
{
    public interface IPokemonService
    {
        Task<Result<PokemonBasicInformation>> GetByName(string name);
    }
}
