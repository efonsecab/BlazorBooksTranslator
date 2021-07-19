using BlazorBooksTranslator.Server.Configuration;
using BlazorBooksTranslator.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace BlazorBooksTranslator.Server.Services
{
    public class PTIBooksTranslationService
    {
        private ILogger<PTIBooksTranslationService> Logger { get; }
        private HttpClient HttpClient { get; }
        private PtiApisConfiguration PtiApisConfiguration { get; }
        public PTIBooksTranslationService(ILogger<PTIBooksTranslationService> logger,
            HttpClient httpClient, PtiApisConfiguration ptiApisConfiguration)
        {
            this.Logger = logger;
            this.HttpClient = httpClient;
            this.PtiApisConfiguration = ptiApisConfiguration;
        }

        public async Task TranslateFromUrl(TranslateModel model, CancellationToken cancellationToken = default)
        {
            try
            {
                string requestUrl = $"https://books-translation-and-analysis-by-pti.p.rapidapi.com/api/1.0/Books/TranslateBookFromWordFile" +
                    $"?emailAddressForResults={model.EmailAddressForResults}" +
                    $"&sourceFileUrl={HttpUtility.UrlEncode(model.SourceFileUrl)}" +
                    $"&bookSourceLanguage={model.BookSourceLanguage}" +
                    $"&bookOutputLanguage={model.BookOutputLanguage}" +
                    $"&translationMode={model.TranslationMode}";

                System.Net.Http.StringContent stringContent = new StringContent(JsonSerializer.Serialize(model));
                stringContent.Headers.Add("x-rapidapi-key", this.PtiApisConfiguration.BooksTranslationConfiguration.RapidApiKey);
                stringContent.Headers.Add("x-rapidapi-host", this.PtiApisConfiguration.BooksTranslationConfiguration.RapidApiHost);
                HttpRequestMessage message = new(HttpMethod.Post, requestUrl);
                message.Content = stringContent;
                using (var response = await this.HttpClient.SendAsync(message))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        var errorMessage = await response.Content.ReadAsStringAsync();
                        throw new Exception(errorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                this.Logger?.LogError(ex.Message, ex);
                throw;
            }
        }

    }

}