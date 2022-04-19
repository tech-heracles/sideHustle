using System.Data;
using System.Globalization;
using System.Web.SessionState;
using System.Collections.Generic;

using Newtonsoft.Json.Linq;

using DbCore;
using DbCore.DbGIS;
using System;
using System.Resources;
using DbCore.IMBUtils.Messages;

namespace RestApi.WebAPI.Models
{
    public class GISRepository
    {
        internal static string transaksionLidhjeObjekteshGISWEB(string veprimi, JObject[] colObjekte, HttpSessionState Session)
        {
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idnderviti = mySessionObjects.ktheNdermarrjeVit(Session);
            int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
            return clsFunksioneGIS.transaksionLidhjeObjekteshGISWEB(veprimi, colObjekte, idNdermarrje, idnderviti, idPerdorues, Session);
        }

        internal static object getRowsForEditWindow(HttpSessionState Session, int layerType, int statusi)
        {
            CultureInfo ci = mySessionObjects.ktheCultureInfo(Session);
            ResourceManager rm = new ResourceManager("Resources.Strings", System.Reflection.Assembly.Load("App_GlobalResources"));

            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idViti = mySessionObjects.ktheIdVitNdermarrje(Session);
            int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
            int idGjuha = mySessionObjects.ktheGjuhe(Session);

            return clsFunksioneGIS.getRowsForEditWindow(layerType, statusi, idNdermarrje, idViti, idPerdorues, idGjuha, rm, ci);
        }

        internal static clsMesazh DergoEmailNgaGIS(HttpSessionState Session, string toEmail, string fromName, string fromEmail, string subject, string bodyMesazh)
        {
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
            int idGjuha = mySessionObjects.ktheGjuhe(Session);
            CultureInfo ci = MessagesResource.KtheCultureInfo(idGjuha);

            return EmailComposer.DergoEmailNgaGIS(ci, idNdermarrje, idPerdorues, toEmail.Split(';'), fromName, fromEmail, subject, bodyMesazh);
        }

        internal static DataTable merrKordinataPerZgjedhjeQV2017(string filter)
        {
            return clsGeoserverGIS.merrKordinataPerZgjedhjeQV2017(filter);
        }

        #region Menu
        internal static List<Dictionary<string, object>> getAllObjectsForQuickSearch(HttpSessionState Session)
        {
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idnderviti = mySessionObjects.ktheNdermarrjeVit(Session);
            int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
            int idGjuha = mySessionObjects.ktheGjuhe(Session);

            return clsFunksioneGIS.getAllObjectsForQuickSearch(idGjuha, idNdermarrje, idnderviti, idPerdorues);
        }

        internal static object getObjectForQuickSearch(HttpSessionState Session, string gid)
        {
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idnderviti = mySessionObjects.ktheNdermarrjeVit(Session);
            int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
            int idGjuha = mySessionObjects.ktheGjuhe(Session);

            return clsFunksioneGIS.getObjectForQuickSearch(idGjuha, idNdermarrje, idnderviti, idPerdorues, gid);
        }
        
        internal static object GetFeatureInfo(HttpSessionState Session, string gid, string layerName, int indeksi)
        {
            int idNdermarrje = mySessionObjects.merrIdNdermarrjeSesioni(Session);
            int idnderviti = mySessionObjects.ktheNdermarrjeVit(Session);
            int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
            int idGjuha = mySessionObjects.ktheGjuhe(Session);

            colDisplayLayersGIS displayLayerCol = mySessionObjects.merrDisplayLayersNgaSession(Session);
            colLayersTypeGIS layersTypeGIS = mySessionObjects.merrLayersTypeNgaSession(Session);

            return clsFunksioneGIS.GetFeatureInfo(gid, layerName, indeksi, idNdermarrje, idnderviti, idPerdorues, idGjuha, displayLayerCol, layersTypeGIS);           
        }

        #endregion

        #region Skedare
        internal static object merrTeGjitheSkedaretUpload(HttpSessionState Session, string tipiSkedar)
        {
            int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
            return clsFunksioneGIS.merrTeGjitheSkedaretUploadSipasLlojit(tipiSkedar, idPerdorues);
        }

        internal static object merrTeGjitheSkedaretUploadPerObjektGeo(HttpSessionState session, string idDytesoreGeo)
        {
            return clsFunksioneGIS.merrTeGjitheSkedaretUploadPerObjektGeo(idDytesoreGeo).PershkrimMesazhi;
        }

        internal static object fshiSkedareUpload(HttpSessionState Session, string serverMapPath, int idSkedari, string filename, string lloji)
        {
            int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
            clsMesazh result = clsFunksioneGIS.updateStatusSkedarNgaId(idSkedari, idPerdorues);
            if (result.Status)
            {
                string pathOld = "", pathNew = "", status = "";
                switch (lloji)
                {
                    case "gpx":
                        status = "ndrysho";
                        pathOld = serverMapPath + "gpxFiles";
                        pathNew = serverMapPath + "fshiFiles";
                        break;
                    case "shp":
                        status = "fshi";
                        pathOld = serverMapPath + "shapeFiles";
                        pathNew = pathOld;
                        break;
                    case "dokGeo":
                        status = "ndrysho";
                        pathOld = serverMapPath + "dokGeo";
                        pathNew = serverMapPath + "dokGeo\\fshiFiles";
                        break;
                }

                result = clsFunksioneGIS.updateStatusSkedarFizik(status, pathOld, pathNew, filename);
            }
            return result.PershkrimMesazhi;
        }

        #endregion
    }
}