using Microsoft.EntityFrameworkCore;
using TitanGate.Website.Api.Domain.Entities;
using TitanGate.Website.Api.Repository.Configurations;

namespace TitanGate.Website.Api.Repository
{
    using Website = Domain.Entities.Website;

    public class RepositoriesContext : DbContext
    {
        public RepositoriesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Website> Websites { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Login> Logins { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new LoginConfiguration())
                .ApplyConfiguration(new CategoryConfiguration())
                .ApplyConfiguration(new WebsiteConfiguration())
                .ApplyConfiguration(new ClientConfiguration());
        }
    }
}
