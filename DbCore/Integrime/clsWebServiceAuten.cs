using System;
using DbCore.IMBUtils.Security;

namespace DbCore.Integrime
{
    public class clsWebServiceAuten
    {
        #region Atributet

        private static string userAutenticate = "Vodafone";
        private static string passAutenticate = "V0d@f0ne123?";
        private DateTime loginTime;
        private bool isAutentik = false;
        #endregion

        #region Konstruktori
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAuten"></param>
        /// <param name="enc"></param>
        public clsWebServiceAuten(string userAuten, string password, string enc)
        {
            try
            {
                if (userAutenticate.Equals(userAuten))
                {
                    loginTime = Convert.ToDateTime(clsEnDecVodafone.dekriptoMesazh(enc), new System.Globalization.CultureInfo("en-US", false));
                    isAutentik = true;
                }
            }
            catch (Exception)
            {
                isAutentik = false;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userAuten"></param>
        /// <param name="enc"></param>
        public clsWebServiceAuten(string userAuten, string password)
        {
            try
            {
                if (userAutenticate.Equals(clsEnDecVodafone.dekriptoMesazh(userAuten)) && passAutenticate.Equals(clsEnDecVodafone.dekriptoMesazh(password)))
                {
                    isAutentik = true;
                }
            }
            catch (Exception)
            {
                isAutentik = false;
            }
        }

        #endregion

        #region Metoda publike
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsAutentik(){
            return isAutentik;
        }
        public DateTime LoginTime(){
            return loginTime;
        }
        #endregion
    }
}
