﻿using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Availability.Core.Employees;
using Servibes.Shared.Communication.Events;
using Servibes.Shared.Services;

namespace Servibes.Availability.Application.Employees.MakeTimeReservation
{
    public class MakeTimeReservationCommandHandler : IRequestHandler<MakeTimeReservationCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAvailabilityUnitOfWork _unitOfWork;
        private readonly IEventProcessor _eventProcessor;
        private readonly IDateTimeServer _dateTime;


        public MakeTimeReservationCommandHandler(
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

        public async Task<Unit> Handle(MakeTimeReservationCommand request, CancellationToken cancellationToken)
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
                request.End,
                _dateTime.Now);

            employee.AddReservation(reservation, null);

            await _unitOfWork.CommitAsync(cancellationToken);
            await _eventProcessor.ProcessAsync(employee.DomainEvents);
            return Unit.Value;
        }
    }
}