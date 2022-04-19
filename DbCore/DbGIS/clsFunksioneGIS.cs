using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Security.Cryptography;

using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Resources;

using System.Web.Security;
using System.Web.SessionState;

using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Net;
using System.Configuration;

using DbCore.DbGIS;
using DbCore.DbAdmin;
using DbCore.DbShare;

using NetTopologySuite.Features;
using NetTopologySuite.IO;
using NetTopologySuite.IO.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Microsoft.SqlServer.Types;
using DbCore.IMBUtils.Messages;

namespace DbCore.DbGIS
{
    public class clsFunksioneGIS
    {

        #region GeometryDataCollection

        /// <summary>
        /// Ne menyre qe te behet zgjedhja sipas tipit te kerkeses
        /// </summary>
        public enum LlojKonvertimiTrupFeatures
        {
            Lite = 1,
            AllColumns = 2,
            AllLidhjeWeb = 3,
            Merge = 4
        }

        public static SqlGeometry kovertoStringNeSQLGeometry(string the_geom)
        {
            SqlGeometry shape = SqlGeometry.Parse(the_geom);
            shape.STSrid = 32634;
            return shape;
        }


        /// <summary>
        /// Merr si parameter nje Koleksion Features dhe ben serializimin e saj te pershtatshme per ngarkimin e tyre ne harte
        /// </summary>
        /// <param name="featureCollection">merr koleksion te dhenash</param>
        /// <returns>kthen string</returns>
        public static string serializoFeatureCollection(NetTopologySuite.Features.FeatureCollection featureCollection)
        {
            var result = "";
            try
            {
                var sb = new StringBuilder();
                var serializer = new NetTopologySuite.IO.GeoJsonSerializer();
                serializer.Formatting = Newtonsoft.Json.Formatting.Indented;

                using (var sw = new StringWriter(sb))
                {
                    serializer.Serialize(sw, featureCollection);
                }

                result = sb.ToString();
                return result;
            }
            catch
            {
                return result;
            }
        }

        public static Newtonsoft.Json.Linq.JObject deserializoFeatureCollection(string featureString)
        {
            var sr = new StringReader(featureString);
            var reader = new NetTopologySuite.IO.GeoJsonReader();
            var geomjson = new GeoJsonSerializer();
            Newtonsoft.Json.Linq.JObject result = (Newtonsoft.Json.Linq.JObject)geomjson.Deserialize(new Newtonsoft.Json.JsonTextReader(sr));
            return result;
        }

        /// <summary>
        /// Merr si parameter nje datatable dhe kthen nje koleksion Features te pershtatura per shkrimin e tyre ne harte
        /// </summary>
        /// <param name="dt">te gjithe te dhenat nga datatable</param>
        /// <returns>kthen koleksion</returns>
        public static NetTopologySuite.Features.FeatureCollection konvertoDataTableNeFeatures(DataTable dt, LlojKonvertimiTrupFeatures tipi)
        {
            var featureCollection = new NetTopologySuite.Features.FeatureCollection();

            try
            {
                var wkbReader = new NetTopologySuite.IO.WKBReader();
                for (int i = 0, count = dt.Rows.Count; i < count; i++)
                {
                    var featureAtributes = new NetTopologySuite.Features.AttributesTable();
                    switch (tipi)
                    {
                        case LlojKonvertimiTrupFeatures.Lite:
                            featureCollection.Add(konvertoRowNeFeaturesLiteGeometry(wkbReader, featureAtributes, dt.Rows[i]));
                            break;
                        case LlojKonvertimiTrupFeatures.AllColumns:
                        case LlojKonvertimiTrupFeatures.AllLidhjeWeb:
                        case LlojKonvertimiTrupFeatures.Merge:
                            featureCollection.Add(konvertoRowNeFeaturesTableGeometry(wkbReader, featureAtributes, dt.Rows[i], dt));
                            break;
                    }
                }
            }
            catch (Exception)
            {
                return featureCollection;
            }
            return featureCollection;
        }





        /// <summary>
        /// Merr si parameter nje DataRow dhe ben leximin e features qe ai ka
        /// </summary>
        /// <param name="wkbReader">sherben per leximin e kolones se gjeometrise e cila duhet te konvertohet ne byte</param>
        /// <param name="featureAtributes">lexon gjithe kolonat e tjera qe na duhen sipas rastit. Default geometri do te ktheje vetem id dhe the_geom</param>
        /// <param name="row">DataRow me all features</param>
        /// <returns>Kthen nje objekt te hartes me gjithe features se tyre</returns>
        internal static NetTopologySuite.Features.Feature konvertoRowNeFeaturesLiteGeometry(NetTopologySuite.IO.WKBReader wkbReader, NetTopologySuite.Features.AttributesTable featureAtributes, DataRow row)
        {
            if (row != null)
            {
                try
                {
                    featureAtributes.AddAttribute("gid", row["gid"].ToString());
                    return new NetTopologySuite.Features.Feature(wkbReader.Read((byte[])row["wkb"]), featureAtributes);
                }
                catch (InvalidCastException)
                {
                    return new NetTopologySuite.Features.Feature();
                }
            }
            else
                return new NetTopologySuite.Features.Feature();
        }
        internal static NetTopologySuite.Features.Feature konvertoRowNeFeaturesTableGeometry(NetTopologySuite.IO.WKBReader wkbReader, NetTopologySuite.Features.AttributesTable featureAtributes, DataRow row, DataTable dt)
        {
            if (row != null)
            {
                try
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        if (column.ColumnName != "wkb" && column.ColumnName != "the_geom")
                        {
                            featureAtributes.AddAttribute(column.ColumnName, row[column.ColumnName].ToString());
                        }
                    }
                    return new NetTopologySuite.Features.Feature(wkbReader.Read((byte[])row["wkb"]), featureAtributes);
                }
                catch (InvalidCastException)
                {
                    return new NetTopologySuite.Features.Feature();
                }
            }
            else
                return new NetTopologySuite.Features.Feature();
        }
        #endregion

        #region ObjektetNeBBox

        public static string konvertoArrayNeStringSingleQuotes(string[] arrayKonvertuar)
        {
            string gidFilter = "";
            if (arrayKonvertuar.Length != 0)
            {
                for (int i = 0; i < arrayKonvertuar.Length; i++)
                {
                    gidFilter = gidFilter + "" + arrayKonvertuar[i] + "";
                    if (i != arrayKonvertuar.Length - 1)
                    {
                        gidFilter = gidFilter + ",";
                    }
                }
            }
            else
            {
                gidFilter = "0";
            }
            return gidFilter;
        }

        public static clsObjBounds konvertoStringNeBBoxObject(string bbox)
        {
            string[] bb = bbox.Split(',');
            clsObjPoint p1 = new clsObjPoint(decimal.Parse(bb[0]), decimal.Parse(bb[1]));
            clsObjPoint p2 = new clsObjPoint(decimal.Parse(bb[2]), decimal.Parse(bb[3]));
            clsObjBounds bounds = new clsObjBounds(p1, p2);
            return bounds;
        }

        public static string konvertoBBoxObjectNePolygon(clsObjBounds bounds)
        {
            return "POLYGON((" + bounds.min.lon + " " + bounds.min.lat + "," + bounds.max.lon + " " + bounds.min.lat + ',' + bounds.max.lon + " " + bounds.max.lat + ',' + bounds.min.lon + " " + bounds.max.lat + ',' + bounds.min.lon + " " + bounds.min.lat + "))";
        }


        #endregion

        #region Protokollet
        
        public static FeatureCollection merrTeDhenatGeofc(System.Web.SessionState.HttpSessionState Session, string layer, string bbox, string gidFilter, string objGeometry, LlojKonvertimiTrupFeatures tipi, int idPerdoruesi, int idObjFillestar)
        {
            FeatureCollection result = null;
            colDisplayLayersGIS displayLayerCol = new colDisplayLayersGIS();
            displayLayerCol = DbCore.mySessionObjects.merrDisplayLayersNgaSession(Session);
            List<clsDisplayLayersGIS> listFilter;

            if (layer != "")
                listFilter = displayLayerCol.FindAll(x => (x.D_MOD == true || x.D_SHTIM == true || x.D_FSH == true) && x.IDENTIFICATION == layer);
            else
                listFilter = displayLayerCol.FindAll(x => x.D_MOD == true || x.D_SHTIM == true || x.D_FSH == true);
            colDisplayLayersGIS displayLayerColFiltruar = new colDisplayLayersGIS(listFilter);

            if (displayLayerColFiltruar.Count > 0)
            {
                int idnderviti = displayLayerColFiltruar[0].WITHFISCALYEAR ? mySessionObjects.ktheNdermarrjeVit(Session) : 0;
                gidFilter = konvertoArrayNeStringSingleQuotes(JsonConvert.DeserializeObject<string[]>(gidFilter));
                clsObjBounds bounds = konvertoStringNeBBoxObject(bbox);

                switch (tipi)
                {
                    case LlojKonvertimiTrupFeatures.Lite:
                        result = merrTeDhenatGeoKerkimSelectfc(bounds, layer, gidFilter, idPerdoruesi);
                        break;
                    case LlojKonvertimiTrupFeatures.AllColumns:
                        result = merrTeDhenatGeoEditimSelectfc(bounds, layer, idnderviti, gidFilter, idPerdoruesi);
                        break;
                    case LlojKonvertimiTrupFeatures.AllLidhjeWeb:
                        result = merrTeDhenatGeoPerLidhjeObjekteshfc(bounds, layer, idnderviti, gidFilter, objGeometry, idObjFillestar);
                        break;
                    case LlojKonvertimiTrupFeatures.Merge:
                        result = merrTeDhenatGeoMergeSelectfc(bounds, idnderviti, gidFilter, objGeometry, idObjFillestar);
                        break;
                }
            }
            return result;
        }
        public static FeatureCollection merrTeDhenatGeoSnapGeoFc(string layers, int idnderviti, string bbox, string excludeGid, int idPerdoruesi)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            layers = konvertoArrayNeStringSingleQuotes(js.Deserialize<string[]>(layers));
            clsObjBounds bounds = konvertoStringNeBBoxObject(bbox);

            return merrTeDhenatGeoEditimSnapGeoFc(bounds, layers, idnderviti, excludeGid, idPerdoruesi);
        }
        
        public static FeatureCollection merrTeDhenatGeoEditimSnapGeoFc(clsObjBounds bounds, string layers, int idndervit, string excludeGid, int idPerdoruesi)
        {
            using (DbGIS.clsDatabaseGIS db = new DbGIS.clsDatabaseGIS())
            {
                DataTable dt = db.merrTeDhenatGeoEditimSnapDB(konvertoBBoxObjectNePolygon(bounds), layers, idndervit, excludeGid, idPerdoruesi);
                return konvertoDataTableNeFeatures(dt, LlojKonvertimiTrupFeatures.AllColumns);
            }
        }

        public static FeatureCollection merrTeDhenatGeoEditimSelectfc(clsObjBounds bounds, string layer, int idnderviti, string gidFilter, int idPerdorues)
        {
            using (DbGIS.clsDatabaseGIS db = new DbGIS.clsDatabaseGIS())
            {
                DataTable dt = db.merrobjektetIntersectByBBoxMeFilter(konvertoBBoxObjectNePolygon(bounds), layer, idnderviti, gidFilter, idPerdorues);

                return konvertoDataTableNeFeatures(dt, LlojKonvertimiTrupFeatures.AllColumns);
            }
        }

        public static FeatureCollection merrTeDhenatGeoKerkimSelectfc(clsObjBounds bounds, string layer, string gidFilter, int idPerdorues)
        {
            using (DbGIS.clsDatabaseGIS db = new DbGIS.clsDatabaseGIS())
            {
                DataTable dt = db.merrobjektetNgaKerkimiMeFilter(konvertoBBoxObjectNePolygon(bounds), layer, gidFilter, idPerdorues);

                return konvertoDataTableNeFeatures(dt, LlojKonvertimiTrupFeatures.AllColumns);
            }
        }

        public static FeatureCollection merrTeDhenatGeoPerLidhjeObjekteshfc(clsObjBounds bounds, string layer, int idnderviti, string gidFilter, string objGeometry, int idObjFillestar)

        {
            DbGIS.clsDatabaseGIS db = new DbGIS.clsDatabaseGIS();
            DataTable dt = db.merrTeDhenatGeoPerLidhjeObjektesh(konvertoBBoxObjectNePolygon(bounds), layer, idnderviti, gidFilter, objGeometry, idObjFillestar);
            return konvertoDataTableNeFeatures(dt, LlojKonvertimiTrupFeatures.AllLidhjeWeb);
        }

        public static FeatureCollection merrTeDhenatGeoMergeSelectfc(clsObjBounds bounds, int idnderviti, string gidFilter, string objGeometry, int idObjFillestar)
        {
            DbGIS.clsDatabaseGIS db = new DbGIS.clsDatabaseGIS();
            DataTable dt = db.merrTeDhenatGeoPerMergeObjektesh(konvertoBBoxObjectNePolygon(bounds), idnderviti, gidFilter, objGeometry, idObjFillestar);
            return konvertoDataTableNeFeatures(dt, LlojKonvertimiTrupFeatures.Merge);
        }

        public static FeatureCollection merrGjeometryPerExtendSipasFunksionitFc(string fromQuery, string whereQuery )
        {
            using (DbGIS.clsDatabaseGIS db = new DbGIS.clsDatabaseGIS())
            {
                DataTable dt = db.merrGjeometryPerExtendSipasFunksionit(fromQuery, whereQuery);
                return konvertoDataTableNeFeatures(dt, LlojKonvertimiTrupFeatures.Lite);
            }
        }

        #endregion

        #region Kerkimi
        public static List<Dictionary<string, object>> getAllObjectsForQuickSearch(int gjuha, int idNdermarrje, int idnderviti, int idPerdoruesi)
        {
            DbGIS.clsDatabaseGIS db = new DbGIS.clsDatabaseGIS();
            DataTable pergjigje = db.getAllObjectsForQuickSearchDB(gjuha, idNdermarrje, idnderviti, idPerdoruesi, "");

            List<Dictionary<string, object>> lista = ConvertDataTabletoList(pergjigje);
            return lista;
        }
        public static string getObjectForQuickSearch(int gjuha, int idNdermarrje, int idnderviti, int idPerdoruesi, string gid)
        {
            DbGIS.clsDatabaseGIS db = new DbGIS.clsDatabaseGIS();
            DataTable pergjigje = db.getAllObjectsForQuickSearchDB(gjuha, idNdermarrje, idnderviti, idPerdoruesi, gid);

            List<Dictionary<string, object>> lista = ConvertDataTabletoList(pergjigje);

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string sJSON = serializer.Serialize(lista);

            return sJSON;
        }
        public static List<Dictionary<string, object>> ChangeKeyFromListDictionaryForGridViewAll(int tableId, List<Dictionary<string, object>> lista)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;

            DbGIS.clsDatabaseGIS db = new DbGIS.clsDatabaseGIS();
            colKonfigurimeGIS allPerkthimet = new colKonfigurimeGIS();
            allPerkthimet.mbushKonfigurime(tableId, db);

            foreach (var listaRow in lista)
            {
                row = new Dictionary<string, object>();
                foreach (var listaColumn in listaRow)
                {
                    clsKonfigurimeGIS tempKonfig = new clsKonfigurimeGIS();
                    string newKey = tempKonfig.findPershkrimByFusha(allPerkthimet, listaColumn.Key).Pershkrimi; //findPershkrimByFusha(allPerkthimet, listaColumn.Key).Pershkrimi;
                    row.Add(newKey, listaColumn.Value);
                }
                rows.Add(row);
            }
            return rows;
        }

        public static List<Dictionary<string, object>> ConvertDataTabletoList(DataTable dt)
        {
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col].ToString());
                }
                rows.Add(row);
            }
            return rows;
        }

        public static object GetFeatureInfo(string gid, string layerName, int indeksi, int idNdermarrje, int idnderviti, int idPerdorues, int idGjuha, colDisplayLayersGIS displayLayerCol, colLayersTypeGIS layersTypeGIS)
        {
            clsDisplayLayersGIS displayLayerClass = new clsDisplayLayersGIS();
            colArkiva arkivaDok = new colArkiva(int.Parse(gid), 142);

            if (layerName == "V_GIS_Layer_KERKIMISPECIFIK")
            {
                clsLayersKokaGIS layKoka = new clsLayersKokaGIS();
                layKoka.mbushObjektLayerKerkimiSipasGid(idnderviti, int.Parse(gid), idGjuha);
                displayLayerClass = displayLayerCol.Find(x => x.IDENTIFICATION == layKoka.IDENTIFICATION);
            }
            else
                displayLayerClass = displayLayerCol.Find(x => x.IDENTIFICATION == layerName);

            if (displayLayerClass.IDLAYERSTYPE != 2 && displayLayerClass.IDLAYERSTYPE != 1 && displayLayerClass.CONNECTURL == "")
            {
                DataTable data = new DataTable();
                string kolonaGride = colDisplayLayersGIS.MerrFushaLayeri(idNdermarrje, idnderviti, displayLayerClass.IDLAYER, true, idGjuha);
                data = colDisplayLayersGIS.MerrTeDhenaPerInfon(idNdermarrje, idnderviti, displayLayerClass.IDLAYER, kolonaGride, int.Parse(gid), idPerdorues, idGjuha);
                return new { teDhenat = clsFunksione.ConvertDataTabletoString(data), indeksi = indeksi, idLayerType = displayLayerClass.IDLAYERSTYPE, webLloji = layersTypeGIS.Find(x => x.IDLAYERSTYPE == displayLayerClass.IDLAYERSTYPE).WEBLLOJI, arkivaDok = arkivaDok };
            }
            else return new { teDhenat = "", indeksi = indeksi, idLayerType = displayLayerClass.IDLAYERSTYPE, webLloji = "", arkivaDok = arkivaDok };

        }

        #endregion

        #region Skedaret
        public static clsMesazh eshteUpload(clsMesazh result, HttpContext context, HttpServerUtility Server, string fileId, string filePrefix, ArrayList filesEx, string fileEx, string filePath)
        {

            if (filesEx.Contains(fileEx))
            {
                var fileNameNew = filePrefix + "_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + Guid.NewGuid().ToString("N") + fileEx;
                var path = Path.Combine(Server.MapPath(filePath), fileNameNew);
                context.Request.Files[fileId].SaveAs(path);
                result.Status = true;
                result.PershkrimMesazhi = fileNameNew;
            }
            else
            {
                result.Status = false;
                result.PershkrimMesazhi = "KUJDES:  Tipi i skedarit nuk eshte i lejuar!";
            }
            return result;
        }
        public static colSkedaretGIS merrTeGjitheSkedaretUploadSipasLlojit(string lloji, int idPerdorues)
        {
            colSkedaretGIS skedaret = new colSkedaretGIS();
            skedaret.mbushSkedareLloji(lloji, idPerdorues);

            //DbCore.clsMesazh result = new DbCore.clsMesazh();
            //result.Status = true;
            //result.PershkrimMesazhi = clsJSONHelper.Serialize<colSkedaretGIS>(skedaret);
            //return result;
            return skedaret;
        }



        public static clsMesazh merrTeGjitheSkedaretUploadPerObjektGeo(string idDytesore)
        {
            colSkedaretGIS skedaret = new colSkedaretGIS();
            skedaret.mbushSkedarePerObjektGeo(idDytesore);

            DbCore.clsMesazh result = new DbCore.clsMesazh();
            result.Status = true;
            result.PershkrimMesazhi = clsJSONHelper.Serialize<colSkedaretGIS>(skedaret);
            return result;
        }
        public static clsMesazh updateStatusSkedarNgaId(int idSkedari, int idPerdoruesi)
        {
            clsSkedaretGIS skedari = new clsSkedaretGIS();
            skedari.IDSKEDARET = idSkedari;
            skedari.IDSTATUSDOK = 2;
            skedari.DTMODIFIKIMI = DateTime.Now;
            skedari.IDPERDORUESI = idPerdoruesi;

            clsMesazh result = skedari.fshi();
            return result;
        }
        public static clsMesazh updateStatusSkedarFizik(string status, string pathOld, string pathNew, string filename)
        {
            clsMesazh result = new clsMesazh();
            switch (status)
            {
                case "ndrysho":
                    result = ndryshoSkedarFolder(pathOld, pathNew, filename);
                    break;
                case "fshi":
                    result = fshiSkedarNgaFolder(pathOld, filename);
                    break;
            }
            return result;
        }
        public static clsMesazh ndryshoSkedarFolder(string pathOld, string pathNew, string filename)
        {
            clsMesazh result = new clsMesazh();
            string sourceFile = System.IO.Path.Combine(pathOld, filename);
            if (File.Exists(sourceFile))
            {
                string destFile = System.IO.Path.Combine(pathNew, filename);
                System.IO.File.Move(sourceFile, destFile);
                result = new clsMesazh(true, "File u ndryshua me sukses!");
            }
            else
            {
                result = new clsMesazh(false, "Nuk ekziston file!");
            }
            return result;
        }
        public static clsMesazh fshiSkedarNgaFolder(string pathOld, string filename)
        {
            string sourceFile = System.IO.Path.Combine(pathOld, filename);
            if (File.Exists(sourceFile))
            {
                try
                {
                    File.SetAttributes(sourceFile, FileAttributes.Normal);
                    File.Delete(sourceFile);
                    return new clsMesazh(true, "File u fshi me sukses!");
                }
                catch (IOException)
                {
                    return new clsMesazh(false, "File u ndryshua me sukses!");
                }
            }
            else
            {
                return new clsMesazh(true, "Nuk ekziston asnje file!");
            }
        }

        public static string merrPerkthimetPerGisDefault(int gjuha)
        {
            CultureInfo ci = MessagesResource.KtheCultureInfo(gjuha);
            ResourceManager rm = new ResourceManager("Resources.StringsGIS", System.Reflection.Assembly.Load("App_GlobalResources"));

            ResourceSet resourceSet = rm.GetResourceSet(ci, true, true);
            clsPerkthimet perkthimet = new clsPerkthimet();
            foreach (DictionaryEntry entry in resourceSet)
            {
                perkthimet.shtoElementCF(entry.Key.ToString(), entry.Value.ToString());
            }
            return new JavaScriptSerializer().Serialize(perkthimet.ElementetPerkthyer);
        }

        public static string stringGetMimeType(System.Drawing.Image i)
        {
            foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders())
            {
                if (codec.FormatID == i.RawFormat.Guid)
                    return codec.MimeType;
            }

            return "image/unknown";
        }

        public static string uniqid(string prefix, bool more_entropy)
        {
            string r;
            if (string.IsNullOrEmpty(prefix))
                prefix = string.Empty;

            if (!more_entropy)
            {
                r = (prefix + System.Guid.NewGuid().ToString()).Substring(13);
            }
            else
            {
                r = (prefix + System.Guid.NewGuid().ToString() + System.Guid.NewGuid().ToString()).Substring(23);
            }
            return Regex.Replace(r, @"[^a-zA-Z0-9\-]", "");
        }
        #endregion

        #region Transaction
        public static string transaksionLidhjeObjekteshGISWEB(string veprimi, JObject[] colObjekte, int idNdermarrje, int idnderviti, int idPerdorues, HttpSessionState Session)
        {
            clsMesazh mesazhi = new clsMesazh(false, "Unauthorized!");
            JToken idLayer;
            colObjekte[0].TryGetValue("IDLAYER", out idLayer);

            colDisplayLayersGIS displayLayers = DbCore.mySessionObjects.merrDisplayLayersNgaSession(Session);  
            clsDisplayLayersGIS layer = displayLayers.Find(x => x.IDLAYER == Convert.ToInt32(idLayer.ToString()));
            int idNdermVit = (layer.WITHFISCALYEAR ? idnderviti : 0);

            DataTable dtLidhjeObjekteWebGis = krijoDataTableHeadersPerLidhjeObjekteWebGis();
            for (int i = 0; i < colObjekte.Length; i++)
            {
                dtLidhjeObjekteWebGis.LoadDataRow(mbushDtLidhjeObjekteWebGis(dtLidhjeObjekteWebGis, colObjekte[i], idNdermarrje, idNdermVit, idPerdorues).ItemArray, false);
            }

            using (clsDatabaseGIS dbGIS = new clsDatabaseGIS())
            {
                dbGIS.beginTransaksion();

                switch (veprimi)
                {
                    case "INSERT":
                        if (layer.D_SHTIM)
                        {
                            if (colObjekte.Length == 1)
                                mesazhi = dbGIS.ruajLidhjeObjekteshGISWEB(dtLidhjeObjekteWebGis);
                        }
                        break;
                    case "UPDATE":
                        if (layer.D_MOD)
                        {
                            if (colObjekte.Length == 1)
                                mesazhi = dbGIS.ndryshoLidhjeObjekteshGISWEB(dtLidhjeObjekteWebGis);
                            else
                                mesazhi = dbGIS.ndryshoLidhjeObjekteshGISWEBCol(dtLidhjeObjekteWebGis);
                        }
                        break;
                    case "DELETE":
                        if (layer.D_FSH)
                        {
                            if (colObjekte.Length == 1)
                                mesazhi = dbGIS.fshiLidhjeObjekteshGISWEB(dtLidhjeObjekteWebGis);
                        }
                        break;
                }

                if (!mesazhi.Status)
                {
                    dbGIS.rollbackTransaksion();
                }
                else
                {
                    dbGIS.commitTransaksion();
                }
            }
            return mesazhi.PershkrimMesazhi;
        }

        /// <summary>
        /// krijon koken e DataTable ekuivalente te klases qe do te sherbeje per insert/update
        /// </summary>
        /// <returns>kthen koken e DataTable te krijuar</returns>
        private static DataTable krijoDataTableHeadersPerLidhjeObjekteWebGis()
        {
            DataTable dtLidhjeObjekteWebGis = new DataTable("dtLidhjeObjekteWebGis");
            dtLidhjeObjekteWebGis.Columns.Add("gid", (new System.Int32()).GetType());
            dtLidhjeObjekteWebGis.Columns.Add("the_geom", (Type.GetType("System.String")));
            dtLidhjeObjekteWebGis.Columns.Add("IDLAYER", (new System.Int32()).GetType());
            dtLidhjeObjekteWebGis.Columns.Add("IDLAYERSTYPE", (new System.Int32()).GetType());
            dtLidhjeObjekteWebGis.Columns.Add("IDMAGAZINA", (new System.Int32()).GetType());
            dtLidhjeObjekteWebGis.Columns.Add("IDKODIFIKIMI", (new System.Int32()).GetType());
            dtLidhjeObjekteWebGis.Columns.Add("IDARTIKULLI", (new System.Int32()).GetType());
            dtLidhjeObjekteWebGis.Columns.Add("IDSERIALI", (new System.Int32()).GetType());
            dtLidhjeObjekteWebGis.Columns.Add("IDKOKADOK", (new System.Int32()).GetType());
            dtLidhjeObjekteWebGis.Columns.Add("IDTRUPIDOKLIDHES", (new System.Int32()).GetType());
            dtLidhjeObjekteWebGis.Columns.Add("DTMODIFIKIMI", (Type.GetType("System.String")));
            dtLidhjeObjekteWebGis.Columns.Add("IDPERDORUESI", (new System.Int32()).GetType());
            dtLidhjeObjekteWebGis.Columns.Add("IDNDERMARJE", (new System.Int32()).GetType());
            dtLidhjeObjekteWebGis.Columns.Add("KODI", (Type.GetType("System.String")));
            dtLidhjeObjekteWebGis.Columns.Add("PERSHKRIMI", (Type.GetType("System.String")));
            dtLidhjeObjekteWebGis.Columns.Add("SERIALKOD", (Type.GetType("System.String")));
            dtLidhjeObjekteWebGis.Columns.Add("KODKODIFIKIMI", (Type.GetType("System.String")));
            dtLidhjeObjekteWebGis.Columns.Add("NRSTATUSI", (new System.Int32()).GetType());
            dtLidhjeObjekteWebGis.Columns.Add("Veprimi", (Type.GetType("System.String")));
            dtLidhjeObjekteWebGis.Columns.Add("GidPrindi", (new System.Int32()).GetType());
            dtLidhjeObjekteWebGis.Columns.Add("IDNDERMVIT", (new System.Int32()).GetType());
            return dtLidhjeObjekteWebGis;
        }

        /// <summary>
        /// konverton nje objekt te klases ne formatin e nje DataRow qe do te sherbeje per veprimin e Insert/Update ne grup
        /// </summary>
        /// <param name="dataTableHeader">sherben per krijimin e rreshtit nga datable i krijuar per kete rast</param>
        /// <param name="rreshtObjekte">eshte objeti i klases qe do te konvertohet</param>
        /// <returns>kthen rreshtin qe perban klasen te konvertuar ne DataRow</returns>
        /// 
        private static DataRow mbushDtLidhjeObjekteWebGis(DataTable dataTableHeader, JObject rreshtObjekte, int idNdermarrje, int idnderviti, int idPerdorues)
        {
            DataRow rreshti = dataTableHeader.NewRow();
            JToken gid, the_geom, IDLAYER, IDLAYERSTYPE, IDMAGAZINA, IDARTIKULLI, IDSERIALI, IDKOKADOK, IDTRUPIDOKLIDHES, KODI, PERSHKRIMI, SERIALKOD, KODKODIFIKIMI, NRSTATUSI, Veprimi, GidPrindi;
            rreshtObjekte.TryGetValue("gid", out gid); rreshti["gid"] = gid;
            rreshtObjekte.TryGetValue("the_geom", out the_geom); rreshti["the_geom"] = the_geom;
            rreshtObjekte.TryGetValue("IDLAYER", out IDLAYER); rreshti["IDLAYER"] = IDLAYER;
            rreshtObjekte.TryGetValue("IDLAYERSTYPE", out IDLAYERSTYPE); rreshti["IDLAYERSTYPE"] = IDLAYERSTYPE;
            rreshtObjekte.TryGetValue("IDKODIFIKIMI", out IDKOKADOK); rreshti["IDKODIFIKIMI"] = IDKOKADOK;
            rreshtObjekte.TryGetValue("IDMAGAZINA", out IDMAGAZINA); rreshti["IDMAGAZINA"] = IDMAGAZINA;
            rreshtObjekte.TryGetValue("IDARTIKULLI", out IDARTIKULLI); rreshti["IDARTIKULLI"] = IDARTIKULLI;
            rreshtObjekte.TryGetValue("IDSERIALI", out IDSERIALI); rreshti["IDSERIALI"] = IDSERIALI;
            rreshtObjekte.TryGetValue("IDKOKADOK", out IDKOKADOK); rreshti["IDKOKADOK"] = IDKOKADOK;
            rreshtObjekte.TryGetValue("IDTRUPIDOKLIDHES", out IDTRUPIDOKLIDHES); rreshti["IDTRUPIDOKLIDHES"] = IDTRUPIDOKLIDHES;
            rreshti["DTMODIFIKIMI"] = "";
            rreshti["IDPERDORUESI"] = idPerdorues;
            rreshti["IDNDERMARJE"] = idNdermarrje;
            rreshtObjekte.TryGetValue("KODI", out KODI); rreshti["KODI"] = KODI;
            rreshtObjekte.TryGetValue("PERSHKRIMI", out PERSHKRIMI); rreshti["PERSHKRIMI"] = PERSHKRIMI;
            rreshtObjekte.TryGetValue("SERIALKOD", out SERIALKOD); rreshti["SERIALKOD"] = SERIALKOD;
            rreshtObjekte.TryGetValue("KODKODIFIKIMI", out KODKODIFIKIMI); rreshti["KODKODIFIKIMI"] = KODKODIFIKIMI;
            rreshtObjekte.TryGetValue("NRSTATUSI", out NRSTATUSI); rreshti["NRSTATUSI"] = NRSTATUSI;
            rreshtObjekte.TryGetValue("Veprimi", out Veprimi); rreshti["Veprimi"] = Veprimi;
            rreshtObjekte.TryGetValue("gidPrindi", out GidPrindi); rreshti["GidPrindi"] = GidPrindi;
            rreshti["IDNDERMVIT"] = idnderviti;
            return rreshti;
        }
        #endregion

        #region ProxyGEOSERVER

        #region Private
        /// <summary>
        /// Kontrollon kerkesen ne nivel service
        /// </summary>
        /// <param name="mesazhi">Mesazhi me identifikues qe mban edhe gabimin nese ka</param>
        /// <param name="context">Kerkesa</param>
        private static void validateAllowedService(clsMesazh mesazhi, HttpContext context)
        {
            switch (context.Request.QueryString["SERVICE"])
            {
                case "WMS":
                    if (context.Request.QueryString["FORMAT"] == null && !context.Request.QueryString["FORMAT"].Contains("image"))
                        mbushMesazhSipasKerkeses(mesazhi, false, 401, "Unauthorized!");
                    break;
                case "WFS":
                    if (!context.Request.QueryString["TYPENAME"].Contains("GISEXPORTLAYERS"))
                        mbushMesazhSipasKerkeses(mesazhi, false, 401, "Unauthorized!");
                    break;
                case "WCS":
                    break;
                case "WPS":
                default:
                    break;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mesazhi"></param>
        /// <param name="context"></param>
        private static void validateAllowedRequest(clsMesazh mesazhi, HttpContext context, clsWorkspaceGIS workspaceAktual, string kerkesa)
        {
            switch (context.Request.QueryString["REQUEST"])
            {
                case "GetLegendGraphic":
                    validateAllowedReqGetLegendGraphic(mesazhi, context, workspaceAktual, kerkesa);
                    break;
                case "GetMap":
                    validateAllowedReqGetMap(mesazhi, context, workspaceAktual, kerkesa);
                    break;
                case "GetFeature":
                    validateAllowedReqGetFeature(mesazhi, context, workspaceAktual, kerkesa);
                    break;
                case "GetFeatureInfo":
                    validateAllowedReqGetFeatureInfo(mesazhi, context, workspaceAktual, kerkesa);
                    break;
                default:
                    break;
            }
        }

        private static void ktheNewUrlProxyGeoserver(clsMesazh mesazhi, string connectUrl, string kerkesa, int idlayerType)
        {
            if (connectUrl != "")
            {
                mesazhi.KodMesazhi = 0;
                mesazhi.PershkrimMesazhi = connectUrl + kerkesa.Split('?')[1];
            }
            else
                mesazhi.KodMesazhi = idlayerType;
        }

        private static void validateAllowedReqGetLegendGraphic(clsMesazh mesazhi, HttpContext context, clsWorkspaceGIS workspaceAktual, string kerkesa)
        {
            colDisplayLayersGIS displayLayers = DbCore.mySessionObjects.merrDisplayLayersNgaSession(context.Session);
            clsDisplayLayersGIS layerRequested = new clsDisplayLayersGIS();

            layerRequested = displayLayers.Find(x => x.IDENTIFICATION == context.Request.QueryString["LAYER"]);
            if (layerRequested.D_AMB || layerRequested.DISPLAYONMAP)
                validateIsBeetweenAllowedScale(mesazhi, layerRequested, context.Request.QueryString["SCALE"]);
            else
                mbushMesazhSipasKerkeses(mesazhi, false, 401, "Unauthorized!");

            ktheNewUrlProxyGeoserver(mesazhi, layerRequested.CONNECTURL, kerkesa, layerRequested.IDLAYERSTYPE);
        }

        private static void validateAllowedReqGetMap(clsMesazh mesazhi, HttpContext context, clsWorkspaceGIS workspaceAktual, string kerkesa)
        {
            colDisplayLayersGIS displayLayers = DbCore.mySessionObjects.merrDisplayLayersNgaSession(context.Session);
            clsDisplayLayersGIS layerRequested = new clsDisplayLayersGIS();

            if (!(context.Request.QueryString["format_options"] != null && context.Request.QueryString["format_options"].Contains("dpi")))
            {
                string layIdentification = context.Request.QueryString["url"].Remove(0, 11);
                layerRequested = displayLayers.Find(x => x.IDENTIFICATION == layIdentification);
                if (!(layerRequested.D_AMB || layerRequested.DISPLAYONMAP))
                    mbushMesazhSipasKerkeses(mesazhi, false, 401, "Unauthorized!");

                ktheNewUrlProxyGeoserver(mesazhi, layerRequested.CONNECTURL, kerkesa, layerRequested.IDLAYERSTYPE);
            }
            else
            {
                ktheNewUrlProxyGeoserver(mesazhi, workspaceAktual.GEOURL + kerkesa, kerkesa, layerRequested.IDLAYERSTYPE);
            }
        }

        private static void validateAllowedReqGetFeature(clsMesazh mesazhi, HttpContext context, clsWorkspaceGIS workspaceAktual, string kerkesa)
        {
            colDisplayLayersGIS displayLayers = DbCore.mySessionObjects.merrDisplayLayersNgaSession(context.Session);
            clsDisplayLayersGIS layerRequested = new clsDisplayLayersGIS();

            string layExport = context.Request.QueryString["url"].Remove(0, 10);
            layerRequested = displayLayers.Find(x => x.IDENTIFICATION == layExport);
            if (!layerRequested.D_EKSPORTO)
                mbushMesazhSipasKerkeses(mesazhi, false, 401, "Unauthorized!");

            ktheNewUrlProxyGeoserver(mesazhi, layerRequested.CONNECTURL, kerkesa, 2);
        }

        private static void validateAllowedReqGetFeatureInfo(clsMesazh mesazhi, HttpContext context, clsWorkspaceGIS workspaceAktual, string kerkesa)
        {
            colDisplayLayersGIS displayLayers = DbCore.mySessionObjects.merrDisplayLayersNgaSession(context.Session);
            clsDisplayLayersGIS layerRequested = new clsDisplayLayersGIS();

            layerRequested = displayLayers.Find(x => x.IDENTIFICATION == Regex.Split(context.Request.QueryString["QUERY_LAYERS"], ",")[0]);                    
            if (layerRequested.D_AMB || layerRequested.DISPLAYONMAP)
                validateIsBeetweenAllowedScale(mesazhi, layerRequested, context.Request.QueryString["SCALE"]);
            else
                mbushMesazhSipasKerkeses(mesazhi, false, 401, "Unauthorized!");

            var connectUrl = layerRequested.CONNECTURL;
            if (connectUrl == "")
                connectUrl = workspaceAktual.GEOURL + "wms?";

            ktheNewUrlProxyGeoserver(mesazhi, connectUrl, kerkesa, 0);
        }


        /// <param name="mesazhi"></param>
        /// <param name="status"></param>
        /// <param name="code"></param>
        /// <param name="description"></param>
        private static void mbushMesazhSipasKerkeses(clsMesazh mesazhi, bool status, int code, string description)
        {
            mesazhi.Status = false;
            mesazhi.KodMesazhi = 401;
            mesazhi.PershkrimMesazhi = "Unauthorized!";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mesazhi"></param>
        /// <param name="layerRequested"></param>
        /// <param name="context"></param>
        private static void validateIsBeetweenAllowedScale(clsMesazh mesazhi, clsDisplayLayersGIS layerRequested, string currentScaleReq)
        {
            //context.Request.QueryString["SCALE"]
            if (currentScaleReq != null)
            {
                float currentScale = float.Parse(currentScaleReq, CultureInfo.InvariantCulture.NumberFormat);

                Int32 minScale = layerRequested.MINSCALE;
                Int32 maxScale = layerRequested.MAXSCALE;
                if (!(currentScale >= minScale && currentScale <= maxScale))
                    mbushMesazhSipasKerkeses(mesazhi, false, 204, "Jashte intervalit te shkalles se percaktuar!");
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private static clsMesazh personalizeRequestForGeoserver(HttpContext context)
        {
            clsMesazh mesazhi = new clsMesazh(200, true, "");
            switch (context.Request.QueryString["REQUEST"])
            {
                case "GetLegendGraphic":
                    mesazhi.PershkrimMesazhi = "&WIDTH=17&HEIGHT=17";
                    break;
                default:
                    break;
            }
            return mesazhi;
        }

        #endregion

        /// <summary>
        /// Pergatit kerkesen e bere nga client-side duke bere fil
        /// </summary>
        /// <param name="context"></param>
        /// <param name="workspaceAktual"></param>
        /// <param name="kerkesa"></param>
        /// <returns></returns>
        public static clsMesazh prepareGeoserverRequest(HttpContext context, clsWorkspaceGIS workspaceAktual, string kerkesa)
        {
            clsMesazh mesazhi = new clsMesazh(200, true, "");

            validateAllowedService(mesazhi, context);

            if (mesazhi.Status)
                validateAllowedRequest(mesazhi, context, workspaceAktual, kerkesa);

            if (mesazhi.Status)
                ktheGeoserverUrlRedirectSipasKerkeses(mesazhi, context, workspaceAktual, kerkesa, mesazhi.KodMesazhi);

            return mesazhi;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mesazhi"></param>
        /// <param name="context"></param>
        /// <param name="workspaceAktual"></param>
        /// <param name="kerkesa"></param>
        public static void ktheGeoserverUrlRedirectSipasKerkeses(clsMesazh mesazhi, HttpContext context, clsWorkspaceGIS workspaceAktual, string kerkesa, int wsGrupimeStruct)
        {
            if (kerkesa.Contains("create.json") || kerkesa.Contains("pdf.printout"))
                mesazhi.PershkrimMesazhi = kerkesa;
            else
            {
                switch (kerkesa)
                {
                    case "printCapabilitiesInfo":
                        mesazhi.PershkrimMesazhi = workspaceAktual.GEOURL + "pdf/info.json?var=printCapabilities";
                        break;
                    case "wps":
                        mesazhi.PershkrimMesazhi = workspaceAktual.GEOURL + "wps?";
                        break;
                    default:
                        switch (wsGrupimeStruct)
                        {
                            case 0:
                                mesazhi.PershkrimMesazhi = mesazhi.PershkrimMesazhi;
                                break;
                            case 1:
                                mesazhi.PershkrimMesazhi = workspaceAktual.GEOURL + workspaceAktual.WSDEFAULT + "/" + kerkesa;
                                break;
                            case 2:
                                mesazhi.PershkrimMesazhi = workspaceAktual.GEOURL + workspaceAktual.WSFUNCTIONS + "/" + kerkesa;
                                break;
                            case 3:
                                mesazhi.PershkrimMesazhi = workspaceAktual.GEOURL + workspaceAktual.WSGUIDING + "/" + kerkesa;
                                break;
                            default:
                                mesazhi.PershkrimMesazhi = workspaceAktual.GEOURL + workspaceAktual.Workspace_name + "/" + kerkesa;
                                break;
                        }
                        break;
                }
                mesazhi.PershkrimMesazhi += ktheGeoserverUrlRedirectMeparametra(context);
                mesazhi.PershkrimMesazhi += personalizeRequestForGeoserver(context).PershkrimMesazhi;
            }
        }

        /// <summary>
        /// Kthen formatin e plote te kerkeses qe behet ne geoserver
        /// </summary>
        /// <param name="context">HttpContext</param>
        /// <returns>url ne string</returns>
        public static string ktheGeoserverUrlRedirectMeparametra(HttpContext context)
        {
            string strMeParametra = "";

            StringBuilder sb = new StringBuilder();
            foreach (string s in context.Request.QueryString)
                if (!(s == null || String.Equals(s, "url") || String.Equals(s, "WS")))
                    sb.AppendFormat("&{0}={1}", s, context.Request.QueryString[s]);
            if (sb.Length > 0)
                strMeParametra = String.Format("&{0}", sb.Remove(0, 1));

            return strMeParametra;
        }

        /// <summary>
        /// Pergatitet kerkesa e bere nga Client-Side per geoserver
        /// </summary>
        /// <param name="context">HttpContext nga kerkesa</param>
        /// <param name="strRedirect">Linku ku do te behet kerkesa ne geoserver</param>
        /// <param name="geoUser">Te dhena per autentifikimin ne geoserver username</param>
        /// <param name="geoPassword">Te dhena per autentifikimin ne geoserver password</param>
        /// <param name="requestTimeOut">Time Out i pritjes se pergjigjes nga Geoserver</param>
        /// <returns></returns>
        public static HttpWebRequest krijoGeoserverHttpWebRequest(HttpContext context, string strRedirect, string geoUser, string geoPassword, int requestTimeOut)
        {
            HttpWebRequest geoRequest = (HttpWebRequest)WebRequest.Create(strRedirect);

            geoRequest.AllowAutoRedirect = false;
            geoRequest.Method = context.Request.HttpMethod;
            geoRequest.ContentType = context.Request.ContentType;
            geoRequest.UserAgent = context.Request.UserAgent;

            geoRequest.Timeout = requestTimeOut;

            geoRequest.Credentials = new NetworkCredential();
            geoRequest.PreAuthenticate = true;

            geoRequest.Headers["Remote-User"] = context.User.Identity.Name;
            foreach (string s in context.Request.Headers)
                if (!WebHeaderCollection.IsRestricted(s) && !String.Equals(s, "Remote-User"))
                    geoRequest.Headers.Add(s, context.Request.Headers[s]);

            if (context.Request.HttpMethod == "POST")
            {
                Stream outputStream = geoRequest.GetRequestStream();
                CopyStream(context.Request.InputStream, outputStream);
                outputStream.Close();
            }

            return geoRequest;
        }

        /// <summary>
        /// Dergohet kerkesa sipas rastit
        /// </summary>
        /// <param name="context">Sherben te modifikimin e header te response ne Client-Side</param>
        /// <param name="actualRequest">HttpWebRequest i konfiguruar per rastin ne fjale</param>
        /// <returns>Nese kerkesa u krye me sukese ose jo</returns>
        public static void dergoKerkesenMeHttpWebRequest(HttpContext context, HttpWebRequest actualRequest)
        {
            clsMesazh mesazhi = new clsMesazh(true, "Kerkese e pasakte nga Client-Side!");
            HttpWebResponse geoResponse;
            try
            {
                geoResponse = (HttpWebResponse)actualRequest.GetResponse();

                context.Response.StatusCode = (int)geoResponse.StatusCode;
                context.Response.StatusDescription = geoResponse.StatusDescription;
                context.Response.ContentType = geoResponse.ContentType;

                if (geoResponse.Headers.Get("Location") != null)
                {
                    string urlSuffix = geoResponse.Headers.Get("Location");
                    if (urlSuffix.ToLower().StartsWith(ConfigurationManager.AppSettings["ProxyUrl"].ToLower()))
                        urlSuffix = urlSuffix.Substring(ConfigurationManager.AppSettings["ProxyUrl"].Length);
                    context.Response.AddHeader("Location", context.Request.Url.GetLeftPart(UriPartial.Authority) + urlSuffix);
                }

                foreach (string s in geoResponse.Headers)
                    if (!WebHeaderCollection.IsRestricted(s) && !String.Equals(s, "Location"))
                        context.Response.AddHeader(s, geoResponse.Headers[s]);
                CopyStream(geoResponse.GetResponseStream(), context.Response.OutputStream);

            }
            catch (WebException we)
            {
                geoResponse = (HttpWebResponse)we.Response;
                if (geoResponse == null)
                {
                    context.Response.StatusCode = 13;
                    context.Response.Write("Nuk mund të kapet geoserver back-end");
                }
                mesazhi.Status = false;
            }

            geoResponse.Close();
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[1024];
            int bytes;
            while ((bytes = input.Read(buffer, 0, 1024)) > 0)
                output.Write(buffer, 0, bytes);
        }


        /// <summary>
        /// I dergohet Kerkesa geoserver ne menyre qe te ndryshohet skema: shtohet workspace i ri, behet reload te dhenat etj.
        /// </summary>
        /// <param name="requestUrl">Url qe do te beje drejtimin e kerkeses</param>
        /// <param name="requestContentType">Content-Type i kerkeses</param>
        /// <param name="requestMethod">Metoda qe do te perdoret</param>
        /// <param name="requestGeoUser">Useri i Geoserver per autentifikim duhet patjeter administrator ne geoserver</param>
        /// <param name="requestGeoPass">Pass i Geoserver per autentifikim duhet patjeter administrator ne geoserver</param>
        /// <param name="requestInformation">Informacioni me kerkesen sipas rastit</param>
		public static clsMesazh geoserverRequestToChangeSchema(string requestUrl, string requestContentType, string requestMethod, string requestGeoUser, string requestGeoPass, string requestInformation)
        {
            clsMesazh mesazhi = new clsMesazh(false, "Ndodhi nje gabim ne komunikimin e krijuar me Geoserver!");

            string url = requestUrl;
            WebRequest request = WebRequest.Create(url);

            request.ContentType = requestContentType;
            request.Method = requestMethod;
            request.Credentials = new NetworkCredential(requestGeoUser, requestGeoPass);

            byte[] buffer = Encoding.GetEncoding("UTF-8").GetBytes(requestInformation);
            Stream reqstr = request.GetRequestStream();
            reqstr.Write(buffer, 0, buffer.Length);
            reqstr.Close();

            WebResponse response = request.GetResponse();

            if (response != null)
            {
                mesazhi.Status = true;
                mesazhi.PershkrimMesazhi = "Kerkesa kaloi me sukses permes Geoserver!";
            }

            return mesazhi;
        }
       
		public static clsMesazh geoserverRequestForRestApi(string requestUrl, string requestContentType, string requestMethod, string requestGeoUser, string requestGeoPass, string requestInformation, string requestAccept)
        {
            clsMesazh mesazhi = new clsMesazh(false, "Ndodhi nje gabim ne komunikimin e krijuar me Geoserver!");

            HttpWebRequest geoRequest = (HttpWebRequest)WebRequest.Create(requestUrl);

            geoRequest.AllowAutoRedirect = false;

            if (requestMethod != "")        geoRequest.Method = requestMethod;
            if (requestContentType != "")   geoRequest.ContentType = requestContentType;
            if (requestAccept != "")        geoRequest.Accept = requestAccept;

            if (requestGeoUser != "" && requestGeoPass != "")
            {
                geoRequest.Credentials = new NetworkCredential(requestGeoUser, requestGeoPass);
                geoRequest.PreAuthenticate = true;
            }

            if (requestInformation != "")
            {
                byte[] buffer = Encoding.GetEncoding("UTF-8").GetBytes(requestInformation);
                Stream reqstr = geoRequest.GetRequestStream();
                reqstr.Write(buffer, 0, buffer.Length);
                reqstr.Close();
            }

            HttpWebResponse geoResponse;
            try
            {
                geoResponse = (HttpWebResponse)geoRequest.GetResponse();

                mesazhi.Status = true;
                using (var sr = new StreamReader(geoResponse.GetResponseStream()))
                {
                    mesazhi.PershkrimMesazhi = sr.ReadToEnd();
                }
            }
            catch (WebException we)
            {
                NLog.LogManager.GetCurrentClassLogger().Error(we.Message);
            }

            return mesazhi;
        }

        #endregion

        #region ProxyPUBLIC

        /// <summary>
        /// Pergatitet response per Client-Side kur kerkesa nuk ishte e sakte
        /// </summary>
        /// <param name="context">HttpContext nga kerkesa</param>
        /// <param name="stCode">Kodi i gabimit</param>
        /// <param name="stDescription">Mesazhi i gabimit</param>
        /// <param name="contentType">Type i response ne client-side</param>
        /// <returns></returns>
        public static void dergoPergjigjeTePasakteNeClientSide(HttpContext context, int stCode, string stDescription, string contentType)
        {
            context.Response.StatusCode = stCode;
            context.Response.StatusDescription = stDescription;
            context.Response.ContentType = contentType;
        }
        #endregion

        #region ElemeteGIS
        /// <summary>
        /// Merr konfigigurimin e ambjentit sipas objektit GIS
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        public static int ktheIdKonfigurimiNgaGisElement(int gid)
        {
            using (clsDatabaseGIS data = new clsDatabaseGIS())
            {
                return data.ktheIdKonfigurimiNgaGisElement(gid);
            }
        }


        public static object getRowsForEditWindow(int layerType, int statusi, int idNdermarrje, int idViti, int idPerdorues, int idGjuha, ResourceManager rm, CultureInfo ci)
        {
            colLayersColsGIS tempLC = new colLayersColsGIS();
            tempLC.merrTeGjitheKolonatSipasLayerTypeDheStatusi(layerType, statusi, idGjuha, idNdermarrje);

            clsDatabaseGIS dbGis = new clsDatabaseGIS();
            DataTable dt = dbGis.merrListeStatuseshSipasTeDrejtaLayeri(layerType, idGjuha, idPerdorues, idNdermarrje, idViti);
            dbGis.Dispose();

            System.Web.Script.Serialization.JavaScriptSerializer serializues = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    if (col.ColumnName == "PERSHKRIMI")
                        row.Add(col.ColumnName, rm.GetString(dr[col].ToString(), ci));
                    else
                        row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            string rezultati = serializues.Serialize(rows);
            return new { tempLC = tempLC, statuset = rezultati };
        }
        #endregion
    }
}