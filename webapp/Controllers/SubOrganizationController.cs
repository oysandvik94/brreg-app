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
    public class SubOrganizationController
    {
        private readonly ILogger<SubOrganizationController> _logger;

        public SubOrganizationController(ILogger<SubOrganizationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Suborganization> Get()
        {
            _logger.LogDebug("Getting all subOrganizations");

            var db = new WebAppDbContext();
            return db.Suborganizations
                .ToList();
        }
        
        [HttpGet("{subOrgNr:int}")]
        public IEnumerable<Suborganization> Get(int subOrgNr)
        {
            _logger.LogDebug($"Loading subOrganization for id: {subOrgNr}");
            
            var db = new WebAppDbContext();
            return db.Suborganizations
                .Where(sub => sub.Suborgnr == subOrgNr)
                .Include(sub => sub.BusinesscodeSuborgs)
                .ToList();
        }
    }
}