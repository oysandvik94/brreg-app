using Microsoft.EntityFrameworkCore;

namespace webapp.Context
{
    public class WebContext : DbContext
    {
        
        public WebContext(DbContextOptions<WebContext> options):base(options) {  }
    }
}