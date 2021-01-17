using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Servibes.Appointments.Core.Appointments;
using Servibes.Appointments.Core.Reservees;

namespace Servibes.Appointments.Infrastructure.Domain.Appointments
{
    public class AppointmentEntityTypeConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(x => x.AppointmentId);

            builder.Property(x => x.ReserveeId).HasColumnName("ReserveeId");
            builder.HasOne<Client>().WithMany().HasForeignKey(x => x.ReserveeId).IsRequired();
            builder.Property(x => x.CompanyId).HasColumnName("CompanyId");
            builder.HasOne<Company>().WithMany().HasForeignKey(x => x.CompanyId).IsRequired();
            builder.Property<string>("_cancellationReason").HasColumnName("CancellationReason");
            builder.Property(x => x.Status).HasColumnName("Status")
                .HasConversion(new EnumToStringConverter<AppointmentStatus>());

            builder.OwnsOne(x => x.Employee, b =>
            {
                b.Property(x => x.EmployeeId).HasColumnName("EmployeeId");
                b.Property(x => x.Name).HasColumnName("EmployeeName");
            });

            builder.OwnsOne(x => x.Service, b =>
            {
                b.Property(x => x.Name).HasColumnName("ServiceName");
                b.Property(x => x.Price).HasColumnName("ServicePrice");
            });

            builder.OwnsOne(x => x.ReservedDate, b =>
            {
                b.Property(x => x.Start).HasColumnName("Start");
                b.Property(x => x.End).HasColumnName("End");
            });
        }
    }
}
