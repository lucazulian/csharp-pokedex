using System.Text.Json.Serialization;

namespace CsharpPokedex.Domain.Models
{
    public class TranslationsContents
    {
        [JsonPropertyName("translated")]
        public string Translated { get; set; }
    }
}