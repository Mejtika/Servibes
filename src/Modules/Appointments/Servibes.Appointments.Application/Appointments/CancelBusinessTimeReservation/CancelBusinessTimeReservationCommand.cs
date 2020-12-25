using System;
using MediatR;

namespace Servibes.Appointments.Application.Appointments.CancelBusinessTimeReservation
{
    public class CancelBusinessTimeReservationCommand : IRequest
    {
        public Guid TimeReservationId { get; }

        public CancelBusinessTimeReservationCommand(Guid timeReservationId)
        {
            TimeReservationId = timeReservationId;
        }
    }
}