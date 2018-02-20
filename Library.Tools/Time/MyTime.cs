using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tools.Time
{
    public static class MyTime
    {
        /// <summary>
        /// Retourne l'heure actuelle de paris
        /// </summary>
        /// <returns></returns>
        public static DateTime GetParisDateTimeNow()
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time");
            DateTime now = DateTime.SpecifyKind(DateTime.UtcNow,DateTimeKind.Utc);
            return TimeZoneInfo.ConvertTimeFromUtc(now, tz);
        }

        /// <summary>
        /// Retourne la date mais dans la zone de paris
        /// </summary>
        /// <param name="iDateTime"></param>
        /// <returns></returns>
        public static DateTime GetParisDateTime( this DateTime iDateTime)
        {
            TimeZoneInfo tz = TimeZoneInfo.FindSystemTimeZoneById("Romance Standard Time");
            DateTime now = DateTime.SpecifyKind(iDateTime, DateTimeKind.Utc);
            return TimeZoneInfo.ConvertTimeFromUtc(now, tz);
        }
    }
}
