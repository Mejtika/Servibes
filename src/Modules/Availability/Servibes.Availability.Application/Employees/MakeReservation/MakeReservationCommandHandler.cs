using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Availability.Core.Employees;
using Servibes.Shared.Communication.Events;
using Servibes.Shared.Services;

namespace Servibes.Availability.Application.Employees.MakeReservation
{
    public class MakeReservationCommandHandler : IRequestHandler<MakeReservationCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAvailabilityUnitOfWork _unitOfWork;
        private readonly IEventProcessor _eventProcessor;
        private readonly IDateTimeServer _dateTime;

        public MakeReservationCommandHandler(
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

        public async Task<Unit> Handle(MakeReservationCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

            if (employee == null)
            {
                throw new Exception($"Employee {request.EmployeeId} not found");
            }

            if (!employee.CheckCompanyCorrectness(request.CompanyId))
            {
                throw new Exception($"Employee doesn't work in company {request.CompanyId}");
            }

            var reservation = Reservation.Create(
                request.Start, 
                request.Start.AddMinutes(request.ServiceDuration),
                _dateTime.Now);
            var reservationSnapshot = CreateReservationSnapshot(request);

            employee.AddReservation(reservation, reservationSnapshot);

            await _unitOfWork.CommitAsync(cancellationToken);
            await _eventProcessor.ProcessAsync(employee.DomainEvents);
            return Unit.Value;
        }

        private static ReservationSnapshot CreateReservationSnapshot(MakeReservationCommand request)
        {
            return new ReservationSnapshot(
                request.CompanyId,
                request.EmployeeId,
                request.ReserveeId,
                request.EmployeeName,
                request.ServiceName,
                request.ServicePrice,
                request.Start,
                request.Start.AddMinutes(request.ServiceDuration));
        }
    }
}
