using DbCore;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_RegjistrimInventarizimi : MyPageBase
    {
        private ResourceManager _rm;
        private static string STR_zgjidhniDtRegj = "Zgjidhni nje datë regjistrimi!";

        protected void Page_Load(object sender, EventArgs e)
        {
        
            int idPerdoruesi;
            int idNdermarrje;
            int idViti;
            bool eshteOwn;
            int idGjuha;
            if (hfState.Count == 0)
            {
                if (!mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
                }

                idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                }
                idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
                eshteOwn = mySessionObjects.merrEshteOwnSesioni(Session);
                idGjuha = mySessionObjects.ktheGjuhe(Session);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("OwnShop", eshteOwn);
                hfState.Set("idGjuha", idGjuha);
                AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelAdministrimiKujdes", ci), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", ci), ButtonCancel, rm.GetString("labelAnullo", ci));

            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idGjuha = (int)hfState["idGjuha"];
            }

            //konfigGrid();
            if (mySessionObjects.merrPeriudheKontabel(Session) != null)
            {
                DbCore.DbAdmin.clsPeriudhaKontabel periudha = mySessionObjects.merrPeriudheKontabel(Session);
                btnPeriudha.Text = periudha.NrPeriudha.ToString();
                lblPeriudhaAktuale.Text = String.Format("{0}-{1}", periudha.FillimiPeriudha.ToShortDateString(), periudha.MbarimiPeriudha.ToShortDateString());
            }

            percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);

            if (!IsPostBack)
            {
                DbCore.DbAdmin.clsPerdorues per = new DbCore.DbAdmin.clsPerdorues(idPerdoruesi);
                hfHapurMbyllur.Value = per.InfoHapur.ToString();
                if (hfShtimModifikim.Value == "")
                {
                    mbushHiddenFieldMePerkthime(ci, rm);
                    if (Request.QueryString["shtim_modifikim"] != "modifikim")
                    {
                        hfShtimModifikim.Value = "shtim";
                        konfiguroVleraFillestareShto(idGjuha, idNdermarrje, rm, ci);
                    }

                    else
                    {
                        hfShtimModifikim.Value = "modifikim";
                        konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, rm, ci);
                    }
                    percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
                }
                else
                    if (hfShtimModifikim.Value == "shtim")
                        konfiguroVleraFillestareShto(idGjuha, idNdermarrje, rm, ci);
                    else
                        if (hfShtimModifikim.Value == "modifikim")
                            konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, rm, ci);

                colNjesiAdministrative colMagazinat = new colNjesiAdministrative();
                colMagazinat.mbushGjitheNjesiAdministrativeAktive(idNdermarrje, idPerdoruesi);
                JavaScriptSerializer serializusi = new JavaScriptSerializer();
                serializusi.MaxJsonLength = 50000000;
                hfTmpColMag.Value = serializusi.Serialize(colMagazinat);
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, mySessionObjects.ktheIdVitNdermarrje(Session), "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                if (Request.QueryString["lloj"] == "agj")
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_RegjistrimInventarizimi.aspx?lloj=agj");
                else tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_RegjistrimInventarizimi.aspx?lloj=ash");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
            }
            Container.Attributes["src"] = "";

        }
        public void MerrTedhenat(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, clsKokaInventarizim koka, ResourceManager rm, System.Globalization.CultureInfo ci)
        {
           
            string kodNiveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(koka.IdNivel);
            cmbLloji.Text = kodNiveli;
            mbushComboKonfigurimet(true, rm, ci, idGjuha, idNdermarrje, idPerdoruesi);
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente, idGjuha);
            cmbKonfigurimi.Text = konf.KodKonfigAmbjente;
            if (koka.IdRaportDesing == 0)
                cmbFormatPrintimi.Text = "";
            else
                cmbFormatPrintimi.Value = koka.IdRaportDesing.ToString();
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;
            int idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 547, "", -1, false);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = DbCore.DbShare.clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));

            if (koka.IdMag != 0)
                btneMagazina.Text = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(koka.IdMag);
            txtNrDok.Text = koka.NrDok;
            dteDtDok.Date = koka.DtDok;
            dteDtRegjistrimi.Date = koka.DtRegj;
            txtPershkrimi.Text = koka.Pershkrimi.ToString();

            DataTable dtlidhur = koka.merrIdsDokLidhur();
            hfLidhur.Value = (!(dtlidhur.Rows.Count == 0)).ToString();
            bool autorizimet = clsKokaInventarizim.kaAutorizime(koka.IdKoka, idPerdoruesi);
            hfAutorizimi.Value = autorizimet.ToString();
            if (!autorizimet)
                hfLidhur.Value = "True";
            AspxWebControlUtils.ShtoLidhje(idPerdoruesi, idViti, idNdermarrje, hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, idGjuha);

        }
        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="ci">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo ci, ResourceManager rm)
        {

            hfState.Set("msgSasiaDuhetNumer", rm.GetString("msgSasiaDuhetNumer", ci));
            hfState.Set("msgArtikulliEshteInaktiv", rm.GetString("msgArtikulliEshteInaktiv", ci));
            hfState.Set("msgNukKryhenVeprimeMeArtikujTePastokueshem", rm.GetString("msgNukKryhenVeprimeMeArtikujTePastokueshem", ci));
            hfState.Set("NukKryhenVeprimeMeArtikujProdhimOseProdhimNeProces", rm.GetString("NukKryhenVeprimeMeArtikujProdhimOseProdhimNeProces", ci));
            hfState.Set("msgNukMundTeKryeniVeprimeMeArtikujTePerbere", rm.GetString("msgNukMundTeKryeniVeprimeMeArtikujTePerbere", ci));
            hfState.Set("msgKaGabimTekRezultatiInfos", rm.GetString("msgKaGabimTekRezultatiInfos", ci));
            hfState.Set("msgZgjidhniNjeArtikullAfatGjate", rm.GetString("msgZgjidhniNjeArtikullAfatGjate", ci));
            hfState.Set("msgEkzistonArtikullNeGride", rm.GetString("msgEkzistonArtikullNeGride", ci));
            hfState.Set("msgKjoMagazineNukEkziston", rm.GetString("msgKjoMagazineNukEkziston", ci));
            hfState.Set("msgPyetjePanjohur", rm.GetString("msgPyetjePanjohur", ci));
            hfState.Set("msgGabimGjateTransferimitTeTeDhenave", rm.GetString("msgGabimGjateTransferimitTeTeDhenave", ci));
            hfState.Set("msgZgjidhDokumentin", rm.GetString("msgZgjidhDokumentin", ci));
            hfState.Set("msgNdodhiGabimGjateMarrjesSeTeDhenave", rm.GetString("msgNdodhiGabimGjateMarrjesSeTeDhenave", ci));
            hfState.Set("msgZgjidhMagazinen", rm.GetString("msgZgjidhMagazinen", ci));
            hfState.Set("msgZgjidhNjesiVartese", rm.GetString("msgZgjidhNjesiVartese", ci));
            hfState.Set("msgNukDuhetTeKeteArtikujMeCmimZero", rm.GetString("msgNukDuhetTeKeteArtikujMeCmimZero", ci));
            hfState.Set("msgKujdesKaCmimZeroNeGride", rm.GetString("msgKujdesKaCmimZeroNeGride", ci));
            hfState.Set("msgShenoniMagazinen", rm.GetString("msgShenoniMagazinen", ci));
            hfState.Set("msgSasiaNukMundTeJeteZero", rm.GetString("msgSasiaNukMundTeJeteZero", ci));
            hfState.Set("msgVleftaDuhetTeJeteNumer", rm.GetString("msgVleftaDuhetTeJeteNumer", ci));
            hfState.Set("msgCmimiDuhetTeJeteNumer", rm.GetString("msgCmimiDuhetTeJeteNumer", ci));
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", rm.GetString("msgPoTransferohetTeDhenatShtypniPerseriRuaj", ci));
            hfState.Set("msgShenoniNumrinEDokumentit", rm.GetString("msgShenoniNumrinEDokumentit", ci));
            hfState.Set("msgZgjidhniLlojin", rm.GetString("msgZgjidhniLlojin", ci));
            hfState.Set("msgShenoniLlogarine", rm.GetString("msgShenoniLlogarine", ci));
            hfState.Set("msgZgjidhniNjeDateDokumenti", rm.GetString("msgZgjidhniNjeDateDokumenti", ci));
            hfState.Set("msgZgjidhniNjeDateRegjstrimi", rm.GetString("msgZgjidhniNjeDateRegjstrimi", ci));
            hfState.Set("msgNukLejohetCmimZeroNeGride", rm.GetString("msgNukLejohetCmimZeroNeGride", ci));
            hfState.Set("msgShperndarjeNeQendratEKostos", rm.GetString("msgShperndarjeNeQendratEKostos", ci));
            hfState.Set("msgNukKeniAutorizimPerTeRuajturKeteDok", rm.GetString("msgNukKeniAutorizimPerTeRuajturKeteDok", ci));
            hfState.Set("msgTrupiDokumentitNukDuhetLeneBosh", rm.GetString("msgTrupiDokumentitNukDuhetLeneBosh", ci));
            hfState.Set("msgKaArtikujPaNjesi", rm.GetString("msgKaArtikujPaNjesi", ci));
            hfState.Set("msgZgjidhiniKonfiguriminEInfosSeArtikullit", rm.GetString("msgZgjidhiniKonfiguriminEInfosSeArtikullit", ci));
            hfState.Set("msgZgjidhPeriudheKontabel", rm.GetString("msgZgjidhPeriudheKontabel", ci));
            hfState.Set("msgShtoArtikull", rm.GetString("msgShtoArtikull", ci));
            hfState.Set("msgNdodhiNjeGabimGjateMarrjesSeMonedhes", rm.GetString("msgNdodhiNjeGabimGjateMarrjesSeMonedhes", ci));
            hfState.Set("headerPopUpZgjidhArtikullin", rm.GetString("headerPopUpZgjidhArtikullin", ci));
            hfState.Set("JQgridShtoArtikullAqt", rm.GetString("JQgridShtoArtikullAqt", ci));
            hfState.Set("MenuKokeDokumenti", MessagesResource.Messages["MenuKokeDokumenti"]);
            hfState.Set("MenuTrupDokumenti", MessagesResource.Messages["MenuTrupDokumenti"]);
            hfState.Set("MenuFundDokumenti", MessagesResource.Messages["MenuFundDokumenti"]);
            GridUtil.perktheButonaGride(hfState, ci);
        }

        public void btnJo_Click(object sender, EventArgs e)
        {
            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            mySessionObjects.ruajTrupatNeSession(Session, new colTrupiMagazina());
            if (pergjigja.Text == "fshi")
            {
                Response.Redirect("RegjistrimInventarizimi.aspx?lloj=" + Request.QueryString["lloj"] + "&fshi=po");

            }
            else if (pergjigja.Text == "ruaj")
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci), pnlMesazhi);
        }

        public void btnPo_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idPerdoruesi, int idViti, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
        
            int idgjuha = mySessionObjects.ktheGjuhe(Session);
            DbCore.DbShare.colMenuItem menu = new DbCore.DbShare.colMenuItem(idgjuha);
            menu.merrMenuItemSipasKomponentesRegjistrime(idgjuha, "Shto_RegjistrimInventarizimi.aspx?lloj=" + Request.QueryString["lloj"], idPerdoruesi, idNdermarrje, idViti, hfShtimModifikim.Value == "modifikim" ? false : true);

            clsKokaInventarizim kokmag = new clsKokaInventarizim();
            if ((hfShtimModifikim.Value == "modifikim"))
            {
                int id = int.Parse(Request.QueryString["id"]);
                kokmag.mbushKokaInventarizimSipasID(id);
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
                }
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "PrintPreview")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if ((hfShtimModifikim.Value != "modifikim") && m.Name == "Fshi")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;


                if (m.Name == "Ruaj")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;

                }
                if (m.Name == "Draft")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                    if (hfShtimModifikim.Value == "modifikim" && kokmag.IdStatusDok == 1)//nqs jemi ne modifikim dhe dokumenti eshte i ruajtur e heqim ruaj sepse nuk lejohet te modifikohet nje dokument i ruajtur
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                if (m.Name == "RuajPrint")
                {
                    if (hfShtimModifikim.Value == "modifikim" || hfShtimModifikim.Value == "shtim")
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                    else
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
           
                }
                if (m.Name == "PrintPreview")
                {
                    if (hfShtimModifikim.Value == "modifikim" )
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                    else
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;

                }
                if (m.Name == "ItemFrame")
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
            }
            EventHandler handlerPerPo = new EventHandler(btnPo_Click);
            EventHandler handlerPerJo = new EventHandler(btnJo_Click);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo, handlerPerPo, handlerPerJo);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.ktheNdermarrjeVit(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session), ASPxMenu1);
        }

        private void vendosDataDefault()
        {
            DateTime sot = new DateTime();
            sot = DateTime.Today;
            DbCore.DbAdmin.clsPeriudhaKontabel periudha = mySessionObjects.merrPeriudheKontabel(Session);
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
        private void konfiguroVleraFillestareShto(int idGjuha, int idNdermarrje, ResourceManager rm, CultureInfo ci)
        {
            dteDtDok.Date = DateTime.Today;
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            dteDtRegjistrimi.Date = DateTime.Today;
            vendosDataDefault();
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina);
            mbushComboNivelesh(idNdermarrje, cmbLloji);
            if (Request.QueryString["lloj"] == "agj")
            {
                cmbLloji.SelectedIndex = 1;
                ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btneMagazina, idPerdoruesi, true, 2, true);
             
            }
            else
            {
                cmbLloji.SelectedIndex = 0;
                ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btneMagazina, idPerdoruesi, true, 1, true);
             
            }
          
            mbushComboKonfigurimet(false, rm, ci, idGjuha, idNdermarrje, idPerdoruesi);
            ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatPrintimi, 135, idNdermarrje, true);


            cmbLloji.ClientSideEvents.Init = "function(s,e){TextChangedLloji();}";
            //e kalojme shtim si false, sepse ne rastin e magazines pavaresisht klientit do merret gjithmone formati i numrit per monedhen baze
            int idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 547, "", -1, true);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = DbCore.DbShare.clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));

        }

        private void mbushComboNivelesh(int idNdermarrje, ASPxComboBox cmblloji)
        {
            cmblloji.DataSource = colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(135, mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session), false);
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
        private void konfiguroVleraFillestareModifiko(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, ResourceManager rm, CultureInfo ci)
        {//mbush kombot dhe gridat
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina);
            ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatPrintimi, 135, idNdermarrje, true);

            mbushComboNivelesh(idNdermarrje, cmbLloji);

            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btneMagazina, idPerdoruesi, false, 0, true);
            cmbLloji.ClientSideEvents.Init = "function(s,e){TextChangedLloji();}";
            int id = int.Parse(Request.QueryString["id"]);
            clsKokaInventarizim kok = new clsKokaInventarizim();

            kok.IdKoka = id;
            kok.mbushKokaInventarizimSipasID(kok.IdKoka);
            if (kok != null)
            {
                MerrTedhenat(idGjuha, idPerdoruesi, idViti, idNdermarrje, kok, rm, ci);
            }
         
            
            

            mbushListeRegjistrimInventarizimiTrupiModifiko(kok);
        }

        /// <summary>
        /// Merr trupin ekzistues te dokumentit te magazines qe po modifikohet
        /// </summary>
        private void mbushListeRegjistrimInventarizimiTrupiModifiko(clsKokaInventarizim koka)
        {//mbush griden me te dhenat

            koka.mbushTrupInventarizim();
            mbushHiddenFieldet(koka.OcolTrupiInventarizimi, koka.IdKonfigAmbjente); ;
        }

        private void mbushHiddenFieldet(colTrupiInventarizim col, int lloji)
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            serializusi.MaxJsonLength = 50000000;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti(lloji);
            HfKonfAmb.Value = serializusi.Serialize(konf);

            HfColTrupMag.Value = serializusi.Serialize(col);

            return;

        }

        private void konfigGrid()
        {
            string emriKomponentes = "Shto_RegjistrimInventarizimi.aspx?lloj=" + Request.QueryString["lloj"];
            DbCore.DbAdmin.clsKomponente oKomponente = new DbCore.DbAdmin.clsKomponente(emriKomponentes);
            DbCore.DbShare.clsKonfigurimAmbjenti konfigurimi = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfigurimi.mbushKonfigDefaultKomponentes(oKomponente.IdKomponente, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.colGridaTrupi trupiGrides = new DbCore.DbAdmin.colGridaTrupi(oKomponente.IdKomponente, konfigurimi.IdKonfigAmbjente, (int)hfState["idGjuha"]);
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            HfGridCol.Value = serializusi.Serialize(trupiGrides);
        }

        private void mbushComboKonfigurimet(bool mod, ResourceManager rm, CultureInfo ci, int idGjuha, int idNdermarje, int idPerdoruesi)
        {
            DbCore.DbShare.colKonfigurimAmbjenti colKonfig = new DbCore.DbShare.colKonfigurimAmbjenti();
            int idKategori = 135;//inventarizim
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
            cmbKonfigurimi.TextFormatString = "{0}"; //cmbKonfigurimi.TextFormatString = "{0};{1}";
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
            int idGjuha = (int)hfState["idGjuha"];
            CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate();
                ruajRegjistrimInventarizimi(1, false);
                mbushHiddenFieldet(new colTrupiInventarizim(), 1);
            }
            if (e.Item.Name == "Draft")
            {
                Page.Validate();
                ruajRegjistrimInventarizimi(0, false);// 0 = statusi i dokumentit(ne kete rast statusi eshte draft)
                mbushHiddenFieldet(new colTrupiInventarizim(), 1);
            }
            if (e.Item.Name == "PrintPreview")
            {

                if (hfShtimModifikim.Value.ToString() == "modifikim")
                {
                    string id = Request.QueryString["id"];
                    if (String.IsNullOrEmpty(cmbFormatPrintimi.Text))
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjINVMesazhSkaFormatPerPrintim", ci), pnlMesazhi);
                    else
                    {
                        int idRaporti = clsRaporti.KtheIdRaporti(idGjuha, Convert.ToInt32(cmbFormatPrintimi.Value));
                        Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idRaporti + "&idDokumenti=" + id + "&printo=false&raportdyte=jo&iddesign=" + cmbFormatPrintimi.Value;
                    }

                    //  Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=38&idDokumenti=" + clsKoka.IdKokaMagazina + "&printo=false&raportdyte=jo&iddesign=" + cmbFormatPrintimi.Value;

                }
                else
                {
                    Page.Validate();
                    ruajRegjistrimInventarizimi(1, false);
                    mbushHiddenFieldet(new colTrupiInventarizim(), 1);
                }
            }
            if (e.Item.Name == "RuajPrint")
            {
                Page.Validate();
                ruajRegjistrimInventarizimi(1, true);
                mbushHiddenFieldet(new colTrupiInventarizim(), 1);

            }
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            pergjigja.Text = "";
            clsKokaInventarizim kokam = new clsKokaInventarizim();
            kokam.IdKoka = int.Parse(Request.QueryString["id"]);
            kokam.mbushKokaInventarizimSipasID(kokam.IdKoka);
            clsMesazh mesazh = new clsMesazh();
            bool lidhur = kokam.eshteILidhur();

            if (lidhur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("vepBankaMsgDokEshteILidhurNukFshihet", ci), pnlMesazhi);
                this.status1.Value = "false";
                return;
            }
            if (kokam.DtDok.Year != new DbCore.DbAdmin.clsNdermarrjeViti(mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                this.status1.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", ci), pnlMesazhi);
                return;
            }
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            //DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(kokam.DtDok, idNdermarrje);
            //clsMesazh mesazhi = periudha.isPeriudheKycur();
            bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(kokam.DtDok, idNdermarrje);
            if (ekycur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPeriudhaEKycur", ci), pnlMesazhi);
                return;
            }

            kokam.IdPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            mesazh = kokam.fshi();
            //Session.Add("trupat", trupat);
            if (mesazh.Status)
            {

                Response.Redirect("RegjistrimInventarizimi.aspx?lloj=" + Request.QueryString["lloj"] + "&fshi=po");
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
        /// Ruan/Modifikon nje objekt clsKokaInventarizim.
        /// Therret funksionin <see cref="krijoRegjistrimMagazine"/>
        /// Therret funksionin <see cref="DbCore.DbRegjistrim.clsKokaInventarizim.ruaj"/> ose <see cref="DbCore.DbRegjistrim.clsKokaInventarizim.modifiko"/>
        /// </summary>
        /// <param name="statusDokumenti">Statusi me te cilin po ruhet dokumenti (I rregullt apo draft)</param>
        private void ruajRegjistrimInventarizimi(int statusDokumenti, bool printo)
        {
            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            colKokaInventarizim regjistrime = new colKokaInventarizim();
            if (Page.IsValid == false)
                return;
            string shfaqmesazhapolupe = "jo";
            string mesazhinformues = "";
            if (isValidRegjistrimInventarizim(statusDokumenti))
            {
                clsMesazh mesazh = new clsMesazh();
                int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                int idGjuha = mySessionObjects.ktheGjuhe(Session);
                try
                {
                    regjistrime.Add(krijoRegjistrimInventarizimi(statusDokumenti, out mesazhinformues, idGjuha, hfShtimModifikim.Value == "shtim" ? true : false));
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                    status1.Value = "false";
                    return;
                }

                foreach (clsKokaInventarizim regjistrim in regjistrime)
                {
                    if (regjistrim.OcolTrupiInventarizimi.Count == 0)
                    {
                        status1.Value = "false";
                        return;
                    }
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_RegjistrimInventarizimi.aspx?lloj=" + Request.QueryString["lloj"]);
                    if (hfShtimModifikim.Value == "shtim")
                    {
                        if ((statusDokumenti == 1 && !tedrejtaInfo.DShtim) || (statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft))
                        //if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                            status1.Value = "false";
                            return;
                        }

                        mesazh = regjistrim.ruaj(hfNrAutoShitje, out shfaqmesazhapolupe, rm, ci, false, "0", "", "", "", true);

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

                        regjistrim.IdKoka = int.Parse(Request.QueryString["id"]);
                        bool lidhur = regjistrim.eshteILidhur();
                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = rm.GetString("msgDokumentiEshteILidhur", ci);
                            status1.Value = "false";
                        }
                        else
                        {
                            if (lidhur == true)
                                mesazh = regjistrim.modifiko(true, out shfaqmesazhapolupe, rm, ci);
                            else
                                mesazh = regjistrim.modifiko(false, out shfaqmesazhapolupe, rm, ci);
                        }

                    }
                    pergjigja.Text = "ruaj";
                    if (mesazh.Status == true)
                    {
                        hfShtimModifikim.Value = "shtim";
                        percaktoTemplateMenu(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.ktheIdVitNdermarrje(Session), idNdermarrje, ASPxMenu1);
                        if (printo)
                           if (Request.QueryString["lloj"]=="ash")
                                Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=dokumentaInvetarizimiAfatshkurter&idDokumenti=" + regjistrim.IdKoka + "&printo=true&raportdyte=jo&iddesign=" + cmbFormatPrintimi.Value;
                            else Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=dokumentaInvetarizimiAfatgjates&idDokumenti=" + regjistrim.IdKoka + "&printo=true&raportdyte=jo&iddesign=" + cmbFormatPrintimi.Value;
                        hfShtimModifikim.Value = "shtim";

                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgDokumentiURuajtMeSukese", ci) + mesazhinformues, pnlMesazhi);

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
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsKokaInventarizim
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <param name="klienti">Klienti</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaInventarizim</returns>
        private clsKokaInventarizim krijoRegjistrimInventarizimi(int statusDokumenti, out string mesazhinformues, int idGjuha, bool shtim)
        {
            mesazhinformues = "";
            
            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, null, ASPxSplitter1, null);

            hfNrAutoShitje = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");

            clsKokaInventarizim koka = new clsKokaInventarizim();

            int idnivel = int.Parse(cmbLloji.Value.ToString());
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbShare.clsKonfigurimAmbjenti clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            clsKonf.mbushKonfigDefaultKomponentes(549, idNdermarrje);
            if (cmbKonfigurimi.Text != "")
            {
                clsKonf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);

            }
            koka = krijoRegjistrimInventarizimi(statusDokumenti, clsKonf, idnivel, ruajTrupinInventarizim(), out mesazhinformues, idGjuha, shtim);

            return koka;
        }

        /// <summary>
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsKokaInventarizim ne rastin kur kemi hyrje
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <param name="eshteTransferim">Tregon nese eshte transferim apo jo</param>
        /// <param name="klienti">Klienti</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaInventarizim</returns>
        private clsKokaInventarizim krijoRegjistrimInventarizimi(int statusDokumenti, DbCore.DbShare.clsKonfigurimAmbjenti clsKonf, int idnivel, colTrupiInventarizim coltrupi, out string mesazhinformues, int idGjuha, bool shtim)
        {
            clsKokaInventarizim mag = new clsKokaInventarizim();
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            mesazhinformues = "";
            int idnderviti = mySessionObjects.ktheNdermarrjeVit(Session);
            int idmagazina = 0;

            string magazina = "", pershkrimi = "";

            if (btneMagazina.Text != "")
            {
                idmagazina = int.Parse(btneMagazina.Value.ToString());
                magazina = btneMagazina.Text;
            }

            if (txtPershkrimi.Text != "")
                pershkrimi = txtPershkrimi.Text;


            int idperdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            int idFormatPrintimi = 0;
            if (!String.IsNullOrEmpty(cmbFormatPrintimi.Text))
            {
                bool parse = int.TryParse(cmbFormatPrintimi.Value.ToString(), out idFormatPrintimi);
                if (!parse)
                    throw new MyException("Formati i printimit nuk ekziston");
            }

            clsMesazh mesazh = mag.krijoInventarizim(0, idnivel, clsKonf.IdKonfigAmbjente, idmagazina, magazina, dteDtDok.Date, txtNrDok.Text, statusDokumenti, idNdermarrje, idnderviti, idperdoruesi, dteDtRegjistrimi.Date, pershkrimi, coltrupi, out mesazhinformues, true, idperdoruesi, shtim, idFormatPrintimi);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
            return mag;
        }



        private colTrupiInventarizim ruajTrupinInventarizim()
        {
            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);
            colTrupiInventarizim trupat = new colTrupiInventarizim();

            for (int i = 0; i < dokumenti.Length; i++)
            {
                clsTrupiInventarizim trup = new clsTrupiInventarizim((Dictionary<string, object>)dokumenti[i]);
                if (trup.Barkod != "" || trup.Serial != "") //ky kusht duhet pare kur te shtohen makrot
                {
                    trupat.Add(trup);
                }
            }

            return trupat;
        }



        /// <summary>
        /// Kontrollon nese jane plotesuar te gjitha fushat e detyrueshme te kokes se dokumentit
        /// </summary>
        private bool isValidRegjistrimInventarizim(int draft)
        {
            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            bool isValid;
            isValid = true;
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);


            if (this.dteDtRegjistrimi.Text == "")
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, STR_zgjidhniDtRegj, pnlMesazhi, LoadingPanel);

                return isValid;
            }
            if (this.dteDtDok.Text == "")
            {
                isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhniNjeDateDokumenti", ci), pnlMesazhi, LoadingPanel);
                return isValid;
            }
            if (dteDtDok.Date.Year != new DbCore.DbAdmin.clsNdermarrjeViti(mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", ci), pnlMesazhi);
                return false;
            }
            DbCore.DbAdmin.clsPeriudhaKontabel periudha = mySessionObjects.merrPeriudheKontabel(Session);
            String mesazhGabimi;
            if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, dteDtDok.Date, periudha, draft))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                return false;
            }

            if (btneMagazina.Text != "")
            {
                clsNjesiAdministrative mag = new clsNjesiAdministrative(btneMagazina.Text, idNdermarrje, idPerdorues);
                if (mag.IdNjesiAdministrative == -1)
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgMagazinaNukEkziston", ci), pnlMesazhi, LoadingPanel);
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
                    else if (Request.QueryString["lloj"] == "agj" && mag.IdLlojMagazine == 1)
                    {
                        isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Kjo magazine eshte per artikujt afatshkurter dhe nuk mund te perdoret per afatgjatet!", pnlMesazhi, LoadingPanel);
                        return isValid;
                    }
                    else if (Request.QueryString["lloj"] == "ash" && mag.IdLlojMagazine == 2)
                    {
                        isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Kjo magazine eshte per artikujt afatgjate dhe nuk mund te perdoret per afathkurter!", pnlMesazhi, LoadingPanel);
                        return isValid;
                    }
                }
            }


            return isValid;
        }
    }
}