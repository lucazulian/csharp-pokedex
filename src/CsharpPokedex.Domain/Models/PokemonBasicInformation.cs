using System.Text.Json.Serialization;

namespace CsharpPokedex.Domain.Models
{
    public class PokemonBasicInformation
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("habitat")]
        public string Habitat { get; set; }

        [JsonPropertyName("isLegendary")]
        public bool IsLegendary { get; set; }
    }
}