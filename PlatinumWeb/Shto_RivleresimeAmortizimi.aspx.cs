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
using DbCore.DbInventari;
using DbCore.DbShare;
using System.Globalization;
using System.Resources;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_RivleresimeAmortizimi : MyPageBase
    {

        public void MerrTedhenat(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, DbCore.DbAsete.clsAmortizimiKoka koka, ResourceManager rm, CultureInfo cultinf)
        {
            string kodNiveli = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheKodNivelRegjistrimi(koka.IdNiveli);
            cmbLloji.Value = koka.IdNiveli.ToString();
            mbushComboKonfigurimet(true, idGjuha, rm, cultinf);
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(koka.IdKonfigurimAmbjenti, idGjuha);
            cmbKonfigurimi.Text = konf.KodKonfigAmbjente;
            if (clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "AR") == "Po")
                koka.ColTrupi = new DbCore.DbAsete.colAmortizimiTrupiRezerva();
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 1007, "", -1, false);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));

            cmbLlogariKunderParti.Text = new clsLlogari(koka.IdLlogKunderparti).NrLlogari;

            if (koka.IdNjesiAdministrative != 0)
            {
                clsNjesiAdministrative njesi = new clsNjesiAdministrative(koka.IdNjesiAdministrative);
                string kodiMag = njesi.Kodi + " (" + njesi.Pershkrimi + ")";
                btneMagazina.SelectedItem = btneMagazina.Items.FindByText(kodiMag);
                //btneMagazina.Value = njesi.IdNjesiAdministrative.ToString();
                //btneMagazina.Text = njesi.Kodi;
                cmbDegeAdministrative.Value = njesi.IdDegeAdministrative.ToString();
            }
            txtNrDok.Text = koka.NrDok;
            dteDtDok.Date = koka.DateDokumenti;
            dteDtRegjistrimi.Date = koka.DateRegjistrimi;
            txtShenime.Text = koka.Shenime;
            cmbStandarti.Value = koka.IdLlojStandarti.ToString();
            DataTable dtlidhur = koka.merrIdsDokLidhur();
            hfLidhur.Value = (!(dtlidhur.Rows.Count == 0)).ToString();
            hfAutorizimi.Value = "True";

            AspxWebControlUtils.ShtoLidhje(idPerdoruesi, idViti, idNdermarrje, hl, dtlidhur, koka.IdDokGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, idGjuha);
        }

   
        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idNdermarrje;
            int idViti;
            int idGjuha;
            bool eshteOwn;
            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session, true, "FaqePaautorizuar");
                }
                idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                if (DbCore.mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                    return;
                }
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                eshteOwn = DbCore.mySessionObjects.merrEshteOwnSesioni(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);

                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idGjuha", idGjuha);
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idGjuha = (int)hfState["idGjuha"];
            }
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1, rm, cultinf);
            if (!IsPostBack)
            {
                mbushHiddenFieldMePerkthime(cultinf, rm);
                DbCore.DbAdmin.clsPerdorues per = new DbCore.DbAdmin.clsPerdorues(idPerdoruesi);
                if (hfShtimModifikim.Value == "")
                {
                    if (Request.QueryString["shtim_modifikim"] == "shtim")
                    {
                        hfShtimModifikim.Value = "shtim";
                        konfiguroVleraFillestareShto(idGjuha, idNdermarrje, rm, cultinf);
                    }
                    else if (Request.QueryString["shtim_modifikim"] == "klonim")
                    {
                        hfShtimModifikim.Value = "klonim";
                        konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, rm, cultinf);
                    }
                    else
                    {
                        hfShtimModifikim.Value = "modifikim";
                        konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, rm, cultinf);
                    }
                    percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1, rm, cultinf);
                }
                else
                    if (hfShtimModifikim.Value == "shtim")
                    konfiguroVleraFillestareShto(idGjuha, idNdermarrje, rm, cultinf);
                else
                        if (hfShtimModifikim.Value == "modifikim")
                    konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, rm, cultinf);
                DbCore.DbRegjistrim.colNjesiAdministrative colMagazinat = new DbCore.DbRegjistrim.colNjesiAdministrative();
                colMagazinat.mbushGjitheNjesiAdministrativeAktiveSipasLlojit(idNdermarrje, idPerdoruesi, 2);//per afatgjate
                JavaScriptSerializer serializusi = new JavaScriptSerializer();
                serializusi.MaxJsonLength = 50000000;
                hfTmpColMag.Value = serializusi.Serialize(colMagazinat);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, DbCore.clsFunksione.GetKomponente(Page.Request));
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
            }
            AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
            //DbCore.clsFunksione.perkthePopUp(popMesazhQK, rm.GetString("labelKujdes", cultinf), lblMsgbox4, rm.GetString("msgDeshironiTeBeniShperndarjenNeQendratEKostos", cultinf), ButtonCancelQK, rm.GetString("cmbboxItemFilterAvancJo", cultinf), ButtonOkQK, rm.GetString("cmbboxItemFilterAvancPo", cultinf));
            Container.Attributes["src"] = "";
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            GridUtil.perktheButonaGride(hfState, cultinf);
            konfigurimi_Label.Text = rm.GetString("lblModeli", cultinf);
            popupUniversal.HeaderText = rm.GetString("headerPopUpText", cultinf);
            hfState.Set("msgTrupiDokumentitNukDuhetLeneBosh", rm.GetString("msgTrupiDokumentitNukDuhetLeneBosh", cultinf));
            hfState.Set("msgNukKeniAutorizimPerTeFshireDok", rm.GetString("msgNukKeniAutorizimPerTeFshireDok", cultinf));
            hfState.Set("msgNukKeniAutorizimKlonim", rm.GetString("msgNukKeniAutorizimKlonim", cultinf));
            hfState.Set("msgNukKeniAutorizimPerTeRuajturKeteDok", rm.GetString("msgNukKeniAutorizimPerTeRuajturKeteDok", cultinf));
            hfState.Set("msgZgjidhniNjeDateRegjstrimi", rm.GetString("msgZgjidhniNjeDateRegjstrimi", cultinf));
            hfState.Set("msgZgjidhniNjeDateDokumenti", rm.GetString("msgZgjidhniNjeDateDokumenti", cultinf));
            hfState.Set("msgShenoniLlogarine", rm.GetString("msgShenoniLlogarine", cultinf));
            hfState.Set("msgShenoniNumrinEDokumentit", rm.GetString("msgShenoniNumrinEDokumentit", cultinf));
            hfState.Set("msgZgjidhniLlojin", rm.GetString("msgZgjidhniLlojin", cultinf));
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", rm.GetString("msgPoTransferohetTeDhenatShtypniPerseriRuaj", cultinf));
            hfState.Set("msgRivleresimAmortVjetorDuhetIBarabarteMeAmortGjithsej", rm.GetString("msgRivleresimAmortVjetorDuhetIBarabarteMeAmortGjithsej", cultinf));
            hfState.Set("msgRivleresimAmortVjetorNukDuhetMeIMadhSeAmortGjithsej", rm.GetString("msgRivleresimAmortVjetorNukDuhetMeIMadhSeAmortGjithsej", cultinf));
            hfState.Set("msgRivleresimAmortVjetorNukDuhetMeIMadhSeGjendja", rm.GetString("msgRivleresimAmortVjetorNukDuhetMeIMadhSeGjendja", cultinf));
            hfState.Set("msgRivleresimAmortGjithsejNukDuhetMeIMadhSeGjendja", rm.GetString("msgRivleresimAmortGjithsejNukDuhetMeIMadhSeGjendja", cultinf));
            hfState.Set("msgAmortizimiFillestarDuhetNumer", rm.GetString("msgAmortizimiFillestarDuhetNumer", cultinf));
            hfState.Set("msgAmortizimiVjetorDuhetNumer", rm.GetString("msgAmortizimiVjetorDuhetNumer", cultinf));
            hfState.Set("msgNdryshimiIVleresDuhetNumer", rm.GetString("msgNdryshimiIVleresDuhetNumer", cultinf));
            hfState.Set("msgAmortizimiGjithsejDuhetNumer", rm.GetString("msgAmortizimiGjithsejDuhetNumer", cultinf));
            hfState.Set("msgGjendjaDuhetNumer", rm.GetString("msgGjendjaDuhetNumer", cultinf));
            hfState.Set("headerPopUpZgjidhArtikullin", rm.GetString("headerPopUpZgjidhArtikullin", cultinf));
            hfState.Set("msgShenoniMagazinen", rm.GetString("msgShenoniMagazinen", cultinf));
            hfState.Set("msgGabimGjateTransferimitTeTeDhenave", rm.GetString("msgGabimGjateTransferimitTeTeDhenave", cultinf));
            hfState.Set("msgNdodhiGabimGjateMarrjesSeTeDhenave", rm.GetString("msgNdodhiGabimGjateMarrjesSeTeDhenave", cultinf));
            hfState.Set("msgSerialetEArtikullitUhoqenSepseNukIPerkasinKesajMagazines", rm.GetString("msgSerialetEArtikullitUhoqenSepseNukIPerkasinKesajMagazines", cultinf));
            hfState.Set("msgZgjidhMagazinen", rm.GetString("msgZgjidhMagazinen", cultinf));
            hfState.Set("popupAdministrimiUniversal", rm.GetString("popupAdministrimiUniversal", cultinf));
            hfState.Set("msgZgjidhDokumentin", rm.GetString("msgZgjidhDokumentin", cultinf));
            hfState.Set("msgKjoMagazineNukEkziston", rm.GetString("msgKjoMagazineNukEkziston", cultinf));
            hfState.Set("msgZgjidhniSerialet", rm.GetString("msgZgjidhniSerialet", cultinf));
            hfState.Set("msgZgjidhniNjeArtikullAfatGjate", rm.GetString("msgZgjidhniNjeArtikullAfatGjate", cultinf));
            hfState.Set("msgKyArtikullNdodhetNjehereNeGride", rm.GetString("msgKyArtikullNdodhetNjehereNeGride", cultinf));
            hfState.Set("msgNukMundTeKryesniVeprimeAnalitikeMeArtMeSerialeTeNdashem", rm.GetString("msgNukMundTeKryesniVeprimeAnalitikeMeArtMeSerialeTeNdashem", cultinf));
            hfState.Set("msgNukMundTeKryesniVeprimeAnalitikeMeArtikujAfatShkurter", rm.GetString("msgNukMundTeKryesniVeprimeAnalitikeMeArtikujAfatShkurter", cultinf));
            hfState.Set("msgNukMundTeKryesniVeprimeAnalitikeMeArtikujJoRezerve", rm.GetString("msgNukMundTeKryesniVeprimeAnalitikeMeArtikujJoRezerve", cultinf));
            hfState.Set("msgArtikulliEshteInaktiv", rm.GetString("msgArtikulliEshteInaktiv", cultinf));
            hfState.Set("msgShperndarjeNeQendratEKostos", rm.GetString("msgShperndarjeNeQendratEKostos", cultinf));
            hfState.Set("msgDeshironiShperndarjeQendraKosto", rm.GetString("msgDeshironiTeBeniShperndarjenNeQendratEKostos", cultinf));
            hfState.Set("MenuKokeDokumenti", rm.GetString("MenuKokeDokumenti", cultinf));
            hfState.Set("MenuTrupDokumenti", rm.GetString("MenuTrupDokumenti", cultinf));
            hfState.Set("MenuFundDokumenti", rm.GetString("MenuFundDokumenti", cultinf));
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idPerdoruesi, int idViti, int idNdermarrje, ASPxMenu aSPxMenu1, ResourceManager rm, CultureInfo ci)
        {
            DbCore.DbShare.colMenuItem menu = new DbCore.DbShare.colMenuItem(DbCore.mySessionObjects.ktheGjuhe(Session));
            menu.merrMenuItemSipasKomponentesRegjistrime(DbCore.mySessionObjects.ktheGjuhe(Session), DbCore.clsFunksione.GetKomponente(Page.Request), idPerdoruesi, idNdermarrje, idViti, hfShtimModifikim.Value == "modifikim" ? false : true);
            DbCore.DbAsete.clsAmortizimiKoka koka = new DbCore.DbAsete.clsAmortizimiKoka();
            if ((hfShtimModifikim.Value == "modifikim"))
            {
                int id = int.Parse(Request.QueryString["id"]);
                koka.ktheAmortizimKokaSipasId(id);
            }
            DbCore.DbKontabiliteti.clsKokaFleteKontabel kok = new clsKokaFleteKontabel();
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
                }
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "PrintPreview")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "Fshi")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;

                if (m.Name == "FletaKontabel")
                {
                    if ((hfShtimModifikim.Value != "modifikim"))
                    {
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;

                    }
                    else
                    {
                        kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 90);

                        if (kok.NrDukumentiKokaFleteKontabel != null)
                            aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&id=" + kok.IdKokaFleteKontabel + "&numur=" + kok.NrDukumentiKokaFleteKontabel + "')";
                        else
                        {
                            aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                        }
                    }

                }
                if (m.Name == "QendraKosto")
                {
                    if ((hfShtimModifikim.Value != "modifikim"))
                    {
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;

                    }
                    else
                    {
                        if (kok.NrDukumentiKokaFleteKontabel != null)
                        {
                            DbCore.DbQendraKosto.clsKokaQendraKosto qend = new DbCore.DbQendraKosto.clsKokaQendraKosto();
                            qend.KtheKokaQKSipasIDGjeneruesDheKonfig(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);


                            if (qend.NrDok != null)
                            {
                                string headerTextShperndarjeNeQK = rm.GetString("msgShperndarjeNeQendratEKostos", ci);
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myButtonClickLupa.LupaUniversal_Click('" + headerTextShperndarjeNeQK + "','LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + "',900,600)";
                            }
                            else
                            {
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;

                            }
                        }
                        else aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    }
                }
                if (m.Name == "Ruaj")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;

                }

                if (m.Name == "ItemFrame")
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, DbCore.clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
            }
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo);
            bool visible = clsFunksione.merrMenuVisibleDraft(hfShtimModifikim.Value == "modifikim" ? false : true, koka.IdStatusDokumenti);
            ASPxMenu1.Items.FindByName("Draft").ClientVisible = visible;
        }
        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheNdermarrjeVit(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1, rm, cultinf);
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
        private void konfiguroVleraFillestareShto(int idGjuha, int idNdermarrje, ResourceManager rm, CultureInfo cultinf)
        {
            dteDtDok.Date = DateTime.Today;
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            dteDtRegjistrimi.Date = DateTime.Today;
            vendosDataDefault();
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina);
            mbushComboNivelesh(idNdermarrje, cmbLloji);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbLlogariKunderParti);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(cmbLlogariKunderParti);
            //DbCore.clsFunksione.mbushComboLlogariaPaKolona(idPerdoruesi, idNdermarrje, cmbLlogariKunderParti);
            //if (Request.QueryString["lloj"] == "amortizim")
                cmbLloji.SelectedIndex = 0;
            //else
            //    cmbLloji.SelectedIndex = 1;
            mbushComboKonfigurimet(false, idGjuha, rm, cultinf);
            ConfigureAspxComboBox.mbushComboStandartAmortizimi(cmbStandarti, idNdermarrje);
            cmbStandarti.SelectedIndex = 0;
            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(idNdermarrje, cmbDegeAdministrative, false);
            ConfigureAspxComboBox.shtoKolonaPerMagazina(btneMagazina);
            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btneMagazina, idPerdoruesi, true, 2, true);
            cmbLloji.ClientSideEvents.Init = "function(s,e){TextChangedLloji();}";
            //e kalojme shtim si false, sepse ne rastin e magazines pavaresisht klientit do merret gjithmone formati i numrit per monedhen baze
            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 1007, "", -1, true);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));

        }
        private void mbushComboNivelesh(int idNdermarrje, ASPxComboBox cmblloji)
        {
            var dt = DbCore.DbRegjistrim.colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(90, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), false);
            if (Request.QueryString["lloj"] == "amortizim")
                cmblloji.DataSource = dt.Table.Select("Kodi like 'AMFI%'").GetDataTable(dt.Table);
            else cmblloji.DataSource = dt.Table.Select("Kodi like 'RIAM%'").GetDataTable(dt.Table);

            cmblloji.TextField = "Kodi";
            cmblloji.ValueField = "IdNivel";
            cmblloji.DataBind();
            cmblloji.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
        }

        /// <summary>
        /// Vendos vlerat default kur po behet modifikim
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNderViti"></param>
        /// <param name="idNdermarrje"></param>
        private void konfiguroVleraFillestareModifiko(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, ResourceManager rm, CultureInfo cultinf)
        {//mbush kombot dhe gridat
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina);
            ConfigureAspxComboBox.mbushComboStandartAmortizimi(cmbStandarti, idNdermarrje);
            mbushComboNivelesh(idNdermarrje, cmbLloji);
            ConfigureAspxComboBox.shtoKolonaPerMagazina(btneMagazina);
            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(idNdermarrje, cmbDegeAdministrative, false);
            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btneMagazina, idPerdoruesi, false, 2, true);
            //DbCore.clsFunksione.shtoKolonaPerLlogarine(cmbLlogariKunderParti);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbLlogariKunderParti);
            //DbCore.clsFunksione.mbushComboLlogariaPaKolona(idPerdoruesi, idNdermarrje, cmbLlogariKunderParti,);
            cmbLloji.ClientSideEvents.Init = "function(s,e){TextChangedLloji();}";

            int id = int.Parse(Request.QueryString["id"]);
            DbCore.DbAsete.clsAmortizimiKoka kok = new DbCore.DbAsete.clsAmortizimiKoka(id);
            if (kok != null)
            {
                MerrTedhenat(idGjuha, idPerdoruesi, idViti, idNdermarrje, kok, rm, cultinf);
            }

            mbushListeRegjistrimAmortizimTrupiModifiko(kok);
        }

        /// <summary>
        /// Merr trupin ekzistues te dokumentit te magazines qe po modifikohet
        /// </summary>
        private void mbushListeRegjistrimAmortizimTrupiModifiko(DbCore.DbAsete.clsAmortizimiKoka koka)
        {//mbush griden me te dhenat
            //clsKusht kusht = new clsKusht(koka.IdKonfigurimAmbjenti, "FAAP");
            //clsAlternativaKushti alternativa = new clsAlternativaKushti(kusht.Vlera);
            if (clsAlternativaKushti.getAlternativa(koka.IdKonfigurimAmbjenti, "FAAP") == "Analitike")
                koka.ColTrupi.merrAmortizimTrupiSipasIdKokaAmortizimi(koka.IdAmortizimi);
            else
                koka.ColTrupi.ktheAmortizimTrupiSipasIdKokaAmortizimiGrupSipasArtikullit(koka.IdAmortizimi);
            mbushHiddenFieldet(koka.ColTrupi, koka.IdKonfigurimAmbjenti, koka.IdLlojStandarti, koka.IdAmortizimi, koka.DateDokumenti); ;
        }

        private void mbushHiddenFieldet(DbCore.DbAsete.colAmortizimiTrupiAbstract col, int lloji, int idllojstandarti, int idkokamagazina, DateTime data)
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            serializusi.MaxJsonLength = 50000000;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti(lloji);
            HfKonfAmb.Value = serializusi.Serialize(konf);

            HfColTrupMag.Value = serializusi.Serialize(col);
            HfColArt.Value = serializusi.Serialize(col.ktheColArtikuj());
            HfColAmort.Value = serializusi.Serialize(col.ktheColAmortizimFillestar(mySessionObjects.merrIdNdermarrjeSesioni(Session), idllojstandarti, data));
            HfColNjesAdminis.Value = serializusi.Serialize(col.ktheColMag(DbCore.mySessionObjects.ktheIdPerdoruesi(Session)));
            DbCore.DbAsete.colAQTSeriale colseriale = col.ktheColSeriale(mySessionObjects.merrIdNdermarrjeSesioni(Session));
            this.HfColDetArt.Value = serializusi.Serialize(colseriale);
            for (int i = 0; i < col.Count; i++)
            {

                DbCore.DbAsete.colAQTSeriale colzgjedhur = new DbCore.DbAsete.colAQTSeriale();
                if (colseriale[i].IdAQTSerial > 0)
                    colzgjedhur.Add(colseriale[i]);

                hfSeriale.Set(col[i].IdArtikulli + "_" + (i + 1), serializusi.Serialize(colzgjedhur));

            }

        }

        private void mbushComboKonfigurimet(bool mod, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            DbCore.DbShare.colKonfigurimAmbjenti colKonfig = new DbCore.DbShare.colKonfigurimAmbjenti();
            int idKategori = 90;//rivleresim amortizimi
            int idNdermarje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            if (cmbLloji.Value != null)
            {
                int idNivel = int.Parse(cmbLloji.Value.ToString());
                colKonfig.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(idKategori, idNivel, idPerdoruesi);
            }
            else
                colKonfig.mbushKonfigAmbjSipasIdKategori(idKategori, idNdermarje, idPerdoruesi, idGjuha);
            cmbKonfigurimi.IncrementalFilteringMode = DevExpress.Web.IncrementalFilteringMode.Contains;
            DbCore.DbShare.colKonfigurimAmbjenti konfVarura = new DbCore.DbShare.colKonfigurimAmbjenti();
            foreach (DbCore.DbShare.clsKonfigurimAmbjenti konfi in colKonfig)
            {
                var alternativa = clsAlternativaKushti.getAlternativa(konfi.IdKonfigAmbjente, "V");
                if (alternativa == "Po" && !mod)
                    konfVarura.Add(konfi);
            }
            foreach (DbCore.DbShare.clsKonfigurimAmbjenti konfi in konfVarura)
            {
                colKonfig.Remove(konfi);
            }
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodKonfigAmbjente";
            colprove.Caption = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimKonfigAmbjente";
            colemer.Caption = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            colemer.Width = 300;
            cmbKonfigurimi.TextFormatString = "{0}";
            cmbKonfigurimi.Columns.Add(colprove);
            cmbKonfigurimi.Columns.Add(colemer);
            cmbKonfigurimi.DataSource = colKonfig;
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
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate();
                ruajRegjistrimAmortizimi(1, true);
                mbushHiddenFieldet(new DbCore.DbAsete.colAmortizimiTrupi(), 1, 0, 0, DateTime.Today);
            }

            if (e.Item.Name == "Draft")
            {
                Page.Validate();
                ruajRegjistrimAmortizimi(0, true);// 0 = statusi i dokumentit(ne kete rast statusi eshte draft)
                mbushHiddenFieldet(new DbCore.DbAsete.colAmortizimiTrupi(), 1, 0, 0, DateTime.Today);
            }

        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {//ben fshirjen e rreshtave te selektuar


            int id = int.Parse(Request.QueryString["id"]);
            DbCore.DbAsete.clsAmortizimiKoka clsKoka = new DbCore.DbAsete.clsAmortizimiKoka(id);
            DbCore.clsMesazh mesazhi = new DbCore.clsMesazh();
            bool lidhur = clsKoka.eshteILidhur();
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (clsKoka.DateDokumenti.Year != new DbCore.DbAdmin.clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", ci), pnlMesazhi);
                return;
            }
            if (lidhur == false)
            {
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                //DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(clsKoka.DateDokumenti, idNdermarrje);
                //DbCore.clsMesazh mesazh = periudha.isPeriudheKycur();
                bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DateDokumenti, idNdermarrje);
                if (ekycur)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPeriudhaEKycur", ci), pnlMesazhi);
                    return;
                }

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DateAmortizimi, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, DbCore.DbRegjistrim.KategoriDokumenti.RivleresimeAmortizimi, clsKoka.IdKonfigurimAmbjenti))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                    return;
                }

                clsKoka.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
                bool rivleresim = false;
                if (Request.QueryString["lloj"] == "rivleresim") rivleresim = true;
                mesazhi = clsKoka.fshiTrans(clsKoka.IdPerdoruesi, 90, rivleresim, new DbCore.DbAsete.colAmortizimiTrupi());

            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDokLidhurNukFshihet", ci), pnlMesazhi);
                return;
            }
            if (mesazhi.Status)
            {
                Response.Redirect("RivleresimeAmortizimi.aspx?lloj=" + Request.QueryString["lloj"] + "&fshi=po&mesazh=" + mesazhi.PershkrimMesazhi);
                return;
            }
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);

        }

        /// <summary>
        /// Ruan/Modifikon nje objekt clsKokaMagazina.
        /// Therret funksionin <see cref="krijoRegjistrimMagazine"/>
        /// </summary>
        /// <param name="statusDokumenti">Statusi me te cilin po ruhet dokumenti (I rregullt apo draft)</param>
        private void ruajRegjistrimAmortizimi(int statusDokumenti, bool kontrollosasi)
        {
            int meKontabilizim = 1;
            if (Page.IsValid == false)
                return;
            string shfaqmesazhapolupe = "jo";
            string mesazhinformues = "";    string mesazhmevonshem = "";
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            if (isValidRegjistrimMagazine(statusDokumenti, rm, ci))
            {
                //TODO Me nestilen per punen e zgjedhjes se Standartit. Nese ka standart te zgjedhur do te thote qe rivleresimi do te jete vetem sipas atij standarti, pra eshte menyra e re e rivleresimit. Ne rast se nuk do te jete fare aktiv rivleresimi do te vazhdoje sic ishte. Ne raste kur nuk ka standart te specifikuar ath standarti do kete vleren -1.
                int idLlojStandarti = -1;
                if (clsAlternativaKushti.getAlternativa(int.Parse(cmbKonfigurimi.Value.ToString()), "RASS") == "Po" && int.Parse(cmbStandarti.Value.ToString()) > 0)
                    idLlojStandarti = int.Parse(cmbStandarti.Value.ToString());
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                DbCore.DbAsete.colSerialetMagazine serialemag = new DbCore.DbAsete.colSerialetMagazine();
                DbCore.DbAsete.colAmortizimiKoka koka = new DbCore.DbAsete.colAmortizimiKoka();
                DbCore.DbAsete.colSerialetMagazine seriale = new DbCore.DbAsete.colSerialetMagazine();
                DbCore.DbShare.clsKonfigurimAmbjenti konfmag = new clsKonfigurimAmbjenti();
                DbCore.DbAsete.colAmortizimiFillestar colaqt = new DbCore.DbAsete.colAmortizimiFillestar();
                try
                {
                    koka = krijoRegjistrimAmortizimi(statusDokumenti, konfmag, seriale, colaqt, rm, ci, idLlojStandarti, out mesazhmevonshem);
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                    status1.Value = "false";
                    return;
                }
                if (mesazhmevonshem != "")
                    clsMenuInfo.ShtoMesazhInformues(MenuInfo, mesazhmevonshem, pnlMesazhi);
                int idPeriudheZgjedhur = DbCore.mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha;
                if (!int.TryParse(hfKontabilizimi.Value, out meKontabilizim))
                    throw new MyException(rm.GetString("msgGabimGjateKonvertimitTeHFKontabilizim", ci));
                if (koka[0].ColTrupi.Count == 0&& koka[0].ColTrupiRezerva.Count==0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo,"Nuk ka trup per kete dokument", pnlMesazhi);
                    status1.Value = "false";
                    return;
                }

                if (statusDokumenti == 0)
                    meKontabilizim = 0;
                string pershkrimFK;
                if (koka[0].Shenime != String.Empty)
                    pershkrimFK = koka[0].Shenime;
                else
                    if (Request.QueryString["lloj"] == "amortizim")
                    pershkrimFK = rm.GetString("msgRivleresimAmortNgaRivleresimiIAmort", ci);
                else
                    pershkrimFK = rm.GetString("msgRivleresimAmortNgaAmortFillestar", ci);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), DbCore.clsFunksione.GetKomponente(Page.Request));
            
                if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim")
                {
                    if ((statusDokumenti == 1 && !tedrejtaInfo.DShtim) || (statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft))
                    //if (!tedrejtaInfo.DShtim)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                        status1.Value = "false";
                        return;
                    }
                    mesazh = koka.ruajListAmortizimeTrans(0, meKontabilizim, out shfaqmesazhapolupe, new DbCore.DbAsete.colAmortizimiKoka(), idPeriudheZgjedhur, 90, seriale, true, colaqt, hfNrAutoShitje, rm, ci, false, out mesazhmevonshem);
                }
                else if (hfShtimModifikim.Value == "modifikim")
                {
                    if ((statusDokumenti == 1 && !tedrejtaInfo.DMod) || (statusDokumenti == 0 && !tedrejtaInfo.DModifikimDraft))
                    //if (!tedrejtaInfo.DMod)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                        status1.Value = "false";
                        return;
                    }
                    int id = int.Parse(Request.QueryString["id"]);
                    koka[0].IdAmortizimi = id;
                    bool lidhur = koka[0].eshteILidhur();
                    if (lidhur.ToString() != hfLidhur.Value)
                    {
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = rm.GetString("msgDokumentiEshteILidhur", ci);
                    }
                    else
                        if (Request.QueryString["lloj"] == "amortizim")
                    {
                        DbCore.DbAsete.clsAmortizimiKoka amvjeter = new DbCore.DbAsete.clsAmortizimiKoka(id);
                        koka[0].IdKrijuesi = amvjeter.IdKrijuesi;
                        koka[0].NrRenditje = amvjeter.NrRenditje;
                        koka[0].IdDokNga = amvjeter.IdAmortizimi;
                        mesazh = koka[0].modifiko(meKontabilizim, lidhur, idPeriudheZgjedhur, 90, out shfaqmesazhapolupe, seriale, false, colaqt, rm, ci);
                    }
                    else mesazh = koka.modifikoList(meKontabilizim, lidhur, idPeriudheZgjedhur, 90, out shfaqmesazhapolupe, seriale, true, bool.Parse(hfState.Get("GJDM").ToString()), konfmag, rm, ci, idLlojStandarti,out mesazhmevonshem);
                }
               
                pergjigja.Text = "ruaj";
                if (mesazh.Status == true)
                {
                    hfqkmesazhi.Value = shfaqmesazhapolupe;
                    if (shfaqmesazhapolupe != "jo")
                    {
                        foreach (DbCore.DbAsete.clsAmortizimiKoka k in koka)
                        {
                            clsKokaFleteKontabel kok = new clsKokaFleteKontabel(k.IdAmortizimi, 90);
                            if (kok.IdKokaFleteKontabel != 0)
                            {
                                hfUrl.Value = "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente;
                                break;
                            }
                        }
                    }

                    hfShtimModifikim.Value = "shtim";
                    percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idNdermarrje, ASPxMenu1, rm, ci);

                    hfShtimModifikim.Value = "shtim";

                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci) + mesazhinformues, pnlMesazhi);

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
            else
            {
                status1.Value = "false";
            }
            pergjigja.ClientVisible = false;
        }

        /// <summary>
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina</returns>
        private DbCore.DbAsete.colAmortizimiKoka krijoRegjistrimAmortizimi(int statusDokumenti, DbCore.DbShare.clsKonfigurimAmbjenti konfmag, DbCore.DbAsete.colSerialetMagazine seriale, DbCore.DbAsete.colAmortizimiFillestar colaqt, ResourceManager rm, CultureInfo ci, int idLlojStandarti, out string mesazhmevonshem)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbShare.clsKonfigurimAmbjenti clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            if (cmbKonfigurimi.Text != "")
                clsKonf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            else
                clsKonf.mbushKonfigDefaultKomponentes(1007, idNdermarrje);

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(dteDtDok.Date, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, KategoriDokumenti.RivleresimeAmortizimi, clsKonf.IdKonfigAmbjente))
                throw new Exception(MessagesResource.Messages["msgPeriodIsClosed"]);

            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            ///DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, null, ASPxSplitter1, null);

            hfNrAutoShitje = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");

            DbCore.DbAsete.colAmortizimiKoka koka = new DbCore.DbAsete.colAmortizimiKoka();

            int idnivel = int.Parse(cmbLloji.Value.ToString());

            List<double> amortizimiFillestar = new List<double>();
            koka = krijoRegjistrimAmortizimi(statusDokumenti, clsKonf, konfmag, idnivel, ruajTrupin(amortizimiFillestar, rm, ci), seriale, amortizimiFillestar, colaqt, rm, ci, idLlojStandarti,out mesazhmevonshem);

            return koka;
        }

        /// <summary>
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina ne rastin kur kemi hyrje
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <param name="klienti">Klienti</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina</returns>
        private DbCore.DbAsete.colAmortizimiKoka krijoRegjistrimAmortizimi(int statusDokumenti, DbCore.DbShare.clsKonfigurimAmbjenti clsKonf, DbCore.DbShare.clsKonfigurimAmbjenti konfmag, int idnivel, DbCore.DbAsete.colAmortizimiTrupiAbstract coltrupi, DbCore.DbAsete.colSerialetMagazine seriale, List<double> amortizimifillestar, DbCore.DbAsete.colAmortizimiFillestar aqtseriale, ResourceManager rm, CultureInfo ci, int idLlojStandarti, out string mesazhmevonshem)
        {
            DbCore.DbAsete.colAmortizimiKoka colAmortizim = new DbCore.DbAsete.colAmortizimiKoka();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            int idmagazina = 0;

            string magazina = "";

            if (btneMagazina.Text != "")
            {
                idmagazina = int.Parse(btneMagazina.Value.ToString());
                magazina = btneMagazina.Text.Split(' ')[0];
            }

            int idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int iddege = 0;
            if (cmbDegeAdministrative.Text != "")
                iddege = int.Parse(cmbDegeAdministrative.Value.ToString());
            clsLlogari llog = new clsLlogari(cmbLlogariKunderParti.Text, idNdermarrje);
            //  if (krijoseriale) krijoSeriale(colserialemag, coltrupi, statusDokumenti, idNdermarrje, idperdoruesi, idperdoruesi, clsKonf, kontrollosasi);
            konfmag.mbushKonfigAmbjSipasId(clsKonf.IdKonfigurimi, DbCore.mySessionObjects.ktheGjuhe(Session));
            DbCore.clsMesazh mesazh;
            if (Request.QueryString["lloj"] == "amortizim")
            {
                DbCore.DbAsete.clsAmortizimiKoka koka = new DbCore.DbAsete.clsAmortizimiKoka();
                int id = 0;
                int.TryParse(Request.QueryString["id"], out id);
                koka.IdAmortizimi = id;
                mesazh = koka.krijoDokumentAmortizimiPerAmortizimFillestar(txtNrDok.Text, idnivel, clsKonf.IdKonfigAmbjente, dteDtDok.Date, dteDtDok.Date, idmagazina, int.Parse(cmbStandarti.Value.ToString()), txtShenime.Text, idnderviti, idNdermarrje, idperdoruesi, dteDtRegjistrimi.Date, statusDokumenti, llog.IdLlogari, coltrupi, aqtseriale, amortizimifillestar, idperdoruesi,out  mesazhmevonshem);
                colAmortizim.Add(koka);
            }
            else
            {
                mesazh = colAmortizim.krijoAmortizimeKokaRivleresim(txtNrDok.Text, idnivel, clsKonf.IdKonfigAmbjente, dteDtDok.Date, dteDtDok.Date, idmagazina, txtShenime.Text, idnderviti, idNdermarrje, idperdoruesi, dteDtRegjistrimi.Date, statusDokumenti, llog.IdLlogari, coltrupi, konfmag, bool.Parse(hfState.Get("GJDM").ToString()), seriale, hfShtimModifikim.Value != "modifikim" ? true : false, idLlojStandarti, out mesazhmevonshem);
            }
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
            return colAmortizim;
        }


        private DbCore.DbAsete.colAmortizimiTrupiAbstract ruajTrupin(List<double> amortizimiFillestar, ResourceManager rm, CultureInfo ci)
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);

            DbCore.DbAsete.colAmortizimiTrupiAbstract trupat;
            if (hfState.Get("AR").Equals("Po"))
                trupat = new DbCore.DbAsete.colAmortizimiTrupiRezerva();
            else trupat = new DbCore.DbAsete.colAmortizimiTrupi();
            int idMagTemp = -1;
            bool isMagENjejte = true;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            bool kontrolloserial = true;
            if (hfState.Get("analitike").ToString() == "2")
                kontrolloserial = false;
            for (int i = 0; i < dokumenti.Length; i++)
            {
                DbCore.DbAsete.clsAmortizimiTrupiAbstract trupMag;
                if (hfState.Get("AR").Equals("Po"))
                    trupMag = new DbCore.DbAsete.clsAmortizimiTrupiRezerva(idNdermarrje, idPerdorues, kontrolloserial, (Dictionary<string, object>)dokumenti[i], Request.QueryString["lloj"], amortizimiFillestar);
                else trupMag = new DbCore.DbAsete.clsAmortizimiTrupi(idNdermarrje, idPerdorues, kontrolloserial, (Dictionary<string, object>)dokumenti[i], Request.QueryString["lloj"], amortizimiFillestar);
                if (trupMag.IdArtikulli > 0 ) //ky kusht duhet pare kur te shtohen makrot
                {
                    if (trupMag.IdNjesiAdministrative == -1)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgMagazinaNukEkziston", ci), pnlMesazhi, LoadingPanel);
                        return new DbCore.DbAsete.colAmortizimiTrupi();
                    }
                    if (idMagTemp == -1)
                        idMagTemp = trupMag.IdNjesiAdministrative;
                    else
                        if (isMagENjejte && trupMag.IdNjesiAdministrative != idMagTemp)
                        isMagENjejte = false;
                    trupat.Add(trupMag);
                }
            }
            btneMagazina.Text = "";

            if (isMagENjejte && trupat.Count > 0)
            {
                clsNjesiAdministrative magazinaPerbashket = new clsNjesiAdministrative(trupat[0].IdNjesiAdministrative, idPerdorues);
                btneMagazina.SelectedItem = btneMagazina.Items.Add(magazinaPerbashket.Kodi + " (" + magazinaPerbashket.Pershkrimi + ")", trupat[0].IdNjesiAdministrative.ToString());
                //string kodMag = clsNjesiAdministrative.kthePershkrimMagazineSipasKodit.ktheKodiNjesiAdministrativeSipasiD(trupat[0].IdNjesiAdministrative, idPerdorues);
                // btneMagazina.Value = trupat[0].IdNjesiAdministrative.ToString();
            }
            return trupat;
        }
        //private void krijoSeriale(DbCore.DbAsete.colSerialetMagazine colserialemag, DbCore.DbAsete.colAmortizimiTrupi trupi, int idstatusdok, int idNdermarrje, int idPerdoruesi, int idKrijuesi, DbCore.DbShare.clsKonfigurimAmbjenti konf, bool kontrollosasi)
        //{
        //    //JavaScriptSerializer serializusi = new JavaScriptSerializer();
        //    //int rreshti = 0;

        //    //DbCore.DbAsete.colSerialetMagazine serialetekzistuese = new DbCore.DbAsete.colSerialetMagazine();
        //    //if (hfShtimModifikim.Value == "modifikim")
        //    //{

        //    //    int id = int.Parse(Request.QueryString["id"]);
        //    //    clsKokaMagazina kok = new clsKokaMagazina();
        //    //    kok.mbushKokaMagazinaSipasID(id);
        //    //    serialetekzistuese.merrSerialetMagazineSipasIDDokumenti(id, idNdermarrje, kok.IdKonfigAmbjente);
        //    //}
        //    //foreach (clsTrupiMagazina trup in trupi)
        //    //{
        //    //    DbCore.DbAsete.colAQTSeriale col = new DbCore.DbAsete.colAQTSeriale();
        //    //    if (trup.IdLlojVeprimi == 1)//rasti artikull
        //    //    {
        //    //        clsArtikulli art = new clsArtikulli(trup.IdArtikulli);
        //    //        if (hfSeriale.Contains(trup.IdArtikulli + "_" + hfIdGride.Get(rreshti.ToString())))
        //    //        {

        //    //            object[] dokumenti = (object[])serializusi.DeserializeObject(hfSeriale.Get(trup.IdArtikulli + "_" + hfIdGride.Get(rreshti.ToString())).ToString());
        //    //            for (int i = 0; i < dokumenti.Length; i++)
        //    //            {
        //    //                DbCore.DbAsete.clsAQTSeriale serial = new DbCore.DbAsete.clsAQTSeriale((Dictionary<string, object>)dokumenti[i]);
        //    //                col.Add(serial);
        //    //                if (serialetekzistuese.Find(x => x.IdAQTSeriali == serial.IdAQTSerial && x.IdNjesiAdministrative == trup.IdMag) == null)

        //    //                    if (serial.IdNjesiAdministrativeAktuale != trup.IdMag)
        //    //                    {
        //    //                        clsNjesiAdministrative mag = new clsNjesiAdministrative(trup.IdMag);
        //    //                        throw new Exception("Seriali " + serial.AqtSerialKod + " nuk ndodhet ne magazinen " + mag.Kodi);
        //    //                    }
        //    //                DbCore.DbAsete.clsSerialetMagazine serialmag = new DbCore.DbAsete.clsSerialetMagazine(0, 0, rreshti, trup.IdArtikulli, serial.IdAQTSerial, konf.IdNivel, konf.IdKonfigAmbjente, trup.IdMag, art.MeSerial ? 1 : (float)(trup.Sasia * trup.Koeficenti), float.Parse((trup.Cmimi).ToString()), float.Parse((trup.Cmimi).ToString()) * (art.MeSerial ? 1 : (float)(trup.Sasia * trup.Koeficenti)), idstatusdok, idNdermarrje, idPerdoruesi, idKrijuesi);
        //    //                colserialemag.Add(serialmag);

        //    //            }

        //    //        }

        //    //        if (art.LlojiArt)
        //    //            if (kontrollosasi && ((trup.Sasia * trup.Koeficenti != col.Count && art.MeSerial) || (!art.MeSerial && col.Count == 0)))
        //    //                throw new Exception("Sasia e artikullit " + trup.KodiArtikull + " eshte e ndryshme nga sasia e serialeve te tij! Doni te vazhdoni me gjenerimin automatik te tyre?");
        //    //    }
        //    //    rreshti++;
        //    //}
        //}


        /// <summary>
        /// Kontrollon nese jane plotesuar te gjitha fushat e detyrueshme te kokes se dokumentit
        /// </summary>
        private bool isValidRegjistrimMagazine(int draft, ResourceManager rm, CultureInfo ci)
        {
            bool isValid;
            isValid = true;
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
            if (cmbLlogariKunderParti.Text != "")
            {
                if (!clsLlogari.ekzistonLlogari(cmbLlogariKunderParti.Text, idNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgLlogNukEkziston", ci), pnlMesazhi);
                    return false;
                }
                else if (!clsLlogari.eshteLlogariAktive(cmbLlogariKunderParti.Text, idNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKjoLlogariNukEshteAktive", ci), pnlMesazhi);
                    return false;
                }
                // return true;
            }

            if (btneMagazina.Text != "")
            {
                DbCore.DbRegjistrim.clsNjesiAdministrative mag = new clsNjesiAdministrative(btneMagazina.Text.Split(' ')[0], idNdermarrje, idPerdorues);
                if (mag.IdNjesiAdministrative == -1)
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKjoMagazineNukEkziston", ci), pnlMesazhi, LoadingPanel);
                    return isValid;
                }
                else
                {
                    mag = new clsNjesiAdministrative(btneMagazina.Text.Split(' ')[0], idNdermarrje, idPerdorues);
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

        protected void cmbLlogariKunderParti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbLlogariKunderParti"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbLlogariKunderParti, e);
                }
            }
        }

        protected void btneMagazina_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback) return;
            if (!Request.Params["__CALLBACKID"].Contains("btneMagazina")) return;
            var idNdermarrje = (int)hfState["idNdermarrje"];
            var idPerdoruesi = (int)hfState["idPerdoruesi"];
            var value = 0;
            if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                return;
            ConfigureAspxComboBox.mbushComboMagazinatMeID(idNdermarrje, btneMagazina, idPerdoruesi, false, 2, true, value);
        }
        protected void btneMagazina_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (!IsCallback) return;
            if (!Request.Params["__CALLBACKID"].Contains("btneMagazina")) return;
            var idNdermarrje = (int)hfState["idNdermarrje"];
            var idPerdoruesi = (int)hfState["idPerdoruesi"];
            ConfigureAspxComboBox.mbushComboMagazinatMeFilter(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, btneMagazina, idPerdoruesi, idNdermarrje, false, 2, true);
        }
        protected void cmbLlogariKunderParti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbLlogariKunderParti"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbLlogariKunderParti, e);
                }
            }
        }
    }
}