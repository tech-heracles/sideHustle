using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace DbCore.IMBUtils.Security
{
    public class RijndaelSimple
    {
        private static string VodKeyString = "anjueolkdiwpoida";//16 char
        private static string VodIVString = "4528711254935489";//16 char
        private static string ImbKeyString = "ankdiwpjueoloida";//16 char
        private static string ImbIVString = "5244548871125493";//16 char


        private static byte[] Decode(string str)
        {
            var decbuff = Convert.FromBase64String(str);
            return decbuff;
        }
        /// <summary>
        /// Dekripton mesazhin me paramtrat e Vodafone(Vetem per vodafone)
        /// </summary>
        /// <param name="crypt">mesazhi qe do dekriptohet</param>
        /// <returns></returns>
        public static String DecryptVodafoneString(string crypt)
        {
            return DecryptString(crypt, VodKeyString, VodIVString, PaddingMode.Zeros);
        }
        /// <summary>
        /// Dekripton mesazhin
        /// </summary>
        /// <param name="crypt">mesazhi qe do dekriptohet</param>
        /// <param name="KeyString">Celsi sekret qe perdor algoritmi per dekriptimin e mesazhit</param>
        /// <param name="IVString">vektori i inicializimit</param>
        /// <returns></returns>
        public static String DecryptString(string crypt, string KeyString, string IVString, PaddingMode paddingMode)
        {

            //byte[] cypher = Decode(crypt);
            var sRet = "";

            var encoding = new UTF8Encoding();
            byte[] cypher = StringToByteArray(crypt);
            var Key = encoding.GetBytes(KeyString);
            var IV = encoding.GetBytes(IVString);

            using (var rj = new RijndaelManaged())
            {
                try
                {
                    rj.Padding = paddingMode;
                    rj.Mode = CipherMode.CBC;
                    rj.KeySize = 128;
                    rj.BlockSize = 128;
                    rj.Key = Key;
                    rj.IV = IV;
                    var ms = new MemoryStream(cypher);

                    using (var cs = new CryptoStream(ms, rj.CreateDecryptor(Key, IV), CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            sRet = sr.ReadLine();
                        }
                    }
                }
                finally
                {
                    rj.Clear();
                }
            }

            return sRet.Trim('\0');
        }
        /// <summary>
        /// Enkripton mesazhin ne baze te parametrave  
        /// </summary>
        /// <param name="message">mesazhi qe do enkriptohet</param>
        /// <param name="KeyString">Celsi sekret qe perdor algoritmi per enkriptimin e mesazhit</param>
        /// <param name="IVString">vektori i inicializimit</param>
        /// <returns></returns>
        public static string EncryptString(string message, string KeyString, string IVString, PaddingMode paddingMode)
        {

            byte[] Key = ASCIIEncoding.UTF8.GetBytes(KeyString);
            byte[] IV = ASCIIEncoding.UTF8.GetBytes(IVString);

            string encrypted = null;
            RijndaelManaged rj = new RijndaelManaged();
            rj.BlockSize = 128;
            rj.Key = Key;
            rj.IV = IV;
            rj.Mode = CipherMode.CBC;
            rj.Padding = paddingMode;
            try
            {
                MemoryStream ms = new MemoryStream();

                using (CryptoStream cs = new CryptoStream(ms, rj.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(message);
                        sw.Close();
                    }
                    cs.Close();
                }
                byte[] encoded = ms.ToArray();
                //encrypted = Convert.ToBase64String(encoded);
                encrypted = BitConverter.ToString(encoded).Replace("-", "");
                ms.Close();
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file error occurred: {0}", e.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: {0}", e.Message);
            }
            finally
            {
                rj.Clear();
            }

            return encrypted;
        }



        /// <summary>
        /// Enkripton mesazhin duke perdorur celsin e Vodafone(Vetem per vodafone)
        /// </summary>
        /// <param name="message">mesazhi qe do enkriptohet</param>
        /// <returns></returns>
        public static string EncryptVodafoneString(string message)
        {
            return EncryptString(message, VodKeyString, VodIVString, PaddingMode.Zeros);
        }

        public static string EncryptDDString(string message)
        {
            string KeyString = System.Web.Configuration.WebConfigurationManager.AppSettings["VodKeyString"];
            string IVString = System.Web.Configuration.WebConfigurationManager.AppSettings["VodIVString"];
            PaddingMode paddingMode = PaddingMode.Zeros;

            byte[] Key = ASCIIEncoding.UTF8.GetBytes(KeyString);
            byte[] IV = ASCIIEncoding.UTF8.GetBytes(IVString);

            string encrypted = null;
            RijndaelManaged rj = new RijndaelManaged();
            rj.BlockSize = 256;
            rj.Key = Key;
            rj.IV = IV;
            rj.Mode = CipherMode.CBC;
            rj.Padding = paddingMode;
            try
            {
                MemoryStream ms = new MemoryStream();

                using (CryptoStream cs = new CryptoStream(ms, rj.CreateEncryptor(Key, IV), CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(message);
                        sw.Close();
                    }
                    cs.Close();
                }
                byte[] encoded = ms.ToArray();
                encrypted = Convert.ToBase64String(encoded);

                ms.Close();
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return null;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file error occurred: {0}", e.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: {0}", e.Message);
            }
            finally
            {
                rj.Clear();
            }

            return encrypted;
        }
        public static string DecryptDDString(string message)
        {

            var sRet = "";
            string KeyString = System.Web.Configuration.WebConfigurationManager.AppSettings["VodKeyString"];
            string IVString = System.Web.Configuration.WebConfigurationManager.AppSettings["VodIVString"];
            PaddingMode paddingMode = PaddingMode.Zeros;
            var encoding = new UTF8Encoding();
            byte[] cypher = Decode(message);
            var Key = encoding.GetBytes(KeyString);
            var IV = encoding.GetBytes(IVString);

            using (var rj = new RijndaelManaged())
            {
                try
                {
                    rj.Padding = paddingMode;
                    rj.Mode = CipherMode.CBC;
                    rj.KeySize = 256;
                    rj.BlockSize = 256;
                    rj.Key = Key;
                    rj.IV = IV;
                    var ms = new MemoryStream(cypher);

                    using (var cs = new CryptoStream(ms, rj.CreateDecryptor(Key, IV), CryptoStreamMode.Read))
                    {
                        using (var sr = new StreamReader(cs))
                        {
                            sRet = sr.ReadLine();
                        }
                    }
                }
                finally
                {
                    rj.Clear();
                }
            }

            return sRet.Trim('\0');

        }
        /// <summary>
        /// Enkripton mesazhin duke perdorur celsat e imb
        /// </summary>
        /// <param name="message">mesazhi qe do enkriptohet</param>
        /// <returns></returns>
        public static string EncryptImbString(string message)
        {
            return EncryptString(message, ImbKeyString, ImbIVString, PaddingMode.PKCS7);
        }
        /// <summary>
        /// dekripton mesazhin duke perdorur celsin e imb
        /// </summary>
        /// <param name="crypt">mesazhi qe do dekriptohet</param>
        /// <returns></returns>
        public static String DecryptImbString(string crypt)
        {
            return DecryptString(crypt, ImbKeyString, ImbIVString, PaddingMode.PKCS7);
        }
  

        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

    }
}