using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Servibes.Availability.Application.Events.External
{
    public class RegistrationCompleted : INotification
    {
        public RegistrationCompleted(List<HoursRangeDto> hoursRangeDtos, List<Guid> employeeId, Guid companyId)
        {
            HoursRangeDtos = hoursRangeDtos;
            EmployeeId = employeeId;
            CompanyId = companyId;
        }

        public Guid CompanyId { get; }
        public List<Guid> EmployeeId { get; }
        public List<HoursRangeDto> HoursRangeDtos { get; }
    }

    public class RegistrationCompletedHandler : INotificationHandler<RegistrationCompleted>
    {
        private readonly IMediator _mediator;

        public RegistrationCompletedHandler(IMediator mediator)
        {
            _mediator = mediator;
        }
        public Task Handle(RegistrationCompleted notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"AVAILABILITY RegistrationCompletedHandler {notification.CompanyId}");
            foreach (var empId in notification.EmployeeId)
            {
                var @event = new CompanyAdded(notification.HoursRangeDtos, empId, notification.CompanyId);
                _mediator.Publish(@event);
            }
            return Task.CompletedTask;
        }
    }

    public class CompanyAdded : INotification
    {
        public List<HoursRangeDto> NotificationHoursRangeDtos { get; }
        public Guid EmployeeId { get; }
        public Guid NotificationCompanyId { get; }

        public CompanyAdded(List<HoursRangeDto> notificationHoursRangeDtos, Guid employeeId, Guid notificationCompanyId)
        {
            NotificationHoursRangeDtos = notificationHoursRangeDtos;
            EmployeeId = employeeId;
            NotificationCompanyId = notificationCompanyId;
        }
    }
}
