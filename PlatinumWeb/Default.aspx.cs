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
                    //string komponente = clsKomponente.merrKomponenteDefaultPerdoruesi(idPerdoruesi);
                    string komponente = clsFunksione.ktheKomponenteDefaultPerPerdorues(idPerdoruesi, idNdermarrje, String.Empty, vitiNdermarrjes);
                    if (komponente != "")
                    {
                        Response.Redirect(komponente);
                        return;
                    }
                }

                DbCore.DbAdmin.clsViti vit = new DbCore.DbAdmin.clsViti();
                vit.mbushVitetMet(vitiNdermarrjes, idNdermarrje);
                mbushTeDhenaPerNdermarjen(nderm, vit);

            }
        }
        private void mbushTeDhenaPerNdermarjen(DbCore.DbAdmin.clsNdermarrje nderm, DbCore.DbAdmin.clsViti viti)
        {
            lblEmerNderm.Text = nderm.NdermarrjePershkrimi;
            lblEmerNIpti.Text = nderm.NdermarrjeNipt;
            lblEmerViti.Text = viti.KodiViti;
            lblEmerQyteti.Text = nderm.NdermarrjeQytetiPershkrimi;

        }
    }
}
