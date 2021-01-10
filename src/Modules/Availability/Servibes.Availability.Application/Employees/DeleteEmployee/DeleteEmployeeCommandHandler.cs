using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Availability.Core.Employees;
using Servibes.Shared.Communication.Brokers;
using Servibes.Shared.Communication.Events;
using Servibes.Shared.Exceptions;
using Servibes.Shared.Services;

namespace Servibes.Availability.Application.Employees.DeleteEmployee
{
    public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAvailabilityUnitOfWork _unitOfWork;
        private readonly IEventProcessor _eventProcessor;
        private readonly IDateTimeServer _dateTimeServer;

        public DeleteEmployeeCommandHandler(
            IEmployeeRepository employeeRepository,
            IAvailabilityUnitOfWork unitOfWork,
            IEventProcessor eventProcessor,
            IDateTimeServer dateTimeServer)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _eventProcessor = eventProcessor;
            _dateTimeServer = dateTimeServer;
        }

        public async Task<Unit> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            if (employee == null)
            {
                throw new AppException($"Employee {request.EmployeeId} not found.");
            }

            if (!employee.CheckCompanyCorrectness(request.CompanyId))
            {
                throw new AppException($"Employee {request.EmployeeId} and company {request.CompanyId} are not match.");
            }

            employee.Delete(_dateTimeServer.Now);
            var @events = employee.DomainEvents;
            _employeeRepository.Delete(employee);
            await _unitOfWork.CommitAsync(cancellationToken);
            await _eventProcessor.ProcessAsync(@events);
            return Unit.Value;
        }
    }
}
