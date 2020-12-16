using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using webapp.Models;

namespace webapp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubOrganizationsController : ControllerBase
    {
        private readonly ILogger<SubOrganizationsController> _logger;
        private readonly WebAppDbContext _context;

        public SubOrganizationsController(WebAppDbContext context, ILogger<SubOrganizationsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Suborganizations> Get()
        {
            _logger.LogDebug("Getting all subOrganizations");
            
            return _context.Suborganizations
                .ToList();
        }
        
        [HttpGet("{subOrgNr:int}")]
        public IEnumerable<Suborganizations> Get(int subOrgNr)
        {
            _logger.LogDebug($"Loading subOrganization for id: {subOrgNr}");
            
            return _context.Suborganizations
                .Where(sub => sub.Suborgnr == subOrgNr)
                .ToList();
        }
    }
}