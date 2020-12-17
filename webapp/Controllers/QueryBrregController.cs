using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using webapp.Models;

namespace webapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QueryBrregController : BaseController
    {
        internal const string BrregApiUri = "https://data.brreg.no/enhetsregisteret/api";
        
        public QueryBrregController(WebAppDbContext context, ILogger<OrganizationsController> logger) : base(context, logger)
        {
        }

        [HttpGet("{searchParam}")]
        public async Task<IActionResult>  Get(string searchParam)
        {
            _logger.LogDebug("Querying brreg on name");
            
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{BrregApiUri}/enheter?navn={searchParam}"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    var resultJson = JObject.Parse(apiResponse);
                    return Ok(resultJson.SelectToken("$._embedded.enheter"));
                }
            }
        }
    }
}