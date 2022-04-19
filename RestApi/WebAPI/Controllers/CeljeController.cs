using System;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;
using DbCore.IMBUtils.Types;
using Newtonsoft.Json.Linq;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using DbCore;
using DbCore.DbShare;
using DbCore.IMBUtils.DataBase;

namespace RestApi.WebAPI.Controllers
{
    public class CeljeController : ApiController, IRequiresSessionState
    {

        [HttpPost, HttpGet]
        public HttpResponseMessage kontrolloEkzistonKodObjekti(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int idObjekti = param["idObjekti"].Value<int>();
                int idndermarje = param["idndermarje"].Value<int>();
                int idPerdorues = param["idPerdorues"].Value<int>();
                int kategoria = param["kategoria"].Value<int>();
                return Request.KthePergjigje(CeljeRepository.kontrolloEkzistonKodObjekti(kodi, idObjekti, idndermarje, idPerdorues, kategoria));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param,ex);
            }
        }
	    [HttpPost, HttpGet]
        public HttpResponseMessage ktheKFSipasKodit(JObject param)
        {
            try
            {
                string kodi = param["kodi"].Value<string>();
                int idNdermarrje;
                Converter.ParseExact(param["idNdermarrje"].Value<string>(), out idNdermarrje,"idNdermarrje");
                return Request.KthePergjigje(CeljeRepository.ktheKFSipasKodit(kodi, idNdermarrje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param,ex);
            }
        }
         [HttpPost, HttpGet]
        public HttpResponseMessage merrBuxhete(JObject param)
        {
            try
            {
                int id = param["idqk"].Value<int>();
                int idNderviti= param["idNderviti"].Value<int>();
                int idllojbuxheti = param["idllojbuxheti"].Value<int>();
                int idviti = param["idviti"].Value<int>();
                bool eshteprojekt = param["eshteprojekt"].Value<bool>();
                return Request.CreateResponse(HttpStatusCode.OK, CeljeRepository.merrBuxhete(id, idNderviti,idllojbuxheti,idviti,eshteprojekt));
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Ndodhi nje problem ne server!" + e.Message);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage merrQkPrind(JObject param)
        {
            try
            {
                int id = param["idqk"].Value<int>();
                return Request.KthePergjigje(CeljeRepository.merrQkPrind(id));
            }
            catch (Exception e)
            {
                return Request.KthePergjigjeGabim(param, e);
            }
        }
        [HttpPost, HttpGet]
        public HttpResponseMessage ktheImazhPerdoruesi(JObject param)
        {
            try
            {              
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(CeljeRepository.ktheImazhPerdoruesi(idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(null,ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage vendosThumbnailDefaultArkiva(JObject param)
        {
            try
            {
                var conn = clsArkiva.ktheConnString(MyConnectionsManager.GetSelectedConNameServer());
                int iddok=  int.Parse(((JValue)(param["idObjekti"])).Value.ToString());
                int idKatObjekti = int.Parse(((JValue)(param["idKategoriObjekti"])).Value.ToString());
                string ext = ((JValue)(param["extention"])).Value.ToString();
                string fullPath = ((JValue)(param["full"])).Value.ToString();//.Replace('/', '\\');
                string name = ((JValue)(param["name"])).Value.ToString();
                int idPerdoruesi = int.Parse(((JValue)(param["idPerdorues"])).Value.ToString());
                string c = CeljeRepository.vendosThumbnailDefaultArkiva(iddok, idKatObjekti, ext, fullPath, name, idPerdoruesi, conn);

              return Request.KthePergjigje(c);
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(null, ex);
            }
        }


    }
}
