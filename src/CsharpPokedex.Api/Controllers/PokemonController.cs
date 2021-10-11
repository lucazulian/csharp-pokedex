using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsharpPokedex.Domain.Models;
using CsharpPokedex.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CsharpPokedex.Api.Controllers
{
    [ApiController]
    [Route("pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;
        private readonly IPokemonService _pokemonService;

        public PokemonController(ILogger<PokemonController> logger, IPokemonService pokemonService)
        {
            this._logger = logger;
            this._pokemonService = pokemonService;
        }

        [HttpGet("{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PokemonBasicInformation))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PokemonBasicInformation>> GetByName([FromRoute] string name)
        {
            return await Get(name, (x) => _pokemonService.GetByName(x));
        }
        
        [HttpGet("translated/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PokemonBasicInformation))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PokemonBasicInformation>> GetTranslatedByName([FromRoute] string name)
        {
            return await Get(name, (x) => _pokemonService.GetTranslatedByName(x));
        }

        private async Task<ActionResult<PokemonBasicInformation>> Get(string name,
            Func<String, Task<Result<PokemonBasicInformation>>> service)
        {
            try
            {
                var pokemonBasicInformation = await service.Invoke(name);
                if (pokemonBasicInformation.IsFailure)
                {
                    var error = pokemonBasicInformation.Error;
                    this._logger.LogError($"Failed while retrieving pokemon {name} with error {error}");

                    return Problem(error, statusCode: Convert.ToInt32(pokemonBasicInformation.Error));
                }

                this._logger.LogInformation($"Returning pokemon {name}");

                return Ok(pokemonBasicInformation.Value);
            }
            catch (Exception e)
            {
                var message = $"Failed while getting pokemon {name}";
                this._logger.LogError(e, message);

                return Problem(message, statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}