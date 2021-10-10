using System.Text.Json.Serialization;

namespace CsharpPokedex.Domain.Models
{
    public class PokemonFlavorTextEntryLanguage
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}