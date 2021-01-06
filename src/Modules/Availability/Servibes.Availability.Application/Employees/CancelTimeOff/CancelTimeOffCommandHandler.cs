using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Availability.Core.Employees;
using Servibes.Shared.Communication.Events;
using Servibes.Shared.Exceptions;
using Servibes.Shared.Services;

namespace Servibes.Availability.Application.Employees.CancelTimeOff
{
    public class CancelTimeOffCommandHandler : IRequestHandler<CancelTimeOffCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAvailabilityUnitOfWork _unitOfWork;
        private readonly IEventProcessor _eventProcessor;
        private readonly IDateTimeServer _dateTime;

        public CancelTimeOffCommandHandler(
            IEmployeeRepository employeeRepository,
            IAvailabilityUnitOfWork unitOfWork,
            IEventProcessor eventProcessor,
            IDateTimeServer dateTime)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _eventProcessor = eventProcessor;
            _dateTime = dateTime;
        }

        public async Task<Unit> Handle(CancelTimeOffCommand request, CancellationToken cancellationToken)
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

            var timeOff = employee.GetTimeOffByDate(request.Start);
            if (timeOff == null)
            {
                throw new AppException($"Time off with start date {request.Start} not found.");
            }

            employee.ReleaseTimeOff(timeOff);

            await _unitOfWork.CommitAsync(cancellationToken);
            await _eventProcessor.ProcessAsync(employee.DomainEvents);
            return Unit.Value;
        }
    }
}