using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapp.Models;

namespace webapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinesscodeController : ControllerBase
    {
        private readonly ILogger<BusinesscodeController> _logger;

        public BusinesscodeController(ILogger<BusinesscodeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Businesscode> Get()
        {
            _logger.LogDebug("Getting all businessCodes");
            
            var db = new WebAppDbContext();
            return db.Businesscodes
                .ToList();
        }
        
        [HttpGet("{busCode}")]
        public IEnumerable<Businesscode> Get(string busCode)
        {
            _logger.LogDebug($"Getting businessCode for id: {busCode}");
            
            var db = new WebAppDbContext();
            return db.Businesscodes
                .Where(busCode => busCode.Businesscode1.Equals(busCode));
        }
    }
}