using Servibes.Shared.BuildingBlocks;

namespace Servibes.Appointments.Core.Appointments.Events
{
    public class AppointmentStateChanged : IDomainEvent
    {
        public Appointment Appointment { get; }

        public AppointmentStateChanged(Appointment appointment)
        {
            Appointment = appointment;
        }
    }
}