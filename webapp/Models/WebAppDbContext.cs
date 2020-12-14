using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace webapp.Models
{
    public partial class WebAppDbContext : DbContext
    {
        public WebAppDbContext()
        {
        }

        public WebAppDbContext(DbContextOptions<WebAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Businesscode> Businesscodes { get; set; }
        public virtual DbSet<Organization> Organizations { get; set; }
        public virtual DbSet<Suborganization> Suborganizations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // TODO: Plaintext secrets are not extracted for simplicity
                optionsBuilder.UseNpgsql("Host=localhost;Database=brregdb;Username=postgres;Password=postgres;Port=5555");
            }
        }
    }
}
