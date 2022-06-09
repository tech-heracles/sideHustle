using DbCore;
using DbCore.DbAdmin;
using DbCore.DbArkaBanka;
using DbCore.DbImporte;
using DbCore.DbProdhimi;
using DbCore.DbRegjistrim;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Messages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Web.SessionState;
using Newtonsoft.Json;
using DbCore.IMBUtils.Types;
using System.Data.OleDb;
using DbCore.IMBUtils;
using System.Web;
using NLog;
using System.IO;
using System.Collections;
using System.Globalization;
using AlphaWeb.Core.SharedKernel;
using static DbCore.DbImporte.clsKonfigImporti;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace RestApi.WebAPI.Models
{
    class ImportiRepository
    {
        /// <summary>
        /// Funksion qe kthen datasource-n me te dhenat per import, kolonat per konfigurimin e grides se importit dhe mesazhin
        /// </summary>
        /// <param name="idKategoria">Id e kategorise per te cilen po kryhet importi</param>
        /// <param name="kategoria">Kodi i kategorise per te cilen po kryhet importi</param>
        /// <param name="formati">Id e formatit i importit</param>
        /// <param name="lloji">Lloji i importit (xls,xlsx,csv,sql)</param>
        /// <param name="emerTabeleKoke">Emri i tabeles se kokes</param>
        /// <param name="emerTabeleTrupi">Emri i tabeles se trupit</param>
        /// <param name="emerTabeleRec">Emri i tabeles se recepturave</param>
        /// <param name="transferoFatura">Transfero Fatura</param>
        /// <param name="tePaImportuara">Merr te paimportuara</param>
        /// <param name="emerSheet">Emri i sheet ne excel</param>
        /// <param name="session">Session</param>
        /// <returns>Objekt i perbere nga datasource, kolonat per konfigurim, mesazhi</returns>
        internal static object KtheDataSourceDheFormatImporti(int idKategoria, string kategoria, int formati, string lloji, string emerTabeleKoke, string emerTabeleTrupi, string emerTabeleRec, bool transferoFatura, bool tePaImportuara, string emerSheet, HttpSessionState session, bool rimerrTeImportuara, int? nrDokumentash)
        {

            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(session);
            int idPerdoruesi = mySessionObjects.ktheIdPerdoruesi(session);
            int idGjuha = mySessionObjects.ktheGjuhe(session);
                       

            switch (lloji.ToLower())
            {
               case "sql":
                    Tuple<DataTable, DataTable, DataTable, object, clsMesazh> teDhenaImporti = KtheDataSourceImportiNgaSql(idKategoria, kategoria, idNdermarrje, idPerdoruesi, formati, emerTabeleKoke, emerTabeleTrupi, emerTabeleRec, transferoFatura, tePaImportuara, idGjuha, session, rimerrTeImportuara, nrDokumentash);

                    DataTable dataSource = teDhenaImporti.Item1;

                    return new { dataSource = new { koka = teDhenaImporti.Item1, trupi = teDhenaImporti.Item2, receptura = teDhenaImporti.Item3 }, columnsKonfig = teDhenaImporti.Item4, mesazh = teDhenaImporti.Item5 };

                case "xls":
                case "xlsx":
                case "csv":
                    Tuple<DataTable, object, clsMesazh> teDhenaImportiFile = KtheDataSourceImportiNgaFile(formati, emerSheet, session);

                    return new { dataSource = new { koka = teDhenaImportiFile.Item1, trupi = DBNull.Value, receptura = DBNull.Value }, columnsKonfig = teDhenaImportiFile.Item2, mesazh = teDhenaImportiFile.Item3 };
            }
            return new { dataSource = new { koka = DBNull.Value, trupi = DBNull.Value, receptura = DBNull.Value }, columnsKonfig = DBNull.Value, mesazh = new clsMesazh() };
        }

        /// <summary>
        /// Funksion qe kthen datasource-n me te dhenat per import, kolonat per konfigurimin e grides se importit dhe mesazhin kur importi kryhen me file
        /// </summary>
        /// <param name="idKategoria">Id e kategorise per te cilen po kryhet importi</param>
        /// <param name="kategoria">Kodi i kategorise per te cilen po kryhet importi</param>
        /// <param name="idNdermarrje">Id e ndermarrjes</param>
        /// <param name="idPerdoruesi">Id e perdoruesit</param>
        /// <param name="formati">Id e formatit i importit</param>
        /// <param name="emerTabeleKoke">Emri i tabeles se kokes</param>
        /// <param name="emerTabeleTrupi">Emri i tabeles se trupit</param>
        /// <param name="emerTabeleRec">Emri i tabeles se recepturave</param>
        /// <param name="transferoFatura">Transfero Fatur</param>
        /// <param name="tePaImportuara">Merr te paimportuara</param>
        /// <param name="idGjuha">Id e gjuhes</param>
        /// <param name="emerSheet">Emri i sheet ne excel</param>
        /// <param name="session">Session</param>
        /// <returns>Tuple i perbere nga datasource, kolonat per konfigurim, mesazhi</returns>
        private static Tuple<DataTable, object, clsMesazh> KtheDataSourceImportiNgaFile(int formati, string emerSheet, HttpSessionState session)
        {
            CultureInfo ci = mySessionObjects.ktheCultureInfo(session);
            var (FileName, FileContent) = mySessionObjects.merrTedhenaNgafileUplodi(session);
            if (string.IsNullOrEmpty(FileName))
                return new Tuple<DataTable, object, clsMesazh>(null, null, new MesazhGabimi(MessagesResource.Messages["msgNukKeniZgjedhurAsnjeSkedar"]));

            OleDbConnection oconn = new OleDbConnection();  //duhet te kete providerin        
            DataTable table = new DataTable();
            clsMesazh mesazh = new MesazhSuksesi();

            string pathDir = HttpContext.Current.Server.MapPath(null);// + @"\Import\";
            pathDir = pathDir.Substring(0, HttpContext.Current.Server.MapPath(null).IndexOf(@"\api\Importi", StringComparison.Ordinal)) + @"\Import\";

            try
            {
                if (FileName.EndsWith(".csv"))
                    table = FileReader.CsvReaderMeHeader(FileContent);
                else
                {
                    DirectoryExtension.CreateDirIfNotExists(pathDir);
                    oconn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + pathDir + FileName + "';Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;ImportMixedTypes=Text\"");

                    string emer;
                    if (!string.IsNullOrEmpty(emerSheet))
                        emer = emerSheet + "$";
                    else
                    {
                        oconn.Open();
                        var dtExcelSchema = oconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        emer = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        oconn.Close();
                    }
                    var ocmd = new OleDbCommand("select * from [" + emer + "]", oconn);
                    oconn.Open();
                    var dba = new OleDbDataAdapter(ocmd);
                    dba.Fill(table);

                }
            }
            catch (OleDbException ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                return new Tuple<DataTable, object, clsMesazh>(null, null, new MesazhGabimi(MessagesResource.Messages["msgTeDhenatESkedaritTePasakta"]));
            }
            catch (Exception ex)
            {
                LogManager.GetCurrentClassLogger().Error(ex.Message);
                return new Tuple<DataTable, object, clsMesazh>(null, null, new MesazhGabimi(ex.Message));
            }
            finally
            {
                try
                {
                    oconn.Close();
                    File.Delete(pathDir + FileName); //perdoret kur kemi OleDbConnection
                }
                catch (Exception ex)
                {
                    LogManager.GetCurrentClassLogger().Error(ex.Message, "Ndodhi nje gabim gjate fshirjes se file-t " + FileName + " ne folderin Import.");
                    mesazh = new MesazhGabimi(MessagesResource.Messages["msgTeDhenatESkedaritTePasakta"]);
                }
            }
            if(!mesazh)
                return new Tuple<DataTable, object, clsMesazh>(null, null, mesazh);

            table.Columns.Add("Id", typeof(int));
            var i = 0;
            foreach (DataRow r in table.Rows)
            {
                r["Id"] = i;
                i++;
            }

            table.PrimaryKey = new[] { table.Columns["Id"] };

            if (formati == 0)
                return new Tuple<DataTable, object, clsMesazh>(null, null, new MesazhGabimi(MessagesResource.Messages["msgImportKyFormatNukEkziston"]));

            var format = new clsKokaFormatImporti(formati);
            if (format.IdKoka == 0)
                return new Tuple<DataTable, object, clsMesazh>(null, null, new MesazhGabimi(MessagesResource.Messages["msgImportKyFormatNukEkziston"]));

            var col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDheVisible(formati, true);

            List<object> fushat = new List<object>();
            fushat.Add(new
            {
                KodiTrupi = "Id",
                PershkrimiTrupi = "Rreshti",
                ReadonlyTrupi = true,
                VisibleTrupi = true
            });

            DateTime datefillimi = new DateTime(), datembarimi = new DateTime();
            foreach (var trup in col)
            {
                if (string.IsNullOrEmpty(trup.FusheType) || trup.FusheType.ToLower().Contains("varchar"))
                    ChangeColumnDataType(table, trup.EmerImporti, typeof(String), false, false);

                if (format.IdKategori == 172)//Alokim Buxheti 
                {
                    for (int nrMuaj = 1; nrMuaj < 12; nrMuaj++)
                    {
                        if (table.Columns[((DbCore.DbListPagesat.Muajt)nrMuaj).ToString()] == null)
                            table = new DataTable();

                        ChangeColumnDataType(table, ((DbCore.DbListPagesat.Muajt)nrMuaj).ToString(), typeof(string), false, false);
                    }
                    continue;
                }

                if (format.IdKategori == 98 && trup.KodKontrolli == "Fillim periudhe")//kur kemi listorare kontrollojme kolona per cdo date midis periudhave
                {
                    DateTime.TryParse(trup.VleraDefault, out datefillimi);
                    DateTime.TryParse(col.Find(x => x.KodKontrolli == "Mbarim periudhe").VleraDefault, out datembarimi);
                    for (var data = datefillimi; data <= datembarimi; data = data.AddDays(1))
                    {
                        if (table.Columns[data.ToString("dddd, d MMMM yyyy", ci)] == null)
                        {
                            table = new DataTable();
                            return new Tuple<DataTable, object, clsMesazh>(null, null, new MesazhGabimi(MessagesResource.Messages["msgDokumentFormatJoiduhur"]));
                        }
                        ChangeColumnDataType(table, data.ToString("dddd, d MMMM yyyy", ci), typeof(string), false, false);
                    }
                    continue;
                }
                if (format.IdKategori == 98 && trup.KodKontrolli == "Mbarim periudhe")
                    continue;
                if (!trup.Shfaq) continue;
                if (table.Columns[trup.EmerImporti.Replace('.', '#')] != null) continue;
                table = new DataTable();
                return new Tuple<DataTable, object, clsMesazh>(null, null, new MesazhGabimi(MessagesResource.Messages["msgDokumentFormatJoiduhur"]));
            }
            switch (format.IdKategori)
            {
                case 17:
                    ChangeColumnDataType(table, "Kohe Fillimi", typeof(string), true, false);
                    ChangeColumnDataType(table, "Kohe Mbarimi", typeof(string), true, false);
                    break;
                case 99:
                    ChangeColumnDataType(table, "Nga ora", typeof(string), true, false);
                    ChangeColumnDataType(table, "Ne ore", typeof(string), true, false);
                    ChangeColumnDataType(table, "Kod punonjesi", typeof(string), false, false);
                    ChangeColumnDataType(table, "Emer", typeof(string), false, false);
                    ChangeColumnDataType(table, "Mbiemer", typeof(string), false, false);
                    ChangeColumnDataType(table, "Totali", typeof(string), false, false);
                    break;
                case 98:
                    ChangeColumnDataType(table, "Kod punonjesi", typeof(string), false, false);
                    ChangeColumnDataType(table, "Emer", typeof(string), false, false);
                    ChangeColumnDataType(table, "Mbiemer", typeof(string), false, false);
                    break;
                case 1:
                case 2:
                    ChangeColumnDataType(table, "Perqindje Zbritje Totale", typeof(decimal), false, false);
                    ChangeColumnDataType(table, "Total Zbritje", typeof(decimal), false, false);
                    break;
                case 172:
                    ChangeColumnDataType(table, "Nr dokumenti", typeof(string), false, false);
                    break;
                case 7:
                    ChangeColumnDataType(table, "Date dok hyrje", typeof(DateTime), false, false);
                    ChangeColumnDataType(table, "Llogaria", typeof(string), false, true);
                    ChangeColumnDataType(table, "Pershkrimi", typeof(string), false, true);
                    break;
            }
            var array = new ArrayList();
            foreach (DataColumn d in table.Columns)
            {
                if (format.IdKategori == 98)//per list oraret shtojme kolonat per datat midis periudhes
                {
                    DateTime data;
                    var style = DateTimeStyles.AssumeLocal;
                    DateTime.TryParse(d.ColumnName, ci, style, out data);
                    if (data != new DateTime())
                    {
                        if (data < datefillimi || data > datembarimi)
                            array.Add(d);
                        continue;
                    }
                }
                if (format.IdKategori == 172 && Enum.IsDefined(typeof(DbCore.DbListPagesat.Muajt), d.ColumnName))//per Alokim Buxheti shtojme kolonat e muajve
                {
                    continue;
                }
                IEnumerable<clsTrupiFormatImporti> t = from c in col
                                                       where c.EmerImporti == d.ColumnName && c.Shfaq
                                                       select c;
                if (!t.Any() && d.ColumnName != "Id")
                    array.Add(d);
            }

            foreach (DataColumn a in array)
                table.Columns.Remove(a);

            foreach (DataColumn d in table.Columns)
            {
                clsTrupiFormatImporti fusheFormati = col.FirstOrDefault(x => x.EmerImporti == d.ColumnName);
                string fusheType = fusheFormati != null ? fusheFormati.FusheType : "string";
                if (d.ColumnName != "Id")
                    fushat.Add(new
                    {
                        KodiTrupi = d.ColumnName,
                        PershkrimiTrupi = d.ColumnName,
                        ReadonlyTrupi = false,
                        VisibleTrupi = true,
                        TipiFushes = KtheTipKoloneSipasFormatit(fusheType.ToLower())
                    });
            }

            return new Tuple<DataTable, object, clsMesazh>(table, new { fushaKoke = fushat, fushaTrupi = new List<object>(), fushaRec = new List<object>() }, new MesazhSuksesi());
        }

        /// <summary>
        /// Funksion qe kthen nje objekt me kolonat per konfigurimin e grides, dhe listat me fushat e kokes, trupit dhe recepturave
        /// </summary>
        /// <param name="formati">Id e formatit te importit</param>
        /// <param name="idKategoria">Id e kategorise per te cilen po kryhet importi</param>
        /// <returns>Tuple i perbere nga kolonat per konfigurimin e grides, fushat e kokes, fushat e trupit dhe fushat e recepturave</returns>
        private static Tuple<object, Tuple<List<string>, List<string>, List<string>>> MerrColFormatTrupiImporti(int formati, int idKategoria)
        {
            colTrupiFormatImporti col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(formati);
            List<clsTrupiFormatImporti> fushaKoke = col.FindAll(x => (x.FusheKokeApoTrupi == 1 && x.Visible == true) || x.FusheKokeApoTrupi == 3 || x.KodKontrolli == "Kod Ndermarrje");
            List<clsTrupiFormatImporti> fushaTrupi = col.FindAll(x => (x.Visible && x.FusheKokeApoTrupi == 2) || x.FusheKokeApoTrupi == 3 || (idKategoria == 45 && x.FusheKokeApoTrupi == 5));
            List<clsTrupiFormatImporti> fushaRec = col.FindAll(x => (x.Visible && x.FusheKokeApoTrupi == 4) || x.FusheKokeApoTrupi == 5);

            List<string> listFushaKoke = fushaKoke.Select(x => x.EmerImporti).ToList<string>();
            if (listFushaKoke.Count > 0)
            {
                listFushaKoke.Add("IDIMPORTSHITJE");
                listFushaKoke.Add("DTLEXIMI");
                listFushaKoke.Add("DTIMPORTI");
            }

            List<string> listFushaTrupi = fushaTrupi.Select(x => x.EmerImporti).ToList<string>();
            if (listFushaTrupi.Count > 0)
            {
                listFushaTrupi.Add("IDIMPORTSHITJE");
                listFushaTrupi.Add("IDIMPORTTRUPISHITJE");
            }

            List<string> listFushaRec = fushaRec.Select(x => x.EmerImporti).ToList<string>();
            if (listFushaRec.Count > 0)
            {
                listFushaRec.Add("IDIMPORTSHITJE");
                listFushaRec.Add("IDIMPORTTRUPISHITJE");
                listFushaRec.Add("IDIMPORTRECEPTURA");
            }

            object kolonaPerKonfig = new { fushaKoke = ktheFushaGatiPerKonfigurim(fushaKoke, Dokumenta.Koka), fushaTrupi = ktheFushaGatiPerKonfigurim(fushaTrupi, Dokumenta.Trupi), fushaRec = ktheFushaGatiPerKonfigurim(fushaRec, Dokumenta.Receptura) };

            Tuple<List<string>, List<string>, List<string>> listFushat = new Tuple<List<string>, List<string>, List<string>>(listFushaKoke, listFushaTrupi, listFushaRec);
            Tuple<object, Tuple<List<string>, List<string>, List<string>>> konfigs = new Tuple<object, Tuple<List<string>, List<string>, List<string>>>(kolonaPerKonfig, listFushat);

            return konfigs;
        }

        /// <summary>
        /// Funksion qe kthen kolonat gati per konfigurimin e grides se importit sipas formatit
        /// </summary>
        /// <param name="fushat">Collectioni me formatin e importit</param>
        /// <param name="kokeApoTrup">1-Fusha koke, 2-Fusha trupi, 4-Fusha recepture</param>
        /// <returns>List me objekte qe permbajne konfigurimet per griden e importit</returns>
        private static List<object> ktheFushaGatiPerKonfigurim(List<clsTrupiFormatImporti> fushat, Dokumenta kokeApoTrup)
        {

            List<object> columns = new List<object>();
            if (fushat.Count == 0)
                return columns;

            columns.Add(new
            {
                KodiTrupi = kokeApoTrup == Dokumenta.Koka ? "IDIMPORTSHITJE" : 
                            kokeApoTrup == Dokumenta.Trupi ? "IDIMPORTTRUPISHITJE" : 
                            kokeApoTrup == Dokumenta.Receptura ? "IDIMPORTRECEPTURA" : "Id",
                PershkrimiTrupi = "Rreshti",
                ReadonlyTrupi = true,
                VisibleTrupi = true,
                TipiFushes = "number",
                FusheKokeApoTrupi = (int)kokeApoTrup,
                sortOrder = "asc"
            });

            foreach (clsTrupiFormatImporti column in fushat)
            {
                if (!column.Shfaq || !column.Visible)
                    continue;
                
                columns.Add(new
                {
                    KodiTrupi = column.EmerImporti,
                    PershkrimiTrupi = column.EmerImporti,
                    ReadonlyTrupi = false,
                    VisibleTrupi = column.Visible,
                    TipiFushes = KtheTipKoloneSipasFormatit(column.FusheType.ToLower()),
                    FusheKokeApoTrupi = column.FusheKokeApoTrupi
                });
            }
            return columns;
        }

        /// <summary>
        /// Funksion qe kthen tipin e kolones se grides se importit sipas FusheType
        /// </summary>
        /// <param name="formati">Formati nga FusheType tek konfigurimi i importit</param>
        /// <returns>String qe permban tipin e fushes</returns>
        private static string KtheTipKoloneSipasFormatit(string formati)
        {
            if (formati.Contains("var"))
                return "string";
            else if (formati.Contains("int") || formati.Contains("float") || formati.Contains("double") || formati.Contains("decimal"))
                return "number";
            else if (formati.Contains("datetime"))
                return "datetime";
            else if (formati.Contains("date"))
                return "date";
            else if (formati.Contains("bit"))
                return "boolean";

            return "string";
        }

        private static Type KtheTipKoloneSipasFormatitPerDataTable(string formati)
        {
            if (formati == null || formati.Contains("var"))
                return typeof(string);
            else if (formati.Contains("int"))
                return typeof(int);
            else if (formati.Contains("float") || formati.Contains("double"))
                return typeof(double);
            else if (formati.Contains("decimal"))
                return typeof(decimal?);
            else if (formati.Contains("date"))
                return typeof(DateTime);
            else if (formati.Contains("bit"))
                return typeof(Boolean);

            return typeof(string);
        }

        /// <summary>
        /// Funksion qe kthen datasource-n me te dhenat per import, kolonat per konfigurimin e grides se importit dhe mesazhin kur importi kryhen nga sql
        /// </summary>
        /// <param name="idKategoria">Id e kategorise per te cilen po kryhet importi</param>
        /// <param name="kategoria">Kodi i kategorise per te cilen po kryhet importi</param>
        /// <param name="idNdermarrje">Id e ndermarrjes</param>
        /// <param name="idPerdoruesi">Id e perdoruesit</param>
        /// <param name="formati">Id e formatit i importit</param>
        /// <param name="emerTabeleKoke">Emri i tabeles se kokes</param>
        /// <param name="emerTabeleTrupi">Emri i tabeles se trupit</param>
        /// <param name="emerTabeleRec">Emri i tabeles se recepturave</param>
        /// <param name="transferoFatura">Transfero Fatur</param>
        /// <param name="tePaImportuara">Merr te paimportuara</param>
        /// <param name="idGjuha">Id e gjuhes</param>
        /// <param name="session">Session</param>
        /// <returns>Tuple i perbere nga datasource i kokes, trupit dhe recepturave, kolonat per konfigurim, mesazhi</returns>
        private static Tuple<DataTable, DataTable, DataTable, object, clsMesazh> KtheDataSourceImportiNgaSql(int idKategoria, string kategoria, int idNdermarrje, int idPerdoruesi, int formati, string emerTabeleKoke, string emerTabeleTrupi, string emerTabeleRec, bool transferoFatura, bool tePaImportuara, int idGjuha, HttpSessionState session, bool rimerrTeImportuara, int? nrDokumentash)
        {
            var idSuperKategori = clsKategoriNivelDok.mbushIDSuperKategoriNivDok(idKategoria);
            emerTabeleKoke = emerTabeleKoke.Replace(" ", "");

            clsMesazh mesazh = idGjuha == 0
                ? clsFunksione.kontrolloPerKaraktereSpeciale(emerTabeleKoke)
                : clsFunksione.kontrolloPerKaraktereSpecialeEng(emerTabeleKoke);
            if (!mesazh.Status)
            {
                mesazh.PershkrimMesazhi = MessagesResource.Messages["tabelaEKokes"] + mesazh.PershkrimMesazhi;
                return new Tuple<DataTable, DataTable, DataTable, object, clsMesazh>(null, null, null, null, mesazh);
            }
            if (idSuperKategori == (int)SuperKategori.Regjistrime)
            {
                emerTabeleTrupi = emerTabeleTrupi.Replace(" ", "");
                mesazh = idGjuha == 0
                    ? clsFunksione.kontrolloPerKaraktereSpeciale(emerTabeleTrupi)
                    : clsFunksione.kontrolloPerKaraktereSpecialeEng(emerTabeleTrupi);
                if (!mesazh.Status)
                {
                    mesazh.PershkrimMesazhi = MessagesResource.Messages["tabelaETrupit"] + mesazh.PershkrimMesazhi;
                    return new Tuple<DataTable, DataTable, DataTable, object, clsMesazh>(null, null, null, null, mesazh);
                }
            }

            mesazh = clsFunksione.kontrolloMosEkzistenceTabelashDheSP(emerTabeleKoke, emerTabeleTrupi, false, idKategoria, emerTabeleRec, idSuperKategori);
            if (!mesazh.Status)
                return new Tuple<DataTable, DataTable, DataTable, object, clsMesazh>(null, null, null, null, mesazh);

            DataTable dataSource;

            if (transferoFatura)
            {
                Tuple<DataTable, clsMesazh> transferimFaturash = TransferoFatura(idKategoria, kategoria, idNdermarrje, idPerdoruesi, formati, emerTabeleKoke, emerTabeleTrupi, emerTabeleRec, tePaImportuara, idGjuha, session, rimerrTeImportuara, nrDokumentash);
                dataSource = transferimFaturash.Item1;
                mesazh = transferimFaturash.Item2;
            }
            else
            {
                dataSource = MbushListeNgaDb(idKategoria, kategoria, idNdermarrje, idPerdoruesi, formati, emerTabeleKoke, emerTabeleTrupi, emerTabeleRec, tePaImportuara, rimerrTeImportuara, nrDokumentash);
                mesazh = new MesazhSuksesi();
            }
            Tuple<object, Tuple<List<string>, List<string>, List<string>>> konfigs = MerrColFormatTrupiImporti(formati, idKategoria);
            
            try
            {
                Tuple<DataTable, DataTable, DataTable> dsDokumenti = merrDsDokumentiTeNdare(dataSource, konfigs);
                return new Tuple<DataTable, DataTable, DataTable, object, clsMesazh>(dsDokumenti.Item1, dsDokumenti.Item2, dsDokumenti.Item3, konfigs.Item1, new MesazhSuksesi());
            }
            catch (Exception ex)
            {
                return new Tuple<DataTable, DataTable, DataTable, object, clsMesazh>(null, null, null, konfigs.Item1, new MesazhGabimi(MessagesResource.Messages["msgDokumentFormatJoiduhur"]));
            }
        }

        /// <summary>
        /// Funksion qe ben ndarjen e rreshtave te importit ne koke, trup dhe recepture
        /// </summary>
        /// <param name="dataSource">Rreshtat per import</param>
        /// <param name="konfigs">Kolonat per ndarje</param>
        /// <returns>Tuple i perbere nga rreshtat e kokes, trupi dhe recepturave</returns>
        private static Tuple<DataTable, DataTable, DataTable> merrDsDokumentiTeNdare(DataTable dataSource, Tuple<object, Tuple<List<string>, List<string>, List<string>>> konfigs)
        {
            if (dataSource == null)
                return new Tuple<DataTable, DataTable, DataTable>(null, null, null);

            DataView dataSourceView = new DataView(dataSource);
            DataTable dataSourceKoka = null, dataSourceTrupi = null, dataSourceRec = null;
            if (konfigs.Item2.Item1.Count > 0)
                dataSourceKoka = dataSourceView.ToTable(true, konfigs.Item2.Item1.ToArray());
            if (konfigs.Item2.Item2.Count > 0)
                dataSourceTrupi = dataSourceView.ToTable(true, konfigs.Item2.Item2.ToArray());
            if (konfigs.Item2.Item3.Count > 0)
                dataSourceRec = dataSourceView.ToTable(true, konfigs.Item2.Item3.ToArray());

            return new Tuple<DataTable, DataTable, DataTable>(dataSourceKoka, dataSourceTrupi, dataSourceRec);
        }

        /// <summary>
        /// Funksion qe ben transferimin e faturave ne import per te cilat eshte checkuar fusha Transfero Fatura
        /// </summary>
        /// <param name="idKategoria">Id e kategorise per te cilen po kryhet importi</param>
        /// <param name="kategoria">Kodi i kategorise per te cilen po kryhet importi</param>
        /// <param name="idNdermarrje">Id e ndermarrjes</param>
        /// <param name="idPerdoruesi">Id e perdoruesit</param>
        /// <param name="formati">Id e formatit i importit</param>
        /// <param name="emerTabeleKoke">Emri i tabeles se kokes</param>
        /// <param name="emerTabeleTrupi">Emri i tabeles se trupit</param>
        /// <param name="emerTabeleRec">Emri i tabeles se recepturave</param>
        /// <param name="tePaImportuara">Merr te paimportuara</param>
        /// <param name="idGjuha">Id e gjuhes</param>
        /// <param name="session">Session</param>
        /// <returns>Tuple i perbere nga rreshtat per import dhe mesazhi</returns>
        private static Tuple<DataTable, clsMesazh> TransferoFatura(int idKategoria, string kategoria, int idNdermarrje, int idPerdoruesi, int formati, string emerTabeleKoke, string emerTabeleTrupi, string emerTabeleRec, bool tePaImportuara, int idGjuha, HttpSessionState session, bool rimerrTeImportuara, int? nrDokumentash)
        {
            //Behet shnderrimi dhe transferimi i policave nga tabela temporale ku i shkruan programi tjeter (i edusoft), tek tabelat e importit. Eshte bere vetem per klientin Albsig.
            if (!colImportSQL.ekzistonTabele("T_TEMP_IMPORTPOLICA"))
                return new Tuple<DataTable, clsMesazh>(null, new MesazhGabimi("Nuk ekziston tabela temporale e policave!"));

            int idErrori = 0;
            var mesazhi = colImportSQL.transferoPolicaNeTabelaImporti(emerTabeleKoke, emerTabeleTrupi, idNdermarrje, idPerdoruesi, out idErrori);
            if (!mesazhi.Status)
                return new Tuple<DataTable, clsMesazh>(null, mesazhi);

            DataTable dataSource = MbushListeNgaDb(idKategoria, kategoria, idNdermarrje, idPerdoruesi, formati, emerTabeleKoke, emerTabeleTrupi, emerTabeleRec, tePaImportuara, rimerrTeImportuara, nrDokumentash);

            if (idErrori != 0)
            {
                var err = colTrupiErrorImporti.ktheErrorImportiSipasIdKoka(idErrori);
                if (err.Rows.Count == 0)
                    return new Tuple<DataTable, clsMesazh>(dataSource, null);

                mySessionObjects.ruajTabeleGabimeshImporti(session, err);
                if (err.Rows.Count > 0)
                {
                    var koka = new clsKokaErrorImporti(0, "Nga transferimi i policave", idKategoria, idNdermarrje, idPerdoruesi);
                    koka.ColTrupi.mbushErrorImportiNgaProgrami(err);
                    mesazhi = koka.ruajErrorImporti();
                    return new Tuple<DataTable, clsMesazh>(dataSource, new MesazhGabimi("Nuk u transferuan te gjitha faturat! Hapni listen e gabimeve per me shume informacion!:Red"));
                }
                else
                    return new Tuple<DataTable, clsMesazh>(dataSource, new MesazhGabimi("Transferimi i faturave perfundoi me sukses!:Green"));
            }
            else
                return new Tuple<DataTable, clsMesazh>(dataSource, new MesazhGabimi("Transferimi i faturave perfundoi me sukses!:Green"));
        }

        /// <summary>
        /// Funksion qe merr dhe kthen rreshtat per import nga databaza
        /// </summary>
        /// <param name="idKategoria">Id e kategorise per te cilen po kryhet importi</param>
        /// <param name="kategoria">Kodi i kategorise per te cilen po kryhet importi</param>
        /// <param name="idNdermarrje">Id e ndermarrjes</param>
        /// <param name="idPerdoruesi">Id e perdoruesit</param>
        /// <param name="formati">Id e formatit i importit</param>
        /// <param name="emerTabeleKoke">Emri i tabeles se kokes</param>
        /// <param name="emerTabeleTrupi">Emri i tabeles se trupit</param>
        /// <param name="emerTabeleRec">Emri i tabeles se recepturave</param>
        /// <param name="tePaImportuara">Merr te paimportuara</param>
        /// <returns>DataTable qe permban rreshtat per import</returns>
        private static DataTable MbushListeNgaDb(int idKategoria, string kategoria, int idNdermarrje, int idPerdoruesi, int formati, string emerTabeleKoke, string emerTabeleTrupi, string emerTabeleRec, bool tePaImportuara, bool rimerrTeImportuara, int? nrDokumentash)
        {
            var nderm = new clsNdermarrje(idNdermarrje);
            DataTable dt = null;
            string nenkategoria = string.Empty, ndermarrjeKod = string.Empty, idprimaryProdukt = string.Empty;

            var col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(formati);
            string idprimary = col.MerrPrimaryKey(kategoria);

            if (!string.IsNullOrWhiteSpace(idprimary))
                colImportSQL.FshiDokumentatTeDuplikuar(emerTabeleKoke, idprimary);

            switch (kategoria)
            {
                case "Shitje":
                case "Blerje":
                    nenkategoria = col.ktheEmerImportiSipasKodKontrolli("Nenkategoria");
                    ndermarrjeKod = col.ktheEmerImportiSipasKodKontrolli("Kod Ndermarrje");
                    idprimary = col.ktheEmerImportiSipasKodKontrolli("Id Shitje Koka");
                    dt = colKokaShitje.merrShitjePerImport(emerTabeleKoke, emerTabeleTrupi, ndermarrjeKod, nderm.NdermarrjeKodi, nenkategoria, idKategoria, idprimary, tePaImportuara, rimerrTeImportuara, nrDokumentash);
                    dt.PrimaryKey = new[] { dt.Columns["IDIMPORTTRUPISHITJE"] };
                    break;
                case "Magazina":
                    nenkategoria = col.ktheEmerImportiSipasKodKontrolli("Nenkategoria");
                    ndermarrjeKod = col.ktheEmerImportiSipasKodKontrolli("Kod Ndermarrje");
                    idprimary = col.ktheEmerImportiSipasKodKontrolli("Id Koka Magazina");
                    dt = colKokaMagazina.merrDokMagazinePerImport(emerTabeleKoke, emerTabeleTrupi, ndermarrjeKod, nderm.NdermarrjeKodi, nenkategoria, idKategoria, idprimary, tePaImportuara, rimerrTeImportuara, nrDokumentash);
                    dt.PrimaryKey = new[] { dt.Columns["IDIMPORTTRUPISHITJE"] };
                    break;
                case "Veprime Arke":
                case "Veprime Banke":
                    nenkategoria = col.ktheEmerImportiSipasKodKontrolli("Nenkategoria");
                    ndermarrjeKod = col.ktheEmerImportiSipasKodKontrolli("Kod Ndermarrje");
                    idprimary = col.ktheEmerImportiSipasKodKontrolli("Id Koka");
                    dt = colVeprimBankaKoka.merrArkaBankaPerImport(emerTabeleKoke, emerTabeleTrupi, ndermarrjeKod, nderm.NdermarrjeKodi, nenkategoria, idKategoria, idprimary, tePaImportuara, rimerrTeImportuara, nrDokumentash);
                    dt.PrimaryKey = new[] { dt.Columns["IDIMPORTTRUPISHITJE"] };
                    break;
                case "Perfitim Buxheti":
                    ndermarrjeKod = col.ktheEmerImportiSipasKodKontrolli("Kod Ndermarrje");
                    idprimary = col.ktheEmerImportiSipasKodKontrolli("Id Koka Buxheti");
                    dt = DbCore.DbBuxheti.ColBKokaBuxheti.merrDokumentaPerImport(emerTabeleKoke, emerTabeleTrupi, ndermarrjeKod, nderm.NdermarrjeKodi, idprimary, tePaImportuara, rimerrTeImportuara, nrDokumentash);
                    dt.PrimaryKey = new[] { dt.Columns["IDIMPORTTRUPISHITJE"] };
                    break;
                case "Ekzekutim Prodhimi":
                    ndermarrjeKod = col.ktheEmerImportiSipasKodKontrolli("Kod Ndermarrje");
                    idprimary = col.ktheEmerImportiSipasKodKontrolli("IdKokaEkzekutimProdhimi");
                    idprimaryProdukt = col.ktheEmerImportiSipasKodKontrolli("IdKokaProduktProdhimi");
                    dt = colKokaEkzekutim.merrEkzekutimePerImport(emerTabeleKoke, emerTabeleTrupi, emerTabeleRec, ndermarrjeKod, nderm.NdermarrjeKodi, idprimary, idprimaryProdukt, tePaImportuara, rimerrTeImportuara, nrDokumentash);
                    dt.PrimaryKey = new[] { dt.Columns["IDIMPORTRECEPTURA"] };
                    break;
                case "Artikuj afatshkurter/afatgjate":
                case "Klient/Furnitor":
                case "Llogari":
                case "Grupet e Artikujve":
                case "Receptura":
                    ndermarrjeKod = col.ktheEmerImportiSipasKodKontrolli("Kod Ndermarrje");
                    dt = colImportSQL.merrObjektePerImportSQL(emerTabeleKoke, ndermarrjeKod, nderm.NdermarrjeKodi, tePaImportuara, rimerrTeImportuara, nrDokumentash);
                    dt.PrimaryKey = new[] { dt.Columns["IDIMPORTSHITJE"] };
                    break;
                case "Inventarizimi":
                case "Inventarizimi afatgjate":
                    ndermarrjeKod = col.ktheEmerImportiSipasKodKontrolli("Kod Ndermarrje");
                    idprimary = col.ktheEmerImportiSipasKodKontrolli("Id Koka");
                    dt = colKokaEkzekutim.merrEkzekutimePerImport(emerTabeleKoke, emerTabeleTrupi, ndermarrjeKod, nderm.NdermarrjeKodi, idprimary, tePaImportuara, rimerrTeImportuara, nrDokumentash);
                    dt.PrimaryKey = new[] { dt.Columns["IDIMPORTTRUPISHITJE"] };
                    break;
                case "Perdoruesit":
                    ndermarrjeKod = col.ktheEmerImportiSipasKodKontrolli("Kod Ndermarrje");
                    dt = colImportSQL.merrObjektePerImportSQL(emerTabeleKoke, ndermarrjeKod, nderm.NdermarrjeKodi, tePaImportuara, rimerrTeImportuara, nrDokumentash);
                    dt.PrimaryKey = new DataColumn[] { dt.Columns["IDIMPORTSHITJE"] };
                    break;
            }

            return dt;
        }

        /// <summary>
        /// Funksion qe ben modifikimin/fshirjen e rreshtave te importit
        /// </summary>
        /// <param name="objRowsKokaDheRecepturaModifikim">Rreshtat per modifikim te kokes dhe recepturave</param>
        /// <param name="objRowsTrupiModifikim">Rreshtat per modifikim te trupi</param>
        /// <param name="objRowsFshirje">Rreshtat per fshirje</param>
        /// <param name="formati">Id e formatit i importit</param>
        /// <param name="idKategoria">Id e kategorise per te cilen po kryhet importi</param>
        /// <param name="tabKoka">Emri i tabeles se kokes</param>
        /// <param name="tabTrupi">Emri i tabeles se trupit</param>
        /// <param name="tabReceptura">Emri i tabeles se recepturave</param>
        /// <param name="session">Session</param>
        /// <returns>Objekt me mesazhin</returns>
        internal static object ModifikoDokumentaNeTabeleTemporale(object objRowsKokaDheRecepturaModifikim, object objRowsTrupiModifikim, object objRowsFshirje, int formati, int idKategoria, string tabKoka, string tabTrupi, string tabReceptura, HttpSessionState Session)
        {
            Dictionary<string, object> dtRowsModifikim = JsonConvert.DeserializeObject<Dictionary<string, object>>(objRowsKokaDheRecepturaModifikim.ToString());
            Dictionary<string, object> dtRowsFshi = JsonConvert.DeserializeObject<Dictionary<string, object>>(objRowsFshirje.ToString());

            var col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKoka(formati);

            var idSuperKategori = clsKategoriNivelDok.mbushIDSuperKategoriNivDok(idKategoria);
            List<string> fushaKoke = col.FindAll(x => (x.FusheKokeApoTrupi == 1 && x.Visible == true) || x.FusheKokeApoTrupi == 3 || x.KodKontrolli == "Kod Ndermarrje").Select(x => x.KodKontrolli).ToList<string>();
            List<string> fushaTrupi = col.FindAll(x => (x.Visible && x.FusheKokeApoTrupi == 2) || x.FusheKokeApoTrupi == 3 || (idKategoria == 45 && x.FusheKokeApoTrupi == 5)).Select(x => x.KodKontrolli).ToList<string>();
            List<string> fushaRec = col.FindAll(x => (x.Visible && x.FusheKokeApoTrupi == 4) || x.FusheKokeApoTrupi == 5).Select(x => x.KodKontrolli).ToList<string>();
            if (fushaKoke.Count > 0)
            {
                fushaKoke.AddIfNotExists("IDIMPORTSHITJE");
                fushaKoke.AddIfNotExists("DTLEXIMI");
                fushaKoke.AddIfNotExists("DTIMPORTI");
            }
            if (fushaTrupi.Count > 0)
                fushaTrupi.AddIfNotExists("IDIMPORTTRUPISHITJE");

            if (fushaRec.Count > 0)
                fushaRec.AddIfNotExists("IDIMPORTRECEPTURA");

            DataTable koka = JsonConvert.DeserializeObject<DataTable>(dtRowsModifikim["koka"].ToString());
            DataTable trupi = CreateTableFromData(objRowsTrupiModifikim, formati);
            DataTable rec = JsonConvert.DeserializeObject<DataTable>(dtRowsModifikim["receptura"].ToString());

            DataTable kokaFshi = JsonConvert.DeserializeObject<DataTable>(dtRowsFshi["kokaFshi"].ToString());
            DataTable trupiFshi = JsonConvert.DeserializeObject<DataTable>(dtRowsFshi["trupiFshi"].ToString());
            DataTable recFshi = JsonConvert.DeserializeObject<DataTable>(dtRowsFshi["recepturaFshi"].ToString());

            clsMesazh mesazh = colImportSQL.modifikoDokumentNeTabeleImporti(fushaKoke, koka, trupi, col, tabKoka, tabTrupi, idKategoria, tabReceptura, rec, idSuperKategori);
            if (!mesazh)
                return mesazh;

            return colImportSQL.fshiDokumentNgaTabelaTemporale(fushaKoke, kokaFshi, trupiFshi, col, tabKoka, tabTrupi, idKategoria, tabReceptura, recFshi, idSuperKategori);
        }

        /// <summary>
        /// Funksion qe kontrollon/importon rreshtat e grides
        /// </summary>
        /// <param name="rreshtaImporti">Rreshtat per import</param>
        /// <param name="idKonfig">Id e konfigurimit te importit</param>
        /// <param name="idKategoria">Id e kategorise per te cilen po kryhet importi</param>
        /// <param name="kategoria">Kodi i kategorise per te cilen po kryhet importi</param>
        /// <param name="formati">Id e formatit i importit</param>
        /// <param name="lloji">Lloji i importit (xls,xlsx,csv,sql)</param>
        /// <param name="transferoFatura">Transfero Fatura</param>
        /// <param name="tePaImportuara">Merr te paimportuara</param>
        /// <param name="permbledhese">Gjenero fature permbledhese</param>
        /// <param name="tabKoka">Emri i tabeles se kokes</param>
        /// <param name="tabTrupi">Emri i tabeles se trupit</param>
        /// <param name="tabRec">Emri i tabeles se recepturave</param>
        /// <param name="mbishkruajVleratEMeparshmeObj">Mbishkruaj vlerat e meparshme te importuara</param>
        /// <param name="importo">True-Importo, False-Kontrollo</param>
        /// <param name="session">Session</param>
        /// <returns>Objekt me mesazhin</returns>
        internal static object KontrolloImportoTeDhenaGridImporti(object rreshtaImporti, int idKonfig, int idKategoria, string kategoria, int formati, string lloji, bool transferoFatura, bool tePaImportuara, bool permbledhese, string tabKoka, string tabTrupi, string tabRec, object mbishkruajVleratEMeparshmeObj, bool importo, HttpSessionState session)
        {
            DataTable teDhenaImporti = CreateTableFromData(rreshtaImporti, formati);
            DataView dv = teDhenaImporti.DefaultView;
            dv.Sort = lloji == "SQL" ? $"IDIMPORTSHITJE {(!string.IsNullOrEmpty(tabTrupi) ? ", IDIMPORTTRUPISHITJE" : "")} {(!string.IsNullOrEmpty(tabRec) ? ", IDIMPORTRECEPTURA" : "")}" : "Id";
            teDhenaImporti = dv.ToTable();
            DataTable rreshtaJoOk = new DataTable();

            bool mbishkruajVleratEMeparshme = Converter.MerrVlereOseDefault<bool>(mbishkruajVleratEMeparshmeObj);
            if (formati == 0)
                return new MesazhGabimi (MessagesResource.Messages["msgImportKyFormatNukEkziston"]);

            var format = new clsKokaFormatImporti(formati);
            if (format.IdKoka == 0)
                return new MesazhGabimi(MessagesResource.Messages["msgImportKyFormatNukEkziston"]);
            
            bool kaVleraTeImportuara = false;

            clsKonfigImporti konfigImporti = new clsKonfigImporti();
            if (idKonfig > 0)
                konfigImporti = new clsKonfigImporti(idKonfig);

            ImporteUtils funksionImporti = new ImporteUtils();
            funksionImporti.Session = session;
            funksionImporti.Rm = new ResourceManager("Resources.Strings", Assembly.Load("App_GlobalResources"));
            funksionImporti.Ci = mySessionObjects.ktheCultureInfo(session);
            funksionImporti.IdGjuha = mySessionObjects.ktheGjuhe(session);
            funksionImporti.IdKategoria = idKategoria;
            funksionImporti.IdNdermarrja = mySessionObjects.merrIdNdermarrjeSesioni(session);
            funksionImporti.IdNdermarrjeVit = mySessionObjects.ktheNdermarrjeVit(session);
            funksionImporti.IdViti = mySessionObjects.ktheIdVitNdermarrje(session);
            funksionImporti.VitiNdermarrjes = mySessionObjects.ktheVitiNdermarrjes(session);
            funksionImporti.IdPerdoruesi = mySessionObjects.ktheIdPerdoruesi(session);
            funksionImporti.Permbledhese = permbledhese;
            funksionImporti.TabKoka = tabKoka;
            funksionImporti.TabTrupi = tabTrupi;
            funksionImporti.TabReceptura = tabRec;
            funksionImporti.VjenNgaImportSQL = lloji.ToLower() == "sql";
            funksionImporti.FormatImporti = format;
            funksionImporti.KonfigImporti = konfigImporti;
            clsMesazh mesazh = funksionImporti.KontrolloImporto(mbishkruajVleratEMeparshme, out kaVleraTeImportuara, teDhenaImporti, ref rreshtaJoOk, importo);

            object rreshtaJoOkObj = null;
            if (rreshtaJoOk.Rows.Count > 0)
            {
                Tuple<object, Tuple<List<string>, List<string>, List<string>>> konfigs = MerrColFormatTrupiImporti(formati, idKategoria);
                if(lloji == "SQL")
                {
                    Tuple<DataTable, DataTable, DataTable> dsDokumenti = merrDsDokumentiTeNdare(rreshtaJoOk, konfigs);
                    rreshtaJoOkObj = new { koka = dsDokumenti.Item1, trupi = dsDokumenti.Item2, receptura = dsDokumenti.Item3 };
                }
                else
                    rreshtaJoOkObj = new { koka = rreshtaJoOk, trupi = DBNull.Value, receptura = DBNull.Value };
            }

            if (funksionImporti.Status == "import")
            {
                return new { status1 = funksionImporti.Status, rreshtaJoOk = rreshtaJoOkObj, kaVleraTeImportuara = kaVleraTeImportuara.ToString(), mesazh = mesazh};
            }
            return new { status1 = "kontrollo", rreshtaJoOk = rreshtaJoOkObj, kaVleraTeImportuara = kaVleraTeImportuara.ToString(), mesazh = mesazh };
        }

        private static DataTable CreateTableFromData(object rreshtaImporti, int formati)
        {
            colTrupiFormatImporti col = colTrupiFormatImporti.merrFormatImportiTrupiSipasIdKokaDheVisible(formati, true);
            DataTable teDhenaImporti = new DataTable();
            List<Dictionary<string, object>> rreshtaDeserialized = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(rreshtaImporti.ToString());
            if (rreshtaDeserialized.Count == 0)
                return teDhenaImporti;
            teDhenaImporti.Columns.AddRange(rreshtaDeserialized.First().Select(r => new DataColumn(r.Key)).ToArray());

            foreach (clsTrupiFormatImporti column in col)
                ChangeColumnDataType(teDhenaImporti, column.EmerImporti, KtheTipKoloneSipasFormatitPerDataTable(column.FusheType.ToLower()), false, true);
            ChangeColumnDataType(teDhenaImporti, "Id", typeof(int), false, true);
            rreshtaDeserialized.ForEach(r => teDhenaImporti.Rows.Add(r.Select(c => c.Value).Cast<object>().ToArray()));

            return teDhenaImporti;
        }
        public static bool PastroTabelatTemporare(string emerTabeleKoka, string emerTabeleTrupi, string emerTabeleKokaHistorik, string emerTabeleTrupiHistorik)
        {
            string emerTabeleKokaFshirje = "";
            string emerTabeletrupiFshirje = "";
            string emerTabeleKokaHistroikFshirje = "";
            string emerTabeleTrupiHistorikFshirje = "";
            clsDatabaseAdmin admin = new clsDatabaseAdmin();
            Queue<string> queue = admin.merrTabelatEImportit();
            while(queue.Count != 0)
            {
                string first = queue.Peek();
                if (emerTabeleKoka == first) emerTabeleKokaFshirje = emerTabeleKoka;
                if(emerTabeleKokaHistorik == first) emerTabeleKokaHistroikFshirje = emerTabeleKokaHistorik;
                if (emerTabeleTrupi == first) emerTabeletrupiFshirje = emerTabeleTrupi;
                if (emerTabeleTrupiHistorik == first) emerTabeleTrupiHistorikFshirje = emerTabeleTrupiHistorik;
                queue.Dequeue();
            }
            if (emerTabeleKokaFshirje != "")
                emerTabeleKoka = $" IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{emerTabeleKoka}') BEGIN DELETE FROM " + emerTabeleKoka + " END ";
            if (emerTabeletrupiFshirje != "")
                emerTabeleTrupi = $" IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{emerTabeleTrupi}') BEGIN DELETE FROM " + emerTabeleTrupi + " END ";
            if (emerTabeleKokaHistroikFshirje != "")
                emerTabeleKokaHistorik = $" IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{emerTabeleKokaHistorik}') BEGIN DELETE FROM " + emerTabeleKokaHistorik + " END ";
            if (emerTabeleTrupiHistorikFshirje != "")
                emerTabeleTrupiHistorik = $" IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{emerTabeleTrupiHistorik}') BEGIN DELETE FROM " + emerTabeleTrupiHistorik + " END ";
            admin.fshiTabelaTemporareImporti(emerTabeleKoka, emerTabeleTrupi, emerTabeleKokaHistorik, emerTabeleTrupiHistorik);
            return true;
        }
        public static bool ChangeColumnDataType(DataTable table, string columnname, Type newtype, bool ore, bool forceRecreate)
        {
            if (table.Columns.Contains(columnname) == false)
                return false;

            var column = table.Columns[columnname];
            if (column.DataType == newtype && !forceRecreate)
                return true;

            try
            {
                var newcolumn = new DataColumn("temperary", newtype);
                table.Columns.Add(newcolumn);
                foreach (DataRow row in table.Rows)
                {
                    try
                    {
                        if (ore)
                            row["temperary"] = Convert.ChangeType(row[columnname].ToString().Substring(11, 5), newtype);
                        else if (newtype == typeof(DateTime))
                        {
                            DateTime.TryParse(row[columnname].ToString(), out DateTime el);
                            row["temperary"] = el;
                        }
                        else
                            row["temperary"] = Convert.ChangeType(row[columnname].ToString(), newtype);
                    }
                    catch (Exception err)
                    {
                        LogManager.GetCurrentClassLogger().Error(err.Message);
                    }
                }
                var index = table.Columns.IndexOf(columnname);
                table.Columns.Remove(columnname);
                newcolumn.ColumnName = columnname;
                newcolumn.SetOrdinal(index);
            }
            catch (Exception err)
            {
                LogManager.GetCurrentClassLogger().Error(err.Message);
                return false;
            }

            return true;
        }
    }
}
