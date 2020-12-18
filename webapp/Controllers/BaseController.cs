using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
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

        protected async Task<JToken> QueryThirdPartiApi(string uri)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync($"{uri}"))
                {
                    var apiResponse = await response.Content.ReadAsStringAsync();
                    return JToken.Parse(apiResponse);
                }
            }
        }
    }
}