using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Exceptions
{
    public class DataNotFound : Exception
    {
        public DataNotFound() { }

        public DataNotFound(string message) : base(message) { }

        public DataNotFound(string message, Exception innerException) : base(
            message, innerException)
        { 
        }
    }
}
