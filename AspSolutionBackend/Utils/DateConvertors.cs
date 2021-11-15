using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace Utils
{
    public class DateConvertors
    {
        public static DateTime GetDateTimeEstoniaNow()
        {
            return DateTime.UtcNow.AddHours(2);
        }
    }
}