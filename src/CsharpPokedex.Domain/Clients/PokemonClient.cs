using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsharpPokedex.Domain.Models;
using Microsoft.Extensions.Logging;

namespace CsharpPokedex.Domain.Clients
{
    public class PokemonClient : IPokemonClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<PokemonClient> _logger;
        
        public PokemonClient(HttpClient httpClient, ILogger<PokemonClient> logger)
        {
            this._httpClient = httpClient;
            this._logger = logger;
        }
        
        public async Task<Result<PokemonSpecies>> GetByName(string name)
        {
            var response = await _httpClient
                .GetAsync($"https://pokeapi.co/api/v2/pokemon-species/{name}");

            var content = await response.Content.ReadAsStringAsync();

            return response.IsSuccessStatusCode
                ? Result.Success<PokemonSpecies>(JsonSerializer.Deserialize<PokemonSpecies>(content))
                : Result.Failure<PokemonSpecies>(((int)response.StatusCode).ToString());
        }
    }
}