using System;

namespace Utils
{
    public class DateUtils
    {
        public static double CalculateHoursBetweenDates(DateTime start, DateTime end)
        {
            return (end - start).TotalHours;
        }

        public static double CalculateSecondsBetweenDates(DateTime start, DateTime end)
        {
            return (end - start).TotalSeconds;
        }
        public static double CalculateMillisecondsBetweenDates(DateTime start, DateTime end)
        {
            return Math.Abs((end - start).TotalMilliseconds);
        }

        public static double CalculateMinutesBetweenDates(DateTime start, DateTime end)
        {
            return (end - start).TotalMinutes;
        }
        public static double MillisecondsToMinutes(double ms)
        {
            return (ms / 1000)/60;
        }
    }
}