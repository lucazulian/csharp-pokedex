using System.Threading.Tasks;
using CsharpPokedex.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CsharpPokedex.Api.Controllers
{
    [ApiController]
    [Route("pokemon")]
    public class PokemonController : ControllerBase
    {
        private readonly ILogger<PokemonController> _logger;

        public PokemonController(ILogger<PokemonController> logger)
        {
            this._logger = logger;
        }

        [HttpGet("{name}")]
        public async Task<ActionResult<Pokemon>> GetByName([FromRoute] string name)
        {
            return Ok("test");
        }
    }
}