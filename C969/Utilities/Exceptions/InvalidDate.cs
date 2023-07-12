using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Exceptions
{
    public class InvalidDate : Exception
    {
        public InvalidDate() { }

        public InvalidDate(string message) : base(message) { }

        public InvalidDate(string message, Exception innerException) : base(
            message, innerException)
        {
        }
    }
}
