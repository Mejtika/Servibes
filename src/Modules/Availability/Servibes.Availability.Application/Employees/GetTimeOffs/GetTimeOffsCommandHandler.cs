using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Availability.Core.Employees;
using Servibes.Shared.Exceptions;
using Servibes.Shared.Services;

namespace Servibes.Availability.Application.Employees.GetTimeOffs
{
    public class GetTimeOffsCommandHandler : IRequestHandler<GetTimeOffsCommand, List<TimeOffDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDateTimeServer _dateTimeServer;

        public GetTimeOffsCommandHandler(
            IEmployeeRepository employeeRepository,
            IDateTimeServer dateTimeServer)
        {
            _employeeRepository = employeeRepository;
            _dateTimeServer = dateTimeServer;
        }

        public async Task<List<TimeOffDto>> Handle(GetTimeOffsCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (employee == null)
            {
                throw new AppException($"Employee with id {request.EmployeeId} not found.");
            }

            if (!employee.CheckCompanyCorrectness(request.CompanyId))
            {
                throw new AppException($"Employee {request.EmployeeId} and company {request.CompanyId} are not match.");
            }

            var timeOffs = employee.GetTimeOffs(_dateTimeServer.Now);

            return timeOffs.Select(x => new TimeOffDto {Start = x.Start, End = x.End}).ToList();
        }
    }
}