using System;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace AlphaWebReports
{
    public class NumberConverter
    {
        private static ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
        private static string[] onesMapping =
       new string[] {
            "Zero", "Nje", "Dy", "Tre", "Kater", "Pese", "Gjashte", "Shtate", "Tete", "Nente",
            "Dhjete", "Njembedhjete", "Dymbedhjete", "Trembedhjete", "Katermbedhjete", "Pesembedhjete",
            "Gjashtembedhjete", "Shtatembedhjete", "Tetembedhjete", "Nentembedhjete"
      };
        private static string[] tensMapping =
            new string[] {
            "Njezet", "Tridhjete", "Dyzet", "Pesedhjete", "Gjashtedhjete", "Shtatedhjete", "Tetedhjete", "Nentedhjete"
        };
        private static string[] groupMapping =
            new string[] {
            "Qind", "Mije", "Milion", "Miliard", "Trilion"
        };
        private static string[] onesMapping_Eng =
     new string[] {
            "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine",
            "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fiveteen",
            "Sixteen", "Seventeen", "Eighteen", "Nineteen"
      };

        private static string[] tensMapping_Eng =
            new string[] {
            "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
        };

        private static string[] groupMapping_Eng =
            new string[] {
            "Hundred", "Thousand", "Million", "Milliard", "Trillion"
        };

        public static String changeToWords(String numb, CultureInfo ci)
        {
            String val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            String endStr = ("");
            if (numb.Trim() == "")
                return "";
            try
            {
                int decimalPlace = numb.IndexOf(".");
                int idxPresjes = numb.IndexOf(",");
                if (decimalPlace > 0 && idxPresjes <= 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = rm.GetString("lblDhe", ci); 
                        pointStr = (" ") + EnglishFromNumber(int.Parse(points),ci) + " " + rm.GetString("lblQindarka", ci);
                    }
                }
                else if (idxPresjes > 0 && decimalPlace <= 0)
                {
                    string[] pjeset = numb.Split(',');
                    string numri = "";
                    for (int i = 0; i < pjeset.Length; i++)
                        numri += pjeset[i];
                    wholeNo = numri;
                }
                else if (idxPresjes > 0 && decimalPlace > 0)
                {
                    string[] pjeset = numb.Split(',');
                    string numri = "";
                    for (int i = 0; i < pjeset.Length; i++)
                        numri += pjeset[i];
                    numb = numri;
                    decimalPlace = numb.IndexOf(".");
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = rm.GetString("lblDhe", ci);
                        pointStr = (" ") + EnglishFromNumber(int.Parse(points), ci) + " " + rm.GetString("lblQindarka", ci);
                    }
                }
                val = String.Format("{0} {1}{2} {3}", EnglishFromNumber(int.Parse(wholeNo), ci).Trim(), andStr, pointStr, endStr);
            }
            catch { ;}

            string fundi = val.Substring(val.Length - 4, 3);
             if (fundi == " " + rm.GetString("lblE", ci) + " ")
                val = val.Substring(0, val.Length - 4);

            return val;
        }
        public static string EnglishFromNumber(long number, CultureInfo ci)
        {
            string retVal = null;
            int group = 0;

            if (number == 0)
            {
                if (ci.Name == "sq-AL")
                    return onesMapping[number];
                else
                    return onesMapping_Eng[number];
            }
            while (number > 0)
            {
                int numberToProcess = (int)(number % 1000);
                number = number / 1000;

                string groupDescription = ProcessGroupSipasGjuhes(numberToProcess,ci);
                if (groupDescription != null)
                {
                    if (group > 0)
                    {
                        if (ci.Name == "sq-AL")
                        
                            retVal = groupMapping[group] + " E " + retVal;
                       
                        else retVal = groupMapping_Eng[group] + " " + retVal;
                    }
                    retVal = groupDescription + " " + retVal;
                }

                group++;
            }

            return /*sign + */" " + retVal;
        }
        private static string ProcessGroupSipasGjuhes(int number, CultureInfo ci)
        {
            if(ci.Name == "sq-AL")
            {
               return ProcessGroup(number);
            }
            else return ProcessGroup_Eng(number);
        }


        private static string ProcessGroup(int number)
        {
           
            int tens = number % 100;
            int hundreds = number / 100;

            string retVal = null;
            if (hundreds > 0)
            {
                retVal = onesMapping[hundreds] + " " + groupMapping[0];
            }
            if (tens > 0)
            {
                if (tens < 20)
                {
                    retVal += ((retVal != null) ? " E " : "") + onesMapping[tens];
                }
                else
                {
                    int ones = tens % 10;
                    tens = (tens / 10) - 2; // 20's offset

                    retVal += ((retVal != null) ? "  E " : "") + tensMapping[tens];

                    if (ones > 0)
                    {
                        retVal += ((retVal != null) ? " E " : "") + onesMapping[ones];
                    }
                }
            }

            return retVal;
        }
        private static string ProcessGroup_Eng(int number)
        {

            int tens = number % 100;
            int hundreds = number / 100;

            string retVal = null;
            if (hundreds > 0)
            {
                retVal = onesMapping_Eng[hundreds] + " " + groupMapping_Eng[0];
            }
            if (tens > 0)
            {
                if (tens < 20)
                {
                    retVal += ((retVal != null) ? " " : "") + onesMapping_Eng[tens];
                }
                else
                {
                    int ones = tens % 10;
                    tens = (tens / 10) - 2; // 20's offset

                    retVal += ((retVal != null) ? " " : "") + tensMapping_Eng[tens];

                    if (ones > 0)
                    {
                        retVal += ((retVal != null) ? " " : "") + onesMapping_Eng[ones];
                    }
                }
            }

            return retVal;
        }

    }
}
