using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.DbShare;
using System.Data.SqlClient;
using System.Data;
using DevExpress.Web;
using System.Configuration;
using System.IO;
using System.Globalization;
using System.Resources;
using System.Reflection;
using DbCore.DbAdmin;
using DbCore;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using PlatinumWeb.ApplicationUtils.Pages;
using System.Web.Services;
using Newtonsoft.Json;

namespace PlatinumWeb
{
    public partial class _Default : MyPageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["kodi"] != null)
                    mySessionObjects.ruajKodNdermarrje(Request.QueryString["kodi"], Session);
                int idNdermarrje = 0;
                if (Request.QueryString["id"] != null)
                {
                    idNdermarrje = Convert.ToInt32(Request.QueryString["id"]);
                    DbCore.mySessionObjects.ruajIdNdermarrjeNeSesion(Session, idNdermarrje.ToString());
                }

                if (Request.QueryString["viti"] != null)

                    DbCore.mySessionObjects.ruajVitiNdermarrjes(Session, Request.QueryString["viti"]);
                int idNderViti = 0;
                if (Request.QueryString["idnderviti"] != null)
                {
                    idNderViti = Convert.ToInt32(Request.QueryString["idnderviti"]);
                    DbCore.mySessionObjects.ruajIdNdermarrjeVit(idNderViti.ToString(), Session);
                }

                //do merret perdoruesi sebashku me te drejtat dhe objekti i krijuar
                //do ruhet ne sesion. Ne menyre qe me vone te kontrollohen te drejtat
                //per kete perdorues.
                try
                {
                    if (idNdermarrje == 0)
                        idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                }
                catch (Exception err)
                {
                    ImbLogger.Error(err);
                    Response.Redirect("Login_Ndermarrje.aspx");
                    return;
                }
                var nderm = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
                DbCore.mySessionObjects.ruajRuajLogNeSesion(Session, nderm.LogNdermarrje);
                String kodiNdermarrjes = DbCore.mySessionObjects.ktheKodNdermarrje(Session);
                String vitiNdermarrjes = DbCore.mySessionObjects.ktheVitiNdermarrjes(Session).ToString();
                DbCore.mySessionObjects.ruajEshteNdermarjeMemeNeSesion(Session, nderm.Prind);
                DbCore.mySessionObjects.ruajEshteNdermarjeOwnNeSesion(Session, nderm.OwnShop);
                DbCore.mySessionObjects.ruajNdermRaportuese(Session, nderm.Raportuesi);
                int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                DbCore.DbAdmin.clsPerdorues perdorues = new DbCore.DbAdmin.clsPerdorues(idPerdoruesi);
                if (perdorues.IdPerdorues > 0)
                {
                    DbCore.mySessionObjects.ruajPerdoruesNeSesion(Session, perdorues);
                }
                else
                {
                    DbCore.mySessionObjects.ruajPerdoruesNeSesion(Session, new clsPerdorues());
                }
                if (Request.QueryString["kontrollodefault"] == "true")
                {
                    string komponente = clsFunksione.ktheKomponenteDefaultPerPerdorues(idPerdoruesi, idNdermarrje, String.Empty, vitiNdermarrjes);
                    if (komponente != "")
                    {
                        Response.Redirect(komponente);
                        return;
                    }
                }
            }
        }

        // ---- Kryefaqja (2026-11-27): favoritet me yll dhe raportet e moduleve brenda panelit te djathte ----

        private static bool EshteLoguar()
        {
            return HttpContext.Current != null && HttpContext.Current.Session != null && mySessionObjects.isLogedIn(HttpContext.Current.Session);
        }

        /// <summary>Faqet dhe raportet me yll te perdoruesit te loguar.</summary>
        [WebMethod(EnableSession = true)]
        public static string MerrFavoritet()
        {
            if (!EshteLoguar()) return "[]";
            try
            {
                return JsonConvert.SerializeObject(DbCore.DbShare.clsRaporti.MerrFavoritet(mySessionObjects.ktheIdPerdoruesi(HttpContext.Current.Session)));
            }
            catch (Exception err)
            {
                ImbLogger.Error(err);
                return "[]";
            }
        }

        /// <summary>Shton (shto = true) ose heq yllin e nje faqeje / raporti.</summary>
        [WebMethod(EnableSession = true)]
        public static bool NdryshoFavorit(string celesi, bool shto)
        {
            if (!EshteLoguar() || String.IsNullOrWhiteSpace(celesi) || celesi.Length > 300) return false;
            try
            {
                DbCore.DbShare.clsRaporti.NdryshoFavorit(mySessionObjects.ktheIdPerdoruesi(HttpContext.Current.Session), celesi.Trim(), shto);
                return true;
            }
            catch (Exception err)
            {
                ImbLogger.Error(err);
                return false;
            }
        }

        /// <summary>Raportet qe perdoruesi mund te hape, per secilin modul: { "2": [{ id, emri }], ... }.</summary>
        [WebMethod(EnableSession = true)]
        public static string MerrRaportet(int[] module)
        {
            var rezultati = new Dictionary<string, object>();
            if (!EshteLoguar() || module == null) return "{}";
            var session = HttpContext.Current.Session;
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(session);
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(session);
            int idGjuha = mySessionObjects.ktheGjuhe(session);
            int idViti = mySessionObjects.ktheIdVitNdermarrje(session);
            foreach (var idModuli in module.Distinct().Take(40))
            {
                try
                {
                    var dt = DbCore.DbShare.clsRaporti.KtheListRaportesh(idNdermarrje, idPerdoruesi, idGjuha, idViti, idModuli);
                    rezultati[idModuli.ToString()] = dt.Rows.Cast<DataRow>().Select(r => new
                    {
                        id = Convert.ToInt32(r["IDRAPORTI"]),
                        emri = Convert.ToString(r["RAPEMRI"])
                    }).ToList();
                }
                catch (Exception err)
                {
                    ImbLogger.Error(err);
                }
            }
            return JsonConvert.SerializeObject(rezultati);
        }
    }
}
