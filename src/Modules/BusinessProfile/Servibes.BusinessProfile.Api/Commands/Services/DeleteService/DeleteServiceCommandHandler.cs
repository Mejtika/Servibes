﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Services.DeleteService
{
    public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand>
    {
        private readonly BusinessProfileContext _context;

        public DeleteServiceCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public Task<Unit> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
        {
            var service = _context.Services.SingleOrDefault(s => s.ServiceId == request.ServiceId && s.CompanyId == request.CompanyId);

            if (service == null)
                throw new ArgumentException($"Service with id {request.ServiceId} and company id {request.CompanyId} doesnt exist.");

            _context.Services.Remove(service);
            _context.SaveChanges();

            return Unit.Task;
        }
    }
}
