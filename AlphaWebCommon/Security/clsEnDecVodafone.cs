using System;

namespace DbCore.IMBUtils.Security
{
    public class clsEnDecVodafone
    {
        #region Atribute

        private const string passPhrase = "Pas5pr@se";
        private const string saltValue = "s@1tValue";
        private const string hashAlgorithm = "SHA1";
        private const int passwordIterations = 2;
        private const string initVector = "@1B2c3D4e5F6g7H8";
        private const int keySize = 256;

        private const string passPhraseIMB = "Pas5pr@se1mb";
        private const string saltValueIMB = "s@1tValue1mb";
        private const string hashAlgorithmIMB = "SHA1";
        private const int passwordIterationsIMB = 2;
        private const string initVectorIMB = "@1B2c3D4e5F6g7H8";
        private const int keySizeIMB = 256;

        #endregion

        #region Konstruktor

        public clsEnDecVodafone()
        {
        }

        #endregion

        #region Metoda Publike

        /// <summary>
        /// Enkripton mesazhin ne baze te 6 parametrave konstant duke perdorur klasen RijndaelSimpleVodafone
        /// </summary>
        /// <param name="mesazhi">Stringa qe duhet enkriptuar</param>
        /// <returns>Kthen stringen e enkriptuar</returns>
        public static string enkriptoMesazh(string mesazhi)
        {
            if (!String.IsNullOrEmpty(mesazhi))
                return RijndaelSimple.EncryptVodafoneString(mesazhi);
                //return RijndaelSimpleOld.Encrypt(mesazhi, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
            return "";
        }

        /// <summary>
        /// Dekripton mesazhin ne baze te 6 parametrave konstant duke perdorur klasen RijndaelSimpleVodafone
        /// </summary>
        /// <param name="mesazhi">Stringa qe duhet dekriptuar</param>
        /// <returns>Kthen stringen e dekriptuar</returns>
        public static string dekriptoMesazh(string mesazhi)
        {
            if (!String.IsNullOrEmpty(mesazhi))
            {
                return RijndaelSimple.DecryptVodafoneString(mesazhi);
                //return RijndaelSimpleOld.Decrypt(mesazhi, passPhrase, saltValue, hashAlgorithm, passwordIterations, initVector, keySize);
            }
            return "";
        }

        /// <summary>
        /// Enkripton mesazhin ne baze te 6 parametrave konstant duke perdorur klasen RijndaelSimpleVodafone
        /// </summary>
        /// <param name="mesazhi">Stringa qe duhet enkriptuar</param>
        /// <returns>Kthen stringen e enkriptuar</returns>
        public static string enkriptoMesazhIMB(string mesazhi)
        {
            if (!String.IsNullOrEmpty(mesazhi))
                return RijndaelSimple.EncryptImbString(mesazhi);
                //return RijndaelSimpleOld.Encrypt(mesazhi, passPhraseIMB, saltValueIMB, hashAlgorithmIMB, passwordIterationsIMB, initVectorIMB, keySizeIMB);
            return "";
        }

        /// <summary>
        /// Dekripton mesazhin ne baze te 6 parametrave konstant duke perdorur klasen RijndaelSimpleVodafone
        /// </summary>
        /// <param name="mesazhi">Stringa qe duhet dekriptuar</param>
        /// <returns>Kthen stringen e dekriptuar</returns>
        public static string dekriptoMesazhIMB(string mesazhi)
        {
            if (!String.IsNullOrEmpty(mesazhi))
            {
                return RijndaelSimple.DecryptImbString(mesazhi);
                //return RijndaelSimpleOld.Decrypt(mesazhi, passPhraseIMB, saltValueIMB, hashAlgorithmIMB, passwordIterationsIMB, initVectorIMB, keySizeIMB);
            }
            return "";
        }

        #endregion
    }
}