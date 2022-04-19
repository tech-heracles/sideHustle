using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using System.Collections.Generic;
using DbCore.DbRegjistrim;
using System.Web.Script.Serialization;
using DbCore;
using PlatinumWeb.Templates;
using System.Collections;
using System.Resources;
using System.Globalization;
using DbCore.DbShare;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using Newtonsoft.Json;

namespace PlatinumWeb
{
    public partial class Shto_Planifikim : MyPageBase
    {
        private const string emriKomponentes = "Shto_Planifikim.aspx";
        /// <summary>
        /// mbush fushat gjate modifikimit te dokumentit
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idNderViti">id e ndermarje vitit</param>
        /// <param name="idNdermarrje"> id e ndermarjes</param>
        /// <param name="koka">koka e planifikimit</param>
        public void MerrTedhenat(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, DbCore.DbProdhimi.clsKokaPlanifikim koka)
        {
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente, idGjuha);
            cmbKonfigurimi.Text = konf.KodKonfigAmbjente;
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup1);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup1, idNdermarrje, 1, koka.IdKonfigAmbjente, idPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup2);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup2, idNdermarrje, 2, koka.IdKonfigAmbjente, idPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup3);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup3, idNdermarrje, 3, koka.IdKonfigAmbjente, idPerdoruesi);
            hfKonffillestar.Value = konf.KodKonfigAmbjente;
            // hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
            if (koka.IdKlientFurnitor != 0)
                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(btneKlientFurnitori, koka.IdKlientFurnitor);
            if (koka.IdMagazina != 0)
                btneMagazina.Text = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(koka.IdMagazina);
            txtNrDok.Text = koka.NrDok;
            dteDtDok.Date = koka.DtDok;
            dteDtRegjistrimi.Date = koka.DtRegj;
            txtShenime.Text = koka.Shenime;
            dteAfatiKohor.Date = koka.AfatiKohor;
            cmbGrup1.Value = koka.IdGrup1.ToString();
            cmbGrup2.Value = koka.IdGrup2.ToString();
            cmbGrup3.Value = koka.IdGrup3.ToString();
            if (koka.IdNjesiProdhimi > 0)
            {
                cmbNjesiProdhimi.Value = koka.IdNjesiProdhimi;
                cmbNjesiProdhimi.Text = clsNjesiProdhimi.ktheKodNjesiProdhimiSipasId(koka.IdNjesiProdhimi);                
            }
            DataTable dtlidhur = koka.merrIdsDokLidhur();
            hfLidhur.Value = (!(dtlidhur.Rows.Count == 0)).ToString();

            bool autorizimet = DbCore.DbProdhimi.clsKokaPlanifikim.kaAutorizime(koka.IdKokaPlanifikim, idPerdoruesi);
            hfAutorizimi.Value = autorizimet.ToString();
            if (!autorizimet)
                hfLidhur.Value = "True";
            cmbFormatiPrintimit.Value = koka.IdRaportDesing.ToString();
            AspxWebControlUtils.ShtoLidhje(idPerdoruesi, idViti, idNdermarrje, hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, idGjuha);
            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 804, "btneKlientFurnitori", -1, true);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            mySessionObjects.ruajFormatSasia(formatMonedhe.ShifraPasPresjesSasia, Session);
        }

        /// <summary>
        /// perdoret per te vendosur theme
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>


        /// <summary>
        /// mbush te dhenat kur faqja ben loadim
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Globalization.CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            
            if (!DbCore.mySessionObjects.isLogedIn(Session))
            {
                DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
            }
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            int idNdermarrje, idViti;


            if (!IsPostBack)
            {
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                mbushHiddenFieldMePerkthime(cultinf, rm);
                int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                hfState.Set("idGjuha", idgjuha);
                hfState.Set("idPerdorues", idPerdoruesi);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);                
                hfState.Set("IdKomponente", DbCore.DbAdmin.clsKomponente.MerrIdKomponenteSipasEmrit(emriKomponentes));
                if (hfShtimModifikim.Value == "")
                {
                    if (Request.QueryString["shtim_modifikim"] == "shtim" || String.IsNullOrEmpty(Request.QueryString["shtim_modifikim"]))
                    {
                        hfShtimModifikim.Value = "shtim";
                        konfiguroVleraFillestareShto(idgjuha, idNdermarrje, rm, cultinf);
                    }
                    else
                    {
                        hfShtimModifikim.Value = "modifikim";
                        konfiguroVleraFillestareModifiko(idgjuha, idPerdoruesi, idViti, idNdermarrje, rm, cultinf);
                    }
                    percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
                }
                else if (hfShtimModifikim.Value == "shtim")
                          konfiguroVleraFillestareShto(idgjuha, idNdermarrje, rm, cultinf);

                else if (hfShtimModifikim.Value == "modifikim")
                         konfiguroVleraFillestareModifiko(idgjuha, idPerdoruesi, idViti, idNdermarrje, rm, cultinf);
              
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_Planifikim.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
                colNjesiAdministrative col = new colNjesiAdministrative();
                if (hfAutorizimi.Value == "False")
                    col.mbushGjitheNjesiAdministrativeSipasLlojit(idNdermarrje, 1);
                else
                    col.mbushGjitheNjesiAdministrative(idNdermarrje, idPerdoruesi, 1);
                DbCore.mySessionObjects.ruajObjectNeSesion(Session, col, "colMagPlanifikimi");
                hfState.Set("colMagPlanifikimi", JsonConvert.SerializeObject(col.Select(x => new { x.IdNjesiAdministrative, x.Kodi }).ToArray()));
            }
            else {
                idNdermarrje = (int)hfState["idNdermarrje"];
                idPerdoruesi = (int)hfState["idPerdorues"];
                idViti = (int)hfState["idViti"];
                percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
            }
            Container.Attributes["src"] = string.Empty;
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {

            hfState.Set("msgZgjidhDokumentin", rm.GetString("msgZgjidhDokumentin", cultinf));
            hfState.Set("msgZgjidhMagazinen", rm.GetString("msgZgjidhMagazinen", cultinf));
            hfState.Set("headerZgjidhKlientFurnitorin", rm.GetString("headerZgjidhKlientFurnitorin", cultinf));
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", rm.GetString("msgPoTransferohetTeDhenatShtypniPerseriRuaj", cultinf));
            hfState.Set("msgShenoniNumrinEDokumentit", rm.GetString("msgShenoniNumrinEDokumentit", cultinf));
            hfState.Set("msgZgjidhniNjeDateDokumenti", rm.GetString("msgZgjidhniNjeDateDokumenti", cultinf));
            hfState.Set("msgZgjidhniNjeDateRegjstrimi", rm.GetString("msgZgjidhniNjeDateRegjstrimi", cultinf));
            hfState.Set("msgKFnukEkziston", rm.GetString("msgKFnukEkziston", cultinf));
            hfState.Set("msgNukKeniAutorizimPerTeRuajturKeteDok", rm.GetString("msgNukKeniAutorizimPerTeRuajturKeteDok", cultinf));
            hfState.Set("msgNukKeniAutorizimPerTeFshireDok", rm.GetString("msgNukKeniAutorizimPerTeFshireDok", cultinf));
            hfState.Set("roundPanelZgjidhArtikullin", rm.GetString("roundPanelZgjidhArtikullin", cultinf));
            hfState.Set("msgEkzistonArtikullGride", rm.GetString("msgEkzistonArtikullGride", cultinf));
            hfState.Set("msgKyArtikullNukIPerketKlasesProdhimOsePProces", rm.GetString("msgKyArtikullNukIPerketKlasesProdhimOsePProces", cultinf));
            hfState.Set("msgSasiaNumer", rm.GetString("msgSasiaNumer", cultinf));
            hfState.Set("msgGabimGjeresiaNumer", rm.GetString("msgGabimGjeresiaNumer", cultinf));
            hfState.Set("msgGjeresiaDuhetNrPozitiv", rm.GetString("msgGjeresiaDuhetNrPozitiv", cultinf));
            hfState.Set("msgGabimGjatesiNumer", rm.GetString("msgGabimGjatesiNumer", cultinf));
            hfState.Set("msgGjatesiaDuhetNrPozitiv", rm.GetString("msgGjatesiaDuhetNrPozitiv", cultinf));
            hfState.Set("MenuKokeDokumenti", rm.GetString("MenuKokeDokumenti", cultinf));
            hfState.Set("MenuTrupDokumenti", rm.GetString("MenuTrupDokumenti", cultinf));
            hfState.Set("MenuFundDokumenti", rm.GetString("MenuFundDokumenti", cultinf));
            hfState.Set("msgArtikullpaDetajim", rm.GetString("msgArtikullpaDetajim", cultinf));
            hfState.Set("msgZgjidhDetajimArtikulli", rm.GetString("msgZgjidhDetajimArtikulli", cultinf));
            hfState.Set("msgDetajimiVendosurNukEkzistonDoniTaCelni", rm.GetString("msgDetajimiVendosurNukEkzistonDoniTaCelni", cultinf));
            hfState.Set("msgShtoDetajim", rm.GetString("msgShtoDetajim", cultinf));
            hfState.Set("msgArtSkaKategoriPerDetajim", rm.GetString("msgArtSkaKategoriPerDetajim", cultinf)); 
            hfState.Set("msgKodiDateSkadenceDuhetFormat", rm.GetString("msgKodiDateSkadenceDuhetFormat", cultinf)); 
            hfState.Set("msgDetajimJoLidhur", rm.GetString("msgDetajimJoLidhur", cultinf));
        }


        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idPerdoruesi, int idViti, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            DbCore.DbShare.colMenuItem menu = new DbCore.DbShare.colMenuItem(DbCore.mySessionObjects.ktheGjuhe(Session));
            menu.merrMenuItemSipasKomponentesRegjistrime(DbCore.mySessionObjects.ktheGjuhe(Session), "Shto_Planifikim.aspx", idPerdoruesi, idNdermarrje, idViti, hfShtimModifikim.Value == "modifikim" ? false : true);

            foreach (DbCore.DbShare.clsMenuItem m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                {
                    clsToolbarConfig.ShtoMenuItem(this.Theme, aSPxMenu1, m);
                }

                if ((hfLidhur.Value == "True") && m.Name == "Draft")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "PrintPreview")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "Fshi")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;

                if (m.Name == "Ruaj")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                if (m.Name == "ItemFrame")
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
            }

            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);
        }

        /// <summary>
        /// vendos daten default
        /// </summary>
        private void vendosDataDefault()
        {
            DateTime sot = new DateTime();
            sot = DateTime.Today;
            DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
            {
                dteDtDok.Value = DateTime.Today;
                dteAfatiKohor.Value = DateTime.Today;
            }
            else
            {
                dteDtDok.Value = periudha.FillimiPeriudha;
                dteAfatiKohor.Value = periudha.FillimiPeriudha;
            }
        }

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestareShto(int idGjuha, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf)
        {
            dteDtDok.Date = DateTime.Today;
            dteAfatiKohor.Date = DateTime.Today;
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            AspxWebControlUtils.vendosDateEditMask(dteAfatiKohor);
            dteDtRegjistrimi.Date = DateTime.Today;
            vendosDataDefault();
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina);
            ConfigureAspxComboBox.ShtoKolonaPerKf(btneKlientFurnitori);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneKlientFurnitori);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbNjesiProdhimi);
            ConfigureAspxComboBox.mbushComboNjesiProdhimi(idNdermarrje, cmbNjesiProdhimi, false);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 44, rm, cultinf, idGjuha);
            cmbKonfigurimi.SelectedIndex = 0;
            cmbKonfigurimi.TextFormatString = "{0}";
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup1);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup1, idNdermarrje, 1, int.Parse(cmbKonfigurimi.Value.ToString()), idPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup2);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup2, idNdermarrje, 2, int.Parse(cmbKonfigurimi.Value.ToString()), idPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup3);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup3, idNdermarrje, 3, int.Parse(cmbKonfigurimi.Value.ToString()), idPerdoruesi);
            hfKonffillestar.Value = cmbKonfigurimi.Text;
            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btneMagazina, idPerdoruesi, true, 1,true);
            ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatiPrintimit, 44, idNdermarrje);
            cmbFormatiPrintimit.SelectedIndex = 0;
            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 804, "btneKlientFurnitori", -1, true);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            mySessionObjects.ruajFormatSasia(formatMonedhe.ShifraPasPresjesSasia, Session);
        }

        /// <summary>
        /// Vendos vlerat default kur po behet modifikim
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNderViti"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestareModifiko(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf)
        {
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);         
            AspxWebControlUtils.vendosDateEditMask(dteAfatiKohor);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina);
            ConfigureAspxComboBox.ShtoKolonaPerKf(btneKlientFurnitori);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneKlientFurnitori);
            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btneMagazina, idPerdoruesi, false, 1,true);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbNjesiProdhimi);
            ConfigureAspxComboBox.mbushComboNjesiProdhimi(idNdermarrje, cmbNjesiProdhimi, false);
            ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatiPrintimit, 44, idNdermarrje);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 44, rm, cultinf, idGjuha);
            cmbKonfigurimi.TextFormatString = "{0}";
            int id = int.Parse(Request.QueryString["id"]);
            DbCore.DbProdhimi.clsKokaPlanifikim kok = new DbCore.DbProdhimi.clsKokaPlanifikim(id);

            if (kok != null)
            {
                MerrTedhenat(idGjuha, idPerdoruesi, idViti, idNdermarrje, kok);
            }   
        }

        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate();
                ruajRegjistrimPlanifikim(1);
            }
            if (e.Item.Name == "PrintPreview")
            {

                if (hfShtimModifikim.Value.ToString() == "modifikim")
                {
                    string id = Request.QueryString["id"];
                    DbCore.DbProdhimi.clsKokaPlanifikim clsKoka = new DbCore.DbProdhimi.clsKokaPlanifikim(Convert.ToInt32(id));
                    Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=formatPlanifikimProdhimi&idDokumenti=" + clsKoka.IdKokaPlanifikim + "&printo=false&raportdyte=jo&iddesign=" + cmbFormatiPrintimit.Value;
                }
                else
                {
                    Page.Validate();
                    ruajRegjistrimPlanifikim(1);
                }
            }

        }
        /// <summary>
        /// fshin dokumentin
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.DbProdhimi.clsKokaPlanifikim kokam = new DbCore.DbProdhimi.clsKokaPlanifikim(int.Parse(Request.QueryString["id"]));
            clsMesazh mesazh = new clsMesazh();
            bool lidhur = kokam.eshteILidhur();

            if (lidhur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDokLidhurNukFshihet", cultinf), pnlMesazhi);
                status1.Value = "false";
                return;
            }
            if (kokam.DtDok.Year != new DbCore.DbAdmin.clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                status1.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", cultinf), pnlMesazhi);
                return;
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(kokam.DtDok, idNdermarrje);
            //clsMesazh mesazhi = periudha.isPeriudheKycur();
            bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(kokam.DtDok, idNdermarrje);
            if (ekycur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPeriudhaEKycur", cultinf), pnlMesazhi);
                return;
            }

            kokam.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            mesazh = kokam.fshi();

            if (mesazh.Status)
            {

                Response.Redirect("Planifikimi.aspx?fshi=po");
                status1.Value = "true";

            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                status1.Value = "false";
                return;
            }
        }

        /// <summary>
        /// Ruan/Modifikon nje objekt clsKokaMagazina.
        /// </summary>
        /// <param name="statusDokumenti">Statusi me te cilin po ruhet dokumenti (I rregullt apo draft)</param>
        private void ruajRegjistrimPlanifikim(int statusDokumenti)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (Page.IsValid == false)
                return;
            if (!isValid(statusDokumenti, rm, ci))
                status1.Value = "false";
            DbCore.DbProdhimi.clsKokaPlanifikim koka = new DbCore.DbProdhimi.clsKokaPlanifikim();
            clsMesazh mesazh;
            try
            {
                if (btneKlientFurnitori.Text == "")
                {
                    koka = krijoRegjistrimPlanifikim(statusDokumenti, 0, "");
                }
                else
                {
                    int kf = Convert.ToInt32(btneKlientFurnitori.Value.ToString());
                    koka = krijoRegjistrimPlanifikim(statusDokumenti, kf, btneKlientFurnitori.Text.Split(' ')[0]);
                }
                if (koka.ColTrupi.Count == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgTrupiDokNukDuhetBosh", ci), pnlMesazhi, LoadingPanel);
                    status1.Value = "false";
                    return;
                }
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi, LoadingPanel);
                status1.Value = "false";
                return;
            }
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_Planifikim.aspx");

            if (hfShtimModifikim.Value == "shtim")
            {
                if ((statusDokumenti == 1 && !tedrejtaInfo.DShtim) || (statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                    status1.Value = "false";
                    return;
                }
                mesazh = koka.ruaj(hfNrAutoShitje, new DbCore.DbProdhimi.colUrdherPorosiPlanifikim());
            }
            else
            {
                if ((statusDokumenti == 1 && !tedrejtaInfo.DMod) || (statusDokumenti == 0 && !tedrejtaInfo.DModifikimDraft))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                    status1.Value = "false";
                    return;
                }
                koka.IdKokaPlanifikim = int.Parse(Request.QueryString["id"]);
                bool lidhur = koka.eshteILidhur();

                if (lidhur.ToString() != hfLidhur.Value)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDokumentiEshteILidhur", ci), pnlMesazhi, LoadingPanel);
                    status1.Value = "false";
                    return;
                }
                DbCore.DbProdhimi.colUrdherPorosiPlanifikim colurdher = new DbCore.DbProdhimi.colUrdherPorosiPlanifikim(koka.IdKokaPlanifikim);
                mesazh = koka.modifiko(lidhur, colurdher);
            }

            if (mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                hl = new HtmlTable();
                pnlLidhur.Update();
                status1.Value = "true";
                hfShtimModifikim.Value = "shtim";
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi, LoadingPanel);
                status1.Value = "false";
            }
        }

        /// <summary>
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <param name="klienti">Klienti</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina</returns>
        private DbCore.DbProdhimi.clsKokaPlanifikim krijoRegjistrimPlanifikim(int statusDokumenti, int klienti, string kodklienti)
        {
            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, null, ASPxSplitter1, null);

            hfNrAutoShitje = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");

            DbCore.DbProdhimi.clsKokaPlanifikim koka = new DbCore.DbProdhimi.clsKokaPlanifikim();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbShare.clsKonfigurimAmbjenti clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            clsKonf.mbushKonfigDefaultKomponentes(804, idNdermarrje);
            if (cmbKonfigurimi.Text != "") 
            {
                clsKonf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            }
            koka = krijoRegjistrimPlanifikim(statusDokumenti, klienti, kodklienti, clsKonf, ruajTrupinEPlanifikimit(true));

            return koka;
        }

        /// <summary>
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina ne rastin kur kemi hyrje
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <param name="klienti">Klienti</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina</returns>
        private DbCore.DbProdhimi.clsKokaPlanifikim krijoRegjistrimPlanifikim(int statusDokumenti, int idKlienti, string kodklienti, DbCore.DbShare.clsKonfigurimAmbjenti clsKonf, DbCore.DbProdhimi.colTrupiPlanifikim coltrupi)
        {
            DbCore.DbProdhimi.clsKokaPlanifikim mag = new DbCore.DbProdhimi.clsKokaPlanifikim();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            int idmagazina = 0, idgrup = 0, idgrup2 = 0, idgrup3 = 0;
            string magazina = "";
            if (cmbGrup1.Text != "")
                idgrup = int.Parse(cmbGrup1.Value.ToString());
            if (cmbGrup2.Text != "")
                idgrup2 = int.Parse(cmbGrup2.Value.ToString());
            if (cmbGrup3.Text != "")
                idgrup3 = int.Parse(cmbGrup3.Value.ToString());

            if (btneMagazina.Text != "")
            {
                idmagazina = int.Parse(btneMagazina.Value.ToString());
                magazina = btneMagazina.Text;
            }
            int idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int idFormatPrintimi = Convert.ToInt32(cmbFormatiPrintimit.Value);
            int idNjesiProdhimi = 0;
            string kodNjesiProdhimi = "";
            if (cmbNjesiProdhimi.Text != "")
            {
                kodNjesiProdhimi = cmbNjesiProdhimi.Text;
                if(cmbNjesiProdhimi.Value == null)
                    throw new DbCore.MyException("Njesia e prodhimit nuk ekziston!");
                bool sukses = int.TryParse(cmbNjesiProdhimi.Value.ToString(), out idNjesiProdhimi);
                if (!sukses)
                    throw new DbCore.MyException("Njesia e prodhimit nuk ekziston!");
            }

            clsMesazh mesazh = mag.krijoPlanifikim(clsKonf.IdNivel, clsKonf.IdKonfigAmbjente, idKlienti, kodklienti, idmagazina, magazina, dteDtDok.Date, txtNrDok.Text, statusDokumenti, idNdermarrje, idnderviti, idperdoruesi, dteDtRegjistrimi.Date, txtShenime.Text, idgrup, idgrup2, idgrup3, dteAfatiKohor.Date, coltrupi, idFormatPrintimi, idNjesiProdhimi, kodNjesiProdhimi);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
            return mag;
        }

        /// <summary>
        /// krijon koleksionin me trupin e dokumentit
        /// </summary>
        /// <param name="ruaj">ruaj boolean qe tregon ne duhen shtuar apo jo reshtat bosh ne ruajtje nuk duhen</param>
        /// <returns>coleksion me trupin e dokumentit</returns>
        private DbCore.DbProdhimi.colTrupiPlanifikim ruajTrupinEPlanifikimit(bool ruaj)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);
            DbCore.DbProdhimi.colTrupiPlanifikim trupat = new DbCore.DbProdhimi.colTrupiPlanifikim();
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            int idMagTemp = -1;
            bool isMagENjejte = true;
            for (int i = 0; i < dokumenti.Length; i++)
            {
                DbCore.DbProdhimi.clsTrupiPlanifikim trupMag = new DbCore.DbProdhimi.clsTrupiPlanifikim(idNdermarrje, idPerdoruesi, (Dictionary<string, object>)dokumenti[i]);
                
                if (ruaj)
                {
                    if (trupMag.IdArtikulli != 0) //ky kusht duhet pare kur te shtohen makrot
                    {
                        if (trupMag.IdMag == 0)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgMagazinaNukEkziston", cultinf), pnlMesazhi, LoadingPanel);
                            return new DbCore.DbProdhimi.colTrupiPlanifikim();
                        }
                        if (idMagTemp == -1)
                            idMagTemp = trupMag.IdMag;
                        else
                            if (isMagENjejte && trupMag.IdMag != idMagTemp)
                                isMagENjejte = false;

                        trupat.Add(trupMag);
                    }
                }
                else trupat.Add(trupMag);
            }
            if (ruaj)
            {
                btneMagazina.Text = "";

                if (isMagENjejte && trupat.Count > 0)
                   btneMagazina.Text = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiD(trupat[0].IdMag, idPerdoruesi);
            }
            return trupat;
        }

        /// <summary>
        /// Kontrollon nese jane plotesuar te gjitha fushat e detyrueshme te kokes se dokumentit
        /// </summary>
        private bool isValid(int draft, ResourceManager rm, CultureInfo ci)
        {
            bool isValid = true;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);

            if (dteDtRegjistrimi.Text == "")
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgListPagesaZgjidhniNjeDateRegjistrimi", ci), pnlMesazhi, LoadingPanel);

                return isValid;
            }
            if (dteAfatiKohor.Text == "")
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem vendosni Afatin kohor", pnlMesazhi, LoadingPanel);

                return isValid;
            }
            if (dteDtDok.Text == "")
            {
                isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLidhjaDokZgjidh1DateDokumenti", ci), pnlMesazhi, LoadingPanel);

                return isValid;
            }
            if (dteDtDok.Date.Year != new DbCore.DbAdmin.clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", ci), pnlMesazhi);
                return false;
            }
            DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            String mesazhGabimi;
            if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, dteDtDok.Date, periudha, draft))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                return false;
            }

            if (btneMagazina.Text != "")
            {
                clsNjesiAdministrative mag = new clsNjesiAdministrative(btneMagazina.Text, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                if (mag.IdNjesiAdministrative == -1)
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKjoMagazineNukEkziston", ci), pnlMesazhi, LoadingPanel);
                    return isValid;
                }
                else
                {
                    if (mag.Aktiv == false)
                    {
                        isValid = false;
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgMagazinaNukEshteAktive", ci), pnlMesazhi, LoadingPanel);
                        return isValid;
                    }
                }
            }
            return isValid;
        }


        /// <summary>
        /// Shtoi - Elsa. Combo klient/furnitorit me autocomplete
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void btneKlientFurnitori_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("btneKlientFurnitori"))
                {
                    int value = 0;
                    if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                        return;
                    ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), (ASPxComboBox)source, value);
                }
            }
        }

        /// <summary>
        /// Shtoi - Elsa. Combo klient/furnitorit me autocomplete
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void btneKlientFurnitori_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("btneKlientFurnitori"))
                {
                    ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), (ASPxComboBox)source, 1);
                }
            }
        }
    }
}