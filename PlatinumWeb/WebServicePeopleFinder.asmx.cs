using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using DbCore.IMBUtils.Security;

namespace PlatinumWeb
{
    /// <summary>
    /// Summary description for WebServicePeopleFinder
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebServicePeopleFinder : System.Web.Services.WebService
    {
        /// <summary>
        /// Metoda qe ekzekutohet nga Web Service per te kerkuar punonjesin.
        /// </summary>
        /// <param name="shprehjaXKerkim">(string) Shprehja e kerkimit te punonjes qe mund te jete emri, mbiemri ose numri i telefonit</param>
        /// <param name="nrTel">(string) Numri i telefoni ku do te dergohet sms.</param>
        /// <param name="username">(string) Username qe sherben per autentikim.</param>
        /// <param name="password">(string) Passwordi qe sherben per autentikim.</param>
        /// <param name="enc">(string) Koha kur eshte bere kerkesa e shprehjes per kerkim qe sherben per autentikim</param>
        /// <returns>Kthen nje list objektesh te rinj</returns>
        [System.Web.Services.WebMethod(EnableSession = false)]
        //[ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public List<PunonjesiPeopleFinder> kerkoPunonjesin(string shprehjaXKerkim, string nrTel, string username, string password, string enc)
        {
            string ipAdresa = HttpContext.Current.Request.UserHostAddress;

            if (!DbCore.DbAdmin.clsLoguPeopleFinder.bllokoLogimPeopleFinder())
            {
                if (Autentifiko(username, password, enc, ipAdresa).Status)
                {
                    DbCore.DbListPagesat.colPunonjes punonjesit = new DbCore.DbListPagesat.colPunonjes();
                    punonjesit.kerkoPunonjesPeopleFinder(shprehjaXKerkim, "");
                    List<PunonjesiPeopleFinder> listaPunonjesit = objektPeopleFinder(punonjesit);
                    if (listaPunonjesit.Count >= 0)
                    {
                        string dergoPunonjes = "";
                        if (listaPunonjesit.Count < 1)
                        dergoPunonjes = "Punonjesi per te cilin po kerkoni, nuk ekziston. Ju lutem kontrolloni edhe njehere te dhenat.";
                        for (int i = 0; i < listaPunonjesit.Count && i < 3; i++)
                        {
                            dergoPunonjes = String.Format("{0}{1} {2} - {3};", dergoPunonjes, listaPunonjesit[i].emer, listaPunonjesit[i].mbiemer, listaPunonjesit[i].telefon);
                        }
                        using (DbCore.VodSendSMS_Service.VFALSendSMSGateWay dergoSMS = new DbCore.VodSendSMS_Service.VFALSendSMSGateWay())
                        {
                            try
                            {
                                dergoSMS.Url = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["VodSendSMS_Service_URL"]);
                                dergoSMS.SendSMS(Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SendSMSVodUser"]), Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SendSMSVodPassword"]), Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["SendSMSVodOriginator"]), nrTel, dergoPunonjes);
                            }
                            catch (Exception err)
                            {
                                NLog.LogManager.GetCurrentClassLogger().Error("Pati nje gabim gjate dergimit te punonjesve! " + err);
                                DbCore.EmailComposer.dergoEmailFailDergimMesazhPeopleFinder(String.Format("People Finder authentication failed from " + ipAdresa + " on " + DateTime.Now + " because of " + "an error during SMS transfer! ", err));
                                return listaPunonjesit;
                            }
                        }
                    }
                    return listaPunonjesit;
                }
                return new List<PunonjesiPeopleFinder>();
            }

            DbCore.EmailComposer.dergoEmailFailAuthPeopleFinder("People Finder authentication failed from " + ipAdresa + " on " + DateTime.Now + " because " + "you have attempted to sign in with the wrong credentials too many times!");
            return new List<PunonjesiPeopleFinder>();
        }

        /// <summary>
        /// Kthen listen e objekteve clsPunonjes ne nje objekt te ri qe ka vetem tre nga fushat Emer, Mbiemer, Telefon
        /// </summary>
        /// <param name="punonjesit">(colPunonjes) Koleksioni i objekteve te punonjesit</param>
        /// <returns>Kthen nje list objektesh te rinj</returns>
        private static List<PunonjesiPeopleFinder> objektPeopleFinder(DbCore.DbListPagesat.colPunonjes punonjesit)
        {
            List<PunonjesiPeopleFinder> listPunonjesish = new List<PunonjesiPeopleFinder>();
            for (int i = 0; i < punonjesit.Count; i++)
            {
                DbCore.DbListPagesat.clsPunonjes punonjesi = punonjesit[i];
                listPunonjesish.Add(new PunonjesiPeopleFinder(punonjesi.Emer, punonjesi.Mbiemer, punonjesi.Telefon));
            }
            return listPunonjesish;
        }

        /// <summary>
        /// Kontrollon autentifikimin e perdoruesit te web servisit te VODAFONE
        /// </summary>
        /// <param name="username">(string) Username per autentifikim </param>
        /// <param name="password">(string) Password per autentifikim </param>
        /// <param name="enc">Koha e hyrjes e enkriptuar me klasen clsEnDecVodafone </param>
        /// <returns>Kthen mesazh nese eshte kryer apo jo autentifikimi </returns>
        private static DbCore.clsMesazh Autentifiko(string username, string password, string enc, string ipAdresa)
        {
            clsWebServiceAuten wsAut = new clsWebServiceAuten(username, password, enc);
            if (!wsAut.IsAutentik())
            {
                DbCore.DbAdmin.clsLoguPeopleFinder logPeopleFinder = new DbCore.DbAdmin.clsLoguPeopleFinder(ipAdresa, DateTime.Now, "Autentifikimi eshte i gabuar! Ju lutem vendosni nje celes te sakte!", "ERR_LOG_PEOPLEFINDER");
                logPeopleFinder.shtoLogError();
                DbCore.EmailComposer.dergoEmailFailAuthPeopleFinder("People Finder authentication failed from " + ipAdresa + " on " + DateTime.Now + " because of " + " wrong username/password!");
                return new DbCore.clsMesazh(false, "Autentifikimi eshte i gabuar! Ju lutem vendosni nje celes te sakte!");
            }
            int sekondaTeToleruara = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["toleroSekonda"]);
            TimeSpan diffTime = DateTime.Now - wsAut.LoginTime();
            if (diffTime.TotalSeconds > sekondaTeToleruara)
            {
                DbCore.DbAdmin.clsLoguPeopleFinder logPeopleFinder = new DbCore.DbAdmin.clsLoguPeopleFinder(ipAdresa, DateTime.Now, "Celesi nuk eshte me i vlefshem!", "ERR_LOG_KOHESERVER");
                logPeopleFinder.shtoLogError();
                DbCore.EmailComposer.dergoEmailFailAuthPeopleFinder("People Finder authentication failed from " + ipAdresa + " on " + DateTime.Now + " because of " + "key expired!");
                return new DbCore.clsMesazh(false, "Celesi nuk eshte me i vlefshem!");
            }
            return new DbCore.clsMesazh(true, "Autentifikimi u krye me sukses");
        }
    }

    public class PunonjesiPeopleFinder
    {
        public string emer;
        public string mbiemer;
        public string telefon;

        public PunonjesiPeopleFinder()
        {

        }

        public PunonjesiPeopleFinder(string emer, string mbiemer, string telefon)
        {
            this.emer = emer;
            this.mbiemer = mbiemer;
            this.telefon = telefon;
        }
    }
}
