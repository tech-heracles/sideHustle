using System;
using System.Globalization;
using System.Web.Configuration;

namespace DbCore.IMBUtils.Security
{
    public class clsWebServiceAuten
    {
        #region Atributet

        private readonly static string userAutenticate = "Vodafone";
       // private readonly static string passAutenticate = "V0d@f0ne123?";
        private readonly DateTime loginTime;
        private readonly bool isAutentik = false;
        #endregion

        #region Konstruktori

        /// <summary>
        /// Konstruktori per autentifikim vetem me user
        /// </summary>
        /// <param name="userAuten">(string) Useri per autentifikim</param>
        /// <param name="enc">(string) Koha kur eshte kerkuar logimi</param>
        public clsWebServiceAuten(string userAuten, string enc)
        {
            try
            {
                if (userAutenticate.Equals(userAuten))
                {
                    this.loginTime = Convert.ToDateTime(clsEnDecVodafone.dekriptoMesazh(enc),new CultureInfo("en-US", false));
                    this.isAutentik = true;
                }
            }
            catch (Exception)
            {
                isAutentik = false;
            }
        }

        /// <summary>
        /// Konstruktori per autentifikim vetem me user
        /// </summary>
        /// <param name="userAuten">(string) Useri per autentifikim</param>
        /// <param name="passAuten">(string) Passwordi per autentifikim</param>
        /// <param name="enc">(string) Koha kur eshte kerkuar logimi</param>
        public clsWebServiceAuten(string userAuten, string passAuten, string enc)
        {
            try
            {
                if (Convert.ToString(WebConfigurationManager.AppSettings["PeopleFinderUser"]).Equals(clsEnDecVodafone.dekriptoMesazh(userAuten)) && Convert.ToString(WebConfigurationManager.AppSettings["PeopleFinderPassword"]).Equals(clsEnDecVodafone.dekriptoMesazh(passAuten)))
                {
                    this.loginTime = Convert.ToDateTime(enc, new CultureInfo("en-US", false));
                    this.isAutentik = true;
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
        /// Merr nese eshte i autentifikuar apo jo useri
        /// </summary>
        /// <returns>Kthen bool nese eshte autentifikuar apo jo useri. True nese eshte autentifikuar dhe false ne te kundert.</returns>
        public bool IsAutentik(){
            return isAutentik;
        }

        /// <summary>
        /// Merr kohen kur eshte kerkuar logini
        /// </summary>
        /// <returns>Kthen DateTime kohen kur eshte kerkuar logini.</returns>
        public DateTime LoginTime(){
            return loginTime;
        }

        #endregion
    }
}
