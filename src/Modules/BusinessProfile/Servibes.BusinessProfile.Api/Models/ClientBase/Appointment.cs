using System;

namespace Servibes.BusinessProfile.Api.Models.ClientBase
{
    public class Appointment
    {
        public Guid AppointmentId { get; set; }

        public Guid ClientId { get; set; }

        public Guid CompanyId { get; set; }
    }
}
