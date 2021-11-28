using System;

namespace Demo.EntitiesDto.Resources.Helpers
{
    public static class DateTimeExtensions
    {
        public static DateTime? ToDateTime(string dateTime)
        {
            return DateTime.TryParse(dateTime, out DateTime newDate) ? newDate : (DateTime?)null;
        }

        public static int GetDifferenceDatesInDays(DateTime initialDate, DateTime endDate)
        {
            return endDate.Subtract(initialDate).Days;
        }

        public static string ToStringFormatDate(string dateTime)
        {
            return DateTime.TryParse(dateTime, out DateTime newDate) ? newDate.ToString("yyyy/MM/dd") : string.Empty;
        }

        public static string ToStringFormatDate(DateTime dateTime)
        {
            return dateTime.ToString("yyyy/MM/dd");
        }

    }
}
