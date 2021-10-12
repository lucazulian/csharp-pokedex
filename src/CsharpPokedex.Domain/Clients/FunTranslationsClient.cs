using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using CsharpPokedex.Domain.Models;
using Microsoft.Extensions.Logging;

namespace CsharpPokedex.Domain.Clients
{
    public class FunTranslationsClient : IFunTranslationsClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<FunTranslationsClient> _logger;

        public FunTranslationsClient(HttpClient httpClient, ILogger<FunTranslationsClient> logger)
        {
            this._httpClient = httpClient;
            this._logger = logger;
        }

        public async Task<Result<Translations>> Get(TranslatorType translator, string text)
        {
            var response = await _httpClient
                .PostAsync(
                    $"https://api.funtranslations.com/translate/{translator}.json",
                    new FormUrlEncodedContent(
                        new[]
                        {
                            new KeyValuePair<string, string>("text", text)
                        }));

            var content = await response.Content.ReadAsStringAsync();

            this.LogResponse(response, content);

            return response.IsSuccessStatusCode
                ? Result.Success<Translations>(JsonSerializer.Deserialize<Translations>(content))
                : Result.Failure<Translations>(((int) response.StatusCode).ToString());
        }

        private void LogResponse(HttpResponseMessage httpResponseMessage, string responseContent)
        {
            this._logger.LogInformation(httpResponseMessage.ToString());

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                this._logger.LogError(
                    $"Failed getting translations: {httpResponseMessage.StatusCode} - {httpResponseMessage.ReasonPhrase} - {responseContent}"
                );
            }
        }
    }
}