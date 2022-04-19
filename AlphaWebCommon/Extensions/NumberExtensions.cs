using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.IMBUtils.Extensions
{
    public static class NumberExtensions
    {
        /// <summary>
        /// kontrollon nese vlera eshte e barabarte me te pakten njeren nga vargu 
        /// </summary>
        /// <param name="number"></param>
        /// <param name="vlerat"></param>
        /// <returns></returns>
        public static bool EqualsAny(this int integer, params int[] vlerat)
        {
            return vlerat.Any(item => item.Equals(integer));
        }
    }
}
