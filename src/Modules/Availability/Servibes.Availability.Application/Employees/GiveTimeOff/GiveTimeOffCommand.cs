﻿using System;
using MediatR;

namespace Servibes.Availability.Application.Employees.GiveTimeOff
{
    public class GiveTimeOffCommand : IRequest
    {
        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public DateTime Start { get; }

        public DateTime End { get; }

        public GiveTimeOffCommand(Guid companyId, Guid employeeId, DateTime start, DateTime end)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
            Start = start;
            End = end;
        }
    }
}