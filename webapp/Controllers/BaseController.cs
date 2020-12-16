using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapp.Models;

namespace webapp.Controllers
{
    public class BaseController : ControllerBase
    {
        protected readonly WebAppDbContext _context;
        protected readonly ILogger<BaseController> _logger;

        public BaseController(WebAppDbContext context, ILogger<BaseController> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}