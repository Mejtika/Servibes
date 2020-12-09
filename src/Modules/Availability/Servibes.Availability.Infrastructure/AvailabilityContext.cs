using Microsoft.EntityFrameworkCore;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Employees;

namespace Servibes.Availability.Infrastructure
{
    public class AvailabilityContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Company> Companies { get; set; }

        public AvailabilityContext(DbContextOptions<AvailabilityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("availability");
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
