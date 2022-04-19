using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DbCore.IMBUtils.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// kontrollon nese vlera permban  te pakten njeren nga vargu 
        /// </summary>
        /// <param name="stringu"></param>
        /// <param name="vlerat"></param>
        /// <returns></returns>
        public static bool ContainsAnyIgnoreCase(this string stringu, params string[] vlera)
        {
            return !string.IsNullOrEmpty(stringu) && vlera.Any(item => stringu.IndexOf(item, StringComparison.InvariantCultureIgnoreCase) > -1);
        }

        /// <summary>
        /// kontrollon nese vlera eshte e barabarte me te pakten njeren nga vargu 
        /// </summary>
        /// <param name="stringu"></param>
        /// <param name="vlerat"></param>
        /// <returns></returns>
        public static bool EqualsAnyIgnoreCase(this string stringu, params string[] vlerat)
        {
            return stringu!=null && vlerat.Any(item => item.Equals(stringu, StringComparison.InvariantCultureIgnoreCase));
        }

        public static bool EqualsIgnoreCase(this string stringu, string vlera)
        {
            return string.Equals(stringu, vlera, StringComparison.InvariantCultureIgnoreCase);
        }

        public static string Replicate(this string source, int multiplier)
        {
            StringBuilder sb = new StringBuilder(multiplier * source.Length);
            for (int i = 0; i < multiplier; i++)
            {
                sb.Append(source);
            }

            return sb.ToString();
        }

        public static string Encode(this string text)
        {
            return HttpContext.Current.Server.UrlPathEncode(text);
        }

        /// <summary>
        /// Metode qe ben heqjen e hapesirave, enter dhe non-breaking spaces ne fillim dhe ne fund te stringut input
        /// </summary>
        /// <param name="text">i kalohet si parameter stringu te cilit do i hiqen karakteret e panevojshme</param>
        /// <returns>Stringu i modifikuar pa hapesirat</returns>
        public static string RemoveSpaces(this string text)
        {
            return Regex.Replace(text.TrimStart().TrimEnd(), @"\u00A0|[\n\r]", "");
        }
    }
}