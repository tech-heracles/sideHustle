using System;
using System.Linq;

namespace AlphaWeb.Core.Extensions
{
    public static class Strings
    {
        public static bool EqualsIgnoreCase(this string str1, params string[] strings)
        {
            return strings != null &&
                   strings.All(str => string.Compare(str1, str, StringComparison.InvariantCultureIgnoreCase) == 0);
        }
        public static bool EqualsAnyIgnoreCase(this string str1, params string[] strings)
        {
            return strings != null &&
                   strings.Any(str => string.Compare(str1, str, StringComparison.InvariantCultureIgnoreCase) == 0);
        }
        public static bool IsNullOrEmpty(this string @string)
        {
            return string.IsNullOrWhiteSpace(@string);
        }
    }
}
