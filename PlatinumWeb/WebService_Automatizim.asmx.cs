using DbCore;
using DbCore.DbAdmin;
using DbCore.DbCRM;
using DbCore.DbRegjistrim;
using DbCore.DbShare;
using DbCore.IMBUtils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using DbCore.DbImporte;
using DbCore.IMBUtils.Extensions;
using RestApi.WebAPI.Models;
using DbCore.IMBUtils.DataBase;
using AlphaWeb.Core.SharedKernel;
using DbCore.IMBUtils.Messages;
using AlphaWeb.Infrastructure.Data.AdoNet;
using Newtonsoft.Json;
using DbCore.DbKontabiliteti;
using DevExpress.Web;
using DbCore.DbArkaBanka;
using DbCore.DbInventari;
using Newtonsoft.Json.Linq;
using DevExpress.XtraEditors.Filtering.Templates;
using AlphaWebReports;
using DocumentFormat.OpenXml.Spreadsheet;
using System.Net.PeerToPeer;

namespace PlatinumWeb
{
    /// <summary>
    /// Summary description for WebService_Automatizim
    /// </summary>
    [WebService(Namespace = "http://alpha.al/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
    // [System.Web.Script.Services.ScriptService]
    public class WebService_Automatizim : System.Web.Services.WebService
    {
        private static NLog.Logger logu = NLog.LogManager.GetCurrentClassLogger();
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public DbCore.clsMesazh ndryshoCmimeAutomatikTollona( int idperdoruesi, int idNdermarrje, DateTime date, string connectionString)
        {
            MyConnectionsManager.SetSelectedConNameServer(connectionString);
            string ip = HttpContext.Current.Request.UserHostAddress;
            if (ip.Equals(System.Configuration.ConfigurationManager.AppSettings["ipEkzekutoWebServiceAuto"]))
            {
                //DbCore.DbAdmin.clsPerdorues perdorues = new DbCore.DbAdmin.clsPerdorues(idperdoruesi);
                DbCore.DbAdmin.clsNdermarrje ndermarje = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
                //CultureInfo cultinf;
                //switch (perdorues.IdGjuha)
                //{
                //    case 0: cultinf = new CultureInfo("sq-AL"); break;
                //    case 1: cultinf = new CultureInfo("en-US"); break;
                //    default: throw new mySessionNewException("CultureInfo ska vlere");
                //}
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

                //int idGjuha = perdorues.IdGjuha;
                int idGjuha = 0; // mySessionObjects.ktheGjuhe(Session);
                CultureInfo cultinf = MessagesResource.KtheCultureInfo(idGjuha);
                bool eshteOwn = ndermarje.OwnShop;
                DbCore.DbAdmin.clsNdermarrjeViti ndervit = new DbCore.DbAdmin.clsNdermarrjeViti();
                int idndermvit = ndervit.merrNdermarrjeVitSipasNdermarjesDheDatesAktuale(idNdermarrje);

                DbCore.clsMesazh mesazh = DbCore.DbTollona.colShitjeMeSerial.merrShitjeMeSerialKonsumuarandryshoCmimin(date.AddDays(-1));
                if (mesazh.Status)
                    mesazh = ekzekutoImportAutomatikTollona(idperdoruesi, idNdermarrje, date, connectionString);
                if (mesazh.Status)
                    mesazh = ekzekutoImportAutomatikTollonaElektronik(idperdoruesi, idNdermarrje, date, connectionString);
                return mesazh;
            }
            return new DbCore.clsMesazh(false, "Kerkesa nuk vjen nga serveri i duhur!");
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public DbCore.clsMesazh ekzekutoImportAutomatikTollona(int idperdoruesi, int idNdermarrje, DateTime date, string connectionString)
        {
            MyConnectionsManager.SetSelectedConNameServer(connectionString);
            string ip = HttpContext.Current.Request.UserHostAddress;
            if (ip.Equals(System.Configuration.ConfigurationManager.AppSettings["ipEkzekutoWebServiceAuto"]))
            {
                //DbCore.DbAdmin.clsPerdorues perdorues = new DbCore.DbAdmin.clsPerdorues(idperdoruesi);
                DbCore.DbAdmin.clsNdermarrje ndermarje = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
                //CultureInfo cultinf;
                //switch (perdorues.IdGjuha)
                //{
                //    case 0: cultinf = new CultureInfo("sq-AL"); break;
                //    case 1: cultinf = new CultureInfo("en-US"); break;
                //    default: throw new mySessionNewException("CultureInfo ska vlere");
                //}
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

                //int idGjuha = perdorues.IdGjuha;
                int idGjuha = 0; // mySessionObjects.ktheGjuhe(Session);
                CultureInfo cultinf = MessagesResource.KtheCultureInfo(idGjuha);
                bool eshteOwn = ndermarje.OwnShop;
                DbCore.DbAdmin.clsNdermarrjeViti ndervit = new DbCore.DbAdmin.clsNdermarrjeViti();
                int idndermvit = ndervit.merrNdermarrjeVitSipasNdermarjesDheDatesAktuale(idNdermarrje);

                DataTable dt = DbCore.DbTollona.colShitjeMeSerial.merrShitjeMeSerialKonsumuaraPerImport(idNdermarrje, date);

                int rreshta = dt.Rows.Count;
                DataTable err = new DataTable();
                err.Columns.Add("Kodi");
                err.Columns.Add("Gabimi");
                err.Columns.Add("Rreshti");
                DataTable rreshtaok = new DataTable();
                DataTable rreshtajoOk = new DataTable();
                rreshtajoOk = dt.Clone();
                rreshtaok = dt.Copy();
                DbCore.DbTollona.colShitjeMeSerial.kontrolloShitjeTollona(idNdermarrje, rreshtaok, err, rreshtajoOk, true, 0, cultinf, rm, idGjuha, eshteOwn, idperdoruesi, idndermvit);
                DbCore.DbAdmin.clsKokaErrorImporti koka = new DbCore.DbAdmin.clsKokaErrorImporti(0, "Nga importi i shitjeve me tollona", 1, idNdermarrje, idperdoruesi);
                koka.ColTrupi.mbushErrorImportiNgaProgrami(err);
                DbCore.clsMesazh mesazh = koka.ruajErrorImporti();
                return mesazh;
            }
            return new DbCore.clsMesazh(false, "Kerkesa nuk vjen nga serveri i duhur!");
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public DbCore.clsMesazh importAutomatikShitjeBlerje(string templateImporti, int idNdermarrje, int idPerdorues, string connectionString)
        {
            try
            {
                MyConnectionsManager.SetSelectedConNameServer(connectionString);
                if (String.IsNullOrEmpty(templateImporti))
                    return new DbCore.clsMesazh(false, "Mungon template i importit! Nuk u importua asnje rresht!");
                DataTable gabime = new DataTable();
                gabime.Columns.Add("Kodi");
                gabime.Columns.Add("Gabimi");
                gabime.Columns.Add("Rreshti");
                clsKonfigImporti konfigImp = new clsKonfigImporti(templateImporti, idNdermarrje);
                if (konfigImp.Id == 0)
                    return new DbCore.clsMesazh(false, String.Format("Template i importit ( {0} ) nuk ekziston! Nuk u importua asnje rresht!", konfigImp.Emer));

                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                int idGjuha = 0;
                CultureInfo cultinf = MessagesResource.KtheCultureInfo(idGjuha);

                DbCore.DbAdmin.clsNdermarrjeViti ndervit = new DbCore.DbAdmin.clsNdermarrjeViti();
                int idNdermVit = ndervit.merrNdermarrjeVitSipasNdermarjesDheDatesAktuale(idNdermarrje);
                DbCore.clsMesazh mesazh = DbCore.clsFunksione.importDokumenteshNgaWS(konfigImp, idNdermarrje, idPerdorues, ref gabime, idNdermVit);
                if (gabime.Rows.Count > 0)
                {
                    DbCore.DbAdmin.clsKokaErrorImporti koka = new DbCore.DbAdmin.clsKokaErrorImporti(0, "Nga importi i " + konfigImp.Emer, konfigImp.Kategoria, idNdermarrje, idPerdorues);
                    koka.ColTrupi.mbushErrorImportiNgaProgrami(gabime);
                    mesazh = koka.ruajErrorImporti();
                    if (!mesazh.Status)
                        return new DbCore.clsMesazh(true, String.Format("( {0} ) Ndodhi nje gabim gjate importit dhe shkrimit te gabimeve ne log!", konfigImp.Emer));
                    else
                        return new DbCore.clsMesazh(false, String.Format("( {0} ) Nuk u importuan te gjthe rreshtat! {1} rreshta kane gabime ne dokument! Kontrollo importin manual!", konfigImp.Emer, gabime.Rows.Count));
                }
                else
                    return new DbCore.clsMesazh(true, String.Format("( {0} ) " + mesazh.PershkrimMesazhi, konfigImp.Emer));
            }
            catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(ex.Message);
                return new DbCore.clsMesazh(false, ex.Message);
            }
        }

        public DbCore.clsMesazh eksportAutomatikDokumentesh(string templateEksporti, int idNdermarrje, int idPerdorues, bool grupoTrupDokumenti, bool hiqRreshtaKomisioni, string connectionString)
        {
            try
            {
                MyConnectionsManager.SetSelectedConNameServer(connectionString);
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
                int idSuperKategori = DbCore.DbRegjistrim.clsKategoriNivelDok.mbushIDSuperKategoriNivDok(int.Parse(konfigEksporti.Kategoria.ToString()));


                bool artikujSet = konfigEksporti.Kategoria.ToString().EqualsAnyIgnoreCase("1", "2", "6") && col.FirstOrDefault(x => x.KodKontrolli == "Artikulli Set").Visible == true;

                bool serialeUnike = konfigEksporti.Kategoria.ToString().EqualsAnyIgnoreCase("1", "2", "6") && col.FirstOrDefault(x => x.KodKontrolli.EqualsAnyIgnoreCase("Seriali Unik Kryesor", "Seriali unik dytesor")).Visible == true;

                switch (konfigEksporti.Kategoria.ToString())
                {
                    case "1":
                        if (grupoTrupDokumenti)
                            table = DbCore.DbRegjistrim.colKokaShitje.merrShitjeTeGrupuaraPerEksport(idNdermarrje, idPerdorues, konfigEksporti.Kategoria, idNdermVit, 2, konfigEksporti.EmerTabKoka, primaryKey, String.Empty);
                        else
                            table = DbCore.DbRegjistrim.colKokaShitje.merrShitjePerEksport(idNdermarrje, idPerdorues, konfigEksporti.Kategoria, idNdermVit, 2, konfigEksporti.EmerTabKoka, primaryKey, String.Empty, konfigEksporti.MerrDokTeModifikuar, konfigEksporti.MerrDokTeFshire, hiqRreshtaKomisioni, artikujSet, serialeUnike,"",true);
                        kodKontrolliTrupi = "IdRreshtiShitje";
                        break;
                    case "2":
                        if (grupoTrupDokumenti)
                            table = DbCore.DbRegjistrim.colKokaShitje.merrShitjeTeGrupuaraPerEksport(idNdermarrje, idPerdorues, 2, idNdermVit, 2, konfigEksporti.EmerTabKoka, primaryKey, String.Empty);
                        else
                            table = DbCore.DbRegjistrim.colKokaShitje.merrShitjePerEksport(idNdermarrje, idPerdorues, 2, idNdermVit, 2, konfigEksporti.EmerTabKoka, primaryKey, String.Empty, konfigEksporti.MerrDokTeModifikuar, konfigEksporti.MerrDokTeFshire, hiqRreshtaKomisioni, artikujSet, serialeUnike, "", true);
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
            } catch (DbCore.MyException gabimi)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(gabimi.Message);
                return new DbCore.clsMesazh(false, gabimi.Message);
            } catch (Exception err)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(err.Message);
                return new DbCore.clsMesazh(false, "Ndodhi nje gabim gjate eksportit!");
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
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public DbCore.clsMesazh eksportAutomatikDokumentesh(string templateEksporti, int idNdermarrje, int idPerdorues, string connectionString)
        {
            return eksportAutomatikDokumentesh( templateEksporti, idNdermarrje, idPerdorues, false, false, connectionString);
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public DbCore.clsMesazh eksportAutomatikDokumentashPaKomision(string templateEksporti, int idNdermarrje, int idPerdorues,bool grupoTrupDokumenti, bool hiqRreshtaKomisioni, string connectionString)
        {
            return eksportAutomatikDokumentesh(templateEksporti, idNdermarrje, idPerdorues, grupoTrupDokumenti, hiqRreshtaKomisioni,connectionString);
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public DbCore.clsMesazh ekzekutoImportAutomatikTollonaLeter(int idperdoruesi, int idNdermarrje, DateTime date, string connectionString)
        {
            MyConnectionsManager.SetSelectedConNameServer(connectionString);
            string ip = HttpContext.Current.Request.UserHostAddress;
            if (ip.Equals(System.Configuration.ConfigurationManager.AppSettings["ipEkzekutoWebServiceAuto"]))
            {
                //DbCore.DbAdmin.clsPerdorues perdorues = new DbCore.DbAdmin.clsPerdorues(idperdoruesi);
                DbCore.DbAdmin.clsNdermarrje ndermarje = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
                //CultureInfo cultinf;
                //switch (perdorues.IdGjuha)
                //{
                //    case 0: cultinf = new CultureInfo("sq-AL"); break;
                //    case 1: cultinf = new CultureInfo("en-US"); break;
                //    default: throw new mySessionNewException("CultureInfo ska vlere");
                //}
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

                //int idGjuha = perdorues.IdGjuha;
                int idGjuha = 0; // mySessionObjects.ktheGjuhe(Session);
                CultureInfo cultinf = MessagesResource.KtheCultureInfo(idGjuha);
                bool eshteOwn = ndermarje.OwnShop;
                DbCore.DbAdmin.clsNdermarrjeViti ndervit = new DbCore.DbAdmin.clsNdermarrjeViti();
                int idndermvit = ndervit.merrNdermarrjeVitSipasNdermarjesDheDatesAktuale(idNdermarrje);

                DataTable dt = DbCore.DbTollona.colTollonaLeter.merrTollonaLeterKonsumuaraPerImport(idNdermarrje, date);

                int rreshta = dt.Rows.Count;
                DataTable err = new DataTable();
                err.Columns.Add("Kodi");
                err.Columns.Add("Gabimi");
                err.Columns.Add("Rreshti");
                DataTable rreshtaok = new DataTable();
                DataTable rreshtajoOk = new DataTable();
                rreshtajoOk = dt.Clone();
                rreshtaok = dt.Copy();
                DbCore.DbTollona.colTollonaLeter.kontrolloShitjeTollona(idNdermarrje, rreshtaok, err, rreshtajoOk, true, 0, cultinf, rm, idGjuha, eshteOwn, idperdoruesi, idndermvit);
                DbCore.DbAdmin.clsKokaErrorImporti koka = new DbCore.DbAdmin.clsKokaErrorImporti(0, "Nga importi i shitjeve me tollona", 1, idNdermarrje, idperdoruesi);
                koka.ColTrupi.mbushErrorImportiNgaProgrami(err);
                DbCore.clsMesazh mesazh = koka.ruajErrorImporti();
                return mesazh;
            }
            return new DbCore.clsMesazh(false, "Kerkesa nuk vjen nga serveri i duhur!");
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public DbCore.clsMesazh ekzekutoImportAutomatikTollonaElektronik(int idperdoruesi, int idNdermarrje, DateTime date, string connectionString)
        {
            MyConnectionsManager.SetSelectedConNameServer(connectionString);
            string ip = HttpContext.Current.Request.UserHostAddress;
            if (ip.Equals(System.Configuration.ConfigurationManager.AppSettings["ipEkzekutoWebServiceAuto"]))
            {
                //DbCore.DbAdmin.clsPerdorues perdorues = new DbCore.DbAdmin.clsPerdorues(idperdoruesi);
                DbCore.DbAdmin.clsNdermarrje ndermarje = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
                //CultureInfo cultinf;
                //switch (perdorues.IdGjuha)
                //{
                //    case 0: cultinf = new CultureInfo("sq-AL"); break;
                //    case 1: cultinf = new CultureInfo("en-US"); break;
                //    default: throw new mySessionNewException("CultureInfo ska vlere");
                //}
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

                //int idGjuha = perdorues.IdGjuha;
                int idGjuha = mySessionObjects.ktheGjuhe(Session);
                CultureInfo cultinf = MessagesResource.KtheCultureInfo(idGjuha);
                bool eshteOwn = ndermarje.OwnShop;
                DbCore.DbAdmin.clsNdermarrjeViti ndervit = new DbCore.DbAdmin.clsNdermarrjeViti();
                int idndermvit = ndervit.merrNdermarrjeVitSipasNdermarjesDheDatesAktuale(idNdermarrje);

                DataTable dt = DbCore.DbTollona.colTollonaElektronik.merrTollonaElektronikKonsumuaraPerImport(idNdermarrje, date);

                int rreshta = dt.Rows.Count;
                DataTable err = new DataTable();
                err.Columns.Add("Kodi");
                err.Columns.Add("Gabimi");
                err.Columns.Add("Rreshti");
                DataTable rreshtaok = new DataTable();
                DataTable rreshtajoOk = new DataTable();
                rreshtajoOk = dt.Clone();
                rreshtaok = dt.Copy();
                DbCore.DbTollona.colTollonaElektronik.kontrolloShitjeTollona(idNdermarrje, rreshtaok, err, rreshtajoOk, true, 0, idGjuha, eshteOwn, idperdoruesi, idndermvit);
                DbCore.DbAdmin.clsKokaErrorImporti koka = new DbCore.DbAdmin.clsKokaErrorImporti(0, "Nga importi i shitjeve me tollona", 1, idNdermarrje, idperdoruesi);
                koka.ColTrupi.mbushErrorImportiNgaProgrami(err);
                DbCore.clsMesazh mesazh = koka.ruajErrorImporti();
                return mesazh;
            }
            return new DbCore.clsMesazh(false, "Kerkesa nuk vjen nga serveri i duhur!");
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public DbCore.clsMesazh ekzekutoImportAutomatikTollonaElektronikSpecifik(int idperdoruesi, int idNdermarrje, DateTime date, string connectionString)
        {
            MyConnectionsManager.SetSelectedConNameServer(connectionString);
            string ip = HttpContext.Current.Request.UserHostAddress;
            if (ip.Equals(System.Configuration.ConfigurationManager.AppSettings["ipEkzekutoWebServiceAuto"]))
            {
                //DbCore.DbAdmin.clsPerdorues perdorues = new DbCore.DbAdmin.clsPerdorues(idperdoruesi);
                DbCore.DbAdmin.clsNdermarrje ndermarje = new DbCore.DbAdmin.clsNdermarrje(idNdermarrje);
                //CultureInfo cultinf;
                //switch (perdorues.IdGjuha)
                //{
                //    case 0: cultinf = new CultureInfo("sq-AL"); break;
                //    case 1: cultinf = new CultureInfo("en-US"); break;
                //    default: throw new mySessionNewException("CultureInfo ska vlere");
                //}
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                int idGjuha = 0; // mySessionObjects.ktheGjuhe(Session);
                bool eshteOwn = ndermarje.OwnShop;
                DbCore.DbAdmin.clsNdermarrjeViti ndervit = new DbCore.DbAdmin.clsNdermarrjeViti();
                int idndermvit = ndervit.merrNdermarrjeVitSipasNdermarjesDheDatesAktuale(idNdermarrje);
                DataTable dt = DbCore.DbTollona.colTollonaElektronik.merrTollonaElektronikKonsumuaraPerImportSpecifik(idNdermarrje, date);
                int rreshta = dt.Rows.Count;
                DataTable err = new DataTable();
                err.Columns.Add("Kodi");
                err.Columns.Add("Gabimi");
                err.Columns.Add("Rreshti");
                DataTable rreshtaok = new DataTable();
                DataTable rreshtajoOk = new DataTable();
                rreshtajoOk = dt.Clone();
                rreshtaok = dt.Copy();
                DbCore.DbTollona.colTollonaElektronik.kontrolloShitjeTollonaSpecifik(idNdermarrje, rreshtaok, err, rreshtajoOk, true, 0, rm, idGjuha, eshteOwn, idperdoruesi, idndermvit);
                DbCore.DbAdmin.clsKokaErrorImporti koka = new DbCore.DbAdmin.clsKokaErrorImporti(0, "Nga importi i shitjeve me tollona", 1, idNdermarrje, idperdoruesi);
                koka.ColTrupi.mbushErrorImportiNgaProgrami(err);
                DbCore.clsMesazh mesazh = koka.ruajErrorImporti();
                return mesazh;
            }
            return new DbCore.clsMesazh(false, "Kerkesa nuk vjen nga serveri i duhur!");
        }
        
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public Object[] exportFotoNeMobile(int numerFotosh, int numerChunk, string kodNdermarrje, string  perdorues, string DataFunditLexuar, string connectionString)
        {            
            MyConnectionsManager.SetSelectedConNameServer(connectionString);
            string ArkivaPath = System.Web.HttpContext.Current.Server.MapPath(null);
            return clsArkiva.LexoFototPerNdermarrjenShtuarPasDatesFunditLexuar(numerFotosh, numerChunk, kodNdermarrje, perdorues, DataFunditLexuar, ArkivaPath);
        }


        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public DbCore.clsMesazh importFotoNgaMobile(int IDOBJEKTI, string FOTO, string KODNDERMARRJE, string USERNAME, int lloji, string connectionString)
        {
            try
            {
                MyConnectionsManager.SetSelectedConNameServer(connectionString);
                byte[] fotoNgaMobile = System.Convert.FromBase64String(FOTO);
                if (fotoNgaMobile == null)
                    return new DbCore.clsMesazh(false, "Stringu base64 i fotos eshte bosh ose ka gabime!");

                int idNdermarrje = DbCore.DbAdmin.clsNdermarrje.ktheIdNdermarrje(KODNDERMARRJE);
                var UploadDirectory = "~/Arkiva/" + idNdermarrje + "/";

                if (!Directory.Exists(Server.MapPath(UploadDirectory)))
                {
                    try
                    {
                        Directory.CreateDirectory(Server.MapPath(UploadDirectory));
                    }
                    catch (System.IO.PathTooLongException err)
                    {
                        string mesazhi = "Pathi i imazhit eshte shume i gjate!"; NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                        return new DbCore.clsMesazh(false, mesazhi);
                    }
                    catch (System.IO.DirectoryNotFoundException err)
                    {
                        string mesazhi = "Pathi nuk eshte i sakte ( for example, it is on an unmapped drive)!"; NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                        return new DbCore.clsMesazh(false, mesazhi);
                    }
                    catch (System.IO.IOException err)
                    {
                        //     The directory specified by path is read-only.
                        string mesazhi = "Direktoria Arkiva eshte 'read-only'!"; NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                        return new DbCore.clsMesazh(false, mesazhi);
                    }
                    catch (System.UnauthorizedAccessException err)
                    {
                        //     The caller does not have the required permission.
                        string mesazhi = "Ju nuk keni te drejta te shkruani ne direktorine Arkiva!"; NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                        return new DbCore.clsMesazh(false, mesazhi);
                    }
                    catch (System.ArgumentNullException err)
                    {
                        //     path is null.
                        string mesazhi = "Pathi eshte null!"; NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                        return new DbCore.clsMesazh(false, mesazhi);
                    }
                    catch (System.ArgumentException err)
                    {
                        //     path is a zero-length string, contains only white space, or contains one
                        //     or more invalid characters as defined by System.IO.Path.InvalidPathChars.-or-path
                        //     is prefixed with, or contains only a colon character (:).

                        string mesazhi = "Pathi eshte string bosh, permban vetem hapesire, ose ka karaktere jo te vlefshme!";
                        NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                        return new DbCore.clsMesazh(false, mesazhi);
                    }
                    catch (System.NotSupportedException err)
                    {
                        //     path contains a colon character (:) that is not part of a drive label ("C:\").

                        string mesazhi = "Pathi permban karakterin : !";
                        NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                        return new DbCore.clsMesazh(false, mesazhi);
                    }
                }

                if (lloji != 0)
                {
                    UploadDirectory += lloji + "/";
                    if (!Directory.Exists(Server.MapPath(UploadDirectory)))
                    {
                        Directory.CreateDirectory(Server.MapPath(UploadDirectory));
                    }
                }
                if (IDOBJEKTI != 0)
                {
                    UploadDirectory += IDOBJEKTI + "/";
                    if (!Directory.Exists(Server.MapPath(UploadDirectory)))
                    {
                        Directory.CreateDirectory(Server.MapPath(UploadDirectory));
                    }
                }
                if (!Directory.Exists(Server.MapPath(UploadDirectory)))
                {
                    try
                    {
                        Directory.CreateDirectory(Server.MapPath(UploadDirectory));
                    }
                    catch (System.IO.PathTooLongException err)
                    {
                        //      The specified path, file name, or both exceed the system-defined maximum
                        //      length. For example, on Windows-based platforms, paths must be less than
                        //      248 characters and file names must be less than 260 characters.
                        string mesazhi = "Pathi " + UploadDirectory + " i imazhit eshte shume i gjate!";
                        NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                        return new DbCore.clsMesazh(false, mesazhi);
                    }
                    catch (System.IO.DirectoryNotFoundException err)
                    {
                        //     The specified path is invalid (for example, it is on an unmapped drive).                     

                        string mesazhi = "Pathi " + UploadDirectory + " nuk eshte i sakte (for example, it is on an unmapped drive)!";
                        NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                        return new DbCore.clsMesazh(false, mesazhi);
                    }
                    catch (System.IO.IOException err)
                    {
                        //     The directory specified by path is read-only.

                        string mesazhi = "Direktoria " + UploadDirectory + " eshte 'read-only'!";
                        NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                        return new DbCore.clsMesazh(false, mesazhi);
                    }
                    catch (System.UnauthorizedAccessException err)
                    {
                        //     The caller does not have the required permission.
                        string mesazhi = "Ju nuk keni te drejta te shkruani ne dirketorine " + UploadDirectory + "!";
                        NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                        return new DbCore.clsMesazh(false, mesazhi);
                    }
                    catch (System.ArgumentNullException err)
                    {
                        //     path is null.

                        string mesazhi = "Pathi " + UploadDirectory + " eshte null!";
                        NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                        return new DbCore.clsMesazh(false, mesazhi);
                    }
                    catch (System.ArgumentException err)
                    {
                        //     path is a zero-length string, contains only white space, or contains one
                        //     or more invalid characters as defined by System.IO.Path.InvalidPathChars.-or-path
                        //     is prefixed with, or contains only a colon character (:).

                        string mesazhi = "Pathi " + UploadDirectory + " eshte string bosh, permban vetem hapsire, ose ka karaktere jo te vlefshme!";
                        NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                        return new DbCore.clsMesazh(false, mesazhi);
                    }
                    catch (System.NotSupportedException err)
                    {
                        //     path contains a colon character (:) that is not part of a drive label ("C:\").

                        string mesazhi = "Pathi " + UploadDirectory + " permban karakterin!";
                        NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                        return new DbCore.clsMesazh(false, mesazhi);
                    }
                }
                var conn = clsArkiva.ktheConnString(MyConnectionsManager.GetSelectedConNameServer());
                var filename = USERNAME + IDOBJEKTI.ToString() + DateTime.Now.ToFileTime() + KODNDERMARRJE + ".Jpeg";
                var resultFilePath = UploadDirectory + filename;
                int idPerdorues = DbCore.DbAdmin.clsPerdorues.ktheIdPerdoruesSipasUsername(USERNAME, idNdermarrje);
                try
                {
                    DbCore.DbShare.clsArkiva ark = new DbCore.DbShare.clsArkiva(0, IDOBJEKTI, lloji, "image/Jpeg", resultFilePath.Substring(2), filename, "", 1, idPerdorues, idPerdorues, 0,conn);
                    DbCore.clsMesazh mesazh = ark.ruaj();

                    if (!mesazh.Status)
                        return new DbCore.clsMesazh(false, "Ndodhi nje gabim gjate ruajtjes se arkives!");

                    try
                    {
                        using (MemoryStream ms = new MemoryStream(fotoNgaMobile))
                        {
                            try
                            {
                                System.Drawing.Image foto = System.Drawing.Image.FromStream(ms);
                                try
                                {
                                    string pathFinalZoro = Server.MapPath(resultFilePath);
                                    foto.Save(pathFinalZoro, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    return new DbCore.clsMesazh(true, "Ruajtja perfundoi me sukses!");
                                }
                                catch (System.ArgumentNullException err)
                                {
                                    //     filename is null.
                                    string mesazhi = "Emri i file nuk eshte i sakte!";
                                    NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                                    return new DbCore.clsMesazh(false, mesazhi);
                                }
                                catch (System.Runtime.InteropServices.ExternalException err)
                                {
                                    //     The image was saved with the wrong image format.-or- The image was saved
                                    //     to the same file it was created from.

                                    string mesazhi = "Imazhi " + resultFilePath + " eshte ruajtur me formatin e gabuar!";
                                    NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                                    return new DbCore.clsMesazh(false, mesazhi);
                                }
                            }
                            catch (System.ArgumentException err)
                            {
                                //     The stream does not have a valid image format-or-stream is null.
                                string mesazhi = "Ndodhi nje gabim gjate krijimit te imazhit nga stringu!";
                                NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                                return new DbCore.clsMesazh(false, mesazhi);
                            }
                        }
                    }
                    catch (System.ArgumentNullException err)
                    {
                        string mesazhi = "Ndodhi nje gabim gjate ruajtjes se arkives!";
                        NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                        return new DbCore.clsMesazh(false, mesazhi);
                    }
                }
                catch (Exception err)
                {
                    string mesazhi = "Ndodhi nje gabim gjate krijimit te imazhit nga stringu!";
                    NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                    return new DbCore.clsMesazh(false, mesazhi);
                }
            }
            catch (System.ArgumentNullException err)
            {
                string mesazhi = "Stringu base64 i fotos eshte bosh ose ka gabime!";
                NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                return new DbCore.clsMesazh(false, mesazhi);
            }
            catch (System.FormatException err)
            {
                string mesazhi = "Stringu base64 i fotos nuk eshte 4 karaktere apo ndonje shumefish i 4!";
                NLog.LogManager.GetCurrentClassLogger().Error(mesazhi, err.Message);
                return new DbCore.clsMesazh(false, mesazhi);
            }
        }
       
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public object importKlientProspektNgaMobile(string KODIMOBILE, string EMERTIMIKF, string NIPTKF, string ADRESA, string QYTETIKODI, string CELKF, string KODGRUP1, string KODGRUP2, string KODNDERMARRJE, string USERNAME, string AGJENTSHITJE, string connectionString)
        {
            MyConnectionsManager.SetSelectedConNameServer(connectionString);
            if (String.IsNullOrEmpty(EMERTIMIKF))
                return new { Status = false, KodMobile = KODIMOBILE, KodiWeb = "", Mesazh = "Emertimi eshte bosh!" };
            if (String.IsNullOrEmpty(ADRESA))
                return new { Status = false, KodMobile = KODIMOBILE, KodiWeb = "", Mesazh = "Adresa eshte bosh!" };
            if (String.IsNullOrEmpty(QYTETIKODI))
                return new { Status = false, KodMobile = KODIMOBILE, KodiWeb = "", Mesazh = "Qyteti eshte bosh!" };
            if (String.IsNullOrEmpty(CELKF))
                return new { Status = false, KodMobile = KODIMOBILE, KodiWeb = "", Mesazh = "Cel kf eshte bosh!" };
            if (String.IsNullOrEmpty(KODGRUP1))
                return new { Status = false, KodMobile = KODIMOBILE, KodiWeb = "", Mesazh = "Kodi i grupit te pare eshte bosh!" };


            int idNdermarrje = DbCore.DbAdmin.clsNdermarrje.ktheIdNdermarrje(KODNDERMARRJE);
            int idPerdorues = DbCore.DbAdmin.clsPerdorues.ktheIdPerdoruesSipasUsername(USERNAME, idNdermarrje);
            DbCore.DbKontabiliteti.clsKlientFurnitor kf = new DbCore.DbKontabiliteti.clsKlientFurnitor();
            DbCore.DbAdmin.clsDatabaseAdmin dbAdmin = new DbCore.DbAdmin.clsDatabaseAdmin();
            int idKonfigAmbjente = DbCore.DbShare.clsKonfigurimAmbjenti.ktheIdKonfigurimiMeKod("K", idNdermarrje);

            int idnrautonrdok = clsAtributeTrupi.merrNrAutomatikSipasKontrollitDheKonfigurimit(idKonfigAmbjente, "txtKodi", 123);
            string kodKlienti = DbCore.DbAdmin.clsNrAutom.merrVlerenNrAutomatik(idnrautonrdok, DateTime.Now);

            JavaScriptSerializer serializusi = new JavaScriptSerializer();
            serializusi.MaxJsonLength = 50000000;
            //nqs konfigurimi eshte qe te gjeneroje nr me dok fillestar nr i dokumentit ri i njejte
            //nqs eshte me nr automatik gjejme nr e radhes per kete dokument

            DevExpress.Web.ASPxHiddenField hidden = new DevExpress.Web.ASPxHiddenField();
            List<NrAuto> list = new List<NrAuto>();
            if (!String.IsNullOrEmpty(kodKlienti))//nqs ka nr automatik
            {
                DbCore.DbAdmin.NrAuto nrdokshi = new NrAuto();
                nrdokshi.kodKontrolli = "KodKlientFurnitor";
                nrdokshi.idNrAuto = idnrautonrdok;
                nrdokshi.vlereNrAuto = kodKlienti;

                list.Add(nrdokshi);
                kodKlienti = nrdokshi.vlereNrAuto;
                hidden.Add("KodKlientFurnitor", serializusi.Serialize(nrdokshi));
            }
            else
            {
                //nqs nuk ka nr automatik gjenerojme nje nr duke mare parasyh nr ekzistues dhe i shtojme nje nr
                kodKlienti = "Prospekt_" + USERNAME + "_" + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + "_" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond;
            }

            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
            int idGjuha = DbCore.DbAdmin.clsPerdorues.ktheGjuhePerdoruesi(idPerdorues);
            CultureInfo ci = MessagesResource.KtheCultureInfo(idGjuha);
            DbCore.DbKontabiliteti.colAdresatKlientFurnitor colAdresa = new DbCore.DbKontabiliteti.colAdresatKlientFurnitor();
            DbCore.DbKontabiliteti.clsAdresaKlientFurnitor adr = new DbCore.DbKontabiliteti.clsAdresaKlientFurnitor(0, 0, 1, ADRESA, "");
            colAdresa.Add(adr);
            try
            {
                kf = kf.KrijoKlientFurnitorPerImport(new DbCore.DbKontabiliteti.clsKlientFurnitor(), DbCore.clsFunksione.ktheStringunPaHapesira(kodKlienti, true), "", true, "", "", DbCore.clsFunksione.ktheStringunPaHapesira(EMERTIMIKF, false), "", NIPTKF, QYTETIKODI, "", "", "", CELKF, "", "", "", "", true, "", "", "", "", "", "", 0, 0, 0, "", "", false, 0, 0, 0, "", AGJENTSHITJE, 0, 0, "", 0, idNdermarrje, DateTime.Now.Year, idPerdorues, idKonfigAmbjente, "", "", "", "", KODGRUP1, KODGRUP2, "", "", colAdresa, new DbCore.DbKontabiliteti.colKontaktiKlientFurnitor(), new DbCore.DbKontabiliteti.colBuxhetet(), new colVleraFushaShtese(), true, 0, true, "", idPerdorues, 0, 0, false, rm, ci, "", 0, 0, true, KODIMOBILE, false, false, false, "", "", "", "", "", "", false, "", "", "");
                DbCore.clsMesazh mesazhinv = kf.Ruaj(hidden, false, "", "", "", "");
                if (mesazhinv.Status)
                    return new { Status = true, KodMobile = KODIMOBILE, KodiWeb = kf.KodKlientFurnitor, Mesazh = "Ruajtja perfundoi me sukses!" };
                else
                    throw new DbCore.MyException(mesazhinv.PershkrimMesazhi);
            }
            catch (DbCore.MyException e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                return new { Status = false, KodMobile = KODIMOBILE, KodiWeb = "", Mesazh = e.Message };
            }
            catch (Exception e)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(e.Message);
                return new { Status = false, KodMobile = KODIMOBILE, KodiWeb = "", Mesazh = e.Message };
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public DbCore.clsMesazh NjoftoMeEmailPerTakimetEPanisura(string KODNDERMARRJE, string EMAILET, string connectionString)
        {
            MyConnectionsManager.SetSelectedConNameServer(connectionString);
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
                logu.Error(ex, "Ora nga webconfig nuk u lexua ne rregull!");
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


        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public DbCore.clsMesazh DergoEmailRaportCRM(string KODNDERMARRJE, string USERNAME, string EMAILET, string connectionString)
        {
            try
            {
                MyConnectionsManager.SetSelectedConNameServer(connectionString);
                int idGjuha = 0;
                DateTime data = DateTime.Now;
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                CultureInfo ci = MessagesResource.KtheCultureInfo(idGjuha);
                int idNdermarrje = clsNdermarrje.ktheIdNdermarrje(KODNDERMARRJE);

                if (string.IsNullOrEmpty(USERNAME)) throw new MyException("username eshte bosh!");



                if (idNdermarrje == 0) throw new MyException(string.Format("Ndermarrja me kodin : '{0}' nuk ekziston!"));

                int idPerdoruesi = clsPerdorues.ktheIdPerdoruesSipasUsername(USERNAME, idNdermarrje);

                if (idPerdoruesi == 0) throw new MyException($"Perdoruesi me username {USERNAME} nuk ekziston!");

                return DbCore.EmailComposer.dergoEmailRaportinCRM(HttpContext.Current.Request, idPerdoruesi, idNdermarrje, idGjuha, rm, ci, data);
            }
            catch (Exception ex)
            {
                return new clsMesazh(false, ex.Message);
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public DbCore.clsMesazh DergoEmailRaportKartolinaDitelindjes(string KODNDERMARRJE, string USERNAME, string connectionString)
        {
            try
            {
                MyConnectionsManager.SetSelectedConNameServer(connectionString);
                int idGjuha = 0;
                DateTime data = DateTime.Now;
                ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));
                CultureInfo ci = MessagesResource.KtheCultureInfo(idGjuha);
                int idNdermarrje = clsNdermarrje.ktheIdNdermarrje(KODNDERMARRJE);

                if (string.IsNullOrEmpty(USERNAME)) throw new MyException("username eshte bosh!");

                if (idNdermarrje == 0) throw new MyException(string.Format("Ndermarrja me kodin : '{0}' nuk ekziston!"));

                int idPerdoruesi = clsPerdorues.ktheIdPerdoruesSipasUsername(USERNAME, idNdermarrje);

                if (idPerdoruesi == 0) throw new MyException($"Perdoruesi me username {USERNAME} nuk ekziston!");

                return DbCore.EmailComposer.dergoEmailRaportinKartolinaDitelindjes(HttpContext.Current.Request, idPerdoruesi, idNdermarrje, idGjuha, rm, ci);
            }
            catch (Exception ex)
            {
                return new clsMesazh(false, ex.Message);
            }
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public DbCore.clsMesazh DergoEmailRaportGjenjdaArtMinMaxSipasMag(string KODNDERMARRJE, string USERNAME, string connectionString)
        {
            try
            {
                MyConnectionsManager.SetSelectedConNameServer(connectionString);
                int idGjuha = 0;
                DateTime data = DateTime.Now;
                int idNdermarrje = clsNdermarrje.ktheIdNdermarrje(KODNDERMARRJE);
                if (string.IsNullOrEmpty(USERNAME)) throw new MyException("username eshte bosh!");
                if (idNdermarrje == 0) throw new MyException(string.Format("Ndermarrja me kodin : '{0}' nuk ekziston!"));
                int idPerdoruesi = clsPerdorues.ktheIdPerdoruesSipasUsername(USERNAME, idNdermarrje);
                if (idPerdoruesi == 0) throw new MyException($"Perdoruesi me username {USERNAME} nuk ekziston!");

                return DbCore.EmailComposer.dergoEmailRaportinGjenjdaArtMinMaxsipasMag(HttpContext.Current.Request, idPerdoruesi, idNdermarrje, idGjuha);
            }
            catch (Exception ex)
            {
                return new clsMesazh(false, ex.Message);
            }
        }
        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public clsMesazh ktheListKonfigRaportesh(int idPerdoruesi, int idNdermarrje, int idModuli, string connectionString)
        {
            try
            {
                MyConnectionsManager.SetSelectedConNameServer(connectionString);
                return new clsMesazh(true, clsRaporti.KtheListKonfigRaportesh(idPerdoruesi, idNdermarrje, idModuli));
            }
            catch (Exception ex)
            {
                return new MesazhGabimi(ex.ToString());
            }
        }

        [System.Web.Services.WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = System.Web.Script.Services.ResponseFormat.Json)]
        public clsMesazh ruajKonfigListRaportesh(int idPerdoruesi, int idNdermarrje, int idModuli, string konfigurimi, string connectionString)
        {
            try
            {
                MyConnectionsManager.SetSelectedConNameServer(connectionString);
                return KonfigurimeRepository.ruajKonfigListRaportesh(idPerdoruesi, idNdermarrje, idModuli, konfigurimi);
            }
            catch (Exception ex)
            {
                return new MesazhGabimi(ex.ToString());
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public clsMesazh GjeneroFaturePermbledheseAutomatike(string perdoruesi, string kodNdermarrja, string connectionString, int dtMuaji)
        {
            try
            {
                MyConnectionsManager.SetSelectedConNameServer(connectionString);
                return clsKokaShitje.GjeneroFaturePermbledheseAutomatike(perdoruesi, kodNdermarrja, dtMuaji);
            }
            catch (Exception ex)
            {
                return new clsMesazh(false, ex.ToString());
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public clsMesazh RefreshServerList()
        {
            try
            {
                MyConnectionsManager.RefreshConnectionStringsPool(DbCore.DbAdmin.colServerConnectionStrings.GetAllConnectionStringsAsDictionary(), MyConnectionsManager.ConnStringNameDefault);
                logu.Info($"{HttpContext.Current.Request.UserHostAddress} : RefreshServerList > MyConnectionsManager.RefreshConnectionStringsPool > Server list refreshed successfully");
                return new clsMesazh(true, "Server list refreshed successfully.");
            }
            catch (Exception ex)
            {
                logu.Error($"{HttpContext.Current.Request.UserHostAddress} : RefreshServerList > MyConnectionsManager.RefreshConnectionStringsPool > {ex.ToString()}");
                return new clsMesazh(false, ex.ToString());
            }
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public clsMesazh addInvoice(string obj)
        {
            try
            {


                //json request
                var json = JsonConvert.DeserializeObject(obj);
                var dictionary = (JObject)JsonConvert.DeserializeObject(obj);
                object trupiobj = dictionary["items"].Value<object>();
                Dictionary<string, string> alphaMetadata = JsonConvert.DeserializeObject<Dictionary<string, string>>(dictionary["alphaMetadata"].Value<object>().ToString());//serializusi.DeserializeObject(gridDataObject) as object[];

                DateTime dtDok = DateTime.Parse(dictionary["docDate"].ToString());
                //
                //Vendosja e databazes
                clsMesazh mesazh = clsLogin.setServerFromOrgName(Session.SessionID, alphaMetadata["organization"].ToString());
                if(!mesazh.Status) return new clsMesazh(false, mesazh.PershkrimMesazhi);
                //Dklarim klasash
                clsNdermarrje ndermarrje = new clsNdermarrje(alphaMetadata["ndermarrja"].ToString());
                clsKonfigurimAmbjenti konfigurimAmbjenti = new clsKonfigurimAmbjenti();
                clsKonfigurimAmbjenti konfigurimAmbjentiMag = new clsKonfigurimAmbjenti();
                clsKokaShitje koka = new clsKokaShitje();
                clsKlientFurnitor kf = new clsKlientFurnitor(dictionary["clientCode"].ToString(), ndermarrje.IdNdermarrje);
                clsMonedha monedha= new clsMonedha();
                clsNdermarrjeViti ndermarrjeViti = new clsNdermarrjeViti();
                clsViti viti = new clsViti(ndermarrje.IdNdermarrje, dtDok.Year.ToString());
                clsDegeAdministrative dega = new clsDegeAdministrative(dictionary["businUnitCode"].ToString(), ndermarrje.IdNdermarrje);
                clsPerdorues perdorues = new clsPerdorues(dictionary["userEmail"].ToString().Split('@')[0], dictionary["userEmail"].ToString(), true);
                colTrupiShitje colTrupiShitje = new colTrupiShitje();
                clsNjesiAdministrative magazina = new clsNjesiAdministrative("warehouse",ndermarrje.IdNdermarrje);
                DbCore.DbArkaBanka.clsVeprimBankaKoka veprimebanka = new DbCore.DbArkaBanka.clsVeprimBankaKoka();
                clsKusht kushtamor = new clsKusht(konfigurimAmbjenti.IdKonfigAmbjente, "ZDAM");
                DbCore.DbAsete.colSerialetMagazine serialemag = new DbCore.DbAsete.colSerialetMagazine();
                clsKonfigurimAmbjenti konfamortizimi = new clsKonfigurimAmbjenti(kushtamor.Vlera, perdorues.IdGjuha);
                clsBanka banka = new clsBanka();
                //
                //Initial variables
                DateTime dtFillimi = DateTime.Now;
                DateTime dtMbarimi = DateTime.Now;
                double kursi = 0;
                int idMenyrePAgese = 0;
                string kodMenyrePAgese = "";
                double zbritje = 0.00;
                double perqindjeZbritje = 0.00;
                double totali = 0;
                double tvsh = 0;
                bool zbritjeNeVlere = false;
                double perqindjeZbritjeTotale = 0.00;
                string adresa = "";
                string einStatus = "";
                bool gjeneroDokMag = false;
                string shfaqmesazhapolupe = "";
                string mesazhInfo = "";
                string shfaqmesazhapolupemagazina = "";
                string shfaqmesazhapolupebanka = "";
                string shfaqmesazhapolupeVDK = "";
                string mesazhmevonshem = "";
                string shtimModifikim = "shtim";
                bool printofature = false;
                bool printogarancifature = false;
                bool pageseFature = false;
                bool kontrolloIMEIFifo = false;
                int procesi = 0;
                int tipiEinvoice = 0;
                int operatori = 0;
                string trupi = trupiobj.ToString();
                string iic = dictionary["nslf"].ToString();
                string nivf = dictionary["nivf"].ToString();
                string nivfKthim = "";
                string eic = dictionary["eic"].ToString();
                string tipVetFaturimi = dictionary["typeOfSelfIss"].ToString();
                string nrdok = dictionary["docNo"].ToString();
                string pershkrimi = dictionary["invoiceDescription"].ToString();
                //
                //Parsing values
                DateTime.TryParse(dictionary["startDate"].ToString(),out dtFillimi);
                DateTime.TryParse(dictionary["endDate"].ToString(),out dtMbarimi);
                double.TryParse(dictionary["totalVatValue"].ToString(), out tvsh);
                double.TryParse(dictionary["totalDiscount"].ToString(), out zbritje);
                double.TryParse(dictionary["totalDisscountPercentage"].ToString(), out zbritje);
                double.TryParse(dictionary["totalValue"].ToString(), out totali);
                double.TryParse(dictionary["exchangeRate"].ToString(), out kursi);
                bool.TryParse(dictionary["generateWarehouseDoc"].ToString(), out gjeneroDokMag);
                //
                //Alternativat
                bool tollona = clsAlternativaKushti.getAlternativa(konfigurimAmbjenti.IdKonfigAmbjente, "RSHTT") == "Po";
                string llojZevendesimi = clsAlternativaKushti.getAlternativa(konfigurimAmbjenti.IdKonfigAmbjente, "ZT");
                bool tollonakastrati = clsAlternativaKushti.getAlternativa(konfigurimAmbjenti.IdKonfigAmbjente, "RSHTTK") == "Po";
                bool tollonakastratielektronik = clsAlternativaKushti.getAlternativa(konfigurimAmbjenti.IdKonfigAmbjente, "RSHTTKE") == "Po";
                bool krijoartri = clsAlternativaKushti.getAlternativa(konfigurimAmbjenti.IdKonfigAmbjente, "BKAR") == "Po";
                bool zevendesimtollonakastrati = clsAlternativaKushti.getAlternativa(konfigurimAmbjenti.IdKonfigAmbjente, "ZTK") == "Po";
                bool kontrolloSasiKonvertimiDheKthimi = clsAlternativaKushti.getAlternativa(konfigurimAmbjenti.IdKonfigAmbjente, "NK") == "Po";
                DbData dbData = new DbData();
                if (clsAlternativaKushti.getAlternativa(konfigurimAmbjenti.IdKonfigAmbjente, "AFI") == "Po")
                    kontrolloIMEIFifo = true;
                bool zevendesimtollona = !(llojZevendesimi == "Jo");
                colTrupiShitje = krijoTrupShtije(perdorues.IdPerdorues, "shitje", ndermarrje.IdNdermarrje, konfigurimAmbjenti.IdKonfigAmbjente, false, trupi, new { }.ToString(), "shtim", gjeneroDokMag, false, "", magazina.Kodi, false, new Dictionary<string, object>(), new Dictionary<string, object>(), perqindjeZbritje, new DbCore.DbAsete.colSerialetMagazine(), false, kursi, 1, konfigurimAmbjentiMag, false, false, false, false, dtDok);
                clsPeriudhaKontabel periudhaKontabel = new clsPeriudhaKontabel(dtDok,ndermarrje.IdNdermarrje);
                //

                //Mbushje
                konfigurimAmbjenti.mbushKonfigAmbjSipasKod(alphaMetadata["invoiceFormat"].ToString(), ndermarrje.IdNdermarrje);
                konfigurimAmbjentiMag.mbushKonfigAmbjSipasKod("FDS", ndermarrje.IdNdermarrje);
                monedha.mbushMonedhen(dictionary["currency"].ToString(), ndermarrje.IdNdermarrje);
                ndermarrjeViti.mbushNdermarrjeVitiSipasNdermarjesDheVitit(ndermarrje.IdNdermarrje, viti.IdViti);
                banka.mbushBankeSipasKodit(dictionary["cashRegisterCode"].ToString(), ndermarrje.IdNdermarrje);
                DataTable op = clsOperator.MerrOperatoretAktive(ndermarrje.IdNdermarrje);
                DataTable processet = koka.ktheIdProcesi(dictionary["profileID"].ToString());
                DataTable eInvTypes = koka.ktheIdTipiEinvoice(dictionary["invoiceTypeCode"].ToString());
                if(processet.Rows.Count > 0) int.TryParse(processet.Rows[0].ItemArray[0].ToString(),out procesi);
                if (eInvTypes.Rows.Count > 0)  int.TryParse(eInvTypes.Rows[0].ItemArray[0].ToString(), out tipiEinvoice);
                for(int i = 0; i < op.Rows.Count; i++)
                {
                    if (op.Rows[i].ItemArray[1].ToString() == dictionary["operatorCode"].ToString())
                        int.TryParse(op.Rows[i].ItemArray[0].ToString(),out operatori);

                }


                //Creating invoice
                koka.krijoShitje(ref gjeneroDokMag, konfigurimAmbjenti.IdNivel, 0, konfigurimAmbjenti.IdKonfigAmbjente, kf.IdKlientFurnitor, kf.KodKlientFurnitor, 0, "0", dtDok, nrdok, "", dtDok, monedha.IdMonedha, monedha.KodiMonedha,
                    kursi, 0, "", dtDok, 0, "", 0, "", idMenyrePAgese, kodMenyrePAgese, 0, "", zbritje, totali, tvsh, dtDok, 1, ndermarrje.IdNdermarrje, ndermarrjeViti.IdNderViti,
                    0, 0, 0, 0, adresa, adresa, pershkrimi, false, dega.IdDegeAdministrative, dega.Kodi, 0, "", perdorues.IdPerdorues, 0,colTrupiShitje,true,konfigurimAmbjenti.KodKonfigAmbjente, periudhaKontabel.IdPeriudha,konfigurimAmbjentiMag, magazina.IdNjesiAdministrative,magazina.Kodi,false,0,0,0,dtDok,totali, StatusAprovimi.Undefined, perdorues.IdPerdorues,0.00,out shfaqmesazhapolupe,new Dictionary<string,object>(),new DbCore.DbQendraKosto.colTrupiQendraKosto()
                    ,0,out mesazhInfo, false,new clsKokaShitje(),0,0,false,false, StatusTrasferimi.PaTransferuar, kf.EmertimiKF,kf.EmailKF,false,false,"", dtFillimi, dtMbarimi,0,0.00,"",0,0.00,"",0,0.00,"","",0,"",false,new clsKokaShitje(),false,banka.IdBanka,true,false,false,false,dtDok,false,false,false,periudhaKontabel.IdPeriudha,ndermarrjeViti.IdViti,"","",zbritjeNeVlere,perqindjeZbritjeTotale,0,0,new colFazaKontrate(),0,new colKlienteFurnitore(),DateTime.Now,new DbData(),"","",false,false,perdorues.IdGjuha,konfigurimAmbjenti,0,kf.NiptiKF,new clsQyteti(kf.QytetiKF).KodiQyteti,false,0,
                    new colSerialeUnikeMagazina(),"",false,0,false,0,"",StatusMarreveshje.Undefined,"","shtim",dtDok,false,nrdok,iic,nivf,operatori,nivfKthim,eic,einStatus,procesi, tipiEinvoice, tipVetFaturimi);
                
                var msg = koka.ruaj(perdorues.IdGjuha, "", true, new Dictionary<string,object>(), periudhaKontabel.IdPeriudha, new colKonvertimi(), true, out veprimebanka, 0, StatusAprovimi.Undefined, 0,
                            out shfaqmesazhapolupemagazina, out shfaqmesazhapolupebanka, out shfaqmesazhapolupeVDK, new clsKokaShitje(), 0, 0, false, false, false, "", serialemag,
                            konfamortizimi, new clsKokaShitje(), out printofature, out printogarancifature, out pageseFature, true, out shfaqmesazhapolupe,konfigurimAmbjenti.KodKonfigAmbjente,
                            false, string.Empty,0, tollona, zevendesimtollona, false, false, "", "", "", tollonakastrati, tollonakastratielektronik, false, "",
                            false, false, zevendesimtollonakastrati, false, kontrolloSasiKonvertimiDheKthimi, new colKokaShitje(), kontrolloIMEIFifo,shtimModifikim == "bli",
                            !konfigurimAmbjenti.KodKonfigAmbjente.Contains("USHmag"), ref dbData, krijoartri, "", "", false, out mesazhmevonshem, false, String.IsNullOrEmpty(nrdok), iic, nivf);
                if(!msg.Status) return new clsMesazh(false, msg.PershkrimMesazhi);
                return new clsMesazh(true, msg.PershkrimMesazhi);
            }
            catch (Exception ex)
            {
                //logu.Error($"{HttpContext.Current.Request.UserHostAddress} : RefreshServerList > MyConnectionsManager.RefreshConnectionStringsPool > {ex.ToString()}");
                return new clsMesazh(false, ex.ToString());
            }
        }
        private colTrupiShitje krijoTrupShtije(int idPerdoruesi, string veprimi, int idNdermarrje, int idKonfAmbj, bool tollon, string gridDataObject, string gridObjectKomision, string shtimModifikim, bool gjenerodokumentmagazine, bool ownshop, string Grup1, string btnMagazina, bool meme, IDictionary<string, object> seriale, IDictionary<string, object> hfIdGride, double perqindjeZbritje, DbCore.DbAsete.colSerialetMagazine colserialemag, bool kontrolloSasi, double kursi, int statusDokumenti, clsKonfigurimAmbjenti konfmag, bool tollonkastati, bool zevendesimtollonakastrati, bool blerengadealer, bool shitjevodafone,DateTime dtdok)
        {
            Dictionary<string, string>[] dokumenti = JsonConvert.DeserializeObject<Dictionary<string, string>[]>(gridDataObject);//serializusi.DeserializeObject(gridDataObject) as object[];
            colTrupiShitje trupat = new colTrupiShitje();
            int idMagTemp = -1;
            bool isMagENjejte = false;
            bool isShitje = true;
            bool konvertim = shtimModifikim == "konvertim";
            bool konvertimblerje = shtimModifikim == "konvertimblerje";
            bool klonim = shtimModifikim == "klonim";
            bool kthim = false;
            bool merrSipasGrupit = clsAlternativaKushti.getAlternativa(idKonfAmbj, "AASG") == "Po";
            bool merrDhurata = clsAlternativaKushti.getAlternativa(idKonfAmbj, "AADH") == "Po";
            var merrMagazinenNgaTrupi = clsAlternativaKushti.getAlternativa(idKonfAmbj, "NKDMMT") == "Po";
            int dokumentiLength = dokumenti.Length;
            bool kthimVod = shtimModifikim == "kthimVod";
            bool ruajBarkod = clsAlternativaKushti.getAlternativa(idKonfAmbj, "RBART") == "Po";
            bool lejoMagNdryshme = clsAlternativaKushti.getAlternativa(idKonfAmbj, "LMNGB") == "Po";
            bool lejoSasiPozitiveKthim = clsAlternativaKushti.getAlternativa(idKonfAmbj, "LSPK") == "Po";
            var nrRendorSerial = -1;
            for (int i = 0; i < dokumentiLength; i++)
            {
                clsTrupiShitje trupi = new clsTrupiShitje(idNdermarrje, idPerdoruesi, dokumenti[i], isShitje, konvertim, meme, merrSipasGrupit, Grup1, merrDhurata, ownshop, gjenerodokumentmagazine, klonim, kthim, veprimi, tollon, i, seriale, hfIdGride, perqindjeZbritje, colserialemag, kontrolloSasi, kursi, statusDokumenti, konfmag, tollonkastati, konvertimblerje, zevendesimtollonakastrati, kthimVod, blerengadealer, shitjevodafone, i + 1, ruajBarkod, false, lejoMagNdryshme, dtdok, lejoSasiPozitiveKthim, ref nrRendorSerial,true);
                if (string.IsNullOrEmpty(trupi.Kodi))
                    continue;

                if (merrMagazinenNgaTrupi || !string.IsNullOrEmpty(btnMagazina))
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
            return trupat;
        }
    }
}