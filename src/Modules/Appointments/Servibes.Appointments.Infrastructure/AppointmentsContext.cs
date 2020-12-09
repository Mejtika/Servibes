using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Servibes.Appointments.Core.Appointments;
using Servibes.Appointments.Core.Shared;
using Servibes.Appointments.Core.TimeReservation;

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

            modelBuilder.Entity<Appointment>(builder =>
            {
                builder.HasKey(x => x.AppointmentId);

                builder.Property<Guid>("_reserveeId").HasColumnName("ReserveeId");
                builder.Property<Guid>("_companyId").HasColumnName("CompanyId");
                builder.Property<AppointmentStatus>("_status").HasColumnName("Status")
                    .HasConversion(new EnumToStringConverter<AppointmentStatus>());

                builder.OwnsOne<Employee>("_employee", b =>
                {
                    b.Property(x => x.EmployeeId).HasColumnName("EmployeeId");
                    b.Property(x => x.Name).HasColumnName("EmployeeName");
                });

                builder.OwnsOne<Service>("_service", b =>
                {
                    b.Property(x => x.Name).HasColumnName("ServiceName");
                    b.Property(x => x.Price).HasColumnName("ServicePrice");
                });

                builder.OwnsOne<ReservationDate>("_reservedDate", b =>
                {
                    b.Property(x => x.Start).HasColumnName("Start");
                    b.Property(x => x.End).HasColumnName("End");
                });
            });

            modelBuilder.Entity<TimeReservation>(builder =>
            {
                builder.HasKey(x => x.TimeReservationId);
                builder.Property<Guid>("_employeeId").HasColumnName("EmployeeId");
                builder.Property<Guid>("_companyId").HasColumnName("CompanyId");
                builder.Property<bool>("_isCanceled").HasColumnName("IsCanceled");

                builder.OwnsOne<ReservationDate>("_reservedDate", b =>
                {
                    b.Property(x => x.Start).HasColumnName("Start");
                    b.Property(x => x.End).HasColumnName("End");
                });
            });
        }
    }
}
