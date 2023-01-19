using DbCore;
using DbCore.DbAdmin;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DevExpress.Web;
using DevExpress.Web.ASPxGauges;
using DevExpress.Web.ASPxGauges.Gauges;
using DevExpress.Web.ASPxGauges.Gauges.State;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.UI.HtmlControls;
using DbCore.IMBUtils.DataBase;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.Pages;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Types;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using AlphaWebCommon.WebUtils.ASPxControlExtensions;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using CacheLayer;
using PlatinumWeb.ApplicationUtils;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using DbCore.DbArkaBanka;
using DbCore.IMBUtils.Fiskalizimi.API;
using System.Web.Configuration;
using Converter = DbCore.IMBUtils.Types.Converter;
using Newtonsoft.Json.Linq;

namespace PlatinumWeb
{
    public partial class Shto_RegjistrimDokumentash : MyPageBase
    {
        private colTrupiMagazina trupat = new colTrupiMagazina();
        private static RSAParameters publickey;
        private static RSAParameters privatekey;

        protected void Page_Load(object sender, EventArgs e)
        {
            ImbLogger.LogTraceShitje("Filloi metoda Page_Load");
            int idPerdoruesi, idGjuha, idNdermarrje, idViti;
            string veprimi, pageStateLloji, guidString;
            int idNdermarrjeVit;
            bool eshteOwn;
            bool ruajLog;
            if (!IsCallback)
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    clsFunksione.logout(Session, false, string.Empty, false);
            int idDokPas = 0, idDokPara = 0;
            if (!IsPostBack)
            {
                if (!mySessionObjects.isLogedIn(Session))
                {
                    clsFunksione.logout(Session, true, "FaqePaautorizuar");
                    return;
                }
                hfState.Set("dokumentBije", false);
                idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                // Info per Id   e perdoruesit
                ImbLogger.LogTraceShitje("Id e perdoruesit te faqes :" + Convert.ToString(idPerdoruesi));

                if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    ImbLogger.LogWarningShitje("Kujdes kodi ndermarrjes = " + Convert.ToString(mySessionObjects.ktheKodNdermarrje(Session)));
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                    return;
                }

                idGjuha = mySessionObjects.ktheGjuhe(Session);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                CultureInfo ci = MessagesResource.KtheCultureInfo(idGjuha);
                int idDok = 0;
                int.TryParse(Request.QueryString["id"], out idDok);
                clsKokaShitje dokMeIdNgaQueryStr = new clsKokaShitje();
                if (idDok > 0)
                    dokMeIdNgaQueryStr.mbushKokaShitjeSipasIDPaTrup(idDok);
                hfShtimModifikim.Value = pageStateLloji = Request.QueryString["shtim_modifikim"];
                //afishimi i nje info sa per prove
                string mesazh = "Kontrolli i llojit te faqes => " + pageStateLloji;
                ImbLogger.LogTraceShitje(mesazh);

                switch (pageStateLloji)
                {
                    case "shtim":
                    case "shtimraport":
                        idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                        idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
                        idNdermarrjeVit = mySessionObjects.ktheNdermarrjeVit(Session);
                        ImbLogger.LogTraceShitje("Vlera e idNdermarrje: " + idNdermarrje + " ,vlera e idViti: " + idViti + " ,vlera idNdermarrjeVit: " + idNdermarrjeVit + "moren nga session per pageStatelloji case: shtim/shtimraport");
                        break;
                    case "klonim":
                    case "kthim":
                    case "kthimVod":
                    case "bli":
                    case "konvertim":
                    case "konvertimblerje":
                    case "rezervim":
                    case "modifikim":
                        if (mySessionObjects.merrIdNdermarrjeSesioni(Session) != dokMeIdNgaQueryStr.IdNdermarrje && mySessionObjects.merrEshteMemeSesioni(Session))
                        {
                            idNdermarrje = dokMeIdNgaQueryStr.IdNdermarrje;
                            idNdermarrjeVit = dokMeIdNgaQueryStr.IdNdermarrjeVit;
                            idViti = dokMeIdNgaQueryStr.IdVitRaportimi;
                            hfState.Set("dokumentBije", true);
                            ImbLogger.LogTraceShitje("Dokument bije:" + Convert.ToString(true) + "Vlera e idNdermarrje: " + idNdermarrje + " ,vlera e idViti: " + idViti + " ,vlera idNdermarrjeVit: " + idNdermarrjeVit);
                        }
                        else
                        {
                            idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                            idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
                            idNdermarrjeVit = mySessionObjects.ktheNdermarrjeVit(Session);
                            ImbLogger.LogTraceShitje("Vlera e idNdermarrje: " + idNdermarrje + " ,vlera e idViti: " + idViti + " ,vlera idNdermarrjeVit: " + idNdermarrjeVit + "moren nga session");
                        }
                        break;
                    default:
                        idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                        idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
                        idNdermarrjeVit = mySessionObjects.ktheNdermarrjeVit(Session);
                        ImbLogger.LogTraceShitje("Vlera e idNdermarrje: " + idNdermarrje + " ,vlera e idViti: " + idViti + " ,vlera idNdermarrjeVit: " + idNdermarrjeVit + "moren nga session");
                        break;
                }

                veprimi = Request.QueryString["shitje_blerje"];
                //idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                if (Request.QueryString["idNderm"] != null && Request.QueryString["vjenNga"] != null && Convert.ToInt32(Request.QueryString["idNderm"]) != idNdermarrje && Request.QueryString["vjenNga"] == "aprovim")
                    clsFunksione.logout(Session, true, "aprovimDokNdermarrjeGabuar");
                if (Request.QueryString["likuiduar"] != null)
                    hfState.Set("likuiduar", Request.QueryString["likuiduar"]);
                //idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
                //idNdermarrjeVit = mySessionObjects.ktheNdermarrjeVit(Session);
                eshteOwn = mySessionObjects.merrEshteOwnSesioni(Session);
                ruajLog = mySessionObjects.merrRuajLog(Session);
                ucEmerSkedari.ValidationSettings.MaxFileSize = DbCore.clsFunksione.merrMaxFileSizePerImport();
                hfState.Set("llojDetajimNdermarrje", DbCore.DbInventari.clsNivelCmimi.merrDetajimCmimeshNdermarrje(idNdermarrje));
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("veprimi", veprimi);
                hfState.Set("idNdermarrjeVit", idNdermarrjeVit);
                hfState.Set("OwnShop", eshteOwn);
                hfState.Set("Meme", mySessionObjects.merrEshteMemeSesioni(Session));
                hfState.Set("Logu", ruajLog);
                hfState.Set("GJDM", false);
                hfState.Set("eshteDokKthimi", false);
                hfState.Set("MosModifikoTrup", false);
                hfState.Set("ci", ci.ToString());
                hfState.Set("MNSA", true);
                hfState.Set("URL_SUBJEKTEPASIV", WebConfigurationManager.AppSettings["URL_SUBJEKTEPASIV"]);
                hfState["guidString"] = guidString;
                EmrateLabelave(ci, rm);
                mbushHiddenFieldMePerkthime(ci, rm);
                hfMerrFazaNgaSesioni.Value = "false";
                hfState.Set("periudha", JsonConvert.SerializeObject(mySessionObjects.merrPeriudheKontabel(Session)));
                hfState.Set("RoliSR", false);
                hfState.Set("idDok", idDok);
                //hfState.Set("idMonBazeNdermarrje", clsMonedha.ktheIdMonedhenENdermarrjes(IdNdermarrja)); //nuk duhet sepse e marrim me poshte me objektin e ndermarrjes

                colRoli rolet = new colRoli();
                rolet.mbushRoletSipasIdPerdoruesi(DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
                foreach (clsRoli roli in rolet)
                {
                    if (roli.KodRoli.StartsWith("SR"))
                        hfState.Set("RoliSR", true);
                }

                clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
                hfState.Set("limitishitjes", nderm.LimitiShitjes);
                hfState.Set("idMonedheNderm", nderm.NdermarrjeMonedha);
                hfState.Set("pershkrimKomponente", clsFunksione.logoAmbient("Shto_RegjistrimDokumentash.aspx?shitje_blerje=" + veprimi, idDok.ToString(), idNdermarrje, idPerdoruesi, true, ci.ToString()));
                HttpCookie cookie = Request.Cookies["adresa"] ?? new HttpCookie("adresa");
                cookie.Value = Request.Url.PathAndQuery;
                cookie.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Set(cookie);
                hfState.Set("skemeBarkodiPeshore", clsKonfigurimKase.merrSkemeBarkodiPershoreje(idNdermarrje));
                hfArkivaDokId.Value = idDok.ToString();
                int.TryParse(Request.QueryString["iddokpara"], out idDokPara);
                int.TryParse(Request.QueryString["iddokpas"], out idDokPas);
                clsKokaShitje.merrDokParaPas(idDok, ref idDokPara, ref idDokPas);
                hfState.Set("idDokPas", idDokPas);
                hfState.Set("idDokPara", idDokPara);
                hfVeprimi.Value = veprimi;
                hfPerdoruesi.Value = idPerdoruesi.ToString();
                //hfShtimModifikim.Value = pageStateLloji = Request.QueryString["shtim_modifikim"];
                DataTable colMagazinatPara = colNjesiAdministrative.ktheTreNjesiteEParaAdministrativeAktiveMeLloj(idNdermarrje, idPerdoruesi, true);
                hfState.Set("tmpColMag", JsonConvert.SerializeObject(colMagazinatPara));
                //hfTmpColMag.Value = JsonConvert.SerializeObject(colMagazinat); 
                clsPerdorues perdoruesi = new clsPerdorues(idPerdoruesi);
                hfPerdoruesAktual.Value = perdoruesi.PerdoruesUsername;
                hfHapurMbyllur.Value = perdoruesi.InfoHapur.ToString();

                colTeDrejtaRoli teDrejtaNiveleRregjistrimi = new colTeDrejtaRoli();
                teDrejtaNiveleRregjistrimi.mbushTeDrejtaPerdoruesiNiveleRegjistrimiPerKomponente(idPerdoruesi, idNdermarrje, idViti, clsFunksione.GetKomponente(Page.Request));
                hfState.Set("teDrejtaNivele", Newtonsoft.Json.JsonConvert.SerializeObject(teDrejtaNiveleRregjistrimi));
                mbushComboNiveli();
                konfiguroTeDrejtat(idPerdoruesi, idNdermarrje, idViti);
                // mbushComboBoxKartat(idNdermarrje);
                int idKlienti = -1;
                DateTime dateDok = clsFunksione.ktheDateOreDefault(mySessionObjects.merrPeriudheKontabel(Session));
                clsMonedha clsMon = new clsMonedha();
                clsMon.mbushMonedhenENdermarrjes(idNdermarrje);
                hfState.Set("mondedheNdermarrjeObj", JsonConvert.SerializeObject(clsMon));
                switch (pageStateLloji)
                {
                    case "shtim":
                    case "shtimraport":
                        idKlienti = -1;
                        konfiguroVleraFillestareShto(idGjuha, veprimi, idNdermarrje, idPerdoruesi, pageStateLloji);
                        break;
                    case "klonim":
                    case "kthim":
                    case "kthimVod":
                    case "bli":
                    case "konvertim":
                    case "konvertimblerje":
                    case "rezervim":
                    case "modifikim":
                        konfiguroVleraFillestareModifiko(idGjuha, veprimi, idViti, idNdermarrje, idPerdoruesi, rm, ci, pageStateLloji, idDok);
                        if (btnKlienti.Value != null && !String.IsNullOrEmpty(btnKlienti.Value.ToString()))
                            Int32.TryParse(btnKlienti.Value.ToString(), out idKlienti);
                        dateDok = data_DateEdit.Date;
                        break;
                    default:
                        break;
                }
                konfiguroTeDrejtaPerMenu();
                if (pageStateLloji == "shtimraport")
                    MbushTeDhenaSipasRaportit();
                AspxWebControlUtils.perkthePopUp(popMesazhQK, rm.GetString("labelRaportKujdes", ci), lblMsgbox4, rm.GetString("msgDeshironiTeBeniShperndarjenNeQendratEKostos", ci), ButtonCancelQK, rm.GetString("btnJO", ci), ButtonOkQK, rm.GetString("btnPO", ci));
                AspxWebControlUtils.perkthePopUp(popKonvertim, rm.GetString("labelRaportKujdes", ci), lblKonvertoNe, rm.GetString("lblKonvertoNe", ci), null, null, ButtonOk2, rm.GetString("btnKonverto", ci));
                var konfigDb = clsFunksione.ktheKonfigDB(506, cmbModeli.Text, idNdermarrje, "btnKlienti", idKlienti, true, idGjuha, idPerdoruesi, pageStateLloji, dateDok, clsFunksione.ktheDateOreDefault(mySessionObjects.merrPeriudheKontabel(Session)));
                hfState.Set("konfigFillestar", JsonConvert.SerializeObject(konfigDb));
                int idSkema = clsKusht.kthevlereSipasKushtitDheIdKonfig(Convert.ToInt32(cmbModeli.Value.ToString()), "ZSP");
                bool[] menute = clsFunksione.merrMenu(idPerdoruesi, idSkema, pageStateLloji == "modifikim" ? false : true, lblStatusAprovimi.Text, idDok, cmbModeli.Text, 1);
                hfState.Set("menuteSipasSkemes", JsonConvert.SerializeObject(menute));
                hfState.Set("kategoriSerialesh", JsonConvert.SerializeObject(new colSerialeUnikeKategori(idNdermarrje)));

            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idGjuha = (int)hfState["idGjuha"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                veprimi = (string)hfState["veprimi"];
                idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
                pageStateLloji = hfShtimModifikim.Value;
                idDokPas = (int)hfState["idDokPas"];
                idDokPara = (int)hfState["idDokPara"];
                //Vendosur ne nje variabel temp vlera aktuale e cmbNiveli qe te behet mbushja perseri e combos kur kemi postback dhe vlera e selectuar nuk humbet
                var temp = cmbNiveli.Value;
                mbushComboNiveli();
                cmbNiveli.SelectedItem = cmbNiveli.Items.FindByValue(temp);
                //  hfMerrFazaNgaSesioni.Value = "true";
            }
            percaktoTemplateMenu(idGjuha, veprimi, idPerdoruesi, idViti, idNdermarrje, ASPxMenu1, pageStateLloji, idDokPara, idDokPas);

            if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
            {
                mbushComboProcesi();
                mbushComboTipiEinvoice();
                mbushComboTipiIVetefaturimit();
            }
            if (ConfigurationManager.AppSettings["ZyreKlient"] == "false")
            {
                cmbNiveli.ClientEnabled = false;
            }
            Container55.Attributes["src"] = string.Empty;
            Container1.Attributes["src"] = string.Empty;
            ImbLogger.LogTraceShitje("Mbaroi metoda Page_Load");
        }

        /// <summary>
        /// Vendos emrat e label ne baze te gjuhes se perdoruesit
        /// </summary>
        /// <param name="ci"> kthen CultureInfo nga sesioni ne baze te gjuhes se perdoruesit</param>
        private void EmrateLabelave(CultureInfo cultinf, ResourceManager rm)
        {
            lblKonfig.Text = rm.GetString("lblLloji", cultinf);
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="cultinf">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void mbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda mbushHiddenFieldMePerkthime me parametra cultinf:{JsonConvert.SerializeObject(cultinf)}, rm:{JsonConvert.SerializeObject(rm)}");
            ucEmerSkedari.ValidationSettings.MaxFileSizeErrorText = rm.GetString("msgSkedarMbi10mb", cultinf);
            hfState.Set("MenuKokeDokumenti", rm.GetString("MenuKokeDokumenti", cultinf));
            hfState.Set("MenuTrupDokumenti", rm.GetString("MenuTrupDokumenti", cultinf));
            hfState.Set("MenuFundDokumenti", rm.GetString("MenuFundDokumenti", cultinf));
            hfState.Set("MenuTrupDokumentiKomision", rm.GetString("MenuTrupDokumentiKomision", cultinf));
            hfState.Set("msgGabimGjateAzhurnimitTeCmimit", rm.GetString("msgGabimGjateAzhurnimitTeCmimit", cultinf));
            hfState.Set("msgNukKeniTeDrejtaNeAsnjeMagazine", rm.GetString("msgNukKeniTeDrejtaNeAsnjeMagazine", cultinf));
            hfState.Set("msgDetajimiVendosurNukEkzistonDoniTaCelni", rm.GetString("msgDetajimiVendosurNukEkzistonDoniTaCelni", cultinf));
            hfState.Set("msgKodiDateSkadenceDuhetFormat", rm.GetString("msgKodiDateSkadenceDuhetFormat", cultinf));
            hfState.Set("msgKySerialMundTePerdoretVetemPerLoan", rm.GetString("msgKySerialMundTePerdoretVetemPerLoan", cultinf));
            hfState.Set("msgArtikulliEshteInaktiv", rm.GetString("msgArtikulliEshteInaktiv", cultinf));
            hfState.Set("msgNukKryhenVeprimeMeArtikujProdhimProces", rm.GetString("msgNukKryhenVeprimeMeArtikujProdhimProces", cultinf));
            hfState.Set("msgNukKryhenVeprimeMeArtikujProdhim", rm.GetString("msgNukKryhenVeprimeMeArtikujProdhim", cultinf));
            hfState.Set("msgNukMundTeKryeniVeprimeMeArtikujTePerbere", rm.GetString("msgNukMundTeKryeniVeprimeMeArtikujTePerbere", cultinf));
            hfState.Set("msgArtikulliNukIPerketKetijGrupDokumenti", rm.GetString("msgArtikulliNukIPerketKetijGrupDokumenti", cultinf));
            hfState.Set("msgNukMundTePerdorenArtikujtJoDhurate", rm.GetString("msgNukMundTePerdorenArtikujtJoDhurate", cultinf));
            hfState.Set("msgKyDokumentGjeneronDokumentMagazine", rm.GetString("msgKyDokumentGjeneronDokumentMagazine", cultinf));
            hfState.Set("msgDuhetTeCaktoniMagazinenPerArtikujtAfatgjate", rm.GetString("msgDuhetTeCaktoniMagazinenPerArtikujtAfatgjate", cultinf));
            hfState.Set("msgNdodhiNjeGabimGjateMarrjesSeTVSH", rm.GetString("msgNdodhiNjeGabimGjateMarrjesSeTVSH", cultinf));
            hfState.Set("msgNdodhiGabimGjateMarrjesSeTeDhenave", rm.GetString("msgNdodhiGabimGjateMarrjesSeTeDhenave", cultinf));
            hfState.Set("msgZgjidhniNjeArtikullAfatGjate", rm.GetString("msgZgjidhniNjeArtikullAfatGjate", cultinf));
            hfState.Set("msgZgjidhniSerialet", rm.GetString("msgZgjidhniSerialet", cultinf));
            hfState.Set("msgGabimGjateTransferimitTeTeDhenave", rm.GetString("msgGabimGjateTransferimitTeTeDhenave", cultinf));
            hfState.Set("msgSasiaRezervimitDuhetNumer", rm.GetString("msgSasiaRezervimitDuhetNumer", cultinf));
            hfState.Set("msgSasiaRezervimitNukDuhetMeEMadheSeSasiaNeFature", rm.GetString("msgSasiaRezervimitNukDuhetMeEMadheSeSasiaNeFature", cultinf));
            hfState.Set("msgEkzistonArtikullNeGride", rm.GetString("msgEkzistonArtikullNeGride", cultinf));
            hfState.Set("msgTrupiDokumentitNukDuhetLeneBosh", rm.GetString("msgTrupiDokumentitNukDuhetLeneBosh", cultinf));
            hfState.Set("msgNdodhiGabimGjateRuajtesSeTeDhenave", rm.GetString("msgNdodhiGabimGjateRuajtesSeTeDhenave", cultinf));
            hfState.Set("msgNdodhiNjeGabimGjateMarrjesSeMonedhes", rm.GetString("msgNdodhiNjeGabimGjateMarrjesSeMonedhes", cultinf));
            hfState.Set("msgZgjidhKF", rm.GetString("msgZgjidhKF", cultinf));
            hfState.Set("msgZgjidhAutomjetin", rm.GetString("msgZgjidhAutomjetin", cultinf));
            hfState.Set("msgNdodhiGabimGjateMarrjesSeTargesSeAutomjetit", rm.GetString("msgNdodhiGabimGjateMarrjesSeTargesSeAutomjetit", cultinf));
            hfState.Set("msgZgjidhMenyrenTransportit", rm.GetString("msgZgjidhMenyrenTransportit", cultinf));
            hfState.Set("msgZgjidhTransportuesin", rm.GetString("msgZgjidhTransportuesin", cultinf));
            hfState.Set("msgZgjidhAgjentShitje", rm.GetString("msgZgjidhAgjentShitje", cultinf));
            hfState.Set("msgZgjidhKushteDergimi", rm.GetString("msgZgjidhKushteDergimi", cultinf));
            hfState.Set("msgZgjidhKushtePagese", rm.GetString("msgZgjidhKushtePagese", cultinf));
            hfState.Set("msgZgjidhAfateMaturimi", rm.GetString("msgZgjidhAfateMaturimi", cultinf));
            hfState.Set("msgGabimMarrjeKlientFurnitor", rm.GetString("msgGabimMarrjeKlientFurnitor", cultinf));
            hfState.Set("msgGabimMarrjeAdresaKf", rm.GetString("msgGabimMarrjeAdresaKf", cultinf));
            hfState.Set("msgKFnukEkziston", rm.GetString("msgKFnukEkziston", cultinf));
            hfState.Set("msgZgjidhMagazinen", rm.GetString("msgZgjidhMagazinen", cultinf));
            hfState.Set("msgGabimMarrjeMagazine", rm.GetString("msgGabimMarrjeMagazine", cultinf));
            hfState.Set("msgKursiNukMundTeJeteZero", rm.GetString("msgKursiNukMundTeJeteZero", cultinf));
            hfState.Set("msgGabimMarrjeKursi", rm.GetString("msgGabimMarrjeKursi", cultinf));
            hfState.Set("msgGabimKursiMonedha", rm.GetString("msgGabimKursiMonedha", cultinf));
            hfState.Set("msgGabimMarrjeMaturim", rm.GetString("msgGabimMarrjeMaturim", cultinf));
            hfState.Set("msgShenoniMagazinen", rm.GetString("msgShenoniMagazinen", cultinf));
            hfState.Set("msgNukDuhetTeKeteArtikujMeCmimZero", rm.GetString("msgNukDuhetTeKeteArtikujMeCmimZero", cultinf));
            hfState.Set("msgKujdesKaCmimZeroNeRreshtin", rm.GetString("msgKujdesKaCmimZeroNeRreshtin", cultinf));
            hfState.Set("msgZbritjaVlerat", rm.GetString("msgZbritjaVlerat", cultinf));
            hfState.Set("msgLlogariCmimZero", rm.GetString("msgLlogariCmimZero", cultinf));
            hfState.Set("msgTeDhenaTePasakta", rm.GetString("msgTeDhenaTePasakta", cultinf));
            hfState.Set("msgZgjidhniLlojinEVeprimit", rm.GetString("msgZgjidhniLlojinEVeprimit", cultinf));
            hfState.Set("headerPopUpZgjidhArtikullin", rm.GetString("headerPopUpZgjidhArtikullin", cultinf));
            hfState.Set("msgLlojiVeprimitIPanjohur", rm.GetString("msgLlojiVeprimitIPanjohur", cultinf));
            hfState.Set("headerPopUpText", rm.GetString("headerPopUpText", cultinf));
            hfState.Set("msgZgjidhMakro", rm.GetString("msgZgjidhMakro", cultinf));
            hfState.Set("msgGabimMarrjeCmimi", rm.GetString("msgGabimMarrjeCmimi", cultinf));
            hfState.Set("msgGabimMarrjeZbritje", rm.GetString("msgGabimMarrjeZbritje", cultinf));
            hfState.Set("msgZgjidhDetajimArtikulli", rm.GetString("msgZgjidhDetajimArtikulli", cultinf));
            hfState.Set("msgGabimZbritjaNumer", rm.GetString("msgGabimZbritjaNumer", cultinf));
            hfState.Set("msgZbritjaNegativeVlerat", rm.GetString("msgZbritjaNegativeVlerat", cultinf));
            hfState.Set("msgGabimGjeresiaNumer", rm.GetString("msgGabimGjeresiaNumer", cultinf));
            hfState.Set("msgGabimGjeresiaJoZero", rm.GetString("msgGabimGjeresiaJoZero", cultinf));
            hfState.Set("msgGabimGjatesiNumer", rm.GetString("msgGabimGjatesiNumer", cultinf));
            hfState.Set("msgGabimGjatesiaJoZero", rm.GetString("msgGabimGjatesiaJoZero", cultinf));
            hfState.Set("msgSasiPermaseNumer", rm.GetString("msgSasiPermaseNumer", cultinf));
            hfState.Set("msgSasiPermaseJoZero", rm.GetString("msgSasiPermaseJoZero", cultinf));
            hfState.Set("msgSasiaNumer", rm.GetString("msgSasiaNumer", cultinf));
            hfState.Set("msgSasiaNukMundTeJeteZero", rm.GetString("msgSasiaNukMundTeJeteZero", cultinf));
            hfState.Set("msgtvshZgjedhurJoEArtikullit", rm.GetString("msgtvshZgjedhurJoEArtikullit", cultinf));
            hfState.Set("msgtvshZgjedhurJoELlogarise", rm.GetString("msgtvshZgjedhurJoELlogarise", cultinf));
            hfState.Set("msgPerqindjaDuhetJeteNumer", rm.GetString("msgPerqindjaDuhetJeteNumer", cultinf));
            hfState.Set("msgPerqindjaVleraNdermjet", rm.GetString("msgPerqindjaVleraNdermjet", cultinf));
            hfState.Set("msgVleraDuhetJeteNumer", rm.GetString("msgVleraDuhetJeteNumer", cultinf));
            hfState.Set("msgVleraNumerPozitiv", rm.GetString("msgVleraNumerPozitiv", cultinf));
            hfState.Set("msgZbritjaJoMeMadheTotal", rm.GetString("msgZbritjaJoMeMadheTotal", cultinf));
            hfState.Set("msgVeprimeMonedheNdryshmekf", rm.GetString("msgVeprimeMonedheNdryshmekf", cultinf));
            hfState.Set("msgKursiDuhetNumer", rm.GetString("msgKursiDuhetNumer", cultinf));
            hfState.Set("msgPoTransferohetTeDhenatShtypniPerseriRuaj", rm.GetString("msgPoTransferohetTeDhenatShtypniPerseriRuaj", cultinf));
            hfState.Set("msgZgjidhniNivelin", rm.GetString("msgZgjidhniNivelin", cultinf));
            hfState.Set("msgShenoniDtDok", rm.GetString("msgShenoniDtDok", cultinf));
            hfState.Set("msgShenoniNumrinEDokumentit", rm.GetString("msgShenoniNumrinEDokumentit", cultinf));
            hfState.Set("msgShenoniKursin", rm.GetString("msgShenoniKursin", cultinf));
            hfState.Set("msgZgjidhniKf", rm.GetString("msgZgjidhniKf", cultinf));
            hfState.Set("msgNukLejohetCmimZeroNeGride", rm.GetString("msgNukLejohetCmimZeroNeGride", cultinf));
            hfState.Set("msgDeshironiShperndarjeQendraKosto", rm.GetString("msgDeshironiShperndarjeQendraKosto", cultinf));
            hfState.Set("msgDeshironiShperndQendraKostoMag", rm.GetString("msgDeshironiShperndQendraKostoMag", cultinf));
            hfState.Set("msgDeshironiTeBeniShperndarjenNeQendraKosto", rm.GetString("msgDeshironiTeBeniShperndarjenNeQendraKosto", cultinf));
            hfState.Set("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit", rm.GetString("msgDoTeBeshShperndarjenNeQKostoTeDifTeKursit", cultinf));
            hfState.Set("msgFaturaDerguaKaseFiskaleSukses", rm.GetString("msgFaturaDerguaKaseFiskaleSukses", cultinf));
            hfState.Set("msgDirektoriaNukEkziston", rm.GetString("msgDirektoriaNukEkziston", cultinf));
            hfState.Set("msgNukKeniAutorizimPerTeRuajturKeteDok", rm.GetString("msgNukKeniAutorizimPerTeRuajturKeteDok", cultinf));
            hfState.Set("msgNukKeniAutorizimKlonim", rm.GetString("msgNukKeniAutorizimKlonim", cultinf));
            hfState.Set("msgNukKeniAutorizimKthim", rm.GetString("msgNukKeniAutorizimKthim", cultinf));
            hfState.Set("msgNukKeniAutorizimKonvertim", rm.GetString("msgNukKeniAutorizimKonvertim", cultinf));
            hfState.Set("msgNukKeniAutorizimPerTeFshireDok", rm.GetString("msgNukKeniAutorizimPerTeFshireDok", cultinf));
            hfState.Set("regjisDokMsgFaturaEshtePrintNeKase", rm.GetString("regjisDokMsgFaturaEshtePrintNeKase", cultinf));
            hfState.Set("labelAdministrimiMsgJeniSigurt", rm.GetString("labelAdministrimiMsgJeniSigurt", cultinf));
            hfState.Set("msgNukKeniTeDrejtaNeKeteAmbjent", rm.GetString("msgNukKeniTeDrejtaNeKeteAmbjent", cultinf));
            hfState.Set("msgNukMundTeLikuidoniNjeFatureDraft", rm.GetString("msgNukMundTeLikuidoniNjeFatureDraft", cultinf));
            hfState.Set("msgNukKonvertohenDokumentatFatOferteKerkese", rm.GetString("msgNukKonvertohenDokumentatFatOferteKerkese", cultinf));
            hfState.Set("msgDokILikuiduar", rm.GetString("msgDokILikuiduar", cultinf));
            hfState.Set("msgTotalFatureKaluarLimitBllokues", rm.GetString("msgTotalFatureKaluarLimitBllokues", cultinf));
            hfState.Set("msgTotalfatureKaluarLimitkf", rm.GetString("msgTotalfatureKaluarLimitkf", cultinf));
            hfState.Set("msgShkruaniKodArtikulli", rm.GetString("msgShkruaniKodArtikulli", cultinf));
            hfState.Set("msgArtikulliNukEkzistonOseJoAutorizim", rm.GetString("msgArtikulliNukEkzistonOseJoAutorizim", cultinf));
            hfState.Set("msgArtikullZgjedhurAfatgjate", rm.GetString("msgArtikullZgjedhurAfatgjate", cultinf));
            hfState.Set("msgGabimRuajtje", rm.GetString("msgGabimRuajtje", cultinf));
            hfState.Set("msgAgjentZgjedhurNjeHere", rm.GetString("msgAgjentZgjedhurNjeHere", cultinf));
            hfState.Set("msgDokNukMundTeKonvertohet", rm.GetString("msgDokNukMundTeKonvertohet", cultinf));
            hfState.Set("msgDownloadPrograminEKasesTeMenu", rm.GetString("msgDownloadPrograminEKasesTeMenu", cultinf));
            hfState.Set("msgNdodhiNjeGabimKodi0001", rm.GetString("msgNdodhiNjeGabimKodi0001", cultinf));
            hfState.Set("msgZgjidhniKlientFurnitorin", rm.GetString("msgZgjidhniKlientFurnitorin", cultinf));
            hfState.Set("msgShtoDetajim", rm.GetString("msgShtoDetajim", cultinf));
            hfState.Set("msgKjoMagazineNukEkziston", rm.GetString("msgKjoMagazineNukEkziston", cultinf));
            hfState.Set("msgSerialetEArtikullitUhoqenSepseNukIPerkasinKesajMagazines", rm.GetString("msgSerialetEArtikullitUhoqenSepseNukIPerkasinKesajMagazines", cultinf));
            hfState.Set("msgGabimGjateMarrjesSeDetajimit", rm.GetString("msgGabimGjateMarrjesSeDetajimit", cultinf));
            hfState.Set("msgKursiRiNdryshonShumeMeKursinMePare", rm.GetString("msgKursiRiNdryshonShumeMeKursinMePare", cultinf));
            hfState.Set("msgFaturatNukJanePrintuar", rm.GetString("msgFaturatNukJanePrintuar", cultinf));
            hfState.Set("regjisDokZgjidhDokPerTeBashkengjitur", rm.GetString("regjisDokZgjidhDokPerTeBashkengjitur", cultinf));
            hfState.Set("msgValidimiKlientit", rm.GetString("msgValidimiKlientit", cultinf));
            hfState.Set("msgShperndarjeNeQendratEKostos", rm.GetString("msgShperndarjeNeQendratEKostos", cultinf));
            hfState.Set("msgZgjidhArkenBankenLupa", rm.GetString("msgZgjidhArkenBankenLupa", cultinf));
            hfState.Set("msgZgjidhMenyrePagese", rm.GetString("msgZgjidhMenyrePagese", cultinf));
            hfState.Set("msgDataDokJashtePeriudhes", rm.GetString("msgDataDokJashtePeriudhes", cultinf));
            hfState.Set("msgZgjidhMenyrePagese", rm.GetString("msgZgjidhMenyrePagese", cultinf));
            hfState.Set("msgJoKthimFatureDraft", rm.GetString("msgJoKthimFatureDraft", cultinf));
            hfState.Set("msgJoKthimDokFatOferteKerkese", rm.GetString("msgJoKthimDokFatOferteKerkese", cultinf));
            hfState.Set("msgJoKthimDokILikuiduar", rm.GetString("msgJoKthimDokILikuiduar", cultinf));
            hfState.Set("msgJoKthimFatureVlereNegative", rm.GetString("msgJoKthimFatureVlereNegative", cultinf));
            hfState.Set("msgVendosniNjeSerialFaturePerKuponatMeFatureTatimore", rm.GetString("msgVendosniNjeSerialFaturePerKuponatMeFatureTatimore", cultinf));
            hfState.Set("msgKyartikullEshteIPerbere", rm.GetString("msgKyartikullEshteIPerbere", cultinf));
            hfState.Set("lblArtikujtPerberes", rm.GetString("lblArtikujtPerberes", cultinf));
            hfState.Set("lblArtikujTePerbere", rm.GetString("lblArtikujTePerbere", cultinf));
            hfState.Set("lblGrupimPerberesish", rm.GetString("lblGrupimPerberesish", cultinf));
            hfState.Set("msgArtikulliJoPerbere", rm.GetString("msgArtikulliJoPerbere", cultinf));
            hfState.Set("msgShkruaniKodArtikulliPerberes", rm.GetString("msgShkruaniKodArtikulliPerberes", cultinf));
            hfState.Set("msgZgjidhniArtikullin", rm.GetString("msgZgjidhniArtikullin", cultinf));
            hfState.Set("msgPlotesoniDetajiminEArtikullit", rm.GetString("msgPlotesoniDetajiminEArtikullit", cultinf));
            hfState.Set("JQgridShtoArtikullAqt", rm.GetString("JQgridShtoArtikullAqt", cultinf));
            hfState.Set("msgShtoArtikull", rm.GetString("msgShtoArtikull", cultinf));
            hfState.Set("popupKategoriShpenzimi", rm.GetString("popupKategoriShpenzimi", cultinf));
            hfState.Set("artMeKodNukExiston", rm.GetString("artMeKodNukExiston", cultinf));
            hfState.Set("labelTitullModal", rm.GetString("labelTitullModal", cultinf));
            hfState.Set("MenuItemMbyll", rm.GetString("MenuItemMbyll", cultinf));
            hfState.Set("msgFaturaEkzistueseEshtePrintuarPrintoKuponTeRi", rm.GetString("msgFaturaEkzistueseEshtePrintuarPrintoKuponTeRi", ci));
            hfState.Set("msgDeshironiTendryshoniCmimet", rm.GetString("msgDeshironiTendryshoniCmimet", cultinf));
            hfState.Set("msgKujdesKursiKembimitNje", MessagesResource.Messages["msgKujdesKursiKembimitNje"]);
            hfState.Set("msgSerialetDoTeFshihen", MessagesResource.Messages["msgSerialetDoTeFshihen"]);
            hfState.Set("msgSerialet1ArtDoTeFshihen1", MessagesResource.Messages["msgSerialet1ArtDoTeFshihen1"]);
            hfState.Set("msgSubjektiMeNiptEshtePasivSipasTativemeDoniTeVazhdoni", MessagesResource.Messages["msgSubjektiMeNiptEshtePasivSipasTativemeDoniTeVazhdoni"]);
            hfState.Set("msgSasiPozitiveKthimShitje", MessagesResource.Messages["msgSasiPozitiveKthimShitje"]);
            hfState.Set("msgSasiMbeturKthimShitje", MessagesResource.Messages["msgSasiMbeturKthimShitje"]);
            hfState.Set("msgArtikullpaDetajim", MessagesResource.Messages["msgArtikullpaDetajim"]);
            hfState.Set("msgDetajimJoLidhur", MessagesResource.Messages["msgDetajimJoLidhur"]);
            hfState.Set("msgDetajimMeKategoriTjeter", MessagesResource.Messages["msgDetajimMeKategoriTjeter"]);
            hfState.Set("msgArtSkaKategoriPerDetajim", MessagesResource.Messages["msgArtSkaKategoriPerDetajim"]);
            hfState.Set("msgExistIDmarreveshjeNukKlonohet", MessagesResource.Messages["msgExistIDmarreveshjeNukKlonohet"]);
            hfState.Set("msgDokNeProcesAprovimi", MessagesResource.Messages["msgDokNeProcesAprovimi"]);
            GridUtil.perktheButonaGride(hfState, cultinf);
            ImbLogger.LogTraceShitje($"Mbaroi metoda mbushHiddenFieldMePerkthime me parametra cultinf:{JsonConvert.SerializeObject(cultinf)}, rm:{JsonConvert.SerializeObject(rm)}");
        }

        private void konfiguroTeDrejtat(int idPerdoruesi, int idNdermarrje, int idViti)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda konfiguroTeDrejtat me parametra idPerdoruesi:{idPerdoruesi}, idNdermarje:{idNdermarrje}, idViti:{idViti}");
            //GIMPROVE merri te gjitha te drejtat njeheresh
            clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Info Artikulli");
            hfTeDrejtaInfoArt.Value = tedrejtaInfo.DAmb.ToString();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Info KF");
            hfTeDrejtaInfoKF.Value = tedrejtaInfo.DAmb.ToString();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Info Llogari");
            hfTeDrejtaInfoLlog.Value = tedrejtaInfo.DAmb.ToString();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
            hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
            string veprimi = (string)hfState["veprimi"];
            if (veprimi != "blerje")
                hfTeDrejtaImportoSeriale.Value = "false";
            else
            {
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Importo seriale");
                hfTeDrejtaImportoSeriale.Value = tedrejtaInfo.DAmb.ToString();
            }
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaArtikullShpejte.aspx");
            hfTeDrejtaArtRi.Value = tedrejtaInfo.DShtim.ToString();
            hfTeDrejtaArtMod.Value = tedrejtaInfo.DMod.ToString();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaShfaqArtikujPerberes.aspx");
            hfTeDrejtaArtPerberes.Value = tedrejtaInfo.DAmb.ToString();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaArkiva.aspx");
            hfTeDrejtaArtImazhe.Value = tedrejtaInfo.DAmb.ToString();
            hfTeDrejtaGrupimPerberes.Value = tedrejtaInfo.DAmb.ToString();
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, clsFunksione.GetKomponente(Page.Request));
            hfTeDrejta.Add("NdryshoCmimShitje", tedrejtaInfo.DNdryshoCmimShitje);
            hfTeDrejta.Add("NdryshoCmimBlerje", tedrejtaInfo.DNdryshoCmimBlerje);
            hfTeDrejta.Add("NdryshoZbritjeAnalitike", tedrejtaInfo.DNdryshoZbritjeAnalitike);
            hfTeDrejta.Add("NdryshoZbritjeTotale", tedrejtaInfo.DNdryshoZbritjeTotale);
            hfTeDrejta.Add("VetemKonvertimFSH", tedrejtaInfo.DKonvertimi);
            hfTeDrejta.Add("KonvertimSipasUSH", tedrejtaInfo.DKonvertimSipasUrdherShitje);
            ImbLogger.LogTraceShitje($"Mbaroi metoda konfiguroTeDrejtat me parametra idPerdoruesi:{idPerdoruesi}, idNdermarje:{idNdermarrje}, idViti:{idViti}");
        }

        private void konfiguroTeDrejtaPerMenu()
        {
            ImbLogger.LogTraceShitje("Filloi metoda konfiguroTeDrejtaPerMenu");
            clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
            var colTeDrejtat = Newtonsoft.Json.JsonConvert.DeserializeObject<colTeDrejtaRoli>(hfState.Get("teDrejtaNivele").ToString());
            tedrejtaInfo = colTeDrejtat.Find(x => x.IdNivelRegjistrimi == Convert.ToInt32(cmbNiveli.Value));
            if (tedrejtaInfo == null)
            {
                tedrejtaInfo = new clsTeDrejtaRoli();
                int idPerdoruesi = Convert.ToInt32(hfState.Get("idPerdoruesi"));
                int idNdermarrje = Convert.ToInt32(hfState.Get("idNdermarrje"));
                int idViti = Convert.ToInt32(hfState.Get("idViti"));
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, clsFunksione.GetKomponente(Page.Request));
            }

            hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
            hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
            hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
            hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
            hfTeDrejta.Add("Fshi", tedrejtaInfo.DFsh);
            hfTeDrejta.Add("Arkiva", tedrejtaInfo.DArkiva);
            hfTeDrejta.Add("Konverto", tedrejtaInfo.DKonverto);
            ImbLogger.LogTraceShitje("Mbaroi metoda konfiguroTeDrejtaPerMenu");
        }

        private void MbushTeDhenaSipasRaportit()
        {
            ImbLogger.LogTraceShitje("Filloi metoda MbushTeDhenaSipasRaportit");
            bool sasipakonvert = false;
            if (Request.QueryString["konfigurim"] != null)
            {
                cmbModeli.Value = Request.QueryString["konfigurim"];
            }
            if (Request.QueryString["filtersasipakonvert"] != null)
            {
                sasipakonvert = bool.Parse(Request.QueryString["filtersasipakonvert"]);
            }
            DataTable dt = !String.IsNullOrWhiteSpace(Request.QueryString["PageId"]) ? mySessionObjects.merrDtNgaSessioni(Session, Request.QueryString["PageId"]) : mySessionObjects.merrDtNgaSessioni(Session);
            colTrupiShitje col = new colTrupiShitje();
            colArtikujt colArtikuj = new colArtikujt();
            colNjesiAdministrative colmag = new colNjesiAdministrative();
            colNjesiteArtikulli colnjesi = new colNjesiteArtikulli();
            colTaksa colTaksa = new colTaksa();
            col.mbushTrupShitjeSipasRaportit(dt, (int)hfState["idNdermarrje"], sasipakonvert, out colArtikuj, out colmag, out colnjesi, out colTaksa);
            object myCols = new { colTrupi = col };
            HfColTrup.Value = JsonConvert.SerializeObject(col);
            colKodbare colKodbare = new colKodbare();
            colKategoriShpenzimi colKategori = new colKategoriShpenzimi();
            colDetajimeArtikulli coldet1 = new colDetajimeArtikulli();
            colDetajimeArtikulli coldet2 = new colDetajimeArtikulli();
            colArtikulliPerberes colArtikujtPerberes = new colArtikulliPerberes();
            foreach (clsArtikulli art in colArtikuj)
            {
                if (art.Klasa == 4)
                    colArtikujtPerberes.merrSipasIdArtikullKryesorePare(art.IdArtikulli, DateTime.Now.Date);
                else
                    colArtikujtPerberes.Add(new clsArtikulliPerberes());
                colKategori.Add(new clsKategoriShpenzimi());
                coldet1.Add(new clsDetajimArtikulli());
                coldet2.Add(new clsDetajimArtikulli());
            }
            HfColArtPerb.Value = JsonConvert.SerializeObject(colArtikujtPerberes);
            HfColArt.Value = JsonConvert.SerializeObject(colArtikuj);
            HfColKodbare.Value = JsonConvert.SerializeObject(colKodbare);
            colLlogarite colLlogari = new colLlogarite();
            HfColllogarite.Value = JsonConvert.SerializeObject(colLlogari);
            HfColKategoriShpenzimi.Value = JsonConvert.SerializeObject(colKategori);
            HfColDetArt.Value = JsonConvert.SerializeObject(coldet1);
            HfColDetArt2.Value = JsonConvert.SerializeObject(coldet2);
            HfColNjesAdminis.Value = JsonConvert.SerializeObject(colmag);
            HfColNjesiArt.Value = JsonConvert.SerializeObject(colnjesi);
            HfColTaksa.Value = JsonConvert.SerializeObject(colTaksa);
            merrLlojeTVSH(int.Parse(cmbModeli.Value.ToString()), (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], colArtikuj, colLlogari, 0);
            ImbLogger.LogTraceShitje("Mbaroi metoda MbushTeDhenaSipasRaportit");
        }

        public void MerrTedhenat(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, clsKokaShitje koka, ResourceManager rm, CultureInfo ci, string shtimModifikim)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda MerrTedhenat me parametra idGjuha:{idGjuha}, idPerdoruesi:{idPerdoruesi}, idViti:{idViti}, idNdermarrje:{idNdermarrje}, koka:{JsonConvert.SerializeObject(koka)}, rm:{JsonConvert.SerializeObject(rm)}, ci:{JsonConvert.SerializeObject(ci)}, shtimModifikim:" + shtimModifikim);
            clsNivelRegjistrimi niveli = new clsNivelRegjistrimi();
            niveli.mbushNivelRegjistrimiSipasIdPaKonvertime(koka.IdNivel);
            double limitShitjeNdermarrje = clsNdermarrje.KtheLimitshitjeNdermarrjes(idNdermarrje);
            if (Request.QueryString["niveli"] != null)
                cmbNiveli.SelectedItem = cmbNiveli.Items.FindByValue(Convert.ToInt32(Request.QueryString["niveli"]));
            else
                cmbNiveli.SelectedItem = cmbNiveli.Items.FindByText(niveli.Pershkrimi);
            bool konvNgaUshNeFsh = false;
            if ((bool)hfTeDrejta["KonvertimSipasUSH"])
            {
                if (shtimModifikim == "modifikim" && niveli.Kodi == "FSH")
                    konvNgaUshNeFsh = clsKokaShitje.eshteDokKonvertuarNgaNiveli(koka.IdShitjeKoka, idNdermarrje, "USH");
                else if (shtimModifikim == "konvertim" || shtimModifikim == "konvertimblerje")
                {
                    if (clsNivelRegjistrimi.ktheKodNivelRegjistrimi(int.Parse(Request.QueryString["niveli"])) == "FSH" && niveli.Kodi == "USH")
                        konvNgaUshNeFsh = true;
                }
            }
            if (cmbModeli.Text.StartsWith("VFONE") && koka.IdStatusDok == 0)
                cbGaranci.Checked = true;

            hfState.Set("Status", koka.IdStatusDok);
            hfState.Set("konvNgaUshNeFsh", konvNgaUshNeFsh);
            hfState.Set("idstatusdok", koka.IdStatusDok);
            if (koka.StatusAprovimi != 0)
                lblStatusAprovimi.Text = ((StatusAprovimi)koka.StatusAprovimi).ToString().Replace('_', ' ');
            string veprimi = (string)hfState["veprimi"];

            mbushComboKonfigurimet(idPerdoruesi, idNdermarrje, 0, veprimi, idGjuha, rm, ci);
            if (Request.QueryString["konfigurim"] != null)
            {
                cmbModeli.Value = Request.QueryString["konfigurim"];
            }
            else if (hfShtimModifikim.Value == "kthimVod")
            {
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasKod("FBKTHIM", idNdermarrje);
                cmbModeli.Value = konf.IdKonfigAmbjente.ToString();
            }
            else if (hfShtimModifikim.Value == "bli")
            {
                DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
                konf.mbushKonfigAmbjSipasKod("FBD", idNdermarrje);//fbd
                cmbModeli.Value = konf.IdKonfigAmbjente.ToString();
            }
            else
            {
                if (koka.IdKonfigAmbjente != 0)
                {
                    clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti();
                    konfig.mbushKonfiguriminMeID(koka.IdKonfigAmbjente);
                    cmbModeli.Text = konfig.KodKonfigAmbjente;
                }
            }
            int idKonfigurimi = int.Parse(cmbModeli.Value.ToString());
            if (shtimModifikim != "konvertim" && shtimModifikim != "konvertimblerje" && !(shtimModifikim == "klonim" && !Convert.ToBoolean(Request.QueryString["kategoriNjejte"])))
            {
                ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup1);
                ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup1, idNdermarrje, 1, koka.IdKonfigAmbjente, idPerdoruesi);
                ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup2);
                ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup2, idNdermarrje, 2, koka.IdKonfigAmbjente, idPerdoruesi);
                ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup3);
                ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup3, idNdermarrje, 3, koka.IdKonfigAmbjente, idPerdoruesi);
                if (koka.IdGrup1 != 0)
                    cmbGrup1.Value = koka.IdGrup1.ToString();
                if (koka.IdGrup2 != 0)
                    cmbGrup2.Value = koka.IdGrup2.ToString();
                if (koka.IdGrup3 != 0)
                    cmbGrup3.Value = koka.IdGrup3.ToString();
            }
            else
            {
                ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup1);
                ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup1, idNdermarrje, 1, idKonfigurimi, idPerdoruesi);
                ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup2);
                ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup2, idNdermarrje, 2, idKonfigurimi, idPerdoruesi);
                ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup3);
                ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup3, idNdermarrje, 3, idKonfigurimi, idPerdoruesi);
                if (koka.IdGrup1 != 0 && clsGrupimDokumentiKoka.ekzistonGrupiPerKeteLlojDok(koka.IdGrup1, idKonfigurimi))
                    cmbGrup1.Value = koka.IdGrup1.ToString();
                if (koka.IdGrup2 != 0 && clsGrupimDokumentiKoka.ekzistonGrupiPerKeteLlojDok(koka.IdGrup2, idKonfigurimi))
                    cmbGrup2.Value = koka.IdGrup2.ToString();
                if (koka.IdGrup3 != 0 && clsGrupimDokumentiKoka.ekzistonGrupiPerKeteLlojDok(koka.IdGrup3, idKonfigurimi))
                    cmbGrup3.Value = koka.IdGrup3.ToString();
            }
            if (((shtimModifikim == "konvertim" || shtimModifikim == "konvertimblerje") && !String.IsNullOrEmpty(Request.QueryString["klientKonv"]) && Request.QueryString["klientKonv"] != "-1") || (!(shtimModifikim == "konvertim" || shtimModifikim == "konvertimblerje") && koka.IdKlientFurnitor != 0))
            {
                hfkf.Value = koka.IdKlientFurnitor.ToString();
                clsKlientFurnitor klienti = new clsKlientFurnitor();
                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(btnKlienti, koka.IdKlientFurnitor);
                klienti.MbushKlientFurnitorSipasId(koka.IdKlientFurnitor);
                txtKrediti.Text = klienti.LimitParalajmerues.ToString();
                txtLimit.Text = klienti.LimitBllokues.ToString();
                if (klienti.MaturimiKF != 0)
                    btnMaturimi.Value = klienti.MaturimiKF.ToString();
                txtPerqindje.Text = klienti.ZbritjeTotal.ToString();
                //%e zbritjes se klientit*totalin ne monedhe baze
                txtVlefte.Text = ((double.Parse(klienti.ZbritjeTotal.ToString()) / 100) * (koka.Totali / koka.Kursi)).ToString();
                txtEmri.Text = klienti.EmertimiKF;
            }
            hfPiket.Value = koka.Pike.ToString();
            int idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(idGjuha, idKonfigurimi, idNdermarrje, 506, "btnKlienti", koka.IdKlientFurnitor, false);
            clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(idKonfigurimi);
            clsFormatKonfigTrup formatMonedhe = DbCore.DbShare.clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            hfState.Set("formatMonedhe", JsonConvert.SerializeObject(formatMonedhe));
            data_DateEdit.Date = koka.DtDok;
            DtFillimi_DateEdit.Date = koka.DtFillimi;
            DtMbarimi_DateEdit.Date = koka.DtMbarimi;
            DtFature_DateEdit.Date = koka.DtFature;
            dteAfatiKohor.Date = koka.AfatKohor;
            txtCash.Text = koka.Cash.ToString();
            cmbMenyrePagese.Value = koka.IdMenyrePagese.ToString();
            cmbLlojMarreveshje.SelectedIndex = koka.IdLlojMarreveshje;
            cmbStatusMarreveshje.SelectedIndex = (shtimModifikim == "klonim" && Request.QueryString["modMarreveshje"] != "modifikim") ? 0 : (int)koka.StatusMarreveshje - 1;
            txtIdMarreveshje.Text = koka.IdMarreveshje;
            //cbMarreveshjeAktive.Checked = koka.AktiveMarrveshja;
            cmbLlojZbritje.SelectedItem = cmbLlojZbritje.Items.FindByText(koka.ZbritjeNeVlere == false ? "Perqindje" : "Vlere");
            txtAdresaFaturimit.Text = koka.AdresaFaturimit;
            txtEmriKlienti.Text = koka.EmerKlienti;
            txtAdresaDergimit.Text = koka.AdresaDergimit;
            txtPershkrimi.Text = koka.Pershkrimi;
            cbKasa.Checked = koka.Kase;
            txtShenime2.Text = koka.Shenime2;
            cbKartaPaPagese.Checked = koka.KartaPaPagese;
            txtKerkuarNga.Text = koka.KerkuarNga;
            txtNrDokMagazine.Text = (shtimModifikim == "modifikim") ? koka.NrDokMagazine : "";
            txtIIC.Text = (shtimModifikim == "modifikim") ? koka.IIC : "";
            txtNIVF.Text = (shtimModifikim == "modifikim") ? koka.NIVF : "";
            if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
            {
                cmbTipiIVetefaturimit.Text = koka.TipiIVetefaturimit;
                txtEIC.Text = (shtimModifikim == "modifikim") ? koka.EIC : "";
                txtNivfKthim.Text = (shtimModifikim == "modifikim") ? koka.NivfKthim : "";
                if (koka.Procesi != 0)
                {
                    if (koka.ktheKodDhePershkrimProcesi(koka.Procesi).Rows[0].ItemArray[0].ToString() == " ()")
                        cmbProcesi.Text = "";
                    else
                        cmbProcesi.Text = koka.ktheKodDhePershkrimProcesi(koka.Procesi).Rows[0].ItemArray[0].ToString();

                }
                if (koka.EInvoiceType != 0)
                {
                    if (koka.ktheKodDhePershkrimTipiEinvoice(koka.EInvoiceType).Rows[0].ItemArray[0].ToString() == " ()")
                        cmbeInvoiceType.Text = "";
                    else
                        cmbeInvoiceType.Text = koka.ktheKodDhePershkrimTipiEinvoice(koka.EInvoiceType).Rows[0].ItemArray[0].ToString();
                }
                if (!String.IsNullOrEmpty(koka.NIVF) && shtimModifikim == "modifikim")
                {
                    txtNIVF.ReadOnly = true;
                    cbFiskalizo.Checked = true;
                    hfState.Set("MosModifikoTrup", true);
                }
                if (!String.IsNullOrEmpty(koka.EIC) && shtimModifikim == "modifikim")
                {
                    txtEIC.ReadOnly = true;
                    cbEinvoice.Checked = true;
                }
                if (shtimModifikim == "kthim" || shtimModifikim == "kthimvod")
                    txtNivfKthim.Text = koka.NIVF;

            }
            if (koka.IdOperator != 0)
                cmbOperatori.Value = koka.IdOperator.ToString();
            DtKerkese_DateEdit.Date = koka.DateKerkese;
            CacheLayer.GlobalCacheManager.MyPageCache["IdDokTransferimNga"] = koka.IdDokTransferimNga;
            string kushtMMDT = clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "MMDT");
            clsKlientFurnitor klientiTeKoka = new clsKlientFurnitor(koka.IdKlientFurnitor);

            if (!string.IsNullOrEmpty(koka.Kontakti))
                txtKontakti.Text = koka.Kontakti;
            else if (kushtMMDT == "ModifikoDok" && !string.IsNullOrEmpty(klientiTeKoka.TelKF) && koka.IdDokNga == 0 && koka.IdStatusDok == 0)
                txtKontakti.Text = klientiTeKoka.TelKF;

            if (koka.Kupon && (koka.Totali > limitShitjeNdermarrje || shtimModifikim != "konvertim"))
                cbKupon.Checked = koka.Kupon;
            else if (kushtMMDT == "ModifikoDok" && klientiTeKoka.Kupon && koka.IdDokNga == 0 && koka.IdStatusDok == 0)
                cbKupon.Checked = klientiTeKoka.Kupon;
            else
                cbKupon.Checked = false;

            if (!string.IsNullOrEmpty(koka.NiptK))
            {
                txtNipt.Text = koka.NiptK;
            }
            else if (kushtMMDT == "ModifikoDok" && !string.IsNullOrEmpty(klientiTeKoka.NiptiKF) && koka.IdDokNga == 0 && koka.IdStatusDok == 0)
            {
                txtNipt.Text = klientiTeKoka.NiptiKF;
            }

            if (!string.IsNullOrEmpty(koka.QytetiK))
            {
                txtQytetiK.Text = koka.QytetiK;
            }
            else if (kushtMMDT == "ModifikoDok" && !string.IsNullOrEmpty(klientiTeKoka.EmriQytetitKF) && koka.IdDokNga == 0 && koka.IdStatusDok == 0)
            {
                txtQytetiK.Text = klientiTeKoka.EmriQytetitKF;
            }

            if (koka.IdKategoriSeriali > 0)
                cmbKategoriSeriali.Value = koka.IdKategoriSeriali.ToString();
            if (koka.Koordinata != null && koka.Koordinata != "")
            {
                btneCaktoNeHarte.Text = clsFunksione.formatoKoordinate(koka.Koordinata);
                hfState.Set("geom", $"[\"{koka.Koordinata}\"]");
            }
            else
            {
                btneCaktoNeHarte.Text = "";
                hfState.Set("geom", "");
            }
            cbShpenzimeJoTeZbritshme.Checked = koka.ShpenzimeJoTeZbritshme;
            if (shtimModifikim != "konvertimblerje" && !(shtimModifikim == "klonim" && !Convert.ToBoolean(Request.QueryString["kategoriNjejte"])))
            {
                if (koka.IdRaportDesing == 0)
                    cmbFormatiPrintimit.Text = "";
                else
                    cmbFormatiPrintimit.Value = koka.IdRaportDesing.ToString();
            }
            else
            {
                int idKonfigurim = Convert.ToInt32(cmbModeli.Value);
                cmbFormatiPrintimit.Value = clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigurim, "cmbFormatiPrintimit", 506);
                cbPrinto.Checked = Convert.ToBoolean(clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(idKonfigurim, "cbPrinto", 506));
            }

            if (koka.IdAutomjet != 0)
            {
                ConfigureAspxComboBox.mbushComboAutomjetiByID(btneAutomjeti, koka.IdAutomjet);
                txtTarga.Text = (clsAutomjete.ktheTargeAutomjetSipasId(koka.IdAutomjet));
            }

            txtKilometra.Text = koka.KilometraAuto.ToString();
            clsKokaMagazina kokam = new clsKokaMagazina();
            int idKategoria = niveli.IdKategori;
            kokam.mbushKokaMagazinaSipasIDGjenerues(koka.IdShitjeKoka, idKategoria == 1 ? 2 : 1, koka.IdKonfigAmbjente);

            if (kokam != null) //TOCHECK Nestila
            {
                hfIdMag.Value = kokam.IdKokaMagazina.ToString();
                if (kokam.IdMagazina != 0)
                {
                    int idMag = Convert.ToInt32(kokam.IdMagazina);
                    ConfigureAspxComboBox.mbushComboMagazinatMeID(idNdermarrje, btnMagazina, idPerdoruesi, true, 0, true, idMag);
                }
                txtPershkrimMagazine.Text = clsNjesiAdministrative.kthePershkrimNjesiAdministrativeSipasiDPaAutorizime(kokam.IdMagazina);
                //  btnMagazina.Text = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(kokam.IdMagazina);
            }
            colAdresatKlientFurnitor adresat = new colAdresatKlientFurnitor();
            if (koka.IdKlientFurnitor != 0)
            {
                adresat = new colAdresatKlientFurnitor(koka.IdKlientFurnitor);
                if (adresat.Count > 0)
                {
                    if (adresat[0].IdTipAdrese == 1)    //adresa biznesi
                        txtAdresaFaturimit.Text = adresat[0].Adresa;
                    if (adresat[0].IdTipAdrese == 2)    //adrese magazine                                                 
                        txtAdresaDergimit.Text = adresat[0].Adresa;
                }
            }
            if (koka.AdresaFaturimit != string.Empty)
                txtAdresaFaturimit.Text = koka.AdresaFaturimit;
            if (koka.AdresaDergimit != string.Empty)
                txtAdresaDergimit.Text = koka.AdresaDergimit;
            if (koka.IdMenyreTransporti != 0)
                btnMenyreTransporti.Text = new clsMenyreTransporti(koka.IdMenyreTransporti).KodiMenyreTransporti;
            txtNumer.Text = koka.NrDok;
            dateTransportimi_DateEdit.Date = koka.DtTransportimi;
            txtNumerSerial.Text = koka.NrSerial;
            if (koka.IdKushtDergimi != 0)
                btnKushtDergimi.Text = new clsKushtDergimi(koka.IdKushtDergimi).KodiKushtDergimi;
            if (koka.IdDegeAdministrative != 0)
            {
                cmbDegeAdministrative.Value = koka.IdDegeAdministrative.ToString();
                txtPershkrimDege.Text = clsDegeAdministrative.mbushPershkrimDegeAdministrativeSipasiD(koka.IdDegeAdministrative);
            }
            if (shtimModifikim != "konvertimblerje" && !(shtimModifikim == "klonim" && !Convert.ToBoolean(Request.QueryString["kategoriNjejte"])) && koka.IdPikeShitjeFurnizimi.ToString() != "0")
                cmbPikeShitjeFurnizimi.Value = koka.IdPikeShitjeFurnizimi.ToString();
            cmbDogana.Value = koka.Dogana.ToString();
            txtNumerProjekti.Text = koka.NrProjekt;
            string muajRaportimi = koka.MuajRaportimi == 0 ? Enum.GetName(typeof(DbCore.DbListPagesat.Muajt), koka.DtDok.Month) : Enum.GetName(typeof(DbCore.DbListPagesat.Muajt), koka.MuajRaportimi);
            string vitRaportimi = koka.IdVitRaportimi == 0 ? Convert.ToString(koka.DtDok.Year) : (cmbVitRaportimi.Items.FindByValue(Convert.ToString(koka.IdVitRaportimi))).Text;
            cmbMuajRaportimi.Text = muajRaportimi;
            cmbVitRaportimi.Text = vitRaportimi;
            if (clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "DK") != "WebService")
                hfState.Set("printuarKase", koka.Kase);
            else
            {
                clsPrintimeKase print = new clsPrintimeKase();
                print.merrDergimeKaseSipasIdShitje(koka.IdShitjeKoka);
                hfState.Set("printuarKase", koka.Kase && print.Derguar && print.Printuar);
            }

            string ngjyra = clsKokaShitje.merrNgjyreKonvertime(false, koka.IdNdermarrje, koka.IdShitjeKoka);
            string ngjyra2 = clsKokaShitje.merrNgjyreKonvertimeMag(koka.IdNdermarrje, koka.IdShitjeKoka);
            string ngjyra3 = clsKokaShitje.merrNgjyreKonvertime(true, koka.IdNdermarrje, koka.IdShitjeKoka);
            if ((!string.IsNullOrEmpty(ngjyra) && ngjyra != "gri") || (!string.IsNullOrEmpty(ngjyra2) && ngjyra2 != "gri") || (!string.IsNullOrEmpty(ngjyra3) && ngjyra3 != "gri"))
                hfState.Set("mesazhKonvertuar", "Jeni i sigurte? Dokumenti " + koka.NrDok + " eshte i konvertuar.");
            else hfState.Set("mesazhKonvertuar", "");
            if (koka.IdAgjent != 0)
            {
                btnAgjenti.Text = new clsAgjentShitje(koka.IdAgjent).KodiAgjentShitje;
                txtPerqindjeAgjent.Text = koka.PerqindjeAgjenti.ToString();
            }
            if (koka.IdAgjenti2 != 0)
            {
                btnAgjenti2.Text = new clsAgjentShitje(koka.IdAgjenti2).KodiAgjentShitje;
                txtPerqindjeAgjent2.Text = koka.PerqindjeAgjenti2.ToString();
            }
            if (koka.IdAgjenti3 != 0)
            {
                btnAgjenti3.Text = new clsAgjentShitje(koka.IdAgjenti3).KodiAgjentShitje;
                txtPerqindjeAgjent3.Text = koka.PerqindjeAgjenti3.ToString();
            }

            ConfigureAspxComboBox.mbushComboArkat(idNdermarrje, idPerdoruesi, btneArka, cmbMenyrePagese.Text == "Karte krediti");

            if (koka.IdArka != 0)
            {
                DbCore.DbArkaBanka.clsBanka banka = new DbCore.DbArkaBanka.clsBanka();
                banka.mbushBanke(koka.IdArka);
                btneArka.Value = banka.KodiBanka;
            }
            hfState.Set("colKlienteFurnitoreVartes", JsonConvert.SerializeObject(koka.ColKlienteFurnitoreVartes));
            hfState.Set("VleraMarreveshje", JsonConvert.SerializeObject(mySessionObjects.MerrVlereMarreveshjeNgaSessioni(Session)));
            if (koka.IdTransportues != 0)
            {
                btnTransportues.Text = clsTransportues.merrEmertimTransportuesSipasId(koka.IdTransportues);
            }
            txtMarresi.Text = koka.Marresi;
            if (koka.IdMonedha != 0)
            {
                string kodiMonedha = clsMonedha.ktheKodMonedheSipasId(koka.IdMonedha);
                cmbMonedha.Text = kodiMonedha;//new clsMonedha(koka.IdMonedha).KodiMonedha;
                cmbMonedhaPagese.Text = kodiMonedha; // new clsMonedha(koka.IdMonedha).KodiMonedha;
            }
            txtKursi.Text = koka.Kursi.ToString();
            txtKursiPagese.Text = koka.Kursi.ToString();

            if (koka.DtMaturimi != DateTime.MinValue || Convert.ToString(koka.DtMaturimi) != "01/01/0001 00:00:00")
                dateMaturimi_DateEdit.Date = koka.DtMaturimi;
            else
                dateMaturimi_DateEdit.Date = koka.DtDok;

            if (hfShtimModifikim.Value == "kthimVod" || hfShtimModifikim.Value == "bli")
            {
                data_DateEdit.Date = DateTime.Today.Date;
                txtNumer.Text = "";
                txtNumerSerial.Text = "";
                txtPershkrimi.Text = "";
            }
            if (koka.IdKarta != 0 && shtimModifikim != "konvertim")
            {
                clsKarta k = new clsKarta(koka.IdKarta);
                ConfigureAspxComboBox.KonfiguroComboBoxKartaById(cmbKarta, koka.IdKarta);
                txtPike.Text = koka.Pike.ToString();
                clsPolitikeKarta pol = new clsPolitikeKarta(k.IdPolitike);

                if (pol.Lloji == 1 || pol.Lloji == 2) //politike me pike ose politike me zbritje&pike
                {
                    int total = 0;
                    int p = clsKarta.KthePikeKarte(k.IdKarta, k.IdNdermarrje);
                    if (p == -999)
                        total = 0;
                    else
                        total = (p - koka.Pike);

                    if (shtimModifikim == "klonim") total = total + koka.Pike;

                    txtTotalPike.Text = total.ToString();
                    hfTotalPikesh.Value = total.ToString();
                }

                hfPolitike.Value = JsonConvert.SerializeObject(pol);
                hfKarta.Value = JsonConvert.SerializeObject(k);
            }
            if (koka.IdFaza != 0)
            {
                clsFazaKontrate faza = new clsFazaKontrate(koka.IdFaza);
                cmbFaza.Value = koka.IdFaza;
                cmbFaza.Text = faza.Pershkrimi;
            }
            txtShoferi.Text = koka.Shoferi;
            txtTarga2.Text = koka.TargaShoferit;
            if (koka.IdKushtPagese != 0)
            {
                clsKushtPageseKoka clsKoka = new clsKushtPageseKoka(koka.IdKushtPagese);
                btnKushtPagese.Text = clsKoka.KodiKushtPagese;
            }
            double uljePerqindje = (koka.Totali != 0 ? Math.Round((koka.Zbritje / koka.Totali) * 100, 2) : 0);
            double nenTotali = koka.TotaliMeZbritjeMeTVSH != 0 ? koka.Totali * (koka.TotaliMeZbritjeMeTVSH - koka.Tvsh) / koka.TotaliMeZbritjeMeTVSH : 0;
            double zbritjePaTvsh = nenTotali - (koka.TotaliMeZbritjeMeTVSH - koka.Tvsh);
            if (koka.Kursi != 0)
            {
                txtTotal1.Text = koka.TotaliMeZbritjeMeTVSH.ToString(); //totali ne monedhe fature/kursin
                txtTotalMeZbritje1.Text = (nenTotali).ToString();
                txtTVSH1.Text = koka.Tvsh.ToString();
                txtTotaliPaTVSH1.Text = (zbritjePaTvsh).ToString();
                txtTotaliMeZbritjePaTVSH1.Text = (nenTotali - zbritjePaTvsh).ToString();
            }
            txtTotal2.Text = (koka.TotaliMeZbritjeMeTVSH * koka.Kursi).ToString();
            txtTotalMeZbritje2.Text = (nenTotali * koka.Kursi).ToString();
            txtTVSH2.Text = (koka.Tvsh * koka.Kursi).ToString();
            txtTotaliPaTVSH2.Text = (zbritjePaTvsh * koka.Kursi).ToString();
            txtTotaliMeZbritjePaTVSH2.Text = ((nenTotali - zbritjePaTvsh) * koka.Kursi).ToString();
            if (shtimModifikim == "konvertim" || shtimModifikim == "konvertimblerje" || shtimModifikim == "rezervim")
            {
                clsPerdorues perdoruesi = new clsPerdorues(idPerdoruesi);
                lblKrijuesi.Text = perdoruesi.PerdoruesUsername;
            }
            else
            {
                clsPerdorues perdoruesi = new clsPerdorues(koka.IdKrijuesi);
                lblKrijuesi.Text = perdoruesi.PerdoruesUsername;
            }

            int[] ids = new int[0];
            var previousPageID = DbCore.IMBUtils.Types.Converter.MerrVlereOseDefault<string>(Request.QueryString["pageCacheId"]);
            if (!string.IsNullOrEmpty(previousPageID))
            {
                var pageCache = GlobalCacheManager.GetPageCacheByPageID(previousPageID);
                ids = (int[])pageCache["idkonvertimi"];
            }

            if ((shtimModifikim == "konvertim" || shtimModifikim == "konvertimblerje") && (ids.Length > 1))
            {
                DataTable dt = colKokaShitje.merrZbritjeSipasKokave(ids);
                if (dt.Rows.Count > 0)
                {
                    txtVlefte.Text = dt.Rows[0]["zbritje"].ToString();
                    if (dt.Rows[0]["totali"].ToString() == "0")
                        txtPerqindje.Text = "0";
                    else
                        txtPerqindje.Text = ((decimal.Parse(dt.Rows[0]["zbritje"].ToString()) / decimal.Parse(dt.Rows[0]["totali"].ToString())) * 100).ToString();
                }
                else
                {
                    txtVlefte.Text = "0";
                    txtPerqindje.Text = "0";
                }
            }
            else
            {
                txtVlefte.Text = (koka.Zbritje).ToString();
                txtPerqindje.Text = (koka.PerqindjeZbritje).ToString();
            }

            merrDokumentaKonvertuar(koka.IdShitjeKoka, shtimModifikim);
            dateRegjistrimi_DateEdit.Date = koka.DtRegjistrimi;

            bool autorizimet = true;
            if (shtimModifikim != "klonim" && shtimModifikim != "konvertim" && shtimModifikim != "konvertimblerje")
                autorizimet = clsKokaShitje.kaAutorizime(koka.IdShitjeKoka, idPerdoruesi);
            hfAutorizimi.Value = autorizimet.ToString();
            if (shtimModifikim == "modifikim")
            {
                DataTable dtlidhur = koka.merrIdsDokLidhur();

                hfLidhur.Value = (dtlidhur.Rows.Count != 0).ToString();

                if (dtlidhur.Columns.Contains("tipi") && dtlidhur.Rows.Count == 1)
                {
                    if (Convert.ToString(dtlidhur.Rows[0]["tipi"]) == "transferuarngamema")
                    {
                        hfLidhur.Value = bool.FalseString;
                        hfState.Set("MosModifikoTrup", true);

                    }
                }

                colGaranciArtikulli garanci = new colGaranciArtikulli(koka.IdShitjeKoka); //Todo Emanuela - pyet Getsonin
                // eshte shtuar qe te mos modifikohet trupi i nje dokumenti per te cilin jane printuar garancite
                if (hfLidhur.Value == "False" && garanci.Count > 0)
                    hfLidhur.Value = "True";
                //if (koka.FaturePermbledhese)
                if (koka.FaturePermbledhese && koka.IdStatusDok != 0)
                    hfLidhur.Value = "True";
                if (!autorizimet)
                    hfLidhur.Value = "True";
                clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti(koka.IdKonfigAmbjente);
                if ((konf.KodKonfigAmbjente == "FBKTHIM" || konf.KodKonfigAmbjente == "FBD") && koka.IdStatusDok != 4)
                    hfLidhur.Value = "True";
                bool tollona = clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "RSHTT") == "Po";
                bool tollonakastrati = clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "RSHTTK") == "Po";
                bool tollonakastratielektronik = clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "RSHTTKE") == "Po";
                if (tollona && (DbCore.DbTollona.clsShitjeMeSerial.kaTollonaShitja(koka.IdShitjeKoka) || !(clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "ZT") == "Jo")))
                {
                    hfLidhur.Value = "True";
                }
                if (koka.IdStatusDok == 1 && tollonakastrati && (DbCore.DbTollona.clsTollonaLeter.kaTollonaShitja(koka.IdShitjeKoka) || !(clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "ZT") == "Jo")))
                {
                    hfLidhur.Value = "True";
                }
                if (tollonakastratielektronik && DbCore.DbTollona.clsTollonaElektronik.kaTollonaShitja(koka.IdShitjeKoka))
                {
                    hfLidhur.Value = "True";
                }
                AspxWebControlUtils.ShtoLidhje(idPerdoruesi, idViti, idNdermarrje, hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, idGjuha);
                shtoGauget(hl, koka);
            }
            if (hfShtimModifikim.Value == "kthimVod" || hfShtimModifikim.Value == "bli")
                hfLidhur.Value = "True";
            if (hfShtimModifikim.Value == "klonim" && Request.QueryString["modMarreveshje"] == "modifikim")
                hfLidhur.Value = "True";
            colNivelRegjistrimi nivele = new colNivelRegjistrimi();
            colKonfigurimAmbjenti colKonfig = new colKonfigurimAmbjenti();
            nivele.mbushKonvertimeNiveli(koka.IdNivel, idPerdoruesi);
            foreach (clsNivelRegjistrimi regj in nivele)
            {
                clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
                colKonfig.mbushKonfigAmbjSipasIdKategoriIdNivel(regj.IdKategori, regj.IdNivel, idPerdoruesi, idGjuha, true);
            }
            if (colKonfig.Count > 0)
                hfKonv.Value = "Po";
            colSerialeUnikeMagazina serialeUnike = new colSerialeUnikeMagazina(kokam.IdKokaMagazina, idNdermarrje);
            hfState.Set("KaSeriale", serialeUnike.Count > 0);
            mySessionObjects.RuajNeSession<colSerialeUnikeMagazina>(Session, serialeUnike, Constants.SERIALE_UNIKE_TE_NGARKUAR, hfState["guidString"].ToString());
            ImbLogger.LogTraceShitje($"Mbaroi metoda MerrTedhenat me parametra idGjuha:{idGjuha}, idPerdoruesi:{idPerdoruesi}, idViti:{idViti}, idNdermarrje:{idNdermarrje}, koka:{JsonConvert.SerializeObject(koka)}, rm:{JsonConvert.SerializeObject(rm)}, ci:{JsonConvert.SerializeObject(ci)}, shtimModifikim:" + shtimModifikim);
        }

        public void MerrTedhenatRez(int idGjuha, int idKonfig, int idPerdoruesi, int idViti, int idNdermarrje, clsKokaRezervime koka, ResourceManager rm, CultureInfo ci, string hfShtimModifikimValue)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda MerrTedhenatRez me parametra idGjuha:{idGjuha}, idKonfig:{idKonfig}, idPerdoruesi:{idPerdoruesi}, idViti:{idViti}, idNdermarrje:{idNdermarrje}, koka:{JsonConvert.SerializeObject(koka)}, rm:{JsonConvert.SerializeObject(rm)}, ci:{JsonConvert.SerializeObject(ci)}, hfShtimModifikimValue:" + hfShtimModifikimValue);
            clsNivelRegjistrimi niveli = new clsNivelRegjistrimi();
            niveli.mbushNivelRegjistrimiSipasIdPaKonvertime(koka.IdNivel);
            if (Request.QueryString["niveli"] != null)
                cmbNiveli.SelectedItem = cmbNiveli.Items.FindByValue(Convert.ToInt32(Request.QueryString["niveli"]));
            else
                cmbNiveli.SelectedItem = cmbNiveli.Items.FindByText(niveli.Pershkrimi);

            string veprimi = (string)hfState["veprimi"];
            mbushComboKonfigurimet(idPerdoruesi, idNdermarrje, 0, veprimi, idGjuha, rm, ci);

            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup1);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup1, idNdermarrje, 1, Convert.ToInt32(cmbModeli.Value), idPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup2);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup2, idNdermarrje, 2, Convert.ToInt32(cmbModeli.Value), idPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup3);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup3, idNdermarrje, 3, Convert.ToInt32(cmbModeli.Value), idPerdoruesi);
            //clsFunksione.mbushComboKlientet(btnKlienti, cmbModeli.Text.Split(';')[0], idPerdoruesi, idNdermarrje); 
            if (koka.IdKlientFurnitor != 0)
            {
                clsKlientFurnitor klienti = new clsKlientFurnitor();
                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(btnKlienti, koka.IdKlientFurnitor);
                klienti.MbushKlientFurnitorSipasId(koka.IdKlientFurnitor);
                txtKrediti.Text = klienti.LimitParalajmerues.ToString();
                txtLimit.Text = klienti.LimitBllokues.ToString();
                if (klienti.MaturimiKF != 0)
                    btnMaturimi.Value = klienti.MaturimiKF.ToString();
                txtPerqindje.Text = klienti.ZbritjeTotal.ToString();
                //%e zbritjes se klientit*totalin ne monedhe baze
                txtVlefte.Text = "0";
                txtEmri.Text = klienti.EmertimiKF;
                clsLlogari llog = new clsLlogari(klienti.IdLlogari);
                cmbMonedha.Value = llog.IdMonedha.ToString();
                cmbMonedhaPagese.Value = llog.IdMonedha.ToString();
                clsKurset kurs = new clsKurset(llog.IdMonedha, koka.DtDok);
                txtKursi.Text = (kurs.VleraKursi == 0 ? 1 : kurs.VleraKursi).ToString();
                txtKursiPagese.Text = (kurs.VleraKursi == 0 ? 1 : kurs.VleraKursi).ToString();
                hfState.Set("idNivelCmimiRez", klienti.IdNivelCmimi);
            }
            else
            {
                txtKursi.Text = "1";
                txtKursiPagese.Text = "1";
            }
            int idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbModeli.Value.ToString()), idNdermarrje, 506, "btnKlienti", koka.IdKlientFurnitor, false);
            clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbModeli.Value.ToString()));
            clsFormatKonfigTrup formatMonedhe = DbCore.DbShare.clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);

            hfState.Set("formatMonedhe", JsonConvert.SerializeObject(formatMonedhe));
            data_DateEdit.Date = koka.DtDok;
            dteAfatiKohor.Date = DateTime.Today;
            cmbDogana.SelectedIndex = 1;
            txtCash.Text = "0";
            txtPershkrimi.Text = koka.Shenime;
            merrDokumentaKonvertuar(koka.IdKokaRezervimi, hfShtimModifikimValue);
            colAdresatKlientFurnitor adresat = new colAdresatKlientFurnitor();
            if (koka.IdKlientFurnitor != 0)
            {
                adresat = new colAdresatKlientFurnitor(koka.IdKlientFurnitor);
                if (adresat.Count > 0)
                {
                    if (adresat[0].IdTipAdrese == 1)//adresa biznesi
                        txtAdresaFaturimit.Text = adresat[0].Adresa;
                    if (adresat[0].IdTipAdrese == 2)//adrese magazine                                                 
                        txtAdresaDergimit.Text = adresat[0].Adresa;
                }
            }

            txtNumer.Text = koka.NrDok;
            if (koka.IdDegeAdministrative != 0)
                cmbDegeAdministrative.Value = koka.IdDegeAdministrative.ToString();

            if (hfShtimModifikimValue == "konvertim" || hfShtimModifikimValue == "konvertimblerje" || hfShtimModifikimValue == "rezervim")
            {
                clsPerdorues perdoruesi = new clsPerdorues(idPerdoruesi);
                lblKrijuesi.Text = perdoruesi.PerdoruesUsername;
            }
            else
            {
                clsPerdorues perdoruesi = new clsPerdorues(koka.IdKrijuesi);
                lblKrijuesi.Text = perdoruesi.PerdoruesUsername;
            }
            dateRegjistrimi_DateEdit.Date = koka.DtRegjistrimi;
            ImbLogger.LogTraceShitje($"Mbaroi metoda MerrTedhenatRez me parametra idGjuha:{idGjuha}, idKonfig:{idKonfig}, idPerdoruesi:{idPerdoruesi}, idViti:{idViti}, idNdermarrje:{idNdermarrje}, koka:{JsonConvert.SerializeObject(koka)}, rm:{JsonConvert.SerializeObject(rm)}, ci:{JsonConvert.SerializeObject(ci)}, hfShtimModifikimValue:" + hfShtimModifikimValue);
        }

        private static void shtoGauget(HtmlTable htmlTable, clsKokaShitje koka)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda shtoGauget me parameter koka:{JsonConvert.SerializeObject(koka)}");
            ASPxGaugeControl text = new ASPxGaugeControl();
            text.BackColor = Color.Transparent;
            text.SaveStateOnCallbacks = true;
            StateIndicatorGauge g = new StateIndicatorGauge();
            StateIndicatorComponent com = new StateIndicatorComponent("ind");
            IndicatorStateWeb s1 = new IndicatorStateWeb(DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight4);
            IndicatorStateWeb s2 = new IndicatorStateWeb(DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight2);
            IndicatorStateWeb s3 = new IndicatorStateWeb(DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight3);
            IndicatorStateWeb s0 = new IndicatorStateWeb(DevExpress.XtraGauges.Core.Model.StateIndicatorShapeType.ElectricLight1);

            Rectangle rec = new Rectangle(0, 0, 18, 18);
            g.Bounds = rec;
            com.States.Add(s0);
            com.States.Add(s1);
            com.States.Add(s2);
            com.States.Add(s3);
            Point p = new Point(9, 9);
            com.Center = p;
            SizeF f = new SizeF(18, 18);
            com.Size = f;
            g.Indicators.Add(com);
            text.Gauges.Add(g);

            text.Height = 18;
            text.Width = 18;
            if (koka.ColorVleraMbetur == "bojeqielli")
                com.StateIndex = 3;
            if (koka.ColorVleraMbetur == "kuqe")
                com.StateIndex = 2;
            else if (koka.ColorVleraMbetur == "gjelber")
                com.StateIndex = 1;
            else if (koka.ColorVleraMbetur == "gri")
                com.StateIndex = 0;
            else com.StateIndex = 4;
            HtmlTableCell cell = new HtmlTableCell();
            cell.Controls.Add(text);
            htmlTable.Rows[0].Cells.RemoveAt(0);
            htmlTable.Rows[0].Cells.Insert(0, cell);
            ImbLogger.LogTraceShitje($"Mbaroi metoda shtoGauget me parameter koka:{JsonConvert.SerializeObject(koka)}");
        }

        private void merrDokumentaKonvertuar(int id, string hfShtimModifikimValue)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda merrDokumentaKonvertuar me parametra id:{id}, hfShtimModifikimValue:" + hfShtimModifikimValue);
            List<object[]> list = new List<object[]>();

            if (hfShtimModifikimValue == "konvertim" || hfShtimModifikimValue == "konvertimblerje")
            {
                int[] ids = new int[0];
                var previousPageID = DbCore.IMBUtils.Types.Converter.MerrVlereOseDefault<string>(Request.QueryString["pageCacheId"]);
                if (!string.IsNullOrEmpty(previousPageID))
                {
                    var pageCache = GlobalCacheManager.GetPageCacheByPageID(previousPageID);
                    ids = (int[])pageCache["idkonvertimi"];
                }
                for (int i = 0; i < ids.Length; i++)
                {
                    clsKokaShitje koka = new clsKokaShitje();
                    koka.mbushKokaShitjeSipasIDPaTrup(ids[i]);
                    object[] dok = { koka.IdShitjeKoka, koka.NrDok, koka.Pershkrimi, koka.IdNivel, koka.IdKlientFurnitor };
                    list.Add(dok);
                }
            }
            else if (hfShtimModifikimValue != "rezervim")
            {
                colKonvertimi col = new colKonvertimi(id);
                foreach (clsKonvertimi konv in col)
                {
                    //DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti(konv.IdKonfigAmbjenteKonvertuar);
                    int idKategori = clsKonfigurimAmbjenti.ktheIdKategori(konv.IdKonfigAmbjenteKonvertuar);
                    if (idKategori == 1 || idKategori == 2)
                    {
                        clsKokaShitje koka = new clsKokaShitje();
                        koka.mbushKokaShitjeSipasIDPaTrup(konv.IdDokKonvertuar);
                        object[] dok = { koka.IdShitjeKoka, koka.NrDok, koka.Pershkrimi, koka.IdNivel, koka.IdKlientFurnitor };
                        list.Add(dok);
                    }
                    else if (idKategori == 6)
                    {
                        clsKokaMagazina mag = new clsKokaMagazina();
                        mag.mbushKokaMagazinaSipasID(konv.IdDokKonvertuar);
                        object[] dok = { mag.IdKokaMagazina, mag.NrDok, mag.Shenime, mag.IdNivel, mag.IdKlientFurnitor };
                        list.Add(dok);
                    }
                }
            }
            hfKonverto.Value = JsonConvert.SerializeObject(list);
            ImbLogger.LogTraceShitje($"Mbaroi metoda merrDokumentaKonvertuar me parametra id:{id}, hfShtimModifikimValue:" + hfShtimModifikimValue);
        }

        public void btnJo_Click(object sender, EventArgs e)
        {
            //mySessionObjects.ruajTrupatNeSession(Session, new colTrupiMagazina());
            //if (pergjigja.Text == "fshi")
            //{
            //    Response.Redirect("RegjistrimDokumentash.aspx?shitje_blerje=" + Request.QueryString["shitje_blerje"] + "&fshi=po");
            //}
            //else 
            if (pergjigja.Text == "ruaj")
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", MessagesResource.KtheCultureInfo((int)hfState["idGjuha"])), pnlMesazhi);
        }

        public void btnPo_Click(object sender, EventArgs e)
        {
            ImbLogger.LogTraceShitje("Filloi metoda btnPo_Click");
            int idGjuha = (int)hfState["idGjuha"];
            string guidString = hfState["guidString"].ToString();
            int idPerdoruesi = IdPerdoruesi;
            CultureInfo ci = MessagesResource.KtheCultureInfo(idGjuha);

            if (pergjigja.Text == "Serialet")
            {
                ruajJoNgaMenuja(false, rm, idPerdoruesi);
                return;
            }
            trupat = mySessionObjects.merrTrupatNgaSesioni(Session);
            clsMesazh mesazh = new clsMesazh(true, rm.GetString("regjisDokMesazhSuksesiRivleresim"));
            string fileLogPath = Server.MapPath("~/log/log.txt");
            clsLogRivleresimInventari log = new clsLogRivleresimInventari();
            try
            {

                log = new clsLogRivleresimInventari(idPerdoruesi, (int)hfState["idNdermarrje"]);
            }
            catch (Exception err)
            {
                ImbLogger.LogErrorShitje("msgGabimGjateRuajtjesSeRivleresimitNeLog");
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                throw new MyException(rm.GetString("msgGabimGjateRuajtjesSeRivleresimitNeLog", ci));
            }
            foreach (clsTrupiMagazina t in trupat)
            {
                //DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(t.IdArtikulli);
                mesazh = clsArtikulli.rivleresimCmimiMesatar(t.IdArtikulli, clsArtikulli.ktheMetodeKostoje(t.IdArtikulli), t.IdMag, t.Data, DateTime.Today, clsArtikulli.ktheKontrollCmimiPerDetajim(t.IdArtikulli), log, ci, rm, (int)hfState["idNdermarrje"], idPerdoruesi);
                if (!mesazh.Status)
                {
                    Response.Redirect("RegjistrimDokumentash.aspx?shitje_blerje=" + Request.QueryString["shitje_blerje"] + "&fshi=rivleresimjo");
                    return;
                }
            }
            mySessionObjects.ruajTrupatNeSession(Session, new colTrupiMagazina());
            if (mesazh.Status)
            {
                Response.Redirect("RegjistrimDokumentash.aspx?shitje_blerje=" + Request.QueryString["shitje_blerje"] + "&fshi=rivleresimpo");
                return;
            }
            ImbLogger.LogTraceShitje("Mbaroi metoda btnPo_Click");
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="veprimi"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idViti"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        /// <param name="hfShtimModifikimValue"></param>
        private void percaktoTemplateMenu(int idGjuha, string veprimi, int idPerdoruesi, int idViti, int idNdermarrje, ASPxMenu aSPxMenu1, string hfShtimModifikimValue, int idDokPara = 0, int idDokPas = 0)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda percaktoTemplateMenu me parametra idGjuha:{idGjuha}, veprimi:" + veprimi + $", idPerdoruesi:{idPerdoruesi}, idViti:{idViti}, idNdermarrje:{idNdermarrje}, hfShtimModifikimValue:" + hfShtimModifikimValue);
            colMenuItem menu = new colMenuItem(idGjuha);
            if (veprimi == "shitjediscount" || veprimi == "bazaar")
                menu.merrMenuItemSipasKomponentesRegjistrime(idGjuha, clsFunksione.GetKomponente(Page.Request), idPerdoruesi, idNdermarrje, idViti, (hfShtimModifikim.Value == "modifikim") ? false : true);
            else
                menu.merrMenuItemSipasKomponentesRegjistrimeDheNivelRegjistrimi(idGjuha, clsFunksione.GetKomponente(Page.Request), idPerdoruesi, idNdermarrje, idViti, Convert.ToInt32(cmbNiveli.Value), (hfShtimModifikim.Value == "modifikim") ? false : true);
            clsKokaFleteKontabel kok = new clsKokaFleteKontabel();

            foreach (clsMenuItem m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                {
                    //if (!m.Enabled) continue;
                    clsToolbarConfig.ShtoMenuItem(Theme, aSPxMenu1, m);
                }

                if ((m.Name == "Pezullo" || m.Name == "Refuzo") && ((hfShtimModifikimValue == "modifikim" && hfState.Get("idstatusdok").ToString() != "0") || (hfShtimModifikimValue == "shtim")))
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if ((hfLidhur.Value == "True") && m.Name == "Draft")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                if ((hfShtimModifikimValue != "modifikim") && (m.Name == "Klono" || m.Name == "KthimB" || m.Name == "KthimSh" || m.Name == "PrintPreview" || m.Name == "Fshi" || m.Name == "DergoEmail"))
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if (((hfShtimModifikimValue != "modifikim") || (cmbNiveli.SelectedItem.GetFieldValue("Kodi").ToString() != "FSH" && cmbNiveli.SelectedItem.GetFieldValue("Kodi").ToString() != "FB")) && m.Name == "Paguaj")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if (m.Name == "FletaKontabel")
                {
                    if ((hfShtimModifikimValue != "modifikim"))
                    {
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    }
                    else
                    {
                        kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 1);

                        if (kok.NrDukumentiKokaFleteKontabel != null)
                            aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&id=" + kok.IdKokaFleteKontabel + "&numur=" + kok.NrDukumentiKokaFleteKontabel + "')";
                        else
                        {
                            kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 2);
                            if (kok.NrDukumentiKokaFleteKontabel != null)
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&id=" + kok.IdKokaFleteKontabel + "&numur=" + kok.NrDukumentiKokaFleteKontabel + "')";
                            else
                            {
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                            }
                        }
                    }
                }
                if (m.Name == "QendraKosto")
                {
                    if ((hfShtimModifikimValue != "modifikim"))
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
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myButtonClickLupa.LupaUniversal_Click('Shperndarje ne qendrat e kostos','LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + "',1100,600)";
                            else
                            {
                                aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                            }
                        }
                        else aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    }
                }

                if (m.Name == "Serialet")
                {
                    clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaSerialeUnike.aspx");
                    if (!tedrejtaInfo.DShtim)
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }
                if (m.Name == "Ruaj")
                {
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                }

                if (m.Name == "ItemFrame")
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1, idDokPara, idDokPas);
                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame" || m.Name == "AutoKonverto")// || m.Name == "Arkiva" || m.Name == "Konverto" || m.Name == "Pezullo" || m.Name == "AutoKonverto")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
                var komponente = clsToolbarConfig.MerrEmerKomponenteSipasEmritTeMenuse(m.Name);
                if (!string.IsNullOrWhiteSpace(komponente))
                {

                    clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(IdPerdoruesi, idNdermarrje, idViti, komponente);
                    if ((m.Name == "KthimB" || m.Name == "KthimSh") && !tedrejtaInfo.DAmb)
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientEnabled = false;
                }
            }
            EventHandler handlerPerPo = new EventHandler(btnPo_Click);
            EventHandler handlerPerJo = new EventHandler(btnJo_Click);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo, handlerPerPo, handlerPerJo);
            int id = 0;
            if (hfShtimModifikimValue == "modifikim")
                int.TryParse(Request.QueryString["id"], out id);

            visibleMenu(idPerdoruesi, veprimi, id, hfShtimModifikimValue);
            if (cmbNiveli.SelectedItem.GetFieldValue("Kodi").ToString() == "FSH")
            {
                bool vetemKonvertimFSH = (bool)hfTeDrejta["VetemKonvertimFSH"];
                if (vetemKonvertimFSH && (hfShtimModifikimValue == "shtim" || hfShtimModifikimValue == "shtimraport" || hfShtimModifikimValue == "klonim"))
                {
                    ASPxMenu1.Items.FindByName("Ruaj").ClientEnabled = false;
                    ASPxMenu1.Items.FindByName("Draft").ClientEnabled = false;
                }
            }
            ImbLogger.LogTraceShitje($"Mbaroi metoda percaktoTemplateMenu me parametra idGjuha:{idGjuha}, veprimi:" + veprimi + $", idPerdoruesi:{idPerdoruesi}, idViti:{idViti}, idNdermarrje:{idNdermarrje}, hfShtimModifikimValue:" + hfShtimModifikimValue);
        }

        private void visibleMenu(int idPerdoruesi, string veprimi, int id, string hfShtimModifikimValue)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda visibleMenu me parametra idPerdoruesi:{idPerdoruesi}, veprimi:{veprimi}, id:{id}, hfShtimModifikimValue:{hfShtimModifikimValue}");
            clsKusht kusht = new clsKusht(Convert.ToInt32(cmbModeli.Value), "ZSP");
            string alternativKushtRD = clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbModeli.Value), "RD");
            string alternativKushtShfaqPezullo = clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbModeli.Value), "SHBP");
            bool[] visible = clsFunksione.merrMenu(idPerdoruesi, kusht.Vlera, hfShtimModifikimValue == "modifikim" ? false : true, lblStatusAprovimi.Text, id, cmbModeli.Text, 1, alternativKushtRD, alternativKushtShfaqPezullo);
            ASPxMenu1.Items.FindByName("Ruaj").ClientVisible = visible[0];
            if (hfShtimModifikimValue == "kthim" || veprimi == "shitjediscount")
                ASPxMenu1.Items.FindByName("Draft").ClientVisible = false;
            else
                ASPxMenu1.Items.FindByName("Draft").ClientVisible = visible[1];
            ASPxMenu1.Items.FindByName("Aprovo").ClientVisible = visible[2];
            ASPxMenu1.Items.FindByName("Refuzo").ClientVisible = visible[3];
            ASPxMenu1.Items.FindByName("Delego").ClientVisible = visible[4];
            ASPxMenu1.Items.FindByName("Komento").ClientVisible = visible[5];
            ASPxMenu1.Items.FindByName("Modifiko").ClientVisible = visible[6];
            try
            {
                ASPxMenu1.Items.FindByName("Konverto").ClientVisible = visible[7];
                ASPxMenu1.Items.FindByName("Paguaj").ClientVisible = visible[9];
            }
            catch (Exception err)
            {
                ImbLogger.LogErrorShitje($"Exception:{err}");
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
            }

            ASPxMenu1.Items.FindByName("Shto").ClientVisible = visible[8];
            ASPxMenu1.Items.FindByName("Fshi").ClientVisible = visible[10];
            if (ASPxMenu1.Items.FindByName("RefuzoDraft") != null)
                ASPxMenu1.Items.FindByName("RefuzoDraft").ClientVisible = visible[12];
            if (ASPxMenu1.Items.FindByName("Pezullo") != null)
                ASPxMenu1.Items.FindByName("Pezullo").ClientVisible = visible[13];
            if (veprimi == "shitje" || veprimi == "shitjediscount" || veprimi == "bazaar")
                ASPxMenu1.Items.FindByName("Validim").ClientVisible = visible[11];
            if (visible[6])
                hfTeDrejtaModSkema.Value = "True";
            else hfTeDrejtaModSkema.Value = "False";

            if (hfShtimModifikimValue == "modifikim" && hfState.Contains("Status") && int.Parse(hfState.Get("Status").ToString()) == 4 && !(bool)hfState["Meme"])
                ASPxMenu1.Items.FindByName("Ruaj").ClientVisible = false;
            ImbLogger.LogTraceShitje($"Mbaroi metoda visibleMenu me parametra idPerdoruesi:{idPerdoruesi}, veprimi:" + veprimi + $", id:{id}, hfShtimModifikimValue:" + hfShtimModifikimValue);
        }

        /// <summary>
        /// ndodh kur menuja ben bound
        /// </summary>
        /// <param name="sender">derguesi</param>
        /// <param name="e">argumentat</param>
        protected void ASPxMenu1_DataBound(object sender, EventArgs e)
        {
            percaktoTemplateMenu((int)hfState["idGjuha"], (string)hfState["veprimi"], (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrjeVit"], (int)hfState["idNdermarrje"], ASPxMenu1, hfShtimModifikim.Value);
        }
        private void initDates()
        {
            dateMaturimi_DateEdit.Date = DateTime.Today;
            dateTransportimi_DateEdit.Date = DateTime.Today;
            DtFillimi_DateEdit.Date = DateTime.Today;
            DtMbarimi_DateEdit.Date = DateTime.Today;
            DtFature_DateEdit.Date = DateTime.Today;
            dateRegjistrimi_DateEdit.Date = DateTime.Today;
            DtKerkese_DateEdit.Date = DateTime.Today;
        }
        private void vendosDateEditMask()
        {
            AspxWebControlUtils.vendosDateEditMask(data_DateEdit);
            AspxWebControlUtils.vendosDateEditMask(dteAfatiKohor);
            AspxWebControlUtils.vendosDateEditMask(dateMaturimi_DateEdit);
            AspxWebControlUtils.vendosDateEditMask(dateTransportimi_DateEdit);
            AspxWebControlUtils.vendosDateEditMask(DtFillimi_DateEdit);
            AspxWebControlUtils.vendosDateEditMask(DtMbarimi_DateEdit);
            AspxWebControlUtils.vendosDateEditMask(DtFature_DateEdit);
            AspxWebControlUtils.vendosDateEditMask(dateRegjistrimi_DateEdit);
            AspxWebControlUtils.vendosDateEditMask(DtKerkese_DateEdit);
        }
        private void konfiguroVleraFillestareShto(int idGjuha, string veprimi, int idNdermarrje, int idPerdoruesi, string pageStateLloji)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda konfiguroVleraFillestareShto me parametra idGjuha:{idGjuha}, veprimi:" + veprimi + $", idNdermarrje:{idNdermarrje},  idPerdoruesi:{idPerdoruesi}, pageStateLloji:" + pageStateLloji);
            initDates();
            vendosDateEditMask();

            hfState.Set("colKlienteFurnitoreVartes", JsonConvert.SerializeObject(new colKlienteFurnitore()));
            ConfigureAspxComboBox.percaktoTemplateComboJoListePaLupe(cmbDegeAdministrative, cmbFormatiPrintimit, cmbPikeShitjeFurnizimi, cmbOperatori);
            ConfigureAspxComboBox.ShtoKolonaKodiDhePershkrimi(cmbPikeShitjeFurnizimi, "IdPikeShitjeFurnizimi");
            ConfigureAspxComboBox.ShtoKolonaKodiDhePershkrimi(cmbDegeAdministrative, "IdDegeAdministrative");
            ConfigureAspxComboBox.ShtoKolonaEmriDheMbiemri(cmbOperatori, "IdOperator");
            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(idNdermarrje, cmbDegeAdministrative, false);
            cmbDegeAdministrative.Items.RemoveAt(0);
            ConfigureAspxComboBox.mbushComboOperatori(cmbOperatori, idNdermarrje);
            ConfigureAspxComboBox.mbushComboKonfigurimKase(cmbKonfigurimKase, idNdermarrje);
            ConfigureAspxComboBox.percaktoTemplateCombo(false, false, cmbKategoriSeriali);
            ConfigureAspxComboBox.mbushComboKategoriSeriali(idNdermarrje, cmbKategoriSeriali);
            cmbKonfigurimKase.SelectedIndex = 0;
            if (veprimi == "shitje" || veprimi == "shitjediscount" || veprimi == "bazaar")
            {
                ConfigureAspxComboBox.mbushComboPikeShitjeFurnizimi(idNdermarrje, cmbPikeShitjeFurnizimi, true, false);
                cmbPikeShitjeFurnizimi.Items.RemoveAt(0);
                ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatiPrintimit, 1, idNdermarrje, true);
            }
            else
            {
                ConfigureAspxComboBox.mbushComboPikeShitjeFurnizimi(idNdermarrje, cmbPikeShitjeFurnizimi, false, false);
                cmbPikeShitjeFurnizimi.Items.RemoveAt(0);
                ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatiPrintimit, 2, idNdermarrje, true);
            }
            ConfigureAspxComboBox.mbushComboMonedha(idPerdoruesi, idNdermarrje, false, cmbMonedha, cmbMonedhaPagese);
            //ConfigureAspxComboBox.mbushComboMonedha(idPerdoruesi, idNdermarrje, cmbMonedhaPagese, false);
            ConfigureAspxComboBox.mbushComboMuaji(cmbMuajRaportimi);
            ConfigureAspxComboBox.mbushComboVitet(cmbVitRaportimi, idNdermarrje);
            cmbVitRaportimi.Value = mySessionObjects.merrPeriudheKontabel(Session).IdViti.ToString();
            clsMonedha clsMon = JsonConvert.DeserializeObject<clsMonedha>(hfState.Get("mondedheNdermarrjeObj").ToString());
            if (clsMon.IdMonedha != 0)
            {
                ListEditItem foundMonedha = cmbMonedha.Items.FindByText(clsMon.KodiMonedha);
                foundMonedha.Selected = true;
                ListEditItem foundMonedhaPagese = cmbMonedhaPagese.Items.FindByText(clsMon.KodiMonedha);
                foundMonedhaPagese.Selected = true;
            }
            //ne kete cast, duke qene se eshte konfigurim fillestar ne shtim, monedha e zgjedhur eshte ajo e ndermarrjes, prandaj kursi eshte 1.
            txtKursi.Text = "1";
            txtKursiPagese.Text = "1";
            if (pageStateLloji == "shtimraport" && Request.QueryString["niveli"] != null)
                cmbNiveli.SelectedItem = cmbNiveli.Items.FindByValue(Convert.ToInt32(Request.QueryString["niveli"]));
            ConfigureAspxComboBox.ShtoKolonaPerKf(btnKlienti);
            ConfigureAspxComboBox.ShtoKolonaPerKartaKlienti(cmbKarta);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btnMagazina, btnKlienti, btneAutomjeti, btnKushtDergimi, btnMenyreTransporti, btnKushtPagese, btnAgjenti, btnAgjenti2, btnAgjenti3,
                btnTransportues, btnShitesi, btnMaturimi, btneArka, cmbKarta, btneCaktoNeHarte);
            ConfigureAspxComboBox.percaktoTemplateComboJoList(txtKursi);
            ConfigureAspxComboBox.percaktoTemplateComboJoList(txtKursiPagese);
            ConfigureAspxComboBox.shtoKolonaPerMagazina(btnMagazina);
            ConfigureAspxComboBox.KonfiguroComboBoxMenyreTransporti(idNdermarrje, btnMenyreTransporti);
            ConfigureAspxComboBox.KonfiguroComboBoxKushteDergimi(idNdermarrje, btnKushtDergimi);
            ConfigureAspxComboBox.KonfiguroComboBoxAgjentesh(IdNdermarrja, IdPerdoruesi, btnAgjenti, btnAgjenti2, btnAgjenti3);
            //ConfigureAspxComboBox.KonfiguroComboBoxAgjentesh(btnAgjenti2, IdNdermarrja, IdPerdoruesi);
            //ConfigureAspxComboBox.KonfiguroComboBoxAgjentesh(btnAgjenti3, IdNdermarrja, IdPerdoruesi);
            ConfigureAspxComboBox.mbushComboTransportues(idNdermarrje, btnTransportues);
            ConfigureAspxComboBox.KonfiguroComboBoxMetoda(cmbMenyrePagese, false);
            ConfigureAspxComboBox.mbushComboLlojMarreveshje(cmbLlojMarreveshje);
            ConfigureAspxComboBox.KonfiguroComboBoxStatusMarreveshje(cmbStatusMarreveshje, false);
            ConfigureAspxComboBox.mbushComboArkat(idNdermarrje, idPerdoruesi, btneArka, cmbMenyrePagese.Text == "Karte krediti");
            ConfigureAspxComboBox.mbushComboLlojZbritje(cmbLlojZbritje);
            ConfigureAspxComboBox.KonfiguroComboBoxKushtPagese(idNdermarrje, btnKushtPagese);
            ConfigureAspxComboBox.KonfiguroComboBoxMaturimi(IdNdermarrja, btnMaturimi);
            ConfigureAspxComboBox.mbushComboDogana(cmbDogana);

            if (Request.QueryString["idartikulli"] != null)
            {
                cmbNiveli.SelectedItem = cmbNiveli.Items.FindByValue(Convert.ToInt32(Request.QueryString["niveli"]));
                mbushComboKonfigurimet(idPerdoruesi, idNdermarrje, 1, veprimi, idGjuha, rm, ci);
                cmbModeli.Value = Request.QueryString["konfigurim"];
                txtKontakti.Text = Request.QueryString["nrklienti"];
                hfMsisdnBazaari.Value = Request.QueryString["nrklienti"];
                hfKodVFOne.Value = (Request.QueryString["kodvodone"] != null) ? Request.QueryString["kodvodone"] : "";
                hfKodKuponiDD.Value = (Request.QueryString["kodkupon"] != null) ? Request.QueryString["kodkupon"] : "";

                cmbNiveli.ClientSideEvents.Init = "function(s,e){ selektoKonfigurim=true; }";//TextChangedNiveli();
            }
            else mbushComboKonfigurimet(idPerdoruesi, idNdermarrje, 1, veprimi, idGjuha, rm, ci);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup1);
            int idKonfigAmbjente = int.Parse(cmbModeli.Value.ToString());
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup1, idNdermarrje, 1, idKonfigAmbjente, idPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup2);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup2, idNdermarrje, 2, idKonfigAmbjente, idPerdoruesi);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup3);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup3, idNdermarrje, 3, idKonfigAmbjente, idPerdoruesi);
            cmbDogana.SelectedIndex = 1;
            lblKrijuesi.Text = hfPerdoruesAktual.Value;
            int idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(idGjuha, idKonfigAmbjente, idNdermarrje, 506, "btnKlienti", -1, true);
            clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(idKonfigAmbjente);
            clsFormatKonfigTrup formatMonedhe = DbCore.DbShare.clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            hfState.Set("formatMonedhe", JsonConvert.SerializeObject(formatMonedhe));
            bool lkvk = clsAlternativaKushti.getAlternativa(idKonfigAmbjente, "LKVK") == "Po";
            hfState.Set("LKVK", lkvk);
            hfState.Set("SDGMZ1", clsAlternativaKushti.getAlternativa(clsGridaTrupi.merrKonfigurimLupeSipasKonfigAmbjentiDheKodit(idKonfigAmbjente, "txtDetajimi"), "SDGMZ") == "Po");
            hfState.Set("SDGMZ2", clsAlternativaKushti.getAlternativa(clsGridaTrupi.merrKonfigurimLupeSipasKonfigAmbjentiDheKodit(idKonfigAmbjente, "txtDetajimi2t"), "SDGMZ") == "Po");
            CacheLayer.GlobalCacheManager.MySessionCache.Remove("SkedareSerial");
            ImbLogger.LogTraceShitje($"Mbaroi metoda konfiguroVleraFillestareShto me parametra idGjuha:{idGjuha}, veprimi:" + veprimi + $", idNdermarrje:{idNdermarrje},  idPerdoruesi:{idPerdoruesi}, pageStateLloji:" + pageStateLloji);
        }

        private void PastroFusha()
        {
            txtTotal1.Text = "0.00";
            txtTotal2.Text = "0.00";
            txtTotalMeZbritje1.Text = "0.00";
            txtTotalMeZbritje2.Text = "0.00";
            txtTVSH1.Text = "0.00";
            txtTVSH2.Text = "0.00";
            txtVlefte.Text = "0.00";
            txtPerqindje.Text = "0.00";
            txtEmri.ReadOnly = true;
            txtDetyrimi.ReadOnly = true;
            txtKrediti.ReadOnly = true;
            txtLimit.ReadOnly = true;
            hfCmimi.Value = string.Empty;
            hfDetajimi.Value = string.Empty;
            hfKodi.Value = string.Empty;
            hfLloji.Value = string.Empty;
            hfIdKodi.Value = string.Empty;
            hfNjesia.Value = string.Empty;
            hfPershkrimi.Value = string.Empty;
            hfSasia.Value = string.Empty;
            hfTVSH.Value = string.Empty;
            hfVlefte.Value = string.Empty;
            hfVlefteTVSH.Value = string.Empty;
            hfZbritje.Value = string.Empty;
            hfArkiva.Clear();
            hfArkivaDokId.Value = string.Empty;
            txtPike.Text = "0";
            txtShenime2.Text = string.Empty;
            cbKartaPaPagese.Checked = false;
            txtKerkuarNga.Text = string.Empty;

            mySessionObjects.hiqObjectNeSesion(Session, Constants.SERIALE_UNIKE_TE_NGARKUAR, hfState.Get<string>("guidString"));
            mySessionObjects.hiqObjectNeSesion(Session, "kategoriId");
            CacheLayer.GlobalCacheManager.MySessionCache.Remove("SkedareSerial");
            mySessionObjects.hiqObjectNeSesion(Session, "fazat");
        }

        /// <summary>
        /// Vendos vlerat default kur po behet modifikim
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="veprimi"></param>
        /// <param name="idViti"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="shtimModifikim"></param>
        private void konfiguroVleraFillestareModifiko(int idGjuha, string veprimi, int idViti, int idNdermarrje, int idPerdoruesi, ResourceManager rm, CultureInfo ci, string shtimModifikim, int id)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda konfiguroVleraFillestareModifiko me parametra idGjuha:{idGjuha}, veprimi:" + veprimi + $", idViti:{idViti}, idNdermarrje:{idNdermarrje}, idPerdoruesi:{idPerdoruesi}, shtimModifikim:" + shtimModifikim + $", id:{id}");

            AspxWebControlUtils.vendosDateEditMask(data_DateEdit, dteAfatiKohor, dateMaturimi_DateEdit, dateTransportimi_DateEdit, dateRegjistrimi_DateEdit, DtFillimi_DateEdit,
                DtMbarimi_DateEdit, DtFature_DateEdit, DtKerkese_DateEdit);
            ConfigureAspxComboBox.mbushComboMonedha(idPerdoruesi, idNdermarrje, false, cmbMonedha, cmbMonedhaPagese);
            //ConfigureAspxComboBox.mbushComboMonedha(idPerdoruesi, idNdermarrje, cmbMonedhaPagese, false);
            ConfigureAspxComboBox.mbushComboMuaji(cmbMuajRaportimi, true);
            ConfigureAspxComboBox.percaktoTemplateCombo(false, false, cmbKategoriSeriali);
            ConfigureAspxComboBox.mbushComboKategoriSeriali(idNdermarrje, cmbKategoriSeriali);
            ConfigureAspxComboBox.mbushComboVitet(cmbVitRaportimi, idNdermarrje, true);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btnMagazina, btnKlienti, btneAutomjeti, btnKushtDergimi, btnMenyreTransporti, btnKushtPagese, btnAgjenti, btnAgjenti2, btnAgjenti3,
                btnTransportues, btnShitesi, btnMaturimi, btneArka, cmbKarta);
            ConfigureAspxComboBox.shtoKolonaPerMagazina(btnMagazina);
            ConfigureAspxComboBox.percaktoTemplateComboJoList(btneCaktoNeHarte);
            ConfigureAspxComboBox.percaktoTemplateComboJoList(txtKursi);
            ConfigureAspxComboBox.percaktoTemplateComboJoList(txtKursiPagese);
            ConfigureAspxComboBox.KonfiguroComboBoxMenyreTransporti(idNdermarrje, btnMenyreTransporti);
            ConfigureAspxComboBox.KonfiguroComboBoxKushteDergimi(idNdermarrje, btnKushtDergimi);
            ConfigureAspxComboBox.KonfiguroComboBoxAgjentesh(IdNdermarrja, 0, btnAgjenti, btnAgjenti2, btnAgjenti3);
            //ConfigureAspxComboBox.KonfiguroComboBoxAgjentesh(btnAgjenti2, IdNdermarrja);
            //ConfigureAspxComboBox.KonfiguroComboBoxAgjentesh(btnAgjenti3, IdNdermarrja);
            ConfigureAspxComboBox.mbushComboTransportues(idNdermarrje, btnTransportues);
            //ConfigureAspxComboBox.mbushComboOperatori(cmbOperatori, idNdermarrje);
            ConfigureAspxComboBox.mbushComboKonfigurimKase(cmbKonfigurimKase, idNdermarrje);
            cmbKonfigurimKase.SelectedIndex = 0;
            ConfigureAspxComboBox.KonfiguroComboBoxMetoda(cmbMenyrePagese, false);
            ConfigureAspxComboBox.mbushComboLlojMarreveshje(cmbLlojMarreveshje);
            ConfigureAspxComboBox.KonfiguroComboBoxStatusMarreveshje(cmbStatusMarreveshje, false);
            ConfigureAspxComboBox.KonfiguroComboBoxKushtPagese(idNdermarrje, btnKushtPagese);
            ConfigureAspxComboBox.ShtoKolonaPerKf(btnKlienti);
            ConfigureAspxComboBox.ShtoKolonaPerKartaKlienti(cmbKarta);
            ConfigureAspxComboBox.KonfiguroComboBoxMaturimi(IdNdermarrja, btnMaturimi);
            ConfigureAspxComboBox.mbushComboDogana(cmbDogana);
            ConfigureAspxComboBox.mbushComboLlojZbritje(cmbLlojZbritje);
            mbushComboKonfigurimet(idPerdoruesi, idNdermarrje, 0, veprimi, idGjuha, rm, ci);
            ConfigureAspxComboBox.percaktoTemplateComboJoListePaLupe(cmbDegeAdministrative, cmbFormatiPrintimit, cmbPikeShitjeFurnizimi, cmbOperatori);
            ConfigureAspxComboBox.ShtoKolonaKodiDhePershkrimi(cmbPikeShitjeFurnizimi, "IdPikeShitjeFurnizimi");
            ConfigureAspxComboBox.ShtoKolonaKodiDhePershkrimi(cmbDegeAdministrative, "IdDegeAdministrative");
            ConfigureAspxComboBox.ShtoKolonaEmriDheMbiemri(cmbOperatori, "IdOperator");
            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(idNdermarrje, cmbDegeAdministrative, true);
            cmbDegeAdministrative.Items.RemoveAt(0);
            ConfigureAspxComboBox.mbushComboOperatori(cmbOperatori, idNdermarrje);

            if (veprimi == "shitje" || veprimi == "shitjediscount" || veprimi == "bazaar")
            {
                ConfigureAspxComboBox.mbushComboPikeShitjeFurnizimi(idNdermarrje, cmbPikeShitjeFurnizimi, true, true);
                cmbPikeShitjeFurnizimi.Items.RemoveAt(0);
                ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatiPrintimit, 1, idNdermarrje, true);
            }
            else
            {
                ConfigureAspxComboBox.mbushComboPikeShitjeFurnizimi(idNdermarrje, cmbPikeShitjeFurnizimi, false, true);
                cmbPikeShitjeFurnizimi.Items.RemoveAt(0);
                ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatiPrintimit, 2, idNdermarrje, true);
            }

            hfIdKontrata.Value = "" + id;

            clsKokaShitje clsKoka = new clsKokaShitje();
            if (shtimModifikim == "rezervim")
            {
                clsKokaRezervime clsKokarez = new clsKokaRezervime();
                clsKokarez.mbushKokaRezervimiSipasID(id);
                if (clsKokarez != null)
                {
                    int idKonfig;
                    if (Request.QueryString["konfigurim"] != null)
                    {
                        clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
                        cmbModeli.Value = Request.QueryString["konfigurim"];
                        idKonfig = Convert.ToInt32(Request.QueryString["konfigurim"]);
                    }
                    else
                    {
                        ImbLogger.LogErrorShitje($"Querystring: konfigurim nuk duhet te jete bosh");
                        throw new MyException(rm.GetString("Querystring: konfigurim nuk duhet te jete bosh", ci));
                    }
                    mbushListeRegjistrimTrupiModifiko(clsKoka.IdKonfigAmbjente, id, idPerdoruesi, idNdermarrje, shtimModifikim, clsKokarez.DtDok, clsKoka.IdKlientFurnitor);
                    MerrTedhenatRez(idGjuha, idKonfig, idPerdoruesi, idViti, idNdermarrje, clsKokarez, rm, ci, shtimModifikim);
                }
                return;
            }
            //modifikim
            clsKoka.mbushKokaShitjeSipasIDPaTrup(id);
            //nqs duhet e urdhershitjes idkonfigu eshte ok perdryshe duhet marre nga querystring["konfigurim"]
            hfState.Set("SDGMZ1", clsAlternativaKushti.getAlternativa(clsGridaTrupi.merrKonfigurimLupeSipasKonfigAmbjentiDheKodit(clsKoka.IdKonfigAmbjente, "txtDetajimi"), "SDGMZ") == "Po");
            hfState.Set("SDGMZ2", clsAlternativaKushti.getAlternativa(clsGridaTrupi.merrKonfigurimLupeSipasKonfigAmbjentiDheKodit(clsKoka.IdKonfigAmbjente, "txtDetajimi2t"), "SDGMZ") == "Po");

            if (clsKoka.IdShitjeKoka != 0)
            {
                mbushListeRegjistrimTrupiModifiko(clsKoka.IdKonfigAmbjente, id, idPerdoruesi, idNdermarrje, shtimModifikim, clsKoka.DtDok, clsKoka.IdKlientFurnitor);
                MerrTedhenat(idGjuha, idPerdoruesi, idViti, idNdermarrje, clsKoka, rm, ci, shtimModifikim);
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Gabim gjate leximit te dokumentit", pnlMesazhi);
                return;
            }
            ImbLogger.LogTraceShitje($"Mbaroi metoda konfiguroVleraFillestareModifiko me parametra idGjuha:{idGjuha}, veprimi:" + veprimi + $", idViti:{idViti}, idNdermarrje:{idNdermarrje}, idPerdoruesi:{idPerdoruesi}, shtimModifikim:" + shtimModifikim + $", id:{id}");
        }

        /// <summary>
        /// Merr trupin ekzistues te dokumentit qe po modifikohet
        /// </summary>
        /// <param name="idKonfig"></param>
        /// <param name="idKokeShitje"></param>
        /// <param name="idPerdoreusi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="hfShtimModifikimValue"></param>
        private void mbushListeRegjistrimTrupiModifiko(int idKonfig, int idKokeShitje, int idPerdoruesi, int idNdermarrje, string hfShtimModifikimValue, DateTime data, int idKF)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda konfiguroVleraFillestareModifiko me parametra idKonfig:{idKonfig}, idKokeShitje:{idKokeShitje}, idPerdoruesi:{idPerdoruesi}, idNdermarrje:{idNdermarrje}, hfShtimModifikimValue:" + hfShtimModifikimValue + $", data:{data}, idKF:{idKF}");
            clsKokaMagazina kokam = new clsKokaMagazina();
            int idKategoria = clsKonfigurimAmbjenti.ktheIdKategori(idKonfig);
            kokam.mbushKokaMagazinaSipasIDGjenerues(idKokeShitje, idKategoria == 1 ? 2 : 1, idKonfig);
            colTrupiShitje trupiShitje = new colTrupiShitje();

            if ((hfShtimModifikimValue == "konvertim" || hfShtimModifikimValue == "konvertimblerje") && clsAlternativaKushti.getAlternativa(idKonfig, "MPASHGJK") == "Jo")
                trupiShitje.mbushGjitheTrupiShitjePaArtikujSherbimNgaKoka(idKokeShitje);
            else
                trupiShitje.mbushGjitheTrupiShitjeNgaKoka(idKokeShitje);

            clsTrupiShitje trupiShitjeFirst = trupiShitje.FirstOrDefault();
            if (trupiShitjeFirst != null && trupiShitjeFirst.Sasia < 0 && mySessionObjects.merrEshteOwnSesioni(Session))
            {
                hfState.Set("eshteDokKthimi", clsKokaShitje.TransferuarNgaFBKthim(idKokeShitje));
            }
            else
                hfState.Set("eshteDokKthimi", trupiShitjeFirst != null && trupiShitjeFirst.IdTrupiKthim != 0 ? true : false);
            mbushHiddenFieldet(idKokeShitje, idKonfig, idPerdoruesi, trupiShitje, idNdermarrje, kokam.IdKokaMagazina, kokam.IdKonfigAmbjente, hfShtimModifikimValue, data, idKF);
            //clsTrupiShitje trupi = new clsTrupiShitje();
            //trupiShitje.Add(trupi);
            ImbLogger.LogTraceShitje($"Mbaroi metoda konfiguroVleraFillestareModifiko me parametra idKonfig:{idKonfig}, idKokeShitje:{idKokeShitje}, idPerdoruesi:{idPerdoruesi}, idNdermarrje:{idNdermarrje}, hfShtimModifikimValue:" + hfShtimModifikimValue + $", data:{data}, idKF:{idKF}");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idKokeShitje"></param>
        /// <param name="idKonfig">nuk duhet ne rastet e boshatisjes</param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="col"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="hfShtimModifikimValue"></param>
        private void mbushHiddenFieldet(int idKokeShitje, int idKonfig, int idPerdoruesi, colTrupiShitje col, int idNdermarrje, int idkokamag, int idkonfmag, string hfShtimModifikimValue, DateTime data, int idKF)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda mbushHiddenFieldet me parametra idKokeShitje:{idKokeShitje}, idKonfig:{idKonfig}, idPerdoruesi:{idPerdoruesi}, idNdermarrje:{idNdermarrje}, idkokamag:{idkokamag}, idkonfmag:{idkonfmag}, hfShtimModifikimValue:" + hfShtimModifikimValue + $", data:{data}, idKF:{idKF}");
            if (hfShtimModifikimValue != "konvertim" && hfShtimModifikimValue != "konvertimblerje" && hfShtimModifikimValue != "rezervim" && hfShtimModifikimValue != "kthim" && hfShtimModifikimValue != "kthimVod" && hfShtimModifikimValue != "bli")
            {
                object myCols = new { colTrupi = col };
                HfColTrup.Value = JsonConvert.SerializeObject(col);
                colArtikujt colArtikuj = new colArtikujt(idKokeShitje);
                //colKodbare colKodbare = new colKodbare();
                //colKodbare.merrKodbarePerShitjen(idKokeShitje);
                DataTable colKodbare = DbCore.DbInventari.colKodbare.merrKodbarArtikulliNeTrupDokShitje(idKokeShitje);
                colArtikulliPerberes colArtikujtPerberes = new colArtikulliPerberes();

                foreach (clsArtikulli art in colArtikuj)
                {
                    if (art.Klasa == 4)
                        colArtikujtPerberes.merrSipasIdArtikullKryesorePare(art.IdArtikulli, data);
                    else
                        colArtikujtPerberes.Add(new clsArtikulliPerberes());
                }
                colLlogarite colLlogari = new colLlogarite(idKokeShitje);
                if (col.Count > 10)
                {
                    HfColArtPerb.Value = JsonConvert.SerializeObject(colArtikujtPerberes);
                    HfColArt.Value = JsonConvert.SerializeObject(colArtikuj);
                    HfColKodbare.Value = JsonConvert.SerializeObject(colKodbare);
                    HfColllogarite.Value = JsonConvert.SerializeObject(colLlogari);
                    HfColKategoriShpenzimi.Value = JsonConvert.SerializeObject(new colKategoriShpenzimi(idKokeShitje));
                    colDetajimeArtikulli colDetArt = new colDetajimeArtikulli();
                    colDetArt.mbushDetajimeSipasNdermarrjesAndAutorizim(idNdermarrje, idPerdoruesi);
                    HfColDetArt.Value = JsonConvert.SerializeObject(new colDetajimeArtikulli(idKokeShitje, 1, colDetArt));
                    HfColDetArt2.Value = JsonConvert.SerializeObject(new colDetajimeArtikulli(idKokeShitje, 2, colDetArt));
                    colNjesiAdministrative colNjesiAd = new colNjesiAdministrative();
                    colNjesiAd.mbushGjitheNjesiAdministrative(idNdermarrje, idPerdoruesi);
                    colNjesiteArtikulli colNjesiArt = new colNjesiteArtikulli(idNdermarrje);
                    colTaksa taksat = new colTaksa(idNdermarrje, idPerdoruesi);
                    HfColNjesAdminis.Value = JsonConvert.SerializeObject(new colNjesiAdministrative(idKokeShitje, colNjesiAd));
                    HfColNjesiArt.Value = JsonConvert.SerializeObject(new colNjesiteArtikulli(idKokeShitje, idNdermarrje, colNjesiArt));
                    HfColTaksa.Value = JsonConvert.SerializeObject(new colTaksa(idKokeShitje, taksat, idNdermarrje, idPerdoruesi));
                }
                else
                {
                    HfColArtPerb.Value = JsonConvert.SerializeObject(colArtikujtPerberes);
                    HfColArt.Value = JsonConvert.SerializeObject(colArtikuj);
                    HfColKodbare.Value = JsonConvert.SerializeObject(colKodbare);
                    HfColllogarite.Value = JsonConvert.SerializeObject(colLlogari);
                    HfColKategoriShpenzimi.Value = JsonConvert.SerializeObject(new colKategoriShpenzimi(idKokeShitje));
                    HfColDetArt.Value = JsonConvert.SerializeObject(new colDetajimeArtikulli(idKokeShitje, 1));
                    HfColDetArt2.Value = JsonConvert.SerializeObject(new colDetajimeArtikulli(idKokeShitje, 2));
                    HfColNjesAdminis.Value = JsonConvert.SerializeObject(new colNjesiAdministrative(idKokeShitje));
                    HfColNjesiArt.Value = JsonConvert.SerializeObject(new colNjesiteArtikulli(idKokeShitje, idNdermarrje));
                    HfColTaksa.Value = JsonConvert.SerializeObject(new colTaksa(idKokeShitje));
                }

                //mbushim fazat nga trupi
                if (hfShtimModifikimValue == "modifikim")
                {
                    colFazaKontrate ocolFazat = new colFazaKontrate();
                    var arrid = new List<int>();
                    foreach (clsTrupiShitje o in col)
                    {
                        if (o.IdTrupiKonvertimi != 0)
                        {
                            int idKontrata = clsTrupiShitje.ktheIdKoka(o.IdTrupiKonvertimi);
                            if (!arrid.Contains(idKontrata))
                            {
                                arrid.Add(idKontrata);
                                colFazaKontrate ocol = new colFazaKontrate(idNdermarrje, idKontrata);
                                ocolFazat.AddRange(ocol);
                            }
                        }
                    }
                    mbushComboBoxFazat(ocolFazat);
                    // hfFazat.Value = serializusi.Serialize(ocolFazat);
                }

                if (hfShtimModifikimValue != "klonim")
                {
                    var nrRendorSerial = -1;
                    for (int i = 0; i < col.Count; i++)
                    {
                        if (col[i].IdLlojVeprimi == 1)
                        {
                            nrRendorSerial++;
                            if (colArtikuj[i].LlojiArt)
                            {
                                var colzgjedhur = new DbCore.DbAsete.colAQTSeriale();
                                colzgjedhur.ktheAQTSerialSipasIdDokumentiDheNrRreshtiNgaSerialeMagazine(idkokamag,
                                    idkonfmag, nrRendorSerial);
                                hfSeriale.Set(col[i].IdKodi + "_" + (i + 1), JsonConvert.SerializeObject(colzgjedhur));
                                hfSasiSeriale.Set(col[i].IdKodi + "_" + (i + 1), colArtikuj[i].MeSerial ? 1 : (float)(col[i].Sasia * (double)(col[i].IdNjesia == colArtikuj[i].Njesi1Artikulli ? 1 : colArtikuj[i].KoeficientArtikulli)));
                            }
                        }
                    }
                }
                merrLlojeTVSH(idKonfig, idPerdoruesi, idNdermarrje, colArtikuj, colLlogari, idKF);
                return;
            }

            int[] ids = new int[0];
            var previousPageID = DbCore.IMBUtils.Types.Converter.MerrVlereOseDefault<string>(Request.QueryString["pageCacheId"]);
            if (!string.IsNullOrEmpty(previousPageID))
            {
                var pageCache = GlobalCacheManager.GetPageCacheByPageID(previousPageID);
                ids = (int[])pageCache["idkonvertimi"];
            }

            colTrupiShitje colgjithe = new colTrupiShitje();
            colArtikujt colartgjithe = new colArtikujt();
            DataTable colKodbaregjithe = new DataTable();
            colKokatMakro colmarkogjithe = new colKokatMakro();
            colLlogarite colllogaritegjithe = new colLlogarite();
            colDetajimeArtikulli coldetgjithe = new colDetajimeArtikulli();
            colDetajimeArtikulli coldetgjithe2 = new colDetajimeArtikulli();
            colNjesiAdministrative colmaggjithe = new colNjesiAdministrative();
            colNjesiteArtikulli colnjesigjithe = new colNjesiteArtikulli();
            colTaksa coltaksagjithe = new colTaksa();
            colKategoriShpenzimi colKategoriShpenzimi = new colKategoriShpenzimi();
            int m = 1;

            if (ids.Length > 0)
            {
                if (hfShtimModifikimValue == "konvertim")
                {
                    clsKokaShitje konv = new clsKokaShitje(ids[0]);
                    if (clsAlternativaKushti.getAlternativa(konv.IdKonfigAmbjente, "DOKKONTRATE") == "Po")
                        hfDokKontrate.Value = "PO";
                }
            }
            colFazaKontrate colTotal = new colFazaKontrate();

            for (int i = 0; i < ids.Length; i++)
            {
                bool marreFazat = false;
                colTrupiShitje trupi = new colTrupiShitje();
                clsKokaShitje konv = new clsKokaShitje(ids[i]);

                if (hfShtimModifikimValue == "konvertim")
                {
                    if (clsAlternativaKushti.getAlternativa(konv.IdKonfigAmbjente, "MPASHGJK") == "Jo")
                        trupi.ktheGjitheTrupiShitjePaArtikujSherbimNgaKokaKonvert(ids[i], idNdermarrje);
                    else
                        trupi.ktheGjitheTrupiShitjeNgaKokaKonvert(ids[i], idNdermarrje);
                    if (clsAlternativaKushti.getAlternativa(konv.IdKonfigAmbjente, "SHVFPK") == "Po" && hfDokKontrate.Value == "PO")
                    {
                        colTotal = new colFazaKontrate(string.Join(",", ids));
                        marreFazat = true;
                    }
                    if (hfDokKontrate.Value == "PO" && !marreFazat)
                    {
                        colFazaKontrate colFazat = new colFazaKontrate(idNdermarrje, ids[i]);
                        colTotal.AddRange(colFazat);
                    }
                }
                else if (hfShtimModifikimValue == "konvertimblerje")
                {
                    if (clsAlternativaKushti.getAlternativa(konv.IdKonfigAmbjente, "MPASHGJK") == "Jo")
                        trupi.ktheGjitheTrupiShitjePaArtikujSherbimNgaKokaKonvertBlerje(ids[i], idNdermarrje);
                    else
                        trupi.ktheGjitheTrupiShitjeNgaKokaKonvertBlerje(ids[i], idNdermarrje);
                }
                else if (hfShtimModifikimValue == "kthim")
                    trupi.ktheGjitheTrupiShitjeNgaKokaKthim(ids[i], idNdermarrje);
                else if (hfShtimModifikimValue == "kthimVod")
                    trupi.mbushGjitheTrupiShitjeNgaKokaPerKthimVod(ids[i]);
                else if (hfShtimModifikim.Value == "bli")
                    trupi.mbushGjitheTrupiShitjeNgaKokaPerBlerje(ids[i]);

                else
                    trupi.ktheGjitheTrupiShitjeNgaKokaRezervime(ids[i], idNdermarrje);

                colgjithe.AddRange(trupi);
                colArtikujt colart = trupi.ktheColArtikuj();
                DataTable colKodbari = colKodbare.merrKodbarArtikulliNeTrupDokShitje(ids[i]);
                colArtikulliPerberes colArtikujtPerberes = new colArtikulliPerberes();
                foreach (clsArtikulli art in colart)
                {
                    if (art.Klasa == 4)
                        colArtikujtPerberes.merrSipasIdArtikullKryesorePare(art.IdArtikulli, data);
                    else
                        colArtikujtPerberes.Add(new clsArtikulliPerberes());
                }
                HfColArtPerb.Value = JsonConvert.SerializeObject(colArtikujtPerberes);
                clsKokaMagazina kokam = new clsKokaMagazina();
                //DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti(idKonfig);
                int idKategoria = clsKonfigurimAmbjenti.ktheIdKategori(idKonfig);
                if (idKategoria == 1)
                    kokam.mbushKokaMagazinaSipasIDGjenerues(ids[i], 2, idKonfig);
                else
                    kokam.mbushKokaMagazinaSipasIDGjenerues(ids[i], 1, idKonfig);

                var nrRendorSerial = -1;
                for (int j = 0; j < trupi.Count; j++)
                {
                    if (trupi[j].IdLlojVeprimi == 1)
                    {
                        nrRendorSerial++;
                        if (trupi[j].IdLlojVeprimi == 1 && colart[j].LlojiArt)
                        {
                            var colzgjedhur = new DbCore.DbAsete.colAQTSeriale();
                            colzgjedhur.ktheAQTSerialSipasIdDokumentiDheNrRreshtiNgaSerialeMagazine(
                                kokam.IdKokaMagazina, kokam.IdKonfigAmbjente, nrRendorSerial);

                            hfSeriale.Set(trupi[j].IdKodi + "_" + m, JsonConvert.SerializeObject(colzgjedhur));
                            hfSasiSeriale.Set(trupi[j].IdKodi + "_" + m, colart[j].MeSerial ? 1 : (float)(trupi[j].Sasia * (double)(trupi[j].IdNjesia == colart[j].Njesi1Artikulli ? 1 : colart[j].KoeficientArtikulli)));
                        }
                    }
                    m++;
                }
                colartgjithe.AddRange(colart);
                colKodbaregjithe.Merge(colKodbari);
                colmarkogjithe.AddRange(trupi.ktheColMakrot());
                colllogaritegjithe.AddRange(trupi.ktheColLlogarite());
                coldetgjithe.AddRange(trupi.ktheColDetArt());
                coldetgjithe2.AddRange(trupi.ktheColDetArt2());
                colmaggjithe.AddRange(trupi.ktheColMag(idPerdoruesi));
                colnjesigjithe.AddRange(trupi.ktheColNjesiArt());
                coltaksagjithe.AddRange(trupi.ktheColTaksa());
                colKategoriShpenzimi.AddRange(trupi.ktheColKategoriShpenzimi());
            }
            object[] listeTvshArtGjithe = new object[colartgjithe.Count];
            object[] listeTvshLlogGjithe = new object[colllogaritegjithe.Count];
            if (colartgjithe.Count > 0 || colllogaritegjithe.Count > 0) //nese ska rreshta kjo pjese nuk ka nevoj te behet
            {
                clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti();
                if (cmbModeli.Text == string.Empty)
                    konfig.mbushKonfigDefaultKomponentes(506, idNdermarrje);
                else
                    konfig.mbushKonfigAmbjSipasKod(cmbModeli.Text, idNdermarrje);

                KonfigurimTVSHGjateRregj llojTvsh;
                switch (clsAlternativaKushti.getAlternativa(idKonfig, "TVSH"))
                {
                    case "Ndermarje":
                        llojTvsh = KonfigurimTVSHGjateRregj.Ndermarrje;
                        break;
                    case "Sipas Artikullit":
                        llojTvsh = KonfigurimTVSHGjateRregj.Sipas_Artikullit;
                        break;
                    case "Pa TVSH":
                        llojTvsh = KonfigurimTVSHGjateRregj.Pa_TVSH;
                        break;
                    default:
                        llojTvsh = KonfigurimTVSHGjateRregj.Undefined;
                        break;
                }
                clsTaksa taksendermarje = new clsTaksa();
                taksendermarje.mbushTakseDefaultNdermarrje(idNdermarrje);
                DbCore.DbRegjistrim.colTaksa taksaNdermarrje = new DbCore.DbRegjistrim.colTaksa(idNdermarrje, DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, idPerdoruesi);
                clsKlientFurnitor kf = new clsKlientFurnitor(idKF);
                clsTaksa taksaKF = new clsTaksa(kf.IdTvsh);
                for (int i = 0; i < colartgjithe.Count; i++)
                {
                    clsArtikulli art = colartgjithe[i];
                    listeTvshArtGjithe[i] = art.ktheTvsh(llojTvsh, idPerdoruesi, taksendermarje, taksaNdermarrje, taksaKF);
                }

                for (int i = 0; i < colllogaritegjithe.Count; i++)
                {
                    clsLlogari llog = colllogaritegjithe[i];
                    listeTvshLlogGjithe[i] = llog.ktheTvsh(llojTvsh, idPerdoruesi, taksendermarje, taksaNdermarrje, taksaKF);
                }
            }

            HfColTvshArt.Value = JsonConvert.SerializeObject(listeTvshArtGjithe);
            HfColTvshLlog.Value = JsonConvert.SerializeObject(listeTvshLlogGjithe);
            HfColKodbare.Value = JsonConvert.SerializeObject(colKodbaregjithe);
            HfColTrup.Value = JsonConvert.SerializeObject(colgjithe);
            HfColArt.Value = JsonConvert.SerializeObject(colartgjithe);
            HfColMakro.Value = JsonConvert.SerializeObject(colmarkogjithe);
            HfColllogarite.Value = JsonConvert.SerializeObject(colllogaritegjithe);
            HfColKategoriShpenzimi.Value = JsonConvert.SerializeObject(colKategoriShpenzimi);
            if (hfShtimModifikimValue == "konvertim" && clsAlternativaKushti.getAlternativa(Convert.ToInt32(Request.QueryString["konfigurim"]), "NADSSAK") == "Po")
                VendosDetajimeNeTrupNeseNukEkzistojne(colgjithe, colartgjithe, coldetgjithe, coldetgjithe2, data);
            HfColDetArt.Value = JsonConvert.SerializeObject(coldetgjithe);
            HfColDetArt2.Value = JsonConvert.SerializeObject(coldetgjithe2);
            HfColNjesAdminis.Value = JsonConvert.SerializeObject(colmaggjithe);
            HfColNjesiArt.Value = JsonConvert.SerializeObject(colnjesigjithe);
            HfColTaksa.Value = JsonConvert.SerializeObject(coltaksagjithe);
            // HfColTvshArt.Value = JsonConvert.SerializeObject(listeTvshArtGjithe);
            //HfColTvshLlog.Value = JsonConvert.SerializeObject(listeTvshLlogGjithe);
            mbushComboBoxFazat(colTotal);
            //  hfFazat.Value = serializusi.Serialize(colTotal);
            ImbLogger.LogTraceShitje($"Mbaroi metoda mbushHiddenFieldet me parametra idKokeShitje:{idKokeShitje}, idKonfig:{idKonfig}, idPerdoruesi:{idPerdoruesi}, idNdermarrje:{idNdermarrje}, idkokamag:{idkokamag}, idkonfmag:{idkonfmag}, hfShtimModifikimValue:" + hfShtimModifikimValue + $", data:{data}, idKF:{idKF}");
        }
        private void merrLlojeTVSH(int idKonfig, int idPerdoruesi, int idNdermarrje, colArtikujt colArtikuj, colLlogarite colLlogari, int idKF)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda merrLlojeTVSH me parametra idKonfig:{idKonfig}, idPerdoruesi:{idPerdoruesi}, idNdermarrje:{idNdermarrje},colArtikuj:{JsonConvert.SerializeObject(colArtikuj)}, colLlogari:{JsonConvert.SerializeObject(colLlogari)}, idKF:{idKF}");
            object[] listeTvshArt = new object[colArtikuj.Count];
            object[] listeTvshLlog = new object[colLlogari.Count];
            if (colArtikuj.Count > 0 || colLlogari.Count > 0) //nese ska rreshta kjo pjese nuk ka nevoj te behet
            {
                clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti();
                if (cmbModeli.Text == string.Empty)
                    konfig.mbushKonfigDefaultKomponentes(506, idNdermarrje);
                else
                    konfig.mbushKonfigAmbjSipasKod(cmbModeli.Text, idNdermarrje);
                KonfigurimTVSHGjateRregj llojTvsh;
                switch (clsAlternativaKushti.getAlternativa(idKonfig, "TVSH"))
                {
                    case "Ndermarje":
                        llojTvsh = KonfigurimTVSHGjateRregj.Ndermarrje;
                        break;
                    case "Sipas Artikullit":
                        llojTvsh = KonfigurimTVSHGjateRregj.Sipas_Artikullit;
                        break;
                    case "Sipas Klientit":
                        llojTvsh = KonfigurimTVSHGjateRregj.Sipas_Klientit;
                        break;
                    case "Pa TVSH":
                        llojTvsh = KonfigurimTVSHGjateRregj.Pa_TVSH;
                        break;
                    default:
                        llojTvsh = KonfigurimTVSHGjateRregj.Undefined;
                        break;
                }
                clsTaksa taksendermarje = new clsTaksa();
                taksendermarje.mbushTakseDefaultNdermarrje(idNdermarrje);
                DbCore.DbRegjistrim.colTaksa taksaNdermarrje = new DbCore.DbRegjistrim.colTaksa(idNdermarrje, DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, idPerdoruesi);
                clsKlientFurnitor kf = new clsKlientFurnitor(idKF);
                clsTaksa taksaKF = new clsTaksa(kf.IdTvsh);
                for (int i = 0; i < colArtikuj.Count; i++)
                {
                    clsArtikulli art = colArtikuj[i];
                    listeTvshArt[i] = art.ktheTvsh(llojTvsh, idPerdoruesi, taksendermarje, taksaNdermarrje, taksaKF);
                }

                for (int i = 0; i < colLlogari.Count; i++)
                {
                    clsLlogari llog = colLlogari[i];
                    listeTvshLlog[i] = llog.ktheTvsh(llojTvsh, idPerdoruesi, taksendermarje, taksaNdermarrje, taksaKF);
                }
            }

            HfColTvshArt.Value = JsonConvert.SerializeObject(listeTvshArt);
            HfColTvshLlog.Value = JsonConvert.SerializeObject(listeTvshLlog);
            ImbLogger.LogTraceShitje($"Filloi metoda merrLlojeTVSH me parametra idKonfig:{idKonfig}, idPerdoruesi:{idPerdoruesi}, idNdermarrje:{idNdermarrje},colArtikuj:{JsonConvert.SerializeObject(colArtikuj)}, colLlogari:{JsonConvert.SerializeObject(colLlogari)}, idKF:{idKF}");
        }

        private void zbrasHiddenFieldet(int idPerdoruesi, int idNdermarrje, string hfShtimModifikimValue)
        {
            mbushHiddenFieldet(0, 0, idPerdoruesi, new colTrupiShitje(), idNdermarrje, 0, 0, hfShtimModifikimValue, DateTime.Today, 0);
        }

        /// <summary>
        /// mbush kombon e konfigurimit dhe kthen konfigurimin e zgjedhur
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="m"></param>
        /// <param name="veprimi"></param>
        /// <returns></returns>
        private void mbushComboKonfigurimet(int idPerdoruesi, int idNdermarrje, int m, string veprimi, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda mbushComboKonfigurimet me parametra idPerdoruesi:{idPerdoruesi}, idNdermarje:{idNdermarrje}, veprimi:" + veprimi + $", idGjuha:{idGjuha}");
            colKonfigurimAmbjenti colKonfig = new colKonfigurimAmbjenti();
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
            if (veprimi == "shitje" || veprimi == "shitjediscount" || veprimi == "bazaar")
                konf.IdKategori = 1;
            else konf.IdKategori = 2;
            konf.IdNdermarje = idNdermarrje;
            if (cmbNiveli.Value != null)
            {
                konf.IdNivel = int.Parse(cmbNiveli.Value.ToString());
                colKonfig.mbushKonfigAmbjSipasIdKategoriIdNivel(konf.IdKategori, konf.IdNivel, idPerdoruesi, idGjuha, true);
            }
            else
                colKonfig.mbushKonfigAmbjSipasIdKategori(konf.IdKategori, konf.IdNdermarje, idPerdoruesi, idGjuha);
            IEnumerable<clsKonfigurimAmbjenti> result = clsFunksione.MerrKonfigurimShitje(colKonfig, veprimi);

            cmbModeli.Columns.Clear();
            ListBoxColumn colprove = new ListBoxColumn();
            colprove.FieldName = "KodKonfigAmbjente";
            colprove.Caption = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci);
            ListBoxColumn colemer = new ListBoxColumn();
            colemer.FieldName = "PershkrimKonfigAmbjente";
            colemer.Caption = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci);
            cmbModeli.TextFormatString = "{0}";
            cmbModeli.Columns.Add(colprove);
            cmbModeli.Columns.Add(colemer);
            cmbModeli.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            //cmbModeli.DropDownStyle = DropDownStyle.DropDown; komentuar sepse kur eshte drop down lejon shtimin e nje lloj konfigurimi te ndryshem nga lista
            cmbModeli.DataSource = result;
            cmbModeli.ValueField = "IdKonfigAmbjente";
            cmbModeli.DataBind();
            if (m == 1)
                cmbModeli.SelectedIndex = 0;
            ImbLogger.LogTraceShitje($"Mbaroi metoda mbushComboKonfigurimet me parametra idPerdoruesi:{idPerdoruesi}, idNdermarje:{idNdermarrje}, veprimi:" + veprimi + $", idGjuha:{idGjuha}");
        }

        private bool konvertuarPlotesisht(bool isBlerje, int idNdermarrje, int[] ids, colTrupiShitje trupShitje)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda konvertuarPlotesisht me parametra isBlerje:{isBlerje}, idNdermarje:{idNdermarrje}, trupShitje:{JsonConvert.SerializeObject(trupShitje)}");
            List<String> artikujtKonvPlot = new List<string>(); //lista e artikujve te konvertuar plotesisht
            for (int i = 0, idsLength = ids.Length; i < idsLength; i++)
            {
                string ngjyra = clsKokaShitje.merrNgjyreKonvertime(isBlerje, idNdermarrje, ids[i]);
                if (ngjyra == "kuqe" || ngjyra == "gjelber")
                {
                    clsKokaShitje koka = new clsKokaShitje();
                    koka.mbushKokaShitjeSipasIDPaTrup(ids[i]);
                    lblMsgboxKonv.Text = String.Format("Dokumenti Nr.{0} Dt.{1} eshte konvertuar plotesisht, doni te vazhdoni?", koka.NrDok, koka.DtDok.ToShortDateString());
                    status1.Value = "konvertuar";
                    return true;
                }
                if (ngjyra == "verdhe") //kontrolli i rreshtave per dok te konvertuar pjeserisht
                {
                    //merr nga db gjithe rreshtat e konvertuar me pare per dokumentin qe do te konvertohet
                    DataTable artKonvertuarPlot = clsKokaShitje.merrArtikujTeKonvertuarPlotesisht(ids[i], idNdermarrje, isBlerje);
                    if (artKonvertuarPlot.Rows.Count == 0)
                        continue;

                    foreach (clsTrupiShitje trupi in trupShitje) // kontrolli per cdo rresht ne trup te dokumentit (qe ndodhen ne gride)
                    {
                        DataRow rreshtKonv = artKonvertuarPlot.AsEnumerable().FirstOrDefault(x => Convert.ToInt32(x["IDSHITJETRUPI"]) == trupi.IdShitjeTrupi); //nga rreshtat e konvertuar me pare kapet vetem rreshti qe perkon me rreshtin ne gride
                        if (rreshtKonv != null && (Convert.ToDouble(rreshtKonv["SASIAMBETUR"]) < trupi.Sasia) && artikujtKonvPlot.FirstOrDefault(x => x == rreshtKonv["KODI"].ToString()) == null) //nqs ekziston rreshti, kontrollohet per tejkalim te sasise dhe nqs ekziston apo jo ne listen e art qe do shfaqen ne mesazh
                            artikujtKonvPlot.Add(rreshtKonv["KODI"].ToString());
                    }

                }
            }
            if (artikujtKonvPlot.Count == 0)
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda konvertuarPlotesisht me parametra isBlerje:{isBlerje}, idNdermarje:{idNdermarrje}, trupShitje:{JsonConvert.SerializeObject(trupShitje)}");
                return false;
            }

            lblMsgboxKonv.Text = String.Format("Artikujt me kod: ({0}) jane konvertuar plotesisht, doni te vazhdoni?", string.Join(",", artikujtKonvPlot.ToArray())); //vendos artikujt ne mesazh
            status1.Value = "konvertuar";
            ImbLogger.LogTraceShitje($"Mbaroi metoda konvertuarPlotesisht me parametra isBlerje:{isBlerje}, idNdermarje:{idNdermarrje}, trupShitje:{JsonConvert.SerializeObject(trupShitje)}");
            return true;
        }

        /// <summary>
        /// Ruan/Modifikon nje objekt clsKokaShitje.
        /// Therret funksionin <see cref="krijoRegjistrim"/>
        /// Therret funksionin gjeneroKontabilizimShitje/gjeneroKontabilizimBlerje
        /// Therret funksionin <see cref="DbCore.DbRegjistrim.clsKokaShitje.ruaj"/> ose <see cref="DbCore.DbRegjistrim.clsKokaShitje.modifikoSh"/>
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNderViti"></param>
        /// <param name="veprimi"></param>
        /// <param name="statusDokumenti">Statusi me te cilin po ruhet dokumenti (I rregullt apo draft)</param>
        /// <param name="statusAprovimi"></param>
        /// <param name="modifiko"></param>
        /// <param name="cultinf"></param>
        /// <param name="rm"></param>
        /// <param name="idetapa"></param>
        /// <param name="shtimModifikim"></param>
        /// <param name="periudha"></param>
        /// <param name="fazat"></param>
        private void ruajRegjistrim(int idGjuha, int idNdermarrje, int idPerdoruesi, int idNdermarrjeVit, string veprimi, int statusDokumenti, bool printo, StatusAprovimi statusAprovimi, bool kase, bool modifiko, bool kontrolloSasi, bool kontrollokonvertim, CultureInfo cultinf, ResourceManager rm, string shtimModifikim, clsPeriudhaKontabel periudha, object fazat)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ruajRegjistrim per dokumentin  {txtNumer.Text} me parametra idGjuha:{idGjuha}, idNdermarje:{idNdermarrje}, idPerdoruesi:{idPerdoruesi}, idNdermarrjeVit:{idNdermarrjeVit}, veprimi:" + veprimi + $", statusDokumenti:{statusDokumenti}, printo:{printo}, StatusAprovimi:{statusAprovimi}, kase:{kase}, modifiko:{modifiko}, kontrolloSasi:{kontrolloSasi}, kontrollokonvertim:{kontrollokonvertim}, CultureInfo:{cultinf}, ResourceManager:{rm}, shtimModifikim:" + shtimModifikim + $", periudha:{periudha}, periudha:{JsonConvert.SerializeObject(periudha)}, fazat:{fazat}");


            clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti();
            if (cmbModeli.Text == string.Empty)
                konfig.mbushKonfigDefaultKomponentes(506, idNdermarrje);
            else
                konfig.mbushKonfigAmbjSipasKod(cmbModeli.Text, idNdermarrje);

            bool isShitje = (veprimi == "shitje" || veprimi == "shitjediscount" || veprimi == "bazaar");
            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(data_DateEdit.Date, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, isShitje ? KategoriDokumenti.Shitje : KategoriDokumenti.Blerje, konfig.IdKonfigAmbjente))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                gabimNeRuajtje(statusDokumenti, true);
                return;
            }

            string kodSoftueris = WebConfigurationManager.AppSettings["kodSoftueri"];
            if ((cbEinvoice.Checked && shtimModifikim == "modifikim" && txtNIVF.Text != "") || (cbFiskalizo.Checked && shtimModifikim == "modifikim" && txtIIC.Text != ""))
                txtIIC.Text = txtIIC.Text;
            else
            {
                if (txtIIC.Text == "" && new clsNdermarrje(IdNdermarrja).Fiskalizimi)
                {
                    var txtIICS = clsFunksione.GjeneroIIC(new clsNdermarrje(idNdermarrje), txtNumer.Text, txtTotal1.Text, "ur271so291", kodSoftueris);
                    if (txtIICS == "Ju lutem ngarkoni filen e passwordit!")
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem ngarkoni filen e passwordit!", pnlMesazhi);
                        return;

                    }
                    else if (txtIICS == "Ju lutem ngarkoni certifikaten e sigurise!")
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem ngarkoni certifikaten e sigurise!", pnlMesazhi);
                        return;

                    }
                }

            }
            System.Diagnostics.Stopwatch myWatchRegjistrimShitje = System.Diagnostics.Stopwatch.StartNew();
            clsKokaShitje kokeShitje;
            bool printofature = false;
            bool printogarancifature = false;
            bool pageseFature = false;
            pergjigja.ClientVisible = false;
            if (!Page.IsValid)
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda ruajRegjistrim per dokumentin  {txtNumer.Text} sepse faqja nuk u validua me sukses. Parametra idGjuha:{idGjuha}, idNdermarje:{idNdermarrje}, idPerdoruesi:{idPerdoruesi}, idNdermarrjeVit:{idNdermarrjeVit}, veprimi:" + veprimi + $", statusDokumenti:{statusDokumenti}, printo:{printo}, StatusAprovimi:{statusAprovimi}, kase:{kase}, modifiko:{modifiko}, kontrolloSasi:{kontrolloSasi}, kontrollokonvertim:{kontrollokonvertim}, CultureInfo:{cultinf}, ResourceManager:{rm}, shtimModifikim:" + shtimModifikim + $", periudha:{periudha}, periudha:{JsonConvert.SerializeObject(periudha)}, fazat:{fazat}");
                gabimNeRuajtje(statusDokumenti, false);
                return;
            }
            if (cmbNiveli.SelectedItem.GetFieldValue("Kodi").ToString() == "FSH" && (shtimModifikim == "shtim" || shtimModifikim == "klonim" || shtimModifikim == "kthim" || shtimModifikim == "kthimVod" || shtimModifikim == "bli"))
            {
                if ((bool)hfTeDrejta["VetemKonvertimFSH"])
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", cultinf), pnlMesazhi);
                    ImbLogger.LogTraceShitje($"Mbaroi metoda ruajRegjistrim per dokumentin {txtNumer.Text} sepse nuk keni te drejta.Parametra idGjuha:{idGjuha}, idNdermarje:{idNdermarrje}, idPerdoruesi:{idPerdoruesi}, idNdermarrjeVit:{idNdermarrjeVit}, veprimi:" + veprimi + $", statusDokumenti:{statusDokumenti}, printo:{printo}, StatusAprovimi:{statusAprovimi}, kase:{kase}, modifiko:{modifiko}, kontrolloSasi:{kontrolloSasi}, kontrollokonvertim:{kontrollokonvertim}, CultureInfo:{cultinf}, ResourceManager:{rm}, shtimModifikim:" + shtimModifikim + $", periudha:{periudha}, periudha:{JsonConvert.SerializeObject(periudha)}, fazat:{fazat}");
                    gabimNeRuajtje(statusDokumenti, true);
                    return;
                }
            }
            //clsMenuInfo.ShtoMesazhGabimi(MenuInfo, string.Empty, pnlMesazhi);


            int idskema = int.Parse(hfSkema.Value);
            if (!modifiko && statusAprovimi == StatusAprovimi.Undefined)
                idskema = 0;

            int.TryParse(Request.QueryString["id"], out int idNgaQueryString);
            int idetapa = 0;
            int idDokPerAprovim = 0;
            if (shtimModifikim == "modifikim")
            {
                idDokPerAprovim = idNgaQueryString;
                if (Request.QueryString["vjenNga"] != null) // etapa e hapur
                    idetapa = int.Parse(Request.QueryString["idetapa"]);
                else
                {//etapa e fundit kur hapet nga shitja
                    clsEtapeAprovimi etapafund = new clsEtapeAprovimi();
                    etapafund.ktheEtapeFunditSipasKokaShitjeDhePerdorues(idDokPerAprovim, idPerdoruesi);
                    idetapa = etapafund.IdEtapa;
                }
            }
            clsKlientFurnitor kf;

            if (statusAprovimi == StatusAprovimi.Per_Aprovim && statusDokumenti != 1 && clsAlternativaKushti.getAlternativa(konfig.IdKonfigAmbjente, "SDAF") == "Ruajtur")
            {
                DbCore.IMBUtils.Types.Converter.Parse(txtTotal1.Text, out double totalFature, "txtTotal1");
                DbCore.IMBUtils.Types.Converter.Parse(txtTVSH1.Text, out double tvshFature, "txtTVSH1");
                int idKf = clsKlientFurnitor.MerrIdKlientFurnitor(btnKlienti.Text.Split(' ')[0], idNdermarrje);
                int idKatDok = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(Convert.ToInt32(cmbNiveli.Value));
                StatusAprovimi stsAprovimi = clsEtapeAprovimi.MerrStatusAprovimi(idskema, new clsDatabaseRegjistrim(), idDokPerAprovim, idPerdoruesi, statusAprovimi, idetapa, idKatDok, idNdermarrje, idKf, totalFature - tvshFature, 0);
                if (stsAprovimi == StatusAprovimi.Aprovuar)
                    statusDokumenti = 1;
            }

            bool rivleresim = false;
            colTrupiMagazina tr = new colTrupiMagazina();
            clsMesazh mesazhi = isValidRegjistrim(idNdermarrjeVit, idNdermarrje, statusDokumenti, out kf, rm, cultinf, shtimModifikim, idPerdoruesi);
            if (!mesazhi.Status)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhi.PershkrimMesazhi, pnlMesazhi);
                ImbLogger.LogTraceShitje($"Mbaroi metoda ruajRegjistrim per dokumentin  {txtNumer.Text} sepse pati problem gjate validimit.Parametra idGjuha:{idGjuha}, idNdermarje:{idNdermarrje}, idPerdoruesi:{idPerdoruesi}, idNdermarrjeVit:{idNdermarrjeVit}, veprimi:" + veprimi + $", statusDokumenti:{statusDokumenti}, printo:{printo}, StatusAprovimi:{statusAprovimi}, kase:{kase}, modifiko:{modifiko}, kontrolloSasi:{kontrolloSasi}, kontrollokonvertim:{kontrollokonvertim}, CultureInfo:{cultinf}, ResourceManager:{rm}, shtimModifikim:" + shtimModifikim + $", periudha:{periudha}, periudha:{JsonConvert.SerializeObject(periudha)}, fazat:{fazat}");
                gabimNeRuajtje(statusDokumenti, true);
                return;
            }

            bool gjenerodokmag = (bool)hfState["GJDM"];
            string arkabanka = "arka";

            bool autoshitje = (bool)hfState["LAVK"];
            bool tollona = clsAlternativaKushti.getAlternativa(konfig.IdKonfigAmbjente, "RSHTT") == "Po";
            bool tollonakastrati = clsAlternativaKushti.getAlternativa(konfig.IdKonfigAmbjente, "RSHTTK") == "Po";
            bool tollonakastratielektronik = clsAlternativaKushti.getAlternativa(konfig.IdKonfigAmbjente, "RSHTTKE") == "Po";
            bool krijoartri = clsAlternativaKushti.getAlternativa(konfig.IdKonfigAmbjente, "BKAR") == "Po";

            string llojZevendesimi = clsAlternativaKushti.getAlternativa(konfig.IdKonfigAmbjente, "ZT");
            bool zevendesimtollona = !(llojZevendesimi == "Jo");
            bool zevendesimtollonakastrati = clsAlternativaKushti.getAlternativa(konfig.IdKonfigAmbjente, "ZTK") == "Po";
            string shfaqmesazhapolupe = "jo";
            string shfaqmesazhapolupemagazina = "jo";//per rastet nqs do merret parasysh magazina ne shfaqen e mesazhit
            string shfaqmesazhapolupebanka = "jo";
            string shfaqmesazhapolupeVDK = "jo";
            string mesazhinformues = string.Empty;
            colKonvertimi colkonvetimi = merrIdDokKonvertuar();
            clsKokaShitje kokaMema = new clsKokaShitje();
            bool eshteOwn = (bool)hfState["OwnShop"];
            clsKonfigurimAmbjenti konfmag = new clsKonfigurimAmbjenti(konfig.IdKonfigurimi, idGjuha);
            clsKusht kushtamor = new clsKusht(konfig.IdKonfigAmbjente, "ZDAM");
            clsKokaShitje faturashitjengaurdhershitjamekupontatimor = hfLidhur.Value == "True" ? null : new clsKokaShitje();
            clsKonfigurimAmbjenti konfamortizimi = new clsKonfigurimAmbjenti(kushtamor.Vlera, idGjuha);
            DbCore.DbAsete.colSerialetMagazine serialemag = new DbCore.DbAsete.colSerialetMagazine();
            bool mekontabilizim = false;
            if (statusDokumenti == 1)//nese nuk eshte draft do gjeneroje kontabilizim perndryshe jo
            {
                if (hfKontabilizimi.Value == "1" || hfKontabilizimi.Value == "2")
                {
                    mekontabilizim = true;
                }
            }
            string mesazhmevonshem = "";
            var serialetUnike = mySessionObjects.MerrNgaSession<colSerialeUnikeMagazina>(Session, Constants.SERIALE_UNIKE_TE_NGARKUAR, hfState.Get<string>("guidString"));

            try
            {
                if (kf.IdKlientFurnitor < 1)
                    kokeShitje = krijoRegjistrim(veprimi, ref gjenerodokmag, idNdermarrjeVit, idPerdoruesi, idNdermarrje, statusDokumenti, 0, string.Empty, konfig, konfmag, statusAprovimi, out shfaqmesazhapolupe, out mesazhinformues, kokaMema, kase, serialemag, kontrolloSasi, faturashitjengaurdhershitjamekupontatimor, tollona, autoshitje, cultinf, rm, shtimModifikim, periudha, mekontabilizim, eshteOwn, idetapa, tollonakastrati, tollonakastratielektronik, fazat, llojZevendesimi, zevendesimtollonakastrati, hfShtimModifikim.Value == "bli", krijoartri, idGjuha, serialetUnike);
                else
                    kokeShitje = krijoRegjistrim(veprimi, ref gjenerodokmag, idNdermarrjeVit, idPerdoruesi, idNdermarrje, statusDokumenti, kf.IdKlientFurnitor, kf.KodKlientFurnitor, konfig, konfmag, statusAprovimi, out shfaqmesazhapolupe, out mesazhinformues, kokaMema, kase, serialemag, kontrolloSasi, faturashitjengaurdhershitjamekupontatimor, tollona, autoshitje, cultinf, rm, shtimModifikim, periudha, mekontabilizim, eshteOwn, idetapa, tollonakastrati, tollonakastratielektronik, fazat, llojZevendesimi, zevendesimtollonakastrati, hfShtimModifikim.Value == "bli", krijoartri, idGjuha, serialetUnike);
            }
            catch (MyException myEx)
            {
                ImbLogger.LogErrorShitje($"Exception:{myEx}");
                DbCore.IMBUtils.Logging.ImbLogger.Error(myEx);
                if (myEx.Message.Contains('?'))
                {
                    clsMenuInfo.ShtoPyetje(MenuInfo, myEx.Message, pnlMesazhi, idGjuha);
                    pergjigja.Text = "Serialet";
                }
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, myEx.Message, pnlMesazhi);
                gabimNeRuajtje(statusDokumenti, true);
                return;
            }
            catch (Exception e)
            {
                ImbLogger.LogErrorShitje($"Exception:{e}");
                DbCore.IMBUtils.Logging.ImbLogger.Error(e);
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, e.Message, pnlMesazhi);
                gabimNeRuajtje(statusDokumenti, true);
                return;
            }
            int[] ids = new int[0];
            var previousPageID = DbCore.IMBUtils.Types.Converter.MerrVlereOseDefault<string>(Request.QueryString["pageCacheId"]);
            colTaksa taksat = new colTaksa(kokeShitje.IdNdermarrje, kokeShitje.IdPerdoruesi);
            if (!string.IsNullOrEmpty(previousPageID))
            {
                var pageCache = GlobalCacheManager.GetPageCacheByPageID(previousPageID);
                ids = (int[])pageCache["idkonvertimi"];
            }
            if (shtimModifikim == "konvertim" && kontrollokonvertim && konvertuarPlotesisht(false, idNdermarrje, ids, kokeShitje.OColTrupiShitje))
            {
                gabimNeRuajtje(statusDokumenti, false);
                return;
            }
            if (shtimModifikim == "konvertimblerje" && kontrollokonvertim && konvertuarPlotesisht(true, idNdermarrje, ids, kokeShitje.OColTrupiShitje))
            {
                gabimNeRuajtje(statusDokumenti, false);
                return;
            }

            clsMesazh mesazh = new clsMesazh(false);
            clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
            int idViti = (int)hfState["idViti"];
            tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, clsFunksione.GetKomponente(Page.Request));
            clsNdermarrje nderm = new clsNdermarrje(IdNdermarrja);
            string serverUrl = clsFunksione.ktheServerUrl(Request);
            DbCore.DbArkaBanka.clsVeprimBankaKoka veprimebanka = new DbCore.DbArkaBanka.clsVeprimBankaKoka();
            bool dergoemail = clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "LE") == "Po";
            bool dergoemailVfOne = clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "DEVFOne") == "Po";
            bool dergoemailMag = clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "DEFM") == "Po";
            bool kontrolloSasiKonvertimiDheKthimi = clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "NK") == "Po";
            bool eshteDokLidhur = false;
            bool kontrolloIMEIFifo = false;
            decimal total1;
            Converter.Parse(txtTotal1.Text, out total1, "txtPerqindje");

            decimal vlera_totale = total1 - Convert.ToDecimal(kokeShitje.TotaliMeZbritjeMeTVSH) + clsKlientFurnitor.MerrDetyrimKf(kf.IdKlientFurnitor, kokeShitje.DtDok);
            if (clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "AFI") == "Po")
                kontrolloIMEIFifo = true;
            if (shtimModifikim == "shtim" || shtimModifikim == "klonim" || shtimModifikim == "shtimraport" || shtimModifikim == "konvertim" || shtimModifikim == "konvertimblerje" || shtimModifikim == "rezervim" || shtimModifikim == "kthim" || shtimModifikim == "kthimVod" || shtimModifikim == "bli")
            {
                if (statusDokumenti == 1 && clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "KKLKF") == "Po" && cmbMenyrePagese.Text != "Pagese Automatike")
                {
                    if (kf.LimitBllokues > 0 && vlera_totale > kf.LimitBllokues)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgTotalFatureKaluarLimitBllokues", cultinf), pnlMesazhi);
                        gabimNeRuajtje(statusDokumenti, true);
                        return;
                    }
                }
                if (hfShtimModifikim.Value == "konvertim")
                {
                    if ((statusDokumenti == 1 && !tedrejtaInfo.DShtim) || (statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft))
                    {
                        ImbLogger.LogWarningShitje("Ju nuk keni te drejta per kete veprim!");
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                        gabimNeRuajtje(statusDokumenti, true);
                        return;
                    }
                }
                else
                if ((statusDokumenti == 1 && !tedrejtaInfo.DShtim) || (statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgNukKeniTeDrejta"], pnlMesazhi);
                    gabimNeRuajtje(statusDokumenti, true);
                    return;
                }
                hfArkiva.Set("kopjoArkiven", shtimModifikim != "shtim");
                clsKokaMagazina mag = new clsKokaMagazina();
                if (isShitje)
                {
                    try
                    {
                        string kodSoftueri = WebConfigurationManager.AppSettings["kodSoftueri"];
                        if (txtIIC.Text == "" && nderm.Fiskalizimi)
                            txtIIC.Text = clsFunksioneFiskalizimi.GjeneroIIC(new clsNdermarrje(idNdermarrje), txtNumer.Text, txtTotal1.Text, "ur271so291", kodSoftueri, Convert.ToDateTime(kokeShitje.DtDok.ToString().Split(' ')[0] + ' ' + kokeShitje.DtKrijimiPajisje.ToString().Split(' ')[1]));
                        DbData dbData = new DbData();
                        //filloi ruajtja me nr date idndermarjre
                        mesazh = kokeShitje.ruaj(idGjuha, serverUrl, true, hfNrAutoShitje, periudha.IdPeriudha, colkonvetimi, gjenerodokmag, out veprimebanka, idskema, statusAprovimi, idetapa,
                            out shfaqmesazhapolupemagazina, out shfaqmesazhapolupebanka, out shfaqmesazhapolupeVDK, kokaMema, 0, 0, dergoemail, eshteOwn, dergoemailVfOne, hfKodVFOne.Value, serialemag,
                            konfamortizimi, faturashitjengaurdhershitjamekupontatimor, out printofature, out printogarancifature, out pageseFature, mekontabilizim, out shfaqmesazhapolupe, cmbModeli.Text,
                            false, string.Empty, shtimModifikim == "kthim" ? idNgaQueryString : 0, tollona, zevendesimtollona, false, false, "", "", "", tollonakastrati, tollonakastratielektronik, false, "",
                            false, false, zevendesimtollonakastrati, cbRenditje.Checked, kontrolloSasiKonvertimiDheKthimi, new colKokaShitje(), kontrolloIMEIFifo, hfShtimModifikim.Value == "bli",
                            !cmbModeli.Text.Contains("USHmag"), ref dbData, krijoartri, hfKodKuponiDD.Value, hfMsisdnBazaari.Value, false, out mesazhmevonshem, false, String.IsNullOrEmpty(txtNrDokMagazine.Text), txtIIC.Text, txtNIVF.Text);
                        if (mesazh.Status)
                        {
                            var viti = new clsViti(periudha.IdViti).KodiViti;
                            if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                            {
                                if (nderm.Fiskalizimi && cbFiskalizo.Checked && statusDokumenti == 1 && veprimi == "shitje" && (shtimModifikim == "shtim" || shtimModifikim == "klonim" || shtimModifikim == "konvertim" || hfState.Get("idstatusdok").ToString() == "0" || shtimModifikim == "kthim") && cmbNiveli.SelectedItem.GetFieldValue("Kodi").ToString() == "FSH")
                                {
                                    clsKlientFurnitor kF = new clsKlientFurnitor(kokeShitje.IdKlientFurnitor);
                                    var arka = new clsBanka();
                                    if (btneArka.Text == "")
                                        arka = new clsBanka(kF.EmriBanka);
                                    else
                                        arka.mbushBankeSipasKodit(btneArka.Text, idNdermarrje);
                                    var dateMaturimi = dateMaturimi_DateEdit.Date.ToString().Split()[0];
                                    clsKokaShitje kokeshitjeje = new clsKokaShitje(kokeShitje.IdShitjeKoka);
                                    dateMaturimi = dateMaturimi.Replace('/', '-');
                                    string[] dateMaturimiList = dateMaturimi.Split(new[] { '-' }, 3);
                                    var dateMaturimiFormatuar = $"{dateMaturimiList[2]}-{dateMaturimiList[1]}-{dateMaturimiList[0]}";
                                    string[] emriMbiemriOperatori = cmbOperatori.Text.Split(' ');
                                    var operatori = clsOperator.MerrKodOperatoriSipasId(kokeShitje.IdOperator, nderm.IdNdermarrje);
                                    var degeAdministrative = clsDegeAdministrative.ktheDegeAdministrativeSipasiD(kokeShitje.IdDegeAdministrative);
                                    var iicSignature = clsFunksioneFiskalizimi.ktheIICSignature(nderm, txtNumer.Text, txtTotal1.Text, "ur271so291", kodSoftueri, kokeshitjeje.DtKrijimi);
                                    DateTimeOffset dtKrijimiPajisjeOffset = new DateTimeOffset(kokeshitjeje.DtKrijimiPajisje);
                                    var timeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
                                    var dtKrijimiOffset = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
                                    bool isEinvoice = cbEinvoice.Checked;
                                    if (new clsKlientFurnitor(kokeshitjeje.IdKlientFurnitor).TipiId != "NUIS")
                                        isEinvoice = false;
                                    var mesazhInvoice = clsFunksioneFiskalizimi.GjeneroMesazhInvoice(nderm, kokeShitje.NrDok, kokeshitjeje.Totali.ToString(), "ur271so291", kodSoftueri, kokeShitje.IIC, iicSignature,
                                        txtAdresaFaturimit.Text, txtPershkrimi.Text, kokeshitjeje.Totali.ToString(), dateMaturimiFormatuar, kokeshitjeje.Totali.ToString(), txtTVSH1.Text, txtPerqindje.Text, btnKlienti.Text.Split(' ')[0], txtNipt.Text, txtTotaliMeZbritjePaTVSH1.Text,
                                        txtQytetiK.Text, txtQytetiK.Text, "", "", kokeShitje.merrTrupShitje(), arka.KodiTCR, cmbMenyrePagese.Text, nderm, kf.KodKlientFurnitor, kokeShitje.Zbritje.ToString(), txtTotaliMeZbritjePaTVSH1.Text, txtNivfKthim.Text,
                                        Convert.ToDateTime(kokeshitjeje.DtDok.ToString().Split(' ')[0] + ' ' + dtKrijimiPajisjeOffset.UtcDateTime.ToString().Split(' ')[1]), cmbOperatori.Text, false, kokeShitje.Kursi.ToString(), isEinvoice,
                                        degeAdministrative["KODNJESIEBIZNES"].ToString(), cmbTipiIVetefaturimit.Text, operatori.ItemArray[0].ToString(), isShitje, false, kokeshitjeje.Dogana, kokeshitjeje.DtMbarimi.ToString(), kokeshitjeje.DtFillimi.ToString(), kokeshitjeje.DtDok.ToString(), dtKrijimiOffset, idNdermarrje, idPerdoruesi, viti);
                                    var nivfFature = clsFunksioneFiskalizimi.InvokeService(mesazhInvoice[0], "FIC", false);
                                    if (nivfFature[0] == "Buyer TIN doesn't exist in RTP." || nivfFature[0] == "Buyer is not active in the RTP.")
                                    {
                                        var objekti = ktheObjektPerNotify(kokeshitjeje, false, "Deshtim", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), nivfFature[0], nivfFature[1], "Fature Fiskalizimi", mesazhInvoice[1], nivfFature[2]);
                                        clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);
                                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Fatura nuk u fiskalizua:Klienti nuk eshte aktiv ne regjistrin e tatimeve", pnlMesazhi);

                                    }
                                    else if (nivfFature[0] == "Buyers TIN is not in the correct format.")
                                    {
                                        var objekti = ktheObjektPerNotify(kokeshitjeje, false, "Deshtim", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), nivfFature[0], nivfFature[1], "Fature Fiskalizimi", mesazhInvoice[1], nivfFature[2]);

                                        clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);
                                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Fatura nuk u fiskalizua: Formati i NIPT-it nuk eshte i rregullt", pnlMesazhi);
                                    }
                                    else if (nivfFature[1] != null)
                                    {
                                        var objekti = ktheObjektPerNotify(kokeshitjeje, false, "Deshtim", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(),nivfFature[0], nivfFature[1],"Fature Fiskalizimi", mesazhInvoice[1], nivfFature[2]);

                                        clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);
                                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim me fiskalizimin, fatura nuk u fiskalizua!" + $"Error:{nivfFature[1]}" + $" Pershkrimi i errorit:{nivfFature[0]}", pnlMesazhi);
                                    }
                                    
                                    else
                                    {
                                       
                                        string[] EIC = new string[1];
                                        if (cbEinvoice.Checked)
                                        {

                                            EIC = DergoEinvoice(kokeshitjeje, kf, nderm, iicSignature, nivfFature[0], operatori,viti);
                                            if (EIC[1] != null)
                                            {
                                                if(EIC[1] == "Fatura nuk u be Einvoice")
                                                {
                                                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Servisi i E-invoice nuk pergjigjet, ju lutem provoni perseri me vone!", pnlMesazhi);
                                                    var objektiEinvoice = ktheObjektPerNotify(kokeshitjeje, false, "Deshtim", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), "Error serveri e-invoice", EIC[1], "Fature E-invoice", EIC[3], "Problem serveri e-invoice");
                                                    clsFunksioneFiskalizimi.dergoWebhookNotify(objektiEinvoice, true);
                                                    EIC[0] = "";
                                                }
                                                else
                                                {
                                                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim me krijimin e fatures elektronike!" + $"Error:{EIC[1]}" + $" Pershkrimi i errorit:{EIC[0]}", pnlMesazhi);
                                                    var objektiEinvoice = ktheObjektPerNotify(kokeshitjeje, false, "Deshtim", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), EIC[0], EIC[1], "Fature E-invoice", EIC[3], EIC[2]);
                                                    clsFunksioneFiskalizimi.dergoWebhookNotify(objektiEinvoice, true);
                                                    EIC[0] = "";
                                                }

                                            }

                                            else
                                            {

                                                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Fatura elektronike u krijua me sukses!", pnlMesazhi);
                                                //var fatura = clsFunksioneFiskalizimi.merrEinvoice(nderm, EIC[0], DateTime.UtcNow);
                                                //var base64 = clsFunksioneFiskalizimi.InvokeService(fatura, "Pdf", true);
                                                //string b64string = base64[0];
                                                //blob.Value = JsonConvert.SerializeObject(new { b64 = b64string });

                                                if (cbPrinto.Checked)
                                                {
                                                    var fatura = clsFunksioneFiskalizimi.merrEinvoice(nderm, EIC[0], DateTime.UtcNow);
                                                    var base64 = clsFunksioneFiskalizimi.InvokeService(fatura, "Pdf", true);
                                                    string b64string = base64[0];
                                                    blob.Value = JsonConvert.SerializeObject(new { b64 = b64string });
                                                }

                                                //ketu e dim qe fatura ka shkuar me sukses te einvoice dhe kemi marr pergjigje suksesi, kshu qe mund te behet ktu kodi ku do hapet pdf
                                            }


                                        }
                                        var objektiEinvoiceSukses = new object();
                                        clsKokaShitje.shtoKodinNivfTeShitja(kokeShitje.IdShitjeKoka, nivfFature[0], nderm.IdNdermarrje, EIC[0], txtIIC.Text);
                                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Fatura u fiskalizua me sukses!", pnlMesazhi);
                                        var objekti = ktheObjektPerNotify(kokeshitjeje, false, "Sukses", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), nivfFature[0], nivfFature[1], "Fature Fiskalizimi", mesazhInvoice[1], nivfFature[2]);
                                        clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);
                                        if (cbEinvoice.Checked)
                                        {
                                            if (EIC[0] == "")
                                                EIC[0] = "";
                                            else
                                            {
                                                objektiEinvoiceSukses = ktheObjektPerNotify(kokeshitjeje, false, "Sukses", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), EIC[0], EIC[1], "Fature E-invoice", EIC[3], EIC[2]);
                                                clsFunksioneFiskalizimi.dergoWebhookNotify(objektiEinvoiceSukses, true);
                                            }
                                        }

                                    }

                                }
                            }

                            hfStatusRuajtje.Value = mesazh.PershkrimMesazhi;
                            clsKokaShitje kokaEshitjes = new clsKokaShitje(kokeShitje.IdShitjeKoka);
                            object[] koka = kokeShitje.krijoObjektPerWebhook(kokaEshitjes, "Shtim", "Shitje");
                            colTrupiShitje trupi = clsKokaShitje.merrTrupiShitjePerWebhook(kokeShitje.IdShitjeKoka);

                            var trupiShitje = trupi.ToList();
                            JObject[] json = new JObject[trupiShitje.Count];
                            int counter = 0;
                            foreach (var t in trupiShitje)
                            {

                                json[counter] = JObject.Parse(JsonConvert.SerializeObject(t));
                                json[counter].Add("kodbari", clsKodbari.ktheKodbarSipasIdArtikulliNjesiaKodbariIPare((string)json[counter].SelectToken("Kodi"), kokeShitje.IdNdermarrje));
                                counter++;
                            }
                            Object kokaDheTrupi;
                            kokaDheTrupi = new
                            {
                                meta = koka[0],
                                koka = koka[1],
                                trupi = json
                            };
                            hfObjektRuajtur.Value = JsonConvert.SerializeObject(kokaDheTrupi);
                            clsFunksione.dergoWebhookDatasetEndpoint(kokaDheTrupi);
                        }
                    }
                    catch (Exception err)
                    {
                        ImbLogger.LogErrorShitje($"Exception:{err}");
                        NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        gabimNeRuajtje(statusDokumenti, true);
                        return;
                    }
                    if (!mesazh.Status)
                    {
                        clsFunksione.KontrolloPerSerialeTePerdoruraNeDokDraft(Session, Container1);
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        gabimNeRuajtje(statusDokumenti, true);
                        return;
                    }
                    mag.mbushKokaMagazinaSipasIDGjenerues(kokeShitje.IdShitjeKoka, 2, kokeShitje.IdKonfigAmbjente);
                }
                else
                {
                    try
                    {
                        string kodSoftueri = WebConfigurationManager.AppSettings["kodSoftueri"];
                        DbData dbData = new DbData();
                        bool kthimVod = hfShtimModifikim.Value == "kthimVod";
                        if (txtIIC.Text == "" && nderm.Fiskalizimi)
                            txtIIC.Text = clsFunksioneFiskalizimi.GjeneroIIC(new clsNdermarrje(idNdermarrje), txtNumer.Text, txtTotal1.Text, "ur271so291", kodSoftueri, Convert.ToDateTime(kokeShitje.DtDok.ToString().Split(' ')[0] + ' ' + kokeShitje.DtKrijimiPajisje.ToString().Split(' ')[1]));
                        mesazh = kokeShitje.ruaj(idGjuha, serverUrl, false, hfNrAutoShitje, periudha.IdPeriudha, colkonvetimi, gjenerodokmag, out veprimebanka, idskema, statusAprovimi, idetapa, out shfaqmesazhapolupemagazina, out shfaqmesazhapolupebanka, out shfaqmesazhapolupeVDK, kokaMema, 0, 0, dergoemail, eshteOwn, dergoemailVfOne, hfKodVFOne.Value, serialemag, konfamortizimi, faturashitjengaurdhershitjamekupontatimor, out printofature, out printogarancifature, out pageseFature, mekontabilizim, out shfaqmesazhapolupe, cmbModeli.Text, false, string.Empty, shtimModifikim == "kthim" ? idNgaQueryString : 0, tollona, zevendesimtollona, false, false, "", "", "", tollonakastrati, tollonakastratielektronik, false, "", false, false, zevendesimtollonakastrati, cbRenditje.Checked, kontrolloSasiKonvertimiDheKthimi, new colKokaShitje(), kontrolloIMEIFifo, hfShtimModifikim.Value == "bli", false, ref dbData, krijoartri, hfKodKuponiDD.Value, hfMsisdnBazaari.Value, kthimVod, out mesazhmevonshem, false, String.IsNullOrEmpty(txtNrDokMagazine.Text), txtIIC.Text, txtNIVF.Text);
                        if (mesazh.Status)
                        {
                            if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                            {
                                if (nderm.Fiskalizimi && cbFiskalizo.Checked && statusDokumenti == 1 && veprimi == "blerje" && (shtimModifikim == "shtim" || shtimModifikim == "klonim" || shtimModifikim == "konvertim" || hfState.Get("idstatusdok").ToString() == "0" || shtimModifikim == "kthim") && cmbNiveli.SelectedItem.GetFieldValue("Kodi").ToString() == "FB")
                                {
                                    clsKlientFurnitor kF = new clsKlientFurnitor(kokeShitje.IdKlientFurnitor);
                                    var arka = new clsBanka();
                                    if (btneArka.Text == "")
                                        arka = new clsBanka(kF.EmriBanka);
                                    else
                                        arka.mbushBankeSipasKodit(btneArka.Text, idNdermarrje);
                                    var dateMaturimi = dateMaturimi_DateEdit.Date.ToString().Split()[0];
                                    clsKokaShitje kokeshitjeje = new clsKokaShitje(kokeShitje.IdShitjeKoka);
                                    dateMaturimi = dateMaturimi.Replace('/', '-');
                                    string[] dateMaturimiList = dateMaturimi.Split(new[] { '-' }, 3);
                                    var dateMaturimiFormatuar = $"{dateMaturimiList[2]}-{dateMaturimiList[1]}-{dateMaturimiList[0]}";
                                    string[] emriMbiemriOperatori = cmbOperatori.Text.Split(' ');
                                    var operatori = clsOperator.MerrKodOperatoriSipasId(kokeShitje.IdOperator, nderm.IdNdermarrje);
                                    var degeAdministrative = clsDegeAdministrative.ktheDegeAdministrativeSipasiD(kokeShitje.IdDegeAdministrative);
                                    var timeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
                                    var dtKrijimiOffset = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
                                    DateTimeOffset dtKrijimiPajisjeOffset = new DateTimeOffset(kokeshitjeje.DtKrijimiPajisje);
                                    if (txtIIC.Text == "")
                                        txtIIC.Text = clsFunksioneFiskalizimi.GjeneroIIC(new clsNdermarrje(idNdermarrje), txtNumer.Text, txtTotal1.Text, "ur271so291", kodSoftueri, Convert.ToDateTime(kokeshitjeje.DtDok.ToString().Split(' ')[0] + ' ' + kokeshitjeje.DtKrijimi.ToString().Split(' ')[1]));
                                    var iicSignature = clsFunksioneFiskalizimi.ktheIICSignature(nderm, txtNumer.Text, txtTotal1.Text, "ur271so291", kodSoftueri, kokeshitjeje.DtKrijimi);
                                    var viti = new clsViti(periudha.IdViti).KodiViti;
                                    bool isEinvoice = cbEinvoice.Checked;
                                    if (new clsKlientFurnitor(kokeshitjeje.IdKlientFurnitor).TipiId != "NUIS")
                                        isEinvoice = false;
                                    var mesazhInvoice = clsFunksioneFiskalizimi.GjeneroMesazhInvoice(nderm, kokeShitje.NrDok, txtTotal1.Text, "ur271so291", kodSoftueri, kokeShitje.IIC, iicSignature,
                                        txtAdresaFaturimit.Text, txtPershkrimi.Text, txtTotal1.Text, dateMaturimiFormatuar, txtTotal1.Text, txtTVSH1.Text, txtPerqindje.Text, btnKlienti.Text.Split(' ')[0], txtNipt.Text, txtTotaliMeZbritjePaTVSH1.Text,
                                        txtQytetiK.Text, txtQytetiK.Text, "", "", kokeShitje.merrTrupShitje(), arka.KodiTCR, cmbMenyrePagese.Text, nderm, kf.KodKlientFurnitor, kokeShitje.Zbritje.ToString(), txtTotaliMeZbritjePaTVSH1.Text, txtNivfKthim.Text, Convert.ToDateTime(kokeshitjeje.DtDok.ToString().Split(' ')[0] + ' ' + dtKrijimiPajisjeOffset.UtcDateTime.ToString().Split(' ')[1]), cmbOperatori.Text, false, kokeShitje.Kursi.ToString(), isEinvoice, degeAdministrative["KODNJESIEBIZNES"].ToString(), cmbTipiIVetefaturimit.Text, operatori.ItemArray[0].ToString(), isShitje, false, kokeshitjeje.Dogana, kokeshitjeje.DtMbarimi.ToString(), kokeshitjeje.DtFillimi.ToString(), kokeshitjeje.DtDok.ToString(), dtKrijimiOffset, idNdermarrje, idPerdoruesi, viti);

                                    var nivfFature = clsFunksioneFiskalizimi.InvokeService(mesazhInvoice[0], "FIC", false);
                                    if (nivfFature[0] == "Buyer TIN doesn't exist in RTP." || nivfFature[0] == "Buyer is not active in the RTP.")
                                    {
                                        var objekti = ktheObjektPerNotify(kokeshitjeje, false, "Deshtim", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), nivfFature[0], nivfFature[1], "Fature Fiskalizimi", mesazhInvoice[1], nivfFature[2]);
                                        clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);
                                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Fatura nuk u fiskalizua:Klienti nuk eshte aktiv ne regjistrin e tatimeve", pnlMesazhi);

                                    }
                                    else if (nivfFature[0] == "Buyers TIN is not in the correct format.")
                                    {
                                        var objekti = ktheObjektPerNotify(kokeshitjeje, false, "Deshtim", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), nivfFature[0], nivfFature[1], "Fature Fiskalizimi", mesazhInvoice[1], nivfFature[2]);

                                        clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);
                                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Fatura nuk u fiskalizua: Formati i NIPT-it nuk eshte i rregullt", pnlMesazhi);
                                    }
                                    else if (nivfFature[1] != null)
                                    {
                                        var objekti = ktheObjektPerNotify(kokeshitjeje, false, "Deshtim", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), nivfFature[0], nivfFature[1], "Fature Fiskalizimi", mesazhInvoice[1], nivfFature[2]);

                                        clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);
                                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim me fiskalizimin, fatura nuk u fiskalizua!" + $"Error:{nivfFature[1]}" + $" Pershkrimi i errorit:{nivfFature[0]}", pnlMesazhi);
                                    }

                                    else
                                    {


                                        var objektiEinvoiceSukses = new object();
                                        clsKokaShitje.shtoKodinNivfTeShitja(kokeShitje.IdShitjeKoka, nivfFature[0], nderm.IdNdermarrje, txtEIC.Text, txtIIC.Text);
                                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Fatura u fiskalizua me sukses!", pnlMesazhi);
                                        var objekti = ktheObjektPerNotify(kokeshitjeje, false, "Sukses", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), nivfFature[0], nivfFature[1], "Fature Fiskalizimi", mesazhInvoice[1], nivfFature[2]);
                                        clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);

                                    }

                                }
                            }
                            hfStatusRuajtje.Value = mesazh.PershkrimMesazhi;
                            clsKokaShitje kokaEshitjes = new clsKokaShitje(kokeShitje.IdShitjeKoka);
                            object[] koka = kokeShitje.krijoObjektPerWebhook(kokaEshitjes, "Shtim", "Blerje");
                            colTrupiShitje trupi = clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka);
                            var trupiShitje = trupi.ToList();
                            JObject[] json = new JObject[trupiShitje.Count];
                            int counter = 0;
                            foreach (var t in trupiShitje)
                            {

                                json[counter] = JObject.Parse(JsonConvert.SerializeObject(t));
                                json[counter].Add("kodbari", clsKodbari.ktheKodbarSipasIdArtikulliNjesiaKodbariIPare((string)json[counter].SelectToken("Kodi"), kokeShitje.IdNdermarrje));
                                counter++;
                            }
                            Object kokaDheTrupi;
                            kokaDheTrupi = new
                            {
                                meta = koka[0],
                                koka = koka[1],
                                trupi = json
                            };
                            hfObjektRuajtur.Value = JsonConvert.SerializeObject(kokaDheTrupi);
                            clsFunksione.dergoWebhookDatasetEndpoint(kokaDheTrupi);
                        }
                    }
                    catch (Exception err)
                    {
                        if (err.Data.Contains("SerialetKeq"))
                        {
                            mesazh = new clsMesazh(false, "Serialet kane gjendje ne magazine");
                            var SerialetKeq = (System.Data.DataTable)err.Data["SerialetKeq"];
                            DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, SerialetKeq);
                            Container1.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=false&db=jo";
                        }

                        ImbLogger.LogErrorShitje($"Exception:{err}");
                        NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        gabimNeRuajtje(statusDokumenti, true);
                        return;
                    }
                    if (!mesazh.Status)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                        gabimNeRuajtje(statusDokumenti, true);
                        return;
                    }
                    mag.mbushKokaMagazinaSipasIDGjenerues(kokeShitje.IdShitjeKoka, 1, kokeShitje.IdKonfigAmbjente);
                }
                if (mag.IdKokaMagazina != 0 && String.Compare(hfKontrollRivleresim.Value, "true", true) == 0 && statusDokumenti != 0)
                    if (mag.rivleresim())
                    {
                        rivleresim = true;
                        tr.mbushGjitheTrupiMagazinaNgaKoka(mag.IdKokaMagazina);
                        trupat.AddRange(tr);
                    }
            }
            else if (shtimModifikim == "modifikim")
            {
                if ((statusDokumenti == 1 && !tedrejtaInfo.DMod) || (statusDokumenti == 0 && !tedrejtaInfo.DModifikimDraft))

                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", cultinf), pnlMesazhi);
                    gabimNeRuajtje(statusDokumenti, true);
                    return;
                }
                kokeShitje.IdShitjeKoka = idNgaQueryString;
                bool faturepermbledhes = false;
                clsKokaShitje kokaekzistuese = new clsKokaShitje();
                kokaekzistuese.mbushKokaShitjeSipasIDPaTrup(kokeShitje.IdShitjeKoka);
                faturepermbledhes = kokaekzistuese.FaturePermbledhese;
                if (statusAprovimi == StatusAprovimi.Undefined)
                    kokeShitje.StatusAprovimi = kokaekzistuese.StatusAprovimi;
                clsKokaMagazina mag = new clsKokaMagazina();

                int idKategoria = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(kokeShitje.IdNivel);
                if (String.Compare(hfKontrollRivleresim.Value, "true", true) == 0)
                {
                    mag.mbushKokaMagazinaSipasIDGjenerues(kokeShitje.IdShitjeKoka, idKategoria == 1 ? 2 : 1, kokeShitje.IdKonfigAmbjente);
                    if (mag.IdKokaMagazina != 0 && statusDokumenti != 0)
                        if (mag.rivleresim())
                        {
                            rivleresim = true;
                            tr.mbushGjitheTrupiMagazinaNgaKoka(mag.IdKokaMagazina);
                            trupat.AddRange(tr);
                        }
                }

                bool lidhur = kokeShitje.eshteILidhur();
                if (hfState.Get<bool>("MosModifikoTrup"))
                    lidhur = false;
                colGaranciArtikulli garanci = new colGaranciArtikulli(kokeShitje.IdShitjeKoka);

                if (!lidhur && garanci.Count > 0)
                    lidhur = true;
                if (faturepermbledhes && kokaekzistuese.IdStatusDok != 0) //faturat permbledhese me status draft vijne nga importi dhe duhet te riruhen normalisht.
                    lidhur = true;
                if (lidhur.ToString() != hfLidhur.Value)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDokumentiEshteILidhur", cultinf), pnlMesazhi);
                    gabimNeRuajtje(statusDokumenti, true);
                    return;
                }
                if (statusDokumenti != 1 && lblStatusAprovimi.Text != string.Empty && lblStatusAprovimi.Text != StatusAprovimi.Aprovuar.ToString())
                    lidhur = true;
                if (modifiko)
                    lidhur = false;
                if (hfState.Get<bool>("MosModifikoTrup"))
                    lidhur = false;
                if (tollona && DbCore.DbTollona.clsShitjeMeSerial.kaTollonaShitja(idNgaQueryString))
                {
                    hfLidhur.Value = "True";
                }
                if (tollonakastrati && DbCore.DbTollona.clsTollonaLeter.kaTollonaShitja(idNgaQueryString))
                {
                    hfLidhur.Value = "True";
                }
                DbCore.DbQendraKosto.clsKokaQendraKosto qend = new DbCore.DbQendraKosto.clsKokaQendraKosto();
                if (shtimModifikim == "modifikim")
                {
                    clsKokaFleteKontabel kok = new clsKokaFleteKontabel();
                    kok = new clsKokaFleteKontabel(idNgaQueryString, isShitje ? 1 : 2);
                    qend.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);
                }
                decimal tot1;
                Converter.Parse(txtTotal1.Text, out tot1, "txtTotal1");
                decimal vleraTotale = tot1 - (kokaekzistuese.IdStatusDok == 0 ? 0 : Convert.ToDecimal(kokaekzistuese.TotaliMeZbritjeMeTVSH)) + Convert.ToDecimal(clsKlientFurnitor.MerrDetyrimKf(kf.IdKlientFurnitor, kokeShitje.DtDok));

                if (statusDokumenti == 1 && clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "KKLKF") == "Po")
                {
                    if (kf.LimitBllokues > 0 && vleraTotale > kf.LimitBllokues)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgTotalFatureKaluarLimitBllokues", cultinf), pnlMesazhi);
                        gabimNeRuajtje(statusDokumenti, true);
                        return;
                    }
                }

                kokeShitje.DtKrijimiPajisje = kokaekzistuese.DtKrijimiPajisje;
                try
                {
                    string kodSoftueri = WebConfigurationManager.AppSettings["kodSoftueri"];
                    DbData dbData = new DbData();
                    if (txtIIC.Text == "" && nderm.Fiskalizimi)
                    {
                        txtIIC.Text = clsFunksioneFiskalizimi.GjeneroIIC(new clsNdermarrje(idNdermarrje), txtNumer.Text, txtTotal1.Text, "ur271so291", kodSoftueri, Convert.ToDateTime(kokeShitje.DtDok.ToString().Split(' ')[0] + ' ' + kokeShitje.DtKrijimiPajisje.ToString().Split(' ')[1]));
                        kokeShitje.IIC = txtIIC.Text;
                    }
                    //kokeShitje.IIC = txtIIC.Text;
                    mesazh = kokeShitje.modifikoSh(idGjuha, serverUrl, lidhur, hfNrAutoShitje, periudha.IdPeriudha, colkonvetimi, gjenerodokmag, idskema, statusAprovimi, idetapa, out shfaqmesazhapolupemagazina,
                        kokaMema, dergoemail, eshteOwn, dergoemailVfOne, serialemag, konfamortizimi, isShitje, faturashitjengaurdhershitjamekupontatimor, out printofature, out printogarancifature,
                        out shfaqmesazhapolupe, mekontabilizim, qend.ColTrupi, cmbModeli.Text, rm, cultinf, tollona, zevendesimtollona, tollonakastrati, tollonakastratielektronik, zevendesimtollonakastrati,
                        cbRenditje.Checked, kontrolloSasiKonvertimiDheKthimi, out veprimebanka, out shfaqmesazhapolupebanka, out shfaqmesazhapolupeVDK, out pageseFature, false, kontrolloIMEIFifo,
                        hfShtimModifikim.Value == "bli", !cmbModeli.Text.Contains("USHmag"), ref dbData, out mesazhmevonshem, false, "", "", "", "");
                    if (mesazh.Status)
                    {
                        var viti = new clsViti(periudha.IdViti).KodiViti;
                        if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                        {
                            if (nderm.Fiskalizimi && cbFiskalizo.Checked && statusDokumenti == 1 && veprimi == "shitje" && (shtimModifikim == "shtim" || shtimModifikim == "klonim" || shtimModifikim == "konvertim" || hfState.Get("idstatusdok").ToString() == "0" || shtimModifikim == "kthim" || shtimModifikim == "modifikim") && cmbNiveli.SelectedItem.GetFieldValue("Kodi").ToString() == "FSH")
                            {
                                clsKlientFurnitor kF = new clsKlientFurnitor(kokeShitje.IdKlientFurnitor);
                                var arka = new clsBanka();
                                if (btneArka.Text == "")
                                    arka = new clsBanka(kF.EmriBanka);
                                else
                                    arka.mbushBankeSipasKodit(btneArka.Text, idNdermarrje);
                                var dateMaturimi = dateMaturimi_DateEdit.Date.ToString().Split()[0];
                                clsKokaShitje kokeshitjeje = new clsKokaShitje(kokeShitje.IdShitjeKoka);
                                dateMaturimi = dateMaturimi.Replace('/', '-');
                                string[] dateMaturimiList = dateMaturimi.Split(new[] { '-' }, 3);
                                var dateMaturimiFormatuar = $"{dateMaturimiList[2]}-{dateMaturimiList[1]}-{dateMaturimiList[0]}";
                                string[] emriMbiemriOperatori = cmbOperatori.Text.Split(' ');
                                var operatori = clsOperator.MerrKodOperatoriSipasId(kokeShitje.IdOperator, nderm.IdNdermarrje);
                                var degeAdministrative = clsDegeAdministrative.ktheDegeAdministrativeSipasiD(kokeShitje.IdDegeAdministrative);
                                var iicSignature = clsFunksioneFiskalizimi.ktheIICSignature(nderm, txtNumer.Text, txtTotal1.Text, "ur271so291", kodSoftueri, kokeshitjeje.DtKrijimi);
                                if (txtNIVF.Text == "" && cbFiskalizo.Checked)
                                {
                                    var timeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
                                    var dtKrijimiOffset = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
                                    DateTimeOffset dtKrijimiPajisjeOffset = new DateTimeOffset(kokeshitjeje.DtKrijimiPajisje);
                                    if (txtIIC.Text == "")
                                        txtIIC.Text = clsFunksioneFiskalizimi.GjeneroIIC(new clsNdermarrje(idNdermarrje), txtNumer.Text, txtTotal1.Text, "ur271so291", kodSoftueri, Convert.ToDateTime(kokeshitjeje.DtDok.ToString().Split(' ')[0] + ' ' + kokeshitjeje.DtKrijimi.ToString().Split(' ')[1]));
                                    bool isEinvoice = cbEinvoice.Checked;
                                    if (new clsKlientFurnitor(kokeshitjeje.IdKlientFurnitor).TipiId != "NUIS")
                                        isEinvoice = false;
                                    var mesazhInvoice = clsFunksioneFiskalizimi.GjeneroMesazhInvoice(nderm, kokeShitje.NrDok, txtTotal1.Text, "ur271so291", kodSoftueri, kokeshitjeje.IIC, iicSignature,
                                           txtAdresaFaturimit.Text, txtPershkrimi.Text, txtTotal1.Text, dateMaturimiFormatuar, txtTotal1.Text, txtTVSH1.Text, txtPerqindje.Text, btnKlienti.Text.Split(' ')[0], txtNipt.Text, txtTotaliMeZbritjePaTVSH1.Text,
                                           txtQytetiK.Text, txtQytetiK.Text, "", "", kokeShitje.merrTrupShitje(), arka.KodiTCR, cmbMenyrePagese.Text, nderm, kf.KodKlientFurnitor, kokeShitje.Zbritje.ToString(), txtTotaliMeZbritjePaTVSH1.Text, txtNivfKthim.Text, Convert.ToDateTime(kokeshitjeje.DtDok.ToString().Split(' ')[0] + ' ' + dtKrijimiPajisjeOffset.UtcDateTime.ToString().Split(' ')[1]), cmbOperatori.Text, false, kokeShitje.Kursi.ToString(), isEinvoice, degeAdministrative["KODNJESIEBIZNES"].ToString(), cmbTipiIVetefaturimit.Text, operatori.ItemArray[0].ToString(), isShitje, true, kokeshitjeje.Dogana, kokeshitjeje.DtMbarimi.ToString(), kokeshitjeje.DtFillimi.ToString(), kokeshitjeje.DtDok.ToString(), dtKrijimiOffset, idNdermarrje, idPerdoruesi, viti);

                                    var nivfFature = clsFunksioneFiskalizimi.InvokeService(mesazhInvoice[0], "FIC", false);
                                    if (nivfFature[0] == "Buyer TIN doesn't exist in RTP." || nivfFature[0] == "Buyer is not active in the RTP.")
                                    {
                                        var objekti = ktheObjektPerNotify(kokeshitjeje, false, "Deshtim", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), nivfFature[0], nivfFature[1], "Fature Fiskalizimi", mesazhInvoice[1], nivfFature[2]);
                                        clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);
                                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Fatura nuk u fiskalizua:Klienti nuk eshte aktiv ne regjistrin e tatimeve", pnlMesazhi);

                                    }
                                    else if (nivfFature[0] == "Buyers TIN is not in the correct format.")
                                    {
                                        var objekti = ktheObjektPerNotify(kokeshitjeje, false, "Deshtim", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), nivfFature[0], nivfFature[1], "Fature Fiskalizimi", mesazhInvoice[1], nivfFature[2]);

                                        clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);
                                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Fatura nuk u fiskalizua: Formati i NIPT-it nuk eshte i rregullt", pnlMesazhi);
                                    }
                                    else if (nivfFature[1] != null)
                                    {
                                        var objekti = ktheObjektPerNotify(kokeshitjeje, false, "Deshtim", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), nivfFature[0], nivfFature[1], "Fature Fiskalizimi", mesazhInvoice[1], nivfFature[2]);

                                        clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);
                                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim me fiskalizimin, fatura nuk u fiskalizua!" + $"Error:{nivfFature[1]}" + $" Pershkrimi i errorit:{nivfFature[0]}", pnlMesazhi);
                                    }

                                    else
                                    {
                                        string[] EIC = new string[1];
                                        if (cbEinvoice.Checked && txtEIC.Text == "")
                                        {
                                            EIC = DergoEinvoice(kokeshitjeje, kf, nderm, iicSignature, nivfFature[0], operatori, viti);
                                            if (EIC[1] != null)
                                            {
                                                if (EIC[1] == "Fatura nuk u be Einvoice")
                                                {
                                                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Servisi i E-invoice nuk pergjigjet, ju lutem provoni perseri me vone!", pnlMesazhi);
                                                    var objektiEinvoice = ktheObjektPerNotify(kokeshitjeje, false, "Deshtim", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), "Error serveri e-invoice", EIC[1], "Fature E-invoice", EIC[3], "Problem serveri e-invoice");
                                                    clsFunksioneFiskalizimi.dergoWebhookNotify(objektiEinvoice, true);
                                                    EIC[0] = "";
                                                }
                                                else
                                                {
                                                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim me krijimin e fatures elektronike!" + $"Error:{EIC[1]}" + $" Pershkrimi i errorit:{EIC[0]}", pnlMesazhi);
                                                    var objektiEinvoice = ktheObjektPerNotify(kokeshitjeje, false, "Deshtim", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), EIC[0], EIC[1], "Fature E-invoice", EIC[3], EIC[2]);
                                                    clsFunksioneFiskalizimi.dergoWebhookNotify(objektiEinvoice, true);
                                                    EIC[0] = "";
                                                }

                                            }
                                            else
                                            {
                                                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Fatura elektronike u krijua me sukses!", pnlMesazhi);
                                                if (cbPrinto.Checked)
                                                {
                                                    var fatura = clsFunksioneFiskalizimi.merrEinvoice(nderm, EIC[0], DateTime.UtcNow);
                                                    var base64 = clsFunksioneFiskalizimi.InvokeService(fatura, "Pdf", true);
                                                    string b64string = base64[0];
                                                    blob.Value = JsonConvert.SerializeObject(new { b64 = b64string });
                                                }
                                                //var fatura = clsFunksioneFiskalizimi.merrEinvoice(nderm, EIC[0], DateTime.UtcNow);
                                                //var base64 = clsFunksioneFiskalizimi.InvokeService(fatura, "ns2:Pdf", true);
                                                //string b64string = base64[0];
                                                //blob.Value = JsonConvert.SerializeObject(new { b64 = b64string});
                                                //ketu e dim qe fatura ka shkuar me sukses te einvoice dhe kemi marr pergjigje suksesi, kshu qe mund te behet ktu kodi ku do hapet pdf
                                            }

                                        }
                                        var objektiEinvoiceSukses = new object();
                                        clsKokaShitje.shtoKodinNivfTeShitja(kokeShitje.IdShitjeKoka, nivfFature[0], nderm.IdNdermarrje, EIC[0], txtIIC.Text);
                                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Fatura u fiskalizua me sukses!", pnlMesazhi);
                                        var objekti = ktheObjektPerNotify(kokeshitjeje, false, "Sukses", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), nivfFature[0], nivfFature[1], "Fature Fiskalizimi", mesazhInvoice[1], nivfFature[2]);
                                        clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);
                                        if (cbEinvoice.Checked)
                                        {
                                            if (EIC[0] == "")
                                                EIC[0] = "";
                                            else
                                            {
                                                objektiEinvoiceSukses = ktheObjektPerNotify(kokeshitjeje, false, "Sukses", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), EIC[0], EIC[1], "Fature E-invoice", EIC[3], EIC[2]);
                                                clsFunksioneFiskalizimi.dergoWebhookNotify(objektiEinvoiceSukses, true);
                                            }
                                        }

                                    }

                                }
                                else if(cbEinvoice.Checked && txtNIVF.Text != "" && txtEIC.Text == "")
                                {
                                    string[] EIC = new string[1];
                                    if (cbEinvoice.Checked)
                                    {
                                        EIC = DergoEinvoice(kokeshitjeje, kf, nderm, iicSignature, kokeShitje.NIVF, operatori,viti);
                                        if (EIC[1] != null)
                                        {
                                            if (EIC[1] == "Fatura nuk u be Einvoice")
                                            {
                                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Servisi i E-invoice nuk pergjigjet, ju lutem provoni perseri me vone!", pnlMesazhi);
                                                var objektiEinvoice = ktheObjektPerNotify(kokeshitjeje, false, "Deshtim", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), "Error serveri e-invoice", EIC[1], "Fature E-invoice", EIC[3], "Problem serveri e-invoice");
                                                clsFunksioneFiskalizimi.dergoWebhookNotify(objektiEinvoice, true);
                                                EIC[0] = "";
                                            }
                                            else
                                            {
                                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim me krijimin e fatures elektronike!" + $"Error:{EIC[1]}" + $" Pershkrimi i errorit:{EIC[0]}", pnlMesazhi);
                                                var objektiEinvoice = ktheObjektPerNotify(kokeshitjeje, false, "Deshtim", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), EIC[0], EIC[1], "Fature E-invoice", EIC[3], EIC[2]);
                                                clsFunksioneFiskalizimi.dergoWebhookNotify(objektiEinvoice, true);
                                                EIC[0] = "";
                                            }
                                        }
                                        else
                                        {
                                            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Fatura elektronike u krijua me sukses!", pnlMesazhi);
                                            //ketu e dim qe fatura ka shkuar me sukses te einvoice dhe kemi marr pergjigje suksesi, kshu qe mund te behet ktu kodi ku do hapet pdf
                                            // mund te marrim pdf dhe ta ruajm ne nje nga keto fushat ate base64, ne js pastaj mund ta konvertojm ne blob
                                            // dhe ifi i printimit, nese eshte checkboxi te behet ky funx dmth e till, do behjet ne te gjitha ato m sipper,thnx, asgje naten diten, ika
                                            if (cbPrinto.Checked)
                                            {
                                                var fatura = clsFunksioneFiskalizimi.merrEinvoice(nderm, EIC[0], DateTime.UtcNow);
                                                var base64 = clsFunksioneFiskalizimi.InvokeService(fatura, "Pdf", true);
                                                string b64string = base64[0];
                                                blob.Value = JsonConvert.SerializeObject(new { b64 = b64string });
                                            }



                                        }
                                        var objektiEinvoiceSukses = new object();
                                        clsKokaShitje.shtoKodinNivfTeShitja(kokeShitje.IdShitjeKoka, kokeShitje.NIVF, nderm.IdNdermarrje, EIC[0], txtIIC.Text);
                                        if (cbEinvoice.Checked)
                                        {
                                            if (EIC[0] == "")
                                                EIC[0] = "";
                                            else
                                            {
                                                objektiEinvoiceSukses = ktheObjektPerNotify(kokeshitjeje, false, "Sukses", clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka), new clsTrupiShitje(), EIC[0], EIC[1], "Fature E-invoice", EIC[3], EIC[2]);
                                                clsFunksioneFiskalizimi.dergoWebhookNotify(objektiEinvoiceSukses, true);
                                            }
                                        }
                                    }
                                }


                            }

                        }
                        string objektiShtije = "";
                        if (isShitje)
                            objektiShtije = "Shitje";
                        else
                            objektiShtije = "Blerje";
                        clsKokaShitje kokaEshitjes = new clsKokaShitje(kokeShitje.IdShitjeKoka);
                        object[] koka = kokeShitje.krijoObjektPerWebhook(kokaEshitjes, "Modifikim", objektiShtije);
                        colTrupiShitje trupi = clsKokaShitje.merrTrupiShitje(kokeShitje.IdShitjeKoka);
                        var trupiShitje = trupi.ToList();
                        JObject[] json = new JObject[trupiShitje.Count];
                        int counter = 0;
                        foreach (var t in trupiShitje)
                        {

                            json[counter] = JObject.Parse(JsonConvert.SerializeObject(t));
                            json[counter].Add("kodbari", clsKodbari.ktheKodbarSipasIdArtikulliNjesiaKodbariIPare((string)json[counter].SelectToken("Kodi"), kokeShitje.IdNdermarrje));
                            counter++;
                        }
                        Object kokaDheTrupi;
                        kokaDheTrupi = new
                        {
                            meta = koka[0],
                            koka = koka[1],
                            trupi = json
                        };
                        hfObjektRuajtur.Value = JsonConvert.SerializeObject(kokaDheTrupi);
                        clsFunksione.dergoWebhookDatasetEndpoint(kokaDheTrupi);
                    }

                }
                catch (Exception err)
                {
                    ImbLogger.LogErrorShitje($"Exception:{err}");
                    NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    gabimNeRuajtje(statusDokumenti, true);
                    return;
                }
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    gabimNeRuajtje(statusDokumenti, true);
                    return;
                }
                if (String.Compare(hfKontrollRivleresim.Value, "true", true) == 0)
                {
                    mag.mbushKokaMagazinaSipasIDGjenerues(kokeShitje.IdShitjeKoka, idKategoria == 1 ? 2 : 1, kokeShitje.IdKonfigAmbjente);
                    if (mag.IdKokaMagazina != 0 && statusDokumenti != 0)
                        if (mag.rivleresimPas())
                        {
                            rivleresim = true;
                            tr.mbushGjitheTrupiMagazinaNgaKoka(mag.IdKokaMagazina);
                            trupat.AddRange(tr);
                        }
                }
            }
            else //as shtim as modifikim
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgGabimGjateRuajtjesSeDokumentit"), pnlMesazhi);
                gabimNeRuajtje(statusDokumenti, true);
                return;
            }
            if (mesazh)
                hfState.Set("MosModifikoTrup", false);
            DbCore.DbShare.clsAtributeTrupi atr = new DbCore.DbShare.clsAtributeTrupi();
            atr.mbushAtributSipasKompKonfDheKontrollit(idGjuha, kokeShitje.IdKonfigAmbjente, "cbGaranci", 506);
            if (atr.VlereDefault != "")
                cbGaranci.Checked = bool.Parse(atr.VlereDefault);

            hfKasa.Value = string.Empty;
            //Session.Add("trupat", trupat);
            mySessionObjects.ruajTrupatNeSession(Session, trupat);
            pergjigja.Text = "ruaj";
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti(kokeShitje.IdKonfigAmbjente);
            string pershkrim = konf.PershkrimKonfigAmbjente;
            if (mesazhmevonshem != "")
                clsMenuInfo.ShtoMesazhInformues(MenuInfo, mesazhmevonshem, pnlMesazhi);

            if ((pershkrim.Contains("Vodafone One") && kokeShitje.IdStatusDok == 0) || (pershkrim.Contains("Porosi") && (eshteOwn || nderm.Prind || (nderm.IdPrindi != null && nderm.IdPrindi != 0))))
            {
                Container55.Attributes["src"] = ""; Container1.Attributes["src"] = "";
            }
            else
            {
                if (printofature)
                {
                    if (String.IsNullOrEmpty(cmbFormatiPrintimit.Text))
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjShitjeMesazhSkaFormatPerPrintim", cultinf), pnlMesazhi);
                    else
                    {
                        int idRaporti = clsRaporti.KtheIdRaporti(idGjuha, Convert.ToInt32(cmbFormatiPrintimit.Value));
                        //raporti i garancise
                        if (faturashitjengaurdhershitjamekupontatimor != null)
                            Container55.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idRaporti + "&idDokumenti=" + faturashitjengaurdhershitjamekupontatimor.IdShitjeKoka + "&printo=true&raportdyte=jo&iddesign=" + cmbFormatiPrintimit.Value;
                    }
                }
                else if (printo)
                {
                    if (String.IsNullOrEmpty(cmbFormatiPrintimit.Text))
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjShitjeMesazhSkaFormatPerPrintim", cultinf), pnlMesazhi);
                    else
                    {
                        int idRaporti = clsRaporti.KtheIdRaporti(idGjuha, Convert.ToInt32(cmbFormatiPrintimit.Value));
                        //raporti i garancise

                        Container55.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idRaporti + "&idDokumenti=" + kokeShitje.IdShitjeKoka + "&printo=true&raportdyte=jo&iddesign=" + cmbFormatiPrintimit.Value;
                    }
                }
                if (printogarancifature)
                {
                    colGaranciArtikulli garanci = new colGaranciArtikulli(faturashitjengaurdhershitjamekupontatimor.IdShitjeKoka);
                    if (garanci.Count > 0)
                    {

                        int idrapgarancia = 142;
                        Container1.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idrapgarancia + "&idDokumenti=" + faturashitjengaurdhershitjamekupontatimor.IdShitjeKoka + "&printo=true&raportdyte=po";
                    }
                }
                else if (cbGaranci.Checked && kokeShitje.IdStatusDok == 1)
                {
                    colGaranciArtikulli garanci = new colGaranciArtikulli(kokeShitje.IdShitjeKoka);
                    if (garanci.Count > 0)
                    {

                        int idrapgarancia = 142;
                        Container1.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idrapgarancia + "&idDokumenti=" + kokeShitje.IdShitjeKoka + "&printo=true&raportdyte=po";
                    }
                }
                if (veprimebanka.Printo)
                {
                    Container1.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=mandatArketimPagese&idDokumenti=" + veprimebanka.IdKoka + "&printo=true&raportdyte=jo&iddesign=" + clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(veprimebanka.IdKonfigAmbjente, "cmbFormatiPrintimit", 301);

                }
            }
            if (mesazh.Status)
            {
                CacheLayer.GlobalCacheManager.MyPageCache["IdDokTransferimNga"] = 0;
                hfqkmesazhi.Value = (kokeShitje.Totali - kokeShitje.Zbritje == 0) ? "jo" : shfaqmesazhapolupe;
                hfqkmesazhimag.Value = (kokeShitje.Totali - kokeShitje.Zbritje == 0) ? "jo" : shfaqmesazhapolupemagazina;
                hfqkmesazhibanka.Value = (kokeShitje.Totali - kokeShitje.Zbritje == 0) ? "jo" : shfaqmesazhapolupebanka;
                hfqkmesazhiVDK.Value = (kokeShitje.Totali - kokeShitje.Zbritje == 0) ? "jo" : shfaqmesazhapolupeVDK;

                if (hfqkmesazhi.Value != "jo")
                {
                    clsKokaFleteKontabel kok;
                    if (isShitje) kok = new clsKokaFleteKontabel(kokeShitje.IdShitjeKoka, 1);
                    else kok = new clsKokaFleteKontabel(kokeShitje.IdShitjeKoka, 2);

                    hfUrl.Value = "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente;
                }
                if (hfqkmesazhimag.Value != "jo")
                {
                    clsKokaFleteKontabel kok = new clsKokaFleteKontabel(kokeShitje.OKokaMagazina.IdKokaMagazina, 6);
                    hfUrlmag.Value = "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente;
                }
                if (hfqkmesazhibanka.Value != "jo")
                {
                    clsKokaFleteKontabel kok = new clsKokaFleteKontabel(veprimebanka.IdKoka, 3);
                    if (kok.IdKokaFleteKontabel == 0 || kok.IdKokaFleteKontabel == -1)
                        kok = new clsKokaFleteKontabel(veprimebanka.IdKoka, 4);

                    if (kok.VleftaFleteKontabel != 0)
                        hfUrlbanka.Value = "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente;
                }
                if (hfqkmesazhiVDK.Value != "jo")
                {
                    foreach (clsDokumentLidhesKoka d in veprimebanka.ODokumentLidhes)
                    {
                        clsKokaFleteKontabel kok = new clsKokaFleteKontabel(d.IdKoka, 10);

                        if (kok.VleftaFleteKontabel != 0)
                            hfUrlVDK.Value += "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + ";";
                    }
                }

            }
            if (cbDergoMeEmail.Checked)
            {
                List<int> mags = new List<int>();
                if (dergoemailMag)
                    mags = kokeShitje.OColTrupiShitje.AsQueryable().Select(x => x.IdMagazina).ToList<int>();
                (clsMesazh gabim, clsMesazh sukses) = EmailComposer.dergoEmailFaturenNgaPerdoruesiLoguar((int)hfState["idGjuha"], cultinf, (int)hfState["idPerdoruesi"], (int)hfState["idViti"], (int)hfState["idNdermarrje"], kokeShitje.IdShitjeKoka, kokeShitje.IdKlientFurnitor, kokeShitje.NrDok, kokeShitje.DtDok, kokeShitje.IdRaportDesing, kokeShitje.IdKonfigAmbjente, mags.ToArray(), dergoemailMag);
                if (gabim.PershkrimMesazhi != "")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, gabim.PershkrimMesazhi, pnlMesazhi);
                if (sukses.PershkrimMesazhi != "")
                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, sukses.PershkrimMesazhi, pnlMesazhi);
            }

            string printDraftNeKase = string.Empty;
            if (kokeShitje.IdStatusDok == 0)
                printDraftNeKase = clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "PKDD");
            if (kase && isShitje && (kokeShitje.IdStatusDok == 1 || printDraftNeKase == "Po") && kokeShitje.PerqindjeZbritje < 100)
            {
                string stream = "";
                var mesazhkasa = kokeShitje.PrintoNeKase(faturashitjengaurdhershitjamekupontatimor, idPerdoruesi, idNdermarrje, kf, ref stream, DbCore.mySessionObjects.merrIPKasaNgaWebServisi(Session), Convert.ToInt32(cmbKonfigurimKase.Value), txtPaguar.Text);

                if (mesazhkasa.Item1.Status && stream != "")
                    hfKasa.Value = stream;
                else if (mesazhkasa.Item1.Status)
                {
                    if (mesazhkasa.Item2 != null)
                    {
                        hfKasaNew.Value = mesazhkasa.Item2;
                    }
                    else
                        clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgFaturaDerguaKaseFiskaleSukses", cultinf), pnlMesazhi);
                    if (mesazhkasa.Item3 != null && mesazhkasa.Item3.Status)
                        clsMenuInfo.ShtoMesazhInformues(MenuInfo, MessagesResource.Messages["msgFshiPLUInfo"], pnlMesazhi);
                }
                else
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhkasa.Item1.PershkrimMesazhi, pnlMesazhi);

            }

            if (rivleresim)
                clsMenuInfo.ShtoPyetje(MenuInfo, mesazhinformues + rm.GetString("regjMagVeprimiSjellNdryshimNeCmimDalje", cultinf), pnlMesazhi, idGjuha);

            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, mesazh.PershkrimMesazhi + mesazhinformues, pnlMesazhi);
            List<Int32> id = new List<int>();
            id.Add(pageseFature ? faturashitjengaurdhershitjamekupontatimor.IdShitjeKoka : kokeShitje.IdShitjeKoka);
            if (kokeShitje.IdKlientFurnitor != 0)
            {
                DbCore.DbArkaBanka.clsBanka banka = new DbCore.DbArkaBanka.clsBanka();
                banka.mbushBanke(kf.EmriBanka);
                arkabanka = banka.LlojArkaBanka ? "banka" : "arka";
            }

            int idKat = veprimi == "shitje" ? 1 : 2;
            double vleraMbetur = DbCore.DbRegjistrim.clsKokaShitje.ktheVlereMbeturPerDok(kokeShitje.IdShitjeKoka, idNdermarrje, idKat);
            status1.Value = "true";
            if ((cmbMenyrePagese.Text == "Pagese" || pageseFature) && kokeShitje.IdStatusDok != 0 && vleraMbetur != 0)
                status1.Value = "pagese";
            hfShtimModifikim.Value = "shtim";
            hl = new HtmlTable();
            pnlLidhur.Update();
            konfiguroVleraFillestareShto(idGjuha, veprimi, idNdermarrje, idPerdoruesi, shtimModifikim);// pnlNiveli.Update();
            PastroFusha();

            string serializeid = JsonConvert.SerializeObject(id);
            string url = string.Empty;
            bool isVleraTotalPozitive = kokeShitje.Totali > 0;
            string llojveprimi = DbCore.DbArkaBanka.clsVeprimBankaKoka.llojVeprimi(isShitje, arkabanka, kokeShitje.Totali);
            if (isShitje && arkabanka == "arka")
                url = "ShtoVeprimBanka.aspx?lloji=" + llojveprimi.ToLower() + "&shtim_modifikim=shtim&id=0&vjenNga=Shto_RegjistrimDokumentash&idfatura=" + serializeid;
            else
                if (isShitje && arkabanka == "banka")
                url = "ShtoVeprimBanka.aspx?lloji=" + llojveprimi.ToLower() + "&shtim_modifikim=shtim&id=0&vjenNga=Shto_RegjistrimDokumentash&idfatura=" + serializeid;
            else
                    if (veprimi == "blerje" && arkabanka == "arka")
                url = "ShtoVeprimBanka.aspx?lloji=" + llojveprimi.ToLower() + "&shtim_modifikim=shtim&id=0&vjenNga=Shto_RegjistrimDokumentash&idfatura=" + serializeid;
            else
                        if (veprimi == "blerje" && arkabanka == "banka")
                url = "ShtoVeprimBanka.aspx?lloji=" + llojveprimi.ToLower() + "&shtim_modifikim=shtim&id=0&vjenNga=Shto_RegjistrimDokumentash&idfatura=" + serializeid;
            hfId.Value = url;
            if (Request.QueryString["kthehu"] != null)  // etapa e hapur
            {
                status1.Value = "kthehu";
                hfId.Value = "Shto_RegjistrimDokumentash.aspx?shitje_blerje=" + veprimi + "&shtim_modifikim=shtim";
            }
            ASPxMenu1.Items.FindByName("Ruaj").ClientEnabled = true;
            ASPxMenu1.Items.FindByName("Draft").ClientEnabled = true;
            visibleMenu(idPerdoruesi, veprimi, idNgaQueryString, hfShtimModifikim.Value);
            myWatchRegjistrimShitje.Stop();
            System.Diagnostics.Trace.WriteLine("myWatchRegjistrimShitje: " + myWatchRegjistrimShitje.Elapsed);
            ImbLogger.LogTraceShitje($"Mbaroi metoda ruajRegjistrim per dokumentin {txtNumer.Text} me parametra idGjuha:{idGjuha}, idNdermarje:{idNdermarrje}, idPerdoruesi:{idPerdoruesi}, idNdermarrjeVit:{idNdermarrjeVit}, veprimi:" + veprimi + $", statusDokumenti:{statusDokumenti}, printo:{printo}, StatusAprovimi:{statusAprovimi}, kase:{kase}, modifiko:{modifiko}, kontrolloSasi:{kontrolloSasi}, kontrollokonvertim:{kontrollokonvertim}, CultureInfo:{cultinf}, ResourceManager:{rm}, shtimModifikim:" + shtimModifikim + $", periudha:{periudha}, periudha:{JsonConvert.SerializeObject(periudha)}, fazat:{fazat}");
        }

        private void gabimNeRuajtje(int statusDokumenti, bool vendosStatusFalse)
        {
            if (vendosStatusFalse)
                status1.Value = "false";
            clsFunksione.removeLastDocFromCache(Session.SessionID);
            ASPxMenu1.Items.FindByName("Ruaj").ClientEnabled = true;
            ASPxMenu1.Items.FindByName("Draft").ClientEnabled = true;
        }

        private clsMesazh refuzoDraft(int idShitjeKoka, string shenime2)
        {
            return clsKokaShitje.modifikoStatusdokNeRefuzuar(idShitjeKoka, shenime2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private colKonvertimi merrIdDokKonvertuar()
        {
            ImbLogger.LogTraceShitje("Filloi metoda merrIdDokKonvertuar");
            colKonvertimi col = new colKonvertimi();

            object[][] dokumenti = JsonConvert.DeserializeObject<object[][]>(hfKonverto.Value);
            for (int i = 0; i < dokumenti.Length; i++)
            {
                clsKonvertimi konv = new clsKonvertimi();
                konv.IdDokKonvertuar = Convert.ToInt32(((object[])dokumenti[i])[0]);
                //clsNivelRegjistrimi niv = new clsNivelRegjistrimi();
                //niv.mbushNivelRegjistrimiSipasIdPaKonvertime(Convert.ToInt32(((object[])dokumenti[i])[3]));
                int idKategoria = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(Convert.ToInt32(((object[])dokumenti[i])[3]));
                if (idKategoria == 1 || idKategoria == 2)
                {
                    //    clsKokaShitje koka = new clsKokaShitje();
                    //    koka.mbushKokaShitjeSipasIDPaTrup(konv.IdDokKonvertuar);
                    konv.IdKonfigAmbjenteKonvertuar = clsKokaShitje.ktheIdKonfigAmbjente(konv.IdDokKonvertuar);//koka.IdKonfigAmbjente;
                }
                else if (idKategoria == 6)
                {
                    clsKokaMagazina mag = new clsKokaMagazina();
                    mag.mbushKokaMagazinaSipasID(konv.IdDokKonvertuar);
                    konv.IdKonfigAmbjenteKonvertuar = mag.IdKonfigAmbjente;
                }
                col.Add(konv);
            }
            ImbLogger.LogTraceShitje("Mbaroi metoda merrIdDokKonvertuar");
            return col;
        }

        private void mbushComboBoxKartat(int idKarta)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda mbushComboBoxKartat me parameter idKarta:{idKarta}");
            int idKlient = 0;
            if ((bool)hfState["LKVK"] && btnKlienti.Value != null)
                idKlient = Convert.ToInt32(btnKlienti.Value);

            cmbKarta.DataSource = clsKarta.MerrKarteSipasIdDheKlientitDt(idKarta, idKlient);
            ConfigureAspxComboBox.ShtoKolonaPerKartaKlienti(cmbKarta);
            cmbKarta.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cmbKarta.DropDownStyle = DropDownStyle.DropDown;
            cmbKarta.DataBind();
            ImbLogger.LogTraceShitje($"Mbaroi metoda mbushComboBoxKartat me parameter idKarta:{idKarta}");
        }

        private void mbushComboBoxFazat(colFazaKontrate col)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda mbushComboBoxFazat me parameter col:{JsonConvert.SerializeObject(col)}");
            cmbFaza.DataSource = col;
            cmbFaza.TextField = "Pershkrimi";
            cmbFaza.ValueField = "IdFaza";
            cmbFaza.IncrementalFilteringMode = IncrementalFilteringMode.Contains;
            cmbFaza.DropDownStyle = DropDownStyle.DropDown;
            cmbFaza.DataBind();
            ImbLogger.LogTraceShitje($"Mbaroi metoda mbushComboBoxFazat me parameter col:{JsonConvert.SerializeObject(col)}");
        }

        //protected void cmbKarta_Callback(object source, DevExpress.Web.CallbackEventArgsBase e)
        //{  
        //    int idNdermarrje = (int)hfState["idNdermarrje"];
        //   mbushComboBoxKartat(idNdermarrje);

        //}


        /// <summary>
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsKokaShitje
        /// </summary>
        /// <param name="veprimi"></param>
        /// <param name="gjeneroDokMag"></param>
        /// <param name="idNderViti"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <param name="klienti">Klienti</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaShitje</returns>
        /// <param name="ci"></param>
        /// <param name="rm"></param>
        /// <param name="shtimModifikim"></param>
        /// <param name="idEtapa"></param>
        private clsKokaShitje krijoRegjistrim(string veprimi, ref bool gjeneroDokMag, int idNderViti, int idPerdoruesi, int idNdermarrje, int statusDokumenti, int klienti, string kodklienti, clsKonfigurimAmbjenti KonfigAmbjente, clsKonfigurimAmbjenti konfmag, StatusAprovimi statusAprovimi, out string shfaqmesazhapolupe, out string mesazhinformues, clsKokaShitje kokamema, bool kasa, DbCore.DbAsete.colSerialetMagazine colserialemag, bool kontrolloSasi, clsKokaShitje faturashitjengaurdhershitjamekupontatimor, bool tollon, bool autoshitje, CultureInfo ci, ResourceManager rm, string shtimModifikim, clsPeriudhaKontabel periudha, bool meKontabilizim, bool eshteOwn, int idEtapa, bool tollonakastrati, bool tollonakastratielektronik, object tmp, string llojZevendesimi, bool zevendesimtollonakastrati, bool blerengadealer, bool krijoartri, int idGjuha, colSerialeUnikeMagazina serialetUnike)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda krijoRegjistrim me parametra veprimi:{veprimi}, gjeneroDokMag:{gjeneroDokMag}, idNderViti:{idNderViti}, idPerdoruesi:{idPerdoruesi}, idNdermarrje:{idNdermarrje}, statusDokumenti:{statusDokumenti}, klienti:{klienti}, kodklienti:{kodklienti}, KonfigAmbjente:{JsonConvert.SerializeObject(KonfigAmbjente)}, konfmag:{JsonConvert.SerializeObject(konfmag)}, kokamema:{JsonConvert.SerializeObject(kokamema)}, kasa:{kasa}, kontrolloSasi:{kontrolloSasi}, faturashitjengaurdhershitjamekupontatimor:{JsonConvert.SerializeObject(faturashitjengaurdhershitjamekupontatimor)}, tollon:{tollon}, autoshitje:{autoshitje}, shtimModifikim:{shtimModifikim}, periudha:{JsonConvert.SerializeObject(periudha)}, meKontabilizim:{meKontabilizim}, eshteOwn:{eshteOwn}, idEtapa:{idEtapa}, tollonakastrati:{tollonakastrati}, tollonakastratielektronik:{tollonakastratielektronik}, llojZevendesimi:{llojZevendesimi}, zevendesimtollonakastrati:{zevendesimtollonakastrati}, blerengadealer:{blerengadealer}, krijoartri:{krijoartri}, idGjuha:{idGjuha}, serialetUnike:{serialetUnike?.Count}");
            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //NrAuto.vendosVleratNrAuto(hfNrAuto, this, null, ASPxSplitter1, null);
            clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
            int menyretransporti = 0, kushtedergimi = 0, idagjenti = 0, kushtepagese = 0, degeadministrative = 0, pikeshitje = 0, idgrup = 0, idgrup2 = 0, idgrup3 = 0, idAgjenti2 = 0, idAgjenti3 = 0, idTransportues = 0, idArka = 0, idKarta = 0, pike = 0, idMuajRaportimi = 0, idVitRaportimi = 0;
            var colKlienteFurnitoreVartes = new colKlienteFurnitore();
            if (btnMenyreTransporti.Text != string.Empty)
                menyretransporti = int.Parse(btnMenyreTransporti.Value.ToString());
            if (btnKushtDergimi.Text != string.Empty)
                kushtedergimi = int.Parse(btnKushtDergimi.Value.ToString());
            if (btnAgjenti.Text != string.Empty)
            {
                idagjenti = clsAgjentShitje.ktheIdAgjentShitje(btnAgjenti.Text, idNdermarrje);
            }
            if (btnAgjenti2.Text != string.Empty)
            {
                idAgjenti2 = clsAgjentShitje.ktheIdAgjentShitje(btnAgjenti2.Text, idNdermarrje);
            }
            if (btnAgjenti3.Text != string.Empty)
            {
                idAgjenti3 = clsAgjentShitje.ktheIdAgjentShitje(btnAgjenti3.Text, idNdermarrje);
            }
            if (btneArka.Text != string.Empty)
            {
                idArka = DbCore.DbArkaBanka.clsBanka.ktheIdBanka(btneArka.Text, idNdermarrje);
            }

            if (hfState.Contains("colKlienteFurnitoreVartes") && hfState.Get("colKlienteFurnitoreVartes").ToString() != string.Empty)
            {
                colKlienteFurnitoreVartes = JsonConvert.DeserializeObject<colKlienteFurnitore>(hfState.Get("colKlienteFurnitoreVartes").ToString());
            }

            if (nderm.Fiskalizimi && statusDokumenti == 1 && veprimi == "shitje" && (shtimModifikim == "shtim" || shtimModifikim == "konvertim" || hfState.Get("idstatusdok").ToString() == "0") && cmbNiveli.SelectedItem.GetFieldValue("Kodi").ToString() == "FSH")

            {
                string kodSoftueri = WebConfigurationManager.AppSettings["kodSoftueri"];

            }
            if (btnKushtPagese.Text != string.Empty)
                kushtepagese = int.Parse(btnKushtPagese.Value.ToString());
            DateTime dttransporti = DateTime.Today;
            if (dateTransportimi_DateEdit.Text != string.Empty)
                dttransporti = dateTransportimi_DateEdit.Date;
            DateTime dtfillimi = DateTime.Today;
            if (DtFillimi_DateEdit.Text != string.Empty)
                dtfillimi = DtFillimi_DateEdit.Date;

            DateTime dtMaturimi = dateMaturimi_DateEdit.Date;
            if (dateMaturimi_DateEdit.Text == string.Empty)
                dtMaturimi = data_DateEdit.Date;

            DateTime dtmbarimi = DateTime.Today;
            if (DtMbarimi_DateEdit.Text != string.Empty)
                dtmbarimi = DtMbarimi_DateEdit.Date;
            DateTime dtfature = DateTime.Today;
            if (DtFature_DateEdit.Text != string.Empty)
                dtfature = DtFature_DateEdit.Date;

            if (cmbDegeAdministrative.Text != string.Empty && cmbDegeAdministrative.Text != " ()")
                degeadministrative = int.Parse(cmbDegeAdministrative.Value.ToString());
            if (cmbPikeShitjeFurnizimi.Text != string.Empty && cmbPikeShitjeFurnizimi.Text != " ()")
                pikeshitje = int.Parse(cmbPikeShitjeFurnizimi.Value.ToString());
            if (cmbGrup1.Text != string.Empty)
                idgrup = int.Parse(cmbGrup1.Value.ToString());
            if (cmbGrup2.Text != string.Empty)
                idgrup2 = int.Parse(cmbGrup2.Value.ToString());
            if (cmbGrup3.Text != string.Empty)
                idgrup3 = int.Parse(cmbGrup3.Value.ToString());

            hfNrAutoShitje = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNumer", "NrDok");
            hfNrAutoShitje = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNumerProjekti", "NrProjekt");
            hfNrAutoShitje = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNumerSerial", "NrSerial");
            hfNrAutoShitje = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDokMagazine", "NrDokMagazine");


            if (nderm.Fiskalizimi && cbFiskalizo.Checked && statusDokumenti == 1 && (veprimi == "shitje" || veprimi == "blerje") && (shtimModifikim == "shtim" || shtimModifikim == "klonim" || shtimModifikim == "konvertim" || hfState.Get("idstatusdok").ToString() == "0" || shtimModifikim == "kthim") && (cmbNiveli.SelectedItem.GetFieldValue("Kodi").ToString() == "FSH" || cmbNiveli.SelectedItem.GetFieldValue("Kodi").ToString() == "FB") || (shtimModifikim == "modifikim" && txtEIC.Text == "" && cbEinvoice.Checked))
            {
                if (veprimi == "blerje" && cmbTipiIVetefaturimit.Text == "")
                    throw new Exception("Plotesoni fushen Tipi i vetefaturimit!");
                string kodSoftueri = WebConfigurationManager.AppSettings["kodSoftueri"];
                if ((cbEinvoice.Checked && shtimModifikim == "modifikim" && txtNIVF.Text != "") || (cbFiskalizo.Checked && shtimModifikim == "modifikim" && txtIIC.Text != ""))
                    txtIIC.Text = txtIIC.Text;
                else
                {
                    if (txtIIC.Text == "")
                    {
                        var txtIICS = clsFunksione.GjeneroIIC(new clsNdermarrje(idNdermarrje), txtNumer.Text, txtTotal1.Text, "ur271so291", kodSoftueri);
                        if (txtIICS == "Ju lutem ngarkoni filen e passwordit!")
                        {
                            throw new Exception("Ju lutem ngarkoni filen e passwordit!");
                        }
                        else if (txtIICS == "Ju lutem ngarkoni certifikaten e sigurise!")
                        {
                            throw new Exception("Ju lutem ngarkoni certifikaten e sigurise!");

                        }
                    }
                }
                clsKlientFurnitor kF = new clsKlientFurnitor();
                kF.mbushKlientFurnitorSipasKodit(kodklienti, nderm.IdNdermarrje);
                if (kF.TipiId != "NUIS" && cbEinvoice.Checked && cbFiskalizo.Checked)
                    throw new Exception("Nuk mund te beni fature einvoice me klient qe nuk ka tip id NUIS!");
                var arka = new clsBanka();
                if (btneArka.Text == "" && kF.EmriBanka == 1 && txtNIVF.Text == "")
                    throw new Exception("Arka e zgjedhur nuk ka kodin TCR!");
                if (btneArka.Text == "")
                    arka = new clsBanka(kF.EmriBanka);
                else
                    arka.mbushBankeSipasKodit(btneArka.Text, idNdermarrje);
                if (arka.KodiTCR == "" || arka.KodiTCR == null && txtNIVF.Text == "")
                {
                    throw new Exception("Arka e zgjedhur nuk ka kodin TCR!");
                }
                if (cmbMenyrePagese.Text == "Pagese Automatike" || cmbMenyrePagese.Text == "Arke")
                {
                    if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizimV6())
                    {
                        DateTime ditaSot = dtfature;
                        var gjendjeArkeDitore = clsGjendjeArkeDitore.merrGjendjeDitoreSipasIdArkeDheDitesSot(arka.IdBanka, ditaSot);
                        if (!gjendjeArkeDitore)
                        {
                            throw new Exception("Regjistroni me pare balancen ditore te arkes!");
                        }
                    }

                }
                DataTable error = new DataTable();
                error.Columns.Add("Kodi");
                error.Columns.Add("Gabimi");
                error.Columns.Add("Rreshti");
                if (cmbDegeAdministrative.Text == "" && cbFiskalizo.Checked && txtNIVF.Text == "")
                {
                    error.Rows.Add("Dega administrative", "Plotesoni degen administrative!");
                }
                string[] kodiDegaAdministrative = cmbDegeAdministrative.Text.Split(' ');
                if (new clsDegeAdministrative(kodiDegaAdministrative[0], idNdermarrje).KodNjesieBiznesi == "" && txtNIVF.Text == "")
                {
                    error.Rows.Add("Dega administrative", "Plotesoni Kodin e njesise se biznesit te dega administrative!");
                }
                if (kF.TipiId != "" || kF.AutoNgarkese == true && txtNIVF.Text == "")
                {
                    if (kF.NiptiKF == "")
                        error.Rows.Add("Nipt klienti", "Vendosni Nipt-in e klientit per fiskalizimin!");
                    if (kF.OColAdresat[0].Adresa == "")
                        error.Rows.Add("Adresa klienti", "Vendosni adresen e klientit per fiskalizimin!");
                }
                if (kF.ShtetiKF == "" && cbEinvoice.Checked && txtNIVF.Text == "")
                    error.Rows.Add("Shteti klienti", "Vendosni shtetin e klientit per fiskalizimin!");
                if (nderm.NdermarrjeNipt == "" && txtNIVF.Text == "")
                    error.Rows.Add("Nipt ndermarrje", "Vendosni Nipt-in e ndermarrjes per fiskalizimin!");
                if (txtNumer.Text.StartsWith("0") && txtNIVF.Text == "")
                    error.Rows.Add("Numer fature", "Numri i fatures nuk mund te filloj me 0!");
                if (cmbOperatori.Text == "" && txtNIVF.Text == "")
                    error.Rows.Add("Operatori", "Operatori duhet zgjedhur per fiskalizimin!!");
                if (txtNumer.Text.Any(Char.IsLetter) && txtNIVF.Text == "")
                    error.Rows.Add("Numer fature", "Numri i fatures nuk duhet te permbaje shkronja per fiskalizimin!");
                DbCore.clsMesazh mesazherror = new DbCore.clsMesazh();
                clsKokaErrorImporti kokaErr = new clsKokaErrorImporti();
                if (error.Rows.Count > 0)
                {
                    kokaErr = new clsKokaErrorImporti(0, "Nga Fiskalizimi ", 1, nderm.IdNdermarrje, nderm.IdPerdoruesi);
                    kokaErr.ColTrupi.mbushErrorImportiNgaProgrami(error);
                    mesazherror = kokaErr.ruajErrorImporti();
                    DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, error);
                    Container1.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=false&db=jo";
                    throw new Exception("Ju lutem plotesoni fushat e kerkuara ne listen e gabimeve!");
                }

            }
            double perqindjeZbritje;
            double.TryParse(txtPerqindje.Text, out perqindjeZbritje);
            double kursi;
            double.TryParse(txtKursi.Text, out kursi);
            bool eshteMemme = (bool)hfState["Meme"];
            //SpeedTest st = new SpeedTest("PlatinumWeb", "Shto_RegjistrimDokumentash");
            //st.SpeedTestMethod("ruajTrupShitje", new object[] { idPerdoruesi, veprimi, idNdermarrje, KonfigAmbjente.IdKonfigAmbjente, tollon, gridDataObject.Value, shtimModifikim, gjeneroDokMag, eshteOwn, cmbGrup1.Text, btnMagazina, eshteMemme, hfSeriale, hfIdGride, perqindjeZbritje, colserialemag, kontrolloSasi, kursi, statusDokumenti, konfmag, ci, rm }, 10);
            //st.startTest();
            bool shitjevodafone = btnMagazina.Text.Length == 3 && KonfigAmbjente.KodKonfigAmbjente.EndsWith(btnMagazina.Text);
            colTrupiShitje trupi = ruajTrupShitje(idPerdoruesi, veprimi, idNdermarrje, KonfigAmbjente.IdKonfigAmbjente, tollon, gridDataObject.Value, gridObjectKomision.Value, shtimModifikim, gjeneroDokMag, eshteOwn, cmbGrup1.Text, btnMagazina, eshteMemme, hfSeriale, hfIdGride, perqindjeZbritje, colserialemag, kontrolloSasi, kursi, statusDokumenti, konfmag, ci, rm, tollonakastrati, zevendesimtollonakastrati, blerengadealer, shitjevodafone);
            //krijoSeriale(colserialemag, trupi, statusDokumenti, idNdermarrje, idPerdoruesi, idPerdoruesi, konfmag, kontrolloSasi, ci, rm, perqindjeZbritje);
            int idmagazina = 0;
            if (btnMagazina.Text != string.Empty)
                idmagazina = int.Parse(btnMagazina.Value.ToString());
            if (trupi.Count == 0)
            {
                ImbLogger.LogErrorShitje("msgTrupiDokNukDuhetBosh");
                throw new Exception(rm.GetString("msgTrupiDokNukDuhetBosh", ci));
            }
            if (statusAprovimi == StatusAprovimi.Deleguar)
            {
                clsEtapeAprovimi etapa = new clsEtapeAprovimi(idEtapa, KonfigAmbjente.IdKategori);
                if (etapa.LlojAprovuesi == 2)
                {
                    colRolPerdorues col = new colRolPerdorues();
                    col.mbushRolePerdoruesSipasRoli(etapa.IdAprovuesi);
                    if (col.Count > 0)
                        idPerdoruesi = col[0].IdPerdorues;
                }
            }

            double perqindjeAgjent = 0;
            if (txtPerqindjeAgjent.Text != string.Empty)
            {
                bool perqindja = double.TryParse(txtPerqindjeAgjent.Text, out perqindjeAgjent);
                if (!perqindja)
                {
                    ImbLogger.LogErrorShitje("msgPerqindjaEAgjentitNukEshteESakte");
                    throw new MyException(rm.GetString("msgPerqindjaEAgjentitNukEshteESakte", ci));
                }
            }
            double perqindjeAgjent2 = 0;
            if (txtPerqindjeAgjent2.Text != string.Empty)
            {
                bool perqindja = double.TryParse(txtPerqindjeAgjent2.Text, out perqindjeAgjent2);
                if (!perqindja)
                {
                    ImbLogger.LogErrorShitje("msgPeqindjaEAgjentitTeDyteNukEshteESakte");
                    throw new MyException(rm.GetString("msgPeqindjaEAgjentitTeDyteNukEshteESakte", ci));
                }
            }
            double perqindjeAgjent3 = 0;
            if (txtPerqindjeAgjent3.Text != string.Empty)
            {
                bool perqindja = double.TryParse(txtPerqindjeAgjent3.Text, out perqindjeAgjent3);
                if (!perqindja)
                {
                    ImbLogger.LogErrorShitje("msgPeqindjaEAgjentitTeTreteNukEshteESakte");
                    throw new MyException(rm.GetString("msgPeqindjaEAgjentitTeTreteNukEshteESakte", ci));
                }
            }
            double kilometraAtuo = 0;
            if (txtKilometra.Text != string.Empty)
            {
                bool km = double.TryParse(txtKilometra.Text, out kilometraAtuo);
                if (!km)
                {
                    ImbLogger.LogErrorShitje("msgKilometratNukJaneTeSakta");
                    throw new MyException(rm.GetString("msgKilometratNukJaneTeSakta"));
                }
            }

            DbCore.DbQendraKosto.clsKokaQendraKosto qend = new DbCore.DbQendraKosto.clsKokaQendraKosto();
            clsKokaRezervime rez = new clsKokaRezervime();
            if (shtimModifikim == "modifikim")
            {
                clsKokaFleteKontabel kok = new clsKokaFleteKontabel();
                clsKokaMagazina kokaEkzistueseMag = new clsKokaMagazina();
                if (veprimi == "shitje" || veprimi == "shitjediscount" || veprimi == "bazaar")
                {
                    kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 1);
                    kokaEkzistueseMag.mbushKokaMagazinaSipasIDGjenerues(int.Parse(Request.QueryString["id"]), 2, KonfigAmbjente.IdKonfigAmbjente);
                }
                else
                {
                    kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 2);
                    kokaEkzistueseMag.mbushKokaMagazinaSipasIDGjenerues(int.Parse(Request.QueryString["id"]), 1, KonfigAmbjente.IdKonfigAmbjente);
                }
                qend.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);
                rez.mbushKokaRezervimiSipasIDGjenerues(kokaEkzistueseMag.IdKokaMagazina, 2, kokaEkzistueseMag.IdKonfigAmbjente);
            }

            bool gjeneromeme = clsAlternativaKushti.getAlternativa(KonfigAmbjente.IdKonfigAmbjente, "GJOSHM") == "Po";
            bool gjenerobij = clsAlternativaKushti.getAlternativa(KonfigAmbjente.IdKonfigAmbjente, "GJUBB") == "Po" || clsAlternativaKushti.getAlternativa(KonfigAmbjente.IdKonfigAmbjente, "GJFBB") == "Po";

            int idAutomjet = 0;
            string targa = string.Empty;
            if (btneAutomjeti.Text != string.Empty)
            {
                //clsAutomjete auto = new clsAutomjete();
                idAutomjet = clsAutomjete.ktheidAutomjetSipasShasise(btneAutomjeti.Text, idNdermarrje);
                targa = txtTarga.Text;
            }
            string emertimi = string.Empty;
            if (btnTransportues.Value != null && btnTransportues.Text != string.Empty)
            {
                idTransportues = int.Parse(btnTransportues.Value.ToString());
                emertimi = btnTransportues.Text;
            }
            if (cmbKarta.Text != string.Empty && cmbKarta.Value != null)
            {
                int.TryParse(cmbKarta.Value.ToString(), out idKarta);
            }
            int.TryParse(txtPike.Text, out pike);
            if (cmbMuajRaportimi.Value != null)
                idMuajRaportimi = Convert.ToInt32(cmbMuajRaportimi.Value);
            if (cmbVitRaportimi.Value != null)
                idVitRaportimi = Convert.ToInt32(cmbVitRaportimi.Value);
            clsKokaShitje koka = new clsKokaShitje();

            colFazaKontrate ocolFaza = (colFazaKontrate)tmp ?? null;
            int idFaza = 0;
            if (cmbFaza.Text != string.Empty && cmbFaza.Value != null)
            {
                int.TryParse(cmbFaza.Value.ToString(), out idFaza);
            }

            int idFormatPrintimi = 0;
            if (!String.IsNullOrEmpty(cmbFormatiPrintimit.Text))
            {
                bool parse = int.TryParse(cmbFormatiPrintimit.Value.ToString(), out idFormatPrintimi);
                if (!parse)
                {
                    ImbLogger.LogErrorShitje("Formati i printimit nuk ekziston");
                    throw new MyException("Formati i printimit nuk ekziston");
                }
            }
            int idkategoriseriali = 0;
            var kategoria = mySessionObjects.MerrNgaSession<clsSerialeUnikeKategori>(Session, "kategoriId");
            if (kategoria != null)
                idkategoriseriali = kategoria.ID;

            if (cmbMenyrePagese.Value == null)
            {
                ImbLogger.LogErrorShitje("Zgjidhni menyren e pageses!");
                throw new MyException("Zgjidhni menyren e pageses!");
            }
            bool zbritjeNeVlere = cmbLlojZbritje.Value.ToString() == "1";
            string koordinata = "";
            if (btneCaktoNeHarte.Text != "" && hfState.Contains("geom"))
            {
                string geomsToDeserialize = Convert.ToString(hfState.Get("geom"));
                if (!String.IsNullOrEmpty(geomsToDeserialize))
                {
                    System.Web.Script.Serialization.JavaScriptSerializer serializusi = new System.Web.Script.Serialization.JavaScriptSerializer();
                    object[] geoms = (object[])serializusi.DeserializeObject(geomsToDeserialize);
                    koordinata = Convert.ToString(geoms[0]);
                }
            }

            int idLlojMarreveshje = -1;
            if (cmbLlojMarreveshje.Text != string.Empty && cmbLlojMarreveshje.Value != null)
            {
                int.TryParse(cmbLlojMarreveshje.Value.ToString(), out idLlojMarreveshje);
            }

            DbCore.DbShare.clsKusht kushtvfone = new DbCore.DbShare.clsKusht(KonfigAmbjente.IdKonfigAmbjente, "ZDVFONE");
            clsKonfigurimAmbjenti konfvfone = new clsKonfigurimAmbjenti(kushtvfone.Vlera);
            int idstatusvjeter = -1;
            string niptKlienti = txtNipt.Text;
            if (hfState.Contains("Status"))
                idstatusvjeter = int.Parse(hfState.Get("Status").ToString());
            double vlefte, total1, tvsh1, perqindje;
            Converter.Parse(txtVlefte.Text, out vlefte, "txtVlefte");
            Converter.Parse(txtTotal1.Text, out total1, "txtTotal1");
            Converter.Parse(txtTVSH1.Text, out tvsh1, "txtTVSH1");
            Converter.Parse(txtPerqindje.Text, out perqindje, "txtPerqindje");

            bool kthim = (shtimModifikim == "kthim" || (bool)hfState["eshteDokKthimi"]);
            string nrDokPerMag = String.IsNullOrEmpty(txtNrDokMagazine.Text) ? txtNumer.Text : txtNrDokMagazine.Text;
            string iic = txtIIC.Text;
            string nivf = txtNIVF.Text;
            string eic = txtEIC.Text;
            string nivfKthim = txtNivfKthim.Text;
            string einStatus = "";
            int procesi = 0;
            int tipiEinvoice = 0;
            if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
            {
                string kodiProcesi = cmbProcesi.Text;
                string[] procesiString = kodiProcesi.Split(' ');
                string kodiTipiEinvoice = cmbeInvoiceType.Text;
                string[] tipiEinvoiceString = kodiTipiEinvoice.Split(' ');
                var procesiId = koka.ktheIdProcesi(procesiString[0]);
                var tipiEinvoiceId = koka.ktheIdTipiEinvoice(tipiEinvoiceString[0]);
                procesi = int.Parse(procesiId.Rows[0].ItemArray[0].ToString());
                tipiEinvoice = int.Parse(tipiEinvoiceId.Rows[0].ItemArray[0].ToString());
            }
            else
            {
                procesi = 0;
                tipiEinvoice = 0;
            }
            double.TryParse(txtCash.Text, out double nrtxtCash);
            int operatori = cmbOperatori.Value != null ? int.Parse(cmbOperatori.Value.ToString()) : 0;
            var mesazh = koka.krijoShitje(ref gjeneroDokMag, int.Parse(cmbNiveli.Value.ToString()), 0, KonfigAmbjente.IdKonfigAmbjente, klienti, kodklienti, 1, txtNumerProjekti.Text, data_DateEdit.Date, txtNumer.Text, txtNumerSerial.Text, dtMaturimi, int.Parse(cmbMonedha.Value.ToString()), cmbMonedha.Text, kursi, menyretransporti, btnMenyreTransporti.Text, dttransporti, kushtedergimi, btnKushtDergimi.Text, idagjenti, btnAgjenti.Text, int.Parse(cmbMenyrePagese.Value.ToString()), cmbMenyrePagese.Text, kushtepagese, btnKushtPagese.Text, vlefte, total1 + vlefte, tvsh1, dateRegjistrimi_DateEdit.Date, statusDokumenti, idNdermarrje, idNderViti, 0, 0, 0, 0, txtAdresaFaturimit.Text, txtAdresaDergimit.Text, txtPershkrimi.Text, bool.Parse(cmbDogana.SelectedItem.Value.ToString()), degeadministrative, cmbDegeAdministrative.Text.Split(' ')[0], pikeshitje, cmbPikeShitjeFurnizimi.Text.Split(' ')[0], idPerdoruesi, idFormatPrintimi, trupi, veprimi == "shitje" || veprimi == "shitjediscount" || veprimi == "bazaar" ? true : false, KonfigAmbjente.KodKonfigAmbjente, periudha.IdPeriudha, konfmag, idmagazina, btnMagazina.Text.Split(' ')[0], meKontabilizim, idgrup, idgrup2, idgrup3, dteAfatiKohor.Date, nrtxtCash, statusAprovimi, idPerdoruesi, perqindjeAgjent, out shfaqmesazhapolupe, hfArkiva, qend.ColTrupi, rez.IdKokaRezervimi, out mesazhinformues, gjeneromeme, kokamema, 0, 0, eshteMemme, gjenerobij, StatusTrasferimi.PaTransferuar, txtEmriKlienti.Text, txtKontakti.Text, kasa, cbKupon.Checked, cmbGrup1.Text, dtfillimi, dtmbarimi, idAutomjet, kilometraAtuo, txtTarga.Text, idAgjenti2, perqindjeAgjent2, btnAgjenti2.Text, idAgjenti3, perqindjeAgjent3, btnAgjenti3.Text, txtMarresi.Text, idTransportues, btnTransportues.Text, false, faturashitjengaurdhershitjamekupontatimor, cbShpenzimeJoTeZbritshme.Checked, idArka, false, tollon, autoshitje, false, dtfature, false, tollonakastrati, tollonakastratielektronik, idMuajRaportimi, idVitRaportimi, txtShoferi.Text, txtTarga2.Text, zbritjeNeVlere, perqindje, idKarta, pike, ocolFaza, idFaza, colKlienteFurnitoreVartes, DateTime.Now, new DbData(), llojZevendesimi, koordinata, blerengadealer, krijoartri, idGjuha, konfvfone, idstatusvjeter, niptKlienti, txtQytetiK.Text, false, idkategoriseriali, serialetUnike, txtShenime2.Text, cbKartaPaPagese.Checked, Convert.ToInt32(CacheLayer.GlobalCacheManager.MyPageCache["IdDokTransferimNga"]), kthim, idLlojMarreveshje, txtIdMarreveshje.Text, (StatusMarreveshje)int.Parse(cmbStatusMarreveshje.Value.ToString()), txtKerkuarNga.Text, shtimModifikim, DtKerkese_DateEdit.Date, hfState.Get<bool>("KGJAPMR"), nrDokPerMag, iic, nivf, operatori, nivfKthim, eic, einStatus, procesi, tipiEinvoice, cmbTipiIVetefaturimit.Text);

            if (!mesazh.Status)
            {
                ImbLogger.LogErrorShitje($"Exception:{mesazh.PershkrimMesazhi}");
                throw new Exception(mesazh.PershkrimMesazhi);
            }

            koka.OFleteKontabel.Kontabilizuar = hfKontabilizimi.Value == "1";

            clsKurset kursiifundit = new clsKurset(koka.IdMonedha, koka.DtDok);
            if (kursiifundit.VleraKursi != 0)
            {
                double diferenca = Math.Abs(kursiifundit.VleraKursi - kursi);
                if (diferenca / kursiifundit.VleraKursi > 0.2)
                    clsMenuInfo.ShtoMesazhInformues(MenuInfo, rm.GetString("msgKursiRiNdryshonShumeMeKursinMePare", ci), pnlMesazhi);
            }
            ImbLogger.LogTraceShitje($"Mbaroi metoda krijoRegjistrim me parametra veprimi:{veprimi}, gjeneroDokMag:{gjeneroDokMag}, idNderViti:{idNderViti}, idPerdoruesi:{idPerdoruesi}, idNdermarrje:{idNdermarrje}, statusDokumenti:{statusDokumenti}, klienti:{klienti}, kodklienti:{kodklienti}, KonfigAmbjente:{JsonConvert.SerializeObject(KonfigAmbjente)}, konfmag:{JsonConvert.SerializeObject(konfmag)}, kokamema:{JsonConvert.SerializeObject(kokamema)}, kasa:{kasa}, kontrolloSasi:{kontrolloSasi}, faturashitjengaurdhershitjamekupontatimor:{JsonConvert.SerializeObject(faturashitjengaurdhershitjamekupontatimor)}, tollon:{tollon}, autoshitje:{autoshitje}, shtimModifikim:{shtimModifikim}, periudha:{JsonConvert.SerializeObject(periudha)}, meKontabilizim:{meKontabilizim}, eshteOwn:{eshteOwn}, idEtapa:{idEtapa}, tollonakastrati:{tollonakastrati}, tollonakastratielektronik:{tollonakastratielektronik}, llojZevendesimi:{llojZevendesimi}, zevendesimtollonakastrati:{zevendesimtollonakastrati}, blerengadealer:{blerengadealer}, krijoartri:{krijoartri}, idGjuha:{idGjuha}, serialetUnike:{JsonConvert.SerializeObject(serialetUnike)}");
            return koka;
        }

        /// <summary>
        /// krijon trupin e shitjes si objekt
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="veprimi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idKonfAmbj"></param>
        /// <param name="tollon"></param>
        /// <param name="gridDataObject"></param>
        /// <param name="shtimModifikim"></param>
        /// <param name="gjenerodokumentmagazine"></param>
        /// <param name="ownshop"></param>
        /// <param name="Grup1"></param>
        /// <param name="btnMagazina"></param>
        /// <param name="meme"></param>
        /// <returns></returns>
        public colTrupiShitje ruajTrupShitje(int idPerdoruesi, string veprimi, int idNdermarrje, int idKonfAmbj, bool tollon, string gridDataObject, string gridObjectKomision, string shtimModifikim, bool gjenerodokumentmagazine, bool ownshop, string Grup1, ASPxComboBox btnMagazina, bool meme, DevExpress.Web.ASPxHiddenField hfSeriale, DevExpress.Web.ASPxHiddenField hfIdGride, double perqindjeZbritje, DbCore.DbAsete.colSerialetMagazine colserialemag, bool kontrolloSasi, double kursi, int statusDokumenti, clsKonfigurimAmbjenti konfmag, CultureInfo ci, ResourceManager rm, bool tollonkastati, bool zevendesimtollonakastrati, bool blerengadealer, bool shitjevodafone)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ruajTrupShitje me parametra idPerdoruesi:{idPerdoruesi}, veprimi:" + veprimi + $", idNdermarrje:{idNdermarrje}, idKonfAmbj:{idKonfAmbj}, tollon:{tollon}, gridDataObject:" + gridDataObject + $", shtimModifikim:" + shtimModifikim + $", gjenerodokumentmagazine:{gjenerodokumentmagazine}, ownshop:{ownshop}, Grup1:" + Grup1 + $", meme:{meme}, perqindjeZbritje:{perqindjeZbritje}, kontrolloSasi:{kontrolloSasi}, kursi:{kursi}, statusDokumenti:{statusDokumenti}, konfmag:{JsonConvert.SerializeObject(konfmag)}, tollonkastati:{tollonkastati}, zevendesimtollonakastrati:{zevendesimtollonakastrati}, blerengadealer:{blerengadealer}, shitjevodafone:{shitjevodafone}");
            Dictionary<string, string>[] dokumenti = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(gridDataObject);//serializusi.DeserializeObject(gridDataObject) as object[];
            Dictionary<string, string>[] dokumentiKomision = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(gridObjectKomision);

            colTrupiShitje trupat = new colTrupiShitje();
            int idMagTemp = -1;
            bool isMagENjejte = false;
            bool isShitje = veprimi == "shitje" || veprimi == "shitjediscount" || veprimi == "bazaar";
            bool konvertim = shtimModifikim == "konvertim";
            bool konvertimblerje = shtimModifikim == "konvertimblerje";
            bool klonim = shtimModifikim == "klonim";
            bool kthim = (shtimModifikim == "kthim" || (bool)hfState["eshteDokKthimi"]);
            bool merrSipasGrupit = clsAlternativaKushti.getAlternativa(idKonfAmbj, "AASG") == "Po";
            bool merrDhurata = clsAlternativaKushti.getAlternativa(idKonfAmbj, "AADH") == "Po";
            var merrMagazinenNgaTrupi = clsAlternativaKushti.getAlternativa(idKonfAmbj, "NKDMMT") == "Po";
            int dokumentiLength = dokumenti.Length;
            int dokumentiKomisionLength = dokumentiKomision.Length;
            bool kthimVod = shtimModifikim == "kthimVod";
            bool ruajBarkod = clsAlternativaKushti.getAlternativa(idKonfAmbj, "RBART") == "Po";
            bool lejoMagNdryshme = clsAlternativaKushti.getAlternativa(idKonfAmbj, "LMNGB") == "Po";
            bool lejoSasiPozitiveKthim = clsAlternativaKushti.getAlternativa(idKonfAmbj, "LSPK") == "Po";
            var nrRendorSerial = -1;
            for (int i = 0; i < dokumentiLength; i++)
            {
                clsTrupiShitje trupi = new clsTrupiShitje(idNdermarrje, idPerdoruesi, dokumenti[i], isShitje, konvertim, meme, merrSipasGrupit, Grup1, merrDhurata, ownshop, gjenerodokumentmagazine, klonim, kthim, veprimi, tollon, i, hfSeriale, hfIdGride, perqindjeZbritje, colserialemag, kontrolloSasi, kursi, statusDokumenti, konfmag, tollonkastati, konvertimblerje, zevendesimtollonakastrati, kthimVod, blerengadealer, shitjevodafone, i + 1, ruajBarkod, false, lejoMagNdryshme, data_DateEdit.Date, lejoSasiPozitiveKthim, ref nrRendorSerial);
                if (string.IsNullOrEmpty(trupi.Kodi))
                    continue;

                if (merrMagazinenNgaTrupi || !string.IsNullOrEmpty(btnMagazina.Text))
                {
                    if (idMagTemp == -1)
                    {
                        idMagTemp = trupi.IdMagazina;
                        isMagENjejte = true;
                    }
                    else if (isMagENjejte && trupi.IdMagazina != idMagTemp)
                        isMagENjejte = false;
                }

                trupat.Add(trupi);
            }
            nrRendorSerial = -1;
            for (int i = 0; i < dokumentiKomisionLength; i++)
            {
                clsTrupiShitje trupi = new clsTrupiShitje(idNdermarrje, idPerdoruesi, dokumentiKomision[i], isShitje, konvertim, meme, merrSipasGrupit, Grup1, merrDhurata, ownshop, gjenerodokumentmagazine, klonim, kthim, veprimi, tollon, i, hfSeriale, hfIdGride, perqindjeZbritje, colserialemag, kontrolloSasi, kursi, statusDokumenti, konfmag, tollonkastati, konvertimblerje, zevendesimtollonakastrati, kthimVod, blerengadealer, shitjevodafone, i + 1, ruajBarkod, true, lejoMagNdryshme, data_DateEdit.Date, lejoSasiPozitiveKthim, ref nrRendorSerial);
                trupat.Add(trupi);
            }

            if (isMagENjejte && trupat.Count > 0)
            {
                clsNjesiAdministrative magazinaPerbashket = new clsNjesiAdministrative(trupat[0].IdMagazina, idPerdoruesi);
                btnMagazina.SelectedItem = btnMagazina.Items.Add(magazinaPerbashket.Kodi + " (" + magazinaPerbashket.Pershkrimi + ")", trupat[0].IdMagazina.ToString());
            }
            else
                btnMagazina.Text = string.Empty;

            ImbLogger.LogTraceShitje($"Mbaroi metoda ruajTrupShitje me parametra idPerdoruesi:{idPerdoruesi}, veprimi:" + veprimi + $", idNdermarrje:{idNdermarrje}, idKonfAmbj:{idKonfAmbj}, tollon:{tollon}, gridDataObject:" + gridDataObject + $", shtimModifikim:" + shtimModifikim + $", gjenerodokumentmagazine:{gjenerodokumentmagazine}, ownshop:{ownshop}, Grup1:" + Grup1 + $", meme:{meme}, perqindjeZbritje:{perqindjeZbritje}, kontrolloSasi:{kontrolloSasi}, kursi:{kursi}, statusDokumenti:{statusDokumenti}, konfmag:{JsonConvert.SerializeObject(konfmag)}, tollonkastati:{tollonkastati}, zevendesimtollonakastrati:{zevendesimtollonakastrati}, blerengadealer:{blerengadealer}, shitjevodafone:{shitjevodafone}");
            return trupat;
        }


        /// <summary>
        /// Kontrollon nese jane plotesuar te gjitha fushat e detyrueshme te kokes se dokumentit
        /// </summary>
        /// <param name="idNderViti"></param>
        /// <param name="hfShtimModifikimValue"></param>
        private clsMesazh isValidRegjistrim(int idNderViti, int idNdermarrje, int draft, out clsKlientFurnitor kf, ResourceManager rm, CultureInfo ci, string hfShtimModifikimValue, int idPerdoruesi)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda isValidRegjistrim idNderViti:{idNderViti}, idNdermarrje:{idNdermarrje}, draft:{draft}, hfShtimModifikimValue:" + hfShtimModifikimValue + $", idPerdoruesi:{idPerdoruesi}");
            bool isValid;
            isValid = true;
            kf = new clsKlientFurnitor();
            ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim idNderViti:{idNderViti}, idNdermarrje:{idNdermarrje}, draft:{draft}, hfShtimModifikimValue:" + hfShtimModifikimValue + $", idPerdoruesi:{idPerdoruesi}");
            if (dateRegjistrimi_DateEdit.Text == string.Empty)
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse duhet te zgjidhni nje date regjistrimi");
                return new clsMesazh(false, rm.GetString("msgZgjidhniNjeDateRegjstrimi", ci));
            }
            if (data_DateEdit.Text == string.Empty)
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse duhet te zgjidhni nje date dokumenti");
                return new clsMesazh(false, rm.GetString("msgZgjidhniNjeDateDokumenti", ci));
            }
            if (data_DateEdit.Date.Year != new clsNdermarrjeViti(idNderViti).Viti)
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse data nuk i perket vitit ushtriimor");
                return new clsMesazh(false, rm.GetString("msgDataNukPerketVititUshtrimor", ci));
            }
            clsPeriudhaKontabel periudha;
            if (hfShtimModifikimValue == "shtim" || hfShtimModifikimValue == "konvertim" || hfShtimModifikimValue == "shtimraport" || hfShtimModifikimValue == "konvertimblerje" || hfShtimModifikimValue == "klonim" || hfShtimModifikimValue == "kthim" || hfShtimModifikimValue == "kthimVod" || hfShtimModifikimValue == "bli")
            {
                periudha = mySessionObjects.merrPeriudheKontabel(Session);
            }
            else
                periudha = new clsPeriudhaKontabel(data_DateEdit.Date, idNdermarrje);
            String mesazhGabimi;
            if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, data_DateEdit.Date, periudha, draft))
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse {mesazhGabimi}");
                return new clsMesazh(false, mesazhGabimi);
            }

            if (cmbMonedha.Text == string.Empty)
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse duhet te zgjidhni nje monedhe");
                return new clsMesazh(false, rm.GetString("msgZgjidhniNjeMonedhe", ci));
            }
            if (txtKursi.Text == string.Empty)
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse duhet te shenoni kursin");
                return new clsMesazh(false, rm.GetString("msgShenoniKursin", ci));
            }
            if (draft == 0 && cmbMenyrePagese.Text == "Pagese Automatike")
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse duhet te beehet pagese automatike draft");
                return new clsMesazh(false, rm.GetString("msgNukMundTeBehetPageseAutomatikeDraft", ci));
            }
            if ((cmbMenyrePagese.Text == "Pagese Automatike" || cmbMenyrePagese.Text == "Pagese") && (cmbNiveli.SelectedItem.GetFieldValue("Kodi").ToString() != "FSH" && cmbNiveli.SelectedItem.GetFieldValue("Kodi").ToString() != "FB"))
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse duhet te beehet pagese automatike per dok urdher");
                return new clsMesazh(false, rm.GetString("msgNukMundTeBeniPageseAutomatikePerDokUrdher", ci));
            }

            if ((cmbMenyrePagese.Text == "Pagese Automatike" || cmbMenyrePagese.Text == "Pagese") && (((hfShtimModifikimValue == "kthim" && Request.QueryString["likuiduar"] == null)) || hfShtimModifikimValue == "kthimVod" || hfShtimModifikimValue == "bli"))
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse duhet te beehet pagese automatike perkthim");
                return new clsMesazh(false, rm.GetString("msgNukMundTeBeniPageseAutomatikePerkthim", ci));
            }

            if ((cmbMenyrePagese.Text == "Karte krediti") && (DbCore.DbArkaBanka.clsBanka.ktheLlojBanka(btneArka.Text, idNdermarrje)) == 0)
            {
                //ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse arka jo e sakte per menyre pagese");
                //return new clsMesazh(false, rm.GetString("msgArkaJoSaktePerMenyrePagese", ci));
            }

            else if ((cmbMenyrePagese.Text == "Pagese Automatike") && (DbCore.DbArkaBanka.clsBanka.ktheLlojBanka(btneArka.Text, idNdermarrje)) == 1)
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse arka jo e sakte per menyre pagese");
                return new clsMesazh(false, rm.GetString("msgArkaJoSaktePerMenyrePagese", ci));
                //clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgArkaJoSaktePerMenyrePagese", ci), pnlMesazhi);
                //return false;
            }

            if (cmbDegeAdministrative.Text != string.Empty && cmbDegeAdministrative.Text != " ()")
            {
                clsDegeAdministrative dege = new clsDegeAdministrative(cmbDegeAdministrative.Text.Split(' ')[0], idNdermarrje);
                if (dege.IdDegeAdministrative == -1)
                {
                    ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse kjo dege administrative nuk ekziston");
                    return new clsMesazh(false, rm.GetString("msgKjoDegeAdministrativeNukEkziston", ci));
                }
                else
                {
                    dege = new clsDegeAdministrative(cmbDegeAdministrative.Text.Split(' ')[0], idNdermarrje);
                    if (dege.Aktiv == false)
                    {
                        ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse kjo dege administrative nuk eshte aktive");
                        return new clsMesazh(false, rm.GetString("msgKjoDegeAdministrativeNukEshteAktive", ci));

                    }
                }
            }
            if (cmbPikeShitjeFurnizimi.Text != string.Empty && cmbPikeShitjeFurnizimi.Text != " ()")
            {
                clsPikeShitjeFurnizimi pike = new clsPikeShitjeFurnizimi(cmbPikeShitjeFurnizimi.Text.Split(' ')[0], idNdermarrje);
                if (pike.IdPikeShitjeFurnizimi == -1)
                {
                    ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse kjo kjo pike shitje furnizimi nuk ekziston");
                    return new clsMesazh(false, rm.GetString("msgKjoPikeShitjeFurnizimiNukEziston", ci));

                }
                else
                {
                    pike = new clsPikeShitjeFurnizimi(cmbPikeShitjeFurnizimi.Text.Split(' ')[0], idNdermarrje);
                    if (pike.Aktiv == false)
                    {
                        ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse kjo kjo pike shitje furnizimi nuk eshte aktive");
                        return new clsMesazh(false, rm.GetString("msgKjoPikeShitjeFurnizimiNukEshteAktive", ci));

                    }
                }
            }
            if (cmbGrup1.Text != string.Empty)
            {

                clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(cmbGrup1.Text, idNdermarrje, 1, idPerdoruesi, Convert.ToInt32(cmbModeli.Value));
                if (grup.IdGrupimKoka < 1)
                {
                    ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse grupimi i pare nuk ekziston nuk ekziston");
                    return new clsMesazh(false, rm.GetString("msgGrupimiIPareNukEkziston", ci));

                }

            }
            if (cmbGrup2.Text != string.Empty)
            {
                clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(cmbGrup2.Text, idNdermarrje, 2, idPerdoruesi, Convert.ToInt32(cmbModeli.Value));
                if (grup.IdGrupimKoka < 1)
                {
                    ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse grupimi i dyte nuk ekziston nuk ekziston");
                    return new clsMesazh(false, rm.GetString("msgGrupimiDyteNukEkziston", ci));

                }

            }
            if (cmbGrup3.Text != string.Empty)
            {
                clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(cmbGrup3.Text, idNdermarrje, 3, idPerdoruesi, Convert.ToInt32(cmbModeli.Value));
                if (grup.IdGrupimKoka < 1)
                {
                    ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse grupimi i trete nuk ekziston nuk ekziston");
                    return new clsMesazh(false, rm.GetString("msgGrupimiITreteNukEkziston", ci));

                }

            }
            if (btnKlienti.Text != string.Empty && btnKlienti.Value != null)
            {

                if (!clsKlientFurnitor.EkzistonKlientFurnitor(btnKlienti.Text.Split(' ')[0], idNdermarrje))
                {

                    btnKlienti.Text = string.Empty;
                    ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse KF nuk ekziston");
                    return new clsMesazh(false, rm.GetString("msgKFnukEkziston", ci));
                }
                if (btnKlienti.Text.Split(' ').Length == 1)
                {
                    btnKlienti.Text = string.Empty;
                    ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse KF nuk eshte ngarkuar sakte");
                    return new clsMesazh(false, rm.GetString("msgKFNukEshteNgarkuarSakte", ci));
                }
                else
                {
                    kf.mbushKlientFurnitorSipasKodit(btnKlienti.Text.Split(' ')[0], idNdermarrje, idPerdoruesi);
                }
                if (kf.IdKlientFurnitor <= 0)
                {
                    ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse nuk keni autorizim per kf.");
                    return new clsMesazh(false, "Ju nuk keni autorizime per kete klient/furnitor!");
                }

                //if (kf.IdKlientFurnitor <= 0)
                //{
                //    ImbLogger.LogInfoShitje($"Mbaroi metoda isValidRegjistrim sepse KF nuk ekziston");
                //    return new clsMesazh(false, rm.GetString("msgKFnukEkziston", ci));
                //}
                if (hfkf.Value != string.Empty)
                {
                    if (kf.IdKlientFurnitor != Convert.ToInt32(hfkf.Value) && hfShtimModifikimValue == "kthim")
                    {
                        ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse Kf nuk duhet te ndryshoje ne kthim");
                        return new clsMesazh(false, rm.GetString("msgKFnukDuhetTeNdryshojNeKthim", ci));
                    }
                }
                if (kf.AktivKF == false)
                {
                    ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse ky klient furnitor nuk eshte aktiv");
                    return new clsMesazh(false, rm.GetString("msgKyKlientFurnitorNukEshteAktiv", ci));

                }

                if (btnTransportues.Text != string.Empty && btnTransportues.Value != null)
                {
                    clsTransportues TransportuesAktiv = new clsTransportues(btnTransportues.Text, idNdermarrje);
                    if (TransportuesAktiv.Aktiv == false)

                    {
                        ImbLogger.LogTraceShitje($"Mbaroi metoda isValidRegjistrim sepse ky tranportues nuk eshte aktiv");
                        return new clsMesazh(false, rm.GetString("msgKyTransportuesNukEshteAktiv", ci));

                    }
                }
                return new clsMesazh(true);

            }
            return new clsMesazh(true);
        }


        /// <summary>
        /// Percakton veprimin qe kryhet kur klikohet nje nga butonat e menuse
        /// </summary>
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            ImbLogger.LogTraceShitje("Filloi metoda ASPxMenu1_ItemClick");
            int id;
            clsKokaShitje clsKoka;
            string guidString = (string)hfState["guidString"];
            int idPerdoruesi = IdPerdoruesi;
            int idNdermarrje = (int)hfState["idNdermarrje"];
            string veprimi = (string)hfState["veprimi"];
            int idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            int idViti = (int)hfState["idViti"];
            int idGjuha = (int)hfState["idGjuha"];
            CultureInfo cultinf = MessagesResource.KtheCultureInfo(idGjuha);
            string hfShtimModifikimValue = hfShtimModifikim.Value;
            var periudha = new clsPeriudhaKontabel();
            int idNiveli = Convert.ToInt32(cmbNiveli.Value);
            clsTeDrejtaRoli teDrejtaInfo = new clsTeDrejtaRoli();
            Dictionary<string, List<string>> paTeDrejta = new Dictionary<string, List<string>>();
            colNivelRegjistrimi niveleRregjistrimi = new colNivelRegjistrimi();

            if (hfShtimModifikimValue == "shtim" || hfShtimModifikimValue == "konvertim" || hfShtimModifikimValue == "shtimraport" || hfShtimModifikimValue == "konvertimblerje")
            {
                periudha = mySessionObjects.merrPeriudheKontabel(Session);
            }
            else
                periudha = new clsPeriudhaKontabel(data_DateEdit.Date, idNdermarrje);

            var fazat = mySessionObjects.merrObjectNgaSesioni(Session, "fazat");
            switch (e.Item.Name)
            {
                case "Ruaj":
                    if (!kaTeDrejtePerVeprimin(e.Item.Name))
                        break;
                    if (hfShtimModifikimValue == "shtim" && !clsFunksione.kontrolloDokNeRuajtje(Session.SessionID, Request.QueryString["scopeID"], txtNumer.Text, data_DateEdit.Date.ToString(), cmbModeli.Text, idNdermarrje.ToString(), idPerdoruesi.ToString()))
                        break;
                    Page.Validate();

                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 1, cbPrinto.Checked, StatusAprovimi.Undefined, cbKasa.Checked, false, true, true, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    zbrasHiddenFieldet(idPerdoruesi, idNdermarrje, hfShtimModifikimValue);
                    break;
                case "Draft":
                    if (!kaTeDrejtePerVeprimin(e.Item.Name))
                        break;
                    if (hfShtimModifikimValue == "shtim" && !clsFunksione.kontrolloDokNeRuajtje(Session.SessionID, Request.QueryString["scopeID"], txtNumer.Text, data_DateEdit.Date.ToString(), cmbModeli.Text, idNdermarrje.ToString(), idPerdoruesi.ToString()))
                        break;
                    Page.Validate();
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 0, cbPrinto.Checked, StatusAprovimi.Undefined, cbKasa.Checked, false, true, true, cultinf, rm, hfShtimModifikimValue, periudha, fazat);// 0 = statusi i dokumentit(ne kete rast statusi eshte draft)
                    zbrasHiddenFieldet(idPerdoruesi, idNdermarrje, hfShtimModifikimValue);
                    break;
                case "Aprovo":
                    Page.Validate();
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 0, cbPrinto.Checked, StatusAprovimi.Per_Aprovim, cbKasa.Checked, false, true, true, cultinf, rm, hfShtimModifikimValue, periudha, fazat);// 1 statusi per aprovim
                    zbrasHiddenFieldet(idPerdoruesi, idNdermarrje, hfShtimModifikimValue);
                    break;
                case "Refuzo":
                    Page.Validate();
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 0, cbPrinto.Checked, StatusAprovimi.Refuzuar, cbKasa.Checked, false, true, true, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    zbrasHiddenFieldet(idPerdoruesi, idNdermarrje, hfShtimModifikimValue);
                    break;
                case "Modifiko":
                    Page.Validate();
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 0, cbPrinto.Checked, StatusAprovimi.Undefined, cbKasa.Checked, true, true, true, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    zbrasHiddenFieldet(idPerdoruesi, idNdermarrje, hfShtimModifikimValue);
                    break;
                case "Delego":
                    Page.Validate();
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 0, cbPrinto.Checked, StatusAprovimi.Deleguar, cbKasa.Checked, false, true, true, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    zbrasHiddenFieldet(idPerdoruesi, idNdermarrje, hfShtimModifikimValue);
                    break;
                case "PrintPreview":
                    if (hfShtimModifikimValue == "modifikim")
                    {
                        id = int.Parse(Request.QueryString["id"]);
                        clsKoka = new clsKokaShitje(id);
                        //clsKoka.mbushKokaShitjeSipasIDPaTrup(id);
                        if (String.IsNullOrEmpty(cmbFormatiPrintimit.Text))
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjShitjeMesazhSkaFormatPerPrintim", cultinf), pnlMesazhi);
                        else
                        {
                            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti(clsKoka.IdKonfigAmbjente);
                            string pershkrim = konf.PershkrimKonfigAmbjente.ToLower();

                            if ((pershkrim.Contains("vodafone one") && clsKoka.IdStatusDok == 0) || pershkrim.Contains("porosi bazaar") || pershkrim.Contains("porosi summer promo"))
                            {
                                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk mund te printoni porosi!", pnlMesazhi);
                                Container55.Attributes["src"] = ""; Container1.Attributes["src"] = "";
                                ImbLogger.LogTraceShitje("Mbaroi metoda ASPxMenu1_ItemClick sepse nuk mund te printoni porosi");
                                return;
                            }
                            int idRaporti = clsRaporti.KtheIdRaporti(idGjuha, Convert.ToInt32(cmbFormatiPrintimit.Value));
                            Container55.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idRaporti + "&idDokumenti=" + id + "&printo=false&raportdyte=jo&iddesign=" + cmbFormatiPrintimit.Value;
                        }
                        //if (cbGaranci.Checked)
                        //{
                        colGaranciArtikulli garancia = new colGaranciArtikulli(id);
                        if (garancia.Count > 0)
                        {
                            int idrapgarancia = 142;
                            Container1.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&idraporti=" + idrapgarancia + "&idDokumenti=" + id + "&printo=false&raportdyte=po";
                        }
                        // }
                    }
                    else
                    {
                        Page.Validate();
                        ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 1, cbPrinto.Checked, StatusAprovimi.Undefined, cbKasa.Checked, false, true, true, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                        zbrasHiddenFieldet(idPerdoruesi, idNdermarrje, hfShtimModifikimValue);
                    }
                    break;
                case "DergoEmail":
                    int idKokaShitje = int.Parse(Request.QueryString["id"]);
                    int idDesign = clsKokaShitje.ktheIdRaportDesign(idKokaShitje);
                    if (idDesign <= 0)
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("regjShitjeMesazhSkaFormatPerPrintim", cultinf), pnlMesazhi);
                    else
                    {
                        clsKoka = new clsKokaShitje();
                        clsKoka.mbushKokaShitjeSipasIDPaTrup(idKokaShitje);
                        bool dergoemailMag = (clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "DEFM") == "Po");
                        (clsMesazh gabim, clsMesazh sukses) = EmailComposer.dergoEmailFaturenNgaPerdoruesiLoguar((int)hfState["idGjuha"], cultinf, (int)hfState["idPerdoruesi"], (int)hfState["idViti"], (int)hfState["idNdermarrje"], idKokaShitje, clsKoka.IdKlientFurnitor, clsKoka.NrDok, clsKoka.DtDok, idDesign, clsKoka.IdKonfigAmbjente, (!dergoemailMag) ? new int[] { } : (clsFunksione.MerrIdMagsTrupiPerDok(idKokaShitje)).Rows.Cast<DataRow>().Select(x => Convert.ToInt32(x[0])).ToArray(), dergoemailMag);
                        if (gabim.PershkrimMesazhi != "")
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, gabim.PershkrimMesazhi, pnlMesazhi);
                        if (sukses.PershkrimMesazhi != "")
                            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, sukses.PershkrimMesazhi, pnlMesazhi);
                    }
                    break;
                case "Fshi":
                    id = int.Parse(Request.QueryString["id"]);
                    clsKoka = new clsKokaShitje();
                    clsKoka.mbushKokaShitjeSipasIDPaTrup(id);
                    clsKoka.IdPerdoruesi = idPerdoruesi;
                    if (txtNIVF.Text != "" && txtNIVF.ReadOnly == true && clsKoka.IdStatusDok == 0)
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk mund te fshini nje fature te fiskalizuar!", pnlMesazhi);
                        return;
                    }


                    bool isShitje = (veprimi == "shitje" || veprimi == "shitjediscount" || veprimi == "bazaar");
                    if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(data_DateEdit.Date, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, isShitje ? KategoriDokumenti.Shitje : KategoriDokumenti.Blerje, clsKoka.IdKonfigAmbjente))
                    {
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Nuk mund te ruani dokumentin pasi periudha eshte e mbyllur.", pnlMesazhi);
                        return;
                    }

                    bool tollonakastrati = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "RSHTTK") == "Po";
                    //clsKusht kushttollona = new clsKusht(clsKoka.IdKonfigAmbjente, "RSHTT");
                    //clsAlternativaKushti alttollona = new clsAlternativaKushti(kushttollona.Vlera);
                    bool tollonakastratielektronik = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "RSHTTKE") == "Po";
                    clsKoka.fshi(idPerdoruesi, veprimi == "shitje" ? true : false, tollonakastrati, tollonakastratielektronik);
                    Response.Redirect("RegjistrimDokumentash.aspx?shitje_blerje=" + veprimi);
                    break;
                case "Pezullo":
                    Page.Validate();
                    int idkoka = int.Parse(Request.QueryString["id"]);
                    clsKoka = new clsKokaShitje();
                    clsKoka.mbushKokaShitjeSipasIDPaTrup(idkoka);
                    if (clsKoka.IdStatusDok == 0)
                    {
                        ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 8, cbPrinto.Checked, StatusAprovimi.Undefined, cbKasa.Checked, false, true, true, cultinf, rm, hfShtimModifikimValue, periudha, fazat);// 0 = statusi i dokumentit(ne kete rast statusi eshte draft)
                        zbrasHiddenFieldet(idPerdoruesi, idNdermarrje, hfShtimModifikimValue);
                    }
                    else clsMenuInfo.ShtoMesazhInformues(MenuInfo, rm.GetString("msgPezullohenVetemDraft", ci), pnlMesazhi);
                    break;
                case "RefuzoDraft":
                    Page.Validate();
                    if (String.IsNullOrEmpty(txtShenime2.Text))
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ju lutem vendosni nje koment para refuzimit te dokumentit!", pnlMesazhi);
                    else
                    {
                        id = int.Parse(Request.QueryString["id"]);
                        clsMesazh msgRefuzuar = refuzoDraft(id, txtShenime2.Text);
                        if (!msgRefuzuar.Status)
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, msgRefuzuar.PershkrimMesazhi, pnlMesazhi);
                        else
                        {
                            Response.Redirect(string.Format("RegjistrimDokumentash.aspx?shitje_blerje={0}&refuzoDraft=po", veprimi));
                            return;
                        }
                    }
                    break;

                default:
                    break;
            }
            ImbLogger.LogTraceShitje("Mbaroi metoda ASPxMenu1_ItemClick");
        }

        /// <summary>
        /// buttoni fshirjes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 

        protected void ButtonOk_Click2(object sender, EventArgs e)
        {
            ImbLogger.LogTraceShitje("Filloi metoda ButtonOk_Click2");
            CultureInfo ci = MessagesResource.KtheCultureInfo((int)hfState["idGjuha"]);
            pergjigja.Text = string.Empty;
            colTrupiMagazina tr = new colTrupiMagazina();
            bool rivleresim = false;
            int id = int.Parse(Request.QueryString["id"]);
            clsKokaShitje clsKoka = new clsKokaShitje();
            clsKoka.mbushKokaShitjeSipasIDPaTrup(id);

            bool lidhur = clsKoka.eshteILidhur();
            bool tollona = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "RSHTT") == "Po";
            bool tollonakastrati = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "RSHTTK") == "Po";
            bool tollonakastratielektronik = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "RSHTTKE") == "Po";
            if (tollona && DbCore.DbTollona.clsShitjeMeSerial.kaTollonaShitja(clsKoka.IdShitjeKoka))
            {
                lidhur = true;
            }
            if (tollonakastrati && DbCore.DbTollona.clsTollonaLeter.kaTollonaShitja(clsKoka.IdShitjeKoka))
            {
                lidhur = true;
            }
            if (tollonakastratielektronik && DbCore.DbTollona.clsTollonaElektronik.kaTollonaShitja(clsKoka.IdShitjeKoka))
            {
                lidhur = true;
            }
            if (lidhur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("vepBankaMsgDokEshteILidhurNukFshihet", ci), pnlMesazhi);
                status1.Value = "false";
                ImbLogger.LogTraceShitje("Mbaroi metoda ButtonOk_Click2 sepse vepBankaMsgDokEshteILidhurNukFshihet");
                return;
            }
            int idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            if (clsKoka.DtDok.Year != new clsNdermarrjeViti(idNdermarrjeVit).Viti)
            {
                status1.Value = "false";
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", ci), pnlMesazhi);
                ImbLogger.LogTraceShitje("Mbaroi metoda ButtonOk_Click2 sepse msgDataNukPerketVititUshtrimor");
                return;
            }
            int idNdermarrje = (int)hfState["idNdermarrje"];
            bool ekycur = DbCore.DbAdmin.clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DtDok, idNdermarrje);
            if (ekycur)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgPeriudhaEKycur", ci), pnlMesazhi);
                ImbLogger.LogTraceShitje("Mbaroi metoda ButtonOk_Click2 sepsemsgPeriudhaEKycur");
                return;
            }

            int idKategoria = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(clsKoka.IdNivel);
            bool isShitje = (idKategoria == 1) ? true : false;
            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(data_DateEdit.Date, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, isShitje ? KategoriDokumenti.Shitje : KategoriDokumenti.Blerje, clsKoka.IdKonfigAmbjente))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                ImbLogger.LogTraceShitje("Mbaroi metoda ButtonOk_Click2 sepse msgPeriodIsClosed");
                return;
            }
            clsMesazh mesazh = new clsMesazh();
            clsKokaMagazina kok = new clsKokaMagazina();
            kok.mbushKokaMagazinaSipasIDGjenerues(clsKoka.IdShitjeKoka, idKategoria == 1 ? 2 : 1, clsKoka.IdKonfigAmbjente);
            if (kok.IdKokaMagazina != 0)
            {
                kok.mbushTrupMagazine(false);
                colArtikujt coleksistues = new colArtikujt(kok.IdKokaMagazina, new clsDatabaseInventari());
                int i = 0;
                foreach (clsTrupiMagazina trup in kok.OcolTrupiMagazina)
                {
                    if (trup.IdLlojVeprimi == 1)
                        trup.Element = coleksistues[i];
                    i++;
                }
                mesazh = kok.kontrolloGjendjeNeFshirje(new colTrupiMagazina(), 0);
                if (!mesazh.Status)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                    ImbLogger.LogTraceShitje($"Mbaroi metoda ButtonOk_Click2 sepse {mesazh.PershkrimMesazhi}");
                    return;
                }
            }
            if (kok.IdKokaMagazina != 0 && string.Compare(hfKontrollRivleresim.Value, "true", StringComparison.OrdinalIgnoreCase) == 0 && kok.IdStatusDok != 0)
                if (kok.rivleresim())
                    rivleresim = true;
            tr.mbushGjitheTrupiMagazinaNgaKoka(kok.IdKokaMagazina);
            trupat.AddRange(tr);
            string guidString = hfState["guidString"].ToString();
            int idPerdoruesi = IdPerdoruesi;
            clsKoka.IdPerdoruesi = idPerdoruesi;
            mesazh = clsKoka.fshi(idPerdoruesi, isShitje, tollonakastrati, tollonakastratielektronik);
            mySessionObjects.ruajTrupatNeSession(Session, trupat);
            if (mesazh.Status)
            {
                if (rivleresim)
                {
                    clsMenuInfo.ShtoPyetje(MenuInfo, rm.GetString("regjMagVeprimiSjellNdryshimNeCmimDalje", ci), pnlMesazhi, (int)hfState["idGjuha"]);
                    pergjigja.Text = "fshi";
                    pergjigja.ClientVisible = false;
                }
                else //if (pergjigja.Text != "Dokumenti eshte i lidhur dhe nuk mund te fshihet!" && pergjigja.Text != "Nuk mund te kryeni veprime sepse periudha eshte e kycur!")
                {
                    string veprimi = (string)hfState["veprimi"];
                    Response.Redirect(string.Format("RegjistrimDokumentash.aspx?shitje_blerje={0}&fshi=po", veprimi));
                    status1.Value = "true";
                    return;
                }
            }
            else
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
                status1.Value = "false";
                ImbLogger.LogTraceShitje("Mbaroi metoda ButtonOk_Click2");
                return;
            }
        }

        protected void ASPxCallback1_Callback(object source, DevExpress.Web.CallbackEventArgs e)
        {
            if (e.Parameter == "formatnumri")
            {
                clsFormatKonfigTrup formatMonedhe = new clsFormatKonfigTrup();
                object form = JsonConvert.DeserializeObject<object>(hfState["formatMonedhe"].ToString());
                Dictionary<string, object> formati = (Dictionary<string, object>)form;
                formatMonedhe = new clsFormatKonfigTrup(int.Parse(formati["ShifraPasPresjesSasia"].ToString()), int.Parse(formati["ShifraPasPresjesCmimi"].ToString()), int.Parse(formati["ShifraPasPresjesVlefta"].ToString()), int.Parse(formati["ShifraPasPresjesZbritja"].ToString()), string.Empty, string.Empty, string.Empty, string.Empty);
                //  vendosDisplayFormatStringTotalet(formatMonedhe);
            }
        }

        private void ruajJoNgaMenuja(bool kontrollosasi, ResourceManager rm, int idPerdoruesi)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ruajJoNgaMenuja me parametra kontrollsasiL{kontrollosasi}, idPerdoruesi:{idPerdoruesi}");
            Page.Validate();
            string veprimi = (string)hfState["veprimi"];
            int idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuha = (int)hfState["idGjuha"];
            CultureInfo cultinf = MessagesResource.KtheCultureInfo(idGjuha);
            string hfShtimModifikimValue = hfShtimModifikim.Value;
            var periudha = new clsPeriudhaKontabel();
            if (hfShtimModifikimValue == "shtim" || hfShtimModifikimValue == "konvertim" || hfShtimModifikimValue == "shtimraport" || hfShtimModifikimValue == "konvertimblerje")
            {
                periudha = mySessionObjects.merrPeriudheKontabel(Session);
            }
            else
                periudha = new clsPeriudhaKontabel(data_DateEdit.Date, idNdermarrje);
            var fazat = mySessionObjects.merrObjectNgaSesioni(Session, "fazat");
            string usernamePerdoruesi = new clsPerdorues(IdPerdoruesi).PerdoruesUsername;
            clsFunksione.dergoLogAlphaweb(new clsNdermarrje(idNdermarrje).NdermarrjePershkrimi, "Shtim shitje/blerje", "Regjistrim shitje ose blerje", clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar(), usernamePerdoruesi);
            switch (hfRuajDraft.Value)
            {
                case "Ruaj":
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 1, cbPrinto.Checked, StatusAprovimi.Undefined, cbKasa.Checked, false, kontrollosasi, true, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    break;
                case "Draft":
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 0, cbPrinto.Checked, StatusAprovimi.Undefined, cbKasa.Checked, false, kontrollosasi, true, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    break;

                case "Aprovo":
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 0, cbPrinto.Checked, StatusAprovimi.Per_Aprovim, cbKasa.Checked, false, kontrollosasi, true, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    break;
                case "Refuzo":
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 0, cbPrinto.Checked, StatusAprovimi.Refuzuar, cbKasa.Checked, false, kontrollosasi, true, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    break;
                case "Delego":
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 0, cbPrinto.Checked, StatusAprovimi.Deleguar, cbKasa.Checked, false, kontrollosasi, true, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    break;
                case "Modifiko":
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 0, cbPrinto.Checked, StatusAprovimi.Undefined, cbKasa.Checked, true, kontrollosasi, true, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    break;

                default:
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 1, cbPrinto.Checked, StatusAprovimi.Undefined, cbKasa.Checked, false, kontrollosasi, true, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    break;
            }
            zbrasHiddenFieldet(idPerdoruesi, idNdermarrje, hfShtimModifikimValue);
            ImbLogger.LogTraceShitje($"Mbaroi metoda ruajJoNgaMenuja me parametra kontrollsasiL{kontrollosasi}, idPerdoruesi:{idPerdoruesi}");
            //   percaktoTemplateInfo();
        }
        /// <summary>
        /// ruan ne rastin kur ka limit, dhe kur shtyp F9
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnruaj_Click(object sender, EventArgs e)
        {
            ruajJoNgaMenuja(true, rm, IdPerdoruesi);

        }

        #region autocomplete

        protected void btnKlienti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback) return;
            if (Request.Params["__CALLBACKID"].Contains("btnKlienti"))
            {
                int value = 0;
                if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))

                    return;

                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], (ASPxComboBox)source, value);
            }
        }

        protected void btnKlienti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("btnKlienti"))
                {
                    ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, (ASPxComboBox)source, cmbModeli.Text, (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], (int)hfState["idGjuha"], 0);
                }
            }
        }

        protected void btneArka_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("btneArka"))
                {
                    if (cmbMenyrePagese.Text == "Karte krediti")
                        ConfigureAspxComboBox.mbushComboArkat((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], (ASPxComboBox)source, true);
                    else
                        ConfigureAspxComboBox.mbushComboArkat((int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"], (ASPxComboBox)source, false);
                }
            }
        }

        protected void btneArka_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("btneArka"))
                {
                    ConfigureAspxComboBox.mbushComboArkaBankaSipasFilter(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, (ASPxComboBox)source, string.Empty, (int)hfState["idPerdoruesi"], (int)hfState["idNdermarrje"]);
                }
            }
        }

        protected void btneAutomjeti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("btneAutomjeti"))
                {
                    int value = 0;
                    if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                        return;
                    ConfigureAspxComboBox.mbushComboAutomjetiByID((ASPxComboBox)source, value);
                }
            }
        }

        protected void btneAutomjeti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("btneAutomjeti"))
                {
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    if (btnKlienti.Text != string.Empty)
                    {
                        int idKlienti = 0;
                        if (btnKlienti.Value != null)
                            idKlienti = int.Parse(btnKlienti.Value.ToString());
                        ConfigureAspxComboBox.mbushComboAutomjeteshMeKlient(idNdermarrje, idKlienti, btneAutomjeti, e);
                    }
                    else
                        ConfigureAspxComboBox.mbushComboAutomjetesh(idNdermarrje, btneAutomjeti, e);
                }
            }
        }


        protected void cmbKarta_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbKarta"))
                {
                    int value = 0;
                    if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                        return;
                    mbushComboBoxKartat(value);

                }
            }
        }

        protected void cmbKarta_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbKarta") && !string.IsNullOrEmpty(e.Filter))
                {
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    bool kartavarurngaklienti = (bool)hfState["LKVK"];
                    int idKlient = 0;
                    if (btnKlienti.Text != string.Empty && kartavarurngaklienti)
                    {
                        if (btnKlienti.Value != null)
                            idKlient = int.Parse(btnKlienti.Value.ToString());
                    }
                    ConfigureAspxComboBox.KonfiguroComboBoxKartaMeFilter(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, cmbKarta, idNdermarrje, idKlient);
                }
            }
        }

        protected void cmbFaza_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("cmbFaza"))
                {
                    int value = 0;
                    if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                        return;
                    ConfigureAspxComboBox.mbushComboFazatById((ASPxComboBox)source, value);

                }
            }
        }

        protected void btnMagazina_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("btnMagazina"))
                {
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    int idPerdoruesi = (int)hfState["idPerdoruesi"];
                    ConfigureAspxComboBox.mbushComboMagazinatMeFilter(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, btnMagazina, idPerdoruesi, idNdermarrje, false, 0, true);
                }
            }
        }

        protected void btnMagazina_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (IsCallback)
            {
                if (Request.Params["__CALLBACKID"].Contains("btnMagazina"))
                {
                    int idNdermarrje = (int)hfState["idNdermarrje"];
                    int idPerdoruesi = (int)hfState["idPerdoruesi"];
                    int value = 0;
                    if (e.Value == null || !Int32.TryParse(e.Value.ToString(), out value))
                        return;

                    ConfigureAspxComboBox.mbushComboMagazinatMeID(idNdermarrje, btnMagazina, idPerdoruesi, false, 0, true, value);
                }
            }
        }

        #endregion

        protected void ButtonOk5_Click(object sender, EventArgs e)
        {
            ImbLogger.LogTraceShitje("Filloi metoda ButtonOk5_Click");
            Page.Validate();
            string veprimi = (string)hfState["veprimi"];
            int idNdermarrjeVit = (int)hfState["idNdermarrjeVit"];
            int idPerdoruesi = IdPerdoruesi;
            int idNdermarrje = (int)hfState["idNdermarrje"];
            int idGjuha = (int)hfState["idGjuha"];
            CultureInfo cultinf = MessagesResource.KtheCultureInfo(idGjuha);
            string hfShtimModifikimValue = hfShtimModifikim.Value;
            var periudha = new clsPeriudhaKontabel();
            if (hfShtimModifikimValue == "shtim" || hfShtimModifikimValue == "konvertim" || hfShtimModifikimValue == "shtimraport" || hfShtimModifikimValue == "konvertimblerje")
            {
                periudha = mySessionObjects.merrPeriudheKontabel(Session);
            }
            else
                periudha = new clsPeriudhaKontabel(data_DateEdit.Date, idNdermarrje);
            var fazat = mySessionObjects.merrObjectNgaSesioni(Session, "fazat");
            switch (hfRuajDraft.Value)
            {
                case "Ruaj":
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 1, cbPrinto.Checked, StatusAprovimi.Undefined, cbKasa.Checked, false, false, false, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    break;
                case "Draft":
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 0, cbPrinto.Checked, StatusAprovimi.Undefined, cbKasa.Checked, false, false, false, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    break;

                case "Aprovo":
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 0, cbPrinto.Checked, StatusAprovimi.Per_Aprovim, cbKasa.Checked, false, false, false, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    break;
                case "Refuzo":
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 0, cbPrinto.Checked, StatusAprovimi.Refuzuar, cbKasa.Checked, false, false, false, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    break;
                case "Delego":
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 0, cbPrinto.Checked, StatusAprovimi.Deleguar, cbKasa.Checked, false, false, false, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    break;
                case "Modifiko":
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 0, cbPrinto.Checked, StatusAprovimi.Undefined, cbKasa.Checked, true, false, false, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    break;

                default:
                    ruajRegjistrim(idGjuha, idNdermarrje, idPerdoruesi, idNdermarrjeVit, veprimi, 1, cbPrinto.Checked, StatusAprovimi.Undefined, cbKasa.Checked, false, false, false, cultinf, rm, hfShtimModifikimValue, periudha, fazat);
                    break;
            }
            zbrasHiddenFieldet(idPerdoruesi, idNdermarrje, hfShtimModifikimValue);
            ImbLogger.LogTraceShitje("Mbaroi metoda ButtonOk5_Click");
        }
        protected void ucEmerSkedari_FileUploadComplete(object sender, DevExpress.Web.FileUploadCompleteEventArgs e)
        {

        }

        protected void cmbKategoriSeriali_Callback(object sender, CallbackEventArgsBase e)
        {

        }

        protected void serialetCallbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            ImbLogger.LogTraceShitje("Filloi metoda serialetCallbackPanel_Callback");
            colSerialeUnikeMagazina serialetNeMagazine = mySessionObjects.MerrNgaSession<colSerialeUnikeMagazina>(Session, Constants.SERIALE_UNIKE_TE_NGARKUAR, hfState.Get<string>("guidString"));
            if (serialetNeMagazine == null)
                serialetNeMagazine = new colSerialeUnikeMagazina();
            var serialetOld = serialetNeMagazine.Clone();

            int idDok = 0;
            int.TryParse(Request.QueryString["id"], out idDok);
            List<Tuple<int, string>> serialeNeFBKthim = clsKokaShitje.merrSerialeNeFBKthim(idDok);


            try
            {
                var dictionarySkedare = CacheLayer.GlobalCacheManager.MySessionCache.Get<Dictionary<string, HttpPostedFile>>("SkedareSerial");
                if (dictionarySkedare == null)
                {
                    ImbLogger.LogErrorShitje("Nuk ka seriale te ngarkuar");
                    throw new MyException("Nuk ka seriale te ngarkuar");
                }

                int idNdermarrje = hfState.Get<int>("idNdermarrje");
                var emerKategoria = hfState.Get<string>("kategoriSeriali");

                clsSerialeUnikeKategori kategori = new clsSerialeUnikeKategori(emerKategoria, idNdermarrje, true);
                if (kategori.ID < 0)
                {
                    ImbLogger.LogErrorShitje("Kategoria e serialit nuk ekziston");
                    throw new MyException("Kategoria e serialit nuk ekziston");
                }
                mySessionObjects.RuajNeSession<clsSerialeUnikeKategori>(Session, kategori, "kategoriId");
                colSerialeUnikeFusha Fushat = new colSerialeUnikeFusha(kategori.ID);

                foreach (KeyValuePair<string, HttpPostedFile> entry in dictionarySkedare)
                {

                    var dtSerialet = clsFunksione.LexoFileImportSerialeUnike(Session, entry.Value, kategori, Fushat);
                    serialetNeMagazine.ShtoSeriale(idNdermarrje, dtSerialet, Fushat, kategori);
                }


                var mesazh = serialetNeMagazine.KontrolloSerialeTePerseritur(kategori.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.RINGARKUES.ToString()) ? kategori.ID : -1);
                if (!mesazh)
                {
                    ImbLogger.LogErrorShitje($"Exception:{mesazh}");
                    throw new MyException(mesazh.PershkrimMesazhi);
                }
                bool ekzistojneSerialet = true;
                if (serialeNeFBKthim.Count > 0)
                    ekzistojneSerialet = serialetNeMagazine.KontrolloSerialet(serialeNeFBKthim, true);

                if (!ekzistojneSerialet)
                {
                    ImbLogger.LogErrorShitje($"Exception:{MessagesResource.Messages["serialiNukGjendetNeFbkthim"]}");
                    throw new MyException(MessagesResource.Messages["serialiNukGjendetNeFbkthim"]);
                }

                List<clsArtikujMeSasi> artMeSas = serialetNeMagazine.MerrArtikujMeSasi(e.Parameter, (bool)hfState["MNSA"], true, false);


                var artikujSeriale = serialetNeMagazine.Where(s => s.IdSeti == 0)
                   .GroupBy(ac => new
                   {
                       ac.IdArtikulli//,
                                     //   ac.IdMag
                   }).Select(ac => new
                   {
                       IdArtikulli = ac.Key.IdArtikulli,
                       //  IdMag = ac.Key.IdMag,
                       Sasia = ac.Sum(acs => acs.Sasia),
                   });
                String artikujt = "";

                mySessionObjects.RuajNeSession<colSerialeUnikeMagazina>(Session, serialetNeMagazine, Constants.SERIALE_UNIKE_TE_NGARKUAR, hfState.Get<string>("guidString"));
                serialetCallbackPanel.JSProperties.Add("cpArtikujSeriale", JsonConvert.SerializeObject(artikujSeriale));

            }
            catch (Exception err)
            {
                mySessionObjects.RuajNeSession<colSerialeUnikeMagazina>(Session, serialetOld, Constants.SERIALE_UNIKE_TE_NGARKUAR, hfState.Get<string>("guidString"));
                ImbLogger.Error(err);
                ImbLogger.LogErrorShitje($"Exception:{err}");
                serialetCallbackPanel.JSProperties.Add("cpSerialMesazhGabimi", err.Message);
            }
            finally
            {
                CacheLayer.GlobalCacheManager.MySessionCache.Remove("SkedareSerial");
            }
            ImbLogger.LogTraceShitje("Mbaroi metoda serialetCallbackPanel_Callback");
        }
        private void mbushComboProcesi()
        {
            clsKokaShitje koka = new clsKokaShitje();
            var dt = koka.ktheVleratProcesi();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].ItemArray[0].ToString() == " ()")
                    cmbProcesi.Items.Add("");
                else
                    cmbProcesi.Items.Add(dt.Rows[i].ItemArray[0].ToString());
            }
        }
        private void mbushComboTipiIVetefaturimit()
        {
            cmbTipiIVetefaturimit.Items.Add("AGREEMENT");
            cmbTipiIVetefaturimit.Items.Add("DOMESTIC");
            cmbTipiIVetefaturimit.Items.Add("ABROAD");
            cmbTipiIVetefaturimit.Items.Add("SELF");
            cmbTipiIVetefaturimit.Items.Add("OTHER");
            cmbTipiIVetefaturimit.Items.Add("");
        }
        private void mbushComboTipiEinvoice()
        {
            clsKokaShitje koka = new clsKokaShitje();
            var dt = koka.ktheVleratTipiEinvoice();
            for (var i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i].ItemArray[0].ToString() == " ()")
                    cmbeInvoiceType.Items.Add("");
                else
                    cmbeInvoiceType.Items.Add(dt.Rows[i].ItemArray[0].ToString());
            }
        }
        private void mbushComboNiveli()
        {
            ImbLogger.LogTraceShitje("Filloi metoda mbushComboNiveli");
            int idPerdoruesi = Convert.ToInt32(hfState.Get("idPerdoruesi"));
            int idNdermarrje = Convert.ToInt32(hfState.Get("idNdermarrje"));
            int idViti = Convert.ToInt32(hfState.Get("idViti"));
            string veprimi = Convert.ToString(hfState.Get("veprimi"));

            if (veprimi == "shitje")
                ConfigureAspxComboBox.KonfiguroComboBoxNivelet(cmbNiveli, idNdermarrje, idPerdoruesi, 1, idViti, clsFunksione.GetKomponente(Page.Request), false, true);
            else if (veprimi == "shitjediscount" || veprimi == "bazaar")
            {
                ConfigureAspxComboBox.KonfiguroComboBoxNiveletSipasKategoriDtCombo(cmbNiveli, idNdermarrje, idPerdoruesi, 1, false, true, true);
                DataRow[] drow = ((DataView)cmbNiveli.DataSource).ToTable().Select("KODI= 'USH'");
                if (drow != null && drow.Length > 0)
                    cmbNiveli.SelectedItem = cmbNiveli.Items.FindByValue(Convert.ToInt32(drow[0]["IDNIVEL"]));
            }

            else
                ConfigureAspxComboBox.KonfiguroComboBoxNivelet(cmbNiveli, idNdermarrje, idPerdoruesi, 2, idViti, clsFunksione.GetKomponente(Page.Request), false, true);
            ImbLogger.LogTraceShitje("Mbaroi metoda mbushComboNiveli");
        }

        private bool kaTeDrejtePerVeprimin(string menuItemName)
        {
            ImbLogger.LogTraceShitje("Filloi metoda kaTeDrejtePerVeprimin me parameter menuItemName:" + menuItemName);
            string veprimi = (string)hfState["veprimi"];
            if (veprimi == "shitje" || veprimi == "blerje")
            {
                int idPerdoruesi = (int)hfState["idPerdoruesi"];
                int idNdermarrje = (int)hfState["idNdermarrje"];
                int idViti = (int)hfState["idViti"];
                string hfShtimModifikimValue = hfShtimModifikim.Value;
                int idNiveli = Convert.ToInt32(cmbNiveli.Value);
                clsTeDrejtaRoli teDrejtaInfo = new clsTeDrejtaRoli();
                teDrejtaInfo.merrTeDrejtaPerKeteKomponenteDheNivelRegjistrimi(idPerdoruesi, idNdermarrje, idViti, clsFunksione.GetKomponente(Page.Request), idNiveli);

                bool kaTeDrejte = true;

                switch (menuItemName)
                {
                    case "Ruaj":
                        if (hfShtimModifikimValue == "shtim" && !teDrejtaInfo.DShtim)
                            kaTeDrejte = teDrejtaInfo.DShtim;
                        if (hfShtimModifikimValue == "modifikim")
                            kaTeDrejte = teDrejtaInfo.DMod;
                        break;
                    case "Draft":
                        if (hfShtimModifikimValue == "shtim")
                            kaTeDrejte = teDrejtaInfo.DShtimDraft;
                        if (hfShtimModifikimValue == "modifikim")
                            kaTeDrejte = teDrejtaInfo.DModifikimDraft;
                        break;
                }
                if (!kaTeDrejte)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                    ImbLogger.LogTraceShitje("Mbaroi metoda kaTeDrejtePerVeprimin me parameter menuItemName:" + menuItemName);
                    return false;
                }
            }
            ImbLogger.LogTraceShitje("Mbaroi metoda kaTeDrejtePerVeprimin me parameter menuItemName:" + menuItemName);
            return true;
        }

        private void VendosDetajimeNeTrupNeseNukEkzistojne(colTrupiShitje col, colArtikujt colArtikuj, colDetajimeArtikulli colDet1, colDetajimeArtikulli colDet2, DateTime data)
        {
            List<Dictionary<string, object>> detajimeNeGride = new List<Dictionary<string, object>>();
            col.FindAll(rreshti => rreshti.IdDetajimArt == 0 && rreshti.IdDetajimArt2 == 0).ForEach(rreshti =>
            {
                clsArtikulli artikulli = colArtikuj.FirstOrDefault(y => y.IdArtikulli == rreshti.IdKodi);
                if (artikulli.IdKategoriDetajimi.EqualsAny(3, 4) || artikulli.IdKategoriDetajimi2.EqualsAny(3, 4))
                {
                    List<Dictionary<string, object>> detSasite = col.FindAll(item => item.IdKodi == rreshti.IdKodi).GroupBy(info => info.IdDetajimArt)
                    .Select(group => new Dictionary<string, object> { { "Detajim", colDet1.FirstOrDefault(x => x.IdDetajimArtikulli == group.Key).KodDetajimArtikulli }, { "Sasi", group.Sum(z => z.Sasia) } })
                    .ToList();
                    detSasite.AddRange(col.FindAll(item => item.IdKodi == rreshti.IdKodi).GroupBy(info => info.IdDetajimArt2)
                    .Select(group => new Dictionary<string, object> { { "Detajim", colDet2.FirstOrDefault(x => x.IdDetajimArtikulli == group.Key).KodDetajimArtikulli }, { "Sasi", group.Sum(z => z.Sasia) } })
                    .ToList());
                    var detSasi = new { shtimModifikim = "shtim", idDok = 0, dokShitje = true, detSasite = detSasite };
                    Dictionary<String, Object>[] detajimDheSasi = new Dictionary<String, Object>[2];
                    detajimDheSasi = clsArtikulli.ktheArtDetajimet(artikulli, IdPerdoruesi, data, rreshti.IdMagazina, 0, JsonConvert.SerializeObject(detSasi));
                    if (detajimDheSasi[0] != null)
                    {
                        clsDetajimArtikulli currentDet1 = colDet1.ElementAtOrDefault(col.IndexOf(rreshti));
                        currentDet1.mbushDetajimArtikulli((clsDetajimArtikulli)((Dictionary<String, Object>)detajimDheSasi[0])["detajim"]);
                        rreshti.IdDetajimArt = currentDet1.IdDetajimArtikulli;
                    }
                    if (detajimDheSasi[1] != null)
                    {
                        clsDetajimArtikulli currentDet2 = colDet2.ElementAtOrDefault(col.IndexOf(rreshti));
                        currentDet2.mbushDetajimArtikulli((clsDetajimArtikulli)((Dictionary<String, Object>)detajimDheSasi[1])["detajim"]);
                        rreshti.IdDetajimArt2 = currentDet2.IdDetajimArtikulli;
                    }
                }
            });
        }

        private string[] DergoEinvoice(clsKokaShitje kokeShitje, clsKlientFurnitor kf, clsNdermarrje nderm, string iicSignature, string nivfFature, DataRow operatori, string viti)
        {

            string[] EIC = new string[1];

            var trupShitje = kokeShitje.merrTrupShitje();
            string kodTVSH = "";
            string arsyeKodTvsh = "";
            if (kokeShitje.Dogana)
            {
                kodTVSH = "G";
                arsyeKodTvsh = "vatex-eu-g";
            }
            else if (kokeShitje.Tvsh == 0 && !kf.AutoNgarkese && !kf.ShitjePaTvsh)
            {
                kodTVSH = "Z";
                arsyeKodTvsh = "vatex-eu-z";
            }
            else if (kf.AutoNgarkese)
            {
                kodTVSH = "AE";
                arsyeKodTvsh = "vatex-eu-ae";
            }
            else if (kf.ShitjePaTvsh)
            {
                kodTVSH = "O";
                arsyeKodTvsh = "vatex-eu-o";
            }
            else if (!kf.AutoNgarkese && !kf.ShitjePaTvsh && kokeShitje.Tvsh != 0)
            {
                kodTVSH = "S";
                arsyeKodTvsh = "vatex-eu-s";
            }
            clsMonedha monedhaKlientit = new clsMonedha(kf.idMonedha);
            string kodMonedha = "";
            string kodShtetiNdermarrje = "";
            string kodShtetiKlienti = kf.ShtetiKF;
            if (monedhaKlientit.KodiMonedha == "LEK")
                kodMonedha = "ALL";
            else
                kodMonedha = monedhaKlientit.KodiMonedha;
            if (nderm.NdermarrjeVendi == "ALB")
                kodShtetiNdermarrje = "AL";
            if (kf.ShtetiKF == "ALB")
                kodShtetiKlienti = "AL";
            var degeAdministrative = clsDegeAdministrative.ktheDegeAdministrativeSipasiD(kokeShitje.IdDegeAdministrative);
            string kodiProcesi = cmbProcesi.Text;
            string[] procesiString = kodiProcesi.Split(' ');
            string kodiTipiEinvoice = cmbeInvoiceType.Text;
            string[] tipiEinvoiceString = kodiTipiEinvoice.Split(' ');
            string kodProcesi = procesiString[0];
            string kodTipiEinvoice = tipiEinvoiceString[0];


            clsQyteti qytetKlienti = new clsQyteti(kf.EmriQytetitKF, nderm.IdNdermarrje);
            clsQyteti qytetiNdermarrje = new clsQyteti(nderm.NdermarrjeQytetiPershkrimi, nderm.IdNdermarrje);
            var zbritjeTotale = Double.Parse(String.Format("{0:0.00}", txtTotaliPaTVSH1.Text));
            clsKokaShitje kokeShitjeje = new clsKokaShitje(kokeShitje.IdShitjeKoka);
            string kodSoftueri = WebConfigurationManager.AppSettings["kodSoftueri"];
            string adresKlienti = txtAdresaFaturimit.Text;
            double kursi = kokeShitje.Kursi;
            DateTimeOffset dtKrijimiPajisjeOffset = new DateTimeOffset(kokeShitje.DtKrijimiPajisje);
            string[] fatureUbl = new string[2];
            if (kokeShitje.DtDok.ToString().Split(' ')[0] == DateTime.Now.ToString("dd/MM/yyyy") && Request.QueryString["shtim_modifikim"] != "modifikim")
                fatureUbl = clsFunksioneFiskalizimi.gjeneroFatureUBL(nderm, kokeShitje.NrDok, viti, kokeShitje.DtDok.ToString(), kokeShitje.DtMaturimi.ToString(), txtIIC.Text, iicSignature, nivfFature, Convert.ToDateTime(kokeShitjeje.DtDok.ToString().Split(' ')[0] + ' ' + dtKrijimiPajisjeOffset.UtcDateTime.ToString().Split(' ')[1]), operatori.ItemArray[0].ToString(), degeAdministrative["KODNJESIEBIZNES"].ToString(), kodSoftueri, kodMonedha, nderm.NdermarrjeNipt, nderm.NdermarrjePershkrimi, nderm.NdermarrjeVendi, qytetiNdermarrje.KodiQyteti, "AL", nderm.NdermarrjeNipt, kokeShitje.Totali, Decimal.Parse(kokeShitje.Totali.ToString()), (kokeShitje.Totali - kokeShitje.Tvsh).ToString(), 20.00, "", "", 1, kf.EmertimiKF, kf.NiptiKF, adresKlienti, qytetKlienti.KodiQyteti, kodShtetiKlienti, "CASH", txtVlefte.Text, kokeShitje.Totali, 100.00, kodTVSH, arsyeKodTvsh, zbritjeTotale, kokeShitje.PerqindjeZbritje, trupShitje, 0.00, 0.00, kodTipiEinvoice, kodProcesi, kursi, kokeShitje.DtFillimi.ToString(), kokeShitje.DtMbarimi.ToString(), kokeShitje.DtDok.ToString(), IdPerdoruesi, IdNdermarrja, Request.QueryString["scopeId"].ToString(), kf, kokeShitje.Pershkrimi, kokeShitje.Shenime2);
            else
                fatureUbl = clsFunksioneFiskalizimi.gjeneroFatureUBL(nderm, kokeShitje.NrDok, viti, kokeShitje.DtDok.ToString(), kokeShitje.DtMaturimi.ToString(), txtIIC.Text, iicSignature, nivfFature, Convert.ToDateTime(kokeShitjeje.DtDok.ToString().Split(' ')[0] + ' ' + dtKrijimiPajisjeOffset.UtcDateTime.ToString().Split(' ')[1]), operatori.ItemArray[0].ToString(), degeAdministrative["KODNJESIEBIZNES"].ToString(), kodSoftueri, kodMonedha, nderm.NdermarrjeNipt, nderm.NdermarrjePershkrimi, nderm.NdermarrjeVendi, qytetiNdermarrje.KodiQyteti, "AL", nderm.NdermarrjeNipt, kokeShitje.Totali, Decimal.Parse(kokeShitje.Totali.ToString()), (kokeShitje.Totali - kokeShitje.Tvsh).ToString(), 20.00, "", "", 1, kf.EmertimiKF, kf.NiptiKF, adresKlienti, qytetKlienti.KodiQyteti, kodShtetiKlienti, "CASH", txtVlefte.Text, kokeShitje.Totali, 100.00, kodTVSH, arsyeKodTvsh, zbritjeTotale, kokeShitje.PerqindjeZbritje, trupShitje, 0.00, 0.00, kodTipiEinvoice, kodProcesi, kursi, kokeShitje.DtFillimi.ToString(), kokeShitje.DtMbarimi.ToString(), kokeShitje.DtDok.ToString(), IdPerdoruesi, IdNdermarrja, Request.QueryString["scopeId"].ToString(), kf, kokeShitje.Pershkrimi, kokeShitje.Shenime2);
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("W. Europe Standard Time");
            var dtKrijimiOffset = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZone);
            var mesazhEInvoice = clsFunksioneFiskalizimi.gjeneroMesazhEInvoice(nderm, fatureUbl[0], dtKrijimiOffset);
            if (WebConfigurationManager.AppSettings["urlEinvoice"] == "https://einvoice.tatime.gov.al/EinvoiceService-v1/EinvoiceService.wsdl")
                EIC = clsFunksioneFiskalizimi.InvokeService(mesazhEInvoice, "EIC", true);
            else
                EIC = clsFunksioneFiskalizimi.InvokeService(mesazhEInvoice, "ns2:EIC", true);
            EIC[3] = fatureUbl[1];
            return EIC;



        }
        private object ktheObjektPerNotify(clsKokaShitje kokaShitje, bool einvoice, string statusi, colTrupiShitje artikujt, clsTrupiShitje trupiShitje, string pershkrim, string kodErrori, string TipFature, string xml, string xmlResponse)
        {
            string emerDatabaze = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar();
            clsKokaShitje kokeShitje = new clsKokaShitje(kokaShitje.IdShitjeKoka);
            clsDegeAdministrative degeAdministrative = new clsDegeAdministrative(kokeShitje.IdDegeAdministrative);
            string kodiINjesiseSeBiznesit = clsDegeAdministrative.ktheDegeAdministrativeSipasiD(kokeShitje.IdDegeAdministrative)["KODNJESIEBIZNES"].ToString();
            clsKlientFurnitor klienti = new clsKlientFurnitor(kokeShitje.IdKlientFurnitor);
            string urlFiskalizimiApp = WebConfigurationManager.AppSettings["urlFiskalizimiApp"];
            clsNdermarrje nderm = new clsNdermarrje(kokaShitje.IdNdermarrje);
            string data = kokaShitje.DtKrijimiPajisje.ToString("yyyy-MM-ddThh:mm:sszzz");
            urlFiskalizimiApp = urlFiskalizimiApp + kokaShitje.IIC + "&tin=" + nderm.NdermarrjeNipt + "&crtd=" + data.Replace("+", "%2B") + "&prc=" + kokaShitje.Totali + "";
            clsNdermarrje ndermarrje = new clsNdermarrje(kokeShitje.IdNdermarrje);
            return new
            {
                docNo = kokaShitje.NrDok,
                issuer = new
                {
                    nuis = ndermarrje.NdermarrjeNipt,
                    organization = emerDatabaze,
                    name = ndermarrje.NdermarrjeKodi

                },

                linkUrl = urlFiskalizimiApp,
                message = pershkrim,
                status = statusi,
                receiver = new
                {
                    nuis = klienti.NiptiKF,
                    name = klienti.EmertimiKF
                },
                type = TipFature,
                xml = new
                {
                    request = xml,
                    Response = xmlResponse
                }

            };
        }
        protected void UcDocumentEinvoice_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
            var connString = MyConnectionsManager.GetSelectedConNameServer();
            string uploadDirectory = "~/DocumentEinvoice/";

            if (!Directory.Exists(MapPath(uploadDirectory)))
            {
                Directory.CreateDirectory(MapPath(uploadDirectory));
            }

            uploadDirectory += $"{connString}/{IdNdermarrja}/{Request.QueryString["scopeID"]}/";

            if (!Directory.Exists(MapPath(uploadDirectory)))
            {
                Directory.CreateDirectory(MapPath(uploadDirectory));
            }

            int i;
            for (i = 0; i < UcDocumentEinvoice.UploadedFiles.Length; i++)
            {
                string filename = System.IO.Path.GetFileName(UcDocumentEinvoice.UploadedFiles[i].FileName);
                if (!File.Exists(uploadDirectory + filename))
                {
                    UcDocumentEinvoice.SaveAs(MapPath(uploadDirectory) + filename);

                }
            }

        }
        protected void upload_Clilck(object sender, EventArgs e)
        {
            DbCore.DbAdmin.clsNdermarrje ndermarrje = new DbCore.DbAdmin.clsNdermarrje(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            var path = DbCore.mySessionObjects.merrPathNgaSesioni(Session) == null ? ndermarrje.Pathname : DbCore.mySessionObjects.merrPathNgaSesioni(Session);

            if (path == null)
                return;
        }
    }
}