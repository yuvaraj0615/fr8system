using fr8customerapi.Models;
using fr8model;
using fr8model.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace fr8customerapi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly CustomerDbContext _context;
        private readonly IHttpClientFactory _taxClientFactory;

        public CustomersController(ILogger<CustomersController> logger, CustomerDbContext context,
                IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _context = context;
            _taxClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IEnumerable<CustomerFreightDto>> Get()
        {
            _logger.LogInformation($"Get customers information");

            List<CustomerFreightDto> dtos = new List<CustomerFreightDto>();

            foreach(var cust in await _context.Customers.ToListAsync())
            {
                dtos.Add(await PopulateCustomerFreights(cust));
            }

            return dtos;
        }

        private async Task<CustomerFreightDto> PopulateCustomerFreights(Customer s)
        {
            decimal tax = await GetStatePercentage(s.ToState);
            decimal taxPercent = tax / (decimal)100.0;
            decimal total = (s.LoadWeight * s.ChargeAmount) +
                                ((s.LoadWeight * s.ChargeAmount) * taxPercent);
            return new CustomerFreightDto()
            {
                CustomerName = s.CustomerName,
                CarrierName = s.CarrierName,
                FromState = s.FromState,
                ToState = s.ToState,
                LoadWeight = s.LoadWeight,
                ChargeAmount = s.ChargeAmount,
                TaxPersent = tax,
                TotalAmount = total.ToString("C")
            };
        }
        
        private async Task<decimal> GetStatePercentage(string state)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                    $"api/taxes/{state}");

            var client = _taxClientFactory.CreateClient("taxapi");

            var response = await client.SendAsync(request);

            Tax tax = null;

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                tax = await JsonSerializer.DeserializeAsync<Tax>(responseStream,
                        new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            }

            return tax.TaxPercent;
        }

        [HttpGet("{customerName}")]
        public Customer GetCustomer(string customerName)
        {
            _logger.LogInformation($"Get customer information {customerName}");
            var tax = _context.Customers.Where(i => i.CustomerName == customerName).FirstOrDefault();
            _logger.LogInformation($"Returned customer details : {customerName}");
            return tax;
        }

    }
}
