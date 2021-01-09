using System;
using System.Collections.Generic;
using MediatR;

namespace Servibes.Appointments.Api
{
    public class GetCompanyTimeReservationsQuery : IRequest<List<TimeReservationDto>>
    {
        public Guid CompanyId { get; }

        public DateTime Date { get; }

        public GetCompanyTimeReservationsQuery(Guid companyId, DateTime date)
        {
            CompanyId = companyId;
            Date = date;
        }
    }
}