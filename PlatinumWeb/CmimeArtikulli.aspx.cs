using DbCore;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DevExpress.Web;
using DevExpress.Web.Data;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web.UI;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    /// <summary>
    /// Kjo klase sherben per te shfaqur shtuar e ndryshuar listen e cmimeve te artikujve
    /// </summary>
    public partial class CmimeArtikulli : MyPageBase
    {
        private static Logger _logu = LogManager.GetCurrentClassLogger();
        private int ShitjeApoBlerje;
        private const string KeyDataSourcePerSession = "gvCmimeArtikujsh";
        private const string KomponenteEmri = "CmimeArtikulli.aspx";

        protected void Page_PreInit(object sender, EventArgs e)
        {
            base.Page_PreInit(sender, e);
            ShitjeApoBlerje = Request.QueryString["lloji"] == "shitje" ? 0 : 1;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!mySessionObjects.isLogedIn(Session))
                {
                    clsFunksione.logout(Session, true, "FaqePaautorizuar");
                }
                if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + base.IdPerdoruesi);
                }

                hfState.Set("idPerdoruesi", IdPerdoruesi);
                hfState.Set("idNdermarrje", IdNdermarrja);
                hfState.Set("idGjuha", IdGjuha);
                hfState.Set("idViti", IdViti);
                hfState.Set("EmriFile", "Lista e cmimeve te shitjes");


                KonfiguroVleraFillestare();
                EmratELabelave();
                MbushHiddenFieldMePerkthime();
                EmrateButonave();

                var konf = new clsKonfigurimAmbjenti(Convert.ToInt32(cmbKonfigurimi.Value), IdGjuha);
                hfState.Set("konfigFillestar", JsonConvert.SerializeObject(new
                {
                    Kodi = konf.KodKonfigAmbjente,
                    Pershkrimi = konf.PershkrimKonfigAmbjente
                }));
            }
            PercaktoTemplateMenu(ASPxMenu1);
        }

        private void MbushHiddenFieldMePerkthime()
        {
            hfState.Set("headerPopUpZgjidhNivelinECmimit", MessagesResource.Messages["headerPopUpZgjidhNivelinECmimit"]);
            hfState.Set("headerPopUpZgjidhArtikullin", MessagesResource.Messages["headerPopUpZgjidhArtikullin"]);
            hfState.Set("headerPopUpZgjidhFurnitorin", MessagesResource.Messages["headerPopUpZgjidhFurnitorin"]);
            hfState.Set("cmbCmimeArtikulliVlere", MessagesResource.Messages["cmbCmimeArtikulliVlere"]);
            hfState.Set("cmbCmimeArtikulliRritje", MessagesResource.Messages["cmbCmimeArtikulliRritje"]);
            hfState.Set("cmbCmimeArtikulliPerqidje", MessagesResource.Messages["cmbCmimeArtikulliPerqidje"]);
            hfState.Set("headerPopUpZgjidhKodifikiminArtikullit", MessagesResource.Messages["headerPopUpZgjidhKodifikiminArtikullit"]);
            hfState.Set("cmbCmimeArtikulliKosto", MessagesResource.Messages["cmbCmimeArtikulliKosto"]);
            hfState.Set("msgCmimeArtikulliSasiteMinimaleDuhenNumerike", MessagesResource.Messages["msgCmimeArtikulliSasiteMinimaleDuhenNumerike"]);
            hfState.Set("msgCmimeArtikulliSasiteMaxDuhenNumerike", MessagesResource.Messages["msgCmimeArtikulliSasiteMaxDuhenNumerike"]);
            hfState.Set("msgCmimeArtikulliCmimetDuhenNumerike", MessagesResource.Messages["msgCmimeArtikulliCmimetDuhenNumerike"]);
            hfState.Set("msgCmimeArtikulliCmimet2DuhenNumerike", MessagesResource.Messages["msgCmimeArtikulliCmimet2DuhenNumerike"]);
            hfState.Set("headerPopUpTextZgjidhNdermarrjet", MessagesResource.Messages["headerPopUpTextZgjidhNdermarrjet"]);
            hfState.Set("msgCmimeArtikulliArtikujTeNdryshuarPorTePaSelektuar", MessagesResource.Messages["msgCmimeArtikulliArtikujTeNdryshuarPorTePaSelektuar"]);
            hfState.Set("msgCmimeArtikulliDoniTeVazhdoni", MessagesResource.Messages["msgCmimeArtikulliDoniTeVazhdoni"]);
            hfState.Set("cmbCmimeArtikulliBarazim", MessagesResource.Messages["cmbCmimeArtikulliBarazim"]);
            hfState.Set("lblModeli", MessagesResource.Messages["lblModeli"]);
            hfState.Set("cmbCmimeArtikulliZbritje", MessagesResource.Messages["cmbCmimeArtikulliZbritje"]);
    }

        private void EmratELabelave()
        {
            lblKonfigurimi.Text = MessagesResource.Messages["lblModeli"];
        }

        /// <summary>
        /// Vendos emrat e butonave ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateButonave()
        {
            btnNdrysho.Text = MessagesResource.Messages["btnNdrysho"];
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idViti"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNermarrje"></param>
        private void PercaktoTemplateMenu(ASPxMenu aSPxMenu1)
        {
            bool mosShfaqDetajim = Request.QueryString["lupe"] != null && Request.QueryString["lupe"].ToLower() == "true";
            clsToolbarConfig.percaktoTemplateMenu((int)hfState["idGjuha"], IdViti, IdPerdoruesi, IdNdermarrja, aSPxMenu1, clsFunksione.GetKomponente(Page.Request), this, MenuInfo, true, true, false, Meme, false, mosShfaqDetajim, !mosShfaqDetajim);
        }


        /// <summary>
        /// mbush kombot me vlerat perkatese
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        private void KonfiguroVleraFillestare()
        {
            AspxWebControlUtils.vendosDateEditMask(dteDtFillimi2, dteDtMbarimi2);
            ConfigureAspxComboBox.mbushComboRritjeZbritje(cmbRritjeZbritje, rm, ci);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(IdPerdoruesi, IdNdermarrja, cmbKonfigurimi, 17, rm, ci, IdGjuha);
            cmbKonfigurimi.SelectedIndex = Request.QueryString["lloji"] == "shitje" ? 0 : 1;
            cmbKonfigurimi.ClientEnabled = false;
            hfState.Set("nivelCmimiDefault", clsKusht.kthevlereSipasKushtitDheIdKonfig(Convert.ToInt32(cmbKonfigurimi.Value), "NCDNC"));
        }

    }
}