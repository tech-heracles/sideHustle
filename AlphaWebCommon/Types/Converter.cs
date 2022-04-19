using System;
using System.Globalization;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;

namespace DbCore.IMBUtils.Types
{
    public class Converter
    {
        public static bool Parse(string value, out int parsedValue, string fusha)
        {
            if (int.TryParse(value, out parsedValue))
                return true;
            if (value == "")
            {
                parsedValue = 0;
                ImbLogger.Trace($"Vlera e fushes {fusha} eshte bosh");
                return true;
            }
            throw new MyException("GABIM PARSIMI int: fusha =>{0}, vlera =>{1}", fusha, value);
        }

        public static bool ParseExact(string value, out int parsedValue, string fusha)
        {
            if (int.TryParse(value, out parsedValue))
                return true;
            throw new MyException("GABIM PARSIMI int: fusha =>{0}, vlera =>{1}", fusha, value);
        }

        /// <summary>
        /// konverton ne bool cdo vlere qe vjen nga DB 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parsedValue"></param>
        /// <param name="fusha"></param>
        /// <returns></returns>
        public static bool Parse(string value, out bool parsedValue, string fusha)
        {


            if (bool.TryParse(value, out parsedValue)) return true;

            if (string.IsNullOrEmpty(value))
            {
                parsedValue = false;

                ImbLogger.Trace($"fusha {fusha} eshte bosh, po e konvertoj ne 'false'!  vlera : {value}");
                return true;
            }

            //per te marr parasysh rastet nese eshte harruar te dekriptohet
            if (value != "0" && value != "1")
                throw new MyException("GABIM PARSIMI  bool:  fusha =>{0} vlera =>{1}", fusha, value);
            ///kjo behet per te marr parasysh rastin kur eshte harruar ndonje vlere te behet convert ne bit
            ///kur dekriptohet
            switch (value)
            {
                case "0":
                    parsedValue = false;
                    break;
                case "1":
                    parsedValue = true;
                    break;
            }
            ImbLogger.Trace($"fushat {fusha} ka kete vlere '{value}',po e konvertoj ne  {parsedValue}!");
            return true;
        }

        public static bool ParseExact(string value, out bool parsedValue, string fusha)
        {
            if (bool.TryParse(value, out parsedValue)) return true;
            throw new MyException("GABIM PARSIMI  bool:  fusha =>{0} vlera =>{1}", fusha, value);
        }

        /// <summary>
        /// converton ne datetime,nese vlera eshte bosh i jep vleren default
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parsedValue"></param>
        /// <param name="fusha"></param>
        /// <returns></returns>
        public static bool Parse(string value, out DateTime parsedValue, string fusha)
        {
            //TODO te merren parasysh te gjithe tipet e mundshme
            if (DateTime.TryParse(value, out parsedValue)) return true;
            if (value != "") throw new MyException("GABIM PARSIMI  DateTime:  fusha =>{0} vlera =>{1}", fusha, value);
            parsedValue = default(DateTime);

            ImbLogger.Trace($"Vlera e fushes {fusha} erdhi bosh");
            return true;
        }

        public static bool ParseExact(string value, out DateTime parsedValue, string fusha)
        {
            if (DateTime.TryParse(value, out parsedValue)) return true;
            throw new MyException("GABIM PARSIMI  DateTime:  fusha =>{0} vlera =>{1}", fusha, value);
        }

        public static bool Parse(string value, out double parsedValue, string fusha)
        {
            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out parsedValue)) return true;
            if (value != "") throw new MyException("GABIM PARSIMI double: fusha =>{0} vlera =>{1} ", fusha, value);
            parsedValue = 0;
            ImbLogger.Trace($"Vlera e fushes {fusha} erdhi bosh");
            return true;
        }

        public static bool ParseExact(string value, out double parsedValue, string fusha)
        {
            if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out parsedValue)) return true;
            throw new MyException("GABIM PARSIMI double: fusha =>{0} vlera =>{1} ", fusha, value);
        }

        /// <summary>
        /// Converton ne decimal,nese vlera eshte boshe le vleren default
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parsedValue"></param>
        /// <param name="fusha"></param>
        /// <returns></returns>
        public static bool Parse(string value, out decimal parsedValue, string fusha)
        {

            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out parsedValue)) return true;
            if (value != "") throw new MyException($"GABIM PARSIMI decimal: fusha =>{fusha} vlera =>{value} ");
            parsedValue = 0;
            return true;
        }

        /// <summary>
        /// perdoret ne rastet kur vlera qe vjen duhe te jete patjeter decimal
        /// </summary>
        /// <param name="value"></param>
        /// <param name="parsedValue"></param>
        /// <param name="fusha"></param>
        /// <returns></returns>
        public static bool ParseExact(string value, out decimal parsedValue, string fusha)
        {
            if (decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture.NumberFormat, out parsedValue)) return true;

            throw new MyException("GABIM PARSIMI decimal: fusha =>{0} vlera =>{1},Vlera duhet te jete patjeter decimal ", fusha, value);
        }

        public static int ConvertToInt(object x)
        {
            var str = x?.ToString();
            if (string.IsNullOrWhiteSpace(str)) return 0;
            int num;
            int.TryParse(str, out num);
            return num;
        }

        public static string ToStringOrEmpty(object value)
        {
            return value?.ToString() ?? string.Empty;
        }

        /// <summary>
        /// ben parsim te njepasnjeshem te stringjeve qe kalohen si parameter dhe ndalon tek e para
        /// qe parsohet me sukses.ne rast ne kundert kthen 0
        /// </summary>
        /// <param name="num"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool GetFirstSuccessConvert(out int num, params string[] values)
        {
            foreach (var value in values)
            {
                if (int.TryParse(value, out num))
                    return true;
            }
            num = 0;
            return false;
        }

        public static T MerrVlereOseDefault<T>(object vleraDefaultStr)
        {

            if (string.IsNullOrWhiteSpace(vleraDefaultStr?.ToString()) || vleraDefaultStr.ToString().ContainsAnyIgnoreCase("null", "undefined", "NaN"))
                return default(T);
            return (T)Convert.ChangeType(vleraDefaultStr, typeof(T));
        }
    }
}