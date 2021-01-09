using System;
using MediatR;

namespace Servibes.Appointments.Application.TimeReservations.CancelBusinessTimeReservation
{
    public class CancelBusinessTimeReservationCommand : IRequest
    {
        public Guid CompanyId { get; }

        public Guid TimeReservationId { get; }

        public CancelBusinessTimeReservationCommand(Guid companyId, Guid timeReservationId)
        {
            CompanyId = companyId;
            TimeReservationId = timeReservationId;
        }
    }
}