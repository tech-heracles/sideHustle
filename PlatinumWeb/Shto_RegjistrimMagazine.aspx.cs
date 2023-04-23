using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Web.UI;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbAsete;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbQendraKosto;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DevExpress.Web;
using Newtonsoft.Json;
using PlatinumWeb.ApplicationUtils.Pages;
using DbCore.IMBUtils.Extensions;
using PlatinumWeb.ApplicationUtils.ASPxControlExtensions;
using System.Web;
using AlphaWebCommon.WebUtils.ASPxControlExtensions;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Types;
using CacheLayer;
using PlatinumWeb.ApplicationUtils;
using PlatinumWeb.ApplicationUtils.ASPxControlUtils;
using DbCore.IMBUtils.Fiskalizimi.API;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using System.Web.Configuration;
using DbCore.DbArkaBanka;

namespace PlatinumWeb
{
    public partial class Shto_RegjistrimMagazine : MyPageBase
    {
        private static string pershkrimDaljeFK = "Nga daljet e magazinës";
        private static string pershkrimHyrjeFK = "Nga hyrjet e magazinës";
        private static string STR_zgjidhniDtRegj = "Zgjidhni nje datë regjistrimi!";

        private colTrupiMagazina trupat = new colTrupiMagazina();
        private string kushtMerrMagMeAutorizim = string.Empty;
        private string guidString;
        bool eshteOwn;


        protected void Page_Load(object sender, EventArgs e)
        {
            int idPerdoruesi;
            int idNdermarrje;
            int idViti;
            int idGjuha;
            int idnderviti;
            if (hfState.Count == 0)
            {
                if (!mySessionObjects.isLogedIn(Session))
                    clsFunksione.logout(Session, true, "FaqePaautorizuar");

                idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                if (mySessionObjects.ktheKodNdermarrje(Session) == null)
                {
                    Response.Redirect("Login_Ndermarrje.aspx?id=" + idPerdoruesi);
                    return;
                }
                idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
                idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
                eshteOwn = mySessionObjects.merrEshteOwnSesioni(Session);
                idGjuha = mySessionObjects.ktheGjuhe(Session);
                idnderviti = mySessionObjects.ktheNdermarrjeVit(Session);
                guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                hfState.Set("idPerdoruesi", idPerdoruesi);
                hfState.Set("idNdermarrje", idNdermarrje);
                hfState.Set("idViti", idViti);
                hfState.Set("OwnShop", eshteOwn);
                hfState.Set("idGjuha", idGjuha);
                hfState.Set("GjeneruarNgaMema", false);
                hfState.Set("HyrjeGjeneruarNgaMema", false);
                hfState.Set("idNderviti", idnderviti);
                hfState.Set("arsyeReloadPyetje", false);
                hfState.Set("MNSA", true);
                hfState.Set("LMDET", true);
                hfState.Set("IdStatusDok", 0);
                hfState.Set("guidString", guidString);
                hfState.Set("vleraDefaultKlonimi", false);
                AspxWebControlUtils.perkthePopUp(popMesazhQK, rm.GetString("labelAdministrimiKujdes", ci), lblMsgbox4, rm.GetString("msgDeshironiTeBeniShperndarjenNeQendratEKostos", ci), ButtonCancelQK, rm.GetString("btnJO", ci), ButtonOkQK, rm.GetString("btnPO", ci));
                
            }
            else
            {
                idPerdoruesi = (int)hfState["idPerdoruesi"];
                idNdermarrje = (int)hfState["idNdermarrje"];
                idViti = (int)hfState["idViti"];
                idGjuha = (int)hfState["idGjuha"];
                idnderviti = (int)hfState["idNderviti"];
                guidString = hfState.Get<string>("guidString");
                eshteOwn = hfState.Get<bool>("OwnShop");
            }

            konfigGrid(idNdermarrje, idGjuha);
            mbushComboTipi();
            mbushComboTransaksion();
            var periudha = mySessionObjects.merrPeriudheKontabel(Session);
            if (periudha != null)
            {
                btnPeriudha.Text = periudha.NrPeriudha.ToString();
                lblPeriudhaAktuale.Text = string.Format("{0}-{1}", periudha.FillimiPeriudha.ToShortDateString(), periudha.MbarimiPeriudha.ToShortDateString());
            }


            if (!IsPostBack)
            {
                hfState.Set("kushtTransferim", null);
                hfState.Set("kushtKonfirmim", null);
                ucEmerSkedari.ValidationSettings.MaxFileSize = clsFunksione.merrMaxFileSizePerImport();

                var per = new clsPerdorues(idPerdoruesi);
                hfHapurMbyllur.Value = per.InfoHapur.ToString();

                if (hfShtimModifikim.Value == string.Empty)
                {
                    MbushHiddenFieldMePerkthime(ci, rm);
                    switch (Request.QueryString["shtim_modifikim"])
                    {
                        case "shtim":
                            hfShtimModifikim.Value = "shtim";
                            KonfiguroVleraFillestareShto(idGjuha, idNdermarrje, rm, ci);
                            break;
                        case "shtimraport":
                            hfShtimModifikim.Value = "shtimraport";
                            KonfiguroVleraFillestareShto(idGjuha, idNdermarrje, rm, ci);
                            break;
                        case "rezervim":
                            hfShtimModifikim.Value = "rezervim";
                            konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, idnderviti, rm, ci);
                            break;
                        case "klonim":
                            hfShtimModifikim.Value = "klonim";
                            konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, idnderviti, rm, ci);
                            break;
                        case "inventarizim":
                            hfShtimModifikim.Value = "inventarizim";
                            konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, idnderviti, rm, ci);
                            break;
                        case "konvertim":
                            hfShtimModifikim.Value = "konvertim";
                            konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, idnderviti, rm, ci);
                            break;
                        default:
                            hfShtimModifikim.Value = "modifikim";
                            konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, idnderviti, rm, ci);
                            break;
                    }
                    percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
                }
               
                else if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "shtimraport")
                    KonfiguroVleraFillestareShto(idGjuha, idNdermarrje, rm, ci);
                else if (hfShtimModifikim.Value == "modifikim")
                    konfiguroVleraFillestareModifiko(idGjuha, idPerdoruesi, idViti, idNdermarrje, idnderviti, rm, ci);
                if (hfShtimModifikim.Value == "shtimraport")
                    MbushTeDhenaSipasRaportit();
                kushtMerrMagMeAutorizim = clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "MAGDESTAUTOR");
                hfState.Set("merrMagazinatMeAutorizim", kushtMerrMagMeAutorizim);
                var colMagazinatPara = colNjesiAdministrative.ktheTreNjesiteEParaAdministrativeAktiveMeLloj(idNdermarrje, idPerdoruesi, kushtMerrMagMeAutorizim != "Jo");

                hfTmpColMag.Value = JsonConvert.SerializeObject(colMagazinatPara);
                var tedrejtaInfo = new clsTeDrejtaRoli();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Info Artikulli");
                hfTeDrejtaInfoArt.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Konfigurime Gride");
                hfTeDrejtaKonfGride.Value = tedrejtaInfo.DAmb.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "Importo seriale");
                hfTeDrejtaImportoSeriale.Value = tedrejtaInfo.DAmb.ToString();

                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaArtikullShpejte.aspx");
                hfTeDrejtaArtRi.Value = tedrejtaInfo.DShtim.ToString();
                tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, clsFunksione.GetKomponente(Page.Request));

                hfTeDrejta.Add("Shtim", tedrejtaInfo.DShtim);
                hfTeDrejta.Add("Modifikim", tedrejtaInfo.DMod);
                hfTeDrejta.Add("ShtimDraft", tedrejtaInfo.DShtimDraft);
                hfTeDrejta.Add("ModifikimDraft", tedrejtaInfo.DModifikimDraft);
            }

            percaktoTemplateMenu(idPerdoruesi, idViti, idNdermarrje, ASPxMenu1);
            if (!(IsPostBack && Request.Form["__EVENTTARGET"] != null && Request.Form["__EVENTTARGET"] == string.Empty))
                Container.Attributes["src"] = string.Empty;
        }
        private void MbushTeDhenaSipasRaportit()
        {
            var magdest = "";

            if (Request.QueryString["konfigurim"] != null)
                cmbKonfigurimi.Value = Request.QueryString["konfigurim"];
            if (Request.QueryString["filtermagdestinacion"] != null && Request.QueryString["filtermagdestinacion"].ToString() != "")
                magdest = Request.QueryString["filtermagdestinacion"].ToString();

            DataTable dt = !String.IsNullOrWhiteSpace(Request.QueryString["PageId"]) ? mySessionObjects.merrDtNgaSessioni(Session, Request.QueryString["PageId"]) : mySessionObjects.merrDtNgaSessioni(Session);
            colTrupiMagazina col = new colTrupiMagazina();
            colArtikujt colArtikuj = new colArtikujt();
            colNjesiAdministrative colmag = new colNjesiAdministrative();
            colNjesiAdministrative colmagdest = new colNjesiAdministrative();
            colNjesiteArtikulli colnjesi = new colNjesiteArtikulli();
            colDetajimeArtikulli coldet1 = new colDetajimeArtikulli();
            colDetajimeArtikulli coldet2 = new colDetajimeArtikulli();
            colTaksa colTaksa = new colTaksa();
            col.mbushTrupMagazinaSipasRaportit(dt, (int)hfState["idNdermarrje"], (int)hfState["idPerdoruesi"], magdest, out colArtikuj, out colmag,out colmagdest, out colnjesi, out coldet2, out coldet1);
            object myCols = new { colTrupi = col };
            HfColTrupMag.Value = JsonConvert.SerializeObject(col);
            colKodbare colKodbare = new colKodbare();
            colKategoriShpenzimi colKategori = new colKategoriShpenzimi();
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti(Convert.ToInt32(cmbKonfigurimi.Value));
            HfKonfAmb.Value = JsonConvert.SerializeObject(konf);
            HfColArt.Value = JsonConvert.SerializeObject(colArtikuj);
            HfColKodbare.Value = JsonConvert.SerializeObject(colKodbare);
            colLlogarite colLlogari = new colLlogarite();
            var colartset = col.ktheColArtikujSet();
            HfColArtSet.Value = JsonConvert.SerializeObject(colartset);
             HfColDetArt.Value = JsonConvert.SerializeObject(coldet1);
            HfColDetArt2.Value = JsonConvert.SerializeObject(coldet2);
            HfColNjesAdminis.Value = JsonConvert.SerializeObject(colmag);
            HfColNjesiArt.Value = JsonConvert.SerializeObject(colnjesi);
            HfColNjesAdminisDest.Value = JsonConvert.SerializeObject(colmagdest);
            hfKonffillestar.Value = konf.KodKonfigAmbjente;
        }
        public void MerrTedhenat(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, clsKokaMagazina koka, ResourceManager rm, CultureInfo ci, bool konvertim)
        {
            hfId.Value = koka.IdKokaMagazina.ToString();
            var kodNiveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(koka.IdNivel);
            bool klonim = hfShtimModifikim.Value == "klonim";
            if ((konvertim || klonim) && Request.QueryString["niveli"] != null)
                cmbLloji.Value = Request.QueryString["niveli"];
            else {
                //cmbLloji.Value = koka.IdNivel;
                cmbLloji.SelectedItem = cmbLloji.Items.FindByValue(koka.IdNivel.ToString());
            }
            mbushComboKonfigurimet(true, rm, ci, idGjuha, idNdermarrje, idPerdoruesi);
            var konf = new clsKonfigurimAmbjenti();

            if ((konvertim || klonim ) && Request.QueryString["konfigurim"] != null)
            {
                cmbKonfigurimi.Value = Request.QueryString["konfigurim"];
                konf.mbushKonfigAmbjSipasId(int.Parse(Request.QueryString["konfigurim"]), idGjuha);
            }
            else
            {
                konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente, idGjuha);
                cmbKonfigurimi.Text = konf.KodKonfigAmbjente;
            }
            cmbKonfigurimi.Value = konf.IdKonfigAmbjente.ToString();
            hfKonffillestar.Value = konf.KodKonfigAmbjente;

            if (klonim)
            {
                koka.IdKonfigAmbjente = konf.IdKonfigAmbjente;
                koka.NIVFSH = "";
                koka.WTNIC = "";
            }

            if (koka.IdKategoriSeriali > 0)
                cmbKategoriSeriali.Value = koka.IdKategoriSeriali.ToString();
            var ngjyra = clsKokaMagazina.merrNgjyreKonvertime(koka.IdNdermarrje, koka.IdKokaMagazina);
            if (ngjyra != "gri" && !string.IsNullOrEmpty(ngjyra))
                hfState.Set("mesazhKonvertuar", "Jeni i sigurte? Dokumenti " + koka.NrDok + " eshte i konvertuar.");
            else
                hfState.Set("mesazhKonvertuar", "");
            kushtMerrMagMeAutorizim = clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "MAGDESTAUTOR");
            hfState.Add("LLN", string.Empty);

            if (koka.IdKonfigGjenerues != 0)
            {
                MbushKomboGrupDokumentash(idNdermarrje, koka.IdKonfigGjenerues);
                var alt = clsAlternativaKushti.getAlternativa(koka.IdKonfigGjenerues, "LLN");
                if (alt == "Ndryshim sasie")
                    hfState.Set("LLN", "Ndryshim sasie");
            }
            else if (cmbKonfigurimi.Text.Split(';')[0] == "FHK")
            {
                var konfFb = new clsKonfigurimAmbjenti();
                konfFb.mbushKonfigAmbjSipasKod("FB", idNdermarrje);
                MbushKomboGrupDokumentash(idNdermarrje, konfFb.IdKonfigAmbjente);
            }
            else
                MbushKomboGrupDokumentash(idNdermarrje, konf.IdKonfigAmbjente);

            if (koka.IdKlientFurnitor != 0)
                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(btneKlientFurnitori, koka.IdKlientFurnitor);
            var idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 510, "btneKlientFurnitori", -1, false);
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            var formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            hfState.Set("formatMonedhe", JsonConvert.SerializeObject(formatMonedhe));
            cmbLlogariKunderParti.Text = new clsLlogari(koka.IdLlogari).NrLlogari;

            if (koka.IdMagazina != 0)
            {
                var idMag = Convert.ToInt32(koka.IdMagazina);
                ConfigureAspxComboBox.mbushComboMagazinatMeID(idNdermarrje, btneMagazina, idPerdoruesi, true, 0, true, idMag);
                txtPershkrimMagazine.Text = clsNjesiAdministrative.kthePershkrimNjesiAdministrativeSipasiDPaAutorizime(koka.IdMagazina);
            }

            var kokatra = new clsKokaMagazina();
            kokatra.mbushKokaMagazinaSipasIDGjenerues(koka.IdKokaMagazina, 1, konf.IdKonfigAmbjente);
            cmbFormatiPrintimit.Value = koka.IdRaportDesing.ToString();

            if (koka.IdAutomjet != 0)
            {
                ConfigureAspxComboBox.mbushComboAutomjetiByID(btneAutomjeti, koka.IdAutomjet);
                txtTarga.Text = (clsAutomjete.ktheTargeAutomjetSipasId(koka.IdAutomjet));
            }

            if (konf.KodKonfigAmbjente != "FDNV" && konf.KodKonfigAmbjente != "FHNV")
            {
                if (kokatra.IdKokaMagazina > 0)
                {
                    int idMag = Convert.ToInt32(kokatra.IdMagazina);
                    ConfigureAspxComboBox.mbushComboMagazinatMeID(idNdermarrje, btneMagazina2, idPerdoruesi, true, 0, false, idMag);
                }
            }
            else
            {
                int idMag = Convert.ToInt32(koka.IdNjesiVartese);
                ConfigureAspxComboBox.mbushComboNjesiVarteseMeID(idNdermarrje, true, btneMagazina2, idMag);
            }

            if (koka.IdDegeAdministrative != 0)
            {
                cmbDegeAdministrative.Value = koka.IdDegeAdministrative.ToString();
                txtPershkrimDege.Text = clsDegeAdministrative.mbushPershkrimDegeAdministrativeSipasiD(koka.IdDegeAdministrative);
            }

            if (koka.IdOperator != 0)
                cmbOperatori.Value = koka.IdOperator.ToString();
            if (koka.Transportuesi != 0)
            {
                btnTransportuesi.Text = clsTransportues.merrEmertimTransportuesSipasId(koka.Transportuesi);
            }
            txtNrDok.Text = koka.NrDok;
            dteDtDok.Date = koka.DtDok;
            dteDtTransporti.Date = koka.DtTransporti;
            dteDtRegjistrimi.Date = koka.DtRegjistrimi;
            txtNrProjekti.Text = koka.NrProjekt;
            cbMeKonfirmim.Checked = koka.MeKonfirmim;
            VendosVleraComboGrupimi(koka.IdGrup1,koka.IdGrup2, koka.IdGrup3, konf.IdKonfigAmbjente);
            txtShenime.Text = koka.Shenime;
            txtVlefta.Text = koka.Vlefta.ToString();
            txtPershkrimi.Text = koka.Pershkrimi;
            txtMagazinieri.Text = koka.Magazinieri;
            txtadresa.Text = koka.Adresa;
            txtShoferi.Text = koka.Shoferi;
            txtTarga2.Text = new clsTransportues(koka.Transportuesi).Targa;
            txtNrSerial.Text = koka.NrSerial;
            txtNIVFSH.Text = koka.NIVFSH;
            txtWTNIC.Text = koka.WTNIC;
            if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
            {
                if(txtNIVFSH.Text != "")
                    cbFiskalizo.Checked = true;
                cbMallraTeDjegshme.Checked = koka.MallraTeDjeghsme;
                cbShoqerimIKerkuar.Checked = koka.ShoqerimIKerkuar;
                cmbTransaksioni.Text = koka.Transaksioni;
                cmbTipiMag.Text = koka.Tipi;
            }
            
            hfState.Set("IdStatusDok", koka.IdStatusDok);
            if (hfShtimModifikim.Value == "modifikim")
            {
                var dtlidhur = koka.merrIdsDokLidhur();
                hfLidhur.Value = (dtlidhur.Rows.Count != 0).ToString();
                if (eshteOwn && dtlidhur.Rows.Count == 1)
                {
                    int idGjenerues = 0, idkonfigambjente = 0;
                    if (dtlidhur.Columns.Contains("idgjenerues"))
                        idGjenerues = Convert.ToInt32(dtlidhur.Rows[0]["idgjenerues"]);
                    if (dtlidhur.Columns.Contains("idkonfigambjente"))
                        idkonfigambjente = Convert.ToInt32(dtlidhur.Rows[0]["idkonfigambjente"]);
                    if (idGjenerues > 0 && clsKokaShitje.eshteGjeneruarDokumentiNgaNdermarrjaMeme(idGjenerues, idkonfigambjente))
                    {
                        if (koka.IdStatusDok == 0)
                        {
                            hfState.Set("GjeneruarNgaMema", true);
                            dtlidhur.Rows.Clear();
                            hfLidhur.Value = bool.FalseString;
                        }
                        else
                        {
                            dtlidhur.Rows[0]["tipi"] = "transferuarmeme";
                        }
                    }
                }

                if (dtlidhur.Rows.Count > 0 && koka.IdLlojDokumentiMagazine == 1 && clsKokaMagazina.eshteDokumentHyrjeTransferimiVod(koka.IdKokaMagazina))
                {
                    var dalja = new clsKokaMagazina(koka.IdGjenerues);
                    if (clsAlternativaKushti.getAlternativa(dalja.IdKonfigAmbjente, "TSD1") == "Po")
                        hfState.Set("HyrjeGjeneruarNgaMema", true);
                }

                var autorizimet = true;
                if (hfShtimModifikim.Value != "klonim" && hfShtimModifikim.Value != "konvertim" && hfShtimModifikim.Value != "konvertimblerje")
                    autorizimet = clsKokaMagazina.kaAutorizime(koka.IdKokaMagazina, idPerdoruesi);
                hfAutorizimi.Value = autorizimet.ToString();
                if (!autorizimet)
                    hfLidhur.Value = "True";

                if (koka.IdStatusDok == 1 && clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "LMD") == "Jo")
                    hfLidhur.Value = "True";

                if (clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "DOKMEKONF") == "Po" && kokatra.IdStatusDok == 1)
                    hfLidhur.Value = "True";
                AspxWebControlUtils.ShtoLidhje(idPerdoruesi, idViti, idNdermarrje, hl, dtlidhur, koka.IdGjenerues, koka.IdNivelGjenerues, koka.IdKonfigGjenerues, idGjuha);
            }
            var alternativaKushtTransferim = clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "DMT");
            hfState.Set("kushtTransferim", alternativaKushtTransferim == string.Empty ? null : alternativaKushtTransferim);

            hfState.Set("kushtKonfirmim", clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "DOKMEKONF"));

            var serialeUnike = new colSerialeUnikeMagazina(koka.IdKokaMagazina, idNdermarrje);
            hfState.Set("KaSeriale", serialeUnike.Count > 0);
            mySessionObjects.RuajNeSession(Session, serialeUnike, Constants.SERIALE_UNIKE_TE_NGARKUAR, guidString);
        }

        public void MerrTedhenatNgaShitja(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, clsKokaShitje koka)
        {
            var kodNiveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(koka.IdNivel);
            if (Request.QueryString["niveli"] != null)
                cmbLloji.Value = Request.QueryString["niveli"];
            else
                cmbLloji.Text = kodNiveli;

            mbushComboKonfigurimet(true, rm, ci, idGjuha, idNdermarrje, idPerdoruesi);
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente, idGjuha);
            if (Request.QueryString["konfigurim"] != null)
            {
                cmbKonfigurimi.Value = Request.QueryString["konfigurim"];
                konf.mbushKonfigAmbjSipasId(int.Parse(Request.QueryString["konfigurim"]), idGjuha);
            }
            else

                if (koka.IdKonfigAmbjente != 0)
                cmbKonfigurimi.Text = konf.KodKonfigAmbjente;

            hfKonffillestar.Value = konf.KodKonfigAmbjente;
            kushtMerrMagMeAutorizim = clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "MAGDESTAUTOR");

            MbushKomboGrupDokumentash(idNdermarrje,
                koka.IdKonfigGjenerues != 0 ? koka.IdKonfigGjenerues : konf.IdKonfigAmbjente);
            if (koka.IdAutomjet != 0)
            {
                ConfigureAspxComboBox.mbushComboAutomjetiByID(btneAutomjeti, koka.IdAutomjet);
                txtTarga.Text = (clsAutomjete.ktheTargeAutomjetSipasId(koka.IdAutomjet));
            }
            if (koka.IdKlientFurnitor != 0)
                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(btneKlientFurnitori, koka.IdKlientFurnitor);
            var idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(idGjuha, konf.IdKonfigAmbjente, idNdermarrje, 510, "btneKlientFurnitori", -1, false);
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(konf.IdKonfigAmbjente);
            var formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            if (koka.IdKategoriSeriali > 0)
                cmbKategoriSeriali.Value = koka.IdKategoriSeriali.ToString();
            hfState.Set("formatMonedhe", JsonConvert.SerializeObject(formatMonedhe));
            if (koka.IdDegeAdministrative != 0)
                cmbDegeAdministrative.Value = koka.IdDegeAdministrative.ToString();
            
            txtNrDok.Text = koka.NrDok;
            dteDtDok.Date = koka.DtDok;
            dteDtTransporti.Date = koka.DtTransportimi;
            dteDtRegjistrimi.Date = koka.DtRegjistrimi;
            txtNrProjekti.Text = koka.NrProjekt;
            VendosVleraComboGrupimi(koka.IdGrup1, koka.IdGrup2, koka.IdGrup3, konf.IdKonfigAmbjente);

            txtShenime.Text = koka.Pershkrimi;
            txtPershkrimi.Text = koka.Pershkrimi;
            txtadresa.Text = koka.AdresaDergimit;
            txtShoferi.Text = koka.Shoferi;
            txtTarga2.Text = new clsTransportues(koka.IdTransportues).Targa;
            txtVlefta.Text = "1";
        }

        private void VendosVleraComboGrupimi(int idGrup1, int idGrup2, int idGrup3, int idKonfig)
        {
            bool klonim = hfShtimModifikim.Value == "klonim";
            if (idGrup1 != 0 && (!klonim || clsGrupimDokumentiKoka.ekzistonGrupiPerKeteLlojDok(idGrup1, idKonfig)))
                cmbGrup1.Value = idGrup1.ToString();
            if (idGrup2 != 0 && (!klonim || clsGrupimDokumentiKoka.ekzistonGrupiPerKeteLlojDok(idGrup2, idKonfig)))
                cmbGrup2.Value = idGrup2.ToString();
            if (idGrup3 != 0 && (!klonim || clsGrupimDokumentiKoka.ekzistonGrupiPerKeteLlojDok(idGrup3, idKonfig)))
                cmbGrup3.Value = idGrup3.ToString();
        }

        public colTrupiMagazina MerrTedhenatInvent(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, int idnderviti, ResourceManager rm, CultureInfo ci)
        {
            const string kodNiveli = "FD";
            if (Request.QueryString["niveli"] != null)
                cmbLloji.Value = Request.QueryString["niveli"];
            else
                cmbLloji.Text = kodNiveli;
            mbushComboKonfigurimet(true, rm, ci, idGjuha, idNdermarrje, idPerdoruesi);
            var konf = new clsKonfigurimAmbjenti();
            if (Request.QueryString["konfigurim"] != null)
            {
                cmbKonfigurimi.Value = Request.QueryString["konfigurim"];
                konf.mbushKonfigAmbjSipasId(int.Parse(Request.QueryString["konfigurim"]), idGjuha);
            }
            else
                cmbKonfigurimi.Text = "FD";
            hfKonffillestar.Value = konf.KodKonfigAmbjente;
            MbushKomboGrupDokumentash(idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));
            var idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 510, "btneKlientFurnitori", -1, false);
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            var formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            hfState.Set("formatMonedhe", JsonConvert.SerializeObject(formatMonedhe));
            if (Request.QueryString["mag"] != null)
                if (Request.QueryString["tab"] == "2")
                    btneMagazina2.Value = Request.QueryString["mag"];
                else
                     ConfigureAspxComboBox.mbushComboMagazinatMeID(idNdermarrje, btneMagazina, idPerdoruesi, true, 0, true, Convert.ToInt32(Request.QueryString["mag"]));

            dteDtDok.Date = DateTime.Parse(Request.QueryString["dtdok"]);
            dteDtTransporti.Date = DateTime.Parse(Request.QueryString["dtdok"]);
            dteDtRegjistrimi.Date = DateTime.Today;
            return KrijoColtrupiInventarizim(idNdermarrje, idnderviti);
        }

        public void MerrTedhenatRez(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, clsKokaRezervime koka, ResourceManager rm, CultureInfo ci)
        {
            var kodNiveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(koka.IdNivel);
            if (Request.QueryString["niveli"] != null)
                cmbLloji.Value = Request.QueryString["niveli"];
            else
                cmbLloji.Text = kodNiveli;

            mbushComboKonfigurimet(true, rm, ci, idGjuha, idNdermarrje, idPerdoruesi);
            var konf = new clsKonfigurimAmbjenti();
            konf.mbushKonfigAmbjSipasId(koka.IdKonfigAmbjente, idGjuha);
            if (Request.QueryString["konfigurim"] != null)
            {
                cmbKonfigurimi.Value = Request.QueryString["konfigurim"];
                konf.mbushKonfigAmbjSipasId(int.Parse(Request.QueryString["konfigurim"]), idGjuha);
            }
            else
                if (koka.IdKonfigAmbjente != 0)
                cmbKonfigurimi.Text = konf.KodKonfigAmbjente;// + ";" + konfig.PershkrimKonfigAmbjente

            hfKonffillestar.Value = konf.KodKonfigAmbjente; // + ";" + konf.PershkrimKonfigAmbjente;

            MbushKomboGrupDokumentash(idNdermarrje,
                koka.IdKonfigGjenerues != 0 ? koka.IdKonfigGjenerues : Convert.ToInt32(cmbKonfigurimi.Value));

            if (koka.IdKlientFurnitor != 0)
                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(btneKlientFurnitori, koka.IdKlientFurnitor);
            var idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 510, "btneKlientFurnitori", -1, false);
            var formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            var formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            hfState.Set("formatMonedhe", JsonConvert.SerializeObject(formatMonedhe));
            if (koka.IdMagazina != 0)
            {
                var idMag = Convert.ToInt32(koka.IdMagazina);
                ConfigureAspxComboBox.mbushComboMagazinatMeID(idNdermarrje, btneMagazina, idPerdoruesi, true, 0, true, idMag);
            }
            if (koka.IdDegeAdministrative != 0)
                cmbDegeAdministrative.Value = koka.IdDegeAdministrative.ToString();
            txtNrDok.Text = koka.NrDok;
            dteDtDok.Date = koka.DtDok;
            dteDtTransporti.Date = koka.DtDok;
            dteDtRegjistrimi.Date = koka.DtRegjistrimi;
            txtShenime.Text = koka.Shenime;
        }

        private void MbushKomboGrupDokumentash(int idNdermarrje, int idKonfigAmbjente)
        {
            var colGrupet = new colGrupimDokumentiKoka(idNdermarrje, new List<int> { 1, 2, 3 }, idKonfigAmbjente, mySessionObjects.ktheIdPerdoruesi(Session));
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup1);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup1, colGrupet, 1);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup2);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup2, colGrupet, 2);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup3);
            ConfigureAspxComboBox.mbushComboGrupeDokumentashSipasKonfigurimit(cmbGrup3, colGrupet, 3);
        }

        private colTrupiMagazina KrijoColtrupiInventarizim(int idNdermarrje, int idnderviti)
        {
            var coltrupi = new colTrupiMagazina();
            var kodbaret = new List<string>();
            if (Request.QueryString["tab"] != null)
            {
                colKrahasimInventarizimics colkrah = new colKrahasimInventarizimics();
                switch (Request.QueryString["tab"])
                {
                    case "0":
                        colkrah = mySessionObjects.MerrNgaSession<colKrahasimInventarizimics>(Session, "KrahasimInventarizimi.aspx?lloj=" + Request.QueryString["llojinv"] + "gvEkzistuese" + idnderviti);
                        break;
                    case "1":
                        colkrah = mySessionObjects.MerrNgaSession<colKrahasimInventarizimics>(Session, "KrahasimInventarizimi.aspx?lloj=" + Request.QueryString["llojinv"] + "gvPerbashket" + idnderviti);
                        break;
                    case "2":
                        colkrah = mySessionObjects.MerrNgaSession<colKrahasimInventarizimics>(Session, "KrahasimInventarizimi.aspx?lloj=" + Request.QueryString["llojinv"] + "gvMagTjeter" + idnderviti);
                        break;
                }

                if (Request.QueryString["llojinv"] == "agj")
                {
                    var njesi = new clsNjesiAdministrative(Request.QueryString["mag"], idNdermarrje);
                    var col = new colNjesiAdministrative();
                    var trupaSipasSeriali = colkrah.GroupBy(x => x.Kodi);
                    var sipasSeriali = trupaSipasSeriali as IList<IGrouping<string, clsKrahasimInventarizimi>> ?? trupaSipasSeriali.ToList();
                    for (int a = 0, count = sipasSeriali.Count; a < count; a++)
                    {
                        var art = new clsArtikulli();
                        art.mbushArtikull(sipasSeriali.ElementAt(a).Key, idNdermarrje);
                        var sasi = (sipasSeriali.ElementAt(a)).Sum(x => x.Diferenca);
                        var sasiprg = (sipasSeriali.ElementAt(a)).Sum(x => x.SasiaPrg);
                        var tr = new clsTrupiMagazina(0, 0, 1, art.IdArtikulli, art.KodArtikulli, art.PershkrimArtikulli, art.Njesi1Artikulli, (Request.QueryString["tab"] == "2") ? Convert.ToDouble(sasiprg) : Convert.ToDouble(sasi), 1, (Request.QueryString["tab"] == "2") ? Convert.ToDouble(sasiprg) : Convert.ToDouble(sasi), 1, -1, 0, 0, sipasSeriali.ElementAt(a).ElementAt(0).Magazina, dteDtDok.Date, 1, 1, -1, 0, 0, -1, 0, 0, 0, 0, 0, 0, art, string.Empty, 0, 0);
                        coltrupi.Add(tr);
                        col.Add(njesi);

                        var colzgjedhur = new colAQTSeriale();
                        for (var i = 0; i < sipasSeriali.ElementAt(a).Count(); i++)
                        {
                            var serial = new clsAQTSeriale();
                            serial.merrAQTSerialSipasKodAQT(sipasSeriali.ElementAt(a).ElementAt(i).Seriali, idNdermarrje);
                            colzgjedhur.Add(serial);
                        }
                        hfSeriale.Set(art.IdArtikulli + "_" + (a + 1), JsonConvert.SerializeObject(colzgjedhur));
                        hfSasiSeriale.Set(art.IdArtikulli + "_" + (a + 1), 1);
                    }
                    if (Request.QueryString["tab"] == "2") HfColNjesAdminisDest.Value = JsonConvert.SerializeObject(col);
                }
                else
                foreach (var krah in colkrah)
                    if (krah.Diferenca != 0)
                    {
                        kodbaret.Add(krah.Barkodi);
                        var art = new clsArtikulli();
                        art.mbushArtikull(krah.Kodi, idNdermarrje);
                        var tr = new clsTrupiMagazina(0, 0, 1, art.IdArtikulli, krah.Kodi, krah.Pershkrimi, art.Njesi1Artikulli, Convert.ToDouble(krah.Diferenca), 1, Convert.ToDouble(krah.Diferenca), 1, -1, 0, 0, int.Parse(btneMagazina.Value.ToString()), dteDtDok.Date, 1, 1, -1, 0, 0, -1, 0, 0, 0, 0, 0, 0, art, string.Empty, 0, 0);
                        coltrupi.Add(tr);
                    }
            }
            HfKodbari.Value = JsonConvert.SerializeObject(kodbaret);
            return coltrupi;
        }

        /// <summary>
        /// Metode qe sherben per te kaluar perkthime te js
        /// </summary>
        /// <param name="ci">Merr culture info</param>
        /// <param name="rm">Merr Resource Manager</param>
        private void MbushHiddenFieldMePerkthime(CultureInfo cultinf, ResourceManager rm)
        {
            ucEmerSkedari.ValidationSettings.MaxFileSizeErrorText = MessagesResource.Messages["msgSkedarMbi10mb"];

            clsFunksione.ShtoPerkthimNeHfState(hfState, "labelAdministrimiMsgJeniSigurt", "MenuKokeDokumenti", "artikujSetProblemeMeRecepturatSasite", "artikujSetProblemeMeRecepturat", "msgSerialet1ArtDoTeFshihen1", "regjisDokZgjidhDokPerTeBashkengjitur", "JQgridShtoArtikullAqt", "msgDokNukMundTeKonvertohet", "headerPopUpZgjidhArtikullin", "msgNdodhiGabimGjateMarrjesSeTargesSeAutomjetit", "msgNdodhiNjeGabimGjateMarrjesSeMonedhes", "msgShtoArtikull", "msgZgjidhPeriudheKontabel", "msgZgjidhiniKonfiguriminEInfosSeArtikullit", "msgKaArtikujPaNjesi", "msgTrupiDokumentitNukDuhetLeneBosh", "msgNukKeniAutorizimPerTeRuajturKeteDok", "msgPlotesoniDetajimin2EArtikullit", "msgPlotesoniDetajiminEArtikullit", "msgDeshironiShperndQendraKostoMag", "msgNukLejohetCmimZeroNeGride", "msgShperndarjeNeQendratEKostos", "msgJepniKF", "msgVendosniNjesineVartese", "msgZgjidhniNjeDateRegjstrimi", "msgZgjidhniNjeDateDokumenti", "msgShenoniLlogarine", "msgZgjidhniLlojin", "MenuTrupDokumenti", "MenuFundDokumenti", "msgSasiaDuhetNumer", "msgDetajimiVendosurNukEkzistonDoniTaCelni", "msgKodiDateSkadenceDuhetFormat", "msgSerialiVendosurNukITakonBlerjes", "msgDetajimiNukEkziston", "msgArtikulliEshteInaktiv", "msgNukKryhenVeprimeMeArtikujTePastokueshem", "msgNukMundTeKryeniVeprimeMeArtikujTePerbere", "NukKryhenVeprimeMeArtikujProdhimOseProdhimNeProces", "msgNukMundTeKryeniVeprimeMeArtikujJoTePerbere", "msgKaGabimTekRezultatiInfos", "msgKaGabimTekRezultatiInfos", "msgNukMundTeKryeniVeprimeMeArtikujJoTePerbere", "msgKaGabimTekRezultatiInfos", "msgZgjidhniNjeArtikullAfatGjate", "msgZgjidhniSerialet", "msgEkzistonArtikullNeGride", "msgKjoMagazineNukEkziston", "msgSerialetEArtikullitUhoqenSepseNukIPerkasinKesajMagazines", "msgShtoDetajim", "msgNukKeniAutorizimKlonim", "msgPyetjePanjohur", "msgGabimGjateTransferimitTeTeDhenave", "msgNukMundTeBeniTransferimNeTeNjejtenMagazine", "msgZgjidhDokumentin", "msgNdodhiGabimGjateMarrjesSeTeDhenave", "msgZgjidhKF", "msgZgjidhMagazinen", "msgZgjidhNjesiVartese", "msgNukDuhetTeKeteArtikujMeCmimZero", "msgKujdesKaCmimZeroNeGride", "msgShenoniMagazinen", "msgShenoniMagazinenDestinacion", "msgNukMundTeBeniTransferimNeTeNjejtenMagazine", "msgZgjidhniLlojinEVeprimit", "msgLlojiVeprimitIPanjohur", "msgSasiaNukMundTeJeteZero", "msgSasiaNukMundTeJeteMeEVogelSeTotDetajimeve", "msgVleftaDuhetTeJeteNumer", "msgCmimiDuhetTeJeteNumer", "msgZgjidhDetajimArtikulli", "msgPoTransferohetTeDhenatShtypniPerseriRuaj", "msgShenoniNumrinEDokumentit", "msgSerialetDoTeFshihen", "lblMsgArtikulliNukEkziston", "msgDetajimMeKategoriTjeter", "msgArtikullpaDetajim", "msgLajmerimMagazineENdryshmeNeGride", "msgDetajimJoLidhur", "msgLajmerimMagazinedestinacionENdryshmeNeGride", "msgLajmerimmagdheMagazinedestinacionENdryshmeNeGride", "msgTransferimMeSasiNegative", "msgKyDokNukPerdoretPerArtAfgj", "msgZgjidhTransportuesin");
            GridUtil.perktheButonaGride(hfState, cultinf);
        }

        public void btnJo_Click(object sender, EventArgs e)
        {
            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            mySessionObjects.ruajTrupatNeSession(Session, new colTrupiMagazina());
            if (pergjigja.Text == "ruaj")
                clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgRuajtjeMeSukses", ci), pnlMesazhi);
        }

        public void btnPo_Click(object sender, EventArgs e)
        {
            CultureInfo cultinf = mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            if (pergjigja.Text == "Serialet")
            {
                ruajJoNgaMenuja(false, mySessionObjects.ktheIdPerdoruesi(Session));
                return;
            }
            trupat = mySessionObjects.merrTrupatNgaSesioni(Session);

            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["regjMagMesazhSuksesRivleresimi"]);
            string fileLogPath = Server.MapPath("~/log/log.txt");
            clsLogRivleresimInventari log = new DbCore.DbAdmin.clsLogRivleresimInventari();
            try
            {
                log = new DbCore.DbAdmin.clsLogRivleresimInventari(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session));
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                throw new MyException(MessagesResource.Messages["msgGabimGjateRuajtjesSeRivleresimitNeLog"]);
            }
            foreach (clsTrupiMagazina t in trupat)
            {
                //DbCore.DbInventari.clsArtikulli art = new DbCore.DbInventari.clsArtikulli(t.IdArtikulli);
                mesazh = clsArtikulli.rivleresimCmimiMesatar(t.IdArtikulli, clsArtikulli.ktheMetodeKostoje(t.IdArtikulli), t.IdMag, t.Data, DateTime.Today, clsArtikulli.ktheKontrollCmimiPerDetajim(t.IdArtikulli), log, cultinf, rm, (int)hfState["idNdermarrje"], (int)hfState["idPerdoruesi"]);
                if (!mesazh.Status)
                    if (pergjigja.Text == "fshi")
                    {
                        Response.Redirect("RegjistrimMagazine.aspx?lloj=" + Request.QueryString["lloj"] + "&fshi=rivleresimjo");
                        return;
                    }
                    else
                    {
                        
                        Response.Redirect("RegjistrimMagazine.aspx?lloj=" + Request.QueryString["lloj"] + "&fshi=rivleresimruajjo");
                        return;
                    }
            }

            mySessionObjects.ruajTrupatNeSession(Session, new colTrupiMagazina());
            if (mesazh.Status)
                if (pergjigja.Text == "fshi")
                {
                    Response.Redirect("RegjistrimMagazine.aspx?lloj=" + Request.QueryString["lloj"] + "&fshi=rivleresimpo");
                    return;
                }
                else {
                    Response.Redirect("RegjistrimMagazine.aspx?lloj=" + Request.QueryString["lloj"] + "&fshi=rivleresimruajpo");
                    return;
                } 
            
        }

        /// <summary>
        /// mbush menune me buttonat perkates sipas faqes
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="aSPxMenu1"> menuja ne te cilat do te shtohen kontrollet</param>
        private void percaktoTemplateMenu(int idPerdoruesi, int idViti, int idNdermarrje, ASPxMenu aSPxMenu1)
        {
            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idgjuha = mySessionObjects.ktheGjuhe(Session);
            colMenuItem menu = new colMenuItem(idgjuha);
            menu.merrMenuItemSipasKomponentesRegjistrime(idgjuha, clsFunksione.GetKomponente(Page.Request), idPerdoruesi, idNdermarrje, idViti, hfShtimModifikim.Value == "modifikim" ? false : true);

            var kokmag = new clsKokaMagazina();
            if (hfShtimModifikim.Value == "modifikim")
            {
                var id = int.Parse(Request.QueryString["id"]);
                hfArkivaDokId.Value = id.ToString();
                kokmag.mbushKokaMagazinaSipasID(id);
            }
            var kok = new clsKokaFleteKontabel();
            foreach (var m in menu)
            {
                if (m.Name != "ItemFilter" && m.Name != "ItemFrame")
                    clsToolbarConfig.ShtoMenuItem(Theme, aSPxMenu1, m);
                if (hfShtimModifikim.Value != "modifikim" && m.Name == "Klono")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if ((hfShtimModifikim.Value == "shtim" || hfLidhur.Value == "True") && m.Name == "Klono")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if (hfLidhur.Value == "True" && m.Name == "Draft")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if (hfShtimModifikim.Value != "modifikim" && m.Name == "PrintPreview")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                if (hfShtimModifikim.Value != "modifikim" && m.Name == "Fshi")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                var kushtikonfirmim = clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "DOKMEKONF");
                if (m.Name == "RefuzoDraft")
                {
                    if (clsAlternativaKushti.getAlternativa(Convert.ToInt32(cmbKonfigurimi.Value), "RD") == "Po" && hfShtimModifikim.Value == "modifikim" && kokmag.IdStatusDok == 0)
                    {
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = true;
                    }
                    else
                    {
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    }
                }
                if (m.Name == "FletaKontabel")
                    if ((hfShtimModifikim.Value != "modifikim"))
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    else
                    {
                        kok = new clsKokaFleteKontabel(int.Parse(Request.QueryString["id"]), 6);

                        if (kok.NrDukumentiKokaFleteKontabel != null)
                            aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myFaqeCelje.kontrolloTeDrejta('Shto_FleteKontabel.aspx?shtim_modifikim=modifikim&id=" + kok.IdKokaFleteKontabel + "&numur=" + kok.NrDukumentiKokaFleteKontabel + "')";
                        else
                            aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    }

                if (m.Name == "QendraKosto")
                    if ((hfShtimModifikim.Value != "modifikim"))
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    else if (kok.NrDukumentiKokaFleteKontabel != null)
                    {
                        var qend = new clsKokaQendraKosto();
                        qend.KtheKokaQKSipasIDGjeneruesDheKonfig(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);

                        if (qend.NrDok != null)
                            aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].NavigateUrl = "javascript: myButtonClickLupa.LupaUniversal_Click('" + rm.GetString("msgShperndarjeNeQendratEKostos", ci) + "','LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente + "',900,600)";
                        else
                            aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    }
                    else aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;



                if (m.Name == "Serialet")
                {
                    var tedrejtaInfo = new clsTeDrejtaRoli();
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, "LupaSerialeUnike.aspx");
                    if (!tedrejtaInfo.DShtim)
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }

                if (m.Name == "Ruaj")
                {
                    int idStatusdok = Convert.ToInt32(hfState.Get("IdStatusDok"));

                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible =  true;

                    if (kushtikonfirmim == "Po" && cmbLloji.Text == "FH" && kokmag.IdStatusDok == 4 && hfShtimModifikim.Value == "modifikim")
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    if (kushtikonfirmim == "Po" && (cmbLloji.Text == "FH") && kokmag.IdStatusDok == 0 && (hfShtimModifikim.Value == "modifikim"))
                    {
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].Text = "Konfirmo";
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].Image.Url = "~/images/new/check.png";
                    }

                    if(idStatusdok == 4 && hfShtimModifikim.Value == "modifikim")
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }

                if (m.Name == "Refuzo")
                    if (cmbLloji.Text != "FH" || (kushtikonfirmim == "Jo") | kokmag.IdStatusDok != 0 || (hfShtimModifikim.Value != "modifikim"))
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;

                if (m.Name == "RuajPrint")
                {
                    if (cmbLloji.Text == "FH" && kushtikonfirmim == "Po" && kokmag.IdStatusDok == 4 && hfShtimModifikim.Value == "modifikim")
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                    if (cmbLloji.Text == "FH" && kushtikonfirmim == "Po" && kokmag.IdStatusDok == 0 && hfShtimModifikim.Value == "modifikim")
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].Text = "Konfirmo dhe printo";
                    if (Convert.ToInt32(hfState.Get("IdStatusDok")) == 4 && hfShtimModifikim.Value == "modifikim")
                        aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].ClientVisible = false;
                }

                if (m.Name == "ItemFrame")
                    clsToolbarConfig.ShtoMenuItemPerFrame(this, aSPxMenu1, clsFunksione.ktheUrlHelpi(m.UrlHelp).Item1);

                if (m.Name == "Shto" || m.Name == "Kerko" || m.Name == "Pastro" || m.Name == "ItemFilter" || m.Name == "ItemFrame" || m.Name == "Arkiva" || m.Name == "Konverto" || m.Name == "Pezullo")
                    aSPxMenu1.Items[aSPxMenu1.Items.Count - 1].BeginGroup = true;
                
            }

            var handlerPerPo = new EventHandler(btnPo_Click);
            var handlerPerJo = new EventHandler(btnJo_Click);
            clsMenuInfo.ShtoMenuItemInfo(this, MenuInfo, handlerPerPo, handlerPerJo);
            var visible = clsFunksione.merrMenuVisibleDraft(hfShtimModifikim.Value != "modifikim", kokmag.IdStatusDok);
            ASPxMenu1.Items.FindByName("Draft").ClientVisible = visible;
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
            var sot = DateTime.Today;
            var periudha = mySessionObjects.merrPeriudheKontabel(Session);
            if (sot >= periudha.FillimiPeriudha && sot <= periudha.MbarimiPeriudha)
            {
                dteDtDok.Value = DateTime.Today;
                dteDtTransporti.Value = DateTime.Today;
            }
            else
                dteDtDok.Value = periudha.FillimiPeriudha; dteDtTransporti.Value = periudha.FillimiPeriudha;
        }

        /// <summary>
        /// Vendos vlerat default kur po behet shtim
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idNdermarrje"></param>
        private void KonfiguroVleraFillestareShto(int idGjuha, int idNdermarrje, ResourceManager rm, CultureInfo ci)
        {
            dteDtDok.Date = DateTime.Today;
            dteDtTransporti.Date = DateTime.Today;
            dteDtRegjistrimi.Date = DateTime.Today;
            vendosDataDefault();
            var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            if (hfShtimModifikim.Value == "shtimraport" && Request.QueryString["niveli"] != null)
                cmbLloji.SelectedItem = cmbLloji.Items.FindByValue(Convert.ToInt32(Request.QueryString["niveli"]));
            AspxWebControlUtils.vendosDateEditMask(dteDtDok, dteDtRegjistrimi, dteDtTransporti);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina, btneKlientFurnitori, btneMagazina2, cmbLlogariKunderParti, btneAutomjeti, cmbKategoriSeriali, btnTransportuesi);
            ConfigureAspxComboBox.percaktoTemplateComboJoListePaLupe(cmbDegeAdministrative, cmbFormatiPrintimit, cmbOperatori);
            ConfigureAspxComboBox.shtoKolonaPerMagazina(btneMagazina2);
            ConfigureAspxComboBox.shtoKolonaPerMagazina(btneMagazina);
            ConfigureAspxComboBox.ShtoKolonaPerKf(btneKlientFurnitori);
            ConfigureAspxComboBox.mbushComboTransportues(idNdermarrje, btnTransportuesi);
            ConfigureAspxComboBox.ShtoKolonaEmriDheMbiemri(cmbOperatori, "IdOperator");
            ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatiPrintimit, 6, idNdermarrje);
            ConfigureAspxComboBox.mbushComboOperatori(cmbOperatori, idNdermarrje);
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(cmbLlogariKunderParti);
            ConfigureAspxComboBox.shtoKolonaPerGrupim(cmbGrup1);
            ConfigureAspxComboBox.ShtoKolonaKodiDhePershkrimi(cmbDegeAdministrative, "IdDegeAdministrative");
            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(idNdermarrje, cmbDegeAdministrative, false);
            cmbDegeAdministrative.Items.RemoveAt(0);
            mbushComboNivelesh(idNdermarrje, cmbLloji);
            mbushComboKonfigurimet(false, rm, ci, idGjuha, idNdermarrje, idPerdoruesi);
            MbushKomboGrupDokumentash(idNdermarrje, Convert.ToInt32(cmbKonfigurimi.Value));

            ConfigureAspxComboBox.mbushComboKategoriSeriali(idNdermarrje, cmbKategoriSeriali);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina, btneKlientFurnitori, btneMagazina2, cmbLlogariKunderParti, btneAutomjeti, btnTransportuesi);
            cmbLloji.SelectedIndex = 0;
            
            //e kalojme shtim si false, sepse ne rastin e magazines pavaresisht klientit do merret gjithmone formati i numrit per monedhen baze
            int idMonedheZgjedhur = clsFunksione.ktheMonedhePerFormatNumri(idGjuha, int.Parse(cmbKonfigurimi.Value.ToString()), idNdermarrje, 510, "btneKlientFurnitori", -1, true);
            clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(int.Parse(cmbKonfigurimi.Value.ToString()));
            var formatMonedhe = clsFormatKonfigTrup.ktheFormatNumriMonedhe(formatNrPerKonfig, idMonedheZgjedhur);
            hfState.Set("formatMonedhe", JsonConvert.SerializeObject(formatMonedhe));
            txtVlefta.Text = "0.00";
        }

        private void mbushComboNivelesh(int idNdermarrje, ASPxComboBox cmblloji)
        {
            DataTable dt;
            dt = clsFunksione.isLlojHyrje(Request)
                ? colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriQeKaneKonfigAmbjenteshDtCombo(6, idNdermarrje, mySessionObjects.ktheIdPerdoruesi(Session), false, "HYRJE", hfShtimModifikim.Value != "modifikim").ToTable()
                : colNivelRegjistrimi.ktheGjitheNivelRegjistrimiSipasKategoriQeKaneKonfigAmbjenteshDtCombo(6, idNdermarrje, mySessionObjects.ktheIdPerdoruesi(Session), false, "DALJE", hfShtimModifikim.Value != "modifikim").ToTable();
            cmblloji.DataSource = dt;
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
        private void konfiguroVleraFillestareModifiko(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, int idnderviti, ResourceManager rm, CultureInfo ci)
        {
            AspxWebControlUtils.vendosDateEditMask(dteDtDok, dteDtTransporti, dteDtRegjistrimi);
            ConfigureAspxComboBox.percaktoTemplateComboMeLupe(btneMagazina, btneKlientFurnitori, btneMagazina2, cmbLlogariKunderParti, btneAutomjeti, cmbKategoriSeriali, btnTransportuesi);
            ConfigureAspxComboBox.shtoKolonaPerMagazina(btneMagazina);
            ConfigureAspxComboBox.shtoKolonaPerMagazina(btneMagazina2);
            ConfigureAspxComboBox.ShtoKolonaPerKf(btneKlientFurnitori);

            ConfigureAspxComboBox.percaktoTemplateComboJoListePaLupe(cmbDegeAdministrative, cmbFormatiPrintimit, cmbOperatori);
            ConfigureAspxComboBox.mbushComboKategoriSeriali(idNdermarrje, cmbKategoriSeriali);
            ConfigureAspxComboBox.mbushComboFormatPrintimi(cmbFormatiPrintimit, 6, idNdermarrje);
            ConfigureAspxComboBox.ShtoKolonaEmriDheMbiemri(cmbOperatori, "IdOperator");
            ConfigureAspxComboBox.mbushComboOperatori(cmbOperatori, idNdermarrje);
            ConfigureAspxComboBox.mbushComboTransportues(idNdermarrje, btnTransportuesi);
            ConfigureAspxComboBox.ShtoKolonaKodiDhePershkrimi(cmbDegeAdministrative, "IdDegeAdministrative");
            ConfigureAspxComboBox.ShtoKolonaPerLlogarine(cmbLlogariKunderParti);
            mbushComboNivelesh(idNdermarrje, cmbLloji);
            ConfigureAspxComboBox.KonfiguroComboBoxDegeAdministrative(idNdermarrje, cmbDegeAdministrative, true);
            cmbDegeAdministrative.Items.RemoveAt(0);

            cmbLloji.ClientSideEvents.Init = "function(s,e){TextChangedLloji();}";
            txtVlefta.Text = "0.00";
            var id = int.Parse(Request.QueryString["id"]);
            var kok = new clsKokaMagazina();
            switch (hfShtimModifikim.Value)
            {
                case "rezervim":
                    clsKokaRezervime clsKokarez = new clsKokaRezervime();
                    clsKokarez.mbushKokaRezervimiSipasID(id);
                    if (clsKokarez.IdKokaRezervimi == 0) return;
                    MerrTedhenatRez(idGjuha, idPerdoruesi, idViti, idNdermarrje, clsKokarez, rm, ci);
                    mbushListeRegjistrimMagazineTrupiModifiko(kok);
                    return;
                case "inventarizim":
                    var coltrup = MerrTedhenatInvent(idGjuha, idPerdoruesi, idViti, idNdermarrje, idnderviti, rm, ci);
                    mbushHiddenFieldet(coltrup, int.Parse(cmbKonfigurimi.Value.ToString()), 0, int.Parse(cmbKonfigurimi.Value.ToString()), idPerdoruesi);
                    return;
                case "konvertim":
                    if (Request.QueryString["fsh"] == "jo")
                    {
                        var clskokamag = new clsKokaMagazina();
                        if (!clskokamag.mbushKokaMagazinaSipasID(id)) return;
                        MerrTedhenat(idGjuha, idPerdoruesi, idViti, idNdermarrje, clskokamag, rm, ci, true);
                        mbushListeRegjistrimMagazineTrupiModifiko(kok);
                    }
                    else
                    {
                        var clskokashitje = new clsKokaShitje();
                        if (!clskokashitje.mbushKokaShitjeSipasIDPaTrup(id))
                            return;
                        MerrTedhenatNgaShitja(idGjuha, idPerdoruesi, idViti, idNdermarrje, clskokashitje);
                        mbushListeRegjistrimMagazineTrupiModifiko(kok);
                    }
                    return;
                case "klonim":
                    if (Convert.ToBoolean(Request.QueryString["kategoriNjejte"]))
                    {
                        kok.mbushKokaMagazinaSipasID(id);
                        MerrTedhenat(idGjuha, idPerdoruesi, idViti, idNdermarrje, kok, rm, ci, false);
                        mbushListeRegjistrimMagazineTrupiModifiko(kok);
                        return;
                    }
                    int idKatDokKlon = Convert.ToInt32(Request.QueryString["idKatDokKlon"]);
                    switch (idKatDokKlon)
                    {
                        case 1:
                        case 2:
                            KlonoShitjeNeMagazine(id, kok, idGjuha, idPerdoruesi, idViti, idNdermarrje, idKatDokKlon);
                            break;
                    }
                    return;
                default:
                    kok.mbushKokaMagazinaSipasID(id);
                    MerrTedhenat(idGjuha, idPerdoruesi, idViti, idNdermarrje, kok, rm, ci, false);
                    mbushListeRegjistrimMagazineTrupiModifiko(kok);
                    return;
            }
        }

        private void KlonoShitjeNeMagazine(int id, clsKokaMagazina kok, int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, int idKatDokKlon)
        {
            hfState.Set("vleraDefaultKlonimi", true);
            clsKokaShitje kokaShitje = new clsKokaShitje();
            if (!kokaShitje.mbushKokaShitjeSipasIDPaTrup(id))
                return;
            MerrTedhenatNgaShitja(idGjuha, idPerdoruesi, idViti, idNdermarrje, kokaShitje);
            kok.OcolTrupiMagazina.ktheGjitheTrupiMagazinaNgaKokaKlonim(idKatDokKlon, id);
            int idMag = kok.OcolTrupiMagazina.Count > 0 ? kok.OcolTrupiMagazina.FirstOrDefault().IdMag : 0;
            if(idMag > 0 && kok.OcolTrupiMagazina.FindIndex(x => x.IdMag != idMag) < 0)
            {
                ConfigureAspxComboBox.mbushComboMagazinatMeID(idNdermarrje, btneMagazina, idPerdoruesi, true, 0, true, idMag);
                txtPershkrimMagazine.Text = clsNjesiAdministrative.kthePershkrimNjesiAdministrativeSipasiDPaAutorizime(idMag);
            }

            mbushListeRegjistrimMagazineTrupiModifiko(kok);
            HfColKodbare.Value = JsonConvert.SerializeObject(colKodbare.merrKodbarArtikulliNeTrupDokShitje(id));
        }

        /// <summary>
        /// Merr trupin ekzistues te dokumentit te magazines qe po modifikohet
        /// </summary>
        private void mbushListeRegjistrimMagazineTrupiModifiko(clsKokaMagazina koka)
        {//mbush griden me te dhenat
            if (koka.OcolTrupiMagazina.Count == 0)
            {
                if ((cmbLloji.Text == "UH" || cmbLloji.Text == "UD") && hfShtimModifikim.Value == "klonim")
                    koka.mbushTrupMagazine(true);
                else
                    koka.mbushTrupMagazine(false);
            }
            mbushHiddenFieldet(koka.OcolTrupiMagazina, koka.IdKonfigAmbjente, koka.IdKokaMagazina, koka.IdKonfigAmbjente, IdPerdoruesi);
        }

        private void mbushHiddenFieldet(colTrupiMagazina col, int lloji, int idkokamagazina, int idkonfig, int idPerdoruesi)
        {
            bool afgj=false;
            if (cmbLloji.Text == "UH" || cmbLloji.Text == "UD")
               afgj = true;
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti(lloji);
            HfKonfAmb.Value = JsonConvert.SerializeObject(konf);
            if (hfShtimModifikim.Value != "rezervim" && hfShtimModifikim.Value != "konvertim")
            {
                HfColTrupMag.Value = JsonConvert.SerializeObject(col);
                var colart = col.ktheColArtikujPaLoop(IdNdermarrja,IdPerdoruesi);
                HfColArt.Value = JsonConvert.SerializeObject(colart);
                var colartset = col.ktheColArtikujSet();
                HfColArtSet.Value = JsonConvert.SerializeObject(colartset);
                 var colKodbari = DbCore.DbInventari.colKodbare.merrKodbarArtikulliNeTrupDokMagazine(idkokamagazina);
                HfColKodbare.Value = JsonConvert.SerializeObject(colKodbari);
                HfColDetArt.Value = JsonConvert.SerializeObject(col.ktheColDetArtPaLoop(IdNdermarrja, idPerdoruesi));
                HfColDetArt2.Value = JsonConvert.SerializeObject(col.ktheColDetArtPaLoop2(IdNdermarrja, idPerdoruesi));
                HfColNjesAdminis.Value = JsonConvert.SerializeObject(col.ktheColMagPaLoop(IdNdermarrja, idPerdoruesi));
                HfColNjesiArt.Value = JsonConvert.SerializeObject(col.ktheColNjesiArt());
                if (hfShtimModifikim.Value != "klonim" && hfShtimModifikim.Value != "inventarizim")
                    for (var i = 0; i < col.Count; i++)
                    {

                        if (col[i].IdLlojVeprimi == 1 && colart[i].LlojiArt)
                        {
                            colAQTSeriale colzgjedhur = new colAQTSeriale();
                            colzgjedhur.ktheAQTSerialSipasIdDokumentiDheNrRreshtiNgaSerialeMagazine(idkokamagazina, lloji, i);

                            hfSeriale.Set(col[i].IdArtikulli + "_" + (i + 1), JsonConvert.SerializeObject(colzgjedhur));
                            hfSasiSeriale.Set(col[i].IdArtikulli + "_" + (i + 1), colart[i].MeSerial ? 1 : col[i].Sasia * col[i].Koeficenti);
                        }
                    }
                if (hfState.Get("kushtTransferim") == null || hfState.Get("kushtTransferim").ToString() == "Jo" || hfShtimModifikim.Value == "inventarizim")
                    HfColNjesAdminisDest.Value = null;
                else
                    HfColNjesAdminisDest.Value = JsonConvert.SerializeObject(col.ktheColMagDest(idkokamagazina, idkonfig, idPerdoruesi));
                if (afgj && col.Count == 0 && hfShtimModifikim.Value == "klonim")
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKyDokNukPerdoretPerArtAfgjFshire", ci), pnlMesazhi);
                return;
            }
            var ids = new int[0];
            var previousPageID = DbCore.IMBUtils.Types.Converter.MerrVlereOseDefault<string>(Request.QueryString["pageCacheId"]);
            if (!string.IsNullOrEmpty(previousPageID))
            {
                var pageCache = GlobalCacheManager.GetPageCacheByPageID(previousPageID);
                ids = (int[])pageCache["idkonvertimi"];
            }
            var m = 1;
            var colgjithe = new colTrupiMagazina();
            var colartgjithe = new colArtikujt();
            var colartsetgjithe = new colArtikujt();
            var coldetgjithe = new colDetajimeArtikulli();
            var coldetgjithe2 = new colDetajimeArtikulli();
            var colmaggjithe = new colNjesiAdministrative();
            var colnjesigjithe = new colNjesiteArtikulli();
            var colmaggjithedesc = new colNjesiAdministrative();
            var colKodbaregjithe = new DataTable();

            foreach (var id in ids)
            {
                var trupi = new colTrupiMagazina();
                if (hfShtimModifikim.Value == "rezervim")
                {
                    trupi.ktheGjitheTrupiMagazinaNgaKokaRezervime(id, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    colgjithe.AddRange(trupi);

                    var colart = trupi.ktheColArtikuj();
                    colartgjithe.AddRange(colart);
                    var colartset = trupi.ktheColArtikujSet();
                    colartsetgjithe.AddRange(colartset);
                    var colKodbari = colKodbare.merrKodbarArtikulliNeTrupDokMagazine(id);
                   
                    if (colKodbari != null)
                        colKodbaregjithe.Merge(colKodbari);
                }
                else
                {
                    if (Request.QueryString["fsh"] == "jo")
                        trupi.ktheGjitheTrupiMagazinaNgaKokaKonvertimUD(id, mySessionObjects.merrIdNdermarrjeSesioni(Session),afgj);
                    else
                        if (clsKokaShitje.merrKodNiveliSipasIdShitjes(id) == "FSH")
                        trupi.ktheGjitheTrupiMagazinaNgaKokaKonvertim(id, mySessionObjects.merrIdNdermarrjeSesioni(Session),afgj);
                    else
                        trupi.ktheGjitheTrupiMagazinaNgaKokaKonvertimUSH(id, mySessionObjects.merrIdNdermarrjeSesioni(Session),afgj);
                    colgjithe.AddRange(trupi);

                    var colart = trupi.ktheColArtikuj();
                    colartgjithe.AddRange(colart);
                    var colartset = trupi.ktheColArtikujSet();
                    colartsetgjithe.AddRange(colartset);
                    var colKodbari = new DataTable();
                    if (Request.QueryString["fsh"] == "jo")
                        colKodbari = colKodbare.merrKodbarArtikulliNeTrupDokMagazine(id);
                    else
                        colKodbari = colKodbare.merrKodbarArtikulliNeTrupDokShitje(id);
                    if (colKodbari != null) //vendosa kete kushtin se kur konvertoja nga shitja me vinte null dhe nuk behej merge tek colKodbaregjithe dhe ngecte
                        colKodbaregjithe.Merge(colKodbari);
                    int hyrjedalje;
                    if (cmbLloji.Text == "FH" || cmbLloji.Text == "UH")
                        hyrjedalje = 1;
                    else hyrjedalje = 2;
                    var koka = new clsKokaMagazina();
                    koka.mbushKokaMagazinaSipasIDGjenerues(id, hyrjedalje, clsKokaShitje.ktheIdKonfigAmbjente(id));
                    for (var j = 0; j < trupi.Count; j++)
                    {
                        if (trupi[j].IdLlojVeprimi == 1 && colart[j].LlojiArt && cmbLloji.Text != "FH")
                        {
                            var colzgjedhur = new colAQTSeriale();
                            colzgjedhur.ktheAQTSerialSipasIdDokumentiDheNrRreshtiNgaSerialeMagazine(koka.IdKokaMagazina, koka.IdKonfigAmbjente, j);

                            hfSeriale.Set(trupi[j].IdArtikulli + "_" + m, JsonConvert.SerializeObject(colzgjedhur));
                            hfSasiSeriale.Set(trupi[j].IdArtikulli + "_" + m, colart[j].MeSerial ? 1 : trupi[j].Sasia * trupi[j].Koeficenti);
                        }
                        m++;
                    }
                    if (afgj && trupi.Count == 0 && hfShtimModifikim.Value == "konvertim")
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKyDokNukPerdoretPerArtAfgjFshire", ci), pnlMesazhi);
                }

                if (Request.QueryString["fsh"] == "jo")
                {
                    var kokamag = new clsKokaMagazina();
                    kokamag.mbushKokaMagazinaSipasID(id);
                    var colnjesi = col.ktheColMagDest(id, kokamag.IdKonfigAmbjente, idPerdoruesi);
                    if (colnjesi.Count == 0)
                        for (var v = 0; v < trupi.Count; v++)
                            colnjesi.Add(new clsNjesiAdministrative());
                    colmaggjithedesc.AddRange(colnjesi);
                }

                coldetgjithe.AddRange(trupi.ktheColDetArt());
                coldetgjithe2.AddRange(trupi.ktheColDetArt2());
                colmaggjithe.AddRange(trupi.ktheColMag(idPerdoruesi));
                colnjesigjithe.AddRange(trupi.ktheColNjesiArt());
            }
            HfColTrupMag.Value = JsonConvert.SerializeObject(colgjithe);
            HfColArt.Value = JsonConvert.SerializeObject(colartgjithe);
            HfColArtSet.Value = JsonConvert.SerializeObject(colartsetgjithe);
            HfColKodbare.Value = JsonConvert.SerializeObject(colKodbaregjithe);
            HfColNjesAdminisDest.Value = JsonConvert.SerializeObject(colmaggjithedesc);
            HfColDetArt.Value = JsonConvert.SerializeObject(coldetgjithe);
            HfColDetArt2.Value = JsonConvert.SerializeObject(coldetgjithe2);
            HfColNjesAdminis.Value = JsonConvert.SerializeObject(colmaggjithe);
            HfColNjesiArt.Value = JsonConvert.SerializeObject(colnjesigjithe);
        }

        private void konfigGrid(int idNdermarrje, int idGjuha)
        {
            const string emriKomponentes = "Shto_RegjistrimMagazine.aspx";
            var oKomponente = new clsKomponente(emriKomponentes);
            var konfigurimi = new clsKonfigurimAmbjenti();
            konfigurimi.mbushKonfigDefaultKomponentes(oKomponente.IdKomponente, idNdermarrje);
            var trupiGrides = new DbCore.DbAdmin.colGridaTrupi(oKomponente.IdKomponente, konfigurimi.IdKonfigAmbjente, idGjuha);
            HfGridCol.Value = JsonConvert.SerializeObject(trupiGrides);
        }
        private void mbushComboTipi()
        {
            cmbTipiMag.Items.Add("WTN");
            cmbTipiMag.Items.Add("SALE");
        }
        private void mbushComboTransaksion()
        {
            cmbTransaksioni.Items.Add("SALES");
            cmbTransaksioni.Items.Add("EXAMINATION");
            cmbTransaksioni.Items.Add("TRANSFER");
            cmbTransaksioni.Items.Add("DOOR");
        }
        private void mbushComboKonfigurimet(bool mod, ResourceManager rm, CultureInfo ci, int idGjuha, int idNdermarje, int idPerdoruesi)
        {
            var colKonfig = new colKonfigurimAmbjenti();
            const int idKategori = 6;
            switch (Request.QueryString["lloj"])
            {
                case "dalje":
                    colKonfig.mbushKonfigAmbjSipasIdKategoriKodNivelPaVartese(idKategori, "FD", idNdermarje, idPerdoruesi, idGjuha, hfShtimModifikim.Value == "modifikim" ? false : true);
                    colKonfig.mbushKonfigAmbjSipasIdKategoriKodNivelPaVartese(idKategori, "UD", idNdermarje, idPerdoruesi, idGjuha, hfShtimModifikim.Value == "modifikim" ? false : true);
                    break;
                case "hyrje":
                    colKonfig.mbushKonfigAmbjSipasIdKategoriKodNivelPaVartese(idKategori, "FH", idNdermarje, idPerdoruesi, idGjuha, hfShtimModifikim.Value == "modifikim" ? false : true);
                    colKonfig.mbushKonfigAmbjSipasIdKategoriKodNivelPaVartese(idKategori, "UH", idNdermarje, idPerdoruesi, idGjuha, hfShtimModifikim.Value == "modifikim" ? false : true);
                    break;
                default:
                    colKonfig.mbushKonfigAmbjSipasIdKategoriPaKonfVartese(idKategori, idNdermarje, idPerdoruesi, idGjuha);
                    break;
            }

            if (cmbLloji.Value != null)
            {
                var idNivel = int.Parse(cmbLloji.Value.ToString());
                colKonfig.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(idKategori, idNivel, idPerdoruesi);
            }
            else
                colKonfig.mbushKonfigAmbjSipasIdKategori(idKategori, idNdermarje, idPerdoruesi, idGjuha);

            cmbKonfigurimi.IncrementalFilteringMode = IncrementalFilteringMode.Contains;

            var konfVarura = new colKonfigurimAmbjenti();
            konfVarura.AddRange(from konfi in colKonfig let alternativa = clsAlternativaKushti.getAlternativa(konfi.IdKonfigAmbjente, "V") where alternativa == "Po" && !mod select konfi);

            foreach (var konfi in konfVarura)
            {
                colKonfig.Remove(konfi);
            }
            var colprove = new ListBoxColumn
            {
                FieldName = "KodKonfigAmbjente",
                Caption = rm.GetString("cmbCmimeArtikulliCaptionKodi", ci)
            };
            var colemer = new ListBoxColumn
            {
                FieldName = "PershkrimKonfigAmbjente",
                Caption = rm.GetString("cmbCmimeArtikulliCaptionPershkrimi", ci),
                Width = 300
            };
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
        protected void ASPxMenu1_ItemClick(object source, MenuItemEventArgs e)
        {
            var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            using (DbData dbData = new DbData())
            {
                switch (e.Item.Name)
                {
                    case "Ruaj":
                        Page.Validate();
                        ruajRegjistrimMagazine(1, false, true, true, idPerdoruesi, dbData, cbDergoMeEmail.Checked, cbDergoEmailDokArkives.Checked);
                        mbushHiddenFieldet(new colTrupiMagazina(), 1, 0, 0, idPerdoruesi);
                        break;

                    case "Refuzo":
                        Page.Validate();
                        ruajRegjistrimMagazine(4, false, true, false, idPerdoruesi, dbData);
                        mbushHiddenFieldet(new colTrupiMagazina(), 1, 0, 0, idPerdoruesi);
                        break;

                    case "Draft":
                        Page.Validate();
                        ruajRegjistrimMagazine(0, false, true, false, idPerdoruesi, dbData);// 0 = statusi i dokumentit(ne kete rast statusi eshte draft)
                        mbushHiddenFieldet(new colTrupiMagazina(), 1, 0, 0, idPerdoruesi);
                        break;

                    case "PrintPreview":
                        if (hfShtimModifikim.Value == "modifikim")
                        {
                            var id = Request.QueryString["id"];
                            //var clsKoka = new clsKokaMagazina();
                            //clsKoka.mbushKokaMagazinaSipasID(int.Parse(id));
                            //var konf = new clsKonfigurimAmbjenti();
                            //konf.mbushKonfiguriminMeID(clsKoka.IdKonfigAmbjente);
                            Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=Rap_Format_Printimi_Magazina&idDokumenti=" + id + "&printo=false&raportdyte=jo&iddesign=" + cmbFormatiPrintimit.Value;//clsKoka.IdKokaMagazina
                        }
                        else
                        {
                            Page.Validate();
                            ruajRegjistrimMagazine(1, false, true, true, idPerdoruesi, dbData);
                            mbushHiddenFieldet(new colTrupiMagazina(), 1, 0, 0, idPerdoruesi);
                        }
                        break;

                    case "RuajPrint":
                        Page.Validate();
                        ruajRegjistrimMagazine(1, true, true, true, idPerdoruesi, dbData);
                        mbushHiddenFieldet(new colTrupiMagazina(), 1, 0, 0, idPerdoruesi);
                        break;
                    case "RefuzoDraft":
                        Page.Validate();

                        Refuzo();
                        break;
                }
            }
        }
        private void Refuzo()
        {
            var idKoka = int.Parse(Request.QueryString["id"]);
            var mesazh = clsKokaMagazina.RefuzoDokument(idKoka);
            if (mesazh)
            {
                Response.Redirect($"RegjistrimMagazine.aspx?lloj={Request.QueryString["lloj"]}&refuzoDraft=po");
                return;
            }
            else
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi);
        }
        private void ruajJoNgaMenuja(bool kontrollosasi, int idPerdoruesi)
        {
            Page.Validate();
            using (DbData dbData = new DbData())
            {
                switch (hfRuajDraft.Value)
                {
                    case "Ruaj":
                        ruajRegjistrimMagazine(1, false, kontrollosasi, true, idPerdoruesi, dbData);
                        break;

                    case "Draft":
                        ruajRegjistrimMagazine(0, false, kontrollosasi, false, idPerdoruesi, dbData);
                        break;

                    case "Refuzo":
                        ruajRegjistrimMagazine(4, false, kontrollosasi, false, idPerdoruesi, dbData);
                        break;

                    case "RuajPrint":
                        ruajRegjistrimMagazine(1, true, kontrollosasi, true, idPerdoruesi, dbData);
                        break;

                    default:
                        ruajRegjistrimMagazine(1, false, kontrollosasi, true, idPerdoruesi, dbData);
                        break;
                }
            }
            mbushHiddenFieldet(new colTrupiMagazina(), 1, 0, 0, idPerdoruesi);
        }

        private bool konvertuarPlotesisht(int idNdermarrje, int[] ids, colTrupiMagazina trupiMagazina)
        {
            List<String> artikujtKonvPlot = new List<string>();
            foreach (int id in ids)
                if (Request.QueryString["fsh"] == "po")
                {
                    string ngjyra = clsKokaShitje.merrNgjyreKonvertimeMag(idNdermarrje, id);
                    if (ngjyra == "kuqe" || ngjyra == "gjelber")
                    {
                        clsKokaShitje koka = new clsKokaShitje();
                        koka.mbushKokaShitjeSipasIDPaTrup(id);
                        lblMsgboxKonv.Text = "Dokumenti Nr." + koka.NrDok + " Dt." + koka.DtDok.ToShortDateString() + " eshte konvertuar plotesisht, doni te vazhdoni?";
                        status1.Value = "konvertuar";
                        return true;
                    }
                    if (ngjyra == "verdhe")
                        artikujtKonvPlot = artikujtTeKonvertuarPlotesishtNeDokument(artikujtKonvPlot, id, trupiMagazina, idNdermarrje, true);
                }
                else
                {//vjen kur e ke brenda magazines
                    var ngjyra = clsKokaMagazina.merrNgjyreKonvertime(idNdermarrje, id);
                    if (ngjyra == "kuqe" || ngjyra == "gjelber")
                    {
                        clsKokaMagazina koka = new clsKokaMagazina();
                        koka.mbushKokaMagazinaSipasID(id);
                        lblMsgboxKonv.Text = "Dokumenti Nr." + koka.NrDok + " Dt." + koka.DtDok.ToShortDateString() + " eshte konvertuar plotesisht, doni te vazhdoni?";
                        status1.Value = "konvertuar";
                        return true;
                    }
                    if (ngjyra == "verdhe")
                        artikujtKonvPlot = artikujtTeKonvertuarPlotesishtNeDokument(artikujtKonvPlot, id, trupiMagazina, idNdermarrje, false);
                }
            if (artikujtKonvPlot.Count == 0)
                return false;

            lblMsgboxKonv.Text = String.Format("Artikujt me kod: ({0}) jane konvertuar plotesisht, doni te vazhdoni?", string.Join(",", artikujtKonvPlot.ToArray())); //vendos artikujt ne mesazh
            status1.Value = "konvertuar";
            return true;
        }

        private static List<string> artikujtTeKonvertuarPlotesishtNeDokument(List<string> artikujtKonvPlot, int idDok, colTrupiMagazina trupiMagazina, int idNdermarrje, bool ngaShitja)
        {
            DataTable artKonvertuarPlot = clsKokaMagazina.merrArtikujTeKonvertuarPlotesisht(idDok, idNdermarrje, ngaShitja);
            if (artKonvertuarPlot.Rows.Count == 0)
                return artikujtKonvPlot;

            foreach (clsTrupiMagazina trupi in trupiMagazina) // kontrolli per cdo rresht ne trup te dokumentit (qe ndodhen ne gride)
            {
                DataRow rreshtKonv;
                rreshtKonv = ngaShitja 
                    ? artKonvertuarPlot.AsEnumerable().FirstOrDefault(x => x["IDSHITJETRUPI"].ToString().EqualsAnyIgnoreCase(trupi.IdTrupiKonvertimFSH.ToString(), trupi.IdTrupiKonvertimUSH.ToString())) 
                    : artKonvertuarPlot.AsEnumerable().FirstOrDefault(x => x["IDTRUPIMAGAZINA"].ToString().EqualsAnyIgnoreCase(trupi.IdTrupiKonvertimUD.ToString()));
                //nga rreshtat e konvertuar me pare kapet vetem rreshti qe perkon me rreshtin ne gride
                if (rreshtKonv != null && (Convert.ToDouble(rreshtKonv["SASIAMBETUR"]) < trupi.Sasia) && artikujtKonvPlot.FirstOrDefault(x => x.EqualsAnyIgnoreCase(rreshtKonv["KODARTIKULLI"].ToString())) == null) //nqs ekziston rreshti, kontrollohet per tejkalim te sasise dhe nqs ekziston apo jo ne listen e art qe do shfaqen ne mesazh
                    artikujtKonvPlot.Add(rreshtKonv["KODARTIKULLI"].ToString());
            }
            return artikujtKonvPlot;
        }

        /// <summary>
        /// Ruan/Modifikon nje objekt clsKokaMagazina.
        /// Therret funksionin <see cref="krijoRegjistrimMagazine"/>
        /// Therret funksionin <see cref="DbCore.DbRegjistrim.clsKokaMagazina.ruaj"/> ose <see cref="DbCore.DbRegjistrim.clsKokaMagazina.modifiko"/>
        /// </summary>
        /// <param name="statusDokumenti">Statusi me te cilin po ruhet dokumenti (I rregullt apo draft)</param>
        /// <param name="idPerdoruesi"></param>
        private void ruajRegjistrimMagazine(int statusDokumenti, bool printo, bool kontrollosasi, bool kontrollokonvertim, int idPerdoruesi, DbData dbData, bool dergoEmail = false, bool dergoEmailDokArkives = false)
        {
            if (txtNIVFSH.Text != "")
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Dokumenti eshte i fiskalizuar dhe nuk mund te modifikohet!", pnlMesazhi);
                return;
            }
            var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            string usernamePerdoruesi = new clsPerdorues(IdPerdoruesi).PerdoruesUsername;
            clsFunksione.dergoLogAlphaweb(new clsNdermarrje(idNdermarrje).NdermarrjePershkrimi, "Shtim Hyrje/Dalje", "Regjistrim dalje ose hyrje ne magazine", clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar(), usernamePerdoruesi);
            colKokaMagazina regjistrime = new colKokaMagazina();
            int meKontabilizim;
            if (Page.IsValid == false)
                return;
            string shfaqmesazhapolupe = "jo";
            bool rivleresim = false;
            string mesazhinformues = string.Empty;
            List<object[]> mesazheInformueseAsete = null;
            bool eshteOwn = (bool)hfState["OwnShop"];
            colTrupiMagazina tr = new colTrupiMagazina();
            var clsKonf = new clsKonfigurimAmbjenti();
            if (string.IsNullOrEmpty(cmbKonfigurimi.Text))
                throw new MyException(MessagesResource.Messages["msgNukKeniZgjedhurLlojinEDokumentit"]);
            clsKonf.mbushKonfigAmbjSipasKod(cmbKonfigurimi.Text, idNdermarrje);

            if (IsValidRegjistrimMagazine(statusDokumenti, clsKonf))
            {
                var mesazh = new clsMesazh();
                var serialemag = new colSerialetMagazine();
                var serialetransf = new colSerialetMagazine();
                var konfamortizimi = new clsKonfigurimAmbjenti();
                var konfamortizimihyrje = new clsKonfigurimAmbjenti();


                colSerialeUnikeKategori kategorite = new colSerialeUnikeKategori(idNdermarrje);
                bool serialeNeDetajim = clsAlternativaKushti.getAlternativa(clsKonf.IdKonfigAmbjente, "TSD1") == "Po";
                bool bashkoArtikujt = clsAlternativaKushti.getAlternativa(clsKonf.IdKonfigAmbjente, "TD1S") == "Po";
                var idGjuha = mySessionObjects.ktheGjuhe(Session);
                try
                {
                    if (btneKlientFurnitori.Text == string.Empty) // nese nuk ka klient furnitor
                    {
                        var myWatch = Stopwatch.StartNew();
                        regjistrime.Add(krijoRegjistrimMagazine(statusDokumenti, 0, string.Empty, out mesazhinformues, serialemag, serialetransf, kontrollosasi, konfamortizimi, konfamortizimihyrje, idGjuha, kategorite, serialeNeDetajim, clsKonf, bashkoArtikujt));
                        myWatch.Stop();
                        if (myWatch.Elapsed > new TimeSpan(0, 0, 1))
                            System.Diagnostics.Trace.WriteLine("krijoRegjistrimMagazine: " + myWatch.Elapsed);
                    }
                    else
                    {
                        var kf = Convert.ToInt32(btneKlientFurnitori.Value.ToString());
                        var kfur = new clsKlientFurnitor(kf);
                        regjistrime.Add(krijoRegjistrimMagazine(statusDokumenti, kf, kfur.KodKlientFurnitor, out mesazhinformues, serialemag, serialetransf, kontrollosasi, konfamortizimi, konfamortizimihyrje, idGjuha, kategorite, serialeNeDetajim, clsKonf, bashkoArtikujt));
                    }
                }
                catch (Exception ex)
                {
                    NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                    if (ex.Message.Contains('?'))
                    {
                        clsMenuInfo.ShtoPyetje(MenuInfo, ex.Message, pnlMesazhi, (int)hfState["idGjuha"]);
                        pergjigja.Text = "Serialet";
                    }
                    else
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, ex.Message, pnlMesazhi);
                    status1.Value = "false";
                    return;
                }
                var gjithmone = false;
                var idPeriudheZgjedhur = mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha;
                if (clsAlternativaKushti.getAlternativa(regjistrime[0].IdKonfigAmbjente, "GJKGJ") == "Po")
                    gjithmone = true;

                var ids = new int[0];
                var previousPageID = DbCore.IMBUtils.Types.Converter.MerrVlereOseDefault<string>(Request.QueryString["pageCacheId"]);
                if (!string.IsNullOrEmpty(previousPageID))
                {
                    var pageCache = GlobalCacheManager.GetPageCacheByPageID(previousPageID);
                    ids = (int[])pageCache["idkonvertimi"];
                }

                foreach (var regjistrim in regjistrime)
                {
                    if (hfShtimModifikim.Value == "konvertim" && kontrollokonvertim && konvertuarPlotesisht(idNdermarrje, ids, regjistrim.OcolTrupiMagazina))
                        return;

                    if (!int.TryParse(hfKontabilizimi.Value, out meKontabilizim))
                        throw new Exception(rm.GetString("msgGabimGjateKonvertimitTeHFKontabilizim", ci));
                    if (regjistrim.OcolTrupiMagazina.Count == 0)
                    {
                        status1.Value = "false";
                        return;
                    }

                    if (hfState.Get("kushtTransferim") != null && hfState.Get("kushtTransferim").ToString() == "Po" && regjistrim.OMagazinaTransferim.OcolTrupiMagazina.Count == 0)
                    {
                        status1.Value = "false";
                        return;
                    }
                    if (statusDokumenti == 0)
                        meKontabilizim = 0;
                    string pershkrimFk;
                    if (regjistrim.Shenime != string.Empty)
                        pershkrimFk = regjistrim.Shenime;
                    else
                        if (regjistrim.IdLlojDokumentiMagazine == 1)
                        pershkrimFk = pershkrimHyrjeFK;
                    else
                        pershkrimFk = pershkrimDaljeFK;
                    clsTeDrejtaRoli tedrejtaInfo = new clsTeDrejtaRoli();
                    string mesazhmevonshem = "";
                    tedrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, mySessionObjects.ktheIdVitNdermarrje(Session), clsFunksione.GetKomponente(Page.Request));
                    if (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "shtimraport" || hfShtimModifikim.Value == "klonim" || hfShtimModifikim.Value == "rezervim" || hfShtimModifikim.Value == "konvertim" || hfShtimModifikim.Value == "inventarizim")
                    {
                        hfArkiva.Set("kopjoArkiven", (hfShtimModifikim.Value == "klonim" || hfShtimModifikim.Value == "konvertim"));
                        if ((statusDokumenti == 1 || statusDokumenti == 4) && !tedrejtaInfo.DShtim || statusDokumenti == 0 && !tedrejtaInfo.DShtimDraft)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                            status1.Value = "false";
                            return;
                        }

                        int[] idinv = MerrIdInventarizimi();

                        if (hfState.Get("kushtTransferim") != null && hfState.Get("kushtTransferim").ToString() == "Po") //Transferim
                            mesazh = regjistrim.ruaj(true, meKontabilizim, hfNrAutoShitje, idPeriudheZgjedhur, pershkrimFk, out shfaqmesazhapolupe, eshteOwn, serialemag, serialetransf, konfamortizimi, konfamortizimihyrje, gjithmone, false, String.Empty, string.Empty, string.Empty, string.Empty, cbRenditje.Checked, false, idinv, false, false, false, out mesazhmevonshem, 0, ref dbData, false, kategorite, serialeNeDetajim, bashkoArtikujt);
                        else
                            mesazh = regjistrim.ruaj(false, meKontabilizim, hfNrAutoShitje, idPeriudheZgjedhur, pershkrimFk, out shfaqmesazhapolupe, eshteOwn, serialemag, serialetransf, konfamortizimi, konfamortizimihyrje, gjithmone, false, String.Empty, string.Empty, string.Empty, string.Empty, cbRenditje.Checked, false, idinv, false, false, false, out mesazhmevonshem, 0, ref dbData, false, kategorite, serialeNeDetajim, bashkoArtikujt);
                        if (statusDokumenti != 0 && (hfKontrollRivleresim.Value.ToLower() == "true" && regjistrim.rivleresim()))
                        {
                            rivleresim = true;
                            tr.mbushGjitheTrupiMagazinaNgaKoka(regjistrim.IdKokaMagazina);
                            trupat.AddRange(tr);
                        }

                    }
                    else if (hfShtimModifikim.Value == "modifikim")
                    {
                        if ((statusDokumenti == 1 || statusDokumenti == 4) && !tedrejtaInfo.DMod || statusDokumenti == 0 && !tedrejtaInfo.DModifikimDraft)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNukKeniTeDrejta", ci), pnlMesazhi);
                            status1.Value = "false";
                            return;
                        }
                        var transferim = false;
                        regjistrim.IdKokaMagazina = int.Parse(Request.QueryString["id"]);
                        var idStatusDok = clsKokaMagazina.merrIdStatusDok(regjistrim.IdKokaMagazina);
                        if (idStatusDok == 2)
                        {
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDokumentiKaNdryshuarHapeniPerseri", ci), pnlMesazhi);
                            status1.Value = "false";
                            return;
                        }
                        if (statusDokumenti != 0 && (hfKontrollRivleresim.Value.ToLower() == "true" && regjistrim.rivleresim()))
                        {
                            rivleresim = true;
                            tr.mbushGjitheTrupiMagazinaNgaKoka(regjistrim.IdKokaMagazina);
                            trupat.AddRange(tr);
                        }
                        var myWatcheshteILidhur = Stopwatch.StartNew();
                        var lidhur = !hfState.Get<bool>("GjeneruarNgaMema") && regjistrim.eshteILidhur();
                        myWatcheshteILidhur.Stop();
                        if (myWatcheshteILidhur.Elapsed > new TimeSpan(0, 0, 1))
                            System.Diagnostics.Trace.WriteLine("myWatcheshteILidhur: " + myWatcheshteILidhur.Elapsed);
                        if (idStatusDok == 1 && clsAlternativaKushti.getAlternativa(regjistrim.IdKonfigAmbjente, "LMD") == "Jo")
                            lidhur = true;
                        if (hfState.Get("kushtTransferim") != null && hfState.Get("kushtTransferim").ToString() == "Po")//Transferim
                            transferim = true;
                        if (lidhur.ToString() != hfLidhur.Value)
                        {
                            mesazh.Status = false;
                            mesazh.PershkrimMesazhi = rm.GetString("msgDokumentiEshteILidhur", ci);
                            status1.Value = "false";
                        }
                        else
                        {
                            var myWatch = Stopwatch.StartNew();
                            mesazh = regjistrim.modifiko(transferim, meKontabilizim, lidhur, pershkrimFk, out shfaqmesazhapolupe, eshteOwn, serialemag, serialetransf, konfamortizimi, konfamortizimihyrje, gjithmone, meKontabilizim, rm, ci, cbRenditje.Checked, false, false, false, out mesazhmevonshem, hfState.Get<bool>("GjeneruarNgaMema"), ref mesazheInformueseAsete, kategorite, serialeNeDetajim, bashkoArtikujt,hfState.Get<bool>("HyrjeGjeneruarNgaMema"));
                            myWatch.Stop();
                            if (myWatch.Elapsed > new TimeSpan(0, 0, 1))
                                System.Diagnostics.Trace.WriteLine("regjistrim.modifiko: " + myWatch.Elapsed);
                        }
                        if (statusDokumenti != 0 && (hfKontrollRivleresim.Value.ToLower() == "true" && regjistrim.rivleresimPas()))
                        {
                            rivleresim = true;
                            tr.mbushGjitheTrupiMagazinaNgaKoka(regjistrim.IdKokaMagazina);
                            trupat.AddRange(tr);
                        }
                    }
                    if (mesazhmevonshem != "")
                        clsMenuInfo.ShtoMesazhInformues(MenuInfo, mesazhmevonshem, pnlMesazhi);
                    if (mesazheInformueseAsete != null)
                        if (mesazheInformueseAsete.Any())
                            foreach (var obj in mesazheInformueseAsete)
                            {
                                var mesazhi = string.Format(rm.GetString("ShtoRegjistrimMagazine.ModifikimAsetiMsg", ci), obj);
                                clsMenuInfo.ShtoMesazhInformues(MenuInfo, mesazhi, pnlMesazhi);
                            }
                    if (dergoEmail || dergoEmailDokArkives)
                    {
                        clsMesazh msg;
                        if (dergoEmailDokArkives)
                        {
                            var arkiva = new colArkiva(regjistrim.IdKokaMagazina, regjistrim.IdKategoria);
                            msg = EmailComposer.dergoEmailFaturenNgaPerdoruesiLoguarNgaMagazina((int)hfState["idGjuha"], ci, (int)hfState["idPerdoruesi"], (int)hfState["idViti"], (int)hfState["idNdermarrje"], regjistrim.IdKokaMagazina, regjistrim.IdKlientFurnitor, regjistrim.NrDok, regjistrim.DtDok, regjistrim.IdRaportDesing, regjistrim.IdKonfigAmbjente, clsNjesiAdministrative.ktheEmailSipasId(regjistrim.IdMagazina), regjistrim.KodMagazina, regjistrim.Pershkrimi, MapPath(null), arkiva != null, arkiva);
                        }
                        else

                            msg = EmailComposer.dergoEmailFaturenNgaPerdoruesiLoguarNgaMagazina((int)hfState["idGjuha"], ci, (int)hfState["idPerdoruesi"], (int)hfState["idViti"], (int)hfState["idNdermarrje"], regjistrim.IdKokaMagazina, regjistrim.IdKlientFurnitor, regjistrim.NrDok, regjistrim.DtDok, regjistrim.IdRaportDesing, regjistrim.IdKonfigAmbjente, clsNjesiAdministrative.ktheEmailSipasId(regjistrim.IdMagazina), regjistrim.KodMagazina, regjistrim.Pershkrimi);
                        if (!mesazh.Status)
                            clsMenuInfo.ShtoMesazhGabimi(MenuInfo, msg.PershkrimMesazhi, pnlMesazhi);
                        else
                            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, msg.PershkrimMesazhi, pnlMesazhi);
                    }
                    mySessionObjects.ruajTrupatNeSession(Session, trupat);
                    pergjigja.Text = "ruaj";
                    if (mesazh.Status)
                    {
                        if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
                        {
                            clsNdermarrje ndermarrje = new clsNdermarrje(idNdermarrje);

                            if (ndermarrje.Fiskalizimi == true && cbFiskalizo.Checked && Request.QueryString["lloj"] == "dalje" && (hfShtimModifikim.Value == "shtim" || hfShtimModifikim.Value == "klonim"))
                            {
                                clsNjesiAdministrative njesiAdministrative = new clsNjesiAdministrative(regjistrim.KodMagazina, ndermarrje.IdNdermarrje);
                                clsQyteti qyteti = new clsQyteti(njesiAdministrative.Qyteti);
                                var timeZone = DateTime.Now.ToString("HH:mm:sszzz");
                                var dateTransportimi = regjistrim.DtTransporti.ToString("yyyy-MM-ddTHH:mm:sszzz");
                                string kodSoftueri = WebConfigurationManager.AppSettings["kodSoftueri"];
                                var wtnicSignature = clsFunksioneFiskalizimi.GjeneroWTNICSignature(ndermarrje, txtNrDok.Text, txtVlefta.Text, "ur271so291", kodSoftueri);
                                var operatoriid = cmbOperatori.Value != null ? int.Parse(cmbOperatori.Value.ToString()) : 0;
                                var operatori = clsOperator.MerrEmerDheMbiemerOperatoriSipasId(operatoriid, IdNdermarrja);
                                var emerMbiemerOperatori = operatori.ItemArray[0].ToString() + " " + operatori.ItemArray[1].ToString();
                                string[] emerMbiemerOperatorit = emerMbiemerOperatori.Split(' ');
                                string[] kodiDegaAdministrative = cmbDegeAdministrative.Text.Split(' ');
                                clsDegeAdministrative degeAdministrative = new clsDegeAdministrative(kodiDegaAdministrative[0], IdNdermarrja);
                                var kodOperatori = clsOperator.MerrKodOperatoriSipasId(operatoriid, IdNdermarrja);
                                clsNjesiAdministrative njesiAdministrativeDestinacion = new clsNjesiAdministrative(new clsKokaMagazina(clsKokaMagazina.merrIdDokHyrjeNgaTransferimi(regjistrim.IdKokaMagazina)).IdMagazina);
                                var mesazhShoqerues = clsFunksioneFiskalizimi.gjeneroFatureShoqeruese(ndermarrje, txtWTNIC.Text, wtnicSignature, cbShoqerimIKerkuar.Checked.ToString(), cbMallraTeDjegshme.Checked.ToString(), txtadresa.Text, "Tirana", txtTarga2.Text, regjistrim.OcolTrupiMagazina, txtVlefta.Text, txtNrDok.Text, regjistrim.Transportuesi, njesiAdministrative.TipiMag, qyteti.KodiQyteti, dateTransportimi, false, degeAdministrative.KodNjesieBiznesi,cmbTipiMag.Text,cmbTransaksioni.Text, njesiAdministrativeDestinacion, njesiAdministrative, kodOperatori.ItemArray[0].ToString(),false);
                                var FWTNIC = clsFunksioneFiskalizimi.InvokeService(mesazhShoqerues[0], "FWTNIC", false);
                                if (FWTNIC[1] != null)
                                {
                                    var objekti = ktheObjektPerNotify(regjistrim, "Deshtim", clsKokaShitje.merrTrupiShitje(regjistrim.IdKokaMagazina), new clsTrupiShitje(), FWTNIC[0], FWTNIC[1], "Fature Shoqeruese", mesazhShoqerues[1], FWTNIC[2]);
                                    clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);
                                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, "Ndodhi nje gabim me fiskalizimin, fatura shoqeruese nuk u fiskalizua!" + $"Error:{FWTNIC[1]}" + $" Pershkrimi i errorit:{FWTNIC[0]}", pnlMesazhi);
                                    
                                }

                                else
                                {
                                    var sukses = regjistrim.shtoNivfshTeMagazina(ndermarrje.IdNdermarrje, regjistrim.IdKokaMagazina, FWTNIC[0]);
                                    clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, "Fatura shoqeruese u fiskalizua me sukses!", pnlMesazhi);
                                    var objekti = ktheObjektPerNotify(regjistrim, "Sukses", clsKokaShitje.merrTrupiShitje(regjistrim.IdKokaMagazina), new clsTrupiShitje(), FWTNIC[0], FWTNIC[1], "Fature Shoqeruese", mesazhShoqerues[1], FWTNIC[2]);
                                    clsFunksioneFiskalizimi.dergoWebhookNotify(objekti, false);
                                }
                            }
                        }
                        
                        var previusPageID = DbCore.IMBUtils.Types.Converter.MerrVlereOseDefault<string>(Request.QueryString["RegjistrimInventarizimiPageId"]);
                        var pageCache = GlobalCacheManager.GetPageCacheByPageID(previusPageID);
                        pageCache["idinventarizimi"] = new int[0];

                        hfqkmesazhi.Value = shfaqmesazhapolupe;
                        if (shfaqmesazhapolupe != "jo")
                        {
                            var kok = new clsKokaFleteKontabel(regjistrim.IdKokaMagazina, 6);

                            hfUrl.Value = "LupaRegjistrimQendraKosto.aspx?shtim_modifikim=modifikim&idDokGjenerues=" + kok.IdKokaFleteKontabel + "&idkonfig=" + kok.IdKonfigAmbjente;
                        }

                        hfShtimModifikim.Value = "shtim";
                        percaktoTemplateMenu(idPerdoruesi, mySessionObjects.ktheIdVitNdermarrje(Session), idNdermarrje, ASPxMenu1);
                        if (printo)
                            Container.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=Rap_Format_Printimi_Magazina&idDokumenti=" + regjistrim.IdKokaMagazina + "&printo=true&raportdyte=jo&iddesign=" + cmbFormatiPrintimit.Value;
                        hfShtimModifikim.Value = "shtim";
                        if (rivleresim)
                            clsMenuInfo.ShtoPyetje(MenuInfo, mesazhinformues + rm.GetString("regjMagVeprimiSjellNdryshimNeCmimDalje", ci), pnlMesazhi, (int)hfState["idGjuha"]);
                        else
                            clsMenuInfo.ShtoMesazhSuksesi(MenuInfo, rm.GetString("msgDokumentiURuajtMeSukese", ci) + mesazhinformues, pnlMesazhi);

                        pnlLidhur.Update();
                        status1.Value = "true";
                    }
                    else
                    {
                        clsFunksione.KontrolloPerSerialeTePerdoruraNeDokDraft(Session, Container1);
                        clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazh.PershkrimMesazhi, pnlMesazhi, LoadingPanel);
                        status1.Value = "false";
                    }
                }
            }
            else
                status1.Value = "false";
            pergjigja.ClientVisible = false;
        }

        private int[] MerrIdInventarizimi()
        {
            var previusPageID = DbCore.IMBUtils.Types.Converter.MerrVlereOseDefault<string>(Request.QueryString["RegjistrimInventarizimiPageId"]);
            var pageCache = GlobalCacheManager.GetPageCacheByPageID(previusPageID);

            int[] idinv = pageCache.Get<int[]>("idinventarizimi");
            if (idinv != null && hfShtimModifikim.Value == "inventarizim")
                return idinv;
            return new int[0];
        }

        /// <summary>
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <param name="klienti">Klienti</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina</returns>
        private clsKokaMagazina krijoRegjistrimMagazine(int statusDokumenti, int klienti, string kodklienti, out string mesazhinformues, colSerialetMagazine colserialemag, colSerialetMagazine colserialeTranf, bool kontrollosasi, clsKonfigurimAmbjenti konfamortizimi, clsKonfigurimAmbjenti konfamortizimihyrje, int idGjuha, colSerialeUnikeKategori kategorite, bool serialeNeDetajim, clsKonfigurimAmbjenti clsKonf, bool bashkoArtikujt)
        {
            mesazhinformues = string.Empty;

            var controls = this.GetAsPxTextEditIdValue();
            controls.AddRange(ASPxSplitter1.GetAsPxTextEditIdValue());

            hfNrAuto = (ASPxHiddenField)NrAuto.VendosVleratNrAuto(hfNrAuto, controls);
            //NrAuto.vendosVleratNrAuto(hfNrAuto, this, null, ASPxSplitter1, null);

            hfNrAutoShitje = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");
            hfNrAutoShitje = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrProjekti", "NrProjekt");
            hfNrAutoShitje = (ASPxHiddenField)NrAuto.ShtoNeRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrSerial", "NrSerial");
            //NrAuto.shtoNeHfRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrDok", "NrDok");
            //NrAuto.shtoNeHfRegjistrime(hfNrAutoShitje, hfNrAuto, "txtNrProjekti", "NrProjekt");

            var koka = new clsKokaMagazina();
            clsNdermarrje nderm = new clsNdermarrje(IdNdermarrja);
            if (nderm.Fiskalizimi && cbFiskalizo.Checked && Request.QueryString["lloj"] == "dalje" && hfShtimModifikim.Value != "modifikim")
            {
                string kodSoftueri = WebConfigurationManager.AppSettings["kodSoftueri"];
                if(txtWTNIC.Text == "")
                    txtWTNIC.Text = clsFunksioneFiskalizimi.GjeneroWTNIC(nderm, txtNrDok.Text, txtVlefta.Text, "ur271so291", kodSoftueri);
                DataTable error = new DataTable();
                error.Columns.Add("Kodi");
                error.Columns.Add("Gabimi");
                error.Columns.Add("Rreshti");

                if (cmbDegeAdministrative.Text == "")
                    error.Rows.Add("Dega administrative", "Plotesoni degen administrative!");
                if (cmbDegeAdministrative.Text != "" && new clsDegeAdministrative(cmbDegeAdministrative.Text, IdNdermarrja).KodNjesieBiznesi == "")
                    error.Rows.Add("Dega administrative", "Plotesoni Kodin e njesise se biznesit te dega administrative!");
                if (nderm.NdermarrjeQytetiPershkrimi == "")
                    error.Rows.Add("Emer qyteti ndermarrje", "Vendosni emrin e qytetit te ndermarrjes per fiskalizimin!");
                if (nderm.NdermarrjeNipt == "")
                    error.Rows.Add("Nipt ndermarrje", "Vendosni Nipt-in e ndermarrjes per fiskalizimin!");
                if (nderm.NdermarrjeVendi == "")
                    error.Rows.Add("Shtet ndermarrje", "Vendosni shtetin e ndermarrjes per fiskalizimin!");
                if (txtNrDok.Text.StartsWith("0"))
                    error.Rows.Add("Numer fature", "Numri i fatures nuk mund te filloj me 0!");
                if (txtNrDok.Text.Any(Char.IsLetter))
                    error.Rows.Add("Numer fature", "Numri i fatures nuk duhet te permbaje shkronja per fiskalizimin!");
                if (cmbOperatori.Text == "")
                    error.Rows.Add("Operatori", "Vendosni operatorin per fiskalizimin!");
                if (txtTarga2.Text == "")
                    error.Rows.Add("Targa e transportuesit", "Vendosni targen e transportuesit per fiskalizimin!");
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
            if (cmbLloji.Text == "FH" || cmbLloji.Text == "UH")
                koka.IdLlojDokumentiMagazine = 1;
            else // kontrollon per dokumentat te tipit FD ose UD a duhet te bejme kontroll Gjendje
                koka.IdLlojDokumentiMagazine = 2;

            var idnivel = int.Parse(cmbLloji.Value.ToString());
            var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var kushtamor = new clsKusht(clsKonf.IdKonfigAmbjente, "ZDAM");
            konfamortizimi.mbushKonfigAmbjSipasId(kushtamor.Vlera, idGjuha);
            var meAutorizim = !(hfState.Get("merrMagazinatMeAutorizim").Equals("Jo"));
            var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            var isOwnShop = bool.Parse(hfState.Get("OwnShop").ToString());
            if (koka.IdLlojDokumentiMagazine == 1)  //Hyrje
                koka = krijoRegjistrimMagazine(statusDokumenti, false, klienti, kodklienti, 1, clsKonf, idnivel, krijoTrupinEMagazines(false, 1, meAutorizim, idNdermarrje, idPerdoruesi, dteDtDok.Date, isOwnShop, cmbKonfigurimi.Text, hfShtimModifikim.Value == "klonim" ? true : false, false, clsKonf.IdKonfigAmbjente, serialeNeDetajim, kategorite, bashkoArtikujt,false), new clsKokaMagazina(), out mesazhinformues, colserialemag, kontrollosasi, true);
            else//Dalje
                if (koka.IdLlojDokumentiMagazine == 2 && (hfState.Get("kushtTransferim") == null || hfState.Get("kushtTransferim").ToString() == "Jo"))//Dalje
                koka = krijoRegjistrimMagazine(statusDokumenti, false, klienti, kodklienti, 2, clsKonf, idnivel, krijoTrupinEMagazines(false, -1, meAutorizim, idNdermarrje, idPerdoruesi, dteDtDok.Date, isOwnShop, cmbKonfigurimi.Text, hfShtimModifikim.Value == "klonim" ? true : false, true, clsKonf.IdKonfigAmbjente, serialeNeDetajim, kategorite, bashkoArtikujt, false), new clsKokaMagazina(), out mesazhinformues, colserialemag, kontrollosasi, true);
            else if (hfState.Get("kushtTransferim") != null && hfState.Get("kushtTransferim").ToString() == "Po")   //Transferim
            {
                var konf = new clsKonfigurimAmbjenti();
                var kushtZfh = new clsKusht(clsKonf.IdKonfigAmbjente, "ZFH");
                var statusdoktransf = 1;
                var kushti = kushtZfh.Vlera.ToString();
                if (kushti != null && kushti != "0")
                {
                    konf.mbushKonfigAmbjSipasId(Convert.ToInt32(kushti));
                    statusdoktransf = hfState.Get("kushtKonfirmim").ToString() == "Po" ? 0 : statusDokumenti;
                }
                else

                    if (hfState.Get("kushtKonfirmim").ToString() == "Jo")
                {
                    konf.mbushKonfigAmbjSipasKod("FHT", idNdermarrje);
                    statusdoktransf = statusDokumenti;
                }
                else
                {
                    konf.mbushKonfigAmbjSipasKod("FHTK", idNdermarrje);
                    statusdoktransf = 0;
                }
                bool lejoModifikimDetajimi = clsAlternativaKushti.getAlternativa(konf.IdKonfigAmbjente, "LMDET") == "Po";
                clsKusht kushtamorhyrje = new clsKusht(konf.IdKonfigAmbjente, "ZDAM");
                konfamortizimihyrje.mbushKonfigAmbjSipasId(kushtamorhyrje.Vlera, idGjuha);
                koka = krijoRegjistrimMagazine(statusDokumenti, false, klienti, kodklienti, 2, clsKonf, idnivel, krijoTrupinEMagazines(false, -1, meAutorizim, idNdermarrje, idPerdoruesi, dteDtDok.Date, isOwnShop, cmbKonfigurimi.Text, hfShtimModifikim.Value == "klonim" ? true : false, true, clsKonf.IdKonfigAmbjente, serialeNeDetajim, kategorite, bashkoArtikujt,false), krijoRegjistrimMagazine(statusdoktransf, true, klienti, kodklienti, 1, konf, konf.IdNivel, krijoTrupinEMagazines(true, 1, meAutorizim, idNdermarrje, idPerdoruesi, dteDtDok.Date, isOwnShop, cmbKonfigurimi.Text, hfShtimModifikim.Value == "klonim" ? true : false, false, clsKonf.IdKonfigAmbjente, serialeNeDetajim, kategorite, bashkoArtikujt, lejoModifikimDetajimi), new clsKokaMagazina(), out mesazhinformues, colserialeTranf, kontrollosasi, false), out mesazhinformues, colserialemag, kontrollosasi, true);
            }
            return koka;
        }

        /// <summary>
        /// Krijon nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina ne rastin kur kemi hyrje
        /// </summary>
        /// <param name="statusDokumenti">Statusi i dokumentit (I rregullt ose draft)</param>
        /// <param name="eshteTransferim">Tregon nese eshte transferim apo jo</param>
        /// <param name="klienti">Klienti</param>
        /// <returns>Kthen nje objekt te tipit DbCore.DbRegjistrim.clsKokaMagazina</returns>
        private clsKokaMagazina krijoRegjistrimMagazine(int statusDokumenti, bool eshteTransferim, int idKlienti, string kodklienti, int idllojdokmag,   clsKonfigurimAmbjenti clsKonf, int idnivel, colTrupiMagazina coltrupi, clsKokaMagazina magtransf, out string mesazhinformues, colSerialetMagazine colserialemag, bool kontrollosasi, bool krijoseriale)
        {
            var mag = new clsKokaMagazina();
            var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            mesazhinformues = string.Empty;
            var idnderviti = mySessionObjects.ktheNdermarrjeVit(Session);
            var idmagazina = 0;
            var idgrup = 0;
            var idgrup2 = 0;
            var idgrup3 = 0;
            var magazina = string.Empty;
            var pershkrimi = string.Empty;
            var magazinieri = string.Empty;
            var adresa = string.Empty;
            if (eshteTransferim && idllojdokmag == 1) //nese po kryhet trasferim ruhet id e magazines destinacion
            {
                if (btneMagazina2.Text != string.Empty)
                {
                    idmagazina = int.Parse(btneMagazina2.Value.ToString());
                    magazina = btneMagazina2.Text.Split(' ')[0];
                }
            }
            else if (btneMagazina.Text != string.Empty)
            {
                idmagazina = int.Parse(btneMagazina.Value.ToString());
                magazina = btneMagazina.Text.Split(' ')[0];
            }
            if (cmbGrup1.Text != string.Empty)
                idgrup = int.Parse(cmbGrup1.Value.ToString());
            if (cmbGrup2.Text != string.Empty)
                idgrup2 = int.Parse(cmbGrup2.Value.ToString());
            if (cmbGrup3.Text != string.Empty)
                idgrup3 = int.Parse(cmbGrup3.Value.ToString());
            if (txtPershkrimi.Text != string.Empty)
                pershkrimi = txtPershkrimi.Text;
            if (txtMagazinieri.Text != string.Empty)
                magazinieri = txtMagazinieri.Text;
            if (txtadresa.Text != string.Empty)
                adresa = txtadresa.Text;
            var idAutomjet = 0;
            var targa = string.Empty;
            if (btneAutomjeti.Text != string.Empty)
            {
                idAutomjet = int.Parse(btneAutomjeti.Value.ToString());
                targa = txtTarga.Text;
            }

            var idperdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            var iddege = 0;
            if (cmbDegeAdministrative.Text != string.Empty && cmbDegeAdministrative.Text != " ()")
                iddege = int.Parse(cmbDegeAdministrative.Value.ToString());
            var llog = new clsLlogari(cmbLlogariKunderParti.Text, idNdermarrje);
            var idnjesivartese = 0;
            var njesia = string.Empty;
            if (clsKonf.KodKonfigAmbjente == "FDNV" || clsKonf.KodKonfigAmbjente == "FHNV")
            {
                idnjesivartese = int.Parse(btneMagazina2.Value.ToString());
                njesia = btneMagazina2.Text.Split(' ')[0];
            }
            var id = 0;
            var kokaekzistuezerez = new clsKokaRezervime();
            var rezShitje = new clsKokaRezervime();
            if (hfShtimModifikim.Value == "modifikim" && Request.QueryString["id"] != null)
            {

                id = int.Parse(Request.QueryString["id"]);
                if (eshteTransferim && idllojdokmag == 1)
                    id = clsKokaMagazina.merrIdDokHyrjeNgaTransferimi(id);
                
                kokaekzistuezerez.mbushKokaRezervimiSipasIDGjenerues(id, 2, clsKonf.IdKonfigAmbjente);

                if (hfState.Get<bool>("GjeneruarNgaMema"))
                {
                    var magekzistues = new clsKokaMagazina(id);
                    rezShitje.mbushKokaRezervimiSipasIDGjenerues(magekzistues.IdGjenerues, 1, magekzistues.IdKonfigGjenerues);
                    rezShitje.mbushTrupRezervime();
                }

            }
            var idkategoriseriali = 0;
            if (!string.IsNullOrEmpty(cmbKategoriSeriali.Text))
            {
                if (cmbKategoriSeriali.Text != "")
                {// to do te merret kategori seriali
                    clsSerialeUnikeKategori kategori = new clsSerialeUnikeKategori(cmbKategoriSeriali.Text, idNdermarrje);
                    if (kategori.ID < 0)
                        throw new MyException("Kategoria e serialit nuk ekziston");
                    idkategoriseriali = kategori.ID;
                }
            }
           
            krijoSeriale(colserialemag, coltrupi, statusDokumenti, idNdermarrje, idperdoruesi, idperdoruesi, clsKonf,
                kontrollosasi, krijoseriale);
            int idFormatPrintimi = cmbFormatiPrintimit.Value != null ? int.Parse(cmbFormatiPrintimit.Value.ToString()) : 0;
            int operatori = cmbOperatori.Value != null ? int.Parse(cmbOperatori.Value.ToString()) : 0;
            int transportuesi = clsTransportues.merrIdTransportuesSipasEmertimit(btnTransportuesi.Text, idNdermarrje);

            var mesazh = mag.krijoMagazine(id, idnivel, clsKonf.IdKonfigAmbjente, idKlienti, kodklienti, idmagazina, magazina, dteDtDok.Date, txtNrDok.Text, 1, txtNrProjekti.Text, clsKonf.IdKategori,
                double.Parse(txtVlefta.Text), statusDokumenti, idNdermarrje, idnderviti, idperdoruesi, dteDtRegjistrimi.Date, idllojdokmag, txtShenime.Text, iddege, cmbDegeAdministrative.Text.Split(' ')[0],
                llog.IdLlogari, cmbLlogariKunderParti.Text, idnjesivartese, njesia, cbMeKonfirmim.Checked, idgrup, idgrup2, idgrup3, pershkrimi, magazinieri, adresa, coltrupi, magtransf, new clsKokaFleteKontabel(),
                kokaekzistuezerez.IdKokaRezervimi, out mesazhinformues, !(hfState.Get("merrMagazinatMeAutorizim").Equals("Jo")), idAutomjet, targa, idFormatPrintimi, idperdoruesi, dteDtTransporti.Date, 
                txtShoferi.Text, txtTarga2.Text, txtNIVFSH.Text, txtWTNIC.Text, operatori, hfArkiva, true, idkategoriseriali, null,false, txtNrSerial.Text, rezShitje,cbMallraTeDjegshme.Checked,cbShoqerimIKerkuar.Checked,cmbTipiMag.Text,cmbTransaksioni.Text, transportuesi);
            if (!mesazh.Status)
                throw new Exception(mesazh.PershkrimMesazhi);
            return mag;
        }

        private colTrupiMagazina krijoTrupinEMagazines(bool eshteTransferim, int shenja, bool meAutorizim, int idNdermarrje, int idPerdorues, DateTime dtDok, bool isOwnShop, string kodKonfigurimi, bool isKlonim, bool dalje, int idKonfigurimi, bool serialeNeDetajim, colSerialeUnikeKategori kategorite, bool bashkoArtikujt, bool lejoModifikimDetajimi)
        {
            var dokumenti = JsonConvert.DeserializeObject<Dictionary<string, object>[]>(gridDataObject.Value);
            var idMagTemp = -1;
            var isMagENjejte = false;
            var trupat = new colTrupiMagazina();
            var serialetUnike = mySessionObjects.MerrNgaSession<colSerialeUnikeMagazina>(Session, Constants.SERIALE_UNIKE_TE_NGARKUAR, guidString);
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
            bool ruajBarkod = clsAlternativaKushti.getAlternativa(idKonfigurimi, "RBART") == "Po";
            var merrMagazinenNgaTrupi = clsAlternativaKushti.getAlternativa(idKonfigurimi, "NKDMMT") == "Po";

            if (eshteTransferim && serialetUnike != null)
            {
                serialetUnike = serialetUnike.Clone();
                serialetUnike.ForEach(s => s.IdLlojDokumentMagazine = 1);
            }

            int loan = 0;

            if (!(dalje || !eshteTransferim || !serialeNeDetajim) && hfState.Get<bool>("GjeneruarNgaMema") && Request.QueryString["id"] != null)
            {
                int idgjenerues = clsKokaMagazina.merrIdGjenerues(int.Parse(Request.QueryString["id"]));
                clsKokaShitje shitje = new clsKokaShitje(idgjenerues);
                clsKlientFurnitor klient = new clsKlientFurnitor(shitje.IdKlientFurnitor, new clsDatabaseKontabilitet());

                if (klient.LlojPorosie == 3)
                    loan = 1;
            }

            foreach (var i in dokumenti)
            {
                var trupMag = new clsTrupiMagazina(idNdermarrje, idPerdorues, eshteTransferim, dtDok, i, isOwnShop, kodKonfigurimi, isKlonim, meAutorizim, ruajBarkod);
                if (trupMag.IdArtikulli <= 0) continue;

                if (trupMag.IdMag == -1)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgMagazinaNukEkziston", ci), pnlMesazhi, LoadingPanel);
                    return new colTrupiMagazina();
                }

                if (merrMagazinenNgaTrupi
                    || (!string.IsNullOrEmpty(btneMagazina.Text) && !eshteTransferim)
                    || (!string.IsNullOrEmpty(btneMagazina2.Text) && eshteTransferim))
                {
                    if (idMagTemp == -1)
                    {
                        idMagTemp = trupMag.IdMag;
                        isMagENjejte = true;
                    }
                    else if (isMagENjejte && trupMag.IdMag != idMagTemp)
                        isMagENjejte = false;
                }

                trupMag.Shenja = shenja;
                trupMag.MerrSerialetUnike(serialetUnike, dalje);

                if (dalje || !eshteTransferim || !serialeNeDetajim)
                    trupat.Add(trupMag);
                else
                {
                    var kat = kategorite.MerrKategoriSipasIdFormatit(((clsArtikulli)trupMag.Element).IdFormatSeriali);

                    if (!((clsArtikulli)trupMag.Element).DetajimArtikulli || kat == null || !kat.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.APARATE.ToString()))
                        trupat.Add(trupMag);
                    else
                    {
                        var trupaFHT = trupMag.ShperndaTrupinSipasSerialeve(idPerdorues, idNdermarrje, lejoModifikimDetajimi, loan);
                        trupat.AddRange(trupaFHT);
                    }
                }
            }

            if (!dalje && eshteTransferim && bashkoArtikujt)
                trupat.BashkoTrupin(kategorite);

            if (!eshteTransferim)
                btneMagazina.Text = string.Empty;

            if (eshteTransferim)
                btneMagazina2.Text = string.Empty;

            if (!isMagENjejte || trupat.Count <= 0)
                return trupat;

            var magazinaPerbashket = new clsNjesiAdministrative(trupat[0].IdMag, idPerdorues);

            if (!eshteTransferim)
                btneMagazina.SelectedItem = btneMagazina.Items.Add(magazinaPerbashket.Kodi + " (" + magazinaPerbashket.Pershkrimi + ")", trupat[0].IdMag.ToString());
            else
                btneMagazina2.SelectedItem = btneMagazina2.Items.Add(magazinaPerbashket.Kodi + " (" + magazinaPerbashket.Pershkrimi + ")", trupat[0].IdMag.ToString());

            return trupat;
        }

        private void krijoSeriale(
            colSerialetMagazine colserialemag, 
            colTrupiMagazina trupi, 
            int idstatusdok, 
            int idNdermarrje, 
            int idPerdoruesi, 
            int idKrijuesi, 
            clsKonfigurimAmbjenti konf, 
            bool kontrollosasi, 
            bool kontrollomag)
        {
            var cultinf = mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            var rreshti = 0;

            var serialetekzistuese = new colSerialetMagazine();
            if (hfShtimModifikim.Value == "modifikim")
            {
                var id = int.Parse(Request.QueryString["id"]);
                var kok = new clsKokaMagazina();
                kok.mbushKokaMagazinaSipasID(id);
                serialetekzistuese.merrSerialetMagazineSipasIDDokumenti(id, idNdermarrje, kok.IdKonfigAmbjente);
            }
            foreach (var trup in trupi)
            {
                var col = new colAQTSeriale();
                if (trup.IdLlojVeprimi == 1)//rasti artikull
                {
                    var art = (clsArtikulli)(trup.Element);
                    if (hfIdGride.Contains(rreshti.ToString()) && hfSeriale.Contains(trup.IdArtikulli + "_" + hfIdGride.Get(rreshti.ToString())))
                    {
                        var dokumenti = JsonConvert.DeserializeObject<Dictionary<string, object>[]>(hfSeriale.Get(trup.IdArtikulli + "_" + hfIdGride.Get(rreshti.ToString())).ToString());
                        foreach (var i in dokumenti)
                        {
                            var serial = new clsAQTSeriale(i);
                            col.Add(serial);
                            if (serialetekzistuese.Find(x => x.IdAQTSeriali == serial.IdAQTSerial && x.IdNjesiAdministrative == trup.IdMag) == null)
                                if (serial.IdNjesiAdministrativeAktuale != trup.IdMag && cmbLloji.Text != "FH" && kontrollomag)
                                {
                                    var mag = new clsNjesiAdministrative(trup.IdMag);
                                    throw new Exception("Seriali " + serial.AqtSerialKod + rm.GetString("msgNukNdodhetNeMagazine", ci) + mag.Kodi);
                                }
                            if (serial.IdAQTArt != trup.IdArtikulli)
                                throw new Exception("Serialet e artikullit " + trup.KodiArtikull + " jane marre gabim!");
                            var serialmag = new clsSerialetMagazine(0, 0, rreshti, trup.IdArtikulli, serial.IdAQTSerial, konf.IdNivel, konf.IdKonfigAmbjente, trup.IdMag, art.MeSerial ? 1 : (trup.Sasia * trup.Koeficenti), double.Parse(trup.Cmimi.ToString()), double.Parse(trup.Cmimi.ToString()) * (art.MeSerial ? 1 : trup.Sasia * trup.Koeficenti), idstatusdok, idNdermarrje, idPerdoruesi, idKrijuesi, 0);
                            colserialemag.Add(serialmag);
                        }
                    }
                    if (art.LlojiArt)
                    {
                        if (kontrollosasi && trup.Sasia * trup.Koeficenti != col.Count && art.MeSerial && col.Count != 0)
                            throw new Exception(rm.GetString("msgSasiaArtikullit", ci) + trup.KodiArtikull + rm.GetString("msgEshteENdryshmeNgaSeriaESerialeve", ci));
                        if (art.MeSerial && trup.Sasia * trup.Koeficenti != Math.Truncate(trup.Sasia * trup.Koeficenti))
                            throw new MyException("Nuk lejohet sasi me presje dhjetore per artikullin " + trup.KodiArtikull + " sepse eshte me serial!");
                    }
                }
                rreshti++;
            }
        }

        protected void ButtonOk5_Click(object sender, EventArgs e)
        {
            Page.Validate();
            var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            using (DbData dbData = new DbData())
            {
                switch (hfRuajDraft.Value)
                {
                    case "Ruaj":
                        ruajRegjistrimMagazine(1, false, false, false, idPerdoruesi, dbData);
                        break;

                    case "Draft":
                        ruajRegjistrimMagazine(0, false, false, false, idPerdoruesi, dbData);
                        break;

                    case "Refuzo":
                        ruajRegjistrimMagazine(4, false, false, false, idPerdoruesi, dbData);
                        break;

                    case "RuajPrint":
                        ruajRegjistrimMagazine(1, true, false, false, idPerdoruesi, dbData);
                        break;

                    default:
                        ruajRegjistrimMagazine(1, false, false, false, idPerdoruesi, dbData);
                        break;
                }
            }
            mbushHiddenFieldet(new colTrupiMagazina(), 1, 0, 0, idPerdoruesi);
        }

        /// <summary>
        /// Kontrollon nese jane plotesuar te gjitha fushat e detyrueshme te kokes se dokumentit
        /// </summary>
        private bool IsValidRegjistrimMagazine(int draft, clsKonfigurimAmbjenti konf)
        {
            var cultinf = mySessionObjects.ktheCultureInfo(Session);
            var rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            var idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            var idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
            if (dteDtRegjistrimi.Text == string.Empty)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, STR_zgjidhniDtRegj, pnlMesazhi, LoadingPanel);
                return false;
            }
            if (dteDtDok.Text == string.Empty)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgZgjidhniNjeDateDokumenti", ci), pnlMesazhi, LoadingPanel);
                return false;
            }
            if (dteDtDok.Date.Year != new clsNdermarrjeViti(mySessionObjects.ktheNdermarrjeVit(Session)).Viti)
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgDataNukPerketVititUshtrimor", ci), pnlMesazhi);
                return false;
            }
            var periudha = mySessionObjects.merrPeriudheKontabel(Session);
            string mesazhGabimi;
            if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, dteDtDok.Date, periudha, draft))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, mesazhGabimi, pnlMesazhi);
                return false;
            }

            if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(dteDtDok.Date, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, KategoriDokumenti.Magazina, konf.IdKonfigAmbjente))
            {
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, MessagesResource.Messages["msgPeriodIsClosed"], pnlMesazhi);
                return false;
            }

            if (cmbLlogariKunderParti.Text != string.Empty)
            {
                if (!clsLlogari.ekzistonLlogari(cmbLlogariKunderParti.Text, idNdermarrje))
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKjoLlogariNukEkziston", ci), pnlMesazhi);
                    return false;
                }
                if (clsLlogari.eshteLlogariAktive(cmbLlogariKunderParti.Text, idNdermarrje)) return true;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKjoLlogariNukEshteAktive", cultinf), pnlMesazhi);
                return false;
            }
            if (btneMagazina2.Text != string.Empty)
            {
                if (cmbKonfigurimi.Text.Split(';')[0] != "FDNV" && cmbKonfigurimi.Text.Split(';')[0] != "FHNV")
                    return true;
                if (clsNjesiVartese.ekziston(btneMagazina2.Text.Split(' ')[0], idNdermarrje)) return true;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgNjesiaVarteseNukEkziston", cultinf), pnlMesazhi);
                return false;
            }


            if (btneMagazina.Text != string.Empty)
            {
                var kodiMag = btneMagazina.Text.Split(' ')[0];
                var mag = new clsNjesiAdministrative(kodiMag, idNdermarrje, idPerdorues);
                if (mag.IdNjesiAdministrative == -1)
                {
					 return false;
                }
                mag = new clsNjesiAdministrative(kodiMag, idNdermarrje, idPerdorues);
                if (mag.Aktiv == false)
                {
					return false;
                }
            }
            if (cmbDegeAdministrative.Text != string.Empty && cmbDegeAdministrative.Text != " ()")
            {
                var dege = new clsDegeAdministrative(cmbDegeAdministrative.Text.Split(' ')[0], idNdermarrje);
                if (dege.IdDegeAdministrative == -1)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKjoDegeAdministrativeNukEkziston", cultinf), pnlMesazhi);
                    return false;
                }
                dege = new clsDegeAdministrative(cmbDegeAdministrative.Text.Split(' ')[0], idNdermarrje);
                if (dege.Aktiv == false)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgKjoDegeAdministrativeNukEshteAktive", cultinf), pnlMesazhi);
                    return false;
                }
            }
            if (btneMagazina2.Text == string.Empty) return true;
            {
                var mag = new clsNjesiAdministrative(btneMagazina2.Text.Split(' ')[0], idNdermarrje, idPerdorues);
                if (mag.IdNjesiAdministrative == -1)
                {
                    clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgMagazinaNukEkziston", cultinf), pnlMesazhi);
                    return false;
                }
                mag = new clsNjesiAdministrative(btneMagazina2.Text.Split(' ')[0], idNdermarrje, idPerdorues);
                if (mag.Aktiv) return true;
                clsMenuInfo.ShtoMesazhGabimi(MenuInfo, rm.GetString("msgMagazinaNukEshteAktive", cultinf), pnlMesazhi, LoadingPanel);
                return false;
            }
        }
        protected void ucEmerSkedari_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {
           
        }
        protected void btnruaj_Click(object sender, EventArgs e)
        {
            ruajJoNgaMenuja(true, mySessionObjects.ktheIdPerdoruesi(Session));
        }

        #region autocomplete combot

        protected void cmbLlogariKunderParti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback) return;
            if (Request.Params["__CALLBACKID"].Contains("cmbLlogariKunderParti"))
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbLlogariKunderParti, e);
        }
        protected void cmbLlogariKunderParti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (!IsCallback) return;
            if (Request.Params["__CALLBACKID"].Contains("cmbLlogariKunderParti"))
                ConfigureAspxComboBox.mbushComboLlogariaPaKolona(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session), cmbLlogariKunderParti, e);
        }

        /// <summary>
        /// Shtoi - Elsa. Combo klient/furnitorit me autocomplete
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void btneKlientFurnitori_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback) return;
            if (!Request.Params["__CALLBACKID"].Contains("btneKlientFurnitori")) return;
            var value = 0;
            if (e.Value == null || !int.TryParse(e.Value.ToString(), out value))
                return;
            ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitoriById(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session), (ASPxComboBox)source, value);
        }

        /// <summary>
        /// Shtoi - Elsa. Combo klient/furnitorit me autocomplete
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void btneKlientFurnitori_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (!IsCallback) return;
            if (Request.Params["__CALLBACKID"].Contains("btneKlientFurnitori"))
                ConfigureAspxComboBox.KonfiguroComboBoxKlientFurnitor(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, (ASPxComboBox)source, cmbLloji.Text.Split(';')[0], mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheGjuhe(Session), 0);
        }

        protected void btneAutomjeti_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback) return;
            if (!Request.Params["__CALLBACKID"].Contains("btneAutomjeti")) return;
            var value = 0;
            if (e.Value == null || !int.TryParse(e.Value.ToString(), out value))
                return;
            ConfigureAspxComboBox.mbushComboAutomjetiByID((ASPxComboBox)source, value);
        }

        protected void btneAutomjeti_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (!IsCallback) return;
            if (!Request.Params["__CALLBACKID"].Contains("btneAutomjeti")) return;
            var idNdermarrje = (int)hfState["idNdermarrje"];
            if (btneKlientFurnitori.Text != string.Empty)
            {
                var idKlienti = 0;
                if (btneKlientFurnitori.Value != null)
                    idKlienti = int.Parse(btneKlientFurnitori.Value.ToString());
                ConfigureAspxComboBox.mbushComboAutomjeteshMeKlient(idNdermarrje, idKlienti, btneAutomjeti, e);
            }
            else
                ConfigureAspxComboBox.mbushComboAutomjetesh(idNdermarrje, btneAutomjeti, e);
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
            ConfigureAspxComboBox.mbushComboMagazinatMeID(idNdermarrje, btneMagazina, idPerdoruesi, false, 0, true, value);
        }

        protected void btneMagazina2_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
        {
            if (!IsCallback) return;
            if (!Request.Params["__CALLBACKID"].Contains("btneMagazina2")) return;
            var idNdermarrje = (int)hfState["idNdermarrje"];
            var idPerdoruesi = (int)hfState["idPerdoruesi"];
            var value = 0;
            if (e.Value == null || !int.TryParse(e.Value.ToString(), out value))
                return;

            if (cmbKonfigurimi.Text.Split(';')[0] == "FDNV" || cmbKonfigurimi.Text.Split(';')[0] == "FHNV")
            {
                ConfigureAspxComboBox.mbushComboNjesiVarteseMeID(idNdermarrje, false, btneMagazina2, value);
            }
            else
                ConfigureAspxComboBox.mbushComboMagazinatMeID(idNdermarrje, btneMagazina2, idPerdoruesi, false, 0, hfState.Get("merrMagazinatMeAutorizim").ToString() != "Jo", value);
        }

        protected void btneMagazina_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (!IsCallback) return;
            if (!Request.Params["__CALLBACKID"].Contains("btneMagazina")) return;
            var idNdermarrje = (int)hfState["idNdermarrje"];
            var idPerdoruesi = (int)hfState["idPerdoruesi"];
            ConfigureAspxComboBox.mbushComboMagazinatMeFilter(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, btneMagazina, idPerdoruesi, idNdermarrje, false, 0, true);
        }

        protected void btneMagazina2_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
        {
            if (!IsCallback) return;
            if (!Request.Params["__CALLBACKID"].Contains("btneMagazina")) return;
            var idNdermarrje = (int)hfState["idNdermarrje"];
            var idPerdoruesi = (int)hfState["idPerdoruesi"];

            if (cmbKonfigurimi.Text.Split(';')[0] == "FDNV" || cmbKonfigurimi.Text.Split(';')[0] == "FHNV")
                ConfigureAspxComboBox.mbushComboNjesiVarteseMeFilter(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, idNdermarrje, btneMagazina2);
            else
                ConfigureAspxComboBox.mbushComboMagazinatMeFilter(e.Filter, e.BeginIndex + 1, e.EndIndex + 1, btneMagazina2, idPerdoruesi, idNdermarrje, false, 0, hfState.Get("merrMagazinatMeAutorizim").ToString() != "Jo");
        }

        #endregion autocomplete combot

        protected void serialetCallbackPanel_Callback(object sender, CallbackEventArgsBase e)
        {
            var serialetNeMagazine = mySessionObjects.MerrNgaSession<colSerialeUnikeMagazina>(Session, Constants.SERIALE_UNIKE_TE_NGARKUAR, hfState.Get<string>("guidString")) ?? new colSerialeUnikeMagazina();
            var serialetOld = serialetNeMagazine.Clone();

            int idDok = 0;
            int.TryParse(Request.QueryString["id"], out idDok);
            List<Tuple<int, string>> serialeNeFDTK = clsKokaMagazina.merrDetajimeNeFDTK(idDok);

            try
            {
                var dictionarySkedare = CacheLayer.GlobalCacheManager.MySessionCache.Get<Dictionary<string, HttpPostedFile>>("SkedareSerial");
                if (dictionarySkedare == null)
                    throw new MyException("Nuk ka seriale te ngarkuar");

                var idNdermarrje = hfState.Get<int>("idNdermarrje");
                var emerKategoria = hfState.Get<string>("kategoriSeriali");

                var kategori = new clsSerialeUnikeKategori(emerKategoria, idNdermarrje, true);
                if (kategori.ID < 0)
                    throw new MyException("Kategoria e serialit nuk ekziston");
                mySessionObjects.RuajNeSession<clsSerialeUnikeKategori>(Session, kategori, "kategoriId");
                var fushat = new colSerialeUnikeFusha(kategori.ID);

                foreach (var entry in dictionarySkedare)
                {
                    var dtSerialet = clsFunksione.LexoFileImportSerialeUnike(Session, entry.Value, kategori, fushat);
                    serialetNeMagazine.ShtoSeriale(idNdermarrje, dtSerialet, fushat, kategori);
                }
                List<clsArtikujMeSasi> artMeSas = serialetNeMagazine.MerrArtikujMeSasi(e.Parameter, (bool)hfState["MNSA"], true,false);

                var mesazh = serialetNeMagazine.KontrolloSerialeTePerseritur(kategori.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.RINGARKUES.ToString()) ? kategori.ID : -1);

                if (!mesazh)
                    throw new MyException(mesazh.PershkrimMesazhi);

                bool ekzistojneSerialet = true;
                if (serialeNeFDTK.Count > 0)
                    ekzistojneSerialet = serialetNeMagazine.KontrolloSerialet(serialeNeFDTK,false);

                if (!ekzistojneSerialet)
                {
                    throw new MyException(MessagesResource.Messages["serialiNukGjendetNeFDTK"]);
                }

                var artikujt = serialetNeMagazine.Where(s => s.IdSeti == 0)
                   .GroupBy(ac => new
                   {
                       ac.IdArtikulli//,
                                     //   ac.IdMag
                   }).Select(ac => new
                   {
                       IdArtikulli = ac.Key.IdArtikulli,
                       Sasia = ac.Sum(acs => acs.Sasia),
                   });


                mySessionObjects.RuajNeSession(Session, serialetNeMagazine, Constants.SERIALE_UNIKE_TE_NGARKUAR, guidString);
                serialetCallbackPanel.JSProperties.Add("cpArtikujSeriale", JsonConvert.SerializeObject(artikujt));
            }
            catch (Exception err)
            {
                mySessionObjects.RuajNeSession(Session, serialetOld, Constants.SERIALE_UNIKE_TE_NGARKUAR, guidString);
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                serialetCallbackPanel.JSProperties.Add("cpSerialMesazhGabimi", err.Message);
            }
            finally
            {
                CacheLayer.GlobalCacheManager.MySessionCache.Remove("SkedareSerial");
            }
        }
        private object ktheObjektPerNotify(clsKokaMagazina kokaMagazina, string statusi, colTrupiShitje artikujt, clsTrupiShitje trupiShitje, string pershkrim, string kodErrori, string TipFature, string xml,string xmlResponse)
        {
            string emerDatabaze = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar();
            clsKokaMagazina kokeMagazine = new clsKokaMagazina(kokaMagazina.IdKokaMagazina);
            clsDegeAdministrative degeAdministrative = new clsDegeAdministrative(kokeMagazine.IdDegeAdministrative);
            string kodiINjesiseSeBiznesit = clsDegeAdministrative.ktheDegeAdministrativeSipasiD(kokeMagazine.IdDegeAdministrative)["KODNJESIEBIZNES"].ToString();
            clsNdermarrje ndermarrje = new clsNdermarrje(kokeMagazine.IdNdermarrje);
            clsTransportues transportues = new clsTransportues(kokeMagazine.Transportuesi);
            var emerOperatori = clsOperator.MerrEmerDheMbiemerOperatoriSipasId(kokeMagazine.IdOperator, kokeMagazine.IdNdermarrje);
            return new
            {
                docNo = kokeMagazine.NrDok,
                issuer = new
                {
                    nuis = TipFature,
                    organization = emerDatabaze,
                    name = ndermarrje.NdermarrjeKodi
               
                },
                linkUrl = "",
                message = pershkrim,
                receiver = new
                {
                    nuis = "",
                    name = ""
                },
                status = statusi,
                type = TipFature,
                xml = new
                {
                    Request = xml,
                    Response = xmlResponse
                }

            };
        }

    }
}