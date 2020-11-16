using fr8model;
using fr8taxapi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fr8taxapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxesController : ControllerBase
    {
        private readonly ILogger<TaxesController> _logger;
        private readonly TaxDbContext _context;

        public TaxesController(ILogger<TaxesController> logger, TaxDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Tax> Get()
        {
            _logger.LogInformation($"Get all state tax information");
            return _context.Taxes.ToList();
        }

        [HttpGet("{state}")]
        public Tax GetTax(string state)
        {
            _logger.LogInformation($"Get Tax information for the state {state}");
            var tax = _context.Taxes.Where(i => i.StateName == state).FirstOrDefault();
            _logger.LogInformation($"Returned {state} state tax information");
            return tax;
        }
    }

}
