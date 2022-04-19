using DbCore;
using DbCore.DbAdmin;
using DbCore.DbCRM;
using DbCore.DbShare;
using DevExpress.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web.UI;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;

namespace PlatinumWeb
{
    public partial class CRMHarte : MyPageBase
    {
        private const string komponente = "CRMHarte.aspx";
        private const int idKomponente = 2003;
        private const string mesazhZgjidhniNje = "Ju lutem zgjidhni te pakten nje Takim!";
        private const int idstatusdok = 1;
        private int idPerdoruesi, idNdermarrjeVit, idViti, idGjuha;
        private int idNdermarrje;
        private string[] periudhat;
        private int idKonfig;
        private CultureInfo cultinf;
        private ResourceManager rm;
        private string filter;

        /// <summary>
        /// perdoret per te vendosur themen e devexpresit faqes
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumentat </param>
        protected void Page_PreInit(object sender, EventArgs e)
        {
            KontrolloTeDrejtaDheAutorizim(mySessionObjects.merrIdNdermarrjeSesioni(Session),mySessionObjects.ktheIdVitNdermarrje(Session));
            Page.Theme = "Moderno";
        }

        /// <summary>
        /// thirret kur faqja lodohet. Ne te behet kontrolli nese perdoruesi eshte i loguar ne
        /// sistem dhe nqs jo ridrejtohet tek forma e logimit thirret inicializimi i konfigurimeve
        /// fillestare te faqes
        /// </summary>
        /// <param name="sender"> derguesi </param>
        /// <param name="e">      argumenti </param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                Response.Redirect(DbCore.IMBUtils.Paths.defaultLoginPath);
            }
            idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            var hfState = callbackPanel.FindControl("hfState") as ASPxHiddenField;
        
            clsPeriudhaKontabel periudhaKontabel = DbCore.mySessionObjects.merrPeriudheKontabel(Session);

            if (!Page.IsPostBack)
            {
                
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);

                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("idViti", idViti);
               // lblUserEmri.Text = DbCore.mySessionObjects.ktheEmerPerdorues(Session);
                cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();

                konf.mbushKonfigAmbjSipasId(idKonfig, idGjuha);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                hfTeDrejta["Ambjenti"] = tedrejtaInfo.DAmb;
                //hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                //hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                EmratELabelave(cultinf, hfState);
                KonfiguroVleraFillestare(idNdermarrje);

                merrKontrolleFiltra(idNdermarrje, idPerdoruesi, periudhaKontabel);

               
            }
            else
            {
                idGjuha = (int)hfState["idGjuha"];
               
                hfState.Set("idPerdoruesi", idPerdoruesi);
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                idViti = (int)hfState["idViti"];

                cultinf = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            }
            List<DbCore.DbCRM.colSkeduler.LevizjeAgjentiModel> levizjet = colSkeduler.merrLevizetEAgjenteve(idNdermarrje, krijoFilterString(periudhaKontabel));

            RuajVleratNeHiddenField(levizjet, hfState);
           
            // periudhat = clsFunksione.merrPeriudhenNgaDeringaHiddenField(hfState);

            percaktoTemplateMenu(ASPxMenuToolBar,idViti,idPerdoruesi,idNdermarrje);
        }

        private void merrKontrolleFiltra( int idNdermarrje, int idPerdoruesi, clsPeriudhaKontabel periudha)
        {
            ASPxRadioButtonList radDtDok = navBarFiltrat.Groups[0].FindControl("radDtDok") as ASPxRadioButtonList;
            ASPxDateEdit txtNgaDok = navBarFiltrat.Groups[0].FindControl("txtNgaDok") as ASPxDateEdit;
            ASPxDateEdit txtDeriDok = navBarFiltrat.Groups[0].FindControl("txtDeriDok") as ASPxDateEdit;

            radDtDok.Value = "Aktuale";
            txtNgaDok.Date = periudha.FillimiPeriudha;
            txtDeriDok.Date = periudha.MbarimiPeriudha;
        }
        private string krijoFilterString(clsPeriudhaKontabel periudhaKontabel)
        {

           
            string agjentet = " ";
            string periudha = "";
            ASPxComboBox cmbAgjenti = navBarFiltrat.Groups[1].FindControl("cmbAgjentShitjesh") as ASPxComboBox;

            if (!string.IsNullOrWhiteSpace(cmbAgjenti.Text))
            {
                agjentet = string.Format(" AND KODIAGJENTSHITJE IN ('{0}') ", string.Join("','", cmbAgjenti.Text.Split(',')));

            }

            ASPxRadioButtonList radioDtList = navBarFiltrat.Groups[0].FindControl("radDtDok") as ASPxRadioButtonList;
            if (radioDtList.SelectedItem != null)
            {
                switch (radioDtList.SelectedItem.Value.ToString())
                {

                    case "Aktuale":
                        periudha = string.Format(" AND CONVERT(date,DATEFILLIMREALIZIMI) BETWEEN '{0}' AND '{1}' ", periudhaKontabel.FillimiPeriudha.ToString("yyyy-MM-dd"), periudhaKontabel.MbarimiPeriudha.ToString("yyyy-MM-dd"));
                        break;
                    case "VitiUshtrimor":

                        clsNdermarrjeViti ndermviti = new clsNdermarrjeViti(idNdermarrjeVit);
                        periudha = string.Format(" AND CONVERT(date,DATEFILLIMREALIZIMI) BETWEEN '{0}' AND '{1}' ", ndermviti.NdermarrjeVitiFillim.ToString("yyyy-MM-dd"), ndermviti.NdermarrjeVitiFund.ToString("yyyy-MM-dd"));

                        break;

                    case "Periudha":
                        ASPxDateEdit txtNgaDok = navBarFiltrat.Groups[0].FindControl("txtNgaDok") as ASPxDateEdit;
                        ASPxDateEdit txtDeriDok = navBarFiltrat.Groups[0].FindControl("txtDeriDok") as ASPxDateEdit;
                        periudha = string.Format(" AND CONVERT(date,DATEFILLIMREALIZIMI) BETWEEN '{0}' AND '{1}' ", txtNgaDok.Date.ToString("yyyy-MM-dd"), txtDeriDok.Date.ToString("yyyy-MM-dd"));

                        break;

                    case "GjitheVitet":
                        periudha = string.Format(" AND CONVERT(date,DATEFILLIMREALIZIMI) BETWEEN '{0}' AND '{1}' ", Convert.ToDateTime("1900-01-01").ToString("yyyy-MM-dd"), DateTime.MaxValue.ToString("yyyy-MM-dd"));
                        break;

                }
            }
           return agjentet + periudha;
        }
        private void RuajVleratNeHiddenField(List<DbCore.DbCRM.colSkeduler.LevizjeAgjentiModel> lista, ASPxHiddenField hfState)
        {
            var col = lista.GroupBy(x => new { x.Agjenti, x.DateFillimRealizimi.Date })
                .Select(x => new
                {
                    Koka = new { Agjenti = x.Key.Agjenti, Data = x.Key.Date.ToString("dd/MM/yyyy") },
                    Koordinata = x.Select(s => s.KoordinateFillimi),
                    TrupatInfo = x.Select(s => new
                    {
                        Klienti = s.Klienti,
                        Ora = s.Ora,
                        Kohezgjatja = s.Kohezgjatja,
                        NrTakimi = s.NrTakimi,
                        Shenime = string.IsNullOrWhiteSpace(s.Description)?" ":s.Description,
                        TeKlienti = s.TeKlienti
                    })
                }).ToList();

            hfState.Set("data", JsonConvert.SerializeObject(col));
        }

        private void percaktoTemplateMenu(ASPxMenu aSPxMenu1, int idViti, int idPerdorues, int idNdermarrje)
        {
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheGjuhe(Session), aSPxMenu1, MenuInfo,Ruaj_ASPxButton_Click,ASPxButtonFshiFilterOk_Click, null, null, DbCore.mySessionObjects.merrEshteMemeSesioni(Session), idNdermarrje, this);
        }


        public static void percaktoTemplateMenu(int idgjuha, ASPxMenu aSPxMenu1, ASPxMenu MenuInfo, EventHandler Ruaj_ASPxButton_Click, EventHandler FshiFilter_ASPxButton_Click, EventHandler btnPo_Click, EventHandler btnJo_Click, bool meme, int idNdermarrje, Page page)
        {
            DbCore.DbShare.colMenuItem menu = new DbCore.DbShare.colMenuItem(idgjuha);
            menu.merrMenuItemSipasKomponentes(idgjuha, 649);

            foreach (DbCore.DbShare.clsMenuItem m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame" && m.Name != "ItemExport")
                {
                    if (!m.Enabled) continue;
                    clsToolbarConfig.ShtoMenuItem(page.Theme, aSPxMenu1, m);
                    if (m.Name == "Ruaj")
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                    if (m.Name == "Sinkronizo" && !meme)
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    if (m.Name == "Trasfero" && !meme)
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                else
                    if (m.Name == "ItemFilter")
                    {
                        //krijohen handler per te caktuar evente server side per kontrolle
                        //keto handler i kalohen si parametra user control per filtrat
                        EventHandler handlerPerRuajFilter = new EventHandler(Ruaj_ASPxButton_Click);
                        EventHandler handlerPerFshiFilter = new EventHandler(FshiFilter_ASPxButton_Click);

                        //shtohet ne menu user control per filtrat e grides
                        clsToolbarConfig.ShtoMenuItemPerFilterRaport(page, aSPxMenu1, handlerPerRuajFilter, handlerPerFshiFilter);
                    }
                    else
                    {
                        clsToolbarConfig.ShtoMenuItemPerFrame(page, aSPxMenu1, DbCore.clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                    }
                if (m.Name == "Shto" || m.Name == "Ndihme" || m.Name == "ItemFilter" || m.Name == "ItemFrame" || m.Name == "Grupo" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemExport")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;

                if (m.Name == "Arkiva")
                {
                    DbCore.DbAdmin.clsNdermarrje nd = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
                    if (!nd.isArkiva) aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
            }
            if (btnJo_Click != null)
                clsMenuInfo.ShtoMenuItemInfo(page, MenuInfo, btnPo_Click, btnJo_Click);
            else clsMenuInfo.ShtoMenuItemInfo(page, MenuInfo);
        }


       public void Ruaj_ASPxButton_Click (object sender, EventArgs e){}
        public void ASPxButtonFshiFilterOk_Click(object sender, EventArgs e)
        {
            //DevExpress.Web.MenuItem itemButton = ASPxMenuToolBar.Items.FindByName("TemplatedItemFilter");
            //ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            //int idFilter = Convert.ToInt32(cmbFiltra.Value);
            //clsFilterKoka filtri = new clsFilterKoka(idFilter);
            //int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            //filtri.IdPerdoruesi = idPerdoruesi;
            //DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            //mesazh = filtri.fshistatus();
            //int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //cmbFiltra.Text = "";
            //mbushComboBoxFiltra(idPerdoruesi, idNdermarrje);
            //percaktoTemplateMenu(ASPxMenuToolBar, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idPerdoruesi, idNdermarrje);

            //if (mesazh.Status)
            //    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            //else
            //    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            ////cmbFiltra.Items.Remove(new ListEditItem(Convert.ToString(cmbFiltra.Value)));
            //cmbFiltra.DataBind();

            //((UpdatePanel)navBarFiltrat.Groups[0].FindControl("updfiltrat")).Update();
        }

        private void KonfiguroVleraFillestare(int idNdermarrje)
        {
            ASPxComboBox cmbAgjenti = navBarFiltrat.Groups[1].FindControl("cmbAgjentShitjesh") as ASPxComboBox;
            ConfigureAspxComboBox.percaktoTemplateComboMeEnableCallback(cmbAgjenti);

            ConfigureAspxComboBox.KonfiguroComboBoxAgjentesh( IdNdermarrja, 0, cmbAgjenti);
        }

        private void EmratELabelave(CultureInfo ci, ASPxHiddenField hfState)
        {
            ((ASPxLabel)navBarFiltrat.Groups[1].FindControl("lblAgjentShitje")).Text = MessagesResource.Messages["filterRaportiAgjentetShitjes"];
            hfState.Set("msgRaportiDuhetTeFshiniNdaresinNgaFushaEKodit", MessagesResource.Messages["msgRaportiDuhetTeFshiniNdaresinNgaFushaEKodit"]);
            hfState.Set("msgZgjidhAgjentinEShitjes", MessagesResource.Messages["msgZgjidhAgjentinEShitjes"]);
        }

        protected void cmbAgjentShitjesh_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback && Request.Params["__CALLBACKID"].Contains("cmbAgjentShitjesh"))
                ConfigureAspxComboBox.KonfiguroComboBoxAgjentesh(IdNdermarrja, 0, (ASPxComboBox)navBarFiltrat.Groups[1].FindControl("cmbAgjentShitjesh"));
        }

       
    }
}