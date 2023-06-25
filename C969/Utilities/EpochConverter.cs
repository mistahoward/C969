using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C969.Utilities
{
    public class EpochConverter
    {
        /// <summary>
        /// Returns the epoch time
        /// </summary>
        /// <returns>The epoch time</returns>
        public static DateTime GetEpoch()
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch;
        }
        /// <summary>
        /// Returns the week number of the specified date in the epoch time
        /// </summary>
        /// <param name="requestedDate">The date for which to calculate the week number</param>
        /// <returns>The week number of the specified date in the epoch time</returns>
        public static int GetEpochWeekNumber(DateTime requestedDate)
        {
            var epoch = GetEpoch();
            var ticksSinceEpoch = requestedDate.Ticks - epoch.Ticks;
            var secondsSinceEpoch = ticksSinceEpoch / TimeSpan.TicksPerSecond;
            var daysSinceEpoch = secondsSinceEpoch / 86400;

            var currentWeekNumber = daysSinceEpoch / 7;
            return (int)currentWeekNumber;
        }
    }
}
