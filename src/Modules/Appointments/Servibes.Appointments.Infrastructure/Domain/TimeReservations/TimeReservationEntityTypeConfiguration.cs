using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Servibes.Appointments.Core.Shared;
using Servibes.Appointments.Core.TimeReservations;

namespace Servibes.Appointments.Infrastructure.Domain.TimeReservations
{
    public class TimeReservationEntityTypeConfiguration : IEntityTypeConfiguration<TimeReservation>
    {
        public void Configure(EntityTypeBuilder<TimeReservation> builder)
        {
            builder.HasKey(x => x.TimeReservationId);
            builder.Property<Guid>("_employeeId").HasColumnName("EmployeeId");
            builder.Property<Guid>("_companyId").HasColumnName("CompanyId");
            builder.Property<TimeReservationStatus>("_status").HasColumnName("Status")
                .HasConversion(new EnumToStringConverter<TimeReservationStatus>()); 

            builder.OwnsOne<ReservationDate>("_reservedDate", b =>
            {
                b.Property(x => x.Start).HasColumnName("Start");
                b.Property(x => x.End).HasColumnName("End");
            });
        }
    }
}
