using System;
using System.Collections.Generic;
using System.Data;
using DbCore.IMBUtils.DataBase;
using System.IO;
using System.Data.SqlClient;
using DbCore.IMBUtils.Logging;
using System.Linq;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbShare
{
    public class clsDatabaseShare : DbData
    {
        public clsDatabaseShare()
            : base()
        {
        }



        public clsDatabaseShare(DbData db) : base(db)
        {
        }
        public clsDatabaseShare(string connectionName) : base(connectionName)
        {

        }

        #region RAPORTET

        internal void MerrNjoftimeSipasDates(int idperdorues, DateTime data, IDataBaseReader colNjoftime)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DATE_NJOFTIMI", data, ParameterDirection.Input);
            dbManager.FillCollection("T_NJOFTIME_PERDORUES_MerrSipasDates", colNjoftime);

        }

        internal DataTable ktheListRaportesh(int idNdermarrje, int idPerdoruesi, int idGjuha, int idViti, int idModuli)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@idndermarje", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idgjuha", idGjuha, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDMODULI", idModuli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@idperdorues", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@idViti", idViti, ParameterDirection.Input);

            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_Moduli_RaportetSipasTeDrejtave").Tables[0];
        }
        internal string ktheKonfigMenuMajtas(int idPerdoruesi, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return (string)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LISTAKONFIGURIMEVEMENUMAJTAS_sel");
        }
        internal clsMesazh ruajKonfigMenuMajtas(int idPerdoruesi, int idNdermarrje, string konfigurimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KONFIGURIMI", konfigurimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LISTAKONFIGURIMEVEMENUMAJTAS_DelAndIns");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }
        internal string ktheListKonfigRaportesh(int idPerdoruesi, int idNdermarrje, int idModuli)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDMODULI", idModuli, ParameterDirection.Input);
            return (string)dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LISTAKONFIGURIMEVERAP_sel");
        }

        internal clsMesazh ruajKonfigListRaportesh(int idPerdoruesi, int idNdermarrje, int idModuli, string konfigurimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDMODULI", idModuli, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KONFIGURIMI", konfigurimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_LISTAKONFIGURIMEVERAP_DelAndIns");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh ShtoRapxDesign(int idNdermarrje, int idRaportDesign, bool zgjedhur)
        {

            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@idRaportDesign", idRaportDesign, ParameterDirection.Input);
            dbManager.AddParameters("@zgjedhur", zgjedhur, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_RAPXDESIGN_ins");
            return new clsMesazh(true, "Insert successful");
        }

        internal int RuajRaportDesign(int idRaporti, string pershkrim, string fileName, string fileNamePortrait, bool iModifikueshem, int idPrindi, bool ruajNeSession, bool autoWidth, bool rollPaper, bool oldViewer)
        {
            dbManager.Open();
            dbManager.CreateParameters(11);
            dbManager.AddOutputParametersWithSize("@IdRaportDesign", -1, 10);
            dbManager.AddInputParameters("@idRaporti", idRaporti);
            dbManager.AddInputParameters("@pershkrimi", pershkrim);
            dbManager.AddInputParameters("@filename", fileName);
            dbManager.AddInputParameters("@fileNamePortrait", fileNamePortrait);
            dbManager.AddInputParameters("@iModifikueshem", iModifikueshem);
            dbManager.AddInputParameters("@idPrindi", idPrindi);
            dbManager.AddInputParameters("@ruajNeSession", ruajNeSession);
            dbManager.AddInputParameters("@autoWidth", autoWidth);
            dbManager.AddInputParameters("@rollPaper", rollPaper);
            dbManager.AddInputParameters("@oldViewer", oldViewer);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_Raportdesing_ins");
            int.TryParse(dbManager.Parameters[0].Value.ToString(), out var idRaportDesign);

            return idRaportDesign;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idRaporti"></param>
        /// <returns></returns>
        internal DataRow merrRaport(int idRaporti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDRAPORTI", idRaporti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RAPORTI_merrRaport");
            if (ds.Tables[0].Rows.Count > 1)
                throw new Exception("ERROR: Ka me shume se nje raport me id-ne:" + idRaporti);
            return ds.Tables[0].Rows.Count == 1 ? ds.Tables[0].Rows[0] : null;
        }
        internal DataRow merrRaportSipasEmritReal(string emerReal)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@emerReal", emerReal, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RAPORTI_merrRaportSipasEmerReal");
            if (ds.Tables[0].Rows.Count > 1)
                throw new Exception("ERROR: Ka me shume se nje raport me emrin:" + emerReal);
            return ds.Tables[0].Rows.Count == 1 ? ds.Tables[0].Rows[0] : null;
        }

        /// <summary>
        /// Ekzekuton SP : prc_T_RAPORTI_merrSubRaportet e cila kthen te gjitha subraportet e raportit qe i kalohet si parameter
        /// </summary>
        /// <param name="idSuperRaport">Id e superRaportit</param>
        /// <returns></returns>
        internal DataTable ktheGjitheSubRaportet(int idSuperRaport)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSUPERRAPORT", idSuperRaport, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RAPORTI_merrSubRaportet");
            return ds.Tables[0];
        }

        /// <summary>
        /// merr pemen e raporteve
        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idViti"></param>
        /// <returns></returns>
        public DataTable merrSiteMap(int idNdermarrje, int idPerdoruesi, int idViti)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);

            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);

            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RAPORTE_SITEMAP").Tables[0];
        }

        internal DataTable merrSipasNdermarjedheRaport(int idndermarje, int idRaporti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDRAP", idRaporti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RAPXDESIGN_merrFileRaporti");
            return ds.Tables[0];
        }

        internal DataTable merrSipasNdermarjedheEmerRaport(int idndermarje, string emerReal)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@emerReal", emerReal, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RAPXDESIGN_merrFileRaportiSipasEmritReal");
            return ds.Tables[0];
        }

        internal int merrIdRaportSipasDesign(int idRaportDesign)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDDESIGN", idRaportDesign, ParameterDirection.Input);
            int idRaporti = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_RAPORTI_merrIdRaportSipasIdDesign"));

            if (idRaporti <= 0)
                throw new Exception("ERROR: Nuk gjendet nje raport me designin me id:" + idRaportDesign);
            return idRaporti;
        }
        internal clsMesazh RuajZgjedhjeDizajniPerNdermarrje(int idNdermarrje, int idDesign)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters("@IDDESIGN", idDesign, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_RAPXDESIGN_updZgjedhur");
            return new clsMesazh(true, "Zgjedhja e dizajnit u ruajt me sukses!");
        }

        internal DataRow GetReportDesignSettings(int idReportDesign)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDREPORTDESIGN", idReportDesign, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RAPORTDESING_GetReportDesignSettings");

            if (ds.Tables[0].Rows.Count > 1)
                throw new Exception("ERROR: Ka me shume se nje raport design me id-ne:" + idReportDesign);
            return ds.Tables[0].Rows.Count == 1 ? ds.Tables[0].Rows[0] : null;
        }
        internal int ktheIdSubRaporti(int idSuperRaporti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSUPERRAPORT", idSuperRaporti, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_RAPORTI_ktheIdSubRaporti"));
        }

        internal int merrIdRaportSipasEmerReal(String emriReal)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@emriReal", emriReal, ParameterDirection.Input);
            int idRaporti = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_RAPORTI_merrIdRaportSipasEmerReal"));

            if (idRaporti < 0)
                throw new Exception("ERROR: Nuk gjendet nje raport me emrin:" + emriReal);
            return idRaporti;
        }
        internal int merrIdRaportPivotGridSipasIdModuli(int idModulPivotGrid)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMODULPIVOTGRID", idModulPivotGrid, ParameterDirection.Input);
            int idRaporti = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_RAPORTI_selIdRapPivotGridSipasModulit"));
            return idRaporti;
        }

        internal DataRow merrSipasRaportDesignId(int idrapdesing)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDRAPORTDESING", idrapdesing, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RAPXDESIGN_merrSipasId");

            if (ds.Tables[0].Rows.Count > 1)
                throw new Exception("ERROR: Ka me shume se nje raport design me id-ne:" + idrapdesing);
            return ds.Tables[0].Rows.Count == 1 ? ds.Tables[0].Rows[0] : null;
        }
        internal DataRow merrRaportSipasEmritReal(string emri, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@emri", emri, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idndermarje", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_merrRaportSipasEmerReal");

            if (ds.Tables[0].Rows.Count > 1)
                throw new Exception("ERROR: Ka me shume se nje raport design me emrin:" + emri);
            return ds.Tables[0].Rows.Count == 1 ? ds.Tables[0].Rows[0] : null;
        }

        internal DataTable merrRaportDesignSipasKategorise(int idkategori, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKATEGORI", idkategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RAPXDESIGN_merrDesingSipasKategorise").Tables[0];
        }
        internal DataTable merrRaportDesignSipasKategorisePerMobile(int idkategori, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKATEGORI", idkategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FORMATEPRINTIMIMOBILE_merrDesingSipasKategorisePerMobile").Tables[0];
        }

        internal DataTable merrDesignSipasRaportit(int idRaporti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDRAPORTI", idRaporti, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RAPORTIDESING_merrTeGjithDizajnetSipasRaportit").Tables[0];
        }

        internal DataTable merrTeGjitheRaportetMeFormatPrintimi()
        {
            dbManager.Open();
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RAPORTI_merrTeGjithRaportetMeDizajn").Tables[0];
        }

      

        internal DataRow merrRaportDesignOrigjinalSipasIdDesign(int idRaportDesign)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddInputParameters("@IDRAPORTDESIGN", idRaportDesign);
            var ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RAPORTDESING_merrDesingOrigjinal");
            if (ds.Tables[0].Rows.Count > 1)
                throw new Exception("ERROR: Ka me shume se nje rapor design me id-ne:" + idRaportDesign);
            return ds.Tables[0].Rows.Count == 1 ? ds.Tables[0].Rows[0] : null;
        }

        public bool kaSubRaporte(int idRaport)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSUPERRAPORT", idRaport, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LIDHJERAPORTI_kaSubRaporte"));
            return Convert.ToBoolean(pergjigje);
        }


        public bool ekzistonPershkrimiNeDB(string pershkrim, int idRaporti)
        {

            dbManager.Open();
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PERSHKRIM", pershkrim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDRAPORTI", idRaporti, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_RAPORTDESIGN_merrPershkrim"));
            return pergjigje != 0;


        }

        public bool kaFormatJoBuxhetor(int idRaport, int idNdermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDRAPORTI", idRaport, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarje, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "T_PASQYRAFINANCIAREKOKA_kaFormatJoBuxhetor"));
            if (pergjigje != 0)
                return true;
            else
                return false;
        }

        #endregion RAPORTET

        #region RAPORTE TRUPI

        internal clsMesazh ruajRaportTrupi(int idRpT, int idRp, int idK, decimal gj, int rend, int drejt)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDRAPTRUPI", idRpT, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDRAP", idRp, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOLONE", idK, ParameterDirection.Input);
            dbManager.AddParameters(3, "RAPTRUPIGJERESI", gj, ParameterDirection.Input);
            dbManager.AddParameters(4, "@RAPTRUPIRENDIT", rend, ParameterDirection.Input);
            dbManager.AddParameters(5, "@RAPTRUPIDREJTIM", drejt, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_RAPORTTRUPI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh FshiArkiva(int idPerdoruesi, string path)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@PATH", path, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARKIVA_fshiSipasPathit");
            return new clsMesazh(true);
        }

        internal clsMesazh modifikoRaportTrupi(int idRpT, int rend, int drejt, bool vis)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDRAPORTTRUPI", idRpT, ParameterDirection.Input);
            dbManager.AddParameters(1, "@RAPTRUPIRENDIT", rend, ParameterDirection.Input);
            dbManager.AddParameters(2, "@RAPTRUPIDREJTIM", drejt, ParameterDirection.Input);
            dbManager.AddParameters(3, "@RATRUPIVISIBLE", vis, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_RAPORTTRUPI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiRaportTrupi(int idRp)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDRAP", idRp, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_RAPORTTRUPI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal DataTable ktheRaportTrupi(int idRap)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDRAP", idRap, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_RAPTRUPI_sel");
            return ds.Tables[0];
        }

        #endregion RAPORTE TRUPI

        #region SP

        /// <summary>
        ///
        /// </summary>
        /// <param name="idSp"></param>
        /// <returns></returns>
        internal DataRow merrSP(int idSp)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSP", idSp, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_SP_sel");
            if (ds.Tables[0].Rows.Count > 1)
                throw new Exception("ERROR: Ka me shume se nje sp me nje id-ne:" + idSp);
            return ds.Tables[0].Rows.Count == 1 ? ds.Tables[0].Rows[0] : null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idSp"></param>
        /// <returns></returns>
        internal string merrEmerSP(int idSp)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSP", idSp, ParameterDirection.Input);
            string emerSp = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_SP_merrEmer").ToString();
            return emerSp;
        }

        /// <summary>
        /// Merr parametrat e sp-se me id te dhene ne input
        /// </summary>
        /// <param name="idSp">id-ja e sp-se</param>
        /// <returns>DataTable e tipit parameter,null perndryshe nese nuk ka parametra ose nuk ekziston sp-ja</returns>
        internal DataTable merrParametraSp(int idSp)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSP", idSp, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PARAM_ktheParametraSp");
            return ds.Tables[0];
        }

        /// <summary>
        /// Merr parametrat e rapotit, perfshire parametrat e sp dhe parametrat e raprtit
        /// </summary>
        /// <param name="idSp">id-ja e sp-se</param>
        /// /// <param name="idRaport">id-ja e raportit</param>
        /// <returns>DataTable e tipit parameter,null perndryshe nese nuk ka parametra ose nuk ekziston sp-ja</returns>
        internal DataTable merrParametraSpRaport(int idSp, int idRaport)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDSP", idSp, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDRAPORT", idRaport, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PARAM_ktheParametraSpRaport");
            return ds.Tables[0];
        }

        /// <summary>
        /// Ekzekuton SP : prc_T_PARAM_ktheParamPerbashketRaporte e cila kthen parametrat e perbashket midis dy raporteve qe i kalohen si param
        /// </summary>
        /// <param name="idRaporti1">ID e raportit te pare</param>
        /// <param name="idRaporti2">ID e raportit te dyte</param>
        /// <returns></returns>
        internal DataTable merrParametatPerbshketRapotesh(int idRaporti1, int idRaporti2)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDRAPORT1", idRaporti1, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDRAPORT2", idRaporti2, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PARAM_ktheParamPerbashketRaporte");
            return ds.Tables[0];
        }

        #endregion SP

        #region SP TRUPI

        internal DataTable mbushParamXSp()
        {
            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_PARAMXSP_sel");
            return ds.Tables[0];
        }

        #endregion SP TRUPI

        #region TRUPI I FILTRAVE

        /// <summary>
        ///
        /// </summary>
        /// <param name="idKokaFilter"></param>
        /// <returns></returns>
        internal DataTable merrFilterTrupin(int idKokaFilter)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKA", idKokaFilter, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FILTERTRUPI_selByKoka");
            return ds.Tables[0];
        }

      
        /// <summary>
        ///
        /// </summary>
        /// <param name="idKokaFilter"></param>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        internal clsMesazh krijoFilterTrupi(DataTable dataTable)
        {
            dbManager.Open();
            dbManager.ClearParameters(); //perdoret per te pastruar parametrat qe perdoren per select
            dbManager.CreateInsertParameters(3);
            dbManager.AddInsertParameters(0, "@IDKOKAFILTER", DbType.Decimal, "IDKOKAFILTER");
            dbManager.AddInsertParameters(1, "@IDKONTROLLI", DbType.Decimal, "IDKONTROLLI");
            dbManager.AddInsertParameters(2, "@VLERA", DbType.String, "VLERA");
            if (dbManager.ExecuteInsert(dataTable, CommandType.StoredProcedure, "prc_T_FilterTrupi_sel", CommandType.StoredProcedure, "prc_T_FILTERTRUPI_ins"))
                return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return new clsMesazh(false, "Ruajtja nuk u krye!"); ;
        }

        internal clsMesazh ruajFilterTrupi(int idKokaFilter, int idKontrolli, string vlera)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKOKAFILTER", idKokaFilter, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKONTROLLI", idKontrolli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@VLERA", vlera, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FILTERTRUPI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

       
        internal clsMesazh fshiFilterTrupi(int idTrupFilter)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDTRUPIFILTER", idTrupFilter, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FILTERTRUPI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }


        #endregion TRUPI I FILTRAVE

        #region KOKA E FILTRAVE

    
        internal DataTable merrTrupinFiltri(int idKokaFilter)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAFILTER", idKokaFilter, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FILTERTRUPI_merrTrupinMeIdKoka");
            //DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FILTERKOKA_merrTrupin");
            return ds.Tables[0];
        }

        /// <summary>
        /// Merr Filtrat per idmoduli, idperdorues, idndermarrje
        /// </summary>
        /// <param name="idModuli"></param>
        /// <param name="idPerdorues"></param>
        /// <param name="idNdermarrje"></param>
        /// <returns>Kthen DataTable</returns>
        internal DataTable merrFilterPerModul(int idRaporti, int idPerdorues, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@idraporti", idRaporti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idPerdorues", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idNdermarrje", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FILTERKOKA_PER_MODUL_sel");
            return ds.Tables[0];
        }

        internal DataTable merrFiltratRaporteveSipasModulit(int idModul, int idPerdorues, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@idModul", idModul, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idPerdorues", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idNdermarrje", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FILTERKOKA_SIPAS_MODULIT_sel");
            return ds.Tables[0];
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idRaporti"></param>
        /// <returns></returns>
        internal DataTable merrFilterPerRaport(int idRaporti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idraporti", idRaporti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FILTER_PER_RAPORT_sel");
            return ds.Tables[0];
        }

     

        internal DataTable merrKomponentPerFilter(int idFilter)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@idfiltri", idFilter, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KOMPONENTE_PER_FILTER_sel");
            return ds.Tables[0];
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="idKokaFilter"></param>
        /// <param name="kodi"></param>
        /// <param name="pershkrimi"></param>
        /// <param name="idperdoruesi"></param>
        /// <param name="idndermarrje"></param>
        /// <param name="idRaport"></param>
        /// <param name="localRaprot"></param>
        /// <param name="localPerdorues"></param>
        /// <param name="localNdermarrje"></param>
        /// <returns></returns>
        internal clsMesazh krijoFilterKoka(out int idKokaFilter, String kodi, String pershkrimi, int idperdoruesi, int idndermarrje,
            int idRaport, bool localRaprot, bool localPerdorues, bool localNdermarrje, int idstatusdok)
        {
            idKokaFilter = -1;

            dbManager.Open();
            dbManager.CreateParameters(10);
            dbManager.AddParameters(0, "@IDKOKAFILTER", idKokaFilter, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KOKAFILTERKODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOKAFILTERPERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", idndermarrje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDRAP", idRaport, ParameterDirection.Input);
            dbManager.AddParameters(6, "@LOCALRAPORT", localRaprot, ParameterDirection.Input);
            dbManager.AddParameters(7, "@LOCALPERDORUES", localPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(8, "@LOCALNDERMARRJE", localNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FILTERKOKA_ins");

            idKokaFilter = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh ruajFilterKoka(out int idKokaFilter, String kodi, String pershkrimi, int idperdoruesi, int idndermarrje, int idstatusdok)
        {
            idKokaFilter = -1;

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDKOKAFILTER", idKokaFilter, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KOKAFILTERKODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOKAFILTERPERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", idndermarrje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FILTERKOKA_ins");

            idKokaFilter = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        [Obsolete("Perdor: clsMesazh ruajFilterKoka(out int idKokaFilter, String kodi, String pershkrimi, int idperdoruesi, int idndermarrje)", true)]
        public clsMesazh ruajFilterKoka(clsFilterKoka koka)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDKOKAFILTER", koka.IdKokaFilter, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KOKAFILTERKODI", koka.KokaFilterKodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOKAFILTERPERSHKRIMI", koka.KokaFilterPershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", koka.IdPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", koka.IdNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FILTERKOKA_ins");

            koka.IdKokaFilter = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh modifikoFilterKoka(int idKokaFilter, String kodi, String pershkrimi, int idperdoruesi, int idndermarrje, int idstatusdok)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDKOKAFILTER", idKokaFilter, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KOKAFILTERKODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOKAFILTERPERSHKRIMI", pershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", idndermarrje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FILTERKOKA_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        [Obsolete("Perdor: clsMesazh modifikoFilterKoka(int idKokaFilter, String kodi, String pershkrimi, int idperdoruesi, int idndermarrje)", true)]
        public clsMesazh modifikoFilterKoka(clsFilterKoka koka)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDKOKAFILTER", koka.IdKokaFilter, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KOKAFILTERKODI", koka.KokaFilterKodi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KOKAFILTERPERSHKRIMI", koka.KokaFilterPershkrimi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", koka.IdPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", koka.IdNdermarrje, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FILTERKOKA_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiFilterKoka(int idKokaFilter)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAFILTER", idKokaFilter, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FILTERKoka_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiFilterKokaStatus(int idKokaFilter, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOKAFILTER", idKokaFilter, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FILTERKoka_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        [Obsolete("Perdor: clsMesazh fshiFilterKoka(int idKokaFilter)", true)]
        public clsMesazh fshiFilterKoka(clsFilterKoka koka)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOKAFILTER", koka.IdKokaFilter, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FILTERKoka_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal DataTable ktheGjitheFilterKokaModuli(int idModuli)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMODULI", idModuli, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FILTERKOKA_selDefault");
            return ds.Tables[0];
        }



        #endregion KOKA E FILTRAVE

        public clsMesazh RuajLidhjenRaportFilter(int idRap, int idKoka)
        {


            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDRAP", idRap, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDFILTERKOKA", idKoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_RAPFILTER_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        #region Kontrollet

        /// <summary>
        /// Merr kontrollet nga db-ja
        /// </summary>
        /// <returns>DataTable me kontrollet e lexuara nga db-ja, null perndryshe</returns>
        internal DataTable merrKontrollet()
        {
            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLE_ktheKontrollet");
            return ds.Tables[0];
        }

        internal DataTable merrKontrolletERaportit(int idRaporti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDRAPORTI", idRaporti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLE_ktheKontrolletERaportit");
            return ds.Tables[0];
        }

        internal DataTable merrKontrolletRaportiSipasIdSp(int idSp)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSP", idSp, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLE_ktheKontrolletERaportitSipasIdSp");
            return ds.Tables[0];
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="idKomponente"></param>
        /// <returns></returns>
        internal DataTable merrKontrolletKomponentes(int idKomponente)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLE_ktheKontrolletKomponentes");
            return ds.Tables[0];
        }

        internal DataRow merrGrupKontrolli(int idGrupi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDGRUP", idGrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLEGRUP_ktheGrup");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// Lexon nga db-ja kontrollin me id idkontroll
        /// </summary>
        /// <param name="idkontroll">id-ja e kontrollit qe do lexohet</param>
        /// <returns>Kthen DataRow me te dhenat e kontrollit, null perndryshe</returns>
        internal DataRow merrKontrollin(int idkontroll)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONTROLL", idkontroll, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLE_ktheKontroll");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// Lexon nga db-ja kontrollin me kodin kodKontroll dhe me idKomponente te dhene
        /// </summary>
        /// <param name="idkontroll">kodi i kontrollit qe do lexohet</param>
        /// <param name="idKomponente">id e komponentes se kontrollit qe do lexohet</param>
        /// <returns>Kthen DataRow me te dhenat e kontrollit, null perndryshe</returns>
        internal DataRow merrKontrollinSipasKodit(string kodKontroll, int idKomponente)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODKONTROLL", kodKontroll, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLE_ktheKontrollSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// Lexon nga db-ja kontrollin me kodin kodKontroll dhe me idKomponente te dhene
        /// </summary>
        /// <param name="idkontroll">kodi i kontrollit qe do lexohet</param>
        /// <param name="idKomponente">id e komponentes se kontrollit qe do lexohet</param>
        /// <returns>Kthen DataRow me te dhenat e kontrollit, null perndryshe</returns>
        internal int ktheTipKontrolli(int idKontrolli)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ID", idKontrolli, ParameterDirection.Input);
            int pergjigja = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONTROLLE_ktheTipKontrolli"));
            return pergjigja;
        }

        internal DataTable merrKontrolletEParametrit(int idParameter)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPARAMETER", idParameter, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLE_merrKontrolletEParametrit");
            return ds.Tables[0];
        }

        /// <summary>
        /// Kthen DataTable te tipit Kontroll per id-ne e parametrit
        /// </summary>
        /// <param name="idSpTrupi">id-ja e dhene ne input</param>
        /// <returns>Kthen DataTable te tipit kontroll, null perndryshe</returns>
        internal DataTable merrKontrolleTeParametrit(int idSpTrupi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDSPTRUPI", idSpTrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLE_ktheKontrolletParametrit");
            return ds.Tables[0];
        }

        #endregion Kontrollet

        #region KONFIGURIMI I DOKUMENTAVE

        internal DataTable merrKonfigDefaultKomponentes(int idKomponente, int idndermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idndermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigDefaultKomponentes");
            return ds == null ? null : ds.Tables[0];
        }

        internal DataRow merrKonfigDefaultNivel(int idNivel, int idndermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNIVEL", idNivel, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idndermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigDefaultNivelRegjistrimi");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal int merrIdKonfigDefaultNivel(int idNivel, int idndermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNIVEL", idNivel, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idndermarrje, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheIdKonfigDefaultNivelRegjistrimi"));
        }

        internal int merrIdKonfigDefaultNivelMeAutorizim(int idNivel, int idndermarrje, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNIVEL", idNivel, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idndermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheIdKonfigDefaultNivelRegjistrimiMeAutorizim"));
        }

        internal int merrIdKategoriNgaKonfigAmbjente(int idKonfigAmbjenti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTI", idKonfigAmbjenti, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheIdKategoriNgaIdKonfigAmbjenti"));
        }

        internal int merrIdNivelNgaKonfigAmbjente(int idKonfigAmbjenti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTI", idKonfigAmbjenti, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheIdNivelNgaIdKonfigAmbjenti"));
        }

        internal string merrKodNivelNgaKonfigAmbjente(int idKonfigAmbjenti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTI", idKonfigAmbjenti, ParameterDirection.Input);
            return dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKodKonfigNgaIdKonfigAmbjenti").ToString();
        }

        internal int merrIdNiveliSipasIdKonfigurimi(int idKonfigAmbjenti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTE", idKonfigAmbjenti, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheIdIdNiveliSipasIdKonfigurimi"));
        }

        internal int merrIdNiveliSipasIdKonfigurimiMeAutorizim(int idKonfigAmbjenti, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTE", idKonfigAmbjenti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheIdIdNiveliSipasIdKonfigurimiMeAutorizim"));
        }

        internal DataTable merrKonfigDefaultKategori(int idKategori)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKATDOK", idKategori, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigDefaultKategori");
            return ds.Tables[0];
        }

        internal DataTable merrKonfigDefaultKategori(int idKategori, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKATDOK", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigSipasKategoriDheIdndermarrje");
            return ds.Tables[0];
        }
        internal DataRow merrKonfiguriminMeKod(string kodKonfig, int idndermarje, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODKONFIG", kodKonfig, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfiguriminMeKodSipasGjuhes");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// Kthen IdKonfigurimAmbjentin - mund the throw SqlException
        /// </summary>
        /// <param name="kodKonfig">Kodi konfigurimit</param>
        /// <param name="idndermarje"> Id e ndermarrjes</param>
        /// <returns>IdKonfigurimAmbjentin</returns>
        internal int merrIdKonfigurimiMeKod(string kodKonfig, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODKONFIG", kodKonfig, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            int idKonfig = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheIdKonfigurimiMeKod"));
            return idKonfig;
        }

        internal DataRow merrIdKonfigurimiTeAmortizimeve(int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheIdKonfigurimiTeAmortizimeve");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal int ktheIdKonfigAmbjentiSipasKategoriDheNderm(int idKategori, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKATDOK", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            object objIdKonfig = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheIdKonfigPerIdKatDheNderm");
            if (objIdKonfig != null && objIdKonfig != DBNull.Value)
                return Convert.ToInt32(objIdKonfig);
            else
                return 0;
        }

        internal double? ktheVlereKonfigAmbjentiSipasVleresKushttShitje (int vlera)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDvlera", vlera, ParameterDirection.Input);
            object vleraRe = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ALTERNATIVAKUSHTI_MINIMUM_SHITJE_ktheVlereSipasId");
            if (vleraRe != null && vleraRe != DBNull.Value)
                return Convert.ToDouble(vleraRe);
            else
                return null;
        }

        internal int  ktheIdKonfigAmbjentiSipasVleresSeMinKushttShitje (String vlere)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@VLERA", vlere, ParameterDirection.Input);
            object vleraRe = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ALTERNATIVAKUSHTI_MINIMUM_SHITJE_ktheIdShtimOseJO");
            if (vleraRe != null && vleraRe != DBNull.Value)
                return Convert.ToInt32(vleraRe);
            else
                return 1;
        }

        internal string ktheVlereKushtiSipasIdAlternativeMultiselect(int idAlternativa)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDALTERNATIVA", idAlternativa, ParameterDirection.Input);
            object vlera = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ALTERNATIVAKUSHTI_MULTISELECT_LUPA_ktheVlereSipasId");
            if (vlera != null && vlera != DBNull.Value)
                return Convert.ToString(vlera);
            else
                return "";
        }

        internal int ktheIdKonfigAmbjentiSipasVleresSeKushtitMultiselectLupa(string vlere)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@VLERA", vlere, ParameterDirection.Input);
            object idAlternativa = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ALTERNATIVAKUSHTI_MULTISELECT_LUPA_ktheIdShtimOseJO");
            if (idAlternativa != null && idAlternativa != DBNull.Value)
                return Convert.ToInt32(idAlternativa);
            else
                return 1;
        }

        internal DataTable merrKonfigurimKategori(int idKategori, int idndermarje, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKATDOK", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigurimKategori");
            return ds.Tables[0];
        }

        internal bool IsRequiredField(int idKonfigurimi, string kodKontrolli, int idKomponente)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddInputParameters("@IDKONFIGAMBJENTE", idKonfigurimi);
            dbManager.AddInputParameters("@KODKONTROLL", kodKontrolli);
            dbManager.AddInputParameters("@IDKOMPON", idKomponente);
            return Convert.ToBoolean(Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ATRIBUTETRUPI_ktheDetyrueshmeSipasKontrolleveSipasKonfigurimitDheKontrollit")));
        }

        internal DataTable merrKonfigurimSipasSuperKategori(int idKategori, int idndermarje, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDSUPERKAT", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigurimSipasSuperKategori");
            return ds.Tables[0];
        }

        internal DataRow merrKonfiguriminMeID(int idKonfig)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIG", idKonfig, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfiguriminMeID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow merrkonfigurimPaLloj(int idKonfig)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIG", idKonfig, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfiguriminMeIDPALLOJ");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable merrAtributetKontrolleveKomponentes(int idKomponente, int idKategori)
        {// kthen vlerat e kontrolleve per konfigurimin default
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKATDOK", idKategori, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ATRIBUTETRUPI_ktheAtributetKontrolleveKomponentes");
            return ds.Tables[0];
        }

        internal DataTable merrAtributetKontrolleveSipasKonfigurimit(int idkonf)
        {// kthen vlerat e kontrolleve per konfigurimin default
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTE", idkonf, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ATRIBUTETRUPI_ktheAtributetKontrolleveSipasKonfigurimit");
            return ds.Tables[0];
        }

        internal DataRow merrAtributetKontrolleveSipasKonfigurimitDheKontrollit(int idGjuha, int idkonf, string kodKontroll, int idKompon)
        {// kthen vlerat e kontrolleve per konfigurimin default
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTE", idkonf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODKONTROLL", kodKontroll, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOMPON", idKompon, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ATRIBUTETRUPI_ktheAtributetKontrolleveSipasKonfigurimitDheKontrollit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataTable merrKontrolletKonfigurimitKomponentes(int idGjuha, int idKomponente, int idKonfigurim)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKONFIGURIM", idKonfigurim, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLE_ktheKontrolletKonfigurimitKomponentes");
            return ds.Tables[0];
        }

        internal DataTable merrKontrolletKonfigurimit(int idGjuha, int idKomponente, int idKonfigurim)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKONFIGURIM", idKonfigurim, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONTROLLE_ktheKontrolletKonfigurimit");
            return ds.Tables[0];
        }

        internal clsMesazh ruajKonfigurim(out int idkonfigambjente, string kodkonfigambjente, string pershkrimkonfigambjente_sq, int idkategori, int radha, bool defaultkonfigambjente, int nivel, int SK, int idnderm, int idkonf, int idstatusdok, int idperdorues, int idKonfigFormatNr, string pershkrimKonfigAmbjente_en, int lloji, int formatmobile, string pershkrimKonfigAmbjente_fr)
        {
            idkonfigambjente = -1;

            dbManager.Open();
            dbManager.CreateParameters(17);
            dbManager.AddParameters(0, "@IDKONFIG", idkonfigambjente, ParameterDirection.Output);
            dbManager.AddParameters(1, "@KODKONFIG", kodkonfigambjente, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMKONFIG", pershkrimkonfigambjente_sq, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKATDOK", idkategori, ParameterDirection.Input);
            dbManager.AddParameters(4, "@RADHA", radha, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DEFAULT", defaultkonfigambjente, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNIVEL", nivel, ParameterDirection.Input);
            if (SK == 0) dbManager.AddParameters(7, "@IDSKEMKONT", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDSKEMKONT", SK, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            if (idkonf == 0) dbManager.AddParameters(9, "@IDKONFIGURIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@IDKONFIGURIMI", idkonf, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            if (idKonfigFormatNr == 0 || idKonfigFormatNr == -1)
                dbManager.AddParameters(12, "@IDKONFIGFORMATNR", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(12, "@IDKONFIGFORMATNR", idKonfigFormatNr, ParameterDirection.Input);
            dbManager.AddParameters(13, "@PERSHKRIMKONFIGENG", pershkrimKonfigAmbjente_en, ParameterDirection.Input);
            dbManager.AddParameters(14, "@LLOJI", lloji, ParameterDirection.Input);
            if (formatmobile == 0) dbManager.AddParameters(15, "@FORMATPRINTIMIMOBILE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(15, "@FORMATPRINTIMIMOBILE", formatmobile, ParameterDirection.Input);
            dbManager.AddParameters(16, "@PERSHKRIMKONFIG_fr", pershkrimKonfigAmbjente_fr, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ins");
            idkonfigambjente = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        internal clsMesazh modifikoKonfigurim(int idkonfigambjente, string kodkonfigambjente, string pershkrimkonfigambjente_sq, int idkategori, int radha, bool defaultkonfigambjente, int nivel, int SK, int idnderm, int idkonf, int idstatusdok, int idperdorues, int idKonfigFormatNr, string pershkrimKonfigAmbjente_en, int lloji, int formatmobile, string pershkrimKonfigAmbjente_fr)
        {
            dbManager.Open();
            dbManager.CreateParameters(17);
            dbManager.AddParameters(0, "@IDKONFIG", idkonfigambjente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODKONFIG", kodkonfigambjente, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERSHKRIMKONFIG", pershkrimkonfigambjente_sq, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKATDOK", idkategori, ParameterDirection.Input);
            dbManager.AddParameters(4, "@RADHA", radha, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DEFAULT", defaultkonfigambjente, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDNIVEL", nivel, ParameterDirection.Input);
            if (SK == 0) dbManager.AddParameters(7, "@IDSKEMKONT", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(7, "@IDSKEMKONT", SK, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            if (idkonf == 0) dbManager.AddParameters(9, "@IDKONFIGURIMI", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(9, "@IDKONFIGURIMI", idkonf, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            if (idKonfigFormatNr == 0 || idKonfigFormatNr == -1)
                dbManager.AddParameters(12, "@IDKONFIGFORMATNR", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(12, "@IDKONFIGFORMATNR", idKonfigFormatNr, ParameterDirection.Input);
            dbManager.AddParameters(13, "@PERSHKRIMKONFIGENG", pershkrimKonfigAmbjente_en, ParameterDirection.Input);
            dbManager.AddParameters(14, "@LLOJI", lloji, ParameterDirection.Input);
            if (formatmobile == 0) dbManager.AddParameters(15, "@FORMATPRINTIMIMOBILE", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(15, "@FORMATPRINTIMIMOBILE", formatmobile, ParameterDirection.Input);
            dbManager.AddParameters(16, "@PERSHKRIMKONFIG_fr", pershkrimKonfigAmbjente_fr, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_upd");
            idkonfigambjente = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        internal clsMesazh fshiAtribut(int idkonf)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTE", idkonf, ParameterDirection.Input);
            dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ATRIBUTETRUPI_delByKonfigurim");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiKonfigurim(int idkonf)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTE", idkonf, ParameterDirection.Input);
            dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal int merrRadheMax(int idnivel)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNIVEL", idnivel, ParameterDirection.Input);
            int radha = 0;
            int.TryParse(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_merrRadheMaxKonfigurimi").ToString(), out radha);
            return radha;
        }

        internal int merrNrAutomatikSipasKontrollitDheKonfigurimit(int idkonfigambjente, int idkomponente, string kodkontroll)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda merrNrAutomatikSipasKontrollitDheKonfigurimit sipas idkonfiambjente:{idkonfigambjente}, idkomponente:{idkomponente}, kodkontrolli{kodkontroll}");
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@kodkontrolli", kodkontroll, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idkomponente", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDkonfigambjente", idkonfigambjente, ParameterDirection.Input);
            int idnrautomtik = 0;
            int.TryParse(Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ATRIBUTETRUPI_merrNrAutomatikSipasKontrollitDheKonfigurimit")), out idnrautomtik);
            ImbLogger.LogTraceShitje($"Mbaroi metoda merrNrAutomatikSipasKontrollitDheKonfigurimit sipas idkonfiambjente:{idkonfigambjente}, idkomponente:{idkomponente}, kodkontrolli{kodkontroll}");
            return idnrautomtik;
        }
        internal int merrIdentifikuesSipasKontrollitDheKonfigurimit(int idkonfigambjente, int idkomponente, string kodkontroll)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@kodkontrolli", kodkontroll, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idkomponente", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDkonfigambjente", idkonfigambjente, ParameterDirection.Input);
            int identifikues = 0;
            int.TryParse(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ATRIBUTETRUPI_merrIdentifikuesSipasKontrollitDheKonfigurimit").ToString(), out identifikues);
            return identifikues;
        }
        internal string merrVleredefaultSipasKontrollitDheKonfigurimit(int idkonfigambjente, int idkomponente, string kodkontroll)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda merrVleredefaultSipasKontrollitDheKonfigurimit me idkonfigambjente:{idkonfigambjente}, me idkomponente; {idkomponente}, kodkontrroll :{kodkontroll}");
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@kodkontrolli", kodkontroll, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idkomponente", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDkonfigambjente", idkonfigambjente, ParameterDirection.Input);
            ImbLogger.LogTraceShitje($"Mbaroi metoda merrVleredefaultSipasKontrollitDheKonfigurimit me idkonfigambjente:{idkonfigambjente}, me idkomponente; {idkomponente}, kodkontrroll :{kodkontroll}");
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ATRIBUTETRUPI_merrVlereDefaultSipasKontrollitDheKonfigurimit"));
        }

        internal string merrVleredefaultSipasKontrollitKodKonfigDheNderm(string kodKonfigambjente, int idNdermarrje, string kodkontroll)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODKONTROLL", kodkontroll, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODKONFIGAMBJENTE", kodKonfigambjente, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ATRIBUTETRUPI_merrVlereDefaultSipasKontrollitDheKodKonfigurimit"));
        }

        internal clsMesazh fshiKonfigurimStatus(int idkonf, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKONFIG", idkonf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh ruajAtribut(int idkontroll, int idkonfigambjente, string vlDefault_sq, bool vis, bool enab, int lupa, int identif, int rresht, int kolon, bool detyrueshme, int idnrautomatik, string vlDefault_en, bool shfaqmobile, double renditjamobile, bool unike, string vlDefault_fr)
        {
            dbManager.Open();
            dbManager.CreateParameters(16);
            dbManager.AddParameters(0, "@IDKONTROLL", idkontroll, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKONFIGAMBJENTE", idkonfigambjente, ParameterDirection.Input);
            dbManager.AddParameters(2, "@VLEREDEFAULT_sq", vlDefault_sq, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VISIBLE", vis, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ENABLED", enab, ParameterDirection.Input);
            if (lupa == 0 || lupa == -1) dbManager.AddParameters(5, "@IDLUPA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@IDLUPA", lupa, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDENTIFIKUES", identif, ParameterDirection.Input);
            dbManager.AddParameters(7, "@RRESHTI", rresht, ParameterDirection.Input);
            dbManager.AddParameters(8, "@KOLONA", kolon, ParameterDirection.Input);
            dbManager.AddParameters(9, "@DETYRUESHME", detyrueshme, ParameterDirection.Input);
            if (idnrautomatik == 0) dbManager.AddParameters(10, "@IDNRAUTOMATIK", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@IDNRAUTOMATIK", idnrautomatik, ParameterDirection.Input);
            dbManager.AddParameters(11, "@VLEREDEFAULT_en", vlDefault_en, ParameterDirection.Input);
            dbManager.AddParameters(12, "@SHFAQMOBILE", shfaqmobile, ParameterDirection.Input);
            dbManager.AddParameters(13, "@RENDITJAMOBILE", renditjamobile, ParameterDirection.Input);
            dbManager.AddParameters(14, "@UNIKE", unike, ParameterDirection.Input);
            dbManager.AddParameters(15, "@VLEREDEFAULT_fr", vlDefault_fr, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ATRIBUTETRUPI_ins");

            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        internal clsMesazh modifikoAtribut(int idkontroll, int idkonfigambjente, string vlDefault_sq, bool vis, bool enab, int lupa, int identif, int rresht, int kolon, bool detyrueshem, int idnrautomatik, int idGjuha, string vlDefault_en, bool shfaqmobile, double renditjamobile, bool unike, string vlDefault_fr)
        {
            dbManager.Open();
            dbManager.CreateParameters(17);
            dbManager.AddParameters(0, "@IDKONTROLL", idkontroll, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKONFIGAMBJENTE", idkonfigambjente, ParameterDirection.Input);
            dbManager.AddParameters(2, "@VLEREDEFAULT_sq", vlDefault_sq, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VISIBLE", vis, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ENABLED", enab, ParameterDirection.Input);
            if (lupa == 0 || lupa == -1) dbManager.AddParameters(5, "@IDLUPA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@IDLUPA", lupa, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDENTIFIKUES", identif, ParameterDirection.Input);
            dbManager.AddParameters(7, "@RRESHTI", rresht, ParameterDirection.Input);
            dbManager.AddParameters(8, "@KOLONA", kolon, ParameterDirection.Input);
            dbManager.AddParameters(9, "@DETYRUESHME", detyrueshem, ParameterDirection.Input);
            if (idnrautomatik == 0) dbManager.AddParameters(10, "@IDNRAUTOMATIK", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@IDNRAUTOMATIK", idnrautomatik, ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            dbManager.AddParameters(12, "@VLEREDEFAULT_en", vlDefault_en, ParameterDirection.Input);
            dbManager.AddParameters(13, "@SHFAQMOBILE", shfaqmobile, ParameterDirection.Input);
            dbManager.AddParameters(14, "@RENDITJAMOBILE", renditjamobile, ParameterDirection.Input);
            dbManager.AddParameters(15, "@UNIKE", unike, ParameterDirection.Input);
            dbManager.AddParameters(16, "@VLEREDEFAULT_fr", vlDefault_fr, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ATRIBUTETRUPI_upd");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        internal clsMesazh modifikoAtributSqEng(int idkontroll, int idkonfigambjente, string vlDefault_sq, bool vis, bool enab, int lupa, int identif, int rresht, int kolon, bool detyrueshem, int idnrautomatik, string vlDefault_en, bool shfaqmobile, double renditjamobile, bool unike, string vlDefault_fr)
        {
            dbManager.Open();
            dbManager.CreateParameters(16);
            dbManager.AddParameters(0, "@IDKONTROLL", idkontroll, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKONFIGAMBJENTE", idkonfigambjente, ParameterDirection.Input);
            dbManager.AddParameters(2, "@VLEREDEFAULT_sq", vlDefault_sq, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VISIBLE", vis, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ENABLED", enab, ParameterDirection.Input);
            if (lupa == 0 || lupa == -1) dbManager.AddParameters(5, "@IDLUPA", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(5, "@IDLUPA", lupa, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDENTIFIKUES", identif, ParameterDirection.Input);
            dbManager.AddParameters(7, "@RRESHTI", rresht, ParameterDirection.Input);
            dbManager.AddParameters(8, "@KOLONA", kolon, ParameterDirection.Input);
            dbManager.AddParameters(9, "@DETYRUESHME", detyrueshem, ParameterDirection.Input);
            if (idnrautomatik == 0) dbManager.AddParameters(10, "@IDNRAUTOMATIK", DBNull.Value, ParameterDirection.Input);
            else dbManager.AddParameters(10, "@IDNRAUTOMATIK", idnrautomatik, ParameterDirection.Input);
            dbManager.AddParameters(11, "@VLEREDEFAULT_en", vlDefault_en, ParameterDirection.Input);
            dbManager.AddParameters(12, "@SHFAQMOBILE", shfaqmobile, ParameterDirection.Input);
            dbManager.AddParameters(13, "@RENDITJAMOBILE", renditjamobile, ParameterDirection.Input);
            dbManager.AddParameters(14, "@UNIKE", unike, ParameterDirection.Input);
            dbManager.AddParameters(15, "@VLEREDEFAULT_fr", vlDefault_fr, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ATRIBUTETRUPI_updSqEng");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        internal clsMesazh modifikoAtributVlereDefault(int idkontroll, int idkonfigambjente, String vleredefault)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKONTROLL", idkontroll, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKONFIGAMBJENTE", idkonfigambjente, ParameterDirection.Input);
            dbManager.AddParameters(2, "@VLEREDEFAULT", vleredefault, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ATRIBUTETRUPI_updVleredefault");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        public clsMesazh ruajGridaKoka(DbCore.DbAdmin.clsGridaKoka gridakoka)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDGRIDAKOKA", gridakoka.IdGridaKoka, ParameterDirection.Output);
            dbManager.AddParameters(1, "@EMRIGRIDAKOKA", gridakoka.EmriGridaKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKOMPONENTE", gridakoka.IdKomponente, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKONFIG", gridakoka.IdKonfigurim, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRIDAKOKA_ins");
            gridakoka.IdGridaKoka = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        public clsMesazh ruajGridaTrupi(DbCore.DbAdmin.clsGridaTrupi gridatrupi)
        {
            dbManager.Open();
            dbManager.CreateParameters(16);
            dbManager.AddParameters(0, "@IDTRUPI", gridatrupi.IdTrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKOKA", gridatrupi.IdKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@INDEX", gridatrupi.IndexTrupi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VISIBLE", gridatrupi.VisibleTrupi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KODI", gridatrupi.KodiTrupi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@PERSHKRIMI", gridatrupi.PershkrimiTrupi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@READONLY", gridatrupi.ReadonlyTrupi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@WIDTH", gridatrupi.WidthTrupi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDLUPA", gridatrupi.IdKonfigAmbjenteLupa, ParameterDirection.Input);
            dbManager.AddParameters(9, "@indexorigjinal", gridatrupi.IndexTrupiOrigjinal, ParameterDirection.Input);
            dbManager.AddParameters(10, "@VISIBLECOSTUMIZE", gridatrupi.VisibleCostumize, ParameterDirection.Input);
            dbManager.AddParameters(11, "@PERSHKRIMI_en", gridatrupi.PershkrimiTrupi_en, ParameterDirection.Input);
            dbManager.AddParameters(12, "@TIPI", gridatrupi.Tipi, ParameterDirection.Input);
            dbManager.AddParameters(13, "@SHFAQMOBILE", gridatrupi.ShfaqMobile, ParameterDirection.Input);
            dbManager.AddParameters(14, "@RENDITJAMOBILE", gridatrupi.RenditjaMobile, ParameterDirection.Input);
            dbManager.AddParameters(15, "@PERSHKRIMI_fr", gridatrupi.PershkrimiTrupi_fr, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GRIDATRUPI_ins");
            gridatrupi.IdTrupi = int.Parse(dbManager.Parameters[0].Value.ToString());
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        
        internal Dictionary<string, string> ktheVleraDefaultTeKontrolleveSipasIdKonfigAmbjeteALL(int idKonfigAmbjente)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTE", idKonfigAmbjente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ATRIBUTETRUPI_merrVleraDefaultTeKontrolleveSipasIdKonfigAmbjeteALL");
            
            return  ds.Tables[0].AsEnumerable()
                            .ToDictionary<DataRow, string, string>(row => row.Field<string>(0),
                                                                row => row.Field<string>(1));
        }


        internal DataTable ktheGjitheKonfigurimetKomponentes(int idkomponente, int idndermarje, int idperdorues, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDKOMPONENTE", idkomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_merrGjitheKonfigurimet");
            return ds.Tables[0];
        }

        public clsMesazh ekzistonKonfigurimSipasIDKONFIG(int id, int idndermarje)
        {
            ImbLogger.LogTraceShitje($"Fillon kerkimi sipas id:{id} dhe idndermarrjes:{idndermarje}");
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTE", id, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            int nrRreshtash = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ekzistonSipasIDKonfigurimi"));
            if (nrRreshtash == 0)
                return new clsMesazh(false, "Nuk ekziston");
            else if (nrRreshtash == 1)
            {
                ImbLogger.LogTraceShitje($"Perfundon kerkimi sipas id:{id} dhe idndermarrjes:{idndermarje}");
                return new clsMesazh(true, "Ekziston");
            }
            else
            {
                ImbLogger.LogTraceShitje($"Perfundon kerkimi sipas id:{id} dhe idndermarrjes:{idndermarje}");
                return new clsMesazh(false, "Error");
            }

        }

        public bool ekzistonKonfigurim(String kod, int idndermarje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODKONFIGAMBJENTE", kod, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_eksiston");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        public bool ekzistonKonfigurimSipasRadhes(int idNiveli, int radha, int idKonfig, int idKatdok)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNIVEL", idNiveli, ParameterDirection.Input);
            dbManager.AddParameters(1, "@RADHA", radha, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKONFIG", idKonfig, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKATDOK", idKatdok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_eksistonSipasRadhes");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        public bool kaVeprimeKonfigurim(int idkonfamb)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTE", idkonfamb, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_kaveprime");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        public bool kaAutorizimKonfigurim(int idkonfamb, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTE", idkonfamb, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "[prc_T_KONFIGAMBJENTE_kaAutorizimKonfigurimi]");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        public bool kaTeDrejteDheAutorizimTeHapeNivelRegjistrimiSipasKonfigurimit(int idKonfAmb, int idPerdorues, int idViti, string komponente)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTE", idKonfAmb, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KOMPONENTE", komponente, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_kaTeDrejteDheAutorizimTeHapeNivelRregjistrimiSipasKonfigurimit");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        public bool kaTeDrejteDheAutorizimTeHapeAmbientSipasKategorise(int idKategoria, int idPerdorues, int idViti, int idKomponente, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDKATEGORIA", idKategoria, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_kaTeDrejteDheAutorizimTeHapeAmbientSipasKategorise");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }
        internal DataTable ktheGjitheKonfigurimeAmbjentesh(int idnderm, int idperdorues, int idsuperkat, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDSUPERKATEGORI", idsuperkat, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_merrGjitheKonfigurimAmbjentesh");
            return ds.Tables[0];
        }

        internal DataTable ktheGjitheKonfigurimeAmbjenteshMePershkrimEng(int idnderm, int idperdorues, int idsuperkat, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDSUPERKATEGORI", idsuperkat, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_merrGjitheKonfigurimAmbjenteshMePershkrimEng");
            return ds.Tables[0];
        }

        internal DataTable ktheGjitheKonfigurimeAmbjenteshDT(int idnderm, int idperdorues, int idsuperkat, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDSUPERKATEGORI", idsuperkat, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_merrGjitheKonfigurimAmbjenteshDT");
            return ds.Tables[0];
        }

        /// <summary>
        /// kthen konfigurimin e ambjentit ne baze te idKategori, idNivelit. Therret SP_ne:
        /// <see cref="prc_T_KONFIGAMBJENTE_ktheKonfigPerIdKatIdNivel"/>
        /// </summary>
        /// <param name="idKategori"></param>
        /// <param name="idNivel"></param>
        /// <param name="idperdorues"></param>
        /// <returns></returns>
        internal DataTable ktheKonfigAmbjSipasIdKategoriIdNivelMeLloj(int idKategori, int idNivel, int idperdorues, bool meLloj = true)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDKATDOK", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVEL", idNivel, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@MeLloji", meLloj, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigPerIdKatIdNivel");
            return ds.Tables[0];
        }

        /// <summary>
        /// kthen konfigurimin e ambjentit ne baze te idKategori, idNivelit dhe gjuhes se perdoruesit. Therret SP-ne:
        /// <see cref="prc_T_KONFIGAMBJENTE_ktheKonfigPerIdKatIdNivelSipasGjuhes"/>
        /// </summary>
        /// <param name="idKategori"></param>
        /// <param name="idNivel"></param>
        /// <param name="idperdorues"></param>
        /// <param name="idGjuha">merr pershkrimin shqip kur gjuha eshte shqip dhe ate anglisht kur gjuha eshte anglisht</param>
        /// <returns></returns>
        internal DataTable ktheKonfigAmbjSipasIdKategoriIdNivel(int idKategori, int idNivel, int idperdorues, int idGjuha, bool meKonfVartes)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDKATDOK", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVEL", idNivel, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            dbManager.AddParameters(4, "@MEKONFVARTES", meKonfVartes, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigPerIdKatIdNivelSipasGjuhes");
            return ds.Tables[0];
        }
        internal DataTable ktheKonfigAmbjSipasIdKategoriIdNivelDokTrans(int idKategori, int idNivel, int idperdorues, int idGjuha, bool doktransf)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDKATDOK", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVEL", idNivel, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DokTrans", doktransf, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigPerIdKatIdNivelSipasGjuhesDMT");
            return ds.Tables[0];
        }

        /// <summary>
        /// kthen konfigurimin e ambjentit ne baze te idKategori, idNivelit dhe gjuhes se perdoruesit. Therret SP-ne:
        /// <see cref="prc_T_KONFIGAMBJENTE_ktheKonfigPerIdKatIdNivelSipasGjuhes"/>
        /// </summary>
        /// <param name="idKategori"></param>
        /// <param name="kodNivel"></param>
        /// <param name="idperdorues"></param>
        /// <param name="idGjuha">merr pershkrimin shqip kur gjuha eshte shqip dhe ate anglisht kur gjuha eshte anglisht</param>
        /// <returns></returns>
        internal DataTable ktheKonfigAmbjSipasKodKategoriIdNivel(int idKategori, string kodNivel, int idNdermarrje, int idperdorues, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDKATDOK", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODNIVEL", kodNivel, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigPerIdKatKodNivelSipasGjuhes");
            return ds.Tables[0];
        }

        /// <summary>
        /// kthen konfigurimin e ambjentit ne baze te idKategori, idNivelit dhe gjuhes se perdoruesit. Therret SP-ne:
        /// <see cref="prc_T_KONFIGAMBJENTE_ktheKonfigPerIdKatIdNivelSipasGjuhes"/>
        /// </summary>
        /// <param name="idKategori"></param>
        /// <param name="kodNivel"></param>
        /// <param name="idperdorues"></param>
        /// <param name="idGjuha">merr pershkrimin shqip kur gjuha eshte shqip dhe ate anglisht kur gjuha eshte anglisht</param>
        /// <returns></returns>
        internal DataTable ktheKonfigAmbjSipasKodKategoriIdNivelPaVartese(int idKategori, string kodNivel, int idNdermarrje, int idperdorues, int idGjuha, bool kushtVartes)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDKATDOK", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODNIVEL", kodNivel, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(5, "@KUSHTVARTES", kushtVartes, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigPerIdKatKodNivelSipasGjuhesPaVartese");
            return ds.Tables[0];
        }

        internal DataTable ktheKonfigAmbjVarteseSipasKodKategoriIdNivel(int idKategori, string kodNivel, int idNdermarrje, int idperdorues, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDKATDOK", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODNIVEL", kodNivel, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigVartesePerIdKatKodNivelSipasGjuhes");
            return ds.Tables[0];
        }

        internal DataTable ktheKonfigAmbjSipasIdKategori(int idKategori, int idnderm, int idperdorues, int idGjuha, bool merrSipasAutorizimit, bool meLloj)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDKATDOK", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            dbManager.AddParameters(4, "@merrSipasAutorizimit", merrSipasAutorizimit, ParameterDirection.Input);
            dbManager.AddParameters(5, "@MeLloji", meLloj, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigPerIdKat");
            return ds.Tables[0];
        }

        internal DataTable ktheKonfigAmbjentiGjeneruarNgaFK( int idNderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigperKushtKontabilizimi");
            return ds.Tables[0];
        }
        internal DataTable ktheKonfigAmbjSipasKategorive(string idKategori, int idnderm, int idperdorues, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@IDKATDOK", idKategori, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters("@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigSipasKategorive");
            return ds.Tables[0];
        }

        internal DataTable ktheKonfigAmbjSipasIdKategoriPaKonfVartese(int idKategori, int idnderm, int idperdorues, int idGjuha, bool merrSipasAutorizimit, bool meLloj)
        {

            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@IDKATDOK", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            dbManager.AddParameters(4, "@merrSipasAutorizimit", merrSipasAutorizimit, ParameterDirection.Input);
            dbManager.AddParameters(5, "@MeLloji", meLloj, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigPerIdKatPaKonfVartese");
            return ds.Tables[0];
        }

        internal DataTable ktheKonfigAmbjSipasIdKategoriDTSmall(int idKategori, int idnderm, int idperdorues, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDKATDOK", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigPerIdKatDTSmall");
            return ds.Tables[0];
        }
       
        internal DataTable mbushKonfigAmbjSipasKategoriveDheGjuhes(string idKategori, int idnderm, int idperdorues, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters("@IDKATDOK", idKategori, ParameterDirection.Input);
            dbManager.AddParameters("@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters("@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters("@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigSipasKategoriveDheGjuhes");
            return ds.Tables[0];
        }

        internal DataTable ktheKonfigAmbjSipasIdKategoriMePershkrimEngMeKusht(int idKategori, int idnderm, int idperdorues, string kushti, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDKATDOK", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KUSHTI", kushti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigPerIdKatMePershkrimEngMeKusht");
            return ds.Tables[0];
        }

        internal DataTable ktheKonfigAmbjSipasIdKategoriJoVartese(int idKategori, int idnderm, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKATDOK", idKategori, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigPerIdKatJoVartese");
            return ds.Tables[0];
        }

        internal DataTable ktheKonfigAmbjSipasIdKomponente(int idKomponente, int idnderm, int idperdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfigPerIdKomponente");
            return ds.Tables[0];
        }

        internal DataRow ktheKonfigAmbjSipasId(int idKonfigAmbjente)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIG", idKonfigAmbjente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfiguriminMeID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow ktheKonfigAmbjSipasId(int idKonfigAmbjente, int idGjuha, bool meLloj = true)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKONFIG", idKonfigAmbjente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            dbManager.AddParameters(2, "@MeLloji", meLloj, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfiguriminMeIDSipasGjuhes");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow ktheKonfigAmbjSipasKod(string kodKonfig, int idndermarje, bool meLloj)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ktheKonfigAmbjSipasKod sipas kodKonfig:{kodKonfig}, idndermarje:{idndermarje}, meLloj:{meLloj}");
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@KODKONFIG", kodKonfig, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@MeLloji", meLloj, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheKonfiguriminMeKod");
            ImbLogger.LogTraceShitje($"Mbaroi metoda ktheKonfigAmbjSipasKod sipas kodKonfig:{kodKonfig}, idndermarje:{idndermarje}, meLloj:{meLloj}");
            if (ds == null)
            {
                ImbLogger.LogTraceShitje($"Metoda ktheKonfigAmbjSipasKod sipas kodKonfig:{kodKonfig}, idndermarje:{idndermarje}, meLloj:{meLloj} ktheu null");
                return null;
            }

            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
            {
                ImbLogger.LogTraceShitje($"Metoda ktheKonfigAmbjSipasKod sipas kodKonfig:{kodKonfig}, idndermarje:{idndermarje}, meLloj:{meLloj} ktheu null");
                return null;
            }
                
            return ds.Tables[0].Rows[0];
        }

        internal string kthePershkrimKonfigAmbjSipasKod(int idKonfigurim)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGAMBJENTE", idKonfigurim, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_kthePershkriminSipasID"));
        }

        /// <summary>
        /// kthen konfigurimin ne default
        /// </summary>
        /// <param name="idkonf"></param>
        /// <param name="idndermarje"></param>
        /// <param name="idndermarjenga"></param>
        /// <param name="idperdoruesi"></param>
        /// <returns></returns>
        internal clsMesazh ktheDefault(int idkonf, int idndermarje, int idndermarjenga, int idperdoruesi)
        {
           
                dbManager.Open();
                dbManager.CreateParameters(4);
                dbManager.AddParameters(0, "@IDKONFIGAMBJENTE", idkonf, ParameterDirection.Input);
                dbManager.AddParameters(1, "@IDNDERMARJE", idndermarje, ParameterDirection.Input);
                dbManager.AddParameters(2, "@IDNDERMNGA", idndermarjenga, ParameterDirection.Input);
                dbManager.AddParameters(3, "@IDPERDORUESI", idperdoruesi, ParameterDirection.Input);
                dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheDokDefault");
                 
           
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        #endregion KONFIGURIMI I DOKUMENTAVE

        #region KONFIGURIMI I FORMATIT TE NUMRAVE

        internal clsMesazh ruajFormat(out int idFormKonf, int idKat, int idNderm, int idstatusdok, string kodi, string emertimi, int idkrijuesi)
        {
            idFormKonf = -1;

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDFORMATKONFIG", idFormKonf, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKATDOK", idKat, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNderm, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSTATUSDOK", idstatusdok, ParameterDirection.Input);
            dbManager.AddParameters(4, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIG_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            idFormKonf = int.Parse(dbManager.Parameters[0].Value.ToString());
            return mesazh;
        }

        internal clsMesazh ruajFormatTrupi(int idFormKonfTrupi, int idFormKonfKoka, int idMon, int idSasi, int idCmim, int idVlefta, int idZbritja)
        {

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDFORMKONFIGTRUPI", idFormKonfTrupi, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDFORMKONFIGKOKA", idFormKonfKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDMONEDHA", idMon, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDFORMATSASIA", idSasi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDFORMATCMIMI", idCmim, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDFORMATVLEFTA", idVlefta, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDFORMATZBRITJA", idZbritja, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIGTRUP_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh modifikoFormat(int idFormKonf, int idKat, string kodi, string emertimi, int idkrijuesi)
        {

            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDFORMATKONFIG", idFormKonf, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKATDOK", idKat, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@EMERTIMI", emertimi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIG_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        internal clsMesazh modifikoFormatTrupi(int idFormKonfTrupi, int idFormKonfKoka, int idMon, int idSasi, int idCmim, int idVlefta, int idZbritja)
        {

            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDFORMKONFIGTRUPI", idFormKonfTrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDFORMKONFIGKOKA", idFormKonfKoka, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDMONEDHA", idMon, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDFORMATSASIA", idSasi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDFORMATCMIMI", idCmim, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDFORMATVLEFTA", idVlefta, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDFORMATZBRITJA", idZbritja, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIGTRUP_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiFormat(int idFormKonf)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFORMATKONFIG", idFormKonf, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIG_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiFormatTrupi(int idFormKonfKoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFORMATNRKOKA", idFormKonfKoka, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIGTRUP_delSipasIdKoka");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiFormatin(int idFormKonf)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFORMATKONFIG", idFormKonf, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIG_updDel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiGjitheFormate(int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDnDERMARRJE", idNdermarrja, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIG_delAll");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal DataTable ktheGjitheFormate(int idNdermarrja)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrja, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIG_selAll");
            return ds.Tables[0];
        }

        internal DataTable ktheFormatTrupiSipasIdKoka(int idKoka)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFORMATNRKOKA", idKoka, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIGTRUP_selSipasIdKoka");
            return ds.Tables[0];
        }

        internal DataTable ktheFormatNrSipasKatNderm(int idKat, int idNderm)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKATEGORIA", idKat, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIG_selSipasIdKategorie");
            return ds.Tables[0];
        }

        internal DataTable ktheFormatNr()
        {

            dbManager.Open();
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FORMATNR_selAll");
            return ds.Tables[0];
        }

        internal int ktheFormatNrSipasVlera(string vlera)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@VLERAFORMAT", vlera.Trim(), ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FORMATNR_selSipasVlera");
            if (ds == null)
                return -1;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return -1;
            int idformat;
            int.TryParse(ds.Tables[0].Rows[0]["IDFORMAT"].ToString(), out idformat);
            return idformat;
        }

        internal DataRow ktheFormatNrSipasId(int idFormat)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFORMAT", idFormat, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FORMATNR_selSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow ktheFormatNrKonfigSipasId(int id)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFORMATNR", id, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIG_selSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow ktheFormatNrKonfigSipasIdKonfigAmbjente(int idKonfigAmbjente)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIMI", idKonfigAmbjente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIG_selFormatPerKonfigurimin");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow ktheFormatNrKonfigTrupSipasIdTrupi(int idTrupi)
        {

            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFORMATNRTRUPI", idTrupi, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIGTRUP_selSipasIdTrupi");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow ktheFormatNrKonfigTrupSipasIdKokaDheMonedha(int idKoka, int idMonedha)
        {

            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDFORMATNRKOKA", idKoka, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDMONEDHA", idMonedha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIGTRUP_selSipasIdKokaDheMonedha");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow ktheFormatNrKonfigSipasKodit(string kodi, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@KODI", kodi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIG_selSipasKodit");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal bool ekzistonKonfigSipasKodit(string kodi, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KODI", kodi, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIG_EkzistonKonfigSipasKodit"));
            return Convert.ToBoolean(pergjigje);
        }

        internal bool eshteLidhurFormatNrMeKonfigurim(int idKonfigFormatNr)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDFORMATKONFIG", idKonfigFormatNr, ParameterDirection.Input);
            int pergjigje = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_FORMATNRKONFIG_eshteLidhurMeKonfigurim"));
            return Convert.ToBoolean(pergjigje);
        }

        #endregion KONFIGURIMI I FORMATIT TE NUMRAVE

        #region KUSHTE TEMPLATE

        internal clsMesazh ruajKushtTemplate(out int idKushtTempl, int idKonfAmbj, int idKu, int vl)
        {
            idKushtTempl = 0;
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDKUSHTEMPLATE", idKushtTempl, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDKONFIGAMBJ", idKonfAmbj, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKUSHT", idKu, ParameterDirection.Input);
            dbManager.AddParameters(3, "@VLERA", vl, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTEMPLATE_ins");
            idKushtTempl = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh modifikoKushtTemplate(int idKushtTempl, int vl)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKUSHTEMPLATE", idKushtTempl, ParameterDirection.Input);
            dbManager.AddParameters(1, "@VLERA", vl, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTEMPLATE_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiKushtTemplate(int idKushtTempl)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKUSHTEMPLATE", idKushtTempl, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTEMPLATE_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiKushtTemplateByKonfigurim(int idkonf)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIM", idkonf, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTE_delByKonfigurim");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal DataRow ktheKushtTemplateSipasID(int idKushtTemplate)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKUSHTEMPLATE", idKushtTemplate, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTEMPLATE_ktheSipasID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal DataRow ktheKushtTemplateSipasIDkonfigurimdheKodKushti(int idkonfigurim, string kodkusht)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda theKushtTemplateSipasIDkonfigurimdheKodKushti me idKonfigurimi:{idkonfigurim} dhe kodKusht:{kodkusht}");
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKONFIGURIM", idkonfigurim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@kodkusht", kodkusht, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTEMPLATE_ktheKushtSipasIdKonfigurimDheKodKushti");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            ImbLogger.LogTraceShitje($"Mbaroi metoda theKushtTemplateSipasIDkonfigurimdheKodKushti me idKonfigurimi:{idkonfigurim} dhe kodKusht:{kodkusht}");
            return ds.Tables[0].Rows[0];
        }

        internal bool krijoKushtTemplate(int idkonfigurim, string kodkusht, int vlera)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKONFIGURIM", idkonfigurim, ParameterDirection.Input);
            dbManager.AddParameters(1, "@kodkusht", kodkusht, ParameterDirection.Input);
            dbManager.AddParameters(2, "@vlera", vlera, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KUSHTEMPLATE_krijoKushtTemplateSipasIdKonfigurimDheKodKushti");
            return true;
        }

        internal DataTable ktheGjitheKushteDefaultNivel(int idNivel, int idKomponente)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNIVEL", idNivel, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTEMPLATE_ktheKushtSipasIdNivel");
            return ds.Tables[0];
        }

        internal DataTable ktheGjitheKushteKonfigurimi(int idKonfAmbjente)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIM", idKonfAmbjente, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTEMPLATE_ktheKushtSipasIdKonfigurim").Tables[0];
        }

        internal DataTable ktheGjitheKushteAlternativa(int idKonfAmbjente)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIM", idKonfAmbjente, ParameterDirection.Input);
            return dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KUSHTE_ALTER_ktheKushtSipasIdKonfigurimDT").Tables[0];
        }

        #endregion KUSHTE TEMPLATE

        #region ALTERNATIVAT E KUSHTEVE

        internal clsMesazh ruajAlternativKushti(int idAlternativaKushti, string alternativa, int idKushti)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDALTERNATIVAKUSHTI", idAlternativaKushti, ParameterDirection.Output);
            dbManager.AddParameters(1, "@ALTERNATIVA", alternativa, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKUSHTI", idKushti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ALTERNATIVAKUSHTI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh ruajKonfLlojRreshti(int idkushtitemplate, int idllojreshti, int rend)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKUSHTEMPLATE", idkushtitemplate, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJRRESHTI", idllojreshti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@rend", rend, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFLLOJRRESHTI_ins");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh modifikoAlternativKushti(int idAlternativaKushti, string alternativa, int idKushti)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDALTERNATIVAKUSHTI", idAlternativaKushti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@ALTERNATIVA", alternativa, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDKUSHTI", idKushti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ALTERNATIVAKUSHTI_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshiAlternativKushti(int idAlternativaKushti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDALTERNTIVAKUSHTI", idAlternativaKushti, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ALTERNATIVAKUSHTI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh fshikonfllojreshti(int idkushttemplate)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKUSHTEMPLATE", idkushttemplate, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KONFLLOJRRESHTI_del");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal DataRow merrAlternativKushtiSipasId(int idAlternativaKushti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDALTERNATIVAKUSHTI", idAlternativaKushti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ALTERNATIVAKUSHTI_merrSipasId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// kthen alternativen sipas id konfigurim ambjentit dhe kodit te kushtit. Therret SP-ne:
        /// <see cref="prc_T_ALTERNATIVAKUSHTI_merrSipasIdKonfigDheKodKusht"/>
        /// </summary>
        /// <param name="idKonfigAmbjente"></param>
        /// <param name="kushtKod"></param>
        /// <returns></returns>
        internal string ktheAlternativeSipasKushtitDheIdKonfig(int idKonfigAmbjenti, string kodKushti)
        {
            ImbLogger.LogTraceShitje($"Filloi metoda ktheAlternativeSipasKushtitDheIdKonfig me idKonfigAmbjenti:{idKonfigAmbjenti}, kodKushti:"+kodKushti);
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKONFIGURIM", idKonfigAmbjenti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@kodkusht", kodKushti, ParameterDirection.Input);
            object altObj = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ALTERNATIVAKUSHTI_merrSipasIdKonfigDheKodKusht");

            if (altObj == null)
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda ktheAlternativeSipasKushtitDheIdKonfig me idKonfigAmbjenti:{idKonfigAmbjenti}, kodKushti:"+kodKushti);
                return "";
            }

            else
            {
                ImbLogger.LogTraceShitje($"Mbaroi metoda ktheAlternativeSipasKushtitDheIdKonfig me idKonfigAmbjenti:{idKonfigAmbjenti}, kodKushti:"+kodKushti);
                return altObj.ToString();
            }
                
        }
        
        internal double? ktheVlereSipasKushtitDheIdKonfig(int idKonfigAmbjenti, string kodKushti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKONFIGURIM", idKonfigAmbjenti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@kodkusht", kodKushti, ParameterDirection.Input);
            object altObj = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ALTERNATIVAKUSHTI_merrVlereSipasIdKonfigDheKodKusht");
            if (altObj == null|| altObj.ToString() == "")
                return null;
            else
                return  Convert.ToDouble(altObj);
        }
        internal int kthevlereSipasKushtitDheIdKonfig(int idKonfigAmbjenti, string kodKushti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKONFIGURIM", idKonfigAmbjenti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@kodkusht", kodKushti, ParameterDirection.Input);
            object altObj = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KUSHTEMPLATE_ktheVlereIdKonfigurimDheKodKushti");
            if (altObj == null)
                return 0;
            else
                return Convert.ToInt32(altObj);
        }
        internal string merrLlogariDheBijaSipasAlternativesZLL_B(int idAlternativa, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDALTERNATIVA", idAlternativa, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            object llogari = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_LLOGARI_merrGjitheBijatSipasKushtitZLL_B");
            if (llogari == null)
                return "";
            else
                return Convert.ToString(llogari);
        }

        /// <summary>
        /// kthen alternativen sipas id konfigurim ambjentit dhe kodit te kushtit. Therret SP-ne:
        /// <see cref="prc_T_ALTERNATIVAKUSHTI_merrSipasIdKonfigDheKodKusht"/>
        /// </summary>
        /// <param name="idKonfigAmbjente"></param>
        /// <param name="kushtKod"></param>
        /// <returns></returns>
        internal string ktheAlternativeSipasKushtitDheIdTrupiDok(int idTrupi, string kodKushti)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDTRUPIDOK", idTrupi, ParameterDirection.Input);
            dbManager.AddParameters(1, "@kodkusht", kodKushti, ParameterDirection.Input);
            object altObj = dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ALTERNATIVAKUSHTI_merrSipasIdTrupDokumentiDheKodKusht");
            if (altObj == null)
                return "";
            else
                return altObj.ToString();
        }

        /// <summary>
        /// Merr nje konfigurim llojrreshti nga db-ja.
        /// </summary>
        /// <param name="idKushTemplate">idkonfig</param>
        /// <returns></returns>
        internal DataTable merrKonfLlojRreshti(int idKushTemplate)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKUSHTEMPLATE", idKushTemplate, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFLLOJRRESHTI_merrKonfLlojRreshti");
            return ds.Tables[0];
        }

        public DataRow merrKonfLlojRreshtiVlere(int idLlojRreshti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLLOJRRESHTI", idLlojRreshti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_LLOJRRESHTI_merrLlojRreshtiVlere");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// Merr konfigurimin default pra me te gjithe llojet e rreshtave
        /// </summary>
        /// <returns></returns>
        internal DataTable merrKonfLlojRreshti(string lloji)
        {
            dbManager.Open();
            dbManager.ClearParameters();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@Lloji", lloji, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KONFLLOJRRESHTI_merrKonfLlojRreshtiDefault");
            return ds.Tables[0];
        }

        internal DataTable merrAlternativKushtiSipasIdKushti(int idkushti)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKUSHTI", idkushti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_ALTERNATIVAKUSHTI_merrSipasIdKushti");
            return ds.Tables[0];
        }

        internal IEnumerable<clsAlternativaKushti> merrAlternativKushtiSipasIdKonfigurimitAll(int idKonfigurimi)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKONFIGURIM", idKonfigurimi, ParameterDirection.Input);
            return dbManager.GetIEnumerbale("prc_T_ALTERNATIVAKUSHTI_ktheAlternativatSipasIdKonfigurimALL", clsAlternativaKushti.Krijo);
        }

        #endregion ALTERNATIVAT E KUSHTEVE

        #region MENU ITEM

        /// <summary>
        /// kthen objektet menu item
        /// </summary>
        ///<param name="idMenuItem"> id e menu items</param>
        ///<returns>nje objekt datarow qe permban menu item me kete id </returns>
        internal DataRow merrMenuItemSipasId(int idMenuItem)
        {


            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDMENUITEM", idMenuItem, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENUITEM_ktheMenuItem");
            DataTable dt = ds.Tables[0];
            if (dt.Rows.Count != 0)
                return dt.Rows[0];
            return null;
        }

        /// <summary>
        /// kthen objektet  menu item sipas emrit
        /// </summary>
        ///<param name="name">name</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha menu item me kete emer  </returns>
        internal DataTable merrMenuItemSipasName(string name)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@NAME", name, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENUITEM_ktheMenuItemSipasName");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;
        }

        /// <summary>
        /// kthen objektet  menu item te kompontentes
        /// </summary>
        ///<param name="idkomponente">id e komponente</param>
        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha menu items te kompontentes </returns>
        internal DataTable merrMenuItemSipasKomponentes(int idKomponente)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKOMPONENTE", idKomponente, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENUITEM_SipasKomponentes");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="emerKomponente"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>
        /// <returns></returns>
        internal DataTable merrMenuItemSipasKomponentes(String emerKomponente, int idPerdoruesi, int idNdermarrje, int idViti, bool shtimmodifikim)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@KOMPONENTE", emerKomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@SHTIMMODIFIKIM", shtimmodifikim, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENUITEM_SipasKomponentesDheTeDrejta");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;
        }

        /// <summary>
        ///  Merr menuitems te komponentes dhe me te drejtat perkatese. Perdoret per ambjentet e regjistrimit, ku jane edhe menute draft.
        /// </summary>
        /// <param name="emerKomponente"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>
        /// <returns></returns>
        internal DataTable merrMenuItemSipasKomponentesRegjistrime(String emerKomponente, int idPerdoruesi, int idNdermarrje, int idViti, bool shtimmodifikim)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@KOMPONENTE", emerKomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@SHTIMMODIFIKIM", shtimmodifikim, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENUITEM_SipasKomponentesDheTeDrejtaRegjistrime");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;
        }

        /// <summary>
        ///  Merr menuitems te komponentes dhe me te drejtat perkatese. Perdoret per ambjentet e regjistrimit, ku jane edhe menute draft.
        /// </summary>
        /// <param name="emerKomponente"></param>
        /// <param name="idPerdoruesi"></param>
        /// <param name="idNdermarrje"></param>
        /// <param name="idViti"></param>
        /// <param name="idNivelRegjistrimi"></param>
        /// <returns></returns>
        internal DataTable merrMenuItemSipasKomponentesRegjistrimeDheNivelRegjistrimi(String emerKomponente, int idPerdoruesi, int idNdermarrje, int idViti, int idNivelRegjistrimi, bool shtimmodifikim)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@KOMPONENTE", emerKomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNIVELREGJISTRIMI", idNivelRegjistrimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@SHTIMMODIFIKIM", shtimmodifikim, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENUITEM_SipasKomponentesDheTeDrejtaRegjistrimeDheNivelRegjistrimi");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;
        }

        internal DataTable merrMenuItemSipasKomponentesDheRaporte(String emerKomponente, int idPerdoruesi, int idNdermarrje, int idViti, bool shtimmodifikim)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@KOMPONENTE", emerKomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@SHTIMMODIFIKIM", shtimmodifikim, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENUITEM_SipasKomponentesDheTeDrejtaDheRaporte");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;
        }

        internal DataTable merrMenuItemPerRaporte(String emerKomponente, int idPerdoruesi, int idNdermarrje, int idViti, bool shtimmodifikim)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@KOMPONENTE", emerKomponente, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@SHTIMMODIFIKIM", shtimmodifikim, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENUITEM_SipasRaportit");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;
        }

        /// <summary>
        /// kthen objektet gjithe menu items
        /// </summary>

        ///<returns>nje objekt datatable qe permban nje koleksion me te gjitha menu items  </returns>
        internal DataTable merrGjitheMenuItems()
        {
            dbManager.Open();

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_MENUITEM_merrTeGjitha");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;
        }

        #endregion MENU ITEM

        #region Arkiva

        internal bool ekzistonDokumentiSipasLlojDokAndID(int llojDok, int idDok)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDLLOJDOK", llojDok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDDOK", idDok, ParameterDirection.Input);
            int kaShenime = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ARKIVA_eksistonDokumenti"));

            return kaShenime > 0;
        }
        internal string merrThumnailDefault(int idDok)
        {
            string path = "";
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@PATH", path, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDDOK", idDok, ParameterDirection.Input);
            path=dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_ARKIVA_MERR_THUMBNAIL").ToString();
            dbManager.Close(); 
            return path;
        }
        internal DataTable ktheArkivaSipasIDDOK(int iddok, int idllojdok)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDDOK", iddok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJDOK", idllojdok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_Arkiva_sel");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;
        }

        internal DataTable ktheImazheArkivaSipasIDDOK(int iddok, int idllojdok)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDDOK", iddok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJDOK", idllojdok, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_Arkiva_sel_imazhe");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;
        }

        public clsMesazh RuajArkiva(out int idArkiv, int iddok, int idllojdok, string ftype, string fpath, String emerConn, string fname, string shenime, int idstatus, int idkrijuesi, int userid, int idKategoriArkive,bool thumb)
        {
            idArkiv = -1;

            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDARKIVA", idArkiv, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDDOK", iddok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJDOK", idllojdok, ParameterDirection.Input);
            dbManager.AddParameters(3, "@FILETYPE", ftype, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PATH", fpath, ParameterDirection.Input);
            dbManager.AddParameters(5, "@ConnEmer", emerConn, ParameterDirection.Input);
            dbManager.AddParameters(6, "@FILENAME", fname, ParameterDirection.Input);
            dbManager.AddParameters(7, "@SHENIME", shenime, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDSTATUSDOK", idstatus, ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDKRIJUESI", idkrijuesi, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDPERDORUESI", userid, ParameterDirection.Input);
            
            if (idKategoriArkive <= 0)
                dbManager.AddParameters(11, "@IDKATEGORIARKIVE", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(11, "@IDKATEGORIARKIVE", idKategoriArkive, ParameterDirection.Input);
            dbManager.AddParameters(12, "@THUMB", thumb, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARKIVA_ins");
            idArkiv = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }
        public clsMesazh setThumbnail( int iddok, int idllojdok, string path, int idperdorues)
        {
            
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDDOK", iddok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJDOK", idllojdok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PATH", path, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idperdorues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARKIVA_SET_THUMBNAIL");
           
            clsMesazh mesazh = new clsMesazh(true, "");
            return mesazh;

        }
        
  

        internal clsMesazh ModifikoSipasPathit(string oldPath, string newPath, string newFileName, int idPerdoruesi,String emerConn)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@OLDPATH", oldPath, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NEWPATH", newPath, ParameterDirection.Input);
            dbManager.AddParameters(2, "@FILENAME", newFileName, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@ConnEmer", emerConn, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARKIVA_ndryshoPath");
            return new clsMesazh(true);
        }
        
        public clsMesazh UpdateArkiva(int idArkiv, int iddok, int idllojdok, string ftype, string fpath, string fname, string shenime, int idstatus, int idkrijuesi, int userid, int idKategoriArkive,String emerConn, bool thumb=false)
        {
            dbManager.Open();
            dbManager.CreateParameters(12);
            dbManager.AddParameters(0, "@IDARKIVA", idArkiv, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDDOK", iddok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLLOJDOK", idllojdok, ParameterDirection.Input);
            dbManager.AddParameters(3, "@FILETYPE", ftype, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PATH", fpath, ParameterDirection.Input);
            dbManager.AddParameters(5, "@FILENAME", fname, ParameterDirection.Input);
            dbManager.AddParameters(6, "@SHENIME", shenime, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSTATUSDOK", idstatus, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDPERDORUESI", userid, ParameterDirection.Input);
            if (idKategoriArkive <= 0)
                dbManager.AddParameters(9, "@IDKATEGORIARKIVE", DBNull.Value, ParameterDirection.Input);
            else
                dbManager.AddParameters(9, "@IDKATEGORIARKIVE", idKategoriArkive, ParameterDirection.Input);
            dbManager.AddParameters(10, "@ConnEmer", emerConn, ParameterDirection.Input);
            dbManager.AddParameters(11, "@THUMB", thumb, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARKIVA_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);
            return mesazh;
        }
        

        internal clsMesazh UpdateArkiva(DataTable dtArkiva, int idDok, int idPerdoruesi,String emerConn)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@DTARKIVA", dtArkiva, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDDOK", idDok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@ConnEmer", emerConn, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_Arkiva_updDT");

            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        internal clsMesazh KopjoArkiva(DataTable dtArkiva, int idDok, int idPerdoruesi,String emerConn)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@DTARKIVA", dtArkiva, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDDOK", idDok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            //dbManager.AddParameters(3, "@ConnEmer", emerConn, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_Arkiva_insDT");

            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }
        public clsMesazh FshiArkiva(int iddok, int idllojdok, string fpath, int idstatus, int userid,String emerConn)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);

            dbManager.AddParameters(0, "@IDDOK", iddok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJDOK", idllojdok, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PATH", fpath, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDSTATUSDOK", idstatus, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUESI", userid, ParameterDirection.Input);
            dbManager.AddParameters(5, "@ConnEmer", emerConn, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARKIVA_upd_status");

            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh UpdateStatusDokFshiPerArkiven(int idDok, int idKategoria, int idPerdoruesi)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);

            dbManager.AddParameters(0, "@IDDOK", idDok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLLOJDOK", idKategoria, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);

            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_ARKIVA_FshiSipasIdDokAndLloji");

            return new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
        }

        #endregion Arkiva

        internal DataTable MerrArkivenSipasRootFolderit(string rootFolder)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@ROOTFOLDER", rootFolder, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_Arkiva_selSipasRootFolderit");
            return ds.Tables.Count != 0 ? ds.Tables[0] : null;
        }
        internal string ktheImazhPerdoruesi(int idPerdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            return Convert.ToString(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_Arkiva_selImazhPerdoruesi"));
        }
        #region Kategori Arkive

        public clsMesazh ruajKategoriArkive(out int idKategoriArkive, int idNivel, string kategoria, int idNdermarrje, int idKrijues)
        {
            idKategoriArkive = -1;
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDKATEGORIARKIVE", idKategoriArkive, ParameterDirection.Output);
            dbManager.AddParameters(1, "@IDNIVEL", idNivel, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KATEGORIA", kategoria, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDKRIJUES", idKrijues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KATEGORIARKIVE_ins");
            idKategoriArkive = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        public clsMesazh modifikoKategoriArkive(int idKategoriArkive, string kategoria, int idModifikues)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDKATEGORIARKIVE", idKategoriArkive, ParameterDirection.Input);
            dbManager.AddParameters(1, "@KATEGORIA", kategoria, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idModifikues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KATEGORIARKIVE_upd");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        public clsMesazh fshiKategoriArkive(int idKategoriArkive, int idModifikues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDKATEGORIARKIVE", idKategoriArkive, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUESI", idModifikues, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_KATEGORIARKIVE_upddel");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        public DataTable ktheKategoriArkiveSipasIdNdermarrje(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORIARKIVE_SelSipasNdermarrjeDt");
            return ds.Tables[0];
        }

        public DataTable ktheKategoriArkiveSipasIdNdermarrjeDheIdKatDok(int idNdermarrje, int idKatDok)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNIVEL", idKatDok, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORIARKIVE_SelSipasIdKatDok");
            return ds.Tables[0];
        }

        public DataRow ktheKategoriArkiveSipasIdKategorie(int idKategori)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDKATEGORIARKIVE", idKategori, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_KATEGORIARKIVE_SelSipasId");
            return ds.Tables[0].Rows[0];
        }

        public bool ekzistonKategoriArkiveSipasNivelDheNdermarrje(int idNdermarrje, int idNivel, string kategoria)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVEL", idNivel, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KATEGORI", kategoria, ParameterDirection.Input);
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KATEGORIARKIVE_ekzistonKategoriPerNivelDheNdermarrje").ToString());
            return nr > 0;
        }

        public bool ekzistonKategoriArkiveSipasNivelDheNdermarrjePervecVetes(int idNdermarrje, int idNivel, string kategoria, int idKategoriArkive)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNIVEL", idNivel, ParameterDirection.Input);
            dbManager.AddParameters(2, "@KATEGORI", kategoria, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDKATEGORIARKIVE", idKategoriArkive, ParameterDirection.Input);
            int nr = Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KATEGORIARKIVE_ekzistonKategoriPerNivelDheNdermarrjePervecVetes").ToString());
            return nr > 0;
        }

        #endregion Kategori Arkive
        internal DataTable merrParametraSpPerReportDesigner(string sp)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@SP", sp, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_MerrParametraSpPerReportDesinger");
            return ds.Tables[0];
        }

        #region raporti veprimtaria ditore
        internal DataTable merrRekordePerTuEksportuar(DataTable dt)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@DATA", dt, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_REKORDE_TE_EKSPORTUAR_PercaktoRekordePerTuEksportuar");
            return ds.Tables[0];
        }

        internal DataTable merrRekordeTeDetyrueshme()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "PRC_T_VEPRIMTARIA_DITORE_FUSHATEDETYRUESHME_selectAll");
            return ds.Tables[0];
        }

        internal void updateRekordeTeEksportuar(DataTable dt)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters("@DATA", dt, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_REKORDE_TE_EKSPORTUAR_UpdateRekordeTeEksportuar");
        }
        #endregion

        internal DataTable ktheRaportetDheAdresatEmail()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_EMAILRAPORT_sel");
            if (ds.Tables.Count != 0)
                return ds.Tables[0];
            return null;
        }

        #region NodeAPI

        internal DataTable merrFototPerNdermarrjenShtuarPasDatesFunditLexuar(int numerFotosh, int numerChunk, string kodNdermarrje, string perdorues, String DataFunditLexuar)
        {
            
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@NRSEL", numerFotosh, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRCHUNK", numerChunk, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DTFUNDITLEXUAR", DataFunditLexuar, ParameterDirection.Input);
            dbManager.AddParameters(3, "@NDERMARRJEKOD", kodNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(4, "@PERDORUES", perdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_Integrim_fotoNgaArkiv");
            return ds.Tables[0];
        }

        internal clsMesazh ruajNjoftim(out int njoftimeid, string titulli, string permbajtja, DateTime dt_fillimi, DateTime dt_mbarimi, DateTime dt_krijimi, DateTime dt_modifikimi, int statusi)
        {
            njoftimeid = -1;
            dbManager.Open();
            dbManager.CreateParameters(8);
            dbManager.AddParameters(0, "@NJOFTIMEID", njoftimeid, ParameterDirection.Output);
            dbManager.AddParameters(1, "@TITULLI", titulli, ParameterDirection.Input);
            dbManager.AddParameters(2, "@PERMBAJTJA", permbajtja, ParameterDirection.Input);
            dbManager.AddParameters(3, "@DATE_FILLIMI", dt_fillimi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@DATE_MBARIMI", dt_mbarimi, ParameterDirection.Input);
            dbManager.AddParameters(5, "@DATE_KRIJIMI", dt_krijimi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DATE_MODIFIKIMI", dt_modifikimi, ParameterDirection.Input);
            dbManager.AddParameters(7, "@STATUSI", statusi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "T_NJOFTIME_ins");
            njoftimeid = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal DateTime ktheDateFunditNjoftimi()
        {
            dbManager.Open();
            return Convert.ToDateTime(dbManager.ExecuteScalar(CommandType.StoredProcedure, "PRC_T_NJOFTIME_getLastDateModified"));
        }

        internal clsMesazh ruajNjoftimPerdorues(out int idnjofrimperdorues, int njoftimid, int idperdoruesi)
        {
            idnjofrimperdorues = -1;
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNJOFTIMPERDORUES", idnjofrimperdorues, ParameterDirection.Output);
            dbManager.AddParameters(1, "@NJOFTIMEID", njoftimid, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUES", idperdoruesi, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "T_NJOFTIME_PERDORUES_ins");
            idnjofrimperdorues = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        #endregion
        internal void updateDateDergimiPerEmaileRaporti(DateTime date, string raportePerUpdate)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters("DateDergimi", date, ParameterDirection.Input);
            dbManager.AddParameters("RaportePerUpdate", raportePerUpdate, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_EMAILRAPORT_upd");
        }
    }
}