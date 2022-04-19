using DbCore;
using DbCore.DbGIS;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using NLog;
using System.Web;
using System.Web.Caching;

namespace PlatinumWeb
{
    /// <summary>
    /// Ndertuar per tu perdorur per kerkesa te ndryshime publike qe behen nga GIS ne forme URL dhe kthimin dinamikisht te permbajtjes
    /// </summary>
    public class GISPublicProxy : IHttpHandler
    {
        private static Logger logu = LogManager.GetCurrentClassLogger();

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string queryStringUrl = context.Request.QueryString["url"];
                switch (queryStringUrl)
                {
                    case "geoserverRequestForPrint":
                        geoserverRequestForPrint(context);
                        break;
                    case "geoserverPrintCapabilities":
                        geoserverPrintCapabilities(context);
                        break;
                    case "geoserverRequestForPublic":
                        geoserverRequestForPublic(context);
                        break;
                    default:
                        clsFunksioneGIS.dergoPergjigjeTePasakteNeClientSide(context, 500, "Kerkese e pa menaxhuar!", "text/plain");
                        break;
                }                
            }
            catch (Exception ex)
            {
                clsFunksioneGIS.dergoPergjigjeTePasakteNeClientSide(context, 500, ex.Message, "text/plain");
                logu.Error(": GEOSERVER PUBLIC - " + context.Request.QueryString.ToString() + " generate error " + ex.Message  );
            }
            finally
            {
                context.ApplicationInstance.CompleteRequest();
            }
        }

        #region Requests to Print Orthophoto 
        /// <summary>
        /// Pergatitet kerkesa e bere nga Geosever per Server Tiles, ne printim
        /// </summary>
        /// <param name="context">HttpContext nga kerkesa</param>
        /// <returns></returns>
        private static void geoserverRequestForPrint(HttpContext context)
        {
            var webClient = new WebClient();

            string strRedirect = context.Request.QueryString["proxyHost"] + context.Request.PathInfo;
            byte[] imageBytes = webClient.DownloadData(strRedirect);

            context.Response.ContentType = webClient.ResponseHeaders["content-type"];
            context.Response.OutputStream.Write(imageBytes, 0, imageBytes.Length);
        }
        /// <summary>
        /// Pergatitet kerkesa e bere ne Geosever per printim
        /// </summary>
        /// <param name="context">HttpContext nga kerkesa</param>
        /// <returns></returns>
        private static void geoserverPrintCapabilities(HttpContext context)
        {
            string kerkesa = context.Request.QueryString["printUrl"];
            if (kerkesa.Contains("create.json") || kerkesa.Contains("pdf.printout"))
            {
                HttpWebRequest geoRequest = clsFunksioneGIS.krijoGeoserverHttpWebRequest(context, kerkesa, "admin", "geoadmin", 60000);
                clsFunksioneGIS.dergoKerkesenMeHttpWebRequest(context, geoRequest);
            }
            else
                clsFunksioneGIS.dergoPergjigjeTePasakteNeClientSide(context, 500, "Kerkese e pa menaxhuar!", "text/plain");
        }
        #endregion

        #region Requests to publish Layers to ArcGIS/QGis ect
        /// <summary>
        /// Pergatitet kerkesa e bere qe do ti dergohet geoserver per workspace publike
        /// </summary>
        /// <param name="context">HttpContext nga kerkesa</param>
        /// <returns></returns>
        private static void geoserverRequestForPublic(HttpContext context)
        {
            logu.Info(": GEOSERVER PUBLIC - QueryStringRequest = " + context.Request.QueryString.ToString());
            clsWorkspaceGIS workspaceAktual = GetSetNeWebCache("GIS_GEOSERVER_PUBLIC", context.Request.QueryString["WS"]);

            if (workspaceAktual != null)
            {
                context.Request.ContentType = ManipulateContentTypeRequest(context.Request.QueryString["REQUEST"]);

                string urlRequest = workspaceAktual.GEOURL + workspaceAktual.WSPUBLIC + "/wms?" + clsFunksioneGIS.ktheGeoserverUrlRedirectMeparametra(context);
                HttpWebRequest geoRequest = clsFunksioneGIS.krijoGeoserverHttpWebRequest(context, urlRequest, workspaceAktual.GEOUSER, workspaceAktual.GEOPASSWORD, 600);
                clsFunksioneGIS.dergoKerkesenMeHttpWebRequest(context, geoRequest);
            }
            else
            {
                logu.Warn(": GEOSERVER PUBLIC - Not Allowed '" + context.Request.QueryString["WS"] + "'");
                clsFunksioneGIS.dergoPergjigjeTePasakteNeClientSide(context, 500, "Kerkese e pa menaxhuar!", "text/plain");
            }
        }

        /// <summary>
        /// TODO Denisa, hiqe nga ketu dhe ndrysho menyren me metoden e re te Getson nga "MyAppCache" ne versionin e radhes nga develop
        /// </summary>
        /// <param name="key"></param>
        /// <param name="wsFilterName"></param>
        /// <returns></returns>
        private static clsWorkspaceGIS GetSetNeWebCache(string key, string wsFilterName)
        {
            clsWorkspaceGIS workspaceAktual = new clsWorkspaceGIS();
            colWorkspaceGIS workspaces = HttpRuntime.Cache[key] as colWorkspaceGIS;
            if (workspaces == null)
            {
                workspaces = new colWorkspaceGIS(true);
                HttpRuntime.Cache.Insert(key, workspaces, null, DateTime.Now.AddMinutes(60),Cache.NoSlidingExpiration);
            }
            workspaceAktual = workspaces.Find(x => x.WSPUBLIC == wsFilterName);
            return workspaceAktual;
        }

        /// <summary>
        /// Lexon llojin e kerkeses dhe manipulon type te content type pasi nga programe te caktuara vjen bopsh
        /// </summary>
        /// <param name="request">lloji i kerkeses qe i behet geoserver</param>
        /// <returns></returns>
        private static string ManipulateContentTypeRequest(string request)
        {
            string ContentType = "";

            switch (request)
            {
                case "GetMap":
                    break;
                case "GetCapabilities":
                    ContentType = "text/xml";
                    break;
                case "GetFeatureInfo":
                    ContentType = "application/json; charset=UTF-8'";
                    break;
                default:
                    ContentType = "image/png";
                    break;
            }

            return ContentType;
        }
        #endregion

        public bool IsReusable
        {
            get { return false; }
        }
    }
}