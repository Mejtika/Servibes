using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Servibes.Sales.Api.Models;

namespace Servibes.Sales.Api
{
    public class SalesContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<WalkInClient> WalkInClients { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Company> Companies  { get; set; }

        public SalesContext(DbContextOptions<SalesContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("sales");
            modelBuilder.Entity<Appointment>(b =>
            {
                b.Property(x => x.Status).HasConversion(new EnumToStringConverter<AppointmentStatus>());
                b.HasOne<Company>().WithMany(x => x.Appointments).HasForeignKey(x => x.CompanyId).IsRequired();
                b.HasOne<Employee>().WithMany(x => x.Appointments).HasForeignKey(x => x.EmployeeId).IsRequired();
            });
        }
    }
}