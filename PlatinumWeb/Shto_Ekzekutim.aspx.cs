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
using DbCore.DbKontabiliteti;
using DbCore.DbProdhimi;
using System.Collections;
using DbCore.DbInventari;
using System.Globalization;
using System.Resources;
using DbCore.DbShare;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Types;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Messages;

namespace PlatinumWeb
{
    public partial class Shto_Ekzekutim : MyPageBase
    {
        /// <summary>
        /// konstante per mesazhin e trupit nuk duhet te jete bosh
        /// </summary>
        //private const string STR_TrupiIDokumentitNukDuhetTeJeteBosh = "Trupi i dokumentit nuk duhet të jetë bosh!";
        //private const string STR_DokumentiEshteILidhurDheNukMundTeFshihet = "Dokumenti është i lidhur dhe nuk mund të fshihet!";
        /// <summary>
        /// konstante per dokumenti eshte i lidhur
        /// </summary>
        //private const string STR_DokumentiEshteILidhur = "Dokumenti është i lidhur";
        /// <summary>
        /// konstante per magazina nuk ekziston
        /// </summary>
        //private const string STR_MagazinaNukEkziston = "Magazina nuk ekziston!";
        /// <summary>
        /// konstante per zgjidhni nje date regjistrimi
        /// </summary>
        //private const string STR_ZgjidhniNjeDateRegjistrimi = "Zgjidhni nje datë regjistrimi!";
        /// <summary>
        /// konstante per zgjidhni nje date dokumenti
        /// </summary>
        //private const string STR_ZgjidhniNjeDateDokumenti = "Zgjidhni një datë dokumenti!";
        /// <summary>
        /// konstante per data nuk i perket vitit ushtrimor
        /// </summary>
        //private const string STR_DataNukIPerketVititUshtrimorTeZgjedhur = "Data nuk i përket vitit ushtrimor të zgjedhur";
        /// <summary>
        /// konstante per magazina nuk ekziston
        /// </summary>
        //private const string STR_KjoMagazineNukEkziston = "Kjo magazinë nuk ekziston!";
        /// <summary>
        /// konstante per magazina nuk eshte aktive
        /// </summary>
        //private const string STR_KjoMagazineNukEshteAktive = "Kjo magazinë nuk është aktive!";
        /// <summary>
        /// perdoret per te vendosur theme
        /// </summary>
        /// <param name="source">derguesi</param>
        /// <param name="e">argumentat</param>

        private  const string komponente = "Shto_Ekzekutim.aspx";
        private string guidString;
        private const string emerGride = "grid_faturat";



        /// <summary>
        /// mbush te dhenat kur faqja ben loadim
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            CultureInfo ci;
            int idPerdoruesi, idGjuha, idNdermarrje, idViti, idNdermarrjeVit;
            bool eshteOwn;
            ASPxGridView grid_faturat = null;
            if (!IsPostBack)
            {
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
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
                grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
                DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
                idNdermarrje = DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = DbCore.mySessionObjects.ktheIdVitNdermarrje(Session);
                idGjuha = DbCore.mySessionObjects.ktheGjuhe(Session);
                idNdermarrjeVit = DbCore.mySessionObjects.ktheNdermarrjeVit(Session);
                eshteOwn = DbCore.mySessionObjects.merrEshteOwnSesioni(Session);
                ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("guidString", guidString);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("eshteOwn", eshteOwn);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("idPeriudha", periudha.IdPeriudha);
                //DbCore.DbAdmin.clsPerdorues per = new DbCore.DbAdmin.clsPerdorues(idPerdoruesi);
                //string hapur = per.InfoHapur.ToString();
                hfHapurMbyllur.Value = DbCore.DbAdmin.clsPerdorues.ktheInfoHapur(idPerdoruesi);
                mbushHiddenFieldMePerkthime();
                EmrateTabeve();

                if (hfShtimModifikim.Value == "")
                {
                    if (Request.QueryString["shtim_modifikim"] == "shtim" || String.IsNullOrEmpty(Request.QueryString["shtim_modifikim"]))
                        hfShtimModifikim.Value = "shtim";
                    else
                        hfShtimModifikim.Value = "modifikim";
                }

                if (hfShtimModifikim.Value == "shtim")
                    konfiguroVleraFillestareShto(idGjuha, idNdermarrje, rm, ci, idPerdoruesi, idNdermarrjeVit, DbCore.mySessionObjects.merrPeriudheKontabel(Session));
                else
                    if (hfShtimModifikim.Value == "modifikim")
                    konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, rm, ci, idNdermarrjeVit);

                DbCore.DbRegjistrim.colNjesiAdministrative colMagazinat = new DbCore.DbRegjistrim.colNjesiAdministrative();
                colMagazinat.mbushGjitheNjesiAdministrativeAktiveSipasLlojit(idNdermarrje, idPerdoruesi, 1);
                JavaScriptSerializer serializusi = new JavaScriptSerializer();
                serializusi.MaxJsonLength = 50000000;
                hfTmpColMag.Value = serializusi.Serialize(colMagazinat);

                DbCore.mySessionObjects.ruajArtProdhNeSesion(Session, DbCore.DbInventari.colArtikujt.merrSipasArtikujNdermarrjesAndAutorizimePerLupeArtikulliProdhimi(idNdermarrje, idPerdoruesi, false, -1, false, false, MessagesResource.Messages["postStringTvsh"], false, false, "", -1, -1, -1));
                DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();
                
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                GridUtil.perktheButonaGride(hfState, ci);
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Info Artikulli");
                hfTeDrejtaInfoArt.Value = tedrejtaInfo.DAmb.ToString();
                AspxWebControlUtils.perkthePopUp(popFshi, MessagesResource.Messages["labelKujdes"], lblMsgbox, MessagesResource.Messages["labelAdministrimiMsgJeniSigurt"], ButtonCancel, MessagesResource.Messages["labelAnullo"]);
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaArkiva.aspx");
                hfTeDrejtaArtImazhe.Value = tedrejtaInfo.DAmb.ToString();
                mbushGrideFaturatngaDB(idNdermarrjeVit, idNdermarrje, idPerdoruesi);
                konfiguroGrideFaturat(false, idGjuha, idNdermarrje, idPerdoruesi);
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, emerGride, grid_faturat, cmbKonfigurimi.Text.Split(';')[0], "806", idGjuha);

            }
            else
            {
                guidString = (string)hfState["guidString"];
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                eshteOwn = (bool)hfState["eshteOwn"];
                grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
                merrGrideFaturatngaSession(idNdermarrjeVit, idNdermarrje, idPerdoruesi);
                konfiguroGrideFaturat(false, idGjuha, idNdermarrje, idPerdoruesi);
            }
           clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, idNdermarrje, emerGride, int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
         
            grid_faturat.PercaktoTitlePanel(this, MenuInfo, pnlMesazhi, hfState, idPerdoruesi, idNdermarrje, idViti, idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()),  komponente, rm, ci);
            percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1, idGjuha);
            Container.Attributes["src"] = "";
        }

        /// <summary>
        /// mbush fushat gjate modifikimit te dokumentit
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idPerdoruesi">id e perdoruesit</param>
        /// <param name="idNderViti">id e ndermarje vitit</param>
        /// <param name="idNdermarrje"> id e ndermarjes</param>
        /// <param name="koka">koka e ekzekutimit</param>
        public void MerrTedhenat(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, clsKokaEkzekutim koka)
        {
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente, idGjuha);
            cmbKonfigurimi.Text = konf.KodKonfigAmbjente;
            //  hfKonffillestar.Value = String.Format("{0};{1}", konf.KodKonfigAmbjente, konf.PershkrimKonfigAmbjente);
            hfKonffillestar.Value = konf.KodKonfigAmbjente;
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup1);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup1, idNdermarrje, 1, koka.IdKonfigAmbjente, idPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup2);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup2, idNdermarrje, 2, koka.IdKonfigAmbjente, idPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup3);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup3, idNdermarrje, 3, koka.IdKonfigAmbjente, idPerdoruesi);

            if (koka.IdMagProdukti != 0)
                btneMagazina.Text = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(koka.IdMagProdukti);
            if (koka.IdMagReceptura != 0)
                btneMagazin2a.Text = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(koka.IdMagReceptura);
            txtNrDok.Text = koka.NrDok;
            dteDtDok.Date = koka.DtDok;
            dteDtRegjistrimi.Date = koka.DtRegj;
            txtShenime.Text = koka.Shenime;
            txtTotali.Text = koka.Totali.ToString();
            cmbGrup1.Value = koka.IdGrup1.ToString();
            cmbGrup2.Value = koka.IdGrup2.ToString();
            cmbGrup3.Value = koka.IdGrup3.ToString();
            if (koka.IdNjesiProdhimi > 0)
            {
                cmbNjesiProdhimi.Value = koka.IdNjesiProdhimi;
                cmbNjesiProdhimi.Text = clsNjesiProdhimi.ktheKodNjesiProdhimiSipasId(koka.IdNjesiProdhimi);
            }
            merrDokumentaKonvertuar(koka.IdKokaEkzekutim);
            DataTable dtlidhur = koka.merrIdsDokLidhur();
            hfLidhur.Value = (!(dtlidhur.Rows.Count == 0)).ToString();

            bool autorizimet = DbCore.DbProdhimi.clsKokaEkzekutim.kaAutorizime(koka.IdKokaEkzekutim, idPerdoruesi);
            hfAutorizimi.Value = autorizimet.ToString();
            if (!autorizimet)
                hfLidhur.Value = "True";

            AspxWebControlUtils.ShtoLidhje(idPerdoruesi, idViti, idNdermarrje, hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, idGjuha);
            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 806, "", -1, false);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));
        }

        private void merrDokumentaKonvertuar(int id)
        {
            ArrayList list = new ArrayList();
            colPlanifikimEkzekutim col = new colPlanifikimEkzekutim(id);
            foreach (clsPlanifikimEkzekutim konv in col)
            {
                list.Add(konv.IdPlanifikimi);
            }
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            hfPlanifikime.Value = serializusi.Serialize(list);
        }

        // <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime()
        {
            hfState.Set("MenuKokeDokumenti", MessagesResource.Messages["MenuKokeDokumenti"]);
            hfState.Set("MenuTrupDokumenti", MessagesResource.Messages["MenuTrupDokumenti"]);
            hfState.Set("MenuFundDokumenti", MessagesResource.Messages["MenuFundDokumenti"]);
            hfState.Set("msgGabimGjateTransferimitTeTeDhenave", MessagesResource.Messages["msgGabimGjateTransferimitTeTeDhenave"]);
            hfState.Set("msgNdodhiGabimGjateMarrjesSeTeDhenave", MessagesResource.Messages["msgNdodhiGabimGjateMarrjesSeTeDhenave"]);
            hfState.Set("msgKyArtikullNukIPerketKlasesProdhimOsePProces", MessagesResource.Messages["msgKyArtikullNukIPerketKlasesProdhimOsePProces"]);
            hfState.Set("msgArtikulliEshteInaktiv", MessagesResource.Messages["msgArtikulliEshteInaktiv"]);
            hfState.Set("msgSasiaAktualeDuhetTeJeteNr", MessagesResource.Messages["msgSasiaAktualeDuhetTeJeteNr"]);
            hfState.Set("msgSasiaAktualeNukMundTeJeteZero", MessagesResource.Messages["msgSasiaAktualeNukMundTeJeteZero"]);
            hfState.Set("msgFiroLigjoreDuhetTeJeteMeEVogelSeSasiaAktuale", MessagesResource.Messages["msgFiroLigjoreDuhetTeJeteMeEVogelSeSasiaAktuale"]);
            hfState.Set("msgZgjidhniLlojinEVeprimit", MessagesResource.Messages["msgZgjidhniLlojinEVeprimit"]);
            hfState.Set("roundPanelZgjidhArtikullin", MessagesResource.Messages["roundPanelZgjidhArtikullin"]);
            hfState.Set("msgZgjidhBurimin", MessagesResource.Messages["msgZgjidhBurimin"]);
            hfState.Set("msgLlojiVeprimitIPanjohur", MessagesResource.Messages["msgLlojiVeprimitIPanjohur"]);
            hfState.Set("msgKostojaDuhetTeJeteNumer", MessagesResource.Messages["msgKostojaDuhetTeJeteNumer"]);
            hfState.Set("msgKostojaNukMundTeJeteNegative", MessagesResource.Messages["msgKostojaNukMundTeJeteNegative"]);
            hfState.Set("msgFiroNumer", MessagesResource.Messages["msgFiroNumer"]);
            hfState.Set("msgFiroLigjoreDuhetTeJeteNumerNegativ", MessagesResource.Messages["msgFiroLigjoreDuhetTeJeteNumerNegativ"]);
            hfState.Set("msgZgjidhDokumentin", MessagesResource.Messages["msgZgjidhDokumentin"]);
            hfState.Set("msgKujdesRecepturatNdryshuanSipasDateSeZgjedhur", MessagesResource.Messages["msgKujdesRecepturatNdryshuanSipasDateSeZgjedhur"]);
            hfState.Set("msgZgjidhMagazinen", MessagesResource.Messages["msgZgjidhMagazinen"]);
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", MessagesResource.Messages["msgPoTransferohetTeDhenatShtypniPerseriRuaj"]);
            hfState.Set("msgShenoniNumrinEDokumentit", MessagesResource.Messages["msgShenoniNumrinEDokumentit"]);
            hfState.Set("msgZgjidhniNjeDateDokumenti", MessagesResource.Messages["msgZgjidhniNjeDateDokumenti"]);
            hfState.Set("msgZgjidhniNjeDateRegjstrimi", MessagesResource.Messages["msgZgjidhniNjeDateRegjstrimi"]);
            hfState.Set("msgShperndarjeNeQendratEKostos", MessagesResource.Messages["msgShperndarjeNeQendratEKostos"]);
            hfState.Set("msgDeshironiShperndarjeQendraKosto", MessagesResource.Messages["msgDeshironiTeBeniShperndarjenNeQendratEKostos"]);
            hfState.Set("msgNukKeniAutorizimPerTeRuajturKeteDok", MessagesResource.Messages["msgNukKeniAutorizimPerTeRuajturKeteDok"]);
            hfState.Set("msgNukKeniAutorizimPerTeFshireDok", MessagesResource.Messages["msgNukKeniAutorizimPerTeFshireDok"]);
            hfState.Set("msgZgjidhDetajimArtikulli", MessagesResource.Messages["msgZgjidhDetajimArtikulli"]);
            hfState.Set("msgKyDokumentPlanifikimiEshteZgjedhurNeGride", MessagesResource.Messages["msgKyDokumentPlanifikimiEshteZgjedhurNeGride"]);
            hfState.Set("msgZgjidhiniKonfiguriminEInfosSeArtikullit", MessagesResource.Messages["msgZgjidhiniKonfiguriminEInfosSeArtikullit"]);
            hfState.Set("msgZgjidhniArtikullin", MessagesResource.Messages["msgZgjidhniArtikullin"]);
            hfState.Set("msgVendosetVetArtikulli", MessagesResource.Messages["msgVendosetVetArtikulli"]);
            hfState.Set("regjisDokZgjidhDokPerTeBashkengjitur", MessagesResource.Messages["regjisDokZgjidhDokPerTeBashkengjitur"]);
            hfState.Set("msgDetajimiVendosurNukEkzistonDoniTaCelni", MessagesResource.Messages["msgDetajimiVendosurNukEkzistonDoniTaCelni"]);
            hfState.Set("msgDetajimMeKategoriTjeter", MessagesResource.Messages["msgDetajimMeKategoriTjeter"]);
            hfState.Set("msgDetajimJoLidhur", MessagesResource.Messages["msgDetajimJoLidhur"]);
            hfState.Set("msgPyetjePanjohur", MessagesResource.Messages["msgPyetjePanjohur"]);
            hfState.Set("msgKodiDateSkadenceDuhetFormat", MessagesResource.Messages["msgKodiDateSkadenceDuhetFormat"]);
            hfState.Set("msgSerialiVendosurNukITakonBlerjes", MessagesResource.Messages["msgSerialiVendosurNukITakonBlerjes"]);
            hfState.Set("msgDetajimiNukEkziston", MessagesResource.Messages["msgDetajimiNukEkziston"]);
            hfState.Set("msgArtikullpaDetajim", MessagesResource.Messages["msgArtikullpaDetajim"]);
            hfState.Set("msgShtoDetajim", MessagesResource.Messages["msgShtoDetajim"]);
        }

        /// <summary>
        /// Vendos emrat e tabeve ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateTabeve()
        {
            ASPxPageControl1.TabPages[0].Text = MessagesResource.Messages["produktetTab"];
            ASPxPageControl1.TabPages[1].Text = MessagesResource.Messages["labelRaportRecepturat"];
            ASPxNavBar1.Groups[0].Text = MessagesResource.Messages["menuPlanifikimProdhimi"];
        }

        public void btnJo_Click(object sender, EventArgs e)
        {
            //CacheLayer.GlobalCacheManager.MySessionCache["trupat"] = new DbCore.DbRegjistrim.colTrupiMagazina();
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
            //if (CacheLayer.GlobalCacheManager.MySessionCache["Fshi"]!=null && CacheLayer.GlobalCacheManager.MySessionCache["Fshi"].ToString() == "fshi")
            if (DbCore.mySessionObjects.merrFshiNgaSesioni(Session) != null && DbCore.mySessionObjects.merrFshiNgaSesioni(Session) == "fshi")
            {
                Response.Redirect("EkzekutimProdhimi.aspx?fshi=po");
                return;
            }
        }

        public void btnPo_Click(object sender, EventArgs e)
        {
            int idGjuha = (int)hfState["idGjuha"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //DbCore.DbRegjistrim.colTrupiMagazina  trupat = (DbCore.DbRegjistrim.colTrupiMagazina)CacheLayer.GlobalCacheManager.MySessionCache["trupat"];
            DbCore.DbRegjistrim.colTrupiMagazina trupat = DbCore.mySessionObjects.merrTrupatNgaSesioni(Session);
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh(true, MessagesResource.Messages["regjMagMesazhSuksesRivleresimi"]);
            //db.krijoManager();
            string fileLogPath = Server.MapPath("~/log/log.txt");
            DbCore.DbAdmin.clsLogRivleresimInventari log = new DbCore.DbAdmin.clsLogRivleresimInventari();
            try
            {
                log = new DbCore.DbAdmin.clsLogRivleresimInventari(idPerdoruesi, idNdermarrje);
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                throw new DbCore.MyException(MessagesResource.Messages["msgGabimGjateRuajtjesSeRivleresimitNeLog"]);
            }
            foreach (DbCore.DbRegjistrim.clsTrupiMagazina t in trupat)
            {
                //DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli();
                //art.mbushArtikull(t.IdArtikulli);
                //mesazh = art.rivleresimCmimiMesatar(t.IdMag, t.Data, DateTime.Today);
                //DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(t.IdArtikulli);
                mesazh = DbCore.DbInventari.clsArtikulli.rivleresimCmimiMesatar(t.IdArtikulli, DbCore.DbInventari.clsArtikulli.ktheMetodeKostoje(t.IdArtikulli), t.IdMag, t.Data, DateTime.Today,log,ci,rm, idNdermarrje, idPerdoruesi);
                if (!mesazh.Status)
                {
                    Response.Redirect("EkzekutimProdhimi.aspx?fshi=rivleresimjo");
                    return;
                }
            }
            //CacheLayer.GlobalCacheManager.MySessionCache["trupat"] = new DbCore.DbRegjistrim.colTrupiMagazina();
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, new DbCore.DbRegjistrim.colTrupiMagazina());
            if (mesazh.Status)
            {
                Response.Redirect("EkzekutimProdhimi.aspx?fshi=rivleresimpo");
                return;
            }
        }
        
        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="idGjuha"></param>
        private void percaktoTemplateMenu(int idPerdoruesi, int idViti, int idNdermarrje, ASPxMenu aSPxMenu1, int idGjuha)
        {
            DbCore.DbShare.colMenuItem menu = new DbCore.DbShare.colMenuItem(idGjuha);
            menu.merrMenuItemSipasKomponentes(idGjuha, komponente, idPerdoruesi, idNdermarrje, idViti, hfShtimModifikim.Value == "modifikim" ? false : true);
            clsKokaFleteKontabel kok = new clsKokaFleteKontabel();
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
                        kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 45);

                        if (kok.NrDukumentiKokaFleteKontabel != null)
                            aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = String.Format("javascript: myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&id={0}&numur={1}')", kok.IdKokaFleteKontabel, kok.NrDukumentiKokaFleteKontabel);
                        else
                        {
                            aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                        }
                    }

                } if (m.Name == "QendraKosto")
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
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myButtonClickLupa.LupaUniversal_Click('Shperndarje ne qendrat e kostos','LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + "',900,600)";
                            else
                            {
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;

                            }
                        }
                        else aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    }
                }
                if (m.Name == "Ruaj")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;

                EventHandler handlerPerRuajFilter = Ruaj_ASPxButton_Click;

                EventHandler handlerPerFshiFilter = FshiFilter_ASPxButton_Click;
                if (m.Name == "ItemFilter")

                    clsToolbarConfig.ShtoMenuItemPerFilter(this, aSPxMenu1, handlerPerRuajFilter, handlerPerFshiFilter);
                if (m.Name == "ItemFrame")
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
                //if (m.Name == "Arkiva")
                //{
                //    if (!DbCore.DbAdmin.clsNdermarrje.meArkive(idNdermarrje))
                //        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].Visible = false;
                //}
            }

            EventHandler handlerPerPo = btnPo_Click;
            EventHandler handlerPerJo = btnJo_Click;
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo, handlerPerPo, handlerPerJo);
            bool visible = clsFunksione.merrMenuVisibleDraft(hfShtimModifikim.Value == "modifikim" ? false : true, kok.IdStatusDokumenti);
            ASPxMenu1.Items.FindByName("Draft").ClientVisible = visible;
        }
        /// <summary>
        /// fshin filtrin e zgjedhur dhe aplikuar mbi gride, fshirje nga DB ku ai eshte ruajtur, dhe jo pastrimi i grides nga aplikimi i filtrit
        /// per kete do sherbeje Clear Filter ne fund te grides
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        public void FshiFilter_ASPxButton_Click(object sender, EventArgs e)
        {

            //kap item qe ka template ne menune e kesaj faqeje
            ASPxGridView grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            int idGjuha = (int)hfState["idGjuha"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idViti = (int)hfState["idViti"];
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;

            DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, emerGride, komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            filtra.mbushFilterPerGrideSipasKodit(cmbFiltra.Text, idNdermarrje, koka.IdGridaKoka);
            if (filtra.FiltraKodi != null)
            {
                DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
                filtra.IdPerdoruesi = idPerdoruesi;
                mesazh = filtra.fshi();
                //mbushComboBoxFiltra();
                clsToolbarConfig.mbushComboBoxFiltra(IdGjuha, idNdermarrje, emerGride, int.Parse(cmbKonfigurimi.Value.ToString()), komponente);                
                percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1, idGjuha);
                if (mesazh.Status == true)
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

                cmbFiltra.Text = "";

            }
        }
        /// <summary>
        /// sherben per te ruajtur filtrin e zgjedhur
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void Ruaj_ASPxButton_Click(object sender, EventArgs e)
        {
            //kap item qe ka template ne menune e kesaj faqeje

            ASPxGridView grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            int idGjuha = (int)hfState["idGjuha"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            int idViti = (int)hfState["idViti"]; int idfiltri = 0;
            MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
            ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
            DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, emerGride, komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbAdmin.clsFiltraGrida filtri = new DbCore.DbAdmin.clsFiltraGrida() { FiltraKodi = cmbFiltra.Text, FiltraShenime = cmbFiltra.Text, FiltraUniversal = false,
                GridaKokaId = koka.IdGridaKoka, FiltraVlera = grid_faturat.FilterExpression, IdPerdoruesi = idPerdoruesi,
                IdNdermarje = idNdermarrje, IdStatusDok = 1 };
            filtri.KoloneRenditje = GridUtil.ktheKolRenditjeNgaGridaPerRuajtje("IdDokumenti", grid_faturat);
            //System.Collections.ObjectModel.ReadOnlyCollection<GridViewDataColumn> kolona = grid_faturat.GetSortedColumns();
            //if (kolona.Count > 0)
            //{
            //    filtri.KoloneRenditje = kolona[0].FieldName;
            //    if (kolona[0].SortOrder == DevExpress.Data.ColumnSortOrder.Ascending)
            //        filtri.DrejtimRenditje = true;
            //    else
            //        filtri.DrejtimRenditje = false;
            //}
            //else
            //{
            //    filtri.KoloneRenditje = "IdDokumenti";
            //    filtri.DrejtimRenditje = true;
            //}

            DbCore.clsMesazh mesazh = GridUtil.ruajFiltra(idNdermarrje, idPerdoruesi, idGjuha, "grid_faturat", komponente, cmbFiltra.Text, grid_faturat.FilterExpression, grid_faturat, "IdDokumenti", int.Parse(cmbKonfigurimi.Value.ToString()), out idfiltri);

            clsToolbarConfig.mbushComboBoxFiltra(idGjuha, idNdermarrje, emerGride, int.Parse(cmbKonfigurimi.Value.ToString()), komponente);
            percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1, idGjuha);
            if (mesazh.Status == true)
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            else clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);

            cmbFiltra.Text = "";
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrjeVit"], (int)hfState["idNdermarrje"], ASPxMenu1, (int)hfState["idGjuha"]);
        }
        /// <summary>
        /// vendos daten default
        /// </summary>
        /// <param name="periudha"></param>
        private void vendosDataDefault(DbCore.DbAdmin.clsPeriudhaKontabel periudha)
        {
            DateTime sot = new DateTime();
            sot = DateTime.Today;
            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
                dteDtDok.Value = DateTime.Today;
            else
                dteDtDok.Value = periudha.FillimiPeriudha;
        }

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idNdermarrjeVit"></param>
        /// <param name="periudha"></param>
        private void konfiguroVleraFillestareShto(int idGjuha, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf, int idPerdoruesi, int idNdermarrjeVit, DbCore.DbAdmin.clsPeriudhaKontabel periudha)
        {
            dteDtDok.Date = DateTime.Today;
            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            dteDtRegjistrimi.Date = DateTime.Today;
            vendosDataDefault(periudha);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazin2a);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbNjesiProdhimi);
            ConfigureAspxComboBox.mbushComboNjesiProdhimi(idNdermarrje, cmbNjesiProdhimi, false);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 45, rm, cultinf, idGjuha);
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
            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btneMagazin2a, idPerdoruesi, true, 1,true);
            colProduktProdhimi col = new colProduktProdhimi();
            colRecepturaProdhimi colrec = new colRecepturaProdhimi();
            //Session.Add("Recepturat", colrec);
            DbCore.mySessionObjects.ruajRecepturatNeSession(Session, colrec);
            //Session.Add("Produktet", col);
            DbCore.mySessionObjects.ruajProdukteProdhimiNeSession(Session, col);
            //  mbushListeArtikujPerberes();
            //  mbushListeRecepturash();
            //inicializoGridFaturat(idNdermarrjeVit, idNdermarrje, idPerdoruesi);
            konfiguroGrideFaturat(true, idGjuha, idNdermarrje, idPerdoruesi);
            int idMonedheZgjedhur = DbCore.clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 806, "", -1, true);
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new DbCore.DbShare.clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            DbCore.DbShare.clsFormatKonfigTrup formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
            hfState.Set("formatMonedhe", serializusi.Serialize(formatMonedhe));
        }

        /// <summary>
        /// Vendos vlerat default kur po behet modifikim
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNderViti"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idNdermarrjeVit"></param>
        private void konfiguroVleraFillestareModifiko(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, System.Resources.ResourceManager rm, System.Globalization.CultureInfo cultinf, int idNdermarrjeVit)
        {//mbush kombot dhe gridat
            // txtNrDok.Enabled = false;

            AspxWebControlUtils.vendosDateEditMask(dteDtDok);
            AspxWebControlUtils.vendosDateEditMask(dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(cmbNjesiProdhimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazin2a);
            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btneMagazina, idPerdoruesi, false, 1,true);
            ConfigureAspxComboBox.mbushComboMagazinat(idNdermarrje, btneMagazin2a, idPerdoruesi, false, 1,true);
            ConfigureAspxComboBox.mbushComboNjesiProdhimi(idNdermarrje, cmbNjesiProdhimi, false);
            ConfigureAspxComboBox.mbushComboKonfigurimeshSipasKategorise(idPerdoruesi, idNdermarrje, cmbKonfigurimi, 45, rm, cultinf, idGjuha);
            cmbKonfigurimi.TextFormatString = "{0}";
            int id = int.Parse(Request.QueryString["id"]);
            clsKokaEkzekutim kok = new clsKokaEkzekutim(id);
            if (kok != null)
            {
                MerrTedhenat(idGjuha, idPerdoruesi, idViti, idNdermarrje, kok);
            }

            mbushTrupin(kok);
            //inicializoGridFaturat(idNdermarrjeVit, idNdermarrje, idPerdoruesi);
            konfiguroGrideFaturat(true, idGjuha, idNdermarrje, idPerdoruesi);
        }

        /// <summary>
        /// Merr trupin ekzistues te dokumentit te magazines qe po modifikohet
        /// </summary>
        private void mbushTrupin(clsKokaEkzekutim koka)
        {
            colProduktProdhimi col = new colProduktProdhimi(koka.IdKokaEkzekutim);
            colNjesiteArtikulli colnjesi = new colNjesiteArtikulli();
            colNjesiAdministrative colmag = new colNjesiAdministrative();            
            List<Object> listaRecepturave = new List<Object>();
            List<Object> listaDetajimeveArtProdhim = new List<Object>();

            foreach (clsProduktProdhimi trup in col)
            {
                var recepturat = new List<Dictionary<String, Object>>();                
                trup.ColReceptura = new colRecepturaProdhimi(trup.Id);
                clsNjesiArtikulli njesi = new clsNjesiArtikulli(trup.IdNjesia);
                colnjesi.Add(njesi);
                clsNjesiAdministrative mag = new clsNjesiAdministrative(trup.IdMag);
                colmag.Add(mag);
                Dictionary<String, Object> detajimeArtProdhim = new Dictionary<string, object>();
                detajimeArtProdhim.Add("det1ArtProdhim", new clsDetajimArtikulli(trup.IdDetajim1));
                detajimeArtProdhim.Add("det2ArtProdhim", new clsDetajimArtikulli(trup.IdDetajim2));
                listaDetajimeveArtProdhim.Add(detajimeArtProdhim);
                foreach (clsRecepturaProdhimi rec in trup.ColReceptura)
                {
                    Dictionary<String, Object> receptura = new Dictionary<string, object>();
                    receptura.Add("rec", rec);
                    receptura.Add("det1", new DbCore.DbInventari.clsDetajimArtikulli(rec.IdDetajimi));
                    receptura.Add("det2", new DbCore.DbInventari.clsDetajimArtikulli(rec.IdDetajimi2));
                    receptura.Add("artikulli", new clsArtikulli(rec.IdArtikulli));
                    clsNjesiAdministrative mag1 = new clsNjesiAdministrative(rec.IdMag);
                    receptura.Add("mag", mag1);
                    if (rec.Lloji == 1)
                    {
                        clsNjesiArtikulli njesi1 = new clsNjesiArtikulli(rec.NjesiArtikull);
                        receptura.Add("njesi", njesi1);
                    }
                    else
                    {
                        switch (rec.NjesiArtikull)
                        {
                            case 1:
                                receptura.Add("njesi", new clsNjesiArtikulli("sec", "sec", 0, 0, 0, ""));
                                break;
                            case 2:
                                receptura.Add("njesi", new clsNjesiArtikulli("min", "min", 0, 0, 0, ""));
                                break;
                            case 3:
                                receptura.Add("njesi", new clsNjesiArtikulli("ore", "ore", 0, 0, 0, ""));
                                break;
                            case 4:
                                receptura.Add("njesi", new clsNjesiArtikulli("dite", "dite", 0, 0, 0, ""));
                                break;
                            default:
                                receptura.Add("njesi", new clsNjesiArtikulli("ore", "ore", 0, 0, 0, ""));
                                break;
                        }
                    }
                    recepturat.Add(receptura);
                }
                clsTrupiShitje tr = new clsTrupiShitje(trup.IdUrdherPorosi);
                trup.Shenime = tr.Shenime;                
                listaRecepturave.Add(recepturat);
            }
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            serializusi.MaxJsonLength = 50000000;
            this.HfColArt.Value = serializusi.Serialize(new { produktet = col, detajimet = listaDetajimeveArtProdhim, njesite = colnjesi, magazinat = colmag, idnjesiprodhim = 0, teDhenaRecepturash = listaRecepturave });
        }

        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            int idViti = idViti = (int)hfState["idViti"];
            int idGjuha = (int)hfState["idGjuha"];
            CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            bool eshteOwn = (bool)hfState["eshteOwn"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            if (e.Item.Name == "Ruaj")
            {
                Page.Validate();
                ruajRegjistrimEkzekutim(1, true, idViti, idNdermarrjeVit, idNdermarrje, ci, eshteOwn, idPerdoruesi, idGjuha);
            }
            if (e.Item.Name == "RuajPrint")
            {
                Page.Validate();
                ruajRegjistrimEkzekutim(1, true, idViti, idNdermarrjeVit, idNdermarrje, ci, eshteOwn, idPerdoruesi, idGjuha, true);
            }
            if (e.Item.Name == "Draft")
            {
                Page.Validate();
                ruajRegjistrimEkzekutim(0, true, idViti, idNdermarrjeVit, idNdermarrje, ci, eshteOwn, idPerdoruesi, idGjuha);
            }
            if (e.Item.Name == "PrintPreview")
            {
                if (hfShtimModifikim.Value.ToString() == "modifikim")
                {
                    string id = Request.QueryString["id"];
                    DbCore.DbProdhimi.clsKokaEkzekutim clsKoka = new DbCore.DbProdhimi.clsKokaEkzekutim();
                    clsKoka.mbushKokaEkzekutimSipasID(int.Parse(id));
                    DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                    konf.mbushKonfiguriminMeID(clsKoka.IdKonfigAmbjente);
                    //if (konf.KodKonfigAmbjente == "FH" || konf.KodKonfigAmbjente == "FD")
                    //{
                    Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=urdherPune&idDokumenti=" + clsKoka.IdKokaEkzekutim + "&printo=false";
                    //}
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
            int idGjuha = (int)hfState["idGjuha"];
            CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            clsKokaEkzekutim kokam = new clsKokaEkzekutim(int.Parse(Request.QueryString["id"]));
            clsMesazh mesazh = new clsMesazh();
            bool lidhur = kokam.eshteILidhur(); DbCore.DbRegjistrim.colTrupiMagazina trupat = new DbCore.DbRegjistrim.colTrupiMagazina();
            DbCore.DbRegjistrim.colTrupiMagazina tr = new DbCore.DbRegjistrim.colTrupiMagazina();
            bool rivleresim = false;
            if (lidhur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgDokLidhurNukFshihet"], pnlMesazhi);
                status1.Value = "false";
                return;
            }
            if (kokam.DtDok.Year != new DbCore.DbAdmin.clsNdermarrjeViti((int)hfState["idNdermarrjeVit"]).Viti)
            {
                status1.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgDataNukPerketVititUshtrimor"], pnlMesazhi);
                return;
            }
            int idNdermarrje = (int)hfState["idNdermarrje"];
            //DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(kokam.DtDok, idNdermarrje);
            //clsMesazh mesazhi = periudha.isPeriudheKycur();
            bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(kokam.DtDok, idNdermarrje);
            if (ekycur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriudhaEKycur"], pnlMesazhi);
                return;
            }

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(kokam.DtDok, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, KategoriDokumenti.EkzekutimProdhimi, kokam.IdKonfigAmbjente))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                return;
            }

            kokam.IdPerdoruesi = (int)hfState["idPerdoruesi"];
            clsKokaMagazina kokhyrje = new clsKokaMagazina();
            clsKokaMagazina kokdalje = new clsKokaMagazina();
            kokdalje.mbushKokaMagazinaSipasIDGjenerues(kokam.IdKokaEkzekutim, 2, kokam.IdKonfigAmbjente);
            kokhyrje.mbushKokaMagazinaSipasIDGjenerues(kokam.IdKokaEkzekutim, 1, kokam.IdKonfigAmbjente);
            if (kokdalje.IdKokaMagazina != 0)
            {
                kokdalje.mbushTrupMagazine(false);
                colArtikujt coleksistues1 = new colArtikujt(kokdalje.IdKokaMagazina, new clsDatabaseInventari());
                int i2 = 0;
                foreach (clsTrupiMagazina trup in kokdalje.OcolTrupiMagazina)
                {
                    if (trup.IdLlojVeprimi == 1)
                        trup.Element = coleksistues1[i2];
                    i2++;
                }
                mesazh = kokdalje.kontrolloGjendjeNeFshirje(new DbCore.DbRegjistrim.colTrupiMagazina(), 0);
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    return;
                }
            }
            if (kokhyrje.IdKokaMagazina != 0)
            {
                kokhyrje.mbushTrupMagazine(false);
                colArtikujt coleksistues1 = new colArtikujt(kokhyrje.IdKokaMagazina, new clsDatabaseInventari());
                int i2 = 0;
                foreach (clsTrupiMagazina trup in kokhyrje.OcolTrupiMagazina)
                {
                    if (trup.IdLlojVeprimi == 1)
                        trup.Element = coleksistues1[i2];
                    i2++;
                }
                mesazh = kokhyrje.kontrolloGjendjeNeFshirje(new DbCore.DbRegjistrim.colTrupiMagazina(), 0);
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    return;
                }
            }
            if (kokdalje.IdKokaMagazina != 0 && hfKontrollRivleresim.Value.ToLower() == "true" && kokdalje.IdStatusDok != 0)
                if (kokdalje.rivleresim())
                {
                    rivleresim = true;
                    tr.mbushGjitheTrupiMagazinaNgaKoka(kokdalje.IdKokaMagazina);
                    trupat.AddRange(tr);
                }
            if (kokhyrje.IdKokaMagazina != 0 && hfKontrollRivleresim.Value.ToLower() == "true" && kokdalje.IdStatusDok != 0)
                if (kokhyrje.rivleresim())
                {
                    rivleresim = true;
                    tr.mbushGjitheTrupiMagazinaNgaKoka(kokhyrje.IdKokaMagazina);
                    trupat.AddRange(tr);
                }
            mesazh = kokam.fshi();
            //Session.Add("trupat", trupat);
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, trupat);
            if (mesazh.Status)
            {
                if (rivleresim)
                {
                    clsMenuInfo.ShtoPyetje(MenuInfo, mesazh.PershkrimMesazhi + MessagesResource.Messages["regjMagVeprimiSjellNdryshimNeCmimDalje"], pnlMesazhi, idGjuha);
                    //Session.Add ("Fshi","fshi");
                    DbCore.mySessionObjects.ruajFshiNeSesion(Session, "fshi");
                }
                else
                {
                    Response.Redirect("EkzekutimProdhimi.aspx?fshi=po");
                    return;
                }
                status1.Value = "true";
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                status1.Value = "false";
                return;
            }
        }
        private bool konvertuarPlotesisht(int idNdermarrje, int[] ids)
        {
            for (int i = 0, idsLength = ids.Length; i < idsLength; i++)
            {
                string ngjyra = clsKokaPlanifikim.eshteEkzekutuarPlanifikimi(ids[i], idNdermarrje);
                if (ngjyra == "kuqe" || ngjyra == "gjelber")
                {
                    clsKokaPlanifikim kokaplan = new clsKokaPlanifikim();
                    kokaplan.mbushKokaPlanifikimSipasIDPaTrup(ids[i]);
                    lblMsgboxKonv.Text = "Dokumenti Nr." + kokaplan.NrDok + " Dt." + kokaplan.DtDok.ToShortDateString() + " eshte konvertuar plotesisht, doni te vazhdoni?";
                    status1.Value = "konvertuar";
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Ruan/Modifikon nje objekt clsKokaEkzekutim.
        /// </summary>
        /// <param name="statusDokumenti">Statusi me te cilin po ruhet dokumenti (I rregullt apo draft)</param>
        /// <param name="idViti"></param>
        /// <param name="idNdermarrjeVit"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="ci"></param>
        /// <param name="eshteOwn"></param>
        /// <param name="idPerdoruesi"></param>
        private void ruajRegjistrimEkzekutim(int statusDokumenti, bool kontrollokonvertim, int idViti, int idNdermarrjeVit, int idNdermarrje, CultureInfo ci, bool eshteOwn, int idPerdoruesi, int idGjuha, bool printo = false)
        {
            if (Page.IsValid == false)
                return;
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            DbCore.DbShare.clsKonfigurimAmbjenti clsKonf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            if (cmbKonfigurimi.Text != "") //cmbKonfigurimi.Value != null && 
                clsKonf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);
            else
                clsKonf.mbushKonfigDefaultKomponentes(806, idNdermarrje);

            if (!isValid(statusDokumenti, rm, ci, idNdermarrjeVit, idNdermarrje, idPerdoruesi, clsKonf))
            {
                status1.Value = "false";
                return;
            }
            string shfaqmesazhapolupe = "jo";
            colTrupiMagazina trupat = new colTrupiMagazina();
            bool rivleresim = false; colTrupiMagazina tr = new colTrupiMagazina();
            clsKokaEkzekutim koka = new clsKokaEkzekutim();
            colPlanifikimEkzekutim colPlanifikimEkzekutimi = merrPlanifikimEkzekutimi();
            clsMesazh mesazh;
            try
            {
                koka = krijoRegjistrimEkzekutim(clsKonf, statusDokumenti, idNdermarrjeVit, idNdermarrje, ci, idPerdoruesi);

                if (koka.ColProdukte.Count == 0)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgTrupiDokNukDuhetBosh"], pnlMesazhi, LoadingPanel);
                    status1.Value = "false";
                    return;
                }
            }
            catch (MyException ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi, LoadingPanel);
                status1.Value = "false";
                return;
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNdodhiNjeGabimGjateKrijimitTeDok"], pnlMesazhi, LoadingPanel);
                status1.Value = "false";
                return;
            }

            int idPeriudha = 0;
            if (hfShtimModifikim.Value == "shtim")
            {
                //periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(); //ne shtim nuk na duhet me ta marrim nga sesioni, se e marrim nga hfState qe ne page_load
                DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
                idPeriudha = periudha.IdPeriudha; //(int)hfState["idPeriudha"];//nqs ndryshon periudhen pasi e ke hapur faqen duhet periudha e re
                JavaScriptSerializer serializusi = new JavaScriptSerializer();
                int[] ids = ((object[])serializusi.DeserializeObject(hfPlanifikime.Value)).Cast<int>().ToArray();
                if (kontrollokonvertim && konvertuarPlotesisht(idNdermarrje, ids))
                    return;
            }
            else
                idPeriudha = DbCore.DbAdmin.clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(koka.DtDok, idNdermarrje);
                //periudha = new DbCore.DbAdmin.clsPeriudhaKontabel(koka.DtDok, idNdermarrje);
         
            bool mekontabilizim = false;
            if (statusDokumenti == 1)//nese nuk eshte draft do gjeneroje kontabilizim perndryshe jo
            {
                if (this.hfKontabilizimi.Value == "1" || hfKontabilizimi.Value == "2")
                {
                    mekontabilizim = true;
                }
            }
            DbCore.DbShare.clsKonfigurimAmbjenti konfdalje = new DbCore.DbShare.clsKonfigurimAmbjenti(int.Parse(hfFD.Value.ToString()));
            //konfdalje.mbushKonfigAmbjSipasKod(konfdalje.KodKonfigAmbjente, idndermarje);
            DbCore.DbShare.clsKonfigurimAmbjenti konfhyrje = new DbCore.DbShare.clsKonfigurimAmbjenti(int.Parse(hfFH.Value.ToString()));
            //konfhyrje.mbushKonfigAmbjSipasKod(konfhyrje.KodKonfigAmbjente, idndermarje);
           
            DbCore.DbRegjistrim.clsKokaMagazina maghyrje = new DbCore.DbRegjistrim.clsKokaMagazina();
            DbCore.DbRegjistrim.clsKokaMagazina magdalje = new DbCore.DbRegjistrim.clsKokaMagazina();
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejtaInfo = new DbCore.DbAdmin.clsTeDrejtaRoli();

            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);

            if (hfShtimModifikim.Value == "shtim")
            {
                if ((statusDokumenti == 1 && !tedrejtaInfo.DShtim) || (statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["mesazhRaportNukKeniTeDrejta"], pnlMesazhi);
                    status1.Value = "false";
                    return;
                }
                //hfArkiva.Set("kopjoArkiven", hfShtimModifikim.Value != "shtim");
                mesazh = koka.ruaj(hfNrAutoShitje, colPlanifikimEkzekutimi, idPeriudha, mekontabilizim, konfdalje, konfhyrje, out shfaqmesazhapolupe, eshteOwn, idGjuha, rm, ci, false, "", "", "", "", false);
                if (statusDokumenti != 0 && hfKontrollRivleresim.Value.ToLower() == "true")
                {
                    magdalje.mbushKokaMagazinaSipasIDGjenerues(koka.IdKokaEkzekutim, 2, koka.IdKonfigAmbjente);
                    maghyrje.mbushKokaMagazinaSipasIDGjenerues(koka.IdKokaEkzekutim, 1, koka.IdKonfigAmbjente);
                    if (maghyrje.IdKokaMagazina != 0)
                        if (maghyrje.rivleresim())
                        {
                            rivleresim = true;
                            tr.mbushGjitheTrupiMagazinaNgaKoka(maghyrje.IdKokaMagazina);
                            trupat.AddRange(tr);
                        }
                    if (magdalje.IdKokaMagazina != 0)
                        if (magdalje.rivleresim())
                        {
                            rivleresim = true;
                            tr.mbushGjitheTrupiMagazinaNgaKoka(magdalje.IdKokaMagazina);
                            trupat.AddRange(tr);
                        }
                }
            }
            else
            {
                if ((statusDokumenti == 1 && !tedrejtaInfo.DMod) || (statusDokumenti == 0 && !tedrejtaInfo.DModifikimDraft))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["mesazhRaportNukKeniTeDrejta"], pnlMesazhi);
                    status1.Value = "false";
                    return;
                }
                koka.IdKokaEkzekutim = int.Parse(Request.QueryString["id"]);
                bool lidhur = koka.eshteILidhur();
                if (lidhur.ToString() != hfLidhur.Value)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgDokumentiEshteILidhur"], pnlMesazhi, LoadingPanel);
                    status1.Value = "false";
                    return;
                }
                if (statusDokumenti != 0 && hfKontrollRivleresim.Value.ToLower() == "true")
                {
                    magdalje.mbushKokaMagazinaSipasIDGjenerues(koka.IdKokaEkzekutim, 2, koka.IdKonfigAmbjente);
                    maghyrje.mbushKokaMagazinaSipasIDGjenerues(koka.IdKokaEkzekutim, 1, koka.IdKonfigAmbjente);
                    if (maghyrje.IdKokaMagazina != 0)
                        if (maghyrje.rivleresim())
                        {
                            rivleresim = true;
                            tr.mbushGjitheTrupiMagazinaNgaKoka(maghyrje.IdKokaMagazina);
                            trupat.AddRange(tr);
                        }
                    if (magdalje.IdKokaMagazina != 0)
                        if (magdalje.rivleresim())
                        {
                            rivleresim = true;
                            tr.mbushGjitheTrupiMagazinaNgaKoka(magdalje.IdKokaMagazina);
                            trupat.AddRange(tr);
                        }
                }

                if (hfShtimModifikim.Value == "shtim")
                {
                    mesazh = koka.modifikoTotal(lidhur, colPlanifikimEkzekutimi, idPeriudha, mekontabilizim, konfdalje, konfhyrje, out shfaqmesazhapolupe, eshteOwn, idGjuha, rm, ci, false);
                }
                else
                    mesazh = koka.modifiko(lidhur, colPlanifikimEkzekutimi, idPeriudha, mekontabilizim, konfdalje, konfhyrje, out shfaqmesazhapolupe, eshteOwn, idGjuha, rm, ci, hfShtimModifikim.Value == "modifikim");
            }

            if (!mesazh.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi, LoadingPanel);
                status1.Value = "false";
                return;
            }
            if (statusDokumenti != 0 && hfKontrollRivleresim.Value.ToLower() == "true")
            {
                magdalje.mbushKokaMagazinaSipasIDGjenerues(koka.IdKokaEkzekutim, 2, koka.IdKonfigAmbjente);
                maghyrje.mbushKokaMagazinaSipasIDGjenerues(koka.IdKokaEkzekutim, 1, koka.IdKonfigAmbjente);
                if (maghyrje.IdKokaMagazina != 0)
                    if (maghyrje.rivleresimPas())
                    {
                        rivleresim = true;
                        tr.mbushGjitheTrupiMagazinaNgaKoka(maghyrje.IdKokaMagazina);
                        trupat.AddRange(tr);
                    }
                if (magdalje.IdKokaMagazina != 0)
                    if (magdalje.rivleresimPas())
                    {
                        rivleresim = true;
                        tr.mbushGjitheTrupiMagazinaNgaKoka(magdalje.IdKokaMagazina);
                        trupat.AddRange(tr);
                    }
            }
            hfqkmesazhi.Value = shfaqmesazhapolupe;

            if (shfaqmesazhapolupe != "jo")
            {
                clsKokaFleteKontabel kok = new clsKokaFleteKontabel(koka.IdKokaEkzekutim, 45);
                hfUrl.Value = "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente;
            }
            if (printo)
                Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=urdherPune&idDokumenti=" + koka.IdKokaEkzekutim + "&printo=true&raportdyte=jo";
            
            DbCore.mySessionObjects.ruajTrupatNeSession(Session, trupat);
            if (rivleresim)
                clsMenuInfo.ShtoPyetje(MenuInfo, mesazh.PershkrimMesazhi + MessagesResource.Messages["msgVeprimNdryshimeRivleresim"], pnlMesazhi, idGjuha);
            else
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
            hl = new HtmlTable();
            pnlLidhur.Update();
            status1.Value = "true";
            hfShtimModifikim.Value = "shtim";
        }

        private colPlanifikimEkzekutim merrPlanifikimEkzekutimi()
        {
            colPlanifikimEkzekutim colPlanifikimEkzekutimi = new colPlanifikimEkzekutim();
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] dokumenti = (object[])serializusi.DeserializeObject(hfPlanifikime.Value);
            for (int i = 0; i < dokumenti.Length; i++)
            {
                clsPlanifikimEkzekutim planifikimEkzekutimi = new clsPlanifikimEkzekutim();
                planifikimEkzekutimi.IdPlanifikimi = Convert.ToInt32((dokumenti[i]));
                clsKokaPlanifikim kokaPlanifikim = new clsKokaPlanifikim(planifikimEkzekutimi.IdPlanifikimi);
                planifikimEkzekutimi.IdKonfigAmbjentePlanifikimi = kokaPlanifikim.IdKonfigAmbjente;
                colPlanifikimEkzekutimi.Add(planifikimEkzekutimi);
            }
            return colPlanifikimEkzekutimi;
        }

        /// <summary>
        /// Krijon nje objekt te tipit clsKokaEkzekutim
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>

        /// <returns>Kthen nje objekt te tipit clsKokaEkzekutim</returns>
        /// <param name="idNdermarrjeVit"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="ci"></param>
        /// <param name="idPerdoruesi"></param>
        private clsKokaEkzekutim krijoRegjistrimEkzekutim(clsKonfigurimAmbjenti clsKonf, int statusDokumenti, int idNdermarrjeVit, int idNdermarrje, CultureInfo ci, int idPerdoruesi)
        {
            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //DbCore.DbAdmin.NrAuto.vendosVleratNrAuto(hfNrAuto, this, null, ASPxSplitter1, null);

            hfNrAutoShitje = (ASPxHiddenField)DbCore.DbAdmin.NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");
            //DbCore.DbAdmin.NrAuto.shtoNeHfRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");

            clsKokaEkzekutim koka = new clsKokaEkzekutim();
            try
            {
                koka = krijoRegjistrimEkzekutim(statusDokumenti, clsKonf, ruajTrupinEPlanifikimit(true, idNdermarrje, ci, idPerdoruesi), idNdermarrjeVit, idNdermarrje, idPerdoruesi);
                return koka;
            }
            catch (MyException ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                throw new MyException(ex.Message);
            }
        }

        /// <summary>
        /// Krijon nje objekt te tipit clsKokaEkzekutim 
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina</returns>
        /// <param name="idnderviti"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idperdoruesi"></param>
        private clsKokaEkzekutim krijoRegjistrimEkzekutim(int statusDokumenti, DbCore.DbShare.clsKonfigurimAmbjenti clsKonf, colProduktProdhimi coltrupi, int idnderviti, int idNdermarrje, int idperdoruesi)
        {
            clsKokaEkzekutim mag = new clsKokaEkzekutim();
            int idmagazina = 0, idmag2 = 0, idgrup = 0, idgrup2 = 0, idgrup3 = 0;
            string magazina = "", mag2 = "";

            if (btneMagazina.Text != "")
            {
                idmagazina = int.Parse(btneMagazina.Value.ToString());
                magazina = btneMagazina.Text;
            }

            if (btneMagazin2a.Text != "")
            {
                idmag2 = int.Parse(btneMagazin2a.Value.ToString());
                mag2 = btneMagazin2a.Text;
            }

            if (cmbGrup1.Text != "")
                idgrup = int.Parse(cmbGrup1.Value.ToString());
            if (cmbGrup2.Text != "")
                idgrup2 = int.Parse(cmbGrup2.Value.ToString());
            if (cmbGrup3.Text != "")
                idgrup3 = int.Parse(cmbGrup3.Value.ToString());

            int idNjesiProdhimi = 0;
            string kodNjesiProdhimi = "";
            if (cmbNjesiProdhimi.Text != "")
            {
                kodNjesiProdhimi = cmbNjesiProdhimi.Text;
                bool sukses = int.TryParse(cmbNjesiProdhimi.Value.ToString(), out idNjesiProdhimi);
                if (!sukses)
                    throw new DbCore.MyException("Njesia e prodhimit nuk ekziston!");
            }
            clsMesazh mesazh = mag.krijoEkzekutim(clsKonf.IdNivel, clsKonf.IdKonfigAmbjente, idmagazina, magazina, idmag2, mag2, dteDtDok.Date, txtNrDok.Text, statusDokumenti, idNdermarrje, idnderviti, idperdoruesi, dteDtRegjistrimi.Date, txtShenime.Text, double.Parse(txtTotali.Text), idgrup, idgrup2, idgrup3, coltrupi, idNjesiProdhimi, kodNjesiProdhimi);
            if (!mesazh.Status)
                throw new DbCore.MyException(mesazh.PershkrimMesazhi);
            return mag;
        }

        /// <summary>
        /// krijon koleksionin me trupin e dokumentit
        /// </summary>
        /// <param name="ruaj">ruaj boolean qe tregon ne duhen shtuar apo jo reshtat bosh ne ruajtje nuk duhen</param>
        /// <returns>coleksion me trupin e dokumentit</returns>
        /// <param name="idNdermarrje"></param>
        /// <param name="cultinf"></param>
        /// <param name="idPerdoruesi"></param>
        private colProduktProdhimi ruajTrupinEPlanifikimit(bool ruaj, int idNdermarrje, CultureInfo cultinf, int idPerdoruesi)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            colProduktProdhimi col = new colProduktProdhimi();
            colRecepturaProdhimi colrec = new colRecepturaProdhimi();
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            object[] dokumenti = (object[])serializusi.DeserializeObject(gridDataObject.Value);
            object[] rec = (object[])serializusi.DeserializeObject(gridDataObject2.Value);
            int idMagTemp = -1, idmagprod = -1;
            bool isMagENjejte = true, ismagprod = true;
            for (int i = 0; i < rec.Length; i++)
            {
                clsRecepturaProdhimi recep = new clsRecepturaProdhimi(idNdermarrje, idPerdoruesi, (Dictionary<string, object>)rec[i]);
                if (recep.IdProdukti != 0)
                    colrec.Add(recep);
            }
            for (int i = 0; i < dokumenti.Length; i++)
            {
                clsProduktProdhimi p = new clsProduktProdhimi(idNdermarrje, idPerdoruesi, (Dictionary<string, object>)dokumenti[i]);
                if (p.IdArtikulli != 0)
                {
                    p.ColReceptura = new colRecepturaProdhimi();
                    if (p.IdMag == 0)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgMagazinaNukEkziston"], pnlMesazhi, LoadingPanel);
                        return new colProduktProdhimi();
                    }
                    if (idmagprod == -1)
                        idmagprod = p.IdMag;
                    else
                        if (ismagprod && p.IdMag != idmagprod)
                        ismagprod = false;
                    foreach (clsRecepturaProdhimi r in colrec)
                    {
                        if (r.IdMag == 0)
                        {
                            //clsMenuInfo.ShtoMesazhGabimi(MenuInfo, STR_MagazinaNukEkziston, pnlMesazhi, LoadingPanel);
                            //return new colProduktProdhimi();
                            throw new MyException(MessagesResource.Messages["msgMagazinaNukEkziston"]);
                        }
                        if (r.IdArtikulli != 0)
                        {
                            clsArtikulli artikulli = new clsArtikulli(r.IdArtikulli);
                            if (!artikulli.Aktiv)
                            {
                                //clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Artikulli me kod: " + artikulli.KodArtikulli + " nuk eshte aktiv!", pnlMesazhi, LoadingPanel);
                                //return new colProduktProdhimi();
                                throw new MyException(MessagesResource.Messages["msgShtoArtikullPrefixNjejes"] + artikulli.KodArtikulli + MessagesResource.Messages["msgNukEshteAktiv"]);
                            }
                            if (!artikulli.MbetjeShitshme && r.SasiaAktuale < 0)
                                throw new MyException(MessagesResource.Messages["msgShtoArtikullPrefixNjejes"] + artikulli.KodArtikulli + MessagesResource.Messages["msgNukEshteMbetjeShitshme"]);
                        }
                        if (r.IdBurimi != 0)
                        {
                            clsBurime burim = new clsBurime(r.IdBurimi);
                            if (!burim.Aktiv)
                            {
                                //clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Burimi me kod: " + burim.Kodi + " nuk eshte aktiv!", pnlMesazhi, LoadingPanel);
                                //return new colProduktProdhimi();
                                throw new MyException(MessagesResource.Messages["msgBurimiMeKod"] + burim.Kodi + MessagesResource.Messages["msgNukEshteAktiv"]);
                            }
                        }
                        if (idMagTemp == -1)
                            idMagTemp = r.IdMag;
                        else
                            if (isMagENjejte && r.IdMag != idMagTemp)
                            isMagENjejte = false;
                        if (r.IdProdukti == p.Id && (r.IdArtikulli != 0 || r.IdBurimi != 0))
                            p.ColReceptura.Add(r);
                    }
                    if (p.ColReceptura.Count == 0)
                        throw new MyException(MessagesResource.Messages["msgNjeNgaProdNukKaReceptura"]);
                    col.Add(p);
                }
            }
            if (ruaj)
            {
                btneMagazin2a.Text = "";

                if (isMagENjejte && colrec.Count > 0)
                {
                    btneMagazin2a.Text = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiD(colrec[0].IdMag, idPerdoruesi);
                }
                btneMagazina.Text = "";

                if (ismagprod && col.Count > 0)
                {
                    btneMagazina.Text = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiD(col[0].IdMag, idPerdoruesi);
                }
            }
            return col;
        }


        /// <summary>
        /// Kontrollon nese jane plotesuar te gjitha fushat e detyrueshme te kokes se dokumentit
        /// </summary>
        /// <param name="idNdermarrjeVit"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        private bool isValid(int draft, ResourceManager rm, CultureInfo ci, int idNdermarrjeVit, int idNdermarrje, int idPerdoruesi, clsKonfigurimAmbjenti konf)
        {
            bool isValid = true;

            if (dteDtRegjistrimi.Text == "")
            {
                isValid = false;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgLidhjaDokZgjidh1DateRegjistrimi"], pnlMesazhi, LoadingPanel);

                return isValid;
            }
            if (dteDtDok.Text == "")
            {
                isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgLidhjaDokZgjidh1DateDokumenti"], pnlMesazhi, LoadingPanel);

                return isValid;
            }
            if (dteDtDok.Date.Year != new DbCore.DbAdmin.clsNdermarrjeViti(idNdermarrjeVit).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgDataNukPerketVititUshtrimor"], pnlMesazhi);
                return false;
            }
            DbCore.DbAdmin.clsPeriudhaKontabel periudha = DbCore.mySessionObjects.merrPeriudheKontabel(Session);
            //DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel((int)hfState["idPeriudha"], ((int)hfState["idGjuha"])); //DbCore.mySessionObjects.merrPeriudheKontabel(Session);//nqs periudha ndrohet pasi eshte hapur faqja jep problem
            String mesazhGabimi;
            if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, dteDtDok.Date, periudha, draft))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                return false;
            }

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(dteDtDok.Date, MyConnectionsManager.GetSelectedConNameServer(), IdNdermarrja, KategoriDokumenti.EkzekutimProdhimi, konf.IdKonfigAmbjente))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                return false;
            }

            if (btneMagazina.Text != "")
            {
                clsNjesiAdministrative mag = new clsNjesiAdministrative(btneMagazina.Text, idNdermarrje, idPerdoruesi);
                if (mag.IdNjesiAdministrative == -1)
                {
                    isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgKjoMagazineNukEkziston"], pnlMesazhi, LoadingPanel);
                    return isValid;
                }
                else
                {
                    if (mag.Aktiv == false)
                    {
                        isValid = false; clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgMagazinaNukEshteAktive"], pnlMesazhi, LoadingPanel);
                        return isValid;
                    }
                }
            }
            return isValid;
        }
     


        #region  GRIDA E FATURAVE
        private void mbushGrideFaturatngaDB(int idNdermarrjeVit, int idNdermarrje, int idPerdoruesi)
        {
            ASPxGridView grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");

            //nder.mbushNdermarrjeViti(idNdermarrjeVit);
            DataTable dt = new DataTable();
            dt = colDokumentat.ktheGjitheDokumentatPlanifikimTePalidhura(idNdermarrje, new DateTime().ToShortDateString(), new DbCore.DbAdmin.clsNdermarrjeViti(idNdermarrjeVit).NdermarrjeVitiFund.ToShortDateString(), idPerdoruesi);
            mySessionObjects.ruajGrideNeSession(komponente,Session, dt);
            grid_faturat.DataSource = dt;
            grid_faturat.DataBind();
            dt.Dispose();
        }
      
        private void merrGrideFaturatngaSession(int idNdermarrjeVit, int idNdermarrje, int idPerdoruesi)
        {
            ASPxGridView grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            DataTable tmpObject;
            bool sukses = mySessionObjects.merrGrideNgaSessioni(komponente,Session, out tmpObject);
            if (!sukses)
                mbushGrideFaturatngaDB(idNdermarrjeVit,idNdermarrje, idPerdoruesi);
            else
            {
                grid_faturat.DataSource = tmpObject;
                grid_faturat.DataBind();
                tmpObject.Dispose();
            }
        }

        private void konfiguroGrideFaturat(bool visibleindex, int idGjuha, int idNdermarrje, int idPerdoruesi)
        {
            ASPxGridView grid_faturat = (ASPxGridView)ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            shtokolona(grid_faturat);
            KonfigurimComboGride.ShtoModelMeDataSource(grid_faturat, () =>
           {
               var konfigurimet = new DbCore.DbShare.colKonfigurimAmbjenti();
               konfigurimet.mbushKonfigAmbjSipasIdKategori(44, idNdermarrje, idPerdoruesi, idGjuha);
               return konfigurimet;
           }, Session, komponente, guidString);
            KonfigurimComboGride.ShtoNivel(grid_faturat, 44, idNdermarrje, idPerdoruesi, idGjuha, Session, komponente, guidString, "IdNiveli");

            System.Globalization.CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            GridUtil.konfigGrideListeEMadhePaTheme(grid_faturat, "IdDokumenti", true);
            grid_faturat.Columns["#"].VisibleIndex = 0;
        }

        private void shtokolona(ASPxGridView grid_faturat)
        {
            GridViewDataTextColumn colnew1;
            GridViewDataDateColumn colnew2;
            GridViewDataColumn colnew3;
            if (grid_faturat.Columns["IdDokumenti"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdDokumenti"; colnew1.VisibleIndex = 0;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["Ngjyra"] == null)
            {
                colnew3 = new GridViewDataTextColumn();
                colnew3.FieldName = "Ngjyra";
                colnew3.VisibleIndex = 8;
                grid_faturat.Columns.Add(colnew3);
            }
            if (grid_faturat.Columns["IdKonfigAmbjente"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdKonfigAmbjente"; colnew1.VisibleIndex = 2;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdNiveli"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdNiveli"; colnew1.VisibleIndex = 1;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["NrDokumenti"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "NrDokumenti"; colnew1.VisibleIndex = 3;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["DtDokumenti"] == null)
            {
                colnew2 = new GridViewDataDateColumn();
                colnew2.FieldName = "DtDokumenti"; colnew2.VisibleIndex = 4;
                grid_faturat.Columns.Add(colnew2);
            }
            if (grid_faturat.Columns["IdKlientFurnitori"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdKlientFurnitori";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["EmertimiKf"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "EmertimiKf"; colnew1.VisibleIndex = 5;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["IdMonedha"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "IdMonedha"; colnew1.VisibleIndex = 6;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["Vlefta"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Vlefta"; colnew1.VisibleIndex = 7;
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["Pershkrimi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Pershkrimi";
                grid_faturat.Columns.Add(colnew1);
            }

            if (grid_faturat.Columns["Status"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Status";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["Produkti"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "Produkti";
                grid_faturat.Columns.Add(colnew1);
            }
            if (grid_faturat.Columns["krijuesi"] == null)
            {
                colnew1 = new GridViewDataTextColumn();
                colnew1.FieldName = "krijuesi";
                grid_faturat.Columns.Add(colnew1);
            }
        }
       
        /// <summary>
        /// Mbush combon e klienteve/furnitoreve ne gride
        /// </summary>
        /// <param name="idNdermarrje"></param>
        private void shtoKlientFurnitor(ASPxGridView grid, int idNdermarrje)
        {
            int visibleindex = grid.Columns["IdKlientFurnitori"].VisibleIndex;
            //DbCore.DbKontabiliteti.clsDatabaseKontabilitet dbKontab = new DbCore.DbKontabiliteti.clsDatabaseKontabilitet();
            grid.Columns.Remove(grid.Columns["IdKlientFurnitori"]);
            GridViewDataComboBoxColumn colnew = new GridViewDataComboBoxColumn();
            DbCore.DbKontabiliteti.colKlienteFurnitore colKlientet = new colKlienteFurnitore();
            colKlientet.Add(new DbCore.DbKontabiliteti.clsKlientFurnitor());
            colKlientet.mbushKlienteFurnitoreNdermarrjes(idNdermarrje);
            colnew.PropertiesComboBox.DataSource = colKlientet;
            colnew.PropertiesComboBox.TextField = "EmertimiKF";
            colnew.PropertiesComboBox.ValueField = "IdKlientFurnitor";
            colnew.FieldName = "IdKlientFurnitori"; colnew.VisibleIndex = visibleindex;
            grid.Columns.Add(colnew);
        }

        protected void grid_faturat_DataBound(object sender, EventArgs e)
        {
            ASPxGridView grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            if (grid_faturat.Columns["#"] == null)
            {
                DevExpress.Web.GridViewCommandColumn check = new DevExpress.Web.GridViewCommandColumn("#") { ShowSelectCheckbox = true, Width = System.Web.UI.WebControls.Unit.Percentage(2) };

                grid_faturat.Settings.ShowFilterRow = true;
                grid_faturat.Settings.ShowHeaderFilterButton = true;
                grid_faturat.Settings.ShowFilterBar = GridViewStatusBarMode.Visible;
                grid_faturat.Settings.ShowFilterRowMenu = true;
                grid_faturat.Columns.Add(check);
                grid_faturat.Settings.ShowGroupPanel = false;
                grid_faturat.KeyFieldName = "IdDokumenti";
                grid_faturat.SettingsBehavior.AllowSelectByRowClick = true;
                grid_faturat.SettingsBehavior.AllowFocusedRow = true;
                grid_faturat.Settings.ShowTitlePanel = false;
                grid_faturat.SettingsText.Title = "Zgjidhni faturat";
            }
        }

        protected void grid_faturat_CustomCallback(object sender, ASPxGridViewCustomCallbackEventArgs e)
        {
            string[] arr = e.Parameters.Split(';');

            ASPxGridView grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            int idNdermarrje = (int)hfState["idNdermarrje"];
           int idGjuha = (int)hfState["idGjuha"];
            if (arr.Length == 1) //rasti kur behet callback nga filtri i periudhes duhet te ngelen filtrat ne gride dhe te mos aplikohet filtri default
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_faturat", grid_faturat, cmbKonfigurimi.Text.Split(';')[0], "806", idGjuha, false);
            else
            {
                grid_faturat.FilterExpression = String.Empty; //ne rastet e tjera duhet pastruar filtri dhe vendosur default-i nga funksioni me poshte
                GridUtil.PercaktoVisibleColumnsGridSipasKodKonfigurimi(idNdermarrje, "grid_faturat", grid_faturat, cmbKonfigurimi.Text.Split(';')[0], "806", idGjuha);
            }
            if (arr.Length == 3)
            {
                if (arr[2] == "")
                    grid_faturat.FilterExpression = " ";
                else
                {
                    DbCore.DbAdmin.clsFiltraGrida filtra = new DbCore.DbAdmin.clsFiltraGrida();
                    DbCore.DbAdmin.clsGridaKoka koka = new DbCore.DbAdmin.clsGridaKoka(idGjuha, emerGride, komponente, idNdermarrje, int.Parse(cmbKonfigurimi.Value.ToString()));
                    filtra.mbushFilterPerGrideSipasKodit(arr[2], idNdermarrje, koka.IdGridaKoka);
                    if (filtra.FiltraKodi != null)
                    {
                        grid_faturat.FilterExpression = filtra.FiltraVlera;
                        GridUtil.renditGriden(filtra.KoloneRenditje, grid_faturat);
                    }
                }
            }
            mbushGrideFaturatngaDB(DbCore.mySessionObjects.ktheNdermarrjeVit(Session), idNdermarrje, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            grid_faturat.Selection.UnselectAll();
        }

        protected void grid_faturat_CustomJSProperties(object sender, ASPxGridViewClientJSPropertiesEventArgs e)
        {
            ASPxGridView grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            e.Properties["cpNoRows"] = grid_faturat.VisibleRowCount;
        }
        protected void grid_faturat_AfterPerformCallback(object sender, ASPxGridViewAfterPerformCallbackEventArgs e)
        {
            ASPxGridView grid_faturat = (ASPxGridView)this.ASPxNavBar1.Groups[0].FindControl("grid_faturat");
            if (e.CallbackName == "COLUMNMOVE" && grid_faturat.AllColumns[int.Parse(e.Args[0])].Width.Value == 0)
                grid_faturat.AllColumns[int.Parse(e.Args[0])].Width = System.Web.UI.WebControls.Unit.Percentage(3);
            if (e.CallbackName == "APPLYFILTER" && e.Args[0] == "")
            {
                MenuItem itemButton = ASPxMenu1.Items.FindByName("TemplatedItemFilter");
                ASPxComboBox cmbFiltra = ((PlatinumWeb.MenuFilter)(itemButton.Template)).FindControl("btnFiltra") as ASPxComboBox;
                cmbFiltra.Text = "";
            }
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            //clsFunksione.ToolTipButonaveMbiGride(grid_faturat, ci, rm);
        }
        protected void grid_faturat_ProcessColumnAutoFilter(object sender, ASPxGridViewAutoFilterEventArgs e)
        {
            if (e.Column.FieldName == "IdKonfigAmbjente" ||
                  e.Column.FieldName == "IdMonedha" || e.Column.FieldName == "IdKlientFurnitori")
            {
                if (Converter.ConvertToInt(e.Value)==0)
                {
                    e.Criteria = null;
                }
            }
        }

        protected void ButtonOk5_Click(object sender, EventArgs e)
        {
            Page.Validate();

            int idViti = idViti = (int)hfState["idViti"];
            int idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuha = (int)hfState["idGjuha"];
            CultureInfo ci = DbCore.IMBUtils.Messages.MessagesResource.KtheCultureInfo(idGjuha);
            bool eshteOwn = (bool)hfState["eshteOwn"];
            int idPerdoruesi = (int)hfState["idPerdoruesi"];
            switch (hfRuajDraft.Value.ToString())
            {
                case "Ruaj":
                    ruajRegjistrimEkzekutim(1, false, idViti, idNdermarrjeVit, idNdermarrje, ci, eshteOwn, idPerdoruesi, idGjuha);
                    break;
                case "Draft":
                    ruajRegjistrimEkzekutim(0, false, idViti, idNdermarrjeVit, idNdermarrje, ci, eshteOwn, idPerdoruesi, idGjuha);
                    break;
            }
        }
        /// <summary>
        /// Krijimi i fushes ngjyra nga eventi qe teigerohet ne aspx
        /// </summary>
        protected void grid_faturat_HtmlDataCellPrepared(object sender, ASPxGridViewTableDataCellEventArgs e)
        {
            if (!e.DataColumn.FieldName.ContainsAnyIgnoreCase("Ngjyra")) return;
            switch (e.CellValue.ToString())
            {

                case "gri":
                    e.Cell.BackColor = System.Drawing.Color.LightGray;
                    break;
                case "verdhe":
                    e.Cell.BackColor = System.Drawing.Color.LightYellow;
                    break;
                case "kuqe":
                    e.Cell.BackColor = System.Drawing.Color.OrangeRed;
                    break;
                case "gjelber":
                    e.Cell.BackColor = System.Drawing.Color.LightGreen;
                    break;
            }
            e.Cell.Text = string.Empty;
        }
        #endregion

        
    }
}