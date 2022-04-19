using System;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using DbCore;
using DbCore.DbGIS;
using Newtonsoft.Json.Linq;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;

namespace RestApi.WebAPI.Controllers
{
    public class GISController : ApiController, IRequiresSessionState
    {
        private HttpSessionState Session => HttpContext.Current.Session;

        #region Edit
        [HttpPost, HttpGet]
        public HttpResponseMessage transaksionLidhjeObjekteshGISWEB(JObject param)
        {
            try
            {
                string veprimi = param["veprimi"].Value<string>();
                JObject[] colObjekte = param["colObjekte"].ToObject<JObject[]>();

                return Request.KthePergjigje(GISRepository.transaksionLidhjeObjekteshGISWEB(veprimi, colObjekte, Session));

            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage MerrTeDhenatGeoEditimSelect(JObject param)
        {
            try
            {
                string layer = param["layer"].Value<string>();
                string filterGid = param["filterGid"].Value<string>();
                string geometry = param["geometry"].Value<string>();
                string bbox = param["bbox"].Value<string>();
                int idObjFillestar = int.Parse(param["idObjFillestar"].Value<string>());
                int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);

                return Request.KthePergjigjeJsonMeGeoSerializer(clsFunksioneGIS.merrTeDhenatGeofc(Session, layer, bbox, filterGid, geometry, clsFunksioneGIS.LlojKonvertimiTrupFeatures.AllColumns, idPerdorues, idObjFillestar));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage MerrTeDhenatGeoEditimSnap(JObject param)
        {
            try
            {
                string layers = param["layers"].Value<string>();
                string excludeGid = param["excludeGid"].Value<string>();
                string bbox = param["bbox"].Value<string>();
                int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);
                int idnderviti = mySessionObjects.ktheNdermarrjeVit(Session);

                return Request.KthePergjigjeJsonMeGeoSerializer(clsFunksioneGIS.merrTeDhenatGeoSnapGeoFc(layers, idnderviti, bbox, excludeGid, idPerdorues));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage MerrTeDhenatGeoPerLidhjeObjekteshNeSelect(JObject param)
        {
            try
            {
                string layer = param["layer"].Value<string>();
                string filterGid = param["filterGid"].Value<string>();
                string geometry = param["geometry"].Value<string>();
                string bbox = param["bbox"].Value<string>();
                int idObjFillestar = int.Parse(param["idObjFillestar"].Value<string>());
                int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);

                return Request.KthePergjigjeJsonMeGeoSerializer(clsFunksioneGIS.merrTeDhenatGeofc(Session, layer, bbox, filterGid, geometry, clsFunksioneGIS.LlojKonvertimiTrupFeatures.AllLidhjeWeb, idPerdorues, idObjFillestar));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage MerrTeDhenatGeoPerMergeObjekteshNeSelect(JObject param)
        {
            try
            {
                string layer = param["layer"].Value<string>();
                string filterGid = param["filterGid"].Value<string>();
                string geometry = param["geometry"].Value<string>();
                string bbox = param["bbox"].Value<string>();
                int idObjFillestar = int.Parse(param["idObjFillestar"].Value<string>());
                int idPerdorues = mySessionObjects.ktheIdPerdoruesi(Session);

                return Request.KthePergjigjeJsonMeGeoSerializer(clsFunksioneGIS.merrTeDhenatGeofc(Session, layer, bbox, filterGid, geometry, clsFunksioneGIS.LlojKonvertimiTrupFeatures.Merge, idPerdorues, idObjFillestar));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public object getRowsForEditWindow(JObject param)
        {
            try
            {
                int layerType = int.Parse(param["layerType"].Value<string>());
                int statusi = int.Parse(param["statusi"].Value<string>());

                return Request.KthePergjigje(GISRepository.getRowsForEditWindow(Session, layerType, statusi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        

        [HttpPost, HttpGet]
        public HttpResponseMessage merrGjeometryPerExtendSipasFunksionit(JObject param)
        {
            try
            {
                string fromQuery = param["fromQuery"].Value<string>();
                string whereQuery = param["whereQuery"].Value<string>();

                return Request.KthePergjigjeJsonMeGeoSerializer(clsFunksioneGIS.merrGjeometryPerExtendSipasFunksionitFc(fromQuery, whereQuery));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage DergoEmailNgaGIS(JObject param)
        {
            try
            {
                string toEmail = param["toEmail"].Value<string>();
                string fromName = param["fromName"].Value<string>();
                string fromEmail = param["fromEmail"].Value<string>();
                string subject = param["subject"].Value<string>();
                string bodyMesazh = param["bodyMesazh"].Value<string>();

                return Request.KthePergjigje(GISRepository.DergoEmailNgaGIS(Session, toEmail, fromName, fromEmail, subject, bodyMesazh));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage MerrKordinataPerZgjedhjeQV2017(JObject param)
        {
            try
            {
                string filter = param["filter"].Value<string>();
                return Request.KthePergjigje(GISRepository.merrKordinataPerZgjedhjeQV2017(filter));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        #endregion

        #region Menu 
        [HttpPost, HttpGet]
        public HttpResponseMessage getAllObjectsForQuickSearch(JObject param) 
        {
            try
            {
               return Request.KthePergjigje(GISRepository.getAllObjectsForQuickSearch(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage getObjectForQuickSearch(JObject param)
        {
            try
            {
                string gid = param["gid"].Value<string>();
                return Request.KthePergjigje(GISRepository.getObjectForQuickSearch(Session, gid));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage GetFeatureInfo(JObject param)
        {
            try
            {
                string gid = param["gId"].Value<string>();
                string layerName = param["layerName"].Value<string>();
                int indeksi = int.Parse(param["indeksi"].Value<string>());
                return Request.KthePergjigje(GISRepository.GetFeatureInfo(Session, gid, layerName, indeksi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        #endregion

        #region Skedare
        [HttpPost, HttpGet]
        public HttpResponseMessage merrTeGjitheSkedaretUpload(JObject param)
        {
            try
            {
                string tipiSkedar = param["lloji"].Value<string>();
                return Request.KthePergjigje(GISRepository.merrTeGjitheSkedaretUpload(Session, tipiSkedar));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        
        [HttpPost, HttpGet]
        public HttpResponseMessage merrTeGjitheSkedaretUploadPerObjektGeo(JObject param)
        {
            try
            {
                string idDytesoreGeo = param["vleraUnikeIdDytesore"].Value<string>();
                return Request.KthePergjigje(GISRepository.merrTeGjitheSkedaretUploadPerObjektGeo(Session, idDytesoreGeo));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage fshiSkedareUpload(JObject param)
        {
            try
            {
                int idSkedari = int.Parse(param["idSkedari"].Value<string>());
                string filename = param["filename"].Value<string>();
                string lloji = param["lloji"].Value<string>();
                string serverMapPath = HttpContext.Current.Server.MapPath("~/UploadFiles/");

                return Request.KthePergjigje(GISRepository.fshiSkedareUpload(Session, serverMapPath,  idSkedari, filename, lloji));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        #endregion

        #region Te Tjera

        [HttpPost, HttpGet]
        public HttpResponseMessage merrIdDytesoreUnike(JObject param)
        {
            try
            {
                HttpContext context = HttpContext.Current;
                return Request.KthePergjigje(clsFunksioneGIS.uniqid(context.Request.ServerVariables["REMOTE_ADDR"], true));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
        #endregion
    }
}