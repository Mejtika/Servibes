using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Servibes.Sales.Api.Models;

namespace Servibes.Sales.Api
{
    public class SalesContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }

        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("sales");
            modelBuilder.Entity<Appointment>(b =>
            {
                b.Property(x => x.Status).HasConversion(new EnumToStringConverter<AppointmentStatus>());
            });
        }
    }
}