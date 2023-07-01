using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Utilities.Exceptions
{
    public class OutsideOfBusinessHours : Exception
    {
        public OutsideOfBusinessHours() { }

        public OutsideOfBusinessHours(string message) : base(message) { }

        public OutsideOfBusinessHours(string message, Exception innerException) : base(
            message, innerException)
        {
        }
    }
}
