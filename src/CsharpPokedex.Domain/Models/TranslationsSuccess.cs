using System.Text.Json.Serialization;

namespace CsharpPokedex.Domain.Models
{
    public class TranslationsSuccess
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}