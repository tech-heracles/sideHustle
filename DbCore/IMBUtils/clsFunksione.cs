using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.DbAsete;
using DbCore.DbGIS;
using DbCore.DbImporte;
using DbCore.DbInventari;
using DbCore.DbKontabiliteti;
using DbCore.DbListPagesat;
using DbCore.DbProdhimi;
using DbCore.DbQendraKosto;
using DbCore.DbShare;
using DbCore.IMBUtils;
using DbCore.Raporte;
using DbCore.VodSendSMS_Service;
using Newtonsoft.Json;
using NLog;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using Page = System.Web.UI.Page;
using DbCore.DbRegjistrim;
using PlatinumWeb;
using System.Web.UI;
using DbCore;
using DbCore.IMBUtils.DataBase;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Security;
using DbCore.IMBUtils.Validation;
using CacheLayer;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Types;
using DbCore.DbBuxheti;
using System.ComponentModel;
using AlphaWeb.Core.SharedKernel;
using AlphaWeb.Core.Interfaces.Localization;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using DbCore.IMBUtils.Fiskalizimi.Controls;
using Google.Apis.Auth.OAuth2;
using Google.Apis.SQLAdmin.v1beta4;
using Google.Apis.Services;
using Google.Apis.Iam.v1;
using Google.Apis.Iam.v1.Data;
using System.Threading.Tasks;
using Data = Google.Apis.SQLAdmin.v1beta4.Data;
using Google.Cloud.Storage.V1;
using System.Net.Mail;
using Google.Cloud.Firestore;

namespace DbCore
{
    /// <summary>
    /// kjo klase perdoret per funksionet te cilat perdoren nga shume faqe ne projekt
    /// </summary>
    public static class clsFunksione
    {
        private static string[] onesMapping = new string[] {
            "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine",
            "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fiveteen",
            "Sixteen", "Seventeen", "Eighteen", "Nineteen"
        };

        private static string[] tensMapping = new string[] {
            "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
        };

        private static string[] groupMapping = new string[] {
            "Hundred", "Thousand", "Million", "Milliard", "Trillion"
        };

        //Kthen pershkrimin me fjale te nje numri
        public static string changeToWords(string numb)
        {
            string val = "", wholeNo = numb, points = "", andStr = "", pointStr = "";
            string endStr = ("");
            if (numb.Trim() == "")
                return "";
            try
            {
                int decimalPlace = numb.IndexOf(".");
                int idxPresjes = numb.IndexOf(",");
                if (decimalPlace > 0 && idxPresjes <= 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = (" Dhe");
                        pointStr = (" ") + EnglishFromNumber(int.Parse(points)) + (" Qindarka");
                    }
                }
                else if (idxPresjes > 0 && decimalPlace <= 0)
                {
                    string[] pjeset = numb.Split(',');
                    string numri = "";
                    for (int i = 0; i < pjeset.Length; i++)
                        numri += pjeset[i];
                    wholeNo = numri;
                }
                else if (idxPresjes > 0 && decimalPlace > 0)
                {
                    string[] pjeset = numb.Split(',');
                    string numri = "";
                    for (int i = 0; i < pjeset.Length; i++)
                        numri += pjeset[i];
                    numb = numri;
                    decimalPlace = numb.IndexOf(".");
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);
                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = (" Dhe");
                        pointStr = (" ") + EnglishFromNumber(int.Parse(points)) + (" Qindarka");
                    }
                }
                val = string.Format("{0}{1}{2}{3}", EnglishFromNumber(int.Parse(wholeNo)).Trim(), andStr, pointStr, endStr);
            }
            catch {; }

            string fundi = val.Substring(val.Length - 4, 3);
            if (fundi == " E ")
                val = val.Substring(0, val.Length - 4);

            return val;
        }

        public static string EnglishFromNumber(long number)
        {
            if (number == 0)
            {
                return onesMapping[number];
            }

            string retVal = null;
            int group = 0;
            while (number > 0)
            {
                int numberToProcess = (int)(number % 1000);
                number = number / 1000;

                string groupDescription = ProcessGroup(numberToProcess);
                if (groupDescription != null)
                {
                    if (group > 0)
                    {
                        retVal = groupMapping[group] + " " + retVal;
                    }
                    retVal = groupDescription + " " + retVal;
                }

                group++;
            }

            return /*sign + */" " + retVal;
        }

        private static string ProcessGroup(int number)
        {
            int tens = number % 100;
            int hundreds = number / 100;

            string retVal = null;
            if (hundreds > 0)
            {
                retVal = onesMapping[hundreds] + " " + groupMapping[0];
            }
            if (tens > 0)
            {
                if (tens < 20)
                {
                    retVal += ((retVal != null) ? " " : "") + onesMapping[tens];
                }
                else
                {
                    int ones = tens % 10;
                    tens = (tens / 10) - 2; // 20's offset

                    retVal += ((retVal != null) ? " " : "") + tensMapping[tens];

                    if (ones > 0)
                    {
                        retVal += ((retVal != null) ? " " : "") + onesMapping[ones];
                    }
                }
            }

            return retVal;
        }

        public static List<object> ConvertDataTabletoList(DataTable dt)
        {
            List<object> rows = new List<object>();
            foreach (DataRow dr in dt.Rows)
            {
                rows.Add(dr);
            }
            return rows;
        }

        /// <summary>
        /// merr prindin e perbashket te dy patheve
        /// </summary>
        /// <param name="path1"></param>
        /// <param name="path2"></param>
        /// <returns></returns>
        public static string GetParent(string path1, string path2)
        {
            int minLength = Math.Min(path1.Length, path2.Length);
            List<char> chars = new List<char>(minLength);
            for (int i = 0; i < minLength; i++)
            {
                if (path1[i] == path2[i])
                    chars.Add(path1[i]);
            }
            return string.Join(string.Empty, chars);

        }
        public static string NormalizePath(string path)
        {
            return Path.GetFullPath(new Uri(path).LocalPath)
                       .TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                       .ToUpperInvariant();
        }
        public static void MerrGjitheFiletRekursiv(string path, ref List<string> lista, string folderiQeSduhet = null)
        {
            if (Directory.Exists(path))
            {
                if (folderiQeSduhet == null || !path.Contains(folderiQeSduhet))
                {
                    string[] files = Directory.GetFiles(path);
                    lista.AddRange(files);

                    foreach (var dir in Directory.GetDirectories(path))
                        MerrGjitheFiletRekursiv(dir, ref lista);
                }
            }
        }

        /// <summary>
        /// per te kthyer pjesen e serverit. psh: te "http://localhost:1234/Default.aspx?un=asdf&somethingelse=fdsa" kthen "http://localhost:1234/"
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string ktheServerUrl(HttpRequest request)
        {
            string strPathAndQuery = request.Url.PathAndQuery;
            return request.Url.AbsoluteUri.Replace(strPathAndQuery, "/");
        }

        /// <summary>
        /// Kontrollon nese data e dhene permbahet apo jo ne periudhen e dhene, dhe nese jo kthen false dhe mesazhin e gabimit, True Perndryshe
        /// </summary>
        /// <param name="mesazhGabimi">Mesazhi i gabimit, perndryshe null nese kthehet true </param>
        /// <param name="dateDokumenti">data dokumentit</param>
        /// <param name="periudha">periudha qe po punohet</param>
        /// <returns>kthen true nese nuk gjen gabim, false nese gjen</returns>
        public static bool checkPeriudheKontabel(out string mesazhGabimi, DateTime dateDokumenti, clsPeriudhaKontabel periudha, int draft)
        {
            if (periudha.Ekycur)
            {
                mesazhGabimi = MessagesResource.Messages["msgNukKryeniVeprimeSePeriudhaEshteEKycur"];
                return false;
            }
            if (draft == 1 && (periudha.FillimiPeriudha > dateDokumenti || periudha.MbarimiPeriudha.AddDays(1) < dateDokumenti.AddMilliseconds(1)))
            {
                mesazhGabimi = MessagesResource.Messages["msgDataEDokDuhetTePerfshihetNePeriudhenEZgjedhur"];
                return false;
            }
            mesazhGabimi = null;
            return true;
        }

        public static clsMesazh checkPeriudheKontabel(DateTime dateDokumenti, clsPeriudhaKontabel periudha, int draft)
        {
            string mesazh = "";
            bool rezultati = checkPeriudheKontabel(out mesazh, dateDokumenti, periudha, draft);
            return new clsMesazh(rezultati, mesazh);
        }

        /// <summary>
        /// qellimi ketu eshte te kontrollohen te drejtat dhe autorizimet,dhe ky funksion te reduktohet
        /// </summary>
        /// <param name="idperdoruesi"></param>
        /// <param name="idndermarje"></param>
        /// <param name="idViti"></param>
        /// <param name="emerkomp"></param>
        /// <param name="liste"></param>
        /// <param name="modifikim"></param>
        /// <param name="id"></param>
        /// <param name="idGjuha"></param>
        /// <param name="idKonfig"></param>
        /// <param name="idNiveli"></param>
        /// <returns></returns>
        public static bool KaTeDrejteTeHapeAmbjentin(int idperdoruesi, int idndermarje, int idViti, string emerkomp, bool liste, bool modifikim, int id, int idGjuha, int idKonfig, int idNiveli, string veprimi)
        {
            var komp = new clsKomponente(emerkomp);
            var tedrejta = new clsTeDrejtaRoli();
            tedrejta.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idViti, emerkomp);
            if (!tedrejta.DAmb)
                return false;
            if (!modifikim && !liste && !tedrejta.DShtim && !tedrejta.DShtimDraft)
                return false;
            if (modifikim)
            {
                var konf = 0;
                if (idKonfig != 0)
                    return clsKonfigurimAmbjenti.kaAutorizimKonfigurimi(idKonfig, idperdoruesi);
                switch (komp.IdKomponente)
                {
                    case 506:
                    case 508:
                    case 554:
                    case 556:
                        konf = clsKokaShitje.ktheIdKonfigAmbjente(id);
                        break;

                    case 301:
                    case 306:
                    case 307:
                    case 308:
                        var banka = new clsVeprimBankaKoka(id);
                        konf = banka.IdKonfigAmbjente;
                        break;

                    case 116:
                        var flete = new clsKokaFleteKontabel(id);
                        konf = flete.IdKonfigAmbjente;
                        break;

                    case 510:
                    case 511:
                        var mag = new clsKokaMagazina();
                        mag.mbushKokaMagazinaSipasID(id);
                        konf = mag.IdKonfigAmbjente;
                        break;
                    case 549:
                    case 550:
                        var inv = new clsKokaInventarizim();
                        inv.mbushKokaInventarizimSipasID(id);
                        konf = inv.IdKonfigAmbjente;
                        break;
                    case 545:
                        var ndryshim = new clsKokaNdryshimCmimSasi();
                        ndryshim.mbushKokaNdryshimCmimSasiSipasID(id);
                        konf = ndryshim.IdKonfigAmbjente;
                        break;

                    case 539:
                    case 540:
                        var rez = new clsKokaRezervime();
                        rez.mbushKokaRezervimiSipasID(id);
                        konf = rez.IdKonfigAmbjente;
                        break;

                    case 518:
                        var shper = new clsShperndarjeShpenzimeKoka(id);
                        konf = shper.IdKonfigAmbjente;
                        break;

                    case 522:
                    case 528:
                        var dog = new clsFleteDoganoreKoka(id);
                        konf = dog.IdKonfigAmbjente;
                        break;

                    case 217:
                        var dok = new clsDokumentLidhesKoka() { IdKoka = id };
                        dok.merrSipasId();
                        konf = dok.IdKonfigAmbjente;
                        break;

                    case 650:
                        var azho = new clsAzhornimKFKoka(id);
                        konf = azho.IdKonfigAmbjente;
                        break;

                    case 679:
                        var mbyll = new clsKokaMbylljeKF(id);
                        konf = mbyll.IdKonfigAmbjente;
                        break;

                    case 653:
                        var vep = new clsVeprimeKFKoka(id);
                        konf = vep.IdKonfigAmbjente;
                        break;

                    case 710:
                        var list = new clsKokaListPagese(id, false);
                        konf = list.IdKonfigAmbjente;
                        break;

                    case 804:
                        var plan = new clsKokaPlanifikim(id);
                        konf = plan.IdKonfigAmbjente;
                        break;

                    case 806:
                        var ekzek = new clsKokaEkzekutim(id);
                        konf = ekzek.IdKonfigAmbjente;
                        break;

                    case 313:
                        var urdher = new clsKokaUrdherPagese(id);
                        konf = urdher.IdKonfigAmbjente;
                        break;
                    case 908:
                        var qendra = new clsKokaQendraKosto(id);
                        konf = qendra.IdKonfigAmbjente;
                        break;

                    case 1003:
                    case 1007:
                    case 1008:
                        var amortizimi = new clsAmortizimiKoka(id);
                        konf = amortizimi.IdKonfigurimAmbjenti;
                        break;

                    case 542:
                        var rip = new clsKokaRiparime(id);
                        konf = rip.IdKonfigAmbjente;
                        break;

                    case 809:
                        var skedulim = new clsKokaSkedulimProdhimi(id);
                        konf = skedulim.IdKonfigAmbjente;
                        break;

                    case 500:
                    case 525:
                    case 526:
                        konf = id;
                        break;

                    case 175:
                    case 212:
                        var konfigurime = new colKonfigurimAmbjenti();
                        var col = new colKategoriNiveleDok();
                        col.mbushGjitheKategoriSipasKomponentes(komp.IdKomponente);
                        if (col.Count == 0)
                            return true;
                        konfigurime.mbushKonfigAmbjSipasIdKomponente(komp.IdKomponente, idndermarje, idperdoruesi);
                        if (komp.IdKomponente == 175 && konfigurime.Count != 0 && id > 0)
                        {
                            clsKokaFormatImporti formati = new clsKokaFormatImporti(id);
                            return clsKonfigurimAmbjenti.kaTeDrejteDheAutorizimTeHapeAmbientSipasKategorise(formati.IdKategori, idperdoruesi, idViti, 175, idndermarje);
                        }
                        return konfigurime.Count != 0;

                    case 2002:
                        return true;
                }
                if (komp.IdKomponente == 2002)
                    return true;
                if (komp.IdKomponente == 506 || komp.IdKomponente == 508)
                    return clsKonfigurimAmbjenti.kaTeDrejteDheAutorizimTeHapeNivelRegjistrimiSipasKonfigurimit(konf, idperdoruesi, idViti, komp.EmriKomponente);
                else
                    return clsKonfigurimAmbjenti.kaAutorizimKonfigurimi(konf, idperdoruesi);
            }

            var lloji = (emerkomp.Contains("=")) ? emerkomp.Split('=')[1] : "";
            string kodniveli;
            switch (lloji)
            {
                case "":
                case "Bilanc":
                case "Pash":
                case "Cashflow":
                case "Grup":
                case "Titull":
                case "Kapitull":
                case "1":
                case "2":
                case "3":
                    kodniveli = "";
                    break;

                case "arka":
                    kodniveli = "ARC";
                    break;

                case "banka":
                    kodniveli = "B";
                    break;

                case "klient":
                    kodniveli = emerkomp.Contains("Grupime") ? "" : "K";
                    break;

                case "furnitor":
                    kodniveli = emerkomp.Contains("Grupime") ? "" : "F";
                    break;

                case "derdhje":
                    kodniveli = "Derdhje";
                    break;

                case "terheqje":
                    kodniveli = "Terheqje";
                    break;

                case "arketim":
                    kodniveli = "Arketim";
                    break;

                case "pagese":
                    kodniveli = "Pagese";
                    break;

                case "afatshkurter":
                    kodniveli = "ART";
                    break;

                case "aqt":
                    kodniveli = "AQT";
                    break;

                case "shitje":
                    kodniveli = emerkomp.Contains("sf") ? "PSH" : "FSH";
                    break;

                case "blerje":
                    kodniveli = "FB";
                    break;

                case "azhornim":
                    kodniveli = "AKF";
                    break;

                case "mbyllje":
                    kodniveli = "MKF";
                    break;

                case "hyrje":
                    kodniveli = emerkomp.Contains("Magazine") ? "FH;UH" : "RH";
                    break;

                case "dalje":
                    kodniveli = emerkomp.Contains("Magazine") ? "FD;UD" : "RD";
                    break;

                case "ash":
                    kodniveli = "IAASH";
                    break;

                case "agj":
                    kodniveli = "IAAGJ";
                    break;

                case "import":
                    kodniveli = "FLDI";
                    break;

                case "export":
                    kodniveli = "FLDE";
                    break;

                case "furnizim":
                    kodniveli = "PF";
                    break;

                case "true":
                    kodniveli = "KLP";
                    break;

                case "false":
                    kodniveli = "KP";
                    break;

                default:
                    kodniveli = "";
                    break;
            }

            var konfigurimet = new colKonfigurimAmbjenti();
            if (kodniveli == "")
            {
                var col = new colKategoriNiveleDok();
                col.mbushGjitheKategoriSipasKomponentes(komp.IdKomponente);
                if (komp.IdKomponente == 414 ||
                    komp.IdKomponente == 2002 ||
                    komp.IdKomponente == 4001 ||
                    komp.IdKomponente == 185)
                    return true;
                if (col.Count == 0)
                    return true;
                konfigurimet.mbushKonfigAmbjSipasIdKomponente(komp.IdKomponente, idndermarje, idperdoruesi);
            }
            else
            {
                var llojet = kodniveli.Split(';');
                foreach (var lloj in llojet)
                {
                    if (lloj == "")
                        continue;
                    var niv = new clsNivelRegjistrimi();
                    niv.mbushNivelRegjistrimiSipasKoditPaKonvertime(lloj, idndermarje);
                    var kat = new clsKategoriNivelDok();
                    kat.mbushKategoriNivelDokSipasID(niv.IdKategori);
                    if (kat.IdSuperKategori == 2 && liste)
                        return true;
                    if (idNiveli == 0)
                        idNiveli = niv.IdNivel;

                    if (niv.IdKategori == 1 || niv.IdKategori == 2)
                    {
                        if (!liste)
                            konfigurimet.mbushKonfigAmbjSipasIdKategoriPaKonfVartese(niv.IdKategori, idndermarje,
                                idperdoruesi, idGjuha);
                        else
                            konfigurimet.mbushKonfigAmbjSipasIdKategori(niv.IdKategori, idndermarje, idperdoruesi,
                                idGjuha);
                    }
                    else
                        konfigurimet.mbushKonfigAmbjSipasIdKategoriIdNivel(niv.IdKategori, idNiveli, idperdoruesi,
                            idGjuha, liste);
                }
            }
            if (konfigurimet.Count == 0) return false;
            if (lloji == "shitje" || lloji == "blerje")
                return MerrKonfigurimShitje(konfigurimet, veprimi).Any();
            return true;
        }

        public static bool kaTeDrejteTeHapeAmbjentin(int idperdoruesi, int idndermarje, int idViti, string emerkomp, bool liste, bool modifikim, int id, int idGjuha, string url = "")
        {
            clsKomponente komp = new clsKomponente(emerkomp);
            clsTeDrejtaRoli tedrejta = new clsTeDrejtaRoli();
            tedrejta.merrTeDrejtaPerKeteKomponente(idperdoruesi, idndermarje, idViti, emerkomp);
            if (!tedrejta.DAmb)
                return false;
            if (!modifikim && !liste && !tedrejta.DShtim && !tedrejta.DShtimDraft)
                return false;
            if (modifikim)
            {
                int konf = 0;
                switch (komp.IdKomponente)
                {
                    case 506:
                    case 508:
                        konf = DbCore.DbRegjistrim.clsKokaShitje.ktheIdKonfigAmbjente(id);
                        break;

                    case 301:
                    case 306:
                    case 307:
                    case 308:
                        clsVeprimBankaKoka banka = new clsVeprimBankaKoka(id);
                        konf = banka.IdKonfigAmbjente;
                        break;

                    case 116:
                        clsKokaFleteKontabel flete = new clsKokaFleteKontabel(id);
                        konf = flete.IdKonfigAmbjente;
                        break;

                    case 510:
                    case 511:
                        DbCore.DbRegjistrim.clsKokaMagazina mag = new DbCore.DbRegjistrim.clsKokaMagazina();
                        mag.mbushKokaMagazinaSipasID(id);
                        konf = mag.IdKonfigAmbjente;
                        break;
                    case 549:
                    case 550:
                        DbCore.DbRegjistrim.clsKokaInventarizim inv = new DbCore.DbRegjistrim.clsKokaInventarizim();
                        inv.mbushKokaInventarizimSipasID(id);
                        konf = inv.IdKonfigAmbjente;
                        break;
                    case 545:
                        DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi ndryshim = new DbCore.DbRegjistrim.clsKokaNdryshimCmimSasi();
                        ndryshim.mbushKokaNdryshimCmimSasiSipasID(id);
                        konf = ndryshim.IdKonfigAmbjente;
                        break;

                    case 539:
                    case 540:
                        DbCore.DbRegjistrim.clsKokaRezervime rez = new DbCore.DbRegjistrim.clsKokaRezervime();
                        rez.mbushKokaRezervimiSipasID(id);
                        konf = rez.IdKonfigAmbjente;
                        break;

                    case 518:
                        DbCore.DbRegjistrim.clsShperndarjeShpenzimeKoka shper = new DbCore.DbRegjistrim.clsShperndarjeShpenzimeKoka(id);
                        konf = shper.IdKonfigAmbjente;
                        break;

                    case 522:
                    case 528:
                        DbCore.DbRegjistrim.clsFleteDoganoreKoka dog = new DbCore.DbRegjistrim.clsFleteDoganoreKoka(id);
                        konf = dog.IdKonfigAmbjente;
                        break;

                    case 217:
                        //  case 265:
                        DbCore.DbRegjistrim.clsDokumentLidhesKoka dok = new DbCore.DbRegjistrim.clsDokumentLidhesKoka() { IdKoka = id };
                        dok.merrSipasId();
                        konf = dok.IdKonfigAmbjente;
                        break;

                    case 650:
                        DbCore.DbRegjistrim.clsAzhornimKFKoka azho = new DbCore.DbRegjistrim.clsAzhornimKFKoka(id);
                        konf = azho.IdKonfigAmbjente;
                        break;

                    case 679:
                        DbCore.DbRegjistrim.clsKokaMbylljeKF mbyll = new DbCore.DbRegjistrim.clsKokaMbylljeKF(id);
                        konf = mbyll.IdKonfigAmbjente;
                        break;

                    case 653:
                        DbCore.DbRegjistrim.clsVeprimeKFKoka vep = new DbCore.DbRegjistrim.clsVeprimeKFKoka(id);
                        konf = vep.IdKonfigAmbjente;
                        break;

                    case 710:
                        clsKokaListPagese list = new clsKokaListPagese(id, false);
                        konf = list.IdKonfigAmbjente;
                        break;

                    case 804:
                        clsKokaPlanifikim plan = new clsKokaPlanifikim(id);
                        konf = plan.IdKonfigAmbjente;
                        break;

                    case 806:
                        clsKokaEkzekutim ekzek = new clsKokaEkzekutim(id);
                        konf = ekzek.IdKonfigAmbjente;
                        break;

                    case 313:
                        clsKokaUrdherPagese urdher = new clsKokaUrdherPagese(id);
                        konf = urdher.IdKonfigAmbjente;
                        break;
                    case 908:
                        clsKokaQendraKosto qendra = new clsKokaQendraKosto(id);
                        konf = qendra.IdKonfigAmbjente;
                        break;

                    case 1003:
                    case 1007:
                    case 1008:
                        clsAmortizimiKoka amortizimi = new clsAmortizimiKoka(id);
                        konf = amortizimi.IdKonfigurimAmbjenti;
                        break;

                    case 542:
                        DbCore.DbRegjistrim.clsKokaRiparime rip = new DbRegjistrim.clsKokaRiparime(id);
                        konf = rip.IdKonfigAmbjente;
                        break;

                    case 809:
                        clsKokaSkedulimProdhimi skedulim = new clsKokaSkedulimProdhimi(id);
                        konf = skedulim.IdKonfigAmbjente;
                        break;

                    case 500:
                    case 525:
                    case 526:

                        konf = id;
                        break;

                    case 175:
                    case 212:
                        colKonfigurimAmbjenti konfigurime = new colKonfigurimAmbjenti();
                        DbCore.DbRegjistrim.colKategoriNiveleDok col = new DbCore.DbRegjistrim.colKategoriNiveleDok();
                        col.mbushGjitheKategoriSipasKomponentes(komp.IdKomponente);
                        if (col.Count == 0)
                            return true;
                        konfigurime.mbushKonfigAmbjSipasIdKomponente(komp.IdKomponente, idndermarje, idperdoruesi);
                        if (konfigurime.Count == 0)
                            return false;
                        else
                            return true;

                    case 2002:
                        return true;
                }
                if (komp.IdKomponente == 2002)
                    return true;
                else
                    if (!clsKonfigurimAmbjenti.kaAutorizimKonfigurimi(konf, idperdoruesi))
                    return false;
                else return true;
            }
            else
            {
                string lloji = (emerkomp.Contains("=")) ? emerkomp.Split('=')[1] : "";
                string kodniveli = "";
                switch (lloji)
                {
                    case "":
                    case "Bilanc":
                    case "Pash":
                    case "Cashflow":
                    case "Grup":
                    case "Titull":
                    case "Kapitull":
                    case "1":
                    case "2":
                    case "3":
                        kodniveli = "";
                        break;

                    case "arka":
                        kodniveli = "ARC";
                        break;

                    case "banka":
                        kodniveli = "B";
                        break;

                    case "klient":
                        if (emerkomp.Contains("Grupime"))
                            kodniveli = "";
                        else kodniveli = "K";
                        break;

                    case "furnitor":
                        //  case "funitor":
                        if (emerkomp.Contains("Grupime"))
                            kodniveli = "";
                        else kodniveli = "F";
                        break;

                    case "derdhje":
                        kodniveli = "Derdhje";
                        break;

                    case "terheqje":
                        kodniveli = "Terheqje";
                        break;

                    case "arketim":
                        kodniveli = "Arketim";
                        break;

                    case "pagese":
                        kodniveli = "Pagese";
                        break;

                    case "afatshkurter":
                        kodniveli = "ART";
                        break;

                    case "aqt":
                        kodniveli = "AQT";
                        break;

                    case "shitje":
                        if (emerkomp.Contains("sf"))
                            kodniveli = "PSH";
                        else kodniveli = "FSH";
                        break;

                    case "blerje":
                        kodniveli = "FB";
                        break;

                    case "azhornim":
                        kodniveli = "AKF";
                        break;

                    case "mbyllje":
                        kodniveli = "MKF";
                        break;

                    case "hyrje":
                        if (emerkomp.Contains("Magazine"))
                            kodniveli = "FH;UH";
                        else kodniveli = "RH";
                        break;

                    case "dalje":
                        if (emerkomp.Contains("Magazine"))
                            kodniveli = "FD;UD";
                        else kodniveli = "RD";
                        break;
                    case "ash":
                        kodniveli = "IAASH";
                        break;
                    case "agj":
                        kodniveli = "IAAGJ";
                        break;
                    case "import":
                        kodniveli = "FLDI";
                        break;

                    case "export":
                        kodniveli = "FLDE";
                        break;

                    case "furnizim":
                        kodniveli = "PF";
                        break;

                    case "true":
                        kodniveli = "KLP";
                        break;

                    case "false":
                        kodniveli = "KP";
                        break;

                    default:
                        kodniveli = "";
                        break;
                }

                colKonfigurimAmbjenti konfigurime = new colKonfigurimAmbjenti();
                if (kodniveli == "")
                {
                    DbCore.DbRegjistrim.colKategoriNiveleDok col = new DbCore.DbRegjistrim.colKategoriNiveleDok();
                    col.mbushGjitheKategoriSipasKomponentes(komp.IdKomponente);
                    if (komp.IdKomponente == 414 || komp.IdKomponente == 2002)
                        return true;
                    if (!DbAnalizeBuxheti.AnalizeBuxheti.KaTeDrejteTeHapeAmbjentin(komp.IdKomponente, idndermarje)) return false;

                    if (col.Count == 0)
                        return true;
                    konfigurime.mbushKonfigAmbjSipasIdKomponente(komp.IdKomponente, idndermarje, idperdoruesi);
                }
                else
                {
                    string[] llojet = kodniveli.Split(';');
                    for (var j = 0; j < llojet.Length; j++)
                    {
                        if (llojet[j] == "")
                            continue;
                        DbCore.DbRegjistrim.clsNivelRegjistrimi niv = new DbCore.DbRegjistrim.clsNivelRegjistrimi();
                        niv.mbushNivelRegjistrimiSipasKoditPaKonvertime(llojet[j], idndermarje);
                        DbCore.DbRegjistrim.clsKategoriNivelDok kat = new DbCore.DbRegjistrim.clsKategoriNivelDok();
                        kat.mbushKategoriNivelDokSipasID(niv.IdKategori);
                        if (kat.IdSuperKategori == 2 && liste)
                            return true;
                        int idNiveli = niv.IdNivel;
                        if (!String.IsNullOrEmpty(url))
                        {
                            NameValueCollection myQuery = HttpUtility.ParseQueryString(url);
                            if (!String.IsNullOrEmpty(myQuery["niveli"]))
                                idNiveli = Convert.ToInt32(myQuery["niveli"]);
                        }
                        if (niv.IdKategori == 1 || niv.IdKategori == 2)
                        {
                            if (!liste && !modifikim)
                                konfigurime.mbushKonfigAmbjSipasIdKategoriPaKonfVartese(niv.IdKategori, idndermarje, idperdoruesi, idGjuha);
                            else
                                konfigurime.mbushKonfigAmbjSipasIdKategori(niv.IdKategori, idndermarje, idperdoruesi, idGjuha);
                        }
                        else
                        {
                            if (!liste && !modifikim)
                                konfigurime.mbushKonfigAmbjSipasIdKategoriIdNivel(niv.IdKategori, idNiveli, idperdoruesi, idGjuha, false);
                            else
                                konfigurime.mbushKonfigAmbjSipasIdKategoriIdNivel(niv.IdKategori, idNiveli, idperdoruesi, idGjuha, true);
                        }
                    }
                }
                //if (!liste && !modifikim) //modifikim
                //{
                //    //listen direkt
                //    colKonfigurimAmbjenti konfVarura = new colKonfigurimAmbjenti();
                //    //konfVarura.mbushKonfigAmbjVarteseSipasIdKategoriKodNivel( TODO PATI
                //    foreach (DbCore.DbShare.clsKonfigurimAmbjenti konfi in konfigurime)
                //    {
                //        DbCore.DbShare.clsKusht kusht = new DbCore.DbShare.clsKusht(konfi.IdKonfigAmbjente, "V");
                //        if (kusht.Vlera == 43)
                //            konfVarura.Add(konfi);
                //    }
                //    foreach (DbCore.DbShare.clsKonfigurimAmbjenti konfi in konfVarura)
                //    {
                //        konfigurime.Remove(konfi);
                //    }
                //}
                if (konfigurime.Count == 0)
                    return false;
                else return true;
            }
        }

        public static bool teDrejta(int idPerdoruesi, int idViti, int idNdermarrje, string emerfaqe)
        {
            clsTeDrejtaRoli tedrejta = new clsTeDrejtaRoli();
            tedrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, emerfaqe);
            if (!tedrejta.DAmb)
                return false;
            return true;
        }

        public static string formatoKoordinate(string koordinata)
        {
            string k = koordinata.Substring(koordinata.IndexOf('(') + 1, koordinata.Length - koordinata.IndexOf('(') - 2);
            string[] koordinatat = k.Split(' ');
            return $"{koordinatat[1]}, {koordinatat[0]}";
        }

        public static string formatoKoordinatePerRuajtje(string koordinata)
        {

            string[] koordinatat = koordinata.Split(',');
            return $"POINT({koordinatat[1].Trim()} {koordinatat[0].Trim()})";
        }
        /// <summary>
        /// kthen nese perdoruesi ne kete ndermarrje ka te drejte te shoh te gjithe dokumentat per komponenten
        /// </summary>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idViti"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="emerfaqe">emri i komponentes</param>
        /// <returns></returns>
        public static bool teDrejtaGjitheDokSipasKomponentes(int idPerdoruesi, int idViti, int idNdermarrje, string emerKomponente)
        {
            clsTeDrejtaRoli tedrejta = new clsTeDrejtaRoli();
            tedrejta.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, emerKomponente);
            return tedrejta.DGjitheDok;
        }

        /// <summary>
        /// kthen nje liste mesazhesh per dokumentat e lidhura me FAF
        /// </summary>
        /// <param name="dokMagazine"></param>
        /// <returns></returns>
        public static List<string> KtheListeMsgPerFAFlidhur(List<object> dokMagazine)
        {
            string iddok = "(" + String.Join(",", dokMagazine) + ")";
            DataTable faf = new DbCore.DbRegjistrim.clsDatabaseRegjistrim().MerrFAFSeriali(iddok);
            List<string> lista = new List<string>();
            for (int d = 0; d < faf.Rows.Count; d++)
            {
                lista.Add("Dokumenti \"" + faf.Rows[d]["NRDOK"] + "\" i datës \"" + faf.Rows[d]["DTDOK"] + "\" është i lidhur me FAF:" + " </br>" + faf.Rows[d]["DOKFAF"].ToString().Replace("&lt;/br&gt;", "</br>"));
            }
            return lista;
        }

        public static DataTable mbushMetodaTransferimiPerSerialeUnike()
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
                return db.mbushMetodaTransferimiPerSerialeUnike();
        }

        public static clsMesazh NdryshoNivelVerbosity(int niveli, string moduli)
        {
            using (clsDatabaseAdmin db = new clsDatabaseAdmin())
                return db.NdryshoNivelVerbosity(niveli, moduli);
        }

        public static string KtheMesazhPerPerdoruesin()
        {
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                return dbAdmin.KtheMesazhPerPerdoruesin();
            }
        }
        public static async Task<bool> CheckIfEmailIsVerified(string email)
        {
            FirebaseConfiguration fb = new FirebaseConfiguration();
            return await fb.checkIfUserIsVerified(email);
        }
        public static bool KonfirmoEmail(string email)
        {
            string apiKey = PasswordHelper.GjenroApiKey(email);
            string timeStamp = DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
            HttpWebRequest webReq = clsFunksione.CreateJSONWebRequest("https://europe-west1-alphaweb.cloudfunctions.net/sendVerificationEmail");
            webReq.Headers.Add("authkey", "ZW1haWxzZW5kZXI6YTVhNGMwZTEwYjEyNzkxMzFiMWZlYjQ3ZWM3YWY1MGM5YzgyNWUzOTIyZTQ3NzU2MjQ4YWFjMTk1NDE5ZTk3Yg==");
            using (Stream stream = webReq.GetRequestStream())
            {
                using (StreamWriter stmw = new StreamWriter(stream))
                {
                    stmw.Write(JsonConvert.SerializeObject(new {
                        email = email,
                        subject = "Email Verification",
                        message = "Pershendetje,<br><br> Per te verifikuar email-in tuaj ndiq linkun<br><br> " + "http://localhost:4000/rest/verifyEmail?email=" + email + "&timestamp=" + timeStamp + "&apikey=" + apiKey + "<br><br>Faleminderit!"
                    }));
                }
            }

            using (WebResponse webResponse = webReq.GetResponse())
            {
                using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                {

                    var ServiceResult = rd.ReadToEnd();
                    if (ServiceResult == "Email Sent!") return true;
                    else return false;
                }
            }
            return false;
        }
        public static async Task<IAsyncResult> gjeneroLinkPerKonfirmimEmaili (string email, string apikey)
        {
            try
            {
                apikey = apikey.Contains('+') == true ? apikey.Replace("+", "%2B") : apikey;
                HttpWebRequest webReq = clsFunksione.CreateJSONWebRequest("https://europe-west1-alphaweb.cloudfunctions.net/sendVerificationEmail");
                string linkDatasetEndpoint = WebConfigurationManager.AppSettings["urlEmailAsign"];
                webReq.Headers.Add("authkey", "ZW1haWxzZW5kZXI6YTVhNGMwZTEwYjEyNzkxMzFiMWZlYjQ3ZWM3YWY1MGM5YzgyNWUzOTIyZTQ3NzU2MjQ4YWFjMTk1NDE5ZTk3Yg==");
                using (Stream stream = webReq.GetRequestStream())
                {
                    using (StreamWriter stmw = new StreamWriter(stream))
                    {
                        stmw.Write(JsonConvert.SerializeObject(new
                        {
                            email = email,
                            subject = "Email Verification",
                            message = "Pershendetje,<br><br> Per te verifikuar email-in tuaj ndiq linkun<br><br> " + linkDatasetEndpoint + "rest/setAlphaOrganization?apiKey=" + apikey
                        }));
                    }
                }
                return webReq.BeginGetResponse(null, null);
            }
            catch(Exception ex)
            {
                return null;

            }

        }

        public static void dergoKerkesePerAprovimPerdoruesi(string perPerdoruesin, string ngaPerdoruesi, int idNdermarje, int idPerdoruesi, int idNdermarjeVit, string status)
        {
            CultureInfo ci;
            ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));

            // 1. merr emailet ku duhet te dergohet njoftimi
            clsDatabaseAdmin da = new clsDatabaseAdmin();
            DataTable aprovuesit = da.merrEmaileDheGjuhePerAprovimePerdoruesish();

            string linkuperd = HttpContext.Current.Server.HtmlEncode(ktheServerUrl(HttpContext.Current.Request)) + "Shto_Perdorues.aspx?arsyeja=ngaEmailKerkeseAprovim&perdorues=" + perPerdoruesin + "&idNderm=" + idNdermarje + "&idVitNdermarrje=" + idNdermarjeVit;

            List<clsMesazh> mesazhet = new List<clsMesazh>();
            foreach (DataRow r in aprovuesit.Rows)
            {
                string emails = r.Field<string>("Emails");
                if (emails == null || emails == "") continue;
                int idgjuha = Convert.ToInt32(r[0]);
                ci = new CultureInfo(Enum.GetName(typeof(KodGjuhePerPerkthim), idgjuha).ToString().Replace("_", "-"));

                // 2. pergatit permbajtjen e e-mail
                string subject = rm.GetString("reportWatermarkPerAprovim", ci);
                string header = rm.GetString("njoftimDokHeaderEmail", ci);
                string footer = rm.GetString("njoftimDokFooterEmail", ci);
                string body = rm.GetString("njoftimPerdoruesPerAprovimBodyEmail", ci);
                body = body.Replace("#status#", status);
                body = body.Replace("#perPerdoruesin#", perPerdoruesin);
                body = body.Replace("#ngaPerdoruesi#", ngaPerdoruesi);
                body = body.Replace("#linkuperd#", "<a href ='" + linkuperd + "'>link</a>");

                // 3. dergo email
                EmailComposer.DergoEmailStandart(idNdermarje, emails.Split(','), status, header + body + footer, idPerdoruesi, true);
            }
        }
        //Kur ktheUrlVersioni eshte true stringu qe kthehet eshte url e versionit
        public static Tuple<string, string, string> ktheUrlHelpi(String helpUrlSuffixKomponente)
        {
            clsDatabaseAdmin data = new clsDatabaseAdmin();
            DataRow dr = data.ktheTeDhenaHelpServeri();
            data.Dispose();


            if (dr == null)
                return null;
            String serveri = dr["HELPNAME"].ToString();
            String faqeKryesore = dr["HELPFAQEKRYESORE"].ToString();



            if (serveri.Length == 0 || faqeKryesore.Length == 0)
                throw new Exception("Nuk ekzistojne te dhenat e serverit te helpit");

            String port = dr["HELPPORT"].ToString();

            faqeKryesore = faqeKryesore.Replace(" ", "_");
            String urlManuali = port.Length == 0 ? serveri + "/" + faqeKryesore : serveri + ":" + port + "/" + faqeKryesore;

            urlManuali = "http://" + urlManuali + ".htm";

            if (helpUrlSuffixKomponente.Length != 0)
            {
                urlManuali += helpUrlSuffixKomponente;
            }

            String versionUrl = port.Length == 0 ? serveri + "/" : serveri + ":" + port + "/";

            versionUrl = "http://" + versionUrl + "#Versione_te_Reja/Versionet e Alpha Web/Versioni " + dr["VersioniFunditHelp"].ToString() + ".htm";


            return new Tuple<string, string, string>(urlManuali, versionUrl, dr["VersioniAlphaWeb"].ToString());

        }

        public static int merrWindowWidthRequested(HttpRequest request)
        {
            try
            {
                return Convert.ToInt32(Convert.ToDouble(request.QueryString["windowWidth"]));
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static int merrIDFiltriPersonalizuar(HttpRequest request)
        {
            try
            {
                return Convert.ToInt32(request.QueryString["idFiltri"]);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public static bool isLlojDalje(HttpRequest request) => request.QueryString["lloj"] == "dalje";

        /// <summary>
        ///
        /// </summary>
        /// <param name="request">HttpRequest as param</param>
        /// <returns></returns>
        public static bool isLlojHyrje(HttpRequest request) => request.QueryString["lloj"] == "hyrje";

        /// <summary>
        /// kthen id-ne e raporti nga querystring
        /// </summary>
        /// <param name="page">faqja</param>
        /// <returns>id-ja raportit</returns>
        public static int ktheIdRaporti(HttpRequest Request)
        {
            int idRaporti;
            var idRaportiString = Request.QueryString["idraporti"];
            int.TryParse(idRaportiString, out idRaporti);
            if (idRaportiString == "")
                LogManager.GetCurrentClassLogger().Error("ktheIdRaporti(" + Request + ") - idRaportiString == \"\"");
            return idRaporti;
        }

        public static bool ktheFiltroQueryString(HttpRequest Request)
        {
            if (Request["Filtro"] != null)
                return Convert.ToBoolean(Request["Filtro"].ToString());
            return false;
        }
        /// <summary>
        /// sherben per te konfiguruar nivelin e logut
        /// </summary>
        public static void konfiguroNLog(string connectionName = "")
        {
            using (clsDatabaseAdmin admin = string.IsNullOrEmpty(connectionName) ? new clsDatabaseAdmin() : new clsDatabaseAdmin(connectionName))
            {
                DataTable dtLevels = admin.ktheNlogLevels();
                string moduli = "*";
                NLog.Config.LoggingRule logger = new NLog.Config.LoggingRule();

                foreach (DataRow row in dtLevels.Rows)
                {
                    foreach (DataColumn level in dtLevels.Columns)
                    {
                        if (level.ToString() == "Moduli")
                        {
                            moduli = row[level].ToString();
                            logger = LogManager.Configuration.LoggingRules.Where(l => l.LoggerNamePattern == moduli).FirstOrDefault();
                            continue;
                        }

                        if (Convert.ToBoolean(row[level]))
                            logger.EnableLoggingForLevel(LogLevel.FromString(Convert.ToString(level)));
                        else
                            logger.DisableLoggingForLevel(LogLevel.FromString(Convert.ToString(level)));
                    }
                }

                LogManager.ReconfigExistingLoggers();
            }
            //fshin loget me te vjetra se 15 dite
            AlphaWebCommon.Logging.LogHelper.DeleteOldLogFiles(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath + "log", 15);
        }

        /// <summary>
        /// perdoret per te kthyer daten ne formatin e duhur
        /// </summary>
        /// <param name="d"> data</param>
        /// <returns> nje string me daten e konvertuar sipas formatit te kultures</returns>
        public static String ktheDateFormat(DateTime d)
        {
            string dataKonvertuar;
            IFormatProvider culture = new CultureInfo("fr-FR", false);
            dataKonvertuar = d.ToString("d", culture);
            return dataKonvertuar;
        }
        //edi 29/04/2009
        //ky funksion konvertimi per daten do perdoret ngado ne kod. Ndersa ne momentet qe
        //behet nje query nga kodi ne lidhje me databazen, apo edhe ne qeureyt qe behen
        //direkt ne databaze apo ne store procedura (kodi i sql-se) duhet qe te behet nje kovertim
        //ne formen  convert(datetime,@NDERMVITFILLIM,103). Parametrat qe do i kalohen store
        //proccedurave do jene gjithmone te tipit varchar.
        //Kur te ndertohet nje query, gjithmone brenda queryt do behet konvertimi ne formatin e
        //mesiperm.
        //N.q.s kemi te bejme me nje sp, kemi dy menyra: 1. ose do behet  gjithmone konvertimi
        //i dates ne formen convert(datetime,dateshembull,103), ose ne fillim te sp-se do perdoret
        //SET DATEFORMAT dmy;. Te dyja menyrat jane ekuivalente.
        //Kur merret nje fushe nga databaza e tipit datetime, duhet gjithmone te behet konvertimi
        //nga query perkates apo nga sp-ja perkatese : convert(varchar(10),shembulldate,103)
        //shembulldate eshte e nje fushe e nje tabele e tipit datetime

        //edi 10/06/2009
        //per sa i perket formatit te dates do veprohet ne kete menyre. Datat qe do shfaqen ne grida
        //dhe neper textboxe te ndryshme do dalin me formatin e dates qe ka kompjuteri. Ne rastet kur
        //do duhet qe data te kalohet si parameter, ajo fillimisht do kthehet ne formatin e duhur
        //nepermejet funksionit te mesiperm dhe gjithashtu tek query duhet te behet konvertimi perkates
        //(convert(datetime,shembulldata,103) , ku shembulldata eshte string.)

        //edi 11/06/2009
        //duket shtuar nje tag globalization tek web.config, formati i te gjitha datave eshte i njejte
        //pavarsisht nga formati i i dates se serverit. Kjo mbase ben te mundur qe mos jete i nevojshem
        //konvertimi i dates nepermjet funksionit te mesiperm. Formati i dates do jete gjithmone
        //dd/mm/yyyy pavarsisht nga formati i dates se serverit. (nuk do behet sic thame me siper(edi 10/06/2009)
        //sepse serveri mund te kete nje konfigurim date jo te deshirueshem.

        /// <summary>
        ///  perdoret per te zevendesuar karakteret e vecanta ne nje string
        /// </summary>
        /// <param name="text"> teksi qe permban karakteret e vecanta</param>
        /// <returns>string me karakteret e shfaqura ne rregull</returns>
        public static string zevendesoKaraktere(string text)
        {
            text = text.Replace("&#235;", "ë");
            text = text.Replace("&#231;", "ç");
            text = text.Replace("&nbsp;", "");
            text = text.Replace("&#199;", "Ç");
            text = text.Replace("&#203;", "Ë");
            text = text.Replace("&amp;", "&");
            return text;
        }

        /// <summary>
        /// Deserializon parametrat neper raporte. Mos e fshi, po bej search ne entire solution.
        /// </summary>
        /// <param name="param">Parametrat e raportit</param>
        /// <returns>Kthen nje collection me parametrat e raportit</returns>
        public static colParameter DeserializoParametrat(string param)
        {
            return Deserializo<colParameter>(param);
        }

        public static T Deserializo<T>(string param)
        {
            return JsonConvert.DeserializeObject<T>(param);
        }

        /// <summary>
        /// Ruan te dhenat per hapjen e raportit te shpejte. Perdoret ne ambientet e listave per hapje njekohesisht te disa faturave.
        /// </summary>
        /// <param name="teDhenaKoke">Te dhenat e kokes. Liste me objekte qe ruan Id dhe Id e Designit te fatures</param>
        /// <param name="rapEmriReal">Emri real i raportit</param>
        /// <param name="Session">Sesioni</param>
        public static clsMesazh ruajTeDhenaRaportiPerHapjeRaportiTeShpejte(List<object> teDhenaKoke, string rapEmriReal, out int nrRreshtaOk, HttpSessionState Session)
        {
            List<Dictionary<string, string>> teDhenaRapFature = new List<Dictionary<string, string>>();
            foreach (object[] item in teDhenaKoke)
                teDhenaRapFature.Add(new Dictionary<string, string> {
                                { "IdKoka" , Convert.ToString(item[0]) },
                                { "NrDok" , Convert.ToString(item[1]) },
                                { "IdDesign" , Convert.ToString(item[2]) }
                });

            List<Dictionary<string, string>> paDesign = teDhenaRapFature.FindAll(x => string.IsNullOrEmpty(x["IdDesign"]));
            teDhenaRapFature.RemoveFromList(paDesign);

            mySessionObjects.ruajObjectNeSesion(Session, teDhenaRapFature, $"TeDhenaRap_{rapEmriReal}");
            nrRreshtaOk = teDhenaRapFature.Count;
            if (paDesign.Count() > 0)
                return new MesazhGabimi(string.Join(", ", paDesign.Select(x => x["NrDok"])));
            return new MesazhSuksesi();
        }

        /// <summary>
        /// Kthen te dhenat e ruajtura ne session per hapjen e raportit te shpejte.
        /// </summary>
        /// <param name="rapEmriReal">Emri real i raportit</param>
        /// <param name="changedDesign">Tregon nese funksioni therritet gjate ndryshimit te design ose jo</param>
        /// <param name="idFatura">Id e fatures qe do te shfaqet</param>
        /// <param name="idDesign">Id e design te fatures</param>
        /// <param name="Session">Sesioni</param>
        /// <returns>Kthen nje Liste me dictionary qe ruan IdKoka - Id e fatures dhe IdDesign - Id e Designit te fatures</returns>
        public static List<Dictionary<string, string>> merrTeDhenaRaportiPerHapjeRaportiTeShpejte(string rapEmriReal, bool changedDesign, string idFatura, int idDesign, HttpSessionState Session)
        {
            List<Dictionary<string, string>> teDhenaRap;
            teDhenaRap = mySessionObjects.MerrNgaSession<List<Dictionary<string, string>>>(Session, $"TeDhenaRap_{rapEmriReal}");
            mySessionObjects.hiqObjectNeSesion(Session, $"TeDhenaRap_{rapEmriReal}");

            if (teDhenaRap == null || teDhenaRap.Count <= 0)
            {
                teDhenaRap = new List<Dictionary<string, string>>();
                teDhenaRap.Add(new Dictionary<string, string>{
                                { "IdKoka" , idFatura },
                                { "IdDesign" , idDesign.ToString() },
                                { "NrDok", string.Empty}
                });
            }

            if (changedDesign)
                teDhenaRap.ForEach(x => x["IdDesign"] = idDesign.ToString());

            return teDhenaRap;
        }

        /// <summary>
        /// ky eshte funksioni qe percakton theme te nje ambjenti (theme te ambjentit kryesor dhe te gridave JQuery). Duhet thirrur gjithmone vetem te Page_PreInit, perndryshe shkakton runtime error
        /// </summary>
        /// <param name="page"> Page eshte faqja ku do aplikohet tema </param>
        /// <param name="idPerdorues"> Id e perdoruesit te loguar </param>
        /// <param name="themeJQuery"> themeJQuery eshte linku ku percaktohet tema per komponente JQuery; tek faqet qe nuk ka komponente JQuery, ky parameter kalohet null </param>
        public static void percaktoThemeAmbjenteDheJQuery(Page page, int idPerdorues)
        {
            clsThemesAmbjente themeAmbjente = new clsThemesAmbjente();
            themeAmbjente.ktheThemeZgjedhurPerdorues(idPerdorues);
            page.Theme = themeAmbjente.PathDevExpress;
            HtmlLink themeJQuery = (HtmlLink)page.FindControl("themeJquery");
            if (themeJQuery == null)
            {
                themeJQuery = new HtmlLink();
                page.Controls.Add(themeJQuery);
            }
            if (themeJQuery != null)
            {
                string emriThemeJQuery = themeAmbjente.PathJquery;
                themeJQuery.Href = @emriThemeJQuery;
            }
        }

        public static void percaktoThemeAmbjenteDheJQueryMeId(Page page, int idThemeAmbjente)
        {
            clsThemesAmbjente themeAmbjente = new clsThemesAmbjente(idThemeAmbjente);

            page.Theme = themeAmbjente.PathDevExpress;

            HtmlLink themeJQuery = (HtmlLink)page.FindControl("themeJquery");
            if (themeJQuery == null)
            {
                themeJQuery = new HtmlLink();
                page.Controls.Add(themeJQuery);
            }
            if (themeJQuery != null)
            {
                string emriThemeJQuery = themeAmbjente.PathJquery;
                themeJQuery.Href = @emriThemeJQuery;
            }
        }

        /// <summary>
        /// merr kushtin filter te konfigurimit
        /// </summary>
        /// <param name="idkonfigAmbjenti">id e konfigurimit</param>
        /// <returns>kthen objektin e filtrit</returns>
        public static clsFiltraGrida merrFilterDefault(int idkonfigAmbjenti)
        {
            clsKusht kusht = new clsKusht(idkonfigAmbjenti, "FILTER");
            if (kusht.IdKusht == 0 || kusht.Vlera == 0)
                return null;
            return new clsFiltraGrida(kusht.Vlera);
        }

        /// <summary>
        /// Ben tedukshme apo te padukshme menuitem te menuse larte ne baze te skemes dhe eshte sthim apo modifikim
        /// </summary>
        /// <param name="idperdoruesi"></param>
        /// <param name="idskema"></param>
        /// <param name="isNotModifikim"></param>
        /// <param name="status"></param>
        /// <param name="idkokashitje">ne modifikim merret idja e dokumentit, ne rastet e tjera si shtim, apo konvertim nuk merret parasysh</param>
        /// <returns></returns>
        public static bool[] merrMenu(int idperdoruesi, int idskema, bool isNotModifikim, string status, int idkokashitje, string kodkonfig, int lloji, string alternativKushtRD = "Jo", string alternativKushtShfaqPezullo = "Jo")
        {
            if (isNotModifikim)
                idkokashitje = 0;
            bool[] visible = new bool[14];
            int idStatusDok = 0;
            if (lloji == 1)//shitje blerje
                idStatusDok = DbRegjistrim.clsKokaShitje.ktheIdStatusDok(idkokashitje);
            else if (lloji == 38)
                idStatusDok = clsKokaListPagese.ktheIdStatusDok(idkokashitje);
            else if (lloji == 179)
                idStatusDok = ClsBKokaBuxheti.ktheIdStatusDok(idkokashitje);
            else if (lloji == 3 || lloji == 4)
                idStatusDok = clsVeprimBankaKoka.ktheIdStatusDokumenti(idkokashitje);

            if (idskema == 0)// rasti pa skeme
            {
                visible[0] = true;//ruaj
                visible[2] = false;//aprovo
                visible[3] = false;//refuzo
                visible[4] = false;//delego
                visible[5] = false;//comento
                visible[6] = false;//modifiko
                visible[8] = true;//shto
                if (isNotModifikim)
                {
                    visible[1] = true;//draft
                    visible[7] = false;//konverto
                    visible[9] = false;//paguaj
                    visible[10] = false;//fshi
                    visible[11] = ((kodkonfig.Length >= 5 && (kodkonfig.Substring(0, 5) == "VFONE" || kodkonfig.Substring(0, 5) == "USHDD")) || (kodkonfig.Length >= 6 && kodkonfig.Substring(0, 6) == "BAZAAR")) ? true : false;//validim
                    visible[12] = false;//refuzoDraft
                    visible[13] = false;//pezullo
                }
                else
                {
                    visible[1] = (idStatusDok == 0) ? true : false;//draft
                    visible[7] = true;//konverto
                    visible[9] = true;//paguaj
                    visible[10] = true;//fshi
                    visible[11] = false;//validim
                    visible[12] = (alternativKushtRD == "Po" && idStatusDok == 0) ? true : false;//refuzoDraft
                    visible[13] = (idStatusDok == 0 && alternativKushtShfaqPezullo == "Po") ? true : false;//pezullo
                }
                return visible;
            }
            clsTrupiSkemaWorkFlow trup = new clsTrupiSkemaWorkFlow();
            bool kaperdorues = trup.merrTrupSipasKokesDhePerdoruesit(idskema, idperdoruesi);
            if (isNotModifikim)
            {
                visible[0] = false;//ruaj
                visible[1] = true;//draft
                visible[2] = (kaperdorues && trup.Niveli == 1) ? true : false;//aprovo  //true -- perdoruesi i nivelit 1 qe mund te krijoje dok   //false -- perdoruesit e tjere
                visible[3] = false;//refuzo
                visible[4] = false;//delego
                visible[5] = false;//comento
                visible[6] = false;//modifiko
                visible[7] = false;//konverto
                visible[8] = true;//shto
                visible[9] = false;//paguaj
                visible[10] = false;//fshi
                visible[11] = ((kodkonfig.Length >= 5 && (kodkonfig.Substring(0, 5) == "VFONE" || kodkonfig.Substring(0, 5) == "USHDD")) || (kodkonfig.Length >= 6 && kodkonfig.Substring(0, 6) == "BAZAAR")) ? true : false;//validim
                visible[12] = false;//refuzoDraft
                visible[13] = false;//pezullo
            }
            else
            {
                visible[0] = false;//ruaj
                visible[1] = false;//draft
                visible[2] = false;//aprovo
                visible[3] = false;//refuzo
                visible[4] = false;//delego
                visible[6] = false;//modifiko
                visible[7] = false;//konverto
                visible[8] = true;//shto
                visible[9] = false;//paguaj
                visible[10] = false;//fshi
                visible[11] = false;//validim
                visible[12] = (alternativKushtRD == "Po" && idStatusDok == 0) ? true : false;//refuzoDraft
                visible[13] = (idStatusDok == 0 && alternativKushtShfaqPezullo == "Po") ? true : false;//pezullo
                if (string.IsNullOrEmpty(status))// dokumentat e ruajtura draft por pa status
                {
                    visible[1] = true;//draft 
                    visible[2] = (kaperdorues && trup.Niveli == 1) ? true : false;//aprovo   //true -- perdoruesi i nivelit 1 qe mund te krijoje dok    //false -- perdoruesit e tjere
                    visible[5] = false;//comento
                    visible[7] = true;//konverto
                    visible[10] = true;//fshi
                }
                if (status == DbCore.DbRegjistrim.StatusAprovimi.Per_Aprovim.ToString().Replace('_', ' '))
                {
                    bool perdoruesEtape = DbRegjistrim.colEtapeAprovimi.eshtePerdoruesiNeEtapeAprovimiPerKokaShitje(idperdoruesi, idkokashitje, lloji);
                    if (!kaperdorues && !perdoruesEtape)// e hap perdorues jo i skemes dhe jo delegues
                        visible[5] = false;//comento
                    else  ///do merret niveli ku ka arritur dokumenti
                    {
                        visible[5] = true;//comento
                        if (perdoruesEtape) //perdoruesit ne etape
                        {
                            visible[2] = true;//aprovo
                            visible[3] = true;//refuzo
                            visible[4] = true;//delego
                            visible[6] = trup.Modifiko;//modifiko
                        }
                    }
                }
                if (status == DbRegjistrim.StatusAprovimi.Aprovuar.ToString())
                {
                    visible[0] = (kaperdorues && trup.Niveli == 1) ? true : false;//ruaj      //true -- perdoruesi i nivelit 1 qe mund te krijoje dok  //false -- perdoruesit e tjere
                    visible[5] = (!kaperdorues) ? false : true;//comento
                    visible[9] = (idStatusDok == 0) ? false : true;//paguaj
                    visible[10] = true;//fshi
                }
                if (status == DbRegjistrim.StatusAprovimi.Refuzuar.ToString())
                {
                    visible[5] = (!kaperdorues) ? false : true;//comento
                    visible[10] = (kaperdorues && trup.Niveli == 1) ? true : false;//fshi  //true -- perdoruesi i nivelit 1 qe mund te krijoje dok  //false -- perdoruesit e tjere
                }
            }

            return visible;
        }

        /// <summary>
        /// Percakton nese butoni Draft duhet te jete i dukshem apo jo i dukshem ne menu. Perdoret tek regjitrimet e dokumenteve.
        /// </summary>
        /// <param name="isNotModifikim">Modifikim, apo jo dokumenti</param>
        /// <param name="idkokashitje">Id e dokumentit nqs eshte modifikim, 0 perndryshe</param>
        /// <returns></returns>
        public static bool merrMenuVisibleDraft(bool isNotModifikim, int idStatusDok) => isNotModifikim || idStatusDok == 0;

        public static decimal ktheCmim(clsNivelCmimi niv, string kodArtikulli, int idperdorues, clsNjesiArtikulli njesi, clsMonedha mon, string date, decimal kursi, decimal sasi, decimal koefiecient, int idNdermarrje, clsDatabaseInventari db, int iddetajim)
        {
            decimal cmimi = 0;
            if (kursi == 0)
                kursi = 1;
            clsDatabaseAdmin dbadm = new clsDatabaseAdmin(db);
            clsKurset kursimonniveli = new clsKurset(niv.IdMonedha, DateTime.Parse(date), dbadm);
            colCmimeArtikujsh colCmime = new colCmimeArtikujsh();
            colCmime.mbushCmimArtikulliSipasNivelitMeDhePaDetajim(kodArtikulli, niv.IdNivelCmimi.ToString(), idperdorues, idNdermarrje, db, iddetajim);
            cmimi = ktheCmim(colCmime.Find(x => x.IdDetajim == iddetajim), niv, njesi, mon, date, kursi, sasi, koefiecient, kursimonniveli);
            return cmimi;
        }

        private static decimal ktheCmim(clsCmimArtikulli clsCmim, clsNivelCmimi niv, clsNjesiArtikulli njesi, clsMonedha mon, string date, decimal kursi, decimal sasi, decimal koefiecient, clsKurset kursimonniveli)
        {
            if (clsCmim == null || clsCmim.IdCmimArtikulli == 0)
                return 0;

            DateTime dataserver = DateTime.Now;
            decimal kursiNiveli = 1;
            if (kursimonniveli.VleraKursi != 0)
                kursiNiveli = decimal.Parse(kursimonniveli.VleraKursi.ToString());
            if (mon.IdMonedha == niv.IdMonedha)
                kursiNiveli = kursi;
            var dateFillimi = new DateTime(clsCmim.DateFillimi.Year, clsCmim.DateFillimi.Month, clsCmim.DateFillimi.Day);
            var dateMbarimi = new DateTime(clsCmim.DateMbarimi.Year, clsCmim.DateMbarimi.Month, clsCmim.DateMbarimi.Day);
            if (DateTime.Parse(date) >= dateFillimi && DateTime.Parse(date) <= dateMbarimi)// jane hequr meqe per momentin nuk perdoret data e mbarrimit
            {
                if (dataserver.TimeOfDay >= clsCmim.KoheFillimi.TimeOfDay && dataserver.TimeOfDay <= clsCmim.KoheMbarimi.TimeOfDay)
                {
                    if (njesi.IdNjesia == clsCmim.IdNjesia && ((clsCmim.SasiMin == 0 || clsCmim.SasiMin <= sasi) && (clsCmim.SasiMax == 0 || clsCmim.SasiMax >= sasi)))
                        return niv.TeVaruraNgaMonedha ? clsCmim.Cmimi * kursiNiveli / kursi : mon.IdMonedha == clsCmim.IdMonedha ? clsCmim.Cmimi : 0;

                    else if ((clsCmim.SasiMin == 0 || clsCmim.SasiMin <= sasi * koefiecient) && (clsCmim.SasiMax == 0 || clsCmim.SasiMax >= sasi * koefiecient))
                        return niv.TeVaruraNgaMonedha ? clsCmim.Cmimi2 * kursiNiveli / kursi : mon.IdMonedha == clsCmim.IdMonedha ? clsCmim.Cmimi2 : 0;
                }
            }
            //Nqs s'plotesohet asnje nga kushtet e mesiperme, duhet te kthehet cmimi = 0 sepse nuk ka cmim.
            return 0;
        }

        public static object[] merrCmimSipasNivelit(int nivelCmimi, string kodArtikulli, int idPerdoruesi, string njesia, string monedha, string date, decimal kursi, decimal sasi, int idNdermarrje, int llojNiveli, clsDatabaseInventari db, int iddetajim)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(db);
            clsMonedha mon = new clsMonedha();
            mon.mbushMonedhen(monedha, idNdermarrje, dbAdmin);
            clsNjesiArtikulli njesi = new clsNjesiArtikulli(njesia, idNdermarrje, db);
            return merrCmimSipasNivelitMeDetajim(nivelCmimi, kodArtikulli, idPerdoruesi, njesi, mon, date, kursi, sasi, idNdermarrje, llojNiveli, db, iddetajim, true);
        }

        public static object[] merrCmimSipasNivelit(int nivelCmimi, string kodArtikulli, int idPerdoruesi, string njesia, string monedha, string date, decimal kursi, decimal sasi, int idNdermarrje, int llojNiveli, int iddetajim)
        {
            using (clsDatabaseInventari db = new clsDatabaseInventari())
            {
                if (String.IsNullOrEmpty(kodArtikulli))
                {
                    object[] result = new object[2];
                    result[0] = 0;
                    result[1] = false;
                    return result;
                }
                return merrCmimSipasNivelit(nivelCmimi, kodArtikulli, idPerdoruesi, njesia, monedha, date, kursi, sasi, idNdermarrje, llojNiveli, db, iddetajim);
            }
        }

        public static object[] merrCmimSipasNivelitMeDetajim(int nivelCmimi, string kodArtikulli, int idPerdoruesi, clsNjesiArtikulli njesia, clsMonedha monedha, string date, decimal kursi, decimal sasi, int idNdermarrje, int shitjeblerje, clsDatabaseInventari db, int iddetajim, bool merrNivBazeNeseSka)
        {
            var result = merrCmimSipasNivelit(nivelCmimi, kodArtikulli, idPerdoruesi, njesia, monedha, date, kursi, sasi, idNdermarrje, shitjeblerje, db, iddetajim, merrNivBazeNeseSka);
            if (iddetajim > 0 && Convert.ToDouble(result[0]) == 0)
                result = merrCmimSipasNivelit(nivelCmimi, kodArtikulli, idPerdoruesi, njesia, monedha, date, kursi, sasi, idNdermarrje, shitjeblerje, db, 0, merrNivBazeNeseSka);

            return result;
        }

        public static object[] merrCmimSipasNivelit(int nivelCmimi, string kodArtikulli, int idPerdoruesi, clsNjesiArtikulli njesia, clsMonedha monedha, string date, decimal kursi, decimal sasi, int idNdermarrje, int llojNiveli, clsDatabaseInventari db, int iddetajim, bool merrNivBazeNeseSka)
        {//percaktohet cmimi per nivelin.
            ImbLogger.LogTraceShitje("Filloi metoda merr cmim sipas nivelit!");
            object[] result = new object[2];
            decimal cmimi = 0;
            decimal koefArt = clsArtikulli.ktheKoeficent(kodArtikulli, idNdermarrje, db);
            //merr nivelin baze. Ne kete rast nuk varet nga variabli merrNivBazeNeseSka, sepse kjo kalohet zero enkas per te marre nivelin e cmimit baze
            if (nivelCmimi == 0)
            {
                clsNivelCmimi nivelBaze = new clsNivelCmimi();
                nivelBaze.mbushNivelCmimiBaze(idNdermarrje, llojNiveli, db);
                if (nivelBaze.IdNivelCmimi == 0)
                {
                    result[0] = cmimi;
                    result[1] = nivelBaze.BrutoNetoNivelCmimi;
                    return result;
                }
                // therret veten duke i kaluar si parameter nivelin baze, ne menyre qe te marre parasysh edhe cmimet per bijte e nivelit baze ne rastin kur ai eshte prind.
                return merrCmimSipasNivelitMeDetajim(nivelBaze.IdNivelCmimi, kodArtikulli, idPerdoruesi, njesia, monedha, date, kursi, sasi, idNdermarrje, llojNiveli, db, iddetajim, merrNivBazeNeseSka);
            }

            clsNivelCmimi niv = new clsNivelCmimi(nivelCmimi, db);

            if (niv.IdNivelCmimi == 0)
                return ktheCmimDheBrutoNetoNivelCmimiBaze(kodArtikulli, idPerdoruesi, njesia, monedha, date, kursi, sasi, idNdermarrje, llojNiveli, koefArt, db, iddetajim);

            colNiveleCmimesh colNivele = new colNiveleCmimesh();
            colNivele.mbushNivelSipasPrindit(nivelCmimi, db);
            bool prind = true;
            if (colNivele.Count == 0) //rasti kur niveli i cmimit nuk eshte prind
            {
                result = ktheCmimDheBrutoNetoNivelCmimi(niv, kodArtikulli, idPerdoruesi, njesia, monedha, date, kursi, sasi, idNdermarrje, llojNiveli, koefArt, db, iddetajim);
                if (Convert.ToDecimal(result[0]) != 0)
                    return result;
                else if (merrNivBazeNeseSka)
                    return ktheCmimDheBrutoNetoNivelCmimiBaze(kodArtikulli, idPerdoruesi, njesia, monedha, date, kursi, sasi, idNdermarrje, llojNiveli, koefArt, db, iddetajim);
            }
            else //rasti kur eshte prind dhe ka bije
            {
                for (int i = 0; i < colNivele.Count; i++)
                {
                    if (prind && niv.PrioritetiNivelCmimi > colNivele[i].PrioritetiNivelCmimi)//prioriteti me i vogel me mire
                    {
                        //prind = false;
                        result = ktheCmimDheBrutoNetoNivelCmimi(colNivele[i], kodArtikulli, idPerdoruesi, njesia, monedha, date, kursi, sasi, idNdermarrje, llojNiveli, koefArt, db, iddetajim);
                        if (Convert.ToDecimal(result[0]) != 0)
                            return result;
                        else
                            continue;
                    }
                    else
                    {
                        result = ktheCmimDheBrutoNetoNivelCmimi(niv, kodArtikulli, idPerdoruesi, njesia, monedha, date, kursi, sasi, idNdermarrje, llojNiveli, koefArt, db, iddetajim);
                        if (Convert.ToDecimal(result[0]) != 0)
                        {
                            return result;
                        }
                        else
                        {
                            result = ktheCmimDheBrutoNetoNivelCmimi(colNivele[i], kodArtikulli, idPerdoruesi, njesia, monedha, date, kursi, sasi, idNdermarrje, llojNiveli, koefArt, db, iddetajim);
                            if (Convert.ToDecimal(result[0]) != 0)
                                return result;
                        }
                    }
                }

                if (!prind && cmimi == 0)
                    return ktheCmimDheBrutoNetoNivelCmimiBaze(kodArtikulli, idPerdoruesi, njesia, monedha, date, kursi, sasi, idNdermarrje, llojNiveli, koefArt, db, iddetajim);
            }
            if (prind)
            {
                result = ktheCmimDheBrutoNetoNivelCmimi(niv, kodArtikulli, idPerdoruesi, njesia, monedha, date, kursi, sasi, idNdermarrje, llojNiveli, koefArt, db, iddetajim);
                if (Convert.ToDecimal(result[0]) != 0)
                    return result;
                else if (merrNivBazeNeseSka)
                    return ktheCmimDheBrutoNetoNivelCmimiBaze(kodArtikulli, idPerdoruesi, njesia, monedha, date, kursi, sasi, idNdermarrje, llojNiveli, koefArt, db, iddetajim);
            }

            result[0] = cmimi;
            result[1] = niv.BrutoNetoNivelCmimi;
            return result;
        }

        public static object[] ktheCmimDheBrutoNetoNivelCmimiBaze(string kodArtikulli, int idPerdoruesi, clsNjesiArtikulli njesia, clsMonedha monedha, string date, decimal kursi, decimal sasi, int idNdermarrje, int shitjeblerje, decimal koefArt, clsDatabaseInventari db, int iddetajim)
        {
            clsNivelCmimi nivelBaze = new clsNivelCmimi();
            nivelBaze.mbushNivelCmimiBaze(idNdermarrje, shitjeblerje, db);
            return ktheCmimDheBrutoNetoNivelCmimi(nivelBaze, kodArtikulli, idPerdoruesi, njesia, monedha, date, kursi, sasi, idNdermarrje, shitjeblerje, koefArt, db, iddetajim);
        }

        public static object[] ktheCmimDheBrutoNetoNivelCmimi(clsNivelCmimi niveli, string kodArtikulli, int idPerdoruesi, clsNjesiArtikulli njesia, clsMonedha monedha, string date, decimal kursi, decimal sasi, int idNdermarrje, int shitjeblerje, decimal koefArt, clsDatabaseInventari db, int iddetajim)
        {
            object[] result = new object[2];
            decimal cmimi = ktheCmim(niveli, kodArtikulli, idPerdoruesi, njesia, monedha, date, kursi, sasi, koefArt, idNdermarrje, db, iddetajim);
            result[0] = cmimi;
            result[1] = niveli.BrutoNetoNivelCmimi;
            return result;
        }

        /// <summary>
        /// Funksioni gjen per cilen monedhe duhet marre formati i numrit per nje ambjent te caktuar.
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <param name="idKonfigurimi">Id e konfigurimit te ambjentit</param>
        /// <param name="idNdermarrje">Id e ndermarrjes</param>
        /// <param name="idKompon">Id e komponentes</param>
        /// <param name="kodKontrolli">Kodi i kontrollit nga i cili varet perzgjedhja e monedhes, si eshte te tabela T_KONTROLLE. Psh tek shitja eshte btnKlienti, te veprime arka/banka eshte banka_ComboBox </param>
        /// <param name="idObjekt"> idObjekti eshte id e klientit per shitjen, id e arkes/bankes per veprimet me banken, etj. Nese funksioni po thirret ne modifikim, apo klonim, etj. i kalojme id e objektit, perndryshe eshte -1. </param>
        /// <param name="shtim"> Parameter qe tregon neser funksioni po thirret ne shtim apo ne modifikim.</param>
        public static int ktheMonedhePerFormatNumri(int idGjuha, int idKonfigurimi, int idNdermarrje, int idKompon, string kodKontrolli, int idObjekt, bool shtim)
        {
            int idMonedha = 0;
            if (idObjekt == -1 || idObjekt == 0)
            {
                if (shtim && kodKontrolli != " " && kodKontrolli != "")
                //Ne qofte se eshte shtim dhe ne konfigurim eshte zgjedhur nje vlere default per kontrollin nga i cili varet percaktimi i monedhes, atehere do merret formati per monedhen e kontrollit, perndryshe do merret monedha baze.
                {
                    clsAtributeTrupi atrKontrolli = new clsAtributeTrupi();
                    atrKontrolli.mbushAtributSipasKompKonfDheKontrollit(idGjuha, idKonfigurimi, kodKontrolli, idKompon);
                    if (atrKontrolli.VlereDefault != null && atrKontrolli.VlereDefault != "")
                    {
                        switch (idKompon)
                        {
                            case 506: //    shitje / blerje
                                clsKlientFurnitor klienti = new clsKlientFurnitor(int.Parse(atrKontrolli.VlereDefault.ToString()));
                                clsLlogari llogKlienti = new clsLlogari(klienti.IdLlogari);
                                idMonedha = llogKlienti.IdMonedha;
                                break;

                            case 301: //    arka / banka
                                clsBanka arka = new clsBanka();
                                arka.mbushBankeSipasKodit(atrKontrolli.VlereDefault.ToString(), idNdermarrje);
                                //arka.mbushBanke(int.Parse(atrKontrolli.VlereDefault.ToString()));
                                idMonedha = arka.IdMonedhaBanka;
                                break;

                            case 710: //    list pagesa
                                idMonedha = int.Parse(atrKontrolli.VlereDefault.ToString());
                                break;

                            case 522: // fleta doganore
                                idMonedha = DbCore.DbRegjistrim.clsKokaShitje.ktheIdMonedhe(idObjekt);
                                break;

                            default:
                                clsMonedha mondNderm = new clsMonedha();
                                mondNderm.mbushMonedhenENdermarrjes(idNdermarrje);
                                idMonedha = mondNderm.IdMonedha;
                                break;
                        }
                    }
                    else
                    {
                        clsMonedha mondNderm = new clsMonedha();
                        mondNderm.mbushMonedhenENdermarrjes(idNdermarrje);
                        idMonedha = mondNderm.IdMonedha;
                    }
                }
                else
                {
                    clsMonedha mondNderm = new clsMonedha();
                    mondNderm.mbushMonedhenENdermarrjes(idNdermarrje);
                    idMonedha = mondNderm.IdMonedha;
                }
            }
            else //nqs idobjekti eshte i ndryshem nga -1 apo 0, atehere do merret formati i monedhes se lidhur me objektin
            {
                switch (idKompon)
                {
                    case 506: //    shitje / blerje
                        clsKlientFurnitor klienti = new clsKlientFurnitor(idObjekt);
                        clsLlogari llogKlienti = new clsLlogari(klienti.IdLlogari);
                        idMonedha = llogKlienti.IdMonedha; break;
                    case 301: //    arka / banka
                        clsBanka arka = new clsBanka();
                        arka.mbushBanke(idObjekt);
                        idMonedha = arka.IdMonedhaBanka;
                        break;

                    case 710://    list pagesa
                        idMonedha = idObjekt;
                        break;

                    case 522: // fleta doganore
                        DbCore.DbRegjistrim.clsKokaShitje fatura = new DbCore.DbRegjistrim.clsKokaShitje();
                        fatura.mbushKokaShitjeSipasIDPaTrup(idObjekt);
                        idMonedha = fatura.IdMonedha;
                        break;

                    default:
                        clsMonedha mondNderm = new clsMonedha();
                        mondNderm.mbushMonedhenENdermarrjes(idNdermarrje);
                        idMonedha = mondNderm.IdMonedha;
                        break;
                }
            }
            return idMonedha;
        }
        /// <summary>
        /// Gjen formatin e kursit te nje monedhe te caktuar.
        /// </summary>
        /// <param name="idMonedha">Id e monedhes</param>
        /// <returns>Kthen numrin e shifrave pas presjes per formatin e kursit. Ne qofte se monedha nuk ka format kursi, atehere kthen 2.</returns>
        public static int MerrVleraFormatKursi(int idMonedha)
        {
            if (idMonedha < 1)
                return 2;
            else
            {
                string formatNr = clsMonedha.ktheFormatNrMonedheSipasId(idMonedha);
                if (formatNr == "0")
                    return 0;
                else
                    return formatNr.Substring(2).Length;
            }
        }

        /// <summary>
        /// Funksion qe shton shifrat pas presjes.
        /// </summary>
        /// <param name="shifraPasPresjes"> Numri i shifrave pas presjes</param>
        /// <param name="vlDefault">Pjesa e plote e numrit</param>
        /// <returns>Kthen numrin me shifra pas presjes</returns>
        public static string krijoNumer(int shifraPasPresjes, string vlDefault)
        {
            if (shifraPasPresjes == 0)
                return vlDefault;
            else
            {
                string nr = vlDefault + ".";
                for (int i = 0; i < shifraPasPresjes; i++)
                {
                    nr = nr + "0";
                }
                return nr;
            }
        }

        /// <summary>
        /// funksion qe ben heqjen e hapesirave dhe enter ne fillim dhe ne fund te stringut input, nuk lejon me shume se 2 hapesira rresht ne mes
        /// te saj,dhe heq char < > , ;  nga stringu
        /// </summary>
        /// <param name="fjala">i kalohet si parameter stringu te cilit do i hiqen karakteret e panevojshme</param>
        /// <returns>stringu i modifikuar pa karakteret e panevojshme</returns>
        public static string ktheStringunPaHapesira(string fjala, bool paHapesiraNeMesTeFjales) => fjala.RemoveSpaces();

        /// <summary>
        /// Ben kontroll per karakteret e palejuara dhe kthen nje mesazh ne varesi te input
        /// </summary>
        /// <param name="text">fjala qe do kontrollet per karakteret: , ; &lt; &gt; hapsire</param> 
        /// <param name="rm"></param>
        /// <param name="ci"></param>
        /// <param name="lloji">Lloji i inputit qe do validohet</param>
        /// <returns>kthen nje objekt te tipit clsMesazh vetem me karakteret e palejuara, mesazhi duhet te modifikohet sipas rastit ku do te perdoret</returns>
        public static clsMesazh kontrolloKaraktereMeMesazh(string text, FusheKontrolli lloji, bool lejoPresje) => kontrolloKaraktereMeMesazh(text, lloji, MessagesResource.Messages, lejoPresje);

        public static clsMesazh kontrolloKaraktereMeMesazh(string text, FusheKontrolli lloji, IMessagesResource MessageResource, bool lejoPresje)
        {
            clsMesazh mesazh = new clsMesazh();
            var ls = MerrKaraktereTePalejuara(text, lejoPresje);
            if (Regex.IsMatch(text, @"\s{2,}|[\t]"))
                return new clsMesazh(false, MessageResource["msgPershkrimiHapesiraTeNjepasnjeshme"]);
            if ((lloji == FusheKontrolli.Kodi || lloji == FusheKontrolli.Kodbari) && System.Text.RegularExpressions.Regex.IsMatch(text, @"\s+"))
                return new clsMesazh(false, MessageResource["msgKodiHapsira"]);
            if (text.Contains("'"))
                return new clsMesazh(false, MessageResource["msgZevendesimThonjeze"]);
            if (text.Contains("+") && lloji == FusheKontrolli.Kodbari)
                return new clsMesazh(false, MessageResource["msgZevendesimPlusi"]);
            else if (ls.Length > 0)
            {
                switch (lloji)
                {
                    case FusheKontrolli.Kodi:
                        mesazh = new clsMesazh(false, $"{MessageResource["msgKodiNukDuhetTePermbajeKetoKaraktere"]} {ls}!");
                        break;
                    case FusheKontrolli.Pershkrimi:
                        mesazh = new clsMesazh(false, $"{MessageResource["msgPershkrimiNukDuhetTePermbajeKetoKaraktere"]} {ls}!");
                        break;
                    case FusheKontrolli.Kodbari:
                        mesazh = new clsMesazh(false, $"{MessageResource["msgKodbariNukDuhetTePermbajeKetoKaraktere"]} {ls}!");
                        break;
                    case FusheKontrolli.Emri:
                        mesazh = new clsMesazh(false, $"{MessageResource["msgEmriNukDuhetTePermbajeKetoKaraktere"]} {ls}!");
                        break;
                    default:
                        mesazh = new clsMesazh(false, $"{MessageResource["msgNukDuhetTePermbajeKetoKaraktere"]} {ls}!");
                        break;
                }
                return mesazh;
            }
            return new clsMesazh(true);
        }
        /// <summary>
        /// ben kontroll per karakteret speciale ne text
        /// </summary>
        /// <param name="text">inputi qe do te kontrollojm</param>
        /// <returns>Kthen nje string me karakteret e palejuara nga texti qe do kontrollohet</returns>
        public static string MerrKaraktereTePalejuara(string text, bool lejoPresje)
        {
            StringBuilder str = new StringBuilder();
            string SpecialChar = lejoPresje ? @"[';<>]" : @"[,';<>]";
            //switch (lloji)
            //{
            //    case FusheKontrolli.Pershkrimi:
            //    case FusheKontrolli.Emri:
            //        SpecialChar = @"[';<>]";
            //        break;
            //    default:
            //        SpecialChar = @"[,';<>]";
            //        break;
            //}
            Regex rgx = new Regex(SpecialChar, RegexOptions.IgnoreCase);
            MatchCollection matches = rgx.Matches(text);
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                    str.Append(match.Value.ToString());
            }
            return str.ToString();
        }
        //TODO GETSON kaloje ne klase  me vete (clsLogin) dhe rregulloje se eshte shume keq 

        public static clsMesazh validoPunonjesinNeLogin(HttpContext httpContext, string username, string password, bool rememberMeSet, string data, bool webServise, ResourceManager rm, CultureInfo ci, clsPunonjes user, string ndermarrjaWS = "", string ipKasaWS = "", string emerPrinteriWS = "", string dyqaniWS = "")
        {
            string arsyeLoginFail = "";
            //clsMesazh validUser;
            clsMesazh mesazh;

            int loginCount = mySessionObjects.merrLoginCount(httpContext.Session);
            int maxLoginAttempts = mySessionObjects.merrMaxLoginAttempts(httpContext.Session);
            Dictionary<string, Dictionary<int, int>> loginAttempts = (Dictionary<string, Dictionary<int, int>>)HttpContext.Current.Application["loginAttempts"];
            Dictionary<string, string> useraAktiv = (Dictionary<string, string>)HttpContext.Current.Application["userAktiv"];
            // string failureText = STR_perdoruesIPasakte;
            mySessionObjects.ruajEmerPerdoruesNeSesion(httpContext.Session, username);
            //i jepen vlera obj perdorues sipas vlerave ne login
            // DbCore.DbAdmin.clsPerdorues user = new DbCore.DbAdmin.clsPerdorues(username);

            #region
            //NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            //foreach (NetworkInterface iface in interfaces)
            //{
            //    IPInterfaceProperties properties = iface.GetIPProperties();

            //    foreach (UnicastIPAddressInformation address in properties.UnicastAddresses)
            //    {
            //        Console.WriteLine("{0} (Mask: {1})", address.Address, address.IPv4Mask);
            //    }
            //}

            //string clientDt = clientDate.Value;
            //string[] clientDateParts = clientDt.Split('/');
            //DateTime clientDate = new DateTime(Convert.ToInt32(clientDateParts[2].Split(' ')[0]), Convert.ToInt32(clientDateParts[1]), Convert.ToInt32(clientDateParts[0]));
            //DateTime serverDate = DateTime.Now;
            //kthen col me obj te perdoruesve me username sa ai i perdoruesit te loguar
            #endregion
            if (user.IdPunonjes != 0)
            {
                clsMesazh lejoLogin = kontrolloNrMaxTentativaLogin(httpContext.Session, loginAttempts, username, null, user.IdPunonjes, loginCount, maxLoginAttempts, rm, ci);
                if (!lejoLogin.Status)
                {
                    return new clsMesazh(false, lejoLogin.PershkrimMesazhi);
                }

                if (!user.Aktiv)
                {
                    clsTrackUser.shtoUserLoginFail("Përdoruesi nuk është aktiv!", username, httpContext.Session.SessionID, httpContext.Request.UserHostAddress);
                    return new clsMesazh(false, rm.GetString("msgLoginPerdoruesiNukEshteAktiv", ci));
                }
                mesazh = new clsMesazh(false);
                if (!webServise)
                {
                    var domainName = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.DOMAINNAME);
                    if (string.IsNullOrEmpty(domainName) && (user.Password.Equals(PasswordHelper.HashLogin(username, password)) || user.Password.Equals(PasswordHelper.HashLogin(char.ToUpper(username[0]) + username.Substring(1), password)) || user.Password.Equals(PasswordHelper.HashLogin(char.ToLower(username[0]) + username.Substring(1), password))))
                    {
                        mesazh = new clsMesazh(true);
                    }
                    else
                    {
                        bool autentifikim = false;
                        try
                        {
                            autentifikim = new LdapAuthentication("LDAP://" + domainName).IsAuthenticated(domainName, username, password);
                        }
                        catch (Exception ex)
                        {
                            ImbLogger.LogTrace($"LdapAuthentication nuk eshte i sakte! -> Domain name :LDAP:// { domainName} - username:{username} - exception: {ex}");
                            autentifikim = false;
                        }
                        if (!string.IsNullOrWhiteSpace(domainName) && autentifikim)
                        {
                            ImbLogger.LogTrace($"Autentifikimi i sakte! -> Domain name :LDAP:// { domainName} - username:{username}");
                            mesazh = new clsMesazh(true);
                        }
                        else
                        {
                            arsyeLoginFail = "Autentifikimi nuk eshte i sakte.";
                            ImbLogger.LogTrace($"Autentifikimi nuk eshte i sakte! -> Domain name :LDAP:// { domainName} - username:{username}");
                            clsMesazh mesazhshtoLoginFail = shtoLoginFail(httpContext.Session, null, loginAttempts, username, loginCount, arsyeLoginFail, user.IdPunonjes, maxLoginAttempts, rm, ci);
                            if (!mesazhshtoLoginFail.Status)
                                return mesazhshtoLoginFail;
                            return new clsMesazh(false, rm.GetString("msgLoginUsernameOsePassIPasakte", ci));
                        }
                    }
                }
                else
                {
                    mesazh = new clsMesazh(true);
                }
                if (mesazh)
                {
                    mySessionObjects.ruajIsLoggedIn(httpContext.Session, "Yes");
                    mySessionObjects.ruajEmerPerdoruesiNeSesion(httpContext.Session, user.Emer + " " + user.Mbiemer
);

                    //ruhet id e perdoruesit sepse nese perdoruesi nuk zgjedh asnje ndermarrje duhet ta
                    //ridrejtojme ne faqen Login_Ndermarrje dhe ti paraqesim listen e ndermarrjeve ku
                    //ky perdorues ka te drejta
                    mySessionObjects.ruajIdPerdoruesiNeSesion(httpContext.Session, user.IdPunonjes.ToString());
                    mySessionObjects.ruajIdNdermarrjeNeSesion(httpContext.Session, user.IdNdermarje.ToString());

                    mySessionObjects.ruajNdermarrjenNgaWebServisi(httpContext.Session, ndermarrjaWS);
                    mySessionObjects.ruajIpKasaNgaWebServisi(httpContext.Session, ipKasaWS);
                    mySessionObjects.ruajPrinterNgaWebServisi(httpContext.Session, emerPrinteriWS);
                    mySessionObjects.ruajDyqaninNgaWebServisi(httpContext.Session, dyqaniWS);
                    clsNdermarrje nderm = new clsNdermarrje(user.IdNdermarje);

                    // DbCore.DbAdmin.clsViti vit = new DbCore.DbAdmin.clsViti(nderm.IdViti);
                    clsNdermarrjeViti vit = new clsNdermarrjeViti();
                    // vit.mbushNdermarrjeVitiSipasNdermarjesDheVitit(nderm.IdNdermarrje, nderm.IdViti);
                    clsViti viti = new clsViti();
                    vit.mbushNdermarrjeVitiSipasNdermarjesDheVitit(nderm.IdNdermarrje, clsViti.ktheIdVitPerNdermarrjenSipasKodit(user.IdNdermarje, DateTime.Now.Year.ToString()));
                    //   vit.mbushVitetMet(vit.KodiViti, user.IdNdermarje);
                    merrNdermarrjenPerPune(httpContext.Session, user.IdPunonjes, user.IdNdermarje, nderm.NdermarrjeKodi, vit.Viti.ToString(), vit.IdNderViti, null);

                }
                else
                {
                    ruajNrLoginFailAttempt(null, loginAttempts, username, loginCount);
                    clsMesazh arriturNrMaxTentativa = kontrolloNrMaxTentativaLogin(httpContext.Session, loginAttempts, username, null, user.IdPunonjes, loginCount, maxLoginAttempts, rm, ci);
                    if (!arriturNrMaxTentativa.Status)
                    {
                        return new clsMesazh(false, arriturNrMaxTentativa.PershkrimMesazhi);
                    }
                    return new clsMesazh(false, rm.GetString("msgLoginUsernameOsePassIPasakte", ci));
                }


            }
            else
            {

                clsMesazh mesazhshtoLoginFail = shtoLoginFail(httpContext.Session, null, loginAttempts, username, loginCount, "Autentifikimi nuk eshte i sakte.", user.IdPunonjes, maxLoginAttempts, rm, ci);
                if (!mesazhshtoLoginFail.Status)
                    return mesazhshtoLoginFail;
                return new clsMesazh(false, rm.GetString("msgLoginUsernameOsePassIPasakte", ci));
            }
            return mesazh;
        }

        public static void krijoTicket(HttpSessionState Session, string username)
        {
            bool rememberMeSet = false;
            FormsAuthentication.Initialize();
            FormsAuthenticationTicket tkt;
            string cookiestr;
            HttpCookie ck;
            tkt = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(Session.Timeout), rememberMeSet, "my custom data");
            cookiestr = FormsAuthentication.Encrypt(tkt);
            ck = new HttpCookie(FormsAuthentication.FormsCookieName, cookiestr);
            if (rememberMeSet)
                ck.Expires = tkt.Expiration;
            ck.Path = FormsAuthentication.FormsCookiePath;
            HttpContext.Current.Response.Cookies.Add(ck);
        }

        /// <summary>
        /// Merr tokenin OTP te userit.
        /// </summary>
        /// <param name="username">Username i userit.</param>
        public static string merrTokeninOtpTeUserit(string username)
        {
            var user = new clsPerdorues(username);
            if (user.IdPerdorues != 0)
            {
                return user.Otp_Token;
            }

            return null;
        }

        /// <summary>
        /// ben te gjitha kontrollet dhe validimet per perdoruesin qe tenton te hyje ne sistem
        /// </summary>
        /// <param name="Session"></param>
        /// <param name="username">username qe plotsohet ne formen e login nga perdoruesi</param>
        /// <param name="password">passwordi qe plotesohet ne formen e login</param>
        /// <param name="rememberMeSet"></param>
        /// <param name="data"></param>
        /// <param name="webServise"></param>
        /// <param name="ndermarrjaWS"></param>
        /// <param name="ipKasaWS"></param>
        /// <param name="emerPrinteriWS"></param>
        /// <param name="dyqaniWS"></param>
        /// <param name="authenticationFromRestart">Kur perdoruesi ndryshon gjuhen dhe password ne kete rast nuk njihet ndaj kontrollohet direkt i kriptuar</param>
        /// <returns>kthen nje objekt clsMesazh</returns>
        public static clsMesazh validoPerdoruesinNeLogin(HttpContext httpContext, string username, string password, bool rememberMeSet, string data, bool webServise, ResourceManager rm, CultureInfo ci, string ndermarrjaWS = "", string ipKasaWS = "", string emerPrinteriWS = "", string dyqaniWS = "", bool authenticationFromRestart = false)
        {
            //string failureText;
            string arsyeLoginFail = "";
            //clsMesazh validUser;
            clsMesazh mesazh;
            int loginCount = mySessionObjects.merrLoginCount(httpContext.Session);
            int maxLoginAttempts = mySessionObjects.merrMaxLoginAttempts(httpContext.Session);
            var loginAttempts = (Dictionary<string, Dictionary<int, int>>)httpContext.Application["loginAttempts"];
            var useraAktiv = (Dictionary<string, string>)httpContext.Application["userAktiv"];
            var user = new clsPerdorues(username);
            if (user.IdPerdorues != 0)
            {
                clsMesazh msgSkadimLicence = clsLicenca.KontrolloSkadiminLicences(user.IdPerdorues, rm, ci);
                if (!msgSkadimLicence.Status)
                {
                    if (msgSkadimLicence.PershkrimMesazhi == "Problem ne validimin e licences!")
                        logout(httpContext.Session, true, "problemLicenca");
                    else logout(httpContext.Session, true, "perfundoiLicenca");
                    return msgSkadimLicence;
                }
                var konfig = new clsKonfigurimeFjalekalimi(user.IdPerdorues);
                //if (user.KontrollPassword)
                //{
                clsMesazh lejoLogin = kontrolloNrMaxTentativaLogin(httpContext.Session, loginAttempts, username, konfig, user.IdPerdorues, loginCount, maxLoginAttempts, rm, ci);
                if (!lejoLogin) return new clsMesazh(false, lejoLogin.PershkrimMesazhi);

                // }

                bool isValidDate = kontrolloDatenKlientServer(data);
                if (!isValidDate)
                {
                    arsyeLoginFail = "Data e klientit dhe e serverit kane diference te pakten 1 dite.";
                    clsMesazh mesazhshtoLoginFail = shtoLoginFail(httpContext.Session, konfig, loginAttempts, username, loginCount, arsyeLoginFail, user.IdPerdorues, maxLoginAttempts, rm, ci);
                    if (!mesazhshtoLoginFail.Status)
                        return mesazhshtoLoginFail;
                    return new clsMesazh(false, rm.GetString("msgLoginProblemMeDatenEKompjutert", ci));
                }
                if (user.PerdoruesIKycur)
                {
                    clsTrackUser.shtoUserLoginFail("Perdoruesi eshte i kycur", username, httpContext.Session.SessionID, httpContext.Request.UserHostAddress);
                    return new clsMesazh(false, rm.GetString("msgLoginPerdoruesiEshteIKycurNukKeniTeDrejtePerTuLoguar", ci));
                }
                if (!user.PerdoruesAktiv)
                {
                    clsTrackUser.shtoUserLoginFail("Përdoruesi nuk është aktiv!", username, httpContext.Session.SessionID, httpContext.Request.UserHostAddress);
                    return new clsMesazh(false, rm.GetString("msgLoginPerdoruesiNukEshteAktiv", ci));
                }
                mesazh = new clsMesazh(false);
                if (!webServise)
                {
                    if ((!authenticationFromRestart && PasswordHelper.ValidoPassword(username, password, user.PerdoruesPassword))
                        ||
                        (authenticationFromRestart && user.PerdoruesPassword.Equals(password))
                       )
                    {
                        mesazh = new clsMesazh(true);
                    }
                    else
                    {
                        bool autentifikim = false;
                        var domainName = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.DOMAINNAME);
                        try
                        {
                            autentifikim = new LdapAuthentication("LDAP://" + domainName).IsAuthenticated(domainName, username, password);
                        }
                        catch (Exception ex)
                        {
                            ImbLogger.LogTrace($"LdapAuthentication nuk eshte i sakte! -> Domain name :LDAP:// { domainName} - username:{username} - exception: {ex}");
                            autentifikim = false;
                        }
                        if (!string.IsNullOrWhiteSpace(domainName) && autentifikim)
                        {
                            mesazh = new clsMesazh(true);
                            ImbLogger.LogTrace($"Autentifikimi i sakte! -> Domain name :LDAP:// { domainName} - username:{username}");
                        }
                        else
                        {
                            arsyeLoginFail = "Autentifikimi nuk eshte i sakte.";
                            ImbLogger.LogTrace($"Autentifikimi nuk eshte i sakte. -> Domain name :LDAP:// { domainName} - username:{username}");
                            clsMesazh mesazhshtoLoginFail = shtoLoginFail(httpContext.Session, konfig, loginAttempts, username, loginCount, arsyeLoginFail, user.IdPerdorues, maxLoginAttempts, rm, ci);
                            if (!mesazhshtoLoginFail.Status)
                                return mesazhshtoLoginFail;
                            return new clsMesazh(false, rm.GetString("msgLoginUsernameOsePassIPasakte", ci));
                        }
                    }
                }
                else
                {
                    mesazh = new clsMesazh(true);
                }

                if (mesazh.Status)
                {
                    // kjo ndodh pasi useri eshte logged in

                    return RuajTrackUser(httpContext, username, rm, ci, ndermarrjaWS, ipKasaWS, emerPrinteriWS, dyqaniWS, loginCount, maxLoginAttempts, loginAttempts, user, konfig);
                }
            }
            else
            {
                clsMesazh mesazhshtoLoginFail = shtoLoginFail(httpContext.Session, null, loginAttempts, username, loginCount, "Autentifikimi nuk eshte i sakte.", user.IdPerdorues, maxLoginAttempts, rm, ci);
                if (!mesazhshtoLoginFail.Status)
                    return mesazhshtoLoginFail;
                return new clsMesazh(false, rm.GetString("msgLoginUsernameOsePassIPasakte", ci));
            }
            return mesazh;
        }
        public static clsMesazh validoPerdoruesinNeLoginWithFirebase(HttpContext httpContext, string username, string email, string password, bool rememberMeSet, string data, bool webServise, ResourceManager rm, CultureInfo ci, string ndermarrjaWS = "", string ipKasaWS = "", string emerPrinteriWS = "", string dyqaniWS = "", bool authenticationFromRestart = false)
        {
            //string failureText;
            string arsyeLoginFail = "";
            //clsMesazh validUser;
            clsMesazh mesazh;
            int loginCount = mySessionObjects.merrLoginCount(httpContext.Session);
            int maxLoginAttempts = mySessionObjects.merrMaxLoginAttempts(httpContext.Session);
            var loginAttempts = (Dictionary<string, Dictionary<int, int>>)httpContext.Application["loginAttempts"];
            var useraAktiv = (Dictionary<string, string>)httpContext.Application["userAktiv"];
            var user = new clsPerdorues(username, email, true);
            if (user.IdPerdorues != 0)
            {
                clsMesazh msgSkadimLicence = clsLicenca.KontrolloSkadiminLicences(user.IdPerdorues, rm, ci);
                if (!msgSkadimLicence.Status)
                {
                    if (msgSkadimLicence.PershkrimMesazhi == "Problem ne validimin e licences!")
                        logout(httpContext.Session, true, "problemLicenca");
                    else logout(httpContext.Session, true, "perfundoiLicenca");
                    return msgSkadimLicence;
                }
                var konfig = new clsKonfigurimeFjalekalimi(user.IdPerdorues);
                //if (user.KontrollPassword)
                //{
                clsMesazh lejoLogin = kontrolloNrMaxTentativaLogin(httpContext.Session, loginAttempts, username, konfig, user.IdPerdorues, loginCount, maxLoginAttempts, rm, ci);
                if (!lejoLogin) return new clsMesazh(false, lejoLogin.PershkrimMesazhi);

                // }

                bool isValidDate = kontrolloDatenKlientServer(data);
                if (!isValidDate)
                {
                    arsyeLoginFail = "Data e klientit dhe e serverit kane diference te pakten 1 dite.";
                    clsMesazh mesazhshtoLoginFail = shtoLoginFail(httpContext.Session, konfig, loginAttempts, username, loginCount, arsyeLoginFail, user.IdPerdorues, maxLoginAttempts, rm, ci);
                    if (!mesazhshtoLoginFail.Status)
                        return mesazhshtoLoginFail;
                    return new clsMesazh(false, rm.GetString("msgLoginProblemMeDatenEKompjutert", ci));
                }
                if (user.PerdoruesIKycur)
                {
                    clsTrackUser.shtoUserLoginFail("Perdoruesi eshte i kycur", username, httpContext.Session.SessionID, httpContext.Request.UserHostAddress);
                    return new clsMesazh(false, rm.GetString("msgLoginPerdoruesiEshteIKycurNukKeniTeDrejtePerTuLoguar", ci));
                }
                if (!user.PerdoruesAktiv)
                {
                    clsTrackUser.shtoUserLoginFail("Përdoruesi nuk është aktiv!", username, httpContext.Session.SessionID, httpContext.Request.UserHostAddress);
                    return new clsMesazh(false, rm.GetString("msgLoginPerdoruesiNukEshteAktiv", ci));
                }
                mesazh = new clsMesazh(false);
                if (!webServise)
                {
                    if ((!authenticationFromRestart && PasswordHelper.ValidoPasswordWithFirebase(username, password, user.PerdoruesPassword))
                        ||
                        (authenticationFromRestart && user.PerdoruesPassword.Equals(password))
                       )
                    {
                        mesazh = new clsMesazh(true);
                    }
                    else
                    {
                        bool autentifikim = false;
                        var domainName = clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.DOMAINNAME);
                        try
                        {
                            autentifikim = new LdapAuthentication("LDAP://" + domainName).IsAuthenticated(domainName, username, password);
                        }
                        catch (Exception ex)
                        {
                            ImbLogger.LogTrace($"LdapAuthentication nuk eshte i sakte! -> Domain name :LDAP:// { domainName} - username:{username} - exception: {ex}");
                            autentifikim = false;
                        }
                        if (!string.IsNullOrWhiteSpace(domainName) && autentifikim)
                        {
                            mesazh = new clsMesazh(true);
                            ImbLogger.LogTrace($"Autentifikimi i sakte! -> Domain name :LDAP:// { domainName} - username:{username}");
                        }
                        else
                        {
                            arsyeLoginFail = "Autentifikimi nuk eshte i sakte.";
                            ImbLogger.LogTrace($"Autentifikimi nuk eshte i sakte. -> Domain name :LDAP:// { domainName} - username:{username}");
                            clsMesazh mesazhshtoLoginFail = shtoLoginFail(httpContext.Session, konfig, loginAttempts, username, loginCount, arsyeLoginFail, user.IdPerdorues, maxLoginAttempts, rm, ci);
                            if (!mesazhshtoLoginFail.Status)
                                return mesazhshtoLoginFail;
                            return new clsMesazh(false, rm.GetString("msgLoginUsernameOsePassIPasakte", ci));
                        }
                    }
                }
                else
                {
                    mesazh = new clsMesazh(true);
                }

                if (mesazh.Status)
                {
                    // kjo ndodh pasi useri eshte logged in

                    return RuajTrackUser(httpContext, username, rm, ci, ndermarrjaWS, ipKasaWS, emerPrinteriWS, dyqaniWS, loginCount, maxLoginAttempts, loginAttempts, user, konfig);
                }
            }
            else
            {
                clsMesazh mesazhshtoLoginFail = shtoLoginFail(httpContext.Session, null, loginAttempts, username, loginCount, "Autentifikimi nuk eshte i sakte.", user.IdPerdorues, maxLoginAttempts, rm, ci);
                if (!mesazhshtoLoginFail.Status)
                    return mesazhshtoLoginFail;
                return new clsMesazh(false, rm.GetString("msgLoginUsernameOsePassIPasakte", ci));
            }
            return mesazh;
        }


        /// <summary>
        /// ben te gjitha kontrollet dhe validimet per perdoruesin qe tenton te hyje ne sistem
        /// </summary>
        /// <param name="Session"></param>
        /// <param name="username">username qe plotsohet ne formen e login nga perdoruesi</param>
        /// <param name="password">passwordi qe plotesohet ne formen e login</param>
        /// <returns>kthen nje objekt clsMesazh</returns>
        public static bool validoPerdoruesUsernmaePass(HttpContext httpContext, string username, string password)
        {
            var user = new clsPerdorues(username);
            if (user.IdPerdorues != 0)
            {
                if (user.PerdoruesIKycur)
                {
                    clsTrackUser.shtoUserLoginFail("Perdoruesi eshte i kycur", username, httpContext.Session.SessionID, httpContext.Request.UserHostAddress);
                    return false;
                }
                if (!user.PerdoruesAktiv)
                {
                    clsTrackUser.shtoUserLoginFail("Përdoruesi nuk është aktiv!", username, httpContext.Session.SessionID, httpContext.Request.UserHostAddress);
                    return false;
                }

                if (PasswordHelper.ValidoPassword(username, password, user.PerdoruesPassword) ||
                    user.PerdoruesPassword.Equals(password))
                    return true;

                return false;
            }

            return false;
        }

        private static clsMesazh RuajTrackUser(HttpContext httpContext, string username, ResourceManager rm, CultureInfo ci, string ndermarrjaWS, string ipKasaWS, string emerPrinteriWS, string dyqaniWS, int loginCount, int maxLoginAttempts, Dictionary<string, Dictionary<int, int>> loginAttempts, clsPerdorues user, clsKonfigurimeFjalekalimi konfig)
        {
            clsTrackUser trackUser = new clsTrackUser(user.IdPerdorues, MyConnectionsManager.GetSelectedConNameServer(httpContext.Session.SessionID));
            if (trackUser.aktiv)
            {
                clsLicenca licence = new clsLicenca();
                licence.mbushLicencen(user.IdPerdorues);
                if (licence.BlockMultipleLogin || konfig.BllokoLogin)
                    httpContext.Application[trackUser.SessionID] = "yes";
            }
            trackUser = new clsTrackUser(MyConnectionsManager.GetSelectedConNameServer(httpContext.Session.SessionID));
            trackUser.SessionID = httpContext.Session.SessionID;
            trackUser.aktiv = true;
            trackUser.LoginDatetime = DateTime.Now.ToString();
            trackUser.LogoutDatetime = DateTime.Now.ToString();
            trackUser.idPerdorues = user.IdPerdorues;
            trackUser.IpAdress = httpContext.Request.UserHostAddress;
            if (trackUser.ruaj())
            {
                ruajTedhenatPasValidimitTeLoginUserit(httpContext.Session, username, user, false, ndermarrjaWS, ipKasaWS, emerPrinteriWS, dyqaniWS);
                if (user.KontrollPassword)
                {
                    clsMesazh msg = kontrolloPassword(konfig, user.IdPerdorues, httpContext, rm, ci);
                    if (!msg.Status)
                        return msg;
                }
                ImbLogger.Info($"Login! U logua perdoruesi me username: {username } dhe sessionid  {httpContext.Session.SessionID } !");
                httpContext.Response.Cookies.Set(new HttpCookie("loadingUrl", clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.LOADING_URL)));
                konfiguroNLog();
                return new clsMesazh(true);
            }
            else
            {
                ruajNrLoginFailAttempt(httpContext.Session, konfig, loginAttempts, username, loginCount);
                clsMesazh arriturNrMaxTentativa = kontrolloNrMaxTentativaLogin(httpContext.Session, loginAttempts, username, konfig, user.IdPerdorues, loginCount, maxLoginAttempts, rm, ci);
                if (!arriturNrMaxTentativa.Status)
                {
                    return new clsMesazh(false, arriturNrMaxTentativa.PershkrimMesazhi);
                }
                return new clsMesazh(false, rm.GetString("msgLoginUsernameOsePassIPasakte", ci));
            }
        }

        public static int validoUserNgaResetimPass(HttpSessionState Session, HttpResponse response, string username, CultureInfo ci, ResourceManager rm, string ndermarrjaWS = "", string ipKasaWS = "", string emerPrinteriWS = "", string dyqaniWS = "")
        {
            clsPerdorues user = new clsPerdorues(username);
            clsMesazh msgSkadimLicence = clsLicenca.KontrolloSkadiminLicences(user.IdPerdorues, rm, ci);
            if (!msgSkadimLicence.Status)
                if (msgSkadimLicence.PershkrimMesazhi == "Problem ne validimin e licences!")
                    logout(Session, true, "problemLicenca");
                else logout(Session, true, "perfundoiLicenca");
            clsTrackUser trackUser = new clsTrackUser(user.IdPerdorues, MyConnectionsManager.GetSelectedConNameServer(Session.SessionID));

            if (trackUser.aktiv)
            {
                clsLicenca licence = new clsLicenca();
                licence.mbushLicencen(user.IdPerdorues);
                clsKonfigurimeFjalekalimi konfig = new clsKonfigurimeFjalekalimi(user.IdPerdorues);
                if (licence.BlockMultipleLogin || konfig.BllokoLogin)
                    HttpContext.Current.Application[trackUser.SessionID] = "yes";
            }
            trackUser = new clsTrackUser(MyConnectionsManager.GetSelectedConNameServer(Session.SessionID));
            trackUser.SessionID = Session.SessionID;
            trackUser.aktiv = true;
            trackUser.LoginDatetime = DateTime.Now.ToString();
            trackUser.LogoutDatetime = DateTime.Now.ToString();
            trackUser.idPerdorues = user.IdPerdorues;
            trackUser.IpAdress = HttpContext.Current.Request.UserHostAddress;
            if (trackUser.ruaj())
            {
                konfiguroNLog();
                return ruajTedhenatPasValidimitTeLoginUserit(HttpContext.Current.Session, username, user, false, ndermarrjaWS, ipKasaWS, emerPrinteriWS, dyqaniWS);

            }
            return user.IdPerdorues;
        }


        public static clsMesazh dergoPinEpaySlip(HttpSessionState Session, string username, int idPerdoruesi, int idNdermarrje)
        {
            clsGjenerimPIN piniRi = new clsGjenerimPIN(idPerdoruesi, idNdermarrje);
            if (piniRi.ruaj())
            {
                //e.Authenticated = true;

                string nrTel = clsPunonjes.merrNumerTelefoneSipasUsername(username);
                var dergoPinMeSMS = clsServerConfiguration.LexoKonfigurimSipasKey<int>(ServerKonfigKey.DERGO_PIN_SMS);
                if (dergoPinMeSMS == 1)
                    return DergoPinMeSMS(piniRi, nrTel);
                return new MesazhSuksesi();

            }
            else
            {
                ImbLogger.Error("Login1_Authenticate(object sender, AuthenticateEventArgs e) - Pati nje gabim gjate gjenerimit te PIN-it! Ju lutem provoni perseri");
                return new clsMesazh(false, "Pati nje gabim gjate gjenerimit te PIN-it! Ju lutem provoni perseri");
            }
        }

        private static clsMesazh DergoPinMeSMS(clsGjenerimPIN piniRi, string nrTel)
        {

            using (VFALSendSMSGateWay dergoSMS = new VFALSendSMSGateWay())
            {
                try
                {
                    dergoSMS.Url = Convert.ToString(ConfigurationManager.AppSettings["VodSendSMS_Service_URL"]);
                    dergoSMS.SendSMS(Convert.ToString(ConfigurationManager.AppSettings["SendSMSVodUser"]), Convert.ToString(ConfigurationManager.AppSettings["SendSMSVodPassword"]), Convert.ToString(ConfigurationManager.AppSettings["SendSMSVodOriginator"]), nrTel, String.Format("{0}\r\n{1}", MessagesResource.Messages["kodiPinPerSherbiminEpayslip"], piniRi.KodiPIN));
                    return new clsMesazh(true);
                }
                catch (Exception err)
                {
                    ImbLogger.Error("Pati nje gabim gjate dergimit te PIN-it! " + err);
                    return new clsMesazh(false, "Pati nje gabim gjate dergimit te PIN-it! " + err);
                }
            }
        }

        public static int ruajTedhenatPasValidimitTeLoginUserit(HttpSessionState Session, string username, clsPerdorues user = null, bool punonjes = false, string ndermarrjaWS = "", string ipKasaWS = "", string emerPrinteriWS = "", string dyqaniWS = "")
        {
            mySessionObjects.ruajIsLoggedIn(Session, "Yes");
            mySessionObjects.ruajNdermarrjenNgaWebServisi(Session, ndermarrjaWS);
            mySessionObjects.ruajIpKasaNgaWebServisi(Session, ipKasaWS);
            mySessionObjects.ruajPrinterNgaWebServisi(Session, emerPrinteriWS);
            mySessionObjects.ruajDyqaninNgaWebServisi(Session, dyqaniWS);
            int idPerdoruesi;
            if (punonjes)
            {
                clsPunonjes punonjesi = new clsPunonjes(username);
                mySessionObjects.ruajEmerPerdoruesiNeSesion(Session, punonjesi.Emer + " " + punonjesi.Mbiemer);
                //nuk duket korrekte TOCHECK GETSON
                mySessionObjects.ruajIdPerdoruesiNeSesion(Session, punonjesi.IdPunonjes.ToString());
                mySessionObjects.ruajIdNdermarrjeNeSesion(Session, punonjesi.IdNdermarje.ToString());
                krijoTicket(Session, punonjesi.Username);
                idPerdoruesi = punonjesi.IdPunonjes;
            }
            else
            {
                mySessionObjects.ruajEmerPerdoruesiNeSesion(Session, user.EmriPerdorues + " " + user.MbiemriPerdorues);
                //ruhet id e perdoruesit sepse nese perdoruesi nuk zgjedh asnje ndermarrje duhet ta
                //ridrejtojme ne faqen Login_Ndermarrje dhe ti paraqesim listen e ndermarrjeve ku
                //ky perdorues ka te drejta
                mySessionObjects.ruajIdPerdoruesiNeSesion(Session, user.IdPerdorues.ToString());
                mySessionObjects.ruajPerdoruesNeSesion(Session, user);
                krijoTicket(Session, user.PerdoruesUsername);
                idPerdoruesi = user.IdPerdorues;
            }

            return idPerdoruesi;

        }

        public static string GetKomponente(HttpRequest Request) => GetKomponente(Request, true);

        public static string GetKomponente(HttpRequest Request, bool meParam) => GetKomponente(Request.RawUrl, meParam);
        public static string GetKomponente(string url, bool meParam)
        {
            if (string.IsNullOrEmpty(url))
                throw new Exception("Parametri url erdhi bosh");

            if (url[0] != '/')
                throw new Exception("url pritet te nise me //, url-ja qe erdhi eshte: " + url);


            if (!meParam)
                return url.Split('/')[1].Split('?')[0];


            var urlPart = url.Split('/')[1];
            var urlParts = urlPart.Split('?');

            if (urlParts.Length == 1)
                return urlPart;

            var parametra = urlParts[1].Split('&');
            if (parametra[0].Contains(ScopeManager.ScopeIdKey))
                if (parametra.Length > 1)
                    return urlParts[0] + "?" + parametra[1];
                else
                    return urlParts[0];
            return url.Split('/')[1].Split('&')[0];

        }
        /// <summary>
        /// kontrollon nese passwordi i perdoruesit ka skaduar ose perdoruesi  ka password te perkohshem.
        ///  Nqs eshte nje nga rastet ath therritet metoda:
        /// <see cref="clsFunksione.avancoPerpara"/>
        /// </summary>
        /// <param name="konfig"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="Session"></param>
        /// <param name="httpResponse"></param>
        /// <returns>kthen true nqs passwordi i perdoruesit nuk duhet te ndryshohet, false perndryshe</returns>
        public static clsMesazh kontrolloPassword(clsKonfigurimeFjalekalimi konfig, int idPerdoruesi, HttpContext httpContext, ResourceManager rm, CultureInfo ci)
        {
            clsPerdorues user = new clsPerdorues(idPerdoruesi);
            if (konfig.NdryshimPasswordiDetyruar && user.PasswordIPerkohshem)
            {
                clsMesazh mesazh = avancoPerpara(httpContext.Response, httpContext.Session, user.IdPerdorues, rm, ci, true, true);
                if (!mesazh) return mesazh;
            }
            if (konfig.SkadoPassword)
            {
                clsMesazh skaduar = konfig.kontrolloSkadencenEPass(user);
                if (!skaduar.Status)
                {
                    avancoPerpara(httpContext.Response, httpContext.Session, idPerdoruesi, rm, ci, true, false, true);
                    return skaduar;
                }
            }
            return new clsMesazh(true);
        }


        public static bool isValidEmail(out string mesazhGabimi, string email)
        {
            if (!Regex.Match(email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*").Success)
            {
                mesazhGabimi = "Kujdes, fusha e emailit duhet te jene ne format emaili";
                return false;
            }
            mesazhGabimi = null;
            return true;
        }

        public static bool isValidMSISDN(string nrTel, out string mesazh)
        {
            if (!Regex.Match(nrTel, @"^[0-9]{10}$").Success)
            {
                mesazh = "Kujdes, fusha e numrit te telefonit te perfaqesuesit te shitjes duhet te permbaje 10 numra, nuk lejohen karakteret speciale!";
                return false;
            }
            mesazh = "";
            return true;
        }


        public static bool isValidNrPersonal(string nrLlogMpesa, out string mesazhGabimi)
        {
            if (!Regex.Match(nrLlogMpesa, @"[A-Za-z].{8}[A-Za-z]$").Success || nrLlogMpesa.Length != 10)
            {
                mesazhGabimi = "Kujdes, numri personal duhet te kete 10 karaktere, te filloj dhe te mbaroje me shkronje!";
                return false;
            }
            mesazhGabimi = "";
            return true;
        }


        private static clsMesazh shtoLoginFail(HttpSessionState Session, clsKonfigurimeFjalekalimi konfig, Dictionary<string, Dictionary<int, int>> loginAttempts, string username, int loginCount, string arsyeLoginFail, int idPerdoruesi, int maxLoginAttempts, ResourceManager rm, CultureInfo ci)
        {
            ruajNrLoginFailAttempt(Session, konfig, loginAttempts, username, loginCount);
            clsTrackUser.shtoUserLoginFail(arsyeLoginFail, username, Session.SessionID, HttpContext.Current.Request.UserHostAddress);
            //if (konfig != null)
            //{
            clsMesazh arriturNrMaxTentativa = kontrolloNrMaxTentativaLogin(Session, loginAttempts, username, konfig, idPerdoruesi, loginCount, maxLoginAttempts, rm, ci);
            if (!arriturNrMaxTentativa.Status)
            {
                return new clsMesazh(false, arriturNrMaxTentativa.PershkrimMesazhi);
            }
            //}
            return new clsMesazh(true, "");
        }

        /// <summary>
        /// kontrollon nese eshte kaluar numri i lejuar i tentative te login dhe e kyc ne rast se ka arritur max e tentativave
        /// </summary>
        /// <param name="loginAttempts">var i tipit Application, i cili ruan te dhenat mbi tentativat e login per secilin perdorues</param>
        /// <param name="username">emri i perdoruesit qe po tenton te logohet</param>
        /// <param name="konfig">objekt i klases clsKonfigurimeFjalekalimi qe mban konfigurimin per licencen e perdoruesit</param>
        /// <param name="idPerdoruesi"></param>
        /// <returns></returns>
        private static clsMesazh kontrolloNrMaxTentativaLogin(HttpSessionState Session, Dictionary<string, Dictionary<int, int>> loginAttempts, string username, clsKonfigurimeFjalekalimi konfig, int idPerdoruesi, int loginCount, int maxLoginAttempts, ResourceManager rm, CultureInfo ci)
        {
            // vjen nga punonjesi nuk kemi konfigurim
            if (konfig == null)
            {
                if (loginCount >= maxLoginAttempts)
                {
                    //Response.Redirect("LoginFail.aspx?arsye=maxLoginAttempts", false);
                    return new clsMesazh(false, rm.GetString("msgLoginNukKeniMeTeDrejteTeLogoheni", ci));
                }
                else return new clsMesazh(true, "");
            }
            else
            {
                //if (konfig.BllokoPerdorues)
                //{
                if (loginAttempts.ContainsKey(username))
                {
                    string mesazh = "";
                    Dictionary<int, int> nrTentativaLogin = new Dictionary<int, int>();//variabel qe mban numrin e tentativave dhe sessionid te perdoruesit
                    nrTentativaLogin = loginAttempts[username];
                    int key = nrTentativaLogin.Last().Key;//numri me i madh i tentativave per login deri ne kete moment
                    int sesioniPErdoruesit = nrTentativaLogin.Last().Value;//vlera me e fundit e numrit te sesionit per perdoruesin
                                                                           //if (Convert.ToInt32(loginAttempts[username]) > konfig.TentativaBllokUserXSession)
                                                                           //{
                                                                           //    Login1.FailureText = "Nuk keni me te drejte te provoni login! Te dhenat ishin te gabuara.";
                                                                           //    return false;
                                                                           //}
                    lock (loginAttempts)
                    {
                        //kontrollon nqs eshte kaluar nr i lejuar i tentativave per login sipas konfigurimit
                        //if (Convert.ToInt32(loginAttempts[username]) >= konfig.MaxSesioneXPerdorues * konfig.TentativaBllokUserXSession)
                        //{
                        //    if (DbCore.DbAdmin.clsPerdorues.kycPerdorues(user.IdPerdorues))
                        //        Login1.FailureText = "Ky perdorues u kyç. Ju nuk keni me te drejte te provoni login! Ju lutemi, kontaktoni me administratorin!";
                        //    return false;
                        if (sesioniPErdoruesit == konfig.MaxSesioneXPerdorues && key >= konfig.TentativaBllokUserXSession)
                        {
                            if (konfig.BllokoPerdorues)
                            {
                                if (clsPerdorues.kycPerdorues(idPerdoruesi))
                                {
                                    loginAttempts.Remove(username);
                                    mesazh = rm.GetString("msgLoginPerdoruesiUKyc", ci);
                                    return new clsMesazh(false, mesazh);
                                }
                            }
                        }
                        if (key >= konfig.TentativaBllokUserXSession && CacheLayer.GlobalCacheManager.MySessionCache[username + sesioniPErdoruesit] != null)
                        {
                            mesazh = rm.GetString("msgLoginNukKeniMeTeDrejteTeLogoheni", ci);
                            return new clsMesazh(false, mesazh);
                        }
                    }
                }
                return new clsMesazh(true);
                //}
                //else //rasti kur nuk e ka te konfiguruar qe pedoruesi te bllokohet, behet bllokimi default(max tre tentativa per nje sesion)
                //{
                //    if (loginCount >= maxLoginAttempts)
                //    {
                //        //Response.Redirect("LoginFail.aspx?arsye=maxLoginAttempts", false);
                //        return new clsMesazh(false, rm.GetString("msgLoginNukKeniMeTeDrejteTeLogoheni", ci));
                //    }
                //}
                // return new clsMesazh(true, "");
            }
        }


        /// <summary>
        /// kontrollon nese data e klientit dhe e serverit ka diference me te madhe se nje dite.
        /// Ne kete rast nuk do te lejohet logimi i perdoruesit.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool kontrolloDatenKlientServer(string data)
        {
            DateTime clDate = Convert.ToDateTime(data, new CultureInfo("en-us", false));
            DateTime serverDate = DateTime.Now;
            TimeSpan span = clDate.Subtract(serverDate);
            if (span.Days > 1)
                return false;
            else return true;
        }

        /// <summary>
        /// ruan numrin e tentative te logimeve te deshtuara per perdoruesin
        /// </summary>
        /// <param name="konfig">objekt i klases clsKonfigurimeFjalekalimi</param>
        /// <param name="loginAttempts">ruan numrin e tentativave per nje perdorues per secilin perdorues</param>
        /// <param name="username">emri i perdoruesit</param>
        /// <param name="loginCount">numri i tentativave per perdoruesit te cilet nuk e kane te konfiguruar te politikat e fjalekalimit nese perdoruesi do bllokohet ose jo</param>
        private static void ruajNrLoginFailAttempt(HttpSessionState session, clsKonfigurimeFjalekalimi konfig, Dictionary<string, Dictionary<int, int>> loginAttempts, string username, int loginCount)
        {
            Dictionary<int, int> nrTentativaSesione = new Dictionary<int, int>();//var qe mban numrin e tentativave dhe sesioneve
            loginCount = loginCount + 1;
            if (konfig != null)//nqs licenca e ketij perdoruesi e ka te konfiguruar numrin maksimal te sesioneve dhe tentativave per sesion
            {
                if (loginAttempts.ContainsKey(username))
                {
                    nrTentativaSesione = loginAttempts[username];
                    int key = nrTentativaSesione.Last().Key;//numri me i madh i tentativave per login deri ne kete moment
                    int nrSession = nrTentativaSesione.Last().Value;//vlera me e fundit e numrit te sesionit per perdoruesin
                    if (key == konfig.TentativaBllokUserXSession && CacheLayer.GlobalCacheManager.GetSessionCacheByKey(session.SessionID)[username + nrSession] == null)
                    {
                        nrTentativaSesione.Clear();
                        HttpContext.Current.Application.Lock();
                        nrSession = nrSession + 1;
                        CacheLayer.GlobalCacheManager.GetSessionCacheByKey(session.SessionID)[username + nrSession] = username + nrSession;
                        nrTentativaSesione.Add(1, nrSession);
                        HttpContext.Current.Application.UnLock();
                    }
                    else
                    {
                        nrTentativaSesione.Add(key + 1, nrSession);
                    }

                    loginAttempts[username] = nrTentativaSesione;
                }
                else
                {
                    nrTentativaSesione.Add(1, 1);
                    CacheLayer.GlobalCacheManager.GetSessionCacheByKey(session.SessionID)[username + 1] = username + 1;
                    loginAttempts.Add(username, nrTentativaSesione);
                }
            }
            else //rasti kur nuk ka konfigurim
            {
                mySessionObjects.ruajLoginCount(session, loginCount);
            }
        }

        /// <summary>
        /// ruan numrin e tentative te logimeve te deshtuara per perdoruesin me username jo te sakte
        /// </summary>
        /// <param name="Session"></param>
        /// <param name="loginAttempts"></param>
        /// <param name="username"></param>
        /// <param name="loginCount"></param>
        private static void ruajNrLoginFailAttempt(HttpSessionState session, Dictionary<string, Dictionary<int, int>> loginAttempts, string username, int loginCount) => ruajNrLoginFailAttempt(session, null, loginAttempts, username, loginCount);

        /// <summary>
        /// kontrollon nese perdoruesi qe po ben kerkesen ka te drejte te resetoje passwordin
        /// </summary>
        /// <param name="username">emri i perdoruesit qe kerkon resetimin e fjalekalimit</param>
        /// <param name="harroPw">eshte 1 vetem ne rastin kur perdoruesi ka harruar pass dhe klikon linkun perkates ne faqen e login</param>
        /// <param name="shfaqLinkResetPass"></param>
        public static clsMesazh dergoVerificationLink(string username, bool punonjes, int idgjuha)
        {
            clsPunonjes pun = null;
            clsPerdorues user = null;
            CultureInfo ci = MessagesResource.KtheCultureInfo(idgjuha);
            ResourceManager rm = MessagesResource.CurrentResourceManager;
            if (punonjes)
            {
                pun = new clsPunonjes(username);
                if (pun.IdPunonjes == 0)
                    return new clsMesazh(false, rm.GetString("lblPerdoruesiEmerNukEkziston", ci));

            }
            else
            {
                user = new clsPerdorues(username);
                if (user.IdPerdoruesi == 0)
                    return new clsMesazh(false, rm.GetString("lblPerdoruesiEmerNukEkziston", ci));

                clsKonfigurimeFjalekalimi konfigPass = new clsKonfigurimeFjalekalimi(user.IdPerdorues);
                if (!konfigPass.ResetPassword)
                {
                    return new clsMesazh(1, false, rm.GetString("perdoruesiSmundTeResetojeFjalekalimin", ci));
                }
            }

            return EmailComposer.DergoEmailVerificationLink(user, HttpContext.Current.Request, pun, idgjuha);

        }

        #region logout Perdorues

        private static Func<string, bool> _redirectOnCallback;
        private static IDictionary<string, object> hfArkiva;

        public static void Initialise(Func<string, bool> redirectOnCallback) =>
            _redirectOnCallback = redirectOnCallback;
        /// <summary>
        /// ben logout perdoruesit
        /// </summary>
        /// <param name="Session"></param>
        /// <param name="dontRedirect">tregon nese do ridrejtohet ose jo ne faqen e login</param>
        /// <param name="queryString">queryString ne rastin kur do behet redirect te faqja e login</param>
        public static void logout(HttpSessionState Session, bool logOut, bool signOutFormsAuth, bool dontRedirect, string queryString = "")
        {

            Logout(Session, logOut, signOutFormsAuth, dontRedirect, Paths.defaultLoginPath, queryString);
        }
        public static void Logout(HttpSessionState Session, bool logOut, bool signOutFormsAuth, bool dontRedirect, string loginUrl, string queryString)
        {
            //string emerPerdoruesi = DbCore.mySessionObjects.merrEmerPerdoruesiNgaSesioni(Session);
            //string emerNdermarrje = DbCore.mySessionObjects.merrNdermarjeselectSesioni(Session);
            Session.Abandon();
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));
            ImbLogger.LogTrace($"(Shkaterrim sesioni) -> SessionId:{Session.SessionID} - Url:(clsFunksione) {HttpContext.Current.Request.Url.PathAndQuery}");
            
            clsFunksione.dergoLogAlphaweb("", "Logout", "Logout", clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar(),"");
            GlobalCacheManager.DestroySessionCache(Session.SessionID);
            if (signOutFormsAuth)
                FormsAuthentication.SignOut();
            if (!dontRedirect)
            {
                string login = loginUrl;
                if (!queryString.Equals(""))
                    login = $"{loginUrl}?arsye=" + queryString + "&google=true";
                Page page = HttpContext.Current.Handler as Page;
                if (page != null && page.IsCallback)
                    _redirectOnCallback(login);
                else
                {
                    try
                    {
                        HttpContext.Current.Response.Redirect(login, true);

                    }
                    catch (ArgumentNullException ex)
                    {
                        ImbLogger.Error(ex);
                    }
                    catch (ArgumentException ex)
                    {
                        ImbLogger.Error(ex);
                    }
                    catch (HttpException ex)
                    {
                        ImbLogger.Error(ex);
                    }
                    catch (ApplicationException ex)
                    {
                        ImbLogger.Error(ex);
                    }
                }
            }
        }
        public static void LogoutRestore(HttpSessionState Session, bool logOut, bool signOutFormsAuth, bool dontRedirect, string loginUrl, string queryString)
        {

            Session.Abandon();
            HttpContext.Current.Response.Cookies.Add(new HttpCookie("ASP.NET_SessionId", ""));

            ImbLogger.LogTrace($"(Shkaterrim sesioni) -> SessionId:{Session.SessionID} - Url:(clsFunksione) {HttpContext.Current.Request.Url.PathAndQuery}");
            GlobalCacheManager.DestroySessionCache(Session.SessionID);
            if (signOutFormsAuth)
                FormsAuthentication.SignOut();
            if (!dontRedirect)
            {
                string login = loginUrl;
                if (!queryString.Equals(""))
                    login = $"{loginUrl}?arsye=" + queryString;

                try
                {
                    HttpContext.Current.Response.Redirect(login, true);
                }
                catch (ArgumentNullException ex)
                {
                    ImbLogger.Error(ex);
                }
                catch (ArgumentException ex)
                {
                    ImbLogger.Error(ex);
                }
                catch (HttpException ex)
                {
                    ImbLogger.Error(ex);
                }
                catch (ApplicationException ex)
                {
                    ImbLogger.Error(ex);
                }

            }
        }

        /// <summary>
        /// ben logout perdoruesit dhe e ridrejton ne faqen e login
        /// </summary>
        /// <param name="Session"></param>
        public static void logout(HttpSessionState Session, bool signOutFormsAuth, string queryString = "", bool signOutUser = true)
        {
            logout(Session, signOutUser, signOutFormsAuth, false, queryString);
        }

        #endregion

        public static clsMesazh avancoPerpara(HttpResponse response, HttpSessionState sesioni, int idPerdoruesi, ResourceManager rm, CultureInfo ci, bool eValiduar, bool ndryshoPassword = false, bool passwordISkaduar = false, bool endResponse = true, string redirectToPage = "")
        {
            if (!eValiduar)
            {

                response.Redirect("AktivizoAlphaWeb.aspx");
                return new clsMesazh(true);
            }
            if (ndryshoPassword)
            {
                response.Redirect("NdryshimFjalekalimi.aspx");
                return new clsMesazh(true);
            }
            if (passwordISkaduar)
            {
                response.Redirect("NdryshimFjalekalimi.aspx?skaduarPass=true", false);//tocheck Getson
                return new clsMesazh(true);
            }
            using (clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin())
            {
                DataTable dt = dbAdmin.merrNdermarrjetEPerdoruesitDataTable(idPerdoruesi);
                int idTheme = clsThemesAmbjente.ktheIdTheme(idPerdoruesi);
                if (dt.Rows.Count == 0 || dt.Rows.Count > 1)//Nese kam me shume se nje ndermarrje shkoj tek faqja e ndermarrjeve
                {
                    response.Redirect(shtoVarToUrl("Login_Ndermarrje.aspx?google=true", "idTheme", idTheme.ToString()), endResponse);
                    return new clsMesazh(true);
                }
                DataRow rreshti = dt.Rows[0];
                int idNdermarrjes = Convert.ToInt32(rreshti[0]);
                String kodiNdermarrjes = rreshti[1].ToString();
                String vitiNdermarrjes = rreshti[3].ToString();
                int idNdermarrjeVit = Convert.ToInt32(rreshti[4]);
                clsMesazh mesazh = merrNdermarrjenPerPune(sesioni, idPerdoruesi, idNdermarrjes, kodiNdermarrjes, vitiNdermarrjes, idNdermarrjeVit, null);
                if (!mesazh.Status)
                    return mesazh;
                mySessionObjects.ruajObjectNeSesion(sesioni, "shfaqdefault");
                if (redirectToPage != "")
                {
                    response.Redirect(shtoVarToUrl(redirectToPage, "idTheme", idTheme.ToString()), false);
                    return mesazh;
                }
                //string komponente = clsKomponente.merrKomponenteDefaultPerdoruesi(idPerdoruesi);
                //if (komponente != "")
                //{
                //    response.Redirect(shtoVarToUrl(komponente, "idTheme", idTheme.ToString()), false);//tocheck Getson
                //    return mesazh;
                //}
                //response.Redirect(shtoVarToUrl("FaqeKryesore.aspx", "idTheme", idTheme.ToString()), false); //tocheck Getson
                string komponente = clsFunksione.ktheKomponenteDefaultPerPerdorues(idPerdoruesi, idNdermarrjes, "FaqeKryesore.aspx", vitiNdermarrjes);
                response.Redirect(shtoVarToUrl(komponente, "idTheme", idTheme.ToString()), false); //tocheck Getson
                return mesazh;
            }
        }

        public static string shtoVarToUrl(string url, string varName, string varValue)
        {
            if (url == "CRMDefault.aspx")
                return url;
            if (url.Contains(varName))
                return url;
            if (url.Contains("?"))
                url += "&" + varName + "=" + varValue;
            else
                url += "?" + varName + "=" + varValue;
            return url;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="response"></param>
        /// <param name="sesioni"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrjes"></param>
        /// <param name="kodiNdermarrjes"></param>
        /// <param name="vitiNdermarrjes"></param>
        /// <param name="idNdermarrjeVit"></param>
        public static clsMesazh merrNdermarrjenPerPune(HttpSessionState sesioni, int idPerdoruesi, int idNdermarrjes, String kodiNdermarrjes, String vitiNdermarrjes, int idNdermarrjeVit, clsPeriudhaKontabel periudha)
        {
            try
            {
                clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();

                mySessionObjects.ruajIdNdermarrjeVit(idNdermarrjeVit.ToString(), sesioni);
                mySessionObjects.ruajKodNdermarrje(kodiNdermarrjes, sesioni);
                mySessionObjects.ruajIdNdermarrjeNeSesion(sesioni, idNdermarrjes.ToString());
                mySessionObjects.ruajVitiNdermarrjes(sesioni, vitiNdermarrjes);
                EmailComposer.IdNdermVit = idNdermarrjeVit.ToString();
                //kevi marre nga defaulti
                clsViti vit = new clsViti();
                vit.mbushVitetMet(vitiNdermarrjes, idNdermarrjes);
                colPeriudhaKontabel periudhat = new colPeriudhaKontabel();
                periudhat.merrSipasViti(vit.IdViti);
                if (periudhat.Count == 0)
                    return new clsMesazh(false, MessagesResource.Messages["msgLoginNdermarrjeKujdesNukEkzistojnePeriudhat"]);
                if (periudha == null)
                {
                    DateTime dtAktuale = Convert.ToDateTime(DateTime.Now.ToString());

                    mySessionObjects.ruajPeriudheKontabelNeSesion(new clsPeriudhaKontabel(), sesioni);
                    bool ekziston = false;
                    for (int i = 0; i < periudhat.Count; i++)
                    {
                        if (dtAktuale >= periudhat[i].FillimiPeriudha && dtAktuale <= periudhat[i].MbarimiPeriudha.AddDays(1))
                        {
                            mySessionObjects.ruajPeriudheKontabelNeSesion(periudhat[i], sesioni);
                            ekziston = true;
                            break;
                        }
                    }

                    if (!ekziston)
                    {
                        if (dtAktuale <= periudhat[0].FillimiPeriudha)
                            if (periudhat[0].EmerPeriudha.ToLower() != clsPeriudhaKontabel.PeriudheFillestare.ToLower())
                            {
                                mySessionObjects.ruajPeriudheKontabelNeSesion(periudhat[0], sesioni);
                            }
                            else
                            {
                                mySessionObjects.ruajPeriudheKontabelNeSesion(periudhat[1], sesioni);
                            }
                        else
                            if (dtAktuale >= periudhat[periudhat.Count - 1].MbarimiPeriudha)
                            if (periudhat[periudhat.Count - 1].EmerPeriudha != clsPeriudhaKontabel.PeriudheMbyllje)
                            {
                                mySessionObjects.ruajPeriudheKontabelNeSesion(periudhat[periudhat.Count - 1], sesioni);
                            }
                            else
                            {
                                mySessionObjects.ruajPeriudheKontabelNeSesion(periudhat[periudhat.Count - 2], sesioni);
                            }
                    }
                }
                else
                {
                    mySessionObjects.ruajPeriudheKontabelNeSesion(periudha, sesioni);
                }
                //end marre
                using (clsNdermarrje nderm = new clsNdermarrje(idNdermarrjes))
                {
                    mySessionObjects.ruajEshteNdermarjeMemeNeSesion(sesioni, nderm.Prind);
                    mySessionObjects.ruajEshteNdermarjeOwnNeSesion(sesioni, nderm.OwnShop);
                    mySessionObjects.ruajRuajLogNeSesion(sesioni, nderm.LogNdermarrje);
                    mySessionObjects.ruajNdermRaportuese(sesioni, nderm.Raportuesi);
                }
                return new clsMesazh(true);
            }
            catch (Exception)
            {
                return new clsMesazh(false, MessagesResource.Messages["msgNdodhi1GabimNeHapjeTeLupesLoginNdermarrjes"]);
            }
        }

        public static clsMesazh merrNdermarrjenPerPune(HttpRequest Request, HttpResponse Response, HttpSessionState Session, int idNdermarrjes, String kodiNdermarrjes, String vitiNdermarrjes, int idNdermarrjeVit, int idPerdoruesi, bool redirectToDefaultComponent, out string scopeId, clsPeriudhaKontabel periudha)
        {
            var newScopeID = ScopeManager.GjeneroScopeId();
            scopeId = newScopeID;
            Request.ServerVariables.Set(ScopeManager.ScopeIdKey, newScopeID);
            GlobalCacheManager.SetTemporaryScopeId(scopeId);
            var mesazh = merrNdermarrjenPerPune(Session, idPerdoruesi, idNdermarrjes, kodiNdermarrjes, vitiNdermarrjes, idNdermarrjeVit, periudha);
            GlobalCacheManager.RemoveTemporaryScopeId();
            if (!mesazh)
            {
                return mesazh;
            }
            ScopeManager.RuajScopeId(newScopeID);

            if (redirectToDefaultComponent)
            {

                int idTheme = IMBUtils.Types.Converter.MerrVlereOseDefault<int>(Request.QueryString["idTheme"]);
                if (idTheme == 0) idTheme = DbCore.DbAdmin.clsThemesAmbjente.ktheIdTheme(idPerdoruesi);
                var currentQueryString = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                currentQueryString.Set("idTheme", idTheme.ToString());
                currentQueryString.Set(ScopeManager.ScopeIdKey, newScopeID);
                string komponente = DbCore.clsFunksione.ktheKomponenteDefaultPerPerdorues(idPerdoruesi, idNdermarrjes, "FaqeKryesore.aspx", vitiNdermarrjes);
                var stringNgjites = komponente.ContainsAnyIgnoreCase("?") ? "&" : "?";
                Response.Redirect(komponente + stringNgjites + currentQueryString.ToString(), false);
            }
            return new MesazhSuksesi();
        }



        public static string KrijoScopeTeRiNgaScopeIVjeter(HttpContext context, string idNderm, string idVitNderm)
        {
            int idNdermarrjes = 0, idNdermarrjeVit = 0;
            string kodiNdermarrjes = "", vitiNdermarrjes = "";
            var idperdoruesi = mySessionObjects.ktheIdPerdoruesi(context.Session);
            clsPeriudhaKontabel periudha = null;
            if (!string.IsNullOrWhiteSpace(idNderm) && !string.IsNullOrWhiteSpace(idVitNderm))
            {
                var nderm = new clsNdermarrje(Convert.ToInt32(idNderm));
                var vit = new clsNdermarrjeViti(Convert.ToInt32(idVitNderm));

                idNdermarrjes = nderm.IdNdermarrje;
                kodiNdermarrjes = nderm.NdermarrjeKodi;
                idNdermarrjeVit = vit.IdNderViti;
                vitiNdermarrjes = Convert.ToString(vit.Viti);
            }
            else
            {
                idNdermarrjes = mySessionObjects.merrIdNdermarrjeSesioni(context.Session);
                kodiNdermarrjes = mySessionObjects.ktheKodNdermarrje(context.Session);
                vitiNdermarrjes = Convert.ToString(mySessionObjects.ktheVitiNdermarrjes(context.Session));
                idNdermarrjeVit = mySessionObjects.ktheNdermarrjeVit(context.Session);
                periudha = mySessionObjects.merrPeriudheKontabel(context.Session);
            }
            string scopeId;
            merrNdermarrjenPerPune(context.Request, context.Response, context.Session, idNdermarrjes, kodiNdermarrjes, vitiNdermarrjes, idNdermarrjeVit, mySessionObjects.ktheIdPerdoruesi(context.Session), false, out scopeId, periudha);

            return scopeId;

        }


        #region eksport_Per_AlphaBank

        /// <summary>
        /// kthen numrin automatik sipas kodit Flex_Cube e cila perdoret per eksportin e te dhenave per Alpha Bank Flex Cube
        /// Pra kthen numrin serial te radhes
        /// </summary>
        /// <param name="idNdermarje"></param>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static string merrNumrinAutomatik(int idNdermarje, HttpSessionState Session)
        {
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(Session);
            List<NrAuto> list = new List<NrAuto>();
            clsMesazh mesazh = new clsMesazh();
            clsNrAutom nrAutom = new clsNrAutom("Flex_Cube", idNdermarje);
            string nrSerialBatchNo = clsNrAutom.merrVlerenNrAutomatik(nrAutom.IdNrAutom, DateTime.Now);
            if (!String.IsNullOrEmpty(nrSerialBatchNo))
            {
                NrAuto nrser = new NrAuto();
                nrser.idNrAuto = nrAutom.IdNrAutom;
                nrser.vlereNrAuto = nrSerialBatchNo;
                list.Add(nrser);
                nrSerialBatchNo = nrser.vlereNrAuto;
            }
            else return "";
            bool kaNdryshimNumri;
            mesazh = NrAuto.RuajVlera(out kaNdryshimNumri, list, DateTime.Now, idPerdoruesi, idNdermarje);
            if (!mesazh.Status)
                return "";
            return nrSerialBatchNo;
        }



        /// <summary>
        /// Vetem per alpha bank Flex Cube
        /// Krijon nje DataTable me te dhenat qe do kete sheet-i master
        /// </summary>
        /// <param name="Session"></param>
        /// <returns></returns>
        public static DataTable krijoDataTablePerMasterSheet(HttpSessionState Session)
        {
            DataTable dt = new DataTable();
            DataRow r1 = dt.NewRow();
            DataColumn[] cols = new DataColumn[6];
            for (int i = 0; i < 6; i++)
            { cols[i] = new DataColumn(); }
            dt.Columns.AddRange(cols);
            DataRow r2 = dt.NewRow();
            DataRow r3 = dt.NewRow();
            DataRow r4 = dt.NewRow();
            string end = "~~END~~";
            r1[0] = "FCC.DETB_UPLOAD_MASTER";
            dt.Rows.Add(r1);
            r2[0] = "BRANCH_CODE"; r2[1] = "SOURCE_CODE"; r2[2] = "BATCH_NO"; r2[3] = "BATCH_DESC"; r2[4] = "USER_ID";
            dt.Rows.Add(r2);
            string nrSerialBatchNo = mySessionObjects.merrNrAutomatikPerFlexCube(Session);
            r3[0] = "001"; r3[1] = "DE_UPLOAD"; r3[2] = nrSerialBatchNo; r3[3] = ""; r3[4] = mySessionObjects.ktheEmerPerdorues(Session); r3[5] = end;
            dt.Rows.Add(r3);
            for (int i = 0; i < 6; i++)
            { r4[i] = end; }
            dt.Rows.Add(r4);
            return dt;
        }

        public static void shtoKoloneQKTeRaportiExcelWorkbook(string filePathToOpen, HttpResponse Response, bool shfaqDtPrintimi)
        {
            // Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
            //Specify the template excel file path.
            //string filePathToOpen = @"c:\Data Entry Batch Upload Amortizimi Tetor.xlsx";
            //string filePathToOpen = AppDomain.CurrentDomain.BaseDirectory + "\\ExcelData.xlsx";
            string pathDir = HttpContext.Current.Server.MapPath(null) + @"\AlphaWebExport\";
            DirectoryExtension.CreateDirIfNotExists(pathDir);
            string[] similarString = filePathToOpen.Split('\\');
            string subStr = similarString[similarString.Length - 1].Split('.')[0];
            //string[] filePaths = Directory.GetFiles(pathDir, subStr + "*.xlsx");
            //DirectoryInfo di = new DirectoryInfo(pathDir);
            //FileSystemInfo[] files = di.GetFileSystemInfos();
            //string[] filePaths = files.OrderBy(f => f.CreationTime);
            DirectoryInfo info = new DirectoryInfo(pathDir);
            string[] filePaths = info.GetFiles(subStr + "*.xlsx").OrderBy(p => p.CreationTime).Select(x => x.Name).ToArray();
            for (int x = 0; x < filePaths.Length; x++)
            {
                filePaths[x] = pathDir + filePaths[x];
            }
            int kolonaPasVitiRaportues = -1;
            int kolonaF = -1;
            int indexVitiRaportuesQK = -1;
            bool shtuarRreshti = false;
            FileInfo newFile = new FileInfo(filePathToOpen);
            for (int f = 0; f < filePaths.Length; f++)
            {
                FileInfo newFileQk = new FileInfo(filePaths[f]);
                if (filePathToOpen == filePaths[f])
                    continue;
                //ExcelPackage excPacQK = new ExcelPackage(newFileQk);
                //FileInfo newFile = new FileInfo(@"Sample2.xlsx");
                ExcelRange columnCells;
                ExcelWorksheet detailWorksheetQk;
                int kolonaFunditEPerdorurQK;
                ExcelPackage excPacQK = new ExcelPackage(newFileQk);
                //{
                ExcelWorkbook WBQK = excPacQK.Workbook;
                //Hap file e excel
                //Microsoft.Office.Interop.Excel.Workbook WB = excelApp.Workbooks.Open(filePathToOpen, Missing.Value, Missing.Value, Missing.Value, Missing.Value,Missing.Value, Missing.Value,Missing.Value, Missing.Value,Missing.Value, Missing.Value,Missing.Value, Missing.Value,Missing.Value, Missing.Value);
                detailWorksheetQk = WBQK.Worksheets.ElementAt(0);
                int rreshtiFunditIPerdorurQK = detailWorksheetQk.Dimension.End.Row;
                kolonaFunditEPerdorurQK = detailWorksheetQk.Dimension.End.Column;
                //Array[] kolonat = new Double[detailWorksheetQk.Dimension.End.Row]
                if (indexVitiRaportuesQK == -1)
                {
                    for (int i = 1; i < kolonaFunditEPerdorurQK; i++)
                    {
                        ExcelRange columnCell = detailWorksheetQk.Cells[1, i];
                        if (columnCell.Value != null)
                        {
                            if (columnCell.Value.Equals("Viti Raportues"))
                            {
                                indexVitiRaportuesQK = i;
                                break;
                            }
                        }
                    }
                }
                columnCells = detailWorksheetQk.Cells[3, indexVitiRaportuesQK, detailWorksheetQk.Dimension.End.Row, indexVitiRaportuesQK];
                //}
                using (ExcelPackage excPac = new ExcelPackage(newFile))
                {
                    //ExcelPackage excPac = new ExcelPackage(newFile);
                    //Get the work book in the file
                    ExcelWorkbook WB = excPac.Workbook;
                    //Hap file e excel
                    ExcelWorksheet detailWorksheet = WB.Worksheets.ElementAt(0);
                    ExcelColumn col = detailWorksheet.Column(5);
                    //Worksheet detailWorksheet = WB.Worksheets[1] as Microsoft.Office.Interop.Excel.Worksheet; //merr sheet-in e pare
                    //detailWorksheet.Name = "QK";
                    detailWorksheet.Cells.Style.Font.Size = 10;
                    detailWorksheet.Cells.Style.Font.Name = "Arial";
                    detailWorksheet.Cells.Style.Numberformat.Format = "@";
                    if (!shtuarRreshti)
                    {
                        detailWorksheet.InsertRow(1, 1);
                        shtuarRreshti = true;
                    }
                    int rreshtiFunditIPerdorur = detailWorksheet.Dimension.End.Row;
                    int kolonaFunditEPerdorur = detailWorksheet.Dimension.End.Column;
                    if (kolonaF == -1)
                    {
                        for (int i = 1; i < kolonaFunditEPerdorur; i++)
                        {
                            ExcelRange columnCell = detailWorksheet.Cells[2, i];
                            if (columnCell.Value != null)
                            {
                                if (columnCell.Value.Equals("Viti Raportues"))
                                {
                                    kolonaF = i + 1;
                                    kolonaPasVitiRaportues = i + 2;
                                    break;
                                }
                            }
                        }
                    }
                    detailWorksheet.Cells[2, kolonaF].Value = detailWorksheetQk.Name;
                    detailWorksheet.Cells[2, kolonaF].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    detailWorksheet.Cells[2, kolonaF].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    detailWorksheet.Cells[2, kolonaF].Style.Font.Bold = true;
                    for (int i = 3; i < rreshtiFunditIPerdorur; i++)
                    {
                        ExcelRange columnCell = detailWorksheet.Cells[i, kolonaF];
                        ExcelRange columnCellQK = detailWorksheetQk.Cells[i - 1, indexVitiRaportuesQK];
                        try
                        {
                            double vlera;
                            string vleraQK = Convert.ToString(columnCellQK.Value);
                            if (vleraQK.Contains("("))
                            {
                                double.TryParse(vleraQK.Replace('(', ' ').Replace(')', ' '), out vlera);
                                vlera = (-1) * vlera;
                            }
                            else
                                double.TryParse(vleraQK, out vlera);
                            columnCell.Value = vlera;
                        }
                        catch (Exception)
                        {
                            columnCell.Value = columnCellQK.Value;
                        }

                        ExcelRange columnCellVitiRaportues = detailWorksheet.Cells[i, indexVitiRaportuesQK];
                        try
                        {
                            double vleraVitiRaportues;
                            string vleraQKVitiRaportues = Convert.ToString(columnCellVitiRaportues.Value);
                            if (vleraQKVitiRaportues.Contains("("))
                            {
                                double.TryParse(vleraQKVitiRaportues.Replace('(', ' ').Replace(')', ' '), out vleraVitiRaportues);
                                vleraVitiRaportues = (-1) * vleraVitiRaportues;
                            }
                            else
                                double.TryParse(vleraQKVitiRaportues, out vleraVitiRaportues);
                            columnCellVitiRaportues.Value = vleraVitiRaportues;
                        }
                        catch (Exception)
                        {
                        }
                        columnCell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                        columnCell.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                        columnCell.Style.Numberformat.Format = "0.00";
                    }
                    excPac.Save();
                }
                kolonaF++;
                excPacQK.Dispose();
            }
            using (ExcelPackage excPacRE = new ExcelPackage(newFile))
            {
                ExcelWorkbook WB = excPacRE.Workbook;
                //Hap file e excel
                ExcelWorksheet detailWorksheet = WB.Worksheets.ElementAt(0);
                int rreshtiFunditIPerdorurRe = detailWorksheet.Dimension.End.Row;
                int kolonaFunditEPerdorurRe = detailWorksheet.Dimension.End.Column;
                detailWorksheet.Cells[1, kolonaPasVitiRaportues, rreshtiFunditIPerdorurRe, kolonaFunditEPerdorurRe].AutoFitColumns();
                detailWorksheet.Cells[1, indexVitiRaportuesQK, rreshtiFunditIPerdorurRe, kolonaFunditEPerdorurRe].Style.Numberformat.Format = "0.00";
                if (shfaqDtPrintimi)
                {
                    detailWorksheet.InsertRow(rreshtiFunditIPerdorurRe + 1, 1);
                    detailWorksheet.Cells[rreshtiFunditIPerdorurRe + 1, 1].Value = DateTime.Now.ToString();
                    detailWorksheet.Cells[rreshtiFunditIPerdorurRe + 1, 1].Style.Font.Size = 10;
                    detailWorksheet.Cells[rreshtiFunditIPerdorurRe + 1, 1].Style.Font.Bold = true;
                    detailWorksheet.Cells[rreshtiFunditIPerdorurRe + 1, 1].Style.Font.Color.SetColor(Color.Gray);
                    detailWorksheet.Cells["A" + (rreshtiFunditIPerdorurRe + 1) + ":D" + (rreshtiFunditIPerdorurRe + 1)].Merge = true;
                }
                excPacRE.Save();
            }
            downloadFileToClient(filePathToOpen, null, Response, filePaths, true);
        }

        /// <summary>
        /// Vetem per Alpha Bank Flex Cube
        /// Hap file e excel qe exportohet nga grida te ambjenti i eksportimit, dhe e modifikon ate
        /// Gjithashtu, shton dhe nje sheet te ri
        /// </summary>
        /// <param name="Tbl"></param>
        /// <param name="emriSheet1Master"></param>
        /// <param name="emriSheet2Detail"></param>
        /// <param name="filePathToOpen"></param>
        /// <param name="Response"></param>
        public static void AddWorksheetToExcelWorkbook(DataTable Tbl, string emriSheet1Master, string emriSheet2Detail, string filePathToOpen, HttpResponse Response)
        {
            FileInfo newFile = new FileInfo(filePathToOpen);
            using (ExcelPackage excPac = new ExcelPackage(newFile))
            {
                //Get the work book in the file
                ExcelWorkbook WB = excPac.Workbook;
                //Hap file e excel
                ExcelWorksheet detailWorksheet = WB.Worksheets.ElementAt(0); //merr sheet-in e pare
                detailWorksheet.Name = emriSheet2Detail;
                detailWorksheet.Cells.Style.Font.Size = 10;
                detailWorksheet.Cells.Style.Font.Name = "Arial";
                detailWorksheet.Cells.Style.Numberformat.Format = "@";

                detailWorksheet.InsertRow(1, 1);
                detailWorksheet.Cells[1, 1].Value = "FCC.DETB_UPLOAD_DETAIL";
                detailWorksheet.Cells["A1"].Style.Font.Size = 8;
                detailWorksheet.Cells["A1"].Style.Font.Bold = true;

                int rreshtiFunditIPerdorur = detailWorksheet.Dimension.End.Row;
                int kolonaFunditEPerdorur = detailWorksheet.Dimension.End.Column;
                for (int i = 3; i <= rreshtiFunditIPerdorur + 1; i++)
                {
                    detailWorksheet.Cells[i, kolonaFunditEPerdorur + 1].Value = "~~END~~";
                }
                for (int i = 1; i <= kolonaFunditEPerdorur; i++)
                {
                    detailWorksheet.Cells[rreshtiFunditIPerdorur + 1, i].Value = "~~END~~";
                }
                detailWorksheet.Cells["F3:F" + rreshtiFunditIPerdorur].Style.Numberformat.Format = "dd/MM/yyyy";
                detailWorksheet.Cells["M3:M" + rreshtiFunditIPerdorur].Style.Numberformat.Format = "dd/MM/yyyy";
                detailWorksheet.Cells["A2:W2"].Style.Font.Size = 8;
                detailWorksheet.Cells["A2:W2"].Style.Font.Bold = true;
                int rreshtiFundit = rreshtiFunditIPerdorur + 1;
                string cellA = "A" + rreshtiFundit;
                string cellEnd = "W" + rreshtiFundit;
                detailWorksheet.Cells[cellA + ":" + cellEnd].Style.Font.Color.SetColor(Color.White);
                detailWorksheet.Cells[cellA + ":" + cellEnd].Style.Fill.PatternType = ExcelFillStyle.Solid;
                detailWorksheet.Cells[cellA + ":" + cellEnd].Style.Fill.BackgroundColor.SetColor(Color.Red);
                detailWorksheet.Cells[cellA + ":" + cellEnd].Style.Font.Bold = true;
                detailWorksheet.Cells[cellA + ":" + cellEnd].Style.Font.Size = 8;
                detailWorksheet.Cells["A2:V2"].Style.Font.Color.SetColor(Color.White);
                detailWorksheet.Cells["A2:V2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                detailWorksheet.Cells["A2:V2"].Style.Fill.BackgroundColor.SetColor(Color.Red);
                detailWorksheet.Cells["W2:W" + rreshtiFunditIPerdorur].Style.Font.Color.SetColor(Color.White);
                detailWorksheet.Cells["W2:W" + rreshtiFunditIPerdorur].Style.Fill.PatternType = ExcelFillStyle.Solid;
                detailWorksheet.Cells["W2:W" + rreshtiFunditIPerdorur].Style.Fill.BackgroundColor.SetColor(Color.Red);
                detailWorksheet.Cells["W2:W" + rreshtiFunditIPerdorur].Style.Font.Bold = true;
                detailWorksheet.Cells["W2:W" + rreshtiFunditIPerdorur].Style.Font.Size = 8;


                //Add 5 new worksheets to the workbook and fill some data
                var newWorksheet = excPac.Workbook.Worksheets.Add(emriSheet1Master);
                excPac.Workbook.Worksheets.MoveToStart(emriSheet1Master);
                //Add a worksheet to the workbook.
                // rows
                for (int i = 0; i < Tbl.Rows.Count; i++)
                {
                    // to do: format datetime values before printing
                    for (int j = 0; j < Tbl.Columns.Count; j++)
                    {
                        newWorksheet.Cells[(i + 1), (j + 1)].Value = Tbl.Rows[i][j];
                        if (!(i == 2 && (j == 0 || j == 3 || j == 4)))
                        {
                            newWorksheet.Cells.Style.Font.Bold = true;
                        }
                        newWorksheet.Cells["A3"].Style.Font.Bold = false;
                        newWorksheet.Cells["D3"].Style.Font.Bold = false;
                        newWorksheet.Cells["E3"].Style.Font.Bold = false;
                        newWorksheet.Cells.Style.Font.Size = 9;
                        newWorksheet.Cells.Style.Font.Name = "Arial";
                        newWorksheet.Cells.Style.Numberformat.Format = "@"; //i formaton qelizat ne formatin text
                        newWorksheet.Cells["A4:F4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        newWorksheet.Cells["A4:F4"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 255, 255));
                        newWorksheet.Cells["A2:F2"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        newWorksheet.Cells["A2:F2"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 255, 255));
                        newWorksheet.Cells["F3"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        newWorksheet.Cells["F3"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 255, 255));
                        newWorksheet.Cells["A1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        newWorksheet.Cells["A1"].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(204, 255, 255));
                    }
                }
                for (int i = 1; i < newWorksheet.Dimension.End.Column; i++)
                {
                    ExcelRange columnCells = newWorksheet.Cells[newWorksheet.Dimension.Start.Row, i, newWorksheet.Dimension.End.Row, i];
                    // Check what is the longest string and set the length
                    int maxLength = columnCells.Max(cell => (cell.Value == null ? 0 : cell.Value.ToString().Count(c => char.IsLetterOrDigit(c))));
                    newWorksheet.Column(i).Width = maxLength + 5;
                }

                //Name the sheet.
                newWorksheet.Name = emriSheet1Master;
                int[] gjatesiteDetail = new int[rreshtiFunditIPerdorur];
                for (int i = 1; i < kolonaFunditEPerdorur; i++)
                {
                    ExcelRange columnCells = detailWorksheet.Cells[detailWorksheet.Dimension.Start.Row, i, detailWorksheet.Dimension.End.Row, i];
                    // Check what is the longest string and set the length
                    int maxLength = columnCells.Max(cell => (cell.Value == null ? 0 : cell.Value.ToString().Count(c => char.IsLetterOrDigit(c))));
                    detailWorksheet.Column(i).Width = maxLength + 4;
                }

                excPac.Save();

                downloadFileToClient(filePathToOpen, WB, Response);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="filePathToOpen"></param>
        /// <param name="WB"></param>
        /// <param name="Response"></param>
        private static void downloadFileToClient(string filePathToOpen, ExcelWorkbook WB, HttpResponse Response, string[] filePaths = null, bool deleteAllFilesInDir = false)
        {
            FileInfo myfile = new FileInfo(filePathToOpen);

            // Checking if file exists
            if (myfile.Exists)
            {
                Response.Clear();
                Response.ClearContent();
                Response.ClearHeaders();
                Response.Charset = "";
                Response.AddHeader("Connection", "Keep-Alive");
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(myfile.Name, Encoding.UTF8));
                Response.WriteFile(myfile.FullName);
                Response.Flush();
                if (deleteAllFilesInDir)
                {
                    foreach (string filePath in filePaths)
                        File.Delete(filePath);
                }
                else
                    File.Delete(filePathToOpen);
                Response.End(); //gerta : hequr se nderpret threadin kryesor

            }
        }

        #endregion eksport_Per_AlphaBank

        public static clsMesazh eksportoLibrin(string emerFile, string DesignPath, DateTime dtfillimi, DataTable dt, int idNder, HttpSessionState Session, HttpRequest Request, HttpResponse Response)
        {//gerta: shtuar per eksportin e librave
            String filePathToOpen = "";
            string filePathToWrite = "";

            clsNdermarrje nd = new clsNdermarrje(idNder);
            try
            {
                string pathDir = HttpContext.Current.Server.MapPath(null) + @"\AlphaWebExport\";
                DirectoryExtension.CreateDirIfNotExists(pathDir);
                filePathToOpen = pathDir + emerFile + ".xlsx";
                filePathToWrite = pathDir + emerFile + "_" + DateTime.Now.ToString("ddMMyyyyHHmm") + ".xlsx";
            }
            catch (Exception ex)
            {
                return new clsMesazh(false, ex.Message);
            }

            switch (emerFile)
            {
                case "liber_blerje":
                    createExcelperLiberBlerje(dt, filePathToOpen, filePathToWrite, dtfillimi, nd.NdermarrjePershkrimi, nd.NdermarrjeNipt, Convert.ToString(dtfillimi.Year), Request, Response);
                    break;

                case "liber_shitje":
                    createExcelperLiberShitje(dt, filePathToOpen, filePathToWrite, dtfillimi, nd.NdermarrjePershkrimi, nd.NdermarrjeNipt, Convert.ToString(dtfillimi.Year), Request, Response);
                    break;

                case "liber_blerje2015":
                    if (DesignPath == "RaportetDs.Blerje.LibriBlerjes2015REF")
                    {
                        if (dt.Columns.Contains("EMERTIMIKF"))
                        {
                            dt.Columns.Remove("EMERTIMIKF");

                        }
                        createExcelperLiberBlerje2015(dt, filePathToOpen, filePathToWrite, dtfillimi, nd.NdermarrjePershkrimi, nd.NdermarrjeNipt, Convert.ToString(dtfillimi.Year), Request, Response);
                    }

                    else
                    {

                        if (dt.Columns.Contains("EMERTIMFATURE"))
                        {
                            dt.Columns.Remove("EMERTIMFATURE");
                        }
                        createExcelperLiberBlerje2015(dt, filePathToOpen, filePathToWrite, dtfillimi, nd.NdermarrjePershkrimi, nd.NdermarrjeNipt, Convert.ToString(dtfillimi.Year), Request, Response);
                    }

                    break;

                case "liber_shitje2015":
                    createExcelperLiberShitje2015(dt, filePathToOpen, filePathToWrite, dtfillimi, nd.NdermarrjePershkrimi, nd.NdermarrjeNipt, Convert.ToString(dtfillimi.Year), Request, Response);
                    break;
                case "LibriShitjes2019":
                    CreateExcelperLiberShitje2019(dt, filePathToOpen, filePathToWrite, dtfillimi, nd.NdermarrjePershkrimi, nd.NdermarrjeNipt, Convert.ToString(dtfillimi.Year), Request, Response);
                    break;
                case "liber_blerjeks2016":
                    createExcelperLiberBlerjeKS2016(dt, filePathToOpen, filePathToWrite, dtfillimi, nd.NdermarrjePershkrimi, nd.NdermarrjeNipt, Convert.ToString(dtfillimi.Year), Request, Response);
                    break;
                case "liber_shitjeks2016":
                    createExcelperLiberShitjeKS2016(dt, filePathToOpen, filePathToWrite, dtfillimi, nd.NdermarrjePershkrimi, nd.NdermarrjeNipt, Convert.ToString(dtfillimi.Year), Request, Response);
                    break;
                case "libri_blerjes_bankat":
                    createExcelperLiberBlerjeBanka(dt, filePathToOpen, filePathToWrite, dtfillimi, nd.NdermarrjePershkrimi, nd.NdermarrjeNipt, Convert.ToString(dtfillimi.Year), Request, Response);
                    break;
                case "LibriBlerjes2019":

                    if (DesignPath == "Blerje.LibriBlerjes2019MeNrReference")
                    {
                        if (dt.Columns.Contains("EMERTIMIKF"))
                        {
                            dt.Columns.Remove("EMERTIMIKF");

                        }
                        CreateExcelperLiberBlerje2019(dt, filePathToOpen, filePathToWrite, dtfillimi, nd.NdermarrjePershkrimi, nd.NdermarrjeNipt, Convert.ToString(dtfillimi.Year), Request, Response);
                    }

                    else
                    {

                        if (dt.Columns.Contains("EMERTIMFATURE"))
                        {
                            dt.Columns.Remove("EMERTIMFATURE");
                        }

                        CreateExcelperLiberBlerje2019(dt, filePathToOpen, filePathToWrite, dtfillimi, nd.NdermarrjePershkrimi, nd.NdermarrjeNipt, Convert.ToString(dtfillimi.Year), Request, Response);
                    }

                    break;
            }

            return new clsMesazh(true);
        }

        private static void createExcelperLiberShitjeKS2016(DataTable Tbl, string filePathToOpen, string pathtoWrite, DateTime dtfillimi, string nderEmer, string nderNipt, string viti, HttpRequest Request, HttpResponse Response)
        {
            int lastDataCell;
            FileInfo newFile = new FileInfo(filePathToOpen);
            FileInfo newFile2 = new FileInfo(pathtoWrite);

            using (ExcelPackage excPac = new ExcelPackage(newFile))
            {
                //Get the work book in the file
                ExcelWorkbook WB = excPac.Workbook;
                //Hap file e excel
                ExcelWorksheet oXLSheet = WB.Worksheets.ElementAt(0);

                oXLSheet.Cells["E3"].Value = nderEmer;
                oXLSheet.Cells["E4"].Value = nderNipt;
                oXLSheet.Cells["E5"].Value = viti;

                if (dtfillimi.Month < 10)
                {
                    oXLSheet.Cells["E6"].Value = "0" + Convert.ToString(dtfillimi.Month) + "/" + viti;
                }
                else
                {
                    oXLSheet.Cells["E6"].Value = Convert.ToString(dtfillimi.Month) + "/" + viti;
                }

                if (Tbl.Rows.Count == 0)
                {
                    lastDataCell = 22;
                    oXLSheet.Cells["J4"].Value = "Ska te dhena ";
                }
                else
                {
                    lastDataCell = 11 + Tbl.Rows.Count;
                    if (lastDataCell < 21) lastDataCell = 21;

                    for (int i = 0; i < Tbl.Rows.Count; i++)
                    {
                        for (int j = 0; j < Tbl.Columns.Count - 1; j++)
                        {
                            if (j == 1)
                                oXLSheet.Cells[(i + 12), (j + 2)].Value = Convert.ToString(Tbl.Rows[i][j]).Substring(0, 2) + "." + Convert.ToString(Tbl.Rows[i][j]).Substring(3, 2) + "." + Convert.ToString(Tbl.Rows[i][j]).Substring(6, 4);

                            else
                                oXLSheet.Cells[(i + 12), (j + 2)].Value = Tbl.Rows[i][j];
                            oXLSheet.Cells[(i + 12), (j + 2)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }

                    //formatimi i dates si text
                    oXLSheet.Cells["C12:C" + lastDataCell].Style.Numberformat.Format = "@";
                    //formatimi i numrave

                    oXLSheet.Cells["H11:H" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["I11:I" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["J11:J" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["K11:K" + lastDataCell].Style.Numberformat.Format = "#,##0.00";

                    oXLSheet.Cells["L11:L" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["L12:L" + lastDataCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    oXLSheet.Cells["L12:L" + lastDataCell].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(234, 240, 246));

                    oXLSheet.Cells["M11:M" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["N11:N" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["O11:O" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["P11:P" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["Q11:Q" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["R11:R" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["S11:S" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["S12:S" + lastDataCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    oXLSheet.Cells["S12:S" + lastDataCell].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(234, 240, 246));

                    oXLSheet.Cells["T11:T" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["U11:U" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["V11:V" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["W11:W" + lastDataCell].Style.Numberformat.Format = "#,##0.00";

                    oXLSheet.Cells["X11:X" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["X12:X" + lastDataCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    oXLSheet.Cells["X12:X" + lastDataCell].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(234, 240, 246));

                    oXLSheet.Cells["Y11:Y" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["Y12:Y" + lastDataCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    oXLSheet.Cells["Y12:Y" + lastDataCell].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(221, 217, 196));

                    //'vendosim totalet
                    oXLSheet.Cells["H11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["I11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["J11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["K11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["L11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["M11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["N11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["O11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["P11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["Q11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["R11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["S11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["T11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["U11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["V11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["W11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["X11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["Y11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";

                }

                excPac.SaveAs(newFile2);
                downloadFileToClient(pathtoWrite, WB, Response);
            }
        }


        /// <summary>
        /// Vetem per Librat
        /// Hap file template te excel per librat, dhe e modifikon ate me te dhenat e raportit
        /// </summary>
        /// <param name="Tbl"></param>
        /// <param name="filePathToOpen"></param>
        /// <param name="Response"></param>
        private static void createExcelperLiberBlerje(DataTable Tbl, string filePathToOpen, string pathtoWrite, DateTime dtfillimi, string nderEmer, string nderNipt, string viti, HttpRequest Request, HttpResponse Response)
        {
            int lastDataCell;
            FileInfo newFile = new FileInfo(filePathToOpen);
            FileInfo newFile2 = new FileInfo(pathtoWrite);

            using (ExcelPackage excPac = new ExcelPackage(newFile))
            {
                //Get the work book in the file
                ExcelWorkbook WB = excPac.Workbook;
                //Hap file e excel
                ExcelWorksheet oXLSheet = WB.Worksheets.ElementAt(0);

                oXLSheet.Cells["C2"].Value = nderEmer;
                oXLSheet.Cells["C3"].Value = nderNipt;
                oXLSheet.Cells["C4"].Value = viti;
                oXLSheet.Cells["C5"].Value = (dtfillimi.Month < 10) ? "0" + dtfillimi.Month : Convert.ToString(dtfillimi.Month);

                if (Tbl.Rows.Count == 0)
                {
                    lastDataCell = 22;
                    oXLSheet.Cells["B7"].Value = "PO";
                }
                else
                {
                    lastDataCell = 12 + Tbl.Rows.Count;
                    if (lastDataCell < 22) lastDataCell = 22;

                    for (int i = 0; i < Tbl.Rows.Count; i++)
                    {
                        for (int j = 0; j < Tbl.Columns.Count; j++)
                        {
                            oXLSheet.Cells[(i + 13), (j + 1)].Value = Tbl.Rows[i][j];

                            oXLSheet.Cells[(i + 13), (j + 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }

                    oXLSheet.Cells["C13:C" + lastDataCell].Style.Numberformat.Format = "dd/MM/yyyy";

                    //'vendosim totalet
                    oXLSheet.Cells["G" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["H" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["I" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["J" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["K" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["L" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["M" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["N" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["O" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["P" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["Q" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["R" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                }

                // 'krijojme footer-in

                oXLSheet.Cells["A" + (lastDataCell + 1)].Value = "Shuma totale";
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":F" + (lastDataCell + 1)].Merge = true;
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":F" + (lastDataCell + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                oXLSheet.Cells["A" + (lastDataCell + 1) + ":R" + (lastDataCell + 1)].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":R" + (lastDataCell + 1)].Style.Font.Bold = true;

                oXLSheet.Cells["A" + (lastDataCell + 2)].Value = "Kutia sipas Formularit te Deklarimit dhe Pageses se TVSH-se";
                oXLSheet.Cells["A" + (lastDataCell + 2) + ":G" + (lastDataCell + 2)].Merge = true;

                oXLSheet.Cells["A" + (lastDataCell + 2) + ":R" + (lastDataCell + 2)].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                oXLSheet.Cells["A" + (lastDataCell + 2) + ":R" + (lastDataCell + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //'vendosim emrat e fushave
                oXLSheet.Cells["H" + (lastDataCell + 2)].Value = "kutia (15)";
                oXLSheet.Cells["I" + (lastDataCell + 2)].Value = "kutia (16)";
                oXLSheet.Cells["J" + (lastDataCell + 2)].Value = "kutia (17)";
                oXLSheet.Cells["K" + (lastDataCell + 2)].Value = "kutia (18)";
                oXLSheet.Cells["L" + (lastDataCell + 2)].Value = "kutia (19)";
                oXLSheet.Cells["M" + (lastDataCell + 2)].Value = "kutia (20)";
                oXLSheet.Cells["N" + (lastDataCell + 2)].Value = "kutia (21)";
                oXLSheet.Cells["O" + (lastDataCell + 2)].Value = "kutia (22)";
                oXLSheet.Cells["P" + (lastDataCell + 2)].Value = "kutia (23)";
                oXLSheet.Cells["Q" + (lastDataCell + 2)].Value = "kutia (24)";
                oXLSheet.Cells["R" + (lastDataCell + 2)].Value = "kutia (25)";

                //'emri mbiemri
                oXLSheet.Cells["Q" + (lastDataCell + 5)].Value = "Emri Mbiemri";
                oXLSheet.Cells["Q" + (lastDataCell + 5)].Style.Font.Bold = true;

                //'shpjegimi
                oXLSheet.Cells["A" + (lastDataCell + 7)].Value = "Shpjegim:";
                oXLSheet.Cells["A" + (lastDataCell + 8)].Value = "Ne rastin kur ky liber do te printohet, per qellime te ruajtjes/ mbajtjes se dokumentacionit, cdo faqe e printuar duhet te kete ne krye permbajtjen / rreshtat nga 1 ne 12.";
                oXLSheet.Cells["A" + (lastDataCell + 9)].Value = "Nr i rreshtave mund te shtohet ne perputhje me nr e transaksioneve";

                excPac.SaveAs(newFile2);
                downloadFileToClient(pathtoWrite, WB, Response);
            }
        }

        private static void createExcelperLiberBlerje2015(DataTable Tbl, string filePathToOpen, string pathtoWrite, DateTime dtfillimi, string nderEmer, string nderNipt, string viti, HttpRequest Request, HttpResponse Response)
        {
            int lastDataCell;
            FileInfo newFile = new FileInfo(filePathToOpen);
            FileInfo newFile2 = new FileInfo(pathtoWrite);

            using (ExcelPackage excPac = new ExcelPackage(newFile))
            {
                //Get the work book in the file
                ExcelWorkbook WB = excPac.Workbook;
                //Hap file e excel
                ExcelWorksheet oXLSheet = WB.Worksheets.ElementAt(0);

                oXLSheet.Cells["C2"].Value = nderEmer;
                oXLSheet.Cells["C3"].Value = nderNipt;
                oXLSheet.Cells["C4"].Value = viti;
                oXLSheet.Cells["C5"].Value = (dtfillimi.Month < 10) ? "0" + dtfillimi.Month : Convert.ToString(dtfillimi.Month);

                if (Tbl.Rows.Count == 0)
                {
                    lastDataCell = 22;
                    oXLSheet.Cells["B7"].Value = "PO";
                }
                else
                {
                    lastDataCell = 12 + Tbl.Rows.Count;
                    if (lastDataCell < 22) lastDataCell = 22;

                    for (int i = 0; i < Tbl.Rows.Count; i++)
                    {
                        for (int j = 0; j < Tbl.Columns.Count - 2; j++)
                        {
                            if (j == 2)
                                oXLSheet.Cells[(i + 13), (j + 1)].Value = Convert.ToString(Tbl.Rows[i][j]).Substring(0, 10);
                            else
                                oXLSheet.Cells[(i + 13), (j + 1)].Value = Tbl.Rows[i][j];

                            oXLSheet.Cells[(i + 13), (j + 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }

                    //formatimi i dates si text
                    oXLSheet.Cells["C13:C" + lastDataCell].Style.Numberformat.Format = "@";
                    //formatimi i numrave
                    oXLSheet.Cells["G13:G" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["H13:H" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["I13:I" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["J13:J" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["K13:K" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["L13:L" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["M13:M" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["N13:N" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["O13:O" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["P13:P" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["Q13:Q" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["R13:R" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["S13:S" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["T13:T" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["U13:U" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["V13:V" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["W13:W" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["X13:X" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["Y13:Y" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["Z13:Z" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["AA13:AA" + lastDataCell + 1].Style.Numberformat.Format = "0";

                    //'vendosim totalet
                    oXLSheet.Cells["G" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["H" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["I" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["J" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["K" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["L" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["M" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["N" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["O" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["P" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["Q" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["R" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["S" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["T" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["U" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["V" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["W" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["X" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["Y" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["Z" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["AA" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                }

                // 'krijojme footer-in

                oXLSheet.Cells["A" + (lastDataCell + 1)].Value = "Shuma totale";
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":F" + (lastDataCell + 1)].Merge = true;
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":F" + (lastDataCell + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                oXLSheet.Cells["A" + (lastDataCell + 1) + ":AA" + (lastDataCell + 1)].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":AA" + (lastDataCell + 1)].Style.Font.Bold = true;

                oXLSheet.Cells["A" + (lastDataCell + 2)].Value = "Kutia sipas Formularit te Deklarimit dhe Pageses se TVSH-se";
                oXLSheet.Cells["A" + (lastDataCell + 2) + ":G" + (lastDataCell + 2)].Merge = true;

                oXLSheet.Cells["A" + (lastDataCell + 2) + ":AA" + (lastDataCell + 2)].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                oXLSheet.Cells["A" + (lastDataCell + 2) + ":AA" + (lastDataCell + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //'vendosim emrat e fushave
                oXLSheet.Cells["H" + (lastDataCell + 2)].Value = "kutia (24)";
                oXLSheet.Cells["I" + (lastDataCell + 2)].Value = "kutia (25)";
                oXLSheet.Cells["J" + (lastDataCell + 2)].Value = "kutia (26)";
                oXLSheet.Cells["K" + (lastDataCell + 2)].Value = "kutia (27)";
                oXLSheet.Cells["L" + (lastDataCell + 2)].Value = "kutia (28)";
                oXLSheet.Cells["M" + (lastDataCell + 2)].Value = "kutia (29)";
                oXLSheet.Cells["N" + (lastDataCell + 2)].Value = "kutia (30)";
                oXLSheet.Cells["O" + (lastDataCell + 2)].Value = "kutia (31)";
                oXLSheet.Cells["P" + (lastDataCell + 2)].Value = "kutia (32)";
                oXLSheet.Cells["Q" + (lastDataCell + 2)].Value = "kutia (33)";
                oXLSheet.Cells["R" + (lastDataCell + 2)].Value = "kutia (34)";
                oXLSheet.Cells["S" + (lastDataCell + 2)].Value = "kutia (35)";
                oXLSheet.Cells["T" + (lastDataCell + 2)].Value = "kutia (36)";
                oXLSheet.Cells["U" + (lastDataCell + 2)].Value = "kutia (37)";
                oXLSheet.Cells["V" + (lastDataCell + 2)].Value = "kutia (38)";
                oXLSheet.Cells["W" + (lastDataCell + 2)].Value = "kutia (39)";
                oXLSheet.Cells["X" + (lastDataCell + 2)].Value = "kutia (40)";
                oXLSheet.Cells["Y" + (lastDataCell + 2)].Value = "kutia (41)";
                oXLSheet.Cells["Z" + (lastDataCell + 2)].Value = "kutia (42)";
                oXLSheet.Cells["AA" + (lastDataCell + 2)].Value = "kutia (43)";

                //'emri mbiemri
                oXLSheet.Cells["T" + (lastDataCell + 5)].Value = "Emri Mbiemri";
                oXLSheet.Cells["T" + (lastDataCell + 5)].Style.Font.Bold = true;

                //'shpjegimi
                oXLSheet.Cells["A" + (lastDataCell + 7)].Value = "Shpjegim:";
                oXLSheet.Cells["A" + (lastDataCell + 8)].Value = "Ne rastin kur ky liber do te printohet, per qellime te ruajtjes/ mbajtjes se dokumentacionit, cdo faqe e printuar duhet te kete ne krye permbajtjen / rreshtat nga 1 ne 12.";
                oXLSheet.Cells["A" + (lastDataCell + 9)].Value = "Nr i rreshtave mund te shtohet ne perputhje me nr e transaksioneve";

                excPac.SaveAs(newFile2);
                downloadFileToClient(pathtoWrite, WB, Response);
            }
        }

        private static void CreateExcelperLiberBlerje2019(DataTable Tbl, string filePathToOpen, string pathtoWrite, DateTime dtfillimi, string nderEmer, string nderNipt, string viti, HttpRequest Request, HttpResponse Response)
        {
            int lastDataCell;
            FileInfo newFile = new FileInfo(filePathToOpen);
            FileInfo newFile2 = new FileInfo(pathtoWrite);

            using (ExcelPackage excPac = new ExcelPackage(newFile))
            {
                //Get the work book in the file
                ExcelWorkbook WB = excPac.Workbook;
                //Hap file e excel
                ExcelWorksheet oXLSheet = WB.Worksheets.ElementAt(0);

                oXLSheet.Cells["C2"].Value = nderEmer;
                oXLSheet.Cells["C3"].Value = nderNipt;
                oXLSheet.Cells["C4"].Value = viti;
                oXLSheet.Cells["C5"].Value = (dtfillimi.Month < 10) ? "0" + dtfillimi.Month : Convert.ToString(dtfillimi.Month);

                if (Tbl.Rows.Count == 0)
                {
                    lastDataCell = 22;
                    oXLSheet.Cells["B7"].Value = "PO";
                }
                else
                {
                    lastDataCell = 12 + Tbl.Rows.Count;
                    if (lastDataCell < 22) lastDataCell = 22;

                    for (int i = 0; i < Tbl.Rows.Count; i++)
                    {
                        for (int j = 0; j < Tbl.Columns.Count - 2; j++)
                        {
                            if (j == 2)
                                oXLSheet.Cells[(i + 13), (j + 1)].Value = Convert.ToString(Tbl.Rows[i][j]).Substring(0, 10);
                            else
                                oXLSheet.Cells[(i + 13), (j + 1)].Value = Tbl.Rows[i][j];

                            oXLSheet.Cells[(i + 13), (j + 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }

                    //formatimi i dates si text
                    oXLSheet.Cells["C13:C" + lastDataCell].Style.Numberformat.Format = "@";
                    //formatimi i numrave
                    oXLSheet.Cells["G13:G" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["H13:H" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["I13:I" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["J13:J" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["K13:K" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["L13:L" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["M13:M" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["N13:N" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["O13:O" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["P13:P" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["Q13:Q" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["R13:R" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["S13:S" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["T13:T" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["U13:U" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["V13:V" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["W13:W" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["X13:X" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["Y13:Y" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["Z13:Z" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["AA13:AA" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["AB13:AB" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["AC13:AC" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["AD13:AD" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["AE13:AE" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["AF13:AF" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["AG13:AG" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["AH13:AH" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["AI13:AI" + lastDataCell + 1].Style.Numberformat.Format = "0";

                    //'vendosim totalet
                    oXLSheet.Cells["G" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["H" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["I" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["J" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["K" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["L" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["M" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["N" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["O" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["P" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["Q" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["R" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["S" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["T" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["U" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["V" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["W" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["X" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["Y" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["Z" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["AA" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["AB" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["AC" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["AD" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["AE" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["AF" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["AG" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["AH" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["AI" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                }

                // 'krijojme footer-in

                oXLSheet.Cells["A" + (lastDataCell + 1)].Value = "Shuma totale";
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":F" + (lastDataCell + 1)].Merge = true;
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":F" + (lastDataCell + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                oXLSheet.Cells["A" + (lastDataCell + 1) + ":AI" + (lastDataCell + 1)].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":AI" + (lastDataCell + 1)].Style.Font.Bold = true;

                oXLSheet.Cells["A" + (lastDataCell + 2)].Value = "Kutia sipas Formularit te Deklarimit dhe Pageses se TVSH-se";
                oXLSheet.Cells["A" + (lastDataCell + 2) + ":G" + (lastDataCell + 2)].Merge = true;

                oXLSheet.Cells["A" + (lastDataCell + 2) + ":AI" + (lastDataCell + 2)].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                oXLSheet.Cells["A" + (lastDataCell + 2) + ":AI" + (lastDataCell + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //'vendosim emrat e fushave
                oXLSheet.Cells["H" + (lastDataCell + 2)].Value = "kutia (26)";
                oXLSheet.Cells["I" + (lastDataCell + 2)].Value = "kutia (27)";
                oXLSheet.Cells["J" + (lastDataCell + 2)].Value = "kutia (28)";
                oXLSheet.Cells["K" + (lastDataCell + 2)].Value = "kutia (29)";
                oXLSheet.Cells["L" + (lastDataCell + 2)].Value = "kutia (30)";
                oXLSheet.Cells["M" + (lastDataCell + 2)].Value = "kutia (31)";
                oXLSheet.Cells["N" + (lastDataCell + 2)].Value = "kutia (32)";
                oXLSheet.Cells["O" + (lastDataCell + 2)].Value = "kutia (33)";
                oXLSheet.Cells["P" + (lastDataCell + 2)].Value = "kutia (34)";
                oXLSheet.Cells["Q" + (lastDataCell + 2)].Value = "kutia (35)";
                oXLSheet.Cells["R" + (lastDataCell + 2)].Value = "kutia (36)";
                oXLSheet.Cells["S" + (lastDataCell + 2)].Value = "kutia (37)";
                oXLSheet.Cells["T" + (lastDataCell + 2)].Value = "kutia (38)";
                oXLSheet.Cells["U" + (lastDataCell + 2)].Value = "kutia (39)";
                oXLSheet.Cells["V" + (lastDataCell + 2)].Value = "kutia (40)";
                oXLSheet.Cells["W" + (lastDataCell + 2)].Value = "kutia (41)";
                oXLSheet.Cells["X" + (lastDataCell + 2)].Value = "kutia (42)";
                oXLSheet.Cells["Y" + (lastDataCell + 2)].Value = "kutia (43)";
                oXLSheet.Cells["Z" + (lastDataCell + 2)].Value = "kutia (44)";
                oXLSheet.Cells["AA" + (lastDataCell + 2)].Value = "kutia (45)";
                oXLSheet.Cells["AB" + (lastDataCell + 2)].Value = "kutia (46)";
                oXLSheet.Cells["AC" + (lastDataCell + 2)].Value = "kutia (47)";
                oXLSheet.Cells["AD" + (lastDataCell + 2)].Value = "kutia (48)";
                oXLSheet.Cells["AE" + (lastDataCell + 2)].Value = "kutia (49)";
                oXLSheet.Cells["AF" + (lastDataCell + 2)].Value = "kutia (50)";
                oXLSheet.Cells["AG" + (lastDataCell + 2)].Value = "kutia (51)";
                oXLSheet.Cells["AH" + (lastDataCell + 2)].Value = "kutia (52)";
                oXLSheet.Cells["AI" + (lastDataCell + 2)].Value = "kutia (53)";

                //'emri mbiemri
                oXLSheet.Cells["T" + (lastDataCell + 5)].Value = "Emri Mbiemri";
                oXLSheet.Cells["T" + (lastDataCell + 5)].Style.Font.Bold = true;

                //'shpjegimi
                oXLSheet.Cells["A" + (lastDataCell + 7)].Value = "Shpjegim:";
                oXLSheet.Cells["A" + (lastDataCell + 8)].Value = "Ne rastin kur ky liber do te printohet, per qellime te ruajtjes/ mbajtjes se dokumentacionit, cdo faqe e printuar duhet te kete ne krye permbajtjen / rreshtat nga 1 ne 12.";
                oXLSheet.Cells["A" + (lastDataCell + 9)].Value = "Nr i rreshtave mund te shtohet ne perputhje me nr e transaksioneve";

                excPac.SaveAs(newFile2);
                downloadFileToClient(pathtoWrite, WB, Response);
            }
        }


        private static void createExcelperLiberBlerjeKS2016(DataTable Tbl, string filePathToOpen, string pathtoWrite, DateTime dtfillimi, string nderEmer, string nderNipt, string viti, HttpRequest Request, HttpResponse Response)
        {
            int lastDataCell;
            FileInfo newFile = new FileInfo(filePathToOpen);
            FileInfo newFile2 = new FileInfo(pathtoWrite);

            using (ExcelPackage excPac = new ExcelPackage(newFile))
            {
                //Get the work book in the file
                ExcelWorkbook WB = excPac.Workbook;
                //Hap file e excel
                ExcelWorksheet oXLSheet = WB.Worksheets.ElementAt(0);

                oXLSheet.Cells["E3"].Value = nderEmer;
                oXLSheet.Cells["E4"].Value = nderNipt;
                oXLSheet.Cells["E5"].Value = viti;

                if (dtfillimi.Month < 10)
                {
                    oXLSheet.Cells["E6"].Value = "0" + Convert.ToString(dtfillimi.Month) + "/" + viti;
                }
                else
                {
                    oXLSheet.Cells["E6"].Value = Convert.ToString(dtfillimi.Month) + "/" + viti;
                }

                if (Tbl.Rows.Count == 0)
                {
                    lastDataCell = 22;
                    oXLSheet.Cells["J4"].Value = "Ska te dhena ";
                }
                else
                {
                    lastDataCell = 11 + Tbl.Rows.Count;
                    if (lastDataCell < 21) lastDataCell = 21;

                    for (int i = 0; i < Tbl.Rows.Count; i++)
                    {
                        for (int j = 0; j < Tbl.Columns.Count - 1; j++)
                        {
                            if (j == 1)
                                oXLSheet.Cells[(i + 12), (j + 2)].Value = Convert.ToString(Tbl.Rows[i][j]).Substring(0, 2) + "." + Convert.ToString(Tbl.Rows[i][j]).Substring(3, 2) + "." + Convert.ToString(Tbl.Rows[i][j]).Substring(6, 4);

                            else
                                oXLSheet.Cells[(i + 12), (j + 2)].Value = Tbl.Rows[i][j];
                            oXLSheet.Cells[(i + 12), (j + 2)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }

                    //formatimi i dates si text
                    oXLSheet.Cells["C12:C" + lastDataCell].Style.Numberformat.Format = "@";
                    //formatimi i numrave

                    oXLSheet.Cells["H11:H" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["I11:I" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["J11:J" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["K11:K" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["L11:L" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["M11:M" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["N11:N" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["O11:O" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["P11:P" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["Q11:Q" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["R11:R" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["S11:S" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["T11:T" + lastDataCell].Style.Numberformat.Format = "#,##0.00";

                    oXLSheet.Cells["T12:T" + lastDataCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    oXLSheet.Cells["T12:T" + lastDataCell].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(225, 255, 225));
                    oXLSheet.Cells["U11:U" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["V11:V" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["W11:W" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["X11:X" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["Y11:Y" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["Z11:Z" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["AA11:AA" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["AB11:AB" + lastDataCell].Style.Numberformat.Format = "#,##0.00";

                    oXLSheet.Cells["AC11:AC" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["AC12:AC" + lastDataCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    oXLSheet.Cells["AC12:AC" + lastDataCell].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(225, 255, 225));

                    oXLSheet.Cells["AD11:AD" + lastDataCell].Style.Numberformat.Format = "#,##0.00";
                    oXLSheet.Cells["AD12:AD" + lastDataCell].Style.Fill.PatternType = ExcelFillStyle.Solid;
                    oXLSheet.Cells["AD12:AD" + lastDataCell].Style.Fill.BackgroundColor.SetColor(Color.FromArgb(221, 217, 196));

                    //'vendosim totalet
                    oXLSheet.Cells["H11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["I11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["J11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["K11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["L11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["M11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["N11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["O11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["P11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["Q11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["R11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["S11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["T11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["U11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["V11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["W11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["X11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["Y11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["Z11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["AA11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["AB11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["AC11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                    oXLSheet.Cells["AD11"].FormulaR1C1 = "=SUM(R[" + (lastDataCell - 11) + "]C:R[1]C)";
                }

                excPac.SaveAs(newFile2);
                downloadFileToClient(pathtoWrite, WB, Response);
            }
        }

        private static void createExcelperLiberShitje2015(DataTable Tbl, string filePathToOpen, string pathtoWrite, DateTime dtfillimi, string nderEmer, string nderNipt, string viti, HttpRequest Request, HttpResponse Response)
        {
            int lastDataCell;
            FileInfo newFile = new FileInfo(filePathToOpen);
            FileInfo newFile2 = new FileInfo(pathtoWrite);

            using (ExcelPackage excPac = new ExcelPackage(newFile))
            {
                //Get the work book in the file
                ExcelWorkbook WB = excPac.Workbook;
                //Hap file e excel
                ExcelWorksheet oXLSheet = WB.Worksheets.ElementAt(0);
                double value;
                oXLSheet.Cells["C2"].Value = nderEmer;
                oXLSheet.Cells["C3"].Value = nderNipt;
                oXLSheet.Cells["C4"].Value = viti;
                oXLSheet.Cells["C5"].Value = (dtfillimi.Month < 10) ? "0" + dtfillimi.Month : Convert.ToString(dtfillimi.Month);

                if (Tbl.Rows.Count == 0)
                {
                    lastDataCell = 22;
                    oXLSheet.Cells["B7"].Value = "PO";
                }
                else
                {
                    lastDataCell = 12 + Tbl.Rows.Count;
                    if (lastDataCell < 22) lastDataCell = 22;
                    for (int i = 0; i < Tbl.Rows.Count; i++)
                    {
                        double totaliShitjeve = 0;
                        for (int j = 0; j < Tbl.Columns.Count - 2; j++)
                        {
                            if (j == 2)
                                oXLSheet.Cells[(i + 13), (j + 1)].Value = Convert.ToString(Tbl.Rows[i][j]).Substring(0, 10);
                            else if (j != 6)
                            {
                                var vlerQelize = (double.TryParse(Tbl.Rows[i][j].ToString(), out value)) ? Math.Round(value) : Tbl.Rows[i][j];
                                oXLSheet.Cells[(i + 13), (j + 1)].Value = vlerQelize;
                                if (j >= 7 && j <= 18)
                                    totaliShitjeve += Convert.ToDouble(vlerQelize);
                            }
                            oXLSheet.Cells[(i + 13), (j + 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                        oXLSheet.Cells[(i + 13), (7)].Value = totaliShitjeve;
                    }

                    oXLSheet.Cells["C13:C" + lastDataCell].Style.Numberformat.Format = "@";

                    oXLSheet.Cells["G13:G" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["H13:H" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["I13:I" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["J13:J" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["K13:K" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["L13:L" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["M13:M" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["N13:N" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["O13:O" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["P13:P" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["Q13:Q" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["R13:R" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["S13:S" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["T13:T" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["U13:U" + lastDataCell + 1].Style.Numberformat.Format = "0";

                    //'vendosim totalet
                    oXLSheet.Cells["G" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["H" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["I" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["J" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["K" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["L" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["M" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["N" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["O" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["P" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["Q" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["R" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["S" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["T" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["U" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                }

                // 'krijojme footer-in

                oXLSheet.Cells["A" + (lastDataCell + 1)].Value = "Shuma totale";
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":F" + (lastDataCell + 1)].Merge = true;
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":F" + (lastDataCell + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                oXLSheet.Cells["A" + (lastDataCell + 1) + ":U" + (lastDataCell + 1)].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":U" + (lastDataCell + 1)].Style.Font.Bold = true;

                oXLSheet.Cells["A" + (lastDataCell + 2)].Value = "Kutia sipas Formularit te Deklarimit dhe Pageses se TVSH-se";
                oXLSheet.Cells["A" + (lastDataCell + 2) + ":G" + (lastDataCell + 2)].Merge = true;

                oXLSheet.Cells["A" + (lastDataCell + 2) + ":U" + (lastDataCell + 2)].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                oXLSheet.Cells["A" + (lastDataCell + 2) + ":U" + (lastDataCell + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //'vendosim emrat e fushave
                oXLSheet.Cells["H" + (lastDataCell + 2)].Value = "kutia (9)";
                oXLSheet.Cells["I" + (lastDataCell + 2)].Value = "kutia (10)";
                oXLSheet.Cells["J" + (lastDataCell + 2)].Value = "kutia (11)";
                oXLSheet.Cells["K" + (lastDataCell + 2)].Value = "kutia (12)";
                oXLSheet.Cells["L" + (lastDataCell + 2)].Value = "kutia (13)";
                oXLSheet.Cells["M" + (lastDataCell + 2)].Value = "kutia (14)";
                oXLSheet.Cells["N" + (lastDataCell + 2)].Value = "kutia (15)";
                oXLSheet.Cells["O" + (lastDataCell + 2)].Value = "kutia (16)";
                oXLSheet.Cells["P" + (lastDataCell + 2)].Value = "kutia (17)";
                oXLSheet.Cells["Q" + (lastDataCell + 2)].Value = "kutia (18)";
                oXLSheet.Cells["R" + (lastDataCell + 2)].Value = "kutia (19)";
                oXLSheet.Cells["S" + (lastDataCell + 2)].Value = "kutia (20)";
                oXLSheet.Cells["T" + (lastDataCell + 2)].Value = "kutia (21)";
                oXLSheet.Cells["U" + (lastDataCell + 2)].Value = "kutia (22)";

                //'emri mbiemri
                oXLSheet.Cells["L" + (lastDataCell + 5)].Value = "Emri Mbiemri";
                oXLSheet.Cells["L" + (lastDataCell + 5)].Style.Font.Bold = true;

                //'shpjegimi
                oXLSheet.Cells["A" + (lastDataCell + 7)].Value = "Shpjegim:";
                oXLSheet.Cells["A" + (lastDataCell + 8)].Value = "Ne rastin kur ky liber do te printohet, per qellime te ruajtjes/ mbajtjes se dokumentacionit, cdo faqe e printuar duhet te kete ne krye permbajtjen / rreshtat nga 1 ne 12.";
                oXLSheet.Cells["A" + (lastDataCell + 9)].Value = "Nr i rreshtave mund te shtohet ne perputhje me nr e transaksioneve";

                excPac.SaveAs(newFile2);
                downloadFileToClient(pathtoWrite, WB, Response);
            }
        }

        private static void CreateExcelperLiberShitje2019(DataTable Tbl, string filePathToOpen, string pathtoWrite, DateTime dtfillimi, string nderEmer, string nderNipt, string viti, HttpRequest Request, HttpResponse Response)
        {
            int lastDataCell;
            FileInfo newFile = new FileInfo(filePathToOpen);
            FileInfo newFile2 = new FileInfo(pathtoWrite);

            using (ExcelPackage excPac = new ExcelPackage(newFile))
            {
                //Get the work book in the file
                ExcelWorkbook WB = excPac.Workbook;
                //Hap file e excel
                ExcelWorksheet oXLSheet = WB.Worksheets.ElementAt(0);
                double value;
                oXLSheet.Cells["C2"].Value = nderEmer;
                oXLSheet.Cells["C3"].Value = nderNipt;
                oXLSheet.Cells["C4"].Value = viti;
                oXLSheet.Cells["C5"].Value = (dtfillimi.Month < 10) ? "0" + dtfillimi.Month : Convert.ToString(dtfillimi.Month);

                if (Tbl.Rows.Count == 0)
                {
                    lastDataCell = 22;
                    oXLSheet.Cells["B7"].Value = "PO";
                }
                else
                {
                    lastDataCell = 12 + Tbl.Rows.Count;
                    if (lastDataCell < 22) lastDataCell = 22;
                    for (int i = 0; i < Tbl.Rows.Count; i++)
                    {
                        double totaliShitjeve = 0;
                        for (int j = 0; j < Tbl.Columns.Count - 2; j++)
                        {
                            if (j == 2)
                                oXLSheet.Cells[(i + 13), (j + 1)].Value = Convert.ToString(Tbl.Rows[i][j]).Substring(0, 10);
                            else if (j != 6)
                            {
                                var vlerQelize = (double.TryParse(Tbl.Rows[i][j].ToString(), out value)) ? Math.Round(value) : Tbl.Rows[i][j];
                                oXLSheet.Cells[(i + 13), (j + 1)].Value = vlerQelize;
                                if (j >= 7 && j <= 22)
                                    totaliShitjeve += Convert.ToDouble(vlerQelize);
                            }
                            oXLSheet.Cells[(i + 13), (j + 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                        oXLSheet.Cells[(i + 13), (7)].Value = totaliShitjeve;
                    }

                    oXLSheet.Cells["C13:C" + lastDataCell].Style.Numberformat.Format = "@";

                    oXLSheet.Cells["G13:G" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["H13:H" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["I13:I" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["J13:J" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["K13:K" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["L13:L" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["M13:M" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["N13:N" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["O13:O" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["P13:P" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["Q13:Q" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["R13:R" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["S13:S" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["T13:T" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["U13:U" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["V13:V" + lastDataCell + 1].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["W13:W" + lastDataCell + 1].Style.Numberformat.Format = "0";

                    //'vendosim totalet
                    oXLSheet.Cells["G" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["H" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["I" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["J" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["K" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["L" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["M" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["N" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["O" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["P" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["Q" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["R" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["S" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["T" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["U" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["V" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["W" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                }

                // 'krijojme footer-in

                oXLSheet.Cells["A" + (lastDataCell + 1)].Value = "Shuma totale";
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":F" + (lastDataCell + 1)].Merge = true;
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":F" + (lastDataCell + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                oXLSheet.Cells["A" + (lastDataCell + 1) + ":W" + (lastDataCell + 1)].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":W" + (lastDataCell + 1)].Style.Font.Bold = true;

                oXLSheet.Cells["A" + (lastDataCell + 2)].Value = "Kutia sipas Formularit te Deklarimit dhe Pageses se TVSH-se";
                oXLSheet.Cells["A" + (lastDataCell + 2) + ":G" + (lastDataCell + 2)].Merge = true;

                oXLSheet.Cells["A" + (lastDataCell + 2) + ":W" + (lastDataCell + 2)].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                oXLSheet.Cells["A" + (lastDataCell + 2) + ":W" + (lastDataCell + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //'vendosim emrat e fushave
                oXLSheet.Cells["H" + (lastDataCell + 2)].Value = "kutia (9)";
                oXLSheet.Cells["I" + (lastDataCell + 2)].Value = "kutia (10)";
                oXLSheet.Cells["J" + (lastDataCell + 2)].Value = "kutia (11)";
                oXLSheet.Cells["K" + (lastDataCell + 2)].Value = "kutia (12)";
                oXLSheet.Cells["L" + (lastDataCell + 2)].Value = "kutia (13)";
                oXLSheet.Cells["M" + (lastDataCell + 2)].Value = "kutia (14)";
                oXLSheet.Cells["N" + (lastDataCell + 2)].Value = "kutia (15)";
                oXLSheet.Cells["O" + (lastDataCell + 2)].Value = "kutia (16)";
                oXLSheet.Cells["P" + (lastDataCell + 2)].Value = "kutia (17)";
                oXLSheet.Cells["Q" + (lastDataCell + 2)].Value = "kutia (18)";
                oXLSheet.Cells["R" + (lastDataCell + 2)].Value = "kutia (19)";
                oXLSheet.Cells["S" + (lastDataCell + 2)].Value = "kutia (20)";
                oXLSheet.Cells["T" + (lastDataCell + 2)].Value = "kutia (21)";
                oXLSheet.Cells["U" + (lastDataCell + 2)].Value = "kutia (22)";
                oXLSheet.Cells["V" + (lastDataCell + 2)].Value = "kutia (23)";
                oXLSheet.Cells["W" + (lastDataCell + 2)].Value = "kutia (24)";

                //'emri mbiemri
                oXLSheet.Cells["L" + (lastDataCell + 5)].Value = "Emri Mbiemri";
                oXLSheet.Cells["L" + (lastDataCell + 5)].Style.Font.Bold = true;

                //'shpjegimi
                oXLSheet.Cells["A" + (lastDataCell + 7)].Value = "Shpjegim:";
                oXLSheet.Cells["A" + (lastDataCell + 8)].Value = "Ne rastin kur ky liber do te printohet, per qellime te ruajtjes/ mbajtjes se dokumentacionit, cdo faqe e printuar duhet te kete ne krye permbajtjen / rreshtat nga 1 ne 12.";
                oXLSheet.Cells["A" + (lastDataCell + 9)].Value = "Nr i rreshtave mund te shtohet ne perputhje me nr e transaksioneve";

                excPac.SaveAs(newFile2);
                downloadFileToClient(pathtoWrite, WB, Response);
            }
        }

        private static void createExcelperLiberShitje(DataTable Tbl, string filePathToOpen, string pathtoWrite, DateTime dtfillimi, string nderEmer, string nderNipt, string viti, HttpRequest Request, HttpResponse Response)
        {
            int lastDataCell;
            FileInfo newFile = new FileInfo(filePathToOpen);
            FileInfo newFile2 = new FileInfo(pathtoWrite);

            using (ExcelPackage excPac = new ExcelPackage(newFile))
            {
                //Get the work book in the file
                ExcelWorkbook WB = excPac.Workbook;
                //Hap file e excel
                ExcelWorksheet oXLSheet = WB.Worksheets.ElementAt(0);

                oXLSheet.Cells["C2"].Value = nderEmer;
                oXLSheet.Cells["C3"].Value = nderNipt;
                oXLSheet.Cells["C4"].Value = viti;
                oXLSheet.Cells["C5"].Value = (dtfillimi.Month < 10) ? "0" + dtfillimi.Month : Convert.ToString(dtfillimi.Month);

                if (Tbl.Rows.Count == 0)
                {
                    lastDataCell = 22;
                    oXLSheet.Cells["B7"].Value = "PO";
                }
                else
                {
                    lastDataCell = 12 + Tbl.Rows.Count;
                    if (lastDataCell < 22) lastDataCell = 22;

                    for (int i = 0; i < Tbl.Rows.Count; i++)
                    {
                        for (int j = 0; j < Tbl.Columns.Count; j++)
                        {
                            oXLSheet.Cells[(i + 13), (j + 1)].Value = Tbl.Rows[i][j];

                            oXLSheet.Cells[(i + 13), (j + 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                        }
                    }

                    oXLSheet.Cells["C13:C" + lastDataCell].Style.Numberformat.Format = "dd/MM/yyyy";

                    //'vendosim totalet
                    oXLSheet.Cells["G" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["H" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["I" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["J" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["K" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["L" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["M" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                }

                // 'krijojme footer-in

                oXLSheet.Cells["A" + (lastDataCell + 1)].Value = "Shuma totale";
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":F" + (lastDataCell + 1)].Merge = true;
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":F" + (lastDataCell + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                oXLSheet.Cells["A" + (lastDataCell + 1) + ":M" + (lastDataCell + 1)].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":M" + (lastDataCell + 1)].Style.Font.Bold = true;

                oXLSheet.Cells["A" + (lastDataCell + 2)].Value = "Kutia sipas Formularit te Deklarimit dhe Pageses se TVSH-se";
                oXLSheet.Cells["A" + (lastDataCell + 2) + ":G" + (lastDataCell + 2)].Merge = true;

                oXLSheet.Cells["A" + (lastDataCell + 2) + ":M" + (lastDataCell + 2)].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                oXLSheet.Cells["A" + (lastDataCell + 2) + ":M" + (lastDataCell + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //'vendosim emrat e fushave
                oXLSheet.Cells["H" + (lastDataCell + 2)].Value = "kutia (9)";
                oXLSheet.Cells["I" + (lastDataCell + 2)].Value = "kutia (10)";
                oXLSheet.Cells["J" + (lastDataCell + 2)].Value = "kutia (11)";
                oXLSheet.Cells["K" + (lastDataCell + 2)].Value = "kutia (12)";
                oXLSheet.Cells["L" + (lastDataCell + 2)].Value = "kutia (13)";
                oXLSheet.Cells["M" + (lastDataCell + 2)].Value = "kutia (14)";

                //'emri mbiemri
                oXLSheet.Cells["J" + (lastDataCell + 5)].Value = "Emri Mbiemri";
                oXLSheet.Cells["J" + (lastDataCell + 5)].Style.Font.Bold = true;

                //'shpjegimi
                oXLSheet.Cells["A" + (lastDataCell + 7)].Value = "Shpjegim:";
                oXLSheet.Cells["A" + (lastDataCell + 8)].Value = "Ne rastin kur ky liber do te printohet, per qellime te ruajtjes/ mbajtjes se dokumentacionit, cdo faqe e printuar duhet te kete ne krye permbajtjen / rreshtat nga 1 ne 12.";
                oXLSheet.Cells["A" + (lastDataCell + 9)].Value = "Nr i rreshtave mund te shtohet ne perputhje me nr e transaksioneve";

                excPac.SaveAs(newFile2);
                downloadFileToClient(pathtoWrite, WB, Response);
            }
        }

        private static void createExcelperLiberBlerjeBanka(DataTable Tbl, string filePathToOpen, string pathtoWrite, DateTime dtfillimi, string nderEmer, string nderNipt, string viti, HttpRequest Request, HttpResponse Response)
        {
            int lastDataCell;
            FileInfo newFile = new FileInfo(filePathToOpen);
            FileInfo newFile2 = new FileInfo(pathtoWrite);

            using (ExcelPackage excPac = new ExcelPackage(newFile))
            {
                //Get the work book in the file
                ExcelWorkbook WB = excPac.Workbook;
                //Hap file e excel
                ExcelWorksheet oXLSheet = WB.Worksheets.ElementAt(0);

                oXLSheet.Cells["C2"].Value = nderEmer;
                oXLSheet.Cells["C3"].Value = nderNipt;
                oXLSheet.Cells["C4"].Value = viti;
                oXLSheet.Cells["C5"].Value = (dtfillimi.Month < 10) ? "0" + dtfillimi.Month : Convert.ToString(dtfillimi.Month);

                if (Tbl.Rows.Count == 0)
                {
                    lastDataCell = 22;
                    oXLSheet.Cells["B7"].Value = "PO";
                }
                else
                {
                    lastDataCell = 12 + Tbl.Rows.Count;
                    if (lastDataCell < 22) lastDataCell = 22;

                    for (int i = 0; i < Tbl.Rows.Count; i++)
                    {
                        for (int j = 0; j < Tbl.Columns.Count - 1; j++)
                        {
                            oXLSheet.Cells[(i + 13), (j + 1)].Value = Tbl.Rows[i][j];

                            oXLSheet.Cells[(i + 13), (j + 1)].Style.Border.BorderAround(ExcelBorderStyle.Thin);
                            oXLSheet.Cells[(i + 13), (j + 1)].Style.Numberformat.Format = "0";
                        }
                    }

                    oXLSheet.Cells["C13:C" + lastDataCell].Style.Numberformat.Format = "dd/MM/yyyy";

                    //'vendosim totalet
                    oXLSheet.Cells["G" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["H" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["I" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["J" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["K" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["L" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["M" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["N" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["O" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["P" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["Q" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";
                    oXLSheet.Cells["R" + (lastDataCell + 1)].FormulaR1C1 = "=SUM(R[-" + (lastDataCell - 12) + "]C:R[-1]C)";

                    oXLSheet.Cells["G" + (lastDataCell + 1)].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["H" + (lastDataCell + 1)].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["I" + (lastDataCell + 1)].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["J" + (lastDataCell + 1)].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["K" + (lastDataCell + 1)].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["L" + (lastDataCell + 1)].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["M" + (lastDataCell + 1)].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["N" + (lastDataCell + 1)].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["O" + (lastDataCell + 1)].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["P" + (lastDataCell + 1)].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["Q" + (lastDataCell + 1)].Style.Numberformat.Format = "0";
                    oXLSheet.Cells["R" + (lastDataCell + 1)].Style.Numberformat.Format = "0";

                }

                // 'krijojme footer-in

                oXLSheet.Cells["A" + (lastDataCell + 1)].Value = "Shuma totale";
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":F" + (lastDataCell + 1)].Merge = true;
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":F" + (lastDataCell + 1)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                oXLSheet.Cells["A" + (lastDataCell + 1) + ":R" + (lastDataCell + 1)].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                oXLSheet.Cells["A" + (lastDataCell + 1) + ":R" + (lastDataCell + 1)].Style.Font.Bold = true;

                oXLSheet.Cells["A" + (lastDataCell + 2)].Value = "Kutia sipas Formularit te Deklarimit dhe Pageses se TVSH-se";
                oXLSheet.Cells["A" + (lastDataCell + 2) + ":G" + (lastDataCell + 2)].Merge = true;

                oXLSheet.Cells["A" + (lastDataCell + 2) + ":R" + (lastDataCell + 2)].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                oXLSheet.Cells["A" + (lastDataCell + 2) + ":R" + (lastDataCell + 2)].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                //'vendosim emrat e fushave
                oXLSheet.Cells["H" + (lastDataCell + 2)].Value = "kutia (15)";
                oXLSheet.Cells["I" + (lastDataCell + 2)].Value = "kutia (16)";
                oXLSheet.Cells["J" + (lastDataCell + 2)].Value = "kutia (17)";
                oXLSheet.Cells["K" + (lastDataCell + 2)].Value = "kutia (18)";
                oXLSheet.Cells["L" + (lastDataCell + 2)].Value = "kutia (19)";
                oXLSheet.Cells["M" + (lastDataCell + 2)].Value = "kutia (20)";
                oXLSheet.Cells["N" + (lastDataCell + 2)].Value = "kutia (21)";
                oXLSheet.Cells["O" + (lastDataCell + 2)].Value = "kutia (22)";
                oXLSheet.Cells["P" + (lastDataCell + 2)].Value = "kutia (23)";
                oXLSheet.Cells["Q" + (lastDataCell + 2)].Value = "kutia (24)";
                oXLSheet.Cells["R" + (lastDataCell + 2)].Value = "kutia (25)";

                //'emri mbiemri
                oXLSheet.Cells["Q" + (lastDataCell + 5)].Value = "Emri Mbiemri";
                oXLSheet.Cells["Q" + (lastDataCell + 5)].Style.Font.Bold = true;

                //'shpjegimi
                oXLSheet.Cells["A" + (lastDataCell + 7)].Value = "Shpjegim:";
                oXLSheet.Cells["A" + (lastDataCell + 8)].Value = "Ne rastin kur ky liber do te printohet, per qellime te ruajtjes/ mbajtjes se dokumentacionit, cdo faqe e printuar duhet te kete ne krye permbajtjen / rreshtat nga 1 ne 12.";
                oXLSheet.Cells["A" + (lastDataCell + 9)].Value = "Nr i rreshtave mund te shtohet ne perputhje me nr e transaksioneve";

                excPac.SaveAs(newFile2);
                downloadFileToClient(pathtoWrite, WB, Response);
            }
        }


        public static void ShtoPerkthimNeHfState(IDictionary<string, object> hfState, params string[] perkthimet)
        {
            foreach (var perkthim in perkthimet)
            {
                hfState[perkthim] = MessagesResource.Messages[perkthim];
            }
        }
        /// <summary>
        /// kthen limitin e madhesise se skedarit qe mund te importohet
        /// </summary>
        /// <returns></returns>
        public static long merrMaxFileSizePerImport()
        {
            String stringMaxFileSize = WebConfigurationManager.AppSettings["MaxFileSize"];
            if (stringMaxFileSize.Equals(String.Empty))
                stringMaxFileSize = "10000000";

            return Convert.ToInt64(stringMaxFileSize);
        }

        /// <summary>
        /// Metode qe kontrollon nje string per karakteret speciale !@#$%^&*()-/,.?{}=+\
        /// </summary>
        /// <param name="stringPerKontroll">String qe do te kontrollohet</param>
        /// <returns>Kthen nje objekt mesazh</returns>
        public static clsMesazh kontrolloPerKaraktereSpeciale(string stringPerKontroll)
        {
            char[] SpecialChars = "!@#$%^&*()-/,.?{}=+\\".ToCharArray();
            string specialChars = new string(SpecialChars);
            int indexOf = stringPerKontroll.IndexOfAny(SpecialChars);
            if (indexOf != -1)
            {
                return new clsMesazh(false, " nuk mund te permbaje: " + specialChars + " keto karaktere speciale!");
            }
            else
            {
                return new clsMesazh(true, "String-u nuk permban karaktere speciale");
            }
        }
        public static clsMesazh kontrolloPerKaraktereSpecialeEng(string stringPerKontroll)
        {
            char[] SpecialChars = "!@#$%^&*()-/,.?{}=+\\".ToCharArray();
            string specialChars = new string(SpecialChars);
            int indexOf = stringPerKontroll.IndexOfAny(SpecialChars);
            if (indexOf != -1)
            {
                return new clsMesazh(false, " must not contain these: " + specialChars + " special characters!");
            }
            else
            {
                return new clsMesazh(true, "The string does not contain special characters");
            }
        }

        public static void vendosVlereDefaultTeDataTable(clsTrupiFormatImporti trupi, DataTable dt)
        {
            if (trupi.Visible && trupi.Shfaq && trupi.VleraDefault != "")
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[trupi.EmerImporti].ToString() == "")
                        dr[trupi.EmerImporti] = trupi.VleraDefault;
                }
            }
        }


        public static clsMesazh importDokumenteshNgaWS(clsKonfigImporti konfigImp, int idNdermarrje, int idPerdorues, ref DataTable gabime, int idNderViti)
        {
            ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            int idGjuha = clsPerdorues.ktheGjuhePerdoruesi(idPerdorues);
            CultureInfo ci = MessagesResource.KtheCultureInfo(idGjuha);
            if (!clsKokaFormatImporti.ekzistonFormati(konfigImp.Formati))
            {
                return new clsMesazh(false, rm.GetString("msgImportKyFormatNukEkziston", ci));
            }

            clsMesazh mesazh = new clsMesazh(true);
            clsKokaFormatImporti format = new clsKokaFormatImporti(konfigImp.Formati);
            colTrupiFormatImporti col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(format.IdKoka);
            string nenkategoria = col.ktheEmerImportiSipasKodKontrolli("Nenkategoria");
            string ndermarrjeKey = col.ktheEmerImportiSipasKodKontrolli("Kod Ndermarrje");
            string primaryKey = col.filtroFormatImportiPerPrimaryKey().EmerImporti;

            DataTable dt = new DataTable();
            clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);


            if (!string.IsNullOrWhiteSpace(primaryKey))
                colImportSQL.FshiDokumentatTeDuplikuar(konfigImp.EmerTabKoka, primaryKey);


            switch (konfigImp.Kategoria.ToString())
            {
                case "1":
                case "2":
                    dt = colKokaShitje.merrShitjePerImport(konfigImp.EmerTabKoka, konfigImp.EmerTabTrupi, ndermarrjeKey, nderm.NdermarrjeKodi, nenkategoria, konfigImp.Kategoria, primaryKey, konfigImp.MerrTePaImportuara, konfigImp.RimerrTeImportuara, null);
                    break;
                case "3":
                case "4":
                    dt = colVeprimBankaKoka.merrArkaBankaPerImport(konfigImp.EmerTabKoka, konfigImp.EmerTabTrupi, ndermarrjeKey, nderm.NdermarrjeKodi, nenkategoria, konfigImp.Kategoria, primaryKey, konfigImp.MerrTePaImportuara, konfigImp.RimerrTeImportuara, null);
                    break;
                case "6":
                    dt = colKokaMagazina.merrDokMagazinePerImport(konfigImp.EmerTabKoka, konfigImp.EmerTabTrupi, ndermarrjeKey, nderm.NdermarrjeKodi, nenkategoria, konfigImp.Kategoria, primaryKey, konfigImp.MerrTePaImportuara, konfigImp.RimerrTeImportuara, null);
                    break;
                case "21":
                    dt = clsPerdorues.merrPerdoruesPerImport(konfigImp.EmerTabKoka, ndermarrjeKey, nderm.NdermarrjeKodi, konfigImp.MerrTePaImportuara, konfigImp.RimerrTeImportuara);
                    break;
                case "45":
                    string primaryKeyProdukt = col.filtroFormatImportiPerPrimaryKeyProduktProdhimi().EmerImporti;
                    dt = colKokaEkzekutim.merrEkzekutimePerImport(konfigImp.EmerTabKoka, konfigImp.EmerTabTrupi, konfigImp.EmerTabRec, ndermarrjeKey, nderm.NdermarrjeKodi, primaryKey, primaryKeyProdukt, konfigImp.MerrTePaImportuara, konfigImp.RimerrTeImportuara, null);
                    break;
                case "12":
                case "13":
                case "14":
                case "67":
                    dt = colImportSQL.merrObjektePerImportSQL(konfigImp.EmerTabKoka, ndermarrjeKey, nderm.NdermarrjeKodi, konfigImp.MerrTePaImportuara, konfigImp.RimerrTeImportuara, null);
                    break;

                case "135":
                case "136":
                    dt = colKokaEkzekutim.merrEkzekutimePerImport(konfigImp.EmerTabKoka, konfigImp.EmerTabTrupi, ndermarrjeKey, nderm.NdermarrjeKodi, primaryKey, konfigImp.MerrTePaImportuara, konfigImp.RimerrTeImportuara, null);
                    break;
                case "177":
                    dt = ColBKokaBuxheti.merrDokumentaPerImport(konfigImp.EmerTabKoka, konfigImp.EmerTabTrupi, ndermarrjeKey, nderm.NdermarrjeKodi, primaryKey, konfigImp.MerrTePaImportuara, konfigImp.RimerrTeImportuara, null);
                    break;
            }

            if (dt.Rows.Count == 0)
            {
                return new clsMesazh(true, rm.GetString("msgSkaRreshtaNeImportim", ci));
            }

            if (konfigImp.Kategoria.ToString() == "45")
                dt.PrimaryKey = new DataColumn[] { dt.Columns["IDIMPORTRECEPTURA"] };
            else
                dt.PrimaryKey = new DataColumn[] { dt.Columns["IDIMPORTTRUPISHITJE"] };
            DataTable tePaImportuara = new DataTable();
            DataTable deadLocked = new DataTable();
            tePaImportuara = dt.Clone();
            int pozicionKodi = -1;
            switch (konfigImp.Kategoria.ToString())
            {
                case "1":
                case "2":
                    mesazh = importDokumenteshShitjeBlerje(dt, idNdermarrje, idNderViti, idPerdorues, ref gabime, ref tePaImportuara, nenkategoria, ndermarrjeKey, primaryKey, col, konfigImp.Kategoria, konfigImp.GjeneroFaturePermbledhese, rm, ci, idGjuha, konfigImp.EmerTabKoka, konfigImp.EmerTabTrupi, nderm, true, true, true, ref deadLocked);
                    break;
                case "3":
                case "4":
                    mesazh = importDokumenteshArkaBanka(dt, idNdermarrje, idPerdorues, ref gabime, ref tePaImportuara, nenkategoria, ndermarrjeKey, primaryKey, col, konfigImp.Kategoria, rm, ci, idGjuha, konfigImp.EmerTabKoka, konfigImp.EmerTabTrupi, nderm, true, true, true, idNderViti);
                    break;
                case "6":
                    mesazh = importDokumenteMagazine(dt, idNdermarrje, idPerdorues, ref gabime, ref tePaImportuara, nenkategoria, ndermarrjeKey, primaryKey, col, konfigImp.Kategoria, rm, ci, idGjuha, konfigImp.EmerTabKoka, konfigImp.EmerTabTrupi, nderm, true, true, true, idNderViti, ref deadLocked);
                    break;
                case "21":
                    pozicionKodi = gjejVendodhjenEKodit(konfigImp.Formati, "Perdoruesi");
                    mesazh = importPerdoruesishAutomatik(konfigImp, dt, gabime, ref tePaImportuara, true, col, true, pozicionKodi, idNdermarrje, idPerdorues, idNderViti, konfigImp.EmerTabKoka);
                    break;
                case "45":
                    string primaryKeyProdukt = col.filtroFormatImportiPerPrimaryKeyProduktProdhimi().EmerImporti;
                    mesazh = importDokumenteshEkzekutimProdhimi(dt, idNdermarrje, idPerdorues, ref gabime, ref tePaImportuara, primaryKey, ndermarrjeKey, primaryKeyProdukt, col, konfigImp.Kategoria, rm, ci, idGjuha, konfigImp.EmerTabKoka, konfigImp.EmerTabTrupi, konfigImp.EmerTabRec, true, true, true, idNderViti);
                    break;
                case "12":
                    pozicionKodi = gjejVendodhjenEKodit(konfigImp.Formati, "Kodi");
                    mesazh = clsImportoKlientFurnitore.importoKlientFurnitor(dt, ref gabime, ref tePaImportuara, true, pozicionKodi, rm, ci, idPerdorues, idNdermarrje, idNderViti, idGjuha, col, true, konfigImp.EmerTabKoka);
                    break;
                case "13":
                    pozicionKodi = gjejVendodhjenEKodit(konfigImp.Formati, "Kodi");
                    mesazh = clsImportoArtikuj.importoArtikuj(dt, ref gabime, ref tePaImportuara, true, pozicionKodi, rm, ci, idPerdorues, idNdermarrje, idNderViti, idGjuha, col, true, konfigImp.EmerTabKoka);
                    break;
                case "14":
                    pozicionKodi = gjejVendodhjenEKodit(konfigImp.Formati, "Numer");
                    mesazh = importoLlogari(dt, ref gabime, ref tePaImportuara, true, pozicionKodi, rm, ci, idPerdorues, idNdermarrje, idNderViti, idGjuha, col, true, konfigImp.EmerTabKoka);
                    break;
                case "67":
                    pozicionKodi = gjejVendodhjenEKodit(konfigImp.Formati, "Kodi");
                    mesazh = importoGrupeArtikujsh(dt, ref gabime, ref tePaImportuara, true, pozicionKodi, rm, ci, idPerdorues, idNdermarrje, idNderViti, idGjuha, col, true, konfigImp.EmerTabKoka);
                    break;
                case "135":
                    pozicionKodi = gjejVendodhjenEKodit(konfigImp.Formati, "Barkodi");
                    mesazh = importDokumenteshInventarizimi(dt, idNdermarrje, idPerdorues, ref gabime, ref tePaImportuara, nenkategoria, ndermarrjeKey, primaryKey, col, konfigImp.Kategoria, rm, ci, idGjuha, konfigImp.EmerTabKoka, konfigImp.EmerTabTrupi, idNderViti, pozicionKodi, true, true, true);
                    break;
                case "136":
                    pozicionKodi = gjejVendodhjenEKodit(konfigImp.Formati, "Seriali");
                    mesazh = importDokumenteshInventarizimi(dt, idNdermarrje, idPerdorues, ref gabime, ref tePaImportuara, nenkategoria, ndermarrjeKey, primaryKey, col, konfigImp.Kategoria, rm, ci, idGjuha, konfigImp.EmerTabKoka, konfigImp.EmerTabTrupi, idNderViti, pozicionKodi, true, true, true);
                    break;
                case "177":
                    mesazh = importoDokumentBuxheti(dt, idNdermarrje, idPerdorues, ref gabime, ref tePaImportuara, ndermarrjeKey, primaryKey, col, konfigImp.Kategoria, rm, ci, idGjuha, konfigImp.EmerTabKoka, konfigImp.EmerTabTrupi, true, true, true, idNderViti);
                    break;
            }
            if (tePaImportuara.Rows.Count > 0)
            {
                IEnumerable<int> idTePaImportuara = tePaImportuara.AsEnumerable().Select(val => (int)val["IDIMPORTSHITJE"]);
                IEnumerable<int> idDeadLocked = deadLocked.AsEnumerable().Select(val => (int)val["IDIMPORTSHITJE"]);
                IEnumerable<int> perStatus3 = idTePaImportuara.Except(idDeadLocked);

                clsDatabazeImporte dbImport = new clsDatabazeImporte();
                string idPerUpdate = string.Join("','", tePaImportuara.AsEnumerable().Select(x => x["IDIMPORTSHITJE"].ToString()).Distinct().ToList());
                ImbLogger.LogInfoImporti("ID e dokumentave me gabime qe nuk u importuan nga tabela " + konfigImp.EmerTabKoka + " : " + idPerUpdate.Replace("'", "") + " .");
                colImportSQL.updateDokTabeleTemportal(idPerUpdate, idNdermarrje, 3, konfigImp.EmerTabKoka, "IDIMPORTSHITJE", ndermarrjeKey, dbImport);
            }
            return mesazh;
        }

        public static clsMesazh importDokumenteshShitjeBlerje(DataTable teDhenatPerImport, int idNdermarrje, int idNdermVit, int idPerdorues, ref DataTable gabime, ref DataTable tePaImportuara, string nenkategoria, string ndermarrjeKey, string primaryKey, colTrupiFormatImporti col, int idKategori, bool gjeneroFaturePermbledhese, ResourceManager rm, CultureInfo ci, int idGjuha, string emerTabKoka, string emerTabTrupi, clsNdermarrje nderm, bool importo, bool vjenNgaImportSQL, bool importAutomatik, ref DataTable deadLocked)
        {
            var fushatEGrupimit = "";
            DataTable dataGrupime;
            var nrDokumentiEmerImport = "";
            var dtDokumentiEmerImport = "";
            var llojDokumentiEmerImport = "";
            var fushaSerialesh = new Dictionary<string, string>();
            var mesazh = new clsMesazh(true);

            if (vjenNgaImportSQL)
            {
                foreach (var trupi in col)
                {
                    vendosVlereDefaultTeDataTable(trupi, teDhenatPerImport);
                    switch (trupi.KodKontrolli)
                    {
                        case "Nr Dokumenti":
                            nrDokumentiEmerImport = trupi.EmerImporti;
                            break;
                        case "Date Dokumenti":
                            dtDokumentiEmerImport = trupi.EmerImporti;
                            break;
                        case "Lloj Dokumenti":
                            llojDokumentiEmerImport = trupi.EmerImporti;
                            break;
                        case "Kodi":
                        case "Seriali Unik Kryesor":
                        case "Seriali Unik Dytesor":
                        case "Artikulli Set":
                        case "Magazina":
                            if (trupi.Visible)
                                fushaSerialesh[trupi.KodKontrolli] = trupi.EmerImporti;
                            break;
                    }
                }
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, primaryKey);
            }
            else
            {
                //string fushaGrupimi = "";
                var fushat = new List<string>();
                foreach (var trupi in col)
                {
                    vendosVlereDefaultTeDataTable(trupi, teDhenatPerImport);
                    switch (trupi.KodKontrolli)
                    {
                        case "Nr Dokumenti":
                            nrDokumentiEmerImport = trupi.EmerImporti;
                            break;
                        case "Date Dokumenti":
                            dtDokumentiEmerImport = trupi.EmerImporti;
                            break;
                        case "Lloj Dokumenti":
                            llojDokumentiEmerImport = trupi.EmerImporti;
                            break;
                        case "Kodi":
                        case "Seriali Unik Kryesor":
                        case "Seriali Unik Dytesor":
                        case "Artikulli Set":
                        case "Magazina":
                            if (trupi.Visible)
                                fushaSerialesh[trupi.KodKontrolli] = trupi.EmerImporti;
                            break;
                    }
                    if (trupi.Visible && trupi.Shfaq && trupi.FusheKokeApoTrupi == 1)
                        fushat.Add(trupi.EmerImporti);
                }

                fushatEGrupimit = string.Join(";", fushat);
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, fushat.ToArray()); //toTell kevi
            }

            var colBlerjeShitje = new colKokaShitje();

            var gabimePermbledhese = false;
            var indexRreshtImporti = 2;
            using (var dbData = new DbData())
            {
                var dbA = new clsDatabaseAdmin(dbData);
                var monedheNdermarrje = new clsMonedha();
                monedheNdermarrje.mbushMonedhenENdermarrjes(idNdermarrje, dbA);
                colSerialeUnikeKategori kategori = new colSerialeUnikeKategori(idNdermarrje);
                foreach (DataRow drDok in dataGrupime.Rows)
                {
                    var selekti = "";
                    Debug.WriteLine($"READED CACHE: {dbData.TransCache.getFromCacheTotal()}");
                    try
                    {
                        DataTable dokumentKokTrup;
                        if (vjenNgaImportSQL)
                        {
                            selekti = $"[{primaryKey}] = '{drDok[primaryKey]}'";
                            dokumentKokTrup = teDhenatPerImport.Select(selekti).CopyToDataTable();
                            mesazh = krijoDokBlerjeShitje(dokumentKokTrup, rm, ci, idNdermarrje, idNdermVit, idPerdorues, col, ref gabime, importo, idGjuha, true, colBlerjeShitje, ref gabimePermbledhese, primaryKey, ndermarrjeKey, gjeneroFaturePermbledhese, idKategori, emerTabKoka, ref indexRreshtImporti, nrDokumentiEmerImport, dtDokumentiEmerImport, llojDokumentiEmerImport, ref tePaImportuara, importAutomatik, new string[0], false, monedheNdermarrje, dbData, ref deadLocked, fushaSerialesh, kategori);

                        }
                        else
                        {
                            var fushat = fushatEGrupimit.Split(';');
                            for (var j = 0; j < fushat.Length; j++)
                            {
                                if (!string.IsNullOrEmpty(drDok[fushat[j]].ToString()))
                                    selekti += "[" + fushat[j] + "] = '" + drDok[fushat[j]].ToString().Replace("'", "''") + "' AND "; //toTell kevi
                            }
                            selekti += "1 = 1";

                            dokumentKokTrup = teDhenatPerImport.Select(selekti).CopyToDataTable();
                            mesazh = krijoDokBlerjeShitje(dokumentKokTrup, rm, ci, idNdermarrje, idNdermVit, idPerdorues, col, ref gabime, importo, idGjuha, false, colBlerjeShitje, ref gabimePermbledhese, "", "", gjeneroFaturePermbledhese, idKategori, "", ref indexRreshtImporti, nrDokumentiEmerImport, dtDokumentiEmerImport, llojDokumentiEmerImport, ref tePaImportuara, importAutomatik, fushat, false, monedheNdermarrje, dbData, ref deadLocked, fushaSerialesh, kategori); //primary key duhet vetem per importin nga sql
                        }
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }

                if (importo)
                {
                    if (gjeneroFaturePermbledhese)
                    {
                        if (!gabimePermbledhese) //todo kevi - ketu vjen nga importi
                        {
                            mesazh = ruajFaturePermbledhese(teDhenatPerImport, ref gabime, colBlerjeShitje, idGjuha, ci, rm, col, primaryKey, ndermarrjeKey, ref indexRreshtImporti, nderm.Prind, nderm.OwnShop, vjenNgaImportSQL, idNdermarrje, emerTabKoka, ref tePaImportuara, importAutomatik, nrDokumentiEmerImport, dtDokumentiEmerImport, llojDokumentiEmerImport, idKategori, dbData);
                        }
                        else if (!importAutomatik)
                        {
                            foreach (DataRow dr in teDhenatPerImport.Rows)
                            {
                                dr[dtDokumentiEmerImport] = DateTime.Parse(dr[dtDokumentiEmerImport].ToString()).ToString("dd/MM/yyyy");
                            }
                            string id = "";
                            if (vjenNgaImportSQL)
                                id = "IDIMPORTTRUPISHITJE";
                            else id = "Id";
                            foreach (clsKokaShitje koka in colBlerjeShitje)
                            {
                                string kodKonfigurimi = clsKonfigurimAmbjenti.ktheKodKonfigurimi(koka.IdKonfigAmbjente);
                                DataTable gabimet = teDhenatPerImport.Select(string.Format("[{0}] = '{1}' AND [{2}] = '{3}' AND [{4}] = '{5}'", nrDokumentiEmerImport, koka.NrDok, dtDokumentiEmerImport, koka.DtDok.ToString("dd/MM/yyyy"), llojDokumentiEmerImport, kodKonfigurimi)).CopyToDataTable();
                                // shto tek te paimportuarat rreshtat qe gjenerojne fature permbledhese
                                foreach (DataRow dr in gabimet.Rows)
                                {
                                    if (tePaImportuara.Select(string.Format("{0} = '{1}'", id, dr[id])).Count() == 0)
                                        tePaImportuara.ImportRow(dr);
                                }
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                    }
                }
                if (!mesazh.Status)
                    return new clsMesazh(false, rm.GetString("msgGabimNeImportim", ci));
                return new clsMesazh(true, rm.GetString("msgImportimMeSukses", ci));
            }
        }
        public static clsMesazh importPerdoruesishAutomatik(clsKonfigImporti konfigImp, DataTable dt, DataTable gabime, ref DataTable tePaImportuara, bool importo, colTrupiFormatImporti col, bool isSqlType, int pozicionKodi, int idNdermarrje, int idPerdorues, int idNderViti, string emerTabKoka)
        {
            try
            {
                //duke qene se do therritet nga webservisi i kalojm disa atribute manualisht
                ImporteUtils import = new ImporteUtils();
                import.IdNdermarrja = idNdermarrje;
                import.IdPerdoruesi = idPerdorues;
                import.IdNdermarrjeVit = idNderViti;
                import.TabKoka = emerTabKoka;
                import.KonfigImporti = konfigImp;
                import.kontrolloPerdorues(dt, gabime, ref tePaImportuara, importo, pozicionKodi, col, isSqlType);
                return new clsMesazh(true);
            }
            catch (Exception ex)
            {
                return new clsMesazh(ex.Message);
            }

        }
        public static clsMesazh importDokumenteshInventarizimi(DataTable teDhenatPerImport, int idNdermarrje, int idPerdorues, ref DataTable gabime, ref DataTable tePaImportuara, string nenkategoria, string ndermarrjeKey, string primaryKey, colTrupiFormatImporti col, int idkategoria, ResourceManager rm, CultureInfo ci, int idGjuha, string emerTabKoka, string emerTabTrupi, int idNdermVit, int pozicionkodi, bool importo, bool vjenNgaImportSQL, bool importAutomatik)
        {
            string fushatEGrupimit = "";
            DataTable dataGrupime;
            string nrDokumentiEmerImport = "";
            string dtDokumentiEmerImport = "";
            string llojDokumentiEmerImport = "";
            clsMesazh mesazh = new clsMesazh(true);

            if (vjenNgaImportSQL)
            {
                foreach (clsTrupiFormatImporti trupi in col)
                {
                    vendosVlereDefaultTeDataTable(trupi, teDhenatPerImport);
                    if (trupi.KodKontrolli == "Nr Dokumenti")
                        nrDokumentiEmerImport = trupi.EmerImporti;
                    if (trupi.KodKontrolli == "Date Dokumenti")
                        dtDokumentiEmerImport = trupi.EmerImporti;
                    if (trupi.KodKontrolli == "Lloj Dokumenti")
                        llojDokumentiEmerImport = trupi.EmerImporti;
                }
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, primaryKey);
            }
            else
            {
                List<string> fushat = new List<string>();
                foreach (clsTrupiFormatImporti trupi in col)
                {
                    vendosVlereDefaultTeDataTable(trupi, teDhenatPerImport);
                    if (trupi.KodKontrolli == "Nr Dokumenti")
                        nrDokumentiEmerImport = trupi.EmerImporti;
                    if (trupi.KodKontrolli == "Date Dokumenti")
                        dtDokumentiEmerImport = trupi.EmerImporti;
                    if (trupi.KodKontrolli == "Lloj Dokumenti")
                        llojDokumentiEmerImport = trupi.EmerImporti;
                    if (trupi.Visible && trupi.Shfaq && trupi.FusheKokeApoTrupi == 1)
                        fushat.Add(trupi.EmerImporti);
                }
                fushatEGrupimit = string.Join(";", fushat);
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, fushat.ToArray());
            }

            if (vjenNgaImportSQL)
            {
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, primaryKey);
            }
            else
            {
                //grupojme dokumentet qe vijne si datatable sipas Nr te dokumentit, dates se dokumentit dhe llojit te dokumentit dhe i ruajme ato tek tabela dataGrupime
                string fushaGrupimi = "";
                foreach (clsTrupiFormatImporti trupi in col)
                {
                    if (trupi.Visible && trupi.Shfaq && trupi.FusheKokeApoTrupi == 1)
                        fushaGrupimi += trupi.EmerImporti + ";";
                }
                fushatEGrupimit = fushaGrupimi.Substring(0, fushaGrupimi.LastIndexOf(';'));//heqim pikepresjen e fundit
                string[] fushat = fushatEGrupimit.Split(';');
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, fushat);
            }
            int indexRreshtImporti = 1;
            using (DbData dbData = new DbData())
            {
                foreach (DataRow drDok in dataGrupime.Rows)
                {
                    DataTable dokumentKokTrup = null;
                    if (vjenNgaImportSQL)
                    {
                        dokumentKokTrup = teDhenatPerImport.Select(string.Format("[{0}] = '{1}'", primaryKey, drDok[primaryKey])).GetDataTable(teDhenatPerImport);
                        mesazh = krijoDokInventarizimi(dokumentKokTrup, rm, ci, idNdermarrje, idPerdorues, col, pozicionkodi, gabime, tePaImportuara, importo, idNdermVit, idGjuha, true, primaryKey, ndermarrjeKey, ref indexRreshtImporti, false, emerTabKoka, new string[0], false, idkategoria, dbData);
                    }
                    else
                    {
                        //krijojme nje tabele te re, ku vendosim dokumentin 
                        string selekti = "";
                        //string[] fushat = fushatEGrupimit.Split(';');
                        string[] fushat = fushatEGrupimit.Split(';');
                        for (int j = 0; j < fushat.Count(); j++)
                        {
                            if (!string.IsNullOrEmpty(drDok[fushat[j]].ToString()))
                            {
                                string fusha = drDok[fushat[j]].ToString().Replace("'", "''");
                                selekti += "[" + fushat[j] + "] = '" + drDok[fushat[j]].ToString() + "' AND ";
                            }
                        }
                        selekti += "1 = 1";
                        dokumentKokTrup = teDhenatPerImport.Select(selekti).GetDataTable(teDhenatPerImport);
                        mesazh = krijoDokInventarizimi(dokumentKokTrup, rm, ci, idNdermarrje, idPerdorues, col, pozicionkodi, gabime, tePaImportuara, importo, idNdermVit, idGjuha, false, "", "", ref indexRreshtImporti, false, "", fushat, false, idkategoria, dbData);
                    }
                }
                if (!mesazh.Status)
                    return new clsMesazh(false, rm.GetString("msgGabimNeImportim", ci));
                return new clsMesazh(true, rm.GetString("msgImportimMeSukses", ci));
            }
        }

        public static clsMesazh krijoDokBlerjeShitje(DataTable dokTable, ResourceManager rm, CultureInfo ci, int idNdermarrje, int idNdermVit, int idPerdorues, colTrupiFormatImporti col, ref DataTable gabime, bool importo, int idGjuha, bool vjenNgaImportSQL, DbRegjistrim.colKokaShitje colKoka, ref bool gabimePermbledhese, string primaryKey, string ndermarrjeKey, bool gjeneroFaturePermb, int idKategoria, string emerTabKoka, ref int indexRreshtImporti, string nrDokumentiEmerImport, string dtDokumentiEmerImport, string llojDokumentiEmerImport, ref DataTable tePaImportuara, bool importAutomatik, string[] fushat, bool ruajrenditje, clsMonedha monedheNdermarrje, DbData dbData, ref DataTable deadLocked, Dictionary<string, string> fushaSerialesh, colSerialeUnikeKategori kategori)
        {
            //dokTable eshte nje tabele me rreshtat e kokes dhe te trupit te nje dokumenti te vetem. Pra, ne qofte se dokumenti ka nje rresht trupi, atehere edhe tabela do te kete nje rresht.
            //Ne qofte se dokumenti ka dy rreshta ne trup, atehere tabela do te kete dy rreshta dhe te dhenat e kokes se dokumentit do te perseriten tek te dy rreshtat.
            //Pra, tabela ka aq rreshta sa trupi i dokumentit. Ne qofte se tabela dokTable eshte null atehere dalim nga funksioni, sepse s'ka dokument per te krijuar.
            if (dokTable == null)
                return null;
            var error = "";
            try
            {
                var mesazh = new clsMesazh(true);
                var koka = new clsKokaShitje();
                var kokaMeme = new clsKokaShitje();
                string nenkategoria = "", llojDokumenti = "", nrDok = "", arka = "", klientFurnitor = "", nrProjekti = "", nrSerial = "", monedha = "", pershkrimi = "",
                    pikeShitjeFurnizimi = "", degeAdministrative = "", menyrePagese = "", adreseDergimi = "", adreseFaturimi = "", grupimdok1 = "", grupimdok2 = "",
                    grupimdok3 = "", agjentShitje1 = "", agjentShitje2 = "", agjentShitje3 = "", emerKlienti = "", kontakti = "", transportuesi = "", marresi = "",
                    automjeti = "", krijuesi = "", targashoferi = "", shoferi = "", koordinata = "", niptKlienti = "", shenime2 = "", llojzbritjetotale = "",
                    qyteti = "", idMarreveshje = "", llojMarreveshje = "", klientFurnitorVartes = "", karta = "", kodKlientIntegrimi = "", kerkuarNga = "", Iic = "", Nivf = "", EIC = "", TipiVetefaturimit = "", NivfKthim = "";

                double perqindjeAgjent1 = 0, perqindjeAgjent2 = 0, perqindjeAgjent3 = 0, zbritje = 0, kursi = 0, kilometra = 0;
                DateTime dtDok = new DateTime(), dtRegjistrimi = new DateTime(), dtMaturimi = new DateTime(), afatKohor = new DateTime(), dtFillimi = new DateTime(), dtMbarimi = new DateTime(), dtFature = new DateTime(), dtKrijimiPajisje = DateTime.Now, dtTransp = new DateTime();
                bool kupon = false, kase = false, shpenzimeJoTeZbritshme = false, dogana = false, kartaPaPagese = false;
                error = "";
                int idNivelGjeneruesi = 0, idKonfigGjeneruesi = 0, IdOperatori = 0, Procesi = 0, eInvoiceType = 0, idGjeneruesi = 0, idDokNga = 0, idDokTransferimNga = 0, idStatusDokImport = 0, idLlojMarveshje = 0;
                bool shitje_blerje = false;
                var kaGabim = false;

                DateTime dateKerkese = new DateTime();
                //marrim vlerat e kokes sipas formatit te importit.
                #region Fushat e kokes
                foreach (clsTrupiFormatImporti trup in col)
                {
                    error = "";
                    switch (trup.KodKontrolli)
                    {
                        case "Nenkategoria":
                            nenkategoria = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Lloj Dokumenti":
                            llojDokumenti = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Nr Dokumenti":
                            nrDok = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Klient/Furnitori":
                            klientFurnitor = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Klient/Furnitori vartes":
                            klientFurnitorVartes = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Numer Projekti":
                            nrProjekti = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Numer Serial":
                            nrSerial = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Monedha":
                            monedha = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Pershkrimi":
                            pershkrimi = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Pike Shitje/Furnizimi":
                            pikeShitjeFurnizimi = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Dege Administrative":
                            degeAdministrative = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Menyre Pagese":
                            menyrePagese = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Adresa e Dergimit":
                            adreseDergimi = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Adresa e Faturimit":
                            adreseFaturimi = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Grupim Dokumenti 1":
                            grupimdok1 = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Grupim Dokumenti 2":
                            grupimdok2 = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Grupim Dokumenti 3":
                            grupimdok3 = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Agjent Shitje 1":
                            agjentShitje1 = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Agjent Shitje 2":
                            agjentShitje2 = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Agjent Shitje 3":
                            agjentShitje3 = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Emer Klienti":
                            emerKlienti = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Kontakti":
                            kontakti = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Krijuesi":
                            krijuesi = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Transportues":
                            transportuesi = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Marresi":
                            marresi = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Automjeti":
                            automjeti = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Dogana":
                            dogana = vendosBool(trup, dokTable.Rows[0], out error);
                            break;

                        case "Perqindje Agjenti 1":
                            perqindjeAgjent1 = vendosDouble(trup, dokTable.Rows[0], out error);
                            break;

                        case "Perqindje Agjenti 2":
                            perqindjeAgjent2 = vendosDouble(trup, dokTable.Rows[0], out error);
                            break;

                        case "Perqindje Agjenti 3":
                            perqindjeAgjent3 = vendosDouble(trup, dokTable.Rows[0], out error);
                            break;

                        case "Total Zbritje":
                            zbritje = vendosDouble(trup, dokTable.Rows[0], out error);
                            break;

                        case "Kursi":
                            kursi = vendosDouble(trup, dokTable.Rows[0], out error);
                            break;

                        case "Kilometra":
                            kilometra = vendosDouble(trup, dokTable.Rows[0], out error);
                            break;

                        case "Date Dokumenti":
                            dtDok = vendosDate(trup, dokTable.Rows[0], out error, true);
                            if (dtDok == DateTime.MinValue)
                                dtDok = DateTime.Today;

                            break;

                        case "Date Regjistrimi":
                            dtRegjistrimi = vendosDate(trup, dokTable.Rows[0], out error);
                            if (dtRegjistrimi == DateTime.MinValue)
                                dtRegjistrimi = DateTime.Today;
                            break;

                        case "Date Maturimi":
                            dtMaturimi = vendosDate(trup, dokTable.Rows[0], out error);
                            if (dtMaturimi == DateTime.MinValue)
                                dtMaturimi = DateTime.Today;
                            break;

                        case "Dt Transporti":
                            dtTransp = vendosDate(trup, dokTable.Rows[0], out error);
                            if (dtTransp == DateTime.MinValue)
                                dtTransp = DateTime.Today;
                            break;
                        case "Afati Kohor":
                            afatKohor = vendosDate(trup, dokTable.Rows[0], out error);
                            if (afatKohor == DateTime.MinValue)
                                afatKohor = DateTime.Today;
                            break;

                        case "Date Fillimi":
                            dtFillimi = vendosDate(trup, dokTable.Rows[0], out error);
                            if (dtFillimi == DateTime.MinValue)
                                dtFillimi = DateTime.Today;
                            break;

                        case "Date Mbarimi":
                            dtMbarimi = vendosDate(trup, dokTable.Rows[0], out error);
                            if (dtMbarimi == DateTime.MinValue)
                                dtMbarimi = DateTime.Today;
                            break;

                        case "Kupon":
                            kupon = vendosBool(trup, dokTable.Rows[0], out error);
                            break;

                        case "Kase":
                            kase = vendosBool(trup, dokTable.Rows[0], out error);
                            break;

                        case "Shpenzime Jo Te Zbritshme":
                            shpenzimeJoTeZbritshme = vendosBool(trup, dokTable.Rows[0], out error);
                            break;

                        case "Id Nivel Gjenerues":
                            idNivelGjeneruesi = vendosInt(trup, dokTable.Rows[0], out error);
                            break;

                        case "Id Konfig Gjenerues":
                            idKonfigGjeneruesi = vendosInt(trup, dokTable.Rows[0], out error);
                            break;
                        case "Id Gjenerues":
                            idGjeneruesi = vendosInt(trup, dokTable.Rows[0], out error);
                            break;
                        case "Id Dok Nga":
                            idDokNga = vendosInt(trup, dokTable.Rows[0], out error);
                            break;
                        case "Dt Fature":
                            dtFature = vendosDate(trup, dokTable.Rows[0], out error);
                            if (dtFature == DateTime.MinValue)
                                dtFature = DateTime.Today;
                            break;
                        case "Targa":
                            //targa duhet vetem per eksportin, ne import nuk duhet te merret parasysh
                            break;
                        case "Targa e shoferit":
                            targashoferi = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Shoferi":
                            shoferi = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Karta":
                            karta = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Pershkrim Klient/Furnitori":
                            //Pershkrim Klient/Furnitori duhet vetem per eksportin, ne import nuk duhet te merret parasysh
                            break;
                        case "Pershkrim Gr. Dok. 1":
                            //Pershkrim Gr. Dok. 1 duhet vetem per eksportin, ne import nuk duhet te merret parasysh
                            break;
                        case "Pershkrim Gr. Dok. 2":
                            //Pershkrim Gr. Dok. 2 duhet vetem per eksportin, ne import nuk duhet te merret parasysh
                            break;
                        case "Pershkrim Gr. Dok. 3":
                            //Pershkrim Gr. Dok. 3 duhet vetem per eksportin, ne import nuk duhet te merret parasysh
                            break;
                        case "Dt Krijimi":
                            dtKrijimiPajisje = (vendosDate(trup, dokTable.Rows[0], out error) == new DateTime()) ? dtKrijimiPajisje : vendosDate(trup, dokTable.Rows[0], out error);
                            break;
                        case "Arka":
                            arka = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Koordinata":
                            koordinata = vendosVlere(trup, dokTable.Rows[0], out error);
                            if (!String.IsNullOrWhiteSpace(koordinata))
                                koordinata = formatoKoordinatePerRuajtje(koordinata);
                            break;
                        case "Shenime 2":
                            shenime2 = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Karta pa pagese":
                            kartaPaPagese = vendosBool(trup, dokTable.Rows[0], out error);
                            break;
                        case "ID Dok Transferim Nga":
                            idDokTransferimNga = vendosInt(trup, dokTable.Rows[0], out error);
                            break;
                        case "Id Status Dok":
                            idStatusDokImport = vendosInt(trup, dokTable.Rows[0], out error);
                            break;
                        case "Lloj Zbritje Totale":
                            llojzbritjetotale = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "QytetiK":
                            qyteti = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "NiptK":
                            niptKlienti = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Kod Klienti Integrimi":
                            kodKlientIntegrimi = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "KerkuarNga":
                            kerkuarNga = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "DateKerkese":
                            dateKerkese = vendosDate(trup, dokTable.Rows[0], out error, true);
                            if (dateKerkese == DateTime.MinValue)
                                dateKerkese = DateTime.Today;
                            break;
                        case "ID e marreveshjes":
                            idMarreveshje = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Lloji i marreveshjes":
                            llojMarreveshje = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "IIC":
                            Iic = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "NIVF":
                            Nivf = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Operatori":
                            if (clsOperator.MerrIdOperatoriSipasKodOperatori(vendosVlere(trup, dokTable.Rows[0], out error), idNdermarrje).ItemArray.Length == 0)
                                IdOperatori = 0;
                            else
                                IdOperatori = Convert.ToInt32(clsOperator.MerrIdOperatoriSipasKodOperatori(vendosVlere(trup, dokTable.Rows[0], out error), idNdermarrje).ItemArray[0].ToString());
                            break;
                        case "EIC":
                            EIC = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Procesi":
                            Procesi = Convert.ToInt32(new clsKokaShitje().ktheIdProcesi(vendosVlere(trup, dokTable.Rows[0], out error)).Rows[0].ItemArray[0].ToString());
                            break;
                        case "E-invoice Type":
                            eInvoiceType = Convert.ToInt32(new clsKokaShitje().ktheIdTipiEinvoice(vendosVlere(trup, dokTable.Rows[0], out error)).Rows[0].ItemArray[0].ToString());
                            break;
                        case "Tipi i vetefaturimit":
                            TipiVetefaturimit = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "NIVF kthim":
                            NivfKthim = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                    }
                    if (error != "")
                    {
                        kaGabim = true;
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                        {
                            if (error.Contains(trup.EmerImporti))
                            {
                                msgGabimi = "Date dokumenti" + " " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " :" + error;
                            }
                            else
                                msgGabimi = "Date dokumenti" + " " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " :" + trup.EmerImporti + " " + error;

                        }
                        else msgGabimi = error;

                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, idKategoria);
                        continue;
                    }
                }

                #endregion

                var dbS = new clsDatabaseShare(dbData);
                var dbK = new clsDatabaseKontabilitet(dbData);
                var dbR = new clsDatabaseRegjistrim(dbData);
                var dbA = new clsDatabaseAdmin(dbData);
                colSerialeUnikeMagazina serialeUnike = null;
                var konfigAmbjenti = new clsKonfigurimAmbjenti(llojDokumenti, idNdermarrje, dbS);
                var kaAutorizim = clsKonfigurimAmbjenti.getAutorizimKonfigurimi(konfigAmbjenti.IdKonfigAmbjente, idPerdorues, dbS);
                var ngarkoTeDhenaKlientVartes = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "NTDKV", dbS) == "Po";

                if (!kaAutorizim)
                    throw new Exception($"Perdoruesi nuk ka autorizim per llojin e dokumentit {konfigAmbjenti.KodKonfigAmbjente}.");

                (string nrAutom, Dictionary<string, object> hiddenFieldPerNrAuto) result = ktheNrAutoPerKonfigurim(konfigAmbjenti.IdKonfigAmbjente, nrDok, "txtNumer", "NrDok", 506, dtDok, dbS);
                string nrAutom = result.nrAutom;
                Dictionary<string, object> hiddenFieldPerNrAuto = result.hiddenFieldPerNrAuto;

                if (String.IsNullOrEmpty(klientFurnitor) && (!String.IsNullOrEmpty(kodKlientIntegrimi)))
                {
                    string kodi = clsKlientFurnitor.MerrKodKlientFurnitorSipasKodIntegrimi(kodKlientIntegrimi, idNdermarrje);
                    if (!string.IsNullOrEmpty(kodi))
                        klientFurnitor = kodi;
                }


                var gjeneronFaturePermbledhese = false;
                if (gjeneroFaturePermb)
                    gjeneronFaturePermbledhese = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "GJDSH", dbS) == "Po";

                if (kaGabim && gjeneronFaturePermbledhese)
                    gabimePermbledhese = true;
                var alternativKushtMMDT = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "MMDT");

                if (importo && alternativKushtMMDT == "Refuzo" && idStatusDokImport == 4 && !gjeneronFaturePermbledhese)
                    return RefuzoDokumentImporti(idDokTransferimNga, shenime2, vjenNgaImportSQL, gjeneronFaturePermbledhese, dbR, emerTabKoka, primaryKey, ndermarrjeKey, idNdermarrje, dbData, dokTable, dtDokumentiEmerImport, ref gabime, ref tePaImportuara, ref indexRreshtImporti, importo, nrDokumentiEmerImport, idKategoria, mesazh, fushat);

                var kokaurdhershitje = new clsKokaShitje();

                var nderm = new clsNdermarrje(idNdermarrje);
                var eshteOwn = nderm.OwnShop;

                if (nderm.Prind && dbR.ktheKokaShitjeEkzistonDoksipasID(idDokTransferimNga)) //import i nje dokumenti shitje, USH e te cilit ekziston te mema
                    kokaurdhershitje.mbushKokaShitjeSipasIDPaTrup(idDokTransferimNga);


                clsKlientFurnitor klient;
                if (clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.FORMAT_EKSPORTI_TRANSFER_USH_KPP) == konfigAmbjenti.KodKonfigAmbjente)
                {
                    klientFurnitorVartes = klientFurnitor;
                    int idKlienti;
                    int.TryParse(clsAtributeTrupi.merrVleredefaultSipasKontrollitDheKonfigurimit(konfigAmbjenti.IdKonfigAmbjente, "btnKlienti", 506), out idKlienti);
                    if (idKlienti == 0)
                        throw new MyException($"Konfigurimi {konfigAmbjenti.KodKonfigAmbjente} nuk ka nje klient te percaktuar!");
                    klient = new clsKlientFurnitor(idKlienti, dbK);
                    klientFurnitor = klient.KodKlientFurnitor;
                }
                else if (kokaurdhershitje.IdShitjeKoka > 0) //import i nje dokumenti shitje, USH e te cilit ekziston te mema )
                {

                    klient = new clsKlientFurnitor(kokaurdhershitje.IdKlientFurnitor, dbK);
                    klientFurnitor = klient.KodKlientFurnitor;
                }
                else
                {
                    klient = new clsKlientFurnitor(klientFurnitor, idNdermarrje, dbK);
                }
                if (string.IsNullOrEmpty(niptKlienti))
                {
                    niptKlienti = klient.NiptiKF;
                }
                if (string.IsNullOrEmpty(kontakti))
                {
                    kontakti = klient.TelKF;
                }
                if (string.IsNullOrEmpty(qyteti))
                {
                    qyteti = klient.EmriQytetitKF;
                }

                if (ngarkoTeDhenaKlientVartes && klientFurnitorVartes != "")
                {
                    var kodeKlienteFurnitoreVartes = klientFurnitorVartes.Split(',');
                    klient.mbushKlientFurnitorSipasKodit(kodeKlienteFurnitoreVartes[0].RemoveSpaces(), idNdermarrje);
                    adreseFaturimi = klient.OColAdresat != null ? klient.OColAdresat[0].Adresa : "";
                    niptKlienti = klient.NiptiKF;
                    kontakti = klient.CelKF;
                    qyteti = klient.EmriQytetitKF;
                    emerKlienti = klient.EmertimFature;
                }

                var alternativaKushtiCmimZero = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "CZ", dbS);

                var cmimZero = string.IsNullOrEmpty(alternativaKushtiCmimZero) || alternativaKushtiCmimZero != "Bllokues";

                bool isMagENjejte = true;

                if (idKategoria == 1 && konfigAmbjenti.IdKategori == 2)
                    throw new Exception("Nuk mund te importoni dokumente blerje, kur keni zgjedhur kategorine shitje!");
                if (idKategoria == 2 && konfigAmbjenti.IdKategori == 1)
                    throw new Exception("Nuk mund te importoni dokumente shitje, kur keni zgjedhur kategorine blerje!");

                var viti = new clsViti(idNdermarrje, dtDok.Year.ToString(), dbA);
                var per = new clsPeriudhaKontabel(dtDok, idNdermarrje, dbA);
                if (per.Ekycur)
                    throw new MyException("Periudha eshte e kycur!");

                shitje_blerje = idKategoria == 1;
                if (MbylljePeriudhe.PeriodClosing.IsPeriodClosed(dtDok, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, shitje_blerje ? KategoriDokumenti.Shitje : KategoriDokumenti.Blerje, konfigAmbjenti.IdKonfigAmbjente))
                {
                    throw new MyException(MessagesResource.Messages["msgPeriodIsClosed"]);
                }

                var ndermarrjeViti = new clsNdermarrjeViti(per.IdViti, idNdermarrje, dbA);
                var idNdermViti = ndermarrjeViti.IdNderViti;

                idPerdorues = kthePerdoruesPerImport(idNdermarrje, idPerdorues, krijuesi, dbA);

                if (dtDok.Year != clsNdermarrjeViti.ktheKodVitiSipasIdNdermViti(idNdermVit))
                    throw new MyException(rm.GetString("msgDataNukPerketVititUshtrimor", ci));

                var colSerialet = new colSerialetMagazine();
                var konfmag = new clsKonfigurimAmbjenti(konfigAmbjenti.IdKonfigurimi, dbS);

                var idStatusDok = 1;
                var meKontabilizim = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "GJK", dbS) != "Jo";
                var sdi = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "SDI", dbS) == "Draft";
                if (sdi)
                {
                    idStatusDok = 0;
                    meKontabilizim = false;
                }

                var klFurn = new clsKlientFurnitor();
                double krs = 1;
                int idMonedha = 0;
                string kodMonedha = "";

                if (klientFurnitor != "")
                {
                    string klLlog = "";
                    if (importo && alternativKushtMMDT == "ModifikoDok" && konfigAmbjenti.IdKategori == 2 && klientFurnitor.Length >= 4) // kodi i klientit eshte kodi i llogarise, nga kodi i klientit 411 nxjerrim ate te furnitorit 401
                    {
                        klLlog = "401006" + klientFurnitor.Substring(klientFurnitor.Length - 4); // 401006 (furnitor i Aparateve) + 4 shifrat e fundit te llogarise se klientit
                        klFurn = new clsKlientFurnitor(klLlog, idNdermarrje, dbK);
                        if (klFurn.IdKlientFurnitor > 0)
                            klientFurnitor = klLlog;
                    }
                    if (klFurn.IdKlientFurnitor == 0)
                        klFurn = new clsKlientFurnitor(klientFurnitor, idNdermarrje, dbK);
                    var llog = new clsLlogari(klFurn.IdLlogari, dbK);
                    idMonedha = llog.IdMonedha;
                    kodMonedha = new clsMonedha(llog.IdMonedha, dbA).KodiMonedha;
                }
                else if (monedha != string.Empty)
                {
                    var mon = new clsMonedha(monedha, idNdermarrje, dbA);
                    idMonedha = mon.IdMonedha;
                    kodMonedha = mon.KodiMonedha;
                }
                else
                {
                    idMonedha = monedheNdermarrje.IdMonedha;
                    kodMonedha = monedheNdermarrje.KodiMonedha;
                }

                if (idMonedha == monedheNdermarrje.IdMonedha)
                    krs = 1;
                else
                {
                    if (kursi == 0)
                    {
                        var alternativa = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "LLK", dbS);
                        var llojKursi = 1;
                        if (alternativa != "")
                            llojKursi = int.Parse(alternativa.Substring(alternativa.Length));
                        krs = clsKurset.merrKursinFunditSipasKodMonedheDateDheLloj(kodMonedha, dtDok.ToString(), idNdermarrje, llojKursi, dbA);
                    }
                    else
                        krs = kursi;
                    if (krs == 0)
                        krs = 1;
                }

                var sasiRezervimiAuto = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "SAR", dbS);

                var loan = 0;
                if (klient.LlojPorosie == 3)
                    loan = 1;//artikujt jane loan 

                //cmimi do te percaktohet sipas karteles se artikullit apo sipas vleres qe ka ne dokumentin qe po importohet
                bool percaktoCmimSipasKarteles = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "MCMKI", dbS).ToLower() == "po";
                bool ngarkoKodbar = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "NKAKNKA", dbS).ToLower() == "po";
                var indexRreshti = indexRreshtImporti;
                var colTrupi = krijoTrupShitjePerImport(col, dokTable, ref indexRreshti, dtDokumentiEmerImport, nrDokumentiEmerImport, ref gabime, out isMagENjejte, idNdermarrje, idPerdorues, cmimZero, ref gabimePermbledhese, importo, gjeneronFaturePermbledhese, ref tePaImportuara, vjenNgaImportSQL, importAutomatik, konfmag, krs, idStatusDok, ref colSerialet, shitje_blerje, idKategoria, sasiRezervimiAuto, dbData, idDokTransferimNga, loan, dtDok, klientFurnitor, percaktoCmimSipasKarteles, kodMonedha, ngarkoKodbar);
                var ndermarrje = new clsNdermarrje(idNdermarrje);



                if (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "MPLSHKKI", dbS) == "Po")
                {
                    if (!kupon)
                    {
                        if (klFurn.Kupon)
                            kupon = true;
                        else if (colTrupi.Sum(trup => trup.VleftaMeTvsh) > ndermarrje.LimitiShitjes)
                            kupon = true;
                    }
                }

                if ((!sdi || sdi &&
                     clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "ANRSVSR", dbS) == "Jo")
                    && clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "MNANSI", dbS) == "Po")
                {
                    if (kupon && nrSerial == "")
                    {
                        var idNrAutoNrSerial =
                            clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(konfigAmbjenti.IdKonfigAmbjente, "txtNumerSerial", 506);
                        if (idNrAutoNrSerial != 0)
                        {
                            var nrSerialShitje = clsNrAutom.merrVlerenNrAutomatik(idNrAutoNrSerial, dtDok);
                            if (!string.IsNullOrEmpty(nrSerialShitje))
                            {
                                var nrAutoSerial = new NrAuto
                                {
                                    kodKontrolli = "NrSerial",
                                    idNrAuto = idNrAutoNrSerial,
                                    vlereNrAuto = nrSerialShitje
                                };
                                hiddenFieldPerNrAuto.Add("NrSerial", JsonConvert.SerializeObject(nrAutoSerial));
                                nrSerial = nrSerialShitje;
                            }
                        }
                    }
                }
                bool gjeneroDokMag = false;
                var idMag = -1;
                var kodMag = "";
                if (isMagENjejte && colTrupi.Count > 0 && colTrupi[0].IdMagazina > 0 && !nderm.Prind && !dbR.ktheKokaShitjeEkzistonDoksipasID(idDokTransferimNga))
                {
                    idMag = colTrupi[0].IdMagazina;
                    kodMag = new clsNjesiAdministrative(idMag, dbR).Kodi;
                }

                var idPeriudheKontabel = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(dtDok, kokaurdhershitje.IdNdermarrje);
                var periudha = new clsPeriudhaKontabel(idPeriudheKontabel, ci);


                var colkonv = new colKonvertimi();

                var idRaporti = clsAtributeTrupi.merrVleredefaultSipasKontrollitKodKonfigDheNderm(llojDokumenti, idNdermarrje, "cmbFormatiPrintimit");
                var idRapDesign = 0;
                var kontrolloGjendje = !importo; //kur eshte 'kontrollo' importo = false dhe kontrolloGjendje duhet true
                if (!string.IsNullOrEmpty(idRaporti))
                    idRapDesign = Convert.ToInt32(idRaporti); //todo cache

                if (nderm.Prind && dbR.ktheKokaShitjeEkzistonDoksipasID(idDokTransferimNga)) //import i nje dokumenti shitje, USH e te cilit ekziston te mema
                {
                    var konf = new clsKonfigurimAmbjenti();
                    konf.mbushKonfigAmbjSipasKod("FSH", idNdermarrje);

                    var konfUSH = new clsKonfigurimAmbjenti(kokaurdhershitje.IdTemplate);

                    string kodAgjenit1USH = string.Empty, kodAgjenit2USH = string.Empty, kodAgjenit3USH = string.Empty, kodDegeAdminUSH = String.Empty, kodPikeShitjeFurnizimiUSH = String.Empty, kodGrupimdok1USH = String.Empty, emertimTransportuesUSH = String.Empty, nrShasieUSH = String.Empty, menyrePageseUSH = String.Empty;

                    if (kokaurdhershitje.IdAgjent != 0)
                    {
                        clsAgjentShitje agj1 = new clsAgjentShitje(kokaurdhershitje.IdAgjent);
                        kodAgjenit1USH = agj1.KodiAgjentShitje;
                    }

                    if (kokaurdhershitje.IdAgjenti2 != 0)
                    {
                        clsAgjentShitje agj2 = new clsAgjentShitje(kokaurdhershitje.IdAgjenti2);
                        kodAgjenit2USH = agj2.KodiAgjentShitje;
                    }

                    if (kokaurdhershitje.IdAgjenti3 != 0)
                    {
                        clsAgjentShitje agj3 = new clsAgjentShitje(kokaurdhershitje.IdAgjenti3);
                        kodAgjenit3USH = agj3.KodiAgjentShitje;
                    }

                    menyrePageseUSH = clsFunksione.ktheMenyrePageseSipasID(kokaurdhershitje.IdMenyrePagese);

                    if (kokaurdhershitje.IdDegeAdministrative != 0)
                    {
                        clsDegeAdministrative deg = new clsDegeAdministrative(kokaurdhershitje.IdDegeAdministrative);
                        kodDegeAdminUSH = deg.Kodi;
                    }
                    if (kokaurdhershitje.IdPikeShitjeFurnizimi != 0)
                    {
                        clsPikeShitjeFurnizimi pike = new clsPikeShitjeFurnizimi(kokaurdhershitje.IdPikeShitjeFurnizimi);
                        kodPikeShitjeFurnizimiUSH = pike.Kodi;
                    }
                    if (kokaurdhershitje.IdGrup1 != 0)
                    {
                        clsGrupimDokumentiKoka gr1 = new clsGrupimDokumentiKoka(kokaurdhershitje.IdGrup1);
                        kodGrupimdok1USH = gr1.Kodi;
                    }
                    if (kokaurdhershitje.IdTransportues != 0)
                    {
                        clsTransportues tr = new clsTransportues(kokaurdhershitje.IdTransportues);
                        emertimTransportuesUSH = tr.Emertimi;
                    }
                    if (kokaurdhershitje.IdAutomjet != 0 && kokaurdhershitje.IdAutomjet != -1)
                    {
                        clsAutomjete auto = new clsAutomjete();
                        auto.mbushAutomjet(kokaurdhershitje.IdAutomjet);
                        nrShasieUSH = auto.NrShasie;
                    }

                    clsGrupimDokumentiKoka grupimdok2USH = new clsGrupimDokumentiKoka(kokaurdhershitje.IdGrup2);
                    clsGrupimDokumentiKoka grupimdok3USH = new clsGrupimDokumentiKoka(kokaurdhershitje.IdGrup3);

                    clsBanka arkaUSH = new clsBanka(kokaurdhershitje.IdArka);
                    int idViti = clsViti.ktheIdVitPerNdermarrjenSipasKodit(kokaurdhershitje.IdNdermarrje, Convert.ToString(dtDok.Year));

                    mesazh = koka.krijoShitjePerImport("FSH", konfUSH.KodKonfigAmbjente, klient, kokaurdhershitje.NrProjekt, dtDok, nrAutom, nrSerial, kokaurdhershitje.DtMaturimi, kodMonedha, idMonedha, krs, kodAgjenit1USH,
                        menyrePageseUSH, zbritje, kokaurdhershitje.DtRegjistrimi, kokaurdhershitje.IdStatusDok, kokaurdhershitje.IdNdermarrje, kokaurdhershitje.AdresaFaturimit, kokaurdhershitje.AdresaDergimit,
                        kokaurdhershitje.Pershkrimi, kokaurdhershitje.Dogana, kodDegeAdminUSH, kodPikeShitjeFurnizimiUSH, idPerdorues, colTrupi, true, kodGrupimdok1USH, grupimdok2USH.Kodi, grupimdok3USH.Kodi,
                        kokaurdhershitje.AfatKohor, idPerdorues, kokaurdhershitje.PerqindjeAgjenti, kodAgjenit2USH, kokaurdhershitje.PerqindjeAgjenti2, kodAgjenit3USH, kokaurdhershitje.PerqindjeAgjenti3,
                        kokaurdhershitje.EmerKlienti, kokaurdhershitje.Kontakti, false, false, kokaurdhershitje.DtFillimi, kokaurdhershitje.DtMbarimi, nrShasieUSH, kokaurdhershitje.KilometraAuto, kokaurdhershitje.ShpenzimeJoTeZbritshme,
                        kokaurdhershitje.Marresi, idNdermViti, kokaurdhershitje.IdRaportDesing, periudha, out gjeneroDokMag, konf, 0, "", false, rm, ci, kokaurdhershitje.DtFature, 0, 0, 0, new clsKonfigurimAmbjenti(), "", "", "", "",
                        emertimTransportuesUSH, DateTime.Today, arkaUSH.KodiBanka, new DbData(), String.Empty, klient.NiptiKF, qyteti, idGjuha, false, idViti, kokaurdhershitje.StatusTransferimi, nderm.Prind, true, kokaMeme, llojzbritjetotale,
                        kokaurdhershitje.Shenime2, llojMarreveshje, idMarreveshje, kokaurdhershitje.KerkuarNga, kokaurdhershitje.DateKerkese, null, dtTransp, Iic, Nivf, IdOperatori, EIC, Procesi, eInvoiceType, TipiVetefaturimit, NivfKthim);

                    clsKonvertimi konv = new clsKonvertimi(0, koka.IdShitjeKoka, kokaurdhershitje.IdShitjeKoka, kokaurdhershitje.IdKonfigAmbjente, koka.IdKonfigAmbjente);
                    colkonv.Add(konv);
                }

                else
                {
                    if (fushaSerialesh.Values.ToList().ContainsAny("Seriali Unik Kryesor", "Seriali Unik Dytesor"))
                    {
                        serialeUnike = new colSerialeUnikeMagazina();
                        serialeUnike.ShtoSerialNgaImporti(dokTable, fushaSerialesh, kategori, idNdermarrje, idKategoria == 1);
                        colTrupi.BashkoTrupin(serialeUnike.MerrIdArtikujt());

                    }

                    mesazh = koka.krijoShitjePerImport(nenkategoria, llojDokumenti, klFurn, nrProjekti, dtDok, nrAutom, nrSerial, dtMaturimi, kodMonedha, idMonedha, krs, agjentShitje1, menyrePagese, zbritje, dtRegjistrimi, idStatusDok, idNdermarrje,
                        adreseFaturimi, adreseDergimi, pershkrimi, dogana, degeAdministrative, pikeShitjeFurnizimi, idPerdorues, colTrupi, shitje_blerje, grupimdok1, grupimdok2, grupimdok3, afatKohor, idPerdorues, perqindjeAgjent1, agjentShitje2,
                        perqindjeAgjent2, agjentShitje3, perqindjeAgjent3, emerKlienti, kontakti, kase, kupon, dtFillimi, dtMbarimi, automjeti, kilometra, shpenzimeJoTeZbritshme, marresi, idNdermViti, idRapDesign, per, out gjeneroDokMag,
                        konfigAmbjenti, idMag, kodMag, gjeneronFaturePermbledhese, rm, ci, dtFature, idNivelGjeneruesi, idKonfigGjeneruesi, alternativKushtMMDT == "ModifikoDok" ? 0 : idDokNga, konfmag, targashoferi, shoferi, klientFurnitorVartes,
                        karta, transportuesi, dtKrijimiPajisje, arka, dbData, koordinata, niptKlienti, qyteti, idGjuha, kontrolloGjendje, viti.IdViti, 0, false, false, kokaMeme, llojzbritjetotale, shenime2, llojMarreveshje, idMarreveshje, kerkuarNga,
                        dateKerkese, serialeUnike, dtTransp, Iic, Nivf, IdOperatori, EIC, Procesi, eInvoiceType, TipiVetefaturimit, NivfKthim);
                }

                if (mesazh.Status && gjeneronFaturePermbledhese)
                {
                    colKoka.Add(koka);
                }

                if (!mesazh.Status)
                {
                    if (gjeneronFaturePermbledhese)
                        gabimePermbledhese = true;
                    throw new MyException(mesazh.PershkrimMesazhi);
                }

                if (importo && !gjeneronFaturePermbledhese)
                {
                    if (!kontrolloPerGabime(vjenNgaImportSQL, fushat, primaryKey, tePaImportuara, dokTable))
                        return new clsMesazh(false, "Ka gabime!");

                    string idDokImporti = "";
                    if (vjenNgaImportSQL)
                    {
                        idDokImporti = dokTable.Rows[0][primaryKey].ToString();
                    }
                    clsVeprimBankaKoka vep = new clsVeprimBankaKoka();
                    string mesazhvdk, mesazhmag, mesazhbanka, shfaqmesazhapolupe;
                    bool printofature, printogarancifature, pagesefature;
                    clsKusht kushtamor = new clsKusht(konfigAmbjenti.IdKonfigAmbjente, "ZDAM");

                    //DbCore.DbShare.clsKonfigurimAmbjenti konfamortizimi = new clsKonfigurimAmbjenti(kushtamor.Vlera, idGjuha);
                    string mesazhmevonshem = "";
                    clsKonfigurimAmbjenti konfamortizimi = kushtamor.Vlera != 0 ? new clsKonfigurimAmbjenti(kushtamor.Vlera, dbS) : new clsKonfigurimAmbjenti();

                    try
                    {
                        if (alternativKushtMMDT == "Refuzo" && idStatusDokImport == 4)
                        {// Rasti kur importohet dok qe ka refuzuar magazinieri te Alphaweb i shopeve te mema 
                            mesazh = clsKokaShitje.ruajRefuzim(idDokImporti, idDokTransferimNga, shenime2, vjenNgaImportSQL, gjeneronFaturePermbledhese, dbR, emerTabKoka, primaryKey, ndermarrjeKey, idNdermarrje, dbData);
                        }
                        else if (alternativKushtMMDT == "ModifikoDok")
                        {
                            koka.IdDokTransferimNga = idDokTransferimNga;
                            if (clsKokaShitje.merrIdShitjeKokaSipasIdTransferimi(idDokNga) != 0) // Rasti kur importohet per here te dyte ne Alphawebin e Finances (magazine)
                            {
                                koka.IdShitjeKoka = clsKokaShitje.merrIdShitjeKokaSipasIdTransferimi(idDokNga);
                                //koka.NrDok = clsKokaShitje.ktheNrDok(koka.IdShitjeKoka);
                                int idskema = (new clsKusht(konfigAmbjenti.IdKonfigAmbjente, "ZSP")).Vlera;
                                string shfaqmesazhapolupemagazina = "jo";
                                bool dergoemail = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "LE") == "Po";
                                bool dergoemailVFOne = (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "DEVFOne") == "Po");
                                bool tollona = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "RSHTT") == "Po";
                                bool zevendesimtollona = !(clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "ZT") == "Jo");
                                bool tollonakastrati = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "RSHTTK") == "Po";
                                bool tollonakastratielektronik = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "RSHTTKE") == "Po";
                                bool zevendesimtollonakastrati = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "ZTK") == "Po";
                                bool kontrolloSasiKonvertimiDheKthimi = (clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "NK") == "Po");
                                bool kontrolloIMEIFifo = (clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "AFI") == "Po");
                                clsKokaQendraKosto qend = new clsKokaQendraKosto();
                                clsKokaFleteKontabel kok = new clsKokaFleteKontabel(koka.IdShitjeKoka, idKategoria);
                                qend.KtheKokaQKSipasIDGjeneruesDheKonfigMeTrup(kok.IdKokaFleteKontabel, kok.IdKonfigAmbjente);

                                mesazh = koka.modifiko(idGjuha, "", koka.eshteILidhur(), hiddenFieldPerNrAuto, per.IdPeriudha, new colKonvertimi(), gjeneroDokMag, idskema, koka.StatusAprovimi, 0, out shfaqmesazhapolupemagazina, new clsKokaShitje(), dergoemail, new clsNdermarrje(idNdermarrje).OwnShop, dergoemailVFOne, new colSerialetMagazine(), konfamortizimi, (idKategoria == 1) ? true : false, new clsKokaShitje(), out printofature, out printogarancifature, out shfaqmesazhapolupe, meKontabilizim, qend.ColTrupi, konfigAmbjenti.KodKonfigAmbjente, rm, ci, tollona, zevendesimtollona, tollonakastrati, tollonakastratielektronik, zevendesimtollonakastrati, false, kontrolloSasiKonvertimiDheKthimi, true, kontrolloIMEIFifo, false, !konfigAmbjenti.KodKonfigAmbjente.Contains("USHmag"), ref dbData, out mesazhmevonshem, vjenNgaImportSQL, idDokImporti, emerTabKoka, primaryKey, ndermarrjeKey);
                            }
                            else // Rasti kur importohet per here te pare ne Alphawebin e Finances (magazine)
                            {
                                mesazh = koka.ruaj(idGjuha, ci, shitje_blerje, hiddenFieldPerNrAuto, per.IdPeriudha, new colKonvertimi(), gjeneroDokMag, out vep, 0, 0, 0, out mesazhmag, out mesazhbanka, out mesazhvdk, new clsKokaShitje(), 0, 0, false, false, false, "", colSerialet, konfamortizimi, new DbCore.DbRegjistrim.clsKokaShitje(), out printofature, out printogarancifature, out pagesefature, meKontabilizim, out shfaqmesazhapolupe, llojDokumenti, rm, vjenNgaImportSQL, idDokImporti, 0, false, false, false, false, emerTabKoka, primaryKey, ndermarrjeKey, false, false, ruajrenditje, false, new colKokaShitje(), false, false, false, ref dbData, false, "", "", false, out mesazhmevonshem, importo, true, "", "");
                            }
                        }
                        else if (nderm.Prind && dbR.ktheKokaShitjeEkzistonDoksipasID(idDokTransferimNga)) // Rasti i importimit te FSH te mema pasi eshte Ruajtur ne magazine
                        {
                            bool kontrolloIMEIFifo = clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "AFI") == "Po";

                            mesazh = koka.ruaj(idGjuha, ci, true, hiddenFieldPerNrAuto, periudha.IdPeriudha, colkonv, false, out vep, 0, kokaurdhershitje.StatusAprovimi, 0, out mesazhmag, out mesazhbanka, out mesazhvdk, kokaMeme, kokaurdhershitje.IdShitjeKoka, 0, false, eshteOwn, false, "", new colSerialetMagazine(), new clsKonfigurimAmbjenti(), new clsKokaShitje(), out printofature, out printogarancifature, out pagesefature, true, out shfaqmesazhapolupe, "FSH", rm, vjenNgaImportSQL, idDokImporti, 0, false, false, false, false, emerTabKoka, primaryKey, ndermarrjeKey, false, ruajrenditje, false, false, new colKokaShitje(), kontrolloIMEIFifo, false, false, ref dbData, false, "", "", false, out mesazhmevonshem, importo, true, "", "");
                        }
                        else
                        {// Rasti normal sic ishte 
                            mesazh = koka.ruaj(idGjuha, ci, shitje_blerje, hiddenFieldPerNrAuto, per.IdPeriudha, new colKonvertimi(), gjeneroDokMag, out vep, 0, 0, 0, out mesazhmag, out mesazhbanka, out mesazhvdk, new clsKokaShitje(), 0, 0, false, false, false, "", colSerialet, konfamortizimi, new DbCore.DbRegjistrim.clsKokaShitje(), out printofature, out printogarancifature, out pagesefature, meKontabilizim, out shfaqmesazhapolupe, llojDokumenti, rm, vjenNgaImportSQL, idDokImporti, 0, false, false, false, false, emerTabKoka, primaryKey, ndermarrjeKey, false, false, ruajrenditje, false, new colKokaShitje(), false, false, false, ref dbData, false, "", "", false, out mesazhmevonshem, importo, true, koka.IIC, koka.NIVF);
                            bool dergoemailMag = clsAlternativaKushti.getAlternativa(koka.IdKonfigAmbjente, "DEFM") == "Po";
                            if (!string.IsNullOrEmpty(kodKlientIntegrimi))
                            {
                                (clsMesazh gabim, clsMesazh sukses) = dergoMesazhKlientitFaturat(idGjuha, idPerdorues, idNdermVit, idNdermarrje, ci, koka.IdShitjeKoka, koka.NrDok, koka.DtDok, koka.IdKlientFurnitor, koka.IdKonfigAmbjente, (dergoemailMag) ? koka.OColTrupiShitje.AsQueryable().Select(x => x.IdMagazina).ToList().ToArray() : new int[] { }, dergoemailMag);
                            }
                        }
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                            msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ": Dokumenti nuk mund te importohet ne kete moment. Ju lutem provoni perseri!";
                        else
                            msgGabimi = "Dokumenti nuk mund te importohet ne kete moment. Ju lutem provoni perseri!";
                        deadLocked.Merge(dokTable);
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, idKategoria);
                        indexRreshtImporti += dokTable.Rows.Count;
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                            msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ": " + mesazh.PershkrimMesazhi;
                        else
                            msgGabimi = mesazh.PershkrimMesazhi;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, idKategoria);
                        //indexRreshtImporti += dokTable.Rows.Count;
                    }
                    if (!mesazh.Status)
                    {
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                            msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ": " + mesazh.PershkrimMesazhi;
                        else msgGabimi = mesazh.PershkrimMesazhi;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, idKategoria);
                        //indexRreshtImporti += dokTable.Rows.Count;
                    }
                }
                indexRreshtImporti += dokTable.Rows.Count;
                return mesazh;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                if (error == "")
                    error = "Gabim gjate krijimit te dokumentit!";
                DateTime datedok = new DateTime();
                string msgGabimi = "";
                bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                if (dateVlefshme)
                    msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " " + error;
                else
                    msgGabimi = error;
                shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, idKategoria);
                indexRreshtImporti += dokTable.Rows.Count;
                return new clsMesazh(false, error);
            }
        }


        public static clsMesazh RefuzoDokumentImporti(int idDokTransferimNga, string shenime2, bool vjenNgaImportSQL, bool gjeneronFaturePermbledhese, clsDatabaseRegjistrim dbR, string emerTabKoka, string primaryKey, string ndermarrjeKey, int idNdermarrje, DbData dbData, DataTable dokTable, string dtDokumentiEmerImport, ref DataTable gabime, ref DataTable tePaImportuara, ref int indexRreshtImporti, bool importo, string nrDokumentiEmerImport, int idKategoria, clsMesazh mesazh, string[] fushat)
        {
            indexRreshtImporti += dokTable.Rows.Count;
            if (!kontrolloPerGabime(vjenNgaImportSQL, fushat, primaryKey, tePaImportuara, dokTable))
                return new clsMesazh(false, "Ka gabime!");

            string idDokImporti = "";
            if (vjenNgaImportSQL)
            {
                idDokImporti = dokTable.Rows[0][primaryKey].ToString();
            }

            try
            {

                mesazh = clsKokaShitje.ruajRefuzim(idDokImporti, idDokTransferimNga, shenime2, vjenNgaImportSQL, gjeneronFaturePermbledhese, dbR, emerTabKoka, primaryKey, ndermarrjeKey, idNdermarrje, dbData);
            }
            catch (Exception ex)
            {
                DateTime datedok = new DateTime();
                string msgGabimi = "";
                bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                if (dateVlefshme)
                    msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ": " + mesazh.PershkrimMesazhi;
                else
                    msgGabimi = mesazh.PershkrimMesazhi;
                shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, idKategoria);
            }
            if (!mesazh.Status)
            {
                DateTime datedok = new DateTime();
                string msgGabimi = "";
                bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                if (dateVlefshme)
                    msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ": " + mesazh.PershkrimMesazhi;
                else msgGabimi = mesazh.PershkrimMesazhi;
                shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, idKategoria);
            }
            return mesazh;
        }

        public static colTrupiShitje krijoTrupShitjePerImport(colTrupiFormatImporti col, DataTable dokTable, ref int indexRreshtImporti, string dtDokumentiEmerImport, string nrDokumentiEmerImport, ref DataTable gabime, out bool isMagENjejte, int idNdermarrje, int idPerdorues, bool cmimZero, ref bool gabimePermbledhese, bool importo, bool gjeneronFaturePermbledhese, ref DataTable tePaImportuara, bool vjenNgaImportSQL, bool importAutomatik, clsKonfigurimAmbjenti konfigMagazina, double kursi, int idstatusdok, ref colSerialetMagazine colSerialet, bool shitje_blerje, int idkategoria, string sasiRezervimiAuto, DbData dbData, int idDokTransferimNga, int loan, DateTime dtDok, string klientFurnitor, bool percaktoCmimNgaKartela, string kodmonedha, bool ngarkoKodbar)
        {
            string error = "";
            clsMesazh mesazh = new clsMesazh(true);
            colTrupiShitje colTrupi = new colTrupiShitje();
            colTrupiShitje colTrupiKomision = new colTrupiShitje();
            int j = 1, i = 1;
            int idMagTemp = -1;
            isMagENjejte = true;
            var ids = new List<int>();
            ids.Add(idDokTransferimNga);
            colTrupiShitje colTrupiUrdher = null;
            var kf = new clsKlientFurnitor();
            bool klientKomision = false;
            clsNdermarrje nderm = new clsNdermarrje(idNdermarrje);

            if (nderm.Prind) colTrupiUrdher = new colTrupiShitje(ids);
            foreach (DataRow dr in dokTable.Rows)
            {
                try
                {
                    //clsTrupiShitje trupi = new clsTrupiShitje();
                    string kodi = "", pershkrimTrupi = "", njesia = "", tvsh = "", magazina = "", detajim1 = "", detajim2 = "", shenime = "", llogariShpenzimi = "", llojVeprimi = "", kategoriShpenzimi = "", seriali = "", kodbari = "", serialiUnikKryesor = "", serialiUnikDytesor = "", shenime2trupi = "", kodArtikulliSet = "";
                    double sasia = 0, cmimi = 0, cmimitvsh = 0, zbritjeAnalitike = 0, zbritjevlere = 0, vleftaPaTvsh = 0, vleftaMeTvsh = 0, gjatesi = 0, gjeresi = 0, sasiPermase = 0, sasiRez = 0, sasiMbeturNgaImporti = 0, vleraKomisionit = 0;
                    DateTime dtFillimTrupi = DateTime.Today, dtMbarimTrupi = DateTime.Today;
                    int llojzbritje = 1, idTrupiTransferimNga = 0;
                    //marrim vlerat e trupit sipas formatit te importit.
                    #region trupi
                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        switch (trup.KodKontrolli)
                        {
                            case "Lloji":
                                llojVeprimi = vendosVlere(trup, dr, out error);
                                break;

                            case "Kodi":
                                kodi = vendosVlere(trup, dr, out error);
                                break;

                            case "Njesia":
                                njesia = vendosVlere(trup, dr, out error);
                                break;

                            case "Sasia":
                                sasia = vendosDouble(trup, dr, out error);
                                sasiMbeturNgaImporti = sasia;
                                break;

                            case "Cmimi":
                                cmimi = vendosDouble(trup, dr, out error);
                                break;
                            case "Cmimi me Tvsh":
                                cmimitvsh = vendosDouble(trup, dr, out error);
                                break;
                            case "Vlefta pa TVSH":
                                vleftaPaTvsh = vendosDouble(trup, dr, out error);
                                break;

                            case "TVSH":
                                tvsh = vendosVlere(trup, dr, out error);
                                break;

                            case "Vlefta me TVSH":
                                vleftaMeTvsh = vendosDouble(trup, dr, out error);
                                break;

                            case "Magazina":
                                magazina = vendosVlere(trup, dr, out error);
                                break;

                            case "Detajimi 1":
                                //detajim1 = vendosVlere(trup, dr, out error);
                                //break;
                                detajim1 = vendosVlere(trup, dr, out error);
                                DateTime dt = new DateTime();
                                bool parseDt = DateTime.TryParse(detajim1, out dt);
                                if (parseDt && detajim1.IndexOf("00:00:00") > 0)
                                    detajim1 = detajim1.Substring(0, detajim1.IndexOf("00:00:00") - 1);
                                break;

                            case "Detajimi 2":
                                detajim2 = vendosVlere(trup, dr, out error);
                                DateTime dt2 = new DateTime();
                                bool parseDt2 = DateTime.TryParse(detajim2, out dt2);
                                if (parseDt2 && detajim2.IndexOf("00:00:00") > 0)
                                    detajim2 = detajim2.Substring(0, detajim2.IndexOf("00:00:00") - 1);
                                break;

                            case "Zbritje Analitike":
                                zbritjeAnalitike = vendosDouble(trup, dr, out error);
                                break;
                            case "Lloj zbritje":
                                switch (vendosVlere(trup, dr, out error))
                                {
                                    case "Perqindje":
                                    case "perqindje":
                                        llojzbritje = 1;
                                        break;
                                    case "Vlere":
                                    case "vlere":
                                        llojzbritje = 2;
                                        break;
                                    case "":
                                        llojzbritje = 1;
                                        break;
                                    default:
                                        error = "Lloj zbritje e pacaktuar";
                                        break;
                                }
                                // llojzbritje = vendosVlere(trup, dr, out error);
                                break;
                            case "Zbritje vlere":
                                zbritjevlere = vendosDouble(trup, dr, out error);
                                break;
                            case "Pershkrim Trupi":
                                pershkrimTrupi = vendosVlere(trup, dr, out error);
                                break;

                            case "Gjeresi":
                                gjeresi = vendosDouble(trup, dr, out error);
                                break;

                            case "Gjatesi":
                                gjatesi = vendosDouble(trup, dr, out error);
                                break;

                            case "Sasi Permase":
                                sasiPermase = vendosDouble(trup, dr, out error);
                                break;

                            case "Shenime":
                                shenime = vendosVlere(trup, dr, out error);
                                break;

                            case "Date Fillimi Trupi":
                                dtFillimTrupi = vendosDate(trup, dr, out error);
                                if (dtFillimTrupi == DateTime.MinValue)
                                    dtFillimTrupi = DateTime.Today;
                                break;

                            case "Date Mbarimi Trupi":
                                dtMbarimTrupi = vendosDate(trup, dr, out error);
                                if (dtMbarimTrupi == DateTime.MinValue)
                                    dtMbarimTrupi = DateTime.Today;
                                break;

                            case "Llogari Shpenzimi":
                                llogariShpenzimi = vendosVlere(trup, dr, out error);
                                break;

                            case "Seriali":
                                seriali = vendosVlere(trup, dr, out error);
                                break;

                            case "Kodbari":
                                kodbari = vendosVlere(trup, dr, out error);
                                break;
                            case "Kategori Shpenzimi":
                                kategoriShpenzimi = vendosVlere(trup, dr, out error);
                                break;
                            case "ID Trupi Transferim Nga":
                                idTrupiTransferimNga = vendosInt(trup, dr, out error);
                                break;
                            case "Seriali Unik Kryesor":
                                serialiUnikKryesor = vendosVlere(trup, dr, out error);
                                break;
                            case "Seriali Unik Dytesor":
                                serialiUnikDytesor = vendosVlere(trup, dr, out error);
                                break;
                            case "Shenime 2 Trupi":
                                shenime2trupi = vendosVlere(trup, dokTable.Rows[0], out error);
                                break;
                            case "Artikulli Set":
                                kodArtikulliSet = vendosVlere(trup, dr, out error);
                                break;
                        }


                        if (error != "")
                        {
                            DateTime datedok = new DateTime();
                            string msgGabimi = "";
                            bool dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                            if (dateVlefshme)
                                msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + " :" + trup.EmerImporti + " duhet te jete numer!";
                            else
                                msgGabimi = error;
                            shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, idkategoria, dr);
                            continue;
                        }
                    }
                    #endregion
                    kf.mbushKlientFurnitorSipasKodit(klientFurnitor, idNdermarrje);


                    clsTaksa takse = new clsTaksa(tvsh, idNdermarrje);

                    if (takse.IdTaksa == 0 && tvsh != "" && tvsh != "Pa TVSH")
                        throw new MyException("Niveli i TVSH-se nuk ekziston!");

                    double koeficientTVSH = (1 + Double.Parse(takse.NormaPerqindje.ToString()) / 100);
                    //double cmimitvsh = 0, zbritjeAnalitike = 0, zbritjevlere = 0, vleftaPaTvsh = 0, vleftaMeTvsh = 0;


                    if (cmimi == 0 && cmimitvsh != 0)

                        cmimi = cmimitvsh / koeficientTVSH;

                    if (!string.IsNullOrWhiteSpace(kodArtikulliSet) && llojVeprimi == "Artikull")
                        kodi = kodArtikulliSet;



                    if (sasiRezervimiAuto.ToLower() == "po")
                        sasiRez = sasia;
                    //llogarisimi zbritjen analitike nese nuk eshte e plotesuar

                    klientKomision = kf.LlogaritKomision;
                    int idNivelZbritje = kf.ZbritjeAnalitike;
                    double zbritjaAnalitikeImport = 0;
                    clsZbritjeAnalitike colZbritje = clsZbritjeAnalitike.ktheZbritjeAnalitikeArtikulliRow(kodi, idNivelZbritje, DateTime.Now.ToString(), idPerdorues, idNdermarrje);

                    zbritjaAnalitikeImport = colZbritje != null ? (double)colZbritje.Zbritja : 0;
                    if (percaktoCmimNgaKartela)
                    {
                        //Tvsh e karteles se artikullit
                        int idTvshArtikulli = DbCore.DbInventari.clsArtikulli.ktheIdTvshSipasKodArt(kodi, idNdermarrje);
                        double koeficientArtikulliTVSH;
                        clsTaksa takseArtikulli;
                        //perdor tvsh e artikullit ose ate default te ndermarrjes
                        if (idTvshArtikulli > 0)
                            takseArtikulli = new clsTaksa(idTvshArtikulli);
                        else
                        {
                            takseArtikulli = new clsTaksa();
                            takseArtikulli.mbushTakseDefaultNdermarrje(idNdermarrje);
                        }
                        koeficientArtikulliTVSH = (1 + Double.Parse(takseArtikulli.NormaPerqindje.ToString()) / 100);


                        double cmimiKarteles = Double.Parse(merrCmimSipasNivelit(kf.IdNivelCmimi, kodi, idPerdorues, njesia, kodmonedha, dtDok.ToString(), (decimal)kursi, (decimal)sasia, idNdermarrje, shitje_blerje ? 1 : 0, 0)[0].ToString());
                        //bejme llogaritjet e fushave te tjera te trupit sipas cmimit dhe tvsh-se se re
                        if (cmimiKarteles > 0)
                        {
                            tvsh = takseArtikulli.KodTaksa;
                            cmimi = cmimiKarteles;
                            cmimitvsh = cmimi * koeficientArtikulliTVSH;
                            vleftaPaTvsh = sasia * cmimi * (1 - zbritjeAnalitike / 100);//
                            vleftaMeTvsh = vleftaPaTvsh * koeficientArtikulliTVSH;
                        }
                    }
                    if (klientKomision)
                        vleraKomisionit = ktheVlereKomision(zbritjaAnalitikeImport, vleftaPaTvsh);

                    clsDatabaseRegjistrim dbR = new clsDatabaseRegjistrim(dbData);

                    clsTrupiShitje trupShitje = new clsTrupiShitje();
                    if (nderm.Prind && dbR.ktheKokaShitjeEkzistonDoksipasID(idDokTransferimNga)) //import i nje dokumenti shitje, USH e te cilit ekziston te mema
                    {

                        var trupat = colTrupiUrdher.FindAll(x => x.Kodi.ToLower() == kodi.ToLower() && x.kaSasiTeMbetur()).ToList();
                        foreach (var trupiurdher in trupat)
                        {
                            clsTrupiShitje trupi = new clsTrupiShitje();
                            if (!trupiurdher.kaSasiTeMbetur())
                                continue;
                            if (sasiMbeturNgaImporti == 0)
                                break;

                            double sasiaTrupiUrdher = 0;
                            if (trupiurdher.Sasimbetur <= sasiMbeturNgaImporti)
                            {
                                sasiaTrupiUrdher = trupiurdher.Sasia;
                                if (!trupiurdher.ulSasiTeMbetur(sasiaTrupiUrdher))
                                    throw new MyException("Sasia e importuar nuk mjafton per te plotesuar sasine e USH ekzistuese!");
                                sasiMbeturNgaImporti -= sasiaTrupiUrdher;
                            }
                            else //rasti kur sasia e importuar eshte 1 dhe sasia e USH eshte me shume se 1, pra duhet bere shperndarja
                            {
                                sasiaTrupiUrdher = 1;
                                if (!trupiurdher.ulSasiTeMbetur(1))
                                    throw new MyException("Sasia e importuar nuk mjafton per te plotesuar sasine e USH-se ekzistuese!");
                                sasiMbeturNgaImporti--;
                            }

                            switch (trupiurdher.IdLlojVeprimi)
                            {
                                case 1:
                                    llojVeprimi = "Artikull";
                                    break;
                                case 2:
                                    llojVeprimi = "Makro";
                                    break;
                                case 3:
                                    llojVeprimi = "Llogari";
                                    break;
                                default:
                                    llojVeprimi = "";
                                    break;
                            }
                            clsNjesiArtikulli njesiaUSH = new clsNjesiArtikulli(trupiurdher.IdNjesia);

                            int idtaksaUSH = 0;
                            double tvshUSH = 0;
                            double vleftaMeTvshUSH = vleftaMeTvsh, vleftaPaTvshUSH = vleftaPaTvsh, zbritjevlereUSH = zbritjevlere, cmimiUSH = cmimi;
                            string kodTakseUSH = "";

                            if (trupiurdher.Tvsh != 0) // merre nga USH
                            {
                                idtaksaUSH = trupiurdher.Tvsh;
                                kodTakseUSH = new clsTaksa(trupiurdher.Tvsh).KodTaksa;
                            }

                            else // merre nga ndermarrja
                            {
                                clsNdermarrje ndermImportuese = new clsNdermarrje(idNdermarrje);
                                clsTaksa taksaUSH = new clsTaksa(ndermImportuese.IdTakse);
                                kodTakseUSH = taksaUSH.KodTaksa;
                            }

                            vleftaMeTvshUSH = (vleftaMeTvsh / sasia) * sasiaTrupiUrdher;
                            vleftaPaTvshUSH = (vleftaPaTvsh / sasia) * sasiaTrupiUrdher;
                            zbritjevlereUSH = (zbritjevlere / sasia) * sasiaTrupiUrdher;

                            if (trupiurdher.IdLlojVeprimi == 1)//per artikujt marrim tvsh e caktuar tek kartela dhe jo te Magazines Vodafone (ish winlinekartes)
                            {
                                //idtaksaUSH = DbCore.DbInventari.clsArtikulli.ktheIdTvshSipasKodArt(trupiurdher.Kodi, idNdermarrje); nuk ka nevoje ta marr nga artikulli sepse e mori nga USH
                                DbCore.DbRegjistrim.clsTaksa taks = new DbCore.DbRegjistrim.clsTaksa(idtaksaUSH);
                                vleftaPaTvshUSH = vleftaMeTvshUSH / (1 + double.Parse(taks.NormaPerqindje.ToString()) / 100);
                                tvshUSH = vleftaMeTvshUSH - vleftaPaTvshUSH;
                                cmimiUSH = (vleftaPaTvshUSH + zbritjevlereUSH) / sasiaTrupiUrdher;
                            }
                            else
                            {
                                tvshUSH = vleftaMeTvshUSH - vleftaPaTvshUSH;
                                double perqindjatvsh = Math.Round(tvshUSH / vleftaPaTvshUSH, 2);

                                colTaksa taksatUSH = new colTaksa(idNdermarrje, idPerdorues);
                                foreach (DbCore.DbRegjistrim.clsTaksa taksa in taksatUSH)
                                    if (Convert.ToDouble(taksa.NormaPerqindje) == perqindjatvsh * 100)
                                        idtaksaUSH = taksa.IdTaksa;
                            }

                            string kodMagUSH = "";
                            if (trupiurdher.IdMagazina != 0)
                            {
                                clsNjesiAdministrative magUSH = new clsNjesiAdministrative(trupiurdher.IdMagazina);
                                kodMagUSH = magUSH.Kodi;
                            }
                            clsTrupiShitje trupiurdherbij = new clsTrupiShitje();
                            trupiurdherbij.merrSipasIdTrasferimi(trupiurdher.IdShitjeTrupi);

                            clsLlogari llogariShpenzimiUSH = new clsLlogari(trupiurdher.IdLlogShpenzimi);
                            zbritjevlereUSH = zbritjeAnalitike * cmimiUSH * sasiaTrupiUrdher / 100;

                            //KtheColKomision(ref colTrupiKomision, trupiurdher, idNdermarrje, klientKomision, vleraKomisionit, kodi, idPerdorues);
                            mesazh = trupi.krijoTrupShitjeImport(trupiurdher.IdShitjeTrupi, trupiurdher.IdShitjeKoka, llojVeprimi, trupiurdher.Kodi, trupiurdher.Pershkrimi, detajim1, detajim2, njesiaUSH.KodNjesia, sasiaTrupiUrdher, cmimiUSH, zbritjeAnalitike, vleftaMeTvshUSH, kodTakseUSH, vleftaPaTvshUSH, kodMagUSH, trupiurdher.Gjeresi, trupiurdher.Gjatesi, trupiurdher.SasiPermasa, trupiurdher.Shenime, trupiurdher.DtFillimi, trupiurdher.DtMbarimi, trupiurdher.SasiRez, trupiurdher.IdTrupiRezervimi, trupiurdherbij.IdShitjeTrupi, idNdermarrje, idPerdorues, llogariShpenzimiUSH.NrLlogari, cmimZero, gjeneronFaturePermbledhese, seriali, j - 1, konfigMagazina, kursi, idstatusdok, ref colSerialet, shitje_blerje, "", 1, zbritjevlereUSH, "", dbData, cmimitvsh, serialiUnikKryesor, loan, dtDok, shenime2trupi, vleraKomisionit, false, 0, ref colTrupiKomision, trupiurdher, klientKomision, true, 0, trupiurdher.IdShitjeTrupi, 0);
                            if (mesazh.Status)
                            {
                                if (idMagTemp == -1)
                                    idMagTemp = trupi.IdMagazina;
                                else
                                    if (isMagENjejte && trupi.IdMagazina != idMagTemp)
                                    isMagENjejte = false;
                                colTrupi.Add(trupi);
                                indexRreshtImporti++;
                                j++;
                            }
                            else
                            {
                                throw new MyException(mesazh.PershkrimMesazhi);
                            }
                        }
                    }
                    else
                    {
                        //KtheColKomision(ref colTrupiKomision,null , idNdermarrje, klientKomision, vleraKomisionit, kodi, idPerdorues);
                        int idTrupiKthimi = 0;
                        int sasiambetur = 0;
                        if (nderm.OwnShop && sasia < 0)
                        {
                            idTrupiKthimi = clsKokaShitje.merrIdTrupiShitjeNeOwnShop(idDokTransferimNga, kodi);
                            sasiambetur = clsTrupiShitje.merrSasineMbetur(idTrupiKthimi);
                        }

                        if (kodbari == "" && ngarkoKodbar)
                            kodbari = clsKodbari.ktheKodbarSipasIdArtikulliNjesiaKodbariIPare(kodi, idNdermarrje);

                        mesazh = trupShitje.krijoTrupShitjeImport(0, 0, llojVeprimi, kodi, pershkrimTrupi, detajim1, detajim2, njesia, sasia, cmimi, zbritjeAnalitike, vleftaMeTvsh, tvsh, vleftaPaTvsh, magazina, gjeresi, gjatesi, sasiPermase, shenime, dtFillimTrupi, dtMbarimTrupi, sasiRez, 0, 0, idNdermarrje, idPerdorues, llogariShpenzimi, cmimZero, gjeneronFaturePermbledhese, seriali, j - 1, konfigMagazina, kursi, idstatusdok, ref colSerialet, shitje_blerje, kodbari, llojzbritje, zbritjevlere, kategoriShpenzimi, dbData, cmimitvsh, "", 0, dtDok, shenime2trupi, vleraKomisionit, false, idTrupiKthimi, ref colTrupiKomision, null, klientKomision, true, 0, 0, sasiambetur);
                        if (mesazh.Status)
                        {
                            if (idMagTemp == -1)
                                idMagTemp = trupShitje.IdMagazina;
                            else
                                if (isMagENjejte && trupShitje.IdMagazina != idMagTemp)
                                isMagENjejte = false;
                            colTrupi.Add(trupShitje);
                            indexRreshtImporti++;
                            j++;
                        }
                        else
                        {
                            throw new MyException(mesazh.PershkrimMesazhi);
                        }
                    }



                }
                catch (MyException ex)
                {
                    DateTime datedok = new DateTime();
                    string msgGabimi = "";
                    bool dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                    if (dateVlefshme)
                        msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + ": " + ex.Message;
                    else
                        msgGabimi = ex.Message;
                    shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, idkategoria, dr);
                    indexRreshtImporti++;
                    j++;
                    if (gjeneronFaturePermbledhese)
                        gabimePermbledhese = true;
                    continue;
                }
            }
            colTrupi.AddRange(colTrupiKomision);
            return colTrupi;
        }

        public static clsMesazh ruajFaturePermbledhese(DataTable dt, ref DataTable gabime, DbCore.DbRegjistrim.colKokaShitje colBlerjeShitje, int idGjuha, CultureInfo ci, ResourceManager rm, colTrupiFormatImporti col, string primaryKey, string ndermarrjeKey, ref int indexRreshtImporti, bool eshteMeme, bool eshteOwn, bool ngaImportSql, int idNdermarrje, string emerTabKoka, ref DataTable tePaImportuara, bool importAutomatik, string nrDokumentiEmerImport, string dtDokumentiEmerImport, string llojDokumentiEmerImport, int idkategoria, DbData dbData)
        {
            var mesazh = new clsMesazh();
            try
            {
                ArrayList array = new ArrayList();
                var colshitje = clsKokaShitje.gjeneroFaturePermbledhese(colBlerjeShitje, new List<DbCore.DbRegjistrim.colKonvertimi>(), eshteMeme, true, array, dbData, idGjuha, ref gabime);
                string serverUrl = "";//DbCore.clsFunksione.ktheServerUrl(request); TOCHECK PATI
                clsVeprimBankaKoka veprimebanka = new clsVeprimBankaKoka();
                string shfaqmesazhapolupemagazina, shfaqmesazhapolupebanka, shfaqmesazhapolupeVDK, shfaqmesazhapolupe;
                bool printofature, printogarancifature, pageseFature;
                var serializusi = new JavaScriptSerializer { MaxJsonLength = 50000000 };
                int indexColShitje = 0;

                foreach (var kokeShitje in colshitje)
                {
                    try
                    {
                        var kushtamor = new clsKusht(kokeShitje.IdKonfigAmbjente, "ZDAM");
                        var konfamortizimi = new clsKonfigurimAmbjenti(kushtamor.Vlera);
                        var dergoemail = clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "LE") == "Po";
                        var dergoemailVfOne = clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "DEVFOne") == "Po";

                        foreach (DataRow dr in dt.Rows)
                            dr[dtDokumentiEmerImport] = DateTime.Parse(dr[dtDokumentiEmerImport].ToString()).ToString("dd/MM/yyyy HH:mm:ss");

                        bool kaGabimCmimi = false;
                        //lejon konfigurimi cmim zero, apo jo. Na duhet per kontrollin e vlerave te trupi.
                        if (clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "CZ") == "Bllokues")
                        {
                            foreach (var trup in kokeShitje.OColTrupiShitje)
                            {
                                if (trup.Cmimi == 0)
                                {
                                    var mesazhGabim = "Fatura permbledhese e gjeneruar nga ";
                                    var shitjetGabim = (colKokaShitje)array[indexColShitje];
                                    var rreshtaMeGabime = new DataTable();

                                    for (int k = 0; k < shitjetGabim.Count(); k++)
                                    {
                                        string dateDok = shitjetGabim[k].DtDok.Day + "/" + shitjetGabim[k].DtDok.Month + "/" + shitjetGabim[k].DtDok.Year;
                                        string kodkonf = clsKonfigurimAmbjenti.ktheKodKonfigurimi(shitjetGabim[0].IdKonfigAmbjente);
                                        mesazhGabim += kodkonf + " " + shitjetGabim[k].NrDok + " " + dateDok + ", ";
                                        rreshtaMeGabime.Merge(dt.Select(string.Format("[{0}] = '{1}' AND [{2}] = '{3}' AND [{4}] = '{5}'", nrDokumentiEmerImport, shitjetGabim[k].NrDok, dtDokumentiEmerImport, shitjetGabim[k].DtDok.ToString("dd/MM/yyyy HH:mm:ss"), llojDokumentiEmerImport, kodkonf)).CopyToDataTable());
                                    }

                                    mesazhGabim += " ka gabime! " + "Nuk lejohet cmimi zero ne trupin e fatures!";
                                    shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, true, mesazhGabim, kokeShitje.NrDok, ngaImportSql, rreshtaMeGabime, idkategoria);
                                    kaGabimCmimi = true;
                                    break;
                                }
                            }
                        }

                        if (kaGabimCmimi)
                        {
                            indexColShitje++;
                            continue;
                        }

                        var konfigFp = new clsKonfigurimAmbjenti();
                        konfigFp.mbushKonfigAmbjSipasId(kokeShitje.IdKonfigAmbjente);

                        var kodKonfigFp = clsKonfigurimAmbjenti.ktheKodKonfigurimi(kokeShitje.IdKonfigAmbjente);

                        var gjeneroDokMag = clsAlternativaKushti.getAlternativa(konfigFp.IdKonfigAmbjente, "GJDM") == "Po";

                        IDictionary<string, object> hidden = new Dictionary<string, object>();
                        var list = new List<NrAuto>();
                        var idnrautonrdok = clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(kokeShitje.IdKonfigAmbjente, "txtNumer", 506);
                        var nrdokshitje = clsNrAutom.merrVlerenNrAutomatik(idnrautonrdok, kokeShitje.DtDok);
                        if (!string.IsNullOrEmpty(nrdokshitje))//nqs ka nr automatik
                        {
                            var nrdokshi = new NrAuto
                            {
                                kodKontrolli = "NrDok",
                                idNrAuto = idnrautonrdok,
                                vlereNrAuto = nrdokshitje
                            };

                            list.Add(nrdokshi);
                            nrdokshitje = nrdokshi.vlereNrAuto;
                            hidden.Add("NrDok", serializusi.Serialize(nrdokshi));
                        }
                        else
                        {
                            Int64 nrd = clsKokaShitje.merrNrMaxDokumenti(idNdermarrje, kokeShitje.DtDok.Day + "/" + kokeShitje.DtDok.Month + "/" + kokeShitje.DtDok.Year + "_%") + 1;
                            nrdokshitje = kokeShitje.DtDok.Day + "/" + kokeShitje.DtDok.Month + "/" + kokeShitje.DtDok.Year + "_" + nrd;//nqs nuk ka nr automatik merr daten e dokumentit +nr incrementues
                            kokeShitje.NrDok = nrdokshitje;
                        }

                        var idnrautonrserial = clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(kokeShitje.IdKonfigAmbjente, "txtNumerSerial", 506);
                        var nrserialshitje = clsNrAutom.merrVlerenNrAutomatik(idnrautonrserial, kokeShitje.DtDok);

                        if (!string.IsNullOrEmpty(nrserialshitje))
                        {
                            var nrser = new NrAuto
                            {
                                kodKontrolli = "NrSerial",
                                idNrAuto = idnrautonrserial,
                                vlereNrAuto = nrserialshitje
                            };

                            list.Add(nrser);
                            nrserialshitje = nrser.vlereNrAuto;
                            hidden.Add("NrSerial", serializusi.Serialize(nrser));
                        }
                        else nrserialshitje = "";

                        var mekontabilizim = clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "GJK") != "Jo";

                        if (clsAlternativaKushti.getAlternativa(kokeShitje.IdKonfigAmbjente, "SDI") == "Draft")
                            mekontabilizim = false;

                        var shitjetGjeneruese = (colKokaShitje)array[indexColShitje];
                        var kodKonfigurimi = clsKonfigurimAmbjenti.ktheKodKonfigurimi(shitjetGjeneruese[0].IdKonfigAmbjente);
                        var idDokumentat = "";

                        if (ngaImportSql)
                        {
                            for (int k = 0; k < shitjetGjeneruese.Count; k++)
                            {
                                var rreshtaGjeneruese = dt.Select(string.Format("[{0}] = '{1}' AND [{2}] = '{3}' AND [{4}] = '{5}'", nrDokumentiEmerImport, shitjetGjeneruese[k].NrDok, dtDokumentiEmerImport, shitjetGjeneruese[k].DtDok.ToString("dd/MM/yyyy HH:mm:ss"), llojDokumentiEmerImport, kodKonfigurimi)).CopyToDataTable();
                                idDokumentat += rreshtaGjeneruese.Rows[0][primaryKey] + ";";
                            }
                        }

                        var mesazhmevonshem = "";
                        var idPeriudhaKont = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(kokeShitje.DtDok, idNdermarrje);

                        mesazh = kokeShitje.ruaj(idGjuha, serverUrl, true, hidden, idPeriudhaKont, new colKonvertimi(), gjeneroDokMag, out veprimebanka, 0, DbCore.DbRegjistrim.StatusAprovimi.Undefined, 0, out shfaqmesazhapolupemagazina, out shfaqmesazhapolupebanka, out shfaqmesazhapolupeVDK, new DbCore.DbRegjistrim.clsKokaShitje(), 0, 0, dergoemail, eshteOwn, dergoemailVfOne, "", new colSerialetMagazine(), konfamortizimi, new DbCore.DbRegjistrim.clsKokaShitje(), out printofature, out printogarancifature, out pageseFature, mekontabilizim, out shfaqmesazhapolupe, kodKonfigFp, ngaImportSql, idDokumentat, 0, false, false, true, false, emerTabKoka, primaryKey, ndermarrjeKey, false, false, false, "", false, false, false, false, false, new colKokaShitje(), false, false, false, ref dbData, false, "", "", false, out mesazhmevonshem, false, true, "", "");

                        if (!mesazh.Status)
                        {
                            var mesazhGabim = "Fatura permbledhese e gjeneruar nga ";
                            var shitjetGabim = (colKokaShitje)array[indexColShitje];
                            var rreshtaMeGabime = new DataTable();

                            for (int k = 0; k < shitjetGabim.Count; k++)
                            {
                                string dateDok = shitjetGabim[k].DtDok.Day + "/" + shitjetGabim[k].DtDok.Month + "/" + shitjetGabim[k].DtDok.Year;
                                mesazhGabim += kodKonfigurimi + " " + shitjetGabim[k].NrDok + " " + dateDok + ", ";
                                rreshtaMeGabime.Merge(dt.Select(string.Format("[{0}] = '{1}' AND [{2}] = '{3}' AND [{4}] = '{5}'", nrDokumentiEmerImport, shitjetGabim[k].NrDok, dtDokumentiEmerImport, shitjetGabim[k].DtDok.ToString("dd/MM/yyyy HH:mm:ss"), llojDokumentiEmerImport, kodKonfigurimi)).CopyToDataTable());
                            }

                            mesazhGabim += " ka gabime! " + mesazh.PershkrimMesazhi;
                            shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, true, mesazhGabim, kokeShitje.NrDok, ngaImportSql, rreshtaMeGabime, idkategoria);
                        }

                        indexColShitje++;
                    }
                    catch (Exception)
                    {
                        var mesazhGabim = "Fatura permbledhese e gjeneruar nga dokumentet ";
                        var shitjetGabim = (colKokaShitje)array[indexColShitje];
                        var kodKonfigurimi = clsKonfigurimAmbjenti.ktheKodKonfigurimi(shitjetGabim[0].IdKonfigAmbjente);
                        var rreshtaMeGabime = new DataTable();
                        for (int k = 0; k < shitjetGabim.Count; k++)
                        {
                            string dateDok = shitjetGabim[k].DtDok.Day + "/" + shitjetGabim[k].DtDok.Month + "/" + shitjetGabim[k].DtDok.Year;
                            mesazhGabim += kodKonfigurimi + " " + shitjetGabim[k].NrDok + " " + dateDok + ", ";
                            rreshtaMeGabime.Merge(dt.Select(string.Format("[{0}] = '{1}' AND [{2}] = '{3}' AND [{4}] = '{5}'", nrDokumentiEmerImport, shitjetGabim[k].NrDok, dtDokumentiEmerImport, shitjetGabim[k].DtDok.Day, llojDokumentiEmerImport, kodKonfigurimi)).CopyToDataTable());
                        }
                        mesazhGabim += " ka gabime! " + mesazh.PershkrimMesazhi;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, true, mesazhGabim, kokeShitje.NrDok, ngaImportSql, rreshtaMeGabime, idkategoria);
                        indexColShitje++;
                        continue;
                    }
                }
                return mesazh;
            }
            catch (MyException)
            {
                object[] arr = { "Fature permbledhese", "Ndodhi nje gabim gjate krijimit te faturave permbledhese", indexRreshtImporti };
                gabime.Rows.Add(arr);
                return new clsMesazh(false, "Ndodhi nje gabim gjate krijimit te fatures permbledhese!");
            }
            catch (Exception)
            {
                object[] arr = { "Fature permbledhese", "Ndodhi nje gabim gjate krijimit te faturave permbledhese", indexRreshtImporti };
                gabime.Rows.Add(arr);
                return new clsMesazh(false, "Ndodhi nje gabim gjate krijimit te fatures permbledhese!");
            }
        }

        public static void shtoGabimeNeDataTable(ref DataTable gabime, ref DataTable tePaImportuara, int indexRreshti, bool importo, string mesazhGabimi, string identifikuesi, bool vjenNgaImportSQL, DataTable dokKoka, int kategoria, DataRow drRreshti = null)
        {
            bool meIndexRreshti = !gabime.Columns.Contains("Rreshti me id");
            string idRreshti = meIndexRreshti ? indexRreshti.ToString() : dokKoka.Rows[0][vjenNgaImportSQL ? "IDIMPORTSHITJE" : "Id"].ToString();

            string id = "";
            if (vjenNgaImportSQL)
            {
                if (kategoria != 45)
                    id = "IDIMPORTTRUPISHITJE";
                else
                    id = "IDIMPORTRECEPTURA";

                if (!meIndexRreshti && drRreshti != null)
                    idRreshti += $" -> {drRreshti[id]}";
            }
            else id = "Id";

            if (importo || drRreshti == null)
                foreach (DataRow dr in dokKoka.Rows)
                {
                    if (tePaImportuara.Select(String.Format("{0} = '{1}'", id, dr[id])).Count() == 0)
                        tePaImportuara.ImportRow(dr);
                }
            else
                tePaImportuara.ImportRow(drRreshti);

            object[] arr = { identifikuesi, mesazhGabimi, idRreshti };
            gabime.Rows.Add(arr);

            if (meIndexRreshti)
                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;

        }

        public static string vendosVlere(clsTrupiFormatImporti trup, DataRow dr, out string error)
        {
            string fusha = "";
            error = "";
            try
            {
                if (!trup.Visible)
                    return fusha;
                if (trup.Shfaq)
                {
                    if (dr[trup.EmerImporti].ToString() != "")
                        return dr[trup.EmerImporti].ToString();
                    if (trup.VleraDefault != "")
                        return trup.VleraDefault;
                    return fusha;
                }
                if (fusha == "" && trup.VleraDefault != "")
                    return trup.VleraDefault;
            }
            catch (Exception)
            {
                error = "Fusha " + trup.EmerImporti + " nuk ekziston ne kete gride!";
            }
            return fusha;
        }

        public static bool vendosBool(clsTrupiFormatImporti trup, DataRow dr, out string error)
        {
            bool fusha = false;
            error = "";
            try
            {
                if (!trup.Visible)
                    return fusha;
                if (trup.Shfaq)
                {
                    if (dr[trup.EmerImporti].ToString() != "")
                    {
                        return (ktheVlereBool(dr[trup.EmerImporti].ToString().ToLower()));
                    }
                    if (trup.VleraDefault != "")
                    {
                        return (ktheVlereBool(trup.VleraDefault.ToString().ToLower()));
                    }
                    return fusha;
                }
                if (trup.VleraDefault != "")
                {
                    return (ktheVlereBool(trup.VleraDefault.ToString().ToLower()));
                }
            }
            catch (Exception)
            {
                error = "Fusha " + trup.EmerImporti + " nuk ekziston ne kete gride!";
            }
            return fusha;
        }

        public static decimal vendosDecimal(clsTrupiFormatImporti trup, DataRow dr, out string error)
        {
            error = "";
            decimal fusha = 0;
            try
            {
                if (!trup.Visible)
                    return fusha;
                if (trup.Shfaq)
                {
                    if (dr[trup.EmerImporti].ToString() != "")
                        return Convert.ToDecimal(dr[trup.EmerImporti]);
                    if (trup.VleraDefault != "")
                        return Convert.ToDecimal(trup.VleraDefault);
                    return fusha;
                }
                if (trup.VleraDefault != "")
                    return Convert.ToDecimal(trup.VleraDefault);
            }
            catch (Exception)
            {
                error = trup.EmerImporti + " nuk eshte numer!";
            }
            return fusha;
        }

        public static double vendosDouble(clsTrupiFormatImporti trup, DataRow dr, out string error)
        {
            error = "";
            double fusha = 0;
            try
            {
                if (!trup.Visible)
                    return fusha;
                if (trup.Shfaq)
                {
                    if (dr[trup.EmerImporti].ToString() != "")
                        return Convert.ToDouble(dr[trup.EmerImporti]);
                    if (trup.VleraDefault != "")
                        return Convert.ToDouble(trup.VleraDefault);
                    return fusha;
                }
                if (trup.VleraDefault != "")
                    return Convert.ToDouble(trup.VleraDefault);
            }
            catch (Exception)
            {
                error = trup.EmerImporti + " nuk eshte numer!";
            }
            return fusha;
        }

        public static DateTime vendosDate(clsTrupiFormatImporti trup, DataRow dr, out string error, bool kontrolloOrezero)
        {
            DateTime fusha = vendosDate(trup, dr, out error);
            if (String.IsNullOrWhiteSpace(error) && !fusha.eshteDatePaOre())
            {
                error = "nuk duhet te perfshije oren! Data duhet te jete ne formatin dd/mm/yyyy!";
            }
            return fusha;
        }
        public static DateTime vendosDate(clsTrupiFormatImporti trup, DataRow dr, out string error)
        {
            error = "";
            DateTime fusha = new DateTime();
            try
            {
                if (!trup.Visible)
                    return fusha;
                if (trup.Shfaq)
                {
                    if (dr[trup.EmerImporti].ToString() != "")
                        return Convert.ToDateTime(dr[trup.EmerImporti]);
                    if (trup.VleraDefault != "")
                        return Convert.ToDateTime(trup.VleraDefault);
                    return fusha;
                }
                if (trup.VleraDefault != "")
                    return Convert.ToDateTime(trup.VleraDefault);
            }
            catch (Exception)
            {

                error = "Fusha: " + trup.EmerImporti + " nuk eshte date! Data duhet te jete ne formatin dd/mm/yyyy!";
            }
            return fusha;
        }

        public static DateTime vendosOre(clsTrupiFormatImporti trup, DataRow dr, out string error)
        {
            error = "";
            DateTime fusha = new DateTime();
            try
            {
                if (!trup.Visible)
                    return fusha;
                if (trup.Shfaq)
                {
                    if (dr[trup.EmerImporti].ToString() != "")
                        return Convert.ToDateTime(dr[trup.EmerImporti]);
                    if (trup.VleraDefault != "")
                        return Convert.ToDateTime(trup.VleraDefault);
                    return fusha;
                }
                if (trup.VleraDefault != "")
                    return Convert.ToDateTime(trup.VleraDefault);
            }
            catch (Exception)
            {
                error = "Fusha " + trup.EmerImporti + "  duhet te jete ne formatin hh:mm!";
            }
            return fusha;
        }

        public static int vendosInt(clsTrupiFormatImporti trup, DataRow dr, out string error)
        {
            error = "";
            int fusha = 0;
            try
            {
                if (!trup.Visible)
                    return fusha;
                if (trup.Shfaq)
                {
                    if (dr[trup.EmerImporti].ToString() != "")
                        return Convert.ToInt32(Convert.ToDecimal(dr[trup.EmerImporti]));
                    if (trup.VleraDefault != "")
                        return Convert.ToInt32(Convert.ToDecimal(trup.VleraDefault));
                    return fusha;
                }
                if (trup.VleraDefault != "")
                    return Convert.ToInt32(Convert.ToDecimal(trup.VleraDefault));
            }
            catch (Exception)
            {
                error = trup.EmerImporti + " nuk eshte numer!";
            }
            return fusha;
        }
        public static void ruajPeriudhatNeHiddenField(bool IsPostBack, IDictionary<string, object> hfState, string periudheDok, clsPeriudhaKontabel oPeriudha, out string datanga, out string dataderi)
        {
            string dataNgaAktuale, dataNgaViti = "";
            string dataDeriAktuale, dataDeriViti = "";
            if (!IsPostBack)
            {
                dataNgaAktuale = oPeriudha.FillimiPeriudha.ToString("dd/MM/yyyy");
                dataDeriAktuale = oPeriudha.MbarimiPeriudha.ToString("dd/MM/yyyy");
                dataNgaViti = "01/01/" + oPeriudha.MbarimiPeriudha.Year;
                dataDeriViti = "31/12/" + oPeriudha.MbarimiPeriudha.Year;
                hfState["dataNgaAktuale"] = dataNgaAktuale;
                hfState["dataDeriAktuale"] = dataDeriAktuale;
                hfState["dataNgaViti"] = dataNgaViti;
                hfState["dataDeriViti"] = dataDeriViti;
            }
            else
            {
                dataNgaAktuale = hfState["dataNgaAktuale"].ToString();
                dataDeriAktuale = hfState["dataDeriAktuale"].ToString();
                dataNgaViti = hfState["dataNgaViti"].ToString();
                dataDeriViti = hfState["dataDeriViti"].ToString();
            }
            switch (periudheDok)
            {
                case "Aktuale":
                    datanga = dataNgaAktuale;
                    dataderi = dataDeriAktuale;
                    break;

                case "Vit ushtrimor":
                    datanga = dataNgaViti;
                    dataderi = dataDeriViti;
                    break;
                case "3 Mujore":
                    datanga = Convert.ToDateTime(dataNgaAktuale).AddMonths(-2).ToString("dd/MM/yyyy");
                    dataderi = dataDeriAktuale;
                    break;
                case "Javore":
                    datanga = DateTime.Now.AddDays(-7).ToString("dd/MM/yyyy");
                    dataderi = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                case "3 Ditore":
                    datanga = DateTime.Now.AddDays(-3).ToString("dd/MM/yyyy");
                    dataderi = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                case "Ditore":
                    datanga = dataderi = DateTime.Now.ToString("dd/MM/yyyy");
                    break;

                default:
                    datanga = dataNgaAktuale;
                    dataderi = dataDeriAktuale;
                    periudheDok = "Aktuale";
                    break;
            }
            hfState["DataDokNga"] = datanga;
            hfState["DataDokDeri"] = dataderi;
            hfState["Periudha"] = periudheDok;
            hfState["periudhaDok"] = "&periudhaDok=" + periudheDok;
        }


        public static clsMesazh kontrolloEkzistenceTabelashDheSP(int idSuperKategori, string emerTabKoka, string emerTabTrupi, bool kontrolloSP, int kategoria, string emerTabRec, bool tabelaImporti, string emerTabKokaHistorik, string emerTabTrupiHistorik, string emerTabRecHistorik)
        {
            clsDatabazeImporte moduliImporte = new clsDatabazeImporte();
            clsMesazh mesazhi = kontrolloEkzistenceTabelashDheSP(moduliImporte, idSuperKategori, emerTabKoka, emerTabTrupi, kontrolloSP, kategoria, emerTabRec, tabelaImporti, emerTabKokaHistorik, emerTabTrupiHistorik, emerTabRecHistorik);
            moduliImporte.Dispose();
            return mesazhi;
        }

        public static clsMesazh kontrolloMosEkzistenceTabelashDheSP(string emerTabKoka, string emerTabTrupi, bool kontrolloSP, int kategoria, string emerTabRec, int idSuperKategori)
        {
            clsDatabazeImporte moduliImporte = new clsDatabazeImporte();
            clsMesazh mesazhi = kontrolloMosEkzistenceTabelashDheSP(moduliImporte, emerTabKoka, emerTabTrupi, kontrolloSP, kategoria, emerTabRec, idSuperKategori);
            moduliImporte.Dispose();
            return mesazhi;
        }

        /// <summary>
        /// Metode qe sherben per te kontrolluar nqs ekzistojne tabelat dhe sp me emertimet e perzgjedhura nga perdoruesi.
        /// </summary>
        /// <param name="moduliImporte">Merr nje objekt nga clsDatabazeImporte</param>
        /// <param name="emerTabKoka">Emri i tabeles se kokes</param>
        /// <param name="emerTabTrupi">Emri i tabeles se trupit</param>
        /// <param name="kontrolloSP">Variabel boolean (True per te kontrolluar dhe SP/False kontrollohen vetem tabelat)</param>
        /// <returns>Kthen nje objekt clsMesazh</returns>
        public static clsMesazh kontrolloEkzistenceTabelashDheSP(clsDatabazeImporte moduliImporte, int idSuperKategori, string emerTabKoka, string emerTabTrupi, bool kontrolloSP, int kategoria, string emerTabRec, bool tabelaImporti, string emerTabKokaHistorik, string emerTabTrupiHistorik, string emerTabRecHistorik)
        {
            try
            {
                if (moduliImporte.ekzistonTabele(emerTabKoka))
                    return new clsMesazh(true, "Tabela e kokes ekziston!");

                if (tabelaImporti && moduliImporte.ekzistonTabele(emerTabKokaHistorik))
                    return new clsMesazh(true, "Tabela e historikut te kokes ekziston!");

                if (kontrolloSP || tabelaImporti)
                {
                    if (moduliImporte.ekzistonStoredProcedure(String.Format("prc_{0}_sel", emerTabKoka)))
                        return new clsMesazh(true, "Stored Procedura e selektit te kokes ekziston!");

                    if (moduliImporte.ekzistonStoredProcedure(String.Format("prc_{0}_ins", emerTabKoka)))
                        return new clsMesazh(true, "Stored Procedura e insertit te kokes ekziston!");

                    if (tabelaImporti && moduliImporte.ekzistonStoredProcedure(String.Format("prc_{0}_upd", emerTabKoka)))
                        return new clsMesazh(true, "Stored Procedura e updatit te kokes ekziston!");
                }

                if (idSuperKategori == (int)SuperKategori.Regjistrime)
                {
                    if (moduliImporte.ekzistonTabele(emerTabTrupi))
                        return new clsMesazh(true, "Tabela e trupit ekziston!");

                    if (tabelaImporti && moduliImporte.ekzistonTabele(emerTabTrupiHistorik))
                        return new clsMesazh(true, "Tabela e historikut te trupit ekziston!");

                    if (kategoria == 45 && moduliImporte.ekzistonTabele(emerTabRec))
                        return new clsMesazh(true, "Tabela e recepturave ekziston!");

                    if (kategoria == 45 && tabelaImporti && moduliImporte.ekzistonTabele(emerTabRecHistorik))
                        return new clsMesazh(true, "Tabela e historikut te recepturave ekziston!");

                    if (kontrolloSP || tabelaImporti)
                    {
                        if (moduliImporte.ekzistonStoredProcedure(String.Format("prc_{0}_sel", emerTabTrupi)))
                            return new clsMesazh(true, "Stored Procedura e selektit te trupit ekziston!");

                        if (moduliImporte.ekzistonStoredProcedure(String.Format("prc_{0}_ins", emerTabTrupi)))
                            return new clsMesazh(true, "Stored Procedura e selektit te trupit ekziston!");

                        if (tabelaImporti && moduliImporte.ekzistonStoredProcedure(String.Format("prc_{0}_upd", emerTabTrupi)))
                            return new clsMesazh(true, "Stored Procedura e updatit te trupit ekziston!");

                        if (kategoria == 45)
                        {
                            if (moduliImporte.ekzistonStoredProcedure(String.Format("prc_{0}_sel", emerTabRec)))
                                return new clsMesazh(true, "Stored Procedura e selektit te recepturave ekziston!");

                            if (moduliImporte.ekzistonStoredProcedure(String.Format("prc_{0}_ins", emerTabRec)))
                                return new clsMesazh(true, "Stored Procedura e insertit te recepturave ekziston!");

                            if (tabelaImporti && moduliImporte.ekzistonStoredProcedure(String.Format("prc_{0}_upd", emerTabRec)))
                                return new clsMesazh(true, "Stored Procedura e updatit te recepturave ekziston!");
                        }
                    }
                }

                return new clsMesazh(false, "Kontrollet u kaluan me sukses!"); //tabelat dhe sp nuk ekzistojne
            }
            catch (Exception c)
            {
                throw new MyException("Ndodhi nje gabim gjate kontrollit te ekzistences se tabelave dinamike!", c);
            }
        }
        public static string ekzistonTabeleHistoriku(clsDatabazeImporte moduliImporte, string emerTabKokaHistorik, int nr)
        {
            string numri = nr > 0 ? $"_{nr}" : string.Empty;
            if (moduliImporte.ekzistonTabele($"{emerTabKokaHistorik}{numri}"))
                return ekzistonTabeleHistoriku(moduliImporte, emerTabKokaHistorik, nr + 1);
            return $"{emerTabKokaHistorik}{numri}";
        }

        public static clsMesazh kontrolloMosEkzistenceTabelashDheSP(clsDatabazeImporte moduliImporte, string emerTabKoka, string emerTabTrupi, bool kontrolloSP, int kategoria, string emerTabRec, int idSuperKategori)
        {
            try
            {
                bool pergjigja = moduliImporte.ekzistonTabele(emerTabKoka);
                if (!pergjigja)
                {
                    return new clsMesazh(false, "Tabela e kokes nuk ekziston!");
                }

                if (idSuperKategori == (int)SuperKategori.Regjistrime)
                {
                    pergjigja = moduliImporte.ekzistonTabele(emerTabTrupi);
                    if (!pergjigja)
                    {
                        return new clsMesazh(false, "Tabela e trupit nuk ekziston!");
                    }
                    if (kategoria == 45)
                    {
                        pergjigja = moduliImporte.ekzistonTabele(emerTabRec);
                        if (!pergjigja)
                        {
                            return new clsMesazh(false, "Tabela e recepturave nuk ekziston!");
                        }
                    }

                }

                if (kontrolloSP)
                {
                    pergjigja = moduliImporte.ekzistonStoredProcedure("prc_" + emerTabKoka + "_sel");
                    if (!pergjigja)
                    {
                        return new clsMesazh(false, "Stored Procedura e selektit te kokes nuk ekziston!");
                    }

                    pergjigja = moduliImporte.ekzistonStoredProcedure("prc_" + emerTabKoka + "_ins");
                    if (!pergjigja)
                    {
                        return new clsMesazh(false, "Stored Procedura e insertit te kokes nuk ekziston!");
                    }

                    if (idSuperKategori == (int)SuperKategori.Regjistrime)
                    {
                        pergjigja = moduliImporte.ekzistonStoredProcedure("prc_" + emerTabTrupi + "_sel");
                        if (!pergjigja)
                        {
                            return new clsMesazh(false, "Stored Procedura e selektit te trupit nuk ekziston!");
                        }

                        pergjigja = moduliImporte.ekzistonStoredProcedure("prc_" + emerTabTrupi + "_ins");
                        if (!pergjigja)
                        {
                            return new clsMesazh(false, "Stored Procedura e selektit te trupit nuk ekziston!");
                        }
                        if (kategoria == 45)
                        {
                            pergjigja = moduliImporte.ekzistonStoredProcedure("prc_" + emerTabRec + "_sel");
                            if (!pergjigja)
                            {
                                return new clsMesazh(false, "Stored Procedura e selektit te recepturave nuk ekziston!");
                            }

                            pergjigja = moduliImporte.ekzistonStoredProcedure("prc_" + emerTabRec + "_ins");
                            if (!pergjigja)
                            {
                                return new clsMesazh(false, "Stored Procedura e insertit te recepturave nuk ekziston!");
                            }
                        }
                    }
                }
                return new clsMesazh(true, "Kontrollet u kaluan me sukses!"); //tabelat dhe sp ekzistojne
            }
            catch (Exception c)
            {
                throw new MyException("Ndodhi nje gabim gjate kontrollit te mos ekzistences se tabelave dinamike!", c);
            }
        }

        /// <summary>
        /// thirret edhe nga Eksporti. Duhet pershtatur per rastin e eksportit manual
        /// </summary>
        /// <param name="col"></param>
        /// <param name="teDhenaPerEksport"></param>
        /// <param name="idKategoria"></param>
        /// <returns></returns>
        public static DataTable ktheDataTableMeKokeDokumentesh(colTrupiFormatImporti col, DataTable teDhenaPerEksport, string kodNdermarrje)
        {
            try
            {
                DataTable koka = new DataTable("koka");
                koka.Columns.Add("IDEKSPORT", (new Decimal()).GetType());

                string fushaGrupimi = "";
                foreach (clsTrupiFormatImporti trupi in col)
                {
                    if (trupi.KodKontrolli == "Kod Ndermarrje" && !trupi.Visible)
                        koka.Columns.Add(trupi.EmerImporti, typeof(string));

                    if ((trupi.Visible && trupi.FusheKokeApoTrupi == 1) || trupi.FusheKokeApoTrupi == 3)
                        fushaGrupimi += trupi.KodKontrolli + ";";

                    if ((trupi.Visible && trupi.FusheKokeApoTrupi == 1) || trupi.FusheKokeApoTrupi == 3)
                        koka.Columns.Add(trupi.EmerImporti, typeof(string));
                }

                string fushatEGrupimit = fushaGrupimi.Substring(0, fushaGrupimi.LastIndexOf(';'));//heqim pikepresjen e fundit
                string[] fushat = fushatEGrupimit.Split(';');
                DataTable dataGrupime = teDhenaPerEksport.DefaultView.ToTable(true, fushat);

                foreach (DataRow oo in dataGrupime.Rows)
                {
                    DataRow rreshti = koka.NewRow();
                    rreshti["IDEKSPORT"] = "0";
                    foreach (clsTrupiFormatImporti trupi in col)
                    {
                        if (trupi.KodKontrolli == "Kod Ndermarrje" && !trupi.Visible)
                            rreshti[trupi.EmerImporti] = kodNdermarrje;
                        if ((trupi.Visible && trupi.FusheKokeApoTrupi == 1) || trupi.FusheKokeApoTrupi == 3)
                        {
                            if (trupi.FusheType == "bit" && (oo[trupi.KodKontrolli].ToString().ToLower() == "po" || oo[trupi.KodKontrolli].ToString().ToLower() == "jo"))
                                rreshti[trupi.EmerImporti] = oo[trupi.KodKontrolli].ToString().ToLower() == "po" ? true : false;
                            else
                                rreshti[trupi.EmerImporti] = oo[trupi.KodKontrolli];

                        }
                    }
                    koka.Rows.Add(rreshti);
                }
                return koka;
            }
            catch (Exception c)
            {
                throw new MyException("Ndodhi nje gabim gjate marrjes se te dhenave per koken e dokumentit!", c);
            }
        }

        public static DataTable ktheDataTableMeProdukteProdhimi(colTrupiFormatImporti col, DataTable teDhenaPerEksport)
        {
            try
            {
                DataTable koka = new DataTable("PRODUKTPRODHIMI");
                koka.Columns.Add("IDEKSPORTTRUPI", (new Decimal()).GetType());
                string fushaGrupimi = "";
                foreach (clsTrupiFormatImporti trupi in col)
                {
                    if ((trupi.Visible && trupi.FusheKokeApoTrupi == 2) || trupi.FusheKokeApoTrupi == 3 || trupi.FusheKokeApoTrupi == 5)
                    {
                        fushaGrupimi += trupi.KodKontrolli + ";";
                        koka.Columns.Add(trupi.EmerImporti, typeof(string));
                    }
                }
                string fushatEGrupimit = fushaGrupimi.Substring(0, fushaGrupimi.LastIndexOf(';'));//heqim pikepresjen e fundit
                string[] fushat = fushatEGrupimit.Split(';');
                DataTable dataGrupime = teDhenaPerEksport.DefaultView.ToTable(true, fushat);

                foreach (DataRow oo in dataGrupime.Rows)
                {
                    DataRow rreshti = koka.NewRow();
                    rreshti["IDEKSPORTTRUPI"] = "0";
                    foreach (clsTrupiFormatImporti trupi in col)
                    {
                        if ((trupi.Visible && trupi.FusheKokeApoTrupi == 2) || trupi.FusheKokeApoTrupi == 3 || trupi.FusheKokeApoTrupi == 5)
                            rreshti[trupi.EmerImporti] = oo[trupi.KodKontrolli];
                    }
                    koka.Rows.Add(rreshti);
                }
                return koka;
            }
            catch (Exception c)
            {
                throw new MyException("Ndodhi nje gabim gjate marrjes se te dhenave per koken e dokumentit!", c);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="col"></param>
        /// <param name="teDhenaPerImport"></param>
        /// <param name="idKategoria"></param>
        /// <returns></returns>
        public static DataTable ktheDataTableMeTrupaDokumentesh(colTrupiFormatImporti col, DataTable teDhenaPerImport, string kodKontrolliTrupi, int idKategoria)
        {
            try
            {
                DataTable trupDok = new DataTable("trupi");
                if (idKategoria == 45)
                    trupDok.Columns.Add("IDEKSPORTRECEPTURA", (new Decimal()).GetType());
                else
                    trupDok.Columns.Add("IDEKSPORTTRUPI", (new Decimal()).GetType());
                foreach (clsTrupiFormatImporti trupi in col)
                {
                    if ((idKategoria == 45 && ((trupi.Visible && trupi.FusheKokeApoTrupi == 4) || trupi.FusheKokeApoTrupi == 5 || trupi.KodKontrolli == kodKontrolliTrupi)) || (idKategoria != 45 && ((trupi.Visible && trupi.FusheKokeApoTrupi == 2) || trupi.FusheKokeApoTrupi == 3 || trupi.KodKontrolli == kodKontrolliTrupi)))
                        trupDok.Columns.Add(trupi.EmerImporti, typeof(string));
                }
                foreach (DataRow oo in teDhenaPerImport.Rows)
                {
                    DataRow rreshti = trupDok.NewRow();
                    if (idKategoria == 45)
                        rreshti["IDEKSPORTRECEPTURA"] = "0";
                    else
                        rreshti["IDEKSPORTTRUPI"] = "0";
                    rreshti[kodKontrolliTrupi] = oo[kodKontrolliTrupi].ToString();
                    foreach (clsTrupiFormatImporti trupi in col)
                    {
                        if ((idKategoria != 45 && ((trupi.Visible && trupi.FusheKokeApoTrupi == 2) || trupi.FusheKokeApoTrupi == 3)) || (idKategoria == 45 && ((trupi.Visible && trupi.FusheKokeApoTrupi == 4) || trupi.FusheKokeApoTrupi == 5)))
                            rreshti[trupi.EmerImporti] = oo[trupi.KodKontrolli];
                    }
                    trupDok.Rows.Add(rreshti);
                }
                return trupDok;
            }
            catch (Exception c)
            {
                throw new MyException("Ndodhi nje gabim gjate marrjes se te dhenave per trupin e dokumentit!", c);
            }
        }

        /// <summary>
        /// Metode qe konverton nje Liste me objekte ne nje DataTable
        /// </summary>
        /// <param name="list">Lista qe do konvertohet</param>
        /// <returns>kthen Listen e konvertuar ne DataTable</returns>
        public static DataTable ConvertListToDataTable(List<object> items)
        {
            DataTable dataTable = new DataTable(typeof(object).Name);
            //Get all the properties
            PropertyInfo[] Props = typeof(object).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name);
            }
            foreach (object item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static clsMesazh importDokumenteMagazine(DataTable teDhenatPerImport, int idNdermarrje, int idPerdorues, ref DataTable gabime, ref DataTable tePaImportuara, string nenkategoria, string ndermarrjeKey, string primaryKey, colTrupiFormatImporti col, int idKategori, ResourceManager rm, CultureInfo ci, int idGjuha, string emerTabKoka, string emerTabTrupi, clsNdermarrje nderm, bool importo, bool vjenNgaImportSQL, bool importAutomatik, int idNdermVit, ref DataTable deadLocked)
        {
            var mesazh = new clsMesazh();
            var fushaSerialesh = new Dictionary<string, string>();
            foreach (var trupi in col)
            {
                vendosVlereDefaultTeDataTable(trupi, teDhenatPerImport);
                switch (trupi.KodKontrolli)
                {

                    case "Kodi i Artikullit":
                        fushaSerialesh["Kodi"] = trupi.EmerImporti;
                        break;
                    case "Seriali Unik Kryesor":
                    case "Seriali Unik Dytesor":
                    case "Artikulli Set":
                    case "Magazina":
                        if (trupi.Visible)
                            fushaSerialesh[trupi.KodKontrolli] = trupi.EmerImporti;
                        break;
                }
            }
            var fushatEGrupimit = "";


            DataTable dataGrupime;
            if (vjenNgaImportSQL)
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, primaryKey);
            else
            {
                //grupojme dokumentet qe vijne si datatable sipas Nr te dokumentit, dates se dokumentit dhe llojit te dokumentit dhe i ruajme ato tek tabela dataGrupime
                var fushaGrupimi = col.Where(trupi => trupi.Visible && trupi.Shfaq && trupi.FusheKokeApoTrupi == 1).Aggregate("", (current, trupi) => current + trupi.EmerImporti + ";");
                fushatEGrupimit = fushaGrupimi.Substring(0, fushaGrupimi.LastIndexOf(';'));//heqim pikepresjen e fundit
                var fushat = fushatEGrupimit.Split(';');
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, fushat);
            }
            var indexRreshtImporti = 1;
            using (var dbData = new DbData())
            {
                colSerialeUnikeKategori kategori = new colSerialeUnikeKategori(idNdermarrje);
                foreach (DataRow drDok in dataGrupime.Rows)
                {
                    try
                    {
                        DataTable dokumentKokTrup;
                        if (vjenNgaImportSQL)
                        {
                            dokumentKokTrup = teDhenatPerImport.Select($"[{primaryKey}] = '{drDok[primaryKey]}'").CopyToDataTable();
                            mesazh = krijoDokMagazine(dokumentKokTrup, rm, ci, idNdermarrje, idPerdorues, col, indexRreshtImporti, gabime, tePaImportuara, importo, idNdermVit, idGjuha, true, primaryKey, ndermarrjeKey, ref indexRreshtImporti, importAutomatik, emerTabKoka, new string[0], false, dbData, ref deadLocked, fushaSerialesh, kategori);
                        }
                        else
                        {
                            //krijojme nje tabele te re, ku vendosim dokumentin
                            //string[] fushat = fushatEGrupimit.Split(';');
                            var fushat = fushatEGrupimit.Split(';');
                            var selekti = fushat.Where(t => !string.IsNullOrEmpty(drDok[t].ToString())).Aggregate("", (current, t) => current + "[" + t + "] = '" + drDok[t] + "' AND ");
                            selekti += "1 = 1";
                            dokumentKokTrup = teDhenatPerImport.Select(selekti).CopyToDataTable();
                            mesazh = krijoDokMagazine(dokumentKokTrup, rm, ci, idNdermarrje, idPerdorues, col, indexRreshtImporti, gabime, tePaImportuara, importo, idNdermVit, idGjuha, false, "", "", ref indexRreshtImporti, importAutomatik, emerTabKoka, fushat, false, dbData, ref deadLocked, fushaSerialesh, kategori);
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            return !mesazh.Status
                ? new clsMesazh(false, rm.GetString("msgGabimNeImportim", ci))
                : new clsMesazh(true, rm.GetString("msgImportimMeSukses", ci));
        }

        public static clsMesazh importDokumenteShperndarjeShpenz(DataTable teDhenatPerImport, int idNdermarrje, int idPerdorues, ref DataTable gabime, ref DataTable tePaImportuara, string nenkategoria, string ndermarrjeKey, string primaryKey, colTrupiFormatImporti col, int idKategori, ResourceManager rm, CultureInfo ci, int idGjuha, string emerTabKoka, string emerTabTrupi, clsNdermarrje nderm, bool importo, bool vjenNgaImportSQL, bool importAutomatik, int idNdermVit, ref DataTable deadLocked)
        {
            var mesazh = new clsMesazh();
            var fushaSerialesh = new Dictionary<string, string>();
            foreach (var trupi in col)
                vendosVlereDefaultTeDataTable(trupi, teDhenatPerImport);

            var fushatEGrupimit = "";


            DataTable dataGrupime;
            if (vjenNgaImportSQL)
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, primaryKey);
            else
            {
                //grupojme dokumentet qe vijne si datatable sipas Nr te dokumentit, dates se dokumentit dhe llojit te dokumentit dhe i ruajme ato tek tabela dataGrupime
                var fushaGrupimi = col.Where(trupi => trupi.Visible && trupi.Shfaq && trupi.FusheKokeApoTrupi == 1).Aggregate("", (current, trupi) => current + trupi.EmerImporti + ";");
                fushatEGrupimit = fushaGrupimi.Substring(0, fushaGrupimi.LastIndexOf(';'));//heqim pikepresjen e fundit
                var fushat = fushatEGrupimit.Split(';');
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, fushat);
            }
            var indexRreshtImporti = 1;
            using (var dbData = new DbData())
            {
                foreach (DataRow drDok in dataGrupime.Rows)
                {
                    try
                    {
                        DataTable dokumentKokTrup;
                        if (vjenNgaImportSQL)
                        {
                            dokumentKokTrup = teDhenatPerImport.Select($"[{primaryKey}] = '{drDok[primaryKey]}'").CopyToDataTable();
                            mesazh = krijoDokShperndarjeShpenz(dokumentKokTrup, rm, ci, idNdermarrje, idPerdorues, col, ref indexRreshtImporti, ref gabime, ref tePaImportuara, importo, idNdermVit, idGjuha, true, primaryKey, ndermarrjeKey, importAutomatik, emerTabKoka, new string[0], false, dbData, ref deadLocked);
                        }
                        else
                        {
                            //krijojme nje tabele te re, ku vendosim dokumentin
                            //string[] fushat = fushatEGrupimit.Split(';');
                            var fushat = fushatEGrupimit.Split(';');
                            var selekti = fushat.Where(t => !string.IsNullOrEmpty(drDok[t].ToString())).Aggregate("", (current, t) => current + "[" + t + "] = '" + drDok[t] + "' AND ");
                            selekti += "1 = 1";
                            dokumentKokTrup = teDhenatPerImport.Select(selekti).CopyToDataTable();
                            mesazh = krijoDokShperndarjeShpenz(dokumentKokTrup, rm, ci, idNdermarrje, idPerdorues, col, ref indexRreshtImporti, ref gabime, ref tePaImportuara, importo, idNdermVit, idGjuha, false, "", "", importAutomatik, emerTabKoka, fushat, false, dbData, ref deadLocked);
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            return !mesazh.Status
                ? new clsMesazh(false, rm.GetString("msgGabimNeImportim", ci))
                : new clsMesazh(true, rm.GetString("msgImportimMeSukses", ci));
        }

        private static clsMesazh krijoDokShperndarjeShpenz(DataTable dokTable, ResourceManager rm, CultureInfo ci, int idNdermarrje, int idPerdorues, colTrupiFormatImporti col, ref int indexRreshtImporti, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int idNdermVit, int idGjuha, bool vjenNgaImportSQL, string primaryKey, string ndermarrjeKey, bool importAutomatik, string emerTabKoka, string[] fushat, bool ruajRenditje, DbData dbData, ref DataTable deadLocked)
        {
            if (dokTable == null)
                return new clsMesazh(false);
            string error = "";
            string nrDokumentiEmerImport = "";
            string dtDokumentiEmerImport = "";
            try
            {
                nrDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Nr dokumenti").EmerImporti;

                clsMesazh mesazh = new clsMesazh(true);
                string llojDok = String.Empty, nrDok = String.Empty, shenime = String.Empty;

                error = "";

                #region Fushat e kokes
                foreach (clsTrupiFormatImporti trup in col)
                {
                    error = "";
                    switch (trup.KodKontrolli)
                    {
                        case "Lloji":
                            llojDok = DbCore.clsFunksione.ktheStringunPaHapesira(DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error), true);
                            break;
                        case "Nr dokumenti":
                            nrDok = DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Pershkrimi":
                            shenime = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                    }
                    if (error != "")
                    {
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                            msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " :" + trup.EmerImporti + " duhet te jete numer!";
                        else msgGabimi = error;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, 7);
                        continue;
                    }
                }
                #endregion

                clsDatabaseShare dbShare = new clsDatabaseShare(dbData);
                clsKonfigurimAmbjenti konfigAmbjenti = new clsKonfigurimAmbjenti(llojDok, idNdermarrje, dbShare);
                if (MbylljePeriudhe.PeriodClosing.IsPeriodClosed(DateTime.Today, dbData.MyScopeDbManager.ConnectionName, idNdermarrje, KategoriDokumenti.ShperndarjeShpenzimesh, konfigAmbjenti.IdKonfigAmbjente))
                    throw new Exception(MessagesResource.Messages["msgPeriodIsClosed"]);

                clsShperndarjeShpenzimeKoka koka = new clsShperndarjeShpenzimeKoka();
                if (clsShperndarjeShpenzimeKoka.ekzistonDokumentShperndarjeShpenzimi(nrDok, DateTime.Today, idNdermarrje, idNdermVit, new clsDatabaseRegjistrim()))
                    throw new MyException(MessagesResource.Messages["msgEkziston1DokShperndarjeShpenzimeshMeKetoTeDhena"]);
                clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbData);
                bool kaAutorizim = clsKonfigurimAmbjenti.getAutorizimKonfigurimi(konfigAmbjenti.IdKonfigAmbjente, idPerdorues, dbShare);
                if (!kaAutorizim)
                    throw new Exception($"Perdoruesi nuk ka autorizim per llojin e dokumentit {konfigAmbjenti.KodKonfigAmbjente}.");

                Tuple<colShperndarjeShpenzimeTrupi, colShperndarjeShpenzimeLlogarite, colShperndarjeShpenzimeFaturat> trupi = krijoTrupDokShperndarjeShpenz(dokTable, idNdermarrje, idPerdorues, col, ref gabime, ref tePaImportuara, importo, nrDokumentiEmerImport, dtDokumentiEmerImport, ref indexRreshtImporti, vjenNgaImportSQL, 7);

                double vleraTotal = trupi.Item2.Sum(x => x.Vlefta);
                int statusDok = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "SDI").ToLower() == "draft" ? 0 : 1;
                koka = new clsShperndarjeShpenzimeKoka(0, nrDok, DateTime.Today, DateTime.Today, shenime, vleraTotal, statusDok, idNdermarrje, idNdermVit, idPerdorues, konfigAmbjenti.IdNivel, konfigAmbjenti.IdKonfigAmbjente, 0, 0, 0, 0);
                koka.OColTrupi = trupi.Item1;
                koka.OColLlogarite = trupi.Item2;
                koka.OColFaturat = trupi.Item3;

                clsKonfigurimAmbjenti konfmag = new DbCore.DbShare.clsKonfigurimAmbjenti(konfigAmbjenti.IdKonfigurimi);
                clsKonfigurimAmbjenti konfamortizimi = new clsKonfigurimAmbjenti();
                DbCore.DbShare.clsKusht kusht = new DbCore.DbShare.clsKusht(konfmag.IdKonfigAmbjente, "ZDAM");
                konfamortizimi.mbushKonfigAmbjSipasId(kusht.Vlera, idGjuha);

                string gjeneroKontablizimi = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "GJK");

                bool gjithmone = false;
                if (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "GJKGJ") == "Po")
                    gjithmone = true;

                bool mekontabilizim = (gjeneroKontablizimi.ToLower() != "jo" && koka.IdStatusDok == 1);

                string shfaqmesazhapolupe = "";
                koka.OColKokaMag = new colKokaMagazina();
                koka.OColFletetKontabel = new colKokatFletetKontabel();
                mesazh = koka.krijoMagazineNgaShperndarjeShpenzimesh(koka, konfmag, mekontabilizim, out shfaqmesazhapolupe, koka.IdKokaShperndarjeShpenz, gjithmone);

                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);

                if (importo)
                {
                    if (!kontrolloPerGabime(vjenNgaImportSQL, fushat, primaryKey, tePaImportuara, dokTable))
                        return new clsMesazh(false, "Ka gabime!");

                    mesazh = koka.ruajMeNrAuto(mekontabilizim, konfamortizimi);

                    if (!mesazh.Status)
                    {
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        msgGabimi = mesazh.PershkrimMesazhi;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, 7);
                    }
                }
                return mesazh;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                if (error == "")
                    error = "Gabim gjate krijimit te dokumentit!";
                string msgGabimi = "";
                msgGabimi = error;
                shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, 7);
                return new clsMesazh(false, error);
            }
        }

        private static Tuple<colShperndarjeShpenzimeTrupi, colShperndarjeShpenzimeLlogarite, colShperndarjeShpenzimeFaturat> krijoTrupDokShperndarjeShpenz(DataTable dokTrupi, int idNdermarrje, int idPerdorues, colTrupiFormatImporti col, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, string nrDokumentiEmerImport, string dtDokumentiEmerImport, ref int indexRreshtImporti, bool vjenNgaImportSQL, int kategoria)
        {
            List<Dictionary<string, object>> llogTotVleraShpenz = new List<Dictionary<string, object>>();
            bool analitike = col.FirstOrDefault(x => x.KodKontrolli == "Kod artikulli").Visible;
            colShperndarjeShpenzimeFaturat colFaturat = new colShperndarjeShpenzimeFaturat();

            colShperndarjeShpenzimeLlogarite colLlogarite = krijoTrupLlogariShperndarjeShpenz(dokTrupi, idNdermarrje, col, ref gabime, ref tePaImportuara, importo, nrDokumentiEmerImport, dtDokumentiEmerImport, ref indexRreshtImporti, vjenNgaImportSQL, kategoria);

            colShperndarjeShpenzimeTrupi colTrupi = krijoTrupDokHyrjeShperndarjeShpenz(dokTrupi, idNdermarrje, col, ref gabime, ref tePaImportuara, importo, nrDokumentiEmerImport, dtDokumentiEmerImport, ref indexRreshtImporti, vjenNgaImportSQL, kategoria, ref colFaturat, ref llogTotVleraShpenz, colLlogarite, analitike);


            if (!analitike)
            {
                double totaliLlogarive = colLlogarite.Sum(x => x.Vlefta);
                double totaliHyrjeve = colTrupi.Sum(x => x.OColTrupiFaturat.Sum(y => y.Vlera));
                double koeficenti = totaliLlogarive / totaliHyrjeve;
                colTrupi.ForEach(x => x.OColTrupiFaturat.ForEach(y => y.Vlera = y.Vlera * koeficenti));
            }
            else
            {
                foreach (Dictionary<string, object> item in llogTotVleraShpenz)
                {
                    double vleraLlogarise = colLlogarite.FindAll(x => x.NrLlogari == item["Llogaria"].ToString()).Sum(x => x.Vlefta);
                    double vleraShpenz = Convert.ToDouble(item["TotVleraShpenz"]);
                    if (Math.Round(vleraLlogarise, 5) != Math.Round(vleraShpenz, 5))
                        throw new MyException($"Vlerat e shperndara per llogarine {item["Llogaria"].ToString()} nuk jane te barabarta");
                }
            }
            return new Tuple<colShperndarjeShpenzimeTrupi, colShperndarjeShpenzimeLlogarite, colShperndarjeShpenzimeFaturat>(colTrupi, colLlogarite, colFaturat);
        }

        private static colShperndarjeShpenzimeLlogarite krijoTrupLlogariShperndarjeShpenz(DataTable dokTrupi, int idNdermarrje, colTrupiFormatImporti col, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, string nrDokumentiEmerImport, string dtDokumentiEmerImport, ref int indexRreshtImporti, bool vjenNgaImportSQL, int kategoria)
        {
            int j = 1;
            colShperndarjeShpenzimeLlogarite colLlogarite = new colShperndarjeShpenzimeLlogarite();
            //grupojme dokumentet e hyrjeve qe vijne si datatable sipas Nr te dokumentit, dates se dokumentit dhe llojit te dokumentit dhe i ruajme ato tek tabela dataGrupimeTrupi
            var fushaGrupimiLlogarite = col.Where(trupi => trupi.Visible && trupi.Shfaq && (trupi.FusheKokeApoTrupi == 1 || trupi.FusheKokeApoTrupi == 5)).Aggregate("", (current, trupi) => current + trupi.EmerImporti + ";");
            string[] fushatEGrupimitLlogarive = (fushaGrupimiLlogarite.Substring(0, fushaGrupimiLlogarite.LastIndexOf(';'))).Split(';');
            DataTable dataGrupimeLlogarite = dokTrupi.DefaultView.ToTable(true, fushatEGrupimitLlogarive);

            bool analitike = col.FirstOrDefault(x => x.KodKontrolli == "Kod artikulli").Visible;
            string error = "";

            foreach (DataRow dr in dataGrupimeLlogarite.Rows)
            {
                try
                {
                    error = "";

                    clsShperndarjeShpenzimeLlogarite llogaria = new clsShperndarjeShpenzimeLlogarite();
                    string nrLlogaria = string.Empty;
                    double vleftaLlog = 0;

                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        #region VendosVlereFushaTrupi
                        switch (trup.KodKontrolli)
                        {
                            case "Llogaria":
                                nrLlogaria = DbCore.clsFunksione.ktheStringunPaHapesira(vendosVlere(trup, dr, out error), true);
                                break;
                            case "Vlefta e llogarise":
                                vleftaLlog = vendosDouble(trup, dr, out error);
                                break;
                        }

                        #endregion
                        if (error != "")
                        {
                            DateTime datedok = new DateTime();
                            string msgGabimi = "";
                            msgGabimi = error;
                            shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, kategoria, dr);
                            continue;
                        }
                    }

                    if (error == "")
                    {

                        clsLlogari llogariObj = new clsLlogari(nrLlogaria, idNdermarrje);
                        if (llogariObj.IdLlogari <= 0)
                            throw new MyException($"Llogaria {nrLlogaria} nuk ekziston!");
                        llogaria = new clsShperndarjeShpenzimeLlogarite(0, 0, llogariObj.IdLlogari, vleftaLlog);
                        llogaria.NrLlogari = llogariObj.NrLlogari;
                        colLlogarite.Add(llogaria);
                        indexRreshtImporti++;
                        j++;
                    }
                }
                catch (MyException ex)
                {
                    DateTime datedok = new DateTime();
                    string msgGabimi = "";
                    indexRreshtImporti++;
                    j++;
                    msgGabimi = ex.Message;
                    shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, kategoria, dr);
                    continue;
                }
            }
            return colLlogarite;
        }

        private static colShperndarjeShpenzimeTrupi krijoTrupDokHyrjeShperndarjeShpenz(DataTable dokTrupi, int idNdermarrje, colTrupiFormatImporti col, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, string nrDokumentiEmerImport, string dtDokumentiEmerImport, ref int indexRreshtImporti, bool vjenNgaImportSQL, int kategoria, ref colShperndarjeShpenzimeFaturat colFaturat, ref List<Dictionary<string, object>> llogTotVleraShpenz, colShperndarjeShpenzimeLlogarite llogarite, bool analitike)
        {
            int j = 1;
            colShperndarjeShpenzimeTrupi colTrupi = new colShperndarjeShpenzimeTrupi();
            //grupojme dokumentet e hyrjeve qe vijne si datatable sipas Nr te dokumentit, dates se dokumentit dhe llojit te dokumentit dhe i ruajme ato tek tabela dataGrupimeTrupi
            var fushaGrupimiDokHyrje = col.Where(trupi => trupi.Visible && trupi.Shfaq && (trupi.FusheKokeApoTrupi == 1 || trupi.FusheKokeApoTrupi == 2)).Aggregate("", (current, trupi) => current + trupi.EmerImporti + ";");
            string[] fushatEGrupimitDokHyrje = (fushaGrupimiDokHyrje.Substring(0, fushaGrupimiDokHyrje.LastIndexOf(';'))).Split(';');
            DataTable dataGrupimeDokHyrje = dokTrupi.DefaultView.ToTable(true, fushatEGrupimitDokHyrje);

            string error = "";

            foreach (DataRow dr in dataGrupimeDokHyrje.Rows)
            {
                try
                {
                    error = "";

                    clsShperndarjeShpenzimeTrupi trupi = new clsShperndarjeShpenzimeTrupi();
                    string llojDokHyrje = string.Empty;
                    string nrDokHyrje = string.Empty;
                    DateTime dtDokHyrje = new DateTime();
                    List<Dictionary<string, object>> artikullVlerShpenz = new List<Dictionary<string, object>>();

                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        #region VendosVlereFushaTrupi
                        switch (trup.KodKontrolli)
                        {
                            case "Lloj dok hyrje":
                                llojDokHyrje = DbCore.clsFunksione.ktheStringunPaHapesira(vendosVlere(trup, dr, out error), true);
                                break;
                            case "Nr dok hyrje":
                                nrDokHyrje = DbCore.clsFunksione.ktheStringunPaHapesira(vendosVlere(trup, dr, out error), true);
                                break;
                            case "Date dok hyrje":
                                dtDokHyrje = vendosDate(trup, dr, out error);
                                break;
                        }

                        #endregion
                        if (error != "")
                        {
                            DateTime datedok = new DateTime();
                            string msgGabimi = "";
                            msgGabimi = error;
                            shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, kategoria, dr);
                            continue;
                        }
                    }

                    DataTable dokumentTrupiFatura = dokTrupi.Select($"[Lloj dok hyrje] = '{llojDokHyrje}' AND Convert([Date dok hyrje],'System.String') = '{dtDokHyrje.ToString()}' AND [Nr dok hyrje] = '{nrDokHyrje}'").CopyToDataTable();
                    dtDokHyrje = dtDokHyrje.Date;

                    if (analitike)
                    {
                        foreach (DataRow rowTrupiFatura in dokumentTrupiFatura.Rows)
                        {
                            clsShperndarjeShpenzimeTrupiFaturat trupiFaturat = new clsShperndarjeShpenzimeTrupiFaturat();
                            string kodArtikulli = string.Empty;
                            decimal vleraShpenz = 0;
                            string magazina = string.Empty;
                            string llogaria = string.Empty;
                            foreach (clsTrupiFormatImporti trupF in col)
                            {
                                switch (trupF.KodKontrolli)
                                {
                                    case "Magazina":
                                        magazina = DbCore.clsFunksione.ktheStringunPaHapesira(vendosVlere(trupF, rowTrupiFatura, out error), true);
                                        break;
                                    case "Kod artikulli":
                                        kodArtikulli = DbCore.clsFunksione.ktheStringunPaHapesira(vendosVlere(trupF, rowTrupiFatura, out error), true);
                                        break;
                                    case "Vlere shpenzimi":
                                        vleraShpenz = vendosDecimal(trupF, rowTrupiFatura, out error);
                                        break;
                                    case "Llogaria":
                                        llogaria = DbCore.clsFunksione.ktheStringunPaHapesira(vendosVlere(trupF, rowTrupiFatura, out error), true);
                                        break;
                                }
                            }
                            artikullVlerShpenz.Add(new Dictionary<string, object>() { { "Magazina", magazina }, { "KodArtikulli", kodArtikulli }, { "VleraShpenz", vleraShpenz }, { "Llogaria", llogaria } });
                        }
                    }
                    else
                    {
                        if (dokumentTrupiFatura.Rows.Count != llogarite.Count)
                            throw new MyException("Vlerat e llogarive nuk jane te sakta");
                    }

                    clsMesazh mesazh = new clsMesazh();
                    if (error == "")
                    {
                        mesazh = trupi.krijoTrupShperndarjeShpenzNgaImporti(idNdermarrje, llojDokHyrje, nrDokHyrje, dtDokHyrje, artikullVlerShpenz, ref llogTotVleraShpenz);
                        if (!mesazh)
                            throw new MyException(mesazh.PershkrimMesazhi);
                        colFaturat.Add(new clsShperndarjeShpenzimeFaturat(0, 0, trupi.IdFatura));
                    }

                    if (mesazh.Status)
                    {
                        colTrupi.Add(trupi);
                        indexRreshtImporti++;
                        j++;
                    }
                    else
                        throw new MyException(mesazh.PershkrimMesazhi);
                }
                catch (MyException ex)
                {
                    DateTime datedok = new DateTime();
                    string msgGabimi = "";
                    indexRreshtImporti++;
                    j++;
                    msgGabimi = ex.Message;
                    shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, kategoria, dr);
                    continue;
                }
            }
            return colTrupi;
        }

        public static void ImportFleteKontabel(DataTable teDhenatPerImport, int idNdermarrje, int idPerdorues, ref DataTable gabime, ref DataTable tePaImportuara, string nenkategoria, string ndermarrjeKey, string primaryKey, colTrupiFormatImporti col, int idKategori, int idGjuha, string emerTabKoka, string emerTabTrupi, clsNdermarrje nderm, bool importo, bool vjenNgaImportSql, bool importAutomatik, int idNdermVit)
        {
            foreach (var trupi in col)
                vendosVlereDefaultTeDataTable(trupi, teDhenatPerImport);
            var fushatEGrupimit = "";

            DataTable dataGrupime;
            if (vjenNgaImportSql)
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, primaryKey);
            else
            {
                var fushaGrupimi = col.Where(trupi => trupi.Visible && trupi.Shfaq && trupi.FusheKokeApoTrupi == 1).Aggregate("", (current, trupi) => current + trupi.EmerImporti + ";");
                fushatEGrupimit = fushaGrupimi.Substring(0, fushaGrupimi.LastIndexOf(';'));
                var fushat = fushatEGrupimit.Split(';');
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, fushat);
            }
            var indexRreshtImporti = 1;
            using (var dbData = new DbData())
            {
                foreach (DataRow drDok in dataGrupime.Rows)
                {
                    try
                    {
                        DataTable dokumentKokTrup;
                        if (vjenNgaImportSql)
                        {
                            dokumentKokTrup = teDhenatPerImport.Select($"[{primaryKey}] = '{drDok[primaryKey]}'").CopyToDataTable();
                            KrijoFleteKontabel(dokumentKokTrup, idNdermarrje, idPerdorues, col,
                                 gabime, tePaImportuara, importo, idNdermVit, idGjuha, true,
                                primaryKey, ref indexRreshtImporti, new string[0], dbData);
                        }
                        else
                        {
                            var fushat = fushatEGrupimit.Split(';');
                            var selekti = fushat.Where(t => !string.IsNullOrEmpty(drDok[t].ToString())).Aggregate("",
                                (current, t) => current + "[" + t + "] = '" + drDok[t] + "' AND ");
                            selekti += "1 = 1";
                            dokumentKokTrup = teDhenatPerImport.Select(selekti).CopyToDataTable();
                            KrijoFleteKontabel(dokumentKokTrup, idNdermarrje, idPerdorues, col, gabime, tePaImportuara, importo, idNdermVit, idGjuha, false, "",
                                ref indexRreshtImporti, new string[0], dbData);
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
        }

        private static void KrijoFleteKontabel(DataTable dokTable, int idNdermarrje, int idPerdorues, colTrupiFormatImporti col, DataTable gabime, DataTable tePaImportuara, bool importo, int idNdermVit, int idGjuha, bool vjenNgaImportSql, string primaryKey, ref int indexRreshtImporti, string[] fushat, DbData dbData)
        {
            if (dokTable == null)
                return;
            var nrDokumentiEmerImport = string.Empty;
            var dtDokumentiEmerImport = string.Empty;
            var error = string.Empty;
            try
            {
                var shfaqmesazhapolupe = "jo";
                nrDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Nr Dokumenti").EmerImporti;
                dtDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Date Dokumenti").EmerImporti;
                var dtDok = new DateTime();
                string lloji = string.Empty,
                    nrDokumenti = string.Empty,
                    nrReference = string.Empty,
                    pershkrimi = string.Empty;

                foreach (var trup in col)
                {
                    error = string.Empty;
                    switch (trup.KodKontrolli)
                    {
                        case "Lloji":
                            lloji = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Nr Dokumenti":
                            nrDokumenti = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Date Dokumenti":
                            dtDok = vendosDate(trup, dokTable.Rows[0], out error, true);
                            if (dtDok == DateTime.MinValue)
                                dtDok = DateTime.Today;
                            break;
                        case "Nr Reference":
                            nrReference = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Pershkrimi":
                            pershkrimi = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                    }
                    if (error == "") continue;
                    var datedok = new DateTime();
                    var dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                    var msgGabimi = dateVlefshme
                        ? "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " :" +
                          trup.EmerImporti + " " + error
                        : error;
                    shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSql, dokTable, 6);
                }
                var koka = new clsKokaFleteKontabel();
                var colTrupi = new colTrupatFletetKontabel();
                var dbShare = new clsDatabaseShare(dbData);
                var konfigAmbjenti = new clsKonfigurimAmbjenti(lloji, idNdermarrje, dbShare);
                if (konfigAmbjenti.IdKonfigAmbjente == 0)
                    throw new Exception($"Lloji i dokumentit '{lloji}' nuk ekziston.");

                koka.EkzistonFleteKontabel(konfigAmbjenti.KodKonfigAmbjente, nrDokumenti, dtDok, idNdermVit);

                var kaAutorizim = clsKonfigurimAmbjenti.getAutorizimKonfigurimi(konfigAmbjenti.IdKonfigAmbjente, idPerdorues, dbShare);
                if (!kaAutorizim)
                    throw new Exception($"Perdoruesi nuk ka autorizim per llojin e dokumentit {konfigAmbjenti.KodKonfigAmbjente}.");
                var per = new clsPeriudhaKontabel(dtDok, idNdermarrje);

                var alternativa = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "LLK");
                var llojKursi = 1;
                if (alternativa != "")
                    llojKursi = int.Parse(alternativa.Substring(alternativa.Length - 1));

                var idMonNdermarrje = clsNdermarrje.ktheIdMonedheNdermSipasID(idNdermarrje, new clsDatabaseAdmin(new clsDatabaseKontabilitet()));
                colTrupi = KrijoTrupiFleteKontabel(dokTable, col, gabime, tePaImportuara, importo,
                    dtDokumentiEmerImport, ref indexRreshtImporti, vjenNgaImportSql, nrDokumentiEmerImport,
                    idNdermarrje, dtDok, clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "LLFK") == "Azhornim", idMonNdermarrje, llojKursi);
                var kokaQendraKosto = clsKokaQendraKosto.KrijoQenderRe(konfigAmbjenti, colTrupi, new colObjektivaKosto(), new List<double>(), new List<double>(), new List<int>(), 1, out shfaqmesazhapolupe, idNdermarrje, idGjuha, "shtim", 0, pershkrimi, dtDok, DateTime.Today, idNdermVit, idPerdorues, nrDokumenti);

                var mesazh = koka.KrijoFleteKontPerImportFk(dbData.MyScopeDbManager.ConnectionName, idNdermVit, nrDokumenti, nrReference, dtDok, DateTime.Today,
                    konfigAmbjenti.KodKonfigAmbjente, 0, pershkrimi, idPerdorues, colTrupi, true, 5, 5, per.IdPeriudha,
                    idNdermarrje, kokaQendraKosto);
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);

                if (importo)
                {
                    if (!kontrolloPerGabime(vjenNgaImportSql, fushat, primaryKey, tePaImportuara, dokTable))
                        return;
                    mesazh = koka.Ruaj(null, new clsDatabaseKontabilitet());
                    if (!mesazh.Status)
                    {
                        DateTime datedok = new DateTime();
                        var dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(),
                            out datedok);
                        var msgGabimi = dateVlefshme
                            ? "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ": " +
                              mesazh.PershkrimMesazhi
                            : mesazh.PershkrimMesazhi;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, true,
                            msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSql, dokTable,
                            5);
                    }
                }

            }
            catch (Exception ex)
            {
                error = ex.Message;
                if (error == "")
                    error = "Gabim gjate krijimit te dokumentit!";
                var datedok = new DateTime();
                var dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                var msgGabimi = dateVlefshme
                    ? "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " " + error
                    : error;
                shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSql, dokTable, 5);
            }
        }

        private static colTrupatFletetKontabel KrijoTrupiFleteKontabel(DataTable dokTrupi, colTrupiFormatImporti col, DataTable gabime, DataTable tePaImportuara, bool importo, string dtDokumentiEmerImport, ref int indexRreshtImporti, bool vjenNgaImportSQL, string nrDokumentiEmerImport, int idNdermarrje, DateTime dateDok, bool azhornim, int idMonNdermarrje, int llojKursi)
        {
            var j = 1;
            var colTrupi = new colTrupatFletetKontabel();

            foreach (DataRow dr in dokTrupi.Rows)
            {
                try
                {
                    string nrLlogari = string.Empty, monedha = string.Empty, pershkrimiTrupi = string.Empty;
                    double vlefteMonDebi = 0,
                        vlefteMonKredi = 0,
                        kursi = 0,
                        vleftaDebi = 0,
                        vleftaKredi = 0;

                    foreach (var trup in col)
                    {
                        var error = string.Empty;
                        switch (trup.KodKontrolli)
                        {
                            case "Nr Llogari":
                                nrLlogari = vendosVlere(trup, dr, out error);
                                break;
                            case "Monedha":
                                monedha = vendosVlere(trup, dr, out error);
                                break;
                            case "Vlefte Mon Debi":
                                vlefteMonDebi = vendosDouble(trup, dr, out error);
                                break;
                            case "Vlefte Mon Kredi":
                                vlefteMonKredi = vendosDouble(trup, dr, out error);
                                break;
                            case "Kursi":
                                kursi = vendosDouble(trup, dr, out error);
                                break;
                            case "Vlefta Debi":
                                vleftaDebi = vendosDouble(trup, dr, out error);
                                break;
                            case "Vlefta Kredi":
                                vleftaKredi = vendosDouble(trup, dr, out error);
                                break;
                            case "Pershkrim Trupi":
                                pershkrimiTrupi = vendosVlere(trup, dr, out error);
                                break;
                        }
                        if (error == "") continue;
                        var datedok = new DateTime();
                        var dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                        var msgGabimi = dateVlefshme
                            ? "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + " :" + trup.EmerImporti + " duhet te jete numer!"
                            : error;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, 5, dr);
                    }

                    if (vlefteMonDebi == 0 && vlefteMonKredi == 0 && vleftaDebi == 0 && vleftaKredi == 0)
                        throw new Exception("Plotesoni te pakten nje nga vleftat!");

                    var trupi = new clsTrupiFleteKontabel(idNdermarrje, azhornim, idMonNdermarrje, nrLlogari,
                        "", pershkrimiTrupi, monedha, kursi.ToString(), vleftaDebi.ToString(), vleftaKredi.ToString(),
                        vlefteMonDebi.ToString(), vlefteMonKredi.ToString(), dateDok, llojKursi);

                    colTrupi.Add(trupi);
                    indexRreshtImporti++;
                    j++;
                }
                catch (MyException ex)
                {
                    var datedok = new DateTime();
                    indexRreshtImporti++; j++;
                    var dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                    var msgGabimi = dateVlefshme
                        ? "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + ": " + ex.Message
                        : ex.Message;
                    shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, 5, dr);
                }
            }
            return colTrupi;
        }

        public static clsMesazh krijoDokMagazine(DataTable dokTable, ResourceManager rm, CultureInfo ci, int idNdermarrje, int idPerdorues, colTrupiFormatImporti col, int pozicionkodi, DataTable gabime, DataTable tePaImportuara, bool importo, int idNdermVit, int idGjuha, bool vjenNgaImportSQL, string primaryKey, string ndermarrjeKey, ref int indexRreshtImporti, bool importAutomatik, string emerTabKoka, string[] fushat, bool ruajrenditje, DbData dbData, ref DataTable deadLocked, Dictionary<string, string> fushaSerialesh, colSerialeUnikeKategori kategori)
        {
            if (dokTable == null)
                return new clsMesazh(false);
            string error = "";
            string nrDokumentiEmerImport = "";
            string dtDokumentiEmerImport = "";
            string mesazhmevonshem = "";
            bool kontrolloGjendje = !importo;
            try
            {
                nrDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Nr Dokumenti").EmerImporti;
                dtDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Date Dokumenti").EmerImporti;

                clsMesazh mesazh = new clsMesazh(true);

                string nenkategoria = "", llojDokumenti = "", nrSerial = "", nrDok = "", klientFurnitor = "", pershkrimi = "", shenime = "", degeAdministrative = "", adresa = "", grupimdok1 = "", grupimdok2 = "",
                    grupimdok3 = "", magazinieri = "", llogari = "", njesivartese = "", automjeti = "", nrProjekti = "", krijuesi = "", kodbari = "", Nivfsh = "", Wtnic = "";
                DateTime dtDok = new DateTime();
                error = "";
                int idNivelGjeneruesi = 0, idKonfigGjeneruesi = 0, idGjeneruesi = 0, idDokNga = 0;
                bool hyrje_dalje = false, mekonfirmimKokaMag = false;

                #region Fushat e kokes
                foreach (clsTrupiFormatImporti trup in col)
                {
                    error = "";
                    switch (trup.KodKontrolli)
                    {
                        case "Nenkategoria":
                            nenkategoria = vendosVlere(trup, dokTable.Rows[0], out error);
                            hyrje_dalje = (nenkategoria.ToUpperInvariant() == "FH" || nenkategoria.ToUpperInvariant() == "UH");
                            break;

                        case "Lloj Dokumenti":
                            llojDokumenti = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Nr Dokumenti":
                            nrDok = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Klient/Furnitori":
                            klientFurnitor = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Pershkrimi":
                            pershkrimi = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Shenime":
                            shenime = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Dege Administrative":
                            degeAdministrative = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Adresa":
                            adresa = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Grupim Dok Magazine 1":
                            grupimdok1 = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Grupim Dok Magazine 2":
                            grupimdok2 = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Grupim Dok Magazine 3":
                            grupimdok3 = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Magazinieri":
                            magazinieri = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Date Dokumenti":
                            dtDok = vendosDate(trup, dokTable.Rows[0], out error, true);
                            if (dtDok == DateTime.MinValue)
                                dtDok = DateTime.Today;
                            break;

                        case "Llogari":
                            llogari = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Njesi Vartese":
                            njesivartese = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Me Konfirmim":
                            mekonfirmimKokaMag = vendosBool(trup, dokTable.Rows[0], out error);
                            break;

                        case "Automjeti":
                            automjeti = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Numer Projekti":
                            nrProjekti = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Krijuesi":
                            krijuesi = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Kodbari":
                            kodbari = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Id Nivel Gjenerues":
                            idNivelGjeneruesi = vendosInt(trup, dokTable.Rows[0], out error);
                            break;

                        case "Id Konfig Gjenerues":
                            idKonfigGjeneruesi = vendosInt(trup, dokTable.Rows[0], out error);
                            break;

                        case "Id Gjenerues":
                            idGjeneruesi = vendosInt(trup, dokTable.Rows[0], out error);
                            break;

                        case "Id Dok Nga":
                            idDokNga = vendosInt(trup, dokTable.Rows[0], out error);
                            break;

                        case "Numri Serial":
                            nrSerial = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "NIVFSH":
                            Nivfsh = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "WTNIC":
                            Wtnic = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                    }
                    if (error != "")
                    {
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                            msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " :" + trup.EmerImporti + " " + error;
                        else msgGabimi = error;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, 6);
                    }
                }

                #endregion

                clsKokaMagazina koka = new clsKokaMagazina();
                colTrupiMagazina colTrupi = new colTrupiMagazina();
                colSerialeUnikeMagazina serialeUnike = null;
                clsKokaMagazina kokaDest = new clsKokaMagazina();
                colTrupiMagazina colTrupiDest = new colTrupiMagazina();
                clsDatabaseShare dbShare = new clsDatabaseShare(dbData);
                clsDatabaseAdmin dbA = new clsDatabaseAdmin(dbData);
                clsKonfigurimAmbjenti konfigAmbjenti = new clsKonfigurimAmbjenti(llojDokumenti, idNdermarrje, dbShare);

                bool kaAutorizim = clsKonfigurimAmbjenti.getAutorizimKonfigurimi(konfigAmbjenti.IdKonfigAmbjente, idPerdorues, dbShare);
                if (!kaAutorizim)
                    throw new Exception($"Perdoruesi nuk ka autorizim per llojin e dokumentit {konfigAmbjenti.KodKonfigAmbjente}.");

                bool meSerialeUnike = fushaSerialesh.Values.ToList().ContainsAny("Seriali Unik Kryesor", "Seriali Unik Dytesor");
                bool transferim = false, meKonfirmim = false;

                if (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "DMT", dbShare) == "Po")
                    transferim = true;

                bool isMagENjejte = true;
                if (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "DOKMEKONF", dbShare) == "Po")
                    meKonfirmim = true;
                colSerialetMagazine colSerialet = new colSerialetMagazine();
                if (!clsViti.ekzistonVitPerNdermarrjen(dtDok.Year.ToString(), idNdermarrje))
                    throw new MyException("Ky vit nuk ekziston!");
                clsPeriudhaKontabel per = new clsPeriudhaKontabel(dtDok, idNdermarrje);
                if (per.IdPeriudha == 0)
                    throw new MyException("Periudha nuk ekziston!");
                if (per.Ekycur)
                    throw new MyException("Periudha eshte e kycur!");

                if (dtDok.Year != clsNdermarrjeViti.ktheKodVitiSipasIdNdermViti(idNdermVit))
                    throw new MyException(rm.GetString("msgDataNukPerketVititUshtrimor", ci));

                if (MbylljePeriudhe.PeriodClosing.IsPeriodClosed(dtDok, MyConnectionsManager.GetSelectedConNameServer(), idNdermarrje, KategoriDokumenti.Magazina, konfigAmbjenti.IdKonfigAmbjente))
                    throw new MyException(MessagesResource.Messages["msgPeriodIsClosed"]);

                int idStatusDok = 0;
                int meKontabilizim = 0;
                if (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "SDI", dbShare) == "Draft")
                {
                    idStatusDok = 0;
                    meKontabilizim = 0;
                }
                else
                {
                    idStatusDok = 1;
                    meKontabilizim = 1;
                }
                int nrAktualGabimesh = gabime.Rows.Count;
                if (meSerialeUnike)
                {
                    serialeUnike = new colSerialeUnikeMagazina();
                    serialeUnike.ShtoSerialNgaImporti(dokTable, fushaSerialesh, kategori, idNdermarrje, false);
                }

                bool lejoCmimZero = true;
                if (hyrje_dalje)
                    lejoCmimZero = (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "CZ", dbShare)).ToLower() != "bllokues";

                bool ngarkoKodbar = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "NKAKNKA", dbShare).ToLower() == "po";

                colTrupi = krijoTrupMagazine(dokTable, idNdermarrje, idPerdorues, col, pozicionkodi, gabime, tePaImportuara, importo, false, hyrje_dalje, out isMagENjejte, nrDokumentiEmerImport, dtDokumentiEmerImport, ref indexRreshtImporti, konfigAmbjenti, 1, idStatusDok, ref colSerialet, vjenNgaImportSQL, importAutomatik, serialeUnike, kategori, false, dtDok, false, lejoCmimZero, ngarkoKodbar);

                bool gabimeNeTrupMagazine = (gabime.Rows.Count - nrAktualGabimesh) != 0;
                if (meSerialeUnike)
                    colTrupi.BashkoTrupin(serialeUnike.MerrIdArtikujMeArtikujSet());
                int idllojDokMag = 1;
                if (!hyrje_dalje)
                    idllojDokMag = 2;
                DateTime dtRegjistrimi = DateTime.Today;

                if (!colTrupi.ValidoArtikujSet(dtDok))
                    throw new MyException("Ka probleme me recepturat e artikujve");
                clsKonfigurimAmbjenti konfigTransferim = new clsKonfigurimAmbjenti();
                colSerialetMagazine serialemagtransf = new colSerialetMagazine();
                int idRaportDesign = 0; string kodMagDest = "";
                bool serialeNeDetajim = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "TSD1", dbShare).ToLower() == "po";
                bool bashkoArtikujt = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "TD1S", dbShare).ToLower() == "po";

                (string nrAutom, Dictionary<string, object> hiddenFieldPerNrAuto) result = ktheNrAutoPerKonfigurim(konfigAmbjenti.IdKonfigAmbjente, nrDok, "txtNrDok", "NrDok", 510, dtDok, dbShare);
                string nrAutom = result.nrAutom;
                Dictionary<string, object> hiddenFieldPerNrAuto = result.hiddenFieldPerNrAuto;

                if (transferim)
                {
                    bool isMagDestENjejte = true;
                    colTrupiMagazina colTrupDest = new colTrupiMagazina();
                    colSerialeUnikeMagazina serialeUnikeClone = new colSerialeUnikeMagazina();
                    if (meSerialeUnike)
                    {
                        serialeUnikeClone = serialeUnike.Clone();
                        serialeUnikeClone.ForEach(s => s.IdLlojDokumentMagazine = 1);
                    }
                    konfigTransferim = new clsKonfigurimAmbjenti(clsKusht.kthevlereSipasKushtitDheIdKonfig(konfigAmbjenti.IdKonfigAmbjente, "ZFH"));

                    bool lejoModifikimDetajimi = clsAlternativaKushti.getAlternativa(konfigTransferim.IdKonfigAmbjente, "LMD", dbShare).ToLower() == "po";
                    if (!gabimeNeTrupMagazine)//ne rastin e dokumentave te transferimit nese ka probleme dalja edhe ky dokument do jape error po ne te njejtat pozicione duke dyfishuar numrin e erroreve
                        colTrupDest = krijoTrupMagazine(dokTable, idNdermarrje, idPerdorues, col, pozicionkodi, gabime, tePaImportuara, importo, true, !hyrje_dalje, out isMagDestENjejte, nrDokumentiEmerImport, dtDokumentiEmerImport, ref indexRreshtImporti, konfigAmbjenti, 1, idStatusDok, ref serialemagtransf, vjenNgaImportSQL, importAutomatik, serialeUnikeClone, kategori, serialeNeDetajim, dtDok, lejoModifikimDetajimi, true, ngarkoKodbar);

                    int idMagDest = -1;
                    if (isMagDestENjejte && colTrupDest.Count > 0)
                    {
                        idMagDest = colTrupDest[0].IdMag;
                        kodMagDest = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(idMagDest);
                    }

                    int idRapDesign = 0;
                    string idRaporti = clsAtributeTrupi.merrVleredefaultSipasKontrollitKodKonfigDheNderm(llojDokumenti, idNdermarrje, "cmbFormatiPrintimit");
                    if (!string.IsNullOrEmpty(idRaporti))
                        idRapDesign = Convert.ToInt32(idRaporti);

                    string niveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(konfigTransferim.IdNivel);

                    if (meSerialeUnike)
                        colTrupi.BashkoTrupin(serialeUnike.MerrIdArtikujMeArtikujSet());

                    if (bashkoArtikujt)
                        colTrupDest.BashkoTrupin(kategori);

                    if (!colTrupDest.ValidoArtikujSet(dtDok))
                        throw new MyException("Ka probleme me recepturat e artikujve");
                    kokaDest.krijoMagazinePerImport(niveli, konfigTransferim.KodKonfigAmbjente, klientFurnitor, idMagDest, kodMagDest, dtDok, nrAutom, 0, nrProjekti, 6, idDokNga, 0, idStatusDok, idNdermarrje, idNdermVit, idPerdorues, dtRegjistrimi, 1, shenime, idNivelGjeneruesi, idKonfigGjeneruesi, idGjeneruesi, degeAdministrative, llogari, njesivartese, mekonfirmimKokaMag, grupimdok1, grupimdok2, grupimdok3, pershkrimi, colTrupDest, new DbCore.DbRegjistrim.clsKokaMagazina(), new clsKokaFleteKontabel(), idRapDesign, konfigTransferim, idPerdorues, automjeti, rm, ci, transferim, kontrolloGjendje, 0, nrSerial, Nivfsh, Wtnic, 0, dbData, false, false, 0);
                }
                int idMag = -1;
                string kodMag = "";
                if (isMagENjejte && colTrupi.Count > 0)
                {
                    idMag = colTrupi[0].IdMag;
                    kodMag = clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(idMag);
                }

                if (transferim && !string.IsNullOrEmpty(kodMagDest) && kodMag == kodMagDest)
                    throw new MyException("Magazina nuk duhet te jete e njejte me magazinen destinacion!");

                idPerdorues = kthePerdoruesPerImport(idNdermarrje, idPerdorues, krijuesi, dbA);
                int.TryParse(clsAtributeTrupi.merrVleredefaultSipasKontrollitKodKonfigDheNderm(llojDokumenti, idNdermarrje, "cmbFormatiPrintimit"), out idRaportDesign);

                if (serialeUnike != null)
                {
                    colTrupi.BashkoTrupin(serialeUnike.MerrIdArtikujMeArtikujSet());
                    foreach (var trupMag in colTrupi)
                        trupMag.MerrSerialetUnike(serialeUnike, hyrje_dalje);
                }
                mesazh = koka.krijoMagazinePerImport(nenkategoria, llojDokumenti, klientFurnitor, idMag, kodMag, dtDok, nrAutom, 0, nrProjekti, 6, idDokNga, 0, idStatusDok, idNdermarrje, idNdermVit, idPerdorues, dtRegjistrimi, idllojDokMag, shenime, idNivelGjeneruesi, idKonfigGjeneruesi, idGjeneruesi, degeAdministrative, llogari, njesivartese, mekonfirmimKokaMag, grupimdok1, grupimdok2, grupimdok3, pershkrimi, colTrupi, kokaDest, new clsKokaFleteKontabel(), idRaportDesign, konfigAmbjenti, idPerdorues, automjeti, rm, ci, transferim, kontrolloGjendje, 0, nrSerial, Nivfsh, Wtnic, 0, dbData, false, false, 0);

                if (!mesazh.Status)
                {
                    throw new Exception(mesazh.PershkrimMesazhi);
                }

                if (importo)
                {
                    if (!kontrolloPerGabime(vjenNgaImportSQL, fushat, primaryKey, tePaImportuara, dokTable))
                        return new clsMesazh(false, "Ka gabime!");

                    string idDokImporti = "";
                    if (vjenNgaImportSQL)
                    {
                        idDokImporti = dokTable.Rows[0][primaryKey].ToString();
                    }
                    string shfaqmesazhapolupe = "jo";
                    bool gjithmone = false;
                    if (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "GJKGJ", dbShare).ToLower() == "po")
                        gjithmone = true;
                    clsKusht kushtamor = new clsKusht(konfigAmbjenti.IdKonfigAmbjente, "ZDAM", dbShare);
                    clsKonfigurimAmbjenti konfamortizimi = new clsKonfigurimAmbjenti();
                    konfamortizimi.mbushKonfigAmbjSipasId(kushtamor.Vlera, idGjuha);
                    clsKonfigurimAmbjenti konfamortizimihyrje = new clsKonfigurimAmbjenti();
                    if (transferim)
                    {
                        clsKusht kushtamortizim = new clsKusht(konfigTransferim.IdKonfigAmbjente, "ZDAM");
                        konfamortizimihyrje.mbushKonfigAmbjSipasId(kushtamortizim.Vlera, idGjuha);
                    }

                    mesazh = koka.ruaj(transferim, meKontabilizim, hiddenFieldPerNrAuto, per.IdPeriudha, "", out shfaqmesazhapolupe, false, colSerialet, serialemagtransf, konfamortizimi, konfamortizimihyrje, gjithmone, vjenNgaImportSQL, idDokImporti, ndermarrjeKey, emerTabKoka, primaryKey, ruajrenditje, false, new int[0], false, false, false, out mesazhmevonshem, 0, ref dbData, importo, null, false, bashkoArtikujt);
                    if (!mesazh.Status)
                    {
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                            msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ": " + mesazh.PershkrimMesazhi;
                        else msgGabimi = mesazh.PershkrimMesazhi;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, 6);
                    }
                }
                return mesazh;
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                DateTime datedok = new DateTime();
                string msgGabimi = "";
                bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                if (dateVlefshme)
                    msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ": " + "Dokumenti nuk mund te importohet ne kete moment. Ju lutem provoni perseri!";
                else
                    msgGabimi = "Dokumenti nuk mund te importohet ne kete moment. Ju lutem provoni perseri!";
                deadLocked.Merge(dokTable);
                shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, 6);
                throw ex;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                if (error == "")
                    error = "Gabim gjate krijimit te dokumentit!";
                DateTime datedok = new DateTime();
                string msgGabimi = "";
                bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                if (dateVlefshme)
                    msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " " + error;
                else
                    msgGabimi = error;
                shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, 6);
                return new clsMesazh(false, error);
            }
        }

        public static (string nrAutom, Dictionary<string, object> hiddenFieldPerNrAuto) ktheNrAutoPerKonfigurim(int idKonfigAmbjenti, string nrDok, string emerFushe, string kodKontrolli, int idKomponente, DateTime dtDok, clsDatabaseShare dbshare)
        {
            var merrNrAutoImport = clsAlternativaKushti.getAlternativa(idKonfigAmbjenti, "MNRAI", dbshare) == "Po";
            var hiddenFieldPerNrAuto = new Dictionary<string, object>();
            var nrAutom = nrDok;
            if (merrNrAutoImport)
            {
                var idnrautonrdok = clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(idKonfigAmbjenti, emerFushe, idKomponente);
                if (idnrautonrdok != 0)
                {
                    var nrdokshitje = clsNrAutom.merrVlerenNrAutomatik(idnrautonrdok, dtDok);
                    if (!string.IsNullOrEmpty(nrdokshitje))
                    {
                        var nrdokshi = new NrAuto
                        {
                            kodKontrolli = kodKontrolli,
                            idNrAuto = idnrautonrdok,
                            vlereNrAuto = nrdokshitje
                        };
                        hiddenFieldPerNrAuto.Add(kodKontrolli, JsonConvert.SerializeObject(nrdokshi));
                        nrAutom = nrdokshitje;
                    }
                }
            }
            if (string.IsNullOrEmpty(nrAutom))
                nrAutom = nrDok;
            return (nrAutom, hiddenFieldPerNrAuto);
        }

        public static colTrupiMagazina krijoTrupMagazine(DataTable dokTrupi, int idNdermarrje, int idPerdorues, colTrupiFormatImporti col, int pozicionkodi, DataTable gabime, DataTable tePaImportuara, bool importo, bool eshteTransferim, bool eshteHyrje, out bool isMagENjejte, string nrDokumentiEmerImport, string dtDokumentiEmerImport, ref int indexRreshtImporti, clsKonfigurimAmbjenti konfigMagazina, double kursi, int idstatusdok, ref colSerialetMagazine colSerialet, bool vjenNgaImportSQL, bool importAutomatik, colSerialeUnikeMagazina serialeUnike, colSerialeUnikeKategori kategori, bool serialeNeDetajim, DateTime dateDokumenti, bool lejoModifikimDetajimi, bool lejoCmimZero, bool ngarkoKodbar)
        {
            string error = "";
            int j = 1;
            colTrupiMagazina colTrupi = new colTrupiMagazina();
            int idMagTemp = -1;
            isMagENjejte = true;
            foreach (DataRow dr in dokTrupi.Rows)
            {
                try
                {
                    error = "";
                    clsTrupiMagazina trupi = new clsTrupiMagazina();
                    int idTrupiKonvertimUd = 0;
                    string kodi = "", njesia = "", magazina = "", detajim1 = "", detajim2 = "", magDestinacion = "", seriali = "", shenime = "", kodbari = "", artikulliSet = "";
                    double sasia = 0, cmimi = 0, vlefta = 0;
                    DateTime dtDok = new DateTime();

                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        #region trupi
                        switch (trup.KodKontrolli)
                        {
                            case "Kodi i Artikullit":
                                kodi = vendosVlere(trup, dr, out error);
                                break;

                            case "Njesia":
                                njesia = vendosVlere(trup, dr, out error);
                                break;

                            case "Sasia":
                                sasia = vendosDouble(trup, dr, out error);
                                break;

                            case "Cmimi":
                                cmimi = vendosDouble(trup, dr, out error);
                                break;

                            case "Vlefta":
                                vlefta = vendosDouble(trup, dr, out error);
                                break;

                            case "Magazina":
                                magazina = vendosVlere(trup, dr, out error);
                                break;

                            case "Kodbari":
                                kodbari = vendosVlere(trup, dr, out error);
                                break;

                            case "Detajimi 1":
                                detajim1 = vendosVlere(trup, dr, out error);
                                DateTime dt = new DateTime();
                                bool parseDt = DateTime.TryParse(detajim1, out dt);
                                if (parseDt && detajim1.IndexOf("00:00:00") > 0)
                                    detajim1 = detajim1.Substring(0, detajim1.IndexOf("00:00:00") - 1);
                                break;

                            case "Detajimi 2":
                                detajim2 = vendosVlere(trup, dr, out error);
                                DateTime dt2 = new DateTime();
                                bool parseDt2 = DateTime.TryParse(detajim2, out dt2);
                                if (parseDt2 && detajim2.IndexOf("00:00:00") > 0)
                                    detajim2 = detajim2.Substring(0, detajim2.IndexOf("00:00:00") - 1);
                                break;

                            case "Mag destinacion":
                                magDestinacion = vendosVlere(trup, dr, out error);
                                break;

                            case "Date Dokumenti":
                                string gabim = "";
                                dtDok = vendosDate(trup, dr, out gabim);
                                if (dtDok == DateTime.MinValue)
                                    dtDok = DateTime.Today;
                                break;

                            case "Seriali":
                                seriali = vendosVlere(trup, dr, out error);
                                break;

                            case "IdTrupiKonvertimUD":
                                idTrupiKonvertimUd = vendosInt(trup, dr, out error);
                                break;

                            case "Shenime Trupi":
                                shenime = vendosVlere(trup, dr, out error);
                                break;
                            case "Artikulli Set":
                                artikulliSet = vendosVlere(trup, dr, out error);
                                break;

                        }
                        #endregion
                        if (error != "")
                        {
                            DateTime datedok = new DateTime();
                            string msgGabimi = "";
                            bool dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                            if (dateVlefshme)
                                msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + " :" + trup.EmerImporti + " duhet te jete numer!";
                            else msgGabimi = error;
                            shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, 6, dr);
                            continue;
                        }
                    }

                    clsMesazh mesazh = new clsMesazh(true);
                    if (kodbari == "" && ngarkoKodbar)
                        kodbari = clsKodbari.ktheKodbarSipasIdArtikulliNjesiaKodbariIPare(kodi, idNdermarrje);
                    if (eshteHyrje)
                        mesazh = trupi.krijoTrupMagazineNgaImporti(kodi, njesia, sasia, cmimi, vlefta, magazina, eshteHyrje, detajim1, detajim2, idNdermarrje, idPerdorues, magDestinacion, eshteTransferim, dtDok, 1, seriali, j - 1, konfigMagazina, kursi, idstatusdok, ref colSerialet, idTrupiKonvertimUd, shenime, artikulliSet, kodbari, lejoCmimZero);
                    else
                        mesazh = trupi.krijoTrupMagazineNgaImporti(kodi, njesia, sasia, cmimi, vlefta, magazina, eshteHyrje, detajim1, detajim2, idNdermarrje, idPerdorues, magDestinacion, eshteTransferim, dtDok, -1, seriali, j - 1, konfigMagazina, kursi, idstatusdok, ref colSerialet, idTrupiKonvertimUd, shenime, artikulliSet, kodbari, lejoCmimZero);
                    if (mesazh.Status)
                    {
                        if (idMagTemp == -1)
                            idMagTemp = trupi.IdMag;
                        else
                            if (isMagENjejte && trupi.IdMag != idMagTemp)
                            isMagENjejte = false;


                        trupi.MerrSerialetUnike(serialeUnike, !eshteHyrje, 0);
                        if (!serialeNeDetajim)
                            colTrupi.Add(trupi);
                        else
                        {
                            var kat = kategori.MerrKategoriSipasIdFormatit(((clsArtikulli)trupi.Element).IdFormatSeriali);
                            if (!((clsArtikulli)trupi.Element).DetajimArtikulli || kat == null || !kat.Kategori.EqualsIgnoreCase(enumKategoriSerialesh.APARATE.ToString()))
                                colTrupi.Add(trupi);
                            else
                            {
                                var trupaFHT = trupi.ShperndaTrupinSipasSerialeve(idPerdorues, idNdermarrje, lejoModifikimDetajimi, 0);
                                colTrupi.AddRange(trupaFHT);
                            }
                        }
                        indexRreshtImporti++; j++;
                    }
                    else
                        throw new MyException(mesazh.PershkrimMesazhi);


                }
                catch (MyException ex)
                {
                    DateTime datedok = new DateTime();
                    string msgGabimi = "";
                    indexRreshtImporti++; j++;
                    bool dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                    if (dateVlefshme)
                        msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + ": " + ex.Message;
                    else msgGabimi = ex.Message;
                    shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, 6, dr);
                    continue;
                }
            }
            return colTrupi;
        }

        public static clsMesazh krijoDokInventarizimi(DataTable dokTable, ResourceManager rm, CultureInfo ci, int idNdermarrje, int idPerdorues, colTrupiFormatImporti col, int pozicionkodi, DataTable gabime, DataTable tePaImportuara, bool importo, int idNdermVit, int idGjuha, bool vjenNgaImportSQL, string primaryKey, string ndermarrjeKey, ref int indexRreshtImporti, bool importAutomatik, string emerTabKoka, string[] fushat, bool ruajrenditje, int idkategoria, DbData dbData)
        {
            if (dokTable == null)
                return new clsMesazh(false);
            string error = "";
            string nrDokumentiEmerImport = "";
            string dtDokumentiEmerImport = "";
            string magazinaEmerImport = "";
            bool kaGabim = false;
            bool kontrolloDate = true;
            try
            {
                nrDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Nr Dok").EmerImporti;
                dtDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Data").EmerImporti;
                magazinaEmerImport = col.filtroFormatImportiSipasFushes("Magazina").EmerImporti;
                clsMesazh mesazh = new clsMesazh(true);

                string nenkategoria = "", llojDokumenti = "", nrDok = "", krijuesi = "", magazina = "";
                DateTime dtDok = new DateTime();
                error = "";
                int idNivelGjeneruesi = 0, idKonfigGjeneruesi = 0, idGjeneruesi = 0, idDokNga = 0;
                bool afatgjate = false;
                if (idkategoria == 136)
                {
                    afatgjate = true;
                    llojDokumenti = "INAAGJ";
                    nenkategoria = "IAAGJ";
                }
                else
                {
                    llojDokumenti = "INAASH";
                    nenkategoria = "IAASH";
                }

                #region Fushat e kokes
                foreach (clsTrupiFormatImporti trup in col)
                {
                    error = "";
                    switch (trup.KodKontrolli)
                    {


                        case "Nr Dok":
                            nrDok = vendosVlere(trup, dokTable.Rows[0], out error);

                            break;

                        //case "Pershkrimi":
                        //    pershkrimi = DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                        //    break;

                        case "Magazina":
                            magazina = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Data":
                            if (!trup.Visible) kontrolloDate = false;
                            dtDok = vendosDate(trup, dokTable.Rows[0], out error);
                            if (dtDok == DateTime.MinValue)
                            { dtDok = DateTime.Today; }
                            break;
                        case "Krijuesi":
                            krijuesi = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;


                    }
                    if (error != "")
                    {
                        kaGabim = true;
                        DateTime datedok = new DateTime();

                        string msgGabimi = ""; bool dateVlefshme = false;
                        if (kontrolloDate)
                        {
                            dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        }
                        if (dateVlefshme)
                            msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " :" + trup.EmerImporti + " duhet te jete numer!";
                        else msgGabimi = error;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dokTable.Rows[0][magazinaEmerImport].ToString(), vjenNgaImportSQL, dokTable, 6);
                        continue;
                    }
                }

                #endregion

                DbCore.DbRegjistrim.clsKokaInventarizim koka = new DbCore.DbRegjistrim.clsKokaInventarizim();
                DbCore.DbRegjistrim.colTrupiInventarizim colTrupi = new DbCore.DbRegjistrim.colTrupiInventarizim();
                clsDatabaseShare dbShare = new clsDatabaseShare(dbData);
                clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbData);
                clsKonfigurimAmbjenti konfigAmbjenti = new clsKonfigurimAmbjenti(llojDokumenti, idNdermarrje, dbShare);
                // konfigAmbjenti.mbushKonfigAmbjSipasKod(llojDokumenti, idNdermarrje);


                if (!clsViti.ekzistonVitPerNdermarrjen(dtDok.Year.ToString(), idNdermarrje))
                    throw new MyException("Ky vit nuk ekziston!");
                clsPeriudhaKontabel per = new clsPeriudhaKontabel(dtDok, idNdermarrje);
                if (per.IdPeriudha == 0)
                    throw new MyException("Periudha nuk ekziston!");
                if (per.Ekycur)
                    throw new MyException("Periudha eshte e kycur!");

                if (dtDok.Year != clsNdermarrjeViti.ktheKodVitiSipasIdNdermViti(idNdermVit))
                    throw new MyException(rm.GetString("msgDataNukPerketVititUshtrimor", ci));

                int idStatusDok = 1;
                if (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "SDI") == "Draft")
                {
                    idStatusDok = 0;

                }
                else
                {
                    idStatusDok = 1;

                }
                JavaScriptSerializer serializusi = new JavaScriptSerializer();
                serializusi.MaxJsonLength = 50000000;
                IDictionary<string, object> hidden = new Dictionary<string, object>();
                List<NrAuto> list = new List<NrAuto>();
                if (String.IsNullOrEmpty(nrDok)) //nqs nuk ka numer dokumenti te caktuar ne excel apo sql, atehere kontrollohet per nr automatik
                {
                    int idnrautonrdok = clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(konfigAmbjenti.IdKonfigAmbjente, "txtNrDok", 549);
                    string nrdokAuto = clsNrAutom.merrVlerenNrAutomatik(idnrautonrdok, dtDok);
                    if (!String.IsNullOrEmpty(nrdokAuto))//nqs ka nr automatik
                    {
                        NrAuto nrdokshi = new NrAuto();
                        nrdokshi.kodKontrolli = "NrDok";
                        nrdokshi.idNrAuto = idnrautonrdok;
                        nrdokshi.vlereNrAuto = nrdokAuto;

                        list.Add(nrdokshi);
                        nrdokAuto = nrdokshi.vlereNrAuto;
                        hidden.Add("NrDok", serializusi.Serialize(nrdokshi));
                    }
                    if (idnrautonrdok != 0)
                        nrDok = nrdokAuto;
                }

                if (nrDok == "")
                    nrDok = magazina + "-" + dtDok.ToShortDateString() + "-" + new Random().Next(10000);

                colTrupi = krijoTrupInventarizim(dokTable, col, pozicionkodi, gabime, tePaImportuara, importo, magazinaEmerImport, dtDokumentiEmerImport, ref indexRreshtImporti, vjenNgaImportSQL, importAutomatik, kontrolloDate);


                DateTime dtRegjistrimi = DateTime.Today;
                int idmag = DbRegjistrim.clsNjesiAdministrative.ktheIdMagazine(magazina, idNdermarrje);
                idPerdorues = clsFunksione.kthePerdoruesPerImport(idNdermarrje, idPerdorues, krijuesi, dbAdmin);

                mesazh = koka.krijoInventarizimPerImport(nenkategoria, llojDokumenti, idmag, magazina, dtDok, nrDok, 0, idStatusDok, idNdermarrje, idNdermVit, idPerdorues, dtRegjistrimi, idNivelGjeneruesi, idKonfigGjeneruesi, idGjeneruesi, colTrupi, konfigAmbjenti, idPerdorues, rm, ci);

                if (!mesazh.Status)
                {
                    throw new Exception(mesazh.PershkrimMesazhi);
                }

                if (importo)
                {
                    //string id = "";
                    //string idDokImporti = "";
                    //if (vjenNgaImportSQL)
                    //{
                    //    idDokImporti = dokTable.Rows[0][primaryKey].ToString();
                    //}
                    //else
                    //    id = "Id";

                    //if (gabime.Rows.Count > 0)
                    //{
                    //    foreach (DataRow dr in dokTable.Rows)
                    //    {
                    //        if (tePaImportuara.Select(String.Format("{0} = '{1}'", id, dr[id])).Count() == 0)
                    //            tePaImportuara.ImportRow(dr);
                    //    }
                    //    return new clsMesazh(false, "Ka gabime!");
                    //}

                    if (!kontrolloPerGabime(vjenNgaImportSQL, fushat, primaryKey, tePaImportuara, dokTable))
                        return new clsMesazh(false, "Ka gabime!");

                    string idDokImporti = "";
                    if (vjenNgaImportSQL)
                    {
                        idDokImporti = dokTable.Rows[0][primaryKey].ToString();
                    }
                    string shfaqmesazhapolupe = "";
                    mesazh = koka.ruaj(hidden, out shfaqmesazhapolupe, rm, ci, vjenNgaImportSQL, idDokImporti, ndermarrjeKey, emerTabKoka, primaryKey, true);
                    if (!mesazh.Status)
                    {
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        bool dateVlefshme = false;
                        if (kontrolloDate)
                        {
                            dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        }
                        if (dateVlefshme)
                            msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ": " + mesazh.PershkrimMesazhi;
                        else msgGabimi = mesazh.PershkrimMesazhi;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][magazinaEmerImport].ToString(), vjenNgaImportSQL, dokTable, 6);
                    }
                }
                return mesazh;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                if (error == "")
                    error = "Gabim gjate krijimit te dokumentit!";
                DateTime datedok = new DateTime();
                string msgGabimi = "";
                bool dateVlefshme = false;
                if (kontrolloDate)
                {
                    dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                }
                if (dateVlefshme)
                    msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " " + error;
                else
                    msgGabimi = error;
                shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][magazinaEmerImport].ToString(), vjenNgaImportSQL, dokTable, 6);
                return new clsMesazh(false, error);
            }
        }

        public static DbCore.DbRegjistrim.colTrupiInventarizim krijoTrupInventarizim(DataTable dokTrupi, colTrupiFormatImporti col, int pozicionkodi, DataTable gabime, DataTable tePaImportuara, bool importo, string magazinaEmerImport, string dtDokumentiEmerImport, ref int indexRreshtImporti, bool vjenNgaImportSQL, bool importAutomatik, bool kontrolloDate)
        {
            string error = "";
            int j = 1;
            DbCore.DbRegjistrim.colTrupiInventarizim colTrupi = new DbCore.DbRegjistrim.colTrupiInventarizim();

            foreach (DataRow dr in dokTrupi.Rows)
            {
                try
                {

                    DbCore.DbRegjistrim.clsTrupiInventarizim trupi = new DbCore.DbRegjistrim.clsTrupiInventarizim();
                    string kodi = "", seriali = "";
                    decimal sasia = 0;

                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        #region trupi
                        switch (trup.KodKontrolli)
                        {
                            case "Barkodi":
                                kodi = vendosVlere(trup, dr, out error);
                                break;
                            case "Sasia":
                                sasia = vendosDecimal(trup, dr, out error);
                                break;
                            case "Seriali":
                                seriali = vendosVlere(trup, dr, out error);
                                break;
                        }
                        #endregion
                        if (error != "")
                        {
                            DateTime datedok = new DateTime();
                            string msgGabimi = "";
                            bool dateVlefshme = false;
                            if (kontrolloDate)
                            {
                                dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                            }
                            if (dateVlefshme)
                                msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + " :" + trup.EmerImporti + " duhet te jete numer!";
                            else msgGabimi = error;
                            shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[magazinaEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, 6, dr);
                            continue;
                        }
                    }

                    clsMesazh mesazh = new clsMesazh();
                    trupi = clsTrupiInventarizim.krijoTrupInventarizimiNgaImporti(kodi, seriali, sasia);
                    colTrupi.Add(trupi);
                    indexRreshtImporti++; j++;
                }
                catch (Exception ex)
                {
                    DateTime datedok = new DateTime();
                    string msgGabimi = "";

                    bool dateVlefshme = false;
                    if (kontrolloDate)
                    {
                        dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                    }
                    if (dateVlefshme)
                        msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + ": " + ex.Message;
                    else msgGabimi = ex.Message;
                    shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[magazinaEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, 6, dr);
                    indexRreshtImporti++; j++;
                    continue;
                }
            }
            return colTrupi;
        }

        public static clsMesazh importDokumenteshArkaBanka(DataTable teDhenatPerImport, int idNdermarrje, int idPerdorues, ref DataTable gabime, ref DataTable tePaImportuara, string nenkategoria, string ndermarrjeKey, string primaryKey, colTrupiFormatImporti col, int idKategori, ResourceManager rm, CultureInfo ci, int idGjuha, string emerTabKoka, string emerTabTrupi, clsNdermarrje nderm, bool importo, bool vjenNgaImportSQL, bool importAutomatik, int idNdermVit)
        {
            clsMonedha monNderm = new clsMonedha();
            monNderm.mbushMonedhenENdermarrjes(idNdermarrje);
            int formati = 0;
            clsMesazh mesazh = new clsMesazh(true);

            foreach (clsTrupiFormatImporti trupi in col)
            {
                vendosVlereDefaultTeDataTable(trupi, teDhenatPerImport);
            }

            string fushatEGrupimit = "";
            DataTable dataGrupime;
            if (vjenNgaImportSQL)
            {
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, primaryKey);
            }
            else
            {
                //grupojme dokumentet qe vijne si datatable sipas fushave te kokes dhe i ruajme ato tek tabela dataGrupime
                string fushaGrupimi = "";
                foreach (clsTrupiFormatImporti trupi in col)
                {
                    if (trupi.Visible && trupi.Shfaq && (trupi.FusheKokeApoTrupi == 1 || trupi.FusheKokeApoTrupi == 3))
                    {
                        //shtojme te stringu fushat e percaktuara te formatit qe jane fusha te kokes se dokumentit, per kete perjashtojme fushat e trupit
                        fushaGrupimi += trupi.EmerImporti + ";";
                    }
                }
                fushatEGrupimit = fushaGrupimi.Substring(0, fushaGrupimi.LastIndexOf(';'));//heqim pikepresjen e fundit
                string[] fushat = fushatEGrupimit.Split(';');
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, fushat);
            }
            int indexRreshtImporti = 1;
            using (DbData dbData = new DbData())
            {
                foreach (DataRow drDok in dataGrupime.Rows)
                {
                    DataTable dokumentKokTrup = null;
                    try
                    {
                        if (vjenNgaImportSQL)
                        {
                            dokumentKokTrup = teDhenatPerImport.Select(String.Format("[{0}] = '{1}'", primaryKey, drDok[primaryKey])).CopyToDataTable();
                            mesazh = krijoDokArkaBanka(dokumentKokTrup, rm, ci, idNdermarrje, idPerdorues, col, gabime, tePaImportuara, importo, idNdermVit, idGjuha, true, ref indexRreshtImporti, monNderm, primaryKey, ndermarrjeKey, idKategori, emerTabKoka, importAutomatik, new string[0], dbData);
                        }
                        else
                        {
                            //krijojme nje tabele te re, ku vendosim dokumentin
                            string selekti = "";
                            string[] fushat = fushatEGrupimit.Split(';');
                            for (int j = 0; j < fushat.Count(); j++)
                            {
                                if (!String.IsNullOrEmpty(drDok[fushat[j]].ToString()))
                                {
                                    bool isDouble = (drDok[fushat[j]].GetType().Name.ToLower() == "double");
                                    string value = isDouble ? ((double)drDok[fushat[j]]).ToString("R") : drDok[fushat[j]].ToString().Replace("'", "''");
                                    selekti += $"[{fushat[j]}] = '{value}' AND ";
                                }
                            }
                            selekti += "1 = 1";

                            dokumentKokTrup = teDhenatPerImport.Select(selekti).CopyToDataTable();
                            mesazh = krijoDokArkaBanka(dokumentKokTrup, rm, ci, idNdermarrje, idPerdorues, col, gabime, tePaImportuara, importo, idNdermVit, idGjuha, false, ref indexRreshtImporti, monNderm, "", "", idKategori, "", importAutomatik, fushat, dbData);
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            if (!mesazh.Status)
                return new clsMesazh(false, rm.GetString("msgGabimNeImportim", ci));
            return new clsMesazh(true, rm.GetString("msgImportimMeSukses", ci));
        }

        public static clsMesazh krijoDokArkaBanka(DataTable dokTable, ResourceManager rm, CultureInfo ci, int idNdermarrje, int idPerdorues, colTrupiFormatImporti col, DataTable gabime, DataTable tePaImportuara, bool importo, int idNdermVit, int idGjuha, bool vjenNgaImportSQL, ref int indexRreshtImporti, clsMonedha monNderm, string primaryKey, string ndermarrjeKey, int kategoria, string emerTabKoka, bool importAutomatik, string[] fushat, DbData dbData)
        {
            if (dokTable == null)
                return new clsMesazh(false);
            string error = "";
            string nrDokumentiEmerImport = "";
            string dtDokumentiEmerImport = "";
            try
            {
                nrDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Nr Dokumenti").EmerImporti;
                dtDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Date dokumenti").EmerImporti;

                clsMesazh mesazh = new clsMesazh(true);

                string nenkategoria = "", llojDokumenti = "", nrDok = "", pershkrimi = "", arkaBanka = "", degeAdministrative = "", nrSerial = "", grupimdok1 = "", grupimdok2 = "", grupimdok3 = "", krijuesi = "";
                DateTime dtDok = new DateTime();
                double shuma = 0, kursi = 1, shumaMonBaze = 0, komisioni = 0;
                error = "";
                bool dokArkaBanka = false; //veprime arke apo banke. false nqs eshte veprim arka, true nqs eshte veprim banke
                if (kategoria.ToString() == "3")
                    dokArkaBanka = false;
                else dokArkaBanka = true;

                #region Fushat e kokes
                foreach (clsTrupiFormatImporti trup in col)
                {
                    error = "";
                    switch (trup.KodKontrolli)
                    {
                        case "Nenkategoria":
                            nenkategoria = DbCore.clsFunksione.ktheStringunPaHapesira(DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error), true);
                            break;
                        case "Lloji":
                            llojDokumenti = DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Nr Dokumenti":
                            nrDok = DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Pershkrimi":
                            pershkrimi = DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Arka/Banka":
                            arkaBanka = DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Dege Administrative":
                            degeAdministrative = DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Shuma":
                            shuma = DbCore.clsFunksione.vendosDouble(trup, dokTable.Rows[0], out error);
                            break;
                        case "Grupim Likuiditete 1":
                            grupimdok1 = DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Grupim Likuiditete 2":
                            grupimdok2 = DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Grupim Likuiditete 3":
                            grupimdok3 = DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Date dokumenti":
                            dtDok = DbCore.clsFunksione.vendosDate(trup, dokTable.Rows[0], out error);
                            if (dtDok == DateTime.MinValue)
                                dtDok = DateTime.Today;
                            break;
                        case "Kursi":
                            kursi = DbCore.clsFunksione.vendosDouble(trup, dokTable.Rows[0], out error);
                            break;
                        case "Nr Serial":
                            nrSerial = DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Shuma ne monedhen baze":
                            shumaMonBaze = DbCore.clsFunksione.vendosDouble(trup, dokTable.Rows[0], out error);
                            break;
                        case "Komisioni":
                            komisioni = DbCore.clsFunksione.vendosDouble(trup, dokTable.Rows[0], out error);
                            break;
                        case "Krijuesi":
                            krijuesi = DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                    }
                    if (error != "")
                    {
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                            msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " :" + trup.EmerImporti + " duhet te jete numer!";
                        else msgGabimi = error;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, kategoria);
                        continue;
                    }
                }
                #endregion

                clsVeprimBankaKoka koka = new clsVeprimBankaKoka();
                colVeprimBankaTrupi colTrupi = new colVeprimBankaTrupi();
                clsDatabaseShare dbShare = new clsDatabaseShare(dbData);
                clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbData);
                clsKonfigurimAmbjenti konfigAmbjenti = new clsKonfigurimAmbjenti(llojDokumenti, idNdermarrje, dbShare);
                bool kaAutorizim = clsKonfigurimAmbjenti.getAutorizimKonfigurimi(konfigAmbjenti.IdKonfigAmbjente, idPerdorues, dbShare);
                if (!kaAutorizim)
                    throw new Exception($"Perdoruesi nuk ka autorizim per llojin e dokumentit {konfigAmbjenti.KodKonfigAmbjente}.");
                //konfigAmbjenti.mbushKonfigAmbjSipasKod(llojDokumenti, idNdermarrje);

                //if (konfigAmbjenti.IdKonfigAmbjente == 0)
                //    throw new Exception(rm.GetString("labelFilterAvancuarLlojDok", ci) + " " + llojDokumenti + " " + rm.GetString("msgNukEkziston", ci));

                object[] nivele = new object[dokTable.Rows.Count];

                //int idMonedhaBanka = DbCore.DbArkaBanka.clsBanka.ktheIdMonedhaBanka(arkaBanka, idNdermarrje);
                if (!clsBanka.ekziston(arkaBanka, idNdermarrje).Status)
                    throw new Exception(rm.GetString("msgArkeBankeNukEkziston", ci));
                clsMonedha monedhaArkesBankes = new clsMonedha();
                monedhaArkesBankes.mbushMonedhenSipasKodArkaBanka(arkaBanka, idNdermarrje);
                if (monedhaArkesBankes.IdMonedha == 0)
                    throw new Exception(rm.GetString("msgMonedheArkeBankeNukEkziston", ci));
                int idPerdoruesPerKontroll = idPerdorues;
                idPerdorues = clsFunksione.kthePerdoruesPerImport(idNdermarrje, idPerdorues, krijuesi, dbAdmin);

                //clsMonedha monArkaBanka = new clsMonedha(idMonedhaBanka);
                //string kodiMonArkaBanka = clsMonedha.ktheKodMonedheSipasId(idMonedhaBanka);
                string alternativa = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "LLK");
                int llojKursi = 1;
                if (alternativa != "")
                {
                    llojKursi = int.Parse(alternativa.Substring(alternativa.Length));
                }

                double kursiMon = 1;
                if (kursi == 0)
                    kursiMon = (new clsKurset(monedhaArkesBankes.IdMonedha, dtDok, konfigAmbjenti.IdKonfigAmbjente, idNdermarrje)).VleraKursi;
                // = clsKurset.merrKursinFunditPerMonedheDateDheLloj(monedhaArkesBankes.IdMonedha, dtDok, llojKursi);
                else kursiMon = kursi;
                if (kursiMon == 0)
                    kursiMon = 1;

                bool kushtiLLMKF = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "LLMKF").ToLower() == "po";

                colTrupi = krijoTrupArkaBanka(dokTable, idNdermarrje, idPerdorues, col, gabime, tePaImportuara, importo, false, dokArkaBanka, ref nivele, nrDokumentiEmerImport, dtDokumentiEmerImport, dtDok, monedhaArkesBankes, monNderm, llojKursi, ref indexRreshtImporti, kursiMon, importAutomatik, vjenNgaImportSQL, kategoria, konfigAmbjenti.IdKonfigAmbjente, kushtiLLMKF);

                clsPeriudhaKontabel per = new clsPeriudhaKontabel(dtDok, idNdermarrje);
                DateTime dtRegjistrimi = DateTime.Today;

                int idStatusDok = 0;
                bool meKontabilizim = false;

                if (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "SDI") == "Draft")
                {
                    idStatusDok = 0;
                    meKontabilizim = false;
                }
                else
                {
                    idStatusDok = 1;
                    if (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "GJK") != "Jo")
                        meKontabilizim = true;
                }
                clsKonfigurimAmbjenti konfigdokLidhes = new clsKonfigurimAmbjenti();
                konfigdokLidhes.mbushKonfigAmbjSipasId(konfigAmbjenti.IdKonfigurimi, idGjuha);
                int idRaportDesign = 0;
                string idRaporti = DbCore.DbShare.clsAtributeTrupi.merrVleredefaultSipasKontrollitKodKonfigDheNderm(llojDokumenti, idNdermarrje, "cmbFormatiPrintimit");
                if (!String.IsNullOrEmpty(idRaporti))
                    idRaportDesign = Convert.ToInt32(idRaporti);

                int idllojdokumenti;//kujdes nuk eshte njesoj me kategorine por ka te beje me drejtimin e lekeve hyjne apo dalin 
                switch (nenkategoria.ToLower())
                {
                    case "terheqje":
                    case "pagese":
                        idllojdokumenti = 3; break;
                    case "derdhje":
                    case "arketim":
                        idllojdokumenti = 4; break;
                    default:
                        idllojdokumenti = 4; break;
                }

                mesazh = koka.krijoVeprimeBankePerImport(arkaBanka, kursiMon, dtDok, DateTime.Today, nrDok, 0, nrSerial, pershkrimi, "Me mirebesim", shuma, shumaMonBaze, komisioni, komisioni * kursi, nenkategoria, idPerdorues, idllojdokumenti, idStatusDok, idNdermVit, 0, 0, 0, 0, degeAdministrative, idNdermarrje, colTrupi, meKontabilizim, monedhaArkesBankes.IdMonedha, "", grupimdok1, grupimdok2, grupimdok3, konfigdokLidhes, nivele, "", "", 0, "", idRaportDesign, idGjuha, rm, ci, per, idStatusDok, llojKursi, konfigAmbjenti, monNderm, kategoria, idPerdoruesPerKontroll, idPerdorues, dbData);

                if (!mesazh.Status)
                {
                    throw new Exception(mesazh.PershkrimMesazhi);
                }

                if (importo)
                {
                    if (!kontrolloPerGabime(vjenNgaImportSQL, fushat, primaryKey, tePaImportuara, dokTable))
                        return new clsMesazh(false, "Ka gabime!");

                    string idDokImporti = "";
                    if (vjenNgaImportSQL)
                    {
                        idDokImporti = dokTable.Rows[0][primaryKey].ToString();
                    }

                    mesazh = koka.ruaj(null, vjenNgaImportSQL, idDokImporti, emerTabKoka, primaryKey, ndermarrjeKey, 0, StatusAprovimi.Undefined, 0, "", false);

                    if (!mesazh.Status)
                    {
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                            msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ": " + mesazh.PershkrimMesazhi;
                        else msgGabimi = mesazh.PershkrimMesazhi;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, kategoria);
                    }
                }
                return mesazh;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                if (error == "")
                    error = "Gabim gjate krijimit te dokumentit!";
                DateTime datedok = new DateTime();
                string msgGabimi = "";
                bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                if (dateVlefshme)
                    msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " " + error;
                else
                    msgGabimi = error;
                shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, kategoria);
                return new clsMesazh(false, error);
            }
        }

        public static colVeprimBankaTrupi krijoTrupArkaBanka(DataTable dokTrupi, int idNdermarrje, int idPerdorues, colTrupiFormatImporti col, DataTable gabime, DataTable tePaImportuara, bool importo, bool eshteTransferim, bool hyrje_dalje, ref object[] nivele, string nrDokumentiEmerImport, string dtDokumentiEmerImport, DateTime dtDok, clsMonedha monArkaBanka, clsMonedha monNderm, int llojKursi, ref int indexRreshtImporti, double kursiMon, bool importAutomatik, bool vjenNgaImportSQL, int kategoria, int idKonfigAmbjenti, bool merrKursFature)
        {
            string error = "";
            int j = 1;
            DbCore.DbArkaBanka.colVeprimBankaTrupi colTrupi = new DbCore.DbArkaBanka.colVeprimBankaTrupi();
            nivele = new object[dokTrupi.Rows.Count];
            int index = 0;
            foreach (DataRow dr in dokTrupi.Rows)
            {
                try
                {
                    error = "";
                    DbCore.DbArkaBanka.clsVeprimBankaTrupi trupi = new DbCore.DbArkaBanka.clsVeprimBankaTrupi();
                    string llojiSubjektit = "", subjekti = "", fatura = "", llojDokFature = "", pershkrimi = "", debiKredi = "";
                    double vlera = 0, vleraMonBaze = 0;

                    DateTime dtFature = DateTime.MinValue;

                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        #region trupi
                        switch (trup.KodKontrolli)
                        {
                            case "Lloji i subjektit":
                                llojiSubjektit = DbCore.clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Subjekti":
                                subjekti = DbCore.clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Debi/Kredi":
                                debiKredi = DbCore.clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Vlera":
                                vlera = DbCore.clsFunksione.vendosDouble(trup, dr, out error);
                                break;
                            case "Fatura":
                                fatura = DbCore.clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Data e Fatures":
                                dtFature = DbCore.clsFunksione.vendosDate(trup, dr, out error);
                                if (dtFature == DateTime.MinValue)
                                    dtFature = DateTime.Today;
                                break;
                            case "Lloj Dokumenti i Fatures":
                                llojDokFature = DbCore.clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Vlera ne monedhen baze":
                                vleraMonBaze = DbCore.clsFunksione.vendosDouble(trup, dr, out error);
                                break;
                            case "Pershkrim Trupi":
                                pershkrimi = DbCore.clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                        }
                        #endregion
                        if (error != "")
                        {
                            DateTime datedok = new DateTime();
                            string msgGabimi = "";
                            bool dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                            if (dateVlefshme)
                                msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + " :" + trup.EmerImporti + " duhet te jete numer!";
                            else msgGabimi = error;
                            shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, kategoria, dr);
                            continue;
                        }
                    }
                    clsMesazh mesazh = new clsMesazh();
                    mesazh = trupi.krijoTrupArkaBankaNgaImporti(llojiSubjektit, subjekti, fatura, llojDokFature, pershkrimi, vlera, vleraMonBaze, debiKredi, dtFature, ref nivele, idNdermarrje, dtDok, idPerdorues, monNderm, monArkaBanka, llojKursi, index, kursiMon, idKonfigAmbjenti, merrKursFature);
                    index++;
                    if (mesazh.Status)
                    {
                        colTrupi.Add(trupi);
                        indexRreshtImporti++; j++;
                    }
                    else
                        throw new MyException(mesazh.PershkrimMesazhi);
                }
                catch (MyException ex)
                {
                    DateTime datedok = new DateTime();
                    string msgGabimi = "";
                    indexRreshtImporti++; j++;
                    bool dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                    if (dateVlefshme)
                        msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + ": " + ex.Message;
                    else msgGabimi = ex.Message;
                    shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, kategoria, dr);
                    continue;
                }
            }
            return colTrupi;
        }

        public static clsMesazh importDokumenteshEkzekutimProdhimi(DataTable teDhenatPerImport, int idNdermarrje, int idPerdorues, ref DataTable gabime, ref DataTable tePaImportuara, string primaryKeyEkzekutim, string ndermarrjeKey, string primaryKeyProdukt, colTrupiFormatImporti col, int idKategori, ResourceManager rm, CultureInfo ci, int idGjuha, string emerTabKoka, string emerTabProd, string emerTabRec, bool importo, bool vjenNgaImportSQL, bool importAutomatik, int idNdermVit)
        {
            clsMesazh mesazh = new clsMesazh(true);
            foreach (clsTrupiFormatImporti trupi in col)
            {
                vendosVlereDefaultTeDataTable(trupi, teDhenatPerImport);
            }

            string fushatEGrupimit = "";
            DataTable dataGrupime;
            if (vjenNgaImportSQL)
            {
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, primaryKeyEkzekutim);
            }
            else
            {
                //grupojme dokumentet qe vijne si datatable sipas fushave te kokes dhe i ruajme ato tek tabela dataGrupime
                string fushaGrupimi = "";
                foreach (clsTrupiFormatImporti trupi in col)
                {
                    if (trupi.Visible && trupi.Shfaq && (trupi.FusheKokeApoTrupi == 1 || trupi.FusheKokeApoTrupi == 3))
                    {
                        //shtojme te stringu fushat e percaktuara te formatit qe jane fusha te kokes se dokumentit, per kete perjashtojme fushat e trupit
                        fushaGrupimi += trupi.EmerImporti + ";";
                    }
                }
                fushatEGrupimit = fushaGrupimi.Substring(0, fushaGrupimi.LastIndexOf(';'));//heqim pikepresjen e fundit
                string[] fushat = fushatEGrupimit.Split(';');
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, fushat);
            }
            int indexRreshtImporti = 1;
            using (DbData dbData = new DbData())
            {
                foreach (DataRow drDok in dataGrupime.Rows)
                {
                    DataTable dokumentKokTrup = null;
                    try
                    {
                        if (vjenNgaImportSQL)
                        {
                            dokumentKokTrup = teDhenatPerImport.Select(String.Format("[{0}] = '{1}'", primaryKeyEkzekutim, drDok[primaryKeyEkzekutim])).CopyToDataTable();
                            mesazh = krijoDokEkzekutimProdhimi(dokumentKokTrup, rm, ci, idNdermarrje, idPerdorues, col, gabime, tePaImportuara, importo, idNdermVit, idGjuha, vjenNgaImportSQL, ref indexRreshtImporti, primaryKeyEkzekutim, primaryKeyProdukt, ndermarrjeKey, emerTabKoka, importAutomatik, new string[0], dbData);
                        }
                        else
                        {
                            //krijojme nje tabele te re, ku vendosim dokumentin
                            string selekti = "";
                            string[] fushat = fushatEGrupimit.Split(';');
                            for (int j = 0; j < fushat.Count(); j++)
                            {
                                if (!String.IsNullOrEmpty(drDok[fushat[j]].ToString()))
                                    selekti += "[" + fushat[j] + "] = '" + drDok[fushat[j]].ToString() + "' AND ";
                            }
                            selekti += "1 = 1";
                            dokumentKokTrup = teDhenatPerImport.Select(selekti).CopyToDataTable();
                            mesazh = krijoDokEkzekutimProdhimi(dokumentKokTrup, rm, ci, idNdermarrje, idPerdorues, col, gabime, tePaImportuara, importo, idNdermVit, idGjuha, vjenNgaImportSQL, ref indexRreshtImporti, primaryKeyEkzekutim, primaryKeyProdukt, ndermarrjeKey, emerTabKoka, importAutomatik, fushat, dbData);
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            if (!mesazh.Status)
                return new clsMesazh(false, rm.GetString("msgGabimNeImportim", ci));
            return new clsMesazh(true, rm.GetString("msgImportimMeSukses", ci));
        }

        public static clsMesazh krijoDokEkzekutimProdhimi(DataTable dokTable, ResourceManager rm, CultureInfo ci, int idNdermarrje, int idPerdorues, colTrupiFormatImporti col, DataTable gabime, DataTable tePaImportuara, bool importo, int idNdermVit, int idGjuha, bool vjenNgaImportSQL, ref int indexRreshtImporti, string primaryKeyEkz, string primaryKeyProd, string ndermarrjeKey, string emerTabKoka, bool importAutomatik, string[] fushat, DbData dbData)
        {
            if (dokTable == null)
                return new clsMesazh(false);
            string error = "";
            string nrDokumentiEmerImport = "";
            string dtDokumentiEmerImport = "";
            bool kaGabim = false;
            try
            {
                nrDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Nr Dokumenti").EmerImporti;
                dtDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Date Dokumenti").EmerImporti;

                clsMesazh mesazh = new clsMesazh(true);

                string llojDokumenti = "", nrDok = "", shenime = "", njesiProdhimi = "", grupimdok1 = "", grupimdok2 = "", grupimdok3 = "", krijuesi = "";
                DateTime dtDok = new DateTime();

                error = "";
                #region Fushat e kokes
                foreach (clsTrupiFormatImporti trup in col)
                {
                    error = "";
                    switch (trup.KodKontrolli)
                    {
                        case "Lloj Dokumenti":
                            llojDokumenti = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Nr Dokumenti":
                            nrDok = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Date Dokumenti":
                            dtDok = vendosDate(trup, dokTable.Rows[0], out error);
                            if (dtDok == DateTime.MinValue)
                                dtDok = DateTime.Today;
                            break;

                        case "Shenime":
                            shenime = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Grupim Dok. 1":
                            grupimdok1 = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Grupim Dok. 2":
                            grupimdok2 = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Grupim Dok. 3":
                            grupimdok3 = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Njesi Prodhimi":
                            njesiProdhimi = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;

                        case "Krijuesi":
                            krijuesi = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                    }
                    if (error != "")
                    {
                        kaGabim = true;
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                            msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " :" + trup.EmerImporti + " duhet te jete numer!";
                        else msgGabimi = error;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, 45);
                        continue;
                    }
                }
                #endregion
                clsDatabaseShare dbShare = new clsDatabaseShare(dbData);
                clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbData);
                clsKonfigurimAmbjenti konfigAmbjenti = new clsKonfigurimAmbjenti(llojDokumenti, idNdermarrje, dbShare);
                //konfigAmbjenti.mbushKonfigAmbjSipasKod(llojDokumenti, idNdermarrje);
                if (MbylljePeriudhe.PeriodClosing.IsPeriodClosed(dtDok, dbData.MyScopeDbManager.ConnectionName, idNdermarrje, KategoriDokumenti.EkzekutimProdhimi, konfigAmbjenti.IdKonfigAmbjente))
                    throw new Exception(MessagesResource.Messages["msgPeriodIsClosed"]);
                idPerdorues = clsFunksione.kthePerdoruesPerImport(idNdermarrje, idPerdorues, krijuesi, dbAdmin);


                if (konfigAmbjenti.IdKonfigAmbjente == 0)
                    throw new Exception("Lloji i dokumentit " + llojDokumenti + " nuk ekziston!");

                clsKusht kushtHyrje = new clsKusht(konfigAmbjenti.IdKonfigAmbjente, "ZKDM");
                clsKonfigurimAmbjenti konfigHyrje = new clsKonfigurimAmbjenti(kushtHyrje.Vlera);

                clsKusht kushtDalje = new clsKusht(konfigAmbjenti.IdKonfigAmbjente, "ZKDM2");
                clsKonfigurimAmbjenti konfigDalje = new clsKonfigurimAmbjenti(kushtDalje.Vlera);

                clsKokaEkzekutim kokaEkzekutim = new clsKokaEkzekutim();
                colProduktProdhimi produktProdhimi = new colProduktProdhimi();

                bool prodsirecepture = clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "LPAPR") == "Po";
                bool isMagENjejteProd, isMagENjejteRec;

                produktProdhimi = krijoProdukteProdhimi(dokTable, idNdermarrje, idPerdorues, col, ref gabime, ref tePaImportuara, importo, nrDokumentiEmerImport, dtDokumentiEmerImport, dtDok, ref indexRreshtImporti, importAutomatik, vjenNgaImportSQL, primaryKeyProd, prodsirecepture, out isMagENjejteProd, out isMagENjejteRec);

                //clsPeriudhaKontabel per = new clsPeriudhaKontabel(dtDok, idNdermarrje);
                int idPeriudheKont = clsPeriudhaKontabel.ktheIdPeriudheSipasDatesDheNdermarrjes(dtDok, idNdermarrje);
                DateTime dtRegjistrimi = DateTime.Today;

                int idStatusDok = 0;
                bool meKontabilizim = false;

                if (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "SDI") == "Draft")
                {
                    idStatusDok = 0;
                    meKontabilizim = false;
                }
                else
                {
                    idStatusDok = 1;
                    if (clsAlternativaKushti.getAlternativa(konfigAmbjenti.IdKonfigAmbjente, "GJK") != "Jo")
                        meKontabilizim = true;
                }

                string kodMagProd = String.Empty, kodMagRec = String.Empty;
                int idMagProd = -1, idMagRec = -1;

                if (isMagENjejteProd && produktProdhimi.Count > 0)
                {
                    idMagProd = produktProdhimi[0].IdMag;
                    kodMagProd = DbCore.DbRegjistrim.clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(idMagProd);
                }

                if (isMagENjejteRec && produktProdhimi.Count > 0)
                {
                    idMagRec = produktProdhimi[0].ColReceptura[0].IdMag;
                    kodMagRec = DbCore.DbRegjistrim.clsNjesiAdministrative.ktheKodiNjesiAdministrativeSipasiDPaAutorizime(idMagRec);
                }
                mesazh = kokaEkzekutim.krijoKokaEkzekutimPerImport("EP", konfigAmbjenti, kodMagProd, kodMagRec, dtDok, nrDok, idStatusDok, idNdermarrje, idNdermVit, idPerdorues, DateTime.Today, shenime, grupimdok1, grupimdok2, grupimdok3, produktProdhimi, njesiProdhimi);

                if (!mesazh.Status)
                {
                    throw new Exception(mesazh.PershkrimMesazhi);
                }

                if (importo)
                {
                    string shfaqmesazhapolupe;
                    if (!kontrolloPerGabime(vjenNgaImportSQL, fushat, primaryKeyEkz, tePaImportuara, dokTable))
                        return new clsMesazh(false, "Ka gabime!");

                    string idDokImporti = "";
                    if (vjenNgaImportSQL)
                    {
                        idDokImporti = dokTable.Rows[0][primaryKeyEkz].ToString();
                    }
                    mesazh = kokaEkzekutim.ruaj(null, new colPlanifikimEkzekutim(), idPeriudheKont, meKontabilizim, konfigDalje, konfigHyrje, out shfaqmesazhapolupe, false, idGjuha, rm, ci, vjenNgaImportSQL, idDokImporti, emerTabKoka, primaryKeyEkz, ndermarrjeKey, false);
                    if (!mesazh.Status)
                    {
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                            msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ": " + mesazh.PershkrimMesazhi;
                        else msgGabimi = mesazh.PershkrimMesazhi;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, 45);
                    }
                }
                return mesazh;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                if (error == "")
                    error = "Gabim gjate krijimit te dokumentit!";
                DateTime datedok = new DateTime();
                string msgGabimi = "";
                bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                if (dateVlefshme)
                    msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " " + error;
                else
                    msgGabimi = error;
                shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, 45);
                return new clsMesazh(false, error);
            }
        }

        public static colProduktProdhimi krijoProdukteProdhimi(DataTable dokTrupi, int idNdermarrje, int idPerdorues, colTrupiFormatImporti col, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, string nrDokumentiEmerImport, string dtDokumentiEmerImport, DateTime dtDok, ref int indexRreshtImporti, bool importAutomatik, bool vjenNgaImportSQL, string primaryKeyProdukt, bool prodsirecepture, out bool isMagENjejteProd, out bool isMagENjejteRec)
        {
            colProduktProdhimi produktProdhimi = new colProduktProdhimi();
            DataTable dataGrupime = dokTrupi.DefaultView.ToTable(true, primaryKeyProdukt);
            DataTable dokumentKokTrup = null;
            int idMagTemp = -1;
            isMagENjejteProd = true;
            isMagENjejteRec = true;
            foreach (DataRow drDok in dataGrupime.Rows)
            {
                try
                {
                    dokumentKokTrup = dokTrupi.Select(String.Format("[{0}] = '{1}'", primaryKeyProdukt, drDok[primaryKeyProdukt])).CopyToDataTable();
                    string kodProdukti = String.Empty, njesiProdukti = String.Empty, magProdukti = String.Empty;
                    double sasiProdukti = 0, sasiPlanProdukti = 0, gjeresiPlanProdukti = 0, gjatesiPlanProdukti = 0, sasiPermaseProdukti = 0;
                    string error;
                    #region Fushat e kokes
                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        switch (trup.KodKontrolli)
                        {
                            case "Kodi i Produktit":
                                kodProdukti = vendosVlere(trup, dokumentKokTrup.Rows[0], out error);
                                break;

                            case "Njesia e Produktit":
                                njesiProdukti = vendosVlere(trup, dokumentKokTrup.Rows[0], out error);
                                break;

                            case "Magazina e Produktit":
                                magProdukti = vendosVlere(trup, dokumentKokTrup.Rows[0], out error);
                                break;

                            case "Sasia e Produktit":
                                sasiProdukti = vendosDouble(trup, dokumentKokTrup.Rows[0], out error);
                                break;

                            case "Sasia Plan e Produktit":
                                sasiPlanProdukti = vendosDouble(trup, dokumentKokTrup.Rows[0], out error);
                                break;

                            case "Gjeresi Plan":
                                gjeresiPlanProdukti = vendosDouble(trup, dokumentKokTrup.Rows[0], out error);
                                break;

                            case "Gjatesi Plan":
                                gjatesiPlanProdukti = vendosDouble(trup, dokumentKokTrup.Rows[0], out error);
                                break;

                            case "Sasi Permase":
                                sasiPermaseProdukti = vendosDouble(trup, dokumentKokTrup.Rows[0], out error);
                                break;
                        }
                        if (error != "")
                        {
                            DateTime datedok = new DateTime();
                            string msgGabimi = "";
                            bool dateVlefshme = DateTime.TryParse(dokumentKokTrup.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                            if (dateVlefshme)
                                msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " :" + trup.EmerImporti + " duhet te jete numer!";
                            else msgGabimi = error;
                            shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dokumentKokTrup.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokumentKokTrup, 45);
                            continue;
                        }
                    }
                    #endregion

                    clsProduktProdhimi produkti = new clsProduktProdhimi();
                    colRecepturaProdhimi recepturat = krijoRecepturaProdukti(dokumentKokTrup, idNdermarrje, idPerdorues, col, ref gabime, ref tePaImportuara, importo, nrDokumentiEmerImport, dtDokumentiEmerImport, dtDok, ref indexRreshtImporti, importAutomatik, vjenNgaImportSQL, kodProdukti, prodsirecepture, out isMagENjejteRec);

                    clsMesazh mesazh = produkti.krijoProduktProdhimiPerImport(kodProdukti, njesiProdukti, magProdukti, sasiProdukti, sasiPlanProdukti, gjeresiPlanProdukti, gjatesiPlanProdukti, sasiPermaseProdukti, idPerdorues, idNdermarrje, recepturat);

                    if (mesazh.Status)
                    {
                        if (idMagTemp == -1)
                            idMagTemp = produkti.IdMag;
                        else
                            if (isMagENjejteProd && produkti.IdMag != idMagTemp)
                            isMagENjejteProd = false;
                        produktProdhimi.Add(produkti);
                    }
                    else
                        throw new MyException(mesazh.PershkrimMesazhi);
                }
                catch (Exception ex)
                {
                    DateTime datedok = new DateTime();
                    string msgGabimi = "";
                    indexRreshtImporti++;
                    bool dateVlefshme = DateTime.TryParse(dokumentKokTrup.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                    if (dateVlefshme)
                        msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", " + ex.Message;
                    else msgGabimi = ex.Message;
                    shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dokumentKokTrup.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, 45);
                    continue;
                }
            }
            return produktProdhimi;
        }

        public static colRecepturaProdhimi krijoRecepturaProdukti(DataTable dokTrupi, int idNdermarrje, int idPerdorues, colTrupiFormatImporti col, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, string nrDokumentiEmerImport, string dtDokumentiEmerImport, DateTime dtDok, ref int indexRreshtImporti, bool importAutomatik, bool vjenNgaImportSQL, string kodProd, bool prodsirecepture, out bool isMagENjejteRec)
        {
            int idMagTemp = -1;
            isMagENjejteRec = true;
            string error = "";
            int j = 1;
            colRecepturaProdhimi recepturat = new colRecepturaProdhimi();
            int index = 0;
            foreach (DataRow dr in dokTrupi.Rows)
            {
                try
                {
                    error = "";
                    clsRecepturaProdhimi trupi = new clsRecepturaProdhimi();
                    string llojRecepture = "", kodRec = "", njesiRec = "", magRec = "";
                    double sasiRec = 0, sasiPlan = 0, firoPerq = 0, firoLigjore = 0;
                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        #region Recepturat
                        switch (trup.KodKontrolli)
                        {
                            case "Lloji i Receptures":
                                llojRecepture = vendosVlere(trup, dr, out error);
                                break;

                            case "Kodi i Receptures":
                                kodRec = vendosVlere(trup, dr, out error);
                                break;

                            case "Njesia e Receptures":
                                njesiRec = vendosVlere(trup, dr, out error);
                                break;

                            case "Magazina e Receptures":
                                magRec = vendosVlere(trup, dr, out error);
                                break;

                            case "Sasia e Receptures":
                                sasiRec = vendosDouble(trup, dr, out error);
                                break;

                            case "Sasia Plan e Recepturave":
                                sasiPlan = vendosDouble(trup, dr, out error);
                                break;

                            case "Firo Perqindje":
                                firoPerq = vendosDouble(trup, dr, out error);
                                break;

                            case "Firo Ligjore":
                                firoLigjore = vendosDouble(trup, dr, out error);
                                break;
                        }
                        #endregion
                        if (error != "")
                        {
                            DateTime datedok = new DateTime();
                            string msgGabimi = "";
                            bool dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                            if (dateVlefshme)
                                msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + " :" + trup.EmerImporti + " duhet te jete numer!";
                            else msgGabimi = error;
                            shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, 45, dr);
                            continue;
                        }
                    }
                    clsMesazh mesazh = new clsMesazh(true);
                    mesazh = trupi.krijoRecepturaProdhimiPerImport(kodRec, llojRecepture, njesiRec, magRec, sasiRec, sasiPlan, firoPerq, firoLigjore, idNdermarrje, idPerdorues, dtDok, kodProd, prodsirecepture);
                    index++;
                    if (mesazh.Status)
                    {
                        if (idMagTemp == -1)
                            idMagTemp = trupi.IdMag;
                        else
                            if (isMagENjejteRec && trupi.IdMag != idMagTemp)
                            isMagENjejteRec = false;
                        recepturat.Add(trupi);
                        indexRreshtImporti++; j++;
                    }
                    else
                        throw new MyException(mesazh.PershkrimMesazhi);
                }
                catch (Exception ex)
                {
                    DateTime datedok = new DateTime();
                    string msgGabimi = "";
                    indexRreshtImporti++; j++;
                    bool dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                    if (dateVlefshme)
                        msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + ": " + ex.Message;
                    else msgGabimi = ex.Message;
                    shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, 45, dr);
                    continue;
                }
            }
            return recepturat;
        }

        public static bool kontrolloPerGabime(bool vjenNgaImportSQL, string[] fushat, string primaryKey, DataTable tePaImportuara, DataTable dokTable)
        {
            if (vjenNgaImportSQL)
            {
                string idDokImporti = dokTable.Rows[0][primaryKey].ToString();
                if (tePaImportuara.Select(String.Format("[{0}] = '{1}'", primaryKey, idDokImporti)).Count() > 0)
                    return false;
            }
            else
            {
                string selekti = "";
                for (int j = 0; j < fushat.Count(); j++)
                {
                    if (!String.IsNullOrEmpty(dokTable.Rows[0][fushat[j]].ToString()))
                        selekti += "[" + fushat[j] + "] = '" + dokTable.Rows[0][fushat[j]].ToString().Replace("'", "''") + "' AND ";
                }
                selekti += "1 = 1";
                if (tePaImportuara.Select(selekti).Count() > 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        ///merr si parameter nje sql type edhe kthen tipin korespondues ne CLR
        /// </summary>
        /// <param name="sqlType"></param>
        /// <returns></returns>
        public static Type GetClrType(SqlDbType sqlType)
        {
            switch (sqlType)
            {
                case SqlDbType.BigInt:
                    return typeof(long?);

                case SqlDbType.Binary:
                case SqlDbType.Image:
                case SqlDbType.Timestamp:
                case SqlDbType.VarBinary:
                    return typeof(byte[]);

                case SqlDbType.Bit:
                    return typeof(bool?);

                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                    return typeof(string);

                case SqlDbType.DateTime:
                case SqlDbType.SmallDateTime:
                case SqlDbType.Date:
                case SqlDbType.Time:
                case SqlDbType.DateTime2:
                    return typeof(DateTime?);

                case SqlDbType.Decimal:
                case SqlDbType.Money:
                case SqlDbType.SmallMoney:
                    return typeof(decimal?);

                case SqlDbType.Float:
                    return typeof(double?);

                case SqlDbType.Int:
                    return typeof(int?);

                case SqlDbType.Real:
                    return typeof(float?);

                case SqlDbType.UniqueIdentifier:
                    return typeof(Guid?);

                case SqlDbType.SmallInt:
                    return typeof(short?);

                case SqlDbType.TinyInt:
                    return typeof(byte?);

                case SqlDbType.Variant:
                case SqlDbType.Udt:
                    return typeof(object);

                case SqlDbType.Structured:
                    return typeof(DataTable);

                case SqlDbType.DateTimeOffset:
                    return typeof(DateTimeOffset?);

                default:
                    throw new ArgumentOutOfRangeException("sqlType");
            }
        }

        /// <summary>
        /// merr si parameter nje string dhe kontrolon nese ai permban vetem karakteren alphanumeric
        /// </summary>
        /// <param name="vlera"></param>
        /// <returns></returns>
        public static bool LejoVetemAlphaNumerik(string vlera)
        {
            string pattern = "[0-9a-zA-Z]*";
            var match = Regex.Match(vlera, pattern);
            return match.Value == vlera;
        }

        /// <summary>
        /// Modifikon lidhjet e autorizimeve sipas llojit te buxhetit, lidheses, shton te reja nese ka, fshin te vjetra nese hiqen autorizime
        /// </summary>
        /// <param name="oColLidhjetAutorizimRejat"></param>
        /// <param name="llojBuxheti"></param>
        /// <param name="idLidhese"></param>
        /// <param name="colLidhjetAutorizimVjetra"></param>
        /// <param name="dbKont"></param>
        /// <returns></returns>
        internal static clsMesazh modifikoLidhjeAutorizimSipasLlojitTeBuxhetit(colLidhjetAutorizim oColLidhjetAutorizimRejat, string llojBuxheti, int idLidhese, colLidhjetAutorizim colLidhjetAutorizimVjetra, clsDatabaseKontabilitet dbKont, clsDatabaseAdmin dbAdmin, bool eshteNeTransaksion, int idperdoruesi)
        {
            clsMesazh mesazh = new clsMesazh(true);
            try
            {
                for (int i = 0; i < oColLidhjetAutorizimRejat.Count; i++)
                {
                    if (mesazh.Status)
                    {
                        int idAutorizimKoka = oColLidhjetAutorizimRejat[i].IdAutorizimeKoka;
                        if (idAutorizimKoka == -1)
                            continue;
                        oColLidhjetAutorizimRejat[i].IdLloji = clsLlojBuxheti.mbushIDLlojBuxheti(llojBuxheti, dbKont);
                        oColLidhjetAutorizimRejat[i].IdLidhese = idLidhese;
                        clsLidhjeAutorizim lidhjeNjejte = colLidhjetAutorizimVjetra.Find(x => x.IdAutorizimeKoka == idAutorizimKoka);
                        if (lidhjeNjejte != null)
                        {   //i heqim nga collectioni autorizimet qe sjane ndryshuar sepse ne te do ngelen vetem autorizimet qe do te fshihen(vendosen status 2)
                            colLidhjetAutorizimVjetra.Remove(lidhjeNjejte);
                            continue;
                        }
                        mesazh = dbAdmin.ruajLidhjeAutorizim(oColLidhjetAutorizimRejat[i].IdLidhjeAutorizim, oColLidhjetAutorizimRejat[i].IdLidhese, oColLidhjetAutorizimRejat[i].IdLloji, oColLidhjetAutorizimRejat[i].IdAutorizimeKoka, 1);
                    }
                    else
                    {
                        if (eshteNeTransaksion)
                            dbKont.rollbackTransaksion();
                        mesazh.Status = false;
                        mesazh.PershkrimMesazhi = mesazh.PershkrimMesazhi;
                        return mesazh;
                    }
                }
                //fshihen autorizimet e vjetra. Ps: Jo ato qe kane ngelur njesoj, ato do ngelen si jane

                mesazh = colLidhjetAutorizim.FshiLidhjeAutorizim(colLidhjetAutorizimVjetra, idperdoruesi, dbAdmin);
                if (!mesazh.Status)
                {
                    if (eshteNeTransaksion)
                        dbKont.rollbackTransaksion();
                    mesazh.Status = false;
                    mesazh.PershkrimMesazhi = mesazh.PershkrimMesazhi;
                    return mesazh;
                }

                return mesazh;
            }
            catch (Exception)
            {
                return new clsMesazh("Ndodhi nje gabim gjate ndryshimit te lidhjeve te autorizimeve ");
            }
        }

        public static int getIdKonfigAmbLupa(string vleraQueryString, int idNdermarrje, string kodNiveli)
        {
            int idNivel = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(kodNiveli, idNdermarrje);
            if (vleraQueryString != "")
            {
                //ketu me intereson id e nivelit
                //ne rastin kur kemi disa konfigurime te lupes per kontrollin i cili gjeneroi thirrjen e kesaj lupe
                //duhet te gjejme cili nga konfigurimet e ka idNivel sa niveli i artikujve
                //i kontrollojme me radhe te gjitha konfigurimet qe i jane kaluar ne query string
                string[] idte = vleraQueryString.Split('-');
                if (idte.Length >= 1)
                    for (int i = 0; i < idte.Length; i++)
                    {
                        int tmpIdKonfig = Convert.ToInt32(idte[i]);
                        if (clsKonfigurimAmbjenti.ktheIdNiveliSipasIdKonfigurimi(tmpIdKonfig) == idNivel)
                            return tmpIdKonfig;
                    }
            }
            return clsKonfigurimAmbjenti.ktheIdKonfigurimi(idNdermarrje, idNivel);
        }

        public static int getIdKonfigAmbLupaMeAutorizim(string vleraQueryString, int idNdermarrje, string kodNiveli, int idPerdoruesi)
        {
            int idNivel = DbCore.DbRegjistrim.clsNivelRegjistrimi.ktheIdNivelRegjistrimiSipasKodi(kodNiveli, idNdermarrje);
            if (vleraQueryString != "")
            {
                //ketu me intereson id e nivelit
                //ne rastin kur kemi disa konfigurime te lupes per kontrollin i cili gjeneroi thirrjen e kesaj lupe
                //duhet te gjejme cili nga konfigurimet e ka idNivel sa niveli i artikujve
                //i kontrollojme me radhe te gjitha konfigurimet qe i jane kaluar ne query string
                string[] idte = vleraQueryString.Split('-');
                if (idte.Length >= 1)
                    for (int i = 0; i < idte.Length; i++)
                    {
                        int tmpIdKonfig = Convert.ToInt32(idte[i]);
                        if (clsKonfigurimAmbjenti.ktheIdNiveliSipasIdKonfigurimiMeAutorizim(tmpIdKonfig, idPerdoruesi) == idNivel)
                            return tmpIdKonfig;
                    }
            }
            return clsKonfigurimAmbjenti.ktheIdKonfigurimiMeAutorizim(idNdermarrje, idNivel, idPerdoruesi);
        }

        public static string ConvertDataTabletoString(DataTable dt)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }

        /// <summary>
        /// sherben per te eksportuar filat
        /// </summary>
        /// <param name="response"></param>
        /// <param name="filearray"></param>
        /// <param name="fileName"></param>
        /// <param name="contentType"></param>
        /// <param name="type"></param>
        public static void WriteResponse(HttpResponse response, MemoryStream ms, string fileName, string contentType, string type)
        {
            response.ClearContent();
            response.Buffer = true;
            response.Cache.SetCacheability(HttpCacheability.Private);
            response.ContentType = contentType;
            ContentDisposition contentDisposition = new ContentDisposition { };
            contentDisposition.FileName = fileName + type;
            contentDisposition.DispositionType = "attachment";

            response.AddHeader("Content-Disposition", contentDisposition.ToString());
            //response.BinaryWrite(filearray);
            ms.WriteTo(response.OutputStream);


            try
            {
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                response.End();
            }
            catch (ThreadAbortException)
            {
            }
        }

        public static DataTable KtheGjitheLlojeMarreveshjesh()
        {
            using (clsDatabaseKontabilitet db = new clsDatabaseKontabilitet())
            {
                return db.ktheGjitheLlojeMarreveshjesh();
            }
        }

        /// <summary>
        /// Kthen 3 vitet e ardhshme pas vleres se vitit qe i kalon si parameter
        /// </summary>
        /// <param name="kodViti"></param>
        /// <returns></returns>
        public static DataTable ktheVitetPerProjektBuxhetet(string kodViti)
        {
            using (clsDatabaseKontabilitet db = new clsDatabaseKontabilitet())
                return db.ktheVitetPerProjektBuxhetet(kodViti);
        }


        public static clsMesazh importoPunonjes(DataTable dt, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, int idNdermVit, int idGjuha, colTrupiFormatImporti col, bool vjenNgaImportSQL, string emerTab)
        {
            try
            {

                string error = "";
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {


                    clsPunonjes punonjes = new clsPunonjes();
                    string nrpersonal = "", emer = "", mbiemer = "", atesia = "", nrsigurimesh = "", qyteti = "", adresa = "", tel = "", email = "", emerkontakti = "", mbiemerkontakti = "", adresekontakti = "", emailkontakti = "", telkontakti = "", shenimekontakti = "", objektivakosto = "", sapid = "", nrpashaporte = "", gjinia = "", kombesia = "", edukimi = "Shkolle Mesme", punameparshme = "Publik", vendodhjet = "", nrjupiter = "", username = "", shenime = "", lejepune = "";
                    string nrllogari = "";
                    int nrrendor = 0;
                    bool aktiv = true, llogaritngalistoraret = false, kryefamiljar = false;
                    DateTime datelindja = DateTime.Today;

                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        #region fushat
                        switch (trup.KodKontrolli)
                        {

                            case "Nr Personal":
                                nrpersonal = vendosVlere(trup, dr, out error);
                                break;
                            case "Emri":
                                emer = vendosVlere(trup, dr, out error);
                                break;
                            case "Atesia":
                                atesia = vendosVlere(trup, dr, out error);
                                break;
                            case "Mbiemri":
                                mbiemer = vendosVlere(trup, dr, out error);
                                break;
                            case "Datelindja":
                                datelindja = vendosDate(trup, dr, out error);
                                break;
                            case "Nr Sigurimesh":
                                nrsigurimesh = vendosVlere(trup, dr, out error);
                                break;
                            case "Qyteti":
                                qyteti = vendosVlere(trup, dr, out error);
                                break;
                            case "Adresa":
                                adresa = vendosVlere(trup, dr, out error);
                                break;
                            case "Tel":
                                tel = vendosVlere(trup, dr, out error);
                                break;
                            case "Email":
                                email = vendosVlere(trup, dr, out error);
                                break;
                            case "Aktiv":
                                aktiv = vendosBool(trup, dr, out error);
                                break;
                            case "Emer Kontakti":
                                emerkontakti = vendosVlere(trup, dr, out error);
                                break;
                            case "Mbiemer Kontakti":
                                mbiemerkontakti = vendosVlere(trup, dr, out error);
                                break;
                            case "Tel Kontakti":
                                telkontakti = vendosVlere(trup, dr, out error);
                                break;
                            case "Adrese Kontakti":
                                adresekontakti = vendosVlere(trup, dr, out error);
                                break;
                            case "Email Kontakti":
                                emailkontakti = vendosVlere(trup, dr, out error);
                                break;
                            case "Shenime Kontakti":
                                shenimekontakti = vendosVlere(trup, dr, out error);
                                break;
                            case "Objektiva e kostos":
                                objektivakosto = vendosVlere(trup, dr, out error);
                                break;
                            case "Llogarit nga listorare":
                                llogaritngalistoraret = vendosBool(trup, dr, out error);
                                break;
                            case "Sap id":
                                sapid = vendosVlere(trup, dr, out error);
                                break;
                            case "Nr Pashaporte":
                                nrpashaporte = vendosVlere(trup, dr, out error);
                                break;
                            case "Gjinia":
                                gjinia = vendosVlere(trup, dr, out error);
                                break;
                            case "Kombesia":
                                kombesia = vendosVlere(trup, dr, out error);
                                break;
                            case "Kryefamiliar":
                                kryefamiljar = vendosBool(trup, dr, out error);
                                break;
                            case "Edukimi":
                                string edu = vendosVlere(trup, dr, out error);
                                if (edu != "")
                                    edukimi = edu;
                                break;
                            case "Puna Meparshme":
                                string pun = vendosVlere(trup, dr, out error);
                                if (pun != "")
                                    punameparshme = pun;
                                break;
                            case "Vendndodhjet":
                                vendodhjet = vendosVlere(trup, dr, out error);
                                break;
                            case "Nr Jupiter":
                                nrjupiter = vendosVlere(trup, dr, out error);
                                break;
                            case "Username":
                                username = vendosVlere(trup, dr, out error);
                                break;
                            case "Shenime":
                                shenime = vendosVlere(trup, dr, out error);
                                break;
                            case "Nr Rendor":
                                nrrendor = vendosInt(trup, dr, out error);
                                break;
                            case "Leje Pune":
                                lejepune = vendosVlere(trup, dr, out error);
                                break;
                            case "Nr llogari pagese":
                                nrllogari = vendosVlere(trup, dr, out error);
                                break;

                        }

                        #endregion

                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], trup.EmerImporti + error, i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                            break;
                        }
                    }
                    if (error != "")
                        continue;


                    try
                    {
                        int idmonedha = clsNdermarrje.ktheIdMonedheNdermSipasID(idndermarje);

                        punonjes = new clsPunonjes(nrpersonal, emer, mbiemer, atesia, datelindja, nrsigurimesh, qyteti, adresa, tel, email, aktiv, emerkontakti, mbiemerkontakti, telkontakti, adresekontakti, emailkontakti, shenimekontakti, "", 1, idmonedha, idPerdorues, idndermarje, "PUN", 1, objektivakosto, llogaritngalistoraret, idPerdorues, sapid, nrpashaporte, gjinia, kombesia, kryefamiljar, edukimi, punameparshme, vendodhjet, nrjupiter, username, shenime, nrrendor, lejepune, nrllogari, "", rm, ci, idGjuha, true, hfArkiva);
                        if (importo)
                        {
                            clsMesazh mesazhinv = new clsMesazh();

                            mesazhinv = punonjes.ruaj(new Dictionary<string, object>(), ci, rm);

                            if (!mesazhinv.Status)
                            {
                                object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                                gabime.Rows.Add(arr);

                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }
                    finally
                    {
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], error, i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                        i++;
                    }
                }
            }
            catch (Exception)
            {
                return new clsMesazh(false);
            }
            return new clsMesazh(true);
        }
        public static clsMesazh importoPunesim(DataTable dt, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, int idNdermVit, int idGjuha, colTrupiFormatImporti col, bool vjenNgaImportSQL, string emerTab)
        {
            try
            {
                string error = "";
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    clsPunesim punesim = new clsPunesim();
                    string nrpersonal = "",
                        detyra = "",
                      departament = "",
                        nendepartament = "",
                        grupi = "",
                        nrkontrate = "",
                        tipkontrate = "",
                        arsyeja = "",
                        periudhaprove = "",
                        periudhanjoftimi = "",
                        profesioni = "",
                        pozicioni = "",
                        roli = "Punonjes",
                        shenime = "",
                    kodeprofesione = "",
                    ndryshimpozicioni = "";
                    bool larguar = false, standby = false, shifte = false, komisione = false, neprove = false;
                    DateTime dtaktivizimi = DateTime.Today, dtfillimi = new DateTime(), dtperfundimi = new DateTime(), dtlargimi = new DateTime(), dtnenshkrimi = new DateTime();


                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";

                        #region fushat

                        switch (trup.KodKontrolli)
                        {
                            case "Nr Personal":
                                nrpersonal = vendosVlere(trup, dr, out error);
                                break;
                            case "Detyra":
                                detyra = vendosVlere(trup, dr, out error);
                                break;
                            case "Dt Fillimi":
                                dtfillimi = vendosDate(trup, dr, out error);
                                break;
                            case "Dt Aktivizimi":
                                dtaktivizimi = vendosDate(trup, dr, out error);
                                break;
                            case "Departamenti":
                                departament = vendosVlere(trup, dr, out error);
                                break;
                            case "Nendepartamenti":
                                nendepartament = vendosVlere(trup, dr, out error);
                                break;
                            case "Grupim Punonjesish":
                                grupi = vendosVlere(trup, dr, out error);
                                break;
                            case "Nr Kontrate":
                                nrkontrate = vendosVlere(trup, dr, out error);
                                break;
                            case "Tip Kontrate":
                                tipkontrate = vendosVlere(trup, dr, out error);
                                break;
                            case "Dt Perfundimi":
                                dtperfundimi = vendosDate(trup, dr, out error);
                                break;
                            case "Larguar":
                                larguar = vendosBool(trup, dr, out error);
                                break;
                            case "Dt Largimi":
                                dtlargimi = vendosDate(trup, dr, out error);
                                break;
                            case "Arsyeja":
                                arsyeja = vendosVlere(trup, dr, out error);
                                break;
                            case "Periudha Njoftimi":
                                periudhanjoftimi = vendosVlere(trup, dr, out error);
                                break;
                            case "Ne Prove":
                                neprove = vendosBool(trup, dr, out error);
                                break;
                            case "Periudha prove":
                                periudhaprove = vendosVlere(trup, dr, out error);
                                break;
                            case "Profesioni":
                                profesioni = vendosVlere(trup, dr, out error);
                                break;
                            case "Pozicioni":
                                pozicioni = vendosVlere(trup, dr, out error);
                                break;
                            case "Kode profesione":
                                kodeprofesione = vendosVlere(trup, dr, out error);
                                break;
                            case "Shifte":
                                shifte = vendosBool(trup, dr, out error);
                                break;
                            case "Komisione":
                                komisione = vendosBool(trup, dr, out error);
                                break;
                            case "Stand by":
                                standby = vendosBool(trup, dr, out error);
                                break;
                            case "Roli":

                                string r = vendosVlere(trup, dr, out error);
                                if (r != "")
                                    roli = r;
                                break;
                            case "Ndryshim Pozicioni":
                                ndryshimpozicioni = vendosVlere(trup, dr, out error);
                                break;
                            case "Dt Nenshkrimi":
                                dtnenshkrimi = vendosDate(trup, dr, out error);
                                break;
                            case "Shenime":
                                shenime = vendosVlere(trup, dr, out error);
                                break;

                        }

                        #endregion

                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], trup.EmerImporti + error, i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                            break;
                        }
                    }
                    if (error != "")
                        continue;


                    try
                    {

                        punesim = new clsPunesim(nrpersonal, departament, nendepartament, detyra, nrkontrate, tipkontrate, dtfillimi, dtperfundimi, larguar, dtlargimi, arsyeja, periudhanjoftimi, neprove, periudhaprove, grupi, profesioni, pozicioni, shifte, standby, roli, ndryshimpozicioni, dtnenshkrimi, shenime, dtaktivizimi, idPerdorues, komisione, kodeprofesione, idndermarje, idGjuha);
                        if (importo)
                        {
                            clsMesazh mesazhinv = new clsMesazh();
                            int idpunesim = 0;
                            if (clsPunesim.ekzistonPunesimPerKetePunonjeMeKeteDateAktivizimi(nrpersonal, dtaktivizimi, out idpunesim))
                                mesazhinv = punesim.modifiko(idpunesim);
                            else mesazhinv = punesim.ruaj();

                            if (!mesazhinv.Status)
                            {
                                object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                                gabime.Rows.Add(arr);

                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }
                    finally
                    {
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], error, i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                        i++;
                    }
                }
            }
            catch (Exception)
            {
                return new clsMesazh(false);
            }
            return new clsMesazh(true);
        }
        public static clsMesazh importoQendraKostoPunonjes(DataTable dt, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, int idNdermVit, int idGjuha, colTrupiFormatImporti col, bool vjenNgaImportSQL, string emerTab)
        {
            try
            {
                string error = "";
                int i = 1;
                foreach (DataRow dr in dt.Rows)
                {
                    clsQendraKostoPunonjes qk = new clsQendraKostoPunonjes();
                    string nrpersonal = "", qk1 = "", qk2 = "", global = "", local = "";
                    DateTime dtaktivizimi = DateTime.Today;


                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";

                        #region fushat

                        switch (trup.KodKontrolli)
                        {
                            case "Nr Personal":
                                nrpersonal = vendosVlere(trup, dr, out error);
                                break;
                            case "Qendra Kosto 1":
                                qk1 = vendosVlere(trup, dr, out error);
                                break;
                            case "Qendra Kosto 2":
                                qk2 = vendosVlere(trup, dr, out error);
                                break;
                            case "Dt Aktivizimi":
                                dtaktivizimi = vendosDate(trup, dr, out error);
                                break;
                            case "Grupim Global":
                                global = vendosVlere(trup, dr, out error);
                                break;
                            case "Grupim Local":
                                local = vendosVlere(trup, dr, out error);
                                break;


                        }

                        #endregion

                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], trup.EmerImporti + error, i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                            break;
                        }
                    }
                    if (error != "")
                        continue;


                    try
                    {

                        qk = new clsQendraKostoPunonjes(nrpersonal, qk1, qk2, dtaktivizimi, idPerdorues, idndermarje);
                        if (importo)
                        {
                            clsMesazh mesazhinv = new clsMesazh();
                            int id = 0;
                            if (clsQendraKostoPunonjes.ekzistonQKPerKetePunonjeMeKeteDateAktivizimi(nrpersonal, dtaktivizimi, out id))
                                mesazhinv = qk.modifiko(id);
                            else mesazhinv = qk.ruaj();

                            if (!mesazhinv.Status)
                            {
                                object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                                gabime.Rows.Add(arr);

                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }
                    finally
                    {
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], error, i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                        i++;
                    }
                }
            }
            catch (Exception)
            {
                return new clsMesazh(false);
            }
            return new clsMesazh(true);
        }


        public static clsMesazh importoLlogari(DataTable dt, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, int idNdermVit, int idGjuha, colTrupiFormatImporti col, bool vjenNgaImportSQL, string emerTab)
        {
            clsMesazh mesazh = new clsMesazh(true);
            string error = "";

            int i = 1;
            foreach (DataRow dr in dt.Rows)
            {
                clsLlogari llog = new clsLlogari();

                string nrllog = "", emer1 = "", emer2 = "", emerFr = "", monedha = "", grupi = "", nengrupi = "", kpf1 = "", kpf2 = "", kpf3 = "", qendraKosto = "", nivelTvsh = "", objektivaKosto = "", llojQendre = "", kategoriShpenzimi = "";
                string primaryKey = "IDIMPORTSHITJE";
                string ndermarrjeKey = "";
                string shenime1 = "", shenime2 = "", shenime3 = "", shenime4 = "", shenime5 = "";

                foreach (clsTrupiFormatImporti trup in col)
                {
                    error = "";
                    #region fushat
                    switch (trup.KodKontrolli)
                    {
                        case "Kod Ndermarrje":
                            ndermarrjeKey = trup.EmerImporti;
                            break;
                        case "Numer":
                            nrllog = vendosVlere(trup, dr, out error);
                            break;
                        case "Emer Llogarie 1":
                            emer1 = vendosVlere(trup, dr, out error);
                            break;
                        case "Emer Llogarie 2":
                            emer2 = vendosVlere(trup, dr, out error);
                            break;
                        case "Monedha":
                            monedha = vendosVlere(trup, dr, out error);
                            break;
                        case "Grupi":
                            grupi = vendosVlere(trup, dr, out error);
                            break;
                        case "Nengrupi":
                            nengrupi = vendosVlere(trup, dr, out error);
                            break;
                        case "Struktura 1":
                            kpf1 = vendosVlere(trup, dr, out error);
                            break;
                        case "Struktura 2":
                            kpf2 = vendosVlere(trup, dr, out error);
                            break;
                        case "Struktura 3":
                            kpf3 = vendosVlere(trup, dr, out error);
                            break;
                        case "Qendra e Kostos":
                            qendraKosto = vendosVlere(trup, dr, out error);
                            break;
                        case "Nivel TVSH-je":
                            nivelTvsh = vendosVlere(trup, dr, out error);
                            break;
                        case "Objektiva Kosto":
                            objektivaKosto = vendosVlere(trup, dr, out error);
                            break;
                        case "Lloj Qendre":
                            llojQendre = vendosVlere(trup, dr, out error);
                            break;
                        case "Kategori Shpenzimi":
                            kategoriShpenzimi = vendosVlere(trup, dr, out error);
                            break;
                        case "Shenime 1":
                            shenime1 = vendosVlere(trup, dr, out error);
                            break;
                        case "Shenime 2":
                            shenime2 = vendosVlere(trup, dr, out error);
                            break;
                        case "Shenime 3":
                            shenime3 = vendosVlere(trup, dr, out error);
                            break;
                        case "Shenime 4":
                            shenime4 = vendosVlere(trup, dr, out error);
                            break;
                        case "Shenime 5":
                            shenime5 = vendosVlere(trup, dr, out error);
                            break;
                    }
                    #endregion

                    if (error != "")
                    {
                        object[] arr = { dr[pozicionkodi], trup.EmerImporti + " " + rm.GetString("msgDuhetTeJeteNumer", ci), i };
                        gabime.Rows.Add(arr);
                        if (importo)
                        {
                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                        break;
                    }
                }
                if (error != "")
                    continue;
                try
                {
                    llog = llog.krijoLlogariPerImport(ktheStringunPaHapesira(nrllog, true), ktheStringunPaHapesira(emer1, false), ktheStringunPaHapesira(emer2, false), ktheStringunPaHapesira(emerFr, false), qendraKosto, kpf1, kpf2, kpf3, nivelTvsh, monedha, grupi, nengrupi, idndermarje, idPerdorues, 1, objektivaKosto, llojQendre, kategoriShpenzimi, true, idGjuha, shenime1, shenime2, shenime3, shenime4, shenime5, rm, ci);

                    if (importo)
                    {
                        if (vjenNgaImportSQL)
                            mesazh = llog.ruaj(vjenNgaImportSQL, dr[primaryKey].ToString(), emerTab, primaryKey, ndermarrjeKey);
                        else
                            mesazh = llog.ruaj(vjenNgaImportSQL, "0", "", primaryKey, ndermarrjeKey);

                        if (!mesazh.Status)
                        {
                            object[] arr = { dr[pozicionkodi], mesazh.PershkrimMesazhi, i };
                            gabime.Rows.Add(arr);
                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
                finally
                {
                    if (error != "")
                    {
                        object[] arr = { dr[pozicionkodi], error, i };
                        gabime.Rows.Add(arr);
                        if (importo)
                        {
                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                    }
                    i++;
                }
            }
            if (!mesazh.Status)
                return new clsMesazh(false, rm.GetString("msgGabimNeImportim", ci));
            return new clsMesazh(true, rm.GetString("msgImportimMeSukses", ci));
        }


        public static clsMesazh importoGrupeArtikujsh(DataTable dt, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, int idNdermVit, int idGjuha, colTrupiFormatImporti col, bool vjenNgaImportSQL, string emerTab)
        {
            clsMesazh mesazh = new clsMesazh(true);
            string error = "";
            const bool shtim = true;
            int i = 1;

            foreach (DataRow dr in dt.Rows)
            {
                clsKodifikimArtikulli kod = new clsKodifikimArtikulli();

                string kodi = "", pershkrimi = "", prind = "", lloj = "";
                string primaryKey = "IDIMPORTSHITJE";
                string ndermarrjeKey = "";
                foreach (clsTrupiFormatImporti trup in col)
                {
                    error = "";
                    switch (trup.KodKontrolli)
                    {
                        case "Kod Ndermarrje":
                            ndermarrjeKey = trup.EmerImporti;
                            break;
                        case "Kodi":
                            kodi = vendosVlere(trup, dr, out error);
                            break;
                        case "Pershkrimi":
                            pershkrimi = vendosVlere(trup, dr, out error);
                            break;
                        case "Prindi":
                            prind = vendosVlere(trup, dr, out error);
                            break;
                        case "Lloji":
                            lloj = vendosVlere(trup, dr, out error);
                            break;
                    }
                    if (error != "")
                    {
                        object[] arr = { dr[pozicionkodi], trup.EmerImporti + " duhet te jete numer!", i };
                        gabime.Rows.Add(arr);
                        if (importo)
                        {
                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                        break;
                    }
                }
                if (error != "")
                    continue;

                try
                {
                    kod = kod.krijoKodifikimPerImport(DbCore.clsFunksione.ktheStringunPaHapesira(kodi, true), DbCore.clsFunksione.ktheStringunPaHapesira(pershkrimi, false), prind, lloj, idndermarje, idPerdorues, shtim, rm, ci);

                    if (importo)
                    {
                        if (vjenNgaImportSQL)
                            mesazh = kod.ruaj(vjenNgaImportSQL, dr[primaryKey].ToString(), emerTab, primaryKey, ndermarrjeKey);
                        else
                            mesazh = kod.ruaj(vjenNgaImportSQL, "0", "", primaryKey, ndermarrjeKey);

                        if (!mesazh.Status)
                        {
                            object[] arr = { dr[pozicionkodi], mesazh.PershkrimMesazhi, i };
                            gabime.Rows.Add(arr);
                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                        }
                    }
                }
                catch (Exception ex)
                {
                    error = ex.Message;
                }
                finally
                {
                    if (error != "")
                    {
                        object[] arr = { dr[pozicionkodi], error, i };
                        gabime.Rows.Add(arr);
                        if (importo)
                        {
                            tePaImportuara.ImportRow(dr);
                            gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;

                        }
                    }
                    i++;
                }
            }
            if (!mesazh.Status)
                return new clsMesazh(false, rm.GetString("msgGabimNeImportim", ci));
            return new clsMesazh(true, rm.GetString("msgImportimMeSukses", ci));
        }

        public static int gjejVendodhjenEKodit(int formati, string kod)
        {
            colTrupiFormatImporti col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDheVisible(formati, true);
            int i = 0;
            foreach (clsTrupiFormatImporti t in col)
            {
                if (t.KodKontrolli == kod)
                    return i;
                if (t.Shfaq)
                    i++;
            }
            return i;
        }
        public static string logoAmbient(string urlKomponente, string id, int idNdermarrje, int idPerdoruesi, bool Logu, String ci)
        {
            clsKomponente oKomponente = new clsKomponente(urlKomponente);
            if (Logu)
                new clsLogu(0, oKomponente.IdKomponente, idNdermarrje, idPerdoruesi, DateTime.Now, id, 0, Logu);
            if (ci.Equals("sq-AL"))
                return oKomponente.PershkrimiKomponente_sq;

            else if (ci.Equals("en-US"))
                return oKomponente.PershkrimKomponente_en;

            else return oKomponente.PershkrimKomponente_fr;
        }
        public static string[] MerrListMuajsh(int idGjuha)
        {
            if (idGjuha == 0)
                return new string[]
                 {
                    Muajt.Janar.ToString(),
                    Muajt.Shkurt.ToString(),
                    Muajt.Mars.ToString(),
                    Muajt.Prill.ToString(),
                    Muajt.Maj.ToString(),
                    Muajt.Qershor.ToString(),
                    Muajt.Korrik.ToString(),
                    Muajt.Gusht.ToString(),
                    Muajt.Shtator.ToString(),
                    Muajt.Tetor.ToString(),
                    Muajt.Nentor.ToString(),
                    Muajt.Dhjetor.ToString()
                 };
            return new string[]
               {
                    Months.January.ToString(),
                    Months.February.ToString(),
                    Months.March.ToString(),
                    Months.April.ToString(),
                    Months.May.ToString(),
                    Months.June.ToString(),
                    Months.July.ToString(),
                    Months.August.ToString(),
                    Months.September.ToString(),
                    Months.October.ToString(),
                    Months.November.ToString(),
                    Months.December.ToString()
            };

        }
        public static string MerrMuajinSipasPeriudhesDheGjuhes(clsPeriudhaKontabel periudha, int idGjuha)
        {
            if (periudha == null) return string.Empty;

            var muajiPeriudhes = periudha.FillimiPeriudha.Month;
            if (idGjuha == 1)
                return Enum.GetName(typeof(Months), muajiPeriudhes);
            return Enum.GetName(typeof(Muajt), muajiPeriudhes);

        }

        public static clsMesazh importoZbritjeAnalitike(DataTable dt, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, int idNdermVit, int idGjuha, colTrupiFormatImporti col, bool vjenNgaImportSQL, string emerTab)
        {
            try
            {
                string error = "";

                int i = 1;
                int idKonfigAmbZbritje = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("ZA", idndermarje);
                foreach (DataRow dr in dt.Rows)
                {
                    clsZbritjeAnalitike zbritja = new clsZbritjeAnalitike();

                    string niveli = "", kodArtikulli = "", llojZbritje = "", njesia1 = "", njesia2 = "";
                    decimal zbritja1 = 0, zbritja2 = 0;
                    string primaryKey = "IDIMPORTSHITJE";
                    DateTime dtFillimi = new DateTime();
                    DateTime dtMbarimi = new DateTime();

                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        #region fushat
                        switch (trup.KodKontrolli)
                        {
                            case "Niveli":
                                niveli = vendosVlere(trup, dr, out error);
                                break;
                            case "Kod Artikulli":
                                kodArtikulli = vendosVlere(trup, dr, out error);
                                break;
                            case "Lloj zbritje":
                                llojZbritje = vendosVlere(trup, dr, out error);
                                break;
                            case "Dt Fillimi":
                                dtFillimi = vendosDate(trup, dr, out error);
                                if (dtFillimi == DateTime.MinValue)
                                    dtFillimi = DateTime.Today;
                                break;
                            case "Njesia 1":
                                njesia1 = vendosVlere(trup, dr, out error);
                                break;
                            case "Njesia 2":
                                njesia2 = vendosVlere(trup, dr, out error);
                                break;
                            case "Zbritja":
                                zbritja1 = vendosDecimal(trup, dr, out error);
                                break;
                            case "Zbritja 2":
                                zbritja2 = vendosDecimal(trup, dr, out error);
                                break;
                            case "Dt Mbarimi":
                                dtMbarimi = vendosDate(trup, dr, out error);
                                if (dtMbarimi == DateTime.MinValue)
                                    dtMbarimi = DateTime.MaxValue;
                                break;
                        }
                        #endregion

                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], trup.EmerImporti + " " + rm.GetString("msgDuhetTeJeteNumer", ci), i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                            break;
                        }
                    }
                    if (error != "")
                        continue;
                    try
                    {
                        zbritja = zbritja.krijoZbritjeAnalitikePerImport(niveli, kodArtikulli, llojZbritje, njesia1, njesia2, zbritja1, zbritja2, dtFillimi, dtMbarimi, idndermarje, idPerdorues, idKonfigAmbZbritje);

                        if (importo)
                        {
                            clsMesazh mesazhinv = new clsMesazh();
                            mesazhinv = zbritja.ruaj();

                            if (!mesazhinv.Status)
                            {
                                object[] arr = { dr[pozicionkodi], mesazhinv.PershkrimMesazhi, i };
                                gabime.Rows.Add(arr);
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }
                    finally
                    {
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], error, i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                        i++;
                    }
                }
            }
            catch (Exception)
            {
                return new clsMesazh(false);
            }
            return new clsMesazh(true);
        }

        public static bool ruajStilRaporti(string styleName, HttpSessionState session, string zoomFactor, int exportFormat, int exportMode, int idPerdoruesi)
        {
            float zoom = float.Parse(zoomFactor);
            if (zoom != 100f && zoom != 115f && zoom != 130f && zoom != 140f && zoom != 160f && zoom != 185f)
                zoom = 0;
            clsMesazh mesazh = clsPerdorues.ruajStilRaporti(idPerdoruesi, styleName, zoom, exportFormat, exportMode);
            clsPerdorues perdoruesi = new clsPerdorues(idPerdoruesi);
            mySessionObjects.ruajPerdoruesNeSesion(session, perdoruesi);
            return mesazh.Status;
        }

        public static string[][] ktheVleraFiltri(string id)
        {
            int idKokaFilter;
            int.TryParse(id, out idKokaFilter);
            colFilterTrupi filtraTrupi = new colFilterTrupi(idKokaFilter);
            string[][] filtra = new string[2][];
            filtra[0] = new string[filtraTrupi.Count];
            filtra[1] = new string[filtraTrupi.Count];
            colKontrolle kontrollet = new colKontrolle();
            kontrollet.merrKontrolletKomponentes((new clsKomponente("Raporti.aspx")).IdKomponente);

            for (int i = 0; i < filtraTrupi.Count; i++)
            {
                clsKontroll kontrolli = kontrollet.merrKontrollin(filtraTrupi[i].IdKontrolli);
                filtra[0][i] = kontrolli != null ? kontrolli.KodKontrolli : "";
                filtra[1][i] = filtraTrupi[i].Vlera;
            }
            return filtra;
        }

        public static DataTable merrgjitheSpAsistenti(bool ekzekuto, List<string> idTePerEkzekutim, int idndermarrje)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            DataTable listaSpAsistenti = dbAdmin.ktheGjitheSpAsistenti();
            string objekti;
            if (!ekzekuto)
            {
                dbAdmin.Dispose();
                return listaSpAsistenti;
            }

            foreach (DataRow row in listaSpAsistenti.Rows)
            {
                if (String.IsNullOrEmpty(row.ItemArray[1].ToString()))
                    continue;
                if (idTePerEkzekutim.Any(s => row.ItemArray[0].ToString().Equals(s)))
                {
                    switch (row.ItemArray[1].ToString())
                    {
                        case "prc_Asistenti_GjejMagazinenMeSerialeTeGabuara":
                        case "prc_Asistenti_DokumentaArkabankaKursNdrysheNeTrupDheFleteKontabel":
                        case "prc_Asistenti_EvidentoDokQendraKostoTeGjeneruarMeShumeSeNjeHerePerFK":
                            objekti = dbAdmin.ekzekutoSpAsistenti(row.ItemArray[1].ToString(), idndermarrje);
                            break;
                        default:
                            objekti = dbAdmin.ekzekutoSpAsistenti(row.ItemArray[1].ToString());
                            break;
                    }
                    if (String.IsNullOrEmpty(objekti))
                        objekti = "Nuk ka asnje gabim per kete rast! :D";
                    row["Objekti"] = objekti;
                }
            }

            dbAdmin.Dispose();
            return listaSpAsistenti;
        }


        public static string ktheMuaj(int index)
        {
            switch (index)
            {
                case 1: return "Janar";
                case 2: return "Shkurt";
                case 3: return "Mars";
                case 4: return "Prill";
                case 5: return "Maj";
                case 6: return "Qershor";
                case 7: return "Korrik";
                case 8: return "Gusht";
                case 9: return "Shtator";
                case 10: return "Tetor";
                case 11: return "Nentor";
                case 12: return "Dhjetor";
                default: return "gabim";
            }

        }

        public static bool KaNdonjeVlereKolonaFloat<T>(List<T> obj, string columnName)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            PropertyInfo pro = properties.FirstOrDefault(x => x.Name.Equals(columnName, StringComparison.OrdinalIgnoreCase));

            foreach (T t in obj)
            {
                if ((float)pro.GetValue(t) != 0)
                    return true;
            }

            return false;
        }

        public static clsMesazh importoKartaKlienti(DataTable dt, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, int idNdermVit, int idGjuha, colTrupiFormatImporti col, bool vjenNgaImportSQL, string emerTab)
        {
            try
            {
                string error = "";

                int i = 1;
                int idKonfigAmbKartaKlienti = clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("KK", idndermarje);
                bool lejoMeTeNjejtinKlient = false;
                if (clsAlternativaKushti.getAlternativa(idKonfigAmbKartaKlienti, "KK1") == "Po")
                    lejoMeTeNjejtinKlient = true;

                foreach (DataRow dr in dt.Rows)
                {
                    clsKarta karta = new clsKarta();

                    string kodi = String.Empty, emri = String.Empty, email = String.Empty, kontakt = String.Empty, politike = String.Empty, kategoriZbritje = String.Empty, klienti = String.Empty, targa = String.Empty, shoferi = String.Empty, qyteti = String.Empty, adresa = String.Empty, departamenti = String.Empty, modeli = String.Empty, id = string.Empty, menyrePagese = String.Empty;
                    int gjendjePikesh = 0;
                    bool aktiv = false;
                    DateTime ditelindja = new DateTime();

                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        #region fushat
                        switch (trup.KodKontrolli)
                        {
                            case "Kodi":
                                kodi = vendosVlere(trup, dr, out error);
                                break;
                            case "Emri":
                                emri = vendosVlere(trup, dr, out error);
                                break;
                            case "Email":
                                email = vendosVlere(trup, dr, out error);
                                break;
                            case "Ditelindja":
                                ditelindja = vendosDate(trup, dr, out error);
                                if (ditelindja == DateTime.MinValue)
                                    ditelindja = DateTime.Today;
                                break;
                            case "Kontakt":
                                kontakt = vendosVlere(trup, dr, out error);
                                break;
                            case "Politike":
                                politike = vendosVlere(trup, dr, out error);
                                break;
                            case "Kategori Zbritje":
                                kategoriZbritje = vendosVlere(trup, dr, out error);
                                break;
                            case "Aktiv":
                                aktiv = vendosBool(trup, dr, out error);
                                break;
                            case "Klienti":
                                klienti = vendosVlere(trup, dr, out error);
                                break;
                            case "Targa":
                                targa = vendosVlere(trup, dr, out error);
                                break;
                            case "Shoferi":
                                shoferi = vendosVlere(trup, dr, out error);
                                break;
                            case "Qyteti":
                                qyteti = vendosVlere(trup, dr, out error);
                                break;
                            case "Adresa":
                                adresa = vendosVlere(trup, dr, out error);
                                break;
                            case "Departamenti":
                                departamenti = vendosVlere(trup, dr, out error);
                                break;
                            case "Modeli":
                                modeli = vendosVlere(trup, dr, out error);
                                break;
                            case "Id Karte":
                                id = vendosVlere(trup, dr, out error);
                                break;
                            case "Menyre Pagese":
                                menyrePagese = vendosVlere(trup, dr, out error);
                                break;
                            case "Gjendje pikesh":
                                gjendjePikesh = vendosInt(trup, dr, out error);
                                break;
                        }
                        #endregion

                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], trup.EmerImporti + " " + error, i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                            break;
                        }
                    }
                    if (error != "")
                        continue;
                    try
                    {
                        karta = clsKarta.KrijoKartePerImoprt(kodi, emri, email, kontakt, politike, kategoriZbritje, klienti, targa, shoferi, qyteti, adresa, aktiv, ditelindja, idPerdorues, idndermarje, lejoMeTeNjejtinKlient, departamenti, modeli, id, menyrePagese, gjendjePikesh);
                        if (importo)
                        {
                            karta.Ruaj();
                        }
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }
                    finally
                    {
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], error, i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                return new clsMesazh(false);
            }
            return new clsMesazh(true);
        }





        public static clsMesazh importoQytete(DataTable dt, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, int idNdermVit, int idGjuha, colTrupiFormatImporti col, bool vjenNgaImportSQL, string emerTab)
        {
            try
            {
                string error = "";

                int i = 1;

                foreach (DataRow dr in dt.Rows)
                {
                    clsQyteti qyteti = new clsQyteti();

                    string kodiqyteti = String.Empty, emriqyteti = String.Empty;


                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        #region fushat
                        switch (trup.KodKontrolli)
                        {
                            case "Kodi":
                                kodiqyteti = vendosVlere(trup, dr, out error);
                                break;
                            case "Qyteti":
                                emriqyteti = vendosVlere(trup, dr, out error);
                                break;

                        }
                        #endregion

                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], trup.EmerImporti + " " + error, i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                            break;
                        }
                    }
                    if (error != "")
                        continue;
                    try
                    {
                        int idStatusDok = 1;
                        bool shtim = true;
                        qyteti = new clsQyteti(kodiqyteti, emriqyteti, idndermarje, idPerdorues, idStatusDok, shtim);

                        if (importo)
                        {
                            clsMesazh mesazh = qyteti.ruaj();

                            if (!mesazh.Status)
                            {
                                object[] arr = { dr[pozicionkodi], mesazh.PershkrimMesazhi, i };
                                gabime.Rows.Add(arr);
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }
                    finally
                    {
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], error, i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                return new clsMesazh(false);
            }
            return new clsMesazh(true);
        }
        public static clsMesazh importoKodbareArtikulli(DataTable dt, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, int idNdermVit, int idGjuha, colTrupiFormatImporti col, bool vjenNgaImportSQL, string emerTab)
        {
            DbData dbData = new DbData();
            try
            {
                string error = "";

                int i = 1;

                foreach (DataRow dr in dt.Rows)
                {
                    clsKodbari kodbari = new clsKodbari();


                    string kodartikulli = "", pershkrimi = "", detajimi1 = "", detajimi2 = "";
                    int idnjesia = 0, llojdetajim = 0;


                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        #region fushat
                        switch (trup.KodKontrolli)
                        {
                            case "Kod artikulli":
                                kodartikulli = vendosVlere(trup, dr, out error);
                                break;
                            case "Kodbar":
                                pershkrimi = vendosVlere(trup, dr, out error);
                                break;
                            case "Njesia":
                                idnjesia = vendosInt(trup, dr, out error);
                                break;
                            case "Detajimi 1":
                                detajimi1 = vendosVlere(trup, dr, out error);
                                break;
                            case "Detajimi 2":
                                detajimi2 = vendosVlere(trup, dr, out error);
                                break;
                        }
                        #endregion

                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], trup.EmerImporti + " " + error, i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                            break;
                        }
                    }
                    if (error != "")
                        continue;
                    try
                    {
                        bool shtim = true;
                        kodbari = kodbari.krijoKodbarePerImport(kodartikulli, DbCore.clsFunksione.ktheStringunPaHapesira(pershkrimi, true), idnjesia, detajimi1, detajimi2, idndermarje, llojdetajim, rm, ci);

                        if (importo)
                        {
                            clsMesazh mesazh = kodbari.ruaj(idndermarje);

                            if (!mesazh.Status)
                            {
                                object[] arr = { dr[pozicionkodi], mesazh.PershkrimMesazhi, i };
                                gabime.Rows.Add(arr);
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        error = ex.Message;
                    }
                    finally
                    {
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], error, i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                return new clsMesazh(false);
            }
            return new clsMesazh(true);
        }

        public static clsMesazh importoGrupeKlientFurnitore(DataTable dt, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, int idNdermVit, int idGjuha, colTrupiFormatImporti col, bool vjenNgaImportSQL, string emerTab)
        {
            using (DbData dbData = new DbData())
            {
                try
                {
                    string error = "";
                    int i = 1;
                    clsDatabaseKontabilitet dbK = new clsDatabaseKontabilitet(dbData);

                    foreach (DataRow dr in dt.Rows)
                    {
                        clsGrupeKF grupimKF = new clsGrupeKF();
                        string kodGrupi = "", pershkrimGrupi = "", llojGrupimi = "", llojKF = "", prindi = "";

                        foreach (clsTrupiFormatImporti trup in col)
                        {
                            error = "";
                            #region fushat
                            switch (trup.KodKontrolli)
                            {
                                case "Kodi":
                                    kodGrupi = vendosVlere(trup, dr, out error);
                                    break;
                                case "Pershkrimi":
                                    pershkrimGrupi = vendosVlere(trup, dr, out error);
                                    break;
                                case "Prindi":
                                    prindi = vendosVlere(trup, dr, out error);
                                    break;
                                case "Lloji":
                                    llojGrupimi = vendosVlere(trup, dr, out error);
                                    break;
                                case "Lloji klient/furnitor":
                                    llojKF = vendosVlere(trup, dr, out error);
                                    break;
                            }
                            #endregion

                            if (error != "")
                            {
                                object[] arr = { dr[pozicionkodi], trup.EmerImporti + " " + error, i };
                                gabime.Rows.Add(arr);
                                if (importo)
                                {
                                    tePaImportuara.ImportRow(dr);
                                    gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                                }
                                break;
                            }
                        }
                        if (error != "")
                            continue;
                        try
                        {
                            int idStatusDok = 1;
                            grupimKF = grupimKF.KrijoKGrupeKlientFurnitorPerImport(kodGrupi.RemoveSpaces(), pershkrimGrupi.RemoveSpaces(), prindi, idPerdorues, idndermarje, idStatusDok, llojGrupimi, llojKF, dbK);

                            if (importo)
                            {
                                clsMesazh mesazh = grupimKF.Ruaj();

                                if (!mesazh.Status)
                                {
                                    object[] arr = { dr[pozicionkodi], mesazh.PershkrimMesazhi, i };
                                    gabime.Rows.Add(arr);
                                    tePaImportuara.ImportRow(dr);
                                    gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            error = ex.Message;
                        }
                        finally
                        {
                            if (error != "")
                            {
                                object[] arr = { dr[pozicionkodi], error, i };
                                gabime.Rows.Add(arr);
                                if (importo)
                                {
                                    tePaImportuara.ImportRow(dr);
                                    gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                                }
                            }
                            i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    ImbLogger.Error(ex);
                    return new clsMesazh(false);
                }
                return new clsMesazh(true);
            }
        }

        public static Dictionary<string, object> vendosObjektetDefault(colAtributeTrupi colAtrTrupi, colKontrolle colKontroll, bool shtim, string llojVeprimi, DateTime? dateDok, int idKonfigurimi, int idNdermarrje, int idPerdorues, int idKomponente, int idGjuha, DateTime? dateDokDefault)
        {
            if (!dateDok.HasValue)
                return new Dictionary<string, object>();

            int nrKontroll = colKontroll.Count;
            var objekteDefault = new Dictionary<string, object>();
            var data = (((llojVeprimi == "klonim" && clsAlternativaKushti.getAlternativa(idKonfigurimi, "VF_VM") == "Po") || (llojVeprimi == "konvertim")) && dateDokDefault.HasValue) ? dateDokDefault.Value : dateDok.Value;
            for (int i = 0; i < nrKontroll; i++)
            {
                var kontrolli = colKontroll[i];
                var atribute = colAtrTrupi[i];
                if (atribute.IdNrAutomatik != 0 && duhetNrAuto(llojVeprimi))
                {//nrAuto        
                    objekteDefault.Add(kontrolli.KodKontrolli, merrVlerenNrAutomatik(kontrolli.KodKontrolli, atribute.IdNrAutomatik, data));
                    continue; //Behet continue, sepse nuk mund te kete edhe numer automatik edhe vlere default. Numri automatik ka perparesi.
                }

                if (string.IsNullOrEmpty(atribute.VlereDefault))
                    continue;

                switch (kontrolli.KodKontrolli)
                {
                    case "btnKlienti":
                    case "btneKlientfurnitorVartes":
                        if (shtim || llojVeprimi == "konvertim" || llojVeprimi == "konvertimblerje")
                        {
                            string llojKursi = colAtrTrupi.FirstOrDefault(atrKursi => atrKursi.IdKontroll == colKontroll.FirstOrDefault(kursi => kursi.KodKontrolli == "txtKursi").IdKontrolli).VlereDefault;
                            objekteDefault.Add(kontrolli.KodKontrolli, ktheOKlientFurnitor(atribute.VlereDefault, dateDok.Value, idKonfigurimi, idNdermarrje, idPerdorues, idKomponente, idGjuha, llojKursi));
                        }
                        continue;
                    case "btneKlientFurnitori":
                        objekteDefault.Add(kontrolli.KodKontrolli, clsKlientFurnitor.KtheKlientFurnitorSipasIdNeseEkziston(Int32.Parse(atribute.VlereDefault)));
                        continue;
                    case "btnMagazina":
                    case "btneMagazina":
                    case "btneMagazina2":
                        objekteDefault.Add(kontrolli.KodKontrolli, clsNjesiAdministrative.ktheMagazineSipasIdNeseEkziston(Int32.Parse(atribute.VlereDefault)));
                        continue;
                    default:
                        break;
                }
            }
            return objekteDefault;
        }

        public static bool duhetNrAuto(string llojVeprimi)
        {

            switch (llojVeprimi)
            {
                case "shtim":
                case "klonim":
                case "shtimraport":
                case "kthim":
                case "kthimVod":
                case "bli":
                case "konvertim":
                case "konvertimblerje":
                case "rezervim":
                    return true;
                default:
                    return false;
            }
        }

        public static bool ktheVlereBool(string vlereImporti)
        {
            if (vlereImporti == "checked" || vlereImporti == "true" || vlereImporti == "po")
                return true;
            return false;
        }
        public static object merrVlerenNrAutomatik(string kodKontrolli, int idNrAuto, DateTime date)
        {
            date = date.ToLocalTime();
            clsNrAutom nrAutom = clsNrAutom.merrNumrinAutomatikSipasId(idNrAuto);
            Dictionary<String, Object> rezultati = clsNrAutom.merrVlerenNrAutomatik(nrAutom, date);
            string vleraPasardhese = rezultati["vleraPasardhese"].ToString();
            bool eshteAktiv = Convert.ToBoolean(rezultati["eshteAktivNrAutomatik"].ToString());
            if (vleraPasardhese == null)
                vleraPasardhese = "";
            long countNrFundit = 0;
            bool eshteNrDrejtFundit = false;
            if (eshteAktiv)
                eshteNrDrejtFundit = clsNrAutom.kontrolloEshteNrAutoDrejtFundit(nrAutom, date, ref countNrFundit);
            return new { kodKontrolli = kodKontrolli, idNrAuto = idNrAuto, vlereNrAuto = vleraPasardhese, EshteNrDrejtFundit = eshteNrDrejtFundit, countNrFundit = countNrFundit };
        }
        public static string merrVlerenERradhesNrAutomatik(string kodKontrolli, int idNrAuto, DateTime date)
        {
            clsNrAutom nrAutom = clsNrAutom.merrNumrinAutomatikSipasId(idNrAuto);
            Dictionary<String, Object> rezultati = clsNrAutom.merrVlerenNrAutomatik(nrAutom, date);
            string vleraPasardhese = rezultati["vleraPasardhese"].ToString();
            return vleraPasardhese ?? "";
        }

        public static object ktheOKlientFurnitor(string KlientFurnitor, DateTime date, int idKonfigurimi, int idNdermarrje, int idPerdorues, int idKomponente, int idGjuha, string llojKursi)
        {
            date = date.ToLocalTime();
            clsKlientFurnitor kf;
            if (int.TryParse(KlientFurnitor, out int idKlientFurnitor))
                kf = new clsKlientFurnitor(idKlientFurnitor);
            else
                kf = new clsKlientFurnitor(KlientFurnitor, idNdermarrje, new clsDatabaseKontabilitet());

            if (kf.IdKlientFurnitor <= 0)
                return null;
            decimal perqindje = 0;
            bool meTVSH = false;
            var taksa = kf.IdTvsh > 0 ? new clsTaksa(kf.IdTvsh) : new clsTaksa();
            colTrupatKategoriteZbritjes trupat = new colTrupatKategoriteZbritjes();
            trupat.mbushTrupatKategoriZbritjeSipasKokes(kf.IdKatZbritje);

            if (kf.IdNivelCmimi != 0)
            {
                clsNivelCmimi niv = new clsNivelCmimi(kf.IdNivelCmimi);
                if (niv.BrutoNetoNivelCmimi == 1)
                    meTVSH = true;
            }
            clsMonedha mon = new clsMonedha();
            mon.mbushMonedhePershk(kf.Monedha, idNdermarrje);
            double kursi = merrKursiSipasMonedhesDatesDheLlojit(mon.IdMonedha, date, int.TryParse(llojKursi, out int llojk) ? llojk : 1);
            decimal detyrimi = clsKlientFurnitor.MerrDetyrimKf(kf.IdKlientFurnitor, date);
            colAdresatKlientFurnitor oColAdresatKF = new colAdresatKlientFurnitor(kf.IdKlientFurnitor);
            string alternativaKushtZbritje = clsAlternativaKushti.getAlternativa(kf.IdKonfig, "ZBKF");
            object[] formateNr = ktheKonfigurimFormatNumri(idKonfigurimi, idNdermarrje, "btnKlienti", kf.IdKlientFurnitor, false, idKomponente, true, idGjuha);
            return new { kf = kf, perqindje = perqindje, idMonedha = mon.IdMonedha, meTVSH = meTVSH, detyrimi = detyrimi, oColAdresatKF = oColAdresatKF, oColTrupatKategoriteZbritjes = trupat, perqindjeagjent = kf.PerqindjeAgjenti, formatZgjedhur = (clsFormatKonfigTrup)formateNr[0], formatKursi = formateNr[1].ToString(), perqindjeagjent2 = kf.PerqindjeAgjenti2, alternativaKushtZbritje = alternativaKushtZbritje, perqindjeagjent3 = kf.PerqindjeAgjenti3, taksa = taksa, kursi = kursi };
        }

        public static object[] ktheKonfigurimFormatNumri(int idKonfigurim, int idNdermarrje, string kodKontrollKlienti, int idKlienti, bool shtim, int idKomp, bool merrFormatKursi, int idGjuha)
        {
            clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(idKonfigurim);
            int idMonedha = clsFunksione.ktheMonedhePerFormatNumri(idGjuha, idKonfigurim, idNdermarrje, idKomp, kodKontrollKlienti, idKlienti, shtim);
            clsFormatKonfigTrup formatMonedhe = new clsFormatKonfigTrup();
            if (formatNrPerKonfig.IdFormatKonfig > 0)
            {
                formatMonedhe = formatNrPerKonfig.KonfigTrupi.merrFormatSipasMonedhes(idMonedha);
                if (formatMonedhe == null)
                    formatMonedhe = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
            }
            else
                formatMonedhe = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);
            int formatKursi = 2;
            if (merrFormatKursi)
            {
                formatKursi = MerrVleraFormatKursi(idMonedha);
            }
            object[] formatet = new object[2];
            formatet[0] = formatMonedhe;
            formatet[1] = formatKursi;
            return formatet;
        }

        public static clsMesazh importoNorma(DataTable dt, ref DataTable gabime, ref DataTable tePaImportuara, bool importo, int pozicionkodi, ResourceManager rm, CultureInfo ci, int idPerdorues, int idndermarje, int idNdermVit, int idGjuha, colTrupiFormatImporti col, int kategoria)
        {
            try
            {
                string error = "";
                int i = 1;
                const bool shtim = true;
                foreach (DataRow dr in dt.Rows)
                {
                    DbCore.DbAsete.clsAseteNormaAmortizimiAbstract norma;
                    if (kategoria == 149)
                        norma = new DbCore.DbAsete.clsNormaAmortizimiRezerva();
                    else norma = new DbCore.DbAsete.clsAseteNormaAmortizimi();

                    string kodiartikulli = "", normemagazine = "", standarti = "", llojamortizimi = "";
                    DateTime dtaktivizimi = new DateTime(2000, 01, 01); //DateTime.Now.Date;

                    decimal norme = 0;
                    DateTime dateregjistrimi = DateTime.Today;

                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        error = "";
                        #region fushat
                        switch (trup.KodKontrolli)
                        {
                            case "Kod artikulli":
                                kodiartikulli = DbCore.clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Standarti i amortizimit":
                                standarti = DbCore.clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Lloji i normes":
                                normemagazine = DbCore.clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Norme":
                                norme = DbCore.clsFunksione.vendosDecimal(trup, dr, out error);
                                break;
                            case "Metode amortizimi":
                            case "Metode Amortizimi":
                                llojamortizimi = DbCore.clsFunksione.vendosVlere(trup, dr, out error);
                                break;
                            case "Date Aktivizimi":
                                dtaktivizimi = DbCore.clsFunksione.vendosDate(trup, dr, out error);
                                break;
                        }
                        #endregion

                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], trup.EmerImporti + " " + error, i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                            break;
                        }
                    }
                    if (error != "")
                        continue;
                    try
                    {
                        norma = norma.krijoPerImport(DbCore.clsFunksione.ktheStringunPaHapesira(kodiartikulli, true), llojamortizimi, standarti, normemagazine, double.Parse(norme.ToString()), idndermarje, shtim, dtaktivizimi);

                        if (importo)
                        {
                            DbCore.DbAsete.clsDatabazeAseteAbstract dbasete;
                            if (kategoria == 149)
                                dbasete = new DbCore.DbAsete.clsDatabazeAseteRezerva();
                            else
                                dbasete = new DbCore.DbAsete.clsDatabazeAsete();

                            int idgrup = dbasete.ktheIDArtikullNormaAmortizimiSipasIDArtikullStandart(norma.IdArtikulli, norma.IdStandartAmortizimi, norma.DtAktivizimi);

                            DbCore.clsMesazh mesazh;
                            if (idgrup > 0)
                                mesazh = dbasete.modifikimiArtikulliNormaAmortizimi(idgrup, norma.IdLlojAmortizimi, norma.NormeMagazine, norma.Norme);
                            else
                                mesazh = dbasete.ruajArtikulliNormaAmortizimi(out idgrup, norma.IdArtikulli, norma.IdLlojAmortizimi, norma.IdStandartAmortizimi, norma.NormeMagazine, norma.Norme, norma.DtAktivizimi);

                            dbasete.Dispose();

                            if (!mesazh.Status)
                            {
                                object[] arr = { dr[pozicionkodi], mesazh.PershkrimMesazhi, i };
                                gabime.Rows.Add(arr);
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                        error = ex.Message;
                    }
                    finally
                    {
                        if (error != "")
                        {
                            object[] arr = { dr[pozicionkodi], error, i };
                            gabime.Rows.Add(arr);
                            if (importo)
                            {
                                tePaImportuara.ImportRow(dr);
                                gabime.Rows[gabime.Rows.Count - 1][2] = tePaImportuara.Rows.Count;
                            }
                        }
                        i++;
                    }
                }
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                return new clsMesazh(false);
            }
            return new clsMesazh(true);
        }


        public static string merrStatusAprovimi(StatusAprovimi statusAprovimi, ResourceManager rm, CultureInfo ci)
        {
            switch (statusAprovimi)
            {
                case StatusAprovimi.Per_Aprovim: return rm.GetString("msgStatusPerAprovim", ci);
                case StatusAprovimi.Aprovuar: return rm.GetString("msgStatusAprovuar", ci);
                case StatusAprovimi.Deleguar: return rm.GetString("reportWatermarkDeleguar", ci);
                case StatusAprovimi.Refuzuar: return rm.GetString("msgStatusRefuzuar", ci);
                case StatusAprovimi.Modifikuar: return rm.GetString("msgModifikuar", ci);
                default: return string.Empty;
            }
        }

        public static int kthePerdoruesPerImport(int idNdermarrje, int idPerdorues, string krijuesi, clsDatabaseAdmin dbA)
        {
            if (String.IsNullOrEmpty(krijuesi))
                return idPerdorues;
            clsPerdorues krijues = new clsPerdorues(krijuesi, dbA);
            //int idKrijues = clsPerdorues.ktheIdPerdoruesSipasUsername(krijuesi, idNdermarrje);
            bool kaLicence = clsPerdorues.kaLicencePerIdNdermarje(idNdermarrje, krijues.IdPerdorues);
            if (!kaLicence)
                throw new MyException("Perdoruesi ska autorizim te kjo ndermarje!");
            return krijues.IdPerdorues;
        }

        public static String ktheNeStringTabeleTeDrejtash(HttpSessionState session)
        {
            DataTable dt = mySessionObjects.merrTeDrejtatNeSesion(session);
            if (dt.Rows.Count == 0)
                return String.Empty;
            return JsonConvert.SerializeObject(dt);
        }


        public static string merrMuaj(int muaj)
        {
            if (MessagesResource.Messages.CurrentCultureInfo.Name == "sq-AL")
                return ((Muajt)muaj).ToString();
            else
                return ((Months)muaj).ToString();

        }

        public static T merrProperty<T>(object o, string property)
        {
            return (T)(o?.GetType().GetProperty(property)?.GetValue(o, null));
        }

        /// <summary>
        /// Lexon filen e importiti te serialeve
        /// </summary>
        /// <param name="kategori">objekti kategoria e serialeve qe do importohen</param>
        /// <param name="Fushat"> collection me Fushat e importit te serialit</param>
        /// <returns></returns>
        public static DataTable LexoFileImportSerialeUnike(HttpSessionState Session, HttpPostedFile f, clsSerialeUnikeKategori kategori, colSerialeUnikeFusha Fushat)
        {
            System.Data.DataTable table = new System.Data.DataTable();

            if (f == null)
            {
                throw new MyException(MessagesResource.Messages["msgNukKeniZgjedhurAsnjeSkedar"]);

            }

            try
            {

                if (f.FileName.EndsWith(".csv") || f.FileName.EndsWith(".txt"))
                {
                    table = FileReader.FileReaderSerialeUnike(f, kategori.SimboliNdares, Fushat.Select(fushe => fushe.Emertimi).ToList(), kategori.MeEmertimKolone);
                }

            }

            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                throw new MyException(ex.Message);
            }
            try
            {
                var file = System.Web.HttpContext.Current.Server.MapPath(null) + @"\Import\" + f.FileName;
                if (File.Exists(file))
                    System.IO.File.Delete(file); //perdoret kur kemi OleDbConnection
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex, "Ndodhi nje gabim gjate fshirjes se file-t " + f.FileName + " ne folderin Import.");
                throw new MyException(MessagesResource.Messages["msgTeDhenatESkedaritTePasakta"]);
            }
            return table;
        }


        /// <summary>
        /// Kthen nje collection me artikuj te grupuar
        /// </summary>
        /// <param name="table"> datatable me seriale </param>
        /// <param name="idNdermarrje"></param>
        /// <param name="emertimi"> emertimi i kolones se datatable per kodin e artikullit</param>
        /// <returns></returns>


        public static string ktheKomponenteDefaultPerPerdorues(int idPerdorues, int idNdermarrje, string ambjentDefaultParacaktuar, string KodViti)
        {
            string ambjentiDef = clsKomponente.merrKomponenteDefaultPerdoruesiSipasLlojit(idPerdorues, false);
            if (ambjentiDef == "Mobile" || ambjentiDef == "Dashboard.aspx")
            {
                var teDrejta = new clsTeDrejtaRoli();
                int idViti = clsViti.ktheIdVitPerNdermarrjenSipasKodit(idNdermarrje, KodViti);
                teDrejta.merrTeDrejtaPerKeteKomponente(idPerdorues, idNdermarrje, idViti, ambjentiDef == "Dashboard.aspx" ? ambjentiDef : "raporteMenaxheriale");
                if (ambjentiDef == "Mobile")
                    ambjentiDef = clsKomponente.merrKomponenteDefaultPerdoruesiSipasLlojit(idPerdorues, true);
                if (!String.IsNullOrEmpty(ambjentiDef) && teDrejta.DAmb)
                    ambjentiDef = "FaqeKryesore.aspx?ambDef=" + ambjentiDef;
                else
                {
                    ambjentiDef = ambjentDefaultParacaktuar;
                    return ambjentiDef;
                }
            }

            if (!String.IsNullOrEmpty(ambjentiDef) && ambjentiDef != "Dashboard.aspx" && ambjentiDef != "CRMDefault.aspx" && ambjentiDef != "GISDefault.aspx"
                && ambjentiDef != "FaqeKryesore.aspx?ambDef=raporteMenaxheriale")
                ambjentiDef += "&shtim_modifikim=shtim";
            if (String.IsNullOrEmpty(ambjentiDef))
                ambjentiDef = ambjentDefaultParacaktuar;
            return ambjentiDef;
        }

        public static string ktheUrlMobile(clsPerdorues perdorues, clsNdermarrje ndermarrje)
        {
            byte[] toEncodeAsBytes = Encoding.ASCII.GetBytes(perdorues.PerdoruesUsername + ":" + perdorues.PerdoruesPassword);
            return $"{clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.URL_MOBILE)}?param=";
        }

        public static clsMesazh eksportAutomatikDokumentesh(string templateEksporti, int idNdermarrje, int idPerdorues, List<int> shitjepertrasferim, bool grupoTrupDokumenti)
        {
            try
            {
                if (String.IsNullOrEmpty(templateEksporti))
                    return new DbCore.clsMesazh(false, "Mungon template i eksportit! Nuk u eksportua asnje rresht!");
                DbCore.DbAdmin.clsKonfigExporti konfigEksporti = new DbCore.DbAdmin.clsKonfigExporti(templateEksporti, idNdermarrje);
                if (!DbCore.DbAdmin.clsKokaFormatImporti.ekzistonFormati(konfigEksporti.Formati))
                {
                    return new DbCore.clsMesazh(false, String.Format("( {0} ) Formati nuk ekziston!", konfigEksporti.Emer));
                }
                DbCore.DbAdmin.clsNdermarrjeViti ndervit = new DbCore.DbAdmin.clsNdermarrjeViti();
                int idNdermVit = ndervit.merrNdermarrjeVitSipasNdermarjesDheDatesAktuale(idNdermarrje);
                DbCore.DbAdmin.colTrupiFormatImporti col = DbCore.DbAdmin.colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(konfigEksporti.Formati);
                DbCore.DbAdmin.clsNdermarrje ndermarrja = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
                string primaryKey = col.filtroFormatImportiPerPrimaryKey().EmerImporti;
                string kodKontrolliTrupi = String.Empty;


                bool artikujSet = col.FirstOrDefault(x => x.KodKontrolli == "Artikulli Set").Visible == true;

                bool serialeUnike = col.FirstOrDefault(x => x.KodKontrolli.EqualsAnyIgnoreCase("Seriali Unik Kryesor", "Seriali unik dytesor")).Visible == true;
                string ids = shitjepertrasferim == null ? "" : shitjepertrasferim.Join(',', x => x.ToString());
                DataTable table = new DataTable();
                int idSuperKategori = DbCore.DbRegjistrim.clsKategoriNivelDok.mbushIDSuperKategoriNivDok(int.Parse(konfigEksporti.Kategoria.ToString()));
                switch (konfigEksporti.Kategoria.ToString())
                {
                    case "1":
                        if (grupoTrupDokumenti)
                            table = DbCore.DbRegjistrim.colKokaShitje.merrShitjeTeGrupuaraPerEksport(idNdermarrje, idPerdorues, konfigEksporti.Kategoria, idNdermVit, 2, konfigEksporti.EmerTabKoka, primaryKey, ids);
                        else
                            table = DbCore.DbRegjistrim.colKokaShitje.merrShitjePerEksport(idNdermarrje, idPerdorues, konfigEksporti.Kategoria, idNdermVit, 2, konfigEksporti.EmerTabKoka, primaryKey, ids, konfigEksporti.MerrDokTeModifikuar, konfigEksporti.MerrDokTeFshire, false, artikujSet, serialeUnike, "", true);
                        kodKontrolliTrupi = "IdRreshtiShitje";
                        break;
                    case "2":
                        if (grupoTrupDokumenti)
                            table = DbCore.DbRegjistrim.colKokaShitje.merrShitjeTeGrupuaraPerEksport(idNdermarrje, idPerdorues, 2, idNdermVit, 2, konfigEksporti.EmerTabKoka, primaryKey, ids);
                        else
                            table = DbCore.DbRegjistrim.colKokaShitje.merrShitjePerEksport(idNdermarrje, idPerdorues, 2, idNdermVit, 2, konfigEksporti.EmerTabKoka, primaryKey, ids, konfigEksporti.MerrDokTeModifikuar, konfigEksporti.MerrDokTeFshire, false, artikujSet, serialeUnike, "", true);
                        kodKontrolliTrupi = "IdRreshtiShitje";
                        break;
                    case "3":
                        table = DbCore.DbArkaBanka.colVeprimBankaKoka.kthedokVeprimeArkaBankaPerEksport(idNdermarrje, idPerdorues, idNdermVit, 3, 2, konfigEksporti.EmerTabKoka, primaryKey, String.Empty);
                        kodKontrolliTrupi = "IdRreshtiBanka";
                        break;
                    case "4":
                        table = DbCore.DbArkaBanka.colVeprimBankaKoka.kthedokVeprimeArkaBankaPerEksport(idNdermarrje, idPerdorues, idNdermVit, 4, 2, konfigEksporti.EmerTabKoka, primaryKey, String.Empty);
                        kodKontrolliTrupi = "IdRreshtiBanka";
                        break;
                    case "6":
                        table = DbCore.DbRegjistrim.colKokaMagazina.merrDokMagazinePerEksport(idNdermarrje, idPerdorues, idNdermVit, 2, konfigEksporti.EmerTabKoka, primaryKey, String.Empty, serialeUnike);
                        kodKontrolliTrupi = "IdRreshtiMagazina";
                        break;
                    case "45":
                        table = DbCore.DbProdhimi.colKokaEkzekutim.merrEkzekutimePerEksport(idNdermarrje, idPerdorues, idNdermVit, 2, konfigEksporti.EmerTabKoka, primaryKey, String.Empty);
                        kodKontrolliTrupi = "IdReceptura";
                        break;
                    case "12":
                        table = DbCore.DbKontabiliteti.colKlienteFurnitore.merrSipasKFNdermarrjesAndAutorizimeDTExport(idNdermarrje, idPerdorues, 2, konfigEksporti.EmerTabKoka, col.filtroFormatImportiSipasFushes("Kodi").EmerImporti);
                        break;
                    case "13":
                        table = DbCore.DbInventari.colArtikujt.ktheArtikujNdermarrjesAndAutorizimeDTExport(idNdermarrje, idPerdorues, false, 2, konfigEksporti.EmerTabKoka, col.filtroFormatImportiSipasFushes("Kodi").EmerImporti);
                        break;
                    case "14":
                        table = DbCore.DbKontabiliteti.colLlogarite.merrSipasLlogariteNdermarrjesAndAutorizimeExport(idNdermarrje, idPerdorues, 2, konfigEksporti.EmerTabKoka, col.filtroFormatImportiSipasFushes("Numer").EmerImporti);
                        break;
                    case "67":
                        table = DbCore.DbInventari.colKodifikimeArtikulli.merrKodifikimArtikulliExport(idNdermarrje, 2, konfigEksporti.EmerTabKoka, col.filtroFormatImportiSipasFushes("Kodi").EmerImporti);
                        break;
                }
                if (table.Rows.Count == 0)
                    return new DbCore.clsMesazh(true, String.Format("( {0} ) Nuk ka asnje rresht per te exportuar!", konfigEksporti.Emer));
                string filterPerDataTable = "";
                DataTable teDhenaPerEksport = table;

                //aplikohet filtri ne konfigurimin e formatit
                if (konfigEksporti.Filtri != 0)
                {
                    filterPerDataTable = DbCore.DbAdmin.clsFiltraExporti.ktheFilterPerDataSet(konfigEksporti.Filtri);
                    DataRow[] rreshtat = table.Select(filterPerDataTable);

                    if (rreshtat.Count() > 0)
                    {
                        teDhenaPerEksport = rreshtat.GetDataTable(table);
                    }
                    else
                        return new DbCore.clsMesazh(true, String.Format("Nuk ka asnje rresht per te exportuar!"));
                }

                //select vetem id shitjet e selektuara mbi dokumentet per eksport 
                string idShitjetTeSelektuara = ktheStringItemsTeBashkuarMeChar(shitjepertrasferim, ',');

                DataRow[] rreshtaTeSelektuarPerTransferim = teDhenaPerEksport.Select("[" + primaryKey + "] IN (" + idShitjetTeSelektuara + ")");
                if (rreshtaTeSelektuarPerTransferim.Count() > 0)
                {
                    teDhenaPerEksport = rreshtaTeSelektuarPerTransferim.GetDataTable(table);
                }
                else
                    return new DbCore.clsMesazh(true, String.Format("Nuk eshte selektuar asnje dokument i vlefshem per eksport!"));

                DataTable koka = DbCore.clsFunksione.ktheDataTableMeKokeDokumentesh(col, teDhenaPerEksport, ndermarrja.NdermarrjeKodi);
                DataTable trupi = new DataTable();
                if (idSuperKategori == (int)SuperKategori.Regjistrime)
                    trupi = DbCore.clsFunksione.ktheDataTableMeTrupaDokumentesh(col, teDhenaPerEksport, kodKontrolliTrupi, konfigEksporti.Kategoria);
                DbCore.clsMesazh mesazh = DbCore.DbImporte.colEksportSQL.ruajDokumentaNeTabeleEksporti(koka, trupi, col, konfigEksporti.EmerTabKoka, konfigEksporti.EmerTabTrupi, konfigEksporti.Kategoria, konfigEksporti.EmerTabRec, new DataTable(), idSuperKategori);
                mesazh.PershkrimMesazhi = String.Format("({0}) {1}", konfigEksporti.Emer, mesazh.PershkrimMesazhi);
                if (ndermarrja.Prind && mesazh.Status)
                {
                    foreach (DataRow row in koka.Rows)
                    {
                        string id = row.Field<string>(primaryKey);
                        mesazh = clsKokaShitje.ndryshoStatusTransferimi(int.Parse(id), StatusTrasferimi.Transferuar);
                    }
                }
                return mesazh;
            }
            catch (DbCore.MyException gabimi)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(gabimi.Message);
                return new DbCore.clsMesazh(false, gabimi.Message);
            }
            catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                return new DbCore.clsMesazh(false, "Ndodhi nje gabim gjate eksportit!");
            }
        }



        public static clsMesazh importoDokumentBuxheti(DataTable teDhenatPerImport, int idNdermarrje, int idPerdorues, ref DataTable gabime, ref DataTable tePaImportuara, string ndermarrjeKey, string primaryKey, colTrupiFormatImporti col, int idKategori, ResourceManager rm, CultureInfo ci, int idGjuha, string emerTabKoka, string emerTabTrupi, bool importo, bool vjenNgaImportSQL, bool importAutomatik, int idNdermVit)
        {
            clsMesazh mesazh = new clsMesazh(true);
            string fushatEGrupimit = "";
            DataTable dataGrupime;

            foreach (clsTrupiFormatImporti trupi in col)
                vendosVlereDefaultTeDataTable(trupi, teDhenatPerImport);

            if (vjenNgaImportSQL)
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, primaryKey);
            else
            {
                string fushaGrupimi = "";
                foreach (clsTrupiFormatImporti trupi in col)
                    if (trupi.Visible && trupi.Shfaq && (trupi.FusheKokeApoTrupi == 1 || trupi.FusheKokeApoTrupi == 3) && trupi.EmerImporti != "Totali Miratuar")
                        fushaGrupimi += trupi.EmerImporti + ";";

                fushatEGrupimit = fushaGrupimi.Substring(0, fushaGrupimi.LastIndexOf(';'));//heqim pikepresjen e fundit
                string[] fushat = fushatEGrupimit.Split(';');
                dataGrupime = teDhenatPerImport.DefaultView.ToTable(true, fushat);
            }
            int indexRreshtImporti = 1;
            using (DbData dbData = new DbData())
            {
                foreach (DataRow drDok in dataGrupime.Rows)
                {
                    DataTable dokumentKokTrup = null;
                    try
                    {
                        if (vjenNgaImportSQL)
                        {
                            dokumentKokTrup = teDhenatPerImport.Select(String.Format("[{0}] = '{1}'", primaryKey, drDok[primaryKey])).CopyToDataTable();
                            if (idKategori == 172)
                                mesazh = krijoDokAlokimBuxheti(dokumentKokTrup, rm, ci, idNdermarrje, idPerdorues, col, gabime, tePaImportuara, importo, idNdermVit, idGjuha, true, ref indexRreshtImporti, primaryKey, ndermarrjeKey, idKategori, emerTabKoka, importAutomatik, new string[0], dbData);
                            else
                                mesazh = krijoDokumentBuxheti(dokumentKokTrup, rm, ci, idNdermarrje, idPerdorues, col, gabime, tePaImportuara, importo, idNdermVit, idGjuha, true, ref indexRreshtImporti, primaryKey, ndermarrjeKey, idKategori, emerTabKoka, importAutomatik, new string[0], dbData);
                        }
                        else
                        {
                            //krijojme nje tabele te re, ku vendosim dokumentin
                            string selekti = "";
                            string[] fushat = fushatEGrupimit.Split(';');
                            for (int j = 0; j < fushat.Count(); j++)
                            {
                                if (!String.IsNullOrEmpty(drDok[fushat[j]].ToString()))
                                    selekti += "[" + fushat[j] + "] = '" + drDok[fushat[j]].ToString() + "' AND ";
                            }
                            selekti += "1 = 1";
                            dokumentKokTrup = teDhenatPerImport.Select(selekti).CopyToDataTable();
                            if (idKategori == 172)
                                mesazh = krijoDokAlokimBuxheti(dokumentKokTrup, rm, ci, idNdermarrje, idPerdorues, col, gabime, tePaImportuara, importo, idNdermVit, idGjuha, false, ref indexRreshtImporti, "", "", idKategori, "", importAutomatik, fushat, dbData);
                            else
                                mesazh = krijoDokumentBuxheti(dokumentKokTrup, rm, ci, idNdermarrje, idPerdorues, col, gabime, tePaImportuara, importo, idNdermVit, idGjuha, false, ref indexRreshtImporti, "", "", idKategori, "", importAutomatik, fushat, dbData);
                        }
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
            if (!mesazh.Status)
                return new clsMesazh(false, rm.GetString("msgGabimNeImportim", ci));
            return new clsMesazh(true, rm.GetString("msgImportimMeSukses", ci));
        }

        public static clsMesazh krijoDokumentBuxheti(DataTable dokTable, ResourceManager rm, CultureInfo ci, int idNdermarrje, int idPerdorues, colTrupiFormatImporti col, DataTable gabime, DataTable tePaImportuara, bool importo, int idNdermVit, int idGjuha, bool vjenNgaImportSQL, ref int indexRreshtImporti, string primaryKey, string ndermarrjeKey, int kategoria, string emerTabKoka, bool importAutomatik, string[] fushat, DbData dbData)
        {
            if (dokTable == null)
                return new clsMesazh(false);
            string error = "";
            string nrDokumentiEmerImport = "";
            string dtDokumentiEmerImport = "";
            try
            {
                nrDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Nr dokumenti").EmerImporti;
                dtDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Date dokumenti").EmerImporti;

                clsMesazh mesazh = new clsMesazh(true);
                string nenkategoria = String.Empty, llojDok = String.Empty, nrDok = String.Empty, llojVeprimi = String.Empty, entiteti = String.Empty, shenime = String.Empty, krijuesi = String.Empty, kodNdermarrje = String.Empty;
                DateTime dtDok = new DateTime();
                error = "";
                int viti = 0;
                decimal buxhetiMiratuar = 0;

                #region Fushat e kokes
                foreach (clsTrupiFormatImporti trup in col)
                {
                    error = "";
                    switch (trup.KodKontrolli)
                    {
                        case "Nenkategoria":
                            nenkategoria = DbCore.clsFunksione.ktheStringunPaHapesira(DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error), true);
                            break;
                        case "Lloj dok":
                            llojDok = DbCore.clsFunksione.ktheStringunPaHapesira(DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error), true);
                            break;
                        case "Nr dokumenti":
                            nrDok = DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Date dokumenti":
                            dtDok = DbCore.clsFunksione.vendosDate(trup, dokTable.Rows[0], out error);
                            if (dtDok == DateTime.MinValue)
                                dtDok = DateTime.Today;
                            break;
                        case "Lloj veprimi":
                            llojVeprimi = DbCore.clsFunksione.ktheStringunPaHapesira(vendosVlere(trup, dokTable.Rows[0], out error), true);
                            break;
                        case "Entiteti":
                            entiteti = DbCore.clsFunksione.ktheStringunPaHapesira(vendosVlere(trup, dokTable.Rows[0], out error), true);
                            break;
                        case "Shenime":
                            shenime = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Kod Ndermarrje":
                            kodNdermarrje = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Viti":
                            viti = Convert.ToInt32(vendosVlere(trup, dokTable.Rows[0], out error));
                            break;
                        case "Totali Miratuar":
                            buxhetiMiratuar = vendosDecimal(trup, dokTable.Rows[0], out error);
                            break;

                    }
                    if (error != "")
                    {
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                            msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " :" + trup.EmerImporti + " duhet te jete numer!";
                        else msgGabimi = error;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, kategoria);
                        continue;
                    }
                }
                #endregion
                viti = viti != 0 ? viti : dtDok.Year;
                var vitImporti = clsNdermarrjeViti.ktheKodVitiSipasIdNdermViti(idNdermVit);
                if (Convert.ToInt32(vitImporti) != viti)
                    throw new Exception("Data e dokumentit nuk i perket vitit ku po behet importi.");

                ClsBKokaBuxheti koka = new ClsBKokaBuxheti();
                ColBTrupiBuxheti colTrupi = new ColBTrupiBuxheti();
                clsDatabaseShare dbShare = new clsDatabaseShare(dbData);
                clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbData);
                clsKonfigurimAmbjenti konfigAmbjenti = new clsKonfigurimAmbjenti(llojDok, idNdermarrje, dbShare);
                bool kaAutorizim = clsKonfigurimAmbjenti.getAutorizimKonfigurimi(konfigAmbjenti.IdKonfigAmbjente, idPerdorues, dbShare);
                if (!kaAutorizim)
                    throw new Exception($"Perdoruesi nuk ka autorizim per llojin e dokumentit {konfigAmbjenti.KodKonfigAmbjente}.");

                object[] nivele = new object[dokTable.Rows.Count];

                int idPerdoruesPerKontroll = idPerdorues;
                idPerdorues = clsFunksione.kthePerdoruesPerImport(idNdermarrje, idPerdorues, krijuesi, dbAdmin);

                colTrupi = krijoTrupDokPerfitimBuxheti(dokTable, idNdermarrje, idPerdorues, col, gabime, tePaImportuara, importo, ref nivele, nrDokumentiEmerImport, dtDokumentiEmerImport, ref indexRreshtImporti, vjenNgaImportSQL, kategoria, dbAdmin);

                koka.krijoKokeDokumentBuxhetiPerImport(idNdermarrje, idPerdorues, kategoria, idNdermVit, nenkategoria, llojDok, nrDok, dtDok, llojVeprimi, entiteti, viti, buxhetiMiratuar, shenime, colTrupi);
                mesazh = koka.Valido();
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);

                if (importo)
                {
                    if (!kontrolloPerGabime(vjenNgaImportSQL, fushat, primaryKey, tePaImportuara, dokTable))
                        return new clsMesazh(false, "Ka gabime!");

                    string idDokImporti = "";
                    if (vjenNgaImportSQL)
                        idDokImporti = dokTable.Rows[0][primaryKey].ToString();

                    koka.VjenNgaImportSql = vjenNgaImportSQL;
                    koka.IdDokImporti = idDokImporti;
                    koka.TabeleKokeImporti = emerTabKoka;

                    mesazh = koka.Ruaj();

                    if (!mesazh.Status)
                    {
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                            msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ": " + mesazh.PershkrimMesazhi;
                        else msgGabimi = mesazh.PershkrimMesazhi;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, kategoria);
                    }
                }
                return mesazh;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                if (error == "")
                    error = "Gabim gjate krijimit te dokumentit!";
                DateTime datedok = new DateTime();
                string msgGabimi = "";
                bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                if (dateVlefshme)
                    msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " " + error;
                else
                    msgGabimi = error;
                shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, kategoria);
                return new clsMesazh(false, error);
            }
        }

        public static ColBTrupiBuxheti krijoTrupDokPerfitimBuxheti(DataTable dokTrupi, int idNdermarrje, int idPerdorues, colTrupiFormatImporti col, DataTable gabime, DataTable tePaImportuara, bool importo, ref object[] nivele, string nrDokumentiEmerImport, string dtDokumentiEmerImport, ref int indexRreshtImporti, bool vjenNgaImportSQL, int kategoria, clsDatabaseAdmin dbAdmin)
        {
            string error = "";
            int j = 1;
            ColBTrupiBuxheti colTrupi = new ColBTrupiBuxheti();

            nivele = new object[dokTrupi.Rows.Count];
            int index = 0;
            ColBKategoriBuxhetimi colKategoriBuxhetimi = new ColBKategoriBuxhetimi();
            if (dokTrupi.Rows.Count > 0 && (kategoria == 181 || kategoria == 175 || kategoria == 179)) //clsKokaBuxheti.IdKatDok == 177 perfitimi
                colKategoriBuxhetimi = ColBKategoriBuxhetimi.merrKategoriBuxhetimiAktiveSipasNdermarrjes(idNdermarrje);
            else
                if (dokTrupi.Rows.Count > 0 && kategoria == 170)
                colKategoriBuxhetimi.MbushKategoriBuxhetimiSipasNdermarrjesDheBijave(idNdermarrje);

            foreach (DataRow dr in dokTrupi.Rows)
            {
                try
                {
                    error = "";
                    ClsBTrupiBuxheti trupi = new ClsBTrupiBuxheti();
                    string lloji = String.Empty, zeri = String.Empty, buxheti = String.Empty, tvsh = String.Empty, artikullBuxhetimi = String.Empty, muaji = string.Empty,
                        ndermarrjeNiveli3 = string.Empty, ndermarrjeNiveli2 = string.Empty;
                    Decimal vleraPaTVSH = 0, vleraMeTVSH = 0;
                    decimal? sasia = null, cmimi = null;


                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        #region VendosVlereFushaTrupi
                        switch (trup.KodKontrolli)
                        {
                            case "Lloji":
                                lloji = DbCore.clsFunksione.ktheStringunPaHapesira(vendosVlere(trup, dr, out error), true);
                                break;
                            case "Zeri":
                                zeri = DbCore.clsFunksione.ktheStringunPaHapesira(vendosVlere(trup, dr, out error), true);
                                break;
                            case "Buxheti":
                                buxheti = DbCore.clsFunksione.ktheStringunPaHapesira(vendosVlere(trup, dr, out error), true);
                                break;
                            case "Vlera pa TVSH":
                            case "Vlera":
                                vleraPaTVSH = vendosDecimal(trup, dr, out error);
                                break;
                            case "TVSH":
                                tvsh = DbCore.clsFunksione.ktheStringunPaHapesira(vendosVlere(trup, dr, out error), true);
                                break;
                            case "Vlera me TVSH":
                                vleraMeTVSH = vendosDecimal(trup, dr, out error);
                                break;
                            case "Artikuj Buxhetimi":
                            case "Artikull Buxhetimi":
                                artikullBuxhetimi = DbCore.clsFunksione.ktheStringunPaHapesira(vendosVlere(trup, dr, out error), true);
                                break;
                            case "Sasia":
                                sasia = vendosDecimal(trup, dr, out error);
                                break;
                            case "Cmimi":
                                cmimi = vendosDecimal(trup, dr, out error);
                                break;
                            case "Periudha":
                                muaji = DbCore.clsFunksione.ktheStringunPaHapesira(vendosVlere(trup, dr, out error), true);
                                break;
                            case "NdermarrjeNiveli3":
                                ndermarrjeNiveli3 = DbCore.clsFunksione.ktheStringunPaHapesira(vendosVlere(trup, dr, out error), true);
                                break;
                            case "NdermarrjeNiveli2":
                                ndermarrjeNiveli2 = DbCore.clsFunksione.ktheStringunPaHapesira(vendosVlere(trup, dr, out error), true);
                                break;
                        }
                        #endregion
                        if (error != "")
                        {
                            DateTime datedok = new DateTime();
                            string msgGabimi = "";
                            bool dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                            if (dateVlefshme)
                                msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + " :" + trup.EmerImporti + " duhet te jete numer!";
                            else msgGabimi = error;
                            shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, kategoria, dr);
                            continue;
                        }
                    }
                    clsMesazh mesazh = new clsMesazh();
                    if (error == "")
                    {
                        mesazh = trupi.krijoTrupDokumentBuxhetiNgaImporti(lloji, zeri, buxheti, tvsh, vleraPaTVSH, vleraMeTVSH, artikullBuxhetimi, colKategoriBuxhetimi, ref nivele, idNdermarrje, idPerdorues, index, kategoria, sasia, cmimi, muaji, ndermarrjeNiveli3, ndermarrjeNiveli2, dbAdmin);

                    }

                    index++;
                    if (mesazh.Status)
                    {
                        colTrupi.Add(trupi);
                        indexRreshtImporti++; j++;
                    }
                    else
                        throw new MyException(mesazh.PershkrimMesazhi);
                }
                catch (MyException ex)
                {
                    DateTime datedok = new DateTime();
                    string msgGabimi = "";
                    indexRreshtImporti++; j++;
                    bool dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                    if (dateVlefshme)
                        msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + ": " + ex.Message;
                    else msgGabimi = ex.Message;
                    shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, kategoria, dr);
                    continue;
                }
            }
            return colTrupi;
        }

        public static clsMesazh krijoDokAlokimBuxheti(DataTable dokTable, ResourceManager rm, CultureInfo ci, int idNdermarrje, int idPerdorues, colTrupiFormatImporti col, DataTable gabime, DataTable tePaImportuara, bool importo, int idNdermVit, int idGjuha, bool vjenNgaImportSQL, ref int indexRreshtImporti, string primaryKey, string ndermarrjeKey, int kategoria, string emerTabKoka, bool importAutomatik, string[] fushat, DbData dbData)
        {
            if (dokTable == null)
                return new clsMesazh(false);
            string error = "";
            string nrDokumentiEmerImport = "";
            string dtDokumentiEmerImport = "";
            try
            {
                nrDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Nr dokumenti").EmerImporti;
                dtDokumentiEmerImport = col.filtroFormatImportiSipasFushes("Date dokumenti").EmerImporti;

                clsMesazh mesazh = new clsMesazh(true);
                error = "";
                int viti = DateTime.Today.Year;
                DateTime dtDok = new DateTime();
                Decimal buxhetiMiratuar = 0;
                string nenkategoria = String.Empty, llojDok = String.Empty, nrDok = String.Empty, shenime = String.Empty;

                #region Fushat e kokes
                foreach (clsTrupiFormatImporti trup in col)
                {
                    error = "";
                    switch (trup.KodKontrolli)
                    {
                        case "Nenkategoria":
                            nenkategoria = DbCore.clsFunksione.ktheStringunPaHapesira(DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error), true);
                            break;
                        case "Lloj dok":
                            llojDok = DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Nr dokumenti":
                            nrDok = DbCore.clsFunksione.vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Date dokumenti":
                            dtDok = DbCore.clsFunksione.vendosDate(trup, dokTable.Rows[0], out error);
                            if (dtDok == DateTime.MinValue)
                                dtDok = DateTime.Today;
                            break;
                        case "Viti":
                            viti = vendosInt(trup, dokTable.Rows[0], out error);
                            break;
                        case "Shenime":
                            shenime = vendosVlere(trup, dokTable.Rows[0], out error);
                            break;
                        case "Buxheti i Miratuar":
                            buxhetiMiratuar = vendosDecimal(trup, dokTable.Rows[0], out error);
                            break;
                    }
                    if (error != "")
                    {
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                            msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " :" + trup.EmerImporti + " duhet te jete numer!";
                        else msgGabimi = error;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, kategoria);
                        continue;
                    }
                }
                #endregion

                var vitImporti = clsNdermarrjeViti.ktheKodVitiSipasIdNdermViti(idNdermVit);
                if (Convert.ToInt32(vitImporti) != dtDok.Year)
                    throw new Exception("Data e dokumentit nuk i perket vitit ku po behet importi.");

                ClsBKokaBuxheti koka = new ClsBKokaBuxheti();
                ColBTrupiBuxheti colTrupi = new ColBTrupiBuxheti();
                clsDatabaseShare dbShare = new clsDatabaseShare(dbData);
                clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin(dbData);
                clsKonfigurimAmbjenti konfigAmbjenti = new clsKonfigurimAmbjenti(llojDok, idNdermarrje, dbShare);
                bool kaAutorizim = clsKonfigurimAmbjenti.getAutorizimKonfigurimi(konfigAmbjenti.IdKonfigAmbjente, idPerdorues, dbShare);
                if (!kaAutorizim)
                    throw new Exception($"Perdoruesi nuk ka autorizim per llojin e dokumentit {konfigAmbjenti.KodKonfigAmbjente}.");

                object[] nivele = new object[dokTable.Rows.Count];

                colTrupi = krijoTrupDokAlokimBuxheti(dokTable, idNdermarrje, idPerdorues, col, gabime, tePaImportuara, importo, ref nivele, nrDokumentiEmerImport, dtDokumentiEmerImport, ref indexRreshtImporti, vjenNgaImportSQL, kategoria);

                koka.krijoKokeDokumentBuxhetiPerImport(idNdermarrje, idPerdorues, 172, idNdermVit, nenkategoria, llojDok, nrDok, dtDok, "", "", viti, buxhetiMiratuar, shenime, colTrupi);
                mesazh = koka.Valido();
                if (!mesazh.Status)
                    throw new Exception(mesazh.PershkrimMesazhi);

                if (importo)
                {
                    if (!kontrolloPerGabime(vjenNgaImportSQL, fushat, primaryKey, tePaImportuara, dokTable))
                        return new clsMesazh(false, "Ka gabime!");

                    string idDokImporti = "";
                    if (vjenNgaImportSQL)
                        idDokImporti = dokTable.Rows[0][primaryKey].ToString();

                    mesazh = koka.Ruaj();

                    if (!mesazh.Status)
                    {
                        DateTime datedok = new DateTime();
                        string msgGabimi = "";
                        bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                        if (dateVlefshme)
                            msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ": " + mesazh.PershkrimMesazhi;
                        else msgGabimi = mesazh.PershkrimMesazhi;
                        shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, kategoria);
                    }
                }
                return mesazh;
            }
            catch (Exception ex)
            {
                error = ex.Message;
                if (error == "")
                    error = "Gabim gjate krijimit te dokumentit!";
                DateTime datedok = new DateTime();
                string msgGabimi = "";
                bool dateVlefshme = DateTime.TryParse(dokTable.Rows[0][dtDokumentiEmerImport].ToString(), out datedok);
                if (dateVlefshme)
                    msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + " " + error;
                else
                    msgGabimi = error;
                shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti - 1, importo, msgGabimi, dokTable.Rows[0][nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTable, kategoria);
                return new clsMesazh(false, error);
            }
        }

        public static ColBTrupiBuxheti krijoTrupDokAlokimBuxheti(DataTable dokTrupi, int idNdermarrje, int idPerdorues, colTrupiFormatImporti col, DataTable gabime, DataTable tePaImportuara, bool importo, ref object[] nivele, string nrDokumentiEmerImport, string dtDokumentiEmerImport, ref int indexRreshtImporti, bool vjenNgaImportSQL, int kategoria)
        {
            string error = "";
            int j = 1;
            ColBTrupiBuxheti colTrupi = new ColBTrupiBuxheti();
            nivele = new object[dokTrupi.Rows.Count];
            int index = 0;
            foreach (DataRow dr in dokTrupi.Rows)
            {
                try
                {
                    error = "";
                    ColBTrupiBuxheti trupiMuajt = new ColBTrupiBuxheti();
                    string prindi = String.Empty, analize = String.Empty, buxheti = String.Empty, pershkrimi = String.Empty;
                    //janar = String.Empty, shkurt = String.Empty, mars = String.Empty, prill = String.Empty, maj = String.Empty, qershor = String.Empty, korrik = String.Empty, gusht = String.Empty, shtator = String.Empty, tetor = String.Empty, nentor = String.Empty, dhjetor = String.Empty;
                    string[] muajt = new string[12];
                    Decimal miratuar = 0;

                    for (int i = 1; i <= 12; i++)//shtimi i kolonave te muajve
                    {
                        clsTrupiFormatImporti muaji = new clsTrupiFormatImporti();
                        muaji.Visible = true;
                        muaji.Shfaq = true;
                        muaji.EmerImporti = muaji.KodKontrolli = ((DbCore.DbListPagesat.Muajt)i).ToString();
                        col.Add(muaji);
                    }

                    foreach (clsTrupiFormatImporti trup in col)
                    {
                        #region VendosVlereFushaTrupi
                        switch (trup.KodKontrolli)
                        {
                            case "Miratuar":
                                miratuar = vendosDecimal(trup, dr, out error);
                                break;
                            case "Pershkrimi":
                                pershkrimi = vendosVlere(trup, dr, out error);
                                break;
                            case "Buxheti":
                                buxheti = vendosVlere(trup, dr, out error);
                                break;
                            case "Prindi":
                                prindi = vendosVlere(trup, dr, out error);
                                break;
                            case "Analize":
                                analize = vendosVlere(trup, dr, out error);
                                break;
                            default:
                                if (Enum.IsDefined(typeof(DbCore.DbListPagesat.Muajt), trup.KodKontrolli))
                                    muajt[Convert.ToInt32(Enum.Parse(typeof(DbCore.DbListPagesat.Muajt), trup.KodKontrolli)) - 1] = vendosVlere(trup, dr, out error);
                                break;

                        }
                        #endregion
                        if (error != "")
                        {
                            DateTime datedok = new DateTime();
                            string msgGabimi = "";
                            bool dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                            if (dateVlefshme)
                                msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + " :" + trup.EmerImporti + " duhet te jete numer!";
                            else msgGabimi = error;
                            shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, kategoria, dr);
                            continue;
                        }
                    }
                    clsMesazh mesazh = new clsMesazh();
                    if (error == "")
                        mesazh = trupiMuajt.krijoTrupSipasMuajveAlokimBuxhetiNgaImporti(idNdermarrje, miratuar, pershkrimi, buxheti, prindi, analize, muajt);
                    index++;
                    if (mesazh.Status)
                    {
                        foreach (ClsBTrupiBuxheti t in trupiMuajt)
                            colTrupi.Add(t);
                        indexRreshtImporti++; j++;
                    }
                    else
                        throw new MyException(mesazh.PershkrimMesazhi);
                }
                catch (MyException ex)
                {
                    DateTime datedok = new DateTime();
                    string msgGabimi = "";
                    indexRreshtImporti++; j++;
                    bool dateVlefshme = DateTime.TryParse(dr[dtDokumentiEmerImport].ToString(), out datedok);
                    if (dateVlefshme)
                        msgGabimi = "Date Dokumenti " + datedok.Day + "/" + datedok.Month + "/" + datedok.Year + ", Rreshti " + j + ": " + ex.Message;
                    else msgGabimi = ex.Message;
                    shtoGabimeNeDataTable(ref gabime, ref tePaImportuara, indexRreshtImporti, importo, msgGabimi, dr[nrDokumentiEmerImport].ToString(), vjenNgaImportSQL, dokTrupi, kategoria, dr);
                    continue;
                }
            }
            return colTrupi;
        }



        public static string ktheStringItemsTeBashkuarMeChar(List<int> lista, char karakteri)
        {
            string result = "";
            foreach (var elem in lista)
            {
                result += elem.ToString() + karakteri;
            }
            result = result.TrimEnd(karakteri);

            return result;
        }
        public static string GjeneroIIC(clsNdermarrje nderm, string txtNumer, string txtTotal1, string cashRegister, string softNum)
        {


            if (nderm.Pathname.ToString() == "")
                return "Ju lutem ngarkoni certifikaten e sigurise!";

            string passpath = nderm.Pathname + $"/password.txt";
            String KEYSTORE_PASS = "";

            byte[] encrypted;
            String KEYSTORE_LOCATION = System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"certifikata.p12";
            if (File.Exists(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt"))
                KEYSTORE_PASS = File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath(nderm.Pathname) + @"password.txt");
            else return "Ju lutem ngarkoni filen e passwordit!";

            String iicInput = "";
            string iicString = "";
            // issuerNuis
            iicInput += "|" + nderm.NdermarrjeNipt;
            // dateTimeCreated
            iicInput += "|" + DateTime.Now.ToString();
            // invoiceNumber
            iicInput += "|" + txtNumer;
            // busiUnit
            iicInput += "|" + nderm.Kodbiznesi;
            // cashRegister
            iicInput += "|" + cashRegister;
            // softNum
            iicInput += "|" + softNum;
            // totalPrice
            iicInput += "|" + Math.Round((Convert.ToDouble(txtTotal1)), 2).ToString();

            using (X509Certificate2 keyStore = new X509Certificate2(KEYSTORE_LOCATION, KEYSTORE_PASS))
            {
                try
                {
                    // Load a private from a key store
                    RSA privateKey = keyStore.GetRSAPrivateKey();
                    // Create IIC signature according to RSASSA-PKCS-v1_5
                    byte[] iicSignature = privateKey.SignData(Encoding.ASCII.GetBytes(iicInput), HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
                    string iicSignatureString = BitConverter.ToString(iicSignature).Replace("-", string.Empty);
                    Console.WriteLine("The IIC signature is: " + iicSignatureString);
                    // Hash IIC signature with MD5 to create IIC
                    byte[] iicb = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(iicSignature);
                    iicString = BitConverter.ToString(iicb).Replace("-", string.Empty);
                    Console.WriteLine("The IIC is: " + iicString);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return iicString;
        }


        public static Tuple<string, string, string> MesazhetInformuese(List<string> teFshire,
            List<string> teLidhur,
            List<string> tePaAutorizuar,
            List<string> periudheKycur,
            List<string> gjendjeNegative,
            List<string> procesaprovimi,
            List<string> tePaFshire,
            List<string> rivleresim,
            List<string> meStatusRuajtur,
            List<string> meStatusKonvertuar,
            List<string> meGjenerimTeLidhur,
            Dictionary<string, List<String>> paTeDrejta,
            List<string> closedPeriod,
            ResourceManager rm,
            CultureInfo cultinf, List<string> tePafiskalizuar)
        {
            string mesazhGabim = "",
                mesazhGabimLidhur = "",
                mesazhGabimPeridheKycur = "",
                mesazhSukses = "",
                mesazhGjendjeNegative = "",
                mesazhProcesAprovimi = "",
                mesazhRivleresim = "",
                mesazhMeStatusRuajtur = "",
                mesazhMeStatusKonvertuar = "",
                mesazhMeGjenerimTeLidhur = "",
                mesazhTeDrejta = "",
                mesazhGabimAutorizim = "",
                mesazhClosedPeriod = "",
                mesazhGabimFiskalizimi = "";

            if (teLidhur.Count == 1)
                mesazhGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", cultinf), String.Join(", ", teLidhur), rm.GetString("regjMagSuffixMesazhNjejesLidhurGabimi", cultinf));
            else if (teLidhur.Count > 1)
                mesazhGabimLidhur = String.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", cultinf), String.Join(", ", teLidhur), rm.GetString("regjMagSuffixMesazhShumesLidhurGabimi", cultinf));
            if (tePaAutorizuar.Count == 1)
                mesazhGabimAutorizim = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", cultinf), String.Join(", ", tePaAutorizuar), rm.GetString("regjMagSuffixMesazhNjejesAutorizimGabimi", cultinf));
            else if (tePaAutorizuar.Count > 1)
                mesazhGabimAutorizim = String.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", cultinf), String.Join(", ", tePaAutorizuar), rm.GetString("regjMagSuffixMesazhShumesAutorizimGabimi", cultinf));
            if (tePafiskalizuar.Count == 1)
                mesazhGabimFiskalizimi = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", cultinf), String.Join(", ", tePafiskalizuar), " eshte i fiskalizuar");
            else if (tePafiskalizuar.Count > 1)
                mesazhGabimFiskalizimi = String.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", cultinf), String.Join(", ", tePafiskalizuar), " jane te fiskalizuar");
            if (periudheKycur.Count == 1)
                mesazhGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", cultinf), String.Join(", ", periudheKycur), rm.GetString("regjMagSuffixMesazhNjejesPeriudheKycurGabimi", cultinf));
            else if (periudheKycur.Count > 1)
                mesazhGabimPeridheKycur = String.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", cultinf), String.Join(", ", periudheKycur), rm.GetString("regjMagSuffixMesazhNjejesPeriudheKycurGabimi", cultinf));
            if (gjendjeNegative.Count == 1)
                mesazhGjendjeNegative = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", cultinf), String.Join(", ", gjendjeNegative), rm.GetString("regjMagSuffixMesazhNjejesGjendjeNegative", cultinf));
            else if (gjendjeNegative.Count > 1)
                mesazhGjendjeNegative = String.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", cultinf), String.Join(", ", gjendjeNegative), rm.GetString("regjMagSuffixMesazhShumesGjendjeNegative", cultinf));
            if (procesaprovimi.Count == 1)
                mesazhProcesAprovimi = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", cultinf), String.Join(", ", procesaprovimi), " eshte ne proces Aprovimi dhe nuk mund te fshihet!");
            else if (procesaprovimi.Count > 1)
                mesazhProcesAprovimi = String.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", cultinf), String.Join(", ", procesaprovimi), " jane ne proces Aprovimi dhe nuk mund te fshihen!");
            if (teFshire.Count == 1)
                mesazhSukses = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", cultinf), String.Join(", ", teFshire), rm.GetString("suffixMesazhNjejesSuksesi", cultinf));
            else if (teFshire.Count > 1)
                mesazhSukses = String.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", cultinf), String.Join(", ", teFshire), rm.GetString("suffixMesazhShumesSuksesi", cultinf));
            if (tePaFshire.Count == 1)
                mesazhGabim = String.Format("{0}{1}{2}", rm.GetString("msgListaLidhjaDokPrefixNjejes", cultinf), String.Join(", ", tePaFshire), " nuk eshte fshire sepse ka ndodhur nje gabim i papritur!");
            else if (tePaFshire.Count > 1)
                mesazhGabim = String.Format("{0}{1}{2}", rm.GetString("msgListaLidhjaDokPrefixShumes", cultinf), String.Join(", ", tePaFshire), " nuk jane fshire sepse ka ndodhur nje gabim i papritur!");
            if (rivleresim.Count == 1)
                mesazhRivleresim = String.Format("{0}{1}{2}", rm.GetString("msgListaLidhjaDokPrefixNjejes", cultinf), String.Join(", ", rivleresim), " sjell ndryshime në cmimin e daljes. Dëshironi të bëni rivlerësim?");
            else if (rivleresim.Count > 1)
                mesazhRivleresim = String.Format("{0}{1}{2}", rm.GetString("msgListaLidhjaDokPrefixShumes", cultinf), String.Join(", ", rivleresim), " sjellin ndryshime në cmimin e daljes. Dëshironi të bëni rivlerësim?");
            if (meStatusRuajtur.Count == 1)
                mesazhMeStatusRuajtur = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", cultinf), String.Join(", ", meStatusRuajtur), " nuk eshte me status Draft dhe nuk mund te fshihet!");
            else if (meStatusRuajtur.Count > 1)
                mesazhMeStatusRuajtur = String.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", cultinf), String.Join(", ", meStatusRuajtur), " nuk jane me status Draft dhe nuk mund te fshihen!");
            if (meStatusKonvertuar.Count == 1)
                mesazhMeStatusKonvertuar = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", cultinf), String.Join(", ", meStatusKonvertuar), " eshte konvertuar dhe nuk mund te fshihet!");
            else if (meStatusKonvertuar.Count > 1)
                mesazhMeStatusKonvertuar = String.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", cultinf), String.Join(", ", meStatusKonvertuar), " jane konvertuar dhe nuk mund te fshihen!");
            if (meGjenerimTeLidhur.Count == 1)
                mesazhMeGjenerimTeLidhur = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", cultinf), String.Join(", ", meGjenerimTeLidhur), rm.GetString("suffixMesazhGjenerimTeLidhurNjejes", cultinf));
            else if (meGjenerimTeLidhur.Count > 1)
                mesazhMeGjenerimTeLidhur = String.Format("{0}{1}{2}", rm.GetString("regjisDokprefixMesazhShumes", cultinf), String.Join(", ", meGjenerimTeLidhur), rm.GetString("suffixMesazhGjenerimTeLidhurShumes", cultinf));
            if (closedPeriod.Count == 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", cultinf), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodNjejes", cultinf));
            else if (closedPeriod.Count > 1)
                mesazhClosedPeriod = String.Format("{0}{1}{2}", rm.GetString("regjisDokPrefixMesazhNjejes", cultinf), String.Join(", ", closedPeriod), rm.GetString("suffixMesazhClosedPeriodShumes", cultinf));

            mesazhTeDrejta = ktheMesazhPerTeDrejtatNivelRregjistrimi(paTeDrejta, "Fshi", rm, cultinf);

            mesazhGabim += mesazhGabimLidhur + mesazhGabimAutorizim + mesazhGabimPeridheKycur + mesazhGjendjeNegative + mesazhProcesAprovimi + mesazhTeDrejta + mesazhMeStatusRuajtur + mesazhMeStatusKonvertuar + mesazhMeGjenerimTeLidhur + mesazhClosedPeriod + mesazhGabimFiskalizimi;
            return new Tuple<string, string, string>(mesazhGabim, mesazhSukses, mesazhRivleresim);
        }

        public static string eshteLidhur(int idkomp, string kodkonfi, string iddokumenti, int idndermarje, int gjuhe)
        {
            int idkomponente;
            clsDatabaseRegjistrim dbRegjistrim = new clsDatabaseRegjistrim();
            int idniveli = 0;
            if (idkomp != 0)//nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                if (kodkonfi != "")
                {
                    idkomponente = idkomp;
                    string kodkonfigurimi = kodkonfi;
                    clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                    clsKonf.mbushKonfiguriminMeKod(kodkonfigurimi, idndermarje, gjuhe);
                    idniveli = clsKonf.IdNivel;
                }
                else
                {
                    idkomponente = idkomp;
                    clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                    clsKonf.mbushKonfigDefaultKomponentes(idkomponente, idndermarje);
                    idniveli = clsKonf.IdNivel;
                }
            }
            else
            {//nese nuk ehste zgjedhur asnje konfigurim merret konfigurimi default
                idkomponente = idkomp;
                clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                clsKonf.mbushKonfigDefaultKomponentes(idkomponente, idndermarje);
                if (clsKonf != null)
                    clsKonf.mbushKonfigDefaultKomponentes(idkomponente, -1);

                idniveli = clsKonf.IdNivel;
            }
            string lidhur = dbRegjistrim.eshteDokumentiILidhurCelje(iddokumenti, idniveli.ToString()).ToString();
            dbRegjistrim.Dispose();
            return lidhur;
        }


        public static string ktheMesazhPerTeDrejtatNivelRregjistrimi(Dictionary<string, List<string>> paTeDrejta, string veprimi, ResourceManager rm, CultureInfo cultinf)
        {
            string mesazhTeDrejta = String.Empty;
            string drejta = String.Empty;
            string folja = String.Empty;
            switch (veprimi)
            {
                case "Riruaj":
                    drejta = "riruajtje";
                    folja = "riruajtur";
                    break;
                case "Fshi":
                    drejta = "fshirje";
                    folja = "fshire";
                    break;
                default:
                    break;
            }
            if (paTeDrejta.Keys.Contains("nrDoks"))
            {
                if (paTeDrejta["nrDoks"].Count == 1)
                    mesazhTeDrejta = $"{rm.GetString("msgListaLidhjaDokPrefixNjejes", cultinf)}{String.Join(", ", paTeDrejta["nrDoks"])} nuk eshte {folja} sepse nuk keni te drejta {drejta} per nenkategorine {String.Join(", ", paTeDrejta["niveleDoks"])}";
                else if (paTeDrejta["nrDoks"].Count > 1 && paTeDrejta["niveleDoks"].Count == 1)
                    mesazhTeDrejta = $"{rm.GetString("msgListaLidhjaDokPrefixNjejes", cultinf)}{String.Join(", ", paTeDrejta["nrDoks"])} nuk jane {folja} sepse nuk keni te drejta {drejta} per nenkategorine {String.Join(", ", paTeDrejta["niveleDoks"])}";
                else if (paTeDrejta["nrDoks"].Count > 1 && paTeDrejta["niveleDoks"].Count > 1)
                    mesazhTeDrejta = $"{rm.GetString("msgListaLidhjaDokPrefixNjejes", cultinf)}{String.Join(", ", paTeDrejta["nrDoks"])} nuk jane {folja} sepse nuk keni te drejta {drejta} per nenkategorite {String.Join(", ", paTeDrejta["niveleDoks"])}";
            }
            return mesazhTeDrejta;
        }

        public static bool kaTeDrejtePerVepriminMeDokumentin(int idNiveli, string nrDok, clsTeDrejtaRoli teDrejtaInfo, ref colNivelRegjistrimi niveleRregjistrimi, int idKategoria, string kolonaTeDrejta, int idNdermarrje, int idPerdoruesi, int idViti, ref Dictionary<string, List<String>> paTeDrejta, string komponente)
        {
            if (String.IsNullOrEmpty(kolonaTeDrejta) || idKategoria <= 0)
                return true;
            if (!paTeDrejta.Keys.Contains("nrDoks"))
            {
                paTeDrejta.Add("nrDoks", new List<string>());
                paTeDrejta.Add("niveleDoks", new List<string>());
            }

            if (niveleRregjistrimi.Count == 0)
                niveleRregjistrimi = clsNivelRegjistrimi.merrGjitheNivelRegjistrimiSipasKategori(idNdermarrje, idPerdoruesi, idKategoria);

            if (idNiveli > 0)
                teDrejtaInfo.merrTeDrejtaPerKeteKomponenteDheNivelRegjistrimi(idPerdoruesi, idNdermarrje, idViti, komponente, idNiveli);
            else
                teDrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
            if (!(bool)typeof(clsTeDrejtaRoli).GetProperty(kolonaTeDrejta).GetValue(teDrejtaInfo, null))
            {
                paTeDrejta["nrDoks"].Add(nrDok);
                string kodNivelRregjistrimi = niveleRregjistrimi.FirstOrDefault(x => x.IdNivel == idNiveli).Kodi;
                if (!paTeDrejta["niveleDoks"].Contains(kodNivelRregjistrimi))
                    paTeDrejta["niveleDoks"].Add(kodNivelRregjistrimi);
                return false;
            }
            return true;
        }

        public static DateTime merrDateNgaOra(string ore)
        {
            string[] min_sec = ore.Split(':');
            return new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(min_sec[0]), Convert.ToInt32(min_sec[1]), 0);
        }
        public static bool EshteOrarPune(string interval, DateTime dataPerKontroll)
        {
            string[] oraret = interval.Split('-');
            DateTime dateFillimi = merrDateNgaOra(oraret[0]);
            DateTime dateMbarimi = merrDateNgaOra(oraret[1]);
            return dataPerKontroll >= dateFillimi && dataPerKontroll <= dateMbarimi;
        }

        public static IEnumerable<clsKonfigurimAmbjenti> MerrKonfigurimShitje(colKonfigurimAmbjenti colKonfig, string veprimi)
        {
            IEnumerable<clsKonfigurimAmbjenti> rezult;
            if (veprimi == "shitjediscount")
                rezult = colKonfig.Where(x =>
                    x.KodKonfigAmbjente.StartsWith("USHDD") ||
                    x.KodKonfigAmbjente.StartsWith("POROSIDD"));
            else if (veprimi == "bazaar")
                rezult = colKonfig.Where(x =>
                    x.KodKonfigAmbjente.StartsWith("BAZAAR") ||
                    x.KodKonfigAmbjente.StartsWith("POROSIBAZAAR"));
            else
                rezult = colKonfig.Where(x =>
                    !x.KodKonfigAmbjente.StartsWith("BAZAAR") &&
                    !x.KodKonfigAmbjente.StartsWith("POROSIBAZAAR") &&
                    !x.KodKonfigAmbjente.StartsWith("USHDD") &&
                    !x.KodKonfigAmbjente.StartsWith("POROSIDD"));
            return rezult;
        }


        public static void ShtoNeTabeleGabimesh(DataTable err, string kod, string pershkrimi, int nrRreshti)
        {
            object[] arr = { kod, pershkrimi, nrRreshti };
            err.AddRow(arr);
        }
        public static String ktheMenyrePageseSipasID(int idMenyrePagese)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda Kthe menyre pagese sipas id:{idMenyrePagese}");
            String menyrepag;
            switch (idMenyrePagese)
            {
                case 0:
                    menyrepag = "Me mirebesim";
                    break;
                case 4:
                    menyrepag = "Pagese";
                    break;
                case 5:
                    menyrepag = "Pagese Automatike";
                    break;
                case 6:
                    menyrepag = "Cash & Bank";
                    break;
                case 7:
                    menyrepag = "Me parapagim";
                    break;
                case 8:
                    menyrepag = "Arke";
                    break;
                case 9:
                    menyrepag = "Karte Krediti";
                    break;
                case 10:
                    menyrepag = "Pezull";
                    break;
                case 11:
                    menyrepag = "Banke";
                    break;
                default:
                    menyrepag = "";
                    break;
            }
            ImbLogger.LogTraceShitje($"Mbaroi metoda Kthe menyre pagese sipas id:{idMenyrePagese}");
            ImbLogger.LogTraceShitje($"Menyra e pageses pas metodes kthe menyre pagese => {menyrepag}");
            return menyrepag;
        }

        public static int ktheMenyrePageseSipasLlojit(String menyrePagese)
        {
            int idMenyrePagese;
            ImbLogger.LogTraceShitje($"Kthimi i id se menyres se pageses me {menyrePagese}");
            switch (menyrePagese)
            {
                case "Me mirebesim":
                    idMenyrePagese = 0;
                    break;

                case "Pagese":
                    idMenyrePagese = 4;
                    break;

                case "Pagese Automatike":
                    idMenyrePagese = 5;
                    break;

                case "Cash & Bank":
                    idMenyrePagese = 6;
                    break;

                case "Me parapagim":
                    idMenyrePagese = 7;
                    break;

                case "Arke":
                    idMenyrePagese = 8;
                    break;

                case "Karte krediti":
                    idMenyrePagese = 9;
                    break;

                case "Pezull":
                    idMenyrePagese = 10;
                    break;
                case "Banke":
                    idMenyrePagese = 11;
                    break;
                case "":
                    idMenyrePagese = 0;
                    break;
                default:
                    idMenyrePagese = -1;
                    break;
            }
            ImbLogger.LogTraceShitje($"Id e menyres se pageses eshte:{idMenyrePagese}");
            return idMenyrePagese;
        }

        public static int ktheIdLlojMarreveshjeSipasLlojit(String llojMarreveshje)
        {
            int idllojMarreveshje;
            ImbLogger.LogTraceShitje($"Kthimi i id se llojit te marreveshjes {llojMarreveshje}");
            switch (llojMarreveshje.Replace('_', ' '))
            {
                case "":
                    idllojMarreveshje = 0;
                    break;
                case "Retention":
                    idllojMarreveshje = 1;
                    break;

                case "Acquisition":
                    idllojMarreveshje = 2;
                    break;

                case "EBU Benefit":
                    idllojMarreveshje = 3;
                    break;

                case "Agreement Tenure Reward":
                    idllojMarreveshje = 4;
                    break;
                default:
                    idllojMarreveshje = -1;
                    break;
            }
            ImbLogger.LogTraceShitje($"Id e llojit te pageses eshte:{llojMarreveshje}");
            return idllojMarreveshje;
        }

        public static double ktheVlereKomision(double perqindje, double vleftaPaTvsh)
        {
            if (perqindje == 0 || vleftaPaTvsh == 0)
                return 0;
            else
                return vleftaPaTvsh * (perqindje / 100);
        }

        public static (clsMesazh, clsMesazh) dergoMesazhKlientitFaturat(int idGjuha, int idPerdoruesi, int idViti, int idNdermarrje, CultureInfo cultinf, int idShitjeKoka, String nrDok, DateTime dtDok, int idKlientFurnitor, int idKonfig, int[] mags, bool dergoemailMag)
        {
            int idDesign = DbCore.DbRegjistrim.clsKokaShitje.ktheIdRaportDesign(idShitjeKoka);
            return EmailComposer.dergoEmailFaturenNgaPerdoruesiLoguar(idGjuha, cultinf, idPerdoruesi, idViti, idNdermarrje, idShitjeKoka, idKlientFurnitor, nrDok, dtDok, idDesign, idKonfig, mags, dergoemailMag);
        }

        public static void KontrolloPerSerialeTePerdoruraNeDokDraft(HttpSessionState Session, HtmlIframe Container1)
        {
            var serialeTePerdoruraNeDokDraft = (DataTable)GlobalCacheManager.MyPageCache["serialeTePerdoruraNeDokDraft"];
            if (serialeTePerdoruraNeDokDraft != null)
            {
                var err = new DataTable();
                err.Columns.Add("Nr Dok Date Dok");
                err.Columns.Add("Gabimi");
                err.Columns.Add("Lloj Dokumenti");
                foreach (DataRow row in serialeTePerdoruraNeDokDraft.Rows)
                {
                    object[] arr = { DateTime.ParseExact(row[2].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture).ToShortDateString() + " | " + row[3], $"Seriali {row[1]} i artikullit {row[5]} eshte perdorur ne dokument draft!", row[4] };
                    err.Rows.Add(arr);
                }
                mySessionObjects.ruajTabeleGabimeshImporti(Session, err);
                Container1.Attributes["src"] = "RaportiShpejte.aspx?Sesioni=false&emriReal=gabimeImporti&printo=false&db=jo&vjen=serialeTePerdoruraNeDokDraft";
                GlobalCacheManager.MyPageCache.Remove("serialeTePerdoruraNeDokDraft");
            }
        }


        public static decimal ktheMarzhGabimiSipasKonfigurimit(int idKonfigAmbjente, int idNderm)
        {
            clsFormatiKonfig formatNrPerKonfig = new clsFormatiKonfig();
            formatNrPerKonfig.mbushFormatNrKonfigSipasIdKonfigAmbjente(idKonfigAmbjente);
            int idMonedha = clsMonedha.ktheIdMonedhenENdermarrjes(idNderm);
            clsFormatKonfigTrup formatMonedhe = new clsFormatKonfigTrup();

            if (formatNrPerKonfig.IdFormatKonfig > 0)
                formatMonedhe = formatNrPerKonfig.KonfigTrupi.merrFormatSipasMonedhes(idMonedha);
            else
                formatMonedhe = new clsFormatKonfigTrup(clsFormatKonfigTrup.defaultFormatSasia, clsFormatKonfigTrup.defaultFormatCmimi, clsFormatKonfigTrup.defaultFormatVlefta, clsFormatKonfigTrup.defaultFormatZbritja, clsFormatKonfigTrup.defaultFormatStringSasia, clsFormatKonfigTrup.defaultFormatStringCmimi, clsFormatKonfigTrup.defaultFormatStringVlefta, clsFormatKonfigTrup.defaultFormatStringZbritja);

            return decimal.Parse((clsFunksione.krijoNumer(formatMonedhe.ShifraPasPresjesVlefta, "0") + "1"), System.Globalization.CultureInfo.InvariantCulture);

        }

        public static DataTable MerrIdMagsTrupiPerDok(int idKokaShitje)
        {
            using (clsDatabaseRegjistrim db = new clsDatabaseRegjistrim())
            {
                return db.MerrIdMagsTrupiPerDokShitje(idKokaShitje);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id e magazines</param>
        /// <param name="idja">Id e artikullit si string</param>
        /// <param name="datedok">data e veprimit</param>
        /// <returns></returns>
        public static object KtheVleraMagazineDheArtikulli(clsNjesiAdministrative magazina, string idja, DateTime datedok)
        {
            var artikulli = new clsArtikulli();
            if (!String.IsNullOrEmpty(idja))
                artikulli = new clsArtikulli(Int32.Parse(idja));

            if (artikulli.IdArtikulli <= 0)
            {
                return new
                {
                    magazina = magazina,
                    gjendjeMag = 0
                };
            }
            datedok = datedok.ToLocalTime();
            double gjendjeTotaleMag = clsTrupiMagazina.merrSasi(artikulli, magazina.IdNjesiAdministrative, datedok, 0);
            return new
            {
                magazina = magazina,
                gjendjeMag = gjendjeTotaleMag
            };
        }

        /// <summary>
        /// Kontrollon nese kane ndryshuar cmime ose zbritje analitike dhe nuk jane selektuar per tu ruajtur
        /// </summary>
        /// <param name="teNdryshuarPorTePaSelektuarKode">array me kode artikujsh qe jane ndryshuar po nuk jane selektuar</param>
        /// <returns></returns>
        public static clsMesazh KontrolloTeNdryshuar(string[] teNdryshuarPorTePaSelektuarKode)
        {
            clsMesazh mesazhi = new clsMesazh();
            if (teNdryshuarPorTePaSelektuarKode.Length != 0)
            {
                mesazhi.PershkrimMesazhi = MessagesResource.Messages["msgCmimeArtikulliArtikujTeNdryshuarPorTePaSelektuar"];
                for (var i = 0; i < teNdryshuarPorTePaSelektuarKode.Length; i++)
                {
                    if (i > 4)
                    {
                        mesazhi.PershkrimMesazhi += "....";
                        break;
                    }
                    mesazhi.PershkrimMesazhi += " " + teNdryshuarPorTePaSelektuarKode[i] + ",";
                }
                mesazhi.PershkrimMesazhi = mesazhi.PershkrimMesazhi.Substring(0, mesazhi.PershkrimMesazhi.Length - 1) + MessagesResource.Messages["msgCmimeArtikulliDoniTeVazhdoni"];
                return mesazhi;
            }
            return new clsMesazh(true, "Kontrolli kaloi me sukses");
        }
        /// <summary>
        /// Perdor MessagesResource.KtheCultureInfo. Lene ketu sepse perdoret nga disajnet e raporteve te ruajtur neper kliente
        /// </summary>
        /// <param name="idGjuha"></param>
        /// <returns></returns>
        [Obsolete]
        public static CultureInfo ktheCultureInfo(int idGjuha)
        {
            return MessagesResource.KtheCultureInfo(idGjuha);
        }

        public static object ktheKonfigDB(int idKomp, string kodKonf, int idNdermarrje, string kodKontrollKlienti, int idKlienti, bool merrFormatKursi, int idGjuha, int idPerdoruesi, string llojVeprimi, DateTime? dateDok, DateTime? dateDokDefault)
        {
            if (dateDok.HasValue)
                dateDok = dateDok.Value.ToLocalTime();
            if (dateDokDefault.HasValue)
                dateDokDefault = dateDokDefault.Value.ToLocalTime();
            int idKonfigurim = -1;
            string kodniveli = "";
            int kategoria;
            bool shtim = (llojVeprimi == "shtim" ? true : false);
            if (kodKonf != "")  //nese eshte zgjedhur nje konfigurim tek combo e konfigurimeve
            {
                clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                clsKonf.mbushKonfiguriminMeKod(kodKonf, idNdermarrje, idGjuha);
                idKonfigurim = clsKonf.IdKonfigAmbjente;
                kodniveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(clsKonf.IdNivel);
                kategoria = clsKonf.IdKategori;
            }
            else
            {//nese nuk ehste zgjedhur asnje konfigurim merret konfigurimi default
                clsKonfigurimAmbjenti clsKonf = new clsKonfigurimAmbjenti();
                clsKonf.mbushKonfigDefaultKomponentes(idKomp, idNdermarrje);
                if (clsKonf != null)
                    clsKonf.mbushKonfigDefaultKomponentes(idKomp, -1);
                idKonfigurim = clsKonf.IdKonfigAmbjente;
                kodniveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(clsKonf.IdNivel);
                kategoria = clsKonf.IdKategori;
            }
            //DbCore.DbShare.colKusht colKushte = new DbCore.DbShare.colKusht();
            DataTable kushtAlternativa = colKusht.mbushGjitheKushteAlternativa(idKonfigurim);
            clsKonfLlojRreshti konfLlojRreshti = clsKonfLlojRreshti.getKonfLlojRreshti(kushtAlternativa, kategoria, "LLD");

            colAtributeTrupi colAtrTrupi = new colAtributeTrupi();
            colAtrTrupi.mbushKontrolletKonfigurimitKomponentes(idGjuha, idKomp, idKonfigurim);
            //DbCore.DbShare.colKontrolle colKontroll1 = colAtrTrupi.ktheKontrolle();
            colKontrolle colKontroll = new colKontrolle(idGjuha, idKomp, idKonfigurim);
            //colKontroll.TableName = "colKontroll";
            colGridaTrupi colGrida = new colGridaTrupi(idKomp, idKonfigurim, idGjuha);

            clsFormatKonfigTrup formatNumri = new clsFormatKonfigTrup();
            string formatiKursit;
            object[] form = ktheKonfigurimFormatNumri(idKonfigurim, idNdermarrje, kodKontrollKlienti, idKlienti, shtim, idKomp, merrFormatKursi, idGjuha);
            formatNumri = (clsFormatKonfigTrup)form[0];
            formatiKursit = form[1].ToString();
            colGrupimDokumentiKoka[] grupimDok = null;
            if (llojVeprimi != "modifikim" && llojVeprimi != "klonim" && llojVeprimi != "kthim" && llojVeprimi != "konvertim" && llojVeprimi != "konvertimblerje" && llojVeprimi != "rezervim")
                grupimDok = colGrupimDokumentiKoka.ktheGrupimDokumentashNderm(kodKonf, idNdermarrje, idPerdoruesi);


            shtim = (llojVeprimi == "shtim" || llojVeprimi == "shtimraport");
            Dictionary<string, object> objekteDefault = clsFunksione.vendosObjektetDefault(colAtrTrupi, colKontroll, shtim, llojVeprimi, dateDok, idKonfigurim, idNdermarrje, idPerdoruesi, idKomp, idGjuha, dateDokDefault);

            var kasePerdorues = new clsPerdorues(idPerdoruesi).IdKonfigKasa;

            return new { colKontroll = colKontroll, colAtrTrupi = colAtrTrupi, colGrida = colGrida, colKushte = kushtAlternativa, kodniveli = kodniveli, konfLlojRreshti = konfLlojRreshti, formatNumri = formatNumri, formatiKursit = formatiKursit, grupimeDokumentesh = grupimDok, objekteDefault = objekteDefault, KasePerdoruesi = kasePerdorues };
        }

        public static DateTime ktheDateOreDefault(clsPeriudhaKontabel periudha)
        {
            DateTime dateServeri = DateTime.Now;
            if (dateServeri >= periudha.FillimiPeriudha && dateServeri <= periudha.MbarimiPeriudha)
                return dateServeri;
            else
                return periudha.FillimiPeriudha;
        }

        public static double merrKursiSipasMonedhesDatesDheLlojit(int idMonedha, DateTime date, int lloji)
        {
            if (idMonedha == 0)
                return 1;
            date = date.ToLocalTime();
            double kursi = clsKurset.merrKursinFunditPerMonedheDateDheLloj(idMonedha, date, lloji);
            if (kursi == 0)
                return 1;
            return kursi;
        }
        public static string kthePershkrimArtikullPerKasen(int metoda, bool merrkodArtikulli, string kodartikull, bool merrPershkrim2Artikulli, string pershkrimArtikull, string pershkrim2Artikull, int length)
        {
            string pershkrimi = "";
            switch (metoda)
            {
                case 1:
                    pershkrimi = merrkodArtikulli ? kodartikull : merrPershkrim2Artikulli ? pershkrim2Artikull : pershkrimArtikull;
                    break;
                case 2:
                    if (merrkodArtikulli)
                        pershkrimi = merrPershkrim2Artikulli ? pershkrim2Artikull + "/" + kodartikull : pershkrimArtikull + "/" + kodartikull;
                    else
                        pershkrimi = merrPershkrim2Artikulli ? pershkrim2Artikull : pershkrimArtikull;
                    break;
            }
            return (length > 0 ? pershkrimi.Substring(0, (pershkrimi.Length < length ? pershkrimi.Length : length)) : pershkrimi);
        }

        public static bool kontrolloDokNeRuajtje(String sessionId, String scopeid, params string[] values)
        {
            List<string> infoDokRuajtur = GlobalCacheManager.MyAppCache.Get<List<String>>("infoDokRuajtur" + sessionId);
            //Ketu behet kontroll nese ka ndonje kerkese per te ruajtur dok me te njejtat te dhena brenda nje intervali te percaktuar tek T_SERVER_CONFIGURATION
            var valuesNewDoc = String.Join("_", values);
            var savingTimeoutConfig = double.Parse(clsServerConfiguration.LexoKonfigurimSipasKey<string>(ServerKonfigKey.DOC_SAVING_TIMEOUT_IN_MIN));
            if (infoDokRuajtur != null && infoDokRuajtur.Count > 0)
            {
                foreach (string dok in infoDokRuajtur)
                {
                    var infoDok = dok.Split('_');
                    DateTime dateVeprimi = Convert.ToDateTime(infoDok[infoDok.Length - 1]);
                    double diferencaKohore = (double)(DateTime.Now - (DateTime)dateVeprimi).TotalMinutes;
                    if (diferencaKohore < savingTimeoutConfig && dok == sessionId + "_" + scopeid + "_" + valuesNewDoc + "_" + dateVeprimi)
                    {
                        ImbLogger.Error(String.Format("Ekziston nje kerkese per te ruajtur dok me sessionid {0}, info {1} ne oren {2}", sessionId, valuesNewDoc, dateVeprimi));
                        return false;
                    }
                }
            }
            if (infoDokRuajtur == null)
                infoDokRuajtur = new List<String>();
            infoDokRuajtur.Add(sessionId + "_" + scopeid + "_" + valuesNewDoc + "_" + DateTime.Now);
            GlobalCacheManager.MyAppCache.Set("infoDokRuajtur" + sessionId, infoDokRuajtur, TimeSpan.FromDays(1));
            return true;
        }

        public static void removeLastDocFromCache(String sessionId)
        {
            List<string> infoDokRuajtur = GlobalCacheManager.MyAppCache.Get<List<String>>("infoDokRuajtur" + sessionId);
            if (infoDokRuajtur == null)
                return;
            infoDokRuajtur.RemoveAt(infoDokRuajtur.Count - 1);
            GlobalCacheManager.MyAppCache.Set("infoDokRuajtur" + sessionId, infoDokRuajtur, TimeSpan.FromDays(1));
        }

        public static HttpWebRequest CreateJSONWebRequest(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            return httpWebRequest;
        }
        public static HttpWebRequest getInstanceNameAndDatabase(string url, string cllientDbName)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "?name=" + cllientDbName);
            httpWebRequest.Method = "GET";
            return httpWebRequest;
        }
        public static HttpWebRequest CreateGetWebRequest(string url, string cllientDbName)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "?name=" + cllientDbName);
            httpWebRequest.Method = "GET";
            return httpWebRequest;
        }
        public static HttpWebRequest CreateGetWebRequestLicence(string url, string cllientDbName)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("client", cllientDbName);
            return httpWebRequest;
        }
        public static HttpWebRequest CreateGetWebRequestExpire(string url, string cllientDbName)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "?clientdb=" + cllientDbName);
            httpWebRequest.Method = "GET";
            return httpWebRequest;
        }
        public static HttpWebRequest GetClientDatabase(string url)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "GET";
            return httpWebRequest;
        }
        public static object getClientDatabaseBackup(string prefix, string[] generations)
        {
            try
            {
                string linkConnectionString = WebConfigurationManager.AppSettings["connectionStringUrl"] + "-test-1";
                string connectionStringame = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar();
                Int32 unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                string project = "alphaweb";
                string instance = "";
                string instanceIp = "";
                var instanca = getInstanceAndDatabaseRequest();

                //Authentication with google service account
                var serviceAccount = System.Web.Hosting.HostingEnvironment.MapPath("~/service_account/service_account_backup.json");
                string serviceAccountJson = File.ReadAllText(serviceAccount);
                var credentialsServiceAccount = JsonConvert.DeserializeObject<object>(serviceAccountJson);
                GoogleCredential credential = Task.Run(() => GoogleCredential.FromJson(serviceAccountJson)).Result;
                string[] credentials = new string[1];
                credentials[0] = "https://www.googleapis.com/auth/cloud-platform";
                if (credential.IsCreateScopedRequired)
                {
                    credential = credential.CreateScoped(credentials);
                }
                SQLAdminService sqlAdminService = new SQLAdminService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Google-SQLAdminSample/0.1",
                });


                var service = new IamService(new IamService.Initializer
                {
                    HttpClientInitializer = credential
                });
                //End of Authentication

                //Copy db
                var storage = StorageClient.Create(credential);
                var copyOptions = new CopyObjectOptions
                {
                    SourceGeneration = long.Parse(prefix.Split('#')[1])
                };
                storage.CopyObject("backup-cloudsqldatabase", prefix.Split('#')[0] + ".gz", "backup-cloudsqldatabase", "databaseToImport/" + unixTimestamp + connectionStringame + ".gz", copyOptions);
                //End of copy


                //Get all instances
                InstancesResource.ListRequest instancesList = sqlAdminService.Instances.List(project);
                Data.InstancesListResponse responseInstanceList;
                do
                {
                    responseInstanceList = instancesList.Execute();
                    if (responseInstanceList.Items == null) continue;
                    foreach (Data.DatabaseInstance databaseInstance in responseInstanceList.Items)
                    {
                        bool sameDatabase = false;
                        bool status = false;
                        string instanceName = databaseInstance.Name;
                        if (instanceName == "quota-manager-database" || instanceName == "alpha-conn-strings" || instanceName == "instance-webedition1" || instanceName == "instance-testime" || instanceName == instanca.Split('/')[0]) continue;
                        string instanceIpConfig = "";
                        if (databaseInstance.IpAddresses.FirstOrDefault().Type == "PRIMARY") instanceIpConfig = databaseInstance.IpAddresses[1].IpAddress;
                        else instanceIpConfig = databaseInstance.IpAddresses.FirstOrDefault().IpAddress;
                        DatabasesResource.ListRequest databases = sqlAdminService.Databases.List(project, instanceName);
                        Data.DatabasesListResponse databasesResponse = databases.Execute();
                        if (databasesResponse.Items.Count >= 30) continue;
                        foreach (Data.Database db in databasesResponse.Items)
                        {
                            if (db.Name == connectionStringame) sameDatabase = true;
                        }
                        if (sameDatabase) continue;
                        if (databasesResponse.Items.Count < 30)
                        {
                            instance = instanceName;
                            instanceIp = instanceIpConfig;
                            break;
                        }
                    }
                    instancesList.PageToken = responseInstanceList.NextPageToken;
                } while (responseInstanceList.NextPageToken != null);

                //Backup instance
                IList<string> databasesListToExport = new List<string>();
                databasesListToExport.Add(connectionStringame);
                Data.InstancesExportRequest requestExport = new Data.InstancesExportRequest();
                requestExport.ExportContext = new Data.ExportContext();
                requestExport.ExportContext.Kind = "sql#exportContext";
                requestExport.ExportContext.Databases = databasesListToExport;
                requestExport.ExportContext.FileType = "BAK";
                string instanceNameToExport = instanca.Split('/')[0];
                requestExport.ExportContext.Uri = "gs://backup-cloudsqldatabase/" + instanceNameToExport + "/" + connectionStringame + ".gz";
                InstancesResource.ExportRequest responseExport = sqlAdminService.Instances.Export(requestExport, project, instanceNameToExport);
                Data.Operation responseOperation = responseExport.Execute();
                OperationsResource.GetRequest operationStatusExport = sqlAdminService.Operations.Get(project, responseOperation.Name);
                bool operationStatusEx = false;
                while (!operationStatusEx)
                {
                    Thread.Sleep(3000);
                    Data.Operation operationResult = operationStatusExport.Execute();
                    if (operationResult.Status == "DONE") operationStatusEx = true;
                }
                //End of backup
                //Migrate all backups
                //Migrate without generation
                //WebRequest webRequestMigration;
                //webRequestMigration = CreateJSONWebRequest("https://europe-west1-alphaweb.cloudfunctions.net/copyAllGenerationsOfBucketAlphaweb");
                //using (Stream stream = webRequestMigration.GetRequestStream())
                //{
                //    using (StreamWriter stmw = new StreamWriter(stream))
                //    {
                //        stmw.Write(JsonConvert.SerializeObject(new
                //        {
                //            fileName = prefix.Split('#')[0],
                //            fileDestination = instance + "/" + connectionStringame + ".gz",
                //            generations = generations
                //        }));
                //    }
                //}
                //Task<WebResponse> webResponseMigration = webRequestMigration.GetResponseAsync();
                //End of migration

                Data.InstancesImportRequest requestBody = new Data.InstancesImportRequest();
                requestBody.ImportContext = new Data.ImportContext();
                requestBody.ImportContext.Uri = "gs://backup-cloudsqldatabase/databaseToImport/" + unixTimestamp + connectionStringame + ".gz";
                requestBody.ImportContext.FileType = "BAK";
                requestBody.ImportContext.Database = connectionStringame;
                InstancesResource.ImportRequest request = sqlAdminService.Instances.Import(requestBody, project, instance);
                Data.Operation response = request.Execute();
                OperationsResource.GetRequest operation = sqlAdminService.Operations.Get(project, response.Name);

                bool operationStatus = false;
                while (!operationStatus)
                {
                    Thread.Sleep(3000);
                    Data.Operation operationResult = operation.Execute();
                    if (operationResult.Status == "DONE")
                    {
                        object connectionStringObject = new
                        {
                            name = connectionStringame,
                            connectionString = $"Data Source={instanceIp};Persist Security Info=True;Initial Catalog={connectionStringame};user Id=sqlserver;password=Alpha.2019;Min pool size=0;Max pool size=1000000", // change praktike1 to instance
                            LOCATION = instance,
                            originalDatabase = connectionStringame,
                            originalInstance = instanca.Split('/')[0]
                        };
                        operationStatus = true;
                        WebRequest webRequest;
                        webRequest = CreateJSONWebRequest(linkConnectionString);
                        using (Stream stream = webRequest.GetRequestStream())
                        {
                            using (StreamWriter stmw = new StreamWriter(stream))
                            {
                                stmw.Write(JsonConvert.SerializeObject(connectionStringObject));
                            }
                        }
                        using (WebResponse webResponse = webRequest.GetResponse())
                        {
                            return new
                            {
                                status = "SUCCESS"
                            };

                        }
                    }
                }
                return new
                {
                    status = "SUCCESS"
                };


            }
            catch (WebException ex)
            {
                return new
                {
                    status = "FAILED"
                };
            }
        }
        public static bool krijoBackup()
        {
            try
            {
                string project = "alphaweb";
                //Authentication with google service account
                var serviceAccount = System.Web.Hosting.HostingEnvironment.MapPath("~/service_account/service_account_backup.json");
                string serviceAccountJson = File.ReadAllText(serviceAccount);
                var credentialsServiceAccount = JsonConvert.DeserializeObject<object>(serviceAccountJson);
                GoogleCredential credential = Task.Run(() => GoogleCredential.FromJson(serviceAccountJson)).Result;
                string[] credentials = new string[1];
                credentials[0] = "https://www.googleapis.com/auth/cloud-platform";
                if (credential.IsCreateScopedRequired)
                {
                    credential = credential.CreateScoped(credentials);
                }
                SQLAdminService sqlAdminService = new SQLAdminService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = credential,
                    ApplicationName = "Google-SQLAdminSample/0.1",
                });


                var service = new IamService(new IamService.Initializer
                {
                    HttpClientInitializer = credential
                });
                //End of Authentication
                string connectionStringame = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar();
                IList<string> databasesListToExport = new List<string>();
                var instanca = getInstanceAndDatabaseRequest();
                databasesListToExport.Add(connectionStringame);
                Data.InstancesExportRequest requestExport = new Data.InstancesExportRequest();
                requestExport.ExportContext = new Data.ExportContext();
                requestExport.ExportContext.Kind = "sql#exportContext";
                requestExport.ExportContext.Databases = databasesListToExport;
                requestExport.ExportContext.FileType = "BAK";
                string instanceNameToExport = instanca.Split('/')[0];
                requestExport.ExportContext.Uri = "gs://backup-cloudsqldatabase/" + instanceNameToExport + "/" + connectionStringame + ".gz";
                InstancesResource.ExportRequest responseExport = sqlAdminService.Instances.Export(requestExport, project, instanceNameToExport);
                Data.Operation responseOperation = responseExport.Execute();
                OperationsResource.GetRequest operationStatusExport = sqlAdminService.Operations.Get(project, responseOperation.Name);
                bool operationStatusEx = false;
                while (!operationStatusEx)
                {
                    Thread.Sleep(500);
                    Data.Operation operationResult = operationStatusExport.Execute();
                    if (operationResult.Status == "DONE")
                        operationStatusEx = true;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public static List<string> getClientDatabaseBackups(string prefix)
        {
            string linkDatasetEndpoint = WebConfigurationManager.AppSettings["backupUrl"];
            List<string> dbList = new List<string>();
            try
            {
                WebRequest webRequest;
                webRequest = CreateGetWebRequest(linkDatasetEndpoint, prefix);

                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        string ServiceResult = rd.ReadToEnd();
                        object dbObject = JsonConvert.DeserializeObject(ServiceResult);
                        IList dbListCollection = (IList)dbObject;
                        foreach (var dbUrl in dbListCollection)
                        {
                            dbList.Add(dbUrl.ToString());
                        }
                        //json.GetType().GetProperty("allUrl").GetValue(json,null)
                    }
                    return dbList;

                }
            }
            catch (WebException ex)
            {
                var dbBoshe = new List<string>();
                return dbBoshe;
            }
        }
        public static bool sendExpireLicenceRequest(string url, string clientDbName)
        {
            List<string> dbList = new List<string>();
            try
            {
                WebRequest webRequest;
                webRequest = CreateGetWebRequestExpire(url, clientDbName);

                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        string ServiceResult = rd.ReadToEnd();
                        return true;
                        //json.GetType().GetProperty("allUrl").GetValue(json,null)
                    }
                }
            }
            catch (WebException ex)
            {
                return false;
            }
        }
        public static string getInstanceAndDatabaseRequest()
        {
            string linkDatasetEndpoint = WebConfigurationManager.AppSettings["instanceUrl"] + "_alphaweb";
            string dbName = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar();
            string ServiceResult = "";
            try
            {
                WebRequest webRequest;
                webRequest = getInstanceNameAndDatabase(linkDatasetEndpoint, dbName);
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    {
                        ServiceResult = rd.ReadToEnd();
                        //json.GetType().GetProperty("allUrl").GetValue(json,null)
                    }
                    return ServiceResult;

                }
            }
            catch (WebException ex)
            {
                return ServiceResult;
            }
        }
        public static bool dergoWebhookDatasetEndpoint(object objekti)
        {
            string result = string.Empty;
            string linkDatasetEndpoint = WebConfigurationManager.AppSettings["urlDatasetEndpoint"];
            try
            {
                WebRequest webRequest;
                webRequest = CreateJSONWebRequest(linkDatasetEndpoint);

                using (Stream stream = webRequest.GetRequestStream())
                {
                    using (StreamWriter stmw = new StreamWriter(stream))
                    {
                        stmw.Write(JsonConvert.SerializeObject(objekti));
                    }
                }
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    //using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    //{

                    //    var ServiceResult = rd.ReadToEnd();
                    //}
                    return true;

                }
            }
            catch (WebException ex)
            {
                return false;
            }

        }
        public static async Task<IAsyncResult> dergoLogAlphaweb(string ndermarrja, string tipVeprimi, string ambjenti, string organizata,string user)
        {
            object obj = new
            {
                Organizata = organizata,
                Ndermarrja = ndermarrja,
                TipVeprimi = tipVeprimi,
                Ambjenti = ambjenti,
                Perdoruesi = user
            };
            string result = string.Empty;
            string linkDatasetEndpoint = WebConfigurationManager.AppSettings["urlLogAlphaweb"];
            try
            {
                WebRequest webRequest;
                webRequest = CreateJSONWebRequest(linkDatasetEndpoint);

                using (Stream stream = webRequest.GetRequestStream())
                {
                    using (StreamWriter stmw = new StreamWriter(stream))
                    {
                        stmw.Write(JsonConvert.SerializeObject(obj));
                    }
                }
                return webRequest.BeginGetResponse(null, null);
                //using (WebResponse webResponse = webRequest.GetResponse())
                //{
                //using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                //{

                //    var ServiceResult = rd.ReadToEnd();
                //}

                //}
            }
            catch (WebException ex)
            {
                return null;
            }

        }
        public static bool expireLicenceRequest(object objekti, string url)
        {
            string linkDatasetEndpoint = url;
            try
            {
                WebRequest webRequest;
                webRequest = CreateJSONWebRequest(linkDatasetEndpoint);

                using (Stream stream = webRequest.GetRequestStream())
                {
                    using (StreamWriter stmw = new StreamWriter(stream))
                    {
                        stmw.Write(JsonConvert.SerializeObject(objekti));
                    }
                }
                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    //using (StreamReader rd = new StreamReader(webResponse.GetResponseStream()))
                    //{

                    //    var ServiceResult = rd.ReadToEnd();
                    //}
                    return true;

                }
            }
            catch (WebException ex)
            {
                return false;
            }

        }
        public static byte[] encrypt(byte[] data, RSAParameters RSAKey, bool Do0AEPadding)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    encryptedData = RSA.Encrypt(data, Do0AEPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException ex)
            {
                return null;
            }
        }
        static public byte[] Decryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
        static public DataTable getAllRolesExxeptSuperUser(){
            return clsRoli.ktheRolePervecSuperUser();
        }
        public static string generateRandomPassword()
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_+=-";
            StringBuilder stringBuilder = new StringBuilder();
            Random random = new Random();
            for (var i = 0; i <= 10; i++)
            {
                stringBuilder.Append(valid[random.Next(valid.Length)]);
            }
            return stringBuilder.ToString();
        }
        public async static Task createLoginWithGmail(string uid, int idNdermarje, int idPerdoruesi, string email,HttpSessionState session)
        {
            clsPerdorues perdoruesi = new clsPerdorues();
            FirebaseConfiguration firebaseConfiguration = new FirebaseConfiguration();
            bool exists = await firebaseConfiguration.checkIfUserExists(uid);
            string username = email.Split('@')[0];
            clsPerdorues user = new clsPerdorues();
            bool ekziston = user.ktheNeseUseriEkzistonGmail(username, true, email);
            string shenim = new clsPerdorues(idPerdoruesi).Shenime;
            var uDetailsFromNotes = await firebaseConfiguration.returnUserDetailsFromNotes(shenim);
            if(uDetailsFromNotes.Count > 0)
                if(uDetailsFromNotes["email"].ToString() != email)
                    return;
            if (ekziston)
                return;
            string kodNdermarrja = new clsNdermarrje(idNdermarje).NdermarrjeKodi;
            string pass = PasswordHelper.HashLogin(username, clsFunksione.generateRandomPassword());
            string alphaOrganization = clsKontrollePerFiskalizimin.ktheInitialCatalogTeLoguar();
            if (!exists) {
                string orgId = await firebaseConfiguration.createNewOrganization(firebaseConfiguration.createOrganizationDetailsObject(alphaOrganization, kodNdermarrja), uid);
                await firebaseConfiguration.createNewUser(firebaseConfiguration.createUserDetailsObject(uid, username, pass, email, alphaOrganization, orgId), uid);
                clsPerdorues.krijoPerdoruesMeGmail(email, username, username, pass, idPerdoruesi);
            }
            else
            {
                bool status = await firebaseConfiguration.updateUserDetails(firebaseConfiguration.createUserDetailsObjectForUpdate(alphaOrganization, pass, username), uid, kodNdermarrja);
                if (status)
                    clsPerdorues.krijoPerdoruesMeGmail(email, username, username, pass, idPerdoruesi);


            }
        }
        public async static Task<bool> merrShenimePerdoruesi(string shenime)
        {
            try
            {
                FirebaseConfiguration firebaseConfiguration = new FirebaseConfiguration();
                return await firebaseConfiguration.checkIfUserExistsWithAlpha(shenime);
            }
            catch(Exception e)
            {
                return false;
            }

        }
        //MOS SHTONI funksione qe prekin databazen ketu
    }
}