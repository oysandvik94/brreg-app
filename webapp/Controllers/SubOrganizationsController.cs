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
    public class SubOrganizationsController
    {
        private readonly ILogger<SubOrganizationsController> _logger;

        public SubOrganizationsController(ILogger<SubOrganizationsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Suborganizations> Get()
        {
            _logger.LogDebug("Getting all subOrganizations");

            var db = new WebAppDbContext();
            return db.Suborganizations
                .ToList();
        }
        
        [HttpGet("{subOrgNr:int}")]
        public IEnumerable<Suborganizations> Get(int subOrgNr)
        {
            _logger.LogDebug($"Loading subOrganization for id: {subOrgNr}");
            
            var db = new WebAppDbContext();
            return db.Suborganizations
                .Where(sub => sub.Suborgnr == subOrgNr)
                .Include(sub => sub.BusinesscodeSuborg)
                .ToList();
        }
    }
}