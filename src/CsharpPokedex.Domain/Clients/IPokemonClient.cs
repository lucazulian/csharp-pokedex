using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsharpPokedex.Domain.Models;

namespace CsharpPokedex.Domain.Clients
{
    public interface IPokemonClient
    {
        Task<Result<PokemonSpecies>> GetByName(string name);
    }
}