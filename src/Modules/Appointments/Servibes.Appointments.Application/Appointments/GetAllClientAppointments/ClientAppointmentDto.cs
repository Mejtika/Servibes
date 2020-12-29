using System;

namespace Servibes.Appointments.Application.Appointments.GetAllClientAppointments
{
    public class ClientAppointmentDto
    {
        public Guid AppointmentId { get; set; }

        public Guid CompanyId { get; set; }

        public string Status { get; set; }

        public Guid EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string ServiceName { get; set; }

        public decimal ServicePrice { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string CancellationReason { get; set; }
    }
}