using System;

namespace DbCore.IMBUtils.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool IsBeforeStartOfCurrentMonth(this DateTime date)
        {
            var now = DateTime.Now;
            var startOfCurrentMonth = new DateTime(now.Year, now.Month, 1);
            return date < startOfCurrentMonth;
        }

        public static bool eshteBrendaperiudhes(this DateTime date, DateTime dtFillimi, DateTime dtMbarimi)
        {
            return date >= dtFillimi && date <= dtMbarimi;
        }

        /// <summary>
        /// kontrollon nese ora eshte e formatit 00:00, pra data mos te perfshije oren.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool eshteDatePaOre(this DateTime date)
        {
            return date.Hour == 0 && date.Millisecond == 0 && date.Minute == 0 && date.Second == 0;
        }
    }
}