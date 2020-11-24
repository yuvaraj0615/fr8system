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
    [Produces("application/json")]
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

        /// <summary>
        /// Get Taxes
        /// </summary>
        /// <returns>Returns tax list</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Tax>), 200)]
        public IEnumerable<Tax> Get()
        {
            _logger.LogInformation($"Get all state tax information");
            return _context.Taxes.ToList();
        }

        /// <summary>
        /// Get Tax by state
        /// </summary>
        /// <param name="state">State acronym</param>
        /// <returns>Returns state tax details</returns>
        [HttpGet("{state}")]
        [ProducesResponseType(typeof(Tax), 200)]
        public Tax GetTax(string state)
        {
            _logger.LogInformation($"Get Tax information for the state {state}");
            var tax = _context.Taxes.Where(i => i.StateName == state).FirstOrDefault();
            _logger.LogInformation($"Returned {state} state tax information");
            return tax;
        }
    }

}
