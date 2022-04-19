using DbCore;
using Newtonsoft.Json.Linq;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.SessionState;

namespace RestApi.WebAPI.Controllers
{
    public class CmimeController : ApiController, IRequiresSessionState
    {
        private HttpSessionState Session { get { return HttpContext.Current.Session; } }
        private int idNdermarrje => mySessionObjects.merrIdNdermarrjeSesioni(Session);
        private int idPerdoruesi => mySessionObjects.ktheIdPerdoruesi(Session);

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheDataSourceKolonash(JObject param)
        {
            try
            {
                int shitjeApoBlerje = param.Value<int>("shitjeApoBlerje");
                return Request.KthePergjigje(CmimeRepository.KtheDataSourceKolonash(idNdermarrje, idPerdoruesi, shitjeApoBlerje));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheListeCmimesh(JObject param)
        {
            try
            {
                int shitjeApoBlerje = param.Value<int>("shitjeApoBlerje");
                bool lupe = param.Value<bool>("lupe");
                bool merrKosto = param.Value<bool>("merrKosto");
                int idNivelCmimi = param.Value<int>("idNivelCmimi");
                return Request.KthePergjigje(CmimeRepository.KtheListeCmimesh(idNdermarrje, idPerdoruesi, shitjeApoBlerje, lupe, merrKosto, idNivelCmimi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage RuajListeCmimesh(JObject param)
        {
            try
            {
                object cmimeObject = param.Value<object>("cmimeObject");
                int shitjeApoBlerje = param.Value<int>("shitjeApoBlerje");
                bool lupe = param.Value<bool>("lupe");
                bool merrKosto = param.Value<bool>("merrKosto");
                int idNivelCmimi = param.Value<int>("idNivelCmimi");
                return Request.KthePergjigje(CmimeRepository.RuajListeCmimesh(idNdermarrje, idPerdoruesi, cmimeObject, shitjeApoBlerje, lupe, merrKosto, idNivelCmimi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage KtheCmimArtikujshSipasNivelit(JObject param)
        {
            try
            {
                string artIds = param.Value<string>("artIds");
                int idNivelCmimi = param.Value<int>("idNivelCmimi");
                return Request.KthePergjigje(CmimeRepository.KtheCmimArtikujshSipasNivelit(Session, artIds, idNivelCmimi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage RuajFilterNivelCmimi(JObject param)
        {
            try
            {
                int idKonfigAmbjente = param.Value<int>("idKonfigAmbjente");
                int idNivelCmimi = param.Value<int>("idNivelCmimi");
                int idNdermarrje = param["idNdermarrje"].Value<int>();
                int idViti = param["idViti"].Value<int>();
                int idPerdoruesi = param["idPerdoruesi"].Value<int>();
                return Request.KthePergjigje(CmimeRepository.RuajFilterNivelCmimi(Session, idKonfigAmbjente, idNivelCmimi, idNdermarrje, idViti, idPerdoruesi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
    }
}
