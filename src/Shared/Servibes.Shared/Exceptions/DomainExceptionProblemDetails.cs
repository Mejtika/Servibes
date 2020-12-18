using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Servibes.Shared.Exceptions
{
    public class DomainExceptionProblemDetails : ProblemDetails
    {
        public DomainExceptionProblemDetails(DomainException exception)
        {
            this.Title = "Business rule error";
            this.Status = StatusCodes.Status409Conflict;
            this.Detail = exception.Message;
            this.Type = "https://servibes/business-rule-error";
        }
    }
}
