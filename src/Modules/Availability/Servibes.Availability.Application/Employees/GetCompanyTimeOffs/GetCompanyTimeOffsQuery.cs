using System;
using System.Collections.Generic;
using MediatR;

namespace Servibes.Availability.Application.Employees.GetCompanyTimeOffs
{
    public class GetCompanyTimeOffsQuery : IRequest<List<CompanyTimeOffDto>>
    {
        public Guid CompanyId { get; }

        public DateTime Date { get; }

        public GetCompanyTimeOffsQuery(Guid companyId, DateTime date)
        {
            CompanyId = companyId;
            Date = date;
        }
    }
}