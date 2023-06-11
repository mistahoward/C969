using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Exceptions
{
    public class ChangeNotPermitted : Exception
    {
        public ChangeNotPermitted() { }

        public ChangeNotPermitted(string message) : base(message) { }

        public ChangeNotPermitted(string message, Exception innerException) : base(
            message, innerException)
        {
        }
    }
}
