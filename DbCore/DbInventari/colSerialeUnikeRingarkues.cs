using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DbCore.DbInventari
{
    public class colSerialeUnikeRingarkues: List<clsSerialeUnikeRingarkues>
    {

        
        public static string ktheSerialinEPare(string serialiKryesor, int sasia)
        {
            if (sasia == 1)
                return serialiKryesor;

            string[] serialParts = serialiKryesor.Split('-');
            if (sasia == 10)
                return $"{serialParts[0]}-{serialParts[1]}-0001";

            return $"{serialParts[0]}-0001-0001";
        }

        public static string ktheSerialinEFundit(string serialiKryesor, int sasia)
        {
            if (sasia == 1)
                return serialiKryesor;

            string[] serialParts = serialiKryesor.Split('-');
            if (sasia == 10)
                return $"{serialParts[0]}-{serialParts[1]}-0010";

            return $"{serialParts[0]}-0010-0010";
        }
    }
}
