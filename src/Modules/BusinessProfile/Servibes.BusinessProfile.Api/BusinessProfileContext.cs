using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Servibes.BusinessProfile.Api.Model;
using Servibes.BusinessProfile.Api.Model.ValueObjects;
using DayOfWeek = Servibes.Shared.Enumerations.DayOfWeek;


namespace Servibes.BusinessProfile.Api
{
    public class BusinessProfileContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Service> Services { get; set; }

        public BusinessProfileContext(DbContextOptions<BusinessProfileContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("business");
            modelBuilder.Entity<Company>(builder =>
            {
                builder.OwnsOne(c => c.PhoneNumber, b => { b.Property(x => x.Value).HasColumnName("PhoneNumber"); });

                builder.OwnsOne(c => c.Address, b =>
                {
                    b.Property(x => x.City).HasColumnName("City");
                    b.Property(x => x.Street).HasColumnName("Street");
                    b.Property(x => x.StreetNumber).HasColumnName("StreetNumber");
                    b.Property(x => x.ZipCode).HasColumnName("ZipCode");
                    b.Property(x => x.FlatNumber).HasColumnName("FlatNumber");
                });

                builder.OwnsMany(c => c.OpeningHours, b =>
                {
                    b.ToTable("OpeningHours");
                    b.WithOwner().HasForeignKey("CompanyId");
                    b.Property<int>("OpeningHourId");
                    b.HasKey("OpeningHourId");
                    b.Property(x => x.DayOfWeek).HasConversion(new EnumToStringConverter<DayOfWeek>());
                });

                //builder.HasMany(c => c.Employees).WithOne().HasForeignKey("CompanyId");
                //builder.HasMany(c => c.Services).WithOne().HasForeignKey("CompanyId");

            });

            modelBuilder.Entity<Service>(builder =>
            {
                builder.HasMany(c => c.Employees).WithOne().HasForeignKey("ServiceId");
                builder.HasOne<Company>().WithMany().HasForeignKey("CompanyId").IsRequired();
            });

            modelBuilder.Entity<Employee>(builder =>
            {
                builder.OwnsMany(e => e.WorkingHours, b =>
                {
                    b.ToTable("WorkingHours");
                    b.WithOwner().HasForeignKey("EmployeeId");
                    b.Property<int>("WorkingHourId");
                    b.HasKey("WorkingHourId");
                    b.Property(x => x.DayOfWeek).HasConversion(new EnumToStringConverter<DayOfWeek>());
                });

                builder.HasOne<Company>().WithMany().HasForeignKey("CompanyId").IsRequired();

            });
        }
    }
}
