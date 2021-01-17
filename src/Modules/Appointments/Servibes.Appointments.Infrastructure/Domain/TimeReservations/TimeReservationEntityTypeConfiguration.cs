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
            builder.Property(x => x.EmployeeId).HasColumnName("EmployeeId");
            builder.Property(x => x.CompanyId).HasColumnName("CompanyId");
            builder.Property(x => x.Status).HasColumnName("Status")
                .HasConversion(new EnumToStringConverter<TimeReservationStatus>()); 

            builder.OwnsOne(x => x.ReservedDate, b =>
            {
                b.Property(x => x.Start).HasColumnName("Start");
                b.Property(x => x.End).HasColumnName("End");
            });
        }
    }
}
