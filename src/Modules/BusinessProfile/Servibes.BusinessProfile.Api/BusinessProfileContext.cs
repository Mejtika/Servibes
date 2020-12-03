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

                //builder.OwnsMany(c => c.OpeningHours, b =>
                //{
                //    b.ToTable("OpeningHours");
                //    b.WithOwner().HasForeignKey("CompanyId");
                //    b.Property<int>("OpeningHourId");
                //    b.HasKey("OpeningHourId");
                //    b.Property(x => x.DayOfWeek).HasConversion(new EnumToStringConverter<DayOfWeek>());
                //});

                builder.OwnsOne(c => c.OpeningHours, b =>
                {
                    b.ToTable("OpeningHours");
                    b.WithOwner().HasForeignKey("CompanyId");
                    b.Property<int>("OpeningHourId");
                    b.HasKey("OpeningHourId");
                    b.OwnsOne<HoursRange>(x => x.Monday, builder => 
                    {
                        builder.Property(x => x.IsAvailable).HasColumnName("Monday_IsAvailable");
                        builder.Property(x => x.Start).HasColumnName("Monday_Start");
                        builder.Property(x => x.End).HasColumnName("Monday_End");
                    });
                    b.OwnsOne<HoursRange>(x => x.Tuesday, builder =>
                    {
                        builder.Property(x => x.IsAvailable).HasColumnName("Tuesday_IsAvailable");
                        builder.Property(x => x.Start).HasColumnName("Tuesday_Start");
                        builder.Property(x => x.End).HasColumnName("Tuesday_End");
                    }); b.OwnsOne<HoursRange>(x => x.Wednesday, builder =>
                    {
                        builder.Property(x => x.IsAvailable).HasColumnName("Wednesday_IsAvailable");
                        builder.Property(x => x.Start).HasColumnName("Wednesday_Start");
                        builder.Property(x => x.End).HasColumnName("Wednesday_End");
                    }); b.OwnsOne<HoursRange>(x => x.Thursday, builder =>
                    {
                        builder.Property(x => x.IsAvailable).HasColumnName("Thursday_IsAvailable");
                        builder.Property(x => x.Start).HasColumnName("Thursday_Start");
                        builder.Property(x => x.End).HasColumnName("Thursday_End");
                    }); b.OwnsOne<HoursRange>(x => x.Friday, builder =>
                    {
                        builder.Property(x => x.IsAvailable).HasColumnName("Friday_IsAvailable");
                        builder.Property(x => x.Start).HasColumnName("Friday_Start");
                        builder.Property(x => x.End).HasColumnName("Friday_End");
                    }); b.OwnsOne<HoursRange>(x => x.Saturday, builder =>
                    {
                        builder.Property(x => x.IsAvailable).HasColumnName("Saturday_IsAvailable");
                        builder.Property(x => x.Start).HasColumnName("Saturday_Start");
                        builder.Property(x => x.End).HasColumnName("Saturday_End");
                    }); b.OwnsOne<HoursRange>(x => x.Sunday, builder =>
                    {
                        builder.Property(x => x.IsAvailable).HasColumnName("Sunday_IsAvailable");
                        builder.Property(x => x.Start).HasColumnName("Sunday_Start");
                        builder.Property(x => x.End).HasColumnName("Sunday_End");
                    });
                });
            });

            modelBuilder.Entity<Service>(builder =>
            {
                builder.OwnsMany(x => x.Performers, b =>
                {
                    b.Property<Guid>("Id");
                    b.HasKey("Id");
                    b.ToTable("Performers");
                    b.WithOwner().HasForeignKey("ServiceId");
                });

                builder.HasOne<Company>().WithMany().HasForeignKey("CompanyId").IsRequired();
            });

            modelBuilder.Entity<Employee>(builder =>
            {
                //builder.OwnsMany(e => e.WorkingHours, b =>
                //{
                //    b.ToTable("WorkingHours");
                //    b.WithOwner().HasForeignKey("EmployeeId");
                //    b.Property<int>("WorkingHourId");
                //    b.HasKey("WorkingHourId");
                //    b.Property(x => x.DayOfWeek).HasConversion(new EnumToStringConverter<DayOfWeek>());
                //});

                builder.OwnsOne(c => c.WorkingHours, b =>
                {
                    b.ToTable("WorkingHours");
                    b.WithOwner().HasForeignKey("EmployeeId");
                    b.Property<int>("WorkingHoursId");
                    b.HasKey("WorkingHoursId");
                    b.OwnsOne<HoursRange>(x => x.Monday, builder =>
                    {
                        builder.Property(x => x.IsAvailable).HasColumnName("Monday_IsAvailable");
                        builder.Property(x => x.Start).HasColumnName("Monday_Start");
                        builder.Property(x => x.End).HasColumnName("Monday_End");
                    });
                    b.OwnsOne<HoursRange>(x => x.Tuesday, builder =>
                    {
                        builder.Property(x => x.IsAvailable).HasColumnName("Tuesday_IsAvailable");
                        builder.Property(x => x.Start).HasColumnName("Tuesday_Start");
                        builder.Property(x => x.End).HasColumnName("Tuesday_End");
                    }); b.OwnsOne<HoursRange>(x => x.Wednesday, builder =>
                    {
                        builder.Property(x => x.IsAvailable).HasColumnName("Wednesday_IsAvailable");
                        builder.Property(x => x.Start).HasColumnName("Wednesday_Start");
                        builder.Property(x => x.End).HasColumnName("Wednesday_End");
                    }); b.OwnsOne<HoursRange>(x => x.Thursday, builder =>
                    {
                        builder.Property(x => x.IsAvailable).HasColumnName("Thursday_IsAvailable");
                        builder.Property(x => x.Start).HasColumnName("Thursday_Start");
                        builder.Property(x => x.End).HasColumnName("Thursday_End");
                    }); b.OwnsOne<HoursRange>(x => x.Friday, builder =>
                    {
                        builder.Property(x => x.IsAvailable).HasColumnName("Friday_IsAvailable");
                        builder.Property(x => x.Start).HasColumnName("Friday_Start");
                        builder.Property(x => x.End).HasColumnName("Friday_End");
                    }); b.OwnsOne<HoursRange>(x => x.Saturday, builder =>
                    {
                        builder.Property(x => x.IsAvailable).HasColumnName("Saturday_IsAvailable");
                        builder.Property(x => x.Start).HasColumnName("Saturday_Start");
                        builder.Property(x => x.End).HasColumnName("Saturday_End");
                    }); b.OwnsOne<HoursRange>(x => x.Sunday, builder =>
                    {
                        builder.Property(x => x.IsAvailable).HasColumnName("Sunday_IsAvailable");
                        builder.Property(x => x.Start).HasColumnName("Sunday_Start");
                        builder.Property(x => x.End).HasColumnName("Sunday_End");
                    });
                });

                builder.HasOne<Company>().WithMany().HasForeignKey("CompanyId").IsRequired();

            });
        }
    }
}
