using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Employees;
using Servibes.Availability.Core.Shared;

namespace Servibes.Availability.Infrastructure.Domain.Employees
{
    public class EmployeeEntityTypeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(x => x.EmployeeId);
            builder.Property<Guid>("_companyId").HasColumnName("CompanyId");
            builder.HasOne<Company>().WithMany().HasForeignKey("_companyId");
            builder.OwnsMany<HoursRange>("_workingHours", b =>
            {
                b.WithOwner().HasForeignKey("EmployeeId");
                b.ToTable("WorkingHours");
                b.Property<Guid>("WorkingHourId");
                b.HasKey("WorkingHourId", "EmployeeId");
                b.Property(p => p.IsAvailable).HasColumnName("IsAvailable");
                b.Property(p => p.DayOfWeek).HasColumnName("DayOfWeek")
                    .HasConversion(new EnumToStringConverter<DayOfWeek>());
                b.Property(p => p.Start).HasColumnName("Start");
                b.Property(p => p.End).HasColumnName("End");
            });

            builder.OwnsMany<Reservation>("_reservations", b =>
            {
                b.WithOwner().HasForeignKey("EmployeeId");
                b.ToTable("Reservations");
                b.Property<Guid>("ReservationId");
                b.HasKey("ReservationId", "EmployeeId");
                b.Property(p => p.Start).HasColumnName("Start");
                b.Property(p => p.End).HasColumnName("End");
            });

            builder.OwnsMany<TimeOff>("_timeOffs", b =>
            {
                b.WithOwner().HasForeignKey("EmployeeId");
                b.ToTable("TimeOffs");
                b.Property<Guid>("TimeOffId");
                b.HasKey("TimeOffId", "EmployeeId");
                b.Property(p => p.Start).HasColumnName("Start");
                b.Property(p => p.End).HasColumnName("End");
            });
        }
    }
}
