using DbCore;
using DbCore.DbGIS;
using DbCore.DbAdmin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class GISDefault : MyPageBase
    {
        private string guidString;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            Page.Theme = "MetropolisBlue";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            redirectPage();

            if (!IsPostBack)
            {
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                //Merren info te nevojshme nga sessioni       
                int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
                int gjuha = mySessionObjects.ktheGjuhe(Session);
                int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                int idViti = mySessionObjects.ktheIdVitNdermarrje(Session);

                //ruhen si objekte ne faqen kryesore GIS
                hfState.Set("guidString", guidString);
                hfState.Set("idPerdoruesi", idPerdorues);
                hfState.Set("username", mySessionObjects.kthePerdorues(Session).PerdoruesUsername);
                hfState.Set("perkthimeHF", clsFunksioneGIS.merrPerkthimetPerGisDefault(gjuha));
                hfState.Set("gjuhaHF", gjuha);
                hfState.Set("GISNdermVitHF", mySessionObjects.ktheNdermarrjeVit(Session));

                //ruhet ne session ndermarrja qe te jete gati sa here qe te duhet
                clsNdermarrje ndermarrja = new clsNdermarrje(idNdermarrje);
                mySessionObjects.ruajNdermarrjePuneNeSession(Session, ndermarrja);
                string gisTheme = ndermarrja.NdermarrjeVendi;
                if (!(gisTheme == "Blue" || gisTheme == "Red" || gisTheme == "Purple"))
                    gisTheme = "Blue";
                hfState.Set("GISTheme", gisTheme);
                GISNdermarrjeHF.Value = new JavaScriptSerializer().Serialize(ndermarrja);
                
                //ruhet ne session tipi i logos qe te jete gati sa here qe te duhet                 
                System.Drawing.Image logoNdermarrje = System.Drawing.Image.FromStream(new MemoryStream(ndermarrja.NdermarrjeLogo));
                mySessionObjects.ruajMimeTypeLogoNdermarrjeNeSession(Session, clsFunksioneGIS.stringGetMimeType(logoNdermarrje));

                //merren te dhena per workspace dhe ruhen edhe ne session per tu kapur ne nje moment te dyte
                clsWorkspaceGIS workspaceAktual = new clsWorkspaceGIS();
                workspaceAktual.mbushWorkspaceSipasNdermarrjesId(idNdermarrje);
                mySessionObjects.ruajWorkspaceNeSession(Session, workspaceAktual);

                //merren te gjithe grupimet e folderave me te pakten nje layer qe ka te drejte leximi tek ai
                colLayersFoldersGIS tempF = new colLayersFoldersGIS();
                tempF.merrSipasTeDrejtaveFolderat(idPerdorues, gjuha, idNdermarrje, idViti, "D_AMB", 1, "");
                folderatShfaqurHF.Value = new JavaScriptSerializer().Serialize(tempF);

                //merren te gjithe tipet e layerave edhe ruhen ne sesion pasi perdoren disa here
                colLayersTypeGIS tempLType = new colLayersTypeGIS();
                tempLType.mbushLayersType();
                mySessionObjects.ruajLayersTypeNeSession(Session, tempLType);
                GISLayersTypeHF.Value = new JavaScriptSerializer().Serialize(tempLType);

                //merren te gjithe projeksionet e celura
                colGeoProjectionsGIS projeksione = new colGeoProjectionsGIS();
                projeksione.merrGeoProjectionsGIS();
                projeksioneHF.Value = new JavaScriptSerializer().Serialize(projeksione);

                var serverConfiguration = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.GIS_GEOSERVER_REQTIMEOUT);
                mySessionObjects.RuajNeSession(Session, serverConfiguration, "ServerConfiguration_GIS_GEOSERVER_REQTIMEOUT");

                //merren te gjithe perdoruesit e ndermarrjes
                colPerdoruesit perdorues = new colPerdoruesit();
                perdorues.mbushGjithePerdoruesitSipasAutorizimit(idPerdorues, 0);
                GISAllUsersHF.Value = new JavaScriptSerializer().Serialize(perdorues);

                string urlImazhPerdoruesi = DbCore.DbShare.clsArkiva.ktheImazhPerdoruesi(idPerdorues);
                if (String.IsNullOrEmpty(urlImazhPerdoruesi) || !System.IO.File.Exists(Server.MapPath(urlImazhPerdoruesi)))
                    urlImazhPerdoruesi = "../images/GIS/DEFAULT/menuUser.png";
                hfState.Set("GISUrlImazhPerdoruesi", urlImazhPerdoruesi);

                var userAutoLogin = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.GIS_USERNAME_DEFAULT);
                hfState.Set("GISAutoUserLogin", userAutoLogin.ToString());
                var licenca = new clsLicenca();
                licenca.mbushLicencen(IdPerdoruesi);
                hfState.Set("googleAnalytics", licenca.GoogleAnalytics);
                hfState.Set("googleAnalyticsTrackingId", licenca.GoogleAnalyticsTrackingId);
            }
        }

        private void redirectPage()
        {
            if (mySessionObjects.ktheIdPerdoruesi(Session) == 0)
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
                return;
            }
            else if (mySessionObjects.merrIdNdermarrjeSesioni(Session) == 0 || mySessionObjects.ktheIdVitNdermarrje(Session) == 0)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + mySessionObjects.ktheIdPerdoruesi(Session));
                return;
            }
            else
            {
                //merren te gjithe Layrat
                colDisplayLayersGIS displayLayers = new colDisplayLayersGIS();
                displayLayers.merrDisplayLayersGIS(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.ktheGjuhe(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdVitNdermarrje(Session));
                mySessionObjects.ruajDisplayLayersNeSession(Session, displayLayers);
                GISLayersHF.Value = new JavaScriptSerializer().Serialize(displayLayers);

                if (displayLayers.Count == 0)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + mySessionObjects.ktheIdPerdoruesi(Session));
                    return;
                }
            }
        }
    }
}