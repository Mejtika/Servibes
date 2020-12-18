using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Availability.Application.ModuleClients;
using Servibes.Availability.Core.Employees;
using Servibes.Shared.Communication.Events;
using Servibes.Shared.Exceptions;
using Servibes.Shared.Services;

namespace Servibes.Availability.Application.Employees.MakeReservation
{
    public class MakeReservationCommandHandler : IRequestHandler<MakeReservationCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAvailabilityUnitOfWork _unitOfWork;
        private readonly IEventProcessor _eventProcessor;
        private readonly IDateTimeServer _dateTime;
        private readonly IReservationClient _reservationClient;

        public MakeReservationCommandHandler(
            IEmployeeRepository employeeRepository,
            IAvailabilityUnitOfWork unitOfWork,
            IEventProcessor eventProcessor,
            IDateTimeServer dateTime,
            IReservationClient reservationClient)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _eventProcessor = eventProcessor;
            _dateTime = dateTime;
            _reservationClient = reservationClient;
        }

        public async Task<Unit> Handle(MakeReservationCommand request, CancellationToken cancellationToken)
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

            var reservationDataDto = await _reservationClient.GetReservationData(request.EmployeeId, request.ServiceId);

            var reservationSnapshot = CreateReservationSnapshot(request, reservationDataDto);

            var reservation = Reservation.Create(
                request.Start, 
                request.Start.AddMinutes(reservationDataDto.ServiceDuration),
                _dateTime.Now);

            employee.AddReservation(reservation, reservationSnapshot);

            await _unitOfWork.CommitAsync(cancellationToken);
            await _eventProcessor.ProcessAsync(employee.DomainEvents);
            return Unit.Value;
        }

        private ReservationSnapshot CreateReservationSnapshot(
            MakeReservationCommand request, 
            ReservationDataDto reservationDataDto)
        {
            return new ReservationSnapshot(
                request.CompanyId,
                request.EmployeeId,
                request.ReserveeId,
                reservationDataDto.EmployeeName,
                reservationDataDto.ServiceName,
                reservationDataDto.ServicePrice,
                request.Start,
                request.Start.AddMinutes(reservationDataDto.ServiceDuration));
        }
    }
}
