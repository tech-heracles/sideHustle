using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using DevExpress.Web;
using System.Collections.Generic;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using System.Web.Script.Serialization;
using DbCore;
using System.Globalization;
using System.Resources;
using DbCore.DbShare;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;

namespace PlatinumWeb
{
    public partial class Shto_RegjistrimRezervimi : MyPageBase
    {
        //private static string pershkrimDaljeFK = "Nga daljet e magazinës";
        //private static string pershkrimHyrjeFK = "Nga hyrjet e magazinës";
        //private static string STR_zgjidhniDtRegj = "Zgjidhni nje datë regjistrimi!";
        private enum Statusi
        {
            neproces = 0,
            ekzekutuar = 1,
            anulluar = 2
        }

        private colTrupiRezervime trupat = new colTrupiRezervime();
        public void MerrTedhenat(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, clsKokaRezervime koka, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            //DbCore.DbRegjistrim.clsNivelRegjistrimi niv = new clsNivelRegjistrimi();
            //niv.IdNivel = koka.IdNivel;
            string kodNiveli = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheKodNivelRegjistrimi(koka.IdNivel);
            cmbLloji.Text = kodNiveli;// niv.merrNivelRegjSipasId().Kodi;
            mbushComboKonfigurimet(true, rm, ci, idGjuha, idNdermarrje, idPerdoruesi); 
            cmbLloji.ClientSideEvents.Init = "function(s,e){TextChangedLloji();}";
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente, idGjuha);
            cmbKonfigurimi.Text = konf.KodKonfigAmbjente;
            
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;

            if (koka.IdKlientFurnitor != 0)
                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(btneKlientFurnitori, koka.IdKlientFurnitor);
            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 540, "btneKlientFurnitori", -1, false);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));
            if (koka.IdMagazina != 0)
                btneMagazina.Text = DbCore.DbRegjistrim.clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(koka.IdMagazina);

            DbCore.DbRegjistrim.clsKokaRezervime kokatra = new clsKokaRezervime();
            kokatra.mbushKokaRezervimiSipasIDGjenerues(koka.IdKokaRezervimi, 1, koka.IdKonfigAmbjente);

            cmbDegeAdministrative.Value = koka.IdDegeAdministrative.ToString();
            txtNrDok.Text = koka.NrDok;
            dteDtDok.Date = koka.DtDok;
            dteDtRegjistrimi.Date = koka.DtRegjistrimi;
            txtShenime.Text = koka.Shenime;
            mbushComboStatuse(cmbStatusi, koka.Statusi);
            DataTable dtlidhur = koka.merrIdsDokLidhur();
            hfLidhur.Value = (!(dtlidhur.Rows.Count == 0)).ToString();
            AspxWebControlUtils.ShtoLidhje(idPerdoruesi, idViti, idNdermarrje, hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, idGjuha);
            txtPrioriteti.Text = koka.Prioriteti.ToString();
            bool autorizimet = DbCore.DbRegjistrim.clsKokaRezervime.kaAutorizime(koka.IdKokaRezervimi, idPerdoruesi);
            hfAutorizimi.Value = autorizimet.ToString();
            if (!autorizimet)
                hfLidhur.Value = "True";
        }

   

        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idPerdoruesi;
            int idGjuha;
            int idNdermarrje;
            int idViti;
            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
                }
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
            }
            if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
            {
                Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
            }
            konfigGrid();

            if (DbCore.mySessionObjects.merrPeriudheKontabel(Session) != null)
            {
                DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
                btnPeriudha.Text = periudha.NrPeriudha.ToString();
                lblPeriudhaAktuale.Text = String.Format("{0}-{1}", periudha.FillimiPeriudha.ToShortDateString(), periudha.MbarimiPeriudha.ToShortDateString());
            }
            percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);

            if (!IsPostBack)
            {
                DbCore.DbAdmin.clsPerdorues per = new DbCore.DbAdmin.clsPerdorues(idPerdoruesi);
                hfHapurMbyllur.Value = per.InfoHapur.ToString();
                mbushHiddenFieldMePerkthime(cultinf, rm);
                if (hfShtimModifikim.Value == "")
                {
                    if (Request.QueryString["shtim_modifikim"] == "shtim")
                    {
                        hfShtimModifikim.Value = "shtim";
                        konfiguroVleraFillestareShto(idGjuha, idNdermarrje, rm, cultinf);
                    }
                    else
                    {
                        hfShtimModifikim.Value = "modifikim";
                        konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, rm, cultinf);
                    } percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);

                }
                else
                    if (hfShtimModifikim.Value == "shtim")
                        konfiguroVleraFillestareShto(idGjuha, idNdermarrje, rm, cultinf);
                    else
                        if (hfShtimModifikim.Value == "modifikim")
                            konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, rm, cultinf);

                DbCore.DbRegjistrim.colNjesiAdministrative colMagazinat = new DbCore.DbRegjistrim.colNjesiAdministrative();
                colMagazinat.mbushGjitheNjesiAdministrativeAktive(idNdermarrje, idPerdoruesi);
                JavaScriptSerializer serializusi = new JavaScriptSerializer();
                serializusi.MaxJsonLength = 50000000;
                hfTmpColMag.Value = serializusi.Serialize(colMagazinat);

                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Info Artikulli");
                hfTeDrejtaInfoArt.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "LupaArtikullShpejte.aspx");
                hfTeDrejtaArtRi.Value = tedrejtaInfo.DShtim.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.clsFunksione.GetKomponente(Page.Request));
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
            }
            Container.Attributes["src"] = "";
            GridUtil.perktheButonaGride(hfState, cultinf);
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            AspxWebControlUtils.perkthePopUp(popKonvertim, rm.GetString("labelKujdes", cultinf), lblKonvertoNe, rm.GetString("lblKonvertoNe", cultinf), ButtonOk2, rm.GetString("btnKonverto", cultinf));
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            lblKonfig.Text = rm.GetString("filterLlojiArtBurim", cultinf);
            hfState.Set("msgSasiaDuhetNumer", rm.GetString("msgSasiaDuhetNumer", cultinf));
            hfState.Set("msgArtikulliEshteInaktiv", rm.GetString("msgArtikulliEshteInaktiv", cultinf));
            hfState.Set("NukKryhenVeprimeMeArtikujProdhimOseProdhimNeProces", rm.GetString("NukKryhenVeprimeMeArtikujProdhimOseProdhimNeProces", cultinf));
            hfState.Set("msgNukKryhenVeprimeMeArtikujTePastokueshem", rm.GetString("msgNukKryhenVeprimeMeArtikujTePastokueshem", cultinf));
            hfState.Set("msgNukMundTeKryeniVeprimeMeArtikujTePerbere", rm.GetString("msgNukMundTeKryeniVeprimeMeArtikujTePerbere", cultinf));
            hfState.Set("msgNukMundTaZgjidhniArtikullinPerRezervim", rm.GetString("msgNukMundTaZgjidhniArtikullinPerRezervim", cultinf));
            hfState.Set("msgEkzistonArtikullNeGride", rm.GetString("msgEkzistonArtikullNeGride", cultinf));
            hfState.Set("msgGabimGjateTransferimitTeTeDhenave", rm.GetString("msgGabimGjateTransferimitTeTeDhenave", cultinf));
            hfState.Set("msgZgjidhDokumentin", rm.GetString("msgZgjidhDokumentin", cultinf));
            hfState.Set("msgZgjidhMagazinen", rm.GetString("msgZgjidhMagazinen", cultinf));
            hfState.Set("headerZgjidhKlientFurnitorin", rm.GetString("headerZgjidhKlientFurnitorin", cultinf));
            hfState.Set("msgKaRreshtaMeSasiZero", rm.GetString("msgKaRreshtaMeSasiZero", cultinf));
            hfState.Set("msgZgjidhniLlojinEVeprimit", rm.GetString("msgZgjidhniLlojinEVeprimit", cultinf));
            hfState.Set("roundPanelZgjidhArtikullin", rm.GetString("roundPanelZgjidhArtikullin", cultinf));
            hfState.Set("msgZgjidhMakro", rm.GetString("msgZgjidhMakro", cultinf));
            hfState.Set("msgLlojiVeprimitIPanjohur", rm.GetString("msgLlojiVeprimitIPanjohur", cultinf));
            hfState.Set("msgSasiaNukMundTeJeteZero", rm.GetString("msgSasiaNukMundTeJeteZero", cultinf));
            hfState.Set("msgSasiaNumer", rm.GetString("msgSasiaNumer", cultinf));
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", rm.GetString("msgPoTransferohetTeDhenatShtypniPerseriRuaj", cultinf));
            hfState.Set("msgZgjidhniLlojin", rm.GetString("msgZgjidhniLlojin", cultinf));
            hfState.Set("msgShenoniNumrinEDokumentit", rm.GetString("msgShenoniNumrinEDokumentit", cultinf));
            hfState.Set("msgZgjidhniNjeDateDokumenti", rm.GetString("msgZgjidhniNjeDateDokumenti", cultinf));
            hfState.Set("msgZgjidhniNjeDateRegjstrimi", rm.GetString("msgZgjidhniNjeDateRegjstrimi", cultinf));
            hfState.Set("msgJepniKF", rm.GetString("msgJepniKF", cultinf));
            hfState.Set("msgNukKeniAutorizimPerTeRuajturKeteDok", rm.GetString("msgNukKeniAutorizimPerTeRuajturKeteDok", cultinf));
            hfState.Set("msgNukKeniAutorizimPerTeFshireDok", rm.GetString("msgNukKeniAutorizimPerTeFshireDok", cultinf));
            hfState.Set("msgNukKeniAutorizimKonvertim", rm.GetString("msgNukKeniAutorizimKonvertim", cultinf));
            hfState.Set("msgTrupiDokumentitNukDuhetLeneBosh", rm.GetString("msgTrupiDokumentitNukDuhetLeneBosh", cultinf));
            hfState.Set("msgKaArtikujPaNjesi", rm.GetString("msgKaArtikujPaNjesi", cultinf));
            hfState.Set("msgZgjidhPeriudheKontabel", rm.GetString("msgZgjidhPeriudheKontabel", cultinf));
            hfState.Set("JQgridShtoArtikull", rm.GetString("JQgridShtoArtikull", cultinf));
            hfState.Set("msgDokNukMundTeKonvertohet", rm.GetString("msgDokNukMundTeKonvertohet", cultinf));
            hfState.Set("JQgridShtoArtikullAqt", rm.GetString("JQgridShtoArtikullAqt", cultinf));
            hfState.Set("MenuKokeDokumenti", rm.GetString("MenuKokeDokumenti", cultinf));
            hfState.Set("MenuTrupDokumenti", rm.GetString("MenuTrupDokumenti", cultinf));
            hfState.Set("MenuFundDokumenti", rm.GetString("MenuFundDokumenti", cultinf));

        }

        public void btnJo_Click(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
            if (pergjigja.Text == "fshi")
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("regjMagFshirjaPerfundoiMeSukses", cultinf), pnlMesazhi);
            else if (pergjigja.Text == "ruaj")
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses",cultinf), pnlMesazhi);
        }

        public void btnPo_Click(object sender, EventArgs e)
        {
            //trupat = DbCore.mySessionObjects.merrTrupatRezNgaSesioni(Session);
            //DbCore.clsMesazh mesazh = new DbCore.clsMesazh(true, "Rivleresimi përfundoi me sukses!");

            //foreach (DbCore.DbRegjistrim.clsTrupiRezervime t in trupat)
            //{

            //    DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(t.IdArtikulli);
            //    mesazh = DbCore.DbInventari.clsArtikulli.rivleresimCmimiMesatar(t.IdArtikulli, art.MetodeKostojeArtikulli, t.IdMag, t.Data, DateTime.Today); 
            //    if (!mesazh.StatusMesazhi)
            //    {
            //        if (pergjigja.Text == "fshi")
            //        Response.Redirect("RegjistrimMagazine.aspx?lloj=" + Request.QueryString["lloj"] + "&fshi=rivleresimjo");
            //        else Response.Redirect("RegjistrimMagazine.aspx?lloj=" + Request.QueryString["lloj"] + "&fshi=rivleresimruajjo");
            //    }
            //}


            //mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
            //if (mesazh.StatusMesazhi)
            //{   if (pergjigja.Text == "fshi")
            //    Response.Redirect("RegjistrimMagazine.aspx?lloj=" + Request.QueryString["lloj"] + "&fshi=rivleresimpo");
            //    else Response.Redirect("RegjistrimMagazine.aspx?lloj=" + Request.QueryString["lloj"] + "&fshi=rivleresimruajpo");
            //}
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idPerdoruesi, int idViti, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            DbCore.DbShare.colMenuItem menu = new DbCore.DbShare.colMenuItem(DbCore.mySessionObjects.ktheGjuhe(Session));
            menu.merrMenuItemSipasKomponentesRegjistrime(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.clsFunksione.GetKomponente(Page.Request), idPerdoruesi, idNdermarrje, idViti, hfShtimModifikim.Value == "modifikim" ? false : true);

            clsKokaRezervime kokmag = new clsKokaRezervime();
            if ((hfShtimModifikim.Value == "modifikim"))
            {
                int id = int.Parse(Request.QueryString["id"]);

                kokmag.mbushKokaRezervimiSipasID(id);
            }
            foreach (DbCore.DbShare.clsMenuItem m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                {
                    //if (!m.Enabled) continue;
                    clsToolbarConfig.ShtoMenuItem(this.Theme, aSPxMenu1, m);
                }

                if ((hfLidhur.Value == "True") && m.Name == "Draft")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    ruaj_draft.ClientEnabled = false;
                }
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "PrintPreview")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "Fshi")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;


                if (m.Name == "Ruaj")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;

                }

                if (m.Name == "RuajPrint")
                {
                    //if (cmbKonfigurimi.Text == "FHTK" && kokmag.IdStatusDok == 4 && (hfShtimModifikim.Value == "modifikim"))
                    //    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    //if (cmbKonfigurimi.Text == "FHTK" && kokmag.IdStatusDok == 0 && (hfShtimModifikim.Value == "modifikim"))
                    //    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].Text = "Konfirmo dhe printo";
                }
                if (m.Name == "ItemFrame")
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, DbCore.clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
            }
            EventHandler handlerPerPo = new EventHandler(btnPo_Click);
            EventHandler handlerPerJo = new EventHandler(btnJo_Click);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo, handlerPerPo, handlerPerJo);
            bool visible = clsFunksione.merrMenuVisibleDraft(hfShtimModifikim.Value == "modifikim" ? false : true, kokmag.IdStatusDok);
            ASPxMenu1.Items.FindByName("Draft").ClientVisible = visible;

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

        private void vendosDataDefault()
        {
            DateTime sot = new DateTime();
            sot = DateTime.Today;
            DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
                this.dteDtDok.Value = DateTime.Today;
            else
                dteDtDok.Value = periudha.FillimiPeriudha;
        }

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestareShto(int idGjuha, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {
            dteDtDok.Date = DateTime.Today;
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            dteDtRegjistrimi.Date = DateTime.Today;
            vendosDataDefault();
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina);
            ConfigureAspxComboBox.ShtoKolonaPerKf(btneKlientFurnitori);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneKlientFurnitori);
            mbushComboNivelesh(idNdermarrje, cmbLloji);
            if (DbCore.clsFunksione.isLlojHyrje(Request))
                cmbLloji.SelectedIndex = 0;
            else if (DbCore.clsFunksione.isLlojDalje(Request))
                cmbLloji.SelectedIndex = 1;
            mbushComboKonfigurimet(false, rm, ci, idGjuha, idNdermarrje, idPerdoruesi);
            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(idNdermarrje, cmbDegeAdministrative, false);
            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btneMagazina, idPerdoruesi, true,0,true);
            cmbLloji.ClientSideEvents.Init = "function(s,e){TextChangedLloji();}";
            mbushComboStatuse(cmbStatusi, 0);
            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 540, "btneKlientFurnitori", -1, true);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));
        }

        private void mbushComboNivelesh(int idNdermarrje, ASPxComboBox cmblloji)
        {
            //colNivelRegjistrimi col = new colNivelRegjistrimi();
            //col.mbushGjitheNivelRegjistrimiSipasKategoriMeKonvertime(78, idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            cmblloji.DataSource = DbCore.DbRegjistrim.colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(78, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), false); ;
            cmblloji.TextField = "Kodi";
            cmblloji.ValueField = "IdNivel";
            cmblloji.DataBind();
            cmblloji.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        private void mbushComboStatuse(ASPxComboBox cmbstatusi, int index)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            cmbstatusi.Items.Add(rm.GetString("cmbNeProces", cultinf));
            cmbstatusi.Items.Add(rm.GetString("cmbEkzekutuar", cultinf));
            cmbstatusi.Items.Add(rm.GetString("cmbAnulluar", cultinf));
            cmbStatusi.SelectedIndex = index;
            cmbstatusi.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            //if ((hfShtimModifikim.Value == "modifikim")) cmbstatusi.ClientEnabled = true;
            //else cmbstatusi.ClientEnabled = false;
        }

        /// <summary>
        /// Vendos vlerat default kur po behet modifikim
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNderViti"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestareModifiko(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci)
        {//mbush kombot dhe gridat
            // txtNrDok.Enabled = false;

            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina);
            ConfigureAspxComboBox.ShtoKolonaPerKf(btneKlientFurnitori);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneKlientFurnitori);

            mbushComboNivelesh(idNdermarrje, cmbLloji);
            cmbLloji.ClientSideEvents.Init = "function(s,e){TextChangedLloji();}";

            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(idNdermarrje, cmbDegeAdministrative, false);

            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btneMagazina, idPerdoruesi, false,0,true);

            int id = int.Parse(Request.QueryString["id"]);
            clsKokaRezervime kok = new clsKokaRezervime();
            kok.IdKokaRezervimi = id;
            kok.mbushKokaRezervimiSipasID(kok.IdKokaRezervimi);
            if (kok != null)
            {
                MerrTedhenat(idGjuha, idPerdoruesi, idViti, idNdermarrje, kok, rm, ci);

            }

            mbushListeRegjistrimTrupiModifiko(idPerdoruesi, idNdermarrje, kok);
        }

        /// <summary>
        /// Merr trupin ekzistues te dokumentit te magazines qe po modifikohet
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        private void mbushListeRegjistrimTrupiModifiko(int idPerdoruesi, int idNdermarrje, clsKokaRezervime koka)
        {//mbush griden me te dhenat
            koka.mbushTrupRezervime();
            mbushHiddenFieldet(idPerdoruesi, idNdermarrje, koka.OcolTrupiRezervime, koka.IdKonfigAmbjente, koka.IdKokaRezervimi, koka.IdKonfigAmbjente); ;
        }

        private void mbushHiddenFieldet(int idPerdoruesi, int idNdermarrje, colTrupiRezervime col, int lloji, int idkokamagazina, int idkonfig)
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            serializusi.MaxJsonLength = 50000000;
            HfColTrupMag.Value = serializusi.Serialize(col);
            DbCore.DbInventari.colArtikujt colArtikuj = col.ktheColArtikuj();
            HfColArt.Value = serializusi.Serialize(colArtikuj);
            HfColNjesAdminis.Value = serializusi.Serialize(col.ktheColMag(idPerdoruesi));
            HfColNjesiArt.Value = serializusi.Serialize(col.ktheColNjesiArt());

            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti(lloji);
            HfKonfAmb.Value = serializusi.Serialize(konf);
        }

        private void konfigGrid()
        {
            string emriKomponentes = DbCore.clsFunksione.GetKomponente(Page.Request);
            DbCore.DbAdmin.clsKomponente oKomponente = new DbCore.DbAdmin.clsKomponente(emriKomponentes);
            DbCore.DbShare.clsKonfigurimAmbjenti konfigurimi = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfigurimi.mbushKonfigDefaultKomponentes(oKomponente.IdKomponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.colGridaTrupi trupiGrides = new DbCore.DbAdmin.colGridaTrupi(oKomponente.IdKomponente, konfigurimi.IdKonfigAmbjente, (int)hfState["idGjuha"]);
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            HfGridCol.Value = serializusi.Serialize(trupiGrides);
        }


        private void mbushComboKonfigurimet(bool mod, System.Resources.ResourceManager rm, System.Globalization.CultureInfo ci, int idGjuha, int idNdermarje, int idPerdoruesi)
        {
            DbCore.DbShare.colKonfigurimAmbjenti colKonfig = new DbCore.DbShare.colKonfigurimAmbjenti();
            int idKategori = 78;//rezervime
            if (cmbLloji.Value != null)
            {
                int idNivel = int.Parse(cmbLloji.Value.ToString());
                colKonfig.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(idKategori, idNivel, idPerdoruesi);
            }
            else
                colKonfig.mbushKonfigAmbjSipasIdKategori(idKategori, idNdermarje, idPerdoruesi, idGjuha);
            cmbKonfigurimi.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
            DbCore.DbShare.colKonfigurimAmbjenti konfVarura = new DbCore.DbShare.colKonfigurimAmbjenti();
            //foreach (DbCore.DbShare.clsKonfigurimAmbjenti konfi in colKonfig)
            //{
            //    DbCore.DbShare.colKusht kushte = new DbCore.DbShare.colKusht();
            //    DbCore.DbShare.clsKusht kusht = new DbCore.DbShare.clsKusht();
            //    kusht.IdKonfigurimAmbjente = konfi.IdKonfigAmbjente;
            //    kushte.mbushGjitheKushteKonfigurimi(kusht.IdKonfigurimAmbjente);
            //    foreach (DbCore.DbShare.clsKusht k in kushte)
            //    {
            //       if (k.Kodi == "V")
            //       if (k.Vlera == 43 && !mod)
            //        konfVarura.Add(konfi);

            //    }
            //}
            foreach (DbCore.DbShare.clsKonfigurimAmbjenti konfi in konfVarura)
            {
                colKonfig.Remove(konfi);
            }
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodKonfigAmbjente";
            colprove.Caption = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimKonfigAmbjente";
            colemer.Caption = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci); colemer.Width = 300;
            cmbKonfigurimi.TextFormatString = "{0}";
            cmbKonfigurimi.Columns.Add(colprove);
            cmbKonfigurimi.Columns.Add(colemer); cmbKonfigurimi.DataSource = colKonfig;
            cmbKonfigurimi.ValueField = "IdKonfigAmbjente";
            cmbKonfigurimi.DataBind();
            cmbKonfigurimi.SelectedIndex = 0;
            hfKonffillestar.Value = cmbKonfigurimi.SelectedItem.Text;
        }


        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, DevExpress.Web.MenuItemEventArgs e)
        {
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate();
                ruajRegjistrimRezervimi(1, false);
                mbushHiddenFieldet(idPerdoruesi, idNdermarrje, new colTrupiRezervime(), 1, 0, 0);
            }
            if (e.Item.Name == "Draft")
            {
                Page.Validate();
                ruajRegjistrimRezervimi(0, false);// 0 = statusi i dokumentit(ne kete rast statusi eshte draft)
                mbushHiddenFieldet(idPerdoruesi, idNdermarrje, new colTrupiRezervime(), 1, 0, 0);
            }
            if (e.Item.Name == "PrintPreview")
            {

                if (hfShtimModifikim.Value.ToString() == "modifikim")
                {
                    string id = Request.QueryString["id"];
                    DbCore.DbRegjistrim.clsKokaRezervime clsKoka = new DbCore.DbRegjistrim.clsKokaRezervime();
                    clsKoka.mbushKokaRezervimiSipasID(int.Parse(id));
                    DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                    konf.mbushKonfiguriminMeID(clsKoka.IdKonfigAmbjente);
                    Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=Rap_Format_Printimi_Magazina&idDokumenti=" + clsKoka.IdKokaRezervimi + "&printo=false";
                }
                else
                {
                    Page.Validate();
                    ruajRegjistrimRezervimi(1, false);
                    mbushHiddenFieldet(idPerdoruesi, idNdermarrje, new colTrupiRezervime(), 1, 0, 0);
                }
            }
            if (e.Item.Name == "RuajPrint")
            {
                Page.Validate();
                ruajRegjistrimRezervimi(1, true);
                mbushHiddenFieldet(idPerdoruesi, idNdermarrje, new colTrupiRezervime(), 1, 0, 0);

            }
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            pergjigja.Text = "";
            DbCore.DbRegjistrim.colTrupiRezervime tr = new DbCore.DbRegjistrim.colTrupiRezervime();

            clsKokaRezervime kokam = new clsKokaRezervime();
            kokam.IdKokaRezervimi = int.Parse(Request.QueryString["id"]);
            kokam.mbushKokaRezervimiSipasID(kokam.IdKokaRezervimi);
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            bool lidhur = kokam.eshteILidhur();
            string ngjyra = clsKokaRezervime.merrNgjyreKonvertimeRezervime(kokam.IdNdermarrje, kokam.IdKokaRezervimi);
            if (ngjyra == "kuqe" || ngjyra == "gjelber" || ngjyra == "verdhe")
                lidhur = true;

            if (lidhur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDokLidhurNukFshihet", cultinf), pnlMesazhi);
                this.status1.Value = "false";
                return;
            }
            if (kokam.DtDok.Year != new DbCore.DbAdmin.clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                this.status1.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", cultinf), pnlMesazhi);
                return;
            }
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(kokam.DtDok, idNdermarrje);
            //DbCore.clsMesazh mesazhi = periudha.isPeriudheKycur();
            bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(kokam.DtDok, idNdermarrje);
            if (ekycur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPeriudhaEKycur", cultinf), pnlMesazhi);
                return;
            }
            kokam.mbushTrupRezervime();
            //mesazh = kokam.kontrolloGjendjeNeFshirje(new DbCore.DbRegjistrim.colTrupiRezervime(), 0);
            //if (!mesazh.StatusMesazhi)
            //{
            //    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            //    return;
            //}
            kokam.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            mesazh = kokam.fshi();
            DbCore.mySessionObjects.ruajTrupatRezNeSession(Session, trupat);
            if (mesazh.Status)
            {
                Response.Redirect("RegjistrimRezervimi.aspx?lloj=" + Request.QueryString["lloj"] + "&fshi=po");
                this.status1.Value = "true";
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                this.status1.Value = "false";
                return;
            }


        }

        /// <summary>
        /// Therritet kur klikohet butoni Ruaj per te ruajtur nje dokument magazine te forme te rregullt (jo draft)
        /// Therret funksionin <see cref="ruajRegjistrimMagazine(1)"/>
        /// </summary>
        protected void ruaj_Button_Click(object sender, EventArgs e)
        {
            Page.Validate();
            ruajRegjistrimRezervimi(1, false);
            mbushHiddenFieldet((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], new colTrupiRezervime(), 1, 0, 0);
        }

        /// <summary>
        /// Therritet kur klikohet butoni Ruaj si draft per te ruajtur nje dokument magazine si draft
        /// Therret funksionin <see cref="ruajRegjistrimMagazine(0)"/>
        /// </summary>
        protected void ruaj_draft_Click(object sender, EventArgs e)
        {
            Page.Validate();
            ruajRegjistrimRezervimi(0, false);// 0 = statusi i dokumentit(ne kete rast statusi eshte draft)
            mbushHiddenFieldet((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], new colTrupiRezervime(), 1, 0, 0);
        }

        /// <summary>
        /// Therritet kur klikohet butoni Anullo. Dergon perdoruesin te lista e dokumentave te magazines.
        /// </summary>
        protected void anullo_Button_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegjistrimRezervimi.aspx?lloj=" + Request.QueryString["lloj"]);
        }

        /// <summary>
        /// Ruan/Modifikon nje objekt clsKokaMagazina.
        /// Therret funksionin <see cref="krijoRegjistrimMagazine"/>
        /// </summary>
        /// <param name="statusDokumenti">Statusi me te cilin po ruhet dokumenti (I rregullt apo draft)</param>
        private void ruajRegjistrimRezervimi(int statusDokumenti, bool printo)
        {
            colKokaRezervime regjistrime = new colKokaRezervime();
            if (Page.IsValid == false)
                return;
            string shfaqmesazhapolupe = "jo";
            string mesazhinformues = "";
            colTrupiRezervime tr = new colTrupiRezervime();
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (isValidRegjistrimRezervimi(statusDokumenti, rm, ci))
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                if (btneKlientFurnitori.Text == "") // nese nuk ka klient furnitor 
                {
                    regjistrime.Add(krijoRegjistrimRezervimi(statusDokumenti, 0, "", out mesazhinformues));
                }
                else
                {
                    clsKlientFurnitor kf = new clsKlientFurnitor(Convert.ToInt32(btneKlientFurnitori.Value));
                    if (kf.IdKlientFurnitor <= 0)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKlientFurnitoriNukEkziston", ci), pnlMesazhi);
                        status1.Value = "false";
                        return;
                    }

                    if (!kf.AktivKF)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKlientFurnitoriNukEshteAktiv", ci), pnlMesazhi);
                        status1.Value = "false";
                        return;
                    }

                    regjistrime.Add(krijoRegjistrimRezervimi(statusDokumenti, kf.IdKlientFurnitor, kf.KodKlientFurnitor, out mesazhinformues));
                }

                int idPeriudheZgjedhur = DbCore.mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha;
                foreach (clsKokaRezervime regjistrim in regjistrime)
                {
                    if (regjistrim.OcolTrupiRezervime.Count == 0)
                    {
                        status1.Value = "false";
                        return;
                    }

                    if (mesazhinformues != "")
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhinformues, pnlMesazhi);
                        status1.Value = "false";
                        return;
                    }

                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.clsFunksione.GetKomponente(Page.Request));

                    if (hfShtimModifikim.Value == "shtim")
                    {
                        if ((statusDokumenti == 1 && !tedrejtaInfo.DShtim) || (statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft))
                        //if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta"), pnlMesazhi);
                            status1.Value = "false";
                            return;
                        }
                        mesazh = regjistrim.ruaj(hfNrAutoShitje, idPeriudheZgjedhur);
                    }
                    else if (hfShtimModifikim.Value == "modifikim")
                    {
                        if ((statusDokumenti == 1 && !tedrejtaInfo.DMod) || (statusDokumenti == 0 && !tedrejtaInfo.DModifikimDraft))
                        //if (!tedrejtaInfo.DMod)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta"), pnlMesazhi);
                            status1.Value = "false";
                            return;
                        }

                        regjistrim.IdKokaRezervimi = int.Parse(Request.QueryString["id"]);

                        bool lidhur = regjistrim.eshteILidhur();

                        bool transferim = false;
                        if (regjistrim.Statusi == Convert.ToInt16(Statusi.neproces)) 
                            transferim = false;
                        else 
                            transferim = true;

                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = rm.GetString("msgDokumentiEshteILidhur", ci);
                            status1.Value = "false";
                        }
                        else
                        {
                            mesazh = regjistrim.modifiko(lidhur, transferim, out shfaqmesazhapolupe);
                        }
                    }

                    DbCore.mySessionObjects.ruajTrupatRezNeSession(Session, trupat);
                    pergjigja.Text = "ruaj";
                    if (mesazh.Status == true)
                    {

                        hfShtimModifikim.Value = "shtim"; percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idNdermarrje, ASPxMenu1);
                        if (printo)
                            Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=Rap_Format_Printimi_Magazina&idDokumenti=" + regjistrim.IdKokaRezervimi + "&printo=true";//te shtohet raporti per RH

                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci), pnlMesazhi);

                        hl = new HtmlTable();
                        pnlLidhur.Update();
                        status1.Value = "true";
                    }
                    else
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi, LoadingPanel);
                        status1.Value = "false";
                    }
                }
            }
            else
            {
                status1.Value = "false";
            }
            pergjigja.ClientVisible = false;
        }

        /// <summary>
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsKokaRezervime
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <param name="klienti">Klienti</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaRezervime</returns>
        private clsKokaRezervime krijoRegjistrimRezervimi(int statusDokumenti, int klienti, string kodklienti, out string mesazhinformues)
        {
            mesazhinformues = "";

            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, null, ASPxSplitter1, null);

            hfNrAutoShitje = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");

            int idnivel = int.Parse(cmbLloji.Value.ToString());
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbShare.clsKonfigurimAmbjenti clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            clsKokaRezervime koka = new clsKokaRezervime();

            if (cmbLloji.Text == "RH")
            {
                koka.Lloji = 1;
                clsKonf.mbushKonfigDefaultKomponentes(539, idNdermarrje);
            }

            if (cmbKonfigurimi.Text != "")
            {
                clsKonf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            }
            clsKokaRezervime rezdaljeekzistuese = new clsKokaRezervime();
            if (hfShtimModifikim.Value == "modifikim")
                rezdaljeekzistuese.mbushKokaRezervimiSipasIDGjenerues(int.Parse(Request.QueryString["id"]), 2, clsKonf.IdKonfigAmbjente);

            if (koka.Lloji == 1 && hfShtimModifikim.Value == "shtim")//RH
            {
                koka = krijoRegjistrimRezervimi(statusDokumenti, klienti, kodklienti, 1, clsKonf, idnivel, Convert.ToInt16(Statusi.neproces), ruajTrupinERezervimit(1, Convert.ToInt16(Statusi.neproces), 0), new clsKokaRezervime(), out mesazhinformues);
            }
            else if (koka.Lloji == 1 && hfShtimModifikim.Value == "modifikim" && cmbStatusi.SelectedIndex == Convert.ToInt16(Statusi.ekzekutuar))
            { //gjenerohet RD
                //DbCore.DbRegjistrim.clsNivelRegjistrimi niv = new clsNivelRegjistrimi() { Kodi = "RD", IdNdermarje = idNdermarrje };
                //niv.merrNivelRegjSipasKodi();
                int idNivelGjen = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("RD", idNdermarrje);
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasKod("RD", idNdermarrje);
                int statusdoktransf = 1;

                koka = krijoRegjistrimRezervimi(statusDokumenti, klienti, kodklienti, 1, clsKonf, idnivel, Convert.ToInt16(Statusi.ekzekutuar), ruajTrupinERezervimit(1, Convert.ToInt16(Statusi.neproces), 0), krijoRegjistrimRezervimi(statusdoktransf, klienti, kodklienti, 2, konf, idNivelGjen, Convert.ToInt16(Statusi.ekzekutuar), ruajTrupinERezervimit(-1, Convert.ToInt16(Statusi.ekzekutuar), rezdaljeekzistuese.IdKokaRezervimi), new clsKokaRezervime(), out mesazhinformues), out mesazhinformues);
            }
            else if (koka.Lloji == 1 && hfShtimModifikim.Value == "modifikim" && cmbStatusi.SelectedIndex == Convert.ToInt16(Statusi.anulluar))
            {
                //gjenerohet RA
                //DbCore.DbRegjistrim.clsNivelRegjistrimi niv = new clsNivelRegjistrimi() { Kodi = "RA", IdNdermarje = idNdermarrje };
                //niv.merrNivelRegjSipasKodi();
                int idNivelGjen = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("RA", idNdermarrje);
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasKod("RA", idNdermarrje);
                int statusdoktransf = 1;

                koka = krijoRegjistrimRezervimi(statusDokumenti, klienti, kodklienti, 1, clsKonf, idnivel, Convert.ToInt16(Statusi.anulluar), ruajTrupinERezervimit(1, Convert.ToInt16(Statusi.neproces), 0), krijoRegjistrimRezervimi(statusdoktransf, klienti, kodklienti, 2, konf, idNivelGjen, Convert.ToInt16(Statusi.anulluar), ruajTrupinERezervimit(-1, Convert.ToInt16(Statusi.anulluar), rezdaljeekzistuese.IdKokaRezervimi), new clsKokaRezervime(), out mesazhinformues), out mesazhinformues);

            }
            else if (koka.Lloji == 1 && hfShtimModifikim.Value == "modifikim" && cmbStatusi.SelectedIndex == Convert.ToInt16(Statusi.neproces))
            {
                koka = krijoRegjistrimRezervimi(statusDokumenti, klienti, kodklienti, 1, clsKonf, idnivel, Convert.ToInt16(Statusi.neproces), ruajTrupinERezervimit(1, Convert.ToInt16(Statusi.neproces), 0), new clsKokaRezervime(), out mesazhinformues);

            }

            else //RD direkt nga ambjenti
            {
                koka = krijoRegjistrimRezervimi(statusDokumenti, klienti, kodklienti, 2, clsKonf, idnivel, Convert.ToInt16(Statusi.ekzekutuar), ruajTrupinERezervimit(-1, Convert.ToInt16(Statusi.neproces), 0), new clsKokaRezervime(), out mesazhinformues);
            }

            return koka;
        }


        private clsKokaRezervime krijoRegjistrimRezervimi(int statusDokumenti, int idKlienti, string kodklienti, int idllojdok, DbCore.DbShare.clsKonfigurimAmbjenti clsKonf, int idnivel, int status, colTrupiRezervime coltrupi, clsKokaRezervime rezGjen, out string mesazhinformues)
        {
            clsKokaRezervime mag = new clsKokaRezervime();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            int idmagazina = 0;
            string magazina = "";
            mesazhinformues = "";
            if (btneMagazina.Text != "")
            {
                idmagazina = int.Parse(btneMagazina.Value.ToString());
                magazina = btneMagazina.Text;
            }

            int idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int iddege;
            if (cmbDegeAdministrative.Text != "")
                iddege = int.Parse(cmbDegeAdministrative.Value.ToString());
            else
                iddege = 0;
            int prioritet;
            if (txtPrioriteti.Text != "")
                prioritet = int.Parse(txtPrioriteti.Text);
            else
                prioritet = 0;

            mag.krijoRezervim(idnivel, clsKonf.IdKonfigAmbjente, idKlienti, kodklienti, idmagazina, magazina, dteDtDok.Date, txtNrDok.Text, idllojdok, statusDokumenti, idNdermarrje, idnderviti, idperdoruesi, dteDtRegjistrimi.Date, txtShenime.Text, iddege, cmbDegeAdministrative.Text, prioritet, status, 0, coltrupi, rezGjen, out mesazhinformues);


            return mag;
        }


        private colTrupiRezervime ruajTrupinERezervimit(int shenja, int lloji, int iddokekzistuesdalje)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);
            colTrupiRezervime trupat = new colTrupiRezervime();
            int idMagTemp = -1;
            bool isMagENjejte = true;
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);

            for (int i = 0; i < dokumenti.Length; i++)
            {
                clsTrupiRezervime trupMag = new clsTrupiRezervime(idNdermarrje, idPerdoruesi, dteDtDok.Date, (Dictionary<string, object>)dokumenti[i], lloji, iddokekzistuesdalje);
                if (trupMag.IdArtikulli > 0)
                {
                    if (trupMag.IdMag == -1)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgMagazinaNukEkziston", cultinf), pnlMesazhi, LoadingPanel);
                        return new colTrupiRezervime();
                    }
                    if (idMagTemp == -1)
                        idMagTemp = trupMag.IdMag;
                    else
                        if (isMagENjejte && trupMag.IdMag != idMagTemp)
                            isMagENjejte = false;
                    trupMag.Shenja = shenja;
                    trupat.Add(trupMag);
                }
            }

            btneMagazina.Text = "";

            if (isMagENjejte && trupat.Count > 0)
            {
                //clsNjesiAdministrative magazinaPerbashket = new clsNjesiAdministrative(trupat[0].IdMag, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));                
                btneMagazina.Text = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiD(trupat[0].IdMag, idPerdoruesi);
            }
            return trupat;
        }


        /// <summary>
        /// Kontrollon nese jane plotesuar te gjitha fushat e detyrueshme te kokes se dokumentit
        /// </summary>
        private bool isValidRegjistrimRezervimi(int draft, ResourceManager rm, CultureInfo ci)
        {
            bool isValid = true;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);


            if (this.dteDtRegjistrimi.Text == "")
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLidhjaDokZgjidh1DateRegjistrimi", ci), pnlMesazhi, LoadingPanel);
                return isValid;
            }
            if (this.dteDtDok.Text == "")
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
            if (!DbCore.clsFunksione.checkPeriudheKontabel(out mesazhGabimi, dteDtDok.Date, periudha, draft))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                return false;
            }

            if (btneMagazina.Text != "")
            {
                DbCore.DbRegjistrim.clsNjesiAdministrative mag = new clsNjesiAdministrative(btneMagazina.Text, idNdermarrje, idPerdorues);
                if (mag.IdNjesiAdministrative == -1)
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKjoMagazineNukEkziston", ci), pnlMesazhi, LoadingPanel);

                    return isValid;
                }
                else
                {
                    mag = new clsNjesiAdministrative(btneMagazina.Text, idNdermarrje, idPerdorues);

                    if (mag.Aktiv == false)
                    {
                        isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgMagazinaNukEshteAktive", ci), pnlMesazhi, LoadingPanel);

                        return isValid;
                    }
                }
            }
            if (this.cmbDegeAdministrative.Text != "")
            {
                DbCore.DbRegjistrim.clsDegeAdministrative dege = new clsDegeAdministrative(cmbDegeAdministrative.Text, idNdermarrje);
                if (dege.IdDegeAdministrative == -1)
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKjoDegeAdministrativeNukEkziston", ci), pnlMesazhi);
                    return isValid;
                }
                else
                {
                    dege = new clsDegeAdministrative(cmbDegeAdministrative.Text, idNdermarrje);

                    if (dege.Aktiv == false)
                    {
                        isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKjoDegeAdministrativeNukEshteAktive", ci), pnlMesazhi);

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
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneKlientFurnitori"))
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
                if (Request.Params["__CALLBACKID"].ToString().Contains("btneKlientFurnitori"))
                {
                    ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, (ASPxComboBox)source, cmbLloji.Text.Split(';')[0], DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheGjuhe(Session),0);
                }
            }
        }


    }
}