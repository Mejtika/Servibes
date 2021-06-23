using MediatR;
using Servibes.Availability.Core.Companies;
using Servibes.Availability.Core.Shared;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Servibes.Availability.Core.Employees;
using Servibes.Shared.Exceptions;

namespace Servibes.Availability.Application.Companies.ChangeOpeningHours
{
    public class ChangeOpeningHoursCommandHandler : IRequestHandler<ChangeOpeningHoursCommand>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IAvailabilityUnitOfWork _unitOfWork;
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IHoursChanger _hoursChanger;

        public ChangeOpeningHoursCommandHandler(
            ICompanyRepository companyRepository,
            IAvailabilityUnitOfWork unitOfWork,
            IEmployeesRepository employeesRepository,
            IHoursChanger hoursChanger)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
            _employeesRepository = employeesRepository;
            _hoursChanger = hoursChanger;
        }

        public async Task<Unit> Handle(ChangeOpeningHoursCommand request, CancellationToken cancellationToken)
        {
            var company = await _companyRepository.GetByIdAsync(request.CompanyId);

            if (company == null)
            {
                throw new AppException($"Company with id {request.CompanyId} not found.");
            }

            var newOpeningHours = request.OpeningHours.Select(x =>
                    HoursRange.Create(x.DayOfWeek, x.IsAvailable, TimeSpan.Parse(x.Start), TimeSpan.Parse(x.End))).ToList();

            var employees = await _employeesRepository.GetAllByCompanyIdAsync(company.CompanyId);

            _hoursChanger.Update(company, employees, newOpeningHours);

            await _unitOfWork.CommitAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
