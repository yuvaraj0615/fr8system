using fr8model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace fr8web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _clientFactory;
        public IEnumerable<CustomerFreightDto> CustomerFreights { get; private set; }

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _clientFactory = httpClientFactory;
        }

        public async Task OnGet()
        {
            CustomerFreights = await GetFreightQuotes();
        }

        private async Task<IEnumerable<CustomerFreightDto>> GetFreightQuotes()
        {
            IEnumerable<CustomerFreightDto> freightDtos;

            var request = new HttpRequestMessage(HttpMethod.Get,
                   $"api/customers");

            var client = _clientFactory.CreateClient("customerapi");

            var response = await client.SendAsync(request);


            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                freightDtos = await JsonSerializer.DeserializeAsync<IEnumerable<CustomerFreightDto>>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }
            else
            {
                freightDtos = Array.Empty<CustomerFreightDto>();
            }

            return freightDtos;
        }

        public async Task<JsonResult> OnGetArrayData()
        {
            CustomerFreights = await GetFreightQuotes();

            return new JsonResult(CustomerFreights);
        }

    }
}
