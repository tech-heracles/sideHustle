using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;

namespace DbCore.IMBUtils.Security
{
   public class PasswordHelper
    {
        // Formati i ri: "pbkdf2-sha256$<iterime>$<salt base64>$<hash base64>". Hash-et e vjetra (HashLogin) mbeten te vlefshme
        // dhe zevendesohen sa here qe perdoruesi vendos nje fjalekalim te ri.
        private const string PrefiksiPbkdf2 = "pbkdf2-sha256$";
        private const int IterimePbkdf2 = 210000;

        /// <summary>
        /// Hash-i qe ruhet per nje fjalekalim te ri: PBKDF2 me salt te rastesishem per cdo fjalekalim.
        /// </summary>
        public static string KrijoHash(string password)
        {
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(salt);
            return PrefiksiPbkdf2 + IterimePbkdf2 + "$" + Convert.ToBase64String(salt) + "$" + Convert.ToBase64String(Pbkdf2Sha256(password, salt, IterimePbkdf2, 32));
        }

        public static bool EshteHashIRi(string hashIRuajtur) =>
            hashIRuajtur != null && hashIRuajtur.StartsWith(PrefiksiPbkdf2, StringComparison.Ordinal);

        /// <summary>
        /// Kontrollon fjalekalimin kundrejt hash-it te ruajtur, ne formatin e ri ose ne ate te vjetrin (HashLogin).
        /// </summary>
        public static bool Verifiko(string username, string password, string hashIRuajtur)
        {
            if (string.IsNullOrEmpty(hashIRuajtur) || password == null)
                return false;
            if (EshteHashIRi(hashIRuajtur))
                return VerifikoPbkdf2(password, hashIRuajtur);
            return !string.IsNullOrEmpty(username) && BarazimKonstant(hashIRuajtur, HashLogin(username, password));
        }

        /// <summary>
        /// Si <see cref="Verifiko"/>, por per hash-et e vjetra pranon edhe shkronjen e pare te username me germe te madhe/vogel.
        /// </summary>
        public static bool ValidoPassword(string username, string password, string perdoruesPassword)
        {
            if (EshteHashIRi(perdoruesPassword))
                return password != null && VerifikoPbkdf2(password, perdoruesPassword);
            return (perdoruesPassword.Equals(HashLogin(username, password)) || perdoruesPassword.Equals(HashLogin(char.ToUpper(username[0]) + username.Substring(1), password)) || perdoruesPassword.Equals(HashLogin(char.ToLower(username[0]) + username.Substring(1), password)));
        }

        private static bool VerifikoPbkdf2(string password, string hashIRuajtur)
        {
            string[] pjeset = hashIRuajtur.Split('$');
            if (pjeset.Length != 4 || !int.TryParse(pjeset[1], out int iterime) || iterime <= 0)
                return false;
            byte[] salt, hash;
            try
            {
                salt = Convert.FromBase64String(pjeset[2]);
                hash = Convert.FromBase64String(pjeset[3]);
            }
            catch (FormatException)
            {
                return false;
            }
            if (hash.Length == 0)
                return false;
            return BarazimKonstant(Pbkdf2Sha256(password, salt, iterime, hash.Length), hash);
        }

        // Rfc2898DeriveBytes ne .NET Framework eshte vetem SHA1 dhe i ngadalte (~0.7s per 100k iterime);
        // PBKDF2 i Windows (CNG) ben te njejten pune ne kod nativ.
        private static byte[] Pbkdf2Sha256(string password, byte[] salt, int iterime, int gjatesia)
        {
            byte[] pw = Encoding.UTF8.GetBytes(password);
            byte[] rezultati = new byte[gjatesia];
            IntPtr alg;
            int status = BCryptOpenAlgorithmProvider(out alg, "SHA256", null, BCRYPT_ALG_HANDLE_HMAC_FLAG);
            if (status != 0)
                throw new CryptographicException(status);
            try
            {
                status = BCryptDeriveKeyPBKDF2(alg, pw, pw.Length, salt, salt.Length, (ulong)iterime, rezultati, rezultati.Length, 0);
                if (status != 0)
                    throw new CryptographicException(status);
                return rezultati;
            }
            finally
            {
                BCryptCloseAlgorithmProvider(alg, 0);
                Array.Clear(pw, 0, pw.Length);
            }
        }

        private const int BCRYPT_ALG_HANDLE_HMAC_FLAG = 0x00000008;

        [DllImport("bcrypt.dll", CharSet = CharSet.Unicode)]
        private static extern int BCryptOpenAlgorithmProvider(out IntPtr phAlgorithm, string pszAlgId, string pszImplementation, int dwFlags);

        [DllImport("bcrypt.dll")]
        private static extern int BCryptCloseAlgorithmProvider(IntPtr hAlgorithm, int dwFlags);

        [DllImport("bcrypt.dll")]
        private static extern int BCryptDeriveKeyPBKDF2(IntPtr hPrf, byte[] pbPassword, int cbPassword, byte[] pbSalt, int cbSalt,
            ulong cIterations, byte[] pbDerivedKey, int cbDerivedKey, int dwFlags);

        // krahasim ne kohe konstante, qe koha e pergjigjes te mos tregoje sa karaktere perputhen
        private static bool BarazimKonstant(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;
            int diferenca = 0;
            for (int i = 0; i < a.Length; i++)
                diferenca |= a[i] ^ b[i];
            return diferenca == 0;
        }

        private static bool BarazimKonstant(string a, string b) =>
            BarazimKonstant(Encoding.UTF8.GetBytes(a), Encoding.UTF8.GetBytes(b));

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
        public static string GjenroApiKey(string email)
        {
            string salt = "##A!phaWEB.2023_##";
            byte[] emailBase64 = Encoding.ASCII.GetBytes(HashData(email + salt));
            return Convert.ToBase64String(emailBase64);

        }


    }
}
