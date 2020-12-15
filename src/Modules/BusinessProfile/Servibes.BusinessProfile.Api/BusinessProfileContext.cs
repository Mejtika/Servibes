using System;
using Microsoft.EntityFrameworkCore;
using Servibes.BusinessProfile.Api.Models;
using Servibes.BusinessProfile.Api.Models.ClientBase;

namespace Servibes.BusinessProfile.Api
{
    public class BusinessProfileContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Client> Clients { get; set; }

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
                builder.HasOne<Company>().WithMany().HasForeignKey("CompanyId").IsRequired();
            });

            modelBuilder.Entity<Appointment>(builder =>
            {
                builder.ToTable("Appointments");
                builder.HasOne<Company>().WithMany().HasForeignKey(x => x.CompanyId).IsRequired();
            });
        }
    }
}
