using Client.API.Infrastructure.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Client.API.Infrastructure
{
    public class ClientContext : DbContext
    {
        public ClientContext(DbContextOptions<ClientContext> options) : base(options)
        {
        }

        public DbSet<Models.Client> Clients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ClientContext).Assembly);
        }
    }
}
