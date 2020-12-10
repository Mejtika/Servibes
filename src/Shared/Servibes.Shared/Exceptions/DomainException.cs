using System;
using System.Collections.Generic;
using System.Text;

namespace Servibes.Shared.Exceptions
{
    public class DomainException : Exception
    {
        protected DomainException(string message) : base(message)
        {
        }
    }
}
