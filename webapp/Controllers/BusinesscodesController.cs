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
        private readonly WebAppDbContext _context;
        private readonly ILogger<BusinesscodesController> _logger;

        public BusinesscodesController(WebAppDbContext context, ILogger<BusinesscodesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Businesscodes> Get()
        {
            _logger.LogDebug("Getting all businessCodes");
            
            return _context.Businesscodes
                .ToList();
        }
        
        [HttpGet("{busCode}")]
        public IEnumerable<Businesscodes> Get(string busCode)
        {
            _logger.LogDebug($"Getting businessCode for id: {busCode}");
            
            return _context.Businesscodes
                .Where(busCode => busCode.Businesscode.Equals(busCode));
        }
    }
}