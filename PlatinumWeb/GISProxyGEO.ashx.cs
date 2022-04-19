using DbCore;
using DbCore.DbGIS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace PlatinumWeb
{
    /// <summary>
    /// Summary description for proxy
    /// </summary>
    public class GISProxyGEO : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    {
        public void ProcessRequest(HttpContext context)
        {
            try
            { 
                string kerkesa = context.Request.QueryString["url"];
                if (!String.IsNullOrEmpty(kerkesa))
                {
                    switch (kerkesa)
                    {
                        case "merrLogoNderrmarrje":
                            context.Response.ContentType = DbCore.mySessionObjects.merrMimeTypeLogoNdermarrjeNgaSession(context.Session);
                            context.Response.BinaryWrite(DbCore.mySessionObjects.merrNdermarrjePuneNgaSession(context.Session).NdermarrjeLogo);
                            break;
                        case "modifikoSkemenGeoserver":
                            int idndermarrje;
                            Int32.TryParse(context.Request.QueryString["idndermarrje"], out idndermarrje);
                            clsWorkspaceGIS workspace = new clsWorkspaceGIS();

                            string llojKerkese = context.Request.QueryString["llojKerkese"];
                            switch (llojKerkese)
                            {
                                case "createNewLayer":
                                    int idFolder, idLayerType, nrStatusi, meAutorizim;
                                    Int32.TryParse(context.Request.QueryString["idFolder"], out idFolder);
                                    Int32.TryParse(context.Request.QueryString["idLayerType"], out idLayerType);
                                    Int32.TryParse(context.Request.QueryString["nrStatusi"], out nrStatusi);
                                    Int32.TryParse(context.Request.QueryString["meAutorizim"], out meAutorizim);
                                    clsMesazh uKrijuaFeatures = clsGeoserverGIS.CreateLayer(idndermarrje, context.Request.QueryString["LayerName"], context.Request.QueryString["Projection"], context.Request.QueryString["Type"], idFolder, idLayerType, nrStatusi, meAutorizim);
                                    break;
                                case "changeLayerDefaultStyle":
                                    workspace.mbushWorkspaceSipasNdermarrjesId(idndermarrje);
                                    clsMesazh uNdryshuaDefaultStyle = clsGeoserverGIS.ChangeLayerDefaultStyle(workspace.Workspace_name, workspace.GEOURL, workspace.GEOUSER, workspace.GEOPASSWORD, context.Request.QueryString["featuresName"], context.Request.QueryString["styleName"]);
                                    break;
                                case "reloadGeoserverStatus":
                                default:
                                    workspace.mbushWorkspaceSipasNdermarrjesId(-1);
                                    clsMesazh uBeReloadGeoserver = clsGeoserverGIS.ReloadGeoserverStatus(workspace.GEOURL, workspace.GEOUSER, workspace.GEOPASSWORD);
                                    break;
                            }
                            break;
                        default:
                            clsMesazh mesazhi = new clsMesazh(200, true, "");
                            clsWorkspaceGIS workspaceAktual = DbCore.mySessionObjects.merrWorkspaceNgaSession(context.Session);
                            mesazhi = clsFunksioneGIS.prepareGeoserverRequest(context, workspaceAktual, kerkesa);

                            if (!mesazhi.Status)
                                completeRequestByUser(context, mesazhi.KodMesazhi, mesazhi.PershkrimMesazhi, "text/plain");
                            else
                            {
                                string requestTimeOut = DbCore.mySessionObjects.MerrNgaSession<string>(context.Session, "ServerConfiguration_GIS_GEOSERVER_REQTIMEOUT");
                                HttpWebRequest geoRequest = clsFunksioneGIS.krijoGeoserverHttpWebRequest(context, mesazhi.PershkrimMesazhi, (workspaceAktual == null ? "admin" : workspaceAktual.GEOUSER), (workspaceAktual == null ? "geoadmin" : workspaceAktual.GEOPASSWORD), Convert.ToInt32(requestTimeOut));
                                clsFunksioneGIS.dergoKerkesenMeHttpWebRequest(context, geoRequest);
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                completeRequestByUser(context, 500, ex.Message, "text/plain");
            }
            finally
            {
                context.ApplicationInstance.CompleteRequest();
            }
        }

        private static void completeRequestByUser(HttpContext context, int code, string description, string type)
        {
            context.Response.StatusCode = code;
            context.Response.StatusDescription = description;
            context.Response.ContentType = type;
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}