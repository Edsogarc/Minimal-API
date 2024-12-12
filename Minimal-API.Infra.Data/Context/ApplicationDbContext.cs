using Microsoft.EntityFrameworkCore;
using Minimal_API.Domain.Entities;

namespace Minimal_API.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Veiculo> Veiculo { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}