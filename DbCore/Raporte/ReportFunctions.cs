using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Web;
using DbCore.DbAdmin;
using DbCore.DbKontabiliteti;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.Messages;
using DevExpress.XtraReports.UI;
using Newtonsoft.Json;
using static System.Convert;
using Parameter = DevExpress.XtraReports.Parameters.Parameter;

namespace DbCore.Raporte
{
    //TODO Mihal te ndryshohet menyra e krijimit te ojbektit XtraReport
    public static class ReportFunctions
    {
        /// <summary>
        /// Kthen raportin sipas id qe i kalohet si parameter.
        /// </summary>
        /// <param name="vjen"></param>
        /// <param name="idGjuha"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idViti"></param>
        /// <param name="idRaporti"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="sqlParamShfaqReport"></param>
        /// <param name="idDesign"></param>
        /// <param name="orientimi"></param>
        /// <param name="guidString"></param>
        /// <param name="scopeId"></param>
        /// <param name="vjenNga"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public static XtraReport krijoObjektRaporti(string vjen, int idGjuha, int idPerdoruesi, int idViti, int idRaporti, int idNdermarrje, colParameter sqlParamShfaqReport, int idDesign, string orientimi, String guidString, string scopeId, string vjenNga = "", bool alphaMobile = false)
        {
            var report = new XtraReport();
            var raporti = new clsRaporti(idGjuha, idRaporti);
            clsNdermarrje Ndermarrje = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
            clsViti Viti = new clsViti(idViti);
            clsPerdorues user = new clsPerdorues(idPerdoruesi);

            if (sqlParamShfaqReport != null)
                KrijoParametraRaporti(report, sqlParamShfaqReport, raporti.RaportiEmriReal);

            var parametraPerKonstruktor = new AlphaWebReports.Common.ParametraRaporti
            {
                IdRaporti = idRaporti,
                IdSubRaporti = clsRaporti.KtheIdSubRaporti(idRaporti),
                Vjen = vjen,
                IdGjuha = idGjuha,
                Ci = MessagesResource.Messages.CurrentCultureInfo,
                GuidString = HttpUtility.UrlEncode(guidString),
                IdNdermarrje = idNdermarrje,
                IdPerdoruesi = idPerdoruesi,
                IdViti = idViti,
                ScopeID = HttpUtility.UrlEncode(scopeId),
                NdermarrjePershkrimi = Ndermarrje.NdermarrjePershkrimi,
                NdermarrjeKodi = Ndermarrje.NdermarrjeKodi,
                NdermarrjeNipt = Ndermarrje.NdermarrjeNipt,
                NdermarrjeVendi = Ndermarrje.NdermarrjeVendi,
                KodiViti = Viti.KodiViti,
                EmriPerdorues = user.EmriPerdorues,
                MbiemriPerdorues = user.MbiemriPerdorues,
                NdermarrjeQytetiPershkrimi = Ndermarrje.NdermarrjeQytetiPershkrimi,
                NdermarrjeTel = Ndermarrje.NdermarrjeTel,
                NdermarrjeKodiFiskal = Ndermarrje.NdermarrjeKodiFiskal

            };

            var folder = $"{IMBUtils.DataBase.MyConnectionsManager.GetSelectedConNameServer()}/";
            var type = MerrReportDesignType(idDesign, idNdermarrje, idRaporti, orientimi, out var reportName);
            //if (alphaMobile) reportName = "RaportetDs.RAP_SHITJE.Fatura.Rap_Fature_Shitje_Redis";
            if (type != null)
                report = (XtraReport)Activator.CreateInstance(type, parametraPerKonstruktor, report);
            else if (clsReportDesigner.EkzistonRaporti(reportName))
                clsReportDesigner.NgarkoLayoutRaporti(report, reportName, clsReportDesigner.RepxReportsFolder);
            else
                throw new NotImplementedException($"Nuk eshte implementuar disajni me idRaporti = {idRaporti}");

            if (clsReportDesigner.EkzistonRaporti(reportName, folder))
                clsReportDesigner.NgarkoLayoutRaporti(report, reportName, folder);
            else if (clsReportDesigner.EkzistonRaporti(reportName, idNdermarrje))
                clsReportDesigner.NgarkoLayoutRaporti(report, reportName, idNdermarrje);

            report.Extensions["filtrat"] = JsonConvert.SerializeObject(sqlParamShfaqReport);
            report.Extensions["raportgjuha"] = Convert.ToString(idGjuha);
            report.Extensions["parametraRaporti"] = JsonConvert.SerializeObject(parametraPerKonstruktor);

            //sa here krijohet nje raport i ri ruajm emrin e tij ne session
            mySessionObjects.RuajEmerRaporti(HttpContext.Current.Session, reportName, guidString);
            foreach (var param in report.Parameters)
                param.Visible = false;
            return report;
        }

        private static Type MerrReportDesignType(int idDesign, int idNdermarrje, int idRaporti, string orientimi, out string reportName)
        {
            //TODO GETSON reports should be loaded automatically, not hardcoded
            var rapDesign = (idDesign != 0) ? new clsRaportDesign(idDesign) : new colRaporteDesign(idNdermarrje, idRaporti).MerrDizajnTeZgjedhur();

            reportName = rapDesign.GetReportName(orientimi);
            var reportsDlls = new[]
            {
                "AlphaWebReports",
                "AlphaWebReports.Amortizimi",
                "AlphaWebReports.Arka",
                "AlphaWebReports.Banka",
                "AlphaWebReports.ListPagesat",
                "AlphaWebReports.Blerje",
                "AlphaWebReports.Shitje",
                "AlphaWebReports.Magazina",
                "AlphaWebReports.KlientFurnitor",
                "AlphaWebReports.Kontabiliteti"
            };
            foreach (var reportDll in reportsDlls)
            {
                var a = Assembly.Load(reportDll);
                //This is a hack in order to keep things simple.. We try to find the report by original namespace not (AlphaWebReports)
                //this should be changed in the near future.
                Type t = a.GetType("AlphaWebReports." + reportName);
                if (t == null)
                {
                    rapDesign = rapDesign.MerrDesignOrigjinal();
                    t = a.GetType("AlphaWebReports." + rapDesign.GetReportName(orientimi));
                }
                if (t != null)
                {
                    return t;
                }
            }
            return null;
        }

        /// <summary>
        /// Vendos pershrimin e parametrave qe do i kalohen raportit, per tu shfaqur ne zonen e filtrave te aplikuar mbi raport
        /// </summary>
        /// <param name="raport">Raporti</param>
        /// <param name="sqlParamShfaqReport">Lista e parametrave te shfaqjes</param>
        private static void KrijoParametraRaporti(XtraReport raport, colParameter sqlParamShfaqReport, string raportiEmerReal)
        {
            foreach (clsParameter parameter in sqlParamShfaqReport)
            {
                var parametri = new Parameter
                {
                    Name = parameter.Emri,
                    Type = typeof(string),
                    Value = parameter.Vlera
                };

                switch (parameter.Emri)
                {
                    case "filterDtTakimi":
                        parametri.Description = MessagesResource.Messages[""];
                        break;
                    case "filterDtMbarimi":
                        parametri.Description = MessagesResource.Messages[""];
                        break;
                    case "filterMenyrePagese":
                        parametri.Description = MessagesResource.Messages["filterMenyrePagese"];
                        break;
                    case "filterKonvertoNe":
                        parametri.Description = MessagesResource.Messages["filterKonvertoNe"];
                        break;
                    case "filterArsyeja":
                        parametri.Description = MessagesResource.Messages["filterArsyeja"];
                        break;
                    case "filterKujtIAdresohet":
                        parametri.Description = MessagesResource.Messages["filterKujtIAdresohet"];
                        break;
                    case "filterAfishoPagen":
                        parametri.Description = MessagesResource.Messages["filterAfishoPagen"];
                        break;
                    case "filterdtFillimi":
                        parametri.Description = MessagesResource.Messages["filterdtFillimi"];
                        break;
                    case "filterStatusCRM":
                        parametri.Description = MessagesResource.Messages["filterStatusCRM"];
                        break;
                    case "filterGlobalBand":
                        parametri.Description = MessagesResource.Messages["filterGlobalBand"];
                        break;
                    case "filterLocalBand":
                        parametri.Description = MessagesResource.Messages["filterLocalBand"];
                        break;
                    case "filterQenderKosto2":
                        parametri.Description = MessagesResource.Messages["filterQenderKosto2"];
                        break;
                    case "filterQenderKosto1":
                        parametri.Description = MessagesResource.Messages["filterQenderKosto1"];
                        break;
                    case "filterNrSigurimesh":
                        parametri.Description = MessagesResource.Messages["filterNrSigurimesh"];
                        break;
                    case "filterSAPID":
                        parametri.Description = MessagesResource.Messages["filterSAPID"];
                        break;
                    case "filterStatusHR":
                        parametri.Description = MessagesResource.Messages["filterStatusHR"];
                        break;
                    case "filterJobTitle":
                        parametri.Description = MessagesResource.Messages["filterJobTitle"];
                        break;
                    case "filterLlogariSintetike":
                        parametri.Description = MessagesResource.Messages["filterLlogariSintetike"];
                        break;
                    case "filterVeprimePeriudhe":
                        parametri.Description = MessagesResource.Messages["filterVeprimePeriudhe"];
                        break;
                    case "filterGjendjeDetyrime":
                        parametri.Description = MessagesResource.Messages["filterGjendjeDetyrime"];
                        break;
                    case "filterGjendja":
                        parametri.Description = MessagesResource.Messages["filterGjendja"];
                        break;
                    case "filterDtDok":
                    case "filterDateKonvertoPike":
                    case "filterDtDokKonvertuar":
                        parametri.Description = MessagesResource.Messages["filterDtDok"];
                        break;
                    case "filterDtKrijimiSeriali":
                        parametri.Description = MessagesResource.Messages["filterDateKrijimi"];
                        break;
                    case "filterDtKrijimi":
                        parametri.Description = MessagesResource.Messages["filterDateKrijimi"];
                        break;
                    case "filterOreKrijimi":
                        parametri.Description = MessagesResource.Messages["labelFilterAvancuarOreKrijimi"];
                        break;
                    case "filterDtDokKrahasues":
                    case "filterDtDokKrahasuesQK":
                        parametri.Description = MessagesResource.Messages["filterDtDokKrahasues"];
                        break;
                    case "filterDtDokKrahasuesMbarim":
                        parametri.Description = MessagesResource.Messages["filterDtDokKrahasuesMbarim"];
                        break;
                    case "filterDtDok2":
                        parametri.Description = MessagesResource.Messages["filterDtDok2"];
                        break;
                    case "filterDtDokKryesor":
                        parametri.Description = MessagesResource.Messages["filterDtDokKryesor"];
                        break;
                    case "filterDtDokLidhes":
                        parametri.Description = MessagesResource.Messages["filterDtDokLidhes"];
                        break;
                    case "filterDtRegj":
                        parametri.Description = MessagesResource.Messages["filterDtRegj"];
                        break;
                    case "filterNumerDokumenti":
                    case "filterNumerDokumenti1":
                        parametri.Description = MessagesResource.Messages["FilterRaportiNumerDokumenti"];
                        break;
                    case "filterNrSerial":
                        parametri.Description = MessagesResource.Messages["FilterRaportiNumerDokumenti"];
                        break;
                    case "filterNrKarte":
                        parametri.Description = MessagesResource.Messages["filterNrKarte"];
                        break;
                    case "filterNrDokKonvertuar":
                        parametri.Description = MessagesResource.Messages["filterNrDokKonvertuar"];
                        break;
                    case "filterNumerDokumentiKryesor":
                        parametri.Description = MessagesResource.Messages["filterNumerDokumentiKryesor"];
                        break;
                    case "filterDtAmortizimi":
                        parametri.Description = MessagesResource.Messages["filterDtAmortizimi"];
                        break;
                    case "filterNumerDokumentiLidhes":
                        parametri.Description = MessagesResource.Messages["filterNumerDokumentiLidhes"];
                        break;
                    case "filterPershkrimi":
                        parametri.Description = MessagesResource.Messages["filterRaportPershkrimi"];
                        break;
                    case "filterNiveli":
                        parametri.Description = MessagesResource.Messages["filterNiveli"];
                        break;
                    case "filterNumerLlogarie":
                    case "filterNumerLlogarie1":
                    case "filterNrLlogaria":
                        parametri.Description = MessagesResource.Messages["filterRaportiNumerLlogarie"];
                        break;
                    case "filterNrReference":
                        parametri.Description = MessagesResource.Messages["filterNrReference"];
                        break;
                    case "IdNdermarje":
                    case "idndermarje":
                        parametri.Description = MessagesResource.Messages["filterRaportIdNdermarje"];
                        break;
                    case "filterLlojDokumenti":
                        parametri.Description = MessagesResource.Messages["filterRaportLlojDokumenti"];
                        break;
                    case "filterLlojDokumentiLidhes":
                        parametri.Description = MessagesResource.Messages["filterLlojDokumentiLidhes"];
                        break;
                    case "filterLlojDokumentiKryesor":
                        parametri.Description = MessagesResource.Messages["filterLlojDokumentiKryesor"];
                        break;
                    case "filterMonedha":
                        parametri.Description = MessagesResource.Messages["filterMonedha"];
                        break;
                    case "filterGrupeBanke":
                        parametri.Description = MessagesResource.Messages["lblGrupBanke"];
                        break;
                    case "filterFurnitor":
                    case "filterFurnitor1":
                    case "filterKlientFurnitor":
                        parametri.Description = MessagesResource.Messages["filterFurnitor"];
                        break;
                    case "filterKlientVartes":
                        parametri.Description = raportiEmerReal == "situacioniklientevememarreveshje"
                            ? MessagesResource.Messages["filterFurnitor"]
                            : MessagesResource.Messages["filterKlientiVartes"];
                        break;
                    case "filterPolitike":
                        parametri.Description = MessagesResource.Messages["labelPolitike"];
                        break;
                    case "filterNjesiVartese":
                        parametri.Description = MessagesResource.Messages["labelNjesiVartese"];
                        break;
                    case "filterKlientiProdhim":
                        parametri.Description = MessagesResource.Messages["MenuItemKlientFurnitor"] + ":";
                        break;
                    case "filterMagazina":
                    case "filterMagazina1":
                    case "filterMagazinaPaLidhese":
                        parametri.Description = MessagesResource.Messages["filterMagazina"];
                        break;
                    case "filterKartaKlient":
                        parametri.Description = MessagesResource.Messages["filterKartaKlientit"];
                        break;
                    case "filterkartaklient":
                        parametri.Description = MessagesResource.Messages["filterKartaKlientit"];
                        break;
                    case "filterMagazinaDes":
                        parametri.Description = MessagesResource.Messages["filterMagazinaDes"];
                        break;
                    case "filterKartela":
                        parametri.Description = MessagesResource.Messages["filterKartela"];
                        break;
                    case "filterQyteti":
                        parametri.Description = MessagesResource.Messages["filterQyteti"];
                        break;
                    case "filterPajisje":
                        parametri.Description = MessagesResource.Messages["labelFilterAvancuarPajisje"] + ":";
                        break;
                    case "filterArkaBanka":
                        parametri.Description = MessagesResource.Messages["filterArkaBanka"];
                        break;
                    case "filterShenimeLP":
                    case "filterShenimeShitje1":
                        parametri.Description = MessagesResource.Messages["filterRaportShenimeShitje"];
                        break;
                    case "filterArkaBankaEmer":
                        parametri.Description = MessagesResource.Messages["filterArkaBankaEmer"];
                        break;
                    case "filterGrupimP":
                        parametri.Description = MessagesResource.Messages["filterGrupimP"];
                        break;
                    case "filterGrupimD":
                        parametri.Description = MessagesResource.Messages["filterGrupimD"];
                        break;
                    case "filterGrupimPKF":
                        parametri.Description = MessagesResource.Messages["filterGrupimPKF"];
                        break;
                    case "filterGrupimDKF":
                        parametri.Description = MessagesResource.Messages["filterGrupimDKF"];
                        break;
                    case "filterGrupimTKF":
                        parametri.Description = MessagesResource.Messages["filterGrupimTKF"];
                        break;
                    case "filterGrupimDokP":
                        parametri.Description = MessagesResource.Messages["filterGrupimDokP"];
                        break;
                    case "filterGrupimDokD":
                        parametri.Description = MessagesResource.Messages["filterGrupimDokD"];
                        break;
                    case "filterGrupimDokT":
                        parametri.Description = MessagesResource.Messages["filterGrupimDokT"];
                        break;
                    case "filterKodbari":
                        parametri.Description = MessagesResource.Messages["filterKodbari"];
                        break;
                    case "filterNjesiArtikulli":
                        parametri.Description = MessagesResource.Messages["filterNjesiArtikulli"];
                        break;
                    case "filterKategorizimArtikuj":
                        parametri.Description = MessagesResource.Messages["filterKategorizimArtikuj"];
                        break;
                    case "filterPikeshitjeFurnizmi":
                        parametri.Description = MessagesResource.Messages["filterPikeshitjeFurnizmi"];
                        break;
                    case "filterLlojArtikulli":
                        parametri.Description = MessagesResource.Messages["filterLlojArtikulli"];
                        break;
                    case "filterMeMbylljeViti":
                        parametri.Description = MessagesResource.Messages["filterMeMbylljeViti"];
                        break;
                    case "filterRecepturaNenprodukte":
                        parametri.Description = MessagesResource.Messages["filterRecepturaNenprodukte"];
                        break;
                    case "filterCikli":
                        parametri.Description = MessagesResource.Messages["filterCikli"];
                        parametri.Type = typeof(object);
                        break;
                    case "filterShfaqVlerat":
                        parametri.Description = MessagesResource.Messages["filterShfaqVlerat"];
                        break;
                    case "filterTipGrafiku":
                        parametri.Description = MessagesResource.Messages["filterTipGrafiku"];
                        break;
                    case "filterTipGrafikuKrahasues":
                        parametri.Description = MessagesResource.Messages["filterTipGrafikuKrahasues"];
                        break;
                    case "filterLikuiduar":
                    case "filterStatus":
                        parametri.Description = MessagesResource.Messages["filterLikuiduar"];
                        break;
                    case "filterKlasaArtikullit":
                        parametri.Description = MessagesResource.Messages["filterKlasaArtikullit"];
                        break;
                    case "Azhornim":
                        parametri.Description = MessagesResource.Messages["filterRaportAzhornim"];
                        break;
                    case "monedheNdermarje":
                        parametri.Description = MessagesResource.Messages["filterMonedha"];
                        break;
                    case "filterMuaji":
                        parametri.Description = MessagesResource.Messages["filterMuaji"];
                        break;
                    case "filterNrPersonalPunonjesi":
                        parametri.Description = MessagesResource.Messages["filterNrPersonalPunonjesi"];
                        break;
                    case "filterKodDep":
                        parametri.Description = MessagesResource.Messages["filterKodDep"];
                        break;
                    case "filterKodNenDep":
                        parametri.Description = MessagesResource.Messages["filterKodNenDep"];
                        break;
                    case "filterFurnitorArt":
                        parametri.Description = MessagesResource.Messages["filterFurnitorArt"];
                        break;
                    case "filterNrUrdherPagese":
                        parametri.Description = MessagesResource.Messages["filterNrUrdherPagese"];
                        break;
                    case "filterNrAprovimit":
                        parametri.Description = MessagesResource.Messages["filterNrAprovimit"];
                        break;
                    case "filterNrSerialUrdherPagese":
                        parametri.Description = MessagesResource.Messages["filterNrSerialUrdherPagese"];
                        break;
                    case "filterNumerProjekti":
                        parametri.Description = MessagesResource.Messages["filterNumerProjekti"];
                        break;
                    case "filterAktivitetiKlient":
                        parametri.Description = MessagesResource.Messages["filterAktivitetiKlient"];
                        break;
                    case "filterLlogariEkonomike":
                        parametri.Description = MessagesResource.Messages["filterLlogariEkonomike"];
                        break;
                    case "filterNenLlogariEkonomike":
                        parametri.Description = MessagesResource.Messages["filterNenLlogariEkonomike"];
                        break;
                    case "filterDtUrdherPagese":
                        parametri.Description = MessagesResource.Messages["filterDtUrdherPagese"];
                        break;
                    case "filterDtAprovimit":
                        parametri.Description = MessagesResource.Messages["filterDtAprovimit"];
                        break;
                    case "filterDegeAdministrative":
                    case "filterDegeAdminPaLidhese":
                        parametri.Description = MessagesResource.Messages["filterDegeAdministrative"];
                        break;
                    case "filterCmimeArtikulli":
                        parametri.Description = MessagesResource.Messages["filterCmimeArtikulli"];
                        break;
                    case "filterDtDokCmimArtikulli":
                        parametri.Description = MessagesResource.Messages["filterDtDokCmimArtikulli"];
                        break;
                    case "filterLlojiArtBurim":
                        parametri.Description = MessagesResource.Messages["filterLlojiArtBurim"];
                        break;
                    case "filterDetajimP":
                        parametri.Description = MessagesResource.Messages["filterDetajimP"];
                        break;
                    case "filterDetajimD":
                        parametri.Description = MessagesResource.Messages["filterDetajimD"];
                        break;
                    case "filterKategoriMaturimi":
                        parametri.Description = MessagesResource.Messages["labelRaportKategoriMaturimi"];
                        break;
                    case "filterdtPeriudheMaturimi":
                        parametri.Description = MessagesResource.Messages["labelRaportDtMaturimi"] + ": ";
                        break;
                    case "filterPerdoruesi":
                        parametri.Description = raportiEmerReal != "HistorikuTeDhenaveTePerfaqesuesveTeShitjes"
                            ? MessagesResource.Messages["filterRaportPerdoruesi"]
                            : MessagesResource.Messages["lblPerdoruesVod"];
                        break;
                    case "filterPershkrimFature":
                        parametri.Description = MessagesResource.Messages["filterPershkrimFature"];
                        break;
                    case "filtertarga":
                        parametri.Description = MessagesResource.Messages["labelRaportTarga"];
                        break;
                    case "filtershoferi":
                        parametri.Description = MessagesResource.Messages["filterRaportiShoferi"];
                        break;
                    case "filterDega":
                        parametri.Description = MessagesResource.Messages["filterDega"];
                        break;
                    case "filterLlogariDebi":
                        parametri.Description = MessagesResource.Messages["filterllogaridebiprocredit"];
                        break;
                    case "filterLlogariKredi":
                        parametri.Description = MessagesResource.Messages["filterllogarikrediprocredit"];
                        break;
                    case "filterKrijuesi":
                        parametri.Description = MessagesResource.Messages["filterRaportKrijuesi"];
                        break;
                    case "filterNumerSerial":
                        parametri.Description = MessagesResource.Messages["filterNumerSerial"];
                        break;
                    case "filterAgjentShitje":
                        parametri.Description = MessagesResource.Messages["filterRaportiAgjentetShitjes"];
                        break;
                    case "filterNivelCmimi":
                        parametri.Description = MessagesResource.Messages["filterRaportiNiveletCmimit"];
                        break;
                    case "filterkodArtikullBurim":
                        parametri.Description = MessagesResource.Messages["filterkodArtikullBurim"];
                        break;
                    case "filterQenderKosto":
                        parametri.Description = MessagesResource.Messages["filterQenderKosto"];
                        break;
                    case "filterNrDokQenderKosto":
                        parametri.Description = MessagesResource.Messages["FilterRaportiNumerDokumenti"];
                        break;
                    case "filterDtDokQenderKosto":
                        parametri.Description = MessagesResource.Messages["filterDtDok"];
                        break;
                    case "filterkodifikimartP":
                        parametri.Description = MessagesResource.Messages["filterGrupimPare"];
                        break;
                    case "filterAfishoArtikuj":
                        parametri.Description = MessagesResource.Messages["lblAfishoArtikuj"];
                        break;
                    case "filterStatusShperndarje":
                        parametri.Description = MessagesResource.Messages["filterStatusShperndarje"];
                        break;
                    case "filterStatusRiparimi":
                        parametri.Description = MessagesResource.Messages["filterStatusRiparimi"];
                        break;
                    case "filterkodifikimartD":
                        parametri.Description = MessagesResource.Messages["filterGrupimDyte"];
                        break;
                    case "filterkodifikimartT":
                        parametri.Description = MessagesResource.Messages["filterGrupimTrete"];
                        break;
                    case "filterNrDokRezervime":
                        parametri.Description = MessagesResource.Messages["filterNrDokRezervime"];
                        break;
                    case "filterLlogariPacaktuar":
                        parametri.Description = MessagesResource.Messages["filterLlogariPacaktuar"];
                        break;
                    case "filterSasiaPakonvertuar":
                        parametri.Description = MessagesResource.Messages["filterSasiaPakonvertuar"];
                        break;
                    case "filterArtGjendjeZero":
                        parametri.Description = MessagesResource.Messages["filterArtGjendjeZero"];
                        break;
                    case "filterDtDokAfatKohor":
                        parametri.Description = MessagesResource.Messages["filterDtDokAfatKohor"];
                        break;
                    case "filterStatusKonvertimi":
                        parametri.Description = MessagesResource.Messages["filterStatusKonvertimi"];
                        break;
                    case "filterShoqeria":
                        parametri.Description = MessagesResource.Messages["filterRaportiShoqeria"];
                        break;
                    case "filterNrTel":
                        parametri.Description = MessagesResource.Messages["filterRaportiNrTel"];
                        break;
                    case "filterCustomerNr":
                        parametri.Description = raportiEmerReal != "PagesatEKryeraPerMPesa"
                            ? "Customer Number"
                            : MessagesResource.Messages["filterSrNumber"];
                        break;
                    case "filterdtPeriudheFillimi":
                        parametri.Description = MessagesResource.Messages["filterdtPeriudheFillimi"];
                        break;
                    case "filterdtPeriudheMbarimi":
                        parametri.Description = MessagesResource.Messages["filterdtPeriudheMbarimi"];
                        break;
                    case "filterVlereShitje":
                        parametri.Description = MessagesResource.Messages["filterVlereShitje"];
                        break;
                    case "filterDtDokPlanifikimi":
                        parametri.Description = MessagesResource.Messages["filterDtDokPlanifikimi"];
                        break;
                    case "filterDtDokProdhimi":
                        parametri.Description = MessagesResource.Messages["filterDtDokProdhimi"];
                        break;
                    case "filterKompania":
                        parametri.Description = MessagesResource.Messages["filterKompania"];
                        break;
                    case "filterDtDokFshirje":
                        parametri.Description = MessagesResource.Messages["filterDtDokFshirje"];
                        break;
                    case "filterPaguar":
                        parametri.Description = MessagesResource.Messages["filterPaguar"];
                        break;
                    case "filterGrupoKlientSipas":
                        parametri.Description = MessagesResource.Messages["filterGrupoKlientSipas"];
                        break;
                    case "filterGrupoSipasArt":
                        parametri.Description = MessagesResource.Messages["filterGrupoSipasArt"];
                        break;
                    case "filterLlojPorosie":
                        parametri.Description = MessagesResource.Messages["filterLlojPorosie"];
                        break;
                    case "filterStatusPerfunduar":
                        parametri.Description = MessagesResource.Messages["filterStatusPerfunduar"];
                        break;
                    case "filterStatusProdhuar":
                        parametri.Description = MessagesResource.Messages["filterStatusProdhuar"];
                        break;
                    case "filterStatusFaturuar":
                        parametri.Description = MessagesResource.Messages["filterStatusFaturuar"];
                        break;
                    case "filterEmertimLlog":
                        parametri.Description = MessagesResource.Messages["filterEmertimLlog"];
                        break;
                    case "filterStandarti":
                        parametri.Description = MessagesResource.Messages["filterStandarti"];
                        break;
                    case "filterSeriali":
                        parametri.Description = MessagesResource.Messages["filterSeriali"];
                        break;
                    case "filterlblKategoriSeriali":
                        parametri.Description = MessagesResource.Messages["filterlblKategoriSeriali"];
                        break;
                    case "filterAuto":
                        parametri.Description = MessagesResource.Messages["filterRaportiAutomjeti"];
                        break;
                    case "filterStatusMagazine":
                        parametri.Description = MessagesResource.Messages["filterRaportiStatusMagazine"];
                        break;
                    case "filterDtReg":
                        parametri.Description = "Dt.Reg";
                        break;
                    case "filterAdreseKlienti":
                        parametri.Description = MessagesResource.Messages["filterAdreseKlienti"];
                        break;
                    case "filterLlogariParapagimi":
                        parametri.Description = MessagesResource.Messages["filterRaportLlogParapagimi"] + ":";
                        break;
                    case "filterLlogariShpenzimi":
                        parametri.Description = MessagesResource.Messages["filterRaportLlogShpenzimi"] + ":";
                        break;
                    case "filterpershkrimArt":
                        parametri.Description = MessagesResource.Messages["filterRaportPershkrimiArtikullit"] + ":";
                        break;
                    case "filterShenim2Shitje":
                        parametri.Description = MessagesResource.Messages["filterRaportPershkrimiArtikullit"] + ":";
                        break;
                    case "filterArtMeCmim":
                        parametri.Description = MessagesResource.Messages["labelFilterArtCmim"] + ":";
                        break;
                    case "filterShenimShitje":
                        parametri.Description = MessagesResource.Messages["filterRaportPershkrimiArtikullit"] + ":";
                        break;
                    case "filterLlojDetajim":
                        parametri.Description = MessagesResource.Messages["filterLlojiDetajimit"] + ":";
                        break;
                    case "filterPershkrimDetajimi":
                        parametri.Description = MessagesResource.Messages["filterPershkrimDetajimi"] + ":";
                        break;
                    case "filterAdresaFaturimit":
                        parametri.Description = MessagesResource.Messages["lblFilterAdreseFaturimi"];
                        break;
                    case "filterLlojVeprimi":
                        parametri.Description = MessagesResource.Messages["filterLlojVeprimi"];
                        break;
                    case "filterDtDokHistorik":
                        parametri.Description = MessagesResource.Messages["filterDtDok"];
                        break;
                    case "filterDtRegjHistorik":
                        parametri.Description = MessagesResource.Messages["filterDtRegj"];
                        break;
                    case "filterPerdoruesiHistorik":
                        parametri.Description = MessagesResource.Messages["filterRaportPerdoruesi"];
                        break;
                    case "filterLlojDokHistorik":
                        parametri.Description = MessagesResource.Messages["filterRaportLlojDokumenti"];
                        break;
                    case "filterNrDokHistorik":
                        parametri.Description = MessagesResource.Messages["FilterRaportiNumerDokumenti"];
                        break;
                    case "filterMagazinaHistorik":
                        parametri.Description = MessagesResource.Messages["filterMagazina"];
                        break;
                    case "filterArkaBankaHistorik":
                        parametri.Description = MessagesResource.Messages["filterArkaBanka"];
                        break;
                    case "filterDegaAdministrativeHistorik":
                        parametri.Description = MessagesResource.Messages["filterDegeAdministrative"];
                        break;
                    case "filterPikeshitjeFurnizmiHistorik":
                        parametri.Description = MessagesResource.Messages["filterPikeshitjeFurnizmi"];
                        break;
                    case "filterNrProjektProdhimi":
                        parametri.Description = MessagesResource.Messages["filterNrProjektProdhimi"];
                        break;
                    case "filterBurimi":
                        parametri.Description = MessagesResource.Messages["burimiTab"];
                        break;
                    case "filterAktiviteti":
                        parametri.Description = MessagesResource.Messages["labelRaportAktiviteti"];
                        break;
                    case "filterKategoriShpenzimi":
                    case "filterKategoriShpenzimi1":
                        parametri.Description = MessagesResource.Messages["lblKategoriShpenzimi"] + ":";
                        break;
                    case "filterLlojSubjekti":
                        parametri.Description = MessagesResource.Messages["filterLlojSubjekti"];
                        break;
                    case "filterBanka":
                        parametri.Description = MessagesResource.Messages["cmbboxItemFilterAvancBanka"] + ":";
                        break;
                    case "monedheLl":
                        parametri.Description = MessagesResource.Messages["labelFilterAvancuarMonedheLl"] + ":";
                        break;
                    case "grupoSipasGrupim1":
                        parametri.Description = MessagesResource.Messages["labelFilterAvancuarSipasGrupim1"];
                        break;
                    case "filterStatusDok":
                        parametri.Description = MessagesResource.Messages["filterStatus"];
                        break;
                    case "filterStatusDokIntegrimi":
                        parametri.Description = MessagesResource.Messages["labelFilterAvancuarStatusi"];
                        break;
                    case "filterKategoriPrind":
                        parametri.Description = MessagesResource.Messages["labelGrupoSipasKategorisePrind"];
                        break;
                    case "filterGrupKFP1":
                    case "filterGrupKFP2":
                    case "filterGrupKFP3":
                        parametri.Description = MessagesResource.Messages["labelFilterKF1"];
                        break;
                    case "filterGrupKFD1":
                    case "filterGrupKFD2":
                    case "filterGrupKFD3":
                        parametri.Description = MessagesResource.Messages["labelFilterKF2"];
                        break;
                    case "filterGrupKFT1":
                    case "filterGrupKFT2":
                    case "filterGrupKFT3":
                        parametri.Description = MessagesResource.Messages["labelFilterKF3"];
                        break;
                    case "filtroKapitull":
                        parametri.Description = MessagesResource.Messages["labelRaportKapitulli"];
                        break;
                    case "filtroProgram":
                        parametri.Description = MessagesResource.Messages["labelProgrami"];

                        break;
                    case "filterGrupKosto":
                        parametri.Description = MessagesResource.Messages["lblFilterLlojKosto"];
                        break;
                    case "shfaqMagVartese":
                        parametri.Description = MessagesResource.Messages["shfaqMagVartese"];
                        break;
                    case "filterElementeWEBGIS":
                        parametri.Description = MessagesResource.Messages["lblElemLayer"];
                        break;
                    case "filterLlojElementesh":
                        parametri.Description = MessagesResource.Messages["lblLlojiElem"];
                        break;
                    case "filterStatusi":
                        parametri.Description = MessagesResource.Messages["labelFilterAvancuarStatusi"];
                        break;
                    case "filterDateKontrate":
                        parametri.Description = MessagesResource.Messages["lblDateKontrate"];
                        break;
                    case "filterKrahasimKosto":
                        parametri.Description = MessagesResource.Messages["filterKrahasimKosto"];
                        break;
                    case "filterKlientAktiv":
                        parametri.Description = MessagesResource.Messages["filterKlientAktiv"];
                        break;
                    case "filterKatShpAktive":
                        parametri.Description = MessagesResource.Messages["filterKatShpAktive"];
                        break;
                    case "filterCmimMePaTVSH":
                        parametri.Description = MessagesResource.Messages["labelCmimi"];
                        break;
                    case "filterStatusiRezervimit":
                        parametri.Description = MessagesResource.Messages["labelFilterAvancuarStatusi"];
                        break;
                    case "filterGjendjeZero":
                        parametri.Description = MessagesResource.Messages["labelFilterAvancuarGjendja"];
                        break;
                    case "filterShfaqArt":
                        parametri.Description = MessagesResource.Messages["labelFilterAvancuarShfaq"];
                        break;
                    case "filterdogana":
                        parametri.Description = MessagesResource.Messages["lbldogana"];
                        break;
                    case "llojFushaShtese":
                        parametri.Description = MessagesResource.Messages["llojFushaShtese"];
                        break;
                    case "filterModeliFushaShtese":
                        parametri.Description = MessagesResource.Messages["filterModeliFushaShtese"];
                        break;
                    case "filterObjekteGIS":
                        parametri.Description = MessagesResource.Messages["filterObjekteGIS"];
                        break;
                    case "filterKodiTac":
                        parametri.Description = MessagesResource.Messages["filterRaportTac"];
                        break;
                    case "filterIdMarveshje":
                        parametri.Description = MessagesResource.Messages["filterIdMarveshje"];
                        break;
                    case "filterQK":
                        parametri.Description = MessagesResource.Messages["filterQK"];
                        break;
                    case "LlogariaSAP":
                        parametri.Description = MessagesResource.Messages["labelFilterAvancuarPershkLlogariaSAP"];
                        break;
                    case "filterSasiaPerTuPorositur":
                        parametri.Description = MessagesResource.Messages["filterSasiaPerTuPorositur"];
                        break;

                    case "filterDateDokLidhes":
                        parametri.Description = MessagesResource.Messages["filterDateDokLidhes"];
                        break;
                    case "filterIdPunonjes":
                        parametri.Description = MessagesResource.Messages["filterIdPunonjes"];
                        break;
                    case "filterLlojCmimi":
                        parametri.Description = MessagesResource.Messages["filterLlojCmimi"];
                        break;

                    case "filterLlojSubjektiKF":
                        parametri.Description = MessagesResource.Messages["filterLlojSubjektiKF"];
                        break;

                    case "filterDtSkadence1":
                        parametri.Description = MessagesResource.Messages["filterDtSkadence1"];
                        break;

                    case "filterDtSkadence2":
                        parametri.Description = MessagesResource.Messages["filterDtSkadence2"];
                        break;

                    case "filterMagazineSkadence":
                        parametri.Description = MessagesResource.Messages["filterMagazineSkadence"];
                        break;
                    case "filterEIC":
                        parametri.Description = "Filter EIC";
                        break;
                    case "filterStatusiEinvoice":
                        parametri.Description = "Statusi Einvoice";
                        break;
                    case "filterDetajim1Receptura":
                        parametri.Description = "Detajim receptura";
                        break;
                    case "filterDetajim2Receptura":
                        parametri.Description = "Detajim receptura 2";
                        break;
                    case "filterDetajim1Artikulli":
                        parametri.Description = "Detajim artikulli";
                        break;
                    case "filterDetajim2Artikulli":
                        parametri.Description = "Detajim artikulli 2";
                        break;
                }

                parameter.Pershkrimi = parametri.Description;
                raport.Parameters.Add(parametri);
            }
        }

        /// <summary>
        /// Krijon nje objekt XtraReport, pa kaluar parametra.
        /// Nevojitet tek ReportDesigner apo ne rastet kur duam te perdorim layoutin e raportit, por jo te dhenat,
        /// si ne rastin e ReportDesigner ku mund te perdorim layoutin e nje raporti ekzistues tek raporti i ri. 
        /// </summary>
        /// <param name="reportName"> Emri i disajnit, sic eshte ruajtur ne fushen filename ose filenamePortrait ne T_RAPORTDESING</param>
        /// <param name="idNdermarrje">per disajnet e edituara, te cilet jane ruajtur ne nivel ndermarrje</param>
        public static XtraReport MerrRaportinPaParametra(string reportName, int idNdermarrje)
        {
            var report = new XtraReport();
            var a = Assembly.Load("AlphaWebReports");
            var t = a.GetType("AlphaWebReports." + reportName);

            if (t != null)
                report = (XtraReport)(Activator.CreateInstance(t));
            else if (clsReportDesigner.EkzistonRaporti(reportName))
                clsReportDesigner.NgarkoLayoutRaporti(report, reportName, clsReportDesigner.RepxReportsFolder);
            else
                throw new NotImplementedException($"Nuk eshte implementuar disajni {reportName}");

            var folder = $"{IMBUtils.DataBase.MyConnectionsManager.GetSelectedConNameServer()}/{idNdermarrje}/";
            if (clsReportDesigner.EkzistonRaporti(reportName, folder))
                clsReportDesigner.NgarkoLayoutRaporti(report, reportName, folder);
            else if (clsReportDesigner.EkzistonRaporti(reportName, idNdermarrje))
                clsReportDesigner.NgarkoLayoutRaporti(report, reportName, idNdermarrje);

            return report;
        }

        public static bool EshteNdertuarDisajniIRaportit(int idNdermarrje, int idRaporti, string orientimi)
        {
            var t = MerrReportDesignType(0, idNdermarrje, idRaporti, orientimi, out var reportName);
            return t != null || clsReportDesigner.EkzistonRaporti(reportName);
        }

        public static void DergoEmailRaporteRezultatImporti(DataTable dtImportuar, DataTable dtDeshtuar, int idKonfigImporti, string emerFormati, int idNdermarrje, int idPerdoruesi)
        {
            try
            {
                CultureInfo ci = new System.Globalization.CultureInfo("sq-AL");

                clsDatabaseAdmin sharedb = new clsDatabaseAdmin();
                DataTable dt = sharedb.merrStatusDheEmailSipasKonfigImporti(idKonfigImporti);

                List<XtraReport> raportet = new List<XtraReport>();
                List<string> rapEmra = new List<string>();
                string emerRapSukses = "RaportiISukseseve_Import", emerRapGabimi = "RaportiIGabimeve_Import", subjekt = "Nga Importimi";

                foreach (DataRow r in dt.Rows)
                {
                    var emails = r.Field<string>("Emails");
                    if (string.IsNullOrEmpty(emails))
                        continue;

                    var statusi = r.Field<string>("Statusi");
                    raportet.Clear();
                    rapEmra.Clear();

                    #region switch (statusi)    ****Krijimi i raporteve sipas statusit****

                    switch (statusi)
                    {
                        case "Bosh":
                            RaporteRezultatImportiPerEmail(ref raportet, ref rapEmra, idNdermarrje, idPerdoruesi, emerRapGabimi, "Deshtuar", dtDeshtuar, ci);
                            RaporteRezultatImportiPerEmail(ref raportet, ref rapEmra, idNdermarrje, idPerdoruesi, emerRapSukses, "Sukses", dtImportuar, ci);
                            break;
                        case "Deshtuar":
                            RaporteRezultatImportiPerEmail(ref raportet, ref rapEmra, idNdermarrje, idPerdoruesi, emerRapGabimi, "Deshtuar", dtDeshtuar.Copy(), ci);
                            break;
                        case "Importuar":
                            RaporteRezultatImportiPerEmail(ref raportet, ref rapEmra, idNdermarrje, idPerdoruesi, emerRapSukses, "Sukses", dtImportuar.Copy(), ci);
                            break;
                    }

                    #endregion

                    var trupEmail = MessagesResource.Messages["emailImporti" + statusi];
                    trupEmail = trupEmail.Replace("#formatImporti#", emerFormati);

                    EmailComposer.DergoEmaileMeAttach(emails.Split(','), subjekt, trupEmail, idNdermarrje, idPerdoruesi, MerrRaporteNePdf(raportet).ToArray(), rapEmra.ToArray());
                }
            }
            catch (Exception e)
            {
                throw new MyException("Importi perfundoi me sukses, por ndodhi nje gabim gjate dergimit te e-mail!");
            }
        }

        private static List<MemoryStream> MerrRaporteNePdf(List<XtraReport> raportet)
        {
            List<MemoryStream> streams = new List<MemoryStream>(raportet.Count);
            foreach (var rap in raportet)
            {
                var pdfStream = new MemoryStream();
                rap.ExportToPdf(pdfStream);
                streams.Add(pdfStream);
            }

            return streams;
        }

        private static void RaporteRezultatImportiPerEmail(ref List<XtraReport> raportet, ref List<string> rapEmra, int idNdermarrje, int idPerdoruesi, string emerRap, string vjenNga, DataTable data, CultureInfo ci)
        {
            var objRaporti = new clsRaporti(0, 104);
            var design = new clsRaportDesign();
            design.merrSipasNdermarjedheRaport(idNdermarrje, objRaporti.IdRaporti);

            var orientimi = design.FileName != "" ? clsRaportDesign._rap_landscape : design.FileNamePortrait != "" ? clsRaportDesign._rap_portrait : "";

            var xtraReport = KrijoRaportNgaDatatable(orientimi, idPerdoruesi, idNdermarrje, design.IdRaportDesign, data, 0, 104);
            raportet.Add(xtraReport);
            rapEmra.Add(emerRap);
        }

        public static XtraReport krijoDesignRaportFature(int wfStatus, int idDesign, int idPerdoruesi, int idNdermarje, int idNderViti, colParameter sqlParamShfaqReport, string orientimi, String guidString, string scopeId, int idGjuha, int idRaporti, string vjenNga = "")
        {
            var report = krijoObjektRaporti(vjenNga, idGjuha, idPerdoruesi, idNderViti, idRaporti, idNdermarje, sqlParamShfaqReport, idDesign, orientimi, guidString, scopeId, vjenNga);
            if (wfStatus >= 0)
            {
                switch (wfStatus)
                {
                    case (int)DbCore.DbRegjistrim.StatusAprovimi.Undefined:
                        report.Watermark.Text = "";
                        report.Watermark.ForeColor = Color.Green;
                        break;
                    case (int)DbCore.DbRegjistrim.StatusAprovimi.Per_Aprovim:
                        report.Watermark.Text = MessagesResource.Messages["reportWatermarkPerAprovim"];
                        report.Watermark.ForeColor = Color.DodgerBlue;
                        break;
                    case (int)DbCore.DbRegjistrim.StatusAprovimi.Aprovuar:
                        report.Watermark.Text = "";
                        report.Watermark.ForeColor = Color.Green;
                        break;
                    case (int)DbCore.DbRegjistrim.StatusAprovimi.Deleguar:
                        report.Watermark.Text = MessagesResource.Messages["reportWatermarkDeleguar"];
                        report.Watermark.ForeColor = Color.Gold;
                        break;
                    case (int)DbCore.DbRegjistrim.StatusAprovimi.Refuzuar:
                        report.Watermark.Text = MessagesResource.Messages["reportWatermarkRefuzuar"];
                        report.Watermark.ForeColor = Color.Red;
                        break;
                    default:
                        report.Watermark.Text = "";
                        break;
                }

                report.Watermark.TextDirection = DevExpress.XtraPrinting.Drawing.DirectionMode.ForwardDiagonal;
                report.Watermark.Font = new System.Drawing.Font(report.Watermark.Font.FontFamily, 40);
                report.Watermark.TextTransparency = 150;
                report.Watermark.ShowBehind = false;
                report.Watermark.PageRange = "1";
            }

            return report;
        }

        public static void konfigDataSetRaporti(XtraReport report, string spname, int idPerdorues, bool azhornim, int idNdermarje, int idnderviti, DateTime dtmbarimi, int idkonfig, int idperiudha, bool alphaMobile = false, params SqlParameter[] paramarray)
        {
            clsDatabaseAdmin dbAdmin = new clsDatabaseAdmin();
            int maxRetry = 1;
            string mesazhGabimi = MessagesResource.Messages["ndodhiNjeGabimGjateMarrjesTeTeDhenave"];
            clsRetryTrans retryTrans = new clsRetryTrans("Raportet", maxRetry);
            do
            {
                try
                {
                    SqlDataAdapter adapter;
                    DataSet dataset;
                    var mesazh = new clsMesazh(true);

                    dbAdmin.beginTransaksion(IsolationLevel.Snapshot);
                    if (azhornim)
                    {
                        var dbkont = new clsDatabaseKontabilitet(dbAdmin);
                        var koka = new clsKokaFleteKontabel("NKM", idnderviti, dbkont);

                        if (koka.NrDukumentiKokaFleteKontabel != null)
                            mesazh = koka.FshiFleteKontabel(koka.IdKokaFleteKontabel, dbkont);

                        if (!mesazh.Status)
                            throw new MyException(mesazh.PershkrimMesazhi);

                        mesazh = clsKokaFleteKontabel.KrijoFleteAzhornim(idNdermarje, idnderviti, dtmbarimi, idPerdorues, idkonfig, idperiudha, dbkont);
                        if (!mesazh.Status)
                            throw new MyException(mesazh.PershkrimMesazhi);

                        mesazh = dbAdmin.GetReportDataAdapter(out adapter, out dataset, spname, alphaMobile, paramarray);
                        if (!mesazh.Status)
                        {
                            if (mesazh.KodMesazhi != ToInt32(IsolationLevel.Snapshot))
                                retryTrans.stopRetrying();//Errori nuk ka ardhur nga transaksioni. Mos riprovo
                            throw new MyException(mesazh.PershkrimMesazhi);
                        }

                        koka = new clsKokaFleteKontabel("NKM", idnderviti, dbkont);
                        if (koka.NrDukumentiKokaFleteKontabel != null)
                            mesazh = koka.FshiFleteKontabel(koka.IdKokaFleteKontabel, dbkont);

                        if (!mesazh.Status)
                            throw new MyException(mesazh.PershkrimMesazhi);
                    }
                    else
                    {
                        mesazh = dbAdmin.GetReportDataAdapter(out adapter, out dataset, spname, alphaMobile, paramarray);
                        if (!mesazh.Status)
                        {
                            if (mesazh.KodMesazhi != ToInt32(IsolationLevel.Snapshot))
                                retryTrans.stopRetrying();//Errori nuk ka ardhur nga transaksioni. Mos riprovo
                            throw new MyException(mesazh.PershkrimMesazhi);
                        }
                    }

                    if (alphaMobile)
                    {
                        DataTable mainTable = dataset.Tables[0].Copy();
                        DataTable newDataTable = new DataTable();
                        foreach (DataColumn column in mainTable.Columns)
                            newDataTable.Columns.Add(column.ColumnName);
                        List<DateTime> dateTimes = new List<DateTime>();
                        for (int i = 0; i < mainTable.Rows.Count; i++)
                        {
                            DataRow currentRow = mainTable.Rows[i];
                            DateTime currentDate = currentRow.Field<DateTime>("dtdok");

                            double totaliMonBaze = 0;
                            double shitjePerjashtuar = 0;
                            double shitjePaTvsh = 0;
                            double vleftaExporte = 0;
                            double furnizimeZero = 0;
                            double vlefta20 = 0;
                            double tvsh20 = 0;
                            double vlefta10 = 0;
                            double tvsh10 = 0;
                            double vlefta6 = 0;
                            double tvsh6 = 0;
                            double totaliAgj = 0;
                            double totaliAuto = 0;
                            double totaliBorxhi = 0;
                            double tvshBorxhi = 0;
                            if (dateTimes.Contains(currentDate)) continue;
                            for (int j = 0; j < mainTable.Rows.Count; j++)
                            {
                                DataRow secondRow = mainTable.Rows[j];
                                if (secondRow.Field<DateTime>("dtdok") == currentDate)
                                {
                                    totaliMonBaze += secondRow.Field<double>("TOTALIMONBAZE");
                                    shitjePerjashtuar += secondRow.Field<double>("SHITJEPERJASHTUAR");
                                    shitjePaTvsh += secondRow.Field<double>("SHITJEPaTVSH");
                                    vleftaExporte += secondRow.Field<double>("VLEFTAEXPORTE");
                                    furnizimeZero += secondRow.Field<double>("furnizimeZero");
                                    vlefta20 += secondRow.Field<double>("VLEFTA20");
                                    tvsh20 += secondRow.Field<double>("TVSH20");
                                    vlefta10 += secondRow.Field<double>("VLEFTA10");
                                    tvsh10 += secondRow.Field<double>("TVSH10");
                                    vlefta6 += secondRow.Field<double>("VLEFTA6");
                                    tvsh6 += secondRow.Field<double>("TVSH6");
                                    //totaliAgj += secondRow.Field<double>("totaliagjente");
                                    totaliAuto += secondRow.Field<double>("tvshautongarkese");
                                    //totaliBorxhi += secondRow.Field<double>("TOTALIBORXHI");
                                    //tvshBorxhi += secondRow.Field<double>("TVSHBORXHI");
                                }

                            }
                            dateTimes.AddIfNotExists(currentDate);

                            dynamic[] rowsToInsert = new dynamic[]
                            {
                                "",
                                "",
                                currentDate.ToString("yyyy-MM-dd"),
                                "",
                                "",
                                "",
                                totaliMonBaze,
                                shitjePerjashtuar,
                                shitjePaTvsh,
                                vleftaExporte,
                                furnizimeZero,
                                vlefta20,
                                tvsh20,
                                vlefta10,
                                tvsh10,
                                vlefta6,
                                tvsh6,
                                totaliAgj,
                                totaliAuto,
                                totaliBorxhi,
                                tvshBorxhi,
                                "",
                                "",
                                "",

                            };
                            newDataTable.Rows.Add(rowsToInsert);



                        }
                        //var a = ;
                        dataset.Tables.Remove(dataset.Tables[0]);
                        dataset.Tables.Add(newDataTable);
                        //newDataTable.Rows.Add(new dynamic[""]);
                    }
                    report.DataSource = dataset;
                    report.DataAdapter = adapter;
                    report.DataMember = spname;
                    retryTrans.stopRetrying();
                    dbAdmin.Dispose();
                }
                catch (Exception ex)
                {
                    dbAdmin.rollbackTransaksion();
                    if (!retryTrans.checkRetry(ex, spname, maxRetry, mesazhGabimi))
                    {
                        ImbLogger.Error(ex.Message);
                        throw ex;
                    }
                    dbAdmin = new clsDatabaseAdmin();
                }
            } while (retryTrans.isRetrying());
        }

        private static void ShrinkDataSet(DataSet dataset)
        {
            var table = dataset.Tables[0];
            if (table.Rows.Count == 0)
                return;

            table.Rows.Clear();
            // table.Rows.Add(table.Rows[0]);
        }

        public static void konfigDataSetRaporti(XtraReport report, string spname, bool sampleData, bool alphaMobile = false, params SqlParameter[] paramarray)
        {
            var dbAdmin = new clsDatabaseAdmin();
            const int maxRetry = 50;

            var retryTrans = new clsRetryTrans("Raportet", maxRetry);

            do
            {
                try
                {
                    dbAdmin.beginTransaksion(IsolationLevel.Snapshot);

                    var mesazh = dbAdmin.GetReportDataAdapter(out var adapter, out var dataset, spname, alphaMobile, paramarray);
                    if (!mesazh.Status)
                    {
                        if (mesazh.KodMesazhi != ToInt32(IsolationLevel.Snapshot))
                            retryTrans.stopRetrying();//Errori nuk ka ardhur nga transaksioni. Mos riprovo

                        throw new MyException(mesazh.PershkrimMesazhi);
                    }

                    if (sampleData)
                        ShrinkDataSet(dataset);

                    dbAdmin.commitTransaksion();
                    report.DataSource = dataset;
                    report.DataAdapter = adapter;
                    report.DataMember = spname;
                    retryTrans.stopRetrying();
                }
                catch (Exception ex)
                {
                    dbAdmin.rollbackTransaksion();
                    if (!retryTrans.checkRetry(ex, spname, maxRetry, "Ndodhi një gabim gjatë marrjes të të dhënave. Ju lutem provoni përsëri!"))
                    {
                        ImbLogger.Error(ex.Message);
                        throw;
                    }
                    dbAdmin = new clsDatabaseAdmin();
                }
            } while (retryTrans.isRetrying());
        }

        public static void CreateReportSurveyCrm(MemoryStream excelStream, MemoryStream htmlStream, int idPerdoruesi, int idNdermarrje, DateTime dtTakimi, clsAgjentShitje agjenti, int idGjuha)
        {
            var report = new clsRaporti(idGjuha, "analitikVeprimtari");
            XtraReport xtraReport;
            try
            {
                xtraReport = krijoObjektRaporti("", idGjuha, idPerdoruesi, 0, report.IdRaporti, idNdermarrje, null, 0, clsRaportDesign._rap_landscape, Guid.NewGuid().ToString(), string.Empty);
            }
            catch (Exception ex)
            {
                throw new MyException($"ndodhi nje gabim ne krijimin e raportit ex {ex.Message}");
            }
            try
            {
                var parametraRaporti = new Dictionary<string, object>
                {
                    {"filterDtTakimi", dtTakimi.ToString("yyyy/MM/dd")},
                    {"idPerdoruesi", idPerdoruesi},
                    {"IdNdermarje", idNdermarrje },
                    {"filterAgjentShitje", agjenti.KodiAgjentShitje }
                };

                var slqParameters = report.CreateParameters(parametraRaporti);
                var storeProcedureName = clsSP.GetStoredProcedureName(report.IdSp);
                konfigDataSetRaporti(xtraReport, storeProcedureName, false, false, slqParameters);
            }
            catch (Exception ex)
            {
                throw new MyException($"ndodhi nje gabim ne mbushjen e raportit ex {ex.Message}");
            }

            xtraReport.ExportToXlsx(excelStream);
            xtraReport.ExportToImage(htmlStream);
        }

        public static void CreateReportBirthdayCard(MemoryStream memoryStream, int idPerdoruesi, int idNdermarrje, int idDesign, DateTime dtDitelindje, string kodKlienti, int idGjuha)
        {
            var report = new clsRaporti(idGjuha, "Kartolina_ditelindjes_klientit");
            XtraReport xtraReport;
            try
            {
                xtraReport = krijoObjektRaporti("", idGjuha, idPerdoruesi, 0, report.IdRaporti, idNdermarrje, null, idDesign, clsRaportDesign._rap_portrait, Guid.NewGuid().ToString(), string.Empty);
            }
            catch (Exception ex)
            {
                throw new MyException($"ndodhi nje gabim ne krijimin e raportit ex {ex.Message}");
            }
            try
            {
                var parametraRaporti = new Dictionary<string, object>
                {
                    {"filterDtDok", ""},
                    {"filterDtDok1", dtDitelindje.ToString("dd/MM/yyyy")},
                    {"filterDtDok2",dtDitelindje.ToString("dd/MM/yyyy")},
                    {"IdNdermarje", idNdermarrje},
                    {"filterKlientFurnitor", kodKlienti}
                };

                var slqParameters = report.CreateParameters(parametraRaporti);
                var storeProcedureName = clsSP.GetStoredProcedureName(report.IdSp);
                konfigDataSetRaporti(xtraReport, storeProcedureName, false, false, slqParameters);
            }
            catch (Exception ex)
            {
                throw new MyException($"ndodhi nje gabim ne mbushjen e raportit ex {ex.Message}");
            }

            xtraReport.ExportToPdf(memoryStream);
        }
        public static void CreateReportGjendjaArtMinMaxSipasMag(MemoryStream memoryStream, int idPerdoruesi, int idNdermarrje, int idDesign, DateTime datedergimierap, int idGjuha)
        {
            var report = new clsRaporti(idGjuha, "gjendjeArtikujshMinMaxSipasMagazines");
            XtraReport xtraReport;
            try
            {
                xtraReport = krijoObjektRaporti("", idGjuha, idPerdoruesi, 0, report.IdRaporti, idNdermarrje, null, idDesign, clsRaportDesign._rap_portrait, Guid.NewGuid().ToString(), string.Empty);
            }
            catch (Exception ex)
            {
                throw new MyException($"ndodhi nje gabim ne krijimin e raportit ex {ex.Message}");
            }
            try
            {
                var parametraRaporti = new Dictionary<string, object>
                {
                    {"filterDtDok", ""},
                    {"filterDtDok1", datedergimierap.ToString("dd/MM/yyyy")},
                    {"filterDtDok2",datedergimierap.ToString("dd/MM/yyyy")},
                    {"IdNdermarje", idNdermarrje},
                    {"filterShfaqArt",3},
                    {"filterSasiaPakonvertuar","PO" },
                    {"filterSasiaPerTuPorositur","PO" },
                    { "filterGjendjeZero" ,0}

                };

                var slqParameters = report.CreateParameters(parametraRaporti);
                var storeProcedureName = clsSP.GetStoredProcedureName(report.IdSp);
                konfigDataSetRaporti(xtraReport, storeProcedureName, false, false, slqParameters);
            }
            catch (Exception ex)
            {
                throw new MyException($"ndodhi nje gabim ne mbushjen e raportit ex {ex.Message}");
            }

            xtraReport.ExportToPdf(memoryStream);
        }
        public static XtraReport CreateXtraReportInvoice(string orientimi, int idPerdoruesi, int idNderViti, int idNdermarrje, int idFatura, int idDesign, DateTime dateDergimiFature, int idGjuha, int idRaporti)
        {
            var report = new clsRaporti(idGjuha, idRaporti);
            var sqlparameters = report.CreateParameters(idNdermarrje, idFatura, idDesign, dateDergimiFature);
            var xtraReport = krijoDesignRaportFature(-1, idDesign, idPerdoruesi, idNdermarrje, idNderViti, null, orientimi, Guid.NewGuid().ToString(), string.Empty, idGjuha, report.IdRaporti);
            var storeProcedureName = clsSP.GetStoredProcedureName(report.IdSp);
            konfigDataSetRaporti(xtraReport, storeProcedureName, idPerdoruesi, false, idNdermarrje, idNderViti, DateTime.Today, 0, 0, false, sqlparameters);
            return xtraReport;
        }

        private static XtraReport KrijoRaportNgaDatatable(string orientimi, int idPerdoruesi, int idNdermarrje, int iddesgin, DataTable dt, int idGjuha, int idRaporti)
        {
            var report = krijoDesignRaportFature(-1, iddesgin, idPerdoruesi, idNdermarrje, 0, null, orientimi, "", "", idGjuha, idRaporti);
            var ds = new DataSet();
            ds.Tables.Add(dt);
            report.DataSource = ds;
            report.DataMember = ds.Tables[0].TableName;
            report.ExportOptions.Pdf.PageRange = "All";
            return report;
        }

        private static string ktheVlerenEParametritNgaQueryString(HttpRequest Request, System.Web.SessionState.HttpSessionState Session, int idRaporti, clsParameter parametri, int idNdermarrje, String guidString, int idDok, int idDesign, bool test, int idPerdoruesi)
        {
            //Ne rastin kur te dhenat i lexojme nga querystring-u (psh : FatureShitjes)
            if (Request.QueryString["Sesioni"] == "false")
            {
                if (parametri.Emri.ToLower() == "idperdoruesi")
                    return idPerdoruesi.ToString();

                if (parametri.Emri.ToLower() == "test")
                {
                    return test.ToString();
                }

                if (parametri.Emri.ToLower() == "idraport")
                {
                    return idRaporti.ToString();
                }

                if (parametri.Emri.ToLower() == "idraportdesign")
                {
                    return idDesign.ToString();
                }

                if (parametri.Emri.ToLower() == "idndermarje")
                {
                    return idNdermarrje.ToString();
                }

                if (parametri.Emri.ToLower() == "idndermarje")
                {
                    return idNdermarrje.ToString();
                }

                if (parametri.Emri.Equals("idfatura", StringComparison.InvariantCultureIgnoreCase)
                    || parametri.Emri.Equals("IDKOKAPLANIFIKIM", StringComparison.InvariantCultureIgnoreCase)
                    || parametri.Emri.Equals("IDKOKAURDHERPAGESA", StringComparison.InvariantCultureIgnoreCase)
                    || parametri.Emri.Equals("idfletemagazina", StringComparison.InvariantCultureIgnoreCase)
                    || parametri.Emri.Equals("idkokaekzekutim", StringComparison.InvariantCultureIgnoreCase)
                    || parametri.Emri.Equals("idkokaarkabanka", StringComparison.InvariantCultureIgnoreCase)
                    || parametri.Emri.Equals("idfletekont", StringComparison.InvariantCultureIgnoreCase)
                    || parametri.Emri.Equals("idveprimekf", StringComparison.InvariantCultureIgnoreCase))
                    return idDok.ToString();

                if (parametri.Emri.ToLower() == "filterllojdokumenti" && !string.IsNullOrEmpty(Request.QueryString["kodkonf"]))
                    return string.Format("{0} = ('{1}') and ", parametri.KolonaDb, Request.QueryString["kodkonf"]);

                if (parametri.Emri.ToLower() == "filternrdokkonvertuar")
                    return string.Format("{0} = ('{1}') and ", parametri.KolonaDb, Request.QueryString["kodfatura"]);

                if (parametri.Emri.ToLower() == "filterdtdokkonvertuar" && !string.IsNullOrEmpty(Request.QueryString["dtdok"]))
                    return string.Format("{0} = ('{1}') and ", parametri.KolonaDb, Request.QueryString["dtdok"]);

                if (parametri.Emri.ToLower() == "filteriddokkonvertuara")
                {
                    var idte = Request.QueryString["idDokKonv"];
                    idte = idte.Replace('-', ',');
                    return idte;
                }

                if (parametri.Emri.ToLower() == "filternumerdokumenti")
                {
                    var numrat = Request.QueryString["nrDok"];
                    if (string.IsNullOrEmpty(numrat))
                        return "";

                    numrat = $"'{numrat.Replace("^", "','")}'";
                    return $"{parametri.KolonaDb} in ({numrat}) AND";
                }

                if (parametri.Emri.ToLower() == "idfaturash")
                {
                    string numrat = Request.QueryString["idfaturash"];
                    if (string.IsNullOrEmpty(numrat))
                        return "";

                    numrat = $"'{numrat.Replace("^", "','")}'";
                    return $"{parametri.KolonaDb} in ({numrat}) AND";
                }

                if (parametri.Emri.ToLower() == "filteridklientfurnitor")
                {
                    string idKlient = Request.QueryString["idKlient"];
                    if (string.IsNullOrEmpty(idKlient))
                        return "";

                    idKlient = $"'{idKlient.Replace("^", "','")}'";
                    return $"{parametri.KolonaDb} in ({idKlient}) AND";
                }

                if (parametri.Emri.ToLower() == "filterartgjendjezero")
                {
                    return Request.QueryString["artGjendjeZero"];
                }

                if (parametri.Emri.ToLower() == "idpunonjes")
                {
                    return !string.IsNullOrEmpty(Request.QueryString["idPunonjes"]) ? Request.QueryString["idPunonjes"] : "0";
                }

                if (parametri.Emri.ToLower() == "salt")
                {
                    return System.Web.Configuration.WebConfigurationManager.AppSettings["salt"];
                }

                if (parametri.Emri.ToLower() == "idte")
                {
                    return Request.QueryString["idDokumenti"];
                }

                if (parametri.Emri == "NgaHistoriku")
                {
                    return (!string.IsNullOrEmpty(Request.QueryString["ngaHistoriku"]) && Convert.ToBoolean(Request.QueryString["ngaHistoriku"])) ? "1" : "0";
                }
            }
            else //Rasti kur te dhenat i lexojme nga sessioni
            {
                var emriReal = Request.QueryString["emriReal"];
                var idSubRaporti = string.IsNullOrEmpty(emriReal) ? ToInt32(Request.QueryString["idraporti"]) : clsRaporti.KtheIdRaportiSipasEmritReal(emriReal);
                var vleraParam = mySessionObjects.merrParametratSubRaportitNgaSesioni(Session, idSubRaporti, guidString);
                if (Request.QueryString[parametri.Emri] != null)
                    vleraParam[parametri.IdParametri] = NdertoVlereParam(parametri, Request.QueryString[parametri.Emri]);

                return vleraParam[parametri.IdParametri];
            }

            return "";
        }

        private static string NdertoVlereParam(clsParameter param, string vlera)
        {
            var vleraReturn = $"{param.KolonaDb} = ('{vlera}') ";
            vleraReturn = vleraReturn.Trim() != ""
                ? $"( {vleraReturn} ) AND "
                : vleraReturn.Trim();
            return vleraReturn;
        }

        //kjo metode eshte temporale sa per te shkeputur varesine e devexpress
        public static XtraReport mbushRaportNgaDB(HttpRequest request, System.Web.SessionState.HttpSessionState session, XtraReport xtraReport, int idNdermarrje, int idPerdorues, bool azhornim, int idnderviti, DateTime dtmbarimi, int idkonfig, int idperiudha, string guidString, int idDok, int idDesign, bool test, string emerRealRaport, clsRaporti report)
        {
            var parametraSp = report.ParametraSp;
            SqlParameter[] sqlparam;
            if (report.ParametraSp.Count > 0)
            {
                bool fatureLidhur = (!string.IsNullOrEmpty(request.QueryString["Lidhur"]) && Convert.ToInt32(request.QueryString["Lidhur"]) == 1);
                int nrParametrash = fatureLidhur ? (parametraSp.Count + 1) : parametraSp.Count;
                sqlparam = new SqlParameter[nrParametrash];
                for (int i = 0; i < parametraSp.Count; i++)
                {
                    //************************************************************************************//
                    //behet kontrolli nqs nuk eshte plotesuar filtri atehere do kalohet si bosh, pa vlere 
                    var vlera = ktheVlerenEParametritNgaQueryString(request, session, report.IdRaporti, parametraSp[i], idNdermarrje, guidString, idDok, idDesign, test, idPerdorues);
                    sqlparam[i] = new SqlParameter
                    {
                        ParameterName = parametraSp[i].Emri,
                        Value = vlera
                    };
                    if ((emerRealRaport == "RptKartelaLlogarive" || emerRealRaport == "KartelaLlogariveMeKunderparti") && parametraSp[i].Emri == "filterDtDok2")
                        dtmbarimi = ToDateTime(vlera);


                }
                if (fatureLidhur)
                {
                    sqlparam[nrParametrash - 1] = new SqlParameter
                    {
                        ParameterName = "lidhur",
                        Value = 1
                    };
                }
            }
            else
                sqlparam = new SqlParameter[0];

            mySessionObjects.ruajParametraRaporti(session, sqlparam, guidString);
            konfigDataSetRaporti(xtraReport, report.EmerSp, idPerdorues, azhornim, idNdermarrje, idnderviti, dtmbarimi, idkonfig, idperiudha, false, sqlparam);
            return xtraReport;
        }

        public static string GetExportedReportPath(int idPerdoruesi, int idGjuha, int idNdermarrje, int idNderViti, int idDok, int idDesign, int idRap)
        {
            XtraReport raporti;
            try
            {
                raporti = CreateXtraReportInvoice(clsRaportDesign._rap_portrait, idPerdoruesi, idNderViti, idNdermarrje, idDok, idDesign, DateTime.Now, idGjuha, idRap);
            }
            catch
            {
                throw new Exception("Ndodhi nje gabim gjate krijimit te formatit te raportit!");
            }

            string pathRaport = HttpContext.Current.Server.MapPath(null) + @"\RaportEmail\";
            DirectoryExtension.CreateDirIfNotExists(pathRaport);
            pathRaport += idNdermarrje + "\\";
            DirectoryExtension.CreateDirIfNotExists(pathRaport);
            pathRaport += "Dokumenti_" + DateTime.Now.ToFileTime() + ".pdf";
            raporti.ExportToPdf(pathRaport);

            return pathRaport;
        }

        public static void ExportXtraReportToPdf(int idPerdoruesi, int idGjuha, int idNdermarrje, int idNderViti, int idFatura, int idDesign, int idRap, MemoryStream pdfStream)
        {
            var raporti = CreateXtraReportInvoice(clsRaportDesign._rap_portrait, idPerdoruesi, idNderViti, idNdermarrje, idFatura, 0, DateTime.Now, idGjuha, idRap);
            raporti.ExportToPdf(pdfStream);
        }
    }
}