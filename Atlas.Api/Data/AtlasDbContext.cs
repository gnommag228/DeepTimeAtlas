using Microsoft.EntityFrameworkCore;
using Atlas.Domain;
using Atlas.Domain.Entities;

namespace Atlas.Api.Data
{
    public class AtlasDbContext : DbContext
    {
        public AtlasDbContext(DbContextOptions<AtlasDbContext> options) : base(options)
        {
        }

        public DbSet<Artifact> Artifacts { get; set; }
        public DbSet<Species> Species => Set<Species>();
        public DbSet<Site> Site => Set<Site>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=atlas_db;Username=postgres;Password=mysecretpassword");
            }
        }
    }
}
