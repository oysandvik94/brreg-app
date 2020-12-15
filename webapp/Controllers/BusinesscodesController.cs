using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapp.Models;

namespace webapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BusinesscodesController : ControllerBase
    {
        private readonly ILogger<BusinesscodesController> _logger;

        public BusinesscodesController(ILogger<BusinesscodesController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Businesscodes> Get()
        {
            _logger.LogDebug("Getting all businessCodes");
            
            var db = new WebAppDbContext();
            return db.Businesscodes
                .ToList();
        }
        
        [HttpGet("{busCode}")]
        public IEnumerable<Businesscodes> Get(string busCode)
        {
            _logger.LogDebug($"Getting businessCode for id: {busCode}");
            
            var db = new WebAppDbContext();
            return db.Businesscodes
                .Where(busCode => busCode.Businesscode.Equals(busCode));
        }
    }
}