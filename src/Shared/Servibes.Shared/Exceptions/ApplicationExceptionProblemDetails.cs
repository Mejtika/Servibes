using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Servibes.Shared.Exceptions
{
    public class ApplicationExceptionProblemDetails : ProblemDetails
    {
        public List<string> Errors { get; }

        public ApplicationExceptionProblemDetails(AppException exception)
        {
            this.Title = "Validation error";
            this.Status = StatusCodes.Status400BadRequest;
            this.Detail = "One or more errors occurred";
            this.Errors = exception.Errors;
            this.Type = "https://servibes/validation-error";
        }
    }
}
