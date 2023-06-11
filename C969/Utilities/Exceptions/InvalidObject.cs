using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Exceptions
{
    public class InvalidObject : Exception
    {
        public InvalidObject() { }

        public InvalidObject(string message) : base(message) { }

        public InvalidObject(string message, Exception innerException) : base(
            message, innerException)
        {
        }
    }
}
