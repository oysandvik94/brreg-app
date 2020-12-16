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
    public class SubOrganizationsController : BaseController
    {
        public SubOrganizationsController(WebAppDbContext context, ILogger<SubOrganizationsController> logger) : base(context, logger)
        {
        }

        [HttpGet]
        public async Task<IEnumerable<Suborganizations>> Get()
        {
            _logger.LogDebug("Getting all subOrganizations");
            
            return await _context.Suborganizations
                .ToListAsync();
        }
        
        [HttpGet("{subOrgNr:int}")]
        public async Task<IEnumerable<Suborganizations>> Get(int subOrgNr)
        {
            _logger.LogDebug($"Loading subOrganization for id: {subOrgNr}");
            
            return await _context.Suborganizations
                .Where(sub => sub.Suborgnr == subOrgNr)
                .ToListAsync();
        }
    }
}