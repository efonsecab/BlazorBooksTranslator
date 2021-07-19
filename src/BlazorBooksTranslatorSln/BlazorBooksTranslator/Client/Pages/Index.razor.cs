using BlazorBooksTranslator.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorBooksTranslator.Client.Pages
{
    public partial class Index
    {
        [Inject]
        private HttpClient HttpClient { get; set; }
        private TranslateModel Model { get; set; } = new();
        private string ErrorMessage { get; set; }
        private string SuccessMessage { get; set; }
        private bool IsLoading { get; set; } = false;
        public async Task OnValidSubmit()
        {
            IsLoading = true;
            this.SuccessMessage = this.ErrorMessage = string.Empty;
            StateHasChanged();
            try
            {
                string requestUrl = "api/BookTranslation/Translate";
                var response = await this.HttpClient.PostAsJsonAsync(requestUrl, this.Model);
                if (response.IsSuccessStatusCode)
                {
                    this.SuccessMessage = "Operation Succeeded";
                }
                else
                {
                    var reason = await response.Content.ReadAsStringAsync();
                    this.ErrorMessage = reason;
                }
            }
            catch (Exception ex)
            {
                this.ErrorMessage = ex.Message;
            }

            finally
            {
                IsLoading = false;
                StateHasChanged();
            }
        }
    }
}
