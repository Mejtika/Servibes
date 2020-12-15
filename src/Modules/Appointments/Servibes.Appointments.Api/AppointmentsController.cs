using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Servibes.Appointments.Api
{
    [ApiController]
    [Route("api/companies")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
