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

        public virtual DbSet<BusinesscodeOrg> BusinesscodeOrg { get; set; }
        public virtual DbSet<BusinesscodeSuborg> BusinesscodeSuborg { get; set; }
        public virtual DbSet<Businesscodes> Businesscodes { get; set; }
        public virtual DbSet<Organizations> Organizations { get; set; }
        public virtual DbSet<Suborganizations> Suborganizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<BusinesscodeOrg>(entity =>
            {
                entity.HasKey(e => new { e.Businesscode, e.Orgnr })
                    .HasName("businesscode_org_pkey");

                entity.HasOne(d => d.BusinesscodeNavigation)
                    .WithMany(p => p.BusinesscodeOrg)
                    .HasForeignKey(d => d.Businesscode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("businesscode_org_businesscode_fkey");

                entity.HasOne(d => d.OrgnrNavigation)
                    .WithMany(p => p.BusinesscodeOrg)
                    .HasForeignKey(d => d.Orgnr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("businesscode_org_orgnr_fkey");
            });

            modelBuilder.Entity<BusinesscodeSuborg>(entity =>
            {
                entity.HasKey(e => new { e.Businesscode, e.Suborgnr })
                    .HasName("businesscode_suborg_pkey");

                entity.HasOne(d => d.BusinesscodeNavigation)
                    .WithMany(p => p.BusinesscodeSuborg)
                    .HasForeignKey(d => d.Businesscode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("businesscode_suborg_businesscode_fkey");

                entity.HasOne(d => d.SuborgnrNavigation)
                    .WithMany(p => p.BusinesscodeSuborg)
                    .HasForeignKey(d => d.Suborgnr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("businesscode_suborg_suborgnr_fkey");
            });

            modelBuilder.Entity<Businesscodes>(entity =>
            {
                entity.HasKey(e => e.Businesscode)
                    .HasName("businesscodes_pkey");
            });

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
