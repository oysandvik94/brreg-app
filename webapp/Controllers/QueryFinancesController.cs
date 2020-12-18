using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapp.Models;

namespace webapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueryFinancesController : BaseController
    {
        internal const string BrregFinancesUri = "http://34.98.91.231";
        
        public QueryFinancesController(WebAppDbContext context, ILogger<QueryFinancesController> logger) : base(context, logger)
        {
        }

        [HttpGet("{orgNr:int}")]
        public async Task<IActionResult> Get(int orgNr)
        {
            _logger.LogDebug($"Querying finances for organization with orgNr: {orgNr}");

            var jToken = await QueryThirdPartiApi($"{BrregFinancesUri}/regnskap?orgNummer={orgNr}");
            
            if (!jToken.Any())
            {
                return NotFound($"Could not find financial information for orgnr: {orgNr}");
            }

            return Ok(jToken.First.SelectToken("$.egenkapitalGjeld"));
        }
    }
}