using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
        public async Task<IEnumerable<Organizations>> Get([FromQuery(Name = "includeSubOrgs")] bool includeSubOrgs, int orgNr)
        {
            _logger.LogDebug($"Getting organization for id {orgNr}");
            
            return await getQueryable(includeSubOrgs).Where(org => org.Orgnr == orgNr).ToListAsync();
        }

        /**
         * Common logic for every Organizations query
         */
        private IQueryable<Organizations> getQueryable(bool includeSubOrgs)
        {
            var orgs = _context.Organizations.AsQueryable();
            return includeSubOrgs ? orgs.Include(org => org.Suborganizations) : orgs;
        }
    }
}