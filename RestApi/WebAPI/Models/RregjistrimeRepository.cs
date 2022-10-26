using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.SessionState;
using DbCore;
using DbCore.DbAdmin;
using DbCore.DbAsete;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbProdhimi;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using Newtonsoft.Json;
using DbCore.DbArkaBanka;
using System.Resources;
using System.Web.Script.Serialization;
using DbCore.IMBUtils.Logging;
using System.Web;
using System.Reflection;
using DbCore.IMBUtils;
using DbCore.IMBUtils.Messages;
using System.Collections;
using DbCore.IMBUtils.Cache;
using System.Diagnostics;
using DbCore.DbImporte;
using DbCore.IMBUtils.DataBase;
using DbCore.DbQendraKosto;
using DbCore.DbBuxheti;
using CacheLayer;
using DbCore.IMBUtils.Extensions;
using System.Net;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using DbCore.IMBUtils.Fiskalizimi.API;
using System.Web.Configuration;
using Newtonsoft.Json.Linq;

namespace RestApi.WebAPI.Models
{
    public class RregjistrimeRepository
    {
        public static object[] CelSerialNqsNukEkziston(string kodserial, int idndermarje, int idperdoruesi, int idartikulli, int lastsel, string serialeekzistuese, string[] serialeteperdoruraNeKeteFature, bool celnqsnukekziston, string magazina, double sasishuma, int iddokmodmagazine)
        {
            object[] result = new object[5];
            result[0] = idartikulli;
            result[2] = "";
            result[4] = lastsel;
            bool ekzistonneKolection = false;
            //clsArtikulli art = new clsArtikulli(idartikulli);
            bool MeSerial = clsArtikulli.EshteMeSerial(idartikulli);
            colAQTSeriale colzgjedhur = new colAQTSeriale();
            if (serialeekzistuese != "")
            {
                colAQTSeriale dokumenti = JsonConvert.DeserializeObject<colAQTSeriale>(serialeekzistuese);
                for (int i = 0; i < dokumenti.Count; i++)
                {
                    clsAQTSeriale serial = dokumenti[i];
                    colzgjedhur.Add(serial);
                    if (serial.AqtSerialKod == kodserial)
                        ekzistonneKolection = true;
                }
            }
            if (!ekzistonneKolection && !MeSerial && colzgjedhur.Count == 1 && magazina != "")
                result[2] = "Nuk mund te shtoni me shume se nje serial per artikujt me seriale te ndashem!";
            for (int s = 0; s < serialeteperdoruraNeKeteFature.Length; s++)
            {
                var dokumenti = JsonConvert.DeserializeObject<colAQTSeriale>(serialeteperdoruraNeKeteFature[s]);
                for (int i = 0; i < dokumenti.Count; i++)
                {
                    clsAQTSeriale serial = dokumenti[i];
                    if (serial.AqtSerialKod == kodserial)
                    {
                        if (MeSerial)
                        {
                            result[2] = "Ky serial eshte perdorur ne kete fature!";
                            ekzistonneKolection = true;
                        }
                        else
                        {
                            clsHistorikAQTSeriale historik = new clsHistorikAQTSeriale();
                            historik.merrHistorikAQTSerialSipasID(serial.IdHistorikAktualPaSerial, idndermarje);
                            double sasidokmod = 0;
                            if (iddokmodmagazine > 0)
                                sasidokmod = clsSerialetMagazine.merrSerialetMagazineSipasIDSerialIDDokSasi(iddokmodmagazine, serial.IdAQTSerial, idndermarje);
                            if (historik.SasiaProgresive + sasidokmod > sasishuma)
                                continue;
                            else
                            {
                                result[2] = "Ky serial eshte perdorur ne kete fature!";
                                ekzistonneKolection = true;
                            }
                        }
                    }
                }
            }
            if (!ekzistonneKolection)
            {
                if (clsAQTSeriale.kontrolloEkzistonAQTSerial(kodserial, idndermarje))
                {
                    clsAQTSeriale seriali = new clsAQTSeriale();
                    seriali.merrAQTSerialSipasKodAQT(kodserial, idndermarje);
                    if (seriali.IdAQTArt != idartikulli)
                        result[2] = "Seriali i perket nje artikulli tjeter!";
                    else if (celnqsnukekziston && clsAQTSeriale.kaveprimeAQTSerial(seriali.IdAQTSerial, idndermarje))
                        result[2] = "Ky serial eshte perdorur ne veprime!";
                    else
                    {
                        if (!celnqsnukekziston)
                        {
                            int idmagazina = clsNjesiAdministrative.ktheIdMagazine(magazina, idndermarje);
                            if (idmagazina != seriali.IdNjesiAdministrativeAktuale && magazina != "")
                                result[2] = "Ky serial nuk ndodhet ne kete magazine!";
                            else colzgjedhur.Add(seriali);
                        }
                        else
                            colzgjedhur.Add(seriali);
                    }
                }
                else
                {
                    if (celnqsnukekziston)
                    {
                        clsAQTSeriale seriali = new clsAQTSeriale();
                        seriali.AqtSerialKod = kodserial;
                        seriali.AqtSerialPershkrim = kodserial;
                        seriali.IdPerdoruesi = idperdoruesi;
                        seriali.IdNdermarrje = idndermarje;
                        seriali.IdStatusDokumenti = 1;
                        seriali.IdKrijuesi = idperdoruesi;
                        seriali.IdAQTArt = idartikulli;
                        seriali.MeSerialPerCope = !MeSerial;
                        seriali.IdNjesiAdministrativeAktuale = 0;
                        seriali.IdHistorikAktualPaSerial = 0;

                        seriali.HistorikSeriali = new clsHistorikAQTSeriale();
                        if (seriali.MeSerialPerCope)
                            seriali.HistorikSeriali = new clsHistorikAQTSeriale(seriali.IdAQTSerial, String.Empty, 0, 0, 0, 1, 0, 0, 1, seriali.IdNdermarrje, seriali.IdPerdoruesi, seriali.IdPerdoruesi, seriali.DtKrijimi, seriali.DtModifikimi);
                        clsMesazh mesazh = new clsMesazh();

                        mesazh = seriali.ruaj();
                        if (mesazh.Status)
                        {
                            colzgjedhur.Add(seriali);
                        }
                        else
                        {
                            result[2] = mesazh.PershkrimMesazhi;
                        }
                    }
                    else result[2] = "Seriali nuk ekziston!";
                }
            }
            result[3] = colzgjedhur.Count;
            result[1] = JsonConvert.SerializeObject(colzgjedhur);
            return result;
        }

        public static colInfoTrupi getInfoArtStructure(int idkoka, int idNdermarrje)
        {
            colInfoTrupi trupatvis = colInfoTrupi.merrInfoSipasIdKokaDheVisibleNew(idkoka, true, idNdermarrje);
            return trupatvis;
        }
        public static decimal ktheNormeTvsh(int idtvsh)
        {
            return clsTaksa.ktheNormePerqindjeMeId(idtvsh);

        }
        public static clsEtapeAprovimi gjejEtapeDokumenti(int idperdoruesi, int idDokumenti, string lloji)
        {
            clsEtapeAprovimi etapa = new clsEtapeAprovimi();
            switch (lloji)
            {
                case "shitje":
                    etapa.ktheEtapeFunditSipasKokaShitjeDhePerdorues(idDokumenti, idperdoruesi);
                    break;
                case "arka":
                    etapa.ktheEtapeFunditSipasKokaVeprimeBankaDhePerdorues(idDokumenti, idperdoruesi);
                    break;
                case "planifikimEkzekutimi":
                    etapa.ktheEtapeFunditSipasIdKokaBuxhetiDhePerdorues(idDokumenti, idperdoruesi);
                    break;
                default:
                    throw new MyException($"LLoji {lloji}  eshte i pa percaktuar ne skemat e aprovimeve");
            }

            return etapa;
        }

        public static bool KontrolloFaturaPaprintuara(int idndermarje, int idperdorues, int iddege,int idnivel)
        {
            colPrintimeNeKase col = new colPrintimeNeKase();
            col.ktheDergimeKaseSipasIdShopIdNdermDheStatus(iddege, false, idperdorues, idndermarje, idnivel);
            return col.Count > 0;
        }
        public static bool KontrolloFaturaBankaPaprintuara(int idndermarje, int idperdorues, int iddege)
        {
            colPrintimeNeKase col = new colPrintimeNeKase();
            col.ktheDergimeKaseSipasIdShopIdNdermDheStatusBanka(iddege, false, idperdorues, idndermarje);
            return col.Count > 0;
        }
        public static string kontrolloKonvertuarDheKase(object[][] id, string lloji, string kodkonfig, int idNdermarrje, int idGjuha)
        {
            ResourceManager rm = MessagesResource.CurrentResourceManager;
            CultureInfo ci = MessagesResource.KtheCultureInfo(idGjuha);
            clsKonfigurimAmbjenti konfigurim = new clsKonfigurimAmbjenti();
            konfigurim.mbushKonfigAmbjSipasKod(kodkonfig, idNdermarrje);
            colKokaShitje colShitjet = new colKokaShitje();
            colKokaMagazina colMagazinat = new colKokaMagazina();
            object[] ids = new object[id.Length];
            for (int i = 0; i < id.Length; i++)
            {
                ids[i] = id[i][0];
            }
            if (konfigurim.IdKategori != 51 && konfigurim.IdKategori != 207)
            {
                colShitjet = new colKokaShitje(String.Join(",", ids));
            }
            else
            {
                colMagazinat = new colKokaMagazina(String.Join(",", ids));
            }
            string mesazh = "";
            string ngjyra = String.Empty, ngjyra2 = String.Empty, ngjyra3 = String.Empty;
            if (konfigurim.IdKategori != 51 && konfigurim.IdKategori != 207)
            {
                bool kase = kontrolloPrintuarKase(id, lloji);
                if (kase)
                    mesazh += rm.GetString("regjisDokMsgFaturaEshtePrintNeKase", ci);
                else mesazh = "Jeni i sigurt? ";
                for (var i = 0; i < colShitjet.Count; i++)
                {
                    clsKokaShitje koka = colShitjet[i];
                    ngjyra = clsKokaShitje.merrNgjyreKonvertime(false, koka.IdNdermarrje, koka.IdShitjeKoka);
                    ngjyra2 = clsKokaShitje.merrNgjyreKonvertimeMag(koka.IdNdermarrje, koka.IdShitjeKoka);
                    ngjyra3 = clsKokaShitje.merrNgjyreKonvertime(true, koka.IdNdermarrje, koka.IdShitjeKoka);
                    if ((!string.IsNullOrEmpty(ngjyra) && ngjyra != "gri") || (!string.IsNullOrEmpty(ngjyra2) && ngjyra2 != "gri") || (!string.IsNullOrEmpty(ngjyra3) && ngjyra3 != "gri"))
                        mesazh += "Dokumenti " + koka.NrDok + " eshte i konvertuar. ";
                }
            }
            else
            {
                mesazh = "Jeni i sigurt? ";
                for (var i = 0; i < colMagazinat.Count; i++)
                {
                    clsKokaMagazina kokamag = colMagazinat[i];
                    ngjyra = clsKokaMagazina.merrNgjyreKonvertime(kokamag.IdNdermarrje, kokamag.IdKokaMagazina);
                    if (ngjyra != "gri" && !string.IsNullOrEmpty(ngjyra))
                        mesazh += "Dokumenti " + kokamag.NrDok + " eshte i konvertuar. ";
                }
            }
            if (mesazh == "Jeni i sigurt? ")
                mesazh = "";
            return mesazh;
        }
        public static bool kontrolloPrintuarKase(object[][] id, string lloji)
        {
            for (int i = 0; i < id.Length; i++)
            {
                string alternativa = clsAlternativaKushti.getAlternativa(Convert.ToInt32(id[i][1]), "MMK");
                if (clsAlternativaKushti.getAlternativa(Convert.ToInt32(id[i][1]), "DK") == "File")
                {
                    if (alternativa == "Po" && (bool)id[i][2])
                        return true;
                }
                else
                {
                    DbCore.DbInventari.clsPrintimeKase print = new DbCore.DbInventari.clsPrintimeKase();
                    print.merrDergimeKaseSipasIdDok(Convert.ToInt32(id[i][0]), lloji);
                    if (alternativa == "Po" && (bool)id[i][2] && print.Derguar)
                        return true;
                }
            }
            return false;
        }
        public static Object KonvertoAuto(int[] ids, string pageId, HttpSessionState Session)
        {
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            int idGjuha = mySessionObjects.ktheGjuhe(Session);
            colKokaShitje colShitjetTeSelektuaraPerAutoKonvert = new colKokaShitje();
            colShitjetTeSelektuaraPerAutoKonvert = new colKokaShitje(String.Join(",", ids));
            colShitjetTeSelektuaraPerAutoKonvert.MbushTrupat();


            DataTable err = new DataTable();
            err.Columns.Add("Kodi");
            err.Columns.Add("Gabimi");
            err.Columns.Add("Rreshti");
            int nrreshta = 0; 

            foreach (clsKokaShitje dok in colShitjetTeSelektuaraPerAutoKonvert)
            {
                nrreshta++;
                int idshitjekoka = dok.IdShitjeKoka; 
                clsKonfigurimAmbjenti konfDok = new clsKonfigurimAmbjenti(dok.IdKonfigAmbjente);

                bool kva = clsAlternativaKushti.getAlternativa(dok.IdKonfigAmbjente, "KVA") == "Po";
                if (!kva)
                {
                    object[] arr = { konfDok.KodKonfigAmbjente + " " + dok.NrDok + " " + dok.DtDok.ToShortDateString(), MessagesResource.Messages["msgNukMundTeKonvertAuto"], nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }

                int idkonfigKVDN = clsKusht.kthevlereSipasKushtitDheIdKonfig(dok.IdKonfigAmbjente, "KVDN");
                if (idkonfigKVDN == 0)
                {
                    object[] arr = { konfDok.KodKonfigAmbjente + " " + dok.NrDok + " " + dok.DtDok.ToShortDateString(), MessagesResource.Messages["msgPaLlojPercaktuar"], nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }

                clsKonfigurimAmbjenti konfigKVDN = new clsKonfigurimAmbjenti(idkonfigKVDN);
                if (clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(dok.IdNivel) != konfigKVDN.IdKategori)
                {
                    object[] arr = { konfDok.KodKonfigAmbjente + " " + dok.NrDok + " " + dok.DtDok.ToShortDateString(), MessagesResource.Messages["msgKategTeNdryshmeKonvertimi"], nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }
                int[] id = { idshitjekoka };
                ListeDokKontrolloKonvertuar obj = KontrolloKonvertuar(id, konfigKVDN.KodKonfigAmbjente, idNdermarrje, idPerdoruesi, idGjuha, pageId ); 
                if (obj.mesazh != "Nuk jane konvertuar")
                {
                    object[] arr = { konfDok.KodKonfigAmbjente + " " + dok.NrDok + " " + dok.DtDok.ToShortDateString(), obj.mesazh, nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }

                #region konverto dok 
                int idNderVit = mySessionObjects.ktheNdermarrjeVit(Session);
                int idPeriudheKontabel = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(DateTime.Today, idNdermarrje);
                CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
                ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
                
                bool gjeneroDokMag = clsAlternativaKushti.getAlternativa(idkonfigKVDN, "GJDM") == "Po";

                colSerialetMagazine serialeMag = new colSerialetMagazine();
                clsKusht kushtamor = new clsKusht(idkonfigKVDN, "ZDAM");
                clsKonfigurimAmbjenti konfamortizimi = new clsKonfigurimAmbjenti(kushtamor.Vlera, idGjuha);
                clsKokaShitje faturashitjengaurdhershitjamekupontatimor = new clsKokaShitje();
                string dokKodMonedhe = new clsMonedha(dok.IdMonedha).KodiMonedha;
                clsKonfigurimAmbjenti konfmag = new clsKonfigurimAmbjenti(konfigKVDN.IdKonfigurimi, idGjuha);
                int idMag = 0;
                string kodMag = String.Empty;
                bool mekontabilizim = (dok.IdStatusDok == 1); //nese nuk eshte draft do gjeneroje kontabilizim perndryshe jo    
                string shfaqmesazhapolupe = "jo";
                string shfaqmesazhapolupemagazina = "jo";//per rastet nqs do merret parasysh magazina ne shfaqen e mesazhit
                string shfaqmesazhapolupebanka = "jo";
                string shfaqmesazhapolupeVDK = "jo";
                string mesazhinformues = string.Empty;
                clsKokaQendraKosto qend = new clsKokaQendraKosto();
                clsKokaShitje kokaMeme = new clsKokaShitje();
                bool gjeneroMeme = clsAlternativaKushti.getAlternativa(idkonfigKVDN, "GJOSHM") == "Po";
                bool eshteMeme = mySessionObjects.merrEshteMemeSesioni(Session);
                bool gjeneroBij = clsAlternativaKushti.getAlternativa(idkonfigKVDN, "GJUBB") == "Po" || clsAlternativaKushti.getAlternativa(idkonfigKVDN, "GJFBB") == "Po";
                string kodGrup1 = new clsGrupimDokumentiKoka(dok.IdGrup1).Kodi;
                bool faturePermbledhese = false, kontrollEkzistence = false, blerenNgaDealeri = false, kontrolloGjendje = false;
                bool tollona = clsAlternativaKushti.getAlternativa(idkonfigKVDN, "RSHTT") == "Po";
                bool autoKlient = clsAlternativaKushti.getAlternativa(idkonfigKVDN, "LAVK") == "Po";
                bool tollonaKastrati = clsAlternativaKushti.getAlternativa(idkonfigKVDN, "RSHTTK") == "Po";
                bool tollonaKastratiElektronik = clsAlternativaKushti.getAlternativa(idkonfigKVDN, "RSHTTKE") == "Po";
                string llojZevendesimi = clsAlternativaKushti.getAlternativa(idkonfigKVDN, "ZT");
                bool zevendesimtollona = !(llojZevendesimi == "Jo");
                bool krijoArtRi = clsAlternativaKushti.getAlternativa(idkonfigKVDN, "BKAR") == "Po";
                clsKusht kushtVfone = new clsKusht(idkonfigKVDN, "ZDVFONE");
                clsKonfigurimAmbjenti konfVfone = new clsKonfigurimAmbjenti(kushtVfone.Vlera);
                var serialetUnike = mySessionObjects.MerrNgaSession<colSerialeUnikeMagazina>(Session, Constants.SERIALE_UNIKE_TE_NGARKUAR);
                bool zevendesimtollonakastrati = clsAlternativaKushti.getAlternativa(idkonfigKVDN, "ZTK") == "Po";

                clsMesazh mesazh = new clsMesazh(true);
                string serverUrl = "", mesazhmevonshem = "";
                clsVeprimBankaKoka veprimeBanka = new clsVeprimBankaKoka();
                clsKusht kushtZSP = new clsKusht(idkonfigKVDN, "ZSP");
                bool dergoemail = clsAlternativaKushti.getAlternativa(idkonfigKVDN, "LE") == "Po";
                bool dergoemailVFOne = clsAlternativaKushti.getAlternativa(idkonfigKVDN, "DEVFOne") == "Po";
                bool eshteOwn = mySessionObjects.merrEshteOwnSesioni(Session);
                bool printofature = false, printogarancifature = false, pageseFature = false, ruajRenditje = false;
                bool kontrolloSasiKonvertimiDheKthimi = clsAlternativaKushti.getAlternativa(idkonfigKVDN, "NK") == "Po";
                bool kontrolloIMEIFifo = clsAlternativaKushti.getAlternativa(idkonfigKVDN, "AFI") == "Po";
                bool isShitje = (konfigKVDN.IdKategori == 1);
                bool konvertim = isShitje;
                bool konvertimBlerje = !isShitje;
                bool merrSipasGrupit = clsAlternativaKushti.getAlternativa(idkonfigKVDN, "AASG") == "Po";
                bool merrDhurata = clsAlternativaKushti.getAlternativa(idkonfigKVDN, "AADH") == "Po";
                string veprimi = isShitje ? "konvertim" : "konvertimblerje";
                double totaliPaTvsh = 0, totaliMeTvsh = 0;
                bool mosPerfshiArtSherbim = clsAlternativaKushti.getAlternativa(dok.IdKonfigAmbjente, "MPASHGJK") == "Jo";
                bool lejoMagNdryshme = clsAlternativaKushti.getAlternativa(dok.IdKonfigAmbjente, "LMNGB") == "Po";
                bool lejoSasiPozitiveKthim = clsAlternativaKushti.getAlternativa(dok.IdKonfigAmbjente, "LSPK") == "Po";
                int idStatusDokKonvert = clsAlternativaKushti.getAlternativa(dok.IdKonfigAmbjente, "SDKA") == "Ruajtur" ? 1 : 0;
                colTrupiShitje trupat = new colTrupiShitje();
                int idMagTemp = -1;
                bool isMagENjejte = true;
                var i = 1;
                foreach (var tr in dok.OColTrupiShitje)  
                {

                    if (tr.IdLlojVeprimi == 1 && mosPerfshiArtSherbim)
                        if (new clsArtikulli(tr.IdKodi).Klasa == 3)
                            continue; // kur kushti MPASHGJK eshte Jo, nuk perfshihen artikujt sherbim ne dok e konvertuar

                    clsTrupiShitje trupi = new clsTrupiShitje(idNdermarrje, idPerdoruesi, isShitje, konvertim, eshteMeme, merrSipasGrupit, kodGrup1, merrDhurata, eshteOwn, gjeneroDokMag, false, false, veprimi, tollona, tollonaKastrati, konvertimBlerje, zevendesimtollonakastrati, false, blerenNgaDealeri, tr.Kodi, tr.IdLlojVeprimi == 1 ? "Artikull" : "Llogari", tr.IdKodi, tr.IdShitjeTrupi, tr.IdTrupiKonvertimi, tr.IdTrupiKonvertimBlerje, tr.IdTrupiRezervimi, tr.IdTrupiTransferimi, tr.IdTrupiKthim, tr.Pershkrimi, tr.KodDetajim1, tr.KodDetajim2, tr.IdNjesia, tr.Sasia, tr.SasiRez, tr.Gjeresi, tr.Gjatesi, tr.SasiPermasa, tr.Cmimi, tr.Zbritje, tr.LlojZbritje, tr.ZbritjeVlere, tr.VleftaPaTvsh, tr.Tvsh, tr.VleftaMeTvsh, tr.IdMagazina, tr.Shenime, tr.NrLlogShpenzimi, tr.DtFillimi, tr.DtMbarimi, tr.Shenime2, tr.IdBarkodi, tr.IdKategoriShpenzimi, konfigKVDN.KodKonfigAmbjente, i, lejoMagNdryshme, dok.DtDok, lejoSasiPozitiveKthim);
                    i++;
                    totaliPaTvsh += tr.VleftaPaTvsh;
                    totaliMeTvsh += tr.VleftaMeTvsh; 

                    if (string.IsNullOrEmpty(trupi.Kodi))
                    {
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = "Dokumenti nuk mund te permbaje rreshta pa artikull/llogari!";
                    }
                    if (idMagTemp == -1)
                        idMagTemp = trupi.IdMagazina;
                    else
                        if (isMagENjejte && trupi.IdMagazina != idMagTemp)
                        isMagENjejte = false;

                    trupat.Add(trupi);
                }
                if (trupat.Count == 0)
                {
                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = MessagesResource.Messages["msgTrupiDokNukDuhetBosh"];
                } 

                if (!mesazh.Status)
                {
                    object[] arr = { konfDok.KodKonfigAmbjente + " " + dok.NrDok + " " + dok.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }
                if (isMagENjejte && trupat.Count > 0)
                {
                    clsNjesiAdministrative magazinaPerbashket = new clsNjesiAdministrative(trupat[0].IdMagazina, idPerdoruesi);
                    kodMag = magazinaPerbashket.Kodi;
                }

                double tvsh = (totaliMeTvsh - totaliPaTvsh) * (1 - dok.PerqindjeZbritje/100);

                clsKokaShitje shitjaKonvert = new clsKokaShitje();
                var merrMagazinePerberesi = clsAlternativaKushti.getAlternativa(konfigKVDN.IdKonfigAmbjente, "KGJAPMR") == "Po";
                try
                {
                    mesazh = shitjaKonvert.krijoShitje(ref gjeneroDokMag, konfigKVDN.IdNivel, dok.IdTemplate, idkonfigKVDN, dok.IdKlientFurnitor > 1 ? dok.IdKlientFurnitor : 0, dok.EmerKlienti, dok.IdProjekt, dok.NrProjekt, DateTime.Today, dok.NrDok, dok.NrSerial, dok.DtMaturimi, dok.IdMonedha, dokKodMonedhe, dok.Kursi, dok.IdMenyreTransporti, dok.KodMenyreTransporti, dok.DtTransportimi, dok.IdKushtDergimi, dok.KodKushtDergimi, dok.IdAgjent, dok.KodAgjenti, dok.IdMenyrePagese, dok.KodMenyrePagese == null ? "" : dok.KodMenyrePagese, dok.IdKushtPagese, dok.KodKushtPagese == null ? "" : dok.KodKushtPagese, dok.Zbritje, totaliMeTvsh, tvsh, DateTime.Today, idStatusDokKonvert, idNdermarrje, idNderVit, 0, 0, 0, 0, dok.AdresaFaturimit, dok.AdresaDergimit, dok.Pershkrimi, dok.Dogana, dok.IdDegeAdministrative, dok.KodDegeAdministrative == null ? "" : dok.KodDegeAdministrative, dok.IdPikeShitjeFurnizimi, dok.KodPikeShitje == null ? "" : dok.KodPikeShitje, idPerdoruesi, dok.IdRaportDesing, trupat, isShitje, konfigKVDN.KodKonfigAmbjente, idPeriudheKontabel, konfmag, idMag, kodMag, mekontabilizim, dok.IdGrup1, dok.IdGrup2, dok.IdGrup3, dok.AfatKohor, dok.Cash, dok.StatusAprovimi, idPerdoruesi, dok.PerqindjeAgjenti, out shfaqmesazhapolupe, dok.HfArkiva, new colTrupiQendraKosto(), 0, out mesazhinformues, gjeneroMeme, kokaMeme, 0, 0, eshteMeme, gjeneroBij, StatusTrasferimi.PaTransferuar, dok.EmerKlienti, dok.Kontakti, dok.Kase, dok.Kupon, kodGrup1 == null ? "" : kodGrup1, dok.DtFillimi, dok.DtMbarimi, dok.IdAutomjet, dok.KilometraAuto, dok.Targa, dok.IdAgjenti2, dok.PerqindjeAgjenti2, dok.KodAgjenti2, dok.IdAgjenti3, dok.PerqindjeAgjenti3, dok.KodAgjenti3, dok.Marresi, dok.IdTransportues, dok.EmertimTr == null ? "" : dok.EmertimTr, faturePermbledhese, faturashitjengaurdhershitjamekupontatimor, dok.ShpenzimeJoTeZbritshme, dok.IdArka, kontrollEkzistence, tollona, autoKlient, false, dok.DtFature, false, tollonaKastrati, tollonaKastratiElektronik, dok.MuajRaportimi, dok.IdVitRaportimi, dok.Shoferi, dok.TargaShoferit == null ? "" : dok.TargaShoferit, dok.ZbritjeNeVlere, dok.PerqindjeZbritje, dok.IdKarta, dok.Pike, dok.OColFazat, dok.IdFaza, dok.ColKlienteFurnitoreVartes, DateTime.Now, new DbData(), llojZevendesimi, dok.Koordinata, blerenNgaDealeri, krijoArtRi, idGjuha, konfVfone, -1, dok.NiptK, dok.QytetiK, kontrolloGjendje, dok.IdKategoriSeriali, serialetUnike, dok.Shenime2, dok.KartaPaPagese, 0, false, 0, dok.IdMarreveshje, StatusMarreveshje.Inaktive, dok.KerkuarNga ,"shtim",dok.DateKerkese,merrMagazinePerberesi, dok.NrDok, "", "", 0,"","","",0,0,"");
                } catch (Exception exp)
                {
                    ImbLogger.Error(exp);
                    mesazh.PershkrimMesazhi = exp.Message;
                    mesazh.Status = false;
                }
                if (!mesazh.Status)
                {
                    object[] arr = { konfDok.KodKonfigAmbjente + " " + dok.NrDok + " " + dok.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }

                try
                {
                    DbData dbData = new DbData();

                    mesazh = shitjaKonvert.ruaj(idGjuha, serverUrl, isShitje, null, idPeriudheKontabel, new colKonvertimi(), gjeneroDokMag, out veprimeBanka, kushtZSP.Vlera, StatusAprovimi.Undefined, 0, out shfaqmesazhapolupemagazina, out shfaqmesazhapolupebanka, out shfaqmesazhapolupeVDK, kokaMeme, 0, 0, dergoemail, eshteOwn, dergoemailVFOne, "", serialeMag, konfamortizimi, faturashitjengaurdhershitjamekupontatimor, out printofature, out printogarancifature, out pageseFature, mekontabilizim, out shfaqmesazhapolupe, konfigKVDN.KodKonfigAmbjente, false, string.Empty, 0, tollona, zevendesimtollona, false, false, "", "", "", tollonaKastrati, tollonaKastratiElektronik, false, "", false, false, zevendesimtollonakastrati, ruajRenditje, kontrolloSasiKonvertimiDheKthimi, new colKokaShitje(), kontrolloIMEIFifo, blerenNgaDealeri, !konfigKVDN.KodKonfigAmbjente.Contains("USHmag"), ref dbData, krijoArtRi, "", "", false, out mesazhmevonshem, false,true,"","");
                } catch (Exception exp)
                {
                    ImbLogger.Error(exp);
                    mesazh.PershkrimMesazhi = exp.Message;
                    mesazh.Status = false;
                }
                if (!mesazh.Status)
                {
                    object[] arr = { konfDok.KodKonfigAmbjente + " " + dok.NrDok + " " + dok.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, nrreshta };
                    err.Rows.Add(arr);
                    continue;
                }
                //else u konvertua

                #endregion

            }

            mySessionObjects.ruajTabeleGabimeshImporti(Session, err);

            bool kaGabime = false;
            if (err.Rows.Count > 0)
                kaGabime = true;
            int nrRreshtaMeGabime = err.Rows.Count;
            int nrReshtaKonvertuar = nrreshta - nrRreshtaMeGabime;

            return new
            {
                kaGabime = kaGabime, nrReshtaKonvertuar = nrReshtaKonvertuar, nrRreshtaMeGabime = nrRreshtaMeGabime
            };
        }

        public static ListeDokKontrolloKonvertuar KontrolloKonvertuar(int[] ids, string kodkonfig, int idNdermarrje, int idPerdoruesi, int idGjuha, string pageId)
        {
            if (ids.Length == 0)
                return new ListeDokKontrolloKonvertuar { nivelet = new colNivelRegjistrimi(), mesazh = String.Empty, ids = ids, colKonfig = new colKonfigurimAmbjenti() };

            string mesazh = "";
            bool merrKonvert1 = false;
            bool merrkonvert2 = false;
            bool merrkonvert3 = false;
            DateTime datamax = DateTime.MinValue;
            int idmax = -1;
            int index = 0;

            //clsKokaShitje koka = new clsKokaShitje();
            clsNivelRegjistrimi niveliaktual = new clsNivelRegjistrimi();
            clsKonfigurimAmbjenti konfigurim = new clsKonfigurimAmbjenti();
            konfigurim.mbushKonfigAmbjSipasKod(kodkonfig, idNdermarrje);
            colKokaShitje colShitjet = new colKokaShitje();
            colKokaMagazina colMagazinat = new colKokaMagazina();
            int idniveli = konfigurim.IdNivel;
            int idNivelKonvert;
            int idkonfigKonvert = 0;
            
            if (konfigurim.IdKategori != 51 && konfigurim.IdKategori != 207)
            {
                colShitjet = new colKokaShitje(String.Join(",", ids));
                if (colShitjet.Count == 0)
                    return new ListeDokKontrolloKonvertuar { nivelet = new colNivelRegjistrimi(), mesazh = "Dokumenti qe doni te konvertoni mund te jete modifikuar nga nje perdorues tjeter! Ju lutemi, rifreskoni listen dhe konvertojeni perseri ose rihapeni!", ids = ids, colKonfig = new colKonfigurimAmbjenti() };
                idNivelKonvert = colShitjet[0].IdNivel;
                idkonfigKonvert = colShitjet[0].IdKonfigAmbjente;
                niveliaktual.mbushNivelRegjistrimiSipasID(idNivelKonvert, new clsDatabaseRegjistrim()); //mbushen nivelet regj tek te cilat mund te konvertohet niveli
            }
            else
            {
                colMagazinat = new colKokaMagazina(String.Join(",", ids));
                if (colMagazinat.Count == 0)
                    return new ListeDokKontrolloKonvertuar { nivelet = new colNivelRegjistrimi(), mesazh = "Dokumenti qe doni te konvertoni mund te jete modifikuar nga nje perdorues tjeter! Ju lutemi, rifreskoni listen dhe konvertojeni perseri ose rihapeni!", ids = ids, colKonfig = new colKonfigurimAmbjenti() };
                idNivelKonvert = colMagazinat[0].IdNivel;
            }
            clsKusht kushtKonvertim = new clsKusht(idkonfigKonvert, "KVDN");
            bool kamagazine = false;
            bool kablerje = false;

            colNivelRegjistrimi niv = new colNivelRegjistrimi();
            colKonfigurimAmbjenti colKonfig = new colKonfigurimAmbjenti();
            if (kushtKonvertim.Vlera != 0)
            {
                clsKonfigurimAmbjenti konvertovetemne = new clsKonfigurimAmbjenti(kushtKonvertim.Vlera);
                clsNivelRegjistrimi nivelkonv = new clsNivelRegjistrimi();
                nivelkonv.mbushNivelRegjistrimiSipasIdPaKonvertime(konvertovetemne.IdNivel);
                niv.Add(nivelkonv);
                colKonfig.Add(konvertovetemne);
            }
            else
            {
                niv.mbushKonvertimeNiveli(idNivelKonvert, idPerdoruesi);

                if (niv.Count == 0)
                    return new ListeDokKontrolloKonvertuar { nivelet = new colNivelRegjistrimi(), mesazh = "Dokumentet nuk mund te konvertohen!", ids = ids, colKonfig = colKonfig };

                foreach (clsNivelRegjistrimi regj in niv)
                {
                    clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
                    colKonfig.mbushKonfigAmbjSipasIdKategoriIdNivel(regj.IdKategori, regj.IdNivel, idPerdoruesi, idGjuha, false);
                    if (regj.IdKategori == 6)
                        kamagazine = true;
                    if (regj.IdKategori == 2 && niveliaktual.IdKategori == 1)
                        kablerje = true;
                }
            }
            string KDPe = clsAlternativaKushti.getAlternativa(konfigurim.IdKonfigAmbjente, "KDPe");
            string KDPr = clsAlternativaKushti.getAlternativa(konfigurim.IdKonfigAmbjente, "KDPr");
            string ngjyra = String.Empty, ngjyra2 = String.Empty, ngjyra3 = String.Empty;
            bool merrNgjyra1 = true, merrNgjyra2 = true, merrNgjyra3 = true;
            bool kaVetemArtSherbimQeNukPerfshihenNeKonvert = true;

            if (konfigurim.IdKategori != 51 && konfigurim.IdKategori != 207)
            {
                for (var i = 0; i < colShitjet.Count; i++)
                {
                    clsKokaShitje koka = colShitjet[i];
                    string status = clsKokaShitje.ktheStatusPorosie(koka.IdShitjeKoka);
                    if (status == "Pezull" && KDPe == "Jo")
                    {
                        mesazh += "Ju nuk mund te konvertoni dokumenta me kete status Porosie";
                        continue;
                    }
                    if (status == "Ne Proces" && KDPr == "Jo")
                    {
                        mesazh += "Ju nuk mund te konvertoni dokumenta me kete status Porosie";
                        continue;
                    }
                    if (koka.IdStatusDok == 4)
                    {
                        mesazh += "Ju nuk mund te konvertoni dokumenta te refuzuar";
                        continue;
                    }

                    if (koka.IdStatusDok == 0)
                    {
                        mesazh += "Ju nuk mund te konvertoni dokumenta draft";
                        continue;
                    }
                    if (koka.DtDok > datamax || (koka.DtDok == datamax && koka.IdShitjeKoka > idmax))
                    {
                        datamax = koka.DtDok;
                        idmax = koka.IdShitjeKoka;
                        index = i;
                    }
                    if (merrNgjyra1)
                        ngjyra = clsKokaShitje.merrNgjyreKonvertime(false, koka.IdNdermarrje, koka.IdShitjeKoka);

                    if (merrNgjyra2)
                        ngjyra2 = clsKokaShitje.merrNgjyreKonvertimeMag(koka.IdNdermarrje, koka.IdShitjeKoka);

                    if (merrNgjyra3)
                        ngjyra3 = clsKokaShitje.merrNgjyreKonvertime(true, koka.IdNdermarrje, koka.IdShitjeKoka);

                    if (ngjyra == "kuqe" || ngjyra == "gjelber")
                        if (ngjyra2 == "kuqe" || ngjyra2 == "gjelber" || !kamagazine)
                            if (ngjyra3 == "kuqe" || ngjyra3 == "gjelber" || !kablerje)
                                mesazh += "Dokumenti Nr." + koka.NrDok + " Dt." + koka.DtDok.ToShortDateString() + " eshte konvertuar plotesisht dhe nuk mund te konvertohet perseri!";
                            else
                                merrkonvert3 = true;
                        else
                            merrkonvert2 = true;
                    else
                        merrKonvert1 = true;
                    if (ngjyra2 != "kuqe" && ngjyra2 != "gjelber")
                        merrkonvert2 = true;
                    if (ngjyra3 != "kuqe" && ngjyra3 != "gjelber")
                        merrkonvert3 = true;

                    if (merrKonvert1)
                        merrNgjyra1 = false;
                    if (merrkonvert2)
                        merrNgjyra2 = false;
                    if (merrkonvert3)
                        merrNgjyra3 = false;
                }

                for (var i = 0; i < colShitjet.Count; i++)
                {
                    clsKokaShitje dok = colShitjet[i];
                    if (clsAlternativaKushti.getAlternativa(dok.IdKonfigAmbjente, "MPASHGJK") == "Po")
                    {
                        kaVetemArtSherbimQeNukPerfshihenNeKonvert = false;
                        break;
                    }
                    dok.mbushTrupShitje();
                    foreach (var tr in dok.OColTrupiShitje)
                    {
                        if (tr.IdLlojVeprimi != 1)
                        {
                            kaVetemArtSherbimQeNukPerfshihenNeKonvert = false;
                            break;
                        }
                        clsArtikulli art = new clsArtikulli(tr.IdKodi);
                        if (art.Klasa != 3)
                        {
                            kaVetemArtSherbimQeNukPerfshihenNeKonvert = false;
                            break;
                        }
                    }
                    if (!kaVetemArtSherbimQeNukPerfshihenNeKonvert)
                        break;
                }

                if (kaVetemArtSherbimQeNukPerfshihenNeKonvert)
                    return new ListeDokKontrolloKonvertuar { nivelet = new colNivelRegjistrimi(), mesazh = "Dokumenti qe po tentoni te konvertoni ka vetem artikuj sherbim, prandaj nuk mund te konvertohet!", ids = ids, colKonfig = colKonfig };

            }
            else
            {
                for (var i = 0; i < colMagazinat.Count; i++)
                {
                    clsKokaMagazina kokamag = colMagazinat[i];
                    if (kokamag.DtDok > datamax || (kokamag.DtDok == datamax && kokamag.IdKokaMagazina > idmax))
                    {
                        datamax = kokamag.DtDok;
                        idmax = kokamag.IdKokaMagazina;
                        index = i;
                    }
                    if (kokamag.IdStatusDok == 0)
                    {
                        mesazh += "Ju nuk mund te konvertoni dokumenta draft";
                        continue;
                    }
                    if (merrNgjyra2)
                        ngjyra = clsKokaMagazina.merrNgjyreKonvertime(kokamag.IdNdermarrje, kokamag.IdKokaMagazina);

                    if (ngjyra == "kuqe" || ngjyra == "gjelber")
                        mesazh += "Dokumenti Nr." + kokamag.NrDok + " Dt." + kokamag.DtDok.ToShortDateString() + " eshte konvertuar plotesisht dhe nuk mund te konvertohet perseri!";
                    else
                        merrkonvert2 = true;

                    if (merrkonvert2)
                        merrNgjyra2 = false;
                }
            }

            int idtemp = ids[0];//vendosim dokumentin me te fundit ne fillim per te mare te dhenat prej tij
            ids[0] = idmax;
            ids[index] = idtemp;
            colKonfigurimAmbjenti konfVarura = new colKonfigurimAmbjenti();
            foreach (clsKonfigurimAmbjenti konfi in colKonfig)
            {
                if (konfi.IdKategori == 6 && !merrkonvert2)
                {
                    konfVarura.Add(konfi);

                    continue;
                }
                else if (konfi.IdKategori == 2 && !merrkonvert3 && niveliaktual.IdKategori == 1)
                {
                    konfVarura.Add(konfi);

                    continue;
                }
                if (((konfi.IdKategori == 1 && niveliaktual.IdKategori == 1) || (konfi.IdKategori == 2 && niveliaktual.IdKategori == 2)) && !merrKonvert1)
                {
                    konfVarura.Add(konfi);
                    continue;
                }
            }
            colNivelRegjistrimi nivelet = new colNivelRegjistrimi();
            foreach (clsNivelRegjistrimi nivel in niv)
            {
                if (nivel.IdKategori == 6 && merrkonvert2)
                {
                    nivelet.Add(nivel);

                    continue;
                }
                else if (nivel.IdKategori == 2 && merrkonvert3 && niveliaktual.IdKategori == 1)
                {
                    nivelet.Add(nivel);

                    continue;
                }
                if (((nivel.IdKategori == 1 && niveliaktual.IdKategori == 1) || (nivel.IdKategori == 2 && niveliaktual.IdKategori == 2)) && merrKonvert1)
                {
                    nivelet.Add(nivel);
                    continue;
                }
            }
            foreach (clsKonfigurimAmbjenti konfi in konfVarura)
            {
                colKonfig.Remove(konfi);
            }
            if (mesazh == "")
                mesazh = "Nuk jane konvertuar";

            var myPageCache = GlobalCacheManager.GetPageCacheByPageID(pageId);
            myPageCache["idkonvertimi"] = ids;

            return new ListeDokKontrolloKonvertuar { nivelet = nivelet, mesazh = mesazh, ids = ids, colKonfig = colKonfig };
        }

        public static string LidhArketime(int[] ids, HttpSessionState Session, int idNdermarrje, int idPerdoruesi, int idGjuha)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = MessagesResource.KtheCultureInfo(idGjuha);
            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            serializusi.MaxJsonLength = 50000000;
            DbCore.clsMesazh mesazh = new DbCore.clsMesazh();
            DataTable err = new DataTable();
            err.Columns.Add("Kodi");
            err.Columns.Add("Gabimi");
            err.Columns.Add("Rreshti");
            clsKonfigurimAmbjenti konfdl = new clsKonfigurimAmbjenti();
            konfdl.mbushKonfigAmbjSipasKod("DL", idNdermarrje);
            bool meKontabilizim = true;
            if (clsAlternativaKushti.getAlternativa(konfdl.IdKonfigAmbjente, "GJK") == "Jo")
                meKontabilizim = false;
            int idnrautonrdok = clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(konfdl.IdKonfigAmbjente, "nrLidhje_TextBox", 217);
            string shfaqmesazhapolupe = "";
            for (int i = 0; i < ids.Length; i++)
            {
                clsKokaShitje koka = new clsKokaShitje(ids[i]);
                clsNivelRegjistrimi nivel = new clsNivelRegjistrimi();
                nivel.mbushNivelRegjistrimiSipasIdPaKonvertime(koka.IdNivel);
                if (nivel.Kodi != "FSH")
                {
                    object[] arr = { koka.NrDok + " " + koka.DtDok.ToShortDateString(), "Ky dokument nuk eshte kategori shitje dhe nuk mund te lidhet me pagese!", i + 1 };
                    err.Rows.Add(arr);
                    continue;


                }
                mesazh = clsDokumentLidhesKoka.LidhArketimeMeFatura(koka, serializusi, rm, ci, idPerdoruesi, DbCore.mySessionObjects.merrPeriudheKontabel(Session).IdPeriudha, idGjuha, out shfaqmesazhapolupe, konfdl, meKontabilizim, idnrautonrdok);

                if (!mesazh.Status)
                {
                    object[] arr = { koka.NrDok + " " + koka.DtDok.ToShortDateString(), mesazh.PershkrimMesazhi, i + 1 };
                    err.Rows.Add(arr);
                    continue;
                }

            }
            DbCore.mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
            if (err.Rows.Count > 0)
            {
                clsKokaErrorImporti kokaerr = new clsKokaErrorImporti(0, "Nga riruatja e shitjeve ", 1, idNdermarrje, idPerdoruesi);
                kokaerr.ColTrupi.mbushErrorImportiNgaProgrami(err);
                mesazh = kokaerr.ruajErrorImporti();
                return "U riruajten " + (ids.Length - err.Rows.Count) + " rreshta dhe deshtuan " + err.Rows.Count + " rreshta! ";

            }
            else
            {
                return "U riruajten te gjitha rreshtat!";
            }

        }
        /// <summary>
        /// gjejme konfigurimin e dokumentit qe duhet te krijojme dhe ruajme id ne session 
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static int RuajIdInventarizimi(int[] ids, string pageId)
        {
            var myPageCache = GlobalCacheManager.GetPageCacheByPageID(pageId);
            myPageCache["idinventarizimi"] = ids;
            return ids[0];
        }

        public static AutoCompleteItem[] ktheACListeArtikujshKodPershkKodbarEShpejt(string infixText, int pershk, string grup, int idNdermarrje, int idPerdoruesi, bool artikujTeShitshem, bool merrVetemAfatgjate, bool merrSipasDetajimit, string klasa)
        {
            if (grup == "Dhurate")
                grup = "";

            DataTable tmpTable = colArtikujt.merrArtikujLikeKodPershkKodbarDT(idNdermarrje, idPerdoruesi, infixText, pershk, grup, artikujTeShitshem, merrVetemAfatgjate, merrSipasDetajimit, klasa);
            AutoCompleteItem[] autoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                autoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                autoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                autoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
            }
            return autoCompleteItem;
        }

        /// <summary>
        /// merr listen e artikujve per autocomplete + objektin e artikujve
        /// </summary>
        /// <param name="infixText">kodi/pershkrimi/kodbari</param>
        /// <param name="pershk">pershrkimi shqip = 1, anglisht = 2</param>
        /// <param name="grup">grupi i artikujve</param>
        /// <param name="idNdermarrje">id e ndermarrjes ku jane artikujt</param>
        /// <param name="idPerdoruesi">id e perdoruesit qe po kryen veprimin</param>
        /// <param name="artikujTeShitshem"></param>
        /// <param name="merrVetemAfatgjate">vetem afatgjate = true, te gjitha = false</param>
        /// <param name="merrSipasDetajimit"></param>
        /// <param name="klasa">klasa e artikullit</param>
        /// <returns>listen me autocomplete + objektin</returns>
        public static AutoCompleteItem[] ktheACListeArtikujshKodPershkKodbarFull(string infixText, int pershk, string grup, int idNdermarrje, int idPerdoruesi, bool artikujTeShitshem, bool merrVetemAfatgjate, bool merrSipasDetajimit, string klasa)
        {
            colArtikujt artikujt = colArtikujt.merrArtikujLikeKodPershkKodbarDTFull(idNdermarrje, idPerdoruesi, infixText, pershk, grup, artikujTeShitshem, merrVetemAfatgjate, merrSipasDetajimit, klasa);
            AutoCompleteItem[] autoCompleteItem = new AutoCompleteItem[artikujt.Count];
            for (int i = 0; i < artikujt.Count; i++)
            {
                autoCompleteItem[i].label = artikujt[i].KodArtikulli;
                autoCompleteItem[i].value = artikujt[i].IdArtikulli.ToString();
                autoCompleteItem[i].desc = pershk == 1 ? artikujt[i].PershkrimArtikulli.ToString() : artikujt[i].PershkrimiAngArtikulli;
                autoCompleteItem[i].objekti = artikujt[i];
            }
            return autoCompleteItem;
        }

        public static AutoCompleteItem[] ktheACListeArtikujshKodPershkKodbarEShpejtSet(string infixText, int pershk, string grup, int idNdermarrje, int idPerdoruesi, bool artikujTeShitshem)
        {
            if (grup == "Dhurate")
                grup = "";

            DataTable tmpTable = colArtikujt.merrArtikujLikeKodPershkKodbarDTPerbere(idNdermarrje, idPerdoruesi, infixText, pershk);
            AutoCompleteItem[] autoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                autoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                autoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                autoCompleteItem[i].desc = tmpTable.Rows[i]["des"].ToString();
            }
            return autoCompleteItem;
        }

        public static AutoCompleteItem[] ktheACListeLlogarish(string infixText, int pershk, int idNdermarrje, int idPerdoruesi)
        {
            DataTable tmpTable = colLlogarite.merrLLogariteLikeKodOsePershkDT(idNdermarrje, idPerdoruesi, infixText, pershk);
            tmpTable.Columns["des"].ColumnName = "desc";
            AutoCompleteItem[] autoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                autoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                autoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                autoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
            }
            return autoCompleteItem;
        }

        public static object ktheAdresatKlientFurnitor(string prefixText, int idNdermarrje)
        {
            if (prefixText == null)
                prefixText = "";
            int idKlientFurnitor = clsKlientFurnitor.MerrIdKlientFurnitor(prefixText, idNdermarrje);
            if (idKlientFurnitor <= 0)
                return new colAdresatKlientFurnitor();
            else
                return new colAdresatKlientFurnitor(idKlientFurnitor);
            
        }

        public static string ktheArtikullPerberes(int id)
        {
            colArtikulliPerberes colArtikujt = new colArtikulliPerberes();
            return colArtikujt.merrKoeficentArtikullPerberes(id);
        }

        public static Object ktheArtikullSipasKodit(string kodi, bool merrfurnitor, bool merrDetajime, int idNdermarrje, HttpSessionState Session)
        {
            clsArtikulli artikulli = new clsArtikulli();
            artikulli.mbushArtikull(kodi, idNdermarrje);
            object detajime = null;
            string katDet1 = string.Empty;
            string katDet2 = string.Empty;
            if (artikulli.IdArtikulli > 0)
            {
                artikulli.mbushKodBare();
                if (merrDetajime)
                {
                    katDet1 = ((KategoriDetajimi)artikulli.IdKategoriDetajimi).ToString(); katDet1 = katDet1 == "0" ? "" : katDet1;
                    katDet2 = ((KategoriDetajimi)artikulli.IdKategoriDetajimi2).ToString(); katDet2 = katDet2 == "0" ? "" : katDet2;
                    detajime = clsArtikulli.MerrDetajimeArtikulli(kodi, idNdermarrje, mySessionObjects.ktheIdPerdoruesi(Session));
                }

                if (merrfurnitor)
                {
                    clsKlientFurnitor furnitor = new clsKlientFurnitor();
                    furnitor.MbushKlientFurnitorSipasId(artikulli.IdFurnitoriKryesor);
                    return new { artikulli = artikulli, furnitori = furnitor, detajime = detajime, katDet1 = katDet1, katDet2 = katDet2 };
                }
                else
                    return new { artikulli = artikulli, detajime = detajime, katDet1 = katDet1, katDet2 = katDet2 };
            }
            else return null;
        }

        public static object[] ktheCmimArtikulli(int idNdermarrje, int idPerdoruesi, string kodArtikulli, int nivelCmimi, string date, string monedha, decimal kursi, int idRreshti, decimal sasi, int llojNiveli, string detajim, int njesia)
        {
            clsArtikulli artikulli = new clsArtikulli(kodArtikulli, idNdermarrje);
            string kodNjesia = String.Empty;
            kodNjesia = njesia == 1 ? artikulli.KodNjesia1 : artikulli.KodNjesia2;
            return ktheCmimArtikulliRow(idNdermarrje, idPerdoruesi, kodArtikulli, nivelCmimi, date, monedha, kodNjesia, kursi, idRreshti, sasi, llojNiveli, detajim);
        }

        public static object[] ktheCmimArtikulliRowNivelBaze(int idNdermarrje, int idPerdoruesi, string kodArtikulli, string date, string njesia,  int idRreshti, decimal sasi, int llojNiveli)
        {
            clsNivelCmimi nivelBaze = new clsNivelCmimi();
            nivelBaze.mbushNivelCmimiBaze(idNdermarrje, 1);
            clsMonedha monNderm = new clsMonedha();
            monNderm.mbushMonedhenENdermarrjes(idNdermarrje);
            return ktheCmimArtikulliRow(idNdermarrje, idPerdoruesi, kodArtikulli, nivelBaze.IdNivelCmimi, date, monNderm.KodiMonedha, njesia, 1, idRreshti, sasi, llojNiveli, 0);
        }

        public static object[] ktheCmimArtikulliRow(int idNdermarrje, int idPerdoruesi, string kodArtikulli, int nivelCmimi, string date, string monedha, string njesia, decimal kursi, int idRreshti, decimal sasi, int llojNiveli, string detajim)
        {
            int iddetajim = 0;
            if (!String.IsNullOrEmpty(detajim) && detajim != "False" && detajim != "false")
            {
                iddetajim = clsDetajimArtikulli.ktheIdDetajimi(detajim, idNdermarrje);
            }
            return ktheCmimArtikulliRow(idNdermarrje, idPerdoruesi, kodArtikulli, nivelCmimi, date, monedha, njesia, kursi, idRreshti, sasi, llojNiveli, iddetajim);
        }

        public static object[] ktheCmimArtikulliRow(int idNdermarrje, int idPerdoruesi, string kodArtikulli, int nivelCmimi, string date, string monedha, string njesia, decimal kursi, int idRreshti, decimal sasi, int llojNiveli, int iddetajim)
        {
            //  System.Threading.Thread.Sleep(5000);
            object[] result = new object[3];
            result[0] = idRreshti;
            object[] resultcmimi = new object[2];
            resultcmimi = clsFunksione.merrCmimSipasNivelit(nivelCmimi, kodArtikulli, idPerdoruesi, njesia, monedha, date, kursi, sasi, idNdermarrje, llojNiveli, iddetajim);
            result[1] = resultcmimi[0];
            result[2] = resultcmimi[1];
            return result;
        }

        public static object ktheCmimetSipasNiveleveArtComboMeKodRow(string kodArtikulli, int idNdermarrje, int idPerdorues, string date, string monedha, string njesia, string kursi, int idRreshti, string sasi, int nivelCmimi, int llojNiveli, string koddetajim) //pse duhet niveli ketu???
        {
            int iddetajim = clsDetajimArtikulli.ktheIdDetajimi(koddetajim, idNdermarrje);
            if (kodArtikulli == "")
                return new { idRreshti = idRreshti, idartikulli = -1, cmimet = new object[0] };
            clsArtikulli artikulli = new clsArtikulli();
            artikulli.mbushArtikull(kodArtikulli, idNdermarrje);
            int idartikulli = artikulli.IdArtikulli;
            if (idartikulli <= 0)
                return new { idRreshti = idRreshti, idartikulli = idartikulli, cmimet = new object[0] };
            DataTable dt = colNiveleCmimesh.merrNiveleNdermarjeDTBlerjeShitjeMeAutorizimePerAutocompleteCmimesh(idNdermarrje, llojNiveli, idPerdorues);
            //object[] cmimet = new object[dt.Rows.Count];
            int i = 0;
            clsMonedha mon = new clsMonedha();
            mon.mbushMonedhen(monedha, idNdermarrje);
            clsNjesiArtikulli njesi = new clsNjesiArtikulli(njesia, idNdermarrje);
            clsDatabaseInventari dbInv = new clsDatabaseInventari();
            List<ListeVleraCmimi> cmimet = new List<ListeVleraCmimi>();
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ListeVleraCmimi vleraCmimi = new ListeVleraCmimi();
                    int idNivel = int.Parse(dr["IdNivelCmimi"].ToString());
                    int detajim = int.Parse(dr["Detajim"].ToString());
                    //if (iddetajim > 0 && detajim == 0)
                    //    continue;
                    //if (iddetajim == 0 && detajim > 0)
                    //    continue;
                    decimal cmimi = Convert.ToDecimal(clsFunksione.merrCmimSipasNivelitMeDetajim(idNivel, artikulli.KodArtikulli, idPerdorues, njesi, mon, date, decimal.Parse(kursi), decimal.Parse(sasi), idNdermarrje, llojNiveli, db, iddetajim, false)[0]);
                    if (cmimi == 0)
                        continue;
                    vleraCmimi.cmimi = Math.Round(cmimi, 7);
                    vleraCmimi.brutoNetoNivelCmimi = int.Parse(dr["BrutoNetoNivelCmimi"].ToString());
                    vleraCmimi.idNivelCmimi = idNivel;
                    vleraCmimi.pershkrimiNivelCmimi = dr["PershkrimNivelCmimi"].ToString();
                    vleraCmimi.Detajim = int.Parse(dr["Detajim"].ToString());
                    vleraCmimi.IdDetajim = vleraCmimi.Detajim > 0 ? iddetajim : 0; //gabim. Nuk e di nqs ky cmimi eshte me detajim apo jo
                    cmimet.Add(vleraCmimi);
                    //cmimet[i] = vleraCmimi;
                    i++;
                }
            }
            dbInv.Dispose();
            return new { idRreshti = idRreshti, idartikulli = idartikulli, cmimet = cmimet };
        }

        public static IEnumerable<AutoCompleteItem> KtheListKategoriShpenzimesh(string infix, int idNdermarrje, int idPerdoruesi)
        {
            return colKategoriShpenzimi.MerrKategoriShpenzimiPerAutoComplete(infix, idNdermarrje, idPerdoruesi);
        }

        public static IEnumerable<AutoCompleteItem> KtheListMagazinat(string infix, int idNdermarrje, int idPerdoruesi, bool meAutorizim, int llojArt)
        {
            return colNjesiAdministrative.merrMagazinatPerAutoComplete(infix, idNdermarrje, idPerdoruesi, meAutorizim, llojArt);
        }



        public static int ktheDegeMagazineSipasKodit(string kodi, int idNdermarrje, int idPerdoruesi)
        {
            clsNjesiAdministrative njesia =
                new clsNjesiAdministrative(kodi, idNdermarrje, idPerdoruesi);
            if (njesia.IdNjesiAdministrative > 0)
            {
                return njesia.IdDegeAdministrative;
            }
            else
                return 0;
        }

        public static object ktheDetyrimi(int idKf, int idKokaShitje, DateTime date)
        {
            if (idKf <= 0) return 0;
            date = date.ToLocalTime();
            DataRow drDetyrimet = clsKlientFurnitor.MerrDetyrimiKfMeparshem(idKf, idKokaShitje, date);

            return new
            {
                detyrimi = drDetyrimet != null ? (double)drDetyrimet["DETYRIMI"] : 0,
                detyrimiMeparshem = drDetyrimet != null ? (double)drDetyrimet["DETYRIMIMEPARSHEM"] : 0
            };
        }
        public static clsMesazh ktheGjendjeArtikulli(int idja, string mag, DateTime data, string koddetajim, string koddetajim2, int iddok, int idndermarje, int idKonfigAmbjente, int idreshti, double totalartikulli, double totaldetajim1, double totaldetajim2, bool shitje_blerje, bool ekzekutimProdhim)
        {
            clsArtikulli artikulli = new clsArtikulli(idja);
           if (artikulli.IdArtikulli == 0 )
                return new clsMesazh(false, String.Format("Artikulli me id {0} nuk ekziston!", idja.ToString()));
            if (!artikulli.KontrollGjendjeArtikulli)
                return new clsMesazh(true);

            if (artikulli.Klasa == 4)
            {
                colArtikulliPerberes col = new colArtikulliPerberes();
                col.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(artikulli.IdArtikulli, data);
                foreach (clsArtikulliPerberes artper in col)
                {
                    clsMesazh mes = ktheGjendjeArtikulliPerArtikull(artper.IdLidheseArt, mag, data, koddetajim, koddetajim2, iddok, idndermarje, idKonfigAmbjente, idreshti, totalartikulli * double.Parse(artper.Koeficienti.ToString()), totaldetajim1 * double.Parse(artper.Koeficienti.ToString()), totaldetajim2 * double.Parse(artper.Koeficienti.ToString()), shitje_blerje, artikulli, false);
                    if (!mes.Status)
                        return mes;
                }
                return new clsMesazh(true);
            }
            else
                return ktheGjendjeArtikulliPerArtikull(idja, mag, data, koddetajim, koddetajim2, iddok, idndermarje, idKonfigAmbjente, idreshti, totalartikulli, totaldetajim1, totaldetajim2, shitje_blerje, artikulli, ekzekutimProdhim);
        }

        public static clsMesazh ktheGjendjeArtikulliPerArtikull(int idja, string mag, DateTime data, string koddetajim, string koddetajim2, int iddok, int idndermarje, int idKonfigAmbjente, int idreshti, double totalartikulli, double totaldetajim1, double totaldetajim2, bool shitje_blerje, clsArtikulli artikulli,  bool ekzekutimProdhim)
        {
            data = data.ToLocalTime();
            //nqs kemi shitje negative ose blerje pozitive dalim sepse keto e shtojne gjendjen
            if ((totalartikulli < 0 && shitje_blerje) || (totalartikulli > 0 && !shitje_blerje && !ekzekutimProdhim))
                return new clsMesazh(true);
            // marrim te dhenat per artikullin, detajimet , magazinen
            //clsArtikulli artikulli = new clsArtikulli(idja);
            clsDetajimArtikulli detajim = new clsDetajimArtikulli();
            detajim.mbushDetajimArtikulli(koddetajim, idndermarje);
            clsDetajimArtikulli detajim2 = new clsDetajimArtikulli();
            detajim2.mbushDetajimArtikulli(koddetajim2, idndermarje);
            clsNjesiAdministrative magazi = new clsNjesiAdministrative(mag, idndermarje);
            //marrim gjithe datat ne te cilat ka veprime ky artikull pas dates qe i vjen si parameter. keto data perdoren per te gjetur gjendjen negative ne ndonje date te mevonshme

            clsKokaMagazina kokamag = new clsKokaMagazina();
            double gjendje = double.MaxValue;
            //kontrollojme nese artikulli lejon gjendje negative dhe nqs jo marrim gjendjen e tij
            if (!clsKokaMagazina.lejonGjendjeNegative(idKonfigAmbjente, magazi.IdNjesiAdministrative, artikulli.IdArtikulli))
                gjendje = clsTrupiMagazina.merrSasi(artikulli, magazi.IdNjesiAdministrative, data, -1);
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti(idKonfigAmbjente);
            colTrupiMagazina trupi = new colTrupiMagazina();
            if (iddok != 0)//marrim te dhenat e dokumentit ekzistues nqs jemi ne modifikim
            {
                if (konf.IdKategori == 1 || konf.IdKategori == 2)
                {
                    kokamag.mbushKokaMagazinaSipasIDGjenerues(iddok, konf.IdKategori == 1 ? 2 : 1, idKonfigAmbjente);
                    iddok = kokamag.IdKokaMagazina;
                }
                trupi.mbushGjitheTrupiMagazinaNgaKoka(iddok);
            }
            double shtimgjendjemod = 0;
            double shtimgjendjedetmod = 0;

            DataTable datapas = colTrupiMagazina.merrDataVeprimiPas(data, idndermarje, magazi.IdNjesiAdministrative, artikulli.IdArtikulli, null);
            ListeVleraTePlotaArtikulli listArt = new ListeVleraTePlotaArtikulli();
            listArt.colGjendjeArtikulli = merrGjendjeArtikulliMag(artikulli.IdArtikulli, !String.IsNullOrEmpty(artikulli.Magazina) ? artikulli.Magazina : mag, idndermarje);
          
            if (ekzekutimProdhim)
            {
                double sasiaProd = totalartikulli + gjendje;
                double sasiaRec =  gjendje - totalartikulli;
                double minArtikull = Convert.ToDouble(artikulli.MinimumArtikulli);
                double maxArtikull = Convert.ToDouble(artikulli.MaximumArtikulli);
                if (artikulli.Klasa == 5 || artikulli.Klasa == 6)
                {
                    if (listArt.colGjendjeArtikulli.Count > 0 && sasiaProd > listArt.colGjendjeArtikulli[0].GjendjaMax && listArt.colGjendjeArtikulli[0].GjendjaMax != 0)
                        return new clsMesazh(true, String.Format(MessagesResource.Messages["MsgSasiaMaxMagArtikulli"], artikulli.KodArtikulli, mag, listArt.colGjendjeArtikulli[0].GjendjaMax));
                    else

                    if (sasiaProd > maxArtikull && maxArtikull != 0)
                        return new clsMesazh(true, String.Format(MessagesResource.Messages["MsgSasiaMaxArtikulli"], artikulli.KodArtikulli, maxArtikull));
                    else

                    if (listArt.colGjendjeArtikulli.Count > 0 && sasiaProd < listArt.colGjendjeArtikulli[0].GjendjaMin
                        && listArt.colGjendjeArtikulli[0].GjendjaMin != 0)//krahasojme gjendjet me max dhe min e magazines
                        return new clsMesazh(true, String.Format(MessagesResource.Messages["MsgSasiaMinMagArtikulli"], artikulli.KodArtikulli, mag, listArt.colGjendjeArtikulli[0].GjendjaMin));

                    else
                       if (sasiaProd < minArtikull && minArtikull != 0)//krahasojme gjendjet me min dhe max te artikullit
                        return new clsMesazh(true, String.Format(MessagesResource.Messages["MsgSasiaMinArtikulli"], artikulli.KodArtikulli, minArtikull));
                }
                else
                {
                    if (listArt.colGjendjeArtikulli.Count > 0 && sasiaRec < listArt.colGjendjeArtikulli[0].GjendjaMin && listArt.colGjendjeArtikulli[0].GjendjaMin != 0)//krahasojme gjendjet me max dhe min e magazines

                        return new clsMesazh(true, String.Format(MessagesResource.Messages["MsgSasiaMinMagArtikulli"], artikulli.KodArtikulli, mag, listArt.colGjendjeArtikulli[0].GjendjaMin));
                    else

                    if (sasiaRec < minArtikull && minArtikull != 0)//krahasojme gjendjet me min dhe max te artikullit
                        return new clsMesazh(true, String.Format(MessagesResource.Messages["MsgSasiaMinArtikulli"], artikulli.KodArtikulli, minArtikull));
                }
                
            }

            if (koddetajim == "" && !ekzekutimProdhim)//nqs nuk kemi detajim ne rresht
            {
                if (shitje_blerje)
                { // per shitje shtojme gjendjen e artikullit per dokumentin ne modifikim. nqs eshte shtim do vije 0
                    shtimgjendjemod += kokamag.ktheTotalinArtikullit(trupi, artikulli.IdArtikulli, magazi.IdNjesiAdministrative);
                    gjendje += shtimgjendjemod;
                    if (totalartikulli > gjendje)//krahasojme gjendjet dhe nqs nuk kemi gjendje shfaqim mesazhin
                        return new clsMesazh(false, "Sasia e daljes është më e madhe se gjendja " + gjendje + " e artikullit: " + artikulli.KodArtikulli + "!");
                }
                else
                {//per blerje zbresim gjendjen e artikullit per dokumentin ne modifikim. nqs eshte shtim do vije 0.
                    shtimgjendjemod -= kokamag.ktheTotalinArtikullit(trupi, artikulli.IdArtikulli, magazi.IdNjesiAdministrative);
                    gjendje += shtimgjendjemod;
                    if (Math.Abs(totalartikulli) > gjendje)//krahasojme gjendjet dhe nqs nuk kemi gjendje shfaqim mesazhin
                        return new clsMesazh(false, "Sasia e hyrjes së artikullit: " + artikulli.KodArtikulli + " çon në gjendje negative!");
                }

                //kontrollojme gjendjen per cdo date pas dates qe i kalohet si parameter ne te cilat kemi veprim me artikullin
                foreach (DataRow t in datapas.Rows)
                {// kontrolli para cdo veprimi
                    int idrenditje = int.MaxValue;// idrenditjes;
                    clsMesazh mesazh = kokamag.kontrollgjendje(artikulli, magazi.IdNjesiAdministrative, DateTime.Parse(t["data"].ToString()), idrenditje, detajim.IdDetajimArtikulli, totalartikulli, totaldetajim1, 1, shitje_blerje, shtimgjendjemod, shtimgjendjedetmod);
                    if (!mesazh.Status)
                        return mesazh;
                }
            }
            else
            {
                if (artikulli.KontrollGjendje && !clsKokaMagazina.lejonGjendjeNegativeDetPare(idKonfigAmbjente, magazi.IdNjesiAdministrative, artikulli.IdArtikulli))// ne rast se kemi detajim kontrollojme ne kemi kontroll gjendje per te
                {//marrim gjendjen per detajimin
                    gjendje = clsTrupiMagazina.merrSasi(artikulli, magazi.IdNjesiAdministrative, data, detajim.IdDetajimArtikulli);

                    if (shitje_blerje)
                    {// per shitje shtojme gjendjen e artikullit per dokumentin ne modifikim. nqs eshte shtim do vije 0
                        shtimgjendjedetmod += kokamag.ktheTotalinArtikullitDetajim(trupi, artikulli.IdArtikulli, detajim.IdDetajimArtikulli, magazi.IdNjesiAdministrative);
                        shtimgjendjemod += kokamag.ktheTotalinArtikullit(trupi, artikulli.IdArtikulli, magazi.IdNjesiAdministrative);
                        gjendje += shtimgjendjedetmod;
                        if (totaldetajim1 > gjendje)//krahasojme gjendjet dhe nqs nuk kemi gjendje shfaqim mesazhin
                        {
                            return new clsMesazh(false, "Sasia e daljes është më e madhe se gjendja " + gjendje + " e artikullit: " + artikulli.KodArtikulli + "me detajim: " + detajim.KodDetajimArtikulli);
                        }
                    }
                    else
                    {
                        //per blerje zbresim gjendjen e artikullit per dokumentin ne modifikim. nqs eshte shtim do vije 0.
                        shtimgjendjedetmod -= kokamag.ktheTotalinArtikullitDetajim(trupi, artikulli.IdArtikulli, detajim.IdDetajimArtikulli, magazi.IdNjesiAdministrative);
                        shtimgjendjemod -= kokamag.ktheTotalinArtikullit(trupi, artikulli.IdArtikulli, magazi.IdNjesiAdministrative);
                        gjendje += shtimgjendjedetmod;
                        if (Math.Abs(totaldetajim1) > gjendje)//krahasojme gjendjet dhe nqs nuk kemi gjendje shfaqim mesazhin
                        {
                            return new clsMesazh(false, "Sasia e hyrjes së artikullit: " + artikulli.KodArtikulli + "me detajim: " + detajim.KodDetajimArtikulli + " çon në gjendje negative!");
                        }
                    }
                    //kontrollojme gjendjen per cdo date pas dates qe i kalohet si parameter ne te cilat kemi veprim me artikullin
                    foreach (DataRow t in datapas.Rows)
                    {// kontrolli para cdo veprimi
                        int idrenditje = int.MaxValue;// idrenditjes;
                        clsMesazh mesazh = kokamag.kontrollgjendje(artikulli, magazi.IdNjesiAdministrative, DateTime.Parse(t["data"].ToString()), idrenditje, detajim.IdDetajimArtikulli, totalartikulli, totaldetajim1, 1, shitje_blerje, shtimgjendjemod, shtimgjendjedetmod);
                        if (!mesazh.Status)
                            return mesazh;
                    }
                }
            }


            double gjendjeDetDyte = double.MaxValue;
            if (koddetajim2 != "")//nqs kemi detajim te dyte bejme kontrollin per te
            {//kotntrollojme nqs lejon gjendje negative detajimi i dyte dhe marrim gjendjen per te
                if (artikulli.KontrollGjendjeDetajim2 && !clsKokaMagazina.lejonGjendjeNegativeDetDyte(idKonfigAmbjente, magazi.IdNjesiAdministrative, artikulli.IdArtikulli))
                {
                    gjendjeDetDyte = clsTrupiMagazina.merrSasiDetajimDyte(artikulli, magazi.IdNjesiAdministrative, data, detajim2.IdDetajimArtikulli);

                    if (shitje_blerje)
                    {// per shitje shtojme gjendjen e artikullit per dokumentin ne modifikim. nqs eshte shtim do vije 0
                        shtimgjendjedetmod += kokamag.ktheTotalinArtikullitDetajimDyte(trupi, artikulli.IdArtikulli, detajim2.IdDetajimArtikulli, magazi.IdNjesiAdministrative);
                        gjendjeDetDyte += shtimgjendjedetmod;
                        if (totaldetajim2 > gjendjeDetDyte)//krahasojme gjendjet dhe nqs nuk kemi gjendje shfaqim mesazhin
                        {
                            return new clsMesazh(false, "Sasia e daljes është më e madhe se gjendja " + gjendjeDetDyte + " e artikullit: " + artikulli.KodArtikulli + "me detajim: " + detajim2.KodDetajimArtikulli);
                        }
                    }
                    else
                    {//per blerje zbresim gjendjen e artikullit per dokumentin ne modifikim. nqs eshte shtim do vije 0.
                        shtimgjendjedetmod -= kokamag.ktheTotalinArtikullitDetajimDyte(trupi, artikulli.IdArtikulli, detajim2.IdDetajimArtikulli, magazi.IdNjesiAdministrative);
                        gjendjeDetDyte += shtimgjendjedetmod;
                        if (Math.Abs(totaldetajim2) > gjendjeDetDyte)//krahasojme gjendjet dhe nqs nuk kemi gjendje shfaqim mesazhin
                        {
                            return new clsMesazh(false, "Sasia e hyrjes së artikullit: " + artikulli.KodArtikulli + "me detajim: " + detajim2.KodDetajimArtikulli + " çon në gjendje negative!");
                        }
                    }
                    //kontrollojme gjendjen per cdo date pas dates qe i kalohet si parameter ne te cilat kemi veprim me artikullin me kete detajim
                    foreach (DataRow t in datapas.Rows)
                    {// kontrolli para cdo veprimi
                        int idrenditje = int.MaxValue;// idrenditjes;
                        clsMesazh mesazh = kokamag.kontrollgjendje(artikulli, magazi.IdNjesiAdministrative, DateTime.Parse(t["data"].ToString()), idrenditje, detajim2.IdDetajimArtikulli, totalartikulli, totaldetajim2, 2, shitje_blerje, shtimgjendjemod, shtimgjendjedetmod);
                        if (!mesazh.Status)
                            return mesazh;
                    }
                }
            }
            return new clsMesazh(true);
        }


        public static colGrupimDokumentiKoka[] ktheGrupimDokumentashNderm(string kodkonfig, int idNdermarrje, int idPerdoruesi)
        {
            return colGrupimDokumentiKoka.ktheGrupimDokumentashNderm(kodkonfig, idNdermarrje, idPerdoruesi);
        }

        public static int ktheIdMonedheNdermarrje(int idNdermarrje)
        {
            return clsMonedha.ktheIdMonedhenENdermarrjes(idNdermarrje);
        }

        public static ListeVleraInfo ktheInfoKFSipasKodit(string kodi, int idNdermarrje, DateTime data, int idInfo)
        {
            int idKf = clsKlientFurnitor.MerrIdKlientFurnitor(kodi, idNdermarrje);
            return ktheInfoKF(idKf, data, idInfo);
        }

        public static ListeVleraInfo ktheInfoKF(int idja, DateTime data, int idInfo)
        {
            clsKlientFurnitor kf = new clsKlientFurnitor(idja);
            ListeVleraInfo lista = kf.KtheInfoKf(data, idInfo);
            return lista;
        }

        public static object ktheKategoriZbritje(string kf, DateTime date, decimal vlefte, decimal kursi, int idmonedha,  int idNdermarrje)
        {
            date = date.ToLocalTime();
            if (kf == null || kf == "")
                return null;
            clsKlientFurnitor oClsKF = new clsKlientFurnitor();
            try
            {
                if (!oClsKF.mbushKlientFurnitorSipasKodit(kf, idNdermarrje).Status || oClsKF.IdKlientFurnitor == 0) return null;
            }
            catch (Exception)
            {
                return null;
            }
            if (oClsKF.ZbritjeTotal != 0)
            {
                return new { zbritje = oClsKF.ZbritjeTotal, eshtePerqindje = true };
            }
            clsKokaKategoriZbritje kokaKategoriZbritje = new clsKokaKategoriZbritje(oClsKF.IdKatZbritje);
            decimal kurskategorie = 0;
            if (kokaKategoriZbritje.IdMonedha != idmonedha)
            {
                clsKurset kurs = new clsKurset(kokaKategoriZbritje.IdMonedha, date);
                if (kurs.VleraKursi == 0)
                    kurs.VleraKursi = 1;
                kurskategorie = Convert.ToDecimal((double)kurs.VleraKursi);
            }
            else kurskategorie = kursi;
            colTrupatKategoriteZbritjes trupKategoriZbritje = new colTrupatKategoriteZbritjes();
            trupKategoriZbritje.mbushTrupatKategoriZbritjeSipasKokes(oClsKF.IdKatZbritje);
            for (int i = trupKategoriZbritje.Count - 1; i >= 0; i--)
            {
                if (trupKategoriZbritje[i].DateFillimi <= date && trupKategoriZbritje[i].DateMbarimi >= date)
                {
                    if (trupKategoriZbritje[i].VleraMax == 0 && trupKategoriZbritje[i].VleraMin == 0)
                        if (trupKategoriZbritje[i].Lloji == 1)
                            return new { zbritje = trupKategoriZbritje[i].Zbritja, eshtePerqindje = true };
                        else
                            return new { zbritje = trupKategoriZbritje[i].Zbritja * kurskategorie / kursi, eshtePerqindje = false };

                    if (trupKategoriZbritje[i].VleraMax != 0)
                        if (trupKategoriZbritje[i].VleraMax * kurskategorie / kursi >= vlefte && trupKategoriZbritje[i].VleraMin * kurskategorie / kursi <= vlefte)
                            if (trupKategoriZbritje[i].Lloji == 1)
                                return new { zbritje = trupKategoriZbritje[i].Zbritja, eshtePerqindje = true };
                            else
                                return new { zbritje = trupKategoriZbritje[i].Zbritja * kurskategorie / kursi, eshtePerqindje = false };
                        else
                            continue;
                    else
                    {
                        if (trupKategoriZbritje[i].VleraMin != 0)
                        {
                            if (trupKategoriZbritje[i].VleraMin * kurskategorie / kursi <= vlefte)
                                if (trupKategoriZbritje[i].Lloji == 1)
                                    return new { zbritje = trupKategoriZbritje[i].Zbritja, eshtePerqindje = true };
                                else
                                    return new { zbritje = trupKategoriZbritje[i].Zbritja * kurskategorie / kursi, eshtePerqindje = false };
                        }
                    }
                }
            }
            return null;
        }

        public static clsKlientFurnitor ktheKlientAutomjeti(int idAutomjeti)
        {
            clsAutomjete auto = new clsAutomjete();
            auto.mbushAutomjet(idAutomjeti);
            if (auto.IdAutomjeti <= 0)
                return null;
            else
            {
                clsKlientFurnitor klient = new clsKlientFurnitor(auto.IdKlienti);
                return klient;
            }
        }

     

        public static string ktheKursinFunditSipasLlojitDB(int idMonedha, int lloji, int idNdermarrje)
        {
            //idMonedha = 0;
            string kursi = "0";
            if (idMonedha == clsMonedha.ktheIdMonedhenENdermarrjes(idNdermarrje))
                return "1";
            colKurset kurset = new colKurset();
            if (lloji != 0)
                kurset.mbushKursetFunditMonedhesSipasLlojit(idMonedha, lloji);
            else
                kurset.mbushKursetFunditMonedhes(idMonedha);
            if (kurset.Count > 0)
                kursi = kurset[0].VleraKursi.ToString();
            return kursi;
        }

        public static Object ktheListeDetajimeshArtikulliNew(string infixText, string art, int lloji, int idNdermarrje, int idPerdoruesi, bool merrPerberesit, DateTime dt, bool sipasGjendjes, string detajimi1, string mag)
        {
            if (art == null)
                return new { autocomplete = new AutoCompleteItem[0], meDetajim = false, ekzistonDetajim = false, kategoriDet = 0, infixText = infixText, lloji = lloji };
            clsArtikulli artikulli = new clsArtikulli();
            artikulli.mbushArtikull(art, idNdermarrje);
            if(artikulli.IdArtikulli == 0)
                return new { autocomplete = new AutoCompleteItem[0], meDetajim = false, ekzistonDetajim = false, kategoriDet = 0, infixText = infixText, lloji = lloji };
            DataTable tmpTable = null;
            clsDatabaseInventari dbInv = new clsDatabaseInventari();
            if (merrPerberesit && artikulli.Klasa == 4)
            {
                colArtikulliPerberes artper = new colArtikulliPerberes(artikulli.IdArtikulli, dt, dbInv);
                colArtikujt artikujt = new colArtikujt(artper.Select(x => x.IdLidheseArt).ToList());
                tmpTable = colDetajimeArtikulli.mbushDetajimeSipasArtikujveAndNdermarrjesAndAutorizimeLike(idNdermarrje, idPerdoruesi, infixText, artikujt.Join(';', x => x.KodArtikulli), lloji, sipasGjendjes);
            }
            else
            {
                tmpTable = colDetajimeArtikulli.mbushDetajimeSipasArtikullitAndNdermarrjesAndAutorizimeLikeNewPati(idNdermarrje, idPerdoruesi, infixText, art, lloji, sipasGjendjes);
            }

            tmpTable.Columns["des"].ColumnName = "desc";
            if (sipasGjendjes)
            {
                DataTable tmpTableMeGjendje = tmpTable.Clone();
                int idDetajimi1 = (lloji == 2 && !String.IsNullOrEmpty(detajimi1)) ? clsDetajimArtikulli.ktheIdDetajimi(detajimi1, idNdermarrje) : 0;
                int idMag = !String.IsNullOrEmpty(mag) ? clsNjesiAdministrative.ktheIdMagazine(mag, idNdermarrje) : -1;
                int nr = 0;
                foreach (DataRow dr in tmpTable.Rows)
                {
                    double sasi = 0;
                    if (lloji == 2 && idDetajimi1 != 0)
                        sasi = clsTrupiMagazina.merrSasiDetajimitDyteDheDetajimPare(artikulli, idMag, dt, idDetajimi1, int.Parse(dr["ID"].ToString()));
                    else
                        sasi = clsTrupiMagazina.merrSasiSipasDetajimit(artikulli, idMag, dt, int.Parse(dr["ID"].ToString()), lloji);

                    if (sasi > 0)
                    {
                        tmpTableMeGjendje.Rows.Add(dr.ItemArray);
                        nr++;
                    }
                    if (nr == 10) //duhen vetem 10 te paret
                        break;
                }
                tmpTable = tmpTableMeGjendje;
            }
            AutoCompleteItem[] autoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                autoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                autoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                autoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
                autoCompleteItem[i].kategori = tmpTable.Rows[i]["kategoria"].ToString();
            }
            bool ekzistonDetajimi = false;
            if(artikulli.DetajimArtikulli)
                ekzistonDetajimi = dbInv.ekzistonDetajim(infixText, idNdermarrje);
            return new { autocomplete = autoCompleteItem, meDetajim = artikulli.DetajimArtikulli, ekzistonDetajim = ekzistonDetajimi, kategoriDet = lloji == 1 ? artikulli.IdKategoriDetajimi : artikulli.IdKategoriDetajimi2, infixText = infixText, lloji = lloji };
        }

        public static DateTime ktheMaturim(string idmat, DateTime dataFat)
        {
            dataFat = dataFat.ToLocalTime();
            int idmaturimi = 0;
            if (idmat != null && idmat != "" && idmat != "undefined")
                idmaturimi = int.Parse(idmat);
            DateTime dataFatures = DateTime.Parse(dataFat.ToString());
            DateTime dataMaturimit = DateTime.Today;
            if (idmaturimi != 0)
            {
                clsMaturimi oClsMaturimi = new clsMaturimi(idmaturimi);
                //DbCore.DbInventari.colMaturimet oColMaturimet = new DbCore.DbInventari.colMaturimet();
                //oColMaturimet = dbInv.ktheMaturim(idmaturimi);
                //foreach (DbCore.DbInventari.clsMaturimi o in oColMaturimet)
                //{
                if (oClsMaturimi.IdDateFillimi == DateFillimiMaturiteti.DateFature)//1=Date fature
                {
                    DateTime dt = dataFatures;
                    if (oClsMaturimi.IdPeriudha == PeriudheMaturiteti.Ditore)//Ditore
                        dt = dt.AddDays(oClsMaturimi.PercaktimMaturimi);
                    else if (oClsMaturimi.IdPeriudha == PeriudheMaturiteti.Javore)//Javore
                        dt = dt.AddDays(oClsMaturimi.PercaktimMaturimi * 7);
                    else if (oClsMaturimi.IdPeriudha == PeriudheMaturiteti.Mujore)//Mujore
                        dt = dt.AddMonths(oClsMaturimi.PercaktimMaturimi);
                    dataMaturimit = dt;
                }
                else if (oClsMaturimi.IdDateFillimi == DateFillimiMaturiteti.FillimMuaji)//2=Fillim muaji
                {
                    DateTime dt = new DateTime(dataFatures.Year, dataFatures.Month, 1);

                    if (oClsMaturimi.IdPeriudha == PeriudheMaturiteti.Ditore)//Ditore
                        dt = dt.AddDays(oClsMaturimi.PercaktimMaturimi);
                    else if (oClsMaturimi.IdPeriudha == PeriudheMaturiteti.Javore)//Javore
                        dt = dt.AddDays(oClsMaturimi.PercaktimMaturimi * 7);
                    else if (oClsMaturimi.IdPeriudha == PeriudheMaturiteti.Mujore)//Mujore
                        dt = dt.AddMonths(oClsMaturimi.PercaktimMaturimi);

                    dataMaturimit = dt;
                }
                else if (oClsMaturimi.IdDateFillimi == DateFillimiMaturiteti.FundMuaji)//3=Fund muaji
                {
                    DateTime dt = new DateTime(dataFatures.Year, dataFatures.Month, 1);
                    dt = dt.AddMonths(1).AddDays(-1);

                    if (oClsMaturimi.IdPeriudha == PeriudheMaturiteti.Ditore)//Ditore
                        dt = dt.AddDays(oClsMaturimi.PercaktimMaturimi);
                    else if (oClsMaturimi.IdPeriudha == PeriudheMaturiteti.Javore)//Javore
                        dt = dt.AddDays(oClsMaturimi.PercaktimMaturimi * 7);
                    else if (oClsMaturimi.IdPeriudha == PeriudheMaturiteti.Mujore)//Mujore
                        dt = dt.AddMonths(oClsMaturimi.PercaktimMaturimi);

                    dataMaturimit = dt;
                }
                //}
            }
            else dataMaturimit = DateTime.Today;
            return dataMaturimit;
        }

        public static object[] ktheNjesiArtikulliComboMeKodRow(string kodArtikulli, int idreshti, string textnjesizgjedhur, int idNdermarrje)
        {
            object[] result = new object[3];
            result[0] = idreshti;
            result[1] = textnjesizgjedhur;
            try
            {
                ComboList[] listeNjesish;
                clsArtikulli art = new clsArtikulli();
                art.merrSipasKodArtikullit(kodArtikulli, idNdermarrje);
                //DbCore.DbInventari.clsArtikulli art = db.merrArtikullSipasKodit(newart);
                clsNjesiArtikulli njesi1 = new clsNjesiArtikulli(art.Njesi1Artikulli);
                clsNjesiArtikulli njesi2 = new clsNjesiArtikulli(art.Njesi2Artikulli);
                //DbCore.DbInventari.colNjesiteArtikulli njesi1 = db.ktheNjesiAritkulli(art.Njesi1Artikulli);
                //DbCore.DbInventari.colNjesiteArtikulli njesi2 = db.ktheNjesiAritkulli(art.Njesi2Artikulli);
                ComboList njesia;
                if (njesi2.IdNjesia != njesi1.IdNjesia)
                    listeNjesish = new ComboList[2];
                else
                    listeNjesish = new ComboList[1];
                if (njesi1 != null)
                {
                    njesia = new ComboList();
                    njesia.value = njesi1.IdNjesia.ToString();
                    njesia.text = njesi1.KodNjesia;
                    listeNjesish[0] = njesia;
                }
                if (njesi2 != null && njesi2.IdNjesia != njesi1.IdNjesia)
                {
                    njesia = new ComboList();
                    njesia.value = njesi2.IdNjesia.ToString();
                    njesia.text = njesi2.KodNjesia;
                    listeNjesish[1] = njesia;
                }
                result[2] = listeNjesish;
                return result;
            }
            catch
            {
                result[2] = null;
                return result;
            }
        }

        public static object ktheOKlientFurnitor(string idKlientFurnitor, DateTime date, int idKonfigurimi, int idNdermarrje, int idPerdorues, int idKomponente, int idGjuha, string llojKursi)
        {
            return clsFunksione.ktheOKlientFurnitor(idKlientFurnitor, date, idKonfigurimi, idNdermarrje, idPerdorues, idKomponente, idGjuha, llojKursi);
        }
        public static object ktheKlientFurnitorSipasId(int idKlientFurnitor)
        {
            return new { klientFurnitor = new clsKlientFurnitor(idKlientFurnitor)};
        }

        public static string kthePershkrimDegeSipasID(int id)
        {
            return clsDegeAdministrative.mbushPershkrimDegeAdministrativeSipasiD(id);
        }

        public static string kthePershkrimMagSipasId(int id)
        {
            return clsNjesiAdministrative.kthePershkrimNjesiAdministrativeSipasiDPaAutorizime(id);
        }

        public static string kthePershkrimMagSipasKodit(string kodi, int idNdermarrje)
        {
            return clsNjesiAdministrative.kthePershkrimMagazineSipasKodit(kodi, idNdermarrje);
        }

        internal static object EkzistonFazaKontrates(int idFaza, int idKontrata, int idNdermarrje)
        {
            return clsFazaKontrate.EkzistonFaza(idFaza, idNdermarrje, idKontrata);
        }

        public static string kthePershkrimMakro(string prefixText, int idNderViti, int idNdermarrje, int idPerdoruesi)
        {
            
            colKokatMakro oCol = new colKokatMakro();
            oCol = new colKokatMakro(idNdermarrje, idPerdoruesi, prefixText);
            if (oCol.Count > 0)
            {
                return oCol[0].PershkrimiKokaMakro;
            }
            else
            {
                return "";
            }
        }
        internal static object ktheArtInfo(string kodbar, int idNdermarrje, int idInfoArt, string mag, bool eshteAfatShkurter, DateTime data, int idViti, int idPerdoruesi)
        {
            clsArtikulli artikulli = new clsArtikulli(kodbar, eshteAfatShkurter, idNdermarrje);
            if (artikulli.IdArtikulli < 1)
                return null;
            if (idInfoArt == 0)
                return new { artikulli = artikulli };
         
          //  int idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
            data = data.ToLocalTime();
            ListeVleraInfo lista = artikulli.merrInfoArtSipasIdKokaDheVisibleVlera(idPerdoruesi, data, "-1", idInfoArt, "-1", idViti, 0, mag, "");
            return new { artikulli = artikulli, infoArt = lista };
        }

        //internal static bool ekzistonKodBar(string kodbar, int idNdermarrje)
        //{
        //    return clsArtikulli.ekzistonKodbar(kodbar, idNdermarrje);
        //}

        public static object[] ktheRowVleraDetajimMeID(int idja, int rreshti, int lloji, string kodartikulli, int idkokamagazina, int idNdermarrje, int idPerdorues, string magazina, string dtDok, bool kontrolloImeiFifo, string[] listeIMEI, bool promocione, bool DokumentTransferimiOwn, bool merrPerberesit)///idkokamagazina perdoret per rastet kur kemi transferimet nga vodafoni per te pare nqs ky detajim eshte tek fatura e blerjes nga e cila eshte konvertuar fh
        {
            clsArtikulli artikulli = new clsArtikulli();
            artikulli.mbushArtikull(kodartikulli, idNdermarrje);

            DateTime dateDok;
            bool date = DateTime.TryParse(dtDok, out dateDok);

            var result = clsDetajimArtikulli.ktheRowVleraDetajimMeID(idja, rreshti, lloji, artikulli, idkokamagazina, idNdermarrje, idPerdorues, magazina, dateDok, kontrolloImeiFifo, listeIMEI, promocione, DokumentTransferimiOwn, date);


            if (!merrPerberesit || artikulli.Klasa != 4 || ((clsDetajimArtikulli)result[1]).IdDetajimArtikulli > 0)
            {
                return result;
            }
            colArtikulliPerberes artper = new colArtikulliPerberes(artikulli.IdArtikulli, date ? dateDok : DateTime.Now, new clsDatabaseInventari());
            colArtikujt artikujt = new colArtikujt(artper.Select(x => x.IdLidheseArt).ToList());
            foreach (var art in artikujt)
            {
                var resultPerberes = clsDetajimArtikulli.ktheRowVleraDetajimMeID(idja, rreshti, lloji, art, idkokamagazina, idNdermarrje, idPerdorues, magazina, dateDok, kontrolloImeiFifo, listeIMEI, promocione, DokumentTransferimiOwn, date);
                if (((clsDetajimArtikulli)resultPerberes[1]).IdDetajimArtikulli > 0)
                    return resultPerberes;
            }

            return result;
        }

        public static object[] ktheRowVleraDetajimMeKod(string kodi, int rreshti, int lloji, string kodartikulli, int idkokamagazina, int idNdermarrje, int idPerdorues, string magazina, string dtDok, bool kontrolloImeiFifo, string[] listeIMEI, bool promocione, bool DokumentTransferimiOwn, bool merrPerberesit)
        {
            clsArtikulli artikulli = new clsArtikulli();
            artikulli.mbushArtikull(kodartikulli, idNdermarrje);
            DateTime dateDok;
            bool date = DateTime.TryParse(dtDok, out dateDok);
            var result = clsDetajimArtikulli.ktheRowVleraDetajimMeKod(kodi, rreshti, lloji, artikulli, idkokamagazina, idNdermarrje, idPerdorues, magazina, dateDok, kontrolloImeiFifo, listeIMEI, promocione, DokumentTransferimiOwn, date, 
                merrPerberesit && artikulli.Klasa == 4);

            if(!merrPerberesit || artikulli.Klasa != 4 || ((clsDetajimArtikulli)result[1]).IdDetajimArtikulli > 0)
            {
                return result;
            }
            colArtikulliPerberes artper = new colArtikulliPerberes(artikulli.IdArtikulli, date ? dateDok : DateTime.Now, new clsDatabaseInventari());
            colArtikujt artikujt = new colArtikujt(artper.Select(x => x.IdLidheseArt).ToList());
            foreach(var art in artikujt)
            {
                var resultPerberes = clsDetajimArtikulli.ktheRowVleraDetajimMeKod(kodi, rreshti, lloji, art, idkokamagazina, idNdermarrje, idPerdorues, magazina, dateDok, kontrolloImeiFifo, listeIMEI, promocione, DokumentTransferimiOwn, date, false);
                if (((clsDetajimArtikulli)resultPerberes[1]).IdDetajimArtikulli > 0)
                    return resultPerberes;
            }

            return result;
            

        }
        public static string kontrollobundle(int[] idte)
        {

            for (int i = 0; i < idte.Length; i++)
            {
                if (idte[i] != 0)
                {
                    clsArtikulli art = new clsArtikulli(idte[i]);
                    if (art.KodOferte != "" && !art.AparatBazaar)
                    {
                        return art.KodOferte;
                    }
                }

            }
            return "";
        }
        public static List<string> ktheTvshSipasArtikullit(int[] idartikulli, string[] lloji)
        {
            List<string> result = new List<string>();
            for (int i = 0; i < idartikulli.Length; i++)
            {
                int idtvsh = 0;
                if (lloji[i] == "Artikull")
                    idtvsh = clsArtikulli.ktheIdTvsh(idartikulli[i]);
                else idtvsh = clsLlogari.ktheNivelTakse(idartikulli[i]);
                result.Add(clsTaksa.ktheKodTakseMeId(idtvsh));
            }
            return result;
        }


        public static DataTable ktheKonfigurimWebhhok(int idnderrmarje)
        {
        
               
            var dt = clsWebhooks.merrWebhookSipasNderm(idnderrmarje);

           
           return dt;
        }
        public static ListeVleraArtikulli ktheRowVleraIdArtDetajimTvsh(int idArt, int rreshti, DateTime data, string kodMag, int idPerdoruesi, KonfigurimTVSHGjateRregj llojTvsh, clsTaksa taksaKF, object sasiaNeGride, bool merrMagMeAutorizim, object magazinatKoka)
        {
            clsArtikulli artikulli = new clsArtikulli(idArt);
            int idNdermarrje = artikulli.IdNdermarje;
            ListeVleraArtikulli listeArt = new ListeVleraArtikulli();
            clsTaksa taksendermarje = new clsTaksa();
            taksendermarje.mbushTakseDefaultNdermarrje(idNdermarrje);
            colTaksa taksaNdermarrje = new colTaksa(idNdermarrje, LlojTakse.Nivel_Tvsh, idPerdoruesi);
            if (artikulli.IdArtikulli > 0)
            {
                artikulli.mbushKodBare();
                Dictionary<String, Object>[] magazinatTrupi = merrMagazinePerTrupDokumentiSipasLlojit(idNdermarrje, idPerdoruesi, merrMagMeAutorizim, magazinatKoka, artikulli, kodMag, String.Empty);
                int idMagazina = magazinatTrupi[0].Count == 0? 0 : int.Parse((((Dictionary<String, Object>)magazinatTrupi[0])["idMagArtKoka"]).ToString());
                listeArt.magazinatTrupi = magazinatTrupi;
                listeArt = artikulli.ktheVleraArtDetajime(idPerdoruesi, data, idMagazina, sasiaNeGride);
                listeArt.listeTvsh = artikulli.ktheTvsh(llojTvsh, idPerdoruesi, taksendermarje, taksaNdermarrje, taksaKF);
                listeArt.idRreshti = rreshti;
                listeArt.artikulli = artikulli;
                listeArt.detajimFundit = new clsMesazh(true);
                return listeArt;
            }
            listeArt.kerkoMeKodbar = false;
            listeArt.idRreshti = rreshti;
            listeArt.detajimFundit = new clsMesazh(true);
            return listeArt;
        }

        public static ListeVleraArtikulliMeNjesiKodbaresh ktheRowVleraIdArtTvsh(int idArt, int rreshti, DateTime data, int idPerdoruesi, KonfigurimTVSHGjateRregj llojTvsh, clsTaksa taksaKF, string kodMag, bool merrMagMeAutorizim, object magazinatKoka)
        {
            clsArtikulli artikulli = new clsArtikulli(idArt);
            clsTaksa taksendermarje = new clsTaksa();
            taksendermarje.mbushTakseDefaultNdermarrje(artikulli.IdNdermarje);
            DbCore.DbRegjistrim.colTaksa taksaNdermarrje = new DbCore.DbRegjistrim.colTaksa(artikulli.IdNdermarje, DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, idPerdoruesi);
            ListeVleraArtikulliMeNjesiKodbaresh listArt = new ListeVleraArtikulliMeNjesiKodbaresh();
            if (artikulli.IdArtikulli > 0)
            {
                artikulli.mbushKodBare();
                //listArt.gjendjeTot = clsTrupiMagazina.merrSasi(artikulli, -1, data, idDetajim);
                listArt.idRreshti = rreshti;
                listArt.artikulli = artikulli;
                listArt.listeTvsh = artikulli.ktheTvsh(llojTvsh, idPerdoruesi, taksendermarje, taksaNdermarrje, taksaKF);
                listArt.detajimFundit = new clsMesazh(true);
                Dictionary<String, Object>[] magazinatTrupi = merrMagazinePerTrupDokumentiSipasLlojit(artikulli.IdNdermarje, idPerdoruesi, merrMagMeAutorizim, magazinatKoka, artikulli, kodMag, String.Empty);
                listArt.magazinatTrupi = magazinatTrupi;
                return listArt;
            }
            listArt.kerkoMeKodbar = false;
            listArt.idRreshti = rreshti;
            return listArt;
        }
        public static object ktheRowVleraIdArtTvshEPlote(int idArt, string kodArt, int rreshti, DateTime data, string kodMag, int idPerdoruesi, KonfigurimTVSHGjateRregj llojTvsh, bool meDetajim, bool merrPershkrimMagazine, bool merrZbritjeAnalitike, int ZbritjaKlientit, int idNdermarrje, bool merrCmim, int NivelCmimi, string monedha, int njesiDef, double kursi, double sasia, int llojNiveli, bool gjendjeMinMax, bool kontrolloImeiFifo, string[][] listeImeiArtikull, bool promocione, clsTaksa taksaKF, object sasiaNeGride, object magazinatKoka, bool merrMagMeAutorizim, string detajim, bool merrSipasDetajimit, int counterWsKodi)
        {
            ListeVleraTePlotaArtikulli listArt = new ListeVleraTePlotaArtikulli();
            int idMag = clsNjesiAdministrative.ktheIdMagazine(kodMag, idNdermarrje);
            int iddetajim = 0;
            listArt.counterWsKodi = counterWsKodi;
            if (!String.IsNullOrEmpty(detajim) && detajim != "False" && detajim != "false")
            {
                iddetajim = clsDetajimArtikulli.ktheIdDetajimi(detajim, idNdermarrje);
            }

            if (meDetajim && idArt != -1)
            {
                ListeVleraArtikulli listeVleraArtikulli = ktheRowVleraIdArtDetajimTvsh(idArt, rreshti, data, kodMag, idPerdoruesi, llojTvsh, taksaKF, sasiaNeGride, merrMagMeAutorizim, magazinatKoka);
                listArt.idRreshti = listeVleraArtikulli.idRreshti;
                listArt.artikulli = listeVleraArtikulli.artikulli;
                listArt.gjendjeTot = listeVleraArtikulli.gjendjeTot;
                listArt.listeTvsh = listeVleraArtikulli.listeTvsh;
                listArt.detajimiPare = listeVleraArtikulli.detajimiPare;
                listArt.detajimiDyte = listeVleraArtikulli.detajimiDyte;
                listArt.kodbari = listeVleraArtikulli.kodbari;
                listArt.detajimFundit = listeVleraArtikulli.detajimFundit;
                listArt.magazinatTrupi = listeVleraArtikulli.magazinatTrupi;
                listArt.kerkoMeKodbar = listeVleraArtikulli.kerkoMeKodbar;
            }
            else
            {
                ListeVleraArtikulliMeNjesiKodbaresh listeVleraArtikulli;
                if (idArt != -1)
                    listeVleraArtikulli = ktheRowVleraIdArtTvsh(idArt, rreshti, data, idPerdoruesi, llojTvsh, taksaKF, kodMag, merrMagMeAutorizim, magazinatKoka);
                else if (meDetajim)
                    listeVleraArtikulli = ktheRowVleraKodArtTvsh(kodArt, rreshti, data, idPerdoruesi, llojTvsh, idNdermarrje, kontrolloImeiFifo, listeImeiArtikull, promocione, kodMag, taksaKF, sasiaNeGride, merrMagMeAutorizim, magazinatKoka, merrSipasDetajimit);
                else
                    listeVleraArtikulli = ktheRowVleraKodArtDetajimTvsh(kodArt, rreshti, data, kodMag, idPerdoruesi, llojTvsh, idNdermarrje, kontrolloImeiFifo, listeImeiArtikull, promocione, taksaKF, merrMagMeAutorizim, magazinatKoka, merrSipasDetajimit);
                listArt.idRreshti = listeVleraArtikulli.idRreshti;
                listArt.artikulli = listeVleraArtikulli.artikulli;
                listArt.gjendjeTot = listeVleraArtikulli.gjendjeTot;
                listArt.listeTvsh = listeVleraArtikulli.listeTvsh;
                listArt.detajimiPare = listeVleraArtikulli.detajimiPare;
                listArt.detajimiDyte = listeVleraArtikulli.detajimiDyte;
                listArt.kodbari = listeVleraArtikulli.kodbari;
                listArt.njesia = listeVleraArtikulli.njesia;
                listArt.detajimFundit = listeVleraArtikulli.detajimFundit;
                listArt.magazinatTrupi = listeVleraArtikulli.magazinatTrupi;
                listArt.kerkoMeKodbar = listeVleraArtikulli.kerkoMeKodbar;
            }
            clsArtikulli artikulli = listArt.artikulli;
            if (artikulli == null)
                return listArt;
            if (!String.IsNullOrEmpty(artikulli.KodArtikulli) && artikulli.Klasa == 4)
            {
                listArt.koefArtPerbere = ktheArtikullPerberes(artikulli.IdArtikulli);
            }
            if (merrPershkrimMagazine)
            {
                listArt.pershkrimMag = kthePershkrimMagSipasId(artikulli.IdMagazina != 0 ? artikulli.IdMagazina : idMag);
            }
            if (!String.IsNullOrEmpty(artikulli.KodArtikulli) && merrZbritjeAnalitike)
            {
                listArt.zbritjeAnalitike = ktheZbritjeAnalitikeArtikulliRow(artikulli.KodArtikulli, ZbritjaKlientit, Convert.ToString(data), idPerdoruesi, idNdermarrje);
            }
            if (!String.IsNullOrEmpty(artikulli.KodArtikulli) && merrCmim)
            {

                string njesia = String.Empty;
                if (meDetajim)
                {
                    if (listArt.njesia == 0)
                        njesia = njesiDef == 2 ? artikulli.KodNjesia2 : artikulli.KodNjesia1;
                    else
                        njesia = listArt.njesia == 2 ? artikulli.KodNjesia2 : artikulli.KodNjesia1;
                }
                else
                    njesia = njesiDef == 2 ? artikulli.KodNjesia2 : artikulli.KodNjesia1;
                int llojDetajimi = clsNivelCmimi.merrDetajimCmimeshNdermarrje(idNdermarrje);
                iddetajim = 0;
                switch (llojDetajimi)
                {
                    case (int)DbCore.CustomEntites.Detajim.Detajim1:
                        if (listArt.detajimiPare != null && listArt.detajimiPare.IdDetajimArtikulli > 0)
                            iddetajim = listArt.detajimiPare.IdDetajimArtikulli;
                        break;
                    case (int)DbCore.CustomEntites.Detajim.Detajim2:
                        if (listArt.detajimiDyte != null && listArt.detajimiDyte.IdDetajimArtikulli > 0)
                            iddetajim = listArt.detajimiDyte.IdDetajimArtikulli;
                        break;
                    default:
                        break;
                }
                //ketu vendoset cmimi
                listArt.cmimArtikulliResult = ktheCmimArtikulliRow(idNdermarrje, idPerdoruesi, artikulli.KodArtikulli, NivelCmimi, Convert.ToString(data), monedha, njesia, (decimal)kursi, rreshti, (decimal)sasia, llojNiveli, iddetajim);
            }
            if (gjendjeMinMax)
            {
                //string mag = !String.IsNullOrEmpty(listArt.pershkrimMag) ? artikulli.Magazina : kodMagazinaPotenciale;
                listArt.colGjendjeArtikulli = merrGjendjeArtikulliMag(artikulli.IdArtikulli, !String.IsNullOrEmpty(artikulli.Magazina) ? artikulli.Magazina : kodMag, idNdermarrje);
            }
            if (artikulli.IdArtikulli > 0)
                listArt.magazinatTrupi = merrMagazinePerTrupDokumentiSipasLlojit(idNdermarrje, idPerdoruesi, merrMagMeAutorizim, magazinatKoka, artikulli, kodMag, String.Empty);
            if (artikulli.IdArtikulli > 0)
                listArt.gjendjeMag = clsTrupiMagazina.merrSasi(artikulli, idMag, data, 0);
            return listArt;
        }

        public static ListeVleraArtikulliMeNjesiKodbaresh ktheRowVleraKodArtDetajimTvsh(string kodKodBarArt, int rreshti, DateTime data, string kodMag, int idPerdoruesi, KonfigurimTVSHGjateRregj llojTvsh, int idNdermarrje, bool kontrolloImeiFifo, string[][] listeImeiArtikull, bool promocione, clsTaksa taksaKF, bool merrMagMeAutorizim, object magazinatKoka, bool merrSipasDetajimit)
        {
            ListeVleraArtikulliMeNjesiKodbaresh listeArt = new ListeVleraArtikulliMeNjesiKodbaresh();
            bool detajim = false;
            bool kodbari = false;
            clsArtikulli artikulli = new clsArtikulli();
            artikulli.mbushArtikull(kodKodBarArt, idNdermarrje);
            if (artikulli.IdArtikulli <= 0)
            {
                artikulli.merrSipasKodbarit(kodKodBarArt, idNdermarrje);

                if (merrSipasDetajimit && artikulli.IdArtikulli <= 0)
                {
                    artikulli.ktheArtikullSipasDetajimit(kodKodBarArt, idNdermarrje);
                    detajim = true;
                }
                else kodbari = true;
            }
            if (artikulli.IdArtikulli > 0)
            {
                Dictionary<String, Object>[] magazinatTrupi = merrMagazinePerTrupDokumentiSipasLlojit(idNdermarrje, idPerdoruesi, merrMagMeAutorizim, magazinatKoka, artikulli, kodMag, String.Empty);
                int idMagazina = int.Parse((((Dictionary<String, Object>)magazinatTrupi[0])["idMagArtKoka"]).ToString());
                listeArt.magazinatTrupi = magazinatTrupi;
                clsTaksa taksendermarje = new clsTaksa();
                taksendermarje.mbushTakseDefaultNdermarrje(idNdermarrje);
                colTaksa taksaNdermarrje = new colTaksa(idNdermarrje, LlojTakse.Nivel_Tvsh, idPerdoruesi);
                listeArt = artikulli.ktheVleraArtDetajimeMeNjesiKodbaresh(idPerdoruesi, data, idMagazina);
                listeArt.listeTvsh = artikulli.ktheTvsh(llojTvsh, idPerdoruesi, taksendermarje, taksaNdermarrje, taksaKF);
                listeArt.idRreshti = rreshti;
                listeArt.artikulli = artikulli;
                listeArt.kerkoMeKodbar = kodbari;

                artikulli.mbushKodBare();
                if (detajim)
                {
                    clsDetajimArtikulli det = new clsDetajimArtikulli();
                    det.mbushDetajimArtikulli(kodKodBarArt, idNdermarrje);
                    colDetajimeArtikulli colDet = new colDetajimeArtikulli();
                    colDet.mbushDetajimeSipasArtikullitNdermarrjesDheLlojit(artikulli.KodArtikulli, artikulli.IdNdermarje, idPerdoruesi, 1);
                    if (colDet.Find(x => x.IdDetajimArtikulli == det.IdDetajimArtikulli) != null)
                        listeArt.detajimiPare = det;
                    else listeArt.detajimiDyte = det;
                    listeArt.detajimFundit = new clsMesazh(true);
                    if (kontrolloImeiFifo)
                    {
                        string[] listeImei = new string[listeImeiArtikull.Length];
                        if (listeImeiArtikull.Length > 0)
                        {
                            for (int i = 0; i < listeImeiArtikull.Length; i++)
                            {
                                if (listeImeiArtikull[i][0] == artikulli.KodArtikulli)
                                    listeImei[i] = listeImeiArtikull[i][1];
                            }
                        }
                        listeArt.detajimFundit = clsDetajimArtikulli.KontrolloDetajimFundit(kodKodBarArt, data, kodMag, artikulli.KodArtikulli, artikulli.IdNdermarje, listeImei, promocione);
                    }
                }
                else listeArt.detajimFundit = new clsMesazh(true);
                if (kodbari)
                {
                    clsKodbari kodb = new clsKodbari();
                    kodb.ktheKodbarSipasPershkrimit(kodKodBarArt, idNdermarrje);
                    listeArt.kodbari = kodKodBarArt;
                    listeArt.njesia = kodb.Njesia;
                    clsDetajimArtikulli det1 = new clsDetajimArtikulli(kodb.Detajim1);
                    listeArt.detajimiPare = det1;
                    clsDetajimArtikulli det2 = new clsDetajimArtikulli(kodb.Detajim2);
                    listeArt.detajimiDyte = det2;
                }
                else
                    listeArt.kodbari = "";

                return listeArt;
            }
            listeArt.idRreshti = rreshti;
            listeArt.detajimFundit = new clsMesazh(true);
            return listeArt;
        }

        public static object eshteVeprimILejuar(string emerKomponente, string veprimi, System.Web.SessionState.HttpSessionState Session)
        {
            bool veprimILejuar = true;
            DbCore.DbAdmin.clsKomponente oKomponente = new DbCore.DbAdmin.clsKomponente(emerKomponente);
            int indexKomponenteje = oKomponente.IdKomponente;
            DbCore.DbAdmin.clsKomponente komp = new DbCore.DbAdmin.clsKomponente(emerKomponente);
            DbCore.DbAdmin.clsTeDrejtaRoli tedrejta = new DbCore.DbAdmin.clsTeDrejtaRoli();
            tedrejta.merrTeDrejtaPerKeteKomponente(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdVitNdermarrje(Session), emerKomponente);
            if (veprimi == "Modifiko")
            {
                veprimILejuar = tedrejta.DPlot || tedrejta.DMod;
            }
            if (veprimi == "Shto")
            {
                veprimILejuar = tedrejta.DPlot || tedrejta.DShtim;
            }
            if (veprimi == "Lexo")
            {
                veprimILejuar = tedrejta.DPlot || tedrejta.DAmb;
            }
            if (veprimi == "Fshi")
            {
                veprimILejuar = tedrejta.DPlot || tedrejta.DFsh;
            }
            if (veprimILejuar)
            {
                return "true";
            }
            else
            {
                return "false";
            }
        }


        internal static object merrTotalAmortizimi(HttpSessionState Session)
        {

            var totali = ((clsAmortizimiKoka)mySessionObjects.merrObjectNgaSesioni(Session)).AmortizimiShteseTotal + ((clsAmortizimiKoka)mySessionObjects.merrObjectModNgaSesioni(Session)).AmortizimiShteseTotal;
            var mesazhi = mySessionObjects.merrMesazhNgaSesioni(Session);
            mySessionObjects.ruajMesazhNeSesion(Session, "");
            return new { totali = totali, mesazhi = mesazhi };

        }

        internal static object KtheTeDhenaLlojPeriudheNew(int kategoria, int selectedIdLlojPeriudhe)
        {
            return colLlojPeriudhe.KtheTeDhenaLlojPeriudheNew(kategoria, selectedIdLlojPeriudhe);
        }

        internal static List<string> KtheTeDhenaLlojPeriudhe(string vleratId)
        {
            int idKategoria = Convert.ToInt32(vleratId.Split(';')[0]);
            int idLlojPeriudha = Convert.ToInt32(vleratId.Split(';')[1]);
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();

            DbCore.DbAdmin.colLlojPeriudhe colLlojPeriudhe = new DbCore.DbAdmin.colLlojPeriudhe();
            colLlojPeriudhe = colLlojPeriudhe.merrLlojPeriudhashSipasIdKatNrAuto(idKategoria);

            List<string> vlerat = new List<string>();

            foreach (DbCore.DbAdmin.clsLlojPeriudhe periudha in colLlojPeriudhe)
            {
                vlerat.Add(periudha.IdLlojPeriudhe + ";" + periudha.LlojPeriudhePershkrimi);
            }

            vlerat.Add(idLlojPeriudha + ";");
            return vlerat;
        }

        public static ListeVleraArtikulliMeNjesiKodbaresh ktheRowVleraKodArtTvsh(string kodKodBarArt, int rreshti, DateTime data, int idPerdoruesi, KonfigurimTVSHGjateRregj llojTvsh, int idNdermarrje, bool kontrolloImeiFifo, string[][] listeImeiArtikull, bool promocione, string kodMag, clsTaksa taksaKF, object sasiteNeGrideObj, bool merrMagMeAutorizim, object magazinatKoka, bool merrSipasDetajimit)
        {
            ListeVleraArtikulliMeNjesiKodbaresh listArt = new ListeVleraArtikulliMeNjesiKodbaresh();
            bool detajim = false;
            bool kodbari = false;
            listArt.detajimFundit = new clsMesazh(true);
            clsArtikulli artikulli = new clsArtikulli();
            artikulli.ktheArtikullSipasKoditDheAutorizime(kodKodBarArt, idNdermarrje, idPerdoruesi);
            if (artikulli.IdArtikulli <= 0)
            {
                artikulli.merrSipasKodbarit(kodKodBarArt, idNdermarrje);
                if (merrSipasDetajimit && artikulli.IdArtikulli <= 0)
                {
                    artikulli.ktheArtikullSipasDetajimit(kodKodBarArt, idNdermarrje);
                    detajim = true;
                }
                else
                    kodbari = true;
            }

            listArt.kerkoMeKodbar = kodbari;
            if (artikulli.IdArtikulli > 0)
            {
                artikulli.mbushKodBare();
                listArt.idRreshti = rreshti;
                listArt.artikulli = artikulli;

                clsTaksa taksendermarje = new clsTaksa();
                taksendermarje.mbushTakseDefaultNdermarrje(idNdermarrje);
                colTaksa taksaNdermarrje = new DbCore.DbRegjistrim.colTaksa(idNdermarrje, LlojTakse.Nivel_Tvsh, idPerdoruesi);
                listArt.listeTvsh = artikulli.ktheTvsh(llojTvsh, idPerdoruesi, taksendermarje, taksaNdermarrje, taksaKF);
                double gjendjaTotale = DbCore.DbRegjistrim.clsTrupiMagazina.merrSasi(artikulli, -1, data, 0);
                Dictionary<String, Object>[] magazinatTrupi = merrMagazinePerTrupDokumentiSipasLlojit(idNdermarrje, idPerdoruesi, merrMagMeAutorizim, magazinatKoka, artikulli, kodMag, String.Empty);
                int idmagazina = magazinatTrupi[0].Count == 0 ? 0: int.Parse((((Dictionary<String, Object>)magazinatTrupi[0])["idMagArtKoka"]).ToString());
                listArt.magazinatTrupi = magazinatTrupi;
                Dictionary<String, Object>[] detajimDheSasi = new Dictionary<String, Object>[2];
                detajimDheSasi = clsArtikulli.ktheArtDetajimet(artikulli, idPerdoruesi, data, idmagazina, gjendjaTotale, sasiteNeGrideObj);
                listArt.detajimiPare = detajimDheSasi[0] == null ? null : (clsDetajimArtikulli)((Dictionary<String, Object>)detajimDheSasi[0])["detajim"];
                listArt.detajimiDyte = detajimDheSasi[1] == null ? null : (clsDetajimArtikulli)((Dictionary<String, Object>)detajimDheSasi[1])["detajim"];
                listArt.gjendjeTot = detajimDheSasi[0] == null ? gjendjaTotale : (double)((Dictionary<String, Object>)detajimDheSasi[0])["sasiTotDet"];

                if (detajim)
                {
                    clsDetajimArtikulli det = new clsDetajimArtikulli();
                    det.mbushDetajimArtikulli(kodKodBarArt, idNdermarrje);
                    colDetajimeArtikulli colDet = new colDetajimeArtikulli();
                    colDet.mbushDetajimeSipasArtikullitNdermarrjesDheLlojit(artikulli.KodArtikulli, artikulli.IdNdermarje, idPerdoruesi, 1);
                    if (colDet.Find(x => x.IdDetajimArtikulli == det.IdDetajimArtikulli) != null)
                        listArt.detajimiPare = det;
                    else listArt.detajimiDyte = det;
                    if (kontrolloImeiFifo)
                    {
                        string[] listeImei = new string[listeImeiArtikull.Length];
                        if (listeImeiArtikull.Length > 0)
                        {
                            for (int i = 0; i < listeImeiArtikull.Length; i++)
                            {
                                if (listeImeiArtikull[i][0] == artikulli.KodArtikulli)
                                    listeImei[i] = listeImeiArtikull[i][1];
                            }
                        }
                        listArt.detajimFundit = clsDetajimArtikulli.KontrolloDetajimFundit(kodKodBarArt, data, kodMag, artikulli.KodArtikulli, artikulli.IdNdermarje, listeImei, promocione);
                    }
                }
                if (kodbari)
                {
                    clsKodbari kodb = new clsKodbari();
                    kodb.ktheKodbarSipasPershkrimit(kodKodBarArt, idNdermarrje);
                    listArt.kodbari = kodKodBarArt;
                    listArt.njesia = kodb.Njesia;
                    listArt.detajimiPare = kodb.Detajim1 > 0 ? new clsDetajimArtikulli(kodb.Detajim1) : listArt.detajimiPare;
                    listArt.detajimiDyte = kodb.Detajim2 > 0 ? new clsDetajimArtikulli(kodb.Detajim2) : listArt.detajimiDyte;
                }
                else
                    listArt.kodbari = "";
                return listArt;
            }
            
            listArt.idRreshti = rreshti;
            return listArt;
        }

        public static string ktheTargeAutomjeti(int idAutomjeti)
        {
            clsAutomjete auto = new clsAutomjete();
            auto.mbushAutomjet(idAutomjeti);
            if (auto.IdAutomjeti <= 0)
                return null;
            else
            {
                string targa = (clsAutomjete.ktheTargeAutomjetSipasId(idAutomjeti)).ToString();
                return targa;
            }
        }

        public static object ktheBuxhetimViteSipasKonfigurimit(HttpSessionState session, string kodKonfigAmbjente)
        {
            clsPeriudhaKontabel periudhaKontabel = mySessionObjects.merrPeriudheKontabel(session);
            int viti = periudhaKontabel.FillimiPeriudha.Year;
            int idKonfigAmbjente = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(kodKonfigAmbjente, mySessionObjects.merrIdNdermarrjeSesioni(session));
            int nrVitesh = clsKusht.kthevlereSipasKushtitDheIdKonfig(idKonfigAmbjente, "NVB");
            return new { viti = viti, nrVitesh = nrVitesh };
        }

        public static colKonfigurimAmbjenti ktheTemplateNiveliAmb(int idNiveli, string veprimi, bool mod, int idPerdoruesi, int idNdermarrje, int idGjuha)
        {
            int idKategori;
            switch (veprimi)
            {
                case "shitje":
                case "shitjediscount":
                case "bazaar":
                    idKategori = 1;
                    break;

                case "blerje":
                    idKategori = 2;
                    break;

                case "magazina":
                    idKategori = 6;
                    break;
                case "inventarizim":
                    idKategori = 135;
                    break;
                case "ndryshimcmimisasi":
                    idKategori = 95;
                    break;

                case "fletekontabel":
                    idKategori = 5;
                    break;

                case "banka":
                    idKategori = 4;
                    break;

                case "arka":
                    idKategori = 3;
                    break;

                case "vkf":
                    idKategori = 20;
                    break;

                case "shsh":
                    idKategori = 7;
                    break;

                case "qendrakosto":
                    idKategori = 75;
                    break;

                case "rezervime":
                    idKategori = 78;
                    break;

                case "riparime":
                    idKategori = 80;
                    break;

                case "amortizimi":
                    idKategori = 86;
                    break;

                case "rivleresimAm":
                    idKategori = 90;
                    break;
                case "rialokimB":
                    idKategori = 175;
                    break;
                case "perfitimB":
                    idKategori = 177;
                    break;
                case "planifikimEkzekutimiB":
                    idKategori = 179;
                    break;
                case "ekzekutimB":
                    idKategori = 181;
                    break;
                default:
                    idKategori = 0;
                    break;
            }
            colKonfigurimAmbjenti modelet = new colKonfigurimAmbjenti();
            if (idNiveli != 0)
                modelet.mbushKonfigAmbjSipasIdKategoriIdNivel(idKategori, idNiveli, idPerdoruesi, idGjuha, true);
            else
                modelet.mbushKonfigAmbjSipasIdKategori(idKategori, idNdermarrje, idPerdoruesi, idGjuha);
            if (modelet.Count == 0)
                modelet.mbushKonfigDefaultKategori(idKategori);

            colKonfigurimAmbjenti konfVarura = new colKonfigurimAmbjenti();
            foreach (clsKonfigurimAmbjenti konfi in modelet)
            {
                var alternativa = clsAlternativaKushti.getAlternativa(konfi.IdKonfigAmbjente, "V");
                if (alternativa == "Po" && !mod)
                {
                    konfVarura.Add(konfi);
                }
            }
            foreach (clsKonfigurimAmbjenti konfi in konfVarura)
            {
                modelet.Remove(konfi);
            }
            return modelet;
        }
        public static konfigAmbientiSlim[] ktheTemplateNiveli(int idNiveli, string veprimi, bool mod, int idPerdoruesi, int idNdermarrje, int idGjuha)
        {
            colKonfigurimAmbjenti modelet = ktheTemplateNiveliAmb(idNiveli, veprimi, mod, idPerdoruesi, idNdermarrje, idGjuha);
            konfigAmbientiSlim[] modeletSlim = new konfigAmbientiSlim[modelet.Count];
            for (int i = 0; i < modelet.Count; i++)
            {
                modeletSlim[i] = new konfigAmbientiSlim() { IdKonfigAmbjente = modelet[i].IdKonfigAmbjente, KodKonfigAmbjente = modelet[i].KodKonfigAmbjente, PershkrimKonfigAmbjente = modelet[i].PershkrimKonfigAmbjente };
            }
            return modeletSlim;
        }
                
        public static ListeVleraLlogaria KtheVleraLlogIDTvsh(int idja, int rreshti, KonfigurimTVSHGjateRregj llojTvsh, int idPerdoruesi, clsTaksa taksaKF)
        {
            ListeVleraLlogaria result = new ListeVleraLlogaria() { idRreshti = rreshti };
            clsLlogari llogaria = new clsLlogari(idja);
            if (llogaria.IdLlogari > 0)
            {
                clsTaksa taksendermarje = new clsTaksa();
                taksendermarje.mbushTakseDefaultNdermarrje(llogaria.IdNdermarja);
                DbCore.DbRegjistrim.colTaksa taksaNdermarrje = new DbCore.DbRegjistrim.colTaksa(llogaria.IdNdermarja, DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, idPerdoruesi);
                result.llogaria = llogaria;
                result.listeTvsh = llogaria.ktheTvsh(llojTvsh, idPerdoruesi, taksendermarje, taksaNdermarrje, taksaKF);
                return result;
            }
            return result;
        }

        public static ListeVleraLlogaria KtheVleraLlogKodTvsh(string kodi, int rreshti, KonfigurimTVSHGjateRregj llojTvsh, int idPerdoruesi, int idNdermarrje, clsTaksa taksaKF)
        {
            ListeVleraLlogaria result = new ListeVleraLlogaria() { idRreshti = rreshti };
            clsLlogari llogaria = new clsLlogari();
            llogaria.merrLlogariAktiveSipasKodit(kodi, idNdermarrje);
            if (llogaria.IdLlogari > 0)
            {
                clsTaksa taksendermarje = new clsTaksa();
                taksendermarje.mbushTakseDefaultNdermarrje(idNdermarrje);
                DbCore.DbRegjistrim.colTaksa taksaNdermarrje = new DbCore.DbRegjistrim.colTaksa(idNdermarrje, DbCore.DbRegjistrim.LlojTakse.Nivel_Tvsh, idPerdoruesi);
                result.llogaria = llogaria;
                result.listeTvsh = llogaria.ktheTvsh(llojTvsh, idPerdoruesi, taksendermarje, taksaNdermarrje, taksaKF);
                return result;
            }
            return result;
        }

        public static ListeVleraLlogaria KtheVleraLlogShpenzimiID(int idja, int rreshti, int idPerdoruesi)
        {
            ListeVleraLlogaria result = new ListeVleraLlogaria() { idRreshti = rreshti };
            clsLlogari llogaria = new clsLlogari(idja);
            if (llogaria.IdLlogari > 0)
            {
                result.llogaria = llogaria;
                return result;
            }
            return result;
        }

        public static ListeVleraLlogaria KtheVleraLlogShpenzimiMeKod(string kodi, int rreshti, int idPerdoruesi, int idNdermarrje)
        {
            ListeVleraLlogaria result = new ListeVleraLlogaria() { idRreshti = rreshti };
            clsLlogari llogaria = new clsLlogari();
            llogaria.merrLlogariAktiveSipasKodit(kodi, idNdermarrje);
            if (llogaria.IdLlogari > 0)
            {
                result.llogaria = llogaria;
                return result;
            }
            return result;
        }

        public static clsKategoriShpenzimi KtheVleraKatShpenzimiMeId(int id, int rreshti, int idPerdoruesi, int idNdermarrje)
        {

            clsKategoriShpenzimi result = new clsKategoriShpenzimi();
            clsKategoriShpenzimi kat = new clsKategoriShpenzimi(id);
            result = kat;

            return result;
        }

        public static clsKategoriShpenzimi KtheVleraKatShpenzimiMeKod(string kodi, int rreshti, int idPerdoruesi, int idNdermarrje)
        {

            clsKategoriShpenzimi result = new clsKategoriShpenzimi();
            clsKategoriShpenzimi kat = new clsKategoriShpenzimi(kodi, idNdermarrje);
            result = kat;

            return result;
        }

        public static object KtheVleraMagazinaMeKod(string kodi, int idNdermarrje, string idja, DateTime datedok)
        {
            clsNjesiAdministrative magazina = new clsNjesiAdministrative(kodi, idNdermarrje);
            return clsFunksione.KtheVleraMagazineDheArtikulli(magazina, idja, datedok);
        }

        public static object KtheVleraMagazinaMeID(int id, string idja, DateTime datedok)
        {
            clsNjesiAdministrative magazina = new clsNjesiAdministrative(id);
            return clsFunksione.KtheVleraMagazineDheArtikulli(magazina, idja, datedok);
        }

        public static clsZbritjeAnalitike ktheZbritjeAnalitikeArtikulliRow(string kodArtikulli, int ZbritjaKlientit, string date, int idPerdoruesi, int idNdermarrje)
        {
            return clsZbritjeAnalitike.ktheZbritjeAnalitikeArtikulliRow( kodArtikulli,  ZbritjaKlientit,  date,  idPerdoruesi,  idNdermarrje);

        }

        public static object[] mbushFushaPerKonvertim(object result, bool merrtedhena, int idNdermarrje, int idPerdorues)
        {
            object[] objekt = new object[13];
            int idklient = 0;
            colTrupiShitje col = new colTrupiShitje();
            if (!merrtedhena)
            {
                objekt[9] = null;
                objekt[10] = null;
            }

            List<object> colKodbaregjithe = new List<object>();
            object[] dokumenti = JsonConvert.DeserializeObject<object[]>(result.ToString());
            for (int i = 0; i < dokumenti.Length; i++)
            {
                int idDok = Convert.ToInt32(((object[])dokumenti[i])[0]);
                if (idklient == 0 && Convert.ToInt32(((object[])dokumenti[i])[4]) != 0)
                {
                    idklient = Convert.ToInt32(((object[])dokumenti[i])[4]);
                }
                clsNivelRegjistrimi niveli = new clsNivelRegjistrimi();
                int idKategoria = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(Convert.ToInt32(((object[])dokumenti[i])[3]));
                //niveli.mbushNivelRegjistrimiSipasIdMeKonvertime(Convert.ToInt32(((object[])dokumenti[i])[3]));
                List<object> colKodbari = new List<object>();
                if (idKategoria == 6)//magazina
                {
                    if (merrtedhena)
                    {
                        objekt[9] = null;
                        clsKokaMagazina mag = new clsKokaMagazina();
                        mag.mbushKokaMagazinaSipasID(idDok);
                        objekt[10] = mag;
                        colKodbari = clsFunksione.ConvertDataTabletoList(DbCore.DbInventari.colKodbare.merrKodbarArtikulliNeTrupDokShitje(idDok));
                    }
                    col.AddRange(mbushTrupiMagazina(idDok, idNdermarrje));
                }
                else
                {
                    if (merrtedhena)
                    {
                        objekt[10] = null;
                        clsKokaShitje shit = new clsKokaShitje();
                        shit.mbushKokaShitjeSipasIDPaTrup(idDok);
                        shit.mbushTrupShitje();
                        objekt[9] = shit; //TOCHECK Nestila --te duhet trupi?!
                        colKodbari = clsFunksione.ConvertDataTabletoList(DbCore.DbInventari.colKodbare.merrKodbarArtikulliNeTrupDokShitje(idDok));
                    }
                    col.AddRange(mbushTrupi(idDok));
                }
                colKodbaregjithe.AddRange(colKodbari);
            }
            objekt[0] = idklient;
            objekt[1] = (col);
            objekt[2] = (col.ktheColArtikuj());
            objekt[3] = (col.ktheColMakrot());
            objekt[4] = (col.ktheColLlogarite());

            objekt[5] = (col.ktheColDetArt());
            objekt[6] = (col.ktheColMag(idPerdorues));
            objekt[7] = (col.ktheColNjesiArt());
            objekt[8] = (col.ktheColTaksa());
            objekt[11] = (col.ktheColDetArt2());
            objekt[12] = colKodbaregjithe; //clsFunksione.ConvertDataTabletoList(colKodbare.merrKodbarArtikulliNeTrupDok(ids[i]));
            return objekt;
        }

        public static ListeVleraInfo mbushInfoArtikulliMeDetajime(int idArt, DateTime data, string detajim, int rreshti, int idInfo, string detajim2, int idViti, int idPerdoruesi, int idklient, string mag, string njesiart, int idKarta)
        {
            data = data.ToLocalTime();
            clsArtikulli artikulli = new clsArtikulli(idArt);
            ListeVleraInfo lista = artikulli.merrInfoArtSipasIdKokaDheVisibleVlera(idPerdoruesi, data, detajim, idInfo, detajim2, idViti, idklient, mag, njesiart, idKarta);
            lista.idRreshti = rreshti;
            return lista;
        }

        public static ListeVleraInfo mbushInfoLlogarie(int idja, DateTime data, int rreshti, int idInfo, int idNderVit, DateTime dt)
        {
            data = data.ToLocalTime();
            clsLlogari llogari = new clsLlogari(idja);
            ListeVleraInfo lista = llogari.merrInfoLlogSipasIdKokaDheVisibleVlera(data, idInfo, idNderVit, dt);
            lista.idRreshti = rreshti;
            return lista;
        }
        public static string merrEmertimKlienti(int idKlient)
        {
            clsKlientFurnitor klient = new clsKlientFurnitor(idKlient);
            string emertimKF = klient.EmertimiKF.ToString();
            return emertimKF;
        }
        public static int ktheidAutomjetSipasShasise(string auto, int idNdermarrje)
        {
            int idAutomjeti = DbCore.DbInventari.clsAutomjete.ktheidAutomjetSipasShasise(auto, idNdermarrje);
            return idAutomjeti;
        }

    



        public static colGjendjeArtikulli merrGjendjeArtikulliMag(int idartikulli, String mag, int idNdermarrje)
        {
            colGjendjeArtikulli col = new colGjendjeArtikulli();
            col.ktheGjendjeMinMaxArtikulliSipasMag(idartikulli, mag, idNdermarrje);
            return col;
        }

        public static string[] merrurlMedianInputCheck()
        {
            string[] linkMedianInputCheck = new string[2];
            linkMedianInputCheck[0] = WebConfigurationManager.AppSettings["urlMedianInputCheck"];
            linkMedianInputCheck[1] = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar();

            return linkMedianInputCheck;
        }

        public static double merrKursiSipasMonedhesDatesDheLlojit(int idMonedha, DateTime date, int lloji)
        {
            return clsFunksione.merrKursiSipasMonedhesDatesDheLlojit(idMonedha, date, lloji);
        }

        public static bool[] MerrMenuPerPerdoruesSipasSkemes(int idskema, int idperdoruesi, bool isNotModifikim, string status, int idkokashitje, string kodkonf, int idlloji)
        {
            return clsFunksione.merrMenu(idperdoruesi, idskema, isNotModifikim, status, idkokashitje, kodkonf, idlloji);
        }

        public static Object merrPerqindjeAgjenti(int idAgj, string llojAgj, int idKlient)
        {
            //clsAgjentShitje Agjent = new clsAgjentShitje(idAgj);
            double perqindja = clsAgjentShitje.merrPerqindjeAgjentiEdheSipasKlientit(idAgj, idKlient, int.Parse(llojAgj));
            return new { perqindjeAgjenti = perqindja, llojAgenti = llojAgj };
        }

        public static object[] KontrolloKthim(int id,bool kthim, string pageId)

        {
            object[] result = new object[2];
            result[0] = id;
            int[] ids = new int[1];
            ids[0] = id;
            
            var myPageCache = GlobalCacheManager.GetPageCacheByPageID(pageId);
            myPageCache["idkonvertimi"] = ids;

            result[1] = "";
            if (clsKokaShitje.KaMagazinaTePaRuajtura(id))
                result[1] = "Ka dokumenta magazine me status draft per kete dokument blerje!";
            else
            {
                clsKokaShitje koka = new clsKokaShitje(id);
                string kthehet = kthim ? "kthehet" : "blihet";
                clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti(koka.IdKonfigAmbjente);
                if (konf.KodKonfigAmbjente != "FB")
                    result[1] = "Ky dokument nuk mund te " + kthehet + "!";
                else
                {
                    clsGrupimDokumentiKoka grup = new clsGrupimDokumentiKoka(koka.IdGrup1);
                    if (grup.Kodi != "Aparate")
                        result[1] = "Ky dokument nuk mund te " + kthehet + "!";

                    else if (!clsKokaShitje.KaTrupiShitjeNgaKokaPerKthim(id))
                    {
                        string kthyer = kthim ? "kthyer" : "blere";
                        if (clsKokaShitje.KaDokumetKthimi(id))
                            result[1] = "Ky dokument eshte " + kthyer + " komplet!";
                        else
                            result[1] = "Nuk ka IMEI per tu " + kthyer + "!";
                    }
                }
            }
            return result;
        }
        public static object[] merrUrlKthim(int id, int idNdermarrje, string pageId)
        {
            List<Int32> idte = new List<int>();
            int[] ids = new int[1];
            ids[0] = id;
            
            var myPageCache = GlobalCacheManager.GetPageCacheByPageID(pageId);
            myPageCache["idkonvertimi"] = ids;

            object[] result = new object[2];
            clsKokaShitje koka = new clsKokaShitje();
            koka.mbushKokaShitjeSipasIDPaTrup(id);
            colTrupiShitje col = new colTrupiShitje();
            col.ktheGjitheTrupiShitjeNgaKokaKthim(id, idNdermarrje);
            result[0] = col;
            if (col.Count == 0)
            {
                result[1] = "negative";
                return result;
            }
            if (koka.IdStatusDok == 0)
            {
                result[1] = "draft";
                return result;
            }
            clsNivelRegjistrimi niv = new clsNivelRegjistrimi();
            niv.IdNivel = koka.IdNivel;
            niv.merrNivelRegjSipasId();
            if (niv.Kodi != "FSH" && niv.Kodi != "FB")
            {
                result[1] = "joFature";
                return result;
            }

            if ((koka.TotaliMeZbritjeMeTVSH > 0 && koka.VleraMbetur <= 0) || (koka.TotaliMeZbritjeMeTVSH < 0 && koka.VleraMbetur >= 0))
            {
                result[1] = "likuiduar";
                return result;
            }
            if (koka.Totali < 0)
            {
                result[1] = "negative";
                return result;
            }
            else
            {
                result[1] = "";
                return result;
            }
        }

        public static string merrUrlPaguaj(int id, string veprimi, string vjenNga, int idPerdoruesi, int idNdermarrje, int idVitNdermarrje)
        {
            List<Int32> idte = new List<int>();

            clsKokaShitje koka = new clsKokaShitje();
            koka.mbushKokaShitjeSipasIDPaTrup(id);
            if (koka.IdStatusDok == 0)
                return "draft";

            if ((koka.TotaliMeZbritjeMeTVSH > 0 && koka.VleraMbetur <= 0) || (koka.TotaliMeZbritjeMeTVSH < 0 && koka.VleraMbetur >= 0))
                return "likuiduar";
            if (koka.TotaliMeZbritjeMeTVSH == 0)
                return "totali0";
            //clsNivelRegjistrimi niv = new clsNivelRegjistrimi();
            //niv.mbushNivelRegjistrimiSipasID(kokaKategoriZbritje.IdNivel);
            string kodniveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(koka.IdNivel);
            if (kodniveli != "FSH" && kodniveli != "FB")
                return "joFature";
            string arkabanka = "arka";
            idte.Add(id);
            if (koka.IdKlientFurnitor != 0)
            {
                clsBanka banka = new clsBanka();
                banka.mbushBanke(clsKlientFurnitor.MerrIdBanke(koka.IdKlientFurnitor));
                if (banka.LlojArkaBanka == true)
                    arkabanka = "banka";
                else arkabanka = "arka";
            }
            if (koka.TotaliMeZbritjeMeTVSH < 0)
                if (veprimi == "shitje")
                    veprimi = "blerje";
                else veprimi = "shitje";
            string serializeid = JsonConvert.SerializeObject(idte);
            string url = "";
            if (veprimi == "shitje" && arkabanka == "arka")
                url = "ShtoVeprimBanka.aspx?lloji=arketim&shtim_modifikim=shtim&id=0&vjenNga=" + vjenNga + "&idfatura=" + serializeid;
            else if (veprimi == "shitje" && arkabanka == "banka")
                url = "ShtoVeprimBanka.aspx?lloji=derdhje&shtim_modifikim=shtim&id=0&vjenNga=" + vjenNga + "&idfatura=" + serializeid;
            else if (veprimi == "blerje" && arkabanka == "arka")
                url = "ShtoVeprimBanka.aspx?lloji=pagese&shtim_modifikim=shtim&id=0&vjenNga=" + vjenNga + "&idfatura=" + serializeid;
            else if (veprimi == "blerje" && arkabanka == "banka")
                url = "ShtoVeprimBanka.aspx?lloji=terheqje&shtim_modifikim=shtim&id=0&vjenNga=" + vjenNga + "&idfatura=" + serializeid;
            //DbCore.DbAdmin.clsKomponente komp = new DbCore.DbAdmin.clsKomponente(url.Split('&')[0]);
            //DbCore.DbAdmin.clsTeDrejtaRoli tedrejta = komp.merrTeDrejtaPerKeteAmbjent(DbCore.mySessionObjects.ktheIdPerdoruesi(Session), DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdVitNdermarrje(Session));

            clsTeDrejtaRoli tedrejta = new clsTeDrejtaRoli();
            tedrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idVitNdermarrje, url.Split('&')[0]);

            if (tedrejta.DAmb == false)
                return "ska te drejta";
            return url;
        }

        public static string ruajHapurMbyllur( bool hapur, int idperdorues, int idperdoruesveprimi)
        {
            clsMesazh mesazh = clsPerdorues.ndryshoInfo(idperdorues, idperdoruesveprimi , hapur);
            return mesazh.PershkrimMesazhi;
        }

        public static string ruajHapurMbyllurminus(bool hapur, int idperdorues, int idperdoruesveprimi)
        {
            clsMesazh mesazh = clsPerdorues.ndryshoInfominus(idperdorues, idperdoruesveprimi, hapur);
            return mesazh.PershkrimMesazhi;
        }

        public static string ruajHapurMbyllurplus(bool hapur, int idperdorues, int idperdoruesveprimi)
        {
            clsMesazh mesazh = clsPerdorues.ndryshoInfoplus(idperdorues, idperdoruesveprimi, hapur);
            return mesazh.PershkrimMesazhi;
        }

        private static IEnumerable<clsTrupiShitje> mbushTrupi(int idkoka)
        {
            colTrupiShitje col = new colTrupiShitje();
            col.mbushGjitheTrupiShitjeNgaKoka(idkoka);
            foreach (clsTrupiShitje tr in col)
            {
                colDetajimeArtikulliRegjistrim detajimet = new colDetajimeArtikulliRegjistrim(tr.IdShitjeTrupi);
                tr.OColDetajimet = detajimet;
                if (tr.OColDetajimet.Count == 1)
                    tr.IdDetajimArt = tr.OColDetajimet[0].IdDetajim;
            }
            return col;
        }

        private static IEnumerable<clsTrupiShitje> mbushTrupiMagazina(int idkoka, int idNdermarrje)
        {
            colTrupiMagazina col = new colTrupiMagazina();
            col.mbushGjitheTrupiMagazinaNgaKoka(idkoka);
            colTrupiShitje colshitje = new colTrupiShitje();
            foreach (clsTrupiMagazina tr in col)
            {
                int idtvsh = 0;
                clsArtikulli art = new clsArtikulli(tr.IdArtikulli);
                if (art.IdTvsh != 0)
                    idtvsh = art.IdTvsh;
                else
                {
                    clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);
                    if (nderm.IdTakse != 0)
                        idtvsh = nderm.IdTakse;
                }
                string tvsh = "Pa TVSH";
                double vleratvsh = 0;

                if (idtvsh != 0)
                {
                    clsTaksa taksa = new clsTaksa(idtvsh);
                    tvsh = taksa.KodTaksa;
                    vleratvsh = double.Parse(taksa.NormaPerqindje.ToString());
                }
                clsTrupiShitje trupi = new clsTrupiShitje(0, 0, tr.IdLlojVeprimi, art.KodArtikulli, art.PershkrimArtikulli, tr.IdDetajimi, tr.IdNjesia, tr.Sasia, tr.Cmimi, 0, tr.Vlefta * (1 + vleratvsh / 100), idtvsh, tr.Vlefta, tr.IdArtikulli, tr.IdMag, 0, 0, 0, "", DateTime.Today, DateTime.Today, 0, 0, 0, 0, 0, 0, 0, art, "", 1, 0, 0, 0, "", "");
                colshitje.Add(trupi);
            }
            return colshitje;
        }

        public static object ktheRowVleraArtMeID(int idja, int rreshti, DateTime data, string kodMagazine, object sasiaNeGride, object magazinatKoka, bool merrMagMeAutorizim, bool meDetajim, int idDetajim, string kodMagDest, int idNdermarrje, int idPerdoruesi, int njesiDef, decimal sasiaNeRresht, bool merrCmim)
        {
            var detajimiPare = new clsDetajimArtikulli();
            var detajimiDyte = new clsDetajimArtikulli();
            var artikulli = new clsArtikulli(idja);
            object[] cmimi = new object[3];
            cmimi[0] = rreshti;
            if (artikulli.IdArtikulli <= 0)
            {
                artikulli = null;
                detajimiPare = null;
                detajimiDyte = null;

                return new
                {
                    idRreshti = rreshti,
                    artikulli = artikulli,
                    gjendjetot = 0,
                    gjendja = 0,
                    detajimi = detajimiPare,
                    detajimi2 = detajimiDyte,
                    gjendjetotdet2 = 0,
                    vleftat = 0,
                    cmimi = cmimi
                };
            }

            data = data.ToLocalTime();
            artikulli.mbushKodBare();
            Dictionary<String, Object>[] magazinatTrupi = merrMagazinePerTrupDokumentiSipasLlojit(idNdermarrje, idPerdoruesi, merrMagMeAutorizim, magazinatKoka, artikulli, kodMagazine, kodMagDest);
            int idmag = clsNjesiAdministrative.ktheIdMagazine(kodMagazine, idNdermarrje);
            double gjendjaTotale = DbCore.DbRegjistrim.clsTrupiMagazina.merrSasi(artikulli, -1, data, 0);

            if (merrCmim)
            {
                //ketu vendoset cmimi
                clsNivelCmimi nivelBaze = new clsNivelCmimi();
                nivelBaze.mbushNivelCmimiBaze(idNdermarrje, 1);
                if (nivelBaze.IdNivelCmimi != 0) {
                    clsMonedha monBaze = new clsMonedha();
                    monBaze.mbushMonedhenENdermarrjes(idNdermarrje);
                    clsNjesiArtikulli njesia = njesiDef == 2 ? new clsNjesiArtikulli(artikulli.KodNjesia2, idNdermarrje) : new clsNjesiArtikulli(artikulli.KodNjesia1, idNdermarrje);
                    object[] cmimires = clsFunksione.merrCmimSipasNivelit(nivelBaze.IdNivelCmimi, artikulli.KodArtikulli, idPerdoruesi, njesia, monBaze, Convert.ToString(data), 1, sasiaNeRresht, idNdermarrje, 1, new clsDatabaseInventari(), 0, true);
                    cmimi[1] = cmimires[0];
                    cmimi[2] = cmimires[1];
                }
            }
            if (!meDetajim)
            {
                return new
                {
                    idRreshti = rreshti,
                    magazinatTrupi = magazinatTrupi,
                    artikulli = artikulli,
                    gjendjetot = gjendjaTotale,
                    gjendja = clsTrupiMagazina.merrSasi(artikulli, idmag, data, idDetajim),
                    vleftat = clsTrupiMagazina.ktheVleftenTotaleSipasArtikullitHPD(artikulli.IdArtikulli, idmag, data, int.MaxValue),
                    cmimi = cmimi
                };
            }

            int idmagazina = 0;
            if (magazinatTrupi != null)
                idmagazina = int.Parse((((Dictionary<String, Object>)magazinatTrupi[0])["idMagArtKoka"]).ToString());
            else
                idmagazina = idmag;
            Dictionary<String, Object>[] detajimDheSasi = new Dictionary<String, Object>[2];
            detajimDheSasi = clsArtikulli.ktheArtDetajimet(artikulli, idPerdoruesi, data, idmagazina, gjendjaTotale, sasiaNeGride);

            return new
            {
                idRreshti = rreshti,
                artikulli = artikulli,
                gjendjetot = detajimDheSasi[0] == null ? gjendjaTotale : (double)detajimDheSasi[0]["sasiTotDet"],
                detajimi = detajimDheSasi[0] == null ? null : (clsDetajimArtikulli)(detajimDheSasi[0])["detajim"],
                detajimi2 = detajimDheSasi[1] == null ? null : (clsDetajimArtikulli)(detajimDheSasi[1])["detajim"],
                gjendjetotdet2 = detajimDheSasi[1] == null ? default(double) : (double)(detajimDheSasi[1])["sasiTotDet"],
                magazinatTrupi = magazinatTrupi,
                gjendja = clsTrupiMagazina.merrSasi(artikulli, idmag, data, 0),
                cmimi = cmimi
            };
        }

        //merr magazinen e duhur qe duhet te vendoset per artikullin tek rreshti qe po shtohet
        public static Dictionary<String, Object>[] merrMagazinePerTrupDokumentiSipasLlojit(int idNderm, int idPerd, bool merrMagMeAutorizim, object magazinatKoka, clsArtikulli artikulli, string kodMag, string kodMagDest)
        {
            if (magazinatKoka == null)
                return null;
            Dictionary<String, Object>[] magazinat = new Dictionary<String, Object>[2];
            Dictionary<string, string> magKoka = JsonConvert.DeserializeObject<Dictionary<string, string>>(magazinatKoka.ToString());
            magazinat[0] = ktheMagazinenEduhur(artikulli, kodMag, idNderm, idPerd, merrMagMeAutorizim, magKoka["magArtKoka"].ToString(), artikulli.Magazina);

            if (magKoka.ContainsKey("magDestKoka"))
            {
                magazinat[1] = ktheMagazinenEduhur(artikulli, kodMagDest, idNderm, idPerd, merrMagMeAutorizim, magKoka["magDestKoka"].ToString(), String.Empty);
            }
            else
                magazinat[1] = new Dictionary<String, Object>();
            return magazinat;
        }

        public static Dictionary<String, Object> ktheMagazinenEduhur(clsArtikulli artikulli, string kodMag, int idNderm, int idPerd, bool merrMagMeAutorizim, string magKoka, string magArtikulli)
        {
            Dictionary<String, Object> magduhur = new Dictionary<String, Object>();
            clsNjesiAdministrative magazina = new clsNjesiAdministrative(kodMag, idNderm);
            DataTable magazinatDefault = colNjesiAdministrative.ktheTreNjesiteEParaAdministrativeAktiveMeLloj(idNderm, idPerd, merrMagMeAutorizim);
            if (magazinatDefault.Rows.Count == 0)
                return magduhur;
            DataRow magDefault = magazinatDefault.Rows[0];
            
            DataRow magTemp;
            object magArt = new object();
            int idMagazina = 0;

            if (((artikulli.LlojiArt && (magazina.IdLlojMagazine == 2 || magazina.IdLlojMagazine == 3)) || (!artikulli.LlojiArt && (magazina.IdLlojMagazine == 1 || magazina.IdLlojMagazine == 3))) && magazina.Kodi != magDefault["KODI"].ToString() && (magArtikulli == "" || magArtikulli == magazina.Kodi))
            {
                magArt = new { IdNjesiAdministrative = magazina.IdNjesiAdministrative, Kodi = magazina.Kodi, Pershkrimi = magazina.Pershkrimi, IdLlojMagazine = magazina.IdLlojMagazine };
                idMagazina = magazina.IdNjesiAdministrative;
            }
            else
            {
                magTemp = colNjesiAdministrative.merrMagazinePerTrupDokumentiSipasLlojit(idNderm, idPerd, merrMagMeAutorizim, artikulli.LlojiArt, magKoka, magArtikulli);
                if (magTemp != null)
                {
                    magArt = new { IdNjesiAdministrative = magTemp["IDNJESIADM"].ToString(), Kodi = magTemp["KODI"].ToString(), Pershkrimi = magTemp["PERSHKRIMI"].ToString(), IdLlojMagazine = magTemp["IDLLOJMAGAZINE"] };
                    idMagazina = int.Parse(magTemp["IDNJESIADM"].ToString());
                }
            }
            magduhur.Add("magazina", magArt);
            magduhur.Add("idMagArtKoka", idMagazina);
            return magduhur;
        }

        public static object[] ktheRowVleraArtMeIDSet(int idja, int rreshti)
        {

            object[] result = new object[2];
            result[0] = rreshti;
            clsArtikulli artikulli = new clsArtikulli(idja);

            if (artikulli.IdArtikulli > 0)
            {
                result[1] = artikulli;
                return result;
            }
            else
            {
                result[1] = null;
                return result;
            }
        }

        public static object[] ktheRowVleraArtRezMeKodOseKodBar(string kodi, int rreshti, DateTime data, int iddetajim, int idmag, int idNdermarrje, int idPerdoruesi)
        {
            data = data.ToLocalTime();
            double sasitot, sasiRez, sasiUB;
            object[] result = new object[5];
            result[0] = rreshti;

            clsArtikulli artikulli = new clsArtikulli();
            artikulli.ktheArtikullSipasKoditDheAutorizime(kodi, idNdermarrje, idPerdoruesi);
            sasiUB = clsTrupiRezervime.merrSasiNgaUB(artikulli);
            if (idmag != 0)
            {
                sasitot = clsTrupiMagazina.merrSasi(artikulli, idmag, data, iddetajim);
                sasiRez = clsTrupiRezervime.merrSasi(artikulli.IdArtikulli, 0, idmag, data); //todo gerta: kalo id
            }
            else
            {
                sasitot = clsTrupiMagazina.merrSasi(artikulli, -1, data, iddetajim);
                sasiRez = clsTrupiRezervime.merrSasiGjitheMag(artikulli, 0, data);
            }

            if (artikulli.IdArtikulli > 0)
            {
                result[1] = artikulli;
                result[2] = sasitot;
                result[3] = sasiRez;
                result[4] = sasiUB;
                return result;
            }
            else
            {
                result[1] = null;
                result[2] = 0;
                result[3] = 0;
                result[4] = 0;
                return result;
            }
        }

        public static Object ktheRowVleraArtMeKodOseKodBar(string kodi, int rreshti, DateTime data, string magazina, object sasiteNeGrideObj, object magazinatKoka, bool merrMagMeAutorizim, int idDetajim, bool meDetajim, string kodMagDest, int idNdermarrje, int idPerdorues, bool merrSipasDetajimit, int njesiDef, decimal sasiaNeRresht, bool merrCmim)
        {
            data = data.ToLocalTime();
            var artikulli = new clsArtikulli();
            var detajimiPare = new clsDetajimArtikulli();
            var detajimiDyte = new clsDetajimArtikulli();
            string kodbarsel = "";
            bool detajim = false;
            bool kodbari = false;
            double sasitot = 0;
            double sasiaTotDet2 = 0;
            double gjendja = 0;
            double vleftat = 0;
            int njesia = 0;
            int idMagazina = 0;
            object[] cmimi = new object[3];
            cmimi[0] = rreshti;
            artikulli.ktheArtikullSipasKoditDheAutorizime(kodi, idNdermarrje, idPerdorues);
            if (artikulli.IdArtikulli <= 0)
            {
                artikulli.merrSipasKodbarit(kodi, idNdermarrje);
                if (merrSipasDetajimit && artikulli.IdArtikulli <= 0)
                {
                    artikulli.ktheArtikullSipasDetajimit(kodi, idNdermarrje);
                    detajim = true;
                }
                else kodbari = true;
            }

            if (artikulli.IdArtikulli <= 0)
            {
                artikulli = null;
                detajimiPare = null;
                detajimiDyte = null;
                return new { idRreshti = rreshti, artikulli = artikulli, gjendjetot = 0, detajimi = detajimiPare, detajimi2 = detajimiDyte, gjendjetotdet2 = 0, kodbarsel = "", gjendja = gjendja, vleftat = vleftat, njesia = njesia, cmimi = cmimi };
            }
            artikulli.mbushKodBare();

            Dictionary<String, Object>[] magazinatTrupi = merrMagazinePerTrupDokumentiSipasLlojit(idNdermarrje, idPerdorues, merrMagMeAutorizim, magazinatKoka, artikulli, magazina, kodMagDest);

            double gjendjaTotale = DbCore.DbRegjistrim.clsTrupiMagazina.merrSasi(artikulli, -1, data, 0);
            if (magazinatTrupi != null)
                idMagazina = int.Parse((((Dictionary<String, Object>)magazinatTrupi[0])["idMagArtKoka"]).ToString());

            if (meDetajim)
            {
                Dictionary<String, Object>[] detajimDheSasi = new Dictionary<String, Object>[2];
                detajimDheSasi = clsArtikulli.ktheArtDetajimet(artikulli, idPerdorues, data, idMagazina, gjendjaTotale, sasiteNeGrideObj);
                sasitot = detajimDheSasi[0] == null ? gjendjaTotale : (double)((Dictionary<String, Object>)detajimDheSasi[0])["sasiTotDet"];
                detajimiPare = detajimDheSasi[0] == null ? null : (clsDetajimArtikulli)((Dictionary<String, Object>)detajimDheSasi[0])["detajim"];
                detajimiDyte = detajimDheSasi[1] == null ? null : (clsDetajimArtikulli)((Dictionary<String, Object>)detajimDheSasi[1])["detajim"];
                sasiaTotDet2 = detajimDheSasi[1] == null ? default(double) : (double)((Dictionary<String, Object>)detajimDheSasi[1])["sasiTotDet"];
            }
            else
            {
                sasitot = gjendjaTotale;
                if (idMagazina > 0)
                {
                    gjendja = clsTrupiMagazina.merrSasi(artikulli, idMagazina, data, idDetajim);
                    vleftat = clsTrupiMagazina.ktheVleftenTotaleSipasArtikullitHPD(artikulli.IdArtikulli, idMagazina, data, int.MaxValue);
                }
            }

            if (detajim)
            {
                clsDetajimArtikulli det = new clsDetajimArtikulli();
                det.mbushDetajimArtikulli(kodi, idNdermarrje);
                colDetajimeArtikulli colDet = new colDetajimeArtikulli();
                colDet.mbushDetajimeSipasArtikullitNdermarrjesDheLlojit(artikulli.KodArtikulli, artikulli.IdNdermarje, idPerdorues, 1);
                if (colDet.Find(x => x.IdDetajimArtikulli == det.IdDetajimArtikulli) != null)
                    detajimiPare = det;
                else detajimiDyte = det;
            }
            if (kodbari)
            {
                clsKodbari kodb = new clsKodbari();
                kodb.ktheKodbarSipasPershkrimit(kodi, artikulli.IdNdermarje);
                kodbarsel = kodi;
                njesia = kodb.Njesia;
                detajimiPare = kodb.Detajim1 > 0 ? new clsDetajimArtikulli(kodb.Detajim1) : detajimiPare;
                detajimiDyte = kodb.Detajim2 > 0 ? new clsDetajimArtikulli(kodb.Detajim2) : detajimiDyte;
            }
            else kodbarsel = "";

            if (merrCmim)
            {
                clsNivelCmimi nivBaze = new clsNivelCmimi();
                nivBaze.mbushNivelCmimiBaze(idNdermarrje, 1);
                if (nivBaze.IdNivelCmimi != 0)
                {
                    clsMonedha monBaze = new clsMonedha();
                    monBaze.mbushMonedhenENdermarrjes(idNdermarrje);

                    clsNjesiArtikulli njesiArt = njesiDef == 2 ? new clsNjesiArtikulli(artikulli.KodNjesia2, idNdermarrje) : new clsNjesiArtikulli(artikulli.KodNjesia1, idNdermarrje);
                    object[] cmimires = clsFunksione.merrCmimSipasNivelit(nivBaze.IdNivelCmimi, artikulli.KodArtikulli, idPerdorues, njesiArt, monBaze, Convert.ToString(data), 1, sasiaNeRresht, idNdermarrje, 1, new clsDatabaseInventari(), 0, true);
                    cmimi[1] = cmimires[0];
                    cmimi[2] = cmimires[1];
                }
            }
            return new { idRreshti = rreshti, artikulli = artikulli, gjendjetot = sasitot, detajimi = detajimiPare, detajimi2 = detajimiDyte, gjendjetotdet2 = sasiaTotDet2, kodbarsel = kodbarsel, gjendja = gjendja, vleftat = vleftat, njesia = njesia, magazinatTrupi = magazinatTrupi, cmimi = cmimi };
        }

        public static object[] ktheRowVleraArtMeKodSet(string kodi, int rreshti, int idNdermarrje, int idPerdorues)
        {

            object[] result = new object[2];
            result[0] = rreshti;
            clsArtikulli artikulli = new clsArtikulli();
            artikulli.ktheArtikullSipasKoditDheAutorizime(kodi, idNdermarrje, idPerdorues);

            if (artikulli.IdArtikulli > 0)
            {
                result[1] = artikulli;
            }

            else result[1] = null;
            return result;
        }
        public static string ktheTargeTransportuesi(int idtransportues)
        {
            clsTransportues tran = new clsTransportues();
            tran.mbushTransportues(idtransportues);
            if (tran.IdTransportues <= 0)
                return null;
            else
            {
                string targa = (clsTransportues.ktheTargeTransportuesSipasId(idtransportues)).ToString();
                return targa;
            }
        }
        public static object[] merrArtikujSet(string kodi, int rreshti, DateTime data,decimal sasia, object magazinatKoka, bool merrMagMeAutorizim, int idNdermarrje, int idPerdoruesi, int idArtikulli)
        {
            data = data.ToLocalTime();
            object[] result = new object[4];
            result[0] = rreshti;
            clsArtikulli artikulli = new clsArtikulli();
            artikulli.ktheArtikullSipasKoditDheAutorizime(kodi, idNdermarrje, idPerdoruesi);
            int i = 0;
            Dictionary<int, Object> dic = new Dictionary<int, Object>();
            Dictionary<int, decimal> koeficientet = new Dictionary<int, decimal>();
            colArtikulliPerberes per = new colArtikulliPerberes();
            if (artikulli.IdArtikulli > 0)
            {
                per.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(artikulli.IdArtikulli, data);
                if(idArtikulli != 0)
                {
                    var art = per.Find(x => x.IdLidheseArt == idArtikulli);
                    if (art != null && art.Koeficienti != 0)
                        sasia = sasia / art.Koeficienti;
                }
                foreach (clsArtikulliPerberes perber in per)
                {
                    koeficientet.Add(i, sasia * perber.Koeficienti);
                    var tedhenaart = ktheRowVleraArtMeID(perber.IdLidheseArt, rreshti + i, data, "", null, magazinatKoka, merrMagMeAutorizim, false, 0, String.Empty, idNdermarrje, idPerdoruesi, 1, sasia, false);
                    dic.Add(i, tedhenaart);
                    i++;
                }
                result[1] = dic;
                result[2] = koeficientet;
            }

            else result[1] = dic;
            result[2] = koeficientet;
            result[3] = per.Select(art => new { IdSeti = art.IdArtikulliKryesor, KodSeti = artikulli.KodArtikulli, IdArtikulli = art.IdLidheseArt, Koeficenti = art.Koeficienti, SasiaSet = sasia });
            return result;
        }

        public static int[] ktheRoletSipasPerdoruesit(int idPerdoruesi)
        {
            colRolPerdorues rolPer = new colRolPerdorues();
            rolPer.mbushRolePerdoruesSipasPerdoruesi(idPerdoruesi);
            int[] rolet = new int[rolPer.Count];
            for (int i = 0; i < rolPer.Count; i++)
            {
                rolet[i] = rolPer[i].IdRoli;
            }
            return rolet;
        }

        public static bool kontrolloLimitSasiKarte(int idartikulli, int idKarte, DateTime dtDok, double totali)
        {
            double limit = 0;
            limit = clsLimitKarta.MerrLimitSasiMbetur(idartikulli, idKarte, dtDok.ToLocalTime());
            if (limit == -99999) return true;
            if (totali > limit) return false;
            else return true;

        }

        public static bool kontrolloLimitVlereKarte(int idartikulli, int idKarte, DateTime dtDok, double totalivlere)
        {
            double limit = 0;
            limit = clsLimitKarta.MerrLimitVlereMbetur(idartikulli, idKarte, dtDok.ToLocalTime());
            if (limit == -99999) return true;
            if (totalivlere > limit) return false;
            else return true;

        }
        public static DbCore.DbKontabiliteti.colTrupPasqyreFinaciare merrKonfigurimPasqyre(int id)
        {
            var col = new DbCore.DbKontabiliteti.colTrupPasqyreFinaciare(id);
            foreach (var t in col)
            {
                t.OColLlogarite = new colLlogariaTrupiPasqyres(t.IdTrupi);
                colBuxhetet colBuxh = new colBuxhetet(t.IdTrupi, "PasqyreFinanciare");
                colBuxh.shtoBuxhetNeIndeksin(0, colBuxh.KtheBuxhetinTotal());
                t.OColBuxhetet = new colBuxhetet();
                t.OColBuxhetet.AddRange(colBuxh);
            }
            return col;
        }
        public static colKarta ktheKartaKlientiDT(int idKlient, int idNdermarrje)
        {
            return new colKarta(idNdermarrje, idKlient);
        }

        public static colKarta ktheKartaKlientiAll(int idNdermarrje)
        {
            return new colKarta(idNdermarrje);
        }

        public static object ktheKategoriZbritjeKarteKlienti(int idKatZbritje, DateTime date, decimal vlefte, decimal kursi, int idmonedha)
        {
            clsKokaKategoriZbritje koka = new clsKokaKategoriZbritje(idKatZbritje);
            decimal kurskategorie = 0;
            if (koka.IdMonedha != idmonedha)
            {
                clsKurset kurs = new clsKurset(koka.IdMonedha, date);
                if (kurs.VleraKursi == 0)
                    kurs.VleraKursi = 1;
                kurskategorie = Convert.ToDecimal((double)kurs.VleraKursi);
            }
            else kurskategorie = kursi;
            colTrupatKategoriteZbritjes trupat = new colTrupatKategoriteZbritjes();
            trupat.mbushTrupatKategoriZbritjeSipasKokes(idKatZbritje);
            for (int i = trupat.Count - 1; i >= 0; i--)
            {
                if (trupat[i].DateFillimi <= date && trupat[i].DateMbarimi >= date)
                    if (trupat[i].VleraMax != 0)
                        if (trupat[i].VleraMax * kurskategorie / kursi >= vlefte && trupat[i].VleraMin * kurskategorie / kursi <= vlefte)
                            if (trupat[i].Lloji == 1)
                                return new { zbritje = trupat[i].Zbritja, eshtePerqindje = true };
                            else
                                return new { zbritje = trupat[i].Zbritja * kurskategorie / kursi, eshtePerqindje = false };
                        else
                            continue;
                    else
                    {
                        if (trupat[i].VleraMin != 0)
                        {
                            if (trupat[i].VleraMin * kurskategorie / kursi <= vlefte)
                                if (trupat[i].Lloji == 1)
                                    return new { zbritje = trupat[i].Zbritja, eshtePerqindje = true };
                                else
                                    return new { zbritje = trupat[i].Zbritja * kurskategorie / kursi, eshtePerqindje = false };
                        }
                        else
                            if (trupat[i].VleraMax == 0 && trupat[i].VleraMin == 0)
                            if (trupat[i].Lloji == 1)
                                return new { zbritje = trupat[i].Zbritja, eshtePerqindje = true };
                            else
                                return new { zbritje = trupat[i].Zbritja * kurskategorie / kursi, eshtePerqindje = false };
                    }
            }
            return null;
        }
        public static clsPolitikeKarta kthePolitikeKarte(int idPolitike)
        {
            clsPolitikeKarta pol = new clsPolitikeKarta(idPolitike);
            return pol;
        }
        public static clsKarta ktheKarteKlienti(int idKarte)
        {
            var karta = new clsKarta(idKarte);
            karta.OColKlientFurnitor = colKlienteFurnitore.MerrKlientFurnitorSipasIdKarte(karta.IdKarta);

            return karta;
        }

        public static colInfoTrupi ruajNeSessionTrupInfoArt(int idkoka, HttpSessionState Session)
        {
            // DbCore.DbAdmin.colInfoTrupi trupatvis = DbCore.DbAdmin.colInfoTrupi.merrInfoSipasIdKokaDheVisible(idkoka, true, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //  DbCore.DbAdmin.colInfoTrupi trupatinvis = DbCore.DbAdmin.colInfoTrupi.merrInfoSipasIdKokaDheVisible(idkoka, false, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            colInfoTrupi trupatvis = colInfoTrupi.merrInfoSipasIdKokaDheVisibleNew(idkoka, true, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            colInfoTrupi trupatinvis = colInfoTrupi.merrInfoSipasIdKokaDheVisibleNew(idkoka, false, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            mySessionObjects.ruajInfoVisibleNeSession(Session, trupatvis);
            mySessionObjects.ruajInfoInVisibleNeSession(Session, trupatinvis);
            return trupatvis;
        }

        public static string ktheKlientFurnitor(string emri, DateTime data, HttpSessionState Session)
        {
            data = data.ToLocalTime();
            if (emri == null)
                emri = "0";
            string klienti = "";
            clsKlientFurnitor oClsKF = new clsKlientFurnitor();
            clsMesazh mesazhi = new clsMesazh();
            try
            {
                mesazhi = oClsKF.mbushKlientFurnitorSipasKodit(emri, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            }
            catch (Exception err)
            {
                ImbLogger.Error(err.Message);
                return klienti;
            }
            //oColKF = dbKontab.ktheKlientFurnitor(int.Parse(prefixText));
            if (mesazhi.Status)
            {
                decimal perqindje = 0;
                bool meTVSH = false;
                colTrupatKategoriteZbritjes trupat = new colTrupatKategoriteZbritjes();
                trupat.mbushTrupatKategoriZbritjeSipasKokes(oClsKF.IdKatZbritje);
                //DbCore.DbInventari.colTrupatKategoriteZbritjes trupat=dbInventari .merrTrupatKategoriZbritjeSipasKokes (oColKF[0].IdKatZbritje );
                if (trupat.Count > 0)
                    perqindje = trupat[trupat.Count - 1].Zbritja;
                clsMonedha mon = new clsMonedha();
                if (oClsKF.IdNivelCmimi != 0)
                {
                    clsNivelCmimi niv = new clsNivelCmimi(oClsKF.IdNivelCmimi);
                    if (niv.BrutoNetoNivelCmimi == 1)
                        meTVSH = true;
                }
                mon.mbushMonedhePershk(oClsKF.Monedha, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                decimal detyrimi = clsKlientFurnitor.MerrDetyrimKf(oClsKF.IdKlientFurnitor, data);
                klienti += oClsKF.EmertimiKF + ";" + oClsKF.MenyraTransportit + ";" + oClsKF.KushteDergimi + ";" + oClsKF.IdPerfaqesuesShitje + ";" + oClsKF.IdKushtePagese + ";" + oClsKF.ZbritjeTotal + ";" + oClsKF.MaturimiKF + ";" + oClsKF.LimitParalajmerues + ";" + oClsKF.LimitBllokues + ";" + oClsKF.ZbritjeAnalitike + ";" + oClsKF.IdNivelCmimi + ";" + oClsKF.KodKlientFurnitor + ";" + perqindje + ";" + mon.IdMonedha + ";" + meTVSH + ";" + detyrimi;

            }
            return klienti;
        }

        public static colNjesiVartese ktheTemplatetNjesiVartese(HttpSessionState Session)
        {
            colNjesiVartese col = new colNjesiVartese();
            col.mbushGjitheNjesiVartese(mySessionObjects.merrIdNdermarrjeSesioni(Session));
            return col;
        }

        public static int kthePikeKarte(int idKarte, int idNdermarrje)
        {
            int pike = 0;
            pike = clsKarta.KthePikeKarte(idKarte, idNdermarrje);
            if (pike == -999) return 0;  //kur pike==-999 nuk ka veprime fare me ate karte
            else return pike;


        }

        public static object kthePikeDheLidhjeKarte(int idKarte, int idNdermarrje)
        {
            int pike = kthePikeKarte(idKarte, idNdermarrje);
            int idNivel = clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi("KK", idNdermarrje);
            bool iLidhur = clsKarta.EshteLidhurKarta(idKarte, idNivel);
            return new { pike = pike, iLidhur = iLidhur };
        }

        public static colTrupiPolitikeKarta ktheTrupiPolitikeKarte(int idPolitike)
        {
            return new colTrupiPolitikeKarta(idPolitike);
        }

        public static object[] ktheKonfigurimFormatNumriMerriTeGjithe(int idKonfigurim, int idNdermarrje, int idPerdoruesi)
        {
            DbCore.DbShare.clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(idKonfigurim);
            if (formatNrPerKonfig.KonfigTrupi.Count == 0)
            {
                DbCore.DbShare.clsFormatKonfigTrup trup = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                formatNrPerKonfig.KonfigTrupi.Add(trup);
            }
            DbCore.DbAdmin.colMonedhat mon = new DbCore.DbAdmin.colMonedhat();
            mon.mbushGjitheMonedhat(idNdermarrje, idPerdoruesi);
            object[] formatet = new object[2];
            formatet[0] = formatNrPerKonfig;
            formatet[1] = mon;
            return formatet;
        }



        public static colNjesiAdministrative ktheArrayMagazinatClientSideTeSerializuar(int idNdermarrje, int idPerdoruesi, bool merrMeAutorizim = true)
        {
            colNjesiAdministrative colMagazinat = new colNjesiAdministrative();
            colMagazinat.mbushGjitheNjesiAdministrativeAktive(idNdermarrje, idPerdoruesi, merrMeAutorizim);
            return colMagazinat;
        }

        public static Object ktheEkzistenceDhePershkrimArtikulliNgaKodbari(int idNdermarrje, string kodbar)
        {
            if (clsArtikulli.ekzistonKodbar(kodbar, idNdermarrje))
                return new { ekziston = true, pershkrimi = clsArtikulli.merrPershkrimArtikulliNgaKodbariNqsEkzistonKodbari(kodbar, idNdermarrje) };
            return new { ekziston = false, pershkrimi = "" };
        }
        public static String merrMesazhNgaSesioni(HttpSessionState Session)
        {
            return mySessionObjects.merrMesazhNgaSesioni(Session);
        }
        public static int ktheIndexSelectedFilterPeriudhaKusht(int idKonfigurimi)
        {
            string alt = clsAlternativaKushti.getAlternativa(idKonfigurimi, "SHDPER");
            return Convert.ToInt32(Enum.Parse(typeof(ListPeriudha), alt.Replace(' ', '_')));
        }



        public static string KthePathinEThemit(string input)
        {
            DbCore.DbAdmin.clsTheme MyTheme = new DbCore.DbAdmin.clsTheme();
            MyTheme.EmriTheme = input;
            MyTheme = MyTheme.merrSipasEmri();

            return MyTheme.PathTheme + ":" + MyTheme.IdTheme;
        }

        internal static object ktheImazheArkive(int idEntitet, int idKategoria)
        {
            colArkiva myArkiva = new colArkiva();
            myArkiva.mbushImazheNgaArkiva(idEntitet, idKategoria);
            clsMesazh mesazh = myArkiva.Count > 0 ? new clsMesazh(true, "lista e imazheve u kthye me sukses") : new clsMesazh(false, "Nuk ka imazhe per kete entitet");
            return new { statusPergjigje = mesazh.Status, mesazh = mesazh.PershkrimMesazhi, listaUrl = myArkiva };
        }

        public static string[] merrPershkrimQendra(string kodi, string key, HttpSessionState Session)
        {
            string[] result = new string[2];
            result[0] = key;

            DbCore.DbQendraKosto.clsQendraKosto qendra = new DbCore.DbQendraKosto.clsQendraKosto(kodi, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            result[1] = qendra.Pershkrimi;

            return result;
        }
        
        internal static object merrKursFatureOseAzhornimiPerIdDok(string ids)
        {
            DataTable dt = colDokumentat.merrKursFatureOseAzhornimiPerIdDok(ids);
            return new { dt };
        }

        public static object[] merrPershkrimObjektiva(string kodi, string key, HttpSessionState Session)
        {
            object[] result = new object[2];
            result[0] = key;

            DbCore.DbQendraKosto.clsObjektivaKosto skema = new DbCore.DbQendraKosto.clsObjektivaKosto(kodi, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            result[1] = skema;

            return result;
        }

        public static object[] KontrolloKonvertuarRezervime(int[] ids, string pageId, HttpSessionState Session)
        {
            int idniveli = 0;
            string mesazh = "";
            for (int i = 0; i < ids.Length; i++)
            {
                clsKokaRezervime koka = new clsKokaRezervime();
                koka.mbushKokaRezervimiSipasID(ids[i]);
                idniveli = koka.IdNivel;
                string ngjyra = clsKokaRezervime.merrNgjyreKonvertimeRezervime(koka.IdNdermarrje, koka.IdKokaRezervimi);
                if (koka.IdStatusDok == 0)
                    mesazh += "Nuk mund te konvertohen dokumenta me status draft!";
                if (ngjyra == "kuqe" || ngjyra == "gjelber")
                    mesazh += "Dokumenti Nr." + koka.NrDok + " Dt." + koka.DtDok.ToShortDateString() + " eshte konvertuar plotesisht dhe nuk mund te konvertohet perseri!";
                //to do kontrollo konvertuar
            }
            colNivelRegjistrimi niv = new colNivelRegjistrimi();
            colKonfigurimAmbjenti colKonfig = new colKonfigurimAmbjenti();
            niv.mbushKonvertimeNiveli(idniveli, mySessionObjects.ktheIdPerdoruesi(Session));
            foreach (clsNivelRegjistrimi regj in niv)
            {
                clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
                colKonfig.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(regj.IdKategori, regj.IdNivel, mySessionObjects.ktheIdPerdoruesi(Session));

            }
            colKonfigurimAmbjenti konfVarura = new colKonfigurimAmbjenti();
            foreach (clsKonfigurimAmbjenti konfi in colKonfig)
            {
                var alternativa = clsAlternativaKushti.getAlternativa(konfi.IdKonfigAmbjente, "V");
                if (alternativa == "Po")
                    konfVarura.Add(konfi);
            }
            foreach (clsKonfigurimAmbjenti konfi in konfVarura)
            {
                colKonfig.Remove(konfi);
            }
            if (mesazh == "")
                mesazh = "Nuk jane konvertuar";
            object[] result = new object[4];
            result[0] = niv;
            result[1] = mesazh;
            result[2] = ids;
            result[3] = colKonfig;

            var myPageCache = GlobalCacheManager.GetPageCacheByPageID(pageId);
            myPageCache["idkonvertimi"] = ids;
            return result;
        }

        public static string[] KontrolloEkzistonIMEI(string detajim, int idartikulli, string key, DateTime data, int idmag, HttpSessionState Session)
        {
            string[] resutl = new string[2];
            resutl[0] = key;
            data = data.ToLocalTime();
            clsDetajimArtikulli det = new clsDetajimArtikulli();
            det.mbushDetajimArtikulli(detajim, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (det.IdDetajimArtikulli < 1)
            {
                resutl[1] = "Nuk ekziston";
                return resutl;

            }
            clsArtikulli art = new clsArtikulli(idartikulli);
            if (!clsDetajimArtikulli.ekzistonDetajimLidhurMeArtikullin(detajim, mySessionObjects.merrIdNdermarrjeSesioni(Session), art.KodArtikulli, 1))
            {
                resutl[1] = "Jo Artikulli";
                return resutl;

            }
            if (det.Loan != 1)
            {
                resutl[1] = "Jo Loan";
                return resutl;

            }
            double sasi = clsTrupiMagazina.merrSasi(art, idmag, data, det.IdDetajimArtikulli);
            if (sasi < 1)
            {
                resutl[1] = "Jo gjendje";
                return resutl;

            }
            resutl[1] = "";
            return resutl;
        }
        public static bool KontrolloKaAutorizimStatusi(int idstatusi, HttpSessionState Session)
        {
            clsStatusRiparimi status = new clsStatusRiparimi(idstatusi, mySessionObjects.ktheIdPerdoruesi(Session));
            if (status.Id > 0)
                return true;
            else return false;
        }
        public static object[] KontrolloKaGjendjeSwap(string detajim, DateTime data, int idmag, HttpSessionState Session)
        {
            object[] result = new object[2];
            data = data.ToLocalTime();
            clsDetajimArtikulli det = new clsDetajimArtikulli();
            det.mbushDetajimArtikulli(detajim, mySessionObjects.merrIdNdermarrjeSesioni(Session));

            if (det.IdDetajimArtikulli < 1)
            {
                result[0] = "Nuk ekziston";
                result[1] = "";
                return result;
            }
            clsArtikulli art = new clsArtikulli();
            art.ktheArtikullSipasDetajimit(det.KodDetajimArtikulli, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (art.IdArtikulli < 1)
            {
                result[0] = "Jo Artikulli";
                result[1] = "";
                return result;
            }
            result[1] = art.KodArtikulli;
            if (det.Loan == 1)
            {
                result[0] = "Loan";
                return result;
            }
            double sasi = clsTrupiMagazina.merrSasi(art, idmag, data, det.IdDetajimArtikulli);
            if (sasi < 1)
            {
                result[0] = "Jo gjendje";
                return result;
            }
            result[0] = "";
            return result;
        }

        internal static object KaGaranciShitja(int idShitjeKoka)
        {
            colGaranciArtikulli garancia = new colGaranciArtikulli(idShitjeKoka);
            return new { KaGaranci = garancia.Count > 0 };
        }

        public static object[] ktheRowGjendjeSerialPerMagazine(string serial, int rreshti, DateTime data, int idndermarja, bool rezerva)
        {
            data = data.ToLocalTime();
            //System.Threading.Thread.Sleep(5000);
            object[] result = new object[2];
            result[0] = rreshti;
            clsAQTSeriale seriali = new clsAQTSeriale();
            seriali.merrAQTSerialSipasKodAQT(serial, idndermarja);
            if (seriali.IdAQTSerial > 0)
            {
                double gjendjatot = 0;
                if (!rezerva)
                    gjendjatot = clsSerialetMagazine.ktheSerialetMagazineSipasIDSerialDheDates(seriali.IdAQTSerial, idndermarja, data, 0);
                else gjendjatot = clsAmortizimiTrupiRezerva.ktheSerialetMagazineRezervaSipasIDSerialDheDates(seriali.IdAQTSerial, idndermarja, data);
                result[1] = gjendjatot;
                return result;
            }
            else
            {
                result[1] = 0;
                return result;
            }
        }
        public static object[] ktheRowGjendjeArtikulliPerMagazine(int idja, int rreshti, DateTime data, string magazina, string serial, int idndermarja, bool rezerva)
        {
            data = data.ToLocalTime();
            //System.Threading.Thread.Sleep(5000);
            object[] result = new object[2];
            result[0] = rreshti;
            // DbInventari.clsArtikulli artikulli = new DbInventari.clsArtikulli(idja);
            clsNjesiAdministrative mag = new clsNjesiAdministrative(magazina, idndermarja);
            if (idja > 0)
            {
                clsAQTSeriale seriali = new clsAQTSeriale();
                seriali.merrAQTSerialSipasKodAQT(serial, idndermarja);
                if (seriali.IdAQTSerial > 0)
                {
                    double gjendjatot = 0;
                    if (!rezerva) gjendjatot = clsSerialetMagazine.ktheSerialetMagazineSipasIDSerialDheDates(seriali.IdAQTSerial, idndermarja, data, 0);
                    else gjendjatot = clsAmortizimiTrupiRezerva.ktheSerialetMagazineRezervaSipasIDSerialDheDates(seriali.IdAQTSerial, idndermarja, data);
                    result[1] = gjendjatot;
                    return result;
                }
                else
                {
                    double gjendjatotart = 0;
                    if (!rezerva) gjendjatotart = clsSerialetMagazine.ktheSerialetMagazineGjendjeTotaleArtikulliNeMagazine(idja, idndermarja, mag.IdNjesiAdministrative, data);
                    else gjendjatotart = clsAmortizimiTrupiRezerva.ktheGjendjeRezervaMagazine(idja, idndermarja, mag.IdNjesiAdministrative, data);
                    result[1] = gjendjatotart;
                    return result;
                }
            }
            else
            {
                result[1] = 0;
                return result;
            }
        }
        public static colKarakteristikaStandartiTrupi merrTrupKarakteristikaAmortizimi(int id)
        {
            colKarakteristikaStandartiTrupi col = new colKarakteristikaStandartiTrupi();
            col.merrTrupinSipasKokes(id);
            return col;
        }
        public static string ktheEmerLlogarie(string prefixText, HttpSessionState Session)
        {
            clsLlogari llog = new clsLlogari(prefixText, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            return llog.EmerLlogari1;
            ////List<string> items = new List<string>();
            ////colLlogarite oColLlogari = new DbCore.colLlogarite();
            ////oColLlogari = oColLlogari.merrLLogariteNdermarrjesAndAutorizimeLike(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session),
            ////    prefixText);
            ////if (oColLlogari.Count > 0)
            ////{
            ////    return oColLlogari[0].EmerLlogari1;
            ////}
            ////else
            ////{
            ////    return "";
            ////}
        }
        public static object[] ktheFaturatFiltruaraShpezobj(int id)
        {

            object[] vlerat = new object[5];
            clsKokaMagazina koka = new clsKokaMagazina();
            koka.mbushKokaMagazinaSipasID(id);
            koka.mbushTrupMagazine(false);
            vlerat[0] = koka;
            colArtikujt colart = new colArtikujt();
            colNjesiAdministrative colmag = new colNjesiAdministrative();
            colNjesiteArtikulli colnjesi = new colNjesiteArtikulli();
            for (int j = 0; j < koka.OcolTrupiMagazina.Count; j++)
            {
                clsArtikulli art = new clsArtikulli(koka.OcolTrupiMagazina[j].IdArtikulli);
                colart.Add(art);
                clsNjesiAdministrative mag = new clsNjesiAdministrative(koka.OcolTrupiMagazina[j].IdMag);
                colmag.Add(mag);
                clsNjesiArtikulli njesia = new clsNjesiArtikulli(koka.OcolTrupiMagazina[j].IdNjesia);
                colnjesi.Add(njesia);
            }
            clsKlientFurnitor kf = new clsKlientFurnitor(koka.IdKlientFurnitor);

            vlerat[1] = colart;
            vlerat[2] = colmag;
            vlerat[3] = colnjesi;
            vlerat[4] = kf;

            return vlerat;
        }

        internal static object VeprimeArkaBanka_DergoFatureMeEmail(string idsKokaDok, HttpSessionState session)
        {
            (clsMesazh mesazh, clsMesazh info) = ArketimeMailHelper.DergoFatureMeEmail(idsKokaDok, mySessionObjects.ktheGjuhe(session), mySessionObjects.ktheIdPerdoruesi(session), mySessionObjects.ktheNdermarrjeVit(session), mySessionObjects.merrIdNdermarrjeSesioni(session));
            return new { mesazhi = mesazh, informimi = info };
        }

        public static clsMesazh ktheMsgFatureShperndare(int idKokaMagazina)
        {
            try
            {
                clsShperndarjeShpenzimeTrupi trup = new clsShperndarjeShpenzimeTrupi();
                bool eShperndare = trup.eshteZShperndareFatura(idKokaMagazina);
                if (eShperndare)
                {
                    clsKokaMagazina koka = new clsKokaMagazina();
                    koka.mbushKokaMagazinaSipasID(idKokaMagazina);
                    if (koka.IdKokaMagazina == 0)
                        throw new Exception();
                    return new clsMesazh(true, "Fatura " + koka.NrDok + " eshte e shperndare!");
                }
            }
            catch (Exception err)
            {
                string mesazhi = "Gabim gjate kontrollit te fatures!";
                ImbLogger.Error(mesazhi + err.Message);
                return new clsMesazh(false, mesazhi);
            }
            return new clsMesazh(true, "");
        }
        public static object[] KtheVleraBurimeMeIDRow(int idja, int rreshti)
        {
            object[] result = new object[3];
            result[0] = rreshti;
            double kosto = 0;
            clsBurime llogaria = new clsBurime(idja);
            if (llogaria.IdBurimi > 0)
            {
                result[1] = llogaria;


                result[2] = llogaria.KostoPlan;
                return result;
            }
            else
            {
                result[1] = null;
                result[2] = kosto;
                return result;
            }
        }
        public static object[] ktheVleraBurimeMeKodRow(string kodi, int rreshti, HttpSessionState Session)
        {
            object[] result = new object[3];
            result[0] = rreshti;
            double kosto = 0;
            clsBurime llogaria = new clsBurime(kodi, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (llogaria.IdBurimi > 0)
            {
                result[1] = llogaria;

                result[2] = llogaria.KostoPlan;
                return result;
            }
            else
            {
                result[1] = null;
                result[2] = kosto;
                return result;
            }
        }
        public static object[] KtheVleraAktiviteteMeIDRow(int idja, int rreshti)
        {
            object[] result = new object[3];
            result[0] = rreshti;
            double kosto = 0;
            clsAktiviteteKoka aktiviteti = new clsAktiviteteKoka(idja);
            if (aktiviteti.IdKoka > 0)
            {
                result[1] = aktiviteti;
                colAktiviteteTrupi trupat = new colAktiviteteTrupi();
                trupat.mbushAktiviteteTrupiSipasIdKoka(aktiviteti.IdKoka);
                foreach (clsAktiviteteTrupi trup in trupat)
                {
                    kosto += double.Parse(trup.Kosto.ToString());
                }
                result[2] = kosto;
                return result;
            }
            else
            {
                result[1] = null;
                result[2] = kosto;
                return result;
            }
        }
        public static object[] ktheVleraAktiviteteMeKodRow(string kodi, int rreshti, HttpSessionState Session)
        {
            object[] result = new object[3];
            result[0] = rreshti;
            double kosto = 0;
            clsAktiviteteKoka llogaria = new clsAktiviteteKoka(kodi, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (llogaria.IdKoka > 0)
            {
                result[1] = llogaria;
                colAktiviteteTrupi trupat = new colAktiviteteTrupi();
                trupat.mbushAktiviteteTrupiSipasIdKoka(llogaria.IdKoka);
                foreach (clsAktiviteteTrupi trup in trupat)
                {
                    kosto += double.Parse(trup.Kosto.ToString());
                }
                result[2] = kosto;
                return result;
            }
            else
            {
                result[1] = null;
                result[2] = kosto;
                return result;
            }
        }

        public static object[] ktheRowVleraArtMeIDPerRec(int idja, int rreshti, DateTime data, int idmag, HttpSessionState Session)
        {
            data = data.ToLocalTime();
            int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
            //System.Threading.Thread.Sleep(5000);
            object[] result = new object[5];
            result[0] = rreshti;
            clsArtikulli artikulli = new clsArtikulli(idja);
            clsTrupiMagazina tr = new clsTrupiMagazina();
            if (idmag == 0)
                result[2] = tr.llogaritCmimMesatar(artikulli, 0, data, -1, 1, idPerdorues);
            else
            {
                if (artikulli.IdMagazina > 0)
                    idmag = artikulli.IdMagazina;
                result[2] = tr.llogaritCmimMesatar(artikulli, idmag, data, -1, 1, idPerdorues);
            }
            if (artikulli.IdArtikulli > 0)
            {
                result[1] = artikulli;
                Dictionary<String, Object>[] detajimDheSasi = new Dictionary<String, Object>[2];
                //per tu pare
                detajimDheSasi = clsArtikulli.ktheArtDetajimet(artikulli, idPerdorues, data, idmag, 0);

                result[3] = detajimDheSasi[0] == null ? null : (clsDetajimArtikulli)((Dictionary<String, Object>)detajimDheSasi[0])["detajim"];
                result[4] = detajimDheSasi[1] == null ? null : (clsDetajimArtikulli)((Dictionary<String, Object>)detajimDheSasi[1])["detajim"];
                return result;
            }
            else
            {
                result[1] = null;
                return result;
            }
        }
        public static object[] ktheRowVleraArtMeKodRec(string kodi, int rreshti, DateTime data, string kodmag, HttpSessionState Session)
        {
            data = data.ToLocalTime();
            object[] result = new object[5];
            result[0] = rreshti;
            int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
            clsArtikulli artikulli = new clsArtikulli();
            artikulli.ktheArtikullSipasKoditDheAutorizime(kodi, mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session));
            clsTrupiMagazina tr = new clsTrupiMagazina();
            int idmag = 0;
            if (kodmag == "")
                result[2] = tr.llogaritCmimMesatar(artikulli, 0, data, -1, 1, idPerdorues);
            else
            {


                clsNjesiAdministrative mag = new clsNjesiAdministrative(kodmag, mySessionObjects.merrIdNdermarrjeSesioni(Session), idPerdorues);
                idmag = mag.IdNjesiAdministrative;
                if (artikulli.IdMagazina > 0)
                    idmag = artikulli.IdMagazina;
                result[2] = tr.llogaritCmimMesatar(artikulli, idmag, data, -1, 1, idPerdorues);
            }
            if (artikulli.IdArtikulli > 0)
            {
                result[1] = artikulli;
                Dictionary<String, Object>[] detajimDheSasi = new Dictionary<String, Object>[2];
                //per tu pare
                detajimDheSasi = clsArtikulli.ktheArtDetajimet(artikulli, idPerdorues, data, idmag, 0);

                result[3] = detajimDheSasi[0] == null ? null : (clsDetajimArtikulli)((Dictionary<String, Object>)detajimDheSasi[0])["detajim"];
                result[4] = detajimDheSasi[1] == null ? null : (clsDetajimArtikulli)((Dictionary<String, Object>)detajimDheSasi[1])["detajim"];


                return result;
            }
            artikulli.merrSipasKodbarit(kodi, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (artikulli.IdArtikulli > 0)
            {
                result[1] = artikulli;
                Dictionary<String, Object>[] detajimDheSasi = new Dictionary<String, Object>[2];
                //per tu pare
                detajimDheSasi = clsArtikulli.ktheArtDetajimet(artikulli, mySessionObjects.ktheIdPerdoruesi(Session), data, idmag, 0);

                result[3] = detajimDheSasi[0] == null ? null : (clsDetajimArtikulli)((Dictionary<String, Object>)detajimDheSasi[0])["detajim"];
                result[4] = detajimDheSasi[1] == null ? null : (clsDetajimArtikulli)((Dictionary<String, Object>)detajimDheSasi[1])["detajim"];


                return result;
            }

            result[1] = null;
            return result;
        }
        public static AutoCompleteItem[] ktheBurime(string prefixText, HttpSessionState Session)
        {
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session); ; //nga sesioni   
            DataTable tmpTable;
            tmpTable = colBurimet.ktheGjitheBurimetSipasNdermarjesLikeDt(mySessionObjects.merrIdNdermarrjeSesioni(Session), prefixText);
            tmpTable.Columns["des"].ColumnName = "desc";
            AutoCompleteItem[] autoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                autoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                autoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                autoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
            }
            return autoCompleteItem;

        }

        public static AutoCompleteItem[] ktheAktiviteteSipasBurimit(string prefixText, int idburimi, HttpSessionState Session)
        {
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session); ; //nga sesioni   
            DataTable tmpTable;
            tmpTable = colAktiviteteKoka.ktheGjitheAktivitetetSipasNdermarjesDheBurimiLikeDt(mySessionObjects.merrIdNdermarrjeSesioni(Session), prefixText, idburimi);
            tmpTable.Columns["des"].ColumnName = "desc";
            AutoCompleteItem[] AutoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                AutoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                AutoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                AutoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
            }
            return AutoCompleteItem;
        }
        public static object[] ktheArtikullPlanifikimi(int idja, int rreshti, HttpSessionState Session)
        {
            object[] result = new object[2];
            result[0] = rreshti;
            colTrupiPlanifikim trupiplan = new colTrupiPlanifikim(idja, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (trupiplan.Count == 1)
            {
                clsArtikulli artikulli = new clsArtikulli(trupiplan[0].IdArtikulli);

                if (artikulli.IdArtikulli > 0)
                {
                    result[1] = artikulli;
                    return result;
                }
                else
                {
                    result[1] = null;
                    return result;
                }
            }
            else
            {
                result[1] = null;
                return result;
            }
        }
        public static AutoCompleteItem[] ktheArtikullPerProdhimPlanifikim(string prefixText, int idplanifikimi, HttpSessionState Session)
        {
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session); ; //nga sesioni   
            DataTable tmpTable;
            tmpTable = colArtikujt.merrArtikujLikeKodPershkKodbarDTArtProdhimPlanifikim(mySessionObjects.merrIdNdermarrjeSesioni(Session), idPerdoruesi, prefixText, 1, idplanifikimi);

            tmpTable.Columns["des"].ColumnName = "desc";
            AutoCompleteItem[] AutoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                AutoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                AutoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                AutoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
            }
            return AutoCompleteItem;
        }
        public static object[] ktheEmailPerdoruesi(string prefixText, string[] perdorues, string[] lloji, int key, HttpSessionState Session)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            CultureInfo ci = DbCore.mySessionObjects.ktheCultureInfo(Session);
            object[] result = new object[3];
            DbCore.DbAdmin.clsPerdorues per = new DbCore.DbAdmin.clsPerdorues();

            DbCore.DbAdmin.colPerdoruesit col = DbCore.DbAdmin.clsPerdorues.merrUserNgaLogin(prefixText);
            if (col.Count != 0)
                result[0] = col[0];
            else result[0] = null;

            result[1] = kontrolloEksistonPerdorues(prefixText, perdorues, lloji, key, Session);
            if (per.ekzistonPerdoruesi(prefixText) == false) result[2] = rm.GetString("msgPerdorues", ci);
            else

                if (col[0].PerdoruesAktiv == false) result[2] = rm.GetString("msgPerdoruesJoAktiv", ci);
            return result;
        }

        private static object kontrolloEksistonPerdorues(string prefixText, string[] perdorues, string[] lloji, int key, HttpSessionState Session)
        {

            int idLicence = DbCore.DbAdmin.clsLicenca.merrIdLicencePerdoruesi(mySessionObjects.ktheIdPerdoruesi(Session));


            for (int i = 0; i < perdorues.Length; i++)
            {
                if (i == key || perdorues[i] == "")
                    continue;
                if (lloji[i] == "1")
                {
                    if (perdorues[i] == prefixText)
                        return "Ekziston ky perdorues ne skeme!";

                }
                else
                {
                    int id = DbCore.DbAdmin.clsRoli.ktheIdRoli(perdorues[i], idLicence);
                    DbCore.DbAdmin.colRolPerdorues role = new DbCore.DbAdmin.colRolPerdorues();
                    role.mbushRolePerdoruesSipasRoli(id);
                    foreach (DbCore.DbAdmin.clsRolPerdorues p in role)
                    {
                        DbCore.DbAdmin.clsPerdorues per = new DbCore.DbAdmin.clsPerdorues(p.IdPerdorues);
                        if (per.PerdoruesUsername == prefixText)
                        {
                            return "Ekziston ky perdorues si pjese e nje roli ne kete skeme!";
                        }

                    }
                }
            }
            return "";
        }
        public static object[] kontrolloKaAdresaroli(string prefixText, string[] perdorues, string[] lloji, int key, HttpSessionState Session)
        {
            object[] result = new object[3];
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            int idLicence = DbCore.DbAdmin.clsLicenca.merrIdLicencePerdoruesi(idPerdoruesi);
            int idroli = DbCore.DbAdmin.clsRoli.ktheIdRoli(prefixText, idLicence);
            if (idroli <= 0)
            {
                result[0] = null;
                result[1] = true;
                return result;
            }
            result[0] = idroli;

            DbCore.DbAdmin.colRolPerdorues role = new DbCore.DbAdmin.colRolPerdorues();
            role.mbushRolePerdoruesSipasRoli(idroli);
            result[1] = false;
            for (int i = 0; i < perdorues.Length; i++)
            {
                if (i == key || perdorues[i] == "")
                    continue;
                if (lloji[i] == "2")
                {
                    if (perdorues[i] == prefixText)
                    {
                        result[2] = "Ekziston ky rol ne skeme!";
                        return result;
                    }

                }
                else
                {

                    foreach (DbCore.DbAdmin.clsRolPerdorues p in role)
                    {
                        DbCore.DbAdmin.clsPerdorues per = new DbCore.DbAdmin.clsPerdorues(p.IdPerdorues);
                        if (per.PerdoruesUsername == perdorues[i])
                        {
                            result[2] = "Ekzistojne perdorues te ketij roli ne kete skeme!";
                            return result;
                        }

                    }
                }
            }
            result[2] = "Kujdes! Perdoruesit ";

            foreach (DbCore.DbAdmin.clsRolPerdorues p in role)
            {
                DbCore.DbAdmin.clsPerdorues per = new DbCore.DbAdmin.clsPerdorues(p.IdPerdorues);
                if (per.PerdoruesEmail == "")
                {
                    result[1] = true;
                    result[2] += per.PerdoruesUsername + ", ";
                }
            }
            if (result[1].ToString() == "True")
            {
                result[2] = result[2].ToString().Substring(0, result[2].ToString().Length - 3) + " nuk kane email te konfiguruar!";
                return result;
            }
            result[2] = "";
            return result;
        }
        public static decimal merrMin(DateTime date, HttpSessionState Session)
        {
            date = date.ToLocalTime();
            DbCore.DbListPagesat.colTatimet col = new DbCore.DbListPagesat.colTatimet(mySessionObjects.merrIdNdermarrjeSesioni(Session), date);
            if (col.Count == 0)
                return 0;
            else return col[col.Count - 1].Max + (decimal)0.01;

        }
        public static double merrKursiSipasKodMonedhesAndDates(string prefixText, string date, int lloji, HttpSessionState Session)
        {
            //DbCore.DbAdmin.clsKurset cls = new DbCore.DbAdmin.clsKurset(prefixText, date, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            double kursi = DbCore.DbAdmin.clsKurset.merrKursinFunditSipasKodMonedheDateDheLloj(prefixText, date, mySessionObjects.merrIdNdermarrjeSesioni(Session), lloji);
            if (kursi != 0 && kursi != -1)
            {
                return kursi;
            }
            else
            {
                return 1;
            }
        }
        public static AutoCompleteItem[] ktheArrayKlienteFurnitoresh(string infixText, int tipKlientFurnitor, HttpSessionState Session)
        {
            //int idPerdoruesi = DbCore.mySessionObjects.ktheIdPerdoruesi(Session); ; //nga sesioni   
            DataTable tmpTable = colKlienteFurnitore.mbushKlienteFurnitoreNdermarrjesAndAutorizimeLikeNew(mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session), infixText, tipKlientFurnitor);
            tmpTable.Columns["des"].ColumnName = "desc";
            AutoCompleteItem[] AutoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                AutoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                AutoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                AutoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
            }
            return AutoCompleteItem;
        }
        public static object[] ktheKlientFurnitorVeprimeKF(string inFixText, DateTime data, HttpSessionState Session)
        {
            data = data.ToLocalTime();
            if (inFixText == null || inFixText == "")
                return null;
            clsKlientFurnitor oClsKF = new clsKlientFurnitor();
            oClsKF.mbushKlientFurnitorLikeKodi(inFixText, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //oColKF = dbKontab.ktheKlientFurnitor(prefixText,DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));            
            if (oClsKF.KodKlientFurnitor == "")
                return null;
            oClsKF.mbushKlientFurnitorSipasKoditAzhornim(oClsKF.KodKlientFurnitor, mySessionObjects.merrIdNdermarrjeSesioni(Session), data);
            //string debikredi = "Debi";
            clsLlogari llog = new clsLlogari(oClsKF.IdLlogari);
            DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha(llog.IdMonedha);
            double kurs;
            //if (oClsKF.LlojiKF == false)
            //    debikredi = "Kredi";                      
            kurs = DbCore.DbAdmin.clsKurset.merrKursinEFundit(mon.IdMonedha, 1);
            object[] veprimeKf = new object[3];
            veprimeKf[0] = oClsKF;
            veprimeKf[1] = mon;
            veprimeKf[2] = kurs;

            // klienti += oClsKF.EmertimiKF + ";" + debikredi + ";" + mon.KodiMonedha + ";" + kurs + ";" + oClsKF.KodKlientFurnitor;
            return veprimeKf;
        }

        public static object[] merrKursetMonedhaveDate(DateTime datedok, int llojKursi, HttpSessionState Session)
        {
            datedok = datedok.ToLocalTime();
            DbCore.DbAdmin.colMonedhat mon = new DbCore.DbAdmin.colMonedhat();
            mon.mbushGjitheMonedhatPozitive(mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session));
            object[] kurse = new object[mon.Count];
            for (int i = 0; i < mon.Count; i++)
            {
                DbCore.DbAdmin.clsMonedha m = mon[i];
                DbCore.DbAdmin.clsKurset cls = new DbCore.DbAdmin.clsKurset(m.IdMonedha, datedok);
                double kursi = DbCore.DbAdmin.clsKurset.merrKursinFunditPerMonedheDateDheLloj(m.IdMonedha, datedok, llojKursi);
                if (kursi == 0 || kursi == -1)
                    kursi = 1;
                kurse[i] = new { idMonedha = m.IdMonedha, kodMonedha = m.KodiMonedha, kursi = kursi };
                //if (cls.IdKursi != 0)
                //{
                //    kurse[i] = new { idMonedha = m.IdMonedha, kodMonedha = m.KodiMonedha, kursi = cls.VleraKursi };
                //}
                //else
                //{
                //    kurse[i] = new { idMonedha = m.IdMonedha, kodMonedha = m.KodiMonedha, kursi = 1 };
                //}
            }
            return kurse;
        }
        public static string ktheNrLlogarie(string prefixText)
        {
            clsLlogari oLlogari = new clsLlogari(int.Parse(prefixText));
            return oLlogari.NrLlogari;
        }
        public static object ktheNrLlogarish(int [] idLlogari)
        {
            List<int> idLlogarite = new List<int>(idLlogari);
            var llogarite = colLlogarite.merrNrLlogariSipasIdLlogarive(idLlogarite);
            return llogarite;
        }

        public static object ktheNdermarrjetDheVitet(int idRoli)
        {
            DbCore.DbAdmin.colNdermarrjet ndermarrjet = new DbCore.DbAdmin.colNdermarrjet(idRoli);
            string vitPerNderm = "";
            foreach (DbCore.DbAdmin.clsNdermarrje nderm in ndermarrjet)
            {
                DbCore.DbAdmin.colVitet vitet = new DbCore.DbAdmin.colVitet();
                vitet.mbushVitetTeNdermarjesDheRolit(idRoli, nderm.IdNdermarrje);
                foreach (DbCore.DbAdmin.clsViti vit in vitet)
                {
                    vitPerNderm += nderm.IdNdermarrje + "," + vit.KodiViti + "," + true + "," + vit.IdViti + ";";
                }
            }
            //vitet.mbushVitetTeNdermarjesDheRolit(idRoli, ndermarrjet[0].IdNdermarrje); //marrim vitet e ndermarrjes se pare ne liste mqns ajo do selektohet si fillim.
            return new { ndermarrjet = ndermarrjet, idroli = idRoli, vitPerNderm = vitPerNderm };
        }
        public static colTeDrejtaRoli ktheRolet(int idGjuha, int idRoli, int idViti, string idlicence, int idNdermarrje)
        {
            colTeDrejtaRoli rolet = new colTeDrejtaRoli();
            clsLicenca lic = new clsLicenca(int.Parse(idlicence));
            rolet = rolet.krijoPemePerTreeGrid(idGjuha, idRoli, idNdermarrje, idViti, lic.IdLlojLicenca);
            return rolet;
        }
        public static object ktheIdVitiSipasKodVitiDheIdNdermarrje(string kodViti, string check, int idNdermarrje)
        {
            DbCore.DbAdmin.clsViti viti = new DbCore.DbAdmin.clsViti();
            viti.mbushVitetMet(kodViti, idNdermarrje);
            return new { idNdermarrje = idNdermarrje, kodViti = kodViti, idViti = viti.IdViti, checkuar = check };
        }
        public static object ktheVitetPerNdermarrjen(int idNdermarrje)
        {
            DbCore.DbAdmin.colVitet vitet = new DbCore.DbAdmin.colVitet(idNdermarrje);
            return new { vitet = vitet };
        }
        public static string[] ktheKurseMonedhashSipasDates(string dtDokumenti, int llojKursi, int idNdermarrje, int idPerdorues)
        {
            DbCore.DbAdmin.colMonedhat mon = new DbCore.DbAdmin.colMonedhat();
            mon.mbushGjitheMonedhat(idNdermarrje, idPerdorues);
            string[] monedhakurs = new string[mon.Count];
            int i = 0;
            foreach (DbCore.DbAdmin.clsMonedha m in mon)
            {
                DbCore.DbAdmin.clsKurset kurs = new DbCore.DbAdmin.clsKurset(m.IdMonedha, DateTime.Parse(dtDokumenti), llojKursi);
                //monedhakurs[i] = m.IdMonedha + ";" + m.KodiMonedha + ";" + kurs.VleraKursi;
                if (kurs.VleraKursi == 0)
                    monedhakurs[i] = m.IdMonedha + ";" + m.KodiMonedha + ";" + 1;
                else
                    monedhakurs[i] = m.IdMonedha + ";" + m.KodiMonedha + ";" + kurs.VleraKursi;
                i++;
            }
            return monedhakurs;
        }
        public static string ktheLlojeClientSide(string text)
        {
            string temp = "";


            string art = 1 + ";" + "Artikull" + "|";
            string mak = 2 + ";" + "Makro" + "|";
            string llog = 3 + ";" + "Llogari" + "|";
            string te = 4 + ";" + "Text" + "|";
            string cn = 5 + ";" + "Credit Note" + "|";
            string nen = 6 + ";" + "Nentotali" + "|";
            string[] rendit = new string[6];
            if (text != "0")
            {
                rendit[int.Parse(text[0].ToString()) - 1] = art;
                rendit[int.Parse(text[1].ToString()) - 1] = mak;
                rendit[int.Parse(text[2].ToString()) - 1] = llog;
                rendit[int.Parse(text[3].ToString()) - 1] = te;
                rendit[int.Parse(text[4].ToString()) - 1] = cn;
                rendit[int.Parse(text[5].ToString()) - 1] = nen;
            }
            else
            {
                rendit[0] = art;
                rendit[1] = mak;
                rendit[2] = llog;
                rendit[3] = te;
                rendit[4] = cn;
                rendit[5] = nen;
            }
            for (int i = 0; i < 6; i++)
                temp += rendit[i];
            if (temp != "")
            {
                temp = temp.Substring(0, temp.Length - 1);
            }
            return temp;
        }
        public static Object merrKursinMonedhenGjendjenSipasBankesAndDates(int idBanka, string dtDokumenti, int idKonfigurimi, int llojKursi, int idPerdorues, int idNdermarrje)
        {

            String kodimonedha = "";
            String kursifundit = "";
            String gjendja = "";
            string mesazh = "";
            clsDatabaseArkaBanka dbArkaBanka = new clsDatabaseArkaBanka();
            clsBanka banka = new clsBanka();
            //banka.mbushBankeSipasKodit(kodBanka, idNdermarje);
            banka.mbushBanke(idBanka);
            clsKonfigurimAmbjenti konfig = new clsKonfigurimAmbjenti(idKonfigurimi);
            if (konfig.IdKategori == 3 && banka.LlojArkaBanka == true)
                mesazh = "Nuk mund te zgjidhni banke per veprimet me arken!";
            else if (konfig.IdKategori == 4 && banka.LlojArkaBanka == false)
                mesazh = "Nuk mund te zgjidhni arke per veprimet me banken!";
            DbCore.DbAdmin.clsMonedha monedha = new DbCore.DbAdmin.clsMonedha(banka.IdMonedhaBanka);
            kodimonedha = monedha.KodiMonedha;
            if (monedha.IdMonedha == DbCore.DbAdmin.clsMonedha.ktheIdMonedhenENdermarrjes(idNdermarrje))
                kursifundit = "1";
            else
            {
                double kursi = DbCore.DbAdmin.clsKurset.merrKursinFunditPerMonedheDateDheLloj(monedha.IdMonedha, DateTime.Parse(dtDokumenti), llojKursi);
                if (kursi == 0 || kursi == -1)
                    kursi = 1;
                kursifundit = Convert.ToString(kursi);
            }
            gjendja = dbArkaBanka.ktheGjendjenBankes(banka.IdBanka, banka.LlojArkaBanka, idPerdorues, idNdermarrje, DateTime.Parse(dtDokumenti)).ToString();

            clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(idKonfigurimi);
            clsFormatKonfigTrup formatMonedhe = new clsFormatKonfigTrup();
            if (formatNrPerKonfig.IdFormatKonfig > 0)
                formatMonedhe = formatNrPerKonfig.KonfigTrupi.merrFormatSipasMonedhes(monedha.IdMonedha);
            else
                formatMonedhe = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);

            int formatKursi = 2;
            if (monedha.IdFormatNrKursi != 0)
            {
                formatKursi = clsFunksione.MerrVleraFormatKursi(banka.IdMonedhaBanka);
            }
            return new { kursifundit = kursifundit, kodimonedha = kodimonedha, gjendja = gjendja, idMonedha = monedha.IdMonedha, degeAdministrative = banka.IdDegeAdministrative, formatMonedhe = formatMonedhe, formatKursi = formatKursi, mesazh = mesazh, banka = banka };
        }
        public static object ktheEmerKlientFurnitorNew(string EmertimiKF, bool LlojiKF, HttpSessionState Session)
        {
            clsKlientFurnitor oClsKF = new clsKlientFurnitor();
            oClsKF.MbushKlientFurnitor(EmertimiKF, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (oClsKF != null && oClsKF.IdKlientFurnitor != 0 && !String.IsNullOrEmpty(oClsKF.Monedha))
            {
                DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha();
                mon.mbushMonedhePershk(oClsKF.Monedha, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                return new { EmertimiKF = oClsKF.EmertimiKF, KodiMonedha = mon.KodiMonedha, LlojiKF = oClsKF.LlojiKF };
            }
            else
            {
                return 0;
            }
        }
        public static object ktheEmerKlientFurnitorNew(clsKlientFurnitor oClsKF, bool LlojiKF, HttpSessionState Session)
        {
            if (oClsKF != null && oClsKF.IdKlientFurnitor != 0 && !String.IsNullOrEmpty(oClsKF.Monedha))
            {
                DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha();
                mon.mbushMonedhePershk(oClsKF.Monedha, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                return new { EmertimiKF = oClsKF.EmertimiKF, KodiMonedha = mon.KodiMonedha, LlojiKF = oClsKF.LlojiKF };
            }
            else
            {
                return 0;
            }
        }
        public static Boolean ktheIsValidDateDokumenti(String dt, HttpSessionState Session)
        {
            Boolean result = false;
            DateTime data = new DateTime();
            data = Convert.ToDateTime(dt);
            DbCore.DbAdmin.clsPeriudhaKontabel periudha = new DbCore.DbAdmin.clsPeriudhaKontabel();
            periudha = mySessionObjects.merrPeriudheKontabel(Session);
            if (data >= periudha.FillimiPeriudha && data <= periudha.MbarimiPeriudha)
                result = true;
            else
                result = false;
            return result;
        }
        public static string ktheKushtePagese(object[] prefixText)
        {
            string kushtetPageses = "";
            try
            {
                //  object kushtetPageses = null;
                for (int i = 0; i < prefixText.Length; i++)
                {
                    {
                        if (prefixText[i] != null && prefixText[i].ToString() != "")
                        {
                            Dictionary<string, object> dic = (Dictionary<string, object>)prefixText[i];

                            Int32 nrrreshtit = (Int32)dic["nrrreshtit"];
                            Int32 idkoka = (Int32)dic["idkoka"];
                            DateTime dataDokBanke = (DateTime)dic["dataDokBanke"];
                            DateTime dataFatures = (DateTime)dic["dataFatures"];
                            double vlefta = (double)dic["vlefta"];

                            int idkushtpagese = 0;
                            //int.TryParse(idkoka, out idkushtpagese); //TODO NESTILA jep error
                            int diferencaDatave = int.Parse((dataDokBanke - dataFatures).Days.ToString());
                            clsKushtPageseKoka clsKoka = new clsKushtPageseKoka(idkushtpagese);
                            //oCol = dbKontab.merrKushtPageseSipasID(int.Parse(vlerat[1]));
                            if (clsKoka != null)
                            {
                                double zbritja = 0;
                                if (clsKoka.LlojiKushtPagese == "E plote")
                                {
                                    //DbCore.colKushtPageseTrupi colTrupi = dbKontab.merrTrupatKushtevePagesesSipasKokes(clsKoka.IdKoka);
                                    colKushtPageseTrupi colTrupi = new colKushtPageseTrupi(clsKoka.IdKoka);
                                    foreach (clsKushtPageseTrupi t in colTrupi)
                                    {
                                        if (t.Intervali == "Dite")
                                        {
                                            double zb = 0;
                                            if (diferencaDatave <= t.Dite)//nqs plotesohet kushti aplikohet zbritja
                                                zb = (double.Parse(t.Zbritje.ToString()) / 100) * vlefta;
                                            if (zbritja < zb)//merret zbritja me me madhe qe i behet klientit(ne rastin kur kemi 2 zbritje qe nsryshojne nga numri i diteve)
                                                zbritja = zb;
                                        }
                                    }
                                }
                                kushtetPageses += nrrreshtit + ":" + clsKoka.EmertimiKushtPagese + ":" + zbritja + ";";

                            }
                        }
                    }

                }
            }
            catch (Exception)
            {
                return "";
            }
            return kushtetPageses;
        }
        public static clsThemesAmbjente ktheThemeAmbjenteSipasId(int id)
        {
            return id == 0 ? null : new clsThemesAmbjente(id);
        }

        public static object KtheEmratThemeSelektuar(int id, HttpSessionState session)
        {
            object[] vlerat = new object[5];
            DbCore.DbAdmin.clsThemesAmbjente theme = new DbCore.DbAdmin.clsThemesAmbjente(id);
            vlerat[0] = new DbCore.DbAdmin.clsThemesDevExpressJQuery(theme.IdThemeFrames).EmriTheme;
            vlerat[1] = new DbCore.DbAdmin.clsThemesDevExpressJQuery(theme.IdThemeFrameKryesor).EmriTheme;
            vlerat[2] = new DbCore.DbAdmin.clsThemesDevExpressJQuery(theme.IdThemeJQuery).EmriTheme;
            vlerat[3] = new DbCore.DbAdmin.clsTheme(theme.IdBgImage).EmriTheme;
            vlerat[4] = id;
            return vlerat;
        }
        public static int KtheIDThemeBgImg(string input)
        {
            DbCore.DbAdmin.clsTheme MyTheme = new DbCore.DbAdmin.clsTheme();
            MyTheme.EmriTheme = input;
            MyTheme = MyTheme.merrSipasEmri();

            return MyTheme.IdTheme;
        }
        public static int KtheIDThemeDevExJQuery(string input)
        {
            //return DbCore.DbAdmin.clsThemesDevExpressJQuery.ktheIdThemeNgaEmri(input)+ ":" + ;
            DbCore.DbAdmin.clsThemesDevExpressJQuery cls = new DbCore.DbAdmin.clsThemesDevExpressJQuery(input);
            return cls.IdTheme;
        }
        public static object KaVeprimeVendndodhje(int id)
        {
            return DbCore.DbListPagesat.clsVendndodhjet.kaVeprimeVendndodhjet(id);
        }


        public static String[] ktheArrayMeDataZbritje(string kodi, string kodbar, string emer1, string emer2, string kodifikim1, string kodifikim2, string furnitori, string njesia, string datafill, string datambar, string idnivelcmimi, HttpSessionState Session)
        {
            if (kodifikim1 != "")
                //kodifikim1 = new DbCore.DbInventari.clsKodifikimArtikulli(kodifikim1, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)).IdKodifikimi.ToString();
                kodifikim1 = new clsKodifikimArtikulli(kodifikim1, mySessionObjects.merrIdNdermarrjeSesioni(Session), 1, false).IdKodifikimi.ToString();
            if (kodifikim2 != "")
                //kodifikim2 = new DbCore.DbInventari.clsKodifikimArtikulli(kodifikim2, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session)).IdKodifikimi.ToString();
                kodifikim2 = new clsKodifikimArtikulli(kodifikim2, mySessionObjects.merrIdNdermarrjeSesioni(Session), 2, false).IdKodifikimi.ToString();
            if (datafill == "01/01/0100")
                datafill = "";
            if (datambar == "01/01/0100")
                datambar = "";
            DataTable dt = colZbritjetAnalitike.merrZbritjeAnalitikeSipasFiltrit(kodi, kodbar, emer1, emer2, kodifikim1, kodifikim2, furnitori, njesia, datafill, datambar, idnivelcmimi, mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session));//shitje
            String[] dtFillimi = (from DataRow dr in dt.Rows
                                  select ((DateTime)dr["DateFillimi"]).ToShortDateString()).ToArray();
            return dtFillimi;
        }
        public static object kaVeprimeOpsion(int idopsioni)
        {
            DbCore.DbCRM.clsDatabaseCRM DbCRM = new DbCore.DbCRM.clsDatabaseCRM();
            if (DbCRM.kaVeprimeOpsion(idopsioni))
                return "true";
            else return "false";
        }
        public static AutoCompleteItem[] ktheArtikullPerProdhim(string prefixText, int klasa, HttpSessionState Session)
        {
            DataTable tmpTable;
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            if (klasa == 4)

                tmpTable = colArtikujt.merrArtikujLikeKodPershkKodbarDTJoProdhim(mySessionObjects.merrIdNdermarrjeSesioni(Session), idPerdoruesi, prefixText, 1);
            else if (klasa == 0) tmpTable = colArtikujt.merrArtikujLikeKodPershkKodbarDTArtProdhim(mySessionObjects.merrIdNdermarrjeSesioni(Session), idPerdoruesi, prefixText, 1);
            else tmpTable = colArtikujt.merrArtikujLikeKodPershkKodbarDTProdhim(mySessionObjects.merrIdNdermarrjeSesioni(Session), idPerdoruesi, prefixText, 1);
            tmpTable.Columns["des"].ColumnName = "desc";
            AutoCompleteItem[] AutoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                AutoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                AutoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                AutoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
            }
            return AutoCompleteItem;
        }

        internal static object[] ktheTemplateteNivelit(string lloji, string veprimi, bool mod, HttpSessionState Session)
        {
            //DbCore.DbShare.clsDatabaseShare dbShare = new DbCore.DbShare.clsDatabaseShare();
            //DbCore.DbRegjistrim.clsDatabaseRegjistrim dbRegj = new DbCore.DbRegjistrim.clsDatabaseRegjistrim();
            int idperdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            DbCore.DbShare.colKonfigurimAmbjenti modelet = new DbCore.DbShare.colKonfigurimAmbjenti();
            DbCore.DbShare.clsKonfigurimAmbjenti konf = new DbCore.DbShare.clsKonfigurimAmbjenti();
            if (veprimi == "shitje")
                konf.IdKategori = 1;
            else if (veprimi == "blerje") konf.IdKategori = 2;
            else if (veprimi == "magazina") konf.IdKategori = 6;
            else if (veprimi == "fletekontabel") konf.IdKategori = 5;
            else if (veprimi == "banka") konf.IdKategori = 4;
            else if (veprimi == "arka") konf.IdKategori = 3;
            else if (veprimi == "vkf") konf.IdKategori = 20;
            else if (veprimi == "shsh") konf.IdKategori = 7;
            else if (veprimi == "qendrakosto") konf.IdKategori = 75;
            else if (veprimi == "rezervime") konf.IdKategori = 78;
            else if (veprimi == "riparime") konf.IdKategori = 80;
            else if (veprimi == "amortizimi") konf.IdKategori = 86;
            else if (veprimi == "rivleresimAm") konf.IdKategori = 90;
            konf.IdNdermarje = mySessionObjects.merrIdNdermarrjeSesioni(Session);

            if (lloji != "")
            {
                int idNiveli = 0;
                idNiveli = int.Parse(lloji);
                konf.IdNivel = idNiveli;
                modelet.mbushKonfigAmbjSipasIdKategoriIdNivelMeLloj(konf.IdKategori, konf.IdNivel, idperdoruesi);
                //modelet = dbShare.merrKonfigAmbjSipasIdKategoriIdNivel(konf, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            }
            else
                modelet.mbushKonfigAmbjSipasIdKategori(konf.IdKategori, konf.IdNdermarje, idperdoruesi, mySessionObjects.ktheGjuhe(Session));
            //modelet = dbShare.merrKonfigAmbjSipasIdKategori(konf, DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            if (modelet.Count == 0)
                modelet.mbushKonfigDefaultKategori(konf.IdKategori);
            //modelet = dbShare.ktheKonfigDefaultKategori(konf.IdKategori);

            DbCore.DbShare.colKonfigurimAmbjenti konfVarura = new DbCore.DbShare.colKonfigurimAmbjenti();
            foreach (DbCore.DbShare.clsKonfigurimAmbjenti konfi in modelet)
            {
                var alternativa = clsAlternativaKushti.getAlternativa(konfi.IdKonfigAmbjente, "V");
                if (alternativa == "Po" && !mod)
                    konfVarura.Add(konfi);
            }
            foreach (DbCore.DbShare.clsKonfigurimAmbjenti konfi in konfVarura)
            {
                modelet.Remove(konfi);
            }
            //foreach (DbCore.DbShare.clsKonfigurimAmbjenti o in modelet)
            //    templatet += o.IdKonfigAmbjente + "," + o.KodKonfigAmbjente + ";" + o.PershkrimKonfigAmbjente + "|";
            object[] modeletSlim = new object[modelet.Count];
            for (int i = 0; i < modelet.Count; i++)
            {
                modeletSlim[i] = new { IdKonfigAmbjente = modelet[i].IdKonfigAmbjente, KodKonfigAmbjente = modelet[i].KodKonfigAmbjente, PershkrimKonfigAmbjente = modelet[i].PershkrimKonfigAmbjente };
            }
            return modeletSlim;
        }
        public static List<string> merrDataNdryshimiAktiviteti(int idkoka)
        {
            return colAktiviteteTrupi.merrDataAktiviteti(idkoka);
        }
        public static List<string> merrDataNdryshimiArtPerberes(int idartikulli)
        {
            return colArtikulliPerberes.merrDataArtikujPerberes(idartikulli);
        }
        public static colGjendjeArtikulli merrGjendjeArtikulli(int idartikulli)
        {
            colGjendjeArtikulli col = new colGjendjeArtikulli();
            col.ktheGjendjeMinMaxArtikulli(idartikulli);
            return col;
        }
        
        public static int[] KtheIdMonedhaSipasIdLlogarise(int idja, int idrreshti)
        {
            int[] result = new int[2];
            result[0] = idrreshti;
            clsLlogari llogaria = new clsLlogari(idja);
            if (llogaria.IdLlogari > 0)
            {
                result[1] = llogaria.IdMonedha;
                return result;
            }
            else
            {

                result[1] = -1; return result;
            }
        }
        public static object[] ndryshoNjesi(int key, int njesia, int idart)
        {
            object[] result = new object[2];

            clsArtikulli art = new clsArtikulli();
            art.mbushArtikull(idart);
            decimal koef = 0;
            if (art.Njesi1Artikulli == njesia)
                koef = art.KoeficientArtikulli;
            else koef = 1 / art.KoeficientArtikulli;
            result[0] = key;
            result[1] = koef;

            return result;
        }

        public static Object ktheRowVleraArtMeIDMeRec(int idja, int rreshti, DateTime data, int magazina, int magazina2, string sasiplanrec, HttpSessionState Session)
        {
            data = data.ToLocalTime();
            clsArtikulli artkryesor = new clsArtikulli(idja);
            return ktheRowVleraArtikulli(artkryesor, rreshti, data, magazina, magazina2, sasiplanrec, Session, "kartela", 1, 0);
        }

        private static Object merrRec(int idartikulli, int idmag, int idmagprodukti, double sasi, DateTime data, clsProduktProdhimi trup, decimal koef, double sasiplan, int idplanifikimi, string sasiburimi, HttpSessionState Session, int idPerdorues)
        {
            return colRecepturaProdhimi.merrRec(idartikulli, idmag, idmagprodukti, sasi, data, trup, koef, sasiplan, idplanifikimi, sasiburimi, idPerdorues);
        }

        public static Object ktheRowVleraArtMeRec(string kodi, int rreshti, DateTime data, int magazina, int magazina2, string sasiplanrec, HttpSessionState Session)
        {
            data = data.ToLocalTime();
            clsArtikulli artkryesor = new clsArtikulli();
            artkryesor.ktheArtikullSipasKoditDheAutorizime(kodi, mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session));
            return ktheRowVleraArtikulli(artkryesor, rreshti, data, magazina, magazina2, sasiplanrec, Session, "kartela", 1, 0);
        }

        public static Object ktheRowVleraArtikulli(clsArtikulli artkryesor, int rreshti, DateTime data, int magazina, int magazina2, string sasiplanrec, HttpSessionState Session, string sasiburimi, double sasia, int idtrupiplanifikimi)
        {
            if (artkryesor.IdArtikulli == 0)
                return new { rreshti = rreshti, artkryesor = artkryesor, prod = new clsProduktProdhimi() };
            colRecepturaProdhimi colrec = new colRecepturaProdhimi();
            colNjesiteArtikulli colnjesirec = new colNjesiteArtikulli();
            colNjesiAdministrative colmagrec = new colNjesiAdministrative();
            colDetajimeArtikulli coldet1 = new colDetajimeArtikulli();
            colDetajimeArtikulli coldet2 = new colDetajimeArtikulli();
            int mag;
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);

            if (artkryesor.IdMagazina != 0)
                mag = artkryesor.IdMagazina;
            else mag = magazina;

            clsProduktProdhimi prod = new clsProduktProdhimi(1, 0, artkryesor.IdArtikulli, artkryesor.KodArtikulli, artkryesor.PershkrimArtikulli, artkryesor.Njesi1Artikulli, 0, sasia, 0, mag, 0, 0, 0, 0, 0, "", 0, 0);
            const decimal koef = 1;
            int idKokaPlanifikim = 0;
            if (idtrupiplanifikimi > 0)
            {
                clsTrupiPlanifikim trup = new clsTrupiPlanifikim(idtrupiplanifikimi, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                idKokaPlanifikim = trup.IdKokaPlanifikim;
            }
            Object receptura = RregjistrimeRepository.merrRec(artkryesor.IdArtikulli, magazina2, magazina, prod.SasiaAktuale, data, prod, koef, (sasiplanrec == "sasi plan") ? prod.SasiaPlanifikuar : prod.SasiaAktuale, idKokaPlanifikim, sasiburimi, Session, idPerdorues);
            prod.Kosto = prod.KostoTotale / prod.SasiaAktuale;
            return new { rreshti = rreshti, artkryesor = artkryesor, prod = prod, teDhenaRecepturash = receptura };
        }

        public static Object MerrPlanifikimeTeGjeneruara( int id, int magazinaprod, int magazinarec, DateTime date, string sasiplanrec, string sasiburimi, HttpSessionState Session)
        {
            clsArtikulli art = new clsArtikulli();
            date = date.ToLocalTime();
            int idPerdorues = DbCore.mySessionObjects.ktheIdPerdoruesi(Session);
            colProduktProdhimi col = new colProduktProdhimi();
            colRecepturaProdhimi colrec = new colRecepturaProdhimi();
            colNjesiteArtikulli colnjesi = new colNjesiteArtikulli();
            colNjesiAdministrative colmag = new colNjesiAdministrative();
            colNjesiteArtikulli colnjesirec = new colNjesiteArtikulli();
            colNjesiAdministrative colmagrec = new colNjesiAdministrative();
            colDetajimeArtikulli coldet1 = new colDetajimeArtikulli();
            colDetajimeArtikulli coldet2 = new colDetajimeArtikulli();
            List<Object> listaRecepturave = new List<Object>();
            List<Object> listaDetajimeveArtProdhim = new List<Object>();
            int idNjesiProdh = clsKokaPlanifikim.ktheNjesiProdhimiSipasIdPlanifikim(id);
            colTrupiPlanifikim coltrupi = new colTrupiPlanifikim(id, mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session));
            foreach (clsTrupiPlanifikim trup in coltrupi)
            {
                if (trup.Sasimbetur == 0)
                    continue;
                int magazina;
                if (magazinaprod != 0)
                    magazina = magazinaprod;
                else magazina = trup.IdMag;
                clsTrupiShitje tr = new clsTrupiShitje(trup.IdUrdherPorosi);
                clsProduktProdhimi prod = new clsProduktProdhimi(col.Count == 0 ? 1 : col[col.Count - 1].Id + 1, 0, trup.IdArtikulli, trup.KodiArtikull, trup.PershkrimArtikull, trup.IdNjesia, trup.Sasia, trup.Sasimbetur, 0, magazina, trup.Gjeresi, trup.Gjatesi, trup.SasiPermase, trup.IdTrupiPlanifikim, trup.IdUrdherPorosi, tr.Shenime, trup.Detajim1, trup.Detajim2);
                clsNjesiArtikulli njesi = new clsNjesiArtikulli(trup.IdNjesia);
                colnjesi.Add(njesi);
                clsNjesiAdministrative mag = new clsNjesiAdministrative(magazina, mySessionObjects.ktheIdPerdoruesi(Session));
                colmag.Add(mag);
                Dictionary<String, Object> detajimeArtProdhim = new Dictionary<string, object>();
                detajimeArtProdhim.Add("det1ArtProdhim", new clsDetajimArtikulli(trup.Detajim1));
                detajimeArtProdhim.Add("det2ArtProdhim", new clsDetajimArtikulli(trup.Detajim2));
                listaDetajimeveArtProdhim.Add(detajimeArtProdhim);
                art.mbushArtikull(trup.IdArtikulli);
                decimal koef = 0;
                if (art.Njesi1Artikulli == trup.IdNjesia)
                    koef = 1;
                else koef = art.KoeficientArtikulli;
                Object receptura = merrRec(trup.IdArtikulli, magazinarec, magazina, trup.Sasimbetur, date, prod, koef, (sasiplanrec == "sasi plan") ? prod.SasiaPlanifikuar : trup.Sasimbetur, id, sasiburimi, Session, idPerdorues);
                listaRecepturave.Add(receptura);
                Object artProd = art.mbushArtikull(trup.IdArtikulli);
                prod.Kosto = prod.KostoTotale / prod.SasiaAktuale;
                col.Add(prod);
            }
            return new { art = art, produktet = col, detajimet = listaDetajimeveArtProdhim, njesite = colnjesi, magazinat = colmag, idnjesiprodhim = idNjesiProdh, teDhenaRecepturash = listaRecepturave, id = id };
        }

        public static string ktheMonedheNdermarrjeClientSide(HttpSessionState Session)
        {
            string temp = "";
            //int idmonedha = -1;            
            DbCore.DbAdmin.colMonedhat colMonedhat = new DbCore.DbAdmin.colMonedhat();
            colMonedhat.mbushGjitheMonedhatAktive(mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session));
            //DbCore.DbAdmin.colMonedhat colMonedhat = dbAdmin.merrGjitheMonedhatAktive(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), DbCore.mySessionObjects.ktheIdPerdoruesi(Session));
            foreach (DbCore.DbAdmin.clsMonedha m in colMonedhat)
            {
                temp = temp + m.IdMonedha + ";" + m.KodiMonedha + "|";
            }
            if (temp != "")
            {
                temp = temp.Substring(0, temp.Length - 1);
            }

            return temp;
        }
        public static double[] merrKursiSipasMonedhesAndDatesDheRreshti(int idMonedha, DateTime date, int idrreshti, string kodmonedha, int lloji, HttpSessionState Session)
        {
            double[] result = new double[2];
            result[0] = idrreshti;
            if (kodmonedha != "")
            {
                DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha();
                mon.mbushMonedhen(kodmonedha, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                idMonedha = mon.IdMonedha;
            }
            if (idMonedha == 0)
            {
                result[1] = 1.00;
                return result;
            }
            date = date.ToLocalTime();
            //DbCore.DbAdmin.clsKurset cls = new DbCore.DbAdmin.clsKurset(idMondedha, date);
            //if (cls.IdKursi != 0)
            //{
            //    result[1] = cls.VleraKursi;
            //    return result;
            //}
            double kursi = DbCore.DbAdmin.clsKurset.merrKursinFunditPerMonedheDateDheLloj(idMonedha, date, lloji);
            if (kursi == 0)
                result[1] = 1;
            else result[1] = kursi;
            return result;
        }
        public static string merrKursetDheMonedhatSipasDates(string[] prefixText, DateTime date, HttpSessionState Session)
        {
            date = date.ToLocalTime();
            List<double> items = new List<double>();
            string temp = "";
            foreach (string s in prefixText)
            {
                if (s != "")
                {
                    DbCore.DbAdmin.clsMonedha monedha = new DbCore.DbAdmin.clsMonedha();
                    monedha.mbushMonedhen(s, mySessionObjects.merrIdNdermarrjeSesioni(Session));
                    //monedha = db.ktheMonedhe(s, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session))[0];
                    //DbCore.DbAdmin.colKurset col = new DbCore.DbAdmin.colKurset();
                    DbCore.DbAdmin.clsKurset cls = new DbCore.DbAdmin.clsKurset();
                    if (s != null)
                        cls = new DbCore.DbAdmin.clsKurset(monedha.IdMonedha, date);
                    //col = db.merrKursetSipasMonedhesAndDates(monedha.IdMonedha, date);

                    if (cls.IdKursi != 0)
                    {
                        temp = temp + s + "|" + cls.VleraKursi + "||";
                        //items.Add(col[0].VleraKursi);
                    }
                    else
                    {
                        temp = temp + s + "|1.00||";
                        //items.Add(0.00);
                    }
                }
            }
            return temp;
        }
        
        public static bool kaVeprimeFusha(int iddokumenti)
        {
            bool lidhur = false;
            if (DbCore.DbAdmin.clsModeliFushaShtese.kaveprime(iddokumenti))
            {
                lidhur = true;
            }
            return lidhur;
        }
        public static clsBanka merrBankaSipasId(int idbanka)
        {
            clsBanka banka = new clsBanka();
            banka.mbushBanke(idbanka);
            return banka;
        }
        public static clsKlientFurnitor merrKfSipasId(int idkf)
        {
            clsKlientFurnitor kf = new clsKlientFurnitor();
            kf.MbushKlientFurnitorSipasId(idkf);
            return kf;
        }

        public static string ktheMonedheLlogSipasKodit(string kodi, HttpSessionState Session)
        {

            clsLlogari llogaria = new clsLlogari(kodi, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (llogaria.IdLlogari > 0)
            {
                return llogaria.KodiMonedha;
            }
            else
                return null;
        }

        public static konfigAmbientiSlim[] ktheKonfigurimetENivelit(string kodNiveli, int idKategori, int idGjuha, int idNdermarrje, int idPerdoruesi)
        {
            colKonfigurimAmbjenti modelet = new colKonfigurimAmbjenti();
            modelet.mbushKonfigAmbjSipasIdKategoriKodNivel(idKategori, kodNiveli, idNdermarrje, idPerdoruesi, idGjuha);
            if (modelet.Count == 0)
                modelet.mbushKonfigDefaultKategori(idKategori, idNdermarrje);

            colKonfigurimAmbjenti konfVarura = new colKonfigurimAmbjenti();
            foreach (clsKonfigurimAmbjenti konfi in modelet)
            {
                var alternativa = clsAlternativaKushti.getAlternativa(konfi.IdKonfigAmbjente, "V");
                if (alternativa == "Po")
                {
                    konfVarura.Add(konfi);
                }
            }
            foreach (clsKonfigurimAmbjenti konfi in konfVarura)
            {
                modelet.Remove(konfi);
            }
            konfigAmbientiSlim[] modeletSlim = new konfigAmbientiSlim[modelet.Count];
            for (int i = 0; i < modelet.Count; i++)
            {
                modeletSlim[i] = new konfigAmbientiSlim() { IdKonfigAmbjente = modelet[i].IdKonfigAmbjente, KodKonfigAmbjente = modelet[i].KodKonfigAmbjente, PershkrimKonfigAmbjente = modelet[i].PershkrimKonfigAmbjente };
            }
            return modeletSlim;
        }
        public static object[] gjejEtapeDokumentiList(int idkoka, int idperdoruesi)
        {
            object[] result = new object[2];
            clsEtapeAprovimi etapa = new clsEtapeAprovimi();
            etapa.ktheEtapeFunditSipasKokaListpagesaDhePerdorues(idkoka, idperdoruesi);
            result[0] = etapa.IdEtapa;
            result[1] = etapa.NrProcesi;
            return result;

        }
        public static string kthePershkrimArtikulli(string prefixText, HttpSessionState Session)
        {

            clsArtikulli art = new clsArtikulli();
            art.merrSipasKodArtikullit(prefixText, mySessionObjects.merrIdNdermarrjeSesioni(Session));

            string artikulli = art.PershkrimArtikulli + ";";
            clsNjesiArtikulli njesi1 = new clsNjesiArtikulli(art.Njesi1Artikulli);
            clsNjesiArtikulli njesi2 = new clsNjesiArtikulli(art.Njesi2Artikulli);

            if (njesi1 != null)
                artikulli += njesi1.IdNjesia + "," + njesi1.PershkrimNjesia + ";";
            else artikulli += ";";
            if (njesi2 != null)
                artikulli += njesi2.IdNjesia + "," + njesi2.PershkrimNjesia;
            else artikulli += "";
            return artikulli;

            //////////List<string> items = new List<string>();
            //////DbCore.DbInventari.colArtikujt oColArtikujt = new DbCore.DbInventari.colArtikujt();
            //////DbCore.DbAdmin.clsPerdorues oPerdorues = new DbCore.DbAdmin.clsPerdorues();
            //////oPerdorues = (DbCore.DbAdmin.clsPerdorues)(Session["oClsPerdoruesi"]);
            //////DbCore.DbInventari.clsDatabaseInventari db = new DbCore.DbInventari.clsDatabaseInventari();
            //////int idNderViti = Convert.ToInt32(DbCore.mySessionObjects.ktheNdermarrjeVit(Session).ToString()); //nga sesioni
            //////int idPerdoruesi = oPerdorues.IdPerdorues; //nga sesioni

            //////oColArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeLike(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idPerdoruesi, prefixText);
            ////////oColArtikujt = db.merrArtikujNdermarrjesAndAutorizimeLike(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session), idPerdoruesi, prefixText);
            //////if (oColArtikujt.Count > 0)
            //////{
            //////   string artikulli= oColArtikujt[0].PershkrimArtikulli+";";
            //////    DbCore.DbInventari.clsNjesiArtikulli njesi1 = new DbCore.DbInventari.clsNjesiArtikulli(oColArtikujt[0].Njesi1Artikulli);
            //////    DbCore.DbInventari.clsNjesiArtikulli njesi2 = new DbCore.DbInventari.clsNjesiArtikulli(oColArtikujt[0].Njesi2Artikulli);
            //////    //DbCore.DbInventari.colNjesiteArtikulli njesi1 = db.ktheNjesiAritkulli(art.Njesi1Artikulli);
            //////    //DbCore.DbInventari.colNjesiteArtikulli njesi2 = db.ktheNjesiAritkulli(art.Njesi2Artikulli);

            //////    if (njesi1 != null)
            //////        artikulli += njesi1.IdNjesia + "," + njesi1.PershkrimNjesia + ";";
            //////    else artikulli += ";";
            //////    if (njesi2 != null)
            //////        artikulli += njesi2.IdNjesia + "," + njesi2.PershkrimNjesia;
            //////    else artikulli += "";
            //////    return artikulli;
            //////}
            //////else
            //////{
            //////    return "";
            //////}
        }
        public static DbCore.DbAdmin.colKurset ktheKurseSipasIdMonedhe(int idMon)
        {
            DbCore.DbAdmin.colKurset colKurset = new DbCore.DbAdmin.colKurset();
            if (idMon != -1)
                colKurset.mbushKursetFunditMonedhes(idMon);
            else
            {
                DbCore.DbAdmin.clsKurset oTrupi = new DbCore.DbAdmin.clsKurset();
                oTrupi.PershkrimLlojKursi = "Kursi1";
                oTrupi.LlojKursi = 1;
                oTrupi.DataKursit = DateTime.Today;
                oTrupi.VleraKursi = 1;
                oTrupi.NjesiaKursit = 1;
                colKurset.Add(oTrupi);
            }
            return colKurset;
        }

        public static object merrImazh(int idndermarje, HttpSessionState Session)
        {

            if (idndermarje == 0) return true;
            DbCore.DbAdmin.clsNdermarrje ndermarrja = new DbCore.DbAdmin.clsNdermarrje(idndermarje);

            return mySessionObjects.ruajImazhNeSesion(Session, ndermarrja.NdermarrjeLogo);


        }
        public static bool merrKodTvshNdermarrje(int idndermarje)
        {
            if (clsKontrollePerFiskalizimin.ktheNeseKlientiEshteAzhornuarPerFiskalizim())
            {
                DbCore.DbAdmin.clsNdermarrje ndermarrja = new DbCore.DbAdmin.clsNdermarrje(idndermarje);
                return ndermarrja.MeTvsh;
            }
            else
                return false;


        }
        public static void ruajFusha(string vlera, string fusha, int index, HttpSessionState Session)
        {

            DbCore.DbAdmin.colTrupiFormatImporti trupavis = mySessionObjects.merrFormatImportiVisibleNgaSesioni(Session);
            if (trupavis.Count == 0)
                return;
            switch (fusha)
            {
                case "txtEmri":
                    trupavis[index].EmerImporti = vlera;
                    break;
                case "txtVlera":
                    trupavis[index].VleraDefault = vlera;
                    break;
                case "cbDetyrueshem":
                    trupavis[index].Detyrueshme = Convert.ToBoolean(vlera);
                    break;
                case "cbShfaq":
                    trupavis[index].Shfaq = Convert.ToBoolean(vlera);
                    break;
            }
        }
        public static object[] ktheArtikullOseMakro2(string prefixText, int count, string contextKey, int klasa, HttpSessionState Session)
        {
            if (contextKey == "Artikull")
                return ktheArtikull2(prefixText, klasa, Session);
            else if (contextKey == "Makro")
                return ktheMakro(prefixText, Session);
            else if (contextKey == "Llogari")
                return ktheLlogari2(prefixText, Session);
            else if (contextKey == "Aktivitete")
                return ktheAktivitete2(prefixText, Session);
            else if (contextKey == "Burim")
                return ktheBurim2(prefixText, Session);
            else if (contextKey == "Qendra")
                return ktheQendra(prefixText, Session);
            else if (contextKey == "Skema")
                return ktheSkema(prefixText, Session);
            else if (contextKey == "Perdorues")
                return kthePerdorues(prefixText, Session);
            else if (contextKey == "Rol")
                return ktheRol(prefixText, Session);
            else return new List<string>().ToArray();
        }
        public static object[] ktheArtikull2(string prefixText, int klasa, HttpSessionState Session)
        {
            colArtikujt oColArtikujt = new colArtikujt();
            if (klasa == 4)// || klasa == 5 || klasa == 6
                oColArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeLikeJoPerbProdhim(mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session), prefixText);
            else oColArtikujt.merrSipasArtikujNdermarrjesAndAutorizimeLikePerProdhim(mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session), prefixText);
            object[] artikujt = new object[oColArtikujt.Count];
            int i = 0;
            foreach (clsArtikulli o in oColArtikujt)
            {
                artikujt[i] = o;
                i++;
            }
            return artikujt;
        }
        public static object[] ktheMakro(string prefixText, HttpSessionState Session)
        {
            colKokatMakro oCol = new colKokatMakro();
            oCol = new colKokatMakro(mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session), prefixText);
            return oCol.Select(x => new
            {
                x.KodiKokaMakro,
                x.PershkrimiKokaMakro

            }).ToArray();
        }
        public static object[] ktheLlogari2(string prefixText, HttpSessionState Session)
        {
            colLlogarite oColLlogari = new colLlogarite();
            oColLlogari = oColLlogari.merrLLogariteNdermarrjesAndAutorizimeLike(mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session), prefixText);
            object[] llogarite = new object[oColLlogari.Count];
            int i = 0;
            foreach (clsLlogari oLlogari in oColLlogari)
            {
                llogarite[i] = oLlogari;
                i++;
            }
            return llogarite;
        }
        public static object[] ktheAktivitete2(string prefixText, HttpSessionState Session)
        {
            colAktiviteteKoka aktivitete = new colAktiviteteKoka();
            aktivitete.mbushGjitheAktivitetetSipasNdermarjesLike(mySessionObjects.merrIdNdermarrjeSesioni(Session), prefixText);
            object[] akt = new object[aktivitete.Count];
            int i = 0;
            foreach (clsAktiviteteKoka aktivitet in aktivitete)
            {
                akt[i] = aktivitet;
                i++;
            }
            return akt;
        }
        public static object[] ktheBurim2(string prefixText, HttpSessionState Session)
        {
            colBurimet aktivitete = new colBurimet();
            aktivitete.mbushGjitheBurimeSipasNdermarjesAktive(mySessionObjects.merrIdNdermarrjeSesioni(Session));
            object[] akt = new object[aktivitete.Count];
            int i = 0;
            foreach (clsBurime aktivitet in aktivitete)
            {
                akt[i] = aktivitet;
                i++;
            }
            return akt;
        }
        public static object[] ktheQendra(string prefixText, HttpSessionState Session)
        {
            DbCore.DbQendraKosto.colQendraKosto col = new DbCore.DbQendraKosto.colQendraKosto();
            col.mbushGjitheQendraKostoBijSipasNdermarjesAktiv(mySessionObjects.merrIdNdermarrjeSesioni(Session));
            return col.ToArray();
        }
        public static object[] ktheSkema(string prefixText, HttpSessionState Session)
        {
            DbCore.DbQendraKosto.colKokaSkemaQK col = new DbCore.DbQendraKosto.colKokaSkemaQK();
            col.mbushGjitheSkematSipasNdermarjes(mySessionObjects.merrIdNdermarrjeSesioni(Session));
            return col.ToArray();
        }
        public static object[] kthePerdorues(string prefixText, HttpSessionState Session)
        {
            DbCore.DbAdmin.colPerdoruesit perdorues = new DbCore.DbAdmin.colPerdoruesit();
            int idLicence = DbCore.DbAdmin.clsLicenca.merrIdLicencePerdoruesi(mySessionObjects.ktheIdPerdoruesi(Session));
            perdorues.mbushGjithePerdoruesitLike(mySessionObjects.ktheIdPerdoruesi(Session), idLicence, prefixText);

            return perdorues.ToArray();
        }
        public static object[] ktheRol(string prefixText, HttpSessionState Session)
        {
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            int idLicence = DbCore.DbAdmin.clsLicenca.merrIdLicencePerdoruesi(idPerdoruesi);

            DbCore.DbAdmin.colRoli role = new DbCore.DbAdmin.colRoli();

            role.mbushRoletLike(idLicence, prefixText);
            return role.ToArray();

        }
        public static object[] kthePershkrimArtikulliP(string prefixText, int idPerdorues, HttpSessionState Session)
        {
            //clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            clsArtikulli art = new clsArtikulli();
            object[] result = new object[2];
            //if (dbInventari.ekzistonArtikull(prefixText, mySessionObjects.merrIdNdermarrjeSesioni(Session)))
            //{
            art.ktheArtikullSipasKoditDheAutorizime(prefixText, mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session));
            //dbInventari.Dispose();
            if (art.IdArtikulli > 0)
            {
                result[0] = art;
                if (art.IdMagazina > 0)
                    result[1] = clsNjesiAdministrative.eshteAktiveKaAutorizimeDheJoFshireNjesiAdministrative(art.IdMagazina, idPerdorues);
                else result[1] = null;
                return result;
            }
            else
            {
                result[0] = null;
                result[1] = null;
                return result;
            }
            //}
            //dbInventari.Dispose();
            //return result;
        }



        public static object[] ktheArtikujPerberesSipasDates(int id, string data, HttpSessionState Session)
        {

            object[] result = new object[4];
            colArtikulliPerberes col = new colArtikulliPerberes();
            colArtikujt colart = new colArtikujt();
            colAktiviteteKoka colakt = new colAktiviteteKoka();
            System.Collections.ArrayList kosto = new System.Collections.ArrayList();

            if (id != -1)
            {
                col.merrSipasIdArtikullKryesoreDates(id, DateTime.Parse(data));
            }
            foreach (clsArtikulliPerberes c in col)
            {
                if (c.Lloji == 1)
                {
                    clsArtikulli art = new clsArtikulli(c.IdLidheseArt);
                    colart.Add(art);
                    colakt.Add(new clsAktiviteteKoka());
                    clsTrupiMagazina mag = new clsTrupiMagazina();
                    kosto.Add(mag.llogaritCmimMesatar(art, 0, DateTime.Parse(data), -1, 1, mySessionObjects.ktheIdPerdoruesi(Session)));
                }
                else
                {
                    clsAktiviteteKoka akt = new clsAktiviteteKoka(c.IdLidheseAkt);
                    colakt.Add(akt);
                    colart.Add(new clsArtikulli());

                    kosto.Add(0);
                }
            }
            result[0] = col;
            result[1] = colart;
            result[2] = colakt;
            result[3] = kosto;

            return result;
        }
        public static bool KontrolloDetajimLidhur(int idartikulli)
        {
            return clsDetajimArtikulli.kontrolloDetajimLidhur(idartikulli);
        }

        public static object[] KontrolloDetajimLidhurSipasLlojit(int idartikulli, int lloji)
        {
            clsArtikulli art = new clsArtikulli(idartikulli);
            object[] result = new object[3];
            result[2] = lloji;
            if (lloji == 1)
                result[1] = art.IdKategoriDetajimi;
            else result[1] = art.IdKategoriDetajimi2;
            result[0] = clsDetajimArtikulli.KontrolloDetajimLidhurSipasLlojit(idartikulli, lloji);
            return result;
        }
        public static object ruajNeSessionURL(string url, HttpSessionState Session)
        {
            return mySessionObjects.ruajURLNeSesion(Session, url);
        }
        public static object[][] merrGjendje(string[] kodikf, DateTime datedok, int[] i, int iddok, string kodkonfigurim, HttpSessionState Session)
        {
            datedok = datedok.ToLocalTime();
            clsKonfigurimAmbjenti konf = new clsKonfigurimAmbjenti();
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            konf.mbushKonfiguriminMeKod(kodkonfigurim, idNdermarrje, mySessionObjects.ktheGjuhe(Session));
            clsGjendjeKlientFurnitor gjendje = new clsGjendjeKlientFurnitor();
            object[][] gj = new object[kodikf.Length][];


            for (int j = 0; j < kodikf.Length; j++)
            {
                gj[j] = new object[4];
                gj[j][0] = i[j];
                double[] gjend = gjendje.MerrGjendje(kodikf[j], idNdermarrje, datedok, iddok, konf.IdNivel);
                gj[j][1] = gjend[0];
                gj[j][2] = gjend[1];
                clsKlientFurnitor kf = new clsKlientFurnitor();
                kf.MbushKlientFurnitor(kodikf[j], idNdermarrje);
                if (kf.LlojiKF && gjend[0] >= 0)
                    gj[j][3] = 1;
                else if (kf.LlojiKF && gjend[0] < 0)
                {
                    gj[j][3] = 2;
                    gj[j][1] = -gjend[0];
                    gj[j][2] = -gjend[1];
                }
                else if (!kf.LlojiKF && gjend[0] >= 0)
                    gj[j][3] = 2;
                else
                {
                    gj[j][3] = 1;
                    gj[j][1] = -gjend[0];
                    gj[j][2] = -gjend[1];
                }
            }
            return gj;
        }
        public static string merrKursetMonedhaveSipasDates(DateTime datedok, bool pershkrimi, int llojkursi, HttpSessionState Session)
        {
            datedok = datedok.ToLocalTime();
            string kurset = "";
            DbCore.DbAdmin.colMonedhat mon = new DbCore.DbAdmin.colMonedhat();
            mon.mbushGjitheMonedhatPozitive(mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session));
            foreach (DbCore.DbAdmin.clsMonedha m in mon)
            {
                //DbCore.DbAdmin.clsKurset cls = new DbCore.DbAdmin.clsKurset();
                //cls = new DbCore.DbAdmin.clsKurset(m.IdMonedha, datedok);
                double kursi = DbCore.DbAdmin.clsKurset.merrKursinFunditPerMonedheDateDheLloj(m.IdMonedha, datedok, llojkursi);
                if (kursi != 0 && kursi != -1)
                {
                    if (pershkrimi)
                        kurset = kurset + m.PershkrimiMonedha + "|" + kursi + "||";
                    else kurset = kurset + m.KodiMonedha + "|" + kursi + "||";
                }
                else
                {
                    if (pershkrimi)
                        kurset = kurset + m.PershkrimiMonedha + "|1||";
                    else kurset = kurset + m.KodiMonedha + "|1||";
                }
            }
            return kurset;
        }
        public static bool eshteAzhornimVeprimiFunditKlientFurnitor(int idKf, DateTime dateSelektuar, string kodMonedhaKlFurn, HttpSessionState Session)
        {
            bool eshteAzhornim = false;
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha();
            mon.mbushMonedhenENdermarrjes(idNdermarrje);
            if (kodMonedhaKlFurn != mon.KodiMonedha)
            {
                eshteAzhornim = clsKlientFurnitor.EshteAzhornimVeprimiFunditKlientFurnitor(idKf, dateSelektuar, idNdermarrje);
                return eshteAzhornim;
            }
            else return true;
        }
        public static Object ktheRowVleraArtMeIDMeRecRes(int idja, int rreshti, DateTime data, string magazi, int magazina2, double sasia, string sasiplanrec, int idtrupiplanifikimi, string sasiburimi, HttpSessionState Session)
        {
            data = data.ToLocalTime();
            clsArtikulli artkryesor = new clsArtikulli(idja);
            if (artkryesor.IdArtikulli == 0)
                return new { rreshti = rreshti, artkryesor = artkryesor };
            int magazina = clsNjesiAdministrative.ktheIdMagazine(magazi, mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session));
            return ktheRowVleraArtikulli(artkryesor, rreshti, data, magazina, magazina2, sasiplanrec, Session, sasiburimi, sasia, idtrupiplanifikimi);
        }

        public static List<object> merrKostoMagazineKod(string recepturat, DateTime data, HttpSessionState Session)
        {
            List<object> result = new List<object>();
            List<Dictionary<string, object>> recepturatList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(recepturat);
            foreach (var recepture in recepturatList)
            {
                clsNjesiAdministrative njesi = new clsNjesiAdministrative(recepture["mag"].ToString(), mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session));

                clsArtikulli art = new clsArtikulli();
                art.mbushArtikull(Convert.ToInt32(recepture["idart"]));
                clsTrupiMagazina tr = new clsTrupiMagazina();
                double kosto = 0;
                if (Convert.ToInt32(recepture["idDok"]) != 0)
                {
                    clsTrupiMagazina trMag = clsTrupiMagazina.ktheTrupiMagazinaSipasReceptureProdhimi(Convert.ToInt32(recepture["idDok"]), art.IdArtikulli, njesi.IdNjesiAdministrative);                    
                    kosto = tr.llogaritCmimMesatar(art, njesi.IdNjesiAdministrative, data, -1, Convert.ToDouble(recepture["sasia"]), mySessionObjects.ktheIdPerdoruesi(Session), false, trMag.IdTrupiMagazina, trMag.IdRenditjes);
                }
                else
                    kosto = tr.llogaritCmimMesatar(art, njesi.IdNjesiAdministrative, data, -1, Convert.ToDouble(recepture["sasia"]), mySessionObjects.ktheIdPerdoruesi(Session));
            }
            return result;
        }

        public static int ktheMonedheNdermarrje(HttpSessionState Session)
        {
            int idmonedha = -1;
            //DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            //string monedha = DbCore.DbAdmin.clsMonedha.ktheMonedhenENdermarrjes(DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            DbCore.DbAdmin.clsMonedha clsMonedha = new DbCore.DbAdmin.clsMonedha();
            //clsMonedha.mbushMonedhen(monedha, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            clsMonedha.mbushMonedhenENdermarrjes(mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //DbCore.DbAdmin.colMonedhat colMonedhat = dbAdmin.ktheMonedhe(monedha, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (clsMonedha != null)
                idmonedha = clsMonedha.IdMonedha;
            return idmonedha;
        }
        public static string ktheFatureZhdoganuar(string[] idt)
        {
            string faturat = "";
            for (int i = 0; i < idt.Length; i++)
            {
                clsFleteDoganoreTrupi trup = new clsFleteDoganoreTrupi();
                bool zhdoganuar = trup.eshteZhdoganuarFatura(int.Parse(idt[i]));
                if (zhdoganuar)
                {
                    string nrdok = clsKokaShitje.ktheNrDok(int.Parse(idt[i]));
                    faturat += nrdok + ",";
                }
            }
            if (faturat.Split(',').Length > 2)
                return "Faturat " + faturat.Substring(0, faturat.Length - 1) + " jane te zhdoganuara!";
            else if (faturat.Split(',').Length == 2)
                return "Fatura " + faturat.Substring(0, faturat.Length - 1) + " eshte e zhdoganuar!";
            else return "";
        }
        public static object[] ktheFaturaPerFleteDoganoreobj(string[] pars)
        {
            object[] vlerat = new object[6];
            colKokaShitje colKokaSh = new colKokaShitje();
            clsKokaShitje oKokaSh = new clsKokaShitje();
            clsTrupiShitje oTrupiSh = new clsTrupiShitje();
            colTrupiShitje colTrupiSh = new colTrupiShitje();
            int k;
            int j;
            int idMonedha = -1;
            colKokaSh = new colKokaShitje();
            for (k = 0; k < pars.Length; k++)
            {
                if (pars[k].ToString() != "")
                {
                    oKokaSh = new clsKokaShitje(Convert.ToInt32(pars[k].ToString()));
                    oKokaSh.mbushTrupShitje();
                    colKokaSh.Add(oKokaSh);
                }
            }
            vlerat[0] = colKokaSh;
            List<colArtikujt> arti = new List<colArtikujt>();
            List<colLlogarite> llog = new List<colLlogarite>();
            List<colNjesiAdministrative> magazina = new List<colNjesiAdministrative>();
            List<colNjesiteArtikulli> njesi = new List<colNjesiteArtikulli>();
            colKlienteFurnitore colkf = new colKlienteFurnitore();
            if (colKokaSh.Count > 0)
            {
                idMonedha = colKokaSh[0].IdMonedha;
            }
            for (k = 0; k < colKokaSh.Count; k++)
            {
                colArtikujt colart = new colArtikujt();
                colLlogarite colllog = new colLlogarite();

                colNjesiAdministrative colmag = new colNjesiAdministrative();
                colNjesiteArtikulli colnjesi = new colNjesiteArtikulli();
                for (j = 0; j < colKokaSh[k].OColTrupiShitje.Count; j++)
                {
                    if (colKokaSh[k].OColTrupiShitje[j].IdLlojVeprimi == 1)
                    {
                        clsArtikulli art = new clsArtikulli(colKokaSh[k].OColTrupiShitje[j].IdKodi);
                        colart.Add(art);
                        colllog.Add(new clsLlogari());
                    }
                    else if (colKokaSh[k].OColTrupiShitje[j].IdLlojVeprimi == 3)
                    {
                        clsLlogari ll = new clsLlogari(colKokaSh[k].OColTrupiShitje[j].IdKodi);

                        colllog.Add(ll);
                        colart.Add(new clsArtikulli());
                    }
                    clsNjesiAdministrative mag = new clsNjesiAdministrative(colKokaSh[k].OColTrupiShitje[j].IdMagazina);
                    colmag.Add(mag);
                    clsNjesiArtikulli njesia = new clsNjesiArtikulli(colKokaSh[k].OColTrupiShitje[j].IdNjesia);
                    colnjesi.Add(njesia);
                }
                clsKlientFurnitor kf = new clsKlientFurnitor(colKokaSh[k].IdKlientFurnitor);
                arti.Add(colart);
                llog.Add(colllog);
                magazina.Add(colmag);
                njesi.Add(colnjesi);
                colkf.Add(kf);
            }
            vlerat[1] = arti;
            vlerat[2] = llog;
            vlerat[3] = magazina;
            vlerat[4] = njesi;
            vlerat[5] = colkf;

            return vlerat;
        }

        public static Object janeUrdherShitje(int[] ids, int[] idNivele)
        {
            colNivelRegjistrimi nivelet = new colNivelRegjistrimi();
            clsNivelRegjistrimi niv = new clsNivelRegjistrimi();
            niv.mbushNivelRegjistrimiSipasIdPaKonvertime(idNivele[0]);
            if (niv.Kodi == "USH")
                return new { janeUSH = true, idte = ids };
            else return new { janeUSH = false, idte = ids };
        }
        public static Object janeOferteShitje(int[] ids, int[] idNivele)
        {
            colNivelRegjistrimi nivelet = new colNivelRegjistrimi();
            clsNivelRegjistrimi niv = new clsNivelRegjistrimi();
            niv.mbushNivelRegjistrimiSipasIdPaKonvertime(idNivele[0]);
            if (niv.Kodi == "OSH")
                return new { janeOSH = true, idte = ids };
            else return new { janeOSH = false, idte = ids };
        }
        public static Object ktheVleratEShitjesPerFiskalizimin(int idKokaShitje)
        {
            string lnkFiskalizimi = WebConfigurationManager.AppSettings["urlFiskalizimiApp"];
            clsKokaShitje kokaShitje = new clsKokaShitje(idKokaShitje);
            clsNdermarrje ndermarrje = new clsNdermarrje(kokaShitje.IdNdermarrje);
            DateTimeOffset dtKrijimiPajisjeOffset = new DateTimeOffset(kokaShitje.DtKrijimiPajisje);
            string DateServeriOffset = clsKontrollePerFiskalizimin.ktheDatenEServeritOffset();
            DateTimeOffset dt = DateTimeOffset.Parse(DateServeriOffset);
            TimeSpan t = dt.Offset;
            DateTimeOffset sourceDate = new DateTimeOffset(Convert.ToDateTime(kokaShitje.DtDok.ToString().Split(' ')[0] + ' ' + dtKrijimiPajisjeOffset.UtcDateTime.ToString().Split(' ')[1]),
                         t);

            DateTimeOffset timezoneIShqiperise = TimeZoneInfo.ConvertTime(sourceDate,
                          TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time"));
            return new { iic = kokaShitje.IIC,niptNdermarrje=ndermarrje.NdermarrjeNipt,totali=Decimal.Parse(Math.Round(kokaShitje.TotaliMeZbritjeMeTVSH*kokaShitje.Kursi,2).ToString()),dtKrijimi= timezoneIShqiperise,linkFiskalizim = lnkFiskalizimi };
        }
        public static Object janeOferteBlerje(int[] ids, int[] idNivele)
        {
            colNivelRegjistrimi nivelet = new colNivelRegjistrimi();
            clsNivelRegjistrimi niv = new clsNivelRegjistrimi();
            niv.mbushNivelRegjistrimiSipasIdPaKonvertime(idNivele[0]);
            if (niv.Kodi == "OB")
                return new { janeOB = true, idte = ids };
            else return new { janeOB = false, idte = ids };
        }
        public static object[] ktheGjendjeVlefteSipasMagazines(int idja, int rreshti, DateTime data, int iddetajim, string magazina, int idndermarje)
        {
            data = data.ToLocalTime();
            //System.Threading.Thread.Sleep(5000);
            object[] result = new object[4];
            result[0] = rreshti;
            clsArtikulli artikulli = new clsArtikulli(idja);

            if (artikulli.IdArtikulli > 0)
            {

                result[1] = artikulli;

                int idmag = clsNjesiAdministrative.ktheIdMagazine(magazina, idndermarje);
                result[2] = clsTrupiMagazina.merrSasi(artikulli, idmag, data, iddetajim);
                result[3] = clsTrupiMagazina.ktheVleftenTotaleSipasArtikullitHPD(artikulli.IdArtikulli, idmag, data, int.MaxValue);
                return result;
            }
            else
            {
                result[1] = null;
                result[2] = 0;
                result[3] = 0;
                return result;
            }
        }
        public static int ktheIdKategoriDetajimi(string lloji, int idArt)
        {
            clsArtikulli artikull = new clsArtikulli();
            artikull.mbushArtikull(idArt);
            if (artikull.IdArtikulli != 0)
            {
                if (lloji == "1")
                    return artikull.IdKategoriDetajimi;
                else if (lloji == "2")
                    return artikull.IdKategoriDetajimi2;
                else return 0;
            }
            else return 0;
        }

        public static colKonfigPivotGridaTrupi ktheKonfigurimRaporti(int idGjuha, int idKonfigRaporti, int idModuli)
        {
            clsKonfigPivotGridaKoka konfigKoka;
            if (idKonfigRaporti != -1)
            {
                konfigKoka = new clsKonfigPivotGridaKoka(idGjuha, idKonfigRaporti);
                return konfigKoka.KonfigPivotGridaTrupi;
            }
            colKonfigPivotGridaTrupi konfigDefault = colKonfigPivotGridaTrupi.ktheKonfigShto(idGjuha, idModuli);

            return konfigDefault;
        }

        public static string eshteDetajimLidhur(string kodi, int idNdermarrje, HttpSessionState Session)
        {
            if (clsDetajimArtikulli.eshteDetajimLidhurMeArtikull(kodi, idNdermarrje))
                return "true";
            else return "false";
        }

        public static object ktheKonfigDB(int idKomp, string kodKonf, int idNdermarrje, string kodKontrollKlienti, int idKlienti, bool merrFormatKursi, int idGjuha, int idPerdoruesi, string llojVeprimi, DateTime? dateDok, DateTime? dateDokDefault)
        {
            return clsFunksione.ktheKonfigDB(idKomp, kodKonf, idNdermarrje, kodKontrollKlienti, idKlienti, merrFormatKursi, idGjuha, idPerdoruesi, llojVeprimi, dateDok, dateDokDefault);
        }

        public static object ruajFushaAnketa(string vlera, string fusha, int index, HttpSessionState Session)
        {
            if (vlera == "")
                return new clsMesazh(false, "Ndodhi nje gabim gjate konvertimit te vleres");

            DbCore.DbCRM.colTrupiAnketa trupavis = mySessionObjects.merrAnketaVisibleNgaSesioni(Session);
            switch (fusha)
            {

                case "cbDetyrueshem":
                    trupavis[index].Detyrueshme = Convert.ToBoolean(vlera);
                    break;
            }
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        public static object[] MerrKonfigurimExporti(int id, HttpSessionState Session)
        {
            object[] result = new object[3];
            DbCore.DbAdmin.clsKonfigExporti konf = new DbCore.DbAdmin.clsKonfigExporti(id);
            result[0] = konf;
            result[1] = DbCore.DbAdmin.colKokaFormatImporti.ktheFormatImportiSipasKategorise(mySessionObjects.merrIdNdermarrjeSesioni(Session), konf.Kategoria);
            DbCore.DbAdmin.colFiltraExporti col = new DbCore.DbAdmin.colFiltraExporti(konf.Formati, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            col.Insert(0, new DbCore.DbAdmin.clsFiltraExporti());
            result[2] = col;
            return result;
        }
        public static DbCore.DbAdmin.colFiltraExporti MerrFiltraExporti(int id, HttpSessionState Session)
        {
            DbCore.DbAdmin.colFiltraExporti col = new DbCore.DbAdmin.colFiltraExporti(id, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            col.Insert(0, new DbCore.DbAdmin.clsFiltraExporti());
            return col;
        }
        public static DbCore.DbAdmin.colKokaFormatImporti MerrFormatImportiSipasKategorise(int idkategoria, HttpSessionState Session)
        {
            return DbCore.DbAdmin.colKokaFormatImporti.ktheFormatImportiSipasKategorise(mySessionObjects.merrIdNdermarrjeSesioni(Session), idkategoria);
        }
        public static colKategoriNiveleDok MerrKategoritePerImport(int idNdermarrje, int idViti, int idPerdoruesi, string komponente)
        {
            colKategoriNiveleDok colKategori = new colKategoriNiveleDok();
            colKategori.Add(new clsKategoriNivelDok(0, "", 0, 0, false, 0, false));
            colKategori.merriTeGjitheKategoritePerImportSipasTeDrejtave(idNdermarrje, idViti, idPerdoruesi, komponente);
            return colKategori;
        }
        public static colFormatKonfigTrupi merrKonfigFormatNrTrupi(int idKokaFormatNr, string idPerdoruesi, string idNdermarrje)
        {
            colFormatKonfigTrupi trupFormati = new colFormatKonfigTrupi();
            DbCore.DbAdmin.colMonedhat colMonedha = new DbCore.DbAdmin.colMonedhat();
            colMonedha.mbushGjitheMonedhatAktive(int.Parse(idNdermarrje), int.Parse(idPerdoruesi));

            if (idKokaFormatNr <= 0)
            {
                for (int i = 0; i < colMonedha.Count; i++)
                {
                    clsFormatKonfigTrup trupi = new clsFormatKonfigTrup(0, 0, colMonedha[i].IdMonedha, clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, colMonedha[i].KodiMonedha, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                    trupFormati.Add(trupi);
                }
                return trupFormati;
            }
            else
            {
                colFormatKonfigTrupi trupKonfig = new colFormatKonfigTrupi();
                trupKonfig.mbushFormatTrupiSipasIdKoka(idKokaFormatNr);
                for (int i = 0; i < colMonedha.Count; i++)
                {
                    clsFormatKonfigTrup trup = trupKonfig.merrFormatSipasMonedhes(colMonedha[i].IdMonedha);
                    if (trup == null)
                        trup = new clsFormatKonfigTrup(0, 0, colMonedha[i].IdMonedha, clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, colMonedha[i].KodiMonedha, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
                    trupFormati.Add(trup);
                }
                return trupFormati;
            }
        }
        public static colProjektProdhimi MerrProjekteTeGjeneruara(int id, HttpSessionState Session)
        {
            //marrim koken e shitjes dhe trupin e saj sipas autorizimeve
            clsKokaShitje koka = new clsKokaShitje();
            koka.mbushKokaShitjeSipasIDPaTrup(id);
            koka.OColTrupiShitje = koka.merrTrupiShitjeDheAutorizime(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //marrim te dhenat per klientin
            clsKlientFurnitor kf = new clsKlientFurnitor(koka.IdKlientFurnitor);
            //marrim artikujt sipas kokes se shitjes
            colArtikujt artikujtPerGjenerim = new colArtikujt(id);
            colProjektProdhimi colprod = new colProjektProdhimi();
            //per cdo rresht te trupit kontrollojme nqs eshte artikull, eshte artikull me prodhim me porosi dhe ka mbetur sasi per tu gjeneruar. 
            //Nqs plotesohen keto kushte gjenerojme projekt prodhimin per kete artikull.
            //Si nr dokumenti per te qene unik marrim koka.NrDok + '-' + trup.Kodi + '-' + new Random().Next(100000) + colprod.Count
            // Ne fund therrasim metoden rekursive krijoProjektProdhimiTeNenProdukte per te marre projekt prodhimet per nenproduktet e ketij artikulli
            foreach (clsTrupiShitje trup in koka.OColTrupiShitje)
            {
                if (trup.IdLlojVeprimi == 1)
                {
                    clsArtikulli art = artikujtPerGjenerim.Find(x => x.IdArtikulli == trup.IdKodi);
                    string kodDetajim1 = "", kodDetajim2 = "";
                    if (trup.IdDetajimArt != 0)
                    {
                        clsDetajimArtikulli detajim1 = new clsDetajimArtikulli(trup.IdDetajimArt);
                        kodDetajim1 = detajim1.KodDetajimArtikulli;
                    }
                    if (trup.IdDetajimArt2 != 0)
                    {
                        clsDetajimArtikulli detajim2 = new clsDetajimArtikulli(trup.IdDetajimArt2);
                        kodDetajim2 = detajim2.KodDetajimArtikulli;
                    }
                    clsProjektProdhimi prod = new clsProjektProdhimi(colprod.Count + 1, koka.IdShitjeKoka, art.IdArtikulli, koka.NrDok + '-' + trup.Kodi + '-' + new Random().Next(100000) + colprod.Count, trup.Pershkrimi,
                                                                     trup.Sasimbetur, trup.Sasia, koka.DtDok, koka.IdKlientFurnitor, kf.EmertimiKF, koka.NrDok, trup.IdMagazina, trup.IdNjesia, art.KodArtikulli, kf.KodKlientFurnitor,
                                                                     trup.Gjeresi, trup.Gjatesi, trup.SasiPermasa, trup.Shenime, trup.IdShitjeTrupi, kodDetajim1, kodDetajim2);
                    if (art.ProdhimMePorosi && trup.Sasimbetur != 0)
                    {
                        colprod.Add(prod);
                    }
                    colprod.AddRange(colProjektProdhimi.krijoProjektProdhimiTeNenProdukte(prod));
                }
            }
            return colprod;
        }
        public static colKonfigurimAmbjenti merrLlojeNgaKategoria(int[] kat, HttpSessionState Session)
        {

            colKonfigurimAmbjenti col = new colKonfigurimAmbjenti();
            for (int i = 0; i < kat.Length; i++)
                col.mbushKonfigAmbjSipasIdKategori(kat[i], mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.ktheGjuhe(Session));
            return col;
        }
        public static String ktheKodPrindiGrupKF(string idja)
        {
            int id = 0;
            if (!String.IsNullOrEmpty(idja))
                id = int.Parse(idja);
            if (id == 0)
                return "";
            else
            {
                clsGrupeKF grupi = new clsGrupeKF(id);
                return new clsGrupeKF(grupi.IdPrindi).KodGrupi;
            }
        }
        public static object kaVeprimeGrupimi(int id)
        {
            return DbCore.DbListPagesat.clsGrupimeLocaleGlobale.kaVeprimeGrupime(id);
        }
        public static object[] MerrKonfigurimImporti(int id, HttpSessionState Session)
        {
            object[] result = new object[2];
            clsKonfigImporti konf = new clsKonfigImporti(id);
            result[0] = konf;
            result[1] = DbCore.DbAdmin.colKokaFormatImporti.ktheFormatImportiSipasKategorise(mySessionObjects.merrIdNdermarrjeSesioni(Session), konf.Kategoria);
            return result;
        }
        public static String ktheKodPrindiGrupArtikull(string idja)
        {
            int id = 0;
            if (!String.IsNullOrEmpty(idja))
                id = int.Parse(idja);
            if (id == 0)
                return "";
            else
            {
                clsKodifikimArtikulli kodifikim = new clsKodifikimArtikulli(id);
                return new clsKodifikimArtikulli(kodifikim.IdPrindi).KodKodifikimi;
            }
        }
        public static string kaVeprimeKodifikim(int iddokumenti, HttpSessionState Session)
        {
            string lidhur = "false";
            clsKodifikimArtikulli cls = new clsKodifikimArtikulli(iddokumenti);
            colKodifikimeArtikulli prind = new colKodifikimeArtikulli();
            prind.mbushKodifikimArtikulliSipasPrindit(iddokumenti);
            clsDatabaseInventari dbInventari = new clsDatabaseInventari();
            if (dbInventari.kaVeprimeKodifikimArtikulliPaStandart(iddokumenti) || prind.Count > 0 || (mySessionObjects.merrEshteMemeSesioni(Session) && clsKodifikimArtikulli.eshteTransferuarTekBij(cls.KodKodifikimi, cls.IdNdermarje, cls.LlojKodifikimi)))
            {
                lidhur = "true";
            }
            dbInventari.Dispose();
            return lidhur;
        }
        public static AutoCompleteItem[] ktheACListeKPFsh(string infixText, int idNdermarrje, int idPerdoruesi)
        {
            DataTable tmpTable = colKPFte.mbushGjitheKPFteSipasGrupitAndAutorizimeLikeNew(1, idNdermarrje, idPerdoruesi, infixText);
            tmpTable.Columns["des"].ColumnName = "desc";
            AutoCompleteItem[] autoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                autoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                autoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                autoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
            }
            return autoCompleteItem;
        }
        public static object[] eksitonLLogNeKetePrind(DbCore.DbKontabiliteti.colTrupPasqyreFinaciare trupat, string lloj, string prind, int index, string kod, string tip, string gjendje, int idndermarje)
        {
            object[] result = new object[2];
            colLlogariaTrupiPasqyres colGjitheLlogarite = new colLlogariaTrupiPasqyres();

            if (kod == "")
            {
                result[0] = false;
                result[1] = "";
                return result;
            }
            //selektohen te trupi i kesaj llogarie
            IEnumerable<DbCore.DbKontabiliteti.clsTrupPasqyreFinanciare> query = from s in trupat
                                                                                 where s.PershkrimiZerit == prind && s.LlojiZerit == lloj
                                                                                 select s;

            colLlogariaTrupiPasqyres colLlog = query.First<DbCore.DbKontabiliteti.clsTrupPasqyreFinanciare>().OColLlogarite;
            if (index < colLlog.Count)
                colLlog[index].IdLlogaria = 0;
            for (int i = 0; i < trupat.Count; i++)
            {
                colGjitheLlogarite.AddRange(trupat[i].OColLlogarite);
            }
            //selektohen llogarite ose kpf qe fillojne me kete kod ose mbarojne me ketekod
            IEnumerable<clsLlogariaTrupiPasqyres> queryllog = from s in colLlog
                                                              where s.Lloji == tip && (s.IdLlogaria.ToString().StartsWith(kod) || kod.StartsWith(s.IdLlogaria.ToString()))
                                                              select s;
            if (queryllog.Count<clsLlogariaTrupiPasqyres>() > 0)
            {
                if (tip == "Llogari")
                {
                    result[0] = true;
                    result[1] = String.Format("ekziston llogaria {0} ose nje llogari qe e permban ate ne kete ze. Ju lutem zgjidhni nje llogari tjeter!", kod);
                    return result;
                }
                else if (tip == "Llogari standarte")
                {
                    result[0] = true;
                    result[1] = String.Format("ekziston llogaria standarte {0}  ose nje llogari qe e permban ate ne kete ze. Ju lutem zgjidhni nje llogari tjeter!", kod);
                    return result;
                }
            }
            if (tip == "Llogari")
            {//kontrolohet nqs ka llogari standarte qe e permbajne kete llogari
                IEnumerable<clsLlogariaTrupiPasqyres> querykpf = from s in colLlog
                                                                 where s.Lloji == "Llogari standarte"
                                                                 select s;
                for (int i = 0; i < querykpf.Count<clsLlogariaTrupiPasqyres>(); i++)
                {
                    colLlogarite colLl = new colLlogarite();
                    colLl.mbushLlogariteNgaKPFLike(querykpf.ElementAt<clsLlogariaTrupiPasqyres>(i).IdLlogaria.ToString(), idndermarje);
                    IEnumerable<clsLlogari> queryll = from s in colLl
                                                      where (s.NrLlogari.StartsWith(kod) || kod.StartsWith(s.NrLlogari))
                                                      select s;
                    if (queryll.Count<clsLlogari>() > 0)
                    {
                        result[0] = true;
                        result[1] = String.Format("ekziston llogaria {0} si pjese e llogarive standarte ne kete ze. Ju lutem zgjidhni nje llogari tjeter!", kod);
                        return result;
                    }
                }
            }
            else
            {//kontrollohet nqs ka llogari qe jane pjese e kesaj llogarie standarte
                colLlogarite oColLlogari = new colLlogarite();
                oColLlogari.mbushLlogariteNgaKPFLike(kod, idndermarje);
                IEnumerable<clsLlogariaTrupiPasqyres> querykpf = from s in colLlog
                                                                 where s.Lloji == "Llogari"
                                                                 select s;
                for (int i = 0; i < querykpf.Count<clsLlogariaTrupiPasqyres>(); i++)
                {

                    IEnumerable<clsLlogari> queryll = from s in oColLlogari
                                                      where (s.NrLlogari.StartsWith(querykpf.ElementAt<clsLlogariaTrupiPasqyres>(i).IdLlogaria.ToString()) || querykpf.ElementAt<clsLlogariaTrupiPasqyres>(i).IdLlogaria.ToString().StartsWith(s.NrLlogari))
                                                      select s;
                    if (queryll.Count<clsLlogari>() > 0)
                    {
                        result[0] = true;
                        result[1] = String.Format("Eksistojne llogari te llogarise standarte {0} ne kete ze. Ju lutem zgjidhni nje llogari tjeter!", kod);
                        return result;
                    }
                }
            }
            //kontrollon te gjitha llogarite qe fillojne me kete kod dhe kontrollohen gjendjet
            IEnumerable<clsLlogariaTrupiPasqyres> querygjendje = from s in colGjitheLlogarite
                                                                 where s.Lloji == tip && (s.IdLlogaria.ToString().StartsWith(kod) || kod.StartsWith(s.IdLlogaria.ToString()))
                                                                 select s;

            for (int i = 0; i < querygjendje.Count<clsLlogariaTrupiPasqyres>(); i++)
            {
                clsLlogariaTrupiPasqyres llogtrupipas = querygjendje.ElementAt<clsLlogariaTrupiPasqyres>(i);
                if (llogtrupipas.Gjendja == gjendje)
                {
                    result[0] = true;
                    if (tip == "Llogari")
                        result[1] = String.Format("ekziston llogaria {0} me kete gjendje. Ju lutem zgjidhni nje llogari tjeter ose ndryshoni gjendjen!", kod);
                    else result[1] = String.Format("ekziston llogaria standarte {0} me kete gjendje. Ju lutem zgjidhni nje llogari tjeter ose ndryshoni gjendjen!", kod);
                    return result;
                }
                if (llogtrupipas.Gjendja == "Gjithmone")
                {
                    result[0] = true;
                    if (tip == "Llogari")
                        result[1] = String.Format("ekziston llogaria {0} me gjendjen Gjithmone. Ju nuk mund ta zgjidhni perseri ate!", kod);
                    else result[1] = String.Format("ekziston llogaria standarte {0} me gjendjen Gjithmone. Ju nuk mund ta zgjidhni perseri ate!", kod);
                    return result;
                }
                if (llogtrupipas.Gjendja == "Debi" && gjendje == "Gjithmone")
                {
                    result[0] = true;
                    if (tip == "Llogari")
                        result[1] = String.Format("ekziston llogaria {0} me gjendjen Debi. Ju nuk mund ta zgjidhni ate me gjendje Gjithmone!", kod);
                    else result[1] = String.Format("ekziston llogaria standarte {0} me gjendjen Debi. Ju nuk mund ta zgjidhni ate me gjendje Gjithmone!", kod);
                    return result;
                }
                if (llogtrupipas.Gjendja == "Kredi" && gjendje == "Gjithmone")
                {
                    result[0] = true;
                    if (tip == "Llogari") result[1] = String.Format("ekziston llogaria {0} me gjendjen Kredi. Ju nuk mund ta zgjidhni ate me gjendje Gjithmone!", kod);
                    else result[1] = String.Format("ekziston llogaria standarte {0} me gjendjen Kredi. Ju nuk mund ta zgjidhni ate me gjendje Gjithmone!", kod);
                    return result;
                }
            }
            if (tip == "Llogari")
            {//shikohet ne ka llogari standarte qe permbajne kete llogari dhe kontrollohet gjendja
                IEnumerable<clsLlogariaTrupiPasqyres> querygjendjeKPF = from s in colGjitheLlogarite
                                                                        where s.Lloji == "Llogari standarte"
                                                                        select s;

                for (int i = 0; i < querygjendjeKPF.Count<clsLlogariaTrupiPasqyres>(); i++)
                {
                    clsLlogariaTrupiPasqyres llogtrupipas = querygjendjeKPF.ElementAt<clsLlogariaTrupiPasqyres>(i);
                    colLlogarite colLl = new colLlogarite();
                    colLl.mbushLlogariteNgaKPFLike(llogtrupipas.IdLlogaria.ToString(), idndermarje);
                    IEnumerable<clsLlogari> queryll = from s in colLl
                                                      where (s.NrLlogari.StartsWith(kod) || kod.StartsWith(s.NrLlogari))
                                                      select s;
                    if (queryll.Count<clsLlogari>() > 0)
                    {
                        if (llogtrupipas.Gjendja == gjendje)
                        {
                            result[0] = true;
                            result[1] = String.Format("ekziston llogaria {0} si pjese e llogarive standarte me kete gjendje. Ju lutem zgjidhni nje llogari tjeter ose ndryshoni gjendjen!", kod);
                            return result;
                        }
                        if (llogtrupipas.Gjendja == "Gjithmone")
                        {
                            result[0] = true;
                            result[1] = String.Format("ekziston llogaria {0} si pjese e llogarive standarte me gjendjen Gjithmone. Ju nuk mund ta zgjidhni perseri ate!", kod);
                            return result;
                        }
                        if (llogtrupipas.Gjendja == "Debi" && gjendje == "Gjithmone")
                        {
                            result[0] = true;
                            result[1] = String.Format("ekziston llogaria {0} si pjese e llogarive standarte me gjendjen Debi. Ju nuk mund ta zgjidhni ate me gjendje Gjithmone!", kod);
                            return result;
                        }
                        if (llogtrupipas.Gjendja == "Kredi" && gjendje == "Gjithmone")
                        {
                            result[0] = true;
                            result[1] = String.Format("ekziston llogaria {0} si pjese e llogarive standarte me gjendjen Kredi. Ju nuk mund ta zgjidhni ate me gjendje Gjithmone!", kod);
                            return result;
                        }
                    }
                }
            }
            else
            {//kontrollohen nqs llogarite e kesaj llogarie standarte ndodhen ne llogarite e grides dhe kontrollohet gjendja
                colLlogarite oColLlogari = new colLlogarite();
                oColLlogari.mbushLlogariteNgaKPFLike(kod, idndermarje);
                IEnumerable<clsLlogariaTrupiPasqyres> querykpf = from s in colGjitheLlogarite
                                                                 where s.Lloji == "Llogari"
                                                                 select s;
                for (int i = 0; i < querykpf.Count<clsLlogariaTrupiPasqyres>(); i++)
                {
                    clsLlogariaTrupiPasqyres llogtrupipas = querykpf.ElementAt<clsLlogariaTrupiPasqyres>(i);

                    IEnumerable<clsLlogari> queryll = from s in oColLlogari
                                                      where (s.NrLlogari.StartsWith(querykpf.ElementAt<clsLlogariaTrupiPasqyres>(i).IdLlogaria.ToString()) || querykpf.ElementAt<clsLlogariaTrupiPasqyres>(i).IdLlogaria.ToString().StartsWith(s.NrLlogari))
                                                      select s;
                    if (queryll.Count<clsLlogari>() > 0)
                    {
                        if (llogtrupipas.Gjendja == gjendje)
                        {
                            result[0] = true;
                            result[1] = String.Format("Eksistojne llogari te llogarise standarte {0} me kete gjendje. Ju lutem zgjidhni nje llogari tjeter ose ndryshoni gjendjen!", kod);
                            return result;
                        }
                        if (llogtrupipas.Gjendja == "Gjithmone")
                        {
                            result[0] = true;
                            result[1] = String.Format("Eksistojne llogari te llogarise standarte  {0}  me gjendjen Gjithmone. Ju nuk mund ta zgjidhni perseri ate!", kod);
                            return result;
                        }
                        if (llogtrupipas.Gjendja == "Debi" && gjendje == "Gjithmone")
                        {
                            result[0] = true;
                            result[1] = String.Format("Eksistojne llogari te llogarise standarte {0}  me gjendjen Debi. Ju nuk mund ta zgjidhni ate me gjendje Gjithmone!", kod);
                            return result;
                        }
                        if (llogtrupipas.Gjendja == "Kredi" && gjendje == "Gjithmone")
                        {
                            result[0] = true;
                            result[1] = String.Format("Eksistojne llogari te llogarise standarte {0} me gjendjen Kredi. Ju nuk mund ta zgjidhni ate me gjendje Gjithmone!", kod);
                            return result;
                        }
                    }
                }
            }
            result[0] = false;
            result[1] = "";
            return result;
        }
        public static object[] ktheRowVleraKPF(int idja, int rreshti)
        {
            object[] result = new object[2];
            result[0] = rreshti;
            clsKPF kpf = new clsKPF(idja);
            if (kpf.IdKPF > 0)
            {
                result[1] = kpf;
                return result;
            }
            else
            {
                result[1] = null;
                return result;
            }
        }
        public static object[] ktheRowVleraKPFMeKod(string kodi, int rreshti, int idNdermarrje, int idPerdoruesi)
        {
            object[] result = new object[2];
            result[0] = rreshti;

            clsKPF kpf = new clsKPF(1, idNdermarrje, idPerdoruesi, kodi);
            if (kpf.IdKPF > 0)
            {
                result[1] = kpf;
                return result;
            }
            else
            {
                result[1] = null;
                return result;
            }
        }
        public static object[] KtheVleraLlogMeIDRow(int idja, int rreshti)
        {
            object[] result = new object[2];
            result[0] = rreshti;
            clsLlogari llogaria = new clsLlogari(idja);
            if (llogaria.IdLlogari > 0)
            {
                result[1] = llogaria;
                return result;
            }
            else
            {
                result[1] = null;
                return result;
            }
        }
        public static Object KtheVleraLlogarishMeIDRow(string idLlogarish, int rreshti)
        {
            List<int> idLlogarite = idLlogarish.Split(',').Select(int.Parse).ToList();
            colLlogarite llogarite = new colLlogarite(idLlogarite);
            return new { result = llogarite, rreshti = rreshti };
        }
        public static object[] ktheVleraLlogMeKodRow(string kodi, int rreshti, int idNdermarrje)
        {
            object[] result = new object[2];
            result[0] = rreshti;
            clsLlogari llogaria = new clsLlogari();
            llogaria.merrLlogariAktiveSipasKodit(kodi, idNdermarrje);
            if (llogaria.IdLlogari > 0)
            {
                result[1] = llogaria;
                return result;
            }
            else
            {
                result[1] = null;
                return result;
            }
        }
        public static object[] ktheIdLlogNgaNumri(string nrLlogInv, string nrLlogBle, string nrLlogShit, string nrLlogTret, string nrLlogShpenz, string nrLlogAmort, HttpSessionState Session)
        {
            object[] idte = new object[6];
            clsLlogari llogInv = new clsLlogari(nrLlogInv, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            idte[0] = llogInv.IdLlogari;
            clsLlogari llogBle = new clsLlogari(nrLlogBle, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            idte[1] = llogBle.IdLlogari;
            clsLlogari llogShit = new clsLlogari(nrLlogShit, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            idte[2] = llogShit.IdLlogari;
            clsLlogari llogTret = new clsLlogari(nrLlogTret, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            idte[3] = llogTret.IdLlogari;
            clsLlogari llogShpenz = new clsLlogari(nrLlogShpenz, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            idte[4] = llogShpenz.IdLlogari;
            clsLlogari llogAmort = new clsLlogari(nrLlogAmort, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            idte[5] = llogAmort.IdLlogari;

            return idte;
        }
        public static string ktheLlojeKurseshPerMonedhe(int id)
        {
            string kurset = "";
            DbCore.DbAdmin.colKurset colKurset = new DbCore.DbAdmin.colKurset();
            colKurset.mbushKursetMonedhes(id);
            //DbCore.DbAdmin.colKurset colKurset = new DbCore.DbAdmin.clsDatabaseAdmin().merrKursetMonedhes(id);
            foreach (DbCore.DbAdmin.clsKurset k in colKurset)
                kurset += k.PershkrimLlojKursi + "," + k.LlojKursi + ";";
            for (int i = colKurset.Count + 1; i <= 6; i++)
                kurset += "Kursi" + i + "," + i + ";";
            return kurset;
        }
        public static int ktheIdKushtiMinimumShitje(String vlera)
        {
            return clsAlternativaKushti.ktheIdPerVlerePerdorues(vlera);

        }

        public static int ktheIdKonfigAmbjentiSipasVleresSeKushtitMultiselectLupa(string vlera)
        {
            return clsAlternativaKushti.ktheIdKonfigAmbjentiSipasVleresSeKushtitMultiselectLupa(vlera);
        }

        public static colNivelRegjistrimi ktheNiveleSipasKat(string kategoria, int idNdermarrje, int idPerdoruesi)
        {
            colNivelRegjistrimi col = new colNivelRegjistrimi();
            clsKategoriNivelDok kat = new clsKategoriNivelDok(kategoria);
            col = clsNivelRegjistrimi.merrGjitheNivelRegjistrimiSipasKategori(idNdermarrje, idPerdoruesi, kat.IdKategori);
            return col;

        }
        public static string merrPershkrimMenyreMesazhi(int idmenyre, int idGjuha)
        {
            DbCore.DbQendraKosto.clsMenyreMesazhi menyre = new DbCore.DbQendraKosto.clsMenyreMesazhi();
            menyre.ktheMenyreSipasId(idmenyre, idGjuha);
            return menyre.Pershkrimi;

        }
        public static float merrKVpjesetimKMK(string monedhakryesor, string monedhalidhes, string date, double kursdoklidhes, int idNdermarrje)
        {
            if (monedhakryesor == monedhalidhes)
                return 1;
            DateTime dat = DateTime.Parse(date);
            clsMonedha mon = new DbCore.DbAdmin.clsMonedha();
            mon.mbushMonedhen(monedhakryesor, idNdermarrje);
            DbCore.DbAdmin.clsKurset kurs = new DbCore.DbAdmin.clsKurset(mon.IdMonedha, dat);
            if (kurs.VleraKursi == 0)
                kurs.VleraKursi = 1;
            return float.Parse((kursdoklidhes / kurs.VleraKursi).ToString());
        }
        public static object ktheLlogariSipasKodit(String kodi, int idNdermarja)
        {
            clsLlogari llog = new clsLlogari(kodi, idNdermarja);
            //llog.mbushLlogariSipasKodit(kodi, idNdermarja);
            return llog;
        }
        public static object[] ktheRowVleraAqtMeKodOseKodBar(string kodi, int rreshti, DateTime data, string magazina, int idndermarje, int idperdoruesi,  bool rezerva)
        {

            data = data.ToLocalTime();
            //System.Threading.Thread.Sleep(5000);
            object[] result = new object[3];
            result[0] = rreshti;

            clsArtikulli artikulli = new clsArtikulli();
            artikulli.ktheArtikullSipasKoditDheAutorizime(kodi, idndermarje, idperdoruesi);

            if (artikulli.IdArtikulli > 0)
            {
                result[1] = artikulli;


            }
            if (artikulli.IdArtikulli <= 0)
            {
                artikulli.merrSipasKodbarit(kodi, idndermarje);

                if (artikulli.IdArtikulli <= 0)
                {
                    artikulli.ktheArtikullSipasDetajimit(kodi, idndermarje);

                }

            }
            if (artikulli.IdArtikulli > 0)
            {
                clsNjesiAdministrative njesi = new clsNjesiAdministrative(magazina, idndermarje);

                double gjendjatot = 0;
                if (!rezerva) gjendjatot = clsSerialetMagazine.ktheSerialetMagazineGjendjeTotaleArtikulliNeMagazine(artikulli.IdArtikulli, idndermarje, njesi.IdNjesiAdministrative, data);
                else gjendjatot = clsAmortizimiTrupiRezerva.ktheGjendjeRezervaMagazine(artikulli.IdArtikulli, idndermarje, njesi.IdNjesiAdministrative, data);
                result[1] = artikulli;
                result[2] = gjendjatot;
                return result;
            }
            else
            {
                result[1] = null;
                result[2] = 0;
                return result;
            }

        }
        public static object[] ktheRowVleraAQTMeID(int idja, int rreshti, DateTime data, int idmagazina, int idndermarja, bool rezerva, bool plotesuarMagKoka)
        {
            data = data.ToLocalTime();
            //System.Threading.Thread.Sleep(5000);
            object[] result = new object[3];
            result[0] = rreshti;
            clsArtikulli artikulli = new clsArtikulli(idja);

            if (artikulli.IdArtikulli > 0)
            {
                double gjendjatot = 0;
                if (!rezerva)
                    gjendjatot = clsSerialetMagazine.ktheSerialetMagazineGjendjeTotaleArtikulliNeMagazine(artikulli.IdArtikulli, idndermarja ,(artikulli.IdMagazina != 0 && artikulli.IdMagazina != null && !plotesuarMagKoka) ? artikulli.IdMagazina : idmagazina, data);
                else
                    gjendjatot = clsAmortizimiTrupiRezerva.ktheGjendjeRezervaMagazine(artikulli.IdArtikulli, idndermarja, (artikulli.IdMagazina != 0 && artikulli.IdMagazina != null && !plotesuarMagKoka) ? artikulli.IdMagazina : idmagazina, data);
                result[1] = artikulli;
                result[2] = gjendjatot;
                return result;
            }
            else
            {
                result[1] = null;
                result[2] = 0;
                return result;
            }
        }
        public static object[] ktheVleraArtRez(int idja, int rreshti, DateTime data, int iddetajim, int idmag)
        {
            data = data.ToLocalTime();
            double sasitot, sasiRez, sasiUB;
            object[] result = new object[5];
            result[0] = rreshti;
            clsArtikulli artikulli = new clsArtikulli(idja);
            sasiUB = clsTrupiRezervime.merrSasiNgaUB(artikulli);
            if (idmag != 0)
            {
                sasitot = clsTrupiMagazina.merrSasi(artikulli, idmag, data, iddetajim);
                sasiRez = clsTrupiRezervime.merrSasi(artikulli.IdArtikulli, 0, idmag, data);         //todo gerta : kalo id e trupit            
            }
            else
            {
                sasitot = clsTrupiMagazina.merrSasi(artikulli, -1, data, iddetajim);
                sasiRez = clsTrupiRezervime.merrSasiGjitheMag(artikulli, 0, data);
            }

            if (artikulli.IdArtikulli > 0)
            {
                result[1] = artikulli;
                result[2] = sasitot;
                result[3] = sasiRez;
                result[4] = sasiUB;
                return result;
            }
            else
            {
                result[1] = null;
                result[2] = 0;
                result[3] = 0;
                result[4] = 0;
                return result;
            }
        }
        public static object[] mbushInfoArtikulliLidhurNew(int idja, string mag, DateTime data, string detajim, int rreshti, string modinfo, int idklient, HttpSessionState Session)
        {
            string idmag = "0";
            if (mag != "0")
            {
                clsNjesiAdministrative njesi = new clsNjesiAdministrative(mag, mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session));
                idmag = njesi.IdNjesiAdministrative.ToString();
            }
            //int iddetajim = -1;
            //if (detajim != "-1")
            //{
            //    clsDetajimArtikulli det = new clsDetajimArtikulli();
            //    det.mbushDetajimArtikulli(detajim, DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //    iddetajim = det.IdDetajimArtikulli;
            //}
            return mbushInfoArtikulliNew(idja, idmag, data, detajim, rreshti, modinfo, idklient, Session);
        }
        public static object[] mbushInfoArtikulliNew(int idja, string idmag, DateTime data, string detajim, int rreshti, string modinfo, int idklient, HttpSessionState Session)
        {
            int idndermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idperdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            colArtikulliPerberes artPerberes = new colArtikulliPerberes();
            data = data.ToLocalTime();
            object[] result = new object[3];
            result[0] = rreshti;
            DbCore.DbAdmin.clsInfoKoka info = new DbCore.DbAdmin.clsInfoKoka(int.Parse(modinfo));
            if (info.IdPeriudha == 0)
            {
                data = data.ToLocalTime();
            }
            else
            {
                DbCore.DbAdmin.clsViti viti = new DbCore.DbAdmin.clsViti();
                viti.mbushVitetMet(new DbCore.DbAdmin.clsNdermarrjeViti(mySessionObjects.ktheNdermarrjeVit(Session)).IdViti);
                data = viti.MbarimiViti;
            }
            int iddetajim = -1;
            if (detajim != "-1")
            {
                clsDetajimArtikulli det = new clsDetajimArtikulli();
                det.mbushDetajimArtikulli(detajim, idndermarrje);
                iddetajim = det.IdDetajimArtikulli;
            }
            DataRow rowklient = null;
            DataRow rowfurnitor = null;
            if (idklient != 0)
                rowklient = clsKokaShitje.ktheShitjenFunditteArtikullitKlient(idja, data, 1, idklient);
            rowfurnitor = clsKokaShitje.ktheShitjenFunditteArtikullitKlient(idja, data, 2, idklient);
            clsArtikulli artikulli = new clsArtikulli(idja);
            DbCore.DbAdmin.colInfoTrupi col = DbCore.DbAdmin.colInfoTrupi.merrInfoSipasIdKokaDheVisible(info.IdInfoKoka, true, idndermarrje);
            DataRow rowb = clsKokaShitje.ktheShitjenFunditteArtikullit(idja, data, 2);//blerja e fundit
            DataRow row = clsKokaShitje.ktheShitjenFunditteArtikullit(idja, data, 1);//shitja e fundit
            clsTrupiMagazina tr = new clsTrupiMagazina();
            double cm = 0; double sasi; double cmtot = 0; double sasitot;
            if (artikulli.Klasa == 4)
            {
                colArtikulliPerberes per = new colArtikulliPerberes();
                //per.merrSipasIdArtikullKryesore(artikulli.IdArtikulli, null);
                per.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(artikulli.IdArtikulli, data);
                foreach (clsArtikulliPerberes art in per)
                {
                    //clsArtikulli a = new clsArtikulli(art.IdLidheseArt, null);
                    clsArtikulli a = new clsArtikulli(art.IdLidheseArt);
                    cm += tr.llogaritCmimMesatar(a, int.Parse(idmag), data, iddetajim, 1, idperdoruesi) * (double)art.Koeficienti;
                    cmtot += tr.llogaritCmimMesatar(a, 0, data, iddetajim, 1, idperdoruesi) * (double)art.Koeficienti;
                }
                sasi = 0;
                sasitot = 0;
            }
            else
            {
                cm = tr.llogaritCmimMesatar(artikulli, int.Parse(idmag), data, iddetajim, 1, idperdoruesi);
                sasi = clsTrupiMagazina.merrSasi(artikulli, int.Parse(idmag), data, iddetajim);
                cmtot = tr.llogaritCmimMesatar(artikulli, 0, data, iddetajim, 1, idperdoruesi);
                sasitot = clsTrupiMagazina.merrSasi(artikulli, -1, data, iddetajim);
            }
            int Magzgjedhur = -1;
            double sasiamagzgjedhur;
            double cmmagzgjedhur = 0;
            int idKarta = 0;
            double limitSasi = 0;
          result[1] = col;
            System.Collections.ArrayList vlerat = new System.Collections.ArrayList();
            if (artikulli.IdArtikulli > 0)
                foreach (DbCore.DbAdmin.clsInfoTrupi trup in col)
                {
                    switch (trup.EmerKolone)
                    {
                        case "Periudha":
                            if (info.IdPeriudha == 0)
                                vlerat.Add("Data Fatures");
                            else vlerat.Add("Viti Ushtrimor");
                            break;
                        case "Njesia":
                            vlerat.Add(artikulli.KodNjesia1);
                            break;
                        case "Njesia2":
                            vlerat.Add(artikulli.KodNjesia2);
                            break;
                        case "SasiaMax":
                            vlerat.Add(artikulli.MaximumArtikulli.ToString("##########.##"));
                            break;
                        case "SasiaMin":
                            vlerat.Add(artikulli.MinimumArtikulli.ToString("##########.##"));
                            break;
                        case "DataBlerjesFundit":
                            if (rowb != null) vlerat.Add(DateTime.Parse(rowb[0].ToString()).ToShortDateString()); else vlerat.Add("");
                            break;
                        case "CmimiBlerjesFundit":
                            if (rowb != null) vlerat.Add(rowb[1]); else vlerat.Add("");
                            break;
                        case "CmimiShitjesseFundit/Klient":
                            if (rowklient != null)
                                vlerat.Add(Convert.ToDouble(rowklient[1]).ToString("##########.##"));
                            else
                                vlerat.Add("");
                            break;
                        case "CmimiNivelitBaze":

                            string cmimNivelBaze = clsArtikulli.ktheCmimArtikulliNivelBaze(artikulli.IdArtikulli, idndermarrje, 0);
                            vlerat.Add(cmimNivelBaze);
                            break;
                        case "CmimiBlerjesseFundit/Furnitor":
                            if (rowfurnitor != null)
                                vlerat.Add(Convert.ToDouble(rowfurnitor[1]).ToString("##########.##"));
                            else
                                vlerat.Add("");
                            break;
                        case "Sasianemagazinenezgjedhur":
                            if (Magzgjedhur == -1)
                                Magzgjedhur = Convert.ToInt32(idmag);
                            sasiamagzgjedhur = (artikulli.Klasa == 4) ? 0 : clsTrupiMagazina.merrSasi(artikulli, Magzgjedhur, data, -1);
                            if (sasiamagzgjedhur != 0)
                                vlerat.Add(sasiamagzgjedhur.ToString("##########.##"));
                            else vlerat.Add(sasiamagzgjedhur.ToString("##########.##"));
                            break;
                        case "Kostonemagazinenezgjedhur":
                            if (Magzgjedhur == -1)
                                Magzgjedhur = Convert.ToInt32(idmag);
                            if (artikulli.Klasa == 4)
                            {
                                if (artPerberes.Count == 0)
                                    artPerberes.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(artikulli.IdArtikulli, data);
                                foreach (clsArtikulliPerberes art in artPerberes)
                                {
                                    //int tmpMetodeKostoje = clsArtikulli.ktheMetodeKostoje(art.IdLidheseArt);
                                    //decimal koef = clsArtikulli.ktheKoeficent(art.IdLidheseArt);
                                    clsArtikulli receptura = new clsArtikulli(art.IdLidheseArt);
                                    cmmagzgjedhur += tr.llogaritCmimMesatar(receptura, Magzgjedhur, data, -1, 1, idperdoruesi) * (double)art.Koeficienti;
                                }
                            }
                            else
                                cmmagzgjedhur = tr.llogaritCmimMesatar(artikulli, Magzgjedhur, data, -1, 1, idperdoruesi);
                            if (cmmagzgjedhur != 0)
                                vlerat.Add(cmmagzgjedhur.ToString("##########.##"));
                            else vlerat.Add(cmmagzgjedhur.ToString("##########.##"));
                            break;
                        case "DataShitjesFundit":
                            if (row != null)
                                vlerat.Add(DateTime.Parse(row[0].ToString()).ToShortDateString());
                            else
                                vlerat.Add("");
                            break;
                        case "CmimiShtijesFundit":
                            if (row != null) vlerat.Add(row[1]);
                            else vlerat.Add("");
                            break;
                        case "SasiaTotale":
                            vlerat.Add(sasitot.ToString("##########.##"));
                            break;
                        case "KostoTotale":
                            vlerat.Add(cmtot.ToString("##########.##"));
                            break;
                        case "SasiPorositur":
                            vlerat.Add(clsTrupiShitje.merrSasiPorositur(artikulli.IdArtikulli).ToString("##########.##"));
                            break;
                        case "SasiRezervuar":
                            vlerat.Add(clsTrupiRezervime.merrSasiGjitheMag(artikulli, 0, data).ToString("##########.##"));
                            break;
                        case "Disponibel":
                            vlerat.Add((sasitot - clsTrupiRezervime.merrSasiGjitheMag(artikulli, 0, data)).ToString("##########.##"));
                            break;
                        case "SasiLirePorositur":
                            vlerat.Add((clsTrupiShitje.merrSasiPorositur(artikulli.IdArtikulli) - clsTrupiRezervime.merrSasiGjitheMag(artikulli, 0, data)).ToString("##########.##"));
                            break;
                        case "KodArtikulli":
                            vlerat.Add(artikulli.KodArtikulli);
                            break;
                        case "Pershkrimi2":
                            vlerat.Add(artikulli.PershkrimiAngArtikulli);
                            break;
                        case "PershkFurnitor":
                            vlerat.Add(artikulli.PershkrimFurnitori);
                            break;
                        case "VendodhjeArtikulli":
                            vlerat.Add(artikulli.VendodhjeArtikulli);
                            break;

                        case "LimitSasi":
                            if (idKarta != 0)
                                limitSasi = DbCore.DbRegjistrim.clsLimitKarta.MerrLimitSasi(artikulli.IdArtikulli, idKarta, data);
                            else limitSasi = 0;
                            vlerat.Add(limitSasi);
                            break;
                        default:
                            string[] split = { "  -  " };
                            string kodmag = trup.PershkrimKolone.Split(split, StringSplitOptions.RemoveEmptyEntries)[1];
                            clsNjesiAdministrative njesi = new clsNjesiAdministrative(kodmag, mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session));
                            if (njesi.IdNjesiAdministrative == -1)
                            {
                                vlerat.Add("jo autorizim");
                                break;
                            }
                            double cmmag = 0; double sasimag; double sasiarez; double sasiadisp; double sasiarezUB; double sasiaporUB; double sasialire;
                            if (artikulli.Klasa == 4)
                            {
                                colArtikulliPerberes per = new colArtikulliPerberes();
                                per.ktheArtikujPerberesSipasIdArtikullitKryesorDheDatesMeTeAfert(artikulli.IdArtikulli, data);
                                foreach (clsArtikulliPerberes art in per)
                                {
                                    clsArtikulli a = new clsArtikulli(art.IdLidheseArt);
                                    cmmag += tr.llogaritCmimMesatar(a, njesi.IdNjesiAdministrative, data, iddetajim, 1, idperdoruesi) * (double)art.Koeficienti;
                                }
                                sasimag = 0;
                                sasiarez = 0;
                                sasiadisp = 0;
                                sasiarezUB = 0;
                                sasiaporUB = 0;
                                sasialire = 0;
                            }
                            else
                            {
                                cmmag = tr.llogaritCmimMesatar(artikulli, njesi.IdNjesiAdministrative, data, iddetajim, 1, idperdoruesi);
                                sasimag = clsTrupiMagazina.merrSasi(artikulli, njesi.IdNjesiAdministrative, data, iddetajim);
                                sasiarez = clsTrupiRezervime.merrSasi(artikulli.IdArtikulli, 0, njesi.IdNjesiAdministrative, data);
                                sasiadisp = sasimag - sasiarez;
                                sasiarezUB = clsTrupiRezervime.merrSasiUB(artikulli.IdArtikulli, 0, njesi.IdNjesiAdministrative, data);
                                sasiaporUB = clsTrupiShitje.merrSasiPorositurSipasMagazines(artikulli.IdArtikulli, njesi.IdNjesiAdministrative);
                                sasialire = sasiaporUB - sasiarezUB;
                            }


                            if (trup.PershkrimKolone.Contains("Sasia"))
                            {
                                if (trup.PershkrimKolone.Contains("Sasia e rezervuar UB"))
                                    vlerat.Add(sasiarezUB.ToString("##########.##"));
                                else if (trup.PershkrimKolone.Contains("Sasia e rezervuar"))
                                    vlerat.Add(sasiarez.ToString("##########.##"));
                                else if (trup.PershkrimKolone.Contains("Sasia Disponibel"))
                                    vlerat.Add(sasiadisp.ToString("##########.##"));
                                else if (trup.PershkrimKolone.Contains("Sasia e porositur"))
                                    vlerat.Add(sasiaporUB.ToString("##########.##"));
                                else if (trup.PershkrimKolone.Contains("Sasia e lire e porositur"))
                                    vlerat.Add(sasialire.ToString("##########.##"));
                                else
                                    vlerat.Add(sasimag.ToString("##########.##"));
                            }
                            else vlerat.Add(cmmag.ToString("##########.##"));



                            break;
                    }
                }
            result[2] = vlerat;
            return result;
        }
        public static object callWsGetAutorizimeArtikulli(int idArtikulli)
        {
            return new { idArtikulli = idArtikulli, autorizime = DbCore.DbAdmin.colAutorizimetKoka.merrAutorizimeArt(idArtikulli) };
        }
        public static string ktheKodArtikulliSipasId(int idArtikulli)
        {
            return clsArtikulli.ktheKodArtikulliSipasId(idArtikulli);
        }
        public static object ktheKlientFurnitorVeprimeKFMeIDRow(int IDkf, int rreshti, DateTime date, int llojKursi, HttpSessionState Session)
        {
            date = date.ToLocalTime();
            clsKlientFurnitor oClsKF = new clsKlientFurnitor();
            double kurs = 0;
            DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha();
            if (IDkf == 0)
                return new { idRreshti = rreshti, oKF = oClsKF, monedha = mon, kursi = kurs };
            //string klienti = "";
            oClsKF = new clsKlientFurnitor(IDkf);
            if (oClsKF.KodKlientFurnitor == "")
                return new { idRreshti = rreshti, oKF = oClsKF, monedha = mon, kursi = kurs };
            oClsKF.mbushKlientFurnitorSipasKoditAzhornim(oClsKF.KodKlientFurnitor, mySessionObjects.merrIdNdermarrjeSesioni(Session), date);
            //string debikredi = "Debi";
            clsLlogari llog = new clsLlogari(oClsKF.IdLlogari);
            mon = new DbCore.DbAdmin.clsMonedha(llog.IdMonedha);
            kurs = DbCore.DbAdmin.clsKurset.merrKursinEFundit(mon.IdMonedha, llojKursi);
            return new { idRreshti = rreshti, oKF = oClsKF, monedha = mon, kursi = kurs };
        }

        public static Object ktheDisaKlientFurnitorVeprimeKFMeIDRow(string IDteKf, int rreshti, DateTime date, int llojKursi, HttpSessionState Session)
        {
            date = date.ToLocalTime();
            DataTable dt = clsKlientFurnitor.ktheDTKlientFurnitorSipasIdAzhornim(IDteKf.Substring(0, IDteKf.Length - 1), mySessionObjects.merrIdNdermarrjeSesioni(Session), date);
            
            return new { result = dt, idRreshti = rreshti };
        }
        public static object callWsGetAutorizimeSipasLlojitDheIdLidhese(int idLidhese, string kodLloji,int idPerdorues)
        {
            return new { idLidhese = idLidhese, autorizime = DbCore.DbAdmin.colAutorizimetKoka.merrAutorizimeSipasIdLidheseDheLloj(idLidhese, kodLloji, idPerdorues) };
        }
        public static bool ktheVlerenEShfaqesSeEinvoice(int idBanka)
        {
            return new clsBanka(idBanka).ShfaqNeEinvoice;
        }
        public static object merrTipinEPerjashtimit(int idTakse)
        {
            return new { tipiIPerjashtimit = new clsTaksa(idTakse).TipiIPerjashtimit};
        }
        public static object merrKodNjesieBiznesiDegeAdministrative(int idNdermarrje, string kodi)
        {
            return new { kodNjesieBiznesi = new clsDegeAdministrative(kodi, idNdermarrje).KodNjesieBiznesi };
        }
        public static object merrTipiMagDheQyteti(int idNjesi)
        {
            var njesiAd = new clsNjesiAdministrative(idNjesi);
            return new {Tipimag = njesiAd.TipiMag , qyteti = njesiAd.Qyteti };
        }
        public static object ktheKlientFurnitorVeprimeKFRow(string kodi, int rreshti, DateTime data, HttpSessionState Session)
        {
            data = data.ToLocalTime();

            clsKlientFurnitor oClsKF = new clsKlientFurnitor();
            double kurs = 0;
            DbCore.DbAdmin.clsMonedha mon = new DbCore.DbAdmin.clsMonedha();
            if (kodi == null || kodi == "")
                return new { idRreshti = rreshti, oKF = oClsKF, monedha = mon, kursi = kurs };

            oClsKF.mbushKlientFurnitorSipasKodit(kodi, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            //oColKF = dbKontab.ktheKlientFurnitor(prefixText,DbCore.mySessionObjects.merrIdNdermarrjeSesioni(Session));      

            if (oClsKF.KodKlientFurnitor == null)
                return new { idRreshti = rreshti, oKF = oClsKF, monedha = mon, kursi = kurs };
            oClsKF.mbushKlientFurnitorSipasKoditAzhornim(oClsKF.KodKlientFurnitor, mySessionObjects.merrIdNdermarrjeSesioni(Session), data);
            //string debikredi = "Debi";
            clsLlogari llog = new clsLlogari(oClsKF.IdLlogari);
            mon = new DbCore.DbAdmin.clsMonedha(llog.IdMonedha);

            //if (oClsKF.LlojiKF == false)
            //    debikredi = "Kredi";                      
            kurs = DbCore.DbAdmin.clsKurset.merrKursinEFundit(mon.IdMonedha, 1);
            return new { idRreshti = rreshti, oKF = oClsKF, monedha = mon, kursi = kurs };
            //object[] veprimeKf = new object[3];
            //veprimeKf[0] = oClsKF;
            //veprimeKf[1] = mon;
            //veprimeKf[2] = kurs;

            //return veprimeKf;
        }
        public static object merrQendraKosto(int lloji, HttpSessionState Session)
        {
            object result;
            if (lloji == 1)
            {
                DbCore.DbQendraKosto.colQendraKosto col = new DbCore.DbQendraKosto.colQendraKosto();

                col.mbushGjitheQendraKostoBijSipasNdermarjesAktiv(mySessionObjects.merrIdNdermarrjeSesioni(Session));
                result = col;
            }
            else
            {
                DbCore.DbQendraKosto.colKokaSkemaQK col = new DbCore.DbQendraKosto.colKokaSkemaQK();

                col.mbushGjitheSkematSipasNdermarjes(mySessionObjects.merrIdNdermarrjeSesioni(Session));
                result = col;
            }
            return result;
        }
        public static object merrInfoComboLlogariByID(int idLlogari, string kodKontrolli)
        {
            clsLlogari llogaria = new clsLlogari(idLlogari);
            if (llogaria.IdLlogari != 0)
                return new { kodKontrolli = kodKontrolli, idLlogari = idLlogari, kodLlogari = llogaria.NrLlogari };
            return null;
        }
        public static object merrInfoComboLlogariByKod(string kodLlogari, string kodKontrolli, HttpSessionState Session)
        {
            clsLlogari llogaria = new clsLlogari(kodLlogari, mySessionObjects.merrIdNdermarrjeSesioni(Session));
            if (llogaria.IdLlogari != 0)
                return new { kodKontrolli = kodKontrolli, idLlogari = llogaria.IdLlogari, kodLlogari = llogaria.NrLlogari };
            return null;
        }
        public static object merrInfoComboSkemaArtikulliByID(int idSkema, string kodKontrolli)
        {
            //clsLlogari llogaria = new clsLlogari(idLlogari);
            clsSkemaKontabilitetiArtikulli skema = new clsSkemaKontabilitetiArtikulli(idSkema);
            if (skema.IdSkemaKontabilitetiArtikulli != 0)
                return new
                {
                    kodKontrolli = kodKontrolli,
                    IdSkemaKontabilitetiArtikulli = skema.IdSkemaKontabilitetiArtikulli,
                    KodiSkemaKontabilitetiArtikulli = skema.KodiSkemaKontabilitetiArtikulli
                };
            return null;
        }
        public static int MerrIdKlientiByTakimi(int idTakimi)
        {
            var sked = new DbCore.DbCRM.clsSkeduler(idTakimi);
            if (sked.Lloji == 1)
                return sked.IdKlienti;
            //nese eshte detyre
            return -1;
        }
        public static int ktheIdSuperKategori(string idKategoria)
        {
            return clsKategoriNivelDok.mbushIDSuperKategoriNivDok(int.Parse(idKategoria));
        }
        public static colMarreveshjetPerKlient ktheMarreveshjeKlienti(int idKlienti)
        {

            colMarreveshjetPerKlient marreveshjet = new colMarreveshjetPerKlient();
            marreveshjet.MbushVetemMarreveshjetEKlientit(idKlienti);
            return marreveshjet;
        }

        public static List<object> ktheTeDhenaFaturashSipasId(List<int> idFaturash)
        {
            List<object> teDhenat = new List<object>();

            return teDhenat;
        }
        public static string ktheTeDhenaFaturashSipasId1(string faturat, int idKatDok, int idPerdoruesi, int idNdermarrje, int idDokumenti, int idNiveli, int idkonfigurim)
        {
            DataTable dtFaturat = (DataTable)JsonConvert.DeserializeObject(faturat, (typeof(DataTable)));
            return JsonConvert.SerializeObject(colDokumentat.mbushKokaShitjePaLikuiduarSipasFaturave(idNdermarrje, idKatDok, idPerdoruesi, dtFaturat, idDokumenti, idNiveli,idkonfigurim));
        }
        public static object[] kontrolloAnulluar(object[] id)
        {
            object[] result = new object[4];

            int idkonfiganullimi = clsKusht.kthevlereSipasKushtitDheIdKonfig(int.Parse(id[1].ToString()), "KVDN");
            result[0] = clsVeprimBankaKoka.kontrolloAnulluar(int.Parse(id[0].ToString())) ? true : false;
            result[1] = idkonfiganullimi == 0 ? false : true;
            result[2] = "ShtoVeprimBanka.aspx?lloji=pagese&shtim_modifikim=anullim&id=0&idkonfigurimi=" + idkonfiganullimi + "&idanullimi=" + id[0];
            result[3] = "Jeni i sigurte per te vazhduar anullimin e arketimit me vlere " + id[2] + " lek, ekzekutuar ne daten " + ((DateTime)id[3]).ToLocalTime().ToShortDateString() + " per MSISDN " + id[4] + "!";

            return result;
        }

        public static void ruajFileImportiCache(string idFile, HttpPostedFile f, HttpSessionState Session, string llojFile)
        {
            var dic = CacheLayer.GlobalCacheManager.MySessionCache.Get<Dictionary<string, HttpPostedFile>>(llojFile);
            if (dic == null)
            {
                Dictionary<string, HttpPostedFile> dict2 = new Dictionary<string, HttpPostedFile>();
                dict2.Add(idFile, f);
                CacheLayer.GlobalCacheManager.MySessionCache.Add(llojFile, dict2);
            }
            else
            {
                dic.Add(idFile, f);
                CacheLayer.GlobalCacheManager.MySessionCache.Add(llojFile, dic);
            }
        }
        public static void fshiFileImportiCache(string idFile, string llojFile, HttpSessionState Session)
        {
            var dic = CacheLayer.GlobalCacheManager.MySessionCache.Get<Dictionary<string, HttpPostedFile>>(llojFile);
            if (dic == null)
            {
                throw new MyException("Skedari eshte Fshire!");
            }
            else
            {
                dic.Remove(idFile);
                CacheLayer.GlobalCacheManager.MySessionCache.Add(llojFile, dic);
            }
        }


        public static object merrTeDhenaKonfigurimiVeprimeBanka(int idKomp, int idKonfigurimi, string kodKonf, string kodKontrolli, int idObjekti, bool shtim, bool merrFormatKursi, bool merrGjitheKonf, int idGjuha, HttpSessionState Session, int idNdermarrje, int idPerdoruesi, string status, int idkokashitje, int idlloji, int rreshti, DateTime data, int idArkaBankaLocalStorage, int idArkaBankaFillestareLocalStorage, string EmertimiKF, bool LlojiKf, DateTime dataKurs)
        {
            //konfigurimi default
            var konfig = KonfigurimeRepository.ktheKonfigAmbjentiMeFormatNumrash(idKomp, kodKonf, kodKontrolli, idObjekti, shtim, merrFormatKursi, merrGjitheKonf, idGjuha, null, idPerdoruesi, idNdermarrje);
            //grupimi i dokumentave
            var grupimDok = colGrupimDokumentiKoka.ktheGrupimDokumentashNderm(kodKonf, idNdermarrje, idPerdoruesi);

            var colKushte = clsFunksione.merrProperty<colKusht>(konfig, "colKushte");
            var colAlterKushti = clsFunksione.merrProperty<colAlternativatKushti>(konfig, "colAlterKusht");
            var colKontrolle = clsFunksione.merrProperty<colKontrolle>(konfig, "colKontroll");
            var colAtrTrupi = clsFunksione.merrProperty<colAtributeTrupi>(konfig, "colAtrTrupi");

            //menuja ne lidhje me skemen e aprovimit
            var skema = clsFunksione.merrMenu(idPerdoruesi, colKushte.Find(kusht => kusht.Kodi == "ZSP")?.Vlera ?? 0, shtim, status, idkokashitje, kodKonf, idlloji);

            //gjetja e subjektit default
            var altLSD = colAlterKushti.Find(alt => alt.IdKushti == colKushte.Find(kusht => kusht.Kodi == "LSD").IdKusht);
            var subjektiDefault = colKushte.Find(kusht => kusht.Kodi == "SD").Vlera;

            string vlereKursi = colAtrTrupi.Find(atr => atr.IdKontroll == colKontrolle.Find(kontroll => kontroll.KodKontrolli == "kursi_TextBox").IdKontrolli).VlereDefault;
            int kursi = string.IsNullOrWhiteSpace(vlereKursi) ? 1 : Convert.ToInt32(vlereKursi);

            object klientFurnitor;
            object llogari;
            object punonjes;
            switch (altLSD.Alternativa)
            {
                case "Llogari":
                    llogari = KtheVleraLlogMeIDRow(subjektiDefault, rreshti);
                    klientFurnitor = false;
                    punonjes = false;
                    break;
                case "Punonjes":
                    punonjes = ListPagesaRepository.KthePunonjesitMeIdRow(subjektiDefault.ToString(), rreshti);
                    llogari = false;
                    klientFurnitor = false;
                    break;
                case "Klient":
                case "Furnitor":
                    klientFurnitor = ktheKlientFurnitorVeprimeKFMeIDRow(subjektiDefault, rreshti, data, kursi, Session);
                    llogari = false;
                    punonjes = false;
                    break;
                default:
                    llogari = false;
                    klientFurnitor = false;
                    punonjes = false;
                    break;
            }
            
            //Webservisi per kursin 
            var ruajLocalStorage = (colAlterKushti.Find(alt => alt.IdKushti == colKushte.Find(kusht => kusht.Kodi == "RVF").IdKusht)).Alternativa == "Po";
            int idArkaBankaFinale = shtim && idArkaBankaFillestareLocalStorage <= 0 ? idObjekti : idArkaBankaFillestareLocalStorage;
            if (ruajLocalStorage && idArkaBankaLocalStorage > 0 && idArkaBankaFinale > 0)
                idArkaBankaFinale = idArkaBankaLocalStorage;
            if (idArkaBankaFinale <= 0) //nqs nuk vendoset nga localstorage, duhet te marrim arken/banken e konfigurimit
            {
                string bankaDef = colAtrTrupi.Find(atr => atr.IdKontroll == colKontrolle.Find(kontroll => kontroll.KodKontrolli == "banka_ComboBox").IdKontrolli).VlereDefault;
                idArkaBankaFinale = string.IsNullOrWhiteSpace(bankaDef) ? -1 : Convert.ToInt32(bankaDef);
            }
            object arkaBanka;
            if (idArkaBankaFinale <= 0)
                arkaBanka = false;
            else
                arkaBanka = merrKursinMonedhenGjendjenSipasBankesAndDates(idArkaBankaFinale, Convert.ToString(data), idKonfigurimi, kursi, idPerdoruesi, idNdermarrje);

            //gjetja e klient furnitorit
            string furnitoriDefault = colAtrTrupi.Find(atr => atr.IdKontroll == colKontrolle.Find(kontroll => kontroll.KodKontrolli == "furnitori_ComboBox").IdKontrolli).VlereDefault;
            object furnitori;
            string kodKf = shtim ? furnitoriDefault : EmertimiKF;



            if (!shtim && !string.IsNullOrEmpty(kodKf))
                furnitori = ktheEmerKlientFurnitorNew(EmertimiKF, LlojiKf, Session);
            else if (shtim && !string.IsNullOrEmpty(furnitoriDefault))
            {
                clsKlientFurnitor oClsKF = new clsKlientFurnitor(Convert.ToInt32(furnitoriDefault));
                furnitori = ktheEmerKlientFurnitorNew(oClsKF, LlojiKf, Session);
            }
            else
                furnitori = false;
            //WebServisi per kurset e monedhave te vet ndermarrjes 
            int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
            var mon = new colMonedhat();
            mon.mbushGjitheMonedhat(idNdermarrje, idPerdorues);
            var monedhatkurs = new string[mon.Count];
            var i = 0;
            foreach (var m in mon)
            {
                var kurs = new clsKurset(m.IdMonedha, data, idKonfigurimi, idNdermarrje);
                if (kurs.VleraKursi == 0)
                    kurs.VleraKursi = 1;
                monedhatkurs[i] = m.IdMonedha + ";" + m.KodiMonedha + ";" + kurs.VleraKursi;
                i++;
            }
            var kasePerdoruesi = new clsPerdorues(idPerdoruesi).IdKonfigKasa;
            return new
            {
                konfig = konfig,
                grupimDok = grupimDok,
                skema = skema,
                arkaBanka = arkaBanka,
                furnitori = furnitori,
                klientFurnitor = klientFurnitor,
                llogari = llogari,
                punonjes = punonjes,
                monedhatkurs = monedhatkurs,
                KasePerdoruesi = kasePerdoruesi
            };
        }
        public static bool NdryshoMesazhEinvoice(string eics, string statusi, int idNdermarrje)
        {
            return clsFunksioneFiskalizimi.ndryshoStatusinEinvoice(eics, statusi, idNdermarrje);
        }
        public static string[] merrEinvoiceEIC(string eic, clsNdermarrje nderm)
        {
            var fatura = clsFunksioneFiskalizimi.merrEinvoice(nderm, eic, DateTime.UtcNow);
            return clsFunksioneFiskalizimi.InvokeService(fatura, "Pdf", true);
            
        }
        public static Object ktheAplikohetTVSHNeTakseApoJo(string kodTakse, int idNderm, int key)
        {
            return new { aplikohet = clsTaksa.ktheAplikohetTVSHNeTakseApoJo(kodTakse, idNderm), key = key };
        }

        public static object FshiDokument(HttpSessionState Session, int[] ids, string komponente, string guidString, string komponShitje_blerje, string komponPerTedrejtat)
        {
            CultureInfo cultinf = mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            List<clsKokaShitje> teFshire = new List<clsKokaShitje>();
            List<string> teLidhur = new List<string>(),
                periudheKycur = new List<string>(),
                gjendjeNegative = new List<string>(),
                procesAprovimi = new List<string>(),
                tePaFshire = new List<string>(),
                rivleresim = new List<string>(),
                tePaAutorizuar = new List<string>(),
                closedPeriod = new List<string>(),
                fiskalizim = new List<string>();
            colTrupiMagazina trupat = new colTrupiMagazina();
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
            colTrupiMagazina tr;

            Dictionary<string, List<string>> paTeDrejta = new Dictionary<string, List<string>>();
            clsTeDrejtaRoli teDrejtaInfo = new clsTeDrejtaRoli();
            colNivelRegjistrimi niveleRregjistrimi = new colNivelRegjistrimi();
            object[] objektiIPlote = new object[ids.Length];
            int counter = 0;
            foreach (int id in ids)
            {
                clsKokaShitje clsKoka = new clsKokaShitje();
                clsKoka.mbushKokaShitjeSipasIDPaTrup(Convert.ToInt32(id));
                object[] koka = clsKoka.krijoObjektPerWebhook(clsKoka, "Fshirje", "Shitje");
                colTrupiShitje trupi = clsKokaShitje.merrTrupiShitje(clsKoka.IdShitjeKoka);
                var trupiShitje = trupi.ToList();
                JObject[] json = new JObject[trupiShitje.Count];
                int counter2 = 0;
                foreach (var t in trupiShitje)
                {

                    json[counter2] = JObject.Parse(JsonConvert.SerializeObject(t));
                    json[counter2].Add("kodbari", clsKodbari.ktheKodbarSipasIdArtikulliNjesiaKodbariIPare((string)json[counter2].SelectToken("Kodi"), clsKoka.IdNdermarrje));
                    counter2++;
                }
                Object kokaDheTrupi;
                objektiIPlote[counter] = new
                {
                    kokaDheTrupi = new
                    {
                        meta = koka[0],
                        koka = koka[1],
                        trupi = json
                    }
                };
                counter++;
                bool isShitje = (komponShitje_blerje.Contains("shitje") || komponShitje_blerje.Contains("bazaar"));
                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(clsKoka.DtDok, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, isShitje ? KategoriDokumenti.Shitje : KategoriDokumenti.Blerje, clsKoka.IdKonfigAmbjente))
                {
                    closedPeriod.Add(clsKoka.NrDok);
                    continue;
                }

                if (komponShitje_blerje.Contains("shitje") || komponShitje_blerje.Contains("blerje") || komponShitje_blerje.Contains("bazaar"))
                {
                    int idKatDok = isShitje ? 1 : 2;
                    if (!clsFunksione.kaTeDrejtePerVepriminMeDokumentin(clsKoka.IdNivel, clsKoka.NrDok, teDrejtaInfo, ref niveleRregjistrimi, idKatDok, "DFsh", idNdermarrje, idPerdoruesi, idViti, ref paTeDrejta, komponPerTedrejtat))
                        continue;
                }


                bool kontrollorivleresim = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "KR") == "Po";
                bool tollona = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "RSHTT") == "Po";
                bool tollonakastrati = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "RSHTTK") == "Po";
                bool tollonakastratielektronik = clsAlternativaKushti.getAlternativa(clsKoka.IdKonfigAmbjente, "RSHTTKE") == "Po";
                bool lidhur = (clsKoka.eshteILidhur()
                    || (tollona && DbCore.DbTollona.clsShitjeMeSerial.kaTollonaShitja(clsKoka.IdShitjeKoka))
                    || (tollonakastrati && DbCore.DbTollona.clsTollonaLeter.kaTollonaShitja(clsKoka.IdShitjeKoka))
                    || (tollonakastratielektronik && DbCore.DbTollona.clsTollonaElektronik.kaTollonaShitja(clsKoka.IdShitjeKoka)));
                if (lidhur)
                {
                    teLidhur.Add(clsKoka.NrDok);
                    continue;
                }
                if (clsKoka.NIVF != "" && clsKoka.IdStatusDok != 0)
                {
                    fiskalizim.Add(clsKoka.NrDok);
                    continue;
                }
                if (!clsKokaShitje.kaAutorizime(clsKoka.IdShitjeKoka, idPerdoruesi))
                {
                    tePaAutorizuar.Add(clsKoka.NrDok);
                    continue;
                }
                if (clsKoka.StatusAprovimi != (StatusAprovimi.Undefined) && clsKoka.StatusAprovimi != (StatusAprovimi.Aprovuar))
                {
                    if (clsKoka.StatusAprovimi == (StatusAprovimi.Refuzuar))
                    {
                        clsKusht kusht = new clsKusht(clsKoka.IdKonfigAmbjente, "ZSP");
                        clsTrupiSkemaWorkFlow trup = new clsTrupiSkemaWorkFlow();
                        trup.merrTrupSipasKokesDhePerdoruesit(kusht.Vlera, idPerdoruesi);
                        if (trup.Niveli != 1)
                        {
                            procesAprovimi.Add(clsKoka.NrDok);
                            continue;
                        }
                    }
                    else
                    {
                        procesAprovimi.Add(clsKoka.NrDok);
                        continue;
                    }
                }
                if (clsKoka.IdStatusDok == 2)
                    continue;
                if (clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(clsKoka.DtDok, idNdermarrje))
                {
                    periudheKycur.Add(clsKoka.NrDok);
                    continue;
                }
                clsKokaMagazina kok = new clsKokaMagazina();
                int idKategoria = clsNivelRegjistrimi.ktheIdKategoriaNivelRegjistrimi(clsKoka.IdNivel);
                if (idKategoria == 1)
                    kok.mbushKokaMagazinaSipasIDGjenerues(clsKoka.IdShitjeKoka, 2, clsKoka.IdKonfigAmbjente);
                else
                    kok.mbushKokaMagazinaSipasIDGjenerues(clsKoka.IdShitjeKoka, 1, clsKoka.IdKonfigAmbjente);
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
                    if (!kok.kontrolloGjendjeNeFshirje(new colTrupiMagazina(), 0).Status)
                    {
                        gjendjeNegative.Add(kok.NrDok);
                        continue;
                    }
                }
                if (kok.IdKokaMagazina != 0 && kontrollorivleresim && kok.IdStatusDok != 0)
                    if (kok.rivleresim())
                    {
                        rivleresim.Add(kok.NrDok);
                        tr = new colTrupiMagazina();
                        tr.mbushGjitheTrupiMagazinaNgaKoka(kok.IdKokaMagazina);
                        trupat.AddRange(tr);
                    }
                clsKoka.IdPerdoruesi = idPerdoruesi;
                clsMesazh mesazhi = clsKoka.fshi(idPerdoruesi, idKategoria == 1 ? true : false, tollonakastrati, tollonakastratielektronik);
                if (mesazhi.Status)
                    teFshire.Add(clsKoka);
                else
                    tePaFshire.Add(clsKoka.NrDok);
            }
            mySessionObjects.ruajTrupatNeSession(guidString, Session, trupat);
            mySessionObjects.RemoveGridRowsInSessionById(komponente, guidString, "IdShitjeKoka", teFshire.Select(ks => ks.IdShitjeKoka).ToList());
            Tuple<string, string, string> mesazhetInformuese = clsFunksione.MesazhetInformuese(teFshire.Select(ks => ks.NrDok).ToList(), teLidhur, tePaAutorizuar, periudheKycur, gjendjeNegative, procesAprovimi, tePaFshire, rivleresim, new List<string>(), new List<string>(), new List<string>(), paTeDrejta, closedPeriod, rm, cultinf, fiskalizim);
            return new
            {
                mesazhGabim = mesazhetInformuese.Item1,
                mesazhSukses = mesazhetInformuese.Item2,
                mesazhRivleresim = mesazhetInformuese.Item3,
                deletedKeys = teFshire.Select(ks => ks.IdShitjeKoka).ToArray(),
                objektifshire = teFshire.ToArray(),
                objektiWebhook = JsonConvert.SerializeObject(objektiIPlote)
            };
        }

        public static clsMesazh BejRivleresim(HttpSessionState Session, string guidString)
        {
            CultureInfo cultinf = mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            colTrupiMagazina trupat = mySessionObjects.merrTrupatNgaSesioni(guidString, Session);
            clsLogRivleresimInventari log = new clsLogRivleresimInventari();
            try
            {
                log = new clsLogRivleresimInventari(mySessionObjects.ktheIdPerdoruesi(Session), mySessionObjects.merrIdNdermarrjeSesioni(Session));
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                return new clsMesazh(false, rm.GetString("msgGabimGjateRuajtjesSeRivleresimitNeLog", cultinf));
            }
            foreach (clsTrupiMagazina t in trupat)
            {
                if (!clsArtikulli.rivleresimCmimiMesatar(t.IdArtikulli, clsArtikulli.ktheMetodeKostoje(t.IdArtikulli), t.IdMag, t.Data, DateTime.Today, clsArtikulli.ktheKontrollCmimiPerDetajim(t.IdArtikulli), log, cultinf, rm, mySessionObjects.merrIdNdermarrjeSesioni(Session), mySessionObjects.ktheIdPerdoruesi(Session)).Status)
                {
                    return new clsMesazh(false, rm.GetString("regjisDokMesazhGabimRivleresimi", cultinf));
                }
            }
            mySessionObjects.ruajTrupatNeSession(guidString, Session, new colTrupiMagazina());
            return new clsMesazh(true, rm.GetString("regjMagMesazhSuksesRivleresimi", cultinf));
        }

        public static Object ktheTeDhenaPerKlientFurnitor(int idobj, string kodlloji, int idkomp, string kodkonfig, int idNdermarrje, int idGjuha,int idperdorues)
        {
            if (idobj == 0)
                return null;

            clsKlientFurnitor klientFurnitor = new clsKlientFurnitor(idobj);
            object autorizime = callWsGetAutorizimeSipasLlojitDheIdLidhese(idobj, kodlloji,0);
            clsBanka banka = new clsBanka();
            if(klientFurnitor.EmriBanka != 0)
                banka = merrBankaSipasId(klientFurnitor.EmriBanka);
            clsKlientFurnitor kfKryesor = new clsKlientFurnitor();
            if(klientFurnitor.Idklientfurnitorkryesor != 0)
                kfKryesor = merrKfSipasId(klientFurnitor.Idklientfurnitorkryesor);
            colMarreveshjetPerKlient marreveshjeKlienti = new colMarreveshjetPerKlient();
            marreveshjeKlienti = ktheMarreveshjeKlienti(idobj);
            string eshteLidhur = clsFunksione.eshteLidhur(idkomp, kodkonfig, idobj.ToString(), idNdermarrje, idGjuha);

            return new {idObjekti = idobj, autorizime = autorizime, banka = banka, kfKryesor = kfKryesor, marreveshjeKlienti = marreveshjeKlienti, lidhur = eshteLidhur, klientFurnitor = klientFurnitor };
        }

        public static List<object> KtheRaportetMeFormatePrintimi(int idGjuha)
        {
            var cr = new colRaporti(idGjuha);
            List<object> raportet = new List<object>();

            foreach (clsRaporti rap in cr)
            {
                raportet.Add(new { RapEmri = rap.RaportiEmri, IdRap = rap.IdRaporti });
            }

            return raportet;
        }

        public static List<object> KtheDizajneSipasRaportit(int idRaporti)
        {
            colRaporteDesign crd = new colRaporteDesign();
            crd.merrSipasRaportit(idRaporti);
            List<object> dizajne = new List<object>();

            foreach (clsRaportDesign design in crd)
            {
                dizajne.Add(new { IdDesign = design.IdRaportDesign, Pershkrimi = design.Pershkrim });
            }
            return dizajne;
        }

        public static List<object> KtheNdermarrjetEPalidhura(int idDesign)
        {
            List<object> ndermarrjePaLidhur = new List<object>();
            using (var dba = new clsDatabaseAdmin())
            {

                DataTable ndermarrje = dba.merrNdermarrjeTePalidhuraMeRaportin(idDesign);

                foreach (DataRow row in ndermarrje.Rows)
                {

                    ndermarrjePaLidhur.Add(new { NdermarrjeKod = row["NDERMARJEKODI"].ToString(), NdermarrjePersh = row["NDERMARJEPERSHK"].ToString(), NdermarrjeId = row["IDNDERMARJE"].ToString() });
                }

            }


            return ndermarrjePaLidhur;

        }


        public static bool LidhNdermarjetMeFormatin(string ndermarrjet, int idformati)
        {
            using (var dba = new clsDatabaseAdmin())
            {
                dba.lidhNdermarjetMeFormatin(ndermarrjet, idformati);

            }
            return true;
        }

        public static object FshiDokumentMagazine(
            HttpSessionState Session, 
            int[] ids, 
            string komponente, 
            string guidString, 
            int periudhaIdViti, 
            string periudhaDok)
        {
            CultureInfo cultinf = mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            colTrupiMagazina trupat = new colTrupiMagazina();
            List<clsKokaMagazina> teFshire = new List<clsKokaMagazina>();
            List<string> teLidhur = new List<string>(),
                periudheKycur = new List<string>(),
                gjendjeNegative = new List<string>(),
                tePaFshire = new List<string>(),
                rivleresim = new List<string>(),
                faf = new List<string>(),
                closedPeriod = new List<string>();            

            faf = clsFunksione.KtheListeMsgPerFAFlidhur(ids.Cast<Object>().ToList());
            bool fafErr = false;
            if (faf.Count != 0)
            {                
                DataTable err = colKokaMagazina.merrFAFdokLidhur(ids.Cast<Object>().ToList());
                if (Convert.ToInt16(err.Rows[0]["NrGabimesh"]) >= 3)
                {
                    mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
                    fafErr = true;
                }
            }
            
            clsMesazh mesazh = new clsMesazh();
            colTrupiMagazina tr;
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            foreach (int id in ids)
            {
                clsKokaMagazina kok = new clsKokaMagazina();
                kok.mbushKokaMagazinaSipasID(Convert.ToInt32(id));
                bool kontrollorivleresim = clsAlternativaKushti.getAlternativa(kok.IdKonfigAmbjente, "KR") == "Po";
                bool lidhur = kok.eshteILidhur();
                bool joLMD = (clsAlternativaKushti.getAlternativa(kok.IdKonfigAmbjente, "LMD") == "Jo");
                bool autorizimet = clsKokaMagazina.kaAutorizime(kok.IdKokaMagazina, mySessionObjects.ktheIdPerdoruesi(Session));
                if (lidhur || joLMD || !autorizimet)
                {
                    teLidhur.Add(kok.NrDok);
                    continue;
                }
                bool kafaf = clsKokaMagazina.KaFAFSeriali(kok.IdKokaMagazina);
                if (kafaf)
                    continue;
                if (kok.IdStatusDok == 2)
                    continue;
                bool ekycur = clsPeriudhaKontabel.eshteKycurPeriudheSipasDateDheNdermarrjes(kok.DtDok, idNdermarrje);
                if (ekycur)
                {
                    periudheKycur.Add(kok.NrDok);
                    continue;
                }

                if (DbCore.MbylljePeriudhe.PeriodClosing.IsPeriodClosed(kok.DtDok, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, KategoriDokumenti.Magazina, kok.IdKonfigAmbjente))
                {
                    closedPeriod.Add(kok.NrDok);
                    continue;
                }

                kok.mbushTrupMagazine(false);
                colArtikujt coleksistues = new colArtikujt(kok.IdKokaMagazina, new clsDatabaseInventari());
                int i = 0;
                foreach (clsTrupiMagazina trup in kok.OcolTrupiMagazina)
                {
                    if (trup.IdLlojVeprimi == 1)
                        trup.Element = coleksistues[i];
                    i++;
                }
                if (!kok.kontrolloGjendjeNeFshirje(new colTrupiMagazina(), 0).Status)
                {
                    gjendjeNegative.Add(kok.NrDok);
                    continue;
                }
                if (kontrollorivleresim && kok.rivleresim() && kok.IdStatusDok != 0)
                {
                    rivleresim.Add(kok.NrDok);
                    tr = new colTrupiMagazina();
                    tr.mbushGjitheTrupiMagazinaNgaKoka(Convert.ToInt32(id));
                    trupat.AddRange(tr);
                }
                kok.IdPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
                mesazh = kok.fshi();
                mySessionObjects.ruajTrupatNeSession(Session, trupat);
                if (mesazh.Status)
                    teFshire.Add(kok);
                else if (mesazh.PershkrimMesazhi.Contains("negative"))
                    gjendjeNegative.Add(kok.NrDok);
                else
                    tePaFshire.Add(kok.NrDok);
            }

            mySessionObjects.ruajTrupatNeSession(guidString, Session, trupat);

            if (periudhaIdViti != 0  && periudhaDok != "")
            {
                string komponenteName = SessionKeyUtils.MerrSessionKeyPerDsGride(komponente, periudhaIdViti, periudhaDok);
                mySessionObjects.RemoveGridRowsInSessionById(komponenteName, guidString, "IdKokaMagazina", teFshire.Select(ks => ks.IdKokaMagazina).ToList());
            }

            Tuple<string, string, string> mesazhetInformuese = clsFunksione.MesazhetInformuese(
                teFshire.Select(ks => ks.NrDok).ToList(),
                teLidhur,
                new List<string>(),
                periudheKycur, 
                gjendjeNegative, 
                new List<string>(), 
                tePaFshire, 
                rivleresim,
                new List<string>(),
                new List<string>(),
                new List<string>(),
                new Dictionary<string, List<string>>(),
                closedPeriod,
                rm, 
                cultinf, new List<string>());
            return new
            {
                mesazhGabim = mesazhetInformuese.Item1,
                mesazhSukses = mesazhetInformuese.Item2,
                mesazhRivleresim = mesazhetInformuese.Item3,
                deletedKeys = teFshire.Select(ks => ks.IdKokaMagazina).ToArray(),
                faf = faf,
                fafErr = fafErr
            };            
        }
        
        public static object MerrNdermarrjetIdRoli(HttpSessionState session, string guidString, int idRoli)
        {
            var ndermarrjet = mySessionObjects.MerrNdermarrjeRolNgaSession(session, guidString, idRoli);
            if (ndermarrjet != null)
                return ndermarrjet;
            var idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(session);
            ndermarrjet = colNdermarrjet.merrNdermarrjetIdRoli(idRoli, idPerdoruesi);
            mySessionObjects.RuajNdermarrjeRolNeSession(session, guidString, idRoli, ndermarrjet);
            return ndermarrjet;
        }
        
        public static object RuajNdermarrjeRolNeSession(HttpSessionState session, string guidString, int idRoli, string[] selected)
        {
            var ndermarrjet = mySessionObjects.MerrNdermarrjeRolNgaSession(session, guidString, idRoli);
            if (ndermarrjet == null)
                mySessionObjects.RuajNdermarrjeRolNeSession(session, guidString, idRoli, null);

            Func<bool, DataRow, DataRow> setRowVisible = (visible, row) =>
            {
                row["Visible"] = visible ? "TRUE" : "FALSE";
                return row;
            };

            var teShtuara = ndermarrjet?
                .AsEnumerable()
                .Where(r => selected.Any(id => id == r["IDNDERMARJE"].ToString()) && r["Visible"].ToString() == "FALSE");
            var teShtuarat = new DataTable();
            if (teShtuara != null && teShtuara.Any())
                teShtuarat = teShtuara.CopyToDataTable();

            var selectedRows = ndermarrjet?
                .AsEnumerable()
                .Select(r => setRowVisible(true, r))
                .Where(r => selected.Any(id => id == r["IDNDERMARJE"].ToString()));

            var unSelectedRows = ndermarrjet?
                .AsEnumerable()
                .Select(r => setRowVisible(false, r))
                .Where(r => selected.All(id => id != r["IDNDERMARJE"].ToString()));

            ndermarrjet = new DataTable();

            if (selected != null && selected.Any())
                ndermarrjet.Merge(selectedRows.CopyToDataTable());

            if (unSelectedRows != null && unSelectedRows.Any())
                ndermarrjet.Merge(unSelectedRows.CopyToDataTable());

            mySessionObjects.RuajNdermarrjeRolNeSession(session, guidString, idRoli, ndermarrjet);

            if (teShtuarat.Rows.Count == 0) return new DataTable();
            var arrTeShtuarat = teShtuarat
                .Rows
                .OfType<DataRow>()
                .Select(k => k[0].ToString())
                .ToArray();
            var ids = string.Join(",", arrTeShtuarat);
            return colVitet.MerrIdVitetMeKodPerNdermarrje(ids);
        }

        public static bool kontrolloEkzistojneDokQePoKonvertohenSipasIdkoka(string idshtije, string idmag, string idrez)
        {
            bool exists;
            using (clsDatabaseRegjistrim dbRegj = new clsDatabaseRegjistrim())
            {
                exists = dbRegj.kontrolloEkzistojneDokQePoKonvertohenSipasIdkoka(idshtije, idmag, idrez);
            }
            return exists;
        }

        public static bool KrahasimiNqsGridaKaTeDhena(HttpSessionState session, int index, string llojiNv, int idNdermarrjeVit)
        {
            colKrahasimInventarizimics colkrah;

            switch (index)
            {
                case 0:
                    colkrah = mySessionObjects.MerrNgaSession<colKrahasimInventarizimics>(session, "KrahasimInventarizimi.aspx?lloj=" + llojiNv + "gvEkzistuese" + idNdermarrjeVit);
                    break;
                case 1:
                    colkrah = mySessionObjects.MerrNgaSession<colKrahasimInventarizimics>(session, "KrahasimInventarizimi.aspx?lloj=" + llojiNv + "gvPerbashket" + idNdermarrjeVit);
                    break;
                case 2:
                    colkrah = mySessionObjects.MerrNgaSession<colKrahasimInventarizimics>(session, "KrahasimInventarizimi.aspx?lloj=" + llojiNv + "gvMagTjeter" + idNdermarrjeVit);
                    break;
                default:
                    colkrah = new colKrahasimInventarizimics();
                    break;
            }

            return colkrah.Any();
        }

        public static object gjejRowSipasIdFaturaNgaDSGrides(HttpSessionState session, object[] idDokumenti, string komponente, int idNdermarrje, int idKatDokShitje, string pageId, string keyFieldName, string fields)
        {
            DataTable dt = mySessionObjects.merrGridFaturatNgaSessioni(pageId + "_" +komponente + idNdermarrje + idKatDokShitje, session, false);
            var rows = dt.AsEnumerable().Where(r => idDokumenti.Any(id => id.ToString() == r.Field<string>(keyFieldName))).ToList();
            List<object[]> orderedRows = new List<object[]>(rows.Count);
            var columns = fields.Split(';');
            if (rows.Count == 0)
            {
                return orderedRows;
            }
            foreach(string iddok in idDokumenti)
            {
                var found = rows.Find(row => row[keyFieldName].ToString() == iddok);
                object[] array = new object[columns.Length];
                for(int i = 0; i < columns.Length; i++)
                {
                    array[i] = found[columns[i]];
                }
                   
                if (found != null)
                    orderedRows.Add(array);
            }


            return orderedRows;
        }
        public static clsMesazh RuajLidhjeDetajim(string kodartikulli,string koddetajim, int idNdermarrje, int lloji, int idperdorues)
        {
            clsArtikulli artikulli = new clsArtikulli(kodartikulli, idNdermarrje);
            clsDetajimArtikulli detajim = new clsDetajimArtikulli(koddetajim, idNdermarrje);
            clsMesazh mesazh = clsDetajimPerArt.ruajLidhje(artikulli,detajim.IdDetajimArtikulli,lloji,idNdermarrje,idperdorues);
            if (!mesazh)
                return mesazh;
            return new MesazhSuksesi(string.Format(MessagesResource.Messages["msgLidhDetajimiMeArtikull"], koddetajim, kodartikulli));
        }

        public static object KtheKlientFurnitorSipasKoditLike(string kodiKlientFurnitor, string kodModeli, int idNdermarrje, int idPerdoruesi, int idGjuha, int idKlientFurnitorKryesor, int idKonfigLupaKf)
        {
            var konfigurimi = new clsKonfigurimAmbjenti();
            konfigurimi.mbushKonfiguriminMeKod(kodModeli, idNdermarrje, idGjuha);

            var alternativa = clsAlternativaKushti.getAlternativa(konfigurimi.IdKonfigAmbjente, "KF");
            if (clsAlternativaKushti.getAlternativa(idKonfigLupaKf, "SHKFVNVTKFK") == "Jo")
                idKlientFurnitorKryesor = 0;
            switch (alternativa)
            {
                case "Klient":
                    return colKlienteFurnitore.MbushKlienteFurnitoreSipasAutorizimeveLike(idNdermarrje, idPerdoruesi,
                        kodiKlientFurnitor, 1, idKlientFurnitorKryesor);
                case "Furnitor":
                    return colKlienteFurnitore.MbushKlienteFurnitoreSipasAutorizimeveLike(idNdermarrje, idPerdoruesi,
                        kodiKlientFurnitor, 0, idKlientFurnitorKryesor);
                default:
                    return colKlienteFurnitore.MbushKlienteFurnitoreSipasAutorizimeveLike(idNdermarrje, idPerdoruesi,
                        kodiKlientFurnitor, 2, idKlientFurnitorKryesor);
            }
        }

        public static object KtheKlientFurnitorSipasIdve(string ids)
        {
            return colKlienteFurnitore.MerrKlienteFurnitoreVartesIdve(ids);
        }
        public static object KerkoMarreveshje(string idMarreveshje, DateTime dtdok)
        {
            CacheLayer.GlobalCacheManager.MySessionCache.Remove("VleraMarreveshje");
            var kokashitje = new clsKokaShitje();
            var mesazh = new clsMesazh(true);
            kokashitje.KtheDokMarreveshjeNgaIdMarreveshje(idMarreveshje);
            if (kokashitje.IdShitjeKoka > 0)
                mesazh = kokashitje.KtheMesazhStatusMarreveshje(idMarreveshje, dtdok, false);
            return new
            {
                Mesazh = mesazh,
                idMarreveshje = idMarreveshje,
                Kokashitje = kokashitje
            };
        }
        public static object NgarkoMarreveshje(HttpSessionState Session,int idNder, DateTime dtdok)
        {
            try
            {
                var reader = new MarreveshjeExcelReader(idNder, dtdok);
                reader.LexoFileMarreveshje();
                mySessionObjects.RuajVleraMarreveshjeNeSession(Session, reader);
                var mesazh = new clsMesazh(true);
                if (reader.Kokashitje.IdShitjeKoka > 0)
                    mesazh = reader.Kokashitje.KtheMesazhStatusMarreveshje(reader.AgreementID, dtdok, true);
                
                return new
                {
                    Mesazh = mesazh,
                    reader.AgreementID,
                    reader.VleraBuxhetit,
                    reader.Klientet,
                    reader.Kokashitje
                };
            }
            catch (MyException ex)
            {
                return new
                {
                    Mesazh = new MesazhGabimi(ex.Message)
                };

            }
            catch (Exception e)
            {
                ImbLogger.Error(e);
                return new
                {
                    Mesazh = new MesazhGabimi(MessagesResource.Messages["msgGabimGjateNgarkimit"])
                };

            }
        }


        public static object CelDheLidhDetajimMeArtikull(int idArtikulli, int detajimPareApoDyte, int llojDetajimi, int kategoriDetajimi, string kodDetajimi, HttpSessionState session)
        {
            try
            {
                int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(session);
                int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(session);
                clsArtikulli artikulli = new clsArtikulli(idArtikulli);
                clsDetajimArtikulli detajim = new clsDetajimArtikulli(kodDetajimi, llojDetajimi, "", idPerdoruesi, kategoriDetajimi, idNdermarrje, "", 1, 0, true);
                clsMesazh mesazh = detajim.ruajShpejte(artikulli, detajimPareApoDyte);
                if (mesazh.Status)
                    mesazh = new MesazhSuksesi(string.Format(MessagesResource.Messages["msgCelDheLidhDetajimiMeArtikull"], kodDetajimi, artikulli.KodArtikulli));

                return new { detajimi = detajim, mesazhi = mesazh };  
            }
            catch (MyException ex)
            {
                return new { detajimi = new clsDetajimArtikulli(), mesazhi = new MesazhGabimi(ex.Message) };
            }
        }

        public static object MerrArtikujAutoComplete(HttpSessionState session, string kodi)
        {
            return colArtikujt.MerrSipasArtikujAktivNdermarrjesAndAutorizimeAc(mySessionObjects.merrIdNdermarrjeSesioni(session), mySessionObjects.ktheIdPerdoruesi(session), kodi);
        }

        public static object MerrKodifikim1AutoComplete(HttpSessionState session, string kodi)
        {
            return colKodifikimeArtikulli.MerrKodifikimArtikulliSipasLlojitAc(1, mySessionObjects.merrIdNdermarrjeSesioni(session), false, kodi);
        }

        public static object MerrNivelZbritjeAutoComplete(HttpSessionState session, string kodi)
        {
            return colNiveleZbritjesh.MerrNivelZbritjeSipasNdermarrjesAc(mySessionObjects.merrIdNdermarrjeSesioni(session), kodi);
        }

        public static object MerrLimitet(HttpSessionState session, int idKarta)
        {
            return clsLimitKarta.MerrLimitetSipasKartes(idKarta);
        }

        public static object kthePiketNeModifikimTeVFONE(int idDok, HttpSessionState session)
        {
            string piket = clsKokaShitje.MerrPikeNeModifikimTeVFONE(idDok, mySessionObjects.merrIdNdermarrjeSesioni(session));
            return new { Piket = piket };
        }

        public static object merrPrindQenderKosto(int idNdermarrje, string Kodi)
        {

            bool eshtePrindQk = ((DbCore.DbQendraKosto.colQendraKosto.mbushGjitheQendraKostoPrindJoFundoreSipasNdermarjesAktiv(idNdermarrje, Kodi)));
            return new { eshtePrindQk = eshtePrindQk, Kodi = Kodi };
  
        }
        public static object restoreDatabase(string prefix, string[] generations)
        {
            object result = clsFunksione.getClientDatabaseBackup(prefix, generations);
            return result;
        }
        public static void logout(HttpSessionState sessionState)
        {
            clsFunksione.LogoutRestore(sessionState, true, true, false, Paths.defaultLoginPath, "RestoreDatabase");
        }
    }
}
