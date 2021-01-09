using System;
using System.Collections.Generic;
using MediatR;

namespace Servibes.Appointments.Api
{
    public class GetCompanyAppointmentsQuery : IRequest<List<AppointmentDto>>
    {
        public Guid CompanyId { get; }

        public DateTime Date { get; }

        public GetCompanyAppointmentsQuery(Guid companyId, DateTime date)
        {
            CompanyId = companyId;
            Date = date;
        }
    }
}