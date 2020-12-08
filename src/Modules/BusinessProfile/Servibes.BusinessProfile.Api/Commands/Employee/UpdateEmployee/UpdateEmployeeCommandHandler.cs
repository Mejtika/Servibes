using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Servibes.BusinessProfile.Api.Commands.Employee.UpdateEmployee
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand>
    {
        private readonly BusinessProfileContext context;

        public UpdateEmployeeCommandHandler(BusinessProfileContext context)
        {
            this.context = context;
        }

        public Task<Unit> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = context.Employees.FirstOrDefault(e => e.EmployeeId == request.EmployeeId && e.CompanyId == request.CompanyId);

            if (employee == null)
                throw new ArgumentException($"Employee with id {request.EmployeeId} and company id {request.CompanyId} doesn't exist.");

            employee.FirstName = request.EmployeeForUpdateDto.FirstName;
            employee.LastName = request.EmployeeForUpdateDto.LastName;

            //TODO: Send event with employee and new working hours

            context.Employees.Update(employee);
            context.SaveChanges();

            return Unit.Task;
        }
    }
}
