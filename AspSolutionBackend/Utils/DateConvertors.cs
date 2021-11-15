using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Utils
{
    // Temporary fix because docker has utc and estonia has utc+2, This needs a better solution
    public class DateConvertors
    {
        public static DateTime GetDateTimeEstoniaNow()
        {
            return DateTime.UtcNow.AddHours(2);
        }

        public static DateTime ConvertDateTimeToEstonian(DateTime dateTime)
        {
            return dateTime.ToUniversalTime().AddHours(2);
        }
    }
}