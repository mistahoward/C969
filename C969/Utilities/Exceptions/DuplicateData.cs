using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Exceptions
{
    public class DuplicateData : Exception
    {
        public DuplicateData() { }

        public DuplicateData(string message) : base(message) { }

        public DuplicateData(string message, Exception innerException) : base(
            message, innerException)
        {
        }
    }
}
