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
        /// <summary>
        /// Converts a UTC date time to user's local time using the user's local time zone from the ApplicationState
        /// </summary>
        /// <param name="utcDateTime">The UTC DateTime to convert</param>
        /// <returns>The local DateTime in the user's local time zone</returns>
        public static DateTime ConvertUtcToUserTime(DateTime utcDateTime)
        {
            TimeZoneInfo localTimeZone = ApplicationState.UserTimeZone;

            // Set the Kind property to Utc
            utcDateTime = DateTime.SpecifyKind(utcDateTime, DateTimeKind.Utc);

            DateTime localDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, localTimeZone);

            return localDateTime;
        }
        /// <summary>
        /// Converts a UTC date time to user's local time using the user's local time zone from the ApplicationState
        /// </summary>
        /// <param name="utcDateTime">The UTC DateTime to convert</param>
        /// <returns>The local DateTime in the user's local time zone with the time zone name</returns>
        public static string ConvertUtcToUserTimeWithTimeZone(DateTime utcDateTime)
        {
            TimeZoneInfo localTimeZone = ApplicationState.UserTimeZone;

            DateTime userDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, localTimeZone);

            // Get the appropriate time zone name
            string timeZoneName;
            if (localTimeZone.IsDaylightSavingTime(userDateTime))
            {
                timeZoneName = localTimeZone.DaylightName;
            }
            else
            {
                timeZoneName = localTimeZone.StandardName;
            }

            // Return a string with the date, time, and time zone name
            return $"{userDateTime:G} {timeZoneName}";
        }
        /// <summary>
        /// Determines if a given date falls within the current week
        /// </summary>
        /// <param name="date">The date to check</param>
        /// <returns>True if the date falls within the current week, otherwise false</returns>
        public static bool IsInCurrentWeek(DateTime date)
        {
            DateTime now = DateTime.Now;
            int delta = DayOfWeek.Monday - now.DayOfWeek;
            DateTime monday = now.AddDays(delta);
            return date >= monday && date < monday.AddDays(7);
        }
        /// <summary>
        /// Determines if a given date falls within the current month
        /// </summary>
        /// <param name="date">The date to check</param>
        /// <returns>True if the date falls within the current month, otherwise false</returns>
        public static bool IsInCurrentMonth(DateTime date)
        {
            return date.Month == DateTime.Now.Month && date.Year == DateTime.Now.Year;
        }
    }
}
