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
    public partial class Shto_RegjistrimNdryshimCmimSasi : MyPageBase
    {
        private static string pershkrimDaljeFK = "Nga ndryshim sasi/cmim";

        private static string STR_zgjidhniDtRegj = "Zgjidhni nje datë regjistrimi!";

        private colTrupiMagazina trupat = new colTrupiMagazina();

        public void MerrTedhenat(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, clsKokaNdryshimCmimSasi koka, ResourceManager rm, CultureInfo ci)
        {

            string kodNiveli = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheKodNivelRegjistrimi(koka.IdNivel);
            cmbLloji.Text = kodNiveli;
            mbushComboKonfigurimet(true, rm, ci, idGjuha, idNdermarrje, idPerdoruesi);
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente, idGjuha);
            cmbKonfigurimi.Text = konf.KodKonfigAmbjente;
            hfKonffillestar.Value = konf.KodKonfigAmbjente + ";" + konf.PershkrimKonfigAmbjente;

            if (koka.IdKonfigGjenerues != 0)
            {
                ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup1);
                ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup1, idNdermarrje, 1, koka.IdKonfigGjenerues, idPerdoruesi);
                ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup2);
                ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup2, idNdermarrje, 2, koka.IdKonfigGjenerues, idPerdoruesi);
                ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup3);
                ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup3, idNdermarrje, 3, koka.IdKonfigGjenerues, idPerdoruesi);
            }
            else
            {
                ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup1);
                ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup1, idNdermarrje, 1, koka.IdKonfigAmbjente, idPerdoruesi);
                ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup2);
                ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup2, idNdermarrje, 2, koka.IdKonfigAmbjente, idPerdoruesi);
                ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup3);
                ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup3, idNdermarrje, 3, koka.IdKonfigAmbjente, idPerdoruesi);
            }

            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 545, "", -1, false);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));
            cmbLlogariKunderParti.Text = new clsLlogari(koka.IdLlogKunderparti).NrLlogari;

            if (koka.IdMagazina != 0)
                btneMagazina.Text = DbCore.DbRegjistrim.clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(koka.IdMagazina);

            cmbDegeAdministrative.Value = koka.IdDegeAdministrative.ToString();
            txtNrDok.Text = koka.NrDok;
            dteDtDok.Date = koka.DtDok;
            dteDtRegjistrimi.Date = koka.DtRegjistrimi;

            if (koka.IdGrup1 != 0)
                cmbGrup1.Value = koka.IdGrup1.ToString();
            if (koka.IdGrup2 != 0)
                cmbGrup2.Value = koka.IdGrup2.ToString();
            if (koka.IdGrup3 != 0)
                cmbGrup3.Value = koka.IdGrup3.ToString();
            txtVlefta.Text = koka.Vlefta.ToString();// + hfFormatNumri["FormatZgjedhurVlefta"].ToString());
            txtPershkrimi.Text = koka.Pershkrimi.ToString();

            DataTable dtlidhur = koka.merrIdsDokLidhur();
            hfLidhur.Value = (!(dtlidhur.Rows.Count == 0)).ToString();


            bool autorizimet = DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi.kaAutorizime(koka.IdKoka, idPerdoruesi);
            hfAutorizimi.Value = autorizimet.ToString();
            if (!autorizimet)
                hfLidhur.Value = "True";
            //DbCore.DbShare.clsKusht kusht = new DbCore.DbShare.clsKusht(koka.IdKonfigAmbjente, "LMD");
            //DbCore.DbShare.clsAlternativaKushti alternativa = new DbCore.DbShare.clsAlternativaKushti(kusht.Vlera);
            if (koka.IdStatusDok == 1 && clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "LMD") == "Jo")
                hfLidhur.Value = "True";
            AspxWebControlUtils.ShtoLidhje(idPerdoruesi, idViti, idNdermarrje, hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, idGjuha);

        }
        
  

        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idPerdoruesi;
            int idNdermarrje;
            int idViti;
            bool eshteOwn;
            int idGjuha;
            if (hfState.Count == 0)
            {
                if (!DbCore.mySessionObjects.isLogedIn(Session))
                {
                    DbCore.clsFunksione.logout(Session,true,"FaqePaautorizuar");
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
                hfState.Set("OwnShop", eshteOwn);
                hfState.Set("idGjuha", idGjuha);
                AspxWebControlUtils.perkthePopUp(popFshi, rm.GetString("labelAdministrimiKujdes", cultinf), lblMsgbox, rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf), ButtonCancel, rm.GetString("labelAnullo", cultinf));
                //clsFunksione.perkthePopUp(popMesazhQK, rm.GetString("labelAdministrimiKujdes", cultinf), lblMsgbox4, rm.GetString("msgDeshironiTeBeniShperndarjenNeQendratEKostos", cultinf), ButtonCancelQK, rm.GetString("btnJO", cultinf), ButtonOkQK, rm.GetString("btnPO", cultinf));
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idGjuha = (int)hfState["idGjuha"];
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


                if (hfShtimModifikim.Value == "")
                {
                    mbushHiddenFieldMePerkthime(cultinf, rm);
                    if (Request.QueryString["shtim_modifikim"] != "modifikim")
                    {
                        hfShtimModifikim.Value = "shtim";
                        konfiguroVleraFillestareShto(idGjuha, idNdermarrje, rm, cultinf);
                    }

                    else
                    {
                        hfShtimModifikim.Value = "modifikim";
                        konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, rm, cultinf);
                    }
                    percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
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
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_RegjistrimNdryshimCmimSasi.aspx");
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
            }
            Container.Attributes["src"] = "";

        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            hfState.Set("MenuKokeDokumenti", rm.GetString("MenuKokeDokumenti", cultinf));
            hfState.Set("MenuTrupDokumenti", rm.GetString("MenuTrupDokumenti", cultinf));
            hfState.Set("MenuFundDokumenti", rm.GetString("MenuFundDokumenti", cultinf));
            hfState.Set("msgSasiaDuhetNumer", rm.GetString("msgSasiaDuhetNumer", cultinf));
            hfState.Set("msgArtikulliEshteInaktiv", rm.GetString("msgArtikulliEshteInaktiv", cultinf));
            hfState.Set("msgNukKryhenVeprimeMeArtikujTePastokueshem", rm.GetString("msgNukKryhenVeprimeMeArtikujTePastokueshem", cultinf));
            hfState.Set("NukKryhenVeprimeMeArtikujProdhimOseProdhimNeProces", rm.GetString("NukKryhenVeprimeMeArtikujProdhimOseProdhimNeProces", cultinf));
            hfState.Set("msgNukMundTeKryeniVeprimeMeArtikujTePerbere", rm.GetString("msgNukMundTeKryeniVeprimeMeArtikujTePerbere", cultinf));
            hfState.Set("msgKaGabimTekRezultatiInfos", rm.GetString("msgKaGabimTekRezultatiInfos", cultinf));
            hfState.Set("msgZgjidhniNjeArtikullAfatGjate", rm.GetString("msgZgjidhniNjeArtikullAfatGjate", cultinf));
            hfState.Set("msgEkzistonArtikullNeGride", rm.GetString("msgEkzistonArtikullNeGride", cultinf));
            hfState.Set("msgKjoMagazineNukEkziston", rm.GetString("msgKjoMagazineNukEkziston", cultinf));
            hfState.Set("msgPyetjePanjohur", rm.GetString("msgPyetjePanjohur", cultinf));
            hfState.Set("msgGabimGjateTransferimitTeTeDhenave", rm.GetString("msgGabimGjateTransferimitTeTeDhenave", cultinf));
            hfState.Set("msgZgjidhDokumentin", rm.GetString("msgZgjidhDokumentin", cultinf));
            hfState.Set("msgNdodhiGabimGjateMarrjesSeTeDhenave", rm.GetString("msgNdodhiGabimGjateMarrjesSeTeDhenave", cultinf));
            hfState.Set("msgZgjidhMagazinen", rm.GetString("msgZgjidhMagazinen", cultinf));
            hfState.Set("msgZgjidhNjesiVartese", rm.GetString("msgZgjidhNjesiVartese", cultinf));
            hfState.Set("msgNukDuhetTeKeteArtikujMeCmimZero", rm.GetString("msgNukDuhetTeKeteArtikujMeCmimZero", cultinf));
            hfState.Set("msgKujdesKaCmimZeroNeGride", rm.GetString("msgKujdesKaCmimZeroNeGride", cultinf));
            hfState.Set("msgShenoniMagazinen", rm.GetString("msgShenoniMagazinen", cultinf));
            hfState.Set("msgSasiaNukMundTeJeteZero", rm.GetString("msgSasiaNukMundTeJeteZero", cultinf));
            hfState.Set("msgVleftaDuhetTeJeteNumer", rm.GetString("msgVleftaDuhetTeJeteNumer", cultinf));
            hfState.Set("msgCmimiDuhetTeJeteNumer", rm.GetString("msgCmimiDuhetTeJeteNumer", cultinf));
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", rm.GetString("msgPoTransferohetTeDhenatShtypniPerseriRuaj", cultinf));
            hfState.Set("msgShenoniNumrinEDokumentit", rm.GetString("msgShenoniNumrinEDokumentit", cultinf));
            hfState.Set("msgZgjidhniLlojin", rm.GetString("msgZgjidhniLlojin", cultinf));
            hfState.Set("msgShenoniLlogarine", rm.GetString("msgShenoniLlogarine", cultinf));
            hfState.Set("msgZgjidhniNjeDateDokumenti", rm.GetString("msgZgjidhniNjeDateDokumenti", cultinf));
            hfState.Set("msgZgjidhniNjeDateRegjstrimi", rm.GetString("msgZgjidhniNjeDateRegjstrimi", cultinf));
            hfState.Set("msgNukLejohetCmimZeroNeGride", rm.GetString("msgNukLejohetCmimZeroNeGride", cultinf));
            hfState.Set("msgShperndarjeNeQendratEKostos", rm.GetString("msgShperndarjeNeQendratEKostos", cultinf));
            hfState.Set("msgNukKeniAutorizimPerTeRuajturKeteDok", rm.GetString("msgNukKeniAutorizimPerTeRuajturKeteDok", cultinf));
            hfState.Set("msgTrupiDokumentitNukDuhetLeneBosh", rm.GetString("msgTrupiDokumentitNukDuhetLeneBosh", cultinf));
            hfState.Set("msgKaArtikujPaNjesi", rm.GetString("msgKaArtikujPaNjesi", cultinf));
            hfState.Set("msgZgjidhiniKonfiguriminEInfosSeArtikullit", rm.GetString("msgZgjidhiniKonfiguriminEInfosSeArtikullit", cultinf));
            hfState.Set("msgZgjidhPeriudheKontabel", rm.GetString("msgZgjidhPeriudheKontabel", cultinf));
            hfState.Set("msgShtoArtikull", rm.GetString("msgShtoArtikull", cultinf));
            hfState.Set("msgNdodhiNjeGabimGjateMarrjesSeMonedhes", rm.GetString("msgNdodhiNjeGabimGjateMarrjesSeMonedhes", cultinf));
            hfState.Set("headerPopUpZgjidhArtikullin", rm.GetString("headerPopUpZgjidhArtikullin", cultinf));
            hfState.Set("JQgridShtoArtikullAqt", rm.GetString("JQgridShtoArtikullAqt", cultinf));
            hfState.Set("msgDeshironiShperndarjeQendraKosto", rm.GetString("msgDeshironiTeBeniShperndarjenNeQendratEKostos", cultinf));
            hfState.Set("msgArtikulliAfatgjate", rm.GetString("msgArtikulliAfatgjate", cultinf));
            GridUtil.perktheButonaGride(hfState, cultinf);
        }

        public void btnJo_Click(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
            if (pergjigja.Text == "fshi")
            {
                Response.Redirect("RegjistrimNdryshimCmimSasi.aspx?fshi=po");
                return;
            }
            else if (pergjigja.Text == "ruaj")
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", cultinf), pnlMesazhi);
        }

        public void btnPo_Click(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            trupat = DbCore.mySessionObjects.merrTrupatNgaSesioni(Session);
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh(true, rm.GetString("regjMagMesazhSuksesRivleresimi", cultinf));
            string fileLogPath = Server.MapPath("~/log/log.txt");
            DbCore.DbAdmin.clsLogRivleresimInventari log = new DbCore.DbAdmin.clsLogRivleresimInventari();
            try
            {
                log = new DbCore.DbAdmin.clsLogRivleresimInventari(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                throw new DbCore.MyException(rm.GetString("msgGabimGjateRuajtjesSeRivleresimitNeLog", ci));
            }
            foreach (DbCore.DbRegjistrim.clsTrupiMagazina t in trupat)
            {
                //DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(t.IdArtikulli);
                mesazh = DbCore.DbInventari.clsArtikulli.rivleresimCmimiMesatar(t.IdArtikulli, DbCore.DbInventari.clsArtikulli.ktheMetodeKostoje(t.IdArtikulli), t.IdMag, t.Data, DateTime.Today,log,ci,rm, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                if (!mesazh.Status)
                {
                    if (pergjigja.Text == "fshi")
                    {
                        Response.Redirect("RegjistrimNdryshimCmimSasi.aspx?fshi=rivleresimjo");
                        return;
                    }
                    else
                    {
                        Response.Redirect("RegjistrimNdryshimCmimSasi.aspx?fshi=rivleresimruajjo");
                        return;
                    }
                    
                }
            }

            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
            if (mesazh.Status)
            {
                if (pergjigja.Text == "fshi")
                {
                    Response.Redirect("RegjistrimNdryshimCmimSasi.aspx?fshi=rivleresimpo");
                    return;
                }
                else
                {
                    Response.Redirect("RegjistrimNdryshimCmimSasi.aspx?fshi=rivleresimruajpo");
                    return;
                }
                
            }
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idPerdoruesi, int idViti, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idgjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
            DbCore.DbShare.colMenuItem menu = new DbCore.DbShare.colMenuItem(idgjuha);
            menu.merrMenuItemSipasKomponentesRegjistrime(idgjuha, "Shto_RegjistrimNdryshimCmimSasi.aspx", idPerdoruesi, idNdermarrje, idViti, hfShtimModifikim.Value == "modifikim" ? false : true);

            clsKokaNdryshimCmimSasi kokmag = new clsKokaNdryshimCmimSasi();
            if ((hfShtimModifikim.Value == "modifikim"))
            {
                int id = int.Parse(Request.QueryString["id"]);
                kokmag.mbushKokaNdryshimCmimSasiSipasID(id);
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
                        kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 95);
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
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myButtonClickLupa.LupaUniversal_Click('" + rm.GetString("msgShperndarjeNeQendratEKostos", cultinf) + "','LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + "',900,600)";
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
                    if (hfShtimModifikim.Value == "modifikim" && kokmag.IdStatusDok == 1)//nqs jemi ne modifikim dhe dokumenti eshte i ruajtur e heqim ruaj sepse nuk lejohet te modifikohet nje dokument i ruajtur
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                if (m.Name == "Draft")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                    if (hfShtimModifikim.Value == "modifikim" && kokmag.IdStatusDok == 1)//nqs jemi ne modifikim dhe dokumenti eshte i ruajtur e heqim ruaj sepse nuk lejohet te modifikohet nje dokument i ruajtur
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                if (m.Name == "RuajPrint")
                {
                    if (cmbKonfigurimi.Text == "FHTK" && kokmag.IdStatusDok == 4 && (hfShtimModifikim.Value == "modifikim"))
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    if (cmbKonfigurimi.Text == "FHTK" && kokmag.IdStatusDok == 0 && (hfShtimModifikim.Value == "modifikim"))
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].Text = "Konfirmo dhe printo";
                    if (hfShtimModifikim.Value == "modifikim" && kokmag.IdStatusDok == 1)//nqs jemi ne modifikim dhe dokumenti eshte i ruajtur e heqim ruaj sepse nuk lejohet te modifikohet nje dokument i ruajtur
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                if (m.Name == "ItemFrame")
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, DbCore.clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
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
        private void konfiguroVleraFillestareShto(int idGjuha, int idNdermarrje, ResourceManager rm, CultureInfo ci)
        {
            dteDtDok.Date = DateTime.Today;
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            dteDtRegjistrimi.Date = DateTime.Today;
            vendosDataDefault();
            int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina);
            mbushComboNivelesh(idNdermarrje, cmbLloji);

            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(cmbLlogariKunderParti);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbLlogariKunderParti);
            cmbLloji.SelectedIndex = 0;
            mbushComboKonfigurimet(false, rm, ci, idGjuha, idNdermarrje, idPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup1);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup1, idNdermarrje, 1, Convert.ToInt32(cmbKonfigurimi.Value), idPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup2);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup2, idNdermarrje, 2, Convert.ToInt32(cmbKonfigurimi.Value), idPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup3);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup3, idNdermarrje, 3, Convert.ToInt32(cmbKonfigurimi.Value), idPerdoruesi);
            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(idNdermarrje, cmbDegeAdministrative, false);

            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btneMagazina, idPerdoruesi, true, 0,true);

            cmbLloji.ClientSideEvents.Init = "function(s,e){TextChangedLloji();}";
            //e kalojme shtim si false, sepse ne rastin e magazines pavaresisht klientit do merret gjithmone formati i numrit per monedhen baze
            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 545, "", -1, true);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));
            txtVlefta.Text = "0.00";
        }

        private void mbushComboNivelesh(int idNdermarrje, ASPxComboBox cmblloji)
        {
            cmblloji.DataSource = DbCore.DbRegjistrim.colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriDtCombo(95, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session), false);
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

            mbushComboNivelesh(idNdermarrje, cmbLloji);
            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(idNdermarrje, cmbDegeAdministrative, true);

            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btneMagazina, idPerdoruesi, false, 0,true);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(cmbLlogariKunderParti);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbLlogariKunderParti);
            cmbLloji.ClientSideEvents.Init = "function(s,e){TextChangedLloji();}";
            txtVlefta.Text = "0.00";
            int id = int.Parse(Request.QueryString["id"]);
            clsKokaNdryshimCmimSasi kok = new clsKokaNdryshimCmimSasi();

            kok.IdKoka = id;
            kok.mbushKokaNdryshimCmimSasiSipasID(kok.IdKoka);
            if (kok != null)
            {
                MerrTedhenat(idGjuha, idPerdoruesi, idViti, idNdermarrje, kok, rm, ci);
            }

            mbushListeRegjistrimNdryshimCmimSasiTrupiModifiko(kok);
        }

        /// <summary>
        /// Merr trupin ekzistues te dokumentit te magazines qe po modifikohet
        /// </summary>
        private void mbushListeRegjistrimNdryshimCmimSasiTrupiModifiko(clsKokaNdryshimCmimSasi koka)
        {//mbush griden me te dhenat

            koka.mbushTrupNdryshimCmimSasi();
            mbushHiddenFieldet(koka.OcolTrupiNdryshimCmimSasi, koka.IdKonfigAmbjente); ;
        }

        private void mbushHiddenFieldet(colTrupiNdryshimCmimSasi col, int lloji)
        {
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            serializusi.MaxJsonLength = 50000000;
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti(lloji);
            HfKonfAmb.Value = serializusi.Serialize(konf);

            HfColTrupMag.Value = serializusi.Serialize(col);
            colArtikujt colart = col.ktheColArtikuj();
           
            HfColArt.Value = serializusi.Serialize(colart);

            HfColNjesAdminis.Value = serializusi.Serialize(col.ktheColMag(DbCore.mySessionObjects.ktheIdPerdoruesi(Session)));
            HfColNjesiArt.Value = serializusi.Serialize(col.ktheColNjesiArt());
            return;

        }

        private void konfigGrid()
        {
            string emriKomponentes = "Shto_RegjistrimNdryshimCmimSasi.aspx";
            DbCore.DbAdmin.clsKomponente oKomponente = new DbCore.DbAdmin.clsKomponente(emriKomponentes);
            DbCore.DbShare.clsKonfigurimAmbjenti konfigurimi = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konfigurimi.mbushKonfigDefaultKomponentes(oKomponente.IdKomponente, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.colGridaTrupi trupiGrides = new DbCore.DbAdmin.colGridaTrupi(oKomponente.IdKomponente, konfigurimi.IdKonfigAmbjente, (int)hfState["idGjuha"]);
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            HfGridCol.Value = serializusi.Serialize(trupiGrides);
        }




        private void mbushComboKonfigurimet(bool mod, ResourceManager rm, CultureInfo ci, int idGjuha, int idNdermarje, int idPerdoruesi)
        {
            DbCore.DbShare.colKonfigurimAmbjenti colKonfig = new DbCore.DbShare.colKonfigurimAmbjenti();
            int idKategori = 95;//ndryshim sasi cmim
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
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate();
                ruajRegjistrimNdryshimCmimSasi(1, false);
                mbushHiddenFieldet(new colTrupiNdryshimCmimSasi(), 1);
            }
            if (e.Item.Name == "Draft")
            {
                Page.Validate();
                ruajRegjistrimNdryshimCmimSasi(0, false);// 0 = statusi i dokumentit(ne kete rast statusi eshte draft)
                mbushHiddenFieldet(new colTrupiNdryshimCmimSasi(), 1);
            }
            if (e.Item.Name == "PrintPreview")
            {

                //if (hfShtimModifikim.Value.ToString() == "modifikim")
                //{
                //    string id = Request.QueryString["id"];
                //    DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi clsKoka = new DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi();
                //    clsKoka.mbushKokaMagazinaSipasID(int.Parse(id));
                //    DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                //    konf.mbushKonfiguriminMeID(clsKoka.IdKonfigAmbjente);
                //    //if (konf.KodKonfigAmbjente == "FH" || konf.KodKonfigAmbjente == "FD")
                //    //{
                //    Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=38&idDokumenti=" + clsKoka.IdKokaMagazina + "&printo=false&raportdyte=jo&iddesign=" + cmbFormatiPrintimit.Value;
                //    //Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idRaporti + "&idDokumenti=" + kokeShtije.IdShitjeKoka + "&printo=true&raportdyte=jo&iddesign=" + cmbFormatiPrintimit.Value;
                //    //}
                //}
                //else
                //{
                //    Page.Validate();
                //    ruajRegjistrimMagazine(1, false);
                //    mbushHiddenFieldet(new colTrupiNdryshimCmimSasi(), 1, 0, 0);
                //}
            }
            if (e.Item.Name == "RuajPrint")
            {
                Page.Validate();
                ruajRegjistrimNdryshimCmimSasi(1, true);
                mbushHiddenFieldet(new colTrupiNdryshimCmimSasi(), 1);

            }
        }

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            pergjigja.Text = "";
            DbCore.DbRegjistrim.colTrupiMagazina tr = new DbCore.DbRegjistrim.colTrupiMagazina();
            bool rivleresim = false;
            clsKokaNdryshimCmimSasi kokam = new clsKokaNdryshimCmimSasi();
            kokam.IdKoka = int.Parse(Request.QueryString["id"]);
            kokam.mbushKokaNdryshimCmimSasiSipasID(kokam.IdKoka);
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            bool lidhur = kokam.eshteILidhur();

            if (lidhur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("vepBankaMsgDokEshteILidhurNukFshihet", cultinf), pnlMesazhi);
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

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(kokam.DtDok, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, KategoriDokumenti.NdryshimSasiCmim, kokam.IdKonfigAmbjente))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                return;
            }

            clsKokaMagazina kok = new clsKokaMagazina();
            kok.mbushKokaMagazinaSipasIDGjenerues(kokam.IdKoka, 1, kokam.IdKonfigAmbjente);
            if (kok.IdKokaMagazina != 0)
            {
                kok.mbushTrupMagazine(false);
                colArtikujt coleksistues1 = new colArtikujt(kok.IdKokaMagazina, new clsDatabaseInventari());
                int i2 = 0;
                foreach (clsTrupiMagazina trup in kok.OcolTrupiMagazina)
                {
                    if (trup.IdLlojVeprimi == 1)
                        trup.Element = coleksistues1[i2];
                    i2++;
                }
                mesazh = kok.kontrolloGjendjeNeFshirje(new DbCore.DbRegjistrim.colTrupiMagazina(), 0);
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    return;
                }
            }
            if (kok.IdKokaMagazina != 0 && hfKontrollRivleresim.Value.ToLower() == "true" && kok.IdStatusDok != 0)
                if (kok.rivleresim())
                {
                    rivleresim = true; tr.mbushGjitheTrupiMagazinaNgaKoka(kok.IdKokaMagazina); trupat.AddRange(tr);
                }


            kokam.IdPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            mesazh = kokam.fshi();
            //Session.Add("trupat", trupat);
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, trupat);
            if (mesazh.Status)
            {
                if (rivleresim)
                {
                    clsMenuInfo.ShtoPyetje(MenuInfo, rm.GetString("regjMagVeprimiSjellNdryshimNeCmimDalje", cultinf), pnlMesazhi, (int)hfState["idGjuha"]);
                    pergjigja.Text = "fshi";
                    pergjigja.ClientVisible = false;
                }
                else //if (pergjigja.Text != "Dokumenti eshte i lidhur dhe nuk mund te fshihet!" && pergjigja.Text != "Nuk mund te kryeni veprime sepse periudha eshte e kycur!")
                {
                    Response.Redirect("RegjistrimNdryshimCmimSasi.aspx?fshi=po");
                    this.status1.Value = "true";
                    return;
                }
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                this.status1.Value = "false";
                return;
            }
        }

        /// <summary>
        /// Ruan/Modifikon nje objekt clsKokaNdryshimCmimSasi.
        /// Therret funksionin <see cref="krijoRegjistrimMagazine"/>
        /// Therret funksionin <see cref="DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi.ruaj"/> ose <see cref="DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi.modifiko"/>
        /// </summary>
        /// <param name="statusDokumenti">Statusi me te cilin po ruhet dokumenti (I rregullt apo draft)</param>
        private void ruajRegjistrimNdryshimCmimSasi(int statusDokumenti, bool printo)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            colKokaNdryshimCmimSasi regjistrime = new colKokaNdryshimCmimSasi();
            int meKontabilizim;
            if (Page.IsValid == false)
                return;
            string shfaqmesazhapolupe = "jo";
            string shfaqmesazhapolupemag = "jo";
            bool rivleresim = false;
            string mesazhinformues = "";

            bool eshteOwn = (bool)hfState["OwnShop"];
            colTrupiMagazina tr = new colTrupiMagazina();
            if (isValidRegjistrimNdryshimSasiCmim(statusDokumenti))
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);


                int idGjuha = mySessionObjects.ktheGjuhe(Session);
                try
                {

                    regjistrime.Add(krijoRegjistrimNdryshimCmimSasi(statusDokumenti, out mesazhinformues, idGjuha));

                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                    status1.Value = "false";
                    return;
                }

                int idPeriudheZgjedhur = DbCore.mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha;

                foreach (clsKokaNdryshimCmimSasi regjistrim in regjistrime)
                {
                    if (!int.TryParse(hfKontabilizimi.Value, out meKontabilizim))
                        throw new Exception(rm.GetString("msgGabimGjateKonvertimitTeHFKontabilizim", cultinf));
                    if (regjistrim.OcolTrupiNdryshimCmimSasi.Count == 0)
                    {
                        status1.Value = "false";
                        return;
                    }
                    bool gjenerodokmag = clsAlternativaKushti.getAlternativa(regjistrim.IdKonfigAmbjente, "GJDM") == "Po";
                    if (statusDokumenti == 0)
                        meKontabilizim = 0;
                    string pershkrimFK;
                    if (regjistrim.Pershkrimi != String.Empty)
                        pershkrimFK = regjistrim.Pershkrimi;
                    else

                        pershkrimFK = Shto_RegjistrimNdryshimCmimSasi.pershkrimDaljeFK;
                    DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), "Shto_RegjistrimNdryshimCmimSasi.aspx");
                    if (hfShtimModifikim.Value == "shtim")
                    {
                        if ((statusDokumenti == 1 && !tedrejtaInfo.DShtim) || (statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft))
                        //if (!tedrejtaInfo.DShtim)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", cultinf), pnlMesazhi);
                            status1.Value = "false";
                            return;
                        }

                        mesazh = regjistrim.ruaj(meKontabilizim, hfNrAutoShitje, idPeriudheZgjedhur, pershkrimFK, out shfaqmesazhapolupe, eshteOwn, out shfaqmesazhapolupemag, gjenerodokmag, rm, cultinf);
                        DbCore.DbRegjistrim.clsKokaMagazina mag = new DbCore.DbRegjistrim.clsKokaMagazina();
                        mag.mbushKokaMagazinaSipasIDGjenerues(regjistrim.IdKoka, 1, regjistrim.IdKonfigAmbjente);
                        if (mag.IdKokaMagazina != 0 && hfKontrollRivleresim.Value.ToLower() == "true" && statusDokumenti != 0)
                            if (mag.rivleresim())
                            {
                                rivleresim = true;
                                tr.mbushGjitheTrupiMagazinaNgaKoka(mag.IdKokaMagazina);
                                trupat.AddRange(tr);
                            }
                    }
                    else if (hfShtimModifikim.Value == "modifikim")
                    {
                        if ((statusDokumenti == 1 && !tedrejtaInfo.DMod) || (statusDokumenti == 0 && !tedrejtaInfo.DModifikimDraft))
                        //if (!tedrejtaInfo.DMod)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", cultinf), pnlMesazhi);
                            status1.Value = "false";
                            return;
                        }

                        regjistrim.IdKoka = int.Parse(Request.QueryString["id"]);
                        DbCore.DbRegjistrim.clsKokaMagazina mag = new DbCore.DbRegjistrim.clsKokaMagazina();
                        if (hfKontrollRivleresim.Value.ToLower() == "true")
                        {

                            mag.mbushKokaMagazinaSipasIDGjenerues(regjistrim.IdKoka, 1, regjistrim.IdKonfigAmbjente);
                            if (mag.IdKokaMagazina != 0 && statusDokumenti != 0)
                                if (mag.rivleresim())
                                {
                                    rivleresim = true;
                                    tr.mbushGjitheTrupiMagazinaNgaKoka(mag.IdKokaMagazina);
                                    trupat.AddRange(tr);
                                }
                        }

                        bool lidhur = regjistrim.eshteILidhur();
                       
                        DbCore.DbQendraKosto.clsKokaQendraKosto qend = new DbCore.DbQendraKosto.clsKokaQendraKosto();
                        clsKokaFleteKontabel kok = new clsKokaFleteKontabel();

                        kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 95);

                        qend.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);


                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = rm.GetString("msgDokumentiEshteILidhur", cultinf);
                            status1.Value = "false";
                        }
                        else
                        {
                            if (lidhur == true)
                                mesazh = regjistrim.modifiko(meKontabilizim, true, pershkrimFK, out shfaqmesazhapolupe, eshteOwn, qend.ColTrupi, out shfaqmesazhapolupemag, gjenerodokmag, rm, cultinf);
                            else
                                mesazh = regjistrim.modifiko(meKontabilizim, false, pershkrimFK, out shfaqmesazhapolupe, eshteOwn, qend.ColTrupi, out shfaqmesazhapolupemag, gjenerodokmag, rm, cultinf);
                        }
                        if (hfKontrollRivleresim.Value.ToLower() == "true")
                        {
                            mag.mbushKokaMagazinaSipasIDGjenerues(regjistrim.IdKoka, 1, regjistrim.IdKonfigAmbjente);
                            if (mag.IdKokaMagazina != 0 && statusDokumenti != 0)
                                if (mag.rivleresimPas())
                                {
                                    rivleresim = true;
                                    tr.mbushGjitheTrupiMagazinaNgaKoka(mag.IdKokaMagazina);
                                    trupat.AddRange(tr);
                                }
                        }
                    }

                    DbCore.mySessionObjects.ruajTrupatNeSession(Session, trupat);
                    pergjigja.Text = "ruaj";
                    if (mesazh.Status == true)
                    {

                        hfqkmesazhi.Value = shfaqmesazhapolupe;
                        if (shfaqmesazhapolupe != "jo")
                        {
                            clsKokaFleteKontabel kok = new clsKokaFleteKontabel(regjistrim.IdKoka, 95);

                            hfUrl.Value = "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente;
                        }

                        hfShtimModifikim.Value = "shtim";
                        percaktoTemplateMenu(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session), idNdermarrje, ASPxMenu1);
                        //if (printo)
                        //    Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=38&idDokumenti=" + regjistrim.IdKoka + "&printo=true&raportdyte=jo&iddesign=" + cmbFormatiPrintimit.Value;
                        hfShtimModifikim.Value = "shtim";
                        if (rivleresim)
                            clsMenuInfo.ShtoPyetje(MenuInfo, mesazhinformues + rm.GetString("regjMagVeprimiSjellNdryshimNeCmimDalje", cultinf), pnlMesazhi, (int)hfState["idGjuha"]);
                        else
                            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgDokumentiURuajtMeSukese", cultinf) + mesazhinformues, pnlMesazhi);

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
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <param name="klienti">Klienti</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi</returns>
        private clsKokaNdryshimCmimSasi krijoRegjistrimNdryshimCmimSasi(int statusDokumenti, out string mesazhinformues, int idGjuha)
        {
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbShare.clsKonfigurimAmbjenti clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();            
            if (cmbKonfigurimi.Text != "")
                clsKonf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            else
                clsKonf.mbushKonfigDefaultKomponentes(545, idNdermarrje);

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(dteDtDok.Date, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, KategoriDokumenti.NdryshimSasiCmim, clsKonf.IdKonfigAmbjente))
                throw new Exception(MessagesResource.Messages["msgPeriodIsClosed"]);

            mesazhinformues = "";
            
            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, null, ASPxSplitter1, null);

            hfNrAutoShitje = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");

            clsKokaNdryshimCmimSasi koka = new clsKokaNdryshimCmimSasi();

            int idnivel = int.Parse(cmbLloji.Value.ToString());
            
            koka = krijoRegjistrimNdryshimCmimSasi(statusDokumenti, clsKonf, idnivel, ruajTrupinNdryshimSasiCmim(), out mesazhinformues, idGjuha);

            return koka;
        }

        /// <summary>
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi ne rastin kur kemi hyrje
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <param name="eshteTransferim">Tregon nese eshte transferim apo jo</param>
        /// <param name="klienti">Klienti</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi</returns>
        private clsKokaNdryshimCmimSasi krijoRegjistrimNdryshimCmimSasi(int statusDokumenti, DbCore.DbShare.clsKonfigurimAmbjenti clsKonf, int idnivel, colTrupiNdryshimCmimSasi coltrupi, out string mesazhinformues, int idGjuha)
        {
            clsKokaNdryshimCmimSasi mag = new clsKokaNdryshimCmimSasi();
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            mesazhinformues = "";
            int idnderviti = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
            int idmagazina = 0, idgrup = 0, idgrup2 = 0, idgrup3 = 0;

            string magazina = "", pershkrimi = "";

            if (btneMagazina.Text != "")
            {
                idmagazina = int.Parse(btneMagazina.Value.ToString());
                magazina = btneMagazina.Text;
            }
            if (cmbGrup1.Text != "")
                idgrup = int.Parse(cmbGrup1.Value.ToString());
            if (cmbGrup2.Text != "")
                idgrup2 = int.Parse(cmbGrup2.Value.ToString());
            if (cmbGrup3.Text != "")
                idgrup3 = int.Parse(cmbGrup3.Value.ToString());
            if (txtPershkrimi.Text != "")
                pershkrimi = txtPershkrimi.Text;


            int idperdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            int iddege = 0;
            if (cmbDegeAdministrative.Text != "")
                iddege = int.Parse(cmbDegeAdministrative.Value.ToString());
            clsLlogari llog = new clsLlogari(cmbLlogariKunderParti.Text, idNdermarrje);

            int id = 0; clsKokaRezervime kokaekzistuezerez = new clsKokaRezervime();
            if (hfShtimModifikim.Value == "modifikim" && Request.QueryString["id"] != null)
            {
                id = int.Parse(Request.QueryString["id"]);
                kokaekzistuezerez.mbushKokaRezervimiSipasIDGjenerues(id, 2, clsKonf.IdKonfigAmbjente);
            }
            bool gjenerodokmag = clsAlternativaKushti.getAlternativa(clsKonf.IdKonfigAmbjente, "GJDM") == "Po";
            DbCore.DbShare.clsKonfigurimAmbjenti konfmag = new DbCore.DbShare.clsKonfigurimAmbjenti(clsKonf.IdKonfigurimi, idGjuha);
            DbCore.clsMesazh mesazh = mag.krijoNdryshimCmimSasi(id, idnivel, clsKonf.IdKonfigAmbjente, idmagazina, magazina, dteDtDok.Date, txtNrDok.Text, double.Parse(txtVlefta.Text), statusDokumenti, idNdermarrje, idnderviti, idperdoruesi, idperdoruesi, dteDtRegjistrimi.Date, iddege, cmbDegeAdministrative.Text, llog.IdLlogari, cmbLlogariKunderParti.Text, idgrup, idgrup2, idgrup3, pershkrimi, coltrupi, new clsKokaFleteKontabel(), out mesazhinformues, true, ref gjenerodokmag, konfmag);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
            return mag;
        }



        private colTrupiNdryshimCmimSasi ruajTrupinNdryshimSasiCmim()
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);
            colTrupiNdryshimCmimSasi trupat = new colTrupiNdryshimCmimSasi();
            int idMagTemp = -1;
            bool isMagENjejte = true;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            for (int i = 0; i < dokumenti.Length; i++)
            {
                clsTrupiNdryshimCmimSasi trup = new clsTrupiNdryshimCmimSasi(idNdermarrje, idPerdorues, (Dictionary<string, object>)dokumenti[i]);
                if (trup.IdArtikulli > 0) //ky kusht duhet pare kur te shtohen makrot
                {
                    if (trup.IdMag == -1)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgMagazinaNukEkziston", cultinf), pnlMesazhi, LoadingPanel);
                        return new colTrupiNdryshimCmimSasi();
                    }
                    if (idMagTemp == -1)
                        idMagTemp = trup.IdMag;
                    else
                        if (isMagENjejte && trup.IdMag != idMagTemp)
                            isMagENjejte = false;

                    trupat.Add(trup);
                }
            }
            btneMagazina.Text = "";

            if (isMagENjejte && trupat.Count > 0)
            {
                  string kodMag = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiD(trupat[0].IdMag, idPerdorues);

                btneMagazina.Text = kodMag;

            }
            return trupat;
        }



        /// <summary>
        /// Kontrollon nese jane plotesuar te gjitha fushat e detyrueshme te kokes se dokumentit
        /// </summary>
        private bool isValidRegjistrimNdryshimSasiCmim(int draft)
        {
            CultureInfo cultinf = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            bool isValid;
            isValid = true;
            int idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);


            if (this.dteDtRegjistrimi.Text == "")
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, STR_zgjidhniDtRegj, pnlMesazhi, LoadingPanel);
                
                return isValid;
            }
            if (this.dteDtDok.Text == "")
            {
                isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhniNjeDateDokumenti", cultinf), pnlMesazhi, LoadingPanel);
                 return isValid;
            }
            if (dteDtDok.Date.Year != new DbCore.DbAdmin.clsNdermarrjeViti(DbCore.mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", cultinf), pnlMesazhi);
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
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKjoLlogariNukEkziston", cultinf), pnlMesazhi);
                    return false;
                }
                else if (!clsLlogari.eshteLlogariAktive(cmbLlogariKunderParti.Text, idNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKjoLlogariNukEshteAktive", cultinf), pnlMesazhi);
                    return false;
                }
                return true;
            }


            if (btneMagazina.Text != "")
            {
                DbCore.DbRegjistrim.clsNjesiAdministrative mag = new clsNjesiAdministrative(btneMagazina.Text, idNdermarrje, idPerdorues);
                if (mag.IdNjesiAdministrative == -1)
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgMagazinaNukEkziston", cultinf), pnlMesazhi, LoadingPanel);
                    return isValid;
                }
                else
                {
                    mag = new clsNjesiAdministrative(btneMagazina.Text, idNdermarrje, idPerdorues);
                    if (mag.Aktiv == false)
                    {
                        isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgMagazinaNukEshteAktive", cultinf), pnlMesazhi, LoadingPanel);
                         return isValid;
                    }
                }
            }
            if (this.cmbDegeAdministrative.Text != "")
            {
                DbCore.DbRegjistrim.clsDegeAdministrative dege = new clsDegeAdministrative(cmbDegeAdministrative.Text, idNdermarrje);
                if (dege.IdDegeAdministrative == -1)
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKjoDegeAdministrativeNukEkziston", cultinf), pnlMesazhi);
                    return isValid;
                }
                else
                {
                    dege = new clsDegeAdministrative(cmbDegeAdministrative.Text, idNdermarrje);
                    if (dege.Aktiv == false)
                    {
                        isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKjoDegeAdministrativeNukEshteAktive", cultinf), pnlMesazhi);
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
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbLlogariKunderParti,e);
                }
            }
        }

        protected void cmbLlogariKunderParti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].ToString().Contains("cmbLlogariKunderParti"))
                {
                    ConfigureAspxComboBox.mbushComboLlogariaPaKolona(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbLlogariKunderParti,e);
                }
            }
        }
    }
}