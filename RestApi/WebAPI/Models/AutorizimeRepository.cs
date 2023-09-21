using System;
using System.Web.SessionState;
using DbCore;
using DbCore.DbAdmin;
using System.Collections.Generic;
using System.Linq;
using DbCore.IMBUtils.Messages;
using System.Threading.Tasks;

namespace RestApi.WebAPI.Models
{
    public class AutorizimeRepository
    {
        public static object[] kaTeDrejteTeHapeAmbjentin(HttpSessionState Session, string emerkomponente, string url, bool newTab, int idNdermarje, int idPerdoruesi, int idGjuha)
        {
            object[] result = new object[3];
            string[] splitid = { "id=" };
            if (idNdermarje == 0)
                clsFunksione.logout(Session, true, "MbarimSessioni");
            int idNdermVit = mySessionObjects.ktheIdVitNdermarrje(Session);
            bool tedrejta = clsFunksione.kaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarje, idNdermVit, emerkomponente, !url.Contains("shtim_modifikim"), url.Contains("=modifikim"), (url.Contains("id=")) ? int.Parse(url.Split(splitid, StringSplitOptions.None)[1].Split('&')[0]) : 0, idGjuha, url);

            result[0] = tedrejta;
            if (tedrejta)
                result[1] = url;
            else
                result[1] = "Ju nuk keni te drejta ose autorizime per kete ambjent";
            result[2] = newTab;

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idPerdoruesi">id perdoruesi</param>
        /// <param name="idNdermarrje">id ndermarrje</param>
        /// <param name="idVitNdermarrje">id vitit te ndermarrjes</param>
        /// <param name="idGjuha">id e gjuhes</param>
        /// <param name="emerkomponente">emri i komponentes</param>
        /// <param name="url">emer komponente bashke me querystring</param>
        /// <returns>Tuple<bool,string,string>(kaTeDrejta,url,mesazhErrori)</returns>
        public static bool kaTeDrejteTeHapeAmbjentin(int idPerdoruesi, int idNdermarrje, int idVitNdermarrje, int idGjuha, string emerkomponente, Dictionary<string, object> queryParams)
        {
            int idDokumenti = 0;
            object idDokuObj = 0;

            int idKonfig = 0;
            object idKonfigStr = 0;

            int idNiveli = 0;
            object idNiveliStr = 0;
            bool modifikim = queryParams.ContainsValue("modifikim");
            bool shtim_mod = queryParams.ContainsKey("shtim_modifikim") || queryParams.ContainsValue("konvertim");

            string veprimi = queryParams.ContainsKey("shtim_modifikim")
                ? queryParams["shtim_modifikim"].ToString()
                : "";

            if (!queryParams.TryGetValue("id", out idDokuObj) || !int.TryParse(idDokuObj?.ToString(), out idDokumenti)) idDokumenti = 0;

            if (!queryParams.TryGetValue("idkonfig", out idKonfigStr) || !int.TryParse(idKonfigStr?.ToString(), out idKonfig)) idKonfig = 0;

            if (!queryParams.TryGetValue("idniveli", out idNiveliStr) || !int.TryParse(idNiveliStr?.ToString(), out idNiveli)) idNiveli = 0;

            return clsFunksione.KaTeDrejteTeHapeAmbjentin(idPerdoruesi, idNdermarrje, idVitNdermarrje, emerkomponente, !shtim_mod, modifikim, idDokumenti, idGjuha, idKonfig, idNiveli, veprimi);

        }
        public static bool fshiGridNgaSessioniLupa(HttpSessionState Session)
        {
            return mySessionObjects.fshiGridNgaSessioniLupa(Session);
        }

        /// <summary>
        /// Ky webservice merr ne input urlne e komponentes dhe kthen emrin dhe si dhe emrin e perdoruesit te loguar
        /// </summary>
        /// <param name="Session"></param>
        /// <param name="urlKomponente"></param>
        /// <param name="idDokRegjistrimi"></param>
        /// <returns></returns>
        public static object KtheInfoLart(HttpSessionState Session, string urlKomponente, int idDokRegjistrimi, int idNdermarrje, int IdPerdoruesi, int idGjuha)
        {
            bool ruajLog = mySessionObjects.merrRuajLog(Session);
            if (urlKomponente == "Raporti.aspx")
                ruajLog = false;
            return new { emerKomponente = clsFunksione.logoAmbient(urlKomponente, idDokRegjistrimi.ToString(), idNdermarrje, IdPerdoruesi, ruajLog, MessagesResource.KtheCultureInfo(idGjuha).IetfLanguageTag) };
        }

        internal static object KtheInfoLart(string urlKomponente, int id, int idNdermarrje, int idPerdoruesi, bool logu, string ci)
        {
            return clsFunksione.logoAmbient(urlKomponente, id.ToString(), idNdermarrje, idPerdoruesi, logu, ci);
        }
        internal async static Task<string> createLoginWithGmail(string uid, int idNdermarrje, int idPerdoruesi, string email, HttpSessionState session, string accessToken)
        {
            return await clsFunksione.createLoginWithGmail(uid, idNdermarrje, idPerdoruesi, email, session, accessToken);
        }
        internal async static Task<object> userControls(int idPerdoruesi, string email, int idNdermarje, string alphaOrganization, string uid, string accessToken)
        {
            return await clsFunksione.userControls(idPerdoruesi, email, idNdermarje, alphaOrganization, uid, accessToken);
        }
        internal async static Task<object> goToDelta(int idPerdoruesi, string email, int idNdermarje, string alphaOrganization, string uid, string accessToken)
        {
            return await clsFunksione.goToDelta(idPerdoruesi, email, idNdermarje, alphaOrganization, uid, accessToken);
        }
        internal async static Task<bool> merrShenimePerdoruesi(string shenime)
        {
            return await clsFunksione.merrShenimePerdoruesi(shenime);
        }
        public static bool ruajNeSessionURLART(HttpSessionState Session, string url)
        {

            mySessionObjects.ruajURLARTNeSesion(Session, url);
            return true;

        }
        public static clsPeriudhaKontabel vendosPeriudhen(HttpSessionState Session, int idPeriudha, int idgjuha, string otherScopeID)
        {
            clsPeriudhaKontabel periudha = new clsPeriudhaKontabel(idPeriudha, idgjuha);
            mySessionObjects.ruajPeriudheKontabelNeSesion(periudha, Session);
            mySessionObjects.ruajPeriudheKontabelNeSesion(periudha, Session, otherScopeID);
            return periudha;

        }

        public static string ktheMesazhPerPerdoruesin()
        {
            return clsFunksione.KtheMesazhPerPerdoruesin();
        }
        public static bool KonfirmoEmail(string email)
        {
            return clsFunksione.KonfirmoEmail(email);
        }
        public static async Task<bool> CheckIfEmailIsVerified(string email)
        {
            return await clsFunksione.CheckIfEmailIsVerified(email);
        }

    }
}