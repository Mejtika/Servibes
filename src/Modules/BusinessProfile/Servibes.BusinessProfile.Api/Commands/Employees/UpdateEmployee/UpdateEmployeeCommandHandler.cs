﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Servibes.BusinessProfile.Api.Commands.Employees.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly BusinessProfileContext _context;

        public UpdateEmployeeCommandHandler(BusinessProfileContext context)
        {
            this._context = context;
        }

        public Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = _context.Employees.SingleOrDefault(e => e.EmployeeId == request.EmployeeId && e.CompanyId == request.CompanyId);

            if (employee == null)
                throw new ArgumentException($"Employee with id {request.EmployeeId} and company id {request.CompanyId} doesn't exist.");

            employee.FirstName = request.EmployeeForUpdateDto.FirstName;
            employee.LastName = request.EmployeeForUpdateDto.LastName;

            _context.SaveChanges();

            return Unit.Task;
        }
    }
}