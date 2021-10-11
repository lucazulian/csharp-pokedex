using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsharpPokedex.Domain.Clients;
using CsharpPokedex.Domain.Models;
using CsharpPokedex.Domain.TranslationStrategies;

namespace CsharpPokedex.Domain.Services
{
    public class PokemonService : IPokemonService
    {
        private const String Language = "en";

        private readonly IPokemonClient _pokemonClient;
        private readonly ITranslationService _translationService;

        public PokemonService(IPokemonClient pokemonClient, ITranslationService translationService)
        {
            this._pokemonClient = pokemonClient;
            this._translationService = translationService;
        }

        public async Task<Result<PokemonBasicInformation>> GetByName(string name)
        {
            var pokemon = await this._pokemonClient.GetByName(name);
            return pokemon.IsSuccess
                ? Result.Success(MapResult(pokemon.Value))
                : Result.Failure<PokemonBasicInformation>(pokemon.Error);
        }

        public async Task<Result<PokemonBasicInformation>> GetTranslatedByName(string name)
        {
            var pokemon = await GetByName(name);
            return pokemon.IsSuccess
                ? Result.Success(MapTranslationResult(pokemon.Value))
                : pokemon;
        }

        private static PokemonBasicInformation MapResult(PokemonSpecies pokemon)
        {
            return new PokemonBasicInformation
            {
                Name = pokemon.Name,
                Description = GetEnDescription(pokemon.FlavorTextEntries),
                Habitat = pokemon.Habitat?.Name,
                IsLegendary = pokemon.IsLegendary
            };
        }

        private PokemonBasicInformation MapTranslationResult(PokemonBasicInformation pokemon)
        {
            return new PokemonBasicInformation
            {
                Name = pokemon.Name,
                Description = this._translationService.Translate(pokemon).Result,
                Habitat = pokemon.Habitat,
                IsLegendary = pokemon.IsLegendary
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