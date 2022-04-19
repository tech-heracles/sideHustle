using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbCore.IMBUtils.Types
{
   public static class DateTimeUtil
    {
       /// <summary>
       /// nese data eshte null ose eshte ajo default (01.01.0001 00:00:00)
       /// </summary>
       /// <param name="date"></param>
       /// <returns></returns>
       public static bool EshteNullOrDefault(DateTime date)
       {
           return date == null || date == default(DateTime);
       }
    }
}
