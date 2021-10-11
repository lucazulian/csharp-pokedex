using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CsharpPokedex.Domain.Clients;
using CsharpPokedex.Domain.Models;
using CsharpPokedex.Domain.TranslationStrategies;
using Microsoft.Extensions.Logging;

namespace CsharpPokedex.Domain.Services
{
    public class TranslationService : ITranslationService
    {
        private readonly ILogger<TranslationService> _logger;
        private readonly IReadOnlyDictionary<TranslatorType, ITranslationStrategy> _strategies;

        public TranslationService(
            ILogger<TranslationService> logger,
            IReadOnlyDictionary<TranslatorType, ITranslationStrategy> strategies)
        {
            this._logger = logger;
            this._strategies = strategies;
        }

        public async Task<string> Translate(PokemonBasicInformation pokemon)
        {
            var strategy = GetStrategy(pokemon);
            return await strategy.Translate(pokemon.Description);
        }

        private ITranslationStrategy GetStrategy(PokemonBasicInformation pokemon)
        {
            if ("cave".Equals(pokemon.Habitat, StringComparison.OrdinalIgnoreCase) || pokemon.IsLegendary)
            {
                return this._strategies[TranslatorType.Yoda];
            }

            return this._strategies[TranslatorType.Shakespeare];
        }
    }
}