﻿using System;

namespace Servibes.Availability.Core.Employees
{
    public class ReservationSnapshot
    {
        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public Guid ReserveeId { get; }

        public string EmployeeName { get; }

        public string ServiceName { get; }

        public decimal ServicePrice { get; }

        public DateTime Start { get; }

        public DateTime End { get; }


        public ReservationSnapshot(
            Guid companyId,
            Guid employeeId,
            Guid reserveeId,
            string employeeName,
            string serviceName,
            decimal servicePrice,
            DateTime start, 
            DateTime end)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
            ReserveeId = reserveeId;
            EmployeeName = employeeName;
            ServiceName = serviceName;
            ServicePrice = servicePrice;
            Start = start;
            End = end;
        }
    }
}
