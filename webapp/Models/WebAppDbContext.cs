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

        public virtual DbSet<Organizations> Organizations { get; set; }
        public virtual DbSet<Suborganizations> Suborganizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<Organizations>(entity =>
            {
                entity.HasKey(e => e.Orgnr)
                    .HasName("organizations_pkey");

                entity.Property(e => e.Orgnr).ValueGeneratedNever();
            });

            modelBuilder.Entity<Suborganizations>(entity =>
            {
                entity.HasKey(e => e.Suborgnr)
                    .HasName("suborganizations_pkey");

                entity.Property(e => e.Suborgnr).ValueGeneratedNever();

                entity.HasOne(d => d.ParentorgnrNavigation)
                    .WithMany(p => p.Suborganizations)
                    .HasForeignKey(d => d.Parentorgnr)
                    .HasConstraintName("suborganizations_parentorgnr_fkey");
            });
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
