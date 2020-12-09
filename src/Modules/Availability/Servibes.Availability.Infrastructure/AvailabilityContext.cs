using Microsoft.EntityFrameworkCore;

namespace Servibes.Availability.Infrastructure
{
    public class AvailabilityContext : DbContext
    {
        public AvailabilityContext(DbContextOptions<AvailabilityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
