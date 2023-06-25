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
            var startOfYear = new DateTime(requestedDate.Year, 1, 1);

            // Adjust startOfYear to the start of first week (Monday)
            int diff = startOfYear.DayOfWeek - DayOfWeek.Monday;
            if (diff < 0) diff += 7;
            startOfYear = startOfYear.AddDays(7 - diff);

            if (requestedDate < startOfYear)
            {
                // If requested date is before the start of the first week, consider it as belonging to the last week of the previous year
                startOfYear = startOfYear.AddDays(-7);
            }

            var ticksSinceStartOfYear = requestedDate.Ticks - startOfYear.Ticks;
            var secondsSinceStartOfYear = ticksSinceStartOfYear / TimeSpan.TicksPerSecond;
            var daysSinceStartOfYear = secondsSinceStartOfYear / 86400;

            var currentWeekNumber = (int)(daysSinceStartOfYear / 7) + 1;

            return currentWeekNumber;
        }
        /// <summary>
        /// Returns the start date of the specified week number in the current year
        /// </summary>
        /// <param name="weekNumber">The week number for which to calculate the start date</param>
        /// <returns>The start date of the specified week number</returns>
        public static DateTime GetStartOfWeek(int weekNumber)
        {
            var currentYear = DateTime.Now.Year;
            var startOfYear = new DateTime(currentYear, 1, 1);
            while (startOfYear.DayOfWeek != DayOfWeek.Monday)
            {
                startOfYear = startOfYear.AddDays(1);
            }
            var startOfWeek = startOfYear.AddDays((weekNumber - 1) * 7);
            return startOfWeek;
        }
    }
}
