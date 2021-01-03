using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Employees;
using Servibes.Availability.Core.Shared;

namespace Servibes.Availability.Application.Employees.ChangeWorkingHours
{
    public class ChangeWorkingHoursCommandHandler : IRequestHandler<ChangeWorkingHoursCommand>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IAvailabilityUnitOfWork _unitOfWork;

        public ChangeWorkingHoursCommandHandler(
            IEmployeeRepository employeeRepository,
            ICompanyRepository companyRepository,
            IAvailabilityUnitOfWork unitOfWork)
        {
            _employeeRepository = employeeRepository;
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(ChangeWorkingHoursCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);
            var newWorkingHours = request.WorkingHours.Select(x =>
                HoursRange.Create(x.DayOfWeek, x.IsAvailable, TimeSpan.Parse(x.Start), TimeSpan.Parse(x.End))).ToList();
            employee.ChangeWorkingHours(company.GetOpeningHours(), newWorkingHours);
            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}