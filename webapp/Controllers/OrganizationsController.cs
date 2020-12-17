using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using webapp.Models;

namespace webapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrganizationsController : BaseController
    {
        public OrganizationsController(WebAppDbContext context, ILogger<OrganizationsController> logger) : base(context, logger)
        {
        }
        
        [HttpGet]
        public async Task<List<Organizations>> Get([FromQuery(Name = "includeSubOrgs")] bool includeSubOrgs)
        {
            _logger.LogDebug("Getting all organizations");

            return await getQueryable(includeSubOrgs).ToListAsync(); ;
        }
        
        [HttpGet("{orgNr:int}")]
        public async Task<IActionResult> Get([FromQuery(Name = "includeSubOrgs")] bool includeSubOrgs, int orgNr)
        {
            _logger.LogDebug($"Getting organization for id {orgNr}");
            
            var result =  await getQueryable(includeSubOrgs).Where(org => org.Orgnr == orgNr).ToListAsync();

            if (!result.Any())
            {
                return NotFound($"Foretak med orgNr {orgNr} ble ikke funnet");
            }
            
            return Ok(result);
        }
        
              
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Organizations>> Create(Organizations org)
        {
            _logger.LogDebug($"Getting organization for id ");

            // Query for subOrganizations
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{QueryBrregController.BrregApiUri}/underenheter?overordnetEnhet={org.Orgnr}"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    var resultJson = JObject.Parse(apiResponse);
                    var subOrgs = resultJson.SelectToken("$._embedded.underenheter");

                    if (subOrgs != null)
                    {
                        org = addSubOrgs(org, subOrgs);
                    }
                }
            }
            
            await _context.Organizations.AddAsync(org);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = org.Orgnr }, org);
        }

        /**
         * Common logic for every Organizations query
         */
        private IQueryable<Organizations> getQueryable(bool includeSubOrgs)
        {
            var orgs = _context.Organizations.AsQueryable();
            return includeSubOrgs ? orgs.Include(org => org.Suborganizations) : orgs;
        }

        private static Organizations addSubOrgs(Organizations org, IEnumerable<JToken> subOrgs)
        {
            foreach (var subOrgToken in subOrgs)
                
            {
                var subOrg = new Suborganizations();
                subOrg.Suborgnr = subOrgToken.SelectToken("organisasjonsnummer").Value<int>();
                subOrg.Suborgname = subOrgToken.SelectToken("navn").Value<string>();
                subOrg.Municipality = subOrgToken.SelectToken("$.beliggenhetsadresse.kommune").Value<string>();
                
                org.Suborganizations.Add(subOrg);
            }

            return org;
        }
    }
}