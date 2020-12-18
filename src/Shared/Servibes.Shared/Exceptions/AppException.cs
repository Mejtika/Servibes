using System;
using System.Collections.Generic;

namespace Servibes.Shared.Exceptions
{
    public class AppException : Exception
    {
        public List<string> Errors { get; }

        public AppException(List<string> errors)
        {
            this.Errors = errors;
        }

        public AppException(string error)
            : this(new List<string> { error })
        {
            
        }
    }
}