using System;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using RestApi.WebAPI.ApiUtils;
using RestApi.WebAPI.Models;
using System.Web.SessionState;

namespace RestApi.WebAPI.Controllers
{
    /// <summary>
    /// controlleri i cili permban te gjitha web service-et qe do perdoren per analizen e buxhetit
    /// </summary>

    [Authorize]
    public class AnalizeBuxhetiController : ApiController, IRequiresSessionState
    {
        private System.Web.SessionState.HttpSessionState Session { get { return HttpContext.Current.Session; } }

        [HttpPost]
        public HttpResponseMessage KaVeprimeMeKeteFushe(JObject param)
        {
            try
            {
                int idAmbjenti = param.Value<int>("idAmbjenti");
                int idRreshti = param.Value<int>("idRreshti");
                return Request.KthePergjigje(AnalizeBuxhetiRepository.KaVeprimeMeKeteFushe(idAmbjenti, idRreshti));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage KontrolloNeseMundTeFshihetShpenzimiKonfig(JObject param)
        {
            try
            {
                int shokID = param.Value<int>("shokID");
                return ExtensionsMethods.KthePergjigje(Request, AnalizeBuxhetiRepository.KontrolloNeseMundTeFshihetShpenzimiKonfig(shokID));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage FshiDataSourceNgaSessioni()
        {
            try
            {
                return Request.KthePergjigje(DbCore.mySessionObjects.ruajObjectNeSesion(Session, null, "dataSourceRaporti"));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(null, ex);
            }
        }
        [HttpPost]
        public HttpResponseMessage MerrNivelinEShpenzimitOperativ(JObject param)
        {
            try
            {
                int idPrindi = param["idPrindi"].Value<int>();

                return Request.KthePergjigje(AnalizeBuxhetiRepository.MerrNivelinEShpenzimitOperativ(idPrindi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage KaVeprimeMeKeteParashikimShpenzimi(JObject param)
        {
            try
            {
                
                int idRreshti = param.Value<int>("idRreshti");
                return Request.KthePergjigje(AnalizeBuxhetiRepository.KaVeprimeMeKeteFusheParashikimi(idRreshti));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage EshtePerdorurPrindiParashikimShpenzimesh(JObject param)
        {
            try
            {

                int idRreshti = param.Value<int>("idRreshti");
                return Request.KthePergjigje(AnalizeBuxhetiRepository.EshtePerdorurPrindiParashikimShpenzimesh(idRreshti));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage KontrolloNeseMundTeFshihetZeriIProkurimit(JObject param)
        {
            try
            {
                int rpkId = param.Value<int>("rpkId");
                return ExtensionsMethods.KthePergjigje(Request, AnalizeBuxhetiRepository.KontrolloNeseMundTeFshihetZeriIProkurimit(rpkId));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }

        [HttpPost]
        public HttpResponseMessage MerrNivelinEZeritTeProkurimeve(JObject param)
        {
            try
            {
                int idPrindi = param["idPrindi"].Value<int>();

                return Request.KthePergjigje(AnalizeBuxhetiRepository.MerrNivelinEZeritTeProkurimeve(idPrindi));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(param, ex);
            }
        }
    }
}