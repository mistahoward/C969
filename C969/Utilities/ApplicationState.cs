using C969.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Utilities
{
    public static class ApplicationState
    {
        private static User _currentUser;
        public static User CurrentUser
        {
            get { return _currentUser; }
            set
            {
                _currentUser = value;
                // When the user changes, update the time zone as well
                UserTimeZone = TimeZoneInfo.Local;
            }
        }
        public static TimeZoneInfo UserTimeZone { get; private set; }
    }
}
