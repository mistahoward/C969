using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Utilities.Exceptions
{
    public class OverlappingAppointments : Exception
    {
        public OverlappingAppointments() { }

        public OverlappingAppointments(string message) : base(message) { }

        public OverlappingAppointments(string message, Exception innerException) : base(
            message, innerException)
        {
        }
    }
}
