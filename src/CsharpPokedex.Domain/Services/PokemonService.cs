using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsharpPokedex.Domain.Clients;
using CsharpPokedex.Domain.Models;

namespace CsharpPokedex.Domain.Services
{
    public class PokemonService : IPokemonService
    {
        private const String Language = "en";

        private readonly IPokemonClient _pokemonClient;

        public PokemonService(IPokemonClient pokemonClient)
        {
            this._pokemonClient = pokemonClient;
        }

        public async Task<Result<PokemonBasicInformation>> GetByName(string name)
        {
            var pokemon = await this._pokemonClient.GetByName(name);
            return pokemon.IsSuccess
                ? Result.Success(MapResult(pokemon))
                : Result.Failure<PokemonBasicInformation>(pokemon.Error);
        }

        private static PokemonBasicInformation MapResult(Result<PokemonSpecies> pokemonResult)
        {
            return new PokemonBasicInformation
            {
                Name = pokemonResult.Value.Name,
                Description = GetEnDescription(pokemonResult.Value.FlavorTextEntries),
                Habitat = pokemonResult.Value.Habitat?.Name,
                IsLegendary = pokemonResult.Value.IsLegendary
            };
        }

        private static string GetEnDescription(PokemonFlavorTextEntry[] flavorTextEntries)
        {
            return flavorTextEntries?
                       .FirstOrDefault(x => Language.Equals(x.Language?.Name, StringComparison.OrdinalIgnoreCase))?
                       .FlavorText
                       .Replace("\n", " ")
                       .Replace("\f", " ")
                   ?? string.Empty;
        }
    }
}