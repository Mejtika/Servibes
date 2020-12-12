using System;
using MediatR;

namespace Servibes.Appointments.Application.Appointments.MakeAppointment
{
    public class MakeAppointmentCommand : IRequest
    {
        public Guid CompanyId { get; }

        public Guid EmployeeId { get; }

        public string EmployeeName { get; }

        public string ServiceName { get; }

        public decimal ServicePrive { get; }

        public int ServiceDuration { get; }

        public DateTime Start { get; }

        public MakeAppointmentCommand(Guid companyId, Guid employeeId, string employeeName, string serviceName, decimal servicePrive, int serviceDuration, DateTime start)
        {
            CompanyId = companyId;
            EmployeeId = employeeId;
            EmployeeName = employeeName;
            ServiceName = serviceName;
            ServicePrive = servicePrive;
            ServiceDuration = serviceDuration;
            Start = start;
        }
    }
}
