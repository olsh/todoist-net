using System;

namespace Todoist.Net.Extensions
{
    internal static class DateTimeExtensions
    {
        public static string ToQueryString(this DateTime dateTime)
        {
            return dateTime.ToString("s");
        }
    }
}
