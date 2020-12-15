using System;
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
    public class OrganizationController : ControllerBase
    {
        private readonly ILogger<OrganizationController> _logger;

        public OrganizationController(ILogger<OrganizationController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public IEnumerable<Organization> Get()
        {
            _logger.LogDebug("Getting all organizations");
            
            var db = new WebAppDbContext();
            return db.Organizations
                .Include(org => org.BusinesscodeOrgs)
                .ToList(); ;
        }
        
        [HttpGet("{orgNr:int}")]
        public IEnumerable<Organization> Get(int orgNr)
        {
            _logger.LogDebug($"Getting organization for id {orgNr}");
            
            var db = new WebAppDbContext();
            return db.Organizations
                .Where(org => org.Orgnr == orgNr)
                .Include(org => org.BusinesscodeOrgs)
                .ToList();
        }
    }
}