using System.Text.Json.Serialization;

namespace CsharpPokedex.Domain.Models
{
    public class PokemonFlavorTextEntry
    {
        [JsonPropertyName("flavor_text")] 
        public string FlavorText { get; set; }

        [JsonPropertyName("language")]
        public PokemonFlavorTextEntryLanguage Language { get; set; } 
    }
}