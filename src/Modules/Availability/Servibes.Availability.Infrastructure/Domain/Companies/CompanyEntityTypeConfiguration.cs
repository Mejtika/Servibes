using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Shared;

namespace Servibes.Availability.Infrastructure.Domain.Companies
{
    class CompanyEntityTypeConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(x => x.CompanyId);
            builder.OwnsMany<HoursRange>("_openingHours", b =>
            {
                b.WithOwner().HasForeignKey("CompanyId");
                b.ToTable("OpeningHours");
                b.Property<Guid>("OpeningHoursId");
                b.HasKey("OpeningHoursId", "CompanyId");
                b.Property(p => p.IsAvailable).HasColumnName("IsAvailable");
                b.Property(p => p.DayOfWeek).HasColumnName("DayOfWeek")
                    .HasConversion(new EnumToStringConverter<DayOfWeek>());
                b.Property(p => p.Start).HasColumnName("Start");
                b.Property(p => p.End).HasColumnName("End");
            });
        }
    }
}
