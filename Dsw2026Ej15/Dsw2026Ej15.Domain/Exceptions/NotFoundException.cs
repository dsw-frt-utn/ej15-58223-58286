using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Exceptions
{
    public class NotFoundException: ValidationException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
