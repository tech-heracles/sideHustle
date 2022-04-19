using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Resources;
using System.Globalization;
using AlphaWeb.Core.Interfaces.Data;
using DbCore.IMBUtils.DataBase;
using Microsoft.SqlServer.Types;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbGIS
{
    /// <remarks>
    ///  Kjo eshte klasa ma e rendesishme e ketij moduli. Ka nje klase te tille per secilin modul te projektit.
    ///  Ketu ndodhen metodat qe therriten nga te gjitha objektet brenda projektit WebGIS.DbCore.
    ///  Secila metode permban thirrjet e Stored procedures ne DB, duke i kaluar parametrat perkates
    ///  Metodat jane te ndara ne Regions sipas objekteve qe i therrasin keto metoda
    /// </remarks>

    public class clsDatabaseGIS : DbData
    {
        #region Atribute
        #endregion

        #region Konstruktoret
        public clsDatabaseGIS() : base() { }
        
        public clsDatabaseGIS(DbData db) : base(db) { }

        public clsDatabaseGIS(string connectionName) : base(connectionName)
        {

        }
        #endregion

        #region Metoda Internal

        #region Layers

        internal bool ekzistonLayerByTableName(string name)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDENTIFICATION", name, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_LAYERS_selSipasNameNeGeoserver");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        public DataTable merrLlojLayeriPerCombo(int idPerdorues, int gjuha, int idNdermarrje, int idViti,bool shfaqOrienutes)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@GJUHAID", gjuha, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@SHFAQORIENTUES", shfaqOrienutes, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_LAYERS_merrLayeraPerComboKerkimi");
            if (ds.Tables[0].Rows.Count != 0)
                return ds.Tables[0];
            else
                return null;
        }

        internal DataRow mbushObjektLayerKerkimiSipasGid(int idnderviti, Int32 gid, int gjuhaId)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@IDNDERMVIT", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(1, "@GID", gid, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GJUHAID", gjuhaId, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_LAYERS_merrSipasGid");
            return ds.Tables[0].Rows[0];
        }        

        internal DataTable ktheLayersType()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_LAYERSTYPE_selAll");
            return ds.Tables[0];
        }
        internal DataTable ktheGjitheObjektetGISDT(int idndermarje, int idnderviti, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(3);
            dbManager.AddParameters(0, "@idndermarrje", idndermarje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idndervit", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idgjuha", idGjuha, ParameterDirection.Input);

            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_GIS_App_LIDHJE_OBJEKTE_WEB_merrGjitheObjektetGISDT");
            return ds.Tables[0];

        }
        /// <summary>
        /// Mbush klasen e tipeve te ndermarrjes sipas id
        /// </summary>
        /// <param name="idLayerType">Id e tipit te layer</param>
        /// <returns>Kthen rreshtin e zgjedhur</returns>
        internal DataRow merrLayersTypeSipasId(int idLayerType)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDLAYERSTYPE", idLayerType, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_GIS_T_GIS_App_LAYERSTYPE_selByID");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        internal int ktheIdKonfigurimiNgaGisElement(int gid)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@gid", gid, ParameterDirection.Input);
            return Convert.ToInt32(dbManager.ExecuteScalar(CommandType.StoredProcedure, "prc_T_KONFIGAMBJENTE_ktheIdKonfigAmbjenteSipasLidhjesGISWEB"));
        }

        /// <summary>
        /// Mbush klasen per te gjithe layerat qe do te shfaqen ne gis
        /// </summary>
        /// <param name="idNdermarrje">kalon si parameter workspace per ndermarrjen e logauar</param>
        /// <param name="idGjuha">kalon si parameter per gjuhen</param>
        /// <returns>Kthen koleksionin e te dhenave e zgjedhur</returns>
        internal DataTable mbushDisplayLayersGIS(int idPerdorues, int gjuha, int idNdermarrje, int idViti)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@GJUHAID", gjuha, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDVITI", idViti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_DISPLAYLAYERS_ALL");
            return ds.Tables[0];
        }
        /// <summary>
        /// Kthen te gjithe trupin e layerave per ndermarrjen aktual. Nese nuk ekzistojne konfigurime specifike per te, kapen konfigurimet default
        /// </summary>
        /// <param name="idNdermarrje">kalon si parameter workspace per ndermarrjen e logauar</param>
        /// <param name="idGjuha">kalon si parameter per gjuhen</param>
        /// <returns>kthen te gjithe layrat baze qe do te shfaqen per rastin konktret</returns>
        internal DataTable merrLayersTrupiGIS(int idNdermarrje, int idGjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDGJUHA", idGjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_LAYERSTRUPI_selSipasNdermDheGjuhes");
            return ds.Tables[0];
        }

        internal DataTable merrSipasTeDrejtaveFolderat(int idPerdorues, int gjuha, int idNdermarrje, int idViti, string lloji, int all, string layerName)
        {
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@GJUHAID", gjuha, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@LLOJI", lloji, ParameterDirection.Input);
            dbManager.AddParameters(5, "@ALL", all, ParameterDirection.Input);
            dbManager.AddParameters(6, "@LAYERNAME", layerName, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_LAYERSFOLDER_merrSipasTeDrejtaveFolderat");
            return ds.Tables[0];
        }

        internal DataTable merrTeGjitheKolonat(int idNdermarrje, int gjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@GJUHAID", gjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_LAYERSCOLS_merrAll");
            return ds.Tables[0];
        }

        internal DataTable merrTeGjitheKolonatSipasIdLayer(int idLayer, int gjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@LAYERID", idLayer, ParameterDirection.Input);
            dbManager.AddParameters(1, "@GJUHAID", gjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_LAYERSCOLS_merrTeGjitheKolonatSipasIdLayer");
            if (ds.Tables[0].Rows.Count != 0)
                return ds.Tables[0];
            else
                return null;
        }

        internal DataTable merrTeGjitheKolonatSipasLayerTypeDheStatusit(int layerType, int nrStatusi, int gjuha, int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@LAYERTYPE", layerType, ParameterDirection.Input);
            dbManager.AddParameters(1, "@NRSTATUSI", nrStatusi, ParameterDirection.Input);
            dbManager.AddParameters(2, "@GJUHAID", gjuha, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_LAYERSCOLS_merrTeGjitheKolonatSipasLayerTypeDheStatusi");
            if (ds.Tables[0].Rows.Count != 0)
                return ds.Tables[0];
            else
                return null;
        }
               

        /// <summary>
        /// Merr te gjithe workspace te celura ne program
        /// </summary>
        /// <returns>DataTable me te gjithe objektet</returns>
        internal DataTable merrAllWorkspaceGrideAmbjenti()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_WORKSPACE_selAllPerGrideAmbjenti");
            if (ds.Tables[0].Rows.Count != 0)
                return ds.Tables[0];
            else
                return null;
        }

        /// <summary>
        /// Merr te gjithe workspace te celura ne program
        /// </summary>
        /// <returns>DataTable me te gjithe objektet</returns>
        internal DataTable merrAllWorkspace()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_WORKSPACE_selAll");
            if (ds.Tables[0].Rows.Count != 0)
                return ds.Tables[0];
            else
                return null;
        }

        internal DataRow merrWorkspaceSipasNdermarrjesId(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_GIS_merrWorkspaceSipasNdermarrjesId");
            if (ds == null)
                return null;
            if (ds.Tables[0].Rows.Count == 0 || ds.Tables[0].Rows.Count > 1)
                return null;
            return ds.Tables[0].Rows[0];
        }

        /// <summary>
        /// Kontrollon nese ekziston ndonje workspace i lidhur me ndermarrjen e dhene, ose nese ekziston nje workspace me te njejtin emer
        /// </summary>
        /// <param name="idnderm">Id e ndermarrjes qe do te kontrollohet</param>
        /// <param name="name">Emri i workspace qe duhet te jete unik ne db</param>
        /// <returns>Kthen true ose false sipas rezutateve</returns>
        internal bool ekzistonWorkspacePerNdermarrjen(int idnderm, string name)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            dbManager.AddParameters(1, "@WORKSPACENAME", name, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_WORKSPACE_SipasNdermarrjeID");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }

        /// <summary>
        /// Kontrollon nese ekziston ndonje layer i lidhur me ndermarrjen e dhene
        /// </summary>
        /// <param name="idnderm">Id e ndermarrjes qe do te kontrollohet</param>
        /// <returns>Kthen true ose false sipas rezutateve</returns>
        internal bool ekzistonLayerNeNdermarrjen(int idnderm)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idnderm, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_LAYERS_SipasNdermarrjeID");
            if (ds.Tables[0].Rows.Count == 1)
                return true;
            else if (ds.Tables[0].Rows.Count == 0)
                return false;
            else return true;
        }


        internal DataTable merrGeoProjectionsGIS()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_GEOPROJECTIONS_selAll");
            return ds.Tables[0];
        }

        /// <summary>
        /// Kthen te gjithe base layerat per ndermarrjen aktual. Nese nuk ekzistojne konfigurime specifike per te, kapen konfigurimet default
        /// </summary>
        /// <param name="idNdermarrje">kalon si parameter workspace per ndermarrjen e logauar</param>
        /// <returns>kthen te gjithe layrat baze qe do te shfaqen per rastin konktret</returns>
        internal DataTable merrBaseLayersGIS(int idNdermarrje)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_BASELAYERS_sel");
            return ds.Tables[0];
        }
        #endregion //end Layers

        #region Kerkimi

        internal DataTable getAllObjectsForQuickSearchDB(int gjuha, int idNdermarrje, int idnderviti, int idPerdoruesi, string gid)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@gjuha", gjuha, ParameterDirection.Input);
            dbManager.AddParameters(1, "@idNdermarrje", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@idNdermVit", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@idPerdoruesi", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(4, "@gid", gid, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_GIS_merrTeDhenatPerKerkimShpejt");
            return ds.Tables[0];
        }


        internal DataTable merrTeDhenatGeoPerLidhjeObjektesh(string bboxPolygon, string layer, int idnderviti, string excludeObject, string geometryObject, int idObjFillestar)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@bboxPolygon", bboxPolygon, ParameterDirection.Input);
            dbManager.AddParameters(1, "@layer", layer, ParameterDirection.Input);
            dbManager.AddParameters(2, "@excludeObject", excludeObject, ParameterDirection.Input);
            dbManager.AddParameters(3, "@geometryObject", geometryObject, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDOBJEKTIFILLESTAR", idObjFillestar, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDNDERMVIT", idnderviti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_GIS_merrTeDhenatGeoPerLidhjeObjektesh");
            return ds.Tables[0];
        }
        /// <summary>
        /// Kjo Sp do te ktheje te gjithe objektet qe ndodhen brenda boxit te vizatuar nga perdoruesi "bboxPolygon", te cilat priten me objektin ekzistues "originObject" me nje gabim me te vogel 1m
        /// dhe qe ploteson kushtet qe merge mund te behet me objekte qe i perkasin statusit te planifikuar ose seriali i objektit eshte i njejte (eshte bere me pare split)  
        /// </summary>
        /// <param name="bboxPolygon">BBox i vizatuar nga perdoruesi</param>
        /// <param name="originObject">gid e objektit kryesore</param>
        /// <param name="geometryObject">geometria e objektit kryesor (merret nga nderfaqja pasi mund te jete ndryshuar)</param>
        /// <returns></returns>
        internal DataTable merrTeDhenatGeoPerMergeObjektesh(string bboxPolygon, int idnderviti, string exludeObjects, string geometryObject, int originObject)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@bboxPolygon", bboxPolygon, ParameterDirection.Input);
            dbManager.AddParameters(1, "@originObject", originObject, ParameterDirection.Input);
            dbManager.AddParameters(2, "@geometryObject", geometryObject, ParameterDirection.Input);
            dbManager.AddParameters(3, "@exludeObjects", exludeObjects, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDNDERMVIT", idnderviti, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_GIS_merrTeDhenatGeoPerMergeObjektesh"); 
            return ds.Tables[0];
        }

        /// <summary>
        /// Kjo Sp do te ktheje te gjithe objektet qe ndodhen brenda boxit te vizatuar nga perdoruesi "bboxPolygon", te cilat priten me objektin ekzistues "originObject" me nje gabim me te vogel 1m
        /// dhe qe ploteson kushtet qe merge mund te behet me objekte qe i perkasin statusit te planifikuar ose seriali i objektit eshte i njejte (eshte bere me pare split)  
        /// </summary>
        /// <param name="bboxPolygon">BBox i vizatuar nga perdoruesi</param>
        /// <param name="originObject">gid e objektit kryesore</param>
        /// <param name="geometryObject">geometria e objektit kryesor (merret nga nderfaqja pasi mund te jete ndryshuar)</param>
        /// <returns></returns>
        internal DataTable merrGjeometryPerExtendSipasFunksionit(string fromQuery, string whereQuery)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@fromQuery", fromQuery, ParameterDirection.Input);
            dbManager.AddParameters(1, "@whereQuery", whereQuery, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_GIS_merrGjeometryPerExtendSipasFunksionit");
            return ds.Tables[0];
        }


        internal DataTable merrobjektetIntersectByBBoxMeFilter(string bboxPolygon, string layer, int idnderviti, string excludeObject, int idPerdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@geometry", bboxPolygon, ParameterDirection.Input);
            dbManager.AddParameters(1, "@em_tabele", layer, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMVIT", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@filterGid", excludeObject, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_GIS_merrObjektetIntersectPerEditim");
            return ds.Tables[0];
        }

        internal DataTable merrTeDhenatGeoEditimSnapDB(string bboxPolygon, string layers, int idnderviti, string excludeObject, int idPerdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@bboxPolygon", bboxPolygon, ParameterDirection.Input);
            dbManager.AddParameters(1, "@layers", layers, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMVIT", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@excludeObject", excludeObject, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_GIS_merrObjektetIntersectPerEditimSnap");
            return ds.Tables[0];
        }

        internal DataTable merrobjektetNgaKerkimiMeFilter(string bboxPolygon, string layer, string excludeObject, int idPerdorues)
        {
            dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@filterGid", excludeObject, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDPERDORUES", idPerdorues, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_GIS_merrObjektetPerKerkimGIS");
            return ds.Tables[0];
        }


        /// <summary>

        /// </summary>
        /// <param name="idNdermarrje"></param>
        /// <param name="idLayeri"></param>
        /// <returns></returns>
        internal DataTable merrFushaLayeri(int idNdermarrje, int idnderviti, int idLayeri, int gjuhaId)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDLAYER", idLayeri, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMVIT", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@GJUHAID", gjuhaId, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_GIS_KerkimGridaKoka");
            return ds.Tables[0];
        }

        internal DataTable merrTrupinEGrides(int idNdermarrje, int idnderviti, int idLayeri,string  kodLayeri, string kolonaGride, string filter, int gjuha, int idPerdoruesi, bool grupo)
        {
            dbManager.Open();
            dbManager.CreateParameters(9);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLAYERI", idLayeri, ParameterDirection.Input);
            dbManager.AddParameters(3, "@KODLAYER", kodLayeri, ParameterDirection.Input);
            dbManager.AddParameters(4, "@FUSHATEGRIDES", kolonaGride, ParameterDirection.Input);
            dbManager.AddParameters(5, "@FILTER", filter, ParameterDirection.Input);
            dbManager.AddParameters(6, "@GJUHAID", gjuha, ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(8, "@GRUPO", grupo, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_GIS_merrTeDhenaKerkimiGridaSipasKoditDheIdTipiOseIdLayeri");
            return ds.Tables[0];
        }

        internal DataTable merrTeDhenaPerInfo(int idNdermarrje, int idnderviti, int idLayeri, string kolonaGride, int gid, int idPerdoruesi, int gjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMVIT", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLAYERI", idLayeri, ParameterDirection.Input);
            dbManager.AddParameters(3, "@FUSHATEGRIDES", kolonaGride, ParameterDirection.Input);
            dbManager.AddParameters(4, "@GID", gid, ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@GJUHAID", gjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_GIS_merrTeDhenaInfo");
            return ds.Tables[0];
        }

        #endregion

        #region Skedaret
        internal DataTable getAllPerkthime(int tabela)
        {
            dbManager.Open();
            string sql = " select FUSHA=emer_kolona, PERSHKRIMI=emer_kolona_sq from kolonat_tab_geo where id_tabela_layer= " + tabela;
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }

        internal DataTable getAllSkedare()
        {
            dbManager.Open();
            string sql = " select * from skedaret";
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }
        internal DataTable getAllSkedareLloji(string lloji, int idPerdorues)
        {
            dbManager.Open();
            string sql = " select * from T_GIS_App_SKEDARET where IDSTATUSDOK=1 AND LLOJI='" + lloji + "' AND IDKRIJUESI='" + idPerdorues + "'";
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }
        internal DataTable getAllSkedareObjektGeo(string idDytesore)
        {
            dbManager.Open();
            string sql = " select * from T_GIS_App_SKEDARET where IDSTATUSDOK=1 AND IDDYTESORE='" + idDytesore + "'";
            DataSet ds = dbManager.ExecuteDataSet(CommandType.Text, sql);
            return ds.Tables[0];
        }


        internal clsMesazh ruajSkedaret(out int IDSKEDARET, string LLOJSKEDARI, string FILETYPE, string PATH, string FILENAME, string SHENIME, int IDSTATUSDOK, DateTime DTKRIJIMI, int IDKRIJUESI, DateTime DTMODIFIKIMI, int IDPERDORUESI, string PRAPASHTESESKEDAR, string IDDYTESORE)
        {
            IDSKEDARET = -1;
            dbManager.Open();
            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@IDSKEDARET", IDSKEDARET, ParameterDirection.Output);
            dbManager.AddParameters(1, "@LLOJSKEDARI", LLOJSKEDARI, ParameterDirection.Input);
            dbManager.AddParameters(2, "@FILETYPE", FILETYPE, ParameterDirection.Input);
            dbManager.AddParameters(3, "@PATH", PATH, ParameterDirection.Input);
            dbManager.AddParameters(4, "@FILENAME", FILENAME, ParameterDirection.Input);
            dbManager.AddParameters(5, "@SHENIME", SHENIME, ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDSTATUSDOK", IDSTATUSDOK, ParameterDirection.Input);
            dbManager.AddParameters(7, "@DTKRIJIMI", DTKRIJIMI, ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDKRIJUESI", IDKRIJUESI, ParameterDirection.Input);
            dbManager.AddParameters(9, "@DTMODIFIKIMI", DTMODIFIKIMI, ParameterDirection.Input);
            dbManager.AddParameters(10, "@IDPERDORUESI", IDPERDORUESI, ParameterDirection.Input);
            dbManager.AddParameters(11, "@PRAPASHTESESKEDAR", PRAPASHTESESKEDAR, ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDDYTESORE", IDDYTESORE, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GIS_App_SKEDARET_ins");
            IDSKEDARET = int.Parse(dbManager.Parameters[0].Value.ToString());
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        internal clsMesazh updateStatusSkedarNgaId(clsSkedaretGIS skedari)
        {
            dbManager.Open();
            dbManager.CreateParameters(4);
            dbManager.AddParameters(0, "@IDSKEDARET", skedari.IDSKEDARET, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDSTATUSDOK", skedari.IDSTATUSDOK, ParameterDirection.Input);
            dbManager.AddParameters(2, "@DTMODIFIKIMI", skedari.DTMODIFIKIMI, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDPERDORUESI", skedari.IDPERDORUESI, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GIS_App_SKEDARET_upd_status");
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);
            return mesazh;
        }

        #endregion // end Skedare

        #region Transaksione
        /// <summary>
        /// Per eliminimin e perseritjes se krijimit te parametrave ne ruajtjen/modifikimin/fshirjen
        /// </summary>
        /// <param name="dbManager"></param>
        /// <param name="dtLidhjeObjekteWebGis"></param>
        /// <param name="gidOut"></param>
        internal static void AddParametersPerLidhjeObjekteshGISWEB(IDbManager dbManager, DataTable dtLidhjeObjekteWebGis, bool gidOut )
        {
            dbManager.CreateParameters(21);
            if (gidOut == true)
                dbManager.AddParameters(0, "@gid", int.Parse(dtLidhjeObjekteWebGis.Rows[0]["gid"].ToString()), ParameterDirection.Output);
            else
                dbManager.AddParameters(0, "@gid", int.Parse(dtLidhjeObjekteWebGis.Rows[0]["gid"].ToString()), ParameterDirection.Input);
            dbManager.AddParameters(1, "@the_geom", dtLidhjeObjekteWebGis.Rows[0]["the_geom"].ToString(), ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDLAYER", int.Parse(dtLidhjeObjekteWebGis.Rows[0]["IDLAYER"].ToString()), ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDLAYERSTYPE", int.Parse(dtLidhjeObjekteWebGis.Rows[0]["IDLAYERSTYPE"].ToString()), ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDMAGAZINA", int.Parse(dtLidhjeObjekteWebGis.Rows[0]["IDMAGAZINA"].ToString()), ParameterDirection.Input);
            dbManager.AddParameters(5, "@IDKODIFIKIMI", int.Parse(dtLidhjeObjekteWebGis.Rows[0]["IDKODIFIKIMI"].ToString()), ParameterDirection.Input);
            dbManager.AddParameters(6, "@IDARTIKULLI", int.Parse(dtLidhjeObjekteWebGis.Rows[0]["IDARTIKULLI"].ToString()), ParameterDirection.Input);
            dbManager.AddParameters(7, "@IDSERIALI", int.Parse(dtLidhjeObjekteWebGis.Rows[0]["IDSERIALI"].ToString()), ParameterDirection.Input);
            dbManager.AddParameters(8, "@IDKOKADOK", int.Parse(dtLidhjeObjekteWebGis.Rows[0]["IDKOKADOK"].ToString()), ParameterDirection.Input);
            dbManager.AddParameters(9, "@IDTRUPIDOKLIDHES", int.Parse(dtLidhjeObjekteWebGis.Rows[0]["IDTRUPIDOKLIDHES"].ToString()), ParameterDirection.Input);
            dbManager.AddParameters(10, "@DTMODIFIKIMI", dtLidhjeObjekteWebGis.Rows[0]["DTMODIFIKIMI"].ToString(), ParameterDirection.Input);
            dbManager.AddParameters(11, "@IDPERDORUESI", int.Parse(dtLidhjeObjekteWebGis.Rows[0]["IDPERDORUESI"].ToString()), ParameterDirection.Input);
            dbManager.AddParameters(12, "@IDNDERMARJE", int.Parse(dtLidhjeObjekteWebGis.Rows[0]["IDNDERMARJE"].ToString()), ParameterDirection.Input);
            dbManager.AddParameters(13, "@KODI", dtLidhjeObjekteWebGis.Rows[0]["KODI"].ToString(), ParameterDirection.Input);
            dbManager.AddParameters(14, "@PERSHKRIMI", dtLidhjeObjekteWebGis.Rows[0]["PERSHKRIMI"].ToString(), ParameterDirection.Input);
            dbManager.AddParameters(15, "@SERIALKOD", dtLidhjeObjekteWebGis.Rows[0]["SERIALKOD"].ToString(), ParameterDirection.Input);
            dbManager.AddParameters(16, "@KODKODIFIKIMI", dtLidhjeObjekteWebGis.Rows[0]["KODKODIFIKIMI"].ToString(), ParameterDirection.Input);
            dbManager.AddParameters(17, "@Veprimi", dtLidhjeObjekteWebGis.Rows[0]["Veprimi"].ToString(), ParameterDirection.Input);
            dbManager.AddParameters(18, "@GidPrindi", int.Parse(dtLidhjeObjekteWebGis.Rows[0]["GidPrindi"].ToString()), ParameterDirection.Input);
            dbManager.AddParameters(19, "@NRSTATUSI", int.Parse(dtLidhjeObjekteWebGis.Rows[0]["NRSTATUSI"].ToString()), ParameterDirection.Input);
            dbManager.AddParameters(20, "@IDNDERMVIT", int.Parse(dtLidhjeObjekteWebGis.Rows[0]["IDNDERMVIT"].ToString()), ParameterDirection.Input);
        }

        /// <summary>
        /// Sherben per te ruajtur Objektet e GIS lidhur me WEB, ruhen ne te njejten kohe edhe lidhja me prindin dhe arkiva e dokumentave
        /// </summary>
        /// <param name="dtLidhjeObjekteWebGis"></param>
        /// <returns></returns>
        internal clsMesazh ruajLidhjeObjekteshGISWEB(DataTable dtLidhjeObjekteWebGis)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh();

            AddParametersPerLidhjeObjekteshGISWEB(dbManager, dtLidhjeObjekteWebGis, true);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GIS_App_LIDHJE_OBJEKTE_WEB_ins");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// Merr nje klase te objektesh behet modifikimi per te gjithe elementet perberese te saj (nivel objekti)
        /// </summary>
        /// <param name="dtLidhjeObjekteWebGis"></param>
        /// <returns></returns>
        internal clsMesazh ndryshoLidhjeObjekteshGISWEB(DataTable dtLidhjeObjekteWebGis)
        {
            clsMesazh mesazh = new clsMesazh();
            dbManager.Open();
            dbManager.CreateParameters(20);
            AddParametersPerLidhjeObjekteshGISWEB(dbManager, dtLidhjeObjekteWebGis, false);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GIS_App_LIDHJE_OBJEKTE_WEB_upd");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }

        /// <summary>
        /// Merr nje koleksion te objektesh behet modifikimi per te gjithe klasat perberese te saj (nivel array)
        /// </summary>
        /// <param name="dtLidhjeObjekteWebGis">RolDrejtaKoka konvertuar ne datatable</param>
        /// <returns>Nese Modifikimi u krye me sukses ose jo</returns>
        internal clsMesazh ndryshoLidhjeObjekteshGISWEBCol(DataTable dtLidhjeObjekteWebGis)
        {
            this.dbManager.Open();
            dbManager.CreateParameters(2);
            dbManager.AddParameters(0, "@type", "prc_T_GIS_App_LIDHJE_OBJEKTE_DATATABLE_upd", ParameterDirection.Input);
            dbManager.AddParameters(1, "@lidhjeObjekte", dtLidhjeObjekteWebGis, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GIS_App_LIDHJE_OBJEKTE_DATATABLE_upd");
            return new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
        }

        /// <summary>
        /// Fshihet objekti nga harta, lidhjet e tij, si dhe ruhet ne arkive veprimi i fshirjes
        /// </summary>
        /// <param name="dtLidhjeObjekteWebGis"></param>
        /// <returns></returns>
        internal clsMesazh fshiLidhjeObjekteshGISWEB(DataTable dtLidhjeObjekteWebGis)
        {
            clsMesazh mesazh = new clsMesazh();
            dbManager.Open();
            dbManager.CreateParameters(20);
            AddParametersPerLidhjeObjekteshGISWEB(dbManager, dtLidhjeObjekteWebGis, false);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GIS_App_LIDHJE_OBJEKTE_WEB_del");
            mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);
            return mesazh;
        }


        /// <summary>
        /// Sherben per te ruajtur nje workspace te ri, i cili do ti bashkengjitet nje 
        /// </summary>
        /// <param name="workspace"> Objekti qe do te ruhet</param>
        /// <returns>Kthen id e objektit te ri</returns>
        internal clsMesazh ruajWorkspaceGIS(clsWorkspaceGIS workspace)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@id_workspace", workspace.Id_workspace, ParameterDirection.Output);
            dbManager.AddParameters(1, "@workspace_name", workspace.Workspace_name, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", workspace.IDNDERMARJE, ParameterDirection.Input);
            dbManager.AddParameters(3, "@GEOURL", workspace.GEOURL, ParameterDirection.Input);
            dbManager.AddParameters(4, "@GEOUSER", workspace.GEOUSER, ParameterDirection.Input);
            dbManager.AddParameters(5, "@GEOPASSWORD", workspace.GEOPASSWORD, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DSDATABASE", workspace.DSDATABASE, ParameterDirection.Input);
            dbManager.AddParameters(7, "@DSPORT", workspace.DSPORT, ParameterDirection.Input);
            dbManager.AddParameters(8, "@DSUSER", workspace.DSUSER, ParameterDirection.Input);
            dbManager.AddParameters(9, "@WSFUNCTIONS", workspace.WSFUNCTIONS, ParameterDirection.Input);
            dbManager.AddParameters(10, "@WSDEFAULT", workspace.WSDEFAULT, ParameterDirection.Input);
            dbManager.AddParameters(11, "@WSGUIDING", workspace.WSGUIDING, ParameterDirection.Input);
            dbManager.AddParameters(12, "@WSPUBLIC", workspace.WSPUBLIC, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GIS_App_WORKSPACE_ins");

            workspace.Id_workspace = int.Parse(dbManager.Parameters[0].Value.ToString());

            return mesazh;
        }
        
        /// <summary>
        /// Sherben per te ruajtur ndryshimet ne konfigurimet e nje workspace 
        /// </summary>
        /// <param name="workspace"> Objekti qe do te ruhet</param>
        /// <returns>Kthen nese ruajtja perdundoi me sukses apo jo</returns>
        internal clsMesazh modifikoWorkspaceGIS(clsWorkspaceGIS workspace)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgModifikimiMeSukses"]);

            dbManager.CreateParameters(13);
            dbManager.AddParameters(0, "@id_workspace", workspace.Id_workspace, ParameterDirection.Input);
            dbManager.AddParameters(1, "@workspace_name", workspace.Workspace_name, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", workspace.IDNDERMARJE, ParameterDirection.Input);
            dbManager.AddParameters(3, "@GEOURL", workspace.GEOURL, ParameterDirection.Input);
            dbManager.AddParameters(4, "@GEOUSER", workspace.GEOUSER, ParameterDirection.Input);
            dbManager.AddParameters(5, "@GEOPASSWORD", workspace.GEOPASSWORD, ParameterDirection.Input);
            dbManager.AddParameters(6, "@DSDATABASE", workspace.DSDATABASE, ParameterDirection.Input);
            dbManager.AddParameters(7, "@DSPORT", workspace.DSPORT, ParameterDirection.Input);
            dbManager.AddParameters(8, "@DSUSER", workspace.DSUSER, ParameterDirection.Input);
            dbManager.AddParameters(9, "@WSFUNCTIONS", workspace.WSFUNCTIONS, ParameterDirection.Input);
            dbManager.AddParameters(10, "@WSDEFAULT", workspace.WSDEFAULT, ParameterDirection.Input);
            dbManager.AddParameters(11, "@WSGUIDING", workspace.WSGUIDING, ParameterDirection.Input);
            dbManager.AddParameters(12, "@WSPUBLIC", workspace.WSPUBLIC, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GIS_App_WORKSPACE_upd");

            return mesazh;
        }

        /// <summary>
        /// Fshin nje workspace
        /// </summary>
        /// <param name="workspace"> Objekti qe do te fshihet</param>
        /// <returns>True/False</returns>
        internal clsMesazh fshiWorkspaceGIS(clsWorkspaceGIS workspace)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);

            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@id_workspace", workspace.Id_workspace, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GIS_App_WORKSPACE_del");

            return mesazh;
        }
        /// <summary>
        /// Sherben per te ruajtur layerat base qe do ti bashkengjiten workspace psh (google, bing, ortofoto ... etj)
        /// </summary>
        /// <param name="workspace"> Objekti qe do te ruhet</param>
        /// <returns>Kthen true ose false ne ruajtjen me sukses</returns>
        internal clsMesazh ruajBaseLayerXWorkspaceGIS(int Id_workspace)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@id_workspace", Id_workspace, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GIS_App_LAYERSXWORKSPACE_ins");

            return mesazh;
        }
        /// <summary>
        /// Sherben per te fshire layerat base qe do i bashkengjiten nje workspace psh (google, bing, ortofoto ... etj)
        /// </summary>
        /// <param name="workspace"> Objekti qe do te ruhet</param>
        /// <returns>Kthen true ose false ne ruajtjen me sukses</returns>
        internal clsMesazh fshiBaseLayerXWorkspaceGIS(int Id_workspace)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["msgFshirjeMeSukses"]);

            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@id_workspace", Id_workspace, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GIS_App_LAYERSXWORKSPACE_del");

            return mesazh;
        }
        /// <summary>
        /// Procedura per ruajten e nje layeri te ri, i cili do te beje shtimin e te dhenave te nevojshme ne te gjithe tabelat duke perfshire edhe tek te drejtat
        /// </summary>
        /// <param name="idndermarrje">Ndermarrja ku do te shtohet layer</param>
        /// <param name="layerName">Pershkrimi i layerit</param>
        /// <param name="identifikuesi">Emri i view unik ne sql dhe ne workspace</param>
        /// <param name="idFolder">Id e grupit te shtresave te hartes ku do te futet layeri</param>
        /// <param name="idLayerType">Tipi i layerit</param>
        /// <param name="nrStatusi">Statusi i layerit</param>
        /// <param name="type">Tipi i gjeometrise</param>
        /// <returns>Kthen true ose false ne ruajtjen me sukses</returns>
        internal clsMesazh ruajNewLayerMeTeDrejtaGIS(int idndermarrje, string layerName, string identifikuesi, int idFolder, int idLayerType, int nrStatusi, string type)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            dbManager.CreateParameters(7);
            dbManager.AddParameters(0, "@IDNDERMARJE", idndermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@DESCRIPTION", layerName, ParameterDirection.Input);
            dbManager.AddParameters(2, "@@IDENTIFICATION", identifikuesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDFOLDER", idFolder, ParameterDirection.Input);
            dbManager.AddParameters(4, "@IDLAYERTYPE", idLayerType, ParameterDirection.Input);
            dbManager.AddParameters(5, "@NRSTATUSI", nrStatusi, ParameterDirection.Input);
            dbManager.AddParameters(6, "@TYPE", type, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GIS_App_LAYERS_insertDataToAllTables");

            return mesazh;
        }

        /// <summary>
        /// Krijon ne menyre automatike strukturen e view qe do te perdore Layer i krijuar
        /// </summary>
        /// <param name="createLayerViewStruc">Stringu qe ekzekutohet</param>
        /// <returns>True nese struktura u shtua me sukses</returns>
        internal clsMesazh createLayerViewStruc(string createLayerViewStruc)
        {
            dbManager.Open();
            clsMesazh mesazh = new clsMesazh(true, MessagesResource.Messages["mesazhRuajtjeMeSukses"]);

            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@VIEWSTRUCT", createLayerViewStruc, ParameterDirection.Input);
            dbManager.ExecuteNonQuery(CommandType.StoredProcedure, "prc_T_GIS_App_LAYERS_createAutomaticView");

            return mesazh;
        }

        #endregion

        internal DataTable MerrObjekteSipasBoundBoxit(int idNdermarrje, string boundBox, int idPerdoruesi, int idViti, int gjuha)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(1, "@BOUND", boundBox, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDPERDORUESI", idPerdoruesi, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@GJUHAID", gjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_GIS_merrTeDhenaKerkimiHapsinor");
            return ds.Tables[0];
        }

        internal DataTable merrFushaLayeriSipasKoditDheIdTipiOseIdLayeri(int idNdermarrje, int idnderviti, int idLayerOseIdLayerType, string kodiLayeri, int gjuha, bool grupo)
        {
            dbManager.Open();
            dbManager.CreateParameters(6);
            dbManager.AddParameters(0, "@KODILAYERI", kodiLayeri, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDLAYERSTYPE", idLayerOseIdLayerType, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDNDERMARJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDNDERMVIT", idnderviti, ParameterDirection.Input);
            dbManager.AddParameters(4, "@GJUHAID", gjuha, ParameterDirection.Input);
            dbManager.AddParameters(5, "@GRUPO", grupo, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_GIS_KerkimGridaKokaSipasKoditDheIdTipiOseIdLayeri");
            return ds.Tables[0];
        }

        internal DataTable merrKordinataPerZgjedhjeQV2017NeDB(string filter)
        {
            dbManager.Open();
            dbManager.CreateParameters(1);
            dbManager.AddParameters(0, "@filter", filter, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_Integrim_GIS_merrZgjedhje2017_QV");
            return ds.Tables[0];
        }

        #endregion

        #region Metoda Publike
        public DataTable merrListeStatuseshSipasTeDrejtaLayeri(int layerType, int gjuha, int idPerdorues, int idNdermarrje, int idViti)
        {
            dbManager.Open();
            dbManager.CreateParameters(5);
            dbManager.AddParameters(0, "@IDPERDORUESI", idPerdorues, ParameterDirection.Input);
            dbManager.AddParameters(1, "@IDNDERMARRJE", idNdermarrje, ParameterDirection.Input);
            dbManager.AddParameters(2, "@IDVITI", idViti, ParameterDirection.Input);
            dbManager.AddParameters(3, "@IDLAYERSTYPE", layerType, ParameterDirection.Input);
            dbManager.AddParameters(4, "@GJUHAID", gjuha, ParameterDirection.Input);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_LAYERS_merrTeGjitheStatusetSipasLayerType");
            if (ds.Tables[0].Rows.Count != 0)
                return ds.Tables[0];
            else
                return null;
        }       

        public DataTable ktheGjitheLlojLayerMagazine()
        {
            dbManager.Open();
            dbManager.CreateParameters(0);
            DataSet ds = dbManager.ExecuteDataSet(CommandType.StoredProcedure, "prc_T_GIS_App_LLOJLAYERMAGAZINE_merrGjitheLlojLayerMagazine");
            return ds.Tables[0];
        }

        #endregion

    }
}