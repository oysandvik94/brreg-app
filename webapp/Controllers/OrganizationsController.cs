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
    public class OrganizationsController : ControllerBase
    {
        private readonly ILogger<OrganizationsController> _logger;

        public OrganizationsController(ILogger<OrganizationsController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet]
        public IEnumerable<Organizations> Get()
        {
            _logger.LogDebug("Getting all organizations");
            
            var db = new WebAppDbContext();
            return db.Organizations
                .Include(org => org.BusinesscodeOrg)
                .ToList(); ;
        }
        
        [HttpGet("{orgNr:int}")]
        public IEnumerable<Organizations> Get(int orgNr)
        {
            _logger.LogDebug($"Getting organization for id {orgNr}");
            
            var db = new WebAppDbContext();
            return db.Organizations
                .Where(org => org.Orgnr == orgNr)
                .Include(org => org.BusinesscodeOrg)
                .ToList();
        }
    }
}