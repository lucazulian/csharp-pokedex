using System.Text.Json.Serialization;

namespace CsharpPokedex.Domain.Models
{
    public class Translations
    {
        [JsonPropertyName("success")]
        public TranslationsSuccess Success { get; set; }

        [JsonPropertyName("contents")] 
        public TranslationsContents Contents { get; set; }
    }
}