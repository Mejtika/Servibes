using System;
using System.Collections.Generic;

namespace Servibes.Sales.Api.Models
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }

        public Guid CompanyId { get; set; }

        public string Name { get; set; }

        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}