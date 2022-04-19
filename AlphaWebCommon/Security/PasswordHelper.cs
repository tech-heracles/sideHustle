using System;
using System.Security.Cryptography;
using System.Text;

namespace DbCore.IMBUtils.Security
{
   public class PasswordHelper
    {
        public static bool ValidoPassword(string username, string password, string perdoruesPassword)
        {
            return (perdoruesPassword.Equals(HashLogin(username, password)) || perdoruesPassword.Equals(HashLogin(char.ToUpper(username[0]) + username.Substring(1), password)) || perdoruesPassword.Equals(HashLogin(char.ToLower(username[0]) + username.Substring(1), password)));
        }


        /// <summary>
        /// perdoret per te kthyer hashin e te dhenave te logimit
        /// </summary>
        /// <param name="userName">username</param>
        /// <param name="password">passwordi</param>
        /// <returns> te dhenat e logimit te hashuara</returns>
        public static string HashLogin(string userName, string password)
        {
            // Perdoret shkronja e pare e emrit per funksionin hash
            return HashData(String.Format("{0}{1}", userName.Substring(0, 1), password));
        }

        public static string HashData(string data)
        {
            SHA256 hasher = SHA256Managed.Create();
            byte[] hashedData = hasher.ComputeHash(Encoding.Unicode.GetBytes(data));

            // Now we'll make it into a hexadecimal string for saving
            StringBuilder sb = new StringBuilder(hashedData.Length * 2);
            foreach (byte b in hashedData)
            {
                sb.AppendFormat("{0:x2}", b);
            }
            return sb.ToString();
        }
        /// <summary>
        /// perdoret per ta hashuar te dhenat
        /// </summary>
        /// <param name="data">te dhenat qe do hashohen</param>
        /// <returns> te dhenat e hashuara</returns>


        public static byte[] HashValidData(string data)
        {
            SHA1 hasher = SHA1.Create();
            return hasher.ComputeHash(Encoding.ASCII.GetBytes(data));//Encoding.Unicode.GetBytes(data));
        }

        public static string GjeneroPassword(int gjatesiPw, int nrSpecialChars, int nrUppercaseLetters, int nrNumberChars)
        {
            return clsStrongPassword.GjeneroPasswordTeVlefshem(gjatesiPw, nrSpecialChars, nrUppercaseLetters, nrNumberChars);
        }
        /// <summary>
        /// perdoret per te gjeneruar nje pasword cfaredo
        /// </summary>
        /// <param name="gjatesiPw"> gjatesia e paswordit</param>
        /// <returns>kthen paswordin e gjeneruar</returns>
        public static string GjeneroPassword(int gjatesiPw)
        {

            return GjeneroPassword(gjatesiPw, 1, 1, 1);
        }


    }
}
