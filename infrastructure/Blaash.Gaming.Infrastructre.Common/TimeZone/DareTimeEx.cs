using System;
namespace Blaash.Gaming.Infrastructre.Common.TimeZone
{
    public static class DateTimeEx
    {

        public static bool IsGreaterThan(this int i, int value)
        {
            return i > value;
        }

        public static DateTime ConverTlocalToUTC(this DateTime localdatetime)
        {
            TimeZoneInfo localTimeZone = TimeZoneInfo.Local;
            var UTCTime = TimeZoneInfo.ConvertTimeToUtc(localdatetime, localTimeZone);

            return UTCTime;
        }


        public static DateTime ConverUTCToTenantTimeZone(this DateTime localdatetime, TimeZoneInfo timeZoneInfo)
        {
  
            var tenantDateTime = TimeZoneInfo.ConvertTimeFromUtc(localdatetime, timeZoneInfo);

            return tenantDateTime;
        }


        public static DateTime ConverTenantLocalTimeToUTC(this DateTime localdatetime, TimeZoneInfo timeZoneInfo)
        { 
            var utcdateTime = TimeZoneInfo.ConvertTimeToUtc(localdatetime, timeZoneInfo);

            return utcdateTime;
        }
    }

    
}
