using DbCore.IMBUtils.DataBase;
using System.Data;
using System;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Messages;
using DbCore.IMBUtils.Logging;
using Newtonsoft.Json;

namespace DbCore.DbBuxheti
{
    public class ClsDatabaseBuxheti : DbData
    {
        #region konstruktoret

        #endregion konstruktoret

        #region B_KategoriBuxheti
        internal void MerrKategoriBuxhetimi(int idNdermarrje, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_KATEGORIBUXHETIMI_merrSipasNdermarrjes", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje);
        }
        internal void MbushKategoriBuxhetimiSipasNdermarrjesDheBijave(int idNdermarrje, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_KATEGORIBUXHETIMI_merrAktiveSipasNdermarrjesDheBijave", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje);
        }

        internal DataTable MerrGjitheBijatBashkeMePrinderitFillestare(int idNdermarrje)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_KATEGORIBUXHETIMI_merrGjitheBijatBashkeMePrinderitFillestare");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje);
            return ds.Tables[0];
        }

        internal void MerrKategoriBuxhetimiSipasIdKategoriBuxhetimi(int idKategoriBuxhetimi, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKategoriBuxhetimi);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDKATEGORIBUXHETIMI", idKategoriBuxhetimi, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_KATEGORIBUXHETIMI_merrSipasIdKategoriBuxhetimi", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKategoriBuxhetimi);
        }

        internal DataTable ktheDokAlokimBuxhetiPerEksport(int idNdermarrje)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_MerrDokAlokimBuxhetiPerEksport");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje);
            return ds.Tables[0];
        }

        internal DataTable ktheDokMiratimBuxhetiPerEksport(int idNdermarrje)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_merrMiratimBuxhetiPerExport");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje);
            return ds.Tables[0];
        }

        internal DataTable merrDokumentaPerImport(string emerTabKoka, string emerTabTrupi, string ndermarrjeKeyFusha, string ndermarjeKodi, string primaryKey, bool merrTePaImportuara, bool rimerrTeImportuara, int? nrDokumentash)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, emerTabKoka, emerTabTrupi, ndermarrjeKeyFusha, ndermarjeKodi, primaryKey, merrTePaImportuara);

            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@T_TEMP_KOKAARKA", emerTabKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@T_TEMP_TRUPIARKA", emerTabTrupi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NDERMARRJEKEY", ndermarrjeKeyFusha, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NDERMARJEKODI", ndermarjeKodi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PRIMARYKEY", primaryKey, ParameterDirection.Input);
            dbManager.AddParameters(5, "@MERRTEPAIMPORTUARA", merrTePaImportuara, ParameterDirection.Input);
            dbManager.AddParameters(6, "@RIMERRTEIMPORTUARA", rimerrTeImportuara, ParameterDirection.Input);
            dbManager.AddParameters(7, "@NRDOKUMENTASH", nrDokumentash, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_TEMP_IMPORT_BUXHETI_merrTeDhenaPerNdermarrjeSipasLlojitDT");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, emerTabKoka, emerTabTrupi, ndermarrjeKeyFusha, ndermarjeKodi, primaryKey, merrTePaImportuara);
            return ds.Tables[0];
        }

        internal DataTable merrKategoriBuxhetimiLikeKodOsePershkDT(int idNdermarrje, string infixText)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje, infixText);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODPERSHK", infixText, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_KATEGORIBUXHETIMI_merrKategoriLikeKodOsePershkDT");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje, infixText);
            return ds.Tables[0];
        }

        public DataTable merrDatasourceTrupiMiratimPlanifikimBuxheti(int idNdermarrje, int idKokaBuxheti, int idKomponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idKokaBuxheti, idKomponente);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDBUXHETIKOKA", idKokaBuxheti, ParameterDirection.Input);
            dbManager.AddParameters("@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_merrDsTrupBuxheti");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idKokaBuxheti, idKomponente);
            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }

        internal DataTable merrLlojeBuxhetiLikeKodOsePershkDT(int idNdermarrje, string infixText)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje, infixText);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODPERSHK", infixText, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_LLOJBUXHETI_merrLlojeBuxhetiLikeKodOsePershkDT");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje, infixText);
            return ds.Tables[0];
        }
        internal DataTable merrLlojeBuxhetiLikeKodOsePershkDTSipasLlogarise(int idNdermarrje, int idLlogaria, string infixText)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idLlogaria, infixText);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOGARIA", idLlogaria, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODPERSHK", infixText, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_LLOJBUXHETI_merrLlojeBuxhetiLikeKodOsePershkDTSipasLlogarise");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idLlogaria, infixText);
            return ds.Tables[0];
        }

        public DataTable merrDatasourceTrupiAlokimBuxheti(int idNdermarrje, int idKokaAlokimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idKokaAlokimi);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDALOKIMIKOKA", idKokaAlokimi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_merrDsTrupAlokimBuxheti");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idKokaAlokimi);
            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }

        public DataTable merrDatasourceTrupiBuxheti(int idBuxhetiKoka)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idBuxhetiKoka);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDBUXHETIKOKA", idBuxhetiKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_merrDsTrupBuxhetiGjitheFushat");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idBuxhetiKoka);
            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }
        public DataTable merrDatasourceTrupiAlokimBuxhetiNgaKonvertimMiratimi(int idKokaBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKokaBuxheti);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDBUXHETIKOKA", idKokaBuxheti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_merrTrupAlokimiNgaKonvertimMiratimi");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKokaBuxheti);
            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }

        public DataTable merrDataTableAlokimBuxhetiPerEksportAmbjenti(int idkokabuxheti, int idNdermarrje, bool eshtekonvertim, string data)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idkokabuxheti, idNdermarrje, eshtekonvertim, data);

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@IDKOKABUXHETI", idkokabuxheti, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@KONVERTIM", eshtekonvertim, ParameterDirection.Input);
            dbManager.AddParameters("@DATA", data, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_merrDokAlokimBuxhetiPerEksportAmbjent");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idkokabuxheti, idNdermarrje, eshtekonvertim, data);
            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }
        public DataTable merrDataTableMiratimBuxhetiPerEksportAmbjenti(int idkokabuxheti, int idNdermarrje, int idKomponente, string data, int idNiveli, string llojDok, string nrDok, decimal totaliFaktik)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idkokabuxheti, idNdermarrje, idKomponente, data, idNiveli, llojDok, nrDok, totaliFaktik);

            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters("@IDBUXHETIKOKA", idkokabuxheti, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            dbManager.AddParameters("@IDNIVELI", idNiveli, ParameterDirection.Input);
            dbManager.AddParameters("@LLOJDOK", llojDok, ParameterDirection.Input);
            dbManager.AddParameters("@DATA", data, ParameterDirection.Input);
            dbManager.AddParameters("@NRDOK", nrDok, ParameterDirection.Input);
            dbManager.AddParameters("@TOTALIFAKTIK", totaliFaktik, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_merrMiratimBuxhetiPerExportAmbienti");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idkokabuxheti, idNdermarrje, idKomponente, data, idNiveli, llojDok, nrDok, totaliFaktik);
            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }

        public DataTable merrTrupBuxhetiPerKonvertim(int idKokaBuxheti, string llojKonvertimiNga)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKokaBuxheti, llojKonvertimiNga);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDBUXHETIKOKA", idKokaBuxheti, ParameterDirection.Input);
            dbManager.AddParameters("@LLOJKONVERTIMINGA", llojKonvertimiNga, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_merrTrupBuxhetiPerKonvertim");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKokaBuxheti, llojKonvertimiNga);
            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }

        internal clsMesazh RuajKategoriBuxhetimi(ClsBKategoriBuxhetimi kategoriBuxhetimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);

            dbManager.Open();
            dbManager.CreateParameters(12);
            dbManager.AddParameters("@IDKATEGORIBUXHETIMI", kategoriBuxhetimi.IdKategoriBuxhetimi, ParameterDirection.Output);
            dbManager.AddParameters("@KODI", kategoriBuxhetimi.Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@PERSHKRIMI", kategoriBuxhetimi.Pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters("@PERSHKRIMI2", kategoriBuxhetimi.Pershkrimi2, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", kategoriBuxhetimi.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", kategoriBuxhetimi.IdKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters("@IDPRINDI", kategoriBuxhetimi.IdPrindi > 0 ? kategoriBuxhetimi.IdPrindi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@NIVELKATEGORIE", kategoriBuxhetimi.NivelKategorie, ParameterDirection.Input);
            dbManager.AddParameters("@IDLLOGARIA", kategoriBuxhetimi.IdLlogaria > 0?kategoriBuxhetimi.IdLlogaria : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@KOEFICENTI", kategoriBuxhetimi.Koeficenti, ParameterDirection.Input);
            dbManager.AddParameters("@INTEGROTENDERMBIJA", kategoriBuxhetimi.GetIntegroKategoriTeNdermBija(), ParameterDirection.Input);
            dbManager.AddParameters("@AKTIVE", kategoriBuxhetimi.Aktive, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KATEGORIBUXHETIMI_insert");
            kategoriBuxhetimi.IdKategoriBuxhetimi = int.Parse(dbManager.Parameters[0].Value.ToString());

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        internal clsMesazh RuajKategoriBuxhetimiNeHistorik(ClsBKategoriBuxhetimi kategoriBuxhetimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);

            dbManager.Open();
            dbManager.CreateParameters(12);
            dbManager.AddParameters("@KODI", kategoriBuxhetimi.Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@PERSHKRIMI", kategoriBuxhetimi.Pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters("@PERSHKRIMI2", kategoriBuxhetimi.Pershkrimi2, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", kategoriBuxhetimi.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDMODIFIKUESI", kategoriBuxhetimi.IdModifikuesi > 0 ? kategoriBuxhetimi.IdModifikuesi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@IDPRINDI", kategoriBuxhetimi.IdPrindi > 0 ? kategoriBuxhetimi.IdPrindi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@NIVELKATEGORIE", kategoriBuxhetimi.NivelKategorie, ParameterDirection.Input);
            dbManager.AddParameters("@IDLLOGARIA", kategoriBuxhetimi.IdLlogaria > 0 ? kategoriBuxhetimi.IdLlogaria : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@KOEFICENTI", kategoriBuxhetimi.Koeficenti, ParameterDirection.Input);
            dbManager.AddParameters("@DTKRIJIMI", kategoriBuxhetimi.DtKrijimi, ParameterDirection.Input);
            dbManager.AddParameters("@INTEGROTENDERMBIJA", kategoriBuxhetimi.GetIntegroKategoriTeNdermBija(), ParameterDirection.Input);
            dbManager.AddParameters("@AKTIVE", kategoriBuxhetimi.Aktive, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KATEGORIBUXHETIMI_HISTORIK_insert");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }


        internal clsMesazh ModifikoKategoriBuxhetimi(ClsBKategoriBuxhetimi kategoriBuxhetimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);

            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddParameters("@KODI", kategoriBuxhetimi.Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@PERSHKRIMI", kategoriBuxhetimi.Pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters("@PERSHKRIMI2", kategoriBuxhetimi.Pershkrimi2, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", kategoriBuxhetimi.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", kategoriBuxhetimi.IdModifikuesi, ParameterDirection.Input);
            dbManager.AddParameters("@IDPRINDI", kategoriBuxhetimi.IdPrindi > 0 ? kategoriBuxhetimi.IdPrindi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@NIVELKATEGORIE", kategoriBuxhetimi.NivelKategorie, ParameterDirection.Input);
            dbManager.AddParameters("@IDLLOGARIA", kategoriBuxhetimi.IdLlogaria > 0 ? kategoriBuxhetimi.IdLlogaria : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@KOEFICENTI", kategoriBuxhetimi.Koeficenti, ParameterDirection.Input);
            dbManager.AddParameters("@INTEGROTENDERMBIJA", kategoriBuxhetimi.GetIntegroKategoriTeNdermBija(), ParameterDirection.Input);
            dbManager.AddParameters("@AKTIVE", kategoriBuxhetimi.Aktive, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KATEGORIBUXHETIMI_update");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        
        internal clsMesazh RuajLlojeBuxhetiPerKategoriBuxhetimi(ClsBKategoriBuxhetimi kategoriBuxhetimi, string idBuxhete)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi, idBuxhete);

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@KODI", kategoriBuxhetimi.Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@IDBUXHETE", idBuxhete, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", kategoriBuxhetimi.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@INTEGROTENDERMBIJA", kategoriBuxhetimi.GetIntegroKategoriTeNdermBija(), ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_LLOJBUXHETI_KATEGORIBUXHETIMI_insert");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi, idBuxhete);
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        internal bool IPerketPershkrimiKategoriseMeKod(string kodi, string pershkrimi, int idNdermarrje)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kodi, pershkrimi, idNdermarrje);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters("@PERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KATEGORIBUXHETIMI_PerputhetPershkrimiMeKodin"));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kodi, pershkrimi, idNdermarrje);
            return  1 == nr;
        }

        //sp kthen -1 nese nuk gjen kategori me kodin e dhene perndryshe kthen id e kategorise
        internal int MerrIdKategoriSipasKodit(string kodi, int idndermarrje)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kodi, idndermarrje);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", idndermarrje, ParameterDirection.Input);
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KATEGORIBUXHETIMI_KtheIdKategoriSipasKodit"));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kodi, idndermarrje);
            return nr;
        }

        public bool EkzistonKategoriBuxhetimiMeKeteKod(string Kodi, int IdNdermarrje, bool IntegroKategoriTeNdermBija)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, IdNdermarrje, IntegroKategoriTeNdermBija);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@KODI", Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@INTEGROTENDERMBIJA", IntegroKategoriTeNdermBija, ParameterDirection.Input);
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KATEGORIBUXHETIMI_ekzistonKategoriBuxhetimiMeKeteKod"));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, IdNdermarrje, IntegroKategoriTeNdermBija);
            return nr == 1;
        }

        internal DataRow MerrGjendjePerKategoriBuxhetimi(int idNderm, DateTime? dtDok, int periudha, int idKategoriBuxhetimi, int idBuxhetiKoka, int idBuxheti, decimal vlera, int idKatDok)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNderm, dtDok, periudha, idKategoriBuxhetimi, idBuxhetiKoka, idBuxheti, vlera, idKatDok);

            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters("@IDKATEGORIBUXHETIMI", idKategoriBuxhetimi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERM", idNderm, ParameterDirection.Input);
            dbManager.AddParameters("@DTDOK", dtDok, ParameterDirection.Input);
            dbManager.AddParameters("@PERIUDHA", periudha, ParameterDirection.Input);
            dbManager.AddParameters("@IDBUXHETIKOKA", idBuxhetiKoka, ParameterDirection.Input);
            dbManager.AddParameters("@IDBUXHETI", idBuxheti, ParameterDirection.Input);
            dbManager.AddParameters("@VLERA", vlera, ParameterDirection.Input);
            dbManager.AddParameters("@IDKATDOK", idKatDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_TRUPIBUXHETI_merrGjendjeKategoriBuxhetimi");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNderm, dtDok, periudha, idKategoriBuxhetimi, idBuxhetiKoka, idBuxheti, vlera, idKatDok);
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds.Tables[0].Rows[0];
        }
        internal decimal MerrGjendjeMbeturNgaPerfitimi(int idNderm, DateTime? dtDok, int idBuxhetiKoka, int idBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNderm, dtDok, idBuxhetiKoka, idBuxheti);

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@IDNDERM", idNderm, ParameterDirection.Input);
            dbManager.AddParameters("@DTDOK", dtDok, ParameterDirection.Input);
            dbManager.AddParameters("@IDBUXHETIKOKA", idBuxhetiKoka, ParameterDirection.Input);
            dbManager.AddParameters("@IDBUXHETI", idBuxheti, ParameterDirection.Input);
            decimal value = Convert.ToDecimal(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_TRUPIBUXHETI_merrGjendjePerfitimi"));

            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNderm, dtDok, idBuxhetiKoka, idBuxheti);
            return value;
        }
        internal DataRow MerrGjendjePerKategoriBuxhetimiNgaKonvertimi(int idKategoriBuxhetimi, int idTrupKonvertimiNga, int idBuxheti, int idBuxhetiKoka)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKategoriBuxhetimi, idTrupKonvertimiNga, idBuxheti, idBuxhetiKoka);

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@IDKATEGORIBUXHETIMI", idKategoriBuxhetimi, ParameterDirection.Input);
            dbManager.AddParameters("@IDTRUPIKONVERTIMI", idTrupKonvertimiNga, ParameterDirection.Input);
            dbManager.AddParameters("@IDBUXHETI", idBuxheti, ParameterDirection.Input);
            dbManager.AddParameters("@IDBUXHETIKOKA", idBuxhetiKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_TRUPIBUXHETI_merrGjendjeKategoriBuxhetimiNgaKonvertimi");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKategoriBuxhetimi, idTrupKonvertimiNga, idBuxheti, idBuxhetiKoka);
            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds.Tables[0].Rows[0];
        }
        public bool EshteKategoriBuxhetimiLidhur(ClsBKategoriBuxhetimi kategoriBuxhetimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@KODI", kategoriBuxhetimi.Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", kategoriBuxhetimi.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@INTEGROTENDERMBIJA", kategoriBuxhetimi.GetIntegroKategoriTeNdermBija(), ParameterDirection.Input);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KATEGORIBUXHETIMI_eshteKategoriBuxhetimiLidhur")) == 1;
        }
        public bool EshteLlogariKategoriBuxhetimiLidhur(ClsBKategoriBuxhetimi kategoriBuxhetimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@KODI", kategoriBuxhetimi.Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", kategoriBuxhetimi.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@INTEGROTENDERMBIJA", kategoriBuxhetimi.GetIntegroKategoriTeNdermBija(), ParameterDirection.Input);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KATEGORIBUXHETIMI_eshteLlogariKategoriBuxhetimiLidhur")) == 1;
        }
        public bool EshteKategoriBuxhetimiPrind(ClsBKategoriBuxhetimi kategoriBuxhetimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@KODI", kategoriBuxhetimi.Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", kategoriBuxhetimi.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@INTEGROTENDERMBIJA", kategoriBuxhetimi.GetIntegroKategoriTeNdermBija(), ParameterDirection.Input);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KATEGORIBUXHETIMI_eshteKategoriBuxhetimiPrind")) == 1;
        }

        internal bool EkzistonDokumentBuxhetiPerKategori(int idKategoriBuxhetimi, int idKatDok, int periudha, DateTime dtDok)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKategoriBuxhetimi, idKatDok, periudha, dtDok);

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@IDKATEGORIBUXHETIMI", idKategoriBuxhetimi, ParameterDirection.Input);
            dbManager.AddParameters("@IDKATDOK", idKatDok, ParameterDirection.Input);
            dbManager.AddParameters("@PERIUDHA", periudha, ParameterDirection.Input);
            dbManager.AddParameters("@DTDOK", dtDok, ParameterDirection.Input);
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_ekzistonDokumentBuxhetiPerKategoriBuxhetimi"));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKategoriBuxhetimi, idKatDok, periudha, dtDok);
            return nr == 1;
        }

        internal clsMesazh FshiKategoriBuxhetimi(ClsBKategoriBuxhetimi kategoriBuxhetimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@KODI", kategoriBuxhetimi.Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", kategoriBuxhetimi.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@INTEGROTENDERMBIJA", kategoriBuxhetimi.GetIntegroKategoriTeNdermBija(), ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KATEGORIBUXHETIMI_delete");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }
        #endregion

        #region B_LlojBuxheti
        internal void MerrLlojBuxhetiSipasNdermarrjes(int idNdermarrje, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_LlojBuxheti_merrSipasNdermarrjes", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje);
        }
        internal void MerrLlojBuxhetiSipasNdermarrjesDheBijave(int idNdermarrje, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_LlojBuxheti_merrSipasNdermarrjesDheBijave", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje);
        }
        internal void MerrLlojBuxhetiSipasNdermarrjesDheLlogarise(int idNdermarrje, int idLlogaria, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idLlogaria);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOGARIA", idLlogaria, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_LlojBuxheti_merrSipasNdermarrjesDheLlogarise", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idLlogaria);
        }
        internal void MerrLlojBuxhetiSipasKategoriBuxhetimi(int idKategoriBuxheti, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKategoriBuxheti);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKATEGORIBUXHETIMI", idKategoriBuxheti, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_LlojBuxheti_merrSipasKategoriBuxhetimi", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKategoriBuxheti);
        }
        internal void MerrLlojBuxhetiSipasBuxheteve(string idBuxhete, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idBuxhete);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDBUXHETE", idBuxhete, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_LlojBuxheti_merrSipasBuxheteve", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idBuxhete);
        }
        internal clsMesazh FshiLlojeBuxhetiPerKategoriBuxhetimi(ClsBKategoriBuxhetimi kategoriBuxhetimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@KODI", kategoriBuxhetimi.Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", kategoriBuxhetimi.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@INTEGROTENDERMBIJA", kategoriBuxhetimi.GetIntegroKategoriTeNdermBija(), ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_LLOJBUXHETI_KATEGORIBUXHETIMI_delete");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kategoriBuxhetimi);
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }
        internal clsMesazh RuajLlojBuxheti(ClsBLlojBuxheti llojBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, llojBuxheti);

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters("@IDLLOJBUXHETI", llojBuxheti.IdLlojBuxheti, ParameterDirection.Output);
            dbManager.AddParameters("@KODI", llojBuxheti.Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@PERSHKRIMI", llojBuxheti.Pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", llojBuxheti.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", llojBuxheti.IdKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters("@INTEGROTENDERMBIJA", llojBuxheti.GetIntegroBuxhetTeNdermBija(), ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_LLOJBUXHETI_insert");
            llojBuxheti.IdLlojBuxheti = int.Parse(dbManager.Parameters[0].Value.ToString());

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, llojBuxheti);
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        internal clsMesazh FshiLlojBuxheti(ClsBLlojBuxheti llojBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, llojBuxheti);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@KODI", llojBuxheti.Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", llojBuxheti.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@INTEGROTENDERMBIJA", llojBuxheti.GetIntegroBuxhetTeNdermBija(), ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_LLOJBUXHETI_delete");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, llojBuxheti);
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        internal clsMesazh ModifikoLlojBuxheti(ClsBLlojBuxheti llojBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, llojBuxheti);

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters("@KODI", llojBuxheti.Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@PERSHKRIMI", llojBuxheti.Pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", llojBuxheti.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", llojBuxheti.IdModifikuesi, ParameterDirection.Input);
            dbManager.AddParameters("@INTEGROTENDERMBIJA", llojBuxheti.GetIntegroBuxhetTeNdermBija(), ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_LLOJBUXHETI_update");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, llojBuxheti);
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        internal bool EshteLlojBuxhetiLidhur(ClsBLlojBuxheti llojBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, llojBuxheti);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@KODI", llojBuxheti.Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", llojBuxheti.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@INTEGROTENDERMBIJA", llojBuxheti.GetIntegroBuxhetTeNdermBija(), ParameterDirection.Input);
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_LLOJBUXHETI_eshteLlojBuxhetiLidhur"));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, llojBuxheti);
            return nr == 1;
        }

        internal clsMesazh fshiTrupDokumentBuxheti(int idKoka)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKoka);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_TRUPIBUXHETI_delete");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKoka);
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        internal int KtheStatusAprovimiDokumenti(int idBuxhetiKoka)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idBuxhetiKoka);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDBUXHETIKOKA", idBuxhetiKoka, ParameterDirection.Input);
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_ktheStatusAprovuar"));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idBuxhetiKoka);
            return nr;
        }

        internal bool EkzistonLlojBuxhetiMeKeteKod(ClsBLlojBuxheti llojBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, llojBuxheti);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@KODI", llojBuxheti.Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", llojBuxheti.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@INTEGROTENDERMBIJA", llojBuxheti.GetIntegroBuxhetTeNdermBija(), ParameterDirection.Input);
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_LLOJBUXHETI_ekzistonLlojBuxhetiMeKeteKod"));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, llojBuxheti);
            return nr == 1;
        }
        internal void MerrLlojBuxhetiSipasIdLlojBuxhetu(int idLlojBuxheti, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idLlojBuxheti);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOJBUXHETI", idLlojBuxheti, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_LLOJEBUXHETI_merrSipasIdLlojBuxheti", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idLlojBuxheti);
        }

        internal void MerrLlojBuxhetiSipasKodLlojBuxheti(string kodLlojBuxheti, int idNdermarrje, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kodLlojBuxheti, idNdermarrje);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODLLOJBUXHETI", kodLlojBuxheti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_LLOJEBUXHETI_merrSipasKodLlojBuxheti", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kodLlojBuxheti, idNdermarrje);
        }

        public DataTable merrLlojeBuxhetiKategoriBuxhetiSipasNdermarrje(int idNdermarrje)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_LLOJBUXHETI_KATEGORIBUXHETIMI_MerrPerNdermarrje");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje);
            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
                return null;
            else
                return ds.Tables[0];
        }
        #endregion

        #region B_KokaBuxheti

        internal clsMesazh RuajKokaBuxheti(ClsBKokaBuxheti kokaBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kokaBuxheti);

            dbManager.Open();
            dbManager.CreateParameters(26);
            dbManager.AddParameters("@IDBUXHETIKOKA", kokaBuxheti.IdBuxhetiKoka, ParameterDirection.Output);
            dbManager.AddParameters("@IDNIVEL", kokaBuxheti.IdNivel, ParameterDirection.Input);
            dbManager.AddParameters("@IDKONFIGAMBJENTE", kokaBuxheti.IdKonfigAmbjente, ParameterDirection.Input);
            dbManager.AddParameters("@NRDOK", kokaBuxheti.NrDok, ParameterDirection.Input);
            dbManager.AddParameters("@DTDOK", kokaBuxheti.DtDok, ParameterDirection.Input);
            dbManager.AddParameters("@TOTALIPLANIFIKUAR", kokaBuxheti.TotaliPlanifikuar, ParameterDirection.Input);
            dbManager.AddParameters("@TOTALI", kokaBuxheti.Totali, ParameterDirection.Input);
            dbManager.AddParameters("@IDSTATUSDOK", kokaBuxheti.IdStatusDok, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERM", kokaBuxheti.IdNderm, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMVIT", kokaBuxheti.IdNdermVit > 0 ? kokaBuxheti.IdNdermVit : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@SHENIME", kokaBuxheti.Shenime, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", kokaBuxheti.IdPerdoruesi > 0 ? kokaBuxheti.IdPerdoruesi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@IDKRIJUESI", kokaBuxheti.IdKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters("@VITI", kokaBuxheti.Viti, ParameterDirection.Input);
            dbManager.AddParameters("@IDRAPORTDESIGN", kokaBuxheti.IdRaportDesign > 0 ? kokaBuxheti.IdRaportDesign : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@IDKOKAKONVERTIMINGA", kokaBuxheti.IdKokaKonvertimiNga > 0 ? kokaBuxheti.IdKokaKonvertimiNga : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@LLOJKONFIGKONVERTIMINGA", kokaBuxheti.LlojKonfigKonvertimiNga, ParameterDirection.Input);
            dbManager.AddParameters("@IDKATDOK", kokaBuxheti.IdKatDok, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMPOSTUESI", kokaBuxheti.IdNdermPostuesi > 0 ? kokaBuxheti.IdNdermPostuesi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@IDDOKPOSTUESI", kokaBuxheti.IdDokPostuesi > 0 ? kokaBuxheti.IdDokPostuesi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@TOTALIPATVSH", kokaBuxheti.TotaliPaTvsh, ParameterDirection.Input);
            dbManager.AddParameters("@LLOJVEPRIMIGJENERIMI", kokaBuxheti.LlojVeprimiGjenerimi > 0 ? kokaBuxheti.LlojVeprimiGjenerimi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@IDENTITETI", kokaBuxheti.IdEntiteti > 0 ? kokaBuxheti.IdEntiteti : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@IDBURIMI", kokaBuxheti.IdBurimi > 0 ? kokaBuxheti.IdBurimi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@STATUSAPROVIMI", (int)kokaBuxheti.StatusAprovimi > 0 ? (int)kokaBuxheti.StatusAprovimi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@DOKTORI", kokaBuxheti.Doktori > 0 ? kokaBuxheti.Doktori : (int?)null, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_insert");
            kokaBuxheti.IdBuxhetiKoka = int.Parse(dbManager.Parameters[0].Value.ToString());

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kokaBuxheti);
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        internal clsMesazh ModifikoKokaBuxheti(ClsBKokaBuxheti kokaBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kokaBuxheti);

            dbManager.Open();
            dbManager.CreateParameters(26);
            dbManager.AddParameters("@IDBUXHETIKOKA", kokaBuxheti.IdBuxhetiKoka, ParameterDirection.Input);
            dbManager.AddParameters("@IDNIVEL", kokaBuxheti.IdNivel, ParameterDirection.Input);
            dbManager.AddParameters("@IDKONFIGAMBJENTE", kokaBuxheti.IdKonfigAmbjente, ParameterDirection.Input);
            dbManager.AddParameters("@NRDOK", kokaBuxheti.NrDok, ParameterDirection.Input);
            dbManager.AddParameters("@DTDOK", kokaBuxheti.DtDok, ParameterDirection.Input); 
            dbManager.AddParameters("@TOTALIPLANIFIKUAR", kokaBuxheti.TotaliPlanifikuar, ParameterDirection.Input);
            dbManager.AddParameters("@TOTALI", kokaBuxheti.Totali, ParameterDirection.Input);
            dbManager.AddParameters("@IDSTATUSDOK", kokaBuxheti.IdStatusDok, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERM", kokaBuxheti.IdNderm, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMVIT", kokaBuxheti.IdNdermVit > 0 ? kokaBuxheti.IdNdermVit : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@SHENIME", kokaBuxheti.Shenime, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", kokaBuxheti.IdPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters("@VITI", kokaBuxheti.Viti, ParameterDirection.Input);
            dbManager.AddParameters("@IDRAPORTDESIGN", kokaBuxheti.IdRaportDesign > 0 ? kokaBuxheti.IdRaportDesign : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@IDKOKAKONVERTIMINGA", kokaBuxheti.IdKokaKonvertimiNga > 0 ? kokaBuxheti.IdKokaKonvertimiNga : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@LLOJKONFIGKONVERTIMINGA", kokaBuxheti.LlojKonfigKonvertimiNga, ParameterDirection.Input);
            dbManager.AddParameters("@IDKATDOK", kokaBuxheti.IdKatDok, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMPOSTUESI", kokaBuxheti.IdNdermPostuesi > 0 ? kokaBuxheti.IdNdermPostuesi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@KODNDERMPOSTUESI", String.IsNullOrEmpty(kokaBuxheti.KodNdermPostuesi) ? null : kokaBuxheti.KodNdermPostuesi, ParameterDirection.Input);
            dbManager.AddParameters("@IDDOKPOSTUESI", kokaBuxheti.IdDokPostuesi > 0 ? kokaBuxheti.IdDokPostuesi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@TOTALIPATVSH", kokaBuxheti.TotaliPaTvsh, ParameterDirection.Input);
            dbManager.AddParameters("@LLOJVEPRIMIGJENERIMI", kokaBuxheti.LlojVeprimiGjenerimi > 0 ? kokaBuxheti.LlojVeprimiGjenerimi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@IDENTITETI", kokaBuxheti.IdEntiteti > 0 ? kokaBuxheti.IdEntiteti : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@IDBURIMI", kokaBuxheti.IdBurimi > 0 ? kokaBuxheti.IdBurimi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@STATUSAPROVIMI", (int)kokaBuxheti.StatusAprovimi > 0 ? (int)kokaBuxheti.StatusAprovimi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@DOKTORI", kokaBuxheti.Doktori > 0 ? kokaBuxheti.Doktori : (int?)null, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_update");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kokaBuxheti);
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        internal void ktheKokaBuxhetiSipasID(int idKoka, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKoka);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_KOKABUXHETI_merrSipasIdKoka", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKoka);
        }

        internal clsMesazh hidhNeHistorikBKokaBuxheti(int idKoka, int idStatusDok, int idPerdoruesi, int idNdermModifikuesi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKoka, idStatusDok, idPerdoruesi, idNdermModifikuesi);

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMMODIFIKUESI", idNdermModifikuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_hidhNeHistorik");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKoka, idStatusDok, idPerdoruesi, idNdermModifikuesi);
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        internal DataTable ktheDokumentBuxhetiPerEksport(int idNdermarrje, int idKatDok)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idKatDok);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKATDOK", idKatDok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_MerrDokumentBuxhetiPerEksport");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idKatDok);
            return ds.Tables[0];
        }

        internal bool EkzistonDokTjeterPerVit(int idViti, int idKatDok, int idNderm, int idKoka, string kodNdermPostuesi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idViti, idKatDok, idNderm, idKoka, kodNdermPostuesi);

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters("@VITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters("@IDKATDOK", idKatDok, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERM", idNderm, ParameterDirection.Input);
            dbManager.AddParameters("@IDBUXEHTIKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters("@KODINDERMPOSTUESI", kodNdermPostuesi, ParameterDirection.Input);
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_merrPlanifikimMiratimPerVit"));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idViti, idKatDok, idNderm, idKoka, kodNdermPostuesi);
            return nr == 1;
        }


        internal bool EkzistonDokBuxhetimiTjeterMeNrDokDate(string nrDok, DateTime? dtDok, int idKatDok, int idNderm, int idBuxhetiKoka, string kodiNdermPostuesi, int idNivel, int idKonfigAmbjenti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, nrDok, dtDok, idKatDok, idNderm, idBuxhetiKoka, kodiNdermPostuesi, idNivel, idKonfigAmbjenti);

            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters("@NRDOK", nrDok, ParameterDirection.Input);
            dbManager.AddParameters("@DTDOK", dtDok, ParameterDirection.Input);
            dbManager.AddParameters("@IDKATDOK", idKatDok, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERM", idNderm, ParameterDirection.Input);
            dbManager.AddParameters("@IDBUXHETIKOKA", idBuxhetiKoka, ParameterDirection.Input);
            dbManager.AddParameters("@KODINDERMPOSTUESI", kodiNdermPostuesi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNIVEL", idNivel, ParameterDirection.Input);
            dbManager.AddParameters("@IDKONFIGAMBJENTI", idKonfigAmbjenti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_merrDokBuxhetiPerNrDokDheDate");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, nrDok, dtDok, idKatDok, idNderm, idBuxhetiKoka, kodiNdermPostuesi, idNivel, idKonfigAmbjenti);
            if (ds.Tables[0] == null || ds.Tables[0].Rows.Count == 0)
                return false;
            else
                return true;
        }
        internal clsMesazh fshiDokBuxheti(int idKoka)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKoka);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_T_B_TRUPIBUXHETI_delete");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKoka);
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        internal clsMesazh PostoBuxhetim(int idDokPerPostim, int idPerdorues)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idDokPerPostim, idPerdorues);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDDOKPOSTIMI", idDokPerPostim, ParameterDirection.Input);
            dbManager.AddParameters("@IDPOSTUESI", idPerdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_posto");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idDokPerPostim, idPerdorues);
            return new clsMesazh(true, "Postimi perfundoi me sukses!");
        }

        internal clsMesazh updateStatusPostuar(int idDok, int idPerdorues)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idDok, idPerdorues);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDDOK", idDok, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_updateStatusPostuar");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idDok, idPerdorues);
            return new clsMesazh(true, "Statusi u ndryshua me sukses!");
        }

        internal bool eshtePostuarMiratimBuxhetiMeId(int idBuxhetiKoka)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idBuxhetiKoka);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDBUXHETIKOKA", idBuxhetiKoka, ParameterDirection.Input);
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_eshtePostuarMiratimi"));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idBuxhetiKoka);
            return nr == 1;
        }

        internal bool eshteKonvertuarDokMPBuxhetiMeId(int idBuxhetiKoka)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idBuxhetiKoka);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDBUXHETIKOKA", idBuxhetiKoka, ParameterDirection.Input);
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_eshteKonvertuar"));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idBuxhetiKoka);
            return nr == 1;
        }
        internal bool eshteKonvertuarPlotesishtDokBuxhetiMeId(int idBuxhetiKoka)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idBuxhetiKoka);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDBUXHETIKOKA", idBuxhetiKoka, ParameterDirection.Input);
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_eshteKonvertuarPlotesishtDokumentBuxheti"));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idBuxhetiKoka);
            return nr == 1;
        }
        internal clsMesazh updateStatusDokBuxheti(int idDok, int idPerdorues, int idStatusDok)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idDok, idPerdorues, idStatusDok);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@IDDOK", idDok, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters("@IDSTATUSDOK", idStatusDok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_updateStatusDok");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idDok, idPerdorues, idStatusDok);
            return new clsMesazh(true, "Statusi u ndryshua me sukses!");
        }
        internal void MerrKokaBuxheti(int idNderm, int viti, int idPerdoruesi, int idKatDok, bool gjitheDokumentat, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNderm, viti, idPerdoruesi, idKatDok, gjitheDokumentat);

            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters("@IDNDERMARRJE", idNderm, ParameterDirection.Input);
            dbManager.AddParameters("@VITI", viti, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters("@GJITHEDOK", gjitheDokumentat, ParameterDirection.Input);
            dbManager.AddParameters("@IDKATDOK", idKatDok, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_KOKABUXHETI_merrSipasNdermarrjesDheVitit", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNderm, viti, idPerdoruesi, idKatDok, gjitheDokumentat);
        }
        internal clsMesazh PostoAlokim(int idDokPerPostim, int idPerdorues)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idDokPerPostim, idPerdorues);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDDOKPERPOSTIM", idDokPerPostim, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_T_B_TRUPIBUXHETI_PostoDokAlokimi");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idDokPerPostim, idPerdorues);
            return new clsMesazh(true, "Postimi perfundoi me sukses!");
        }
        internal clsMesazh PostoDokBuxhetiSipasDestinacionitINS(int idDokPerPostim, int idKonfigAmbjenti, int idPerdorues)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idDokPerPostim, idKonfigAmbjenti, idPerdorues);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@IDDOKPERPOSTIM", idDokPerPostim, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters("@IDKONFIGAMBJENTI", idKonfigAmbjenti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_T_B_TRUPIBUXHETI_PostoDokBuxhetiInsSipasDestinacionit");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idDokPerPostim, idKonfigAmbjenti, idPerdorues);
            return new clsMesazh(true, "Postimi perfundoi me sukses!");
        }
        internal clsMesazh PostoDokBuxhetiSipasDestinacionitINSOutId(int idDokPerPostim, int idKonfigAmbjenti, int idPerdorues, out int idDokPostuar)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idDokPerPostim, idKonfigAmbjenti, idPerdorues);

            idDokPostuar = 0;
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@IDDOKINSERTED", idDokPostuar, ParameterDirection.Output);
            dbManager.AddParameters("@IDDOKPERPOSTIM", idDokPerPostim, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters("@IDKONFIGAMBJENTI", idKonfigAmbjenti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_T_B_TRUPIBUXHETI_PostoDokBuxhetiInsSipasDestinacionitOutInsertedId");
            idDokPostuar = int.Parse(dbManager.Parameters[0].Value.ToString());

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idDokPerPostim, idKonfigAmbjenti, idPerdorues);
            return new clsMesazh(true, "Postimi perfundoi me sukses!");
        }
        internal clsMesazh UpdateStatusRefuzuar(int idKoka, int idPerdorues, int idKonfigAmbjenti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKoka, idPerdorues, idKonfigAmbjenti);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@IDDOK", idKoka, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters("@IDKONFIGAMBJENTI", idKonfigAmbjenti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_updateStatusRefuzuar");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKoka, idPerdorues, idKonfigAmbjenti);
            return new clsMesazh(true, "Postimi perfundoi me sukses!");
        }
        internal clsMesazh ModifikoAlokimTeBija(int idPerdorues, int idKokaAlokimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idPerdorues, idKokaAlokimi);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters("@IDKOKAALOKIMI", idKokaAlokimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_T_B_TRUPIBUXHETI_PostoDokTeBija");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idPerdorues, idKokaAlokimi);
            return new clsMesazh(true, "Modifikimi te qendra perfundoi me sukses!");
        }
        internal clsMesazh ModifikoRialokimTeBijaDheMbesa(int idPerdorues, int idKokaRialokimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idPerdorues, idKokaRialokimi);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters("@IDBUXHETIKOKA", idKokaRialokimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_T_B_TRUPIBUXHETI_PostoDokTeBijaDheMbesa");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idPerdorues, idKokaRialokimi);
            return new clsMesazh(true, "Modifikimi te qendra perfundoi me sukses!");
        }

        internal int ktheIdStatusDokKokaBuxheti(int idkoka)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idkoka);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idkoka, ParameterDirection.Input);
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_merrIdStatusDok"));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idkoka);
            return nr;
        }

        public DataTable merrLlojeBurimi()
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_B_LLOJEBURIMI_select");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return ds.Tables[0];
        }
        #endregion

        #region B_TrupiBuxheti
        internal void ktheBTrupiBuxhetiSipasId(int idTrupi, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idTrupi);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDBUXHETITRUPI", idTrupi, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_TRUPIBUXHETI_merrSipasId", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idTrupi);
        }
        internal void ktheBTrupiBuxhetiSipasIdKoka(int idKoka, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKoka);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKoka, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_TRUPIBUXHETI_merrSipasIdKoka", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKoka);
        }

        internal clsMesazh RuajTrupiBuxheti(ClsBTrupiBuxheti trupiBuxheti)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            dbManager.Open();
            dbManager.CreateParameters(17);
            dbManager.AddParameters("@IDBUXHETITRUPI", trupiBuxheti.IdBuxhetiTrupi, ParameterDirection.Output);
            dbManager.AddParameters("@IDBUXHETIKOKA", trupiBuxheti.IdBuxhetiKoka, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", trupiBuxheti.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDKATEGORIBUXHETIMI", trupiBuxheti.IdKategoriBuxhetimi, ParameterDirection.Input);
            dbManager.AddParameters("@VLERAPLANIFIKUAR", trupiBuxheti.VleraPlanifikuar, ParameterDirection.Input);
            dbManager.AddParameters("@VLERA", trupiBuxheti.Vlera, ParameterDirection.Input);
            dbManager.AddParameters("@IDTRUPIKONVERTIMINGA", trupiBuxheti.IdTrupiKonvertimiNga, ParameterDirection.Input);
            dbManager.AddParameters("@LLOJKONVERTIMINGA", trupiBuxheti.LlojKonvertimiNga, ParameterDirection.Input);
            dbManager.AddParameters("@VLERAKATEGORISE", trupiBuxheti.VleraKategorise, ParameterDirection.Input);
            dbManager.AddParameters("@IDBUXHETI", trupiBuxheti.IdBuxheti, ParameterDirection.Input);
            dbManager.AddParameters("@LLOGARITNEGJENDJE", trupiBuxheti.LlogaritNeGjendje, ParameterDirection.Input);
            dbManager.AddParameters("@IDLLOJPERIUDHE", trupiBuxheti.IdLlojPeriudhe, ParameterDirection.Input);
            dbManager.AddParameters("@PERIUDHA", trupiBuxheti.Periudha, ParameterDirection.Input);
            dbManager.AddParameters("@IDOBJEKTI", trupiBuxheti.IdObjekti > 0 ? trupiBuxheti.IdObjekti : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@LLOJOBJEKTI", trupiBuxheti.LlojObjekti > 0 ? trupiBuxheti.LlojObjekti : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@VLERAPATVSH", trupiBuxheti.VleraPaTvsh, ParameterDirection.Input);
            dbManager.AddParameters("@IDTVSH", trupiBuxheti.IdTvsh > 0 ? trupiBuxheti.IdTvsh : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@CMIMI", trupiBuxheti.Cmimi, ParameterDirection.Input);
            dbManager.AddParameters("@SASIA", trupiBuxheti.Sasia, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_TRUPIBUXHETI_insert");
            trupiBuxheti.IdBuxhetiTrupi = int.Parse(dbManager.Parameters[0].Value.ToString());

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        internal void ruajTrupBuxhetiDT(DataTable rreshtaPerTuRuajtur)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@TRUPIBUXHETI_NDRYSHUAR", rreshtaPerTuRuajtur, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_TRUPIBUXHETI_MERGEDT");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
        }


        internal int KtheIdGjeneruarDokumenti(int idBuxhetiKoka)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idBuxhetiKoka);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@IDBUXHETIKOKA", idBuxhetiKoka, ParameterDirection.Input);
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KOKABUXHETI_kaGjeneruarDokument"));

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idBuxhetiKoka);
            return nr;
        }
        #endregion

        #region B_Komponente
        internal clsMesazh RuajKomponente(ClsBKomponente komponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, komponente);

            dbManager.Open();
            dbManager.CreateParameters(16);
            dbManager.AddParameters("@ID", komponente.Id, ParameterDirection.Output);
            dbManager.AddParameters("@IDNDERMARRJE", komponente.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDKRIJUESI", komponente.IdKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters("@IDMODIFIKUESI", komponente.IdModifikuesi > 0 ? komponente.IdModifikuesi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@DTKRIJIMI", komponente.DtKrijimi, ParameterDirection.Input);
            dbManager.AddParameters("@DTMODIFIKIMI", komponente.DtModifikimi, ParameterDirection.Input);
            dbManager.AddParameters("@KODI", komponente.Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@PERSHKRIMI", komponente.Pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters("@TIPI", komponente.Tipi, ParameterDirection.Input);
            dbManager.AddParameters("@NJESIA", komponente.Njesia, ParameterDirection.Input);
            dbManager.AddParameters("@IDBUXHETI", komponente.IdBuxheti > 0 ? komponente.IdBuxheti : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@FORMULA", komponente.Formula, ParameterDirection.Input);
            dbManager.AddParameters("@VLERAMIN", komponente.VleraMin, ParameterDirection.Input);
            dbManager.AddParameters("@VLERAMAX", komponente.VleraMax, ParameterDirection.Input);
            dbManager.AddParameters("@LLOJKUFIZIMI", komponente.LlojKufizimi, ParameterDirection.Input);
            dbManager.AddParameters("@AKTIVE", komponente.Aktive, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOMPONENTE_insert");
            komponente.Id = int.Parse(dbManager.Parameters[0].Value.ToString());

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, komponente);
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        internal clsMesazh ModifikoKomponente(ClsBKomponente komponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, komponente);

            dbManager.Open();
            dbManager.CreateParameters(16);
            dbManager.AddParameters("@ID", komponente.Id, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", komponente.IdNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDKRIJUESI", komponente.IdKrijuesi, ParameterDirection.Input);
            dbManager.AddParameters("@IDMODIFIKUESI", komponente.IdModifikuesi > 0 ? komponente.IdModifikuesi : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@DTKRIJIMI", komponente.DtKrijimi, ParameterDirection.Input);
            dbManager.AddParameters("@DTMODIFIKIMI", komponente.DtModifikimi, ParameterDirection.Input);
            dbManager.AddParameters("@KODI", komponente.Kodi, ParameterDirection.Input);
            dbManager.AddParameters("@PERSHKRIMI", komponente.Pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters("@TIPI", komponente.Tipi, ParameterDirection.Input);
            dbManager.AddParameters("@NJESIA", komponente.Njesia, ParameterDirection.Input);
            dbManager.AddParameters("@IDBUXHETI", komponente.IdBuxheti > 0 ? komponente.IdBuxheti : (int?)null, ParameterDirection.Input);
            dbManager.AddParameters("@FORMULA", komponente.Formula, ParameterDirection.Input);
            dbManager.AddParameters("@VLERAMIN", komponente.VleraMin, ParameterDirection.Input);
            dbManager.AddParameters("@VLERAMAX", komponente.VleraMax, ParameterDirection.Input);
            dbManager.AddParameters("@LLOJKUFIZIMI", komponente.LlojKufizimi, ParameterDirection.Input);
            dbManager.AddParameters("@AKTIVE", komponente.Aktive, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOMPONENTE_update");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, komponente);
            return new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
        }
        internal clsMesazh RuajKomponenteNeHistorik(ClsBKomponente komponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, komponente);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@ID", komponente.Id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOMPONENTEHISTORIK_insert");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, komponente);
            return new clsMesazh(true, "Ruajtja ne historik perfundoi me sukses!");
        }
        internal clsMesazh FshiKomponente(ClsBKomponente komponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, komponente);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@ID", komponente.Id, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOMPONENTE_delete");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, komponente);
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }
        internal clsMesazh RuajLidhjeKomponente(DataTable lidhjeKomponente)//nuk eshte bere prc
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@T_B_KOMPONENTELIDHJE_TYPE", lidhjeKomponente, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOMPONENTELIDHJE_MERGEDT");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return new MesazhSuksesi(MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        internal clsMesazh FshiLidhjeKomponente(int idKomponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponente);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOMPONENTELIDHJE_delete");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponente);
            return new MesazhSuksesi(MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        internal void ktheKomponenteSipasId(int id, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, id);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", id, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_KOMPONENTE_merrSipasIdKomponente", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, id);
        }
        internal void ktheKomponenteSipasNdermarrje(int idNdermarrje, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_KOMPONENTE_merrSipasIdNdermarrje", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje);
        }
        internal void MerrTeGjithaLidhjetKomponenteve(int idKomponente, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponente);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_KOMPONENTELIDHJE_merrAllLidhjet", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponente);
        }
        internal void KtheLidhjeKomponenteSipasIdKomponente(int idKomponente, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponente);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_KOMPONENTELIDHJE_merrSipasIdKomponente", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponente);
        }
        internal void KtheLidhjeKomponenteAllTePalidhuraSipasBuxhetit(int idKomponente, int idLlojBuxheti, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponente, idLlojBuxheti);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJBUXHETI", idLlojBuxheti, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_KOMPONENTE_merrAllTePalidhuraSipasBuxhetit", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponente, idLlojBuxheti);
        }
        internal void ktheLidhjeKomponenteAllSipasNdermarrjes(int idNdermarrje, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_KOMPONENTE_merrAllSipasNdermarrjes", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje);
        }
        internal clsMesazh RuajVlereKomponenteNeHistorik(ClsBKomponenteVlere komponenteVlere, bool ngaGjenerimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, komponenteVlere, ngaGjenerimi);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDKOKA", komponenteVlere.IdKoka, ParameterDirection.Input);
            dbManager.AddParameters("@NGAGJENERIMI", ngaGjenerimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOMPONENTEVLEREHISTORIK_insert");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, komponenteVlere, ngaGjenerimi);
            return new clsMesazh(true, "Ruajtja ne historik perfundoi me sukses!");
        }
        internal clsMesazh FshiVlereKomponente(ClsBKomponenteVlere komponenteVlere, bool ngaGjenerimi)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, komponenteVlere, ngaGjenerimi);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDKOKA", komponenteVlere.IdKoka, ParameterDirection.Input);
            dbManager.AddParameters("@NGAGJENERIMI", ngaGjenerimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOMPONENTEVLERE_delete");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, komponenteVlere, ngaGjenerimi);
            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }
        internal clsMesazh RuajVlereKomponente(DataTable komponenteVlere)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@T_B_KOMPONENTEVLERE_TYPE", komponenteVlere, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_B_KOMPONENTEVLERE_MERGEDT");

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi);
            return new MesazhSuksesi(MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        internal clsMesazh EkzistonKomponenteMeKeteKod(string kodi, int idNdermarrje)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kodi, idNdermarrje);

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            bool ekziston = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KOMPONENTE_ekzistonKomponenteMeKeteKod")) == 1;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kodi, idNdermarrje);
            return new clsMesazh(ekziston);
        }
        internal clsMesazh EshtePerdorurKomponenteNeFormule(string kodi, int idNdermarrje, bool vtmAktive)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, kodi, idNdermarrje, vtmAktive);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@VETEMAKTIVE", vtmAktive, ParameterDirection.Input);
            bool perdorur = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KOMPONENTE_eshtePerdorurKomponenteNeFormule")) == 1;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, kodi, idNdermarrje, vtmAktive);
            return new clsMesazh(perdorur, perdorur ? MessagesResource.Messages["msgKomponentePerdorurNeFormule"]: MessagesResource.Messages["msgKomponenteJoPerdorurNeFormule"]);
        }
        internal clsMesazh EshtePerdorurKomponenteNeVeprime(int idKomponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponente);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@ID", idKomponente, ParameterDirection.Input);
            bool perdorur = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KOMPONENTE_eshtePerdorurNeVeprime")) == 1;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponente);
            return new clsMesazh(perdorur, perdorur ? MessagesResource.Messages["msgKomponentePerdorurNeVeprime"] : "");
        }
        internal clsMesazh EshteLidhurMeNdermarrje(int idKomponente)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idKomponente);

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@ID", idKomponente, ParameterDirection.Input);
            bool lidhur = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_B_KOMPONENTE_eshteLidhurMeNdermarrje")) == 1;

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idKomponente);
            return new clsMesazh(lidhur, lidhur ? MessagesResource.Messages["msgKomponenteLidhurMeNdermarrje"] : "");
        }

        internal void merrKomponenteBuxhetiVlereSipasNdermarrjes(int idNdermarrje, int idPerdoruesi, int idNjesia, IDataBaseReader objectToFill)
        {
            ImbLogger.LogEnter(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idPerdoruesi, idNjesia);

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@NJESIA", idNjesia, ParameterDirection.Input);
            dbManager.FillCollection("prc_T_B_KOMPONENTEVLERE_merrKomponenteBuxhetiVlereSipasNdermarrjes", objectToFill);

            ImbLogger.LogExit(ImbLogger.LogTraceBuxhetimi, idNdermarrje, idPerdoruesi, idNjesia);
        }
        #endregion
    }
}
