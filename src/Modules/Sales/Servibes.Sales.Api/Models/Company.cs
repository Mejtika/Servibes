using System;
using System.Collections.Generic;

namespace Servibes.Sales.Api.Models
{
    public class Company
    {
        public Guid CompanyId { get; set; }

        public Guid WalkInClientId { get; set; }

        public Guid OwnerId { get; set; }

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
