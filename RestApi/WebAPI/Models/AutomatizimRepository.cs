using DbCore;
using DbCore.DbAdmin;
using DbCore.DbCRM;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DbCore.DbImporte;
using DbCore.IMBUtils.Extensions;
using DbCore.IMBUtils.Logging;
using DbCore.IMBUtils.DataBase;
using AlphaWebCommon.Logging;
using DbCore.IMBUtils.Messages;

namespace RestApi.WebAPI.Models
{
    public class AutomatizimRepository
    {
        public static DbCore.clsMesazh importAutomatikShitjeBlerje(string templateImporti, int idNdermarrje, int idPerdorues)
        {
            try
            {
                if (String.IsNullOrEmpty(templateImporti))
                    return new DbCore.clsMesazh(false, "Mungon template i importit! Nuk u importua asnje rresht!");
                DataTable gabime = new DataTable();
                gabime.Columns.Add("Kodi");
                gabime.Columns.Add("Gabimi");
                gabime.Columns.Add("Rreshti");
                clsKonfigImporti konfigImp = new clsKonfigImporti(templateImporti, idNdermarrje);
                if (konfigImp.Id == 0)
                    return new DbCore.clsMesazh(false, String.Format("Template i importit ( {0} ) nuk ekziston! Nuk u importua asnje rresht!", konfigImp.Emer));

                DbCore.DbAdmin.clsNdermarrjeViti ndervit = new DbCore.DbAdmin.clsNdermarrjeViti();
                int idNdermVit = ndervit.merrNdermarrjeVitSipasNdermarjesDheDatesAktuale(idNdermarrje);
                DbCore.clsMesazh mesazh = DbCore.clsFunksione.importDokumenteshNgaWS(konfigImp, idNdermarrje, idPerdorues, ref gabime, idNdermVit);
                if (gabime.Rows.Count > 0)
                {
                    DbCore.DbAdmin.clsKokaErrorImporti koka = new DbCore.DbAdmin.clsKokaErrorImporti(0, "Nga importi i " + konfigImp.Emer, konfigImp.Kategoria, idNdermarrje, idPerdorues);
                    koka.ColTrupi.mbushErrorImportiNgaProgrami(gabime);
                    mesazh = koka.ruajErrorImporti();
                    if (!mesazh.Status)
                        return new DbCore.clsMesazh(true, string.Format("( {0} ) Ndodhi nje gabim gjate importit dhe shkrimit te gabimeve ne log!", konfigImp.Emer));
                    else
                        return new DbCore.clsMesazh(false, string.Format("( {0} ) Nuk u importuan te gjthe rreshtat! {1} rreshta kane gabime ne dokument! Kontrollo importin manual!", konfigImp.Emer, gabime.Rows.Count));
                }
                else
                    return new DbCore.clsMesazh(true, string.Format("( {0} ) " + mesazh.PershkrimMesazhi, konfigImp.Emer));
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
                return new DbCore.clsMesazh(false, ex.Message);
            }
        }

        /// <summary>
        /// Funksion per eksportin automatik
        /// </summary>
        /// <param name="templateEksporti"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermVit"></param>
        /// <param name="rm"></param>
        /// <param name="ci"></param>
        /// <returns></returns>
        public static DbCore.clsMesazh eksportAutomatikDokumentesh(string templateEksporti, int idNdermarrje, int idPerdorues)
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
                DataTable table = new DataTable();
                bool artikujSet = konfigEksporti.Kategoria.ToString().EqualsAnyIgnoreCase("1", "2", "6") && col.FirstOrDefault(x => x.KodKontrolli == "Artikulli Set").Visible == true;

                bool serialeUnike = konfigEksporti.Kategoria.ToString().EqualsAnyIgnoreCase("1", "2", "6") && col.FirstOrDefault(x => x.KodKontrolli.EqualsAnyIgnoreCase("Seriali Unik Kryesor", "Seriali unik dytesor")).Visible == true;
                int idSuperKategori = DbCore.DbRegjistrim.clsKategoriNivelDok.mbushIDSuperKategoriNivDok(int.Parse(konfigEksporti.Kategoria.ToString()));
                switch (konfigEksporti.Kategoria.ToString())
                {
                    case "1":
                        table = DbCore.DbRegjistrim.colKokaShitje.merrShitjePerEksport(idNdermarrje, idPerdorues, konfigEksporti.Kategoria, idNdermVit, 2, konfigEksporti.EmerTabKoka, primaryKey, String.Empty, konfigEksporti.MerrDokTeModifikuar, konfigEksporti.MerrDokTeFshire, false,artikujSet,serialeUnike, "", true);
                        kodKontrolliTrupi = "IdRreshtiShitje";
                        break;
                    case "2":
                        table = DbCore.DbRegjistrim.colKokaShitje.merrShitjePerEksport(idNdermarrje, idPerdorues, 2, idNdermVit, 2, konfigEksporti.EmerTabKoka, primaryKey, String.Empty, konfigEksporti.MerrDokTeModifikuar, konfigEksporti.MerrDokTeFshire, false,artikujSet,serialeUnike, "", true);
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
                if (konfigEksporti.Filtri != 0)
                {
                    filterPerDataTable = DbCore.DbAdmin.clsFiltraExporti.ktheFilterPerDataSet(konfigEksporti.Filtri);
                    DataRow[] rreshtat = table.Select(filterPerDataTable);
                    if (rreshtat.Count() > 0)
                    {
                        teDhenaPerEksport = rreshtat.GetDataTable(table);
                    }
                    else
                        return new DbCore.clsMesazh(true, String.Format("( {0} ) Nuk ka asnje rresht per te exportuar!", konfigEksporti.Emer));
                }
                DataTable koka = DbCore.clsFunksione.ktheDataTableMeKokeDokumentesh(col, teDhenaPerEksport, ndermarrja.NdermarrjeKodi);
                DataTable trupi = new DataTable();
                if (idSuperKategori == 2)
                    trupi = DbCore.clsFunksione.ktheDataTableMeTrupaDokumentesh(col, teDhenaPerEksport, kodKontrolliTrupi, konfigEksporti.Kategoria);
                DbCore.clsMesazh mesazh = DbCore.DbImporte.colEksportSQL.ruajDokumentaNeTabeleEksporti(koka, trupi, col, konfigEksporti.EmerTabKoka, konfigEksporti.EmerTabTrupi, konfigEksporti.Kategoria, konfigEksporti.EmerTabRec, new DataTable(), idSuperKategori);
                mesazh.PershkrimMesazhi = String.Format("({0}) {1}", konfigEksporti.Emer, mesazh.PershkrimMesazhi);
                return mesazh;
            }
            catch (DbCore.MyException gabimi)
            {
               ImbLogger.Error(gabimi);
                return new DbCore.clsMesazh(false, gabimi.Message);
            }
            catch (Exception err)
            {
               ImbLogger.Error(err);
                return new DbCore.clsMesazh(false, "Ndodhi nje gabim gjate eksportit!");
            }
        }

        public static DbCore.clsMesazh NjoftoMeEmailPerTakimetEPanisura(string KODNDERMARRJE, string EMAILET)
        {
            string subject = "Lajmerim per agjentet";
            DateTime data = DateTime.Now;
            DateTime OraLimit = new DateTime(data.Year, data.Month, data.Day, 8, 0, 0);//ora default
            try
            {
                var oraLejuarKonfig = DateTime.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["OraLimitPerTakimetCRM"]);
                OraLimit = new DateTime(data.Year, data.Month, data.Day, oraLejuarKonfig.Hour, oraLejuarKonfig.Minute, 0);
            }
            catch (Exception ex)
            {
                ImbLogger.Error(ex);
            }

            clsMesazh mesazh = new clsMesazh(true, "Te gjithe agjentet jane duke kryer takimet sipas orarit te percaktuar!");

            colEmail emails = new colEmail(subject, DateTime.Now);

            if (emails.Count < 1)
            {
                string[] emailet = EMAILET.Split(';');
                int idNdermarrje = clsNdermarrje.ktheIdNdermarrje(KODNDERMARRJE);

                if (idNdermarrje == 0)
                {
                    return new clsMesazh(false, string.Format("Ndermarrja me kodin : '{0}' nuk ekziston!"));
                }

                if (emailet.Length == 0)
                {
                    return new clsMesazh(false, string.Format("Lista e emaileve eshte bosh!"));
                }

                colAgjenteShitje agjentPanisurAkoma = new colAgjenteShitje();
                colAgjenteShitje agjentTePaKontaktuar = new colAgjenteShitje();
                colAgjenteShitje agjentJashteRrezes = new colAgjenteShitje();

                colSkeduler takimetEPanisurKesajDate = new colSkeduler(KODNDERMARRJE, data);
                List<int> idUnike = takimetEPanisurKesajDate.GroupBy(x => x.IdPerdoruesi).Select(x => x.Key).ToList();

                colTakimePerKontroll listaETakimeve = new colTakimePerKontroll(KODNDERMARRJE, data);

                foreach (clsTakimePerKontroll takim in listaETakimeve)
                {
                    takim.UpdateStatusLexuar();

                    if (string.IsNullOrWhiteSpace(takim.KoordinataFillimTakimi))
                    {
                        agjentPanisurAkoma.Add(new clsAgjentShitje(takim.IdAgjentShitje));
                    }
                    else if (!takim.BrendaRrezes && !string.IsNullOrWhiteSpace(takim.KoordinataFillimTakimi) && takim.DtKrijimi.Value <= OraLimit)
                    {

                        agjentJashteRrezes.Add(new clsAgjentShitje(takim.IdAgjentShitje));
                    }
                    else if (!string.IsNullOrWhiteSpace(takim.KoordinataFillimTakimi) && takim.DtKrijimi.Value > OraLimit)
                    {
                        agjentPanisurAkoma.Add(new clsAgjentShitje(takim.IdAgjentShitje));
                    }
                }

                List<int> IdAgjenteshTeKontaktuar = listaETakimeve.Select(x => x.IdPerdoruesi).ToList();
                List<int> agjentetEPakontaktuar = idUnike.Except(IdAgjenteshTeKontaktuar).ToList<int>();

                foreach (int idPeroruesMobile in agjentetEPakontaktuar)
                {
                    clsAgjentShitje agjent = clsAgjentShitje.MerrAgjentShitjeSipasIdPerdoruesModile(idNdermarrje, idPeroruesMobile);
                    agjentTePaKontaktuar.Add(agjent);
                    clsTakimePerKontroll takimIKontrolluar = new clsTakimePerKontroll
                    {
                        Perdorues = agjent.EmriAgjentShitje,
                        KodNdermarrje = KODNDERMARRJE,
                        Lexuar = true,
                        Uuid = "ALPHAWEB_CRM",
                        RegID = "ALPHAWEB_CRM",
                    };

                    takimIKontrolluar.Ruaj();
                }
                if (agjentPanisurAkoma.Count > 0 || agjentTePaKontaktuar.Count > 0 || agjentJashteRrezes.Count > 0)

                    mesazh = DbCore.EmailComposer.DergoEmailListAgjenteshProblematik(emailet, HttpContext.Current.Request, agjentPanisurAkoma, agjentTePaKontaktuar, agjentJashteRrezes, subject, idNdermarrje);
            }
            else
            {
                var tePaderguar = emails.Where(x => x.Status == statusEmail.perDergim).ToList<clsEmail>();

                if (tePaderguar.Count > 0)
                    mesazh = DbCore.EmailComposer.RidergoEmailListAgjenteshProblematik(tePaderguar);
                else
                    mesazh = new clsMesazh(true, "Emaili eshte derguar njehere!");
            }

            return mesazh;
        }
        public static DbCore.clsMesazh DergoEmailRaportCRM(string KODNDERMARRJE, string USERNAME, string EMAILET)
        {
            try
            {
                int idGjuha = 0;
                DateTime data = DateTime.Now;
                int idNdermarrje = clsNdermarrje.ktheIdNdermarrje(KODNDERMARRJE);

                if (string.IsNullOrEmpty(USERNAME)) throw new MyException("username eshte bosh!");



                if (idNdermarrje == 0) throw new MyException(string.Format("Ndermarrja me kodin : '{0}' nuk ekziston!"));

                int idPerdoruesi = clsPerdorues.ktheIdPerdoruesSipasUsername(USERNAME, idNdermarrje);

                if (idPerdoruesi == 0) throw new MyException($"Perdoruesi me username {USERNAME} nuk ekziston!");

                return DbCore.EmailComposer.dergoEmailRaportinCRM(HttpContext.Current.Request, idPerdoruesi, idNdermarrje, idGjuha, MessagesResource.CurrentResourceManager, MessagesResource.KtheCultureInfo(idGjuha), data);
            }
            catch (Exception ex)
            {
                return new clsMesazh(false, ex.Message);
            }
        }
        public static DbCore.clsMesazh DergoEmailRaportKartolinaDitelindjes(string KODNDERMARRJE, string USERNAME)
        {
            try
            {
                int idGjuha = 0;
                DateTime data = DateTime.Now;
                int idNdermarrje = clsNdermarrje.ktheIdNdermarrje(KODNDERMARRJE);

                if (string.IsNullOrEmpty(USERNAME)) throw new MyException("username eshte bosh!");

                if (idNdermarrje == 0) throw new MyException(string.Format("Ndermarrja me kodin : '{0}' nuk ekziston!"));

                int idPerdoruesi = clsPerdorues.ktheIdPerdoruesSipasUsername(USERNAME, idNdermarrje);

                if (idPerdoruesi == 0) throw new MyException($"Perdoruesi me username {USERNAME} nuk ekziston!");

                return DbCore.EmailComposer.dergoEmailRaportinKartolinaDitelindjes(HttpContext.Current.Request, idPerdoruesi, idNdermarrje, idGjuha, MessagesResource.CurrentResourceManager, MessagesResource.KtheCultureInfo(idGjuha));
            }
            catch (Exception ex)
            {
                return new clsMesazh(false, ex.Message);
            }
        }
        
        public static object MerrLogeSistemiDataTable(DateTime dataNga, DateTime dataDeri, string moduli, string verbosity)
        {
            string folderi = LogHelper.MerrFolderPerLoget(HttpContext.Current.Server.MapPath("../../log"), moduli);

            DataTable loget = LogHelper.KrijoTabeleLogesh();

            try
            {
                List<string> dirs = LogHelper.MerrSkedaret(dataNga, dataDeri, verbosity, folderi, moduli == string.Empty);
                if (!dirs.Any())
                    return new { dataSource = loget, kaTeDhena = false, mesazh = "Nuk ka loge per kerkimin e bere." };
                foreach (string dir in dirs)
                {
                    string line;
                    System.IO.StreamReader file = new System.IO.StreamReader(dir);
                    var lloji = dir.Substring(dir.IndexOf("log\\") + 4, dir.LastIndexOf("\\") - dir.LastIndexOf("log\\") - 3);
                    var fileName = dir.Substring(dir.LastIndexOf("\\") + 1, dir.Length - dir.LastIndexOf("\\") - 1);
                    var tipi = fileName.Substring(fileName.IndexOf(".") + 1, fileName.LastIndexOf(".") - fileName.IndexOf(".") - 1);
                    while ((line = file.ReadLine()) != null)
                    {
                        LogHelper.ShtoRresht(line, loget, lloji, tipi);
                    }
                    file.Close();
                }
            }
            catch (Exception e)
            {
                return new { dataSource = loget, kaTeDhena = true, mesazh = e.ToString() };
            }
            return new { dataSource = loget, kaTeDhena = true, mesazh = "" };
        }
        
        internal static object RuajNivelVerbosity(int verbosity, string moduli)
        {
            try
            {
                clsMesazh mesazh = clsFunksione.NdryshoNivelVerbosity(verbosity, moduli);
                clsFunksione.konfiguroNLog();
                return new { mesazh = mesazh.PershkrimMesazhi, error = false };
            }
            catch(Exception e)
            {
                return new { mesazh = e.ToString(), error = true };
            }
        }

    }
}
