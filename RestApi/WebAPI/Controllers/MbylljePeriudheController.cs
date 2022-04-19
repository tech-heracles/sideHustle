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
    public class MbylljePeriudheController: ApiController, IRequiresSessionState
    {
        private HttpSessionState Session { get { return HttpContext.Current.Session; } }

        [HttpPost, HttpGet]
        public HttpResponseMessage GetPageConfigurations()
        {
            try
            {
                return Request.KthePergjigje(MbylljePeriudheRepository.GetPageConfigurations(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage GetWizardConfigurations()
        {
            try
            {
                return Request.KthePergjigje(MbylljePeriudheRepository.GetWizardConfigurations(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage GetMonths(JObject param )
        {
            try
            {
                int yearId = param["yearId"].ToObject<int>();
                int idModuli = param["entityId"].ToObject<int>();
                int idAction = param["actionId"].ToObject<int>();
                return Request.KthePergjigje(MbylljePeriudheRepository.GetMonths(Session, yearId, idModuli, idAction));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }
        }


        [HttpPost, HttpGet]
        public HttpResponseMessage StartActionMbylljePeriudhe(JObject param)
        {
            try
            {
                int idModuli = param["entityId"].ToObject<int>();
                int idAction = param["actionId"].ToObject<int>();
                int idNdermarrjeVit = param["yearId"].ToObject<int>();
                object muajtObj = param["months"].ToObject<object>();
                return Request.KthePergjigje(MbylljePeriudheRepository.StartActionMbylljePeriudhe(Session, idModuli, idAction, idNdermarrjeVit, muajtObj));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage StopActionMbylljePeriudhe(JObject param)
        {
            try
            {
                return Request.KthePergjigje(MbylljePeriudheRepository.StopActionMbylljePeriudhe(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }
        }

        [HttpPost, HttpGet]
        public HttpResponseMessage CheckRunningProcess()
        {
            try
            {
                return Request.KthePergjigje(MbylljePeriudheRepository.CheckRunningProcess(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }
        }


        [HttpPost, HttpGet]
        public HttpResponseMessage GetPeriodSummaryDetailsReport(JObject param)
        {
            try
            {
                string kodiModuli = param["entityCode"].ToObject<string>();
                int viti = param["year"].ToObject<int>();
                int muaji = param["month"].ToObject<int>();
                return Request.KthePergjigje(MbylljePeriudheRepository.GetPeriodSummaryDetailsReport(Session, kodiModuli, viti, muaji));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }
        }



        [HttpPost, HttpGet]
        public HttpResponseMessage GetPeriodSummaries()
        {
            try
            {
                return Request.KthePergjigje(MbylljePeriudheRepository.GetPeriodSummaries(Session));
            }
            catch (Exception ex)
            {
                return Request.KthePergjigjeGabim(ex);
            }
        }
    }
}
