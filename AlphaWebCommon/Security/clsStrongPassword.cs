using System;

namespace DbCore.IMBUtils.Security
{
    public class clsStrongPassword
    {
        static char[] startingChars = new char[] { '<', '&' };
        //create constant strings for each type of characters
        static string alphaCaps = "QWERTYUIOPASDFGHJKLZXCVBNM";
        static string alphaLow = "qwertyuiopasdfghjklzxcvbnm";
        static string numerics = "1234567890";
        static string special = "~!@#$%^&*()_+|{}:\"<>?`-=\\[];',./";
        //create another string which is a concatenation of all above
        static string allChars = alphaCaps + alphaLow + numerics + special;
        static Random r = new Random();
        /// <summary>
        /// Gjeneron nje pasword i cili ka ne permbajtje te pakten nje shkronje te madhe, nje shkronje te vogel, nje numer dhe nje karakter non-alphanumeric
        /// </summary>
        /// <param name="length">gjatesia qe do kete passwordi qe do gjenerohet</param>
        /// <returns></returns>
        private static string GenerateStrongPassword(int length, int nrSpecialChars, int nrUppercaseLetters, int nrNumberChars)
        {
            String generatedPassword = "";
            if (length < 4)
                throw new Exception("Number of characters should be greater than 4.");
            // Generate four repeating random numbers are postions of
            // lower, upper, numeric and special characters
            // By filling these positions with corresponding characters,
            // we can ensure the password has atleast one
            // character of those types
            int pos;//, pLower, pUpper, pNumber, pSpecial;
            string[] arr = new string[length];
            string posArray = "0123456789";
            if (length < posArray.Length)
                posArray = posArray.Substring(0, length);

            for (int s = 0; s < nrSpecialChars; s++)
            {
                pos = getRandomPosition(ref posArray);
                arr[pos] = getRandomChar(special);
            }
            for (int u = 0; u < nrUppercaseLetters; u++)
            {
                pos = getRandomPosition(ref posArray);
                arr[pos] = getRandomChar(alphaCaps);
            }
            for (int n = 0; n < nrNumberChars; n++)
            {
                pos = getRandomPosition(ref posArray);
                arr[pos] = getRandomChar(numerics);
            }
            for (int a = 0; a < arr.Length; a++)
            {
                //pjesa e mbetur domerret random nga karakteret lowercase sepse karaketeret speciale, numerike, dhe uppercase duhet te jene ekzaktesisht aq si jane caktuar ne politikat e fjalekalimit
                if (String.IsNullOrEmpty(arr[a]))
                    arr[a] = getRandomChar(alphaLow);
                generatedPassword += arr[a];

            }
            return generatedPassword;
        }
        public static string GjeneroPasswordTeVlefshem(int length, int nrSpecialChars, int nrUppercaseLetters, int nrNumberChars)
        {
            var u_gjet = false;
            string passGjeneruar = "";
            var nrTentativash = 0;
            //te mos provohet me shume se 100 here
            while (!u_gjet && nrTentativash < 100)
            {
                passGjeneruar = GenerateStrongPassword(length, nrSpecialChars, nrUppercaseLetters, nrNumberChars);
                int posProblematik = 0;
                u_gjet = !IsDangerousString(passGjeneruar, out posProblematik);
                nrTentativash++;
            }
            return passGjeneruar;
        }
        public static bool IsDangerousString(string s, out int matchIndex)
        {
            matchIndex = 0;
            int startIndex = 0;
            while (true)
            {
                int num2 = s.IndexOfAny(startingChars, startIndex);
                if (num2 < 0)
                {
                    return false;
                }
                if (num2 == (s.Length - 1))
                {
                    return false;
                }
                matchIndex = num2;
                char ch = s[num2];
                if (ch != '&')
                {
                    if ((ch == '<') && ((IsAtoZ(s[num2 + 1]) || (s[num2 + 1] == '!')) || ((s[num2 + 1] == '/') || (s[num2 + 1] == '?'))))
                    {
                        return true;
                    }
                }
                else if (s[num2 + 1] == '#')
                {
                    return true;
                }
                startIndex = num2 + 1;
            }
        }

        private static bool IsAtoZ(char c)
        {
            return (((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')));
        }

        private static string getRandomChar(string fullString)
        {
            return fullString.ToCharArray()[(int)Math.Floor(r.NextDouble() * fullString.Length)].ToString();
        }
        private static int getRandomPosition(ref string posArray)
        {
            int pos;
            string randomChar = posArray.ToCharArray()[(int)Math.Floor(r.NextDouble() * posArray.Length)].ToString();
            pos = int.Parse(randomChar);
            posArray = posArray.Replace(randomChar, "");
            return pos;
        }
    }
}
