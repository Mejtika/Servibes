using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Servibes.Appointments.Core.Appointments;
using Servibes.Appointments.Core.Shared;

namespace Servibes.Appointments.Infrastructure.Domain.Appointments
{
    public class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(x => x.AppointmentId);

            builder.Property<Guid>("_reserveeId").HasColumnName("ReserveeId");
            builder.Property<Guid>("_companyId").HasColumnName("CompanyId");
            builder.Property<string>("_cancellationReason").HasColumnName("CancellationReason");
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
        }
    }
}
