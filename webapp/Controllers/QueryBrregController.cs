using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapp.Models;

namespace webapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueryBrregController : BaseController
    {
        internal const string BrregApiUri = "https://data.brreg.no/enhetsregisteret/api";
        
        public QueryBrregController(WebAppDbContext context, ILogger<QueryBrregController> logger) : base(context, logger)
        {
        }

        [HttpGet("{searchParam}")]
        public async Task<IActionResult>  Get(string searchParam)
        {
            _logger.LogDebug("Querying brreg on name");
            
            var jToken = await QueryThirdPartiApi($"{BrregApiUri}/enheter?navn={searchParam}");
            return Ok(jToken.SelectToken("$._embedded.enheter"));
        }
    }
}