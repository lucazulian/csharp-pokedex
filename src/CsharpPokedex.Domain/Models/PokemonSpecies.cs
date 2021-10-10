using System.Text.Json.Serialization;

namespace CsharpPokedex.Domain.Models
{
    public class PokemonSpecies
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("habitat")]
        public PokemonHabitat Habitat { get; set; }

        [JsonPropertyName("is_legendary")]
        public bool IsLegendary { get; set; }

        [JsonPropertyName("flavor_text_entries")]
        public PokemonFlavorTextEntry[] FlavorTextEntries { get; set; } 
    }
}