using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Employees;
using Servibes.Availability.Core.Shared;

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

            modelBuilder.Entity<Employee>(builder =>
            {
                builder.HasKey(x => x.EmployeeId);
                builder.Property<Guid>("_companyId").HasColumnName("CompanyId");
                builder.HasOne<Company>().WithMany().HasForeignKey("_companyId");
                builder.OwnsMany<HoursRange>("_workingHours", y =>
                {
                    y.WithOwner().HasForeignKey("EmployeeId");
                    y.ToTable("WorkingHours");
                    y.Property<Guid>("WorkingHourId");
                    y.HasKey("WorkingHourId");
                    y.Property(p => p.IsAvailable).HasColumnName("IsAvailable");
                    y.Property(p => p.DayOfWeek).HasColumnName("DayOfWeek")
                        .HasConversion(new EnumToStringConverter<DayOfWeek>());
                    y.Property(p => p.Start).HasColumnName("Start");
                    y.Property(p => p.End).HasColumnName("End");
                });

                builder.OwnsMany<Reservation>("_reservations", b =>
                {
                    b.WithOwner().HasForeignKey("EmployeeId");
                    b.ToTable("Reservations");
                    b.Property<Guid>("ReservationId");
                    b.HasKey("ReservationId");
                    b.Property(p => p.Start).HasColumnName("Start");
                    b.Property(p => p.End).HasColumnName("End");
                });

                builder.OwnsMany<TimeOff>("_timeOffs", b =>
                {
                    b.WithOwner().HasForeignKey("EmployeeId");
                    b.ToTable("TimeOffs");
                    b.Property<Guid>("TimeOffId");
                    b.HasKey("TimeOffId");
                    b.Property(p => p.Start).HasColumnName("Start");
                    b.Property(p => p.End).HasColumnName("End");
                });
            });

            modelBuilder.Entity<Company>(builder =>
            {
                builder.HasKey(x => x.CompanyId);
                builder.OwnsMany<HoursRange>("_openingHours", y =>
                {
                    y.WithOwner().HasForeignKey("CompanyId");
                    y.ToTable("OpeningHours");
                    y.Property<Guid>("OpeningHoursId");
                    y.HasKey("OpeningHoursId");
                    y.Property(p => p.IsAvailable).HasColumnName("IsAvailable");
                    y.Property(p => p.DayOfWeek).HasColumnName("DayOfWeek")
                        .HasConversion(new EnumToStringConverter<DayOfWeek>());
                    y.Property(p => p.Start).HasColumnName("Start");
                    y.Property(p => p.End).HasColumnName("End");
                });
            });
        }
    }
}
