using Microsoft.EntityFrameworkCore;
using Servibes.Appointments.Core.Appointments;
using Servibes.Appointments.Core.TimeReservations;

namespace Servibes.Appointments.Infrastructure
{
    public class AppointmentsContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; }

        public DbSet<TimeReservation> TimeReservations { get; set; }

        public AppointmentsContext(DbContextOptions<AppointmentsContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("appointments");
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }
    }
}
