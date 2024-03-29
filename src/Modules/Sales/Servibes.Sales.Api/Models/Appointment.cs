﻿using System;

namespace Servibes.Sales.Api.Models
{
    public class Appointment
    {
        public Guid AppointmentId { get; set; }

        public Guid ReserveeId { get; set; }

        public Guid CompanyId { get; set; }

        public Guid EmployeeId { get; set; }

        public string ServiceName { get; set; }

        public decimal Price { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public AppointmentStatus Status { get; set; }
    }
}
