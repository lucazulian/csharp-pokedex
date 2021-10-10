using System.Text.Json.Serialization;

namespace CsharpPokedex.Domain.Models
{
    public class PokemonHabitat
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}