using System;
using DbCore.DbBuxheti;
using DbCore.IMBUtils.Messages;
using DbCore.DbAdmin;
using System.Data;
using DbCore;
using System.Web.SessionState;
using System.Web;
using DbCore.DbRegjistrim;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Linq;
using DbCore.DbShare;
using DbCore.DbKontabiliteti;
using DbCore.DbArkaBanka;
using DbCore.IMBUtils.Extensions;
using System.Net.Http;
using System.IO;
using AlphaWeb.Core.Interfaces.Localization;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using DbCore.DbInventari;
using DbCore.IMBUtils.Logging;

namespace RestApi.WebAPI.Models
{
    class BuxhetiRepository
    {
        private System.Web.SessionState.HttpSessionState Session { get { return HttpContext.Current.Session; } }
        internal static object KtheLlojeBuxhetiSipasIdKategoriBuxhetimi(int idKategoriBuxhetimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKategoriBuxhetimi);

            var colLlojeBuxheti = ColBLlojBuxheti.KtheSipasKategoriBuxhetimi(idKategoriBuxhetimi);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKategoriBuxhetimi);
            return new { LlojeBuxheti = colLlojeBuxheti };
        }

        internal static object KtheKategoriBuxhetimiSipasIdKategoriBuxhetimi(IMessagesResource messages, int idKategoriBuxhetimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKategoriBuxhetimi);

            var clsKategoriBuxhetimi = new ClsBKategoriBuxhetimi(messages, idKategoriBuxhetimi);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKategoriBuxhetimi);
            return new { clsKategoriBuxhetimi = clsKategoriBuxhetimi };
        }

        internal static object KtheDokumentBuxheti(int idKomponente, int idKonfigurim, int idNdermarrje, int idGjuha, string emerGride, int idKokaBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponente, idKonfigurim, idNdermarrje, idGjuha, emerGride, idKokaBuxheti);

            var kokaBuxheti = new ClsBKokaBuxheti();
            kokaBuxheti.MbushKokePaTrup(idKokaBuxheti);

            var dsTrupiBuxheti = MerrTrupBuxhetiSipasKokes(kokaBuxheti, idKomponente, idNdermarrje, idKonfigurim);

            var columnsKonfig = new colGridaTrupi(emerGride, idKomponente, idKonfigurim, idGjuha);

            var objekteKoke = merrObjekteKoke(kokaBuxheti);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponente, idKonfigurim, idNdermarrje, idGjuha, emerGride, idKokaBuxheti);
            return new { dsTrupiBuxheti = dsTrupiBuxheti, columnsKonfig = columnsKonfig, kokaBuxheti = kokaBuxheti, objekteKoke = objekteKoke };
        }

        internal static object GjeneroPlanifikimBuxheti(HttpSessionState Session, int idKomponente, int idKonfigurim, int idNdermarrje, int idPerdoruesi, int idKokaBuxheti)
        {
            try
            {
                ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponente, idKonfigurim, idNdermarrje, idPerdoruesi, idKokaBuxheti);

                ClsBKokaBuxheti kokaBuxheti = new ClsBKokaBuxheti();
                kokaBuxheti.MbushKokePaTrup(idKokaBuxheti);
                DataTable dsTrupiBuxheti = (DataTable)MerrTrupBuxhetiSipasKokes(kokaBuxheti, idKomponente, idNdermarrje, idKonfigurim);
                ColBKomponenteLidhje lidhjetAll = new ColBKomponenteLidhje();
                lidhjetAll.mbushAllSipasNdermarrjes(idNdermarrje);
                ColBKomponente komponentetAll = new ColBKomponente(idNdermarrje);
                ColBKomponenteVlere komponenteVlereAll = new ColBKomponenteVlere(idNdermarrje, idPerdoruesi, (int)EnumBNjesiKomponente.Tabelare);
                ClsBKomponenteLidhje lidhje = new ClsBKomponenteLidhje();
                ClsBKomponente komponente = new ClsBKomponente();

                ColBKomponenteVlere komponenteGjeneruar = new ColBKomponenteVlere();
                foreach (DataRow item in dsTrupiBuxheti.Rows)
                {

                    lidhje = lidhjetAll.Find(x => x.IdKategoriBuxhetimi == Convert.ToInt32(item["IdKategoriBuxhetimi"]));
                    if (lidhje == null)
                    {
                        item["VleraPlanifikuar"] = Convert.ToDecimal(0);
                        continue;
                    }

                    komponente = komponentetAll.Find(x => x.Id == lidhje.IdKomponente);
                    if (!komponente.Aktive)
                        continue;

                    var formula = ClsBKomponente.gjeneroKomponenteNgaFormula(komponente, komponente.Formula, Convert.ToInt32(item["IdNdermarrjeNiveli3"]), komponentetAll, komponenteVlereAll, ref komponenteGjeneruar, idPerdoruesi);
                    var vlera = ClsBKomponente.KtheSipasVlereMinMax(Convert.ToDecimal(formula), komponente.VleraMin, komponente.VleraMax, komponente.LlojKufizimi);

                    item["VleraPlanifikuar"] = Convert.ToDecimal(vlera);
                }
                mySessionObjects.RuajNeSession<ColBKomponenteVlere>(Session, komponenteGjeneruar, "komponenteTeGjeneruara");

                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponente, idKonfigurim, idNdermarrje, idPerdoruesi, idKokaBuxheti);
                return new { dsTrupiBuxheti = dsTrupiBuxheti, mesazh = new clsMesazh(true, "Gjenerimi u krye me sukses.") };
            }
            catch (Exception msg)
            {
                ImbLogger.LogErrorBuxhetimi(msg);
                return new { dsTrupiBuxheti = new ColBTrupiBuxheti() , mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, msg.Message) };
            }
        }

        private static object MerrTrupBuxhetiSipasKokes(ClsBKokaBuxheti kokaBuxheti, int idKomponente, int idNdermarrje, int idKonfigurim)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kokaBuxheti, idKomponente, idNdermarrje, idKonfigurim);

            var idKatDok = kokaBuxheti.IdKatDok > 0 ? kokaBuxheti.IdKatDok : clsKonfigurimAmbjenti.ktheIdKategori(idKonfigurim);
            object trupi;
            switch (idKatDok)
            {
                case 170:
                case 171:
                    using (var db = new ClsDatabaseBuxheti())
                        trupi = db.merrDatasourceTrupiMiratimPlanifikimBuxheti(idNdermarrje, kokaBuxheti.IdBuxhetiKoka, idKomponente);
                    break;
                case 172:
                    using (var db = new ClsDatabaseBuxheti())
                        trupi = db.merrDatasourceTrupiAlokimBuxheti(idNdermarrje, kokaBuxheti.IdBuxhetiKoka);
                    break;
                case 175:
                case 177:
                case 179:
                case 181:
                    using (var db = new ClsDatabaseBuxheti())
                        return db.merrDatasourceTrupiBuxheti(kokaBuxheti.IdBuxhetiKoka);
                default:
                    trupi = new ColBTrupiBuxheti();
                    break;
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kokaBuxheti, idKomponente, idNdermarrje, idKonfigurim);
            return trupi;
        }

        internal static HttpContent EksportoDokumentAlokimBuxheti(int idNdermarrje, bool eshtekonvertim, int idkokabuxheti, string data)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje, eshtekonvertim, idkokabuxheti, data);

            DataTable dt = new DataTable();
            using (ClsDatabaseBuxheti db = new ClsDatabaseBuxheti())
                dt = db.merrDataTableAlokimBuxhetiPerEksportAmbjenti(idkokabuxheti, idNdermarrje, eshtekonvertim, data);

            int idformati = clsKokaFormatImporti.ktheIdKokaSipasKoditdheKategorise(idNdermarrje, 172, "Format Standart Alokim Buxheti");
            DbCore.DbAdmin.colTrupiFormatImporti col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(idformati);

            HttpContent exportContext = EksportoDokumentPerExcel(col, dt, 172);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje, eshtekonvertim, idkokabuxheti, data);
            return exportContext;
        }

        internal static HttpContent EksportoDokumentMiratimBuxheti(int idkokabuxheti, int idNdermarrje, int idKomponente, string data, int idNiveli, string llojDok, string nrDok, decimal totaliFaktik)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idkokabuxheti, idNdermarrje, idKomponente, data, idNiveli, llojDok, nrDok, totaliFaktik);

            DataTable dt = new DataTable();
            using (ClsDatabaseBuxheti db = new ClsDatabaseBuxheti())
                dt = db.merrDataTableMiratimBuxhetiPerEksportAmbjenti(idkokabuxheti, idNdermarrje, idKomponente, data, idNiveli, llojDok, nrDok, totaliFaktik);

            int idformati = clsKokaFormatImporti.ktheIdKokaSipasKoditdheKategorise(idNdermarrje, 170, "Format Standart Miratim Buxheti");
            DbCore.DbAdmin.colTrupiFormatImporti col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(idformati);

            HttpContent exportContext = EksportoDokumentPerExcel(col, dt, 170);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idkokabuxheti, idNdermarrje, idKomponente, data, idNiveli, llojDok, nrDok, totaliFaktik);
            return exportContext;
        }

        internal static HttpContent EksportoDokumentPerExcel(colTrupiFormatImporti col, DataTable dt, int idKatDok)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, col, dt, idKatDok);

            foreach (DbCore.DbAdmin.clsTrupiFormatImporti trup in col)
                if (!(trup.Shfaq && trup.Visible))
                    dt.Columns.Remove(trup.KodKontrolli);

            using (var excelStream = new MemoryStream())
            using (ExcelPackage ep = new ExcelPackage(excelStream))
            {
                ExcelWorkbook eW = ep.Workbook;

                eW.Worksheets.Add("Lista");
                ExcelWorksheet sheet = eW.Worksheets[1];
                var colIndex = 1;
                sheet.InsertRow(1, dt.Rows.Count + 1);
                sheet.InsertColumn(1, dt.Columns.Count);
                foreach (DataColumn column in dt.Columns)
                {
                    sheet.Column(colIndex).Width = 30;
                    sheet.Cells[1, colIndex].Style.Border.BorderAround(new ExcelBorderStyle());
                    if (idKatDok == 172)
                    {
                        sheet.Cells[1, colIndex++].Value = column.ColumnName;
                        continue;
                    }
                    var colFormati = col.FirstOrDefault(x => x.KodKontrolli == column.ColumnName);
                    sheet.Cells[1, colIndex++].Value = colFormati.EmerImporti;
                }
                var rowIndex = 2;
                foreach (DataRow row in dt.Rows)
                {
                    colIndex = 1;
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ColumnName == "Date dokumenti")
                        {
                            sheet.Cells[rowIndex, colIndex].Style.Numberformat.Format = "dd/mm/yyyy";
                            sheet.Cells[rowIndex, colIndex++].Value = Convert.ToDateTime(row[column.ToString()]);
                        }
                        else
                            sheet.Cells[rowIndex, colIndex++].Value = row[column.ToString()];
                    }
                    rowIndex++;
                }
                var range = sheet.Cells[1, 1, rowIndex - 1, colIndex - 1];

                // add the excel table entity
                var table = sheet.Tables.Add(range, "table1");
                table.TableStyle = OfficeOpenXml.Table.TableStyles.Light8;
                table.ShowTotal = false;

                ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, col, dt, idKatDok);
                return new ByteArrayContent(ep.GetAsByteArray());
            }
        }
 
        internal static object KtheDokumentAlokimBuxheti(int idKomponente, int idKonfigurim, int idNdermarrje, int idGjuha, string emerGride, int idKokaAlokimi, string shtimModifikim, string llojKonvertimiNga)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponente, idKonfigurim, idNdermarrje, idGjuha, emerGride, idKokaAlokimi, shtimModifikim,  llojKonvertimiNga);

            var dsTrupiAlokimBuxheti = new object();
            var kokaBuxheti = new ClsBKokaBuxheti();

            if (shtimModifikim == "konvertim")
            {
                switch (llojKonvertimiNga)
                {
                    case "MB":
                        kokaBuxheti.MbushKokePaTrup(idKokaAlokimi);
                        kokaBuxheti.IdKokaKonvertimiNga = kokaBuxheti.IdBuxhetiKoka;
                        kokaBuxheti.IdBuxhetiKoka = 0;
                        kokaBuxheti.LlojKonfigKonvertimiNga = llojKonvertimiNga;

                        using (var db = new ClsDatabaseBuxheti())
                            dsTrupiAlokimBuxheti = db.merrDatasourceTrupiAlokimBuxhetiNgaKonvertimMiratimi(idKokaAlokimi);
                        break;
                }
            }
            else
            {
                kokaBuxheti.MbushKokePaTrup(idKokaAlokimi);
                dsTrupiAlokimBuxheti = MerrTrupBuxhetiSipasKokes(kokaBuxheti, idKomponente, idNdermarrje, idKonfigurim);
            }

            var columnsKonfig = new colGridaTrupi(emerGride, idKomponente, idKonfigurim, idGjuha);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponente, idKonfigurim, idNdermarrje, idGjuha, emerGride, idKokaAlokimi, shtimModifikim, llojKonvertimiNga);
            return new { dsTrupiAlokimBuxheti = dsTrupiAlokimBuxheti, columnsKonfig = columnsKonfig, kokaAlokimBuxheti = kokaBuxheti };
        }
        internal static object RuajDokumentBuxheti(HttpSessionState Session, string komponente, int idNdermarrje, int idPerdoruesi, int idViti, object kokaBuxheti, object trupiBuxheti, int idSkemeAprovimi, int statusAprovimi, int idEtapa)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, komponente, idNdermarrje, idPerdoruesi, idViti, kokaBuxheti, trupiBuxheti, idSkemeAprovimi, statusAprovimi, idEtapa);

            var clsKokaBuxheti = Newtonsoft.Json.JsonConvert.DeserializeObject<ClsBKokaBuxheti>(kokaBuxheti.ToString());
            var colTrupiBuxheti = Newtonsoft.Json.JsonConvert.DeserializeObject<ColBTrupiBuxheti>(trupiBuxheti.ToString());

            clsKokaBuxheti.DtDok = Convert.ToDateTime(clsKokaBuxheti.DtDok).ToLocalTime();
            clsKokaBuxheti.EtapeAprovimi = new clsEtapeAprovimi();
            clsKokaBuxheti.EtapeAprovimi.IdEtapa = idEtapa;  
            clsKokaBuxheti.EtapeAprovimi.IdSkema = idSkemeAprovimi;
            clsKokaBuxheti.EtapeAprovimi.StatusAprovimi = statusAprovimi;
            if ((StatusAprovimi)statusAprovimi == StatusAprovimi.Deleguar)
            {
                clsEtapeAprovimi etapa = new clsEtapeAprovimi(idEtapa, clsKokaBuxheti.IdKatDok);
                if (etapa.LlojAprovuesi == 2)
                {
                    colRolPerdorues col = new colRolPerdorues();
                    col.mbushRolePerdoruesSipasRoli(etapa.IdAprovuesi);
                    if (col.Count > 0)
                        idPerdoruesi = col[0].IdPerdorues;
                }
            }
            if (clsKokaBuxheti.IdBuxhetiKoka < 1 && (StatusAprovimi)statusAprovimi == StatusAprovimi.Undefined) // shtim dhe status aprovimi 0
                clsKokaBuxheti.EtapeAprovimi.IdSkema = 0;

            var teDrejtaInfo = merrTeDrejtaRoliPerDokument(clsKokaBuxheti, idPerdoruesi, idNdermarrje, idViti, komponente);

            var periudha = mySessionObjects.merrPeriudheKontabel(Session);
            string mesazhGabimi;
            if (!clsFunksione.checkPeriudheKontabel(out mesazhGabimi, (DateTime)clsKokaBuxheti.DtDok, periudha, 1))
                return new { mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, mesazhGabimi), idKokaBuxheti = clsKokaBuxheti.IdBuxhetiKoka };    

            var mesazh = new clsMesazh();
            clsKokaBuxheti.OColBTrupiBuxheti = colTrupiBuxheti;

            vendosKategoriBuxhetimiNeTrup(ref clsKokaBuxheti);

            if (clsKokaBuxheti.IdBuxhetiKoka > 0)
            {
                if(!((teDrejtaInfo.DMod && clsKokaBuxheti.IdStatusDok == 1) || (teDrejtaInfo.DModifikimDraft && clsKokaBuxheti.IdStatusDok == 0)))
                    return new { mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Ju nuk keni te drejta per kete veprim."), idKokaBuxheti = clsKokaBuxheti.IdBuxhetiKoka };
                mesazh = clsKokaBuxheti.Modifiko();
            }
            else
            {
                if (!((teDrejtaInfo.DShtim && clsKokaBuxheti.IdStatusDok == 1) || (teDrejtaInfo.DShtimDraft && clsKokaBuxheti.IdStatusDok == 0)) && 
                    clsNivelRegjistrimi.ktheKodNivelRegjistrimi(clsKokaBuxheti.IdNivel) != "IB")
                    return new { mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Ju nuk keni te drejta per kete veprim."), idKokaBuxheti = clsKokaBuxheti.IdBuxhetiKoka };
                mesazh = clsKokaBuxheti.Ruaj();
            }
            
            switch (clsKokaBuxheti.IdKatDok)
            {
                case 170:
                    if (mesazh.Status && clsKokaBuxheti.IdStatusDok == 1)
                    {
                        mesazh = PostoDokumentBuxheti(clsKokaBuxheti.IdBuxhetiKoka, idPerdoruesi, idNdermarrje, idViti, komponente, 0, 0);
                        if (!mesazh.Status)
                            return new { mesazh = new MesazhSuksesi("Dokumenti u ruajt, por nuk u postua. " + mesazh.PershkrimMesazhi), idKokaBuxheti = clsKokaBuxheti.IdBuxhetiKoka };
                    }
                    break;
                case 171:
                    if (mesazh.Status)
                    {
                        ColBKomponenteVlere komponenteTeGjeneruara = mySessionObjects.MerrNgaSession<ColBKomponenteVlere>(Session, "komponenteTeGjeneruara");
                        if (komponenteTeGjeneruara != null)
                        {
                            komponenteTeGjeneruara.ForEach(x => x.IdKoka = clsKokaBuxheti.IdBuxhetiKoka);
                            komponenteTeGjeneruara.FshiDheRiruaj(true);
                            mySessionObjects.hiqObjectNeSesion(Session, "komponenteTeGjeneruara");
                        }
                    }
                    break;
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, komponente, idNdermarrje, idPerdoruesi, idViti, kokaBuxheti, trupiBuxheti, idSkemeAprovimi, statusAprovimi, idEtapa);
            return new { mesazh = mesazh, idKokaBuxheti = clsKokaBuxheti.IdBuxhetiKoka };
        }

        /// <summary>
        /// Vendos ne rreshtat e trupit kategorite e buxhetimit kur nuk eshte zgjedhur nga perdoruesi. Vetem ne rastet kur lidhja midis kategorise dhe llogarise eshte 1 me 1
        /// </summary>
        /// <param name="clsKokaBuxheti">dokumenti i buxhetit</param>
        private static void vendosKategoriBuxhetimiNeTrup(ref ClsBKokaBuxheti clsKokaBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, clsKokaBuxheti);

            if (clsKokaBuxheti.OColBTrupiBuxheti.Count > 0 && (clsKokaBuxheti.IdKatDok == 179 || clsKokaBuxheti.IdKatDok == 181)) //clsKokaBuxheti.IdKatDok == 177 perfitimi
            {
                var kategoriBuxhetimi = ColBKategoriBuxhetimi.merrKategoriBuxhetimiAktiveSipasNdermarrjes(clsKokaBuxheti.OColBTrupiBuxheti[0].IdNdermarrje);
                int idLlogaria = 0;
                foreach (var rresht in clsKokaBuxheti.OColBTrupiBuxheti)
                {
                    if (rresht.IdKategoriBuxhetimi > 0)
                        continue;
                    idLlogaria = rresht.IdObjekti;
                    if (rresht.LlojObjekti == (int)llojRreshtiShitje.Artikull)
                    {
                        clsArtikulli artikulli = new clsArtikulli(rresht.IdObjekti);
                        idLlogaria = artikulli.LlojiArt ? artikulli.IdLlogariTeTrete : artikulli.IdLlogariBlerje;
                    }
                    var kategoriPerLlogari = kategoriBuxhetimi.FindAll(x => x.IdLlogaria == idLlogaria);
                    if (kategoriPerLlogari.Count == 1)
                        rresht.IdKategoriBuxhetimi = kategoriPerLlogari.First().IdKategoriBuxhetimi;
                }
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, clsKokaBuxheti);
        }

        internal static object FshiDokumentBuxheti(HttpSessionState session, int[] ids, string komponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, ids, komponente);

            CultureInfo cultinf = mySessionObjects.ktheCultureInfo(session);
            ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            List<ClsBKokaBuxheti> teFshire = new List<ClsBKokaBuxheti>();
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(session);
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(session);
            int idViti = mySessionObjects.ktheIdVitNdermarrje(session);

            DataTable tabGabimesh = new DataTable();
            tabGabimesh.Columns.Add("Kodi");
            tabGabimesh.Columns.Add("Gabimi");
            tabGabimesh.Columns.Add("Rreshti");

            Dictionary<string, List<string>> paTeDrejta = new Dictionary<string, List<string>>();
            clsTeDrejtaRoli teDrejtaInfo = new clsTeDrejtaRoli();
            colNivelRegjistrimi niveleRregjistrimi = new colNivelRegjistrimi();
            string planifikim_miratim = HttpUtility.ParseQueryString(komponente)[0];
            int i = 0;
            clsMesazh mesazh = new MesazhSuksesi(MessagesResource.Messages["msgFshirjeMeSukses"]);
            foreach (int id in ids)
            {
                i++;
                ClsBKokaBuxheti koka_B = new ClsBKokaBuxheti();
                koka_B.MbushKokePaTrup(Convert.ToInt32(id));
                var idNiveli = 0;
                switch (koka_B.IdKatDok)
                {
                    case 175:
                    case 177:
                    case 179:
                    case 181:
                        idNiveli = koka_B.IdNivel;
                        break;
                    default:
                        idNiveli = 0;
                        break;
                }
                if (!clsFunksione.kaTeDrejtePerVepriminMeDokumentin(idNiveli, koka_B.NrDok, teDrejtaInfo, ref niveleRregjistrimi, koka_B.IdKatDok, "DFsh", idNdermarrje, idPerdoruesi, idViti, ref paTeDrejta, komponente.Split('&')[0]))
                {
                    mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, clsFunksione.ktheMesazhPerTeDrejtatNivelRregjistrimi(paTeDrejta, "Fshi", rm, cultinf));
                    tabGabimesh.AddRow(koka_B.NrDok, $"Date Dokumenti {((DateTime)koka_B.DtDok).ToShortDateString()} - {mesazh.PershkrimMesazhi}", i);
                    continue;
                }


                if (koka_B.IdStatusDok == 2)
                {
                    tabGabimesh.AddRow(koka_B.NrDok, $"Date Dokumenti {((DateTime)koka_B.DtDok).ToShortDateString()} - Dokumenti eshte fshire njehere!", i);
                    mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Dokumenti eshte fshire njehere!");
                    continue;
                }

                if (((koka_B.IdStatusDok == 1 || koka_B.IdStatusDok == 9 || koka_B.IdStatusDok == 8) && (koka_B.IdKatDok == 170 || koka_B.IdKatDok == 172 || koka_B.IdKatDok == 175 || koka_B.IdKatDok == 179)))
                {
                    tabGabimesh.AddRow(koka_B.NrDok, $"Date Dokumenti {((DateTime)koka_B.DtDok).ToShortDateString()} - Dokumenti nuk eshte me status Draft dhe nuk mund te fshihet!", i);
                    mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Dokumenti nuk eshte me status Draft dhe nuk mund te fshihet!");
                    continue;
                }

                if(koka_B.IdKatDok == 170 || koka_B.IdKatDok == 171)
                {
                    mesazh = ClsBKokaBuxheti.EshteKonvertuarDokMPBuxhetiMeId(id);
                    if (!mesazh.Status)
                    {
                        tabGabimesh.AddRow(koka_B.NrDok, $"Date Dokumenti {((DateTime)koka_B.DtDok).ToShortDateString()} - {mesazh.PershkrimMesazhi}", i);
                        continue;
                    }
                }
                    

                koka_B.IdPerdoruesi = idPerdoruesi;

                mesazh = koka_B.Fshi();
                tabGabimesh.AddRow(koka_B.NrDok, $"Date Dokumenti {((DateTime)koka_B.DtDok).ToShortDateString()} - {mesazh.PershkrimMesazhi}", i);
                if (!mesazh.Status)
                    continue;

                mySessionObjects.RemoveGridRowsInSessionById<ClsBKokaBuxheti>(komponente, "IdBuxhetiKoka", null, x => x.IdBuxhetiKoka == koka_B.IdBuxhetiKoka);
                teFshire.Add(koka_B);
            }

            if(ids.Count() > 1)
                mySessionObjects.ruajTabeleGabimeshImporti(session, tabGabimesh);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, ids, komponente);
            return new
            {
                mesazhGabim = mesazh ? string.Empty : mesazh.PershkrimMesazhi,
                mesazhSukses = mesazh ? mesazh.PershkrimMesazhi : string.Empty,
                deletedKeys = teFshire.Select(ks => ks.IdBuxhetiKoka).ToArray(),
                keysCount = ids.Count()
            };
        }

        internal static object NdryshoStatusDokumenti(int[] idDokumentash, int idStatusDok, int idPerdoruesi, int idNdermarrje, int idViti, string komponente, string shenime, HttpSessionState Session)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idDokumentash, idStatusDok, idPerdoruesi, idNdermarrje, idViti, komponente, shenime);

            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            string suksese = "", gabime = "";
            List<string> paTeDrejta = new List<string>();
            List<string> poModifikuar = new List<string>();
            List<string> joModifikuar = new List<string>();
            List<string> poRefuzuar = new List<string>();
            List<string> joRefuzuar = new List<string>();

            foreach (int id in idDokumentash)
            {
                ClsBKokaBuxheti koka_B = new ClsBKokaBuxheti(id);
                clsTeDrejtaRoli teDrejtaInfo = new clsTeDrejtaRoli();
                teDrejtaInfo = merrTeDrejtaRoliPerDokument(koka_B, idPerdoruesi, idNdermarrje, idViti, komponente);

                koka_B.IdStatusDok = idStatusDok;
                koka_B.IdPerdoruesi = idPerdoruesi;
                if (komponente.Contains("B_Shto_Regjistrim"))
                    koka_B.Shenime = shenime;

                if (!((teDrejtaInfo.DMod && koka_B.IdStatusDok == 1) || (teDrejtaInfo.DModifikimDraft && koka_B.IdStatusDok == 0) || (teDrejtaInfo.DPezullo && koka_B.IdStatusDok == 8)))
                {
                    paTeDrejta.Add(koka_B.NrDok);
                }
                koka_B.IdNdermVit = mySessionObjects.ktheNdermarrjeVit(Session);
                clsMesazh mesazh = koka_B.Modifiko();
                if (mesazh)
                {
                    if (idStatusDok == 8)
                        poRefuzuar.Add(koka_B.NrDok);
                    else
                        poModifikuar.Add(koka_B.NrDok);
                }
                else
                {
                    if (idStatusDok == 8)
                        joRefuzuar.Add("Dok. " + koka_B.NrDok + " - " + mesazh.PershkrimMesazhi);
                    else
                        joModifikuar.Add("Dok. " + koka_B.NrDok + " - " + mesazh.PershkrimMesazhi);
                }
            }
            if (paTeDrejta.Any())
            {
                gabime = rm.GetString("msgPaTeDrejtaPerVeprimin", ci);
                gabime = gabime.Replace("#nrDocs#", string.Join(", ", paTeDrejta.ToArray()));
                gabime += "<br>";
            }
            if (joModifikuar.Any())
            {
                gabime += string.Join("<br>",joModifikuar.ToArray());
            }
            if (joRefuzuar.Any())
            {
                gabime += string.Join("<br>", joRefuzuar.ToArray());
            }
            if (poModifikuar.Any())
            {
                suksese = rm.GetString("msgModifikimiuKryemeSukses", ci);
                suksese = suksese.Replace("#nrDocs#", string.Join(", ", poModifikuar.ToArray()));
                suksese += "<br>";
            }
            if (poRefuzuar.Any())
            {
                suksese += rm.GetString("msgRefuzimiuRuajtmeSukses", ci);
                suksese = suksese.Replace("#nrDocs#", string.Join(", ", poRefuzuar.ToArray()));
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idDokumentash, idStatusDok, idPerdoruesi, idNdermarrje, idViti, komponente, shenime);
            return new { mesazhSukses = suksese, mesazhGabim = gabime };
        }

        internal static object KtheLlogariDhePrindSipasIds(int idPrindi, int idLlogari)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idPrindi, idLlogari);

            clsLlogari llogaria = new clsLlogari();
            ClsBKategoriBuxhetimi prindi = new ClsBKategoriBuxhetimi();
            if (idLlogari > 0)
                llogaria = new clsLlogari(idLlogari);
            if (idPrindi > 0)
                prindi = new ClsBKategoriBuxhetimi(MessagesResource.Messages, idPrindi);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idPrindi, idLlogari);
            return new { prindi = prindi, llogaria = llogaria };
        }

        internal static object PostoDokumentMiratimPlanifikimBuxheti(int idKokaBuxheti, int idPerdoruesi, int idNdermarrje, int idViti, string komponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKokaBuxheti, idPerdoruesi, idNdermarrje, idViti, komponente);

            var mesazh = PostoDokumentBuxheti(idKokaBuxheti, idPerdoruesi, idNdermarrje, idViti, komponente, 0, 0);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKokaBuxheti, idPerdoruesi, idNdermarrje, idViti, komponente);
            return new { mesazh = mesazh };
        }

        private static clsMesazh PostoDokumentBuxheti(int idKokaBuxheti, int idPerdoruesi, int idNdermarrje, int idViti, string komponente, int idSkemeAprovimi, int statusAprovimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKokaBuxheti, idPerdoruesi, idNdermarrje, idViti, komponente, idSkemeAprovimi, statusAprovimi);

            var clsKokaBuxheti = new ClsBKokaBuxheti();
            clsKokaBuxheti.MbushKokePaTrup(idKokaBuxheti);

            string kodNiveli = clsNivelRegjistrimi.ktheKodNivelRegjistrimi(clsKokaBuxheti.IdNivel);
            string kodKonfigAmbjente = new clsKonfigurimAmbjenti(clsKokaBuxheti.IdKonfigAmbjente).KodKonfigAmbjente;
            clsNdermarrje nd = new clsNdermarrje(clsKokaBuxheti.IdNderm);
            if (statusAprovimi == 1 && kodNiveli == "PIB" && nd.Nivelstrukture != 1)
                idSkemeAprovimi = clsKusht.kthevlereSipasKushtitDheIdKonfig(clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod(kodKonfigAmbjente, nd.Raportuesi), "ZSP");

            clsKokaBuxheti.EtapeAprovimi = new clsEtapeAprovimi();
            clsKokaBuxheti.EtapeAprovimi.IdSkema = idSkemeAprovimi;
            clsKokaBuxheti.EtapeAprovimi.StatusAprovimi = statusAprovimi;

            var teDrejtaInfo = merrTeDrejtaRoliPerDokument(clsKokaBuxheti, idPerdoruesi, idNdermarrje, idViti, komponente);
            
            if (!teDrejtaInfo.DAutoKonverto)
                return new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, "Ju nuk keni te drejta per te postuar.");
            var mesazh = clsKokaBuxheti.PostoDokumentBuxheti(idPerdoruesi);
            if (mesazh)
                switch (clsKokaBuxheti.IdKatDok)
                {
                    case 170:
                        EmailComposer.DergoEmailNjoftuesPerMiratimBuxheti(clsKokaBuxheti.IdNderm, clsKokaBuxheti.Viti, idPerdoruesi);
                        break;
                    case 172:
                        EmailComposer.DergoEmailNjoftuesPerAlokimBuxheti(idPerdoruesi, clsKokaBuxheti.IdNderm, nd.Raportuesi, clsKokaBuxheti.Viti, nd.NdermarrjeKodi);
                    break;
                    case 175:
                        EmailComposer.DergoEmailKerkeseRialokimBuxheti(idPerdoruesi, clsKokaBuxheti.IdNderm, nd.Raportuesi, clsKokaBuxheti.DtDok, clsKokaBuxheti.IdKonfigAmbjente);
                        break;
                    case 179:
                        switch (kodNiveli)
                        {
                            case "PIB":
                                EmailComposer.DergoEmailKerkesePlanifikimInvestimBuxheti(idPerdoruesi, clsKokaBuxheti.IdNderm, nd.Raportuesi, clsKokaBuxheti.DtDok, clsKokaBuxheti.IdKonfigAmbjente);
                                break;
                            case "PEB":
                                EmailComposer.DergoEmailPlanifikimEkzekutimBuxheti(idPerdoruesi, clsKokaBuxheti.IdNderm, nd.Raportuesi, clsKokaBuxheti.Viti, clsKokaBuxheti.IdStatusDok, nd.NdermarrjeKodi);
                                break;
                        }
                        break;
                }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKokaBuxheti, idPerdoruesi, idNdermarrje, idViti, komponente, idSkemeAprovimi, statusAprovimi);
            return mesazh;
        }

        internal static object PostoDokumentAlokimBuxheti(int idKokaAlokimi, int idPerdoruesi, int idNdermarrje, int idViti, string komponente, int idSkemeAprovimi, int statusAprovimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKokaAlokimi, idPerdoruesi, idNdermarrje, idViti, komponente, idSkemeAprovimi, statusAprovimi);

            var mesazh = PostoDokumentBuxheti(idKokaAlokimi, idPerdoruesi, idNdermarrje, idViti, komponente, idSkemeAprovimi, statusAprovimi);
            if (mesazh)
            {
                Predicate<ClsBKokaBuxheti> predicate = x => x.IdBuxhetiKoka == idKokaAlokimi;
                mySessionObjects.RemoveGridRowsInSessionById<ClsBKokaBuxheti>(komponente, "IdBuxhetiKoka", null, predicate);
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKokaAlokimi, idPerdoruesi, idNdermarrje, idViti, komponente, idSkemeAprovimi, statusAprovimi);
            return new
            {
                mesazh = mesazh,
                idPostuar = idKokaAlokimi
            };
        }

        public static object KontrolloKonvertuar(int id, int idKonfigKonvertimiNga, int idNdermarrje, int idPerdoruesi, int idGjuha)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, id, idKonfigKonvertimiNga, idNdermarrje, idPerdoruesi, idGjuha);

            var niv = new colNivelRegjistrimi();
            var colKonfig = new colKonfigurimAmbjenti();
            var konfigurim = new clsKonfigurimAmbjenti(idKonfigKonvertimiNga);

            if (id <= 0)
                return new
                {
                    nivelet = niv, mesazh = String.Empty, id = id, colKonfig = colKonfig, llojKonvertimiNga = konfigurim.KodKonfigAmbjente
                };

            string mesazh = "";
            var clsMesazh = new clsMesazh();

            #region kontrollKonvertimi Planifikimi
            var kokaBuxheti = new ClsBKokaBuxheti();
            kokaBuxheti.MbushKokePaTrup(id);
            if (kokaBuxheti.IdStatusDok != 1)
                return new
                {
                    nivelet = niv, mesazh = "Dokumenti nuk mund te konvertohet pasi nuk eshte me status Ruajtur.", id = id, colKonfig = colKonfig, llojKonvertimiNga = konfigurim.KodKonfigAmbjente
                };
            switch (kokaBuxheti.IdKatDok)
            {
                case 170:
                case 171:
                    clsMesazh = ClsBKokaBuxheti.EshteKonvertuarDokMPBuxhetiMeId(id);
                    break;
                case 179:
                    clsMesazh = ClsBKokaBuxheti.EshteKonvertuarPlotesishtDokBuxhetiMeId(id);
                    break;

            }
            if (!clsMesazh.Status)
                return new
                {
                    nivelet = niv, mesazh = clsMesazh.PershkrimMesazhi, id = id, colKonfig = colKonfig, llojKonvertimiNga = konfigurim.KodKonfigAmbjente
                };
            #endregion

            int idniveli = konfigurim.IdNivel;

            niv.mbushKonvertimeNiveli(idniveli, idPerdoruesi);
            if (niv.Count == 0)
                return new
                {
                    nivelet = niv, mesazh = "Dokumentet nuk mund te konvertohen!", id = id, colKonfig = colKonfig, llojKonvertimiNga = konfigurim.KodKonfigAmbjente
                };

            foreach (var regj in niv)
            {
                var konf = new clsKonfigurimAmbjenti();
                colKonfig.mbushKonfigAmbjSipasIdKategoriIdNivel(regj.IdKategori, regj.IdNivel, idPerdoruesi, idGjuha, false);
            }

            if (mesazh == "")
                mesazh = "Nuk jane konvertuar";

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, id, idKonfigKonvertimiNga, idNdermarrje, idPerdoruesi, idGjuha);
            return new
            {
                nivelet = niv, mesazh = mesazh, id = id, colKonfig = colKonfig, llojKonvertimiNga = konfigurim.KodKonfigAmbjente
            };
        }

        internal static clsNdermarrje KtheNdermarrjeSipasId(int idNdermarrje)
        {
            return new clsNdermarrje(idNdermarrje);
        }

        public static AutoCompleteItem[] KtheACListeLlojeBuxheti(string infixText, int idNdermarrje)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, infixText, idNdermarrje);

            DataTable tmpTable = ColBLlojBuxheti.merrLlojeBuxhetiLikeKodOsePershkDT(idNdermarrje, infixText);
            AutoCompleteItem[] autoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                autoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                autoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                autoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, infixText, idNdermarrje);
            return autoCompleteItem;
        }
        public static AutoCompleteItem[] KtheACListeLlojeBuxhetiSipasLlogarise(string infixText, int idNdermarrje, int idLlogaria)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, infixText, idNdermarrje, idLlogaria);

            DataTable tmpTable = ColBLlojBuxheti.merrLlojeBuxhetiLikeKodOsePershkDTSipasLlogarise(idNdermarrje, idLlogaria, infixText);
            AutoCompleteItem[] autoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                autoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                autoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                autoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, infixText, idNdermarrje, idLlogaria);
            return autoCompleteItem;
        }

        public static AutoCompleteItem[] KtheACListeKategoriBuxhetimi(string infixText, int idNdermarrje)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, infixText, idNdermarrje);

            DataTable tmpTable = ColBKategoriBuxhetimi.merrKategoriBuxhetimiLikeKodOsePershkDT(idNdermarrje, infixText);
            AutoCompleteItem[] autoCompleteItem = new AutoCompleteItem[tmpTable.Rows.Count];
            for (int i = 0; i < tmpTable.Rows.Count; i++)
            {
                autoCompleteItem[i].label = tmpTable.Rows[i]["label"].ToString();
                autoCompleteItem[i].value = tmpTable.Rows[i]["value"].ToString();
                autoCompleteItem[i].desc = tmpTable.Rows[i]["desc"].ToString();
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, infixText, idNdermarrje);
            return autoCompleteItem;
        }

        internal static colTaksa ktheListeTaksashSipasNdermarrjes(int idNdermarrje, int idPerdoruesi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idPerdoruesi);

            var taksat = new colTaksa(idNdermarrje, LlojTakse.Nivel_Tvsh, idPerdoruesi);
            clsTaksa takseDef = new clsTaksa();
            takseDef.IdTaksa = clsTaksa.idTaksaPaTVSH2;
            takseDef.KodTaksa = clsTaksa.kodTaksaPaTVSH;
            takseDef.NormaPerqindje = 0;
            taksat.Add(takseDef);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idPerdoruesi);
            return taksat;
        }

        private static object merrObjekteKoke(ClsBKokaBuxheti objBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, objBuxheti);

            var entiteti = new object();
            int idNivelCmimi = 0;
            object doktori = new object();
            if (objBuxheti.IdEntiteti > 0)
            {
                switch (objBuxheti.LlojVeprimiGjenerimi)
                {
                    case (int)EnumBLlojeVeprimi.Shitje:
                    case (int)EnumBLlojeVeprimi.Blerje:
                        var klientFurnitori = new clsKlientFurnitor(objBuxheti.IdEntiteti);
                        entiteti = new { value = klientFurnitori.IdKlientFurnitor, text = klientFurnitori.KodKlientFurnitor };
                        idNivelCmimi = klientFurnitori.IdNivelCmimi;
                        break;
                    case (int)EnumBLlojeVeprimi.Arketim:
                    case (int)EnumBLlojeVeprimi.Pagese:
                        var arkaBanka = new clsBanka(objBuxheti.IdEntiteti);
                        entiteti = new { value = arkaBanka.IdBanka, text = arkaBanka.KodiBanka };
                        break;
                }
            }
            if (objBuxheti.Doktori > 0)
            {
                DataRow auto = clsAutomjete.ktheAutomjetSipasId(objBuxheti.Doktori);
                doktori = new { value = auto[0], text = auto[1] };
            }
            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, objBuxheti);
            return new { entiteti = entiteti, idNivelCmimi = idNivelCmimi, doktori = doktori };
        }

        private static clsTeDrejtaRoli merrTeDrejtaRoliPerDokument(ClsBKokaBuxheti clsKokaBuxheti, int idPerdoruesi, int idNdermarrje, int idViti, string komponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, clsKokaBuxheti, idPerdoruesi, idNdermarrje, idViti, komponente);

            var teDrejtaInfo = new clsTeDrejtaRoli();
            switch (clsKokaBuxheti.IdKatDok)
            {
                case 175:
                case 177:
                case 179:
                case 181:
                    teDrejtaInfo.merrTeDrejtaPerKeteKomponenteDheNivelRegjistrimi(idPerdoruesi, idNdermarrje, idViti, komponente, clsKokaBuxheti.IdNivel);
                    break;
                default:
                    teDrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
                    break;
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, clsKokaBuxheti, idPerdoruesi, idNdermarrje, idViti, komponente);
            return teDrejtaInfo;
        }

        internal static object KtheDokumentBuxhetiNgaKonvertimi(int idKomponente, int idKonfigurim, int idNdermarrje, int idGjuha, string emerGride, int idKokaBuxheti, string llojKonvertimiNga)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponente, idKonfigurim, idNdermarrje, idGjuha, emerGride, idKokaBuxheti, llojKonvertimiNga);

            var kokaBuxheti = new ClsBKokaBuxheti();
            var dsTrupiBuxheti = new object();
            kokaBuxheti.MbushKokePaTrup(idKokaBuxheti);
            switch (llojKonvertimiNga)
            {
                case "PEB":
                case "PIB":
                    kokaBuxheti.IdKokaKonvertimiNga = kokaBuxheti.IdBuxhetiKoka;
                    kokaBuxheti.IdBuxhetiKoka = 0;
                    kokaBuxheti.LlojKonfigKonvertimiNga = llojKonvertimiNga;

                    using (var db = new ClsDatabaseBuxheti())
                        dsTrupiBuxheti = db.merrTrupBuxhetiPerKonvertim(idKokaBuxheti, llojKonvertimiNga);
                    break;
            }

            var columnsKonfig = new colGridaTrupi(emerGride, idKomponente, idKonfigurim, idGjuha);

            var objekteKoke = merrObjekteKoke(kokaBuxheti);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponente, idKonfigurim, idNdermarrje, idGjuha, emerGride, idKokaBuxheti, llojKonvertimiNga);
            return new { dsTrupiBuxheti = dsTrupiBuxheti, columnsKonfig = columnsKonfig, kokaBuxheti = kokaBuxheti, objekteKoke = objekteKoke };
        }

        internal static object KtheKomponenteBuxhetiMeId(int idKomponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponente);

            var komponente = new ClsBKomponente(idKomponente);
            var buxhetiLidhur = new ClsBLlojBuxheti(MessagesResource.Messages, komponente.IdBuxheti);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponente);
            return new { komponente = komponente, buxhetiLidhur = buxhetiLidhur };
        }

        internal static object RuajKomponenteBuxheti(object komponenteBuxheti, object komponenteBuxhetiLidhje, string komponente, int idPerdoruesi, int idNdermarrje, int idViti, HttpSessionState Session)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, komponenteBuxheti, komponenteBuxhetiLidhje, komponente, idPerdoruesi, idNdermarrje, idViti);

            var mesazh = new clsMesazh();
            var clsKomponenteBuxheti = Newtonsoft.Json.JsonConvert.DeserializeObject<ClsBKomponente>(komponenteBuxheti.ToString());
            var colKomponenteLidhje = Newtonsoft.Json.JsonConvert.DeserializeObject<ColBKomponenteLidhje>(komponenteBuxhetiLidhje.ToString());
            clsKomponenteBuxheti.LidhjeKomponente = colKomponenteLidhje;
            clsKomponenteBuxheti.Formula = new string(clsKomponenteBuxheti.Formula.Where(c => !char.IsWhiteSpace(c)).ToArray());

            clsTeDrejtaRoli teDrejtaInfo = new clsTeDrejtaRoli();
            teDrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);

            if (clsKomponenteBuxheti.Id > 0)
            {
                if (!teDrejtaInfo.DMod)
                    return new { mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["msgAdministrimiNukKeniTeDrejteVeprimi"])};
                mesazh = clsKomponenteBuxheti.Modifiko();
            }
            else
            {
                if (!teDrejtaInfo.DShtim)
                    return new { mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["msgAdministrimiNukKeniTeDrejteVeprimi"])};
                mesazh = clsKomponenteBuxheti.Ruaj();
            }
            if (mesazh)
            {
                mySessionObjects.RemoveGridRowsInSessionById<ClsBKomponente>("gvKomponenteBuxheti", "Id", null, x => x.Id == clsKomponenteBuxheti.Id);
                var sessionList = mySessionObjects.MerrNgaSession<ColBKomponente>(Session, "gvKomponenteBuxheti");
                sessionList.Add(clsKomponenteBuxheti);
                mySessionObjects.RuajNeSession<ColBKomponente>(Session, sessionList, "gvKomponenteBuxheti");
            }

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, komponenteBuxheti, komponenteBuxhetiLidhje, komponente, idPerdoruesi, idNdermarrje, idViti);
            return new { mesazh = mesazh };
        }
        internal static object FshiKomponenteBuxheti(int[] idKomponenteBuxheti, string komponente, int idPerdoruesi, int idNdermarrje, int idViti, HttpSessionState Session)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponenteBuxheti, komponente, idPerdoruesi, idNdermarrje, idViti);

            clsTeDrejtaRoli teDrejtaInfo = new clsTeDrejtaRoli();
            teDrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
            if (!teDrejtaInfo.DFsh)
                return new { mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["msgAdministrimiNukKeniTeDrejteVeprimi"]) };

            var idTeFshira = new List<int>();
            var tePafshira = new List<string>();
            var teFshira = new List<string>();
            var tePerdoruraNeFormule = new List<string>();
            var mesazh = new clsMesazh();
            foreach (var id in idKomponenteBuxheti)
            {
                var komponenteEkzistuese = new ClsBKomponente(id);

                mesazh = komponenteEkzistuese.Fshi();
                if (!mesazh)
                {
                    tePafshira.Add(komponenteEkzistuese.Kodi);
                    continue;
                }
                idTeFshira.Add(id);
                teFshira.Add(komponenteEkzistuese.Kodi);
                mySessionObjects.RemoveGridRowsInSessionById<ClsBKomponente>("gvKomponenteBuxheti", "Id", null, x => x.Id == id);
            }

            if(idKomponenteBuxheti.Length == 1)
                return new { mesazh = mesazh };

            if (teFshira.Count == 0 && tePafshira.Count > 1)
                mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["msgKomponenteMeKodShumes"] + String.Join(",", tePafshira) + MessagesResource.Messages["msgKomponenteNukMundTeFshihen"]);
            else
                if(teFshira.Count > 1 && tePafshira.Count > 1)
                mesazh = new clsMesazh(true, MessagesResource.Messages["msgKomponenteMeKodShumes"] + String.Join(",", teFshira)  + MessagesResource.Messages["msgKomponentetFshi"] + MessagesResource.Messages["msgKomponentePjsz"] + MessagesResource.Messages["msgKomponenteMeKodShumesLower"] + String.Join(",", tePafshira) + MessagesResource.Messages["msgKomponenteNukMundTeFshihen"]);
            else
                mesazh = new clsMesazh(true, MessagesResource.Messages["msgKomponenteMeKodShumes"] + String.Join(",", teFshira) + MessagesResource.Messages["msgKomponentetFshi"]);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponenteBuxheti, komponente, idPerdoruesi, idNdermarrje, idViti);
            return new { mesazh = mesazh };
        }

        internal static object KtheKategoriPerLidhjeSipasBuxhetit(int idKomponenteBuxheti, int idLlojBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponenteBuxheti, idLlojBuxheti);

            var komponenteLidhje = new ColBKomponenteLidhje(idKomponenteBuxheti);
            var komponenteLidhjeAllTePalidhura = new ColBKomponenteLidhje();
            komponenteLidhjeAllTePalidhura.mbushAllTePalidhuraSipasBuxhetit(idKomponenteBuxheti, idLlojBuxheti);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponenteBuxheti, idLlojBuxheti);
            return new { komponenteLidhje = komponenteLidhje.Concat(komponenteLidhjeAllTePalidhura) };
        }

        internal static object KtheKomponenteBuxhetiVlera(int idNdermarrje, int idPerdoruesi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idPerdoruesi);

            var dataSource = new ColBKomponenteVlere(idNdermarrje, idPerdoruesi, (int)EnumBNjesiKomponente.Tabelare);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idPerdoruesi);
            return new { dataSource = dataSource };
        }

        internal static object RuajKomponenteBuxhetiVlera(object komponenteBuxhetiVlere, string komponente, int idPerdoruesi, int idNdermarrje, int idViti, HttpSessionState Session)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, komponenteBuxhetiVlere, komponente, idPerdoruesi, idNdermarrje, idViti);

            clsTeDrejtaRoli teDrejtaInfo = new clsTeDrejtaRoli();
            teDrejtaInfo.merrTeDrejtaPerKeteKomponente(idPerdoruesi, idNdermarrje, idViti, komponente);
            if(!teDrejtaInfo.DShtim)
                return new { mesazh = new MesazhGabimi(ImbLogger.LogWarningBuxhetimi, MessagesResource.Messages["msgAdministrimiNukKeniTeDrejteVeprimi"]) };

            var dataSource = Newtonsoft.Json.JsonConvert.DeserializeObject<ColBKomponenteVlere>(komponenteBuxhetiVlere.ToString());
            var mesazh = dataSource.Ruaj();

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, komponenteBuxhetiVlere, komponente, idPerdoruesi, idNdermarrje, idViti);
            return new { mesazh = mesazh };
        }
    }
}
