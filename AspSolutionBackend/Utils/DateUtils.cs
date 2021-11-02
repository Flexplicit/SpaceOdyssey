using System;

namespace Utils
{
    public class DateUtils
    {
        public static double CalculateHoursBetweenDates(DateTime start, DateTime end)
        {
            return (end - start).TotalHours;
        }
    }
}